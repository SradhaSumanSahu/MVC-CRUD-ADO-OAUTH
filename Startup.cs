using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVC_CRUD_ADO_OAUTH.Startup))]
namespace MVC_CRUD_ADO_OAUTH
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
