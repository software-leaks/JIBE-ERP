using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using SMS.Data;

namespace SMS.Business.eForms
{
    public class BLL_DANFormO2AlcoholTestLog
    {

        private static string connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
        private static IFormatProvider iFormatProvider = new System.Globalization.CultureInfo("en-GB", true);


        public static DataTable Get_Assembly_Details(string Assembly_Name, int Vessel_ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Assembly_Name",Assembly_Name),
                                            new SqlParameter("@Vessel_ID",Vessel_ID)
                                        };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "FRM_Get_Assembly_Details", sqlprm).Tables[0];
        }
        
        
        public static DataSet Get_DNA_Alcohol_Test_Log(int? Main_Report_ID ,int? Vessel_ID)
        {
                 
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Main_Report_ID",Main_Report_ID),                                            
                                            new SqlParameter("@Vessel_ID",Vessel_ID),                                            
                                        };
        
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "FRM_Get_DNA_Alcohol_Test_Log", sqlprm);

        }

       

        public static DataTable Get_Current_Crew_OnBoard(int Vessel_ID)
        {
            SqlParameter sqlprm = new SqlParameter("@Vessel_ID", Vessel_ID);
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "FRM_Get_Current_Crew_OnBoard", sqlprm).Tables[0];
        }
        
        
        public static DataTable Get_RankList()
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_Get_RankList").Tables[0];
        }


        public static DataTable Get_PortList_Mini_DL(string SearchText)
        {

            if (SearchText == "0")
                SearchText = "";

            SqlParameter[] sqlprm = new SqlParameter[] { new SqlParameter("@SearchText", SearchText) };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_INF_Get_PortList_Mini", sqlprm).Tables[0];

        }




        #region  - UPDATE -

        public static int Update_DANFormO2AlcoholTestLog(int Main_Report_ID, int Vessel_ID, DateTime? ReportDate, DataTable dtMEItems,string Form_Assembly_Name, int Modified_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Main_Report_ID",Main_Report_ID),                                            
                                            new SqlParameter("@Vessel_ID",Vessel_ID),  
                                            new SqlParameter("@ReportDate",ReportDate),  
                                            new SqlParameter("@dtMEItems",dtMEItems),                                            
                                            new SqlParameter("@Form_Assembly_Name",Form_Assembly_Name),
                                            new SqlParameter("@Lib_User_ID",Modified_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "FRM_Update_DNA_Alcohol_Test_Log", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        #endregion


    }
}
