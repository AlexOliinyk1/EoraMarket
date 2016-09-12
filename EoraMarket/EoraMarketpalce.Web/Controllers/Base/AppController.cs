using Microsoft.AspNet.Identity;
using System;
using System.Web.Mvc;

namespace EoraMarketpalce.Web.Controllers.Base
{
    public class AppController : Controller
    {
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