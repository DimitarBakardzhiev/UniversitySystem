using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(UniversitySystem.Web.Startup))]

namespace UniversitySystem.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            this.ConfigureAuth(app);
        }
    }
}
