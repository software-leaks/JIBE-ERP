using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace SMS.Data.Infrastructure
{
    public class DAL_Infra_DashBoard
    {
        static string _internalConnection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;

        public static DataSet Get_Provision_Last_Supplied_DL()
        {
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "DASH_GET_PROV_LAST_SUPPLIED");
        }

        public static DataSet Get_Rreqsn_Count_DL(string FormType, int Fleet, string User_ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Form_Type",FormType),
                                            new SqlParameter("@Fleet",Fleet),
                                            new SqlParameter("@UserID",User_ID)
                                        };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "DASH_GET_REQSN_COUNT", sqlprm);
        }

        public static DataTable Get_Pending_Reqsn_DL(int User_ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                           new SqlParameter("@UserID",User_ID)
                                        };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "DASH_GET_PENDING_REQSN", sqlprm).Tables[0];
        }

        public static DataTable Get_Supplier_Evaluation_Search(int? UserID)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@UserID",UserID),
            };

            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "DASH_GET_ASL_PENDING_EVALUATION", obj);
            return ds.Tables[0];
        }
        public static DataSet Get_Pending_Invoice_Approval(int? UserID)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@UserID",UserID),
            };

            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "DASH_Get_Invoice_Pending_Approval", obj);
            return ds;
        }
        public static DataTable Get_Opex_Fleet_Details(int? UserID)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@UserID",UserID),
            };

            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "DASH_Get_Opex_Fleet_Details", obj);
            return ds.Tables[0];
        }

        public static DataSet Get_Opex_Vessel_Report(int? UserID, string Fleet_ID)
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
        public static int UPD_User_Menu_Favourite_DL(int UserID, DataTable Menu)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                           new SqlParameter("@USER_ID",UserID),
                                           new SqlParameter("@MENUCODE_FAVOURITE",Menu)
                                        };
            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "DASH_UPD_USER_MENU_FAVOURITE", sqlprm);
        }

        public static DataTable Get_User_Menu_Favourite_DL(int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                           new SqlParameter("@USER_ID",UserID),
                                           
                                        };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "DASH_GET_USER_MENU_FAVOURITE", sqlprm).Tables[0];

        }

        public static DataTable Get_User_Menu_List_DL(int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                           new SqlParameter("@USER_ID",UserID),
                                           
                                        };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "DASH_GET_USER_MENU_LIST", sqlprm).Tables[0];

        }

        public static DataTable Get_Reqsn_Processing_Time_DL(string FormType, int Fleet, int User_ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Form_Type",FormType),
                                            new SqlParameter("@Fleet",Fleet),
                                            new SqlParameter("@UserID",User_ID)
                                        };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "DASH_GET_REQSN_PROCESSING_TIME", sqlprm).Tables[0];

        }

        public static DataTable Get_Evaluation_Schedules_DL(int User_ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@UserID",User_ID)
                                        };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "CRW_EVA_SP_Get_EvaluationSchedules", sqlprm).Tables[0];

        }

        public static DataSet Get_CrewPerformance_DL(int User_ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@UserID",User_ID)
                                        };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "DASH_GET_CREW_BY_EVALUATION", sqlprm);

        }
        public static DataSet Get_CrewEvaluationDue_DL(int User_ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@UserID",User_ID)
                                        };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "CRW_EVA_SP_Get_DueEvaluation", sqlprm);

        }
        public static DataTable Get_Evaluation_60Percent_DL(int User_ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@UserID",User_ID)
                                        };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "CRW_EVA_SP_Get_Evaluation_60Percent", sqlprm).Tables[0];

        }
        public static DataTable Get_Snippet_Access_DL(int User_ID, int? DepID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@User_ID",User_ID),
                                            new SqlParameter("@DepID",DepID)
                                        };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "DASH_GET_Snippet_Access", sqlprm).Tables[0];

        }
        public static DataTable Get_Snippet_Access_OnDashboard_DL(int User_ID, int? DepID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@User_ID",User_ID),
                                            new SqlParameter("@DepID",DepID)
                                        };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "DASH_GET_Snippet_Access_OnDashboard", sqlprm).Tables[0];

        }
        public static int UPD_Snippet_Access_DL(int UserID, DataTable Snippet, int CreatedBy)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                           new SqlParameter("@USERID",UserID),
                                           new SqlParameter("@tblSnippet_Access",Snippet),
                                           new SqlParameter("@CREATED_BY",CreatedBy)

                                        };
            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "DASH_UPD_Snippet_Access", sqlprm);
        }

        public static DataTable Get_Pending_NCR_DL(int? Assignor, int? DepartmentID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                           new SqlParameter("@ASSIGNOR",Assignor),
                                           new SqlParameter("@DEPT_OFFICE",DepartmentID)
                                           
                                        };

            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "DASH_GET_Pending_NCR", sqlprm).Tables[0];
        }
        public static DataTable Get_Pending_NCR_ALL_Dept_DL(int? Assignor)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                           new SqlParameter("@ASSIGNOR",Assignor)                                           
                                        };

            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "DASH_GET_Pending_NCR_ALL_DEPT", sqlprm).Tables[0];
        }

        public static DataTable Get_Pending_Travel_PO_DL(int User_ID)
        {
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "DASH_Get_Pending_Travel_PO", new SqlParameter("@User_ID", User_ID)).Tables[0];
        }

        public static DataTable Get_Pending_Logistic_PO_DL(int User_ID)
        {
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "DASH_Get_Pending_Logistic_PO", new SqlParameter("@User_ID", User_ID)).Tables[0];
        }

        public static DataTable Get_Portcalls_Vessel_DL(int UserID)
        {
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "DASH_Get_PortCalls_Vessel", new SqlParameter("@UserID", UserID)).Tables[0];
        }

        public static DataTable Get_Portcalls_Month_DL(int UserID)
        {
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "DASH_Get_PortCalls_Month", new SqlParameter("@UserID", UserID)).Tables[0];
        }

        public static DataTable Get_Pending_WorkList_DL(int UserID)
        {
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "DASH_Get_Pending_WorkList", new SqlParameter("@UserID", UserID)).Tables[0];
        }

        public static DataTable Get_Pending_TotalWorkListToVerify_DL()
        {
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "DASH_Get_Pending_TotalWorkListToVerify").Tables[0];
        }
        
        public static DataTable getPendingInterviewList_DL(int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@UserID",UserID)
                                        };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "SP_CRW_Get_PendingInterviewList", sqlprm).Tables[0];
        }

        public static DataTable getPendingInterviewList_By_UserID_DL(int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@UserID",UserID)
                                        };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "SP_CRW_Get_PendingInterviewList_By_UserID", sqlprm).Tables[0];
        }

        public static DataTable Get_WorkList_DueIn_7Days_DL()
        {
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "DASH_Get_WorkList_DueIn_7Days").Tables[0];
        }

        public static DataTable Get_Pending_CTM_Approval_DL(int UserID)
        {
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "DASH_Get_Pending_CTM_Approval", new SqlParameter("@User_ID", UserID)).Tables[0];
        }

        public static DataTable Get_CTM_Confirmation_Not_Received_DL(int UserID)
        {
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "ACC_GET_CTM_Confirmation_Not_Received", new SqlParameter("@User_ID", UserID)).Tables[0];
        }

        public static DataTable Search_DashBoardSnippet(string searchtext, int? Deptid, int? AutoRefresh
           , string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@SearchText", searchtext),
                   new System.Data.SqlClient.SqlParameter("@DeptID", Deptid),
                   new System.Data.SqlClient.SqlParameter("@AutoRefresh", AutoRefresh),

                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                    
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "SP_INF_Dash_Board_Snippet_Search", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds.Tables[0];
        }


        public static DataTable List_DashBoardSnippet(int? ID)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "SP_INF_Get_Dash_Board_Snippet_List", new SqlParameter("@ID", ID)).Tables[0];

        }

        public static int Insert_DashBoardSnippet(string Snippet_ID, string Snippet_Name, string Snippet_Function_Name, int? Department_ID,string Department_Color, int? Auto_Refresh, int? CreatedBy)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                           new SqlParameter("@Snippet_ID",Snippet_ID),
                                           new SqlParameter("@Snippet_Name",Snippet_Name),
                                           new SqlParameter("@Snippet_Function_Name",Snippet_Function_Name),
                                           new SqlParameter("@Department_ID",Department_ID),
                                           new SqlParameter("@Department_Color",Department_Color),
                                           new SqlParameter("@Auto_Refresh",Auto_Refresh),
                                           new SqlParameter("@Created_By",CreatedBy),
                                        };
            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "SP_INF_Insert_Dash_Board_Snippet", sqlprm);
        }

        public static int Edit_DashBoardSnippet(int ID, string Snippet_ID, string Snippet_Name, string Snippet_Function_Name, int? Department_ID, string Department_Color, int? Auto_Refresh, int? Modified_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ID",ID),
                                            new SqlParameter("@Snippet_ID",Snippet_ID),
                                            new SqlParameter("@Snippet_Name",Snippet_Name),
                                            new SqlParameter("@Snippet_Function_Name",Snippet_Function_Name),
                                            new SqlParameter("@Department_ID",Department_ID),
                                            new SqlParameter("@Department_Color",Department_Color),
                                            new SqlParameter("@Auto_Refresh",Auto_Refresh),
                                            new SqlParameter("@Modified_By",Modified_By),
                                        };
            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "SP_INF_Update_Dash_Board_Snippet", sqlprm);
        }

        public static int Delete_DashBoardSnippet(int ID, int DeletedBy)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                           new SqlParameter("@ID",ID),
                                           new SqlParameter("@Deleted_By",DeletedBy),
                                        };
            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "SP_INF_Del_Dash_Board_Snippet", sqlprm);
        }


        public static DataTable Get_Surv_DueinNext30Days_DL(int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            { 
                new SqlParameter("@UserID", UserID)
            };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "DASH_Get_Surv_DueinNext30Days", sqlprm).Tables[0];
        }
        public static DataTable Get_Surv_DueinNext7DaysAndOverdue_DL(int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            { 
                new SqlParameter("@UserID", UserID)
            };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "DASH_Get_Surv_DueinNext7DaysAndOverdue", sqlprm).Tables[0];
        }
        public static DataTable Get_Surv_NA_PendingVerification_DL(int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            { 
                new SqlParameter("@UserID", UserID)
            };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "DASH_Get_Surv_NA_PendingVerification", sqlprm).Tables[0];
        }
        public static DataTable Get_Surv_PendingVerification_DL(int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            { 
                new SqlParameter("@UserID", UserID)
            };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "DASH_Get_Surv_PendingVerification", sqlprm).Tables[0];
        }
        public static DataTable Get_Surv_ExpDateBydayCount_DL(int UserID, string ExpairyFromDaysCount, string ExpairyToDaysCount) //Added By Vasu, New Feature
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            { 
                new SqlParameter("@UserID", UserID),
                new SqlParameter("@ExpairyFromDaysCount", Convert.ToInt32(ExpairyFromDaysCount)),
                  new SqlParameter("@ExpairyToDaysCount",Convert.ToInt32(ExpairyToDaysCount))
               
            };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "DASH_Get_Surv_ExpDateBydayCount", sqlprm).Tables[0];
        }

        public static DataTable Get_PMS_Overdue_Job_Count_DL(int? User_ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@UserID",User_ID)
                                        };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "DASH_GET_PMS_OVERDUE_JOB_COUNT", sqlprm).Tables[0];
        }

        public static DataTable Get_Cylinder_Oil_Consumption(int UserID)
        {
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "DASH_Get_Cylinder_Oil_Consumption", new SqlParameter("@UserID", UserID)).Tables[0];
        }

        public static string Get_Events_Done(int User_ID)
        {
            return Convert.ToString(SqlHelper.ExecuteScalar(_internalConnection, CommandType.StoredProcedure, "DASH_Get_Events_Done", new SqlParameter("@User_ID", User_ID)));
        }
        public static DataSet Get_Decklog_Anomalies(int User_ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@UserID",User_ID)
                                        };

            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "DASH_GET_DECKLOG_ANOMALIES_SNAPSHOT");
        }

        public static DataSet Get_Enginelog_Anomalies(int User_ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@UserID",User_ID)
                                        };

            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "DASH_GET_ENGINELOG_ANOMALIES_SNAPSHOT");
        }
        public static DataTable Get_OverDue_Inspection(int User_ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@UserID",User_ID)
                                        };

            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "DASH_GET_OverDueInspection", sqlprm).Tables[0];
        }
        public static DataTable Get_DueInMonth_Inspection(int User_ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@UserID",User_ID)
                                        };

            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "DASH_GET_DueInspection", sqlprm).Tables[0];




        }
        public static DataTable Get_Completed_Inspection_DL(int User_ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@UserID",User_ID)
                                        };

            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "DASH_GET_CompletedInspection", sqlprm).Tables[0];

        }
        public static DataTable Get_InvItems_BelowTreshold_DL(int User_ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@UserID",User_ID)
                                        };

            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "DASH_GET_BelowTreshold_InventoryItems", sqlprm).Tables[0];

        }
        public static DataSet GetPendingCardApprovalList_DL(int User_ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@UserID",User_ID)
                                        };

            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "DASH_Get_PendingCardApprovalList", sqlprm);

        }
        public static DataTable Get_CrewEvaluation_Feedback(int User_ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@UserID",User_ID)
                                        };

            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "DASH_Get_CrewEvaluation_Feedback", sqlprm).Tables[0];
        }

        public static DataSet Get_CrewPerformanceVerification_DL(int User_ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@UserID",User_ID)
                                        };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "DASH_GET_CREW_BY_EVALUATION_PRFORMANCE", sqlprm);

        }

        public static DataTable GetPendingBriefingList(int USer_ID)
        {
            SqlParameter[] sqlparam = new SqlParameter[] 
            {
                 new SqlParameter("@UserID",USer_ID)
            };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "DASH_GET_Pending_CrewBriefing", sqlparam).Tables[0];

        }
        public static DataTable Get_CrewOnboardListRankWise(int USer_ID)
        {
            SqlParameter[] sqlparam = new SqlParameter[] 
            {
                 new SqlParameter("@UserID",USer_ID)
            };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "DASH_Get_CrewOnboardListRankWise", sqlparam).Tables[0];

        }
        public static DataTable Get_CrewSeniorityReward(int USer_ID)
        {
            SqlParameter[] sqlparam = new SqlParameter[] 
            {
                 new SqlParameter("@UserID",USer_ID)
            };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "DASH_Get_CrewSeniorityRewardList", sqlparam).Tables[0];

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


        public static DataTable Get_VoyageSnippetData(int User_ID)
        {
            SqlParameter[] sqlparam = new SqlParameter[] 
            {
                 new SqlParameter("@User_Id",User_ID)
            };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "Dash_Get_VoyageSnippetData", sqlparam).Tables[0];

        }


        public static DataTable Get_CPSnippetData(int USer_ID)
        {
            SqlParameter[] sqlparam = new SqlParameter[] 
            {
                 new SqlParameter("@User_Id",USer_ID)
            };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "Dash_Get_CP_SnippetData", sqlparam).Tables[0];

        }
        public static DataSet GET_PerformacneManager(int User_ID, int Days)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@UserID",User_ID),
                                             new SqlParameter("@Days",Days)
                                        };

            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "DASH_GET_PerformacneManager", sqlprm);

        }
        public static DataTable Get_WorkList_Incident_180days()
        {
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "DASH_Get_WorkList_Incident_180days").Tables[0];
        }
        public static DataTable Get_WorkList_NearMiss_180days()
        {
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "DASH_Get_WorkList_NearMiss_180days").Tables[0];
        }

        public static DataTable Get_Crw_RefusedToSignEval() 
        {
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "sp_Get_Crw_RefusedToSignEval").Tables[0];

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
