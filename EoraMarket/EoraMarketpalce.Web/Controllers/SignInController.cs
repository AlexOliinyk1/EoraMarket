using EoraMarketpalce.Web.Common.Identity;
using EoraMarketpalce.Web.Controllers.Base;
using EoraMarketpalce.Web.Models.Auth;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Practices.Unity;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace EoraMarketpalce.Web.Controllers
{
    [AllowAnonymous]
    public class SignInController : AppController
    {
        public EoraSignInManager SignInManager { get; set; }

        public EoraUserManager UserManager { get; set; }

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

        [HttpGet]
        public ViewResult Index()
        {
            return View();
        } 

        [HttpPost]
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
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = login.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(login);
            }
        }
    }
}