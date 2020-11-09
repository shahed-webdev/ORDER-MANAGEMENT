using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ORDER_MANAGEMENT.Startup))]
namespace ORDER_MANAGEMENT
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
