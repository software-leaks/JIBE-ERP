using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//custom defined libaries
using SMS.Data.TRAV;

namespace SMS.Business.TRAV
{
    public class BLL_TRV_Invoice
    {
        public DataSet Get_Request_For_Invoice(int fleetid, int vesselid, int agentid,
            string dateFrom, string dateTo, string status,int UserID)
        {
            DAL_TRV_Invoice objInvoice = new DAL_TRV_Invoice();
            try
            {
                return objInvoice.Get_Request_For_Invoice_DL(fleetid, vesselid, agentid, dateFrom, dateTo, status, UserID);
            }
            catch { throw; }
            finally { objInvoice = null; }
        }


        public Boolean Save_Invoice(int RequestID, string Invoice_Number, DateTime Invoice_Date,
            decimal Invoice_Amount, string currency, DateTime Due_Date, int Created_by, string Remarks)
        {
            DAL_TRV_Invoice objInvoice = new DAL_TRV_Invoice();
            try
            {
                return objInvoice.Save_Invoice_DL(RequestID, Invoice_Number, Invoice_Date,
                    Invoice_Amount, currency, Due_Date, Created_by, Remarks);
            }
            catch { throw; }
            finally { objInvoice = null; }
        }

        //
        public Boolean Save_Invoice(int ID, int Updated_by)
        {
            DAL_TRV_Invoice objInvoice = new DAL_TRV_Invoice();
            try
            {
                return objInvoice.Pay_Invoice_DL(ID, Updated_by);
            }
            catch { throw; }
            finally { objInvoice = null; }
        }

    }
}
