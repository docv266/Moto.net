using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Moto.Startup))]
namespace Moto
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
