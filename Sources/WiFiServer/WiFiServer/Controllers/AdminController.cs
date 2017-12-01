using System.Web.Mvc;
using WiFiServer.Helpers;

namespace WiFiServer.Controllers
{
    public class AdminController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return Session["Username"] != null ? View("AdminDashboard") : View();
        }

        [HttpPost]
        public ActionResult Index(string Login, string Password)
        {
            if (Session["Username"] != null)
            {
                return View("AdminDashboard");
            }

            if (!ModelState.IsValid) return View();
            {
                var user = new DbHelper().Authenticate(Login, Password);
                if (user == null) return View();
                Session["Username"] = user.LOGIN;
                return View("AdminDashboard");
            }
        }

        [HttpPost]
        public ActionResult Remove(string DeviceId, bool block)
        {
            if (Session["Username"] != null)
            {
                new DbHelper().ChangeBlockedDeviceStatus(DeviceId, block);
                return View("AdminDashboard");
            }
            return RedirectToAction("Index");
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index");
        }
    }
}