using Microsoft.Owin;

using Owin;

[assembly: OwinStartupAttribute(typeof(ElementsWeb.Web.Startup))]

namespace ElementsWeb.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            this.ConfigureAuth(app);
        }
    }
}
