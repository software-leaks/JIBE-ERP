using System;
using System.Configuration;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using SMS.Data;
using SMS.Properties;
/// <summary>
/// Summary description for DALInvoiceBudget
/// </summary>
namespace SMS.Data.PURC
{
    public class DAL_PURC_InvoiceBudget
    {
        private string _internalConnection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;

        public DAL_PURC_InvoiceBudget()
        {

        }

       


        public DataSet SelectInvoiceDetails(string VesselCode)
        {
            try
            {
                DataSet dsInvDe = new System.Data.DataSet();

                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[]
             { 
               new System.Data.SqlClient.SqlParameter("@VesselCode",VesselCode), 
             };

                return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_SP_GET_INVOICE_DETAILS", obj);

            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public DataSet SelectBudgetCode()
        {
            try
            {
               
                return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_SP_Get_BudgetCode");
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
        public DataSet SelectBudgetCode(string ReqsnCode, string VesselCode, int Reqsn_Type)
        {
            try
            {
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[]
             { 
               new System.Data.SqlClient.SqlParameter("@ReqsnCode",ReqsnCode), 
                new System.Data.SqlClient.SqlParameter("@VesselCode",VesselCode),
                new System.Data.SqlClient.SqlParameter("@Reqsn_Type",Reqsn_Type),
             };
                return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_Get_BudgetCode", obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
        public string Check_Update_BudgetCode(string ReqsnCode, string VesselCode, string BudgetCode,DataTable  dtQtn)
        {
            try
            {
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[]
             { 
               new System.Data.SqlClient.SqlParameter("@ReqsnCode",ReqsnCode), 
                new System.Data.SqlClient.SqlParameter("@VesselCode",VesselCode),
                new System.Data.SqlClient.SqlParameter("@BudgetCode",BudgetCode),
                new System.Data.SqlClient.SqlParameter("@dtQtn",dtQtn),
             };
               
                return (string)SqlHelper.ExecuteScalar(_internalConnection, CommandType.StoredProcedure, "PURC_UPD_BudgetCode", obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
        public int InsertInvoiceDetails(string VesselCode, string OrderCode, decimal OrderAmt,
                                    string InvoiceNo, string InvoiceDate, decimal InvoiceAmt,
                                    string InvoiceSupplier, string ApporveStatus, string ApporveBy, string ApproveComments,
                                    int CreatedBy, string strFilePath, string strFileName, string strFileType,
             string ReqsnCode, string CatalogID, string InvoiceType, string InvoiceDueDate)
        {

            IFormatProvider provider = new System.Globalization.CultureInfo("en-CA", true);
            DateTime dInvoiceDate = DateTime.Parse(InvoiceDate, provider, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime dInvoiceDueDate = DateTime.Parse(InvoiceDueDate, provider, System.Globalization.DateTimeStyles.NoCurrentDateDefault);




            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[]
             { 
               new System.Data.SqlClient.SqlParameter("@VesselCode",VesselCode),
               new System.Data.SqlClient.SqlParameter("@ORDER_CODE",OrderCode),
               new System.Data.SqlClient.SqlParameter("@ORDER_Amount",OrderAmt),
               new System.Data.SqlClient.SqlParameter("@INVOICE_NO",InvoiceNo),
               new System.Data.SqlClient.SqlParameter("@INVOICE_DATE",dInvoiceDate),
               new System.Data.SqlClient.SqlParameter("@INVOICE_Amount",InvoiceAmt),
               new System.Data.SqlClient.SqlParameter("@INVOICE_SUPPLIER",InvoiceSupplier),
               new System.Data.SqlClient.SqlParameter("@APPROVER_Status",ApporveStatus),
               new System.Data.SqlClient.SqlParameter("@APPROVE_By",ApporveBy),
               new System.Data.SqlClient.SqlParameter("@APPROVE_Comments",ApproveComments),
               new System.Data.SqlClient.SqlParameter("@Created_By",CreatedBy),
               new System.Data.SqlClient.SqlParameter("@FilePaths",strFilePath),
               new System.Data.SqlClient.SqlParameter("@FileNames",strFileName),
               new System.Data.SqlClient.SqlParameter("@FileType",strFileType),
               new System.Data.SqlClient.SqlParameter("@REQCODE",ReqsnCode),
               new System.Data.SqlClient.SqlParameter("@SystemCode",CatalogID),
               new System.Data.SqlClient.SqlParameter("@InvoiceType",InvoiceType),
               new System.Data.SqlClient.SqlParameter("@invoiceDueDate",dInvoiceDueDate)


             };

            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PMS_INS_Invoice_Details", obj);


        }


        public DataTable GetPONumberForInvoicing(string VesselCode)
        {
            try
            {
                DataTable dtInvDe = new System.Data.DataTable();

                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[]
             { 
               new System.Data.SqlClient.SqlParameter("@VesselCode",VesselCode), 
             };

                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_SP_Get_PONumber_For_Invoice", obj);
                dtInvDe = ds.Tables[0];
                return dtInvDe;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public int UpdateInvoiceApproval(string VesselCode, string OrderCode, string InvoiceNo, string strComments, int intApproveBy)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[]
             { 
               new System.Data.SqlClient.SqlParameter("@VesselCode",VesselCode),
               new System.Data.SqlClient.SqlParameter("@ORDER_CODE",OrderCode),
               new System.Data.SqlClient.SqlParameter("@InvoiceNo",InvoiceNo),
               new System.Data.SqlClient.SqlParameter("@Comments" ,strComments),
               new System.Data.SqlClient.SqlParameter("@ApproveBy" ,intApproveBy),
             };

            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PMS_Upd_Invoice_Approval", obj);


        }


        public DataSet Get_PODetails_For_Invoice(string VesselCode, string OrderCode)
        {
            try
            {

                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[]
             { 
               new System.Data.SqlClient.SqlParameter("@VesselCode",VesselCode), 
               new System.Data.SqlClient.SqlParameter("@ORDERCODE",OrderCode)  
             };

                return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_SP_Get_PODetails_For_Invoice", obj);

            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
    }
}