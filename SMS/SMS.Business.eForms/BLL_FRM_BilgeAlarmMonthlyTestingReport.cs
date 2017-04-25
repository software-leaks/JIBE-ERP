using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using SMS.Data;

namespace BilgeAlarmMonthlyTestingReport
{
    public class BLL_FRM_BilgeAlarmMonthlyTestingReport
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
        public static DataSet Get_BilgeAlarmMonthlyTestingReport_New(int Vessel_ID)
        {
            SqlParameter sqlprm = new SqlParameter("@Vessel_ID", Vessel_ID);
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "FRM_Get_Bilge_Alarm_Monthly_Testing_Report", sqlprm);
        }
        public static DataSet Get_BilgeAlarmMonthlyTestingReport_Details(int Main_Report_ID, int Vessel_ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Main_Report_ID",Main_Report_ID),
                                            new SqlParameter("@Vessel_ID",Vessel_ID)
                                        };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "FRM_Get_Bilge_Alarm_Monthly_Testing_Report", sqlprm);
        }
        public static DataSet Get_BilgeAlarmMonthlyTestingReport_Main(int Main_Report_ID, int Vessel_ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Main_Report_ID",Main_Report_ID),
                                            new SqlParameter("@Vessel_ID",Vessel_ID)
                                        };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "FRM_Get_Bilge_Alarm_Monthly_Testing_Report_Main", sqlprm);
        }
        #endregion

        
    }
}
