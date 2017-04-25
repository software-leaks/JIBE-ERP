using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Configuration;


namespace SMS.Data.PMS
{
    public class DAL_PMS_Library_Jobs
    {

        private string _internalConnection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;

        public DAL_PMS_Library_Jobs()
        {
        }


        public DataSet LibraryJobList(int jobid)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
           { 
                   new System.Data.SqlClient.SqlParameter("@JOBID", jobid),
           };

            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "TEC_PR_JOB_LIST", obj);
        }


        public DataSet LibraryJobIndividualDetails(int jobid, int? JobHistoryID, string QueryFlag)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
           { 
                   new System.Data.SqlClient.SqlParameter("@JOBID", jobid),
                   new System.Data.SqlClient.SqlParameter("@JobHistoryID", JobHistoryID),
                   new System.Data.SqlClient.SqlParameter("@QueryFlag", QueryFlag)
                    
           };

            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "TEC_PR_JOB_INDIVIDUAL_DETAILS", obj);
        }

        /// <summary>
        /// For binding Function, SystemLocation & SubSystemLoaction
        /// </summary>
        public DataSet LibraryJobIndividualDetails(int jobid, int? JobHistoryID, string QueryFlag, int SysLocID,int SubSysLocID)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
           { 
                   new System.Data.SqlClient.SqlParameter("@JOBID", jobid),
                   new System.Data.SqlClient.SqlParameter("@JobHistoryID", JobHistoryID),
                   new System.Data.SqlClient.SqlParameter("@QueryFlag", QueryFlag),
                   new System.Data.SqlClient.SqlParameter("@SYSLOC", SysLocID),
                   new System.Data.SqlClient.SqlParameter("@SUBSYSLOC", SubSysLocID)
                    
           };

            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "TEC_PR_JOB_INDIVIDUAL_DETAILS", obj);
        }
        public DataSet LibraryJobSearch(int? systemid, int? subsysteid, int? vesselid, int? deptid, int? rankid, string jobtitle
            , int? IsActive, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@SYSTEMID", systemid),
                   new System.Data.SqlClient.SqlParameter("@SUBSYSTEMID",subsysteid),
                   new System.Data.SqlClient.SqlParameter("@VESSELID",vesselid),
                   new System.Data.SqlClient.SqlParameter("@DEPTID", deptid),
                   new System.Data.SqlClient.SqlParameter("@RANKID",rankid),
                   new System.Data.SqlClient.SqlParameter("@JOBTITLE", jobtitle),
                   new System.Data.SqlClient.SqlParameter("@ISACTIVE",IsActive), 
                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                    
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "TEC_PR_JOB_SEARCH", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds;
        }


        public DataSet ManageSystemJobSearch(int? systemid, int? subsysteid, int? vesselid, int? deptid, int? rankid, string jobtitle
            , int? IsActive, string searchtext, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@SYSTEMID", systemid),
                   new System.Data.SqlClient.SqlParameter("@SUBSYSTEMID",subsysteid),
                   new System.Data.SqlClient.SqlParameter("@VESSELID",vesselid),
                   new System.Data.SqlClient.SqlParameter("@DEPTID", deptid),
                   new System.Data.SqlClient.SqlParameter("@RANKID",rankid),
                   new System.Data.SqlClient.SqlParameter("@JOBTITLE", jobtitle),
                   new System.Data.SqlClient.SqlParameter("@ISACTIVE",IsActive),
                   new System.Data.SqlClient.SqlParameter("@SEARCHTEXT",searchtext), 
                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                    
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "TEC_PR_JOB_SEARCH_MANAGESYSTEM", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds;
        }

        /// <summary>
        /// Added by reshma : parameter added for RA : Is_RAMandatory & Is_RAApproval
        /// </summary>
        /// <param name="Vessel_ID">Vessel Id</param>
        /// <param name="SystemID">system id for specific job</param>
        /// <param name="SubSystemID">sub system id for specific job</param>
        /// <param name="DeptID">Department id for specific job</param>
        /// <param name="rankid">rank id</param>
        /// <param name="jobtitle"> job title</param>
        /// <param name="isactive">for check active status</param>
        /// <param name="searchtext"> serach text </param>
        /// <param name="Is_RAMandatory">check if RA is mandatory or not</param>
        /// <param name="Is_RAApproval">check if RA form is approve by office or not  </param>
        /// <param name="sortby">Column name by which data to be sorted</param>
        /// <param name="sortdirection">Direction in which data to be sorted 'ASC' or 'DESC'</param>
        /// <param name="pagenumber">Page Number of displaying data </param>
        /// <param name="pagesize">Max data to be return</param>
        /// <param name="isfetchcount">Return Total Job Count</param>
        /// <param name="TableID"></param>
        /// <returns></returns>
        public DataSet ManageSystemJobSearch(int? systemid, int? subsysteid, int? vesselid, int? deptid, int? rankid, string jobtitle
            , int? IsActive, string searchtext, int? Is_RAMandatory, int? Is_RAApproval, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@SYSTEMID", systemid),
                   new System.Data.SqlClient.SqlParameter("@SUBSYSTEMID",subsysteid),
                   new System.Data.SqlClient.SqlParameter("@VESSELID",vesselid),
                   new System.Data.SqlClient.SqlParameter("@DEPTID", deptid),
                   new System.Data.SqlClient.SqlParameter("@RANKID",rankid),
                   new System.Data.SqlClient.SqlParameter("@JOBTITLE", jobtitle),
                   new System.Data.SqlClient.SqlParameter("@ISACTIVE",IsActive),
                   new System.Data.SqlClient.SqlParameter("@SEARCHTEXT",searchtext), 
                   new System.Data.SqlClient.SqlParameter("@Is_RAMandatory",Is_RAMandatory),
                   new System.Data.SqlClient.SqlParameter("@Is_RAApproval",Is_RAApproval),
                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                    
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "TEC_PR_JOB_SEARCH_MANAGESYSTEM", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds;
        }
        public DataSet LibraryJobGetToCopy(string systemcode, int? subsystemid, int? vesselid, int? IsActive)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@SYSTEMCODE", systemcode),
                   new System.Data.SqlClient.SqlParameter("@SUBSYSTEMID",subsystemid),
                   new System.Data.SqlClient.SqlParameter("@VESSELID",vesselid),
                   new System.Data.SqlClient.SqlParameter("@ISACTIVE",IsActive), 
            };

            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "TEC_PR_GET_JOB_TO_COPY", obj);

        }



        public DataSet LibraryJobGetToMove(string systemcode, int? subsystemid, int? vesselid, int? IsActive)
        {


            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@SYSTEMCODE", systemcode),
                   new System.Data.SqlClient.SqlParameter("@SUBSYSTEMID",subsystemid),
                   new System.Data.SqlClient.SqlParameter("@VESSELID",vesselid),
                   new System.Data.SqlClient.SqlParameter("@ISACTIVE",IsActive), 
            };

            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "TEC_PR_GET_JOB_TO_MOVE", obj);

        }
        /// <summary>
        /// Added by reshma : parameter added for RA : Is_RAMandatory & Is_RAApproval
        /// </summary>
        /// <param name="userid">user Id </param>
        /// <param name="systemid">system id for specific job</param>
        /// <param name="subsystemid">sub system id for specific job</param>
        /// <param name="vesselid">vessel id</param>
        /// <param name="deptid">Department id for specific job</param>
        /// <param name="rankid">rank id</param>
        /// <param name="jobtitle">job title</param>
        /// <param name="jobdesc">job description</param>
        /// <param name="frequency">check frequency</param>
        /// <param name="frequencytype">check frequency type</param>
        /// <param name="cms">check if job is CMS or not</param>
        /// <param name="critical">check if job is critical or not</param>
        /// <param name="Job_Code"> job code</param>
        /// <param name="Is_Tech_Required"></param>
        /// <param name="Is_SafetyAlarm">check for SafetyAlarm</param>
        /// <param name="Is_Calibration">check for calibration</param>
        /// <param name="Is_RAMandatory">check if RA is mandatory or not</param>
        /// <param name="Is_RAApproval">check if RA form is approve by office or not</param>
        /// <returns></returns>
        
        public int LibraryJobSave(int userid, int systemid, int subsystemid, int vesselid, int deptid, int rankid, string jobtitle
           , string jobdesc, int frequency, int frequencytype, int cms, int critical, string Job_Code, int? Is_Tech_Required, int? Is_SafetyAlarm, int? Is_Calibration, int? Is_RAMandatory, int? Is_RAApproval)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@USERID", userid ),
                   new System.Data.SqlClient.SqlParameter("@SYSTEM_ID", systemid ),
                   new System.Data.SqlClient.SqlParameter("@SUBSYSTEM_ID", subsystemid),
                   new System.Data.SqlClient.SqlParameter("@VESSEL_ID", vesselid),
                   new System.Data.SqlClient.SqlParameter("@DEPT_ID", deptid),
                   new System.Data.SqlClient.SqlParameter("@RANK_ID",rankid),
                   new System.Data.SqlClient.SqlParameter("@JOBTITLE",jobtitle),
                   new System.Data.SqlClient.SqlParameter("@JOBDESCRIPTION", jobdesc ),
                   new System.Data.SqlClient.SqlParameter("@FREQUENCY",frequency),
                   new System.Data.SqlClient.SqlParameter("@FREQUENCY_TYPE",frequencytype),
                   new System.Data.SqlClient.SqlParameter("@CMS",cms),
                   new System.Data.SqlClient.SqlParameter("@CRITICAL",critical),
                   new System.Data.SqlClient.SqlParameter("@Job_Code",Job_Code),
                   new System.Data.SqlClient.SqlParameter("@Is_Tech_Required",Is_Tech_Required),
                   new System.Data.SqlClient.SqlParameter("@Is_SafetyAlarm",Is_SafetyAlarm),
                   new System.Data.SqlClient.SqlParameter("@Is_Calibration",Is_Calibration),
                   new System.Data.SqlClient.SqlParameter("@Is_RAMandatory",Is_RAMandatory),
                   new System.Data.SqlClient.SqlParameter("@Is_RAApproval",Is_RAApproval),
                   new System.Data.SqlClient.SqlParameter("@Return", SqlDbType.Int),
            };
            obj[obj.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "TEC_PR_JOB_INSERT", obj);
            return Convert.ToInt32(obj[obj.Length - 1].Value.ToString());
        }

        public int LibraryJobSave(int userid, int systemid, int subsystemid, int vesselid, int deptid, int rankid, string jobtitle
            , string jobdesc, int frequency, int frequencytype, int cms, int critical, string Job_Code, int? Is_Tech_Required, int? Is_SafetyAlarm, int? Is_Calibration)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@USERID", userid ),
                   new System.Data.SqlClient.SqlParameter("@SYSTEM_ID", systemid ),
                   new System.Data.SqlClient.SqlParameter("@SUBSYSTEM_ID", subsystemid),
                   new System.Data.SqlClient.SqlParameter("@VESSEL_ID", vesselid),
                   new System.Data.SqlClient.SqlParameter("@DEPT_ID", deptid),
                   new System.Data.SqlClient.SqlParameter("@RANK_ID",rankid),
                   new System.Data.SqlClient.SqlParameter("@JOBTITLE",jobtitle),
                   new System.Data.SqlClient.SqlParameter("@JOBDESCRIPTION", jobdesc ),
                   new System.Data.SqlClient.SqlParameter("@FREQUENCY",frequency),
                   new System.Data.SqlClient.SqlParameter("@FREQUENCY_TYPE",frequencytype),
                   new System.Data.SqlClient.SqlParameter("@CMS",cms),
                   new System.Data.SqlClient.SqlParameter("@CRITICAL",critical),
                   new System.Data.SqlClient.SqlParameter("@Job_Code",Job_Code),
                   new System.Data.SqlClient.SqlParameter("@Is_Tech_Required",Is_Tech_Required),
                   new System.Data.SqlClient.SqlParameter("@Is_SafetyAlarm",Is_SafetyAlarm),
                   new System.Data.SqlClient.SqlParameter("@Is_Calibration",Is_Calibration),
                   new System.Data.SqlClient.SqlParameter("@Return", SqlDbType.Int),
            };
            obj[obj.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "TEC_PR_JOB_INSERT", obj);
            return Convert.ToInt32(obj[obj.Length - 1].Value.ToString());
        }


        public int LibraryJobSave(int userid, int systemid, int subsystemid, int vesselid, int deptid, int rankid, string jobtitle
            , string jobdesc, int frequency, int frequencytype, int cms, int critical, string Job_Code, int? Is_Tech_Required)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@USERID", userid ),
                   new System.Data.SqlClient.SqlParameter("@SYSTEM_ID", systemid ),
                   new System.Data.SqlClient.SqlParameter("@SUBSYSTEM_ID", subsystemid),
                   new System.Data.SqlClient.SqlParameter("@VESSEL_ID", vesselid),
                   new System.Data.SqlClient.SqlParameter("@DEPT_ID", deptid),
                   new System.Data.SqlClient.SqlParameter("@RANK_ID",rankid),
                   new System.Data.SqlClient.SqlParameter("@JOBTITLE",jobtitle),
                   new System.Data.SqlClient.SqlParameter("@JOBDESCRIPTION", jobdesc ),
                   new System.Data.SqlClient.SqlParameter("@FREQUENCY",frequency),
                   new System.Data.SqlClient.SqlParameter("@FREQUENCY_TYPE",frequencytype),
                   new System.Data.SqlClient.SqlParameter("@CMS",cms),
                   new System.Data.SqlClient.SqlParameter("@CRITICAL",critical),
                   new System.Data.SqlClient.SqlParameter("@Job_Code",Job_Code),
                   new System.Data.SqlClient.SqlParameter("@Is_Tech_Required",Is_Tech_Required),
                   
                   new System.Data.SqlClient.SqlParameter("@Return", SqlDbType.Int),
            };
            obj[obj.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "TEC_PR_JOB_INSERT", obj);
            return Convert.ToInt32(obj[obj.Length - 1].Value.ToString());
        }

        public int LibraryJobSaveFromOtherVessel(int userid, string tosystemcode, int tosubsystemid, int tovesselid, int deptid, int rankid, string jobtitle
            , string jobdesc, int frequency, int frequencytype, int cms, int critical, int? safetyalarm, int? calibration)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@USERID", userid ),
                   new System.Data.SqlClient.SqlParameter("@TO_SYSTEM_CODE", tosystemcode ),
                   new System.Data.SqlClient.SqlParameter("@TO_SUBSYSTEM_ID", tosubsystemid),
                   new System.Data.SqlClient.SqlParameter("@TO_VESSEL_ID", tovesselid),
                   new System.Data.SqlClient.SqlParameter("@DEPT_ID", deptid),
                   new System.Data.SqlClient.SqlParameter("@RANK_ID",rankid),
                   new System.Data.SqlClient.SqlParameter("@JOBTITLE",jobtitle),
                   new System.Data.SqlClient.SqlParameter("@JOBDESCRIPTION", jobdesc ),
                   new System.Data.SqlClient.SqlParameter("@FREQUENCY",frequency),
                   new System.Data.SqlClient.SqlParameter("@FREQUENCY_TYPE",frequencytype),
                   new System.Data.SqlClient.SqlParameter("@CMS",cms),
                   new System.Data.SqlClient.SqlParameter("@CRITICAL",critical),
                   new System.Data.SqlClient.SqlParameter("@SAFETYALARM",safetyalarm),
                   new System.Data.SqlClient.SqlParameter("@CALIBRATION",calibration),
            };
            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "TEC_PR_JOB_INSERT_FROM_OTHER_VESSEL", obj);

        }

        /// <summary>
        /// Added by reshma : parameter added for RA : Is_RAMandatory & Is_RAApproval
        /// </summary>
        /// <param name="userid">user Id </param>
        /// <param name="tosystemcode">system id for specific job</param>
        /// <param name="tosubsystemid">sub system id for specific job</param>
        /// <param name="tovesselid">vessel id</param>
        /// <param name="deptid">Department id for specific job</param>
        /// <param name="rankid">rank id</param>
        /// <param name="jobtitle">job title</param>
        /// <param name="jobdesc">job description</param>
        /// <param name="frequency">check frequency</param>
        /// <param name="frequencytype">check frequency type</param>
        /// <param name="cms">check if job is CMS or not</param>
        /// <param name="critical">check if job is critical or not</param>
        /// <param name="Job_Code"> job code</param>
        /// <param name="Is_Tech_Required"></param>
        /// <param name="Is_SafetyAlarm">check for SafetyAlarm</param>
        /// <param name="Is_Calibration">check for calibration</param>
        /// <param name="Is_RAMandatory">check if RA is mandatory or not</param>
        /// <param name="Is_RAApproval">check if RA form is approve by office or not</param>
        /// <returns></returns>
        public int LibraryJobSaveFromOtherVessel(int userid, string tosystemcode, int tosubsystemid, int tovesselid, int deptid, int rankid, string jobtitle
           , string jobdesc, int frequency, int frequencytype, int cms, int critical, int? safetyalarm, int? calibration, int? Is_RAMandatory, int? Is_RAApproval)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@USERID", userid ),
                   new System.Data.SqlClient.SqlParameter("@TO_SYSTEM_CODE", tosystemcode ),
                   new System.Data.SqlClient.SqlParameter("@TO_SUBSYSTEM_ID", tosubsystemid),
                   new System.Data.SqlClient.SqlParameter("@TO_VESSEL_ID", tovesselid),
                   new System.Data.SqlClient.SqlParameter("@DEPT_ID", deptid),
                   new System.Data.SqlClient.SqlParameter("@RANK_ID",rankid),
                   new System.Data.SqlClient.SqlParameter("@JOBTITLE",jobtitle),
                   new System.Data.SqlClient.SqlParameter("@JOBDESCRIPTION", jobdesc ),
                   new System.Data.SqlClient.SqlParameter("@FREQUENCY",frequency),
                   new System.Data.SqlClient.SqlParameter("@FREQUENCY_TYPE",frequencytype),
                   new System.Data.SqlClient.SqlParameter("@CMS",cms),
                   new System.Data.SqlClient.SqlParameter("@CRITICAL",critical),
                   new System.Data.SqlClient.SqlParameter("@SAFETYALARM",safetyalarm),
                   new System.Data.SqlClient.SqlParameter("@CALIBRATION",calibration),
                   new System.Data.SqlClient.SqlParameter("@Is_RAMandatory",Is_RAMandatory),
                   new System.Data.SqlClient.SqlParameter("@Is_RAApproval",Is_RAApproval),
            };
            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "TEC_PR_JOB_INSERT_FROM_OTHER_VESSEL", obj);

        }

        public int LibraryJobMoveFromOtherSubSystem(int userid, string tosystemcode, int tosubsystemid, int jobid)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@USERID", userid ),
                   new System.Data.SqlClient.SqlParameter("@TO_SYSTEM_CODE", tosystemcode ),
                   new System.Data.SqlClient.SqlParameter("@TO_SUBSYSTEM_ID", tosubsystemid),
                   new System.Data.SqlClient.SqlParameter("@JOBID", jobid),
            };
            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "TEC_PR_JOB_MOVE_FROM_OTHER_SUBSYSTEM", obj);
        }


        public int LibraryJobOverWrite(int userid, int jobid)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@USERID", userid ),   
                new System.Data.SqlClient.SqlParameter("@JOBID", jobid ),
                 
             };
            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "TEC_PR_JOB_OVERWRITE", obj);

        }





        public int LibraryJobUpdate(int userid, int jobsid, int systemid, int subsystemid, int vesselid, int deptid, int rankid, string jobtitle
          , string jobdesc, int frequency, int frequencytype, int cms, int critical, string Job_Code, int? Is_Tech_Required)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@USERID", userid ),
                   new System.Data.SqlClient.SqlParameter("@JOBID", jobsid ),
                   new System.Data.SqlClient.SqlParameter("@SYSTEM_ID", systemid ),
                   new System.Data.SqlClient.SqlParameter("@SUBSYSTEM_ID", subsystemid),
                   new System.Data.SqlClient.SqlParameter("@VESSEL_ID", vesselid),
                   new System.Data.SqlClient.SqlParameter("@DEPT_ID", deptid),
                   new System.Data.SqlClient.SqlParameter("@RANK_ID",rankid),
                   new System.Data.SqlClient.SqlParameter("@JOBTITLE",jobtitle),
                   new System.Data.SqlClient.SqlParameter("@JOBDESCRIPTION", jobdesc ),
                   new System.Data.SqlClient.SqlParameter("@FREQUENCY",frequency),
                   new System.Data.SqlClient.SqlParameter("@FREQUENCY_TYPE",frequencytype),
                   new System.Data.SqlClient.SqlParameter("@CMS",cms),
                   new System.Data.SqlClient.SqlParameter("@CRITICAL",critical),
                   new System.Data.SqlClient.SqlParameter("@Job_Code",Job_Code),
                   new System.Data.SqlClient.SqlParameter("@Is_Tech_Required",Is_Tech_Required),
            };
            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "TEC_PR_JOB_UPDATE", obj);

        }
        public int LibraryJobUpdate(int userid, int jobsid, int systemid, int subsystemid, int vesselid, int deptid, int rankid, string jobtitle
       , string jobdesc, int frequency, int frequencytype, int cms, int critical, string Job_Code, int? Is_Tech_Required, int? Is_SafetyAlarm, int? Is_Calibration)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@USERID", userid ),
                   new System.Data.SqlClient.SqlParameter("@JOBID", jobsid ),
                   new System.Data.SqlClient.SqlParameter("@SYSTEM_ID", systemid ),
                   new System.Data.SqlClient.SqlParameter("@SUBSYSTEM_ID", subsystemid),
                   new System.Data.SqlClient.SqlParameter("@VESSEL_ID", vesselid),
                   new System.Data.SqlClient.SqlParameter("@DEPT_ID", deptid),
                   new System.Data.SqlClient.SqlParameter("@RANK_ID",rankid),
                   new System.Data.SqlClient.SqlParameter("@JOBTITLE",jobtitle),
                   new System.Data.SqlClient.SqlParameter("@JOBDESCRIPTION", jobdesc ),
                   new System.Data.SqlClient.SqlParameter("@FREQUENCY",frequency),
                   new System.Data.SqlClient.SqlParameter("@FREQUENCY_TYPE",frequencytype),
                   new System.Data.SqlClient.SqlParameter("@CMS",cms),
                   new System.Data.SqlClient.SqlParameter("@CRITICAL",critical),
                   new System.Data.SqlClient.SqlParameter("@Job_Code",Job_Code),
                   new System.Data.SqlClient.SqlParameter("@Is_Tech_Required",Is_Tech_Required),
                   new System.Data.SqlClient.SqlParameter("@Is_SafetyAlarm",Is_SafetyAlarm),
                   new System.Data.SqlClient.SqlParameter("@Is_Calibration",Is_Calibration),
            };
            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "TEC_PR_JOB_UPDATE", obj);

        }
        /// <summary>
        /// Added by reshma : parameter added for RA : Is_RAMandatory & Is_RAApproval
        /// </summary>
        /// <param name="userid">user Id </param>
        /// <param name="systemid">system id for specific job</param>
        /// <param name="subsystemid">sub system id for specific job</param>
        /// <param name="vesselid">vessel id</param>
        /// <param name="deptid">Department id for specific job</param>
        /// <param name="rankid">rank id</param>
        /// <param name="jobtitle">job title</param>
        /// <param name="jobdesc">job description</param>
        /// <param name="frequency">check frequency</param>
        /// <param name="frequencytype">check frequency type</param>
        /// <param name="cms">check if job is CMS or not</param>
        /// <param name="critical">check if job is critical or not</param>
        /// <param name="Job_Code"> job code</param>
        /// <param name="Is_Tech_Required"></param>
        /// <param name="Is_SafetyAlarm">check for SafetyAlarm</param>
        /// <param name="Is_Calibration">check for calibration</param>
        /// <param name="Is_RAMandatory">check if RA is mandatory or not</param>
        /// <param name="Is_RAApproval">check if RA form is approve by office or not</param>
        /// <returns></returns>
        public int LibraryJobUpdate(int userid, int jobsid, int systemid, int subsystemid, int vesselid, int deptid, int rankid, string jobtitle
      , string jobdesc, int frequency, int frequencytype, int cms, int critical, string Job_Code, int? Is_Tech_Required, int? Is_SafetyAlarm, int? Is_Calibration, int? Is_RAMandatory, int? Is_RAApproval)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@USERID", userid ),
                   new System.Data.SqlClient.SqlParameter("@JOBID", jobsid ),
                   new System.Data.SqlClient.SqlParameter("@SYSTEM_ID", systemid ),
                   new System.Data.SqlClient.SqlParameter("@SUBSYSTEM_ID", subsystemid),
                   new System.Data.SqlClient.SqlParameter("@VESSEL_ID", vesselid),
                   new System.Data.SqlClient.SqlParameter("@DEPT_ID", deptid),
                   new System.Data.SqlClient.SqlParameter("@RANK_ID",rankid),
                   new System.Data.SqlClient.SqlParameter("@JOBTITLE",jobtitle),
                   new System.Data.SqlClient.SqlParameter("@JOBDESCRIPTION", jobdesc ),
                   new System.Data.SqlClient.SqlParameter("@FREQUENCY",frequency),
                   new System.Data.SqlClient.SqlParameter("@FREQUENCY_TYPE",frequencytype),
                   new System.Data.SqlClient.SqlParameter("@CMS",cms),
                   new System.Data.SqlClient.SqlParameter("@CRITICAL",critical),
                   new System.Data.SqlClient.SqlParameter("@Job_Code",Job_Code),
                   new System.Data.SqlClient.SqlParameter("@Is_Tech_Required",Is_Tech_Required),
                   new System.Data.SqlClient.SqlParameter("@Is_SafetyAlarm",Is_SafetyAlarm),
                   new System.Data.SqlClient.SqlParameter("@Is_Calibration",Is_Calibration),
                   new System.Data.SqlClient.SqlParameter("@Is_RAMandatory",Is_RAMandatory),
                   new System.Data.SqlClient.SqlParameter("@Is_RAApproval",Is_RAApproval),
            };
            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "TEC_PR_JOB_UPDATE", obj);

        }
        public int LibraryJobDelete(int userid, int jobid)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@USERID", userid ),   
                new System.Data.SqlClient.SqlParameter("@JOBID", jobid ),
                 
             };
            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "TEC_PR_JOB_DELETE", obj);

        }

        public int LibraryJobRestore(int userid, int jobid)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@USERID", userid ),   
                new System.Data.SqlClient.SqlParameter("@JOBID", jobid ),
                 
             };
            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "TEC_PR_JOB_RESTORE", obj);

        }




        public DataTable LibraryGetPMSSystemParameterList(string parenttypecode, string searchtext)
        {
            DataTable dt = new DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@PARENT_CODE", parenttypecode), 
                   new System.Data.SqlClient.SqlParameter("@SEARCHTEXT", searchtext),
             };
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "TEC_PR_GETSYSTEM_PARAMETER_LIST", obj);
            dt = ds.Tables[0];
            return dt;

        }



        public DataSet LibraryCatalogueLocationAssignSearch(string systemcode, string SubSystemCode, string searchtext, int vesselid, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {

            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@SYSTEMCODE", systemcode),
                    new SqlParameter("@SUBSYSTEMCODE",SubSystemCode),
                   new System.Data.SqlClient.SqlParameter("@LOCSEARCHTEXT",searchtext),
                   new System.Data.SqlClient.SqlParameter("@VESSELID",vesselid),
                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                 
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                    
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "TEC_PR_CATALOGUE_ASSIGN_LOCATION_SEARCH", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds;

        }
        public DataSet ManageSystemLocationAssignSearch(string systemcode, string SubSystemCode, string searchtext, int vesselid, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {

            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@SYSTEMCODE", systemcode),
                    new SqlParameter("@SUBSYSTEMCODE",SubSystemCode),
                   new System.Data.SqlClient.SqlParameter("@LOCSEARCHTEXT",searchtext),
                   new System.Data.SqlClient.SqlParameter("@VESSELID",vesselid),
                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                 
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                    
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "TEC_PMS_MANAGESYSTEM_ASSIGN_LOCATION_SEARCH", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds;

        }
        public DataSet PMS_Get_AssignLocation(string systemcode, string SubSystemCode, string searchtext, int vesselid, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {

            DataTable dt = new DataTable();

            SqlParameter[] obj = new SqlParameter[] 
            { 
                   new SqlParameter("@SYSTEMCODE", systemcode),
                   new SqlParameter("@SUBSYSTEMCODE",SubSystemCode),
                   new SqlParameter("@LOCSEARCHTEXT",searchtext),
                   new SqlParameter("@VESSELID",vesselid),
                   new SqlParameter("@SORTBY",sortby), 
                   new SqlParameter("@SORTDIRECTION",sortdirection), 
                 
                   new SqlParameter("@PAGENUMBER",pagenumber),
                   new SqlParameter("@PAGESIZE",pagesize),
                   new SqlParameter("@ISFETCHCOUNT",isfetchcount),
                    
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PMS_Get_AssignLocation", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds;

        }
        public int PMS_Update_AssignLocationStatus(string systemcode, string SubSystemCode, string CategoryCode, int ModifiedBy, int? LocationCode, int VesselID)
        {

            DataTable dt = new DataTable();

            SqlParameter[] obj = new SqlParameter[] 
            { 
                   new SqlParameter("@SystemCode", systemcode),
                   new SqlParameter("@SubSystemCode",SubSystemCode),
                   new SqlParameter("@CategoryCode",CategoryCode),
                   new SqlParameter("@ModifiedBy",ModifiedBy),
                  new SqlParameter("@LocationCode",LocationCode),
                   new SqlParameter("@VesselCode",VesselID),
                    
            };
            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PMS_Update_AssignLocationStatus", obj);

        }
        public int LibraryCatalogueLocationAssignSave(int userid, string systemcode, string SubSystemCode, int? locationcode, int vesselcode, string Category_Code)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@USERID", userid),
                   new System.Data.SqlClient.SqlParameter("@SYSTEMCODE", systemcode),
                   new SqlParameter("@SubSystemCode",SubSystemCode),
                   new System.Data.SqlClient.SqlParameter("@LOCATIONID", locationcode),
                   new System.Data.SqlClient.SqlParameter("@VESSELID", vesselcode),
                     new System.Data.SqlClient.SqlParameter("@Category_Code", Category_Code),
                   
                   
            };
            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "TEC_PR_CATALOGUE_ASSIGN_LOCATION_SAVE", obj);
        }

        public int LibraryCatalogueLocationAssignDelete(int userid, int AssignlocationID, int vesselcode)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@USERID", userid),
                   new System.Data.SqlClient.SqlParameter("@ASSGINLOCATIONID", AssignlocationID),
                   new System.Data.SqlClient.SqlParameter("@VESSELID", vesselcode),
                   
            };
            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "TEC_PR_CATALOGUE_ASSIGN_LOCATION_DELETE", obj);
        }


        public DataTable LibraryGetCatalogueLocationAssign(string systemcode, int vesselcode)
        {
            DataTable dt = new DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@SYSTEMCODE", systemcode), 
                   new System.Data.SqlClient.SqlParameter("@VESSELID", vesselcode),
             };
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "TEC_PR_GET_CATALOGUE_LOCATION_ASSIGN", obj);
            dt = ds.Tables[0];
            return dt;

        }





        public DataSet LibraryMachineryInforSearch(string search, string systemcode, string systemdesc, string systemdesc_FilterType, string deptType, int? Dept, int? fleetcode, int? ddlvessel
            , string maker, int? Function, int? Location, string maker_FilterType, int? IsActive, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@SerchText", search),
                   new System.Data.SqlClient.SqlParameter("@SYSTEMCODE", systemcode),
                   new System.Data.SqlClient.SqlParameter("@SYSTEMDESC",systemdesc),
                   new System.Data.SqlClient.SqlParameter("@SYSTEMDESC_FilterType",systemdesc_FilterType),


                   new System.Data.SqlClient.SqlParameter("@DEPTTYPE",deptType),

                   new System.Data.SqlClient.SqlParameter("@Dept",Dept),
                 

                   new System.Data.SqlClient.SqlParameter("@FLEETCODE", fleetcode),
                   new System.Data.SqlClient.SqlParameter("@VESSELCODE",ddlvessel),

                   new System.Data.SqlClient.SqlParameter("@MAKER", maker),
                     new System.Data.SqlClient.SqlParameter("@Function", Function),
                       new System.Data.SqlClient.SqlParameter("@Location", Location),
                   new System.Data.SqlClient.SqlParameter("@MAKER_FilterType", maker_FilterType),

                 

                   new System.Data.SqlClient.SqlParameter("@ISACTIVE",IsActive), 
                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 

                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                    
            };

            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "TEC_PR_Machinery_Information", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds;
        }




        public DataTable GetMachineryInfoPopup(int systemid)
        {
            DataTable dt = new DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@SystemID", systemid), 
                  
             };
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "TEC_PR_Get_Machinery_Popup", obj);
            dt = ds.Tables[0];
            return dt;

        }



        public DataTable LibraryGetJOB_eFORM_MAPPING_SEARCH(string SearchText, int? Vessel_ID, int? Job_ID
            , string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@SearchText", SearchText),
                   new System.Data.SqlClient.SqlParameter("@Vessel_ID",Vessel_ID),
                   new System.Data.SqlClient.SqlParameter("@Job_ID",Job_ID),
                    
                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 

                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
            };

            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            dt = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "TEC_PR_JOB_eFORM_MAPPING_SEARCH", obj).Tables[0];
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return dt;
        }



        public int LibrarySaveJob_eForm_Mapping(int? ID, int? Vessel_ID, int? Job_ID, int? Form_ID, int? ChkStatus, int? UserID)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@ID", ID ),
                new System.Data.SqlClient.SqlParameter("@Vessel_ID", Vessel_ID ),
                new System.Data.SqlClient.SqlParameter("@Job_ID", Job_ID ),
                new System.Data.SqlClient.SqlParameter("@Form_ID", Form_ID),
                new System.Data.SqlClient.SqlParameter("@ChkStatus", ChkStatus),
                new System.Data.SqlClient.SqlParameter("@UserID", UserID),
            };
            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "TEC_PR_JOB_eFORM_MAPPING_SAVE", obj);

        }



        public DataTable LibraryGetJobInstructionAttachment(int VESSEL_ID, int JOB_ID)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@VESSEL_ID", VESSEL_ID),
                   new System.Data.SqlClient.SqlParameter("@JOB_ID", JOB_ID),
            };

            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_PR_Get_JOB_INSTRUCTIONS_ATTACHMENTS", obj).Tables[0];

        }


        public int LibrarySaveJobInstructionAttachment(int VESSEL_ID, int JOB_ID, string ATTACHMENT_NAME, string ATTACHMENT_PATH, int CREATED_BY, int? SIZE)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@VESSEL_ID", VESSEL_ID ),
                   new System.Data.SqlClient.SqlParameter("@JOB_ID", JOB_ID ),
                   new System.Data.SqlClient.SqlParameter("@ATTACHMENT_NAME", ATTACHMENT_NAME),
                   new System.Data.SqlClient.SqlParameter("@ATTACHMENT_PATH", ATTACHMENT_PATH),
                   new System.Data.SqlClient.SqlParameter("@CREATED_BY", CREATED_BY),
                   new System.Data.SqlClient.SqlParameter("@SIZE", SIZE),
            };

            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_PR_INSERT_JOB_INSTRUCTIONS_ATTACHMENTS", obj);

        }

        public int LibraryDeleteJobInstructionAttachment(string ATTACHMENT_PATH, int CREATED_BY)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@ATTACHMENT_PATH", ATTACHMENT_PATH),
                   new System.Data.SqlClient.SqlParameter("@CREATED_BY", CREATED_BY),
            };
            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_PR_Delete_JOB_INSTRUCTIONS_ATTACHMENTS", obj);
        }


        public DataSet Get_Functional_Tree_Data(int Vessel_ID)
        {
            DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "TEC_GET_Functional_Tree_Data", new SqlParameter("@Vessel_ID", Vessel_ID));
            ds.Tables[0].TableName = "function";
            //ds.Tables[1].TableName = "system";
            //ds.Tables[2].TableName = "systemlocation";
            //ds.Tables[3].TableName = "subsystem";
            //ds.Tables[4].TableName = "subsystemlocation";
            return ds;
        }


        public DataTable Get_Functional_Tree_Data(string Function_ID, int Vessel_ID, string Equipment_Type, int? SafetyAlarm, int? Calibration, int? Critical)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@Function", Function_ID),
                 new System.Data.SqlClient.SqlParameter("@Vessel_ID", Vessel_ID),
                 new SqlParameter("@Equipment_Type",Equipment_Type),
                 new System.Data.SqlClient.SqlParameter("@IsSafetyAlarm", SafetyAlarm),
                 new System.Data.SqlClient.SqlParameter("@IsCalibration", Calibration),
                 new System.Data.SqlClient.SqlParameter("@IsCritical", Critical)
            };

            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "TEC_GET_Functional_Tree_Data", obj).Tables[0];

        }
        public DataTable    Get_Functional_Tree_Data_ManageSystem(string Function_ID, int Vessel_ID, string function_Code, string SearchText, string Equipment_Type, int? IsActive, string FormType)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@Function", Function_ID),
                 new System.Data.SqlClient.SqlParameter("@Vessel_ID", Vessel_ID),
                 new SqlParameter("@Equipment_Type",Equipment_Type),
                 new System.Data.SqlClient.SqlParameter("@FilterFuntionCode", function_Code),
                 new SqlParameter("@SearchText",SearchText),
                  new SqlParameter("@IsActive",IsActive),
                  new SqlParameter("@FormType",FormType)
                  
            };

            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "TEC_GET_Functional_Tree_Data_ManageSystem1", obj).Tables[0];

        }
        public DataTable Get_EQP_Planned_Jobs(int Vessel_ID, string SystemID, string SubSystemID, string SystemLocation, string SubSystemLocation)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@Vessel_ID", Vessel_ID),
                   new System.Data.SqlClient.SqlParameter("@SystemID", SystemID),
                   new System.Data.SqlClient.SqlParameter("@SubSystemID", SubSystemID),
                   new System.Data.SqlClient.SqlParameter("@SystemLocation", SystemLocation),
                   new System.Data.SqlClient.SqlParameter("@SubSystemLocation", SubSystemLocation),
            };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "TEC_GET_EQP_Planned_Jobs", obj).Tables[0];

        }


        public DataTable Get_EQP_UnPlanned_Jobs(int Vessel_ID, string SystemID, string SubSystemID, string SystemLocation, string SubSystemLocation)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@Vessel_ID", Vessel_ID),
                   new System.Data.SqlClient.SqlParameter("@SystemID", SystemID),
                   new System.Data.SqlClient.SqlParameter("@SubSystemID", SubSystemID),
                   new System.Data.SqlClient.SqlParameter("@SystemLocation", SystemLocation),
                   new System.Data.SqlClient.SqlParameter("@SubSystemLocation", SubSystemLocation),
            };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "TEC_GET_EQP_UnPlanned_Jobs", obj).Tables[0];

        }

        public DataTable Get_EQP_Requisitions(int Vessel_ID, string SystemID)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@Vessel_ID", Vessel_ID),
                   new System.Data.SqlClient.SqlParameter("@SystemID", SystemID),
                 
            };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "TEC_GET_EQP_Requisitions", obj).Tables[0];

        }

        public DataTable Get_EQP_Run_Hours(int Vessel_ID, string SystemID, string SubSystemID, string SystemLocation, string SubSystemLocation)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@Vessel_ID", Vessel_ID),
                   new System.Data.SqlClient.SqlParameter("@SystemID", SystemID),
                   new System.Data.SqlClient.SqlParameter("@SubSystemID", SubSystemID),
                   new System.Data.SqlClient.SqlParameter("@SystemLocation", SystemLocation),
                   new System.Data.SqlClient.SqlParameter("@SubSystemLocation", SubSystemLocation),
            };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "TEC_GET_EQP_Run_Hours", obj).Tables[0];

        }

        public DataTable Get_EQP_Spare_Consumption(int Vessel_ID, string SystemID, string SubSystemID, string SystemLocation, string SubSystemLocation)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@Vessel_ID", Vessel_ID),
                   new System.Data.SqlClient.SqlParameter("@SystemID", SystemID),
                   new System.Data.SqlClient.SqlParameter("@SubSystemID", SubSystemID),
                   new System.Data.SqlClient.SqlParameter("@SystemLocation", SystemLocation),
                   new System.Data.SqlClient.SqlParameter("@SubSystemLocation", SubSystemLocation),
            };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "TEC_GET_EQP_Spare_Consumption", obj).Tables[0];

        }
        public DataTable Get_EQP_Spare_Consumption_ManageSystem(int Vessel_ID, string SystemID, string SubSystemID)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@Vessel_ID", Vessel_ID),
                   new System.Data.SqlClient.SqlParameter("@SystemID", SystemID),
                   new System.Data.SqlClient.SqlParameter("@SubSystemID", SubSystemID),
                
            };

            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "TEC_GET_EQP_Spare_Consumption_ManageSystem", obj).Tables[0];

        }
        public DataTable Get_Machinery_Details(int Vessel_ID, string SystemID, string SubSystemID)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@Vessel_ID", Vessel_ID),
                   new System.Data.SqlClient.SqlParameter("@SystemID", SystemID),
                   new SqlParameter("@SubSystemID",SubSystemID)
                 
            };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "TEC_GET_Machinery_Details", obj).Tables[0];
        }



        public DataSet Get_Equipment_Location(int Vessel_ID, int SystemID, int? SubSystemID, int SystemLocation, int? SubSystemLocation)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@Vessel_ID", Vessel_ID),
                   new System.Data.SqlClient.SqlParameter("@System_ID", SystemID),
                   new System.Data.SqlClient.SqlParameter("@SubSystem_ID", SubSystemID),
                   new System.Data.SqlClient.SqlParameter("@System_Location_Code", SystemLocation),
                   new System.Data.SqlClient.SqlParameter("@SubSystem_Location_Code", SubSystemLocation),
            };
            DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "TEC_GET_Equipment_Location", obj);
            ds.Tables[0].TableName = "ACTIVELOCATION";
            ds.Tables[1].TableName = "SPARELOCATION";
            return ds;
        }

        public int Upd_Equipment_Replacement(int Vessel_ID, int Location_Code, int Active_Location_ID, int Spare_Location_ID, string Remark, int UserID)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@Vessel_ID", Vessel_ID),
                    new System.Data.SqlClient.SqlParameter("@Location_Code", Location_Code),
                   new System.Data.SqlClient.SqlParameter("@Active_Location_ID", Active_Location_ID),
                   new System.Data.SqlClient.SqlParameter("@Spare_Location_ID", Spare_Location_ID),
                   new System.Data.SqlClient.SqlParameter("@Remark", Remark),
                   new System.Data.SqlClient.SqlParameter("@UserID", UserID),
            };
            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "TEC_UPD_Equipment_Replacement", obj);

        }

        public DataTable Get_EQP_Replacement_History(int Vessel_ID, int Location_Code)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@Vessel_ID", Vessel_ID),
                   new System.Data.SqlClient.SqlParameter("@Location_Code", Location_Code)
                                    
            };


            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "TEC_GET_EQP_Replacement_History", obj).Tables[0];

        }

        public int TEC_MOVE_SYSTEM_SUBSYSTEM(string SystemID_parent, string SystemId_ToMove)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@SystemID_parent", SystemID_parent),
                   new System.Data.SqlClient.SqlParameter("@SystemId_ToMove", SystemId_ToMove)
                                    
            };


            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "TEC_MOVE_SYSTEM_SUBSYSTEM", obj);

        }

        public DataTable Get_Equipment_Replacement_History(int? Vessel_ID, int? Function, int? SystemLocation, int? SubSystemLocation, int? Page_Index, int? Page_Size, ref int Is_Fetch_Count)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new SqlParameter("@Vessel_ID", Vessel_ID),
                   new SqlParameter("@Function", Function),
                   new SqlParameter("@SystemLocation", SystemLocation),
                   new SqlParameter("@SubSystemLocation", SubSystemLocation),

                   new SqlParameter("@PAGE_INDEX", Page_Index),
                   new SqlParameter("@PAGE_SIZE",Page_Size),
                   new SqlParameter("@IS_FETCH_COUNT", Is_Fetch_Count),
                  
            };

            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;

            DataTable dt = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "TEC_GET_Equipment_Replacement_History", obj).Tables[0];
            Is_Fetch_Count = Convert.ToInt32(obj[obj.Length - 1].Value);
            return dt;

        }

        public DataTable Get_EQP_Lib_Planned_Jobs(int Vessel_ID, string SystemID, string SubSystemID, string SystemLocation, string SubSystemLocation)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@Vessel_ID", Vessel_ID),
                   new System.Data.SqlClient.SqlParameter("@SystemID", SystemID),
                   new System.Data.SqlClient.SqlParameter("@SubSystemID", SubSystemID),
                   new System.Data.SqlClient.SqlParameter("@SystemLocation", SystemLocation),
                   new System.Data.SqlClient.SqlParameter("@SubSystemLocation", SubSystemLocation),
            };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "TEC_GET_EQP_Lib_Planned_Jobs", obj).Tables[0];

        }
        public DataTable Get_EQP_Lib_Planned_Jobs_ManageSystem(int Vessel_ID, string SystemID, string SubSystemID)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@Vessel_ID", Vessel_ID),
                   new System.Data.SqlClient.SqlParameter("@SystemID", SystemID),
                   new System.Data.SqlClient.SqlParameter("@SubSystemID", SubSystemID),
                 
            };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "TEC_GET_EQP_Lib_Planned_Jobs_ManageSystem", obj).Tables[0];

        }
        public int Upd_Copy_PMS_Data(int SRC_Vessel_ID, int TRG_Vessel_ID, int UserID, int Retain_Existing)
        {
            SqlParameter[] obj = new SqlParameter[] 
            { 
                   new SqlParameter("@SRC_Vessel_ID", SRC_Vessel_ID),
                   new SqlParameter("@TRG_Vessel_ID", TRG_Vessel_ID),
                   new SqlParameter("@UserID", UserID),
                   new SqlParameter("@Retain_Existing", Retain_Existing) ,
                   new SqlParameter("@return", SqlDbType.Int) 
                
            };

            obj[obj.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "TEC_UPD_COPY_PMS_DATA", obj);
            return Convert.ToInt32(obj[obj.Length - 1].Value);
        }
        //Method to get all system and subsystem based on VesselID


        public DataTable Get_System_SubsystemTreeData(int Vessel_ID)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@Function", DBNull.Value),
                   new System.Data.SqlClient.SqlParameter("@Vessel_ID", Vessel_ID),
                   new System.Data.SqlClient.SqlParameter("@Equipment_Type", DBNull.Value),
            };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "TEC_RunHour_SP_GetSystemSubsytemTreeData", obj).Tables[0];

        }

        public DataTable PMS_Get_DestinationSystemSubsystemTreeData(int Vessel_ID)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@Vessel_ID", Vessel_ID),
            };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "TEC_RunHour_SP_GetSystemSubsytemDestination", obj).Tables[0];
        }

        public DataTable PMS_Get_SystemSubsystemInfo(int SourceID, string SystemType)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@ID", SourceID),
                   new System.Data.SqlClient.SqlParameter("@SystemType", SystemType),
            };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "TEC_RunHour_SP_GetSystemSubsytemInfo", obj).Tables[0];
        }

        public int PMS_INS_EquipmentRunHours(DataTable dt, int UserID, string SourceType)
        {
            SqlParameter[] prm = new SqlParameter[] 
            { 
              new SqlParameter("@EquipmentRunningHourSettings", dt), 
              new SqlParameter("@UserID", UserID),              
              new SqlParameter("@SourceType", SourceType),
              new SqlParameter("@return", SqlDbType.Int) 
            };
            prm[prm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "TEC_RunHour_SP_Ins_EquipmentRunningHours", prm);
            return Convert.ToInt32(prm[prm.Length - 1].Value);
        }

        public DataSet PMS_Get_EquipmentRunningHoursInfo(int VesselID, int SystemID, int ParentSystemID)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@VesselID", VesselID),
                   new System.Data.SqlClient.SqlParameter("@EquipmentID", SystemID),
                   new System.Data.SqlClient.SqlParameter("@ParentEquipmentID", ParentSystemID),
            };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "TEC_RunHour_SP_GetEquipmentRunningHoursInfo", obj);
        }

        public DataTable PMS_GET_CheckIfEquipmentIsDirectlyRunHourBased(int EquipmentID, string EquipmentType)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@EquipmentID", EquipmentID),
                   new System.Data.SqlClient.SqlParameter("@EquipmentType", EquipmentType),
            };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "TEC_RunHour_SP_EquipmentIsDirectlyRunHourBased", obj).Tables[0];
        }

        public DataTable PMS_GET_DestinationLinks(int EquipmentID, string SystemType)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@EquipmentID", EquipmentID),
                   new System.Data.SqlClient.SqlParameter("@EquipmentType", SystemType),
            };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "TEC_RunHour_SP_EquipmentLinks", obj).Tables[0];
        }

        public int PMS_GET_IsJobRunHourBased(int SystemID, int SubSystemID, int VesselID)
        {
            SqlParameter[] prm = new SqlParameter[] 
            { 
               new SqlParameter("@SYSTEMID", SystemID), 
              new SqlParameter("@SUBSYSTEMID", SubSystemID), 
              new SqlParameter("@VESSELID", VesselID),
              new SqlParameter("@return", SqlDbType.Int) 
            };
            prm[prm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "TEC_GET_SP_IsJobRunHourBased", prm);
            return Convert.ToInt32(prm[prm.Length - 1].Value);
        }

        public DataTable PMS_Get_CheckIfSystemExits(int SystemID)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@SystemID", SystemID),
            };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PMS_Get_CheckIfSystemExits", obj).Tables[0];
        }

        public DataTable PMS_Get_CheckIfSubSystemExits(string SubSystemID)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@SubSystemID", SubSystemID),
            };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PMS_Get_CheckIfSubSystemExits", obj).Tables[0];
        }

        public int PMS_Get_IsSystemRunHourBased(int SystemID, int VesselID)
        {
            SqlParameter[] prm = new SqlParameter[] 
            { 
              new SqlParameter("@SYSTEMID", SystemID), 
              new SqlParameter("@VESSELID", VesselID),
              new SqlParameter("@return", SqlDbType.Int) 
            };
            prm[prm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PMS_Get_IsSystemRunHourBased", prm);
            return Convert.ToInt32(prm[prm.Length - 1].Value);
        }

        public DataTable PMS_Get_SingleEquipmentRunningHoursInfo(int VesselID, int SystemID, int ParentID)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@VesselID", VesselID),
                   new System.Data.SqlClient.SqlParameter("@SystemID", SystemID),
                   new System.Data.SqlClient.SqlParameter("@ParentID", ParentID),
            };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PMS_Get_SingleEquipmentInfo", obj).Tables[0];
        }

        /// <summary>
        /// For fetching search function
        /// </summary>
        /// <param name="Search">Search keyword</param>
        /// <param name="ParentType">Parent Code for function</param>
        /// <param name="sortby">For Sorting</param>
        /// <param name="sortdirection">sortdirection</param>
        /// <param name="pagenumber">Give the page no</param>
        /// <param name="pagesize">Give the page size</param>
        /// <param name="isfetchcount">Total fetch count</param>
        /// <returns>Display the Function List using search keyword</returns>
        public DataTable PMS_Get_FunctionBySearch(string Search, int ParentType, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@Search", Search),
                   new System.Data.SqlClient.SqlParameter("@ParentType", ParentType),
                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                    
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PMS_Get_FunctionBySearch", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds.Tables[0];
        }

        /// <summary>
        /// For fetching function list against ParentTpe
        /// </summary>
        /// <param name="ParentType">Parent Code for function</param>
        /// <param name="sortby">For Sorting</param>
        /// <param name="sortdirection">sortdirection</param>
        /// <param name="pagenumber">Give the page no</param>
        /// <param name="pagesize">Give the page size</param>
        /// <param name="isfetchcount">Total fetch count</param>
        /// <returns>Display the Function List using Parent Type</returns>
        
        public DataTable PMS_Get_Function(int ParentType, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            DataTable dt = new DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@ParentType", ParentType),     
                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
             };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PMS_Get_Function", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            dt = ds.Tables[0];
            return dt;

        }
        /// <summary>
        ///  For Add Function
        /// </summary>
        /// <param name="Code">Primary Key</param>
        /// <param name="ParentType">Parent Code for Function</param>
        /// <param name="ShortCode">ShortCode for function</param>
        /// <param name="FunctionName">Description</param>
        /// <param name="userID">UserID</param>
        /// <returns>Insert the function in database</returns>
        public int PMS_Insert_Function(int? Code, int? ParentType, string ShortCode, string FunctionName, int userID)
        {
            try
            {
                SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Code",Code),
                                            new SqlParameter("@ParentType",ParentType),
                                            new SqlParameter("@ShortCode",ShortCode),
                                            new SqlParameter("@FuctionName",FunctionName),
                                            new SqlParameter("@UserID",userID)
                                        };

                return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PMS_Insert_Function", sqlprm);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Bind Function name against Code
        /// </summary>
        /// <param name="Code">Primary key</param>
        /// <returns>Details of specific function for update.</returns>
        public DataSet PMS_Get_BindFunctionName(int Code)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@Code", Code),

            };
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PMS_Get_BindFunctionName", obj);
            return ds;
        }
        
        /// <summary>
        /// For Update Function
        /// </summary>
        /// <param name="Code">Primary Key</param>
        /// <param name="ParentType"></param>
        /// <param name="ShortCode"></param>
        /// <param name="FunctionName"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public int PMS_Update_Function(int? Code, int? ParentType, string ShortCode, string FunctionName, int userID)
        {
            try
            {
                SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Code",Code),
                                            new SqlParameter("@ParentType",ParentType),
                                            new SqlParameter("@ShortCode",ShortCode),
                                            new SqlParameter("@FuctionName",FunctionName),
                                            new SqlParameter("@UserID",userID)
                                        };

                return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PMS_Update_Function", sqlprm);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Delete the function
        /// </summary>
        /// <param name="Code">Primary Key</param>        
        /// <param name="userID">UserID</param>
        /// <returns>Delete the function</returns>
        public int PMS_Delete_Function(int? Code, int userID)
        {
            try
            {
                SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Code",Code),
                                            //new SqlParameter("@ID",Id),
                                           // new SqlParameter("@JOBID",JobId),
                                            new SqlParameter("@UserID",userID)
                                        };

                return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PMS_Delete_Function", sqlprm);
            }
            catch
            {
                throw;
            }
        }
        // Added by Reshma JIT : 11222
        /// <summary>
        /// Delete system & sub system location
        /// </summary>
        /// <param name="SystemLocationID">system location Id</param>
        /// <param name="SubSysLocationID">sub system location id</param>
        /// <param name="VESSELID">vessel id</param>
        /// <returns></returns>
        public int PMS_GET_JobsPerformOnLocation(int? SystemLocationID, int? SubSysLocationID, int VESSELID)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@SystemLocationID", SystemLocationID),
                   new System.Data.SqlClient.SqlParameter("@SubSysLocationID", SubSysLocationID),
                   new System.Data.SqlClient.SqlParameter("@VESSELID", VESSELID),

                   
            };

           
            int isfetchcount  = Convert.ToInt32(SqlHelper.ExecuteScalar(_internalConnection, CommandType.StoredProcedure, "PMS_GET_JobsPerformOnLocation", obj).ToString());
            return isfetchcount; 
          
            //return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PMS_GET_ASSIGN_LOCATION", obj);
            //isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            //return ds.Tables[0];
        }
    }
}
