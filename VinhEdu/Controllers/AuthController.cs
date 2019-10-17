using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using VinhEdu.Models;
using VinhEdu.Repository;
using VinhEdu.Utilities;
using VinhEdu.ViewModels;
using static VinhEdu.Models.AdditionalDefinition;

namespace VinhEdu.Controllers
{
    public class AuthController : Controller
    {
        UnitOfWork db = new UnitOfWork();
        public ActionResult Index()
        {
            return RedirectToAction("Login");
        }
        public ActionResult Login()
        {
            if (Request.IsAuthenticated)
            {
                return RedirectToAction("Index", "Admin");
            }
            return View();
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
        public void setCookie(string username, bool rememberme = false, string role = "normal")
        {
            var authTicket = new FormsAuthenticationTicket(
                               1,
                               username,
                               DateTime.Now,
                               DateTime.Now.AddMinutes(120),
                               rememberme,
                               role
                               );

            string encryptedTicket = FormsAuthentication.Encrypt(authTicket);

            var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
            Response.Cookies.Add(authCookie);
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model, string ReturnUrl)
        {

            if (ModelState.IsValid)
            {
                var exist = db.UserRepository.CheckExistByIdentifier(model.Identify);
                if (exist)
                {
                    var user = db.UserRepository.FindByIdentifier(model.Identify);
                    if (user.Password == Common.CalculateMD5Hash(model.Password) && (user.Status == UserStatus.Activated || user.Status == UserStatus.NotActivated))
                    {
                        setCookie(user.Identifier, model.RememberMe, user.Role);
                        Session["UserID"] = user.ID;
                        Session["Name"] = user.FullName;
                        if (ReturnUrl != null)
                        {
                            return Redirect(ReturnUrl);
                        }

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
                    ViewBag.Error = "Sai tài khoản hoặc mật khẩu!";
                    return View();
                }

            }

            ViewBag.Error = "Sai tài khoản hoặc mật khẩu!";
            return View();
        }

        [Authorize]
        public ActionResult ChangePass()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ChangePass(ChangePassViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.oldpassword == model.password)
                {
                    ViewBag.anno = "Mật khẩu mới không được trùng mật khẩu cũ !";
                    return View();
                }
                else
                {
                    User user = db.UserRepository.FindByID((int)Session["UserID"]);
                    user.Password = Common.CalculateMD5Hash(model.password);
                    db.SaveChanges();
                    return RedirectToAction("Logout");
                }
            }
            return View();
        }
    }
}