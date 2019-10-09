using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using static VinhEdu.Models.AdditionalDefinition;

namespace VinhEdu.Models
{
    public class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        public UserStatus status { get; set; }

        [Required]
        [StringLength(500)]
        public string password { get; set; }

        public Gender gender { get; set; }

        [Required]
        [StringLength(20)]
        public string StudentID { get; set; }

        [StringLength(100)]
        public string FullName { get; set; }

        [StringLength(100)]
        public string email { get; set; }
        [Required]
        public UserType type { get; set; }

        [Required]
        [StringLength(15)]
        public string role { get; set; }

        [StringLength(200)]
        public string AvatarImage { get; set; }

        [ForeignKey("Subject")]
        public int SubjectID { get; set; }
        public virtual Subject Subject { get; set; }

        [Required]
        [ForeignKey("School")]
        public int SchoolID { get; set; }
        public virtual School School { get; set; }

        [Required]
        [ForeignKey("ClassStudent")]
        public int ClassStudentID { get; set; }
        public virtual Class ClassStudent { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<Class> ClassTeachers { get; set; }
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public ICollection<QuizTest> QuizTests { get; set; }



    }
}