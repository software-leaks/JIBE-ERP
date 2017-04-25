using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.IO;
using System.Configuration;
using System.Data.SqlClient;

using System.Data;
using System.Web.Configuration;
using System.Security.Cryptography;

using SMS.Data;
using SMS.Business.Crew;
using SMS.Business.Infrastructure;

/// <summary>
/// Summary description for UploadCrewDocuments
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
//[System.Web.Script.Services.ScriptService]
public class UploadCrewDocuments : System.Web.Services.WebService {
    private string UploadPath;
    private string connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
    
    public UploadCrewDocuments()
    {
        try
        {
            string uploadConfigSetting = ConfigurationManager.AppSettings["UploadPath"].ToString();
            uploadConfigSetting += "\\CrewDocuments";

            if (Path.IsPathRooted(uploadConfigSetting))
                UploadPath = uploadConfigSetting;
            else
                UploadPath = Server.MapPath(uploadConfigSetting);
            if (!Directory.Exists(UploadPath))
                CustomSoapException("Upload Folder not found", "The folder " + UploadPath + " does not exist");
        }
        catch {
            CustomSoapException("Upload Folder not found", "The folder " + UploadPath + " does not exist");
        }
    }

    
    [WebMethod]
    public string Ping()
    {
        return "OK";
    }

    [WebMethod]
    public long GetMaxRequestLength()
    {
        try
        {
            return (ConfigurationManager.GetSection("system.web/httpRuntime") as HttpRuntimeSection).MaxRequestLength;
        }
        catch (Exception ex)
        {
            CustomSoapException(ex.GetType().Name, ex.Message);
            return 4096;
        }
    }

    #region Upload
    /// <summary>
    /// Append a chunk of bytes to a file.
    /// The client should ensure that all messages are sent in sequence. 
    /// This method always overwrites any existing file with the same name
    /// </summary>
    /// <param name="FileName">The name of the file that this chunk belongs to, e.g. Vista.ISO</param>
    /// <param name="buffer">The byte array, i.e. the chunk being transferred</param>
    /// <param name="Offset">The offset at which to write the buffer to</param>
    [WebMethod]
    public void AppendChunk(string FileName, byte[] buffer, long Offset)
    {
        string FilePath = Path.Combine(UploadPath, FileName);

        if (Offset == 0)	// new file, create an empty file
            File.Create(FilePath).Close();

        // open a file stream and write the buffer.  Don't open with FileMode.Append because the transfer may wish to start a different point
        using (FileStream fs = new FileStream(FilePath, FileMode.Open, FileAccess.ReadWrite, FileShare.Read))
        {
            fs.Seek(Offset, SeekOrigin.Begin);
            fs.Write(buffer, 0, buffer.Length);
        }
    }
    #endregion

    #region download

    /// <summary>
    /// Download a chunk of a file from the Upload folder on the server. 
    /// </summary>
    /// <param name="FileName">The FileName to download</param>
    /// <param name="Offset">The offset at which to fetch the next chunk</param>
    /// <param name="BufferSize">The size of the chunk</param>
    /// <returns>The chunk as a byte[]</returns>
    [WebMethod]
    public byte[] DownloadChunk(string FileName, long Offset, int BufferSize)
    {
        string FilePath = Path.Combine(UploadPath, FileName);

        // check that requested file exists
        if (!File.Exists(FilePath))
            CustomSoapException("File not found", String.Format("The file {0} does not exist", FilePath));

        long FileSize = new FileInfo(FilePath).Length;

        // if the requested Offset is larger than the file, quit.
        if (Offset > FileSize)
            CustomSoapException("Invalid Download Offset", String.Format("The file size is {0}, received request for offset {1}", FileSize, Offset));

        // open the file to return the requested chunk as a byte[]
        byte[] TmpBuffer;
        int BytesRead;

        try
        {
            using (FileStream fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                fs.Seek(Offset, SeekOrigin.Begin);	// this is relevent during a retry. otherwise, it just seeks to the start
                TmpBuffer = new byte[BufferSize];
                BytesRead = fs.Read(TmpBuffer, 0, BufferSize);	// read the first chunk in the buffer (which is re-used for every chunk)
            }
            if (BytesRead != BufferSize)
            {
                // the last chunk will almost certainly not fill the buffer, so it must be trimmed before returning
                byte[] TrimmedBuffer = new byte[BytesRead];
                Array.Copy(TmpBuffer, TrimmedBuffer, BytesRead);
                return TrimmedBuffer;
            }
            else
                return TmpBuffer;
        }
        catch (Exception ex)
        {
            CustomSoapException("Error reading file", ex.Message);
            return null;
        }
    }

    /// <summary>
    /// Get the number of bytes in a file in the Upload folder on the server.
    /// The client needs to know this to know when to stop downloading
    /// </summary>
    [WebMethod]
    public long GetFileSize(string FileName)
    {

        string FilePath = UploadPath + "\\" + FileName;

        // check that requested file exists
        if (!File.Exists(FilePath))
            CustomSoapException("File not found", "The file " + FilePath + " does not exist");

        return new FileInfo(FilePath).Length;
    }

    /// <summary>
    /// Return a list of filenames from the Upload folder on the server
    /// </summary>
    [WebMethod]
    public List<string> GetFilesList()
    {

        List<string> files = new List<string>();
        foreach (string s in Directory.GetFiles(UploadPath))
            files.Add(Path.GetFileName(s));
        return files;
    }
    #endregion

    #region file hashing
    [WebMethod]
    public string CheckFileHash(string FileName)
    {

        string FilePath = UploadPath + "\\" + FileName;
        MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
        byte[] hash;
        using (FileStream fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read, FileShare.Read, 4096))
            hash = md5.ComputeHash(fs);
        return BitConverter.ToString(hash);
    }
    #endregion

    #region Exception Handling
    /// <summary>
    /// Throws a soap exception.  It is formatted in a way that is more readable to the client, after being put through the xml serialisation process
    /// Typed exceptions don't work well across web services, so these exceptions are sent in such a way that the client
    /// can determine the 'name' or type of the exception thrown, and any message that went with it, appended after a : character.
    /// </summary>
    /// <param name="exceptionName"></param>
    /// <param name="message"></param>
    public static void CustomSoapException(string exceptionName, string message)
    {
        throw new System.Web.Services.Protocols.SoapException(exceptionName + ": " + message, new System.Xml.XmlQualifiedName("BufferedUpload"));
    }

    #endregion

    #region Crew Module releted
    [WebMethod]
    public DataTable GetFleetList()
    {
        BLL_Infra_VesselLib objBLL = new BLL_Infra_VesselLib();
        return objBLL.GetFleetList(1);
    }

    [WebMethod]
    public DataTable Get_VesselList()
    {
        BLL_Infra_VesselLib objBLL = new BLL_Infra_VesselLib();
        return objBLL.Get_VesselList(0,0,1,"",1);
        //return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_INF_Get_VesselList").Tables[0];
    }


    [WebMethod]
    public DataTable Get_VesselListByFleet(int FleetID)
    {
        BLL_Infra_VesselLib objBLL = new BLL_Infra_VesselLib();
        return objBLL.Get_VesselList(FleetID, 0, 1, "", 1);
    }

    [WebMethod]
    public DataTable Get_StatusList()
    {
        DataTable dt = new DataTable("Status");
        dt.Columns.Add("StatusID", typeof(string));
        dt.Columns.Add("Status", typeof(string));

        DataRow dr1 = dt.NewRow();
        dr1[0] = "DECISION PENDING";
        dr1[1] = "Decision Pending";
        dt.Rows.Add(dr1);

        DataRow dr2 = dt.NewRow();
        dr2[0] = "NO VOYAGE";
        dr2[1] = "Approved(NO VOY)";
        dt.Rows.Add(dr2);


        DataRow dr3 = dt.NewRow();
        dr3[0] = "ASSIGNED";
        dr3[1] = "Vessel Assigned";
        dt.Rows.Add(dr3);


        DataRow dr4 = dt.NewRow();
        dr4[0] = "PLANNED";
        dr4[1] = "Event Planned";
        dt.Rows.Add(dr4);

        DataRow dr5 = dt.NewRow();
        dr5[0] = "CURRENT";
        dr5[1] = "Current";
        dt.Rows.Add(dr5);

        DataRow dr6 = dt.NewRow();
        dr6[0] = "SIGNED OFF";
        dr6[1] = "Signed OFF";
        dt.Rows.Add(dr6);

        DataRow dr7 = dt.NewRow();
        dr7[0] = "NTBR";
        dr7[1] = "NTBR";
        dt.Rows.Add(dr7);

        DataRow dr8 = dt.NewRow();
        dr8[0] = "INACTIVE";
        dr8[1] = "Inactive";
        dt.Rows.Add(dr8);
        return dt;
    }


    [WebMethod]
    public DataTable Get_RankList()
    {
        return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_CRW_Get_RankList").Tables[0];
    }

    //[WebMethod]
    //public DataTable Get_Crewlist_Index1(string CREW_STATUS, int VESSEL_ID, string SearchString)
    //{
    //    try
    //    {
    //        BLL_Crew_CrewDetails objCrew = new BLL_Crew_CrewDetails();

    //        DataTable dt = objCrew.Get_Crewlist_Index(0, SearchString, SearchString, CREW_STATUS, "", "", 0, VESSEL_ID, 0, 0, 0, 0);

    //        return dt;
    //    }
    //    catch
    //    {
    //        throw;
    //    }
    //}

    [WebMethod]
    public DataTable Get_Crewlist_Index(string CREW_STATUS, int VESSEL_ID, string SearchString)
    {
        BLL_Crew_CrewDetails objBLL = new BLL_Crew_CrewDetails();
        return objBLL.Get_Crewlist_ForAddIn(CREW_STATUS, VESSEL_ID, SearchString);
    }

//    [WebMethod]
//    public DataTable Get_Crewlist_Index(string CREW_STATUS, int VESSEL_ID, string SearchString, int Fleet_ID)
//    {
//        try
//        {
//            DateTime Dt_FromJoningDate = DateTime.Today.AddYears(-10);
//            DateTime Dt_ToJoningDate = DateTime.Today;

//            //string UserType = GetCurrentUserType(iUserID);

//            string SQL = "";

//            SQL = @"SELECT     CD.ID, CD.Staff_Code as 'Staff Code', CD.Staff_Name + ' ' + isnull(CD.Staff_Surname,'') as Name,                              
//                              CASE WHEN rank.Rank_Short_Name IS NULL THEN CRW_LIB_Crew_Ranks.Rank_Short_Name ELSE rank.Rank_Short_Name END AS Rank, 
//                                LIB_Country.Country_Name as Nationality, 
//                            CASE 
//			                    WHEN CD.Status_Code IS NOT NULL  THEN CD.Status_Code 
//			                    WHEN CD.ACTIVE_STATUS = 0 THEN 'INACTIVE' 
//			                    WHEN V.Sign_On_Date IS NOT NULL AND V.SIGN_OFF_DATE IS NULL AND (V.EST_SING_OFF_DATE BETWEEN GETDATE() AND DATEADD(DD, 30, GETDATE()) OR V.EST_SING_OFF_DATE < GETDATE()) THEN 'COC DUE' 
//			                    WHEN V.Sign_On_Date IS NOT NULL AND V.SIGN_OFF_DATE IS NOT NULL AND EV.CrewID IS NULL THEN 'SIGNED OFF' 
//			                    WHEN V.Sign_On_Date IS NOT NULL AND V.SIGN_OFF_DATE IS NULL THEN 'CURRENT' 
//			                    WHEN EV.CrewID IS NOT NULL AND ev.Event_Date IS NOT NULL THEN 'PLANNED' 
//			                    WHEN UnA.CrewID_UnAssigned IS NOT NULL AND ev.Event_Date IS NULL THEN 'ASSIGNED' 
//			                    WHEN isnull(CD.ManningOfficeStatus, 0) = 0 OR isnull(CD.CrewManagerApproval, 0) = 0 THEN 'PENDING' 
//                                WHEN CD.CrewManagerApproval = -1 THEN 'REJECTED' 
//			                    WHEN ((V.Sign_On_Date IS NULL AND V.SIGN_OFF_DATE IS NULL) AND (CD.MANNINGOFFICESTATUS = 1 AND CD.CREWMANAGERAPPROVAL = 1) AND (EV.CrewID IS NULL AND ev.Event_Date IS NULL)) THEN 'NO VOYAGE' END AS CrewStatus
//
//                FROM         (SELECT     VoyageID, SUM(Amount) AS Amount
//                                   FROM          ACC_DTL_JOINING_EARN_DEDUCTION
//                                   WHERE      (Active_Status = 1)
//                                   GROUP BY VoyageID) AS salary RIGHT OUTER JOIN
//                                  CRW_LIB_Crew_Ranks AS rank RIGHT OUTER JOIN
//                                  LIB_VESSELS INNER JOIN
//                                      (SELECT     V1.ID, V1.CrewID, V1.Joining_Date,v1.Sign_On_Date, V1.Joining_Rank, V1.Vessel_ID, V1.Est_Sing_Off_Date, V1.Staff_Code, V1.Sign_Off_Date, V1.Voyage_Remarks, 
//                                                               V1.COC_Modified
//                                        FROM          CRW_Dtl_Crew_Voyages AS V1 INNER JOIN
//                                                                   (SELECT     CrewID, MAX(Sign_On_Date) AS Sign_On_Date
//                                                                     FROM          CRW_Dtl_Crew_Voyages
//                                                                     WHERE      (Active_Status = 1)
//                                                                     GROUP BY CrewID) AS V ON V1.CrewID = V.CrewID AND V1.Sign_On_Date = V.Sign_On_Date
//                                        WHERE      (V1.Active_Status = 1) AND (V1.Sign_On_Date IS NOT NULL)) AS V ON LIB_VESSELS.Vessel_ID = V.Vessel_ID LEFT OUTER JOIN
//                                  CRW_DTL_StaffRemarks ON V.COC_Modified = CRW_DTL_StaffRemarks.ID ON rank.ID = V.Joining_Rank ON salary.VoyageID = V.ID RIGHT OUTER JOIN
//                                  CRW_LIB_Crew_Details AS CD LEFT OUTER JOIN
//                                      (SELECT     PKID, CrewID_SigningOff, CrewID_UnAssigned
//                                        FROM          CRW_DTL_CrewAssignment AS CRW_DTL_CrewAssignment_1) AS SOff INNER JOIN
//                                  CRW_LIB_Crew_Details AS OnSigner ON SOff.CrewID_UnAssigned = OnSigner.ID ON CD.ID = SOff.CrewID_SigningOff LEFT OUTER JOIN
//                                      (SELECT     PKID, CrewID_UnAssigned, CrewID_SigningOff
//                                        FROM          CRW_DTL_CrewAssignment) AS UnA INNER JOIN
//                                  CRW_LIB_Crew_Details AS OffSigner ON UnA.CrewID_SigningOff = OffSigner.ID ON CD.ID = UnA.CrewID_UnAssigned LEFT OUTER JOIN
//                                      (SELECT     CRW_DTL_CrewChangeEvent_Members.CrewID, MAX(CRW_DTL_CrewChangeEvent.Event_Date) AS Event_Date
//                                        FROM          CRW_DTL_CrewChangeEvent INNER JOIN
//                                                               CRW_DTL_CrewChangeEvent_Members ON CRW_DTL_CrewChangeEvent.PKID = CRW_DTL_CrewChangeEvent_Members.EventID
//                                        WHERE      (CRW_DTL_CrewChangeEvent.Active_Status = 1) AND (CRW_DTL_CrewChangeEvent_Members.Active_Status = 1) AND 
//                                                               (CRW_DTL_CrewChangeEvent.Event_Status = 1) AND (CRW_DTL_CrewChangeEvent.Event_Date >= GETDATE())
//                                        GROUP BY CRW_DTL_CrewChangeEvent_Members.CrewID) AS EV ON CD.ID = EV.CrewID LEFT OUTER JOIN
//                                  LIB_COMPANY ON CD.ManningOfficeID = LIB_COMPANY.Id LEFT OUTER JOIN
//                                  CRW_LIB_Crew_Ranks ON CD.Rank_Applied = CRW_LIB_Crew_Ranks.ID LEFT OUTER JOIN
//                                  LIB_Country ON CD.Staff_Nationality = LIB_Country.ID ON V.CrewID = CD.ID
//                    WHERE (CD.ID > 0)";

//            //if (CrewID > 0)
//            //    SQL += " AND CD.ID = @CrewID";

//            if (CREW_STATUS != "INACTIVE")
//                SQL += " AND (CD.Active_Status = 1) ";

//            if (SearchString != "")
//                SQL += " AND (UPPER(CD.Staff_Name) LIKE @SearchString OR UPPER(CD.Staff_Midname) LIKE @SearchString or UPPER(CD.Staff_Surname) LIKE  @SearchString or STAFF_CODE like  @SearchString or LIB_Country.Country_Name like @SearchString) ";



//            switch (CREW_STATUS)
//            {
//                case "CURRENT":
//                    SQL += " AND (V.SIGN_ON_DATE IS NOT NULL AND  v.SIGN_OFF_DATE IS NULL) ";

//                    if (VESSEL_ID != 0)
//                    {
//                        SQL += " AND v.Vessel_ID = @Vessel_ID";
//                    }
//                    if (Fleet_ID != 0)
//                    {
//                        SQL += " AND LIB_VESSELS.FleetCode = @Fleet_ID";
//                    }
//                    break;
//                case "SIGNED OFF":
//                    SQL += " AND (V.Sign_On_Date IS NOT NULL AND V.SIGN_OFF_DATE IS NOT NULL)";
//                    break;

//                case "NO VOYAGE":
//                    SQL += " AND ((V.Sign_On_Date IS  NULL AND  v.SIGN_OFF_DATE IS NULL) and (CD.MANNINGOFFICESTATUS = 1 AND CD.CrewManagerApproval = 1) AND (SOff.CrewID_UnAssigned IS  NULL AND ev.Event_Date IS  NULL))";
//                    break;

//                case "INACTIVE":
//                    SQL += " AND (CD.ACTIVE_STATUS = 0)";
//                    break;

//                case "ASSIGNED":
//                    SQL += " AND (SOff.CrewID_UnAssigned IS NOT NULL)";
//                    break;

//                case "PLANNED":
//                    SQL += " AND (EV.Event_Date is not null)";
//                    break;

//                case "DECISION PENDING":
//                    SQL += " AND (isnull(CD.ManningOfficeStatus, 0)= 0 OR isnull(CD.CrewManagerApproval,0) = 0 )";
//                    break;

//                case "NTBR":
//                    SQL += " AND (CD.Status_Code = 'NTBR')";
//                    break;

//                case "MEDICALLY UNFIT":
//                    SQL += " AND (CD.Status_Code = 'MEDICALLY UNFIT')";
//                    break;

//                case "REJECTED":
//                    SQL += " AND (CD.CrewManagerApproval = -1)";
//                    break;
//            }

//            SQL += " ORDER BY LIB_VESSELS.VESSEL_SHORT_NAME,CRW_LIB_Crew_Ranks.Rank_Sort_Order";

//            SqlParameter[] sqlprm = new SqlParameter[]
//                                        { 
//                                            new SqlParameter("@Vessel_ID",VESSEL_ID),
//                                            new SqlParameter("@Fleet_ID",Fleet_ID),
//                                            new SqlParameter("@SearchString","%" + SearchString.ToUpper() + "%"),
//                                            new SqlParameter("@JoinFrmDT",Dt_FromJoningDate),
//                                            new SqlParameter("@JoinTODT",Dt_ToJoningDate),
//                                        };

//            return ExecuteQuery(SQL, sqlprm);


//        }
//        catch
//        {
//            throw;
//        }
//    }


    private string GetCurrentUserType(int iUserID)
    {
        return Get_UserData_DL(iUserID, "USER_TYPE");
    }

    private string Get_UserData_DL(int UserID, string DataColumn)
    {
        string SQL = "";

        SQL = "SELECT " + DataColumn + " FROM LIB_USER WHERE USERID=@UserID";

        SqlParameter[] obj = new SqlParameter[]{ 
                                                      new SqlParameter("@UserID",UserID)
                                                  };

        return Convert.ToString(SqlHelper.ExecuteScalar(connection, CommandType.Text, SQL, obj));
    }

    [WebMethod]
    public DataTable AuthenticateUser(string UserName, string Password)
    {
        BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
        DataSet ds = objUser.Get_UserCredentials(UserName,DMS.DES_Encrypt_Decrypt.Encrypt(Password));

        DataTable UserTable = new DataTable("UserTable");
        UserTable.Columns.Add(new DataColumn("UserId"));
        UserTable.Columns.Add(new DataColumn("User_Name"));
        UserTable.Columns.Add(new DataColumn("User_Type"));
        UserTable.Columns.Add(new DataColumn("User_FullName"));
        UserTable.Columns.Add(new DataColumn("Company_Name"));
        UserTable.Columns.Add(new DataColumn("COMPANY_ID"));

        if (ds.Tables["Login"].Rows.Count > 0)
        {
            DataRow row = UserTable.NewRow();

            row["UserId"] = ds.Tables["Login"].Rows[0]["UserId"].ToString();
            row["User_Name"] = ds.Tables["Login"].Rows[0]["User_Name"].ToString();
            row["User_Type"] = ds.Tables["Login"].Rows[0]["User_Type"].ToString();
            row["User_FullName"] = ds.Tables["Login"].Rows[0]["User_FullName"].ToString();
            row["Company_Name"] = ds.Tables["Login"].Rows[0]["Company_Name"].ToString();
            row["COMPANY_ID"] = ds.Tables["Login"].Rows[0]["COMPANY_ID"].ToString();

            UserTable.Rows.Add(row);
        }
        else
        {
        }
        return UserTable;

    }


    private DataTable ExecuteQuery(string SQLCommandText, SqlParameter[] sqlprm)
    {
        return SqlHelper.ExecuteDataset(connection, CommandType.Text, SQLCommandText, sqlprm).Tables[0];
    }

    [WebMethod]
    public DataTable Get_DocTypeList()
    {
        return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_DMS_Get_DocTypeList").Tables[0];
    }

    [WebMethod]
    public DataTable Get_AssignedAttributesToTypeID(int DocTypeID)
    {
        SqlParameter sqlprm = new SqlParameter("@DocTypeID", DocTypeID);
        return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_DMS_Get_AssignedAttributesToTypeID", sqlprm).Tables[0];
    }

    [WebMethod]
    public void SaveCrewDocument(int CrewID, int RankID, int iDocID, int iTypeID, string DocumentName, string FileType, string DocFileName, string FilePath, string DocNo, string IssuePlace, string IssueDate, string ExpiryDate, int CreatedBy)
    {
        try
        {
            //UPDATE_CrewDocument(CrewID, iDocID, iTypeID, sDocName, DocNo, IssueDate, IssuePlace, ExpiryDate, CreatedBy);
            //UPDATE_DocumentChecklist(CrewID, iTypeID, 1, RankID, "", DocNo, IssueDate, IssuePlace, ExpiryDate, CreatedBy, FilePath);

            DateTime Dt_IssueDate = DateTime.Parse("1900/01/01");
            if (IssueDate != "")
                Dt_IssueDate = DateTime.Parse(IssueDate);

            DateTime Dt_ExpiryDate = DateTime.Parse("1900/01/01");
            if (ExpiryDate != "")
                Dt_ExpiryDate = DateTime.Parse(ExpiryDate);

            string DocFileExt = Path.GetExtension(DocFileName);
            int DocID = INS_CrewDocuments_DL(CrewID, iTypeID, DocumentName, DocFileName, DocFileExt, CreatedBy, RankID, DocNo, Dt_IssueDate, IssuePlace, Dt_ExpiryDate);

            UPDATE_DocumentChecklist(CrewID, iTypeID, 1, RankID, "", DocNo, IssueDate, IssuePlace, ExpiryDate, CreatedBy, FilePath);

        }
        catch
        {
        }
    }

    [WebMethod]
    public void SaveCrewFeedback(int CrewID, string FeedBack, string AttachmentName, int CreatedBy)
    {
        try
        {
            BLL_Crew_CrewDetails objBLLCrew = new BLL_Crew_CrewDetails();
            objBLLCrew.INS_CrewRemarks(CrewID, FeedBack, AttachmentName, CreatedBy);
        }
        catch
        {
        }
    }

    public int INS_CrewDocuments_DL(int CrewID, int DocTypeID, string DocumentName, string DocFileName, string DocFileExt, int Created_By, int RankID, string DocNo, DateTime IssueDate, string IssuePalce, DateTime ExpiryDate)
    {
        SqlParameter[] sqlprm = new SqlParameter[]
            {   
                new SqlParameter("@CrewID",CrewID),
                new SqlParameter("@DocumentName",DocumentName),
                new SqlParameter("@DocFileName",DocFileName),
                new SqlParameter("@DocFileExt",DocFileExt),
                new SqlParameter("@DocTypeID",DocTypeID),
                new SqlParameter("@Created_By",Created_By),
                new SqlParameter("@RankID",RankID),
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

    public int UPDATE_DocumentChecklist(int CrewID, int DocTypeID, int AnswerYN, int RankID, string Remark, string DocNo, string IssueDate, string IssuePlace, string ExpiryDate, int Modified_By, string FilePath)
    {
        try
        {
            if (IssueDate != null && ExpiryDate != null)
            {
                DateTime Dt_IssueDate = DateTime.Parse("01/01/1900");
                if (IssueDate.Length > 0)
                    Dt_IssueDate = DateTime.Parse(IssueDate);

                DateTime Dt_ExpiryDate = DateTime.Parse("01/01/1900");
                if (ExpiryDate.Length > 0)
                    Dt_ExpiryDate = DateTime.Parse(ExpiryDate);

                UPDATE_DocumentChecklist_DL(CrewID, DocTypeID, AnswerYN, RankID, Remark, DocNo, Dt_IssueDate, IssuePlace, Dt_ExpiryDate, Modified_By);

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
    
    public int UPDATE_DocumentChecklist_DL(int CrewID, int DocTypeID, int AnswerYN, int RankID, string Remarks, string DocNo, DateTime IssueDate, string IssuePlace, DateTime ExpiryDate, int Modified_By)
    {
        SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewID",CrewID),
                                            new SqlParameter("@DocTypeID",DocTypeID),
                                            new SqlParameter("@AnswerYN",AnswerYN),
                                            new SqlParameter("@RankID",RankID),
                                            new SqlParameter("@Remarks",Remarks),
                                            new SqlParameter("@DocNo",DocNo),
                                            new SqlParameter("@IssueDate",IssueDate),
                                            new SqlParameter("@IssuePlace",IssuePlace),
                                            new SqlParameter("@ExpiryDate",ExpiryDate),
                                            new SqlParameter("@Modified_By",Modified_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
        sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
        SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_CRW_Update_DocumentChecklist", sqlprm);
        return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
    }

    //public int UPDATE_CrewDocument(int CrewID, int DocID, int DocTypeID, string DocumentName, string DocNo, string IssueDate, string IssuePalce, string ExpiryDate, int Modified_By)
    //{
    //    try
    //    {
    //        DateTime Dt_IssueDate = DateTime.Parse("1900/01/01");
    //        if (IssueDate != "")
    //            Dt_IssueDate = DateTime.Parse(IssueDate);

    //        DateTime Dt_ExpiryDate = DateTime.Parse("1900/01/01");
    //        if (ExpiryDate != "")
    //            Dt_ExpiryDate = DateTime.Parse(ExpiryDate);

    //        return UPDATE_CrewDocument_DL(CrewID, DocID, DocTypeID, DocumentName, DocNo, Dt_IssueDate, IssuePalce, Dt_ExpiryDate, Modified_By);
    //    }
    //    catch
    //    {
    //        throw;
    //    }
    //}
    //public int UPDATE_CrewDocument_DL(int CrewID, int DocID, int DocTypeID, string DocumentName, string DocNo, DateTime IssueDate, string IssuePalce, DateTime ExpiryDate, int Modified_By)
    //{
    //    SqlParameter[] sqlprm = new SqlParameter[]
    //                                    { 
    //                                        new SqlParameter("@DocID",DocID),
    //                                        new SqlParameter("@DocTypeID",DocTypeID),
    //                                        new SqlParameter("@DocName",DocumentName),                                            
    //                                        new SqlParameter("@DocNo",DocNo),
    //                                        new SqlParameter("@IssueDate",IssueDate),
    //                                        new SqlParameter("@IssuePalce",IssuePalce),
    //                                        new SqlParameter("@ExpiryDate",ExpiryDate),
    //                                        new SqlParameter("@Modified_By",Modified_By),                                            
    //                                        new SqlParameter("return",SqlDbType.Int)
    //                                    };
    //    sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
    //    SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_DMS_Update_Crew_Document", sqlprm);
    //    return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
    //}


    #endregion


    #region Crew Card releted

    [WebMethod]
    public DataTable Get_CardStatusList()
    {
        DataTable dt = new DataTable("Status");
        dt.Columns.Add("StatusID", typeof(string));
        dt.Columns.Add("Status", typeof(string));

        DataRow dr1 = dt.NewRow();
        dr1[0] = "0";
        dr1[1] = "Proposed";
        dt.Rows.Add(dr1);

        DataRow dr2 = dt.NewRow();
        dr2[0] = "1";
        dr2[1] = "Approved";
        dt.Rows.Add(dr2);


        DataRow dr3 = dt.NewRow();
        dr3[0] = "-1";
        dr3[1] = "Rejected";
        dt.Rows.Add(dr3);

        return dt;
    }

    [WebMethod]
    public DataTable Get_CrewCardList(int Fleet_Code, int Vessel_ID, int Approval_Status, string SearchString, int UserID)
    {
        BLL_Crew_CrewDetails objBLL = new BLL_Crew_CrewDetails();
        return objBLL.Get_CrewCardIndex(Fleet_Code, Vessel_ID, 0, Approval_Status, SearchString, UserID);
    }

    [WebMethod]
    public DataTable Get_AttachmentTypeList()
    {
        DataTable dt = new DataTable("Status");
        dt.Columns.Add("AttachmentType", typeof(string));
        dt.Columns.Add("AttachmentTypeName", typeof(string));

        dt.Rows.Add("0","- SELECT TYPE -");
        dt.Rows.Add("1","Letter of Warning");
        dt.Rows.Add("2","LogBook Entry");
        dt.Rows.Add("3", "Proposer Attachment");
        dt.Rows.Add("4","Approver Attachment");

        return dt;
    }


    [WebMethod]
    public void SaveCrewCard_Attachment(int CardID, int AttachmentType, string AttachmentName, string AttachmentPath, int Created_By)
    {
        try
        {
            BLL_Crew_CrewDetails objBLLCrew = new BLL_Crew_CrewDetails();
            objBLLCrew.INS_CrewCard_Attachment(CardID, AttachmentType, AttachmentName, AttachmentPath, Created_By);
        }
        catch
        {
        }
    }

    #endregion

}
