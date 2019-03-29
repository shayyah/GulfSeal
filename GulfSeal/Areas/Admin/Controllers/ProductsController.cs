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
using GulfSeal.Areas.Admin.Models.ViewModels;

namespace GulfSeal.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,Editor")]
    public class ProductsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
         
        

        // GET: Admin/Products/Create
        public ActionResult Create()
        {
            ViewBag.FamilyId = new SelectList(db.Families, "Id", "Title");
            return View();
        }
         
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product); 
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
                            string pathString = System.IO.Path.Combine(originalDirectory.ToString(), "Products");

                            string extension = Path.GetExtension(file.FileName);
                            var newFileName = imageName + extension;

                             

                            bool isExists = System.IO.Directory.Exists(pathString);

                            if (!isExists)
                                System.IO.Directory.CreateDirectory(pathString);


                            var path = $"{pathString}\\{newFileName}";
                            file.SaveAs(path);


                            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority +
                                Request.ApplicationPath.TrimEnd('/') + "/";
                            product.Link = baseUrl + "/Files/Products/" + newFileName;
                            product.FileName = imageName + "";
                            product.Extinstion = extension;

                        }
                    }
                    db.SaveChanges();
                    return RedirectToAction("CreateProductLanguages", "Products", new { id = product.Id, area = "Admin" });
                  
                }
                catch (Exception e)
                {
                    ViewBag.FamilyId = new SelectList(db.Families, "Id", "Title", product.FamilyId);
                    return View(product);
                }


            }

            ViewBag.FamilyId = new SelectList(db.Families, "Id", "Title", product.FamilyId);
            return View(product);
        }

        // GET: Admin/Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.FamilyId = new SelectList(db.Families, "Id", "Title", product.FamilyId);
            return View(product);
        }
          
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();

                 
                return RedirectToAction("CreateProductLanguages", "Products", new { id = product.Id, area = "Admin" });
            }
            ViewBag.FamilyId = new SelectList(db.Families, "Id", "Title", product.FamilyId);
            return View(product);
        }

        
         
        public ActionResult CreateProductLanguages(int id)
        { 
            ViewBag.Languages = db.Languages.ToList();
            ViewBag.productId = id; 
            return View();
        }


        [HttpPost]
        public ActionResult CreateProductLanguages(List<ProductTranslation> ProductContents, int productId)
        {

            foreach (ProductTranslation productTranslation in ProductContents)
            {
                ProductTranslation productContent = new ProductTranslation
                {
                    ProductId = productId,
                    LanguageId = productTranslation.LanguageId,
                    Name = productTranslation.Name,
                    ReinforcedDensity = productTranslation.ReinforcedDensity,
                    ReinforcedType = productTranslation.ReinforcedType,
                    ServiceType = productTranslation.ServiceType,
                    Thickness = productTranslation.Thickness,
                    Type = productTranslation.Type,
                    Width = productTranslation.Width,
                    Length = productTranslation.Length
                };

                if (productTranslation.Name != null && productTranslation.ReinforcedDensity != ""
                    && productTranslation.ReinforcedType != null && productTranslation.ServiceType != ""
                    && productTranslation.Thickness != null && productTranslation.Type != ""
                    && productTranslation.Width != null && productTranslation.Length != "") 
                    db.ProductTranslations.Add(productContent);

            }
            db.SaveChanges();


            return RedirectToAction("Index", "Families", new { area = "Admin" });
        }



        public ActionResult EditProductLanguages(int id)
        {
            ViewBag.Languages = db.Languages.ToList();
            ViewBag.productId = id;

            List<ProductTranslation> ProductContents = db.ProductTranslations.Where(x => x.ProductId == id).ToList();
            ProductTranslationsViewModel model = new ProductTranslationsViewModel();

            int ProductContentsCount = ProductContents.Count();
            for (int i = 0; i < db.Languages.Count() - ProductContentsCount; i++)
            {
                ProductContents.Add(new ProductTranslation());
            }
            model.ProductContents = ProductContents;

            return View(model);
        }


        [HttpPost]
        public ActionResult EditProductLanguages(List<ProductTranslation> ProductContents, int productId)
        {

            foreach (ProductTranslation productTranslation in ProductContents)
            { 

                if (productTranslation.Name != null && productTranslation.ReinforcedDensity != ""
                    && productTranslation.ReinforcedType != null && productTranslation.ServiceType != ""
                    && productTranslation.Thickness != null && productTranslation.Type != ""
                    && productTranslation.Width != null && productTranslation.Length != "")
                {

                    ProductTranslation old = db.ProductTranslations.Where(x => x.LanguageId == productTranslation.LanguageId
                            && x.ProductId == productTranslation.ProductId).FirstOrDefault();

                    if (old == null)
                    {
                        db.ProductTranslations.Add(productTranslation);
                    }
                    else
                    {
                        old.Length = productTranslation.Length;
                        old.Name = productTranslation.Name;
                        old.ReinforcedDensity = productTranslation.ReinforcedDensity;
                        old.ServiceType = productTranslation.ServiceType;
                        old.ReinforcedType = productTranslation.ReinforcedType;
                        old.Thickness = productTranslation.Thickness;
                        old.Width = productTranslation.Width;
                    }
                }
                else
                {
                    ProductTranslation old = db.ProductTranslations.Where(x => x.LanguageId == productTranslation.LanguageId
                            && x.ProductId == productTranslation.ProductId).FirstOrDefault();

                    if (old != null)
                    {
                        db.ProductTranslations.Remove(old);
                    }
                }

            }
            db.SaveChanges();


            return RedirectToAction("Index", "Families", new { area = "Admin" });
        }





        // GET: Admin/Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Admin/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }



    }
}
