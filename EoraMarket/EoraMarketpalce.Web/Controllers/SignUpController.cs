using EoraMarketpalce.Web.Common.Identity;
using EoraMarketpalce.Web.Controllers.Base;
using EoraMarketpalce.Web.Models.Auth;
using EoraMarketplace.Data.Domain.Users;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace EoraMarketpalce.Web.Controllers
{
    [AllowAnonymous]
    public class SignUpController : AppController
    {
        public EoraSignInManager SignInManager { get; set; }

        public EoraUserManager UserManager { get; set; }

        /// <summary>
        ///     Ctor.
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="signInManager"></param>
        public SignUpController(EoraUserManager userManager, EoraSignInManager signInManager)
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
        public async Task<ActionResult> Index(SignUpViewModel register)
        {
            if(ModelState.IsValid)
            {
                var user = new User { UserName = register.Email, Email = register.Email };
                var result = await UserManager.CreateAsync(user, register.Password);
                if(result.Succeeded)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    return RedirectToAction("Index", "Home");
                }
                AddErrors(result);
            }

            return View(register);
        }
    }
}