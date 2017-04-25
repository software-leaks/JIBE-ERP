using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SMS.Data.PURC;
using System.Data;

namespace SMS.Business.PURC
{
    public class BLL_PURC_Permissions
    {
         DAL_PURC_Permissions  objDALPrmsion = new  DAL_PURC_Permissions();

         public int SavePermissions(DataTable dtsave, DataTable dtDepUser, string AccessID, string AccessType, int user, string departments, string savetype)
        {

            return objDALPrmsion.SavePermissions(dtsave,dtDepUser, AccessID, AccessType, user, departments, savetype);

        }
        public  DataTable PURC_Get_UserTypeAccess(int UserID, string Variable_Type)
        {

            return objDALPrmsion.PURC_Get_UserTypeAccess(UserID,Variable_Type);

        }
        public DataSet PURC_GetDepartmentUsers(string DepartmentId, string accessId)
        {

            return objDALPrmsion.PURC_GetDepartmentUsers(DepartmentId, accessId);

        }
        public DataSet Get_Dep_Cat_SubCat(string user)
        {
            return objDALPrmsion.Get_Dep_Cat_SubCat(user);
        }
        public DataSet PURC_GET_CAT_SUBCAT(string Filter, string types)
        {
            return objDALPrmsion.PURC_GET_CAT_SUBCAT(Filter, types);
        }
        public DataSet PURC_GET_PermitedUsers(DataTable PermitedValues, DataTable PermitedUser, string Gettypes)
        {
            return objDALPrmsion.PURC_GET_PermitedUsers(PermitedValues, PermitedUser, Gettypes);
        }
    }

}
