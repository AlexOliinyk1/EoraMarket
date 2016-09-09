using System.Security.Claims;
using System.Threading.Tasks;
using EoraMarketplace.Data.Domain.Users;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace EoraMarketpalce.Web.Models.Identity
{
    public class EoraSignInManager : SignInManager<User, int>
    {
        public EoraSignInManager(UserManager<User, int> userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
            this.AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie;
        }
    }
}