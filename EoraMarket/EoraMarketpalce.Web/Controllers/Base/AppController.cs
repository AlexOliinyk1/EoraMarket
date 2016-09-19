using EoraMarketpalce.Web.Common.Constants;
using EoraMarketpalce.Web.Models.Characters;
using EoraMarketplace.Data.Domain.Characters;
using Microsoft.AspNet.Identity;
using System;
using System.Web.Mvc;

namespace EoraMarketpalce.Web.Controllers.Base
{
    public class AppController : Controller
    {
        /// <summary>
        ///     Get or set active user character stored in session
        /// </summary>
        public CharacterInfoViewModel ActiveCharacter
        {
            get {
                if(Session[AppConsts.CHARACTER_STORE_NAME] == null)
                    return null;
                return Session[AppConsts.CHARACTER_STORE_NAME] as CharacterInfoViewModel;
            }
            set {
                Session[AppConsts.CHARACTER_STORE_NAME] = value;
            }
        }

        protected ActionResult RedirectToLocal(string returnUrl)
        {
            if(Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        protected void AddErrors(IdentityResult result)
        {
            foreach(var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        protected void AddErrors(Exception result)
        {
            ModelState.AddModelError("", result.Message);
        }
    }
}