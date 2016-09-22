using EoraMarketpalce.Web.Common.Constants;
using EoraMarketpalce.Web.Common.Identity;
using EoraMarketpalce.Web.Controllers.Base;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;

namespace EoraMarketpalce.Web.Controllers
{
    [AllowAnonymous]
    public class HomeController : AppController
    {
        public EoraUserManager UserManager { get; private set; }

        /// <summary>
        ///     Ctor.
        /// </summary>
        /// <param name="userManager"></param>
        public HomeController(EoraUserManager userManager)
        {
            UserManager = userManager;
        }

        public ActionResult Index()
        {
            if(User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Shop");

            return View();
        }

        [Authorize]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}