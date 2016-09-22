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
        public static Task<ClaimsIdentity> GenerateUserIdentityAsync(this User user, UserManager<User, int> manager)
        {
            return manager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
        }

        public static Task<ClaimsIdentity> GenerateUserIdentityAsync(this User user, UserManager<User, int> manager, string authType)
        {
            return manager.CreateIdentityAsync(user, authType);
        }

        public static bool IsAdmin(this IPrincipal identity)
        {
            return identity.IsInRole(AppConsts.AdminRoleName);
        }

        public static bool IsUser(this IPrincipal identity)
        {
            return identity.IsInRole(AppConsts.UserRoleName);
        }

        public static string UrlToHtmlValid(this HtmlHelper htmlHelper, string originUrl)
        {
            return originUrl.Replace("~", "");
        }
    }
}