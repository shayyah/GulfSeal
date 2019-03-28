using GulfSeal.Models;
using Microsoft.Owin;
using Owin;

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
            StaticContent staticContent = new StaticContent();
            staticContent.About = "About";
            staticContent.Privacy = "Privacy";
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                db.StaticContents.Add(staticContent);
                db.SaveChanges();
            }

        }

    }
}
