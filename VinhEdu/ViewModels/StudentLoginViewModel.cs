using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using static VinhEdu.Models.AdditionalDefinition;

namespace VinhEdu.ViewModels
{
    public class StudentLoginViewModel
    {
        public LoginType LoginType { get; set; } = LoginType.Student;
        public string Identify { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
    public class TeacherLoginViewModel
    {
        public LoginType LoginType { get; set; } = LoginType.Teacher;

        [DataType(DataType.EmailAddress)]
        public string Identify { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}