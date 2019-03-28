using GulfSeal.Areas.Admin.Models.ViewModels;
using GulfSeal.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
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


        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Information information = db.Informations.Find(id);
            if (information == null)
            {
                return HttpNotFound();
            } 
            return View(information);
        }

        [HttpPost]
        public ActionResult Edit(Information information)
        {
            if (ModelState.IsValid)
            {
                db.Entry(information).State = EntityState.Modified;


                try
                {
                    var fName = "";
                    foreach (string fileName in Request.Files)
                    {
                        Guid imageName = Guid.NewGuid();

                        HttpPostedFileBase file = Request.Files[fileName];
                        fName = file.FileName;
                        information.InformationType = db.InformationTypes.Find(information.InformationTypeId);
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


                            information.InformationURL = baseUrl + "/Files/" + information.InformationType.Title + "/" + newFileName;


                        }
                    }

                    db.SaveChanges();
                }
                catch (Exception e)
                {

                }


                db.SaveChanges();


                return RedirectToAction("Index", "Information", new { InformationTypeId = information.InformationTypeId, area = "Admin" });
            } 
            return View(information);
        }


        public ActionResult EditInformationTranslation(int informationId)
        { 
            ViewBag.informationId = informationId;

            List<InformationTranslation> InformationTranslations = db.InformationTranslations.Where(x => x.InformationId == informationId).ToList();
            InformationTranslationsViewModel model = new InformationTranslationsViewModel();

            List<InformationTranslationsWithLanguagesViewModel> InformationTranslationsWithLanguage = new List<InformationTranslationsWithLanguagesViewModel>();
            foreach (var lan in db.Languages.ToList())
            {
                InformationTranslationsWithLanguagesViewModel temp = new InformationTranslationsWithLanguagesViewModel();
                temp.LanguageId = lan.Id; 
                temp.LanguageName = lan.Name; 
                if (InformationTranslations.Where(x => x.LanguageId == lan.Id).FirstOrDefault() == null)
                {
                    temp.InformationTranslation = new InformationTranslation();
                }
                else
                {
                    temp.InformationTranslation = InformationTranslations.Where(x => x.LanguageId == lan.Id).FirstOrDefault();
                }

                InformationTranslationsWithLanguage.Add(temp);
            }
            model.InformationTranslationsWithLanguages = InformationTranslationsWithLanguage;

            return View(model); 
        }

        [HttpPost]
        public ActionResult EditInformationTranslation(List<InformationTranslationsWithLanguagesViewModel> InformationTranslationsWithLanguages,
            int informationId)
        {

            foreach (InformationTranslationsWithLanguagesViewModel InformationTranslationsWithLanguage in InformationTranslationsWithLanguages)
            {

                if (InformationTranslationsWithLanguage.InformationTranslation.Title != null 
                    && InformationTranslationsWithLanguage.InformationTranslation.Title != "")
                {

                    InformationTranslation old = db.InformationTranslations
                        .Where(x => x.LanguageId == InformationTranslationsWithLanguage.InformationTranslation.LanguageId
                            && x.InformationId == InformationTranslationsWithLanguage.InformationTranslation.InformationId).FirstOrDefault();

                    if (old == null)
                    {
                        db.InformationTranslations.Add(InformationTranslationsWithLanguage.InformationTranslation);
                    }
                    else
                    {
                        old.Title = InformationTranslationsWithLanguage.InformationTranslation.Title; 
                    }
                }
                else
                {
                    InformationTranslation old = db.InformationTranslations
                        .Where(x => x.LanguageId == InformationTranslationsWithLanguage.InformationTranslation.LanguageId
                            && x.InformationId == InformationTranslationsWithLanguage.InformationTranslation.InformationId).FirstOrDefault();

                    if (old != null)
                    {
                        db.InformationTranslations.Remove(old);
                    }
                }

            }
            db.SaveChanges();


            return RedirectToAction("Edit", "Information", new { id = informationId, area = "Admin" });
        }



        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Information Information = db.Informations.Find(id);
            if (Information == null)
            {
                return HttpNotFound();
            }
            return View(Information);
        }

        // POST: Admin/InformationTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Information Information = db.Informations.Find(id);
            int InformationTypeId = Information.InformationTypeId;
            db.Informations.Remove(Information);
            db.SaveChanges();
            return RedirectToAction("Index", "Information", new { area="Admin", InformationTypeId  = InformationTypeId });

        }

    }
}