using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VinhEdu.Models
{
    public class BaseClassList
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BaseClassList()
        {
        }

        public int ID { get; set; }

        [Required]
        [StringLength(100)]
        public string ClassName { get; set; }
    }
}