using GulfSeal.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GulfSeal.Areas.Admin.Controllers
{
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
                            language.Flag = baseUrl + "/Files/Flags/" + newFileName;
                            

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
    }
}
