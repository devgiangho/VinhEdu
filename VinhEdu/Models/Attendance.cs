namespace VinhEdu.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Attendance")]
    public partial class Attendance
    {
        public int ID { get; set; }

        public int StudentID { get; set; }

        public int ClassID { get; set; }

        public int? Status { get; set; }

        public DateTime AttendanceDate { get; set; }

        public virtual Class Class { get; set; }

        public virtual User User { get; set; }
    }
}
