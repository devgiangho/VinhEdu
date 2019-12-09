using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace VinhEdu.Models
{
    public class Contact
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public int StudentID { get; set; }
        [Required]
        public int TeacherID { get; set; }
        [StringLength(5000)]
        public string Message { get; set; }
        [ForeignKey("Class")]
        public int ClassID { get; set; }
        [ForeignKey("Configure")]
        public int ConfigureID { get; set; }
        public virtual Configure Configure { get; set; }
        public virtual Class Class { get; set; }
        [Required]
        public SendFrom SendFrom { get; set; }
        [Required]
        public DateTime SendTime { get; set; }
        public DateTime? ReadTime { get; set; }
    }
    public enum SendFrom
    {
        FromTeacher = 1,
        FromStudent = 2,
    }
}