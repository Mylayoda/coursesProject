using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(courses.Startup))]
namespace courses
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
