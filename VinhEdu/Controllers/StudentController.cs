using Newtonsoft.Json;
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
    [Authorize(Roles = "student")]
    public class StudentController : Controller
    {
        UnitOfWork db = new UnitOfWork();
        public ActionResult Index()
        {
            return View();
        }
        public ViewResult PointBoard()
        {
            return View();
        }
        /// <summary>
        /// Lấy điểm năm học và kì học hiện tại
        /// </summary>
        /// <returns></returns>
        public JsonResult GetCurrentPointBoard()
        {
            var CurrentConfig = db.ConfigRepository.GetAll().Where(z => z.IsActive == true).FirstOrDefault().ID;//(int)Session["ConfigID"];
            var Semester  = db.context.Settings.FirstOrDefault().Semester;
            var UserID = (int)Session["UserID"];
            var CurrentClass = db.MemberRepository.GetAll()
                .Where(e => e.ConfigureID == CurrentConfig && e.UserID == UserID 
                && e.LearnStatus == AdditionalDefinition.LearnStatus.Learning).FirstOrDefault();
            
            List<ShowMark> showMarks = (from m in db.context.ClassMembers
                                     join p in db.context.PointBoards on m.UserID equals p.StudentID into pm
                                     from p in pm.DefaultIfEmpty()
                                     where m.User.Status == AdditionalDefinition.UserStatus.Activated
                                     where m.User.ID == UserID
                                     where m.ConfigureID == CurrentConfig
                                     where p.Semester == Semester
                                     where m.ClassID == CurrentClass.ClassID
                                        select new ShowMark
                                     {
                                         SubjectName = p.Subject.SubjectName,
                                         StudentID = m.UserID,
                                         StudentName = m.User.FullName,
                                         TempScore = p.Score,
                                     }).ToList();
            foreach (var item in showMarks)
            {
                if (item.TempScore != null)
                {
                    item.Score = JsonConvert.DeserializeObject<Score>(item.TempScore);
                }
            }
            return Json(showMarks, JsonRequestBehavior.AllowGet);
        }
    }
}