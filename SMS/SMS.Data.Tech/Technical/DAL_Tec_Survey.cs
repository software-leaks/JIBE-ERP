using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;



namespace SMS.Data.Technical
{
	public class DAL_Tec_Survey
	{
		private string connection = "";
		public DAL_Tec_Survey(string ConnectionString)
		{
			connection = ConnectionString;
		}

		public DAL_Tec_Survey()
		{
			connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
		}
		public DataTable Get_Survey_MainCategoryList_DL()
		{
			return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SURV_Get_SurveyMainCategoryList").Tables[0];
		}
		public DataTable Get_Survey_CategoryList_DL(string SearchText)
		{
			SqlParameter[] sqlprm = new SqlParameter[]
			{ 
				new SqlParameter("@SearchText", SearchText)
			};
			return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SURV_Sp_Get_SurveyCategoryList", sqlprm).Tables[0];
		}

		public DataTable Get_SurveyCertificate_List_DL(int CategoryID)
		{
			SqlParameter[] sqlprm = new SqlParameter[]
			{ 
				new SqlParameter("@CategoryID", CategoryID)
			};
			return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SURV_Sp_Get_SurveyCertificateList", sqlprm).Tables[0];
		}

		public DataTable Get_SurveyCertificate_List_DL(int CategoryID, string SearchText)
		{
			SqlParameter[] sqlprm = new SqlParameter[]
			{ 
				new SqlParameter("@CategoryID", CategoryID),
				new SqlParameter("@SearchText", SearchText)
			};
			return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SURV_Sp_Get_SurveyCertificateList_Lib", sqlprm).Tables[0];
		}

		public DataTable Get_SurveyCertificate_List_DL(DataTable dtCatIDList, int CatID, int FleetID, int VesselID)
		{
			SqlParameter[] sqlprm = new SqlParameter[]
			{ 
				new SqlParameter("@FleetID", FleetID),
				new SqlParameter("@VesselID", VesselID),
				new SqlParameter("@CategoryID", CatID),
				new SqlParameter("@dtCatIDList", dtCatIDList)
			};
			return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SURV_Sp_Get_SurveyCertificateList", sqlprm).Tables[0];
		}
		public DataSet Get_SurvayList_DL(int FleetID, int VesselID, int MainCategoryId, int CategoryID, int CertificateID, DateTime IssueFrom, DateTime IssueTo, DateTime ExpFrom, DateTime ExpTo, int Verified, string SearchText, int ExpiryInDays, Boolean ShowAll, string CatIDList, int? PAGE_SIZE, int? PAGE_INDEX, int? SelectRecordCount)
		{
			SqlParameter[] sqlprm = new SqlParameter[]
			{ 
				new SqlParameter("@FleetID", FleetID),
				new SqlParameter("@VesselID", VesselID),
				new SqlParameter("@MainCategoryId", MainCategoryId),
				new SqlParameter("@CategoryID", CategoryID),
				new SqlParameter("@CertificateID", CertificateID),
				new SqlParameter("@IssueFrom", IssueFrom),
				new SqlParameter("@IssueTo", IssueTo),
				new SqlParameter("@ExpFrom", ExpFrom),
				new SqlParameter("@ExpTo", ExpTo),
				new SqlParameter("@Verified", Verified),
				new SqlParameter("@SearchText", "%"+SearchText+"%"),                
				new SqlParameter("@ExpiryInDays",ExpiryInDays),
				new SqlParameter("@ShowAll",ShowAll),
				new SqlParameter("@CatIDList",CatIDList),
				new SqlParameter("@PAGE_SIZE",PAGE_SIZE),
				new SqlParameter("@PAGE_INDEX",PAGE_INDEX) ,
				new SqlParameter("@SelectRecordCount",SelectRecordCount)
			};
			return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SURV_Get_SurveyCertificateList", sqlprm);
		}
	   /* public DataSet Get_SurvayList_DL(int FleetID, int VesselID, int MainCategoryId, int CategoryID, int CertificateID, DateTime IssueFrom, DateTime IssueTo, DateTime ExpFrom, DateTime ExpTo, int Verified, string SearchText, int ExpiryInDays, Boolean ShowAll, string CatIDList, int? PAGE_SIZE, int? PAGE_INDEX, int? SelectRecordCount)
		{

			string SQL = "";
			string SQL_RecCount = "";

			SQL = @"SELECT                          
						tblSurvVessel_3Details.OfficeID, 
						tblSurvVessel_3Details.DateOfIssue, 
						tblSurvVessel_3Details.DateOfExpiry, 
						tblSurvVessel_3Details.Remarks, 
						tblSurvVessel_3Details.Verified_By, 
						tblSurvVessel_3Details.Verified_Date, 
						tblSurvVessel_3Details.NAExpiryDate, 
						tblSurvVessel_3Details.FollowupReminderDt, 
						tblSurvVessel_3Details.FollowupReminder,
						tblSurvVessel_2Lib.Vessel_ID, 
						tblSurvVessel_2Lib.Surv_Vessel_ID, 
						tblSurvVessel_2Lib.Surv_ID, 
						tblSurvVessel_2Lib.EquipmentType, 
						tblSurvVessel_3Details.Surv_Details_ID,                          
						LIB_SurveyCategories.Survey_Category, 
						tblSurv_Lib.Survey_Cert_Name, 
						tblSurv_Lib.Survey_Cert_remarks, 
						tblSurv_Lib.Term, 
						tblSurv_Lib.Survey_Category_ID, 
						tblSurv_Lib.GraceRange,
						LIB_VESSELS.Vessel_Name, 
						ISNULL((SELECT     COUNT(0) AS Expr1 FROM tblSurvVessel_5Attach
							  WHERE  (Vessel_ID = tblSurvVessel_3Details.Vessel_ID) AND (Surv_Details_ID = tblSurvVessel_3Details.Surv_Details_ID)), 0) AS AttachmentCount, 
						CASE WHEN len(tblSurvVessel_3Details.Verified_Date) > 0 THEN 1 ELSE 0 END AS Verified, 
						LIB_VESSELS.FleetCode, tblSurv_Lib.Sort_Order,
						LIB_SurveyMainCategories.Survey_Category AS Survey_MainCategory,
						tblSurvVessel_3Details.ExtensionDate
					FROM         tblSurv_Lib INNER JOIN
											tblSurvVessel_2Lib ON tblSurv_Lib.Surv_ID = tblSurvVessel_2Lib.Surv_ID INNER JOIN
											LIB_SurveyCategories ON tblSurv_Lib.Survey_Category_ID = LIB_SurveyCategories.ID INNER JOIN
											LIB_SurveyCategories  AS LIB_SurveyMainCategories ON LIB_SurveyCategories.MainCategoryId = LIB_SurveyMainCategories.ID INNER JOIN
											LIB_VESSELS ON tblSurvVessel_2Lib.Vessel_ID = LIB_VESSELS.Vessel_ID INNER JOIN
											tblSurvVessel_3Details INNER JOIN
												(SELECT     Vessel_ID, Surv_Vessel_ID, MAX(Created_Date) AS MaxCreated_Date
												FROM          tblSurvVessel_3Details AS tblSurvVessel_3Details_1
												WHERE      (Vessel_ID > 0)
					GROUP BY Vessel_ID, Surv_Vessel_ID) AS MaxSurvDetails 
						ON tblSurvVessel_3Details.Vessel_ID = MaxSurvDetails.Vessel_ID 
						AND  tblSurvVessel_3Details.Surv_Vessel_ID = MaxSurvDetails.Surv_Vessel_ID 
						AND tblSurvVessel_3Details.Created_Date = MaxSurvDetails.MaxCreated_Date ON 
						tblSurvVessel_2Lib.Surv_Vessel_ID = tblSurvVessel_3Details.Surv_Vessel_ID 
						AND tblSurvVessel_2Lib.Vessel_ID = tblSurvVessel_3Details.Vessel_ID
					
					WHERE     (tblSurvVessel_2Lib.ACTIVE = 1) 
						AND (tblSurv_Lib.Active = 1)
						AND LIB_VESSELS.HANDOVER_DATE is null";

			if (Verified == 1)
				SQL += @" AND tblSurvVessel_3Details.Verified_Date is not null";
			else if (Verified == -1)
				SQL += @" AND tblSurvVessel_3Details.Verified_Date is  null";

			if (ShowAll != true)
			{
				SQL += @" AND ((tblSurvVessel_3Details.NAExpiryDate <> - 1) OR (isnull(tblSurvVessel_3Details.NAExpiryDate,0) =0))";

				SQL += @" AND (tblSurvVessel_3Details.DateOfIssue between @IssueFrom and @IssueTo)";

				SQL += @" AND (
								(( tblSurvVessel_3Details.DateOfExpiry between @ExpFrom and @ExpTo) AND (tblSurvVessel_3Details.NAExpiryDate <> -1) ) 
								OR tblSurvVessel_3Details.DateOfExpiry is null 
							)";
			}

			if (VesselID > 0)
				SQL += " and tblSurvVessel_2Lib.Vessel_ID  = @VesselID";
			else if (FleetID > 0)
				SQL += " and LIB_VESSELS.fleetcode  = @FleetID";

			if (CategoryID > 0)
				SQL += " and tblSurv_Lib.Survey_Category_ID  =  @CategoryID";

			if (CatIDList != "")
				SQL += " AND tblSurv_Lib.Survey_Category_ID IN (" + CatIDList + ")";

			if ((CategoryID == 0 && CatIDList == "") && @MainCategoryId > 0)
				SQL += " and LIB_SurveyCategories.MainCategoryId  = @MainCategoryId";

			if (CertificateID > 0)
				SQL += " and tblSurvVessel_2Lib.Surv_ID  = @CertificateID";

			if (SearchText != "")
			{
				SQL += " AND (LIB_SurveyCategories.Survey_Category like @SearchText or tblSurv_Lib.Survey_Cert_Name like @SearchText)";
			}

			if (ExpiryInDays != 0 || ShowAll == true) //0 = Custom Dates
			{


				SQL += @" UNION
					SELECT
						tblSurvVessel_3Details.OfficeID, 
						tblSurvVessel_3Details.DateOfIssue, 
						tblSurvVessel_3Details.DateOfExpiry, 
						tblSurvVessel_3Details.Remarks, 
						tblSurvVessel_3Details.Verified_By, 
						tblSurvVessel_3Details.Verified_Date, 
						tblSurvVessel_3Details.NAExpiryDate, 
						tblSurvVessel_3Details.FollowupReminderDt, 
						tblSurvVessel_3Details.FollowupReminder,
						tblSurvVessel_2Lib.Vessel_ID, 
						tblSurvVessel_2Lib.Surv_Vessel_ID, 
						tblSurvVessel_2Lib.Surv_ID, 
						tblSurvVessel_2Lib.EquipmentType, 
						0 AS Surv_Details_ID, 
						LIB_SurveyCategories.Survey_Category, 
						tblSurv_Lib.Survey_Cert_Name, 
						tblSurv_Lib.Survey_Cert_remarks, 
						tblSurv_Lib.Term, 
						tblSurv_Lib.Survey_Category_ID, 
						tblSurv_Lib.GraceRange,
						LIB_VESSELS.Vessel_Name,
						0 AS AttachmentCount,
						0 AS verified,
						LIB_VESSELS.FleetCode, tblSurv_Lib.Sort_Order,
						LIB_SurveyMainCategories.Survey_Category AS Survey_MainCategory,
						tblSurvVessel_3Details.ExtensionDate
					FROM         tblSurvVessel_2Lib INNER JOIN
					  tblSurv_Lib ON tblSurvVessel_2Lib.Surv_ID = tblSurv_Lib.Surv_ID INNER JOIN
					  LIB_SurveyCategories ON tblSurv_Lib.Survey_Category_ID = LIB_SurveyCategories.ID INNER JOIN
					  LIB_SurveyCategories  AS LIB_SurveyMainCategories ON LIB_SurveyCategories.MainCategoryId = LIB_SurveyMainCategories.ID INNER JOIN
					  LIB_VESSELS ON tblSurvVessel_2Lib.Vessel_ID = LIB_VESSELS.Vessel_ID LEFT OUTER JOIN
					  tblSurvVessel_3Details ON tblSurvVessel_2Lib.Vessel_ID = tblSurvVessel_3Details.Vessel_ID AND 
					  tblSurvVessel_2Lib.Surv_Vessel_ID = tblSurvVessel_3Details.Surv_Vessel_ID
					WHERE     (tblSurvVessel_2Lib.ACTIVE = 1) 
						AND (tblSurv_Lib.Active = 1) 
						AND LIB_VESSELS.HANDOVER_DATE is null
						AND (tblSurvVessel_2Lib.Surv_Vessel_ID NOT IN
												(SELECT     Surv_Vessel_ID
												FROM          tblSurvVessel_3Details AS tblSurvVessel_3Details_1
												WHERE      (Vessel_ID = tblSurvVessel_2Lib.Vessel_ID)))";


				if (VesselID > 0)
					SQL += " and tblSurvVessel_2Lib.Vessel_ID  = @VesselID";
				else if (FleetID > 0)
					SQL += " and LIB_VESSELS.fleetcode  = @FleetID";

				if (CategoryID > 0)
					SQL += " and tblSurv_Lib.Survey_Category_ID  = @CategoryID";

				if (CatIDList != "")
					SQL += " AND tblSurv_Lib.Survey_Category_ID IN (" + CatIDList + ")";

				if ((CategoryID == 0 && CatIDList == "") && @MainCategoryId > 0)
					SQL += " and LIB_SurveyCategories.MainCategoryId  = @MainCategoryId";

				if (CertificateID > 0)
					SQL += " and tblSurvVessel_2Lib.Surv_ID  = @CertificateID";

				if (SearchText != "")
				{
					SQL += " AND (LIB_SurveyCategories.Survey_Category like @SearchText or tblSurv_Lib.Survey_Cert_Name like @SearchText)";
				}

			}

			if (SelectRecordCount == 1)
				SQL_RecCount = " SELECT COUNT(0) FROM ( " + SQL + ") B";

			SQL = " SELECT * from (SELECT *, CASE WHEN isnull(@PAGE_INDEX,0)> 0  THEN ROW_NUMBER() OVER(ORDER BY Vessel_Name, Sort_Order ) ELSE 0 END AS ROWNUM FROM (" + SQL + ") MainList) A WHERE A.ROWNUM between (( ISNULL(@PAGE_INDEX,-1) - 1) * ISNULL(@PAGE_SIZE,1) + 1) AND (ISNULL(@PAGE_INDEX,-1) * ISNULL(@PAGE_SIZE,-1)) ";


			SqlParameter[] sqlprm = new SqlParameter[]
			{ 
				new SqlParameter("@FleetID", FleetID),
				new SqlParameter("@VesselID", VesselID),
				new SqlParameter("@MainCategoryId", MainCategoryId),
				new SqlParameter("@CategoryID", CategoryID),
				new SqlParameter("@CertificateID", CertificateID),
				new SqlParameter("@IssueFrom", IssueFrom),
				new SqlParameter("@IssueTo", IssueTo),
				new SqlParameter("@ExpFrom", ExpFrom),
				new SqlParameter("@ExpTo", ExpTo),
				new SqlParameter("@Verified", Verified),
				new SqlParameter("@SearchText", "%"+SearchText+"%"),
				new SqlParameter("@PAGE_SIZE",SqlDbType.Int),
				new SqlParameter("@PAGE_INDEX",SqlDbType.Int)
				
			};

			sqlprm[sqlprm.Length - 1].Value = PAGE_INDEX;
			sqlprm[sqlprm.Length - 2].Value = PAGE_SIZE;

			return SqlHelper.ExecuteDataset(connection, CommandType.Text, SQL + SQL_RecCount, sqlprm);
		}
		*/
		public DataTable Get_AssignedSurvayList_DL(int FleetID, int VesselID, int MainCatID, int CatID)
		{
			SqlParameter[] sqlprm = new SqlParameter[]
			{ 
				new SqlParameter("@FleetID", FleetID),
				new SqlParameter("@VesselID", VesselID),
				new SqlParameter("@MainCatID", MainCatID),
				new SqlParameter("@CatID", CatID)
			};
			return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SURV_Sp_Get_AssignedSurvayList", sqlprm).Tables[0];
		}

		public DataTable Get_NASurvayList_DL(int FleetID, int VesselID, int CatID, int CertificateID, int? Verified)
		{
			SqlParameter[] sqlprm = new SqlParameter[]
			{ 
				new SqlParameter("@FleetID", FleetID),
				new SqlParameter("@VesselID", VesselID),
				new SqlParameter("@CatID", CatID),
				new SqlParameter("@CertificateID", CertificateID),
				new SqlParameter("@Verified", Verified)
			};
			return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SURV_Sp_Get_NASurvayList", sqlprm).Tables[0];
		}

		public DataTable Get_NASurvayList_DL(int FleetID, int VesselID, DataTable CatIDList, int CertificateID, int? Verified)
		{
			SqlParameter[] sqlprm = new SqlParameter[]
			{ 
				new SqlParameter("@FleetID", FleetID),
				new SqlParameter("@VesselID", VesselID),
				new SqlParameter("@CatIDList", CatIDList),
				new SqlParameter("@CertificateID", CertificateID),
				new SqlParameter("@Verified", Verified)
			};
			return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SURV_Sp_Get_NASurvayList_MultiCat", sqlprm).Tables[0];
		}

		public DataTable Get_SurvayDetailList_DL(int Vessel_ID, int Surv_Vessel_ID)
		{
			SqlParameter[] sqlprm = new SqlParameter[]
			{ 
				new SqlParameter("@Vessel_ID", Vessel_ID),
				new SqlParameter("@Surv_Vessel_ID", Surv_Vessel_ID)
			};

			return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SURV_Sp_Get_SurveyDetailList", sqlprm).Tables[0];
		}

		public DataTable Get_SurvayDetails_DL(int Vessel_ID, int Surv_Vessel_ID, int Surv_Details_ID, int OfficeID)
		{
			SqlParameter[] sqlprm = new SqlParameter[]
			{ 
				new SqlParameter("@Vessel_ID", Vessel_ID),
				new SqlParameter("@Surv_Vessel_ID", Surv_Vessel_ID),
				new SqlParameter("@Surv_Details_ID", Surv_Details_ID),
				new SqlParameter("@OfficeID", OfficeID)
			};

			return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SURV_Sp_Get_SurvayDetails", sqlprm).Tables[0];
		}

		public DataSet Get_NewSurvayDetails_DL(int Vessel_ID, int Surv_Vessel_ID)
		{
			SqlParameter[] sqlprm = new SqlParameter[]
			{ 
				new SqlParameter("@Vessel_ID", Vessel_ID),
				new SqlParameter("@Surv_Vessel_ID", Surv_Vessel_ID)
			};

			return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SURV_Sp_Get_NewSurvayDetails", sqlprm);
		}

		public DataTable Get_SurvayAttachments_DL(int Vessel_ID, int Surv_Details_ID)
		{
			SqlParameter[] sqlprm = new SqlParameter[]
			{ 
				new SqlParameter("@Vessel_ID", Vessel_ID),
				new SqlParameter("@Surv_Details_ID", Surv_Details_ID)
			};

			return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SURV_Sp_Get_SurvayAttachments", sqlprm).Tables[0];
		}

		public DataTable Get_SurvayAttachments_DL(int Vessel_ID, int Surv_Details_ID, ref int Allowed_Size)
		{
			SqlParameter[] sqlprm = new SqlParameter[]
			{ 
				new SqlParameter("@Vessel_ID", Vessel_ID),
				new SqlParameter("@Surv_Details_ID", Surv_Details_ID),
				new SqlParameter("return",SqlDbType.Int)
			};
			sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;

			DataTable dt = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SURV_Sp_Get_SurvayAttachments", sqlprm).Tables[0];
			Allowed_Size = int.Parse(sqlprm[sqlprm.Length - 1].Value.ToString());
			return dt;
		}

		public DataTable Get_SurvayFollowups_DL(int Vessel_ID, int Surv_Details_ID, int OfficeID)
		{
			SqlParameter[] sqlprm = new SqlParameter[]
			{ 
				new SqlParameter("@Vessel_ID", Vessel_ID),
				new SqlParameter("@Surv_Details_ID", Surv_Details_ID),
				new SqlParameter("@OfficeID", OfficeID)
			};

			return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SURV_Sp_Get_SurvayFollowUps", sqlprm).Tables[0];
		}

		public int INSERT_New_Followup_DL(int Vessel_ID, int Surv_Details_ID, int OfficeID, string FollowUpText, int CreatedBy)
		{
			SqlParameter[] sqlprm = new SqlParameter[]
			{ 
				new SqlParameter("@Vessel_ID", Vessel_ID),
				new SqlParameter("@Surv_Details_ID", Surv_Details_ID),
				new SqlParameter("@OfficeID", OfficeID),
				new SqlParameter("@FollowUpText", FollowUpText),
				new SqlParameter("@CreatedBy", CreatedBy),
				new SqlParameter("return",SqlDbType.Int)
			};
			sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
			SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SURV_Sp_INSERT_New_Followup", sqlprm);
			return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
		}

		public int INSERT_New_Attachment_DL(int Vessel_ID, int Surv_Details_ID, string AttachmentName, string AttachmentPath, long Size_Bytes, int CreatedBy, int MaxSizeFileSize,int DocTypeId,string IssueDate,string Remarks)
		{
			SqlParameter[] sqlprm = new SqlParameter[]
			{ 
				new SqlParameter("@Vessel_ID", Vessel_ID),
				new SqlParameter("@Surv_Details_ID", Surv_Details_ID),
				new SqlParameter("@AttachmentName", AttachmentName),
				 new SqlParameter("@AttachmentPath", AttachmentPath),
				new SqlParameter("@Size_Bytes", Size_Bytes),
				new SqlParameter("@CreatedBy", CreatedBy),
				new SqlParameter("@MaxSizeFileSize", MaxSizeFileSize),
				new SqlParameter("@DocTypeId", DocTypeId),
				new SqlParameter("@DateOfIssue", IssueDate),
				new SqlParameter("@Remarks", Remarks),
				new SqlParameter("return",SqlDbType.Int)
			};
			sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
			SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SURV_Sp_INSERT_New_Attachment", sqlprm);
			return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
		}

		public int INSERT_SurveyDetails_DL(int Vessel_ID, int Surv_Vessel_ID, DateTime DateOfIssue, DateTime DateOfExpiry, string Remarks, DateTime FollowupReminderDt, string FollowupReminder, int Created_By, int NoExpiry,
			int GraceRange, DateTime ExtensionDate,int CertificateNo,int IssuingAuthorityID)
		{
			SqlParameter[] sqlprm = new SqlParameter[]
			{ 
				new SqlParameter("@Vessel_ID", Vessel_ID),
				new SqlParameter("@Surv_Vessel_ID", Surv_Vessel_ID),
				new SqlParameter("@DateOfIssue", DateOfIssue),

				new SqlParameter("@DateOfExpiry", DateOfExpiry),
				new SqlParameter("@Remarks", Remarks),
				new SqlParameter("@FollowupReminderDt", FollowupReminderDt),
				new SqlParameter("@FollowupReminder", FollowupReminder),
				new SqlParameter("@Created_By", Created_By),
				new SqlParameter("@NoExpiry", NoExpiry),
				new SqlParameter("@GraceRange", GraceRange),
				new SqlParameter("@ExtensionDate", ExtensionDate),
				new SqlParameter("@Certificate_No", CertificateNo),
				new SqlParameter("@IssuingAuthority_ID", IssuingAuthorityID),
				new SqlParameter("return",SqlDbType.Int)
			};

			sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
			SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SURV_Sp_INSERT_SurveyDetails", sqlprm);
			return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
		}

		public int UPDATE_SurveyDetails_DL(int Vessel_ID, int Surv_Vessel_ID, int Surv_Details_ID, int Office_ID, DateTime DateOfIssue, DateTime DateOfExpiry, string Remarks, DateTime FollowupReminderDt, string FollowupReminder,
			int Modified_By, int NoExpiry, int GraceRange, DateTime ExtensionDate, int CertificateNo, int IssuingAuthorityID)
		{
			SqlParameter[] sqlprm = new SqlParameter[]
			{ 
				new SqlParameter("@Vessel_ID", Vessel_ID),
				new SqlParameter("@Surv_Details_ID", Surv_Details_ID),
				new SqlParameter("@Surv_Vessel_ID", Surv_Vessel_ID),
				new SqlParameter("@Office_ID", Office_ID),                
				new SqlParameter("@DateOfIssue", DateOfIssue),
				new SqlParameter("@DateOfExpiry", DateOfExpiry),
				new SqlParameter("@Remarks", Remarks),
				new SqlParameter("@FollowupReminderDt", FollowupReminderDt),
				new SqlParameter("@FollowupReminder", FollowupReminder),
				new SqlParameter("@Modified_By", Modified_By),
				new SqlParameter("@NoExpiry", NoExpiry),
				new SqlParameter("@GraceRange", GraceRange),
				new SqlParameter("@ExtensionDate", ExtensionDate),
				 new SqlParameter("@Certificate_No", CertificateNo),
				new SqlParameter("@IssuingAuthority_ID", IssuingAuthorityID),
				new SqlParameter("return",SqlDbType.Int)
			};

			sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
			SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SURV_Sp_UPDATE_SurveyDetails", sqlprm);
			return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
		}

		public int Verify_Survey_DL(int Vessel_ID, int Surv_Vessel_ID, int Surv_Details_ID, int Office_ID, int VerificationStatus, int Verified_By)
		{
			SqlParameter[] sqlprm = new SqlParameter[]
			{ 
				new SqlParameter("@Vessel_ID", Vessel_ID),
				new SqlParameter("@Surv_Details_ID", Surv_Details_ID),
				new SqlParameter("@Surv_Vessel_ID", Surv_Vessel_ID),
				new SqlParameter("@Office_ID", Office_ID),                
				new SqlParameter("@VerificationStatus", VerificationStatus),                
				new SqlParameter("@Verified_By", Verified_By),                
				new SqlParameter("return",SqlDbType.Int)
			};

			sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
			SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SURV_Sp_Verify_Survey", sqlprm);
			return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
		}

		public int UPDATE_SurveyStatus_DL(int Vessel_ID, int Surv_Vessel_ID, int SurveyStatus, int Modified_By)
		{
			SqlParameter[] sqlprm = new SqlParameter[]
			{ 
				new SqlParameter("@Vessel_ID", Vessel_ID),
				new SqlParameter("@Surv_Vessel_ID", Surv_Vessel_ID),
				new SqlParameter("@SurveyStatus", SurveyStatus),
				new SqlParameter("@Modified_By", Modified_By),
				new SqlParameter("return",SqlDbType.Int)
			};

			sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
			SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SURV_Sp_UPDATE_SurveyStatus", sqlprm);
			return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
		}

		public int Verify_NAMarked_Survey_DL(int Vessel_ID, int Surv_Vessel_ID, int VerifyStatus, int Modified_By)
		{
			SqlParameter[] sqlprm = new SqlParameter[]
			{ 
				new SqlParameter("@Vessel_ID", Vessel_ID),
				new SqlParameter("@Surv_Vessel_ID", Surv_Vessel_ID),
				new SqlParameter("@VerifyStatus", VerifyStatus),
				new SqlParameter("@Modified_By", Modified_By),
				new SqlParameter("return",SqlDbType.Int)
			};

			sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
			SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SURV_Sp_Verify_NAMarked_Survey", sqlprm);
			return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
		}

		public int INSERT_Survey_Category_DL(int MainCategoryId, string CategoryName, int CreatedBy)
		{
			SqlParameter[] sqlprm = new SqlParameter[]
			{ 
				new SqlParameter("@MainCategoryId", MainCategoryId),
				new SqlParameter("@CategoryName", CategoryName),
				new SqlParameter("@CreatedBy", CreatedBy),
				new SqlParameter("return",SqlDbType.Int)
			};
			sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
			SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SURV_Sp_INSERT_Survey_Category", sqlprm);
			return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
		}

		public int INSERT_Survey_Certificate_DL(string CertificateName, int CategoryID, int CreatedBy, int Term, string Survey_Cert_remarks, int? Alert_Insurance, int? GraceRange)
		{
			SqlParameter[] sqlprm = new SqlParameter[]
			{ 
				new SqlParameter("@CertificateName", CertificateName),
				new SqlParameter("@CategoryID", CategoryID),
				new SqlParameter("@Term", Term),
				new SqlParameter("@Survey_Cert_remarks", Survey_Cert_remarks),
				new SqlParameter("@CreatedBy", CreatedBy),
				new SqlParameter("@Alert_Insurance", Alert_Insurance),
				new SqlParameter("@GraceRange", GraceRange),
				new SqlParameter("return",SqlDbType.Int)
			};
			sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
			SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SURV_Sp_INSERT_Survey_Certificate", sqlprm);
			return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
		}

		public int UPDATE_Survey_Category_DL(int MainCategoryId, int CategoryID, string CategoryName, int ModifiedBy)
		{
			SqlParameter[] sqlprm = new SqlParameter[]
			{ 
				new SqlParameter("@MainCategoryId", MainCategoryId),
				new SqlParameter("@CategoryID", CategoryID),
				new SqlParameter("@CategoryName", CategoryName),
				new SqlParameter("@ModifiedBy", ModifiedBy),
				new SqlParameter("return",SqlDbType.Int)
			};
			sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
			SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SURV_Sp_UPDATE_Survey_Category", sqlprm);
			return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
		}

		public int UPDATE_Survey_Certificate_DL(int CertificateID, string CertificateName, int CategoryID, int ModifiedBy, int Term, string Survey_Cert_remarks, int? Alert_Insurance, int? GraceRange)
		{
			SqlParameter[] sqlprm = new SqlParameter[]
			{ 
				new SqlParameter("@CertificateID", CertificateID),
				new SqlParameter("@CertificateName", CertificateName),
				new SqlParameter("@CategoryID", CategoryID),
				new SqlParameter("@Term", Term),
				new SqlParameter("@Survey_Cert_remarks", Survey_Cert_remarks),
				new SqlParameter("@ModifiedBy", ModifiedBy),
				new SqlParameter("@Alert_Insurance", Alert_Insurance),
				 new SqlParameter("@GraceRange", GraceRange),
				new SqlParameter("return",SqlDbType.Int)
			};
			sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
			SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SURV_Sp_UPDATE_Survey_Certificate", sqlprm);
			return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
		}

		public int DELETE_Survey_Category_DL(int CategoryID, int DeletedBy)
		{
			SqlParameter[] sqlprm = new SqlParameter[]
			{ 
				new SqlParameter("@CategoryID", CategoryID),
				new SqlParameter("@DeletedBy", DeletedBy),
				new SqlParameter("return",SqlDbType.Int)
			};
			sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
			SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SURV_Sp_DELETE_Survey_Category", sqlprm);
			return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
		}

		public int DELETE_Survey_Certificate_DL(int CertificateID, int DeletedBy)
		{
			SqlParameter[] sqlprm = new SqlParameter[]
			{ 
				new SqlParameter("@CertificateID", CertificateID),
				new SqlParameter("@DeletedBy", DeletedBy),
				new SqlParameter("return",SqlDbType.Int)
			};
			sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
			SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SURV_Sp_DELETE_Survey_Certificate", sqlprm);
			return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
		}

		public int Assign_SurveyToVessel_DL(int VesselID, int Surv_ID, string EquipmentType, string IssuingAuth, int CreatedBy)
		{
			SqlParameter[] sqlprm = new SqlParameter[]
			{ 
				new SqlParameter("@VesselID", VesselID),
				new SqlParameter("@Surv_ID", Surv_ID),
				new SqlParameter("@EquipmentType ", EquipmentType ),
				new SqlParameter("@IssuingAuth", IssuingAuth),
				new SqlParameter("@CreatedBy", CreatedBy),
				new SqlParameter("return",SqlDbType.Int)
			};
			sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
			SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SURV_Sp_Assign_SurveyToVessel", sqlprm);
			return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
		}

		public int UPDATE_Survey_CertificateRemarks_DL(int VesselID, int Surv_Vessel_ID, int Surv_Detail_ID, int ModifiedBy, string Survey_Cert_remarks, int? Term, string MakeModel, string IssuingAuthority)
		{
			SqlParameter[] sqlprm = new SqlParameter[]
			{ 
				new SqlParameter("@VesselID", VesselID),
				new SqlParameter("@Surv_Vessel_ID", Surv_Vessel_ID),
				new SqlParameter("@Surv_Detail_ID", Surv_Detail_ID),
				new SqlParameter("@Survey_Cert_remarks", Survey_Cert_remarks),
				new SqlParameter("@Term", Term),
				new SqlParameter("@MakeModel", MakeModel),
				new SqlParameter("@IssuingAuthority", IssuingAuthority),
				new SqlParameter("@ModifiedBy", ModifiedBy),
				new SqlParameter("return",SqlDbType.Int)
			};
			sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
			SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SURV_Sp_UPDATE_Survey_CertificateRemarks", sqlprm);
			return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
		}

		public DataTable Get_Survey_Certificate_Search(string searchtext, int? MainCategoryID, int? SurvCategoryID, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
		{

			System.Data.DataTable dt = new System.Data.DataTable();
			System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
			{ 
				   
				   new System.Data.SqlClient.SqlParameter("@SerchText", searchtext),
				   new System.Data.SqlClient.SqlParameter("@SurvMainCategoryID", MainCategoryID),
				   new System.Data.SqlClient.SqlParameter("@SurvCategoryID", SurvCategoryID),
				   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
				   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
				   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
				   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
				   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
					
			};
			obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
			System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SURV_Sp_Get_Survey_List_Search", obj);
			isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
			return ds.Tables[0];
		}

		public DataTable Get_Survey_Certificate_List_By_SurvID_DL(int? Surv_Certificate_ID)
		{
			System.Data.DataTable dt = new System.Data.DataTable();

			System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
			{ 
				   new System.Data.SqlClient.SqlParameter("@Surv_Certificate_ID", Surv_Certificate_ID)
			};

			return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SURV_Sp_Get_Survey_List_By_Surv_ID", obj).Tables[0];

		}
		public DataTable Get_Survey_CategoryList_ByMainCategoryId_DL(int MainCategoryId)
		{
			SqlParameter[] sqlprm = new SqlParameter[]
			{ 
				new SqlParameter("@MainCategoryId", MainCategoryId)
			};
			return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SURV_Get_SurveyCategoryList", sqlprm).Tables[0];
		}
		public DataTable Get_Survey_CategoryList_DL(int? Surv_Cetegory_ID)
		{
			SqlParameter[] sqlprm = new SqlParameter[]
			{ 
				new SqlParameter("@Surv_Cetegory_ID", Surv_Cetegory_ID)
			};
			return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SURV_Sp_Get_SurveyCategoryList_By_CategoryID", sqlprm).Tables[0];
		}

		public DataTable Get_Survey_Category_Search(string searchtext, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
		{

			System.Data.DataTable dt = new System.Data.DataTable();
			System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
			{ 
				   
				   new System.Data.SqlClient.SqlParameter("@SearchText", searchtext),
				  
				   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
				   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
				   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
				   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
				   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
					
			};
			obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
			System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SURV_Sp_Get_Survey_Category_List_Search", obj);
			isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
			return ds.Tables[0];
		}
		public int INSERT_Document_Type_DL( int ID,string DocumentType, int CreatedBy)
		{
			SqlParameter[] sqlprm = new SqlParameter[]
			{                 
				new SqlParameter("@ID" ,ID),                
				new SqlParameter("@DocumentType", DocumentType),
				new SqlParameter("@CreatedBy", CreatedBy),                                
				new SqlParameter("return",SqlDbType.Int)
			};
			sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
			SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SURV_INS_LIB_SurveyDocumentType", sqlprm);
			return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
		}

		public int UPDATE_Document_Type_DL(int ID, string DocumentType, int CreatedBy)
		{
			SqlParameter[] sqlprm = new SqlParameter[]
			{                 
				new SqlParameter("@ID" ,ID),                
				new SqlParameter("@DocumentType", DocumentType),
				new SqlParameter("@CreatedBy", CreatedBy),                                
				new SqlParameter("return",SqlDbType.Int)
			};
			sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
			SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SURV_UPD_LIB_SurveyDocumentType", sqlprm);
			return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
		}

		public int DELETE_SDocument_Type_DL(int ID, int CreatedBy)
		{
			SqlParameter[] sqlprm = new SqlParameter[]
			{                 
				new SqlParameter("@ID" ,ID),                                
				new SqlParameter("@CreatedBy", CreatedBy),                                
				new SqlParameter("return",SqlDbType.Int)
			};
			sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
			SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SURV_DEL_LIB_SurveyDocumentType", sqlprm);
			return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
		}                

		public DataTable Get_Survey_Document_Type_Search(string searchtext, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
		{

			System.Data.DataTable dt = new System.Data.DataTable();
			System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
			{ 
				   
				   new System.Data.SqlClient.SqlParameter("@SearchText", searchtext),
				  
				   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
				   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
				   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
				   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
				   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
					
			};
			obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
			System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SURV_Get_Survey_Document_Type_List_Search", obj);
			isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
			return ds.Tables[0];
		}

		public DataTable Get_Survey_Document_Type_DL(int? ID)
		{
			SqlParameter[] sqlprm = new SqlParameter[]
			{ 
				new SqlParameter("@ID", ID)
			};
			return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SURV_GET_LIB_SurveyDocumentType", sqlprm).Tables[0];
		}

		public DataTable Get_Survey_Document_Type_List_DL()
		{
			return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SURV_Get_DocumentTypeList").Tables[0];
		}

		public DataTable Get_Survay_CertificateAuthorityList_DL(string SearchText)
		{
			SqlParameter[] sqlprm = new SqlParameter[]
			{ 
				new SqlParameter("@SearchText", SearchText)
			};
			return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SURV_Get_Certificate_AuthorityList", sqlprm).Tables[0];
		}

		public int INSERT_Survey_CertificateAuthority_DL(string Authority, int CreatedBy)
		{
			SqlParameter[] sqlprm = new SqlParameter[]
			{                
				new SqlParameter("@Authority", Authority),
				new SqlParameter("@CreatedBy", CreatedBy),
				new SqlParameter("return",SqlDbType.Int)
			};
			sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
			SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SURV_INSERT_Certificate_Authority", sqlprm);
			return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
		}
		public int UPDATE_Survey_CertificateAuthority_DL(int ID, string Authority, int ModifiedBy)
		{
			SqlParameter[] sqlprm = new SqlParameter[]
			{ 
			   new SqlParameter("@ID", ID), 
				new SqlParameter("@Authority", Authority),               
				new SqlParameter("@ModifiedBy", ModifiedBy),
				new SqlParameter("return",SqlDbType.Int)
			};
			sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
			SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SURV_UPDATE_Certificate_Authority", sqlprm);
			return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
		}

		public int DELETE_Survey_CertificateAuthority_DL(int ID, int DeletedBy)
		{
			SqlParameter[] sqlprm = new SqlParameter[]
			{ 
				new SqlParameter("@ID", ID),
				new SqlParameter("@DeletedBy", DeletedBy),
				new SqlParameter("return",SqlDbType.Int)
			};
			sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
			SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SURV_DELETE_Certificate_Authority", sqlprm);
			return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
		}
		public DataTable Get_Authorit_DL()
		{
			return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SURV_Get_CertificateAuthority").Tables[0];
		}

		public DataTable Get_AuthoritBySurv_Details_ID_DL(int survDetailsID, int survVesselId)
		{
			 SqlParameter[] sqlprm = new SqlParameter[]
			{ 
			 new SqlParameter("@SurvDetailsID", survDetailsID),
			 new SqlParameter("@Surv_Vessel_ID", survVesselId),
			 };
			 return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SURV_Get_CertificateAuthorityID", sqlprm).Tables[0];
		}
	}
}
