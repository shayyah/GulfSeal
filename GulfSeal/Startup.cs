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
        }
    }
}
