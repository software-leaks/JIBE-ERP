
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

using SMS.Data.Crew;
using SMS.Properties;
using SMS.Data.DMS;
using SMS.Data;
using SMS.Data.Infrastructure;
using System.Globalization;




namespace SMS.Business.Crew
{
    public class BLL_Crew_CrewDetails
    {
        DAL_Crew_CrewDetails objCrewDAL = new DAL_Crew_CrewDetails();
        DAL_DMS_Document objDALDMS = new DAL_DMS_Document();
        DAL_Infra_UserCredentials objDalUser = new DAL_Infra_UserCredentials();
        IFormatProvider iFormatProvider = new System.Globalization.CultureInfo("en-GB", true);

        #region  - CREW DETAILS - GET -
        public DataTable GetCrewIfCrewExists(string FirstName, string Surname, string DOB, string PassportNo)
        {
            DateTime Dt_DOB = DateTime.Parse("1900/01/01");
            if (DOB.Length > 0)
                Dt_DOB = DateTime.Parse(DOB);

            return objCrewDAL.GetCrewIfExists_DL(FirstName, Surname, Dt_DOB, PassportNo);
        }

        public DataTable Get_Crewlist_InterviewPlanner(int CrewID, string STAFF_NAME, string STAFF_CODE, int ManningOfficeID, int RankID, int iUserID)
        {
            try
            {

                string UserType = GetCurrentUserType(iUserID);

                string SQL = "";

                SQL = @"SELECT     CD.ID, CD.Staff_Code, CD.Staff_Name, CD.Staff_Midname, CD.Staff_Surname, ISNULL(CD.Staff_Name, '') + ' ' + ISNULL(CD.Staff_Midname, '') 
                      + ' ' + ISNULL(CD.Staff_Surname, '') AS 'Staff_FullName', CD.Staff_Nationality,  CD.Passport_Number, 
                       V.Joining_Rank, CD.Alias,  LIB_Country.Country_Name, 
                       CRW_LIB_Crew_Ranks.Rank_Short_Name AS Rank_Applied_Name, 
                    CASE WHEN rank.Rank_Short_Name IS NULL THEN CRW_LIB_Crew_Ranks.Rank_Short_Name ELSE rank.Rank_Short_Name END AS Rank_Name, 
                    CD.ManningOfficeID, LIB_Country.ISO_Code
                    FROM         CRW_LIB_Crew_Details AS CD LEFT OUTER JOIN
                      CRW_LIB_Crew_Ranks ON CD.Staff_Rank = CRW_LIB_Crew_Ranks.ID LEFT OUTER JOIN
                      LIB_Country ON CD.Staff_Nationality = LIB_Country.ID LEFT OUTER JOIN
                          (SELECT     B.ID, A.CrewID, A.Staff_Code, B.Joining_Date,B.Sign_On_Date, B.Sign_Off_Date, B.Joining_Rank, B.Voyage_Remarks, B.Vessel_ID, B.Est_Sing_Off_Date
                            FROM          (SELECT     CrewID, Staff_Code, MAX(Sign_On_Date) AS Sign_On_Date
                                                    FROM          CRW_Dtl_Crew_Voyages
                                                    GROUP BY CrewID, Staff_Code) AS A INNER JOIN
                                                   CRW_Dtl_Crew_Voyages AS B ON A.CrewID = B.CrewID AND A.Sign_On_Date = B.Sign_On_Date) AS V LEFT OUTER JOIN
                      CRW_LIB_Crew_Ranks AS rank ON V.Joining_Rank = rank.ID ON CD.ID = V.CrewID
                WHERE     (1 = 1)";

                if (CrewID > 0)
                    SQL += " AND CD.ID = @CrewID";
                else
                {

                    if (STAFF_NAME != "")
                        SQL += " AND (CD.Staff_Name LIKE @Name or CD.Staff_Midname LIKE @Name or CD.Staff_Surname LIKE  @Name) ";

                    if (STAFF_CODE == "NULL")
                        SQL += " AND (CD.Staff_Code is null) ";

                    else if (STAFF_CODE != "")
                        SQL += " AND (CD.Staff_Code LIKE @Staff_Code) ";
                }

                if (RankID != 0)
                    SQL += " AND (v.Joining_Rank = @RankID) ";

                if (UserType == "MANNING AGENT")
                {
                    SQL += " AND (CD.ManningOfficeID = @ManningOfficeID) ";
                }


                SQL += " ORDER BY CRW_LIB_Crew_Ranks.Rank_Sort_Order";

                SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewID",CrewID),
                                            new SqlParameter("@UserID",iUserID),
                                            new SqlParameter("@Name","%"+STAFF_NAME+"%"),
                                            new SqlParameter("@Staff_Code","%"+STAFF_CODE+"%"),
                                            new SqlParameter("@ManningOfficeID",ManningOfficeID),
                                            new SqlParameter("@RankID",RankID)
                                        };

                return objCrewDAL.ExecuteQuery(SQL, sqlprm);


            }
            catch
            {
                throw;
            }
        }

        public DataTable Get_Crewlist_ForDMS(int CrewID, string Staff_Code, string Staff_Name, int UserCompanyID)
        {
            try
            {
                //if (Staff_Code == null)
                //    Staff_Code = "";
                //else
                //    Staff_Code = "%" + Staff_Code + "%";

                //if (Staff_Name== null)
                //    Staff_Name = "";
                //else
                //    Staff_Name = "%" + Staff_Name + "%";

                string SQL = @"SELECT     CRW_LIB_Crew_Details.ID, CRW_LIB_Crew_Details.Staff_Code, 
                                CRW_LIB_Crew_Details.Staff_Name, 
                                (case when CRW_LIB_Crew_Ranks.Rank_Short_Name is null then  CRW_LIB_Crew_Ranks_1.Rank_Short_Name else CRW_LIB_Crew_Ranks.Rank_Short_Name end )AS Rank_Short_Name
                            FROM         CRW_LIB_Crew_Ranks AS CRW_LIB_Crew_Ranks_1 INNER JOIN
                                                  CRW_LIB_Crew_Details ON CRW_LIB_Crew_Ranks_1.ID = CRW_LIB_Crew_Details.Staff_Rank LEFT OUTER JOIN
                                                  CRW_DTL_CURRENT_VOYAGE_VIEW INNER JOIN
                                                  CRW_LIB_Crew_Ranks ON CRW_DTL_CURRENT_VOYAGE_VIEW.Joining_Rank = CRW_LIB_Crew_Ranks.ID ON 
                                                  CRW_LIB_Crew_Details.ID = CRW_DTL_CURRENT_VOYAGE_VIEW.CrewID
                            WHERE     (CRW_LIB_Crew_Details.Active_Status = 1)";


                SQL += " AND (CRW_LIB_Crew_Details.ManningOfficeID = @ManningOfficeID) ";

                if ((Staff_Code == null || Staff_Code == "") && (Staff_Name == null || Staff_Name == ""))
                {
                    if (CrewID != 0)
                    {
                        SQL += " AND (CRW_LIB_Crew_Details.ID = @CrewID)";
                    }
                }
                else
                {
                    if (Staff_Code != null && Staff_Code != "")
                    {
                        Staff_Code = "%" + Staff_Code + "%";
                        SQL += " AND (UPPER(CRW_LIB_Crew_Details.Staff_Code) LIKE UPPER(@Staff_Code))";
                    }
                    if (Staff_Name != null && Staff_Name != "")
                    {
                        Staff_Name = "%" + Staff_Name + "%";
                        SQL += " AND  (CRW_LIB_Crew_Details.Staff_Name LIKE @Staff_Name OR CRW_LIB_Crew_Details.Staff_SurName LIKE @Staff_Name)";
                    }
                }

                SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewID",CrewID),
                                            new SqlParameter("@Staff_Code", Staff_Code),
                                            new SqlParameter("@Staff_Name", Staff_Name),
                                            new SqlParameter("@ManningOfficeID",UserCompanyID)
                                        };

                return objCrewDAL.ExecuteQuery(SQL, sqlprm);
            }
            catch
            {
                throw;
            }
        }

        public DataTable Get_Crewlist_ForAddIn(string CREW_STATUS, int VESSEL_ID, string SearchString)
        {
            try
            {
                DateTime Dt_FromJoningDate = DateTime.Today.AddYears(-10);
                DateTime Dt_ToJoningDate = DateTime.Today;

                //string UserType = GetCurrentUserType(iUserID);

                string SQL = "";

                SQL = @"SELECT     CD.ID, CD.Staff_Code as 'Staff Code', CD.Staff_Name + ' ' + isnull(CD.Staff_Surname,'') as Name,                              
                              CASE WHEN rank.Rank_Short_Name IS NULL THEN CRW_LIB_Crew_Ranks.Rank_Short_Name ELSE rank.Rank_Short_Name END AS Rank, 
                                LIB_Country.Country_Name as Nationality, 
                            CASE 
                                WHEN CD.Status_Code IS NOT NULL  THEN CD.Status_Code 
                                WHEN CD.ACTIVE_STATUS = 0 THEN 'INACTIVE' 
                                WHEN V.Sign_On_Date IS NOT NULL AND V.SIGN_OFF_DATE IS NULL AND (V.EST_SING_OFF_DATE BETWEEN GETDATE() AND DATEADD(DD, 30, GETDATE()) OR V.EST_SING_OFF_DATE < GETDATE()) THEN 'COC DUE' 
                                WHEN V.Sign_On_Date IS NOT NULL AND V.SIGN_OFF_DATE IS NOT NULL AND EV.CrewID IS NULL THEN 'SIGNED OFF' 
                                WHEN V.Sign_On_Date IS NOT NULL AND V.SIGN_OFF_DATE IS NULL THEN 'CURRENT' 
                                WHEN EV.CrewID IS NOT NULL AND ev.Event_Date IS NOT NULL THEN 'PLANNED' 
                                WHEN UnA.CrewID_UnAssigned IS NOT NULL AND ev.Event_Date IS NULL THEN 'ASSIGNED' 
                                WHEN isnull(CD.ManningOfficeStatus, 0) = 0 OR isnull(CD.CrewManagerApproval, 0) = 0 THEN 'PENDING' 
                                WHEN CD.CrewManagerApproval = -1 THEN 'REJECTED' 
                                WHEN ((V.Sign_On_Date IS NULL AND V.SIGN_OFF_DATE IS NULL) AND (CD.MANNINGOFFICESTATUS = 1 AND CD.CREWMANAGERAPPROVAL = 1) AND (EV.CrewID IS NULL AND ev.Event_Date IS NULL)) THEN 'NO VOYAGE' END AS CrewStatus

                FROM         (SELECT     VoyageID, SUM(Amount) AS Amount
                                   FROM          ACC_DTL_JOINING_EARN_DEDUCTION
                                   WHERE      (Active_Status = 1)
                                   GROUP BY VoyageID) AS salary RIGHT OUTER JOIN
                                  CRW_LIB_Crew_Ranks AS rank RIGHT OUTER JOIN
                                  LIB_VESSELS INNER JOIN
                                      (SELECT     V1.ID, V1.CrewID, V1.Joining_Date,v1.Sign_On_Date, V1.Joining_Rank, V1.Vessel_ID, V1.Est_Sing_Off_Date, V1.Staff_Code, V1.Sign_Off_Date, V1.Voyage_Remarks, 
                                                               V1.COC_Modified
                                        FROM          CRW_Dtl_Crew_Voyages AS V1 INNER JOIN
                                                                   (SELECT     CrewID, MAX(Sign_On_Date) AS Sign_On_Date
                                                                     FROM          CRW_Dtl_Crew_Voyages
                                                                     WHERE      (Active_Status = 1)
                                                                     GROUP BY CrewID) AS V ON V1.CrewID = V.CrewID AND V1.Sign_On_Date = V.Sign_On_Date
                                        WHERE      (V1.Active_Status = 1) AND (V1.Sign_On_Date IS NOT NULL)) AS V ON LIB_VESSELS.Vessel_ID = V.Vessel_ID LEFT OUTER JOIN
                                  CRW_DTL_StaffRemarks ON V.COC_Modified = CRW_DTL_StaffRemarks.ID ON rank.ID = V.Joining_Rank ON salary.VoyageID = V.ID RIGHT OUTER JOIN
                                  CRW_LIB_Crew_Details AS CD LEFT OUTER JOIN
                                      (SELECT     PKID, CrewID_SigningOff, CrewID_UnAssigned
                                        FROM          CRW_DTL_CrewAssignment AS CRW_DTL_CrewAssignment_1) AS SOff INNER JOIN
                                  CRW_LIB_Crew_Details AS OnSigner ON SOff.CrewID_UnAssigned = OnSigner.ID ON CD.ID = SOff.CrewID_SigningOff LEFT OUTER JOIN
                                      (SELECT     PKID, CrewID_UnAssigned, CrewID_SigningOff
                                        FROM          CRW_DTL_CrewAssignment) AS UnA INNER JOIN
                                  CRW_LIB_Crew_Details AS OffSigner ON UnA.CrewID_SigningOff = OffSigner.ID ON CD.ID = UnA.CrewID_UnAssigned LEFT OUTER JOIN
                                      (SELECT     CRW_DTL_CrewChangeEvent_Members.CrewID, MAX(CRW_DTL_CrewChangeEvent.Event_Date) AS Event_Date
                                        FROM          CRW_DTL_CrewChangeEvent INNER JOIN
                                                               CRW_DTL_CrewChangeEvent_Members ON CRW_DTL_CrewChangeEvent.PKID = CRW_DTL_CrewChangeEvent_Members.EventID
                                        WHERE      (CRW_DTL_CrewChangeEvent.Active_Status = 1) AND (CRW_DTL_CrewChangeEvent_Members.Active_Status = 1) AND 
                                                               (CRW_DTL_CrewChangeEvent.Event_Status = 1) AND (CRW_DTL_CrewChangeEvent.Event_Date >= GETDATE())
                                        GROUP BY CRW_DTL_CrewChangeEvent_Members.CrewID) AS EV ON CD.ID = EV.CrewID LEFT OUTER JOIN
                                  LIB_COMPANY ON CD.ManningOfficeID = LIB_COMPANY.Id LEFT OUTER JOIN
                                  CRW_LIB_Crew_Ranks ON CD.Rank_Applied = CRW_LIB_Crew_Ranks.ID LEFT OUTER JOIN
                                  LIB_Country ON CD.Staff_Nationality = LIB_Country.ID ON V.CrewID = CD.ID
                    WHERE (CD.ID > 0)";

                //if (CrewID > 0)
                //    SQL += " AND CD.ID = @CrewID";

                if (CREW_STATUS != "INACTIVE")
                    SQL += " AND (CD.Active_Status = 1) ";

                if (SearchString != "")
                {
                    SQL += " AND (CD.Staff_Name LIKE @SearchString ";
                    SearchString = SearchString.Trim();
                    string[] arrSrarch = SearchString.Split(' ');
                    if (arrSrarch.Length >= 1)
                    {
                        for (int i = 0; i < arrSrarch.Length; i++)
                        {
                            if (arrSrarch[i].Length >= 3)
                                SQL += " OR CD.STAFF_NAME LIKE '%" + arrSrarch[i] + "%' OR CD.STAFF_SURNAME LIKE '%" + arrSrarch[i] + "%' OR CD.STAFF_CODE LIKE '%" + arrSrarch[i] + "%'";
                        }
                    }
                    SQL += ")";
                }
                //SQL += " AND (UPPER(CD.Staff_Name) LIKE @SearchString OR UPPER(CD.Staff_Midname) LIKE @SearchString or UPPER(CD.Staff_Surname) LIKE  @SearchString or STAFF_CODE like  @SearchString or LIB_Country.Country_Name like @SearchString) ";



                switch (CREW_STATUS)
                {
                    case "CURRENT":
                        SQL += " AND (V.SIGN_ON_DATE IS NOT NULL AND  v.SIGN_OFF_DATE IS NULL) ";

                        if (VESSEL_ID != 0)
                        {
                            SQL += " AND v.Vessel_ID = @Vessel_ID";
                        }

                        break;
                    case "SIGNED OFF":
                        SQL += " AND (V.Sign_On_Date IS NOT NULL AND V.SIGN_OFF_DATE IS NOT NULL)";
                        break;

                    case "NO VOYAGE":
                        SQL += " AND ((V.Sign_On_Date IS  NULL AND  v.SIGN_OFF_DATE IS NULL) and (CD.MANNINGOFFICESTATUS = 1 AND CD.CrewManagerApproval = 1) AND (SOff.CrewID_UnAssigned IS  NULL AND ev.Event_Date IS  NULL))";
                        break;

                    case "INACTIVE":
                        SQL += " AND (CD.ACTIVE_STATUS = 0)";
                        break;

                    case "ASSIGNED":
                        SQL += " AND (SOff.CrewID_UnAssigned IS NOT NULL)";
                        break;

                    case "PLANNED":
                        SQL += " AND (EV.Event_Date is not null)";
                        break;

                    case "DECISION PENDING":
                        SQL += " AND (isnull(CD.ManningOfficeStatus, 0)= 0 OR isnull(CD.CrewManagerApproval,0) = 0 )";
                        break;

                    case "NTBR":
                        SQL += " AND (CD.Status_Code = 'NTBR')";
                        break;

                    case "MEDICALLY UNFIT":
                        SQL += " AND (CD.Status_Code = 'MEDICALLY UNFIT')";
                        break;

                    case "REJECTED":
                        SQL += " AND (CD.CrewManagerApproval = -1)";
                        break;
                }

                SQL += " ORDER BY LIB_VESSELS.VESSEL_SHORT_NAME,CRW_LIB_Crew_Ranks.Rank_Sort_Order";

                SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Vessel_ID",VESSEL_ID),
                                            new SqlParameter("@SearchString","%" + SearchString.ToUpper() + "%"),
                                            new SqlParameter("@JoinFrmDT",Dt_FromJoningDate),
                                            new SqlParameter("@JoinTODT",Dt_ToJoningDate),
                                        };

                return objCrewDAL.ExecuteQuery(SQL, sqlprm);


            }
            catch
            {
                throw;
            }
        }

        public DataTable Get_StaffStatusRemarks(int CrewID, int UserCompanyID)
        {
            try
            {
                return objCrewDAL.Get_StaffStatusRemarks_DL(CrewID, UserCompanyID);
            }
            catch
            {
                throw;
            }
        }

        public string GetCurrentUserType(int iUserID)
        {
            return objDalUser.Get_UserData_DL(iUserID, "USER_TYPE");
        }

        public string GetCurrentUserDesignation(int iUserID)
        {
            return objDalUser.Get_UserData_DL(iUserID, "DESIGNATION");
        }

        public DataTable getInterviewDetails(int InterviewID)
        {
            return objCrewDAL.Get_InterviewDetails_DL(InterviewID);
        }

        public DataTable Get_PlannedInterviewDetails(int UserID, int CrewID)
        {
            return objCrewDAL.Get_PlannedInterviewDetails_DL(UserID, CrewID);
        }

        public DataTable Get_AllInterviewesForTheCrewByUserDept(int UserID, int CrewID)
        {
            return objCrewDAL.Get_InterviewesForTheCrewByUserDept_DL(UserID, CrewID);
        }

        public SqlDataReader Get_PlannedInterviewesForTheMonth(int UserID, int CrewID, int Month, int Year, int ShowCalForAll)
        {
            return objCrewDAL.Get_PlannedInterviewesForTheMonth_DL(UserID, CrewID, Month, Year, ShowCalForAll);
        }

        public SqlDataReader Get_InterviewResultsForCrew(int CrewID)
        {
            return objCrewDAL.Get_InterviewResultsForCrew_DL(CrewID);
        }

        public SqlDataReader Get_UserAnswers(int CrewID, int InterviewID)
        {
            return objCrewDAL.Get_UserAnswers_DL(CrewID, InterviewID);
        }

        public DataSet Get_InterviewerRecomendations(int CrewID, int InterviewID)
        {
            return objCrewDAL.Get_InterviewerRecomendations_DL(CrewID, InterviewID);
        }

        public SqlDataReader getInterviewsScheduledForToday(int UserID)
        {
            return objCrewDAL.getInterviewsScheduledForToday_DL(UserID);
        }

        public SqlDataReader getPendingInterviewList(int UserID)
        {
            return objCrewDAL.getPendingInterviewList_DL(UserID);
        }

        public SqlDataReader getPendingInterviewListt_By_UserID(int UserID)
        {
            return objCrewDAL.getPendingInterviewList_By_UserID_DL(UserID);
        }

        public DataTable Get_CrewChangeAlerts(int UserID)
        {
            return objCrewDAL.Get_CrewChangeAlerts_DL(UserID);
        }

        public DataTable getPlannedInterviewList(int iCrewID, int iUserID, string InterviewType)
        {
            return objCrewDAL.Get_PlannedInterviewList_DL(iCrewID, iUserID, InterviewType);
        }

        public DataTable Get_CrewPersonalDetailsByID(int ID)
        {
            try
            {
                return objCrewDAL.Get_CrewPersonalDetailsByID_DL(ID);
            }
            catch
            {
                throw;
            }
        }

        public string Get_CrewPersonalDetailsByID(int ID, string DataColumn)
        {
            try
            {
                DataTable dt = objCrewDAL.Get_CrewPersonalDetailsByID_DL(ID);
                if (dt.Rows.Count > 0)
                {
                    return dt.Rows[0][DataColumn].ToString();
                }
                else
                    return "";
            }
            catch
            {
                return "";
            }
        }

        public DataTable Get_CrewPreJoiningExp(int ID)
        {
            try
            {
                return objCrewDAL.Get_CrewPreJoiningExp_DL(ID);
            }
            catch
            {
                throw;
            }
        }
        public DataTable Get_PreJoiningExpCrewWise(int ID)
        {
            try
            {
                return objCrewDAL.Get_PreJoiningExpCrewWise_DL(ID);
            }
            catch
            {
                throw;
            }
        }
        public DataTable Get_CrewVoyages(int ID)
        {
            try
            {
                return objCrewDAL.Get_CrewVoyages_DL(ID);
            }
            catch
            {
                throw;
            }
        }

        public DataTable Get_VoyagesForSignOnEvent(int CrewID, int Vessel_ID, int UserID)
        {
            try
            {
                return objCrewDAL.Get_VoyagesForSignOnEvent_DL(CrewID, Vessel_ID, UserID);
            }
            catch
            {
                throw;
            }
        }
        public DataTable Get_VoyagesForSignOffEvent(int CrewID, int Vessel_ID, int UserID)
        {
            try
            {
                return objCrewDAL.Get_VoyagesForSignOffEvent_DL(CrewID, Vessel_ID, UserID);
            }
            catch
            {
                throw;
            }
        }
        public DataTable Get_CrewVoyages(int ID, int VoyageID)
        {
            try
            {
                return objCrewDAL.Get_CrewVoyages_DL(ID, VoyageID);
            }
            catch
            {
                throw;
            }
        }
        public DataTable Get_CrewVoyages(int ID, int VoyageID, int UserID)
        {
            try
            {
                return objCrewDAL.Get_CrewVoyages_DL(ID, VoyageID, UserID);
            }
            catch
            {
                throw;
            }
        }
        public DataTable Get_CrewLastVoyage(int CrewID)
        {
            try
            {
                return objCrewDAL.Get_CrewLastVoyage_DL(CrewID);
            }
            catch
            {
                throw;
            }
        }

        public DataTable Get_CrewWages(int ID)
        {
            try
            {
                return objCrewDAL.Get_CrewWages_DL(ID);
            }
            catch
            {
                throw;
            }
        }

        public DataTable Get_CrewPassportAndSeamanDetails(int ID)
        {
            try
            {
                return objCrewDAL.Get_CrewPassportAndSeamanDetails_DL(ID);
            }
            catch
            {
                throw;
            }
        }

        public DataTable Get_CrewPreviousContactDetails(int ID)
        {
            try
            {
                return objCrewDAL.Get_CrewPreviousContactDetails_DL(ID);
            }
            catch
            {
                throw;
            }
        }

        public DataTable Get_CrewDocumentList(int CrewID)
        {
            try
            {
                return objCrewDAL.Get_CrewDocumentList_DL(CrewID);
            }
            catch
            {
                throw;
            }
        }

        public DataTable Get_CrewDocumentList(int CrewID, string FilterString)
        {
            try
            {
                return objCrewDAL.Get_CrewDocumentList_DL(CrewID, FilterString);
            }
            catch
            {
                throw;
            }
        }

        //Added for DMS CR 11292
        public DataTable Get_CrewDocumentList(int CrewID, string FilterString, int isArchived = 0)
        {
            try
            {
                return objCrewDAL.Get_CrewDocumentList_DL(CrewID, FilterString, isArchived);
            }
            catch
            {
                throw;
            }
        }

        public DataTable Get_CrewDocumentList(int CrewID, string FilterString, string DocTypeName)
        {
            try
            {
                return objCrewDAL.Get_CrewDocumentList_DL(CrewID, FilterString, DocTypeName);
            }
            catch
            {
                throw;
            }
        }

        //Added for DMS CR 11292
        public DataTable Get_CrewDocumentList(int CrewID, string FilterString, string DocTypeName, int isArchived = 0)
        {
            try
            {
                //if (DocTypeName != null)
                //{
                //    DocTypeName = DocTypeName.Contains("-") ? DocTypeName.Replace("-", "/") : DocTypeName;
                //}
                return objCrewDAL.Get_CrewDocumentList_DL(CrewID, FilterString, DocTypeName, isArchived);
            }
            catch
            {
                throw;
            }
        }

        public DataTable Get_CrewDocumentList(int CrewID, string FilterString, string DocTypeName, int VogId, int isArchived = 0)
        {
            try
            {
                if (DocTypeName != null)
                {
                    DocTypeName = DocTypeName.Contains("--") ? DocTypeName.Replace("--", "/") : DocTypeName;
                }
                return objCrewDAL.Get_CrewDocumentList_DL(CrewID, FilterString, DocTypeName, VogId, isArchived);
            }
            catch
            {
                throw;
            }
        }

        public DataTable Get_CrewDocumentList(int CrewID, int UserCompanyID, string FilterString)
        {
            string SQL = @"SELECT     DMS_DTL_DOCUMENT.Created_By, ISNULL(DMS_DTL_DOCUMENT.DocTypeID, 0) AS DocTypeID, DMS_DTL_DOCUMENT.DocFilePath, 
                      DMS_DTL_DOCUMENT.DocFileName, DMS_DTL_DOCUMENT.DocName, DMS_DTL_DOCUMENT.DocFileExt, DMS_DTL_DOCUMENT.DocNo, 
                      DMS_DTL_DOCUMENT.Version, DMS_DTL_DOCUMENT.DocID, DMS_DTL_DOCUMENT.Date_Of_Creation, DMS_DTL_DOCUMENT.Date_Of_Modification, 
                      DMS_DTL_DOCUMENT.SizeByte, LIB_USER.First_Name + ' ' + LIB_USER.Last_Name AS Created_By_Name, DMS_LIB_DOCTYPES.DocTypeName, 
                      CRW_LIB_Crew_Details.ManningOfficeID, CRW_Dtl_Crew_Voyages.Staff_Code, 
                      CRW_LIB_Crew_Ranks.Rank_Short_Name, 
                      convert(varchar,CRW_Dtl_Crew_Voyages.Sign_On_Date, 111) as Sign_On_Date,
                      CRW_DTL_DocumentChecklist.VoyageID, LIB_VESSELS.Vessel_Short_Name
                    FROM         CRW_Dtl_Crew_Voyages INNER JOIN
                                          CRW_DTL_DocumentChecklist ON CRW_Dtl_Crew_Voyages.ID = CRW_DTL_DocumentChecklist.VoyageID INNER JOIN
                                          CRW_LIB_Crew_Ranks ON CRW_Dtl_Crew_Voyages.Joining_Rank = CRW_LIB_Crew_Ranks.ID INNER JOIN
                                          LIB_VESSELS ON CRW_Dtl_Crew_Voyages.Vessel_ID = LIB_VESSELS.Vessel_ID RIGHT OUTER JOIN
                                          CRW_LIB_Crew_Details INNER JOIN
                                          DMS_DTL_DOCUMENT ON CRW_LIB_Crew_Details.ID = DMS_DTL_DOCUMENT.CrewID INNER JOIN
                                              (SELECT     CrewID, MAX(DocID) AS DocID, MAX(Version) AS Version
                                                FROM          DMS_DTL_DOCUMENT AS a
                                                GROUP BY CrewID, DocName) AS doclist ON DMS_DTL_DOCUMENT.DocID = doclist.DocID ON 
                                          CRW_DTL_DocumentChecklist.DocID = doclist.DocID LEFT OUTER JOIN
                                          DMS_LIB_DOCTYPES ON DMS_DTL_DOCUMENT.DocTypeID = DMS_LIB_DOCTYPES.DocTypeID LEFT OUTER JOIN
                                          LIB_USER ON DMS_DTL_DOCUMENT.Created_By = LIB_USER.UserID
                    WHERE     (DMS_DTL_DOCUMENT.Active_Status = 1)";

            //SQL += " and (CRW_LIB_Crew_Details.ManningOfficeID = " + UserCompanyID + ")";

            if (CrewID > 0)
                SQL += " AND DMS_DTL_DOCUMENT.CrewID = " + CrewID;

            try
            {
                if (FilterString != null && FilterString != "")
                {
                    string[] cols = { "DMS_DTL_DOCUMENT.DocName", "DMS_DTL_DOCUMENT.DocNo", "First_Name", "Last_Name", "DMS_LIB_DOCTYPES.DocTypeName" };
                    string[] arParam = FilterString.Split(' ');
                    string sqlTemp = "";
                    if (FilterString.Length > 0)
                    {
                        for (int i = 0; i < cols.Length; i++)
                        {
                            for (int j = 0; j < arParam.Length; j++)
                            {
                                if (sqlTemp.Length > 0)
                                    sqlTemp += " OR ";

                                sqlTemp += cols[i] + " like '%" + arParam[j] + "%' ";
                            }
                        }

                        if (sqlTemp.Length > 0)
                            SQL += " AND (" + sqlTemp + ")";
                    }


                }
                SQL += " ORDER BY DocName";
                return objCrewDAL.ExecuteQuery(SQL);
            }
            catch
            {
                throw;
            }

        }

        public DataTable Get_CrewDocumentList(int CrewID, int UserCompanyID, string FilterString, string DocTypeName)
        {
            string SQL = @"SELECT     DMS_DTL_DOCUMENT.Created_By, ISNULL(DMS_DTL_DOCUMENT.DocTypeID, 0) AS DocTypeID, DMS_DTL_DOCUMENT.DocFilePath, 
                      DMS_DTL_DOCUMENT.DocFileName, DMS_DTL_DOCUMENT.DocName, DMS_DTL_DOCUMENT.DocFileExt, DMS_DTL_DOCUMENT.DocNo, 
                      DMS_DTL_DOCUMENT.Version, DMS_DTL_DOCUMENT.DocID, DMS_DTL_DOCUMENT.Date_Of_Creation, DMS_DTL_DOCUMENT.Date_Of_Modification, 
                      DMS_DTL_DOCUMENT.SizeByte, LIB_USER.First_Name + ' ' + LIB_USER.Last_Name AS Created_By_Name, DMS_LIB_DOCTYPES.DocTypeName, 
                      CRW_LIB_Crew_Details.ManningOfficeID, CRW_Dtl_Crew_Voyages.Staff_Code, 
                      CRW_LIB_Crew_Ranks.Rank_Short_Name, 
                      convert(varchar,CRW_Dtl_Crew_Voyages.Sign_On_Date, 111) as Sign_On_Date,
                      CRW_DTL_DocumentChecklist.VoyageID, LIB_VESSELS.Vessel_Short_Name
                    FROM         CRW_Dtl_Crew_Voyages INNER JOIN
                                          CRW_DTL_DocumentChecklist ON CRW_Dtl_Crew_Voyages.ID = CRW_DTL_DocumentChecklist.VoyageID INNER JOIN
                                          CRW_LIB_Crew_Ranks ON CRW_Dtl_Crew_Voyages.Joining_Rank = CRW_LIB_Crew_Ranks.ID INNER JOIN
                                          LIB_VESSELS ON CRW_Dtl_Crew_Voyages.Vessel_ID = LIB_VESSELS.Vessel_ID RIGHT OUTER JOIN
                                          CRW_LIB_Crew_Details INNER JOIN
                                          DMS_DTL_DOCUMENT ON CRW_LIB_Crew_Details.ID = DMS_DTL_DOCUMENT.CrewID INNER JOIN
                                              (SELECT     CrewID, MAX(DocID) AS DocID, MAX(Version) AS Version
                                                FROM          DMS_DTL_DOCUMENT AS a
                                                GROUP BY CrewID, DocName) AS doclist ON DMS_DTL_DOCUMENT.DocID = doclist.DocID ON 
                                          CRW_DTL_DocumentChecklist.DocID = doclist.DocID LEFT OUTER JOIN
                                          DMS_LIB_DOCTYPES ON DMS_DTL_DOCUMENT.DocTypeID = DMS_LIB_DOCTYPES.DocTypeID LEFT OUTER JOIN
                                          LIB_USER ON DMS_DTL_DOCUMENT.Created_By = LIB_USER.UserID
                    WHERE     (DMS_DTL_DOCUMENT.Active_Status = 1)";



            SQL += " and (CRW_LIB_Crew_Details.ManningOfficeID = " + UserCompanyID + ")";

            //if (CrewID > 0)
            SQL += " AND DMS_DTL_DOCUMENT.CrewID = " + CrewID;

            if (DocTypeName == "NEW")
                SQL += " AND  DMS_LIB_DOCTYPES.DocTypeName is null";
            else if (DocTypeName != "" && DocTypeName != "DOCUMENTS")
                SQL += " AND  DMS_LIB_DOCTYPES.DocTypeName ='" + DocTypeName + "'";

            try
            {
                if (FilterString != null && FilterString != "")
                {
                    string[] cols = { "DMS_DTL_DOCUMENT.DocName", "DMS_DTL_DOCUMENT.DocNo", "First_Name", "Last_Name", "DMS_LIB_DOCTYPES.DocTypeName" };
                    string[] arParam = FilterString.Split(' ');
                    string sqlTemp = "";
                    if (FilterString.Length > 0)
                    {
                        for (int i = 0; i < cols.Length; i++)
                        {
                            for (int j = 0; j < arParam.Length; j++)
                            {
                                if (sqlTemp.Length > 0)
                                    sqlTemp += " OR ";

                                sqlTemp += cols[i] + " like '%" + arParam[j] + "%' ";
                            }
                        }

                        if (sqlTemp.Length > 0)
                            SQL += " AND (" + sqlTemp + ")";
                    }

                }

                SQL += " ORDER BY DocName, Version desc";
                return objCrewDAL.ExecuteQuery(SQL);
            }
            catch
            {
                throw;
            }

        }

        public DataTable Get_CrewDocumentList(int CrewID, int UserCompanyID, string FilterString, int DocTypeID)
        {
            //            string SQL = @"SELECT     DMS_DTL_DOCUMENT.Created_By, ISNULL(DMS_DTL_DOCUMENT.DocTypeID, 0) AS DocTypeID, DMS_DTL_DOCUMENT.DocFilePath, 
            //                      DMS_DTL_DOCUMENT.DocFileName, DMS_DTL_DOCUMENT.DocName, DMS_DTL_DOCUMENT.DocFileExt, DMS_DTL_DOCUMENT.DocNo, 
            //                      DMS_DTL_DOCUMENT.Version, DMS_DTL_DOCUMENT.DocID, DMS_DTL_DOCUMENT.Date_Of_Creation, DMS_DTL_DOCUMENT.Date_Of_Modification, 
            //                      DMS_DTL_DOCUMENT.SizeByte, LIB_USER.First_Name + ' ' + LIB_USER.Last_Name AS Created_By_Name, DMS_LIB_DOCTYPES.DocTypeName, 
            //                      CRW_LIB_Crew_Details.ManningOfficeID
            //                    FROM         CRW_LIB_Crew_Details INNER JOIN
            //                                            DMS_DTL_DOCUMENT ON CRW_LIB_Crew_Details.ID = DMS_DTL_DOCUMENT.CrewID INNER JOIN
            //                                                (SELECT     CrewID,MAX(DocID) AS DocID, MAX(Version) AS Version
            //                                                FROM          DMS_DTL_DOCUMENT AS a
            //                                                GROUP BY CrewID,DocName) AS doclist ON DMS_DTL_DOCUMENT.DocID = doclist.DocID LEFT OUTER JOIN
            //                                            DMS_LIB_DOCTYPES ON DMS_DTL_DOCUMENT.DocTypeID = DMS_LIB_DOCTYPES.DocTypeID LEFT OUTER JOIN
            //                                            LIB_USER ON DMS_DTL_DOCUMENT.Created_By = LIB_USER.UserID
            //                    WHERE     (DMS_DTL_DOCUMENT.Active_Status = 1)";


            string SQL = @"SELECT     DMS_DTL_DOCUMENT.Created_By, ISNULL(DMS_DTL_DOCUMENT.DocTypeID, 0) AS DocTypeID, DMS_DTL_DOCUMENT.DocFilePath, 
                      DMS_DTL_DOCUMENT.DocFileName, DMS_DTL_DOCUMENT.DocName, DMS_DTL_DOCUMENT.DocFileExt, DMS_DTL_DOCUMENT.DocNo, 
                      DMS_DTL_DOCUMENT.Version, DMS_DTL_DOCUMENT.DocID, DMS_DTL_DOCUMENT.Date_Of_Creation, DMS_DTL_DOCUMENT.Date_Of_Modification, 
                      DMS_DTL_DOCUMENT.SizeByte, LIB_USER.First_Name + ' ' + LIB_USER.Last_Name AS Created_By_Name, DMS_LIB_DOCTYPES.DocTypeName, 
                      CRW_LIB_Crew_Details.ManningOfficeID, CRW_Dtl_Crew_Voyages.Staff_Code, 
                      CRW_LIB_Crew_Ranks.Rank_Short_Name, 
                      convert(varchar,CRW_Dtl_Crew_Voyages.Sign_On_Date, 111) as Sign_On_Date,
                      CRW_DTL_DocumentChecklist.VoyageID, LIB_VESSELS.Vessel_Short_Name
                    FROM         CRW_Dtl_Crew_Voyages INNER JOIN
                                          CRW_DTL_DocumentChecklist ON CRW_Dtl_Crew_Voyages.ID = CRW_DTL_DocumentChecklist.VoyageID INNER JOIN
                                          CRW_LIB_Crew_Ranks ON CRW_Dtl_Crew_Voyages.Joining_Rank = CRW_LIB_Crew_Ranks.ID INNER JOIN
                                          LIB_VESSELS ON CRW_Dtl_Crew_Voyages.Vessel_ID = LIB_VESSELS.Vessel_ID RIGHT OUTER JOIN
                                          CRW_LIB_Crew_Details INNER JOIN
                                          DMS_DTL_DOCUMENT ON CRW_LIB_Crew_Details.ID = DMS_DTL_DOCUMENT.CrewID INNER JOIN
                                              (SELECT     CrewID, MAX(DocID) AS DocID, MAX(Version) AS Version
                                                FROM          DMS_DTL_DOCUMENT AS a
                                                GROUP BY CrewID, DocName) AS doclist ON DMS_DTL_DOCUMENT.DocID = doclist.DocID ON 
                                          CRW_DTL_DocumentChecklist.DocID = doclist.DocID LEFT OUTER JOIN
                                          DMS_LIB_DOCTYPES ON DMS_DTL_DOCUMENT.DocTypeID = DMS_LIB_DOCTYPES.DocTypeID LEFT OUTER JOIN
                                          LIB_USER ON DMS_DTL_DOCUMENT.Created_By = LIB_USER.UserID
                    WHERE     (DMS_DTL_DOCUMENT.Active_Status = 1)";


            SQL += " and (CRW_LIB_Crew_Details.ManningOfficeID = " + UserCompanyID + ")";

            //if (CrewID > 0)
            SQL += " AND DMS_DTL_DOCUMENT.CrewID = " + CrewID;

            if (DocTypeID != 0)
                SQL += " AND  DMS_DTL_DOCUMENT.DocTypeID ='" + DocTypeID + "'";

            try
            {
                if (FilterString != null && FilterString != "")
                {
                    string[] cols = { "DMS_DTL_DOCUMENT.DocName", "DMS_DTL_DOCUMENT.DocNo", "First_Name", "Last_Name", "DMS_LIB_DOCTYPES.DocTypeName" };
                    string[] arParam = FilterString.Split(' ');
                    string sqlTemp = "";
                    if (FilterString.Length > 0)
                    {
                        for (int i = 0; i < cols.Length; i++)
                        {
                            for (int j = 0; j < arParam.Length; j++)
                            {
                                if (sqlTemp.Length > 0)
                                    sqlTemp += " OR ";

                                sqlTemp += cols[i] + " like '%" + arParam[j] + "%' ";
                            }
                        }

                        if (sqlTemp.Length > 0)
                            SQL += " AND (" + sqlTemp + ")";
                    }


                }
                SQL += " ORDER BY DocName, Version Desc";
                return objCrewDAL.ExecuteQuery(SQL);
            }
            catch
            {
                throw;
            }

        }

        public DataTable Get_CrewDocumentList(int CrewID, int UserCompanyID, string FilterString, string DocTypeName, string DocName)
        {

            string SQL = @"SELECT     DMS_DTL_DOCUMENT.Created_By, ISNULL(DMS_DTL_DOCUMENT.DocTypeID, 0) AS DocTypeID, DMS_DTL_DOCUMENT.DocFilePath, 
                      DMS_DTL_DOCUMENT.DocFileName, DMS_DTL_DOCUMENT.DocName, DMS_DTL_DOCUMENT.DocFileExt, DMS_DTL_DOCUMENT.DocNo, 
                      DMS_DTL_DOCUMENT.Version, DMS_DTL_DOCUMENT.DocID, DMS_DTL_DOCUMENT.Date_Of_Creation, DMS_DTL_DOCUMENT.Date_Of_Modification, 
                      DMS_DTL_DOCUMENT.SizeByte, LIB_USER.First_Name + ' ' + LIB_USER.Last_Name AS Created_By_Name, DMS_LIB_DOCTYPES.DocTypeName, 
                      CRW_LIB_Crew_Details.ManningOfficeID, CRW_Dtl_Crew_Voyages.Staff_Code, 
                      CRW_LIB_Crew_Ranks.Rank_Short_Name, 
                      convert(varchar,CRW_Dtl_Crew_Voyages.Sign_On_Date, 111) as Sign_On_Date,
                      CRW_DTL_DocumentChecklist.VoyageID, LIB_VESSELS.Vessel_Short_Name
                    FROM         CRW_Dtl_Crew_Voyages INNER JOIN
                                          CRW_DTL_DocumentChecklist ON CRW_Dtl_Crew_Voyages.ID = CRW_DTL_DocumentChecklist.VoyageID INNER JOIN
                                          CRW_LIB_Crew_Ranks ON CRW_Dtl_Crew_Voyages.Joining_Rank = CRW_LIB_Crew_Ranks.ID INNER JOIN
                                          LIB_VESSELS ON CRW_Dtl_Crew_Voyages.Vessel_ID = LIB_VESSELS.Vessel_ID RIGHT OUTER JOIN
                                          CRW_LIB_Crew_Details INNER JOIN
                                          DMS_DTL_DOCUMENT ON CRW_LIB_Crew_Details.ID = DMS_DTL_DOCUMENT.CrewID INNER JOIN
                                              (SELECT     CrewID, MAX(DocID) AS DocID, MAX(Version) AS Version
                                                FROM          DMS_DTL_DOCUMENT AS a
                                                GROUP BY CrewID, DocName) AS doclist ON DMS_DTL_DOCUMENT.DocID = doclist.DocID ON 
                                          CRW_DTL_DocumentChecklist.DocID = doclist.DocID LEFT OUTER JOIN
                                          DMS_LIB_DOCTYPES ON DMS_DTL_DOCUMENT.DocTypeID = DMS_LIB_DOCTYPES.DocTypeID LEFT OUTER JOIN
                                          LIB_USER ON DMS_DTL_DOCUMENT.Created_By = LIB_USER.UserID
                    WHERE     (DMS_DTL_DOCUMENT.Active_Status = 1)";





            SQL += " and (CRW_LIB_Crew_Details.ManningOfficeID = " + UserCompanyID + ")";

            //if (CrewID > 0)
            SQL += " AND DMS_DTL_DOCUMENT.CrewID = " + CrewID;

            if (DocTypeName == "NEW")
                SQL += " AND  DMS_LIB_DOCTYPES.DocTypeName is null";
            else if (DocTypeName != "" && DocTypeName != "DOCUMENTS")
                SQL += " AND  DMS_LIB_DOCTYPES.DocTypeName ='" + DocTypeName + "'";

            if (DocName != null && DocName != "")
                SQL += " AND DMS_DTL_DOCUMENT.DocName = '" + DocName + "'";


            try
            {
                if (FilterString != null && FilterString != "")
                {
                    string[] cols = { "DMS_DTL_DOCUMENT.DocName", "DMS_DTL_DOCUMENT.DocNo", "First_Name", "Last_Name", "DMS_LIB_DOCTYPES.DocTypeName" };
                    string[] arParam = FilterString.Split(' ');
                    string sqlTemp = "";
                    if (FilterString.Length > 0)
                    {
                        for (int i = 0; i < cols.Length; i++)
                        {
                            for (int j = 0; j < arParam.Length; j++)
                            {
                                if (sqlTemp.Length > 0)
                                    sqlTemp += " OR ";

                                sqlTemp += cols[i] + " like '%" + arParam[j] + "%' ";
                            }
                        }

                        if (sqlTemp.Length > 0)
                            SQL += " AND (" + sqlTemp + ")";
                    }

                }

                SQL += " ORDER BY DocName, Version desc";
                return objCrewDAL.ExecuteQuery(SQL);
            }
            catch
            {
                throw;
            }

        }

        public DataTable Get_CrewDocumentDetailsByDocID(int DocID)
        {
            try
            {
                return objCrewDAL.Get_CrewDocumentDetailsByDocID_DL(DocID);
            }
            catch
            {
                throw;
            }
        }

        public int IsMandatoryDocumentsUploaded(int CrewID)
        {
            try
            {
                return objCrewDAL.IsMandatoryDocumentsUploaded_DL(CrewID);
            }
            catch
            {
                throw;
            }
        }

        public DataTable Get_DocAttributeValueByDocID(int DocID)
        {
            try
            {
                return objCrewDAL.Get_DocAttributeValueByDocID_DL(DocID);
            }
            catch
            {
                throw;
            }
        }

        public DataTable Get_Crew_DocAttributeValueForUpdate(int DocID, int CrewID)
        {
            try
            {
                DataTable dt = objCrewDAL.Get_Crew_DocAttributeValueForUpdate_DL(DocID);

                DataTable dtUpdate = new DataTable();

                dtUpdate.Columns.Add("DocID", typeof(int));
                dtUpdate.Columns.Add("CrewID", typeof(int));

                foreach (DataRow dr in dt.Rows)
                {
                    dtUpdate.Columns.Add(dr["AttributeName"].ToString(), typeof(string));
                }

                DataRow drNew = dtUpdate.NewRow();

                drNew["DocID"] = DocID;
                drNew["CrewID"] = CrewID;

                foreach (DataRow dr2 in dt.Rows)
                {
                    drNew[dr2["AttributeName"].ToString()] = dr2["AttributeValue_String"].ToString();
                }
                dtUpdate.Rows.Add(drNew);

                return dtUpdate;
            }
            catch
            {
                throw;
            }
        }

        public DataSet Get_DocumentExpiryList(int userid)
        {
            try
            {
                return objCrewDAL.Get_DocumentExpiryList_DL(userid);
            }
            catch
            {
                throw;
            }
        }

        public DataTable Get_Crew_DependentsByCrewID(int CrewID, int IsNOK)
        {
            try
            {
                return objCrewDAL.Get_Crew_DependentsByCrewID_DL(CrewID, IsNOK);
            }
            catch
            {
                throw;
            }
        }
        public DataSet Get_Crew_CompanyDetails(int? CompanyID)
        {
            try
            {
                return objCrewDAL.Get_Crew_CompanyDetails(CompanyID);
            }
            catch
            {
                throw;
            }
        }
        public DataTable Get_ManningAgentList(int UserCompanyID)
        {
            try
            {
                return objCrewDAL.Get_ManningAgentList_DL(UserCompanyID);
            }
            catch
            {
                throw;
            }
        }

        public DataTable Get_VesselOwnerList(int UserCompanyID)
        {
            try
            {
                return objCrewDAL.Get_VesselOwnerList_DL(UserCompanyID);
            }
            catch
            {
                throw;
            }
        }

        public DataTable Get_VesselManagerList(int UserCompanyID)
        {
            try
            {
                return objCrewDAL.Get_VesselManagerList_DL(UserCompanyID);
            }
            catch
            {
                throw;
            }
        }

        public DataTable Get_CrewAssignments(int VesselID, int UserID)
        {
            try
            {
                return objCrewDAL.Get_CrewAssignments_DL(VesselID, UserID);
            }
            catch
            {
                throw;
            }
        }

        public DataTable Get_CrewAssignments(int VesselID, int RankID, string FreeText, int Nationality, int UserID)
        {
            try
            {
                return objCrewDAL.Get_CrewAssignments_DL(VesselID, RankID, FreeText, Nationality, UserID);
            }
            catch
            {
                throw;
            }
        }

        public DataTable Get_EventDetails(int EventID)
        {
            try
            {
                return objCrewDAL.Get_EventDetails_DL(EventID);
            }
            catch
            {
                throw;
            }
        }

        public DataSet Get_CrewChangeEvents(int FleetID, int VesselID, int EventStatus, int PortID, string Event_From_Date, string Event_To_Date, string SearchText, int SessionUser)
        {
            try
            {

                DateTime Dt_Event_From_Date = DateTime.Parse("1900/01/01");
                if (Event_From_Date != "")
                    Dt_Event_From_Date = DateTime.Parse(Event_From_Date);

                DateTime Dt_Event_To_Date = DateTime.Parse("1900/01/01");
                if (Event_To_Date != "")
                    Dt_Event_To_Date = DateTime.Parse(Event_To_Date);

                return objCrewDAL.Get_CrewChangeEvents_DL(FleetID, VesselID, EventStatus, PortID, Dt_Event_From_Date, Dt_Event_To_Date, SearchText, SessionUser);
            }
            catch
            {
                throw;
            }
        }

        public DataSet Get_CrewChangeEvents_ByVessel(int VesselID, int SessionUser)
        {
            try
            {

                return objCrewDAL.Get_CrewChangeEvents_ByVessel_DL(VesselID, SessionUser);
            }
            catch
            {
                throw;
            }
        }

        public DataSet Get_CrewChangeEvents_ByCrew(int CrewID, int SessionUser)
        {
            try
            {

                return objCrewDAL.Get_CrewChangeEvents_ByCrew_DL(CrewID, SessionUser);
            }
            catch
            {
                throw;
            }
        }

        public DataSet Get_CrewChangeEvent(int EventID, int SessionUser)
        {
            try
            {

                return objCrewDAL.Get_CrewChangeEvent_DL(EventID, SessionUser);
            }
            catch
            {
                throw;
            }
        }

        public int CloseEvent(int EventID, int SessionUser)
        {
            try
            {
                return objCrewDAL.CloseEvent_DL(EventID, SessionUser);
            }
            catch
            {
                throw;
            }
        }
        public int DeleteEvent(int EventID, int SessionUser)
        {
            try
            {
                return objCrewDAL.DeleteEvent_DL(EventID, SessionUser);
            }
            catch
            {
                throw;
            }
        }
        public DataTable Get_CrewRemarks(int CrewID, int UserID, int PAGE_SIZE, int PAGE_INDEX, ref int SelectRecordCount)
        {
            try
            {

                return objCrewDAL.Get_CrewRemarks_DL(CrewID, UserID, PAGE_SIZE, PAGE_INDEX, ref SelectRecordCount);
            }
            catch
            {
                throw;
            }
        }

        public DataTable Get_CROO_Feedbacks(int CrewID, int UserID, string SearchText, int PAGE_SIZE, int PAGE_INDEX, ref int SelectRecordCount)
        {
            try
            {
                return objCrewDAL.Get_CROO_Feedbacks_DL(CrewID, UserID, SearchText, PAGE_SIZE, PAGE_INDEX, ref SelectRecordCount);
            }
            catch
            {
                throw;
            }
        }

        public DataTable Load_CrewActivityLog(int CrewID, int UserID, int PAGE_SIZE, int PAGE_INDEX, ref int SelectRecordCount)
        {
            try
            {

                return objCrewDAL.Load_CrewActivityLog_DL(CrewID, UserID, PAGE_SIZE, PAGE_INDEX, ref SelectRecordCount);
            }
            catch
            {
                throw;
            }
        }

        public DataTable Load_CrewTravelLog(int CrewID, int UserID, int PAGE_SIZE, int PAGE_INDEX, ref int SelectRecordCount)
        {
            try
            {

                return objCrewDAL.Load_CrewTravelLog_DL(CrewID, UserID, PAGE_SIZE, PAGE_INDEX, ref SelectRecordCount);
            }
            catch
            {
                throw;
            }
        }
        public DataTable Get_CrewJoiningChecklist(int CrewID)
        {
            try
            {
                return objCrewDAL.Get_CrewJoiningChecklist_DL(CrewID);
            }
            catch
            {
                throw;
            }
        }

        public DataTable Get_Crew_DocumentChecklist(int CrewID)
        {
            try
            {
                return objCrewDAL.Get_Crew_DocumentChecklist_DL(CrewID);
            }
            catch
            {
                throw;
            }
        }

        public DataTable Get_Crew_VoyageDocuments(int CrewID, int VoyageID)
        {
            try
            {
                return objCrewDAL.Get_Crew_VoyageDocuments_DL(CrewID, VoyageID);
            }
            catch
            {
                throw;
            }
        }
        public DataTable Get_Crew_PerpetualDocuments(int CrewID, string SearchText, int DocumentGroupId)
        {
            try
            {
                return objCrewDAL.Get_Crew_PerpetualDocuments_DL(CrewID, SearchText, DocumentGroupId);
            }
            catch
            {
                throw;
            }
        }
        public DataTable Get_Crew_PerpetualDocument(int CrewID, int DocTypeId)
        {
            try
            {
                return objCrewDAL.Get_Crew_PerpetualDocument_DL(CrewID, DocTypeId);
            }
            catch
            {
                throw;
            }
        }
        public DataTable Get_CrewAgreementData(int CrewID, int VoyageID)
        {
            try
            {
                return objCrewDAL.Get_CrewAgreementData_DL(CrewID, VoyageID);
            }
            catch
            {
                throw;
            }
        }

        public string Get_VID_SEQ(int VesselID)
        {
            return objCrewDAL.Get_VID_SEQ_DL(VesselID);
        }

        public int Get_ONBD_Count(int VesselID)
        {
            return objCrewDAL.Get_ONBD_Count_DL(VesselID);
        }

        public string Get_VID_SEQ(int VesselID, string SEQ_Date)
        {
            return objCrewDAL.Get_VID_SEQ_DL(VesselID);
        }

        public int Get_ONBD_Count(int VesselID, string ONBD_Date)
        {
            return objCrewDAL.Get_ONBD_Count_DL(VesselID);
        }

        public string Get_SEQAndONBD(int VesselID)
        {
            try
            {
                return objCrewDAL.Get_SEQAndONBD_DL(VesselID);

            }
            catch
            {
                return "";
                //throw;
            }
        }

        public DataTable Get_CrewNationality(int SessionUserID)
        {
            try
            {
                return objCrewDAL.Get_CrewNationality_DL(SessionUserID);
            }
            catch
            {
                throw;
            }
        }

        public DataTable Get_CrewNotificationDetails(int ID)
        {
            try
            {
                return objCrewDAL.Get_CrewNotificationDetails_DL(ID);
            }
            catch
            {
                throw;
            }
        }
        public DataSet Get_Crew_Mail_attachment(int CrewID)
        {
            try
            {
                return objCrewDAL.Get_Crew_Mail_attachment(CrewID);
            }
            catch
            {
                throw;
            }
        }
        public DataSet Get_MailDetailsForEventID(int EventID, int EventTypeID)
        {
            try
            {
                return objCrewDAL.Get_MailDetailsForEventID_DL(EventID, EventTypeID);
            }
            catch
            {
                throw;
            }
        }

        public DataTable Get_Sign_Off_Reasons()
        {
            try
            {
                return objCrewDAL.Get_Sign_Off_Reasons_DL();
            }
            catch
            {
                throw;
            }
        }


        public DataTable Get_Airport_Continents(string SearchString)
        {
            try
            {
                return objCrewDAL.Get_Airport_Continents_DL(SearchString);
            }
            catch
            {
                throw;
            }
        }
        public DataTable Get_Airport_Countries(string Continent, string SearchString)
        {
            try
            {
                return objCrewDAL.Get_Airport_Countries_DL(Continent, SearchString);
            }
            catch
            {
                throw;
            }
        }
        public DataTable Get_Airport_Cities(int Country, string SearchString)
        {
            try
            {
                return objCrewDAL.Get_Airport_Cities_DL(Country, SearchString);
            }
            catch
            {
                throw;
            }
        }

        public DataTable Get_AirportList(int AirportID)
        {
            try
            {
                return objCrewDAL.Get_AirportList_DL(AirportID);
            }
            catch
            {
                throw;
            }
        }
        public DataTable Get_AirportList(string SearchString)
        {
            try
            {
                return objCrewDAL.Get_AirportList_DL(SearchString);
            }
            catch
            {
                throw;
            }
        }
        public DataTable Get_AirportList(int Country, string SearchString)
        {
            try
            {
                return objCrewDAL.Get_AirportList_DL(Country, SearchString);
            }
            catch
            {
                throw;
            }
        }
        public DataTable Get_AirportList(string City, string SearchString)
        {
            try
            {
                return objCrewDAL.Get_AirportList_DL(City, SearchString);
            }
            catch
            {
                throw;
            }
        }
        public DataTable Get_AirportList(int Country, string City, string SearchString)
        {
            try
            {
                return objCrewDAL.Get_AirportList_DL(Country, City, SearchString);
            }
            catch
            {
                throw;
            }
        }
        public DataTable Get_AirportList(int Country, string City, string Iata_Code, string SearchString)
        {
            try
            {
                return objCrewDAL.Get_AirportList_DL(Country, City, Iata_Code, SearchString);
            }
            catch
            {
                throw;
            }
        }

        public DataTable Get_Crew_MissingData(int CrewID)
        {
            try
            {
                return objCrewDAL.Get_Crew_MissingData_DL(CrewID);
            }
            catch
            {
                throw;
            }
        }

        public int Get_RejectedInterviewCount(int CrewID)
        {
            try
            {
                return objCrewDAL.Get_RejectedInterviewCount_DL(CrewID);
            }
            catch
            {
                throw;
            }
        }

        public DataTable Get_PortCall_List(int Vessel_ID)
        {
            try
            {
                return objCrewDAL.Get_PortCall_List_DL(Vessel_ID);
            }
            catch
            {
                throw;
            }
        }
        public DataTable Get_PortCall_List(int Vessel_ID, string EventDate)
        {
            try
            {
                DateTime Dt_EventDate = DateTime.Parse(EventDate, iFormatProvider);
                return objCrewDAL.Get_PortCall_List_DL(Vessel_ID, Dt_EventDate);
            }
            catch
            {
                throw;
            }
        }
        public DataTable Get_PortCall_Details(int Port_Call_ID, int VesselID)
        {
            try
            {
                return objCrewDAL.Get_PortCall_Details_DL(Port_Call_ID, VesselID);
            }
            catch
            {
                throw;
            }
        }

        public DataSet Get_CrewWorkflow(int CrewID,int UserId)
        {
            try
            {
                return objCrewDAL.Get_CrewWorkflow_DL(CrewID,UserId);
            }
            catch
            {
                throw;
            }
        }

        public DataTable Get_ContractTemplate(int ContractId)
        {
            try
            {
                return objCrewDAL.Get_ContractTemplate_DL(ContractId);
            }
            catch
            {
                throw;
            }
        }

        public DataSet Get_MissingDataReport(int Vessel_Manager, int UserID)
        {
            try
            {
                return objCrewDAL.Get_MissingDataReport_DL(Vessel_Manager, UserID);
            }
            catch
            {
                throw;
            }
        }
        public DataSet Get_MissingDataDetails(int Vessel_Manager, int UserID)
        {
            try
            {
                return objCrewDAL.Get_MissingDataDetails_DL(Vessel_Manager, UserID);
            }
            catch
            {
                throw;
            }
        }

        public DataSet Get_MissingDataDetails(int Vessel_Manager, int UserID, int ManningOfficeID, string Col)
        {
            try
            {
                return objCrewDAL.Get_MissingDataDetails_DL(Vessel_Manager, UserID, ManningOfficeID, Col);
            }
            catch
            {
                throw;
            }
        }

        public DataSet Get_CrewComplaints(int CrewID, int UserID)
        {
            try
            {

                return objCrewDAL.Get_CrewComplaints_DL(CrewID, UserID);
            }
            catch
            {
                throw;
            }
        }

        public DataSet Get_CrewComplaintList(int CrewID, int UserID)
        {
            try
            {

                return objCrewDAL.Get_CrewComplaintList_DL(CrewID, UserID);
            }
            catch
            {
                throw;
            }
        }
        public DataSet Get_CrewComplaintLog(int Worklist_ID, int Vessel_ID, int UserID)
        {
            try
            {

                return objCrewDAL.Get_CrewComplaintLog_DL(Worklist_ID, Vessel_ID, UserID);
            }
            catch
            {
                throw;
            }
        }

        public DataSet Get_CrewCardStatus(int CrewID, int UserID)
        {
            try
            {

                return objCrewDAL.Get_CrewCardStatus_DL(CrewID, UserID);
            }
            catch
            {
                throw;
            }
        }
        //2nd
        public DataTable Get_CrewCardIndex(string SearchText, int? FlletCode, int? VesselID, int? Nationality, int? ApprovalStatus, int UserID, int UserCompanyID, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            return objCrewDAL.Get_CrewCardIndex_DL(SearchText, FlletCode, VesselID, Nationality, ApprovalStatus, UserID, UserCompanyID, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);
        }

        public DataTable Get_CrewCardIndex(int FlletCode, int VesselID, int Nationality, int ApprovalStatus, string SearchText, int UserID)
        {
            try
            {
                return objCrewDAL.Get_CrewCardIndex_DL(FlletCode, VesselID, Nationality, ApprovalStatus, SearchText, UserID);
            }
            catch
            {
                throw;
            }
        }

        public DataTable Get_CardAttachments(int CardID, int UserID)
        {
            try
            {

                return objCrewDAL.Get_CardAttachments_DL(CardID, UserID);
            }
            catch
            {
                throw;
            }
        }
        public DataTable Get_CrewUniformSize(int CrewID)
        {
            try
            {

                return objCrewDAL.Get_CrewUniformSize_DL(CrewID);
            }
            catch
            {
                throw;
            }
        }

        public DataTable Get_UniformSizeReport(DataTable dtFilters, int UserID)
        {
            try
            {
                return objCrewDAL.Get_CrewUniformSize_DL(dtFilters, UserID);
            }
            catch
            {
                throw;
            }
        }

        public Boolean Validate_BeforeApproval(int CrewID)
        {
            int Office_InterviewCount = 0;
            try
            {
                SqlDataReader dr = DAL_Crew_Interview.Get_InterviewResultsForCrew_DL(CrewID);
                while (dr.Read())
                {
                    if (dr["office_dept"].ToString() != "Manning Office")
                    {
                        Office_InterviewCount++;
                    }
                }

                if (Office_InterviewCount > 0)
                    return true;
                else
                    return false;
            }
            catch
            {
                throw;
            }
        }

        public Boolean Validate_ManningOfficeAccess(int CrewID, int UserCompanyID)
        {
            int Count = 0;

            DataTable dt = objCrewDAL.Get_ManningAgentList_DL(UserCompanyID);
            string ManningOfficeID = Get_CrewPersonalDetailsByID(CrewID, "ManningOfficeID");

            try
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (ManningOfficeID == dr["ID"].ToString())
                    {
                        Count++;
                    }
                }
                if (Count > 0)
                    return true;
                else
                    return false;
            }
            catch
            {
                throw;
            }
        }

        public DataTable Get_CrewHeightWaistWeight(int CrewID)
        {
            try
            {

                return objCrewDAL.Get_CrewHeightWaistWeight_DL(CrewID);
            }
            catch
            {
                throw;
            }
        }

        public DataSet Get_CrewInfo_ToolTip(int CrewID, int UserID)
        {
            try
            {

                return objCrewDAL.Get_CrewInfo_ToolTip_DL(CrewID, UserID);
            }
            catch
            {
                throw;
            }
        }

        public DataSet Get_TradeZoneList(int RankID, int UserID)
        {
            try
            {

                return objCrewDAL.Get_TradeZoneList_DL(RankID, UserID);
            }
            catch
            {
                throw;
            }
        }

        public DataTable Get_USVisaAlerts(int UserID)
        {
            return objCrewDAL.Get_USVisaAlerts_DL(UserID);
        }

        public DataTable Get_CrewAgreementRecords(int CrewID, int VoyageID, int Stage, int UserID)
        {
            try
            {
                return objCrewDAL.Get_CrewAgreementRecords_DL(CrewID, VoyageID, Stage, UserID);
            }
            catch
            {
                throw;
            }
        }
        public DataTable Get_CrewAgreementRecords(int ID, int UserID)
        {
            try
            {
                return objCrewDAL.Get_CrewAgreementRecords_DL(ID, UserID);
            }
            catch
            {
                throw;
            }
        }
        public DataTable Get_CrewAgreementStatus(int VoyageID, int UserID)
        {
            try
            {
                return objCrewDAL.Get_CrewAgreementStatus_DL(VoyageID, UserID);
            }
            catch
            {
                throw;
            }
        }
        public DataTable Download_Contract_ByManningOffice(int ID, int UserID)
        {
            try
            {
                return objCrewDAL.Download_Contract_ByManningOffice_DL(ID, UserID);
            }
            catch
            {
                throw;
            }
        }
        public DataTable Get_Contracts_ForDigiSign_Alerts(int UserID)
        {
            return objCrewDAL.Get_Contracts_ForDigiSign_Alerts_DL(UserID);
        }
        public DataTable Get_Contracts_ToVerify_Alerts(int UserID)
        {
            return objCrewDAL.Get_Contracts_ToVerify_Alerts_DL(UserID);
        }

        public DataTable Get_Interviews(int RankID, int UserID)
        {
            try
            {
                return objCrewDAL.Get_Interviews_DL(RankID, UserID);
            }
            catch
            {
                throw;
            }
        }

        public DataTable Get_CrewPreJoiningExp_FromInterview(int CrewID)
        {
            try
            {
                return objCrewDAL.Get_CrewPreJoiningExp_FromInterview_DL(CrewID);
            }
            catch
            {
                throw;
            }
        }

        public DataTable Get_SideLetter_Template(int UserCompanyID, int TemplateID)
        {
            try
            {
                return objCrewDAL.Get_SideLetter_Template_DL(UserCompanyID, TemplateID);
            }
            catch
            {
                throw;
            }
        }

        public DataTable Get_SideLetter_ForVoyage(int VoyageID, int CrewID, int UserID)
        {
            try
            {
                return objCrewDAL.Get_SideLetter_ForVoyage_DL(VoyageID, CrewID, UserID);
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region  - CREW DETAILS - INSERT -
        public int INS_NewCrewDetails(CrewProperties objCrew)
        {
            try
            {
                int ID = objCrewDAL.INS_NewCrewDetails_DL(objCrew);
                return ID;
            }
            catch
            {
                throw;
            }
        }

        public int INS_CrewPreviousContacts(int id, int CrewID, string Name, string PIC, string Telephone, string Fax, string Email, int Created_By)
        {
            try
            {
                return objCrewDAL.UPDATE_CrewPreviousContacts_DL(id, CrewID, Name, PIC, Telephone, Fax, Email, Created_By);
            }
            catch
            {
                throw;
            }
        }


        public int INS_CrewPreJoiningExp(int CrewID, string Vessel_Name, string Flag, string Vessel_Type, string DWT, int GRT, string CompanyName, int Rank, string Date_From, string Date_To, int Months, int Days, int Created_By, string ME_MakeModel, int ME_BHP)
        {
            try
            {
                DateTime Dt_Date_From = DateTime.Parse(Date_From, iFormatProvider);
                DateTime Dt_Date_To = DateTime.Parse(Date_To, iFormatProvider);

                System.TimeSpan diffResult = Dt_Date_To.Subtract(Dt_Date_From);

                int totDays = diffResult.Days;
                const double daysToMonths = 30;

                double months = totDays / daysToMonths;

                string s = months.ToString("0.00", CultureInfo.InvariantCulture);
                string[] parts = s.Split('.');
                Months = int.Parse(parts[0]);
                Months = Convert.ToInt32(months);

                Days = Convert.ToInt32((months - Months) * daysToMonths);

                return objCrewDAL.INS_CrewPreJoiningExp_DL(CrewID, Vessel_Name, Flag, Vessel_Type, DWT, GRT, CompanyName, Rank, Dt_Date_From, Dt_Date_To, Months, Days, Created_By, ME_MakeModel, ME_BHP, null);
            }
            catch
            {
                throw;
            }
        }
        public int INS_CrewPreJoiningExp(int CrewID, string Vessel_Name, string Flag, string Vessel_Type, string DWT, int GRT, string CompanyName, int Rank, string Date_From, string Date_To, int Months, int Days, int Created_By, string ME_MakeModel, int ME_BHP, int? CompanyID)
        {
            try
            {
                DateTime Dt_Date_From = DateTime.Parse(Date_From, iFormatProvider);
                DateTime Dt_Date_To = DateTime.Parse(Date_To, iFormatProvider);

                System.TimeSpan diffResult = Dt_Date_To.Subtract(Dt_Date_From);

                int totDays = diffResult.Days+1;
                const double daysToMonths = 30;

                double months = totDays / daysToMonths;

                string s = months.ToString("0.00", CultureInfo.InvariantCulture);
                string[] parts = s.Split('.');
                Months = int.Parse(parts[0]);
                Months = Convert.ToInt32(months);

                Days = Convert.ToInt32((months - Months) * daysToMonths);

                return objCrewDAL.INS_CrewPreJoiningExp_DL(CrewID, Vessel_Name, Flag, Vessel_Type, DWT, GRT, CompanyName, Rank, Dt_Date_From, Dt_Date_To, Months, Days, Created_By, ME_MakeModel, ME_BHP, CompanyID);
            }
            catch
            {
                throw;
            }
        }
        public int INS_CrewVoyages(int CrewID, int Joining_Type, string Joining_Date, string SignOn_Date, string SignOffDate, int Joining_Rank, int Vessel_Code, string COC_Date, int Joining_Port, int Created_By, int RankScaleId, int ContractId, string DOAHomePort, int VesselTypeAssignment)
        {
            try
            {
                if (SignOn_Date == null || SignOn_Date == "")
                    SignOn_Date = "1900/01/01";
                if (SignOffDate == null || SignOffDate == "")
                    SignOffDate = "1900/01/01";
                if (Joining_Date == null || Joining_Date == "")
                    Joining_Date = "1900/01/01";
                if (COC_Date == null || COC_Date == "")
                    COC_Date = "1900/01/01";
                if (DOAHomePort == null || DOAHomePort == "")
                    DOAHomePort = "1900/01/01";
                DateTime dtSignOn_Date = DateTime.Parse(SignOn_Date, iFormatProvider);
                DateTime dtSignOffDate = DateTime.Parse(SignOffDate, iFormatProvider);
                DateTime dtJoining_Date = DateTime.Parse(Joining_Date, iFormatProvider);
                DateTime dtCOC_Date = DateTime.Parse(COC_Date, iFormatProvider);
                DateTime dtDOAHomePort = DateTime.Parse(DOAHomePort, iFormatProvider);

                return objCrewDAL.INS_CrewVoyages_DL(CrewID, Joining_Type, dtJoining_Date, dtSignOn_Date, dtSignOffDate, Joining_Rank, Vessel_Code, dtCOC_Date, Joining_Port, Created_By, RankScaleId, ContractId, dtDOAHomePort, VesselTypeAssignment);
            }
            catch
            {
                throw;
            }
        }
        public int INS_CrewVoyages(int CrewID, int Joining_Type, string Joining_Date, string SignOn_Date, string SignOffDate, int Joining_Rank, int Vessel_Code, string COC_Date, int Joining_Port, int Created_By)
        {
            try
            {
                if (Joining_Date == null || Joining_Date == "")
                    Joining_Date = "1900/01/01";
                if (COC_Date == null || COC_Date == "")
                    COC_Date = "1900/01/01";
                if (SignOn_Date == null || SignOn_Date == "")
                    SignOn_Date = "1900/01/01";
                if (SignOffDate == null || SignOffDate == "")
                    SignOffDate = "1900/01/01";
                DateTime dtJoining_Date = DateTime.Parse(Joining_Date, iFormatProvider);
                DateTime dtCOC_Date = DateTime.Parse(COC_Date, iFormatProvider);
                DateTime dtSignOn_Date = DateTime.Parse(SignOn_Date, iFormatProvider);
                DateTime dtSignOffDate = DateTime.Parse(SignOffDate, iFormatProvider);

                return objCrewDAL.INS_CrewVoyages_DL(CrewID, Joining_Type, dtJoining_Date, dtSignOn_Date, dtSignOffDate, Joining_Rank, Vessel_Code, dtCOC_Date, Joining_Port, Created_By);
            }
            catch
            {
                throw;
            }
        }
        public int INS_CrewWages(int ID)
        {
            try
            {
                return objCrewDAL.INS_CrewWages_DL(ID);
            }
            catch
            {
                throw;
            }
        }
        public int INS_CrewDocuments(int CrewID, string DocumentName, string DocFileName, string DocFileExt, int DocTypeID, int Created_By)
        {
            try
            {
                return objCrewDAL.INS_CrewDocuments_DL(CrewID, DocumentName, DocFileName, DocFileExt, DocTypeID, Created_By);
            }
            catch
            {
                throw;
            }
        }
        public int INS_CrewDocuments(int CrewID, string DocumentName, string DocFileName, string DocFileExt, int DocTypeID, int Created_By, string DocNo, string IssueDate, string IssuePalce, string ExpiryDate)
        {
            try
            {
                DateTime Dt_IssueDate = DateTime.Parse("1900/01/01");
                if (IssueDate != "")
                    Dt_IssueDate = DateTime.Parse(IssueDate);

                DateTime Dt_ExpiryDate = DateTime.Parse("1900/01/01");
                if (ExpiryDate != "")
                    Dt_ExpiryDate = DateTime.Parse(ExpiryDate);

                return objCrewDAL.INS_CrewDocuments_DL(CrewID, DocTypeID, DocumentName, DocFileName, DocFileExt, Created_By, DocNo, Dt_IssueDate, IssuePalce, Dt_ExpiryDate);
            }
            catch
            {
                throw;
            }
        }
        public int INS_CrewDocuments(int CrewID, string DocumentName, string DocFileName, string DocFileExt, int DocTypeID, int Created_By, string DocNo, string IssueDate, string IssuePalce, string ExpiryDate, int CountryOfIssue)
        {
            try
            {
                DateTime Dt_IssueDate = DateTime.Parse("1900/01/01");
                if (IssueDate != "")
                    Dt_IssueDate = DateTime.Parse(IssueDate);

                DateTime Dt_ExpiryDate = DateTime.Parse("1900/01/01");
                if (ExpiryDate != "")
                    Dt_ExpiryDate = DateTime.Parse(ExpiryDate);

                return objCrewDAL.INS_CrewDocuments_DL(CrewID, DocTypeID, DocumentName, DocFileName, DocFileExt, Created_By, DocNo, Dt_IssueDate, IssuePalce, Dt_ExpiryDate, CountryOfIssue);
            }
            catch
            {
                throw;
            }
        }
        public int INS_Crew_DependentDetails(int CrewID, string FullName, string Relationship, string Address, string Phone, int IsNOK, int Created_By)
        {
            try
            {

                return objCrewDAL.INS_Crew_DependentDetails_DL(CrewID, FullName, Relationship, Address, Phone, IsNOK, Created_By);
            }
            catch
            {
                throw;
            }
        }
        public int INS_Crew_DependentDetails(int VarAdd, int CrewID, string FirstName, string Surname, string Relationship, string Address1, string Address2, string Address, string SSN, DateTime? DOB, String City, string State, string Country, String ZipCode, string Phone, int IsBeneficiary, int IsNOK, int Modified_By)
        {
            try
            {

                return objCrewDAL.INS_Crew_DependentDetails_DL(VarAdd, CrewID, FirstName, Surname, Relationship, Address1, Address2, Address, SSN, DOB, City, State, Country, ZipCode, Phone, IsBeneficiary, IsNOK, Modified_By);
            }
            catch
            {
                throw;
            }
        }
        public int CRW_Get_CrewVoyageCountByCrewID(int CrewID)
        {
            try
            {


                return objCrewDAL.CRW_Get_CrewVoyageCountByCrewID(CrewID);
            }
            catch
            {
                throw;
            }
        }
        public int INS_CrewInterviewPlanning(int CrewID, int Rank, string CandidateName, string InterviewPlanDate, int PlannedInterviewerID, string InterviewerPosition, int Created_By, string TimeZone, string InterviewType, int IQID)
        {
            try
            {
                DateTime dtInterviewPlanDate = DateTime.Parse(InterviewPlanDate, iFormatProvider);

                return objCrewDAL.INS_CrewInterviewPlanning_DL(CrewID, Rank, CandidateName, dtInterviewPlanDate, PlannedInterviewerID, InterviewerPosition, Created_By, TimeZone, InterviewType, IQID);
            }
            catch
            {
                throw;
            }
        }
        public int INS_CrewInterviewPlanning(int CrewID, int Rank, string CandidateName, string InterviewPlanDate, int PlannedInterviewerID, string InterviewerPosition, int Created_By, string TimeZone)
        {
            try
            {
                DateTime dtInterviewPlanDate = DateTime.Parse(InterviewPlanDate, iFormatProvider);

                return objCrewDAL.INS_CrewInterviewPlanning_DL(CrewID, Rank, CandidateName, dtInterviewPlanDate, PlannedInterviewerID, InterviewerPosition, Created_By, TimeZone);
            }
            catch
            {
                throw;
            }
        }
        public int INS_CrewInterviewPlanning(int CrewID, int Rank, string CandidateName, string InterviewPlanDate, int PlannedInterviewerID, string InterviewerPosition, int Created_By)
        {
            try
            {
                DateTime dtInterviewPlanDate = DateTime.Parse(InterviewPlanDate, iFormatProvider);

                return objCrewDAL.INS_CrewInterviewPlanning_DL(CrewID, Rank, CandidateName, dtInterviewPlanDate, PlannedInterviewerID, InterviewerPosition, Created_By);
            }
            catch
            {
                throw;
            }
        }
        public int INS_CrewInterviewAnswer(int CrewID, int InterviewID, int QuestionID, int UserAnswer, string UserAnswerText, string SubAns1, string SubAns2, string SubAns3, string SubAns4, string SubAns5, int Created_By)
        {
            try
            {
                return objCrewDAL.INS_CrewInterviewAnswer_DL(CrewID, InterviewID, QuestionID, UserAnswer, UserAnswerText, SubAns1, SubAns2, SubAns3, SubAns4, SubAns5, Created_By);
            }
            catch
            {
                throw;
            }
        }
        public int INS_CrewInterviewAnswer(int CrewID, int InterviewID, int QuestionID, int UserAnswer, string UserRemark, int Created_By)
        {
            try
            {
                return objCrewDAL.INS_CrewInterviewAnswer_DL(CrewID, InterviewID, QuestionID, UserAnswer, UserRemark, "", "", "", "", "", Created_By);
            }
            catch
            {
                throw;
            }
        }
        public int INS_Crew_RecomendedVessels(int CrewID, int InterviewID, int VesselID, int Modified_By)
        {
            try
            {
                return objCrewDAL.INS_Crew_RecomendedVessel_DL(CrewID, InterviewID, VesselID, Modified_By);
            }
            catch
            {
                throw;
            }
        }

        public int INS_Crew_RecomendedZones(int CrewID, int InterviewID, int ZoneID, int Modified_By)
        {
            try
            {
                return objCrewDAL.INS_Crew_RecomendedZone_DL(CrewID, InterviewID, ZoneID, Modified_By);
            }
            catch
            {
                throw;
            }
        }
        public int INS_CrewRemarks(int CrewID, string Remarks, string AttachmentPath, int Created_By)
        {
            return INS_CrewRemarks(CrewID, Remarks, AttachmentPath, Created_By, 0);
        }
        public int INS_CrewRemarks(int CrewID, string Remarks, string AttachmentPath, int Created_By, int VoyageID)
        {
            try
            {

                return objCrewDAL.INS_CrewRemarks_DL(CrewID, Remarks, AttachmentPath, Created_By, VoyageID);
            }
            catch
            {
                throw;
            }
        }
        public int INS_Crew_Maintenance_Feedback(int CrewID, string Remarks, string AttachmentPath, int Created_By, int VoyageID, int Job_Type, int Vessel_ID, int Worklist_ID, int Office_ID, int PMS_JobID, int PMS_JobHistoryID)
        {
            try
            {
                return objCrewDAL.INS_Crew_Maintenance_Feedback_DL(CrewID, Remarks, AttachmentPath, Created_By, VoyageID, Job_Type, Vessel_ID, Worklist_ID, Office_ID, PMS_JobID, PMS_JobHistoryID);
            }
            catch
            {
                throw;
            }
        }


        public DataTable Get_CrewMaintenanceFeedback(int CrewID, int UserID)
        {
            try
            {
                return objCrewDAL.Get_CrewMaintenanceFeedback_DL(CrewID, UserID);
            }
            catch
            {
                throw;
            }
        }



        public int INS_CrewAssignment(int CrewID_SigningOff, int CrewID_UnAssigned, int JoiningRank, int Created_By)
        {
            try
            {

                return objCrewDAL.INS_CrewAssignment_DL(CrewID_SigningOff, CrewID_UnAssigned, JoiningRank, Created_By);
            }
            catch
            {
                throw;
            }
        }
        public int ADD_CrewAssignment(int CrewID_SigningOff, int CrewID_UnAssigned, int JoiningRank, int Created_By)
        {
            try
            {

                return objCrewDAL.ADD_CrewAssignment_DL(CrewID_SigningOff, CrewID_UnAssigned, JoiningRank, Created_By);
            }
            catch
            {
                throw;
            }
        }



        #endregion

        #region  - CREW DETAILS - UPDATE -

        public int UPDATE_CrewPersonalDetails(CrewProperties objCrew)
        {
            try
            {
                return objCrewDAL.UPDATE_CrewPersonalDetails_DL(objCrew);
            }
            catch
            {
                throw;
            }
        }
        public int UPDATE_CrewPersonalDetails(CrewProperties objCrew, DataTable dtVesselTypes)
        {
            try
            {
                return objCrewDAL.UPDATE_CrewPersonalDetails_DL(objCrew, dtVesselTypes);
            }
            catch
            {
                throw;
            }
        }
        public int UPDATE_MultinationalCrewInfo(int CrewID, int MultinationalCrewExp, string MultinationalCrewNat, int Modified_By)
        {
            try
            {
                return objCrewDAL.UPDATE_MultinationalCrewInfo_DL(CrewID, MultinationalCrewExp, MultinationalCrewNat, Modified_By);
            }
            catch
            {
                throw;
            }
        }


        public int UPDATE_CrewPreJoiningExp(int ID, string Vessel_Name, string Flag, string Vessel_Type, string DWT, string GRT, string CompanyName, int Rank, string Date_From, string Date_To, int Modified_By, string ME_MakeModel, int ME_BHP, int? CompanyID)
        {
            try
            {
                DateTime Dt_Date_From = DateTime.Parse(Date_From, iFormatProvider);
                DateTime Dt_Date_To = DateTime.Parse(Date_To, iFormatProvider);

                System.TimeSpan diffResult = Dt_Date_To.Subtract(Dt_Date_From);

                int totDays = diffResult.Days + 1;
                const double daysToMonths = 30;

                double months = totDays / daysToMonths;
                int Days = 0, Months = 0;

                string s = months.ToString("0.00", CultureInfo.InvariantCulture);
                string[] parts = s.Split('.');

                Months = Convert.ToInt32(parts[0]);
                Days = Convert.ToInt32((months - Months) * daysToMonths);

                int iGRT = 0;
                if (GRT != null && GRT != "")
                    iGRT = int.Parse(GRT);

                return objCrewDAL.UPDATE_CrewPreJoiningExp_DL(ID, Vessel_Name, Flag, Vessel_Type, DWT, iGRT, CompanyName, Rank, Dt_Date_From, Dt_Date_To, Months, Days, Modified_By, ME_MakeModel, ME_BHP, CompanyID);
            }
            catch
            {
                throw;
            }
        }

        public int UPDATE_COC_Date(int VoyID, string COC_Date, string Remark, int Modified_By)
        {
            try
            {
                DateTime dtCOC_Date = DateTime.Parse(COC_Date, iFormatProvider);
                return objCrewDAL.UPDATE_COC_Date_DL(VoyID, dtCOC_Date, Remark, Modified_By);
            }
            catch
            {
                throw;
            }
        }

        public int UPDATE_CrewVoyages(int ID, int CrewID, int Joining_Type, int Vessel_ID, string Joining_Date, int Joining_Rank, int Joining_Port, string Sign_On_Date, string Sign_Off_Date, int Sign_Off_Port, int Sign_Off_Reason, int Modified_By, string MPA_Ref, string DOA_HomePort, int RankScaleId, int ContractId, string COCDate, int VesselTypeAssignment)
        {
            try
            {
                if (Joining_Date == null || Joining_Date == "")
                    Joining_Date = "1900/01/01";

                if (Sign_On_Date == null || Sign_On_Date == "")
                    Sign_On_Date = "1900/01/01";

                if (Sign_Off_Date == null || Sign_Off_Date == "")
                    Sign_Off_Date = "1900/01/01";

                if (DOA_HomePort == null || DOA_HomePort == "")
                    DOA_HomePort = "1900/01/01";

                if (COCDate == null || COCDate == "")
                    COCDate = "1900/01/01";

                DateTime dtJoining_Date = DateTime.Parse(Joining_Date);

                DateTime dtSign_On_Date = DateTime.Parse(Sign_On_Date, iFormatProvider);
                DateTime dtSign_Off_Date = DateTime.Parse(Sign_Off_Date, iFormatProvider);
                DateTime dtDOA_HomePort = DateTime.Parse(DOA_HomePort, iFormatProvider);
                DateTime dtCOCDate = DateTime.Parse(COCDate, iFormatProvider);

                int returnvalue = objCrewDAL.UPDATE_CrewVoyages_DL(ID, CrewID, Joining_Type, Vessel_ID, Joining_Rank, dtJoining_Date, Joining_Port, dtSign_On_Date, dtSign_Off_Date, Sign_Off_Port, Sign_Off_Reason, Modified_By, MPA_Ref, dtDOA_HomePort, RankScaleId, ContractId, dtCOCDate, VesselTypeAssignment);
                return returnvalue;
            }
            catch
            {
                throw;
            }
        }

        public int UPDATE_CrewWages(int ID)
        {
            try
            {
                return 1;// objCrewDAL.UPDATE_CrewWages_DL(ID);
            }
            catch
            {
                throw;
            }
        }
        public int UPDATE_CrewPassportAndSeamanDetails(int CrewID, string Passport_Number, string Passport_Issue_Date, string Passport_Expiry_Date, string Passport_PlaceOf_Issue, string Seaman_Book_Number, string Seaman_Book_Issue_Date, string Seaman_Book_Expiry_Date, string Seaman_Book_PlaceOf_Issue, int Modified_By)
        {
            try
            {
                if (Seaman_Book_Issue_Date == "")
                    Seaman_Book_Issue_Date = "1900/01/01";
                if (Seaman_Book_Expiry_Date == "")
                    Seaman_Book_Expiry_Date = "1900/01/01";

                if (Passport_Issue_Date == "")
                    Passport_Issue_Date = "1900/01/01";
                if (Passport_Expiry_Date == "")
                    Passport_Expiry_Date = "1900/01/01";

                DateTime Dt_Passport_Issue_Date = DateTime.Parse(Passport_Issue_Date, iFormatProvider);
                DateTime Dt_Seaman_Book_Issue_Date = DateTime.Parse(Seaman_Book_Issue_Date, iFormatProvider);

                DateTime Dt_Passport_Expiry_Date = DateTime.Parse(Passport_Expiry_Date, iFormatProvider);
                DateTime Dt_Seaman_Book_Expiry_Date = DateTime.Parse(Seaman_Book_Expiry_Date, iFormatProvider);

                return objCrewDAL.UPDATE_CrewPassportAndSeamanDetails_DL(CrewID, Passport_Number.ToUpper(), Dt_Passport_Issue_Date, Dt_Passport_Expiry_Date, Passport_PlaceOf_Issue.ToUpper(), Seaman_Book_Number.ToUpper(), Dt_Seaman_Book_Issue_Date, Dt_Seaman_Book_Expiry_Date, Seaman_Book_PlaceOf_Issue.ToUpper(), Modified_By);
            }
            catch
            {
                throw;
            }
        }

        public int UPDATE_CrewDocument(int CrewID, int DocID, int DocTypeID, string DocumentName, string DocNo, string IssueDate, string IssuePalce, string ExpiryDate, int Modified_By, int IssueCountry)
        {
            try
            {
                DateTime Dt_IssueDate = DateTime.Parse("1900/01/01");
                if (IssueDate != "")
                    Dt_IssueDate = DateTime.Parse(IssueDate);

                DateTime Dt_ExpiryDate = DateTime.Parse("1900/01/01");
                if (ExpiryDate != "")
                    Dt_ExpiryDate = DateTime.Parse(ExpiryDate);

                return objCrewDAL.UPDATE_CrewDocument_DL(CrewID, DocID, DocTypeID, DocumentName, DocNo, Dt_IssueDate, IssuePalce, Dt_ExpiryDate, Modified_By, IssueCountry);
            }
            catch
            {
                throw;
            }
        }
        public int UPDATE_CrewDocument(int DocID, string DocName, string DocNo, int SizeByte, int DocTypeID, int Modified_By)
        {
            try
            {

                return objCrewDAL.UPDATE_CrewDocument_DL(DocID, DocName, DocNo, SizeByte, DocTypeID, Modified_By);
            }
            catch
            {
                throw;
            }
        }
        public int UPDATE_CrewDocument(int DocID, int DocTypeID, int Modified_By)
        {
            try
            {
                return objCrewDAL.UPDATE_CrewDocument_DL(DocID, DocTypeID, Modified_By);
            }
            catch
            {
                throw;
            }
        }

        public int UPDATE_DocumentAttributeValues(int DocID, int CrewID, int AttributeID, string AttributeValue_String, string AttributeDataType, int Modified_By)
        {
            try
            {
                if (AttributeDataType == "DATETIME")
                {
                    DateTime dt = DateTime.Parse(AttributeValue_String, iFormatProvider);
                    AttributeValue_String = dt.ToString("MM/dd/yyyy");
                }

                return objCrewDAL.INS_DocumentAttributeValues_DL(DocID, CrewID, AttributeID, AttributeValue_String, AttributeDataType, Modified_By);
            }
            catch
            {
                throw;
            }
        }
        public int UPDATE_DocumentAttributeValues(int ID, string AttributeValue_String, int Modified_By)
        {
            try
            {
                return objCrewDAL.UPDATE_DocumentAttributeValues_DL(ID, AttributeValue_String, Modified_By);
            }
            catch
            {
                throw;
            }
        }
        public int UPDATE_DocTypeAndCreateAttributeValues(int DocID, int CrewID, string DocName, int iDocTypeID, int Modified_By)
        {
            try
            {

                string sDocTypeID = objDALDMS.Get_DocTypeID_DL(DocID);

                if (int.Parse(sDocTypeID) != iDocTypeID)
                {
                    objDALDMS.Del_All_AttributeValuesForNewDoc_DL(DocID);
                    objDALDMS.Create_DocAttributesForNewDoc_DL(DocID, CrewID);
                }
                return 1;
            }
            catch
            {
                throw;
            }
        }
        public int UPDATE_Crew_DependentDetails(int AddVar, int ID, string FirstName, string Surname, string Relationship, string Address1, string Address2, string Address, string SSN, DateTime? DOB, String City, string State, string Country, String ZipCode, string Phone, int IsBeneficiary, int IsNOK, int Modified_By)
        {
            try
            {
                return objCrewDAL.UPDATE_Crew_DependentDetails_DL(AddVar, ID, FirstName, Surname, Relationship, Address1, Address2, Address, SSN, DOB, City, State, Country, ZipCode, Phone, IsBeneficiary, IsNOK, Modified_By);
            }
            catch
            {
                throw;
            }

        }

        public int UPDATE_Crew_DependentDetails(int ID, string FullName, string Relationship, string Address, string Phone, int IsNOK, int Modified_By)
        {
            try
            {
                return objCrewDAL.UPDATE_Crew_DependentDetails_DL(ID, FullName, Relationship, Address, Phone, IsNOK, Modified_By);
            }
            catch
            {
                throw;
            }

        }

        public int UPDATE_Crew_DependentDetails(int ID, string FullName, string Relationship, int Modified_By)
        {
            try
            {


                return objCrewDAL.UPDATE_Crew_DependentDetails_DL(ID, FullName, Relationship, "", "", 0, Modified_By);
            }
            catch
            {
                throw;
            }

        }
        public int UPDATE_Crew_DependentDetails(string FullName, string Relationship, string DOB, int Modified_By, int ID)
        {
            try
            {


                return objCrewDAL.UPDATE_Crew_DependentDetails_DL(ID, FullName, Relationship, "", "", 0, Modified_By);
            }
            catch
            {
                throw;
            }

        }
        public int UPDATE_CrewPhotoURL(int CrewID, string URL)
        {
            try
            {
                return objCrewDAL.UPDATE_CrewPhotoURL_DL(CrewID, URL);
            }
            catch
            {
                throw;
            }
        }

        public int UPDATE_CrewInterviewPlanning(int CrewID, int InterviewID, string InterviewPlanDate, int PlannedInterviewerID, int Rank, string CandidateName, int Modified_By)
        {
            try
            {
                DateTime dtInterviewPlanDate = DateTime.Parse(InterviewPlanDate, iFormatProvider);

                return objCrewDAL.UPDATE_CrewInterviewPlanning_DL(InterviewID, dtInterviewPlanDate, PlannedInterviewerID, CrewID, Rank, CandidateName, Modified_By);
            }
            catch
            {
                throw;
            }
        }
        public int UPDATE_CrewInterviewPlanning(int ID, string InterviewPlanDate, string InterviewPlanH, string InterviewPlanM, int Modified_By)
        {
            try
            {
                string PlanDateTime = InterviewPlanDate + " " + InterviewPlanH + ":" + InterviewPlanM;
                if (int.Parse(InterviewPlanH) < 12)
                    PlanDateTime += " AM";
                else
                    PlanDateTime += " PM";


                DateTime dtInterviewPlanDate = DateTime.Parse(PlanDateTime, iFormatProvider);

                return objCrewDAL.UPDATE_CrewInterviewPlanningDate_DL(ID, dtInterviewPlanDate, Modified_By);
            }
            catch
            {
                throw;
            }
        }


        public int UPDATE_CrewInterviewPlanning(string InterviewPlanDate, string InterviewPlanH, string InterviewPlanM, int Modified_By, string TimeZone, int TZID, int ID)
        {
            try
            {
                string PlanDateTime = InterviewPlanDate + " " + InterviewPlanH + ":" + InterviewPlanM;
                if (int.Parse(InterviewPlanH) < 12)
                    PlanDateTime += " AM";
                else
                    PlanDateTime += " PM";


                DateTime dtInterviewPlanDate = DateTime.Parse(PlanDateTime, iFormatProvider);

                return objCrewDAL.UPDATE_CrewInterviewPlanningDate_DL(ID, dtInterviewPlanDate, Modified_By, TZID);
            }
            catch
            {
                throw;
            }
        }

        public int UPDATE_CrewInterviewResult(int CrewID, int InterviewID, int InterviewerID, string InterviewDate, int RankID, string InterviewerPosition, string ResultText, string Selected, string OtherText, int Modified_By)
        {
            try
            {
                DateTime dtInterviewDate = DateTime.Parse(InterviewDate, iFormatProvider);
                int iSelected = 0;
                if (Selected != "")
                    iSelected = int.Parse(Selected);

                return objCrewDAL.UPDATE_CrewInterviewResult_DL(CrewID, InterviewID, InterviewerID, InterviewerPosition, dtInterviewDate, RankID, ResultText, iSelected, OtherText, Modified_By);
            }
            catch
            {
                throw;
            }
        }
        public int UPDATE_CrewInterviewAnswer(int CrewID, int InterviewID, int QuestionID, int UserAnswer, string UserAnswerText, int Created_By)
        {
            try
            {
                return objCrewDAL.UPDATE_CrewInterviewAnswer_DL(CrewID, InterviewID, QuestionID, UserAnswer, UserAnswerText, Created_By);
            }
            catch
            {
                throw;
            }
        }
        public int UPDATE_CrewInterviewAnswer(int CrewID, int InterviewID, int QuestionID, int UserAnswer, string UserAnswerText, string SubAns1, string SubAns2, string SubAns3, string SubAns4, string SubAns5, int Modified_By)
        {
            try
            {
                return objCrewDAL.UPDATE_CrewInterviewAnswer_DL(CrewID, InterviewID, QuestionID, UserAnswer, UserAnswerText, SubAns1, SubAns2, SubAns3, SubAns4, SubAns5, Modified_By);
            }
            catch
            {
                throw;
            }
        }
        public int UPDATE_CrewApprovalByHeadOffice(int CrewID, int ApproverID, string ApprovalDate, int ApprovalYesNo, string ApprovalOtherText, string ApprovalRemark, int QuickApproval, int Approved_Rank)
        {
            try
            {
                DateTime dtApprovalDate = DateTime.Parse(ApprovalDate, iFormatProvider);

                return objCrewDAL.UPDATE_CrewApprovalByHeadOffice_DL(CrewID, ApproverID, dtApprovalDate, ApprovalYesNo, ApprovalOtherText, ApprovalRemark, QuickApproval, Approved_Rank);
            }
            catch
            {
                throw;
            }
        }

        public int UPDATE_CrewApprovalByHeadOffice(int CrewID, int ApproverID, string ApprovalDate, int ApprovalYesNo, string ApprovalOtherText, string ApprovalRemark, int QuickApproval, int Approved_Rank, DataTable dtVesselTypes)
        {
            try
            {
                DateTime dtApprovalDate = DateTime.Parse(ApprovalDate, iFormatProvider);

                return objCrewDAL.UPDATE_CrewApprovalByHeadOffice_DL(CrewID, ApproverID, dtApprovalDate, ApprovalYesNo, ApprovalOtherText, ApprovalRemark, QuickApproval, Approved_Rank, dtVesselTypes);
            }
            catch
            {
                throw;
            }
        }
        public int CREATE_CrewChangeEvent(int VesselID, string EventDate, int PortID, int Modified_By, int Port_Call_ID)
        {
            try
            {
                DateTime Dt_EventDate = DateTime.Parse(EventDate);

                DataTable dt = objCrewDAL.Get_PortCall_Details_DL(Port_Call_ID, VesselID);
                if (dt.Rows.Count > 0)
                {
                    DateTime dtArrival = DateTime.Parse(dt.Rows[0]["Arrival"].ToString());
                    DateTime dtDeparture = DateTime.Parse(dt.Rows[0]["Departure"].ToString());

                    dtArrival = DateTime.Parse(dtArrival.ToShortDateString());

                    if (Dt_EventDate >= dtArrival && Dt_EventDate <= dtDeparture)
                        return objCrewDAL.CREATE_CrewChangeEvent_DL(VesselID, Dt_EventDate, PortID, Modified_By, Port_Call_ID);
                    else
                        return -1;//Event date is not inside the port call dates
                }
                else
                {
                    return objCrewDAL.CREATE_CrewChangeEvent_DL(VesselID, Dt_EventDate, PortID, Modified_By, Port_Call_ID);
                }
            }
            catch
            {
                throw;
            }
        }

        public int CREATE_CrewChangeEvent(int VesselID, DateTime Dt_EventDate, int PortID, int Modified_By, int Port_Call_ID)
        {
            try
            {
                DataTable dt = objCrewDAL.Get_PortCall_Details_DL(Port_Call_ID, VesselID);
                if (dt.Rows.Count > 0)
                {
                    DateTime dtArrival = DateTime.Parse(dt.Rows[0]["Arrival"].ToString());
                    DateTime dtDeparture = DateTime.Parse(dt.Rows[0]["Departure"].ToString());

                    dtArrival = DateTime.Parse(dtArrival.ToShortDateString());

                    if (Dt_EventDate >= dtArrival && Dt_EventDate <= dtDeparture)
                        return objCrewDAL.CREATE_CrewChangeEvent_DL(VesselID, Dt_EventDate, PortID, Modified_By, Port_Call_ID);
                    else
                        return -1;//Event date is not inside the port call dates
                }
                else
                {
                    return objCrewDAL.CREATE_CrewChangeEvent_DL(VesselID, Dt_EventDate, PortID, Modified_By, Port_Call_ID);
                }
            }
            catch
            {
                throw;
            }
        }

        public int UPDATE_CrewChangeEvent(int EventID, string EventDate, int PortID, string EventRemark, int Modified_By, int Port_Call_ID)
        {
            try
            {
                DateTime Dt_EventDate = DateTime.Parse(EventDate);
                return objCrewDAL.UPDATE_CrewChangeEvent_DL(EventID, Dt_EventDate, PortID, EventRemark, Modified_By, Port_Call_ID);
            }
            catch
            {
                throw;
            }
        }

        public int UPDATE_CrewChangeEvent(int EventID, DateTime Dt_EventDate, int PortID, string EventRemark, int Modified_By, int Port_Call_ID)
        {
            try
            {
                return objCrewDAL.UPDATE_CrewChangeEvent_DL(EventID, Dt_EventDate, PortID, EventRemark, Modified_By, Port_Call_ID);
            }
            catch
            {
                throw;
            }
        }

        public int AddCrewTo_CrewChangeEvent(int EventID, int CrewID, int ON_OFF, int AssignmentID, int Created_By)
        {
            try
            {
                return objCrewDAL.AddCrewTo_CrewChangeEvent_DL(EventID, CrewID, ON_OFF, AssignmentID, Created_By);
            }
            catch
            {
                throw;
            }
        }
        public int AddCrewTo_CrewChangeEvent(int EventID, int CrewID, int ON_OFF, int AssignmentID, int Created_By, int VoyID)
        {
            try
            {
                return objCrewDAL.AddCrewTo_CrewChangeEvent_DL(EventID, CrewID, ON_OFF, AssignmentID, Created_By, VoyID);
            }
            catch
            {
                throw;
            }
        }
        public int AddCrewTo_CrewChangeEvent(int EventID, int CrewID, int ON_OFF, int AssignmentID, int Created_By, string Joining_Date, string COC_Date, int Joining_Rank)
        {
            try
            {
                DateTime dtJoining_Date = DateTime.Parse("1900/01/01");
                DateTime dtCOC_Date = DateTime.Parse("1900/01/01");

                if (Joining_Date != "")
                    dtJoining_Date = DateTime.Parse(Joining_Date);

                if (COC_Date != "")
                    dtCOC_Date = DateTime.Parse(COC_Date);

                return objCrewDAL.AddCrewTo_CrewChangeEvent_DL(EventID, CrewID, ON_OFF, AssignmentID, Created_By, dtJoining_Date, dtCOC_Date, Joining_Rank);
            }
            catch
            {
                throw;
            }
        }
        public int AddCrewTo_CrewChangeEvent(int EventID, int CrewID, int ON_OFF, int AssignmentID, int Created_By, string Joining_Date, string COC_Date, int Joining_Rank, int VoyageID, int RankScaleId)
        {
            try
            {
                DateTime dtJoining_Date = DateTime.Parse("1900/01/01");
                DateTime dtCOC_Date = DateTime.Parse("1900/01/01");

                if (Joining_Date != "")
                    dtJoining_Date = DateTime.Parse(Joining_Date);

                if (COC_Date != "")
                    dtCOC_Date = DateTime.Parse(COC_Date);

                return objCrewDAL.AddCrewTo_CrewChangeEvent_DL(EventID, CrewID, ON_OFF, AssignmentID, Created_By, dtJoining_Date, dtCOC_Date, Joining_Rank, VoyageID, RankScaleId);
            }
            catch
            {
                throw;
            }
        }

        public int UPDATE_CrewJoiningChecklist(int CrewID, int QuestionID, int Answer, string Remark, int Modified_By)
        {
            try
            {
                return objCrewDAL.UPDATE_CrewJoiningChecklist_DL(CrewID, QuestionID, Answer, Remark, Modified_By);
            }
            catch
            {
                throw;
            }
        }
        public int UPDATE_DocumentChecklist(int CrewID, int DocID, int DocTypeID, string DocName, int AnswerYN, int RankID, string Remark, string DocNo, string IssueDate, string IssuePlace, string ExpiryDate, int Modified_By, string FilePath, int VoyageID)
        {
            try
            {
                if (IssueDate != null && ExpiryDate != null)
                {
                    DateTime Dt_IssueDate = DateTime.Parse("01/01/1900");
                    if (IssueDate.Length > 0)
                        Dt_IssueDate = DateTime.Parse(IssueDate, iFormatProvider);

                    DateTime Dt_ExpiryDate = DateTime.Parse("01/01/1900");
                    if (ExpiryDate.Length > 0)
                        Dt_ExpiryDate = DateTime.Parse(ExpiryDate, iFormatProvider);

                    objCrewDAL.UPDATE_DocumentChecklist_DL(CrewID, DocID, DocTypeID, DocName, AnswerYN, RankID, Remark, DocNo, Dt_IssueDate, IssuePlace, Dt_ExpiryDate, Modified_By, VoyageID);



                    return 1;
                }
                else
                    return 0;
            }
            catch
            {
                throw;
            }


        }
        public int UPDATE_DocumentChecklist(int CrewID, int DocID, int DocTypeID, string DocName, int AnswerYN, int RankID, string Remark, string DocNo, string IssueDate, string IssuePlace, string ExpiryDate, int Modified_By, string FilePath, int VoyageID, int SaveAndReplace)
        {
            try
            {
                if (IssueDate != null && ExpiryDate != null)
                {
                    DateTime Dt_IssueDate = DateTime.Parse("01/01/1900");
                    if (IssueDate.Length > 0)
                        Dt_IssueDate = DateTime.Parse(IssueDate, iFormatProvider);

                    DateTime Dt_ExpiryDate = DateTime.Parse("01/01/1900");
                    if (ExpiryDate.Length > 0)
                        Dt_ExpiryDate = DateTime.Parse(ExpiryDate, iFormatProvider);

                    objCrewDAL.UPDATE_DocumentChecklist_DL(CrewID, DocID, DocTypeID, DocName, AnswerYN, RankID, Remark, DocNo, Dt_IssueDate, IssuePlace, Dt_ExpiryDate, Modified_By, VoyageID, SaveAndReplace);



                    return 1;
                }
                else
                    return 0;
            }
            catch
            {
                throw;
            }


        }

        public int UPDATE_CrewStatus(int CrewID, string Status_Code, string Remark, int Modified_By)
        {
            try
            {
                return objCrewDAL.UPDATE_CrewStatus_DL(CrewID, Status_Code, Remark, Modified_By);
            }
            catch
            {
                throw;
            }
        }

        public int UPDATE_CrewUniformSize(int CrewID, string Shoe, string TShirt, string CargoPant, string Overall, int Modified_By)
        {
            try
            {
                return objCrewDAL.UPDATE_CrewUniformSize_DL(CrewID, Shoe, TShirt, CargoPant, Overall, Modified_By);
            }
            catch
            {
                throw;
            }
        }

        public int UPDATE_HeightWaistWeight(int CrewID, string Height, string Waist, string Weight, int Modified_By)
        {
            try
            {
                return objCrewDAL.UPDATE_HeightWaistWeight_DL(CrewID, Height, Waist, Weight, Modified_By);
            }
            catch
            {
                throw;
            }
        }


        #endregion

        #region  - CREW DETAILS - DELETE -

        public int DEL_CrewPersonalDetails(int ID)
        {
            try
            {
                return 1;// objCrewDAL.DEL_CrewPersonalDetails_DL(ID);
            }
            catch
            {
                throw;
            }
        }
        public int DEL_CrewPreJoiningExp(int ID, int Deleted_By)
        {
            try
            {
                return objCrewDAL.DEL_CrewPreJoiningExp_DL(ID, Deleted_By);
            }
            catch
            {
                throw;
            }
        }


        public int DEL_CrewVoyages(int ID, int Deleted_By)
        {
            try
            {
                return objCrewDAL.DEL_CrewVoyages_DL(ID, Deleted_By);
            }
            catch
            {
                throw;
            }
        }
        public int DEL_CrewWages(int ID)
        {
            try
            {
                return 1;// objCrewDAL.DEL_CrewWages_DL(ID);
            }
            catch
            {
                throw;
            }
        }
        public int DEL_Crew_DocumentByDocID(int DocID, int SessionUserID)
        {
            try
            {
                return objCrewDAL.DEL_Crew_DocumentByDocID_DL(DocID, SessionUserID);
            }
            catch
            {
                throw;
            }
        }
        public int DEL_Crew_DependentDetails(int ID, int Deleted_By)
        {
            try
            {
                return objCrewDAL.DEL_Crew_DependentDetails_DL(ID, Deleted_By);
            }
            catch
            {
                throw;
            }
        }
        public int DEL_CrewInterviewPlanning(int ID, int Deleted_By)
        {
            try
            {
                return objCrewDAL.DEL_CrewInterviewPlanning_DL(ID, Deleted_By);
            }
            catch
            {
                throw;
            }
        }
        public int DEL_CrewInterviewAnswers(int CrewID, int InterviewID, int Deleted_By)
        {
            try
            {
                return objCrewDAL.DEL_CrewInterviewAnswers_DL(CrewID, InterviewID, Deleted_By);
            }
            catch
            {
                throw;
            }
        }
        //public int Undo_Manning_Approval(int CrewID, int Deleted_By)
        //{
        //    try
        //    {
        //        return objCrewDAL.Undo_Manning_Approval_DL(CrewID, Deleted_By);
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}
        public int Delete_CrewAssignment(int ID, int Deleted_By)
        {
            try
            {

                return objCrewDAL.Delete_CrewAssignment_DL(ID, Deleted_By);
            }
            catch
            {
                throw;
            }
        }
        public int Delete_CrewChangeEvent_Planned(int ID, int Deleted_By)
        {
            try
            {

                return objCrewDAL.Delete_CrewChangeEvent_Planned_DL(ID, Deleted_By);
            }
            catch
            {
                throw;
            }
        }
        public int RemoveCrewFrom_CrewChangeEvent(int EventID, int CrewID, int Deleted_By)
        {
            try
            {
                return objCrewDAL.RemoveCrewFrom_CrewChangeEvent_DL(EventID, CrewID, Deleted_By);
            }
            catch
            {
                throw;
            }
        }
        public int Delete_Crew(int CrewID, int Deleted_By)
        {
            try
            {
                return objCrewDAL.Delete_Crew_DL(CrewID, Deleted_By);
            }
            catch
            {
                throw;
            }
        }

        #endregion

        public int Send_CrewNotification(int CrewID, int ManningOfficeID, int EventID, int EventType, string MailTo, string CC, string BCC, string Subject, string EmailBody, string AttachmentPath, string ItemType, string MeetingTime, int Created_By, string ReadyStatus)
        {
            return Send_CrewNotification(CrewID, ManningOfficeID, EventID, EventType, MailTo, CC, BCC, Subject, EmailBody, AttachmentPath, ItemType, MeetingTime, Created_By, ReadyStatus, null);
        }

        public int Send_CrewNotification_attachment(int MailID, string Attachment_Name, string Attachment_Path, int CreatedBy)
        {
            return objCrewDAL.Send_CrewNotification_attachment(MailID, Attachment_Name, Attachment_Path, CreatedBy);
        }
        public int Send_CrewNotification(int CrewID, int ManningOfficeID, int EventID, int EventType, string MailTo, string CC, string BCC, string Subject, string EmailBody, string AttachmentPath, string ItemType, string MeetingTime, int Created_By, string ReadyStatus, string TimeZone)
        {
            try
            {
                DateTime Dt_MeetingTime = DateTime.Parse("1900/01/01");
                if (MeetingTime != "")
                    Dt_MeetingTime = DateTime.Parse(MeetingTime);

                return objCrewDAL.Send_CrewNotification_DL(CrewID, ManningOfficeID, EventID, EventType, MailTo, CC, BCC, Subject, EmailBody, AttachmentPath, ItemType, Dt_MeetingTime, Created_By, ReadyStatus, TimeZone);
            }
            catch
            {
                throw;
            }
        }

        public int UPDATE_CrewNotification(int MsgID, string MailTo, string CC, string BCC, string Subject, string EmailBody, string AttachmentPath, string MeetingTime, int Modified_By, string ReadyStatus)
        {
            try
            {
                DateTime Dt_MeetingTime = DateTime.Parse("1900/01/01");
                if (MeetingTime != "")
                    Dt_MeetingTime = DateTime.Parse(MeetingTime);

                return objCrewDAL.UPDATE_CrewNotification_DL(MsgID, MailTo, CC, BCC, Subject, EmailBody, AttachmentPath, Dt_MeetingTime, Modified_By, ReadyStatus);
            }
            catch
            {
                throw;
            }
        }

        public int Discard_MailMessage(int MsgID, int Deleted_By)
        {
            try
            {
                return objCrewDAL.Discard_MailMessage_DL(MsgID, Deleted_By);
            }
            catch
            {
                throw;
            }
        }

        public DataTable UPDATE_VoyageDocumentChecklist()
        {
            try
            {
                return objCrewDAL.UPDATE_VoyageDocumentChecklist();
            }
            catch
            {
                throw;
            }
        }

        public int Save_Contract_Template(int ContractId, string TemplateName, string TemplateText, int Created_By)
        {
            try
            {

                return objCrewDAL.Save_Contract_Template_DL(ContractId, TemplateName, TemplateText, Created_By);
            }
            catch
            {
                throw;
            }
        }

        public int INS_CrewYellow_RedCard(int CrewID, int CardType, string Remarks, int Created_By)
        {
            try
            {

                return objCrewDAL.INS_CrewYellow_RedCard_DL(CrewID, CardType, Remarks, Created_By);
            }
            catch
            {
                throw;
            }
        }

        public int Approve_CrewYellow_RedCard(int Approval_Status, int CardID, string ApproverRemarks, int Created_By)
        {
            try
            {

                return objCrewDAL.Approve_CrewYellow_RedCard_DL(Approval_Status, CardID, ApproverRemarks, Created_By);
            }
            catch
            {
                throw;
            }
        }

        public int INS_CrewCard_Attachment(int CardID, int AttachmentType, string AttachmentName, string AttachmentPath, int Created_By)
        {
            try
            {

                return objCrewDAL.INS_CrewCard_Attachment_DL(CardID, AttachmentType, AttachmentName, AttachmentPath, Created_By);
            }
            catch
            {
                throw;
            }
        }

        public int UPDATE_CrewCard_AttachmentStatus(int CardID, string AttachmentType, int Status, int Updated_By)
        {
            try
            {

                return objCrewDAL.UPDATE_CrewCard_AttachmentStatus_DL(CardID, AttachmentType, Status, Updated_By);
            }
            catch
            {
                throw;
            }
        }

        public int INS_CrewCard_Remarks(int CardID, string Remarks, string RemarksType, int Created_By)
        {
            try
            {

                return objCrewDAL.INS_CrewCard_Remarks_DL(CardID, Remarks, RemarksType, Created_By);
            }
            catch
            {
                throw;
            }
        }

        public SqlDataReader Get_CrewCard_Remarks(int CardID, int UserID)
        {
            try
            {

                return objCrewDAL.Get_CrewCard_Remarks_DL(CardID, UserID);
            }
            catch
            {
                throw;
            }
        }

        public int Approve_JoiningRank(int VoyageID, int RankID, int Approved_By, string ApproverRemarks)
        {
            try
            {
                return objCrewDAL.Approve_JoiningRank_DL(VoyageID, RankID, Approved_By, ApproverRemarks);
            }
            catch
            {
                throw;
            }
        }

        public int TransferCrew(int CurrentVoyageID, int CrewID, int Vessel_ID, int Joining_Type, int Joining_Rank, string Joining_Date, string Sign_On_Date, int Joining_Port, string COC_Date, int Created_By, int ContractId)
        {
            try
            {
                DateTime dtJoining_Date = DateTime.Parse(Joining_Date, iFormatProvider);
                DateTime dtCOC_Date = DateTime.Parse(COC_Date, iFormatProvider);

                DateTime dtSign_On_Date = DateTime.Parse("1900/01/01", iFormatProvider);
                if (Sign_On_Date.Length > 0)
                    dtSign_On_Date = DateTime.Parse(Sign_On_Date);

                return objCrewDAL.TransferCrew_DL(CurrentVoyageID, CrewID, Vessel_ID, Joining_Type, Joining_Rank, dtJoining_Date, dtSign_On_Date, Joining_Port, dtCOC_Date, Created_By, ContractId);
            }
            catch
            {
                throw;
            }
        }

        public int TransferCrew(int CurrentVoyageID, int CrewID, int Vessel_ID, int Joining_Type, int Joining_Rank, string Joining_Date, string Sign_On_Date, int Joining_Port, string COC_Date, int Created_By, string Sign_Off_Date, int SignOff_Port, int NewWageContractId, int ContractId)
        {
            try
            {
                DateTime dtJoining_Date = DateTime.Parse(Joining_Date, iFormatProvider);
                DateTime dtCOC_Date = DateTime.Parse(COC_Date, iFormatProvider);

                DateTime dtSign_On_Date = DateTime.Parse("1900/01/01", iFormatProvider);
                if (Sign_On_Date.Length > 0)
                    dtSign_On_Date = DateTime.Parse(Sign_On_Date);

                DateTime dtSign_Off_Date = DateTime.Parse("1900/01/01", iFormatProvider);
                if (Sign_Off_Date.Length > 0)
                    dtSign_Off_Date = DateTime.Parse(Sign_Off_Date);

                return objCrewDAL.TransferCrew_DL(CurrentVoyageID, CrewID, Vessel_ID, Joining_Type, Joining_Rank, dtJoining_Date, dtSign_On_Date, Joining_Port, dtCOC_Date, Created_By, dtSign_Off_Date, SignOff_Port, NewWageContractId, ContractId);
            }
            catch
            {
                throw;
            }
        }
        public int TransferCrew(int CurrentVoyageID, int CrewID, int Vessel_ID, int Joining_Type, int Joining_Rank, string Joining_Date, string Sign_On_Date, int Joining_Port, string COC_Date, int Created_By, string Sign_Off_Date, int SignOff_Port, int ContractId)
        {
            try
            {
                DateTime dtJoining_Date = DateTime.Parse(Joining_Date, iFormatProvider);
                DateTime dtCOC_Date = DateTime.Parse(COC_Date, iFormatProvider);

                DateTime dtSign_On_Date = DateTime.Parse("1900/01/01", iFormatProvider);
                if (Sign_On_Date.Length > 0)
                    dtSign_On_Date = DateTime.Parse(Sign_On_Date);

                DateTime dtSign_Off_Date = DateTime.Parse("1900/01/01", iFormatProvider);
                if (Sign_Off_Date.Length > 0)
                    dtSign_Off_Date = DateTime.Parse(Sign_Off_Date);

                return objCrewDAL.TransferCrew_DL(CurrentVoyageID, CrewID, Vessel_ID, Joining_Type, Joining_Rank, dtJoining_Date, dtSign_On_Date, Joining_Port, dtCOC_Date, Created_By, dtSign_Off_Date, SignOff_Port, ContractId);
            }
            catch
            {
                throw;
            }
        }
        public DataTable Get_Transfer_Promotions(int CrewID, int VoyageID, int UserID)
        {
            try
            {
                return objCrewDAL.Get_Transfer_Promotions_DL(CrewID, VoyageID, UserID);
            }
            catch
            {
                throw;
            }
        }

        public int Delete_Transfer_Planning(int Transfer_ID, int UserID)
        {
            try
            {
                return objCrewDAL.Delete_Transfer_Planning_DL(Transfer_ID, UserID);
            }
            catch
            {
                throw;
            }
        }

        public int UPDATE_Crew_DirectContract(int CrewID, int DirectContract, int Modified_By)
        {
            try
            {
                return objCrewDAL.UPDATE_Crew_DirectContract_DL(CrewID, DirectContract, Modified_By);
            }
            catch
            {
                throw;
            }
        }

        public int Insert_CrewAgreementRecord(int CrewID, int VoyageID, int Stage, int Contract_Template_ID, string DocumentName, string DocFileName, string DocFilePath, int UserID)
        {
            try
            {
                return objCrewDAL.Insert_CrewAgreementRecord_DL(CrewID, VoyageID, Stage, Contract_Template_ID, DocumentName, DocFileName, DocFilePath, UserID);
            }
            catch
            {
                throw;
            }
        }
        public int Insert_CrewAgreementRecord(int CrewID, int VoyageID, int Stage, int Contract_Template_ID, string DocumentName, string DocFileName, string DocFilePath, int UserID, int pagecount)
        {
            try
            {
                return objCrewDAL.Insert_CrewAgreementRecord_DL(CrewID, VoyageID, Stage, Contract_Template_ID, DocumentName, DocFileName, DocFilePath, UserID, pagecount);
            }
            catch
            {
                throw;
            }
        }
        public int Verify_CrewAgreement(int CrewID, int VoyageID, int UserID)
        {
            try
            {
                return objCrewDAL.Verify_CrewAgreement_DL(CrewID, VoyageID, UserID);
            }
            catch
            {
                throw;
            }
        }

        public int Undo_Verify_CrewAgreement(int CrewID, int VoyageID, int UserID)
        {
            try
            {
                return objCrewDAL.Undo_Verify_CrewAgreement_DL(CrewID, VoyageID, UserID);
            }
            catch
            {
                throw;
            }
        }

        public int Undo_DigiSign_CrewAgreement(int CrewID, int VoyageID, int UserID)
        {
            try
            {
                return objCrewDAL.Undo_DigiSign_CrewAgreement_DL(CrewID, VoyageID, UserID);
            }
            catch
            {
                throw;
            }
        }

        public int Undo_StaffsSign_CrewAgreement(int CrewID, int VoyageID, int UserID)
        {
            try
            {
                return objCrewDAL.Undo_StaffsSign_CrewAgreement_DL(CrewID, VoyageID, UserID);
            }
            catch
            {
                throw;
            }
        }

        public int Save_Sideletter_Template(int TemplateID, string TemplateName, string TemplateText, int Created_By)
        {
            try
            {
                return objCrewDAL.Save_Sideletter_Template_DL(TemplateID, TemplateName, TemplateText, Created_By);
            }
            catch
            {
                throw;
            }
        }

        public DataTable Get_Seniority_Log(int CrewID, int VoyageID, int UserID)
        {
            try
            {
                return objCrewDAL.Get_Seniority_Log_DL(CrewID, VoyageID, UserID);
            }
            catch
            {
                throw;
            }
        }

        //public int NationalityCheck_SendForApproval(int Vessel_ID, int EventID, int CrewID, int CurrentRank_ID, int JoiningRank_ID, string Sender_Remarks, int Created_By)
        //{
        //    try
        //    {
        //        return objCrewDAL.NationalityCheck_SendForApproval_DL(Vessel_ID, EventID, CrewID, CurrentRank_ID, JoiningRank_ID,Sender_Remarks, Created_By);
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}

        public int NationalityCheck_SendForApproval(int Vessel_ID, int CrewID, int CurrentRank, int JoiningRank, string Sender_Remarks, int Created_By, int EventID = 0, int CrewID_SigningOff = 0)
        {
            try
            {
                return objCrewDAL.NationalityCheck_SendForApproval_DL(Vessel_ID, CrewID, CurrentRank, JoiningRank, Sender_Remarks, Created_By, EventID, CrewID_SigningOff);
            }
            catch
            {
                throw;
            }
        }

        public int NationalityCheck_NewJoiner(int Vessel_ID, int CrewID, int JoiningRank, int UserID, int EventID = 0, int CrewID_SigningOff = 0)
        {
            try
            {
                return objCrewDAL.NationalityCheck_NewJoiner_DL(Vessel_ID, CrewID, JoiningRank, UserID, EventID, CrewID_SigningOff);
            }
            catch
            {
                throw;
            }
        }

        public DataTable Get_NationalityCheck_Approvals(int UserID, int RequestID)
        {
            try
            {
                return objCrewDAL.Get_NationalityCheck_Approvals_DL(UserID, RequestID);
            }
            catch
            {
                throw;
            }
        }

        public int NationalityCheck_Approval(int RequestID, string ApproverRemarks, int Approval, int UserID)
        {
            try
            {
                return objCrewDAL.NationalityCheck_Approval_DL(RequestID, ApproverRemarks, Approval, UserID);
            }
            catch
            {
                throw;
            }
        }


        public DataTable Get_Crew_FeedBack_Viewer_Search(DataTable FleetID, DataTable VesselID, DataTable Rank, DataTable Nationality, DataTable ManningOffice, DataTable CommentedBy, DateTime? dtFrom, DateTime? dtTo
           , string SearchText, int? Status, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {

            try
            {
                return objCrewDAL.Get_Crew_FeedBack_Viewer_Search(FleetID, VesselID, Rank, Nationality, ManningOffice, CommentedBy, dtFrom, dtTo, SearchText, Status, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);
            }
            catch
            {
                throw;
            }

        }




        public DataTable Get_Crew_Medical_History_Viewer_Search(DataTable FleetID, DataTable VesselID, DataTable Rank, DataTable Nationality, DataTable ManningOffice
        , DateTime? dtFrom, DateTime? dtTo
        , string SearchText, int? Status, int? CrewStatus, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {

            return objCrewDAL.Get_Crew_Medical_History_Viewer_Search(FleetID, VesselID, Rank, Nationality, ManningOffice, dtFrom, dtTo, SearchText, Status, CrewStatus, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);

        }

        public DataTable Get_SystemParameter(string Parent)
        {
            try
            {
                return objCrewDAL.Get_SystemParameter_DL(Parent);
            }
            catch
            {
                throw;
            }
        }

        public DataTable Get_Crew_Matrix(int CrewID)
        {
            try
            {
                return objCrewDAL.Get_Crew_Matrix_DL(CrewID);
            }
            catch
            {
                throw;
            }
        }
        public void Insert_Crew_Matrix(int CrewID, int? Certificate_Of_Competency, int? Issuing_Country, string Administration_Acceptance, string Tanker_Certification, string STCWVPara, string Radio_Qualification, string English_Proficiency, int Created_By)
        {
            try
            {
                objCrewDAL.Insert_Crew_Matrix_DL(CrewID, Certificate_Of_Competency, Issuing_Country, Administration_Acceptance, Tanker_Certification, STCWVPara, Radio_Qualification, English_Proficiency, Created_By);
            }
            catch
            {
                throw;
            }
        }
        public int Delete_CrewMatrix_Attachments(int Code)
        {
            try
            {
                return objCrewDAL.Delete_CrewMatrix_Attachments_DL(Code);
            }
            catch
            {
                throw;
            }
        }
        public DataTable Get_CrewMatrix_Attachments(int CrewID)
        {
            try
            {
                return objCrewDAL.Get_CrewMatrix_Attachments_DL(CrewID);
            }
            catch
            {
                throw;
            }
        }
        public int Insert_Crew_MatrixAttachment(int CrewID, string Attachment_Name, string Flag_Attach, int UserID)
        {
            try
            {
                return objCrewDAL.Insert_Crew_MatrixAttachment_DL(CrewID, Attachment_Name, Flag_Attach, UserID);
            }
            catch
            {
                throw;
            }
        }

        public int Validate_InterviewConfig(int CrewID)
        {
            try
            {
                return objCrewDAL.Validate_InterviewConfig_DL(CrewID);
            }
            catch
            {
                throw;
            }
        }
        public int Update_CrewManningOffice(int CrewID, int ManningOfficeId)
        {
            try
            {
                return objCrewDAL.Update_CrewManningOffice_DL(CrewID, ManningOfficeId);
            }
            catch
            {
                throw;
            }
        }
        public int Get_RejectedInterviewCount_MA(int CrewID)
        {
            try
            {
                return objCrewDAL.Get_RejectedInterviewCount_MA_DL(CrewID);
            }
            catch
            {
                throw;
            }
        }
        public int SP_CRW_Recommendation(int CrewID, int CreatedBy)
        {
            try
            {
                return objCrewDAL.SP_CRW_Recommendation_DL(CrewID, CreatedBy);
            }
            catch
            {
                throw;
            }
        }
        public int Get_RejectedCount(int CrewID)
        {
            try
            {
                return objCrewDAL.Get_RejectedCount_DL(CrewID);
            }
            catch
            {
                throw;
            }
        }
        public int Undo_Rejection(int CrewID, int Modified_By)
        {
            try
            {
                return objCrewDAL.Undo_Rejection_DL(CrewID, Modified_By);
            }
            catch
            {
                throw;
            }
        }
        public int ResetCrewPassword(int CrewID, int Modified_By)
        {
            try
            {
                return objCrewDAL.ResetCrewPassword_DL(CrewID, Modified_By);
            }
            catch
            {
                throw;
            }
        }
        public DataTable Get_Crew_Documents(int CrewID, string SearchText)
        {
            try
            {
                return objCrewDAL.Get_Crew_Documents_DL(CrewID, SearchText);
            }
            catch
            {
                throw;
            }
        }
        public int Check_Crew_Refererce(int CrewID)
        {
            try
            {
                return objCrewDAL.Check_Crew_Refererce_DL(CrewID);
            }
            catch
            {
                throw;
            }
        }

        public DataTable Get_Crew_Referer_Details(int CrewID)
        {
            try
            {
                return objCrewDAL.Get_Crew_Referer_Details_DL(CrewID);
            }
            catch
            {
                throw;
            }
        }
        public int Save_Crew_Reference_Details(int ID, int CrewID, string REFERER_NAME, string REFERER_MOBILE, string REFERENCE_DATE, string PERSON_QUIERED_NAME, string PERSON_QUIERED_TITLE, int User_ID)
        {
            try
            {
                DateTime dt = DateTime.Parse(REFERENCE_DATE);

                return objCrewDAL.Save_Crew_Reference_Details_DL(ID, CrewID, REFERER_NAME, REFERER_MOBILE, dt, PERSON_QUIERED_NAME, PERSON_QUIERED_TITLE, User_ID);
            }
            catch
            {
                throw;
            }
        }



        public int DEL_CrewReferenceDetail(int CrewID, int ID, int Deleted_By)
        {
            try
            {
                return objCrewDAL.DEL_CrewReferenceDetail_DL(CrewID, ID, Deleted_By);
            }
            catch
            {
                throw;
            }
        }
        public DataTable Get_CrewFBMInfo(int CrewID)
        {
            try
            {
                return objCrewDAL.Get_CrewFBMInfo_DL(CrewID);
            }
            catch
            {
                throw;
            }
        }
        public DataTable Get_WagesNationalityList(int RankID, int Contract_Type)
        {
            try
            {
                return objCrewDAL.Get_WagesNationalityList_DL(RankID, Contract_Type);
            }
            catch
            {
                throw;
            }
        }
        public DataTable Get_CrewOtherServices(int CrewID)
        {
            try
            {
                return objCrewDAL.Get_CrewOtherServices_DL(CrewID);
            }
            catch
            {
                throw;
            }
        }
        public int Delete_CrewOtherServices(int ID, int CrewID, int User_ID)
        {
            try
            {
                return objCrewDAL.Delete_CrewOtherServices_DL(ID, CrewID, User_ID);
            }
            catch
            {
                throw;
            }
        }
        public int Update_CrewOtherServices(int ID, int CrewID, DateTime DateFrom, DateTime DateTo, int JoiningRank, string ServiceType, int User_ID, string Remarks)
        {
            try
            {
                return objCrewDAL.Update_CrewOtherServices_DL(ID, CrewID, DateFrom, DateTo, JoiningRank, ServiceType, User_ID, Remarks);
            }
            catch
            {
                throw;
            }
        }
        public int Insert_CrewOtherServices(int CrewID, DateTime DateFrom, DateTime DateTo, int JoiningRank, string ServiceType, int User_ID, string Remarks)
        {
            try
            {
                return objCrewDAL.Insert_CrewOtherServices_DL(CrewID, DateFrom, DateTo, JoiningRank, ServiceType, User_ID, Remarks);
            }
            catch
            {
                throw;
            }
        }
        public DataTable Get_CrewOtherService_Dtl(int ID)
        {
            try
            {
                return objCrewDAL.Get_CrewOtherService_Dtl_DL(ID);
            }
            catch
            {
                throw;
            }
        }
        public DataTable Get_RJBList_Index(int? RankId, int? Status, string FromDate, string ToDate, int? CurrVesselId, string ExpectFromDate, string ExpectToDate, string SearchText, int? UserID, int PAGE_SIZE, int PAGE_INDEX, ref int SelectRecordCount, string sortbycoloumn, int? sortdirection)
        {
            try
            {
                return objCrewDAL.Get_RJBList_Index_DL(null, RankId, Status, FromDate, ToDate, CurrVesselId, ExpectFromDate, ExpectToDate, SearchText, UserID, PAGE_SIZE, PAGE_INDEX, ref SelectRecordCount, sortbycoloumn, sortdirection);
            }
            catch
            {
                throw;
            }
        }

        public DataTable Get_RJBList_Index(int ID)
        {
            try
            {
                int SelectRecordCount = 1;
                DateTime dtFrom = DateTime.Parse("1900/01/01");
                DateTime dtTo = DateTime.Parse("2900/01/01");
                return objCrewDAL.Get_RJBList_Index_DL(ID, null, null, dtFrom.ToString("yyyy/MM/dd"), dtTo.ToString("yyyy/MM/dd"), null, dtFrom.ToString("yyyy/MM/dd"), dtTo.ToString("yyyy/MM/dd"), "", null, 1, 1, ref SelectRecordCount, "", null);
            }
            catch
            {
                throw;
            }
        }
        public int Update_RJBApproval(int Status, int ID, int User_ID, decimal? Amount, string Remark, string Mode)
        {
            try
            {
                return objCrewDAL.Update_RJBApproval_DL(Status, ID, User_ID, Amount, Remark, Mode);
            }
            catch
            {
                throw;
            }
        }
        public int CheckCrewStatus(int CrewID)
        {
            try
            {
                return objCrewDAL.CheckCrewStatus_DL(CrewID);
            }
            catch
            {
                throw;
            }
        }
        public DataSet Get_CrewSeniorityDetails(int CrewId)
        {
            try
            {
                return objCrewDAL.Get_CrewSeniorityDetails_DL(CrewId);
            }
            catch
            {
                throw;
            }
        }
        public DataSet Get_CrewSeniorityForReversing(int CrewId)
        {
            try
            {
                return objCrewDAL.Get_CrewSeniorityForReversing_DL(CrewId);
            }
            catch
            {
                throw;
            }
        }
        public int GetCrewServiceStatus(int CrewID)
        {
            try
            {
                return objCrewDAL.GetCrewServiceStatus(CrewID);
            }
            catch
            {
                throw;
            }
        }
        public int Get_CrewStatus(int CrewID)
        {
            try
            {
                return objCrewDAL.Get_CrewStatus_DL(CrewID);
            }
            catch
            {
                throw;
            }
        }
        public DataTable Get_GET_CrewRankScale(int CrewId)
        {
            try
            {
                return objCrewDAL.Get_GET_CrewRankScale_DL(CrewId);
            }
            catch
            {
                throw;
            }
        }

        public int Save_AttachedFileInfo(int CrewID, string FileName, string FilePath, int Created_By)
        {
            try
            {
                return objCrewDAL.Save_AttachedFileInfo_DL(CrewID, FileName, FilePath, Created_By);
            }
            catch
            {
                throw;
            }
        }
        public DataTable Get_AttachedFileInfo(int CrewId)
        {
            try
            {
                return objCrewDAL.Get_AttachedFileInfo_DL(CrewId);
            }
            catch
            {
                throw;
            }
        }

        public int Delete_AttachedFileInfo(int ID, int DeletedBy)
        {
            try
            {
                return objCrewDAL.Delete_AttachedFileInfo_DL(ID, DeletedBy);
            }
            catch
            {
                throw;
            }
        }

        public DataTable Check_Get_AttachedFilePath(int CrewId)
        {
            try
            {
                return objCrewDAL.Check_Get_AttachedFilePatho_DL(CrewId);
            }
            catch
            {
                throw;
            }
        }
        public DataTable Get_Crew_VoyageVerifiedDocuments(int CrewID, int VoyageID)
        {
            try
            {
                return objCrewDAL.Get_Crew_VoyageVerifiedDocuments_DL(CrewID, VoyageID);
            }
            catch
            {
                throw;
            }
        }
        public DataTable Get_CrewPersonalDetailsByID1(int ID)
        {
            try
            {
                return objCrewDAL.Get_CrewPersonalDetailsByID1(ID);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Get Crew years of exp in operator, rank, tanker type and all tankers
        /// </summary>
        /// <param name="CrewId"></param>
        /// <returns></returns>
        public DataSet CRW_Get_CM_CrewYearsExp(int CrewID, int VesselID, string Date, int RankId, int VoyageID)
        {
            return objCrewDAL.CRW_Get_CM_CrewYearsExp(CrewID, VesselID, Date, RankId, VoyageID);
        }

        /// <summary>
        /// Get Default Values for Crew Matrix Detail tab
        /// </summary>
        /// <param name="CrewId"></param>
        /// <returns></returns>
        public DataSet Get_CrewMatix_DetailAndValue(int CrewID)
        {
            return objCrewDAL.Get_CrewMatix_DetailAndValue(CrewID);
        }



        public int Check_CrewAssignment(int CrewID_SigningOff, int CrewID_UnAssigned, ref string VesselName)
        {
            try
            {

                return objCrewDAL.Check_CrewAssignment_DL(CrewID_SigningOff, CrewID_UnAssigned, ref VesselName);
            }
            catch
            {
                throw;
            }
        }
        public int Delete_CrewOpenAssignment(int ID, int Deleted_By)
        {
            try
            {

                return objCrewDAL.Delete_CrewOpenAssignment_DL(ID, Deleted_By);
            }
            catch
            {
                throw;
            }
        }

        public DataTable Get_GroupName(DataTable dt)
        {
            try
            {
                return objCrewDAL.Get_GroupName(dt);
            }
            catch
            {
                throw;
            }
        }

        public DataTable CheckVesselTypeForCrew(int CrewId, int VesselId)
        {
            try
            {
                return objCrewDAL.CheckVesselTypeForCrew(CrewId, VesselId);
            }
            catch
            {
                throw;
            }
        }
        public DataTable CheckVesselTypeForCrew(DataTable dtCrewId, int VesselId)
        {
            try
            {
                return objCrewDAL.CheckVesselTypeForCrew(dtCrewId, VesselId);
            }
            catch
            {
                throw;
            }
        }
        public DataTable GET_VesselTypeForCrew(int CrewId)
        {
            try
            {
                return objCrewDAL.GET_VesselTypeForCrew(CrewId);
            }
            catch
            {
                throw;
            }
        }
        public DataSet CRW_GET_DefaultValuesCrewAddEdit(int UserCompanyID)
        {
            return objCrewDAL.CRW_GET_DefaultValuesCrewAddEdit(UserCompanyID);
        }

        public DataSet CRW_LIB_CD_GetCrewDetails(int CrewId)
        {
            return objCrewDAL.CRW_LIB_CD_GetCrewDetails(CrewId);
        }

        public int CRW_INS_CrewDetailsScreen1(string Staff_Surname, string Staff_Name, string Staff_Midname, string Alias,
                                              string Staff_Born_Place, DateTime? Staff_Birth_Date, string MaritalStatus, int Staff_Nationality, int Race,
                                              string SSN, string Address, string AddressLine1, string AddressLine2, string City, string State, string ZipCode,
                                              int CountryId, string NearestAirport, int NearestAirportID, string Telephone, string Email, string Mobile,
                                              string Fax, int Rank_Applied, int ManningOfficeID, DateTime? Available_From_Date, DateTime? HireDate, int UnionId, int UnionBranchID, int VeteranStatusID,
                                              int UnionBookId, int CrewID, int CreatedModifiedBy, string AddressType, string CrewImageURL, ref string Result)
        {
            return objCrewDAL.CRW_INS_CrewDetailsScreen1(Staff_Surname, Staff_Name, Staff_Midname, Alias,
                                              Staff_Born_Place, Staff_Birth_Date, MaritalStatus, Staff_Nationality, Race,
                                              SSN, Address, AddressLine1, AddressLine2, City, State, ZipCode,
                                              CountryId, NearestAirport, NearestAirportID, Telephone, Email, Mobile,
                                              Fax, Rank_Applied, ManningOfficeID, Available_From_Date, HireDate, UnionId, UnionBranchID, VeteranStatusID,
                                              UnionBookId, CrewID, CreatedModifiedBy, AddressType, CrewImageURL, ref Result);
        }

        public string CRW_INS_CrewDetailsScreen2(CrewProperties objCrewProperties, int CreatedModifiedBy, ref string Result)
        {
            return objCrewDAL.CRW_INS_CrewDetailsScreen2(objCrewProperties, CreatedModifiedBy, ref Result);
        }

        public int CRW_INS_CrewDetailsScreen3(int CrewID, string FirstName, string Surname, string RealtionShip, string PhoneNumber, DateTime? DOB, string SSN, string Address, string Address1, string Address2, string City, string State, int Country, string ZipCode, int Isbeneficiary, int CreatedModifiedBy, int NOKID, string AddressType, ref int Result)
        {
            return objCrewDAL.CRW_INS_CrewDetailsScreen3(CrewID, FirstName, Surname, RealtionShip, PhoneNumber, DOB, SSN, Address, Address1, Address2, City, State, Country, ZipCode, Isbeneficiary, CreatedModifiedBy, NOKID, AddressType, ref Result);
        }
        public DataTable CheckCrewMandatoryFields(int CrewID)
        {
            return objCrewDAL.CheckCrewMandatoryFields(CrewID);
        }
        public int CRW_INS_AddVesselTye(DataTable dtCrewID, int VesselId, int CreatedBy, ref int Result)
        {
            return objCrewDAL.CRW_INS_AddVesselTye(dtCrewID, VesselId, CreatedBy, ref Result);
        }
    }
}
