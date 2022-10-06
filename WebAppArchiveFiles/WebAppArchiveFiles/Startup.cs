using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebAppArchiveFiles.Startup))]
namespace WebAppArchiveFiles
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
