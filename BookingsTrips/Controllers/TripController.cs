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
                    TripTripleCabinsPrice = f.TripTripleCabinsPrice,
                    FloorSingleCabinsCount = f.Floor.FloorSingleCabinsCount,
                    FloorDoubleCabinsCount = f.Floor.FloorDoubleCabinsCount,
                    FloorTripleCabinsCount = f.Floor.FloorTripleCabinsCount
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
                Id = trip.Id,
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
                BoatId = db.Boats.FirstOrDefault(b => b.TripId == trip.Id).Id
            };
            List<SelectListItem> flights = db.Flights.OrderByDescending(d => d.CreatedOn).Take(50).ToList().Select(r => new SelectListItem()
            {
                Value = r.Id.ToString(),
                Text = r.FromAirport + " إلى " + r.ToAirport + " - تاريخ " + r.FromDate.ToString("yyyy/MM/dd")
            }).ToList();
            ViewBag.Flights = flights;
            List<SelectListItem> boats = db.Boats.OrderByDescending(d => d.CreatedOn).Take(50).ToList().Select(r => new SelectListItem()
            {
                Value = r.Id.ToString(),
                Text = "من تاريخ " + r.FromDate.ToString("yyyy/MM/dd") + " إلى تاريخ " + r.ToDate.ToString("yyyy/MM/dd")
            }).ToList();
            ViewBag.Boats = boats;

            ViewBag.Tab = 0;
            return View(model);
        }

        // POST: Trip/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TripEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var trip = db.Trips.Include(p => p.TripCabinsPrices).FirstOrDefault(t => t.Id == model.Id);
                trip.FromDate = model.FromDate;
                trip.ToDate = model.ToDate;
                trip.StartPoint = model.StartPoint;
                trip.EndPoint = model.EndPoint;
                trip.Cost = model.Cost;
                trip.AdultPrice = model.AdultPrice;
                trip.TeenPrice = model.TeenPrice;
                trip.ChildPrice = model.ChildPrice;
                trip.BabyPrice = model.BabyPrice;
                trip.EditedBy = User.Identity.GetUserId();
                trip.EditedOn = DateTime.Now;
                db.TripCabinsPrices.RemoveRange(trip.TripCabinsPrices);
                db.Entry(trip).State = EntityState.Modified;

                var tripFlight = db.Flights.Find(model.FlightId);
                if (tripFlight.TripId != trip.Id)
                {
                    tripFlight.Trip = trip;
                    db.Entry(tripFlight).State = EntityState.Modified;
                }
                var tripBoat = db.Boats.Include(b => b.Floors).SingleOrDefault(i => i.Id == model.BoatId);
                if (tripBoat.TripId != trip.Id)
                {
                    tripBoat.Trip = trip;
                    db.Entry(tripBoat).State = EntityState.Modified;
                }

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
                    TripTripleCabinsPrice = f.TripTripleCabinsPrice,
                    FloorSingleCabinsCount = f.Floor.FloorSingleCabinsCount,
                    FloorDoubleCabinsCount = f.Floor.FloorDoubleCabinsCount,
                    FloorTripleCabinsCount = f.Floor.FloorTripleCabinsCount
                }).ToList();
                ViewBag.Tab = 1;
                TempData["alert"] = "<script>Swal.fire({icon: 'success', title: 'تم الحفظ بنجاح', showConfirmButton: false, timer: 1500})</script>";
                return View();
            }
            List<SelectListItem> flights = db.Flights.OrderByDescending(d => d.CreatedOn).Take(50).ToList().Select(r => new SelectListItem()
            {
                Value = r.Id.ToString(),
                Text = r.FromAirport + " إلى " + r.ToAirport + " - تاريخ " + r.FromDate.ToString("yyyy/MM/dd")
            }).ToList();
            ViewBag.Flights = flights;
            List<SelectListItem> boats = db.Boats.OrderByDescending(d => d.CreatedOn).Take(50).ToList().Select(r => new SelectListItem()
            {
                Value = r.Id.ToString(),
                Text = "من تاريخ " + r.FromDate.ToString("yyyy/MM/dd") + " إلى تاريخ " + r.ToDate.ToString("yyyy/MM/dd")
            }).ToList();
            ViewBag.Boats = boats;

            ViewBag.Tab = 0;
            return View(model);
        }

        // POST: Trip/EditTripCabinsPrice
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

        // GET: Trip/AddTransport/5
        public ActionResult AddTransport(int? id)
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
            var model = new TripAddTransportViewModel
            {
                Id = trip.Id
            };
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

            return View();
        }

        // POST: Trip/AddTransport
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddTransport(TripAddTransportViewModel model)
        {
            if (model.FlightId != null || model.BoatId != null)
            {
                Trip trip = db.Trips.Find(model.Id);
                if (trip == null)
                {
                    return HttpNotFound();
                }
                if (model.FlightId != null)
                {
                    var tripFlight = db.Flights.Find(model.FlightId);
                    tripFlight.Trip = trip;
                    db.Entry(tripFlight).State = EntityState.Modified;
                }
                if (model.BoatId != null)
                {
                    var tripBoat = db.Boats.Include(b => b.Floors).SingleOrDefault(i => i.Id == model.BoatId);
                    tripBoat.Trip = trip;
                    db.Entry(tripBoat).State = EntityState.Modified;
                }

                db.SaveChanges();
                TempData["alert"] = "<script>Swal.fire({icon: 'success', title: 'تم الحفظ بنجاح', showConfirmButton: false, timer: 1500})</script>";
            }
            else
            {
                TempData["alert"] = "<script>Swal.fire({icon: 'warning', title: 'لم يتم أي تغيير !', showConfirmButton: false, timer: 1500})</script>";
            }
            return RedirectToAction("Index");
        }

        // GET: Trip/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trip trip = db.Trips.Include(t => t.TripCabinsPrices).Include("TripCabinsPrices.Floor").FirstOrDefault(i => i.Id==id);
            if (trip == null)
            {
                return HttpNotFound();
            }
            var model = new TripDetailsViewModel
            {
                Id = trip.Id,
                FromDate = trip.FromDate,
                ToDate = trip.ToDate,
                StartPoint = trip.StartPoint,
                EndPoint = trip.EndPoint,
                Cost = trip.Cost,
                AdultPrice = trip.AdultPrice,
                TeenPrice = trip.TeenPrice,
                ChildPrice = trip.ChildPrice,
                BabyPrice = trip.BabyPrice,
                CreatedBy = db.Users.Where(u => u.Id == trip.CreatedBy).FirstOrDefault().FullName,
                CreatedOn = trip.CreatedOn,
                EditedBy = db.Users.Where(u => u.Id == trip.EditedBy).FirstOrDefault().FullName,
                EditedOn = trip.EditedOn,
                FlightsIds = db.Flights.Where(f => f.TripId==trip.Id).ToList().Select(r => r.FromAirport + " إلى " + r.ToAirport + " - تاريخ " + r.FromDate.ToString("yyyy/MM/dd")).ToArray(),
                BoatsIds = db.Boats.Where(f => f.TripId == trip.Id).ToList().Select(r  => "من تاريخ " + r.FromDate.ToString("yyyy/MM/dd") + " إلى تاريخ " + r.ToDate.ToString("yyyy/MM/dd")).ToArray(),
                TripDetailsCabinsPrices = trip.TripCabinsPrices.Select(f => new TripDetailsCabinsPrice
                {
                    FloorNumber = f.Floor.FloorNumber,
                    TripSingleCabinsPrice = f.TripSingleCabinsPrice,
                    TripDoubleCabinsPrice = f.TripDoubleCabinsPrice,
                    TripTripleCabinsPrice = f.TripTripleCabinsPrice,
                    FloorSingleCabinsCount = f.Floor.FloorSingleCabinsCount,
                    FloorDoubleCabinsCount = f.Floor.FloorDoubleCabinsCount,
                    FloorTripleCabinsCount = f.Floor.FloorTripleCabinsCount
                }).ToList()
            };
            return View(model);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

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
