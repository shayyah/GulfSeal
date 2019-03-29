using GulfSeal.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GulfSeal.Controllers  
{ 
    public class LanguageController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Language
        public ActionResult Index()
        {
            return PartialView(db.Languages.ToList());
        }
 
    }
}
