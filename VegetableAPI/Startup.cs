using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(VegetableAPI.Startup))]
namespace VegetableAPI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
