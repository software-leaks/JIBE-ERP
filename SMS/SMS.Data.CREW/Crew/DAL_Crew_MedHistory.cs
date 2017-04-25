using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace SMS.Data.Crew
{
    public class DAL_Crew_MedHistory
    {
        static IFormatProvider iFormatProvider = new System.Globalization.CultureInfo("en-GB", true);

        private static string connection = "";

        public DAL_Crew_MedHistory(string ConnectionString)
        {
            connection = ConnectionString;
        }
         static DAL_Crew_MedHistory()
        {
            connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
        }

        public static DataTable Get_Crew_MedHistory_Types_DL(int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@UserID",UserID)
                                        };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_MED_SP_Get_MedHistory_Types", sqlprm).Tables[0];
        }
        public static DataTable Get_Crew_MedHistory_CostItem_Types_DL(int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@UserID",UserID)
                                        };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_MED_SP_Get_MedHistory_CostItem_Types", sqlprm).Tables[0];
        }

        public static DataTable Get_Crew_MedHistory_DL(int FleetCode, int Vessel_ID, int CrewID, int Case_Type, int Status, string SearchText, int UserID, int PAGE_SIZE, int PAGE_INDEX, ref int SelectRecordCount, string SortBy, int? SortDirection)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@FleetCode",FleetCode),
                                            new SqlParameter("@Vessel_ID",Vessel_ID),
                                            new SqlParameter("@CrewID",CrewID),
                                            new SqlParameter("@Case_Type",Case_Type),
                                            new SqlParameter("@Status",Status),
                                            new SqlParameter("@SearchText",SearchText),
                                            new SqlParameter("@UserID",UserID),
                                            new SqlParameter("@PAGE_SIZE", PAGE_SIZE),
                                            new SqlParameter("@PAGE_INDEX",PAGE_INDEX),
                                            new SqlParameter("@ORDER_BY",SortBy),
                                            new SqlParameter("@SORT_DIRECTION",SortDirection),
                                            new SqlParameter("@SelectRecordCount",SelectRecordCount)

                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.InputOutput;

            DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_MED_SP_Get_MedHistory", sqlprm);
            SelectRecordCount = Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);

            return ds.Tables[0];
        }
        
        public static DataTable INSERT_Crew_MedHistory_DL(int VoyageID, int CrewID, int Case_Type, int Status, string Case_Detail, DateTime dtCase_Date, int Created_By, DateTime dtCase_To_Date)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewID",CrewID),
                                            new SqlParameter("@VoyageID",VoyageID),
                                            new SqlParameter("@Case_Type_ID",Case_Type),
                                            new SqlParameter("@Status",Status),
                                            new SqlParameter("@Case_Detail",Case_Detail),
                                            new SqlParameter("@Case_Date",dtCase_Date),
                                            new SqlParameter("@Created_By",Created_By),        
                                            new SqlParameter("@Case_To_Date",dtCase_To_Date),         
                                           // new SqlParameter("return",SqlDbType.Int)
                                        };

           // sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_MED_SP_Insert_MedHistory", sqlprm).Tables[0];
            // SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_MED_SP_Insert_MedHistory", sqlprm);
           // return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public static int UPDATE_Crew_MedHistory_DL(int Case_ID, int VoyageID, int CrewID, int Case_Type, int Status, string Case_Detail, DateTime dtCase_Date, int Created_By, DateTime dtCase_To_Date, int Office_ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Case_ID",Case_ID),
                                            new SqlParameter("@VoyageID",VoyageID),
                                            new SqlParameter("@CrewID",CrewID),
                                            new SqlParameter("@Case_Type",Case_Type),
                                            new SqlParameter("@Status",Status),
                                            new SqlParameter("@Case_Detail",Case_Detail),
                                            new SqlParameter("@Case_Date",dtCase_Date),
                                            new SqlParameter("@Created_By",Created_By),
                                            new SqlParameter("@Case_To_Date",dtCase_To_Date),
                                            new SqlParameter("@Office_ID",Office_ID),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };

            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_MED_SP_Update_MedHistory", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public static DataSet Get_Crew_MedHistory_Details_DL(int ID, int Vessel_ID, int Office_ID, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ID",ID),
                                            new SqlParameter("@Vessel_ID",Vessel_ID),
                                            new SqlParameter("@Office_ID",Office_ID),
                                            new SqlParameter("@UserID",UserID)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "dbo.CRW_MED_SP_Get_MedHistory_Details", sqlprm);
        }      
        /// /////////////////////////////
        public static DataTable Get_Med_CostItem_Attachments_DL(int Cost_Item_ID, int Case_ID, int Vessel_ID, int Office_ID, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Cost_Item_ID",Cost_Item_ID),
                                             new SqlParameter("@Case_ID",Case_ID),
                                            new SqlParameter("@Vessel_ID",Vessel_ID),
                                            new SqlParameter("@Office_ID",Office_ID),
                                            new SqlParameter("@UserID",UserID)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_MED_SP_Get_CostItem_Attachments", sqlprm).Tables[0];
        }

        public static int INSERT_Crew_MedHistory_Attachment_DL(int Case_ID, int Vessel_ID, int Office_ID, string Attachment_Name, string Attachment_Path, decimal Attachment_Size, int Created_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Case_ID",Case_ID),
                                            new SqlParameter("@Vessel_ID",Vessel_ID),
                                            new SqlParameter("@Office_ID",Office_ID),
                                            new SqlParameter("@Attachment_Name",Attachment_Name),
                                            new SqlParameter("@Attachment_Path",Attachment_Path),
                                            new SqlParameter("@Attachment_Size",Attachment_Size),
                                            new SqlParameter("@Created_By",Created_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };

            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_MED_SP_Insert_MedHistory_Attachment", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public static int DELETE_Crew_MedHistory_Attachment_DL(int Attachment_ID, int Case_ID, int Vessel_ID, int Office_ID, int Deleted_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Attachment_ID",Attachment_ID),
                                            new SqlParameter("@Case_ID",Case_ID),
                                            new SqlParameter("@Vessel_ID",Vessel_ID),
                                            new SqlParameter("@Office_ID",Office_ID),
                                            new SqlParameter("@Deleted_By",Deleted_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "dbo.CRW_MED_SP_Delete_MedHistory_Attachment", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public static DataTable Get_Crew_MedHistory_Attachments_DL(int Case_ID, int Vessel_ID, int Office_ID, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Case_ID",Case_ID),
                                             new SqlParameter("@Vessel_ID",Vessel_ID),
                                            new SqlParameter("@Office_ID",Office_ID),
                                            new SqlParameter("@UserID",UserID)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_MED_SP_Get_MedHistory_Attachments", sqlprm).Tables[0];
        }

        public static int INSERT_Crew_MedHistory_FollowUp_DL(int Case_ID, int Vessel_ID, int Office_ID, string FollowUpText, int Created_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Case_ID",Case_ID),
                                            new SqlParameter("@Vessel_ID",Vessel_ID),
                                            new SqlParameter("@Office_ID",Office_ID),
                                            new SqlParameter("@FollowUpText",FollowUpText),
                                            new SqlParameter("@Created_By",Created_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };

            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_MED_SP_Insert_MedHistory_FollowUp", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public static int DELETE_Crew_MedHistory_FollowUp_DL(int FollowUpID, int Vessel_ID, int Office_ID, int Deleted_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@FollowUpID",FollowUpID),
                                            new SqlParameter("@Vessel_ID",Vessel_ID),
                                            new SqlParameter("@Office_ID",Office_ID),
                                            new SqlParameter("@Deleted_By",Deleted_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_MED_SP_Delete_MedHistory_FollowUp", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public static DataTable Get_Crew_MedHistory_Followups_DL(int Case_ID, int Vessel_ID, int Office_ID, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Case_ID",Case_ID),
                                            new SqlParameter("@Vessel_ID",Vessel_ID),
                                            new SqlParameter("@Office_ID",Office_ID),
                                            new SqlParameter("@UserID",UserID)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_MED_SP_Get_MedHistory_FollowUps", sqlprm).Tables[0];
        }


        public static int INSERT_Med_Cost_Item_DL(int Case_ID, int Vessel_ID, int Office_ID, DateTime Exp_Date, string Desc, int Exp_Type, int Local_Curr, decimal? Local_Amt, decimal? USD_Amt, int Created_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Case_ID",Case_ID),
                                            new SqlParameter("@Vessel_ID",Vessel_ID),
                                            new SqlParameter("@Office_ID",Office_ID),
                                            new SqlParameter("@Exp_Date",Exp_Date),
                                            new SqlParameter("@Desc",Desc),
                                            new SqlParameter("@Exp_Type",Exp_Type),
                                            new SqlParameter("@Local_Curr",Local_Curr),
                                            new SqlParameter("@Local_Amt",Local_Amt),
                                            new SqlParameter("@USD_Amt",USD_Amt),
                                            new SqlParameter("@Created_By",Created_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };

            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_MED_SP_Insert_Med_Cost_Item", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        public static int UPDATE_Med_Cost_Item_DL(int Cost_Item_ID, int Case_ID, int Vessel_ID, int Office_ID, DateTime Exp_Date, string Desc, int Exp_Type, int Local_Curr, decimal? Local_Amt, decimal? USD_Amt, int Created_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Cost_Item_ID",Cost_Item_ID),                                            
                                            new SqlParameter("@Case_ID",Case_ID),
                                            new SqlParameter("@Vessel_ID",Vessel_ID),
                                            new SqlParameter("@Office_ID",Office_ID),
                                            new SqlParameter("@Exp_Date",Exp_Date),
                                            new SqlParameter("@Desc",Desc),
                                            new SqlParameter("@Exp_Type",Exp_Type),
                                            new SqlParameter("@Local_Curr",Local_Curr),
                                            new SqlParameter("@Local_Amt",Local_Amt),
                                            new SqlParameter("@USD_Amt",USD_Amt),
                                            new SqlParameter("@Created_By",Created_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };

            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_MED_SP_Update_Med_Cost_Item", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        public static int DELETE_Med_Cost_Item_DL(int Cost_Item_ID, int Case_ID, int Vessel_ID, int Office_ID, int Deleted_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Cost_Item_ID",Cost_Item_ID),
                                            new SqlParameter("@Case_ID",Case_ID),
                                            new SqlParameter("@Vessel_ID",Vessel_ID),
                                            new SqlParameter("@Office_ID",Office_ID),
                                            new SqlParameter("@Deleted_By",Deleted_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_MED_SP_Delete_Med_Cost_Item", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public static int INSERT_Med_Cost_Item_Attachment_DL(int Case_ID, int Cost_Item_ID, int Vessel_ID, int Office_ID, string Attachment_Name, string Attachment_Path, decimal Attachment_Size, int Created_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Case_ID",Case_ID),
                                            new SqlParameter("@Cost_Item_ID",Cost_Item_ID),
                                            new SqlParameter("@Vessel_ID",Vessel_ID),
                                            new SqlParameter("@Office_ID",Office_ID),
                                            new SqlParameter("@Attachment_Name",Attachment_Name),
                                            new SqlParameter("@Attachment_Path",Attachment_Path),
                                            new SqlParameter("@Attachment_Size",Attachment_Size),
                                            new SqlParameter("@Created_By",Created_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };

            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_MED_SP_Insert_MedHistory_Attachment", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        /// ///////////////
        public static int Approve_Crew_MedHistory_Claims_DL(int Crew_MedHistory_ID, int Vessel_ID, DataTable dtClaims, int ApprovedBy)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@dtClaims",dtClaims),
                                            new SqlParameter("@Crew_MedHistory_ID",Crew_MedHistory_ID),
                                            new SqlParameter("@Vessel_ID",Vessel_ID),
                                            new SqlParameter("@ApprovedBy",ApprovedBy),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[0].SqlDbType = SqlDbType.Structured;
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_QRY_SP_Approve_Crew_MedHistory_Claims", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public static int Reject_Crew_MedHistory_Claims_DL(int Crew_MedHistory_ID, int Vessel_ID, DataTable dtClaims, int ApprovedBy)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@dtClaims",dtClaims),
                                            new SqlParameter("@Crew_MedHistory_ID",Crew_MedHistory_ID),
                                            new SqlParameter("@Vessel_ID",Vessel_ID),
                                            new SqlParameter("@ApprovedBy",ApprovedBy),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[0].SqlDbType = SqlDbType.Structured;
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_QRY_SP_Reject_Crew_MedHistory_Claims", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
    }
}
