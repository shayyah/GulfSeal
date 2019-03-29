using GulfSeal.Areas.Admin.Models.ViewModels;
using GulfSeal.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace GulfSeal.Controllers
{
    public class ArticleController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Article
        public ActionResult Index()
        {
            return View(db.Articles.ToList());
        }
         
    }
}
