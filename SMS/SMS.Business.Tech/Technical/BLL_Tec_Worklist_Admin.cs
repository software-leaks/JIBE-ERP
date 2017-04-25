using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SMS.Data.Technical;

namespace SMS.Business.Technical
{
    public class BLL_Tec_Worklist_Admin
    {
       
        DAL_Tec_Worklist_Admin Obj = new DAL_Tec_Worklist_Admin();

        public DataTable SearchAssigner(string searchtext, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            return Obj.SearchAssigner(searchtext != "" ? searchtext : null, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);
        }

        public DataTable Get_Assigner_List(int? AssignerID)
        {
            return Obj.Get_Assigner_List_DL(AssignerID);
        }

        public int EditAssigner(int AssignerID, string AssignerValue, int CreatedBy)
        {
            return Obj.EditAssigner_DL(AssignerID, AssignerValue, CreatedBy);
        }

        public int InsertAssigner(string AssignerValue, int CreatedBy)
        {
            return Obj.InsertAssigner_DL(AssignerValue, CreatedBy);
        }

        public int DeleteAssigner(int AssignerID, int CreatedBy)
        {
            return Obj.DeleteAssigner_DL(AssignerID, CreatedBy);
        }

    }
}
