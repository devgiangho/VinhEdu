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

        //public int HomeRoomTeacherID { get; set; }

        //[StringLength(500)]
        //public string StudentList { get; set; }

        //[StringLength(500)]
        //public string TeacherList { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Attendance> Attendances { get; set; }

        ////public virtual Configure Configure { get; set; }

        public virtual School School { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ClassMember> ClassMembers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PointBoard> PointBoards { get; set; }
    }
}
