using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Web.Http;

namespace EoraMarketpalce.Web
{
    public class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            
            var jsonFormater = GlobalConfiguration.Configuration.Formatters.JsonFormatter;

            jsonFormater.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            jsonFormater.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        }
    }
}