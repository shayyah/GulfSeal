using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GulfSeal.Models;

namespace GulfSeal.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,Editor")]
    public class FamiliesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Families
        public ActionResult Index()
        {
            return View(db.Families.OrderByDescending(x=>x.Rank).ToList());
        }

         

        // GET: Admin/Families/Create
        public ActionResult Create()
        {
            return View();
        }
         
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Family family)
        {
            if (ModelState.IsValid)
            {
                db.Families.Add(family);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(family);
        }
         
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Family family = db.Families.Find(id);
            if (family == null)
            {
                return HttpNotFound();
            }
            return View(family);
        }
         
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Family family)
        {
            if (ModelState.IsValid)
            {
                db.Entry(family).State = EntityState.Modified;
                family.LastUpdatedAt = DateTime.Now;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(family);
        }
         
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Family family = db.Families.Find(id);
            if (family == null)
            {
                return HttpNotFound();
            }
            return View(family);
        }
         
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Family family = db.Families.Find(id);
            db.Families.Remove(family);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
         
    }
}
