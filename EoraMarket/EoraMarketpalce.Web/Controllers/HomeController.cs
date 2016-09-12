using EoraMarketpalce.Web.Controllers.Base;
using System.Web.Mvc;

namespace EoraMarketpalce.Web.Controllers
{
    public class HomeController : AppController
    {
        public ActionResult Index()
        {
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