using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SMS.Data.Operation;

namespace SMS.Business.Operation
{
    public class BLL_OPS_BunkerAnalysis
    {
        static IFormatProvider iFormatProvider = new System.Globalization.CultureInfo("en-GB", true);

        public static DataTable Get_BunkerAnalysisReport(int FleetCode, int Vessel_ID, int Bunker_Supplier, int Bunker_Lab, int Status, string DateFrom, string DateTo, int UserID, int? PAGE_SIZE, int? PAGE_INDEX, ref int SelectRecordCount)
        {
            DateTime dtDateFrom = DateTime.Parse("1900/01/01");
            DateTime dtDateTo = DateTime.Parse("2900/01/01");

            if(DateFrom != "")
                dtDateFrom = DateTime.Parse(DateFrom, iFormatProvider);

            if (DateTo != "")
                dtDateTo = DateTime.Parse(DateTo, iFormatProvider);

            return DAL_OPS_BunkerAnalysis.Get_BunkerAnalysisReport_DL(FleetCode, Vessel_ID, Bunker_Supplier, Bunker_Lab, Status, dtDateFrom, dtDateTo, UserID, PAGE_SIZE, PAGE_INDEX, ref SelectRecordCount);
        }

        public static DataTable Get_LOAnalysisReport(int FleetCode, int Vessel_ID, int LO_Supplier, int LO_Lab, int ReportStatus, string DateFrom, string DateTo, int UserID, int BunkerPort, string AirwayBill, int? PAGE_SIZE, int? PAGE_INDEX, ref int SelectRecordCount)
        {
            DateTime dtDateFrom = DateTime.Parse("1900/01/01");
            DateTime dtDateTo = DateTime.Parse("2900/01/01");

            if (DateFrom != "")
                dtDateFrom = DateTime.Parse(DateFrom, iFormatProvider);

            if (DateTo != "")
                dtDateTo = DateTime.Parse(DateTo, iFormatProvider);

            return DAL_OPS_BunkerAnalysis.Get_LOAnalysisReport_DL(FleetCode, Vessel_ID, LO_Supplier, LO_Lab, ReportStatus, dtDateFrom, dtDateTo, UserID, BunkerPort, AirwayBill, PAGE_SIZE, PAGE_INDEX, ref SelectRecordCount);
        }

        public static DataSet UPDATE_BunkerAnalysis(int ID, int Vessel_ID, string Bunkering_Date, int PortID, int Bunker_SupplierID, int LabID, string AirwayBill, int Status, int UserID)
        {
            DateTime dtBunkering_Date  = DateTime.Parse("1900/01/01");

            if (Bunkering_Date != "")
                dtBunkering_Date = DateTime.Parse(Bunkering_Date, iFormatProvider);

            return DAL_OPS_BunkerAnalysis.UPDATE_BunkerAnalysis_DL(ID,Vessel_ID, dtBunkering_Date, PortID, Bunker_SupplierID, LabID, AirwayBill, Status, UserID);
        }
        
        public static int UPDATE_BunkerAnalysisStatus(int ID, int Status, int SampleReceived, int UserID)
        {
            return DAL_OPS_BunkerAnalysis.UPDATE_BunkerAnalysisStatus_DL(ID, Status, SampleReceived, UserID);
        }

        public static int UPDATE_BunkerAnalysisStatus(int ID, int Status, int UserID)
        {
            return DAL_OPS_BunkerAnalysis.UPDATE_BunkerAnalysisStatus_DL(ID, Status, UserID);
        }

        public static DataTable Get_BunkerSampleAttachments(int Sample_ID, int Type, int UserID)
        {
            return DAL_OPS_BunkerAnalysis.Get_BunkerSampleAttachments_DL(Sample_ID, Type, UserID);
        }

        public static int Insert_BunkerSampleAttachment(int Sample_ID, int Type, string Attachment_Name, string File_Name, int UserID)
        {
            return DAL_OPS_BunkerAnalysis.Insert_BunkerSampleAttachment_DL(Sample_ID, Type, Attachment_Name, File_Name, UserID);
        }

        public static int SendMail_ToVessel(int Sample_ID, int UserID)
        {
            return DAL_OPS_BunkerAnalysis.SendMail_ToVessel_DL(Sample_ID, UserID);
        }
        
        public static int SendMail_ToInternal(int Sample_ID, int UserID)
        {
            return DAL_OPS_BunkerAnalysis.SendMail_ToInternal_DL(Sample_ID, UserID);
        }

        public static int SendMail_AckSampleReceived(int Sample_ID, int UserID)
        {
            return DAL_OPS_BunkerAnalysis.SendMail_AckSampleReceived_DL(Sample_ID, UserID);
        }

        public static DataTable Get_BunkerSupplierList(int UserID)
        {
            return DAL_OPS_BunkerAnalysis.Get_BunkerSupplierList_DL(UserID);
        }
        public static DataTable Get_LOSupplierList(int UserID)
        {
            return DAL_OPS_BunkerAnalysis.Get_LOSupplierList_DL(UserID);
        }

        public static DataTable Get_BunkerTestingLabList(int UserID)
        {
            return DAL_OPS_BunkerAnalysis.Get_BunkerTestingLabList_DL(UserID);
        }
        public static DataTable Get_LOTestingLabList(int UserID)
        {
            return DAL_OPS_BunkerAnalysis.Get_LOTestingLabList_DL(UserID);
        }
        public static DataTable Get_LOTestingLabList(string SearchText, int CountryID, int UserID,  string sortbycoloumn, int? sortdirection, int CurrentPageIndex, int PageSize, ref int rowcount)
        {
            return DAL_OPS_BunkerAnalysis.Get_LOTestingLabList_DL(SearchText, CountryID, UserID,  sortbycoloumn, sortdirection, CurrentPageIndex, PageSize, ref rowcount);
        }
        public DataTable GetSupplierInfo(string UserId, string strPassword)
        {
            return DAL_OPS_BunkerAnalysis.GetSupplierInfo(UserId, strPassword);
        }

        public static DataTable Get_LOTestingLabByID(int ID, int UserID)
        {
            return DAL_OPS_BunkerAnalysis.Get_LOTestingLabByID_DL(ID, UserID);
        }  
        public static int Insert_LO_Testing_Lab(string Lab_Name, string Address, string EMail, string Phone, int CountryID, int UserID)
        {
            try
            {
                return DAL_OPS_BunkerAnalysis.Insert_LO_Testing_Lab_DL(Lab_Name, Address, EMail, Phone, CountryID, UserID);
            }
            catch
            {
                throw;
            }
        }

        public static int Update_LO_Testing_Lab(int ID, string Lab_Name, string Address, string EMail, string Phone, int CountryID, int UserID)
        {
            try
            {
                return DAL_OPS_BunkerAnalysis.Update_LO_Testing_Lab_DL(ID, Lab_Name, Address, EMail, Phone, CountryID, UserID);
            }
            catch
            {
                throw;
            }
        }

        public static int Delete_LO_Testing_Lab(int ID, int UserID)
        {
            try
            {
                return DAL_OPS_BunkerAnalysis.Delete_LO_Testing_Lab_DL(ID,UserID);
            }
            catch
            {
                throw;
            }
        }

        public static int Insert_LO_SampleAttachment(int Sample_ID, int Type, string Attachment_Name, string File_Name, int UserID)
        {
            return DAL_OPS_BunkerAnalysis.Insert_LO_SampleAttachment_DL(Sample_ID, Type, Attachment_Name, File_Name, UserID);
        }

    }

}