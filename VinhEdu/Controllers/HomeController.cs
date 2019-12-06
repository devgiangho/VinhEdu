using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VinhEdu.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
            {
                if (User.IsInRole("student"))
                {
                    return RedirectToAction("Index", "Student");
                }
                if (User.IsInRole("teacher") || User.IsInRole("headmaster"))
                {
                    return RedirectToAction("Index", "Teacher");
                }
                return RedirectToAction("Index", "Admin");
            }
            return RedirectToAction("Login", "Auth");
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