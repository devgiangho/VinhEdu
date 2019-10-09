using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace VinhEdu.Models
{
    public class PointTable
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PointTable()
        {

        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        public int StudentID { get; set; }

        [Required]
        public int TeacherID { get; set; }

        [StringLength(500)]
        public string Up1Score { get; set; }

        [StringLength(500)]
        public string Up2Score { get; set; }

        [StringLength(100)]

        public string Up3Score { get; set; }
        [Required]
        [ForeignKey("School")]
        public int SchoolID { get; set; }
        public virtual School School { get; set; }

        [Required]
        [ForeignKey("Class")]
        public int ClassID { get; set; }
        public virtual Class Class { get; set; }

        [Required]
        [ForeignKey("Subject")]
        public int SubjectID { get; set; }
        public virtual Subject Subject { get; set; }
    }
}