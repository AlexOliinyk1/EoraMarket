using System;
using Microsoft.Practices.Unity;
using EoraMarket.DataAccess.Repositories;
using EoraMarketplace.Data.Domain.Users;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using EoraMarketplace.Data;

namespace EoraMarketplace.Injector.Web
{
    public class WebBootstrapper : Bootstrapper<WebBootstrapper>
    {
        protected internal override void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<DbContext, EoraDbContext>();
            container.RegisterType<IdentityDbContext<User, Role, int, IdentityUserLogin<int>, IdentityUserRole<int>, IdentityUserClaim<int>>, EoraDbContext>();

            container.RegisterType<IRepository<User>, Repository<User>>();
            container.RegisterType<IRepository<Role>, Repository<Role>>();
        }
    }
}
