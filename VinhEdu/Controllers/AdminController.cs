using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VinhEdu.Models;
using VinhEdu.Repository;
using VinhEdu.Utilities;
using VinhEdu.ViewModels;

namespace VinhEdu.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        // GET: Admin
        UnitOfWork db = new UnitOfWork();
        EduVinhContext Context = new EduVinhContext();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CreateStudent()
        {
            List<School> lst = db.SchoolRepository.GetAll().ToList();
            List<Configure> configures = db.ConfigRepository.GetAll().ToList();
            ViewBag.Config = new SelectList(configures, "ID", "SchoolYear");
            ViewBag.SchoolList = new SelectList(lst, "SchoolID", "SchoolName");
            return View();
        }
        public ViewResult AllStudent()
        {
            List<School> lst = db.SchoolRepository.GetAll().ToList();
            List<Configure> configures = db.ConfigRepository.GetAll().ToList();
            int id = lst.First().SchoolID;
            List<Class> classes = db.ClassRepository.GetAll().Where(e => e.SchoolID == id).ToList();
            ViewBag.Class = new SelectList(classes, "ClassID", "ClassName");
            ViewBag.Config = new SelectList(configures, "ID", "SchoolYear");
            ViewBag.SchoolList = new SelectList(lst, "SchoolID", "SchoolName");
            return View();
        }
        public JsonResult GetStudent(int ClassID, int ConfigureID)
        {
            var q = (from u in Context.Users
                     join a in Context.ClassMembers
                     on u.ID equals a.UserID
                     where a.ConfigureID == ConfigureID && u.Type == AdditionalDefinition.UserType.Student &&a.ClassID == ClassID
                     && u.Status == AdditionalDefinition.UserStatus.Activated
                     select new StudentList
                     {
                         Identifier = u.Identifier,
                         DateOfBirth = u.DateOfBirth,
                         FullName = u.FullName,
                         Gender = u.Gender,
                     }).ToList();

            return Json(q, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteStudent(string Identifier)
        {
            try
            {
                User u = db.UserRepository.FindByIdentifier(Identifier);
                u.Status = AdditionalDefinition.UserStatus.Deleted;
                db.SaveChanges();
                return Json("Thành công", JsonRequestBehavior.AllowGet);
            }
            catch(Exception e)
            {
                Response.StatusCode = 500;
                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetClassBySchoolID(int id)
        {
            try
            {
                bool exist =  db.ClassRepository.GetAll().Where(e => e.SchoolID == id).Any();
                if(exist)
                {
                    var lst = db.ClassRepository.GetAll().Where(e => e.SchoolID == id)
                    .Select(c => new
                    {
                        c.ClassID,
                        c.ClassName,
                    })
                    .ToList();

                    return Json(lst, JsonRequestBehavior.AllowGet);
                }
                Response.StatusCode = 500;
                return Json(new { Message = "Trường học không tồn tại" }, JsonRequestBehavior.AllowGet);
            }
            catch(Exception e)
            {
                Response.StatusCode = 500;
                return Json(new { Message = e }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult AddBatchStudent(List<StudentList> students, int ClassID, int ConfigureID)
        {
            try
            {
                List<User> lst = new List<User>();
                foreach(StudentList item in students)
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
                            Identifier = item.Identifier.ToLower(),
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
                //Thêm vào lớp
                lst.ForEach(e =>
                {
                    db.MemberRepository.Add(new ClassMember
                    {
                        ClassID = ClassID,
                        ConfigureID = ConfigureID,
                        UserID = e.ID,
                        IsCurrent = true,
                    });
                });
                db.SaveChanges();
                return Json(new {message = "Thêm thành công!"}, JsonRequestBehavior.AllowGet);
            }
            catch(Exception e)
            {
                Response.StatusCode = 500;
                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }
            
        }
    }
    
}