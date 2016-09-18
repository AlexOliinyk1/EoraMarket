using EoraMarketpalce.Web.Common.Identity;
using EoraMarketpalce.Web.Common.Extentions;
using EoraMarketplace.Data.Domain.Users;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System;
using System.Web.Mvc;
using EoraMarketpalce.Web.Common.Constants;
using Microsoft.Owin.Security.OAuth;
using EoraMarketpalce.Web.Common.Providers;

[assembly: OwinStartup(typeof(EoraMarketpalce.Web.Startup))]
namespace EoraMarketpalce.Web
{
    public class Startup
    {
        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

        public static string PublicClientId { get; private set; }

        /// <summary>
        ///     Configure owin context for application
        /// </summary>
        /// <param name="app"></param>
        public void Configuration(IAppBuilder app)
        {
            TryCreateDefaultRolesAndUsers();

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

            PublicClientId = "self";
            OAuthOptions = new OAuthAuthorizationServerOptions {
                TokenEndpointPath = new PathString("/Token"),
                Provider = new ApplicationOAuthProvider(PublicClientId),
                AuthorizeEndpointPath = new PathString("/api/Account/ExternalLogin"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(14),
                AllowInsecureHttp = true
            };

            app.UseOAuthBearerTokens(OAuthOptions);
        }

        /// <summary>
        ///     Create default identity entities for roles and main admin
        /// </summary>
        private void TryCreateDefaultRolesAndUsers()
        {
            var roleManager = new RoleManager<Role, int>(DependencyResolver.Current.GetService<IRoleStore<Role, int>>());
            var UserManager = new UserManager<User, int>(DependencyResolver.Current.GetService<IUserStore<User, int>>());

            if(!roleManager.RoleExists(AppConsts.AdminRoleName))
            {
                Role adminRole = new Role {
                    Name = AppConsts.AdminRoleName,
                    Description = AppConsts.AdminRoleDescription
                };

                roleManager.Create(adminRole);

                User admin = new User {
                    UserName = AppConsts.SuperUserName,
                    Email = AppConsts.SuperUserName
                };

                var adminResult = UserManager.Create(admin, AppConsts.SuperUserPass);

                if(adminResult.Succeeded)
                {
                    var adminRoleResult = UserManager.AddToRole(admin.Id, adminRole.Name);
                }
            }

            if(!roleManager.RoleExists(AppConsts.UserRoleName))
            {
                var role = new Role {
                    Name = AppConsts.UserRoleName,
                    Description = AppConsts.UserRoleDescription
                };

                roleManager.Create(role);
            }
        }
    }
}
