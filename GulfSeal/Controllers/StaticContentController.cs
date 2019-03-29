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

namespace GulfSeal.Controllers 
{ 
    public class StaticContentController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/StaticContents
        public ActionResult Index()
        {
            return PartialView(db.StaticContents.ToList());
        }

        public ActionResult About()
        {
            return View(db.StaticContents.First());
        }

        public ActionResult Privacy()
        {
            return View(db.StaticContents.First());
        }

    }
}
