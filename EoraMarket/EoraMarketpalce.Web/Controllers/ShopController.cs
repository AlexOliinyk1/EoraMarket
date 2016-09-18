using System.Web.Mvc;

namespace EoraMarketpalce.Web.Controllers
{
    [AllowAnonymous]
    public class ShopController : Controller
    {
        // GET: Shop
        public ActionResult Index()
        {
            return View();
        }
    }
}