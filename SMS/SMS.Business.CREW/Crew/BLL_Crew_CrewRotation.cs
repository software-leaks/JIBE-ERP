using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.SqlClient;

using SMS.Data.Crew;
using SMS.Data.Infrastructure;


namespace SMS.Business.Crew
{
    public class BLL_Crew_CrewRotation
    {
        //DAL_Crew_CrewDetails objCrewDAL = new DAL_Crew_CrewDetails();
        //DAL_Infra_UserCredentials objDalUser = new DAL_Infra_UserCredentials();

        static IFormatProvider  iFormatProvider = new System.Globalization.CultureInfo("en-GB", true);

        public static DataTable Get_SalaryExportReport(int CrewID, string CREW_STATUS, string FROM_JOINDATE, string TO_JOINDATE, int NATIONALITY, int VESSEL_ID, int ManningOfficeID, int RankID, int COC, int FleetID, int Vessel_Manager, string SearchText, int UserID)
        {
            try
            {
                DateTime Dt_FromJoningDate = DateTime.Today;
                DateTime Dt_ToJoningDate = DateTime.Today;

                string UserType = GetCurrentUserType(UserID);

                string SQL = "";

                SQL = @"SELECT     
                    CURR_VESSEL.Vessel_Name, 
                    CD.ID, CD.Staff_Code, 
                    CD.Staff_Surname,
                    CD.Staff_Name,
                    CRW_DTL_BankAccounts.Beneficiary, 
                    CRW_DTL_BankAccounts.Bank_Name, 
                    CRW_DTL_BankAccounts.Bank_Address, 
                    (char(39) + CRW_DTL_BankAccounts.Acc_NO) as Acc_NO, 
                    CRW_DTL_BankAccounts.SwiftCode,
                    '' as Other, 
                    '' as Basic,
                    '' as Advance,
                    '' as Expenses,
                    '' as ToPay,
                    V.Sign_On_Date, 
                    V.Est_Sing_Off_Date, 
                    V.Sign_Off_Date
        FROM         LIB_COMPANY AS MANNING_OFFICES RIGHT OUTER JOIN
                          (SELECT     CRW_DTL_CrewChangeEvent_Members.CrewID, MAX(CRW_DTL_CrewChangeEvent.Event_Date) AS Event_Date
                            FROM          CRW_DTL_CrewChangeEvent INNER JOIN
                                                   CRW_DTL_CrewChangeEvent_Members ON CRW_DTL_CrewChangeEvent.PKID = CRW_DTL_CrewChangeEvent_Members.EventID
                            WHERE      (CRW_DTL_CrewChangeEvent.Active_Status = 1) AND (CRW_DTL_CrewChangeEvent_Members.Active_Status = 1) AND 
                                                   (CRW_DTL_CrewChangeEvent.Event_Status = 1) AND (CRW_DTL_CrewChangeEvent.Event_Date >= GETDATE())
                            GROUP BY CRW_DTL_CrewChangeEvent_Members.CrewID) AS EV RIGHT OUTER JOIN
                          (SELECT     ID, CrewID, Beneficiary, Bank_Name, Bank_Address, Acc_NO, Default_Acc, Verified, VerifiedBy, Link, Vessel_Code, Created_By, Date_Of_Creatation, 
                                                   Modified_By, Date_Of_Modification, Deleted_By, Date_Of_Deletion, Active_Status, SwiftCode, ID_Vessel, Office_ID
                            FROM          CRW_DTL_BankAccounts AS CRW_DTL_BankAccounts_1
                           ) AS CRW_DTL_BankAccounts RIGHT OUTER JOIN
                      CRW_LIB_Crew_Details AS CD ON CRW_DTL_BankAccounts.CrewID = CD.ID LEFT OUTER JOIN
                      CRW_LIB_Crew_Ranks AS AppliedRank ON CD.Rank_Applied = AppliedRank.ID LEFT OUTER JOIN
                      LIB_VESSELS INNER JOIN
                      CRW_LIB_Crew_Ranks AS AssignedRank INNER JOIN
                      CRW_DTL_Last_VesselAssignment_View ON AssignedRank.ID = CRW_DTL_Last_VesselAssignment_View.Joining_Rank ON 
                      LIB_VESSELS.Vessel_ID = CRW_DTL_Last_VesselAssignment_View.Vessel_ID ON CD.ID = CRW_DTL_Last_VesselAssignment_View.CrewID ON 
                      EV.CrewID = CD.ID ON MANNING_OFFICES.Id = CD.ManningOfficeID LEFT OUTER JOIN
                      CRW_LIB_Crew_Ranks AS ApprovedRank ON CD.Staff_Rank = ApprovedRank.ID LEFT OUTER JOIN
                      LIB_Country ON CD.Staff_Nationality = LIB_Country.ID LEFT OUTER JOIN
                          (SELECT     VoyageID, SUM(Amount) AS Amount
                            FROM          ACC_DTL_JOINING_EARN_DEDUCTION
                            WHERE      (Active_Status = 1)
                            GROUP BY VoyageID) AS salary RIGHT OUTER JOIN
                      CRW_LIB_Crew_Ranks AS rank RIGHT OUTER JOIN
                      LIB_VESSELS AS CURR_VESSEL INNER JOIN
                          (SELECT     V1.ID, V1.CrewID, V1.Joining_Date, V1.Sign_On_Date, V1.Joining_Rank, V1.Vessel_ID, V1.Est_Sing_Off_Date, V1.Staff_Code, V1.Sign_Off_Date, 
                                                   V1.Voyage_Remarks, V1.COC_Modified
                            FROM          CRW_Dtl_Crew_Voyages AS V1 INNER JOIN
                                                       (SELECT     CrewID, MAX(Sign_On_Date) AS Sign_On_Date
                                                         FROM          CRW_Dtl_Crew_Voyages
                                                         WHERE      (Active_Status = 1)
                                                         GROUP BY CrewID) AS V ON V1.CrewID = V.CrewID AND V1.Sign_On_Date = V.Sign_On_Date
                            WHERE      (V1.Active_Status = 1) AND (V1.Sign_On_Date IS NOT NULL)) AS V ON CURR_VESSEL.Vessel_ID = V.Vessel_ID INNER JOIN
                      LIB_COMPANY AS VESSEL_MANAGERS ON CURR_VESSEL.Vessel_Manager = VESSEL_MANAGERS.Id ON rank.ID = V.Joining_Rank ON salary.VoyageID = V.ID ON 
                      CD.ID = V.CrewID
    WHERE     (CD.ID > 0)";

                if (CrewID > 0)
                    SQL += " AND CD.ID = @CrewID";

                if (CREW_STATUS != "INACTIVE")
                    SQL += " AND (CD.Active_Status = 1) ";

                if (SearchText != "")
                {
                    SQL += " AND (CD.Staff_Name LIKE @Name ";
                    SearchText = SearchText.Trim();
                    string[] arrSrarch = SearchText.Split(' ');
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


                if (NATIONALITY != 0)
                    SQL += " AND (CD.Staff_Nationality = @Nation) ";

                if (RankID != 0)
                    SQL += " AND (v.Joining_Rank = @RankID or AssignedRank.ID = @RankID or ApprovedRank.ID = @RankID or AppliedRank.ID = @RankID) ";

                if (UserType == "MANNING AGENT")
                {
                    SQL += " AND (CD.ManningOfficeID = @ManningOfficeID) ";

                    if (Vessel_Manager != 0)
                        SQL += " AND (CURR_VESSEL.VESSEL_MANAGER = @Vessel_Manager) ";
                }
                else if (UserType == "OFFICE USER")
                {
                    if (ManningOfficeID == 0)
                        SQL += " AND (CD.ManningOfficeID = (select LIB_USER.CompanyID from LIB_USER where UserID=@UserID ) OR CD.ManningOfficeID IN (SELECT CHILD_COMPANY_ID FROM LIB_COMPANY_RELATIONSHIPS WHERE PARENT_COMPANY_ID=(select LIB_USER.CompanyID from LIB_USER where UserID=@UserID ) AND RELATION=1)) ";
                    else
                        SQL += " AND (CD.ManningOfficeID = @ManningOfficeID) ";


                    if (Vessel_Manager != 0)
                        SQL += " AND (CURR_VESSEL.VESSEL_MANAGER = @Vessel_Manager) ";
                }
                else if (UserType == "VESSEL MANAGER")
                {
                    SQL += " AND (CURR_VESSEL.VESSEL_MANAGER = @Vessel_Manager or CURR_VESSEL.VESSEL_MANAGER =(select LIB_USER.CompanyID from LIB_USER where UserID=@UserID )) ";
                    SQL += " AND (CD.ManningOfficeID in (select parent_company_id from LIB_COMPANY_RELATIONSHIPS where child_company_id = (select LIB_USER.CompanyID from LIB_USER where UserID=@UserID ) and relation = 2)) ";

                }
                else if (UserType == "ADMIN")
                {
                    if (ManningOfficeID != 0)
                        SQL += " AND (CD.ManningOfficeID = @ManningOfficeID) ";

                    if (Vessel_Manager != 0)
                        SQL += " AND (CURR_VESSEL.VESSEL_MANAGER = @Vessel_Manager) ";

                }

                if (COC > 0)
                {
                    SQL += " AND (V.Sign_On_Date IS NOT NULL AND V.SIGN_OFF_DATE IS NULL AND (V.EST_SING_OFF_DATE BETWEEN GETDATE() and DATEADD(DD," + COC + ",GETDATE()) ))";
                }




                switch (CREW_STATUS)
                {
                    case "CURRENT":

                        SQL += " AND (V.SIGN_ON_DATE IS NOT NULL AND  v.SIGN_OFF_DATE IS NULL) ";

                        if (FROM_JOINDATE != "" && TO_JOINDATE != "")
                        {
                            Dt_FromJoningDate = DateTime.Parse(FROM_JOINDATE, iFormatProvider);
                            Dt_ToJoningDate = DateTime.Parse(TO_JOINDATE, iFormatProvider);

                            SQL += " AND (v.Joining_Date between @JoinFrmDT and @JoinTODT ) ";
                        }

                        if (VESSEL_ID != 0)
                        {
                            SQL += " AND (v.Vessel_ID = @Vessel_ID)";
                        }

                        if (FleetID != 0)
                        {
                            SQL += " AND (CURR_VESSEL.FleetCode = @FleetID)";
                        }


                        break;
                    case "SIGNED OFF":
                        SQL += " AND (V.Sign_On_Date IS NOT NULL AND V.SIGN_OFF_DATE IS NOT NULL)";
                        SQL += " AND cd.status_code is null";
                        break;

                    case "NO VOYAGE":
                        SQL += " AND ((V.Sign_On_Date IS  NULL AND  v.SIGN_OFF_DATE IS NULL) and (CD.MANNINGOFFICESTATUS = 1 AND CD.CrewManagerApproval = 1) AND (SOff.CrewID_UnAssigned IS  NULL AND ev.Event_Date IS  NULL)) ";
                        SQL += " AND cd.status_code is null";

                        break;

                    case "INACTIVE":
                        SQL += " AND (CD.ACTIVE_STATUS = 0)";

                        break;

                    case "ASSIGNED":
                        SQL += " AND (SOff.CrewID_UnAssigned IS NOT NULL)";
                        SQL += " AND cd.status_code is null";
                        break;

                    case "PLANNED":
                        SQL += " AND (EV.Event_Date is not null)";
                        SQL += " AND cd.status_code is null";
                        break;

                    case "DECISION PENDING":
                        SQL += " AND (isnull(CD.ManningOfficeStatus, 0)= 0 OR isnull(CD.CrewManagerApproval,0) = 0 )";
                        SQL += " AND cd.status_code is null";
                        break;

                    case "NTBR":
                        SQL += " AND (CD.Status_Code = 'NTBR')";
                        break;

                    case "MEDICALLY UNFIT":
                        SQL += " AND (CD.Status_Code = 'MEDICALLY UNFIT')";
                        break;

                    case "REJECTED":
                        SQL += " AND (CD.CrewManagerApproval = -1)";
                        SQL += " AND cd.status_code is null";
                        break;

                    case "":
                        if (VESSEL_ID != 0)
                        {
                            SQL += " AND (v.Vessel_ID = @Vessel_ID or CRW_DTL_Last_VesselAssignment_View.vessel_id = @Vessel_ID )";
                        }

                        if (FleetID != 0)
                        {
                            SQL += " AND (CURR_VESSEL.FleetCode = @FleetID or LIB_VESSELS.FLEETCODE=@FleetID)";
                        }
                        break;

                }

                SQL += " ORDER BY rank.Rank_Sort_Order, AssignedRank.Rank_Sort_Order,ApprovedRank.Rank_Sort_Order, Staff_SurName";

                SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewID",CrewID),
                                            new SqlParameter("@UserID",UserID),
                                            new SqlParameter("@Name","%"+SearchText+"%"),                                            
                                            new SqlParameter("@JoinFrmDT",Dt_FromJoningDate),
                                            new SqlParameter("@JoinTODT",Dt_ToJoningDate),
                                            new SqlParameter("@Nation",NATIONALITY),
                                            new SqlParameter("@Vessel_ID",VESSEL_ID),
                                            new SqlParameter("@ManningOfficeID",ManningOfficeID),
                                            new SqlParameter("@RankID",RankID),
                                            new SqlParameter("@FleetID",FleetID),
                                            new SqlParameter("@Vessel_Manager",Vessel_Manager)
                                        };

                return DAL_Crew_CrewRotation.ExecuteQuery(SQL, sqlprm);


            }
            catch
            {
                throw;
            }
        }

        public static DataSet Get_RotationReport(int CrewID, int Vessel_Manager, int ManningOfficeID, int FleetID, int VESSEL_ID, int RankID, string From_Dt, string To_Dt, string SearchText, int UserID)
        {
            try
            {
                return DAL_Crew_CrewRotation.Get_RotationReport_DL(CrewID, Vessel_Manager, ManningOfficeID, FleetID, VESSEL_ID, RankID, From_Dt, To_Dt, SearchText, UserID);
            }
            catch
            {
                throw;
            }
        }

        private static string GetCurrentUserType(int iUserID)
        {
            return DAL_Crew_CrewRotation.Get_UserData_DL(iUserID, "USER_TYPE");
        }
    }
}
