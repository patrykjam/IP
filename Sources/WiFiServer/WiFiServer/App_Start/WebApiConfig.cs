using System.Web.Http;

namespace WiFiServer
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "TrackingApi",
                routeTemplate: "{controller}"
            );
        }
    }
}
