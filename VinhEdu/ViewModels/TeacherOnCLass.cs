using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using static VinhEdu.Models.AdditionalDefinition;

namespace VinhEdu.ViewModels
{
    public class TeacherOnCLass
    {
        public int TeacherID { get; set; }
        public int? SubjectID { get; set; }
        public string FullName { get; set; }
        public Gender Gender { get; set; }
        public string SubjectName { get; set; }
        public bool? IsHomeTeacher { get; set; }
    }
}