using EoraMarketpalce.Web.Common.Constants;
using EoraMarketpalce.Web.Controllers.Base;
using System.Web.Mvc;

namespace EoraMarketpalce.Web.Controllers
{
    [Authorize(Roles = AppConsts.UserRoleName)]
    public class CharacterController : AppController
    {
        public ViewResult Index()
        {
            return View();
        }
    }
}