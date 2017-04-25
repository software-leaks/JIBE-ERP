using System.Data;
using SMS.Properties;
using SMS.Data.PURC;
using System;
using SMS.Data;

namespace SMS.Business.PURC
{

    public partial class BLL_PURC_Purchase
    {

        DAL_PURC_UnitPackings objunit = new DAL_PURC_UnitPackings();

        public DataTable UnitPackings_Search(string searchtext, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            return objunit.UnitPackings_Search(searchtext, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);
        }

        public DataTable UnitPackings_List(int? ID)
        {
            return objunit.UnitPackings_List(ID);
        }

        public int InsertUnitPackings(string MAIN_PACK, string ABREVIATION, int Created_By)
        {
            return objunit.InsertUnitPackings(MAIN_PACK, ABREVIATION, Created_By);
        }

        public int EditUnitPackings(int ID, string MAIN_PACK, string ABREVIATION, int Modified_By)
        {
            return objunit.EditUnitPackings(ID, MAIN_PACK, ABREVIATION, Modified_By);
        }

        public int DeleteUnitPackings(int ID, int Deleted_By)
        {
            return objunit.DeleteUnitPackings(ID, Deleted_By);
        }

    }

}
