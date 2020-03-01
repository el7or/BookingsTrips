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
    public class CustomerController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Customer
        public ActionResult Index(string searchText)
        {
            var customers = new List<CustomerListViewModel>();
            if (searchText != null && searchText != "")
            {
                customers = db.Customers.Where(c => c.Name.Contains(searchText) || c.Phone == searchText).Select(c => new CustomerListViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    Email = c.Email,
                    Phone = c.Phone,
                    LastCalls = c.Calls.OrderByDescending(d => d.CreatedOn).Take(3).Select(p => new LastCall
                    {
                        Note = p.Note,
                        CreatedOn = db.Users.Where(u => u.Id == p.CreatedBy).FirstOrDefault().CreatedOn
                    }).ToList()
                }).ToList();
            }
            return View(customers);

        }

        // GET: Customer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customer/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CustomerCreateViewModel model)
        {
            if (db.Customers.FirstOrDefault(u => u.Name == model.Name) != null)
            {
                ModelState.AddModelError("", "هذا العميل مسجل قبل ذلك !");
            }
            if (ModelState.IsValid)
            {
                var customer = new Customer
                {
                    Name = model.Name,
                    Email = model.Email,
                    Phone = model.Phone,
                    CreatedBy = User.Identity.GetUserId(),
                    CreatedOn = DateTime.Now,
                    EditedBy = User.Identity.GetUserId(),
                    EditedOn = DateTime.Now
                };
                db.Customers.Add(customer);
                var call = new Call
                {
                    Customer = customer,
                    Note = model.CallNote,
                    CreatedBy = User.Identity.GetUserId(),
                    CreatedOn = DateTime.Now,
                    EditedBy = User.Identity.GetUserId(),
                    EditedOn = DateTime.Now
                };
                db.Calls.Add(call);
                db.SaveChanges();
                return RedirectToAction("Index", new { searchText = model.Name });
            }

            return View(model);
        }

        // GET: Customer/CreateCall/5
        public ActionResult CreateCall(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            var CustomerCall = new CustomerCreateCallViewModel
            {
                CustomerId = customer.Id,
                Name = customer.Name,
                Phone = customer.Phone
            };
            return View(CustomerCall);
        }

        // POST: Customer/CreateCall/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCall(CustomerCreateCallViewModel model)
        {
            if (ModelState.IsValid)
            {
                var call = new Call
                {
                    CustomerId = model.CustomerId,
                    Note = model.CallNote,
                    CreatedBy = User.Identity.GetUserId(),
                    CreatedOn = DateTime.Now,
                    EditedBy = User.Identity.GetUserId(),
                    EditedOn = DateTime.Now
                };
                db.Calls.Add(call);
                db.SaveChanges();
                return RedirectToAction("Index", new { searchText = model.Name });
            }
            return View(model);
        }

        public ActionResult CustomerCalls(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            var customerCalls = db.Calls.Where(c => c.CustomerId == id).OrderByDescending(d => d.CreatedOn).Take(50)
                .Select(c => new CustomerCallListViewModel
                {
                    CreatedByName = db.Users.Where(u => u.Id == c.CreatedBy).FirstOrDefault().FullName,
                    CreatedOn = c.CreatedOn,
                    CallNote = c.Note
                }).ToList();
            ViewBag.CustomerName = customer.Name;
            return View(customerCalls);
        }

        //// GET: Customer/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Customer customer = db.Customers.Find(id);
        //    if (customer == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(customer);
        //}

        //// POST: Customer/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,Name,Phone,Email,IsActive,IsDeleted,CreatedBy,CreatedOn,EditedBy,EditedOn")] Customer customer)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(customer).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(customer);
        //}

        //// GET: Customer/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Customer customer = db.Customers.Find(id);
        //    if (customer == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(customer);
        //}

        //// POST: Customer/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Customer customer = db.Customers.Find(id);
        //    db.Customers.Remove(customer);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        //// GET: Customer/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Customer customer = db.Customers.Find(id);
        //    if (customer == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(customer);
        //}

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
