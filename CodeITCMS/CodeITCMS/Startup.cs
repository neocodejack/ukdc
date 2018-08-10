using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CodeITCMS.Startup))]
namespace CodeITCMS
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
