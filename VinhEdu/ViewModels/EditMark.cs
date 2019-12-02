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
        [RegularExpression(@"^[x]|(([1-9][0-9]{0,1}(\.[\d]{1,2})?|10))$")]
        public string M1 { get; set; }
        [RegularExpression(@"^[x]|(([1-9][0-9]{0,1}(\.[\d]{1,2})?|10))$")]
        public string M2 { get; set; }
        [RegularExpression(@"^[x]|(([1-9][0-9]{0,1}(\.[\d]{1,2})?|10))$")]
        public string M3 { get; set; }
        [RegularExpression(@"^[x]|(([1-9][0-9]{0,1}(\.[\d]{1,2})?|10))$")]
        public string M4 { get; set; }
        [RegularExpression(@"^[x]|(([1-9][0-9]{0,1}(\.[\d]{1,2})?|10))$")]
        public string M5 { get; set; }
        [RegularExpression(@"^[x]|(([1-9][0-9]{0,1}(\.[\d]{1,2})?|10))$")]
        public string M6 { get; set; }
        [RegularExpression(@"^[x]|(([1-9][0-9]{0,1}(\.[\d]{1,2})?|10))$")]
        public string M7 { get; set; }
        [RegularExpression(@"^[x]|(([1-9][0-9]{0,1}(\.[\d]{1,2})?|10))$")]
        public string T1 { get; set; }
        [RegularExpression(@"^[x]|(([1-9][0-9]{0,1}(\.[\d]{1,2})?|10))$")]
        public string T2 { get; set; }
        [RegularExpression(@"^[x]|(([1-9][0-9]{0,1}(\.[\d]{1,2})?|10))$")]
        public string T3 { get; set; }
        [RegularExpression(@"^[x]|(([1-9][0-9]{0,1}(\.[\d]{1,2})?|10))$")]
        public string K1 { get; set; }
    }
}