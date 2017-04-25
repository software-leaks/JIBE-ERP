using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SMS.Data.Infrastructure;

namespace SMS.Business.INFRA
{
   public class BLL_Infra_Approval_Group_Department
    {
       DAL_Infra_Approval_Group_Department objDAL = new DAL_Infra_Approval_Group_Department();
        public BLL_Infra_Approval_Group_Department()
        {

        }

        public DataSet Get_DepartmentList(string Form_Type, int? GroupID)
        {
            return objDAL.Get_DepartmentList(Form_Type, GroupID);
        }

        public DataTable Get_UserList(string UserType, int? GroupID)
        {
            return objDAL.Get_UserList(UserType, GroupID);
        }

        public DataSet Get_Approval_Group(int? GroupID)
        {
            return objDAL.Get_Approval_Group(GroupID);
        }

        public int INS_Approval_Group(int? GroupID, string Depart_Type, string Group_Name, int UserID)
        {

            return objDAL.INS_Approval_Group(GroupID, Depart_Type, Group_Name, UserID);
        }
        public int INS_Approval_Group_Department(int? GroupID, DataTable dt, string DepartmentType, int? CreatedBy)
        {
            return objDAL.INS_Approval_Group_Department(GroupID, dt, DepartmentType, CreatedBy);
        }
    }
}
