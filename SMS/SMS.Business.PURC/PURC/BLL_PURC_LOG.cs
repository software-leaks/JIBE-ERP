using System;
using System.Data;
using SMS.Data.PURC;

namespace SMS.Business.PURC
{
    public class BLL_PURC_LOG
    {
        public static DataTable Get_Log_PO_List(int Vessel_ID, string Supplier_Code, string Order_Code, int? Page_Index, int? Page_Size, ref int is_Fetch_Count)
        {
            return DAL_PURC_LOG.Get_Log_PO_List_DL(Vessel_ID, Supplier_Code, Order_Code, Page_Index, Page_Size, ref is_Fetch_Count);

        }

        public static int Ins_Log_Create_LogisticPO(int Vessel_ID, DataTable dtPO, int UserID)
        {
            return DAL_PURC_LOG.Ins_Log_Create_LogisticPO_DL(Vessel_ID, dtPO, UserID);
        }

        public static int Ins_Log_LogisticPO_Details(int Log_ID, string Currency, string PO_Type, string Cost_Type, string Hub, string Supplier_Code, int Port, string Remark, DataTable dtItems, int UserID, string PO_Supplier)
        {
            return DAL_PURC_LOG.Ins_Log_LogisticPO_Details_DL(Log_ID, Currency, PO_Type, Cost_Type, Hub, Supplier_Code, Port, Remark, dtItems, UserID, PO_Supplier);
        }

        public static DataSet Get_Log_Logistic_PO_Details(int Log_ID)
        {
            return DAL_PURC_LOG.Get_Log_Logistic_PO_Details_DL(Log_ID);
        }


        public static DataTable Get_Log_LogisticPO_List(int? Vessel_ID, string Supplier_Code, string Order_Code, int? ShowAllUser, int? ShowActive, int? Page_Index, int? Page_Size, ref int is_Fetch_Count)
        {
            return DAL_PURC_LOG.Get_Log_LogisticPO_List_DL(Vessel_ID, Supplier_Code, Order_Code, ShowAllUser, ShowActive, Page_Index, Page_Size, ref is_Fetch_Count);

        }

        public static DataTable Get_Log_LogisticPO_List(DataTable Fleet_ID, DataTable Vessel_ID, string Supplier_Code, string Order_Code, int? ShowAllUser, int? ShowActive,string Log_status, int? Page_Index, int? Page_Size, ref int is_Fetch_Count)
        {
            return DAL_PURC_LOG.Get_Log_LogisticPO_List_DL(Fleet_ID, Vessel_ID, Supplier_Code, Order_Code, ShowAllUser, ShowActive, Log_status, Page_Index, Page_Size, ref is_Fetch_Count);

        }

        public static int Upd_Log_LogisticPO_Item(int Item_ID, int User_ID)
        {
            return DAL_PURC_LOG.Upd_Log_LogisticPO_Item_DL(Item_ID, User_ID);
        }
        public static DataTable Get_Log_Logistic_Approver()
        {
            return DAL_PURC_LOG.Get_Log_Logistic_Approver_DL();
        }

        public static decimal Get_Log_Logistic_Approval_Limit(int User_ID)
        {
            return DAL_PURC_LOG.Get_Log_Logistic_Approval_Limit_DL(User_ID);
        }
        public static int Ins_Log_Logistic_Approval_Entry(int LogedInUser_ID, int Approver_ID, decimal Approval_Amount, int Log_ID, string Purchaser_Remark, string Approver_Remark, int IsFinalApproval = 0)
        {
            return DAL_PURC_LOG.Ins_Log_Logistic_Approval_Entry_DL(LogedInUser_ID, Approver_ID, Approval_Amount, Log_ID, Purchaser_Remark, Approver_Remark, IsFinalApproval);
        }

        public static DataTable Get_Log_Logistic_Approvals(int Log_ID)
        {
            return DAL_PURC_LOG.Get_Log_Logistic_Approvals_DL(Log_ID);
        }

        public static int Upd_Log_Delete_Logistic_ReqsnPO(int ID, int User_ID)
        {
            return DAL_PURC_LOG.Upd_Log_Delete_Logistic_ReqsnPO_DL(ID, User_ID);
        }

        public static int Upd_Log_Delete_LogisticPO(int Log_ID, int User_ID)
        {
            return DAL_PURC_LOG.Upd_Log_Delete_LogisticPO_DL(Log_ID, User_ID);
        }

        public static DataTable Get_Log_Remark(int Log_ID, int? Remark_Type)
        {
            return DAL_PURC_LOG.Get_Log_Remark_DL(Log_ID, Remark_Type);
        }

        public static int Ins_Log_Remark(int Log_ID, string Remark, int User_ID, int Remark_Type)
        {
            return DAL_PURC_LOG.Ins_Log_Remark_DL(Log_ID, Remark, User_ID, Remark_Type);
        }

        public static DataTable Get_Log_Hub_List()
        {
            return DAL_PURC_LOG.Get_Log_Hub_List_DL();
        }

        public static DataTable Get_Log_AgentList_ByHub(string Hub_Code)
        {
            return DAL_PURC_LOG.Get_Log_AgentList_ByHub_DL(Hub_Code);
        }

        public static DataTable Get_VesselInLogisticPO(int LOG_ID)
        {
            return DAL_PURC_LOG.Get_VesselInLogisticPO_DL(LOG_ID);
        }

        public static int Upd_Log_ReworkToPurchaser(int Log_ID, int User_ID)
        {
            return DAL_PURC_LOG.Upd_Log_ReworkToPurchaser_DL(Log_ID, User_ID);
        }

        public static int Upd_Log_Cancel_LPO(int Log_ID, int User_ID)
        {
            return DAL_PURC_LOG.Upd_Log_Cancel_LPO_DL(Log_ID, User_ID);
        }

        public static int Upd_Log_Order_Details(string Dlvinstruction, string DlvPort, DateTime ETA, string Remark, DateTime ETD, string AgentDTL, string DOCUMENT_CODE, string ORDER_CODE, int modified_by)
        {
            return DAL_PURC_LOG.Upd_Log_Order_Details_DL(Dlvinstruction, DlvPort, ETA, Remark, ETD, AgentDTL, DOCUMENT_CODE, ORDER_CODE, modified_by);
        }

        public static DataTable Get_Log_Raise_PO(string Order_Code, string DocumentCode)
        {
            return DAL_PURC_LOG.Get_Log_Raise_PO_DL(Order_Code, DocumentCode);
        }

        public static DataSet Get_RaisePO_EmailInfo(string Order_Code, string Document_Code, int UserID)
        {
            return DAL_PURC_LOG.Get_RaisePO_EmailInfo_DL(Order_Code, Document_Code, UserID);
        }

        public static DataTable Get_Log_POList_Raise(int Log_ID)
        {
            return DAL_PURC_LOG.Get_Log_POList_Raise_DL(Log_ID);
        }
        public static DataTable Get_Log_Attachment(string Log_ID)
        {
            return DAL_PURC_LOG.Get_Log_Attachment_DL(Log_ID);
        }

        public static DataTable Get_Log_Deleted_LPO(string Log_ID)
        {
            return DAL_PURC_LOG.Get_Log_Deleted_LPO_DL(Log_ID);
        }

        public static DataTable Get_LogisticPOList_OutLook(int Vessel_ID, string Supplier_Code, string SearchText)
        {
            return DAL_PURC_LOG.Get_LogisticPOList_OutLook_DL(Vessel_ID, Supplier_Code, SearchText);
        }

        public static DataTable Get_LogisticPO_Supplier()
        {
            return DAL_PURC_LOG.Get_LogisticPO_Supplier_DL();
        }

        public DataTable SearchPOList(string searchtext, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {

            return DAL_PURC_LOG.SearchPOList_DL(searchtext, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);

        }

        public DataTable get_Vessels()
        {
            return DAL_PURC_LOG.get_Vessels_DL();
        }    

        public DataTable Get_POnumberByID(int id)
        {
            return DAL_PURC_LOG.Get_POnumberByID_DL(id);
        }

        public int InsertPONo(string pONO, decimal amt, int vesselId, string supplier, DateTime?  orderDT,  int Created_By)
        {

            return DAL_PURC_LOG.InsertPONo_DL(pONO, amt, vesselId, supplier, orderDT, Created_By);

        }

        public int EditPONo(int Id, string pONO, decimal amt, int vesselId, string supplier, DateTime? orderDT, int Modified_By)
        {

            return DAL_PURC_LOG.EditPONo_DL(Id, pONO, amt, vesselId, supplier, orderDT, Modified_By);

        }

        public int DeletePoNumbe(int Id,int Deleted_By)
        {

            return DAL_PURC_LOG.DeletePoNumbe_DL(Id, Deleted_By);

        }
        public int InsertPOItem(int pOID, string itemDescription, decimal itemPrice, int itemQtn, string itemUnit,int Created_By)
        {

            return DAL_PURC_LOG.InsertPONo_Item_DL( pOID,  itemDescription,  itemPrice,  itemQtn,  itemUnit, Created_By);

        }

        public DataTable GetPOItems(int _pONO)
        {
            return DAL_PURC_LOG.GetPOItems_DL(_pONO);
        }
        public int UpdatePOItem(int itemID, string itemDescription, decimal itemPrice, int itemQtn, string itemUnit, int Modified_By)
        {

            return DAL_PURC_LOG.UpdatePOItem_DL(itemID, itemDescription, itemPrice, itemQtn, itemUnit, Modified_By);

        }

        public int DeletePoItem(int Id, int Deleted_By)
        {

            return DAL_PURC_LOG.DeleteItem_DL(Id, Deleted_By);

        }


    }
}
