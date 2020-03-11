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
                    var floor = new Floor
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
                    };
                    db.Floors.Add(floor);
                    for (int ii = 0; ii < model.UsersCount; ii++)
                    {
                        db.UserCabinsCounts.Add(new UserCabinsCount
                        {
                            Floor = floor,
                            UserId = User.Identity.GetUserId(),
                            UserSingleCabinsCount = 0,
                            UserDoubleCabinsCount = 0,
                            UserTripleCabinsCount = 0,
                            CreatedBy = User.Identity.GetUserId(),
                            CreatedOn = DateTime.Now,
                            EditedBy = User.Identity.GetUserId(),
                            EditedOn = DateTime.Now
                        });
                    }
                }
                db.SaveChanges();

                ViewData["Tab1Model"] = boat.Floors.Select(f => new FloorCabinsCountViewModel
                {
                    Id = f.Id,
                    FloorNumber = f.FloorNumber,
                    FloorSingleCabinsCount = f.FloorSingleCabinsCount,
                    FloorDoubleCabinsCount = f.FloorDoubleCabinsCount,
                    FloorTripleCabinsCount = f.FloorTripleCabinsCount
                }).ToList();
                ViewBag.Tab = 1;
                TempData["alert"] = "<script>Swal.fire({icon: 'success', title: 'تم الحفظ بنجاح.', showConfirmButton: false, timer: 1500})</script>";
                return View();
            }
            ViewBag.Tab = 0;
            return View(model);
        }

        // POST: Boat/EditFloorCabinsCount
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditFloorCabinsCount(IEnumerable<FloorCabinsCountViewModel> model)
        {
            if (ModelState.IsValid)
            {
                List<UserCabinsCountViewModel> tab2Model = new List<UserCabinsCountViewModel>();

                foreach (var item in model)
                {
                    var floor = db.Floors.Include(f =>f.UserCabinsCounts).FirstOrDefault(i =>i.Id== item.Id);
                    floor.FloorCabinsCount = item.FloorSingleCabinsCount + item.FloorDoubleCabinsCount + item.FloorTripleCabinsCount;
                    floor.FloorSingleCabinsCount = item.FloorSingleCabinsCount;
                    floor.FloorDoubleCabinsCount = item.FloorDoubleCabinsCount;
                    floor.FloorTripleCabinsCount = item.FloorTripleCabinsCount;
                    floor.EditedBy = User.Identity.GetUserId();
                    floor.EditedOn = DateTime.Now;
                    db.Entry(floor).State = EntityState.Modified;

                    var floorCabinsAssignCount = new UserCabinsCountViewModel
                    {
                        Id = item.Id,
                        FloorNumber = item.FloorNumber,
                        FloorSingleCabinsCount = item.FloorSingleCabinsCount,
                        FloorSingleCabinsAssignedCount = 0,
                        FloorDoubleCabinsCount = item.FloorDoubleCabinsCount,
                        FloorDoubleCabinsAssignedCount = 0,
                        FloorTripleCabinsCount = item.FloorTripleCabinsCount,
                        FloorTripleCabinsAssignedCount = 0,
                        FloorCabinsUsers = floor.UserCabinsCounts.Select(u => new FloorCabinsUser
                        {
                            Id = u.Id,
                            UserId = u.UserId,
                            UserSingleCabinsCount = u.UserSingleCabinsCount,                            
                            UserDoubleCabinsCount = u.UserDoubleCabinsCount,
                            UserTripleCabinsCount = u.UserTripleCabinsCount
                        }).ToList()
                    };
                    tab2Model.Add(floorCabinsAssignCount);
                }
                db.SaveChanges();

                List<SelectListItem> usersList = db.Users.Select(u => new SelectListItem()
                {
                    Value = u.Id,
                    Text = u.FullName
                }).ToList();
                ViewBag.Users = usersList;
                ViewData["Tab2Model"] = tab2Model;
                ViewBag.Tab = 2;
                TempData["alert"] = "<script>Swal.fire({icon: 'success', title: 'تم الحفظ بنجاح.', showConfirmButton: false, timer: 1500})</script>";
                return View("Create");
            }
            ViewData["Tab1Model"] = model;
            ViewBag.Tab = 1;
            return View("Create");
        }

        // Post: Boat/EditUserCabinsCount/5
        [HttpPost]
        public ActionResult EditUserCabinsCount(IEnumerable<UserCabinsCountViewModel> model)
        {
            if (ModelState.IsValid)
            {
                foreach (var item in model)
                {
                    foreach (var userCabins in item.FloorCabinsUsers)
                    {
                        var userFloorCabins = db.UserCabinsCounts.Find(userCabins.Id);
                        userFloorCabins.UserId = userCabins.UserId;
                        userFloorCabins.UserSingleCabinsCount = userCabins.UserSingleCabinsCount;
                        userFloorCabins.UserDoubleCabinsCount = userCabins.UserDoubleCabinsCount;
                        userFloorCabins.UserTripleCabinsCount = userCabins.UserTripleCabinsCount;
                        userFloorCabins.EditedBy = User.Identity.GetUserId();
                        userFloorCabins.EditedOn = DateTime.Now;
                        db.Entry(userFloorCabins).State = EntityState.Modified;
                    }
                }
                db.SaveChanges();
                TempData["alert"] = "<script>Swal.fire({icon: 'success', title: 'تم الحفظ بنجاح.', showConfirmButton: false, timer: 1500})</script>";
                return RedirectToAction("Index");
            }
                List<SelectListItem> usersList = db.Users.Select(u => new SelectListItem()
            {
                Value = u.Id,
                Text = u.FullName
            }).ToList();
            ViewBag.Users = usersList;
            ViewBag.Tab = 2;
            ViewData["Tab2Model"] = model;
            return View("Create");
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

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        //// GET: Boat/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Boat boat = db.Boats.Find(id);
        //    if (boat == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(boat);
        //}

        //// GET: Boat/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Boat boat = db.Boats.Find(id);
        //    if (boat == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(boat);
        //}

        //// POST: Boat/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Boat boat = db.Boats.Find(id);
        //    db.Boats.Remove(boat);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}
    }
}
