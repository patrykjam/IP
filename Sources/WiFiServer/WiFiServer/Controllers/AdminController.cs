using System;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;
using WiFiServer.Helpers;

namespace WiFiServer.Controllers
{
    public class AdminController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            if (Session["Username"] != null)
            {
                return View("AdminDashboard");
            }
            return View();
        }

        [HttpPost]
        [RequireRouteValues(new[] { "Login" })]
        public ActionResult Index(string Login, string Password)
        {
            var devices = new DbHelper().GetDevices();
            if (Session["Username"] != null)
            {
                return View("AdminDashboard");
            }

            if (ModelState.IsValid)
            {
                {
                    var user = new DbHelper().Authenticate(Login, Password);
                    if (user != null)
                    {
                        Session["Username"] = user.LOGIN;
                        return View("AdminDashboard");
                    }
                }
            }
            return View("AdminDashboard");
            return View();
        }

        [HttpPost]
        [RequireRouteValues(new []{"DeviceId"})]
        public ActionResult Index(string DeviceId, bool block)
        {
            if (Session["Username"] != null)
            {
                return View("AdminDashboard");
            }

            return View("Index");
        }
    }
}