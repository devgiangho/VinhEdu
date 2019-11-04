using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static VinhEdu.Models.AdditionalDefinition;

namespace VinhEdu.ViewModels
{
    public class SubjectList
    {
        public int SubjectID { get; set; }
        public String SubjectName { get; set; }
        public int TeacherID { get; set; } = -1;
        public bool? IsHomeTeacher { get; set; } = false;
        public Gender Gender { get; set; }
        public String FullName { get; set; }
    }
}