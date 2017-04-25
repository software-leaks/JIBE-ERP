using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using SMS.Data.Infrastructure;

namespace SMS.Business.Infrastructure
{
   public  class BLL_Infra_CompMailAction
    {
       DAL_Infra_CompMailAction objDAL = new DAL_Infra_CompMailAction();

        public BLL_Infra_CompMailAction()
        {
            //
            // TODO: Add constructor logic here
            //
        }


        public DataTable SearchCompMailAction(string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            return objDAL.SearchCompMailAction(sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);

        }

        public DataTable Get_CompMailAction(int CMailID)
         {
             return objDAL.Get_CompMailAction(CMailID);

         }


        public int EditCompMailAction(string Subject, string MailTo,string MailCc, string Body, int CMailID, int Created_By)
        {
            try
            {
                return objDAL.EditCompMailAction(Subject, MailTo, MailCc, Body, CMailID, Created_By);
            }
            catch
            {
                throw;
            }
        }

        
    }
}
