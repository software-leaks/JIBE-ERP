using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;


namespace SMS.Data.ASL
{
   public  class DAL_TypeManagement
    {

        IFormatProvider iFormatProvider = CultureInfo.GetCultureInfo("en-GB");
        private string connection = "";
        public DAL_TypeManagement(string ConnectionString)
        {
            connection = ConnectionString;
        }

        public DAL_TypeManagement()
        {
            connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
        }

        public DataTable Get_SysVariable_Type_DL()
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "ASL_Get_SysVariable_Type").Tables[0];
        }
        public DataTable Get_UserTypeAccess_DL(string variable_Type, int userid, int LoginUser)
        {
            SqlParameter[] obj = new SqlParameter[] { 
                new SqlParameter("@variable_Type", variable_Type),
                 new SqlParameter("@userid", userid),
                new SqlParameter("@loginUser",LoginUser)
            };
            DataSet ds = new DataSet(); SqlHelper.FillDataset(connection, CommandType.StoredProcedure, "ASL_Get_UserTypeAccess", ds, new string[] { "usdnme" }, obj);
            return ds.Tables[0];
        }
        public DataTable Get_UserTypeAccess(int UserID, string Variable_Type, string Variable_Code, string Approver_Type)
        {
            SqlParameter[] obj = new SqlParameter[] { 
                 new SqlParameter("@userid", UserID),
                 new SqlParameter("@Variable_Type", Variable_Type),
                 new SqlParameter("@Variable_Code", Variable_Code),
                 new SqlParameter("@Approver_Type", Approver_Type)
            };
            DataSet ds = new DataSet(); SqlHelper.FillDataset(connection, CommandType.StoredProcedure, "ASL_Get_UserAccessTypeWise", ds, new string[] { "usdnme" }, obj);
            return ds.Tables[0];
        }
        public int Update_User_Type_Access_DL(int UserID, string VCode, int View, int Add, int Edit, int Delete, int Approve, int Admin, string Variable_Type, int Created_By)
        {
            SqlParameter[] obj = new SqlParameter[]{
                new SqlParameter("@UserID",UserID),
                new SqlParameter("@VCode",VCode),
                new SqlParameter("@Access",View),
                new SqlParameter("@Access_Add",Add),
                new SqlParameter("@Access_Edit",Edit),
                new SqlParameter("@Access_Delete",Delete),
                new SqlParameter("@Access_Approve",Approve),
                new SqlParameter("@Access_Admin",Admin),
                new SqlParameter("@Variable_Type",Variable_Type),
                new SqlParameter("@Created_By",Created_By)
            };

            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "ASL_Update_User_Type_Access", obj);
            return 1;

        }
        public int Copy_TypeAccessFromUser_DL(int CopyFromUserID, int CopyToUserID, int AppendMode, string Selected_Mod_Code, int Created_By)
        {
            SqlParameter[] obj = new SqlParameter[]{
                new SqlParameter("@CopyFromUserID",CopyFromUserID),
                new SqlParameter("@CopyToUserID",CopyToUserID),
                new SqlParameter("@AppendMode",AppendMode),
                new SqlParameter("@Selected_Mod_Code",Selected_Mod_Code),
                new SqlParameter("@Created_By",Created_By)
            };

            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "ASL_Copy_TypeAccessFromUser", obj);
            return 1;
        }

    }
}
