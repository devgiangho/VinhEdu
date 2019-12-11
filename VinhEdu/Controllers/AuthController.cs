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
            if (Request.IsAuthenticated)
            {
                return RedirectToAction("Index", "Admin");
            }
            return RedirectToAction("Login");
        }
        public ActionResult Login()
        {
            if (Request.IsAuthenticated)
            {
                if (User.IsInRole("student"))
                {
                    return RedirectToAction("Index", "Student");
                }
                if (User.IsInRole("teacher"))
                {
                    return RedirectToAction("Index", "Teacher");
                }
                if (User.IsInRole("headmaster"))
                {
                    return RedirectToAction("Index", "HeadMaster");
                }
                return RedirectToAction("Index", "Admin");
            }
            return View();
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
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
            if (Request.IsAuthenticated)
            {
                return RedirectToAction("Index", "Admin");
            }
            if (ModelState.IsValid)
            {
                var exist = db.UserRepository.CheckExistByIdentifier(model.Identify);
                if (exist)
                {
                    var user = db.UserRepository.FindByIdentifier(model.Identify);
                    if (user.Password == Common.CalculateMD5Hash(model.Password) && (user.Status == UserStatus.Activated || user.Status == UserStatus.NotActivated))
                    {
                        
                        var currentconfig = db.ConfigRepository.GetAll().Where(z => z.IsActive == true).FirstOrDefault();
                        Semester setting = db.context.Settings.FirstOrDefault().Semester;
                        setCookie(user.Identifier, model.RememberMe, user.Role);
                        Session["UserID"] = user.ID;
                        Session["Name"] = user.FullName;
                        if(user.SubjectID != null)
                        {
                            //Nếu là giáo viên thì lấy môn đang dạy
                            Session["SubjectID"] = user.SubjectID;
                            Session["SubjectName"] = user.Subject.SubjectName;
                            Session["SemesterName"] = setting.GetDisplayName();
                        }
                        if(user.Type == UserType.Student)
                        {
                            //Nếu là học sinh thì lấy lớp học hiện tại
                            Session["ClassName"] = user.ClassMembers
                                .Where(c => c.ConfigureID == currentconfig.ID &&
                                c.LearnStatus != LearnStatus.Finished && c.LearnStatus != LearnStatus.Switched)
                                .Select(c => c.Class.ClassName)
                                .FirstOrDefault();
                            Session["SchoolName"] = user.ClassMembers
                                .Where(c => c.ConfigureID == currentconfig.ID &&
                                c.LearnStatus != LearnStatus.Switched)
                                .Select(c => c.Class.School.SchoolName);
                        }
                        if(user.Type == UserType.HeadMaster)
                        {
                            Session["SchoolName"] = user.School.SchoolName;
                        }
                        Session["SemesterName"] = setting.GetDisplayName();
                        Session["ConfigID"] = currentconfig.ID;
                        Session["SchoolYear"] = currentconfig.SchoolYear;

                        if (ReturnUrl != null)
                        {
                            return Redirect(ReturnUrl);
                        }

                        if (User.IsInRole("student"))
                        {
                            return RedirectToAction("Index", "Student");
                        }
                        if (User.IsInRole("teacher"))
                        {
                            return RedirectToAction("Index", "Teacher");
                        }
                        if (User.IsInRole("headmaster"))
                        {
                            return RedirectToAction("Index", "HeadMaster");
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
        [Authorize(Roles = "admin")]
        public JsonResult ChangeUserPass(string Identifier, string NewPass)
        {
            try
            {
                bool exist = db.UserRepository.CheckExistByIdentifier(Identifier);
                if (exist)
                {
                    User u = db.UserRepository.FindByIdentifier(Identifier);
                    u.Password = Common.CalculateMD5Hash(NewPass);
                    db.SaveChanges();
                    return Json("Thành công", JsonRequestBehavior.AllowGet);
                }
                Response.StatusCode = 500;
                return Json("Lỗi", JsonRequestBehavior.AllowGet);
            }
            catch(Exception e)
            {
                Response.StatusCode = 500;
                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }
        }
    }
}