using System;
using System.Configuration;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using SMS.Data;
using SMS.Properties;


namespace SMS.Data.PURC
{
    public class DAL_PURC_LibraryCatalogue
    {
        private string _internalConnection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;

        public DAL_PURC_LibraryCatalogue()
        {
        }



        public DataSet LibraryCatalogueList(string systemid)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
           { 
                   new System.Data.SqlClient.SqlParameter("@SYSTEMID", systemid),
           };

            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_PR_SYSTEMLIST", obj);
        }



        public DataSet LibraryCatalogueSearch(string systemcode, string systemdesc, string deptType, string deptcode, int vesselcode, string makercode
            , int? IsActive, string sortby, int? sortdirection, int pagenumber, int pagesize, int isfetchcount)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@SYSTEMCODE", systemcode),
                   new System.Data.SqlClient.SqlParameter("@SYSTEMDESC",systemdesc),
                   new System.Data.SqlClient.SqlParameter("@DEPTTYPE",deptType),
                   new System.Data.SqlClient.SqlParameter("@DEPTCODE", deptcode),
                   new System.Data.SqlClient.SqlParameter("@VESSELCODE",vesselcode),
                   new System.Data.SqlClient.SqlParameter("@MAKERCODE", makercode),
                   new System.Data.SqlClient.SqlParameter("@ISACTIVE",IsActive), 
                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                   new System.Data.SqlClient.SqlParameter("@ROWCOUNT",SqlDbType.Int)  
            };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_PR_CATALOGUESEARCH", obj);

        }


        public DataSet LibraryCatalogueSearch(string systemcode, string systemdesc, string deptType, string deptcode, int Fleetid, int vesselcode, string makercode
             , int? IsActive, string sortby, int? sortdirection, int pagenumber, int pagesize, int isfetchcount)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@SYSTEMCODE", systemcode),
                   new System.Data.SqlClient.SqlParameter("@SYSTEMDESC",systemdesc),
                   new System.Data.SqlClient.SqlParameter("@DEPTTYPE",deptType),
                   new System.Data.SqlClient.SqlParameter("@DEPTCODE", deptcode),
                   new System.Data.SqlClient.SqlParameter("@FLEETID",Fleetid),
                   new System.Data.SqlClient.SqlParameter("@VESSELCODE",vesselcode),
                   new System.Data.SqlClient.SqlParameter("@MAKERCODE", makercode),
                   new System.Data.SqlClient.SqlParameter("@ISACTIVE",IsActive), 
                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                   new System.Data.SqlClient.SqlParameter("@ROWCOUNT",SqlDbType.Int)  
            };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_PR_CATALOGUESEARCH", obj);

        }


        public DataSet LibraryCatalogueSearch_PMS(string systemcode, string systemdesc, string deptType, int? Function_ID, int Fleetid, int vesselcode, string makercode
              , int? IsActive, string sortby, int? sortdirection, int pagenumber, int pagesize, int isfetchcount)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@SYSTEMCODE", systemcode),
                   new System.Data.SqlClient.SqlParameter("@SYSTEMDESC",systemdesc),
                   new System.Data.SqlClient.SqlParameter("@DEPTTYPE",deptType),
                   new System.Data.SqlClient.SqlParameter("@FUNCTION_ID", Function_ID),
                   new System.Data.SqlClient.SqlParameter("@FLEETID",Fleetid),
                   new System.Data.SqlClient.SqlParameter("@VESSELCODE",vesselcode),
                   new System.Data.SqlClient.SqlParameter("@MAKERCODE", makercode),
                   new System.Data.SqlClient.SqlParameter("@ISACTIVE",IsActive), 
                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                   new System.Data.SqlClient.SqlParameter("@ROWCOUNT",SqlDbType.Int)  
            };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_PR_CATALOGUESEARCH_PMS", obj);

        }


        public int LibraryCatalogueSave(int userid, string systemcode, string systemdesc, string stystemparticular, string maker, string setinstalled, string model
            , string dept, string vesselid, string functionid, string accountcode)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@USERID", userid ),
                   new System.Data.SqlClient.SqlParameter("@SYSTEM_CODE", systemcode ),
                   new System.Data.SqlClient.SqlParameter("@SYSTEM_DESCRIPTION", systemdesc),
                   new System.Data.SqlClient.SqlParameter("@SYSTEM_PARTICULARS", stystemparticular),
                   new System.Data.SqlClient.SqlParameter("@MAKER", maker),
                   new System.Data.SqlClient.SqlParameter("@SET_INSTALED",setinstalled),
                   new System.Data.SqlClient.SqlParameter("@MODEL",model),
                   new System.Data.SqlClient.SqlParameter("@DEPT1", dept ),
                   new System.Data.SqlClient.SqlParameter("@VESSEL_ID",vesselid),
                   new System.Data.SqlClient.SqlParameter("@FUNCTIONID",functionid),
                   new System.Data.SqlClient.SqlParameter("@ACCOUNTCODE",accountcode),
                   new System.Data.SqlClient.SqlParameter("@Return", SqlDbType.Int),

              
            };

            obj[obj.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_PR_SYSTEMINSERT", obj);
            return Convert.ToInt32(obj[obj.Length - 1].Value.ToString());
        }


        public int LibraryCatalogueSave(int userid, string systemcode, string systemdesc, string stystemparticular, string maker, string setinstalled, string model
         , string dept, string vesselid, string functionid, string accountcode, int? AddSubSysFlag, string SerialNumber, int IsLocationRequired, int Run_Hour, int Critical)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@USERID", userid ),
                   new System.Data.SqlClient.SqlParameter("@SYSTEM_CODE", systemcode ),
                   new System.Data.SqlClient.SqlParameter("@SYSTEM_DESCRIPTION", systemdesc),
                   new System.Data.SqlClient.SqlParameter("@SYSTEM_PARTICULARS", stystemparticular),
                   new System.Data.SqlClient.SqlParameter("@MAKER", maker),
                   new System.Data.SqlClient.SqlParameter("@SET_INSTALED",setinstalled),
                   new System.Data.SqlClient.SqlParameter("@MODEL",model),
                   new System.Data.SqlClient.SqlParameter("@DEPT1", dept ),
                   new System.Data.SqlClient.SqlParameter("@VESSEL_ID",vesselid),
                   new System.Data.SqlClient.SqlParameter("@FUNCTIONID",functionid),
                   new System.Data.SqlClient.SqlParameter("@ACCOUNTCODE",accountcode),
                   new System.Data.SqlClient.SqlParameter("@ADDSUBSYSFLAG",AddSubSysFlag),
                   new System.Data.SqlClient.SqlParameter("@SERIALNUMBER",SerialNumber),
                      new System.Data.SqlClient.SqlParameter("@IsLocationRequired",IsLocationRequired),
                       new System.Data.SqlClient.SqlParameter("@Run_Hour",Run_Hour),
                       new System.Data.SqlClient.SqlParameter("@Critical",Critical),
                   new System.Data.SqlClient.SqlParameter("@Return", SqlDbType.Int),


              
            };

            obj[obj.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_PR_SYSTEMINSERT", obj);
            return Convert.ToInt32(obj[obj.Length - 1].Value.ToString());
        }


        public int PMS_Insert_NewSystem(int userid, string systemcode, string systemdesc, string stystemparticular, string maker, string setinstalled, string model
       , string dept, string vesselid, string functionid, string accountcode, int? AddSubSysFlag, string SerialNumber, int IsLocationRequired, int Run_Hour, int Critical, string DeptType)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@USERID", userid ),
                   new System.Data.SqlClient.SqlParameter("@SYSTEM_CODE", systemcode ),
                   new System.Data.SqlClient.SqlParameter("@SYSTEM_DESCRIPTION", systemdesc),
                   new System.Data.SqlClient.SqlParameter("@SYSTEM_PARTICULARS", stystemparticular),
                   new System.Data.SqlClient.SqlParameter("@MAKER", maker),
                   new System.Data.SqlClient.SqlParameter("@SET_INSTALED",setinstalled),
                   new System.Data.SqlClient.SqlParameter("@MODEL",model),
                   new System.Data.SqlClient.SqlParameter("@DEPT1", dept ),
                   new System.Data.SqlClient.SqlParameter("@VESSEL_ID",vesselid),
                   new System.Data.SqlClient.SqlParameter("@FUNCTIONID",functionid),
                   new System.Data.SqlClient.SqlParameter("@ACCOUNTCODE",accountcode),
                   new System.Data.SqlClient.SqlParameter("@ADDSUBSYSFLAG",AddSubSysFlag),
                   new System.Data.SqlClient.SqlParameter("@SERIALNUMBER",SerialNumber),
                   new System.Data.SqlClient.SqlParameter("@IsLocationRequired",IsLocationRequired),
                   new System.Data.SqlClient.SqlParameter("@Run_Hour",Run_Hour),
                   new System.Data.SqlClient.SqlParameter("@Critical",Critical),
                   new System.Data.SqlClient.SqlParameter("@DeptType",DeptType),

                   new System.Data.SqlClient.SqlParameter("@Return", SqlDbType.Int),



               
            };

            obj[obj.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PMS_Insert_NewSystem", obj);
            return Convert.ToInt32(obj[obj.Length - 1].Value.ToString());
        }

        // Added By Prashant ("ServiceAccount" added)
        public int PMS_Insert_NewSystem(int userid, string systemcode, string systemdesc, string stystemparticular, string maker, string setinstalled, string model
       , string dept, string vesselid, string functionid, string accountcode, int? AddSubSysFlag, string SerialNumber, int IsLocationRequired, int Run_Hour, int Critical, string DeptType, string ServiceAccount)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@USERID", userid ),
                   new System.Data.SqlClient.SqlParameter("@SYSTEM_CODE", systemcode ),
                   new System.Data.SqlClient.SqlParameter("@SYSTEM_DESCRIPTION", systemdesc),
                   new System.Data.SqlClient.SqlParameter("@SYSTEM_PARTICULARS", stystemparticular),
                   new System.Data.SqlClient.SqlParameter("@MAKER", maker),
                   new System.Data.SqlClient.SqlParameter("@SET_INSTALED",setinstalled),
                   new System.Data.SqlClient.SqlParameter("@MODEL",model),
                   new System.Data.SqlClient.SqlParameter("@DEPT1", dept ),
                   new System.Data.SqlClient.SqlParameter("@VESSEL_ID",vesselid),
                   new System.Data.SqlClient.SqlParameter("@FUNCTIONID",functionid),
                   new System.Data.SqlClient.SqlParameter("@ACCOUNTCODE",accountcode),
                   new System.Data.SqlClient.SqlParameter("@ADDSUBSYSFLAG",AddSubSysFlag),
                   new System.Data.SqlClient.SqlParameter("@SERIALNUMBER",SerialNumber),
                   new System.Data.SqlClient.SqlParameter("@IsLocationRequired",IsLocationRequired),
                   new System.Data.SqlClient.SqlParameter("@Run_Hour",Run_Hour),
                   new System.Data.SqlClient.SqlParameter("@Critical",Critical),
                   new System.Data.SqlClient.SqlParameter("@DeptType",DeptType),
                   new System.Data.SqlClient.SqlParameter("@ServiceAccount",ServiceAccount),
                   new System.Data.SqlClient.SqlParameter("@Return", SqlDbType.Int),



               
            };

            obj[obj.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PMS_Insert_NewSystems", obj);
            return Convert.ToInt32(obj[obj.Length - 1].Value.ToString());
        }

        public int PMS_Update_System(int userid, int systemid, string systemcode, string systemdesc, string stystemparticular, string maker, string setinstalled, string model
      , string dept, string vesselid, string functionid, string accountcode, string SerialNumber, int Run_Hour, int Critical)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@USERID", userid ),
                   new System.Data.SqlClient.SqlParameter("@SYSTEMID", systemid ),
                   new System.Data.SqlClient.SqlParameter("@SYSTEM_CODE", systemcode ),
                   new System.Data.SqlClient.SqlParameter("@SYSTEM_DESCRIPTION", systemdesc),
                   new System.Data.SqlClient.SqlParameter("@SYSTEM_PARTICULARS", stystemparticular),
                   new System.Data.SqlClient.SqlParameter("@MAKER", maker),
                   new System.Data.SqlClient.SqlParameter("@SET_INSTALED",setinstalled),
                   new System.Data.SqlClient.SqlParameter("@MODEL",model),
                   new System.Data.SqlClient.SqlParameter("@DEPT1", dept ),
                   new System.Data.SqlClient.SqlParameter("@VESSEL_ID",vesselid),
                   new System.Data.SqlClient.SqlParameter("@FUNCTIONID",functionid),
                   new System.Data.SqlClient.SqlParameter("@ACCOUNTCODE",accountcode),
                   new System.Data.SqlClient.SqlParameter("@SERIALNUMBER",SerialNumber), 
                   new System.Data.SqlClient.SqlParameter("@Run_Hour",Run_Hour),
                   new System.Data.SqlClient.SqlParameter("@Critical",Critical),

            };
            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PMS_Update_System", obj);
        }

        //Added by prashant ("ServiceAccount" added)
        public int PMS_Update_System(int userid, int systemid, string systemcode, string systemdesc, string stystemparticular, string maker, string setinstalled, string model
    , string dept, string vesselid, string functionid, string accountcode, string SerialNumber, int Run_Hour, int Critical, string ServiceAccount)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@USERID", userid ),
                   new System.Data.SqlClient.SqlParameter("@SYSTEMID", systemid ),
                   new System.Data.SqlClient.SqlParameter("@SYSTEM_CODE", systemcode ),
                   new System.Data.SqlClient.SqlParameter("@SYSTEM_DESCRIPTION", systemdesc),
                   new System.Data.SqlClient.SqlParameter("@SYSTEM_PARTICULARS", stystemparticular),
                   new System.Data.SqlClient.SqlParameter("@MAKER", maker),
                   new System.Data.SqlClient.SqlParameter("@SET_INSTALED",setinstalled),
                   new System.Data.SqlClient.SqlParameter("@MODEL",model),
                   new System.Data.SqlClient.SqlParameter("@DEPT1", dept ),
                   new System.Data.SqlClient.SqlParameter("@VESSEL_ID",vesselid),
                   new System.Data.SqlClient.SqlParameter("@FUNCTIONID",functionid),
                   new System.Data.SqlClient.SqlParameter("@ACCOUNTCODE",accountcode),
                   new System.Data.SqlClient.SqlParameter("@SERIALNUMBER",SerialNumber), 
                   new System.Data.SqlClient.SqlParameter("@Run_Hour",Run_Hour),
                   new System.Data.SqlClient.SqlParameter("@Critical",Critical),
                    new System.Data.SqlClient.SqlParameter("@ServiceAccount",ServiceAccount),

            };
            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PMS_Update_System", obj);
        }

        public int LibraryCatalogueUpdate(int userid, int systemid, string systemcode, string systemdesc, string stystemparticular, string maker, string setinstalled, string model
, string dept, string vesselid, string functionid, string accountcode, string SerialNumber, int Run_Hour, int Critical)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@USERID", userid ),
                   new System.Data.SqlClient.SqlParameter("@SYSTEMID", systemid ),
                   new System.Data.SqlClient.SqlParameter("@SYSTEM_CODE", systemcode ),
                   new System.Data.SqlClient.SqlParameter("@SYSTEM_DESCRIPTION", systemdesc),
                   new System.Data.SqlClient.SqlParameter("@SYSTEM_PARTICULARS", stystemparticular),
                   new System.Data.SqlClient.SqlParameter("@MAKER", maker),
                   new System.Data.SqlClient.SqlParameter("@SET_INSTALED",setinstalled),
                   new System.Data.SqlClient.SqlParameter("@MODEL",model),
                   new System.Data.SqlClient.SqlParameter("@DEPT1", dept ),
                   new System.Data.SqlClient.SqlParameter("@VESSEL_ID",vesselid),
                   new System.Data.SqlClient.SqlParameter("@FUNCTIONID",functionid),
                   new System.Data.SqlClient.SqlParameter("@ACCOUNTCODE",accountcode),
                   new System.Data.SqlClient.SqlParameter("@SERIALNUMBER",SerialNumber), 
                      new System.Data.SqlClient.SqlParameter("@Run_Hour",Run_Hour),
                       new System.Data.SqlClient.SqlParameter("@Critical",Critical),
            };
            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_PR_SYSTEMSUPDATE", obj);
        }


        public int LibraryCatalogueDelete(int userid, int systemid)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@ID", systemid ),
                   new System.Data.SqlClient.SqlParameter("@USERID", userid ),
             };
            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_PR_SYSTEMSDELETE", obj);

        }

        public int LibraryCatalogueRestore(int userid, int systemid)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@ID", systemid ),
                   new System.Data.SqlClient.SqlParameter("@USERID", userid ),
             };
            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_PR_SYSTEMSRESTORE", obj);
        }


        public int PMS_Get_SystemStatus(int systemid)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@SystemID", systemid ),
                  
             };
            return Convert.ToInt32(SqlHelper.ExecuteScalar(_internalConnection, CommandType.StoredProcedure, "PMS_Get_SystemStatus", obj));
        }

        public int PMS_Get_SubSystemStatus(int subsystemid)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@SubSystemID", subsystemid ),
                   
             };
            return Convert.ToInt32(SqlHelper.ExecuteScalar(_internalConnection, CommandType.StoredProcedure, "PMS_Get_SubSystemStatus", obj));
        }

        public DataSet LibrarySubCatalogueList(string subsystemid)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
           { 
                   new System.Data.SqlClient.SqlParameter("@SUBSYSTEMID", subsystemid),
           };

            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_PR_SUBSYSTEMLIST", obj);
        }


        public DataSet LibrarySubCatalogueSearch(string systemcode, string subsystemdesc, string subsystemparticular
            , int? IsActive, string sortby, int? sortdirection, int pagenumber, int pagesize, int? isfetchcount)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@SYSTEMCODE", systemcode),
                   new System.Data.SqlClient.SqlParameter("@SUBSYSTEMDESC",subsystemdesc),
                   new System.Data.SqlClient.SqlParameter("@SUBSYSTEMPARTICULAR", subsystemparticular),

                   new System.Data.SqlClient.SqlParameter("@ISACTIVE",IsActive), 
                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                   new System.Data.SqlClient.SqlParameter("@ROWCOUNT",SqlDbType.Int)  
            };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_PR_SUBCATALOGUESEARCH", obj);

        }


        public int PMS_Insert_NewSubSystem(int userid, string systemcode, string substystemdesc, string subsytemparticular, string Maker, string Model, string SerialNo, int IsLocationRequired, int Run_Hour, int Critical, string setInstalled)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                    new System.Data.SqlClient.SqlParameter("@USERID", userid ),
                    new System.Data.SqlClient.SqlParameter("@SYSTEM_CODE", systemcode ),
                    new System.Data.SqlClient.SqlParameter("@SUBSYSTEM_DESCRIPTION", substystemdesc),
                    new System.Data.SqlClient.SqlParameter("@SUBSYSTEM_PARTICULARS", subsytemparticular),
                    new System.Data.SqlClient.SqlParameter("@Maker", Maker),
                    new System.Data.SqlClient.SqlParameter("@Model", Model),
                    new System.Data.SqlClient.SqlParameter("@SerialNo", SerialNo),
                    new SqlParameter("@IsLocationRequired",IsLocationRequired),
                    new System.Data.SqlClient.SqlParameter("@Run_Hour",Run_Hour),
                    new System.Data.SqlClient.SqlParameter("@Critical",Critical),
                   new System.Data.SqlClient.SqlParameter("@SET_INSTALED",setInstalled),
                    new System.Data.SqlClient.SqlParameter("@Return", SqlDbType.Int),
                    
            };

            obj[obj.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PMS_Insert_NewSubSystem", obj);
            return Convert.ToInt32(obj[obj.Length - 1].Value.ToString());
        }
        public int LibrarySubCatalogueSave(int userid, string systemcode, string subsystemcode, string substystemdesc, string subsytemparticular, string Maker, string Model, string SerialNo, int IsLocationRequired, int Run_Hour, int Critical, int Copy_Run_Hour)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@USERID", userid ),
                   new System.Data.SqlClient.SqlParameter("@SYSTEM_CODE", systemcode ),
                   new System.Data.SqlClient.SqlParameter("@SUBSYSTEM_CODE", subsystemcode),
                   new System.Data.SqlClient.SqlParameter("@SUBSYSTEM_DESCRIPTION", substystemdesc),
                   new System.Data.SqlClient.SqlParameter("@SUBSYSTEM_PARTICULARS", subsytemparticular),
                    new System.Data.SqlClient.SqlParameter("@Maker", Maker),
                   new System.Data.SqlClient.SqlParameter("@Model", Model),
                   new System.Data.SqlClient.SqlParameter("@SerialNo", SerialNo),
                   new SqlParameter("@IsLocationRequired",IsLocationRequired),
                      new System.Data.SqlClient.SqlParameter("@Run_Hour",Run_Hour),
                       new System.Data.SqlClient.SqlParameter("@Critical",Critical),
                           new System.Data.SqlClient.SqlParameter("@Copy_Run_Hour",Copy_Run_Hour),
                   new System.Data.SqlClient.SqlParameter("@Return", SqlDbType.Int),
                    
            };

            obj[obj.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_PR_SUBSYSTEMINSERT", obj);
            return Convert.ToInt32(obj[obj.Length - 1].Value.ToString());
        }
        public int LibrarySubCatalogueUpdate(int userid, int subsystemid, string systemcode, string subsystemcode, string substystemdesc, string subsytemparticular, string Maker, string Model, string SerialNo, int Run_Hour, int Critical, int Copy_Run_Hour)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@USERID", userid ),
                   new System.Data.SqlClient.SqlParameter("@SUBSYSTEMID", subsystemid ),
                   new System.Data.SqlClient.SqlParameter("@SYSTEM_CODE", systemcode ),
                   new System.Data.SqlClient.SqlParameter("@SUBSYSTEM_DESCRIPTION", substystemdesc),
                   new System.Data.SqlClient.SqlParameter("@SUBSYSTEM_PARTICULARS", subsytemparticular),

                   new System.Data.SqlClient.SqlParameter("@Maker", Maker),
                   new System.Data.SqlClient.SqlParameter("@Model", Model),
                   new System.Data.SqlClient.SqlParameter("@SerialNo", SerialNo),
                       new System.Data.SqlClient.SqlParameter("@Run_Hour",Run_Hour),
                       new System.Data.SqlClient.SqlParameter("@Critical",Critical),
                    new System.Data.SqlClient.SqlParameter("@Copy_Run_Hour",Copy_Run_Hour),
            };
            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_PR_SUBSYSTEMSUPDATE", obj);

        }
        public int PMS_Update_SubSystem(int userid, int subsystemid, string systemcode, string subsystemcode, string substystemdesc, string subsytemparticular, string Maker, string Model, string SerialNo, int Run_Hour, int Critical, string setinstalled)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                    new System.Data.SqlClient.SqlParameter("@USERID", userid ),
                    new System.Data.SqlClient.SqlParameter("@SUBSYSTEMID", subsystemid ),
                    new System.Data.SqlClient.SqlParameter("@SYSTEM_CODE", systemcode ),
                    new System.Data.SqlClient.SqlParameter("@SUBSYSTEM_DESCRIPTION", substystemdesc),
                    new System.Data.SqlClient.SqlParameter("@SUBSYSTEM_PARTICULARS", subsytemparticular),

                    new System.Data.SqlClient.SqlParameter("@Maker", Maker),
                    new System.Data.SqlClient.SqlParameter("@Model", Model),
                    new System.Data.SqlClient.SqlParameter("@SerialNo", SerialNo),
                    new System.Data.SqlClient.SqlParameter("@Run_Hour",Run_Hour),
                    new System.Data.SqlClient.SqlParameter("@Critical",Critical),
                    new System.Data.SqlClient.SqlParameter("@SET_INSTALED",setinstalled),
            };
            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PMS_Update_SubSystem", obj);

        }

        public int LibrarySubCatalogueDelete(int userid, int subsystemid)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@ID", subsystemid),
                   new System.Data.SqlClient.SqlParameter("@USERID", userid),
             };
            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_PR_SUBSYSTEMSDELETE", obj);

        }



        public int LibrarySubCatalogueRestore(int userid, int subsystemid)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@ID", subsystemid),
                   new System.Data.SqlClient.SqlParameter("@USERID", userid),
             };
            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_PR_SUBSYSTEMSRESTORE", obj);

        }


        public DataSet LibraryItemList(string itemid)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
           { 
                   new System.Data.SqlClient.SqlParameter("@ITEMID", itemid),
           };

            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_PR_ITEMLIST", obj);
        }



        public DataSet LibraryItemSearch(string systemcode, string subsystemcode, int? vesselid, string partnumber, string drawingnumber, string name, string longdesc,
             int? IsActive, string sortby, int? sortdirection, int pagenumber, int pagesize, int? isfetchcount, out int rowcount)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@SYSTEMCODE", systemcode),
                   new System.Data.SqlClient.SqlParameter("@SUBSYSTEMCODE",subsystemcode),
                   new System.Data.SqlClient.SqlParameter("@VESSELID", vesselid),
                   new System.Data.SqlClient.SqlParameter("@PARTNUMBER", partnumber),
                   new System.Data.SqlClient.SqlParameter("@DRAWINGNUMBER",drawingnumber),
                   new System.Data.SqlClient.SqlParameter("@NAME", name),
                   new System.Data.SqlClient.SqlParameter("@LONGDESC", longdesc),
              
                   new System.Data.SqlClient.SqlParameter("@ISACTIVE",IsActive), 
                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                   new System.Data.SqlClient.SqlParameter("@ROWCOUNT",SqlDbType.Int)  
            };
            obj[obj.Length - 1].Direction = ParameterDirection.Output;

            DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_PR_ITEMSEARCH", obj);
            rowcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds;

        }

        public DataSet LibraryItemSearch(string systemcode, string subsystemcode, int? vesselid, string partnumber, string drawingnumber, string name, string longdesc,
            int? IsActive, string searchtext, string sortby, int? sortdirection, int pagenumber, int pagesize, int? isfetchcount, out int rowcount)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@SYSTEMCODE", systemcode),
                   new System.Data.SqlClient.SqlParameter("@SUBSYSTEMCODE",subsystemcode),
                   new System.Data.SqlClient.SqlParameter("@VESSELID", vesselid),
                   new System.Data.SqlClient.SqlParameter("@PARTNUMBER", partnumber),
                   new System.Data.SqlClient.SqlParameter("@DRAWINGNUMBER",drawingnumber),
                   new System.Data.SqlClient.SqlParameter("@NAME", name),
                   new System.Data.SqlClient.SqlParameter("@LONGDESC", longdesc),
                   new System.Data.SqlClient.SqlParameter("@ISACTIVE",IsActive), 
                   new System.Data.SqlClient.SqlParameter("@SEARCHTEXT",searchtext), 
                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                   new System.Data.SqlClient.SqlParameter("@ROWCOUNT",SqlDbType.Int)  
            };
            obj[obj.Length - 1].Direction = ParameterDirection.Output;

            DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_PR_ITEMSEARCH", obj);
            rowcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds;

        }

        public DataSet LibraryALLItemSearch(string systemcode, string subsystemcode, int? vesselid, string partnumber, string drawingnumber, string name, string longdesc,
            int? IsActive, string sortby, int? sortdirection, int pagenumber, int pagesize, int? isfetchcount, out int rowcount)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@SYSTEMCODE", systemcode),
                   new System.Data.SqlClient.SqlParameter("@SUBSYSTEMCODE",subsystemcode),
                   new System.Data.SqlClient.SqlParameter("@VESSELID", vesselid),
                   new System.Data.SqlClient.SqlParameter("@PARTNUMBER", partnumber),
                   new System.Data.SqlClient.SqlParameter("@DRAWINGNUMBER",drawingnumber),
                   new System.Data.SqlClient.SqlParameter("@NAME", name),
                   new System.Data.SqlClient.SqlParameter("@LONGDESC", longdesc),
              
                   new System.Data.SqlClient.SqlParameter("@ISACTIVE",IsActive), 
                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                   new System.Data.SqlClient.SqlParameter("@ROWCOUNT",SqlDbType.Int)  
            };
            obj[obj.Length - 1].Direction = ParameterDirection.Output;

            DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_PR_ALL_ITEMS_EARCH", obj);
            rowcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds;

        }




        public string LibraryItemSave(int userid, string systemcode, string subsystemcode, string partnumber, string name, string description, string drawingnumber
       , string unit, decimal? inventorymin, decimal? inverntorymax, string vesselid, int? itemcategory, string image_url, string product_details, int? critical_flag)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@USERID", userid ),
                   new System.Data.SqlClient.SqlParameter("@SYSTEM_CODE", systemcode ),
                   new System.Data.SqlClient.SqlParameter("@SUBSYSTEM_CODE", subsystemcode),
                   new System.Data.SqlClient.SqlParameter("@PART_NUMBER", partnumber),
                   new System.Data.SqlClient.SqlParameter("@NAME", name),
                   new System.Data.SqlClient.SqlParameter("@DESCRIPTION", description),
                   new System.Data.SqlClient.SqlParameter("@DRAWING_NUMBER", drawingnumber),
                   new System.Data.SqlClient.SqlParameter("@UNIT", unit),
                   new System.Data.SqlClient.SqlParameter("@INVENTORY_MIN", inventorymin),
                   new System.Data.SqlClient.SqlParameter("@INVENTORY_MAX", inverntorymax),
                   new System.Data.SqlClient.SqlParameter("@VESSEL_ID", vesselid),
                   new System.Data.SqlClient.SqlParameter("@Item_Category", itemcategory),
                   new System.Data.SqlClient.SqlParameter("@Image_Url", image_url),
                   new System.Data.SqlClient.SqlParameter("@Product_Details", product_details),
                   new System.Data.SqlClient.SqlParameter("@critical_flag", critical_flag),

                   new System.Data.SqlClient.SqlParameter("@ITEMID_OUT", SqlDbType.VarChar,30),
            };
            obj[obj.Length - 1].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_PR_ITEMSINSERT", obj);
            return obj[obj.Length - 1].Value.ToString();
        }

        public string LibraryItemSave(int userid, string systemcode, string subsystemcode, string partnumber, string name, string description, string drawingnumber
       , string unit, decimal? inventorymin, decimal? inverntorymax, string vesselid, int? itemcategory, string image_url, string product_details, int? critical_flag, Boolean Catalogue_Item)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@USERID", userid ),
                   new System.Data.SqlClient.SqlParameter("@SYSTEM_CODE", systemcode ),
                   new System.Data.SqlClient.SqlParameter("@SUBSYSTEM_CODE", subsystemcode),
                   new System.Data.SqlClient.SqlParameter("@PART_NUMBER", partnumber),
                   new System.Data.SqlClient.SqlParameter("@NAME", name),
                   new System.Data.SqlClient.SqlParameter("@DESCRIPTION", description),
                   new System.Data.SqlClient.SqlParameter("@DRAWING_NUMBER", drawingnumber),
                   new System.Data.SqlClient.SqlParameter("@UNIT", unit),
                   new System.Data.SqlClient.SqlParameter("@INVENTORY_MIN", inventorymin),
                   new System.Data.SqlClient.SqlParameter("@INVENTORY_MAX", inverntorymax),
                   new System.Data.SqlClient.SqlParameter("@VESSEL_ID", vesselid),
                   new System.Data.SqlClient.SqlParameter("@Item_Category", itemcategory),
                   new System.Data.SqlClient.SqlParameter("@Image_Url", image_url),
                   new System.Data.SqlClient.SqlParameter("@Product_Details", product_details),
                   new System.Data.SqlClient.SqlParameter("@critical_flag", critical_flag),
                   new System.Data.SqlClient.SqlParameter("@Catalogue_Item", Catalogue_Item),
                   new System.Data.SqlClient.SqlParameter("@ITEMID_OUT", SqlDbType.VarChar,30),
            };
            obj[obj.Length - 1].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_INS_REQN_ITEMS", obj);
            return obj[obj.Length - 1].Value.ToString();
        }


        public int LibraryItemUpdate(int userid, string itemid, string systemcode, string subsystemcode, string partnumber, string name, string description, string drawingnumber
         , string unit, decimal? inventorymin, decimal? inverntorymax, string vesselid, int? itemcategory, string image_url, string product_details, int? critical_flag)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@USERID", userid ),
                   new System.Data.SqlClient.SqlParameter("@ITEMID", itemid ),
                   new System.Data.SqlClient.SqlParameter("@SYSTEM_CODE", systemcode ),
                   new System.Data.SqlClient.SqlParameter("@SUBSYSTEM_CODE", subsystemcode),
                   new System.Data.SqlClient.SqlParameter("@PART_NUMBER", partnumber),
                   new System.Data.SqlClient.SqlParameter("@NAME", name),
                   new System.Data.SqlClient.SqlParameter("@DESCRIPTION", description),
                   new System.Data.SqlClient.SqlParameter("@DRAWING_NUMBER", drawingnumber),
                   new System.Data.SqlClient.SqlParameter("@UNIT", unit),
                   new System.Data.SqlClient.SqlParameter("@INVENTORY_MIN", inventorymin),
                   new System.Data.SqlClient.SqlParameter("@INVENTORY_MAX", inverntorymax),
                   new System.Data.SqlClient.SqlParameter("@VESSEL_ID", vesselid),
                   new System.Data.SqlClient.SqlParameter("@Item_Category", itemcategory),
                   new System.Data.SqlClient.SqlParameter("@Image_Url", image_url),
                   new System.Data.SqlClient.SqlParameter("@Product_Details", product_details),
                   new System.Data.SqlClient.SqlParameter("@critical_flag", critical_flag),
            };

            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_PR_ITEMSUPDATE", obj);
        }


        public int LibraryItemDelete(int userid, string itemid)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@ID", itemid),
                   new System.Data.SqlClient.SqlParameter("@USERID", userid),
             };
            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_PR_ITEMDELETE", obj);

        }


        public int LibraryItemRestore(int userid, string itemid)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@ID", itemid),
                   new System.Data.SqlClient.SqlParameter("@USERID", userid),
             };
            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_PR_ITEMRESTORE", obj);
        }


        public DataTable LibraryGetSystemParameterList(string parenttypecode, string searchtext)
        {
            DataTable dt = new DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@PARENT_TYPE", parenttypecode), 
                   new System.Data.SqlClient.SqlParameter("@SEARCHTEXT", searchtext),
             };
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_PR_GETSYSTEM_PARAMETER_LIST", obj);
            dt = ds.Tables[0];
            return dt;

        }


        public DataTable LibraryGetNextSystemCode()
        {
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "TEC_PR_GET_NEXT_SYSYTEM_CODE").Tables[0];
        }


        public DataTable GET_ROB_Less_Min_Quantity_Search(string searchtext, string Form_Type, int? VESSEL_ID, string System_Code
                    , string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   
                   new System.Data.SqlClient.SqlParameter("@SearchText", searchtext),
                   new System.Data.SqlClient.SqlParameter("@Form_Type", Form_Type),
                   new System.Data.SqlClient.SqlParameter("@VESSEL_ID", VESSEL_ID),
                   new System.Data.SqlClient.SqlParameter("@System_Code", System_Code),


                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                    
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "INF_DASH_GET_ROB_Less_Min_Quantity", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds.Tables[0];
        }





        public DataTable GET_SYSTEM_LOCATION(int Function, int VESSEL_ID)
        {

            DataTable dt = new DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@Function", Function), 
                   new System.Data.SqlClient.SqlParameter("@VESSEL_ID", VESSEL_ID), 
             };
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "TEC_PR_GET_SYSTEM_LOCATION", obj);
            dt = ds.Tables[0];
            return dt;

        }



        public DataTable GET_SUBSYTEMSYSTEM_LOCATION(string SYSTEMCODE, int? SUBSYSTEMID, int VESSEL_ID)
        {
            DataTable dt = new DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@SYSTEMCODE", SYSTEMCODE), 
                   new System.Data.SqlClient.SqlParameter("@SUBSYSTEMID", SUBSYSTEMID), 
                   new System.Data.SqlClient.SqlParameter("@VESSEL_ID", VESSEL_ID),
             };
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "TEC_PR_GET_SUBSYTEMSYSTEM_LOCATION", obj);
            dt = ds.Tables[0];
            return dt;

        }


        public DataTable GET_Job_Done_History_List(int? Job_ID, int? Vessel_ID)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@JOB_ID", Job_ID),
                   new System.Data.SqlClient.SqlParameter("@VESSEL_ID", Vessel_ID),
            };

            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "TEC_GET_JOB_DONE_HISTORY_LIST", obj).Tables[0];
        }



        public DataTable GET_SUBSYTEMSYSTEM_ASSIGNED_LOCATION(string SYSTEMCODE, int? SUBSYSTEMID, int VESSEL_ID)
        {
            DataTable dt = new DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@SYSTEMID", SYSTEMCODE), 
                   new System.Data.SqlClient.SqlParameter("@SUBSYSTEMID", SUBSYSTEMID), 
                   new System.Data.SqlClient.SqlParameter("@VESSEL_ID", VESSEL_ID),
             };
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "TEC_PR_GET_SUBSYTEMSYSTEM_ASSIGNED_LOCATION", obj);
            dt = ds.Tables[0];
            return dt;

        }

        public int INSERT_ADHOC_JOB(int? Job_History_id, int? Office_ID, int Vessel_ID, int Function_ID, int Location_ID, int System_ID, int SubSystem_ID, int SubSystem_Location_ID, string Job_Short_Description
            , string Job_Long_Description, string Job_Status, DateTime? Date_Done, int Created_By
            , int? PIC, int? Assigner, int? Defer_to_DryDock, string Priority, int? Inspector, DateTime? Inspection_Date, int? Dept_On_Ship, int? Dept_on_Office
            , DateTime? Expected_completion, DateTime? Completed_on, int PrimaryCategory, int SecondaryCategory, int? PSC_SIRE, string REQUISITION_CODE, int? IsSafetyAlam, int? IsCalibration
            , int? IsFunctional, string Effect, decimal? FunctionalDecimal, int? Unit, int? JobWorkType, ref int Return_ID)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@Job_History_id", Job_History_id),
                new System.Data.SqlClient.SqlParameter("@Office_ID", Office_ID),
                new System.Data.SqlClient.SqlParameter("@Vessel_ID", Vessel_ID),
                new System.Data.SqlClient.SqlParameter("@Function_ID", Function_ID),
                new System.Data.SqlClient.SqlParameter("@Location_ID", Location_ID),
                new System.Data.SqlClient.SqlParameter("@System_ID", System_ID),
                new System.Data.SqlClient.SqlParameter("@SubSystem_ID", SubSystem_ID),
                new System.Data.SqlClient.SqlParameter("@SubSystem_Location_ID", SubSystem_Location_ID),
                new System.Data.SqlClient.SqlParameter("@Job_Short_Description", Job_Short_Description),
                new System.Data.SqlClient.SqlParameter("@Job_Long_Description", Job_Long_Description),
                new System.Data.SqlClient.SqlParameter("@Job_Status", Job_Status),
                new System.Data.SqlClient.SqlParameter("@Date_Done", Date_Done),
                new System.Data.SqlClient.SqlParameter("@Created_By", Created_By),
                new System.Data.SqlClient.SqlParameter("@PIC", PIC),
                new System.Data.SqlClient.SqlParameter("@Assigner", Assigner),
                new System.Data.SqlClient.SqlParameter("@Defer_to_DryDock", Defer_to_DryDock),
                new System.Data.SqlClient.SqlParameter("@Priority", Priority),
                new System.Data.SqlClient.SqlParameter("@Inspector", Inspector),
                new System.Data.SqlClient.SqlParameter("@Inspection_Date", Inspection_Date),
                new System.Data.SqlClient.SqlParameter("@Dept_On_Ship", Dept_On_Ship),
                new System.Data.SqlClient.SqlParameter("@Dept_on_Office", Dept_on_Office),
                new System.Data.SqlClient.SqlParameter("@Expected_completion", Expected_completion),
                new System.Data.SqlClient.SqlParameter("@Completed_on", Completed_on),
                new System.Data.SqlClient.SqlParameter("@Primary_Category", PrimaryCategory),
                new System.Data.SqlClient.SqlParameter("@Secondary_Category", SecondaryCategory),
                new System.Data.SqlClient.SqlParameter("@PSC_SIRE", PSC_SIRE),
                new System.Data.SqlClient.SqlParameter("@REQUISITION_CODE", REQUISITION_CODE),
                new System.Data.SqlClient.SqlParameter("@IsSafetyAlarm", IsSafetyAlam),
                new System.Data.SqlClient.SqlParameter("@IsCalibration", IsCalibration),                
                new System.Data.SqlClient.SqlParameter("@IsFunctional", IsFunctional),
                new System.Data.SqlClient.SqlParameter("@Effect", Effect),
                new System.Data.SqlClient.SqlParameter("@SetPointDecimal", FunctionalDecimal),
                new System.Data.SqlClient.SqlParameter("@SetPointUnit", Unit),
                new System.Data.SqlClient.SqlParameter("@JobWorkType", JobWorkType),
                new System.Data.SqlClient.SqlParameter("@Return_ID",SqlDbType.Int),


             };
            obj[obj.Length - 1].Direction = ParameterDirection.Output;
            int retval = SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "TEC_PR_INSERT_ADHOC_JOB", obj);
            Return_ID = Convert.ToInt32(obj[obj.Length - 1].Value);
            return retval;

        }


        public int Verify_By_office_Adhoc_Job(int? Job_History_id, int Vessel_ID, int Office_ID, int Created_By)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@Job_History_id", Job_History_id),
                new System.Data.SqlClient.SqlParameter("@Vessel_ID", Vessel_ID),
                new System.Data.SqlClient.SqlParameter("@Office_ID", Office_ID),
                new System.Data.SqlClient.SqlParameter("@CREATED_BY", Created_By),
            };

            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "TEC_Adhoc_Job_Verify_By_Office", obj);


        }



        public int Rework_Adhoc_Job(int? Job_History_id, int Vessel_ID, int Office_ID, int Created_By)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@Job_Histroy_Id", Job_History_id),
                new System.Data.SqlClient.SqlParameter("@Vessel_ID", Vessel_ID),
                new System.Data.SqlClient.SqlParameter("@Office_ID", Office_ID),
                new System.Data.SqlClient.SqlParameter("@CREATED_BY", Created_By),
            };

            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "TEC_Adhoc_Job_Rework", obj);


        }





        public DataTable GET_Adhoc_Job_Search(string searchtext, int? FleetID, int? Vessel_ID, int? Location_ID, int? System_ID, int? SubSystem_ID, int? SubSysLocation_ID, string Job_Status
                     , string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   
                   new System.Data.SqlClient.SqlParameter("@SearchText", searchtext),
                   new System.Data.SqlClient.SqlParameter("@FleetID", FleetID),
                   new System.Data.SqlClient.SqlParameter("@Vessel_ID", Vessel_ID),
                   new System.Data.SqlClient.SqlParameter("@Location_ID", Location_ID),
                   new System.Data.SqlClient.SqlParameter("@System_ID", System_ID),
                   new System.Data.SqlClient.SqlParameter("@SubSystem_ID", SubSystem_ID),
                    new System.Data.SqlClient.SqlParameter("@SubSystemLocation_ID", SubSysLocation_ID),
                   new System.Data.SqlClient.SqlParameter("@Job_Status", Job_Status),
                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                    
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "TEC_PR_GET_Adhoc_Job_Search", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds.Tables[0];
        }

        public DataTable GET_Adhoc_Job_Search(string searchtext, int? FleetID, int? Vessel_ID, int? Location_ID, int? System_ID, int? SubSystem_ID, string Job_Status
                , string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   
                   new System.Data.SqlClient.SqlParameter("@SearchText", searchtext),
                   new System.Data.SqlClient.SqlParameter("@FleetID", FleetID),
                   new System.Data.SqlClient.SqlParameter("@Vessel_ID", Vessel_ID),
                   new System.Data.SqlClient.SqlParameter("@Location_ID", Location_ID),
                   new System.Data.SqlClient.SqlParameter("@System_ID", System_ID),
                   new System.Data.SqlClient.SqlParameter("@SubSystem_ID", SubSystem_ID),
                   new System.Data.SqlClient.SqlParameter("@Job_Status", Job_Status),
                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                    
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "TEC_PR_GET_Adhoc_Job_Search", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds.Tables[0];
        }
        public DataTable GET_Adhoc_Job_Details(int? Job_Histroy_Id, int? Vessel_ID, int? Office_ID, int Job_ID)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@Job_Histroy_Id", Job_Histroy_Id),
                   new System.Data.SqlClient.SqlParameter("@Vessel_ID", Vessel_ID),
                   new System.Data.SqlClient.SqlParameter("@Office_ID", Office_ID),
                   new System.Data.SqlClient.SqlParameter("@Job_ID", Job_ID),
            };

            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "TEC_GET_Adhoc_Job_Details", obj).Tables[0];
        }




        public int DELETE_JOB_DONE_ATTACHMENT(int ID, int Vessel_ID, int JOB_HISTORY_ID, int Office_ID, int Created_By)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@ID", ID),
                new System.Data.SqlClient.SqlParameter("@VESSEL_ID", Vessel_ID),
                new System.Data.SqlClient.SqlParameter("@JOB_HISTORY_ID", JOB_HISTORY_ID),
                new System.Data.SqlClient.SqlParameter("@Office_ID", Office_ID),
                new System.Data.SqlClient.SqlParameter("@CREATED_BY", Created_By),
             };
            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PMS_DEL_JOB_DONE_ATTACHMENT", obj);

        }


        public DataTable GET_JOB_DONE_ATTACHMENT(int VESSEL_ID, int? Office_ID, int? JOB_HISTORY_ID, int? JH_Office_ID)
        {
            DataTable dt = new DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@VESSEL_ID", VESSEL_ID), 
                       new System.Data.SqlClient.SqlParameter("@Office_ID", Office_ID), 
                   new System.Data.SqlClient.SqlParameter("@JOB_HISTORY_ID", JOB_HISTORY_ID),
                       new System.Data.SqlClient.SqlParameter("@JH_Office_ID", JH_Office_ID),
            };
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PMS_GET_JOB_DONE_ATTACHMENT", obj);
            dt = ds.Tables[0];
            return dt;

        }



        public int INSERT_JOB_DONE_ATTACHMENT(int Vessel_ID, int JOB_HISTORY_ID, int Office_ID, string ATTACHMENT_NAME, string ATTACHMENT_PATH, int SIZE, int Created_By, int? JH_Office_ID)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                
                new System.Data.SqlClient.SqlParameter("@VESSEL_ID", Vessel_ID),
                new System.Data.SqlClient.SqlParameter("@JOB_HISTORY_ID", JOB_HISTORY_ID),
                new System.Data.SqlClient.SqlParameter("@Office_ID", Office_ID),
                new System.Data.SqlClient.SqlParameter("@ATTACHMENT_NAME", ATTACHMENT_NAME),
                new System.Data.SqlClient.SqlParameter("@ATTACHMENT_PATH", ATTACHMENT_PATH),
                new System.Data.SqlClient.SqlParameter("@SIZE", SIZE),
                new System.Data.SqlClient.SqlParameter("@CREATED_BY", Created_By),
                 new System.Data.SqlClient.SqlParameter("@JH_Office_ID", JH_Office_ID),

             };
            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PMS_INS_JOB_DONE_ATTACHMENT", obj);

        }


        public DataTable GET_JOB_HISTORY_REMARKS(int VESSEL_ID, int? Office_ID, int? JOB_HISTORY_ID, int? JH_Office_ID)
        {
            DataTable dt = new DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@VESSEL_ID", VESSEL_ID), 
                   new System.Data.SqlClient.SqlParameter("@Office_ID", Office_ID), 
                   new System.Data.SqlClient.SqlParameter("@JOB_HISTORY_ID", JOB_HISTORY_ID),
                   new System.Data.SqlClient.SqlParameter("@JH_Office_ID", JH_Office_ID),
            };
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PMS_GET_JOB_HISTORY_REMARKS", obj);
            dt = ds.Tables[0];
            return dt;
        }


        public int INSERT_JOB_HISTORY_REMARKS(int Vessel_ID, int History_Office_ID, int History_ID, string Remark, int Created_By, int? Office_ID)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                
                new System.Data.SqlClient.SqlParameter("@VESSEL_ID", Vessel_ID),
                new System.Data.SqlClient.SqlParameter("@History_Office_ID", History_Office_ID),
                new System.Data.SqlClient.SqlParameter("@History_ID", History_ID),
                new System.Data.SqlClient.SqlParameter("@Remark", Remark),
                new System.Data.SqlClient.SqlParameter("@Created_By", Created_By),
                new System.Data.SqlClient.SqlParameter("@Office_ID", Office_ID),
               
             };
            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PMS_INS_JOB_HISTORY_REMARKS", obj);

        }




        public int Delete_JOB_HISTORY_REMARKS(int Vessel_ID, int Office_ID, int History_ID, int Created_By)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                
                new System.Data.SqlClient.SqlParameter("@VESSEL_ID", Vessel_ID),
                new System.Data.SqlClient.SqlParameter("@Office_ID", Office_ID),
                new System.Data.SqlClient.SqlParameter("@History_ID", History_ID),
                new System.Data.SqlClient.SqlParameter("@Created_By", Created_By),
               
             };
            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PMS_DEL_JOB_HISTORY_REMARKS", obj);

        }





        public DataSet Get_CriticalEquipmentIndex(int? Vessel_ID, int? Page_Index, int? Page_Size, ref int is_Fetch_Count)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
            {
 
                new SqlParameter("@Vessel_ID",Vessel_ID),
                new SqlParameter("@PAGE_INDEX",Page_Index),
                new SqlParameter("@PAGE_SIZE",Page_Size),
                new SqlParameter("@IS_FETCH_COUNT",is_Fetch_Count)
            };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.InputOutput;
            DataSet dt = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PMS_Get_CriticalEquipmentIndex", sqlprm);
            is_Fetch_Count = int.Parse(sqlprm[sqlprm.Length - 1].Value.ToString());
            return dt;


        }




        public int INSERT_AUTOMATIC_REQUISTION(int Company_Code, bool IsAutoReqsn, Boolean IsReqSupplier_Confirm)
        {
            SqlParameter[] sqlParam = new SqlParameter[]
            {
                   new SqlParameter("@Company_Code",Company_Code),
                   new SqlParameter("@Is_Auto_Requisition",IsAutoReqsn),
                   new SqlParameter("@Is_Req_Supplier_Confirm",IsReqSupplier_Confirm)
            };
            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_INSERT_AUTO_REQSN", sqlParam);
        }


        public DataTable GET_AUTOMATIC_REQUISTION(int Company_Code)
        {
            SqlParameter[] sqlParam = new SqlParameter[]
            {
                   new SqlParameter("@Company_Code",Company_Code)
                   
            };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_GET_AUTO_REQSN", sqlParam).Tables[0];
        }

        public DataTable GetMeatItemSetting()
        {
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_GET_Meat_Item_Setting");
            return ds.Tables[0];
        }
        public void UpdateMeatItemSetting(string MeatAllowance, string MeatLimit, int USERID)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            {                    
                new System.Data.SqlClient.SqlParameter("@MeatAllowance", MeatAllowance),
                new System.Data.SqlClient.SqlParameter("@MeatLimit", MeatLimit),
                new System.Data.SqlClient.SqlParameter("@USERID", USERID),
            };
            SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_UPD_Meat_Item_Setting", obj);

        }
        public DataTable GetRequisitionList(int Vessel_ID)
        {

            SqlParameter[] sqlprm = new SqlParameter[] { 
                new System.Data.SqlClient.SqlParameter("@Vessel_ID", Vessel_ID),
            };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_GET_REQUISITION_LIST", sqlprm).Tables[0];


        }

        public DataTable LibraryGetAlarmUnit()
        {
            SqlParameter[] sqlprm = new SqlParameter[] { 
                new System.Data.SqlClient.SqlParameter("@UnitName", ""),
                new System.Data.SqlClient.SqlParameter("@PageSize", 1000),
                new System.Data.SqlClient.SqlParameter("@PageNumber", 1),
            };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "SP_TEC_GET_ALARMUNIT", sqlprm).Tables[0];

        }

        public DataTable LibraryGetAlarmEffect()
        {
            SqlParameter[] sqlprm = new SqlParameter[] { 
                new System.Data.SqlClient.SqlParameter("@EffectName", ""),
                new System.Data.SqlClient.SqlParameter("@PageSize", 1000),
                new System.Data.SqlClient.SqlParameter("@PageNumber", 1),
            };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "SP_TEC_GET_ALARMEFFECT", sqlprm).Tables[0];

        }
        public int INS_Rank_Purc(int RankID, string Mode, int UserID)
        {
            SqlParameter[] SqlParam = new SqlParameter[]
            {  
                new SqlParameter("@RankID",RankID),
                new SqlParameter("@Mode",Mode),
                new SqlParameter("@User_ID",UserID)
   	        };
            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "INF_INS_UPD_PURC_RANK_CONFIG", SqlParam);
        }
        public DataTable Get_Purc_Rank()
        {
            DataTable dt = new DataTable();
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "INF_GET_PURC_RANK_CONFIG", null).Tables[0];

        }

        public string Get_Department(string SystemID)
        {
            string Dept=string.Empty;
            DataTable dt = new DataTable();
            SqlParameter [] sqlParam= new SqlParameter[]
            {
                new SqlParameter("@System_Code",SystemID)
            };
            dt = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_GET_DEPARTMENT_BY_SYSTEM_CODE", sqlParam).Tables[0];
            if (dt.Rows.Count > 0)
            {
                Dept = dt.Rows[0]["Dept1"].ToString();
            }
            return Dept;
        }

        public DataSet Get_Purc_LIB_ItemCategory(string searchtext, string Reqsn_Type, string id, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            SqlParameter[] sqlprm = new SqlParameter[] { 
                 new System.Data.SqlClient.SqlParameter("@SearchText", searchtext),
                   new System.Data.SqlClient.SqlParameter("@Category_Type", Reqsn_Type),
                   new System.Data.SqlClient.SqlParameter("@id",id),
                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                
            };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "Get_Purc_LIB_ItemCategory", sqlprm);
            isfetchcount = Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
            return ds;

        }
        public int PURC_INS_UPD_ItemCategory(string Cat_Name, string CatShortName, string Cat_Type, string Cat_id, string User, int del)
        {
            SqlParameter[] SqlParam = new SqlParameter[]
            {  
                new SqlParameter("@CategoryName",Cat_Name),
                new SqlParameter("@CategoryShortName",CatShortName),
                new SqlParameter("@CatagoryType",Cat_Type),
                new SqlParameter("@id",Cat_id),
                new SqlParameter("@user",User),
                new SqlParameter("@del",del)
   	        };
            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "Purc_INS_UPD_ItemCategory", SqlParam);
        }
        public DataSet Get_Purc_LIB_Functions(string searchtext, string Reqsn_Type, string id, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            SqlParameter[] sqlprm = new SqlParameter[] { 
                 new System.Data.SqlClient.SqlParameter("@SearchText", searchtext),
                 new SqlParameter("@Reqsn_Type",Reqsn_Type),
                   new System.Data.SqlClient.SqlParameter("@id",id),
                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                
            };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "Get_Purc_LIB_Functions", sqlprm);
            isfetchcount = Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
            return ds;
        }
        public int Purc_INS_UPD_Functions(string Func_Name, string Func_Code, string ReqsnType, string Func_Type, string Func_id, string User, string action)
        {
            SqlParameter[] SqlParam = new SqlParameter[]
            {  
                new SqlParameter("@FunctionName",Func_Name),
                new SqlParameter("@ReqsnType",ReqsnType),
                new SqlParameter("@ShortCode",Func_Code),
                new SqlParameter("@FunctionType",Func_Type),
                new SqlParameter("@id",Func_id),
                new SqlParameter("@user",User),
                new SqlParameter("@action",action),
   	        };
            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "Purc_INS_UPD_Functions", SqlParam);
        }
        public bool CheckFunctionType(string id)
        {
            SqlParameter[] SqlParam = new SqlParameter[]
            {  
                new SqlParameter("@id",id),
                
   	        };
            return SqlHelper.ExecuteScalar(_internalConnection, CommandType.StoredProcedure, "Purc_Check_reqsn_Type", SqlParam).ToString() == "Service" ? true:false;
        }

        public DataTable Purc_GetReqsnTypes()
        {
            DataTable dt = new DataTable();
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "Purc_GetReqsnTypes", null).Tables[0];
        }
    }
}
