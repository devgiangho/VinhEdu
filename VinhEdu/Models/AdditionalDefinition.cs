using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VinhEdu.Models
{
    public class AdditionalDefinition
    {
        public enum Semmester
        {
            [Display(Name = "Học kỳ I")]
            Teacher = 1,
            [Display(Name = "Học kỳ II")]
            Student = 2,
        }
        public enum LoginType
        {
            [Display(Name = "Giáo Viên")]
            Teacher = 0,
            [Display(Name = "Học Sinh")]
            Student = 1,
        }
        public enum Gender
        {
            [Display(Name = "Nam")]
            Male = 0,
            [Display(Name = "Nữ")]
            Female = 1,
        }
        public enum UserStatus
        {
            NotActivated = 0,
            Activated = 1,
            Deleted = 2,

        }
        public enum UserType
        {
            Admin = 0,
            HeadMaster = 1,
            Teacher = 2,
            Student = 3,
        }
    }
}