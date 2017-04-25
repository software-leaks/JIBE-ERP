using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//Customer defined libaries
using SMS.Data;
using SMS.Properties;

namespace SMS.Data.SLC
{
   public class DAL_SLC_ConsumptionReport
    {
       private string _internalConnection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;

        public DataTable Get_SlopChestConsumptionReport_DL(string DATE, string CREW, int? ITEM, int? PAGE_INDEX, int? PAGE_SIZE, ref int IS_FETCH_COUNT)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@DATE",DATE),    
                     new System.Data.SqlClient.SqlParameter("@CREW",CREW),   
                       new System.Data.SqlClient.SqlParameter("@ITEM", ITEM),   
                       new System.Data.SqlClient.SqlParameter("@PAGE_INDEX", PAGE_INDEX),   
                       new System.Data.SqlClient.SqlParameter("@PAGE_SIZE", PAGE_SIZE),   
                      new System.Data.SqlClient.SqlParameter("@IS_FETCH_COUNT", IS_FETCH_COUNT),     
                    
            };

            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "SlopChest_GET_ConsumptionReport", obj);
            //System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INSP_GET_ALLChecklist");
            IS_FETCH_COUNT = int.Parse(obj[obj.Length - 1].Value.ToString());

            dt = ds.Tables[0];
                    return dt;
                
        }


        public DataTable SelectItems()
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            try
            {

                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "SlopChest_SP_Get_Items");
                dt = ds.Tables[0];
                return dt;
            }
            catch (Exception ex)
            {

                dt = null;
                throw ex;
            }
        }

    }
}
