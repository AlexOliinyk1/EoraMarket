using Microsoft.Practices.Unity;
using EoraMarketplace.Data.Domain.Users;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using EoraMarketplace.Data;
using EoraMarketplace.DataAccess.Repositories;

namespace EoraMarketplace.Injector.Web
{
    /// <summary>
    ///     Dependency injector for Asp.Net application client
    /// </summary>
    public class WebBootstrapper : Bootstrapper<WebBootstrapper>
    {
        /// <summary>
        ///     Register all dependencies for current injector
        /// </summary>
        /// <param name="container">Instance of Unity container</param>
        protected internal override void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<DbContext, EoraDbContext>(new PerRequestLifetimeManager());
            //container.RegisterType<IdentityDbContext<User, Role, int, UserLogins, UserRoles, UserClaims>, EoraDbContext>();
        }
    }
}
