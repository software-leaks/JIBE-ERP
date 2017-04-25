using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;

namespace SMS.BuildService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IBuildService
    {
        [OperationContract]
        DataSet SyncBuildAssemblies();
        
        [OperationContract]
        DataTable GetDeltaLicenseRequest(int Vessel_Owner);

        [OperationContract]
        int UpdJiBeDeltaSyncFlag(int Vessel_ID);

        [OperationContract]
        int UpdJiBeDeltaAutoSyncFlag(int Vessel_ID, string SyncConn_String);

        [OperationContract]
        string GetNewDeltaLicenseKey(int Vessel_ID, string AutorizedKey);

        [OperationContract]
        DataTable GetDeltaDllAcknowledgement(int Vessel_ID);

        [OperationContract]
        Boolean CreateDeltaData(int Vessel_ID, DateTime SetupCreationDate);

        [OperationContract]
        Boolean PutVesselReleaseFileToJibe(string FileName, byte[] fileData);

        ///Description: Method used to Sync SQL to vessel after verification from JIT End,Vessel List and Query is sent from JIT
        [OperationContract]
        string SyncBuildQuery(DataTable vesselID, string strSQLQuery);

        // Get incremental SQL script log for a vessel pass the previous sql script version from JIT
        [OperationContract]
        DataTable GET_Delta_SQL_Script_Log(int Vessel_ID, int SqlVersion);
        
        ///Description: Method used to Get details of SQL which are executed at Vessel side.
        [OperationContract]
        DataTable GET_SQL_Script_Log();

        ///Description: Method used to Get details of DLL log.
        [OperationContract]
        DataTable GET_Delta_DLL_Log(int Vessel_ID, int LastID);
    }
}
