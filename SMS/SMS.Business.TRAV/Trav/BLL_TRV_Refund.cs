using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//custom defined libararies
using SMS.Properties;
using SMS.Data.TRAV;

namespace SMS.Business.TRAV
{
    public class BLL_TRV_Refund
    {
        public DataSet GetRefund_RequestList(int fleetid, int vesselid, int agentid, string sectorfrom, string sectorto, string trvFrom, string trvto
            , string status, string searchtext
            , int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            DAL_TRV_Refund Refund = new DAL_TRV_Refund();
            try {
                
                return Refund.Get_Refund_Requests_DL(fleetid, vesselid, agentid,sectorfrom,sectorto,trvFrom,trvto, status,searchtext,pagenumber,pagesize,ref isfetchcount);
            
            }
            catch { throw; }
        }


        public Boolean UpdateRefund(int id, decimal no_show_amout, decimal cancellation_amount,
            decimal refund_amout, decimal amount_received, string modify_remarks, int modified_by)
        {
            DAL_TRV_Refund Refund = new DAL_TRV_Refund();
            try
            {
                return Refund.UpdateRefund_DL(id, no_show_amout, cancellation_amount,
                    refund_amout, amount_received, modify_remarks, modified_by);
            }
            catch { throw; }
        }

    }
}
