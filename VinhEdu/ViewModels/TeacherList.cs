using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using static VinhEdu.Models.AdditionalDefinition;

namespace VinhEdu.ViewModels
{
    public class TeacherList
    {
        public int ID { get; set; }
        [MinLength(5)]
        [MaxLength(30)]
        [Required]
        public string Identifier { get; set; }
        [Required]
        public int ClassID { get; set; }
        [MinLength(5)]
        [MaxLength(30)]
        [Required]
        public string FullName { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; } = DateTime.Now;

        public UserStatus Status { get; set; }
        [Required]
        public Gender Gender { get; set; }
        //[Required]
        //public string Password { get; set; }
        public string SubjectName { get; set; }
        public bool? IsHomeTeacher { get; set; }
    }
}