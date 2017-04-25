using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace SMS.Data.Technical
{
    public class DAL_QMS_Test
    {
        public static string ConnectionString
        {
            get 
            { 
                return ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString; 
            }            
        }

        public static DataTable QMS_Test_Search()
        {            
            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] {};
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(DAL_Tec_ErLog.ConnectionString, CommandType.StoredProcedure, "PRQMS_TESTSEARCH", obj);
            return ds.Tables[0];
            
        }
        public static DataTable QMS_Test_Edit(int ID)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            {                   
                  new System.Data.SqlClient.SqlParameter("@Id", ID)                 
                    
            };

            System.Data.DataSet ds = SqlHelper.ExecuteDataset(DAL_Tec_ErLog.ConnectionString, CommandType.StoredProcedure, "PRQMS_TESTEDIT", obj);

            return ds.Tables[0];
        }
        public static int QMS_Test_Update(int id, string details)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            {                   
                  new System.Data.SqlClient.SqlParameter("@Id", id) ,
                  new System.Data.SqlClient.SqlParameter("@NOTES", details)    
                    
            };

            return SqlHelper.ExecuteNonQuery(DAL_Tec_ErLog.ConnectionString, CommandType.StoredProcedure, "PRQMS_TESTUPDATE", obj);

        }
        public static int QMS_Test_Insert(string details)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            {                   
                 new System.Data.SqlClient.SqlParameter("@NOTES", details)                
                    
            };

            return SqlHelper.ExecuteNonQuery(DAL_Tec_ErLog.ConnectionString, CommandType.StoredProcedure, "PRQMS_TESTINSERT", obj);
        }

    }
}
