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
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        // GET: Admin
        UnitOfWork db = new UnitOfWork();
        EduVinhContext Context = new EduVinhContext();
        public ActionResult Index()
        {
            int configID = (int)Session["ConfigID"];
            int UserID = (int)Session["UserID"];
            ViewBag.countSchool = db.SchoolRepository.GetAll().Count();
            ViewBag.countClass = db.ClassRepository.GetAll().Count();
            ViewBag.countStudent = db.UserRepository.AllUser()
                .Where(c => c.Type == UserType.Student)
                .Count();
            ViewBag.countTeacher = db.UserRepository.AllUser()
                .Where(c => c.Type == UserType.Teacher)
                .Count();
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
            List<Configure> configures = db.ConfigRepository.GetAll().OrderByDescending(e => e.IsActive).ToList();
            int SchoolId = lst.First().SchoolID;
            List<Class> classes = db.ClassRepository.GetAll().Where(e => e.SchoolID == SchoolId).ToList();
            ViewBag.Class = new SelectList(classes, "ClassID", "ClassName");
            ViewBag.Config = new SelectList(configures, "ID", "SchoolYear");
            ViewBag.SchoolList = new SelectList(lst, "SchoolID", "SchoolName");
            return View();
        }
        public ViewResult CreateTeacher()
        {
            TeacherViewModel model = new TeacherViewModel
            {
                Subject = db.SubjectRepository.GetAll().Select(m =>
                    new SelectListItem
                    {
                        Text = m.SubjectName,
                        Value = m.ID.ToString(),
                    }
                    ).ToList(),
            };
            List<School> lst = db.SchoolRepository.GetAll().ToList();
            List<Configure> configures = db.ConfigRepository.GetAll().ToList();
            ViewBag.Config = new SelectList(configures, "ID", "SchoolYear");
            ViewBag.SchoolList = new SelectList(lst, "SchoolID", "SchoolName");
            return View(model);
        }
        public ViewResult AllTeacher()
        {
            List<School> lst = db.SchoolRepository.GetAll().ToList();
            List<Configure> configures = db.ConfigRepository.GetAll().OrderByDescending(e => e.IsActive).ToList();
            int SchoolId = lst.First().SchoolID;
            List<Class> classes = new List<Class>();
            classes.Add(new Class
            {
                ClassID = 0,
                ClassName = "Tất cả"
            });
            classes.AddRange(db.ClassRepository.GetAll().Where(e => e.SchoolID == SchoolId).ToList());
            ViewBag.Class = new SelectList(classes, "ClassID", "ClassName");
            ViewBag.Config = new SelectList(configures, "ID", "SchoolYear");
            ViewBag.SchoolList = new SelectList(lst, "SchoolID", "SchoolName");
            return View();
        }
        public ViewResult TeacherClass()
        {
            List<School> lst = db.SchoolRepository.GetAll().ToList();
            List<Configure> configures = db.ConfigRepository.GetAll().OrderByDescending(e => e.IsActive).ToList();
            int SchoolId = lst.First().SchoolID;
            List<Class> classes = db.ClassRepository.GetAll().Where(e => e.SchoolID == SchoolId).ToList();
            //List<SubjectList> subjects = db.SubjectRepository.GetAll()
            //    .Select(c => new SubjectList
            //    { 
            //        SubjectID = c.ID,
            //        SubjectName = c.SubjectName,
            //    }).ToList();
            ViewBag.Class = new SelectList(classes, "ClassID", "ClassName");
            ViewBag.Config = new SelectList(configures, "ID", "SchoolYear");
            ViewBag.SchoolList = new SelectList(lst, "SchoolID", "SchoolName");
            //ViewBag.Subjects = subjects;
            return View();
        }
        [HttpGet]
        public ViewResult Setting()
        {
            List<Configure> configures = db.ConfigRepository.GetAll().OrderByDescending(e => e.IsActive).ToList();
            ViewBag.Config = new SelectList(configures, "ID", "SchoolYear");
            Setting setting = Context.Settings.FirstOrDefault();
            ViewBag.countFailStudent = db.UserRepository.AllUser()
                .Where(c => c.canGradeUp == false && c.Type == UserType.Student && c.isFinished == false)
                .Count();
            return View(setting);
        }
        [HttpPost]
        public JsonResult Setting(string org,Semester semester, int ConfigID)
        {
            
            Setting setting = Context.Settings.FirstOrDefault();
            setting.OrganizationName = org;
            setting.Semester = semester;
            List<Configure> configure = Context.Configures.ToList();
            configure.ForEach((e) =>
            {
                if (e.IsActive)
                {
                    e.IsActive = false;
                }
                if (e.ID == ConfigID)
                {
                    e.IsActive = true;
                }
            });
            Context.SaveChanges();
            return Json("Cập nhật thành công");
        }
        /// <summary>
        /// Sang năm học mới
        /// </summary>
        /// <returns></returns>
        public JsonResult NextYear()
        {
            Configure configure = db.ConfigRepository.GetAll().Where(z => z.IsActive == true).FirstOrDefault();
            configure.IsActive = false;
            int currentConfigID = configure.ID;
            ///
            Setting setting = Context.Settings.FirstOrDefault();
            setting.Semester = Semester.HK1;
            
            Configure nextConfigure = db.ConfigRepository.FindByID(currentConfigID+1);
            nextConfigure.IsActive = true;
            //Cho học sinh lên lớp hoặc ở lại
            List<User> lstStudent = db.UserRepository.AllUser()
                .Where(c => c.Type == UserType.Student && c.isFinished != true)
                .ToList();
            string classType = "A";
            bool existNext = false; 
            try
            {
                foreach (var item in lstStudent)
                {
                    ClassMember member = db.MemberRepository.GetAll()
                        .Where(c => c.ConfigureID == currentConfigID && c.UserID == item.ID &&
                            (c.LearnStatus == LearnStatus.Learning || c.LearnStatus == LearnStatus.Duplicated))
                        .FirstOrDefault();

                    if (item.canGradeUp)
                    {
                        switch (member.Class.Grade)
                        {
                            case Grade.G9:
                                member.LearnStatus = LearnStatus.Finished;
                                item.isFinished = true;
                                break;
                            case Grade.G8:
                                member.LearnStatus = LearnStatus.Finished;
                                classType = member.Class.ClassName.Substring(1, 1);
                                existNext = db.context.Classes.Any(c => c.ClassName.Contains(classType) && c.Grade == Grade.G9);
                                if (existNext)
                                {
                                    int nextID = db.context.Classes.Where(c => c.ClassName.Contains(classType) && c.Grade == Grade.G9).First().ClassID;
                                    ClassMember newMem = new ClassMember
                                    {
                                        ClassID = nextID,
                                        ConfigureID = nextConfigure.ID,
                                        LearnStatus = LearnStatus.Learning,
                                        UserID = item.ID,
                                    };
                                    db.context.ClassMembers.Add(newMem);
                                }
                                else
                                {
                                    int nextID = db.context.Classes.Where(c => c.Grade == Grade.G9).First().ClassID;
                                    ClassMember newMem = new ClassMember
                                    {
                                        ClassID = nextID,
                                        ConfigureID = nextConfigure.ID,
                                        LearnStatus = LearnStatus.Learning,
                                        UserID = item.ID,
                                    };
                                    db.context.ClassMembers.Add(newMem);
                                }
                                //GradeUp(item.ID, Grade.G8, member.Class.ClassName,configure.ID, db.context);
                                break;
                            case Grade.G7:
                                member.LearnStatus = LearnStatus.Finished;
                                classType = member.Class.ClassName.Substring(1, 1);
                                existNext = db.context.Classes.Any(c => c.ClassName.Contains(classType) && c.Grade == Grade.G8);
                                if (existNext)
                                {
                                    int nextID = db.context.Classes.Where(c => c.ClassName.Contains(classType) && c.Grade == Grade.G8).First().ClassID;
                                    ClassMember newMem = new ClassMember
                                    {
                                        ClassID = nextID,
                                        ConfigureID = nextConfigure.ID,
                                        LearnStatus = LearnStatus.Learning,
                                        UserID = item.ID,
                                    };
                                    db.context.ClassMembers.Add(newMem);
                                }
                                else
                                {
                                    int nextID = db.context.Classes.Where(c => c.Grade == Grade.G8).First().ClassID;
                                    ClassMember newMem = new ClassMember
                                    {
                                        ClassID = nextID,
                                        ConfigureID = nextConfigure.ID,
                                        LearnStatus = LearnStatus.Learning,
                                        UserID = item.ID,
                                    };
                                    db.context.ClassMembers.Add(newMem);
                                }
                                //GradeUp(item.ID, Grade.G7, member.Class.ClassName, configure.ID, db.context);
                                break;
                            case Grade.G6:
                                member.LearnStatus = LearnStatus.Finished;
                                classType = member.Class.ClassName.Substring(1, 1);
                                existNext = db.context.Classes.Any(c => c.ClassName.Contains(classType) && c.Grade == Grade.G7);
                                if (existNext)
                                {
                                    int nextID = db.context.Classes.Where(c => c.ClassName.Contains(classType) && c.Grade == Grade.G7).First().ClassID;
                                    ClassMember newMem = new ClassMember
                                    {
                                        ClassID = nextID,
                                        ConfigureID = nextConfigure.ID,
                                        LearnStatus = LearnStatus.Learning,
                                        UserID = item.ID,
                                    };
                                    db.context.ClassMembers.Add(newMem);
                                }
                                else
                                {
                                    int nextID = db.context.Classes.Where(c => c.Grade == Grade.G7).First().ClassID;
                                    ClassMember newMem = new ClassMember
                                    {
                                        ClassID = nextID,
                                        ConfigureID = nextConfigure.ID,
                                        LearnStatus = LearnStatus.Learning,
                                        UserID = item.ID,
                                    };
                                    db.context.ClassMembers.Add(newMem);
                                }
                                //GradeUp(item.ID, Grade.G6, member.Class.ClassName, configure.ID, db.context);
                                break;
                        }
                    }
                    else
                    {
                        //Không đủ điểm thì cho học lại.
                        member.LearnStatus = LearnStatus.Finished;
                        ClassMember newMember = new ClassMember
                        {
                            ClassID = member.ClassID,
                            ConfigureID = nextConfigure.ID,
                            UserID = item.ID,
                            LearnStatus = LearnStatus.Duplicated,
                        };
                        db.MemberRepository.Add(newMember);
                    }

                    db.SaveChanges();
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
           
            return Json("CHÚC MỪNG NĂM HỌC MỚI");
        }
        /// <summary>
        /// Lên lớp, thêm trạng thái lớp mới
        /// </summary>
        /// <param name="studentID"></param>
        /// <param name="grade"></param>
        /// <param name="className"></param>
        /// <param name="configID"></param>
        /// <param name="context"></param>
        public void GradeUp(int studentID, Grade grade,string className,int configID, EduVinhContext context)
        {
            EduVinhContext _context = context;
            string classType = className.Substring(1, 1);
            bool existNext = _context.Classes.Any(c => c.ClassName.Contains(classType) && c.Grade == grade);
            if (existNext)
            {
                int nextID = _context.Classes.Where(c => c.ClassName.Contains(classType) && c.Grade == grade).First().ClassID;
                ClassMember newMem = new ClassMember
                {
                    ClassID = nextID,
                    ConfigureID = configID,
                    LearnStatus = LearnStatus.Learning,
                    UserID = studentID,
                };
                _context.ClassMembers.Add(newMem);
            }
            else
            {
                int nextID = context.Classes.Where(c=> c.Grade == grade).First().ClassID;
                ClassMember newMem = new ClassMember
                {
                    ClassID = nextID,
                    ConfigureID = configID,
                    LearnStatus = LearnStatus.Learning,
                    UserID = studentID,
                };
                _context.ClassMembers.Add(newMem);
            }
            _context.SaveChanges();
        }
    }

}