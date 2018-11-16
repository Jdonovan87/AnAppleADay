using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AnAppleADay_03.Startup))]
namespace AnAppleADay_03
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
