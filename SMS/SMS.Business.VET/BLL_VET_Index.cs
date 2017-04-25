using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SMS.Data.VET;
using System.Data;
using System.IO;

namespace SMS.Business.VET
{
    public class BLL_VET_Index
    {
        DAL_VET_Index objVetDAL = new DAL_VET_Index();

        /// <summary>
        /// Get table of vetting
        /// </summary>
        /// <param name="dtVessel">List of Vessels</param>
        /// <param name="dtVetType">List Vetting Type ID</param>
        /// <param name="dtVetIndxStatus">List OF Vetting Status</param>
        /// <param name="DueInDays">Due in Days </param>
        /// <param name="IsValid">0:All 1:- Valid 2: Invalid</param>
        /// <param name="IsOpnObs"> 0:All 1:Open 2:Close </param>
        /// <param name="dtOilMajor">List Of Oilmajor</param>
        /// <param name="dtInspector">List of Inspector</param>
        /// <param name="dtExInspector">List Of External Inspector</param>
        /// <param name="VetDateFrom">Vetting From Date</param>
        /// <param name="VetDateTo">Vetting To Date</param>
        /// <param name="dtJobStatus">List Of Job Status </param>
        /// <param name="SearchVessel">SearchByVesselName</param>
        /// <param name="sortby"> Sort By Column</param>
        /// <param name="sortdirection"> Direction for Sorting Data</param>
        /// <param name="pagenumber">Current Page Number</param>
        /// <param name="pagesize">Page Size</param>
        /// <param name="isfetchcount">Total Page Count</param>
        /// <returns></returns>
        public DataSet VET_Get_VettingIndex(DataTable dtVessel, DataTable dtVetType, DataTable dtVetIndxStatus, int? DueInDays, int? IsValid, int? IsOpnObs, DataTable dtOilMajor, DataTable dtInspector, DataTable dtExInspector, DateTime? VetDateFrom, DateTime? VetDateTo, DataTable dtJobStatus, string SearchVessel, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            try
            {
                return objVetDAL.VET_Get_VettingIndex(dtVessel, dtVetType, dtVetIndxStatus, DueInDays, IsValid, IsOpnObs, dtOilMajor, dtInspector, dtExInspector, VetDateFrom, VetDateTo, dtJobStatus, SearchVessel, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Delete Planned Vetting
        /// </summary>
        /// <param name="Vetting_ID">ID of Vetting</param>
        /// <param name="UserID">ID of Review </param>
        /// <returns></returns>
        public int VET_Del_PlannedVetting(int? Vetting_ID, int UserID)
        {
            try
            {
                return objVetDAL.VET_Del_PlannedVetting(Vetting_ID, UserID);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Update Vetting Status
        /// </summary>
        /// <param name="Vetting_ID">ID Of Vetting</param>
        /// <param name="UserID">Id of User</param>
        /// <returns></returns>
        public int VET_Upd_VettingReworkStatus(int? Vetting_ID, int UserID)
        {
            try
            {
                return objVetDAL.VET_Upd_VettingReworkStatus(Vetting_ID, UserID);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Get tool tip for open observation
        /// </summary>
        /// <param name="Vetting_ID">ID of vetting</param>
        /// <returns>Table of open observation</returns>
        public DataTable VET_Get_OpenObservationTooltip(int? Vetting_ID)
        {
            try
            {
                return objVetDAL.VET_Get_OpenObservationTooltip(Vetting_ID);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Get tool tip for close observation
        /// </summary>
        /// <param name="Vetting_ID">ID vetting</param>
        /// <returns>Table of close observation</returns>
        public DataTable VET_Get_CloseObservationTooltip(int? Vetting_ID)
        {
            try
            {
                return objVetDAL.VET_Get_CloseObservationTooltip(Vetting_ID);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Get tool tip for Note
        /// </summary>
        /// <param name="Vetting_ID">ID of Vetting</param>
        /// <returns>Table of Notes</returns>
        public DataTable VET_Get_NoteTooltip(int? Vetting_ID)
        {
            try
            {
                return objVetDAL.VET_Get_NoteTooltip(Vetting_ID);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Get tool tip for Pending Jobs for Vetting
        /// </summary>
        /// <param name="Vetting_ID">ID of Vetting</param>
        /// <returns>Table of Pending Jobs</returns>
        public DataTable VET_Get_PendingJobsTooltip(int? Vetting_ID)
        {
            try
            {
                return objVetDAL.VET_Get_PendingJobsTooltip(Vetting_ID);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Get List of questionnaire
        /// </summary>
        /// <returns></returns>
        public DataTable VET_GET_QuestionnireList()
        {
            try
            {
                return objVetDAL.VET_GET_QuestionnireList();
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Get Section list By QuestionnireId
        /// </summary>
        /// <param name="dtQuestionnaire_ID"> List of Questionnaire Id</param>
        /// <returns>Table of Sections</returns>
        public DataTable VET_Get_SectionByQuestionnireId(DataTable dtQuestionnaire_ID)
        {
            try
            {
                return objVetDAL.VET_Get_SectionByQuestionnireId(dtQuestionnaire_ID);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Get Question no By Questionnaire ID
        /// </summary>
        /// <param name="dtQuestionnaire_ID">List Questionnaire ID</param>
        /// <returns></returns>
        public DataTable VET_Get_QuestionNoByQuestionnireId(DataTable dtQuestionnaire_ID, DataTable dtSectionNo)
        {
            try
            {
                return objVetDAL.VET_Get_QuestionNoByQuestionnireId(dtQuestionnaire_ID, dtSectionNo);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Get Observation Categories
        /// </summary>
        /// <returns></returns>
        public DataTable VET_Get_ObservationCategories(string Mode)
        {
            try
            {
                return objVetDAL.VET_Get_ObservationCategories(Mode);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Get observation for Indedx
        /// </summary>
        /// <param name="dtQuestionnaire_ID">List of Questionnaire ID</param>
        /// <param name="dtSectionNO">List of Section No</param>
        /// <param name="dtQuestionNo">List of Question No</param>
        /// <param name="ObsNoteType">1: Notes 2: Observation </param>
        /// <param name="ObservationStatus">Status of observation</param>
        /// <param name="Fleet_Id">ID of Fleet</param>
        /// <param name="dtVessel">List Of Vessels</param>
        /// <param name="dtOilMajor">List of Oil Major</param>
        /// <param name="dtInspector">List of Inspector</param>
        /// <param name="dtExInspector">List External Inspector</param>
        /// <param name="dtCategories">List Categories</param>
        /// <param name="dtRiskLevel">List Risk Level</param>
        /// <param name="Search_Obs_Vessel">Search By observation and Vessel Name </param>
        /// <param name="VetDateFrom">Vetting From Date</param>
        /// <param name="VetDateTo">Vetting To Date</param>
        /// <param name="sortby"> Sort By Column</param>
        /// <param name="sortdirection"> Direction for Sorting Data</param>
        /// <param name="pagenumber">Current Page Number</param>
        /// <param name="pagesize">Size of Page</param>
        /// <param name="isfetchcount">Total Records return Count</param>
        /// <returns>Table of Observation</returns>
        public DataSet VET_Get_ObservationIndex(DataTable dtQuestionnaire_ID, DataTable dtSectionNO, DataTable dtQuestionNo, int? ObsNoteType, string ObservationStatus, int? Fleet_Id, DataTable dtVessel, DataTable dtOilMajor, DataTable dtInspector, DataTable dtExInspector, DataTable dtCategories, DataTable dtRiskLevel, string Search_Obs_Vessel, DateTime? VetDateFrom, DateTime? VetDateTo, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            try
            {
                return objVetDAL.VET_Get_ObservationIndex(dtQuestionnaire_ID, dtSectionNO, dtQuestionNo, ObsNoteType, ObservationStatus, Fleet_Id, dtVessel, dtOilMajor, dtInspector, dtExInspector, dtCategories, dtRiskLevel, Search_Obs_Vessel, VetDateFrom, VetDateTo, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Get Tool Tip For Related Jobs to Observation
        /// </summary>
        /// <param name="Question_ID">ID of Question </param>
        /// <param name="Observation_ID">ID of Observation</param>
        /// <returns>Table of related jobs</returns>
        public DataTable VET_Get_ObsIndxRelatedJobsTooltip(int? Question_ID, int? Observation_ID)
        {
            try
            {
                return objVetDAL.VET_Get_ObsIndxRelatedJobsTooltip(Question_ID, Observation_ID);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Method is used to insert observations details that is attched with worklist job
        /// </summary>
        /// <param name="Observation_ID">Observation id that is attached to worklist job</param>
        /// <param name="Jobhistory_ID">Valid worklist id</param>
        /// <param name="Vessel_ID">For which vessel worklist job is created</param>
        /// <param name="Office_ID">Valid office id</param>
        /// <param name="UserID">Login user id</param>
        /// <returns></returns>
        public int VET_INS_Assign_Job_To_Be_Vetting(int Observation_ID, int Jobhistory_ID, int Vessel_ID, int Office_ID, int UserID)
        {
            try
            {
                return objVetDAL.VET_INS_Assign_Job_To_Be_Vetting(Observation_ID, Jobhistory_ID, Vessel_ID, Office_ID, UserID);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Method is used to display observation wise jobs and vetting wise job list
        /// </summary>
        /// <param name="FilterJobsBy"> Observation ID or Vetting ID</param>
        /// <param name="Mode">Type of filter :value is  Observation or Vetting</param>
        /// <returns>Return jobs details</returns>
        public DataTable VET_Get_Vetting_Jobs_Details(int FilterJobsBy, string Mode)
        {
            try
            {
                return objVetDAL.VET_Get_Vetting_Jobs_Details(FilterJobsBy, Mode);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Method is used to fetch vetting details for report
        /// </summary>
        /// <param name="Vetting_ID">Selected vetting ID from vetting index</param>
        /// <param name="UserID">Login user id</param>
        /// <returns>retrun report details in html format</returns>
        public DataTable VET_Get_Vetting_Report(int Vetting_ID, int UserID)
        {
            try
            {
                return objVetDAL.VET_Get_Vetting_Report(Vetting_ID, UserID);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Get Vetting attachement type
        /// </summary>
        /// <returns>table attachment type</returns>
        public DataTable VET_Get_VettingAttachmentType()
        {
            try
            {
                return objVetDAL.VET_Get_VettingAttachmentType();
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Insert Vetting attachements
        /// </summary>
        /// <param name="Vetting_ID">ID of Vetting</param>
        /// <param name="Vetting_AttahmtType_ID">ID of Attachement</param>
        /// <param name="Attachement_Name">Attachement NAme </param>
        /// <param name="Attachement_Path">Attachement Path</param>
        /// <param name="userID">ID of User</param>
        /// <returns></returns>
        public int VET_Ins_VettingAttachments(int? Vetting_ID, int? Vetting_AttahmtType_ID, string Attachement_Name, string Attachement_Path, int userID)
        {
            try
            {
                return objVetDAL.VET_Ins_VettingAttachments(Vetting_ID, Vetting_AttahmtType_ID, Attachement_Name, Attachement_Path, userID);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Check if  Attachement exists
        /// </summary>
        /// <param name="Vetting_ID">ID of vetting</param>
        /// <param name="Vetting_AttahmtType_ID">ID of Vetting Attachement</param>
        /// <returns>Table os attchement</returns>
        public DataSet VET_Get_VettingExistAttachment(int? Vetting_ID, int? Vetting_AttahmtType_ID)
        {
            try
            {
                return objVetDAL.VET_Get_VettingExistAttachment(Vetting_ID, Vetting_AttahmtType_ID);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Get Attchement
        /// </summary>
        /// <param name="Vetting_ID">ID of Vetting</param>
        /// <returns></returns>
        public DataTable VET_Get_VettingAttachment(int? Vetting_ID)
        {
            try
            {
                return objVetDAL.VET_Get_VettingAttachment(Vetting_ID);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Update Vetting Attachments
        /// </summary>
        /// <param name="Vetting_ID">ID of Vetting</param>
        /// <param name="Vetting_AttahmtType_ID">ID of Attchement Type</param>
        /// <param name="Attachement_Name">Attachement NAme</param>
        /// <param name="Attachement_Path">Attahcement Path</param>
        /// <param name="userID">ID of User</param>
        /// <returns></returns>
        public int VET_Upd_VettingAttachments(int? Vetting_ID, int? Vetting_AttahmtType_ID, string Attachement_Name, string Attachement_Path, int userID)
        {
            try
            {
                return objVetDAL.VET_Upd_VettingAttachments(Vetting_ID, Vetting_AttahmtType_ID, Attachement_Name, Attachement_Path, userID);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Delete Vetting Attchment
        /// </summary>
        /// <param name="Vetting_Attachmt_ID">ID of Attchment</param>
        /// <param name="userID">ID of User</param>
        /// <returns></returns>
        public int VET_Del_VettingAttachment(int? Vetting_Attachmt_ID, int userID)
        {
            try
            {
                return objVetDAL.VET_Del_VettingAttachment(Vetting_Attachmt_ID, userID);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Get Section List by Vetting ID
        /// </summary>
        /// <param name="Vetting_ID">ID of vetting</param>
        /// <returns></returns>
        public DataTable VET_Get_SectionListByVettingId(int? Vetting_ID, string Mode)
        {
            try
            {
                return objVetDAL.VET_Get_SectionListByVettingId(Vetting_ID, Mode);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Get Question No By Vetting ID
        /// </summary>
        /// <param name="Vetting_ID">ID of Vetting </param>
        /// <returns></returns>
        public DataTable VET_Get_QuestionNoByVettingId(int? Vetting_ID, string Mode)
        {
            try
            {
                return objVetDAL.VET_Get_QuestionNoByVettingId(Vetting_ID, Mode);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Get Observation Type Lsut
        /// </summary>
        /// <returns></returns>
        public DataTable VET_Get_ObservationTypeList()
        {
            try
            {
                return objVetDAL.VET_Get_ObservationTypeList();
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Get Related Open Observation Count
        /// </summary>
        /// <returns></returns>
        public DataTable VET_Get_RelatedOpenObsCount()
        {
            try
            {
                return objVetDAL.VET_Get_RelatedOpenObsCount();
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Get Question By Question ID
        /// </summary>
        /// <param name="Question_ID">ID of Question</param>
        /// <returns></returns>
        public DataTable VET_Get_QuestionByQuestionNo(int? Question_ID)
        {
            try
            {
                return objVetDAL.VET_Get_QuestionByQuestionNo(Question_ID);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Get Observation
        /// </summary>
        /// <param name="Vetting_ID">ID of Vetting</param>
        /// <param name="Question_ID">ID of Question </param>
        /// <param name="Observation_ID">ID of Observation</param>
        /// <returns></returns>
        public DataTable VET_Get_Observation(int? Vetting_ID, int? Question_ID, int? Observation_ID)
        {
            try
            {
                return objVetDAL.VET_Get_Observation(Vetting_ID, Question_ID, Observation_ID);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Get Response
        /// </summary>
        /// <param name="Vetting_ID">ID of Vetting</param>
        /// <param name="Observation_ID">ID of Observation</param>
        /// <returns>Get table Response</returns>
        public DataSet VET_Get_Response(int? Vetting_ID, int? Observation_ID)
        {
            try
            {
                return objVetDAL.VET_Get_Response(Vetting_ID, Observation_ID);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Get Jobs related to observation
        /// </summary>
        /// <param name="Vetting_ID">ID of Vetting</param>
        /// <param name="Observation_ID">ID of Observation</param>
        /// <returns>Table of Related jobs</returns>
        public DataSet VET_Get_ObsRelatedJobs(int? Vetting_ID, int? Observation_ID)
        {
            try
            {
                return objVetDAL.VET_Get_ObsRelatedJobs(Vetting_ID, Observation_ID);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Insert Observation or Note
        /// </summary>
        /// <param name="Vetting_ID">ID of Vetting </param>
        /// <param name="Question_ID">ID of Question </param>
        /// <param name="Observation_Type_ID">IF of Observation Type ID</param>
        /// <param name="Description">Description </param>
        /// <param name="OBSCategory_ID">ID of Observation Category</param>
        /// <param name="Risk_Level"> Level of Risk</param>
        /// <param name="Status">Status of Observation</param>
        /// <param name="UserID">ID of User</param>
        /// <returns>Tabel of Observation ID </returns>
        public DataTable VET_Ins_Observation_Note(int? Vetting_ID, int? Question_ID, int? Observation_Type_ID, string Description, int? OBSCategory_ID, int? Risk_Level, string Status, int UserID)
        {
            try
            {
                return objVetDAL.VET_Ins_Observation_Note(Vetting_ID, Question_ID, Observation_Type_ID, Description, OBSCategory_ID, Risk_Level, Status, UserID);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Update Observation or Note
        /// </summary>
        /// <param name="Observation_ID"> ID of obsetrvation</param>
        /// <param name="Vetting_ID"> ID of Vetting</param>
        /// <param name="Question_ID">ID of Question</param>
        /// <param name="Observation_Type_ID">ID Observation Type</param>
        /// <param name="Description">Description</param>
        /// <param name="OBSCategory_ID">ID of Observation Category</param>
        /// <param name="Risk_Level"> Level of Risk</param>
        /// <param name="Status">Status of Observation</param>
        /// <param name="UserID">ID of User</param>
        /// <returns></returns>
        public int VET_Upd_Observation_Note(int? Observation_ID, int? Vetting_ID, int? Question_ID, int? Observation_Type_ID, string Description, int? OBSCategory_ID, int? Risk_Level, string Status, int UserID)
        {
            try
            {
                return objVetDAL.VET_Upd_Observation_Note(Observation_ID, Vetting_ID, Question_ID, Observation_Type_ID, Description, OBSCategory_ID, Risk_Level, Status, UserID);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Insert Response  Attachement
        /// </summary>
        /// <param name="Response_ID">ID of resposne</param>
        /// <param name="Attachement_Name">attchment name</param>
        /// <param name="Attachement_Path">Attachment Path</param>
        /// <param name="UserID">ID of user who add attchement</param>
        /// <returns></returns>
        public int VET_Ins_ResponseAttachment(int? Response_ID, string Attachement_Name, string Attachement_Path, int UserID)
        {
            try
            {
                return objVetDAL.VET_Ins_ResponseAttachment(Response_ID, Attachement_Name, Attachement_Path, UserID);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Insert Response
        /// </summary>
        /// <param name="Observation_ID">ID Observation </param>
        /// <param name="Response">Response</param>
        /// <param name="UserID">ID Of User</param>
        /// <returns></returns>
        public DataTable VET_Ins_Response(int? Observation_ID, string Response, int UserID)
        {
            try
            {
                return objVetDAL.VET_Ins_Response(Observation_ID, Response, UserID);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Get Reponse Attchment
        /// </summary>
        /// <param name="Response_ID">ID Reponse</param>
        /// <returns></returns>
        public DataTable VET_Get_ResponseAttachment(int? Response_ID)
        {
            try
            {
                return objVetDAL.VET_Get_ResponseAttachment(Response_ID);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Get Jobs related to Observation
        /// </summary>
        /// <param name="Vessel_ID">ID of Vessel</param>
        /// <param name="Observation_ID">ID of Observation</param>
        /// <returns></returns>
        public DataSet VET_Get_ObservationWorklistJobs(int Vessel_ID, int? Observation_ID)
        {
            return objVetDAL.VET_Get_ObservationWorklistJobs(Vessel_ID, Observation_ID);
        }
        /// <summary>
        /// Insert Assign Jobs
        /// </summary>
        /// <param name="dtInspectionWorklist">list Of Vetting Worklist Mapping ID</param>
        /// <param name="Observation_ID">ID of Observation</param>
        /// <param name="UserID">ID of User</param>
        /// <returns></returns>
        public int VET_INS_Obs_Assign_Jobs(DataTable dtInspectionWorklist, int Observation_ID, int UserID)
        {
            return objVetDAL.VET_INS_Obs_Assign_Jobs(dtInspectionWorklist, Observation_ID, UserID);
        }

        /// <summary>
        /// Unlink assig jobs
        /// </summary>
        /// <param name="OBSJobID">Mapping ID Observation and Job</param>
        /// <param name="UserId">ID of User</param>
        /// <returns></returns>
        public int VET_Upd_UnlinkWorklistJobs(int OBSJobID, int UserId)
        {
            return objVetDAL.VET_Upd_UnlinkWorklistJobs(OBSJobID, UserId);
        }

        /// <summary>
        /// GEt List Of Port
        /// </summary>
        /// <returns></returns>
        public DataTable VET_Get_PortList()
        {
            return objVetDAL.VET_Get_PortList();
        }

        /// <summary>
        /// Get Inspector List By  Vetting Type
        /// </summary>
        /// <param name="Vetting_Type_ID">Id of Vetting Type</param>
        /// <returns></returns>
        public DataTable VET_Get_InspectorListByVettingType(int Vetting_Type_ID)
        {
            return objVetDAL.VET_Get_InspectorListByVettingType(Vetting_Type_ID);
        }

        /// <summary>
        /// Update Vetting Details
        /// </summary>
        /// <param name="Vetting_ID">ID of Vetting </param>
        /// <param name="Vetting_Name">Name of vetting</param>
        /// <param name="Vetting_Date">date of vetting</param>
        /// <param name="Vetting_Type_ID">ID of Vetting Type</param>
        /// <param name="Vetting_Type_Name">Name of Vetting Type</param>
        /// <param name="Inspector_ID">ID of Inspector</param>
        /// <param name="OilMajor_ID">ID of Oil Major</param>
        /// <param name="Port_ID">ID of Port</param>
        /// <param name="Port_Call_ID">Port call ID of Port</param>
        /// <param name="No_Of_Days">Number of days</param>
        /// <param name="Response_Next_Due">due date of next response</param>
        /// <param name="UserID">ID of User ID</param>
        /// <returns></returns>
        public int VET_Upd_PerformVettingDetails(int? Vetting_ID, string Vetting_Name, DateTime? Vetting_Date, int? Vetting_Type_ID, string Vetting_Type_Name, int? Inspector_ID, int? OilMajor_ID, int? Port_ID, int? Port_Call_ID, int? No_Of_Days, DateTime? Response_Next_Due, int? UserID)
        {
            return objVetDAL.VET_Upd_PerformVettingDetails(Vetting_ID, Vetting_Name, Vetting_Date, Vetting_Type_ID, Vetting_Type_Name, Inspector_ID, OilMajor_ID, Port_ID, Port_Call_ID, No_Of_Days, Response_Next_Due, UserID);
        }
        /// <summary>
        /// Update Vetting Details
        /// </summary>
        /// <param name="Vetting_ID">ID of Vetting </param>
        /// <param name="Vetting_Name">Name of vetting</param>
        /// <param name="Vetting_Type_ID">ID of Vetting Type</param>
        /// <param name="Vetting_Type_Name">Name of Vetting Type</param>
        /// <param name="Vetting_Date">date of vetting</param>
        /// <param name="Questionnaire_ID"> ID of Questionnaire</param>
        /// <param name="Inspector_ID">ID of Inspector</param>
        /// <param name="OilMajor_ID">ID of Oil Major</param>
        /// <param name="Port_ID">ID of Port</param>
        /// <param name="No_Of_Days">Number of days</param>
        /// <param name="UserID">ID of User ID</param>
        /// <returns></returns>
        public int VET_Upd_VettingDetails(int Vetting_ID, string Vetting_Name, int Vetting_Type_ID, string Vetting_Type_Name, DateTime Vetting_Date, int Questionnaire_ID, int Inspector_ID, int OilMajor_ID, int Port_ID, int No_Of_Days, int UserID)
        {
            return objVetDAL.VET_Upd_VettingDetails(Vetting_ID, Vetting_Name, Vetting_Type_ID, Vetting_Type_Name, Vetting_Date, Questionnaire_ID, Inspector_ID, OilMajor_ID, Port_ID, No_Of_Days, UserID);
        }

        /// <summary>
        /// Get Questionnaire ID by Question ID
        /// </summary>
        /// <param name="Question_ID">ID of Question</param>
        /// <returns></returns>
        public DataTable VET_Get_QuestionnaireIdByQuestionId(int? Question_ID)
        {
            try
            {
                return objVetDAL.VET_Get_QuestionnaireIdByQuestionId(Question_ID);
            }
            catch
            {
                throw;
            }
        }


        /// <summary>
        /// Get Perform Vetting Details
        /// </summary>
        /// <param name="Vetting_ID">ID of Vetting</param>
        /// <returns></returns>
        public DataTable VET_Get_Perform_Vetting_Detail(int Vetting_ID)
        {
            return objVetDAL.VET_Get_Perform_Vetting_Detail(Vetting_ID);
        }

        /// <summary>
        /// Get Published Questionnaire List
        /// </summary>
        /// <param name="VesselID">ID of Vessel</param>
        /// <param name="Vetting_Type_ID">ID of Vetting Type</param>
        /// <returns></returns>
        public DataTable VET_GET_Published_QuestionnireList(int VesselID, int Vetting_Type_ID)
        {
            return objVetDAL.VET_GET_Published_QuestionnireList(VesselID, Vetting_Type_ID);
        }

        /// <summary>
        /// Get Completed Jobs list for tool tip
        /// </summary>
        /// <param name="Vetting_ID">ID of Vetting </param>
        /// <param name="Question_ID">ID Question </param>
        /// <param name="Observation_ID">ID of Observation</param>
        /// <returns></returns>
        public DataTable VET_Get_CompletedJobsTooltip(int? Vetting_ID, int? Question_ID, int? Observation_ID)
        {
            try
            {
                return objVetDAL.VET_Get_CompletedJobsTooltip(Vetting_ID, Question_ID, Observation_ID);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Get Verified Jobs list for tool tip
        /// </summary>
        /// <param name="Vetting_ID">ID of Vetting </param>
        /// <param name="Question_ID">ID Question </param>
        /// <param name="Observation_ID">ID of Observation</param>
        /// <returns></returns>
        public DataTable VET_Get_VerifiedJobsTooltip(int? Vetting_ID, int? Question_ID, int? Observation_ID)
        {
            try
            {
                return objVetDAL.VET_Get_VerifiedJobsTooltip(Vetting_ID, Question_ID, Observation_ID);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Get Deferred Jobs list for tool tip
        /// </summary>
        /// <param name="Vetting_ID">ID of Vetting </param>
        /// <param name="Question_ID">ID Question </param>
        /// <param name="Observation_ID">ID of Observation</param>
        /// <returns></returns>
        public DataTable VET_Get_DefferedJobsTooltip(int? Vetting_ID, int? Question_ID, int? Observation_ID)
        {
            try
            {
                return objVetDAL.VET_Get_DefferedJobsTooltip(Vetting_ID, Question_ID, Observation_ID);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Get Observation response list for tool tip
        /// </summary>
        /// <param name="Observation_ID">ID of Observation</param>
        /// <returns></returns>
        public DataTable VET_Get_ObsResponseTooltip(int? Observation_ID)
        {
            try
            {
                return objVetDAL.VET_Get_ObsResponseTooltip(Observation_ID);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Insert New Vetting
        /// </summary>
        /// <param name="Vessel_Id">ID of Vessel</param>
        /// <param name="Vetting_Date">Date Of Vetting</param>
        /// <param name="Vetting_Type_ID">ID Vetting Type</param>
        /// <param name="Vetting_Type_Name">Name Of Vetting Type</param>
        /// <param name="Questionaire_ID">ID of Questionnaire</param>
        /// <param name="Oil_Major_ID">ID of Oil Major</param>
        /// <param name="Inspector_ID">ID of Inspector</param>
        /// <param name="Port_ID"></param>
        /// <param name="PortCallID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public int VET_Ins_Vetting(int? Vessel_Id, DateTime? Vetting_Date, int? Vetting_Type_ID, string Vetting_Type_Name, int? Questionaire_ID, int? Oil_Major_ID, int? Inspector_ID, int? Port_ID, int? PortCallID, int? UserID, ref int ReturnValue)
        {
            try
            {
                return objVetDAL.VET_Ins_Vetting(Vessel_Id, Vetting_Date, Vetting_Type_ID, Vetting_Type_Name, Questionaire_ID, Oil_Major_ID, Inspector_ID, Port_ID, PortCallID, UserID, ref ReturnValue);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Check if Vetting Exists
        /// </summary>
        /// <param name="Vessel_Id">ID Vetting </param>
        /// <param name="Vetting_Date">Date Of Vetting</param>
        /// <param name="Vetting_Type_ID">ID of Vetting Type</param>
        /// <returns></returns>
        public DataTable VET_Get_ExistVetting(int? Vessel_Id, DateTime? Vetting_Date, int? Vetting_Type_ID, int? VettingID)
        {
            try
            {
                return objVetDAL.VET_Get_ExistVetting(Vessel_Id, Vetting_Date, Vetting_Type_ID, VettingID);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Get Question No By Vetting ID & Section No
        /// </summary>
        /// <param name="Vetting_ID">ID of Vetting </param>
        /// <param name="Section_No">ID of Section_No </param>
        /// <returns></returns>
        public DataTable VET_Get_QuestionByVettingId_SectionNo(int? Vetting_ID, int? Section_No, string Mode)
        {
            try
            {
                return objVetDAL.VET_Get_QuestionByVettingId_SectionNo(Vetting_ID, Section_No, Mode);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Method is used to insert observations which is import by user.
        /// </summary>
        /// <param name="Vetting_ID">import data on which vetting id against.</param>
        /// <param name="Questionaire_ID">Questionnaire id of vetting</param>
        /// <param name="Vessel_Name">for which veseel</param>
        /// <param name="Vetting_Date">vetting date</param>
        /// <param name="dtQuestion">it has three columns data- question number, type and inspector comments</param>
        /// <param name="UserID">login users id</param>
        /// <returns></returns>
        public int VET_Ins_Import_Obs(int? Vetting_ID, int? Questionaire_ID, DataTable dtQuestion, int? UserID, ref int ReturnValue)
        {
            try
            {
                return objVetDAL.VET_Ins_Import_Obs(Vetting_ID, Questionaire_ID, dtQuestion, UserID, ref ReturnValue);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Procedure is used to check wheather provided question details is exists or not
        /// </summary>
        /// <param name="Questionaire_ID">questionnaire id for this vetting</param>
        /// <param name="QuestionNum">valid question number</param>
        /// <param name="ReturnValue">return question id </param>    
        public int VET_Questionnaire_Exists(int? Questionaire_ID, string QuestionNum, ref int ReturnValue)
        {
            try
            {
                return objVetDAL.VET_Questionnaire_Exists(Questionaire_ID, QuestionNum, ref ReturnValue);
            }
            catch
            {
                throw;
            }

        }
        /// <summary>
        /// Procedure is used to get questionnaire number from questionniare id
        /// </summary>
        /// <param name="Questionaire_ID">questionnaire id of vetting</param>
        /// <param name="QuestionnaireNum">question number</param>     
        public int VET_Get_QuestNumber_By_QuestionnaireID(int? Questionaire_ID, ref int QuestionnaireNum)
        {
            try
            {
                return objVetDAL.VET_Get_QuestNumber_By_QuestionnaireID(Questionaire_ID, ref QuestionnaireNum);
            }
            catch
            {
                throw;
            }

        }
        /// <summary>
        /// Insert import attachment
        /// </summary>
        /// <param name="Vetting_ID">vetting id for which is import is done</param>
        /// <param name="Attachement_Name">attachment path file name</param>
        /// <param name="Attachement_Path">path of file</param>
        /// <param name="userID">log user id</param>  
        public int VET_Ins_AttatmentImport(int? Vetting_ID, string Attachement_Name, string Attachement_Path, int userID)
        {
            try
            {
                return objVetDAL.VET_Ins_AttatmentImport(Vetting_ID, Attachement_Name, Attachement_Path, userID);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 	Insert import ERROR LOG details
        /// </summary>
        /// <param name="Vetting_ID"></param>
        /// <param name="Attachement_Name">attachment path file name</param>
        /// <param name="userID">log user id</param>      
        public int VET_Ins_ImportObs_ErrorLog(int? Vetting_ID, string Attachement_Path, int userID)
        {
            try
            {
                return objVetDAL.VET_Ins_ImportObs_ErrorLog(Vetting_ID, Attachement_Path, userID);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// get  import ERROR LOG details
        /// </summary>
        /// <param name="Vetting_ID">vetting id for which is import is done</param> 
        public DataTable VET_Get_ImportObs_ErrorLog(int? Vetting_ID)
        {
            try
            {
                return objVetDAL.VET_Get_ImportObs_ErrorLog(Vetting_ID);
            }
            catch
            {
                throw;
            }
        }
    }
    

    public class ImportObservation : System.Web.UI.Page
    {     

        DAL_VET_Index objVetDAL = new DAL_VET_Index();
        int VettingID = 0;
        bool ErrorLog = false;
        private int GetSessionUserID()
        {
            if (Session["USERID"] != null)
                return int.Parse(Session["USERID"].ToString());
            else
                return 0;
        }

        /// <summary>
        /// Method is used to validate table and column.Check wheather table and columns are exists or not
        /// </summary>
        /// <param name="ds">read xml in to dataset</param>
        /// <returns>return 1 if all tables and columns are present</returns>
        public string ImportValidation(DataSet ds, int Vetting_ID)
        {
            VettingID = Vetting_ID;
            StringBuilder sbValidationMessage = new StringBuilder();
            bool isvalid = true;

            try
            {

                if (ds.Tables.Contains("Header"))
                {
                    if (ds.Tables["Header"].Columns.Contains("DocumentType") == false)
                    {
                        ImportExceptionLog("Table Name Header ,Column Name: DocumentType - Not Found.");
                        isvalid = false;
                    }
                    if (ds.Tables.Contains("Attribute"))
                    {

                        if (ds.Tables.Contains("Question"))
                        {
                            if (ds.Tables["Question"].Columns.Contains("questionNum") == false)
                            {
                                ImportExceptionLog("Table Name Question ,Column Name: questionNum - Not Found.");
                                isvalid = false;
                            }

                            if (ds.Tables["Question"].Columns.Contains("InspectorComments") == false)
                            {
                                ImportExceptionLog("Table Name Question ,Column Name: InspectorComments - Not Found.");
                                isvalid = false;
                            }

                            if (ds.Tables["Question"].Columns.Contains("InspectorObservations") == false)
                            {
                                ImportExceptionLog("Table Name Question ,Column Name: InspectorObservations - Not Found.");
                                isvalid = false;
                            }

                        }
                        else
                        {
                            ImportExceptionLog("Table Name : Question - Not Found.");
                            isvalid = false;
                        }

                        if (ds.Tables["Attribute"].Rows.Count > 0)
                        {
                            if (ds.Tables["Attribute"].Columns.Contains("Key") == false)
                            {
                                ImportExceptionLog("Table Name : Attribute , Column Name: Key - Not Found.");
                                isvalid = false;
                            }

                            if (ds.Tables["Attribute"].Columns.Contains("Value") == false)
                            {
                                ImportExceptionLog("Table Name: Attribute , Column Name: Value - Not Found.");
                                isvalid = false;
                            }

                            DataRow[] dratt = ds.Tables["Attribute"].Select("Key='Vessel name'");
                            if (dratt.Length <= 0)
                            {
                                ImportExceptionLog("Table Name: Attribute , Column Name: Key ,Vessel name- Not Found.");
                                isvalid = false;
                            }
                            DataRow[] drInspdt = ds.Tables["Attribute"].Select("Key='Inspection date'");
                            if (drInspdt.Length <= 0)
                            {
                                ImportExceptionLog("Table Name:Attribute , Column Name: Key ,Inspection date - Not Found.");
                                isvalid = false;
                            }
                            DataRow[] drPort = ds.Tables["Attribute"].Select("Key='Inspection port'");
                            if (drPort.Length <= 0)
                            {
                                ImportExceptionLog("Table Name:Attribute , Column Name: Key ,Inspection port - Not Found.");
                                isvalid = false;
                            }
                        }

                    }
                    else
                    {

                        ImportExceptionLog("Table Name : Attribute - Not Found.");
                        isvalid = false;
                    }
                }
                else
                {
                    ImportExceptionLog("Table Name : Header - Not Found.");
                    isvalid = false;

                }


            }
            catch (Exception ex)
            {

                UDFLib.WriteExceptionLog(ex);
                sbValidationMessage.Append("Exception:Invalid file.");
            }

            if (isvalid == false)
                sbValidationMessage.Append("Invalid file.");

            return sbValidationMessage.ToString();
        }

        /// <summary>
        /// Method is used to check columns are not null or empty
        /// </summary>
        /// <param name="dsvalue">read xml in to dataset</param>
        /// <returns>return 1 if all columns have value</returns>
        public Dictionary<string, string> ImportValueValidation(DataSet dsvalue, string Questionnaire, string VesselName, string PortName, string VettingDate, int Vetting_ID, ref int IsDataInValid, ref int IsObsExists)
        {
            VettingID= Vetting_ID ;
            Dictionary<string, string> objDicResult = new Dictionary<string, string>();
           

            StringBuilder sbValidationMessage = new StringBuilder();
            bool isvalid = true;
            try
            {

                BLL_VET_Index objIndex = new BLL_VET_Index();

                if (dsvalue.Tables.Count > 0)
                {
                    if (dsvalue.Tables["Header"].Rows[0]["DocumentType"].ToString() == "")
                    {
                        ImportExceptionLog("Table Name:Attribute , Column Name: DocumentType value not found.");
                        isvalid = false;
                    }
                    else
                    {
                        int QuestionnaireNum = 0;
                        int QuestionnaireNumber = objIndex.VET_Get_QuestNumber_By_QuestionnaireID(UDFLib.ConvertToInteger(Questionnaire), ref QuestionnaireNum);
                        if (QuestionnaireNumber != Convert.ToInt32(dsvalue.Tables["Header"].Rows[0]["DocumentType"]))
                        {
                            ImportExceptionLog("Table Name:Header , Column Name: DocumentType :" + dsvalue.Tables["Header"].Rows[0]["DocumentType"].ToString() + " data not match with existing data.");
                            objDicResult.Add("QUESTIONNAIRE", "Questionnaire number does not exist in the system");
                            isvalid = false;
                        }
                        else
                        {

                            DataRow[] drQuestNum = dsvalue.Tables["Question"].Select("questionNum IS NULL OR questionNum =''");
                            if (drQuestNum.Length > 0)
                            {
                                for (int j = 0; j < drQuestNum.Length; j++)
                                {
                                    ImportExceptionLog("Table Name:Question , questionNum not found for Element key:" + drQuestNum[j][0].ToString() + ".");

                                }
                            }

                            DataRow[] dratt = dsvalue.Tables["Attribute"].Select("Key='Vessel name'");
                            if (dratt.Length > 0)
                            {
                                if (dratt[0][1].ToString() != VesselName)
                                {
                                    ImportExceptionLog("Table Name:Attribute , Column Name: Key ,Vessel name :" + dratt[0][1].ToString() + " does not match with vetting.");
                                    IsDataInValid = 1;

                                }
                            }                          

                            DataRow[] drInspdt = dsvalue.Tables["Attribute"].Select("Key='Inspection date'");
                            if (drInspdt.Length > 0 )
                            {
                                if (UDFLib.ConvertUserDateFormat(drInspdt[0][1].ToString()) != VettingDate)
                                {
                                    ImportExceptionLog("Table Name:Attribute , Column Name: Key ,Inspection date :" + drInspdt[0][1].ToString() + " does not match with vetting.");
                                    IsDataInValid = 1;

                                }
                            }
                           
                            DataRow[] drPort = dsvalue.Tables["Attribute"].Select("Key='Inspection port'");
                            if (drPort.Length > 0)
                            {
                                if ((drPort[0][1].ToString().Trim()) != PortName)
                                {
                                    ImportExceptionLog("Table Name:Attribute , Column Name: Key ,Inspection port :" + drPort[0][1].ToString() + " does not match with vetting.");
                                    IsDataInValid = 1;

                                }
                            }                        
                                                      
                            BLL_VET_VettingLib objVett = new BLL_VET_VettingLib();
                            DataTable dtobs = objVett.VET_Get_ObservationList(Vetting_ID);
                            if (dtobs.Rows.Count > 0)
                            {
                                IsObsExists = 1;
                                objDicResult.Add("ISOBSEXISTS", "ObservationExists");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                UDFLib.WriteExceptionLog(ex);
                sbValidationMessage.Append("Exception:Invalid file.");
            }
            if (isvalid == false)
                sbValidationMessage.Append("Invalid file.");

            objDicResult.Add("ERRORMESSAGE", sbValidationMessage.ToString());

            return objDicResult;

        }

        /// <summary>
        /// Method is used to save imported xml records
        /// </summary>
        /// <param name="ds">read xml in to dataset</param>
        /// <returns>return 1 if successfully insert all records</returns>
        /// 
        public string SaveImportObservation(DataSet ds, string Questionnaire, int Vetting_ID)
        {
            VettingID = Vetting_ID;
            StringBuilder sbValidationMessage = new StringBuilder();
            bool isvalid = true;

            int result = 0;
            try
            {
                int ReturnValue = 0;

                DataTable dtQuestion = new DataTable();
                dtQuestion.Columns.Add("questionNum");
                dtQuestion.Columns.Add("Type");
                dtQuestion.Columns.Add("ObsDescription");
                dtQuestion.Columns.Add("QuestionID");
                int i = 0;
                DataRow[] drQuestNum = ds.Tables["Question"].Select("questionNum IS NOT NULL AND questionNum <>'' ");
                if (drQuestNum.Length > 0)
                {
                    foreach (DataRow sourcerow in ds.Tables["Question"].Select("questionNum IS NOT NULL AND questionNum <>'' "))
                    {

                        if ((sourcerow["InspectorComments"].ToString().Trim() != "" && sourcerow["InspectorComments"] != null) || (sourcerow["InspectorObservations"].ToString().Trim() != "" && sourcerow["InspectorObservations"] != null))
                        {
                            string ObsText = string.Empty;
                            DataRow destRow = dtQuestion.NewRow();
                            destRow["questionNum"] = sourcerow["questionNum"];
                            if (sourcerow["InspectorComments"].ToString().Trim() != "" && sourcerow["InspectorComments"] != null)
                            {
                                destRow["Type"] = "Note";
                                destRow["ObsDescription"] = sourcerow["InspectorComments"];
                                ObsText = sourcerow["InspectorComments"].ToString();
                            }
                            else if (sourcerow["InspectorObservations"].ToString().Trim() != "" && sourcerow["InspectorObservations"] != null)
                            {
                                destRow["Type"] = "Observation";
                                destRow["ObsDescription"] = sourcerow["InspectorObservations"];
                                ObsText = sourcerow["InspectorObservations"].ToString();
                            }
                          
                                String[] substrings = sourcerow["questionNum"].ToString().Split('.');
                                if (substrings.Length > 4)
                                {
                                    ImportExceptionLog("Table Name:Question , Column Name: questionNum ,Invalid Question number:" + sourcerow["questionNum"].ToString() + ".");

                                }
                                else
                                {
                                    int QuestionID = Convert.ToInt32(objVetDAL.VET_Questionnaire_Exists(UDFLib.ConvertToInteger(Questionnaire), sourcerow["questionNum"].ToString(), ref ReturnValue));
                                    if (QuestionID == 0)
                                    {
                                        ImportExceptionLog("The following observations and notes could not be imported, Question number : " + sourcerow["questionNum"].ToString() + " : " + ObsText + ".");

                                    }
                                    else
                                    {
                                        destRow["QuestionID"] = QuestionID;
                                        dtQuestion.Rows.Add(destRow);
                                    }
                                   
                                }
                           

                        }
                    }
                  
                    if (dtQuestion.Rows.Count > 0)
                        result = objVetDAL.VET_Ins_Import_Obs(VettingID, UDFLib.ConvertToInteger(Questionnaire), dtQuestion, GetSessionUserID(), ref ReturnValue);
                    if (result == 0)
                    {
                        ImportExceptionLog("Error in insert Observations.");
                        isvalid = false;
                    }
                }
                else
                {
                    ImportExceptionLog("Table Name:Question , Column Name: questionNum ,Invalid Question number.");
                    isvalid = false;

                }

            }
            catch (Exception ex)
            {

                UDFLib.WriteExceptionLog(ex);
                sbValidationMessage.Append("Exception:Invalid file.");
            }

            if (isvalid == false)
                sbValidationMessage.Append("Invalid file.");

            return sbValidationMessage.ToString();

        }

        /// <summary>
        /// Method is used to error log
        /// </summary>
        /// <param name="Errormsg">error message</param>
        public void ImportExceptionLog(string Errormsg)
        {
            try
            {
                string ErrorPath = string.Empty;
                string ExceptionLogFileName = string.Empty;
                string FilePath = string.Empty;
                if (!Directory.Exists(Server.MapPath("~/uploads/Vetting/ImportExceptionLog")))
                    Directory.CreateDirectory(Server.MapPath("~/uploads/Vetting/ImportExceptionLog"));
                ErrorPath = Server.MapPath("~/uploads/Vetting/ImportExceptionLog");
                ExceptionLogFileName = ErrorPath + "\\Log_Import_VID_" + VettingID + ".txt";
                FilePath = "~/uploads/Vetting/ImportExceptionLog/Log_Import_VID_" +VettingID + ".txt";
                objVetDAL.VET_Ins_ImportObs_ErrorLog(VettingID, FilePath, GetSessionUserID());
                using (System.IO.StreamWriter strmWriter = new System.IO.StreamWriter(ExceptionLogFileName, true))
                {
                    if (ErrorLog == false)
                    {
                        strmWriter.WriteLine("-------------------------------------------------------------------------------");
                        strmWriter.WriteLine("On " + DateTime.Now.ToString());
                        ErrorLog = true;
                    }

                    strmWriter.WriteLine(Errormsg);
                }

            }
            catch (Exception ex)
            {
                UDFLib.WriteExceptionLog(ex);
            }

        }
    }
}
