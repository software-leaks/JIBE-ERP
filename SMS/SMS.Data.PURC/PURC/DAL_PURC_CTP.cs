using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace SMS.Data.PURC
{
    public class DAL_PURC_CTP
    {
        static string _internalConnection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;

        public static DataTable Get_Ctp_Items_DL(string System_Code, string SubSystem_Code, string Item_Search, DataTable dtSelected_Items, DataTable dtSelected_SubCatalogue, DataTable dtDeSelected_SubCatalogue_items, int isSelected, int? Quotation_ID, int Contract_ID, int? PageNumber, int? PageSize, ref int isFetchCount)
        {
            SqlParameter[] prm = new SqlParameter[]
            {
                new SqlParameter("@SYSTEMCODE",System_Code),
                new SqlParameter("@SUBSYSTEMCODE",SubSystem_Code),
                new SqlParameter("@ITEM_SEARCH",Item_Search),
                new SqlParameter("@TBL_SELECTED_ITEM_REF_CODE",dtSelected_Items),
                new SqlParameter("@TBL_SELECTED_SubCatalogue",dtSelected_SubCatalogue),
                new SqlParameter("@TBL_DeSelected_SubCatalogue_items",dtDeSelected_SubCatalogue_items),
                new SqlParameter("@IS_SELECTED",isSelected),
                new SqlParameter("@Quotation_ID",Quotation_ID),
                 new SqlParameter("@Contract_ID",Contract_ID),
                new SqlParameter("@PAGENUMBER",PageNumber),
                new SqlParameter("@PAGESIZE",PageSize),
                new SqlParameter("@ISFETCHCOUNT",isFetchCount)
            };
            prm[prm.Length - 1].Direction = ParameterDirection.InputOutput;
            DataTable dt = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_GET_CTP_ITEMS", prm).Tables[0];
            isFetchCount = Convert.ToInt32(prm[prm.Length - 1].Value.ToString());
            return dt;
        }



        public static int Insert_Ctp_CreateNewContract_DL(string System_Code, int Department_ID, DataTable dtSelected_Items, DataTable dtSelected_SubCatalogue, DataTable dtDeSelected_SubCatalogue_items, int Created_by)
        {
            SqlParameter[] prm = new SqlParameter[]
            {
                new SqlParameter("@SYSTEMCODE",System_Code),
                new SqlParameter("@Department",Department_ID),
                new SqlParameter("@TBL_SELECTED_ITEM_REF_CODE",dtSelected_Items),
                new SqlParameter("@TBL_SELECTED_SubSystemCode",dtSelected_SubCatalogue),
                new SqlParameter("@TBL_DeSelected_SubCatalogue_items",dtDeSelected_SubCatalogue_items),
                new SqlParameter("@CREATED_BY",Created_by),
                new SqlParameter("@return",SqlDbType.Int)
            };
            prm[prm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_INS_CTP_CreateNewContract", prm);
            return Convert.ToInt32(prm[prm.Length - 1].Value.ToString());

        }

        public static DataTable Ins_Ctp_SendRFQ_DL(int Contract_ID, DataTable dtSuppliers, int Created_By)
        {
            SqlParameter[] prm = new SqlParameter[]
            {
                new SqlParameter("@Contract_ID",Contract_ID),
                new SqlParameter("@Tbl_Supplier",dtSuppliers),
                new SqlParameter("@CREATED_BY",Created_By),
                
            };

            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_INS_CTP_SendRFQ", prm).Tables[0];



        }

        public static DataTable Get_Ctp_Contract_List_DL(string Supplier_Code, int? Dept_ID, DateTime? Effdt_From, DateTime? Effdt_To, string Qtn_status, int? Contract_Status, int? Port_ID, string Search_Item, int? Page_Index, int? Page_Size, ref int is_Fetch_Count)
        {
            SqlParameter[] prm = new SqlParameter[]
            {
                new SqlParameter("@Supplier_Code",Supplier_Code),
                new SqlParameter("@Dept_ID",Dept_ID),
                new SqlParameter("@Effdt_From",Effdt_From),
                new SqlParameter("@Effdt_To",Effdt_To),
                new SqlParameter("@Qtn_status",Qtn_status),
                new SqlParameter("@Contract_Status",Contract_Status),
                new SqlParameter("@Port_ID",Port_ID),
                new SqlParameter("@Search_Item",Search_Item),
                
                new SqlParameter("@Page_Index",Page_Index),
                new SqlParameter("@Page_Size",Page_Size),
                new SqlParameter("@is_Fetch_Count",is_Fetch_Count)
            };
            prm[prm.Length - 1].Direction = ParameterDirection.InputOutput;
            DataTable dt = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_GET_CTP_CONTRACTLIST", prm).Tables[0];
            is_Fetch_Count = Convert.ToInt32(prm[prm.Length - 1].Value.ToString());
            return dt;
        }

        public static DataTable Get_Ctp_Contract_Details_DL(int Quotation_ID, int Approval_status, string Search_Item, string SubCatalogue, int? Page_Index, int? Page_Size, ref int is_Fetch_Count)
        {
            SqlParameter[] prm = new SqlParameter[]
            {
                new SqlParameter("@Quotation_ID",Quotation_ID),
                new SqlParameter("@Approval_status",Approval_status),
                new SqlParameter("@SubCatalogue",SubCatalogue),
                new SqlParameter("@Search_Item",Search_Item),
                
                new SqlParameter("@Page_Index",Page_Index),
                new SqlParameter("@Page_Size",Page_Size),
                new SqlParameter("@is_Fetch_Count",is_Fetch_Count)
            };
            prm[prm.Length - 1].Direction = ParameterDirection.InputOutput;
            DataTable dt = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_GET_CTP_CONTRACT_DETAILS", prm).Tables[0];
            is_Fetch_Count = Convert.ToInt32(prm[prm.Length - 1].Value.ToString());
            return dt;
        }

        public static DataTable Get_Ctp_Contract_Info_DL(int Quotation_ID, int Contract_ID)
        {
            SqlParameter[] prm = new SqlParameter[]
            {
                new SqlParameter("@Quotation_ID",Quotation_ID),
                new SqlParameter("@Contract_ID",Contract_ID),
             
            };

            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_GET_CTP_CONTRACT_INFO", prm).Tables[0];


        }

        public static void Upd_Ctp_Items_Price_DL(int Quotation_ID, DataTable dtPrice, int UserID, int IsFinalize, DataTable dtCharges)
        {
            SqlParameter[] prm = new SqlParameter[]
            {
                new SqlParameter("@Quotation_ID",Quotation_ID),
                new SqlParameter("@Tbl_Price",dtPrice),
                new SqlParameter("@CREATED_BY",UserID),
                new SqlParameter("@IsFinalize",IsFinalize),
                new SqlParameter("@Tbl_Charges",dtCharges)
             
            };

            SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_UPD_CTP_ITEMS_RATE", prm);

        }

        public static int Insert_Ctp_QuotationItems_DL(int Quotation_ID, DataTable dtSelected_Items, DataTable dtSelected_SubCatalogue, DataTable dtDeSelected_SubCatalogue_items, int Created_by, int Contract_ID)
        {
            SqlParameter[] prm = new SqlParameter[]
            {
               
                new SqlParameter("@Quotation_ID",Quotation_ID),
                 new SqlParameter("@Contract_ID",Contract_ID),
                new SqlParameter("@TBL_SELECTED_ITEM_REF_CODE",dtSelected_Items),
                new SqlParameter("@TBL_SELECTED_SubSystemCode",dtSelected_SubCatalogue),
                new SqlParameter("@TBL_DeSelected_SubCatalogue_items",dtDeSelected_SubCatalogue_items),
                new SqlParameter("@CREATED_BY",Created_by),
                new SqlParameter("@return",SqlDbType.Int)
                
            };
            prm[prm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_INS_CTP_Quotation_Items", prm);
            return Int32.Parse(prm[prm.Length - 1].Value.ToString());

        }

        public static int Update_Ctp_Remove_QtnItem_DL(int Qtn_Item_ID, int Quotation_ID, int UserID)
        {
            SqlParameter[] prm = new SqlParameter[]
            {               
                new SqlParameter("@Quotation_ID",Quotation_ID),
                new SqlParameter("@qtn_item_id",Qtn_Item_ID),
                new SqlParameter("@userid",UserID),
             };

            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_UPD_CTP_REMOVE_QTNITEM", prm);
        }

        public static int Update_Ctp_QtnItem_Remark_DL(int User_Type, string Remark, int UserID, int Qtn_Item_ID)
        {
            SqlParameter[] prm = new SqlParameter[]
            {             
                new SqlParameter("@User_Type",User_Type),
                new SqlParameter("@qtn_item_id",Qtn_Item_ID),
                new SqlParameter("@userid",UserID),
                new SqlParameter("@Remark",Remark)
             };

            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_UPD_CTP_QTNITEM_REMARK", prm);
        }

        public static int Update_Ctp_ReworkToSupplier_DL(int UserID, int Quotation_ID)
        {
            SqlParameter[] prm = new SqlParameter[]
            {             
                new SqlParameter("@UserID",UserID),
                new SqlParameter("@Quotation_ID",Quotation_ID),
                
             };

            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_UPD_CTP_REWORK_TO_SUPPLIER", prm);
        }

        public static DataSet Get_Ctp_Supplier_Mail_DL(int Quotation_ID)
        {
            SqlParameter[] prm = new SqlParameter[]
            {             
               
                new SqlParameter("@Quotation_ID",Quotation_ID)
                
             };

            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_GET_CTP_Supplier_Mail", prm);
        }

        public static DataSet Get_Ctp_RFQ_Items_DL(int Quotation_ID)
        {
            SqlParameter[] prm = new SqlParameter[]
            {             
               
                new SqlParameter("@Quotation_ID",Quotation_ID)
                
             };

            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_GET_CTP_RFQ_ITEMS", prm);
        }

        public static DataTable Get_Ctp_Qtn_Eval_Supplier_DL(DataTable dtQuotation_ID)
        {
            SqlParameter[] prm = new SqlParameter[]
            {             
               
                new SqlParameter("@TBL_QUOTATIONS",dtQuotation_ID)
                
             };

            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_GET_CTP_Qtn_Eval_Supplier", prm).Tables[0];
        }

        public static DataTable Get_Ctp_Qtn_Eval_Item_DL(DataTable dtQuotation_ID, string Search_Item, string Search_Subcatalogue, int? QtnID_Quotedtems, int? Page_Index, int? Page_Size, ref int Is_fetch_count, int? Sort_Direction, string Sort_Column)
        {
            SqlParameter[] prm = new SqlParameter[]
            {             
               
                new SqlParameter("@TBL_QUOTATIONS",dtQuotation_ID),
                new SqlParameter("@SEARCH_ITEM",Search_Item),
                new SqlParameter("@SEARCH_SUBCATALOGUE",Search_Subcatalogue),
                new SqlParameter("@QTNID_QUOTEDITEMS",QtnID_Quotedtems),

                new SqlParameter("@PAGE_INDEX",Page_Index),
                new SqlParameter("@PAGE_SIZE",Page_Size),
                new SqlParameter("@SORT_DIRECTION",Sort_Direction),
                new SqlParameter("@SORT_COLUMN",Sort_Column),
                new SqlParameter("@IS_FETCH_COUNT",Is_fetch_count),

             };

            prm[prm.Length - 1].Direction = ParameterDirection.InputOutput;
            DataTable dtres = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_GET_CTP_Qtn_Eval_Item", prm).Tables[0];
            Is_fetch_count = Convert.ToInt32(prm[prm.Length - 1].Value);
            return dtres;
        }

        public static int Upd_Ctp_Approve_Contract_DL(int Approver_ID, DataTable dtQuotation, DateTime Effective_Date, DateTime Expiry_Date, string Approver_Remark)
        {
            SqlParameter[] prm = new SqlParameter[]
            {                
                new SqlParameter("@APPROVER_ID",Approver_ID),
                new SqlParameter("@TBL_QUOTATION_ID",dtQuotation),
                new SqlParameter("@EFFECTIVE_DATE",Effective_Date),
                new SqlParameter("@EXPIRY_DATE",Expiry_Date),
                new SqlParameter("@APPROVER_REMARK",Approver_Remark)
            };

            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_UPD_CTP_APPROVE_CONTRACT", prm);
        }

        public static DataTable Get_Ctp_Non_Contract_Items_DL(DateTime? Ord_DT_From, DateTime? Ord_DT_To, string Subcatalogue, string Catalogue, int? Page_Index, int? Page_Size, ref int Is_fetch_count, int? Sort_Direction, string Sort_Column)
        {
            SqlParameter[] prm = new SqlParameter[]
            {             
               
                new SqlParameter("@ORD_DT_FROM",Ord_DT_From),
                new SqlParameter("@ORD_DT_TO",Ord_DT_To),
                new SqlParameter("@SUBCATALOGUE",Subcatalogue),
                new SqlParameter("@CATALOGUE",Catalogue),

                new SqlParameter("@PAGE_INDEX",Page_Index),
                new SqlParameter("@PAGE_SIZE",Page_Size),
                new SqlParameter("@SORT_DIRECTION",Sort_Direction),
                new SqlParameter("@SORT_COLUMN",Sort_Column),
                new SqlParameter("@IS_FETCH_COUNT",Is_fetch_count),

             };

            prm[prm.Length - 1].Direction = ParameterDirection.InputOutput;
            DataTable dtres = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_GET_CTP_Non_Contract_Items", prm).Tables[0];
            Is_fetch_count = Convert.ToInt32(prm[prm.Length - 1].Value);
            return dtres;
        }


        public static DataTable Get_Ctp_Contract_List_ByCatalogue_DL(string Catalogue_Code, int? Port_ID, string Supplier_Code, string Quotation_ID)
        {
            SqlParameter[] prm = new SqlParameter[]
            {             
               
                new SqlParameter("@CATALOGUE_CODE",Catalogue_Code),
                new SqlParameter("@Port_ID",Port_ID),
                new SqlParameter("@Supplier_Code",Supplier_Code),
                new SqlParameter("@Quotation_ID",Quotation_ID)
            };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_GET_CTP_Contract_List_ByCatalogue", prm).Tables[0];
        }

        public static int UPD_Ctp_Delete_Contract_DL(int Quoation_ID, int Contract_ID, int UserId)
        {
            SqlParameter[] prm = new SqlParameter[]
            {             
                new SqlParameter("@Quoation_ID",Quoation_ID),
                new SqlParameter("@Contract_ID",Contract_ID),
                new SqlParameter("@UserId",UserId),
                
            };
            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_UPD_CTP_Delete_Contract", prm);
        }


        public static int INS_CTP_Copy_Contract(int Quotation_ID, int Created_By, string Remark, DataTable dtDept_Catalogue)
        {
            SqlParameter[] prm = new SqlParameter[]
            {
                new SqlParameter("@Quotation_ID",Quotation_ID),
                new SqlParameter("@CREATED_BY",Created_By),
                new SqlParameter("@APPROVER_REMARK",Remark),
                new SqlParameter("@tblDEPT_CATALOGUE",dtDept_Catalogue),
                 new SqlParameter("@return", SqlDbType.Int)
            };

           
           prm[prm.Length-1].Direction = ParameterDirection.ReturnValue;
            
             SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_INS_CTP_Copy_Contract", prm);
            return Convert.ToInt32(prm[prm.Length-1].Value);
        }

        public static int UPD_CTP_Recall_Approved_Contract(int Quotation_ID, int User_ID, string Remark)
        {
            SqlParameter[] prm = new SqlParameter[]
            {
                new SqlParameter("@Quotation_ID",Quotation_ID),
                new SqlParameter("@User_ID",User_ID),
                new SqlParameter("@Remark",Remark)
            };

            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_UPD_CTP_Recall_Approved_Contract", prm);
        }

    }
}
