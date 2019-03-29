using GulfSeal.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace GulfSeal.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,Editor")]
    public class VendorController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/Vendor
        public ActionResult Index()
        {
            return View(db.Vendors.ToList());
        }

        public ActionResult Details(int Id)
        {
            return View(db.Vendors.Find(Id));
        }


        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Vendor vondor)
        {

            if (ModelState.IsValid)
            {
                db.Vendors.Add(vondor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vondor);
        }


        public ActionResult Edit(int Id)
        {
            return View(db.Vendors.Find(Id));
        }

        [HttpPost]
        public ActionResult Edit(Vendor vondor)
        {

            if (ModelState.IsValid)
            { 
                db.Entry(vondor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vondor);
        }



        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vendor vendor = db.Vendors.Find(id);
            if (vendor == null)
            {
                return HttpNotFound();
            }
            return View(vendor);
        }

        // POST: Admin/Vendors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Vendor vendor = db.Vendors.Find(id);
            db.Vendors.Remove(vendor);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}