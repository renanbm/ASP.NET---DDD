using Microsoft.Owin;
using Owin;
using RM.Architecture.UI.Sistema;

[assembly: OwinStartup(typeof(Startup))]

namespace RM.Architecture.UI.Sistema
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}