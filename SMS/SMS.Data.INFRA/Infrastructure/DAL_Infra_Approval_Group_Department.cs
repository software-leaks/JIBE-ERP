using System;
using System.Collections.Generic;


using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;

namespace SMS.Data.Infrastructure
{
    public class DAL_Infra_Approval_Group_Department
    {
        IFormatProvider iFormatProvider = CultureInfo.GetCultureInfo("en-GB");

        private string connection = "";
        public DAL_Infra_Approval_Group_Department(string ConnectionString)
        {
            connection = ConnectionString;
        }
        public DAL_Infra_Approval_Group_Department()
        {
            connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
        }


        public DataSet Get_DepartmentList(string Form_Type, int? GroupID)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@Form_Type", Form_Type),
                   new System.Data.SqlClient.SqlParameter("@GroupID", GroupID)
            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INF_Get_Approval_Group_Department_List", obj);
        }

        public DataTable Get_UserList(string UserType, int? GroupID)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@UserType", UserType),
                   new System.Data.SqlClient.SqlParameter("@GroupID", GroupID)
            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INF_Get_Approval_Group_UserList", obj).Tables[0];
        }

        public DataSet Get_Approval_Group(int? GroupID)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@GroupID", GroupID)
            };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INF_Get_Approval_Group_Details", obj);
        }

        public int INS_Approval_Group(int? GroupID, string Depart_Type, string Group_Name, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                          new SqlParameter("@GroupID",GroupID),
                                          new SqlParameter("@Depart_Type",Depart_Type),
                                          new SqlParameter("@Group_Name",Group_Name),
                                          new SqlParameter("@Created_By",UserID),
                                         };
            return Convert.ToInt16(SqlHelper.ExecuteScalar(connection, CommandType.StoredProcedure, "INF_INS_UPD_Approval_Group", sqlprm));
        }

        public int INS_Approval_Group_Department(int? GroupID, DataTable dt, string DepartmentType, int? CreatedBy)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                 new System.Data.SqlClient.SqlParameter("@GroupID", GroupID),
                 new System.Data.SqlClient.SqlParameter("@dt", dt),
                 new System.Data.SqlClient.SqlParameter("@DepartmentType", DepartmentType),
                 new System.Data.SqlClient.SqlParameter("@CreatedBy", CreatedBy),
            };

            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "INF_INS_Approval_Group_Department", obj);
        }
    }
}
