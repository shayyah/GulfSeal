using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GulfSeal.Models;
using System.Web.Script.Serialization;
using System.IO;

namespace GulfSeal.Areas.Admin.Controllers
{
    public class sec_langController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/sec_lang
        public ActionResult Index()
        {
           
            var sec_langs = db.sec_langs.Include(s => s.Language);
            return View(sec_langs.ToList());
        }

        // GET: Admin/sec_lang/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sec_lang sec_lang = db.sec_langs.Find(id);
            if (sec_lang == null)
            {
                return HttpNotFound();
            }
            return View(sec_lang);
        }

        // GET: Admin/sec_lang/Create
        public ActionResult Create()
        {
            ViewBag.langs = db.Languages.ToList(); ;
            ViewBag.langId = new SelectList(db.Languages, "Id", "Name");
            return View();
        }

        // POST: Admin/sec_lang/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create( string sec_lang)
        {

            List<sec_lang> result = new JavaScriptSerializer()
             .Deserialize<List<sec_lang>>(sec_lang);
            var fName = "";
            string baseUrl = "";
            string final_url = "";
            //save the image
            try
            {

                foreach (string fileName in Request.Files)
                {
                    Guid imageName = Guid.NewGuid();

                    HttpPostedFileBase file = Request.Files[fileName];
                    fName = file.FileName;
                    if (file != null && file.ContentLength > 0)
                    {
                        var originalDirectory = new DirectoryInfo($"{Server.MapPath(@"\")}Files");
                        string pathString = System.IO.Path.Combine(originalDirectory.ToString(), "sec");

                        string extension = Path.GetExtension(file.FileName);
                        var newFileName = imageName + extension;

                        bool isExists = System.IO.Directory.Exists(pathString);

                        if (!isExists)
                            System.IO.Directory.CreateDirectory(pathString);


                        var path = $"{pathString}\\{newFileName}";
                        file.SaveAs(path);
                        baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority +
                            Request.ApplicationPath.TrimEnd('/') + "/";

                        final_url = baseUrl + "/Files/sec/" + newFileName;
                    }
                }
            }
            catch (Exception e)
            {
                return View();
            }

            Guid sim_key = Guid.NewGuid();

            foreach (var a in result)
            {
                sec_lang temp = new sec_lang();
                temp.title = a.title;
                temp.body = a.body;
                temp.langId = a.langId;
                temp.image_url = final_url;
                temp.sim_key = sim_key.ToString();
                temp.page = a.page;
                temp.type = a.type;
                db.sec_langs.Add(temp);
                db.SaveChanges();
            }

            return RedirectToAction("Index", "sec_lang", new { Area = "Admin" });

        }

        // GET: Admin/sec_lang/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sec_lang sec_lang = db.sec_langs.Find(id);
            List<sec_lang> list_sec_lang = db.sec_langs.Where(x => x.sim_key == sec_lang.sim_key).ToList();
            List<Language> lang_list = db.Languages.ToList();
            List<Language> new_lang = lang_list.Where(p => !list_sec_lang.Any(p2 => p2.langId == p.Id)).ToList();
            foreach (var a in new_lang)
            {
                sec_lang new_lang_o = new sec_lang();
                sec_lang temp = list_sec_lang.FirstOrDefault();
                new_lang_o.sim_key = temp.sim_key;
                new_lang_o.title = "";
                new_lang_o.body = "";
                new_lang_o.image_url = temp.image_url;
                new_lang_o.page = temp.page;
                new_lang_o.type = temp.type;
                new_lang_o.langId = a.Id;
                list_sec_lang.Add(new_lang_o);
            }
            if (sec_lang == null)
            {
                return HttpNotFound();
            }

            ViewBag.lang_list = lang_list;
            return View(list_sec_lang);
        }

        // POST: Admin/sec_lang/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string sec_lang)
        {

            List<sec_lang> result = new JavaScriptSerializer()
                 .Deserialize<List<sec_lang>>(sec_lang);
            var fName = "";
            string baseUrl = "";
            string final_url = "";
            //save the image
            try
            {

                foreach (string fileName in Request.Files)
                {
                    Guid imageName = Guid.NewGuid();

                    HttpPostedFileBase file = Request.Files[fileName];
                    fName = file.FileName;
                    if (file != null && file.ContentLength > 0)
                    {

                        //delete old file
                        var old_originalDirectory = new DirectoryInfo($"{Server.MapPath(@"\")}Files");
                        string old_pathString = System.IO.Path.Combine(old_originalDirectory.ToString(), "SEC");
                        //C:\\Users\\خالد\\Desktop\\Projects\\GulfSeal\\GulfSeal\\Files\\partenrs"
                        //"http://localhost:49944//Files/partenrs/14792958-a7d1-4dac-a903-a78b8dac1912.jpg"
                        var oldFileName = result.FirstOrDefault().image_url.Split('/').Last().ToString();



                        var old_path = old_pathString + "\\" + oldFileName;

                        //delete old files
                        if (System.IO.File.Exists(old_path))
                            System.IO.File.Delete(old_path);




                        var originalDirectory = new DirectoryInfo($"{Server.MapPath(@"\")}Files");
                        string pathString = System.IO.Path.Combine(originalDirectory.ToString(), "sec");

                        string extension = Path.GetExtension(file.FileName);
                        var newFileName = imageName + extension;

                        bool isExists = System.IO.Directory.Exists(pathString);

                        if (!isExists)
                            System.IO.Directory.CreateDirectory(pathString);


                        var path = $"{pathString}\\{newFileName}";
                        file.SaveAs(path);
                        baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority +
                            Request.ApplicationPath.TrimEnd('/') + "/";

                        final_url = baseUrl + "/Files/sec/" + newFileName;
                    }
                }
            }
            catch (Exception e)
            {
                return View();
            }

            Guid sim_key = Guid.NewGuid();

            foreach (var a in result)
            {
                sec_lang temp = new sec_lang();
                temp = db.sec_langs.Find(a.Id);
                if(temp== null)
                {
                    sec_lang s = a;
                    a.image_url = final_url;
                    db.sec_langs.Add(s);
                    db.SaveChanges();
                }
                temp = a;
                temp.image_url = final_url;
                db.Entry(temp).State = EntityState.Modified;
                db.SaveChanges();
            }


            return RedirectToAction("Index", "sec_lang", new { Area = "Admin" });

           
        }

        // GET: Admin/sec_lang/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sec_lang sec_lang = db.sec_langs.Find(id);
            if (sec_lang == null)
            {
                return HttpNotFound();
            }
            return View(sec_lang);
        }

        // POST: Admin/sec_lang/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            sec_lang sec_lang = db.sec_langs.Find(id);
            db.sec_langs.Remove(sec_lang);
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
