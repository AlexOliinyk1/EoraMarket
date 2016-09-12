using EoraMarketplace.Data;
using Owin;
using System;

namespace EoraMarketplace.Injector.Web
{
    public class OwinBootstrapper
    {
        public static IAppBuilder InitOwinContext(IAppBuilder builder)
        {
            builder.CreatePerOwinContext(CreateContext);
            return builder;
        }

        private static EoraDbContext CreateContext()
        {
            return Activator.CreateInstance<EoraDbContext>();
        }
    }
}
