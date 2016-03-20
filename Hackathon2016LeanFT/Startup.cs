using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Hackathon2016LeanFT.Startup))]
namespace Hackathon2016LeanFT
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
