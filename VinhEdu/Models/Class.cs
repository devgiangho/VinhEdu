namespace VinhEdu.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Class")]
    public partial class Class
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Class()
        {
            Attendances = new HashSet<Attendance>();
            ClassMembers = new HashSet<ClassMember>();
            PointBoards = new HashSet<PointBoard>();
        }

        public int ClassID { get; set; }

        [Required]
        [StringLength(100)]
        public string ClassName { get; set; }

        public int SchoolID { get; set; }
        [Required]
        public Grade Grade { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Attendance> Attendances { get; set; }

        ////public virtual Configure Configure { get; set; }

        public virtual School School { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ClassMember> ClassMembers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PointBoard> PointBoards { get; set; }
    }
    public enum Grade
    {
        [Display(Name = "Khối 6")]
        G6 = 6,
        [Display(Name = "Khối 7")]
        G7 = 7,
        [Display(Name = "Khối 8")]
        G8 = 8,
        [Display(Name = "Khối 9")]
        G9 = 9,
    }
}
