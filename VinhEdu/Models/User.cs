namespace VinhEdu.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

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

        [StringLength(20)]
        public string StudentID { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        public int? SubjectID { get; set; }

        [Required]
        [StringLength(15)]
        public string Role { get; set; }

        public int Type { get; set; }

        public int Gender { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Attendance> Attendances { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ClassMember> ClassMembers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PointBoard> PointBoards { get; set; }

        public virtual Subject Subject { get; set; }
    }
}
