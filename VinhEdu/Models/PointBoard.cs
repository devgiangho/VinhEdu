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

        [StringLength(500)]
        public string ScoreX1 { get; set; }

        [StringLength(500)]
        public string ScoreX2 { get; set; }

        [StringLength(200)]
        public string ScoreX3 { get; set; }

        public int Semester { get; set; }

        public virtual Class Class { get; set; }

        public virtual User User { get; set; }
    }
}
