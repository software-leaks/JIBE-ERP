using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Configuration;

namespace SMS.Data.PURC
{
    public class DAL_PURC_Permissions
    {
        private static string _internalConnection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
        public int SavePermissions(DataTable dtSave, DataTable dtUserDep, string AccessID, string AccessType, int user, string departments,string savetype)
        {
            try
            {
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@User_Permission", dtSave),
                   new System.Data.SqlClient.SqlParameter("@LIB_Approval", dtUserDep),
                   new System.Data.SqlClient.SqlParameter("@AccessID",AccessID),
                   new System.Data.SqlClient.SqlParameter("@AccessType", AccessType),
                   new System.Data.SqlClient.SqlParameter("@CurrentUser", user),
                   new System.Data.SqlClient.SqlParameter("@SaveType", savetype),
            };
                int result = SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_INS_Users_Permissions", obj);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DataTable PURC_Get_UserTypeAccess(int UserID, string Variable_Type)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] { 
                 new System.Data.SqlClient.SqlParameter("@userid", UserID),
                 new System.Data.SqlClient.SqlParameter("@Variable_Type", Variable_Type),


                
            };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_Get_UserAccessTypeWise", obj).Tables[0];
        }
        public DataSet PURC_GetDepartmentUsers(string DepartmentId,string accessId)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] { 
                 new System.Data.SqlClient.SqlParameter("@dep_ids", DepartmentId),
                 new System.Data.SqlClient.SqlParameter("@accessId", accessId),

        };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_GetDepartmentUsers", obj);
        }
        public DataSet Get_Dep_Cat_SubCat(string user)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] { 
                 new System.Data.SqlClient.SqlParameter("@user", user),};
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_Get_UserAccessed_Func_Sys_SubSys", obj);
        }
        public DataSet PURC_GET_CAT_SUBCAT(string filter,string types)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] { 
                 new System.Data.SqlClient.SqlParameter("@Filter", filter), new System.Data.SqlClient.SqlParameter("@Gettype", types),};
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_GET_CAT_SUBCAT", obj);
        }

        public DataSet PURC_GET_PermitedUsers(DataTable PermitedValues,DataTable PermitedUser, string Gettypes)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] { 
                  new System.Data.SqlClient.SqlParameter("@User_Permission", PermitedValues),
             new System.Data.SqlClient.SqlParameter("@LIB_Approval", PermitedUser),
           new System.Data.SqlClient.SqlParameter("@GETType", Gettypes),};
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_GET_PermitedUsers", obj);
        }
        
    }
}
