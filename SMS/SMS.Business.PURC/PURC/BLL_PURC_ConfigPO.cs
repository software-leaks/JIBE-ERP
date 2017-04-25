using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SMS.Data.PURC;
using System.Data;
using SMS.Properties.PURC;

namespace SMS.Business.PURC
{
    public class BLL_PURC_Config_PO
    {
        DAL_PURC_Config_PO purc_Config_DAL = new DAL_PURC_Config_PO();

        public string PURC_Save_Config__BLL(POConfig POconfig, DataTable ConfigPOTable) // Save PO Configuration
        {
            return purc_Config_DAL.PURC_Save_Config_DAL(POconfig, ConfigPOTable);
        }

        public DataTable PURC_POConfigSearch_BLL(string searchtext, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            return purc_Config_DAL.PURC_POConfigSearch_DAL(searchtext != "" ? searchtext : null, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);
        }
        public DataSet Get_ddlListBLL()
        {
            return purc_Config_DAL.Get_ddlList_DAL();
        }
        public int PURC_Delete_Config_BLL(POConfig POconfig)
        {
            return purc_Config_DAL.PURC_Delete_Config_DAL(POconfig);
        }
        public DataSet PURC_Get_Config_BLL(string Potype)
        {
            return purc_Config_DAL.PURC_Get_Config_DAL(Potype);
        }
        public DataSet PURC_Get_Configured_AccountType(string userid, string Reqsn_Types)
        {
            return purc_Config_DAL.Purc_Get_ConfiguredPO_type(userid, Reqsn_Types);
 
        }
        public DataSet PURC_Get_Configured_ReqsnType()
        {
            return purc_Config_DAL.PURC_Get_Configured_ReqsnType();
 
        }
        public DataSet PURC_Get_Configured_Functions(string Reqsn_type)
        {
            return purc_Config_DAL.PURC_Get_Configured_Functions(Reqsn_type);
 
        }
    }
}
