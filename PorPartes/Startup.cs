using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PorPartes.Startup))]
namespace PorPartes
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
