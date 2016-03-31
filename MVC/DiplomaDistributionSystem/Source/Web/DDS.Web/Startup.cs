using Microsoft.Owin;

using Owin;

[assembly: OwinStartupAttribute(typeof(DDS.Web.Startup))]

namespace DDS.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            this.ConfigureAuth(app);
        }
    }
}
