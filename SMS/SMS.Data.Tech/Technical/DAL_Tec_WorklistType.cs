using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SMS.Properties;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace SMS.Data.Technical
{
    public class DAL_Tec_WorklistType
    {
        public static string connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
        public DAL_Tec_WorklistType(string ConnectionString)
        {
            connection = ConnectionString;
        }
        public DAL_Tec_WorklistType()
        {

        }
        public static void TEC_UPD_WORKLISTTYPE(string Worklist_Type, string Worklist_Type_Display)
        {
             
            SqlParameter[] sqlprm = new SqlParameter[]
            { 
                new SqlParameter("@Worklist_Type",Worklist_Type),
                new SqlParameter("@Worklist_Type_Display",Worklist_Type_Display), 
            };
           
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "TEC_UPD_WORKLISTTYPE", sqlprm); 
            
        }

        public static DataSet TEC_GET_WORKLISTYPE()
        {

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "TEC_GET_WORKLISTYPE", null);

        }
    }

       
}
