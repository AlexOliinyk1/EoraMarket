using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using EoraMarketplace.Data.Domain.Users;
using Microsoft.Owin;

namespace EoraMarketpalce.Web.Common.Identity
{
    public class EoraSignInManager : SignInManager<User, int>
    {
        public EoraSignInManager(UserManager<User, int> userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
            this.AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie;
        }

        public static EoraSignInManager Create(IdentityFactoryOptions<EoraSignInManager> options, IOwinContext context)
        {
            return new EoraSignInManager(context.GetUserManager<EoraUserManager>(), context.Authentication);
        }
    }
}