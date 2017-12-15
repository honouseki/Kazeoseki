using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Kazeoseki.Web.Startup))]
namespace Kazeoseki.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
