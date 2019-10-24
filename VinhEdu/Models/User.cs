namespace VinhEdu.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using static VinhEdu.Models.AdditionalDefinition;

    [Table("User")]
    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            Attendances = new HashSet<Attendance>();
            ClassMembers = new HashSet<ClassMember>();
            PointBoards = new HashSet<PointBoard>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string FullName { get; set; }

        [StringLength(100)]
        public string Identifier { get; set; }

        public int? SubjectID { get; set; }
        public int? SchoolID { get; set; }

        [Required]
        [StringLength(15)]
        public string Role { get; set; }
        [Required]
        [StringLength(500)]
        public string Password { get; set; }

        [Required]
        public UserType Type { get; set; }

        [Required]
        public Gender Gender { get; set; }

        [Required]
        public UserStatus Status { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        public DateTime CreateDate { get; set; } = DateTime.Now;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Attendance> Attendances { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ClassMember> ClassMembers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PointBoard> PointBoards { get; set; }

        public virtual Subject Subject { get; set; }
        public virtual School School { get; set; }
    }
}
