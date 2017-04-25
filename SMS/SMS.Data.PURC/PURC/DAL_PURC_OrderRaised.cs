using System;
using System.Configuration;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using SMS.Data;
using SMS.Properties;
/// <summary>
/// Summary description for DALOrderRaised
/// </summary>
namespace SMS.Data.PURC
{
    public class DAL_PURC_OrderRaised
    {
        private string _internalConnection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;


        public DAL_PURC_OrderRaised()
        {

        }

        


        public DataTable SelectSupplierToSendOrderEval(string ReqCode, string VesselCode,string QTNCode)
        {
            try
            {
                System.Data.DataTable dtSupplier = new System.Data.DataTable();
                System.Data.SqlClient.SqlParameter obj = new System.Data.SqlClient.SqlParameter("@StringQuery", SqlDbType.VarChar);
                obj.Value = "select SUPPLIER,SHORT_NAME,Qprice.QUOTATION_CODE,Total_Item, convert(varchar(10),getdate(),103) as send_date, 'False' as status   from dbo.Lib_Suppliers  SA inner join (select QUOTATION_CODE, SUPPLIER_CODE,Quotation_Status,count(ITEM_REF_CODE) Total_Item,	sum(isnull(QUOTED_QTY,0)) Total_qty,sum(isnull(QUOTED_QTY,0)*isnull(QUOTED_PRICE,0)) Total_Prize,sum(isnull(QUOTED_QTY,0)*isnull(QUOTED_DISCOUNT,0)*isnull(QUOTED_PRICE,0)/100) Total_discount ,Vessel_code from PURC_DTL_QUOTED_PRICES where QUOTATION_CODE  in (select  QUOTATION_CODE  from dbo.PURC_DTL_REQSN where line_type='Q' and REQUISITION_CODE='" + ReqCode + "' and  EVALUATION_OPTION='1' and Vessel_code ='" + VesselCode + "') 	group by Vessel_code,  QUOTATION_CODE, SUPPLIER_CODE,Quotation_Status ) Qprice on SA.SUPPLIER=Qprice.SUPPLIER_CODE  and Qprice.Vessel_code ='" + VesselCode + "' and Qprice.QUOTATION_CODE = '" + QTNCode + "'";
                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "[ExeQuery]", obj);
                dtSupplier = ds.Tables[0];
                return dtSupplier;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public DataTable SelectSupplierToSendOrder(string ReqCode, string VesselCode)
        {
            try
            {
                DataTable dtSupplier = new DataTable();
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[]
             { 
               new System.Data.SqlClient.SqlParameter("@ReqCode",ReqCode),
               new System.Data.SqlClient.SqlParameter("@VesselCode",VesselCode)  
             };
                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_SP_Get_SupplierToSendOrder", obj);
                dtSupplier = ds.Tables[0];
                return dtSupplier;

                //obj.Value = "select SUPPLIER,SHORT_NAME,Qprice.QUOTATION_CODE,Total_Item, convert(varchar(10),getdate(),103) as send_date, 'False' as status   from dbo.Lib_Suppliers  SA inner join (select QUOTATION_CODE, SUPPLIER_CODE,Quotation_Status,count(ITEM_REF_CODE) Total_Item,	sum(isnull(QUOTED_QTY,0)) Total_qty,sum(isnull(QUOTED_QTY,0)*isnull(QUOTED_PRICE,0)) Total_Prize,sum(isnull(QUOTED_QTY,0)*isnull(QUOTED_DISCOUNT,0)*isnull(QUOTED_PRICE,0)/100) Total_discount ,Vessel_code from PURC_DTL_QUOTED_PRICES where QUOTATION_CODE=(select top 1 QUOTATION_CODE  from dbo.PURC_DTL_REQSN where line_type='Q' and REQUISITION_CODE='" + ReqCode+"' and  EVALUATION_OPTION='1' and Vessel_code ='"+VesselCode+"') 	group by Vessel_code,  QUOTATION_CODE, SUPPLIER_CODE,Quotation_Status ) Qprice on SA.SUPPLIER=Qprice.SUPPLIER_CODE  and Qprice.Vessel_code ='"+ VesselCode +"'";
                // obj.Value = "select req.Order_Code, SUPPLIER,SHORT_NAME,Req.QUOTATION_CODE,b.Total_Item, convert(varchar(10),getdate(),103) as send_date, 'False' as status,case when Req.APPROVED_DATE is null then 'NO' else 'YES' end APPROVED_Status ,Req.Vessel_code from dbo.Lib_Suppliers  SA inner join PURC_DTL_REQSN Req on SA.SUPPLIER=req.ORDER_SUPPLIER inner join (select count(*) Total_Item,QUOTATION_CODE, SUPPLIER_CODE from PURC_DTL_QUOTED_PRICES where EVALUATION_OPTION=1 and Vessel_code ='" + VesselCode + "' group by QUOTATION_CODE, SUPPLIER_CODE ) B  on Req.QUOTATION_CODE=B.QUOTATION_CODE and req.ORDER_SUPPLIER=B.SUPPLIER_CODE where Req.line_type='O' and  REQUISITION_CODE='" + ReqCode + "' and Req.Vessel_code ='" + VesselCode + "'"; 

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public DataTable SelectItemsHierarchyForPenDeliveredReqsn(string ReqCode, string VesselCode)
        {

            try
            {
                DataTable dtSupplier = new DataTable();
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[]
             { 
               new System.Data.SqlClient.SqlParameter("@ReqCode",ReqCode),
               new System.Data.SqlClient.SqlParameter("@VesselCode",VesselCode)  
             };
                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_SP_Get_ItemHierchy_Pen_Del_Reqsn", obj);
                dtSupplier = ds.Tables[0];
                return dtSupplier;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }



        public DataTable SelectPOToSendSupplier(string ReqCode, string VesselCode)
        {
            try
            {
                DataTable dtSupplier = new DataTable();
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[]
             { 
               new System.Data.SqlClient.SqlParameter("@ReqCode",ReqCode),
               new System.Data.SqlClient.SqlParameter("@VesselCode",VesselCode) 
               //new SqlParameter("@QTNCode",QTNCode)
             };
                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_SP_Get_POToSendSupplier", obj);
                dtSupplier = ds.Tables[0];
                return dtSupplier;

                //obj.Value = "select SUPPLIER,SHORT_NAME,Qprice.QUOTATION_CODE,Total_Item, convert(varchar(10),getdate(),103) as send_date, 'False' as status   from dbo.Lib_Suppliers  SA inner join (select QUOTATION_CODE, SUPPLIER_CODE,Quotation_Status,count(ITEM_REF_CODE) Total_Item,	sum(isnull(QUOTED_QTY,0)) Total_qty,sum(isnull(QUOTED_QTY,0)*isnull(QUOTED_PRICE,0)) Total_Prize,sum(isnull(QUOTED_QTY,0)*isnull(QUOTED_DISCOUNT,0)*isnull(QUOTED_PRICE,0)/100) Total_discount ,Vessel_code from PURC_DTL_QUOTED_PRICES where QUOTATION_CODE=(select top 1 QUOTATION_CODE  from dbo.PURC_DTL_REQSN where line_type='Q' and REQUISITION_CODE='" + ReqCode+"' and  EVALUATION_OPTION='1' and Vessel_code ='"+VesselCode+"') 	group by Vessel_code,  QUOTATION_CODE, SUPPLIER_CODE,Quotation_Status ) Qprice on SA.SUPPLIER=Qprice.SUPPLIER_CODE  and Qprice.Vessel_code ='"+ VesselCode +"'";
                // obj.Value = "select req.Order_Code, SUPPLIER,SHORT_NAME,Req.QUOTATION_CODE,b.Total_Item, convert(varchar(10),getdate(),103) as send_date, 'False' as status,case when Req.APPROVED_DATE is null then 'NO' else 'YES' end APPROVED_Status ,Req.Vessel_code from dbo.Lib_Suppliers  SA inner join PURC_DTL_REQSN Req on SA.SUPPLIER=req.ORDER_SUPPLIER inner join (select count(*) Total_Item,QUOTATION_CODE, SUPPLIER_CODE from PURC_DTL_QUOTED_PRICES where EVALUATION_OPTION=1 and Vessel_code ='" + VesselCode + "' group by QUOTATION_CODE, SUPPLIER_CODE ) B  on Req.QUOTATION_CODE=B.QUOTATION_CODE and req.ORDER_SUPPLIER=B.SUPPLIER_CODE where Req.line_type='O' and  REQUISITION_CODE='" + ReqCode + "' and Req.Vessel_code ='" + VesselCode + "'"; 

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet GetDataToGeneratPO(string ReqCode, string VesselCode, string SuppCode, string QuotationCode, string DocumentCode)
        {
            try
            {
                DataSet dsPO = new DataSet();

                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[]
             { 
               
               new System.Data.SqlClient.SqlParameter("@SupplierCode",SuppCode),
               new System.Data.SqlClient.SqlParameter("@ReqCode",ReqCode), 
               new System.Data.SqlClient.SqlParameter("@VesselCode",VesselCode), 
               new System.Data.SqlClient.SqlParameter("@QuotationCode",QuotationCode),
               new System.Data.SqlClient.SqlParameter("@DocumentCode",DocumentCode)
             };
                dsPO = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_SP_Generate_PO_PDF", obj);
                return dsPO;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public int UpdReqQuotationStatus(string ReqCode, string VesselCode, string DocumentCode, string suppCode, string UpdatedBy)
        {

            try
            {
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[]
             { 
               new System.Data.SqlClient.SqlParameter("@ReqCode",ReqCode),
               new System.Data.SqlClient.SqlParameter("@VesselCode",VesselCode), 
               new System.Data.SqlClient.SqlParameter("@DocumentCode",DocumentCode),
               new System.Data.SqlClient.SqlParameter("@SuppCode",suppCode),
               new System.Data.SqlClient.SqlParameter("@UpdatedBy",UpdatedBy),
        
             };
                return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_SP_Upd_ReqQuotation_Status", obj);


            }
            catch (Exception ex)
            {
                throw ex;
            }


        }


        public int InsertDataForPO(string ReqCode, string VesselCode, string SuppCode, string QuotationCode, string CreatedBy, string DocumentCode)
        {
            try
            {
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[]
             { 
               new System.Data.SqlClient.SqlParameter("@SupplierCode",SuppCode), 
               new System.Data.SqlClient.SqlParameter("@ReqCode",ReqCode),
               new System.Data.SqlClient.SqlParameter("@VesselCode",VesselCode), 
               new System.Data.SqlClient.SqlParameter("@QuotationCode",QuotationCode), 
               new System.Data.SqlClient.SqlParameter("@CreatedBy",CreatedBy),
               new System.Data.SqlClient.SqlParameter("@DocumentCode",DocumentCode)
               
             };
                return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_SP_Ins_PO_New", obj);

            }
            catch (Exception ex)
            {
                throw ex;
            }


        }


        public DataTable SelectItemsHierarchyForPO(string VesselCode, string REQUISITION_CODE, string REQ_Supplier)
        {
            try
            {
                DataSet dsPO = new DataSet();

                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[]
             { 
               
               new System.Data.SqlClient.SqlParameter("@VesselCode",VesselCode),
               new System.Data.SqlClient.SqlParameter("@RequisitionCode",REQUISITION_CODE), 
               new System.Data.SqlClient.SqlParameter("@SupplierCode",REQ_Supplier)
             };
                dsPO = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_SP_Get_EvalDetail", obj);
                return dsPO.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public int UpdateDataForPO(string ReqCode, string VesselCode, string ItemsString, string QtyString)
        {
            try
            {
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[]
             { 
               new System.Data.SqlClient.SqlParameter("@VesselCode",VesselCode), 
               new System.Data.SqlClient.SqlParameter("@ReqCode",ReqCode),
               new System.Data.SqlClient.SqlParameter("@ItemStrings",ItemsString), 
               new System.Data.SqlClient.SqlParameter("@QtyString",QtyString), 
             };
                return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_SP_UPDATE_PO", obj);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public DataTable PODetails(string RFQCODE, string QTCODE, string SUPLCODE, string DOCCODE, string VESSELCODE)
        {
            try
            {
                DataSet dsPO = new DataSet();

                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[]
             { 
               
               new System.Data.SqlClient.SqlParameter("@RFQCODE",RFQCODE),
               new System.Data.SqlClient.SqlParameter("@QTCODE",QTCODE), 
               new System.Data.SqlClient.SqlParameter("@SUPLCODE",SUPLCODE),
               new System.Data.SqlClient.SqlParameter("@DOCCODE",DOCCODE),
               new System.Data.SqlClient.SqlParameter("@VESSELCODE",VESSELCODE)
             };
                dsPO = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_SP_POReport", obj);
                return dsPO.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public DataTable POArrove(string RFQCODE, string QTCODE, string SUPLCODE, string USERNAME)
        {
            try
            {
                DataSet dsPO = new DataSet();

                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[]
             { 
               
               new System.Data.SqlClient.SqlParameter("@RFQCODE",RFQCODE),
               new System.Data.SqlClient.SqlParameter("@QTCODE",QTCODE), 
               new System.Data.SqlClient.SqlParameter("@SUPLCODE",SUPLCODE),
                new System.Data.SqlClient.SqlParameter("@USERNAME",USERNAME)
             };
                dsPO = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_SP_POApproval", obj);
                return dsPO.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void POApproving(string RFQCODE, string QTCODE, string SUPLCODE, string USERNAME, string Comment, string VesselCode, string BudgetCode)
        {
            try
            {
                DataSet dsPO = new DataSet();

                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[]
             { 
               
               new System.Data.SqlClient.SqlParameter("@RFQCODE",RFQCODE),
               new System.Data.SqlClient.SqlParameter("@QTCODE",QTCODE), 
               new System.Data.SqlClient.SqlParameter("@SUPLCODE",SUPLCODE),
               new System.Data.SqlClient.SqlParameter("@USERNAME",USERNAME),
               new System.Data.SqlClient.SqlParameter("@Comment",Comment),
              new System.Data.SqlClient.SqlParameter("@VesselCode",VesselCode),
              new System.Data.SqlClient.SqlParameter("@Budget_Code", BudgetCode)
             };
                SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_SP_POApproving", obj);
                // dsPO.Tables[0] ;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public DataTable SelectREFQ(string RFQCODE, string VesselCode, string SUPLCODE, string QTCODE)
        {
            try
            {
                DataSet dsPO = new DataSet();

                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[]
             { 
               
               new System.Data.SqlClient.SqlParameter("@RFQCODE",RFQCODE),
               new System.Data.SqlClient.SqlParameter("@VesselCode",VesselCode),
               new System.Data.SqlClient.SqlParameter("@SUPLCODE",SUPLCODE),
               new System.Data.SqlClient.SqlParameter("@QTCODE",QTCODE)
             };
                dsPO = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_SP_SelectREFQ", obj);
                return dsPO.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public int PMSUpdOtherPODetails(string Dlvinstruction, string DlvPort, string ETA, string Remark, string ETD, string AgentDTL, string DOCUMENT_CODE, string REQUISITION_CODE, int Vessel_Code, string QUOTATION_CODE, string QUOTATION_SUPPLIER,int modified_by)
        {
            IFormatProvider provider = new System.Globalization.CultureInfo("en-CA", true);
            DateTime dteta = DateTime.Parse(ETA, provider, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime dtetd = DateTime.Parse(ETD, provider, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { new SqlParameter("@Dlvinstruction",Dlvinstruction),
                                          new SqlParameter("@DlvPort",DlvPort), // For Loan Details this field will be Loan Type from ACC_Lib_Systems_Parameters table
                                          new SqlParameter("@ETA",dteta),
                                          new SqlParameter("@Remark",Remark),
                                          new SqlParameter("@ETD",dtetd),
                                          new SqlParameter("@AgentDTL",AgentDTL),
                                          new SqlParameter("@DOCUMENT_CODE",DOCUMENT_CODE),
                                          new SqlParameter("@REQUISITION_CODE",REQUISITION_CODE),
                                          new SqlParameter("@Vessel_Code",Vessel_Code),
                                          new SqlParameter("@QUOTATION_CODE",QUOTATION_CODE),
                                          new SqlParameter("@QUOTATION_SUPPLIER",QUOTATION_SUPPLIER),
                                          new SqlParameter("@modified_by",modified_by),
                                          new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[12].Direction = ParameterDirection.ReturnValue;

            SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_SP_Upd_OthersPODetails", sqlprm);
            return Convert.ToInt32(sqlprm[12].Value);
        }
        public DataSet CancelPODetails(string RFQCODE, string Order_CODE, string SUPLCODE, string DOCCODE, string VESSELCODE)
        {
            try
            {
                DataSet dsPO = new DataSet();

                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[]
                { 
               
                    new System.Data.SqlClient.SqlParameter("@RFQCODE",RFQCODE),
                    new System.Data.SqlClient.SqlParameter("@ORDER_CODE",Order_CODE), 
                    new System.Data.SqlClient.SqlParameter("@SUPLCODE",SUPLCODE),
                    new System.Data.SqlClient.SqlParameter("@DOCCODE",DOCCODE),
                    new System.Data.SqlClient.SqlParameter("@VESSELCODE",VESSELCODE)
                };
                dsPO = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_SP_CancelPOReport", obj);
                return dsPO;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }



    }
}