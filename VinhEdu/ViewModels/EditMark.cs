using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VinhEdu.ViewModels
{
    public class ShowMark {
        public int StudentID { get; set; }
        public string StudentName { get; set; }
        public string SubjectName { get; set; }
        public int SubjectID { get; set; }
        public Score Score { get; set; }
        public string TempScore { get; set; }
    }
    public class EditMark
    {
        public int StudentID { get; set; }
        public string StudentName { get; set; }
        public string ClassName { get; set; }
        public int SubjectID { get; set; }

        //[StringLength(1000)]
        public Score Score { get; set; }
        public string TempScore { get; set; }
    }
    public class Score
    {
        [Range(-1,10)]
        public int M1 { get; set; }
        [Range(-1, 10)]
        public int M2 { get; set; }
        [Range(-1, 10)]
        public int M3 { get; set; }
        [Range(-1, 10)]
        public int M4 { get; set; }
        [Range(-1, 10)]
        public int M5 { get; set; }
        [Range(-1, 10)]
        public int M6 { get; set; }
        [Range(-1, 10)]
        public int M7 { get; set; }
        [Range(-1, 10)]
        public int T1 { get; set; }
        [Range(-1, 10)]
        public int T2 { get; set; }
        [Range(-1, 10)]
        public int T3 { get; set; }
        [Range(-1, 10)]
        public int K1 { get; set; }
    }
}