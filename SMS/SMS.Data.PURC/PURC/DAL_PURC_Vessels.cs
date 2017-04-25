using System;
using System.Configuration;
 
 
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
 
using   SMS.Data;
using SMS.Properties;

/// <summary>
/// Summary description for DALVessels
/// </summary>
namespace SMS.Data.PURC
{
    public class DAL_PURC_Vessels
    {
        private string _internalConnection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;


        public DAL_PURC_Vessels()
        {
        }

        

        public DataSet SelectFleet()
        {

            try
            {
                // System.Data.DataTable dtVessels = new System.Data.DataTable();
                System.Data.SqlClient.SqlParameter obj = new System.Data.SqlClient.SqlParameter("@StringQuery", SqlDbType.VarChar);

                obj.Value = "select Name,code from dbo.Lib_VesselLib_Systems_Parameters where Parent_Code = 1";
                //obj.Value = "SELECT distinct Tech_Manager as Fleet FROM Lib_Vessels where [Active_Status]=1 ";
                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "[ExeQuery]", obj);
                // dtVessels = ds.Tables[0];
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public string SelectFleetByUser(string user)
        {
            try
            {
                // System.Data.DataTable dtVessels = new System.Data.DataTable();
                // System.Data.SqlClient.SqlParameter obj = new System.Data.SqlClient.SqlParameter("@StringQuery", SqlDbType.VarChar);

                //obj.Value = "select Name,code from dbo.Lib_VesselLib_Systems_Parameters where Parent_Code = 1";
                //obj.Value = "SELECT distinct Tech_Manager as Fleet FROM Lib_Vessels where [Active_Status]=1 ";
                string Fleet = SqlHelper.ExecuteScalar(_internalConnection, CommandType.Text, "select tech_manager from lib_user where userid='" + user + "'").ToString();
                // dtVessels = ds.Tables[0];
                return Fleet;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable SelectVessels()
        {

            try
            {
                System.Data.DataTable dtFleet = new System.Data.DataTable();
                System.Data.SqlClient.SqlParameter obj = new System.Data.SqlClient.SqlParameter("@StringQuery", SqlDbType.VarChar);
                obj.Value = "SELECT row_Number() over(order by Vessel_id)  as row_number,vessel_id as [Vessel_Code],Vessel_Name as Vessels, [Vessel_Short_Name] ,Tech_Manager,fleetcode  FROM Lib_Vessels where [Active_Status]=1 order by Vessel_Name";

                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "[ExeQuery]", obj);
                dtFleet = ds.Tables[0];
                return dtFleet;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public string Get_LegalTerm_DL(int LegalType)
        {
            return SqlHelper.ExecuteScalar(_internalConnection, CommandType.StoredProcedure, "PURC_GET_LegalTerms", new SqlParameter("@LegalType", LegalType)).ToString();
        }


    }
}