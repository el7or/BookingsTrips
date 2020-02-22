using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BookingsTrips.Startup))]
namespace BookingsTrips
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
