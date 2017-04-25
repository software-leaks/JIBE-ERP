using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.SqlClient;
using SMS.Data;
using System.Configuration;
using SMS.Properties;

namespace SMS.Data.TRAV
{
    public class DAL_TRV_Supplier
    {
        SqlConnection conn;

        /// <summary>
        /// Created by EGSoft
        /// </summary>
        /// <returns>Datable with the list of all active suppliers</returns>
        public DataTable GetSuppliers(string Supplier_Category, string Supplier_Search)
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString);
            try
            {
                SqlParameter[] sqlprm = {new SqlParameter("@Supplier_Category", Supplier_Category),
                                            new SqlParameter("@Supplier_Search", Supplier_Search)};

                return SqlHelper.ExecuteDataset(conn, CommandType.StoredProcedure, "PURC_Get_SupplierList", sqlprm).Tables[0];
            }
            catch { throw; }
            finally { conn.Close(); }
        }

        /// <summary>
        /// Get Supplier email id
        /// </summary>
        /// <param name="id">Id of supplier</param>
        /// <returns>Email as string</returns>
        public string GetSupplier_Email(int id)
        {
            string result = "";
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString);
            try
            {
                SqlDataReader dr;
                SqlParameter sqlprm = new SqlParameter("ID", id);
                dr = SqlHelper.ExecuteReader(conn, CommandType.StoredProcedure, "CRW_TRV_SP_Get_SupplierEmail", sqlprm);
                if (dr.HasRows)
                {
                    dr.Read();
                    result = dr.GetString(0).ToString();
                }
                return result;
            }
            catch { throw; }
        }
        public static DataTable Get_SupplierList_DL(string Supplier_Search)
        {
            string strConn = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;

            SqlParameter[] prm = new SqlParameter[]
            {
                new SqlParameter("@Supplier_Search",Supplier_Search)
                            
            };
            return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "CRW_TRV_SP_Get_SupplierList", prm).Tables[0];
        }

        public static DataTable Get_SupplierList_DL(string Supplier_Search, int RFQSent_REQ_ID)
        {
            string strConn = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;

            SqlParameter[] prm = new SqlParameter[]
            {
                new SqlParameter("@Supplier_Search",Supplier_Search),
                new SqlParameter("@RFQSent_REQ_ID",RFQSent_REQ_ID)
                            
            };
            return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "CRW_TRV_SP_Get_SupplierList", prm).Tables[0];
        }

    }
}
