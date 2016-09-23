using System;
using Microsoft.Practices.Unity;
using EoraMarketplace.Injector.Web;
using Microsoft.AspNet.Identity;
using EoraMarketpalce.Web.Common.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using EoraMarketplace.Data.Domain.Users;
using Microsoft.Owin.Security;
using System.Web;
using EoraMarketplace.Injector.Services;
using Microsoft.AspNet.Identity.Owin;

namespace EoraMarketpalce.Web.App_Start
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public class UnityConfig
    {
        private static Lazy<IUnityContainer> _container = new Lazy<IUnityContainer>(() => {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        /// <summary>
        /// Get the configured Unity container.
        /// </summary>
        public static IUnityContainer ConfiguredContainer { get { return _container.Value; } }

        /// <summary>Registers the type mappings with the Unity container.</summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>There is no need to register concrete types such as controllers or API controllers (unless you want to 
        /// change the defaults), as Unity allows resolving a concrete type even if it was not previously registered.</remarks>
        private static void RegisterTypes(IUnityContainer container)
        {
            //  register global dependencies
            WebBootstrapper.SetupDependency(container);
            RepositoryBootstrapper.SetupDependency(container);
            ServiceBootstrapper.SetupDependency(container);

            //  register owin dependencies
            container.RegisterType<IRoleStore<Role, int>, RoleStore<Role, int, UserRoles>>();
            container.RegisterType<IUserStore<User, int>, UserStore<User, Role, int, UserLogins, UserRoles, UserClaims>>();
            container.RegisterType<IAuthenticationManager>(new InjectionFactory(c => HttpContext.Current.GetOwinContext().Authentication));

            //  register inner app dependencies
            container.RegisterType<EoraUserManager>();
            container.RegisterType<EoraSignInManager>();
        }
    }
}
