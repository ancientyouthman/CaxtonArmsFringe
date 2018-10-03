using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CaxtonArmsFringe.Startup))]
namespace CaxtonArmsFringe
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
