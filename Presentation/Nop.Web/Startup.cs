using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Nop.Web.Startup))]
namespace Nop.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
