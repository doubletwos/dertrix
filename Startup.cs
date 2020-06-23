using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Zeus.Startup))]
namespace Zeus
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
