using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Motonet.Startup))]
namespace Motonet
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
