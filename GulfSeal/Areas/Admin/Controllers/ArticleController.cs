using GulfSeal.Areas.Admin.Models.ViewModels;
using GulfSeal.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace GulfSeal.Areas.Admin.Controllers
{
    public class ArticleController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Article
        public ActionResult Index()
        {
            return View(db.Articles.ToList());
        }

         


        public ActionResult Details(int id)
        {
            
            return View(db.Articles.Where(x=>x.Id == id).Include(x => x.MediaFiles) 
                .Include(x=>x.ArticleContents).FirstOrDefault());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Article article)
        {
            if (ModelState.IsValid)
            {
                db.Articles.Add(article);
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
                            string pathString = System.IO.Path.Combine(originalDirectory.ToString(), "Articles");

                            string extension = Path.GetExtension(file.FileName);
                            var newFileName = imageName + extension;

                            bool isExists = System.IO.Directory.Exists(pathString);

                            if (!isExists)
                                System.IO.Directory.CreateDirectory(pathString);


                            var path = $"{pathString}\\{newFileName}";
                            file.SaveAs(path);


                            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority +
                                Request.ApplicationPath.TrimEnd('/') + "/";
                            article.Link = baseUrl + "/Files/Articles/" + newFileName;
                            article.FileName = imageName + "";
                            article.Extinstion = extension;

                        }
                    }

                    db.SaveChanges();
                }
                catch (Exception e)
                { 
                    return View(article);
                }


            }
            else
            {
                return View(article);
            }



            return RedirectToAction("CreateArticleLanguages", "Article", new {id= article.Id, area = "Admin" });
             
        }

        public ActionResult CreateArticleLanguages(int id)
        {

            ViewBag.Languages = db.Languages.ToList();
            ViewBag.articleId = id;
            return View();
        }

        [HttpPost]
        public ActionResult CreateArticleLanguages(List<ArticleContent> ArticleContents, int articleId)
        {

            foreach (ArticleContent articleCon in ArticleContents)
            {
                ArticleContent articleContent = new ArticleContent
                {
                    ArticleId = articleId,
                    Title = articleCon.Title,
                    Description = articleCon.Description,
                    LanguageId = articleCon.LanguageId
                };

                if(articleContent.Title != null && articleContent.Title !=""
                    && articleContent.Description != null && articleContent.Description != "")
                    db.ArticleContents.Add(articleContent);

            }
            db.SaveChanges();


            return RedirectToAction("UploadFiles", "Article", new { articleId = articleId, area = "Admin" }); 
        }

        public ActionResult UploadFiles(int articleId)
        { 
            return View(articleId);
        }

        [HttpPost]
        public ActionResult UploadFilesPost(int articleId)
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
                        string pathString = System.IO.Path.Combine(originalDirectory.ToString(), "Articles");

                        string extension = Path.GetExtension(file.FileName);
                        var newFileName = imageName + extension;

                        bool isExists = System.IO.Directory.Exists(pathString);

                        if (!isExists)
                            System.IO.Directory.CreateDirectory(pathString);


                        var path = $"{pathString}\\{newFileName}";
                        file.SaveAs(path);


                        string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority +
                            Request.ApplicationPath.TrimEnd('/') + "/";

                        Media media = new Media();
                        media.ArticleId = articleId;

                        media.Link = baseUrl + "/Files/Articles/" + newFileName;
                        media.FileName = imageName + "";
                        media.Extinstion = extension;

                        db.Medias.Add(media);
                    }
                }

                db.SaveChanges();
            }
            catch (Exception e)
            {

            }

            return RedirectToAction("Index", "Article", new {  area = "Admin" }); 
        }



        public ActionResult Edite(int id)
        {
            
            return View(db.Articles.Find(id));
        }
        [HttpPost]
        public ActionResult Edite([Bind(Include = "Id,Title,Description,CreatedAt,LastUpdatedAt,FileName,Link,Extinstion")]Article article)
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
                        string old_pathString = System.IO.Path.Combine(old_originalDirectory.ToString(), "Articles");
                        var oldFileName =article.FileName + article.Extinstion;
                        var old_path = $"{old_pathString}\\{oldFileName}";
                        //delete old files
                        if (System.IO.File.Exists(old_path))
                            System.IO.File.Delete(old_path);



                        var originalDirectory = new DirectoryInfo($"{Server.MapPath(@"\")}Files");
                        string pathString = System.IO.Path.Combine(originalDirectory.ToString(), "Articles");

                        string extension = Path.GetExtension(file.FileName);
                        var newFileName = imageName + extension;

                        bool isExists = System.IO.Directory.Exists(pathString);

                        if (!isExists)
                            System.IO.Directory.CreateDirectory(pathString);


                        var path = $"{pathString}\\{newFileName}";
                        file.SaveAs(path);


                        string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority +
                                Request.ApplicationPath.TrimEnd('/') + "/";
                        article.Link = baseUrl + "/Files/Articles/" + newFileName;
                        article.FileName = imageName + "";
                        article.Extinstion = extension;
                       
                    }
                }

                db.Entry(article).State = EntityState.Modified;
                db.SaveChanges();

            }
            catch (Exception e)
            {

            }
           return RedirectToAction("Index", "Article", new { area = "Admin" });
        }
       
        public ActionResult EditArticleLanguages(int id)
        {
            ViewBag.Languages = db.Languages.ToList();
            ViewBag.articleId = id;
            ArticleContentsViewModels acvm = new ArticleContentsViewModels();
            acvm.ArticleContents = db.ArticleContents.Where(x => x.ArticleId == id).ToList();
            //تشيك اذا كان عدد اللغات يساوي عدد اللغات المضافة الى المقالة
            if(db.Languages.Count()== db.ArticleContents.Where(x => x.ArticleId == id).Count())
            {
                return View(acvm);
            }
            else
            {
                foreach (var x in db.Languages.ToList())
                {
                    bool lang_added_to_article = false;
                    foreach(var a in acvm.ArticleContents )
                    {
                        if(x.Id == a.LanguageId)
                        {
                            lang_added_to_article = true;
                        }
                        
                    }
                    if(!lang_added_to_article)
                    {
                        ArticleContent temp = new ArticleContent();
                        temp.ArticleId = id;
                        temp.LanguageId = x.Id ;
                        temp.Description = "-";
                        temp.Title = "-";
                        db.ArticleContents.Add(temp);
                    }
                }
                try
                {
                    db.SaveChanges();
                }
                catch (Exception exception)
                {

                }
               
                acvm.ArticleContents = db.ArticleContents.Where(x => x.ArticleId == id).ToList();
                return View(acvm);
            }
            
        }

        [HttpPost]
        public ActionResult EditArticleLanguages(List<ArticleContent> ArticleContents, int articleId)
        {

            foreach (ArticleContent articleCon in ArticleContents)
            {
                articleCon.ArticleId = articleId;
                if (articleCon.Title != null && articleCon.Title != ""
                    && articleCon.Description != null && articleCon.Description != "")
                    db.Entry(articleCon).State = EntityState.Modified;
            }
            db.SaveChanges();


            return RedirectToAction("Index", "Article", new { articleId = articleId, area = "Admin" });
        }

        public ActionResult EditArticleMedia(int id)
        {

            return View(id);
        }
        public string EditArticleMedia_(int id)
        {
            List<string> links_list = db.Medias.Where(x => x.ArticleId == id).Select(y => y.Link).ToList();
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(links_list);
            
            return json;
        }


       [HttpPost]
        public string delete_upload_image(string deleted_img_name,int id)
        {

            if(deleted_img_name!="")
            {
                List<string> to_delete_imgName = deleted_img_name.Split(',').ToList();
                //http://localhost:49944//Files/Articles/53a9f0fb-0081-429d-af86-a3a9befc7c8b.jpg
                foreach(var a in to_delete_imgName)
                {
                    //delete from database
                    string image_name_with_extention = a.Split('/').Last();
                    string image_name = image_name_with_extention.Split('.').First();
                    Media temp = db.Medias.Where(x => x.FileName == image_name).FirstOrDefault();
                    db.Medias.Remove(temp);
                    db.SaveChanges();



                    var old_originalDirectory = new DirectoryInfo($"{Server.MapPath(@"\")}Files");
                    string old_pathString = System.IO.Path.Combine(old_originalDirectory.ToString(), "Articles");
                
                    var old_path = $"{old_pathString}\\{image_name_with_extention}";
                    //delete old files

                    //delete from server
                    if (System.IO.File.Exists(old_path))
                        System.IO.File.Delete(old_path);
                }


            }
            try
            {
                Article article = db.Articles.Find(id);

                var fName = "";
                foreach (string fileName in Request.Files)
                {
                    Media media = new Media();
                    
                    Guid imageName = Guid.NewGuid();

                    HttpPostedFileBase file = Request.Files[fileName];
                    fName = file.FileName;
                    if (file != null && file.ContentLength > 0)
                    {
                        var originalDirectory = new DirectoryInfo($"{Server.MapPath(@"\")}Files");
                        string pathString = System.IO.Path.Combine(originalDirectory.ToString(), "Articles");

                        string extension = Path.GetExtension(file.FileName);
                        var newFileName = imageName + extension;

                        bool isExists = System.IO.Directory.Exists(pathString);

                        if (!isExists)
                            System.IO.Directory.CreateDirectory(pathString);


                        var path = $"{pathString}\\{newFileName}";
                        file.SaveAs(path);


                        string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority +
                            Request.ApplicationPath.TrimEnd('/') + "/";
                        media.Link = baseUrl + "/Files/Articles/" + newFileName;
                        media.FileName = newFileName ;
                        media.Extinstion = extension;
                        media.ArticleId = id;

                        db.Medias.Add(media);

                    }
                }

                db.SaveChanges();
            }
            catch (Exception e)
            {
                //return View(article);
            }
            return "";

        }
    }
}
