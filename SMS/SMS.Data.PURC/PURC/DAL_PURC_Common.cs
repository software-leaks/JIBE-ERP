using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Data.SqlTypes;
using SMS.Properties;

namespace SMS.Data.PURC
{
    public class DAL_PURC_Common
    {
        static string _internalConnection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;

        public static Int16 GET_Is_SentToSuppdt(string ReqsnCode)
        {
            SqlParameter[] prm = new SqlParameter[]
            {
                new SqlParameter("@ReqsnCode",ReqsnCode)
            };

            return Convert.ToInt16(SqlHelper.ExecuteScalar(_internalConnection, CommandType.StoredProcedure, "PURC_GET_Is_SentToSuppdt", prm));
        }

        public static DataTable Get_ReqsnItemToApprove_DL(string ReqsnCode)
        {
            SqlParameter[] prm = new SqlParameter[]
            {
                new SqlParameter("@ReqsnCode",ReqsnCode)
            };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_Get_ReqsnItemToApprove", prm).Tables[0];
        }

        public static Int16 Get_IsApproved_ReqsnItem_DL(string ReqsnCode)
        {
            SqlParameter[] prm = new SqlParameter[]
            {
                new SqlParameter("@ReqsnCode",ReqsnCode)
            };
            return Convert.ToInt16(SqlHelper.ExecuteScalar(_internalConnection, CommandType.StoredProcedure, "PURC_Get_IsApproved_ReqsnItem", prm));
        }

        public static Int16 Update_ApproveReqsnItem_DL(string ReqsnCode, int ID, int OfficeID, decimal ORDQty, string OrdUnit, int UpdBy, decimal UnitPrice, decimal Discount, int BugtCode)
        {
            SqlParameter[] prm = new SqlParameter[]
            {
                new SqlParameter("@ReqsnCode",ReqsnCode),
                new SqlParameter("@ID",ID),
                new SqlParameter("@OfficeID",OfficeID),
                new SqlParameter("@ORDQty",ORDQty),
                new SqlParameter("@OrdUnit",OrdUnit),
                new SqlParameter("@UpdBy",UpdBy),
                new SqlParameter("@UnitPrice",UnitPrice),
                new SqlParameter("@Discount",Discount),
                new SqlParameter("@BugtCode",BugtCode)
               
            };
            return Convert.ToInt16(SqlHelper.ExecuteScalar(_internalConnection, CommandType.StoredProcedure, "PURC_Update_ApproveReqsnItem", prm));
        }

        public static DataTable Get_AssignedAttach_DL(string ReqsnCode, string FileName)
        {
            SqlParameter[] prm = new SqlParameter[]
            {
                new SqlParameter("@ReqsnCode",ReqsnCode),
                new SqlParameter("@FileName",FileName)
            };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_Get_AssignedAttach", prm).Tables[0];
        }

        public static int Update_AssignedAttachFile_DL(string ReqsnCode, string FileName, string SuppCode, int IsAssigned, int UpdatedBy)//,int Vessel_ID
        {
            SqlParameter[] prm = new SqlParameter[]
            {
                new SqlParameter("@FileName",FileName),
                new SqlParameter("@ReqsnCode",ReqsnCode),
                new SqlParameter("@SuppCode",SuppCode),
                new SqlParameter("@IsAssigned",IsAssigned),
                new SqlParameter("@Created_By",UpdatedBy)
                //new SqlParameter("@VesselCode",Vessel_ID)
            };
            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_Update_AssignedAttachFile", prm);
        }

        public static DataTable GET_SuppName_AttachedFile_DL(string ReqsnCode, string FileName)
        {
            SqlParameter[] prm = new SqlParameter[]
            {
                new SqlParameter("@ReqsnCode",ReqsnCode),
                new SqlParameter("@FileName",FileName)
            };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_GET_SuppName_AttachedFile", prm).Tables[0];
        }

        public static int Insert_Mail_DL(int ReqsnID, int VesselID, int MailID)
        {
            SqlParameter[] prm = new SqlParameter[]
            {
                new SqlParameter("@ReqsnID",ReqsnID),
                new SqlParameter("@MailID",MailID),
                new SqlParameter("@VesselID",VesselID)
               
            };
            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_INSERT_MAIL", prm);
        }


        public static DataTable Get_ReqsnType_Log_DL(string ReqsnCode)
        {
            SqlParameter[] prm = new SqlParameter[]
            {
                new SqlParameter("@ReqsnCode",ReqsnCode)
                
            };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_GET_REQSNTYPE_LOG", prm).Tables[0];
        }

        public static int UPDATE_CancelPO_DL(string OrderCode, string Remark, int UserId)
        {
            SqlParameter[] prm = new SqlParameter[]
            {
                new SqlParameter("@OrderCode",OrderCode),
                new SqlParameter("@Remark",Remark),
                new SqlParameter("@CancelledBy",UserId)
               
            };
            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_UPDATE_CancelPO", prm);
        }

        public static DataTable Get_POItem_ToCancel_DL(string OrderCode)
        {
            SqlParameter[] prm = new SqlParameter[]
            {
                new SqlParameter("@OrderCode",OrderCode)
                
            };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_GET_POITEM_TOCANCEL", prm).Tables[0];
        }

        public static int UPDATE_CancelPOItems_DL(string OrderCode, string Remark, int UserId, string ItemID)
        {
            SqlParameter[] prm = new SqlParameter[]
            {
                new SqlParameter("@OrderCode",OrderCode),
                new SqlParameter("@Remark",Remark),
                new SqlParameter("@CancelledBy",UserId),
                new SqlParameter("@ItemID",ItemID)
               
            };
            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_UPDATE_CancelPOItems", prm);
        }

        public static DataTable Get_CancelReqsn_DL(int? fleetid, int? vesselcode, string depttype, string deptcode
            , int? reqsntype, string req_order_code, int? pageindex, int? pagesize, ref int isfetchcount)
        {
            SqlParameter[] obj = new SqlParameter[]
            {
                 new SqlParameter("@FLEET_ID",fleetid)
                ,new SqlParameter("@VESSEL_CODE",vesselcode) 
                ,new SqlParameter("@DEPT_TYPE",depttype)
                ,new SqlParameter("@DEPT_CODE",deptcode)
                ,new SqlParameter("@REQSN_TYPE",reqsntype)
                ,new SqlParameter("@REQ_ORD_CODE",req_order_code)
            
                ,new SqlParameter("@PAGE_INDEX",pageindex)
                ,new SqlParameter("@PAGE_SIZE",pagesize)
                ,new SqlParameter("@IS_FETCH_COUNT",isfetchcount)
               
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_GET_CancelLog", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds.Tables[0];
        }

        public static DataTable Get_CancelReqsn_DL(DataTable fleetid, DataTable vesselcode, string depttype, DataTable deptcode
            , int? reqsntype, string req_order_code, int? pageindex, int? pagesize, ref int isfetchcount, string Sort_By, string Sort_Direction)
        {
            SqlParameter[] obj = new SqlParameter[]
            {
                 new SqlParameter("@FLEET_ID",fleetid)
                ,new SqlParameter("@VESSEL_CODE",vesselcode) 
                ,new SqlParameter("@DEPT_TYPE",depttype)
                ,new SqlParameter("@DEPT_CODE",deptcode)
                ,new SqlParameter("@REQSN_TYPE",reqsntype)
                ,new SqlParameter("@REQ_ORD_CODE",req_order_code)
            
                ,new SqlParameter("@PAGE_INDEX",pageindex)
                ,new SqlParameter("@PAGE_SIZE",pagesize)
                ,new SqlParameter("@SORT_BY",Sort_By)
                ,new SqlParameter("@SORT_DIRECTION",Sort_Direction)
                ,new SqlParameter("@IS_FETCH_COUNT",isfetchcount)
               
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_GET_CancelLog", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds.Tables[0];
        }

        public static DataTable Get_RFQ_Supplier_DL(string Reqsncode)
        {
            SqlParameter[] prm = new SqlParameter[]
            {
              
                new SqlParameter("@ReqsnCode",Reqsncode)
               
            };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_GET_RFQ_SUPPLIER", prm).Tables[0];
        }

        public static DataTable GET_ItemType_DL(string QuotationCode, string ItemRefCode)
        {
            SqlParameter[] prm = new SqlParameter[]
             { 
               
               new SqlParameter("@QuotationCode",QuotationCode),
               new SqlParameter("@ItemRefCode",ItemRefCode)

             };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_GET_ItemType", prm).Tables[0];
        }


        public static DataTable GET_ItemTypeAll_DL(string QuotationCode)
        {
            SqlParameter[] prm = new SqlParameter[]
             { 
               
               new SqlParameter("@QuotationCode",QuotationCode),
              

             };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_GET_ItemTypeAll", prm).Tables[0];
        }

        public static DataTable GET_Item_History_DL(string VesselCode, string ItemRefCode)
        {
            SqlParameter[] prm = new SqlParameter[]
             { 
               
               new SqlParameter("@Vessel_Code",VesselCode),
               new SqlParameter("@Item_Ref_Code",ItemRefCode)

             };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_GET_Item_History", prm).Tables[0];
        }

        public static DataTable Get_QuotedRates_ByItem_DL(string ReqsnCode, string ItemRefCode)
        {
            SqlParameter[] prm = new SqlParameter[]
             { 
               
               new SqlParameter("@reqsn_code",ReqsnCode),
               new SqlParameter("@item_ref_code",ItemRefCode)

             };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_Get_QuotedRates_ByItem", prm).Tables[0];
        }

        public static DataTable GET_ItemDetails_ByItemRefCode(string ItemRefCode)
        {
            SqlParameter[] prm = new SqlParameter[]
             { 
               
              
               new SqlParameter("@item_ref_code",ItemRefCode)

             };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_GET_ItemDetails_ByItemRefCode", prm).Tables[0];
        }


        public static DataTable GET_ReqsnPermanentCancelLog_DL(int Vcode)
        {
            SqlParameter[] prm = new SqlParameter[]
             { 
               
              
               new SqlParameter("@VesselCode",Vcode)

             };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_GET_ReqsnPermanentCancelLog", prm).Tables[0];
        }


        public static DataTable Get_Supplier_Category_DL()
        {
            string squery = "select Category_Name,Category_code from Lib_Suppliers_Category";
            DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.Text, squery);
            DataTable dt = new DataTable();
            if (ds.Tables.Count > 0)
                dt = ds.Tables[0];

            return dt;

        }

        public static DataTable Get_Inventory_Item_List_DL(
                                                        int Fleet,
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
            DateTime dtfrom = DateTime.Parse(FROMDATE);
            DateTime dtto = DateTime.Parse(TODATE);

            SqlParameter[] prm = new SqlParameter[]
             { 
               
              
               new SqlParameter("@Fleet",Fleet==0?SqlInt32.Null :Fleet),
               new SqlParameter("@Vessel_ID",Vessel_ID==0?SqlInt32.Null:Vessel_ID),
               new SqlParameter("@Department_Code",Department_Code=="0"?SqlString.Null:Department_Code),
               new SqlParameter("@System_Code",System_Code=="0"?SqlString.Null:System_Code),
               new SqlParameter("@Subsystem_Code",Subsystem_Code=="0"?SqlString.Null:Subsystem_Code),
               new SqlParameter("@Part_Number",Part_Number=="0"?SqlString.Null:Part_Number),
               new SqlParameter("@Drawing_Number",Drawing_Number=="0"?SqlString.Null:Drawing_Number),
               new SqlParameter("@Short_Description",Short_Description=="0"?SqlString.Null:Short_Description),
          
               new SqlParameter("@FROMDATE",dtfrom),
               new SqlParameter("@TODATE",dtto),
               new SqlParameter("@Inventory_Qty",Inventory_Qty),
               new SqlParameter("@Latest",Latest),
                new SqlParameter("@Inventory_Qty_Less",Inventory_Qty_Less),
                new SqlParameter("@Inventory_Qty_Greater",Inventory_Qty_Greater),



               new SqlParameter("@PageIndex",PageIndex),
               new SqlParameter("@PageSize",PageSize),
                 new SqlParameter("@IsCritical",isCritical),
               new SqlParameter("@IsFetch_Count",IsFetch_Count)
            };

            prm[prm.Length - 1].Direction = ParameterDirection.InputOutput;
            DataTable dt = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_Get_Inventory_Item_List", prm).Tables[0];
            IsFetch_Count = Convert.ToInt32(prm[prm.Length - 1].Value);
            return dt;
        }


        public static int Get_Inventory_Item_List_Count_DL(
                                                       int Fleet,
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

            SqlParameter[] prm = new SqlParameter[]
             { 
               
              
               new SqlParameter("@Fleet",Fleet),
               new SqlParameter("@Vessel_ID",Vessel_ID),
               new SqlParameter("@Department_Code",Department_Code),
               new SqlParameter("@System_Code",System_Code),
               new SqlParameter("@Subsystem_Code",Subsystem_Code),
               new SqlParameter("@Part_Number",Part_Number),
               new SqlParameter("@Drawing_Number",Drawing_Number),
               new SqlParameter("@Short_Description",Short_Description),
            
               new SqlParameter("@FROMDATE",FROMDATE),
               new SqlParameter("@TODATE",TODATE),
               new SqlParameter("@Inventory_Qty",Inventory_Qty),
               new SqlParameter("@Latest",Latest),
               new SqlParameter("return",SqlDbType.Int)
               };
            prm[prm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_Get_Inventory_Item_List_Count", prm);
            return Convert.ToInt32(prm[prm.Length - 1].Value);
        }


        public static DataSet Get_Items_ForAddAdditional_DL(string SystemCode, string SubSystemCode, string partNo, string DrawNo, string Description)
        {
            SqlParameter[] prm = new SqlParameter[]
             { 
               
               new SqlParameter("@SystemCode",SystemCode),
               new SqlParameter("@SubSystemCode",SubSystemCode),
               new SqlParameter("@partNo",partNo),
               new SqlParameter("@DrawNo",DrawNo),
               new SqlParameter("@Description",Description),

             };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_Get_Items_ForAddAdditional", prm);
        }

        public static void Update_OrderQty_From_ReqstQty_DL(string ReqsnCode, string DocumentCode, string BgtCode)
        {
            string Query = @"--update PURC_DTL_SUPPLY_ITEMS set ORDER_QTY=REQUESTED_QTY where REQUISITION_CODE='" + ReqsnCode + "' and  DOCUMENT_CODE='" + DocumentCode + @"' and ORDER_QTY is null;
                              update PURC_DTL_REQSN set BUDGET_CODE='" + BgtCode + "' where line_type='R' and  REQUISITION_CODE='" + ReqsnCode + "'";

            SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.Text, Query);
        }


        public static DataSet Get_QTN_Approver_DL(string ReqsnCode)
        {
            SqlParameter[] prm = new SqlParameter[]
             { 
               
               new SqlParameter("@ReqsnCode",ReqsnCode),
             

             };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_GET_QTN_Approver", prm);
        }

        public static string Get_BGTCode_Reqsn_DL(string ReqsnCode)
        {
            string Query = @"select top 1 BUDGET_CODE from PURC_DTL_REQSN where REQUISITION_CODE='" + ReqsnCode + "'";


            return SqlHelper.ExecuteScalar(_internalConnection, CommandType.Text, Query).ToString();
        }

        public static DataTable Get_Supplier_ValidDate_DL(string Supplier)
        {
            SqlParameter[] prm = new SqlParameter[]
             { 
               
               new SqlParameter("@Suppliers",Supplier),
             

             };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_Get_Supplier_ValidDate", prm).Tables[0];
        }

        public static int CheckHierarchy_SendForApproval_DL(string ReqsnCode, string DocumentCode, int VesselCode, string LogedInUserID, decimal RqstAmt, decimal ApproverLimit, DataTable dtQuotationList_ForTopApprover)
        {
            SqlParameter[] prm = new SqlParameter[]
             { 
               
                 new SqlParameter("@ReqsnCode",ReqsnCode),
                 new SqlParameter("@DocumentCode",DocumentCode),
                 new SqlParameter("@VesselCode",VesselCode),
                 new SqlParameter("@LogedInUserID",LogedInUserID),
           
                 new SqlParameter("@ApproverLimit",ApproverLimit),
                 new SqlParameter("@return",SqlDbType.Int),
                 new SqlParameter("@QTNCode",dtQuotationList_ForTopApprover)
             };
            prm[5].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_SP_CheckHierarchy_SendForApproval", prm);
            return Convert.ToInt32(prm[5].Value);
        }

        public static int INS_Remarks_DL(string DocCode, int UserID, string Remark, int Remark_Type)
        {
            SqlParameter[] prm = new SqlParameter[]
            {
                new SqlParameter("@DocumentCode",DocCode),
                new SqlParameter("@UserID",UserID),
                new SqlParameter("@remark",Remark),
                 new SqlParameter("@Remark_Type",Remark_Type)
               
            };
            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_SP_INS_Remarks", prm);
        }

        public static DataTable GET_Remarks_DL(string DocCode, int Remark_Type)
        {
            SqlParameter[] prm = new SqlParameter[]
            {
                new SqlParameter("@DocumentCode",DocCode),
                                 new SqlParameter("@Remark_Type",Remark_Type)
               
            };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_SP_GET_Remarks", prm).Tables[0];
        }

        public static DataTable Get_ReqsnItems_DL(string ReqsCode)
        {
            SqlParameter[] prm = new SqlParameter[]
            {
                new SqlParameter("@ReqsCode",ReqsCode)
                 
               
            };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "Purc_Get_ReqsnItems", prm).Tables[0];
        }

        public static DataTable Get_SupplierDetails_ByCode_DL(string SuppCode)
        {
            SqlParameter[] prm = new SqlParameter[]
            {
                new SqlParameter("@SuppCode",SuppCode)
                 
               
            };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_Get_SupplierDetails_ByCode", prm).Tables[0];
        }

        public static int Get_Supplier_Status_DL(string SuppCode)
        {

            SqlParameter[] prm = new SqlParameter[]
            {
                new SqlParameter("@suppCode",SuppCode),
                new SqlParameter("@return",SqlDbType.Int)
                             
            };

            prm[1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_Get_Supplier_Status", prm);
            return Convert.ToInt32(prm[1].Value);
        }

        public static DataTable Get_PONumbers_DL(string ReqsnCode)
        {
            SqlParameter[] prm = new SqlParameter[]
            {
                new SqlParameter("@ReqsnCode",ReqsnCode)
                 
               
            };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_Get_PONumbers", prm).Tables[0];
        }

        public static DataTable Get_Delivered_Requisition_Stage_DL(int? fleetid, int? vesselcode, string depttype, string deptcode
            , int? reqsntype, string req_order_code, int? pageindex, int? pagesize, ref int isfetchcount, string Sort_By, string Sort_Direction)
        {
            SqlParameter[] prm = new SqlParameter[]
            {
                 new SqlParameter("@FLEET_ID",fleetid)
                ,new SqlParameter("@VESSEL_CODE",vesselcode) 
                ,new SqlParameter("@DEPT_TYPE",depttype)
                ,new SqlParameter("@DEPT_CODE",deptcode)
                ,new SqlParameter("@REQSN_TYPE",reqsntype)
                ,new SqlParameter("@REQ_ORD_CODE",req_order_code)
                
                ,new SqlParameter("@PAGE_INDEX",pageindex)
                ,new SqlParameter("@PAGE_SIZE",pagesize)
                ,new SqlParameter("@SORT_BY",Sort_By)
                ,new SqlParameter("@SORT_DIRECTION",Sort_Direction)
                ,new SqlParameter("@IS_FETCH_COUNT",isfetchcount)
               
            };
            prm[prm.Length - 1].Direction = ParameterDirection.InputOutput;
            DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_Get_Delivered_Requisition_Stage", prm);
            isfetchcount = Convert.ToInt32(prm[prm.Length - 1].Value);
            return ds.Tables[0];
        }
        public static DataTable Get_Delivered_Requisition_Stage_DL(DataTable fleetid, DataTable vesselcode, string depttype, DataTable deptcode
           , int? reqsntype, string req_order_code, int? Diff_Qty, int? pageindex, int? pagesize, ref int isfetchcount, string Sort_By, string Sort_Direction)
        {
            SqlParameter[] prm = new SqlParameter[]
            {
                 new SqlParameter("@FLEET_ID",fleetid)
                ,new SqlParameter("@VESSEL_CODE",vesselcode) 
                ,new SqlParameter("@DEPT_TYPE",depttype)
                ,new SqlParameter("@DEPT_CODE",deptcode)
                ,new SqlParameter("@REQSN_TYPE",reqsntype)
                ,new SqlParameter("@REQ_ORD_CODE",req_order_code)
                ,new SqlParameter("@DIFF_QTY",Diff_Qty)
            
                ,new SqlParameter("@PAGE_INDEX",pageindex)
                ,new SqlParameter("@PAGE_SIZE",pagesize)
                ,new SqlParameter("@SORT_BY",Sort_By)
                ,new SqlParameter("@SORT_DIRECTION",Sort_Direction)
                ,new SqlParameter("@IS_FETCH_COUNT",isfetchcount)
               
            };
            prm[prm.Length - 1].Direction = ParameterDirection.InputOutput;
            DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_Get_Delivered_Requisition_Stage", prm);
            isfetchcount = Convert.ToInt32(prm[prm.Length - 1].Value);
            return ds.Tables[0];
        }

        public static DataTable PURC_GET_Quotation_ByReqsnCode_DL(string ReqsnCode)
        {
            SqlParameter[] prm = new SqlParameter[]
            {
                new SqlParameter("@REQSNCODE",ReqsnCode)
                 
               
            };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_GET_Quotation_ByReqsnCode", prm).Tables[0];
        }

        public static int Activate_Cancelled_Reqsn_DL(string Document_Code, int UserID)
        {
            SqlParameter[] prm = new SqlParameter[]
            {
                new SqlParameter("@DOCUMENT_CODE",Document_Code),
                new SqlParameter("@UserID",UserID),
               
               
            };
            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_Activate_Cancelled_Reqsn", prm);
        }

        public static int Activate_Cancelled_PO_DL(string Order_Code, int UserID)
        {
            SqlParameter[] prm = new SqlParameter[]
            {
                new SqlParameter("@ORDER_CODE",Order_Code),
                new SqlParameter("@UserID",UserID),
               
               
            };
            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_Activate_Cancelled_PO", prm);
        }

        public static int INS_SYNC_PO_DL(string Order_Code)
        {
            SqlParameter[] prm = new SqlParameter[]
            {
                new SqlParameter("@ORDER_CODE",Order_Code)
                              
               
            };
            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_INS_SYNC_PO", prm);
        }

        public static DataTable GET_REQSN_VMT_DL(string DocCode)
        {
            SqlParameter[] prm = new SqlParameter[]
            {
                new SqlParameter("@DOCUMENTCODE",DocCode)
             
            };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_GET_REQSN_VMT", prm).Tables[0];
        }

        public static DataTable Get_Quotation_Items_Compare_DL(string Catalogue, string ReqsnCode, string DocumentCode, string VesselCode, string SupplierCodes, string QuotationCodes, string ExchangeRates)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strTables = new StringBuilder();

            strSql.Append(@"select distinct M.ITEM_SERIAL_NO
                                        ,'False' chechstatus
                                        , a.QUOTATION_CODE
                                        ,a.ITEM_REF_CODE
                                        , a.ITEM_SHORT_DESC
                                        ,a.ITEM_FULL_DESC
                                        ,a.QUOTED_QTY
                                        ,case when item.Drawing_Number='0' then '' else item.Drawing_Number end Drawing_Number
                                        ,Item.Part_Number
                                        ,isnull(Item.Long_Description,'') Long_Description
                                        , M.ITEM_COMMENT 
                                        ,M.ORDER_UNIT_ID
                                        ,item.Unit_and_Packings
                                        ,M.ROB_Qty
                                        ,M.REQUESTED_QTY
                                        ,M.ORDER_QTY
                                        ,isnull(M.ITEM_INTERN_REF,0) as ITEM_INTERN_REF
                                        ,a.Vessel_Code
                                        ,PURC_LIB_SUBSYSTEMS.Subsystem_Description
                                        ,isnull(notdeldItems.ITEM_REF_CODE,'0') as NotDeliverd
                                        ,isnull((select active_status from purc_dtl_reqsn where order_code=m.order_code),0) as Active_PO    
                                        ,'" + Catalogue + "' as Catalogue, '" + ReqsnCode + "' as Reqsnno");

            strTables.Append(@"  from PURC_Dtl_Quoted_Prices a 
                                inner  join PURC_Lib_Items item 
                                    On item.Item_Intern_Ref=A.Item_Ref_Code 
                                inner join PURC_Dtl_Supply_Items M 
                                    on M.item_ref_code=A.item_ref_code and M.Document_Code=a.Document_Code and a.Vessel_Code=M.Vessel_Code 
                                inner join PURC_LIB_SUBSYSTEMS 
                                    on PURC_LIB_SUBSYSTEMS.Subsystem_Code  =M.ITEM_SUBSYSTEM_CODE and PURC_LIB_SUBSYSTEMS.System_Code=M.ITEM_SYSTEM_CODE 
                                 left join  ( select  ITEM_REF_CODE from  PURC_DTL_SUPPLY_ITEMS where DOCUMENT_CODE<>'" + DocumentCode + "' and DELIVERD_QTY is null and Vessel_Code=" + VesselCode + @") notdeldItems
                                    on notdeldItems.ITEM_REF_CODE=m.ITEM_REF_CODE     
                                    ");


            string[] arrQuotationCode = QuotationCodes.Split(',');
            string[] arrSupplierCode = SupplierCodes.Split(',');
            string[] arrExchRate = ExchangeRates.Split(',');

            int is_1st_Time = 1;
            int Index = 0;
            foreach (string QUOTATION_CODE in arrQuotationCode)
            {
                if (QUOTATION_CODE.Length > 4)
                {

                    string str = "";
                    string Col_supp_Alias = "";
                    string ExchRate = arrExchRate[Index];
                    string SuppCode = arrSupplierCode[Index];
                    Col_supp_Alias = "Supp" + SuppCode + Index.ToString();

                    strSql.Append(",");
                    strTables.Append(" inner Join ");

                    str = @"(select supl.ITEM_REF_CODE
                                ,( QUOTED_RATE * " + ExchRate + ") " + Col_supp_Alias + @"_Rate
                                ,QUOTED_PRICE " + Col_supp_Alias + @"_Price
                                ,QUOTED_DISCOUNT " + Col_supp_Alias + @"_Discount
                                , QUOTATION_REMARKS " + Col_supp_Alias + @"_Remark
                                , case when isnull(EVALUATION_OPTION,0)=1 then 'True' else 'False' end as " + Col_supp_Alias + @"_Status
                                ,QUOTATION_CODE
                                ,(((cast(QUOTED_RATE*" + ExchRate + " as decimal(18,2))*supl.ORDER_QTY)-(cast(QUOTED_RATE*" + ExchRate + " as decimal(18,2))*supl.ORDER_QTY*cast(QUOTED_DISCOUNT as decimal(18,2))/100))) " + Col_supp_Alias + @"_Amount
                                , isnull(Lead_Time,'') " + Col_supp_Alias + @"_Lead_Time
                                , [Description]" + Col_supp_Alias + @"_ItemType    
                                from PURC_Dtl_Quoted_Prices 
                                    inner join PURC_DTL_SUPPLY_ITEMS supl 
                                          on supl.DOCUMENT_CODE=PURC_Dtl_Quoted_Prices.DOCUMENT_CODE and supl.ITEM_REF_CODE=PURC_Dtl_Quoted_Prices.ITEM_REF_CODE  
                                    inner join PURC_LIB_SYSTEM_PARAMETERS 
                                           on Code=Item_Type where supplier_code='" + SuppCode
                        + "' and QUOTATION_CODE ='" + QUOTATION_CODE + "')  " + Col_supp_Alias + " on " + Col_supp_Alias + ".ITEM_REF_CODE = a.ITEM_REF_CODE ";
                    if (is_1st_Time == 1)
                    {
                        str += " and " + Col_supp_Alias + ".QUOTATION_CODE=a.QUOTATION_CODE ";
                    }
                    is_1st_Time = 0;

                    strTables.Append(str);
                    strSql.Append(Col_supp_Alias);
                    strSql.Append(".* ");


                    Index++;
                }
            }


            strSql.Append(" ");
            strSql.Append(strTables);

            strSql.Append("where  a.Document_code ='" + DocumentCode + "' and a.active_status=1 order by M.ITEM_SERIAL_NO");

            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.Text, strSql.ToString()).Tables[0];
        }

        public static DataSet Get_ReqsnItems_Split_ToVessel_DL(string Reqsn_Code, DataTable dtVessels)
        {

            SqlParameter[] prm = new SqlParameter[]
            {
                new SqlParameter("@REQSN_CODE",Reqsn_Code),
                new SqlParameter("@TBL_VESSELS",dtVessels)
             
            };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_GET_REQSNITEMS_SPLIT_TOVESSEL", prm);
        }


        public static DataTable UPD_Reqsn_Split_IntoVessel_DL(string Reqsn_Code, DataTable dtItemsVessels, int UserID)
        {

            SqlParameter[] prm = new SqlParameter[]
            {
                new SqlParameter("@REQSN_CODE",Reqsn_Code),
                new SqlParameter("@TBL_ITEM_VSL",dtItemsVessels),
                new SqlParameter("@Created_By",UserID)
             
            };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_UPD_REQSN_SPLIT_INTOVESSELS", prm).Tables[0];
        }

        public static int UPD_Reqsn_Split_IntoVessel_Finalize_DL(string ReqsnCode, DataTable dtChild_Reqsn_Code, int UserID)
        {
            SqlParameter[] prm = new SqlParameter[]
            {
                 new SqlParameter("@REQSN_CODE",ReqsnCode),
                 new SqlParameter("@CHILD_REQSN_CODE",dtChild_Reqsn_Code),
                  new SqlParameter("@USER_ID",UserID),
            };

            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_UPD_REQSN_SPLIT_INTOVESSELS_FINALIZE", prm);

        }

        public static DataTable Get_ReqsnItems_Split_ToVessel_Report_DL(string Reqsn_Code)
        {

            SqlParameter[] prm = new SqlParameter[]
            {
                new SqlParameter("@REQSN_CODE",Reqsn_Code),
                            
            };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_GET_REQSNITEMS_SPLIT_TOVESSEL_REPORT", prm).Tables[0];
        }


        public static DataTable Get_Bulk_Purchase_Reqsn_DL(int? fleetid, int? vesselcode, string depttype, string deptcode
          , int? reqsntype, string req_order_code, int? pageindex, int? pagesize, ref int isfetchcount)
        {

            System.Data.DataTable dtFleet = new System.Data.DataTable();

            SqlParameter[] obj = new SqlParameter[]
            {
                 new SqlParameter("@FLEET_ID",fleetid)
                ,new SqlParameter("@VESSEL_CODE",vesselcode) 
                ,new SqlParameter("@DEPT_TYPE",depttype)
                ,new SqlParameter("@DEPT_CODE",deptcode)
                ,new SqlParameter("@REQSN_TYPE",reqsntype)
                ,new SqlParameter("@REQ_ORD_CODE",req_order_code)
            
                ,new SqlParameter("@PAGE_INDEX",pageindex)
                ,new SqlParameter("@PAGE_SIZE",pagesize)
                ,new SqlParameter("@IS_FETCH_COUNT",isfetchcount)
               
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_GET_BULK_PURCHASE_REQSN", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds.Tables[0];


        }

        public static DataTable Get_Bulk_Purchase_Reqsn_DL(DataTable fleetid, DataTable vesselcode, string depttype, DataTable deptcode
         , int? reqsntype, string req_order_code, int? REQSN_STATUS, int? pageindex, int? pagesize, ref int isfetchcount)
        {

            System.Data.DataTable dtFleet = new System.Data.DataTable();

            SqlParameter[] obj = new SqlParameter[]
            {
                 new SqlParameter("@FLEET_ID",fleetid)
                ,new SqlParameter("@VESSEL_CODE",vesselcode) 
                ,new SqlParameter("@DEPT_TYPE",depttype)
                ,new SqlParameter("@DEPT_CODE",deptcode)
                ,new SqlParameter("@REQSN_TYPE",reqsntype)
                ,new SqlParameter("@REQ_ORD_CODE",req_order_code)
                ,new SqlParameter("@REQSN_STATUS",REQSN_STATUS)

                ,new SqlParameter("@PAGE_INDEX",pageindex)
                ,new SqlParameter("@PAGE_SIZE",pagesize)
                ,new SqlParameter("@IS_FETCH_COUNT",isfetchcount)
               
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_GET_BULK_PURCHASE_REQSN", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds.Tables[0];


        }




        public static DataTable Get_Split_SavedVessel_DL(string Reqsn_Code, int Finalize)
        {

            SqlParameter[] prm = new SqlParameter[]
            {
                new SqlParameter("@REQSN_CODE",Reqsn_Code),
                new SqlParameter("@FINALIZED",Finalize)
                            
            };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_GET_SPLIT_SAVEDVESSELS", prm).Tables[0];
        }

        public static int Get_Check_ReqsnValidity_DL(string Reqsn_Code)
        {
            return Convert.ToInt32(SqlHelper.ExecuteScalar(_internalConnection, CommandType.StoredProcedure, "PURC_GET_CHECK_REQSN_VALIDITY", new SqlParameter("@REQSN_CODE", Reqsn_Code)));
        }

        public static DataTable Get_SupplierList_DL(string Supplier_Category, string Supplier_Search)
        {

            SqlParameter[] prm = new SqlParameter[]
            {
                new SqlParameter("@Supplier_Search",Supplier_Search),
                 new SqlParameter("@Supplier_Category",Supplier_Category)
                            
            };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_Get_SupplierList", prm).Tables[0];
        }

        public static int Get_Bulk_Reqsn_Finalized_DL(string Reqsn_Code)
        {
            return Convert.ToInt32(SqlHelper.ExecuteScalar(_internalConnection, CommandType.StoredProcedure, "PURC_Get_BLK_Reqsn_Finalized", new SqlParameter("@Reqsn_Code", Reqsn_Code)));
        }
        public static int UPD_RollBack_Bulk_Reqsn_DL(string Reqsn_Code, int UserID)
        {
            SqlParameter[] prm = new SqlParameter[]
            {
                new SqlParameter("@Reqsn_Code",Reqsn_Code),
                 new SqlParameter("@UserID",UserID),
                 new SqlParameter("@return",SqlDbType.Int)
                            
            };
            prm[prm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_UPD_RollBack_Bulk_Reqsn", prm);
            return Convert.ToInt32(prm[prm.Length - 1].Value);
        }

        public static DataTable Get_BudgetCode_ByReqsnType(int Reqsn_Type)
        {
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_Get_BudgetCode_ByReqsnType", new SqlParameter("@Reqsn_Type", Reqsn_Type)).Tables[0];
        }

        public static DataTable Get_Reqsn_Type_Budget(string Search)
        {
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_Get_Reqsn_Type_Budget", new SqlParameter("@Search", Search)).Tables[0];
        }

        public static int Upd_Reqsn_Type_Budget(string Action, int Budget_Code, int Reqsn_Type, int? ID, int UserID)
        {

            SqlParameter[] prm = new SqlParameter[]
            {
                new SqlParameter("@Action",Action),
                new SqlParameter("@UserID",UserID),
                new SqlParameter("@Budget_Code",Budget_Code),
                new SqlParameter("@Reqsn_Type",Reqsn_Type),
                new SqlParameter("@ID",ID)
            };

            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_UPD_Reqsn_Type_Budget", prm);

        }

        public static int Get_Reqsn_IsValidToClose(string Requisition_Code)
        {
            return Convert.ToInt32(SqlHelper.ExecuteScalar(_internalConnection, CommandType.StoredProcedure, "PURC_GET_Reqsn_IsValidToClose", new SqlParameter("@REQUISITION_CODE", Requisition_Code)));
        }

        public static int Upd_Close_Requisition(string Requisition_Code, int UserID)
        {
            SqlParameter[] prm = new SqlParameter[] { new SqlParameter("@REQUISITION_CODE", Requisition_Code), new SqlParameter("@UserID", UserID) };
            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_UPD_Close_Requisition", prm);
        }


        public static DataTable Get_Provision_Limit_Vessels()
        {
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_GET_Provision_Limit_Vessels").Tables[0];
        }

        public static DataSet Get_Provisions_Approval_Limit(DataTable dtVessels, int Page_Index, int Page_Size, string SubCatalogue, string Search, int? MaxQty)
        {
            SqlParameter[] prm = new SqlParameter[]{new SqlParameter("@TBL_VESSELS", dtVessels),
                                                    new SqlParameter("@PAGE_INDEX",Page_Index)
                                                    ,new SqlParameter("@PAGE_SIZE",Page_Size)
                                                    ,new SqlParameter("@SubCatalogue",SubCatalogue)
                                                    ,new SqlParameter("@Search",Search)
                                                    ,new SqlParameter("@MaxQty",MaxQty)
                                                   
                                                   };

            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_GET_Provisions_Approval_Limit", prm);


        }

        public static int Upd_Provisions_Approval_Limit(string Item_Ref_Code, decimal Max_Qty, decimal Max_Cost, int UserID, int Vessel_ID)
        {
            SqlParameter[] prm = new SqlParameter[]{ new SqlParameter("@Item_Ref_Code", Item_Ref_Code), 
                                                     new SqlParameter("@Max_Qty", Max_Qty) ,
                                                     new SqlParameter("@Max_Cost", Max_Cost) ,
                                                     new SqlParameter("@UserID", UserID) ,
                                                     new SqlParameter("@Vessel_ID", Vessel_ID) ,
                                                     new SqlParameter("@return",SqlDbType.Int)
                                                    };

            prm[prm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_UPD_Provisions_Approval_Limit", prm);
            return Convert.ToInt32(prm[prm.Length - 1].Value);


        }

        public static int Upd_Copy_Provisions_Approval_Limit(int Assigned_Vessel_ID	,DataTable tbl_Selected_Vessels,int UserID	)
        {
            SqlParameter[] prm = new SqlParameter[]{ new SqlParameter("@Assigned_Vessel_ID", Assigned_Vessel_ID), 
                                                     new SqlParameter("@tbl_Selected_Vessels", tbl_Selected_Vessels) ,
                                                     new SqlParameter("@UserID", UserID) 
                                                 };
            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_UPD_Copy_Provisions_Approval_Limit", prm);
            
        }

        public static DataTable Get_Check_Provision_Limit(string Document_Code)
        {

            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "GET_Check_Provision_Limit", new SqlParameter("@Document_Code", Document_Code)).Tables[0];

        }

        public static void UpdateIsMeat(string id, int isMeat, int USERID)
        {
            SqlParameter[] prm = new SqlParameter[]{ 
                                                     new SqlParameter("@id", id), 
                                                     new SqlParameter("@isMeat", isMeat) ,
                                                     new SqlParameter("@USERID", USERID)  
                                                    };


            SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_UPD_Meat_Item", prm);

        }
        public static void UpdateSlopChestItems(string id, bool isSlopChest, int UserID)
        {
             SqlParameter[] prm = new SqlParameter[]{ 
                                                     new SqlParameter("@id", id), 
                                                     new SqlParameter("@IsSlopchest", isSlopChest) ,
                                                     new SqlParameter("@USERID", UserID)  
                                                    };


             SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_UPD_SlopChest_Item", prm);
            
        }


        public static DataTable Get_LIB_Page_Config(int ClientID, string KeyValue)
        {
            SqlParameter[] prm = new SqlParameter[]{ 
                                                     
                                                     new SqlParameter("@ClientID", ClientID) ,
                                                     new SqlParameter("@KeyValue", KeyValue)  
                                                    };

            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "Get_LIB_Page_Config", prm).Tables[0];
        }
        #region Grading with option

        public static int INS_UPD_DEL_Grading_DL(int? ID, string Grade_Name, int? GradeType, int? Min, int? Max, int? Division, int UserID, string Mode)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ID",ID),
                                            new SqlParameter("@Grade_Name",Grade_Name),
                                            new SqlParameter("@Grade_Type",GradeType),
                                            new SqlParameter("@Min",Min),
                                            new SqlParameter("@Max",Max),
                                            new SqlParameter("@Divisions",Division),
                                            new SqlParameter("@User_ID",UserID),
                                            new SqlParameter("@Mode",Mode),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_INS_GRADING", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public static int INS_UPD_DEL_GradingOption_DL(int Grade_ID, string OptionText, decimal OptionValue, int Created_By)
        {
           
            SqlParameter[] sqlprm = new SqlParameter[]
                                            { 
                                                new SqlParameter("@Grade_ID",Grade_ID),
                                                new SqlParameter("@OptionText",OptionText),
                                                new SqlParameter("@OptionValue",OptionValue),                                            
                                                new SqlParameter("@User_ID",Created_By),
                                                new SqlParameter("@Mode","A"),
                                                new SqlParameter("return",SqlDbType.Int)
                                            };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_INS_GRADINGOPTIONS", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        public static DataTable Get_GradingList_DL()
        {
           
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_GET_GRADING").Tables[0];
        }

        public static DataTable Get_GradingOptions_DL(int Grade_ID)
        {
            string connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
            SqlParameter[] sqlprm = new SqlParameter[]
            { 
                new SqlParameter("@Grade_ID",Grade_ID)
            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "PURC_GET_GRADINGOPTIONS", sqlprm).Tables[0];
        }
        #endregion


        #region Question
        public static int INS_UPD_DEL_Question_DL(int? ID, string Question, int? Grading_Type,int? Type_ID,int UserID, string Mode)
        {
            
                SqlParameter[] sqlprm = new SqlParameter[]
                { 
                    new SqlParameter("@ID",ID),
                    new SqlParameter("@Question",Question),
                    new SqlParameter("@Grading_Type",Grading_Type),
                    new SqlParameter("@Type_ID",Type_ID),
                    new SqlParameter("@User_ID",UserID),
                    new SqlParameter("@Mode",Mode),
                    new SqlParameter("return",SqlDbType.Int)
                };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_INS_QUESTION", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        public static DataTable Get_QuestionList_DL(string searchtext,string dept)
        {
             SqlParameter[] sqlprm = new SqlParameter[]
            { 
                new SqlParameter("@Search_Text",searchtext),
                new SqlParameter("@type_id",dept),
            };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_GET_QUESTION_LIST", sqlprm).Tables[0];
        }
        
        public static DataTable Get_GradingType()
        {
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_GET_GRADING_TYPE").Tables[0];
        }
        #endregion


        #region CancelPO
        public static int PURC_UPD_CancelPO_FilePath(string OrderCode,string FileName,string FilePath,string Remark,int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            { 
                new SqlParameter("@Order_Code",OrderCode),
                new SqlParameter("@File_Name",FileName),
                new SqlParameter("@File_Path",FilePath),
                new SqlParameter("@Remark",Remark),
                new SqlParameter("@UserID",UserID),

            };
            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_UPD_CancelPO_FilePath", sqlprm);
        }

        #endregion


        public static int PURC_Get_Active_PO_Count(string Order_Code)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            { 
                new SqlParameter("@Order_Code",Order_Code),
                new SqlParameter("return",SqlDbType.Int)
            };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "purc_get_active_po_count", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
            
        }

        public static DataTable Get_Purc_Items(string Reqsn_Code)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            { 
                new SqlParameter("@REQUISITION_CODE",Reqsn_Code)
            };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_GET_REQUISITION_ITEMS", sqlprm).Tables[0];
        }
        #region PO_Log
        public static DataSet PURC_Get_Type(int? UserID, string FilterType)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@UserID", UserID),
                new System.Data.SqlClient.SqlParameter("@FilterType", FilterType),
            };

            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "POLOG_Get_Type", obj);
        }
        public static DataSet PURC_Get_AccountClassification(int? UserID, string POType,string ReqnType)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@UserID", UserID),
                new System.Data.SqlClient.SqlParameter("@POType", POType),
                new System.Data.SqlClient.SqlParameter("@ReqnType", ReqnType),
            };

            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_Get_Account_Classification", obj);
        }
        public static DataSet PURC_Get_Configuration(string PO_Type)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@PO_Type", PO_Type),
            };

            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_Get_PO_Config", obj);
        }
        public static DataSet SelectCatalog(int? UserID, string Reqn_Type, string Function_Type, int Vessel_ID)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@UserID", UserID),
                new System.Data.SqlClient.SqlParameter("@Reqn_Type", Reqn_Type),
                new System.Data.SqlClient.SqlParameter("@Function_Type", Function_Type),
                new System.Data.SqlClient.SqlParameter("@Vessel_ID", Vessel_ID),
            };

            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_Get_Catalouge", obj);
        }
        public static DataSet PURC_Checking_Requisition(IventoryItemData objDOInventoryItem)
        {
            try
            {

                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] { 
                      new System.Data.SqlClient.SqlParameter("@PO_Type", objDOInventoryItem.POType),
                   new System.Data.SqlClient.SqlParameter("@VESSEL_CODE", objDOInventoryItem.VesselCode),
                   new System.Data.SqlClient.SqlParameter("@DEPARTMENT", objDOInventoryItem.Department),
                   //new System.Data.SqlClient.SqlParameter("@DOCUMENT_CODE", objDOInventoryItem.DocumentCode),
                   new System.Data.SqlClient.SqlParameter("@DOCUMENT_NO", objDOInventoryItem.DocumentNumber),
                   new System.Data.SqlClient.SqlParameter("@TOTAL_ITEMS", objDOInventoryItem.Totalitem),
                   new System.Data.SqlClient.SqlParameter("@LINE_TYPE", objDOInventoryItem.LineType),
                   new System.Data.SqlClient.SqlParameter("@Created_By", objDOInventoryItem.CreatedBy),
                   new System.Data.SqlClient.SqlParameter("@ReqType", objDOInventoryItem.RequisitionType),
                   new System.Data.SqlClient.SqlParameter("@USER", objDOInventoryItem.UserName),
                   new System.Data.SqlClient.SqlParameter("@URGENCY_CODE", objDOInventoryItem.UrgencyCode),
                   new System.Data.SqlClient.SqlParameter("@Reqstcatalouge", objDOInventoryItem.SystemCode),
                   new System.Data.SqlClient.SqlParameter("@Account_Type",objDOInventoryItem.Account_Type),
                   new System.Data.SqlClient.SqlParameter("@Delivery_Port",objDOInventoryItem.Delivery_Port),
                   new System.Data.SqlClient.SqlParameter("@Delivery_Date",objDOInventoryItem.Delivery_Date),
                   new System.Data.SqlClient.SqlParameter("@Port_Call",objDOInventoryItem.Port_Call),
                   new System.Data.SqlClient.SqlParameter("@Owner_Code",objDOInventoryItem.Owner_Code),
                };

                //obj[11].Direction = ParameterDirection.ReturnValue;
                return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_Check_Requisition_Number", obj);
                //string ReqturnId = Convert.ToString(ds.Tables[0].Rows[0]["DOCUMENT_CODE"]);
                //return ReqturnId;
            }
            catch (Exception ex)
            {

                throw ex;


            }

        }
        public static DataSet PURC_Get_RequisitionDeatils(int? UserID, string DocumentCode)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@UserID", UserID),
                new System.Data.SqlClient.SqlParameter("@DocumentCode", DocumentCode),
            };

            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_Get_Requisition_Deatils", obj);
        }
       
        public static int ItemImageUpdate(int userid, string itemid, string systemcode, string subsystemcode, string image_url, string product_details)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@USERID", userid ),
                   new System.Data.SqlClient.SqlParameter("@ITEMID", itemid ),
                   new System.Data.SqlClient.SqlParameter("@SYSTEM_CODE", systemcode ),
                   new System.Data.SqlClient.SqlParameter("@SUBSYSTEM_CODE", subsystemcode),
                   
                   new System.Data.SqlClient.SqlParameter("@Image_Url", image_url),
                   new System.Data.SqlClient.SqlParameter("@Product_Details", product_details),
            };

            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_PR_ITEMSIMAGEUPDATE", obj);
        }
        public static DataTable PURC_Get_PO_Type(int UserID, string AccessType)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@UserID", UserID),
                new System.Data.SqlClient.SqlParameter("@AccessType", AccessType)
            };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_GET_PO_TYPE", obj).Tables[0];
        }
        public static DataTable PURC_Get_Sys_Variable(int UserID, string FilterType)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@UserID", UserID),
                new System.Data.SqlClient.SqlParameter("@FilterType", FilterType)
            };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_Get_Sys_Variable", obj).Tables[0];
        }
        public static DataTable PURC_Get_Supplier_Type(int UserID, string FilterType)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@UserID", UserID),
                new System.Data.SqlClient.SqlParameter("@FilterType", FilterType)
            };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_Get_Supplier_Type", obj).Tables[0];
        }
        public static DataTable PURC_Get_UserTypeAccess(int UserID, string Variable_Type)
        {
            SqlParameter[] obj = new SqlParameter[] { 
                 new SqlParameter("@userid", UserID),
                 new SqlParameter("@Variable_Type", Variable_Type),
                
            };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_Get_UserAccessTypeWise", obj).Tables[0];
        }
        #endregion



        public static DataSet PURC_Get_Requisition_Type(string PO_Type,int?User_ID)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@PO_Type", PO_Type),
                new System.Data.SqlClient.SqlParameter("@User_ID", User_ID)
            };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_GET_REQSN_TYPE", obj);
        }

        public static DataSet Filter_Catalog(int? UserID, string Function_Type)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@UserID", UserID),
                new System.Data.SqlClient.SqlParameter("@Function_Type", Function_Type)
            };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_FILTER_CATLOGUE", obj);
        }

        public static DataSet PURC_Filter_AccountClassification(int? UserID, string POType, string ReqnType)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@UserID", UserID),
                new System.Data.SqlClient.SqlParameter("@POType", POType),
                new System.Data.SqlClient.SqlParameter("@ReqnType", ReqnType),
            };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_FILTER_ACC_CLASSIFICATION", obj);
        }
        public static DataTable Get_Delivered_Requisition_Stage_DL_New(string fleetid, string vesselcode, string depttype, string deptcode, string req_order_code, string potype, string acctype, string reqsntype, string catalogue, string FromDate, string ToDate, string accClass, string dtUrgency, string reqstatus, string Supplier,string Delivery_Code, int? pageindex, int? pagesize, ref int isfetchcount, string Sort_By, string Sort_Direction)
        {
            SqlParameter[] prm = new SqlParameter[]
            {
                new SqlParameter("@FLEET_ID",fleetid)
                ,new SqlParameter("@VESSEL_CODE",vesselcode) 
                ,new SqlParameter("@DEPT_CODE",deptcode)

                ,new SqlParameter("@PO_TYPE",potype)
                ,new SqlParameter("@ACC_TYPE",acctype)
                ,new SqlParameter("@REQSN_TYPE",reqsntype)
                ,new SqlParameter("@SYSTEM",catalogue)


                ,new SqlParameter("@FROM_DATE",FromDate)
                ,new SqlParameter("@TO_DATE",ToDate)
                ,new SqlParameter("@ACC_CLASSIFICATION",accClass)
                ,new SqlParameter("@URGENCY_LVL",dtUrgency)
                ,new SqlParameter("@REQ_STATUS",reqstatus)

                ,new SqlParameter("@DEPT_TYPE",depttype)
                ,new SqlParameter("@REQ_ORD_CODE",req_order_code)

                ,new SqlParameter("@SUPPLIER",Supplier)
                ,new SqlParameter("@DELIVERY_CODE",Delivery_Code)
                

                ,new SqlParameter("@PAGE_INDEX",pageindex)
                ,new SqlParameter("@PAGE_SIZE",pagesize)
                ,new SqlParameter("@SORT_BY",Sort_By)
                ,new SqlParameter("@SORT_DIRECTION",Sort_Direction)
                ,new SqlParameter("@IS_FETCH_COUNT",isfetchcount)
               
            };
            prm[prm.Length - 1].Direction = ParameterDirection.InputOutput;
            DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_Get_Delivered_Requisition_Stage_New", prm);
            isfetchcount = Convert.ToInt32(prm[prm.Length - 1].Value);
            return ds.Tables[0];
        }

        public static DataTable Get_CancelReqsn_DL_New(string fleetid, string vesselcode, string depttype, string deptcode, string req_order_code, string potype, string acctype, string reqsntype, string catalogue, string FromDate, string ToDate, string accClass, string dtUrgency, string reqstatus, int? pageindex, int? pagesize, ref int isfetchcount, string Sort_By, string Sort_Direction)
        {
            SqlParameter[] obj = new SqlParameter[]
            {
                new SqlParameter("@FLEET_ID",fleetid)
                ,new SqlParameter("@VESSEL_CODE",vesselcode) 
                 ,new SqlParameter("@DEPT_CODE",deptcode)
               
               
                ,new SqlParameter("@PO_TYPE",potype)
                ,new SqlParameter("@ACC_TYPE",acctype)
                ,new SqlParameter("@REQSN_TYPE",reqsntype)
                ,new SqlParameter("@SYSTEM",catalogue)


                ,new SqlParameter("@FROM_DATE",FromDate)
                ,new SqlParameter("@TO_DATE",ToDate)
                ,new SqlParameter("@ACC_CLASSIFICATION",accClass)
                ,new SqlParameter("@URGENCY_LVL",dtUrgency)
                ,new SqlParameter("@REQ_STATUS",reqstatus)

                 ,new SqlParameter("@DEPT_TYPE",depttype)
                ,new SqlParameter("@REQ_ORD_CODE",req_order_code)
            
                ,new SqlParameter("@PAGE_INDEX",pageindex)
                ,new SqlParameter("@PAGE_SIZE",pagesize)
                ,new SqlParameter("@SORT_BY",Sort_By)
                ,new SqlParameter("@SORT_DIRECTION",Sort_Direction)
                ,new SqlParameter("@IS_FETCH_COUNT",isfetchcount)
               
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_GET_CancelLog_New", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds.Tables[0];
        }

        public static DataTable Get_User_Vessel_Assign(int Vessel_ID, int UserID)
        {
            SqlParameter[] obj = new SqlParameter[]
            {
                new SqlParameter("@Vessel_ID",Vessel_ID)
               ,new SqlParameter("@User_ID",UserID) 
            };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_GET_USER_VESSEL_ASSIGNMENT", obj).Tables[0];
        }

        public static DataTable Get_Function(int UserID, int ReqsnType)
        {
            SqlParameter[] obj = new SqlParameter[]
            {
                new SqlParameter("@UserID",UserID)
               ,new SqlParameter("@Reqsn_Type",ReqsnType) 
            };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_Get_Function", obj).Tables[0];
        }
        public static DataSet PURC_INS_Duplicate_RequisitionDeatils(string DocumentCode, int VESSEL_CODE, int Delivery_Port, string Delivery_Date, int? UserID, string user_Name)
        {
            SqlParameter[] prm = new SqlParameter[]
            {
              
                new SqlParameter("@Document_Code",DocumentCode),
                new SqlParameter("@VESSEL_CODE",VESSEL_CODE),
                new SqlParameter("@Delivery_Port",Delivery_Port),
                new SqlParameter("@Delivery_Date",Delivery_Date),
                 new SqlParameter("@UserID",UserID),
                  new SqlParameter("@USER",user_Name)
            };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_INS_Duplicate_Requisition", prm);
        }
        public static DataTable PURC_Get_ItemCategory(int UserID, string FilterType)
        {
            SqlParameter[] obj = new SqlParameter[]
            {
                new SqlParameter("@UserID",UserID)
               ,new SqlParameter("@FilterType",FilterType) 
            };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_Get_ItemCategory", obj).Tables[0];
        }
        public static DataTable GET_Buyer_Remarks(string DocCode)
        {
            SqlParameter[] prm = new SqlParameter[]
            {
                new SqlParameter("@DocumentCode",DocCode)
            };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_GET_BUYER_REMARKS", prm).Tables[0];
        }
    }
}
