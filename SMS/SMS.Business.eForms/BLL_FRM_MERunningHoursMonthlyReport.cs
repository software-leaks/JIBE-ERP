using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using SMS.Data;

namespace MERunningHoursMonthlyReport
{
    public class BLL_FRM_MERunningHoursMonthlyReport
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
        public static DataSet Get_ME_RHrs_NewReport(int Vessel_ID)
        {
            SqlParameter sqlprm = new SqlParameter("@Vessel_ID", Vessel_ID);
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "FRM_Get_ME_RHrs_NewReport", sqlprm);
        }
        public static DataSet Get_ME_RHrs_ReportDetails(int Main_Report_ID, int Vessel_ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Main_Report_ID",Main_Report_ID),
                                            new SqlParameter("@Vessel_ID",Vessel_ID)
                                        };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "FRM_Get_ME_RHrs_ReportDetails", sqlprm);
        }

        public static int Update_MERunningHrsReport(int Main_Report_ID, int Vessel_ID, string ME_Type, DateTime? ReportDate, decimal Total_RH_LastMonth, decimal Total_RH_ThisMonth, DataTable dtMEItems,  
            decimal RH_From_Last_OH_GOVERNOR,decimal RH_From_Last_OH_TCH1, decimal RH_From_Last_OH_TCH2, decimal RH_From_Last_OH_TCH3,
            decimal AC1_Air_Side_RH_From_Last_Cleaning, decimal AC1_Water_Side_RH_From_Last_Cleaning, decimal AC1_Pressure_Drop_In_mm_WC, 
            decimal AC2_Air_Side_RH_From_Last_Cleaning, decimal AC2_Water_Side_RH_From_Last_Cleaning, decimal AC2_Pressure_Drop_In_mm_WC,
            decimal AC3_Air_Side_RH_From_Last_Cleaning, decimal AC3_Water_Side_RH_From_Last_Cleaning, decimal AC3_Pressure_Drop_In_mm_WC,
            DateTime? CRANK_CASE_INSPECTION_LAST_DONE, DateTime? CRANK_SHAFT_DEFLECTION_LAST_DONE, DateTime? LUBE_OIL_SAMPLE_LANDED_LAST_DONE, DateTime? 
            ME_TC_WATER_WASH_GAS_SIDE_LAST_DONE, DateTime? ME_GOVERNOR_LO_CHANGE_LAST_DONE, string Form_Assembly_Name, int Modified_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Main_Report_ID",Main_Report_ID),                                            
                                            new SqlParameter("@Vessel_ID",Vessel_ID),                                            
                                            new SqlParameter("@ME_Type",ME_Type),                                            
                                            new SqlParameter("@ReportDate",ReportDate),  
                                            new SqlParameter("@Total_RH_LastMonth",Total_RH_LastMonth),  
                                            new SqlParameter("@Total_RH_ThisMonth",Total_RH_ThisMonth),  
                                            new SqlParameter("@dtMEItems",dtMEItems),                                            
                                            new SqlParameter("@RH_From_Last_OH_GOVERNOR",RH_From_Last_OH_GOVERNOR),                                            
                                            new SqlParameter("@RH_From_Last_OH_TCH1",RH_From_Last_OH_TCH1),                                            
                                            new SqlParameter("@RH_From_Last_OH_TCH2",RH_From_Last_OH_TCH2),                                            
                                            new SqlParameter("@RH_From_Last_OH_TCH3",RH_From_Last_OH_TCH3),                                            
                                            new SqlParameter("@AC1_Air_Side_RH_From_Last_Cleaning",AC1_Air_Side_RH_From_Last_Cleaning),
                                            new SqlParameter("@AC1_Water_Side_RH_From_Last_Cleaning",AC1_Water_Side_RH_From_Last_Cleaning),
                                            new SqlParameter("@AC1_Pressure_Drop_In_mm_WC",AC1_Pressure_Drop_In_mm_WC),
                                            new SqlParameter("@AC2_Air_Side_RH_From_Last_Cleaning",AC2_Air_Side_RH_From_Last_Cleaning),
                                            new SqlParameter("@AC2_Water_Side_RH_From_Last_Cleaning",AC2_Water_Side_RH_From_Last_Cleaning),
                                            new SqlParameter("@AC2_Pressure_Drop_In_mm_WC",AC2_Pressure_Drop_In_mm_WC),
                                            new SqlParameter("@AC3_Air_Side_RH_From_Last_Cleaning",AC3_Air_Side_RH_From_Last_Cleaning),
                                            new SqlParameter("@AC3_Water_Side_RH_From_Last_Cleaning",AC3_Water_Side_RH_From_Last_Cleaning),
                                            new SqlParameter("@AC3_Pressure_Drop_In_mm_WC",AC3_Pressure_Drop_In_mm_WC),
                                            new SqlParameter("@CRANK_CASE_INSPECTION_LAST_DONE",CRANK_CASE_INSPECTION_LAST_DONE),
                                            new SqlParameter("@CRANK_SHAFT_DEFLECTION_LAST_DONE",CRANK_SHAFT_DEFLECTION_LAST_DONE),
                                            new SqlParameter("@LUBE_OIL_SAMPLE_LANDED_LAST_DONE",LUBE_OIL_SAMPLE_LANDED_LAST_DONE),
                                            new SqlParameter("@ME_TC_WATER_WASH_GAS_SIDE_LAST_DONE",ME_TC_WATER_WASH_GAS_SIDE_LAST_DONE),
                                            new SqlParameter("@ME_GOVERNOR_LO_CHANGE_LAST_DONE",ME_GOVERNOR_LO_CHANGE_LAST_DONE),
                                            new SqlParameter("@Form_Assembly_Name",Form_Assembly_Name),
                                            new SqlParameter("@Lib_User_ID",Modified_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "FRM_Update_ME_RHrs_Report", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        #endregion
    }
}
