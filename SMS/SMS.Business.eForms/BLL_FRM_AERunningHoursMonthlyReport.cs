using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using SMS.Data;

namespace AERunningHoursMonthlyReport
{
    public class BLL_FRM_AERunningHoursMonthlyReport
    {

        private static string connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
        private static IFormatProvider iFormatProvider = new System.Globalization.CultureInfo("en-GB", true);

        #region  - GET -
        public static DataTable Get_Assembly_Details(string Assembly_Name, int Vessel_ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Assembly_Name",Assembly_Name),
                                            new SqlParameter("@Vessel_ID",Vessel_ID)
                                        };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "FRM_Get_Assembly_Details", sqlprm).Tables[0];
        }
        public static DataSet Get_AEMonthlyRunningHrsReport(int Main_Report_ID, int Vessel_ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Main_Report_ID",Main_Report_ID),
                                            new SqlParameter("@Vessel_ID",Vessel_ID)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "FRM_Get_AE_RHrs_Report_Details", sqlprm);

        }
        public static int Update_AERunningHrsReport(int Main_Report_ID, int Vessel_ID, string AE_Type, DateTime? ReportDate, DataTable dtItems, DataTable dtRHSummary, string Form_Assembly_Name, int Modified_By)
        {            
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Main_Report_ID",Main_Report_ID),                                            
                                            new SqlParameter("@Vessel_ID",Vessel_ID),                                            
                                            new SqlParameter("@AE_Type",AE_Type),                                            
                                            new SqlParameter("@ReportDate",ReportDate),                                            
                                            new SqlParameter("@dtRHSummary",dtRHSummary),                                            
                                            new SqlParameter("@dtItems",dtItems),                                            
                                            new SqlParameter("@Lib_User_ID",Modified_By),
                                            new SqlParameter("@Form_Assembly_Name",Form_Assembly_Name),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "FRM_Update_AE_RHrs_Report", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        #endregion
    }
}
