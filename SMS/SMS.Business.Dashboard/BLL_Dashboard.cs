using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Web;
using System.ComponentModel;
using System.Collections;
using System.Data.SqlTypes;
using SMS.Data.Dashboard;

namespace SMS.Business.Dashboard
{
    public class BLL_Dashboard
    {
        public static DataTable Dash_FMS_Get_ScheduleFileApprovalOverdue(int UserID)
        {
            return DAL_Dashboard.Dash_FMS_Get_ScheduleFileApprovalOverdue_DL(UserID);
        }
        public static DataTable Dash_FMS_Get_ScheduleFileReceivingOverdue(int UserID)
        {
            return DAL_Dashboard.Dash_FMS_Get_ScheduleFileReceivingOverdue_DL(UserID);
        }
        public static DataTable Dash_Get_Pending_NCR(int? Assignor, int? DepartmentID)
        {
            return DAL_Dashboard.Dash_Get_Pending_NCR_DL(Assignor, DepartmentID);
        }
        public static DataTable Dash_Get_Pending_NCR_ALL_Dept(int? Assignor)
        {
            return DAL_Dashboard.Dash_Get_Pending_NCR_ALL_Dept_DL(Assignor);
        }
        public static DataTable Dash_Get_Pending_Travel_PO(int User_ID)
        {
            return DAL_Dashboard.Dash_Get_Pending_Travel_PO_DL(User_ID);
        }
        public static DataTable Dash_Get_Pending_Logistic_PO(int User_ID)
        {
            return DAL_Dashboard.Dash_Get_Pending_Logistic_PO_DL(User_ID);
        }
        public static DataTable Dash_Get_Pending_Reqsn(int User_ID)
        {
            return DAL_Dashboard.Dash_Get_Pending_Reqsn_DL(User_ID);
        }
        public static DataSet Dash_Get_Provision_Last_Supplied()
        {
            return DAL_Dashboard.Dash_Get_Provision_Last_Supplied_DL();
        }
        public static DataTable Dash_Get_User_Menu_Favourite(int UserID)
        {
            return DAL_Dashboard.Dash_Get_User_Menu_Favourite_DL(UserID);
        }
        public static DataTable Dash_Get_Pending_WorkList(int UserID)
        {
            return DAL_Dashboard.Dash_Get_Pending_WorkList_DL(UserID);
        }
        public static DataTable Dash_Get_Pending_TotalWorkListToVerify()
        {
            return DAL_Dashboard.Dash_Get_Pending_TotalWorkListToVerify_DL();
        }
        public static DataTable Dash_GetPendingBriefingList(int USer_ID)
        {
            return DAL_Dashboard.Dash_GetPendingBriefingList_DL(USer_ID);
        }
        public static DataTable Dash_getPendingInterviewList(int UserID)
        {
            return DAL_Dashboard.Dash_getPendingInterviewList_DL(UserID);
        }
        public static DataTable Dash_getPendingInterviewListt_By_UserID(int UserID)
        {
            return DAL_Dashboard.Dash_getPendingInterviewList_By_UserID_DL(UserID);
        }
        public static DataTable Dash_Get_WorkList_DueIn_7Days()
        {
            return DAL_Dashboard.Dash_Get_WorkList_DueIn_7Days_DL();
        }
        public static DataTable Dash_Get_Pending_CTM_Approval(int UserID)
        {
            return DAL_Dashboard.Dash_Get_Pending_CTM_Approval_DL(UserID);
        }
        public static DataTable Dash_Get_CTM_Confirmation_Not_Received(int UserID)
        {
            return DAL_Dashboard.Dash_Get_CTM_Confirmation_Not_Received_DL(UserID);
        }
        public static DataTable Dash_Get_OpsWorklistDueIn7Days(int UserID)
        {
            return DAL_Dashboard.Dash_Get_OpsWorklistDueIn7Days_DL(UserID);
        }
        public static DataTable Dash_Get_Surv_DueinNext30Days(int UserID)
        {
            return DAL_Dashboard.Dash_Get_Surv_DueinNext30Days_DL(UserID);
        }
        public static DataTable Dash_Get_Surv_DueinNext7DaysAndOverdue(int UserID)
        {
            return DAL_Dashboard.Dash_Get_Surv_DueinNext7DaysAndOverdue_DL(UserID);
        }
        public static DataTable Dash_Get_Surv_PendingVerification(int UserID)
        {
            return DAL_Dashboard.Dash_Get_Surv_PendingVerification_DL(UserID);
        }
        public static DataTable Dash_Get_Surv_ExpDateBydayCount(int UserID, string ExpairyFromDaysCount, string ExpairyToDaysCount)
        {
            return DAL_Dashboard.Dash_Get_Surv_ExpDateBydayCount_DL(UserID, ExpairyFromDaysCount, ExpairyToDaysCount);
        }
        public static DataTable Dash_Get_PMS_Overdue_Job_Count(int? UserID)
        {
            return DAL_Dashboard.Dash_Get_PMS_Overdue_Job_Count_DL(UserID);
        }
        public static DataTable Dash_Get_Cylinder_Oil_Consumption(int UserID)
        {
            return DAL_Dashboard.Dash_Get_Cylinder_Oil_Consumption_DL(UserID);
        }
        public static string Dash_Get_Events_Done(int UserID)
        {
            return DAL_Dashboard.Dash_Get_Events_Done_DL(UserID);
        }
        public static DataTable Dash_Get_OverDue_Inspection(int User_ID)
        {
            return DAL_Dashboard.Dash_Get_OverDue_Inspection_DL(User_ID);
        }
        public static DataSet Dash_Get_CrewPerformance(int User_ID)
        {
            return DAL_Dashboard.Dash_Get_CrewPerformance_DL(User_ID);
        }
        public static DataSet Dash_Get_CrewEvaluationDue(int User_ID)
        {
            return DAL_Dashboard.Dash_Get_CrewEvaluationDue_DL(User_ID);
        }
        public static DataSet Dash_Get_CrewPerformanceVerification(int User_ID)
        {
            return DAL_Dashboard.Dash_Get_CrewPerformanceVerification_DL(User_ID);
        }
        public static DataTable Dash_Get_DueInMonth_Inspection(int User_ID)
        {
            return DAL_Dashboard.Dash_Get_DueInMonth_Inspection_DL(User_ID);
        }
        public static DataTable Dash_Get_Portcalls_Vessel(int UserID)
        {
            return DAL_Dashboard.Dash_Get_Portcalls_Vessel_DL(UserID);
        }
        public static DataTable Dash_Get_Portcalls_Month(int UserID)
        {
            return DAL_Dashboard.Dash_Get_Portcalls_Month_DL(UserID);
        }
        public static DataTable Dash_Get_Completed_Inspection(int UserID)
        {
            return DAL_Dashboard.Dash_Get_Completed_Inspection_DL(UserID);
        }
        public static DataTable Dash_Get_InvItems_BelowTreshold(int UserID)
        {
            return DAL_Dashboard.Dash_Get_InvItems_BelowTreshold_DL(UserID);
        }
        public static DataSet Dash_GetPendingCardApprovalList(int UserID)
        {
            return DAL_Dashboard.Dash_GetPendingCardApprovalList_DL(UserID);
        }
        public static DataTable Dash_Get_Supplier_Evaluation_Search(int? UserID)
        {
            return DAL_Dashboard.Dash_Get_Supplier_Evaluation_Search_DL(UserID);
        }
        public static DataSet Dash_Get_Pending_Invoice_Approval(int? UserID)
        {
            return DAL_Dashboard.Dash_Get_Pending_Invoice_Approval_DL(UserID);
        }
        public static DataTable Dash_Get_Opex_Fleet_Details(int? UserID)
        {
            return DAL_Dashboard.Dash_Get_Opex_Fleet_Details_DL(UserID);
        }
        public static DataTable Dash_Get_CPSnippetData(int USer_ID)
        {
            return DAL_Dashboard.Dash_Get_CPSnippetData_DL(USer_ID);
        }
        public static DataTable Dash_Get_CrewEvaluation_Feedback(int User_ID)
        {
            return DAL_Dashboard.Dash_Get_CrewEvaluation_Feedback_DL(User_ID);
        }
        public static DataTable Dash_Get_CrewOnboardListRankWise(int USer_ID)
        {
            return DAL_Dashboard.Dash_Get_CrewOnboardListRankWise_DL(USer_ID);
        }
        public static DataTable Dash_Get_CrewSeniorityReward(int UserID)
        {
            return DAL_Dashboard.Dash_Get_CrewSeniorityReward_DL(UserID);
        }
        public static DataSet Dash_Get_Opex_Vessel_Report(int? UserID, string Fleet_ID)
        {
            return DAL_Dashboard.Dash_Get_Opex_Vessel_Report_DL(UserID, Fleet_ID);
        }
        public static DataTable Dash_GetFleetList(int UserCompanyID)
        {
            return DAL_Dashboard.Dash_GetFleetList_DL(UserCompanyID);
        }
        public static DataTable Dash_GetDeptType()
        {
            return DAL_Dashboard.Dash_GetDeptType_DL();
        }
        public static DataTable Dash_Get_Fleet_By_UserID(int UserID)
        {
            return DAL_Dashboard.Dash_Get_Fleet_By_UserID_DL(UserID);
        }
        public static DataSet Dash_Get_Rreqsn_Count(string FormType, int Fleet, string User_ID)
        {
            return DAL_Dashboard.Dash_Get_Rreqsn_Count_DL(FormType, Fleet, User_ID);
        }
        public static DataTable Dash_Get_Reqsn_Processing_Time(string FormType, int Fleet, int User_ID)
        {
            return DAL_Dashboard.Dash_Get_Reqsn_Processing_Time_DL(FormType, Fleet, User_ID);
        }
        public static DataSet Dash_Get_Decklog_Anomalies(int User_ID)
        {
            return DAL_Dashboard.Dash_Get_Decklog_Anomalies(User_ID);
        }
        public static DataSet Dash_Get_Enginelog_Anomalies(int User_ID)
        {
            return DAL_Dashboard.Dash_Get_Enginelog_Anomalies(User_ID);
        }
        public static DataTable Dash_Get_Evaluation_60Percent(int User_ID)
        {
            return DAL_Dashboard.Dash_Get_Evaluation_60Percent_DL(User_ID);
        }
        public static DataTable Dash_Get_Evaluation_Schedules(int User_ID)
        {
            return DAL_Dashboard.Dash_Get_Evaluation_Schedules_DL(User_ID);
        }
        public static DataTable Dash_Get_CrewCardIndex(int FlletCode, int VesselID, int Nationality, int ApprovalStatus, string SearchText, int UserID)
        {
            return DAL_Dashboard.Dash_Get_CrewCardIndex_DL(FlletCode, VesselID, Nationality, ApprovalStatus, SearchText, UserID);
        }
        public static DataSet Dash_Get_CrewComplaintList(int CrewID, int UserID)
        {
            return DAL_Dashboard.Dash_Get_CrewComplaintList_DL(CrewID, UserID);
        }
        public static DataTable Dash_Get_VoyageSnippetData(int USer_ID)
        {
            return DAL_Dashboard.Dash_Get_VoyageSnippetData_DL(USer_ID);
        }
        public static DataTable Dash_OPS_Get_RecentCPROB()
        {
            return DAL_Dashboard.Dash_OPS_Get_RecentCPROB_DL();
        }
        public static DataTable Dash_Get_Snippet_Access_OnDashboard(int User_ID, int? DepID)
        {
            return DAL_Dashboard.Dash_Get_Snippet_Access_OnDashboard_DL(User_ID, DepID);
        }
        public static string Dash_GET_DashBoard_LayoutByUser(int User_ID, int DepID)
        {
            return DAL_Dashboard.Dash_GET_DashBoard_LayoutByUser_DL(User_ID, DepID);
        }
        public static DataSet Dash_GET_PerformacneManager(int USer_ID, int Days)
        {
            return DAL_Dashboard.Dash_GET_PerformacneManager_DL(USer_ID, Days);
        }
        public static DataTable Dash_Get_WorkList_Incident_180days()
        {
            return DAL_Dashboard.Dash_Get_WorkList_Incident_180days_DL();
        }
        public static DataTable Dash_Get_WorkList_NearMiss_180days()
        {
            return DAL_Dashboard.Dash_Get_WorkList_NearMiss_180days_DL();
        }
        public static void Dash_Insert_Dashboard_LayoutByUser(int User_ID, string Layout, int DepID)
        {
            DAL_Dashboard.Dash_Insert_Dashboard_LayoutByUser_DL(User_ID, Layout, DepID);
        }
        public static DataTable Dash_Get_MyOperationWorklist(int UserID)
        {
            return DAL_Dashboard.Dash_Get_MyOperationWorklist_DL(UserID);
        }
        public static DataTable Dash_Get_OpsWorklistOverdue(int UserID)
        {
            return DAL_Dashboard.Dash_Get_OpsWorklistOverdue_DL(UserID);
        }
        public static string Dash_Update_SnippetColor(int User_ID, string SnippetID, string color)
        {
            return DAL_Dashboard.Dash_Update_SnippetColor_DL( User_ID, SnippetID, color);
        }
        public static string Dash_Update_SnippetTitle(int User_ID, string SnippetID, string Title)
        {
            return DAL_Dashboard.Dash_Update_SnippetTitle_DL( User_ID, SnippetID, Title);
        }
        public static string Mail_CrewOnboardListRankWise(int USer_ID, string MailBody)
        {
            return DAL_Dashboard.Mail_CrewOnboardListRankWise(USer_ID, MailBody);
        }
        public static DataTable Dash_Get_Surv_NA_PendingVerification(int UserID)
        {
            return DAL_Dashboard.Dash_Get_Surv_NA_PendingVerification_DL(UserID);
        }
        public static void Dash_Update_DefaultDashboard(int UserID)
        {
            DAL_Dashboard.Dash_Update_DefaultDashboard_DL(UserID);
        }
        public static DataSet Dash_Get_CrewComplaintLog(int Worklist_ID, int Vessel_ID, int UserID)
        {
            try
            {

                return DAL_Dashboard.Dash_Get_CrewComplaintLog_DL(Worklist_ID, Vessel_ID, UserID);
            }
            catch
            {
                throw;
            }
        }

        public static DataTable Dash_Get_RestHourSnippet(int UserID)
        {
            try
            {

                return DAL_Dashboard.Dash_Get_RestHourSnippet(UserID);
            }
            catch
            {
                throw;
            }
        }


        public static DataTable Get_Crew_Information(int CrewID, DateTime? Date)
        {
            try
            {

                return DAL_Dashboard.Get_Crew_Information(CrewID, Date);
            }
            catch
            {
                throw;
            }
        }


        public static DataTable Get_Crw_RefusedToSignEval()
        {
            try
            {

                return DAL_Dashboard.Get_Crw_RefusedToSignEval();
            }
            catch
            {
                throw;
            }
        }


        public static void Dash_InsertDel_CrwAction_RefusedToSign(int CrewId, int Schedule_ID, int Del)
        {
            DAL_Dashboard.Dash_InsertDel_CrwAction_RefusedToSign(CrewId, Schedule_ID,Del);
        }
        /// <summary>
        /// Method is uesd to display all the vetting inspections that are about to expire in the next 30 days.   
        /// </summary>
        /// <param name="UserId">Login user ID</param>
        /// <returns></returns>
        public static DataTable Dash_Get_Vetting_Exp_In_Next_30Days(int UserId)
        {
            return DAL_Dashboard.Dash_Get_Vetting_Exp_In_Next_30Days(UserId);
        }
        /// <summary>
        /// Method is used to display all the vetting inspections that were expired or failed
        /// </summary>
        /// <param name="UserId">Login user ID</param>
        /// <returns></returns>
        public static DataTable Dash_Get_Exp_Failed_Vetting_Insp(int UserId)
        {
            return DAL_Dashboard.Dash_Get_Exp_Failed_Vetting_Insp(UserId);
        }

        #region for Snippet of PO and Invoice

        public static DataSet Get_POAndInvoice_SummarySnippet()
        {
            return DAL_Dashboard.Get_POAndInvoice_SummarySnippet_DL();
        }

        public static DataTable Get_Pending_POApprovals_Snippet(int? UserID)
        {
            try
            {

                return DAL_Dashboard.Get_Pending_POApprovals_Snippet_DL(UserID);
            }
            catch
            {
                throw;
            }
        }

        public static DataTable Get_Pending_InvoiceVerification_Snippet(int? UserID)
        {
            try
            {

                return DAL_Dashboard.Get_Pending_InvoiceVerification_Snippet_DL(UserID);
            }
            catch
            {
                throw;
            }
        }

        public static DataTable Get_Pending_InvoiceApprovals_Snippet(int? UserID)
        {
            try
            {

                return DAL_Dashboard.Get_Pending_InvoiceApprovals_Snippet_DL(UserID);
            }
            catch
            {
                throw;
            }
        }

        #endregion
    }
    
}
