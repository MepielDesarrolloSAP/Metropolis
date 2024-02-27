using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Gate.Startup))]
namespace Gate
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
