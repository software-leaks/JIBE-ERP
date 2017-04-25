using System;
using System.Collections.Generic;


using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;

namespace SMS.Data.Infrastructure
{
    public class DAL_Infra_CompMailAction
    {
        IFormatProvider iFormatProvider = CultureInfo.GetCultureInfo("en-GB");

        private string connection = "";
        public DAL_Infra_CompMailAction(string ConnectionString)
        {
            connection = ConnectionString;
        }
        public DAL_Infra_CompMailAction()
        {
            connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;

        }



        public DataTable SearchCompMailAction(string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 

                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                    
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_INF_Get_CompanyMainAction_Search", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds.Tables[0];
        }
        public DataTable Get_CompMailAction(int CMailID)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@CMailID", CMailID),

            };
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_INF_Get_CompanyMainAction_List", obj);
            return ds.Tables[0];
        }




        public int EditCompMailAction(string Subject, string MailTo, string MailCc, string Body, int CMailID, int Created_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { new SqlParameter("@Subject",Subject),
                                          new SqlParameter("@MailTo",MailTo),
                                          new SqlParameter("@MailCc",MailCc),
                                          new SqlParameter("@Body",Body),
                                          new SqlParameter("@CMailID",CMailID),
                                          new SqlParameter("@Created_By",Created_By)
                                         };
            return Convert.ToInt32(SqlHelper.ExecuteScalar(connection, CommandType.StoredProcedure, "SP_INF_Update_CompanyMainAction", sqlprm));
        }

       

       
    }
}
