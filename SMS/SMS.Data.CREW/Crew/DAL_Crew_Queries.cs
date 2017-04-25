using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace SMS.Data.Crew
{
    public class DAL_Crew_Queries
    {
        static IFormatProvider iFormatProvider = new System.Globalization.CultureInfo("en-GB", true);

        private static string connection = "";

        public DAL_Crew_Queries(string ConnectionString)
        {
            connection = ConnectionString;
        }

        static DAL_Crew_Queries()
        {
            connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
        }

        public static DataTable Get_CrewQueries_DL(int FleetCode, int Vessel_ID, int CrewID, int Query_Type, int Status, string SearchText, int UserID, int PAGE_SIZE, int PAGE_INDEX, ref int SelectRecordCount, string SortBy, int? SortDirection)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@FleetCode",FleetCode),
                                            new SqlParameter("@Vessel_ID",Vessel_ID),
                                            new SqlParameter("@CrewID",CrewID),
                                            new SqlParameter("@Query_Type",Query_Type),
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

            DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_QRY_SP_Get_CrewQueries", sqlprm);
            SelectRecordCount = Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);

            return ds.Tables[0];            
        }
        public static DataSet Get_CrewQuery_Details_DL(int QueryID, int Vessel_ID, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@QueryID",QueryID),
                                            new SqlParameter("@Vessel_ID",Vessel_ID),
                                            new SqlParameter("@UserID",UserID)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_QRY_SP_Get_CrewQuery_Details", sqlprm);
        }

        public static DataTable Get_CrewQuery_Types_DL(int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@UserID",UserID)
                                        };
            
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_QRY_SP_Get_CrewQuery_Types", sqlprm).Tables[0];
        }
        public static int INSERT_CrewQuery_Type_DL(string CrewQuery_Type, int Created_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewQuery_Type",CrewQuery_Type),
                                            new SqlParameter("@Created_By",Created_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };

            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_QRY_SP_Insert_CrewQuery_Type", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public static int UPDATE_CrewQuery_Type_DL(int CrewQuery_Type_ID, string CrewQuery_Type, int Updated_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewQuery_Type_ID",CrewQuery_Type_ID),                                            
                                            new SqlParameter("@CrewQuery_Type",CrewQuery_Type),
                                            new SqlParameter("@Modified_By",Updated_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_QRY_SP_Update_CrewQuery_Type", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public static int DELETE_CrewQuery_Type_DL(int CrewQuery_Type_ID, int Deleted_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewQuery_Type_ID",CrewQuery_Type_ID),
                                            new SqlParameter("@Deleted_By",Deleted_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_QRY_SP_Delete_CrewQuery_Type", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        public static DataTable Get_CrewQuery_Approvers_DL(int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@UserID",UserID)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_QRY_SP_Get_CrewQuery_Approvers", sqlprm).Tables[0];
        }
        public static int INSERT_CrewQuery_Approver_DL(int ApproverID, int Created_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ApprovarID",ApproverID),
                                            new SqlParameter("@Created_By",Created_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };

            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_QRY_SP_Insert_CrewQuery_Approver", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public static int DELETE_CrewQuery_Approver_DL(int ApproverID, int Deleted_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ApprovarID",ApproverID),
                                            new SqlParameter("@Deleted_By",Deleted_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_QRY_SP_Delete_CrewQuery_Approvar", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        public static DataTable Get_Claim_Attachments_DL(int CrewQuery_ID, int Vessel_ID, int ClaimID, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewQuery_ID",CrewQuery_ID),
                                            new SqlParameter("@Vessel_ID",Vessel_ID),
                                            new SqlParameter("@ClaimID",ClaimID),
                                            new SqlParameter("@UserID",UserID)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_QRY_SP_Get_Claim_Attachments", sqlprm).Tables[0];
        }
        public static DataTable Get_CrewQuery_Attachments_DL(int CrewQuery_ID, int Vessel_ID,  int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewQuery_ID",CrewQuery_ID),
                                            new SqlParameter("@Vessel_ID",Vessel_ID),
                                            new SqlParameter("@UserID",UserID)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_QRY_SP_Get_CrewQuery_Attachments", sqlprm).Tables[0];
        }
        public static DataTable Get_CrewQuery_Followups_DL(int CrewQuery_ID, int Vessel_ID, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewQuery_ID",CrewQuery_ID),
                                            new SqlParameter("@Vessel_ID",Vessel_ID),
                                            new SqlParameter("@UserID",UserID)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_QRY_SP_Get_CrewQuery_Followups", sqlprm).Tables[0];
        }

        //public static int INSERT_CrewQuery_Attachment_DL(string AttachmentName, string AttachmentPath, int AttachmentSize, int CrewQuery_ID, int Vessel_ID, int Created_By)
        //{
        //    SqlParameter[] sqlprm = new SqlParameter[]
        //                                { 
        //                                    new SqlParameter("@AttachmentName",AttachmentName),
        //                                    new SqlParameter("@AttachmentPath",AttachmentPath),
        //                                    new SqlParameter("@AttachmentSize",AttachmentSize),
        //                                    new SqlParameter("@CrewQuery_ID",CrewQuery_ID),
        //                                    new SqlParameter("@Vessel_ID",Vessel_ID),
        //                                    new SqlParameter("@Created_By",Created_By),
        //                                    new SqlParameter("return",SqlDbType.Int)
        //                                };

        //    sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
        //    SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_QRY_SP_Insert_CrewQuery_Attachment", sqlprm);
        //    return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        //}
        //public static int DELETE_CrewQuery_Attachment_DL(int CrewQuery_ID, int Vessel_ID, int Attachment_ID, int Deleted_By)
        //{
        //    SqlParameter[] sqlprm = new SqlParameter[]
        //                                { 
        //                                    new SqlParameter("@CrewQuery_ID",CrewQuery_ID),
        //                                    new SqlParameter("@Vessel_ID",Vessel_ID),
        //                                    new SqlParameter("@Attachment_ID",Attachment_ID),
        //                                    new SqlParameter("@Deleted_By",Deleted_By),
        //                                    new SqlParameter("return",SqlDbType.Int)
        //                                };
        //    sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
        //    SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_QRY_SP_Delete_CrewQuery_Attachment", sqlprm);
        //    return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        //}

        public static int INSERT_CrewQuery_FollowUp_DL(int CrewQuery_ID, int Vessel_ID, string FollowUpText, int Sent_To_Ship, int Created_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewQuery_ID",CrewQuery_ID),
                                            new SqlParameter("@Vessel_ID",Vessel_ID),
                                            new SqlParameter("@FollowUpText",FollowUpText),
                                            new SqlParameter("@Created_By",Created_By),
                                            new SqlParameter("@Sent_To_Ship",Sent_To_Ship),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };

            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_QRY_SP_Insert_CrewQuery_FollowUp", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public static int DELETE_CrewQuery_FollowUp_DL(int CrewQuery_ID, int Vessel_ID, int FollowUp_ID, int Deleted_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewQuery_ID",CrewQuery_ID),
                                            new SqlParameter("@Vessel_ID",Vessel_ID),
                                            new SqlParameter("@FollowUp_ID",FollowUp_ID),
                                            new SqlParameter("@Deleted_By",Deleted_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_QRY_SP_Delete_CrewQuery_FollowUp", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        public static int Approve_CrewQuery_Claims_DL(int CrewQuery_ID, int Vessel_ID, DataTable dtClaims, int ApprovedBy)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@dtClaims",dtClaims),
                                            new SqlParameter("@CrewQuery_ID",CrewQuery_ID),
                                            new SqlParameter("@Vessel_ID",Vessel_ID),
                                            new SqlParameter("@ApprovedBy",ApprovedBy),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[0].SqlDbType = SqlDbType.Structured;
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_QRY_SP_Approve_CrewQuery_Claims", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public static int Reject_CrewQuery_Claims_DL(int CrewQuery_ID, int Vessel_ID, DataTable dtClaims, int ApprovedBy)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@dtClaims",dtClaims),
                                            new SqlParameter("@CrewQuery_ID",CrewQuery_ID),
                                            new SqlParameter("@Vessel_ID",Vessel_ID),
                                            new SqlParameter("@ApprovedBy",ApprovedBy),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[0].SqlDbType = SqlDbType.Structured;
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_QRY_SP_Reject_CrewQuery_Claims", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
    }
}
