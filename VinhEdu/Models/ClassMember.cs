namespace VinhEdu.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using static VinhEdu.Models.AdditionalDefinition;

    [Table("ClassMember")]
    public partial class ClassMember
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ClassID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UserID { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ConfigureID { get; internal set; }

        //public bool IsCurrent { get; set; }

        public bool? IsHomeTeacher { get; set; }
        public LearnStatus LearnStatus { get; set; }

        public virtual Class Class { get; set; }

        public virtual User User { get; set; }
        public virtual Configure Configure { get; set; }
    }
}
