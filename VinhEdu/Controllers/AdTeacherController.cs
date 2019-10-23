using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VinhEdu.Models;
using VinhEdu.Repository;
using VinhEdu.ViewModels;

namespace VinhEdu.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdTeacherController : Controller
    {
        // GET: AdTeacher
        UnitOfWork db = new UnitOfWork();
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult CreateTeacher(TeacherViewModel model)
        {
            if (ModelState.IsValid)
            {
                bool exist = db.UserRepository.CheckExistByIdentifier(model.Identifier);
                if(!exist)
                {
                    User teacher = new User
                    {
                        Identifier = model.Identifier,
                        Password = Utilities.Common.CalculateMD5Hash(model.Password),
                        FullName = model.FullName,
                        DateOfBirth = model.DateOfBirth,
                        SubjectID = model.SubjectID,
                        Status = model.Status,
                        Role = "teacher",
                        Type = AdditionalDefinition.UserType.Teacher,
                        Gender = model.Gender,
                        CreateDate = DateTime.Now,
                    };
                    db.UserRepository.AddUser(teacher);
                    //Thêm vào lớp
                    ClassMember member = new ClassMember
                    {
                        UserID = teacher.ID,
                        ClassID = model.ClassID,
                        ConfigureID = model.ConfigureID,
                    };
                    db.MemberRepository.Add(member);
                    db.SaveChanges();
                    return Json(new { message = "Thành công", success = true }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { message = "Đã tồn tại Email này trên hệ thống", success = false }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { message = "Nhập các trường chưa đúng", success = false }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}