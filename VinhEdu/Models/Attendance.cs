namespace VinhEdu.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using static VinhEdu.Models.AdditionalDefinition;

    [Table("Attendance")]
    public partial class Attendance
    {
        public int ID { get; set; }

        public int StudentID { get; set; }


        public int? Status { get; set; }
        public int ConfigureID { get; internal set; }
        public virtual Configure Configure { get; set; }

        public Semester Semmester { get; set; }

        public DateTime AttendanceDate { get; set; }


        public virtual User User { get; set; }
    }
}
