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
    [Authorize(Roles = "teacher")]
    public class TeacherController : Controller
    {
        UnitOfWork db = new UnitOfWork();
        EduVinhContext Context = new EduVinhContext();
        public ActionResult Index()
        {
            return View();
        }
        public ViewResult ClassList()
        {
            Configure configure = db.ConfigRepository.GetAll()
                .Where(e => e.IsActive).First();
            int userid = (int)Session["UserID"];
            List<ClassList> classLists = (from m in Context.ClassMembers
                                          join c in Context.Classes on m.ClassID equals c.ClassID
                                          join u in Context.Users on m.UserID equals u.ID
                                          where m.ConfigureID == configure.ID
                                          where u.ID == userid
                                          select new ClassList
                                          {
                                              ClassName = c.ClassName,
                                              ClassID = c.ClassID,
                                              //StudentCount = c.ClassMembers.Where(e => e.ConfigureID == configure.ID && ).Count(),
                                          }).ToList();
            return View(classLists);
        }
    }
}