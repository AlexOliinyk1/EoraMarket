using EoraMarketpalce.Web.Common.Constants;
using EoraMarketpalce.Web.Controllers.Base;
using System.Web.Mvc;

namespace EoraMarketpalce.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = AppConsts.AdminRoleName)]
    public class AdminPanelController : AppController
    {
        public ViewResult Index()
        {
            return View();
        }
    }
}