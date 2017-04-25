using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SMS.Data.PortageBill;

namespace SMS.Business.PortageBill
{
   public class BLL_PB_Admin
    {

       DAL_PB_Admin obj = new DAL_PB_Admin();

       public DataSet SearchSalaryStructure(int? Parent_Code, string searchtext, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount, int? ParentCode)
        {
            return obj.SearchSalaryStructure(Parent_Code, searchtext, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount,  ParentCode);
        }

        public DataTable Get_SalaryStructureList(int? Code)
        {
            return obj.Get_SalaryStructureList_DL(Code);
        }
        public DataTable Get_SalaryTypeList(int? Code)
        {
            return obj.Get_SalaryTypeList_DL(Code);
        }
        public int InsertSalaryStructure(int? Parent_Code, string Name, string Description, string KeyValue, int? SalaryType, int? Sort_Order, string Flage, int CreatedBy, int AutoPopulate, int VesselSpecific, int? Payableat)
        {
            return obj.InsertSalaryStructure_DL(Parent_Code, Name, Description, KeyValue, SalaryType, Sort_Order, Flage, CreatedBy, AutoPopulate, VesselSpecific,Payableat);
        }

        public int EditSalaryStructure(int Code, int? Parent_Code, string Name, string Description, string KeyValue, int? SalaryType, int? Sort_Order, string Flage, int ModifiedBy, int AutoPopulate, int VesselSpecific,int? Paybleat)
        {
            return obj.EditSalaryStructure_DL(Code, Parent_Code, Name, Description, KeyValue, SalaryType, Sort_Order, Flage, ModifiedBy, AutoPopulate, VesselSpecific, Paybleat);
        }

        public int DeleteSalaryStructure(int Code, int DeletedBy)
        {
            return obj.DeleteSalaryStructure_DL(Code, DeletedBy);
        }

        public DataTable Get_SalaryStructureParentList()
        {
            return obj.Get_SalaryStructureParentList_DL();
        }

        public int Swap_Sort_Order(int Code,int Parent_Code, int MoveUpDown, int Modified_By)
        {
            return obj.Swap_Sort_Order_DL(Code,Parent_Code, MoveUpDown, Modified_By);
        
        }
        public DataTable Get_KeyValueList()
        {
            return obj.Get_KeyValueList_DL();
        }
        public int INS_CASH_ADV_LIMIT(int? ID, int? RankCat, int? Percent, int UserId, string Mode)
        {
            return obj.INS_CASH_ADV_LIMIT(ID, RankCat, Percent, UserId, Mode);
        }
        public DataTable GetCashAdvanceLimit()
        {
            return obj.GET_CASH_ADV_LIMIT();
        }
        public DataTable GetRankCashAdvanceLimit()
        {
            return obj.GetRankCashAdvanceLimit();
        }

    }
}
