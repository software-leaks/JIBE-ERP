using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SMS.Data.Inspection;

namespace SMS.Business.Inspection
{
    public partial class BLL_Tec_Inspection
    {
        DAL_Tec_Inspection objFileAttachment = new DAL_Tec_Inspection();
        public DataTable Get_Dept_OnShip()
        {
            return objFileAttachment.Get_Dept_OnShip();
        }
        public DataTable Get_Dept_InOffice()
        {
            return objFileAttachment.Get_Dept_InOffice();
        }
        public DataSet INSP_Get_Worklist(DataTable dtFilter, ref int Record_Count)
        {
            return objFileAttachment.INSP_Get_Worklist(dtFilter, ref Record_Count);
        }
        public DataSet GetAllWorklistType()
        {
            return objFileAttachment.GetAllWorklistType_DL();
        }
        public DataSet INSP_Get_WorklistReport(int InspectionID)
        {
            return objFileAttachment.INSP_Get_WorklistReport(InspectionID);
        }
        public int IsVessel(int Vessel_ID)
        {
            return objFileAttachment.IsVessel(Vessel_ID);
        }
        public int Insert_Worklist_Attachment(int Vessel_ID, int Worklist_ID, int WL_Office_ID, string Attach_Name, string Attach_Path, long Attach_Size, int Created_By)
        {
            return objFileAttachment.Insert_Worklist_Attachment_DL(Vessel_ID, Worklist_ID, WL_Office_ID, Attach_Name, Attach_Path, Attach_Size, Created_By);
        }
        public int Insert_ActivityObject(int Vessel_ID, int Worklist_ID, int WL_Office_ID, string Attach_Name, string Attach_Path, int Created_By)
        {
            return objFileAttachment.Insert_ActivityObject_DL(Vessel_ID, Worklist_ID, WL_Office_ID, Attach_Name, Attach_Path, Created_By);
        }
        public DataTable Insert_NewJob(int VESSEL_ID, string JOB_DESCRIPTION,
string DATE_RAISED, string DATE_ESTMTD_CMPLTN, string DATE_COMPLETED, int PRIORITY,
int ASSIGNOR, int NCR_YN, int DEPT_SHIP, int DEPT_OFFICE, string REQSN_MSG_REF, int DEFER_TO_DD,
int CATEGORY_NATURE, int CATEGORY_PRIMARY, int CATEGORY_SECONDARY, int CATEGORY_MINOR, int PIC, int CREATED_BY, int TOSYNC, string Causes, string CorrectiveAction, string PreventiveAction, int Inspector, string InspectionDate, string WL_TYPE, string Activity_ID, string status, int? LocationID, int? PSC_SIRE)
        {
            try
            {

                return objFileAttachment.Insert_NewJob_DL(VESSEL_ID, JOB_DESCRIPTION,
                    DATE_RAISED, DATE_ESTMTD_CMPLTN, DATE_COMPLETED, PRIORITY,
                    ASSIGNOR, NCR_YN, DEPT_SHIP, DEPT_OFFICE, REQSN_MSG_REF, DEFER_TO_DD,
                    CATEGORY_NATURE, CATEGORY_PRIMARY, CATEGORY_SECONDARY, CATEGORY_MINOR, PIC, CREATED_BY, TOSYNC, Causes, CorrectiveAction, PreventiveAction, Inspector, InspectionDate, WL_TYPE, Activity_ID, status, LocationID, PSC_SIRE);
            }
            catch
            {
                throw;
            }
            finally
            {
            }
        }
        public int Update_Job(int VESSEL_ID, int WorkList_ID, int OFFICE_ID, string DATE_ESTMTD_CMPLTN, string DATE_COMPLETED, int PRIORITY,
       int ASSIGNOR, int NCR_YN, int DEPT_SHIP, int DEPT_OFFICE, string REQSN_MSG_REF, int DEFER_TO_DD,
       int CATEGORY_NATURE, int CATEGORY_PRIMARY, int CATEGORY_SECONDARY, int CATEGORY_MINOR, string PIC, int CREATED_BY, int TOSYNC, int Inspector, string InspectionDate, string WL_TYPE, int? PSC_SIRE)
        {
            try
            {
                return objFileAttachment.Update_Job_DL(VESSEL_ID, WorkList_ID, OFFICE_ID, DATE_ESTMTD_CMPLTN, DATE_COMPLETED, PRIORITY,
                ASSIGNOR, NCR_YN, DEPT_SHIP, DEPT_OFFICE, REQSN_MSG_REF, DEFER_TO_DD,
                CATEGORY_NATURE, CATEGORY_PRIMARY, CATEGORY_SECONDARY, CATEGORY_MINOR, PIC, CREATED_BY, TOSYNC, Inspector, InspectionDate, WL_TYPE, PSC_SIRE);

            }
            catch
            {

                throw;
            }
            finally
            {
            }
        }
        public DataTable Get_WorkList_Index(DataTable dtFilter, ref int Record_Count)
        {
            return objFileAttachment.Get_WorkList_Index(dtFilter, ref Record_Count);
        }
        public DataTable INSP_GET_WORKLIST_JOB(DataTable dtFilter, ref int Record_Count)
        {
            return objFileAttachment.INSP_GET_WORKLIST_JOB(dtFilter, ref Record_Count);
        }
        public DataSet Get_JobDetails_ByID(int OFFICE_ID, int WORKLIST_ID, int VESSEL_ID)
        {
            return objFileAttachment.Get_JobDetails_ByID(OFFICE_ID, WORKLIST_ID, VESSEL_ID);
        }
        public int INSP_Delete_WorklistJob(int VESSEL_ID, int WorkList_ID, int OFFICE_ID)
        {
            try
            {
                return objFileAttachment.INSP_Delete_WorklistJob(VESSEL_ID, WorkList_ID, OFFICE_ID);
            }
            catch
            {
                throw;
            }
            finally
            {
            }

        }
        public int INSP_Delete_Activity(int VESSEL_ID, int WorkList_ID, int OFFICE_ID)
        {
            try
            {
                return objFileAttachment.INSP_Delete_Activity(VESSEL_ID, WorkList_ID, OFFICE_ID);
            }
            catch
            {
                throw;
            }
            finally
            {
            }

        }
        public int Upd_Worklist_Status(int Vessel_ID, int Worklist_ID, int WL_Office_ID, int User_ID, string Remark, string Action_Type)
        {
            return objFileAttachment.Upd_Worklist_Status(Vessel_ID, Worklist_ID, WL_Office_ID, User_ID, Remark, Action_Type);
        }
        public DataTable Get_Worklist_Attachments(int Vessel_ID, int Worklist_ID, int WL_Office_ID, int UserID)
        {
            return objFileAttachment.Get_Worklist_Attachments_DL(Vessel_ID, Worklist_ID, WL_Office_ID, UserID);
        }
        public int INSP_Delete_WorklistAttachment(int Vessel_ID, int Worklist_ID, int WL_Office_ID, int AttachmentID, int Deleted_By)
        {
            return objFileAttachment.INSP_Delete_WorklistAttachment(Vessel_ID, Worklist_ID, WL_Office_ID, AttachmentID, Deleted_By);
        }
        public int INSP_Delete_ActivityObject(int Vessel_ID, int Worklist_ID, int WL_Office_ID, int AttachmentID, int Deleted_By)
        {
            return objFileAttachment.INSP_Delete_ActivityObject(Vessel_ID, Worklist_ID, WL_Office_ID, AttachmentID, Deleted_By);
        }
        //public DataSet Get_JobDetails_ByID(int OFFICE_ID, int WORKLIST_ID, int VESSEL_ID)
        //{
        //    return objFileAttachment.Get_JobDetails_ByID(OFFICE_ID, WORKLIST_ID, VESSEL_ID);
        //}
        //public int Upd_Worklist_Status(int Vessel_ID, int Worklist_ID, int WL_Office_ID, int User_ID, string Remark, string Action_Type)
        //{
        //    return objFileAttachment.Upd_Worklist_Status(Vessel_ID, Worklist_ID, WL_Office_ID, User_ID, Remark, Action_Type);
        //}



        public int INSP_Get_RatingsFromChecklistRating(int InspectionID, string ChecklistIDs)
        {
            return objFileAttachment.INSP_Get_RatingsFromChecklistRating(InspectionID, ChecklistIDs);
        }
        public int CheckExists_Schedule(int INSPECTORID, string scheduledate,int durJobs, int UserID, int returnval)
        {
            return objFileAttachment.CheckExists_Schedule_DL( INSPECTORID,  scheduledate, durJobs, UserID, returnval);
        }
        public int Save_Schedule(DataTable dtSchedule, DataTable dtSettings, DataTable dtChecklist, int UserID, int? InspectionID, DataTable dtPortList)
        {
            return objFileAttachment.Save_Schedule_DL(dtSchedule, dtSettings, dtChecklist, UserID, InspectionID, dtPortList);
        }
        public DataSet INSP_Get_SuptInspReportIconConfig(int CompanyID)
        {
            return objFileAttachment.INSP_Get_SuptInspReportIconConfig(CompanyID);
        }
      
        public DataTable GetAttachedFileInfo(string Vessel_ID)
        {

            return objFileAttachment.GetAttachedFileInfo(Vessel_ID);

        }
        public DataTable Get_VesselAttendence(int InspectorID, int InspectionID)
        {

            return objFileAttachment.Get_VesselAttendence(InspectorID, InspectionID);

        }
        public DataSet INSP_Get_RatingLegends()
        {

            return objFileAttachment.INSP_Get_RatingLegends();

        }

        public DataTable GetAttachedFileInfo(int? fleetcode, int? ddlvessel, string search, string category, int? supplier
            , string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {

            return objFileAttachment.GetAttachedFileInfo(fleetcode, ddlvessel, search, category, supplier,
                sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);

        }


        public int SaveAttachedFileInfo(string Vessel_ID, string InspectionDetailId,   string FileName, string FilePath, string CreatedBy )
        {

            return objFileAttachment.SaveAttachedFileInfo(Vessel_ID, InspectionDetailId , FileName, FilePath ,CreatedBy);

        }

        public DataTable INSP_Get_Attachments_DL(string Reqsnno, int VesselID)
        {
            return objFileAttachment.INSP_Get_Attachments_DL(Reqsnno, VesselID);
        }

        public DataTable TEC_Get_SupritendentAttendanceWithPort(DateTime StartDateOfMonth, int CompanyID)
        {
            return objFileAttachment.TEC_Get_SupritendentAttendanceWithPort(StartDateOfMonth,CompanyID);
        }
        public DataTable TEC_Get_SupritendentAttendanceWithPort(DateTime StartDateOfMonth)
        {
            return objFileAttachment.TEC_Get_SupritendentAttendanceWithPort(StartDateOfMonth);
        }
        public DataTable TEC_Get_SupInspectionDetailsByDate(DateTime CurrentDate, int InspectorID, int CompanyID)
        {
            return objFileAttachment.TEC_Get_SupInspectionDetailsByDate(CurrentDate, InspectorID, CompanyID);
        }

        public int INSP_Update_InspectionForReOpen(int InspectionID, int UserID, string Reason)
        {
            return objFileAttachment.INSP_Update_InspectionForReOpen(InspectionID, UserID, Reason);
        }
        public DataTable TEC_Get_SupInspectionDetailsByDate(DateTime CurrentDate, int InspectorID)
        {
            return objFileAttachment.TEC_Get_SupInspectionDetailsByDate(CurrentDate, InspectorID);
        }
        // remove this method
        public int INSP_Delete_Attachments_DL(int ID, int Vessel_ID, int UserId)
        {
            return objFileAttachment.INSP_Delete_Attachments_DL(ID, Vessel_ID, UserId);
        }
        public DataSet Get_CaledndarData(DateTime? DateFrom, DateTime? DateTo, int? Company_ID)
        {
            return objFileAttachment.Get_CaledndarData(DateFrom, DateTo, Company_ID);
        }
        public DataSet Get_CaledndarDataBySupt(DateTime? DateFrom, DateTime? DateTo, int? Company_ID)
        {
            return objFileAttachment.Get_CaledndarDataBySupt(DateFrom, DateTo, Company_ID);
        }
        public DataTable Insert_NewJob(int VESSEL_ID, string JOB_DESCRIPTION,
string DATE_RAISED, string DATE_ESTMTD_CMPLTN, string DATE_COMPLETED, int PRIORITY,
int ASSIGNOR, int NCR_YN, int DEPT_SHIP, int DEPT_OFFICE, string REQSN_MSG_REF, int DEFER_TO_DD,
int CATEGORY_NATURE, int CATEGORY_PRIMARY, int CATEGORY_SECONDARY, int CATEGORY_MINOR, int PIC, int CREATED_BY, int TOSYNC, string Causes, string CorrectiveAction, string PreventiveAction, int Inspector, string InspectionDate, string WL_TYPE, string Activity_ID, string status, int? LocationID, int? PSC_SIRE, int IsConditionalReport)
        {
            try
            {

                return objFileAttachment.Insert_NewJob_DL(VESSEL_ID, JOB_DESCRIPTION,
                    DATE_RAISED, DATE_ESTMTD_CMPLTN, DATE_COMPLETED, PRIORITY,
                    ASSIGNOR, NCR_YN, DEPT_SHIP, DEPT_OFFICE, REQSN_MSG_REF, DEFER_TO_DD,
                    CATEGORY_NATURE, CATEGORY_PRIMARY, CATEGORY_SECONDARY, CATEGORY_MINOR, PIC, CREATED_BY, TOSYNC, Causes, CorrectiveAction, PreventiveAction, Inspector, InspectionDate, WL_TYPE, Activity_ID, status, LocationID, PSC_SIRE,IsConditionalReport);
            }
            catch
            {
                throw;
            }
            finally
            {
            }
        }



        #region Inspection New Transfer Function From Worklist 04-05-2015

        public int INSP_Get_RatingsByRating(string Rating)
        {
            try
            {

                return objFileAttachment.INSP_Get_RatingsByRating(Rating);
            }
            catch
            {
                throw;
            }
            finally
            {

            }
        }
        public int INSP_Update_ExecutiveSummary(string ReportNo, string CompanyName, int InspectionID, int CreatedBy)
        {
            try
            {

                return objFileAttachment.INSP_Update_ExecutiveSummary(ReportNo, CompanyName, InspectionID, CreatedBy);
            }
            catch
            {
                throw;
            }
            finally
            {

            }
        }
        public int InsertExecutiveSummary(int SummaryTopicKey, string SummaryTopicDetails, int InspectionID, int CreatedBy, DateTime DataOfCreation, int ActiveStatus)
        {
            try
            {

                return objFileAttachment.InsertExecutiveSummary(SummaryTopicKey, SummaryTopicDetails, InspectionID, CreatedBy, DataOfCreation, ActiveStatus);
            }
            catch
            {
                throw;
            }
            finally
            {

            }

        }
        public int InsertExecutiveSummaryBunkers(int InspectionID, string TotalFOLog, string TotalFOMeasured, string TotalMDOLog, string TotalMDOMeasured, string TotalMGOLog, string TotalMGOMeasured, int CreatedBy, DateTime DateOfCreation, int ActiveStatus)
        {
            try
            {

                return objFileAttachment.InsertExecutiveSummaryBunkers(InspectionID, TotalFOLog, TotalFOMeasured, TotalMDOLog, TotalMDOMeasured, TotalMGOLog, TotalMGOMeasured, CreatedBy, DateOfCreation, ActiveStatus);
            }
            catch
            {
                throw;
            }
            finally
            {

            }

        }
        public DataSet GetExecutiveSummary(int InspectionID)
        {
            try
            {

                return objFileAttachment.GetExecutiveSummary(InspectionID);
            }
            catch
            {
                throw;
            }
            finally
            {

            }

        }
        public DataSet INSP_Get_InspectionReportInfo(int InspectionID)
        {
            try
            {

                return objFileAttachment.INSP_Get_InspectionReportInfo(InspectionID);
            }
            catch
            {
                throw;
            }
            finally
            {

            }

        }

        public DataSet GetVesselInspectionDetails(int InspectionID)
        {
            try
            {

                return objFileAttachment.GetVesselInspectionDetails(InspectionID);
            }
            catch
            {
                throw;
            }
            finally
            {

            }

        }

        public DataSet GetRatings(string CheckListIDs)
        {
            try
            {

                return objFileAttachment.GetRatings(CheckListIDs);
            }
            catch
            {
                throw;
            }
            finally
            {

            }

        }
        public DataSet GetRatings()
        {
            try
            {

                return objFileAttachment.GetRatings();
            }
            catch
            {
                throw;
            }
            finally
            {

            }

        }

        public DataSet GetRatingsByID(int ID)
        {
            try
            {

                return objFileAttachment.GetRatingsByID(ID);
            }
            catch
            {
                throw;
            }
            finally
            {

            }

        }
        public DataSet GetRatingsByValue(string RatingValue)
        {
            try
            {

                return objFileAttachment.GetRatingsByValue(RatingValue);
            }
            catch
            {
                throw;
            }
            finally
            {

            }

        }
        public DataSet GetCategoryRating(string InspectionID, string CheckListIDs)
        {
            try
            {

                return objFileAttachment.GetCategoryRating(InspectionID, CheckListIDs);
            }
            catch
            {
                throw;
            }
            finally
            {

            }

        }
        public DataSet GetCategoryRating(string InspectionID)
        {
            try
            {

                return objFileAttachment.GetCategoryRating(InspectionID);
            }
            catch
            {
                throw;
            }
            finally
            {

            }

        }
        public DataSet GetSubCategoryRating(string ParentCode, string InspectionID)
        {
            try
            {

                return objFileAttachment.GetSubCategoryRating(ParentCode, InspectionID);
            }
            catch
            {
                throw;
            }
            finally
            {

            }

        }
        public int DeleteRestoreRating(int ID, bool ActiveStatus, string ModifiedBy, DateTime DateOfModification)
        {
            try
            {

                return objFileAttachment.DeleteRestoreRating(ID, ActiveStatus, ModifiedBy, DateOfModification);
            }
            catch
            {
                throw;
            }
            finally
            {

            }

        }
        public int UpdateRating(int ID, string rating, int RatingValue, string RatingColor, string ModifiedBy, DateTime DateOfModification, int ActiveStatus)
        {
            try
            {

                return objFileAttachment.UpdateRating(ID, rating, RatingValue, RatingColor, ModifiedBy, DateOfModification, ActiveStatus);
            }
            catch
            {
                throw;
            }
            finally
            {

            }

        }

        public int InsertRating(string rating, string RatingValue, string RatingColor, string CreatedBy, DateTime DateOfCreation, int ActiveStatus)
        {
            try
            {

                return objFileAttachment.InsertRating(rating, RatingValue, RatingColor, CreatedBy, DateOfCreation, ActiveStatus);
            }
            catch
            {
                throw;
            }
            finally
            {

            }

        }
        public int InsertCategoryRating(string SystemCode, string SystemLastReport, string SystemCurrentReport, string SystemRating, string ScheduleID, string InspectionID, string CreatedBy, DateTime DateOfCreation, string ActiveStatus)
        {
            try
            {

                return objFileAttachment.InsertCategoryRating(SystemCode, SystemLastReport, SystemCurrentReport, SystemRating, ScheduleID, InspectionID, CreatedBy, DateOfCreation, ActiveStatus);
            }
            catch
            {
                throw;
            }
            finally
            {

            }

        }
        public int InsertSubCategoryRating(string SystemCode, string SubSystemCode, string SubSystemSecLastReport, string SubSystemLastReport, string SubSystemCurrentReport, string SubSystemRating, string Remarks, string AdditionalRemark, string ScheduleID, string InspectionID, string CreatedBy, DateTime DateOfCreation, string ActiveStatus)
        {
            try
            {

                return objFileAttachment.InsertSubCategoryRating(SystemCode, SubSystemCode, SubSystemSecLastReport, SubSystemLastReport, SubSystemCurrentReport, SubSystemRating, Remarks, AdditionalRemark, ScheduleID, InspectionID, CreatedBy, DateOfCreation, ActiveStatus);
            }
            catch
            {
                throw;
            }
            finally
            {

            }

        }
        public int INSP_Update_SubSystemRating(int InspectionID, int SubSystemID, string RatingValue, string Rating, int ModifiedBy, DateTime DateOfModification)
        {
            try
            {

                return objFileAttachment.INSP_Update_SubSystemRating(InspectionID, SubSystemID, RatingValue, Rating, ModifiedBy, DateOfModification);
            }
            catch
            {
                throw;
            }
            finally
            {

            }

        }
        public int INSP_Update_SubSystemRating(int InspectionID, int? SubSystemID, string RatingValue, string Rating, int ModifiedBy, DateTime DateOfModification)
        {
            try
            {

                return objFileAttachment.INSP_Update_SubSystemRating(InspectionID, SubSystemID, RatingValue, Rating, ModifiedBy, DateOfModification);
            }
            catch
            {
                throw;
            }
            finally
            {

            }

        }
        public DataSet GetYearlyInspectionSummary(string EndYear)
        {
            try
            {

                return objFileAttachment.GetYearlyInspectionSummary(EndYear);
            }
            catch
            {
                throw;
            }
            finally
            {

            }

        }

        public DataSet Get_Schedule_Details(int ScheduleID, int UserID, int SchDetailId, int SubSysID)
        {
            return objFileAttachment.Get_Schedule_Details_DL(ScheduleID, UserID, SchDetailId, SubSysID);
        }

        public DataSet Get_Schedule_Details(int ScheduleID, int UserID, int SchDetailId, int? LocationID, int LocationNodeID)
        {
            return objFileAttachment.Get_Schedule_Details_DL(ScheduleID, UserID, SchDetailId, LocationID, LocationNodeID);
        }

        public void Update_Inspection(int InspectionDetailID, int InspectorID, DateTime? InspectionDate, string Status, int User_Id, int OnBoard, int OnShore, DateTime? CompletionDate, int? DurJobs)
        {
            objFileAttachment.Update_Inspection(InspectionDetailID, InspectorID, InspectionDate, Status, User_Id, OnBoard, OnShore, CompletionDate, DurJobs);
        }
        public DataTable INSP_Get_WorklistReportWithImages(int InspectionDetailId, int ShowImages, string ReportType)
        {
            return objFileAttachment.INSP_Get_WorklistReportWithImages(InspectionDetailId, ShowImages, ReportType);
        }
        public DataTable INSP_Get_WorklistReportWithCategoryGrouping(int InspectionDetailId, int ShowImages, string ReportType, string ChecklistIDs)
        {
            return objFileAttachment.INSP_Get_WorklistReportWithCategoryGrouping(InspectionDetailId, ShowImages, ReportType, ChecklistIDs);
        }
        public DataTable INSP_Get_WorklistReportWithCategoryGrouping(int InspectionDetailId, int ShowImages, string ReportType)
        {
            return objFileAttachment.INSP_Get_WorklistReportWithCategoryGrouping(InspectionDetailId, ShowImages, ReportType);
        }
      

        public DataTable Get_InspectionScheduleByVessel(int Vessel_ID, int StartMonth, int Eva_Type, string SearchText)
        {
            try
            {
                return objFileAttachment.Get_InspectionScheduleByVessel_DL(Vessel_ID, StartMonth, Eva_Type, SearchText);
            }
            catch
            {
                throw;
            }

        }

        public DataSet INSP_Get_InspectionPort(int InspectionID)
        {
            try
            {

                return objFileAttachment.INSP_Get_InspectionPort(InspectionID);
            }
            catch
            {
                throw;
            }
            finally
            {

            }
        }
        public int INSP_InsertUpdate_InspectionPort(DataTable dtPorts, int InspectionID, int CreatedBy)
        {
            try
            {

                return objFileAttachment.INSP_InsertUpdate_InspectionPort(dtPorts, InspectionID, CreatedBy);
            }
            catch
            {
                throw;
            }
            finally
            {

            }
        }

        public int INSP_Get_WorklistReportByLocationID(int InspectionID, int SubSystemID)
        {
            try
            {

                return objFileAttachment.INSP_Get_WorklistReportByLocationID(InspectionID, SubSystemID);
            }
            catch
            {
                throw;
            }
            finally
            {

            }
        }
        public int INSP_Get_WorklistJobsCountByLocationID(int InspectionID, int SubSystemID)
        {
            try
            {

                return objFileAttachment.INSP_Get_WorklistJobsCountByLocationID(InspectionID, SubSystemID);
            }
            catch
            {
                throw;
            }
            finally
            {

            }
        }
        public int INSP_Get_WorklistJobsCountByLocationID(int InspectionID, int? SubSystemID)
        {
            try
            {
                return objFileAttachment.INSP_Get_WorklistJobsCountByLocationID(InspectionID, SubSystemID);
            }
            catch
            {
                throw;
            }
            finally
            {

            }
        }

        public int INSP_Get_WorklistJobsCountByLocationNodeID(int InspectionID, int LocationNodeID)
        {
            try
            {
                return objFileAttachment.INSP_Get_WorklistJobsCountByLocationNodeID(InspectionID, LocationNodeID);
            }
            catch
            {
                throw;
            }
            finally
            {

            }
        }
        public DataSet Get_Current_Schedules(string Status, int Vessel_ID, DateTime? InspectionFromDate, DateTime? InspectionToDate, int PortId)
        {
            return objFileAttachment.Get_Current_Schedules_DL(Status, Vessel_ID, InspectionFromDate,InspectionToDate, PortId);
        }
        public DataSet Get_Current_Schedules(string Frequency_Type, int Last_Run_Success, string Status, string SearchText, int UserID, int? Page_Index, int? Page_Size, ref int is_Fetch_Count, int? Vessel_ID, int? InspectorID, DateTime? DateFrom, DateTime? DateTo, int? InspectionTypeId, int? OverDue, string sortexp, int? Fleet_Id)
        {
            return objFileAttachment.Get_Current_Schedules_DL(Frequency_Type, Last_Run_Success, Status, SearchText, UserID, Page_Index, Page_Size, ref is_Fetch_Count, Vessel_ID, InspectorID, DateFrom, DateTo, InspectionTypeId, OverDue, sortexp, Fleet_Id, null);
        }

        public DataSet Get_Current_Schedules(string Frequency_Type, int Last_Run_Success, string Status, string SearchText, int UserID, int? Page_Index, int? Page_Size, ref int is_Fetch_Count, int? Vessel_ID, int? InspectorID, DateTime? DateFrom, DateTime? DateTo, int? InspectionTypeId, int? OverDue, string sortexp, int? Fleet_Id, int? Company_ID = null)
        {
            return objFileAttachment.Get_Current_Schedules_DL(Frequency_Type, Last_Run_Success, Status, SearchText, UserID, Page_Index, Page_Size, ref is_Fetch_Count, Vessel_ID, InspectorID, DateFrom, DateTo, InspectionTypeId, OverDue, sortexp, Fleet_Id, Company_ID);
        }

        public DataSet Get_Current_Schedules(int InspectionId,string Frequency_Type, int Last_Run_Success, string Status, string SearchText, int UserID,  int? Page_Index, int? Page_Size, ref int is_Fetch_Count, int? Vessel_ID, int? InspectorID, DateTime? DateFrom, DateTime? DateTo, int? InspectionTypeId, int? OverDue, string sortexp, int? Fleet_Id, int? Company_ID = null)
        {
            return objFileAttachment.Get_Current_Schedules_DL(InspectionId,Frequency_Type, Last_Run_Success, Status, SearchText, UserID, Page_Index, Page_Size, ref is_Fetch_Count, Vessel_ID, InspectorID, DateFrom, DateTo, InspectionTypeId, OverDue, sortexp, Fleet_Id, Company_ID);
        }

        public DataTable Get_Supritendent_Users(int? Fleet_ID, int? Vessel_ID)
        {
            return objFileAttachment.Get_Supritendent_Users(Fleet_ID, Vessel_ID);
        }
        public DataTable TEC_WL_Get_Supritendent_UsersByCompanyID(int? CompanyID, int? Vessel_ID)
        {
            return objFileAttachment.TEC_WL_Get_Supritendent_UsersByCompanyID(CompanyID, Vessel_ID);
        }
        public DataTable Get_Checklists(int? Vessel_ID)
        {
            return objFileAttachment.Get_ChecklistOnVesselType_DL(Vessel_ID);
        }


        public int Save_InspectionWorklist(DataTable dtInspectionWorklist, int UserID)
        {
            return objFileAttachment.Save_InspectionWorklist(dtInspectionWorklist, UserID);
        }

        public DataSet Get_CheckList_Worklist_DL(int Vesel_ID, int NodeID)
        {
            return objFileAttachment.Get_CheckList_Worklist_DL(Vesel_ID, NodeID);
        }

        public DataTable Get_ImagePath(DataTable dtInspectionWorklist)
        {
            return objFileAttachment.Get_ImagePath_DL(dtInspectionWorklist);
        }


        public int Save_InspectionWorklistWithNodeVal(DataTable dtInspectionWorklist, int? InspectionId, int? LocationID, int? LocationNodeID, int UserID)
        {
            return objFileAttachment.Save_InspectionWorklistWithNodeVal_DL(dtInspectionWorklist, InspectionId, LocationID, LocationNodeID, UserID);
        }

        public int Save_InspectionWorklistWithNodeVal(DataTable dtInspectionWorklist, int UserID)
        {
            return objFileAttachment.Save_InspectionWorklistWithNodeVal_DL(dtInspectionWorklist, UserID);
        }

        public int TEC_WL_INS_InspectionWorklist_Location(int InspectionId, int WorkiListID, int VesselID, int OfficeID, int LocationID, int CreatedBy)
        {
            return objFileAttachment.TEC_WL_INS_InspectionWorklist_Location(InspectionId, WorkiListID, VesselID, OfficeID, LocationID, CreatedBy);
        }
        public int TEC_WL_INS_InspectionWorklist_Location(int InspectionId, int WorkiListID, int VesselID, int OfficeID, int? LocationID, int LocationNodeID, int CreatedBy)
        {
            return objFileAttachment.TEC_WL_INS_InspectionWorklist_Location(InspectionId, WorkiListID, VesselID, OfficeID, LocationID, LocationNodeID, CreatedBy);
        }
        public void Del_ScheduledInspection(int? ID, int? DELETED_BY)
        {
            objFileAttachment.Del_ScheduledInspection(ID, DELETED_BY);
        }
        public DataSet Get_InspInfo(int? UserId)
        {
            return objFileAttachment.Get_InspInfo(UserId);

        }
        public DataSet Get_CheckList_Worklist_DL_Direct(int Vesel_ID,int InsDetailID)
        {
            return objFileAttachment.Get_CheckList_Worklist_DL_Direct(Vesel_ID,  InsDetailID);
        }

        public DataSet Get_SurveyCertificateToolTip(int Surv_Details_ID, int Surv_Vessel_ID, int Vessel_ID, int OfficeID, char Type)
        {
            return objFileAttachment.Get_SurveyCertificateToolTip(Surv_Details_ID,Surv_Vessel_ID,Vessel_ID, OfficeID,Type);
        }

        public int SurveyRenewalInspection(int Surv_Details_ID, int Surv_Vessel_ID, int Vessel_ID, int OfficeID, int ScheduleID,ref int ReturnInspectionID)
        {
            return objFileAttachment.SurveyRenewalInspection(Surv_Details_ID, Surv_Vessel_ID, Vessel_ID, OfficeID, ScheduleID,ref ReturnInspectionID);
        }
        #endregion

    }
}
