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
        public ActionResult StudentLogin(StudentLoginViewModel model, string ReturnUrl)
        {

            if (ModelState.IsValid)
            {
                var exist = db.UserRepository.CheckExistByStudentID(model.Identify);
                if (exist)
                {
                    var user = db.UserRepository.FindByStudentID(model.Identify);
                    if (user.Password == Common.CalculateMD5Hash(model.Password) && (user.Status == UserStatus.Activated || user.Status == UserStatus.NotActivated))
                    {
                        setCookie(user.StudentID, model.RememberMe, user.Role);
                        if (ReturnUrl != null)
                            return Redirect(ReturnUrl);
                        return RedirectToAction("Index", "Admin");
                    }
                    ViewBag.Error = "Sai tài khoản hoặc mật khẩu!";
                    return View();
                }

            }

            ViewBag.Error = "Sai tài khoản hoặc mật khẩu!";
            return View();
        }
        [HttpPost]
        public ActionResult TeacherLogin(TeacherLoginViewModel model, string ReturnUrl)
        {

            if (ModelState.IsValid)
            {
                var exist = db.UserRepository.CheckExistByEmail(model.Identify);
                if (exist)
                {
                    var user = db.UserRepository.FindByEmail(model.Identify);
                    if (user.Password == Common.CalculateMD5Hash(model.Password) && (user.Status == UserStatus.Activated || user.Status == UserStatus.NotActivated))
                    {
                        setCookie(user.Email, model.RememberMe, user.Role);
                        if (ReturnUrl != null)
                            return Redirect(ReturnUrl);
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
                    User user = null;
                    if(User.IsInRole("student"))
                    {
                        user = db.UserRepository.FindByStudentID(User.Identity.Name);
                    }
                    else
                    {
                        user = db.UserRepository.FindByEmail(User.Identity.Name);
                    }
                    if (user != null)
                    {
                        user.Password = Common.CalculateMD5Hash(model.password);
                        db.SaveChanges();
                        return RedirectToAction("Logout");
                    }
                }
            }
            return View();
        }
    }
}