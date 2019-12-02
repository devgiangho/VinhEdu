using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VinhEdu.Models;
using VinhEdu.Repository;
using VinhEdu.ViewModels;
using Newtonsoft.Json;

namespace VinhEdu.Controllers
{
    [Authorize(Roles = "teacher")]
    public class TeacherController : Controller
    {
        UnitOfWork db = new UnitOfWork();
        //EduVinhContext Context = new EduVinhContext();
        public ActionResult Index()
        {
            return View();
        }
        public ViewResult ClassList()
        {
            Configure configure = db.ConfigRepository.GetAll()
                .Where(e => e.IsActive).First();
            int userid = (int)Session["UserID"];
            List<ClassList> classLists = (from m in db.context.ClassMembers
                                          join c in db.context.Classes on m.ClassID equals c.ClassID
                                          join u in db.context.Users on m.UserID equals u.ID
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
        /// <summary>
        /// Quản lý điểm
        /// </summary>
        /// <param name="ClassID"></param>
        /// <returns></returns>
        public ActionResult PointBoard(int ClassID)
        {
            try
            {
                var className = db.ClassRepository.FindByID(ClassID).ClassName;
                var CurrentConfig = (int)Session["ConfigID"];
                var UserID = (int)Session["UserID"];
                bool Ismember = db.MemberRepository.GetAll()
                .Any(z => z.ClassID == ClassID && z.UserID == UserID);
                if(Ismember)
                {
                    ViewBag.Title = "Quản lý điểm";
                    ViewBag.ClassID = ClassID;
                    ViewBag.ClassName = className;
                    return View();
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception e)
            {
                return RedirectToAction("ClassList");
            }
            
        }
        /// <summary>
        /// Lấy điểm của học sinh trong lớp (năm học hiện tại)
        /// </summary>
        /// <param name="ClassID"></param>
        /// <returns></returns>
        public JsonResult GetStudentMark(int ClassID)
        {
            var CurrentConfig = (int)Session["ConfigID"];
            var SubjectID = (int)Session["SubjectID"];
            List<EditMark> member = (from m in db.context.ClassMembers
                                     join p in db.context.PointBoards on m.UserID equals p.StudentID into pm
                                     from p in pm.DefaultIfEmpty()
                                     where m.User.Status == AdditionalDefinition.UserStatus.Activated
                                     where m.User.Type == AdditionalDefinition.UserType.Student
                                     where m.ClassID == ClassID
                                     where p.SubjectID == SubjectID
                                     select new EditMark {
                                         SubjectID = SubjectID,
                                         StudentID = m.UserID,
                                         StudentName = m.User.FullName,
                                         TempScore = p.Score,
                                     }).ToList();
            if(member.Count == 0)
            {
                member = (from m in db.context.ClassMembers
                          join p in db.context.PointBoards on m.UserID equals p.StudentID into pm
                          from p in pm.DefaultIfEmpty()
                          where m.User.Status == AdditionalDefinition.UserStatus.Activated
                          where m.User.Type == AdditionalDefinition.UserType.Student
                          where m.ClassID == ClassID
                          select new EditMark
                          {
                              SubjectID = SubjectID,
                              StudentID = m.UserID,
                              StudentName = m.User.FullName,
                              TempScore = null,
                          }).ToList();
            }
            foreach (var item in member)
            {
                if (item.TempScore != null)
                {
                    item.Score = JsonConvert.DeserializeObject<Score>(item.TempScore);
                }
            }
            return Json(member, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Cập nhật bảng điểm
        /// </summary>
        /// <param name="ClassID"></param>
        /// <param name="Listmark"></param>
        /// <returns></returns>
        public JsonResult UpdateStudentMark(int ClassID, IEnumerable<EditMark> Listmark)
        {
            if(ModelState.IsValid)
            {
                var CurrentConfig = (int)Session["ConfigID"];
                var SubjectID = (int)Session["SubjectID"];
                var CurrentSemester = db.context.Settings.FirstOrDefault().Semester;
                foreach (var mark in Listmark)
                {
                    bool HasMark = db.context.PointBoards
                        .Any(e => e.ClassID == ClassID &&
                        e.StudentID == mark.StudentID && e.SubjectID == SubjectID);
                    if (HasMark)
                    {
                        PointBoard CurrentMark = db.context.PointBoards
                        .Where(e => e.ClassID == ClassID &&
                        e.StudentID == mark.StudentID && e.SubjectID == SubjectID &&  e.ConfigureID == CurrentConfig).First();
                        var dumpData = JsonConvert.SerializeObject(mark.Score);
                        CurrentMark.Score = dumpData;
                        db.context.SaveChanges();
                    }
                    else
                    {
                        PointBoard NewMark = new PointBoard
                        {
                            ClassID = ClassID,
                            Score = JsonConvert.SerializeObject(mark.Score), // mark.Score,
                            StudentID = mark.StudentID,
                            SubjectID = mark.SubjectID,
                            ConfigureID = CurrentConfig,
                            Semester = CurrentSemester == AdditionalDefinition.Semester.HK1 ? AdditionalDefinition.Semester.HK1 : AdditionalDefinition.Semester.HK2
                        };
                        db.context.PointBoards.Add(NewMark);
                        db.context.SaveChanges();
                    }

                }
                List<EditMark> member = (from m in db.context.ClassMembers
                                         join p in db.context.PointBoards on m.UserID equals p.StudentID into pm
                                         from p in pm.DefaultIfEmpty()
                                         where m.User.Status == AdditionalDefinition.UserStatus.Activated
                                         where m.User.Type == AdditionalDefinition.UserType.Student
                                         where m.ClassID == ClassID
                                         where p.SubjectID == SubjectID
                                         select new EditMark
                                         {
                                             SubjectID = SubjectID,
                                             StudentID = m.UserID,
                                             StudentName = m.User.FullName,
                                             TempScore = p.Score,
                                             //Score = JsonConvert.DeserializeObject<Score>(p.Score),
                                         }).ToList();
                List<EditMark> Result = new List<EditMark>();
                foreach (var item in member)
                {
                    if (item.TempScore != null)
                    {
                        item.Score = JsonConvert.DeserializeObject<Score>(item.TempScore);
                    }
                }
                return Json(new { Message = "Cập nhật thành công", Member = member, Success = true }, JsonRequestBehavior.AllowGet);

            }
            return Json(new { Message = "Bạn nhập điểm không hợp lệ \n Điểm từ x (chưa có) và 0 đến 10", Success = false }, JsonRequestBehavior.AllowGet);
        }
    }
}