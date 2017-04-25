using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SMS.Data.PMS;
using System.Data;

namespace SMS.Business.PMS
{
    public class BLL_PMS_Job_Status
    {

        SMS.Data.PMS.DAL_PMS_Job_Status objjobstatus = new DAL_PMS_Job_Status();

        public DataSet TecJobStatusSearch(int? fleetcode, int? vesselid, int? locationid, string systemid, string subsystemid, int? jobdeptid, int? rankid, string jobtitlesearchtext, int? jobidsearchtext, int? critical, int? cms
           , DateTime? fromdate, DateTime? todate, string DueDateFlageSearch, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {

            return objjobstatus.TecJobStatusSearch(fleetcode, vesselid, locationid, systemid, subsystemid, jobdeptid, rankid, jobtitlesearchtext, jobidsearchtext, critical, cms, fromdate, todate, DueDateFlageSearch
                , sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);

        }


        public DataSet TecJobStatusSearch(int? fleetcode, DataTable DTCF_VESSELID, DataTable DTCF_LOCATIONID, DataTable DTCF_SUBSYSTEMID
            , DataTable DTCF_DEPARTMENTID, DataTable DTCF_RankID
            , string SearchJobID, string SearchJobTitle, DataTable DTCF_CRITICAL, DataTable DTCF_CMS, DateTime? fromdate, DateTime? todate, string DueDateFlageSearch
            , string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {

            return objjobstatus.TecJobStatusSearch(fleetcode, DTCF_VESSELID, DTCF_LOCATIONID, DTCF_SUBSYSTEMID, DTCF_DEPARTMENTID, DTCF_RankID
                , SearchJobID, SearchJobTitle, DTCF_CRITICAL, DTCF_CMS, fromdate, todate, DueDateFlageSearch, sortby, sortdirection, pagenumber, pagesize, ref  isfetchcount);

        }



        public DataSet TecJobStatusHistorySearch(int? jobid, int? fleetcode, int? vesselid, int? locationid, string systemid, string subsystemid, int? rankid, int? jobdeptid, string jobtitlesearchtext, int? jobidsearchtext, int? critical, int? cms
          , DateTime? fromdate, DateTime? todate, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {

            return objjobstatus.TecJobStatusHistorySearch(jobid, fleetcode, vesselid, locationid, systemid, subsystemid, rankid, jobdeptid, jobtitlesearchtext, jobidsearchtext, critical, cms, fromdate, todate
                , sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);

        }


        public DataSet TecJobStatusIndex(int? jobid, int? fleetcode, int? VESSELID, int? Function_ID, int? System_ID, int? System_Location_ID, int? SubSystem_ID, int? SubSystem_Location_ID, DataTable DTCF_RANKID, string SearchJobID, string SearchJobTitle, DataTable DTCF_CRITICAL, DataTable DTCF_CMS, DateTime? fromdate, DateTime? todate, int? IsHistory, string DueDateFlageSearch
                                             , int? SafetyAlarm, int? Calibration, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {

            return objjobstatus.TecJobStatusIndex(jobid, fleetcode, VESSELID, Function_ID, System_ID, System_Location_ID, SubSystem_ID, SubSystem_Location_ID, DTCF_RANKID, SearchJobID, SearchJobTitle, DTCF_CRITICAL, DTCF_CMS, fromdate, todate, IsHistory, DueDateFlageSearch
                , SafetyAlarm, Calibration, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);
        }

        public DataSet TecJobStatusIndex(int? jobid, int? fleetcode, int? VESSELID, int? Function_ID, int? System_ID, int? System_Location_ID, int? SubSystem_ID, int? SubSystem_Location_ID, DataTable DTCF_RANKID, string SearchJobID, string SearchJobTitle, int? IsCritical, int? IsCMS, DateTime? fromdate, DateTime? todate, DateTime? advfromdate, DateTime? advtodate, int? IsHistory, string DueDateFlageSearch
        , int? PendingOfcVerify, int? SafetyAlarm, int? Calibration, int? PostponeJob, int? FollowupRAdded, int? JobWithMandateRiskAssess, int? JobWithSubRiskAssess, int? JobWithDDock, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            return objjobstatus.TecJobStatusIndex(jobid, fleetcode, VESSELID, Function_ID, System_ID, System_Location_ID, SubSystem_ID, SubSystem_Location_ID, DTCF_RANKID, SearchJobID, SearchJobTitle, IsCritical, IsCMS, fromdate, todate, advfromdate, advtodate, IsHistory, DueDateFlageSearch
              , PendingOfcVerify, SafetyAlarm, Calibration, PostponeJob, FollowupRAdded, JobWithMandateRiskAssess, JobWithSubRiskAssess, JobWithDDock, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);
        }
        /// <summary>
        /// Bind jobs status
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
        public DataSet TecJobStatusIndex(int? jobid, int? fleetcode, int? VESSELID, int? Function_ID, int? System_ID, int? System_Location_ID, int? SubSystem_ID, int? SubSystem_Location_ID, DataTable DTCF_RANKID, string SearchJobID, string SearchJobTitle, int? IsCritical, int? IsCMS, DateTime? fromdate, DateTime? todate, DateTime? advfromdate, DateTime? advtodate, int? IsHistory, string DueDateFlageSearch
        , int? PendingOfcVerify, int? SafetyAlarm, int? Calibration, int? PostponeJob, int? FollowupRAdded, int? JobWithMandateRiskAssess, int? JobWithSubRiskAssess, int? JobWithDDock, string sortby, int? sortdirection, int? pagenumber, int? pagesize, int? RHDone, ref int isfetchcount)
        {
            return objjobstatus.TecJobStatusIndex(jobid, fleetcode, VESSELID, Function_ID, System_ID, System_Location_ID, SubSystem_ID, SubSystem_Location_ID, DTCF_RANKID, SearchJobID, SearchJobTitle, IsCritical, IsCMS, fromdate, todate, advfromdate, advtodate, IsHistory, DueDateFlageSearch
              , PendingOfcVerify, SafetyAlarm, Calibration, PostponeJob, FollowupRAdded, JobWithMandateRiskAssess, JobWithSubRiskAssess, JobWithDDock, sortby, sortdirection, pagenumber, pagesize, RHDone, ref isfetchcount);
        }

        /// <summary>
        /// Bind jobs status
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
        /// <param name="Is_RAMandatory">check if RA is mandatory or not</param>
        /// <param name="Is_RASubmitted">check if RA form is approve by office or not</param>
        /// <param name="isfetchcount"></param>
        /// <returns></returns>
        public DataSet TecJobStatusIndex(int? jobid, int? fleetcode, int? VESSELID, int? Function_ID, int? System_ID, int? System_Location_ID, int? SubSystem_ID, int? SubSystem_Location_ID, DataTable DTCF_RANKID, string SearchJobID, string SearchJobTitle, int? IsCritical, int? IsCMS, DateTime? fromdate, DateTime? todate, DateTime? advfromdate, DateTime? advtodate, int? IsHistory, string DueDateFlageSearch
        , int? PendingOfcVerify, int? SafetyAlarm, int? Calibration, int? PostponeJob, int? FollowupRAdded, int? JobWithMandateRiskAssess, int? JobWithSubRiskAssess, int? JobWithDDock, string sortby, int? sortdirection, int? pagenumber, int? pagesize, int? RHDone, int? Is_RAMandatory, int? Is_RASubmitted, ref int isfetchcount)
        {
            return objjobstatus.TecJobStatusIndex(jobid, fleetcode, VESSELID, Function_ID, System_ID, System_Location_ID, SubSystem_ID, SubSystem_Location_ID, DTCF_RANKID, SearchJobID, SearchJobTitle, IsCritical, IsCMS, fromdate, todate, advfromdate, advtodate, IsHistory, DueDateFlageSearch
              , PendingOfcVerify, SafetyAlarm, Calibration, PostponeJob, FollowupRAdded, JobWithMandateRiskAssess, JobWithSubRiskAssess, JobWithDDock, sortby, sortdirection, pagenumber, pagesize, RHDone, Is_RAMandatory, Is_RASubmitted, ref isfetchcount);
        }

        public DataSet TecMachineryRunningHoursSearch(int? fleetcode, int? vesselid, int? locationid, DateTime? fromdate, DateTime? todate, string displayrecordtype
        , string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {

            return objjobstatus.TecMachineryRunningHoursSearch(fleetcode, vesselid, locationid, fromdate, todate, displayrecordtype
                    , sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);

        }

        public DataSet TecMachineryRunningHoursSearch(int? fleetcode, int? VESSELID, DateTime? FROMREADDATE, DateTime? TOREADDATE
            , DateTime? FROMCREATEDDATE, DateTime? TOCREATEDDATE, string displayrecordtype, int? Function_ID, string System_ID, int? System_Location_ID, int? SubSystem_ID, int? SubSystem_Location_ID,
            string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {

            return objjobstatus.TecMachineryRunningHoursSearch(fleetcode, VESSELID, FROMREADDATE, TOREADDATE, FROMCREATEDDATE, TOCREATEDDATE
           , displayrecordtype, Function_ID, System_ID, System_Location_ID, SubSystem_ID, SubSystem_Location_ID, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);

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
        /// <param name="sortdirection">sort direction|| ASC ||DESC </param>
        /// <param name="pagenumber"></param>
        /// <param name="pagesize">no. of rows displyed per page</param>
        /// <param name="isfetchcount">no. of rows fetched </param>
        /// <param name="DisplayInheritingCounters">to display linked systems / sub-systems</param>
        /// <returns>List of Machinery running hours details</returns>
        public DataSet TecMachineryRunningHoursSearch(int? fleetcode, int? VESSELID, DateTime? FROMREADDATE, DateTime? TOREADDATE, DateTime? FROMCREATEDDATE, DateTime? TOCREATEDDATE, 
                                                      string displayrecordtype, int? Function_ID, string System_ID, int? System_Location_ID, int? SubSystem_ID, int? SubSystem_Location_ID,
                                                      string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount, int DisplayInheritingCounters)
        {

            return objjobstatus.TecMachineryRunningHoursSearch(fleetcode, VESSELID, FROMREADDATE, TOREADDATE, FROMCREATEDDATE, TOCREATEDDATE, displayrecordtype, Function_ID, System_ID, 
                                                                System_Location_ID, SubSystem_ID, SubSystem_Location_ID, sortby, sortdirection, pagenumber, pagesize, 
                                                                ref isfetchcount, DisplayInheritingCounters);

        }


        public DataTable TecGetJobsSubCatalogue()
        {
            return objjobstatus.TecGetJobsSubCatalogue();
        }

        public DataTable TecGet_Locations_SubCatalogue(int? locationID, int? VesselID)
        {
            return objjobstatus.TecGet_Locations_SubCatalogue(locationID, VesselID);
        }

        public DataTable TecGet_JobStatus_Locations_SubCatalogue(DataTable DTCF_LOCATIONID, DataTable DTCF_VESSELID)
        {
            return objjobstatus.TecGet_JobStatus_Locations_SubCatalogue(DTCF_LOCATIONID, DTCF_VESSELID);
        }



        public DataTable TecGet_JobHistory_Locations_SubCatalogue(DataTable DTCF_LOCATIONID, DataTable DTCF_VESSELID)
        {
            return objjobstatus.TecGet_JobHistory_Locations_SubCatalogue(DTCF_LOCATIONID, DTCF_VESSELID);
        }



        public DataSet TecJobOverDueSearch(int? fleetcode, int? vesselid, int? locationid, string systemid, string subsystemid
           , int? jobdeptid, int? rankid, string jobtitlesearchtext, int? jobidsearchtext
           , int? critical, int? cms, DateTime? fromdate, DateTime? todate, int? month, int? year, string status
           , string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {

            return objjobstatus.TecJobOverDueSearch(fleetcode, vesselid, locationid, systemid, subsystemid, jobdeptid, rankid, jobtitlesearchtext, jobidsearchtext
                , critical, cms, fromdate, todate, month, year, status, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);

        }


        public DataSet TecJobOverDueSearch(int? fleetcode, DataTable DTCF_VESSELID, DataTable DTCF_LOCATIONID, DataTable DTCF_SUBSYSTEMID
            , int? jobdeptid, DataTable DTCF_RankID, string JOB_TITLE_SEARCH, string TYCF_JOB_TITLE_SEARCH, int? JOB_ID_SEARCH, string TYCF_JOB_ID_SEARCH
            , DataTable DTCF_CRITICAL, DataTable DTCF_CMS, DateTime? FROMDATE, DateTime? TODATE, int? MONTH, int? YEAR, string STATUS
            , string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {

            return objjobstatus.TecJobOverDueSearch(fleetcode, DTCF_VESSELID, DTCF_LOCATIONID, DTCF_SUBSYSTEMID, jobdeptid, DTCF_RankID, JOB_TITLE_SEARCH, TYCF_JOB_TITLE_SEARCH
                , JOB_ID_SEARCH, TYCF_JOB_ID_SEARCH, DTCF_CRITICAL, DTCF_CMS, FROMDATE, TODATE, MONTH, YEAR, STATUS, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);

        }


        public DataSet TecJobsOverdueHistorySearch(int? jobid, int? fleetcode, int? vesselid, int? locationid, string systemid, string subsystemid, int? rankid, int? jobdeptid, string jobtitlesearchtext, int? jobidsearchtext
            , int? critical, int? cms, DateTime? fromdate, DateTime? todate
            , string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            return objjobstatus.TecJobsOverdueHistorySearch(jobid, fleetcode, vesselid, locationid, systemid, subsystemid, rankid, jobdeptid, jobtitlesearchtext, jobidsearchtext, critical, cms
                , fromdate, todate, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);

        }

        public DataSet TecJobsOverdueList(int joboverdueid)
        {
            return objjobstatus.TecJobsOverdueList(joboverdueid);
        }


        public int TecJobsOverdueResponse(int userid, int joboverdueid, DateTime modifiedcompletiondate, string suptdresponse, int vesselcode)
        {
            return objjobstatus.TecJobsOverdueResponse(userid, joboverdueid, modifiedcompletiondate, suptdresponse, vesselcode);
        }



        public DataSet TecJobsSparesItemUsedSearch(int? vesselid, int? jobid, int? jobhistoryid, string systemid, string subsystemid, string itemsearchtext
         , string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            return objjobstatus.TecJobsSparesItemUsedSearch(vesselid, jobid, jobhistoryid, systemid, subsystemid, itemsearchtext
         , sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);

        }


        public DataSet TecJobDoneNotDoneSummarySearch(int? fleetcode, int? Vesselid, int? RankCategory, string SearchText, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            return objjobstatus.TecJobDoneNotDoneSummarySearch(fleetcode, Vesselid, RankCategory, SearchText, sortby, sortdirection, pagenumber, pagesize, ref  isfetchcount);

        }

        public DataSet TecJobDoneNotDoneSummarySearch(int? fleetcode, int? vesselid, DataTable DTCF_RANKID, int? StaffCode, string TYCF_StaffCode, string StaffName, string TYCF_StaffName
          , string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            return objjobstatus.TecJobDoneNotDoneSummarySearch(fleetcode, vesselid, DTCF_RANKID, StaffCode, TYCF_StaffCode, StaffName, TYCF_StaffName, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);
        }

        public DataSet TecJobDoneVesselWiseSummarySearch(int? fleetcode, int? vesselid, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            return objjobstatus.TecJobDoneVesselWiseSummarySearch(fleetcode, vesselid, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);
        }

        public DataSet TecJobDailyUpdatingSummarySearch(int? fleetcode, DateTime? fromdate, DateTime? todate)
        {
            return objjobstatus.TecJobDailyUpdatingSummarySearch(fleetcode, fromdate, todate);
        }

        public DataTable TecJobGetJobDoneAttachment(int? JobHistoryID, int? VesselID)
        {
            return objjobstatus.TecJobGetJobDoneAttachment(JobHistoryID, VesselID);
        }

        public DataTable TecJobsGetRanks()
        {
            return objjobstatus.TecJobsGetRanks();
        }


        public DataTable Get_Machinery_Location(int? Vessel_ID, int? Run_Hour)
        {
            return objjobstatus.Get_Machinery_Location(Vessel_ID, Run_Hour);
        }

        public DataTable Get_ParentChildLocationLinkage(int VesselID, int ParentSystemID)
        {
            return objjobstatus.Get_ParentChildLocationLinkage(VesselID, ParentSystemID);
        }

        public int AddUpdateUnit(int UnitID, string UnitName, int UserID)
        {
            try
            {
                return objjobstatus.AddUpdateAlarmUnit(UnitID, UnitName, UserID);
            }
            catch
            {
                throw;
            }
        }

        public DataSet GetAlarmUnits(string UnitName, int? pagenumber, int? pagesize)
        {
            return objjobstatus.GetAlarmUnit(UnitName, pagenumber, pagesize);
        }


        public int DeleteAlarmUnit(int UnitID, int UserID)
        {
            try
            {
                return objjobstatus.DeleteAlarmUnit(UnitID, UserID);
            }
            catch
            {
                throw;
            }
        }


        public int AddUpdateAlarmEffect(int EffectID, string EffectName, int UserID)
        {
            try
            {
                return objjobstatus.AddUpdateAlarmEffect(EffectID, EffectName, UserID);
            }
            catch
            {
                throw;
            }
        }

        public DataSet GetAlarmEffects(string EffectName, int? pagenumber, int? pagesize)
        {
            return objjobstatus.GetAlarmEffect(EffectName, pagenumber, pagesize);
        }


        public int DeleteAlarmEffect(int EffectID, int UserID)
        {
            try
            {
                return objjobstatus.DeleteAlarmEffect(EffectID, UserID);
            }
            catch
            {
                throw;
            }
        }



        #region  PMS Historical Overdue Jobs Function Added By Someshwar Dated 11-02-2016

        public DataSet PMS_Get_OverdueJobs(int? jobid, int? fleetcode, DataTable dtVessel, int? Function_ID, int? System_ID, int? System_Location_ID, int? SubSystem_ID, int? SubSystem_Location_ID, DataTable DTCF_RANKID, string SearchJobID, string SearchJobTitle, int IsCritical, int IsCMS, DateTime? fromdate, DateTime? todate, int? IsHistory, int? JobStatus, string DueDateFlageSearch
                                     , int? SafetyAlarm, int? Calibration, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount, ref  int istotaljobcount, ref int isodjobcount, ref int iscritodjobcount)
        {

            return objjobstatus.PMS_Get_OverdueJobs(jobid, fleetcode, dtVessel, Function_ID, System_ID, System_Location_ID, SubSystem_ID, SubSystem_Location_ID, DTCF_RANKID, SearchJobID, SearchJobTitle, IsCritical, IsCMS, fromdate, todate, IsHistory, JobStatus, DueDateFlageSearch
                , SafetyAlarm, Calibration, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount, ref  istotaljobcount, ref isodjobcount, ref iscritodjobcount);
        }

       
        public DataSet PMS_Get_OverdueJobs(int? jobid, int? fleetcode, DataTable dtVessel, int? Function_ID, int? System_ID, int? System_Location_ID, int? SubSystem_ID, int? SubSystem_Location_ID,
                                           DataTable DTCF_RANKID, string SearchJobID, string SearchJobTitle, int IsCritical, int IsCMS, DateTime? fromdate, DateTime? todate, int? IsHistory,
                                           int? JobStatus, string DueDateFlageSearch, int? PendingOfcVerify, int? SafetyAlarm, int? Calibration, DateTime? advfromdate, DateTime? advtodate, int? PostponeJob, int? FollowupRAdded,
                                           int? JobWithMandateRiskAssess, int? JobWithSubRiskAssess, int? JobWithDDock, string sortby, int? sortdirection, int? pagenumber, int? pagesize,
                                           ref int isfetchcount, ref  int istotaljobcount, ref int isodjobcount, ref int iscritodjobcount)
        {

            return objjobstatus.PMS_Get_OverdueJobs(jobid, fleetcode, dtVessel, Function_ID, System_ID, System_Location_ID, SubSystem_ID, SubSystem_Location_ID, DTCF_RANKID, SearchJobID,
                                                    SearchJobTitle, IsCritical, IsCMS, fromdate, todate, IsHistory, JobStatus, DueDateFlageSearch, PendingOfcVerify, SafetyAlarm, Calibration, advfromdate, advtodate, PostponeJob, FollowupRAdded, JobWithMandateRiskAssess,
                                                    JobWithSubRiskAssess, JobWithDDock, sortby, sortdirection,
                                                    pagenumber, pagesize, ref isfetchcount, ref  istotaljobcount, ref isodjobcount, ref iscritodjobcount);
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
                                           ref int isfetchcount, ref  int istotaljobcount, ref int isodjobcount, ref int iscritodjobcount)
        {

            return objjobstatus.PMS_Get_OverdueJobs(jobid, fleetcode, dtVessel, Function_ID, System_ID, System_Location_ID, SubSystem_ID, SubSystem_Location_ID, DTCF_RANKID, SearchJobID,
                                                    SearchJobTitle, IsCritical, IsCMS, fromdate, todate, IsHistory, JobStatus, DueDateFlageSearch, PendingOfcVerify, SafetyAlarm, Calibration, advfromdate, advtodate, PostponeJob, FollowupRAdded, JobWithMandateRiskAssess,
                                                    JobWithSubRiskAssess, JobWithDDock, Is_RAMandatory, Is_RASubmitted, sortby, sortdirection,
                                                    pagenumber, pagesize, ref isfetchcount, ref  istotaljobcount, ref isodjobcount, ref iscritodjobcount);
        }
        public int PMs_Get_OverdueJobsCount(int? jobid, int? fleetcode, DataTable dtVessel, int? Function_ID, int? System_ID, int? System_Location_ID, int? SubSystem_ID, int? SubSystem_Location_ID,
                                          DataTable DTCF_RANKID, string SearchJobID, string SearchJobTitle, int IsCritical, int IsCMS, DateTime? fromdate, DateTime? todate, int? IsHistory,
                                          int? JobStatus, string DueDateFlageSearch, int? PendingOfcVerify, int? SafetyAlarm, int? Calibration, DateTime? advfromdate, DateTime? advtodate, int? PostponeJob, int? FollowupRAdded,
                                          int? JobWithMandateRiskAssess, int? JobWithSubRiskAssess, int? JobWithDDock, int? Is_RAMandatory, int? Is_RASubmitted, ref  int istotaljobcount, ref int isodjobcount, ref int iscritodjobcount)
        {

            return objjobstatus.PMs_Get_OverdueJobsCount(jobid, fleetcode, dtVessel, Function_ID, System_ID, System_Location_ID, SubSystem_ID, SubSystem_Location_ID, DTCF_RANKID, SearchJobID,
                                                    SearchJobTitle, IsCritical, IsCMS, fromdate, todate, IsHistory, JobStatus, DueDateFlageSearch, PendingOfcVerify, SafetyAlarm, Calibration, advfromdate, advtodate, PostponeJob, FollowupRAdded, JobWithMandateRiskAssess,
                                                    JobWithSubRiskAssess, JobWithDDock, Is_RAMandatory, Is_RASubmitted, ref  istotaljobcount, ref isodjobcount, ref iscritodjobcount);
        }
        public DataTable PMS_Get_SystemLocation(int Function, DataTable dtVessel)
        {
            return objjobstatus.PMS_Get_SystemLocation(Function, dtVessel);
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
                return objjobstatus.PMS_DTL_RAFormsByJobID(JobHistoryID);
            }
            catch (Exception)
            {
                throw;
            }


        }

        public DataTable PMS_Get_SubSystemLocation(string SYSTEMCODE, int? SUBSYSTEMID, DataTable dtVessel)
        {
            return objjobstatus.PMS_Get_SubSystemLocation(SYSTEMCODE, SUBSYSTEMID, dtVessel);
        }
        #endregion

    }
}
