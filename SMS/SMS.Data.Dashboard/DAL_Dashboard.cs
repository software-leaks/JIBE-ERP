using System;
using System.Data;
using System.Configuration;
using SMS.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace SMS.Data.Dashboard
{
    public class DAL_Dashboard
    {
        static string _internalConnection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
        public static DataTable Dash_FMS_Get_ScheduleFileApprovalOverdue_DL(int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
			{
				new SqlParameter("@UserID",UserID),
			   
			};
            DataSet ds = new DataSet();
            ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "FMS_Get_ScheduleFileApprovalOverdue", sqlprm);
            return ds.Tables[0];
        }

        public static DataTable Dash_FMS_Get_ScheduleFileReceivingOverdue_DL(int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
			{
				new SqlParameter("@UserID",UserID),
			 
			};
            DataSet ds = new DataSet();
            ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "FMS_Get_ScheduleFileReceivingOverdue", sqlprm);
            return ds.Tables[0];
        }
        public static DataTable Dash_Get_Pending_NCR_DL(int? Assignor, int? DepartmentID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                           new SqlParameter("@ASSIGNOR",Assignor),
                                           new SqlParameter("@DEPT_OFFICE",DepartmentID)
                                           
                                        };

            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "DASH_GET_Pending_NCR", sqlprm).Tables[0];
        }
        public static DataTable Dash_Get_Pending_NCR_ALL_Dept_DL(int? Assignor)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                           new SqlParameter("@ASSIGNOR",Assignor)                                           
                                        };

            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "DASH_GET_Pending_NCR_ALL_DEPT", sqlprm).Tables[0];
        }
        public static DataTable Dash_Get_Pending_Travel_PO_DL(int User_ID)
        {
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "DASH_Get_Pending_Travel_PO", new SqlParameter("@User_ID", User_ID)).Tables[0];
        }

        public static DataTable Dash_Get_Pending_Logistic_PO_DL(int User_ID)
        {
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "DASH_Get_Pending_Logistic_PO", new SqlParameter("@User_ID", User_ID)).Tables[0];
        }
        public static DataTable Dash_Get_Pending_Reqsn_DL(int User_ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                           new SqlParameter("@UserID",User_ID)
                                        };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "DASH_GET_PENDING_REQSN", sqlprm).Tables[0];
        }
        public static DataSet Dash_Get_Provision_Last_Supplied_DL()
        {
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "DASH_GET_PROV_LAST_SUPPLIED");
        }
        public static DataTable Dash_Get_User_Menu_Favourite_DL(int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                           new SqlParameter("@USER_ID",UserID),
                                           
                                        };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "DASH_GET_USER_MENU_FAVOURITE", sqlprm).Tables[0];

        }
        public static DataTable Dash_Get_Pending_WorkList_DL(int UserID)
        {
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "DASH_Get_Pending_WorkList", new SqlParameter("@UserID", UserID)).Tables[0];
        }
        public static DataTable Dash_Get_Pending_TotalWorkListToVerify_DL()
        {
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "DASH_Get_Pending_TotalWorkListToVerify").Tables[0];
        }
        public static DataTable Dash_GetPendingBriefingList_DL(int USer_ID)
        {
            SqlParameter[] sqlparam = new SqlParameter[] 
            {
                 new SqlParameter("@UserID",USer_ID)
            };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "DASH_GET_Pending_CrewBriefing", sqlparam).Tables[0];

        }
        public static DataTable Dash_getPendingInterviewList_DL(int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@UserID",UserID)
                                        };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "SP_CRW_Get_PendingInterviewList", sqlprm).Tables[0];
        }
        public static DataTable Dash_getPendingInterviewList_By_UserID_DL(int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@UserID",UserID)
                                        };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "SP_CRW_Get_PendingInterviewList_By_UserID", sqlprm).Tables[0];
        }
        public static DataTable Dash_Get_WorkList_DueIn_7Days_DL()
        {
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "DASH_Get_WorkList_DueIn_7Days").Tables[0];
        }
        public static DataTable Dash_Get_Pending_CTM_Approval_DL(int UserID)
        {
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "DASH_Get_Pending_CTM_Approval", new SqlParameter("@User_ID", UserID)).Tables[0];
        }
        public static DataTable Dash_Get_CTM_Confirmation_Not_Received_DL(int UserID)
        {
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "ACC_GET_CTM_Confirmation_Not_Received", new SqlParameter("@User_ID", UserID)).Tables[0];
        }
        public static DataTable Dash_Get_OpsWorklistDueIn7Days_DL(int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
              { 
                new SqlParameter("@UserID",UserID)
            };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "OPS_SP_Get_WorklistDueIn7Days", sqlprm).Tables[0];
        }
        public static DataTable Get_Surv_DueinNext30Days_DL(int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            { 
                new SqlParameter("@UserID", UserID)
            };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "DASH_Get_Surv_DueinNext30Days", sqlprm).Tables[0];
        }
        public static DataTable Dash_Get_Surv_DueinNext30Days_DL(int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            { 
                new SqlParameter("@UserID", UserID)
            };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "DASH_Get_Surv_DueinNext30Days", sqlprm).Tables[0];
        }
        public static DataTable Dash_Get_Surv_DueinNext7DaysAndOverdue_DL(int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            { 
                new SqlParameter("@UserID", UserID)
            };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "DASH_Get_Surv_DueinNext7DaysAndOverdue", sqlprm).Tables[0];
        }
        public static DataTable Dash_Get_Surv_PendingVerification_DL(int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            { 
                new SqlParameter("@UserID", UserID)
            };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "DASH_Get_Surv_PendingVerification", sqlprm).Tables[0];
        }
        public static DataTable Dash_Get_Surv_ExpDateBydayCount_DL(int UserID, string ExpairyFromDaysCount, string ExpairyToDaysCount) 
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            { 
                 new SqlParameter("@UserID", UserID),
                new SqlParameter("@ExpairyFromDaysCount", Convert.ToInt32(ExpairyFromDaysCount)),
                  new SqlParameter("@ExpairyToDaysCount",Convert.ToInt32(ExpairyToDaysCount))
               
            };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "DASH_Get_Surv_ExpDateBydayCount", sqlprm).Tables[0];
        }
        public static DataTable Dash_Get_PMS_Overdue_Job_Count_DL(int? User_ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@UserID",User_ID)
                                        };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "DASH_GET_PMS_OVERDUE_JOB_COUNT", sqlprm).Tables[0];
        }
        public static DataTable Dash_Get_Cylinder_Oil_Consumption_DL(int UserID)
        {
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "DASH_Get_Cylinder_Oil_Consumption", new SqlParameter("@UserID", UserID)).Tables[0];
        }
        public static string Dash_Get_Events_Done_DL(int User_ID)
        {
            return Convert.ToString(SqlHelper.ExecuteScalar(_internalConnection, CommandType.StoredProcedure, "DASH_Get_Events_Done", new SqlParameter("@User_ID", User_ID)));
        }
        public static DataTable Dash_Get_OverDue_Inspection_DL(int User_ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@UserID",User_ID)
                                        };

            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "DASH_GET_OverDueInspection", sqlprm).Tables[0];
        }
        public static DataSet Dash_Get_CrewPerformance_DL(int User_ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@UserID",User_ID)
                                        };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "DASH_GET_CREW_BY_EVALUATION", sqlprm);

        }
        public static DataSet Dash_Get_CrewEvaluationDue_DL(int User_ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@UserID",User_ID)
                                        };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "CRW_EVA_SP_Get_DueEvaluation", sqlprm);

        }
        public static DataSet Dash_Get_CrewPerformanceVerification_DL(int User_ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@UserID",User_ID)
                                        };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "DASH_GET_CREW_BY_EVALUATION_PRFORMANCE", sqlprm);

        }
        public static DataTable Dash_Get_DueInMonth_Inspection_DL(int User_ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@UserID",User_ID)
                                        };

            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "DASH_GET_DueInspection", sqlprm).Tables[0];

        }
        public static DataTable Dash_Get_Portcalls_Vessel_DL(int UserID)
        {
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "DASH_Get_PortCalls_Vessel", new SqlParameter("@UserID", UserID)).Tables[0];
        }
        public static DataTable Dash_Get_Portcalls_Month_DL(int UserID)
        {
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "DASH_Get_PortCalls_Month", new SqlParameter("@UserID", UserID)).Tables[0];
        }
        public static DataTable Dash_Get_Completed_Inspection_DL(int User_ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@UserID",User_ID)
                                        };

            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "DASH_GET_CompletedInspection", sqlprm).Tables[0];

        }
        public static DataTable Dash_Get_InvItems_BelowTreshold_DL(int User_ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@UserID",User_ID)
                                        };

            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "DASH_GET_BelowTreshold_InventoryItems", sqlprm).Tables[0];

        }
        public static DataSet Dash_GetPendingCardApprovalList_DL(int User_ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@UserID",User_ID)
                                        };

            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "DASH_Get_PendingCardApprovalList", sqlprm);

        }
        public static DataTable Dash_Get_Supplier_Evaluation_Search_DL(int? UserID)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@UserID",UserID),
            };

            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "DASH_GET_ASL_PENDING_EVALUATION", obj);
            return ds.Tables[0];
        }
        public static DataSet Dash_Get_Pending_Invoice_Approval_DL(int? UserID)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@UserID",UserID),
            };

            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "DASH_Get_Invoice_Pending_Approval", obj);
            return ds;
        }
        public static DataTable Dash_Get_Opex_Fleet_Details_DL(int? UserID)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@UserID",UserID),
            };

            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "DASH_Get_Opex_Fleet_Details", obj);
            return ds.Tables[0];
        }
        public static DataTable Dash_Get_CPSnippetData_DL(int USer_ID)
        {
            SqlParameter[] sqlparam = new SqlParameter[] 
            {
                 new SqlParameter("@User_Id",USer_ID)
            };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "Dash_Get_CP_SnippetData", sqlparam).Tables[0];

        }
        public static DataTable Dash_Get_CrewEvaluation_Feedback_DL(int User_ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@UserID",User_ID)
                                        };

            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "DASH_Get_CrewEvaluation_Feedback", sqlprm).Tables[0];
        }
        public static DataTable Dash_Get_CrewOnboardListRankWise_DL(int USer_ID)
        {
            SqlParameter[] sqlparam = new SqlParameter[] 
            {
                 new SqlParameter("@UserID",USer_ID)
            };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "DASH_Get_CrewOnboardListRankWise", sqlparam).Tables[0];

        }
        public static DataTable Dash_Get_CrewSeniorityReward_DL(int USer_ID)
        {
            SqlParameter[] sqlparam = new SqlParameter[] 
            {
                 new SqlParameter("@UserID",USer_ID)
            };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "DASH_Get_CrewSeniorityRewardList", sqlparam).Tables[0];

        }
        public static DataSet Dash_Get_Opex_Vessel_Report_DL(int? UserID, string Fleet_ID)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@UserID",UserID),
                   new System.Data.SqlClient.SqlParameter("@Fleet_ID",Fleet_ID),
            };

            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "DASH_Get_Opex_Details", obj);
            return ds;
        }
        public static DataTable Dash_GetFleetList_DL(int UserCompanyID)
        {
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "SP_INF_Get_FleetList", new SqlParameter("UserCompanyID", UserCompanyID)).Tables[0];
        }
        public static DataTable Dash_GetDeptType_DL()
        {
            try
            {
                DataSet res = SqlHelper.ExecuteDataset(_internalConnection, CommandType.Text, "select Short_Code,Description from dbo.PURC_LIB_SYSTEM_PARAMETERS where Parent_Type='157'");
                return res.Tables[0];
            }
            catch (Exception ex)
            {

                throw ex;

            }
        }
        public static DataTable Dash_Get_Fleet_By_UserID_DL(int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]{ 
                                                      new SqlParameter("@UserID",UserID)
                                                        												  };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "INF_GET_FLEET_BY_USERID", sqlprm).Tables[0];

        }
        public static DataSet Dash_Get_Rreqsn_Count_DL(string FormType, int Fleet, string User_ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Form_Type",FormType),
                                            new SqlParameter("@Fleet",Fleet),
                                            new SqlParameter("@UserID",User_ID)
                                        };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "DASH_GET_REQSN_COUNT", sqlprm);
        }
        public static DataTable Dash_Get_Reqsn_Processing_Time_DL(string FormType, int Fleet, int User_ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Form_Type",FormType),
                                            new SqlParameter("@Fleet",Fleet),
                                            new SqlParameter("@UserID",User_ID)
                                        };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "DASH_GET_REQSN_PROCESSING_TIME", sqlprm).Tables[0];

        }
        public static DataSet Dash_Get_Decklog_Anomalies(int User_ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@UserID",User_ID)
                                        };

            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "DASH_GET_DECKLOG_ANOMALIES_SNAPSHOT");
        }
        public static DataSet Dash_Get_Enginelog_Anomalies(int User_ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@UserID",User_ID)
                                        };

            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "DASH_GET_ENGINELOG_ANOMALIES_SNAPSHOT");
        }
        public static DataTable Dash_Get_Evaluation_60Percent_DL(int User_ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@UserID",User_ID)
                                        };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "CRW_EVA_SP_Get_Evaluation_60Percent", sqlprm).Tables[0];

        }
        public static DataTable Dash_Get_Evaluation_Schedules_DL(int User_ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@UserID",User_ID)
                                        };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "CRW_EVA_SP_Get_EvaluationSchedules", sqlprm).Tables[0];

        }
        public static DataTable Dash_Get_CrewCardIndex_DL(int FlletCode, int VesselID, int Nationality, int ApprovalStatus, string SearchText, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Fleet",FlletCode),
                                            new SqlParameter("@Vessel",VesselID),
                                            new SqlParameter("@Nationality",Nationality),
                                            new SqlParameter("@Status",ApprovalStatus),
                                            new SqlParameter("@SearchText",SearchText),
                                            new SqlParameter("@UserID",UserID)
                                        };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "SP_CRW_Get_CrewCardIndex", sqlprm).Tables[0];

        }
        public static DataSet Dash_Get_CrewComplaintList_DL(int CrewID, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewID",CrewID),
                                            new SqlParameter("@UserID",UserID)
                                        };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "SP_CRW_Get_CrewComplaintList", sqlprm);

        }
        public static DataTable Dash_OPS_Get_RecentCPROB_DL()
        {
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "OPS_Get_RecentCPROB").Tables[0];
        }
        public static DataTable Dash_Get_VoyageSnippetData_DL(int User_ID)
        {
            SqlParameter[] sqlparam = new SqlParameter[] 
            {
                 new SqlParameter("@User_Id",User_ID)
            };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "Dash_Get_VoyageSnippetData", sqlparam).Tables[0];

        }
        public static DataTable Dash_Get_Snippet_Access_OnDashboard_DL(int User_ID, int? DepID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@User_ID",User_ID),
                                            new SqlParameter("@DepID",DepID)
                                        };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "DASH_GET_Snippet_Access_OnDashboard", sqlprm).Tables[0];

        }
        public static string Dash_GET_DashBoard_LayoutByUser_DL(int User_ID, int DepID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                           new SqlParameter("@User_ID", User_ID),
                                            new SqlParameter("@DepID",DepID),
                                         };
            return Convert.ToString(SqlHelper.ExecuteScalar(_internalConnection, CommandType.StoredProcedure, "DASH_GET_LayoutByUser", sqlprm));
        }
        public static DataSet Dash_GET_PerformacneManager_DL(int User_ID, int Days)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@UserID",User_ID),
                                             new SqlParameter("@Days",Days)
                                        };

            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "DASH_GET_PerformacneManager", sqlprm);

        }
        public static DataTable Dash_Get_WorkList_Incident_180days_DL()
        {
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "DASH_Get_WorkList_Incident_180days").Tables[0];
        }
        public static DataTable Dash_Get_WorkList_NearMiss_180days_DL()
        {
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "DASH_Get_WorkList_NearMiss_180days").Tables[0];
        }
        public static void Dash_Insert_Dashboard_LayoutByUser_DL(int User_ID, string Layout, int DepID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@USER_ID",User_ID),
                                            new SqlParameter("@Layout",Layout),
                                            new SqlParameter("@DepID",DepID),
                                         };

            SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "DASH_Insert_LayoutByUser", sqlprm);
        }
        public static DataTable Dash_Get_MyOperationWorklist_DL(int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
              { 
                new SqlParameter("@UserID",UserID)
            };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "OPS_SP_Get_MyTaskList", sqlprm).Tables[0];
        }
        public static DataTable Dash_Get_OpsWorklistOverdue_DL(int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
              { 
                new SqlParameter("@UserID",UserID)
            };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "OPS_SP_Get_WorklistOverdue", sqlprm).Tables[0];
        }
        public static string Dash_Update_SnippetColor_DL(int User_ID, string SnippetID, string color)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@USER_ID",User_ID),
                                            new SqlParameter("@SnippetID",SnippetID),
                                           new SqlParameter("@color",color),
                                         };
            return Convert.ToString(SqlHelper.ExecuteScalar(_internalConnection, CommandType.StoredProcedure, "DASH_UPD_SnippetColor", sqlprm));
        }
        public static string Dash_Update_SnippetTitle_DL(int User_ID, string SnippetID, string Title)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@USER_ID",User_ID),
                                            new SqlParameter("@SnippetID",SnippetID),
                                           new SqlParameter("@Title",Title),
                                         };
            return Convert.ToString(SqlHelper.ExecuteScalar(_internalConnection, CommandType.StoredProcedure, "DASH_UPD_SnippetTitle", sqlprm));
        }
        public static string Mail_CrewOnboardListRankWise(int USer_ID, string MailBody)
        {
            SqlParameter[] sqlparam = new SqlParameter[] 
            {
                 new SqlParameter("@UserID",USer_ID),
                  new SqlParameter("@MailBody",MailBody),
            };
            return SqlHelper.ExecuteScalar(_internalConnection, CommandType.StoredProcedure, "DASH_Mail_CrewOnboardListRankWise", sqlparam).ToString();

        }
        public static DataTable Dash_Get_Surv_NA_PendingVerification_DL(int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            { 
                new SqlParameter("@UserID", UserID)
            };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "DASH_Get_Surv_NA_PendingVerification", sqlprm).Tables[0];
        }
        public static void Dash_Update_DefaultDashboard_DL(int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            { 
                new SqlParameter("@USER_ID", UserID)
            };
            SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "DASH_UPD_DefaultSetting", sqlprm);
        }
        public static DataSet Dash_Get_CrewComplaintLog_DL(int Worklist_ID, int Vessel_ID, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Worklist_ID",Worklist_ID),
                                            new SqlParameter("@Vessel_ID",Vessel_ID),
                                            new SqlParameter("@UserID",UserID)
                                        };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "SP_CRW_Get_CrewComplaintLog", sqlprm);
        }

        public static DataTable Dash_Get_RestHourSnippet(int UserID)
        {
            SqlParameter[] sqlparam = new SqlParameter[] 
            {
                 new SqlParameter("@User_Id",UserID)
            };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "Dash_Get_RestHourData", sqlparam).Tables[0];

        }

        public static DataTable Get_Crew_Information(int CrewID, DateTime? Date)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewID",CrewID),
                                            new SqlParameter("@DATE",Date),
                                           
                                        };

            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "INF_GET_CREW_INFORMATION", sqlprm).Tables[0];
        }

        public static DataTable Get_Crw_RefusedToSignEval()
        {
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "CRW_Evaluation_SP_Get_CrwRefusedToSignEval").Tables[0];

        }


        public static void Dash_InsertDel_CrwAction_RefusedToSign(int CrewId, int Schedule_ID, int Del)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewId",CrewId),
                                            new SqlParameter("@Schedule_ID",Schedule_ID),
                                            new SqlParameter("@Del",Del)
                                         };

            SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "CRW_Evaluation_SP_Insert_CrwActionRefusedToSign", sqlprm);
        }
        /// <summary>
        /// Method is uesd to display all the vetting inspections that are about to expire in the next 30 days.   
        /// </summary>
        /// <param name="UserId">Login user ID</param>
        /// <returns></returns>
        public static DataTable Dash_Get_Vetting_Exp_In_Next_30Days(int UserId)
        {
            SqlParameter[] sqlparam = new SqlParameter[] 
            {
                 new SqlParameter("@User_Id",UserId)
            };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "Dash_Get_Vetting_Exp_In_Next_30Days",sqlparam).Tables[0];

        }
        /// <summary>
        /// Method is used to display all the vetting inspections that were expired or failed
        /// </summary>
        /// <param name="UserId">Login user ID</param>
        /// <returns></returns>
        public static DataTable Dash_Get_Exp_Failed_Vetting_Insp(int UserId)
        {
            SqlParameter[] sqlparam = new SqlParameter[] 
            {
                 new SqlParameter("@User_Id",UserId)
            };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "Dash_Get_Exp_Failed_Vetting_Insp", sqlparam).Tables[0];

        }

        #region for Summary Snippet of PO and Invoice

        public static DataSet Get_POAndInvoice_SummarySnippet_DL()
        {
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "DASH_Get_Summary_POInvoice");
        }

        /// <summary>
        /// Method is used to display all pending PO Approval
        /// </summary>
        /// <param name="UserId">Login user ID</param>
        /// <returns></returns>

        public static DataTable Get_Pending_POApprovals_Snippet_DL(int? UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@UserID",UserID)
                                        };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "DASH_Get_PendingPOApproval", sqlprm).Tables[0];

        }
        /// <summary>
        /// Method is used to display all pending Invoice Verifications
        /// </summary>
        /// <param name="UserId">Login user ID</param>
        /// <returns></returns>

        public static DataTable Get_Pending_InvoiceVerification_Snippet_DL(int? UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@UserID",UserID)
                                        };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "DASH_GET_PendingInvoiceverification", sqlprm).Tables[0];

        }

        /// <summary>
        /// Method is used to display all pending Invoice Approvals.
        /// </summary>
        /// <param name="UserId">Login user ID</param>
        /// <returns></returns>

        public static DataTable Get_Pending_InvoiceApprovals_Snippet_DL(int? UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@UserID",UserID)
                                        };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "DASH_GET_PendingInvoiceApprovals", sqlprm).Tables[0];

        }

        #endregion
    }
}
