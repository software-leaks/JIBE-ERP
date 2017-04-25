using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using SMS.Data;

namespace AEPerformanceReport
{
    public class BLL_FRM_AEPerformanceReport
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
        
        public static DataSet Get_AEPerformanceReport(int Main_Report_ID, int Vessel_ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Main_Report_ID",Main_Report_ID),
                                            new SqlParameter("@Vessel_ID",Vessel_ID)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "FRM_Get_AE_Perf_Report", sqlprm);
        }

        public static DataSet Get_AEPerformanceReport_Engine(int Engine_No, int Main_Report_ID, int Vessel_ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Engine_No",Engine_No),
                                            new SqlParameter("@Main_Report_ID",Main_Report_ID),
                                            new SqlParameter("@Vessel_ID",Vessel_ID)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "FRM_Get_AE_Perf_Report_Engine", sqlprm);
        }

        #endregion

        
    }
}
