using System;
using System.Configuration;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using SMS.Data;
using SMS.Properties;

/// <summary>
/// Summary description for DALVessels IDALQuatationEvalution.cs
/// </summary>

namespace SMS.Data.PURC
{
    public class DAL_PURC_QuatationEvalution
    {

        private string _internalConnection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;


        public DAL_PURC_QuatationEvalution()
        {
        }



        public DataTable SelectQuatationSendBySupplier(string req, string Vessel_code, string DocumentCode)
        {


            System.Data.DataTable dt = new System.Data.DataTable();
            try
            {
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[]
             { 
               new System.Data.SqlClient.SqlParameter("@reqCode",req),
               new System.Data.SqlClient.SqlParameter("@Vessel_Code",Vessel_code),
               new System.Data.SqlClient.SqlParameter("@Document_Code",DocumentCode)
               
             };

                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_SP_Get_Supp_Info_Quatation", obj);

                dt = ds.Tables[0];
            }
            catch (Exception ex)
            {

                dt = null;
            }
            return dt;

        }

        public DataTable SelectQuatationSendBySupplier_Report(string req, string Vessel_code, string DocumentCode)
        {


            System.Data.DataTable dt = new System.Data.DataTable();
            try
            {
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[]
             { 
               new System.Data.SqlClient.SqlParameter("@reqCode",req),
               new System.Data.SqlClient.SqlParameter("@Vessel_Code",Vessel_code),
               new System.Data.SqlClient.SqlParameter("@Document_Code",DocumentCode)
               
             };

                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_sp_Get_Supp_Info_Quatation_Report", obj);

                dt = ds.Tables[0];
            }
            catch (Exception ex)
            {

                dt = null;
            }
            return dt;

        }

        public DataTable SelectItemsQuatationSuppliedBySupplier(string strQuery)
        {
            System.Data.DataTable dtQuata = new System.Data.DataTable();
            System.Data.SqlClient.SqlParameter obj = new System.Data.SqlClient.SqlParameter("@StringQuery", SqlDbType.VarChar);
            obj.Value = strQuery.ToString();
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "[ExeQuery]", obj);
            dtQuata = ds.Tables[0];
            return dtQuata;
        }


        public DataTable getRequisionForQuatation()
        {

            try
            {
                System.Data.DataTable dtSupp = new System.Data.DataTable();
                System.Data.SqlClient.SqlParameter obj = new System.Data.SqlClient.SqlParameter("@StringQuery", SqlDbType.VarChar);
                obj.Value = "select distinct Z.Name_Dept,Z.Code,Z.SYSTEM_Description,Z.system_code,z.REQUISITION_CODE,z.QUOTATION_CODE  from ( Select req.QUOTATION_CODE, req.QUOTATION_SUPPLIER,req.DOCUMENT_DATE requestion_Date, req.REQUISITION_CODE, dep.Name_Dept,dep.Code,req.TOTAL_ITEMS,a.SYSTEM_Description,a.system_code,Req.Line_type,B.Quotation_Status  from dbo.PURC_DTL_REQSN Req LEFT OUTER JOIN dbo.PURC_LIB_DEPARTMENTS dep on req.DEPARTMENT=dep.code  inner join (SELECT DISTINCT SITEM.REQUISITION_CODE,slib.SYSTEM_Description,slib.SYSTEM_CODE    FROM dbo.PURC_DTL_SUPPLY_ITEMS SITEM INNER jOIN dbo.PURC_LIB_SYSTEMS Slib on  SITEM.ITEM_SYSTEM_CODE=slib.SYSTEM_CODE) A on A.REQUISITION_CODE=Req.REQUISITION_CODE   inner Join   (select distinct QUOTATION_CODE,Quotation_Status   from dbo.PURC_DTL_QUOTED_PRICES where Quotation_Status='S') B on Req.QUOTATION_CODE=B.QUOTATION_CODE ) Z";
                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "[ExeQuery]", obj);
                dtSupp = ds.Tables[0];
                return dtSupp;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public DataTable GetSupplierForQuatation(string Reqcode, string VesselCode, string DocumentCode)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            try
            {
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[]
             { 
               new System.Data.SqlClient.SqlParameter("@ReqCode",Reqcode),
               new System.Data.SqlClient.SqlParameter("@VesselCode",VesselCode),
               new System.Data.SqlClient.SqlParameter("@Document_code",DocumentCode)
               
             };
                //// obj.Value = "Select Req.REQUISITION_CODE,req.DOCUMENT_DATE requestion_Date, req.REQUISITION_CODE,dep.Name_Dept,req.TOTAL_ITEMS,a.SYSTEM_Description,Req.Line_type from dbo.PURC_DTL_REQSN Req LEFT OUTER JOIN dbo.PURC_LIB_DEPARTMENTS dep on req.DEPARTMENT=dep.code inner join (SELECT DISTINCT SITEM.REQUISITION_CODE,slib.SYSTEM_Description FROM dbo.PURC_DTL_SUPPLY_ITEMS SITEM INNER jOIN dbo.PURC_LIB_SYSTEMS Slib on SITEM.ITEM_SYSTEM_CODE=slib.SYSTEM_CODE) A on A.REQUISITION_CODE=Req.REQUISITION_CODE";
                //string reqSql = "Select req.QUOTATION_CODE, req.QUOTATION_SUPPLIER,sul.SHORT_NAME, Req.REQUISITION_CODE,a.SYSTEM_Description,Req.Line_type,B.Quotation_Status from dbo.PURC_DTL_REQSN Req inner join (SELECT DISTINCT SITEM.REQUISITION_CODE,slib.SYSTEM_Description FROM dbo.PURC_DTL_SUPPLY_ITEMS SITEM INNER jOIN dbo.PURC_LIB_SYSTEMS Slib on SITEM.ITEM_SYSTEM_CODE=slib.SYSTEM_CODE) A on A.REQUISITION_CODE=Req.REQUISITION_CODE inner Join (select distinct QUOTATION_CODE,Quotation_Status from dbo.PURC_DTL_QUOTED_PRICES where Quotation_Status='S') B on Req.QUOTATION_CODE=B.QUOTATION_CODE inner join dbo.Lib_Suppliers sul on req.QUOTATION_SUPPLIER=sul.SUPPLIER";

                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_SP_GetSupplier_For_Quotation", obj);

                dt = ds.Tables[0];
            }
            catch (Exception ex)
            {

                dt = null;
            }
            return dt;
        }


        public DataTable GetItemsSendedToSupplier(string req)
        {
            try
            {
                System.Data.DataTable dtSupp = new System.Data.DataTable();
                System.Data.SqlClient.SqlParameter obj = new System.Data.SqlClient.SqlParameter("@StringQuery", SqlDbType.VarChar);

                obj.Value = "select distinct a.ITEM_SERIAL_NO,'False' chechstatus, a.QUOTATION_CODE,a.ITEM_REF_CODE, a.ITEM_SHORT_DESC,a.ITEM_FULL_DESC,a.QUOTED_UNIT_ID,a.QUOTED_QTY    from PURC_DTL_QUOTED_PRICES a     where  a.QUOTATION_CODE=(select top 1 QUOTATION_CODE    from dbo.PURC_DTL_REQSN where REQUISITION_CODE='" + req + "'  and Line_type='Q') ";
                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "[ExeQuery]", obj);
                dtSupp = ds.Tables[0];
                return dtSupp;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public int UpdateQuotForRework(string ReqCode, string VesselCode, string DocumentCode, string SuppCode, string QuotCode)
        {
            try
            {
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[]
             { 
               new System.Data.SqlClient.SqlParameter("@ReqCode",ReqCode),
               new System.Data.SqlClient.SqlParameter("@VesselCode",VesselCode),
               new System.Data.SqlClient.SqlParameter("@DocumentCode",DocumentCode),
               new System.Data.SqlClient.SqlParameter("@QuotationCode",QuotCode),
               new System.Data.SqlClient.SqlParameter("@SupplierCode",SuppCode)
             };
                return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_SP_SEND_REWORK_FOR_QUOTATION", obj);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public DataTable CountRFQSendForReQuotation(string ReqCode, string VesselCode, string DocumentCode)
        {
            try
            {
                System.Data.DataTable dtRFQReQtn = new System.Data.DataTable();

                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[]
             { 
               new System.Data.SqlClient.SqlParameter("@ReqCode",ReqCode),
               new System.Data.SqlClient.SqlParameter("@VesselCode",VesselCode),
               new System.Data.SqlClient.SqlParameter("@DocumentCode",DocumentCode),
             };
                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_SP_Get_RFQ_SendForReQuotaiont", obj);
                dtRFQReQtn = ds.Tables[0];
                return dtRFQReQtn;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DataTable SelectPendingPOApproval(string VesselCode)
        {
            try
            {
                System.Data.DataTable dtFleet = new System.Data.DataTable();
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[]
             { 
               new System.Data.SqlClient.SqlParameter("@VesselCode",VesselCode)
             };

                //System.Data.SqlClient.SqlParameter obj = new System.Data.SqlClient.SqlParameter("@StringQuery", SqlDbType.VarChar);
                //obj.Value = "SELECT DISTINCT itv.[ID],itv.[Part_Number],itv.[Short_Description],itv.[Unit_and_Packings], SITEM.[REQUESTED_QTY],SITEM.[ITEM_COMMENT], SITEM.REQUISITION_CODE,slib.SYSTEM_Description FROM dbo.PURC_DTL_SUPPLY_ITEMS SITEM  INNER jOIN dbo.PURC_LIB_SYSTEMS Slib on SITEM.ITEM_SYSTEM_CODE=slib.SYSTEM_CODE INNER jOIN  dbo.PURC_LIB_ITEMS itv on itv.[ID]=SITEM.[ITEM_REF_CODE] where SITEM.REQUISITION_CODE = '"+strRequistionCode + "'";

                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_SP_Get_PendingPOApproval", obj);
                dtFleet = ds.Tables[0];
                return dtFleet;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable SelectRequisitionDeliveryStatus(int? CurrentCity, int? CurrentPort, int? DeliveryPort,
            string CurrentStage, int? Fleet_ID, int? Vessel_Code, string Dept_Code, string Dept_Type, string REQ_ORD_Code, int? Reqsn_Type, string Supplier_name, string PO_Date, int? Page_Index, int? Page_Size, ref int Is_Fetch_Count, string Sort_By, string Sort_Direction)
        {
            try
            {
                SqlDateTime dtPODate = PO_Date != null ? DateTime.Parse(PO_Date) : SqlDateTime.Null;
                DataTable dtFleet = new DataTable();
                SqlParameter[] obj = new SqlParameter[]
             { 
                new SqlParameter("@CURRENT_CITY",CurrentCity),
                new SqlParameter("@CURRENT_PORT",CurrentPort),
                new SqlParameter("@DELIVERY_PORT",DeliveryPort),
                new SqlParameter("@CURRENT_STAGE",CurrentStage),
                new SqlParameter("@FLEET_ID",Fleet_ID), 
                new SqlParameter("@VESSEL_CODE",Vessel_Code), 
                new SqlParameter("@DEPT_CODE",Dept_Code), 
                new SqlParameter("@DEPT_TYPE",Dept_Type), 
                new SqlParameter("@REQ_ORD_CODE",REQ_ORD_Code !=null?REQ_ORD_Code.Trim():REQ_ORD_Code), 
                new SqlParameter("@REQSN_TYPE",Reqsn_Type), 
                new SqlParameter("@SUPPLIER_NAME",Supplier_name), 
                new SqlParameter("@PO_DATE",dtPODate), 
                      
                        
                new SqlParameter("@PAGE_INDEX",Page_Index), 
                new SqlParameter("@PAGE_SIZE",Page_Size), 
                new SqlParameter("@SORT_BY",Sort_By),
                new SqlParameter("@SORT_DIRECTION",Sort_Direction),
                new SqlParameter("@IS_FETCH_COUNT",Is_Fetch_Count),
             };
                obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;

                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_SP_Get_PendingDeliveryUpdate", obj);
                dtFleet = ds.Tables[0];
                Is_Fetch_Count = Convert.ToInt32(obj[obj.Length - 1].Value);
                return dtFleet;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable SelectRequisitionDeliveryStatus(int? CurrentCity, int? CurrentPort, int? DeliveryPort, DataTable CurrentStage
            , DataTable Fleet_ID, DataTable Vessel_Code, DataTable Dept_Code, string Dept_Type, string REQ_ORD_Code, int? Reqsn_Type, DataTable Supplier_name
            , DateTime? PO_Date, DateTime? To_PO_Date, int? Page_Index, int? Page_Size, ref int Is_Fetch_Count, string Sort_By, string Sort_Direction)
        {
            try
            {

                DataTable dtFleet = new DataTable();
                SqlParameter[] obj = new SqlParameter[]
             { 
                new SqlParameter("@CURRENT_CITY",CurrentCity),
                new SqlParameter("@CURRENT_PORT",CurrentPort),
                new SqlParameter("@DELIVERY_PORT",DeliveryPort),
                new SqlParameter("@CURRENT_STAGE",CurrentStage),
                new SqlParameter("@FLEET_ID",Fleet_ID), 
                new SqlParameter("@VESSEL_CODE",Vessel_Code), 
                new SqlParameter("@DEPT_CODE",Dept_Code), 
                new SqlParameter("@DEPT_TYPE",Dept_Type), 
                new SqlParameter("@REQ_ORD_CODE",REQ_ORD_Code !=null?REQ_ORD_Code.Trim():REQ_ORD_Code), 
                new SqlParameter("@REQSN_TYPE",Reqsn_Type), 
                new SqlParameter("@SUPPLIER_NAME",Supplier_name), 
                new SqlParameter("@PO_DATE",PO_Date), 
                new SqlParameter("@TOPO_DATE",To_PO_Date), 
                      
                        
                new SqlParameter("@PAGE_INDEX",Page_Index), 
                new SqlParameter("@PAGE_SIZE",Page_Size), 
                new SqlParameter("@SORT_BY",Sort_By),
                new SqlParameter("@SORT_DIRECTION",Sort_Direction),
                new SqlParameter("@IS_FETCH_COUNT",Is_Fetch_Count),
             };
                obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;

                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_SP_Get_PendingDeliveryUpdate", obj);
                dtFleet = ds.Tables[0];
                Is_Fetch_Count = Convert.ToInt32(obj[obj.Length - 1].Value);
                return dtFleet;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable SelectRequisitionDeliveryStatus_Export(int? CurrentCity, int? CurrentPort, int? DeliveryPort, DataTable CurrentStage, DataTable Fleet_ID, DataTable Vessel_Code
            , DataTable Dept_Code, string Dept_Type, string REQ_ORD_Code, int? Reqsn_Type, DataTable Supplier_name
            , DateTime? PO_Date, DateTime? To_PO_Date, int? Page_Index, int? Page_Size, ref int Is_Fetch_Count)
        {
            try
            {

                DataTable dtFleet = new DataTable();
                SqlParameter[] obj = new SqlParameter[]
             { 
                new SqlParameter("@CURRENT_CITY",CurrentCity),
                new SqlParameter("@CURRENT_PORT",CurrentPort),
                new SqlParameter("@DELIVERY_PORT",DeliveryPort),
                new SqlParameter("@CURRENT_STAGE",CurrentStage),
                new SqlParameter("@FLEET_ID",Fleet_ID), 
                new SqlParameter("@VESSEL_CODE",Vessel_Code), 
                new SqlParameter("@DEPT_CODE",Dept_Code), 
                new SqlParameter("@DEPT_TYPE",Dept_Type), 
                new SqlParameter("@REQ_ORD_CODE",REQ_ORD_Code !=null?REQ_ORD_Code.Trim():REQ_ORD_Code), 
                new SqlParameter("@REQSN_TYPE",Reqsn_Type), 
                new SqlParameter("@SUPPLIER_NAME",Supplier_name), 
                new SqlParameter("@PO_DATE",PO_Date), 
                new SqlParameter("@TOPO_DATE",To_PO_Date),       
                        
                new SqlParameter("@PAGE_INDEX",Page_Index), 
                new SqlParameter("@PAGE_SIZE",Page_Size), 
                new SqlParameter("@IS_FETCH_COUNT",Is_Fetch_Count),
             };
                obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;

                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_SP_Get_PendingDeliveryUpdate_Export", obj);
                dtFleet = ds.Tables[0];
                Is_Fetch_Count = Convert.ToInt32(obj[obj.Length - 1].Value);
                return dtFleet;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable Get_Approval_Limit_DL(int UserID, string DeptShortCode)
        {
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_GET_APPROVAL_LIMIT", new SqlParameter("@LogedInUserID", UserID), new SqlParameter("@DeptShortCode", DeptShortCode)).Tables[0];
        }

        public DataTable Get_Approver_History_DL(string Reqsn)
        {
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_GET_APPROVER_HISTORY", new SqlParameter("@Reqsn", Reqsn)).Tables[0];
        }

        public DataTable Get_Delivery_History_DL(string ItemSystemcode, string Vesselcode)
        {
            DataTable dt = new DataTable();
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[]
             { 
               new SqlParameter("@ItemSubSystemcode",ItemSystemcode),
               new SqlParameter("@VesselCode",Vesselcode) 
             };

            DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_Get_DeliveryHistory_Items", obj);
            return dt = ds.Tables[0];
        }

        public DataSet GetSupplierQuote(string req, string Vessel_code, string DocumentCode)
        {


            System.Data.DataSet dt = new System.Data.DataSet();
            try
            {
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[]
                { 
                    new System.Data.SqlClient.SqlParameter("@reqCode",req),
                    new System.Data.SqlClient.SqlParameter("@Document_Code",DocumentCode),
                    new System.Data.SqlClient.SqlParameter("@Vessel_Code",Vessel_code)
                };

                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_Get_Supp_Quote", obj);

                dt = ds;
            }
            catch (Exception ex)
            {

                dt = null;
            }
            return dt;

        }
        public DataTable SelectRequisitionDeliveryStatus_New(string fleetid, string vesselcode, string depttype, string deptcode, string req_order_code, string potype, string acctype, string reqsntype, string catalogue, string FromDate, string ToDate, string accClass, string dtUrgency, string reqstatus, string Delivery_Port,string Supplier, int? pageindex, int? pagesize, ref int isfetchcount, string Sort_By, string Sort_Direction)
        {
            try
            {

                DataTable dtFleet = new DataTable();
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
                        ,new SqlParameter("@SUPPLIER",Supplier)
                        ,new SqlParameter("@DELIVERY_PORT",Delivery_Port)

                        ,new SqlParameter("@PAGE_INDEX",pageindex)
                        ,new SqlParameter("@PAGE_SIZE",pagesize)
                        ,new SqlParameter("@SORT_BY",Sort_By)
                        ,new SqlParameter("@SORT_DIRECTION",Sort_Direction)
                        ,new SqlParameter("@IS_FETCH_COUNT",isfetchcount)

                 };
                obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;

                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_sp_Get_PendingDeliveryUpdate_New", obj);
                dtFleet = ds.Tables[0];
                isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
                return dtFleet;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

}