using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BookingsTrips.Models;
using BookingsTrips.Models.ViewModels;
using Microsoft.AspNet.Identity;

namespace BookingsTrips.Controllers
{
    public class TripController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Trip
        public ActionResult Index()
        {
            var model = db.Trips.OrderByDescending(d => d.CreatedOn).Take(50).Select(t => new TripIndexViewModel
            {
                Id = t.Id,
                StartPoint = t.StartPoint,
                EndPoint = t.EndPoint,
                FromDate = t.FromDate,
                ToDate = t.ToDate
            });
            return View(model.ToList());
        }

        // GET: Trip/Create
        public ActionResult Create()
        {
            List<SelectListItem> flights = db.Flights.OrderByDescending(d => d.CreatedOn).Take(50).Where(t => t.TripId==null).ToList().Select(r => new SelectListItem()
            {
                Value = r.Id.ToString(),
                Text = r.FromAirport + " إلى " + r.ToAirport + " - تاريخ " + r.FromDate.ToString("yyyy/MM/dd")
            }).ToList();
            ViewBag.Flights = flights;
            List<SelectListItem> boats = db.Boats.OrderByDescending(d => d.CreatedOn).Take(50).Where(t => t.TripId == null).ToList().Select(r => new SelectListItem()
            {
                Value = r.Id.ToString(),
                Text = "من تاريخ " + r.FromDate.ToString("yyyy/MM/dd") + " إلى تاريخ " + r.ToDate.ToString("yyyy/MM/dd")
            }).ToList();
            ViewBag.Boats = boats;

            ViewBag.Tab = 0;
            return View();
        }

        // POST: Trip/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TripCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var trip = new Trip
                {
                    FromDate = model.FromDate,
                    ToDate = model.ToDate,
                    StartPoint = model.StartPoint,
                    EndPoint = model.EndPoint,
                    Cost = model.Cost,
                    AdultPrice = model.AdultPrice,
                    TeenPrice = model.TeenPrice,
                    ChildPrice = model.ChildPrice,
                    BabyPrice = model.BabyPrice,
                    CreatedBy = User.Identity.GetUserId(),
                    CreatedOn = DateTime.Now,
                    EditedBy = User.Identity.GetUserId(),
                    EditedOn = DateTime.Now
                };
                db.Trips.Add(trip);
                var tripFlight = db.Flights.Find(model.FlightId);
                tripFlight.Trip = trip;
                db.Entry(tripFlight).State = EntityState.Modified;

                var tripBoat = db.Boats.Include(b => b.Floors).SingleOrDefault(i => i.Id == model.BoatId);
                tripBoat.Trip = trip;
                db.Entry(tripBoat).State = EntityState.Modified;

                foreach (var floor in tripBoat.Floors)
                {
                    db.TripCabinsPrices.Add(new TripCabinsPrice
                    {
                        Trip = trip,
                        Floor = floor,
                        TripSingleCabinsPrice = 0,
                        TripDoubleCabinsPrice = 0,
                        TripTripleCabinsPrice = 0,
                        CreatedBy = User.Identity.GetUserId(),
                        CreatedOn = DateTime.Now,
                        EditedBy = User.Identity.GetUserId(),
                        EditedOn = DateTime.Now
                    });
                }
                db.SaveChanges();

                ViewData["Tab1Model"] = trip.TripCabinsPrices.Select(f => new TripCabinsPriceViewModel
                {
                    Id = f.Id,
                    FloorNumber = f.Floor.FloorNumber,
                    TripSingleCabinsPrice = f.TripSingleCabinsPrice,
                    TripDoubleCabinsPrice = f.TripDoubleCabinsPrice,
                    TripTripleCabinsPrice = f.TripTripleCabinsPrice
                }).ToList();
                ViewBag.Tab = 1;
                TempData["alert"] = "<script>Swal.fire({icon: 'success', title: 'تم الحفظ بنجاح', showConfirmButton: false, timer: 1500})</script>";
                return View();
            }
            List<SelectListItem> flights = db.Flights.OrderByDescending(d => d.CreatedOn).Take(50).Where(t => t.TripId == null).ToList().Select(r => new SelectListItem()
            {
                Value = r.Id.ToString(),
                Text = r.FromAirport + " إلى " + r.ToAirport + " - تاريخ " + r.FromDate.ToString("yyyy/MM/dd")
            }).ToList();
            ViewBag.Flights = flights;
            List<SelectListItem> boats = db.Boats.OrderByDescending(d => d.CreatedOn).Take(50).Where(t => t.TripId == null).ToList().Select(r => new SelectListItem()
            {
                Value = r.Id.ToString(),
                Text = "من تاريخ " + r.FromDate.ToString("yyyy/MM/dd") + " إلى تاريخ " + r.ToDate.ToString("yyyy/MM/dd")
            }).ToList();
            ViewBag.Boats = boats;
            ViewBag.Tab = 0;
            return View(model);
        }

        // GET: Trip/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trip trip = db.Trips.Find(id);
            if (trip == null)
            {
                return HttpNotFound();
            }
            var model = new TripEditViewModel
            {
                Id  =trip.Id,
                FromDate = trip.FromDate,
                ToDate = trip.ToDate,
                StartPoint = trip.StartPoint,
                EndPoint = trip.EndPoint,
                Cost = trip.Cost,
                AdultPrice = trip.AdultPrice,
                TeenPrice = trip.TeenPrice,
                ChildPrice = trip.ChildPrice,
                BabyPrice = trip.BabyPrice,
                FlightId = db.Flights.FirstOrDefault(f => f.TripId == trip.Id).Id,
                BoatId = db.Boats.FirstOrDefault(f => f.TripId == trip.Id).Id
            };
            ViewBag.Tab = 0;
            return View(model);
        }

        // POST: Trip/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,StartPoint,EndPoint,FromDate,ToDate,Cost,AdultPrice,TeenPrice,ChildPrice,BabyPrice,IsActive,IsDeleted,CreatedBy,CreatedOn,EditedBy,EditedOn")] Trip trip)
        {
            if (ModelState.IsValid)
            {
                db.Entry(trip).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(trip);
        }
        
        // POST: Boat/EditFloorCabinsCount
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditTripCabinsPrice(IEnumerable<TripCabinsPriceViewModel> model)
        {
            if (ModelState.IsValid)
            {
                foreach (var item in model)
                {
                    var floor = db.TripCabinsPrices.Find(item.Id);
                    floor.TripSingleCabinsPrice = item.TripSingleCabinsPrice;
                    floor.TripDoubleCabinsPrice = item.TripDoubleCabinsPrice;
                    floor.TripTripleCabinsPrice = item.TripTripleCabinsPrice;
                    floor.EditedBy = User.Identity.GetUserId();
                    floor.EditedOn = DateTime.Now;
                    db.Entry(floor).State = EntityState.Modified;                    
                }
                db.SaveChanges();
                TempData["alert"] = "<script>Swal.fire({icon: 'success', title: 'تم الحفظ بنجاح', showConfirmButton: false, timer: 1500})</script>";
                return RedirectToAction("Index");
            }
            ViewData["Tab1Model"] = model;
            ViewBag.Tab = 1;
            return View("Create");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        //// GET: Trip/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Trip trip = db.Trips.Find(id);
        //    if (trip == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(trip);
        //}

        //// GET: Trip/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Trip trip = db.Trips.Find(id);
        //    if (trip == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(trip);
        //}

        //// POST: Trip/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Trip trip = db.Trips.Find(id);
        //    db.Trips.Remove(trip);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}
    }
}
