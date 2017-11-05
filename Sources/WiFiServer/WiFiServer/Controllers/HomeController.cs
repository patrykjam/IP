using System.Web.Mvc;

namespace WiFiServer.Controllers
{
    public class HomeController : Controller
    {
        public string Index()
        {
            ViewBag.Title = "WiFi Tracker";
            
            return "Hello";
        }
    }
}
