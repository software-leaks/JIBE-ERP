using System;
using System.Data;
using SMS.Data.PURC;
using SMS.Properties;
using SMS.Data;



/// <summary>
/// Summary description for InvoiceBudget
/// </summary>

namespace SMS.Business.PURC
{

    public partial class BLL_PURC_Purchase
    {

        DAL_PURC_InvoiceBudget objInvoiceBudget = new DAL_PURC_InvoiceBudget();

        public DataSet SelectInvoiceDetails(string VesselCode)
        {

            return objInvoiceBudget.SelectInvoiceDetails(VesselCode);



        }

        public DataSet SelectBudgetCode()
        {

            return objInvoiceBudget.SelectBudgetCode();

        }
        public DataSet SelectBudgetCode(string ReqsnCode, string VesselCode, int Reqsn_Type)
        {

            return objInvoiceBudget.SelectBudgetCode(ReqsnCode, VesselCode, Reqsn_Type);

        }
        public string Check_Update_BudgetCode(string ReqsnCode, string VesselCode,string BudgetCode,DataTable dtQtn)
        {

            return objInvoiceBudget.Check_Update_BudgetCode(ReqsnCode, VesselCode, BudgetCode, dtQtn);

        }
        public int InsertInvoiceDetails(string VesselCode, string OrderCode, decimal OrderAmt,
                                string InvoiceNo, string InvoiceDate, decimal InvoiceAmt,
                                string InvoiceSupplier, string ApporveStatus, string ApporveBy, string ApproveComments,
                                int CreatedBy, string strFilePath, string strFileName, string strFileType
                                , string ReqsnCode, string CatalogID, string InvoiceType, string InvoiceDueDate)
        {

            return objInvoiceBudget.InsertInvoiceDetails(VesselCode, OrderCode, OrderAmt, InvoiceNo, InvoiceDate, InvoiceAmt, InvoiceSupplier, ApporveStatus, ApporveBy, ApproveComments, CreatedBy, strFilePath, strFileName, strFileType, ReqsnCode, CatalogID, InvoiceType, InvoiceDueDate);


        }


        public DataTable GetPONumberForInvoicing(string VesselCode)
        {

            return objInvoiceBudget.GetPONumberForInvoicing(VesselCode);


        }


        public int UpdateInvoiceApproval(string VesselCode, string OrderCode, string InvoiceNo, string strComments, int intApproveBy)
        {

            return objInvoiceBudget.UpdateInvoiceApproval(VesselCode, OrderCode, InvoiceNo, strComments, intApproveBy);


        }


        public DataSet Get_PODetails_For_Invoice(string VesselCode, string OrderCode)
        {

            return objInvoiceBudget.Get_PODetails_For_Invoice(VesselCode, OrderCode);

        }

    }

}