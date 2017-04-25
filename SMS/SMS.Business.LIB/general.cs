using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Text;


using System.Data;

using System.Security.Cryptography;
using System.Configuration;
using System.Diagnostics;

   public class General
    {
        /// <summary>
        /// Checks whether the string value can be evaluated to a value of type (int?).
        /// </summary>
        /// <param name="value">value</param>
        /// <returns>int? Nullable int value</returns>
        public static int? GetNullableInteger(string value)
        {
            int? i = null;
            int ii;

            if (int.TryParse(value, out ii))
                i = ii;
            return i;
        }


        public static Guid? GetNullableGuid(string value)
        {
            try
            {
                Guid result = new Guid(value);
                return result;
            }
            catch
            {
                return null;
            }
        }

        public static string GetNullableString(string value)
        {
            if (string.IsNullOrEmpty(value) || value.ToUpper().Equals("DUMMY"))
                return null;
            return value;
        }
        /// <summary>
        /// Gets a nullable date time value for a datetime string provided it is either null string or invalid date string
        /// </summary>
        /// <param name="datetime">datetime value represented as string</param>
        /// <returns>DateTime?: Nullable datetime object is returned if 1st argument is either null string or invalid date string</returns>
        public static DateTime? GetNullableDateTime(string datetime)
        {
            DateTime? dt = null;
            DateTime dtt;

            if (DateTime.TryParse(datetime, out dtt))
                dt = dtt;
            return dt;
        }

        /// <summary>
        /// Gets a nullable Decimal for the string provided it is either null string or invalid decimal string
        /// </summary>
        /// <param name="value"></param>
        /// <returns>Decimal?: Nullable decimal object</returns>
        public static Decimal? GetNullableDecimal(string value)
        {
            decimal? result = null;
            decimal tmpvalue;

            if (decimal.TryParse((string)value, out tmpvalue))
                result = tmpvalue;
            return result;
        }
        /// <summary>
        /// Gets a "dd/MMM/yyyy" format string for the date time string provided it is either null string or invalid format string
        /// </summary>
        /// <param name="datetimestring"> datase datetime values </param>
        /// <returns>String("dd/MMM/yyyy")?:null string object</returns>
        public static string GetDateTimeToString(string datetimestring)
        {
            string dtformatstring = null;
            DateTime dtt;

            if (DateTime.TryParse(datetimestring, out dtt))
                dtformatstring = dtt.ToString("dd/MMM/yyyy");
            return dtformatstring;
        }
        public static string GetMixedCase(string myString)
        {

            System.Globalization.CultureInfo cultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;
            System.Globalization.TextInfo textInfo = cultureInfo.TextInfo;
            string mixedCaseString = textInfo.ToTitleCase(myString.ToLower());
            return mixedCaseString;
        }

        public static bool IsvalidEmail(string email)
        {
            email = email.Replace(';', ',');
            email = email.Replace(" ", "");
            string[] mailids = email.Split(new char[] { ',' });

            foreach (string id in mailids)
            {
                string regex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                                @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                                @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
                Regex re = new Regex(regex);
                if (!re.IsMatch(id))
                    return (false);
            }
            return (true);
        }

       
   }
    
 