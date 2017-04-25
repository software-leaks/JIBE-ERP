using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Security;
using System.Security.Cryptography;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.Services;
using System.Web.Services.Protocols;

using System.Data;
using System.Data.SqlClient;
using System.Text;
using SMS.Data;
using SMS.Business.Infrastructure;
using SMS.Business.TRAV;
using SMS.Business.Crew;
using SMS.Business.PURC;


/// <summary>
/// A set of methods to upload and download chunks of a file using MTOM
/// </summary>
[WebService(Namespace = "http://www.codeproject.com/soap/MTOMWebServices.asp")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class MTOM : System.Web.Services.WebService
{
    private string UploadPath;
    private string Connection_Req = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;

    public MTOM()
    {
        // check that the upload folder exists
        string uploadConfigSetting = ConfigurationManager.AppSettings["UploadPath"].ToString();
        uploadConfigSetting += "\\Purchase";
        if (Path.IsPathRooted(uploadConfigSetting))
            UploadPath = uploadConfigSetting;
        else
            UploadPath = Server.MapPath(uploadConfigSetting);
        if (!Directory.Exists(UploadPath))
            CustomSoapException("Upload Folder not found", "The folder " + UploadPath + " does not exist");
    }


    /// <summary>
    /// A dummy method to check the connection to the web service
    /// </summary>
    [WebMethod]
    public void Ping()
    {
    }

    /// <summary>
    /// The winforms client needs to know what is the max size of chunk that the server 
    /// will accept.  this is defined by MaxRequestLength, which can be overridden in
    /// web.config.
    /// </summary>
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

    #region Purchanse Module releted

    [WebMethod]
    public DataTable getRequisitionList(string ReqID, string dept, string vessel_code, string ReqType)
    {
        string reqid_ = "";
        string dept_ = "";
        string vessel_code_ = "";
        string ReqType_ = "";
        try
        {
            if (dept == "0")
                dept_ = "";
            else
                dept_ = dept;

            if (vessel_code == "0")
                vessel_code_ = "";
            else
                vessel_code_ = vessel_code;

            if (ReqType == "0")
                ReqType_ = "";
            else
                ReqType_ = ReqType;

            reqid_ = ReqID;

            DataTable dtReqList = getReqlist(reqid_, dept_, vessel_code_, ReqType_);
            return dtReqList;
        }
        catch (Exception ex)
        {
            CustomSoapException(ex.GetType().Name, ex.Message);
            return null;
        }
    }

    private DataTable getReqlist(string ReqID, string DeptName, string VesselID, string ReqType)
    {
        StringBuilder QueryText = new StringBuilder();
        QueryText.Append(@"
                         SELECT     Lib_Vessels.Vessel_Short_Name as Vessel, PURC_Lib_Departments.Dept_Name as Department, PURC_Dtl_Reqsn.REQUISITION_CODE as 'Requisition Code', 
                                      PURC_Dtl_Reqsn.ORDER_CODE as 'Order Code' 
                FROM         PURC_Dtl_Reqsn INNER JOIN
                                      Lib_Vessels ON PURC_Dtl_Reqsn.Vessel_Code = Lib_Vessels.Vessel_ID LEFT OUTER JOIN
                                      Lib_Suppliers ON PURC_Dtl_Reqsn.QUOTATION_SUPPLIER = Lib_Suppliers.SUPPLIER LEFT OUTER JOIN
                                      PURC_Lib_Departments ON PURC_Dtl_Reqsn.DEPARTMENT = PURC_Lib_Departments.Dept_Short_Code
        ");

        string ConditionText = "";
        string finalQueryText = "";
        if (VesselID != "")
            ConditionText += "PURC_Dtl_Reqsn.Vessel_Code='" + VesselID + "'";



        if (DeptName != "")
        {
            if (ConditionText == "")
                ConditionText += " PURC_Dtl_Reqsn.DEPARTMENT='" + DeptName + "'";
            else
                ConditionText += " and PURC_Dtl_Reqsn.DEPARTMENT='" + DeptName + "'";
        }

        if (ReqID != "")
        {
            if (ConditionText == "")
                ConditionText += " PURC_Dtl_Reqsn.REQUISITION_CODE Like '%" + ReqID + "%' or PURC_Dtl_Reqsn.QUOTATION_CODE Like '%" + ReqID + "%' or PURC_Dtl_Reqsn.INVOICE_NO Like '%" + ReqID + "%' or PURC_Dtl_Reqsn.ORDER_CODE Like '%" + ReqID + "%' or PURC_Dtl_Reqsn.DELIVERY_CODE Like '%" + ReqID + "%'";
            else
                ConditionText += " and PURC_Dtl_Reqsn.REQUISITION_CODE Like '%" + ReqID + "%' or PURC_Dtl_Reqsn.QUOTATION_CODE Like '%" + ReqID + "%' or PURC_Dtl_Reqsn.INVOICE_NO Like '%" + ReqID + "%' or PURC_Dtl_Reqsn.ORDER_CODE Like '%" + ReqID + "%' or PURC_Dtl_Reqsn.DELIVERY_CODE Like '%" + ReqID + "%'";

        }

        string LineTypes = "'R','Q','O','D'";
        LineTypes = LineTypes.Replace(ReqType, "0");

        if (ReqType != "")
        {
            if (ConditionText == "")
                ConditionText += " PURC_Dtl_Reqsn.LINE_TYPE='" + ReqType + "' and PURC_Dtl_Reqsn.LINE_TYPE not in (" + LineTypes + ")";
            else
                ConditionText += " and PURC_Dtl_Reqsn.LINE_TYPE='" + ReqType + "' and PURC_Dtl_Reqsn.LINE_TYPE not in (" + LineTypes + ")";
        }

        if (ConditionText != "")
            finalQueryText = QueryText.ToString() + " where PURC_Dtl_Reqsn.active_status=1 and " + ConditionText;
        else
            finalQueryText = QueryText.ToString();

        return SqlHelper.ExecuteDataset(Connection_Req, CommandType.Text, finalQueryText).Tables[0];

    }

    [WebMethod]
    public DataTable getVesselList()
    {
        BLL_Infra_VesselLib objVsl = new BLL_Infra_VesselLib();

        return objVsl.Get_VesselList(0,0,0,"",1);
    }

    [WebMethod]
    public DataTable getDeptList()
    {
        StringBuilder QueryText = new StringBuilder();
        QueryText.Append(@"SELECT        distinct PURC_LIB_DEPARTMENTS.Dept_Name, PURC_DTL_REQSN.DEPARTMENT
            FROM            PURC_DTL_REQSN INNER JOIN
                                     PURC_LIB_DEPARTMENTS ON PURC_DTL_REQSN.DEPARTMENT = PURC_LIB_DEPARTMENTS.Dept_Short_Code
            ORDER BY PURC_LIB_DEPARTMENTS.Dept_Name
                            ");
        return SqlHelper.ExecuteDataset(Connection_Req, CommandType.Text, QueryText.ToString()).Tables[0];
    }

    [WebMethod]
    public DataTable getTypeList()
    {
        StringBuilder QueryText = new StringBuilder();
        QueryText.Append(@"
                             SELECT  distinct  PURC_Dtl_Reqsn.LINE_TYPE, CASE PURC_Dtl_Reqsn.LINE_TYPE WHEN 'R' THEN 'Requisition' WHEN 'O' THEN 'Order' WHEN 'Q' THEN 'Quotation' WHEN 'D' THEN 'Deliver' END
                       AS LineTypeText
                    FROM         PURC_Dtl_Reqsn order by  PURC_Dtl_Reqsn.LINE_TYPE desc");
        return SqlHelper.ExecuteDataset(Connection_Req, CommandType.Text, QueryText.ToString()).Tables[0];
    }

    [WebMethod]
    public DataTable getCateList()
    {
        //DataTable dt = new DataTable("Category");
        //dt.Columns.Add("Cat_ID", typeof(string));
        //dt.Columns.Add("Categery", typeof(string));

        //DataRow dr1 = dt.NewRow();
        //dr1[0] = "Delivery Update";
        //dr1[1] = "Delivery Update";
        //dt.Rows.Add(dr1);

        //DataRow dr2 = dt.NewRow();
        //dr2[0] = "Non-Budgeted approval from owners";
        //dr2[1] = "Non-Budgeted approval from owners";
        //dt.Rows.Add(dr2);


        //DataRow dr3 = dt.NewRow();
        //dr3[0] = "Supplier Communication";
        //dr3[1] = "Supplier Communication";
        //dt.Rows.Add(dr3);


        //DataRow dr4 = dt.NewRow();
        //dr4[0] = "Specifications/Drawings";
        //dr4[1] = "Specifications/Drawings";
        //dt.Rows.Add(dr4);

        //DataRow dr5 = dt.NewRow();
        //dr5[0] = "Others";
        //dr5[1] = "Others";
        //dt.Rows.Add(dr5);

        ////DataSet dsCatlist = new DataSet();
        ////dsCatlist.Tables.Add(dt);
        //return dt;


        string SQL = "SELECT code as Cat_ID,short_code,description as Category  from PURC_LIB_SYSTEM_PARAMETERS where parent_type='263' order by Category";
        return SqlHelper.ExecuteDataset(Connection_Req, CommandType.Text, SQL).Tables[0];

    }

    [WebMethod]
    public DataTable getSupplierList()
    {
        return SqlHelper.ExecuteDataset(Connection_Req, CommandType.Text, "SELECT    SUPPLIER, SHORT_NAME FROM   Lib_Suppliers WHERE     (Active_Status = 1) order by SHORT_NAME asc").Tables[0];
    }

    [WebMethod]
    public DataTable getSupplierListForRequisitions(string Requisition_Code_List)
    {
        string SQL = @"

SELECT DISTINCT 
                      PURC_DTL_REQSN.REQUISITION_CODE, PURC_DTL_QUOTED_PRICES.SUPPLIER_CODE,Lib_Ports.PORT_ID,
                        Lib_Ports.PORT_NAME as 'Port', Lib_Suppliers.Full_NAME AS 'Supplier Name'                      
FROM         PURC_DTL_QUOTED_PRICES INNER JOIN
                      PURC_DTL_REQSN ON PURC_DTL_QUOTED_PRICES.DOCUMENT_CODE = PURC_DTL_REQSN.DOCUMENT_CODE INNER JOIN
                      Lib_Suppliers ON PURC_DTL_QUOTED_PRICES.SUPPLIER_CODE = Lib_Suppliers.SUPPLIER INNER JOIN
                      Lib_Ports ON PURC_DTL_REQSN.DELIVERY_PORT = Lib_Ports.PORT_ID
                      WHERE PURC_DTL_REQSN.REQUISITION_CODE in (" + Requisition_Code_List + @")
                union  
		
                SELECT DISTINCT 
                                      PURC_DTL_REQSN.REQUISITION_CODE, 0 as SUPPLIER_CODE,0 as PORT_ID,
                                        '' as 'Port', '- FOR INTERNAL VIEW -' AS 'Supplier Name'                      
                FROM         PURC_DTL_QUOTED_PRICES INNER JOIN
                                      PURC_DTL_REQSN ON PURC_DTL_QUOTED_PRICES.DOCUMENT_CODE = PURC_DTL_REQSN.DOCUMENT_CODE 
                                       WHERE PURC_DTL_REQSN.REQUISITION_CODE in (" + Requisition_Code_List + ")";

        //SqlParameter sqlprm = new SqlParameter("@Requisition_Code", Requisition_Code_List);

        return SqlHelper.ExecuteDataset(Connection_Req, CommandType.Text, SQL).Tables[0];
    }

    //    [WebMethod]
    //    public DataTable getSupplierListForRequisitions(string Requisition_Code_List, string SearchString)
    //    {
    //        string SQL = @"SELECT     PURC_DTL_REQSN.DOCUMENT_CODE, PURC_DTL_QUOTED_PRICES.SUPPLIER_CODE, Lib_Suppliers.Full_NAME
    //            FROM         PURC_DTL_QUOTED_PRICES INNER JOIN
    //                      PURC_DTL_REQSN ON PURC_DTL_QUOTED_PRICES.DOCUMENT_CODE = PURC_DTL_REQSN.DOCUMENT_CODE INNER JOIN
    //                      Lib_Suppliers ON PURC_DTL_QUOTED_PRICES.SUPPLIER_CODE = Lib_Suppliers.SUPPLIER
    //            where PURC_DTL_REQSN.REQUISITION_CODE in (@Requisition_Code)  
    //            and (Lib_Suppliers.Full_NAME like @SearchString or Lib_Suppliers.Full_NAME like @SearchString or Lib_Suppliers.COUNTRY like @SearchString )";


    //        SqlParameter[] sqlprm = new SqlParameter[]
    //                                        { 
    //                                            new SqlParameter("@Requisition_Code",Requisition_Code_List),
    //                                            new SqlParameter("@SearchString",SearchString)
    //                                        };
    //        return SqlHelper.ExecuteDataset(Connection_Req, CommandType.Text, SQL, sqlprm).Tables[0];
    //    }

    [WebMethod]
    public DataTable getSupplierlstAtSearch(string SearchString)
    {
        string SQL = @"SELECT     SUPPLIER, FULL_NAME 
                        FROM         LIB_SUPPLIERS 
                        WHERE  active_status =1 
                        and ( upper(SUPPLIER) LIKE @SearchString   
	                        OR upper(FULL_NAME) LIKE  @SearchString 
	                        OR upper(CITY) LIKE  @SearchString 
	                        OR upper(COUNTRY) LIKE @SearchString) 
                        ORDER BY FULL_NAME ";
        SqlParameter sqlprm = new SqlParameter("@SearchString", "%" + SearchString.ToUpper() + "%");
        return SqlHelper.ExecuteDataset(Connection_Req, CommandType.Text, SQL, sqlprm).Tables[0];
    }


    //    public void insertRecIntoAtt(string reqID, int VesselID, string SupplierCode, string FileType, string fileName, string filePath, int userID)
    //    {

    //        string SQL = @"Declare @ID int set @ID=(select isnull(max(isnull(ID,0)),0)+1 from PURC_DTL_File_Attach;
    //            INSERT INTO PURC_DTL_File_Attach
    //                       (ID,
    //                        Office_ID
    //                       ,Vessel_Code
    //                       ,Requisition_Code
    //                       ,Supplier_Code
    //                       ,File_Type
    //                       ,File_Name
    //                       ,File_Path
    //                       ,Created_By
    //                       ,Date_Of_Creatation
    //                       ,Active_Status)
    //                 VALUES
    //                       (
    //                       @ID,
    //                       @officeID,
    //                       @Vessel_Code,
    //                       '@Requisition_Code',
    //                       '@Supplier_Code',
    //                       '@File_Type',
    //                       '@File_Name', 
    //                       '@File_Path',
    //                       @Created_By, 
    //                       @Date_Of_Creatation,
    //                       @Active_Status)
    //                    ";

    //        SqlParameter[] sqlprm = new SqlParameter[]
    //                                        { 
    //                                            new SqlParameter("@officeID",1),
    //                                            new SqlParameter("@Vessel_Code",VesselID),
    //                                            new SqlParameter("@Requisition_Code",reqID),
    //                                            new SqlParameter("@Supplier_Code",SupplierCode),
    //                                            new SqlParameter("@File_Type",FileType),
    //                                            new SqlParameter("@File_Name",fileName),
    //                                            new SqlParameter("@File_Path","OLATT/" + fileName),
    //                                            new SqlParameter("@Created_By",userID),
    //                                            new SqlParameter("@Date_Of_Creatation",DateTime.Now),
    //                                            new SqlParameter("@Active_Status",1)
    //                                        };

    //        SqlHelper.ExecuteNonQuery(Connection_Req, CommandType.Text, SQL, sqlprm);
    //    }


    [WebMethod]
    public int SaveAttachedFileInfo(string VesselCode, string ReqCode, string suppCode, string FileType, string FileName, string FilePath, int PortID, int CreatedBy, int CategoryID)
    {
        try
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[]
             {
             
             new System.Data.SqlClient.SqlParameter("@VesselCode", VesselCode), 
             new System.Data.SqlClient.SqlParameter("@ReqsCode",ReqCode),
             new System.Data.SqlClient.SqlParameter("@SuppCode", suppCode), 
             new System.Data.SqlClient.SqlParameter("@FileType",FileType),
             new System.Data.SqlClient.SqlParameter("@FileName", FileName), 
             new System.Data.SqlClient.SqlParameter("@FilePath",FilePath) ,
             new System.Data.SqlClient.SqlParameter("@CreatedBy",CreatedBy.ToString()),
             new System.Data.SqlClient.SqlParameter("@PortID",PortID) ,
             new System.Data.SqlClient.SqlParameter("@CategoryID",CategoryID) ,
             new SqlParameter("@return",SqlDbType.Int)
           
             };

            obj[obj.Length - 1].Direction = ParameterDirection.ReturnValue;
            int RetVal = SqlHelper.ExecuteNonQuery(Connection_Req, CommandType.StoredProcedure, "PURC_SP_Ins_File_Attachment_info", obj);
            if (RetVal == 1)
            {
                RetVal = int.Parse(obj[obj.Length - 1].Value.ToString());
            }
            return RetVal;
        }
        catch (Exception ex)
        {
            throw ex;
        }


    }
    #endregion

    #region Logistic Module

    [WebMethod]
    public DataTable get_LogisticPOList_OutLook(int Vessel_ID,string Supplier_Code, string SearchText)
    {
        return BLL_PURC_LOG.Get_LogisticPOList_OutLook(Vessel_ID, Supplier_Code, SearchText);
    }

    [WebMethod]
    public DataTable get_LogisticPO_Supplier_OutLook()
    {
        return BLL_PURC_LOG.Get_LogisticPO_Supplier();
    }

    #endregion

    //    #region Crew Module releted
    //    [WebMethod]
    //    public DataTable GetFleetList()
    //    {
    //        return SqlHelper.ExecuteDataset(Connection_Req, CommandType.Text, "SELECT NAME,CODE FROM DBO.LIB_VESSELLIB_SYSTEMS_PARAMETERS WHERE PARENT_CODE = 1").Tables[0];
    //    }

    //    [WebMethod]
    //    public DataTable Get_VesselList()
    //    {
    //        return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_INF_Get_VesselList").Tables[0];
    //    }

    //    [WebMethod]
    //    public DataTable Get_StatusList()
    //    {
    //        DataTable dt = new DataTable("Status");
    //        dt.Columns.Add("StatusID", typeof(string));
    //        dt.Columns.Add("Status", typeof(string));

    //        DataRow dr1 = dt.NewRow();
    //        dr1[0] = "DECISION PENDING";
    //        dr1[1] = "Decision Pending";
    //        dt.Rows.Add(dr1);

    //        DataRow dr2 = dt.NewRow();
    //        dr2[0] = "NO VOYAGE";
    //        dr2[1] = "Approved(NO VOY)";
    //        dt.Rows.Add(dr2);


    //        DataRow dr3 = dt.NewRow();
    //        dr3[0] = "ASSIGNED";
    //        dr3[1] = "Vessel Assigned";
    //        dt.Rows.Add(dr3);


    //        DataRow dr4 = dt.NewRow();
    //        dr4[0] = "PLANNED";
    //        dr4[1] = "Event Planned";
    //        dt.Rows.Add(dr4);

    //        DataRow dr5 = dt.NewRow();
    //        dr5[0] = "CURRENT";
    //        dr5[1] = "Current";
    //        dt.Rows.Add(dr5);

    //        DataRow dr6 = dt.NewRow();
    //        dr6[0] = "SIGNED OFF";
    //        dr6[1] = "Signed OFF";
    //        dt.Rows.Add(dr6);

    //        DataRow dr7 = dt.NewRow();
    //        dr7[0] = "NTBR";
    //        dr7[1] = "NTBR";
    //        dt.Rows.Add(dr7);

    //        DataRow dr8 = dt.NewRow();
    //        dr8[0] = "INACTIVE";
    //        dr8[1] = "Inactive";
    //        dt.Rows.Add(dr8);
    //        return dt;
    //    }


    //    [WebMethod]
    //    public DataTable Get_RankList()
    //    {
    //        return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_CRW_Get_RankList").Tables[0];
    //    }

    //    [WebMethod]
    //    public DataTable Get_Crewlist_Index(string CREW_STATUS, int VESSEL_ID, string SearchString)
    //    {
    //        try
    //        {
    //            DateTime Dt_FromJoningDate =  DateTime.Today.AddYears(-10);
    //            DateTime Dt_ToJoningDate = DateTime.Today;

    //            //string UserType = GetCurrentUserType(iUserID);

    //            string SQL = "";

    //            SQL = @"SELECT     CD.ID, CD.Staff_Code as 'Staff Code', CD.Staff_Name + ' ' + isnull(CD.Staff_Surname,'') as Name,                              
    //                              CASE WHEN rank.Rank_Short_Name IS NULL THEN CRW_LIB_Crew_Ranks.Rank_Short_Name ELSE rank.Rank_Short_Name END AS Rank, 
    //                                LIB_Country.Country_Name as Nationality, 
    //                               CASE
    //						        WHEN CD.Status_Code = 'NTBR' THEN 'NTBR' 
    //						        WHEN CD.ACTIVE_STATUS = 0 THEN 'INACTIVE'    
    //                                
    //                                WHEN V.JOINING_DATE IS NOT NULL AND V.SIGN_OFF_DATE IS NULL AND (V.EST_SING_OFF_DATE BETWEEN GETDATE() and DATEADD(DD,30,GETDATE()) OR V.EST_SING_OFF_DATE < GETDATE()) THEN 'COC DUE'                                
    //                                WHEN V.JOINING_DATE IS NOT NULL AND V.SIGN_OFF_DATE IS NOT NULL AND EV.CrewID_UnAssigned IS NULL THEN 'SIGNED OFF'
    //                                WHEN V.JOINING_DATE IS NOT NULL AND V.SIGN_OFF_DATE IS NULL THEN 'CURRENT'
    //
    //						        WHEN EV.CrewID_UnAssigned IS NOT NULL AND ev.Event_Date IS NOT NULL THEN 'PLANNED'
    //                                WHEN EV.CrewID_UnAssigned IS NOT NULL AND ev.Event_Date IS NULL THEN 'ASSIGNED'
    //						        
    //                                WHEN isnull(CD.ManningOfficeStatus, 0)= 0 OR isnull(CD.CrewManagerApproval,0) = 0 THEN 'PENDING'						
    //						        WHEN ((V.JOINING_DATE IS NULL AND V.SIGN_OFF_DATE IS NULL ) and (MANNINGOFFICESTATUS = 1 AND CREWMANAGERAPPROVAL = 1) AND (EV.CrewID_UnAssigned IS  NULL AND ev.Event_Date IS  NULL)) THEN 'NO VOYAGE' END AS CrewStatus
    //
    //                FROM         LIB_COMPANY RIGHT OUTER JOIN
    //                                      (SELECT    CRW_DTL_CrewAssignment_1.CrewID_UnAssigned, CRW_DTL_CrewAssignment.CrewID_SigningOff,  
    //                      CRW_DTL_CrewChangeEvent.PKID as EventID, CRW_DTL_CrewChangeEvent.PortID, CRW_DTL_CrewChangeEvent.Event_Date
    //FROM         CRW_DTL_CrewChangeEvent INNER JOIN
    //                      CRW_DTL_CrewChangeEvent_Members ON CRW_DTL_CrewChangeEvent.PKID = CRW_DTL_CrewChangeEvent_Members.EventID LEFT OUTER JOIN
    //                      CRW_DTL_CrewAssignment ON CRW_DTL_CrewChangeEvent_Members.CrewID = CRW_DTL_CrewAssignment.CrewID_SigningOff LEFT OUTER JOIN
    //                      CRW_DTL_CrewAssignment AS CRW_DTL_CrewAssignment_1 ON 
    //                      CRW_DTL_CrewChangeEvent_Members.CrewID = CRW_DTL_CrewAssignment_1.CrewID_UnAssigned) EV RIGHT OUTER JOIN
    //                                      CRW_LIB_Crew_Details AS CD ON EV.CrewID_UnAssigned = CD.ID ON LIB_COMPANY.Id = CD.ManningOfficeID LEFT OUTER JOIN
    //                                      CRW_LIB_Crew_Ranks ON CD.Rank_Applied = CRW_LIB_Crew_Ranks.ID LEFT OUTER JOIN
    //                                      LIB_Country ON CD.Staff_Nationality = LIB_Country.ID LEFT OUTER JOIN
    //                                          (SELECT     WC.WageContractID, WC.CrewID, WC.VoyageID, SUM(ACC_DTL_JOINING_EARN_DEDUCTION.Amount) AS Amount
    //                                            FROM          ACC_DTL_Crew_WageContract AS WC INNER JOIN
    //                                                                   ACC_DTL_JOINING_EARN_DEDUCTION ON WC.WageContractID = ACC_DTL_JOINING_EARN_DEDUCTION.WagesContractID
    //                                            GROUP BY WC.WageContractID, WC.CrewID, WC.VoyageID) AS salary RIGHT OUTER JOIN
    //                                      LIB_VESSELS INNER JOIN
    //                                          (SELECT     B.ID, A.CrewID, A.Staff_Code, B.Joining_Date, B.Sign_Off_Date, B.Joining_Rank, B.Voyage_Remarks, B.Vessel_ID,B.Est_Sing_Off_Date
    //                                            FROM          (SELECT     CrewID, Staff_Code, MAX(Joining_Date) AS joining_date
    //                                                                    FROM          CRW_Dtl_Crew_Voyages
    //                                                                    GROUP BY CrewID, Staff_Code) AS A INNER JOIN
    //                                                                   CRW_Dtl_Crew_Voyages AS B ON A.CrewID = B.CrewID AND A.joining_date = B.Joining_Date) AS V ON 
    //                                      LIB_VESSELS.Vessel_ID = v.Vessel_ID LEFT OUTER JOIN
    //                                      CRW_LIB_Crew_Ranks AS rank ON v.Joining_Rank = rank.ID ON salary.VoyageID = v.ID ON CD.ID = v.CrewID
    //                WHERE     (1=1)";

    //            //if (CrewID > 0)
    //            //    SQL += " AND CD.ID = @CrewID";

    //            if (CREW_STATUS != "INACTIVE")
    //                SQL += " AND (CD.Active_Status = 1) ";

    //            if (SearchString != "")
    //                SQL += " AND (UPPER(CD.Staff_Name) LIKE @SearchString OR UPPER(CD.Staff_Midname) LIKE @SearchString or UPPER(CD.Staff_Surname) LIKE  @SearchString or STAFF_CODE like  @SearchString or LIB_Country.Country_Name like @SearchString) ";

    //            //if (STAFF_CODE == "NULL")
    //            //    SQL += " AND (CD.Staff_Code is null) ";

    //            //else if (STAFF_CODE != "")
    //            //    SQL += " AND (CD.Staff_Code LIKE @Staff_Code) ";

    //            //if (NATIONALITY != 0)
    //            //    SQL += " AND (CD.Staff_Nationality = @Nation) ";

    //            //if (RankID != 0)
    //            //    SQL += " AND (v.Joining_Rank = @RankID) ";

    //            //if (UserType == "MANNING AGENT")
    //            //{
    //            //    SQL += " AND (CD.ManningOfficeID = @ManningOfficeID) ";
    //            //}

    //            //else if (UserType == "OFFICE USER")
    //            //{
    //            //    if (ManningOfficeID != 0)
    //            //        SQL += " AND (CD.ManningOfficeID = @ManningOfficeID) ";

    //            //    SQL += " AND (isnull(CD.ManningOfficeStatus,0)  = 1)";
    //            //}
    //            //else if (UserType == "ADMIN")
    //            //{
    //            //    if (ManningOfficeID != 0)
    //            //        SQL += " AND (CD.ManningOfficeID = @ManningOfficeID) ";
    //            //}

    //            //if (COC > 0)
    //            //{
    //            //    SQL += " AND (V.JOINING_DATE IS NOT NULL AND V.SIGN_OFF_DATE IS NULL AND (V.EST_SING_OFF_DATE BETWEEN GETDATE() and DATEADD(DD," + COC + ",GETDATE()) ))";
    //            //}

    //            switch (CREW_STATUS)
    //            {
    //                case "CURRENT":
    //                    SQL += " AND (V.JOINING_DATE IS NOT NULL AND  v.SIGN_OFF_DATE IS NULL) AND (v.Joining_Date between @JoinFrmDT and @JoinTODT ) ";

    //                    if (VESSEL_ID != 0)
    //                    {
    //                        SQL += " AND v.Vessel_ID = @Vessel_ID";
    //                    }

    //                    break;
    //                case "SIGNED OFF":
    //                    SQL += " AND (V.JOINING_DATE IS NOT NULL AND V.SIGN_OFF_DATE IS NOT NULL AND EV.CrewID_UnAssigned IS  NULL) and (MANNINGOFFICESTATUS = 1 AND CrewManagerApproval = 1) ";
    //                    break;

    //                case "NO VOYAGE":
    //                    SQL += " AND ((V.JOINING_DATE IS  NULL AND  v.SIGN_OFF_DATE IS NULL) and (MANNINGOFFICESTATUS = 1 AND CrewManagerApproval = 1) AND (EV.CrewID_UnAssigned IS  NULL AND ev.Event_Date IS  NULL))";
    //                    break;

    //                case "INACTIVE":
    //                    SQL += " AND (CD.ACTIVE_STATUS = 0)";
    //                    break;

    //                case "ASSIGNED":
    //                    SQL += " AND ((V.JOINING_DATE IS  NULL AND  v.SIGN_OFF_DATE IS NULL) AND CD.ID = EV.CrewID_UnAssigned AND ev.Event_Date IS NULL)";
    //                    break;

    //                case "PLANNED":
    //                    SQL += " AND ( (V.JOINING_DATE IS  NULL AND  v.SIGN_OFF_DATE IS NULL) AND CD.ID = EV.CrewID_UnAssigned AND ev.Event_Date IS NOT NULL)";
    //                    break;

    //                case "DECISION PENDING":
    //                    SQL += " AND (isnull(CD.ManningOfficeStatus, 0)= 0 OR isnull(CD.CrewManagerApproval,0) = 0 )";
    //                    break;

    //                case "NTBR":
    //                    SQL += " AND (CD.Status_Code = 'NTBR')";
    //                    break;
    //            }

    //            SQL += " ORDER BY LIB_VESSELS.VESSEL_SHORT_NAME,CRW_LIB_Crew_Ranks.Rank_Sort_Order";

    //            SqlParameter[] sqlprm = new SqlParameter[]
    //                                        { 
    //                                            new SqlParameter("@Vessel_ID",VESSEL_ID),
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

    //    private string GetCurrentUserType(int iUserID)
    //    {
    //        return Get_UserData_DL(iUserID, "USER_TYPE");
    //    }

    //    private string Get_UserData_DL(int UserID, string DataColumn)
    //    {
    //        string SQL = "";

    //        SQL = "SELECT " + DataColumn + " FROM LIB_USER WHERE USERID=@UserID";

    //        SqlParameter[] obj = new SqlParameter[]{ 
    //                                                      new SqlParameter("@UserID",UserID)
    //                                                  };

    //        return Convert.ToString(SqlHelper.ExecuteScalar(connection, CommandType.Text, SQL, obj));
    //    }

    //    private DataTable ExecuteQuery(string SQLCommandText, SqlParameter[] sqlprm)
    //    {
    //        return SqlHelper.ExecuteDataset(connection, CommandType.Text, SQLCommandText, sqlprm).Tables[0];
    //    }

    //    [WebMethod]
    //    public DataTable Get_DocTypeList()
    //    {
    //        return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_DMS_Get_DocTypeList").Tables[0];
    //    }

    //    [WebMethod]
    //    public DataTable Get_AssignedAttributesToTypeID(int DocTypeID)
    //    {
    //        SqlParameter sqlprm = new SqlParameter("@DocTypeID", DocTypeID);
    //        return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_DMS_Get_AssignedAttributesToTypeID", sqlprm).Tables[0];
    //    }

    //    [WebMethod]
    //    public void SaveCrewDocument(int CrewID, int RankID, int iDocID, int iTypeID, string sDocName, string FileType, string FileName, string FilePath, string DocNo, string IssuePlace, string IssueDate, string ExpiryDate, int CreatedBy)    
    //    {        
    //        try
    //        {
    //            //UPDATE_CrewDocument(CrewID, iDocID, iTypeID, sDocName, DocNo, IssueDate, IssuePlace, ExpiryDate, CreatedBy);
    //            UPDATE_DocumentChecklist(CrewID, iTypeID, 1, RankID, "", DocNo, IssueDate, IssuePlace, ExpiryDate, CreatedBy, FilePath);

    //        }
    //        catch 
    //        {
    //        }
    //    }

    //    public int UPDATE_CrewDocument(int CrewID, int DocID, int DocTypeID, string DocumentName, string DocNo, string IssueDate, string IssuePalce, string ExpiryDate, int Modified_By)
    //    {
    //        try
    //        {
    //            DateTime Dt_IssueDate = DateTime.Parse("1900/01/01");
    //            if (IssueDate != "")
    //                Dt_IssueDate = DateTime.Parse(IssueDate);

    //            DateTime Dt_ExpiryDate = DateTime.Parse("1900/01/01");
    //            if (ExpiryDate != "")
    //                Dt_ExpiryDate = DateTime.Parse(ExpiryDate);

    //            return UPDATE_CrewDocument_DL(CrewID, DocID, DocTypeID, DocumentName, DocNo, Dt_IssueDate, IssuePalce, Dt_ExpiryDate, Modified_By);
    //        }
    //        catch
    //        {
    //            throw;
    //        }
    //    }
    //    public int UPDATE_DocumentChecklist(int CrewID, int DocTypeID, int AnswerYN, int RankID, string Remark, string DocNo, string IssueDate, string IssuePlace, string ExpiryDate, int Modified_By, string FilePath)
    //    {
    //        try
    //        {
    //            if (IssueDate != null && ExpiryDate != null)
    //            {
    //                DateTime Dt_IssueDate = DateTime.Parse("01/01/1900");
    //                if (IssueDate.Length > 0)
    //                    Dt_IssueDate = DateTime.Parse(IssueDate);

    //                DateTime Dt_ExpiryDate = DateTime.Parse("01/01/1900");
    //                if (ExpiryDate.Length > 0)
    //                    Dt_ExpiryDate = DateTime.Parse(ExpiryDate);

    //                UPDATE_DocumentChecklist_DL(CrewID, DocTypeID, AnswerYN, RankID, Remark, DocNo, Dt_IssueDate, IssuePlace, Dt_ExpiryDate, Modified_By);

    //                string FileExt = "";
    //                string FileName = "";

    //                if (FilePath != null && FilePath != "")
    //                {
    //                    FileName = System.IO.Path.GetFileName(FilePath).ToLower();
    //                    FileExt = System.IO.Path.GetExtension(FilePath).ToLower();

    //                    INS_CrewDocuments_DL(CrewID, DocTypeID, DocNo, FileName, FileExt, Modified_By, DocNo, Dt_IssueDate, IssuePlace, Dt_ExpiryDate);
    //                }

    //                return 1;
    //            }
    //            else
    //                return 0;
    //        }
    //        catch
    //        {
    //            throw;
    //        }


    //    }
    //    public int INS_CrewDocuments_DL(int CrewID, int DocTypeID, string DocumentName, string DocFileName, string DocFileExt, int Created_By, string DocNo, DateTime IssueDate, string IssuePalce, DateTime ExpiryDate)
    //    {
    //        SqlParameter[] sqlprm = new SqlParameter[]
    //            {   
    //                new SqlParameter("@CrewID",CrewID),
    //                new SqlParameter("@DocumentName",DocumentName),
    //                new SqlParameter("@DocFileName",DocFileName),
    //                new SqlParameter("@DocFileExt",DocFileExt),
    //                new SqlParameter("@DocTypeID",DocTypeID),
    //                new SqlParameter("@Created_By",Created_By),
    //                new SqlParameter("@DocNo",DocNo),
    //                new SqlParameter("@IssueDate",IssueDate),
    //                new SqlParameter("@IssuePlace",IssuePalce),
    //                new SqlParameter("@ExpiryDate",ExpiryDate),
    //                new SqlParameter("return",SqlDbType.Int)
    //            };
    //        sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
    //        SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_DMS_Insert_Crew_Document", sqlprm);
    //        return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
    //    }

    //    public int UPDATE_CrewDocument_DL(int CrewID, int DocID, int DocTypeID, string DocumentName, string DocNo, DateTime IssueDate, string IssuePalce, DateTime ExpiryDate, int Modified_By)
    //        {
    //            SqlParameter[] sqlprm = new SqlParameter[]
    //                                        { 
    //                                            new SqlParameter("@DocID",DocID),
    //                                            new SqlParameter("@DocTypeID",DocTypeID),
    //                                            new SqlParameter("@DocName",DocumentName),                                            
    //                                            new SqlParameter("@DocNo",DocNo),
    //                                            new SqlParameter("@IssueDate",IssueDate),
    //                                            new SqlParameter("@IssuePalce",IssuePalce),
    //                                            new SqlParameter("@ExpiryDate",ExpiryDate),
    //                                            new SqlParameter("@Modified_By",Modified_By),                                            
    //                                            new SqlParameter("return",SqlDbType.Int)
    //                                        };
    //            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
    //            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_DMS_Update_Crew_Document", sqlprm);
    //            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
    //        }

    //    public int UPDATE_DocumentChecklist_DL(int CrewID, int DocTypeID, int AnswerYN, int RankID, string Remarks, string DocNo, DateTime IssueDate, string IssuePlace, DateTime ExpiryDate, int Modified_By)
    //    {
    //        SqlParameter[] sqlprm = new SqlParameter[]
    //                                        { 
    //                                            new SqlParameter("@CrewID",CrewID),
    //                                            new SqlParameter("@DocTypeID",DocTypeID),
    //                                            new SqlParameter("@AnswerYN",AnswerYN),
    //                                            new SqlParameter("@RankID",RankID),
    //                                            new SqlParameter("@Remarks",Remarks),
    //                                            new SqlParameter("@DocNo",DocNo),
    //                                            new SqlParameter("@IssueDate",IssueDate),
    //                                            new SqlParameter("@IssuePlace",IssuePlace),
    //                                            new SqlParameter("@ExpiryDate",ExpiryDate),
    //                                            new SqlParameter("@Modified_By",Modified_By),
    //                                            new SqlParameter("return",SqlDbType.Int)
    //                                        };
    //        sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
    //        SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_CRW_Update_DocumentChecklist", sqlprm);
    //        return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
    //    }

    //    #endregion

    #region Travel Module releted

    [WebMethod]
    public DataTable getRequisitionStages()
    {
        try
        {
            return SqlHelper.ExecuteDataset(Connection_Req, CommandType.StoredProcedure, "CRW_TRV_SP_Get_RequisitionStages").Tables[0];
        }
        catch (Exception ex)
        {
            CustomSoapException(ex.GetType().Name, ex.Message);
            return null;
        }
    }

    [WebMethod]
    public DataTable Get_SupplierList(string searchtext)
    {
        try
        {
            return BLL_TRV_Supplier.Get_SupplierList(searchtext);
        }
        catch (Exception ex)
        {
            CustomSoapException(ex.GetType().Name, ex.Message);
            return null;
        }
    }

    [WebMethod]
    public DataTable Get_AirportList(string searchtext)
    {
        try
        {
            BLL_Crew_CrewDetails objCrew = new BLL_Crew_CrewDetails();
            return objCrew.Get_AirportList(searchtext);
        }
        catch (Exception ex)
        {
            CustomSoapException(ex.GetType().Name, ex.Message);
            return null;
        }
    }

    [WebMethod]
    public DataTable Get_UploadCategoryList()
    {
        try
        {
            return SqlHelper.ExecuteDataset(Connection_Req, CommandType.StoredProcedure, "CRW_TRV_SP_Get_UploadCategories").Tables[0];
        }
        catch (Exception ex)
        {
            CustomSoapException(ex.GetType().Name, ex.Message);
            return null;
        }
    }

    [WebMethod]
    public DataSet GetRequestList(int vessel_id, string stage, string request_id, string pax_name, string airport, int agent_id)
    {
        try
        {
            BLL_TRV_TravelRequest objTrv = new BLL_TRV_TravelRequest();
            DataSet ds = new DataSet();
            int rowcount = 1;
            ds = objTrv.GetRequestList(vessel_id, stage, request_id, pax_name, airport, agent_id, ref rowcount);
            return ds;
        }
        catch (Exception ex)
        {
            CustomSoapException(ex.GetType().Name, ex.Message);
            return null;
        }
    }

    [WebMethod]
    public Boolean SaveAttchement(int requestid, string attachment_name, string attachment_path, string attachment_type, string refnumber, int userid)
    {
        BLL_TRV_Attachment objTrv = new BLL_TRV_Attachment();
        try { 
                return objTrv.SaveAttchement(requestid, attachment_name, attachment_path, attachment_type, refnumber, userid,null); 
        }
        catch { throw; }
        finally { objTrv = null; }
    }
    #endregion

}