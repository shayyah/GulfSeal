using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GulfSeal.Models;
using System.IO;
using System.Web.Script.Serialization;

namespace GulfSeal.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,Editor")]
    public class partenrsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/partenrs
        public ActionResult Index()
        {
            return View(db.partenrs.ToList());
        }

        // GET: Admin/partenrs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            partenrs partenrs = db.partenrs.Find(id);
            if (partenrs == null)
            {
                return HttpNotFound();
            }
            return View(partenrs);
        }

        // GET: Admin/partenrs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/partenrs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create([Bind(Include = "Id,LogoLink,name,order")] partenrs partenrs)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var fName = "";
                    foreach (string fileName in Request.Files)
                    {
                        Guid imageName = Guid.NewGuid();

                        HttpPostedFileBase file = Request.Files[fileName];
                        fName = file.FileName;
                        if (file != null && file.ContentLength > 0)
                        {
                            var originalDirectory = new DirectoryInfo($"{Server.MapPath(@"\")}Files");
                            string pathString = System.IO.Path.Combine(originalDirectory.ToString(), "partenrs");

                            string extension = Path.GetExtension(file.FileName);
                            var newFileName = imageName + extension;

                            bool isExists = System.IO.Directory.Exists(pathString);

                            if (!isExists)
                                System.IO.Directory.CreateDirectory(pathString);


                            var path = $"{pathString}\\{newFileName}";
                            file.SaveAs(path);


                            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority +
                                Request.ApplicationPath.TrimEnd('/') + "/";

                            partenrs.LogoLink= baseUrl + "/Files/partenrs/" + newFileName;
                            db.partenrs.Add(partenrs);
                            db.SaveChanges();
                            return RedirectToAction("Index", "partenrs", new { Area = "Admin" });

                        }
                    }
                }
                catch (Exception e)
                {
                    return View(partenrs);
                }
            }
            return RedirectToAction("Index", "partenrs", new { Area = "Admin" });
        }

        // GET: Admin/partenrs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            partenrs partenrs = db.partenrs.Find(id);
            if (partenrs == null)
            {
                return HttpNotFound();
            }
            return View(partenrs);
        }

        // POST: Admin/partenrs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,LogoLink,name,order")] partenrs partenrs)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var fName = "";
                    foreach (string fileName in Request.Files)
                    {
                        Guid imageName = Guid.NewGuid();

                        HttpPostedFileBase file = Request.Files[fileName];
                        fName = file.FileName;
                        if (file != null && file.ContentLength > 0)
                        {
                            var old_originalDirectory = new DirectoryInfo($"{Server.MapPath(@"\")}Files");
                            string old_pathString = System.IO.Path.Combine(old_originalDirectory.ToString(), "partenrs");
                            //C:\\Users\\خالد\\Desktop\\Projects\\GulfSeal\\GulfSeal\\Files\\partenrs"
                            //"http://localhost:49944//Files/partenrs/14792958-a7d1-4dac-a903-a78b8dac1912.jpg"
                            var oldFileName = partenrs.LogoLink.Split('/').Last().ToString();



                            var old_path = old_pathString+"\\"+ oldFileName;

                            //delete old files
                            if (System.IO.File.Exists(old_path))
                                System.IO.File.Delete(old_path);


                            //add new logo
                            var originalDirectory = new DirectoryInfo($"{Server.MapPath(@"\")}Files");
                            string pathString = System.IO.Path.Combine(originalDirectory.ToString(), "partenrs");

                            string extension = Path.GetExtension(file.FileName);
                            var newFileName = imageName + extension;

                            bool isExists = System.IO.Directory.Exists(pathString);

                            if (!isExists)
                                System.IO.Directory.CreateDirectory(pathString);


                            var path = $"{pathString}\\{newFileName}";
                            file.SaveAs(path);


                            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority +
                                Request.ApplicationPath.TrimEnd('/') + "/";




                            partenrs.LogoLink = baseUrl + "/Files/partenrs/" + newFileName;
                            

                        }
                    }
                }
                catch (Exception e)
                {
                    return View(partenrs);
                }
                db.Entry(partenrs).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "partenrs", new { Area = "Admin" });
            }
            return View(partenrs);
        }


        public string GetPartnerMedia(int id)
        {
            List<string> links_list = db.partenrs.Where(x => x.Id == id).Select(y => y.LogoLink).ToList();
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(links_list);
            return json;
        }

        // GET: Admin/partenrs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            partenrs partenrs = db.partenrs.Find(id);
            if (partenrs == null)
            {
                return HttpNotFound();
            }
            return View(partenrs);
        }

        // POST: Admin/partenrs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            partenrs partenrs = db.partenrs.Find(id);
            //db.partenrs.Remove(partenrs);
            //db.SaveChanges();
            //return RedirectToAction("Index");

            //delete from database
            string image_name_with_extention = partenrs.LogoLink.Split('/').Last();
            string image_name = image_name_with_extention.Split('.').First();
            db.partenrs.Remove(partenrs);
            db.SaveChanges();

            var old_originalDirectory = new DirectoryInfo($"{Server.MapPath(@"\")}Files");
            string old_pathString = System.IO.Path.Combine(old_originalDirectory.ToString(), "partenrs");

            var old_path = $"{old_pathString}\\{image_name_with_extention}";
            //delete old files

            //delete from server
            if (System.IO.File.Exists(old_path))
                System.IO.File.Delete(old_path);

            return RedirectToAction("Index", "partenrs", new { Area = "Admin" });


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
