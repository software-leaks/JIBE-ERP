using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SMS.Data;
using SMS.Properties;

namespace SMS.Data.TRAV
{
    public class DAL_TRV_Refund
    {
        SqlConnection conn;
        public DataSet Get_Refund_Requests_DL(int fleetid, int vesselid, int agentid, string sectorfrom, string sectorto, string trvFrom, string trvto, 
             string status ,string searchtext
            , int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString);
            DataTable dtRequest = new DataTable("Request");
            DataTable dtPax = new DataTable("Pax"); ;
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter[] sqlprm = { 
                                            new SqlParameter("@FleetID", fleetid), 
                                       new SqlParameter("@VesselID", vesselid),                                       
                                       new SqlParameter("@TravelAgent", agentid),
                                       new SqlParameter("@SectorFrom", sectorfrom),
                                       new SqlParameter("@SectorTo", sectorto),
                                       new SqlParameter("@TravelFrom", trvFrom),
                                       new SqlParameter("@TravelTo", trvto),
                                       new SqlParameter("@Status", status),
                                       new SqlParameter("@SearchText", searchtext),
                                       new SqlParameter("@PAGENUMBER",pagenumber),
                                       new SqlParameter("@PAGESIZE",pagesize),
                                       new SqlParameter("@ISFETCHCOUNT",isfetchcount) 
                                       
                                    };
                sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.InputOutput;
                Ds = SqlHelper.ExecuteDataset(conn, CommandType.StoredProcedure, "CRW_TRV_SP_Get_Refund_TravelRequests", sqlprm);
                isfetchcount = Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
                Ds.Tables[0].TableName = "Request";

                Ds.Relations.Add("RequestPax", Ds.Tables[0].Columns["RequestID"], Ds.Tables[1].Columns["RequestID"]);
                return Ds;
            }
            catch { throw; }
            finally { conn.Close(); }
        }


        public Boolean UpdateRefund_DL(int id, decimal no_show_amout, decimal cancellation_amount,
            decimal refund_amout, decimal amount_received, string modify_remarks, int modified_by)
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString);
            try
            {
                SqlParameter[] sqlprm = { new SqlParameter("@id", id), 
                                       new SqlParameter("@no_show_amount", no_show_amout),                                       
                                       new SqlParameter("@cancellation_amount", cancellation_amount),
                                       new SqlParameter("@refund_amount", refund_amout),
                                       new SqlParameter("@amount_received", amount_received),
                                       new SqlParameter("@modify_remarks", modify_remarks),
                                       new SqlParameter("@modified_by", modified_by),
                                    };

                SqlHelper.ExecuteNonQuery(conn, CommandType.StoredProcedure, "CRW_TRV_SP_Update_Refund", sqlprm);
                return true;
            }
            catch { throw; }
            finally { conn.Close(); }
        }

    }
}
