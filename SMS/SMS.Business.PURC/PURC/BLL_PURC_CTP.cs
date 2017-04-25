using System.Data;
using SMS.Data.PURC;
using System;

namespace SMS.Business.PURC
{
    public class BLL_PURC_CTP
    {
        public static DataTable Get_Ctp_Items(string System_Code, string SubSystem_Code, string Item_Search, DataTable dtSelected_Items, DataTable dtSelected_SubCatalogue, DataTable dtDeSelected_SubCatalogue_items, int isSelected, int? Quotation_ID, int? PageNumber, int? PageSize, ref int isFetchCount, int Contract_ID = 0)
        {
            return DAL_PURC_CTP.Get_Ctp_Items_DL(System_Code, SubSystem_Code, Item_Search, dtSelected_Items, dtSelected_SubCatalogue, dtDeSelected_SubCatalogue_items, isSelected, Quotation_ID, Contract_ID, PageNumber, PageSize, ref  isFetchCount);
        }

        public static int Insert_Ctp_CreateNewContract(string System_Code, int Department_ID, DataTable dtSelected_Items, DataTable dtSelected_SubCatalogue, DataTable dtDeSelected_SubCatalogue_items, int Created_by)
        {
            return DAL_PURC_CTP.Insert_Ctp_CreateNewContract_DL(System_Code, Department_ID, dtSelected_Items, dtSelected_SubCatalogue, dtDeSelected_SubCatalogue_items, Created_by);
        }

        public static DataTable Ins_Ctp_SendRFQ(int Contract_ID, DataTable dtSuppliers, int Created_By)
        {
            return DAL_PURC_CTP.Ins_Ctp_SendRFQ_DL(Contract_ID, dtSuppliers, Created_By);
        }

        public static DataTable Get_Ctp_Contract_List(string Supplier_Code, int? Dept_ID, DateTime? Effdt_From, DateTime? Effdt_To, string Qtn_status, int? Contract_Status, int? Port_ID, string Search_Item, int? Page_Index, int? Page_Size, ref int is_Fetch_Count)
        {
            return DAL_PURC_CTP.Get_Ctp_Contract_List_DL(Supplier_Code, Dept_ID, Effdt_From, Effdt_To, Qtn_status, Contract_Status, Port_ID, Search_Item, Page_Index, Page_Size, ref  is_Fetch_Count);
        }

        public static DataTable Get_Ctp_Contract_Details(int Quotation_ID, int Approval_status, string Search_Item, string SubCatalogue, int? Page_Index, int? Page_Size, ref int is_Fetch_Count)
        {
            return DAL_PURC_CTP.Get_Ctp_Contract_Details_DL(Quotation_ID, Approval_status, Search_Item, SubCatalogue, Page_Index, Page_Size, ref  is_Fetch_Count);

        }

        public static DataTable Get_Ctp_Contract_Info(int Quotation_ID, int Contract_ID = 0)
        {
            return DAL_PURC_CTP.Get_Ctp_Contract_Info_DL(Quotation_ID, Contract_ID);
        }


        public static void Upd_Ctp_Items_Price(int Quotation_ID, DataTable dtPrice, int UserID, int IsFinalize, DataTable dtCharges)
        {

            DAL_PURC_CTP.Upd_Ctp_Items_Price_DL(Quotation_ID, dtPrice, UserID, IsFinalize, dtCharges);
        }

        public static int Insert_Ctp_QuotationItems(int Quotation_ID, DataTable dtSelected_Items, DataTable dtSelected_SubCatalogue, DataTable dtDeSelected_SubCatalogue_items, int Created_by, int Contract_ID = 0)
        {
            return DAL_PURC_CTP.Insert_Ctp_QuotationItems_DL(Quotation_ID, dtSelected_Items, dtSelected_SubCatalogue, dtDeSelected_SubCatalogue_items, Created_by, Contract_ID);

        }

        public static int Update_Ctp_Remove_QtnItem(int Qtn_Item_ID, int Quotation_ID, int UserID)
        {
            return DAL_PURC_CTP.Update_Ctp_Remove_QtnItem_DL(Qtn_Item_ID, Quotation_ID, UserID);
        }


        public static int Update_Ctp_QtnItem_Remark(int User_Type, string Remark, int UserID, int Qtn_Item_ID)
        {
            return DAL_PURC_CTP.Update_Ctp_QtnItem_Remark_DL(User_Type, Remark, UserID, Qtn_Item_ID);
        }

        public static int Update_Ctp_ReworkToSupplier(int UserID, int Quotation_ID)
        {
            return DAL_PURC_CTP.Update_Ctp_ReworkToSupplier_DL(UserID, Quotation_ID);
        }

        public static DataSet Get_Ctp_Supplier_Mail(int Quotation_ID)
        {
            return DAL_PURC_CTP.Get_Ctp_Supplier_Mail_DL(Quotation_ID);
        }

        public static DataSet Get_Ctp_RFQ_Items(int Quotation_ID)
        {
            return DAL_PURC_CTP.Get_Ctp_RFQ_Items_DL(Quotation_ID);
        }

        public static DataTable Get_Ctp_Qtn_Eval_Supplier(DataTable dtQuotation_ID)
        {
            return DAL_PURC_CTP.Get_Ctp_Qtn_Eval_Supplier_DL(dtQuotation_ID);
        }

        public static DataTable Get_Ctp_Qtn_Eval_Item(DataTable dtQuotation_ID, string Search_Item, string Search_Subcatalogue, int? QtnID_Quotedtems, int? Page_Index, int? Page_Size, ref int Is_fetch_count, int? Sort_Direction, string Sort_Column)
        {
            return DAL_PURC_CTP.Get_Ctp_Qtn_Eval_Item_DL(dtQuotation_ID, Search_Item, Search_Subcatalogue, QtnID_Quotedtems, Page_Index, Page_Size, ref Is_fetch_count, Sort_Direction, Sort_Column);
        }

        public static int Upd_Ctp_Approve_Contract(int Approver_ID, DataTable dtQuotation, DateTime Effective_Date, DateTime Expiry_Date, string Approver_Remark)
        {
            return DAL_PURC_CTP.Upd_Ctp_Approve_Contract_DL(Approver_ID, dtQuotation, Effective_Date, Expiry_Date, Approver_Remark);
        }

        public static DataTable Get_Ctp_Non_Contract_Items(DateTime? Ord_DT_From, DateTime? Ord_DT_To, string Subcatalogue, string Catalogue, int? Page_Index, int? Page_Size, ref int Is_fetch_count, int? Sort_Direction, string Sort_Column)
        {
            return DAL_PURC_CTP.Get_Ctp_Non_Contract_Items_DL(Ord_DT_From, Ord_DT_To, Subcatalogue, Catalogue, Page_Index, Page_Size, ref  Is_fetch_count, Sort_Direction, Sort_Column);
        }

        public static DataTable Get_Ctp_Contract_List_ByCatalogue(string Catalogue_Code, int? Port_ID, string Supplier_Code, string Quotation_ID)
        {
            return DAL_PURC_CTP.Get_Ctp_Contract_List_ByCatalogue_DL(Catalogue_Code, Port_ID, Supplier_Code, Quotation_ID);
        }


        public static int UPD_Ctp_Delete_Contract(int Quoation_ID,int Contract_ID, int UserId)
        {
            return DAL_PURC_CTP.UPD_Ctp_Delete_Contract_DL(Quoation_ID,Contract_ID, UserId);
        }

        public static int INS_CTP_Copy_Contract(int Quotation_ID, int Created_By, string Remark, DataTable dtDept_Catalogue)
        {
            return DAL_PURC_CTP.INS_CTP_Copy_Contract(Quotation_ID, Created_By, Remark, dtDept_Catalogue);
        }

        public static int UPD_CTP_Recall_Approved_Contract(int Quotation_ID, int User_ID, string Remark)
        {
            return DAL_PURC_CTP.UPD_CTP_Recall_Approved_Contract(Quotation_ID, User_ID, Remark);
        }
    }
}
