using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Projekt_ANM.Startup))]
namespace Projekt_ANM
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
