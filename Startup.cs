using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(e_CarSharing.Startup))]
namespace e_CarSharing
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
