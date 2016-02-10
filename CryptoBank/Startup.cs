using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CryptoBank.Startup))]
namespace CryptoBank
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
