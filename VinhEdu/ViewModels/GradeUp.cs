using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VinhEdu.ViewModels
{
    public class GradeUp
    {
        public int studentID { get; set; }
        public string studentName { get; set; }
        public double HK1 { get; set; }
        public double HK2 { get; set; }
        public bool ableToGradeUp {get;set;}
    }
}