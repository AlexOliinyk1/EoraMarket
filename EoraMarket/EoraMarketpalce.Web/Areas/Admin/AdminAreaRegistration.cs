using System.Web.Mvc;

namespace EoraMarketpalce.Web.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration
    {
        private readonly string _areaName = "Admin";

        public override string AreaName
        {
            get { return _areaName; }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { controller = "AdminPanel", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}