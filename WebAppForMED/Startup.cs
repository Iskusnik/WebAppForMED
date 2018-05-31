using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebAppForMED.Startup))]
namespace WebAppForMED
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
