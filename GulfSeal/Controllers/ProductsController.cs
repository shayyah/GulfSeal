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

namespace GulfSeal.Controllers 
{
    public class ProductsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {

            return View(db.Products.ToList());
        }
         

    }
}
