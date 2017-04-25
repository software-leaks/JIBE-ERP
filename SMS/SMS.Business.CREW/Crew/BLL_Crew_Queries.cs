using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SMS.Data.Crew;

namespace SMS.Business.Crew
{
    public class BLL_Crew_Queries
    {
        static IFormatProvider iFormatProvider = new System.Globalization.CultureInfo("en-GB", true);

        public static DataTable Get_CrewQueries(int FleetCode, int Vessel_ID, int CrewID, int Query_Type, int Status, string SearchText, int UserID, int PAGE_SIZE, int PAGE_INDEX, ref int SelectRecordCount, string SortBy, int? SortDirection)
        {
            return DAL_Crew_Queries.Get_CrewQueries_DL(FleetCode, Vessel_ID, CrewID, Query_Type, Status, SearchText, UserID, PAGE_SIZE, PAGE_INDEX, ref SelectRecordCount, SortBy, SortDirection);
        }
        public static DataSet Get_CrewQuery_Details(int QueryID, int Vessel_ID, int UserID)
        {
            return DAL_Crew_Queries.Get_CrewQuery_Details_DL(QueryID, Vessel_ID, UserID);
        }
        
        public static DataTable Get_CrewQuery_Types(int UserID)
        {
            return DAL_Crew_Queries.Get_CrewQuery_Types_DL(UserID);
        }
        public static int INSERT_CrewQuery_Type(string CrewQuery_Type, int Created_By)
        {
            return DAL_Crew_Queries.INSERT_CrewQuery_Type_DL(CrewQuery_Type, Created_By);
        }
        public static int UPDATE_CrewQuery_Type(int CrewQuery_Type_ID, string CrewQuery_Type, int Updated_By)
        {
            return DAL_Crew_Queries.UPDATE_CrewQuery_Type_DL(CrewQuery_Type_ID, CrewQuery_Type, Updated_By);
        }
        public static int DELETE_CrewQuery_Type(int CrewQuery_Type_ID, int Deleted_By)
        {
            return DAL_Crew_Queries.DELETE_CrewQuery_Type_DL(CrewQuery_Type_ID, Deleted_By);
        }
        
        public static DataTable Get_CrewQuery_Approvers(int UserID)
        {
            return DAL_Crew_Queries.Get_CrewQuery_Approvers_DL(UserID);
        }
        public static int INSERT_CrewQuery_Approver(int ApproverID, int Created_By)
        {
            return DAL_Crew_Queries.INSERT_CrewQuery_Approver_DL(ApproverID, Created_By);
        }
        public static int DELETE_CrewQuery_Approver(int ApproverID, int Deleted_By)
        {
            return DAL_Crew_Queries.DELETE_CrewQuery_Approver_DL(ApproverID, Deleted_By);
        }
        
        public static DataTable Get_Claim_Attachments(int CrewQuery_ID, int Vessel_ID, int ClaimID, int UserID)
        {
            return DAL_Crew_Queries.Get_Claim_Attachments_DL(CrewQuery_ID, Vessel_ID, ClaimID, UserID);
        }
        public static DataTable Get_CrewQuery_Attachments(int CrewQuery_ID, int Vessel_ID, int UserID)
        {
            return DAL_Crew_Queries.Get_CrewQuery_Attachments_DL(CrewQuery_ID, Vessel_ID,  UserID);
        }
        public static DataTable Get_CrewQuery_Followups(int CrewQuery_ID, int Vessel_ID, int UserID)
        {
            return DAL_Crew_Queries.Get_CrewQuery_Followups_DL(CrewQuery_ID, Vessel_ID, UserID);
        }
        //public static int INSERT_CrewQuery_Attachment(string AttachmentName, string AttachmentPath, int AttachmentSize, int CrewQuery_ID, int Vessel_ID, int Created_By)
        //{
        //    return DAL_Crew_Queries.INSERT_CrewQuery_Attachment_DL(AttachmentName, AttachmentPath, AttachmentSize, CrewQuery_ID, Vessel_ID, Created_By);
        //}
        //public static int DELETE_CrewQuery_Attachment(int CrewQuery_ID, int Vessel_ID, int Attachment_ID, int Deleted_By)
        //{
        //    return DAL_Crew_Queries.DELETE_CrewQuery_Attachment_DL(CrewQuery_ID, Vessel_ID, Attachment_ID, Deleted_By);
        //}

        public static int INSERT_CrewQuery_FollowUp(int CrewQuery_ID, int Vessel_ID, string FollowUpText, int Sent_To_Ship, int Created_By)
        {
            return DAL_Crew_Queries.INSERT_CrewQuery_FollowUp_DL(CrewQuery_ID, Vessel_ID, FollowUpText, Sent_To_Ship, Created_By);
        }
        public static int DELETE_CrewQuery_FollowUp(int CrewQuery_ID, int Vessel_ID, int FollowUp_ID, int Deleted_By)
        {
            return DAL_Crew_Queries.DELETE_CrewQuery_FollowUp_DL(CrewQuery_ID, Vessel_ID, FollowUp_ID, Deleted_By);
        }

        public static int Approve_CrewQuery_Claims(int CrewQuery_ID, int Vessel_ID, DataTable dtClaims, int ApprovedBy)
        {
            return DAL_Crew_Queries.Approve_CrewQuery_Claims_DL(CrewQuery_ID, Vessel_ID, dtClaims, ApprovedBy);
        }

        public static int Reject_CrewQuery_Claims(int CrewQuery_ID, int Vessel_ID, DataTable dtClaims, int ApprovedBy)
        {
            return DAL_Crew_Queries.Reject_CrewQuery_Claims_DL(CrewQuery_ID, Vessel_ID, dtClaims, ApprovedBy);
        }
    }
}
