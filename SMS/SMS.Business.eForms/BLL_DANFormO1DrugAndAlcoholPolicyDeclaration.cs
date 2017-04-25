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
   public class BLL_DANFormO1DrugAndAlcoholPolicyDeclaration
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
       public static DataTable Get_RankList()
       {
           return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_Get_RankList").Tables[0];
       }

       public static DataTable Get_Crewlist_Index(DateTime ? Reportdate)
       {
           SqlParameter[] sqlprm = new SqlParameter[]
            {
               
                new SqlParameter("@REPORTDATE",Reportdate)
            };        

          return  SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_Get_Crewlist", sqlprm).Tables[0];           
       }

       public static int InsertDrugAndAlcoholPolicy(int Vessel_ID, string Form_Assembly_Name, int Crew_ID, int Rank_ID, DateTime? Date_signed_on, int Mouthpiece_issue, int Witness_Crew_ID, DateTime? ReportDate, int Modified_By)
       {
           SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Vessel_ID",Vessel_ID),                                            
                                            new SqlParameter("@Form_Assembly_Name",Form_Assembly_Name),                                            
                                            new SqlParameter("@Crew_ID",Crew_ID),                                            
                                            new SqlParameter("@Rank_ID",Rank_ID),                                            
                                            new SqlParameter("@Date_signed_on",Date_signed_on),                                            
                                            new SqlParameter("@Mouthpiece_issue",Mouthpiece_issue),                                            
                                            new SqlParameter("@Witness_Crew_ID",Witness_Crew_ID),
                                            new SqlParameter("@ReportDate",ReportDate),
                                            new SqlParameter("@USERID",Modified_By)
                                           
                                        };

           return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "FRM_INS_DNA_Drug_Alcohol_Policy_Declartion", sqlprm);
           
       }
       public static DataTable GetDrugAndAlcoholPolicy(int ID, int Vessel_ID)
       {
           SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ID",ID),
                                            new SqlParameter("@Vessel_ID",Vessel_ID)
                                        };

           return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "FRM_GET_DNA_Drug_Alcohol_Policy_Declartion", sqlprm).Tables[0];
       }
    }
}
