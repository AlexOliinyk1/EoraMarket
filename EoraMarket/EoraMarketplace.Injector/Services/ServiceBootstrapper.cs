using Microsoft.Practices.Unity;
using EoraMarketplace.Services.Characters;
using EoraMarketplace.Services.Goods;
using EoraMarketplace.Services.Stats;

namespace EoraMarketplace.Injector.Services
{
    public class ServiceBootstrapper : Bootstrapper<ServiceBootstrapper>
    {
        protected internal override void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<ICharacterService, CharacterService>();
            container.RegisterType<IGoodsService, GoodsService>();
            container.RegisterType<IStatsService, StatsService>();
        }
    }
}
