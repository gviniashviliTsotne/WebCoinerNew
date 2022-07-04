 using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebCoiner.Startup))]
namespace WebCoiner
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
