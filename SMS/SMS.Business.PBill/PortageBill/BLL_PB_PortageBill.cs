using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SMS.Data.PortageBill;
using SMS.Properties;

namespace SMS.Business.PortageBill
{
    public class BLL_PB_PortageBill
    {
        static IFormatProvider iFormatProvider = new System.Globalization.CultureInfo("en-GB", true);

        public static DataTable Get_PortageBills(int Fleet_ID, int Vessel_ID, int? Month = null, int? Year = null)
        {
            try
            {
                return DAL_PB_PortageBill.Get_PortageBills_DL(Fleet_ID, Vessel_ID, Month, Year);
            }
            catch
            {
                throw;
            }

        }


        public static DataTable Get_PortageBills(int Fleet_ID, int Vessel_ID, int? Month, int? Year, int? Page_Index, int? Page_Size, ref int is_Fetch_Count)
        {
            try
            {
                return DAL_PB_PortageBill.Get_PortageBills_DL(Fleet_ID, Vessel_ID, Month, Year, Page_Index, Page_Size, ref is_Fetch_Count);
            }
            catch
            {
                throw;
            }

        }

        public static DataSet Get_PortageBill(int Month, int Year, int? Vessel_ID, string Search, int? Generate_PBill)
        {
             return DAL_PB_PortageBill.Get_PortageBill( Month,  Year,  Vessel_ID,  Search,  Generate_PBill);
        }

        public static DataSet Get_PortageBill_Details(int Vessel_ID, string PB_Month, string PB_Year, int PreparedBy)
        {
            try
            {
                return DAL_PB_PortageBill.Get_PortageBill_Details_DL(Vessel_ID, PB_Month, PB_Year, PreparedBy);
            }
            catch
            {
                throw;
            }

        }

        public static string Get_LastPortageBillDate(int Vessel_ID)
        {
            try
            {
                return DAL_PB_PortageBill.Get_LastPortageBillDate_DL(Vessel_ID);
            }
            catch
            {
                throw;
            }

        }

        public static DataSet Get_CapCashReport(string month, string year, string Vessel)
        {
            return DAL_PB_PortageBill.Get_CapCashReport_DL(month, year, Vessel);
        }

        public static DataSet LatestReportID()
        {
            return DAL_PB_PortageBill.LatestReportID_DL();
        }

        public static DataSet ReportDetailsByID(string Id, string Vessel)
        {
            return DAL_PB_PortageBill.ReportDetailsByID_DL(Id, Vessel);
        }

        public static int AddNew_Allotment(int CrewID, int VoyageID, int Vessel_ID, int BankAccID, decimal Amount, string PBill_Date, int UserID, int is_special_alt)
        {
            try
            {
                return DAL_PB_PortageBill.AddNew_Allotment_DL(CrewID, VoyageID, Vessel_ID, BankAccID, Amount, PBill_Date, UserID, is_special_alt);
            }
            catch
            {
                throw;
            }
        }

        public static DataTable Get_Allotments(int FleetID, int Vessel_ID, string Month, string Year, int Approve_Status, int BankID, int Sent_Status, string SearchText, int? CrewID = null, int? Verification_sts = null, int? AllotmentID = null, int? AmountValue = null, bool FlagedItems = false, int Country_ID=0)
        {
            try
            {
                return DAL_PB_PortageBill.Get_Allotments_DL(FleetID, Vessel_ID, Month, Year, Approve_Status, BankID, Sent_Status, SearchText, CrewID, Verification_sts, AllotmentID, AmountValue, FlagedItems,Country_ID);
            }
            catch
            {
                throw;
            }
        }

        public static int Verify_Allotment(int Vessel_ID, int ID, int Verified_By)
        {
            try
            {
                return DAL_PB_PortageBill.Verify_Allotment_DL(Vessel_ID, ID, Verified_By);
            }
            catch
            {
                throw;
            }

        }

        public static int INSERT_Allotment(int Vessel_ID, int CrewID, string BankAcc, string CreateDate, int Currency_ID, Decimal Amount, int is_special_alt, int Created_By)
        {
            try
            {

                DateTime Dt_CreateDate = DateTime.Parse("1900/01/01");
                if (CreateDate != "")
                    Dt_CreateDate = DateTime.Parse(CreateDate, iFormatProvider);

                return DAL_PB_PortageBill.INSERT_Allotment_DL(Vessel_ID, CrewID, BankAcc, CreateDate, Currency_ID, Amount, is_special_alt, Created_By);
            }
            catch
            {
                throw;
            }

        }

        public static int Delete_Allotment(int AllotmentID, int Deleted_By)
        {
            try
            {
                return DAL_PB_PortageBill.Delete_Allotment_DL(AllotmentID, Deleted_By);
            }
            catch
            {
                throw;
            }

        }

        public static int Delete_Allotment(int AllotmentID, int Vessel_ID, DateTime PBDate, int Deleted_By)
        {
            try
            {
                return DAL_PB_PortageBill.Delete_Allotment_DL(AllotmentID, Vessel_ID, PBDate, Deleted_By);
            }
            catch
            {
                throw;
            }

        }

        public static DataTable Get_CTM_Requests(DataTable FleetID, DataTable Vessel_ID, string FromDate, string ToDate, string CTMStatus, string SearchText, int? PendingWithUserID, int? Page_Index, int? Page_Size, ref int is_Fetch_Count)
        {
            try
            {


                return DAL_PB_PortageBill.Get_CTM_Requests_DL(FleetID, Vessel_ID, UDFLib.ConvertDateToNull(FromDate), UDFLib.ConvertDateToNull(ToDate), CTMStatus, SearchText, PendingWithUserID, Page_Index, Page_Size, ref  is_Fetch_Count);
            }
            catch
            {
                throw;
            }
        }

        public static DataSet Get_CTM_Details(int Vessel_ID, int ID, int Office_ID)
        {
            try
            {
                return DAL_PB_PortageBill.Get_CTM_Details_DL(Vessel_ID, ID, Office_ID);
            }
            catch
            {
                throw;
            }
        }

        public static DataTable Get_Cash_On_Board(int Vessel_ID)
        {
            return DAL_PB_PortageBill.Get_Cash_On_Board(Vessel_ID);
        }

        public static DataTable Get_CTM_Denominations(int Vessel_ID, int CTM_ID, int Office_ID)
        {
            try
            {
                return DAL_PB_PortageBill.Get_CTM_Denominations_DL(Vessel_ID, CTM_ID, Office_ID);
            }
            catch
            {
                throw;
            }
        }

        public static int UPDATE_CTM_Denominations(int Vessel_ID, int CTM_ID, int CTM_Office_ID, int ID, int Notes_Office, int Denomination, int Modified_By)
        {
            try
            {
                return DAL_PB_PortageBill.UPDATE_CTM_Denominations_DL(Vessel_ID, CTM_ID, CTM_Office_ID, ID, Notes_Office, Denomination, Modified_By);
            }
            catch
            {
                throw;
            }
        }

        public static int UPDATE_CTM_Approval(int Vessel_ID, int CTM_ID, int CTM_Office_ID, decimal ApprovedAmt, int Approved_By, string Remarks, int ApprovalStatus, string Order_Supplier, decimal Supplier_Commission)
        {
            try
            {
                return DAL_PB_PortageBill.UPDATE_CTM_Approval_DL(Vessel_ID, CTM_ID, CTM_Office_ID, ApprovedAmt, Approved_By, Remarks, ApprovalStatus, Order_Supplier, Supplier_Commission);
            }
            catch
            {
                throw;
            }
        }

        public static int UPDATE_CTM_Port(int Vessel_ID, int CTM_ID, int CTM_Office_ID, string CTM_Date, int CTM_Port, int Modified_By, int PortCall_ID)
        {
            try
            {
                return DAL_PB_PortageBill.UPDATE_CTM_Port_DL(Vessel_ID, CTM_ID, CTM_Office_ID, CTM_Date, CTM_Port, Modified_By, PortCall_ID);
            }
            catch
            {
                throw;
            }
        }

        public static int UPDATE_CTM_Finalize(int Vessel_ID, int CTM_ID, int CTM_Office_ID, int Modified_By)
        {
            try
            {
                return DAL_PB_PortageBill.UPDATE_CTM_Finalize_DL(Vessel_ID, CTM_ID, CTM_Office_ID, Modified_By);
            }
            catch
            {
                throw;
            }
        }

        public static DataTable Get_VesselMinCTM(int Fleet_ID, int Vessel_ID, int UserID)
        {
            try
            {
                return DAL_PB_PortageBill.Get_VesselMinCTM_DL(Fleet_ID, Vessel_ID, UserID);
            }
            catch
            {
                throw;
            }
        }

        public static int UPDATE_VesselMinCTM(int Vessel_ID, decimal MinCTM, int UserID)
        {
            try
            {
                return DAL_PB_PortageBill.UPDATE_VesselMinCTM_DL(Vessel_ID, MinCTM, UserID);
            }
            catch
            {
                throw;
            }
        }

        public static DataTable Get_Crew_Salary_Instructions(int CrewID, int VoyageID, int UserID)
        {
            try
            {
                return DAL_PB_PortageBill.Get_Crew_Salary_Instructions_DL(CrewID, VoyageID, UserID);
            }
            catch
            {
                throw;
            }
        }

        public static int INS_Crew_Salary_Instruction(int CrewID, int Vessel_ID, int Voyage_ID, int Earn_Deduction, string PBill_Date, int SalaryType, decimal Amount, string Remarks, int Created_By)
        {
            try
            {
                DateTime Dt_PBill_Date = DateTime.Today;
                if (PBill_Date != "")
                    Dt_PBill_Date = DateTime.Parse(PBill_Date, iFormatProvider);

                return DAL_PB_PortageBill.INS_Crew_Salary_Instruction_DL(CrewID, Vessel_ID, Voyage_ID, Earn_Deduction, Dt_PBill_Date, SalaryType, Amount, Remarks, Created_By);
            }
            catch
            {
                throw;
            }
        }

        public static int UPDATE_Crew_Salary_Instruction(int ID, int CrewID, int Vessel_ID, int Voyage_ID, int Earn_Deduction, string PBill_Date, int SalaryType, decimal Amount, string Remarks, int Created_By)
        {
            try
            {

                return DAL_PB_PortageBill.UPDATE_Crew_Salary_Instruction_DL(ID, CrewID, Vessel_ID, Voyage_ID, Earn_Deduction, PBill_Date, SalaryType, Amount, Remarks, Created_By);
            }
            catch
            {
                throw;
            }
        }

        public static int Update_Allotment(int Vessel_ID, int Allotment_ID, decimal Amount, int Account_ID, int Modified_BY)
        {
            return DAL_PB_PortageBill.Update_Allotment_DL(Vessel_ID, Allotment_ID, Amount, Account_ID, Modified_BY);
        }


        public static DataSet ACC_Get_CrewWages_ByMonth(int CrewID, int Month, int Year)
        {
            return DAL_PB_PortageBill.ACC_Get_CrewWages_ByMonth_DL(CrewID, Month, Year);
        }


        public static DataSet Get_Allotments_ByCrewID(int CrewId, int PBMonth, int PBYear)
        {
            return DAL_PB_PortageBill.Get_Allotments_ByCrewID_DL(CrewId, PBMonth, PBYear);
        }

        public static DataTable Get_BOW_Document(int Vessel_ID, DateTime? PBill_Date)
        {
            return DAL_PB_PortageBill.Get_BOW_Document_DL(Vessel_ID, PBill_Date);
        }


        public static DataSet Get_PB_YearMonth(int? Year)
        {
            return DAL_PB_PortageBill.Get_PB_YearMonth_DL(Year);
        }

        public static int Upd_CTM_Rework(int CTM_ID, int Vessel_ID, string Remark, int UserID)
        {
            return DAL_PB_PortageBill.Upd_CTM_Rework_DL(CTM_ID, Vessel_ID, Remark, UserID);
        }


        public static void Send_Mail_CTM_Received()
        {
            DAL_PB_PortageBill.Send_Mail_CTM_Received_DL();
        }

        public static DataTable Get_CaptCash_Items_Attachments(int Vessel_ID, int CaptCash_Details_ID)
        {
            return DAL_PB_PortageBill.Get_CaptCash_Items_Attachments(Vessel_ID, CaptCash_Details_ID);
        }

        public static DataTable Get_Portage_Bill_Attachments(int Vessel_ID, string month, string year, int Doc_type)
        {

            return DAL_PB_PortageBill.Get_Portage_Bill_Attachments(Vessel_ID, month, year, Doc_type);
        }



        public static DataSet ACC_GET_Allotment_Flag(int Allotment_ID, int Vessel_ID, DateTime PBill_Date)
        {
            return DAL_PB_PortageBill.ACC_GET_Allotment_Flag(Allotment_ID, Vessel_ID, PBill_Date);

        }

        public static int ACC_INS_Allotment_Flag(int Allotment_ID, int Vessel_ID, DateTime PBill_Date, string Remark, int Created_By, DataTable tblAttach, string Flag_Status)
        {
            return DAL_PB_PortageBill.ACC_INS_Allotment_Flag(Allotment_ID, Vessel_ID, PBill_Date, Remark, Created_By, tblAttach, Flag_Status);
        }

        public static string ACC_Get_Allotment_Flag_Mail(int UserID)
        {
            return DAL_PB_PortageBill.ACC_Get_Allotment_Flag_Mail(UserID);
        }

        public static DataTable Allotment_Flag_Mail_Users(string Search)
        {
            return DAL_PB_PortageBill.Allotment_Flag_Mail_Users(Search);
        }

        public static int UPD_Allotment_Flag_Mail_Users(DataTable dtList, int UserID)
        {
            return DAL_PB_PortageBill.UPD_Allotment_Flag_Mail_Users(dtList, UserID);
        }

        public static int UPD_Save_CTM_Details(int User_ID, CTM_Deatils prCTM)
        {
            return DAL_PB_PortageBill.UPD_Save_CTM_Details(User_ID, prCTM);
        }

        public static DataTable Get_CTM_OffSignersList(int CTM_ID, int Vessel_ID)
        {

            return DAL_PB_PortageBill.Get_CTM_OffSignersList(CTM_ID, Vessel_ID);
        }

        public static DataTable GET_Side_Letter(int Voyage_ID)
        {
            return DAL_PB_PortageBill.GET_Side_Letter(Voyage_ID);
        }

        public static DataTable Get_Crew_Welfare_Details(int Welfare_ID, int Vessel_ID, DateTime? PB_Date)
        {
            return DAL_PB_PortageBill.Get_Crew_Welfare_Details(Welfare_ID, Vessel_ID, PB_Date);
        }

        public static DataTable Get_Welfare_Details_Documents(int Welfare_Details_ID, int Vessel_ID)
        {
            return DAL_PB_PortageBill.Get_Welfare_Details_Documents(Welfare_Details_ID, Vessel_ID);
        }

        public static DataTable Get_Crew_Welfare_Effective_Dates(int? Vessel_ID)
        {
            return DAL_PB_PortageBill.Get_Crew_Welfare_Effective_Dates(Vessel_ID);
        }

        public static DataTable Get_Lib_Crew_Welfare(int? Vessel_ID, DateTime? Effective_Date, int Page_Index, int Page_Size, ref int Record_Count)
        {
            return DAL_PB_PortageBill.Get_Lib_Crew_Welfare(Vessel_ID, Effective_Date, Page_Index, Page_Size, ref Record_Count);
        }

        public static int UPD_Lib_Crew_Welfare(int Welfare_ID, decimal Welfare_Amount, int Vessel_ID, DateTime Effective_Date, int UserID)
        {
            return DAL_PB_PortageBill.UPD_Lib_Crew_Welfare(Welfare_ID, Welfare_Amount, Vessel_ID, Effective_Date, UserID);
        }
        
        public static int Get_Bank_Account_Status(int AllotmentID, int Vessel_ID)
        {
            return DAL_PB_PortageBill.Get_Bank_Account_Status(AllotmentID,Vessel_ID);
        }

        public static DateTime? Get_Valid_Date_TO_Rework_Allotment(int Vessel_ID)
        {
            return DAL_PB_PortageBill.Get_Valid_Date_TO_Rework_Allotment(Vessel_ID);
        }

        public static int UPD_Rework_Allotment(int Vessel_ID, int UserID, DateTime PBill_Date)
        {
            return DAL_PB_PortageBill.UPD_Rework_Allotment(Vessel_ID, UserID, PBill_Date);
        }

        public static int Unverify_Allotment(int Vessel_ID, int ID, int Verified_By)
        {
            try
            {
                return DAL_PB_PortageBill.Unverify_Allotment_DL(Vessel_ID, ID, Verified_By);
            }
            catch
            {
                throw;
            }

        }

        public static DataTable Get_CTM_Attachments(int CTM_ID, int Vessel_ID)
        {
            return DAL_PB_PortageBill.Get_CTM_Attachments_DL(CTM_ID, Vessel_ID);
        }

        public static DataTable Get_PerMOAllotments(int FleetID, int Vessel_ID, string Month, string Year, int MO_ID, int BankID, int? CrewID = null, int Country_ID = 0)
        {
            try
            {
                return DAL_PB_PortageBill.Get_PerMOAllotments_DL(FleetID, Vessel_ID, Month, Year, MO_ID, BankID, CrewID, Country_ID);
            }
            catch
            {
                throw;
            }
        }

        //New Methods for Add Capt Cash (Un-Finalized) 
        public static DataSet Get_CapCashReportCur(int? Vessel_ID)
        {
            return DAL_PB_PortageBill.Get_CapCashReport_Cur_DL(Vessel_ID);
        }

        public static DataSet ReportDetailsByID_Cur(string Id, string Vessel)
        {
            return DAL_PB_PortageBill.ReportDetailsByID_Cur_DL(Id, Vessel);
        }

        public static int Upd_Generate_PortageBill(string Month, string Year, int Vessel_ID, int UserID, int? Crew_ID)
        {
           return DAL_PB_PortageBill.Upd_Generate_PortageBill(Month, Year, Vessel_ID, UserID,  Crew_ID);
        }

        public static int Upd_Finalize_PortageBill(int Month, int Year, int Vessel_ID, int UserID)
        {
           return DAL_PB_PortageBill.Upd_Finalize_PortageBill( Month, Year, Vessel_ID, UserID);
        }


        public static DataTable Get_Open_PortageBill()
        {
            return DAL_PB_PortageBill.Get_Open_PortageBill();
        }
        public static int INS_Initial_Office_Portage_Bill_DL(int Vessel_ID, DateTime? Date, int UserID)
        {
            return  DAL_PB_PortageBill.INS_Initial_Office_Portage_Bill_DL(Vessel_ID, Date, UserID);
        }
        

    }
}
