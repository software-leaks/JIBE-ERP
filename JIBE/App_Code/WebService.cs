using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;

using SMS.Business.Crew;
using SMS.Business.Infrastructure;
using SMS.Business.Operation;
using SMS.Business.PURC;
using SMS.Business.QMS;
using SMS.Business.PortageBill;
using SMS.Business.TRAV;
using BLLQuotation;
using SMS.Business.PMS;
using SMS.Data.PMS;
using System.Xml;
using System.Net;


/// <summary>
/// Summary description for WebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class WebService : System.Web.Services.WebService
{


    public WebService()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string Tree()
    {

        string strOut = @"[{'text': '1. Pre Lunch (120 min)','expanded': true,'classes': 'important',
                        'children':
                            [{'text': '1.1 The State of the Powerdome (30 min)'},
                             {'text': '1.2 The Future of jQuery (30 min)'},
                             {'text': '1.2 jQuery UI - A step to richnessy (60 min)'}
		                    ]
	                    }
                       ]";
        return strOut;
    }

    [WebMethod(EnableSession = true)]
    public string getPlannedInterviewForTheMonth(string userid, string crewid, string m, string y, string showcalforall)
    {
        BLL_Crew_CrewDetails objCrewBLL = new BLL_Crew_CrewDetails();
        int Month = 1;
        int Year = 1900;
        int UserID = 0;
        int CrewID = 0;
        int ShowCalForAll = 0;

        if (m != "" && m != "undefined")
            Month = int.Parse(m);
        if (y != "" && y != "undefined")
            Year = int.Parse(y);
        if (userid != "" && userid != "undefined")
            UserID = int.Parse(userid);
        if (crewid != "" && crewid != "undefined")
            CrewID = int.Parse(crewid);
        if (showcalforall != "" && showcalforall != "undefined")
            ShowCalForAll = int.Parse(showcalforall);

        SqlDataReader dr = objCrewBLL.Get_PlannedInterviewesForTheMonth(UserID, CrewID, Month, Year, ShowCalForAll);

        return GetJsArray(dr);
    }

    [WebMethod]
    public string getInterviewResultForCrew(string crewid)
    {
        BLL_Crew_CrewDetails objCrewBLL = new BLL_Crew_CrewDetails();
        int CrewID = 0;
        if (crewid != "" && crewid != "undefined")
            CrewID = int.Parse(crewid);

        SqlDataReader dr = objCrewBLL.Get_InterviewResultsForCrew(CrewID);
        SqlDataReader dr2 = objCrewBLL.Get_InterviewResultsForCrew(CrewID);


        string ResultTable = GetJsArray(dr);

        while (dr2.Read())
        {
            int InterviewID = int.Parse(dr2["ID"].ToString());

            SqlDataReader drRes = objCrewBLL.Get_UserAnswers(CrewID, InterviewID);
            DataSet ds = objCrewBLL.Get_InterviewerRecomendations(CrewID, InterviewID);

            string AnswarTable = GetJsArray(drRes);
            string RecomendedVessels = GetJsArray(ds.Tables[0]);
            string RecomendedZones = GetJsArray(ds.Tables[1]);

            if (AnswarTable != "")
                AnswarTable = "[" + AnswarTable + "]";
            else
                AnswarTable = "''";

            if (RecomendedVessels != "")
                RecomendedVessels = "[" + RecomendedVessels + "]";
            else
                RecomendedVessels = "''";

            if (RecomendedZones != "")
                RecomendedZones = "[" + RecomendedZones + "]";
            else
                RecomendedZones = "''";

            ResultTable = ResultTable.Replace("'" + "UserAnswerTable" + InterviewID + "'", AnswarTable);
            ResultTable = ResultTable.Replace("'" + "RecomendedVessels" + InterviewID + "'", RecomendedVessels);
            ResultTable = ResultTable.Replace("'" + "RecomendedZones" + InterviewID + "'", RecomendedZones);
        }
        return ResultTable;
    }

    [WebMethod(EnableSession = true)
    ]
    public string getInterviewsScheduledForToday(string userid)
    {
        BLL_Crew_CrewDetails objCrewBLL = new BLL_Crew_CrewDetails();

        int UserID = 0;
        if (userid != "")
            UserID = int.Parse(userid);

        SqlDataReader dr = objCrewBLL.getInterviewsScheduledForToday(UserID);
        return GetJsArray(dr);
    }

    [WebMethod]
    public string getPendingInterviewList(string userid)
    {
        BLL_Crew_CrewDetails objCrewBLL = new BLL_Crew_CrewDetails();

        int UserID = 0;
        if (userid != "")
            UserID = int.Parse(userid);

        SqlDataReader dr = objCrewBLL.getPendingInterviewList(UserID);
        return GetJsArray(dr);
    }

    [WebMethod]
    public string getPendingInterviewList_By_UserID(string userid)
    {
        BLL_Crew_CrewDetails objCrewBLL = new BLL_Crew_CrewDetails();

        int UserID = 0;
        if (userid != "")
            UserID = int.Parse(userid);

        SqlDataReader dr = objCrewBLL.getPendingInterviewListt_By_UserID(UserID);
        return GetJsArray(dr);
    }
    [WebMethod]
    public string getCrewChangeAlerts(string userid, string DateFormat)
    {

        BLL_Crew_CrewDetails objCrewBLL = new BLL_Crew_CrewDetails();

        int UserID = 0;
        if (userid != "")
            UserID = int.Parse(userid);

        DataTable dt = objCrewBLL.Get_CrewChangeAlerts(UserID);
        foreach (DataRow row in dt.Rows)
            row["Event_Date"] = UDFLib.ConvertUserDateFormat(Convert.ToString(row["Event_Date"]), DateFormat);

        return GetJsArray(dt);
    }

    [WebMethod]
    public string getDocumentExpiryList(string user_id, string DateFormat)
    {
        try
        {
            BLL_Crew_CrewDetails objCrewBLL = new BLL_Crew_CrewDetails();

            if (user_id.Length > 0)
            {
                int UserID = int.Parse(user_id);
                DataSet ds = objCrewBLL.Get_DocumentExpiryList(UserID);
                DataTable dt = ds.Tables[0];

                foreach (DataRow row in dt.Rows)
                    row["DateOfExpiry"] = UDFLib.ConvertUserDateFormat(Convert.ToString(row["DateOfExpiry"]), DateFormat);

                if (dt.Rows.Count > 0)
                {
                    return GetJsArray(dt);
                }
                else
                    return "";

            }
            else
                return "";
        }
        catch
        {
            return "";
        }

    }

    [WebMethod]
    public string getLoggedinUserList(string userid)
    {
        try
        {
            BLL_Infra_UserCredentials objUserBLL = new BLL_Infra_UserCredentials();

            DataTable dt = objUserBLL.Get_LoggedinUsers(int.Parse(userid));

            return GetJsArray(dt);
        }
        catch
        {
            return "";
        }

    }

    [WebMethod]
    public string getVesselMovements(string vessel_id)
    {
        string strConn = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
        SqlConnection objCon = new SqlConnection(strConn);
        try
        {

            //string SQL = "Select Port_Call_ID, Created_Date, Vessel_Code, Port_Name,Arrival, Berthing,Departure	from Port_Calls where Vessel_Code = '" + vessel_code.ToUpper() + "' and DPL_Port_Number < 1  Order by IsNull(Arrival,GetDate()+100), Port_Call_ID ";

            string SQL = @"SELECT     Port_Calls.Port_Call_ID, Port_Calls.Created_Date, Port_Calls.Port_Name, Port_Calls.Arrival, Port_Calls.Berthing, Port_Calls.Departure, SL_Supplier.Supplier_Name, 
                      VESSELS.VESSEL_CODE
                FROM         Port_Calls INNER JOIN
                                      VESSELS ON Port_Calls.Vessel_Code = VESSELS.VESSEL_CODE LEFT OUTER JOIN
                                      SL_Supplier ON Port_Calls.Owners_Agent = SL_Supplier.Supplier_Code
                WHERE     (Port_Calls.DPL_Port_Number < 1) AND (VESSELS.SmsLog_ID = ";
            SQL += vessel_id + ")";
            SQL += " ORDER BY ISNULL(Port_Calls.Arrival, GETDATE() + 100), Port_Calls.Port_Call_ID";

            objCon.Open();


            SqlCommand objCom = new SqlCommand(SQL, objCon);
            SqlDataReader dr = objCom.ExecuteReader();

            return GetJsArray(dr);

        }
        catch
        {
            return "";
        }
        finally
        {
            objCon.Close();
        }
    }

    [WebMethod]
    public string getLatestNoonReport(string vessel_code)
    {
        try
        {
            DataTable dt = BLL_OPS_VoyageReports.Get_LatestNoonReport(vessel_code);

            return GetJsArray(dt);

        }
        catch
        {
            return "";
        }
        finally
        {
        }
    }

    [WebMethod]
    public string setCrewCard_AttachmentStatus(string cardid, string atttype, string status, string userid)
    {
        try
        {
            BLL_Crew_CrewDetails objCrewBLL = new BLL_Crew_CrewDetails();

            int ret = objCrewBLL.UPDATE_CrewCard_AttachmentStatus(UDFLib.ConvertToInteger(cardid), atttype, UDFLib.ConvertToInteger(status), UDFLib.ConvertToInteger(userid));
            return "1";
        }
        catch
        {
            return "0";
        }
        finally
        {
        }
    }

    [WebMethod]
    public string getUSVisaAlerts(string userid, string DateFormat)
    {
        BLL_Crew_CrewDetails objCrewBLL = new BLL_Crew_CrewDetails();

        int UserID = 0;
        if (userid != "")
            UserID = int.Parse(userid);

        DataTable dt = objCrewBLL.Get_USVisaAlerts(UserID);

        foreach (DataRow row in dt.Rows)
            row["Us_Visa_Expiry"] = UDFLib.ConvertUserDateFormat(Convert.ToString(row["Us_Visa_Expiry"]), DateFormat);

        return GetJsArray(dt);
    }

    [WebMethod(EnableSession = true)]
    public string getCrewCardRemarks(string cardid, string userid)
    {
        BLL_Crew_CrewDetails objCrewBLL = new BLL_Crew_CrewDetails();

        int UserID = 0;
        if (userid != "")
            UserID = int.Parse(userid);

        int CardID = 0;
        if (cardid != "")
            CardID = int.Parse(cardid);

        SqlDataReader dr = objCrewBLL.Get_CrewCard_Remarks(CardID, UserID);
        return GetJsArray(dr);
    }

    [WebMethod(EnableSession = true)]
    public string getCrewTransferDetails(string crewid, string voyageid, string userid)
    {
        BLL_Crew_CrewDetails objCrewBLL = new BLL_Crew_CrewDetails();
        DataTable dt = objCrewBLL.Get_Transfer_Promotions(UDFLib.ConvertToInteger(crewid), UDFLib.ConvertToInteger(voyageid), UDFLib.ConvertToInteger(userid));
        UDFLib.ChangeColumnDataType(dt, "Effective_Date", typeof(string));
        if (dt.Rows.Count > 0)
        {
            foreach (DataRow dr in dt.Rows)
            {
                if (!string.IsNullOrEmpty(dr["Effective_Date"].ToString()))
                {
                    dr["Effective_Date"] = UDFLib.ConvertUserDateFormat(Convert.ToDateTime(dr["Effective_Date"].ToString()).ToString("dd/MM/yyyy"), UDFLib.GetDateFormat());
                }
            }
        }
        return UDFLib.CreateHtmlTableFromDataTable(dt, new string[] { "From Vessel", "Current Rank", "To Vessel", "Joining Rank", "Effective Date" }, new string[] { "FromVessel", "FromRank", "ToVessel", "ToRank", "Effective_Date" }, "Crew Transfer/Promotion Details");

    }

    [WebMethod]
    public string getContracts_ForDigiSign_Alerts(string userid, string DateFormat)
    {
        BLL_Crew_CrewDetails objCrewBLL = new BLL_Crew_CrewDetails();

        int UserID = 0;
        if (userid != "")
            UserID = int.Parse(userid);

        DataTable dt = objCrewBLL.Get_Contracts_ForDigiSign_Alerts(UserID);

        foreach (DataRow row in dt.Rows)
            row["CONTRACT_DATE"] = UDFLib.ConvertUserDateFormat(Convert.ToString(row["CONTRACT_DATE"]), DateFormat);

        return GetJsArray(dt);
    }

    [WebMethod]
    public string getContracts_ToVerify_Alerts(string userid, string DateFormat)
    {
        BLL_Crew_CrewDetails objCrewBLL = new BLL_Crew_CrewDetails();

        int UserID = 0;
        if (userid != "")
            UserID = int.Parse(userid);

        DataTable dt = objCrewBLL.Get_Contracts_ToVerify_Alerts(UserID);

        foreach (DataRow row in dt.Rows)
            row["CONTRACT_DATE"] = UDFLib.ConvertUserDateFormat(Convert.ToString(row["CONTRACT_DATE"]), DateFormat);

        return GetJsArray(dt);
    }

    #region PortageBill methods

    [WebMethod]
    public string get_CrewWages(string CrewID, string Month, string Year)
    {
        return UDFLib.CreateHtmlTableFromDataTable(BLL_PB_PortageBill.ACC_Get_CrewWages_ByMonth(Convert.ToInt32(CrewID), Int32.Parse(Month), Int32.Parse(Year)).Tables[0], new string[] { "Type", "Component", "Amount" }, new string[] { "Salary_type", "name", "amount" }, "Salary Details:"); ;
    }

    [WebMethod]
    public string RequestUserPreference(string RequestID, string UserID)
    {
        try
        {
            int RequestID_ = 0;
            int UserID_ = 0;

            if (RequestID != "")
                RequestID_ = int.Parse(RequestID);

            if (UserID != "")
                UserID_ = int.Parse(UserID);

            BLL_TRV_TravelRequest objTrv = new BLL_TRV_TravelRequest();
            return objTrv.RequestUserPreference(RequestID_, UserID_);


        }
        catch
        {
            return "0";
        }
    }

    [WebMethod]
    public string Get_ApprovalUserList(string RequestID, string UserID)
    {
        try
        {
            int RequestID_ = 0;
            int UserID_ = 0;

            if (RequestID != "")
                RequestID_ = int.Parse(RequestID);

            if (UserID != "")
                UserID_ = int.Parse(UserID);

            BLL_TRV_TravelRequest objTrv = new BLL_TRV_TravelRequest();
            objTrv.RequestUserPreference(RequestID_, UserID_);

            return "1";
        }
        catch
        {
            return "0";
        }
    }

    #endregion


    #region Purchase methods//----------------------------------------
    [WebMethod]
    public string Purc_Get_Remarks(string DocumentCode, string Remark_Type)
    {

        int RemarkType = 0;

        if (Remark_Type != "")
            RemarkType = int.Parse(Remark_Type);

        DataTable dtRemarks = BLL_PURC_Common.GET_Remarks(DocumentCode, RemarkType);
        return GetJsArray(dtRemarks);
    }

    [WebMethod]
    public void Purc_Ins_Remarks(string DocCode, string UserID, string Remark, string Remark_Type)
    {

        int RemarkType = 0;


        if (Remark_Type != "")
            RemarkType = int.Parse(Remark_Type);

        int sts = BLL_PURC_Common.INS_Remarks(DocCode, Convert.ToInt32(UserID), Remark, RemarkType);

    }

    [WebMethod]
    public string Purc_Get_SupplierDetails_ByCode(string SuppCode)
    {
        DataTable dtSuppDetails = BLL_PURC_Common.Get_SupplierDetails_ByCode(SuppCode);
        return GetJsArray(dtSuppDetails);
    }
    [WebMethod]
    public string Purc_Get_Supplier_Status(string SuppCode)
    {
        return BLL_PURC_Common.Get_Supplier_Status(SuppCode).ToString();
    }

    [WebMethod]
    public string Purc_Get_Quotation_Items_Compare(string Catalogue, string ReqsnCode, string DocumentCode, string VesselCode, string SupplierCodes, string QuotationCodes, string ExchangeRates)
    {
        DataTable dtItems = BLL_PURC_Common.Get_Quotation_Items_Compare(Catalogue, ReqsnCode, DocumentCode, VesselCode, SupplierCodes, QuotationCodes, ExchangeRates);
        return GetJsArray(dtItems);
    }

    [WebMethod]
    public string Purc_Get_Check_ReqsnValidity(string ReqsnCode)
    {
        return BLL_PURC_Common.Get_Check_ReqsnValidity(ReqsnCode).ToString();
    }

    [WebMethod]
    public string Purc_Get_Approval_History(string Log_ID)
    {
        return UDFLib.CreateHtmlTableFromDataTable(BLL_PURC_LOG.Get_Log_Logistic_Approvals(Convert.ToInt32(Log_ID)), new string[] { "Name", "Amount", "Date", "Remark" }, new string[] { "First_Name", "AMOUNT", "APPDATE", "Approver_Remark" }, new string[] { "left", "right", "left", "left" }, "Approvals:");
    }

    [WebMethod]
    public string Get_Log_Remark(string Log_ID, string Remark_Type)
    {
        return UDFLib.CreateHtmlTableFromDataTable(BLL_PURC_LOG.Get_Log_Remark(Convert.ToInt32(Log_ID), UDFLib.ConvertIntegerToNull(Remark_Type)),
                                                new string[] { "User", "Date", "Stage", "Remark" },
                                                new string[] { "First_Name", "RDate", "RemarkType", "remark" }, "");
    }

    [WebMethod]
    public string Ins_Log_Remark(string Remark_Type, string Remark, string User_ID, string Log_ID)
    {
        return BLL_PURC_LOG.Ins_Log_Remark(Convert.ToInt32(Log_ID), Remark, Convert.ToInt32(User_ID), Convert.ToInt32(Remark_Type)).ToString();
    }

    [WebMethod]
    public string asyncUpdateSupplRemarks(string Quotation_Code, string Item_Ref_Code, string Remark)
    {
        clsQuotationBLL objQtn = new clsQuotationBLL();
        return objQtn.UpdateSupplRemarks("", Quotation_Code, "", "", Item_Ref_Code, "", Remark).Tables[0].Rows[0][0].ToString();
    }
    [WebMethod]
    public string asyncGetSupplRemarks(string Quotation_Code, string Item_Ref_Code)
    {
        clsQuotationBLL objQtn = new clsQuotationBLL();
        return objQtn.GetSupplRemarks(Quotation_Code, Item_Ref_Code);
    }

    [WebMethod]

    public string asyncGetMachinery_Popup(string systemid)
    {

        DAL_PMS_Library_Jobs obj = new DAL_PMS_Library_Jobs();

        return UDFLib.CreateHtmlTableFromDataTable(obj.GetMachineryInfoPopup(Convert.ToInt32(systemid))
            , new string[] { "Description", "Model", "Set Installed", "Particular", "Maker", "Function" }
            , new string[] { "System_Description", "Module_Type", "Set_Instaled", "System_Particulars", "Maker", "Functions" }
            , new string[] { "left", "left", "left", "left", "left", "left" }
            , "", "", "", UDTRepeatDirection.Vertical, "Machinery's Info.");
    }




    #endregion//-----------------------------------------------------------

    #region Meeting room booking methods//----------------------------------------

    [WebMethod]
    public string getMeetingRoomBookingCalendar(string bookingdate, string userid)
    {

        if (bookingdate != "")
        {


        }

        SqlDataReader dr = BLL_BookingCalendar.BookingCalenderListForDate(Convert.ToDateTime(bookingdate), Convert.ToInt32(userid.ToString()));
        return GetJsArray(dr);

    }

    [WebMethod]

    public string CheckValidBookingDateTime(string bookingdate, string bookingtimefrom, string bookingtimeto)
    {
        return BLL_BookingCalendar.BookingCalenderCheckValidBokingDateTime(Convert.ToDateTime(bookingdate), bookingtimefrom, bookingtimeto);

    }



    #endregion



    //---------------GENERIC FUNCTIONS-----------------------------------------------------
    private string getRecords(string table, string fields, string filter, string orderby)
    {
        string connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
        SqlConnection myConnection = new SqlConnection(connection);
        SqlDataReader dr;
        string res = "";
        //string sColumnValues = "";
        string[] arFields = fields.Split(',');

        string sql = "SELECT " + fields + " FROM " + table;
        if (filter.Length > 0) sql += " WHERE " + filter;
        if (orderby.Length > 0) sql += " ORDER BY " + orderby;

        SqlCommand myCommand = new SqlCommand(sql, myConnection);
        try
        {
            myConnection.Open();
            dr = myCommand.ExecuteReader();

            res = GetJsArray(dr);
        }
        catch (Exception ex)
        {
            return "ERROR:" + ex.Message + sql;
        }
        finally
        {
            myConnection.Close();
            myConnection.Dispose();
        }
        return res;
    }

    private string GetJsArray(SqlDataReader dr)
    {
        string res = "";
        string sColumnValues = "";

        while (dr.Read())
        {
            if (res.Length > 0) res += ",";
            res += "{";
            sColumnValues = "";
            for (int i = 0; i < dr.VisibleFieldCount; i++)
            {
                if (sColumnValues.Length > 0) sColumnValues += ",";
                sColumnValues += dr.GetName(i).ToString() + ":'" + dr[i].ToString() + "'";
            }
            res += ReplaceSpecialCharacter(sColumnValues);
            res += "}";
        }
        if (!string.IsNullOrEmpty(res))
        {
            res = "[" + res + "]";
        }
        return res;

    }

    private string GetJsArray(DataTable dt)
    {
        string res = "";
        string sColumnValues = "";

        foreach (DataRow dr in dt.Rows)
        {
            if (res.Length > 0) res += ",";
            res += "{";
            sColumnValues = "";
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                if (sColumnValues.Length > 0) sColumnValues += ",";
                sColumnValues += dt.Columns[i].ColumnName.ToString() + ":'" + dr[i].ToString() + "'";
            }
            res += ReplaceSpecialCharacter(sColumnValues);
            res += "}";
        }
        if (!string.IsNullOrEmpty(res))
        {
            res = "[" + res + "]";
        }
        return res;

    }


    public static string ReplaceSpecialCharacter(string str)
    {
        //return str.Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("'", "&apos;").Replace("\"", "&quot;");
        string ret = str.Replace(@"\", @"\\");


        return ret;
    }

    [WebMethod]
    public string InsertVideoView(string userid, string chaperid, string itemname)
    {
        try
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("PID", typeof(int));


            BLL_Crew_Training.INSERT_Training(UDFLib.ConvertToInteger(userid), DateTime.Now, 1, UDFLib.ConvertToInteger(userid), "Video", Convert.ToDecimal(1), UDFLib.ConvertToInteger(userid), dt, Convert.ToInt32(chaperid), itemname);
            return "";
        }
        catch
        {
            return "";
        }

    }

    [WebMethod(EnableSession = true)]
    public string ValidateUSAddress(string AddressLine1, string AddressLine2, string City, string State, string ZipCode, string Country, string Type, string CrewName, string ClientName, string Mode, string StaffCode, string DOB, string AppliedRank)
    {
        StringBuilder Return = new StringBuilder();

        BLL_Crew_Admin objBLL_Crew_Admin = new BLL_Crew_Admin();
        DataTable dt = objBLL_Crew_Admin.CRW_LIB_AddressValidationSetting();
        string Url = "", Username = "", Password = "", ToEmailId = "", FromEmailId = "", EmailBody = "";

        try
        {
            var requestXml = new XmlDocument();
            StringBuilder strURLParamter = new StringBuilder();

            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    dt.DefaultView.RowFilter = "key='USPSUrl'";
                    Url = Convert.ToString(dt.DefaultView[0]["Value"]).Trim();

                    dt.DefaultView.RowFilter = "";
                    dt.DefaultView.RowFilter = "key='Username'";
                    Username = Convert.ToString(dt.DefaultView[0]["Value"]).Trim();

                    dt.DefaultView.RowFilter = "";
                    dt.DefaultView.RowFilter = "key='Password'";
                    Password = Convert.ToString(dt.DefaultView[0]["Value"]).Trim();

                    dt.DefaultView.RowFilter = "";
                    dt.DefaultView.RowFilter = "key='ToEmailId'";
                    ToEmailId = Convert.ToString(dt.DefaultView[0]["Value"]).Trim();

                    dt.DefaultView.RowFilter = "";
                    dt.DefaultView.RowFilter = "key='FromEmailId'";
                    FromEmailId = Convert.ToString(dt.DefaultView[0]["Value"]).Trim();

                    dt.DefaultView.RowFilter = "";
                    dt.DefaultView.RowFilter = "key='EmailBody'";
                    EmailBody = Convert.ToString(dt.DefaultView[0]["Value"]).Trim();

                    if (Url != "" && Username != "" && Password != "")
                    {
                        strURLParamter.Append("<AddressValidateRequest USERID=\"" + Username + "\">");

                        strURLParamter.Append("<Address ID=\"0\">");
                        strURLParamter.Append("<FirmName />");
                        strURLParamter.Append("<Address1>" + AddressLine1.Trim() + "</Address1>");
                        strURLParamter.Append("<Address2>" + AddressLine2.Trim() + "</Address2>");
                        strURLParamter.Append("<City>" + City.Trim() + "</City>");
                        strURLParamter.Append("<State>" + State.Trim() + "</State>");
                        strURLParamter.Append("<Zip5>" + ZipCode.Trim() + "</Zip5>");
                        strURLParamter.Append("<Zip4></Zip4>");
                        strURLParamter.Append("</Address>");
                        strURLParamter.Append("</AddressValidateRequest>");

                        // build XML request 

                        var httpRequest = HttpWebRequest.Create(Url + "?API=Verify&XML=" + strURLParamter.ToString());
                        httpRequest.Method = "POST";
                        httpRequest.ContentType = "text/xml";

                        // set appropriate headers

                        using (var requestStream = httpRequest.GetRequestStream())
                        {
                            requestXml.Save(requestStream);
                        }

                        using (var response = (HttpWebResponse)httpRequest.GetResponse())
                        using (var responseStream = response.GetResponseStream())
                        {
                            // may want to check response.StatusCode to
                            // see if the request was successful

                            var responseXml = new XmlDocument();
                            responseXml.Load(responseStream);
                            if (responseXml.SelectSingleNode("//Error") != null)
                            {
                                XmlNodeList objXmlNode = responseXml.SelectNodes("AddressValidateResponse/Address/Error");
                                if (objXmlNode != null)
                                {
                                    foreach (XmlNode item in objXmlNode)
                                    {
                                        Return.Append("Result:Invalid,");
                                        Return.Append("ErrorMessage: " + item["Description"].InnerText);
                                    }
                                }
                            }
                            else
                            {
                                XmlNodeList objXmlNode = responseXml.SelectNodes("AddressValidateResponse/Address");
                                if (objXmlNode != null)
                                {
                                    foreach (XmlNode item in objXmlNode)
                                    {
                                        string Address1 = "", Address2 = "";
                                        if (item["Address1"] != null)
                                        {
                                            Address1 = item["Address1"].InnerText;
                                            if (item["Address1"].InnerText.Contains("#"))
                                                AddressLine1 = "# " + AddressLine1;
                                        }
                                        if (item["Address2"] != null)
                                        {
                                            Address2 = item["Address2"].InnerText;
                                            if (item["Address2"].InnerText.Contains("#"))
                                                AddressLine2 = "# " + AddressLine2;
                                        }

                                        if (AddressLine1.ToLower() != Address1.ToLower() || AddressLine2.ToLower() != Address2.ToLower() || City.ToLower() != item["City"].InnerText.ToLower() || State.ToLower() != item["State"].InnerText.ToLower() || ZipCode.ToLower() != item["Zip5"].InnerText.ToLower())
                                        {
                                            Return.Append("Result:ValidAddress,");
                                            Return.Append("Address Line 1: " + Address2 + ",");
                                            Return.Append("Address Line 2: " + Address1 + ",");
                                            Return.Append("City: " + item["City"].InnerText + ",");
                                            Return.Append("State: " + item["State"].InnerText + ",");
                                            Return.Append("ZipCode: " + item["Zip5"].InnerText);
                                        }
                                        else
                                        {
                                            Return.Append("Result:Valid");
                                        }
                                    }
                                }
                            }
                        }

                    }
                    else
                    {
                        Return.Append("Result:No Settings");//No setting exists in DB
                    }
                }
                else
                    Return.Append("Result:No Settings");
            }
            else
                Return.Append("Result:No Settings");
        }
        catch (Exception ex)
        {
            if (Type == "Crew")
            {
                if (!ex.ToString().ToLower().Contains(":line"))
                {
                    BLL_Crew_CrewDetails objCrew = new BLL_Crew_CrewDetails();
                    EmailBody = EmailBody.Replace("$$Client$$", ClientName);
                    EmailBody = EmailBody.Replace("$$CrewName$$", CrewName);
                    EmailBody = EmailBody.Replace("$$Date$$", DateTime.Now.ToString());
                    EmailBody = EmailBody.Replace("$$Status$$", ex.ToString());
                    EmailBody = StaffCode == "" ? EmailBody.Replace("$$StaffCode$$", "-") : EmailBody.Replace("$$StaffCode$$", StaffCode);
                    EmailBody = DOB == "" ? EmailBody.Replace("$$DOB$$", "-") : EmailBody.Replace("$$DOB$$", DOB);
                    EmailBody = AppliedRank == "" ? EmailBody.Replace("$$AppliedRank$$", "-") : EmailBody.Replace("$$AppliedRank$$", AppliedRank);

                    if (Mode == "Add")
                        EmailBody = EmailBody.Replace("$$Remark$$", "USPS service is down/not responding while adding the crew.");
                    else if (Mode == "Edit")
                        EmailBody = EmailBody.Replace("$$Remark$$", "USPS service is down/not responding while updating the crew.");

                    objCrew.Send_CrewNotification(0, 0, 0, 0, ToEmailId, "", "", ClientName + "- USPS Service Error", EmailBody, "", "MAIL", "", 1, "READY");
                }
            }
            Return.Append("Result:Error"); //-- Error
        }
        return Return.ToString();
    }
}
