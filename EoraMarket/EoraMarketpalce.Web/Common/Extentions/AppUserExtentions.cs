using EoraMarketpalce.Web.Common.Constants;
using EoraMarketplace.Data.Domain.Users;
using Microsoft.AspNet.Identity;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace EoraMarketpalce.Web.Common.Extentions
{
    public static class AppUserExtentions
    {
        /// <summary>
        ///     Extension to create user identity data by user model
        /// </summary>
        /// <param name="user">User model</param>
        /// <param name="manager">Identity user manager</param>
        /// <returns></returns>
        public static Task<ClaimsIdentity> GenerateUserIdentityAsync(this User user, UserManager<User, int> manager)
        {
            return manager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
        }

        /// <summary>
        ///     Extension to create user identity data by user model
        /// </summary>
        /// <param name="user">User model</param>
        /// <param name="manager">Identity user manager</param>
        /// <param name="authType">Authentication type</param>
        /// <returns></returns>
        public static Task<ClaimsIdentity> GenerateUserIdentityAsync(this User user, UserManager<User, int> manager, string authType)
        {
            return manager.CreateIdentityAsync(user, authType);
        }

        /// <summary>
        ///     Check if currently user is loggined in Administrator role
        /// </summary>
        /// <param name="identity"></param>
        /// <returns></returns>
        public static bool IsAdmin(this IPrincipal identity)
        {
            return identity.IsInRole(AppConsts.AdminRoleName);
        }

        /// <summary>
        ///     Check if currently user is loggined in User role
        /// </summary>
        /// <param name="identity"></param>
        /// <returns></returns>
        public static bool IsUser(this IPrincipal identity)
        {
            return identity.IsInRole(AppConsts.UserRoleName);
        }

        /// <summary>
        ///     Make image url valid for image src tag
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="originUrl">Image url</param>
        /// <returns></returns>
        public static string UrlToHtmlValid(this HtmlHelper htmlHelper, string originUrl)
        {
            return ImageManager.UrlToHtmlValid(originUrl);
        }
    }
}