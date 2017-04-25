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
    public class DAL_SLC_Admin
    {
        private string _internalConnection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
        private  static string _internalConnection1 = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
        public DAL_SLC_Admin()
        {

        }

        public DataTable Get_SlopchestIndex_DL(int? VESSEL_ID, int? YEAR, int? MONTH, int PAGE_INDEX, int PAGE_SIZE, ref int IS_FETCH_COUNT)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@VESSEL_ID", VESSEL_ID),    
                     new System.Data.SqlClient.SqlParameter("@YEAR", YEAR),   
                       new System.Data.SqlClient.SqlParameter("@MONTH", MONTH),   
                       new System.Data.SqlClient.SqlParameter("@PAGE_INDEX", PAGE_INDEX),   
                       new System.Data.SqlClient.SqlParameter("@PAGE_SIZE", PAGE_SIZE),   
                      new System.Data.SqlClient.SqlParameter("@IS_FETCH_COUNT", IS_FETCH_COUNT),     
                    
            };

            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_Get_Slopchest_Index", obj);
            //System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INSP_GET_ALLChecklist");
            IS_FETCH_COUNT = int.Parse(obj[obj.Length - 1].Value.ToString());
            return ds.Tables[0];
        }

        public DataTable Get_SlopchestSettings_DL()
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_GET_SlopChest_Settings");
            return ds.Tables[0];
        }



        public int INSERT_SCSettings_DL(string Key, int value, int? Created_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Key",Key),
                                            new SqlParameter("@value",value),
                                            new SqlParameter("@Created_By",Created_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_DTL_Insert_SCSetting", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }


        public DataTable Get_SlopChestConsumptionReport_DL(int? DATE, int? CREW, int? ITEM, int PAGE_INDEX, int PAGE_SIZE, ref int IS_FETCH_COUNT)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@DATE", DATE),    
                     new System.Data.SqlClient.SqlParameter("@CREW", CREW),   
                       new System.Data.SqlClient.SqlParameter("@ITEM", ITEM),   
                       new System.Data.SqlClient.SqlParameter("@PAGE_INDEX", PAGE_INDEX),   
                       new System.Data.SqlClient.SqlParameter("@PAGE_SIZE", PAGE_SIZE),   
                      new System.Data.SqlClient.SqlParameter("@IS_FETCH_COUNT", IS_FETCH_COUNT),     
                    
            };

            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "SlopChest_GET_ConsumptionReport", obj);
            //System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INSP_GET_ALLChecklist");
            IS_FETCH_COUNT = int.Parse(obj[obj.Length - 1].Value.ToString());
            return ds.Tables[0];
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


        public static DataTable Get_SlopChest_Items()
        {
            return SqlHelper.ExecuteDataset(_internalConnection1, CommandType.StoredProcedure, "PURC_GET_SlopChest_Items", null).Tables[0];
        }

        public static int INS_UPD_SlopChest_Commision(string STR, int UserID)
        {

            SqlParameter[] prm = new SqlParameter[]{ 
                                                     
                                                     new SqlParameter("@STR", STR) ,
                                                     new SqlParameter("@USERID", UserID)  
                                                    };

            return SqlHelper.ExecuteNonQuery(_internalConnection1, CommandType.StoredProcedure, "PURC_INS_UPD_SlopChest_Commision", prm);
        }
    }
}
