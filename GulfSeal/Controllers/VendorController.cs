using GulfSeal.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace GulfSeal.Controllers
{  
    public class VendorController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/Vendor
        public ActionResult Index()
        {
            return View(db.Vendors.ToList());
        }

       
    }
}