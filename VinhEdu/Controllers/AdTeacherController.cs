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
                    //NẾU CÓ NGƯỜI KHÁC DẠY LỚP ĐÓ RỒI THÌ THÔI KHÔNG THÊM ĐƯỢC NỮA
                    //bool check = db.MemberRepository.GetAll()
                    //    .Any(e => e.ClassID == model.ClassID && e.User.SubjectID == model.SubjectID);
                    var SchoolID = model.SchoolID;
                    User teacher = new User
                    {
                        Identifier = model.Identifier,
                        Password = Utilities.Common.CalculateMD5Hash(model.Password),
                        FullName = model.FullName,
                        SchoolID = SchoolID,
                        DateOfBirth = model.DateOfBirth,
                        SubjectID = model.SubjectID,
                        Status = model.Status,
                        Role = "teacher",
                        Type = AdditionalDefinition.UserType.Teacher,
                        Gender = model.Gender,
                        CreateDate = DateTime.Now,
                    };
                    db.UserRepository.AddUser(teacher);
                    
                    //if(!check)
                    //{
                    //    //Thêm vào lớp
                    //    ClassMember member = new ClassMember
                    //    {
                    //        UserID = teacher.ID,
                    //        ClassID = model.ClassID,
                    //        ConfigureID = model.ConfigureID,
                    //    };
                    //    db.MemberRepository.Add(member);
                    //}
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
            var q = new List<TeacherList>();
            if (ClassID != 0)
            {
                q = (from u in Context.Users
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
            }
            else
            {
                q = (from u in Context.Users
                     where u.Type == AdditionalDefinition.UserType.Teacher
                     && u.Status != AdditionalDefinition.UserStatus.Deleted
                     select new TeacherList
                     {
                         Identifier = u.Identifier,
                         DateOfBirth = u.DateOfBirth,
                         FullName = u.FullName,
                         Gender = u.Gender,
                         Status = u.Status,
                         SubjectName = u.Subject.SubjectName,
                         IsHomeTeacher = false,
                     }).ToList();
            }
            

            return Json(q, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Lấy danh sách giáo viên đứng lớp
        /// </summary>
        /// <param name="ClassID"></param>
        /// <param name="ConfigureID"></param>
        /// <returns></returns>
        public JsonResult GetTeacherOnClass(int ClassID, int ConfigureID)
        {
            var q = (from u in Context.Users
                     join a in Context.ClassMembers
                     on u.ID equals a.UserID
                     where a.ConfigureID == ConfigureID && u.Type == AdditionalDefinition.UserType.Teacher && a.ClassID == ClassID
                     && u.Status != AdditionalDefinition.UserStatus.Deleted
                     select new TeacherOnCLass
                     {
                         TeacherID = u.ID,
                         FullName = u.FullName,
                         Gender = u.Gender,
                         SubjectID = u.SubjectID,
                         SubjectName = u.Subject.SubjectName,
                         IsHomeTeacher = a.IsHomeTeacher,
                     }).ToList();
            List<SubjectList> subjects = db.SubjectRepository.GetAll()
                .Select(c => new SubjectList
                {
                    SubjectID = c.ID,
                    SubjectName = c.SubjectName,
                }).ToList();
            subjects.ForEach((item) =>
            {
                if(q.Any(a => a.SubjectID == item.SubjectID))
                {
                    TeacherOnCLass tc = q.Find(z => z.SubjectID == item.SubjectID);
                    item.TeacherID = tc.TeacherID;
                    item.FullName = tc.FullName;
                    item.IsHomeTeacher = tc.IsHomeTeacher;
                    item.Gender = tc.Gender;
                }
                else
                {
                    item.FullName = "Chưa chọn GV";
                }
            });
            return Json(subjects, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Lấy danh sách giáo viên của trường theo môn
        /// </summary>
        /// <param name="ClassID"></param>
        /// <param name="ConfigureID"></param>
        /// <returns></returns>
        public JsonResult GetTeacherBySubject(int SchoolID, int SubjectID)
        {
            var lst = db.UserRepository.AllUser().Where(e => e.SchoolID == SchoolID 
                        && e.SubjectID == SubjectID 
                        && e.Type == UserType.Teacher 
                        && e.Status != UserStatus.Deleted)
                    .Select(c => new
                    {
                        c.ID,
                        c.FullName,
                    })
                    .ToList();

            return Json(lst, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Đổi giáo viên của môn trong 1 lớp
        /// </summary>
        /// <param name="ClassID"></param>
        /// <param name="ConfigID"></param>
        /// <param name="TeacherID"></param>
        /// <returns></returns>
        public JsonResult ChangeTeacherSubject(int ClassID, int ConfigID,int? OldTeacherID,int NewTeacherID)
        {
            try
            {
                //Nếu có giáo viên khác dạy trước thì xóa đi
                bool exist = db.MemberRepository
                    .GetAll()
                    .Any(e => e.ConfigureID == ConfigID && e.ClassID == ClassID && e.UserID == OldTeacherID);
                if(exist)
                {
                    ClassMember member = db.MemberRepository.GetAll()
                        .Where(e => e.ConfigureID == ConfigID && e.ClassID == ClassID && e.UserID == OldTeacherID)
                        .FirstOrDefault();
                    db.MemberRepository.Delete(member);
                }
                ClassMember newMem = new ClassMember
                {
                    ClassID = ClassID,
                    UserID = NewTeacherID,
                    ConfigureID = ConfigID,
                };
                db.MemberRepository.Add(newMem);
                db.SaveChanges();
                return Json("Đổi giáo viên thành công", JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Response.StatusCode = 500;
                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }
        }
        /// <summary>
        /// Xóa gv
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
        /// Cập nhật nhiều GV 1 lúc
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