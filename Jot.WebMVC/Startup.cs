using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Jot.WebMVC.Startup))]
namespace Jot.WebMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
