using EoraMarketpalce.Web.Common.Identity;
using EoraMarketpalce.Web.Common.Extentions;
using EoraMarketplace.Data.Domain.Users;
using EoraMarketplace.Injector.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Practices.Unity;
using Owin;
using System;
using System.Web.Mvc;
using Microsoft.Practices.Unity.Mvc;
using Microsoft.Owin.Security;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

[assembly: OwinStartup(typeof(EoraMarketpalce.Web.Startup))]
namespace EoraMarketpalce.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            SetupDependency(app);

            app.UseCookieAuthentication(new CookieAuthenticationOptions {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/SignIn"),
                Provider = new CookieAuthenticationProvider {
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<EoraUserManager, User, int>(
                            TimeSpan.FromMinutes(30),
                            (manager, user) => { return user.GenerateUserIdentityAsync(manager); },
                            (id) => id.GetUserId<int>()
                        )
                }
            });

        }

        private void SetupDependency(IAppBuilder app)
        {
            UnityContainer container = new UnityContainer();

            //  register global dependencies
            WebBootstrapper.SetupDependency(container);

            //  register owin dependencies
            container.RegisterType<IUserStore<User, int>, UserStore<User, Role, int, IdentityUserLogin<int>, IdentityUserRole<int>, IdentityUserClaim<int>>>();
            container.RegisterType<IAuthenticationManager>(new InjectionFactory(c => HttpContext.Current.GetOwinContext().Authentication));

            //  register inner app dependencies
            container.RegisterType<EoraUserManager>();
            container.RegisterType<EoraSignInManager>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}
