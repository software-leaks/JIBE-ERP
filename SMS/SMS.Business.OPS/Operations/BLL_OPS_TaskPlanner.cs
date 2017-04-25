using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SMS.Data.Operation;

namespace SMS.Business.Operation
{
    public class BLL_OPS_TaskPlanner
    {
        static IFormatProvider iFormatProvider = new System.Globalization.CultureInfo("en-GB", true);

        public static DataTable Get_TaskList(int? FleetCode, int? Vessel_ID, int? CATEGORY_PRIMARY, int? PIC, int? Status, DateTime? DateFrom, DateTime? DateTo, DateTime? ExpCompFrom, DateTime? ExpCompTo, int UserID, string Description, int? PAGE_SIZE, int? PAGE_INDEX, ref int SelectRecordCount, string SortColumn, int SortDirection,int isPrivate, int? NoOFDays = null)
        {
            return DAL_OPS_TaskPlanner.Get_TaskList_DL(FleetCode, Vessel_ID, CATEGORY_PRIMARY, PIC, Status, DateFrom, DateTo, ExpCompFrom, ExpCompTo, UserID, Description, PAGE_SIZE, PAGE_INDEX, ref SelectRecordCount, SortColumn, SortDirection, isPrivate, NoOFDays);
        }
        
        public static DataSet Get_TaskDetails(int WORKLIST_ID, int VESSEL_ID, int OFFICE_ID, int UserID)
        {
            return DAL_OPS_TaskPlanner.Get_TaskDetails_DL(WORKLIST_ID, VESSEL_ID, OFFICE_ID, UserID);
        }

        public static DataTable Create_New_Task(int VESSEL_ID, string JOB_DESCRIPTION, DateTime? DATE_RAISED, DateTime? DATE_ESTMTD_CMPLTN, DateTime? DATE_COMPLETED, int CATEGORY_PRIMARY, int PIC, string PORT_CALL_ID, int CREATED_BY, int isPrivate)
        {
            return DAL_OPS_TaskPlanner.Create_New_Task_DL(VESSEL_ID, JOB_DESCRIPTION, DATE_RAISED, DATE_ESTMTD_CMPLTN, DATE_COMPLETED, CATEGORY_PRIMARY, PIC, PORT_CALL_ID, CREATED_BY, isPrivate);
        }

        public static int UPDATE_Task(int WORKLIST_ID, int VESSEL_ID, int OFFICE_ID, string JOB_DESCRIPTION, DateTime? DATE_RAISED, DateTime? DATE_ESTMTD_CMPLTN, DateTime? DATE_COMPLETED, int CATEGORY_PRIMARY, int PIC, string PORT_CALL_ID, int MODIFIED_BY, int isPrivate, int IsVerify = 0)
        {
            return DAL_OPS_TaskPlanner.UPDATE_Task_DL(WORKLIST_ID, VESSEL_ID, OFFICE_ID, JOB_DESCRIPTION, DATE_RAISED, DATE_ESTMTD_CMPLTN, DATE_COMPLETED, CATEGORY_PRIMARY, PIC, PORT_CALL_ID, MODIFIED_BY, IsVerify, isPrivate);
        }
        
        public static int UPDATE_Task_PortCall(int Worklist_ID, int Vessel_ID, int Office_ID, string Port_Call_ID, int UserID)
        {
            return DAL_OPS_TaskPlanner.UPDATE_Task_PortCall_DL(Worklist_ID, Vessel_ID, Office_ID, Port_Call_ID, UserID);
        }
        
        public static int UPDATE_Task_Status(int Worklist_ID, int Vessel_ID, int Office_ID, int Status, DateTime? Completion_Date, string CompletionRemark, int UserID)
        {
            return DAL_OPS_TaskPlanner.UPDATE_Task_Status_DL(Worklist_ID, Vessel_ID, Office_ID, Status, Completion_Date,CompletionRemark, UserID);
        }

        public static DataTable Get_FollowupList(int WORKLIST_ID, int VESSEL_ID, int OFFICE_ID, int UserID)
        {
            return DAL_OPS_TaskPlanner.Get_FollowupList_DL(WORKLIST_ID, VESSEL_ID, OFFICE_ID, UserID);
        }

        public static DataTable Get_PrimaryCat_List()
        {
            return DAL_OPS_TaskPlanner.Get_PrimaryCat_List_DL();
        }

        public static DataTable Get_Vessel_PortCallList(int VESSEL_ID, DateTime? FromDate, int UserID)
        {
            return DAL_OPS_TaskPlanner.Get_Vessel_PortCallList_DL(VESSEL_ID, FromDate, UserID);
        }

        public static int Insert_Followup(int Worklist_ID, int VESSEL_ID, int OFFICE_ID, string FOLLOWUP, int CREATED_BY)
        {
            return DAL_OPS_TaskPlanner.Insert_Followup(Worklist_ID, VESSEL_ID, OFFICE_ID, FOLLOWUP, CREATED_BY);
        }

        public static int ReActivate_Task(int Worklist_ID, int VESSEL_ID, int OFFICE_ID, int UserID)
        {
            return DAL_OPS_TaskPlanner.ReActivate_Task_DL(Worklist_ID, VESSEL_ID, OFFICE_ID, UserID);
        }

    }
}
