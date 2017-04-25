using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;
using SMS.Business.BDS;

namespace SMS.BuildService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class BuildService : IBuildService
    {
        public DataSet SyncBuildAssemblies()
        {
            BLL_Build_Service objVessel = new BLL_Build_Service();
            return objVessel.Get_JiBeBuildAssemblies();
        }
        
        ///Description: Method used to Sync SQL to vessel after verification from JIT End,Vessel List and Query is sent from JIT
        public string SyncBuildQuery(DataTable vesselID, string strSQLQuery)
        {
            BLL_Build_Service objVessel = new BLL_Build_Service();
            //Select Single Vessel ID and pass to it in loop
            DataRow[] drs = vesselID.Select("Vessel_ID is not null");
            foreach (DataRow row in drs)
            {
                objVessel.SYNC_SQlQueryToVessels("", "", "", Convert.ToInt32(row["Vessel_ID"]), strSQLQuery, "", "");
            }
            return "true";
        }
        #region Delta DLL

        ///Description: Method used to Get details of SQL which are executed at Vessel side.
        public DataTable GET_SQL_Script_Log()
        {
            BLL_Build_Service objVessel = new BLL_Build_Service();
            return objVessel.GET_SQL_Script_Log();
        }
        // Get incremental SQL script log for a vessel pass the previous sql script version from JIT
        public DataTable GET_Delta_SQL_Script_Log(int Vessel_ID, int SqlVersion)
        {
            BLL_Build_Service objVessel = new BLL_Build_Service();
            return objVessel.GET_Delta_SQL_Script_Log(Vessel_ID,SqlVersion);
        }
        ///Description: Method used to Get details of SQL which are executed at Vessel side.
        public DataTable GET_Delta_DLL_Log(int Vessel_ID, int LastID)
        {
            BLL_Build_Service objVessel = new BLL_Build_Service();
            return objVessel.GET_Delta_DLL_Log(Vessel_ID,LastID);
        }
               
        // Get license request details from Jibe Main DB
        public DataTable GetDeltaLicenseRequest(int Vessel_Owner)
        {
            BLL_Build_Service objVessel = new BLL_Build_Service();
            return objVessel.Get_JiBeDeltaLicenseKeyRequest(Vessel_Owner);
        }

        // Update JIBESync DB Installation flag to 1 for sending/reciveing data to or from vessel
        public int UpdJiBeDeltaSyncFlag(int Vessel_ID)
        {
            BLL_Build_Service objVessel = new BLL_Build_Service();
            return objVessel.Upd_JiBeDeltaSyncFlag(Vessel_ID);
        }

        // update jibesync DB LIB_VESSELS's Autosync flag to 1 
        public int UpdJiBeDeltaAutoSyncFlag(int Vessel_ID, string SyncConn_String)
        {
            BLL_Build_Service objVessel = new BLL_Build_Service();
            return objVessel.Upd_JiBeDeltaAutoSyncFlag(Vessel_ID, SyncConn_String);
        }
        /// <summary>
        /// get the execution log and store in CRM DB
        /// </summary>
        /// <param name="Vessel_ID"></param>
        /// <returns></returns>
        public DataTable GetDeltaDllAcknowledgement(int Vessel_ID)
        {
            BLL_Build_Service objVessel = new BLL_Build_Service();
            return objVessel.Get_JibeDeltaDllAcknowledgement(Vessel_ID);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Vessel_ID"></param>
        /// <param name="SetupCreationDate"></param>
        /// <returns></returns>
        public Boolean CreateDeltaData(int Vessel_ID, DateTime SetupCreationDate)
        {
            BLL_Build_Service objVessel = new BLL_Build_Service();
            return objVessel.Upd_JibeDeltaData(Vessel_ID, SetupCreationDate); ;
        }

        /// <summary>
        /// save file byte data at passed location
        /// </summary>
        /// <param name="FileName"> path to save the file </param>
        /// <param name="fileData"> byte data of a file</param>
        /// <returns>flag</returns>
        public Boolean PutVesselReleaseFileToJibe(string FileName,byte[] fileData)
        {
            BLL_Build_Service objVessel = new BLL_Build_Service();
            return objVessel.Put_VesselReleaseFileToJibe(FileName,fileData);
        }
        /// <summary>
        /// Generate the License key based on autorizedkey 
        // Insert record in DATA_Log table and send generated licensekey to calling app
        /// </summary>
        /// <param name="Vessel_ID"></param>
        /// <param name="AutorizedKey"> autroized key</param>
        /// <returns>license key </returns>
        public string GetNewDeltaLicenseKey(int Vessel_ID, string AutorizedKey)
        {
            BLL_Build_Service objVessel = new BLL_Build_Service();
            return objVessel.Upd_JiBeDeltaLicenseKey(Vessel_ID, AutorizedKey);
        }  
        #endregion
    }
}
