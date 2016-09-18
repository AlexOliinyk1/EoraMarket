using EoraMarketplace.Data.Domain.Users;
using Microsoft.AspNet.Identity;
using System.Security.Claims;
using System.Threading.Tasks;

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
    }
}