using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SMS.Data.Crew;


namespace SMS.Business.Crew
{
    public class BLL_Crew_Disbursement
    {
        static IFormatProvider iFormatProvider = new System.Globalization.CultureInfo("en-GB", true);

        #region -- GET --
        public static DataTable Get_MODisbursementFeeTypes()
        {
            try
            {
                return DAL_Crew_Disbursement.Get_MODisbursementFeeTypes_DL();
            }
            catch
            {
                throw;
            }

        }
        public static DataTable Get_MODisbursementFee(int ManningOfficeID, int FeeType)
        {
            try
            {
                return DAL_Crew_Disbursement.Get_MODisbursementFee_DL(ManningOfficeID, FeeType);
            }
            catch
            {
                throw;
            }

        }
        public static DataSet Get_MOAgencyFee(int UserID)
        {
            try
            {
                return DAL_Crew_Disbursement.Get_MOAgencyFee_DL(UserID);
            }
            catch
            {
                throw;
            }

        }
        public static DataSet Get_MOProcessingFee(int UserID)
        {
            try
            {
                return DAL_Crew_Disbursement.Get_MOProcessingFee_DL(UserID);
            }
            catch
            {
                throw;
            }

        }
        public static DataTable Get_AllCrewFeeStatus(int FleetCode, int VesselID, int ManningOfficeID, int Rank_Category, int UserID, int Crew_Status, int Fee_Type, int Approved_YesNo, string Sign_On_From, string Sign_On_To,string Approved_From, string Approved_To, int? PAGE_SIZE, int? PAGE_INDEX, ref int SelectRecordCount, ref decimal GrandTotal,int Month,int Year,string SearchText)
        {
            try
            {
                DateTime dtSign_On_From = DateTime.Parse("1900/01/01");
                if (Sign_On_From  != "")
                    dtSign_On_From = DateTime.Parse(Sign_On_From, iFormatProvider);

                DateTime dtSign_On_To = DateTime.Parse("2900/01/01");
                if (Sign_On_To  != "")
                    dtSign_On_To = DateTime.Parse(Sign_On_To, iFormatProvider);

                DateTime dtApproved_From = DateTime.Parse("1900/01/01");
                if (Approved_From != "")
                    dtApproved_From = DateTime.Parse(Approved_From, iFormatProvider);

                DateTime dtApproved_To = DateTime.Parse("2900/01/01");
                if (Approved_To != "")
                    dtApproved_To = DateTime.Parse(Approved_To, iFormatProvider);

                return DAL_Crew_Disbursement.Get_AllCrewFeeStatus_DL(FleetCode, VesselID, ManningOfficeID, Rank_Category, UserID, Crew_Status, Fee_Type, Approved_YesNo, dtSign_On_From, dtSign_On_To, dtApproved_From, dtApproved_To, PAGE_SIZE, PAGE_INDEX, ref SelectRecordCount, ref GrandTotal, Month, Year, SearchText);
            }
            catch
            {
                throw;
            }

        }
        public static DataTable Get_MODisbursementFeeDetails(int ID, int UserID)
        {
            try
            {
                return DAL_Crew_Disbursement.Get_MODisbursementFeeDetails_DL(ID, UserID);
            }
            catch
            {
                throw;
            }

        }
        public static DataTable Get_VesselList_CrewFee(int UserID, int FleetCode)
        {
            try
            {
                return DAL_Crew_Disbursement.Get_VesselList_CrewFee_DL(UserID, FleetCode);
            }
            catch
            {
                throw;
            }

        }

        #endregion

        public static int AddNew_ProcessingFee(int CrewID, int Vessel_ID, int VoyageID, int ManningOfficeID, int FeeType, decimal Amount, string Due_Date, int Created_By, string Remarks)
        {
            try
            {
                DateTime Dt_Due_Date = DateTime.Parse("1900/01/01");
                if (Due_Date != "")
                    Dt_Due_Date = DateTime.Parse(Due_Date, iFormatProvider);

                return DAL_Crew_Disbursement.AddNew_ProcessingFee_DL(CrewID, Vessel_ID, VoyageID, ManningOfficeID, FeeType, Amount, Dt_Due_Date, Created_By, Remarks);
            }
            catch
            {
                throw;
            }
        }


        #region -- UPDATE --
        public static int UPDATE_MODisbursementFee_DL(int ManningOfficeID, int FeeType, decimal Amount, int ID, string EffectiveDate, int Created_By, int Rank_Category)
        {
            int sts = 0;
            try
            {
                DateTime Dt_EffectiveDate = DateTime.Parse("1900/01/01");
                if (DateTime.TryParse(EffectiveDate, out Dt_EffectiveDate) && Dt_EffectiveDate != DateTime.Parse("1900/01/01"))
                {
                    sts = DAL_Crew_Disbursement.UPDATE_MODisbursementFee_DL(ManningOfficeID, FeeType, Amount, ID, Dt_EffectiveDate, Created_By, Rank_Category);
                }
                return sts;
            }
            catch
            {
                throw;
            }

        }


        public static int UPDATE_MODisbursementFee_DL( DataTable dt, int FeeType, int Created_By)
        {
            int sts = 0;
            try
            {
                DateTime Dt_EffectiveDate = DateTime.Parse("1900/01/01");
                //if (DateTime.TryParse(EffectiveDate, out Dt_EffectiveDate) && Dt_EffectiveDate != DateTime.Parse("1900/01/01"))
                //{
                    sts = DAL_Crew_Disbursement.UPDATE_MODisbursementFee_DL(dt, FeeType, Created_By);
               // }
                return sts;
            }
            catch
            {
                throw;
            }

        }      
        public static int UPDATE_ApprovedStatus(int ID, int CrewID, int Approved_YesNo, string Approved_Date, int Approved_By, string Remarks)
        {
            try
            {
                DateTime dtApproved_Date = DateTime.Now;
                if (Approved_Date != "")
                    dtApproved_Date = DateTime.Parse(Approved_Date, iFormatProvider);

                return DAL_Crew_Disbursement.UPDATE_ApprovedStatus_DL(ID, CrewID, Approved_YesNo, dtApproved_Date, Approved_By, Remarks);
            }
            catch
            {
                throw;
            }

        }
        #endregion
    }
}
