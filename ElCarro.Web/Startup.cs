using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ElCarro.Web.Startup))]
namespace ElCarro.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
