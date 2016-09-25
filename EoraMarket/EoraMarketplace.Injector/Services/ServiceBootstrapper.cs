using Microsoft.Practices.Unity;
using EoraMarketplace.Services.Characters;
using EoraMarketplace.Services.Goods;
using EoraMarketplace.Services.Stats;

namespace EoraMarketplace.Injector.Services
{
    /// <summary>
    ///     Dependency injector for application services
    /// </summary>
    public class ServiceBootstrapper : Bootstrapper<ServiceBootstrapper>
    {
        /// <summary>
        ///     Register all dependencies for current injector
        /// </summary>
        /// <param name="container">Instance of Unity container</param>
        protected internal override void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<ICharacterService, CharacterService>();
            container.RegisterType<IGoodsService, GoodsService>();
            container.RegisterType<IStatsService, StatsService>();
        }
    }
}
