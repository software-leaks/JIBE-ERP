using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using SMS.Data;
using System.Configuration;

namespace SMS.Data.eForms
{
    public class DAL_Infra_LashingGearInventoryLib
    {
        private static string connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;

        public DAL_Infra_LashingGearInventoryLib()
        {
            connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
        }
        public DataTable Lashing_Gear_Inventory_Search(string searchtext, string ItemModel, int? Vessel_ID, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@SerchText", searchtext),
                   new System.Data.SqlClient.SqlParameter("@Model_No", ItemModel),
                  
                   new System.Data.SqlClient.SqlParameter("@Vessel_ID", Vessel_ID),

                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                    
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "FRM_LIB_LASHING_GEAR_INVENTORY_Search", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds.Tables[0];


        }
        public int Insert_Lashing_Gear_Inventory(string Item_Description, string Model_No, string Carg_Securing_Mannual_No, int? Vessel_ID, int CreatedBy)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Item_Description",Item_Description),
                                            new SqlParameter("@Model_No",Model_No),
                                             new SqlParameter("@Carg_Securing_Mannual_No",Carg_Securing_Mannual_No),
                                            new SqlParameter("@Vessel_ID",Vessel_ID),
                                            new SqlParameter("@CreatedBy",CreatedBy),
                                            
                                         };

            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "FRM_LIB_INSERT_LASHING_GEAR_INVENTORY", sqlprm);
        }

        public int Lashing_Gear_Inventory_DL(int ID, string Item_Description, string Model_No, string Carg_Securing_Mannual_No, int? Vessel_ID, int CreatedBy)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ID",ID),
                                            new SqlParameter("@Item_Description",Item_Description),
                                            new SqlParameter("@Model_No",Model_No),
                                            new SqlParameter("@Carg_Securing_Mannual_No",Carg_Securing_Mannual_No),
                                            new SqlParameter("@Vessel_ID",Vessel_ID),
                                            new SqlParameter("@CreatedBy",CreatedBy),
                                            
                                         };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "FRM_LIB_UPDATE_Lashing_Gear_Inventory", sqlprm);
        }

        public DataTable Lashing_Gear_Inventory_DL(int? ID, int Vessel_ID)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@ID", ID),
                   new SqlParameter("@Vessel_ID",Vessel_ID),
            };

            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "FRM_LIB_Lashing_Gear_Inventory_List", obj);

            return ds.Tables[0];
        }

        public int Delete_Lashing_Gear_Inventory_DL(int ID, int Vessel_ID, int CreatedBy)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ID",ID),
                                            new SqlParameter("@Vessel_ID",Vessel_ID),
                                            new SqlParameter("@CreatedBy",CreatedBy),
                                            
                                         };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "FRM_LIB_Delete_Lashing_Gear_Inventory", sqlprm);
        }
    }
}
