using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace SMS.Data.Operation
{
    public class DAL_OPS_TaskPlanner
    {
        private static string connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
        public DAL_OPS_TaskPlanner(string ConnectionString)
        {
            connection = ConnectionString;
        }
        public DAL_OPS_TaskPlanner()
        {
            connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
        }

        public static DataTable Get_TaskList_DL(int? FleetCode, int? Vessel_ID, int? CATEGORY_PRIMARY, int? PIC, int? Status, DateTime? DateRaiseFrom, DateTime? DateRaiseTo, DateTime? ExpCompFrom, DateTime? ExpCompTo, int UserID, string Description, int? PAGE_SIZE, int? PAGE_INDEX, ref int SelectRecordCount, string SortColumn, int SortDirection, int isPrivate, int? NoOFDays)
        {
            SqlParameter[] sqlprm = new SqlParameter[] { 
                        new SqlParameter("@FleetCode", FleetCode),
                        new SqlParameter("@Vessel_ID", Vessel_ID),
                        new SqlParameter("@CATEGORY_PRIMARY", CATEGORY_PRIMARY),
                        new SqlParameter("@PIC", PIC),
                        new SqlParameter("@Status", Status) ,
                        new SqlParameter("@DateRaiseFrom", DateRaiseFrom) ,
                        new SqlParameter("@DateRaiseTo", DateRaiseTo),
                        new SqlParameter("@ExpCompFrom", ExpCompFrom) ,
                        new SqlParameter("@ExpCompTo", ExpCompTo),
                        new SqlParameter("@UserID", UserID),
                        new SqlParameter("@Description", Description),
                        new SqlParameter("@NoOfDays",NoOFDays),
                        new SqlParameter("@PAGE_SIZE", PAGE_SIZE),
                        new SqlParameter("@PAGE_INDEX",PAGE_INDEX),
                        new SqlParameter("@SortColumn",SortColumn),
                        new SqlParameter("@SortDirection",SortDirection),
                        new SqlParameter("@Private",isPrivate),
                        new SqlParameter("@SelectRecordCount",SelectRecordCount)
                    };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.InputOutput;

            DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "OPS_SP_Get_TaskList", sqlprm);
            SelectRecordCount = Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);

            return ds.Tables[0];
        }

        public static DataSet Get_TaskDetails_DL(int WORKLIST_ID, int VESSEL_ID, int OFFICE_ID, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
              { 
                   new SqlParameter("@OFFICE_ID", OFFICE_ID),
                   new SqlParameter("@WORKLIST_ID", WORKLIST_ID),
                   new SqlParameter("@VESSEL_ID", VESSEL_ID),
                   new SqlParameter("@UserID", UserID)
              };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "OPS_SP_Get_TaskDetails", sqlprm);
        }

        public static DataTable Create_New_Task_DL(int VESSEL_ID, string JOB_DESCRIPTION, DateTime? DATE_RAISED, DateTime? DATE_ESTMTD_CMPLTN, DateTime? DATE_COMPLETED, int CATEGORY_PRIMARY, int PIC, string PORT_CALL_ID, int CREATED_BY, int isPrivate)
        {
            SqlParameter[] sqlprm = new SqlParameter[] { 
                        new SqlParameter("@VESSEL_ID", VESSEL_ID),
                        new SqlParameter("@JOB_DESCRIPTION", JOB_DESCRIPTION),
                        new SqlParameter("@DATE_RAISED", DATE_RAISED),
                        new SqlParameter("@DATE_ESTMTD_CMPLTN", DATE_ESTMTD_CMPLTN),
                        new SqlParameter("@DATE_COMPLETED", DATE_COMPLETED) ,
                        new SqlParameter("@CATEGORY_PRIMARY", CATEGORY_PRIMARY) ,
                        new SqlParameter("@PIC", PIC),
                        new SqlParameter("@PORT_CALL_ID", PORT_CALL_ID) ,
                        new SqlParameter("@CREATED_BY", CREATED_BY),
                        new SqlParameter("@isPrivate", isPrivate)
                    };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "OPS_SP_Insert_Task", sqlprm).Tables[0];
        }

        public static int UPDATE_Task_DL(int WORKLIST_ID, int VESSEL_ID, int OFFICE_ID, string JOB_DESCRIPTION, DateTime? DATE_RAISED, DateTime? DATE_ESTMTD_CMPLTN, DateTime? DATE_COMPLETED, int CATEGORY_PRIMARY, int PIC, string PORT_CALL_ID, int MODIFIED_BY, int IsVerify, int isPrivate)
        {
            SqlParameter[] sqlprm = new SqlParameter[] { 
                        new SqlParameter("@WORKLIST_ID", WORKLIST_ID),
                        new SqlParameter("@VESSEL_ID", VESSEL_ID),
                        new SqlParameter("@OFFICE_ID", OFFICE_ID),
                        new SqlParameter("@JOB_DESCRIPTION", JOB_DESCRIPTION),
                        new SqlParameter("@DATE_RAISED", DATE_RAISED),
                        new SqlParameter("@DATE_ESTMTD_CMPLTN", DATE_ESTMTD_CMPLTN),
                        new SqlParameter("@DATE_COMPLETED", DATE_COMPLETED) ,
                        new SqlParameter("@CATEGORY_PRIMARY", CATEGORY_PRIMARY) ,
                        new SqlParameter("@PIC", PIC),
                        new SqlParameter("@PORT_CALL_ID", PORT_CALL_ID) ,
                        new SqlParameter("@MODIFIED_BY", MODIFIED_BY),
                        new SqlParameter("@IsVerify",IsVerify),
                        new SqlParameter("@isPrivate",isPrivate),                        
                        new SqlParameter("return",SqlDbType.Int)
                    };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "OPS_SP_Update_Task", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        public static int UPDATE_Task_PortCall_DL(int Worklist_ID, int Vessel_ID, int Office_ID, string Port_Call_ID, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[] { 
                new SqlParameter("@Worklist_ID", Worklist_ID),
                new SqlParameter("@Vessel_ID", Vessel_ID),
                new SqlParameter("@Office_ID", Office_ID),
                new SqlParameter("@Port_Call_ID", Port_Call_ID),
                new SqlParameter("@UserID", UserID),
                new SqlParameter("return",SqlDbType.Int)
            };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "OPS_SP_UPDATE_Task_PortCall", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        public static int UPDATE_Task_Status_DL(int Worklist_ID, int Vessel_ID, int Office_ID, int Status, DateTime? Completion_Date ,string CompletionRemark, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[] { 
                new SqlParameter("@Worklist_ID", Worklist_ID),
                new SqlParameter("@Vessel_ID", Vessel_ID),
                new SqlParameter("@Office_ID", Office_ID),
                new SqlParameter("@Status", Status),
                new SqlParameter("@Completion_Date", Completion_Date),
                new SqlParameter("@CompletionRemark", CompletionRemark),
                new SqlParameter("@UserID", UserID),
                new SqlParameter("return",SqlDbType.Int)
            };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "OPS_SP_UPDATE_Task_Status", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        public static DataTable Get_FollowupList_DL(int WORKLIST_ID, int VESSEL_ID, int OFFICE_ID, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
              { 
                new SqlParameter("@WORKLIST_ID",WORKLIST_ID) ,
                new SqlParameter("@VESSEL_ID",VESSEL_ID),
                new SqlParameter("@OFFICE_ID",OFFICE_ID),   
                new SqlParameter("@UserID", UserID)
                              
            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "OPS_SP_Get_TaskFollowupList", sqlprm).Tables[0];
        }

        public static DataTable Get_PrimaryCat_List_DL()
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "OPS_SP_Get_PrimaryCat_List").Tables[0];
        }

        public static DataTable Get_Vessel_PortCallList_DL(int VESSEL_ID,  DateTime? FromDate, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
              { 
                   new SqlParameter("@VESSEL_ID", VESSEL_ID),
                   new SqlParameter("@FromDate", FromDate),
                   new SqlParameter("@UserID", UserID)
              };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "OPS_SP_Get_Vessel_PortCallList", sqlprm).Tables[0];
        }

        public static int Insert_Followup(int WORKLIST_ID, int VESSEL_ID, int OFFICE_ID, string FOLLOWUP, int CREATED_BY)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
              { 
                new SqlParameter("@OFFICE_ID",OFFICE_ID),
                new SqlParameter("@WORKLIST_ID",WORKLIST_ID),
                new SqlParameter("@VESSEL_ID",VESSEL_ID),
                new SqlParameter("@FOLLOWUP",FOLLOWUP), 
                new SqlParameter("@CREATED_BY",CREATED_BY),
                new SqlParameter("return",SqlDbType.Int)
            };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "OPS_SP_Insert_FollowUp", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        public static int ReActivate_Task_DL(int Worklist_ID, int VESSEL_ID, int OFFICE_ID, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
              { 
                new SqlParameter("@OFFICE_ID",OFFICE_ID),
                new SqlParameter("@WORKLIST_ID",Worklist_ID),
                new SqlParameter("@VESSEL_ID",VESSEL_ID),
          
                new SqlParameter("@MODIFIED_BY",UserID),
                new SqlParameter("return",SqlDbType.Int)
            };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "OPS_SP_ReActivate_Task", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        public static DataTable Get_MyOperationWorklist_DL(int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
              { 
                new SqlParameter("@UserID",UserID)
            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "OPS_SP_Get_MyTaskList", sqlprm).Tables[0];
        }

        public static DataTable Get_OpsWorklistDueIn7Days_DL(int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
              { 
                new SqlParameter("@UserID",UserID)
            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "OPS_SP_Get_WorklistDueIn7Days", sqlprm).Tables[0];
        }

        public static DataTable Get_OpsWorklistOverdue_DL(int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
              { 
                new SqlParameter("@UserID",UserID)
            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "OPS_SP_Get_WorklistOverdue", sqlprm).Tables[0];
        }
    }
}
