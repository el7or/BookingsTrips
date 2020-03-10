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
    public class BoatController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Boat
        public ActionResult Index()
        {
            var boats = db.Boats.OrderByDescending(d => d.CreatedOn).Take(50).Select(b => new BoatIndexViewModel
            {
                Id = b.Id,
                FromDate = b.FromDate,
                ToDate = b.ToDate,
                FloorsCount = b.FloorsCount,
                CabinsCount = b.Floors.Sum(f => f.FloorCabinsCount)
            });
            return View(boats.ToList());
        }

        // GET: Boat/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Boat boat = db.Boats.Find(id);
            if (boat == null)
            {
                return HttpNotFound();
            }
            return View(boat);
        }

        // GET: Boat/Create
        public ActionResult Create()
        {
            ViewBag.Tab = 0;
            return View();
        }

        // POST: Boat/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BoatCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var boat = new Boat
                {
                    FromDate = model.FromDate,
                    ToDate = model.ToDate,
                    FloorsCount = model.FloorsCount,
                    CreatedBy = User.Identity.GetUserId(),
                    CreatedOn = DateTime.Now,
                    EditedBy = User.Identity.GetUserId(),
                    EditedOn = DateTime.Now
                };
                db.Boats.Add(boat);
                for (int i = 0; i < boat.FloorsCount; i++)
                {
                    db.Floors.Add(new Floor
                    {
                        Boat = boat,
                        FloorNumber = i + 1,
                        FloorCabinsCount = 0,
                        FloorSingleCabinsCount = 0,
                        FloorDoubleCabinsCount = 0,
                        FloorTripleCabinsCount = 0,
                        CreatedBy = User.Identity.GetUserId(),
                        CreatedOn = DateTime.Now,
                        EditedBy = User.Identity.GetUserId(),
                        EditedOn = DateTime.Now
                    });
                }
                db.SaveChanges();
                ViewBag.Tab = 1;
                return View();
            }

            return View(model);
        }

        // GET: Boat/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Boat boat = db.Boats.Find(id);
            if (boat == null)
            {
                return HttpNotFound();
            }
            return View(boat);
        }

        // POST: Boat/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FromDate,ToDate,FloorsCount,IsActive,IsDeleted,CreatedBy,CreatedOn,EditedBy,EditedOn")] Boat boat)
        {
            if (ModelState.IsValid)
            {
                db.Entry(boat).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(boat);
        }

        // GET: Boat/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Boat boat = db.Boats.Find(id);
            if (boat == null)
            {
                return HttpNotFound();
            }
            return View(boat);
        }

        // POST: Boat/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Boat boat = db.Boats.Find(id);
            db.Boats.Remove(boat);
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
