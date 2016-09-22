using EoraMarketpalce.Web.Common.Constants;
using EoraMarketpalce.Web.Common.Filters;
using EoraMarketpalce.Web.Controllers.Base;
using System.Web.Mvc;

namespace EoraMarketpalce.Web.Controllers
{
    [Authorize]
    public class ShopController : AppController
    {
        public ActionResult Index()
        {
            if(User.IsInRole(AppConsts.UserRoleName) && ActiveCharacter == null)
                return RedirectToAction("Index", "Character");

            return View();
        }

        [HttpGet]
        [AccessAuthorize(Roles = AppConsts.AdminRoleName)]
        public ViewResult CreateProduct()
        {
            return View();
        }
    }
}