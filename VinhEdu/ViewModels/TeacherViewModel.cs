using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static VinhEdu.Models.AdditionalDefinition;

namespace VinhEdu.ViewModels
{
    public class TeacherViewModel
    {
        public int ID { get; set; }
        [MinLength(10)]
        [MaxLength(30)]
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Identifier { get; set; }

        [MinLength(5)]
        [MaxLength(30)]
        [Required]
        public string FullName { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }

        public UserStatus Status { get; set; }
        [Required]
        public Gender Gender { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public virtual string SubjectName { get; set; }
        public IEnumerable<SelectListItem> Subject { get; set; }
        [Required(ErrorMessage = "Chưa chọn môn học")]
        public int SubjectID { get; set; }
        [Required]
        public int ClassID { get; set; }
        [Required]
        public int ConfigureID { get; set; }
    }
}