using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SMS.Data.Crew;

namespace SMS.Business.Crew
{
    public class BLL_Crew_MedHistory
    {
        static IFormatProvider iFormatProvider = new System.Globalization.CultureInfo("en-GB", true);

        public static DataTable  INSERT_Crew_MedHistory(int VoyageID, int CrewID, int Case_Type, int Status, string Case_Detail, string Case_Date, int Created_By, string Case_To_Date)
        {
            if (Case_To_Date == null || Case_To_Date == "")
                Case_To_Date = "1900/01/01";

            DateTime dtCase_Date = DateTime.Parse(Case_Date, iFormatProvider);
            DateTime dtCase_To_Date = DateTime.Parse(Case_To_Date, iFormatProvider);
            return DAL_Crew_MedHistory.INSERT_Crew_MedHistory_DL(VoyageID, CrewID, Case_Type, Status, Case_Detail, dtCase_Date, Created_By, dtCase_To_Date);
        }
        public static int UPDATE_Crew_MedHistory(int Case_ID, int VoyageID, int CrewID, int Case_Type, int Status, string Case_Detail, string Case_Date, int Created_By, string Case_To_Date, int Office_ID)
        {
            if (Case_To_Date == null || Case_To_Date == "")
                Case_To_Date = "1900/01/01";

            DateTime dtCase_Date = DateTime.Parse(Case_Date, iFormatProvider);
            DateTime dtCase_To_Date = DateTime.Parse(Case_To_Date, iFormatProvider);
            return DAL_Crew_MedHistory.UPDATE_Crew_MedHistory_DL(Case_ID, VoyageID, CrewID, Case_Type, Status, Case_Detail, dtCase_Date, Created_By, dtCase_To_Date, Office_ID);
        }
        public static DataTable Get_Crew_MedHistory(int FleetCode, int Vessel_ID, int CrewID, int Case_Type, int Status, string SearchText, int UserID, int PAGE_SIZE, int PAGE_INDEX, ref int SelectRecordCount, string SortBy, int? SortDirection)
        {
            return DAL_Crew_MedHistory.Get_Crew_MedHistory_DL(FleetCode, Vessel_ID, CrewID, Case_Type, Status, SearchText, UserID, PAGE_SIZE, PAGE_INDEX, ref SelectRecordCount, SortBy, SortDirection);
        }
        public static DataSet Get_Crew_MedHistory_Details(int ID,int Vessel_ID,int Office_ID, int UserID)
        {
            return DAL_Crew_MedHistory.Get_Crew_MedHistory_Details_DL(ID,Vessel_ID,Office_ID, UserID);
        }
        
        public static DataTable Get_Crew_MedHistory_Types(int UserID)
        {
            return DAL_Crew_MedHistory.Get_Crew_MedHistory_Types_DL(UserID);
        }
        public static DataTable Get_Crew_MedHistory_CostItem_Types(int UserID)
        {
            return DAL_Crew_MedHistory.Get_Crew_MedHistory_CostItem_Types_DL(UserID);
        }
       /// //////////////////////////////
        public static DataTable Get_Med_CostItem_Attachments(int Cost_Item_ID, int Case_ID, int Vessel_ID, int Office_ID, int UserID)
        {
            return DAL_Crew_MedHistory.Get_Med_CostItem_Attachments_DL(Cost_Item_ID,Case_ID, Vessel_ID, Office_ID, UserID);
        }
        public static DataTable Get_Crew_MedHistory_Attachments(int Case_ID, int Vessel_ID, int Office_ID, int UserID)
        {
            return DAL_Crew_MedHistory.Get_Crew_MedHistory_Attachments_DL(Case_ID, Vessel_ID, Office_ID, UserID);
        }
        public static DataTable Get_Crew_MedHistory_Followups(int Case_ID, int Vessel_ID, int Office_ID, int UserID)
        {
            return DAL_Crew_MedHistory.Get_Crew_MedHistory_Followups_DL(Case_ID, Vessel_ID, Office_ID, UserID);
        }

        public static int INSERT_Crew_MedHistory_FollowUp(int Case_ID, int Vessel_ID, int Office_ID, string FollowUpText, int Created_By)
        {
            return DAL_Crew_MedHistory.INSERT_Crew_MedHistory_FollowUp_DL(Case_ID, Vessel_ID, Office_ID, FollowUpText, Created_By);
        }
        public static int DELETE_Crew_MedHistory_FollowUp(int FollowUpID, int Vessel_ID, int Office_ID, int Deleted_By)
        {
            return DAL_Crew_MedHistory.DELETE_Crew_MedHistory_FollowUp_DL(FollowUpID, Vessel_ID, Office_ID, Deleted_By);
        }

        public static int INSERT_Crew_MedHistory_Attachment(int Case_ID, int Vessel_ID, int Office_ID, string Attachment_Name, string Attachment_Path, decimal Attachment_Size, int Created_By)
        {
            return DAL_Crew_MedHistory.INSERT_Crew_MedHistory_Attachment_DL(Case_ID, Vessel_ID, Office_ID, Attachment_Name, Attachment_Path, Attachment_Size, Created_By);
        }
        public static int DELETE_Crew_MedHistory_Attachment(int Attachment_ID, int Case_ID, int Vessel_ID, int Office_ID, int Deleted_By)
        {
            return DAL_Crew_MedHistory.DELETE_Crew_MedHistory_Attachment_DL(Attachment_ID,Case_ID, Vessel_ID, Office_ID, Deleted_By);
        }

        public static int INSERT_Med_Cost_Item(int Case_ID, int Vessel_ID, int Office_ID, string Exp_Date, string Desc, int Exp_Type, int Local_Curr, decimal? Local_Amt, decimal? USD_Amt, int Created_By)
        {
            DateTime dtExp_Date = DateTime.Parse(Exp_Date, iFormatProvider);
            return DAL_Crew_MedHistory.INSERT_Med_Cost_Item_DL(Case_ID, Vessel_ID, Office_ID, dtExp_Date, Desc, Exp_Type, Local_Curr, Local_Amt, USD_Amt, Created_By);
        }
        public static int UPDATE_Med_Cost_Item(int Cost_Item_ID, int Case_ID, int Vessel_ID, int Office_ID, string Exp_Date, string Desc, int Exp_Type, int Local_Curr, decimal? Local_Amt, decimal? USD_Amt, int Created_By)
        {
            DateTime dtExp_Date = DateTime.Parse(Exp_Date, iFormatProvider);
            return DAL_Crew_MedHistory.UPDATE_Med_Cost_Item_DL(Cost_Item_ID, Case_ID, Vessel_ID, Office_ID, dtExp_Date, Desc, Exp_Type, Local_Curr, Local_Amt, USD_Amt, Created_By);
        }
        public static int DELETE_Med_Cost_Item(int Cost_Item_ID, int Case_ID, int Vessel_ID, int Office_ID, int Deleted_By)
        {
            return DAL_Crew_MedHistory.DELETE_Med_Cost_Item_DL(Cost_Item_ID,Case_ID, Vessel_ID, Office_ID, Deleted_By);
        }
        public static int INSERT_Med_Cost_Item_Attachment(int Case_ID, int Cost_Item_ID, int Vessel_ID, int Office_ID, string Attachment_Name, string Attachment_Path, decimal Attachment_Size, int Created_By)
        {
            return DAL_Crew_MedHistory.INSERT_Med_Cost_Item_Attachment_DL(Case_ID, Cost_Item_ID, Vessel_ID, Office_ID, Attachment_Name, Attachment_Path, Attachment_Size, Created_By);
        }
        ////////////////////////////////////////
        public static int Approve_Crew_MedHistory_Claims(int Crew_MedHistory_ID, int Vessel_ID, DataTable dtClaims, int ApprovedBy)
        {
            return DAL_Crew_MedHistory.Approve_Crew_MedHistory_Claims_DL(Crew_MedHistory_ID, Vessel_ID, dtClaims, ApprovedBy);
        }
        public static int Reject_Crew_MedHistory_Claims(int Crew_MedHistory_ID, int Vessel_ID, DataTable dtClaims, int ApprovedBy)
        {
            return DAL_Crew_MedHistory.Reject_Crew_MedHistory_Claims_DL(Crew_MedHistory_ID, Vessel_ID, dtClaims, ApprovedBy);
        }
      
    }
}
