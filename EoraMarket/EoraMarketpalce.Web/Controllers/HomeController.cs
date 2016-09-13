using EoraMarketpalce.Web.Common.Constants;
using EoraMarketpalce.Web.Common.Identity;
using EoraMarketpalce.Web.Controllers.Base;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;

namespace EoraMarketpalce.Web.Controllers
{
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
            return View();
        }

        [Authorize]
        public ActionResult About()
        {
            var isAdmin = User.IsInRole(AppConsts.AdminRoleName);
            var id = User.Identity.GetUserId();
            UserManager.GetEmailAsync()
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