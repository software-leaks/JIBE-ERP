using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;

namespace SMS.Data.Infrastructure
{
    public class DAL_Infra_Approval_Type
    {
        IFormatProvider iFormatProvider = CultureInfo.GetCultureInfo("en-GB");

        private string connection = "";
        public DAL_Infra_Approval_Type(string ConnectionString)
        {
            connection = ConnectionString;
        }

        public DAL_Infra_Approval_Type()
        {
            connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
        }

        public int INS_Approval_Limit_DL(string Type_Key, string Type_Value,int Amount_Applicable, int Created_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            { 
                new SqlParameter("@Type_Key",Type_Key),
                new SqlParameter("@Type_Value",Type_Value),
                 new SqlParameter("@Amount_Applicable",Amount_Applicable),
                new SqlParameter("@Created_By",Created_By),
            };

            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "INF_INS_Approval_Type", sqlprm);
        }
        public int UPD_Approval_Type_DL(int ID, string Type_Value,int AMOUNT_APPLICABLE, int Modified_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            { 
                new SqlParameter("@ID",ID),
                new SqlParameter("@Type_Value",Type_Value),
                new SqlParameter("@AMOUNT_APPLICABLE",AMOUNT_APPLICABLE),
                new SqlParameter("@Modified_By",Modified_By),
            };

            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "INF_UPD_Approval_Type", sqlprm);
        }
        public DataTable Get_Approval_Type_DL(string searchText)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            { 
                new SqlParameter("@searchText",searchText),
            };

            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INF_GET_Approval_Type", sqlprm);
            return ds.Tables[0];
        }
        public int DEL_Approval_Type_DL(int ID, int Deleted_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            { 
                new SqlParameter("@ID",ID),
                new SqlParameter("@Deleted_By",Deleted_By),
            };

            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "INF_DEL_Approval_Type", sqlprm);
        }
        

    }
}
