using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace SMS.Data.Infrastructure
{
    public class DAL_Infra_FirewallLog
    {
        private static string connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;

        public static DataSet Get_FirewallLog_DL(int UserID, DateTime? FromDate, DateTime? ToDate, string DateFilterType, DataTable Category, DataTable IP, DataTable UserName, int? PAGE_NUMBER, int? PAGE_SIZE, ref int isFetchCount)
        {
            SqlParameter[] sqlprm = new SqlParameter[] 
                { 
                    new SqlParameter("@UserID", UserID),
                    new SqlParameter("@FromDate", FromDate),
                    new SqlParameter("@ToDate", ToDate),
                    new SqlParameter("@DateFilterType", DateFilterType),

                    new SqlParameter("@Category", Category),
                    new SqlParameter("@IP", IP),
                    new SqlParameter("@UserName", UserName),
                    new SqlParameter("@PAGE_NUMBER",PAGE_NUMBER),
                    new SqlParameter("@PAGE_SIZE",PAGE_SIZE),
                    new SqlParameter("@isFetchCount",isFetchCount)
                };

            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.InputOutput;

            DataSet ds= SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_INF_Get_FirewallLog", sqlprm);
            isFetchCount = Convert.ToInt32( sqlprm[sqlprm.Length - 1].Value);

            return ds;
        }

        public static DataTable Get_FirewallLog_Export_DL(int UserID, DateTime? FromDate, DateTime? ToDate, string DateFilterType, DataTable Category, DataTable IP, DataTable UserName)
        {
            SqlParameter[] sqlprm = new SqlParameter[] 
                { 
                    new SqlParameter("@UserID", UserID),
                    new SqlParameter("@FromDate", FromDate),
                    new SqlParameter("@ToDate", ToDate),
                    new SqlParameter("@DateFilterType", DateFilterType),

                    new SqlParameter("@Category", Category),
                    new SqlParameter("@IP", IP),
                    new SqlParameter("@UserName", UserName)
                };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_INF_Get_FirewallLog_Export", sqlprm).Tables[0];
            
        }
    }
}
