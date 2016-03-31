using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(ChampionSystem.Api.Startup))]

namespace ChampionSystem.Api
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
