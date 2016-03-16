using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Diploma.Web.Startup))]
namespace Diploma.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
