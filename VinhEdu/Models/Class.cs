using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace VinhEdu.Models
{
    public class Class
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Class()
        {

        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ClassID { get; set; }

        [Required]
        [StringLength(20)]
        public string ClassName { get; set; }

        [Required]
        [ForeignKey("School")]
        public int SchoolID { get; set; }
        public virtual  School School { get; set; }

        [Required]
        [ForeignKey("HomeRoomTeacher")]
        public int HomeRoomTeacherID { get; set; }
        public virtual User HomeRoomTeacher { get; set; }
    }
}