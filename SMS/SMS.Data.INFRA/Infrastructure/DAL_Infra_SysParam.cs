using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;



/// <summary>
/// Summary description for BLL_Infra_SysParam
/// </summary>
/// 
namespace SMS.Data.Infrastructure
{
    public class DAL_Infra_SysParam
    {

        IFormatProvider iFormatProvider = CultureInfo.GetCultureInfo("en-GB");

        private string connection = "";
        public DAL_Infra_SysParam(string ConnectionString)
        {
            connection = ConnectionString;
        }
        public DAL_Infra_SysParam()
        {
            connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
        }


        //public DataTable GetModules(int status)
        //{
        //    DataSet ds = new DataSet();
        //    if (status != 2)
        //    {
        //        SqlParameter[] obj = new SqlParameter[] { new SqlParameter("@active", SqlDbType.Bit) };
        //        obj[0].Value = status;
        //        SqlHelper.FillDataset(connection, CommandType.StoredProcedure, "SP_INF_Param_GetModules", ds, new string[] { "Modules" }, obj);
        //    }
        //    else
        //        SqlHelper.FillDataset(connection, CommandType.StoredProcedure, "SP_INF_Param_GetModules", ds, new string[] { "Modules" });
        //    return ds.Tables[0];
        //}

        public int InsertSysModule_DL(string name, string TableName, int user, int active)
        {
            SqlParameter[] obj = new SqlParameter[]
            {
                new SqlParameter("@name",SqlDbType.VarChar),
                new SqlParameter("@tablename",SqlDbType.VarChar),
                new SqlParameter("@user",SqlDbType.BigInt),
                new SqlParameter("@active",SqlDbType.BigInt)
            };
            obj[0].Value = name;
            obj[1].Value = TableName;
            obj[2].Value = user;
            obj[3].Value = active;
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_INF_Param_InsertModule", obj);
        }

        public DataSet GetModulesTable_DL()
        {

            DataSet ds = new DataSet();
            SqlHelper.FillDataset(connection, CommandType.StoredProcedure, "SP_INS_Get_Tables_FromSysTables", ds, new string[] { "tables" });
            return ds;
        }


        public DataTable GetListValues_DL(string tablename, string parentcode, string status)
        {
            string Query = "";
            if (parentcode == "")
            {
                Query = "select code,name from " + tablename + " where Parent_Code is null";
                if (status != "2")
                    Query += " and Active_Status=" + status + " order by name";
            }
            else
            {
                Query = "select code,name from " + tablename + " where Parent_Code =" + parentcode;
                if (status != "2")
                    Query += " and Active_Status=" + status + " order by name";
            }
            DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.Text, Query);
            return ds.Tables[0];


        }


        public int InsertSysParameter_DL(string query)
        {
            return SqlHelper.ExecuteNonQuery(connection, CommandType.Text, query);
        }


        public int DeleteSysParameter_DL(string query)
        {
            return SqlHelper.ExecuteNonQuery(connection, CommandType.Text, query);
        }


        public DataSet GetDetailOfSystemParameter_DL(int code, string tablename)
        {
            string Query = "Select code,name,description,Active_Status from " + tablename + " where Code=" + code.ToString();
            return SqlHelper.ExecuteDataset(connection, CommandType.Text, Query);
        }

        public int UpdateSysParam_DL(int ChildId, string Name, int Parent, string Description, int Active, string tablename, int user)
        {
            string Query = "";
            if (Parent == -1)
                Query = "update " + tablename + " set Parent_Code=null,name='" + Name.ToString() + "',description='" + Description.ToString() + "',Active_Status=" + Active.ToString() + ",Modified_By=" + user.ToString() + ",Date_Of_Modification=GetDate() where Code=" + ChildId.ToString();
            else
                Query = "update " + tablename + " set Parent_Code=" + Parent.ToString() + ",name='" + Name.ToString() + "',description='" + Description.ToString() + "',Active_Status=" + Active.ToString() + ",Modified_By=" + user.ToString() + ",Date_Of_Modification=GetDate() where Code=" + ChildId.ToString();
            return SqlHelper.ExecuteNonQuery(connection, CommandType.Text, Query);
        }
    }
}