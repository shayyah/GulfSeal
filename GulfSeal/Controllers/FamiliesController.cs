using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GulfSeal.Models;


namespace GulfSeal.Controllers
{
    public class FamiliesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Families
        public ActionResult Index()
        {
            return View(db.Families.OrderByDescending(x=>x.Rank).ToList());
        }
         
    }
}
