
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SMS.Data.Infrastructure;
using System.Data;
using SMS.Data.INFRA;

namespace SMS.Business.Infrastructure
{
    public class BLL_Infra_PortageBillRule
    {
        DAL_Infra_PortageBillRule ObjDAL = new DAL_Infra_PortageBillRule();
      

        public BLL_Infra_PortageBillRule()
        {

        }


        public DataTable Portage_Bill_Rule_Search(string searchtext, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            return ObjDAL.Portage_Bill_Rule_Search(searchtext, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);
        }
        public int Insert_Portage_Bill_Rule(string Rule_Name, string Value, int Type)
        {
            return ObjDAL.Insert_Portage_Bill_Rule(Rule_Name, Value, Type);
        }

        public int Edit_Portage_Bill_Rule(int ID, string Rule_Name, string Value, int Type)
        {
            return ObjDAL.Edit_Portage_Bill_Rule_DL(ID, Rule_Name, Value, Type);
        }
        public DataTable Get_Portage_Bill_Rule(int? ID)
        {
            return ObjDAL.Get_Portage_Bill_Rule_DL(ID);
        }

        //public int Delete_Lashing_Gear_Inventory(int ID, int Vessel_ID, int CreatedBy)
        //{
        //    return ObjDAL.Delete_Lashing_Gear_Inventory_DL(ID, Vessel_ID, CreatedBy);
        //}
    }
}