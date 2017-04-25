using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SMS.Data.PMS;
using System.Data;


namespace SMS.Business.PMS
{
    public class BLL_PMS_Library_Jobs
    {


        SMS.Data.PMS.DAL_PMS_Library_Jobs objjobs = new DAL_PMS_Library_Jobs();


        //DAL_Tec_Library_Jobs objTecjobs = new DAL_Tec_Library_Jobs(); 


        public DataSet LibraryJobList(int jobid)
        {
            return objjobs.LibraryJobList(jobid);

        }


        public DataSet LibraryJobIndividualDetails(int jobid, int? JobHistoryID, string QueryFlag)
        {
            return objjobs.LibraryJobIndividualDetails(jobid, JobHistoryID, QueryFlag);
        }

        /// <summary>
        /// For binding Function, SystemLocation & SubSystemLoaction
        /// </summary>
       
        public DataSet LibraryJobIndividualDetails(int jobid, int? JobHistoryID, string QueryFlag, int SysLocID, int SubSysLocID)
        {
            return objjobs.LibraryJobIndividualDetails(jobid, JobHistoryID, QueryFlag,SysLocID, SubSysLocID);
        }

        public DataSet LibraryJobSearch(int? systemid, int? subsysteid, int? vesselid, int? deptid, int? rankid, string jobtitle
            , int? IsActive, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {

            return objjobs.LibraryJobSearch(systemid, subsysteid, vesselid, deptid, rankid, jobtitle, IsActive, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);

        }
        public DataSet ManageSystemJobSearch(int? systemid, int? subsysteid, int? vesselid, int? deptid, int? rankid, string jobtitle
            , int? IsActive, string searchtext, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {

            return objjobs.ManageSystemJobSearch(systemid, subsysteid, vesselid, deptid, rankid, jobtitle, IsActive, searchtext, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);

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

            return objjobs.ManageSystemJobSearch(systemid, subsysteid, vesselid, deptid, rankid, jobtitle, IsActive, searchtext, Is_RAMandatory, Is_RAApproval, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);

        }

        public DataSet LibraryJobGetToCopy(string systemcode, int? subsystemid, int? vesselid, int? IsActive)
        {
            return objjobs.LibraryJobGetToCopy(systemcode, subsystemid, vesselid, IsActive);
        }

        public DataSet LibraryJobGetToMove(string systemcode, int? subsystemid, int? vesselid, int? IsActive)
        {
            return objjobs.LibraryJobGetToMove(systemcode, subsystemid, vesselid, IsActive);
        }

        public int LibraryJobSave(int userid, int systemid, int subsystemid, int vesselid, int deptid, int rankid, string jobtitle
         , string jobdesc, int frequency, int frequencytype, int cms, int critical, string Job_Code, int? Is_Tech_Required)
        {
            return objjobs.LibraryJobSave(userid, systemid, subsystemid, vesselid, deptid, rankid, jobtitle, jobdesc, frequency, frequencytype, cms, critical, Job_Code, Is_Tech_Required);

        }
        public int LibraryJobSave(int userid, int systemid, int subsystemid, int vesselid, int deptid, int rankid, string jobtitle
        , string jobdesc, int frequency, int frequencytype, int cms, int critical, string Job_Code, int? Is_Tech_Required, int? Is_SafetyAlarm, int? Is_Calibration)
        {
            return objjobs.LibraryJobSave(userid, systemid, subsystemid, vesselid, deptid, rankid, jobtitle, jobdesc, frequency, frequencytype, cms, critical, Job_Code, Is_Tech_Required, Is_SafetyAlarm, Is_Calibration);

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
            return objjobs.LibraryJobSave(userid, systemid, subsystemid, vesselid, deptid, rankid, jobtitle, jobdesc, frequency, frequencytype, cms, critical, Job_Code, Is_Tech_Required, Is_SafetyAlarm, Is_Calibration, Is_RAMandatory, Is_RAApproval);

        }

        public int LibraryJobSaveFromOtherVessel(int userid, string tosystemcode, int tosubsystemid, int tovesselid, int deptid, int rankid, string jobtitle
                 , string jobdesc, int frequency, int frequencytype, int cms, int critical, int? safetyalarm, int? calibration)
        {
            return objjobs.LibraryJobSaveFromOtherVessel(userid, tosystemcode, tosubsystemid, tovesselid, deptid, rankid, jobtitle, jobdesc, frequency, frequencytype, cms, critical, safetyalarm, calibration);
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
            return objjobs.LibraryJobSaveFromOtherVessel(userid, tosystemcode, tosubsystemid, tovesselid, deptid, rankid, jobtitle, jobdesc, frequency, frequencytype, cms, critical, safetyalarm, calibration, Is_RAMandatory, Is_RAApproval);
        }


        public int LibraryJobMoveFromOtherSubSystem(int userid, string tosystemcode, int tosubsystemid, int jobid)
        {
            return objjobs.LibraryJobMoveFromOtherSubSystem(userid, tosystemcode, tosubsystemid, jobid);
        }


        public int LibraryJobOverWrite(int userid, int jobid)
        {
            return objjobs.LibraryJobOverWrite(userid, jobid);
        }

        public int LibraryJobUpdate(int userid, int jobsid, int systemid, int subsystemid, int vesselid, int deptid, int rankid, string jobtitle
          , string jobdesc, int frequency, int frequencytype, int cms, int critical, string Job_Code, int? Is_Tech_Required)
        {

            return objjobs.LibraryJobUpdate(userid, jobsid, systemid, subsystemid, vesselid, deptid, rankid, jobtitle, jobdesc, frequency, frequencytype, cms, critical, Job_Code, Is_Tech_Required);

        }
        public int LibraryJobUpdate(int userid, int jobsid, int systemid, int subsystemid, int vesselid, int deptid, int rankid, string jobtitle
        , string jobdesc, int frequency, int frequencytype, int cms, int critical, string Job_Code, int? Is_Tech_Required, int? Is_SafetyAlarm, int? Is_Calibration)
        {

            return objjobs.LibraryJobUpdate(userid, jobsid, systemid, subsystemid, vesselid, deptid, rankid, jobtitle, jobdesc, frequency, frequencytype, cms, critical, Job_Code, Is_Tech_Required, Is_SafetyAlarm, Is_Calibration);

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

            return objjobs.LibraryJobUpdate(userid, jobsid, systemid, subsystemid, vesselid, deptid, rankid, jobtitle, jobdesc, frequency, frequencytype, cms, critical, Job_Code, Is_Tech_Required, Is_SafetyAlarm, Is_Calibration, Is_RAMandatory, Is_RAApproval);

        }
        public int LibraryJobDelete(int userid, int jobid)
        {

            return objjobs.LibraryJobDelete(userid, jobid);
        }


        public int LibraryJobRestore(int userid, int jobid)
        {

            return objjobs.LibraryJobRestore(userid, jobid);
        }


        public DataTable LibraryGetPMSSystemParameterList(string parenttypecode, string searchtext)
        {
            return objjobs.LibraryGetPMSSystemParameterList(parenttypecode, searchtext);
        }



        public DataSet LibraryCatalogueLocationAssignSearch(string systemcode, string SubSystemCode, string searchtext, int vesselid, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {

            return objjobs.LibraryCatalogueLocationAssignSearch(systemcode, SubSystemCode, searchtext, vesselid, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);

        }
        public DataSet ManageSystemLocationAssignSearch(string systemcode, string SubSystemCode, string searchtext, int vesselid, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {

            return objjobs.ManageSystemLocationAssignSearch(systemcode, SubSystemCode, searchtext, vesselid, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);

        }
        public DataSet PMS_Get_AssignLocation(string systemcode, string SubSystemCode, string searchtext, int vesselid, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {

            return objjobs.PMS_Get_AssignLocation(systemcode, SubSystemCode, searchtext, vesselid, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);

        }
        public int LibraryCatalogueLocationAssignSave(int userid, string systemcode, string SubSystemCode, int? locationcode, int vesselcode, string Category_Code)
        {
            return objjobs.LibraryCatalogueLocationAssignSave(userid, systemcode, SubSystemCode, locationcode, vesselcode, Category_Code);
        }
        public int PMS_Update_AssignLocationStatus(string systemcode, string SubSystemCode, string CategoryCode, int ModifiedBy, int? LocationCode, int VesselID)
        {
            return objjobs.PMS_Update_AssignLocationStatus(systemcode, SubSystemCode, CategoryCode, ModifiedBy, LocationCode, VesselID);
        }

        public int LibraryCatalogueLocationAssignDelete(int userid, int AssignlocationID, int vesselcode)
        {
            return objjobs.LibraryCatalogueLocationAssignDelete(userid, AssignlocationID, vesselcode);
        }

        public DataTable LibraryGetCatalogueLocationAssign(string systemcode, int vesselcode)
        {
            return objjobs.LibraryGetCatalogueLocationAssign(systemcode, vesselcode);
        }

        public DataSet LibraryMachineryInforSearch(string search, string systemcode, string systemdesc, string systemdesc_FilterType, string deptType, int? Dept, int? fleetcode, int? ddlvessel
            , string maker, int? Function, int? Location, string maker_FilterType, int? IsActive, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            return objjobs.LibraryMachineryInforSearch(search, systemcode, systemdesc, systemdesc_FilterType, deptType, Dept, fleetcode, ddlvessel, maker, Function, Location, maker_FilterType
                , IsActive, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);
        }

        public DataTable GetMachineryInfoPopup(int systemid)
        {
            return objjobs.GetMachineryInfoPopup(systemid);
        }


        public DataTable LibraryGetJOB_eFORM_MAPPING_SEARCH(string SearchText, int? Vessel_ID, int? Job_ID
         , string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            return objjobs.LibraryGetJOB_eFORM_MAPPING_SEARCH(SearchText, Vessel_ID, Job_ID, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);
        }

        public int LibrarySaveJob_eForm_Mapping(int? ID, int? Vessel_ID, int? Job_ID, int? Form_ID, int? ChkStatus, int? UserID)
        {
            return objjobs.LibrarySaveJob_eForm_Mapping(ID, Vessel_ID, Job_ID, Form_ID, ChkStatus, UserID);
        }


        public DataTable LibraryGetJobInstructionAttachment(int VESSEL_ID, int JOB_ID)
        {
            return objjobs.LibraryGetJobInstructionAttachment(VESSEL_ID, JOB_ID);
        }

        public int LibrarySaveJobInstructionAttachment(int VESSEL_ID, int JOB_ID, string ATTACHMENT_NAME, string ATTACHMENT_PATH, int CREATED_BY, int? SIZE)
        {
            return objjobs.LibrarySaveJobInstructionAttachment(VESSEL_ID, JOB_ID, ATTACHMENT_NAME, ATTACHMENT_PATH, CREATED_BY, SIZE);
        }


        public int LibraryDeleteJobInstructionAttachment(string ATTACHMENT_NAME, int CREATED_BY)
        {
            return objjobs.LibraryDeleteJobInstructionAttachment(ATTACHMENT_NAME, CREATED_BY);
        }

        public DataSet Get_Functional_Tree_Data(int Vessel_ID)
        {
            return objjobs.Get_Functional_Tree_Data(Vessel_ID);
        }

        public DataTable Get_Functional_Tree_Data(string Function_ID, int Vessel_ID, string Equipment_Type, int? SafetyAlarm, int? Calibration, int? Critical)
        {
            if (Equipment_Type != null)
                Equipment_Type = Equipment_Type == "1" ? "AC" : "SP";

            return objjobs.Get_Functional_Tree_Data(Function_ID, Vessel_ID, Equipment_Type, SafetyAlarm ?? 0, Calibration ?? 0, Critical ?? 0);
        }
        public DataTable Get_Functional_Tree_Data_ManageSystem(string Function_ID, int Vessel_ID, string function_code, string searchText, string Equipment_Type, int? IsActive, string FormType)
        {
            if (Equipment_Type != null)
                Equipment_Type = Equipment_Type == "1" ? "AC" : "SP";

            return objjobs.Get_Functional_Tree_Data_ManageSystem(Function_ID, Vessel_ID, function_code, searchText, Equipment_Type, IsActive, FormType);
        }

        public DataTable Get_EQP_Planned_Jobs(int Vessel_ID, string SystemID, string SubSystemID, string SystemLocation, string SubSystemLocation)
        {
            return objjobs.Get_EQP_Planned_Jobs(Vessel_ID, SystemID, SubSystemID, SystemLocation, SubSystemLocation);
        }

        public DataTable Get_EQP_UnPlanned_Jobs(int Vessel_ID, string SystemID, string SubSystemID, string SystemLocation, string SubSystemLocation)
        {
            return objjobs.Get_EQP_UnPlanned_Jobs(Vessel_ID, SystemID, SubSystemID, SystemLocation, SubSystemLocation);
        }

        public DataTable Get_EQP_Requisitions(int Vessel_ID, string SystemID)
        {
            return objjobs.Get_EQP_Requisitions(Vessel_ID, SystemID);
        }

        public DataTable Get_EQP_Run_Hours(int Vessel_ID, string SystemID, string SubSystemID, string SystemLocation, string SubSystemLocation)
        {
            return objjobs.Get_EQP_Run_Hours(Vessel_ID, SystemID, SubSystemID, SystemLocation, SubSystemLocation);
        }

        public DataTable Get_EQP_Spare_Consumption(int Vessel_ID, string SystemID, string SubSystemID, string SystemLocation, string SubSystemLocation)
        {
            return objjobs.Get_EQP_Spare_Consumption(Vessel_ID, SystemID, SubSystemID, SystemLocation, SubSystemLocation);
        }

        public DataTable Get_EQP_Spare_Consumption_ManageSystem(int Vessel_ID, string SystemID, string SubSystemID)
        {
            return objjobs.Get_EQP_Spare_Consumption_ManageSystem(Vessel_ID, SystemID, SubSystemID);
        }

        public DataTable Get_Machinery_Details(int Vessel_ID, string SystemID, string SubSystemID)
        {
            return objjobs.Get_Machinery_Details(Vessel_ID, SystemID, SubSystemID);
        }

        public DataSet Get_Equipment_Location(int Vessel_ID, int SystemID, int? SubSystemID, int SystemLocation, int? SubSystemLocation)
        {
            return objjobs.Get_Equipment_Location(Vessel_ID, SystemID, SubSystemID, SystemLocation, SubSystemLocation);
        }

        public int Upd_Equipment_Replacement(int Vessel_ID, int Location_Code, int Active_Location_ID, int Spare_Location_ID, string Remark, int UserID)
        {
            return objjobs.Upd_Equipment_Replacement(Vessel_ID, Location_Code, Active_Location_ID, Spare_Location_ID, Remark, UserID);
        }

        public DataTable Get_EQP_Replacement_History(int Vessel_ID, int Location_Code)
        {
            return objjobs.Get_EQP_Replacement_History(Vessel_ID, Location_Code);
        }


        public int TEC_MOVE_SYSTEM_SUBSYSTEM(string SystemID_parent, string SystemId_ToMove)
        {
            return objjobs.TEC_MOVE_SYSTEM_SUBSYSTEM(SystemID_parent, SystemId_ToMove);
        }

        public DataTable Get_Equipment_Replacement_History(int? Vessel_ID, int? Function, int? SystemLocation, int? SubSystemLocation, int? Page_Index, int? Page_Size, ref int Is_Fetch_Count)
        {
            return objjobs.Get_Equipment_Replacement_History(Vessel_ID, Function, SystemLocation, SubSystemLocation, Page_Index, Page_Size, ref Is_Fetch_Count);
        }

        public DataTable Get_EQP_Lib_Planned_Jobs(int Vessel_ID, string SystemID, string SubSystemID, string SystemLocation, string SubSystemLocation)
        {
            return objjobs.Get_EQP_Lib_Planned_Jobs(Vessel_ID, SystemID, SubSystemID, SystemLocation, SubSystemLocation);
        }
        public DataTable Get_EQP_Lib_Planned_Jobs_ManageSystem(int Vessel_ID, string SystemID, string SubSystemID)
        {
            return objjobs.Get_EQP_Lib_Planned_Jobs_ManageSystem(Vessel_ID, SystemID, SubSystemID);
        }
        public int Upd_Copy_PMS_Data(int SRC_Vessel_ID, int TRG_Vessel_ID, int UserID, int Retain_Existing)
        {
            return objjobs.Upd_Copy_PMS_Data(SRC_Vessel_ID, TRG_Vessel_ID, UserID, Retain_Existing);
        }

        //Tushar Solanki 
        //July 10, 2015
        //New Methods for Copy Run Hour Linkage 

        //Method to get all system and subsystem based on VesselID


        public DataTable Get_System_SubsystemTreeData(int Vessel_ID)
        {
            return objjobs.Get_System_SubsystemTreeData(Vessel_ID);
        }

        public DataTable PMS_Get_DestinationSystemSubsystemTreeData(int Vessel_ID)
        {
            return objjobs.PMS_Get_DestinationSystemSubsystemTreeData(Vessel_ID);
        }

        public DataTable PMS_Get_SystemSubsystemInfo(int SourceID, string SystemType)
        {
            return objjobs.PMS_Get_SystemSubsystemInfo(SourceID, SystemType);
        }

        public int PMS_INS_EquipmentRunHours(DataTable dt, int UserID, string SourceType)
        {
            return objjobs.PMS_INS_EquipmentRunHours(dt, UserID, SourceType);
        }

        public DataSet PMS_Get_EquipmentRunningHoursInfo(int VesselID, int SystemID, int ParentSystemID)
        {
            return objjobs.PMS_Get_EquipmentRunningHoursInfo(VesselID, SystemID, ParentSystemID);
        }

        public DataTable PMS_GET_CheckIfEquipmentIsDirectlyRunHourBased(int EquipmentID, string EquipmentType)
        {
            return objjobs.PMS_GET_CheckIfEquipmentIsDirectlyRunHourBased(EquipmentID, EquipmentType);
        }

        public DataTable PMS_GET_DestinationLinks(int EquipmentID, string SystemType)
        {
            return objjobs.PMS_GET_DestinationLinks(EquipmentID, SystemType);
        }
        public int PMS_GET_IsJobRunHourBased(int System, int SubSystemID, int VesselID)
        {
            return objjobs.PMS_GET_IsJobRunHourBased(System, SubSystemID, VesselID);
        }

        public DataTable PMS_Get_CheckIfSystemExits(int SystemID)
        {
            return objjobs.PMS_Get_CheckIfSystemExits(SystemID);
        }

        public DataTable PMS_Get_CheckIfSubSystemExits(string SubSystemID)
        {
            return objjobs.PMS_Get_CheckIfSubSystemExits(SubSystemID);
        }

        public int PMS_Get_IsSystemRunHourBased(int SystemID, int VesselID)
        {
            return objjobs.PMS_Get_IsSystemRunHourBased(SystemID, VesselID);
        }

        public DataTable PMS_Get_SingleEquipmentRunningHoursInfo(int VesselID, int SystemID, int ParentID)
        {
            return objjobs.PMS_Get_SingleEquipmentRunningHoursInfo(VesselID, SystemID, ParentID);
        }
        // Added by Reshma JIT : 11222
        public int PMS_GET_JobsPerformOnLocation(int? SystemLocationID, int? SubSysLocationID, int VESSELID)
        {
            return objjobs.PMS_GET_JobsPerformOnLocation(SystemLocationID, SubSysLocationID, VESSELID);
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
            return objjobs.PMS_Get_FunctionBySearch(Search, ParentType, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);

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
            return objjobs.PMS_Get_Function(ParentType, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);
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
                return objjobs.PMS_Insert_Function(Code, ParentType, ShortCode, FunctionName, userID);
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
            return objjobs.PMS_Get_BindFunctionName(Code);

        }
        /// <summary>
        ///  For Update Function
        /// </summary>
        /// <param name="Code">Primary Key</param>
        /// <param name="ParentType">Parent Code for Function</param>
        /// <param name="ShortCode">ShortCode for function</param>
        /// <param name="FunctionName">Description</param>
        /// <param name="userID">UserID</param>
        /// <returns>Update the function in database</returns>
        public int PMS_Update_Function(int? Code, int? ParentType, string ShortCode, string FunctionName, int userID)
        {
            try
            {
                return objjobs.PMS_Update_Function(Code, ParentType, ShortCode, FunctionName, userID);
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
                return objjobs.PMS_Delete_Function(Code, userID);
            }
            catch
            {
                throw;
            }
        }
    }
}
