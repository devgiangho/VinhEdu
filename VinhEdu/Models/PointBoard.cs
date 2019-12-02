namespace VinhEdu.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PointBoard")]
    public partial class PointBoard
    {
        public int ID { get; set; }

        public int StudentID { get; set; }

        public int ClassID { get; set; }

        [StringLength(1000)]
        public string Score { get; set; }

        public int Semester { get; set; }

        [ForeignKey("Subject")]
        public int SubjectID { get; set; }
        public virtual Subject Subject { get; set; }
        public virtual Class Class { get; set; }

        public virtual User User { get; set; }
    }
}
