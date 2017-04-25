using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SMS.Data.PURC;
using System.Data.SqlClient;
using System.Data;
using SMS.Properties;

namespace SMS.Business.PURC
{
    public class BLL_PURC_Common
    {
        public static Int16 GET_Is_SentToSuppdt(string ReqsnCode)
        {
            return DAL_PURC_Common.GET_Is_SentToSuppdt(ReqsnCode);
        }

        public static DataTable Get_ReqsnItemToApprove(string ReqsnCode)
        {
            return DAL_PURC_Common.Get_ReqsnItemToApprove_DL(ReqsnCode);
        }
        public static Int16 Get_IsApproved_ReqsnItem(string ReqsnCode)
        {
            return DAL_PURC_Common.Get_IsApproved_ReqsnItem_DL(ReqsnCode);
        }
        public static Int16 Update_ApproveReqsnItem(string ReqsnCode, int ID, int OfficeID, decimal ORDQty, string OrdUnit, int UpdBy, decimal UnitPrice, decimal Discount, int BugtCode)
        {
            return DAL_PURC_Common.Update_ApproveReqsnItem_DL(ReqsnCode, ID, OfficeID, ORDQty, OrdUnit, UpdBy, UnitPrice, Discount, BugtCode);
        }

        public static DataTable Get_AssignedAttach(string ReqsnCode, string FileName)
        {
            return DAL_PURC_Common.Get_AssignedAttach_DL(ReqsnCode,FileName);
        }


        public static int Update_AssignedAttachFile(string ReqsnCode,string FileName, string SuppCode, int IsAssigned, int UpdatedBy)//, int Vessel_ID
        {
            return DAL_PURC_Common.Update_AssignedAttachFile_DL(ReqsnCode,FileName, SuppCode, IsAssigned, UpdatedBy);//, Vessel_ID
        }

        public static DataTable GET_SuppName_AttachedFile(string ReqsnCode, string FileName)
        {
            return DAL_PURC_Common.GET_SuppName_AttachedFile_DL(ReqsnCode, FileName);
        }
        public static int Insert_Mail(int ReqsnID, int VesselID, int MailID)
        {
            return DAL_PURC_Common.Insert_Mail_DL(ReqsnID, VesselID, MailID);
        }

        public static DataTable Get_ReqsnType_Log(string ReqsnCode)
        {
            return DAL_PURC_Common.Get_ReqsnType_Log_DL(ReqsnCode);
        }

        public static int UPDATE_CancelPO(string OrderCode, string Remark, int UserId)
        {
            return DAL_PURC_Common.UPDATE_CancelPO_DL(OrderCode, Remark, UserId);
        }

        public static DataTable Get_POItem_ToCancel(string OrderCode)
        {
            return DAL_PURC_Common.Get_POItem_ToCancel_DL(OrderCode);
        }

        public static int UPDATE_CancelPOItems(string OrderCode, string Remark, int UserId, string ItemID)
        {
            return DAL_PURC_Common.UPDATE_CancelPOItems_DL(OrderCode, Remark, UserId, ItemID);
        }
        public static DataTable Get_CancelReqsn(int? fleetid, int? vesselcode, string depttype, string deptcode
            , int? reqsntype, string req_order_code, int? pageindex, int? pagesize, ref int isfetchcount)
        {
            return DAL_PURC_Common.Get_CancelReqsn_DL(fleetid, vesselcode, depttype, deptcode, reqsntype, req_order_code, pageindex, pagesize, ref isfetchcount);
        }

        public static DataTable Get_CancelReqsn(DataTable fleetid, DataTable vesselcode, string depttype, DataTable deptcode
           , int? reqsntype, string req_order_code, int? pageindex, int? pagesize, ref int isfetchcount, string Sort_By, string Sort_Direction)
        {
            return DAL_PURC_Common.Get_CancelReqsn_DL(fleetid, vesselcode, depttype, deptcode, reqsntype, req_order_code, pageindex, pagesize, ref isfetchcount,Sort_By,Sort_Direction);
        }

        public static DataTable Get_RFQ_Supplier(string Reqsncode)
        {

            return DAL_PURC_Common.Get_RFQ_Supplier_DL(Reqsncode);
        }

        public static DataTable GET_ItemType(string QuotationCode, string ItemRefCode)
        {
            return DAL_PURC_Common.GET_ItemType_DL(QuotationCode, ItemRefCode);
        }


        public static DataTable GET_ItemTypeAll(string QuotationCode)
        {
            return DAL_PURC_Common.GET_ItemTypeAll_DL(QuotationCode);
        }

        public static DataTable GET_Item_History(string VesselCode, string ItemRefCode)
        {
            return DAL_PURC_Common.GET_Item_History_DL(VesselCode, ItemRefCode);
        }

        public static DataTable Get_QuotedRates_ByItem(string ReqsnCode, string ItemRefCode)
        {
            return DAL_PURC_Common.Get_QuotedRates_ByItem_DL(ReqsnCode, ItemRefCode);
        }

        public static DataTable GET_ItemDetails_ByItemRefCode(string ItemRefCode)
        {
            return DAL_PURC_Common.GET_ItemDetails_ByItemRefCode(ItemRefCode);
        }

        public static DataTable GET_ReqsnPermanentCancelLog_DL(int Vcode)
        {
            return DAL_PURC_Common.GET_ReqsnPermanentCancelLog_DL(Vcode);
        }

        public static DataTable Get_Supplier_Category()
        {
            return DAL_PURC_Common.Get_Supplier_Category_DL();
        }

        public static DataTable Get_Inventory_Item_List(int Fleet,
                                                      int Vessel_ID,
                                                      string Department_Code,
                                                      string System_Code,
                                                      string Subsystem_Code,
                                                      string Part_Number,
                                                      string Drawing_Number,
                                                      string Short_Description,

                                                      string FROMDATE,
                                                      string TODATE,
                                                      int Inventory_Qty,
                                                      int Latest,
                                                     int? Inventory_Qty_Less,
                                                     int? Inventory_Qty_Greater,

                                                      int PageIndex,
                                                      int PageSize,
                                                      int isCritical,
                                                      ref int IsFetch_Count
                                                      )
        {
            return DAL_PURC_Common.Get_Inventory_Item_List_DL(
                                                            Fleet,
                                                         Vessel_ID,
                                                         Department_Code,
                                                         System_Code,
                                                         Subsystem_Code,
                                                         Part_Number,
                                                         Drawing_Number,
                                                         Short_Description,

                                                         FROMDATE,
                                                         TODATE,
                                                         Inventory_Qty,
                                                         Latest,
                                                         Inventory_Qty_Less,
                                                         Inventory_Qty_Greater,

                                                         PageIndex,
                                                         PageSize,
                                                         isCritical,
                                                         ref IsFetch_Count
                                                          );
        }

        public static int Get_Inventory_Item_List_Count(int Fleet,
                                                    int Vessel_ID,
                                                    string Department_Code,
                                                    string System_Code,
                                                    string Subsystem_Code,
                                                    string Part_Number,
                                                    string Drawing_Number,
                                                    string Short_Description,

                                                    string FROMDATE,
                                                    string TODATE,
                                                    int Inventory_Qty,
                                                    int Latest

                                                    )
        {
            return DAL_PURC_Common.Get_Inventory_Item_List_Count_DL(
                                                            Fleet,
                                                         Vessel_ID,
                                                         Department_Code,
                                                         System_Code,
                                                         Subsystem_Code,
                                                         Part_Number,
                                                         Drawing_Number,
                                                         Short_Description,

                                                         FROMDATE,
                                                         TODATE,
                                                         Inventory_Qty,
                                                         Latest

                                                          );
        }

        public static DataSet Get_Items_ForAddAdditional(string SystemCode, string SubSystemCode, string partNo, string DrawNo, string Description)
        {
            return DAL_PURC_Common.Get_Items_ForAddAdditional_DL(SystemCode, SubSystemCode, partNo, DrawNo, Description);
        }

        public static void Update_OrderQty_From_ReqstQty(string ReqsnCode, string DocumentCode, string BgtCode)
        {
            DAL_PURC_Common.Update_OrderQty_From_ReqstQty_DL(ReqsnCode, DocumentCode, BgtCode);
        }

        public static DataSet Get_QTN_Approver(string ReqsnCode)
        {
            return DAL_PURC_Common.Get_QTN_Approver_DL(ReqsnCode);
        }

        public static string Get_BGTCode_Reqsn(string Reqsn)
        {
            return DAL_PURC_Common.Get_BGTCode_Reqsn_DL(Reqsn);
        }

        public static DataTable Get_Supplier_ValidDate(string Supplier)
        {
            return DAL_PURC_Common.Get_Supplier_ValidDate_DL(Supplier);
        }

        public static int CheckHierarchy_SendForApproval(string ReqsnCode, string DocumentCode, int VesselCode, string LogedInUserID, decimal RqstAmt, decimal ApproverLimit, DataTable dtQuotationList_ForTopApprover)
        {
            return DAL_PURC_Common.CheckHierarchy_SendForApproval_DL(ReqsnCode, DocumentCode, VesselCode, LogedInUserID, RqstAmt, ApproverLimit, dtQuotationList_ForTopApprover);
        }

        public static int INS_Remarks(string DocCode, int UserID, string Remark, int Remark_Type)
        {
            return DAL_PURC_Common.INS_Remarks_DL(DocCode, UserID, Remark, Remark_Type);

        }

        public static DataTable GET_Remarks(string DocCode, int Remark_Type)
        {
            return DAL_PURC_Common.GET_Remarks_DL(DocCode, Remark_Type);
        }

        public static DataTable Get_ReqsnItems(string ReqsCode)
        {
            return DAL_PURC_Common.Get_ReqsnItems_DL(ReqsCode);
        }
        public static DataTable Get_SupplierDetails_ByCode(string SuppCode)
        {
            return DAL_PURC_Common.Get_SupplierDetails_ByCode_DL(SuppCode);
        }

        public static int Get_Supplier_Status(string SuppCode)
        {
            return DAL_PURC_Common.Get_Supplier_Status_DL(SuppCode);
        }
        public static DataTable Get_PONumbers(string ReqsnCode)
        {
            return DAL_PURC_Common.Get_PONumbers_DL(ReqsnCode);
        }

        public static DataTable Get_Delivered_Requisition_Stage(int? fleetid, int? vesselcode, string depttype, string deptcode
            , int? reqsntype, string req_order_code, int? pageindex, int? pagesize, ref int isfetchcount, string Sort_By, string Sort_Direction)
        {
            return DAL_PURC_Common.Get_Delivered_Requisition_Stage_DL(fleetid, vesselcode, depttype, deptcode
            , reqsntype, req_order_code, pageindex, pagesize, ref isfetchcount, Sort_By, Sort_Direction);
        }

        public static DataTable Get_Delivered_Requisition_Stage(DataTable fleetid, DataTable vesselcode, string depttype, DataTable deptcode
           , int? reqsntype, string req_order_code, int? Diff_Qty, int? pageindex, int? pagesize, ref int isfetchcount, string Sort_By, string Sort_Direction)
        {
            return DAL_PURC_Common.Get_Delivered_Requisition_Stage_DL(fleetid, vesselcode, depttype, deptcode
            , reqsntype, req_order_code, Diff_Qty, pageindex, pagesize, ref isfetchcount,Sort_By,Sort_Direction);
        }

        public static DataTable PURC_GET_Quotation_ByReqsnCode(string ReqsnCode)
        {
            return DAL_PURC_Common.PURC_GET_Quotation_ByReqsnCode_DL(ReqsnCode);
        }

        public static int Activate_Cancelled_Reqsn(string Document_Code, int UserID)
        {
            return DAL_PURC_Common.Activate_Cancelled_Reqsn_DL(Document_Code, UserID);
        }

        public static int Activate_Cancelled_PO_DL(string Order_Code, int UserID)
        {
            return DAL_PURC_Common.Activate_Cancelled_PO_DL(Order_Code, UserID);
        }

        public static int INS_SYNC_PO(string Order_Code)
        {
            return DAL_PURC_Common.INS_SYNC_PO_DL(Order_Code);
        }

        public static DataTable GET_REQSN_VMT(string DocCode)
        {
            return DAL_PURC_Common.GET_REQSN_VMT_DL(DocCode);
        }


        public static DataTable Get_Quotation_Items_Compare(string Catalogue, string ReqsnCode, string DocumentCode, string VesselCode, string SupplierCodes, string QuotationCodes, string ExchangeRates)
        {
            return DAL_PURC_Common.Get_Quotation_Items_Compare_DL(Catalogue, ReqsnCode, DocumentCode, VesselCode, SupplierCodes, QuotationCodes, ExchangeRates);
        }


        public static DataSet Get_ReqsnItems_Split_ToVessel(string Reqsn_Code, DataTable dtVessels)
        {
            return DAL_PURC_Common.Get_ReqsnItems_Split_ToVessel_DL(Reqsn_Code, dtVessels);
        }

        public static DataTable UPD_Reqsn_Split_IntoVessel(string Reqsn_Code, DataTable dtItemsVessels, int UserID)
        {

            return DAL_PURC_Common.UPD_Reqsn_Split_IntoVessel_DL(Reqsn_Code, dtItemsVessels, UserID);
        }

        public static int UPD_Reqsn_Split_IntoVessel_Finalize(string ReqsnCode, DataTable dtChild_Reqsn_Code, int UserID)
        {
            return DAL_PURC_Common.UPD_Reqsn_Split_IntoVessel_Finalize_DL(ReqsnCode, dtChild_Reqsn_Code, UserID);
        }

        public static DataTable Get_ReqsnItems_Split_ToVessel_Report(string Reqsn_Code)
        {

            return DAL_PURC_Common.Get_ReqsnItems_Split_ToVessel_Report_DL(Reqsn_Code);
        }


        public static DataTable Get_Bulk_Purchase_Reqsn(int? fleetid, int? vesselcode, string depttype, string deptcode
        , int? reqsntype, string req_order_code, int? pageindex, int? pagesize, ref int isfetchcount)
        {
            return DAL_PURC_Common.Get_Bulk_Purchase_Reqsn_DL(fleetid, vesselcode, depttype, deptcode, reqsntype, req_order_code, pageindex, pagesize, ref  isfetchcount);
        }

        public static DataTable Get_Bulk_Purchase_Reqsn(DataTable fleetid, DataTable vesselcode, string depttype, DataTable deptcode
      , int? reqsntype, string req_order_code, int? Reqsn_Sts, int? pageindex, int? pagesize, ref int isfetchcount)
        {
            return DAL_PURC_Common.Get_Bulk_Purchase_Reqsn_DL(fleetid, vesselcode, depttype, deptcode, reqsntype, req_order_code, Reqsn_Sts, pageindex, pagesize, ref  isfetchcount);
        }

        public static DataTable Get_Split_SavedVessel(string Reqsn_Code, int Finalize)
        {
            return DAL_PURC_Common.Get_Split_SavedVessel_DL(Reqsn_Code, Finalize);
        }

        public static int Get_Check_ReqsnValidity(string Reqsn_Code)
        {
            return DAL_PURC_Common.Get_Check_ReqsnValidity_DL(Reqsn_Code);
        }

        public static DataTable Get_SupplierList(string Supplier_Category, string Supplier_Search)
        {
            return DAL_PURC_Common.Get_SupplierList_DL(Supplier_Category, Supplier_Search);
        }


        public static int Get_Bulk_Reqsn_Finalized(string Reqsn_Code)
        {
            return DAL_PURC_Common.Get_Bulk_Reqsn_Finalized_DL(Reqsn_Code);
        }


        public static int UPD_RollBack_Bulk_Reqsn(string Reqsn_Code, int UserID)
        {
            return DAL_PURC_Common.UPD_RollBack_Bulk_Reqsn_DL(Reqsn_Code, UserID);
        }

        public static DataTable Get_BudgetCode_ByReqsnType(int Reqsn_Type)
        {
            return DAL_PURC_Common.Get_BudgetCode_ByReqsnType(Reqsn_Type);
        }

        public static DataTable Get_Reqsn_Type_Budget(string Search)
        {
            return DAL_PURC_Common.Get_Reqsn_Type_Budget(Search);

        }
        public static int Upd_Reqsn_Type_Budget(string Action, int Budget_Code, int Reqsn_Type, int? ID, int UserID)
        {
            return DAL_PURC_Common.Upd_Reqsn_Type_Budget(Action, Budget_Code, Reqsn_Type, ID, UserID);
        }

        public static int Get_Reqsn_IsValidToClose(string Requisition_Code)
        {
            return DAL_PURC_Common.Get_Reqsn_IsValidToClose(Requisition_Code);
        }

        public static int Upd_Close_Requisition(string Requisition_Code, int UserID)
        {
            return DAL_PURC_Common.Upd_Close_Requisition(Requisition_Code,UserID);
        }

        public static DataTable Get_Provision_Limit_Vessels()
        {
            return DAL_PURC_Common.Get_Provision_Limit_Vessels();
        }

        public static DataSet Get_Provisions_Approval_Limit(DataTable dtVessels, int Page_Index, int Page_Size, string SubCatalogue, string Search, int? MaxQty)
        {
            return DAL_PURC_Common.Get_Provisions_Approval_Limit(dtVessels, Page_Index, Page_Size, SubCatalogue, Search,MaxQty);
        }

        public static int Upd_Provisions_Approval_Limit(string Item_Ref_Code, decimal Max_Qty, decimal Max_Cost, int UserID, int Vessel_ID)
        {
            return DAL_PURC_Common.Upd_Provisions_Approval_Limit( Item_Ref_Code,  Max_Qty,  Max_Cost,  UserID,  Vessel_ID);

        }
        public static int Upd_Copy_Provisions_Approval_Limit(int Assigned_Vessel_ID, DataTable tbl_Selected_Vessels, int UserID)
        {
            return DAL_PURC_Common.Upd_Copy_Provisions_Approval_Limit(Assigned_Vessel_ID, tbl_Selected_Vessels, UserID);
        }

        public static DataTable Get_Check_Provision_Limit(string Document_Code)
        {
          return  DAL_PURC_Common.Get_Check_Provision_Limit(Document_Code);
        }
        public static void UpdateIsMeat(string id, int isMeat, int USERID)
        {
            DAL_PURC_Common.UpdateIsMeat(id, isMeat, USERID);
        }
        public static void UpdateSlopChestItems(string id, bool isSlopChest, int UserID)
        {
            DAL_PURC_Common.UpdateSlopChestItems(id, isSlopChest, UserID);
        }
       
        public DataTable Get_LIB_Page_Config( int ClientID, string KeyValue)
        {
            return DAL_PURC_Common.Get_LIB_Page_Config(ClientID, KeyValue);
        }
        public static int INS_UPD_DEL_Grading(int? ID, string Grading_Name, int? Grade_Type, int? Min, int? Max, int? Divisions, int UserID, string Mode)
        {
            try
            {
                return DAL_PURC_Common.INS_UPD_DEL_Grading_DL(ID, Grading_Name, Grade_Type, Min, Max, Divisions, UserID, Mode);
            }
            catch
            {
                throw;
            }

        }
        public static int INS_UPD_DEL_GradingOption(int Grade_ID, string OptionText, decimal OptionValue, int UserID)
        {
            try
            {
                return DAL_PURC_Common.INS_UPD_DEL_GradingOption_DL(Grade_ID, OptionText, OptionValue, UserID);
            }
            catch
            {
                throw;
            }

        }
        public static DataTable Get_GradingList()
        {
            try
            {
                return DAL_PURC_Common.Get_GradingList_DL();
            }
            catch
            {
                throw;
            }

        }
        public static DataTable Get_GradingOptions(int Grade_ID)
        {
            try
            {
                return DAL_PURC_Common.Get_GradingOptions_DL(Grade_ID);
            }
            catch
            {
                throw;
            }
        }
        public static DataTable Get_QuestionList(string searchtext, string dept)
        {
            try
            {
                return DAL_PURC_Common.Get_QuestionList_DL(searchtext, dept);
            }
            catch
            {
                throw;
            }

        }
        public static int INS_UPD_DEL_Question(int? ID, string Question, int? Grading_Type,int ? Type_ID, int UserID, string Mode)
        {
            try
            {
                return DAL_PURC_Common.INS_UPD_DEL_Question_DL(ID, Question, Grading_Type,Type_ID, UserID, Mode);
            }
            catch
            {
                throw;
            }
        }

        public static DataTable Get_GradingType()
        {
            try
            {
                return DAL_PURC_Common.Get_GradingType();
            }
            catch
            {
                throw;
            }

        }
        #region CancelPO
        public static int PURC_UPD_CancelPO_FilePath(string OrderCode, string FileName, string FilePath, string Remark, int UserID)
        {
            try
            {
                return DAL_PURC_Common.PURC_UPD_CancelPO_FilePath(OrderCode, FileName, FilePath,Remark,UserID);
            }
            catch
            {
                throw;
            }
        }
        #endregion
        #region
        public static int Get_Active_PO_Count(string Order_Code)
        {
            try
            {
                return DAL_PURC_Common.PURC_Get_Active_PO_Count(Order_Code);
            }
            catch
            {
                throw;
            }
        }
        #endregion
        #region
        public static DataTable Get_Purc_Items (string Reqsn_Code)
        { 
            try
            {
                return DAL_PURC_Common.Get_Purc_Items(Reqsn_Code);
            }
            catch
            {
                throw;
            }


        }
        
        #endregion

        #region PO_Log
        public static DataSet PURC_Get_Type(int? UserID, string FilterType)
        {
            return DAL_PURC_Common.PURC_Get_Type(UserID, FilterType);
        }
        public static DataSet PURC_Get_AccountClassification(int? UserID, string FilterType,string Reqn_Type)
        {
            return DAL_PURC_Common.PURC_Get_AccountClassification(UserID, FilterType, Reqn_Type);
        }
        public static DataSet PURC_Get_Configuration(string FilterType)
        {
            return DAL_PURC_Common.PURC_Get_Configuration(FilterType);
        }
        public static DataSet SelectCatalog(int? UserID, string Reqn_Type, string Function_Type,int vessel_ID)
        {

            return DAL_PURC_Common.SelectCatalog(UserID, Reqn_Type, Function_Type, vessel_ID);
        }
      
        public static DataSet PURC_Checking_Requisition(IventoryItemData objDoInventory)
        {

            return DAL_PURC_Common.PURC_Checking_Requisition(objDoInventory);
        }
       public static DataSet PURC_Get_RequisitionDeatils(int? UserID, string DocumentCode)
        {

            return DAL_PURC_Common.PURC_Get_RequisitionDeatils(UserID,DocumentCode);
        }
      
       public static int ItemImageUpdate(int userid, string itemid, string systemcode, string subsystemcode, string image_url, string product_details)
       {

           return DAL_PURC_Common.ItemImageUpdate(userid, itemid, systemcode, subsystemcode, image_url, product_details);
       }
        public static DataTable PURC_Get_PO_Type(int UserID, string AccessType)
       {
           return DAL_PURC_Common.PURC_Get_PO_Type(UserID,AccessType);
       }
        public static DataTable PURC_Get_Sys_Variable(int UserID, string FilterType)
        {
            return DAL_PURC_Common.PURC_Get_Sys_Variable(UserID, FilterType);
        }
        public static DataTable PURC_Get_Supplier_Type(int UserID, string FilterType)
        {
            return DAL_PURC_Common.PURC_Get_Supplier_Type(UserID, FilterType);
        }
        public static DataSet PURC_Get_Requisition_Type(string PO_Type,int? User_ID)
        {
            return DAL_PURC_Common.PURC_Get_Requisition_Type(PO_Type,User_ID);
        }
        public static DataSet Filter_Catalog(int? UserID, string Function_Type)
        {
            return DAL_PURC_Common.Filter_Catalog(UserID, Function_Type);
        }
        public static DataSet PURC_Filter_AccountClassification(int? UserID, string FilterType, string Reqn_Type)
        {
            return DAL_PURC_Common.PURC_Filter_AccountClassification(UserID, FilterType, Reqn_Type);
        }
        public static DataTable Get_Delivered_Requisition_Stage_DL_New(string fleetid, string vesselcode, string depttype, string deptcode, string req_order_code, string potype, string acctype, string reqsntype, string catalogue, string FromDate, string ToDate, string accClass, string dtUrgency, string reqstatus, string Supplier, string Delivery_Code, int? pageindex, int? pagesize, ref int isfetchcount, string Sort_By, string Sort_Direction)
        {
            return DAL_PURC_Common.Get_Delivered_Requisition_Stage_DL_New(fleetid, vesselcode, depttype, deptcode, req_order_code, potype, acctype, reqsntype, catalogue, FromDate, ToDate, accClass, dtUrgency, reqstatus, Supplier, Delivery_Code, pageindex, pagesize, ref isfetchcount, Sort_By, Sort_Direction);
        }

        public static DataTable Get_CancelReqsn_DL_New(string fleetid, string vesselcode, string depttype, string deptcode, string req_order_code, string potype, string acctype, string reqsntype, string catalogue, string FromDate, string ToDate, string accClass, string dtUrgency, string reqstatus, int? pageindex, int? pagesize, ref int isfetchcount, string Sort_By, string Sort_Direction)
        {
            return DAL_PURC_Common.Get_CancelReqsn_DL_New(fleetid, vesselcode, depttype, deptcode
                            , req_order_code, potype, acctype, reqsntype, catalogue, FromDate, ToDate, accClass, dtUrgency, reqstatus, pageindex, pagesize, ref isfetchcount, Sort_By, Sort_Direction);
        }
        public static DataTable Get_User_Vessel_Assign(int Vessel_ID,int UserID)
        {
            return DAL_PURC_Common.Get_User_Vessel_Assign(Vessel_ID,UserID);
        }
        public static DataTable Get_Function(int UserID , int ReqsnType)
        {
            return DAL_PURC_Common.Get_Function(UserID, ReqsnType);
        }

        public static DataSet PURC_INS_Duplicate_RequisitionDeatils(string DocumentCode, int VESSEL_CODE, int Delivery_Port, string Delivery_Date, int UserID, string user_Name)
        {

            return DAL_PURC_Common.PURC_INS_Duplicate_RequisitionDeatils(DocumentCode, VESSEL_CODE, Delivery_Port, Delivery_Date, UserID, user_Name);
        }

        public static DataTable PURC_Get_ItemCategory(int UserID, string FilterType)
        {
            return DAL_PURC_Common.PURC_Get_ItemCategory(UserID, FilterType);
        }
        public static DataTable PURC_Get_UserTypeAccess(int UserID, string Variable_Type)
        {
            return DAL_PURC_Common.PURC_Get_UserTypeAccess(UserID, Variable_Type);
        }
        public static DataTable GET_Buyer_Remarks(string DocCode)
        {
            return DAL_PURC_Common.GET_Buyer_Remarks(DocCode);
        }
        #endregion
    }


}
