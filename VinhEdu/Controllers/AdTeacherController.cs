using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VinhEdu.Models;
using VinhEdu.Repository;
using VinhEdu.ViewModels;
using static VinhEdu.Models.AdditionalDefinition;

namespace VinhEdu.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdTeacherController : Controller
    {
        // GET: AdTeacher
        UnitOfWork db = new UnitOfWork();
        EduVinhContext Context = new EduVinhContext();
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
        /// <summary>
        /// Lấy danh sách giáo viên của lớp
        /// </summary>
        /// <param name="ClassID"></param>
        /// <param name="ConfigureID"></param>
        /// <returns></returns>
        public JsonResult GetTeacher(int ClassID, int ConfigureID)
        {
            var q = (from u in Context.Users
                     join a in Context.ClassMembers
                     on u.ID equals a.UserID
                     where a.ConfigureID == ConfigureID && u.Type == AdditionalDefinition.UserType.Teacher && a.ClassID == ClassID
                     && u.Status != AdditionalDefinition.UserStatus.Deleted
                     select new TeacherList
                     {
                         Identifier = u.Identifier,
                         DateOfBirth = u.DateOfBirth,
                         FullName = u.FullName,
                         Gender = u.Gender,
                         Status = u.Status,
                         SubjectName = u.Subject.SubjectName,
                         IsHomeTeacher = a.IsHomeTeacher,
                     }).ToList();

            return Json(q, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Xóa học sinh
        /// </summary>
        /// <param name="Identifier"></param>
        /// <returns></returns>
        public JsonResult DeleteTeacher(string Identifier)
        {
            try
            {
                User u = db.UserRepository.FindByIdentifier(Identifier);
                u.Status = AdditionalDefinition.UserStatus.Deleted;
                db.SaveChanges();
                return Json("Thành công", JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Response.StatusCode = 500;
                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }
        }
        /// <summary>
        /// Cập nhật nhiều học sinh 1 lúc
        /// </summary>
        /// <param name="students"></param>
        /// <returns></returns>
        public JsonResult UpdateBatchTeacher(List<TeacherList> teachers)
        {
            try
            {
                foreach (TeacherList item in teachers)
                {
                    User u = db.UserRepository.FindByIdentifier(item.Identifier);
                    u.Identifier = item.Identifier;
                    u.DateOfBirth = item.DateOfBirth;
                    u.FullName = item.FullName;
                    u.Gender = item.Gender;
                    u.Status = item.Status;
                    db.SaveChanges();
                }
                return Json(new { message = "Cập nhật thành công!" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Response.StatusCode = 500;
                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }

        }
    }
}