using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SmartMonkey.Startup))]
namespace SmartMonkey
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
