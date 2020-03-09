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
    public class FlightController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Flight
        public ActionResult Index()
        {
            var flights = db.Flights.OrderByDescending(d => d.CreatedOn).Take(50).Select(f => new FlightIndexViewModel
            {
                Id = f.Id,
                FromAirport = f.FromAirport,
                ToAirport = f.ToAirport,
                FromDate = f.FromDate,
                Seats = f.Seats,
                Price = f.Price
            });
            return View(flights.ToList());
        }

        // GET: Flight/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Flight flight = db.Flights.Find(id);
            if (flight == null)
            {
                return HttpNotFound();
            }
            var model = new FlightDetailsViewModel
            {
                Id = flight.Id,
                FromAirport = flight.FromAirport,
                ToAirport = flight.ToAirport,
                FromDate = flight.FromDate,
                ToDate = flight.ToDate,
                Seats = flight.Seats,
                SeatsAvailable = flight.Seats,
                Cost = flight.Cost,
                Price = flight.Price,
                CreatedBy = db.Users.Where(u => u.Id == flight.CreatedBy).FirstOrDefault().FullName,
                CreatedOn = flight.CreatedOn
            };
            return View(model);
        }

        // GET: Flight/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Flight/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FlightCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var flight = new Flight
                {
                    FromAirport = model.FromAirport,
                    ToAirport = model.ToAirport,
                    FromDate = model.FromDate,
                    ToDate = model.ToDate,
                    Seats = model.Seats,
                    Cost = model.Cost,
                    Price = model.Price,
                    CreatedBy = User.Identity.GetUserId(),
                    CreatedOn = DateTime.Now,
                    EditedBy = User.Identity.GetUserId(),
                    EditedOn = DateTime.Now
                };
                db.Flights.Add(flight);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: Flight/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Flight flight = db.Flights.Find(id);
            if (flight == null)
            {
                return HttpNotFound();
            }
            var model = new FlightEditViewModel
            {
                Id = flight.Id,
                FromAirport = flight.FromAirport,
                ToAirport = flight.ToAirport,
                FromDate = flight.FromDate,
                ToDate = flight.ToDate,
                Seats = flight.Seats,
                Cost = flight.Cost,
                Price = flight.Price
            };
            return View(model);
        }

        // POST: Flight/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FlightEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var flight = db.Flights.Find(model.Id);
                flight.Id = model.Id;
                flight.FromAirport = model.FromAirport;
                flight.ToAirport = model.ToAirport;
                flight.FromDate = model.FromDate;
                flight.ToDate = model.ToDate;
                flight.Seats = model.Seats;
                flight.Cost = model.Cost;
                flight.Price = model.Price;
                flight.EditedBy = User.Identity.GetUserId();
                flight.EditedOn = DateTime.Now;
                db.Entry(flight).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: Flight/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Flight flight = db.Flights.Find(id);
            if (flight == null)
            {
                return HttpNotFound();
            }
            return View(flight);
        }

        // POST: Flight/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Flight flight = db.Flights.Find(id);
            db.Flights.Remove(flight);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
