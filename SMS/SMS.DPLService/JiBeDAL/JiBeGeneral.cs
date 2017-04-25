using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DALJibe
{
    public class JiBeGeneral
    {
        public static double ConvertLatLon(string sConvertString)
        {
            int index = sConvertString.IndexOf("'");
            if (index > 0)
                sConvertString = sConvertString.Substring(0, index);
            sConvertString = sConvertString.Replace("-", ".");
            sConvertString = sConvertString.Replace(" ", "");
            return Convert.ToDouble(sConvertString);
        }
        public static string Conv_Deg2Decimal_new(double degree, double mins, double secs, string directions)
        {

            string inputval = "";
            string input = inputval;
            string sign = "";

            double sd = 0;



            if ((directions.ToUpper() == "S") || (directions.ToUpper() == "W"))
            {
                sign = "-";
            }


            sd = (degree) + (mins / 60) + (secs / 3600);

            if (sign == "-")
            {
                sd = sd * (-1);
            }


            sd = Math.Round(sd, 6);
            string sdnew_other = Convert.ToString(sd);
            string sdnew1_other = "";
            sdnew1_other = string.Format("{0:0.000000}", sd);
            return sdnew1_other;


        }
    }
}