using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GulfSeal.Models;
using GulfSeal.Areas.Admin.Models.ViewModels;

namespace GulfSeal.Areas.Admin.Controllers
{
    public class StaticContentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/StaticContents
        public ActionResult Index()
        {
            return View(db.StaticContents.ToList());
        }

        public ActionResult Edit(int id)
        {
            return View(db.StaticContents.Find(id));
        }

        [HttpPost]
        public ActionResult Edit(StaticContent model)
        {
            if (ModelState.IsValid)
            {
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();

            }
            return View(model);
        }



        // GET: Admin/StaticContents/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StaticContent staticContent = db.StaticContents.Find(id);
            if (staticContent == null)
            {
                return HttpNotFound();
            }
            return View(staticContent);
        }
         
        

         
        
        public ActionResult EditStaticTranslation(int staticId)
        {
            ViewBag.staticId = staticId;

            List<StaticContentTranslation> StaticContentTranslations = db.StaticContentTranslations.Where(x => x.StaticContentId == staticId).ToList();
            StaticTranslationsViewModel model = new StaticTranslationsViewModel();

            List<StaticTranslationsWithLanguagesViewModel> StaticTranslationsWithLanguages = new List<StaticTranslationsWithLanguagesViewModel>();
            foreach (var lan in db.Languages.ToList())
            {
                StaticTranslationsWithLanguagesViewModel temp = new StaticTranslationsWithLanguagesViewModel();
                temp.LanguageId = lan.Id;
                temp.LanguageName = lan.Name;
                if (StaticContentTranslations.Where(x => x.LanguageId == lan.Id).FirstOrDefault() == null)
                {
                    temp.StaticContentTranslation = new StaticContentTranslation();
                }
                else
                {
                    temp.StaticContentTranslation = StaticContentTranslations.Where(x => x.LanguageId == lan.Id).FirstOrDefault();
                }

                StaticTranslationsWithLanguages.Add(temp);
            }
            model.StaticTranslationsWithLanguages = StaticTranslationsWithLanguages;

            return View(model);
        }

        [HttpPost]
        public ActionResult EditStaticTranslation(List<StaticTranslationsWithLanguagesViewModel> StaticTranslationsWithLanguages,
            int staticId)
        {

            foreach (StaticTranslationsWithLanguagesViewModel StaticTranslationsWithLanguage in StaticTranslationsWithLanguages)
            {

                if (StaticTranslationsWithLanguage.StaticContentTranslation.Privacy != null
                    && StaticTranslationsWithLanguage.StaticContentTranslation.Privacy != ""
                    && StaticTranslationsWithLanguage.StaticContentTranslation.About != null
                    && StaticTranslationsWithLanguage.StaticContentTranslation.About != ""
                    )
                {

                    StaticContentTranslation old = db.StaticContentTranslations
                        .Where(x => x.LanguageId == StaticTranslationsWithLanguage.StaticContentTranslation.LanguageId
                            && x.StaticContentId == StaticTranslationsWithLanguage.StaticContentTranslation.StaticContentId).FirstOrDefault();

                    if (old == null)
                    {
                        db.StaticContentTranslations.Add(StaticTranslationsWithLanguage.StaticContentTranslation);
                    }
                    else
                    {
                        old.Privacy = StaticTranslationsWithLanguage.StaticContentTranslation.Privacy;
                        old.About = StaticTranslationsWithLanguage.StaticContentTranslation.About;
                    }
                }
                else
                {
                    StaticContentTranslation old = db.StaticContentTranslations
                        .Where(x => x.LanguageId == StaticTranslationsWithLanguage.StaticContentTranslation.LanguageId
                            && x.StaticContentId == StaticTranslationsWithLanguage.StaticContentTranslation.StaticContentId)
                            .FirstOrDefault();

                    if (old != null)
                    {
                        db.StaticContentTranslations.Remove(old);
                    }
                }

            }
            db.SaveChanges();


            return RedirectToAction("Index", "StaticContents", new { area = "Admin" });
        }

    }
}
