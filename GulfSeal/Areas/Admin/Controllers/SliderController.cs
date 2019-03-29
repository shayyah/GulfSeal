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
    public class SliderController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Slider
        public ActionResult Index()
        {

            List<slider_packeg > tmp= db.slider_packegs.ToList();
            List<slider_packeg> new_list = new List<slider_packeg>();

            for(int i=0;i<tmp.Count();i++)
            {
                bool contain_sim = true;
                foreach(var a in new_list)
                {
                    if(a.semilarity_key==tmp.ElementAt(i).semilarity_key)
                    {
                        contain_sim = false;
                        continue;
                    }

                }
                if(contain_sim)
                {
                    new_list.Add(tmp.ElementAt(i));
                }
            }

            return View(new_list) ;


        }
        public ActionResult Create()
        {

            return View(db.Languages.ToList());
            
        }

        [HttpPost]
        public ActionResult Create(string slider_data)
        {
            //"arabic***arabic subtitle***1###chines***chines subtitle***2###"
            List<string> all_sliders = slider_data.Split(new string[] { "###" }, StringSplitOptions.None).ToList();
            all_sliders.RemoveAt(all_sliders.Count() - 1);
            //List<events> result = new JavaScriptSerializer()
            Guid semililarity_key= Guid.NewGuid(); ;
            var fName = "";
            var newFileName = "";
            string baseUrl = "";
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
                        string pathString = System.IO.Path.Combine(originalDirectory.ToString(), "slider");

                        string extension = Path.GetExtension(file.FileName);
                        newFileName = imageName + extension;

                        bool isExists = System.IO.Directory.Exists(pathString);

                        if (!isExists)
                            System.IO.Directory.CreateDirectory(pathString);


                        var path = $"{pathString}\\{newFileName}";
                        file.SaveAs(path);
                        baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority +
                            Request.ApplicationPath.TrimEnd('/') + "/";



                    }
                }
            }
            catch (Exception e)
            {
                return View();
            }
            foreach (var item in all_sliders)
            {
                List<string> slider_items= item.Split(new string[] { "***" }, StringSplitOptions.None).ToList();
                slider_packeg s = new slider_packeg();
                s.image_url = baseUrl + "/Files/slider/" + newFileName;
                s.slider_name = slider_items.ElementAt(0);
                s.sublider_name = slider_items.ElementAt(1);
                s.lang_id = Int32.Parse(slider_items.ElementAt(2));
                s.lang_name = db.Languages.Find(Int32.Parse(slider_items.ElementAt(2))).Name;
                s.semilarity_key = semililarity_key.ToString();
                db.slider_packegs.Add(s);
                db.SaveChanges();
            }
            return RedirectToAction("Index", "Slider", new { Area = "Admin" });

        }


        public ActionResult Edit(string simkey)
        {

            List<slider_packeg> slider_list = db.slider_packegs.Where(x => x.semilarity_key == simkey).ToList();
            return View(slider_list);
        }

        [HttpPost]
        public ActionResult Edit_post(string slider_data)
        {

            List<slider_packeg> result = new JavaScriptSerializer()
           .Deserialize<List<slider_packeg>>(slider_data);
            string final_url = "";
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

                        //delete old file
                        var old_originalDirectory = new DirectoryInfo($"{Server.MapPath(@"\")}Files");
                        string old_pathString = System.IO.Path.Combine(old_originalDirectory.ToString(), "slider");
                        //C:\\Users\\خالد\\Desktop\\Projects\\GulfSeal\\GulfSeal\\Files\\partenrs"
                        //"http://localhost:49944//Files/partenrs/14792958-a7d1-4dac-a903-a78b8dac1912.jpg"
                        var oldFileName = result.FirstOrDefault().image_url.Split('/').Last().ToString();



                        var old_path = old_pathString + "\\" + oldFileName;

                        //delete old files
                        if (System.IO.File.Exists(old_path))
                            System.IO.File.Delete(old_path);


                        //add new filse
                        var originalDirectory = new DirectoryInfo($"{Server.MapPath(@"\")}Files");
                        string pathString = System.IO.Path.Combine(originalDirectory.ToString(), "slider");

                        string extension = Path.GetExtension(file.FileName);
                        var newFileName = imageName + extension;

                        bool isExists = System.IO.Directory.Exists(pathString);

                        if (!isExists)
                            System.IO.Directory.CreateDirectory(pathString);


                        var path = $"{pathString}\\{newFileName}";
                        file.SaveAs(path);


                        string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority +
                            Request.ApplicationPath.TrimEnd('/') + "/";

                        slider_packeg s = new slider_packeg();
                        final_url = baseUrl + "/Files/slider/" + newFileName;
                    }
                }
            }
            catch (Exception e)
            {
                return View(e);
            }

            foreach(var r in result)
            {
                slider_packeg temp = db.slider_packegs.Find(r.Id);

                if(final_url != "")
                    temp.image_url = final_url;
                temp.slider_name = r.slider_name;
                temp.sublider_name = r.sublider_name;
                db.Entry(temp).State = EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("Index", "slider", new { Area = "Admin" });


        }

        public ActionResult Details(string simke)
        {
        
            return View(db.slider_packegs.Where(x=>x.semilarity_key == simke));
        }

        public ActionResult Delete(string simkey)
        {

            return View(db.slider_packegs.Where(x => x.semilarity_key == simkey));
        }
        public ActionResult Delete_confirm(string simkey)
        {
            List<slider_packeg> sp = db.slider_packegs.Where(x => x.semilarity_key == simkey).ToList();

            //delete old file
            var old_originalDirectory = new DirectoryInfo($"{Server.MapPath(@"\")}Files");
            string old_pathString = System.IO.Path.Combine(old_originalDirectory.ToString(), "slider");
            var oldFileName = sp.FirstOrDefault().image_url.Split('/').Last().ToString();
            var old_path = old_pathString + "\\" + oldFileName;

            //delete old files
            if (System.IO.File.Exists(old_path))
                System.IO.File.Delete(old_path);

            foreach (var a in sp)
            {
                db.slider_packegs.Remove(a);
                db.SaveChanges();
            }
            return RedirectToAction("Index", "slider", new { Area = "Admin" });

        }

    }
}