using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VinhEdu.Models
{
    public class CustomScoreRequired : ValidationAttribute
    {
        //public double Score { get; set; }
        //public string StrScore { get; set; }

        public CustomScoreRequired()
        {
            //this.Score = 0;
            //this.StrScore = "x";
        }

        public override bool IsValid(object value)
        {
            string strValue = value as string;
            double dValue = 0;
            if(String.IsNullOrWhiteSpace(strValue))
            {
                return false;
            }
            else
            {
                if(strValue == "x")
                {
                    return true;
                }
                else
                {
                    bool isDouble = Double.TryParse(strValue, out dValue);
                    if(isDouble)
                    {
                        if(dValue<= 10 && dValue >= 0)
                        {
                            return true;
                        }
                        return false;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}