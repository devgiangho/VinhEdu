using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using VinhEdu.Utilities;
using VinhEdu.ViewModels;
using static VinhEdu.Models.AdditionalDefinition;
using VinhEdu.Models;
using VinhEdu.Repository;

namespace VinhEdu.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdStudentController : Controller
    {
        // GET: AdStudent
        UnitOfWork db = new UnitOfWork();
        EduVinhContext Context = new EduVinhContext();
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// Lấy danh sách học sinh của lớp
        /// </summary>
        /// <param name="ClassID"></param>
        /// <param name="ConfigureID"></param>
        /// <returns></returns>
        public JsonResult GetStudent(int ClassID, int ConfigureID)
        {
            var q = (from u in Context.Users
                     join a in Context.ClassMembers
                     on u.ID equals a.UserID
                     where a.ConfigureID == ConfigureID && u.Type == AdditionalDefinition.UserType.Student && a.ClassID == ClassID
                     && u.Status != AdditionalDefinition.UserStatus.Deleted && a.LearnStatus != LearnStatus.Switched
                     select new StudentList
                     {
                         Identifier = u.Identifier,
                         DateOfBirth = u.DateOfBirth,
                         FullName = u.FullName,
                         Gender = u.Gender,
                         Status = u.Status
                     }).ToList();

            return Json(q, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Xóa học sinh
        /// </summary>
        /// <param name="Identifier"></param>
        /// <returns></returns>
        public JsonResult DeleteStudent(string Identifier)
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
        /// Lấy danh sách lớp AJAX
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult GetClassBySchoolID(int id)
        {
            try
            {
                List<School> lst = db.context.Schools.ToList();
                bool exist = lst.Any(a => a.SchoolID == id); ///db.context.Schools.Any(e => e.SchoolID == id);
                if (exist)
                {
                    var classes = db.ClassRepository.GetAll().Where(e => e.SchoolID == id)
                    .Select(c => new ClassSelect
                    {
                        ClassID = c.ClassID,
                        ClassName = c.ClassName,
                    })
                    .ToList();
                    //List<ClassSelect> classes = new List<ClassSelect>();
                    //classes.Add(new ClassSelect
                    //{
                    //    ClassID = 0,
                    //    ClassName = "Tất cả"
                    //});
                    //classes.AddRange(lst);

                    return Json(classes, JsonRequestBehavior.AllowGet);
                }
                Response.StatusCode = 500;
                return Json(new { Message = "Trường học không tồn tại" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Response.StatusCode = 500;
                return Json(new { Message = e }, JsonRequestBehavior.AllowGet);
            }
        }
        /// <summary>
        /// Thêm nhiều học sinh 1 lúc
        /// </summary>
        /// <param name="students"></param>
        /// <param name="ClassID"></param>
        /// <param name="ConfigureID"></param>
        /// <returns></returns>
        public JsonResult AddBatchStudent(List<StudentList> students, int ClassID, int ConfigureID)
        {
            try
            {
                List<User> lst = new List<User>();
                foreach (StudentList item in students)
                {
                    User std = null;
                    bool checkExist = db.UserRepository.CheckExistByIdentifier(item.Identifier.ToLower());
                    if (checkExist)
                    {
                        ///  throw new Exception();
                        Response.StatusCode = 500;
                        return Json("Tài khoản với Mã: " + item.Identifier + " đã tồn tại trên hệ thống.", JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        std = new User
                        {
                            DateOfBirth = item.DateOfBirth,
                            CreateDate = DateTime.Now,
                            FullName = item.FullName,
                            Identifier = item.Identifier.ToUpper(),
                            Password = Common.CalculateMD5Hash(item.Password),
                            Role = "student",
                            Status = AdditionalDefinition.UserStatus.Activated,
                            Type = AdditionalDefinition.UserType.Student,
                            Gender = item.Gender,
                        };
                        lst.Add(std);
                    }

                }
                db.UserRepository.AddRangeUser(lst);
                db.SaveChanges();
                //Thêm vào lớp
                lst.ForEach(e =>
                {
                    db.MemberRepository.Add(new ClassMember
                    {
                        ClassID = ClassID,
                        ConfigureID = ConfigureID,
                        UserID = e.ID,
                        LearnStatus = LearnStatus.Learning,
                    });
                });
                db.SaveChanges();
                return Json(new { message = "Thêm thành công!" }, JsonRequestBehavior.AllowGet);
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
        public JsonResult UpdateBatchStudent(List<StudentList> students)
        {
            try
            {
                foreach (StudentList item in students)
                {
                    User u = db.UserRepository.FindByIdentifier(item.Identifier);
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
        /// <summary>
        /// Chuyển lớp cho học sinh
        /// </summary>
        /// <param name="ClassID"></param>
        /// <param name="Identifier"></param>
        /// <returns></returns>
        public JsonResult SwitchClass(int ClassID, string Identifier)
        {
            try
            {
                // Lấy niên khóa
                Configure Configure = db.ConfigRepository.GetAll().Where(e => e.IsActive).FirstOrDefault();
                // Nếu chuyển ở năm học hiện tại thì cho phép, còn lại thì không
                User student = db.UserRepository.FindByIdentifier(Identifier);
                bool IsLearning = db.MemberRepository.GetAll().Any(e => e.LearnStatus == LearnStatus.Learning && e.ConfigureID == Configure.ID && e.UserID == student.ID);
                if (!IsLearning)
                {
                    return Json(new { message = "Học sinh này đã tốt nghiệp", success = false }, JsonRequestBehavior.AllowGet);
                }
                List<ClassMember> old_member = db.MemberRepository.GetAll().Where(e => e.UserID == student.ID).ToList();
                bool CurrentClass = old_member.Any(e => e.ClassID == ClassID && Configure.ID == e.ConfigureID);
                //Không cho chuyển trùng lớp đang học
                if (CurrentClass)
                {
                    return Json(new { message = "Bạn chuyển trùng lớp đang học", success = false }, JsonRequestBehavior.AllowGet);
                }
                LearnStatus status = LearnStatus.Learning;
                bool SwitchOldClass = false;
                //Bỏ các lớp khác
                old_member.ForEach(e =>
                {
                    // Lấy tình trạng học của lớp cũ để đưa sang lớp mới
                    if (e.LearnStatus == LearnStatus.Learning)
                    {
                        status = e.LearnStatus;
                        //Đánh dấu đã chuyển
                        e.LearnStatus = LearnStatus.Switched;
                    }
                    //Nếu trường hợp chuyển lại lớp cũ..
                    if (e.LearnStatus == LearnStatus.Switched && e.ClassID == ClassID)
                    {
                        e.LearnStatus = LearnStatus.Learning;
                        SwitchOldClass = true;
                    }

                });
                if (!SwitchOldClass)
                {
                    ClassMember new_member = new ClassMember
                    {
                        UserID = student.ID,
                        ClassID = ClassID,
                        ConfigureID = Configure.ID,
                        LearnStatus = status,
                    };
                    db.MemberRepository.Add(new_member);
                }
                db.SaveChanges();
                return Json(new { message = "Chuyển lớp thành công!", success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Response.StatusCode = 500;
                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }
        }
    }
}