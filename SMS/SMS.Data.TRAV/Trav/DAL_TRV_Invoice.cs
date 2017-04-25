using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//custom defined libaries
using SMS.Properties;
using SMS.Data.TRAV;

namespace SMS.Data.TRAV
{
    public class DAL_TRV_Invoice
    {
        SqlConnection conn;
        public DataSet Get_Request_For_Invoice_DL(int fleetid, int vesselid, int agentid,
            string dateFrom, string dateTo, string status, int UserID)
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString.ToString());
            try
            {
                SqlParameter[] sqlprm = { 
                                            new SqlParameter("@FleetID", fleetid), 
                                            new SqlParameter("@VesselID", vesselid),                                                                              
                                            new SqlParameter("@TravelAgent", agentid),
                                            new SqlParameter("@DateFrom", dateFrom),
                                            new SqlParameter("@DateTo", dateTo),
                                            new SqlParameter("@Status", status),
                                            new SqlParameter("@UserID", UserID)
                                        };

                return SqlHelper.ExecuteDataset(conn, CommandType.StoredProcedure, "CRW_TRV_SP_Get_RequestForInvoice", sqlprm);
            }
            catch { throw; }
            finally { conn = null; }
        }

        public Boolean Save_Invoice_DL(int RequestID, string Invoice_Number, DateTime Invoice_Date,
            decimal Invoice_Amount, string Currency, DateTime Due_Date, int Created_by, string Remarks)
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString);
            try
            {
                SqlParameter[] sqlprm = { new SqlParameter("@RequestID", RequestID),
                                        new SqlParameter("@Invoice_Number", Invoice_Number),
                                        new SqlParameter("@Invoice_Date", Invoice_Date),
                                        new SqlParameter("@Invoice_Amount", Invoice_Amount),
                                        new SqlParameter("@Currency", Currency),
                                        new SqlParameter("@Due_Date", Due_Date),
                                        new SqlParameter("@Created_by", Created_by),
                                        new SqlParameter("@Remarks", Remarks)
                                        };
                SqlHelper.ExecuteNonQuery(conn, CommandType.StoredProcedure, "CRW_TRV_SP_Add_Invoice", sqlprm);
                return true;
            }
            catch { throw; }
        }

        public Boolean Pay_Invoice_DL(int ID, int Updated_by)
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString);
            try
            {
                SqlParameter[] sqlprm = { new SqlParameter("@ID", ID),
                                        new SqlParameter("@Updated_by", Updated_by),
                                        };
                SqlHelper.ExecuteNonQuery(conn, CommandType.StoredProcedure, "CRW_TRV_SP_Pay_Invoice", sqlprm);
                return true;
            }
            catch { throw; }
        }
    }
}
