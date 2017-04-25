using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace SMS.Data.Crew
{
    public class DAL_Crew_CrewList
    {
        private static string connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
        static IFormatProvider iFormatProvider = new System.Globalization.CultureInfo("en-GB", true);

        //public DAL_Crew_CrewList(string ConnectionString)
        //{
        //    connection = ConnectionString;
        //}

        //public DAL_Crew_CrewList()
        //{
        //    connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
        //}

        public static DataTable Get_CrewListBySearchParam_DL(string SQL)
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.Text, SQL).Tables[0];
        }

        public static DataTable Get_Crewlist_Index_DL(DataTable dtFilters, int UserID, int PAGE_SIZE, int PAGE_INDEX, ref int SelectRecordCount, string sortbycoloumn, int? sortdirection)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            {
                new SqlParameter("dtFilters",dtFilters),
                new SqlParameter("@UserID",UserID),
                new SqlParameter("@PAGE_SIZE", PAGE_SIZE),
                new SqlParameter("@PAGE_INDEX",PAGE_INDEX),
                new SqlParameter("@ORDER_BY",sortbycoloumn),
                new SqlParameter("@SORT_DIRECTION",sortdirection),
                new SqlParameter("@SelectRecordCount",SelectRecordCount)
            };
            sqlprm[0].SqlDbType = SqlDbType.Structured;
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.InputOutput;

            DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_CRW_Get_Crewlist_Index", sqlprm);
            SelectRecordCount = Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);

            return ds.Tables[0];
        }
        public static DataTable Get_Crewlist_Index_DL(DataTable dtFilters, DataTable dtVesselType, int UserID, int PAGE_SIZE, int PAGE_INDEX, ref int SelectRecordCount, string sortbycoloumn, int? sortdirection)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            {
                new SqlParameter("dtFilters",dtFilters),
                new SqlParameter("dtVesselTypes",dtVesselType),
                new SqlParameter("@UserID",UserID),
                new SqlParameter("@PAGE_SIZE", PAGE_SIZE),
                new SqlParameter("@PAGE_INDEX",PAGE_INDEX),
                new SqlParameter("@ORDER_BY",sortbycoloumn),
                new SqlParameter("@SORT_DIRECTION",sortdirection),
                new SqlParameter("@SelectRecordCount",SelectRecordCount)
            };
            sqlprm[0].SqlDbType = SqlDbType.Structured;
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.InputOutput;

            DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_CRW_Get_Crewlist_Index", sqlprm);
            SelectRecordCount = Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);

            return ds.Tables[0];
        }
        public static DataTable Get_UnAssigned_CrewList_History_DL(string Staff_Code, int UserCompanyID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Staff_Code",Staff_Code),
                                            new SqlParameter("@UserCompanyID",UserCompanyID)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "Get_UnAssigned_CrewList_History", sqlprm).Tables[0];
        }

        public static DataSet Get_Crewlist_IconView_DL(int Vessel_ID, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@VesselID",Vessel_ID),
                                            new SqlParameter("@UserID",UserID)
                                        };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_CRW_Get_Crewlist_IconView", sqlprm);
        }

        public static DataTable Get_Crewlist_OnSigners_DL(string FilterText, int VesselID, int iUserID, int Event_VesselID, int PAGE_SIZE, int PAGE_INDEX, ref int SelectRecordCount)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Filter",'%'+FilterText.ToUpper() +'%'),
                                            new SqlParameter("@VesselID",VesselID),                                            
                                            new SqlParameter("@UserID",iUserID),
                                            new SqlParameter("@Event_VesselID",Event_VesselID),
                                            new SqlParameter("@PAGE_SIZE",PAGE_SIZE),
                                            new SqlParameter("@PAGE_INDEX",PAGE_INDEX),
                                            new SqlParameter("@SelectRecordCount", SelectRecordCount)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.InputOutput;

            DataTable dt = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_CRW_Get_Crewlist_OnSigners", sqlprm).Tables[0];
            SelectRecordCount = Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
            return dt;
        }
        public static DataTable Get_Crewlist_OnSigners_DL(string FilterText, int VesselID, int iUserID, int Event_VesselID, int EventID, int PAGE_SIZE, int PAGE_INDEX, ref int SelectRecordCount)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Filter",'%'+FilterText.ToUpper() +'%'),
                                            new SqlParameter("@VesselID",VesselID),                                            
                                            new SqlParameter("@UserID",iUserID),
                                            new SqlParameter("@Event_VesselID",Event_VesselID),
                                            new SqlParameter("@EventID",EventID),
                                            new SqlParameter("@PAGE_SIZE",PAGE_SIZE),
                                            new SqlParameter("@PAGE_INDEX",PAGE_INDEX),
                                            new SqlParameter("@SelectRecordCount", SelectRecordCount)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.InputOutput;

            DataTable dt = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_Get_Crewlist_OnSigners", sqlprm).Tables[0];
            SelectRecordCount = Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
            return dt;
        }
        public static DataTable Get_Crewlist_OnSigners_DL(string FilterText, int VesselID, int iUserID, int Event_VesselID, int EventID, int PAGE_SIZE, int PAGE_INDEX, ref int SelectRecordCount,DataTable  dtVesselTypes)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Filter",'%'+FilterText.ToUpper() +'%'),
                                            new SqlParameter("@VesselID",VesselID),                                            
                                            new SqlParameter("@UserID",iUserID),
                                            new SqlParameter("@Event_VesselID",Event_VesselID),
                                            new SqlParameter("@EventID",EventID),
                                            new SqlParameter("@dtVesselTypes",dtVesselTypes),
                                            new SqlParameter("@PAGE_SIZE",PAGE_SIZE),
                                            new SqlParameter("@PAGE_INDEX",PAGE_INDEX),
                                            new SqlParameter("@SelectRecordCount", SelectRecordCount)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.InputOutput;

            DataTable dt = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_CRW_Get_Crewlist_OnSigners", sqlprm).Tables[0];
            SelectRecordCount = Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
            return dt;
        }

        public static DataTable Get_Crewlist_OffSigners_DL(int VesselID, string FilterText, int iUserID, int PAGE_SIZE, int PAGE_INDEX, ref int SelectRecordCount)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Filter",'%'+FilterText.ToUpper() +'%'),
                                            new SqlParameter("@VesselID",VesselID),
                                            new SqlParameter("@UserID",iUserID),                                            
                                            new SqlParameter("@PAGE_SIZE",PAGE_SIZE),
                                            new SqlParameter("@PAGE_INDEX",PAGE_INDEX),
                                            new SqlParameter("@SelectRecordCount", SelectRecordCount)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.InputOutput;
            DataTable dt = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_CRW_Get_Crewlist_OffSigners", sqlprm).Tables[0];
            SelectRecordCount = Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
            return dt;
        }
        public static DataTable Get_CrewList_Allotment_DL(int Vessel_ID, string FreeText, string PBill_Date, int UserID, int PAGE_SIZE, int PAGE_INDEX, ref int SelectRecordCount)
        {
            DateTime Dt_PBill_Date = DateTime.Parse("1900/01/01");
            if (PBill_Date.Length > 0)
                Dt_PBill_Date = DateTime.Parse(PBill_Date, iFormatProvider);

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Vessel_ID",Vessel_ID),
                                            new SqlParameter("@PBill_Date",Dt_PBill_Date),
                                            new SqlParameter("@FreeText",FreeText),
                                            new SqlParameter("@UserID",UserID),
                                            new SqlParameter("@PAGE_SIZE",PAGE_SIZE),
                                            new SqlParameter("@PAGE_INDEX",PAGE_INDEX),
                                            new SqlParameter("@SelectRecordCount",SelectRecordCount)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.InputOutput;
            DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_CRW_Get_CrewList_Allotment", sqlprm);

            SelectRecordCount = Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
            return ds.Tables[0];
        }

        public static DataTable Get_SigningOff_CrewList_DL(int FleetCode, int VesselID, int RankID, int Nationality, string SignOffFromDate, string SignOffToDate, string FreeText, int UserID, int PAGE_SIZE, int PAGE_INDEX, ref int SelectRecordCount)
        {
            DateTime Dt_SignOffFromDate = DateTime.Parse("1900/01/01");
            if (SignOffFromDate.Length > 0)
                Dt_SignOffFromDate = DateTime.Parse(SignOffFromDate, iFormatProvider);

            DateTime Dt_SignOffToDate = DateTime.Parse("2999/01/01");
            if (SignOffToDate.Length > 0)
                Dt_SignOffToDate = DateTime.Parse(SignOffToDate, iFormatProvider);

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@FleetCode",FleetCode),
                                            new SqlParameter("@VesselID",VesselID),
                                            new SqlParameter("@RankID",RankID),
                                            new SqlParameter("@Nationality",Nationality),
                                            new SqlParameter("@SignOffFromDate",Dt_SignOffFromDate),
                                            new SqlParameter("@SignOffToDate",Dt_SignOffToDate),
                                            new SqlParameter("@FreeText",FreeText),
                                            new SqlParameter("@UserID",UserID),
                                            new SqlParameter("@PAGE_SIZE",PAGE_SIZE),
                                            new SqlParameter("@PAGE_INDEX",PAGE_INDEX),
                                            new SqlParameter("@SelectRecordCount",SelectRecordCount)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.InputOutput;
            DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_CRW_Get_SigningOff_CrewList", sqlprm);

            SelectRecordCount = Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
            return ds.Tables[0];
        }

        public static DataTable Get_DetailCrewAssignmentList_DL(int FleetCode, int VesselID, int RankID, int Nationality, string SignOffFromDate, string SignOffToDate, string FreeText, int UserID, int PAGE_SIZE, int PAGE_INDEX, ref int SelectRecordCount)
        {
            DateTime Dt_SignOffFromDate = DateTime.Parse("1900/01/01");
            if (SignOffFromDate.Length > 0)
                Dt_SignOffFromDate = DateTime.Parse(SignOffFromDate, iFormatProvider);

            DateTime Dt_SignOffToDate = DateTime.Parse("2999/01/01");
            if (SignOffToDate.Length > 0)
                Dt_SignOffToDate = DateTime.Parse(SignOffToDate, iFormatProvider);

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@FleetCode",FleetCode),
                                            new SqlParameter("@VesselID",VesselID),
                                            new SqlParameter("@RankID",RankID),
                                            new SqlParameter("@Nationality",Nationality),
                                            new SqlParameter("@SignOffFromDate",Dt_SignOffFromDate),
                                            new SqlParameter("@SignOffToDate",Dt_SignOffToDate),
                                            new SqlParameter("@FreeText",FreeText),
                                            new SqlParameter("@UserID",UserID),
                                            new SqlParameter("@PAGE_SIZE",PAGE_SIZE),
                                            new SqlParameter("@PAGE_INDEX",PAGE_INDEX),
                                            new SqlParameter("@SelectRecordCount",SelectRecordCount)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.InputOutput;
            DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_CRW_Get_SigningOffCrewList", sqlprm);

            SelectRecordCount = Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
            return ds.Tables[0];
        }
        public static DataTable Get_UnAssigned_CrewList_DL(int ManningOfficeID, int Nationality, int RankID, string AvailableFromDate, string AvailableToDate, string FreeText, int VesselID, int VesselIdOffSigner, int Available_Option, int UserID,DataTable dtVesselType, int PAGE_SIZE, int PAGE_INDEX, ref int SelectRecordCount, string sortbycoloumn, int? sortdirection)
        {
            DateTime Dt_AvailableFromDate = DateTime.Parse("1900/01/01");
            if (AvailableFromDate.Length > 0)
                Dt_AvailableFromDate = DateTime.Parse(AvailableFromDate, iFormatProvider);

            DateTime Dt_AvailableToDate = DateTime.Parse("2999/01/01");
            if (AvailableToDate.Length > 0)
                Dt_AvailableToDate = DateTime.Parse(AvailableToDate, iFormatProvider);

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ManningOfficeID",ManningOfficeID),
                                            new SqlParameter("@Nationality",Nationality),
                                            new SqlParameter("@RankID",RankID),
                                            new SqlParameter("@AvailableFromDate",Dt_AvailableFromDate),
                                            new SqlParameter("@AvailableToDate",Dt_AvailableToDate),
                                            new SqlParameter("@FreeText",FreeText),
                                            new SqlParameter("@VesselID",VesselID),
                                            new SqlParameter("@VesselID_OffSignner",VesselIdOffSigner),
                                            new SqlParameter("@Available_Option",Available_Option),
                                            new SqlParameter("@UserID",UserID),
                                             new SqlParameter("@dtVesselType",dtVesselType),
                                            new SqlParameter("@PAGE_SIZE",PAGE_SIZE),
                                            new SqlParameter("@PAGE_INDEX",PAGE_INDEX),
                                             new SqlParameter("@ORDER_BY",sortbycoloumn),
                                            new SqlParameter("@SORT_DIRECTION",sortdirection),
                                            new SqlParameter("@SelectRecordCount",SelectRecordCount)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.InputOutput;
            DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_CRW_Get_UnAssignedCrewList", sqlprm);

            SelectRecordCount = Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
            return ds.Tables[0];
        }
        public static DataTable Get_UnAssigned_CrewList_DL(int ManningOfficeID, int Nationality, int RankID, string AvailableFromDate, string AvailableToDate, string FreeText, int VesselID, int VesselIdOffSigner, int Available_Option, int UserID, int PAGE_SIZE, int PAGE_INDEX, ref int SelectRecordCount, string sortbycoloumn, int? sortdirection)
        {
            DateTime Dt_AvailableFromDate = DateTime.Parse("1900/01/01");
            if (AvailableFromDate.Length > 0)
                Dt_AvailableFromDate = DateTime.Parse(AvailableFromDate, iFormatProvider);

            DateTime Dt_AvailableToDate = DateTime.Parse("2999/01/01");
            if (AvailableToDate.Length > 0)
                Dt_AvailableToDate = DateTime.Parse(AvailableToDate, iFormatProvider);

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ManningOfficeID",ManningOfficeID),
                                            new SqlParameter("@Nationality",Nationality),
                                            new SqlParameter("@RankID",RankID),
                                            new SqlParameter("@AvailableFromDate",Dt_AvailableFromDate),
                                            new SqlParameter("@AvailableToDate",Dt_AvailableToDate),
                                            new SqlParameter("@FreeText",FreeText),
                                            new SqlParameter("@VesselID",VesselID),
                                            new SqlParameter("@VesselID_OffSignner",VesselIdOffSigner),
                                            new SqlParameter("@Available_Option",Available_Option),
                                            new SqlParameter("@UserID",UserID),
                                            new SqlParameter("@PAGE_SIZE",PAGE_SIZE),
                                            new SqlParameter("@PAGE_INDEX",PAGE_INDEX),
                                             new SqlParameter("@ORDER_BY",sortbycoloumn),
                                            new SqlParameter("@SORT_DIRECTION",sortdirection),
                                            new SqlParameter("@SelectRecordCount",SelectRecordCount)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.InputOutput;
            DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_CRW_Get_UnAssignedCrewList", sqlprm);

            SelectRecordCount = Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
            return ds.Tables[0];
        }
        public static DataTable Get_UnAssigned_CrewList_DL(int ManningOfficeID, int Nationality, int RankID, string AvailableFromDate, string AvailableToDate, string FreeText, int VesselID, int Available_Option, int UserID, int PAGE_SIZE, int PAGE_INDEX, ref int SelectRecordCount)
        {
            DateTime Dt_AvailableFromDate = DateTime.Parse("1900/01/01");
            if (AvailableFromDate.Length > 0)
                Dt_AvailableFromDate = DateTime.Parse(AvailableFromDate, iFormatProvider);

            DateTime Dt_AvailableToDate = DateTime.Parse("2999/01/01");
            if (AvailableToDate.Length > 0)
                Dt_AvailableToDate = DateTime.Parse(AvailableToDate, iFormatProvider);

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ManningOfficeID",ManningOfficeID),
                                            new SqlParameter("@Nationality",Nationality),
                                            new SqlParameter("@RankID",RankID),
                                            new SqlParameter("@AvailableFromDate",Dt_AvailableFromDate),
                                            new SqlParameter("@AvailableToDate",Dt_AvailableToDate),
                                            new SqlParameter("@FreeText",FreeText),
                                            new SqlParameter("@VesselID",VesselID),
                                            new SqlParameter("@Available_Option",Available_Option),
                                            new SqlParameter("@UserID",UserID),
                                            new SqlParameter("@PAGE_SIZE",PAGE_SIZE),
                                            new SqlParameter("@PAGE_INDEX",PAGE_INDEX),
                                            new SqlParameter("@SelectRecordCount",SelectRecordCount)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.InputOutput;
            DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_CRW_Get_UnAssigned_CrewList", sqlprm);

            SelectRecordCount = Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
            return ds.Tables[0];
        }
        public static DataTable Get_Crewlist_Export_DL(DataTable dtFilters, DataTable dtExportColumns, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            {
                new SqlParameter("dtFilters",dtFilters),
                new SqlParameter("dtExportColumns",dtExportColumns),
                new SqlParameter("@UserID",UserID)
            };
            sqlprm[0].SqlDbType = SqlDbType.Structured;
            sqlprm[1].SqlDbType = SqlDbType.Structured;

            DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_CRW_Get_Crewlist_Export", sqlprm);
            return ds.Tables[0];
        }

        public static DataTable Get_CrewByCode_Name_Rank(string Search_Text)
        {
            SqlParameter sqlprm = new SqlParameter("@Search_Text", Search_Text);
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_TRV_Get_CrewDetailByName", sqlprm).Tables[0];
        }

        public static DataSet Get_Crewlist_History(int vesselid, int countryid, int rankid, DateTime SignOffFromDate, DateTime SignOffToDate, int PAGE_SIZE, int PAGE_INDEX, int PAGE_SIZE1, int PAGE_INDEX1, ref int SelectRecordCount, ref int SelectRecordCount1)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                {
                    new SqlParameter("@Vessel",vesselid),
                    new SqlParameter("@Nationality",countryid),
                    new SqlParameter("@Rankid",rankid) ,
                    new SqlParameter("@JoiningDateFrom",SignOffFromDate),
                    new SqlParameter("@JoiningDateTo",SignOffToDate),
                    new SqlParameter("@PAGE_SIZE", PAGE_SIZE),
                    new SqlParameter("@PAGE_INDEX",PAGE_INDEX),                    
                    new SqlParameter("@PAGE_SIZE1", PAGE_SIZE1),
                    new SqlParameter("@PAGE_INDEX1",PAGE_INDEX1),
                    new SqlParameter("@SelectRecordCount",SelectRecordCount),
                    new SqlParameter("@SelectRecordCount1",SelectRecordCount1)
                };
            sqlprm[sqlprm.Length - 2].Direction = ParameterDirection.InputOutput;
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.Output;

            DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_CRW_Get_Crewlist_History", sqlprm);
            SelectRecordCount = Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
            SelectRecordCount1 = Convert.ToInt32(sqlprm[sqlprm.Length - 2].Value);
            return ds;
        }
        public static DataTable Get_Super_Att_Vessel_Report_DL(int FleetCode, int Vessel_ID, string SignOn_From, string SignOn_To, string SearchString, int UserID, int PAGE_SIZE, int PAGE_INDEX, ref int SelectRecordCount, ref int TotalDays)
        {
            DateTime? dtSignOn_From = null;
            if (SignOn_From.Length > 0)
                dtSignOn_From = DateTime.Parse(SignOn_From, iFormatProvider);

            DateTime? dtSignOn_To = null;
            if (SignOn_To.Length > 0)
                dtSignOn_To = DateTime.Parse(SignOn_To, iFormatProvider);


            SqlParameter[] sqlprm = new SqlParameter[]
            {
                new SqlParameter("@FleetCode",FleetCode),
                new SqlParameter("@Vessel_ID",Vessel_ID),
                new SqlParameter("@SignOn_From", dtSignOn_From),
                new SqlParameter("@SignOn_To",dtSignOn_To),
                new SqlParameter("@SearchString",SearchString),
                new SqlParameter("@UserID",UserID),
                new SqlParameter("@PAGE_SIZE",PAGE_SIZE),
                new SqlParameter("@PAGE_INDEX",PAGE_INDEX),
                new SqlParameter("@SelectRecordCount",SelectRecordCount),
                new SqlParameter("@TotalDays",TotalDays)
            };
            sqlprm[sqlprm.Length - 2].Direction = ParameterDirection.InputOutput;
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.Output;

            DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_CRW_Get_Super_Att_Vessel_Report", sqlprm);

            SelectRecordCount = Convert.ToInt32(sqlprm[sqlprm.Length - 2].Value);
            TotalDays = Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);

            return ds.Tables[0];
        }

        public static DataTable Get_Crew_Birthday_Calendar_DL(int UserID)
        {
            SqlParameter sqlprm = new SqlParameter("@UserID", UserID);
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_CRW_Get_Crew_Birthday_Calendar", sqlprm).Tables[0];
        }
        public static DataTable Get_HandOver_Search(int? FleetCode, int? Vessel_ID, string SearchString, int? Rank, string SortBy, int? SortDirection, int? PageSize, int? PageNumber, ref int RecordCount)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
            {
                new SqlParameter("@FleetCode",FleetCode),
                new SqlParameter("@Vessel_ID",Vessel_ID),
                new SqlParameter("@SEARCHTEXT",SearchString),
                new SqlParameter("@Rank",Rank),
                new SqlParameter("@SORTBY", SortBy),
                new SqlParameter("@SORTDIRECTION",SortDirection),
                new SqlParameter("@PAGENUMBER",PageNumber),
                new SqlParameter("@PAGESIZE",PageSize),                
                new SqlParameter("@RecordCount",RecordCount) 
            };

            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.InputOutput;
            DataTable dt = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_GET_HANDOVER_SEARCH", sqlprm).Tables[0];
            RecordCount = Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
            return dt;
        }

        public static DataSet Get_HandOverDetail_Search(int HandoverID, int Vessel_ID, string SearchString, string SortBy, int? SortDirection, int PageSize, int PageNumber, ref int RecordCount)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
            {
                new SqlParameter("@HANDOVER_ID",HandoverID),
                new SqlParameter("@Vessel_ID",Vessel_ID),
                new SqlParameter("@SEARCHTEXT",SearchString),
                new SqlParameter("@SORTBY", SortBy),
                new SqlParameter("@SORTDIRECTION",SortDirection),
                new SqlParameter("@PAGENUMBER",PageNumber),
                new SqlParameter("@PAGESIZE",PageSize),                
                new SqlParameter("@RecordCount",RecordCount) 
            };

            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.InputOutput;

            DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_GET_HANDOVER_DETAIL_SEARCH", sqlprm);

            RecordCount = Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);


            return ds;
        }
        public static DataSet Get_ChechList_Search(int HandoverID, int Vessel_ID, string SearchString, string SortBy, int? SortDirection, int PageSize, int PageNumber, ref int RecordCount)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
            {
                new SqlParameter("@HANDOVER_ID",HandoverID),
                new SqlParameter("@Vessel_ID",Vessel_ID),
                new SqlParameter("@SEARCHTEXT",SearchString),
                new SqlParameter("@SORTBY", SortBy),
                new SqlParameter("@SORTDIRECTION",SortDirection),
                new SqlParameter("@PAGENUMBER",PageNumber),
                new SqlParameter("@PAGESIZE",PageSize),                
                new SqlParameter("@RecordCount",RecordCount) 
            };

            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.InputOutput;

            DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_GET_HANDOVER_CHECKLIST_SEARCH", sqlprm);

            RecordCount = Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);


            return ds;
        }
        public static DataTable Get_CrewMatrix_Report(int VesselID, int? VesselType, int? CompanyID)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
            {
                new SqlParameter("@VesselID",VesselID),
                new SqlParameter("@VesselType",VesselType),
                new SqlParameter("@CompanyID",CompanyID),
               // new SqlParameter("@CrewType", CrewType),
            };


            DataTable ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_CRW_Get_CrewMatrix_Report", sqlprm).Tables[0];

            return ds;
        }
        public static DataTable Get_VesselForCrewMatrix(int CompanyID)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
            {
                new SqlParameter("@CompanyID",CompanyID),
            };


            DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_CRW_Get_VesselForCrewMatrix", sqlprm);



            return ds.Tables[0];
        }
        public static DataSet Get_VesselTypeForCrewMatrix(int VesselID)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
            {
                new SqlParameter("@VesselID",VesselID),
            };


            DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_CRW_Get_VesselTypeForCrewMatrix", sqlprm);

            return ds;
        }
        public static DataTable Get_AdminCrewlist_DL(string SearchText, int ManningOfficeId, int UserID, int PAGE_SIZE, int PAGE_INDEX, ref int SelectRecordCount)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            {
                new SqlParameter("@SearchText",SearchText),
                 new SqlParameter("@ManningOfficeId",ManningOfficeId),
                new SqlParameter("@UserID",UserID),
                new SqlParameter("@PAGE_SIZE", PAGE_SIZE),
                new SqlParameter("@PAGE_INDEX",PAGE_INDEX),
                new SqlParameter("@SelectRecordCount",SelectRecordCount)
            };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.InputOutput;

            DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_CRW_GET_AdminCrewList", sqlprm);
            SelectRecordCount = Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);

            return ds.Tables[0];
        }

        public static DataSet CRW_DTL_CM_GetCrewMatrix(int VesselID, int EventID, int OilMajorId, string date)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            {
                new SqlParameter("@VesselId",VesselID),
                new SqlParameter("@EventID",EventID),
                new SqlParameter("@OilMajorId",OilMajorId),
                new SqlParameter("@Date", date),
            };
            DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "[CRW_DTL_CM_GetCrewMatrix]", sqlprm);
            return ds;
        }

        public static DataTable Get_UnAssigned_CrewList_DL(int ManningOfficeID, int Nationality, int RankID, string AvailableFromDate, string AvailableToDate, string FreeText, int VesselID, int VesselIdOffSigner, int Available_Option, int UserID, int PAGE_SIZE, int PAGE_INDEX, ref int SelectRecordCount, string sortbycoloumn, int? sortdirection, int YearsInOperator, int YearsInRank, int YearsInAllTypeTanker, DataTable dtVesselTypes)
        {
            DateTime Dt_AvailableFromDate = DateTime.Parse("1900/01/01");
            if (AvailableFromDate.Length > 0)
                Dt_AvailableFromDate = DateTime.Parse(AvailableFromDate, iFormatProvider);

            DateTime Dt_AvailableToDate = DateTime.Parse("2999/01/01");
            if (AvailableToDate.Length > 0)
                Dt_AvailableToDate = DateTime.Parse(AvailableToDate, iFormatProvider);

            SqlParameter[] sqlprm = new SqlParameter[]
            { 
                new SqlParameter("@ManningOfficeID",ManningOfficeID),
                new SqlParameter("@Nationality",Nationality),
                new SqlParameter("@RankID",RankID),
                new SqlParameter("@AvailableFromDate",Dt_AvailableFromDate),
                new SqlParameter("@AvailableToDate",Dt_AvailableToDate),
                new SqlParameter("@FreeText",FreeText),
                new SqlParameter("@VesselID",VesselID),
                new SqlParameter("@VesselID_OffSignner",VesselIdOffSigner),
                new SqlParameter("@Available_Option",Available_Option),
                new SqlParameter("@UserID",UserID),
                new SqlParameter("@YearsInOperator",YearsInOperator),
                new SqlParameter("@YearsInRank",YearsInRank),
                new SqlParameter("@YearsInAllTypeTanker",YearsInAllTypeTanker),
                new SqlParameter("@dtVesselTypes",dtVesselTypes),
                new SqlParameter("@PAGE_SIZE",PAGE_SIZE),
                new SqlParameter("@PAGE_INDEX",PAGE_INDEX),
                new SqlParameter("@ORDER_BY",sortbycoloumn),
                new SqlParameter("@SORT_DIRECTION",sortdirection),
                new SqlParameter("@SelectRecordCount",SelectRecordCount)
            };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.InputOutput;
            DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_CM_Get_UnAssignedCrewList", sqlprm);

            SelectRecordCount = Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
            return ds.Tables[0];
        }
   
        
    }
}
