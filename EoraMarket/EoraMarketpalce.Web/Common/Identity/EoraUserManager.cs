using System;
using EoraMarketplace.Data.Domain.Users;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System.Web.Mvc;
using EoraMarketplace.Services.Email;

namespace EoraMarketpalce.Web.Common.Identity
{
    /// <summary>
    ///     Identity User manager
    /// </summary>
    public class EoraUserManager : UserManager<User, int>
    {
        /// <summary>
        ///     Ctor.
        /// </summary>
        /// <param name="store"></param>
        public EoraUserManager(IUserStore<User, int> store)
            : base(store)
        {
            this.Store = store;
        }

        /// <summary>
        ///     Create instance of EoraUserManager
        /// </summary>
        /// <param name="options"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public static EoraUserManager Create(IdentityFactoryOptions<EoraUserManager> options, IOwinContext context)
        {
            var manager = new EoraUserManager(DependencyResolver.Current.GetService<IUserStore<User, int>>());
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<User, int>(manager) {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = false,
                RequireUppercase = false,
            };

            // Configure user lockout defaults
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug it in here.
            //manager.RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<ApplicationUser> {
            //    MessageFormat = "Your security code is {0}"
            //});
            //manager.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<ApplicationUser> {
            //    Subject = "Security Code",
            //    BodyFormat = "Your security code is {0}"
            //});
            manager.EmailService = new EmailService();
            //manager.SmsService = new SmsService();
            var dataProtectionProvider = options.DataProtectionProvider;
            if(dataProtectionProvider != null)
            {
                manager.UserTokenProvider =
                    new DataProtectorTokenProvider<User, int>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }
    }
}