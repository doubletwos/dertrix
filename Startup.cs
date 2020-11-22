using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Dertrix.Startup))]
namespace Dertrix
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
