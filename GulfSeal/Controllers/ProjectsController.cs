using GulfSeal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GulfSeal.Controllers
{
    public class ProjectsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Projects
        public ActionResult LastProjects()
        {
            return PartialView(db.Projects.OrderByDescending(x=>x.Rank).Take(6).ToList());
        }
    }
}