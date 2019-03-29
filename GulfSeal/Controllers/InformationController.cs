using GulfSeal.Areas.Admin.Models.ViewModels;
using GulfSeal.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;


namespace GulfSeal.Controllers
{
    public class InformationController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/Information
        public ActionResult Index(int InformationTypeId)
        { 
            return View(db.Informations.Where(x=>x.InformationTypeId == InformationTypeId).ToList());
        }
 
       
        
    }
}