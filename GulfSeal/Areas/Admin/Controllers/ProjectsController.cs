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
using GulfSeal.Areas.Admin.Models.Utility;
using GulfSeal.Areas.Admin.Models.ViewModels;

namespace GulfSeal.Areas.Admin.Controllers
{
    public class ProjectsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Projects
        public ActionResult Index()
        {
            return View(db.Projects.OrderByDescending(x=>x.Rank).ToList());
        }

         

        // GET: Admin/Projects/Create
        public ActionResult Create()
        {
            return View();
        }
         
        [HttpPost] 
        public ActionResult Create(Project project, int ProjectTypeIndex)
        {

            project.ProjectType = Enums.getProjectType(ProjectTypeIndex);
            if (ModelState.IsValid)
            {
                db.Projects.Add(project);
                db.SaveChanges();


                try
                {
                    var fName = "";
                    foreach (string fileName in Request.Files)
                    {
                        string imageName = project.Id +"";

                        HttpPostedFileBase file = Request.Files[fileName];
                        fName = file.FileName;
                        if (file != null && file.ContentLength > 0)
                        {
                            var originalDirectory = new DirectoryInfo($"{Server.MapPath(@"\")}Files");
                            string pathString = System.IO.Path.Combine(originalDirectory.ToString(), "Projects");

                            string extension = Path.GetExtension(file.FileName);
                            var newFileName = imageName + extension;

                            bool isExists = System.IO.Directory.Exists(pathString);

                            if (!isExists)
                                System.IO.Directory.CreateDirectory(pathString);


                            var path = $"{pathString}\\{newFileName}";
                            file.SaveAs(path);


                            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority +
                                Request.ApplicationPath.TrimEnd('/') + "/";


                            project.ImageURL = baseUrl + "/Files/Projects/" + newFileName;


                            db.SaveChanges();
                        }
                    }

                }
                catch (Exception e)
                {

                }


                return RedirectToAction("Index");
            }

            return View(project);
        }

        // GET: Admin/Projects/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }
         
        [HttpPost] 
        public ActionResult Edit(Project project, int ProjectTypeIndex)
        {
            project.ProjectType = Enums.getProjectType(ProjectTypeIndex);

            if (ModelState.IsValid)
            {
                db.Entry(project).State = EntityState.Modified;
                db.SaveChanges();

                try
                {
                    var fName = "";
                    foreach (string fileName in Request.Files)
                    {
                        string imageName = project.Id + "";

                        HttpPostedFileBase file = Request.Files[fileName];
                        fName = file.FileName;
                        if (file != null && file.ContentLength > 0)
                        {
                            var originalDirectory = new DirectoryInfo($"{Server.MapPath(@"\")}Files");
                            string pathString = System.IO.Path.Combine(originalDirectory.ToString(), "Projects");

                            string extension = Path.GetExtension(file.FileName);
                            var newFileName = imageName + extension;

                            bool isExists = System.IO.Directory.Exists(pathString);

                            if (!isExists)
                                System.IO.Directory.CreateDirectory(pathString);


                            var path = $"{pathString}\\{newFileName}";
                            file.SaveAs(path);


                            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority +
                                Request.ApplicationPath.TrimEnd('/') + "/";


                            project.ImageURL = baseUrl + "/Files/Projects/" + newFileName;


                            db.SaveChanges();
                        }
                    }

                }
                catch (Exception e)
                {

                }


                return RedirectToAction("Index");
            }
            return View(project);
        }
         
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Admin/Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Project project = db.Projects.Find(id);
            db.Projects.Remove(project);
            db.SaveChanges();
            return RedirectToAction("Index");
        }





        public ActionResult EditProjectTranslation(int projectId)
        {
            ViewBag.projectId = projectId;

            List<ProjectTranslation> ProjectTranslations = db.ProjectTranslations.Where(x => x.ProjectId == projectId).ToList();
            ProjectTranslationsViewModel model = new ProjectTranslationsViewModel();

            List<ProjectTranslationsWithLanguagesViewModel> ProjectTranslationsWithLanguage = new List<ProjectTranslationsWithLanguagesViewModel>();
            foreach (var lan in db.Languages.ToList())
            {
                ProjectTranslationsWithLanguagesViewModel temp = new ProjectTranslationsWithLanguagesViewModel();
                temp.LanguageId = lan.Id;
                temp.LanguageName = lan.Name;
                if (ProjectTranslations.Where(x => x.LanguageId == lan.Id).FirstOrDefault() == null)
                {
                    temp.ProjectTranslation = new ProjectTranslation();
                }
                else
                {
                    temp.ProjectTranslation = ProjectTranslations.Where(x => x.LanguageId == lan.Id).FirstOrDefault();
                }

                ProjectTranslationsWithLanguage.Add(temp);
            }
            model.ProjectTranslationsWithLanguages = ProjectTranslationsWithLanguage;

            return View(model);
        }

        [HttpPost]
        public ActionResult EditProjectTranslation(List<ProjectTranslationsWithLanguagesViewModel> ProjectTranslationsWithLanguages,
            int projectId)
        {

            foreach (ProjectTranslationsWithLanguagesViewModel ProjectTranslationsWithLanguage in ProjectTranslationsWithLanguages)
            {

                if (ProjectTranslationsWithLanguage.ProjectTranslation.Title != null
                    && ProjectTranslationsWithLanguage.ProjectTranslation.Title != "")
                {

                    ProjectTranslation old = db.ProjectTranslations
                        .Where(x => x.LanguageId == ProjectTranslationsWithLanguage.ProjectTranslation.LanguageId
                            && x.ProjectId == ProjectTranslationsWithLanguage.ProjectTranslation.ProjectId).FirstOrDefault();

                    if (old == null)
                    {
                        db.ProjectTranslations.Add(ProjectTranslationsWithLanguage.ProjectTranslation);
                    }
                    else
                    {
                        old.Title = ProjectTranslationsWithLanguage.ProjectTranslation.Title;
                    }
                }
                else
                {
                    ProjectTranslation old = db.ProjectTranslations
                        .Where(x => x.LanguageId == ProjectTranslationsWithLanguage.ProjectTranslation.LanguageId
                            && x.ProjectId == ProjectTranslationsWithLanguage.ProjectTranslation.ProjectId).FirstOrDefault();

                    if (old != null)
                    {
                        db.ProjectTranslations.Remove(old);
                    }
                }

            }
            db.SaveChanges();


            return RedirectToAction("Index", "Projects", new { id = projectId, area = "Admin" });
        }


    }
}
