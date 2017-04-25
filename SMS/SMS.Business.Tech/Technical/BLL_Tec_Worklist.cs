using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

using SMS.Data.Technical;
using SMS.Data.Infrastructure;


/// <summary>
/// Summary description for BL_Tec_WL_Worklist
/// </summary>
namespace SMS.Business.Technical
{
    public class BLL_Tec_Worklist
    {
        public BLL_Tec_Worklist()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        DAL_Tec_Worklist objDLL = new DAL_Tec_Worklist();
        DAL_Infra_UserCredentials objDLLUser = new DAL_Infra_UserCredentials();
        public DataSet TEC_WL_Get_PSCSIRE()
        {
            return objDLL.TEC_WL_Get_PSCSIRE();
        }
        public DataSet TEC_WL_Get_ActivePSCSIRE()
        {
            return objDLL.TEC_WL_Get_ActivePSCSIRE();
        }
        public DataSet TEC_WL_Get_PSCSIREByID(int ID)
        {
            return objDLL.TEC_WL_Get_PSCSIREByID(ID);
        }
        public int TEC_WL_Insert_PSCSIRE(string PSCSIRE, int CreatedBy)
        {
            return objDLL.TEC_WL_Insert_PSCSIRE(PSCSIRE, CreatedBy);
        }

        public int TEC_WL_Update_PSCSIRE(int ID, string PSC_SIRE, int ModifiedBy)
        {
            return objDLL.TEC_WL_Update_PSCSIRE(ID, PSC_SIRE, ModifiedBy);
        }

        public int TEC_WL_Delete_PSCSIRE(int ID, int DeletedBy)
        {
            return objDLL.TEC_WL_Delete_PSCSIRE(ID, DeletedBy);
        }
        public int TEC_WL_Restore_PSCSIRE(int ID, int ModifiedBy)
        {
            return objDLL.TEC_WL_Restore_PSCSIRE(ID, ModifiedBy);
        }

        public DataSet GetAllNature()
        {
            return objDLL.GetAllNature_DL();
        }

        public DataSet TEC_Get_Category()
        {
            return objDLL.TEC_Get_Category();
        }
        public DataSet TEC_Get_CategoryByID(int CategoryID)
        {
            return objDLL.TEC_Get_CategoryByID(CategoryID);
        }
        public int TEC_Insert_Category(string CategoryName, string CategoryType, int CreatedBy)
        {
            return objDLL.TEC_Insert_Category(CategoryName, CategoryType, CreatedBy);
        }
        public DataSet TEC_Get_ActiveCategory()
        {
            return objDLL.TEC_Get_ActiveCategory();
        }
        public int TEC_Update_Category(int CategoryID, string CategoryName, string CategoryType, int ModifiedBy)
        {
            return objDLL.TEC_Update_Category(CategoryID, CategoryName, CategoryType, ModifiedBy);
        }

        public int TEC_Delete_Category(int CategoryID, int DeletedBy)
        {
            return objDLL.TEC_Delete_Category(CategoryID, DeletedBy);
        }
        public int TEC_Restore_Category(int CategoryID, int ModifiedBy)
        {
            return objDLL.TEC_Restore_Category(CategoryID, ModifiedBy);
        }


        public DataSet GetPrimaryByNatureID(Int32 i32NatureID)
        {
            try
            {

                return objDLL.GetPrimaryByNatureID_DL(i32NatureID);
            }
            catch
            {
                throw;
            }
            finally
            {

            }

        }
        public DataSet GetSecondaryByPrimaryID(int intNature)
        {
            try
            {

                return objDLL.GetSecondaryByPrimaryID_DL(intNature);
            }
            catch
            {
                throw;
            }
            finally
            {

            }

        }
        public DataSet GetMinorBySecondaryID(int SecondaryID)
        {
            try
            {

                return objDLL.GetMinorBySecondaryID_DL(SecondaryID);
            }
            catch
            {
                throw;
            }
            finally
            {

            }

        }

        public DataTable Get_Dept_OnShip()
        {
            return objDLL.Get_Dept_OnShip();
        }
        public DataTable Get_Dept_InOffice()
        {
            return objDLL.Get_Dept_InOffice();
        }
        public DataTable Get_JobPriority()
        {
            return objDLL.Get_JobPriority();
        }
        public DataTable Get_Assigner()
        {
            return objDLL.Get_Assigner();
        }

        public DataSet Get_FilterWorklist(string SQL)
        {
            return objDLL.Get_FilterWorklist(SQL);
        }

        public DataTable Insert_NewJob(int VESSEL_ID, string JOB_DESCRIPTION,
   string DATE_RAISED, string DATE_ESTMTD_CMPLTN, string DATE_COMPLETED, int PRIORITY,
   int ASSIGNOR, int NCR_YN, int DEPT_SHIP, int DEPT_OFFICE, string REQSN_MSG_REF, int DEFER_TO_DD,
   int CATEGORY_NATURE, int CATEGORY_PRIMARY, int CATEGORY_SECONDARY, int CATEGORY_MINOR, int PIC, int CREATED_BY, int TOSYNC, string Causes, string CorrectiveAction, string PreventiveAction, int Inspector, string InspectionDate, string WL_TYPE, string Activity_ID, string status, int? LocationID, int? PSC_SIRE)
        {
            try
            {

                return objDLL.Insert_NewJob_DL(VESSEL_ID, JOB_DESCRIPTION,
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

        public DataTable Get_WorkList_Index(DataTable dtFilter, ref int Record_Count)
        {
            return objDLL.Get_WorkList_Index(dtFilter, ref Record_Count);
        }
        public DataTable Get_InspectorName_ByInspectionID(int InspectionID)
        {
            return objDLL.Get_InspectorName_ByInspectionID_DL(InspectionID).Tables[0];
        }
        public DataSet INSP_Get_Worklist(DataTable dtFilter, ref int Record_Count)
        {
            return objDLL.INSP_Get_Worklist(dtFilter, ref Record_Count);
        }
        public DataSet Get_JobDetails_ByID(int OFFICE_ID, int WORKLIST_ID, int VESSEL_ID)
        {
            return objDLL.Get_JobDetails_ByID(OFFICE_ID, WORKLIST_ID, VESSEL_ID);
        }

        public int Create_Mail_Job_Details(int OFFICE_ID, int WORKLIST_ID, int VESSEL_ID)
        {
            return objDLL.Create_Mail_Job_Details(OFFICE_ID, WORKLIST_ID, VESSEL_ID);
        }

        public DataTable Get_WorkList_Calendar(string JobType, DateTime? StartDate, DateTime? EndDate, int UserID)
        {
            return objDLL.Get_WorkList_Calendar(JobType, StartDate, EndDate, UserID);
        }

        public int IsVessel(int Vessel_ID)
        {
            return objDLL.IsVessel(Vessel_ID);
        }


        public DataSet GetAllWorklistType()
        {
            return objDLL.GetAllWorklistType_DL();
        }


        public int INSP_Delete_WorklistJob(int VESSEL_ID, int WorkList_ID, int OFFICE_ID)
        {
            try
            {
                return objDLL.INSP_Delete_WorklistJob(VESSEL_ID, WorkList_ID, OFFICE_ID);
            }
            catch
            {
                throw;
            }
            finally
            {
            }

        }
        public DataTable Insert_NewJob(int VESSEL_ID, string JOB_DESCRIPTION,
           string DATE_RAISED, string DATE_ESTMTD_CMPLTN, string DATE_COMPLETED, int PRIORITY,
           int ASSIGNOR, int NCR_YN, int DEPT_SHIP, int DEPT_OFFICE, string REQSN_MSG_REF, int DEFER_TO_DD,
           int CATEGORY_NATURE, int CATEGORY_PRIMARY, int CATEGORY_SECONDARY, int CATEGORY_MINOR, int PIC, int CREATED_BY, int TOSYNC, string Causes, string CorrectiveAction, string PreventiveAction, int Inspector, string InspectionDate, string WL_TYPE, int? PSC_SIRE)
        {
            try
            {

                return objDLL.Insert_NewJob_DL(VESSEL_ID, JOB_DESCRIPTION,
                    DATE_RAISED, DATE_ESTMTD_CMPLTN, DATE_COMPLETED, PRIORITY,
                    ASSIGNOR, NCR_YN, DEPT_SHIP, DEPT_OFFICE, REQSN_MSG_REF, DEFER_TO_DD,
                    CATEGORY_NATURE, CATEGORY_PRIMARY, CATEGORY_SECONDARY, CATEGORY_MINOR, PIC, CREATED_BY, TOSYNC, Causes, CorrectiveAction, PreventiveAction, Inspector, InspectionDate, WL_TYPE, PSC_SIRE);
            }
            catch
            {
                throw;
            }
            finally
            {
            }
        }
        // //Added by anjali Dt:18-02-2016

        public DataTable Insert_NewJob(int VESSEL_ID, string JOB_DESCRIPTION, string DATE_RAISED, string DATE_ESTMTD_CMPLTN, string DATE_COMPLETED, int PRIORITY,
                                         int ASSIGNOR, int NCR_YN, int DEPT_SHIP, int DEPT_OFFICE, string REQSN_MSG_REF, int DEFER_TO_DD, int CATEGORY_NATURE, int CATEGORY_PRIMARY,
                                         int CATEGORY_SECONDARY, int CATEGORY_MINOR, int PIC, int CREATED_BY, int TOSYNC, string Causes, string CorrectiveAction, string PreventiveAction,
                                         int Inspector, string InspectionDate, string WL_TYPE, int? PSC_SIRE, int _functionID, string _locationID, string _subLocation)
        {
            try
            {

                return objDLL.Insert_NewJob_DL(VESSEL_ID, JOB_DESCRIPTION, DATE_RAISED, DATE_ESTMTD_CMPLTN, DATE_COMPLETED, PRIORITY, ASSIGNOR, NCR_YN, DEPT_SHIP, DEPT_OFFICE, REQSN_MSG_REF,
                                               DEFER_TO_DD, CATEGORY_NATURE, CATEGORY_PRIMARY, CATEGORY_SECONDARY, CATEGORY_MINOR, PIC, CREATED_BY, TOSYNC, Causes, CorrectiveAction, PreventiveAction,
                                               Inspector, InspectionDate, WL_TYPE, PSC_SIRE, _functionID, _locationID, _subLocation);
            }
            catch
            {
                throw;
            }
            finally
            {
            }
        }


        public int Update_Job(int VESSEL_ID, int WorkList_ID, int OFFICE_ID, string DATE_ESTMTD_CMPLTN, string DATE_COMPLETED, int PRIORITY, int ASSIGNOR, int NCR_YN, int DEPT_SHIP, int DEPT_OFFICE,
                              string REQSN_MSG_REF, int DEFER_TO_DD, int CATEGORY_NATURE, int CATEGORY_PRIMARY, int CATEGORY_SECONDARY, int CATEGORY_MINOR, string PIC, int CREATED_BY, int TOSYNC,
                              int Inspector, string InspectionDate, string WL_TYPE, int? PSC_SIRE, int _functionID, string _locationID, string _subLocation)
        {
            try
            {
                return objDLL.Update_Job_DL(VESSEL_ID, WorkList_ID, OFFICE_ID, DATE_ESTMTD_CMPLTN, DATE_COMPLETED, PRIORITY, ASSIGNOR, NCR_YN, DEPT_SHIP, DEPT_OFFICE,
                                            REQSN_MSG_REF, DEFER_TO_DD, CATEGORY_NATURE, CATEGORY_PRIMARY, CATEGORY_SECONDARY, CATEGORY_MINOR, PIC, CREATED_BY, TOSYNC,
                                            Inspector, InspectionDate, WL_TYPE, PSC_SIRE, _functionID, _locationID, _subLocation);

            }
            catch
            {

                throw;
            }
            finally
            {
            }
        }

        // //Added by anjali Dt:18-02-2016

        public int Update_Job(int VESSEL_ID, int WorkList_ID, int OFFICE_ID, string DATE_ESTMTD_CMPLTN, string DATE_COMPLETED, int PRIORITY,
          int ASSIGNOR, int NCR_YN, int DEPT_SHIP, int DEPT_OFFICE, string REQSN_MSG_REF, int DEFER_TO_DD,
          int CATEGORY_NATURE, int CATEGORY_PRIMARY, int CATEGORY_SECONDARY, int CATEGORY_MINOR, string PIC, int CREATED_BY, int TOSYNC, int Inspector, string InspectionDate, string WL_TYPE, int? PSC_SIRE)
        {
            try
            {
                return objDLL.Update_Job_DL(VESSEL_ID, WorkList_ID, OFFICE_ID, DATE_ESTMTD_CMPLTN, DATE_COMPLETED, PRIORITY,
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

        public int Update_Job_Causes(string Causes, int VESSEL_ID, int WorkList_ID, int OFFICE_ID, int CREATED_BY, int TOSYNC)
        {
            try
            {
                return objDLL.Update_Job_Causes_DL(Causes, VESSEL_ID, WorkList_ID, OFFICE_ID, CREATED_BY, TOSYNC);
            }
            catch
            {

                throw;
            }
            finally
            {
            }
        }

        public int Update_Job_CorrectiveAction(string CorrectiveAction, int VESSEL_ID, int WorkList_ID, int OFFICE_ID, int CREATED_BY, int TOSYNC)
        {
            try
            {
                return objDLL.Update_Job_CorrectiveAction_DL(CorrectiveAction, VESSEL_ID, WorkList_ID, OFFICE_ID, CREATED_BY, TOSYNC);
            }
            catch
            {

                throw;
            }
            finally
            {
            }
        }

        public int Update_Job_PreventiveAction(string PreventiveAction, int VESSEL_ID, int WorkList_ID, int OFFICE_ID, int CREATED_BY, int TOSYNC)
        {
            try
            {
                return objDLL.Update_Job_PreventiveAction_DL(PreventiveAction, VESSEL_ID, WorkList_ID, OFFICE_ID, CREATED_BY, TOSYNC);
            }
            catch
            {

                throw;
            }
            finally
            {
            }
        }

        public int Insert_Followup(int OFFICE_ID, int Worklist_ID, int VESSEL_ID, string FOLLOWUP, int CREATED_BY, int TOSYNC)
        {
            try
            {
                return objDLL.Insert_Followup(OFFICE_ID, Worklist_ID, VESSEL_ID, FOLLOWUP, CREATED_BY, TOSYNC);
            }
            catch
            {
                throw;
            }
            finally
            {
            }
        }

        //public DataTable Get_FollowupList_ByJobID(int OFFICE_ID)
        //{
        //    try
        //    {
        //        return objDLL.Get_FollowupList_ByJobID(OFFICE_ID);
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //    finally
        //    {
        //    }
        //}

        public DataTable Get_FollowupList(int OFFICE_ID, int VESSEL_ID, int WORKLIST_ID)
        {
            try
            {
                return objDLL.Get_FollowupList_DL(OFFICE_ID, VESSEL_ID, WORKLIST_ID);
            }
            catch
            {
                throw;
            }
            finally
            {
            }
        }


        public DataTable Get_WorkList_Crew_Involved(int OFFICE_ID, int VESSEL_ID, int WORKLIST_ID)
        {
            try
            {
                return objDLL.Get_WorkList_Crew_Involved_DL(OFFICE_ID, VESSEL_ID, WORKLIST_ID);
            }
            catch
            {
                throw;
            }
            finally
            {
            }
        }

        public DataTable Get_Crew_Involved_To_Add_List(string SearchText, int? VESSEL_ID, int? Rank_ID)
        {
            try
            {
                return objDLL.Get_Crew_Involved_To_Add_List_DL(SearchText, VESSEL_ID, Rank_ID);
            }
            catch
            {
                throw;
            }
            finally
            {
            }

        }

        public int WorkList_Crew_Involved_Insert(int? OFFICE_ID, int? VESSEL_ID, int? WORKLIST_ID, int? VOYAGE_ID, int? CREATED_BY)
        {

            return objDLL.WorkList_Crew_Involved_Insert_DL(OFFICE_ID, VESSEL_ID, WORKLIST_ID, VOYAGE_ID, CREATED_BY);

        }


        public int WorkList_Crew_Involved_Delete(int? ID, int? VESSEL_ID, int? OFFICE_ID, int? CREATED_BY)
        {
            return objDLL.WorkList_Crew_Involved_Delete_DL(ID, VESSEL_ID, OFFICE_ID, CREATED_BY);
        }

        public DataTable Get_IncompleteJobs()
        {
            try
            {
                DataTable dt = objDLL.Get_IncompleteJobs_DL();
                return AddSummaryRow(dt);
            }
            catch
            {
                throw;
            }
            finally
            {
            }
        }

        public DataTable Get_CompletedJobs(string StartDate, string EndDate)
        {
            try
            {
                DateTime Dt_StartDate = DateTime.Parse(StartDate);
                DateTime Dt_EndDate = DateTime.Parse(EndDate);
                DataTable dt = objDLL.Get_CompletedJobs_DL(Dt_StartDate, Dt_EndDate);

                return AddSummaryRow(dt);

            }
            catch
            {
                throw;
            }
            finally
            {
            }
        }

        public DataSet Get_CompletedJobs_Prev2Wks(string StartDate, string EndDate)
        {
            try
            {
                DateTime Dt_StartDate = DateTime.Parse(StartDate);
                DateTime Dt_EndDate = DateTime.Parse(EndDate);

                return objDLL.Get_CompletedJobs_Prev2Wks_DL(Dt_StartDate, Dt_EndDate);
            }
            catch
            {
                throw;
            }
            finally
            {
            }
        }

        public int Get_CompletedJobs_Count(string StartDate, string EndDate)
        {
            try
            {
                DateTime Dt_StartDate = DateTime.Parse(StartDate);
                DateTime Dt_EndDate = DateTime.Parse(EndDate);

                return objDLL.Get_CompletedJobsCount_DL(Dt_StartDate, Dt_EndDate);
            }
            catch
            {
                throw;
            }
            finally
            {
            }
        }

        public DataTable Get_IncompleteJobs_NoFollowUp()
        {
            try
            {
                DataTable dt = objDLL.Get_IncompleteJobs_NoFollowUp_DL();
                return AddSummaryRow(dt);
            }
            catch
            {
                throw;
            }
            finally
            {
            }
        }

        public DataTable Get_IncompleteJobs_WithFollowUp()
        {
            try
            {
                DataTable dt = objDLL.Get_IncompleteJobs_WithFollowUp_DL();
                return AddSummaryRow(dt);

            }
            catch
            {
                throw;
            }
            finally
            {
            }
        }

        private DataTable AddSummaryRow(DataTable dt)
        {
            dt.Columns.Add(new DataColumn("Total", typeof(int)));
            DataRow drTot = dt.NewRow();
            int rowTotal = 0;


            foreach (DataRow dr in dt.Rows)
            {
                rowTotal = 0;

                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (i == 0)
                    {
                        drTot[i] = "Total";

                    }
                    else if (i == dt.Columns.Count - 1)
                    {
                        dr[i] = rowTotal.ToString();
                    }
                    else
                    {
                        if (dr[i].ToString() != "")
                        {
                            int val = 0;
                            int Tot = 0;

                            val = int.Parse(dr[i].ToString());

                            if (drTot[i].ToString() != "")
                                Tot = int.Parse(drTot[i].ToString());

                            Tot += val;
                            rowTotal += val;

                            drTot[i] = Tot.ToString();
                        }
                    }


                }
            }

            dt.Rows.Add(drTot);
            return dt;
        }

        public int Sync_Job_AfterUpdate(int Vessel_ID, int Worklist_ID, int OFFICE_ID)
        {
            try
            {
                return objDLL.Sync_Job_AfterUpdate_DL(Vessel_ID, Worklist_ID, OFFICE_ID);
            }
            catch
            {

                throw;
            }
            finally
            {
            }
        }

        public int UPDATE_Tech_Meeting_Flag(int Vessel_ID, int Worklist_ID, int OFFICE_ID, int FLAG_Tech_Meeting, int Flagged_By)
        {
            try
            {
                return objDLL.UPDATE_Tech_Meeting_Flag_DL(Vessel_ID, Worklist_ID, OFFICE_ID, FLAG_Tech_Meeting, Flagged_By);
            }
            catch
            {

                throw;
            }
            finally
            {
            }
        }

        public int ReleaseComplaint_ToFlag(int Vessel_ID, int Worklist_ID, int OFFICE_ID, int Released_By, string DPARemarks)
        {
            try
            {
                return objDLL.ReleaseComplaint_ToFlag_DL(Vessel_ID, Worklist_ID, OFFICE_ID, Released_By, DPARemarks);
            }
            catch
            {

                throw;
            }
            finally
            {
            }
        }

        public DataTable Get_Worklist_Attachments(int Vessel_ID, int Worklist_ID, int WL_Office_ID, int UserID)
        {
            return objDLL.Get_Worklist_Attachments_DL(Vessel_ID, Worklist_ID, WL_Office_ID, UserID);
        }

        public int Insert_Worklist_Attachment(int Vessel_ID, int Worklist_ID, int WL_Office_ID, string Attach_Name, string Attach_Path, long Attach_Size, int Created_By)
        {
            return objDLL.Insert_Worklist_Attachment_DL(Vessel_ID, Worklist_ID, WL_Office_ID, Attach_Name, Attach_Path, Attach_Size, Created_By);
        }

        public int Insert_ActivityObject(int Vessel_ID, int Worklist_ID, int WL_Office_ID, string Attach_Name, string Attach_Path, int Created_By)
        {
            return objDLL.Insert_ActivityObject_DL(Vessel_ID, Worklist_ID, WL_Office_ID, Attach_Name, Attach_Path, Created_By);
        }


        public int Delete_Worklist_Attachment(int Vessel_ID, int Worklist_ID, int WL_Office_ID, int AttachmentID, int Deleted_By)
        {
            return objDLL.Delete_Worklist_Attachment_DL(Vessel_ID, Worklist_ID, WL_Office_ID, AttachmentID, Deleted_By);
        }

        public int INSP_Delete_WorklistAttachment(int Vessel_ID, int Worklist_ID, int WL_Office_ID, int AttachmentID, int Deleted_By)
        {
            return objDLL.INSP_Delete_WorklistAttachment(Vessel_ID, Worklist_ID, WL_Office_ID, AttachmentID, Deleted_By);
        }

        public int INSP_Delete_ActivityObject(int Vessel_ID, int Worklist_ID, int WL_Office_ID, int AttachmentID, int Deleted_By)
        {
            return objDLL.INSP_Delete_ActivityObject(Vessel_ID, Worklist_ID, WL_Office_ID, AttachmentID, Deleted_By);
        }

        public DataTable Get_InspectorList()
        {
            try
            {
                return objDLL.Get_InspectorList_DL();
            }
            catch
            {
                throw;
            }
        }

        public int Upd_Worklist_Status(int Vessel_ID, int Worklist_ID, int WL_Office_ID, int User_ID, string Remark, string Action_Type)
        {
            return objDLL.Upd_Worklist_Status(Vessel_ID, Worklist_ID, WL_Office_ID, User_ID, Remark, Action_Type);
        }

        public DataTable Get_Reqsn_List(string Search, int Vessel_ID)
        {
            return objDLL.Get_Reqsn_List(Search, Vessel_ID);
        }

        public int INSP_Delete_Activity(int VESSEL_ID, int WorkList_ID, int OFFICE_ID)
        {
            try
            {
                return objDLL.INSP_Delete_Activity(VESSEL_ID, WorkList_ID, OFFICE_ID);
            }
            catch
            {
                throw;
            }
            finally
            {
            }

        }
        public bool Get_Worklist_Access_ByUser(int User_ID, string Action_Type, string JOB_TYPE)
        {
            bool wlAccess = false;
            DataTable dt = objDLL.Get_Worklist_Access_ByUser(User_ID, Action_Type, JOB_TYPE);
            if (dt.Rows.Count > 0)
                wlAccess = dt.Rows[0]["active_status"].ToString() == "1" ? true : false;

            return wlAccess;
        }

        public DataTable Get_WL_JobCreationTrend()
        {
            return objDLL.Get_WL_JobCreationTrend();
        }
        public DataTable Get_WL_JobsUpdated_InLast24Hours()
        {
            return objDLL.Get_WL_JobsUpdated_InLast24Hours();
        }
        public DataTable Get_WL_JobsCreated_InLast24Hours()
        {
            return objDLL.Get_WL_JobsCreated_InLast24Hours();
        }
        public DataTable Get_WL_Jobs_Super_Inspected()
        {
            return objDLL.Get_WL_Jobs_Super_Inspected();
        }
        public DataTable Get_WL_NCR_Raised_InLast3Months()
        {
            return objDLL.Get_WL_NCR_Raised_InLast3Months();
        }
        public DataTable Get_WL_NCR_Trend_BarChart()
        {
            return objDLL.Get_WL_NCR_Trend_BarChart();
        }
        public DataTable Get_WL_Super_Inspected_BarChart()
        {
            return objDLL.Get_WL_Super_Inspected_BarChart();
        }
        public DataTable Get_WL_HighPriority_Jobs()
        {
            return objDLL.Get_WL_HighPriority_Jobs();
        }

        public DataTable Get_Worklist_Audio_Files()
        {
            return objDLL.Get_Worklist_Audio_Files();
        }

        public int Upd_Worklist_Attachment_FOLLOWUP(int Vessel_ID, int Followup_ID, int WL_Office_ID, int User_ID, string Followup_Status, string Attach_Path, string Action_type, int Worklist_ID, string Followup)
        {
            return objDLL.Upd_Worklist_Attachment_FOLLOWUP(Vessel_ID, Followup_ID, WL_Office_ID, User_ID, Followup_Status, Attach_Path, Action_type, Worklist_ID, Followup);
        }



        public DataTable Generate_Report(int InspectionDetailId, int ShowImages)
        {
            return objDLL.Generate_Report(InspectionDetailId, ShowImages);
        }
        public DataSet INSP_Get_WorklistReport(int InspectionID)
        {
            return objDLL.INSP_Get_WorklistReport(InspectionID);
        }


        public DataTable Get_Crewlist_Index_By_Vessel(string SearchText, int? RankID, int? Status, int? UserID, int? PAGE_SIZE, int? PAGE_INDEX, string _SORT_COLUMN, string _SORT_DIRECTION, int? Vessel_ID, ref int SelectRecordCount)
        {
            try
            {
                return objDLL.Get_Crewlist_Index_By_Vessel(SearchText, RankID, Status, UserID, PAGE_SIZE, PAGE_INDEX, _SORT_COLUMN, _SORT_DIRECTION, Vessel_ID, ref SelectRecordCount);
            }
            catch
            {
                throw;
            }
            finally
            {
            }
        }
        public DataSet Get_WorkList_Index_Filter(int Filter_Type, int PAGE_INDEX, int? PAGE_SIZE, ref int Record_Count)
        {

            return objDLL.Get_WorkList_Index_Filter(Filter_Type, PAGE_INDEX, PAGE_SIZE, ref Record_Count);
        }
        public DataSet Get_WorkList_Index_Filter(int Filter_Type, int PAGE_INDEX, int? PAGE_SIZE, ref int Record_Count, string SortBy, string SORT_DIRECTION)
        {

            return objDLL.Get_WorkList_Index_Filter(Filter_Type, PAGE_INDEX, PAGE_SIZE, ref Record_Count, SortBy, SORT_DIRECTION);
        }
        public DataSet Generate_Excel_Report(DataTable WLIDS)
        {
            return objDLL.Generate_Excel_Report(WLIDS);
        }


        //Added by vasu Dt:18-02-2016
        #region Function_Location_SubLocation

        public DataTable GetSettingforFunctions()
        {
            return objDLL.GetSettingforFunctions();
        }

        public int SaveJobFunctionSetting(DataTable dt, int _logUserID)
        {
            return objDLL.SaveJobFunctionSetting(dt, _logUserID);
        }

        public DataTable GetJob_Function_Details(int OFFICE_ID, int WORKLIST_ID, int VESSEL_ID)
        {
            return objDLL.GetJob_Function_Details(OFFICE_ID, WORKLIST_ID, VESSEL_ID);
        }

        public DataTable GET_SYSTEM_LOCATION(int Function, int VESSEL_ID)
        {

            return objDLL.GET_SYSTEM_LOCATION(Function, VESSEL_ID);
        }


        public DataTable GET_SUBSYTEMSYSTEM_LOCATION(string SYSTEMCODE, int? SUBSYSTEMID, int VESSEL_ID)
        {
            return objDLL.GET_SUBSYTEMSYSTEM_LOCATION(SYSTEMCODE, SUBSYSTEMID, VESSEL_ID);

        }
        public string asyncGet_Function_Information(int Worklist_ID, int WL_Office_ID, int Vessel_ID)
        {
            return objDLL.asyncGet_Function_Information(Convert.ToInt32(Worklist_ID), Convert.ToInt32(WL_Office_ID), Convert.ToInt32(Vessel_ID));
        }

        public DataTable Set_Exist_SystemLocation(int code)
        {
            return objDLL.Set_Exist_SystemLocation(code);
        }

        public DataTable Set_Exist_SubSystemLocation(int code)
        {
            return objDLL.Set_Exist_SubSystemLocation(code);
        }
        #endregion

        //Added by vasu Dt:18-02-2016


        public int IsJobAssignedInInspection(int Vessel_ID, int WL_Office_ID, int Worklist_ID)
        {
            return objDLL.IsJobAssignedInInspection( Vessel_ID,  WL_Office_ID,  Worklist_ID);
        }
    }

}