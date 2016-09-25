using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using EoraMarketplace.Data.Domain.Users;
using Microsoft.Owin;

namespace EoraMarketpalce.Web.Common.Identity
{
    /// <summary>
    ///     Identity Sign In manager
    /// </summary>
    public class EoraSignInManager : SignInManager<User, int>
    {
        /// <summary>
        ///     Ctor.
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="authenticationManager"></param>
        public EoraSignInManager(UserManager<User, int> userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
            this.AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie;
        }

        /// <summary>
        ///     Create instance of EoraSignInManager
        /// </summary>
        /// <param name="options"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public static EoraSignInManager Create(IdentityFactoryOptions<EoraSignInManager> options, IOwinContext context)
        {
            return new EoraSignInManager(context.GetUserManager<EoraUserManager>(), context.Authentication);
        }
    }
}