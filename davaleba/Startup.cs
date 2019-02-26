using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(davaleba.Startup))]
namespace davaleba
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
