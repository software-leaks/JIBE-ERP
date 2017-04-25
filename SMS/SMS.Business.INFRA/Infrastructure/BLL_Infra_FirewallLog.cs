using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SMS.Data.Infrastructure;

namespace SMS.Business.Infrastructure
{ 
    public class BLL_Infra_FirewallLog
    {
        static IFormatProvider iFormatProvider = new System.Globalization.CultureInfo("en-GB", true);

        public static DataSet Get_FirewallLog(int UserID, DateTime? FromDate, DateTime? ToDate, string DateFilterType, DataTable Category, DataTable IP, DataTable UserName, int? PAGE_NUMBER, int? PAGE_SIZE, ref int isFetchCount)
        {
            try
            {
                return DAL_Infra_FirewallLog.Get_FirewallLog_DL(UserID, FromDate, ToDate, DateFilterType, Category, IP, UserName, PAGE_NUMBER, PAGE_SIZE, ref isFetchCount);
            }
            catch
            {
                throw;
            }
        }

        public static DataTable Get_FirewallLog_Export(int UserID, DateTime? FromDate, DateTime? ToDate, string DateFilterType, DataTable Category, DataTable IP, DataTable UserName)
        {
            try
            {
                return DAL_Infra_FirewallLog.Get_FirewallLog_Export_DL(UserID, FromDate, ToDate, DateFilterType, Category, IP, UserName);
            }
            catch
            {
                throw;
            }
        }
    }
}
