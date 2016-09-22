using Microsoft.Practices.Unity;
using EoraMarketplace.Data.Domain.Users;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using EoraMarketplace.Data;
using EoraMarketplace.DataAccess.Repositories;

namespace EoraMarketplace.Injector.Web
{
    public class WebBootstrapper : Bootstrapper<WebBootstrapper>
    {
        protected internal override void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<DbContext, EoraDbContext>(new PerThreadLifetimeManager());
            container.RegisterType<IdentityDbContext<User, Role, int, UserLogins, UserRoles, UserClaims>, EoraDbContext>();
        }
    }
}
