using System;
using System.Data;
using System.Configuration;
using SMS.Data.PURC;
using SMS.Properties;
using SMS.Data;



namespace SMS.Business.PURC
{

    public partial class BLL_PURC_Purchase
    {

        DAL_PURC_InventoryItem objInventoryItem = new DAL_PURC_InventoryItem();



        public static DataTable SelectItemForInventory(string CatalogID,string SubCatalg,string ItemDesc,string PartNo, string DrawNo,string ReqsnCode,int PageIndex,int PageSize)
        {

            return DAL_PURC_InventoryItem.SelectItemForInventory(CatalogID, SubCatalg, ItemDesc, PartNo, DrawNo, ReqsnCode, PageIndex, PageSize);


        }
        public static DataTable Get_Create_New_Requisition(string CatalogID, string SubCatalg, string ItemDesc, string PartNo, string DrawNo, string DocCode, int PageIndex, int PageSize, ref int count)
        {
            return DAL_PURC_InventoryItem.Get_Create_New_Requisition(CatalogID, SubCatalg, ItemDesc, PartNo, DrawNo, DocCode, PageIndex, PageSize, ref count);
        }

        public static int SelectItemForInventory_Count(string CatalogID, string SubCatalg, string ItemDesc, string PartNo, string DrawNo, string ReqsnCode)
        {

            return DAL_PURC_InventoryItem.SelectItemForInventory_Count(CatalogID, SubCatalg, ItemDesc, PartNo, DrawNo, ReqsnCode);


        }

        public DataTable SelectItemForInventoryForRequisition(string Vessel_Code)
        {

            return objInventoryItem.SelectItemForInventoryForRequisition(Vessel_Code);

        }



        public DataTable SelectItemForInventoryFilter(string RequisitionCode, string VesselCode, string Document_code,string SystemCode)
        {

            return objInventoryItem.SelectItemForInventoryFilter(RequisitionCode, VesselCode, Document_code,SystemCode);

        }


        public DataTable getRequisition(string Document_Code)
        {

            return objInventoryItem.getRequisition(Document_Code);


        }


        public DataTable SelectRequisitionCodeForCombo()
        {

            return objInventoryItem.SelectRequisitionCodeForCombo();


        }

        public int SaveInventroySupplyItem(IventoryItemData objDOInventoryItem)
        {

            return objInventoryItem.SaveInventroySupplyItem(objDOInventoryItem);

        }

        public string GenerateRequisitionNumber(IventoryItemData objDOInventoryItem)
        {

            return objInventoryItem.GenerateRequisitionNumber(objDOInventoryItem);

        }

        public int AddInventoryItem(IventoryItemData objDOInventoryItem)
        {

            return objInventoryItem.AddInventoryItem(objDOInventoryItem);


        }

        public int DeleteRequisitionItem(string RequisitionCode, string Documentcode)
        {

            return objInventoryItem.DeleteRequisitionItem(RequisitionCode, Documentcode);


        }
        public DataSet ConfiguredItemPreview(string Document_code, string ReqsCode, string Searchtext)
        {
            return objReports.ConfiguredSupplierPreview(Document_code, ReqsCode, Searchtext);
        }
        public DataSet getCurrentRates()
        {
            return objInventoryItem.getCurrentRates();
        }

        public string FinalizeRequisitionItem(string RequisitionCode, string Documentcode, string VesselCode, string PortName, string DeliveryDate, string RequistionComment)
        {
            return objInventoryItem.FinalizeRequisitionItem(RequisitionCode, Documentcode, VesselCode, PortName, DeliveryDate, RequistionComment);

        }

        public int AddCommentToRequisition(string RequisitionCode, string RequistionComment, string Documentcode)
        {

            return objInventoryItem.AddCommentToRequisition(RequisitionCode, RequistionComment, Documentcode);


        }

        public DataTable getDeliveryPort()
        {

            return objInventoryItem.getDeliveryPort();


        }

        public DataSet GetPOListForLogisticCompany()
        {

            return objInventoryItem.GetPOListForLogisticCompany();


        }

        public int AddConsolidatedPO(string strRequisition_Code, string strQuotation_Code, string strOrder_Code, string strDelivery_Port,
        DateTime dtDelivery_Date, string strDocument_Code, string strSupplier_Code, string strAssign_Agent_Code, Int32 i32Vessel_Code,
        Int32 i32Created_By)
        {

            return objInventoryItem.AddConsolidatedPO(strRequisition_Code, strQuotation_Code, strOrder_Code, strDelivery_Port,
            dtDelivery_Date, strDocument_Code, strSupplier_Code, strAssign_Agent_Code, i32Vessel_Code, i32Created_By);


        }

        public DataSet GetPOItemListForReport(Int32 i32Vessel_Code, string strAgent_Code)
        {

            return objInventoryItem.GetPOItemListForReport(i32Vessel_Code, strAgent_Code);


        }
        public DataSet GET_PURC_DEP_ON_DOCCODE(string DocCode)
        {
            return objInventoryItem.GET_PURC_DEP_ON_DOCCODE(DocCode);
            
        }
        public DataSet Get_Purc_Questions(string DocumnetCode,string DeptCode)
        {
            return objInventoryItem.Get_Purc_Questions(DocumnetCode, DeptCode);
        }
        public DataSet Get_Purc_Questions_Options(int QuestionID)
        {
            return objInventoryItem.Get_Purc_Questions_Options(QuestionID);
        }
        public DataSet Get_WorkList_Search(string SearchText, int? VESSEL_ID, string sortby, string sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            return objInventoryItem.Get_WorkList_Search(SearchText, VESSEL_ID, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);
        }
        public int Insert_Purc_WorkList(int? ID, int? WorklistID, int? OfficeID, int? VesselID, string DocCode, string Mode, int UserID)
        {
            return objInventoryItem.Insert_Purc_WorkList(ID, WorklistID, OfficeID, VesselID, DocCode, Mode, UserID);
        }
        public DataTable Get_Purc_Worklist(int? OFFICE_ID, int? VESSEL_ID, string DocumentCode)
        {
            return objInventoryItem.Get_Purc_Worklist(OFFICE_ID, VESSEL_ID, DocumentCode);
        }
        public int Insert_Purc_Question(string REQSN_CODE, string DocumentCode, int UserID, DataTable dtQuest, int? OFFICE_ID,int? VESSEL_ID)
        {
            return objInventoryItem.Insert_Purc_Question(REQSN_CODE, DocumentCode, UserID, dtQuest,OFFICE_ID,VESSEL_ID);
        }
        public DataTable Get_Purc_New_Reqsn_Docment_Code(string VesselID, string Dept)
        {
            return objInventoryItem.Get_Purc_New_Reqsn_Docment_Code(VesselID, Dept);
        }
        public DataTable Purc_Create_Finalize_New_Requisition(string PortName,string DeliveryDate,string RequistionComment,DataTable dtSupItem)
        {
            return objInventoryItem.Purc_Create_Finalize_New_Requisition(PortName, DeliveryDate, RequistionComment, dtSupItem);
        }

        public int Insert_Supplier_remarks_settings(string Item_Ref_code,string ReqsnCode,string DocCode,int Isshow, int UserID)
        {
            return objInventoryItem.ItemInsert_Supplier_remarks_settings(Item_Ref_code, ReqsnCode, DocCode, Isshow, UserID);
            
        }
        public int DeleteSupplyItem(string ItemRefCode, string Documentcode)
        {
            return objInventoryItem.DeleteSupplyItem(ItemRefCode, Documentcode);
 
        }
        public int UpdateSupplyQnty(string ItemRefCode,string docCode, decimal qty)
        {
            return objInventoryItem.UpdateSupplyQnty(ItemRefCode,docCode, qty);

        }
        public int PURC_UPD_Reqsn_supplyitems(string ItemRefCode, string docCode, decimal Qnty, decimal Discount, decimal PricePerUnit,decimal vat,decimal withhold)
        {
            return objInventoryItem.PURC_UPD_Reqsn_supplyitems(ItemRefCode, docCode, Qnty, Discount, PricePerUnit,vat, withhold);

        }
        public int PURC_UPD_Reqsn_supplyitems(string ItemRefCode, string docCode, decimal Qnty, decimal Discount, decimal PricePerUnit )
        {
            return objInventoryItem.PURC_UPD_Reqsn_supplyitems(ItemRefCode, docCode, Qnty, Discount, PricePerUnit );

        }
        public int UpdateNoQuoteRqsn(string ITEM_REF_CODE, string DOCUMENT_CODE, int ORDER_VAT, int Withholding_Tax_Rate, string ORDER_SUPPLIER, string DELIVERY_PORT, DateTime DELIVERY_DATE, int TotalPrice, string ORDER_DISCOUNT, string REQUISITION_CODE)
        {
            return objInventoryItem.UpdateNoQuoteRqsn(ITEM_REF_CODE, DOCUMENT_CODE, ORDER_VAT, Withholding_Tax_Rate, ORDER_SUPPLIER, DELIVERY_PORT, DELIVERY_DATE, TotalPrice, ORDER_DISCOUNT, REQUISITION_CODE);

        }

        public DataSet Get_SelectedPort()
        {
            return objInventoryItem.Get_SelectedPort();

        }
        public DataSet getSupplierProperty(string suppCode)
        {
            return objInventoryItem.getSupplierProperty(suppCode);

        }
        
        public string SaveUpdate_FinalQuotation(IventoryItemData objDOInventoryItem,int CurrentUser)
        {
            return objInventoryItem.SaveUpdate_FinalQuotation(objDOInventoryItem, CurrentUser);
        
        }
        public int SaveUpdate_Quotation(IventoryItemData objDOInventoryItem, int CurrentUser,decimal TP,DataTable dt)
        {
            return objInventoryItem.SaveUpdate_Quotation(objDOInventoryItem, CurrentUser,TP,dt);

        }
        public DataSet PURC_Get_Sys_Variable(int UserID, string FilterType)
        {
            return objInventoryItem.PURC_Get_Sys_Variable(UserID, FilterType);
        }
    }
}