using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static VinhEdu.Models.AdditionalDefinition;

namespace VinhEdu.Models
{
    public class Setting
    {
        public int ID { get; set; } = 1;
        [Required]
        [StringLength(100)]
        [MinLength(10)]
        public string OrganizationName { get; set; }
        [Required]
        public Semester Semester { get; set; }

    }
}