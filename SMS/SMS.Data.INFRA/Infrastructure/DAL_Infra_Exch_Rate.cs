using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;
using SMS.Data;


namespace SMS.Business.Infrastructure
{
   public class DAL_Infra_Exch_Rate
   {


         IFormatProvider iFormatProvider = CultureInfo.GetCultureInfo("en-GB");

        private string connection = "";

        public DAL_Infra_Exch_Rate(string ConnectionString)
        {
            connection = ConnectionString;
        }

        public DAL_Infra_Exch_Rate()
        {
            connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;

        }

        public DataTable SearchExchangeRate(string searchtext,int? CurrPrivious, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@SearchText", searchtext),
                  new System.Data.SqlClient.SqlParameter("@Current_Previous",CurrPrivious), 
                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                    
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INF_Get_Exch_Rate_List_Search", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds.Tables[0];
        }

        public DataTable Get_ExchangeRate_List_DL(string CURR_CODE, DateTime? CURR_DATE)
        {

            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@Curr_code", CURR_CODE),
                   new System.Data.SqlClient.SqlParameter("@Date", CURR_DATE),
            };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INF_GET_Exch_Rate_DETAILS_List", obj).Tables[0];
        }

        public int Edit_ExchangeRate_DL(string CURR_CODE, decimal? EXCH_RATE, DateTime? CURR_DATE, int CREATED_BY)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CURR_CODE",CURR_CODE),
                                            new SqlParameter("@EXCH_RATE",EXCH_RATE),
                                            new SqlParameter("@CURR_DATE",CURR_DATE),
                                            new SqlParameter("@CREATED_BY",CREATED_BY),
                                         };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "INF_UPD_Exch_Rate", sqlprm);
        }

        public int Edit_ExchangeRate_DL(string CURR_CODE, decimal? EXCH_RATE, DateTime? CURR_DATE)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Curr_code",CURR_CODE),
                                            new SqlParameter("@Exch_rate",EXCH_RATE),
                                            new SqlParameter("@Date",CURR_DATE), 
                                            
                                         };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "INF_UPD_Exch_Rate", sqlprm);
        }


        public int Insert_ExchangeRate_DL(string CURR_CODE, decimal? EXCH_RATE, DateTime? CURR_DATE)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Curr_code",CURR_CODE),
                                            new SqlParameter("@Exch_rate",EXCH_RATE),
                                            new SqlParameter("@Date",CURR_DATE),                                          
                                           
                                         };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "INF_INS_Exch_Rate", sqlprm);
        }

       //
        //public int Insert_ExchangeRate_DL(string CURR_CODE, decimal? EXCH_RATE, DateTime? CURR_DATE, int CREATED_BY)
        //{
        //    SqlParameter[] sqlprm = new SqlParameter[]
        //                                { 
        //                                    new SqlParameter("@CURR_CODE",CURR_CODE),
        //                                    new SqlParameter("@EXCH_RATE",EXCH_RATE),
        //                                    new SqlParameter("@CURR_DATE",CURR_DATE),
        //                                    new SqlParameter("@CREATED_BY",CREATED_BY),
                                            
        //                                 };
        //    return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "INF_INS_Exch_Rate", sqlprm);
        //}
        public int Delete_ExchangeRate_DL(string CURR_CODE, DateTime? CURR_DATE, int CreatedBy)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CURR_CODE",CURR_CODE),
                                            new SqlParameter("@CURR_DATE",CURR_DATE),
                                            new SqlParameter("@Created_By",CreatedBy),

                                         };

            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "INF_DEL_Exch_Rate", sqlprm);
        }

        public int Delete_ExchangeRate_DL(string CURR_CODE,  DateTime? CURR_DATE)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CURR_CODE",CURR_CODE),
                                          
                                            new SqlParameter("@Date",CURR_DATE),

                                         };

            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "INF_DEL_Exch_Rate", sqlprm);
        }

        public DataTable Check_Exchange_List(string Curr_code, DateTime? Date)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                 new System.Data.SqlClient.SqlParameter("@Curr_code", Curr_code) ,   
                 new System.Data.SqlClient.SqlParameter("@Date", Date)   
            };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INF_CHECKDUP_Exch_Rate", obj).Tables[0];
          
        }
   }





}
