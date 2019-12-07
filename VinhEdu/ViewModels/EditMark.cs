using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using VinhEdu.Models;

namespace VinhEdu.ViewModels
{
    public class MarkStudent
    {
        public int StudentID { get; set; }
        public string StudentName { get; set; }
        public List<SubjectScore> SubjectScores { get; set; }
    }
    public class SubjectScore
    {
        public string SubjectName { get; set; }
        public int SubjectID { get; set; }
        public Score Score { get; set; }
        public string TempScore { get; set; }
    }
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
        [CustomScoreRequired]
        public string M1 { get; set; } = "x";
        [CustomScoreRequired]
        public string M2 { get; set; } = "x";
        [CustomScoreRequired]
        public string M3 { get; set; } = "x";
        [CustomScoreRequired]
        public string M4 { get; set; } = "x";
        [CustomScoreRequired]
        public string P1 { get; set; } = "x";
        [CustomScoreRequired]
        public string P2 { get; set; } = "x";
        [CustomScoreRequired]
        public string P3 { get; set; } = "x";
        [CustomScoreRequired]
        public string T1 { get; set; } = "x";
        [CustomScoreRequired]
        public string T2 { get; set; } = "x";
        [CustomScoreRequired]
        public string T3 { get; set; } = "x";
        [CustomScoreRequired]
        public string K1 { get; set; } = "x";
    }
}