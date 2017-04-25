using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using SMS.Data;
using System.Configuration;


namespace QC1200
{
	public class BLLL_FRM_QC1200
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


		public static DataSet GET_QC_CERReportMain(int Main_Report_ID, int Vessel_ID)
		{
			SqlParameter[] sqlprm = new SqlParameter[]
										{ 
											new SqlParameter("@Main_Report_ID",Main_Report_ID),
											new SqlParameter("@Vessel_ID",Vessel_ID)
										};
			return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "FRM_GET_QC_CER_Report_Main", sqlprm);

		}
		public static DataSet Get_QCCERNewReport(int Vessel_ID)
		{
			SqlParameter sqlprm = new SqlParameter("@Vessel_ID", Vessel_ID);
			return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "FRM_Get_QC_CER_NewReport", sqlprm);
		}

		#region  - UPDATE -

		public static int Update_QCCREReport(int Main_Report_ID, int Vessel_ID, DateTime? ReportDate,
		  string Quarter, string Staff_Code, string Rank_Short_Name, string Passport_Number, int PORT_ID, string MASTERS_NAME, string ENG_NAME, string Type_Of_Vessel
		  , string Type_Of_Engine, string DUTY_DESCRIPTION, string REEMPLOYMENT_DESCRIPTION
		  , string Crew_Remarks, string Superintendent_Remarks, string Professionalism, string English_Language, string Responsibility
		, string Dedication, string Loyalty, string Leadership, string Health, string Sobriety, string Discipline, string Initiative
		, string Obedience, string Social_Relations, string REEMPLOYMENT, string ARPA, string COLREG
		, string Stability, string Watch_Keeping_DO, string First_Aid_DO, string Safety_DO, string Env_Awr_DO, string GK_Of_Machinary
		, string Safety_EO, string Watch_Keeping_EO, string Electric_Plant, string Maintenance, string Env_Awr_EO, string Env_Awr_Rating,
			string Form_Assembly_Name, int Modified_By)
		{
			SqlParameter[] sqlprm = new SqlParameter[]
										{ 
											new SqlParameter("@Main_Report_ID",Main_Report_ID),                                            
											new SqlParameter("@Vessel_ID",Vessel_ID),       
											new SqlParameter("@ReportDate",ReportDate),  
											new SqlParameter("@Quarter",Quarter), 
											new SqlParameter("@Staff_Code",	 Staff_Code), 
											new SqlParameter("@Rank_Short_Name",	 Rank_Short_Name), 
											new SqlParameter("@Passport_Number",	Passport_Number	 ), 
											new SqlParameter("@PORT_ID",		PORT_ID			 ), 
											new SqlParameter("@MASTERS_NAME",	MASTERS_NAME	), 				 
											new SqlParameter("@ENG_NAME",	ENG_NAME			), 	     
											new SqlParameter("@Type_Of_Vessel",	Type_Of_Vessel		), 		 
											new SqlParameter("@Type_Of_Engine", Type_Of_Engine), 
											new SqlParameter("@DUTY_DESCRIPTION", DUTY_DESCRIPTION), 
											new SqlParameter("@REEMPLOYMENT_DESCRIPTION",REEMPLOYMENT_DESCRIPTION), 					 
											new SqlParameter("@Crew_Remarks",Crew_Remarks				 ), 
											new SqlParameter("@Superintendent_Remarks",Superintendent_Remarks), 
											new SqlParameter("@Professionalism",Professionalism), 
											new SqlParameter("@English_Language",English_Language), 
											new SqlParameter("@Responsibility",Responsibility), 
											new SqlParameter("@Dedication",Dedication), 
											new SqlParameter("@Loyalty",Loyalty), 
											new SqlParameter("@Leadership",Leadership), 
											new SqlParameter("@Health",Health), 
											new SqlParameter("@Sobriety", Sobriety ), 
											new SqlParameter("@Discipline",Discipline), 
											new SqlParameter("@Initiative",Initiative), 
											new SqlParameter("@Obedience",Obedience), 
											new SqlParameter("@Social_Relations",Social_Relations), 
											new SqlParameter("@REEMPLOYMENT",REEMPLOYMENT), 
											new SqlParameter("@ARPA",ARPA), 
											new SqlParameter("@COLREG",COLREG), 
											new SqlParameter("@Stability",Stability), 
											new SqlParameter("@Watch_Keeping_DO",Watch_Keeping_DO), 
											new SqlParameter("@First_Aid_DO",First_Aid_DO), 
											new SqlParameter("@Safety_DO",Safety_DO), 
											new SqlParameter("@Env_Awr_DO",Env_Awr_DO), 
											new SqlParameter("@GK_Of_Machinary",GK_Of_Machinary), 
											new SqlParameter("@Safety_EO",Safety_EO), 
											new SqlParameter("@Watch_Keeping_EO",Watch_Keeping_EO), 
											new SqlParameter("@Electric_Plant",Electric_Plant), 
											new SqlParameter("@Maintenance",Maintenance), 
											new SqlParameter("@Env_Awr_EO",Env_Awr_EO),                                             
											new SqlParameter("@Env_Awr_Rating",Env_Awr_Rating), 
											new SqlParameter("@Form_Assembly_Name",Form_Assembly_Name),
											new SqlParameter("@Lib_User_ID",Modified_By),
											new SqlParameter("return",SqlDbType.Int)
										};
			sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
			SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "FRM_UPD_QC_CRE_Report", sqlprm);
			return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
		}
		#endregion
	}


}
