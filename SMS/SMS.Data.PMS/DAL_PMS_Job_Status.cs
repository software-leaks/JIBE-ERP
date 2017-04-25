using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using SMS.Data;


namespace SMS.Data.PMS
{
    public class DAL_PMS_Job_Status
    {
        private string _internalConnection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;

        public DAL_PMS_Job_Status()
        {

        }

        public DataTable TecGetJobsSubCatalogue()
        {
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "TEC_PR_GET_Jobs_SubCatalogue").Tables[0];
        }


        public DataTable TecGet_Locations_SubCatalogue(int? locationID, int? VesselID)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                 new System.Data.SqlClient.SqlParameter("@LocationID", locationID),
                 new System.Data.SqlClient.SqlParameter("@VesselID", VesselID)
            };

            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "TEC_PR_GET_Locations_SubCatalogue", obj).Tables[0];


        }



        public DataTable TecGet_JobStatus_Locations_SubCatalogue(DataTable DTCF_LOCATIONID, DataTable DTCF_VESSELID)
        {


            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                 new System.Data.SqlClient.SqlParameter("@DTCF_LOCATIONID", DTCF_LOCATIONID),
                 new System.Data.SqlClient.SqlParameter("@DTCF_VESSELID", DTCF_VESSELID)
            };

            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "TEC_PR_GET_JOBSTATUS_LOCATIONS_SUBCATALOGUE_BY_CUSTOM_FILTER", obj).Tables[0];

        }


        public DataTable TecGet_JobHistory_Locations_SubCatalogue(DataTable DTCF_LOCATIONID, DataTable DTCF_VESSELID)
        {


            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                 new System.Data.SqlClient.SqlParameter("@DTCF_LOCATIONID", DTCF_LOCATIONID),
                 new System.Data.SqlClient.SqlParameter("@DTCF_VESSELID", DTCF_VESSELID)
            };

            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "TEC_PR_GET_JOBHISTORY_LOCATIONS_SUBCATALOGUE_BY_CUSTOM_FILTER", obj).Tables[0];

        }




        public DataSet TecJobStatusSearch(int? fleetcode, int? vesselid, int? locationid, string systemid, string subsystemid
            , int? jobdeptid, int? rankid, string jobtitlesearchtext, int? jobidsearchtext
            , int? critical, int? cms, DateTime? fromdate, DateTime? todate, string DueDateFlageSearch
            , string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@FLEETCODE", fleetcode),
                   new System.Data.SqlClient.SqlParameter("@VESSELID", vesselid),
                   new System.Data.SqlClient.SqlParameter("@LOCATIONID", locationid), 
                   new System.Data.SqlClient.SqlParameter("@SystemCodeID",systemid),
                   new System.Data.SqlClient.SqlParameter("@SubSystemID",subsystemid),
                   new System.Data.SqlClient.SqlParameter("@DepartmentID",jobdeptid),
                   new System.Data.SqlClient.SqlParameter("@RankID",rankid),
                   new System.Data.SqlClient.SqlParameter("@JOB_TITLE_SEARCH", jobtitlesearchtext),
                   new System.Data.SqlClient.SqlParameter("@JOB_ID_SEARCH", jobidsearchtext),
                   new System.Data.SqlClient.SqlParameter("@CRITICAL",critical),
                   new System.Data.SqlClient.SqlParameter("@CMS", cms),
                   new System.Data.SqlClient.SqlParameter("@FROMDATE", fromdate),
                   new System.Data.SqlClient.SqlParameter("@TODATE", todate),
                   new System.Data.SqlClient.SqlParameter("@DueDateFlageSearch", DueDateFlageSearch),
                  
                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                    
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "TEC_PR_JOB_STATUS_SEARCH", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds;
        }


        public DataSet TecJobStatusSearch(int? fleetcode, DataTable DTCF_VESSELID, DataTable DTCF_LOCATIONID, DataTable DTCF_SUBSYSTEMID
          , DataTable DTCF_DEPARTMENTID, DataTable DTCF_RankID
          , string SearchJobID, string SearchJobTitle, DataTable DTCF_CRITICAL, DataTable DTCF_CMS, DateTime? fromdate, DateTime? todate, string DueDateFlageSearch
          , string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@FLEETCODE", fleetcode),
                
                new System.Data.SqlClient.SqlParameter("@DTCF_VESSELID", DTCF_VESSELID),
                new System.Data.SqlClient.SqlParameter("@DTCF_LOCATIONID", DTCF_LOCATIONID), 
                new System.Data.SqlClient.SqlParameter("@DTCF_SUBSYSTEMID",DTCF_SUBSYSTEMID),
                new System.Data.SqlClient.SqlParameter("@DTCF_DEPARTMENTID",DTCF_DEPARTMENTID),
                new System.Data.SqlClient.SqlParameter("@DTCF_RankID",DTCF_RankID),

                new System.Data.SqlClient.SqlParameter("@SearchJobID", SearchJobID),
                new System.Data.SqlClient.SqlParameter("@SearchJobTitle", SearchJobTitle),

                new System.Data.SqlClient.SqlParameter("@DTCF_CRITICAL",DTCF_CRITICAL),
                new System.Data.SqlClient.SqlParameter("@DTCF_CMS", DTCF_CMS),

                new System.Data.SqlClient.SqlParameter("@FROMDATE", fromdate),
                new System.Data.SqlClient.SqlParameter("@TODATE", todate),

                new System.Data.SqlClient.SqlParameter("@DueDateFlageSearch", DueDateFlageSearch),
                new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                    
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "TEC_PR_JOB_STATUS_SEARCH_BY_CUSTOM_FILTER", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds;
        }


        public DataSet TecJobStatusHistorySearch(int? jobid, int? fleetcode, int? vesselid, int? locationid, string systemid, string subsystemid, int? rankid
            , int? jobdeptid, string jobtitlesearchtext, int? jobidsearchtext
            , int? critical, int? cms, DateTime? fromdate, DateTime? todate
            , string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   
                   new System.Data.SqlClient.SqlParameter("@JOBID", jobid),   
                   new System.Data.SqlClient.SqlParameter("@FLEETCODE", fleetcode),
                   new System.Data.SqlClient.SqlParameter("@VESSELID", vesselid),
                   new System.Data.SqlClient.SqlParameter("@LOCATIONID", locationid), 
                   new System.Data.SqlClient.SqlParameter("@SystemCodeID",systemid),
                   new System.Data.SqlClient.SqlParameter("@SubSystemID",subsystemid),
                   new System.Data.SqlClient.SqlParameter("@DepartmentID",jobdeptid),
                   new System.Data.SqlClient.SqlParameter("@RankID",rankid),
                   new System.Data.SqlClient.SqlParameter("@JOB_TITLE_SEARCH", jobtitlesearchtext),
                   new System.Data.SqlClient.SqlParameter("@JOB_ID_SEARCH", jobidsearchtext),
                   new System.Data.SqlClient.SqlParameter("@CRITICAL",critical),
                   new System.Data.SqlClient.SqlParameter("@CMS", cms),
                   new System.Data.SqlClient.SqlParameter("@FROMDATE", fromdate),
                   new System.Data.SqlClient.SqlParameter("@TODATE", todate),
                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                    
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "TEC_PR_JOB_HISTORY_SEARCH", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds;
        }


        public DataSet TecJobStatusIndex(int? jobid, int? fleetcode, int? VESSELID, int? Function_ID, int? System_ID, int? System_Location_ID, int? SubSystem_ID, int? SubSystem_Location_ID, DataTable DTCF_RANKID, string SearchJobID, string SearchJobTitle, DataTable DTCF_CRITICAL, DataTable DTCF_CMS, DateTime? fromdate, DateTime? todate, int? IsHistory, string DueDateFlageSearch
          , int? SafetyAlarm, int? Calibration, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   
                   new System.Data.SqlClient.SqlParameter("@JOBID", jobid),   
                   new System.Data.SqlClient.SqlParameter("@FLEETCODE", fleetcode),

                   new System.Data.SqlClient.SqlParameter("@VESSELID", VESSELID),
                   new System.Data.SqlClient.SqlParameter("@Function_ID",Function_ID ), 
                   new System.Data.SqlClient.SqlParameter("@System_ID",System_ID),
                   new System.Data.SqlClient.SqlParameter("@System_Location_ID",System_Location_ID),
                   new System.Data.SqlClient.SqlParameter("@SubSystem_ID",SubSystem_ID),
                   new System.Data.SqlClient.SqlParameter("@SubSystem_Location_ID",SubSystem_Location_ID),
                   new System.Data.SqlClient.SqlParameter("@DTCF_RANKID",DTCF_RANKID),
                             
                   new System.Data.SqlClient.SqlParameter("@SearchJobID", SearchJobID),
                   new System.Data.SqlClient.SqlParameter("@SearchJobTitle", SearchJobTitle),
                   
                   new System.Data.SqlClient.SqlParameter("@DTCF_CRITICAL",DTCF_CRITICAL),
                   new System.Data.SqlClient.SqlParameter("@DTCF_CMS", DTCF_CMS),

                   new System.Data.SqlClient.SqlParameter("@FROMDATE", fromdate),
                   new System.Data.SqlClient.SqlParameter("@TODATE", todate),
                   new System.Data.SqlClient.SqlParameter("@DueDateFlageSearch", DueDateFlageSearch),
                   new System.Data.SqlClient.SqlParameter("@SafetyAlarm", SafetyAlarm),
                   new System.Data.SqlClient.SqlParameter("@Calibration", Calibration),

                   new System.Data.SqlClient.SqlParameter("@IsHistory", IsHistory),

                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                    
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "TEC_JOB_STATUS_LIST", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds;
        }

        public DataSet TecJobStatusIndex(int? jobid, int? fleetcode, int? VESSELID, int? Function_ID, int? System_ID, int? System_Location_ID, int? SubSystem_ID, int? SubSystem_Location_ID, DataTable DTCF_RANKID, string SearchJobID, string SearchJobTitle, int? IsCritical, int? IsCMS, DateTime? fromdate, DateTime? todate, DateTime? advfromdate, DateTime? advtodate, int? IsHistory, string DueDateFlageSearch
         , int? PendingOfcVerify, int? SafetyAlarm, int? Calibration, int? PostponeJob, int? FollowupRAdded, int? JobWithMandateRiskAssess, int? JobWithSubRiskAssess, int? JobWithDDock, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   
                   new System.Data.SqlClient.SqlParameter("@JOBID", jobid),   
                   new System.Data.SqlClient.SqlParameter("@FLEETCODE", fleetcode),

                   new System.Data.SqlClient.SqlParameter("@VESSELID", VESSELID),
                   new System.Data.SqlClient.SqlParameter("@Function_ID",Function_ID ), 
                   new System.Data.SqlClient.SqlParameter("@System_ID",System_ID),
                   new System.Data.SqlClient.SqlParameter("@System_Location_ID",System_Location_ID),
                   new System.Data.SqlClient.SqlParameter("@SubSystem_ID",SubSystem_ID),
                   new System.Data.SqlClient.SqlParameter("@SubSystem_Location_ID",SubSystem_Location_ID),
                   new System.Data.SqlClient.SqlParameter("@DTCF_RANKID",DTCF_RANKID),
                             
                   new System.Data.SqlClient.SqlParameter("@SearchJobID", SearchJobID),
                   new System.Data.SqlClient.SqlParameter("@SearchJobTitle", SearchJobTitle),
                   
                   new System.Data.SqlClient.SqlParameter("@IsCritical",IsCritical),
                   new System.Data.SqlClient.SqlParameter("@IsCMS", IsCMS),

                   new System.Data.SqlClient.SqlParameter("@FROMDATE", fromdate),
                   new System.Data.SqlClient.SqlParameter("@TODATE", todate),

                   new System.Data.SqlClient.SqlParameter("@ADVFROMDATE", advfromdate),
                   new System.Data.SqlClient.SqlParameter("@ADVTODATE", advtodate),

                   new System.Data.SqlClient.SqlParameter("@DueDateFlageSearch", DueDateFlageSearch),

                   new System.Data.SqlClient.SqlParameter("@PendingOfcVerify", PendingOfcVerify),

                   new System.Data.SqlClient.SqlParameter("@SafetyAlarm", SafetyAlarm),
                   new System.Data.SqlClient.SqlParameter("@Calibration", Calibration),
               
                   new System.Data.SqlClient.SqlParameter("@PostponedJob", PostponeJob),
                   new System.Data.SqlClient.SqlParameter("@FollowupAdded", FollowupRAdded),
                   new System.Data.SqlClient.SqlParameter("@JobWithMandateRiskAssessment", JobWithMandateRiskAssess),
                   new System.Data.SqlClient.SqlParameter("@JobWithSubmittedRiskAssessment", JobWithSubRiskAssess),
                   new System.Data.SqlClient.SqlParameter("@JobWithDifferDDock", JobWithDDock),
                   
                   new System.Data.SqlClient.SqlParameter("@IsHistory", IsHistory),

                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),                   
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                   
                    
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "TEC_JOB_STATUS_LIST", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds;
        }

        /// <summary>
        /// Bind jobs
        /// </summary>
        /// <param name="VesselId">Vessel Id</param>
        /// <param name="LocationId">location Id</param>
        /// <param name="SystemLocation">system location of system</param>
        /// <param name="SubSystemLocation">sub system location of system</param>
        /// <param name="DepartmentID">Department Id for system</param>
        /// <param name="RankID">Rank Id</param>
        /// <param name="SearchText">Search text like job code or title</param>
        /// <param name="critical">Check for critical jobs</param>
        /// <param name="cms">Check for CMS jobs</param>
        /// <param name="fromdate">from date</param>
        /// <param name="todate">To date</param>
        /// <param name="DueDateFlageSearch">For filter like "This Month","Overdue"</param>
        /// <param name="VerifyFlag">Check for job is verifie or not</param>
        /// <param name="JobFreq">Set frequency for job</param>
        /// <param name="IsSafetyAlarm">check IsSafetyAlarm for job</param>
        /// <param name="IsCalibration">check IsCalibration for job</param>
        /// <param name="sortby">for sorting </param>
        /// <param name="sortdirection">for sortdirection</param>
        /// <param name="pagenumber">used for page number</param>
        /// <param name="pagesize">used for page size</param>
        /// <param name="RHDone">for search RunHours done</param>
        /// <param name="isfetchcount"></param>
        /// <returns></returns>
        public DataSet TecJobStatusIndex(int? jobid, int? fleetcode, int? VESSELID, int? Function_ID, int? System_ID, int? System_Location_ID, int? SubSystem_ID, int? SubSystem_Location_ID, DataTable DTCF_RANKID, string SearchJobID, string SearchJobTitle, int? IsCritical, int?  IsCMS, DateTime? fromdate, DateTime? todate, DateTime? advfromdate, DateTime? advtodate, int? IsHistory, string DueDateFlageSearch
          , int? PendingOfcVerify, int? SafetyAlarm, int? Calibration, int? PostponeJob, int? FollowupRAdded, int? JobWithMandateRiskAssess, int? JobWithSubRiskAssess, int? JobWithDDock, string sortby, int? sortdirection, int? pagenumber, int? pagesize,int? RHDone, ref int isfetchcount)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   
                   new System.Data.SqlClient.SqlParameter("@JOBID", jobid),   
                   new System.Data.SqlClient.SqlParameter("@FLEETCODE", fleetcode),

                   new System.Data.SqlClient.SqlParameter("@VESSELID", VESSELID),
                   new System.Data.SqlClient.SqlParameter("@Function_ID",Function_ID ), 
                   new System.Data.SqlClient.SqlParameter("@System_ID",System_ID),
                   new System.Data.SqlClient.SqlParameter("@System_Location_ID",System_Location_ID),
                   new System.Data.SqlClient.SqlParameter("@SubSystem_ID",SubSystem_ID),
                   new System.Data.SqlClient.SqlParameter("@SubSystem_Location_ID",SubSystem_Location_ID),
                   new System.Data.SqlClient.SqlParameter("@DTCF_RANKID",DTCF_RANKID),
                             
                   new System.Data.SqlClient.SqlParameter("@SearchJobID", SearchJobID),
                   new System.Data.SqlClient.SqlParameter("@SearchJobTitle", SearchJobTitle),
                   
                   new System.Data.SqlClient.SqlParameter("@IsCritical",IsCritical),
                   new System.Data.SqlClient.SqlParameter("@IsCMS", IsCMS),

                   new System.Data.SqlClient.SqlParameter("@FROMDATE", fromdate),
                   new System.Data.SqlClient.SqlParameter("@TODATE", todate),

                   new System.Data.SqlClient.SqlParameter("@ADVFROMDATE", advfromdate),
                   new System.Data.SqlClient.SqlParameter("@ADVTODATE", advtodate),

                   new System.Data.SqlClient.SqlParameter("@DueDateFlageSearch", DueDateFlageSearch),

                   new System.Data.SqlClient.SqlParameter("@PendingOfcVerify", PendingOfcVerify),

                   new System.Data.SqlClient.SqlParameter("@SafetyAlarm", SafetyAlarm),
                   new System.Data.SqlClient.SqlParameter("@Calibration", Calibration),
               
                   new System.Data.SqlClient.SqlParameter("@PostponedJob", PostponeJob),
                   new System.Data.SqlClient.SqlParameter("@FollowupAdded", FollowupRAdded),
                   new System.Data.SqlClient.SqlParameter("@JobWithMandateRiskAssessment", JobWithMandateRiskAssess),
                   new System.Data.SqlClient.SqlParameter("@JobWithSubmittedRiskAssessment", JobWithSubRiskAssess),
                   new System.Data.SqlClient.SqlParameter("@JobWithDifferDDock", JobWithDDock),
                   
                   new System.Data.SqlClient.SqlParameter("@IsHistory", IsHistory),

                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                    new System.Data.SqlClient.SqlParameter("@RHDone",RHDone),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                   
                    
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "TEC_JOB_STATUS_LIST", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds;
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
        /// <param name="sortby">for sorting </param>
        /// <param name="sortdirection">for sortdirection</param>
        /// <param name="pagenumber">used for page number</param>
        /// <param name="pagesize">used for page size</param>
        /// <param name="RHDone">for search RunHours done</param>
        /// <param name="isfetchcount"></param>
        /// <returns></returns>
        public DataSet TecJobStatusIndex(int? jobid, int? fleetcode, int? VESSELID, int? Function_ID, int? System_ID, int? System_Location_ID, int? SubSystem_ID, int? SubSystem_Location_ID, DataTable DTCF_RANKID, string SearchJobID, string SearchJobTitle, int? IsCritical, int? IsCMS, DateTime? fromdate, DateTime? todate, DateTime? advfromdate, DateTime? advtodate, int? IsHistory, string DueDateFlageSearch
          , int? PendingOfcVerify, int? SafetyAlarm, int? Calibration, int? PostponeJob, int? FollowupRAdded, int? JobWithMandateRiskAssess, int? JobWithSubRiskAssess, int? JobWithDDock, string sortby, int? sortdirection, int? pagenumber, int? pagesize, int? RHDone, int? Is_RAMandatory, int? Is_RASubmitted, ref int isfetchcount)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   
                   new System.Data.SqlClient.SqlParameter("@JOBID", jobid),   
                   new System.Data.SqlClient.SqlParameter("@FLEETCODE", fleetcode),

                   new System.Data.SqlClient.SqlParameter("@VESSELID", VESSELID),
                   new System.Data.SqlClient.SqlParameter("@Function_ID",Function_ID ), 
                   new System.Data.SqlClient.SqlParameter("@System_ID",System_ID),
                   new System.Data.SqlClient.SqlParameter("@System_Location_ID",System_Location_ID),
                   new System.Data.SqlClient.SqlParameter("@SubSystem_ID",SubSystem_ID),
                   new System.Data.SqlClient.SqlParameter("@SubSystem_Location_ID",SubSystem_Location_ID),
                   new System.Data.SqlClient.SqlParameter("@DTCF_RANKID",DTCF_RANKID),
                             
                   new System.Data.SqlClient.SqlParameter("@SearchJobID", SearchJobID),
                   new System.Data.SqlClient.SqlParameter("@SearchJobTitle", SearchJobTitle),
                   
                   new System.Data.SqlClient.SqlParameter("@IsCritical",IsCritical),
                   new System.Data.SqlClient.SqlParameter("@IsCMS", IsCMS),

                   new System.Data.SqlClient.SqlParameter("@FROMDATE", fromdate),
                   new System.Data.SqlClient.SqlParameter("@TODATE", todate),

                   new System.Data.SqlClient.SqlParameter("@ADVFROMDATE", advfromdate),
                   new System.Data.SqlClient.SqlParameter("@ADVTODATE", advtodate),

                   new System.Data.SqlClient.SqlParameter("@DueDateFlageSearch", DueDateFlageSearch),

                   new System.Data.SqlClient.SqlParameter("@PendingOfcVerify", PendingOfcVerify),

                   new System.Data.SqlClient.SqlParameter("@SafetyAlarm", SafetyAlarm),
                   new System.Data.SqlClient.SqlParameter("@Calibration", Calibration),
               
                   new System.Data.SqlClient.SqlParameter("@PostponedJob", PostponeJob),
                   new System.Data.SqlClient.SqlParameter("@FollowupAdded", FollowupRAdded),
                   new System.Data.SqlClient.SqlParameter("@JobWithMandateRiskAssessment", JobWithMandateRiskAssess),
                   new System.Data.SqlClient.SqlParameter("@JobWithSubmittedRiskAssessment", JobWithSubRiskAssess),
                   new System.Data.SqlClient.SqlParameter("@JobWithDifferDDock", JobWithDDock),
                   
                   new System.Data.SqlClient.SqlParameter("@IsHistory", IsHistory),

                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@RHDone",RHDone),
                   new System.Data.SqlClient.SqlParameter("@Is_RAMandatory",Is_RAMandatory),
                   new System.Data.SqlClient.SqlParameter("@Is_RAApproval",Is_RASubmitted),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                   
                    
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "TEC_JOB_STATUS_LIST", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds;
        }

        public DataSet TecMachineryRunningHoursSearch(int? fleetcode, int? vesselid, int? locationid, DateTime? fromdate, DateTime? todate, string displayrecordtype
         , string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@FLEETCODE", fleetcode),
                   new System.Data.SqlClient.SqlParameter("@VESSELID", vesselid),
                   new System.Data.SqlClient.SqlParameter("@LOCATIONID", locationid), 
                   new System.Data.SqlClient.SqlParameter("@FROMDATE", fromdate),
                   new System.Data.SqlClient.SqlParameter("@TODATE", todate),
                   new System.Data.SqlClient.SqlParameter("@DISPLAYRECORDTYPE", displayrecordtype),
                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "TEC_PR_Machine_RHour_Search", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds;
        }


        public DataSet TecMachineryRunningHoursSearch(int? fleetcode, int? VESSELID, DateTime? FROMREADDATE, DateTime? TOREADDATE
            , DateTime? FROMCREATEDDATE, DateTime? TOCREATEDDATE
            , string displayrecordtype, int? Function_ID, string System_ID, int? System_Location_ID, int? SubSystem_ID, int? SubSystem_Location_ID, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@FLEETCODE", fleetcode),
                   new System.Data.SqlClient.SqlParameter("@VESSELID", VESSELID),
                   //new System.Data.SqlClient.SqlParameter("@LOCATIONID", LOCATIONID), 
                   //new System.Data.SqlClient.SqlParameter("@SUBSYSTEM_LOCATIONID", SUBSYSTEM_LOCATIONID),

                   new System.Data.SqlClient.SqlParameter("@FROMREADDATE", FROMREADDATE),
                   new System.Data.SqlClient.SqlParameter("@TOREADDATE", TOREADDATE),
                
                   new System.Data.SqlClient.SqlParameter("@FROMCREATEDDATE", FROMCREATEDDATE),
                   new System.Data.SqlClient.SqlParameter("@TOCREATEDDATE", TOCREATEDDATE),

                   new System.Data.SqlClient.SqlParameter("@DISPLAYRECORDTYPE", displayrecordtype),
                   new System.Data.SqlClient.SqlParameter("@Function_ID",Function_ID ), 
                   new System.Data.SqlClient.SqlParameter("@System_ID",System_ID),
                   new System.Data.SqlClient.SqlParameter("@System_Location_ID",System_Location_ID),
                   new System.Data.SqlClient.SqlParameter("@SubSystem_ID",SubSystem_ID),
                   new System.Data.SqlClient.SqlParameter("@SubSystem_Location_ID",SubSystem_Location_ID),
                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "TEC_PR_MACHINE_RHOUR_SEARCH_BY_CUSTOM_FILTER", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds;
        }

        /// <summary>
        /// To get list of machinery running hours conditionally on selected filters.
        /// </summary>
        /// <param name="fleetcode">Selected id fleet</param>
        /// <param name="VESSELID">Selected Id of Vessel</param>
        /// <param name="FROMREADDATE"></param>
        /// <param name="TOREADDATE"></param>
        /// <param name="FROMCREATEDDATE"></param>
        /// <param name="TOCREATEDDATE"></param>
        /// <param name="displayrecordtype">Dispaly type : History/Current</param>
        /// <param name="Function_ID">Id of selected function</param>
        /// <param name="System_ID">Id of selected system</param>
        /// <param name="System_Location_ID">Id of system location for selected system</param>
        /// <param name="SubSystem_ID">Id of sub system</param>
        /// <param name="SubSystem_Location_ID">Id of subsystem location for selected sub-system</param>
        /// <param name="sortby"></param>
        /// <param name="sortdirection">Sort direction|| ASC ||DESC </param>
        /// <param name="pagenumber"></param>
        /// <param name="pagesize">no. of rows displyed per page</param>
        /// <param name="isfetchcount">no. of rows fetched </param>
        /// <param name="DisplayInheritingCounters">to display linked systems / sub-systems</param>
        /// <returns>List of Machinery running hours details</returns>
        public DataSet TecMachineryRunningHoursSearch(int? fleetcode, int? VESSELID, DateTime? FROMREADDATE, DateTime? TOREADDATE, DateTime? FROMCREATEDDATE, DateTime? TOCREATEDDATE, 
                                                      string displayrecordtype, int? Function_ID, string System_ID, int? System_Location_ID, int? SubSystem_ID, int? SubSystem_Location_ID, 
                                                      string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount, int DisplayInheritingCounters)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@FLEETCODE", fleetcode),
                   new System.Data.SqlClient.SqlParameter("@VESSELID", VESSELID),

                   new System.Data.SqlClient.SqlParameter("@FROMREADDATE", FROMREADDATE),
                   new System.Data.SqlClient.SqlParameter("@TOREADDATE", TOREADDATE),
                
                   new System.Data.SqlClient.SqlParameter("@FROMCREATEDDATE", FROMCREATEDDATE),
                   new System.Data.SqlClient.SqlParameter("@TOCREATEDDATE", TOCREATEDDATE),

                   new System.Data.SqlClient.SqlParameter("@DISPLAYRECORDTYPE", displayrecordtype),
                   new System.Data.SqlClient.SqlParameter("@Function_ID",Function_ID ), 
                   new System.Data.SqlClient.SqlParameter("@System_ID",System_ID),
                   new System.Data.SqlClient.SqlParameter("@System_Location_ID",System_Location_ID),
                   new System.Data.SqlClient.SqlParameter("@SubSystem_ID",SubSystem_ID),
                   new System.Data.SqlClient.SqlParameter("@SubSystem_Location_ID",SubSystem_Location_ID),
                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                   new System.Data.SqlClient.SqlParameter("@DisplayInheritingCounters",DisplayInheritingCounters),
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "TEC_PR_MACHINE_RHOUR_SEARCH_BY_CUSTOM_FILTER", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds;
        }


        public DataSet TecJobOverDueSearch(int? fleetcode, int? vesselid, int? locationid, string systemid, string subsystemid
           , int? jobdeptid, int? rankid, string jobtitlesearchtext, int? jobidsearchtext
           , int? critical, int? cms, DateTime? fromdate, DateTime? todate, int? month, int? year, string status
           , string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@FLEETCODE", fleetcode),
                   new System.Data.SqlClient.SqlParameter("@DTCF_VESSELID", vesselid),
                   new System.Data.SqlClient.SqlParameter("@LOCATIONID", locationid), 
                   new System.Data.SqlClient.SqlParameter("@SystemCodeID",systemid),
                   new System.Data.SqlClient.SqlParameter("@SubSystemID",subsystemid),
                   new System.Data.SqlClient.SqlParameter("@DepartmentID",jobdeptid),
                   new System.Data.SqlClient.SqlParameter("@RankID",rankid),
                   new System.Data.SqlClient.SqlParameter("@JOB_TITLE_SEARCH", jobtitlesearchtext),
                   new System.Data.SqlClient.SqlParameter("@JOB_ID_SEARCH", jobidsearchtext),
                   new System.Data.SqlClient.SqlParameter("@CRITICAL",critical),
                   new System.Data.SqlClient.SqlParameter("@CMS", cms),
                   new System.Data.SqlClient.SqlParameter("@FROMDATE", fromdate),
                   new System.Data.SqlClient.SqlParameter("@TODATE", todate),
                   new System.Data.SqlClient.SqlParameter("@MONTH", month),
                   new System.Data.SqlClient.SqlParameter("@YEAR", year),
                   new System.Data.SqlClient.SqlParameter("@STATUS", status),
                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "TEC_PR_JOB_OVERDUE_SEARCH", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds;
        }



        public DataSet TecJobOverDueSearch(int? fleetcode, DataTable DTCF_VESSELID, DataTable DTCF_LOCATIONID, DataTable DTCF_SUBSYSTEMID
            , int? JOBDEPTID, DataTable DTCF_RankID, string JOB_TITLE_SEARCH, string TYCF_JOB_TITLE_SEARCH, int? JOB_ID_SEARCH, string TYCF_JOB_ID_SEARCH
            , DataTable DTCF_CRITICAL, DataTable DTCF_CMS, DateTime? FROMDATE, DateTime? TODATE, int? MONTH, int? YEAR, string STATUS
            , string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                    
                new System.Data.SqlClient.SqlParameter("@FLEETCODE", fleetcode),
                new System.Data.SqlClient.SqlParameter("@DTCF_VESSELID", DTCF_VESSELID),
                new System.Data.SqlClient.SqlParameter("@DTCF_LOCATIONID", DTCF_LOCATIONID), 
                new System.Data.SqlClient.SqlParameter("@DTCF_SUBSYSTEMID",DTCF_SUBSYSTEMID),
                new System.Data.SqlClient.SqlParameter("@DEPARTMENTID",JOBDEPTID),
                new System.Data.SqlClient.SqlParameter("@DTCF_RankID",DTCF_RankID),
                new System.Data.SqlClient.SqlParameter("@JOB_TITLE_SEARCH", JOB_TITLE_SEARCH),
                new System.Data.SqlClient.SqlParameter("@TYCF_JOB_TITLE_SEARCH", TYCF_JOB_TITLE_SEARCH),
                new System.Data.SqlClient.SqlParameter("@JOB_ID_SEARCH", JOB_ID_SEARCH),
                new System.Data.SqlClient.SqlParameter("@TYCF_JOB_ID_SEARCH", TYCF_JOB_ID_SEARCH),
                new System.Data.SqlClient.SqlParameter("@DTCF_CRITICAL",DTCF_CRITICAL),
                new System.Data.SqlClient.SqlParameter("@DTCF_CMS", DTCF_CMS),
                new System.Data.SqlClient.SqlParameter("@FROMDATE", FROMDATE),
                new System.Data.SqlClient.SqlParameter("@TODATE", TODATE),
                new System.Data.SqlClient.SqlParameter("@MONTH", MONTH),
                new System.Data.SqlClient.SqlParameter("@YEAR", YEAR),
                new System.Data.SqlClient.SqlParameter("@STATUS", STATUS),

                new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "TEC_PR_JOB_OVERDUE_SEARCH_BY_CUSTOM_FILTER", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds;
        }




        public DataSet TecJobsOverdueHistorySearch(int? jobid, int? fleetcode, int? vesselid, int? locationid, string systemid, string subsystemid
            , int? rankid, int? jobdeptid, string jobtitlesearchtext, int? jobidsearchtext
            , int? critical, int? cms, DateTime? fromdate, DateTime? todate
            , string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   
                   new System.Data.SqlClient.SqlParameter("@JOBID", jobid),   
                   new System.Data.SqlClient.SqlParameter("@FLEETCODE", fleetcode),
                   new System.Data.SqlClient.SqlParameter("@VESSELID", vesselid),
                   new System.Data.SqlClient.SqlParameter("@LOCATIONID", locationid), 
                   new System.Data.SqlClient.SqlParameter("@SystemCodeID",systemid),
                   new System.Data.SqlClient.SqlParameter("@SubSystemID",subsystemid),
                   new System.Data.SqlClient.SqlParameter("@DepartmentID",jobdeptid),
                   new System.Data.SqlClient.SqlParameter("@RankID",rankid),
                   new System.Data.SqlClient.SqlParameter("@JOB_TITLE_SEARCH", jobtitlesearchtext),
                   new System.Data.SqlClient.SqlParameter("@JOB_ID_SEARCH", jobidsearchtext),
                   new System.Data.SqlClient.SqlParameter("@CRITICAL",critical),
                   new System.Data.SqlClient.SqlParameter("@CMS", cms),
                   new System.Data.SqlClient.SqlParameter("@FROMDATE", fromdate),
                   new System.Data.SqlClient.SqlParameter("@TODATE", todate),
                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                    
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "TEC_PR_JOB_OVERDUE_HISTORY_SEARCH", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds;
        }


        public DataSet TecJobsOverdueList(int joboverdueid)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            {
                new System.Data.SqlClient.SqlParameter("@JOBOVERDUE_ID", joboverdueid)  
            };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "TEC_PR_JOB_OVERDUE_LIST", obj);

        }


        public int TecJobsOverdueResponse(int userid, int joboverdueid, DateTime modifiedcompletiondate, string suptdresponse, int vesselcode)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@USERID", userid),
                   new System.Data.SqlClient.SqlParameter("@JOBOVERDUE_ID", joboverdueid),
                   new System.Data.SqlClient.SqlParameter("@MODIFIYCOMPLETIONDATE", modifiedcompletiondate),
                   new System.Data.SqlClient.SqlParameter("@SUPTD_RESPONSE", suptdresponse),
                   new System.Data.SqlClient.SqlParameter("@VESSEL_ID", vesselcode),
                   
            };
            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "TEC_PR_OVERDUE_JOB_RESPONSED", obj);
        }




        public DataSet TecJobsSparesItemUsedSearch(int? vesselid, int? jobid, int? jobhistoryid, string systemid, string subsystemid, string itemsearchtext
        , string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {

            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@VESSELID", vesselid),
                   new System.Data.SqlClient.SqlParameter("@JOBID", jobid),   
                   new System.Data.SqlClient.SqlParameter("@HISTORYID", jobhistoryid),
                   new System.Data.SqlClient.SqlParameter("@SYSTEMID", systemid),
                   new System.Data.SqlClient.SqlParameter("@SUBSYSTEMID", subsystemid),
                   new System.Data.SqlClient.SqlParameter("@ItemSearchText", itemsearchtext), 

                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                    
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "TEC_PR_SPARES_USED_SEARCH", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds;

        }


        public DataSet TecJobDoneNotDoneSummarySearch(int? fleetcode, int? VesselId, int? RankCategory, string SearchText, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {

            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@FLEETCODE", fleetcode),
                   new System.Data.SqlClient.SqlParameter("@VesselId", VesselId),
                   new System.Data.SqlClient.SqlParameter("@RankCategory", RankCategory),   
                   new System.Data.SqlClient.SqlParameter("@SearchText", SearchText),
                   
                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "TEC_PR_JOB_DONE_NOT_DONE_SUMMARY_SEARCH", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds;

        }



        public DataSet TecJobDoneNotDoneSummarySearch(int? fleetcode, int? vesselid, DataTable DTCF_RANKID, int? StaffCode, string TYCF_StaffCode, string StaffName, string TYCF_StaffName
            , string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {

            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@FLEETCODE", fleetcode),
                   new System.Data.SqlClient.SqlParameter("@VESSELID", vesselid),
                   new System.Data.SqlClient.SqlParameter("@DTCF_RANKID", DTCF_RANKID),   

                   new System.Data.SqlClient.SqlParameter("@StaffCode", StaffCode),
                   new System.Data.SqlClient.SqlParameter("@TYCF_StaffCode", TYCF_StaffCode),
                   new System.Data.SqlClient.SqlParameter("@StaffName", StaffName),
                   new System.Data.SqlClient.SqlParameter("@TYCF_StaffName", TYCF_StaffName),
                   
                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "TEC_PR_JOB_DONE_NOT_DONE_SUMMARY_SEARCH_BY_CUSTOM_FILTER", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds;

        }

        public DataSet TecJobDoneVesselWiseSummarySearch(int? fleetcode, int? vesselid, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {

            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@FLEETCODE", fleetcode),
                   new System.Data.SqlClient.SqlParameter("@VESSELID", vesselid),

                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "TEC_PR_JOB_DONE_Vessel_Wise_Summary", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds;

        }

        public DataSet TecJobDailyUpdatingSummarySearch(int? fleetcode, DateTime? fromdate, DateTime? todate)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@FLEETCODE", fleetcode),
                   new System.Data.SqlClient.SqlParameter("@FROMDATE", fromdate),
                   new System.Data.SqlClient.SqlParameter("@TODATE", todate),
            };
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "TEC_PR_JOB_SEARCH_DAILY_UPDATING_SUMMARY", obj);
            return ds;
        }

        public DataTable TecJobGetJobDoneAttachment(int? JobHistoryID, int? VesselID)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@JOB_HISTORY_ID", JobHistoryID),
                new System.Data.SqlClient.SqlParameter("@VESSEL_ID", VesselID),
            };

            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "TEC_PR_JOB_DONE_ATTACHMENTS", obj).Tables[0];

        }


        public DataTable TecJobsGetRanks()
        {

            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "TEC_PR_GET_Jobs_Rank").Tables[0];


        }

        public DataTable Get_Machinery_Location(int? Vessel_ID, int? Run_Hour)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@Vessel_ID", Vessel_ID), 
                   new System.Data.SqlClient.SqlParameter("@Run_Hour", Run_Hour),
                  
            };

            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "TEC_GET_Machinery_Location", obj).Tables[0];
        }
        public DataTable Get_ParentChildLocationLinkage(int Vessel_ID, int ParentID)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@VesselID", Vessel_ID), 
                   new System.Data.SqlClient.SqlParameter("@ParentEquipmentID", ParentID),
                  
            };

            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "TEC_RunHour_SP_GetParentChildLinkage", obj).Tables[0];
        }

        public int AddUpdateAlarmUnit(int UnitID, string UnitName, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { new SqlParameter("@UnitID",UnitID),
                                          new SqlParameter("@UnitName",UnitName),
                                          new SqlParameter("@UserID",UserID)
                                         };
            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "SP_TEC_InsertUpdate_AlarmUnit", sqlprm);
        }
        public int DeleteAlarmUnit(int UnitID, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { new SqlParameter("@UnitID",UnitID),
                                          new SqlParameter("@UserID",UserID)
                                         };
            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "SP_TEC_Del_AlarmUnit", sqlprm);
        }
        public DataSet GetAlarmUnit(string UnitName, int? pagenumber, int? pagesize)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new SqlParameter("@UnitName",UnitName),
                   new SqlParameter("@PageSize",pagesize),
                   new SqlParameter("@PageNumber",pagenumber),
                  
            };

            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "SP_TEC_GET_ALARMUNIT", obj);
        }

        public int AddUpdateAlarmEffect(int EffectID, string EffectName, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { new SqlParameter("@EffectID",EffectID),
                                          new SqlParameter("@EffectName",EffectName),
                                          new SqlParameter("@UserID",UserID)
                                         };
            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "SP_TEC_InsertUpdate_AlarmEffect", sqlprm);
        }

        public DataSet GetAlarmEffect(string EffectName, int? pagenumber, int? pagesize)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new SqlParameter("@EffectName",EffectName),
                   new SqlParameter("@PageSize",pagesize),
                   new SqlParameter("@PageNumber",pagenumber),
                  
                  
            };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "SP_TEC_GET_ALARMEFFECT", obj);
        }

        public int DeleteAlarmEffect(int EffectID, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { new SqlParameter("@EffectID",EffectID),
                                          new SqlParameter("@UserID",UserID)
                                         };
            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "SP_TEC_Del_AlarmEffect", sqlprm);
        }

        #region PMS Historical Overdue Jobs Function Added By Someshwar Dated 11-02-2016

        public DataSet PMS_Get_OverdueJobs(int? jobid, int? fleetcode, DataTable dtVessel, int? Function_ID, int? System_ID, int? System_Location_ID, int? SubSystem_ID, int? SubSystem_Location_ID, DataTable DTCF_RANKID, string SearchJobID, string SearchJobTitle, int IsCritical, int IsCMS, DateTime? fromdate, DateTime? todate, int? IsHistory, int? JobStatus, string DueDateFlageSearch
   , int? SafetyAlarm, int? Calibration, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount, ref int istotaljobcount, ref int isodjobcount, ref int iscritodjobcount)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   
                   new System.Data.SqlClient.SqlParameter("@JOBID", jobid),   
                   new System.Data.SqlClient.SqlParameter("@FLEETCODE", fleetcode),

                   new System.Data.SqlClient.SqlParameter("@VESSELID", dtVessel),
                   new System.Data.SqlClient.SqlParameter("@Function_ID",Function_ID ), 
                   new System.Data.SqlClient.SqlParameter("@System_ID",System_ID),
                   new System.Data.SqlClient.SqlParameter("@System_Location_ID",System_Location_ID),
                   new System.Data.SqlClient.SqlParameter("@SubSystem_ID",SubSystem_ID),
                   new System.Data.SqlClient.SqlParameter("@SubSystem_Location_ID",SubSystem_Location_ID),
                   new System.Data.SqlClient.SqlParameter("@DTCF_RANKID",DTCF_RANKID),
                             
                   new System.Data.SqlClient.SqlParameter("@SearchJobID", SearchJobID),
                   new System.Data.SqlClient.SqlParameter("@SearchJobTitle", SearchJobTitle),
                   
                   new System.Data.SqlClient.SqlParameter("@IsCritical",IsCritical),
                   new System.Data.SqlClient.SqlParameter("@IsCMS", IsCMS),

                   new System.Data.SqlClient.SqlParameter("@FROMDATE", fromdate),
                   new System.Data.SqlClient.SqlParameter("@TODATE", todate),
                   new System.Data.SqlClient.SqlParameter("@DueDateFlageSearch", DueDateFlageSearch),
                   new System.Data.SqlClient.SqlParameter("@SafetyAlarm", SafetyAlarm),
                   new System.Data.SqlClient.SqlParameter("@Calibration", Calibration),

                   new System.Data.SqlClient.SqlParameter("@IsHistory", IsHistory),
                   new System.Data.SqlClient.SqlParameter("@JobStatus", JobStatus),
                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                   new System.Data.SqlClient.SqlParameter("@ISTOTALJOBCOUNT",istotaljobcount),
                   new System.Data.SqlClient.SqlParameter("@ISODJOBCOUNT",isodjobcount),
                   new System.Data.SqlClient.SqlParameter("@ISCRITODJOBCOUNT",iscritodjobcount),
                    
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PMS_Get_OverdueJobs", obj);
            isfetchcount = Convert.ToInt32(ds.Tables[1].Rows[0][0].ToString());  //Convert.ToInt32(obj[obj.Length - 3].Value);
            istotaljobcount = Convert.ToInt32(ds.Tables[2].Rows[0][0].ToString());  //Convert.ToInt32(obj[obj.Length - 3].Value);
            isodjobcount = Convert.ToInt32(ds.Tables[3].Rows[0][0].ToString());//Convert.ToInt32(obj[obj.Length - 2].Value);
            iscritodjobcount = Convert.ToInt32(ds.Tables[4].Rows[0][0].ToString());// Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds;
        }

       
        public DataSet PMS_Get_OverdueJobs(int? jobid, int? fleetcode, DataTable dtVessel, int? Function_ID, int? System_ID, int? System_Location_ID, int? SubSystem_ID, int? SubSystem_Location_ID,
                                           DataTable DTCF_RANKID, string SearchJobID, string SearchJobTitle, int IsCritical, int IsCMS, DateTime? fromdate, DateTime? todate, int? IsHistory,
                                           int? JobStatus, string DueDateFlageSearch, int? PendingOfcVerify, int? SafetyAlarm, int? Calibration, DateTime? advfromdate, DateTime? advtodate, int? PostponeJob, int? FollowupRAdded,
                                           int? JobWithMandateRiskAssess, int? JobWithSubRiskAssess, int? JobWithDDock, string sortby, int? sortdirection, int? pagenumber, int? pagesize,
                                           ref int isfetchcount, ref int istotaljobcount, ref int isodjobcount, ref int iscritodjobcount)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   
                   new System.Data.SqlClient.SqlParameter("@JOBID", jobid),   
                   new System.Data.SqlClient.SqlParameter("@FLEETCODE", fleetcode),

                   new System.Data.SqlClient.SqlParameter("@VESSELID", dtVessel),
                   new System.Data.SqlClient.SqlParameter("@Function_ID",Function_ID ), 
                   new System.Data.SqlClient.SqlParameter("@System_ID",System_ID),
                   new System.Data.SqlClient.SqlParameter("@System_Location_ID",System_Location_ID),
                   new System.Data.SqlClient.SqlParameter("@SubSystem_ID",SubSystem_ID),
                   new System.Data.SqlClient.SqlParameter("@SubSystem_Location_ID",SubSystem_Location_ID),
                   new System.Data.SqlClient.SqlParameter("@DTCF_RANKID",DTCF_RANKID),
                             
                   new System.Data.SqlClient.SqlParameter("@SearchJobID", SearchJobID),
                   new System.Data.SqlClient.SqlParameter("@SearchJobTitle", SearchJobTitle),
                   
                   new System.Data.SqlClient.SqlParameter("@IsCritical",IsCritical),
                   new System.Data.SqlClient.SqlParameter("@IsCMS", IsCMS),

                   new System.Data.SqlClient.SqlParameter("@FROMDATE", fromdate),
                   new System.Data.SqlClient.SqlParameter("@TODATE", todate),
                   new System.Data.SqlClient.SqlParameter("@DueDateFlageSearch", DueDateFlageSearch),
                   new System.Data.SqlClient.SqlParameter("@PendingOfcVerify", PendingOfcVerify),
                   new System.Data.SqlClient.SqlParameter("@SafetyAlarm", SafetyAlarm),
                   new System.Data.SqlClient.SqlParameter("@Calibration", Calibration),

                   new System.Data.SqlClient.SqlParameter("@IsHistory", IsHistory),
                   new System.Data.SqlClient.SqlParameter("@JobStatus", JobStatus),
                   new System.Data.SqlClient.SqlParameter("@ADVFROMDATE", advfromdate),
                   new System.Data.SqlClient.SqlParameter("@ADVTODATE", advtodate),
                   new System.Data.SqlClient.SqlParameter("@PostponedJob", PostponeJob),
                   new System.Data.SqlClient.SqlParameter("@FollowupAdded", FollowupRAdded),
                   new System.Data.SqlClient.SqlParameter("@JobWithMandateRiskAssessment", JobWithMandateRiskAssess),
                   new System.Data.SqlClient.SqlParameter("@JobWithSubmittedRiskAssessment", JobWithSubRiskAssess),
                   new System.Data.SqlClient.SqlParameter("@JobWithDifferDDock", JobWithDDock),
                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                   new System.Data.SqlClient.SqlParameter("@ISTOTALJOBCOUNT",istotaljobcount),
                   new System.Data.SqlClient.SqlParameter("@ISODJOBCOUNT",isodjobcount),
                   new System.Data.SqlClient.SqlParameter("@ISCRITODJOBCOUNT",iscritodjobcount),

                 
                    
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PMS_Get_OverdueJobs", obj);
            isfetchcount = Convert.ToInt32(ds.Tables[1].Rows[0][0].ToString());  //Convert.ToInt32(obj[obj.Length - 3].Value);
            istotaljobcount = Convert.ToInt32(ds.Tables[2].Rows[0][0].ToString());  //Convert.ToInt32(obj[obj.Length - 3].Value);
            isodjobcount = Convert.ToInt32(ds.Tables[3].Rows[0][0].ToString());//Convert.ToInt32(obj[obj.Length - 2].Value);
            iscritodjobcount = Convert.ToInt32(ds.Tables[4].Rows[0][0].ToString());// Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds;
        }

        /// <summary>
        /// to bind history of jobs : changes done by reshma added Is_Ra_Mandatory & Is_approval
        /// </summary>
        /// <param name="jobid"></param>
        /// <param name="fleetcode"></param>
        /// <param name="dtVessel">ID of Vessel</param>
        /// <param name="Function_ID">ID of function</param>
        /// <param name="System_ID">ID of system </param>
        /// <param name="System_Location_ID">ID of system Location</param>
        /// <param name="SubSystem_ID">ID of Subsystem </param>
        /// <param name="SubSystem_Location_ID">ID of Subsystem Location</param>
        /// <param name="DTCF_RANKID">Dept ID of System</param>
        /// <param name="SearchJobID">Search text for job code</param>
        /// <param name="SearchJobTitle">Search text for job title</param>
        /// <param name="IsCritical">Check for Job is critical</param>
        /// <param name="IsCMS">Check for Job is CMS</param>
        /// <param name="fromdate">Start Date for Filter (Next Due Date)</param>
        /// <param name="todate">End Date for Filter (Next Due Date)</param>
        /// <param name="IsHistory"></param>
        /// <param name="JobStatus"></param>
        /// <param name="DueDateFlageSearch">filter job which are overdue,7 days,This month</param>
        /// <param name="PendingOfcVerify">Filter job pendng for office verification</param>
        /// <param name="SafetyAlarm">Filter job of Safety Alarm </param>
        /// <param name="Calibration">Filter job of Calibration</param>
        /// <param name="advfromdate">Start Date for Filter (Next Due Date)</param>
        /// <param name="advtodate">End Date for Filter (Next Due Date)</param>
        /// <param name="PostponeJob"></param>
        /// <param name="FollowupRAdded"></param>
        /// <param name="JobWithMandateRiskAssess"></param>
        /// <param name="JobWithSubRiskAssess"></param>
        /// <param name="JobWithDDock"></param>
        /// <param name="Is_RAMandatory">check if RA is mandatory or not</param>
        /// <param name="Is_RASubmitted">check if RA form is submitted by vessel or not</param>
        /// <param name="sortby">olumn name by which data to be sorted</param>
        /// <param name="sortdirection">Direction in which data to be sorted 'ASC' or 'DESC'</param>
        /// <param name="pagenumber">Page Number of displaying data      </param>
        /// <param name="pagesize">Max data to be return</param>
        /// <param name="isfetchcount"></param>
        /// <param name="istotaljobcount"></param>
        /// <param name="isodjobcount"></param>
        /// <param name="iscritodjobcount"></param>
        /// <returns></returns>
        public DataSet PMS_Get_OverdueJobs(int? jobid, int? fleetcode, DataTable dtVessel, int? Function_ID, int? System_ID, int? System_Location_ID, int? SubSystem_ID, int? SubSystem_Location_ID,
                                           DataTable DTCF_RANKID, string SearchJobID, string SearchJobTitle, int IsCritical, int IsCMS, DateTime? fromdate, DateTime? todate, int? IsHistory,
                                           int? JobStatus, string DueDateFlageSearch, int? PendingOfcVerify, int? SafetyAlarm, int? Calibration, DateTime? advfromdate, DateTime? advtodate, int? PostponeJob, int? FollowupRAdded,
                                           int? JobWithMandateRiskAssess, int? JobWithSubRiskAssess, int? JobWithDDock, int? Is_RAMandatory, int? Is_RASubmitted, string sortby, int? sortdirection, int? pagenumber, int? pagesize,
                                           ref int isfetchcount, ref int istotaljobcount, ref int isodjobcount, ref int iscritodjobcount)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   
                   new System.Data.SqlClient.SqlParameter("@JOBID", jobid),   
                   new System.Data.SqlClient.SqlParameter("@FLEETCODE", fleetcode),

                   new System.Data.SqlClient.SqlParameter("@VESSELID", dtVessel),
                   new System.Data.SqlClient.SqlParameter("@Function_ID",Function_ID ), 
                   new System.Data.SqlClient.SqlParameter("@System_ID",System_ID),
                   new System.Data.SqlClient.SqlParameter("@System_Location_ID",System_Location_ID),
                   new System.Data.SqlClient.SqlParameter("@SubSystem_ID",SubSystem_ID),
                   new System.Data.SqlClient.SqlParameter("@SubSystem_Location_ID",SubSystem_Location_ID),
                   new System.Data.SqlClient.SqlParameter("@DTCF_RANKID",DTCF_RANKID),
                             
                   new System.Data.SqlClient.SqlParameter("@SearchJobID", SearchJobID),
                   new System.Data.SqlClient.SqlParameter("@SearchJobTitle", SearchJobTitle),
                   
                   new System.Data.SqlClient.SqlParameter("@IsCritical",IsCritical),
                   new System.Data.SqlClient.SqlParameter("@IsCMS", IsCMS),

                   new System.Data.SqlClient.SqlParameter("@FROMDATE", fromdate),
                   new System.Data.SqlClient.SqlParameter("@TODATE", todate),
                   new System.Data.SqlClient.SqlParameter("@DueDateFlageSearch", DueDateFlageSearch),
                   new System.Data.SqlClient.SqlParameter("@PendingOfcVerify", PendingOfcVerify),
                   new System.Data.SqlClient.SqlParameter("@SafetyAlarm", SafetyAlarm),
                   new System.Data.SqlClient.SqlParameter("@Calibration", Calibration),

                   new System.Data.SqlClient.SqlParameter("@IsHistory", IsHistory),
                   new System.Data.SqlClient.SqlParameter("@JobStatus", JobStatus),
                   new System.Data.SqlClient.SqlParameter("@ADVFROMDATE", advfromdate),
                   new System.Data.SqlClient.SqlParameter("@ADVTODATE", advtodate),
                   new System.Data.SqlClient.SqlParameter("@PostponedJob", PostponeJob),
                   new System.Data.SqlClient.SqlParameter("@FollowupAdded", FollowupRAdded),
                   new System.Data.SqlClient.SqlParameter("@JobWithMandateRiskAssessment", JobWithMandateRiskAssess),
                   new System.Data.SqlClient.SqlParameter("@JobWithSubmittedRiskAssessment", JobWithSubRiskAssess),
                   new System.Data.SqlClient.SqlParameter("@JobWithDifferDDock", JobWithDDock),
                   new System.Data.SqlClient.SqlParameter("@Is_RAMandatory",Is_RAMandatory),
                   new System.Data.SqlClient.SqlParameter("@Is_RAApproval",Is_RASubmitted),
                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                   new System.Data.SqlClient.SqlParameter("@ISTOTALJOBCOUNT",istotaljobcount),
                   new System.Data.SqlClient.SqlParameter("@ISODJOBCOUNT",isodjobcount),
                   new System.Data.SqlClient.SqlParameter("@ISCRITODJOBCOUNT",iscritodjobcount),

                 
                    
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PMS_Get_OverdueJobs", obj);
            isfetchcount = Convert.ToInt32(ds.Tables[1].Rows[0][0].ToString());  //Convert.ToInt32(obj[obj.Length - 3].Value);
            //istotaljobcount = Convert.ToInt32(ds.Tables[2].Rows[0][0].ToString());  //Convert.ToInt32(obj[obj.Length - 3].Value);
            //isodjobcount = Convert.ToInt32(ds.Tables[3].Rows[0][0].ToString());//Convert.ToInt32(obj[obj.Length - 2].Value);
            //iscritodjobcount = Convert.ToInt32(ds.Tables[4].Rows[0][0].ToString());// Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds;
        }


        public int PMs_Get_OverdueJobsCount(int? jobid, int? fleetcode, DataTable dtVessel, int? Function_ID, int? System_ID, int? System_Location_ID, int? SubSystem_ID, int? SubSystem_Location_ID,
                                    DataTable DTCF_RANKID, string SearchJobID, string SearchJobTitle, int IsCritical, int IsCMS, DateTime? fromdate, DateTime? todate, int? IsHistory,
                                    int? JobStatus, string DueDateFlageSearch, int? PendingOfcVerify, int? SafetyAlarm, int? Calibration, DateTime? advfromdate, DateTime? advtodate, int? PostponeJob, int? FollowupRAdded,
                                    int? JobWithMandateRiskAssess, int? JobWithSubRiskAssess, int? JobWithDDock, int? Is_RAMandatory, int? Is_RASubmitted, ref int istotaljobcount, ref int isodjobcount, ref int iscritodjobcount)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   
                   new System.Data.SqlClient.SqlParameter("@JOBID", jobid),   
                   new System.Data.SqlClient.SqlParameter("@FLEETCODE", fleetcode),

                   new System.Data.SqlClient.SqlParameter("@VESSELID", dtVessel),
                   new System.Data.SqlClient.SqlParameter("@Function_ID",Function_ID ), 
                   new System.Data.SqlClient.SqlParameter("@System_ID",System_ID),
                   new System.Data.SqlClient.SqlParameter("@System_Location_ID",System_Location_ID),
                   new System.Data.SqlClient.SqlParameter("@SubSystem_ID",SubSystem_ID),
                   new System.Data.SqlClient.SqlParameter("@SubSystem_Location_ID",SubSystem_Location_ID),
                   new System.Data.SqlClient.SqlParameter("@DTCF_RANKID",DTCF_RANKID),
                             
                   new System.Data.SqlClient.SqlParameter("@SearchJobID", SearchJobID),
                   new System.Data.SqlClient.SqlParameter("@SearchJobTitle", SearchJobTitle),
                   
                   new System.Data.SqlClient.SqlParameter("@IsCritical",IsCritical),
                   new System.Data.SqlClient.SqlParameter("@IsCMS", IsCMS),

                   new System.Data.SqlClient.SqlParameter("@FROMDATE", fromdate),
                   new System.Data.SqlClient.SqlParameter("@TODATE", todate),
                   new System.Data.SqlClient.SqlParameter("@DueDateFlageSearch", DueDateFlageSearch),
                   new System.Data.SqlClient.SqlParameter("@PendingOfcVerify", PendingOfcVerify),
                   new System.Data.SqlClient.SqlParameter("@SafetyAlarm", SafetyAlarm),
                   new System.Data.SqlClient.SqlParameter("@Calibration", Calibration),

                   new System.Data.SqlClient.SqlParameter("@IsHistory", IsHistory),
                   new System.Data.SqlClient.SqlParameter("@JobStatus", JobStatus),
                   new System.Data.SqlClient.SqlParameter("@ADVFROMDATE", advfromdate),
                   new System.Data.SqlClient.SqlParameter("@ADVTODATE", advtodate),
                   new System.Data.SqlClient.SqlParameter("@PostponedJob", PostponeJob),
                   new System.Data.SqlClient.SqlParameter("@FollowupAdded", FollowupRAdded),
                   new System.Data.SqlClient.SqlParameter("@JobWithMandateRiskAssessment", JobWithMandateRiskAssess),
                   new System.Data.SqlClient.SqlParameter("@JobWithSubmittedRiskAssessment", JobWithSubRiskAssess),
                   new System.Data.SqlClient.SqlParameter("@JobWithDifferDDock", JobWithDDock),
                   new System.Data.SqlClient.SqlParameter("@Is_RAMandatory",Is_RAMandatory),
                   new System.Data.SqlClient.SqlParameter("@Is_RAApproval",Is_RASubmitted),
                  new System.Data.SqlClient.SqlParameter("@ISTOTALJOBCOUNT",istotaljobcount),
                   new System.Data.SqlClient.SqlParameter("@ISODJOBCOUNT",isodjobcount),
                   new System.Data.SqlClient.SqlParameter("@ISCRITODJOBCOUNT",iscritodjobcount),

                 
                    
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PMs_Get_OverdueJobsCount", obj);
           // isfetchcount = Convert.ToInt32(ds.Tables[1].Rows[0][0].ToString());  //Convert.ToInt32(obj[obj.Length - 3].Value);
            istotaljobcount = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());  //Convert.ToInt32(obj[obj.Length - 3].Value);
            isodjobcount = Convert.ToInt32(ds.Tables[1].Rows[0][0].ToString());//Convert.ToInt32(obj[obj.Length - 2].Value);
            iscritodjobcount = Convert.ToInt32(ds.Tables[2].Rows[0][0].ToString());// Convert.ToInt32(obj[obj.Length - 1].Value);
            return 1;
        }
        public DataTable PMS_Get_SystemLocation(int Function, DataTable dtVessel)
        {

            DataTable dt = new DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@Function", Function), 
                   new System.Data.SqlClient.SqlParameter("@dtVESSEL", dtVessel), 
             };
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PMS_Get_SystemLocation", obj);
            dt = ds.Tables[0];
            return dt;

        }

        /// <summary>
        ///  Added by reshma RA to fetch RA form
        /// </summary>
        /// <param name="JobHistoryID"> Job history id</param>
        /// <returns></returns>
        public DataTable PMS_DTL_RAFormsByJobID(int JobHistoryID)
        {
            try
            {
                SqlParameter[] sqlprm = new SqlParameter[]
										{ 
										   new System.Data.SqlClient.SqlParameter("@JobID", JobHistoryID)
										};
                return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PMS_DTL_RAFormsByJobID", sqlprm).Tables[0];
            }
            catch (Exception)
            {
                throw;
            }


        }

        public DataTable PMS_Get_SubSystemLocation(string SYSTEMCODE, int? SUBSYSTEMID, DataTable dtVessel)
        {

            DataTable dt = new DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                    new System.Data.SqlClient.SqlParameter("@SYSTEMCODE", SYSTEMCODE), 
                   new System.Data.SqlClient.SqlParameter("@SUBSYSTEMID", SUBSYSTEMID), 
                   new System.Data.SqlClient.SqlParameter("@dtVESSEL", dtVessel), 
             };
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PMS_Get_SubSystemLocation", obj);
            dt = ds.Tables[0];
            return dt;

        }

        #endregion

    }
}
