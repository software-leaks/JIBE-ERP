using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using SMS.Data;
using System.Configuration;

namespace SMS.Data.Operation
{
    public class DAL_OPS_BunkerAnalysis
    {
        private static string connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
        public DAL_OPS_BunkerAnalysis(string ConnectionString)
        {
            connection = ConnectionString;
        }
        public DAL_OPS_BunkerAnalysis()
        {
            connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
        }

        public static DataTable Get_BunkerAnalysisReport_DL(int FleetCode, int Vessel_ID, int Bunker_Supplier, int Bunker_Lab, int Status, DateTime DateFrom, DateTime DateTo, int UserID, int? PAGE_SIZE, int? PAGE_INDEX, ref int SelectRecordCount)
        {
            SqlParameter[] sqlprm = new SqlParameter[] { 
                new SqlParameter("@FleetCode", FleetCode),
                new SqlParameter("@Vessel_ID", Vessel_ID),
                new SqlParameter("@Bunker_Supplier", Bunker_Supplier),
                new SqlParameter("@Bunker_Lab", Bunker_Lab),
                new SqlParameter("@Status", Status) ,
                new SqlParameter("@DateFrom", DateFrom) ,
                new SqlParameter("@DateTo", DateTo),
                new SqlParameter("@UserID", UserID),
                new SqlParameter("@PAGE_SIZE", PAGE_SIZE),
                new SqlParameter("@PAGE_INDEX",PAGE_INDEX),
                new SqlParameter("@SelectRecordCount",SelectRecordCount)
            };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.InputOutput;

            DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "OPS_SP_Get_BunkerAnalysisReport", sqlprm);
            SelectRecordCount = Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
            return ds.Tables[0];            
        }

        public static DataTable Get_LOAnalysisReport_DL(int FleetCode, int Vessel_ID, int Bunker_Supplier, int Bunker_Lab, int Status, DateTime DateFrom, DateTime DateTo, int UserID, int BunkerPort, string AirwayBill, int? PAGE_SIZE, int? PAGE_INDEX, ref int SelectRecordCount)
        {
            SqlParameter[] sqlprm = new SqlParameter[] { 
                new SqlParameter("@FleetCode", FleetCode),
                new SqlParameter("@Vessel_ID", Vessel_ID),
                new SqlParameter("@Bunker_Supplier", Bunker_Supplier),
                new SqlParameter("@Bunker_Lab", Bunker_Lab),
                new SqlParameter("@Status", Status) ,
                new SqlParameter("@DateFrom", DateFrom) ,
                new SqlParameter("@DateTo", DateTo),
                new SqlParameter("@UserID", UserID),
                new SqlParameter("@BunkerPort", BunkerPort),
                new SqlParameter("@AirwayBill", AirwayBill),
                new SqlParameter("@PAGE_SIZE", PAGE_SIZE),
                new SqlParameter("@PAGE_INDEX",PAGE_INDEX),
                new SqlParameter("@SelectRecordCount",SelectRecordCount)
            };            
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.InputOutput;

            DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "OPS_SP_Get_LOAnalysisReport", sqlprm);
            SelectRecordCount = Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
            return ds.Tables[0];
        }


        public static DataSet UPDATE_BunkerAnalysis_DL(int ID, int Vessel_ID, DateTime Bunkering_Date, int PortID, int Bunker_SupplierID, int LabID, string AirwayBill, int Status, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[] { 
                new SqlParameter("@ID", ID),
                new SqlParameter("@Vessel_ID", Vessel_ID),
                new SqlParameter("@Bunkering_Date", Bunkering_Date),
                new SqlParameter("@PortID", PortID),
                new SqlParameter("@Bunker_SupplierID", Bunker_SupplierID) ,
                new SqlParameter("@LabID", LabID) ,
                new SqlParameter("@AirwayBill", AirwayBill),
                new SqlParameter("@Status", Status),
                new SqlParameter("@UserID", UserID) 
            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "OPS_SP_UPDATE_BunkerAnalysis", sqlprm);
        }

        public static int UPDATE_BunkerAnalysisStatus_DL(int ID, int Status, int SampleReceived, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[] { 
                new SqlParameter("@ID", ID),
                new SqlParameter("@Status", Status),
                new SqlParameter("@SampleReceived", SampleReceived),
                new SqlParameter("@UserID", UserID),
                new SqlParameter("return",SqlDbType.Int)
            };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "OPS_SP_UPDATE_BunkerAnalysisStatus", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        public static int UPDATE_BunkerAnalysisStatus_DL(int ID, int Status, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[] { 
                new SqlParameter("@ID", ID),
                new SqlParameter("@Status", Status),
                new SqlParameter("@UserID", UserID),
                new SqlParameter("return",SqlDbType.Int)
            };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "OPS_SP_UPDATE_BunkerAnalysisStatus", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        
        public static DataTable Get_BunkerSampleAttachments_DL(int Sample_ID, int Type, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[] { 
                new SqlParameter("@Sample_ID", Sample_ID),
                new SqlParameter("@Type", Type),
                new SqlParameter("@UserID", UserID)
            };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "OPS_SP_Get_BunkerSampleAttachments", sqlprm).Tables[0];
        }

        public static int Insert_BunkerSampleAttachment_DL(int Sample_ID, int Type, string Attachment_Name, string File_Name, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[] { 
                new SqlParameter("@Sample_ID", Sample_ID),
                new SqlParameter("@Type", Type),
                new SqlParameter("@Attachment_Name", Attachment_Name),
                new SqlParameter("@File_Name", File_Name),
                new SqlParameter("@UserID", UserID),
                new SqlParameter("return",SqlDbType.Int)

            };

            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "OPS_SP_Insert_BunkerSampleAttachment", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        public static int SendMail_ToVessel_DL(int Sample_ID, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[] { 
                new SqlParameter("@Sample_ID", Sample_ID),
                new SqlParameter("@UserID", UserID),
                new SqlParameter("return",SqlDbType.Int)
            };

            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "OPS_SP_SendMail_ToVessel", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        public static int SendMail_ToInternal_DL(int Sample_ID, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[] { 
                new SqlParameter("@Sample_ID", Sample_ID),
                new SqlParameter("@UserID", UserID),
                new SqlParameter("return",SqlDbType.Int)
            };

            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "OPS_SP_SendMail_ToInternal", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        public static int SendMail_AckSampleReceived_DL(int Sample_ID, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[] { 
                new SqlParameter("@Sample_ID", Sample_ID),
                new SqlParameter("@UserID", UserID),
                new SqlParameter("return",SqlDbType.Int)
            };

            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "OPS_SP_SendMail_AckSampleReceived", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        public static DataTable Get_BunkerSupplierList_DL(int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[] { 
                new SqlParameter("@UserID", UserID)
            };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "OPS_SP_Get_BunkerSupplierList", sqlprm).Tables[0];
        }
        public static DataTable Get_LOSupplierList_DL(int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[] { 
                new SqlParameter("@UserID", UserID)
            };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "OPS_SP_Get_LOSupplierList", sqlprm).Tables[0];
        }

        public static DataTable Get_BunkerTestingLabList_DL(int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[] { 
                new SqlParameter("@UserID", UserID)
            };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "OPS_SP_Get_BunkerTestingLabList", sqlprm).Tables[0];
        }
        public static DataTable Get_LOTestingLabList_DL(int UserID)
        {
            int rowcount = 0;
            SqlParameter[] sqlprm = new SqlParameter[] { 
                new SqlParameter("@UserID", UserID),
                new SqlParameter("@ISFETCHCOUNT", rowcount)
            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "OPS_SP_Get_LOTestingLabList", sqlprm).Tables[0];
        }
        public static DataTable GetSupplierInfo(string UserId, string strPassword)
        {
            DataTable dt = new DataTable();
            string Query = "SELECT * FROM PURC_LIB_QUOTATION_USER WHERE USERID = '" + UserId + "'";
            DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.Text, Query);
            dt = ds.Tables[0];
            return dt;

        }

        public static DataTable Get_LOTestingLabList_DL(string SearchText, int CountryID, int UserID, string sortbycoloumn, int? sortdirection, int CurrentPageIndex, int PageSize, ref int rowcount)
        {
            SqlParameter[] sqlprm = new SqlParameter[] { 
                new SqlParameter("@UserID", UserID),
                new SqlParameter("@SearchText", SearchText),
                new SqlParameter("@CountryID", CountryID),
                new SqlParameter("@SORTBY", sortbycoloumn),
                new SqlParameter("@SORTDIRECTION", sortdirection),
                new SqlParameter("@PAGENUMBER", CurrentPageIndex),
                new SqlParameter("@PAGESIZE", PageSize),
                new SqlParameter("@ISFETCHCOUNT", rowcount)               
            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "OPS_SP_Get_LOTestingLabList", sqlprm).Tables[0];
        }
        public static DataTable Get_LOTestingLabByID_DL(int ID, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[] { 
                new SqlParameter("@ID", ID),
                new SqlParameter("@UserID", UserID)
            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "OPS_SP_Get_LOTestingLabByID", sqlprm).Tables[0];
        }
        public static int Insert_LO_Testing_Lab_DL(string Lab_Name, string Address, string EMail, string Phone, int CountryID, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[] { 
                new SqlParameter("@Lab_Name", Lab_Name),
                new SqlParameter("@Address", Address),
                new SqlParameter("@EMail", EMail),
                new SqlParameter("@Phone", Phone),
                new SqlParameter("@CountryID", CountryID),
                new SqlParameter("@UserID", UserID),
                new SqlParameter("return",SqlDbType.Int)

            };

            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "OPS_SP_Insert_LOTestingLab", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        public static int Update_LO_Testing_Lab_DL(int ID, string Lab_Name, string Address, string EMail, string Phone, int CountryID, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[] { 
                new SqlParameter("@ID", ID),
                new SqlParameter("@Lab_Name", Lab_Name),
                new SqlParameter("@Address", Address),
                new SqlParameter("@EMail", EMail),
                new SqlParameter("@Phone", Phone),
                new SqlParameter("@CountryID", CountryID),
                new SqlParameter("@UserID", UserID),
                new SqlParameter("return",SqlDbType.Int)

            };

            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "OPS_SP_Update_LOTestingLab", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        public static int Delete_LO_Testing_Lab_DL(int ID, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[] { 
                new SqlParameter("@ID", ID),
                new SqlParameter("@UserID", UserID),
                new SqlParameter("return",SqlDbType.Int)

            };

            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "OPS_SP_Delete_LOTestingLab", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        public static int Insert_LO_SampleAttachment_DL(int Sample_ID, int Type, string Attachment_Name, string File_Name, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[] { 
                new SqlParameter("@Sample_ID", Sample_ID),
                new SqlParameter("@Type", Type),
                new SqlParameter("@Attachment_Name", Attachment_Name),
                new SqlParameter("@File_Name", File_Name),
                new SqlParameter("@UserID", UserID),
                new SqlParameter("return",SqlDbType.Int)

            };

            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "OPS_SP_Insert_LO_SampleAttachment", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

    }
}
