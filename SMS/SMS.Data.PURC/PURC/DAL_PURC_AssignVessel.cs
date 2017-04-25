using System;
using System.Configuration;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using SMS.Data;
using SMS.Properties;


/// <summary>
/// Summary description for DALAssignVessel
/// </summary>
/// 
namespace SMS.Data.PURC
{
    public class DAL_PURC_AssignVessel
    {


        private string _internalConnection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;

        public DAL_PURC_AssignVessel()
        {

        }


        //****** It's get vessel Location for Vessel Location Screen.
        public DataTable GetLocation()
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            try
            {
                System.Data.SqlClient.SqlParameter obj = new System.Data.SqlClient.SqlParameter("@StringQuery", SqlDbType.VarChar);
                obj.Value = "SELECT row_Number() over(order by Code) as SN,[Code] ,[Parent_Type],[Description] FROM [Lib_IID_System_Parameters] where Parent_Type ='1' and Active_Status =1";
                //obj.Value = "SELECT row_Number() over(order by Code) as SN,[Code] ,[Parent_Type],[Description] FROM [Lib_IID_System_Parameters]";
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

        public DataTable GetVesselName()
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            try
            {
                System.Data.SqlClient.SqlParameter obj = new System.Data.SqlClient.SqlParameter("@StringQuery", SqlDbType.VarChar);
                obj.Value = "SELECT row_Number() over(order by Vessel_Code)  as row_number,[Vessel_Code],Vessel_Name as Vessels, [Vessel_Short_Name] ,Tech_Manager as Fleet FROM Lib_Vessels where [Active_Status]=1 order by Vessel_Name ";

                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "[ExeQuery]", obj);
                dt = ds.Tables[0];
            }
            catch (Exception ex)
            {

                dt = null;
            }
            return dt;
        }

        public DataTable GetFleet()
        {

            System.Data.DataTable dt = new System.Data.DataTable();
            try
            {
                System.Data.SqlClient.SqlParameter obj = new System.Data.SqlClient.SqlParameter("@StringQuery", SqlDbType.VarChar);
                obj.Value = "SELECT distinct Tech_Manager as Fleet FROM Lib_Vessels where [Active_Status]=1 ";

                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "[ExeQuery]", obj);
                dt = ds.Tables[0];
            }
            catch (Exception ex)
            {

                dt = null;
            }
            return dt;
        }

        public DataTable assignCatalogs()
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.SqlClient.SqlParameter obj = new System.Data.SqlClient.SqlParameter("@StringQuery", SqlDbType.VarChar);
            //obj.Value = "select VL.[Vessel_Code],VL.[Location_Code],SL.system_code,SL.system_description,SL.system_particulars from PURC_LIB_SYSTEMS SL inner join PURC_DTL_VESSEL_LOCATIONS VL on SL.System_Code=VL.System_Code where VL.Active_Status=1 and SL.Active_Status=1 and SL.Parent_Type ='1'";
            obj.Value = "select VL.[Vessel_Code],VL.[Location_Code],SL.system_code,SL.system_description,SL.system_particulars from PURC_LIB_SYSTEMS SL inner join PURC_DTL_VESSEL_LOCATIONS VL on SL.System_Code=VL.System_Code where VL.Active_Status=1 and SL.Active_Status=1";
            try
            {
                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "[ExeQuery]", obj);
                dt = ds.Tables[0];
            }
            catch (Exception ex)
            {

                dt = null;
            }
            return dt;
        }
        public DataTable UnassignCatalogs()
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.SqlClient.SqlParameter obj = new System.Data.SqlClient.SqlParameter("@StringQuery", SqlDbType.VarChar);
            obj.Value = "select system_code,system_description,system_particulars from PURC_LIB_SYSTEMS where Active_Status=1 order by system_description";
            try
            {
                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "[ExeQuery]", obj);
                dt = ds.Tables[0];
            }
            catch (Exception ex)
            {

                dt = null;
            }
            return dt;
        }


        public int VLassignLocation(AssignVesselData objDOAssignVessel)
        {

            try
            {
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@Vessel_Code", objDOAssignVessel.Vessel_code),
                   new System.Data.SqlClient.SqlParameter("@Location_Code", objDOAssignVessel.LocationCode),
                   new System.Data.SqlClient.SqlParameter("@System_Code", objDOAssignVessel.Systemcode),
                   new System.Data.SqlClient.SqlParameter("@Location_Comments", objDOAssignVessel.LocationComments),
                   new System.Data.SqlClient.SqlParameter("@Created_By", objDOAssignVessel.CurrentUser)
            };
                return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "[PURC_SP_Ins_Vessel_Location]", obj);
            }
            catch (Exception ex)
            {
                throw ex;

            }


        }
        public int VLUnassignLocation(AssignVesselData objDOAssignVessel)
        {
            try
            {
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[]
            { 
                   new System.Data.SqlClient.SqlParameter("@Vessel_Code", objDOAssignVessel.Vessel_code),
                   new System.Data.SqlClient.SqlParameter("@Location_Code", objDOAssignVessel.LocationCode),
                   new System.Data.SqlClient.SqlParameter("@System_Code", objDOAssignVessel.Systemcode),
                   new System.Data.SqlClient.SqlParameter("@Location_Comments", objDOAssignVessel.LocationComments),
                   new System.Data.SqlClient.SqlParameter("@Created_By", objDOAssignVessel.CurrentUser)
            };
                return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "[PURC_SP_Ins_Vessel_Location]", obj);
            }
            catch (Exception ex)
            {
                throw ex;

            }

        }


        public int VDeleteLocation(string Vessel_code, string code)
        {

            System.Data.SqlClient.SqlParameter obj = new System.Data.SqlClient.SqlParameter("@StringQuery", SqlDbType.VarChar);
            obj.Value = "Update  PURC_DTL_VLOCATIONS set Active_Status=0 where vessel_code='" + Vessel_code + "' and Location_code ='" + code + "'  update PURC_DTL_VESSEL_LOCATIONS set Active_Status=0 where vessel_code='" + Vessel_code + "' and Location_code ='" + code + "'";
            try
            {
                return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "[ExeQuery]", obj);

            }
            catch (Exception ex)
            {
                throw ex;

            }

        }
        public int VDeletecatelog(string Vessel_code, string code, string System_Code)
        {

            System.Data.SqlClient.SqlParameter obj = new System.Data.SqlClient.SqlParameter("@StringQuery", SqlDbType.VarChar);
            obj.Value = "update PURC_DTL_VESSEL_LOCATIONS set Active_Status=0 where vessel_code='" + Vessel_code + "' and Location_code ='" + code + "' and System_Code='" + System_Code + "'";
            try
            {
                return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "[ExeQuery]", obj);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }


        public DataTable VassignCatalogs(string Vessel_code)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.SqlClient.SqlParameter obj = new System.Data.SqlClient.SqlParameter("@StringQuery", SqlDbType.VarChar);
            obj.Value = "select VL.[Vessel_Code],VL.[Location_Code],SL.system_code,SL.system_description,SL.system_particulars from PURC_LIB_SYSTEMS SL inner join PURC_DTL_VESSEL_LOCATIONS VL on SL.System_Code=VL.System_Code where VL.[Vessel_Code]='" + Vessel_code + "' and VL.Active_Status=1 and SL.Active_Status=1 and ";
            try
            {
                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "[ExeQuery]", obj);
                dt = ds.Tables[0];
            }
            catch (Exception ex)
            {

                dt = null;
            }
            return dt;
        }
        public DataTable VUnassignCatalogs(string Vessel_code)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.SqlClient.SqlParameter obj = new System.Data.SqlClient.SqlParameter("@StringQuery", SqlDbType.VarChar);
            obj.Value = "select system_code,system_description,system_particulars from PURC_LIB_SYSTEMS where system_code not in (select system_code from PURC_DTL_VESSEL_LOCATIONS where vessel_code='" + Vessel_code + "' and Active_Status=1) and Active_Status=1";
            try
            {
                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "[ExeQuery]", obj);
                dt = ds.Tables[0];
            }
            catch (Exception ex)
            {

                dt = null;
            }
            return dt;
        }


        public DataTable VLassignCatalogs(string Vessel_code, string code)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.SqlClient.SqlParameter obj = new System.Data.SqlClient.SqlParameter("@StringQuery", SqlDbType.VarChar);
            obj.Value = "select VL.[Vessel_Code],VL.[Location_Code],SL.system_code,SL.system_description,SL.system_particulars from PURC_LIB_SYSTEMS SL inner join PURC_DTL_VESSEL_LOCATIONS VL on SL.System_Code=VL.System_Code where VL.[Vessel_Code]='" + Vessel_code + "' and Location_code ='" + code + "' and SL.Active_Status=1 and VL.Active_Status=1 ";
            try
            {
                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "[ExeQuery]", obj);
                dt = ds.Tables[0];
            }
            catch (Exception ex)
            {

                dt = null;
            }
            return dt;
        }
        public DataTable VLUnassignCatalogs(string Vessel_code, string code)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.SqlClient.SqlParameter obj = new System.Data.SqlClient.SqlParameter("@StringQuery", SqlDbType.VarChar);
            obj.Value = "select system_code,system_description,system_particulars from PURC_LIB_SYSTEMS where system_code not in (select system_code from PURC_DTL_VESSEL_LOCATIONS where vessel_code='" + Vessel_code + "' and Location_code ='" + code + "' and Active_Status=1) and Active_Status=1";
            try
            {
                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "[ExeQuery]", obj);
                dt = ds.Tables[0];
            }
            catch (Exception ex)
            {

                dt = null;
            }
            return dt;
        }

        public DataTable assignLocation()
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            try
            {
                System.Data.SqlClient.SqlParameter obj = new System.Data.SqlClient.SqlParameter("@StringQuery", SqlDbType.VarChar);
                //obj.Value = "SELECT distinct row_Number() over(order by Code) as SN,[Code],PURC_DTL_VESSEL_LOCATIONS.vessel_code,[Parent_Type],[Description] FROM [PURC_LIB_SYSTEM_PARAMETERS]  inner join PURC_DTL_VESSEL_LOCATIONS on [PURC_LIB_SYSTEM_PARAMETERS].[Code]=PURC_DTL_VESSEL_LOCATIONS.Location_code where  PURC_DTL_VESSEL_LOCATIONS.Active_Status=1 and PURC_LIB_SYSTEM_PARAMETERS.Active_Status=1 and PURC_LIB_SYSTEM_PARAMETERS.Parent_Type='1' group by [Code],PURC_DTL_VESSEL_LOCATIONS.vessel_code,[Parent_Type],[Description]";
                obj.Value = "select Loc.Location_Code,Loc.Short_Code,Loc.Description,VLoc.Vessel_Code from PURC_LIB_LOCATIONS Loc inner join PURC_DTL_VLOCATIONS VLoc on VLoc.Location_Code=Loc.Location_Code where VLoc.Active_Status=1 ";
                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "[ExeQuery]", obj);
                dt = ds.Tables[0];
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;

            }

        }
        public DataTable UnassignLocation()
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            try
            {
                System.Data.SqlClient.SqlParameter obj = new System.Data.SqlClient.SqlParameter("@StringQuery", SqlDbType.VarChar);
                // obj.Value = "SELECT row_Number() over(order by Code) as SN,[Code] ,[Parent_Type],[Description] FROM [PURC_LIB_SYSTEM_PARAMETERS] where [Code] not in (select Location_code from  PURC_DTL_VESSEL_LOCATIONS where Active_Status=1) and Active_Status=1 and Parent_Type='1' order by Description";
                obj.Value = "select Location_Code,Short_Code,Description from PURC_LIB_LOCATIONS";
                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "[ExeQuery]", obj);
                return dt = ds.Tables[0];

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public DataTable VassignLocation(string vessel_code)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            try
            {
                System.Data.SqlClient.SqlParameter obj = new System.Data.SqlClient.SqlParameter("@StringQuery", SqlDbType.VarChar);
                // obj.Value = "SELECT row_Number() over(order by Code) as SN,[Code],PURC_DTL_VESSEL_LOCATIONS.vessel_code,[Parent_Type],[Description] FROM [PURC_LIB_SYSTEM_PARAMETERS]  inner join PURC_DTL_VESSEL_LOCATIONS on [PURC_LIB_SYSTEM_PARAMETERS].[Code]=PURC_DTL_VESSEL_LOCATIONS.Location_code where PURC_DTL_VESSEL_LOCATIONS.Vessel_code='" + vessel_code + "' and PURC_DTL_VESSEL_LOCATIONS.Active_Status=1 and PURC_LIB_SYSTEM_PARAMETERS.Active_Status=1 and PURC_LIB_SYSTEM_PARAMETERS.Parent_Type='1' group by [Code],PURC_DTL_VESSEL_LOCATIONS.vessel_code,[Parent_Type],[Description]";
                obj.Value = "select Loc.Location_Code,Loc.Short_Code,Loc.Description,VLoc.Vessel_Code from PURC_LIB_LOCATIONS Loc inner join PURC_DTL_VLOCATIONS VLoc on VLoc.Location_Code=Loc.Location_Code where VLoc.Active_Status=1 and VLoc.Vessel_Code='" + vessel_code + "'";

                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "[ExeQuery]", obj);
                dt = ds.Tables[0];
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;

            }


        }
        public DataTable VUnassignLocation(string Vessel_code)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            try
            {
                System.Data.SqlClient.SqlParameter obj = new System.Data.SqlClient.SqlParameter("@StringQuery", SqlDbType.VarChar);
                // obj.Value = "SELECT row_Number() over(order by Code) as SN,[Code] ,[Parent_Type],[Description] FROM [PURC_LIB_SYSTEM_PARAMETERS] where [Code] not in (select Location_code from  PURC_DTL_VESSEL_LOCATIONS where vessel_code='" + Vessel_code + "' and Active_Status=1) and Active_Status=1 and Parent_Type='1'";
                obj.Value = "select Location_Code,Short_Code,Description from PURC_LIB_LOCATIONS where Location_Code not in (select Location_code from  PURC_DTL_VESSEL_LOCATIONS where vessel_code='" + Vessel_code + "' and Active_Status=1)";
                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "[ExeQuery]", obj);
                return dt = ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }



    }
}