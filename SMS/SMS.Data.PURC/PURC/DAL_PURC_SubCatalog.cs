using System;
using System.Configuration;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using SMS.Data;
using SMS.Properties;
namespace SMS.Data.PURC
{

    public class DAL_PURC_SubCatalog
    {
        private string _internalConnection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;


        public DAL_PURC_SubCatalog()
        {
        }

      

        public DataTable SelectSubCatalogs()
        {

            try
            {
                DataTable dt = new DataTable();
                //System.Data.SqlClient.SqlParameter obj = new System.Data.SqlClient.SqlParameter("@StringQuery", SqlDbType.VarChar);
                //obj.Value = "SELECT [ID],[SYSTEM_CODE],[SUBSYSTEM_CODE],[SUBSYSTEM_DESCRIPTION],[SUBSYSTEM_PARTICULARS],[MAKER],[FUNCTIONS],[SET_INSTALED],[MODULE_TYPE],[PREFERENCE_SUPPLIER] FROM [PURC_LIB_SUBSYSTEMS] where Active_status=1";

                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_SP_Get_SubCatalouge");
                dt = ds.Tables[0];
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public DataTable GetSubCatalogsByCatalogs(string Catlg)
        {

            try
            {
                SqlParameter obj = new SqlParameter("@Catlg", SqlDbType.VarChar);
                obj.Value = Catlg;
                DataTable dt = new DataTable();
                //System.Data.SqlClient.SqlParameter obj = new System.Data.SqlClient.SqlParameter("@StringQuery", SqlDbType.VarChar);
                //obj.Value = "SELECT [ID],[SYSTEM_CODE],[SUBSYSTEM_CODE],[SUBSYSTEM_DESCRIPTION],[SUBSYSTEM_PARTICULARS],[MAKER],[FUNCTIONS],[SET_INSTALED],[MODULE_TYPE],[PREFERENCE_SUPPLIER] FROM [PURC_LIB_SUBSYSTEMS] where Active_status=1";

                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_SP_Get_SubCatalougeByCatalouge", obj);
                dt = ds.Tables[0];
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public DataTable GetSubCatalogid()
        {

            try
            {
                DataTable dt = new DataTable();
                System.Data.SqlClient.SqlParameter obj = new System.Data.SqlClient.SqlParameter("@StringQuery", SqlDbType.VarChar);
                obj.Value = "SELECT [SYSTEM_CODE],[SUBSYSTEM_CODE],[SUBSYSTEM_DESCRIPTION] FROM [PMS_INV_Lid_SUBSYSTEMS_LIBRARY]";

                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "[ExeQuery]", obj);
                dt = ds.Tables[0];
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public int UpdateSubCatalogs(SubCatalogData objSubCatlogDO)
        {

            try
            {
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] { 
                   new System.Data.SqlClient.SqlParameter("@ID",objSubCatlogDO.SubCatalogId), 
                   new System.Data.SqlClient.SqlParameter("@SYSTEM_CODE", objSubCatlogDO.System_code),
                   new System.Data.SqlClient.SqlParameter("@SUBSYSTEM_DESCRIPTION", objSubCatlogDO.SubSystem_Description),
                   new System.Data.SqlClient.SqlParameter("@SUBSYSTEM_PARTICULARS", objSubCatlogDO.Subsystem_Particulars),
                   new System.Data.SqlClient.SqlParameter("@MAKER", objSubCatlogDO.Maker),
                   new System.Data.SqlClient.SqlParameter("@FUNCTIONS", objSubCatlogDO.Functions),
                   new System.Data.SqlClient.SqlParameter("@SET_INSTALED", objSubCatlogDO.Set_Installed),
                   new System.Data.SqlClient.SqlParameter("@MODULE_TYPE", objSubCatlogDO.Model_Type),
                   new System.Data.SqlClient.SqlParameter("@Link", objSubCatlogDO.Link),
                   new System.Data.SqlClient.SqlParameter("@Modified_By", objSubCatlogDO.CurrentUser)};

                return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "[PURC_SP_upd_INV_SUBSYSTEMS_LIBRARY]", obj);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public int DeleteSubCatelogs(SubCatalogData objSubCatlogDO)
        {
            try
            {
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[]
            {
                   new System.Data.SqlClient.SqlParameter("@ID", objSubCatlogDO.SubCatalogId),
                   new System.Data.SqlClient.SqlParameter("@Deleted_By", objSubCatlogDO.CurrentUser)
            };
                return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "[PURC_SP_del_SubSystems_Library]", obj);

            }
            catch (Exception ex)
            {
                throw ex;
            }


        }


        public int SaveSubCatalogs(SubCatalogData objSubCatlogDO)
        {
            try
            {
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@System_code", objSubCatlogDO.System_code),
                   new System.Data.SqlClient.SqlParameter("@SubSYSTEM_DESCRIPTION", objSubCatlogDO.SubSystem_Description),
                   new System.Data.SqlClient.SqlParameter("@SubSYSTEM_PARTICULARS", objSubCatlogDO.Subsystem_Particulars),
                   new System.Data.SqlClient.SqlParameter("@MAKER", objSubCatlogDO.Maker),
                   new System.Data.SqlClient.SqlParameter("@FUNCTIONS", objSubCatlogDO.Functions),
                   new System.Data.SqlClient.SqlParameter("@SET_INSTALED", objSubCatlogDO.Set_Installed),
                   new System.Data.SqlClient.SqlParameter("@MODULE_TYPE", objSubCatlogDO.Model_Type),
                   new System.Data.SqlClient.SqlParameter("@PREFERENCE_SUPPLIER",objSubCatlogDO.Preferred_Supplier),
                   new System.Data.SqlClient.SqlParameter("@Link", objSubCatlogDO.Link),
                   new System.Data.SqlClient.SqlParameter("@Created_By",objSubCatlogDO.CurrentUser) ,
                   new System.Data.SqlClient.SqlParameter("@ReturnID",DbType.Int32)    

                           
                      
            };
                obj[10].Direction = ParameterDirection.ReturnValue;
                int result = SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "[PURC_SP_Ins_INV_SUBSYSTEM_LIBRARY]", obj);
                Int32 ReqturnId = Convert.ToInt32(obj[10].Value);
                return ReqturnId;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public DataSet GetSupplierScope(string SupplCode)
        {
            try
            {
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@Supplier_Code", SupplCode)
            };
                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PMS_Get_Supplier_Scope", obj);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertSupplierScope(string RegScope, string UnRegScope, string CommentString)
        {
            try
            {
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@RegScopexml",RegScope),
                   new System.Data.SqlClient.SqlParameter("@UnRegScopexml", UnRegScope),
                    new System.Data.SqlClient.SqlParameter("@CommentStringxml", CommentString)
                    
            };

                SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "[ASL_INS_Supplier_Scope]", obj);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetCategory_FileType()
        {
            try
            {
                DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.Text, "select code,short_code,description  from PURC_LIB_SYSTEM_PARAMETERS where parent_type='263'");
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



    }

}