using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

using SMS.Data.Infrastructure;

/// <summary>
/// Summary description for BLL_Infra_Port
/// </summary>
/// 
namespace SMS.Business.Infrastructure
{
    public class BLL_Infra_Approval_Group
    {
        DAL_Infra_Approval_Group objApprGroup = new DAL_Infra_Approval_Group();

        public BLL_Infra_Approval_Group()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public DataTable Get_Approval_Group_List_DL(int Group_ID)
        {
            return objApprGroup.Get_Approval_Group_List_DL(Group_ID);
        }

        public DataTable Get_Approval_Group_Search(string searchText, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            return objApprGroup.Get_Approval_Group_Search(searchText, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);
        }

        public int INS_Approval_Group(string Group_Name, int Created_By)
        {
            return objApprGroup.INS_Approval_Group_DL(Group_Name, Created_By);
        }

        public int Upd_Approval_Group(int? Group_ID, string Group_Name, int Created_By)
        {
            return objApprGroup.Upd_Approval_Group_DL(Group_ID, Group_Name, Created_By);
        }

        public int Del_Approval_Group(int? ID, int Created_By)
        {
            return objApprGroup.Del_Approval_Group_DL(ID, Created_By);
        }


      
    }
}
