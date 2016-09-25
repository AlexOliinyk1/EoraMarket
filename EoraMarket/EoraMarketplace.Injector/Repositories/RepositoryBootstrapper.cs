using Microsoft.Practices.Unity;
using EoraMarketplace.DataAccess.Repositories;
using EoraMarketplace.Data.Domain.Users;
using EoraMarketplace.Data.Domain.Characters;
using EoraMarketplace.Data.Domain.Goods;

namespace EoraMarketplace.Injector.Services
{
    /// <summary>
    ///     Dependency injector for application repositories
    /// </summary>
    public class RepositoryBootstrapper : Bootstrapper<RepositoryBootstrapper>
    {
        /// <summary>
        ///     Register all dependencies for current injector
        /// </summary>
        /// <param name="container">Instance of Unity container</param>
        protected internal override void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<IRepository<User>, Repository<User>>();
            container.RegisterType<IRepository<Role>, Repository<Role>>();

            container.RegisterType<IRepository<Character>, Repository<Character>>();
            container.RegisterType<IRepository<Class>, Repository<Class>>();
            container.RegisterType<IRepository<Race>, Repository<Race>>();
            container.RegisterType<IRepository<CharactersProducts>, Repository<CharactersProducts>>();
            container.RegisterType<IRepository<Product>, Repository<Product>>();
            container.RegisterType<IRepository<ProductStat>, Repository<ProductStat>>();

            container.RegisterType<IRepository<MarketProduct>, Repository<MarketProduct>>();
        }
    }
}
