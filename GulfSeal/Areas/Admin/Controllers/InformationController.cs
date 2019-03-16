using GulfSeal.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GulfSeal.Areas.Admin.Controllers
{
    public class InformationController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/Information
        public ActionResult Index(int InformationTypeId)
        {
            ViewBag.InformationTypeId = InformationTypeId;
            return View(db.Informations.Where(x=>x.InformationTypeId == InformationTypeId).ToList());
        }

        public ActionResult Create(int InformationTypeId)
        {
            ViewBag.InformationTypeId = InformationTypeId;
            ViewBag.informationType = db.InformationTypes.Find(InformationTypeId).Extentions.ToString(); 

            return View();
        }

        [HttpPost]
        public ActionResult Create(Information information)
        {
            if(ModelState.IsValid)
            {
                db.Informations.Add(information);
                db.SaveChanges();

                information.InformationType = db.InformationTypes.Find(information.InformationTypeId);

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
                            string pathString = System.IO.Path.Combine(originalDirectory.ToString(), information.InformationType.Title);

                            string extension = Path.GetExtension(file.FileName);
                            var newFileName = imageName + extension;

                            bool isExists = System.IO.Directory.Exists(pathString);

                            if (!isExists)
                                System.IO.Directory.CreateDirectory(pathString);


                            var path = $"{pathString}\\{newFileName}";
                            file.SaveAs(path);


                            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority +
                                Request.ApplicationPath.TrimEnd('/') + "/";


                            information.InformationURL = baseUrl + "/Files/"+ information.InformationType.Title + "/" + newFileName;
                         
                             
                        }
                    }

                    db.SaveChanges();
                }
                catch (Exception e)
                {

                }

                db.SaveChanges();
                return new JsonResult();
            }


            ViewBag.InformationTypeId = information.InformationTypeId;
            ViewBag.informationType = db.InformationTypes.Find(information.InformationTypeId).Extentions.ToString();
            return new JsonResult();

        }

    }
}