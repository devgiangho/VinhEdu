using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VinhEdu.Repository;
using VinhEdu.ViewModels;
using VinhEdu.Models;
using static VinhEdu.Models.AdditionalDefinition;
using Newtonsoft.Json;

namespace VinhEdu.Controllers
{
    [Authorize(Roles = "headmaster")]
    public class HeadMasterController : Controller
    {

        UnitOfWork db = new UnitOfWork();
        // GET: HeadMaster
        [Authorize(Roles = "headmaster")]
        public ActionResult Index()
        {
            return RedirectToAction("ClassRoomList");
            return View();
        }
        /// <summary>
        /// Danh sách lớp
        /// </summary>
        /// <returns></returns>
        public ActionResult ClassRoomList()
        {
            int userID = (int)Session["UserID"];
            bool existSchool = db.SchoolRepository.GetAll()
                .Any(c => c.HeadMasterID == userID);
            if (!existSchool)
            {
                return RedirectToAction("ClassRoomList");
            }
            int masterSchoolID = db.SchoolRepository.GetAll()
                .Where(c => c.HeadMasterID == userID).Select(c => c.SchoolID).FirstOrDefault();
            List<ClassListHeadMaster> classLists = db.ClassRepository.GetAll()
                .Where(c => c.SchoolID == masterSchoolID)
                .Select(c => new ClassListHeadMaster
                {
                    ClassID = c.ClassID,
                    ClassName = c.ClassName,
                    StudentCount = c.ClassMembers
                    .Where(x => x.User.Type == UserType.Student &&
                    (x.LearnStatus == LearnStatus.Learning || x.LearnStatus == LearnStatus.Duplicated))
                    .Count(),
                    Grade = c.Grade,
                    HomeTeacher = c.ClassMembers
                    .Where(z => z.IsHomeTeacher == true).FirstOrDefault().User.FullName,
                })
                .ToList();
            return View(classLists);
        }
        public ActionResult GradeUpManage(int? ClassID)
        {
            int configID = (int)Session["ConfigID"];
            if (!ClassID.HasValue)
            {
                return RedirectToAction("ClassRoomList");
            }
            ViewBag.ClassID = ClassID.Value;
            ViewBag.ClassName = db.ClassRepository.FindByID(ClassID.Value).ClassName;
            ViewBag.Semester = db.context.Settings.FirstOrDefault().Semester.ToString();
            return View();
        }
        /// <summary>
        /// Lấy điểm theo học kì của 1 lớp nào đó
        /// </summary>
        /// <param name="semester"></param>
        /// <returns></returns>
        public JsonResult GetStudentClassMark(int classID,int semester)
        {
            try
            {
                int configID = (int)Session["ConfigID"];
                var CurrentSemester = semester == 1 ? Semester.HK1 : Semester.HK2;
                int ClassID = classID;
                List<int> lstStudent = db.MemberRepository.GetAll()
                    .Where(c => c.LearnStatus != LearnStatus.Finished
                    && c.ClassID == ClassID && configID == c.ConfigureID)
                    .Select(c => c.UserID).ToList();
                List<MarkStudent> markList = new List<MarkStudent>();
                List<int> subjectList = db.SubjectRepository.GetAll().Select(c => c.ID).ToList();
                if (lstStudent.Count == 0)
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
                foreach (var studentID in lstStudent)
                {

                    List<SubjectScore> lstSubjectScores = new List<SubjectScore>();
                    foreach (var subjectID in subjectList)
                    {
                        SubjectScore mark = new SubjectScore();
                        bool HasMark = db.context.PointBoards
                            .Any(e => e.ClassID == ClassID && e.Semester == CurrentSemester &&
                            e.StudentID == studentID && e.SubjectID == subjectID && e.ConfigureID == configID);
                        if (HasMark)
                        {
                            mark = (from m in db.context.ClassMembers
                                    join p in db.context.PointBoards on m.UserID equals p.StudentID into pm
                                    from p in pm.DefaultIfEmpty()
                                    where m.UserID == studentID
                                    where m.ClassID == ClassID
                                    where p.SubjectID == subjectID
                                    where p.ConfigureID == configID
                                    where p.Semester == CurrentSemester
                                    select new SubjectScore
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
                                StudentID = studentID,
                                SubjectID = subjectID,
                                ConfigureID = configID,
                                Semester = CurrentSemester
                            };
                            db.context.PointBoards.Add(NewMark);

                            db.context.SaveChanges();
                            mark = (from m in db.context.ClassMembers
                                    join p in db.context.PointBoards on m.UserID equals p.StudentID into pm
                                    from p in pm.DefaultIfEmpty()
                                    where m.UserID == studentID
                                    where m.ClassID == ClassID
                                    where p.SubjectID == subjectID
                                    where p.ConfigureID == configID
                                    select new SubjectScore
                                    {
                                        SubjectID = subjectID,
                                        SubjectName = p.Subject.SubjectName,
                                        TempScore = p.Score,
                                    }).First();
                            if (mark.TempScore != null)
                            {
                                mark.Score = JsonConvert.DeserializeObject<Score>(mark.TempScore);
                            }
                        }
                        lstSubjectScores.Add(mark);
                    }

                    string studentName = db.UserRepository.FindByID(studentID).FullName;
                    MarkStudent itemMark = new MarkStudent
                    {
                        StudentID = studentID,
                        StudentName = studentName,
                        SubjectScores = lstSubjectScores
                    };
                    markList.Add(itemMark);
                }

                return Json(markList, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetStudentClassMarkByName(int classID, int semester,string name)
        {
            try
            {
                int configID = (int)Session["ConfigID"];
                var CurrentSemester = semester == 1 ? Semester.HK1 : Semester.HK2;
                int ClassID = classID;
                List<int> lstStudent = new List<int>();
                if(String.IsNullOrWhiteSpace(name))
                {
                    lstStudent = db.MemberRepository.GetAll()
                    .Where(c => c.LearnStatus != LearnStatus.Finished
                    && c.ClassID == ClassID && configID == c.ConfigureID)
                    .Select(c => c.UserID).ToList();
                }
                else
                {
                    lstStudent = db.MemberRepository.GetAll()
                    .Where(c => c.LearnStatus != LearnStatus.Finished && c.User.FullName.Contains(name)
                    && c.ClassID == ClassID && configID == c.ConfigureID)
                    .Select(c => c.UserID).ToList();
                }
                List<MarkStudent> markList = new List<MarkStudent>();
                List<int> subjectList = db.SubjectRepository.GetAll().Select(c => c.ID).ToList();
                if (lstStudent.Count == 0)
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
                foreach (var studentID in lstStudent)
                {

                    List<SubjectScore> lstSubjectScores = new List<SubjectScore>();
                    foreach (var subjectID in subjectList)
                    {
                        SubjectScore mark = new SubjectScore();
                        bool HasMark = db.context.PointBoards
                            .Any(e => e.ClassID == ClassID && e.Semester == CurrentSemester &&
                            e.StudentID == studentID && e.SubjectID == subjectID && e.ConfigureID == configID);
                        if (HasMark)
                        {
                            mark = (from m in db.context.ClassMembers
                                    join p in db.context.PointBoards on m.UserID equals p.StudentID into pm
                                    from p in pm.DefaultIfEmpty()
                                    where m.UserID == studentID
                                    where m.ClassID == ClassID
                                    where p.SubjectID == subjectID
                                    where p.ConfigureID == configID
                                    where p.Semester == CurrentSemester
                                    select new SubjectScore
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
                                StudentID = studentID,
                                SubjectID = subjectID,
                                ConfigureID = configID,
                                Semester = CurrentSemester
                            };
                            db.context.PointBoards.Add(NewMark);

                            db.context.SaveChanges();
                            mark = (from m in db.context.ClassMembers
                                    join p in db.context.PointBoards on m.UserID equals p.StudentID into pm
                                    from p in pm.DefaultIfEmpty()
                                    where m.UserID == studentID
                                    where m.ClassID == ClassID
                                    where p.SubjectID == subjectID
                                    where p.ConfigureID == configID
                                    select new SubjectScore
                                    {
                                        SubjectID = subjectID,
                                        SubjectName = p.Subject.SubjectName,
                                        TempScore = p.Score,
                                    }).First();
                            if (mark.TempScore != null)
                            {
                                mark.Score = JsonConvert.DeserializeObject<Score>(mark.TempScore);
                            }
                        }
                        lstSubjectScores.Add(mark);
                    }

                    string studentName = db.UserRepository.FindByID(studentID).FullName;
                    MarkStudent itemMark = new MarkStudent
                    {
                        StudentID = studentID,
                        StudentName = studentName,
                        SubjectScores = lstSubjectScores
                    };
                    markList.Add(itemMark);
                }

                return Json(markList, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult UpdateGradeUpStatus(List<GradeUpViewModel> studentList)
        {
            int configID = (int)Session["ConfigID"];
            foreach(var item in studentList)
            {
                User u = db.UserRepository.FindByID(item.studentID);
                u.canGradeUp = item.canGradeUp;
            }
            db.SaveChanges();
            return Json("Cập nhật thành công");
        }

    }
}