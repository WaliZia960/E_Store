using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SocialLogins.Startup))]
namespace SocialLogins
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
