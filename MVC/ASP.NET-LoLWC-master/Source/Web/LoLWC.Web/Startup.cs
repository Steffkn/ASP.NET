using Microsoft.Owin;

using Owin;

[assembly: OwinStartupAttribute(typeof(LoLWC.Web.Startup))]

namespace LoLWC.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            this.ConfigureAuth(app);
        }
    }
}
