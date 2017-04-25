using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;

using SMS.Data.Crew;
using System.Data.SqlClient;

namespace SMS.Business.Crew
{
    public class BLL_Crew_CrewList
    {
        static IFormatProvider iFormatProvider = new System.Globalization.CultureInfo("en-GB", true);

        
        public static DataTable Get_CrewListBySearchParam(string Name, int Nationality, string Status, DateTime FromJoningDate, DateTime ToJoningDate)
        {
            try
            {
                string Query = @"SELECT STAFF_CODE,STAFF_SURNAME,STAFF_NAME,STAFF_NATIONALITY, 
                            CONVERT(VARCHAR(10),STAFF_BIRTH_DATE, 105) AS 'STAFF_BIRTH_DATE',
                            STAFF_BORN_PLACE,PASSPORT_NUMBER,  CONVERT(VARCHAR(10),PASSPORT_EXPIRY_DATE, 105) AS 'PASSPORT_EXPIRY_DATE',
                            ID FROM CRW_LIB_CREW_DETAILS  WHERE ACTIVE_STATUS=1 ";

                if (Name != "")
                    Query += " AND (STAFF_NAME like '%'+'" + Name + "' +'%' OR STAFF_SURNAME like  '%'+'" + Name + "' +'%')";

                if (Nationality != 0)
                    Query += "AND STAFF_NATIONALITY = " + Nationality.ToString();

                if (Status.ToUpper() == "NO VOYAGES")
                    Query += " AND STAFF_CODE NOT IN (SELECT STAFF_CODE FROM CRW_Dtl_Crew_Voyages  WHERE Active_Status=1 )";

                //else if (Status.ToUpper() == "CURRENT")
                //    Query += " AND STAFF_CODE IN (SELECT STAFF_CODE FROM CRW_Dtl_Crew_Voyages  WHERE Active_Status=1)";

                else if (Status.ToUpper() == "OFF" || Status.ToUpper() == "CURRENT")
                {
                    Query += " AND STAFF_CODE in ( SELECT STAFF_CODE FROM CRW_Dtl_Crew_Voyages  WHERE Active_Status=1";
                    if (Status.ToUpper() == "OFF")
                    {
                        Query += "  AND SIGN_OFF_DATE IS NOT NULL ";
                    }
                    else
                    {
                        Query += "  AND  SIGN_OFF_DATE IS  NULL   ";
                    }

                    if (FromJoningDate != DateTime.MinValue)
                    {
                        Query += "  AND JOINING_DATE <= '" + FromJoningDate.ToString() + "'  ";
                    }
                    if (ToJoningDate != DateTime.MinValue)
                    {
                        Query += "  AND  JOINING_DATE >= '" + FromJoningDate.ToString() + "'  ";
                    }
                    Query += "  ) ";
                }


                return DAL_Crew_CrewList.Get_CrewListBySearchParam_DL(Query);
            }
            catch
            {
                throw;
            }

        }

        public static DataTable Get_CrewListBySearchParam(int FleetCode, int VesselID, string Name, int Nationality, string Status, string FromJoningDate, string ToJoningDate, string StaffCode)
        {

            try
            {
                DateTime Dt_FromJoningDate = DateTime.Today;
                if (FromJoningDate.Length > 0)
                    Dt_FromJoningDate = DateTime.Parse(FromJoningDate, iFormatProvider);

                DateTime Dt_ToJoningDate = DateTime.Today;
                if (ToJoningDate.Length > 0)
                    Dt_ToJoningDate = DateTime.Parse(ToJoningDate, iFormatProvider);



                string Query = @"SELECT STAFF_CODE,STAFF_SURNAME,STAFF_NAME, (STAFF_NAME + ' ' + isnull(STAFF_SURNAME,'')) as STAFF_FULLNAME, STAFF_NATIONALITY, 
                            CONVERT(VARCHAR(10),STAFF_BIRTH_DATE, 105) AS 'STAFF_BIRTH_DATE',
                            STAFF_BORN_PLACE,PASSPORT_NUMBER,  CONVERT(VARCHAR(10),PASSPORT_EXPIRY_DATE, 105) AS 'PASSPORT_EXPIRY_DATE',
                            ID, '' as STATUSTEXT
                            FROM CRW_LIB_CREW_DETAILS  WHERE ACTIVE_STATUS=1 ";



                if (Name != "")
                    Query += " AND (STAFF_NAME like '%'+'" + Name + "' +'%' OR STAFF_SURNAME like  '%'+'" + Name + "' +'%')";

                if (StaffCode.Length > 0)
                    Query += " AND (STAFF_CODE like '%" + StaffCode + "%')";

                if (Nationality != 0)
                    Query += "AND STAFF_NATIONALITY = " + Nationality.ToString();

                if (Status.ToUpper() == "NO VOYAGES")
                    Query += " AND STAFF_CODE NOT IN (SELECT STAFF_CODE FROM CRW_Dtl_Crew_Voyages  WHERE Active_Status=1 )";

                //else if (Status.ToUpper() == "CURRENT")
                //    Query += " AND STAFF_CODE IN (SELECT STAFF_CODE FROM CRW_Dtl_Crew_Voyages  WHERE Active_Status=1)";

                else if (Status.ToUpper() == "OFF" || Status.ToUpper() == "CURRENT")
                {
                    Query += " AND STAFF_CODE in ( SELECT STAFF_CODE FROM CRW_Dtl_Crew_Voyages,Lib_vessels where CRW_Dtl_Crew_Voyages.vessel_id = lib_vessels.vessel_id  and  CRW_Dtl_Crew_Voyages.Active_Status=1";
                    if (Status.ToUpper() == "OFF")
                    {
                        Query += "  AND SIGN_OFF_DATE IS NOT NULL ";
                    }
                    else
                    {
                        Query += "  AND  SIGN_OFF_DATE IS  NULL   ";
                    }

                    if (FromJoningDate.Length > 0)
                    {
                        Query += "  AND JOINING_DATE >= '" + Dt_FromJoningDate.ToString() + "'  ";
                    }
                    if (ToJoningDate.Length > 0)
                    {
                        Query += "  AND  JOINING_DATE <= '" + Dt_ToJoningDate.ToString() + "'  ";
                    }

                    if (VesselID > 0)
                    {
                        Query += "  AND  CRW_Dtl_Crew_Voyages.Vessel_ID = " + VesselID.ToString() + " ";
                    }
                    else if (FleetCode > 0)
                    {
                        Query += "  AND  lib_vessels.fleet_code = " + FleetCode.ToString() + " ";
                    }

                    Query += "  ) ";
                }


                return DAL_Crew_CrewList.Get_CrewListBySearchParam_DL(Query);
            }
            catch
            {
                throw;
            }

        }
        public static DataTable Get_CrewList_Allotment(int Vessel_ID, string FreeText, string PBill_Date, int UserID, int PAGE_SIZE, int PAGE_INDEX, ref int SelectRecordCount)
        {
            return DAL_Crew_CrewList.Get_CrewList_Allotment_DL( Vessel_ID,FreeText,PBill_Date, UserID, PAGE_SIZE, PAGE_INDEX, ref SelectRecordCount);
        }

        public static DataTable Get_SigningOff_CrewList(int FleetCode, int VesselID, int RankID, int Nationality, string SignOffFromDate, string SignOffToDate, string FreeText, int UserID, int PAGE_SIZE, int PAGE_INDEX, ref int SelectRecordCount)
        {
            return DAL_Crew_CrewList.Get_SigningOff_CrewList_DL(FleetCode, VesselID, RankID, Nationality, SignOffFromDate, SignOffToDate, FreeText, UserID, PAGE_SIZE, PAGE_INDEX, ref SelectRecordCount);
        }
        public static DataTable Get_DetailCrewAssignment_List(int FleetCode, int VesselID, int RankID, int Nationality, string SignOffFromDate, string SignOffToDate, string FreeText, int UserID, int PAGE_SIZE, int PAGE_INDEX, ref int SelectRecordCount)
        {
            return DAL_Crew_CrewList.Get_DetailCrewAssignmentList_DL(FleetCode, VesselID, RankID, Nationality, SignOffFromDate, SignOffToDate, FreeText, UserID, PAGE_SIZE, PAGE_INDEX, ref SelectRecordCount);
        }
        public static DataTable Get_UnAssigned_CrewList(int ManningOfficeID, int Nationality, int RankID, string AvailableFromDate, string AvailableToDate, string FreeText, int VesselID, int VesselIdOffSigner, int Available_Option, int UserID, DataTable dtVesselType, int PAGE_SIZE, int PAGE_INDEX, ref int SelectRecordCount, string sortbycoloumn, int? sortdirection)
        {
            return DAL_Crew_CrewList.Get_UnAssigned_CrewList_DL(ManningOfficeID, Nationality, RankID, AvailableFromDate, AvailableToDate, FreeText, VesselID, VesselIdOffSigner, Available_Option, UserID, dtVesselType, PAGE_SIZE, PAGE_INDEX, ref SelectRecordCount, sortbycoloumn, sortdirection);
        }
        public static DataTable Get_UnAssigned_CrewList(int ManningOfficeID, int Nationality, int RankID, string AvailableFromDate, string AvailableToDate, string FreeText, int VesselID, int VesselIdOffSigner, int Available_Option, int UserID,int PAGE_SIZE, int PAGE_INDEX, ref int SelectRecordCount, string sortbycoloumn, int? sortdirection)
        {
            return DAL_Crew_CrewList.Get_UnAssigned_CrewList_DL(ManningOfficeID, Nationality, RankID, AvailableFromDate, AvailableToDate, FreeText, VesselID, VesselIdOffSigner, Available_Option, UserID, PAGE_SIZE, PAGE_INDEX, ref SelectRecordCount, sortbycoloumn, sortdirection);
        }
        public static DataTable Get_UnAssigned_CrewList(int ManningOfficeID, int Nationality, int RankID, string AvailableFromDate, string AvailableToDate, string FreeText, int VesselID, int Available_Option, int UserID, int PAGE_SIZE, int PAGE_INDEX, ref int SelectRecordCount)
        {
            return DAL_Crew_CrewList.Get_UnAssigned_CrewList_DL(ManningOfficeID, Nationality, RankID, AvailableFromDate, AvailableToDate, FreeText, VesselID, Available_Option, UserID, PAGE_SIZE, PAGE_INDEX, ref SelectRecordCount);
        }
        public static DataTable Get_UnAssigned_CrewList_History(string Staff_Code, int UserCompanyID)
        {
            return DAL_Crew_CrewList.Get_UnAssigned_CrewList_History_DL(Staff_Code, UserCompanyID);
        }

        public static DataTable Get_Crewlist_Index(DataTable dtFilters, int UserID, int PAGE_SIZE, int PAGE_INDEX, ref int SelectRecordCount)
        {
            return DAL_Crew_CrewList.Get_Crewlist_Index_DL(dtFilters, UserID, PAGE_SIZE, PAGE_INDEX, ref SelectRecordCount, "", 0);
        }

        public static DataTable Get_Crewlist_Index(DataTable dtFilters, int UserID, int PAGE_SIZE, int PAGE_INDEX, ref int SelectRecordCount, string sortbycoloumn, int? sortdirection)
        {
            return DAL_Crew_CrewList.Get_Crewlist_Index_DL(dtFilters, UserID, PAGE_SIZE, PAGE_INDEX, ref SelectRecordCount, sortbycoloumn, sortdirection);
        }
        public static DataTable Get_Crewlist_Index(DataTable dtFilters,DataTable dtVesselType, int UserID, int PAGE_SIZE, int PAGE_INDEX, ref int SelectRecordCount, string sortbycoloumn, int? sortdirection)
        {
            return DAL_Crew_CrewList.Get_Crewlist_Index_DL(dtFilters,dtVesselType, UserID, PAGE_SIZE, PAGE_INDEX, ref SelectRecordCount, sortbycoloumn, sortdirection);
        }
        public static DataSet Get_Crewlist_IconView(int VESSEL_ID, int iUserID)
        {
            try
            {
                return DAL_Crew_CrewList.Get_Crewlist_IconView_DL(VESSEL_ID, iUserID);
            }
            catch
            {
                throw;
            }
        }

        public static DataTable Get_Crewlist_OnSigners(string FilterText, int VesselID, int iUserID, int Event_VesselID, int PAGE_SIZE, int PAGE_INDEX, ref int SelectRecordCount)
        {
            try
            {

                return DAL_Crew_CrewList.Get_Crewlist_OnSigners_DL(FilterText, VesselID, iUserID, Event_VesselID,  PAGE_SIZE, PAGE_INDEX, ref SelectRecordCount);
            }
            catch
            {
                throw;
            }
        }
        public static DataTable Get_Crewlist_OnSigners(string FilterText, int VesselID, int iUserID, int Event_VesselID, int EventID, int PAGE_SIZE, int PAGE_INDEX, ref int SelectRecordCount)
        {
            try
            {

                return DAL_Crew_CrewList.Get_Crewlist_OnSigners_DL(FilterText, VesselID, iUserID, Event_VesselID, EventID, PAGE_SIZE, PAGE_INDEX, ref SelectRecordCount);
            }
            catch
            {
                throw;
            }
        }
        public static DataTable Get_Crewlist_OnSigners(string FilterText, int VesselID, int iUserID, int Event_VesselID, int EventID, int PAGE_SIZE, int PAGE_INDEX, ref int SelectRecordCount, DataTable dtVesselTypes)
        {
            try
            {

                return DAL_Crew_CrewList.Get_Crewlist_OnSigners_DL(FilterText, VesselID, iUserID, Event_VesselID, EventID, PAGE_SIZE, PAGE_INDEX, ref SelectRecordCount, dtVesselTypes);
            }
            catch
            {
                throw;
            }
        }

        public static DataTable Get_Crewlist_OffSigners(int VesselID, string FilterText, int iUserID, int PAGE_SIZE, int PAGE_INDEX, ref int SelectRecordCount)
        {
            try
            {
                return DAL_Crew_CrewList.Get_Crewlist_OffSigners_DL(VesselID, FilterText, iUserID, PAGE_SIZE, PAGE_INDEX, ref SelectRecordCount);
            }
            catch
            {
                throw;
            }
        }

        public static DataTable Get_Crewlist_Export(DataTable dtFilters, DataTable dtExportColumns, int UserID)
        {
            try
            {
                return DAL_Crew_CrewList.Get_Crewlist_Export_DL(dtFilters, dtExportColumns, UserID);
            }
            catch
            {
                throw;
            }
        }

        public DataTable Get_CrewByCode_Name_Rank(string Search_Text)
        {
            try
            {
                if (Search_Text == null)
                    Search_Text = "";
                return DAL_Crew_CrewList.Get_CrewByCode_Name_Rank(Search_Text);
            }
            catch
            {
                throw;
            }
        }

        public static DataSet Get_Crewlist_History(int vesselid, int countryid, int rankid, DateTime SignOffFromDate, DateTime SignOffToDate, int PAGE_SIZE, int PAGE_INDEX, int PAGE_SIZE1, int PAGE_INDEX1, ref int SelectRecordCount, ref int SelectRecordCount1)
        {
            return DAL_Crew_CrewList.Get_Crewlist_History(vesselid, countryid, rankid, SignOffFromDate, SignOffToDate, PAGE_SIZE, PAGE_INDEX, PAGE_SIZE1, PAGE_INDEX1, ref SelectRecordCount, ref SelectRecordCount1);
        }
        public static DataTable Get_Super_Att_Vessel_Report(int FleetCode, int Vessel_ID, string SignOn_From, string SignOn_To, string SearchString, int UserID, int PAGE_SIZE, int PAGE_INDEX, ref int SelectRecordCount, ref int TotalDays)
        {
            return DAL_Crew_CrewList.Get_Super_Att_Vessel_Report_DL(FleetCode, Vessel_ID, SignOn_From, SignOn_To, SearchString, UserID, PAGE_SIZE, PAGE_INDEX, ref SelectRecordCount, ref TotalDays);
        }

        public static DataTable Get_Crew_Birthday_Calendar(int UserID)
        {
            return DAL_Crew_CrewList.Get_Crew_Birthday_Calendar_DL(UserID);
        }
        public static DataTable Get_HandOver_Search(int? FleetCode, int? Vessel_ID, string SearchString, int? Rank, string SortBy, int? SortDirection, int? PageSize, int? PageNumber, ref int RecordCount)
        {
            return DAL_Crew_CrewList.Get_HandOver_Search(FleetCode, Vessel_ID, SearchString, Rank, SortBy, SortDirection, PageSize, PageNumber, ref RecordCount);

        }

        public static DataSet Get_HandOverDetail_Search(int HandoverID, int Vessel_ID, string SearchString, string SortBy, int? SortDirection, int PageSize, int PageNumber, ref int RecordCount)
        {
            return DAL_Crew_CrewList.Get_HandOverDetail_Search(HandoverID, Vessel_ID, SearchString, SortBy, SortDirection, PageSize, PageNumber, ref RecordCount);

        }
        public static DataSet Get_ChechList_Search(int HandoverID, int Vessel_ID, string SearchString, string SortBy, int? SortDirection, int PageSize, int PageNumber, ref int RecordCount)
        {
            return DAL_Crew_CrewList.Get_ChechList_Search(HandoverID, Vessel_ID, SearchString, SortBy, SortDirection, PageSize, PageNumber, ref RecordCount);

        }
        public static DataTable Get_CrewMatrix_Report(int VesselID, int? VesselType, int? CompanyID)
        {
            return DAL_Crew_CrewList.Get_CrewMatrix_Report( VesselID,  VesselType,  CompanyID);

        }
        public static DataTable Get_VesselForCrewMatrix(int CompanyID)
        {
            return DAL_Crew_CrewList.Get_VesselForCrewMatrix( CompanyID);

        }
        public static DataSet Get_VesselTypeForCrewMatrix(int VesselID)
        {
            return DAL_Crew_CrewList.Get_VesselTypeForCrewMatrix( VesselID);

        }
        public static DataTable Get_AdminCrewlist(string SearchText, int ManningOfficeId, int UserID, int PAGE_SIZE, int PAGE_INDEX, ref int SelectRecordCount)
        {
            return DAL_Crew_CrewList.Get_AdminCrewlist_DL(SearchText, ManningOfficeId, UserID, PAGE_SIZE, PAGE_INDEX, ref SelectRecordCount);
        }

        /// <summary>
        /// Get Details for Crew MAtirx
        /// </summary>
        /// <param name="VesselID"></param>
        /// <param name="EventID"></param>
        /// <param name="OilMajorId"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DataSet CRW_DTL_CM_GetCrewMatrix(int VesselID,int EventID,int OilMajorId,string date)
        {
            return DAL_Crew_CrewList.CRW_DTL_CM_GetCrewMatrix(VesselID,EventID,OilMajorId,date);
        }
        public static DataTable Get_UnAssigned_CrewList(int ManningOfficeID, int Nationality, int RankID, string AvailableFromDate, string AvailableToDate, string FreeText, int VesselID, int VesselIdOffSigner, int Available_Option, int UserID, int PAGE_SIZE, int PAGE_INDEX, ref int SelectRecordCount, string sortbycoloumn, int? sortdirection, int YearsInOperator, int YearsInRank, int YearsInAllTypeTanker, DataTable dtVesselTypes)
        {
            return DAL_Crew_CrewList.Get_UnAssigned_CrewList_DL(ManningOfficeID, Nationality, RankID, AvailableFromDate, AvailableToDate, FreeText, VesselID, VesselIdOffSigner, Available_Option, UserID, PAGE_SIZE, PAGE_INDEX, ref SelectRecordCount, sortbycoloumn, sortdirection, YearsInOperator, YearsInRank, YearsInAllTypeTanker, dtVesselTypes);
        }
    }


}
