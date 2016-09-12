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

        /// <summary>
        ///     Get sign up view
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ViewResult Index()
        {
            return View();
        }

        /// <summary>
        ///     Process data from sign up form
        /// </summary>
        /// <param name="register">Sign up view model</param>
        /// <returns></returns>
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

                    //  TODO: send email confirmation
                    //string emailCode = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    //var emailCallbackUrl = Url.Action("ConfirmEmail", "SignUp", new { id = user.Id, code = emailCode }, protocol: Request.Url.Scheme);
                    //await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + emailCallbackUrl + "\">here</a>");

                    return RedirectToAction("Index", "Home");
                }

                AddErrors(result);
            }

            return View(register);
        }

        /// <summary>
        ///     Process email confirmation request
        /// </summary>
        /// <param name="id">Id of user that do confirm</param>
        /// <param name="code">Confirmation token</param>
        /// <returns></returns>
        public async Task<ActionResult> ConfirmEmail(int id, string code)
        {
            if(id == 0 || code == null)
            {
                return View("Error");
            }
            var result = await UserManager.ConfirmEmailAsync(id, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }
    }
}