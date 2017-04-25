using System;
using System.Configuration;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using SMS.Data;
using SMS.Properties;

/// <summary>
/// Summary description for DALLocation
/// </summary>
namespace SMS.Data.PURC
{
    public class DAL_PURC_Location
    {

        private string _internalConnection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;


        public DAL_PURC_Location()
        {
        }

        public DataTable Location_Search(string searchtext, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   
                   new System.Data.SqlClient.SqlParameter("@SearchText", searchtext),

                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                   
                    
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_SP_Location_Search", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds.Tables[0];
        }


        public int SaveLocation(LocationData objDOLocation)
        {

            try
            {
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] { 
                   new System.Data.SqlClient.SqlParameter("@Parent_Type", objDOLocation.ParentType),
                   new System.Data.SqlClient.SqlParameter("@ShortCode", objDOLocation.ShortCode),
                   new System.Data.SqlClient.SqlParameter("@Short_Description", objDOLocation.ShortDiscription),
                   new System.Data.SqlClient.SqlParameter("@Long_Discription", objDOLocation.LongDiscription),
                   new System.Data.SqlClient.SqlParameter("@created_By", objDOLocation.CurrentUser),
                   new System.Data.SqlClient.SqlParameter("@NoOfLoc", objDOLocation.NoOfLoc),
                    new System.Data.SqlClient.SqlParameter("@VesselType", objDOLocation.VesselType),
              };
                //obj[0].Value = ParentType;
                //obj[1].Value = Discription;
                //obj[2].Value = User;
                // System.Data.SqlClient.SqlParameter obj = new System.Data.SqlClient.SqlParameter("@StringQuery", SqlDbType.VarChar);
                return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_SP_Ins_Lib_System_Parameters", obj);
            }
            catch (Exception ex)
            {
                throw ex;

            }


        }
        public int SaveEditLocation(LocationData objDOLocation)
        {

            try
            {
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] { 
                   new System.Data.SqlClient.SqlParameter("@LocationID", objDOLocation.LocationID),
                   new System.Data.SqlClient.SqlParameter("@Short_Description", objDOLocation.ShortDiscription),
                    new System.Data.SqlClient.SqlParameter("@Modified_By", objDOLocation.CurrentUser),
                
              };
                //obj[0].Value = ParentType;
                //obj[1].Value = Discription;
                //obj[2].Value = User;
                // System.Data.SqlClient.SqlParameter obj = new System.Data.SqlClient.SqlParameter("@StringQuery", SqlDbType.VarChar);
                return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_Update_VesselLocation", obj);
            }
            catch (Exception ex)
            {
                throw ex;

            }


        }
        public int PMS_Insert_AssignLocation(string ShortCode,string ShortDesc,string SystemCode,string SubSysCode,int VesselID,string CatCode,int CreatedBy)
        {

            try
            {
                SqlParameter[] obj = new SqlParameter[] { 
                   new SqlParameter("@ShortCode", ShortCode),
                   new SqlParameter("@ShortDescription", ShortDesc),
                   new SqlParameter("@SystemCode", SystemCode),
                   new SqlParameter("@SubSystemCode", SubSysCode),/* Here Sub System ID is passed not code */
                   new SqlParameter("@VesselID", VesselID),
                   new SqlParameter("@CategoryCode", CatCode),
                    new SqlParameter("@CreatedBy", CreatedBy),
              };
                //obj[0].Value = ParentType;
                //obj[1].Value = Discription;
                //obj[2].Value = User;
                // System.Data.SqlClient.SqlParameter obj = new System.Data.SqlClient.SqlParameter("@StringQuery", SqlDbType.VarChar);
                return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PMS_Insert_AssignLocation", obj);
            }
            catch (Exception ex)
            {
                throw ex;

            }


        }


        public int DeleteLocation(LocationData objDOLocation)
        {

            try
            {

                System.Data.SqlClient.SqlParameter[] sqlpram = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@Code", objDOLocation.Code),
                   new System.Data.SqlClient.SqlParameter("@Deleted_By", objDOLocation.CurrentUser)
            };

                //obj[0].Value = DeptID;
                //obj[1].Value = UserId;

                // System.Data.SqlClient.SqlParameter obj = new System.Data.SqlClient.SqlParameter("@StringQuery", objDOCatalog);
                SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "[PURC_SP_del_System_Parameters]", sqlpram);
                return 0;
            }
            catch (Exception ex)
            {

                throw ex;

            }


        }


        public int EditLocation(LocationData objDoLocation)
        {

            try
            {
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] { 
                  new System.Data.SqlClient.SqlParameter("@Code", objDoLocation.Code),
                   new System.Data.SqlClient.SqlParameter("@Parent_Type", objDoLocation.ParentType),
                   new System.Data.SqlClient.SqlParameter("@Short_Code", objDoLocation.ShortCode),
                   new System.Data.SqlClient.SqlParameter("@ShortDescription", objDoLocation.ShortDiscription),
                   new System.Data.SqlClient.SqlParameter("@LongDescription", objDoLocation.LongDiscription),
                   new System.Data.SqlClient.SqlParameter("@Updateted_By", objDoLocation.CurrentUser),
                    new System.Data.SqlClient.SqlParameter("@VesselType", objDoLocation.VesselType),
              };
                //obj[0].Value = Code;
                //obj[1].Value = ParentType;
                //obj[2].Value = Discription;
                //obj[3].Value = User;
                // System.Data.SqlClient.SqlParameter obj = new System.Data.SqlClient.SqlParameter("@StringQuery", SqlDbType.VarChar);
                return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "[PURC_SP_Edit_Lib_System_Parameters]", obj);
            }
            catch (Exception ex)
            {
                throw ex;

            }

        }

        //**** Select Location for Master Screen Location 
        public DataTable SelectLocation()
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            try
            {
                System.Data.SqlClient.SqlParameter obj = new System.Data.SqlClient.SqlParameter("@StringQuery", SqlDbType.VarChar);

                obj.Value = "select * from PURC_LIB_SYSTEM_PARAMETERS where Parent_Type='1'  and Active_Status=1 ";
                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "[ExeQuery]", obj);
                dt = ds.Tables[0];
                return dt;
            }
            catch (Exception ex)
            {

                dt = null;
                throw ex;
            }


        }

        public int CheckchildCount(string Pcode)
        {
            try
            {
                SqlParameter obj = new SqlParameter();
                obj.Value = "Select count(*) from PURC_LIB_SYSTEM_PARAMETERS where Parent_Type ='" + Pcode + "' and Active_Status ='1'";
                return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "[ExeQuery]", obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable BindLocationCombo()
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            try
            {
                System.Data.SqlClient.SqlParameter obj = new System.Data.SqlClient.SqlParameter("@StringQuery", SqlDbType.VarChar);
                obj.Value = "select distinct code,Short_Code,Description from dbo.PURC_LIB_SYSTEM_PARAMETERS where Active_Status=1 and Parent_Type =0";
                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "[ExeQuery]", obj);
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