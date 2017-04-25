using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SMS.Data.Infrastructure;

namespace SMS.Business.Infrastructure
{
    public class BLL_Infra_DashBoard
    {

        public static DataSet Get_Provision_Last_Supplied()
        {
            return DAL_Infra_DashBoard.Get_Provision_Last_Supplied_DL();
        }

        public static DataSet Get_Rreqsn_Count(string FormType, int Fleet, string User_ID)
        {
            return DAL_Infra_DashBoard.Get_Rreqsn_Count_DL(FormType, Fleet, User_ID);
        }

        public static DataTable Get_Pending_Reqsn(int User_ID)
        {
            return DAL_Infra_DashBoard.Get_Pending_Reqsn_DL(User_ID);
        }
        public static DataTable Get_Supplier_Evaluation_Search(int? UserID)
        {
            return DAL_Infra_DashBoard.Get_Supplier_Evaluation_Search(UserID);
        }

        public static DataSet Get_Pending_Invoice_Approval(int? UserID)
        {
            return DAL_Infra_DashBoard.Get_Pending_Invoice_Approval(UserID);
        }
        public static DataTable Get_Opex_Fleet_Details(int? UserID)
        {
            return DAL_Infra_DashBoard.Get_Opex_Fleet_Details(UserID);
        }
        public static DataSet Get_Opex_Vessel_Report(int? UserID,string Fleet_ID)
        {
            return DAL_Infra_DashBoard.Get_Opex_Vessel_Report(UserID, Fleet_ID);
        }
        public static int UPD_User_Menu_Favourite(int UserID, DataTable Menu)
        {
            return DAL_Infra_DashBoard.UPD_User_Menu_Favourite_DL(UserID, Menu);
        }

        public static DataTable Get_User_Menu_List(int UserID)
        {
            return DAL_Infra_DashBoard.Get_User_Menu_List_DL(UserID);
        }

        public static DataTable Get_User_Menu_Favourite(int UserID)
        {
            return DAL_Infra_DashBoard.Get_User_Menu_Favourite_DL(UserID);
        }

        public static DataTable Get_Reqsn_Processing_Time(string FormType, int Fleet, int User_ID)
        {
            return DAL_Infra_DashBoard.Get_Reqsn_Processing_Time_DL(FormType, Fleet, User_ID);
        }

        public static DataTable Get_Evaluation_Schedules(int User_ID)
        {
            return DAL_Infra_DashBoard.Get_Evaluation_Schedules_DL(User_ID);
        }
        public static DataTable Get_Evaluation_60Percent(int User_ID)
        {
            return DAL_Infra_DashBoard.Get_Evaluation_60Percent_DL(User_ID);
        }

        public static DataTable Get_Snippet_Access(int User_ID ,int? DepID)
        {
            return DAL_Infra_DashBoard.Get_Snippet_Access_DL(User_ID, DepID);
        }
        public static DataTable Get_Snippet_Access_OnDashboard(int User_ID, int? DepID)
        {
            return DAL_Infra_DashBoard.Get_Snippet_Access_OnDashboard_DL(User_ID, DepID);
        }
        public static int UPD_Snippet_Access(int UserID, DataTable Snippet, int CreatedBy)
        {
            return DAL_Infra_DashBoard.UPD_Snippet_Access_DL(UserID, Snippet, CreatedBy);
        }
        public static DataTable Get_Pending_NCR(int? Assignor, int? DepartmentID)
        {
            return DAL_Infra_DashBoard.Get_Pending_NCR_DL(Assignor, DepartmentID);
        }

        public static DataTable Get_Pending_NCR_ALL_Dept(int? Assignor)
        {
            return DAL_Infra_DashBoard.Get_Pending_NCR_ALL_Dept_DL(Assignor);
        }


        public static DataTable Get_Pending_Travel_PO(int User_ID)
        {
            return DAL_Infra_DashBoard.Get_Pending_Travel_PO_DL(User_ID);
        }

        public static DataTable Get_Pending_Logistic_PO(int User_ID)
        {
            return DAL_Infra_DashBoard.Get_Pending_Logistic_PO_DL(User_ID);
        }
        public static DataTable Get_Portcalls_Month(int UserID)
        {
            return DAL_Infra_DashBoard.Get_Portcalls_Month_DL(UserID);
        }

        public static DataTable Get_Portcalls_Vessel(int UserID)
        {
            return DAL_Infra_DashBoard.Get_Portcalls_Vessel_DL(UserID);
        }

        public static DataTable Get_Pending_WorkList(int UserID)
        {
            return DAL_Infra_DashBoard.Get_Pending_WorkList_DL(UserID);
        }

        public static DataTable Get_Pending_TotalWorkListToVerify()
        {
            return DAL_Infra_DashBoard.Get_Pending_TotalWorkListToVerify_DL();
        }

        public static DataTable getPendingInterviewList(int UserID)
        {
            return DAL_Infra_DashBoard.getPendingInterviewList_DL(UserID);
        }

        public static DataTable getPendingInterviewListt_By_UserID(int UserID)
        {
            return DAL_Infra_DashBoard.getPendingInterviewList_By_UserID_DL(UserID);
        }

        public static DataTable Get_WorkList_DueIn_7Days()
        {
            return DAL_Infra_DashBoard.Get_WorkList_DueIn_7Days_DL();
        }

        public static DataTable Get_Pending_CTM_Approval(int UserID)
        {
            return DAL_Infra_DashBoard.Get_Pending_CTM_Approval_DL(UserID);
        }
        public static DataTable Get_CTM_Confirmation_Not_Received(int UserID)
        {
            return DAL_Infra_DashBoard.Get_CTM_Confirmation_Not_Received_DL(UserID);
        }

        public DataTable Search_DashBoardSnippet(string searchtext, int? Deptid, int? AutoRefresh , string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            return DAL_Infra_DashBoard.Search_DashBoardSnippet(searchtext, Deptid, AutoRefresh, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);
        }

        public static DataTable List_DashBoardSnippet(int? ID)
        {
            return DAL_Infra_DashBoard.List_DashBoardSnippet(ID);
        }

        public static int Insert_DashBoardSnippet(string Snippet_ID, string Snippet_Name, string Snippet_Function_Name, int? Department_ID, string Department_Color, int? Auto_Refresh, int? CreatedBy)
        {
            return DAL_Infra_DashBoard.Insert_DashBoardSnippet(Snippet_ID, Snippet_Name, Snippet_Function_Name, Department_ID, Department_Color, Auto_Refresh, CreatedBy);
        }

        public static int Edit_DashBoardSnippet(int ID, string Snippet_ID, string Snippet_Name, string Snippet_Function_Name, int? Department_ID, string Department_Color, int? Auto_Refresh, int? Modified_By)
        {
            return DAL_Infra_DashBoard.Edit_DashBoardSnippet(ID, Snippet_ID, Snippet_Name, Snippet_Function_Name, Department_ID, Department_Color, Auto_Refresh, Modified_By);
        }

        public static int Delete_DashBoardSnippet(int ID, int DeletedBy)
        {
            return DAL_Infra_DashBoard.Delete_DashBoardSnippet(ID, DeletedBy);
        }

        public static DataTable Get_PMS_Overdue_Job_Count_DL(int? User_ID)
        {
            return DAL_Infra_DashBoard.Get_PMS_Overdue_Job_Count_DL(User_ID);
        }
        public static DataSet Get_Decklog_Anomalies(int User_ID)
        {
            return DAL_Infra_DashBoard.Get_Decklog_Anomalies(User_ID);
        }

        public static DataSet Get_Enginelog_Anomalies(int User_ID)
        {
            return DAL_Infra_DashBoard.Get_Enginelog_Anomalies(User_ID);
        }
        public static DataTable Get_OverDue_Inspection(int User_ID)
        {
            return DAL_Infra_DashBoard.Get_OverDue_Inspection(User_ID);
        }
        public static DataTable Get_DueInMonth_Inspection(int User_ID)
        {
            return DAL_Infra_DashBoard.Get_DueInMonth_Inspection(User_ID);
        }
        public static DataTable Get_Completed_Inspection(int UserID)
        {
            return DAL_Infra_DashBoard.Get_Completed_Inspection_DL(UserID);
        }
        public static DataTable Get_InvItems_BelowTreshold(int UserID)
        {
            return DAL_Infra_DashBoard.Get_InvItems_BelowTreshold_DL(UserID);
        }
        public static DataSet GetPendingCardApprovalList(int UserID)
        {
            return DAL_Infra_DashBoard.GetPendingCardApprovalList_DL(UserID);
        }
        public static DataTable Get_CrewEvaluation_Feedback(int User_ID)
        {
            return DAL_Infra_DashBoard.Get_CrewEvaluation_Feedback(User_ID);
        }
        public static DataTable GetPendingBriefingList(int USer_ID)
        {
            return DAL_Infra_DashBoard.GetPendingBriefingList(USer_ID);
        }
        public static DataTable Get_CrewOnboardListRankWise(int USer_ID)
        {
            return DAL_Infra_DashBoard.Get_CrewOnboardListRankWise(USer_ID);
        }
        public static DataTable Get_CrewSeniorityReward(int UserID)
        {
            return DAL_Infra_DashBoard.Get_CrewSeniorityReward(UserID);
        }
        public static string Mail_CrewOnboardListRankWise(int USer_ID, string MailBody)
        {
            return DAL_Infra_DashBoard.Mail_CrewOnboardListRankWise(USer_ID, MailBody);
        }
        public static DataTable Get_VoyageSnippetData(int USer_ID)
        {
             return DAL_Infra_DashBoard.Get_VoyageSnippetData(USer_ID);
        }

        public static DataTable Get_CPSnippetData(int USer_ID)
        {
            return DAL_Infra_DashBoard.Get_CPSnippetData(USer_ID);
        }
        public static DataSet GET_PerformacneManager(int USer_ID, int Days)
        {
            return DAL_Infra_DashBoard.GET_PerformacneManager(USer_ID, Days);
        }
        public static DataTable Get_WorkList_Incident_180days()
        {
            return DAL_Infra_DashBoard.Get_WorkList_Incident_180days();
        }
        public static DataTable Get_WorkList_NearMiss_180days()
        {
            return DAL_Infra_DashBoard.Get_WorkList_NearMiss_180days();
        }

        #region for Snippet of PO and Invoice

        public static DataSet Get_POAndInvoice_SummarySnippet()
        {
            return DAL_Infra_DashBoard.Get_POAndInvoice_SummarySnippet_DL();
        }


        public static DataTable Get_Pending_POApprovals_Snippet(int? UserID)
        {
            try
            {

                return DAL_Infra_DashBoard.Get_Pending_POApprovals_Snippet_DL(UserID);
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

                return DAL_Infra_DashBoard.Get_Pending_InvoiceVerification_Snippet_DL(UserID);
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

                return DAL_Infra_DashBoard.Get_Pending_InvoiceApprovals_Snippet_DL(UserID);
            }
            catch
            {
                throw;
            }
        }
        #endregion
    }
}
