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
    public class InformationTypesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/InformationTypes
        public ActionResult Index()
        {
            return View(db.InformationTypes.ToList());
        }

        // GET: Admin/InformationTypes/Details/5
        public ActionResult Details(int? id)
        {
             
            return RedirectToAction("Index", "Information",new { InformationTypeId = id, Area = "Admin"});
        }

        // GET: Admin/InformationTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/InformationTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Extentions")] InformationType informationType)
        {
            if (ModelState.IsValid)
            {
                db.InformationTypes.Add(informationType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(informationType);
        }

        // GET: Admin/InformationTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InformationType informationType = db.InformationTypes.Find(id);
            if (informationType == null)
            {
                return HttpNotFound();
            }
            return View(informationType);
        }

        // POST: Admin/InformationTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Extentions")] InformationType informationType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(informationType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(informationType);
        }

        // GET: Admin/InformationTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InformationType informationType = db.InformationTypes.Find(id);
            if (informationType == null)
            {
                return HttpNotFound();
            }
            return View(informationType);
        }

        // POST: Admin/InformationTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            InformationType informationType = db.InformationTypes.Find(id);
            db.InformationTypes.Remove(informationType);
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
