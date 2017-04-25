using System;
using System.Configuration;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using SMS.Data;
using SMS.Properties;

namespace SMS.Data.PURC
{
    public class DAL_PURC_InventoryItem
    {
        private static string _internalConnection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;

        public DAL_PURC_InventoryItem()
        {
        }



        public static DataTable SelectItemForInventory(string CatalogID, string SubCatalg, string ItemDesc, string PartNo, string DrawNo, string ReqsnCode, int PageIndex, int PageSize)
        {
            try
            {
                DataTable dtDept = new System.Data.DataTable();

                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[]
             { 
               new System.Data.SqlClient.SqlParameter("@CatalogID",CatalogID),
               new System.Data.SqlClient.SqlParameter("@SubCatgID",SubCatalg),   
               new System.Data.SqlClient.SqlParameter("@ItemDescp",ItemDesc),   
               new System.Data.SqlClient.SqlParameter("@PartNo",PartNo),   
               new System.Data.SqlClient.SqlParameter("@DrawNo",DrawNo), 
               new System.Data.SqlClient.SqlParameter("@ReqsnCode",ReqsnCode),   
               new System.Data.SqlClient.SqlParameter("@PageIndex",PageIndex),   
               new System.Data.SqlClient.SqlParameter("@PageSize",PageSize),   
             };
                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_SP_Get_InventoryItems", obj);
                dtDept = ds.Tables[0];
                return dtDept;

            }
            catch (Exception ex)
            {
                throw ex;
            }


        }


        public static DataTable Get_Create_New_Requisition(string CatalogID, string SubCatalg, string ItemDesc, string PartNo, string DrawNo, string DocCode, int PageIndex, int PageSize, ref int count)
        {
            try
            {
                DataTable dtDept = new System.Data.DataTable();

                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[]
             { 
               new System.Data.SqlClient.SqlParameter("@CatalogID",CatalogID),
               new System.Data.SqlClient.SqlParameter("@SubCatgID",SubCatalg),   
               new System.Data.SqlClient.SqlParameter("@ItemDescp",ItemDesc),   
               new System.Data.SqlClient.SqlParameter("@PartNo",PartNo),   
               new System.Data.SqlClient.SqlParameter("@DrawNo",DrawNo), 
               new System.Data.SqlClient.SqlParameter("@DOCUMENT_CODE",DocCode),   
               new System.Data.SqlClient.SqlParameter("@PageIndex",PageIndex),   
               new System.Data.SqlClient.SqlParameter("@PageSize",PageSize), 
               new SqlParameter("@RecordCount",SqlDbType.Int)
             };

                obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
                dtDept = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_Get_Create_New_Requisition", obj).Tables[0];
                count = Convert.ToInt32(obj[obj.Length - 1].Value);
                return dtDept;

            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public static int SelectItemForInventory_Count(string CatalogID, string SubCatalg, string ItemDesc, string PartNo, string DrawNo, string ReqsnCode)
        {
            try
            {
                DataTable dtDept = new System.Data.DataTable();

                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[]
             { 
               new System.Data.SqlClient.SqlParameter("@CatalogID",CatalogID),
               new System.Data.SqlClient.SqlParameter("@SubCatgID",SubCatalg),   
               new System.Data.SqlClient.SqlParameter("@ItemDescp",ItemDesc),   
               new System.Data.SqlClient.SqlParameter("@PartNo",PartNo),   
               new System.Data.SqlClient.SqlParameter("@DrawNo",DrawNo), 
               new System.Data.SqlClient.SqlParameter("@ReqsnCode",ReqsnCode) 
              
             };
               return  int.Parse( SqlHelper.ExecuteScalar(_internalConnection, CommandType.StoredProcedure, "PURC_SP_Get_InventoryItems_Count", obj).ToString());
                

            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public DataTable SelectItemForInventoryFilter(string RequisitionCode, string VesselCode, string Document_code, string SystemCode)
        {
            System.Data.DataTable dtInvReq = new System.Data.DataTable();
            try
            {
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[]
             { 
               new System.Data.SqlClient.SqlParameter("@RequiCode",RequisitionCode),
                new System.Data.SqlClient.SqlParameter("@VesselCode",VesselCode)   ,
                 new System.Data.SqlClient.SqlParameter("@DocumentCode",Document_code),
                 new SqlParameter("@SystemCode",SystemCode)
             };

                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_SP_Get_Requistion_ReqItems", obj);
                dtInvReq = ds.Tables[0];
                return dtInvReq;


                //obj.Value = "select  REQUISITION_CODE,isnull(REQUESTED_QTY,0) as Required,isnull([Drawing Link],'') as link,isnull(ITEM_COMMENT,'') as Item_comments,b.Item_Intern_Ref,b.[ID],isnull(b.[Unit_and_Packings],'') as Unit_and_Packings,b.[Vessel_Code],b.[Department_Code],b.[System_Code],b.Subsystem_Code,b.[Item_Ref_Code],b.[Inventory_Date],isnull(b.[Inventory_Qty],0) [Inventory_Qty] ,isnull(b.[TO_Repair_Qty],0) [TO_Repair_Qty],isnull(b.[Working_Qty],0) [Working_Qty],isnull(b.[Min_Qty],0) [Min_Qty],isnull(b.[Max_Qty],0) [Max_Qty],isnull(b.[Delivered_Items],0) [Delivered_Items],b.[Inventory_Type],b.[Used_Items],b.[Prev_Item_Ref_Code] ,b.[Active_Status], b.Short_Description, b.Long_Description,b.Drawing_Number,b.Item_Address  from (SELECT lib_item.Item_Intern_Ref, lib_item.[ID],isnull(lib_item.[Unit_and_Packings],'') as Unit_and_Packings,vs_inv.[Vessel_Code],vs_inv.[Department_Code],lib_item.[System_Code],lib_item.Subsystem_Code,vs_inv.[Item_Ref_Code],vs_inv.[Inventory_Date],vs_inv.[Inventory_Qty],vs_inv.[TO_Repair_Qty],vs_inv.[Working_Qty],vs_inv.[Min_Qty],vs_inv.[Max_Qty],vs_inv.[Delivered_Items],vs_inv.[Inventory_Type],vs_inv.[Used_Items],vs_inv.[Prev_Item_Ref_Code] ,vs_inv.[Active_Status], lib_item.Short_Description, lib_item.Long_Description,lib_item.Drawing_Number,lib_item.Item_Address FROM (SELECT [Min_Qty],[Max_Qty],[Delivered_Items],[Inventory_Type],[Used_Items],[Prev_Item_Ref_Code], [Vessel_Code],[Department_Code],Item_Ref_Code,Inventory_Date,[Inventory_Qty],[TO_Repair_Qty],[Working_Qty],[Active_Status] FROM (SELECT ROW_NUMBER() OVER ( PARTITION BY Item_Ref_Code ORDER BY Item_Ref_Code,Inventory_Date DESC ) AS 'RowNumber',[Min_Qty],[Max_Qty],[Delivered_Items],[Inventory_Type],[Used_Items],[Prev_Item_Ref_Code],[Vessel_Code],[Department_Code],Item_Ref_Code,Inventory_Date,[Inventory_Qty],[TO_Repair_Qty],[Working_Qty],[Active_Status] FROM PURC_DTL_VESSELS_INVENTORY where Inventory_Date is not null  ) dt WHERE RowNumber <= 1 ) vs_inv RIGHT OUTER join PURC_LIB_ITEMS lib_item  on lib_item.Item_Intern_Ref=vs_inv.Item_Ref_Code and vs_inv.[Active_Status]=lib_item.[Active_Status] where lib_item.[Active_Status]=1) b left outer join (SELECT Item_Ref_Code,REQUISITION_CODE,REQUESTED_QTY,ITEM_COMMENT,[Drawing Link] FROM (SELECT ROW_NUMBER() OVER ( PARTITION BY Item_Ref_Code ORDER BY Item_Ref_Code,REQUISITION_CODE DESC ) AS 'RowNumber',Item_Ref_Code,REQUISITION_CODE,REQUESTED_QTY,ITEM_COMMENT,[Drawing Link] FROM dbo.PURC_DTL_SUPPLY_ITEMS where REQUISITION_CODE ='" + RequisitionCode + "' ) dt WHERE RowNumber <= 1 ) a on a.ITEM_REF_CODE=b.Item_Intern_Ref";
                //////obj.Value = "SELECT row_Number() over(order by Vessel_Code)  as row_number,[Vessel_Code], [Vessel_Short_Name] FROM Lib_Vessels where [Active_Status]=1 ";
                ////obj.Value = "SELECT vs_inv.[ID],vs_inv.[Vessel_Code],vs_inv.[Department_Code],vs_inv.[System_Code],vs_inv.Subsystem_Code,vs_inv.[Item_Ref_Code],vs_inv.[Inventory_Date],vs_inv.[Inventory_Qty],vs_inv.[TO_Repair_Qty],vs_inv.[Working_Qty],vs_inv.[Min_Qty],vs_inv.[Max_Qty],vs_inv.[Delivered_Items],vs_inv.[Inventory_Type],vs_inv.[Used_Items],vs_inv.[Prev_Item_Ref_Code] ,vs_inv.[Active_Status],lib_item.Short_Description, lib_item.Long_Description,lib_item.Drawing_Number,lib_item.Item_Address ,lib_item.Unit_and_Packings   FROM [eLog].[dbo].[PURC_DTL_VESSELS_INVENTORY] vs_inv inner join PURC_LIB_ITEMS lib_item on lib_item.Item_Intern_Ref=vs_inv.Item_Ref_Code and vs_inv.[Active_Status]=lib_item.[Active_Status] where lib_item.[Active_Status]=1";
                //System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "[ExeQuery]", obj);
                //dt = ds.Tables[0];
                //return dt;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public DataTable SelectItemForInventoryForRequisition(string Vessel_Code)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            try
            {
                System.Data.SqlClient.SqlParameter obj = new System.Data.SqlClient.SqlParameter("@VesselCode", Vessel_Code);
                //obj.Value = Vessel_Code;
                //System.Data.SqlClient.SqlParameter obj = new System.Data.SqlClient.SqlParameter("@StringQuery", SqlDbType.VarChar);
                //obj.Value = "select distinct PURC_DTL_SUPPLY_ITEMS.ITEM_SYSTEM_CODE, PURC_DTL_REQSN.requisition_code + '/' + right(year(PURC_DTL_REQSN.document_Date),2) +' ' + PURC_DTL_REQSN.[user] +' ' + convert( varchar(10), PURC_DTL_REQSN.document_Date,10) +' Urgency: ' +  case PURC_DTL_REQSN.URGENCY_CODE   when 'N' Then 'Normal'  when 'U' Then 'Urgent'  end requisition_code,PURC_DTL_REQSN.DOCUMENT_CODE  from [PURC_DTL_REQSN]  inner join PURC_DTL_SUPPLY_ITEMS on  PURC_DTL_SUPPLY_ITEMS.REQUISITION_CODE=PURC_DTL_REQSN.requisition_code and PURC_DTL_SUPPLY_ITEMS.Vessel_Code=PURC_DTL_REQSN.Vessel_Code where PURC_DTL_REQSN.SENT_TO is null and PURC_DTL_REQSN.LINE_TYPE='R'  and (PURC_DTL_REQSN.requisition_code Like 'OST%' Or PURC_DTL_REQSN.requisition_code Like 'OSP%') and PURC_DTL_REQSN.requisition_code not in (select requisition_code from  PURC_DTL_REQSN where line_type='Q' and Vessel_Code ='" + Vessel_Code.ToString() + "')and PURC_DTL_REQSN.Vessel_Code ='" + Vessel_Code.ToString() + "'";
                ////obj.Value = "select distinct PURC_DTL_SUPPLY_ITEMS.ITEM_SYSTEM_CODE, PURC_DTL_REQSN.requisition_code + '/' + right(year(PURC_DTL_REQSN.document_Date),2) +' ' + PURC_DTL_REQSN.[user] +' ' + convert( varchar(10), PURC_DTL_REQSN.document_Date,10) +' Urgency: ' +  case PURC_DTL_REQSN.URGENCY_CODE   when 'N' Then 'Normal'  when 'U' Then 'Urgent'  end requisition_code  from [PURC_DTL_REQSN]  inner join PURC_DTL_SUPPLY_ITEMS on  PURC_DTL_SUPPLY_ITEMS.REQUISITION_CODE=PURC_DTL_REQSN.requisition_code where PURC_DTL_REQSN.SENT_TO is null and PURC_DTL_REQSN.LINE_TYPE='R' and (PURC_DTL_REQSN.requisition_code Like 'OST%' Or PURC_DTL_REQSN.requisition_code Like 'OSP%') and PURC_DTL_REQSN.requisition_code not in (select requisition_code from  PURC_DTL_REQSN where line_type='Q')";
                //obj.Value = "select  REQUISITION_CODE,isnull(REQUESTED_QTY,0) as Required,isnull([Drawing Link],'') as link,isnull(ITEM_COMMENT,'') as Item_comments,b.* from (SELECT  lib_item.[ID],isnull(lib_item.[Unit_and_Packings],'') as Unit_and_Packings,vs_inv.[Vessel_Code],vs_inv.[Department_Code],lib_item.[System_Code],lib_item.Subsystem_Code,vs_inv.[Item_Ref_Code],vs_inv.[Inventory_Date],vs_inv.[Inventory_Qty],vs_inv.[TO_Repair_Qty],vs_inv.[Working_Qty],vs_inv.[Min_Qty],vs_inv.[Max_Qty],vs_inv.[Delivered_Items],vs_inv.[Inventory_Type],vs_inv.[Used_Items],vs_inv.[Prev_Item_Ref_Code] ,vs_inv.[Active_Status], lib_item.Short_Description, lib_item.Long_Description,lib_item.Drawing_Number,lib_item.Item_Address FROM (SELECT [Min_Qty],[Max_Qty],[Delivered_Items],[Inventory_Type],[Used_Items],[Prev_Item_Ref_Code], [Vessel_Code],[Department_Code],Item_Ref_Code,Inventory_Date,[Inventory_Qty],[TO_Repair_Qty],[Working_Qty],[Active_Status] FROM (SELECT ROW_NUMBER() OVER ( PARTITION BY Item_Ref_Code ORDER BY Item_Ref_Code,Inventory_Date DESC ) AS 'RowNumber',[Min_Qty],[Max_Qty],[Delivered_Items],[Inventory_Type],[Used_Items],[Prev_Item_Ref_Code],[Vessel_Code],[Department_Code],Item_Ref_Code,Inventory_Date,[Inventory_Qty],[TO_Repair_Qty],[Working_Qty],[Active_Status] FROM PURC_DTL_VESSELS_INVENTORY where Inventory_Date is not null  ) dt WHERE RowNumber <= 1 ) vs_inv inner join PURC_LIB_ITEMS lib_item  on lib_item.Item_Intern_Ref=vs_inv.Item_Ref_Code and vs_inv.[Active_Status]=lib_item.[Active_Status] where lib_item.[Active_Status]=1) b left outer join (SELECT Item_Ref_Code,REQUISITION_CODE,REQUESTED_QTY,ITEM_COMMENT,[Drawing Link] FROM (SELECT ROW_NUMBER() OVER ( PARTITION BY Item_Ref_Code ORDER BY Item_Ref_Code,REQUISITION_CODE DESC ) AS 'RowNumber',Item_Ref_Code,REQUISITION_CODE,REQUESTED_QTY,ITEM_COMMENT,[Drawing Link] FROM dbo.PURC_DTL_SUPPLY_ITEMS where REQUISITION_CODE ='" + reqscode +"') dt WHERE RowNumber <= 1 ) a on a.ITEM_REF_CODE=b.ITEM_REF_CODE";
                ////obj.Value = "SELECT row_Number() over(order by Vessel_Code)  as row_number,[Vessel_Code], [Vessel_Short_Name] FROM Lib_Vessels where [Active_Status]=1 ";
                //obj.Value = "SELECT vs_inv.[ID],vs_inv.[Vessel_Code],vs_inv.[Department_Code],vs_inv.[System_Code],vs_inv.Subsystem_Code,vs_inv.[Item_Ref_Code],vs_inv.[Inventory_Date],vs_inv.[Inventory_Qty],vs_inv.[TO_Repair_Qty],vs_inv.[Working_Qty],vs_inv.[Min_Qty],vs_inv.[Max_Qty],vs_inv.[Delivered_Items],vs_inv.[Inventory_Type],vs_inv.[Used_Items],vs_inv.[Prev_Item_Ref_Code] ,vs_inv.[Active_Status],lib_item.Short_Description, lib_item.Long_Description,lib_item.Drawing_Number,lib_item.Item_Address ,lib_item.Unit_and_Packings   FROM [eLog].[dbo].[PURC_DTL_VESSELS_INVENTORY] vs_inv inner join PURC_LIB_ITEMS lib_item on lib_item.Item_Intern_Ref=vs_inv.Item_Ref_Code and vs_inv.[Active_Status]=lib_item.[Active_Status] where lib_item.[Active_Status]=1";
                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_SP_GetRequisition_List", obj);
                dt = ds.Tables[0];
                return dt;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public DataTable getRequisition(string Document_Code)
        {

            System.Data.DataTable dt = new System.Data.DataTable();
            try
            {
                System.Data.SqlClient.SqlParameter obj = new System.Data.SqlClient.SqlParameter("@StringQuery", SqlDbType.VarChar);
                //obj.Value = "select REQUISITION_CODE,DOCUMENT_CODE,convert(varchar(20),DOCUMENT_DATE,103) as DOCUMENT_DATE from [PURC_DTL_REQSN] where id=(select max(id) from [PURC_DTL_REQSN])  and Active_status ='1'";
                obj.Value = "select REQUISITION_CODE,DOCUMENT_CODE,convert(varchar(20),DOCUMENT_DATE,103) as DOCUMENT_DATE from [PURC_DTL_REQSN] where DOCUMENT_CODE='"+Document_Code+"' and Active_status ='1'";
                // obj.Value = "select lv.vessel_code,sl.System_Description from Lib_Vessels lv inner join PURC_LIB_SYSTEMS sl on (lv.Vessel_Code=sl.Vessel_Code) inner join PURC_LIB_DEPARTMENTS dpt on (sl.Vessel_Code=dpt.Vessel_Code) where dpt.Active_Status =1 and sl.Active_Status =1";
                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "[ExeQuery]", obj);
                dt = ds.Tables[0];
                return dt;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public DataTable SelectRequisitionCodeForCombo()
        {
            //select requisition_code + '/' + right(year(document_Date),2) +' ' + [user] +' ' + convert( varchar(10), document_Date,10) +' Urgency: ' +  case URGENCY_CODE   when 'N' Then 'Normal'  when 'U' Then 'Urgent'  end requisition_code  from [PURC_DTL_REQSN] where SENT_TO is null and LINE_TYPE='R' and [user]='" + userName + "'";
            System.Data.DataTable dt = new System.Data.DataTable();
            try
            {
                System.Data.SqlClient.SqlParameter obj = new System.Data.SqlClient.SqlParameter("@StringQuery", SqlDbType.VarChar);
                //obj.Value = "select requisition_code + '/' + right(year(document_Date),2) +' ' + [user] +' ' + convert( varchar(10), document_Date,10) +' Urgency: ' +  case URGENCY_CODE   when 'N' Then 'Normal'  when 'U' Then 'Urgent'  end requisition_code_format ,[PURC_DTL_REQSN].[user],[PURC_DTL_REQSN].[ID] ,[PURC_DTL_REQSN].[requisition_code] from [PURC_DTL_REQSN] where SENT_TO is null and LINE_TYPE='R'";

                obj.Value = "select distinct PURC_DTL_REQSN.requisition_code,Slb.[Vessel_Code],dept.Code,Slb.[System_Code],PURC_DTL_REQSN.requisition_code + '/' + right(year(PURC_DTL_REQSN.document_Date),2) +' ' + PURC_DTL_REQSN.[user] +' ' + convert( varchar(10), PURC_DTL_REQSN.document_Date,10) +' Urgency: ' +  case PURC_DTL_REQSN.URGENCY_CODE   when 'N' Then 'Normal'  when 'U' Then 'Urgent'  end requisition_code_Format ,lv.Vessel_Name,Slb.System_Description,dept.Name_Dept,PURC_DTL_REQSN.Date_Of_Creatation,Slb.Maker,Slb.System_Particulars from [PURC_DTL_REQSN] inner join Lib_Vessels lv on lv.VESSEL_CODE =[PURC_DTL_REQSN].VESSEL_CODE inner join PURC_LIB_SYSTEMS Slb on Slb.[Vessel_Code]=lv.VESSEL_CODE inner join PURC_LIB_DEPARTMENTS dept on dept.Code =PURC_DTL_REQSN.Department where PURC_DTL_REQSN.SENT_TO is null and PURC_DTL_REQSN.LINE_TYPE='R'  and [PURC_DTL_REQSN].VESSEL_CODE = lv.VESSEL_CODE and lv.[Active_Status]=Slb.[Active_Status]";
                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "[ExeQuery]", obj);
                dt = ds.Tables[0];
                return dt;
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

        public string SaveUpdate_FinalQuotation(IventoryItemData objDOInventoryItem, int CurrentUser)
        {

            try
            {
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
                { 
                   new System.Data.SqlClient.SqlParameter("@DOCUMENT_CODE", objDOInventoryItem.DocumentCode),
                   new System.Data.SqlClient.SqlParameter("@DELIVERY_DATE", objDOInventoryItem.RequisitionCode),
                   new System.Data.SqlClient.SqlParameter("@DELIVERY_PORT",objDOInventoryItem.Delivery_Port),
                   new System.Data.SqlClient.SqlParameter("@DISCOUNT", objDOInventoryItem.Discount),
                   new System.Data.SqlClient.SqlParameter("@Withholding_Tax_Rate", objDOInventoryItem.WithHoldTax),
                   new System.Data.SqlClient.SqlParameter("@VAT", objDOInventoryItem.VAT),
                   new System.Data.SqlClient.SqlParameter("@Advance", objDOInventoryItem.Advance),
                   new System.Data.SqlClient.SqlParameter("@User", CurrentUser),
                   new System.Data.SqlClient.SqlParameter("@Currency",objDOInventoryItem.Currency ),
                   new System.Data.SqlClient.SqlParameter("@ORDER_SUPPLIER", objDOInventoryItem.SupplierID)
                
                };


                DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_INS_UPD_FinalQuotation", obj);
                string ReqturnId = Convert.ToString(ds.Tables[1].Rows[0]["REQUISITION_CODE"]);
                return ReqturnId;

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }


        public int SaveUpdate_Quotation(IventoryItemData objDOInventoryItem, int CurrentUser, decimal HD_TP,DataTable dt)
        {

            try
            {
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
                { 
                   new System.Data.SqlClient.SqlParameter("@DOCUMENT_CODE", objDOInventoryItem.DocumentCode),
                   new System.Data.SqlClient.SqlParameter("@VAT", objDOInventoryItem.VAT),
                   new System.Data.SqlClient.SqlParameter("@User", CurrentUser),
                    new System.Data.SqlClient.SqlParameter("@DeliveryPORT", objDOInventoryItem.Delivery_Port),
                    new System.Data.SqlClient.SqlParameter("@ORDER_SUPPLIER", objDOInventoryItem.SupplierID),
                    new System.Data.SqlClient.SqlParameter("@Withhold", objDOInventoryItem.WithHoldTax),
                   new System.Data.SqlClient.SqlParameter("@Discount", objDOInventoryItem.Discount),
                   new System.Data.SqlClient.SqlParameter("@SupplyDate", objDOInventoryItem.Delivery_Date),
                   new System.Data.SqlClient.SqlParameter("@Currency",objDOInventoryItem.Currency ),
                   new System.Data.SqlClient.SqlParameter("@Advance",objDOInventoryItem.Advance ),
                   new System.Data.SqlClient.SqlParameter("@tp",HD_TP),
                   new System.Data.SqlClient.SqlParameter("@ItemTable",dt),
                
                };


                return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_INS_UPD_Quotation", obj);

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public int SaveInventroySupplyItem(IventoryItemData objDOInventoryItem)
        {

            try
            {
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
                { 
                   new System.Data.SqlClient.SqlParameter("@VESSEL_CODE", objDOInventoryItem.VesselCode),
                   new System.Data.SqlClient.SqlParameter("@REQUISITION_CODE", objDOInventoryItem.RequisitionCode),
                   new System.Data.SqlClient.SqlParameter("@ITEM_REF_CODE",objDOInventoryItem.ItemRefCode),
                   new System.Data.SqlClient.SqlParameter("@ITEM_INTERN_REF", objDOInventoryItem.ItemInternRef),
                   new System.Data.SqlClient.SqlParameter("@ITEM_SYSTEM_CODE", objDOInventoryItem.SystemCode),
                   new System.Data.SqlClient.SqlParameter("@ITEM_SUBSYSTEM_CODE", objDOInventoryItem.SubSystemCode),
                   new System.Data.SqlClient.SqlParameter("@ROB_QTY", objDOInventoryItem.ROB),
                   new System.Data.SqlClient.SqlParameter("@REQUESTED_QTY", objDOInventoryItem.reqestedQty), 
                   new System.Data.SqlClient.SqlParameter("@ITEM_FULL_DESC", objDOInventoryItem.itemFullDesc),
                   new System.Data.SqlClient.SqlParameter("@ITEM_SHORT_DESC", objDOInventoryItem.itemShortDesc),
                   new System.Data.SqlClient.SqlParameter("@SAVED_LINE", objDOInventoryItem.SavedLine),
                   //new System.Data.SqlClient.SqlParameter("@Total_Qty", objDOInventoryItem.Totalitem),
                   new System.Data.SqlClient.SqlParameter("@DRAWING_NUMBER",objDOInventoryItem.Drawing_Number),
                   new System.Data.SqlClient.SqlParameter("@Drawing_Link", objDOInventoryItem.DrawingLink),
                   new System.Data.SqlClient.SqlParameter("@ITEM_COMMENT", objDOInventoryItem.ItemComment),
                   new System.Data.SqlClient.SqlParameter("@Created_By",objDOInventoryItem.CreatedBy),
                   new System.Data.SqlClient.SqlParameter("@Document_Code",objDOInventoryItem.DocumentCode) ,
                   new System.Data.SqlClient.SqlParameter("@ReturnID",DbType.Int32)
                
                };

                obj[obj.Length - 1].Direction = ParameterDirection.ReturnValue;
                int result = SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_SP_Ins_INV_SUPPLY_ITEM", obj);
                Int32 ReqturnId = Convert.ToInt32(obj[obj.Length - 1].Value);
                return ReqturnId;

                // return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "[PURC_SP_Ins_INV_SUPPLY_ITEM]", obj);

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
       
        public string GenerateRequisitionNumber(IventoryItemData objDOInventoryItem)
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
                   //new System.Data.SqlClient.SqlParameter("@ReturnID",SqlDbType.VarChar)
                   
                };

                //obj[11].Direction = ParameterDirection.ReturnValue;
                DataSet ds=SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_SP_Generate_Requisition_Number", obj);
                string ReqturnId = Convert.ToString(ds.Tables[0].Rows[0]["DOCUMENT_CODE"]);
                return ReqturnId;
            }
            catch (Exception ex)
            {

                throw ex;


            }

        }

        public int AddInventoryItem(IventoryItemData objDOInventoryItem)
        {
            try
            {
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[]
            { 
                   new System.Data.SqlClient.SqlParameter("@Vessel_Code", SqlDbType.VarChar),
                   new System.Data.SqlClient.SqlParameter("@Department_Code", SqlDbType.VarChar),
                   new System.Data.SqlClient.SqlParameter("@System_Code", SqlDbType.VarChar),
                   new System.Data.SqlClient.SqlParameter("@Subsystem_Code", SqlDbType.VarChar),
                   new System.Data.SqlClient.SqlParameter("@Item_Ref_Code", SqlDbType.VarChar),
                   new System.Data.SqlClient.SqlParameter("@Inventory_Date", SqlDbType.VarChar),
                   new System.Data.SqlClient.SqlParameter("@Inventory_Qty", SqlDbType.VarChar),
                   new System.Data.SqlClient.SqlParameter("@TO_Repair_Qty", SqlDbType.VarChar),
                   new System.Data.SqlClient.SqlParameter("@Working_Qty", SqlDbType.VarChar),
                   new System.Data.SqlClient.SqlParameter("@Min_Qty", SqlDbType.VarChar),
                   new System.Data.SqlClient.SqlParameter("@Max_Qty", SqlDbType.VarChar),
                   new System.Data.SqlClient.SqlParameter("@Delivered_Items", SqlDbType.VarChar),
                   new System.Data.SqlClient.SqlParameter("@Inventory_Type", SqlDbType.VarChar),
                   new System.Data.SqlClient.SqlParameter("@Used_Items", SqlDbType.VarChar),
                   new System.Data.SqlClient.SqlParameter("@Price_Book", SqlDbType.VarChar),
                   new System.Data.SqlClient.SqlParameter("@Storage_Place_1", SqlDbType.VarChar),
                   new System.Data.SqlClient.SqlParameter("@Storage_Place_2", SqlDbType.VarChar),
                   new System.Data.SqlClient.SqlParameter("@Prev_Item_Ref_Code", SqlDbType.VarChar),
                   new System.Data.SqlClient.SqlParameter("@Sync_Flag", SqlDbType.VarChar),
                   new System.Data.SqlClient.SqlParameter("@Link", SqlDbType.VarChar),
                   new System.Data.SqlClient.SqlParameter("@Created_By", SqlDbType.VarChar)
            };
                return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "[PURC_SP_Ins_INV_Dtl_Vessels_Inventrory]", obj);

            }
            catch (Exception ex)
            {

                throw ex;


            }

        }

        public int DeleteRequisitionItem(string RequisitionCode, string Documentcode)
        {
            try
            {
                SqlParameter[] obj = new SqlParameter[] 
            { 
              new System.Data.SqlClient.SqlParameter("@REQUISITION_CODE", RequisitionCode),
               new System.Data.SqlClient.SqlParameter("@Document_code", Documentcode),
              new System.Data.SqlClient.SqlParameter("@ReturnID",DbType.Int32)
            };
                obj[2].Direction = ParameterDirection.ReturnValue;
                int result = SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_SP_Delete_to_Requisition_Item", obj);
                Int32 ReqturnId = Convert.ToInt32(obj[2].Value);
                return ReqturnId;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public int DeleteSupplyItem(string itemRefcode, string Documentcode)
        {
            try
            {
                SqlParameter[] obj = new SqlParameter[] 
            { 
              new System.Data.SqlClient.SqlParameter("@itemRefcode", itemRefcode),
              new System.Data.SqlClient.SqlParameter("@Document_code", Documentcode),
            };

                return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "[PURC_UPD_Reqsn_Item]", obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public int UpdateNoQuoteRqsn(string ITEM_REF_CODE, string DOCUMENT_CODE, int ORDER_VAT, int Withholding_Tax_Rate, string ORDER_SUPPLIER, string DELIVERY_PORT, DateTime DELIVERY_DATE, int TotalPrice, string ORDER_DISCOUNT, string REQUISITION_CODE)
        {
            try
            {
                SqlParameter[] obj = new SqlParameter[] 
            { 
              new System.Data.SqlClient.SqlParameter("@DELIVERY_PORT", DELIVERY_PORT),
              new System.Data.SqlClient.SqlParameter("@DELIVERY_DATE", DELIVERY_DATE),
              new System.Data.SqlClient.SqlParameter("@Withholding_Tax_Rate",Withholding_Tax_Rate),
              new System.Data.SqlClient.SqlParameter("@ORDER_VAT",ORDER_VAT),
              new System.Data.SqlClient.SqlParameter("@ORDER_SUPPLIER",ORDER_SUPPLIER),
              new System.Data.SqlClient.SqlParameter("@ORDER_DISCOUNT",ORDER_DISCOUNT),
              new System.Data.SqlClient.SqlParameter("@ORDER_PRICE",TotalPrice),
              new System.Data.SqlClient.SqlParameter("@REQUISITION_CODE",REQUISITION_CODE),
              new System.Data.SqlClient.SqlParameter("@ITEM_REF_CODE",ITEM_REF_CODE),
              new System.Data.SqlClient.SqlParameter("@DOCUMENT_CODE",DOCUMENT_CODE)

            };

                return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "[PURC_UPD_Reqsn_Qty]", obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public int UpdateSupplyQnty(string itemRefcode,string DocCode,decimal Qnty)
        {
            try
            {
                SqlParameter[] obj = new SqlParameter[] 
            { 
              new System.Data.SqlClient.SqlParameter("@itemRefcode", itemRefcode),
              new System.Data.SqlClient.SqlParameter("@docCode", DocCode),

              new System.Data.SqlClient.SqlParameter("@Qnty",Qnty)
            };

                return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "[PURC_UPD_Reqsn_Qty]", obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public int PURC_UPD_Reqsn_supplyitems(string itemRefcode, string DocCode, decimal Qnty, decimal Discount, decimal PricePerUnit)
        {
            try
            {
                SqlParameter[] obj = new SqlParameter[] 
            { 
              new System.Data.SqlClient.SqlParameter("@itemRefcode", itemRefcode),
              new System.Data.SqlClient.SqlParameter("@docCode", DocCode),

              new System.Data.SqlClient.SqlParameter("@Qnty",Qnty),
              new System.Data.SqlClient.SqlParameter("@ORDER_DISCOUNT",Discount),
              new System.Data.SqlClient.SqlParameter("@ORDER_RATE",PricePerUnit),
            };

                return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "[PURC_UPD_Reqsn_supplyitems]", obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public int PURC_UPD_Reqsn_supplyitems(string itemRefcode, string DocCode, decimal Qnty, decimal Discount, decimal PricePerUnit, decimal vat, decimal withhold)
        {
            try
            {
                SqlParameter[] obj = new SqlParameter[] 
            { 
              new System.Data.SqlClient.SqlParameter("@itemRefcode", itemRefcode),
              new System.Data.SqlClient.SqlParameter("@docCode", DocCode),

              new System.Data.SqlClient.SqlParameter("@Qnty",Qnty),
              new System.Data.SqlClient.SqlParameter("@ORDER_DISCOUNT",Discount),
              new System.Data.SqlClient.SqlParameter("@ORDER_RATE",PricePerUnit),
              new System.Data.SqlClient.SqlParameter("@Withhold",withhold),
              new System.Data.SqlClient.SqlParameter("@VAT",vat),
            };

                return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "[PURC_UPD_Reqsn_supplyitems]", obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public string FinalizeRequisitionItem(string RequisitionCode, string Documentcode, string VesselCode, string PortName, string DeliveryDate, string RequistionComment)
        {
            try
            {
                SqlParameter[] obj = new SqlParameter[]
            { 
               new  SqlParameter("@REQUISITION_CODE", RequisitionCode),
               new  SqlParameter("@Document_code ", Documentcode),
               new  SqlParameter("@Vessel_Code ", VesselCode),
               new  SqlParameter("@Port_Name ", PortName),
               new  SqlParameter("@Delivery_Date ", DeliveryDate),
               new SqlParameter ("@REQUI_COMMENT",RequistionComment),
               
            };
                
                DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_SP_Finalize_Item_Requisition", obj);
                string ReqturnId = Convert.ToString(ds.Tables[1].Rows[0]["REQUISITION_CODE"]);
                return ReqturnId;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public int AddCommentToRequisition(string RequisitionCode, string RequistionComment, string Documentcode)
        {
            try
            {
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[]
            { 
             new System.Data.SqlClient.SqlParameter("@REQUISITION_CODE",RequisitionCode),
             new System.Data.SqlClient.SqlParameter("@REQUI_COMMENT", RequistionComment),
             new System.Data.SqlClient.SqlParameter("@Document_code", Documentcode),
             new System.Data.SqlClient.SqlParameter("@ReturnID",DbType.Int32)
            };
                obj[3].Direction = ParameterDirection.ReturnValue;
                int result = SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_SP_AddComment_to_Requisition", obj);
                Int32 ReqturnId = Convert.ToInt32(obj[3].Value);
                return ReqturnId;
            }
            catch (Exception ex)
            {
                throw ex;

            }

        }
        public DataSet ConfiguredSupplierPreview(string Document_code, string ReqsCode, string Searchtext)
        {
            try
            {
                DataSet dtDept = new System.Data.DataSet();
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[]
             {
             new System.Data.SqlClient.SqlParameter("@Document_code",Document_code),
             new System.Data.SqlClient.SqlParameter("@ReqsCode",ReqsCode),
             new System.Data.SqlClient.SqlParameter("@Searchtext",ReqsCode)
           
             };
                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_Get_ConfiguredSupplierPreview", obj);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public DataSet getCurrentRates()
        {
            try
            {
                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PRUC_Get_CurrentRates");
               
                return ds;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public DataTable getDeliveryPort()
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            try
            {
                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_SP_Get_DeliveryPort");
                dt = ds.Tables[0];
                return dt;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public DataSet GetPOListForLogisticCompany()
        {
            System.Data.DataSet ds = new System.Data.DataSet();
            try
            {
                ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_SP_Get_POListForLogisticCompany");
                return ds;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public int AddConsolidatedPO(string strRequisition_Code, string strQuotation_Code, string strOrder_Code, string strDelivery_Port,
            DateTime dtDelivery_Date, string strDocument_Code, string strSupplier_Code, string strAssign_Agent_Code, Int32 i32Vessel_Code,
            Int32 i32Created_By)
        {
            try
            {
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[]
            { 
             new System.Data.SqlClient.SqlParameter("@Requisition_Code",strRequisition_Code),
             new System.Data.SqlClient.SqlParameter("@Quotation_Code", strQuotation_Code),
             new System.Data.SqlClient.SqlParameter("@Order_Code", strOrder_Code),
             new System.Data.SqlClient.SqlParameter("@Delivery_Port", strDelivery_Port),
             new System.Data.SqlClient.SqlParameter("@Delivery_Date", dtDelivery_Date),
             new System.Data.SqlClient.SqlParameter("@Document_Code", strDocument_Code),
             new System.Data.SqlClient.SqlParameter("@Supplier_Code", strSupplier_Code),
             new System.Data.SqlClient.SqlParameter("@Assign_Agent_Code", strAssign_Agent_Code),
             new System.Data.SqlClient.SqlParameter("@Vessel_Code", i32Vessel_Code),
             new System.Data.SqlClient.SqlParameter("@Created_By", i32Created_By)
            };
                int result = SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_SP_Ins_ConsolidatedPO", obj);
                Int32 ReqturnId = Convert.ToInt32(obj[3].Value);
                return ReqturnId;
            }
            catch (Exception ex)
            {
                throw ex;

            }

        }
        public DataSet Get_SelectedPort()
        {
            try
            {
                DataSet dtDept = new System.Data.DataSet();
                
                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_Get_SelectedPort");
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }
        public  DataSet PURC_Get_Sys_Variable(int UserID, string FilterType)
        {
             try
            {
                DataSet dtDept = new System.Data.DataSet();
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[]
             {
                new System.Data.SqlClient.SqlParameter("@UserID", UserID),
                new System.Data.SqlClient.SqlParameter("@FilterType", FilterType)
                };
                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_Get_Sys_Variable", obj);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }
        public DataSet GetPOItemListForReport(Int32 i32Vessel_Code, string strAgent_Code)
        {
            System.Data.DataSet ds = new System.Data.DataSet();
            try
            {
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] { 
                   new System.Data.SqlClient.SqlParameter("@Vessel_Code", i32Vessel_Code),
                   new System.Data.SqlClient.SqlParameter("@Agent_Code", strAgent_Code)
            };
                ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_SP_Get_POItemListForReport", obj);
                return ds;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public int Insert_Purc_Question(string REQSN_CODE, string DocumentCode, int UserID, DataTable dtQuest, int? OFFICE_ID, int? VESSEL_ID)
        {
            SqlParameter[] obj = new SqlParameter[] 
            { 
                 new  SqlParameter("@DocumentCode",DocumentCode),
                 new SqlParameter("@REQSN_CODE", REQSN_CODE),  
                new SqlParameter("@OFFICE_ID",OFFICE_ID) ,               
                new SqlParameter("@VESSEL_ID",VESSEL_ID) ,  
                 new SqlParameter("@UserID",UserID),
                 new SqlParameter("@dtQuestion",dtQuest),
                 new SqlParameter("@ReturnID",DbType.Int32),
            };
            obj[6].Direction = ParameterDirection.ReturnValue;
            int result = SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_INS_UPD_QUESTION_ANSWERS", obj);
            Int32 returnID = Convert.ToInt32(obj[6].Value);
            return returnID;
        }
        public DataSet Get_Purc_Questions(string DocumentCode, string DeptCode)
        {
            DataSet ds = new System.Data.DataSet();
            try
            {
                SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
                { 
                   new System.Data.SqlClient.SqlParameter("@DOCUMENT_CODE", DocumentCode),
                   new System.Data.SqlClient.SqlParameter("@DEPT_CODE", DeptCode)
                };
                ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_GET_QUESTION", obj);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet Get_Purc_Questions_Options(int QuestionID)
        {
            DataSet ds = new System.Data.DataSet();
            try
            {
                SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
                { 
                   new System.Data.SqlClient.SqlParameter("@Question_ID", QuestionID),
                };
                ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_GET_QUESTION_OPTION", obj);
                
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet Get_WorkList_Search(string SearchText, int? VESSEL_ID, string sortby, string sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {

            SqlParameter[] obj = new SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@SearchText", SearchText),
                   new System.Data.SqlClient.SqlParameter("@VESSEL_ID",VESSEL_ID), 
                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_GET_WORKLIST_SEARCH", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds;
        }

        public int Insert_Purc_WorkList(int? ID, int? WorklistID, int? OfficeID, int? VesselID, string DocCode, string Mode, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            { 
                new SqlParameter("@ID",ID) ,        
                new SqlParameter("@OFFICE_ID",OfficeID),               
                new SqlParameter("@VESSEL_ID",VesselID),               
                new SqlParameter("@WORKLIST_ID",WorklistID) ,   
                new SqlParameter("@Document_Code",DocCode) ,   
                new SqlParameter("@Mode",Mode) , 
                new SqlParameter("@CREATED_BY",UserID) , 
            };
            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_Worklist_Insert", sqlprm);
        }
        public DataTable Get_Purc_Worklist(int? OFFICE_ID, int? VESSEL_ID, string DocumentCode)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            { 
                new SqlParameter("@OFFICE_ID",OFFICE_ID) ,               
                new SqlParameter("@VESSEL_ID",VESSEL_ID) ,               
                new SqlParameter("@Document_Code",DocumentCode)                
            };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "Get_PURC_WorkList", sqlprm).Tables[0];
        }

        public DataTable Get_Purc_New_Reqsn_Docment_Code(string VesselID, string Dept)
        {
            DataTable dtReq = new DataTable();
            SqlParameter[] sqlPrm = new SqlParameter[]
            {
                 new SqlParameter("@VESSEL_CODE",VesselID) ,     
                 new SqlParameter("@DEPARTMENT",Dept)
            };
            dtReq =(SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_GET_REQSN_CODE_FOR_OFFICE", sqlPrm)).Tables[0];
            return dtReq;
        }
        public DataTable Purc_Create_Finalize_New_Requisition(string PortName,string DeliveryDate,string RequistionComment,DataTable dtSupItem)
        {
            DataTable RequisitionCode =new  DataTable();
            SqlParameter[] sqlprm = new SqlParameter[]
            { 
                   new SqlParameter("@Port_Name ", PortName),
                   new SqlParameter("@Delivery_Date ", DeliveryDate),
                   new SqlParameter("@REQUI_COMMENT",RequistionComment),
                   new SqlParameter("@dtSupItem",dtSupItem),
                  
            };
            RequisitionCode=SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_CREATE_REQSN_FINALIZE", sqlprm).Tables[1];
            return RequisitionCode;
        }


        public DataSet GET_PURC_DEP_ON_DOCCODE(string DocCode)
        {
            DataSet dsRequisitionCode = new DataSet();
            SqlParameter[] sqlprm = new SqlParameter[]
            { 
                   new SqlParameter("@DOCUMENT_CODE", DocCode),
            };
            dsRequisitionCode = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "GET_DEPT_ON_DOC_CODE", sqlprm);

            return dsRequisitionCode;
        }

        public int ItemInsert_Supplier_remarks_settings(string Item_Ref_code, string ReqsnCode, string DocCode, int Isshow, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            { 
                   new SqlParameter("@DOCUMENT_CODE", DocCode),
                   new SqlParameter("@REQUISITION_CODE", ReqsnCode),
                   new SqlParameter("@ITEM_REF_CODE",Item_Ref_code),
                   new SqlParameter("@IsShow",Isshow),
                   new SqlParameter("@UserID",UserID) , 
                  
            };
            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_INS_SUPPLIER_REMARKS_SETTINGS", sqlprm);
        }
        public DataSet getSupplierProperty(string supplierCode)
        {
            try
            {
                SqlParameter[] obj = new SqlParameter[]
            { 
               new  SqlParameter("@SuppCode", supplierCode),
              
               
            };

                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_GETSupplierProperty", obj);

                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }

}