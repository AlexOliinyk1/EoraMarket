using EoraMarketpalce.Web.Areas.Admin.Models;
using EoraMarketpalce.Web.Common.Constants;
using EoraMarketpalce.Web.Common.Identity;
using EoraMarketpalce.Web.Controllers.Base;
using EoraMarketplace.Data.Domain.Users;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EoraMarketpalce.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = AppConsts.AdminRoleName)]
    public class AdminPanelController : AppController
    {
        public EoraSignInManager SignInManager { get; set; }

        public EoraUserManager UserManager { get; set; }

        /// <summary>
        ///     Ctor.
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="signInManager"></param>
        public AdminPanelController(EoraUserManager userManager, EoraSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ViewResult Index()
        {
            return View();
        }

        [HttpGet]
        public ViewResult InviteAdmin()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> InviteAdmin(AdminInviteModel model)
        {
            if(ModelState.IsValid)
            {
                User user = UserManager.FindByName(model.InvitedEmail);
                bool canSend = false;

                if(user == null)
                {
                    user = new User { UserName = model.InvitedEmail, Email = model.InvitedEmail, EmailConfirmed = true };
                    IdentityResult result = await UserManager.CreateAsync(user);
                    canSend = result.Succeeded;

                    if(!canSend)
                    {
                        foreach(var error in result.Errors)
                        {
                            ModelState.AddModelError("", error);
                        }
                    }
                }
                else
                    canSend = true;

                if(canSend)
                {
                    try
                    {
                        await SendActivationMail(user);
                        return RedirectToAction("Index");
                    }
                    catch(System.Exception exc)
                    {
                        ModelState.AddModelError("", exc.Message);
                    }
                }
            }

            return View(model);
        }

        private async Task SendActivationMail(User user)
        {
            string code = await HttpContext.GetOwinContext().GetUserManager<EoraUserManager>().GeneratePasswordResetTokenAsync(user.Id);

            var callbackUrl = Url.Action(
                "ResetPassword",
                "SignIn",
                new { userId = user.Id, code = code, area = "" },
                protocol: Request.Url.Scheme);

            string body = @"<h4>Welcome to EoraMarketplace!</h4><p>To get started, please <a href="""
                + callbackUrl + @""">activate</a> your account.</p><p>The account must be activated within 24 hours from receving this mail.</p>";

            await HttpContext.GetOwinContext().GetUserManager<EoraUserManager>().SendEmailAsync(user.Id, "Welcome to EoraMarketplace!", body);
        }
    }
}