using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SMS.Data;
using SMS.Properties;

namespace SMS.Data.SLC
{
    public class DAL_SLC_Report
    {
        private string _internalConnection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
        public DataTable SelectVessel()
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            try
            {

                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "SlopChest_Get_Vessel");
                dt = ds.Tables[0];
                return dt;
            }
            catch (Exception ex)
            {

                dt = null;
                throw ex;
            }
        }

        public DataTable Get_SlopchestReport_DL(int? VESSEL_ID, int? YEAR, int? MONTH)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@VESSEL_ID", VESSEL_ID),    
                     new System.Data.SqlClient.SqlParameter("@YEAR", YEAR),   
                       new System.Data.SqlClient.SqlParameter("@MONTH", MONTH)   
                      
            };

            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "SLC_Get_Slopchest_Report", obj);
            return ds.Tables[0];
        }
    }
}
