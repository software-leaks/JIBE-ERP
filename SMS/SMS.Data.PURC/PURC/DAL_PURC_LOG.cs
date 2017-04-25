using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace SMS.Data.PURC
{
    public class DAL_PURC_LOG
    {
        static string _internalConnection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;

        public static DataTable Get_Log_PO_List_DL(int Vessel_ID, string Supplier_Code, string Order_Code, int? Page_Index, int? Page_Size, ref int is_Fetch_Count)
        {
            SqlParameter[] prm = new SqlParameter[] {
                                                      new SqlParameter("@Vessel_ID",Vessel_ID),
                                                      new SqlParameter("@Supplier_Code",Supplier_Code),
                                                      new SqlParameter("@Order_Code",Order_Code),

                                                      new SqlParameter("@PAGE_INDEX",Page_Index),
                                                      new SqlParameter("@PAGE_SIZE",Page_Size),
                                                      new SqlParameter("@IS_FETCH_COUNT",is_Fetch_Count),

                                                    };
            prm[prm.Length - 1].Direction = ParameterDirection.InputOutput;

            DataTable dt = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_GET_LOG_PO_LIST", prm).Tables[0];

            is_Fetch_Count = int.Parse(prm[prm.Length - 1].Value.ToString());
            return dt;

        }

        public static int Ins_Log_Create_LogisticPO_DL(int Vessel_ID, DataTable dtPO, int UserID)
        {
            SqlParameter[] prm = new SqlParameter[] {
                                                      new SqlParameter("@Vessel_ID",Vessel_ID),
                                                      new SqlParameter("@TBL_POLIST",dtPO),
                                                      new SqlParameter("@USER_ID",UserID),
                                                      new SqlParameter("@Return",SqlDbType.Int)
                                                     };
            prm[prm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_INS_LOG_Create_LogisticPO", prm);
            return Convert.ToInt32(prm[prm.Length - 1].Value);


        }

        public static int Ins_Log_LogisticPO_Details_DL(int Log_ID, string Currency, string PO_Type, string Cost_Type, string Hub, string Supplier_Code, int Port, string Remark, DataTable dtItems, int UserID, string PO_Supplier)
        {
            SqlParameter[] prm = new SqlParameter[] {
                                                      new SqlParameter("@LOG_ID",Log_ID),
                                                      new SqlParameter("@Currency",Currency),
                                                      new SqlParameter("@PO_Type",PO_Type),
                                                      new SqlParameter("@Cost_Type",Cost_Type),
                                                      new SqlParameter("@Hub",Hub),
                                                      new SqlParameter("@Supplier_Code",Supplier_Code),
                                                      new SqlParameter("@Port",Port),
                                                      new SqlParameter("@Remark",Remark),
                                                      new SqlParameter("@tbl_Items",dtItems),
                                                      new SqlParameter("@UserID",UserID),
                                                      new SqlParameter("@PO_Supplier",PO_Supplier)
                                                     
                                                     };

            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_INS_LOG_Logistic_PO_Details", prm);

        }

        public static DataSet Get_Log_Logistic_PO_Details_DL(int Log_ID)
        {
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_GET_LOG_Logistic_PO_Details", new SqlParameter("@LOG_ID", Log_ID));
        }


        public static DataTable Get_Log_LogisticPO_List_DL(int? Vessel_ID, string Supplier_Code, string Order_Code, int? ShowAllUser, int? ShowActive, int? Page_Index, int? Page_Size, ref int is_Fetch_Count)
        {
            SqlParameter[] prm = new SqlParameter[] {
                                                      new SqlParameter("@Vessel_ID",Vessel_ID),
                                                      new SqlParameter("@Supplier_Code",Supplier_Code),
                                                      new SqlParameter("@Order_Code",Order_Code),
                                                      new SqlParameter("@ShowAllUser",ShowAllUser),
                                                      new SqlParameter("@ShowActive",ShowActive),

                                                      new SqlParameter("@PAGE_INDEX",Page_Index),
                                                      new SqlParameter("@PAGE_SIZE",Page_Size),
                                                      new SqlParameter("@IS_FETCH_COUNT",is_Fetch_Count),

                                                    };
            prm[prm.Length - 1].Direction = ParameterDirection.InputOutput;

            DataTable dt = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_GET_LOG_LogisticPO_List", prm).Tables[0];

            is_Fetch_Count = int.Parse(prm[prm.Length - 1].Value.ToString());
            return dt;

        }
        public static DataTable Get_Log_LogisticPO_List_DL(DataTable Fleet_ID, DataTable Vessel_ID, string Supplier_Code, string Order_Code, int? ShowAllUser, int? ShowActive,string Log_status, int? Page_Index, int? Page_Size, ref int is_Fetch_Count)
        {
            SqlParameter[] prm = new SqlParameter[] {

                                                      new SqlParameter("@Fleet_ID",Fleet_ID),
                                                      new SqlParameter("@Vessel_ID",Vessel_ID),
                                                      new SqlParameter("@Supplier_Code",Supplier_Code),
                                                      new SqlParameter("@Order_Code",Order_Code),
                                                      new SqlParameter("@ShowAllUser",ShowAllUser),
                                                      new SqlParameter("@ShowActive",ShowActive),
                                                       new SqlParameter("@Log_status",Log_status),

                                                      new SqlParameter("@PAGE_INDEX",Page_Index),
                                                      new SqlParameter("@PAGE_SIZE",Page_Size),
                                                      new SqlParameter("@IS_FETCH_COUNT",is_Fetch_Count),

                                                    };
            prm[prm.Length - 1].Direction = ParameterDirection.InputOutput;

            DataTable dt = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_GET_LOG_LogisticPO_List", prm).Tables[0];

            is_Fetch_Count = int.Parse(prm[prm.Length - 1].Value.ToString());
            return dt;

        }
        public static int Upd_Log_LogisticPO_Item_DL(int Item_ID, int User_ID)
        {
            SqlParameter[] prm = new SqlParameter[] {
                                                      new SqlParameter("@Item_ID",Item_ID),
                                                      new SqlParameter("@User_ID",User_ID)
                                                     };
            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_UPD_LOG_LogisticPO_Item", prm);
        }

        public static DataTable Get_Log_Logistic_Approver_DL()
        {
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_GET_LOG_Logistic_Approver").Tables[0];
        }
        public static decimal Get_Log_Logistic_Approval_Limit_DL(int User_ID)
        {
            return Convert.ToDecimal(SqlHelper.ExecuteScalar(_internalConnection, CommandType.StoredProcedure, "PURC_GET_LOG_Logistic_APPROVAL_LIMIT", new SqlParameter("@LogedInUserID", User_ID)));
        }
        public static int Ins_Log_Logistic_Approval_Entry_DL(int LogedInUser_ID, int Approver_ID, decimal Approval_Amount, int Log_ID, string Purchaser_Remark, string Approver_Remark, int IsFinalApproval)
        {


            SqlParameter[] pList = {    new SqlParameter("@LogInUserID", LogedInUser_ID),
                                        new SqlParameter("@ApproverID", Approver_ID),
                                        new SqlParameter("@ApprovalAmt", Approval_Amount),
                                        new SqlParameter("@Log_ID",Log_ID),
                                        new SqlParameter("@Purchaser_Remark",Purchaser_Remark),
                                        new SqlParameter("@Approver_Remark",Approver_Remark),
                                        new SqlParameter("@IsFinalApproval",IsFinalApproval)
                                       };
            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_INS_LOG_Logistic_Approvals_Entry", pList);
        }

        public static DataTable Get_Log_Logistic_Approvals_DL(int Log_ID)
        {
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_GET_LOG_Logistic_Approvals", new SqlParameter("Log_ID", Log_ID)).Tables[0];
        }

        public static int Upd_Log_Delete_Logistic_ReqsnPO_DL(int ID, int User_ID)
        {
            SqlParameter[] SPrm = {    new SqlParameter("@ID", ID),
                                        new SqlParameter("@User_ID", User_ID)
                                     };

            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_UPD_LOG_Delete_Logistic_ReqsnPO", SPrm);
        }

        public static int Upd_Log_Delete_LogisticPO_DL(int Log_ID, int User_ID)
        {
            SqlParameter[] SPrm = {    new SqlParameter("@Log_ID", Log_ID),
                                        new SqlParameter("@User_ID", User_ID)
                                     };
            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_UPD_LOG_Delete_LogisticPO", SPrm);
        }

        public static DataTable Get_Log_Remark_DL(int Log_ID, int? Remark_Type)
        {
            SqlParameter[] SPrm = {    new SqlParameter("@Log_ID", Log_ID),
                                      new SqlParameter("@Remark_Type", Remark_Type)
                                  };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_GET_LOG_Remark", SPrm).Tables[0];
        }


        public static int Ins_Log_Remark_DL(int Log_ID, string Remark, int User_ID, int Remark_Type)
        {
            SqlParameter[] SPrm = {    new SqlParameter("@Log_ID", Log_ID),
                                      new SqlParameter("@Remark", Remark),
                                       new SqlParameter("@User_ID", User_ID),
                                       new SqlParameter("@Remark_Type", Remark_Type),

                                  };
            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_INS_LOG_Remark", SPrm);
        }

        public static DataTable Get_Log_Hub_List_DL()
        {
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_GET_LOG_HubList").Tables[0];
        }

        public static DataTable Get_Log_AgentList_ByHub_DL(string Hub_Code)
        {
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_GET_LOG_AgentList_ByHub", new SqlParameter("@Hub_Code", Hub_Code)).Tables[0];
        }

        public static DataTable Get_VesselInLogisticPO_DL(int LOG_ID)
        {
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_GET_LOG_VesselInLogistic", new SqlParameter("@LOG_ID", LOG_ID)).Tables[0];
        }

        public static int Upd_Log_ReworkToPurchaser_DL(int Log_ID, int User_ID)
        { 
            SqlParameter[] SPrm = {    new SqlParameter("@Log_ID", Log_ID),
                                        new SqlParameter("@User_ID", User_ID)
                                     };
            return SqlHelper.ExecuteNonQuery(_internalConnection,CommandType.StoredProcedure,"PURC_UPD_LOG_ReworkToPurchaser",SPrm);
        }

        public static int Upd_Log_Cancel_LPO_DL(int Log_ID, int User_ID)
        {
            SqlParameter[] SPrm = {    new SqlParameter("@Log_ID", Log_ID),
                                        new SqlParameter("@User_ID", User_ID),
                                        new SqlParameter("@return",SqlDbType.Int)
                                     };

            SPrm[SPrm.Length-1].Direction = ParameterDirection.ReturnValue;

             SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_UPD_LOG_Cancel_LPO", SPrm);

             return Convert.ToInt32(SPrm[SPrm.Length - 1].Value);

        }

        public static int Upd_Log_Order_Details_DL(string Dlvinstruction, string DlvPort, DateTime ETA, string Remark, DateTime ETD, string AgentDTL, string DOCUMENT_CODE, string ORDER_CODE, int modified_by)
        {
            SqlParameter[] SPrm = {    new SqlParameter("@Dlvinstruction", Dlvinstruction),
                                       new SqlParameter("@DlvPort", DlvPort),
                                       new SqlParameter("@ETA", ETA),
                                       new SqlParameter("@Remark", Remark),
                                       new SqlParameter("@ETD", ETD),
                                       new SqlParameter("@AgentDTL", AgentDTL),
                                       new SqlParameter("@DOCUMENT_CODE", DOCUMENT_CODE),
                                       new SqlParameter("@ORDER_CODE", ORDER_CODE),
                                       new SqlParameter("@modified_by", modified_by),
                                     };
            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_UPD_LOG_Order_Details", SPrm);
        }

        public static DataTable Get_Log_Raise_PO_DL(string Order_Code, string DocumentCode)
        {
            SqlParameter[] SPrm = {    new SqlParameter("@Order_Code", Order_Code),
                                        new SqlParameter("@DocumentCode", DocumentCode)
                                     };

            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_GET_LOG_RAISE_PO", SPrm).Tables[0];
        }

        public static DataSet Get_RaisePO_EmailInfo_DL(string Order_Code, string Document_Code, int UserID)
        {
            SqlParameter[] SPrm = {    new SqlParameter("@ORDER_CODE", Order_Code),
                                       new SqlParameter("@Document_code", Document_Code),
                                       new SqlParameter("@UserID",UserID)
                                     };

            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_GET_LOG_RaisePO_EmailInfo", SPrm);
        }


        public static DataTable Get_Log_POList_Raise_DL(int Log_ID)
        {
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_GET_LOG_POList_Raise", new SqlParameter("@LOG_ID", Log_ID)).Tables[0];
        }

        public static DataTable Get_Log_Attachment_DL(string Log_ID)
        {
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_GET_LOG_Attachment", new SqlParameter("@LOG_ID", Log_ID)).Tables[0];
        }

        public static DataTable Get_Log_Deleted_LPO_DL(string Log_ID)
        {
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_GET_LOG_Deleted_LPO", new SqlParameter("@LOG_ID", Log_ID)).Tables[0];
        }

        public static DataTable Get_LogisticPOList_OutLook_DL(int Vessel_ID, string Supplier_Code, string SearchText)
        {
            SqlParameter[] spm = new SqlParameter[] { new SqlParameter("@Vessel_ID", Vessel_ID),
                                                      new SqlParameter("@Supplier_Code",Supplier_Code),
                                                      new SqlParameter("@SearchText",SearchText)

                                                    };

            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_GET_LOG_LogisticPO_List_OutLook", spm).Tables[0];
        }

        public static DataTable Get_LogisticPO_Supplier_DL()
        {
            return SqlHelper.ExecuteDataset(_internalConnection,CommandType.StoredProcedure,"PURC_GET_LOG_LogisticPO_Supplier").Tables[0];
        }
        public static DataTable SearchPOList_DL(string searchtext
       , string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@SerchText", searchtext),
                
               
                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                    
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_SP_POList_Search", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds.Tables[0];
        }


        public static DataTable get_Vessels_DL()
        {
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_SP_GetVessels").Tables[0];
        }   

        public static DataTable Get_POnumberByID_DL(int id)
        {
            SqlParameter[] spm = new SqlParameter[] { new SqlParameter("@Id", id) };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_SP_POList_ByID",spm).Tables[0];
        }



        public static int InsertPONo_DL(string pONO, decimal amt, int vesselId, string supplier, DateTime? orderDT, int CreatedBy)
        {
            SqlParameter[] SPrm = {   new SqlParameter("@PO_Number", pONO ),
                                       new SqlParameter("@PO_Amount", amt),
                                       new SqlParameter("@Vessel_Id", vesselId),
                                       new SqlParameter("@Supplier_name", supplier),
                                       new SqlParameter("@Order_Date", orderDT),
                                       new SqlParameter("@Created_By", CreatedBy)                                  
                                     };
            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_INSERT_PO_NUMBER", SPrm);
        }

        public static int EditPONo_DL(int Id, string pONO, decimal amt, int vesselId, string supplier, DateTime? orderDT, int Modified_By)
        {
            SqlParameter[] SPrm = {  new SqlParameter("@Id", Id ),
                                      new SqlParameter("@PO_Number", pONO ),
                                       new SqlParameter("@PO_Amount", amt),
                                       new SqlParameter("@Vessel_Id", vesselId),
                                       new SqlParameter("@Supplier_name", supplier),
                                       new SqlParameter("@Order_Date", orderDT),
                                       new SqlParameter("@Modified_By", Modified_By)                                  
                                     };
            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_UPDATE_PO_NUMBER", SPrm);
        }
        public static int DeletePoNumbe_DL(int Id,int Deleted_By) 
        {
            SqlParameter[] SPrm = {  new SqlParameter("@Id", Id ),                                   
                                       new SqlParameter("@Deleted_By", Deleted_By)                                  
                                     };
            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_DELETE_PO_NUMBER", SPrm);
        }
        public static DataTable GetPOItems_DL(int pOId)
        {
            SqlParameter[] spm = new SqlParameter[] { new SqlParameter("@PO_Id", pOId) };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_SP_Get_POItemList", spm).Tables[0];
        }

        public static int InsertPONo_Item_DL(int pOID, string itemDescription, decimal itemPrice, int itemQtn, string itemUnit, int Created_By)
        {
            SqlParameter[] SPrm = {    new SqlParameter("@pOID", pOID),
                                       new SqlParameter("@itemDescription", itemDescription),
                                       new SqlParameter("@itemPrice", itemPrice),
                                       new SqlParameter("@itemQtn", itemQtn),
                                       new SqlParameter("@itemUnit", itemUnit),
                                       new SqlParameter("@Created_By", Created_By)                                  
                                     };
            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_INSERT_PO_Item", SPrm);
        }
        public static int UpdatePOItem_DL(int itemID, string itemDescription, decimal itemPrice, int itemQtn, string itemUnit, int Modified_By)
        {
            SqlParameter[] SPrm = {    new SqlParameter("@Item_ID", itemID),
                                       new SqlParameter("@itemDescription", itemDescription),
                                       new SqlParameter("@itemPrice", itemPrice),
                                       new SqlParameter("@itemQtn", itemQtn),
                                       new SqlParameter("@itemUnit", itemUnit),
                                       new SqlParameter("@Modified_By", Modified_By)                                  
                                     };
            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_UPDATE_PO_ITEM", SPrm);
        }

        public static int DeleteItem_DL(int Id, int Deleted_By) 
        {
            SqlParameter[] SPrm = {  new SqlParameter("@Id", Id ),                                   
                                       new SqlParameter("@Deleted_By", Deleted_By)                                  
                                     };
            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_DELETE_PO_Items", SPrm);
        }
    }
}
