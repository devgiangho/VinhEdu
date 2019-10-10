using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
namespace VinhEdu.Models
{
    public class ScoreBoard
    {
        public int score1 { get; set; } = -1;
        public int score2 { get; set; } = -1;
        public int score3 { get; set; } = -1;
        public int score4 { get; set; } = -1;
        public int score5 { get; set; } = -1;
        public int score6 { get; set; } = -1;
        public int score7 { get; set; } = -1;
        public int score8 { get; set; } = -1;
        public int score9 { get; set; } = -1;
        public int score10 { get; set; } = -1;
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
        //public void parseObject(string json)
        //{
        //    this = JsonConvert.DeserializeObject(json);
        //}

    }
    public class ScoreBoard3
    {
        public int score1 { get; set; } = -1;
        public int score2 { get; set; } = -1;
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}