using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace SMS.Data.Crew
{
    public static class DAL_Crew_CrewMail
    {
        static string connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;

        public static DataTable Get_PendingItems_DL(int FleetCode, int Vessel_ID, int UserID, string SearchText,int? PAGE_SIZE, int? PAGE_INDEX, ref int SelectRecordCount)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@FleetCode",FleetCode),
                                            new SqlParameter("@Vessel_ID",Vessel_ID),
                                            new SqlParameter("@UserID",UserID),
                                            new SqlParameter("@SearchText",SearchText),
                                            new SqlParameter("@PAGE_SIZE",PAGE_SIZE),
                                            new SqlParameter("@PAGE_INDEX",PAGE_INDEX),
                                            new SqlParameter("@SelectRecordCount",SelectRecordCount)                                            
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.InputOutput;
            DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_CM_SP_Get_PendingItems", sqlprm);
            SelectRecordCount = Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);

            return ds.Tables[0];
        }

        public static DataTable Get_PacketItems_Async(int PacketID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@PacketID",PacketID)                                            
                                        };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_CM_SP_Get_PacketItems_Async", sqlprm).Tables[0];
        }

        public static DataSet Get_PacketItems_DL(int FleetCode, int Vessel_ID, int Status, int UserID, string SearchText, int? PAGE_SIZE, int? PAGE_INDEX, ref int SelectRecordCount)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@FleetCode",FleetCode),
                                            new SqlParameter("@Vessel_ID",Vessel_ID),
                                            new SqlParameter("@Status",Status),
                                            new SqlParameter("@UserID",UserID),
                                            new SqlParameter("@SearchText",SearchText),
                                            new SqlParameter("@PAGE_SIZE",PAGE_SIZE),
                                            new SqlParameter("@PAGE_INDEX",PAGE_INDEX),
                                            new SqlParameter("@SelectRecordCount",SelectRecordCount)                                            
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.InputOutput;
            DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_CM_SP_Get_PacketItems", sqlprm);
            SelectRecordCount = Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);

            return ds;
        }
                
        public static DataSet Get_VesselOpenPacket_DL(int Vessel_ID, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Vessel_ID",Vessel_ID),
                                            new SqlParameter("@UserID",UserID)
                                        };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_CM_SP_Get_VesselOpenPacket", sqlprm);
        }
        
        public static DataSet Get_MailPacket_DL(int PacketID, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@PacketID",PacketID),
                                            new SqlParameter("@UserID",UserID)
                                        };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_CM_SP_Get_MailPacket", sqlprm);
        }

        public static int INSERT_CrewMailItem_DL(int Vessel_ID, string Item_Ref, string Item_Desc, decimal Item_Qty, string Item_Remarks, DateTime DatePlaced, int Created_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Vessel_ID",Vessel_ID),
                                            new SqlParameter("@Item_Ref",Item_Ref),
                                            new SqlParameter("@Item_Desc",Item_Desc),
                                            new SqlParameter("@Item_Qty",Item_Qty),
                                            new SqlParameter("@Item_Remarks",Item_Remarks),
                                            new SqlParameter("@DatePlaced",DatePlaced),
                                            new SqlParameter("@Created_By",Created_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_CM_SP_Insert_CrewMailItem", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        public static int DELETE_CrewMailItem_DL(int ID, int Deleted_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ID",ID),
                                            new SqlParameter("@Deleted_By",Deleted_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_CM_SP_DELETE_MailItem", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        public static int Save_MailPacket_DL(int PacketID, DateTime DateSent, string SentUsing, string AirwayBill, int DeliveryPort, DateTime ETA, int Modified_By, string ApproverRemarks)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@PacketID",PacketID),
                                            new SqlParameter("@DateSent",DateSent),
                                            new SqlParameter("@SentUsing",SentUsing),
                                            new SqlParameter("@AirwayBill",AirwayBill),
                                            new SqlParameter("@DeliveryPort",DeliveryPort),
                                            new SqlParameter("@ETA",ETA),
                                            new SqlParameter("@Modified_By",Modified_By),
                                            new SqlParameter("@ApproverRemarks",ApproverRemarks),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_CM_SP_Save_Packet", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        public static int SaveAndSend_MailPacket_DL(int PacketID, DateTime DateSent, string SentUsing, string AirwayBill, int DeliveryPort, DateTime ETA, int Modified_By, string ApproverRemarks)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@PacketID",PacketID),
                                            new SqlParameter("@DateSent",DateSent),
                                            new SqlParameter("@SentUsing",SentUsing),
                                            new SqlParameter("@AirwayBill",AirwayBill),
                                            new SqlParameter("@DeliveryPort",DeliveryPort),
                                            new SqlParameter("@ETA",ETA),
                                            new SqlParameter("@Modified_By",Modified_By),
                                            new SqlParameter("@ApproverRemarks",ApproverRemarks),
                                            new SqlParameter("@SendToShip",1),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_CM_SP_Save_Packet", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        public static int Discard_MailPacket_DL(int PacketID, int Modified_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@PacketID",PacketID),
                                            new SqlParameter("@Modified_By",Modified_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_CM_SP_Discard_MailPacket", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public static int AddItemTo_Packet_DL(int Vessel_ID, int PacketID, int ItemID, int Created_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Vessel_ID",Vessel_ID),
                                            new SqlParameter("@PacketID",PacketID),
                                            new SqlParameter("@ItemID",ItemID),
                                            new SqlParameter("@Created_By",Created_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_CM_SP_AddItemTo_Packet", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        public static int RemoveItem_FromPacket_DL(int ID, int Deleted_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ItemID",ID),
                                            new SqlParameter("@Deleted_By",Deleted_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_CM_SP_RemoveFrom_Packet", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        public static int SendMail_Vessel_DL(int PacketID, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@PacketID",PacketID),
                                            new SqlParameter("@UserID",UserID),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_CM_SP_SendMail_Vessel", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        
        public static DataTable Generate_CoverLetter_DL(int PacketID, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@PacketID",PacketID),
                                            new SqlParameter("@UserID",UserID)
                                        };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_CM_SP_Generate_Coverletter", sqlprm).Tables[0];

            //sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            //SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_CM_SP_Generate_Coverletter", sqlprm);
            //return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
    }
}
