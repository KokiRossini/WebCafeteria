using Owin;
using Microsoft.Owin;

[assembly: OwinStartup(typeof(WebCafeteria.Web.Startup))]
namespace WebCafeteria.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
