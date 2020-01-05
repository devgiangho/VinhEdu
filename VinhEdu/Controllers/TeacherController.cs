using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VinhEdu.Models;
using VinhEdu.Repository;
using VinhEdu.ViewModels;
using Newtonsoft.Json;
using static VinhEdu.Models.AdditionalDefinition;
using System.Data.Entity;

namespace VinhEdu.Controllers
{
    [Authorize(Roles = "teacher,admin,headmaster")]
    public class TeacherController : Controller
    {
        UnitOfWork db = new UnitOfWork();
        //EduVinhContext Context = new EduVinhContext();
        public ActionResult Index()
        {
            int configID = (int)Session["ConfigID"];
            int UserID = (int)Session["UserID"];
            bool isHomeTeacher = db.MemberRepository
                .GetAll().Any(c => c.UserID == UserID && c.IsHomeTeacher == true && c.ConfigureID == configID);
            ViewBag.isHomeTeacher = isHomeTeacher;
            if (isHomeTeacher)
            {
                var HomeClass = db.MemberRepository
                .GetAll().Where(c => c.UserID == UserID && c.IsHomeTeacher == true)
                .Select(c => new ShowHomeClass
                {
                    ClassName = c.Class.ClassName,
                    CountStudent = c.Class.ClassMembers.Count(b => b.LearnStatus == LearnStatus.Learning),
                    ClassID = c.ClassID
                }).First();
                ViewBag.HomeClass = HomeClass;
            }
            ViewBag.teachingClass = db.MemberRepository.GetAll()
                .Where(c => c.ConfigureID == configID && c.UserID == UserID)
                .Count();
            ViewBag.SubjectName = db.SubjectRepository.FindByID((int)Session["SubjectID"]).SubjectName;
            return View();
        }
        /// <summary>
        /// Lớp mình chủ nhiệm
        /// </summary>
        /// <returns></returns>
        public ActionResult MyHomeClass()
        {
            int configID = (int)Session["ConfigID"];
            int UserID = (int)Session["UserID"];
            bool isHomeTeacher = db.MemberRepository
               .GetAll().Any(c => c.UserID == UserID && c.IsHomeTeacher == true && c.ConfigureID == configID);
            if(isHomeTeacher)
            {
                int ClassID = db.MemberRepository
               .GetAll().Where(c => c.UserID == UserID && c.IsHomeTeacher == true && c.ConfigureID == configID)
               .First().ClassID;
                ViewBag.ClassID = ClassID;
                List<ViewModels.StudentList> lstStudent = db.MemberRepository.
                    GetAll().Where(c => c.ClassID == ClassID && c.ConfigureID == configID
                    && c.User.Type == UserType.Student && c.LearnStatus == LearnStatus.Learning && c.User.Status == UserStatus.Activated)
                    .Select(c => new StudentList
                    {
                        ID = c.UserID,
                        DateOfBirth = c.User.DateOfBirth,
                        FullName = c.User.FullName,
                        Gender = c.User.Gender
                    }).ToList();
                return View(lstStudent);
            }
            return RedirectToAction("Index");
        }
        public ViewResult Message()
        {
            return View();
        }
        /// <summary>
        /// Xem điểm lớp mình chủ nhiệm theo học kì
        /// </summary>
        /// <param name="semester"></param>
        /// <returns></returns>
        public JsonResult GetHomeClassScoreBoard(int semester)
        {
            try
            {
                int configID = (int)Session["ConfigID"];
                int UserID = (int)Session["UserID"];
                var CurrentSemester = semester == 1 ? Semester.HK1 : Semester.HK2;
                int ClassID = db.MemberRepository
                   .GetAll().Where(c => c.UserID == UserID && c.IsHomeTeacher == true && c.ConfigureID == configID)
                   .First().ClassID;
                List<int> lstStudent = db.MemberRepository.GetAll()
                    .Where(c => c.LearnStatus  != LearnStatus.Finished
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
            catch(Exception e)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }
        /// <summary>
        /// Lấy danh sách tất cả các môn
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "teacher,admin,headmaster")]
        public JsonResult GetSubjectInstance()
        {
            var subjectList = db.SubjectRepository
                .GetAll().Select(c => new
                {
                    SubjectID = c.ID,
                    SubjectName = c.SubjectName,
                }).ToList();
            return Json(subjectList, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Danh sách lớp dạy
        /// </summary>
        /// <returns></returns>
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
                    ViewBag.Semester = db.context.Settings.FirstOrDefault().Semester.GetDisplayName();
                    ViewBag.SubjectName = db.UserRepository.FindByID(UserID).Subject.SubjectName;
                    return View();
                }
                else
                {
                    throw new Exception();
                }
            }
            catch
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
            var CurrentSemester = db.context.Settings.FirstOrDefault().Semester;
            List<EditMark> editMarks = new List<EditMark>();
            List<int> lstStudent = db.MemberRepository.GetAll()
                   .Where(c => c.LearnStatus != LearnStatus.Finished
                   && c.ClassID == ClassID && CurrentConfig == c.ConfigureID && c.User.Status == UserStatus.Activated)
                   .Select(c => c.UserID).ToList();
            foreach (int studentID in lstStudent)
            {
                bool HasMark = db.context.PointBoards
                        .Any(e => e.ClassID == ClassID &&
                        e.StudentID == studentID && e.SubjectID == SubjectID && e.Semester == CurrentSemester);
                if (!HasMark)
                {
                    Score emptyScore = new Score();
                    PointBoard NewMark = new PointBoard
                    {
                        ClassID = ClassID,
                        Score = JsonConvert.SerializeObject(emptyScore),
                        StudentID = studentID,
                        SubjectID = SubjectID,
                        ConfigureID = CurrentConfig,
                        Semester = CurrentSemester
                    };
                    db.context.PointBoards.Add(NewMark);
                    db.SaveChanges();
                }
                EditMark editItem = db.context.PointBoards
                .Where(c => c.ClassID == ClassID && c.ConfigureID == c.ConfigureID
                && c.Semester == CurrentSemester && c.SubjectID == SubjectID && c.StudentID == studentID)
                .Select(c => new EditMark
                {
                    SubjectID = c.SubjectID,
                    StudentName = c.User.FullName,
                    TempScore = c.Score,
                    StudentID = c.StudentID
                }).FirstOrDefault();
                if (editItem.TempScore != null)
                {
                    editItem.Score = JsonConvert.DeserializeObject<Score>(editItem.TempScore);
                    editItem.TempScore = null;
                }
                editMarks.Add(editItem);
            }
            return Json(editMarks, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetStudentMarkByName(int ClassID,string name)
        {
            var CurrentConfig = (int)Session["ConfigID"];
            var SubjectID = (int)Session["SubjectID"];
            var CurrentSemester = db.context.Settings.FirstOrDefault().Semester;
            List<EditMark> editMarks = new List<EditMark>();
            List<int> lstStudent = new List<int>();
            if (String.IsNullOrEmpty(name))
            {
               lstStudent = db.MemberRepository.GetAll()
                   .Where(c => c.LearnStatus != LearnStatus.Finished
                   && c.ClassID == ClassID && CurrentConfig == c.ConfigureID && c.User.Status == UserStatus.Activated)
                   .Select(c => c.UserID).ToList();
            }
            else
            {
                lstStudent = db.MemberRepository.GetAll()
                   .Where(c => c.LearnStatus != LearnStatus.Finished && c.User.FullName.Contains(name)
                   && c.ClassID == ClassID && CurrentConfig == c.ConfigureID && c.User.Status == UserStatus.Activated)
                   .Select(c => c.UserID).ToList();
            }
            foreach (int studentID in lstStudent)
            {
                bool HasMark = db.context.PointBoards
                        .Any(e => e.ClassID == ClassID &&
                        e.StudentID == studentID && e.SubjectID == SubjectID && e.Semester == CurrentSemester);
                if (!HasMark)
                {
                    Score emptyScore = new Score();
                    PointBoard NewMark = new PointBoard
                    {
                        ClassID = ClassID,
                        Score = JsonConvert.SerializeObject(emptyScore),
                        StudentID = studentID,
                        SubjectID = SubjectID,
                        ConfigureID = CurrentConfig,
                        Semester = CurrentSemester
                    };
                    db.context.PointBoards.Add(NewMark);
                    db.SaveChanges();
                }
                EditMark editItem = db.context.PointBoards
                .Where(c => c.ClassID == ClassID && c.ConfigureID == c.ConfigureID
                && c.Semester == CurrentSemester && c.SubjectID == SubjectID && c.StudentID == studentID)
                .Select(c => new EditMark
                {
                    SubjectID = c.SubjectID,
                    StudentName = c.User.FullName,
                    TempScore = c.Score,
                    StudentID = c.StudentID
                }).FirstOrDefault();
                if (editItem.TempScore != null)
                {
                    editItem.Score = JsonConvert.DeserializeObject<Score>(editItem.TempScore);
                    editItem.TempScore = null;
                }
                editMarks.Add(editItem);
            }
            return Json(editMarks, JsonRequestBehavior.AllowGet);
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
                        e.StudentID == mark.StudentID && e.SubjectID == SubjectID
                        && e.Semester == CurrentSemester);
                    if (HasMark)
                    {
                        PointBoard CurrentMark = db.context.PointBoards
                        .Where(e => e.ClassID == ClassID &&
                        e.StudentID == mark.StudentID && e.SubjectID == SubjectID &&  e.ConfigureID == CurrentConfig).First();
                        var dumpData = JsonConvert.SerializeObject(mark.Score);

                        CurrentMark.Score = dumpData;

                        db.context.Entry(CurrentMark).State = EntityState.Modified;
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
                            Semester = CurrentSemester == Semester.HK1 ? Semester.HK1 : Semester.HK2
                        };
                        db.context.PointBoards.Add(NewMark);
                        db.SaveChanges();
                    }

                }
                // Đổ lại dữ liệu
                List<int> lstStudent = db.MemberRepository.GetAll()
                   .Where(c => c.LearnStatus != LearnStatus.Finished
                   && c.ClassID == ClassID && CurrentConfig == c.ConfigureID)
                   .Select(c => c.UserID).ToList();
                List<EditMark> editMarks = new List<EditMark>();
                foreach (int studentID in lstStudent)
                {
                    EditMark editItem = db.context.PointBoards
                    .Where(c => c.ClassID == ClassID && c.ConfigureID == c.ConfigureID
                    && c.Semester == CurrentSemester && c.SubjectID == SubjectID && c.StudentID == studentID)
                    .Select(c => new EditMark
                    {
                        SubjectID = c.SubjectID,
                        StudentName = c.User.FullName,
                        TempScore = c.Score,
                        StudentID = c.StudentID
                    }).FirstOrDefault();
                    if (editItem.TempScore != null)
                    {
                        editItem.Score = JsonConvert.DeserializeObject<Score>(editItem.TempScore);
                        editItem.TempScore = null;
                    }
                    editMarks.Add(editItem);
                }
                return Json(new { Message = "Cập nhật thành công", Member = editMarks, Success = true }, JsonRequestBehavior.AllowGet);

            }
            return Json(new { Message = "Bạn nhập điểm không hợp lệ \n Điểm từ x (chưa có) và 0 đến 10", Success = false }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Gửi tin nhắn cho phụ huynh
        /// </summary>
        /// <param name="studentID"></param>
        /// <param name="classID"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public JsonResult SendContact(int studentID,int classID,string message)
        {
            var CurrentConfig = (int)Session["ConfigID"];
            var UserID = (int)Session["UserID"];
            try
            {
                Contact contact = new Contact
                {
                    ClassID = classID,
                    ConfigureID = CurrentConfig,
                    Message = message,
                    SendFrom = SendFrom.FromTeacher,
                    TeacherID = UserID,
                    SendTime = DateTime.Now,
                    StudentID = studentID,
                };
                db.context.Contacts.Add(contact);
                db.SaveChanges();
                return Json(new { success = true, message = "Đã gửi tin nhắn thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new {success = false, message = "Lỗi xảy ra, vui lòng thử lại sau" }, JsonRequestBehavior.AllowGet);
            }
            
        }
         
    }
}