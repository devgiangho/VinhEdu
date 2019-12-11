using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VinhEdu.Models;

namespace VinhEdu.ViewModels
{
    public class ClassListHeadMaster
    {
        public int ClassID { get; set; }
        public string ClassName { get; set; }
        public Grade Grade { get; set; }
        public int StudentCount { get; set; }
        public string HomeTeacher { get; set; }
    }
}