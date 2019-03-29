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

namespace GulfSeal.Controllers
{
    [Authorize(Roles = "Admin,Editor")]
    public class sec_langController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/sec_lang
        public ActionResult Index()
        { 
            var sec_langs = db.sec_langs.Include(s => s.Language);
            return View(sec_langs.ToList());
        }
          
    }
}
