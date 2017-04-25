using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

using SMS.Data.Crew;
using SMS.Properties;



namespace SMS.Data.Crew
{
    public class DAL_Crew_CrewDetails
    {
        private string connection = "";
        public DAL_Crew_CrewDetails(string ConnectionString)
        {
            connection = ConnectionString;
        }

        public DAL_Crew_CrewDetails()
        {
            connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
        }

        #region  - CREW LIST AND STAFF REMARKS -
        public DataTable Get_CrewListBySearchParam_DL(string SQL)
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.Text, SQL).Tables[0];
        }

        public DataTable Get_StaffStatusRemarks_DL(int CrewID, int UserCompanyID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewID",CrewID),
                                            new SqlParameter("@UserCompanyID",UserCompanyID)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_CRW_Get_StaffStatusRemarks", sqlprm).Tables[0];
        }
        #endregion

        #region  - CREW DETAILS - GET -

        public DataTable GetCrewIfExists_DL(string FirstName, string Surname, DateTime DOB, string PassportNo)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@FirstName",FirstName.ToUpper()),
                                            new SqlParameter("@Surname",Surname.ToUpper()),
                                            new SqlParameter("@DOB",DOB),
                                            new SqlParameter("@PassportNo",PassportNo)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_CRW_Get_Crew_IfExists", sqlprm).Tables[0];
        }

        public DataTable Get_Crewlist_Index_DL(string Staff_Name, string Staff_Code, string Crew_Status, DateTime FromJoinDate, DateTime ToJoinDate, int Nationality, int Vessel_ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@NAME",Staff_Name),
                                            new SqlParameter("@Staff_Code",Staff_Code),
                                            new SqlParameter("@CRWSTATUS",Crew_Status),
                                            new SqlParameter("@JOINFRMDT",FromJoinDate),
                                            new SqlParameter("@JOINTODT",ToJoinDate),
                                            new SqlParameter("@NATION",Nationality),
                                            new SqlParameter("@VESSEL",Vessel_ID)
                                        };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_CRW_Get_Crewlist_Index", sqlprm).Tables[0];
        }

        public DataTable Get_CrewPersonalDetailsByID_DL(int ID)
        {
            SqlParameter sqlprm = new SqlParameter("ID", ID);
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_CRW_Get_CrewDetailsByCrewID", sqlprm).Tables[0];
        }
        public DataTable Get_CrewPreJoiningExp_DL(int ID)
        {
            SqlParameter sqlprm = new SqlParameter("CrewID", ID);
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_CRW_Get_CrewPreJoiningExp", sqlprm).Tables[0];
        }
        public DataTable Get_PreJoiningExpCrewWise_DL(int ID)
        {
            SqlParameter sqlprm = new SqlParameter("ID", ID);
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_Get_CrewPreJoiningExp", sqlprm).Tables[0];
        }
        public DataTable Get_CrewVoyages_DL(int ID)
        {
            SqlParameter sqlprm = new SqlParameter("CrewID", ID);
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_CRW_Get_CrewVoyages", sqlprm).Tables[0];
        }
        public DataTable Get_VoyagesForSignOnEvent_DL(int CrewID, int Vessel_ID, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewID",CrewID),
                                            new SqlParameter("@Vessel_ID",Vessel_ID),                                            
                                            new SqlParameter("@UserID",UserID)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_CRW_Get_VoyagesForSignOnEvent", sqlprm).Tables[0];
        }
        public DataTable Get_VoyagesForSignOffEvent_DL(int CrewID, int Vessel_ID, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewID",CrewID),
                                            new SqlParameter("@Vessel_ID",Vessel_ID),                                            
                                            new SqlParameter("@UserID",UserID)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_CRW_Get_VoyagesForSignOffEvent", sqlprm).Tables[0];
        }
        public DataTable Get_CrewVoyages_DL(int ID, int VoyageID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewID",ID),
                                            new SqlParameter("@VoyID",VoyageID)                                            
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_CRW_Get_CrewVoyages", sqlprm).Tables[0];
        }
        public DataTable Get_CrewVoyages_DL(int ID, int VoyageID, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewID",ID),
                                            new SqlParameter("@VoyID",VoyageID),
                                            new SqlParameter("@UserID",UserID)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_CRW_Get_CrewVoyages", sqlprm).Tables[0];
        }

        public DataTable Get_CrewLastVoyage_DL(int CrewID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewID",CrewID)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_CRW_Get_CrewLastVoyage", sqlprm).Tables[0];
        }
        public DataTable Get_CrewWages_DL(int ID)
        {
            SqlParameter sqlprm = new SqlParameter("CrewID", ID);
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_CRW_Get_CrewWages", sqlprm).Tables[0];
        }
        public DataTable Get_CrewPassportAndSeamanDetails_DL(int CrewID)
        {
            SqlParameter sqlprm = new SqlParameter("CrewID", CrewID);
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_CRW_Get_CrewPassportAndSeamanDetails", sqlprm).Tables[0];
        }
        public DataTable Get_CrewPreviousContactDetails_DL(int CrewID)
        {
            SqlParameter sqlprm = new SqlParameter("CrewID", CrewID);
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_CRW_Get_CrewPreviousContactDetails", sqlprm).Tables[0];
        }

        public DataTable Get_CrewDocumentList_DL(int CrewID)
        {
            SqlParameter sqlprm = new SqlParameter("CrewID", CrewID);
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_DMS_Get_DocumentsByCrewID", sqlprm).Tables[0];
        }

        public DataTable Get_CrewDocumentList_DL(int CrewID, string FilterString)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewID",CrewID),
                                            new SqlParameter("@FilterString",FilterString)                                           
                                        };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "DMS_Get_AllDocumentsByCrewID", sqlprm).Tables[0];
        }

        //@isArchived added for DMS CR 11292
        public DataTable Get_CrewDocumentList_DL(int CrewID, string FilterString, int isArchived)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewID",CrewID),
                                            new SqlParameter("@FilterString",FilterString),
                                             new SqlParameter("@isArchived",isArchived)
                                        };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "DMS_Get_AllDocumentsByCrewID", sqlprm).Tables[0];
        }

        public DataTable Get_CrewDocumentList_DL(int CrewID, string FilterString, string DocTypeName)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewID",CrewID),
                                            new SqlParameter("@FilterString",FilterString),
                                            new SqlParameter("@DocTypeName",DocTypeName)
                                        };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "DMS_Get_DocumentsByCrewID", sqlprm).Tables[0];
        }

        //@isArchived added for DMS CR 11292
        public DataTable Get_CrewDocumentList_DL(int CrewID, string FilterString, string DocTypeName, int isArchived)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewID",CrewID),
                                            new SqlParameter("@FilterString",FilterString),
                                            new SqlParameter("@DocTypeName",DocTypeName),
                                            new SqlParameter("@isArchived",isArchived)
                                        };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "DMS_Get_DocumentsByCrewID", sqlprm).Tables[0];
        }

        public DataTable Get_CrewDocumentList_DL(int CrewID, string FilterString, string DocTypeName,int VogId, int isArchived)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewID",CrewID),
                                            new SqlParameter("@FilterString",FilterString),
                                            new SqlParameter("@DocTypeName",DocTypeName),
                                            new SqlParameter("@isArchived",isArchived),
                                            new SqlParameter("@VogId",VogId)
                                        };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "DMS_Get_DocumentsByCrewID", sqlprm).Tables[0];
        }

        public DataTable Get_CrewDocumentDetailsByDocID_DL(int DocID)
        {
            SqlParameter sqlprm = new SqlParameter("DocID", DocID);
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_DMS_Get_DocumentDetailByDocID", sqlprm).Tables[0];
        }
        public DataTable Get_DocAttributeValueByDocID_DL(int DocID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@DocID",DocID)
                                        };
            DataTable dt = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_DMS_Get_DocAttributeValueByDocID", sqlprm).Tables[0];
            return dt;
        }
        public DataTable Get_Crew_DocAttributeValueForUpdate_DL(int DocID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@DocID",DocID)
                                        };
            DataTable dt = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_DMS_Get_Crew_DocAttributeValueForUpdate", sqlprm).Tables[0];
            return dt;
        }
        public DataTable Get_Crew_DependentsByCrewID_DL(int CrewID, int IsNOK)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewID",CrewID),
                                            new SqlParameter("@IsNOK",IsNOK)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_CRW_Get_Crew_DependentsByCrewID", sqlprm).Tables[0];
        }
        public DataSet Get_Crew_CompanyDetails(int? CompanyID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CompanyID",CompanyID),
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_CRW_Get_Crew_CompanyDetails", sqlprm);
        }
        public int IsMandatoryDocumentsUploaded_DL(int CrewID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]{
                new SqlParameter("CrewID", CrewID),                
                new SqlParameter("return",SqlDbType.Int)
            };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_DMS_IsMandatoryDocumentsUploaded", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        public DataTable Get_PlannedInterviewList_DL(int iCrewID, int iUserID, string InterviewType)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewID",iCrewID),
                                            new SqlParameter("@UserID",iUserID),
                                            new SqlParameter("@InterviewType",InterviewType)

                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_CRW_Get_PlannedInterviewList", sqlprm).Tables[0];
        }
        public DataTable Get_InterviewDetails_DL(int InterviewID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@InterviewID",InterviewID)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_CRW_Get_InterviewDetails", sqlprm).Tables[0];
        }
        public SqlDataReader Get_PlannedInterviewesForTheMonth_DL(int UserID, int CrewID, int Month, int Year, int ShowCalForAll)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@UserID",UserID),
                                            new SqlParameter("@CrewID",CrewID),
                                            new SqlParameter("@M",Month),
                                            new SqlParameter("@Y",Year),
                                            new SqlParameter("@ShowCalForAll",ShowCalForAll),
                                        };
            return SqlHelper.ExecuteReader(connection, CommandType.StoredProcedure, "SP_CRW_Get_PlannedInterviewesForTheMonth", sqlprm);
        }
        public DataTable Get_PlannedInterviewDetails_DL(int UserID, int CrewID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@UserID",UserID),
                                            new SqlParameter("@CrewID",CrewID)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_CRW_Get_PlannedInterviewDetailsForTheCrew", sqlprm).Tables[0];
        }
        public DataTable Get_InterviewesForTheCrewByUserDept_DL(int UserID, int CrewID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@UserID",UserID),
                                            new SqlParameter("@CrewID",CrewID)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_CRW_Get_InterviewesForTheCrewByUserDept", sqlprm).Tables[0];
        }

        public SqlDataReader Get_InterviewResultsForCrew_DL(int CrewID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewID",CrewID)
                                        };
            return SqlHelper.ExecuteReader(connection, CommandType.StoredProcedure, "SP_CRW_Get_InterviewResultsForCrew", sqlprm);
        }
        public SqlDataReader Get_UserAnswers_DL(int CrewID, int InterviewID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewID",CrewID),
                                            new SqlParameter("@InterviewID",InterviewID)
                                        };
            return SqlHelper.ExecuteReader(connection, CommandType.StoredProcedure, "SP_CRW_Get_UserAnswers", sqlprm);
        }
        public DataSet Get_InterviewerRecomendations_DL(int CrewID, int InterviewID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewID",CrewID),
                                            new SqlParameter("@InterviewID",InterviewID)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_CRW_Get_InterviewerRecomendations", sqlprm);
        }

       public SqlDataReader getInterviewsScheduledForToday_DL(int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@UserID",UserID)
                                        };
            return SqlHelper.ExecuteReader(connection, CommandType.StoredProcedure, "SP_CRW_Get_InterviewsScheduledForUser", sqlprm);
        }

        public SqlDataReader getPendingInterviewList_DL(int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@UserID",UserID)
                                        };
            return SqlHelper.ExecuteReader(connection, CommandType.StoredProcedure, "SP_CRW_Get_PendingInterviewList", sqlprm);
        }

        public SqlDataReader getPendingInterviewList_By_UserID_DL(int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@UserID",UserID)
                                        };
            
            return SqlHelper.ExecuteReader(connection, CommandType.StoredProcedure, "SP_CRW_Get_PendingInterviewList_By_UserID", sqlprm);
        }

        public DataTable Get_CrewChangeAlerts_DL(int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@UserID",UserID)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_CRW_Get_CrewChangeAlerts", sqlprm).Tables[0];
        }

        public DataTable Get_ManningAgentList_DL(int UserCompanyID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]{
                    new SqlParameter("@UserCompanyID", UserCompanyID)
            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_INF_Get_ManningOfficeList", sqlprm).Tables[0];
        }
        public DataTable Get_VesselOwnerList_DL(int UserCompanyID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]{
                    new SqlParameter("@UserCompanyID", UserCompanyID)
            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_INF_Get_VesselOwnerList", sqlprm).Tables[0];
        }
        public DataTable Get_VesselManagerList_DL(int UserCompanyID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]{
                    new SqlParameter("@UserCompanyID", UserCompanyID)
            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_INF_Get_VesselManagerList", sqlprm).Tables[0];
        }

        public DataSet Get_DocumentExpiryList_DL(int UserID)
        {
            SqlParameter sqlprm = new SqlParameter("@UserID", UserID);
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_DMS_Get_DocExpiryList", sqlprm);
        }

        public DataTable ExecuteQuery(string SQL)
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.Text, SQL).Tables[0];
        }
        public DataTable ExecuteQuery(string SQLCommandText, SqlParameter[] sqlprm)
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.Text, SQLCommandText, sqlprm).Tables[0];
        }
        public DataSet ExecuteDataset(string SQLCommandText, SqlParameter[] sqlprm)
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.Text, SQLCommandText, sqlprm);
        }

        public DataTable Get_CrewAssignments_DL(int VesselID, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@VesselID",VesselID),
                                            new SqlParameter("@UserID",UserID),
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_CRW_Get_CrewAssignmentsByVessel_Pending", sqlprm).Tables[0];
        }
        public DataTable Get_CrewAssignments_DL(int VesselID, int RankID, string SearchText, int Nationality, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@VesselID",VesselID),
                                            new SqlParameter("@RankID",RankID),
                                            new SqlParameter("@SearchText",SearchText),
                                            new SqlParameter("@Nationality",Nationality),
                                            new SqlParameter("@UserID",UserID)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_CRW_Get_CrewAssignmentsByVessel", sqlprm).Tables[0];
        }

        public DataTable Get_EventDetails_DL(int EventID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@EventID",EventID)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_CRW_Get_EventDetails", sqlprm).Tables[0];
        }

        public DataSet Get_CrewChangeEvents_DL(int FleetID, int VesselID, int EventStatus, int PortID, DateTime Event_From_Date, DateTime Event_To_Date, string SearchText, int SessionUser)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@FleetID",FleetID),
                                            new SqlParameter("@VesselID",VesselID),
                                            new SqlParameter("@EventStatus",EventStatus),
                                            new SqlParameter("@PortID",PortID),
                                            new SqlParameter("@Event_From_Date",Event_From_Date),
                                            new SqlParameter("@Event_To_Date",Event_To_Date),
                                            new SqlParameter("@SearchText",SearchText),
                                            new SqlParameter("@SessionUser",SessionUser)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_CRW_Get_CrewChangeEvents", sqlprm);
        }
        public DataSet Get_CrewChangeEvents_ByVessel_DL(int VesselID, int SessionUser)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@VesselID",VesselID),
                                            new SqlParameter("@SessionUser",SessionUser),
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_CRW_Get_CrewChangeEvents_ByVessel", sqlprm);
        }
        public DataSet Get_CrewChangeEvent_DL(int EventID, int SessionUser)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@EventID",EventID),
                                            new SqlParameter("@SessionUser",SessionUser),
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_CRW_Get_CrewChangeEvent", sqlprm);
        }
        public DataSet Get_CrewChangeEvents_ByCrew_DL(int CrewID, int SessionUser)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewID",CrewID),
                                            new SqlParameter("@SessionUser",SessionUser),
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_CRW_Get_CrewChangeEvents_ByCrew", sqlprm);
        }
        public int CloseEvent_DL(int EventID, int SessionUser)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@EventID",EventID),
                                            new SqlParameter("@SessionUser",SessionUser),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_CRW_Close_Event", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public int DeleteEvent_DL(int EventID, int SessionUser)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@EventID",EventID),
                                            new SqlParameter("@SessionUser",SessionUser),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_CRW_Delete_Event", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public DataTable Get_CrewRemarks_DL(int CrewID, int UserID, int PAGE_SIZE, int PAGE_INDEX, ref int SelectRecordCount)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewID",CrewID),
                                            new SqlParameter("@UserID",UserID),
                                            new SqlParameter("@PAGE_SIZE",PAGE_SIZE),
                                            new SqlParameter("@PAGE_INDEX",PAGE_INDEX),
                                            new SqlParameter("@SelectRecordCount",SelectRecordCount)
                                        };

            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.InputOutput;

            DataTable dt = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_CRW_Get_CrewRemarks", sqlprm).Tables[0];
            SelectRecordCount = Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
            return dt;
        }

        public DataTable Get_CROO_Feedbacks_DL(int CrewID, int UserID, string SearchText, int PAGE_SIZE, int PAGE_INDEX, ref int SelectRecordCount)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewID",CrewID),
                                            new SqlParameter("@UserID",UserID),
                                            new SqlParameter("@SearchText",SearchText),
                                            new SqlParameter("@PAGE_SIZE",PAGE_SIZE),
                                            new SqlParameter("@PAGE_INDEX",PAGE_INDEX),
                                            new SqlParameter("@SelectRecordCount",SelectRecordCount)
                                        };

            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.InputOutput;

            DataTable dt = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_CRW_Get_CROO_Feedbacks", sqlprm).Tables[0];
            SelectRecordCount = Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
            return dt;
        }
        public DataTable Load_CrewActivityLog_DL(int CrewID, int UserID, int PAGE_SIZE, int PAGE_INDEX, ref int SelectRecordCount)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewID",CrewID),
                                            new SqlParameter("@UserID",UserID),
                                            new SqlParameter("@PAGE_SIZE",PAGE_SIZE),
                                            new SqlParameter("@PAGE_INDEX",PAGE_INDEX),
                                            new SqlParameter("@SelectRecordCount",SelectRecordCount)
                                        };

            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.InputOutput;

            DataTable dt = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_CRW_Get_CrewActivityLog", sqlprm).Tables[0];
            SelectRecordCount = Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
            return dt;
        }

        public DataTable Load_CrewTravelLog_DL(int CrewID, int UserID, int PAGE_SIZE, int PAGE_INDEX, ref int SelectRecordCount)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewID",CrewID),
                                            new SqlParameter("@UserID",UserID),
                                            new SqlParameter("@PAGE_SIZE",PAGE_SIZE),
                                            new SqlParameter("@PAGE_INDEX",PAGE_INDEX),
                                            new SqlParameter("@SelectRecordCount",SelectRecordCount)
                                        };

            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.InputOutput;

            DataTable dt = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_TRV_SP_Get_CrewTravelList", sqlprm).Tables[0];
            SelectRecordCount = Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
            return dt;
        }

        public DataTable Get_CrewJoiningChecklist_DL(int CrewID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewID",CrewID)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_CRW_Get_CrewJoiningChecklist", sqlprm).Tables[0];
        }

        public DataTable Get_Crew_DocumentChecklist_DL(int CrewID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewID",CrewID)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_CRW_Get_Crew_DocumentChecklist", sqlprm).Tables[0];
        }

        public DataTable Get_Crew_VoyageDocuments_DL(int CrewID, int VoyageID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewID",CrewID),
                                            new SqlParameter("@VoyageID",VoyageID)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_CRW_Get_Crew_VoyageDocuments", sqlprm).Tables[0];
        }

        public DataTable Get_Crew_PerpetualDocuments_DL(int CrewID, string SearchText, int DocumentGroupId)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewID",CrewID),
                                            new SqlParameter("@SearchText",SearchText),
                                            new SqlParameter("@DocumentGroupId",DocumentGroupId)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_CRW_Get_Crew_PerpetualDocuments", sqlprm).Tables[0];
        }
        public DataTable Get_Crew_PerpetualDocument_DL(int CrewID, int DocTypeId)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewID",CrewID),
                                            new SqlParameter("@DocumentTypeId",DocTypeId)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_Get_Crew_PerpetualDocument", sqlprm).Tables[0];
        }
        public DataTable Get_CrewAgreementData_DL(int CrewID, int VoyageID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewID",CrewID),
                                            new SqlParameter("@VoyageID",VoyageID)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_CRW_Get_CrewAgreementData", sqlprm).Tables[0];
        }

        public int Get_ONBD_Count_DL(int VesselID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@VesselID",VesselID),              
                                            new SqlParameter("return",SqlDbType.Int)
            };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_CRW_Get_ONBD_Count", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        public string Get_SEQAndONBD_DL(int VesselID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@VesselID",VesselID)
            };
            return SqlHelper.ExecuteScalar(connection, CommandType.StoredProcedure, "SP_CRW_Get_SEQ_ONBD_Count", sqlprm).ToString();
        }

        public DataTable Get_CrewNationality_DL(int SessionUserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@SessionUserID",SessionUserID)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_CRW_Get_NationalityList", sqlprm).Tables[0];
        }

        public string Get_VID_SEQ_DL(int VesselID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@VesselID",VesselID)
            };
            return SqlHelper.ExecuteScalar(connection, CommandType.StoredProcedure, "SP_CRW_Get_SEQ_ONBD_Count", sqlprm).ToString();


            //string sReturn = "";
            //string strConn = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
            //SqlConnection objCon = new SqlConnection(strConn);

            //string SQL = "select Answer_Text from VID_Master_Answers where Master_Question_ID = '00000068' and SMSLOG_VESSEL_ID = " + VesselID.ToString() ;
            //objCon.Open();


            //SqlCommand objCom = new SqlCommand(SQL, objCon);
            //SqlDataReader dr = objCom.ExecuteReader();

            //if (dr.Read())
            //{
            //    sReturn = dr[0].ToString();
            //}
            //return sReturn;
        }

        public DataTable Get_CrewNotificationDetails_DL(int ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ID",ID)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_CRW_Get_CrewNotificationDetails", sqlprm).Tables[0];
        }
        public DataSet Get_Crew_Mail_attachment(int CrewID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewID",CrewID),
                                            
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_DMS_Get_DocumentsByCrewID_Filtered", sqlprm);
        }


        public DataSet Get_MailDetailsForEventID_DL(int EventID, int EventTypeID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@EventID",EventID),
                                            new SqlParameter("@EventTypeID",EventTypeID)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_CRW_Get_MailDetailsForEventID", sqlprm);
        }

        public DataTable Get_Sign_Off_Reasons_DL()
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_CRW_Get_Sign_Off_Reasons").Tables[0];
        }

        public DataTable Get_Airport_Continents_DL(string SearchString)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Query",'%'+SearchString+'%')
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_CRW_Get_Airport_Continents", sqlprm).Tables[0];
        }
        public DataTable Get_Airport_Countries_DL(string Continent, string SearchString)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Query",'%'+SearchString+'%'),
                                            new SqlParameter("@Continent",'%'+Continent+'%')
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_CRW_Get_Airport_Countries", sqlprm).Tables[0];
        }
        public DataTable Get_Airport_Cities_DL(int Country, string SearchString)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Query",'%'+SearchString+'%'),
                                            new SqlParameter("@CountryID",Country)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_CRW_Get_Airport_Cities", sqlprm).Tables[0];
        }

        public DataTable Get_AirportList_DL(int AirportID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@AirportID",AirportID)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_CRW_Get_AirportList", sqlprm).Tables[0];

        }

        public DataTable Get_AirportList_DL(string SearchString)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Query",'%'+SearchString+'%')
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_CRW_Get_AirportList", sqlprm).Tables[0];

        }
        public DataTable Get_AirportList_DL(int Country, string SearchString)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Query",'%'+SearchString+'%'),
                                            new SqlParameter("@CountryID",Country)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_CRW_Get_AirportList", sqlprm).Tables[0];
        }
        public DataTable Get_AirportList_DL(string City, string SearchString)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Query",'%'+SearchString+'%'),
                                            new SqlParameter("@City",'%'+City+'%')
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_CRW_Get_AirportList", sqlprm).Tables[0];
        }
        public DataTable Get_AirportList_DL(int Country, string City, string SearchString)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CountryID",Country),
                                            new SqlParameter("@Query",'%'+SearchString+'%'),
                                            new SqlParameter("@City",'%'+City+'%')
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_CRW_Get_AirportList", sqlprm).Tables[0];
        }
        public DataTable Get_AirportList_DL(int Country, string City, string Iata_Code, string SearchString)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CountryID",Country),
                                            new SqlParameter("@City", City),
                                            new SqlParameter("@Iata_Code",Iata_Code),
                                            new SqlParameter("@Query",SearchString)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_CRW_Get_AirportList", sqlprm).Tables[0];
        }

        public DataTable Get_Crew_MissingData_DL(int CrewID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewID",CrewID)
                                        };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_CRW_Get_Crew_MissingData", sqlprm).Tables[0];
        }

        public int Get_RejectedInterviewCount_DL(int CrewID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewID",CrewID),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;

            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_CRW_Get_RejectedInterviewCount", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public DataTable Get_PortCall_List_DL(int Vessel_ID, DateTime EventDate)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Vessel_ID",Vessel_ID),
                                            new SqlParameter("@EventDate",EventDate)
                                        };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_CRW_Get_PortCall_List", sqlprm).Tables[0];
        }
        public DataTable Get_PortCall_List_DL(int Vessel_ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Vessel_ID",Vessel_ID)
                                        };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_CRW_Get_PortCall_List", sqlprm).Tables[0];
        }
        public DataTable Get_PortCall_Details_DL(int Port_Call_ID, int VesselID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Port_Call_ID",Port_Call_ID),
                                            new SqlParameter("@VesselID",VesselID)
                                        };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_CRW_Get_PortCall_Details", sqlprm).Tables[0];
        }
        public DataSet Get_CrewWorkflow_DL(int CrewID, int UserId)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewID",CrewID),
                                              new SqlParameter("@UserID",UserId)
                                        };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_CRW_Get_CrewWorkflow", sqlprm);
        }


        public DataTable Get_ContractTemplate_DL(int ContractId)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ContractId",ContractId)
                                        };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_CRW_Get_ContractTemplate", sqlprm).Tables[0];
        }
        public DataSet Get_MissingDataReport_DL(int Vessel_Manager, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Vessel_Manager",Vessel_Manager),
                                            new SqlParameter("@UserID",UserID)
                                        };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_RPT_SP_Get_ReportData", sqlprm);
        }
        public DataSet Get_MissingDataDetails_DL(int Vessel_Manager, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Vessel_Manager",Vessel_Manager),
                                            new SqlParameter("@UserID",UserID)
                                        };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_RPT_SP_Get_ReportDataDetails", sqlprm);
        }

        public DataSet Get_MissingDataDetails_DL(int Vessel_Manager, int UserID, int ManningOfficeID, string Col)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Vessel_Manager",Vessel_Manager),
                                            new SqlParameter("@UserID",UserID),
                                            new SqlParameter("@ManningOfficeID",ManningOfficeID),
                                            new SqlParameter("@Col",Col)
                                        };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_RPT_SP_Get_ReportDataDetails", sqlprm);
        }

        public DataSet Get_CrewComplaints_DL(int CrewID, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewID",CrewID),
                                            new SqlParameter("@UserID",UserID)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_CRW_Get_CrewComplaints", sqlprm);
        }
        public DataSet Get_CrewComplaintList_DL(int CrewID, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewID",CrewID),
                                            new SqlParameter("@UserID",UserID)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_CRW_Get_CrewComplaintList", sqlprm);

        }
        public DataSet Get_CrewComplaintLog_DL(int Worklist_ID, int Vessel_ID, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Worklist_ID",Worklist_ID),
                                            new SqlParameter("@Vessel_ID",Vessel_ID),
                                            new SqlParameter("@UserID",UserID)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_CRW_Get_CrewComplaintLog", sqlprm);
        }
        public DataSet Get_CrewCardStatus_DL(int CrewID, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewID",CrewID),
                                            new SqlParameter("@UserID",UserID)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_CRW_Get_CrewCardStatus", sqlprm);

        }
        public DataTable Get_CrewCardIndex_DL(int FlletCode, int VesselID, int Nationality, int ApprovalStatus, string SearchText, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Fleet",FlletCode),
                                            new SqlParameter("@Vessel",VesselID),
                                            new SqlParameter("@Nationality",Nationality),
                                            new SqlParameter("@Status",ApprovalStatus),
                                            new SqlParameter("@SearchText",SearchText),
                                            new SqlParameter("@UserID",UserID)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_CRW_Get_CrewCardIndex", sqlprm).Tables[0];

        }
        //2nd
        public DataTable Get_CrewCardIndex_DL(string SearchText, int? FleetCode, int? VesselID, int? Nationality, int? ApprovalStatus, int UserID, int UserCompanyID, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@SerchText", SearchText),
                   new System.Data.SqlClient.SqlParameter("@Fleet", FleetCode),
                   new System.Data.SqlClient.SqlParameter("@Vessel", VesselID),
                   new System.Data.SqlClient.SqlParameter("@Nationality", Nationality),
                   new System.Data.SqlClient.SqlParameter("@Status", ApprovalStatus),
                   new System.Data.SqlClient.SqlParameter("@UserID", UserID),
                   new System.Data.SqlClient.SqlParameter("@UserCompanyID", UserCompanyID),
                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                    
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_CRW_Get_CrewCardIndex_Search", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds.Tables[0];
        }
        public DataTable Get_CardAttachments_DL(int CardID, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CardID",CardID),
                                            new SqlParameter("@UserID",UserID)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_CRW_Get_CardAttachments", sqlprm).Tables[0];

        }
        public DataTable Get_CrewUniformSize_DL(int CrewID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewID",CrewID)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_CRW_Get_CrewUniformSize", sqlprm).Tables[0];

        }
        public DataTable Get_CrewUniformSize_DL(DataTable dtFilters, int UserID)
        {
            System.Data.SqlClient.SqlParameter[] sqlprm = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@dtFilters", dtFilters),
                   new System.Data.SqlClient.SqlParameter("@UserID",UserID),
            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_CRW_Get_AllCrewUniformSize", sqlprm).Tables[0];

        }
        public DataTable Get_CrewHeightWaistWeight_DL(int CrewID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewID",CrewID)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_CRW_Get_CrewHeightWaistWeight", sqlprm).Tables[0];

        }
        public DataSet Get_CrewInfo_ToolTip_DL(int CrewID, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewID",CrewID),
                                            new SqlParameter("@UserID",UserID)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_CRW_Get_CrewInfo_ToolTip", sqlprm);

        }
        public DataSet Get_TradeZoneList_DL(int RankID, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@RankID",RankID),
                                            new SqlParameter("@UserID",UserID)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_CRW_Get_TradeZoneList", sqlprm);

        }

        public DataTable Get_USVisaAlerts_DL(int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@UserID",UserID)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_CRW_Get_CrewUSVisaAlerts", sqlprm).Tables[0];
        }

        public DataTable Get_Transfer_Promotions_DL(int CrewID, int VoyageID, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewID",CrewID),
                                            new SqlParameter("@VoyageID",VoyageID),
                                            new SqlParameter("@UserID",UserID)
                                        };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_CRW_Get_Crew_Transfer_Promotions", sqlprm).Tables[0];
        }
        public int Delete_Transfer_Planning_DL(int Transfer_ID, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Transfer_ID",Transfer_ID),
                                            new SqlParameter("@UserID",UserID),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };

            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_CRW_Delete_Crew_Transfer_Promotions", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);

        }

        public DataTable Get_CrewAgreementRecords_DL(int CrewID, int VoyageID, int Stage, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewID",CrewID),
                                            new SqlParameter("@VoyID",VoyageID),
                                            new SqlParameter("@Stage",Stage),
                                            new SqlParameter("@UserID",UserID)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_CRW_Get_CrewAgreementRecords", sqlprm).Tables[0];
        }
        public DataTable Get_CrewAgreementRecords_DL(int ID, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ID",ID),
                                            new SqlParameter("@UserID",UserID)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_CRW_Get_CrewAgreementRecords", sqlprm).Tables[0];
        }
        public DataTable Get_CrewAgreementStatus_DL(int @VoyageID, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("VoyageID",@VoyageID),
                                            new SqlParameter("UserID",@UserID)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_CRW_Get_CrewAgreementStatus", sqlprm).Tables[0];
        }

        public DataTable Get_Contracts_ForDigiSign_Alerts_DL(int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@UserID",UserID)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_CRW_Get_Contracts_ForDigiSign_Alerts", sqlprm).Tables[0];
        }
        public DataTable Get_Contracts_ToVerify_Alerts_DL(int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@UserID",UserID)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_CRW_Get_Contracts_ToVerify_Alerts", sqlprm).Tables[0];
        }
        public DataTable Download_Contract_ByManningOffice_DL(int ID, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ID",ID),
                                            new SqlParameter("@UserID",UserID)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_CRW_Download_Agreement_ByMO", sqlprm).Tables[0];
        }

        public DataTable Get_Interviews_DL(int RankID, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@RankID",RankID),
                                            new SqlParameter("@UserID",UserID)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_CRW_Get_Interviews", sqlprm).Tables[0];

        }

        public DataTable Get_GradingList_DL()
        {

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_Get_Interview_GradingList").Tables[0];
        }
        public int INSERT_Grading_DL(string Grading_Name, int Grade_Type, int Min, int Max, int Divisions, int Created_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Grade_Name",Grading_Name),
                                            new SqlParameter("@Grade_Type",Grade_Type),
                                            new SqlParameter("@Min",Min),
                                            new SqlParameter("@Max",Max),
                                            new SqlParameter("@Divisions",Divisions),
                                            new SqlParameter("@Created_By",Created_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_EVA_SP_Insert_Grading", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public int UPDATE_Grading_DL(int ID, string Grading_Name, int Grading_Type, int Min, int Max, int Divisions, int Modified_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ID",ID),
                                            new SqlParameter("@Grade_Name",Grading_Name),
                                            new SqlParameter("@Grade_Type",Grading_Type),
                                            new SqlParameter("@Min",Min),
                                            new SqlParameter("@Max",Max),
                                            new SqlParameter("@Divisions",Divisions),
                                            new SqlParameter("@Modified_By",Modified_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_EVA_SP_Update_Grading", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public int DELETE_Grading_DL(int ID, int Deleted_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ID",ID),
                                            new SqlParameter("@Deleted_By",Deleted_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_EVA_SP_Delete_Grading", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        public DataTable Get_GradingOptions_DL(int Grade_ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Grade_ID",Grade_ID)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_EVA_SP_Get_GradingOptions", sqlprm).Tables[0];
        }
        public int INSERT_GradingOption_DL(int Grade_ID, string OptionText, decimal OptionValue, int Created_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Grade_ID",Grade_ID),
                                            new SqlParameter("@OptionText",OptionText),
                                            new SqlParameter("@OptionValue",OptionValue),                                            
                                            new SqlParameter("@Created_By",Created_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_EVA_SP_Insert_GradingOption", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public int UPDATE_GradingOption_DL(int Option_ID, string OptionText, decimal OptionValue, int Modified_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Option_ID",Option_ID),
                                            new SqlParameter("@OptionText",OptionText),
                                            new SqlParameter("@OptionValue",OptionValue),
                                            new SqlParameter("@Modified_By",Modified_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_EVA_SP_Update_GradingOption", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public int DELETE_GradingOptions_DL(int Grade_ID, int Deleted_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Grade_ID",Grade_ID),
                                            new SqlParameter("@Deleted_By",Deleted_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_EVA_SP_Delete_GradingOptions", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        public DataTable Get_CrewPreJoiningExp_FromInterview_DL(int CrewID)
        {
            SqlParameter sqlprm = new SqlParameter("CrewID", CrewID);
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_CRW_Get_CrewPreJoiningExp_FromInterview", sqlprm).Tables[0];
        }
        public DataTable Get_StaffSailingInfo_DL(int CrewID, int RankID, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewID",CrewID),
                                            new SqlParameter("@RankID",RankID),
                                            new SqlParameter("@UserID",UserID)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_CRW_Get_StaffSailingInfo", sqlprm).Tables[0];
        }
        public DataTable Get_StaffInfo_DL(int StaffCode)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@StaffCode",StaffCode)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_Get_StaffInfo", sqlprm).Tables[0];
        }
        public DataTable Get_SideLetter_Template_DL(int UserCompanyID, int TemplateID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@TemplateID",TemplateID),
                                            new SqlParameter("@UserCompanyID",UserCompanyID)
                                        };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_CRW_Get_SideLetter_Template", sqlprm).Tables[0];
        }
        public DataTable Get_SideLetter_ForVoyage_DL(int VoyageID, int CrewID, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@VoyageID",VoyageID),
                                            new SqlParameter("@CrewID",CrewID),
                                            new SqlParameter("@UserID",UserID)
                                        };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_CRW_Get_SideLetter_ForVoyage", sqlprm).Tables[0];
        }
        #endregion

        #region  - CREW DETAILS - INSERT -
        public int INS_NewCrewDetails_DL(CrewProperties objCrew)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Rank",objCrew.RankID),
                                            new SqlParameter("@AvailableDate",objCrew.Available_From_Date),
                                            new SqlParameter("@Staff_Surname",objCrew.Surname),
                                            new SqlParameter("@Staff_Name",objCrew.GivenName),                                            
                                            new SqlParameter("@Alias",objCrew.Alias),
                                            new SqlParameter("@Staff_Birth_Date",objCrew.DateOfBirth),
                                            new SqlParameter("@Staff_Born_Place",objCrew.PlaceofBirth),
                                            new SqlParameter("@Staff_Nationality",objCrew.Nationality),
                                            new SqlParameter("@MaritalStatus",objCrew.MaritalStatus),
                                            new SqlParameter("@Telephone",objCrew.Telephone),
                                            new SqlParameter("@Address",objCrew.Address),
                                            new SqlParameter("@Mobile",objCrew.Mobile),
                                            new SqlParameter("@Fax",objCrew.Fax),
                                            new SqlParameter("@EMail",objCrew.EMail),
                                            new SqlParameter("@NearestAirport",objCrew.NearestInternationalAirport),
                                            new SqlParameter("@NearestAirportID",objCrew.NearestInternationalAirportID),
                                            new SqlParameter("@Passport_Number",objCrew.Passport_Number),
                                            new SqlParameter("@Passport_Issue_Date",objCrew.Passport_Issue_Date),
                                            new SqlParameter("@Passport_Expiry_Date",objCrew.Passport_Expiry_Date),
                                            new SqlParameter("@Passport_PlaceOf_Issue",objCrew.Passport_PlaceOf_Issue), 
                                            new SqlParameter("@Seaman_Book_Number",objCrew.Seaman_Book_Number),
                                            new SqlParameter("@Seaman_Book_Issue_Date",objCrew.Seaman_Book_Issue_Date),
                                            new SqlParameter("@Seaman_Book_Expiry_Date",objCrew.Seaman_Book_Expiry_Date),
                                            new SqlParameter("@Seaman_Book_PlaceOf_Issue",objCrew.Seaman_Book_PlaceOf_Issue),  
                                            new SqlParameter("@Workedwith_Multinational_Crew",objCrew.Workedwith_Multinational_Crew),  
                                            new SqlParameter("@MultinationalCrew_Nationalities",objCrew.MultinationalCrew_Nationalities),  
                                            new SqlParameter("@ManningOfficeID",objCrew.ManningOfficeID),  
                                            new SqlParameter("@Us_Visa_Flag",objCrew.USVisa_Flag),                                            
                                            new SqlParameter("@Us_Visa_Number",objCrew.USVisa_Number),                                            
                                            new SqlParameter("@Us_Visa_Expiry",objCrew.USVisa_Expiry), 
                                            new SqlParameter("@Created_By",objCrew.Created_By), 
                                            new SqlParameter("@Allotment_AccType ",objCrew.Allotment_AccType ),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_CRW_Insert_CrewPersonalDetails", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);

        }

        public int INS_CrewPreJoiningExp_DL(int CrewID, string Vessel_Name, string Flag, string Vessel_Type, string DWT, int GRT, string CompanyName, int Rank, DateTime Date_From, DateTime Date_To, int Months, int Days, int Created_By, string ME_MakeModel, int ME_BHP, int? CompanyID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewID",CrewID),
                                            new SqlParameter("@Vessel_Name",Vessel_Name),
                                            new SqlParameter("@Flag",Flag),
                                            new SqlParameter("@Vessel_Type",Vessel_Type),
                                            new SqlParameter("@DWT",DWT),
                                            new SqlParameter("@GRT",GRT),
                                            new SqlParameter("@CompanyName",CompanyName),
                                            new SqlParameter("@Rank",Rank),
                                            new SqlParameter("@Date_From",Date_From),
                                            new SqlParameter("@Date_To",Date_To),
                                            new SqlParameter("@Months",Months),
                                            new SqlParameter("@Days",Days),
                                            new SqlParameter("@Created_By",Created_By),
                                            new SqlParameter("@ME_MakeModel",ME_MakeModel), 
                                            new SqlParameter("@ME_BHP",ME_BHP),
                                            new SqlParameter("@CompanyID",CompanyID),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_CRW_Insert_CrewPreJoiningExp", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);

        }
        public int INS_CrewVoyages_DL(int CrewID, int Joining_Type, DateTime Joining_Date, DateTime SignOn_Date, DateTime SignOffDate, int Joining_Rank, int Vessel_ID, DateTime COCDate, int Joining_Port, int Created_By, int RankScaleId, int ContractId, DateTime DOAHomePort, int VesselTypeAssignment)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewID",CrewID),
                                            new SqlParameter("@Joining_Type",Joining_Type),
                                            new SqlParameter("@Joining_Date",Joining_Date),
                                            new SqlParameter("@Sign_On_date",SignOn_Date),
                                            new SqlParameter("@SignOffDate",SignOffDate),
                                            new SqlParameter("@Joining_Rank",Joining_Rank),
                                            new SqlParameter("@Vessel_ID",Vessel_ID),
                                            new SqlParameter("@COCDate",COCDate),
                                            new SqlParameter("@Joining_Port",Joining_Port),
                                            new SqlParameter("@Created_By",Created_By),
                                            new SqlParameter("@RankScaleId",RankScaleId),
                                            new SqlParameter("@ContractId",ContractId),
                                            new SqlParameter("@DOAHomePort",DOAHomePort),
                                            new SqlParameter("@VesselTypeAssignment",VesselTypeAssignment),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_CRW_Insert_NewVoyage", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public int INS_CrewVoyages_DL(int CrewID, int Joining_Type, DateTime Joining_Date, DateTime SignOn_Date, DateTime SignOffDate, int Joining_Rank, int Vessel_ID, DateTime COCDate, int Joining_Port, int Created_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewID",CrewID),
                                            new SqlParameter("@Joining_Type",Joining_Type),
                                            new SqlParameter("@Joining_Date",Joining_Date),
                                            new SqlParameter("@Sign_On_date",SignOn_Date),
                                            new SqlParameter("@SignOffDate",SignOffDate),
                                            new SqlParameter("@Joining_Rank",Joining_Rank),
                                            new SqlParameter("@Vessel_ID",Vessel_ID),
                                            new SqlParameter("@COCDate",COCDate),
                                            new SqlParameter("@Joining_Port",Joining_Port),
                                            new SqlParameter("@Created_By",Created_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_CRW_Insert_NewVoyage", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public int INS_CrewWages_DL(int ID)
        {
            SqlParameter sqlprm = new SqlParameter("CrewID", ID);
            //return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_CRW_INS_CrewWages", sqlprm).Tables[0];
            return 1;
        }
        public int INS_CrewDocuments_DL(int CrewID, string DocumentName, string DocFileName, string DocFileExt, int DocTypeID, int Created_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            {   
                new SqlParameter("@CrewID",CrewID),
                new SqlParameter("@DocumentName",DocumentName),
                new SqlParameter("@DocFileName",DocFileName),
                new SqlParameter("@DocFileExt",DocFileExt),
                new SqlParameter("@DocTypeID",DocTypeID),
                new SqlParameter("@Created_By",Created_By),
                new SqlParameter("return",SqlDbType.Int)
            };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_DMS_Insert_Crew_Document", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public int INS_CrewDocuments_DL(int CrewID, int DocTypeID, string DocumentName, string DocFileName, string DocFileExt, int Created_By, string DocNo, DateTime IssueDate, string IssuePalce, DateTime ExpiryDate)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            {   
                new SqlParameter("@CrewID",CrewID),
                new SqlParameter("@DocumentName",DocumentName),
                new SqlParameter("@DocFileName",DocFileName),
                new SqlParameter("@DocFileExt",DocFileExt),
                new SqlParameter("@DocTypeID",DocTypeID),
                new SqlParameter("@Created_By",Created_By),
                new SqlParameter("@DocNo",DocNo),
                new SqlParameter("@IssueDate",IssueDate),
                new SqlParameter("@IssuePlace",IssuePalce),
                new SqlParameter("@ExpiryDate",ExpiryDate),
                new SqlParameter("return",SqlDbType.Int)
            };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_DMS_Insert_Crew_Document", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        public int INS_CrewDocuments_DL(int CrewID, int DocTypeID, string DocumentName, string DocFileName, string DocFileExt, int Created_By, string DocNo, DateTime IssueDate, string IssuePalce, DateTime ExpiryDate, int CountryOfIssue)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            {   
                new SqlParameter("@CrewID",CrewID),
                new SqlParameter("@DocumentName",DocumentName),
                new SqlParameter("@DocFileName",DocFileName),
                new SqlParameter("@DocFileExt",DocFileExt),
                new SqlParameter("@DocTypeID",DocTypeID),
                new SqlParameter("@Created_By",Created_By),
                new SqlParameter("@DocNo",DocNo),
                new SqlParameter("@IssueDate",IssueDate),
                new SqlParameter("@IssuePlace",IssuePalce),
                new SqlParameter("@ExpiryDate",ExpiryDate),
                new SqlParameter("@CountryOfIssue",CountryOfIssue),
                new SqlParameter("return",SqlDbType.Int)
            };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_DMS_Insert_Crew_Document", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public int INS_DocumentAttributeValues_DL(int DocID, int CrewID, int AttributeID, string AttributeValue_String, string AttributeDataType, int Modified_By)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@DocID",DocID),
                                            new SqlParameter("@CrewID",CrewID),
                                            new SqlParameter("@AttributeID",AttributeID),
                                            new SqlParameter("@AttributeValue",AttributeValue_String),                                            
                                            new SqlParameter("@AttributeDataType",AttributeDataType),
                                            new SqlParameter("@Modified_By",Modified_By),                                            
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_DMS_Insert_DocAttributeValue_String", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        public int INS_Crew_DependentDetails_DL(int CrewID, string FullName, string Relationship, string Address, string Phone, int IsNOK, int Created_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewID",CrewID),
                                            new SqlParameter("@FullName",FullName),
                                            new SqlParameter("@Relationship",Relationship),
                                            new SqlParameter("@Address",Address),                                            
                                            new SqlParameter("@Phone",Phone),
                                            new SqlParameter("@IsNOK",IsNOK),
                                            new SqlParameter("@Created_By",Created_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_CRW_Insert_Crew_DependentDetails", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);

        }

        public int INS_Crew_DependentDetails_DL(int VarAdd, int CrewID, string FirstName, string Surname, string Relationship, string Address1, string Address2, string Address, string SSN, DateTime? DOB, String City, string State, string Country, String ZipCode, string Phone, int IsBeneficiary, int IsNOK, int Modified_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                             new SqlParameter("@VarAdd",VarAdd),
                                            new SqlParameter("@CrewID",CrewID),
                                          new SqlParameter("@FirstName",FirstName),
                                            new SqlParameter("@Surname",Surname),
                                            new SqlParameter("@Relationship",Relationship),
                                            new SqlParameter("@Address1",Address1),
                                            new SqlParameter("@Address2",Address2),  
                                             new SqlParameter("@Address",Address),  
                                            new SqlParameter("@SSN",SSN),    
                                            new SqlParameter("@DOB",DOB),    
                                            new SqlParameter("@City",City),    
                                            new SqlParameter("@State",State),    
                                            new SqlParameter("@Country",Country),    
                                            new SqlParameter("@ZipCode",ZipCode),    
                                            new SqlParameter("@Phone",Phone),
                                            new SqlParameter("@IsNOK",IsNOK),
                                            new SqlParameter("@Modified_By",Modified_By),
                                              new SqlParameter("@IsBeneficiary",IsBeneficiary),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_CRW_Insert_Crew_DependentDetails", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);

        }
        public int CRW_Get_CrewVoyageCountByCrewID(int CrewID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewID",@CrewID),
                                            
                                        };

            return Convert.ToInt32(SqlHelper.ExecuteScalar(connection, CommandType.StoredProcedure, "CRW_Get_CrewVoyageCountByCrewID", sqlprm));
        }
        public int INS_CrewInterviewPlanning_DL(int CrewID, int Rank, string CandidateName, DateTime InterviewPlanDate, int PlannedInterviewerID, string InterviewerPosition, int Created_By, string TimeZone, string InterviewType, int IQID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewID",CrewID),
                                            new SqlParameter("@Rank",Rank),
                                            new SqlParameter("@CandidateName",CandidateName),
                                            new SqlParameter("@InterviewPlanDate",InterviewPlanDate),
                                            new SqlParameter("@PlannedInterviewerID",PlannedInterviewerID),
                                            new SqlParameter("@InterviewerPosition",InterviewerPosition),
                                            new SqlParameter("@Created_By",Created_By),
                                            new SqlParameter("@TimeZone",TimeZone),
                                           new SqlParameter("@InterviewType",InterviewType),
                                           new SqlParameter("@IQID",IQID),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_CRW_Insert_CrewInterviewPlanning", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public int INS_CrewInterviewPlanning_DL(int CrewID, int Rank, string CandidateName, DateTime InterviewPlanDate, int PlannedInterviewerID, string InterviewerPosition, int Created_By, string TimeZone)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewID",CrewID),
                                            new SqlParameter("@Rank",Rank),
                                            new SqlParameter("@CandidateName",CandidateName),
                                            new SqlParameter("@InterviewPlanDate",InterviewPlanDate),
                                            new SqlParameter("@PlannedInterviewerID",PlannedInterviewerID),
                                            new SqlParameter("@InterviewerPosition",InterviewerPosition),
                                            new SqlParameter("@Created_By",Created_By),
                                            new SqlParameter("@TimeZone",TimeZone),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_CRW_Insert_CrewInterviewPlanning", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        public int INS_CrewInterviewPlanning_DL(int CrewID, int Rank, string CandidateName, DateTime InterviewPlanDate, int PlannedInterviewerID, string InterviewerPosition, int Created_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewID",CrewID),
                                            new SqlParameter("@Rank",Rank),
                                            new SqlParameter("@CandidateName",CandidateName),
                                            new SqlParameter("@InterviewPlanDate",InterviewPlanDate),
                                            new SqlParameter("@PlannedInterviewerID",PlannedInterviewerID),
                                            new SqlParameter("@InterviewerPosition",InterviewerPosition),
                                            new SqlParameter("@Created_By",Created_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_CRW_Insert_CrewInterviewPlanning", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public int INS_CrewInterviewAnswer_DL(int CrewID, int InterviewID, int QuestionID, int UserAnswer, string UserRemark, string SubAns1, string SubAns2, string SubAns3, string SubAns4, string SubAns5, int Created_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewID",CrewID),
                                            new SqlParameter("@InterviewID",InterviewID),
                                            new SqlParameter("@QuestionID",QuestionID),
                                            new SqlParameter("@UserAnswer",UserAnswer),
                                            new SqlParameter("@UserRemark",UserRemark),
                                            new SqlParameter("@SubAns1",SubAns1),
                                            new SqlParameter("@SubAns2",SubAns2),
                                            new SqlParameter("@SubAns3",SubAns3),
                                            new SqlParameter("@SubAns4",SubAns4),
                                            new SqlParameter("@SubAns5",SubAns5),
                                            new SqlParameter("@Created_By",Created_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_CRW_Insert_CrewInterviewAnswer", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }


        public int INS_CrewAssignment_DL(int CrewID_SigningOff, int CrewID_UnAssigned, int JoiningRank, int Created_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewID_SigningOff",CrewID_SigningOff),
                                            new SqlParameter("@CrewID_UnAssigned",CrewID_UnAssigned),
                                            new SqlParameter("@JoiningRank",JoiningRank),                                            
                                            new SqlParameter("@Created_By",Created_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_CRW_Insert_CrewAssignment", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public int ADD_CrewAssignment_DL(int CrewID_SigningOff, int CrewID_UnAssigned, int JoiningRank, int Created_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                          new SqlParameter("@CrewID_SigningOff",CrewID_SigningOff),
                                            new SqlParameter("@CrewID_UnAssigned",CrewID_UnAssigned),
                                            new SqlParameter("@JoiningRank",JoiningRank),                                            
                                            new SqlParameter("@Created_By",Created_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_CRW_INS_CrewAssignment", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public int INS_CrewRemarks_DL(int CrewID, string Remarks, string AttachmentPath, int Created_By, int VoyageID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewID",CrewID),
                                            new SqlParameter("@Remarks",Remarks),
                                            new SqlParameter("@AttachmentPath",AttachmentPath),
                                            new SqlParameter("@Created_By",Created_By),
                                            new SqlParameter("@VoyageID",VoyageID),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_CRW_Insert_StaffRemarks", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public int INS_Crew_Maintenance_Feedback_DL(int CrewID, string Remarks, string AttachmentPath, int Created_By, int VoyageID, int Job_Type
            , int Vessel_ID, int Worklist_ID, int Office_ID, int PMS_JobID, int PMS_JobHistoryID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@VoyageID",VoyageID),
                                            new SqlParameter("@Remarks",Remarks),
                                            new SqlParameter("@AttachmentPath",AttachmentPath),
                                            new SqlParameter("@Created_By",Created_By),
                                            new SqlParameter("@Job_Type",Job_Type),                                            
                                            new SqlParameter("@Vessel_ID",Vessel_ID),
                                            new SqlParameter("@Worklist_ID",Worklist_ID),
                                            new SqlParameter("@Office_ID",Office_ID),
                                            new SqlParameter("@PMS_JobID",PMS_JobID),
                                            new SqlParameter("@PMS_JobHistoryID",PMS_JobHistoryID),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };

            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_CRW_Insert_Crew_Maintenance_Feedback", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }


        public DataTable Get_CrewMaintenanceFeedback_DL(int CrewID, int UserID)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewID",CrewID),
                                            new SqlParameter("@UserID",UserID)
                                        };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_CRW_Get_Crew_Maintenance_Feedback", sqlprm).Tables[0];

        }







        public int INS_Crew_RecomendedVessel_DL(int CrewID, int InterviewID, int VesselID, int Modified_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewID",CrewID),
                                            new SqlParameter("@InterviewID",InterviewID),
                                            new SqlParameter("@VesselID",VesselID),
                                            new SqlParameter("@Created_By",Modified_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_CRW_Insert_Crew_RecomendedVessel", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        public int INS_Crew_RecomendedZone_DL(int CrewID, int InterviewID, int ZoneID, int Modified_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewID",CrewID),
                                            new SqlParameter("@InterviewID",InterviewID),
                                            new SqlParameter("@ZoneID",ZoneID),
                                            new SqlParameter("@Created_By",Modified_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_CRW_Insert_Crew_RecomendedZone", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }



        #endregion

        #region  - CREW DETAILS - UPDATE -
        public int UPDATE_CrewPreviousContacts_DL(int id, int CrewID, string Name, string PIC, string Telephone, string Fax, string Email, int Created_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@id",id),
                                            new SqlParameter("@CrewID",CrewID),
                                            new SqlParameter("@Name",Name),
                                            new SqlParameter("@PIC",PIC),
                                            new SqlParameter("@Telephone",Telephone),
                                            new SqlParameter("@Fax",Fax),
                                            new SqlParameter("@Email",Email),                                           
                                            new SqlParameter("@Created_By",Created_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_CRW_Update_CrewPreviousContacts", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public int UPDATE_CrewPersonalDetails_DL(CrewProperties objCrew)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewID",objCrew.CrewID),
                                            new SqlParameter("@Rank",objCrew.RankID),
                                            new SqlParameter("@AvailableDate",objCrew.Available_From_Date),
                                            new SqlParameter("@Staff_Surname",objCrew.Surname.ToUpper()),
                                            new SqlParameter("@Staff_Name",objCrew.GivenName.ToUpper()),                                            
                                            new SqlParameter("@Alias",objCrew.Alias),
                                            new SqlParameter("@Staff_Birth_Date",objCrew.DateOfBirth),
                                            new SqlParameter("@Staff_Born_Place",objCrew.PlaceofBirth),
                                            new SqlParameter("@Staff_Nationality",objCrew.Nationality),
                                            new SqlParameter("@MaritalStatus",objCrew.MaritalStatus),
                                            new SqlParameter("@Telephone",objCrew.Telephone),
                                            new SqlParameter("@Address",objCrew.Address.ToUpper()),
                                            new SqlParameter("@Mobile",objCrew.Mobile),
                                            new SqlParameter("@Fax",objCrew.Fax),
                                            new SqlParameter("@EMail",objCrew.EMail),
                                            new SqlParameter("@NearestAirport",objCrew.NearestInternationalAirport.ToUpper()),
                                            new SqlParameter("@NearestAirportID",objCrew.NearestInternationalAirportID ),
                                            new SqlParameter("@ManningOfficeID",objCrew.ManningOfficeID),
                                            new SqlParameter("@Us_Visa_Flag",objCrew.USVisa_Flag),                                            
                                            new SqlParameter("@Us_Visa_Number",objCrew.USVisa_Number),                                            
                                            new SqlParameter("@Us_Visa_Expiry",objCrew.USVisa_Expiry),                                            
                                            new SqlParameter("@Modified_By",objCrew.Modified_By),
                                            new SqlParameter("@Allotment_AccType ",objCrew.Allotment_AccType ),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_CRW_UPDATE_CrewPersonalDetails", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public int UPDATE_CrewPersonalDetails_DL(CrewProperties objCrew, DataTable dtVesselTypes)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewID",objCrew.CrewID),
                                            new SqlParameter("@Rank",objCrew.RankID),
                                            new SqlParameter("@AvailableDate",objCrew.Available_From_Date),
                                            new SqlParameter("@Staff_Surname",objCrew.Surname.ToUpper()),
                                            new SqlParameter("@Staff_Name",objCrew.GivenName.ToUpper()),                                            
                                            new SqlParameter("@Alias",objCrew.Alias),
                                            new SqlParameter("@Staff_Birth_Date",objCrew.DateOfBirth),
                                            new SqlParameter("@Staff_Nationality",objCrew.Nationality),
                                            new SqlParameter("@Telephone",objCrew.Telephone),
                                            new SqlParameter("@ManningOfficeID",objCrew.ManningOfficeID),
                                            new SqlParameter("@Modified_By",objCrew.Modified_By),
                                             new SqlParameter("@Mobile",objCrew.Mobile),
                                              new SqlParameter("@HireDate",objCrew.HireDate),
                                               new SqlParameter("@UnionID",objCrew.UnionID),
                                                new SqlParameter("@UnionBranch",objCrew.UnionBranch),
                                                 new SqlParameter("@UnionBook",objCrew.UnionBook),
                                                  new SqlParameter("@Permanent",objCrew.Permanent),
                                                   new SqlParameter("@Email",objCrew.EMail),
                                            new SqlParameter("@dtVesselTypes",dtVesselTypes ),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_CRW_UPDATE_CrewPersonalDetails", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        public int UPDATE_MultinationalCrewInfo_DL(int CrewID, int MultinationalCrewExp, string MultinationalCrewNat, int Modified_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewID",CrewID),
                                           new SqlParameter("@MultinationalCrewExp",MultinationalCrewExp),
                                           new SqlParameter("@MultinationalCrewNat",MultinationalCrewNat),
                                           new SqlParameter("@Modified_By",Modified_By),                                           
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_CRW_UPDATE_MultinationalCrewInfo", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);

        }

        public int UPDATE_CrewPreJoiningExp_DL(int ID, string Vessel_Name, string Flag, string Vessel_Type, string DWT, int GRT, string CompanyName, int Rank, DateTime Date_From, DateTime Date_To, int Months, int Days, int Modified_By, string ME_MakeModel, int ME_BHP, int? CompanyID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ID",ID),
                                            new SqlParameter("@Vessel_Name",Vessel_Name.ToUpper()),
                                            new SqlParameter("@Flag",Flag),
                                            new SqlParameter("@Vessel_Type",Vessel_Type),
                                            new SqlParameter("@DWT",DWT),
                                            new SqlParameter("@GRT",GRT),
                                            new SqlParameter("@CompanyName",CompanyName.ToUpper()),
                                            new SqlParameter("@Rank",Rank),
                                            new SqlParameter("@Date_From",Date_From),
                                            new SqlParameter("@Date_To",Date_To),
                                            new SqlParameter("@Months",Months),
                                            new SqlParameter("@Days",Days),
                                            new SqlParameter("@Modified_By",Modified_By),
                                            new SqlParameter("@ME_MakeModel",ME_MakeModel), 
                                            new SqlParameter("@ME_BHP",ME_BHP),
                                            new SqlParameter("@CompanyID",CompanyID),
                                        };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_CRW_UPDATE_CrewPreJoiningExp", sqlprm);
        }

        public int UPDATE_COC_Date_DL(int VoyID, DateTime dtCOC_Date, string Remark, int Modified_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@VoyID",VoyID),
                                           new SqlParameter("@COC_Date",dtCOC_Date),
                                           new SqlParameter("@Remark",Remark),
                                            new SqlParameter("@Modified_By",Modified_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_CRW_UPDATE_COC_Date", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);

        }

        public int UPDATE_CrewVoyages_DL(int ID, int CrewID, int Vessel_ID, int Joining_Rank, DateTime Joining_Date, int Joining_Port, int Modified_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ID",ID),
                                            new SqlParameter("@CrewID",CrewID),
                                            new SqlParameter("@Joining_Date",Joining_Date),
                                            new SqlParameter("@Joining_Rank",Joining_Rank),
                                            new SqlParameter("@Vessel_ID",Vessel_ID),                                            
                                            new SqlParameter("@Joining_Port",Joining_Port),
                                            new SqlParameter("@Modified_By",Modified_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_CRW_UPDATE_CrewVoyages", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);

        }
        public int UPDATE_CrewVoyages_DL(int ID, int CrewID, int Vessel_ID, int Joining_Rank, DateTime Joining_Date, DateTime COC_Date, int Joining_Port, int Modified_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ID",ID),
                                            new SqlParameter("@CrewID",CrewID),
                                            new SqlParameter("@Joining_Date",Joining_Date),
                                            new SqlParameter("@Joining_Rank",Joining_Rank),
                                            new SqlParameter("@Vessel_ID",Vessel_ID),
                                            new SqlParameter("@COCDate",COC_Date),
                                            new SqlParameter("@Joining_Port",Joining_Port),
                                            new SqlParameter("@Modified_By",Modified_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_CRW_UPDATE_CrewVoyages", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);

        }
        public int UPDATE_CrewVoyages_DL(int ID, int CrewID, int Vessel_ID, int Joining_Rank, DateTime Joining_Date, DateTime COC_Date, int Joining_Port, int Modified_By, DateTime Sign_On_Date, DateTime Sign_Off_Date, int Sign_Off_Port, int Sign_Off_Reason)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ID",ID),
                                            new SqlParameter("@CrewID",CrewID),
                                            new SqlParameter("@Joining_Date",Joining_Date),
                                            new SqlParameter("@Joining_Rank",Joining_Rank),
                                            new SqlParameter("@Vessel_ID",Vessel_ID),
                                            new SqlParameter("@COCDate",COC_Date),
                                            new SqlParameter("@Joining_Port",Joining_Port),
                                            new SqlParameter("@Modified_By",Modified_By),
                                            new SqlParameter("@Sign_On_Date",Sign_On_Date),
                                            new SqlParameter("@Sign_Off_Date",Sign_Off_Date),
                                            new SqlParameter("@Sign_Off_Port",Sign_Off_Port),
                                            new SqlParameter("@Sign_Off_Reason",Sign_Off_Reason),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_CRW_UPDATE_CrewVoyages", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);

        }
        public int UPDATE_CrewVoyages_DL(int ID, int CrewID, int Joining_Type, int Vessel_ID, int Joining_Rank, DateTime Joining_Date, int Joining_Port, DateTime Sign_On_Date, DateTime Sign_Off_Date, int Sign_Off_Port, int Sign_Off_Reason, int Modified_By, string MPA_Ref, DateTime DOA_HomePort, int RankScaleId, int ContractId, DateTime COCDate, int VesselTypeAssignment)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ID",ID),
                                            new SqlParameter("@CrewID",CrewID),
                                            new SqlParameter("@Joining_Type",Joining_Type),
                                            new SqlParameter("@Joining_Date",Joining_Date),
                                            new SqlParameter("@Joining_Rank",Joining_Rank),
                                            new SqlParameter("@Vessel_ID",Vessel_ID),
                                            new SqlParameter("@Joining_Port",Joining_Port),
                                            new SqlParameter("@Modified_By",Modified_By),
                                            new SqlParameter("@Sign_On_Date",Sign_On_Date),
                                            new SqlParameter("@Sign_Off_Date",Sign_Off_Date),
                                            new SqlParameter("@Sign_Off_Port",Sign_Off_Port),
                                            new SqlParameter("@Sign_Off_Reason",Sign_Off_Reason),
                                            new SqlParameter("@MPA_Ref",MPA_Ref),
                                            new SqlParameter("@DOA_HomePort",DOA_HomePort),
                                            new SqlParameter("@RankScaleId",RankScaleId),
                                            new SqlParameter("@ContractId",ContractId),
                                            new SqlParameter("@COCDate",COCDate),
                                            new SqlParameter("@VesselTypeAssignment",VesselTypeAssignment),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_CRW_UPDATE_CrewVoyages", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);

        }

        public int UPDATE_CrewWages_DL(int ID)
        {
            SqlParameter sqlprm = new SqlParameter("CrewID", ID);
            return 1;//SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_CRW_UPDATE_CrewWages", sqlprm).Tables[0];
        }

        public int UPDATE_CrewPassportAndSeamanDetails_DL(int CrewID, string Passport_Number, DateTime Passport_Issue_Date, DateTime Passport_Expiry_Date, string Passport_PlaceOf_Issue, string Seaman_Book_Number, DateTime Seaman_Book_Issue_Date, DateTime Seaman_Book_Expiry_Date, string Seaman_Book_PlaceOf_Issue, int Modified_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewID",CrewID),
                                            new SqlParameter("@Passport_Number",Passport_Number),
                                            new SqlParameter("@Passport_Issue_Date",Passport_Issue_Date),
                                            new SqlParameter("@Passport_Expiry_Date",Passport_Expiry_Date),
                                            new SqlParameter("@Passport_PlaceOf_Issue",Passport_PlaceOf_Issue),                                            
                                            new SqlParameter("@Seaman_Book_Number",Seaman_Book_Number),
                                            new SqlParameter("@Seaman_Book_Issue_Date",Seaman_Book_Issue_Date),
                                            new SqlParameter("@Seaman_Book_Expiry_Date",Seaman_Book_Expiry_Date),
                                            new SqlParameter("@Seaman_Book_PlaceOf_Issue",Seaman_Book_PlaceOf_Issue),  
                                            new SqlParameter("@Modified_By",Modified_By),                                            
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_CRW_Update_CrewPassportAndSeamanDetails", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        public int UPDATE_CrewDocument_DL(int CrewID, int DocID, int DocTypeID, string DocumentName, string DocNo, DateTime IssueDate, string IssuePalce, DateTime ExpiryDate, int Modified_By, int IssueCountry)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@DocID",DocID),
                                            new SqlParameter("@DocTypeID",DocTypeID),
                                            new SqlParameter("@DocName",DocumentName),                                            
                                            new SqlParameter("@DocNo",DocNo),
                                            new SqlParameter("@IssueDate",IssueDate),
                                            new SqlParameter("@IssuePalce",IssuePalce),
                                            new SqlParameter("@ExpiryDate",ExpiryDate),
                                            new SqlParameter("@Modified_By",Modified_By),    
                                            new SqlParameter("@IssueCountry",IssueCountry),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_DMS_Update_Crew_Document", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public int UPDATE_CrewDocument_DL(int DocID, string DocName, string DocNo, int SizeByte, int DocTypeID, int Modified_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@DocID",DocID),
                                            new SqlParameter("@DocName",DocName),
                                            new SqlParameter("@DocNo",DocNo),
                                            new SqlParameter("@SizeByte",SizeByte),
                                            new SqlParameter("@DocTypeID",DocTypeID),
                                            new SqlParameter("@Modified_By",Modified_By),                                            
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_DMS_Update_Crew_Document", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public int UPDATE_CrewDocument_DL(int DocID, int DocTypeID, int Modified_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@DocID",DocID),
                                            new SqlParameter("@DocTypeID",DocTypeID),
                                            new SqlParameter("@Modified_By",Modified_By),                                            
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_DMS_Update_DocumentType", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        public int UPDATE_Crew_DependentDetails_DL(int ID, string FullName, string Relationship, string Address, string Phone, int IsNOK, int Modified_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ID",ID),
                                            new SqlParameter("@FullName",FullName),
                                            new SqlParameter("@Relationship",Relationship),
                                            new SqlParameter("@Address",Address),                                            
                                            new SqlParameter("@Phone",Phone),
                                            new SqlParameter("@IsNOK",IsNOK),
                                            new SqlParameter("@Modified_By",Modified_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_CRW_Update_Crew_DependentDetails", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);

        }

        public int UPDATE_Crew_DependentDetails_DL(int AddVar, int ID, string FirstName, string Surname, string Relationship, string Address1, string Address2, string Address, string SSN, DateTime? DOB, String City, string State, string Country, String ZipCode, string Phone, int IsBeneficiary, int IsNOK, int Modified_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                             new SqlParameter("@AddVar",AddVar),
                                            new SqlParameter("@ID",ID),
                                            new SqlParameter("@FirstName",FirstName),
                                            new SqlParameter("@Surname",Surname),
                                            new SqlParameter("@Relationship",Relationship),
                                            new SqlParameter("@Address1",Address1),
                                            new SqlParameter("@Address2",Address2),   
                                            new SqlParameter("@Address",Address),  
                                            new SqlParameter("@SSN",SSN),    
                                            new SqlParameter("@DOB",DOB),    
                                            new SqlParameter("@City",City),    
                                            new SqlParameter("@State",State),    
                                            new SqlParameter("@Country",Country),    
                                            new SqlParameter("@ZipCode",ZipCode),    
                                            new SqlParameter("@Phone",Phone),
                                             new SqlParameter("@IsBeneficiary",IsBeneficiary),
                                            new SqlParameter("@IsNOK",IsNOK),
                                            new SqlParameter("@Modified_By",Modified_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_CRW_Update_Crew_DependentDetails", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);

        }

        public int UPDATE_DocumentAttributeValues_DL(int AttributeValueID, string AttributeValue_String, int Modified_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@AttributeValueID",AttributeValueID),
                                            new SqlParameter("@AttributeValue",AttributeValue_String),                                            
                                            new SqlParameter("@Modified_By",Modified_By),                                            
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_DMS_Update_DocAttributeValue_String", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        public int UPDATE_CrewPhotoURL_DL(int CrewID, string PhotoURL)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewID",CrewID),
                                            new SqlParameter("@PhotoURL",PhotoURL),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_CRW_Update_CrewPhotoURL", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);

        }

        public int UPDATE_CrewInterviewPlanning_DL(int InterviewID, DateTime InterviewPlanDate, int PlannedInterviewerID, int CrewID, int Rank, string CandidateName, int Modified_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@InterviewID",InterviewID),
                                            new SqlParameter("@InterviewPlanDate",InterviewPlanDate),
                                            new SqlParameter("@PlannedInterviewerID",PlannedInterviewerID),
                                            new SqlParameter("@CrewID",CrewID),
                                            new SqlParameter("@Rank",Rank),
                                            new SqlParameter("@CandidateName",CandidateName),
                                            new SqlParameter("@Modified_By",Modified_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_CRW_Update_CrewInterviewPlanning", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);

        }

        public int UPDATE_CrewInterviewPlanningDate_DL(int ID, DateTime InterviewPlanDate, int Modified_By, int TZID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ID",ID),
                                            new SqlParameter("@InterviewPlanDate",InterviewPlanDate),
                                            new SqlParameter("@Modified_By",Modified_By),
                                            new SqlParameter("@TZID",TZID),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_CRW_Update_CrewInterviewPlanningDate", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);

        }

        public int UPDATE_CrewInterviewPlanningDate_DL(int ID, DateTime InterviewPlanDate, int Modified_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ID",ID),
                                            new SqlParameter("@InterviewPlanDate",InterviewPlanDate),
                                            new SqlParameter("@Modified_By",Modified_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_CRW_Update_CrewInterviewPlanningDate", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);

        }

        public int UPDATE_CrewInterviewPlanningDate_DL(int ID, DateTime InterviewPlanDate, int Modified_By, string TimeZone)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ID",ID),
                                            new SqlParameter("@InterviewPlanDate",InterviewPlanDate),
                                            new SqlParameter("@TimeZone",TimeZone),
                                            new SqlParameter("@Modified_By",Modified_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_CRW_Update_CrewInterviewPlanningDate", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);

        }

        public int UPDATE_CrewInterviewResult_DL(int CrewID, int InterviewID, int InterviewerID, string InterviewerPosition, DateTime InterviewDate, int CrewRankID, string ResultText, int Selected, string OtherText, int Modified_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewID",CrewID),
                                            new SqlParameter("@InterviewID",InterviewID),
                                            new SqlParameter("@InterviewerID",InterviewerID),
                                            new SqlParameter("@InterviewerPosition",InterviewerPosition),
                                            new SqlParameter("@InterviewDate",InterviewDate),
                                            new SqlParameter("@CrewRankID",CrewRankID),
                                            new SqlParameter("@ResultText",ResultText),
                                            new SqlParameter("@Selected",Selected),
                                            new SqlParameter("@OtherText",OtherText),
                                            new SqlParameter("@Modified_By",Modified_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_CRW_Update_CrewInterviewResult", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        public int UPDATE_CrewInterviewAnswer_DL(int CrewID, int InterviewID, int QuestionID, int UserAnswer, string UserAnswerText, int Modified_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewID",CrewID),
                                            new SqlParameter("@InterviewID",InterviewID),
                                            new SqlParameter("@QuestionID",QuestionID),
                                            new SqlParameter("@UserAnswer",UserAnswer),
                                            new SqlParameter("@UserAnswerText",UserAnswerText),
                                            new SqlParameter("@Modified_By",Modified_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_CRW_Update_CrewInterviewAnswer", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public int UPDATE_CrewInterviewAnswer_DL(int CrewID, int InterviewID, int QuestionID, int UserAnswer, string UserAnswerText, string SubAns1, string SubAns2, string SubAns3, string SubAns4, string SubAns5, int Modified_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewID",CrewID),
                                            new SqlParameter("@InterviewID",InterviewID),
                                            new SqlParameter("@QuestionID",QuestionID),
                                            new SqlParameter("@UserAnswer",UserAnswer),
                                            new SqlParameter("@UserAnswerText",UserAnswerText),
                                            new SqlParameter("@SubAns1",SubAns1),
                                            new SqlParameter("@SubAns2",SubAns2),
                                            new SqlParameter("@SubAns3",SubAns3),
                                            new SqlParameter("@SubAns4",SubAns4),
                                            new SqlParameter("@SubAns5",SubAns5),
                                            new SqlParameter("@Modified_By",Modified_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_CRW_Update_CrewInterviewAnswer", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        public int UPDATE_CrewApprovalByHeadOffice_DL(int CrewID, int ApproverID, DateTime ApprovalDate, int ApprovalYesNo, string ApprovalOtherText, string ApprovalRemark, int QuickApproval, int Approved_Rank)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewID",CrewID),
                                            new SqlParameter("@ApproverID",ApproverID),
                                            new SqlParameter("@ApprovalDate",ApprovalDate),
                                            new SqlParameter("@ApprovalYesNo",ApprovalYesNo),
                                            new SqlParameter("@ApprovalOtherText",ApprovalOtherText),
                                            new SqlParameter("@ApprovalRemark",ApprovalRemark),  
                                            new SqlParameter("@QuickApproval",QuickApproval), 
                                            new SqlParameter("@Approved_Rank",Approved_Rank), 
                                            
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_CRW_Update_CrewApprovalByHeadOffice", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public int UPDATE_CrewApprovalByHeadOffice_DL(int CrewID, int ApproverID, DateTime ApprovalDate, int ApprovalYesNo, string ApprovalOtherText, string ApprovalRemark, int QuickApproval, int Approved_Rank, DataTable dtVesselTypes)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewID",CrewID),
                                            new SqlParameter("@ApproverID",ApproverID),
                                            new SqlParameter("@ApprovalDate",ApprovalDate),
                                            new SqlParameter("@ApprovalYesNo",ApprovalYesNo),
                                            new SqlParameter("@ApprovalOtherText",ApprovalOtherText),
                                            new SqlParameter("@ApprovalRemark",ApprovalRemark),  
                                            new SqlParameter("@QuickApproval",QuickApproval), 
                                            new SqlParameter("@Approved_Rank",Approved_Rank), 
                                            new SqlParameter("@dtVesselTypes",dtVesselTypes), 
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_CRW_Update_CrewApprovalByHeadOffice", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public int CREATE_CrewChangeEvent_DL(int VesselID, DateTime EventDate, int PortID, int Created_By, int Port_Call_ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@VesselID",VesselID),
                                            new SqlParameter("@EventDate",EventDate),
                                            new SqlParameter("@PortID",PortID),
                                            new SqlParameter("@Created_By",Created_By),    
                                            new SqlParameter("@Port_Call_ID",Port_Call_ID),                                                
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_CRW_Insert_CrewChangeEvent", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public int UPDATE_CrewChangeEvent_DL(int EventID, DateTime EventDate, int PortID, string EventRemark, int Modified_By, int Port_Call_ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@EventID",EventID),
                                            new SqlParameter("@EventDate",EventDate),
                                            new SqlParameter("@PortID",PortID),
                                            new SqlParameter("@EventRemark",EventRemark),
                                            new SqlParameter("@Modified_By",Modified_By),   
                                            new SqlParameter("@Port_Call_ID",Port_Call_ID),                                            
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_CRW_Update_CrewChangeEvent", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        public int AddCrewTo_CrewChangeEvent_DL(int EventID, int CrewID, int ON_OFF, int AssignmentID, int Created_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@EventID",EventID),
                                            new SqlParameter("@CrewID",CrewID),
                                            new SqlParameter("@ON_OFF",ON_OFF),
                                            new SqlParameter("@AssignmentID",AssignmentID),
                                            new SqlParameter("@Created_By",Created_By),                                            
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_CRW_AddCrewToEvent", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public int AddCrewTo_CrewChangeEvent_DL(int EventID, int CrewID, int ON_OFF, int AssignmentID, int Created_By, int VoyID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@EventID",EventID),
                                            new SqlParameter("@CrewID",CrewID),
                                            new SqlParameter("@ON_OFF",ON_OFF),
                                            new SqlParameter("@AssignmentID",AssignmentID),
                                            new SqlParameter("@Created_By",Created_By),
                                            new SqlParameter("@VoyageID",VoyID),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_CRW_AddCrewToEvent", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public int AddCrewTo_CrewChangeEvent_DL(int EventID, int CrewID, int ON_OFF, int AssignmentID, int Created_By, DateTime Joining_Date, DateTime COC_Date, int Joining_Rank)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@EventID",EventID),
                                            new SqlParameter("@CrewID",CrewID),
                                            new SqlParameter("@ON_OFF",ON_OFF),
                                            new SqlParameter("@AssignmentID",AssignmentID),
                                            new SqlParameter("@Created_By",Created_By),
                                            new SqlParameter("@Joining_Date",Joining_Date),
                                            new SqlParameter("@COC_Date",COC_Date),
                                            new SqlParameter("@Joining_Rank",Joining_Rank),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };

            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_CRW_AddCrewToEvent", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public int AddCrewTo_CrewChangeEvent_DL(int EventID, int CrewID, int ON_OFF, int AssignmentID, int Created_By, DateTime Joining_Date, DateTime COC_Date, int Joining_Rank, int VoyageID, int RankScaleId)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@EventID",EventID),
                                            new SqlParameter("@CrewID",CrewID),
                                            new SqlParameter("@ON_OFF",ON_OFF),
                                            new SqlParameter("@AssignmentID",AssignmentID),
                                            new SqlParameter("@Created_By",Created_By),
                                            new SqlParameter("@Joining_Date",Joining_Date),
                                            new SqlParameter("@COC_Date",COC_Date),
                                            new SqlParameter("@Joining_Rank",Joining_Rank),
                                            new SqlParameter("@VoyageID",VoyageID),
                                            new SqlParameter("@RankScaleId",RankScaleId),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };

            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_CRW_AddCrewToEvent", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        public int RemoveCrewFrom_CrewChangeEvent_DL(int EventID, int CrewID, int Deleted_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@EventID",EventID),
                                            new SqlParameter("@CrewID",CrewID),
                                            new SqlParameter("@Deleted_By",Deleted_By),                                            
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_CRW_RemoveCrewFromEvent", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public int UPDATE_CrewJoiningChecklist_DL(int CrewID, int QuestionID, int AnswerYN, string Remarks, int Modified_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewID",CrewID),
                                            new SqlParameter("@QuestionID",QuestionID),
                                            new SqlParameter("@AnswerYN",AnswerYN),
                                            new SqlParameter("@Remarks",Remarks),
                                            new SqlParameter("@Modified_By",Modified_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_CRW_Update_CrewJoiningChecklist", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public int UPDATE_DocumentChecklist_DL(int CrewID, int DocID, int DocTypeID, string DocName, int AnswerYN, int RankID, string Remarks, string DocNo, DateTime IssueDate, string IssuePlace, DateTime ExpiryDate, int Modified_By, int VoyageID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewID",CrewID),
                                            new SqlParameter("@DocID",DocID),
                                            new SqlParameter("@DocTypeID",DocTypeID),
                                            new SqlParameter("@DocName",DocName),
                                            new SqlParameter("@AnswerYN",AnswerYN),
                                            new SqlParameter("@RankID",RankID),
                                            new SqlParameter("@Remarks",Remarks),
                                            new SqlParameter("@DocNo",DocNo),
                                            new SqlParameter("@IssueDate",IssueDate),
                                            new SqlParameter("@IssuePlace",IssuePlace),
                                            new SqlParameter("@ExpiryDate",ExpiryDate),
                                            new SqlParameter("@Modified_By",Modified_By),
                                            new SqlParameter("@VoyageID",VoyageID),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_CRW_Update_DocumentChecklist", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public int UPDATE_DocumentChecklist_DL(int CrewID, int DocID, int DocTypeID, string DocName, int AnswerYN, int RankID, string Remarks, string DocNo, DateTime IssueDate, string IssuePlace, DateTime ExpiryDate, int Modified_By, int VoyageID, int SaveAndReplace)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewID",CrewID),
                                            new SqlParameter("@DocID",DocID),
                                            new SqlParameter("@DocTypeID",DocTypeID),
                                            new SqlParameter("@DocName",DocName),
                                            new SqlParameter("@AnswerYN",AnswerYN),
                                            new SqlParameter("@RankID",RankID),
                                            new SqlParameter("@Remarks",Remarks),
                                            new SqlParameter("@DocNo",DocNo),
                                            new SqlParameter("@IssueDate",IssueDate),
                                            new SqlParameter("@IssuePlace",IssuePlace),
                                            new SqlParameter("@ExpiryDate",ExpiryDate),
                                            new SqlParameter("@Modified_By",Modified_By),
                                            new SqlParameter("@VoyageID",VoyageID),
                                            new SqlParameter("@SaveAndReplace",SaveAndReplace),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_CRW_Update_DocumentChecklist", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        public int UPDATE_CrewStatus_DL(int CrewID, string Status_Code, string Remark, int Modified_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewID",CrewID),
                                            new SqlParameter("@Status_Code",Status_Code),
                                            new SqlParameter("@Remark",Remark),
                                            new SqlParameter("@Modified_By",Modified_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_CRW_Update_CrewStatus", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public int UPDATE_CrewUniformSize_DL(int CrewID, string Shoe, string TShirt, string CargoPant, string Overall, int Modified_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewID",CrewID),
                                            new SqlParameter("@Shoe",Shoe),
                                            new SqlParameter("@TShirt",TShirt),
                                            new SqlParameter("@CargoPant",CargoPant),
                                            new SqlParameter("@Overall",Overall),
                                            new SqlParameter("@Modified_By",Modified_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_CRW_Update_UniformSize", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public int UPDATE_HeightWaistWeight_DL(int CrewID, string Height, string Waist, string Weight, int Modified_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewID",CrewID),
                                            new SqlParameter("@Height",Height),
                                            new SqlParameter("@Waist",Waist),
                                            new SqlParameter("@Weight",Weight),
                                            new SqlParameter("@Modified_By",Modified_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_CRW_Update_HeightWaistWeight", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }



        #endregion

        #region  - CREW DETAILS - DELETE -
        public int DEL_CrewPersonalDetails_DL(int ID)
        {
            SqlParameter sqlprm = new SqlParameter("ID", ID);
            return 1;//SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_CRW_DEL_CrewPersonalDetails", sqlprm).Tables[0];
        }
        public int DEL_CrewPreJoiningExp_DL(int ID, int Deleted_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ID",ID),
                                            new SqlParameter("@Deleted_By",Deleted_By)
                                        };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_CRW_DEL_CrewPreJoiningExp", sqlprm);
        }
        public int DEL_CrewVoyages_DL(int ID, int Deleted_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ID",ID),
                                            new SqlParameter("@Deleted_By",Deleted_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_CRW_DEL_CrewVoyages", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public int DEL_CrewWages_DL(int ID)
        {
            SqlParameter sqlprm = new SqlParameter("CrewID", ID);
            return 1;//SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_CRW_DEL_CrewWages", sqlprm).Tables[0];
        }
        public int DEL_Crew_DocumentByDocID_DL(int DocID, int SessionUserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@DocID",DocID),
                                            new SqlParameter("@Deleted_By",SessionUserID),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_DMS_Del_DocumentByDocID", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public int DEL_Crew_DependentDetails_DL(int ID, int Deleted_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ID",ID),
                                            new SqlParameter("@Deleted_By",Deleted_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_CRW_Del_Crew_DependentDetails", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);

        }

        public int DEL_CrewInterviewPlanning_DL(int ID, int Deleted_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ID",ID),
                                            new SqlParameter("@Deleted_By",Deleted_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_CRW_Del_CrewInterviewPlanning", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public int DEL_CrewInterviewAnswers_DL(int CrewID, int InterviewID, int Deleted_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewID",CrewID),
                                            new SqlParameter("@InterviewID",InterviewID),
                                            new SqlParameter("@Deleted_By",Deleted_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_CRW_Del_CrewInterviewAnswers", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        //public int Undo_Manning_Approval_DL(int CrewID, int Deleted_By)
        //{
        //    SqlParameter[] sqlprm = new SqlParameter[]
        //                                { 
        //                                    new SqlParameter("@CrewID",CrewID),
        //                                    new SqlParameter("@Deleted_By",Deleted_By),
        //                                    new SqlParameter("return",SqlDbType.Int)
        //                                };
        //    sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
        //    SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_CRW_Undo_Manning_Approval", sqlprm);
        //    return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        //}
        public int Delete_CrewAssignment_DL(int ID, int Deleted_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ID",ID),
                                            new SqlParameter("@Deleted_By",Deleted_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_CRW_Del_CrewAssignment", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public int Delete_CrewChangeEvent_Planned_DL(int ID, int Deleted_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ID",ID),
                                            new SqlParameter("@Deleted_By",Deleted_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_CRW_Del_CrewChangeEvent_Planned", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        public int Delete_Crew_DL(int CrewID, int Deleted_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewID",CrewID),
                                            new SqlParameter("@Deleted_By",Deleted_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_CRW_Delete_CrewRecords", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }


        #endregion

        //public int Validate_CrewContractPrinting_DL(int CrewID)
        //{
        //    SqlParameter[] sqlprm = new SqlParameter[]
        //                                { 
        //                                    new SqlParameter("@CrewID",CrewID),
        //                                    new SqlParameter("return",SqlDbType.Int)
        //                                };
        //    sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
        //    SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_CRW_Validate_CrewContractPrinting", sqlprm);
        //    return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        //}
        public int Send_CrewNotification_attachment(int MailID, string Attachment_Name, string Attachment_Path, int CreatedBy)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CREWMAILID",MailID),
                                            new SqlParameter("@ATTACHMENT_NAME",Attachment_Name),
                                            new SqlParameter("@ATTACHMENT_PATH",Attachment_Path),
                                            new SqlParameter("@USERID",CreatedBy)

                                            
                                        };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_SP_CREW_MAIL_ATTACHMENT_SAVE", sqlprm);
        }
        public int Send_CrewNotification_DL(int CrewID, int ManningOfficeID, int EventID, int EventType, string MailTo, string CC, string BCC, string Subject, string EmailBody, string AttachmentPath, string ItemType, DateTime Dt_MeetingTime, int Created_By, string ReadyStatus, string TimeZone)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewID",CrewID),
                                            new SqlParameter("@ManningOfficeID",ManningOfficeID),
                                            new SqlParameter("@EventID",EventID),
                                            new SqlParameter("@EventType",EventType),
                                            new SqlParameter("@MailTo",MailTo),
                                            new SqlParameter("@CC",CC),
                                            new SqlParameter("@BCC",BCC),
                                            new SqlParameter("@Subject",Subject),
                                            new SqlParameter("@EmailBody",EmailBody),
                                            new SqlParameter("@AttachmentPath",AttachmentPath),
                                            new SqlParameter("@ItemType",ItemType),
                                            new SqlParameter("@MeetingTime",Dt_MeetingTime),
                                            new SqlParameter("@Created_By",Created_By),
                                            new SqlParameter("@ReadyStatus",ReadyStatus),
                                            new SqlParameter("@TimeZone",TimeZone),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_CRW_Insert_Crew_Notification", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        public int UPDATE_CrewNotification_DL(int MsgID, string MailTo, string CC, string BCC, string Subject, string EmailBody, string AttachmentPath, DateTime Dt_MeetingTime, int Modified_By, string ReadyStatus)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@MsgID",MsgID),
                                            new SqlParameter("@MailTo",MailTo),
                                            new SqlParameter("@CC",CC),                                            
                                            new SqlParameter("@Subject",Subject),
                                            new SqlParameter("@EmailBody",EmailBody),
                                            new SqlParameter("@AttachmentPath",AttachmentPath),
                                            new SqlParameter("@MeetingTime",Dt_MeetingTime),
                                            new SqlParameter("@Modified_By",Modified_By),
                                            new SqlParameter("@ReadyStatus",ReadyStatus),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_CRW_Update_Crew_Notification", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public int Discard_MailMessage_DL(int MsgID, int Deleted_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@MsgID",MsgID),
                                            new SqlParameter("@Deleted_By",Deleted_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_CRW_Discard_MailMessage", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        public DataTable UPDATE_VoyageDocumentChecklist()
        {

            string SQL = @"SELECT     DMS_DTL_DOCUMENT.CrewID, DMS_DTL_DOCUMENT.DocID, DMS_DTL_DOCUMENT.DocName, DMS_DTL_DOCUMENT.DocTypeID, 
                    CRW_DTL_Last_Voyage_View.ID AS Voyage_ID, CRW_DTL_Last_Voyage_View.Vessel_ID, CRW_DTL_Last_Voyage_View.Joining_Rank, 
                    DMS_DTL_DOCUMENT.DateOfIssue, DMS_DTL_DOCUMENT.DateOfExpiry, DMS_DTL_DOCUMENT.PlaceOfIssue
                FROM         DMS_DTL_DOCUMENT INNER JOIN
                                        CRW_DTL_Last_Voyage_View ON DMS_DTL_DOCUMENT.CrewID = CRW_DTL_Last_Voyage_View.CrewID LEFT OUTER JOIN
                                        CRW_DTL_DocumentChecklist ON DMS_DTL_DOCUMENT.DocID = CRW_DTL_DocumentChecklist.DocID
                WHERE     (DMS_DTL_DOCUMENT.Active_Status = 1) AND (DMS_DTL_DOCUMENT.DocTypeID IN
                                            (SELECT     DocTypeID
                                            FROM          DMS_LIB_DOCTYPES
                                            WHERE      (Voyage = 1) AND (Active_Status = 1))) AND (ISNULL(CRW_DTL_DocumentChecklist.VoyageID, 0) = 0)
                ORDER BY DMS_DTL_DOCUMENT.CrewID";


            DataTable dt = ExecuteQuery(SQL);

            string SQL_Insert = "";
            foreach (DataRow dr in dt.Rows)
            {
                SQL_Insert = "Declare @iExists int = 0; SELECT @iExists =ID FROM CRW_DTL_DocumentChecklist WHERE active_status=1 and CrewID = @CrewID and DocTypeID = @DocTypeID and VoyageID = @Voyage_ID";

                SQL_Insert += " if(@iExists = 0) begin ";

                SQL_Insert += @"INSERT INTO CRW_DTL_DocumentChecklist (CrewID,DocID,DocName, DocTypeID, AnswerYN, RankID, Remark,DocNo, IssueDate,IssuePlace,ExpiryDate, Created_By, Date_of_Creation, VoyageID) 
                                VALUES
                            (@CrewID,@DocID,@DocName, @DocTypeID, 1,@Joining_Rank, '.' , '' , @DateOfIssue,@PlaceOfIssue,@DateOfExpiry, @Modified_By , GETDATE(), @Voyage_ID)";


                SQL_Insert += " end ";

                DateTime Dt_Issuedate = DateTime.Parse("1901/01/01");
                DateTime Dt_ExpiryDate = DateTime.Parse("1901/01/01");

                if (dr["DateOfIssue"].ToString() != "")
                    Dt_Issuedate = DateTime.Parse(dr["DateOfIssue"].ToString());

                if (dr["DateOfExpiry"].ToString() != "")
                    Dt_ExpiryDate = DateTime.Parse(dr["DateOfExpiry"].ToString());

                SqlParameter[] sqlprm = new SqlParameter[]
                                    { 
                                        new SqlParameter("@CrewID",int.Parse( dr["CrewID"].ToString())),
                                        new SqlParameter("@DocID",int.Parse( dr["DocID"].ToString())),
                                        new SqlParameter("@DocTypeID",int.Parse( dr["DocTypeID"].ToString())),
                                        new SqlParameter("@DocName",dr["DocName"].ToString()),                                        
                                        new SqlParameter("@Joining_Rank",int.Parse(dr["Joining_Rank"].ToString())),
                                        new SqlParameter("@DateOfIssue",Dt_Issuedate),
                                        new SqlParameter("@PlaceOfIssue",dr["PlaceOfIssue"].ToString()),
                                        new SqlParameter("@DateOfExpiry",Dt_ExpiryDate),
                                        new SqlParameter("@Modified_By",0),
                                        new SqlParameter("@Voyage_ID",int.Parse( dr["Voyage_ID"].ToString()))                                        
                                    };

                SqlHelper.ExecuteNonQuery(connection, CommandType.Text, SQL_Insert, sqlprm);

            }
            return dt;
        }

        public int Save_Contract_Template_DL(int ContractId, string TemplateName, string TemplateText, int Created_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ContractId",ContractId),
                                            new SqlParameter("@TemplateName",TemplateName),
                                            new SqlParameter("@TemplateText",TemplateText),
                                            new SqlParameter("@Created_By",Created_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_CRW_Save_Contract_Template", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        public int INS_CrewYellow_RedCard_DL(int CrewID, int CardType, string Remarks, int Created_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewID",CrewID),
                                            new SqlParameter("@CardType",CardType),
                                            new SqlParameter("@Remarks",Remarks),
                                            new SqlParameter("@Created_By",Created_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_CRW_Propose_CrewCard", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public int Approve_CrewYellow_RedCard_DL(int Approval_Status, int CardID, string ApproverRemarks, int Created_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Approval_Status",Approval_Status),
                                            new SqlParameter("@CardID",CardID),
                                            new SqlParameter("@ApproverRemarks",ApproverRemarks),
                                            new SqlParameter("@Created_By",Created_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_CRW_Approve_CrewYellow_RedCard", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public int INS_CrewCard_Attachment_DL(int CardID, int AttachmentType, string AttachmentName, string AttachmentPath, int Created_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CardID",CardID),
                                            new SqlParameter("@AttachmentType",AttachmentType),
                                            new SqlParameter("@AttachmentName",AttachmentName),
                                            new SqlParameter("@AttachmentPath",AttachmentPath),
                                            new SqlParameter("@Created_By",Created_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_CRW_INS_Card_Attachment", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public int UPDATE_CrewCard_AttachmentStatus_DL(int CardID, string AttachmentType, int Status, int Updated_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CardID",CardID),
                                            new SqlParameter("@AttachmentType",AttachmentType),
                                            new SqlParameter("@Status",Status),
                                            new SqlParameter("@Updated_By",Updated_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_CRW_UPDATE_Card_AttachmentStatus", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        public int INS_CrewCard_Remarks_DL(int CardID, string Remarks, string RemarksType, int Created_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CardID",CardID),
                                            new SqlParameter("@Remarks",Remarks),
                                            new SqlParameter("@RemarksType",RemarksType),
                                            new SqlParameter("@Created_By",Created_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_CRW_INS_CrewCard_Remarks", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public SqlDataReader Get_CrewCard_Remarks_DL(int CardID, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CardID",CardID),
                                            new SqlParameter("@UserID",UserID)
                                        };
            return SqlHelper.ExecuteReader(connection, CommandType.StoredProcedure, "SP_CRW_Get_CrewCard_Remarks", sqlprm);

        }
        public int Approve_JoiningRank_DL(int VoyageID, int RankID, int Approved_By, string ApproverRemarks)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@VoyageID",VoyageID),
                                            new SqlParameter("@RankID",RankID),
                                            new SqlParameter("@Approved_By",Approved_By),
                                            new SqlParameter("@ApproverRemarks",ApproverRemarks),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_CRW_Approve_JoiningRank", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        public int TransferCrew_DL(int CurrentVoyageID, int CrewID, int Vessel_ID, int Joining_Type, int Joining_Rank, DateTime Joining_Date, DateTime Sign_On_Date, int Joining_Port, DateTime COC_Date, int Created_By, int ContractId)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CurrentVoyageID",CurrentVoyageID),
                                            new SqlParameter("@CrewID",CrewID),
                                            new SqlParameter("@Vessel_ID",Vessel_ID),
                                            new SqlParameter("@Joining_Type",Joining_Type),
                                            new SqlParameter("@Joining_Rank",Joining_Rank),
                                            new SqlParameter("@Joining_Date",Joining_Date),
                                            new SqlParameter("@Sign_On_Date",Sign_On_Date),
                                            new SqlParameter("@Joining_Port",Joining_Port),
                                            new SqlParameter("@COC_Date",COC_Date),
                                            new SqlParameter("@Created_By",Created_By),
                                            new SqlParameter("@ContractId",ContractId),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_CRW_Transfer_Planning", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        public int TransferCrew_DL(int CurrentVoyageID, int CrewID, int Vessel_ID, int Joining_Type, int Joining_Rank, DateTime Joining_Date, DateTime Sign_On_Date, int Joining_Port, DateTime COC_Date, int Created_By, DateTime Sign_Off_Date, int SignOff_Port, int NewWageContractId, int ContractId)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CurrentVoyageID",CurrentVoyageID),
                                            new SqlParameter("@CrewID",CrewID),
                                            new SqlParameter("@Vessel_ID",Vessel_ID),
                                            new SqlParameter("@Joining_Type",Joining_Type),
                                            new SqlParameter("@Joining_Rank",Joining_Rank),
                                            new SqlParameter("@Joining_Date",Joining_Date),
                                            new SqlParameter("@Sign_On_Date",Sign_On_Date),
                                            new SqlParameter("@Joining_Port",Joining_Port),
                                            new SqlParameter("@COC_Date",COC_Date),
                                            new SqlParameter("@Created_By",Created_By),
                                            new SqlParameter("@Sign_Off_Date",Sign_Off_Date),
                                            new SqlParameter("@Sign_Off_Port",SignOff_Port),
                                            new SqlParameter("@NewWageContractId",NewWageContractId),
                                            new SqlParameter("@ContractId",ContractId),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_CRW_Transfer_Planning", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public int TransferCrew_DL(int CurrentVoyageID, int CrewID, int Vessel_ID, int Joining_Type, int Joining_Rank, DateTime Joining_Date, DateTime Sign_On_Date, int Joining_Port, DateTime COC_Date, int Created_By, DateTime Sign_Off_Date, int SignOff_Port, int ContractId)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CurrentVoyageID",CurrentVoyageID),
                                            new SqlParameter("@CrewID",CrewID),
                                            new SqlParameter("@Vessel_ID",Vessel_ID),
                                            new SqlParameter("@Joining_Type",Joining_Type),
                                            new SqlParameter("@Joining_Rank",Joining_Rank),
                                            new SqlParameter("@Joining_Date",Joining_Date),
                                            new SqlParameter("@Sign_On_Date",Sign_On_Date),
                                            new SqlParameter("@Joining_Port",Joining_Port),
                                            new SqlParameter("@COC_Date",COC_Date),
                                            new SqlParameter("@Created_By",Created_By),
                                            new SqlParameter("@Sign_Off_Date",Sign_Off_Date),
                                            new SqlParameter("@Sign_Off_Port",SignOff_Port),
                                            new SqlParameter("@ContractId",ContractId),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_CRW_Transfer_Planning", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        public int UPDATE_Crew_DirectContract_DL(int CrewID, int DirectContract, int Modified_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewID",CrewID),
                                            new SqlParameter("@DirectContract",DirectContract),
                                            new SqlParameter("@Modified_By",Modified_By),                                            
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_CRW_Update_Crew_DirectContract", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);

        }


        public int Insert_CrewAgreementRecord_DL(int CrewID, int VoyageID, int Stage, int Contract_Template_ID, string DocumentName, string DocFileName, string DocFilePath, int Created_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewID",CrewID),
                                            new SqlParameter("@VoyageID",VoyageID),
                                            new SqlParameter("@Stage",Stage),
                                            new SqlParameter("@Contract_Template_ID",Contract_Template_ID),
                                            new SqlParameter("@DocName",DocumentName),
                                            new SqlParameter("@DocFileName",DocFileName),
                                            new SqlParameter("@DocFilePath",DocFilePath),
                                            new SqlParameter("@Created_By",Created_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_CRW_Insert_Crew_Agreement_Document", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public int Insert_CrewAgreementRecord_DL(int CrewID, int VoyageID, int Stage, int Contract_Template_ID, string DocumentName, string DocFileName, string DocFilePath, int Created_By, int pagecount)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewID",CrewID),
                                            new SqlParameter("@VoyageID",VoyageID),
                                            new SqlParameter("@Stage",Stage),
                                            new SqlParameter("@Contract_Template_ID",Contract_Template_ID),
                                            new SqlParameter("@DocName",DocumentName),
                                            new SqlParameter("@DocFileName",DocFileName),
                                            new SqlParameter("@DocFilePath",DocFilePath),
                                            new SqlParameter("@Created_By",Created_By),
                                            new SqlParameter("@pagecount",pagecount),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_CRW_Insert_Crew_Agreement_Document", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public int Verify_CrewAgreement_DL(int CrewID, int VoyageID, int Created_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewID",CrewID),
                                            new SqlParameter("@VoyageID",VoyageID),
                                            new SqlParameter("@Created_By",Created_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_CRW_Verify_CrewAgreement", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        public int Undo_Verify_CrewAgreement_DL(int CrewID, int VoyageID, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewID",CrewID),
                                            new SqlParameter("@VoyageID",VoyageID),
                                            new SqlParameter("@UserID",UserID),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_CRW_Undo_Verify_CrewAgreement", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public int Undo_DigiSign_CrewAgreement_DL(int CrewID, int VoyageID, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewID",CrewID),
                                            new SqlParameter("@VoyageID",VoyageID),
                                            new SqlParameter("@UserID",UserID),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_CRW_Undo_DigiSign_CrewAgreement", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public int Undo_StaffsSign_CrewAgreement_DL(int CrewID, int VoyageID, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewID",CrewID),
                                            new SqlParameter("@VoyageID",VoyageID),
                                            new SqlParameter("@UserID",UserID),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_CRW_Undo_StaffsSign_CrewAgreement", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        public int Save_Sideletter_Template_DL(int TemplateID, string TemplateName, string TemplateText, int Created_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@TemplateID",TemplateID),
                                            new SqlParameter("@TemplateName",TemplateName),
                                            new SqlParameter("@TemplateText",TemplateText),
                                            new SqlParameter("@Created_By",Created_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_CRW_Save_SideLetter_Template", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public DataTable Get_Seniority_Log_DL(int CrewID, int VoyageID, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewID",CrewID),
                                            new SqlParameter("@VoyageID",VoyageID),
                                            new SqlParameter("@UserID",UserID)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "ACC_SP_Get_Seniority_Log", sqlprm).Tables[0];

        }
        //public int NationalityCheck_SendForApproval_DL(int Vessel_ID, int EventID, int CrewID, int CurrentRank_ID, int JoiningRank_ID, string Sender_Remarks, int Created_By)
        //{
        //    SqlParameter[] sqlprm = new SqlParameter[]
        //                                { 
        //                                    new SqlParameter("@Vessel_ID",Vessel_ID),
        //                                    new SqlParameter("@EventID",EventID),
        //                                    new SqlParameter("@CrewID",CrewID),
        //                                    new SqlParameter("@CurrentRank_ID",CurrentRank_ID),
        //                                    new SqlParameter("@JoiningRank_ID",JoiningRank_ID),
        //                                    new SqlParameter("@Sender_Remarks",Sender_Remarks),
        //                                    new SqlParameter("@Created_By",Created_By),
        //                                    new SqlParameter("return",SqlDbType.Int)
        //                                };
        //    sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
        //    SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_SP_NationalityCheck_SendForApproval", sqlprm);
        //    return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        //}
        public int NationalityCheck_SendForApproval_DL(int Vessel_ID, int CrewID, int CurrentRank, int JoiningRank, string Sender_Remarks, int Created_By, int EventID = 0, int CrewID_SigningOff = 0)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Vessel_ID",Vessel_ID),
                                            new SqlParameter("@CrewID",CrewID),
                                            new SqlParameter("@CurrentRank",CurrentRank),                                            
                                            new SqlParameter("@JoiningRank",JoiningRank),                                            
                                            new SqlParameter("@Sender_Remarks",Sender_Remarks),
                                            new SqlParameter("@EventID",EventID),                                            
                                            new SqlParameter("@CrewID_SigningOff",CrewID_SigningOff),                                                                                        
                                            new SqlParameter("@Created_By",Created_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_SP_NationalityCheck_SendForApproval", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        public int NationalityCheck_NewJoiner_DL(int Vessel_ID, int CrewID, int JoiningRank, int UserID, int EventID = 0, int CrewID_SigningOff = 0)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Vessel_ID",Vessel_ID),
                                            new SqlParameter("@CrewID",CrewID),
                                            new SqlParameter("@JoiningRank",JoiningRank),
                                            new SqlParameter("@EventID",EventID),                                            
                                            new SqlParameter("@CrewID_SigningOff",CrewID_SigningOff),                                                                                        
                                            new SqlParameter("@UserID",UserID),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_SP_NationalityCheck_NewJoiner", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        public DataTable Get_NationalityCheck_Approvals_DL(int UserID, int RequestID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@UserID",UserID),
                                            new SqlParameter("@RequestID",RequestID),
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "Get_NationalityCheck_Approvals", sqlprm).Tables[0];
        }

        public int NationalityCheck_Approval_DL(int RequestID, string ApproverRemarks, int Approval, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@RequestID",RequestID),
                                            new SqlParameter("@ApproverRemarks",ApproverRemarks),
                                            new SqlParameter("@Approval",Approval),
                                            new SqlParameter("@UserID",UserID),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_SP_NationalityCheck_Approval", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }



        public DataTable Get_Crew_FeedBack_Viewer_Search(DataTable FleetID, DataTable VesselID, DataTable Rank, DataTable Nationality, DataTable ManningOffice, DataTable CommentedBy, DateTime? dtFrom, DateTime? dtTo
            , string SearchText, int? Status, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {

            SqlParameter[] obj = new SqlParameter[]
            {                   

                new System.Data.SqlClient.SqlParameter("@FleetID",FleetID),
                new System.Data.SqlClient.SqlParameter("@VesselID",VesselID),
                new System.Data.SqlClient.SqlParameter("@Rank",Rank),
                new System.Data.SqlClient.SqlParameter("@Nationality",Nationality),
                new System.Data.SqlClient.SqlParameter("@ManningOffice",ManningOffice),
                new System.Data.SqlClient.SqlParameter("@CommentedBy",CommentedBy),
                new System.Data.SqlClient.SqlParameter("@dtFrom",dtFrom),
                new System.Data.SqlClient.SqlParameter("@dtTo",dtTo),
                new System.Data.SqlClient.SqlParameter("@SearchText",SearchText),
                new System.Data.SqlClient.SqlParameter("@Status",Status),

                new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_Staff_Feedback_Viewer_Search", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds.Tables[0];

        }


        public DataTable Get_Crew_Medical_History_Viewer_Search(DataTable FleetID, DataTable VesselID, DataTable Rank, DataTable Nationality, DataTable ManningOffice
            , DateTime? dtFrom, DateTime? dtTo
            , string SearchText, int? Status, int? CrewStatus, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {

            SqlParameter[] obj = new SqlParameter[]
            {                   

                new System.Data.SqlClient.SqlParameter("@FleetID",FleetID),
                new System.Data.SqlClient.SqlParameter("@VesselID",VesselID),
                new System.Data.SqlClient.SqlParameter("@Rank",Rank),
                new System.Data.SqlClient.SqlParameter("@Nationality",Nationality),
                new System.Data.SqlClient.SqlParameter("@ManningOffice",ManningOffice),
                new System.Data.SqlClient.SqlParameter("@dtFrom",dtFrom),
                new System.Data.SqlClient.SqlParameter("@dtTo",dtTo),
                new System.Data.SqlClient.SqlParameter("@SearchText",SearchText),
                new System.Data.SqlClient.SqlParameter("@Status",Status),
                new System.Data.SqlClient.SqlParameter("@CrewStatus",CrewStatus),
                new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_Medical_History_Viewer_Search", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds.Tables[0];

        }


        public DataTable Get_SystemParameter_DL(string Parent)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Parent",Parent),
                                    
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_Get_SystemParameter", sqlprm).Tables[0];
        }
        public DataTable Get_Crew_Matrix_DL(int CrewID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewID",CrewID),
                                    
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_SP_Get_Crew_Matrix", sqlprm).Tables[0];
        }
        public void Insert_Crew_Matrix_DL(int CrewID, int? Certificate_Of_Competency, int? Issuing_Country, string Administration_Acceptance, string Tanker_Certification, string STCWVPara, string Radio_Qualification, string English_Proficiency, int Created_By)
        {

            SqlParameter[] obj = new SqlParameter[]
            {                   

                new System.Data.SqlClient.SqlParameter("@CrewID",CrewID),
                new System.Data.SqlClient.SqlParameter("@Certificate_Of_Competency",Certificate_Of_Competency),
                new System.Data.SqlClient.SqlParameter("@Issuing_Country",Issuing_Country),
                new System.Data.SqlClient.SqlParameter("@Administration_Acceptance",Administration_Acceptance),
                new System.Data.SqlClient.SqlParameter("@Tanker_Certification",Tanker_Certification),
                new System.Data.SqlClient.SqlParameter("@STCWVPara",STCWVPara),
                new System.Data.SqlClient.SqlParameter("@Radio_Qualification",Radio_Qualification),
                new System.Data.SqlClient.SqlParameter("@English_Proficiency",English_Proficiency),
                new System.Data.SqlClient.SqlParameter("@CreatedBy",Created_By),
            };

            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_SP_Insert_Crew_Matrix", obj);

        }
        public int Delete_CrewMatrix_Attachments_DL(int Code)
        {

            SqlParameter[] obj = new SqlParameter[]
             {
             
             new System.Data.SqlClient.SqlParameter("@Code", Code)
          
             };

            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_SP_Delete_Crew_MatrixAttachment", obj);

        }
        public DataTable Get_CrewMatrix_Attachments_DL(int CrewID)
        {

            SqlParameter[] obj = new SqlParameter[]
             {
             
             new System.Data.SqlClient.SqlParameter("@CrewID", CrewID)
          
             };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_SP_Get_Crew_MatrixAttachment", obj).Tables[0];

        }
        public int Insert_Crew_MatrixAttachment_DL(int CrewID, string Attachment_Name, string Flag_Attach, int UserID)
        {
            SqlParameter[] obj = new SqlParameter[]
            {                   

                new System.Data.SqlClient.SqlParameter("@CrewID",CrewID),
                new System.Data.SqlClient.SqlParameter("@Attachment_Name",Attachment_Name),
                new System.Data.SqlClient.SqlParameter("@Attachment_Path",Flag_Attach),
                new System.Data.SqlClient.SqlParameter("@CreatedBy",UserID),
            };

            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_SP_Insert_Crew_MatrixAttachment", obj);
        }
        #region QuestionBank
        //public DataTable Get_CriteriaList_DL(string SearchText, int CategoryID)
        //{
        //    SqlParameter[] sqlprm = new SqlParameter[]
        //                                { 
        //                                    new SqlParameter("@SearchText",SearchText),
        //                                    new SqlParameter("@Category_ID",CategoryID)
        //                                };

        //    return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_Get_InterviewCriteriaList", sqlprm).Tables[0];
        //}

        //public int INSERT_Criteria_DL(string Criteria, string SubQuestion, int CatID, int Grading_Type, int Created_By)
        //{
        //    SqlParameter[] sqlprm = new SqlParameter[]
        //                                { 
        //                                    new SqlParameter("@Criteria",Criteria),
        //                                    new SqlParameter("@SubQuestion",SubQuestion),
        //                                    new SqlParameter("@Category_ID",CatID),
        //                                    new SqlParameter("@Grading_Type",Grading_Type),
        //                                    new SqlParameter("@Created_By",Created_By),
        //                                    new SqlParameter("return",SqlDbType.Int)
        //                                };
        //    sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
        //    SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_Insert_InterviewCriteria", sqlprm);
        //    return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        //}
        //public int UPDATE_Criteria_DL(int ID, string Criteria, int CatID, int Grading_Type, int Modified_By)
        //{
        //    SqlParameter[] sqlprm = new SqlParameter[]
        //                                { 
        //                                    new SqlParameter("@ID",ID),
        //                                    new SqlParameter("@Criteria",Criteria),
        //                                    new SqlParameter("@Category_ID",CatID),
        //                                    new SqlParameter("@Grading_Type",Grading_Type),
        //                                    new SqlParameter("@Modified_By",Modified_By),
        //                                    new SqlParameter("return",SqlDbType.Int)
        //                                };
        //    sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
        //    SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_Update_InterviewCriteria", sqlprm);
        //    return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        //}
        //public int DELETE_Criteria_DL(int ID, int Deleted_By)
        //{
        //    SqlParameter[] sqlprm = new SqlParameter[]
        //                                { 
        //                                    new SqlParameter("@ID",ID),
        //                                    new SqlParameter("@Deleted_By",Deleted_By),
        //                                    new SqlParameter("return",SqlDbType.Int)
        //                                };
        //    sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
        //    SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_Delete_InterviewCriteria", sqlprm);
        //    return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        //}
        #endregion



        public int Validate_InterviewConfig_DL(int CrewID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewID",CrewID),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_CRW_Check_Interview_Config", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public int Update_CrewManningOffice_DL(int CrewID, int ManningOfficeId)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewID",CrewID),
                                            new SqlParameter("@ManningOfficeId",ManningOfficeId),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_CRW_Update_CrewManningOffice", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public int Get_RejectedInterviewCount_MA_DL(int CrewID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewID",CrewID),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;

            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_CRW_Get_RejectedInterviewCount_MA", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public int SP_CRW_Recommendation_DL(int CrewID, int CreatedBy)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewID",CrewID),
                                            new SqlParameter("@Created_By",CreatedBy),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;

            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_CRW_Recommendation_ManningAgent", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public int Undo_Rejection_DL(int CrewID, int Modified_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewID",CrewID),
                                            new SqlParameter("@Modified_By",Modified_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_CRW_Undo_Rejection", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public int Get_RejectedCount_DL(int CrewID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewID",CrewID),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;

            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_CRW_Check_RejectedStatus", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public int ResetCrewPassword_DL(int CrewID, int Modified_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewID",CrewID),
                                            new SqlParameter("@Modified_By",Modified_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_CRW_ResetPassword", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public DataTable Get_Crew_Documents_DL(int CrewID, string SearchText)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewID",CrewID),
                                            new SqlParameter("@SearchText",SearchText)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_CRW_Get_Crew_Documents", sqlprm).Tables[0];
        }
        public int Check_Crew_Refererce_DL(int CrewID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            { 
                new SqlParameter("@CrewID",CrewID),
                new SqlParameter("return",SqlDbType.Int)
            };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_REFERER_CHECK", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        public DataTable Get_Crew_Referer_Details_DL(int CrewID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            { 
                new SqlParameter("@CrewID",CrewID)
            };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_GET_CRW_REFERER_DTL", sqlprm).Tables[0];
        }

        public int Save_Crew_Reference_Details_DL(int Id, int CrewID, string RefererName, string RefererMobile, DateTime ReferenceDate, string PERSON_QUIERED_NAME, string PersonQuieredTitle, int User_ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ID",Id),
                                            new SqlParameter("@CrewID",CrewID),
                                            new SqlParameter("@REFERER_NAME",RefererName),
                                            new SqlParameter("@REFERER_MOBILE",RefererMobile),
                                            new SqlParameter("@REFERENCE_DATE",ReferenceDate),
                                            new SqlParameter("@QUIERED_BY",User_ID),
                                            new SqlParameter("@PERSON_QUIERED_TITLE",PersonQuieredTitle),
                                            new SqlParameter("@USER_ID",User_ID),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_SAVE_CRW_REFERER_DTL", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        public int DEL_CrewReferenceDetail_DL(int CrewID, int ID, int Deleted_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ID",ID),
                                            new SqlParameter("@CrewID",CrewID),
                                            new SqlParameter("@DELETED_BY",Deleted_By)
                                        };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_DEL_CrewReference", sqlprm);
        }
        public DataTable Get_CrewFBMInfo_DL(int CrewID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewID",CrewID)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_GET_FBM_INFO", sqlprm).Tables[0];
        }
        public int DEL_CrewEvalaution_DL(int ID, int CrewId, int VoyageId, int Deleted_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ID",ID),
                                            new SqlParameter("@CrewID",CrewId),
                                            new SqlParameter("@VoyageId",VoyageId),
                                            new SqlParameter("@Deleted_By",Deleted_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_CRW_Del_CrewEvaluation", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public DataTable Get_WagesNationalityList_DL(int RankID, int Contract_Type)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@RankID",RankID),
                                            new SqlParameter("@Contract_Type",Contract_Type)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_Get_WagesNationalityList", sqlprm).Tables[0];
        }
        public DataTable Get_NationalityForWages_DL(int RankID, int Contract_Type)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@RankID",RankID),
                                            new SqlParameter("@Contract_Type",Contract_Type)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_Get_NationalityForWages", sqlprm).Tables[0];
        }
        public DataTable Get_CrewOtherServices_DL(int CrewID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewID",CrewID),
                                            
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_Get_CrewJoiningService", sqlprm).Tables[0];
        }
        public int Delete_CrewOtherServices_DL(int ID, int CrewID, int User_ID)
        {

            SqlParameter[] prm = new SqlParameter[] {   
                                                        new SqlParameter("@ID",ID ),
                                                        new SqlParameter("@CrewID",CrewID ),
                                                        new SqlParameter("@User_ID",User_ID ),
                                                        
                                                    };
            return Convert.ToInt32(SqlHelper.ExecuteScalar(connection, CommandType.StoredProcedure, "CRW_DEL_OtherServices", prm));

        }
        public int Update_CrewOtherServices_DL(int ID, int CrewID, DateTime DateFrom, DateTime DateTo, int JoiningRank, string ServiceType, int User_ID, string Remarks)
        {

            SqlParameter[] prm = new SqlParameter[] {   
                                                        new SqlParameter("@ID",ID ),
                                                        new SqlParameter("@CrewID",CrewID ),
                                                        new SqlParameter("@DateFrom",DateFrom ),
                                                        new SqlParameter("@DateTo",DateTo ),
                                                        new SqlParameter("@JoiningRank",JoiningRank ),
                                                        new SqlParameter("@SeviceType",ServiceType ),
                                                        new SqlParameter("@UserID",User_ID ),
                                                        new SqlParameter("@Remarks",Remarks ),
                                                    };
            return Convert.ToInt32(SqlHelper.ExecuteScalar(connection, CommandType.StoredProcedure, "CRW_UPD_OtherServices", prm));

        }
        public int Insert_CrewOtherServices_DL(int CrewID, DateTime DateFrom, DateTime DateTo, int JoiningRank, string ServiceType, int User_ID, string Remarks)
        {

            SqlParameter[] prm = new SqlParameter[] {   
                                                        new SqlParameter("@CrewID",CrewID ),
                                                        new SqlParameter("@DateFrom",DateFrom ),
                                                        new SqlParameter("@DateTo",DateTo ),
                                                        new SqlParameter("@JoiningRank",JoiningRank ),
                                                        new SqlParameter("@SeviceType",ServiceType ),
                                                        new SqlParameter("@UserID",User_ID ),
                                                        new SqlParameter("@Remarks",Remarks ),
                                                    };
            return Convert.ToInt32(SqlHelper.ExecuteScalar(connection, CommandType.StoredProcedure, "CRW_INS_OtherServices", prm));
        }
        public DataTable Get_CrewOtherService_Dtl_DL(int ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ID",ID),
                                            
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_Get_OtherServices_Dtl", sqlprm).Tables[0];
        }
        public DataTable Get_RJBList_Index_DL(int? ID, int? RankId, int? Status, string FromDate, string ToDate, int? CurrVesselId, string ExpectFromDate, string ExpectToDate, string SearchText, int? UserID, int PAGE_SIZE, int PAGE_INDEX, ref int SelectRecordCount, string sortbycoloumn, int? sortdirection)
        {
            SqlParameter[] prm = new SqlParameter[] {   
                                                        new SqlParameter("@ID",ID ),
                                                        new SqlParameter("@RankId",RankId ),
                                                        new SqlParameter("@Status",Status ),
                                                        new SqlParameter("@FromDate",FromDate ),
                                                        new SqlParameter("@ToDate",ToDate ),
                                                         new SqlParameter("@CurrVesselId",CurrVesselId ),
                                                        new SqlParameter("@ExpectFromDate",ExpectFromDate ),
                                                        new SqlParameter("@ExpectToDate",ExpectToDate ),
                                                        new SqlParameter("@SearchText",SearchText ),
                                                        new SqlParameter("@UserID",UserID ),
                                                        new SqlParameter("@PAGE_SIZE", PAGE_SIZE),
                                                        new SqlParameter("@PAGE_INDEX",PAGE_INDEX),
                                                        new SqlParameter("@ORDER_BY",sortbycoloumn),
                                                        new SqlParameter("@SORT_DIRECTION",sortdirection),
                                                        new SqlParameter("@SelectRecordCount",SelectRecordCount)
            };
            // prm[0].SqlDbType = SqlDbType.Structured;
            prm[prm.Length - 1].Direction = ParameterDirection.InputOutput;

            DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_Get_RJBList_Index", prm);
            SelectRecordCount = Convert.ToInt32(prm[prm.Length - 1].Value);

            return ds.Tables[0];

        }
        public int Update_RJBApproval_DL(int Status, int ID, int User_ID, decimal? Amount, string Remark, string Mode)
        {

            SqlParameter[] prm = new SqlParameter[] {   new SqlParameter("@Amount",Amount ),
                                                        new SqlParameter("@Status",Status ),
                                                        new SqlParameter("@ID",ID ),
                                                        new SqlParameter("@UserID",User_ID ),
                                                         new SqlParameter("@Remark",Remark ),
                                                         new SqlParameter("@Mode",Mode ),
                                                    };
            return Convert.ToInt32(SqlHelper.ExecuteScalar(connection, CommandType.StoredProcedure, "CRW_Update_RJBApproval", prm));
        }
        public int CheckCrewStatus_DL(int CrewID)
        {
            SqlParameter[] prm = new SqlParameter[] {   
                                                      
                                                        new SqlParameter("@CrewID",CrewID ),
                                                        
                                                    };
            return Convert.ToInt32(SqlHelper.ExecuteScalar(connection, CommandType.StoredProcedure, "CRW_Get_CrewStatus", prm));
        }
        public DataSet Get_CrewSeniorityDetails_DL(int CrewID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewId",CrewID)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "GET_CRW_CREW_SENIORITY_DETAILS", sqlprm);
        }
        public int CheckCrewSeniority(int CrewId)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewID",CrewId),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_CHECK_Crew_Seniority", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public DataSet Get_CrewSeniorityForReversing_DL(int CrewID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewId",CrewID)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "GET_CREW_SENIORITY_FOR_REVERSING", sqlprm);
        }
        public string Get_RJBFormula(string Description)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Description",Description)
            };
            return SqlHelper.ExecuteScalar(connection, CommandType.StoredProcedure, "CRW_Get_RJBFormula_ByDesc", sqlprm).ToString();
        }
        public int GetCrewServiceStatus(int CrewId)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewID",CrewId),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_GET_CREW_SERVICE_STATUS", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public int Get_CrewStatus_DL(int CrewId)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewId",CrewId),
                                            new SqlParameter("return",SqlDbType.Int)
            };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_GET_CREW_STATUS", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        public DataTable Get_GET_CrewRankScale_DL(int CrewId)
        {
            SqlParameter[] prm = new SqlParameter[] {   
                                                        new SqlParameter("@CrewId",CrewId )
            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_GET_CrewRankScale", prm).Tables[0];
        }

        public int Save_AttachedFileInfo_DL(int Crew_Id, string FileName, string FilePath, int CreatedBy)
        {
            try
            {
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[]
             {
             new System.Data.SqlClient.SqlParameter("@Crew_Id", Crew_Id), 
             new System.Data.SqlClient.SqlParameter("@FileName", FileName), 
             new System.Data.SqlClient.SqlParameter("@FilePath",FilePath) ,
             new System.Data.SqlClient.SqlParameter("@CreatedBy",CreatedBy),            
           
             };
                int RetVal = SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_Insert_Attachment", obj);

                return RetVal;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public DataTable Get_AttachedFileInfo_DL(int CrewId)
        {
            try
            {
                SqlParameter[] prm = new SqlParameter[] 
            {   
              new SqlParameter("@Crew_Id",CrewId )
            };
                return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_Get_Attachment", prm).Tables[0];
            }
            catch (Exception)
            {
                return null;
            }
        }
        public int Delete_AttachedFileInfo_DL(int Id, int DeletedBy)
        {
            try
            {
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[]
             {
             new System.Data.SqlClient.SqlParameter("@id", Id),            
             new System.Data.SqlClient.SqlParameter("@DeletedBy",DeletedBy),          
           
             };
                int RetVal = SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_Delete_Attachment", obj);

                return RetVal;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public DataTable Check_Get_AttachedFilePatho_DL(int CrewId)
        {
            try
            {
                SqlParameter[] prm = new SqlParameter[] 
            {   
              new SqlParameter("@Crew_Id",CrewId )
            };
                return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_Check_Get_AttachmentPath", prm).Tables[0];
            }
            catch (Exception)
            {
                return null;
            }
        }
        public DataTable Get_Crew_VoyageVerifiedDocuments_DL(int CrewID, int VoyageID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                           // new SqlParameter("@CrewID",CrewID),
                                            new SqlParameter("@VoyageID",VoyageID)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_Get_VoyageVerifiedDocuments", sqlprm).Tables[0];
        }

        public DataTable Get_CrewPersonalDetailsByID1(int ID)
        {
            try
            {
                SqlParameter sqlprm = new SqlParameter("ID", ID);
                return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_Get_CrewDetailsByCrewID", sqlprm).Tables[0];
            }
            catch (Exception)
            {
                return null;
            }
        }


        /// <summary>
        /// Get Default Values for Crew Matrix Detail tab
        /// </summary>
        /// <param name="CrewId"></param>
        /// <returns></returns>
        public DataSet Get_CrewMatix_DetailAndValue(int CrewId)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlParameter[] sqlprm = new SqlParameter[] {
                    new SqlParameter("@CrewID", CrewId)
                };
                ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_Get_CM_DetailAndValue", sqlprm);
            }
            catch (Exception ex)
            {
                ds = null;
            }
            return ds;
        }

        /// <summary>
        ///  Get Crew years of exp in operator, rank, tanker type and all tankers
        /// </summary>
        /// <param name="CrewId"></param>
        /// <returns></returns>
        public DataSet CRW_Get_CM_CrewYearsExp(int CrewId, int VesselID, string Date, int RankId, int VoyageID)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlParameter[] sqlprm = new SqlParameter[] {
                    new SqlParameter("@CrewID", CrewId),
                    new SqlParameter("@Vessel_ID",VesselID),
                    new SqlParameter("@Date",Date),
                    new SqlParameter("@RankID",RankId),
                    new SqlParameter("@VoyageID",VoyageID)
                };
                ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "[CRW_Get_CM_CrewYearsExp]", sqlprm);
            }
            catch (Exception ex)
            {
                ds = null;
            }
            return ds;
        }


        public int Check_CrewAssignment_DL(int CrewID_SigningOff, int CrewID_UnAssigned ,ref string VesselName)
        {
             SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                          new SqlParameter("@CrewID_SigningOff",CrewID_SigningOff),
                                            new SqlParameter("@CrewID_UnAssigned",CrewID_UnAssigned),
                                            new SqlParameter("@OnboardVesselName",SqlDbType.VarChar,50),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 2].Direction = ParameterDirection.InputOutput;
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_Check_CrewAssignment", sqlprm);
            VesselName = sqlprm[sqlprm.Length - 2].Value.ToString();
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public int Delete_CrewOpenAssignment_DL(int ID, int Deleted_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewID_UnAssigned",ID),
                                            new SqlParameter("@Deleted_By",Deleted_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_Del_CrewOpenAssignment", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public DataTable Get_Record_Info(int CrewId, string DocType)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewId",CrewId),
                                            new SqlParameter("@DocType",DocType)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_Get_CrewMatrixLogInfo", sqlprm).Tables[0];
        }

        public DataTable Get_GroupName(DataTable dt)
        {
            string SQL = string.Empty;
            string strGroupId =string.Empty;

            DataTable dtTable = new DataTable();
            
            foreach(DataRow dr in dt.Rows)
            {
            strGroupId += dr["GroupID"]+",";
            }
                         
             if (strGroupId.Length > 0)
             {
                 strGroupId = strGroupId.Substring(0, strGroupId.Length - 1);

                 SQL = "	SELECT GroupID, GroupName from DMS_Lib_DocumentGroup  where Active_Status = 1  and GroupID in (" + strGroupId + ")";
                 dtTable = ExecuteQuery(SQL);
             }
            return dtTable;
        }
        public DataTable CheckVesselTypeForCrew(int CrewId, int VesselId)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewId",CrewId),
                                            new SqlParameter("@VesselId",VesselId)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_CheckVesselTypeForCrew", sqlprm).Tables[0];
        }
        public DataTable CheckVesselTypeForCrew(DataTable dtCrewId, int VesselId)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@dtCrewId",dtCrewId),
                                            new SqlParameter("@VesselId",VesselId)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_CheckVesselTypeForCrews", sqlprm).Tables[0];
        }
        public DataTable GET_VesselTypeForCrew(int CrewId)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewId",CrewId)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_GET_VesselTypeForCrew", sqlprm).Tables[0];
        }
        public DataSet CRW_GET_DefaultValuesCrewAddEdit(int UserCompanyID)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlParameter[] sqlprm = new SqlParameter[] {
                    new SqlParameter("@UserCompanyID", UserCompanyID)
                };
                ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "[CRW_GET_DefaultValuesCrewAddEdit]", sqlprm);
            }
            catch (Exception ex)
            {
                ds = null;
            }
            return ds;
        }

        public DataSet CRW_LIB_CD_GetCrewDetails(int CrewId)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlParameter[] sqlprm = new SqlParameter[] {
                    new SqlParameter("@CrewID", CrewId)
                };
                ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "[CRW_LIB_CD_GetCrewDetails]", sqlprm);
            }
            catch (Exception ex)
            {
                ds = null;
            }
            return ds;
        }

        public int CRW_INS_CrewDetailsScreen1(string Staff_Surname, string Staff_Name, string Staff_Midname, string Alias,
                                           string Staff_Born_Place, DateTime? Staff_Birth_Date, string MaritalStatus, int Staff_Nationality, int Race,
                                           string SSN, string Address, string AddressLine1, string AddressLine2, string City, string State, string ZipCode,
                                           int CountryId, string NearestAirport, int NearestAirportID, string Telephone, string Email, string Mobile,
                                           string Fax, int Rank_Applied, int ManningOfficeID, DateTime? Available_From_Date, DateTime? HireDate, int UnionId, int UnionBranchID, int VeteranStatusID,
                                           int UnionBookId, int CrewID, int CreatedModifiedBy, string AddressType,string CrewImageURL, ref string Result)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Staff_Surname",Staff_Surname),
                                            new SqlParameter("@Staff_Name",Staff_Name),
                                            new SqlParameter("@Staff_Midname",Staff_Midname),
                                            new SqlParameter("@Alias",Alias),
                                            new SqlParameter("@Staff_Born_Place",Staff_Born_Place),
                                            new SqlParameter("@Staff_Birth_Date",Staff_Birth_Date),
                                            new SqlParameter("@MaritalStatus",MaritalStatus),
                                            new SqlParameter("@Staff_Nationality",Staff_Nationality),
                                            new SqlParameter("@Race",Race),
                                            new SqlParameter("@SSN",SSN),
                                            new SqlParameter("@Address",Address),
                                            new SqlParameter("@AddressLine1",AddressLine1),
                                            new SqlParameter("@AddressLine2",AddressLine2),
                                            new SqlParameter("@City",City),
                                            new SqlParameter("@State",State),
                                            new SqlParameter("@ZipCode",ZipCode),
                                            new SqlParameter("@CountryId",CountryId),
                                            new SqlParameter("@NearestAirport",NearestAirport),
                                            new SqlParameter("@NearestAirportID",NearestAirportID),
                                            new SqlParameter("@Telephone",Telephone),
                                            new SqlParameter("@Email",Email),
                                            new SqlParameter("@Mobile",Mobile),
                                            new SqlParameter("@Fax",Fax),
                                            new SqlParameter("@Rank_Applied",Rank_Applied),
                                            new SqlParameter("@ManningOfficeID",ManningOfficeID),
                                            new SqlParameter("@Available_From_Date",Available_From_Date),
                                            new SqlParameter("@HireDate",HireDate),
                                            new SqlParameter("@UnionId",UnionId),
                                            new SqlParameter("@UnionBranchID",UnionBranchID),
                                            new SqlParameter("@VeteranStatusID",VeteranStatusID),
                                            new SqlParameter("@UnionBookId",UnionBookId),
                                            new SqlParameter("@CrewID",CrewID),
                                            new SqlParameter("@CreatedModifiedBy",CreatedModifiedBy),
                                            new SqlParameter("@AddressType",AddressType),
                                            new SqlParameter("@CrewImageURL",CrewImageURL),
                                            new SqlParameter("@Result",SqlDbType.VarChar)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.Output;
            sqlprm[sqlprm.Length - 1].Size = 500;
            SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_INS_CrewDetailsScreen1", sqlprm);
            Result = Convert.ToString(sqlprm[sqlprm.Length - 1].Value);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }


        public string CRW_INS_CrewDetailsScreen2(CrewProperties objCrew, int CreatedModifiedBy,ref string Result)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Passport_Number",objCrew.Passport_Number),
                                            new SqlParameter("@Passport_PlaceOf_Issue",objCrew.Passport_PlaceOf_Issue),
                                            new SqlParameter("@Passport_Issue_Date",objCrew.Passport_Issue_Date),
                                            new SqlParameter("@Passport_Expiry_Date",objCrew.Passport_Expiry_Date),
                                            new SqlParameter("@Passport_CountryId",objCrew.Passport_Country),
                                            new SqlParameter("@Seaman_Book_Number",objCrew.Seaman_Book_Number),
                                            new SqlParameter("@Seaman_Book_Issue_date",objCrew.Seaman_Book_Issue_Date),
                                            new SqlParameter("@Seaman_Book_Expiry_Date",objCrew.Seaman_Book_Expiry_Date),
                                            new SqlParameter("@Seaman_Book_PlaceOf_Issue",objCrew.Seaman_Book_PlaceOf_Issue),
                                            new SqlParameter("@Seaman_CountryId",objCrew.Seaman_Book_Country),
                                            new SqlParameter("@MMC_Number",objCrew.MMC_Number),
                                            new SqlParameter("@MMC_Issue_date",objCrew.MMC_Issue_Date),
                                            new SqlParameter("@MMC_Expiry_Date",objCrew.MMC_Expiry_Date),
                                            new SqlParameter("@MMC_PlaceOf_Issue",objCrew.MMC_PlaceOf_Issue),
                                            new SqlParameter("@MMC_CountryId",objCrew.MMC_Country),
                                            new SqlParameter("@TWIC_Number",objCrew.TWIC_Number),
                                            new SqlParameter("@TWIC_Issue_date",objCrew.TWIC_Issue_Date),
                                            new SqlParameter("@TWIC_Expiry_Date",objCrew.TWIC_Expiry_Date),
                                            new SqlParameter("@TWIC_PlaceOf_Issue",objCrew.TWIC_PlaceOf_Issue),
                                            new SqlParameter("@TWIC_CountryId",objCrew.TWIC_Country),
                                            new SqlParameter("@Us_Visa_Flag",objCrew.USVisa_Flag),
                                            new SqlParameter("@Us_Visa_Expiry",objCrew.USVisa_Expiry),
                                            new SqlParameter("@Us_Visa_Number",objCrew.USVisa_Number),
                                            new SqlParameter("@Us_Issue_Date",objCrew.USVisa_Issue_Date),
                                            new SqlParameter("@School",objCrew.School),
                                            new SqlParameter("@SchoolYearGraduated",objCrew.SchoolYearGraduated),
                                            new SqlParameter("@Naturaliztion",objCrew.Naturaliztion),
                                            new SqlParameter("@NaturaliztionDate",objCrew.NaturaliztionDate),
                                            new SqlParameter("@Height",objCrew.Height),
                                            new SqlParameter("@Weight",objCrew.Weight),
                                            new SqlParameter("@Waist",objCrew.Waist),
                                            new SqlParameter("@ShoeSize",objCrew.ShoeSize),
                                            new SqlParameter("@TShirtSize",objCrew.TShirtSize),
                                            new SqlParameter("@CargoPantSize",objCrew.CargoPantSize),
                                            new SqlParameter("@OverallSize",objCrew.OverallSize),
                                            new SqlParameter("@CustomField1",objCrew.CF1),
                                            new SqlParameter("@CustomField2",objCrew.CF2),
                                            new SqlParameter("@CustomField3",objCrew.CF3),
                                            new SqlParameter("@CrewID",objCrew.CrewID),
                                            new SqlParameter("@EnglishProficiency",objCrew.EnglishProficiency),
                                            new SqlParameter("@CreatedModifiedBy",CreatedModifiedBy),
                                            new SqlParameter("@Result",SqlDbType.VarChar)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.Output;
            sqlprm[sqlprm.Length - 1].Size = 500;
            SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "[CRW_INS_CrewDetailsScreen2]", sqlprm);
            Result = Convert.ToString(sqlprm[sqlprm.Length - 1].Value);
            return Convert.ToString(sqlprm[sqlprm.Length - 1].Value);
        }

        public int CRW_INS_CrewDetailsScreen3(int CrewID, string FirstName, string Surname, string RealtionShip, string PhoneNumber, DateTime? DOB, string SSN, string Address, string Address1, string Address2, string City, string State, int Country, string ZipCode, int Isbeneficiary, int CreatedModifiedBy, int NOKID, string AddressType, ref int Result)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                  { 
                   new SqlParameter("@CrewID",CrewID),
                   new SqlParameter("@FirstName",FirstName),
                   new SqlParameter("@SurName",Surname),
                   new SqlParameter("@Relationship",RealtionShip),
                   new SqlParameter("@SSN",SSN),
                   new SqlParameter("@Address",Address),
                   new SqlParameter("@AddressLine1",Address1),
                   new SqlParameter("@AddressLine2",Address2),
                   new SqlParameter("@City",City),
                   new SqlParameter("@State",State),
                   new SqlParameter("@ZipCode",ZipCode),
                   new SqlParameter("@CountryId",Country),
                   new SqlParameter("@Phone",PhoneNumber),
                   new SqlParameter("@DOB",DOB),
                   new SqlParameter("@Isbeneficiary",Isbeneficiary),
                   new SqlParameter("@AddressType",AddressType),
                   new SqlParameter("@NOKID",NOKID),
                   new SqlParameter("@CreatedModifiedBy",CreatedModifiedBy),
                   new SqlParameter("@Result",SqlDbType.Int)
                   };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "[CRW_INS_CrewDetailsScreen3]", sqlprm);
            Result = Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        public DataTable CheckCrewMandatoryFields(int CrewID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            { 
                new SqlParameter("@CrewID",CrewID)
            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_CHECK_CrewMandatoryFields", sqlprm).Tables[0];
        }

        public int CRW_INS_AddVesselTye(DataTable dtCrewID, int VesselId, int CreatedBy, ref int Result)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            { 
                new SqlParameter("@dtCrewID",dtCrewID),
                new SqlParameter("@VesselId",VesselId),
                new SqlParameter("@CreatedBy",CreatedBy),
                new SqlParameter("@Result",SqlDbType.Int)
            };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "[CRW_INS_AddVesselTye]", sqlprm);
            Result = Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
    }
}
