using System;
using System.Collections.Generic;
 
 
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;


/// <summary>
/// Summary description for BLL_Infra_Currency
/// </summary>
/// 
namespace SMS.Data.Infrastructure
{
    public class DAL_Infra_Currency
    {
        IFormatProvider iFormatProvider = CultureInfo.GetCultureInfo("en-GB");

        private string connection = "";
        public DAL_Infra_Currency(string ConnectionString)
        {
            connection = ConnectionString;
        }
        public DAL_Infra_Currency()
        {
            connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;

        }



        public DataTable SearchCurrency(string searchtext, int? countrycode, int? isfavorite
           , string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@SerchText", searchtext),
                   new System.Data.SqlClient.SqlParameter("@CountryCode", countrycode),
                   new System.Data.SqlClient.SqlParameter("@IsFavorite", isfavorite), 
                    
                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                    
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_INF_Get_Currency_Search", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds.Tables[0];
        }



        public DataTable Get_CurrencyList_DL()
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_INF_Get_CurrencyList").Tables[0];
        }

        public int EditCurrency_DL(string Currency_Code, string Currency_Discription, int Country, int Currency_ID, bool favorite, int Created_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { new SqlParameter("@curr_id",Currency_ID),
                                          new SqlParameter("@curr_code",Currency_Code),
                                          new SqlParameter("@curr_desp",Currency_Discription),
                                          new SqlParameter("country",Country),
                                          new SqlParameter("@favorite",favorite),
                                          new SqlParameter("@Created_By",Created_By)
                                         };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_INF_Update_Currency", sqlprm);
        }

        public int InsertCurrency_DL(string Currency_Code, string Currency_Discription, int Country, bool favorite, int Created_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { new SqlParameter("@currcode",Currency_Code),
                                          new SqlParameter("@currdesc",Currency_Discription),
                                          new SqlParameter("@country",Country),
                                          new SqlParameter("@favorite",favorite),
                                          new SqlParameter("@Created_By",Created_By)
                                         };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_INF_Insert_Currency", sqlprm);
        }

        public int DeleteCurrency_DL(int Currency_ID, int Created_By)
        {
    
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CURR_ID",Currency_ID),
                                            new SqlParameter("@Created_By",Created_By)
                                        };

            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_INF_Del_Currency", sqlprm);
        }


        public DataTable ExecuteQuery(string SQL)
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.Text, SQL).Tables[0];
        }
    }

}