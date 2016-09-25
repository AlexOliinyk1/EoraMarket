using EoraMarketpalce.Web.Controllers.Base;
using System.Web.Mvc;

namespace EoraMarketpalce.Web.Controllers
{
    public class ErrorController : AppController
    {
        public ActionResult NotFound()
        {
            return View();
        }

        public ActionResult ServerError()
        {
            return View();
        }

        public ActionResult Forbidden()
        {
            return View();
        }
    }
}