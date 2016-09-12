using EoraMarketpalce.Web.Common.Identity;
using EoraMarketpalce.Web.Common.Extentions;
using EoraMarketplace.Data.Domain.Users;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System;

[assembly: OwinStartup(typeof(EoraMarketpalce.Web.Startup))]
namespace EoraMarketpalce.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
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
    }
}
