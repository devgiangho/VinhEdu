using Newtonsoft.Json;
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
            ViewBag.Semester = db.context.Settings.FirstOrDefault().Semester.GetDisplayName();
            return View();
        }
        /// <summary>
        /// Lấy điểm năm học và kì học hiện tại
        /// </summary>
        /// <returns></returns>
        public JsonResult GetStudentPointBoard(Semester semester)
        {
            var CurrentConfig = (int)Session["ConfigID"];
            var CurrentSemester = semester;
            var UserID = (int)Session["UserID"];
            int ClassID = db.MemberRepository
                   .GetAll().Where(c => c.UserID == UserID && c.ConfigureID == CurrentConfig 
                   && (c.LearnStatus != AdditionalDefinition.LearnStatus.Finished))
                   .First().ClassID;
            var CurrentClass = db.MemberRepository.GetAll()
                .Where(e => e.ConfigureID == CurrentConfig && e.UserID == UserID 
                && e.LearnStatus == AdditionalDefinition.LearnStatus.Learning).FirstOrDefault();
            List<int> subjectList = db.SubjectRepository.GetAll().Select(c => c.ID).ToList();
            List<ShowMark> markList = new List<ShowMark>();
            foreach (var subjectID in subjectList)
            {
                ShowMark mark = new ShowMark();
                bool HasMark = db.context.PointBoards
                    .Any(e => e.ClassID == ClassID &&
                    e.StudentID == UserID && e.SubjectID == subjectID
                    && e.ConfigureID == CurrentConfig && e.Semester == CurrentSemester);
                if (HasMark)
                {
                    mark = (from m in db.context.ClassMembers
                            join p in db.context.PointBoards on m.UserID equals p.StudentID into pm
                            from p in pm.DefaultIfEmpty()
                            where m.UserID == UserID
                            where m.ClassID == ClassID
                            where p.SubjectID == subjectID
                            where p.ConfigureID == CurrentConfig
                            where p.Semester == CurrentSemester
                            select new ShowMark
                            {
                                SubjectID = subjectID,
                                SubjectName = p.Subject.SubjectName,
                                TempScore = p.Score,
                            }).First();
                    if (mark.TempScore != null)
                    {
                        mark.Score = JsonConvert.DeserializeObject<Score>(mark.TempScore);
                        mark.TempScore = null;
                    }
                }
                else
                {
                    PointBoard NewMark = new PointBoard
                    {
                        ClassID = ClassID,
                        Score = JsonConvert.SerializeObject(new Score()),
                        StudentID = UserID,
                        SubjectID = subjectID,
                        ConfigureID = CurrentConfig,
                        Semester = CurrentSemester,// == AdditionalDefinition.Semester.HK1 ? AdditionalDefinition.Semester.HK1 : AdditionalDefinition.Semester.HK2
                    };
                    db.context.PointBoards.Add(NewMark);

                    db.context.SaveChanges();
                    mark = (from m in db.context.ClassMembers
                            join p in db.context.PointBoards on m.UserID equals p.StudentID into pm
                            from p in pm.DefaultIfEmpty()
                            where m.UserID == UserID
                            where m.ClassID == ClassID
                            where p.SubjectID == subjectID
                            where p.ConfigureID == CurrentConfig
                            where p.Semester == CurrentSemester
                            select new ShowMark
                            {
                                SubjectID = subjectID,
                                SubjectName = p.Subject.SubjectName,
                                TempScore = p.Score,
                            }).First();
                    if (mark.TempScore != null)
                    {
                        mark.Score = JsonConvert.DeserializeObject<Score>(mark.TempScore);
                        mark.TempScore = null;
                    }
                }
                markList.Add(mark);
            }
            return Json(markList, JsonRequestBehavior.AllowGet);
        }
    }
}