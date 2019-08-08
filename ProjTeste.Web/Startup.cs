using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ProjTeste.Web.Startup))]
namespace ProjTeste.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
