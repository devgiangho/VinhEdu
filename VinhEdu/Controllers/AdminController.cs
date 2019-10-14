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
    public class AdminController : Controller
    {
        // GET: Admin
        UnitOfWork db = new UnitOfWork();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CreateStudent()
        {
            List<School> lst = db.SchoolRepository.GetAll().ToList();
            ViewBag.SchoolList = new SelectList(lst, "SchoolID", "SchoolName");
            return View();
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
        public JsonResult AddBatchStudent(List<StudentList> students)
        {
            try
            {
                List<User> lst = new List<User>();
                foreach(StudentList item in students)
                {
                    User std = new User
                    {
                        DateOfBirth = item.DateOfBirth,
                        CreateDate = DateTime.Now,
                        FullName = item.FullName,
                        Identifier = item.Identifier,
                        Password = item.Password,
                        Role = "student",
                        Status = AdditionalDefinition.UserStatus.Activated,
                        Type = AdditionalDefinition.UserType.Student,
                        Gender = item.Gender,
                    };
                    lst.Add(std);
                }
                db.UserRepository.AddRangeUser(lst);
                //Thêm vào lớp
            }
            catch(Exception e)
            {
                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }
            
        }
    }
    
}