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
        public enum LearnStatus
        {
            [Display(Name = "Đã học xong")]
            Finished = 0,
            [Display(Name = "Đang học")]
            Learning = 1,
            [Display(Name = "Đã chuyển lớp")]
            Switched = 2,
            [Display(Name = "Học lại")]
            Duplicated = 3,
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
            [Display(Name = "Không hoạt động")]
            NotActivated = 0,
            [Display(Name ="Đang hoạt động")]
            Activated = 1,
            [Display(Name = "Đã xóa")]
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