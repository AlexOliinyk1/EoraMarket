using EoraMarketpalce.Web.Common.Constants;
using EoraMarketpalce.Web.Common.Extentions;
using EoraMarketpalce.Web.Common.Identity;
using EoraMarketpalce.Web.Controllers.Base;
using EoraMarketpalce.Web.Models.Auth;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Practices.Unity;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EoraMarketpalce.Web.Controllers
{
    [AllowAnonymous]
    public class SignInController : AppController
    {
        /// <summary>
        ///     Get instance of AuthenticationManager by Owin context
        /// </summary>
        private IAuthenticationManager AuthenticationManager
        {
            get { return HttpContext.GetOwinContext().Authentication; }
        }

        public EoraSignInManager SignInManager { get; private set; }

        public EoraUserManager UserManager { get; private set; }

        /// <summary>
        ///     Ctor.
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="signInManager"></param>
        public SignInController(EoraUserManager userManager, EoraSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        /// <summary>
        ///     Get view to login user
        /// </summary>
        /// <param name="returnUrl">Url of location where user was before auth request</param>
        /// <returns></returns>
        [HttpGet]
        public ViewResult Index(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        /// <summary>
        ///     Process login request
        /// </summary>
        /// <param name="login">Login credentials</param>
        /// <param name="returnUrl">Url of location where user was before auth request</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(SignInViewModel login, string returnUrl)
        {
            if(!ModelState.IsValid)
            {
                return View(login);
            }

            var result = await SignInManager.PasswordSignInAsync(login.Email, login.Password, login.RememberMe, shouldLockout: false);

            switch(result)
            {
                case SignInStatus.Success:
                    if(!string.IsNullOrEmpty(returnUrl))
                        return RedirectToLocal(returnUrl);

                    return RedirectToAction("Index", "Home");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(login);
            }
        }

        /// <summary>
        ///     Process log out request
        /// </summary>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        public async Task<RedirectToRouteResult> SignOut()
        {
            ActiveCharacter = null;
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);

            return RedirectToAction("Index", "Home");
        }
    }
}