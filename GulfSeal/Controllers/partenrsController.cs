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
using System.Web.Script.Serialization;

namespace GulfSeal.Controllers
{
    [Authorize(Roles = "Admin,Editor")]
    public class partenrsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/partenrs
        public ActionResult Index()
        {
            return View(db.partenrs.ToList());
        }
         
    }
}
