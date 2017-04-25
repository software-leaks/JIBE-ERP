using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using System.Configuration;

namespace SMS.Data.LMS
{
    public class DAL_LMS_Help
    {
         private static string connection = "";
        public DAL_LMS_Help(string ConnectionString)
        {
            connection = ConnectionString;
        }
        static DAL_LMS_Help()
        {
            connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
        }

        public static DataSet Get_Enabled_Menu()
        {
            DataSet dt = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "LMS_GET_ENABLED_MENU_LIST", null);
            return dt;
        }
        public static DataSet Get_Help_Resources(int? Menu_Code)
        {
            SqlParameter[] prm = new SqlParameter[] {

                                                    
                                                      new SqlParameter("@Menu_Code",Menu_Code), 

                                                    };
            DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "LMS_GET_HELP_RESOURCES", prm);
            return ds;
        }
        public static void InsertUpdate_Help_Resource(int Menu_Code, int Parent_ID, string Parent_Type, int UserID)
        {
            SqlParameter[] prm = new SqlParameter[] {

                                                    
                                                      new SqlParameter("@Menu_Code",Menu_Code),
                                                      new SqlParameter("@Parent_ID",Parent_ID),
                                                      new SqlParameter("@Parent_Type",Parent_Type),
                                                      new SqlParameter("@UserID",UserID),
                                                   

                                                    };
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "LMS_INSUPD_HELP", prm);
             
        }

        public static void Delete_Help_Resource(int? Help_ID, int? UserID)
        {
            SqlParameter[] prm = new SqlParameter[] {

                                                    
                                                      new SqlParameter("@Help_ID",Help_ID),
                                                      new SqlParameter("@UserID",UserID),
                                                    

                                                    };

            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "LMS_DEL_HELP", prm);

          

        }
    }
}
