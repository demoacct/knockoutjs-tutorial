using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SearchCollection.Startup))]
namespace SearchCollection
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
