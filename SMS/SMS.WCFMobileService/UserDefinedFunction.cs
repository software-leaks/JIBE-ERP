using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Security.Cryptography;
using System.IO;
using System.Text;
using System.Configuration;
using System.Data.SqlTypes;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
using System.Collections;
using System.Collections.ObjectModel;

/// <summary>
/// Summary description for UserDefinedFunction
/// </summary>
public class UDFLib_WCF
{

    public UDFLib_WCF()
    {
        //
        // TODO: Add constructor logic here
        //
    }



    public static decimal ConvertToDecimal(decimal? value)
    {

        decimal retValue = 0;
        if (decimal.TryParse(value.ToString(), out retValue))
        {

        }

        return retValue;
    }

    public static decimal ConvertToDecimal(string value)
    {

        decimal retValue = 0;
        if (decimal.TryParse(value, out retValue))
        {

        }

        return retValue;
    }

    public static int ConvertToInteger(string value)
    {

        int retValue = 0;
        if (int.TryParse(value, out retValue))
        {

        }

        return retValue;
    }



    public static decimal ConvertToDecimal(dynamic value)
    {

        decimal retValue = 0;
        decimal.TryParse(Convert.ToString(value), out retValue);

        return retValue;
    }

    public static int ConvertToInteger(dynamic value)
    {

        int retValue = 0;
        int.TryParse(Convert.ToString(value), out retValue);
        return retValue;
    }


    public static bool ConvertToBool(dynamic value)
    {

        bool retValue;
        bool.TryParse(Convert.ToString(value), out retValue);
        return retValue;
    }

    public static Int16 ConvertToInt16(dynamic value)
    {

        Int16 retValue = 0;
        Int16.TryParse(Convert.ToString(value), out retValue);
        return retValue;
    }

    public static int? ConvertIntegerToNull(object value)
    {
        int retValue = 0;
        int.TryParse(Convert.ToString(value), out retValue);

        if (retValue == 0)
            return null;
        else
            return retValue;
    }

    public static decimal? ConvertDecimalToNull(object value)
    {
        decimal? retVal = 0;
        decimal outValue = 0;
        if (decimal.TryParse(Convert.ToString(value), out outValue))
        {
            retVal = outValue;
        }
        else
            retVal = null;

        return retVal;

    }


    public static DateTime? ConvertDateToNull(object value)
    {
        DateTime? retValue = new DateTime();
        DateTime outValue = new DateTime();
        if (DateTime.TryParse(Convert.ToString(value), out outValue))
        {
            retValue = outValue;
        }
        else
            retValue = null;

        return retValue;

    }

    public static DateTime ConvertToDate(object value)
    {
        DateTime retValue = new DateTime();
        DateTime outValue = new DateTime();
        if (DateTime.TryParse(Convert.ToString(value), out outValue))
        {
            retValue = outValue;
        }


        return retValue;

    }


    public static string ConvertStringToNull(object value)
    {
        if (Convert.ToString(value) == "0" || Convert.ToString(value) == "")
            return null;
        else
            return Convert.ToString(value);
    }


}



