using System;
using System.Configuration;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using SMS.Data;
using SMS.Properties;

/// <summary>
/// Summary description for DALItems
/// </summary>
namespace SMS.Data.PURC
{
    public class DAL_PURC_Items
    {
        private string _internalConnection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;


        public DAL_PURC_Items()
        {
        }

        


        public int SaveItems(ItemsData objDOItems)
        {

            try
            {
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   
                   new System.Data.SqlClient.SqlParameter("@Item_Intern_Ref", objDOItems.ItemInternRef),
                   new System.Data.SqlClient.SqlParameter("@System_Code", objDOItems.System_code),
                   new System.Data.SqlClient.SqlParameter("@Subsystem_Code", objDOItems.SubSystem_Code),
                   new System.Data.SqlClient.SqlParameter("@Part_Number", objDOItems.Part_Number),
                   new System.Data.SqlClient.SqlParameter("@Short_Description", objDOItems.Short_Description),
                   new System.Data.SqlClient.SqlParameter("@Long_Description", objDOItems.Long_Description),
                   new System.Data.SqlClient.SqlParameter("@Unit_and_Packings", objDOItems.Unit_and_Packings),
                   new System.Data.SqlClient.SqlParameter("@Inventory_Min",objDOItems.Min_Qty ),
                   new System.Data.SqlClient.SqlParameter("@Inventory_Max",objDOItems.Max_Qty ),
                   new System.Data.SqlClient.SqlParameter("@Drawing_Number", objDOItems.Drawing_Number),
                   new System.Data.SqlClient.SqlParameter("@Item_Address", objDOItems.Item_Address),
                   new System.Data.SqlClient.SqlParameter("@Link", objDOItems.Link),
                   new System.Data.SqlClient.SqlParameter("@Created_By", objDOItems.CurrentUser),
                   new System.Data.SqlClient.SqlParameter("@ReturnID",DbType.Int32) 
            };
                obj[13].Direction = ParameterDirection.ReturnValue;
                int result = SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "[PURC_SP_Ins_INV_ITEM]", obj);
                Int32 ReqturnId = Convert.ToInt32(obj[13].Value);
                return ReqturnId;
            }
            catch (Exception ex)
            {
                throw ex;

            }


        }


        public int DeleteItems(ItemsData objDOItems)
        {
            try
            {
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[]
            {
                   new System.Data.SqlClient.SqlParameter("@ID", objDOItems.ItemID),
                   new System.Data.SqlClient.SqlParameter("@Deleted_By", objDOItems.CurrentUser)
            };
                return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "[PURC_SP_del_INV_ITEM]", obj);
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }


        public int EditItems(ItemsData objDoItems)
        {

            try
            {
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] { 
                   new System.Data.SqlClient.SqlParameter("@ItemID", objDoItems.ItemID),
                   //new System.Data.SqlClient.SqlParameter("@ITEM_INTERN_REF", objDoItems.ItemInternRef),
                   new System.Data.SqlClient.SqlParameter("@Part_number", objDoItems.Part_Number),
                   new System.Data.SqlClient.SqlParameter("@SHORT_DESCRIPTION", objDoItems.Short_Description),
                   new System.Data.SqlClient.SqlParameter("@LONG_DESCRIPTION", objDoItems.Long_Description),
                   new System.Data.SqlClient.SqlParameter("@UNIT_and_Packings", objDoItems.Unit_and_Packings),
                   new System.Data.SqlClient.SqlParameter("@INVENTORY_MIN", objDoItems.Min_Qty),
                   new System.Data.SqlClient.SqlParameter("@INVENTORY_MAX", objDoItems.Max_Qty),
                   new System.Data.SqlClient.SqlParameter("@DRAWING_NUMBER", objDoItems.Drawing_Number),
                   new System.Data.SqlClient.SqlParameter("@Item_Address", objDoItems.Item_Address),
                   new System.Data.SqlClient.SqlParameter("@Link", objDoItems.Link),
                   new System.Data.SqlClient.SqlParameter("@Modified_By", objDoItems.CurrentUser)
              };
                return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "[PURC_SP_upd_INV_ITEM]", obj);
            }
            catch (Exception ex)
            {
                throw ex;

            }

        }

        public DataTable SelectItems(string CatalogID, string SubCatalogID)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            try
            {
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@CatalogID", CatalogID),
                   new System.Data.SqlClient.SqlParameter("@SubCatalogID",SubCatalogID)
            };
                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_SP_Get_Items", obj);
                dt = ds.Tables[0];
                return dt;
            }
            catch (Exception ex)
            {

                dt = null;
                throw ex;
            }


        }


        public DataTable SelectUnitnPackage()
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            try
            {
                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_SP_Get_UnitnPackage");
                dt = ds.Tables[0];
                return dt;
            }
            catch (Exception ex)
            {

                dt = null;
                throw ex;
            }
        }

        public DataSet SelectUnitnPackageDataSet()
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            try
            {
                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_SP_Get_UnitnPackage");
                return ds;
               
            }
            catch (Exception ex)
            {

               
                throw ex;
            }
        }

    }
}