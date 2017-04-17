using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BeerTracker.Web.Startup))]
namespace BeerTracker.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
