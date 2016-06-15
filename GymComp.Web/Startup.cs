using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GymComp.Web.Startup))]
namespace GymComp.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
