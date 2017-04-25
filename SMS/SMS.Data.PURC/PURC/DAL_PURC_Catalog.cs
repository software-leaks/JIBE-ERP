using System.Data;
using System.Configuration;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using SMS.Data;
using SMS.Properties;
using System;

/// <summary>
/// Summary description for DALCatalog
/// </summary>
namespace SMS.Data.PURC
{
    public class DAL_PURC_Catalog
    {

        private string _internalConnection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
        private static string _Connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;


        public DAL_PURC_Catalog()
        {
        }



        public int SaveCatalog(CatalogData objDOCatalog)
        {

            try
            {
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[]
            { 
                   new System.Data.SqlClient.SqlParameter("@SYSTEM_DESCRIPTION", objDOCatalog.System_Description),
                   new System.Data.SqlClient.SqlParameter("@SYSTEM_PARTICULARS", objDOCatalog.System_Particulars),
                   new System.Data.SqlClient.SqlParameter("@MAKER", objDOCatalog.Maker),
                   new System.Data.SqlClient.SqlParameter("@FUNCTIONS", objDOCatalog.Functions),
                   new System.Data.SqlClient.SqlParameter("@SET_INSTALED", objDOCatalog.Set_Installed),
                   new System.Data.SqlClient.SqlParameter("@MODULE_TYPE", objDOCatalog.Model_Type),
                   new System.Data.SqlClient.SqlParameter("@PREFERENCE_SUPPLIER", objDOCatalog.Preferred_Supplier),
                   new System.Data.SqlClient.SqlParameter("@Link", objDOCatalog.Link),
                   new System.Data.SqlClient.SqlParameter("@Vessel_Code", objDOCatalog.VesselCode),
                   new System.Data.SqlClient.SqlParameter("@Created_By", objDOCatalog.CurrentUser),
                   new System.Data.SqlClient.SqlParameter("@updsqlDeptCatalog", objDOCatalog.updateQuery),
                   new System.Data.SqlClient.SqlParameter("@ReturnID",DbType.Int32)    
            };
                obj[11].Direction = ParameterDirection.ReturnValue;
                int result = SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "[PURC_SP_Ins_INV_SYSTEMS_LIBRARY]", obj);
                Int32 ReqturnId = Convert.ToInt32(obj[11].Value);
                return ReqturnId;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public int DeleteCatalog(CatalogData objDOCatalog)
        {
            try
            {

                System.Data.SqlClient.SqlParameter[] sqlpram = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@ID", objDOCatalog.CatalogID),
                   new System.Data.SqlClient.SqlParameter("@Deleted_By", objDOCatalog.CurrentUser)
            };

                SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "[PURC_SP_del_INV_Systems_Library]", sqlpram);
                return 0;
            }
            catch (Exception ex)
            {

                throw ex;

            }

        }

        public int EditCatalog(CatalogData objDoCatalog)
        {
            int retValue;
            try
            {
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[]
            { 
                   new System.Data.SqlClient.SqlParameter("@System_Code", objDoCatalog.SystemCode),
                   new System.Data.SqlClient.SqlParameter("@System_Description", objDoCatalog.System_Description),
                   new System.Data.SqlClient.SqlParameter("@System_Particulars", objDoCatalog.System_Particulars),
                   new System.Data.SqlClient.SqlParameter("@Maker", objDoCatalog.Maker),
                   new System.Data.SqlClient.SqlParameter("@Functions", objDoCatalog.Functions),
                   new System.Data.SqlClient.SqlParameter("@Set_Instaled", objDoCatalog.Set_Installed),
                   new System.Data.SqlClient.SqlParameter("@Module_Type", objDoCatalog.Model_Type),
                   new System.Data.SqlClient.SqlParameter("@Preference_Supplier", objDoCatalog.Preferred_Supplier),
                   new System.Data.SqlClient.SqlParameter("@Link", objDoCatalog.Link),
                   new System.Data.SqlClient.SqlParameter("@Vessel_Code", objDoCatalog.VesselCode),
                   new System.Data.SqlClient.SqlParameter("@Modified_By", objDoCatalog.CurrentUser)
            };
                retValue = SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "[PURC_SP_upd_INV_SYSTEMS_LIBRARY]", obj);
            }
            catch (Exception ex)
            {
                retValue = 0;

            }
            return retValue;

        }

        public DataTable SelectCatalog()
        {
            try
            {
                DataTable dtDept = new System.Data.DataTable();
                //string strQuery = "SELECT [SYSTEM_CODE],[SYSTEM_DESCRIPTION],[SYSTEM_PARTICULARS],[MAKER],[FUNCTIONS],[SET_INSTALED],[MODULE_TYPE] ,[PREFERENCE_SUPPLIER],[Dept1],[Dept2],[Dept3],[Dept4],[Dept5],[Dept6],[Dept7],[Dept8],[Dept9],[Dept10],[Dept11],[Dept12],[Dept13],[Dept14],[Dept15],[Vessel_Code] FROM PURC_LIB_SYSTEMS WHERE [Active_Status]=1";
                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_SP_Get_Catalouge");
                dtDept = ds.Tables[0];
                return dtDept;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }


        public DataTable SelectCatalogMaster()
        {
            try
            {
                DataTable dtDept = new System.Data.DataTable();
                //string strQuery = "SELECT [SYSTEM_CODE],[SYSTEM_DESCRIPTION],[SYSTEM_PARTICULARS],[MAKER],[FUNCTIONS],[SET_INSTALED],[MODULE_TYPE] ,[PREFERENCE_SUPPLIER],[Dept1],[Dept2],[Dept3],[Dept4],[Dept5],[Dept6],[Dept7],[Dept8],[Dept9],[Dept10],[Dept11],[Dept12],[Dept13],[Dept14],[Dept15],[Vessel_Code] FROM PURC_LIB_SYSTEMS WHERE [Active_Status]=1";
                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_SP_Get_Lib_Catalouge");
                dtDept = ds.Tables[0];
                return dtDept;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }


        public DataTable SelectFunctionType()
        {
            try
            {
                DataTable dtFuncType = new System.Data.DataTable();
                //string strQuery = "SELECT [SYSTEM_CODE],[SYSTEM_DESCRIPTION],[SYSTEM_PARTICULARS],[MAKER],[FUNCTIONS],[SET_INSTALED],[MODULE_TYPE] ,[PREFERENCE_SUPPLIER],[Dept1],[Dept2],[Dept3],[Dept4],[Dept5],[Dept6],[Dept7],[Dept8],[Dept9],[Dept10],[Dept11],[Dept12],[Dept13],[Dept14],[Dept15],[Vessel_Code] FROM PURC_LIB_SYSTEMS WHERE [Active_Status]=1";
                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_SP_Get_FunctionType");
                dtFuncType = ds.Tables[0];
                return dtFuncType;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable SelectCatalogByDept(string dept)
        {
            try
            {

                SqlParameter[] obj = new SqlParameter[]
                                        { 
                                             new SqlParameter("@Dept",  dept),
                                            //new SqlParameter("@Staff_Code",Staff_Code),
                                            //new SqlParameter("@Joining_Date",Joining_Date),
                                        };


                DataTable dtDept = new System.Data.DataTable();
                //string strQuery = "SELECT [SYSTEM_CODE],[SYSTEM_DESCRIPTION],[SYSTEM_PARTICULARS],[MAKER],[FUNCTIONS],[SET_INSTALED],[MODULE_TYPE] ,[PREFERENCE_SUPPLIER],[Dept1],[Dept2],[Dept3],[Dept4],[Dept5],[Dept6],[Dept7],[Dept8],[Dept9],[Dept10],[Dept11],[Dept12],[Dept13],[Dept14],[Dept15],[Vessel_Code] FROM PURC_LIB_SYSTEMS WHERE [Active_Status]=1";
                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_SP_Get_CatalougeByDept", obj);
                dtDept = ds.Tables[0];
                return dtDept;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public static DataTable Get_Catalogues(string Dept_Type, string Dept_Code)
        {
            SqlParameter[] prm = new SqlParameter[] { new SqlParameter("@Dept_Type", Dept_Type), new SqlParameter("@Dept_Code", Dept_Code) };

            return SqlHelper.ExecuteDataset(_Connection, CommandType.StoredProcedure, "PURC_GET_Catalogues", prm).Tables[0];

        }



    }
}