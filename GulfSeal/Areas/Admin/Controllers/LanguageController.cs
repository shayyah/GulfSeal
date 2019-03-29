using GulfSeal.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GulfSeal.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class LanguageController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Language
        public ActionResult Index()
        {
            return View(db.Languages.ToList());
        }

        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Create(Language language)
        {

            if (ModelState.IsValid)
            {
                db.Languages.Add(language);
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
                            string pathString = System.IO.Path.Combine(originalDirectory.ToString(), "Flags");

                            string extension = Path.GetExtension(file.FileName);
                            var newFileName = imageName + extension;

                            bool isExists = System.IO.Directory.Exists(pathString);

                            if (!isExists)
                                System.IO.Directory.CreateDirectory(pathString);


                            var path = $"{pathString}\\{newFileName}";
                            file.SaveAs(path);


                            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority +
                                Request.ApplicationPath.TrimEnd('/') + "/";
                            language.FlagImageUrl = baseUrl + "/Files/Flags/" + newFileName;
                            language.FlagImageName = newFileName;


                        }
                    }

                    db.SaveChanges();
                }
                catch (Exception e)
                {

                }


                return RedirectToAction("Index", "Language", new { area = "Admin" });
            }
            return View();
        }


        public ActionResult Edit(int id)
        {
            return View(db.Languages.Find(id));
        }


        [HttpPost]
        public ActionResult Edit(Language language)
        {
            if (ModelState.IsValid)
            {
                db.Entry(language).State = EntityState.Modified;

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
                            string old_pathString = System.IO.Path.Combine(old_originalDirectory.ToString(), "Flags");
                            var oldFileName = language.FlagImageName;
                            var old_path = $"{old_pathString}\\{oldFileName}";
                            //delete old files
                            if (System.IO.File.Exists(old_path))
                                System.IO.File.Delete(old_path);


                            var originalDirectory = new DirectoryInfo($"{Server.MapPath(@"\")}Files");
                            string pathString = System.IO.Path.Combine(originalDirectory.ToString(), "Flags");

                            string extension = Path.GetExtension(file.FileName);
                            var newFileName = imageName + extension;

                            bool isExists = System.IO.Directory.Exists(pathString);

                            if (!isExists)
                                System.IO.Directory.CreateDirectory(pathString);


                            var path = $"{pathString}\\{newFileName}";
                            file.SaveAs(path);


                            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority +
                                Request.ApplicationPath.TrimEnd('/') + "/";
                            language.FlagImageUrl = baseUrl + "/Files/Flags/" + newFileName;
                            language.FlagImageName = newFileName;


                        }
                    }

                    db.SaveChanges();
                }
                catch (Exception e)
                {

                }


                return RedirectToAction("Index", "Language", new { area = "Admin" });
            }
            return View(language);
        }


        public ActionResult Delete(int id)
        {
            return View(db.Languages.Find(id));
        }


        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id) {

            Language language = db.Languages.Find(id);

            var old_originalDirectory = new DirectoryInfo($"{Server.MapPath(@"\")}Files");
            string old_pathString = System.IO.Path.Combine(old_originalDirectory.ToString(), "Flags");
            var oldFileName = language.FlagImageName;
            var old_path = $"{old_pathString}\\{oldFileName}";
            //delete old files
            if (System.IO.File.Exists(old_path))
                System.IO.File.Delete(old_path);

            db.Languages.Remove(language);
            db.SaveChanges();
            return RedirectToAction("Index", "Language", new { area = "Admin" });
        }

    }
}
