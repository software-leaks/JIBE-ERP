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
    public class BLL_ColdWorkPermit
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



        public static DataSet Get_Cold_Work_Permit_Details(int? Main_Report_ID, int? Vessel_ID)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Main_Report_ID",Main_Report_ID),                                            
                                            new SqlParameter("@Vessel_ID",Vessel_ID),                                            
                                        };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "FRM_Get_Cold_Work_Permit_Details", sqlprm);

        }



        #region  - UPDATE -

        public static int Update_Cold_Work_Permit(int? ID, int Vessel_ID,int? Form_ID, DateTime? ReportDate, string Reference_No, int Distribution
            ,  DateTime? Permit_Valid_From,DateTime? Permit_Valid_Till,string Location_Name,int Permit_Issue, string Work_Description
            ,string Carrying_out_work,string Person_Attendance, int Vented_to_atm,int Drained,int Washed,int Purged,string Other_1,int Spaded
            ,int Disconnected ,int Closed,string Other_2, int Oil ,int  Gas ,int H2S ,int Steam ,int Pressure , int Surrounding_hazards,int Certificate_issued
            , string Protection_Worn, string Material_Service, string Hazardous_Material, string Special_Precautions, int? Officer_in_Charge, DateTime? Officer_Sign_Date
            ,int Created_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ID"	,ID),							 
	                                         new SqlParameter("@Vessel_ID",	Vessel_ID),					 
	                                         new SqlParameter("@Form_ID",Form_ID),					 
	                                         new SqlParameter("@ReportDate",ReportDate),					 
	                                         new SqlParameter("@Reference_No",Reference_No),
	                                         new SqlParameter("@Distribution",Distribution),					 
	                                         new SqlParameter("@Permit_Valid_From",Permit_Valid_From),
	                                         new SqlParameter("@Permit_Valid_Till",	Permit_Valid_Till),			 	
	                                         new SqlParameter("@Location_Name",Location_Name),					 
	                                         new SqlParameter("@Permit_Issue",  Permit_Issue),                
	                                         new SqlParameter("@Work_Description",Work_Description),				 
	                                         new SqlParameter("@Carrying_out_work",	Carrying_out_work),			  
	                                         new SqlParameter("@Person_Attendance",	Person_Attendance),			 
	                                         new SqlParameter("@Vented_to_atm",	Vented_to_atm),			 
	                                         new SqlParameter("@Drained",Drained),						 
	                                         new SqlParameter("@Washed",Washed),						 
	                                         new SqlParameter("@Purged",Purged),						 
	                                         new SqlParameter("@Other_1",Other_1),						 
	                                         new SqlParameter("@Spaded",Spaded),						 
	                                         new SqlParameter("@Disconnected",Disconnected),
	                                         new SqlParameter("@Closed",Closed),						 
	                                         new SqlParameter("@Other_2",Other_2),						 
	                                         new SqlParameter("@Oil",Oil),							 
	                                         new SqlParameter("@Gas",Gas),							 
	                                         new SqlParameter("@H2S",H2S),							 
	                                         new SqlParameter("@Steam",Steam),							 
	                                         new SqlParameter("@Pressure",Pressure),						 
	                                         new SqlParameter("@Surrounding_hazards",Surrounding_hazards),			 
	                                         new SqlParameter("@Certificate_issued",Certificate_issued),		 
	                                         new SqlParameter("@Protection_Worn",Protection_Worn),				 
	                                         new SqlParameter("@Material_Service",Material_Service),				 
	                                         new SqlParameter("@Hazardous_Material",Hazardous_Material)	,		 
	                                         new SqlParameter("@Special_Precautions",Special_Precautions),			 
	                                         new SqlParameter("@Officer_in_Charge",	Officer_in_Charge),			 
	                                         new SqlParameter("@Officer_Sign_Date",Officer_Sign_Date),				 
	                                         new SqlParameter("@Created_By",Created_By),	
                                             new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "FRM_Update_Cold_Work_Permit", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        #endregion


    }
}
