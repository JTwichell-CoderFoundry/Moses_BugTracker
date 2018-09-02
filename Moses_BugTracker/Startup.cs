using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Moses_BugTracker.Startup))]
namespace Moses_BugTracker
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
