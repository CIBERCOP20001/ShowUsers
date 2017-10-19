using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShowUsers.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var logged = (Session["Logged"] != null) ? Convert.ToBoolean(Session["Logged"]) : false;
            if (logged)
            {
                ViewBag.Title = "Show App Users";
                return View();
            }
            else
                return RedirectToAction("Login", "Account");
        }

        public ActionResult Logout()
        {
            Session["Logged"] = false;
            return RedirectToAction("Login", "Account");
        }
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