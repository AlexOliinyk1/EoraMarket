using Microsoft.Practices.Unity;
using EoraMarketplace.DataAccess.Repositories;
using EoraMarketplace.Data.Domain.Users;
using EoraMarketplace.Data.Domain.Characters;

namespace EoraMarketplace.Injector.Services
{
    public class RepositoryBootstrapper : Bootstrapper<RepositoryBootstrapper>
    {
        protected internal override void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<IRepository<User>, Repository<User>>();
            container.RegisterType<IRepository<Role>, Repository<Role>>();

            container.RegisterType<IRepository<Character>, Repository<Character>>();
            container.RegisterType<IRepository<Class>, Repository<Class>>();
            container.RegisterType<IRepository<Race>, Repository<Race>>();
        }
    }
}
