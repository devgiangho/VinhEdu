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
        public CheckMark CaculateScore(List<int> studentList, int ClassID)
        {
            int configID = (int)Session["ConfigID"];
            List<int> subjectList = db.SubjectRepository.GetAll().Select(c => c.ID).ToList();
            List<MarkStudent> studentMarks = new List<MarkStudent>();
            foreach (var studentID in studentList)
            {
                List<SubjectScore> lstSubjectScores = new List<SubjectScore>();
                foreach (var subjectID in subjectList)
                {
                    SubjectScore mark = new SubjectScore();
                    bool HasMark = db.context.PointBoards
                        .Any(e => e.ClassID == ClassID && e.Semester == Semester.HK1 &&
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
                                where p.Semester == Semester.HK1
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
                            mark.finalScore = CaculateFinalSubjectScore(mark.Score);
                        }
                        else
                        {
                            mark.finalScore = "x";
                        }
                    }
                    else
                    {
                        return new CheckMark
                        {
                            isFinished = false,
                            markStudents = null,
                        };
                    }
                    lstSubjectScores.Add(mark);
                }
                studentMarks.Add(new MarkStudent
                {
                    StudentID = studentID,
                    SubjectScores = lstSubjectScores,
                });
            }
            return new CheckMark {
                isFinished = true,
                markStudents = studentMarks,
            };
        }
        public string CaculateFinalSubjectScore(Score score)
        {
            var checkM = score.M1 != "x" || score.M2 != "x" || score.M3 != "x" || score.M4 != "x";
            var checkP = score.P1 != "x" && score.P2 != "x" && score.P3 != "x";
            var checkT = score.T1 != "x" && score.T2 != "x" && score.T3 != "x";
            var checkHK = score.K1 != "x";

            if (checkHK && checkM && checkP && checkT)
            {
                string[] x1Score = { score.M1, score.M2, score.M3, score.M4, score.P1, score.P2, score.P3 };
                string[] x2Score = {score.T1, score.T2, score.T3} ;
                var scoreCount = 0;
                double x1Count = 0;
                double x2Count = 0;
                foreach(string x1 in x1Score)
                {
                    if(x1 != "x")
                    {
                        scoreCount++;
                        x1Count += Convert.ToDouble(x1);
                    }
                }
                foreach (string x2 in x2Score)
                {
                    if (x2 != "x")
                    {
                        scoreCount++;
                        x2Count += Convert.ToDouble(x2);
                    }
                }
                //let countX1 = x1Score.reduce(reducer, 0);
                //let countX2 = x2Score.reduce(reducer2, 0);
                double finalX1andX2 = x1Count + x2Count + (Convert.ToDouble(score.K1) * 3);
                scoreCount += 3; // hệ số điểm học kì
                return (finalX1andX2 / scoreCount).ToString("0.##");
            }
            return "x";
        }
    }
}