using Microsoft.Practices.Unity;
using EoraMarketplace.Services.Characters;

namespace EoraMarketplace.Injector.Services
{
    public class ServiceBootstrapper : Bootstrapper<ServiceBootstrapper>
    {
        protected internal override void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<ICharacterService, CharacterService>();
        }
    }
}
