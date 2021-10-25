using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(VidlyWithAuth.Startup))]
namespace VidlyWithAuth
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
