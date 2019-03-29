using GulfSeal.Areas.Admin.Models.Utility;
using GulfSeal.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using System;
using System.Linq;

[assembly: OwinStartupAttribute(typeof(GulfSeal.Startup))]
namespace GulfSeal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            InitDatabase();
        }

        public void InitDatabase()
        {

            ApplicationDbContext db = new ApplicationDbContext();


            StaticContent staticContent = new StaticContent();
            staticContent.About = "About";
            staticContent.Privacy = "Privacy"; 
            if(db.StaticContents.Count() == 0)
            { 
                db.StaticContents.Add(staticContent);
                db.SaveChanges();
            } 

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

            string roleName = Enums.UserRoles.Admin.ToString();

            if (!roleManager.RoleExists(roleName))
            {
                roleManager.Create(new IdentityRole(roleName));
            }

            roleName = Enums.UserRoles.Editor.ToString();
            if (!roleManager.RoleExists(roleName))
            {
                roleManager.Create(new IdentityRole(roleName));
            }

                 

            if (db.Users.Where(x => x.UserName == "Admin@GulfSeal.com").Count() == 0)
            {
                ApplicationUser user = new ApplicationUser(); 
                user.UserName = "Admin@GulfSeal.com";
                user.Email = "Admin@GulfSeal.com";
                var check = userManager.Create(user, "admin@pass");
                if (check.Succeeded)
                {
                    userManager.AddToRole(user.Id, Enums.UserRoles.Admin.ToString());
                }
            }

            if (db.Users.Where(x => x.UserName == "Editor@GulfSeal.com").Count() == 0)
            {
                ApplicationUser user = new ApplicationUser();
                user.UserName = "Editor@GulfSeal.com";
                user.Email = "Editor@GulfSeal.com";
                var check = userManager.Create(user, "Editor@pass");
                if (check.Succeeded)
                {
                    userManager.AddToRole(user.Id, Enums.UserRoles.Editor.ToString());
                }
            }

        }

    }



}
