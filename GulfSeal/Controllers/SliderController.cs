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
    public class SliderController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Slider
        public ActionResult Index()
        { 
            List<slider_packeg > tmp= db.slider_packegs.ToList();
            List<slider_packeg> new_list = new List<slider_packeg>();

            for(int i=0;i<tmp.Count();i++)
            {
                bool contain_sim = true;
                foreach(var a in new_list)
                {
                    if(a.semilarity_key==tmp.ElementAt(i).semilarity_key)
                    {
                        contain_sim = false;
                        continue;
                    }

                }
                if(contain_sim)
                {
                    new_list.Add(tmp.ElementAt(i));
                }
            } 
            return View(new_list); 
        }

    }
}