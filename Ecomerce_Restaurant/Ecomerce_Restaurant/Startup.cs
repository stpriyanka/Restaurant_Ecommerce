using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Ecomerce_Restaurant.Startup))]
namespace Ecomerce_Restaurant
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
