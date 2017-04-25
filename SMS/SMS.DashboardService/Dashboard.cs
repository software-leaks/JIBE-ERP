using System.Web.Services;
using System.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using SMS.Business.Dashboard;
using System.Collections;
using System.Web;
using System.Xml.Serialization;
using System.Web.Services.Protocols;

[WebService(Namespace = "JibeWebServiceNS")]
[System.Web.Script.Services.ScriptService]

public partial class JibeDashboardService : System.Web.Services.WebService
{
    public JibeDashboardService()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string asyncGet_OverdueApproval_FileSchedules(string UserID)
    {
        UDCHyperLink alink = new UDCHyperLink();
        alink.Target = "_blank";

        Dictionary<string, UDCHyperLink> diclink = new Dictionary<string, UDCHyperLink>();



        return UDFLib.CreateHtmlTableFromDataTable(BLL_Dashboard.Dash_FMS_Get_ScheduleFileApprovalOverdue(UDFLib.ConvertToInteger(UserID)),
            new string[] { "Vessel", "File Name", "Schedule Date" },
            new string[] { "Vessel_Name", "FileName", "Schedule_Date" },
            diclink,
            new Dictionary<string, UDCToolTip>(),
            new string[] { },
            "tbl-common-Css",
            "hdr-common-Css",
            "row-common-Css");

    }
    [WebMethod]
    public string asyncGet_OverdueReceiving_FileSchedules(string UserID)
    {
        UDCHyperLink alink = new UDCHyperLink();
        alink.Target = "_blank";


        Dictionary<string, UDCHyperLink> diclink = new Dictionary<string, UDCHyperLink>();



        return UDFLib.CreateHtmlTableFromDataTable(BLL_Dashboard.Dash_FMS_Get_ScheduleFileReceivingOverdue(UDFLib.ConvertToInteger(UserID)),
            new string[] { "Vessel", "File Name", "Schedule Date" },
            new string[] { "Vessel_Name", "FileName", "Schedule_Date" },
            diclink,
            new Dictionary<string, UDCToolTip>(),
            new string[] { },
            "tbl-common-Css",
            "hdr-common-Css",
            "row-common-Css");

    }
    [WebMethod]
    public string asyncGet_Pending_NCR(int? Assignor, int? DepartmentID)
    {
        UDCHyperLink alink = new UDCHyperLink();
        alink.Target = "_blank";
        alink.NaviagteURL = "../Technical/worklist/ViewJob.aspx";
        alink.QueryStringDataColumn = new string[] { "OFFICE_ID", "VESSEL_ID", "WORKLIST_ID" };
        alink.QueryStringText = new string[] { "OFFID", "VID", "WLID" };

        Dictionary<string, UDCHyperLink> diclink = new Dictionary<string, UDCHyperLink>();

        diclink.Add("JOB_DESCRIPTION_Short", alink);

        return UDFLib.CreateHtmlTableFromDataTable(BLL_Dashboard.Dash_Get_Pending_NCR(Assignor, DepartmentID),
            new string[] { "Vessel", "Job Description", "Assignor", "Expected Compl. Date" },
            new string[] { "Vessel_Short_Name", "JOB_DESCRIPTION_Short", "AssignorName", "DATE_ESTMTD_CMPLTN" },
            diclink,
            new Dictionary<string, UDCToolTip>(),
            new string[] { },
            "tbl-common-Css",
            "hdr-common-Css",
            "row-common-Css");

    }
    [WebMethod]
    public string asyncGet_Pending_NCR_ALL_Dept(int? Assignor)
    {
        UDCHyperLink alink = new UDCHyperLink();
        alink.Target = "_blank";
        alink.NaviagteURL = "../Technical/worklist/ViewJob.aspx";
        alink.QueryStringDataColumn = new string[] { "OFFICE_ID", "VESSEL_ID", "WORKLIST_ID" };
        alink.QueryStringText = new string[] { "OFFID", "VID", "WLID" };

        Dictionary<string, UDCHyperLink> diclink = new Dictionary<string, UDCHyperLink>();

        diclink.Add("JOB_DESCRIPTION_Short", alink);

        return UDFLib.CreateHtmlTableFromDataTable(BLL_Dashboard.Dash_Get_Pending_NCR_ALL_Dept(Assignor),
            new string[] { "Vessel", "Job Description", "Department", "Assignor", "Expected Compl. Date" },
            new string[] { "Vessel_Short_Name", "JOB_DESCRIPTION_Short", "INOFFICE_DEPT", "AssignorName", "DATE_ESTMTD_CMPLTN" },
            diclink,
            new Dictionary<string, UDCToolTip>(),
            new string[] { },
            "tbl-common-Css",
            "hdr-common-Css",
            "row-common-Css");

    }
    [WebMethod]
    public string asyncGet_Pending_Travel_PO(int User_ID)
    {
        UDCHyperLink aLink = new UDCHyperLink("", "../travel/Evaluation.aspx", new string[] { "requestid" }, new string[] { "Request_ID" }, "_blank");
        Dictionary<string, UDCHyperLink> dicLink = new Dictionary<string, UDCHyperLink>();
        dicLink.Add("Request_ID", aLink);

        return UDFLib.CreateHtmlTableFromDataTable(BLL_Dashboard.Dash_Get_Pending_Travel_PO(User_ID),
           new string[] { "Request ID", "Received On" },
           new string[] { "Request_ID", "Received_On" },
           dicLink,
            new Dictionary<string, UDCToolTip>(),
           new string[] { "center", "left" },
           "tbl-common-Css",
           "hdr-common-Css",
           "row-common-Css");

    }
    [WebMethod]
    public string asyncGet_Pending_Logistic_PO(int User_ID)
    {
        UDCHyperLink aLink = new UDCHyperLink("", "../purchase/LOG_Logistics_PO_List.aspx?LPOAPPROVAL=1", new string[] { }, new string[] { }, "_blank");
        Dictionary<string, UDCHyperLink> dicLink = new Dictionary<string, UDCHyperLink>();
        dicLink.Add("LOG_ID", aLink);

        return UDFLib.CreateHtmlTableFromDataTable(BLL_Dashboard.Dash_Get_Pending_Logistic_PO(User_ID),
           new string[] { "Logistic ID", "Received On" },
           new string[] { "LOG_ID", "Received_On" },
           dicLink,
            new Dictionary<string, UDCToolTip>(),
           new string[] { "center", "left" },
           "tbl-common-Css",
           "hdr-common-Css",
           "row-common-Css");
    }
    [WebMethod]
    public string asyncGet_Pending_Reqsn_PO(int User_ID)
    {
        UDCHyperLink alink = new UDCHyperLink();
        alink.Target = "_blank";
        alink.NaviagteURL = "../purchase/QuatationEvalution.aspx";
        alink.QueryStringDataColumn = new string[] { "REQUISITION_CODE", "document_code", "Vessel_Code", "QUOTATION_CODE", "OnHold" };
        alink.QueryStringText = new string[] { "Requisitioncode", "Document_Code", "Vessel_Code", "QUOTATION_CODE", "onhold" };

        Dictionary<string, UDCHyperLink> diclink = new Dictionary<string, UDCHyperLink>();

        diclink.Add("REQUISITION_CODE", alink);

        return UDFLib.CreateHtmlTableFromDataTable(BLL_Dashboard.Dash_Get_Pending_Reqsn(User_ID),
            new string[] { "Vessel", "Requisition Code" },
            new string[] { "Vessel_Short_Name", "REQUISITION_CODE" },
            diclink,
             new Dictionary<string, UDCToolTip>(),
            new string[] { },
            "tbl-common-Css",
            "hdr-common-Css",
            "row-common-Css");



    }
    [WebMethod]
    public string asyncGet_Provision_Last_Supplied()
    {
        return UDFLib.CreateHtmlTableFromDataTable(BLL_Dashboard.Dash_Get_Provision_Last_Supplied().Tables[0],
            new string[] { "Vessel", "Last supplied on" },
            new string[] { "Vessel_Short_Name", "DELIVERY_DATE" },

            new string[] { },
            "tbl-common-Css",
            "hdr-common-Css",
            "row-common-Css");

    }
    [WebMethod]
    public string asyncGet_User_Menu_Favourite(int UserID)
    {
        UDCHyperLink aLink = new UDCHyperLink("Menu_Link", "", new string[] { }, new string[] { }, "_blank");
        Dictionary<string, UDCHyperLink> dicLink = new Dictionary<string, UDCHyperLink>();
        dicLink.Add("Menu_Short_Discription", aLink);

        return UDFLib.CreateHtmlTableFromDataTable(BLL_Dashboard.Dash_Get_User_Menu_Favourite(UserID),
           new string[] { },
           new string[] { "Menu_Short_Discription" },
           dicLink,
            new Dictionary<string, UDCToolTip>(),
           new string[] { "left" },
           "tbl-common-Css",
           "hdr-common-Css",
           "row-common-Css");

    }
    [WebMethod]
    public string asyncGet_PendingWorkList(int UserID)
    {
        UDCHyperLink alink = new UDCHyperLink();
        alink.Target = "_blank";
        alink.NaviagteURL = "../Technical/worklist/ViewJob.aspx";
        alink.QueryStringDataColumn = new string[] { "OFFICE_ID", "VESSEL_ID", "WORKLIST_ID" };
        alink.QueryStringText = new string[] { "OFFID", "VID", "WLID" };

        Dictionary<string, UDCHyperLink> dicLink = new Dictionary<string, UDCHyperLink>();
        dicLink.Add("strJobDescription", alink);

        UDCToolTip TP = new UDCToolTip("strJobDescription", "JOB_DESCRIPTION", false);
        Dictionary<string, UDCToolTip> dicToolTip = new Dictionary<string, UDCToolTip>();
        dicToolTip.Add("strJobDescription", TP);


        return UDFLib.CreateHtmlTableFromDataTable(BLL_Dashboard.Dash_Get_Pending_WorkList(UserID),
           new string[] { "Vessel", "Job Code", "Job Details", "Est. Comp. Date" },
           new string[] { "Vessel_Short_Name", "WLID_DISPLAY", "strJobDescription", "strEstCompldate" },
           dicLink,
           dicToolTip,
           new string[] { "left" },
           "tbl-common-Css",
           "hdr-common-Css",
           "row-common-Css");
    }
    [WebMethod]
    public string asyncGet_PendingWorkListVerification()
    {
        UDCHyperLink alink = new UDCHyperLink("", "../Operations/TaskPlanner/TaskIndex.aspx?ViewPendingVerification=1", new string[] { }, new string[] { }, "");
        Dictionary<string, UDCHyperLink> dicLink = new Dictionary<string, UDCHyperLink>();
        dicLink.Add("TotalTask", alink);

        return UDFLib.CreateHtmlTableFromDataTable(BLL_Dashboard.Dash_Get_Pending_TotalWorkListToVerify(),
          new string[] { "No of tasks pending for verification" },
          new string[] { "TotalTask" },
          dicLink,
          new Dictionary<string, UDCToolTip>(),
          new string[] { "left" },
          "tbl-common-Css",
          "hdr-common-Css",
          "row-common-Css");



    }
    [WebMethod]
    public string getPendingCrewBriefingList(int UserID, string DateFormat)
    {
        UDCHyperLink formBrieflink = new UDCHyperLink("", "../crew/CrewBriefing.aspx", new string[] { "ID", "CrewID" }, new string[] { "ID", "CrewID" }, "");
        UDCHyperLink formStaffcodelink = new UDCHyperLink("", "../crew/CrewDetails.aspx", new string[] { "ID" }, new string[] { "CrewID" }, "");
        Dictionary<string, UDCHyperLink> dickLink = new Dictionary<string, UDCHyperLink>();
        dickLink.Add("Briefing_Name", formBrieflink);
        dickLink.Add("Staff_Code", formStaffcodelink);

        DataTable dt = BLL_Dashboard.Dash_GetPendingBriefingList(UserID);
        foreach (DataRow row in dt.Rows)
            row["InterviewPlanDate_Time"] = UDFLib.ConvertUserDateFormatTime(Convert.ToString(row["InterviewPlanDate_Time"]), DateFormat);

        return UDFLib.CreateHtmlTableFromDataTable(dt,
                new string[] { "Staff Code", "Candidate Name", "Rank", "Briefing Name", "Planned Date" },
                new string[] { "Staff_Code", "CandidateName", "Rank_Short_Name", "Briefing_Name", "InterviewPlanDate_Time" },
                dickLink, new Dictionary<string, UDCToolTip>(), new string[] { "left", "center", "left" },
                "tbl-common-Css", "hdr-common-Css", "row-common-Css");

    }
    [WebMethod]
    public string getPendingInterviewList(int UserID, string DateFormat)
    {
        UDCHyperLink alink = new UDCHyperLink("", "../crew/CrewDetails.aspx", new string[] { "ID" }, new string[] { "CrewID" }, "");
        Dictionary<string, UDCHyperLink> dicLink = new Dictionary<string, UDCHyperLink>();
        dicLink.Add("CandidateName", alink);

        DataTable dt = BLL_Dashboard.Dash_getPendingInterviewList(UserID);
        foreach (DataRow row in dt.Rows)
            row["InterviewPlanDate_Time"] = UDFLib.ConvertUserDateFormatTime(Convert.ToString(row["InterviewPlanDate_Time"]), DateFormat);

        return UDFLib.CreateHtmlTableFromDataTable(dt,
          new string[] { "Candidate Name", "Applied Rank", "Interviewer", "Planned Date" },
          new string[] { "CandidateName", "AppliedRank", "Interviewer", "InterviewPlanDate_Time" },
          dicLink,
          new Dictionary<string, UDCToolTip>(),
          new string[] { "left", "center", "left", "left" },
          "tbl-common-Css",
          "hdr-common-Css",
          "row-common-Css");
        ;

    }
    [WebMethod]
    public string getPendingInterviewList_By_UserID(int UserID, string DateFormat)
    {
        DataTable dt = BLL_Dashboard.Dash_getPendingInterviewListt_By_UserID(UserID);
        foreach (DataRow row in dt.Rows)
            row["InterviewPlanDate_Time"] = UDFLib.ConvertUserDateFormatTime(Convert.ToString(row["InterviewPlanDate_Time"]), DateFormat);

        UDCHyperLink alink = new UDCHyperLink("", "../crew/CrewDetails.aspx", new string[] { "ID" }, new string[] { "CrewID" }, "");
        Dictionary<string, UDCHyperLink> dicLink = new Dictionary<string, UDCHyperLink>();
        dicLink.Add("CandidateName", alink);

        return UDFLib.CreateHtmlTableFromDataTable(dt,
          new string[] { "Candidate Name", "Applied Rank", "Interviewer", "Planned Date" },
          new string[] { "CandidateName", "AppliedRank", "Interviewer", "InterviewPlanDate_Time" },
          dicLink,
          new Dictionary<string, UDCToolTip>(),
          new string[] { "left", "center", "left", "left" },
          "tbl-common-Css",
          "hdr-common-Css",
          "row-common-Css");


    }
    [WebMethod]
    public string getWorklist_DueIn_7Days()
    {
        UDCHyperLink alink = new UDCHyperLink();
        alink.Target = "_blank";
        alink.NaviagteURL = "../Technical/worklist/ViewJob.aspx";
        alink.QueryStringDataColumn = new string[] { "OFFICE_ID", "VESSEL_ID", "WORKLIST_ID" };
        alink.QueryStringText = new string[] { "OFFID", "VID", "WLID" };

        Dictionary<string, UDCHyperLink> diclink = new Dictionary<string, UDCHyperLink>();

        diclink.Add("JOB_DESCRIPTION", alink);

        return UDFLib.CreateHtmlTableFromDataTable(BLL_Dashboard.Dash_Get_WorkList_DueIn_7Days(),
                new string[] { "Vessel", "Code", "Job Description", "Date Raised", "Expected Compln" },
                new string[] { "VESSEL_SHORT_NAME", "WLID_DISPLAY", "JOB_DESCRIPTION", "DATE_RAISED", "DATE_ESTMTD_CMPLTN" },
                diclink,
                new Dictionary<string, UDCToolTip>(),
                new string[] { },
                "tbl-common-Css",
                "hdr-common-Css",
                "row-common-Css");
    }
    [WebMethod]
    public string asyncGet_Pending_CTM_Approval(int User_ID, string DateFormat)
    {
        UDCHyperLink aLink = new UDCHyperLink("", "../portagebill/CTMIndex.aspx?CTMAPPROVAL=1", new string[] { }, new string[] { }, "_blank");
        Dictionary<string, UDCHyperLink> dicLink = new Dictionary<string, UDCHyperLink>();
        dicLink.Add("Vessel_Name", aLink);

        DataTable dt = BLL_Dashboard.Dash_Get_Pending_CTM_Approval(User_ID);
        foreach (DataRow row in dt.Rows)
            row["Received_On"] = UDFLib.ConvertUserDateFormat(Convert.ToString(row["Received_On"]), DateFormat);

        return UDFLib.CreateHtmlTableFromDataTable(dt,
           new string[] { "Vessel Name", "Received On" },
           new string[] { "Vessel_Name", "Received_On" },
           dicLink,
            new Dictionary<string, UDCToolTip>(),
           new string[] { "center", "left" },
           "tbl-common-Css",
           "hdr-common-Css",
           "row-common-Css");
    }
    [WebMethod]
    public string asyncGet_CTM_Confirmation_Not_Received(int User_ID, string DateFormat)
    {
        UDCHyperLink aLink = new UDCHyperLink("", "../portagebill/CTMRequest.aspx", new string[] { "ID", "Vessel_ID" }, new string[] { "ID", "Vessel_ID" }, "_blank");
        Dictionary<string, UDCHyperLink> dicLink = new Dictionary<string, UDCHyperLink>();
        dicLink.Add("Vessel_Name", aLink);

        DataTable dt = BLL_Dashboard.Dash_Get_CTM_Confirmation_Not_Received(User_ID);
        foreach (DataRow row in dt.Rows)
            row["DateOfApproval"] = UDFLib.ConvertUserDateFormat(Convert.ToString(row["DateOfApproval"]), DateFormat);

        return UDFLib.CreateHtmlTableFromDataTable(dt,
           new string[] { "Vessel Name", "Approved On", "Amount" },
           new string[] { "Vessel_Name", "DateOfApproval", "ApprovedAmt" },
           dicLink,
            new Dictionary<string, UDCToolTip>(),
           new string[] { "left", "left", "Right" },
           "tbl-common-Css",
           "hdr-common-Css",
           "row-common-Css");
    }
    [WebMethod]
    public string Get_OpsWorklistDueIn7Days(int UserID)
    {
        return UDFLib.CreateHtmlTableFromDataTable(BLL_Dashboard.Dash_Get_OpsWorklistDueIn7Days(UserID),
             new string[] { "Vessel", "Task", "Expected Completion", "Created By" },
             new string[] { "VESSEL_SHORT_NAME", "JOB_DESCRIPTION", "DATE_ESTMTD_CMPLTN", "Created_By_Name" },

             new string[] { "left", "left", "left", "left" },
            "tbl-common-Css",
               "hdr-common-Css",
               "row-common-Css");
    }
    [WebMethod]
    public string Get_Surv_DueinNext30Days(int UserID, string DateFormat)
    {
        DataTable dt = BLL_Dashboard.Dash_Get_Surv_DueinNext30Days(UserID);
        foreach (DataRow row in dt.Rows)
        {
            row["CALCULATEDEXPIRYDATE"] = UDFLib.ConvertUserDateFormat(Convert.ToString(row["CALCULATEDEXPIRYDATE"]), DateFormat);
            row["DATEOFISSUE"] = UDFLib.ConvertUserDateFormat(Convert.ToString(row["DATEOFISSUE"]), DateFormat);
            row["DATEOFEXPIRY"] = UDFLib.ConvertUserDateFormat(Convert.ToString(row["DATEOFEXPIRY"]), DateFormat);
        }
        return UDFLib.CreateHtmlTableFromDataTable(dt,
             new string[] { "Vessel", "Category", "Survey/Certificate Name", "Expiry Date", "Calc Exp Date" },
             new string[] { "VESSEL_SHORT_NAME", "SURVEY_CATEGORY", "SURVEY_CERT_NAME", "DATEOFEXPIRY", "CALCULATEDEXPIRYDATE" },

             new string[] { "left", "left", "left", "left", "left" },
            "tbl-common-Css",
               "hdr-common-Css",
               "row-common-Css");
    }
    [WebMethod]
    public string Get_Surv_DueinNext7DaysAndOverdue(int UserID, string DateFormat)
    {
        DataTable dt = BLL_Dashboard.Dash_Get_Surv_DueinNext7DaysAndOverdue(UserID);
        foreach (DataRow row in dt.Rows)
        {
            row["CALCULATEDEXPIRYDATE"] = UDFLib.ConvertUserDateFormat(Convert.ToString(row["CALCULATEDEXPIRYDATE"]), DateFormat);
            row["DATEOFISSUE"] = UDFLib.ConvertUserDateFormat(Convert.ToString(row["DATEOFISSUE"]), DateFormat);
            row["DATEOFEXPIRY"] = UDFLib.ConvertUserDateFormat(Convert.ToString(row["DATEOFEXPIRY"]), DateFormat);
        }
        return UDFLib.CreateHtmlTableFromDataTable(dt,
             new string[] { "Vessel", "Category", "Survey/Certificate Name", "Expiry Date", "Calc Exp Date" },
             new string[] { "VESSEL_SHORT_NAME", "SURVEY_CATEGORY", "SURVEY_CERT_NAME", "DATEOFEXPIRY", "CALCULATEDEXPIRYDATE" },

             new string[] { "left", "left", "left", "left", "left" },
            "tbl-common-Css",
               "hdr-common-Css",
               "row-common-Css");
    }
    [WebMethod]
    public string Get_Surv_PendingVerification(int UserID, string DateFormat)
    {
        DataTable dt = BLL_Dashboard.Dash_Get_Surv_PendingVerification(UserID);
        foreach (DataRow row in dt.Rows)
        {
            row["CALCULATEDEXPIRYDATE"] = UDFLib.ConvertUserDateFormat(Convert.ToString(row["CALCULATEDEXPIRYDATE"]), DateFormat);
            row["DATEOFISSUE"] = UDFLib.ConvertUserDateFormat(Convert.ToString(row["DATEOFISSUE"]), DateFormat);
            row["DATEOFEXPIRY"] = UDFLib.ConvertUserDateFormat(Convert.ToString(row["DATEOFEXPIRY"]), DateFormat);
        }
        return UDFLib.CreateHtmlTableFromDataTable(dt,
             new string[] { "Vessel", "Category", "Survey/Certificate Name", "Expiry Date", "Calc Exp Date" },
             new string[] { "VESSEL_SHORT_NAME", "SURVEY_CATEGORY", "SURVEY_CERT_NAME", "DATEOFEXPIRY", "CALCULATEDEXPIRYDATE" },

             new string[] { "left", "left", "left", "left", "left" },
            "tbl-common-Css",
               "hdr-common-Css",
               "row-common-Css");
    }
    [WebMethod]
    public string Get_Surv_NA_PendingVerification(int UserID, string DateFormat)
    {
        DataTable dt = BLL_Dashboard.Dash_Get_Surv_NA_PendingVerification(UserID);
        foreach (DataRow row in dt.Rows)
        {
            row["CALCULATEDEXPIRYDATE"] = UDFLib.ConvertUserDateFormat(Convert.ToString(row["CALCULATEDEXPIRYDATE"]), DateFormat);
            row["DATEOFISSUE"] = UDFLib.ConvertUserDateFormat(Convert.ToString(row["DATEOFISSUE"]), DateFormat);
            row["DATEOFEXPIRY"] = UDFLib.ConvertUserDateFormat(Convert.ToString(row["DATEOFEXPIRY"]), DateFormat);
            row["NA_MARKED_DATE"] = UDFLib.ConvertUserDateFormat(Convert.ToString(row["NA_MARKED_DATE"]), DateFormat);
        }
        return UDFLib.CreateHtmlTableFromDataTable(dt,
             new string[] { "Vessel", "Category", "Survey/Certificate Name", "Expiry Date", "Calc Exp Date" },
             new string[] { "VESSEL_SHORT_NAME", "SURVEY_CATEGORY", "SURVEY_CERT_NAME", "DATEOFEXPIRY", "CALCULATEDEXPIRYDATE" },

             new string[] { "left", "left", "left", "left", "left" },
            "tbl-common-Css",
               "hdr-common-Css",
               "row-common-Css");
    }
    [WebMethod]
    public string Get_Surv_ExpDateBydayCount(int UserID, string ExpairyFromDaysCount, string ExpairyToDaysCount, string DateFormat)
    {
        DataTable dt = BLL_Dashboard.Dash_Get_Surv_ExpDateBydayCount(UserID, ExpairyFromDaysCount, ExpairyToDaysCount);
        foreach (DataRow row in dt.Rows)
        {
            row["CALCULATEDEXPIRYDATE"] = UDFLib.ConvertUserDateFormat(Convert.ToString(row["CALCULATEDEXPIRYDATE"]), DateFormat);
            row["DATEOFISSUE"] = UDFLib.ConvertUserDateFormat(Convert.ToString(row["DATEOFISSUE"]), DateFormat);
            row["DATEOFEXPIRY"] = UDFLib.ConvertUserDateFormat(Convert.ToString(row["DATEOFEXPIRY"]), DateFormat);
        }
        UDCHyperLink alink = new UDCHyperLink();
        alink.Target = "_blank";
        alink.NaviagteURL = "../Surveys/SurveyDetails.aspx";
        alink.QueryStringDataColumn = new string[] { "VESSEL_ID", "SURV_VESSEL_ID", "OFFICEID", "SURV_DETAILS_ID" };
        alink.QueryStringText = new string[] { "VESSEL_ID", "SURV_VESSEL_ID", "OFFICEID", "SURV_DETAILS_ID" };
        Dictionary<string, UDCHyperLink> dicLink = new Dictionary<string, UDCHyperLink>();
        dicLink.Add("SURVEY_CERT_NAME", alink);
        return UDFLib.CreateHtmlTableFromDataTable(dt,
             new string[] { "Vessel", "Category", "Survey/Certificate Name", "Expiry Date", "Calc Exp Date" },
             new string[] { "VESSEL_SHORT_NAME", "SURVEY_CATEGORY", "SURVEY_CERT_NAME", "DATEOFEXPIRY", "CALCULATEDEXPIRYDATE" },
             dicLink,
            new Dictionary<string, UDCToolTip>(),
              new string[] { "left", "left", "center", "left", "left" },
              "tbl-common-Css",
                "hdr-common-Css",
                "row-common-Css");
    }
    [WebMethod]
    public string Get_PMS_Overdue_Job(int? User_ID)
    {

        UDCHyperLink aLink = new UDCHyperLink("", "../Technical/PMS/PMSJobProcess.aspx", new string[] { "Vessel_ID", "OverDueSearchFlage" }, new string[] { "Vessel_ID", "OverDueSearchFlage" }, "_blank");
        Dictionary<string, UDCHyperLink> dicLink = new Dictionary<string, UDCHyperLink>();
        dicLink.Add("VESSEL_NAME", aLink);


        return UDFLib.CreateHtmlTableFromDataTable(BLL_Dashboard.Dash_Get_PMS_Overdue_Job_Count(User_ID),
               new string[] { "Vessel Name", "Total Overdue Jobs" },
               new string[] { "VESSEL_NAME", "TOTALJOB" },
               dicLink,
               new Dictionary<string, UDCToolTip>(),
               new string[] { "center", "center" },
               "tbl-common-Css",
               "hdr-common-Css",
               "row-common-Css");


    }
    [WebMethod]
    public string asyncGet_Cylinder_Oil_Consumption(int UserID)
    {

        UDCHyperLink aLink = new UDCHyperLink("", "../Operations/NoonReport.aspx", new string[] { "id" }, new string[] { "PKID" }, "_blank");
        Dictionary<string, UDCHyperLink> dicLink = new Dictionary<string, UDCHyperLink>();
        dicLink.Add("Report_Date", aLink);

        return UDFLib.CreateHtmlTableFromDataTable(BLL_Dashboard.Dash_Get_Cylinder_Oil_Consumption(UserID),
           new string[] { "Vessel", "Date", "Location", "Eng RPM", "MECYL", "KW" },
           new string[] { "Vessel_Name", "Report_Date", "Location_Name", "EngRPM", "MECYL_Cons", "ME_KW" },
           dicLink,
            new Dictionary<string, UDCToolTip>(),
           new string[] { "left", "left", "left", "center", "center", "center" },
           "tbl-common-Css",
           "hdr-common-Css",
           "row-common-Css");

    }
    [WebMethod]
    public string asyncGet_Events_Done(int User_ID)
    {
        return BLL_Dashboard.Dash_Get_Events_Done(User_ID);
    }
    [WebMethod]
    public string asyncGet_OverDue_Inspection(int UserID)
    {
        Dictionary<string, UDCHyperLink> dicLink = new Dictionary<string, UDCHyperLink>();

        return UDFLib.CreateHtmlTableFromDataTable(BLL_Dashboard.Dash_Get_OverDue_Inspection(UserID),
           new string[] { "Vessel", "Inspection Date", "Inspector Name" },
           new string[] { "Vessel_Name", "ScheduleDate", "InspectorName" },
           dicLink,
            new Dictionary<string, UDCToolTip>(),
           new string[] { "left", "center", "left" },
           "tbl-common-Css",
           "hdr-common-Css",
           "row-common-Css");

    }
    [WebMethod]
    public string Get_CrewListByPerformance(int UserID)
    {
        DataSet ds = BLL_Dashboard.Dash_Get_CrewPerformance(UserID);

        UDCHyperLink alink = new UDCHyperLink("", "../crew/CrewDetails.aspx", new string[] { "ID" }, new string[] { "CrewID" }, "");
        UDCHyperLink alinkView = new UDCHyperLink("", "../CrewEvaluation/CrewEvaluations.aspx", new string[] { "CrewID" }, new string[] { "CrewID" }, "");
        Dictionary<string, UDCHyperLink> dicLink = new Dictionary<string, UDCHyperLink>();
        dicLink.Add("STAFF_CODE", alink);
        dicLink.Add("View", alinkView);

        List<UDCAction> dicAction = new List<UDCAction>();
        dicAction.Add(new UDCAction(new string[][] { new string[] { "AddNewFeedback", "onclick" } }, "../Images/Plus.gif", new string[] { "CrewID" }));
        dicAction.Add(new UDCAction(new string[][] { new string[] { "RequestFeedback", "onclick" } }, "../Images/feedback1.png", new string[] { "STAFF_CODE" }));

        UDCToolTip TP = new UDCToolTip("Average_Percentage", "AVG_PER", false);
        Dictionary<string, UDCToolTip> dicToolTip = new Dictionary<string, UDCToolTip>();
        dicToolTip.Add("Average_Percentage", TP);


        string YellowCards = UDFLib.CreateHtmlTableFromDataTable(ds.Tables[0],
            new string[] { "Staff Code", "Staff Name", "Rank", "Vessel", "", "Eval(%)", "" },
            new string[] { "STAFF_CODE", "STAFF_FULLNAME", "RANK", "VESSEL_NAME", "VoyageCount", "AVG_PER", "View" },
            dicLink,
           new Dictionary<string, UDCToolTip>(),
            new string[] { "left", "left", "left", "left", "left", "left" },
            "tbl-common-Css",
            "hdr-common-Css",
            "row-common-Css");

        string Bottom25Percent = UDFLib.CreateHtmlTableFromDataTable(ds.Tables[1],
            new string[] { "Staff Code", "Staff Name", "", "", "Rank", "Vessel", "", "Eval(%)", "" },
            new string[] { "STAFF_CODE", "STAFF_FULLNAME", "ActionCount", "PendingEvaluationCount", "RANK", "VESSEL_NAME", "VoyageCount", "AVG_PER", "View" },
            dicLink,
            new Dictionary<string, UDCToolTip>(),
            new string[] { "left", "left", "left", "left", "left", "left" },
            "tbl-common-Css",
            "hdr-common-Css",
            "row-common-Css");

        string Top15Percent = UDFLib.CreateHtmlTableFromDataTable(ds.Tables[2],
            new string[] { "Staff Code", "Staff Name", "Rank", "Vessel", "", "Eval(%)", "" },
            new string[] { "STAFF_CODE", "STAFF_FULLNAME", "RANK", "VESSEL_NAME", "VoyageCount", "AVG_PER", "View" },
            dicLink,
            new Dictionary<string, UDCToolTip>(),
            new string[] { "left", "left", "left", "left", "left", "left" },
            "tbl-common-Css",
            "hdr-common-Css",
            "row-common-Css");

        return "<div class='section-header'>Bottom 25 %:</div>" + Bottom25Percent + "<div class='section-header'>Staff with Yellow/Red Card:</div>" + YellowCards + " <div class='section-header'>Top 25 %:</div>" + Top15Percent;
    }
    [WebMethod]
    public string Get_CrewEvaluationDueList(int UserID, string DateFormat)
    {
        UDCHyperLink alink = new UDCHyperLink("", "../crew/CrewDetails.aspx", new string[] { "ID" }, new string[] { "CrewID" }, "");
        Dictionary<string, UDCHyperLink> dicLink = new Dictionary<string, UDCHyperLink>();
        dicLink.Add("STAFF_CODE", alink);

        DataTable dt = BLL_Dashboard.Dash_Get_CrewEvaluationDue(UserID).Tables[0];

        foreach (DataRow row in dt.Rows)
            row["DueDate"] = UDFLib.ConvertUserDateFormat(Convert.ToString(row["DueDate"]), DateFormat);

        return UDFLib.CreateHtmlTableFromDataTable(dt,
                new string[] { "Staff Code", "Staff Name", "Rank", "Vessel", "Due Date" },
                new string[] { "STAFF_CODE", "STAFF_FULLNAME", "RANK", "VESSEL_NAME", "DueDate" },
                dicLink,
                new Dictionary<string, UDCToolTip>(),
                new string[] { "left", "left", "left", "left", "left" },
                 "tbl-common-Css",
                  "hdr-common-Css",
                  "row-common-Css");



    }
    [WebMethod]
    public string Get_CrewListByPerformanceVerification(int UserID)
    {
        DataSet ds = BLL_Dashboard.Dash_Get_CrewPerformanceVerification(UserID);

        UDCHyperLink alink = new UDCHyperLink("", "../crew/CrewDetails.aspx", new string[] { "ID" }, new string[] { "CrewID" }, "");
        UDCHyperLink alinkVerify = new UDCHyperLink("", "../CrewEvaluation/DoEvaluation.aspx", new string[] { "CrewID", "EID", "DTLID", "M", "SchID" }, new string[] { "CrewID", "Evaluation_ID", "DTID", "DueDate", "Schedule_ID" }, "");
        Dictionary<string, UDCHyperLink> dicLink = new Dictionary<string, UDCHyperLink>();
        dicLink.Add("STAFF_CODE", alink);
        dicLink.Add("Verify", alinkVerify);





        UDCToolTip TP = new UDCToolTip("Average_Percentage", "AVG_PER", false);
        Dictionary<string, UDCToolTip> dicToolTip = new Dictionary<string, UDCToolTip>();
        dicToolTip.Add("Average_Percentage", TP);



        string Bottom15Percent = UDFLib.CreateHtmlTableFromDataTable(ds.Tables[0],
            new string[] { "Staff Code", "Staff Name", "Rank", "Vessel", "Eval(%)", "" },
            new string[] { "STAFF_CODE", "STAFF_FULLNAME", "Rank_Short_Name", "VESSEL_NAME", "AVG_PER", "Verify" },
            dicLink,
            new Dictionary<string, UDCToolTip>(),
            new string[] { "left", "left", "left", "left", "left", "left" },
            "tbl-common-Css",
            "hdr-common-Css",
            "row-common-Css");

        string percentage = "";
        if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
        {
            percentage = "<div class='section-header'> Bottom " + ds.Tables[0].Rows[0]["Percentage"] + "%:</div>";
        }

        return percentage + Bottom15Percent;
    }
    [WebMethod]
    public string asyncGet_DueInMonth_Inspection(int UserID)
    {

        //UDCHyperLink aLink = new UDCHyperLink("", "../Operations/NoonReport.aspx", new string[] { "id" }, new string[] { "PKID" }, "_blank");
        Dictionary<string, UDCHyperLink> dicLink = new Dictionary<string, UDCHyperLink>();
        //dicLink.Add("Report_Date", aLink);

        return UDFLib.CreateHtmlTableFromDataTable(BLL_Dashboard.Dash_Get_DueInMonth_Inspection(UserID),
           new string[] { "Vessel", "Inspection Schedule Date", "Inspector Name" },
           new string[] { "Vessel_Name", "ScheduleDate", "InspectorName" },
           dicLink,
            new Dictionary<string, UDCToolTip>(),
           new string[] { "left", "center", "left" },
           "tbl-common-Css",
           "hdr-common-Css",
           "row-common-Css");

    }
    public class Portcalls_Vessel
    {

        public string VesselCode { get; set; }

        public int? PortCount { get; set; }

    }
    [WebMethod]
    public List<Portcalls_Vessel> AsyncGet_PortCallsVessel(int User_ID)
    {
        DataTable dt_Vessel = BLL_Dashboard.Dash_Get_Portcalls_Vessel(User_ID);
        List<Portcalls_Vessel> dataList = new List<Portcalls_Vessel>();

        foreach (DataRow dtrow in dt_Vessel.Rows)
        {

            Portcalls_Vessel details = new Portcalls_Vessel();

            details.VesselCode = dtrow[3].ToString();

            details.PortCount = UDFLib.ConvertIntegerToNull(dtrow[0]);

            dataList.Add(details);

        }
        return dataList;
    }
    public class Portcalls_Month
    {

        public string header { get; set; }

        public int? PortCount { get; set; }

    }
    [WebMethod]
    public List<Portcalls_Month> AsyncGet_PortCallsMonth(int User_ID)
    {
        DataTable dt_Month = BLL_Dashboard.Dash_Get_Portcalls_Month(User_ID);
        List<Portcalls_Month> dataList = new List<Portcalls_Month>();

        foreach (DataRow dtrow in dt_Month.Rows)
        {

            Portcalls_Month details = new Portcalls_Month();

            details.header = dtrow[0].ToString();

            details.PortCount = UDFLib.ConvertIntegerToNull(dtrow[4]);

            dataList.Add(details);

        }
        return dataList;
    }
    [WebMethod]
    public string asyncGet_Completed_Inspection(int UserID)
    {
        UDCHyperLink alink = new UDCHyperLink();
        alink.Target = "_blank";
        alink.NaviagteURL = "../Technical/worklist/SupdtInspReport.aspx";
        alink.QueryStringDataColumn = new string[] { "SchDetailId", "ShowImages" };
        alink.QueryStringText = new string[] { "SchDetailId", "ShowImages" };

        Dictionary<string, UDCHyperLink> diclink = new Dictionary<string, UDCHyperLink>();

        diclink.Add("Report", alink);

        return UDFLib.CreateHtmlTableFromDataTable(BLL_Dashboard.Dash_Get_Completed_Inspection(UserID),
           new string[] { "Vessel", "Inspection Type", "Date", "View Report", "Pending<br>(Non-NCR)", "Pending<br>(NCR)" },/*, "Pending(Non-NCR)", "Pending(NCR)"*/
           new string[] { "Vessel_Name", "InspectionTypeName", "ActualDate", "Report", "PendingNonNCRJobs", "PendingNCRJobs" },/*, "PendingNonNCRJobs", "PendingNCRJobs"*/
           diclink,
            new Dictionary<string, UDCToolTip>(),
           new string[] { "left", "center", "left" },
           "tbl-common-Css",
           "hdr-common-Css",
           "row-common-Css rowHeight");

    }
    [WebMethod]
    public string asyncGet_InvItems_BelowTreshold(int UserID)
    {
        UDCHyperLink alink = new UDCHyperLink();
        alink.Target = "_blank";
        alink.NaviagteURL = "../purchase/RequisitionSummary.aspx";
        alink.QueryStringDataColumn = new string[] { "REQUISITION_CODE", "DOCUMENT_CODE", "Vessel_Code", "DEPARTMENT", "hold" };
        alink.QueryStringText = new string[] { "REQUISITION_CODE", "Document_Code", "Vessel_Code", "Dept_Code", "hold" };

        Dictionary<string, UDCHyperLink> diclink = new Dictionary<string, UDCHyperLink>();

        diclink.Add("REQUISITION_CODE", alink);

        return UDFLib.CreateHtmlTableFromDataTable(BLL_Dashboard.Dash_Get_InvItems_BelowTreshold(UserID),
           new string[] { "Vessel", "Item Name", "Treshold(Min/Max)", "Current ROB", "Requisition No." },
           new string[] { "Vessel_Short_Name", "Short_Description", "TresholdLimits", "Inventory_Qty", "REQUISITION_CODE" },
           diclink,
            new Dictionary<string, UDCToolTip>(),
           new string[] { "left", "left", "center", "center", "left" },
           "tbl-common-Css",
           "hdr-common-Css",
           "row-common-Css");

    }
    [WebMethod]
    public string GetPendingCardApprovalList(int UserID, string DateFormat)
    {
        Dictionary<string, UDCHyperLink> diclink = new Dictionary<string, UDCHyperLink>();
        UDCHyperLink alinkCrew = new UDCHyperLink("", "../crew/CrewDetails.aspx", new string[] { "ID" }, new string[] { "CrewID" }, "");
        DataSet ds = BLL_Dashboard.Dash_GetPendingCardApprovalList(UserID);
        DataTable dt1 = ds.Tables[0];
        foreach (DataRow row in dt1.Rows)
            row["Date_Of_Creation"] = UDFLib.ConvertUserDateFormat(Convert.ToString(row["Date_Of_Creation"]), DateFormat);
        DataTable dt2 = ds.Tables[1];
        foreach (DataRow row in dt2.Rows)
            row["Date_Of_Creation"] = UDFLib.ConvertUserDateFormat(Convert.ToString(row["Date_Of_Creation"]), DateFormat);

        diclink.Add("Staff_Code", alinkCrew);

        string PendingYellowCardApproval = UDFLib.CreateHtmlTableFromDataTable(dt1,
           new string[] { "Staff Code", "Crew", "Proposed Date", "Proposed By", "Approve" },
           new string[] { "Staff_Code", "CrewName", "Date_Of_Creation", "ProposedBy", "Approve" },
           diclink,
            new Dictionary<string, UDCToolTip>(),
           new string[] { "left", "center", "left" },
           "tbl-common-Css",
           "hdr-common-Css",
           "row-common-Css");

        string PendingRedCardApproval = UDFLib.CreateHtmlTableFromDataTable(dt2,
          new string[] { "Staff Code", "Crew", "Proposed Date", "Proposed By", "Approve" },
          new string[] { "Staff_Code", "CrewName", "Date_Of_Creation", "ProposedBy", "Approve" },
          diclink,
           new Dictionary<string, UDCToolTip>(),
          new string[] { "left", "center", "left" },
          "tbl-common-Css",
          "hdr-common-Css",
          "row-common-Css");

        return "<div class='section-header'>Pending Red Card Approval:</div>" + PendingRedCardApproval + "<div class='section-header'>Pending Yellow Card Approval:</div>" + PendingYellowCardApproval;
    }
    [WebMethod]
    public string GetPendingSupplierApprovalList(int UserID)
    {
        UDCHyperLink alink = new UDCHyperLink();
        alink.Target = "_blank";
        alink.NaviagteURL = "../ASL/ASL_Data_Index.aspx";
        alink.QueryStringDataColumn = new string[] { "Supplier_Code", "Supplier_Name", "Proposed_By" };
        alink.QueryStringText = new string[] { "Supplier_Code", "Supplier_Name", "Proposed_By" };

        Dictionary<string, UDCHyperLink> diclink = new Dictionary<string, UDCHyperLink>();
        DataTable dt = BLL_Dashboard.Dash_Get_Supplier_Evaluation_Search(UDFLib.ConvertIntegerToNull(UserID));
        //DataTable dtinfo = DAL_Infra_Common.Get_Deck_Record_Information(Table_Name, Where);
        //DataTable ds = BLL_ASL_Supplier.Get_Supplier_Eval_Search(UDFLib.ConvertIntegerToNull(UserID));
        diclink.Add("Supplier_Name", alink);

        return UDFLib.CreateHtmlTableFromDataTable(dt,
           new string[] { "Supplier Name", "Supplier Type", "Propose Status", "Days", "Proposed By", "For Approval", "For Final Approval", "Urgent" },
           new string[] { "Supplier_Name", "Supplier_Type", "Propose_Status", "For_Period", "Proposed_By", "ForApproval", "ForFinalApproval", "Urgent_Flag" },
           diclink,
            new Dictionary<string, UDCToolTip>(),
           new string[] { "left", "center", "left" },
           "tbl-common-Css",
           "hdr-common-Css",
           "row-common-Css");
    }
    [WebMethod]
    public string GetPendingInvoiceApproval(int UserID)
    {
        UDCHyperLink alink = new UDCHyperLink();

        alink.QueryStringDataColumn = new string[] { "Login_Name" };
        alink.QueryStringText = new string[] { "Login_Name" };

        Dictionary<string, UDCHyperLink> diclink = new Dictionary<string, UDCHyperLink>();
        DataSet ds = BLL_Dashboard.Dash_Get_Pending_Invoice_Approval(UDFLib.ConvertIntegerToNull(UserID));


        return UDFLib.CreateHtmlTableFromDataTable(ds.Tables[0],
           new string[] { "Verification", "Approval", "Rework", "Advance", "Overdue" },
           new string[] { "Pending_Verification", "Pending_Approval", "Pending_Rework", "Pending_Advance", "Pending_Urgent" },
           diclink,
            new Dictionary<string, UDCToolTip>(),
           new string[] { "center", "center", "left" },
           "tbl-common-Css",
           "hdr-common-Css",
           "row-common-Css");
    }
    [WebMethod]
    public string Get_Opex_Vessel_Report(int UserID)
    {
        UDCHyperLink alink = new UDCHyperLink();
        StringBuilder strTable = new StringBuilder();


        alink.QueryStringDataColumn = new string[] { "VESSEL_NAME" };
        alink.QueryStringText = new string[] { "VESSEL_NAME" };

        Dictionary<string, UDCHyperLink> diclink = new Dictionary<string, UDCHyperLink>();
        DataTable dtFleet = BLL_Dashboard.Dash_Get_Opex_Fleet_Details(UDFLib.ConvertIntegerToNull(UserID));

        if (dtFleet.Rows.Count > 0)
        {
            if (dtFleet.Rows[0]["Name"].ToString() != "False")
            {
                strTable.Append("<table class='tbl-css-dash' CELLPADDING='0' CELLSPACING='1'  >");


                if (dtFleet.Columns.Count > 0)
                {
                    strTable.Append("<tr >");
                    for (int i = 1; i < dtFleet.Columns.Count; i++)
                    {
                        strTable.Append("<th class='hdr-common-Css'  width='auto' >");
                        strTable.Append("<b>" + dtFleet.Columns[i].ToString() + "</b>");
                        strTable.Append("</th>");
                    }
                    strTable.Append("</tr>");
                }
                for (int j = 0; j < dtFleet.Rows.Count; j++)
                {
                    strTable.Append("<tr class='row-common-Css' style='color:white; background-color:yellow;font-weight :normal;' >");

                    strTable.Append("<th class='row-common-Css' style='text-align: left;'  width='auto' >");
                    strTable.Append("" + dtFleet.Rows[j][1].ToString() + "");
                    strTable.Append("</th>");
                    for (int i = 2; i < dtFleet.Columns.Count; i++)
                    {
                        strTable.Append("<th class='row-common-Css' style='text-align: right;'  width='auto' >");
                        strTable.Append("" + dtFleet.Rows[j][i].ToString() + "");
                        strTable.Append("</th>");
                    }
                    strTable.Append("</tr>");
                    DataSet dsVessel = BLL_Dashboard.Dash_Get_Opex_Vessel_Report(UDFLib.ConvertIntegerToNull(UserID), dtFleet.Rows[j][0].ToString());

                    if (dsVessel.Tables[0].Rows.Count > 0)
                    {
                        if (dsVessel.Tables[0].Rows[0]["VESSEL_NAME"].ToString() != "False")
                        {
                            for (int k = 0; k < dsVessel.Tables[0].Rows.Count; k++)
                            {
                                decimal Vessel_Avg = 0;
                                strTable.Append("<tr class='row-common-Css'>");
                                for (int t = 0; t < dsVessel.Tables[0].Columns.Count; t++)
                                {
                                    if (t > 0)
                                    {
                                        int Vessel_Point = Convert.ToInt16(dsVessel.Tables[1].Rows[k][t].ToString());
                                        Vessel_Avg = Convert.ToDecimal(dsVessel.Tables[2].Rows[k][t].ToString());
                                        if (Vessel_Point == 1)
                                        {
                                            strTable.Append("<th class='NoAnomaly' style='color:white;font-weight :normal;text-align: right;'  width='auto' >");
                                        }
                                        else if (Vessel_Point == 2)
                                        {
                                            strTable.Append("<th class='AnomalyCell' style='color:white;font-weight :normal;text-align: right;'   width='auto' >");
                                        }
                                        else
                                        {
                                            strTable.Append("<th class='AlternatingRowStyle-css-dash' style='font-weight :normal;text-align: right;'  width='auto' >");
                                        }

                                    }
                                    else
                                    {
                                        strTable.Append("<th class='AlternatingRowStyle-css-dash' style='font-weight :normal;'  width='auto' >");
                                    }
                                    if (Vessel_Avg == 0)
                                    {
                                        strTable.Append("" + dsVessel.Tables[0].Rows[k][t].ToString() + "");
                                    }
                                    else
                                    {
                                        strTable.Append("<table width='100%'>");
                                        strTable.Append("<tr>");
                                        strTable.Append("<td >"); strTable.Append("" + Vessel_Avg + "" + "/"); strTable.Append("<br />");
                                        strTable.Append("</td>");
                                        strTable.Append("</tr>");
                                        strTable.Append("<tr>");
                                        strTable.Append("<td>"); strTable.Append("" + "(" + dsVessel.Tables[0].Rows[k][t].ToString() + ")" + "");
                                        strTable.Append("</td>");
                                        strTable.Append("</tr>");
                                        strTable.Append("</table>");
                                    }
                                    strTable.Append("</th>");
                                }
                                strTable.Append("</tr>");
                            }
                        }
                    }

                }

                strTable.Append("</table>");
            }
            else
            {
                strTable.Append("No record found !");
            }
        }
        else
            strTable.Append("No record found !");

        return strTable.ToString();
    }
    [WebMethod]
    public string Get_CP_SnippetData(int UserID)
    {

        StringBuilder strTable = new StringBuilder();
        strTable.Append("<div><table CELLPADDING='2' CELLSPACING='0' width='100%'   style='border-collapse:collapse' >");
        strTable.Append("<tr >");
        strTable.Append("<th class='HeaderStyle-css-dash'  width='auto' >");
        strTable.Append("Vessel");
        strTable.Append("</th>");

        strTable.Append("<th class='HeaderStyle-css-dash'  width='auto' >");
        strTable.Append("Charterer");
        strTable.Append("</th>");
        strTable.Append("<th class='HeaderStyle-css-dash'  width='auto' >");
        strTable.Append("Rate");
        strTable.Append("</th>");
        strTable.Append("<th class='HeaderStyle-css-dash'  width='auto' >");
        strTable.Append("End TC");
        strTable.Append("</th>");
        strTable.Append("<th class='HeaderStyle-css-dash'  width='auto' >");
        strTable.Append("Hire Overdue");
        strTable.Append("</th>");
        strTable.Append("</tr>");

        DataTable dt = BLL_Dashboard.Dash_Get_CPSnippetData(UDFLib.ConvertToInteger(UserID));
        if (dt.Rows.Count > 0)
        {
            foreach (DataRow dr in dt.Rows)
            {
                strTable.Append("<tr class='RowStyle-css-dash' style='text-align: center;border-style: solid;border-width:thin;border-color:gray'>");
                strTable.Append("<td style='text-align: left;border-style: solid;border-width:thin;border-color:gray'>");
                strTable.Append(dr["Vessel_Name"].ToString());
                strTable.Append("</td>");

                strTable.Append("<td style='text-align: left;border-style: solid;border-width:thin;border-color:gray'>");
                strTable.Append(dr["Charterer"].ToString());
                strTable.Append("</td>");

                strTable.Append("<td style='text-right: left;border-style: solid;border-width:thin;border-color:gray'>");
                strTable.Append(dr["Hire_Rate"].ToString());
                strTable.Append("</td>");

                if (dr["NoticeReceived"].ToString() == "0")
                    strTable.Append("<td style='text-align: left;border-style: solid;border-width:thin;border-color:gray'>");
                else
                    strTable.Append("<td style='text-align: left;color:red;border-style: solid;border-width:thin;border-color:gray'>");
                strTable.Append(dr["Next_Hire_Due_Date"].ToString());
                strTable.Append("</td>");


                strTable.Append("<td style='text-right: left;border-style: solid;border-width:thin;border-color:gray'>");
                strTable.Append(dr["OverDue"].ToString());
                strTable.Append("</td>");

                strTable.Append("</tr>");
            }
        }
        else
            strTable.Append(" <tr style='text-align: center;'><td  colspan='5'>No record found !</td></tr>");

        strTable.Append("</table></div>");

        return strTable.ToString();


    }
    [WebMethod]
    public string asyncGet_CrewEvaluation_Feedback(int UserID, string DateFormat)
    {
        UDCHyperLink alinkCrew = new UDCHyperLink("", "../crew/CrewDetails.aspx", new string[] { "ID" }, new string[] { "CrewID" }, "");
        UDCHyperLink alinkView = new UDCHyperLink("", "../CrewEvaluation/DoEvaluation.aspx", new string[] { "CrewID", "EID", "DTLID", "M", "SchID" }, new string[] { "CrewID", "Evaluation_ID", "CrewEvaluation_ID", "DueDate", "ID" }, "");
        Dictionary<string, UDCHyperLink> dicLink = new Dictionary<string, UDCHyperLink>();
        dicLink.Add("Staff_Code", alinkCrew);
        dicLink.Add("Feedback_DueDate", alinkView);

        DataTable dt = BLL_Dashboard.Dash_Get_CrewEvaluation_Feedback(UserID);
        ChangeColumnDataType(dt, "DueDate", typeof(string));
        foreach (DataRow row in dt.Rows)
        {
            if (DateTime.Parse(row["DueDate"].ToString()) < DateTime.Now)
            {
                row["DueDate"] = UDFLib.ConvertUserDateFormat(Convert.ToString(row["DueDate"]), DateFormat);
                string s = "<div style=" + "color:white;background-color:red" + ">";
                row["DueDate"] = s + row["DueDate"].ToString() + "</div>";
            }
        }
        return UDFLib.CreateHtmlTableFromDataTable(dt,
           new string[] { "Vessel", "Staff Code", "Name", "Rank", "Requested By", "DueDate" },
           new string[] { "Vessel_Short_Name", "Staff_Code", "Staff_FullName", "Rank_Short_Name", "RequestedBy", "DueDate" },
           dicLink,
            new Dictionary<string, UDCToolTip>(),
           new string[] { "left", "center", "left" },
           "tbl-common-Css",
           "hdr-common-Css",
           "row-common-Css");

    }
    public static bool ChangeColumnDataType(DataTable table, string columnname, Type newtype)
    {
        if (table.Columns.Contains(columnname) == false)
            return false;

        DataColumn column = table.Columns[columnname];
        if (column.DataType == newtype)
            return true;

        try
        {
            DataColumn newcolumn = new DataColumn("temporary", newtype);
            table.Columns.Add(newcolumn);
            foreach (DataRow row in table.Rows)
            {
                try
                {
                    row["temporary"] = Convert.ChangeType(row[columnname], newtype);
                }
                catch
                {
                }
            }
            table.Columns.Remove(columnname);
            newcolumn.ColumnName = columnname;
        }
        catch (Exception)
        {
            return false;
        }

        return true;
    }
    [WebMethod]
    public string Get_CrewOnboardListRankWise(int UserID)
    {
        return GetHTMLTable_CrewOnboardListRankWise(UserID);

    }
    public string GetHTMLTable_CrewOnboardListRankWise(int UserID)
    {
        Dictionary<string, UDCHyperLink> diclink = new Dictionary<string, UDCHyperLink>();

        DataTable dt = UDFLib.PivotTable("Rank_Short_Name", "ONBD_Count", "Rank_Sort_Order", new string[] { "Vessel_ID" }, new string[] { }, BLL_Dashboard.Dash_Get_CrewOnboardListRankWise(UserID));
        ArrayList list = new ArrayList();
        ArrayList listDirection = new ArrayList();
        foreach (DataColumn col in dt.Columns)
        {
            if (col.ToString() != "Vessel_ID")
                list.Add(col.ToString());

            if (col.ToString() == "Vessel")
                listDirection.Add("left");
            else if (col.ToString() != "Vessel_ID")
                listDirection.Add("center");
        }
        string[] dtArray = (string[])list.ToArray(typeof(string));
        string[] dtDirection = (string[])list.ToArray(typeof(string));

        return UDFLib.CreateHtmlTableFromDataTable(dt,
           dtArray,
           dtArray,
           diclink,
            new Dictionary<string, UDCToolTip>(),
           dtDirection,
           "tbl-common-Css",
           "hdr-common-Css",
           "row-OnBoardCount-Css");

    }
    [WebMethod]
    public string Get_CrewSeniorityReward(int UserID)
    {
        UDCHyperLink alinkCrew = new UDCHyperLink("", "../crew/CrewDetails.aspx", new string[] { "ID" }, new string[] { "CrewID" }, "");
        Dictionary<string, UDCHyperLink> dicLink = new Dictionary<string, UDCHyperLink>();
        dicLink.Add("Staff_Code", alinkCrew);

        UDCHyperLink alinkStatus = new UDCHyperLink("", "../crew/CrewCompanySeniorityReward.aspx", new string[] { "Staff_Code", "SeniorityYear" }, new string[] { "Staff_Code", "SeniorityYear" }, "");
        dicLink.Add("RewardStatus", alinkStatus);

        return UDFLib.CreateHtmlTableFromDataTable(BLL_Dashboard.Dash_Get_CrewSeniorityReward(UserID),
           new string[] { "Staff Code", "Name", "Rank", "Seniority Year", "Seniority Days", "Status" },
           new string[] { "Staff_Code", "Staff_FullName", "Rank_Short_Name", "SeniorityYear", "SeniorityDays", "RewardStatus" },
           dicLink,
            new Dictionary<string, UDCToolTip>(),
           new string[] { "left", "center", "left" },
           "tbl-common-Css",
           "hdr-common-Css",
           "row-common-Css");

    }
    [WebMethod]
    public string AsyncGet_ReqsnCount(int User_ID, int CompanyID)
    {
        StringBuilder strTable = new StringBuilder();

        DataTable FleetDT = BLL_Dashboard.Dash_GetFleetList(CompanyID);
        DataTable DeptDt = BLL_Dashboard.Dash_GetDeptType();

        DataTable dtUserFleet = BLL_Dashboard.Dash_Get_Fleet_By_UserID(User_ID);

        if (dtUserFleet.Rows.Count > 0)
        {
            strTable.Append("<table class='tbl-css-dash' CELLPADDING='0' CELLSPACING='1'  >");
            foreach (DataRow drflt in FleetDT.Rows)
            {
                DataRow[] drUserFleet = dtUserFleet.Select("fleetcode='" + drflt["code"].ToString() + "'");
                if (drUserFleet.Length > 0)
                {
                    strTable.Append("<tr> <td class='cell-HD-css' ><span>" + drflt["Name"].ToString() + "</span> </td></tr>");
                    strTable.Append("<tr>");
                    foreach (DataRow drDep in DeptDt.Rows)
                    {
                        if (drDep["Short_Code"].ToString() != "ALL")
                        {
                            strTable.Append("<td class='cell-HD-css' ><span>" + drDep["Description"].ToString() + "</span> </td>");
                        }
                    }
                    strTable.Append("</tr>");
                    strTable.Append("<tr class='td-css-dash'>");
                    foreach (DataRow drDep in DeptDt.Rows)
                    {

                        if (drDep["Short_Code"].ToString() != "ALL")
                        {
                            strTable.Append("<td class='td-css-dash'><table class='cell-HD-css' >");
                            DataTable dtReqsnCount = BLL_Dashboard.Dash_Get_Rreqsn_Count(drDep["Short_Code"].ToString(), UDFLib.ConvertToInteger(drflt["code"].ToString()), User_ID.ToString()).Tables[0];


                            if (dtReqsnCount.Rows.Count > 0)
                            {
                                if (dtReqsnCount.Columns.Count > 0)
                                {
                                    strTable.Append("<tr >");
                                    for (int i = 0; i < dtReqsnCount.Columns.Count; i++)
                                    {
                                        strTable.Append("<th class='HeaderStyle-css-dash'  width='auto' >");
                                        strTable.Append("<b>" + dtReqsnCount.Columns[i].ToString() + "</b>");
                                        strTable.Append("</th>");
                                    }
                                    strTable.Append("</tr>");
                                }
                                for (int j = 0; j < dtReqsnCount.Rows.Count; j++)
                                {
                                    if (j % 2 == 0)
                                        strTable.Append("<tr class='RowStyle-css-dash'>");
                                    else
                                        strTable.Append("<tr class='AlternatingRowStyle-css-dash'>");

                                    for (int i = 0; i < dtReqsnCount.Columns.Count; i++)
                                    {
                                        strTable.Append("<td >");
                                        strTable.Append(dtReqsnCount.Rows[j][i].ToString().Replace("\n", "<br>"));
                                        strTable.Append("</td>");
                                    }
                                    strTable.Append("</tr>");
                                }

                            }
                            else
                            {
                                strTable.Append("<tr ><th class='HeaderStyle-css-dash' width='auto'>Message</th></tr>");
                                strTable.Append("<tr ><td  class='RowStyle-css-dash'>No record found !</td></tr>");
                            }
                            strTable.Append("</table></td>");
                        }


                    }
                    strTable.Append("</tr>");
                }
            }
            strTable.Append("</table>");
        }
        else
            strTable.Append("No record found !");

        return strTable.ToString();
    }
    [WebMethod]
    public string AsyncGet_Requisition_Processing_Time(int User_ID, int CompanyID)
    {
        StringBuilder strTable = new StringBuilder();

        DataTable FleetDT = BLL_Dashboard.Dash_GetFleetList(CompanyID);
        DataTable DeptDt = BLL_Dashboard.Dash_GetDeptType();
        DataTable dtUserFleet = BLL_Dashboard.Dash_Get_Fleet_By_UserID(User_ID);

        if (dtUserFleet.Rows.Count > 0)
        {
            strTable.Append("<table class='tbl-css-dash' CELLPADDING='0' CELLSPACING='1'  >");
            foreach (DataRow drflt in FleetDT.Rows)
            {
                DataRow[] drUserFleet = dtUserFleet.Select("fleetcode='" + drflt["code"].ToString() + "'");
                if (drUserFleet.Length > 0)
                {
                    strTable.Append("<tr> <td class='cell-HD-css' ><span>" + drflt["Name"].ToString() + "</span> </td></tr>");
                    strTable.Append("<tr>");
                    foreach (DataRow drDep in DeptDt.Rows)
                    {
                        if (drDep["Short_Code"].ToString() != "ALL")
                        {
                            strTable.Append("<td class='cell-HD-css' ><span>" + drDep["Description"].ToString() + "</span> </td>");
                        }
                    }
                    strTable.Append("</tr>");
                    strTable.Append("<tr class='td-css-dash'>");
                    foreach (DataRow drDep in DeptDt.Rows)
                    {

                        if (drDep["Short_Code"].ToString() != "ALL")
                        {
                            strTable.Append("<td class='td-css-dash'><table class='cell-HD-css' >");
                            DataTable dtReqsnCount = BLL_Dashboard.Dash_Get_Reqsn_Processing_Time(drDep["Short_Code"].ToString(), UDFLib.ConvertToInteger(drflt["code"].ToString()), User_ID);


                            if (dtReqsnCount.Rows.Count > 0)
                            {
                                if (dtReqsnCount.Columns.Count > 0)
                                {
                                    strTable.Append("<tr >");
                                    for (int i = 0; i < dtReqsnCount.Columns.Count; i++)
                                    {
                                        strTable.Append("<th class='HeaderStyle-css-dash'  width='auto' >");
                                        strTable.Append("<b>" + dtReqsnCount.Columns[i].ToString() + "</b>");
                                        strTable.Append("</th>");
                                    }
                                    strTable.Append("</tr>");
                                }
                                for (int j = 0; j < dtReqsnCount.Rows.Count; j++)
                                {
                                    if (j % 2 == 0)
                                        strTable.Append("<tr class='RowStyle-css-dash'>");
                                    else
                                        strTable.Append("<tr class='AlternatingRowStyle-css-dash'>");

                                    for (int i = 0; i < dtReqsnCount.Columns.Count; i++)
                                    {
                                        strTable.Append("<td >");
                                        strTable.Append(dtReqsnCount.Rows[j][i].ToString().Replace("\n", "<br>"));
                                        strTable.Append("</td>");
                                    }
                                    strTable.Append("</tr>");
                                }

                            }
                            else
                            {
                                strTable.Append("<tr ><th class='HeaderStyle-css-dash' width='auto'>Message</th></tr>");
                                strTable.Append("<tr ><td  class='RowStyle-css-dash'>No record found !</td></tr>");
                            }
                            strTable.Append("</table></td>");
                        }


                    }
                    strTable.Append("</tr>");
                }
            }
            strTable.Append("</table>");
        }
        else
            strTable.Append("No record found !");

        return strTable.ToString();
    }
    [WebMethod]
    public string AsyncGet_DecklogAnomalies(int User_ID)
    {
        StringBuilder strTable = new StringBuilder();
        strTable.Append("<table CELLPADDING='2' CELLSPACING='0'  style='border-collapse:collapse' >");
        strTable.Append("<tr >");
        strTable.Append("<th class='HeaderStyle-css-dash'  width='auto' >");
        strTable.Append("Vessel");
        strTable.Append("</th>");
        for (int i = 0; i < 7; i++)
        {
            strTable.Append("<th class='HeaderStyle-css-dash'  width='auto' >");
            strTable.Append(DateTime.Now.AddDays(-i).ToString("dd/MMM/yyyy"));
            strTable.Append("</th>");
        }
        strTable.Append("</tr>");
        DataTable ds = BLL_Dashboard.Dash_Get_Decklog_Anomalies(User_ID).Tables[0];
        for (int j = 0; j < ds.Rows.Count; j++)
        {
            if (j % 2 == 0)
                strTable.Append("<tr class='RowStyle-css-dash'>");
            else
                strTable.Append("<tr class='AlternatingRowStyle-css-dash'>");

            strTable.Append("<td>");
            strTable.Append(ds.Rows[j]["Vessel_Name"].ToString().Replace("\n", "<br>"));
            strTable.Append("</td>");



            for (int i = 1; i < 8; i++)
            {
                string LogBookID = ds.Rows[j]["LogBookID" + (i - 1)].ToString().Trim();
                string Vessel_ID = ds.Rows[j]["Vessel_ID"].ToString().Trim();

                strTable.Append("<td style='border-style:dotted; border-width: 0.5px;' ");
                if ((LogBookID.Trim().Length != 0))
                {
                    strTable.Append("onclick=\"javascript:window.open('../Operations/DeckLog/DeckLogBookDetails.aspx?DeckLogBookID=" + LogBookID + "&Vessel_ID=" + Vessel_ID + "');return false;\" ");
                }
                try
                {
                    if (ds.Rows[j][i].ToString().Trim() != "")
                    {
                        if (Convert.ToInt32(ds.Rows[j][i].ToString().Trim()) == 0)
                        {
                            strTable.Append("class='NoAnomaly'");
                        }
                        else if (Convert.ToInt32(ds.Rows[j][i].ToString().Trim()) > 0)
                        {
                            strTable.Append("class='AnomalyCell'");
                        }
                    }
                    else
                    {
                        strTable.Append("class='NoData'");
                    }
                }
                catch (Exception)
                {
                }


                strTable.Append(">   ");
                strTable.Append("</td>");
            }
            strTable.Append("</tr>");
        }
        strTable.Append("</table>");
        return strTable.ToString();
    }
    [WebMethod]
    public string AsyncGet_EnginelogAnomalies(int User_ID)
    {
        StringBuilder strTable = new StringBuilder();
        strTable.Append("<table CELLPADDING='2' CELLSPACING='0'  style='border-collapse:collapse' >");
        strTable.Append("<tr >");
        strTable.Append("<th class='HeaderStyle-css-dash'  width='auto' >");
        strTable.Append("Vessel");
        strTable.Append("</th>");
        for (int i = 0; i < 7; i++)
        {
            strTable.Append("<th class='HeaderStyle-css-dash'  width='auto' >");
            strTable.Append(DateTime.Now.AddDays(-i).ToString("dd/MMM/yyyy"));
            strTable.Append("</th>");
        }
        strTable.Append("</tr>");
        DataTable ds = BLL_Dashboard.Dash_Get_Enginelog_Anomalies(User_ID).Tables[0];
        for (int j = 0; j < ds.Rows.Count; j++)
        {
            if (j % 2 == 0)
                strTable.Append("<tr class='RowStyle-css-dash'>");
            else
                strTable.Append("<tr class='AlternatingRowStyle-css-dash'>");

            strTable.Append("<td>");
            strTable.Append(ds.Rows[j]["Vessel_Name"].ToString().Replace("\n", "<br>"));
            strTable.Append("</td>");



            for (int i = 1; i < 8; i++)
            {
                string LOG_ID = ds.Rows[j]["LOG_ID" + (i - 1)].ToString().Trim();
                string Vessel_ID = ds.Rows[j]["Vessel_ID"].ToString().Trim();

                strTable.Append("<td style='border-style:dotted; border-width: 0.5px;' ");
                if ((LOG_ID.Trim().Length != 0))
                {
                    strTable.Append("onclick=\"javascript:window.open('../Technical/ERLog/ERLogDetails.aspx?LOGID=" + LOG_ID + "&VESSELID=" + Vessel_ID + "');return false;\" ");
                }
                try
                {
                    if (ds.Rows[j][i].ToString().Trim() != "")
                    {
                        if (Convert.ToInt32(ds.Rows[j][i].ToString().Trim()) == 0)
                        {
                            strTable.Append("class='NoAnomaly'");
                        }
                        else if (Convert.ToInt32(ds.Rows[j][i].ToString().Trim()) > 0)
                        {
                            strTable.Append("class='AnomalyCell'");
                        }
                    }
                    else
                    {
                        strTable.Append("class='NoData'");
                    }
                }
                catch (Exception)
                {
                }


                strTable.Append(">   ");
                strTable.Append("</td>");
            }
            strTable.Append("</tr>");
        }
        strTable.Append("</table>");
        return strTable.ToString();
    }
    [WebMethod]
    public string AsyncGet_CrewEvaluation_60Percent(int User_ID, string DateFormat)
    {
        UDCHyperLink alink = new UDCHyperLink("", "../crew/CrewDetails.aspx", new string[] { "ID" }, new string[] { "CrewID" }, "");
        Dictionary<string, UDCHyperLink> dicLink = new Dictionary<string, UDCHyperLink>();
        dicLink.Add("STAFF_CODE", alink);

        DataTable dt = BLL_Dashboard.Dash_Get_Evaluation_60Percent(User_ID);
        foreach (DataRow row in dt.Rows)
            row["evaluation_date"] = UDFLib.ConvertUserDateFormat(Convert.ToString(row["evaluation_date"]), DateFormat);

        return UDFLib.CreateHtmlTableFromDataTable(dt,
                new string[] { "Vessel", "S/Code", "Staff Name", "Rank", "Evaluation Date", "Marks(%)" },
                new string[] { "Vessel_Short_Name", "STAFF_CODE", "Staff_FullName", "Rank_Short_Name", "evaluation_date", "AvgMarks" },
                dicLink,
            new Dictionary<string, UDCToolTip>(),
                new string[] { "left", "left", "left", "left", "center", "center" },
               "tbl-common-Css",
                  "hdr-common-Css",
                  "row-common-Css");

    }
    [WebMethod]
    public string AsyncGet_CrewEvaluationsList(int User_ID, string DateFormat)
    {
        UDCHyperLink alink = new UDCHyperLink("", "../crew/CrewDetails.aspx", new string[] { "ID" }, new string[] { "CrewID" }, "");
        Dictionary<string, UDCHyperLink> dicLink = new Dictionary<string, UDCHyperLink>();
        dicLink.Add("STAFF_CODE", alink);

        DataTable dt = BLL_Dashboard.Dash_Get_Evaluation_Schedules(User_ID);
        foreach (DataRow row in dt.Rows)
            row["DueDate"] = UDFLib.ConvertUserDateFormat(Convert.ToString(row["DueDate"]), DateFormat);

        return UDFLib.CreateHtmlTableFromDataTable(dt,
                new string[] { "Vessel", "S/Code", "Staff Name", "Rank", "Date" },
                new string[] { "Vessel_Short_Name", "STAFF_CODE", "Staff_FullName", "Rank_Short_Name", "DueDate" },
                dicLink,
            new Dictionary<string, UDCToolTip>(),
                new string[] { "left", "left", "left", "left", "left" },
                "tbl-common-Css",
                  "hdr-common-Css",
                  "row-common-Css");
    }
    [WebMethod]
    public string AsyncGet_CrewCardProposed(int User_ID, string DateFormat)
    {
        UDCHyperLink alink = new UDCHyperLink("", "../crew/CrewDetails.aspx", new string[] { "ID" }, new string[] { "CrewID" }, "");
        Dictionary<string, UDCHyperLink> dicLink = new Dictionary<string, UDCHyperLink>();
        dicLink.Add("STAFF_CODE", alink);

        DataTable dt = BLL_Dashboard.Dash_Get_CrewCardIndex(0, 0, 0, 0, "", User_ID);
        foreach (DataRow row in dt.Rows)
            row["Date_Of_Creation"] = UDFLib.ConvertUserDateFormat(Convert.ToString(row["Date_Of_Creation"]), DateFormat);

        return UDFLib.CreateHtmlTableFromDataTable(dt,
                new string[] { "Staff Code", "Name", "Rank", "Vessel", "Proposed By", "Propose Date", "Status" },
                new string[] { "STAFF_CODE", "Staff_FullName", "Rank_short_Name", "Vessel_Short_Name", "ProposedBy", "Date_Of_Creation", "Status" },
                dicLink,
            new Dictionary<string, UDCToolTip>(),
                new string[] { "left", "left", "center", "center", "left", "left" },
                "tbl-common-Css",
                  "hdr-common-Css",
                  "row-common-Css");
    }
    [WebMethod]
    public string AsyncGet_CrewCpmplaints(int User_ID, string DateFormat)
    {
        string strTable = "";
        int j;

        strTable = "<div id='dvCrewComplaintList'>";


        for (int i = 0; i < 2; i++)
        {
            strTable = strTable + @"<div>
            <table style='width: 100%; border: 1px solid #cccccc; font-family: Tahoma; font-size: 11px;'>
                <tr class='gradiant-css-blue'>
                    <td style='font-weight: bold; font-size: 12px;'>";
            if (i == 0)
                strTable = strTable + "Complaints Escalated to DPA";
            else
                strTable = strTable + "Pending Crew Complaints";

            strTable = strTable + @"</td>
                </tr>
                <tr>
                    <td style='vertical-align: top; border: 1px solid #cccccc;'>
                        <div><table style='width: 100%; border-collapse: collapse' border='1' cellpadding='2'
                                        cellspacing='0'>
                                        <tr style='background-color: #627AA8; color: Aqua; font-weight: bold; text-align: center;'>
                                            <td style='text-align: center; width: 20px;'>
                                            </td>
                                            <td>
                                                Vessel
                                            </td>
                                            <td style='width: 100px'>
                                                Escalated On
                                            </td>
                                            <td colspan='3'>
                                                Escalated By
                                            </td>
                                            <td>
                                                Complaint
                                            </td>
                                            <td>
                                                Status
                                            </td>
                                        </tr> ";

            if (i == 0)
                j = 1;
            else
                j = 0;

            DataTable dt = BLL_Dashboard.Dash_Get_CrewComplaintList(j, User_ID).Tables[0];
            foreach (DataRow row in dt.Rows)
                row["Escalated_On"] = UDFLib.ConvertUserDateFormat(Convert.ToString(row["Escalated_On"]), DateFormat);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    strTable = strTable + @"<tr style='text-align: center;'>
                                        <td>
                                            <img src='../Images/Plus.png' alt='' class='dbx-toggle' onclick='showEscalationLog(event," + row["worklist_id"] + "," + row["vessel_id"] + "," + User_ID + @")' />
                                        </td>
                                        <td>
                                            " + row["vessel_name"] + @"</td> <td>
                                        
                                            " + row["Escalated_On"] + @"
                                        </td>
                                        <td>
                                            <a href='../crew/CrewDetails.aspx?ID=" + row["Escalated_By"] + @"' target='_blank'>
                                                " + row["Escalated_By_Staff_Code"] + @"</a>
                                        </td>
                                        <td>
                                            " + row["Escalated_By_Rank"] + @"
                                        </td>
                                        <td style='text-align: left;'>
                                             " + row["Escalated_by_Name"] + @"
                                        </td>
                                        <td style='text-align: left; text-decoration: none;'>
                                            <a href='../Technical/worklist/ViewJob.aspx?OFFID= " + row["office_id"] + @"&WLID= " + row["worklist_id"] + @"&VID= " + row["vessel_id"] + @"'
                                                target='_blank'>
                                                 " + row["JOB_DESCRIPTION"] + @"</a>
                                        </td>
                                        <td style='text-align: left; color: Red;'>
                                             " + row["status"] + @"
                                        </td>
                                    </tr>
                                    <tr class='" + row["worklist_id"] + @"' style='display: none'>
                                        <td colspan='8'>
                                            <div id='dvLog" + row["worklist_id"] + @"'>
                                            </div>
                                        </td>
                                    </tr>";
                }
            }
            else
                strTable = strTable + " <tr style='text-align: center;'><td  colspan='8'>No record found !</td></tr>";

            strTable = strTable + @" </table></div>
                                        </td>
                                    </tr>
                                </table>
                            </div>";

        }

        strTable = strTable + "</div>";
        return strTable;
    }
    [WebMethod]
    public string asyncGet_EscalationLog(int wlid, int vid, int userid, string DateFormat)
    {
        string strTable = "";
        strTable = "<div>";

        DataTable dt = BLL_Dashboard.Dash_Get_CrewComplaintLog(wlid, vid, userid).Tables[0];

        foreach (DataRow row in dt.Rows)
            row["Escalated_On"] = UDFLib.ConvertUserDateFormat(Convert.ToString(row["Escalated_On"]), DateFormat);

        strTable = strTable + @"<table style='width: 100%; border-collapse: collapse' border='1' cellpadding='2'
                cellspacing='0'>
                <tr style='background-color: #627AA8; color: Aqua; font-weight: bold; text-align: center;'>
                    <td>
                        Escalated On
                    </td>
                    <td colspan='3'>
                        Escalated By
                    </td>
                    <td colspan='3'>
                        Escalated To
                    </td>
                </tr>";


        if (dt.Rows.Count > 0)
        {
            foreach (DataRow row in dt.Rows)
            {
                strTable = strTable + @" <tr style='text-align: center;'>
                    <td>
                       " + row["Escalated_On"] + @"
                    </td>
                    <td>
                        <a href='../crew/CrewDetails.aspx?ID=" + row["Escalated_By"] + @"' target='_blank'>
                            " + row["Escalated_By_Staff_Code"] + @"</a>
                    </td>
                    <td>
                        " + row["Escalated_By_Rank"] + @"
                    </td>
                    <td style='text-align: left;'>
                        " + row["Escalated_by_Name"] + @"
                    </td>
                    <td>
                        <a href='../crew/CrewDetails.aspx?ID=" + row["Escalated_To"] + @"' target='_blank'>
                             " + row["Escalated_To_Staff_Code"] + @"</a>
                    </td>
                    <td>
                         " + row["Escalated_To_Rank"] + @"
                    </td>
                    <td style='text-align: left;'>
                         " + row["Escalated_To_Name"] + @"
                    </td>
                </tr>";
            }
        }
        else
            strTable = strTable + " <tr style='text-align: center;'><td  colspan='7'>No record found !</td></tr>";

        strTable = strTable + @"</table></div>";

        return strTable;
    }

    [WebMethod]
    public string AsyncGet_VoyageAlert(int UserID)
    {

        StringBuilder strTable = new StringBuilder();
        strTable.Append("<div><table CELLPADDING='2' CELLSPACING='0'  style='border-collapse:collapse;' >");
        strTable.Append("<tr >");
        strTable.Append("<th class='HeaderStyle-css-dash'  width='auto' >");
        strTable.Append("Vessel");
        strTable.Append("</th>");

        strTable.Append("<th class='HeaderStyle-css-dash'  width='auto' >");
        strTable.Append("Wind");
        strTable.Append("</th>");
        strTable.Append("<th class='HeaderStyle-css-dash'  width='auto' >");
        strTable.Append("Ins. Speed");
        strTable.Append("</th>");
        strTable.Append("<th class='HeaderStyle-css-dash'  width='auto' >");
        strTable.Append("Avg. Speed");
        strTable.Append("</th>");
        strTable.Append("<th class='HeaderStyle-css-dash'  width='auto' >");
        strTable.Append("CP HO");
        strTable.Append("</th>");
        strTable.Append("<th class='HeaderStyle-css-dash'  width='auto' >");
        strTable.Append("Act HO");
        strTable.Append("</th>");
        strTable.Append("<th class='HeaderStyle-css-dash'  width='auto' >");
        strTable.Append("CP DO");
        strTable.Append("</th>");
        strTable.Append("<th class='HeaderStyle-css-dash'  width='auto' >");
        strTable.Append("Act DO");
        strTable.Append("</th>");
        strTable.Append("<th class='HeaderStyle-css-dash'  width='auto' >");
        strTable.Append("FW Cons");
        strTable.Append("</th>");
        strTable.Append("<th class='HeaderStyle-css-dash'  width='auto' >");
        strTable.Append("Low Cons");
        strTable.Append("</th>");
        strTable.Append("<th class='HeaderStyle-css-dash'  width='auto' >");
        strTable.Append("Date");
        strTable.Append("</th>");

        strTable.Append("</tr>");
        DataTable dt = BLL_Dashboard.Dash_Get_VoyageSnippetData(UserID);
        DataTable dtCPROB;
        dtCPROB = BLL_Dashboard.Dash_OPS_Get_RecentCPROB();
        if (dt.Rows.Count > 0)
        {
            foreach (DataRow dr in dt.Rows)
            {
                bool stsLow = false;
                bool AnyExcceed = false;
                string strCPFlt = "Vessel_ID=" + dr["VESSEL_ID"].ToString() + " AND datatype like '%ROB%'";


                DataRow[] drCPROB = dtCPROB.Select(strCPFlt);

                foreach (DataRow drROB in drCPROB)
                {
                    string DataCode = drROB["Data_Code"].ToString().ToUpper().Trim();
                    decimal datavalue = decimal.Parse(drROB["data_value"].ToString());

                    if (DataCode == "HO_ROB" && decimal.Parse(dr["HO_ROB"].ToString()) < datavalue)
                    {
                        stsLow = true;
                        AnyExcceed = true;
                        break;
                    }

                    else if (DataCode == "DO_ROB" && decimal.Parse(dr["DO_ROB"].ToString()) < datavalue)
                    {
                        AnyExcceed = true;
                        stsLow = true;
                        break;
                    }

                    else if (DataCode == "AECC_ROB" && decimal.Parse(dr["AECC_ROB"].ToString()) < datavalue)
                    {
                        AnyExcceed = true;
                        stsLow = true;
                        break;
                    }



                    else if (DataCode == "MECC_ROB" && decimal.Parse(dr["MECC_ROB"].ToString()) < datavalue)
                    {
                        AnyExcceed = true;
                        stsLow = true;
                        break;
                    }

                    else if (DataCode == "MECYL_ROB" && decimal.Parse(dr["MECYL_ROB"].ToString()) < datavalue)
                    {
                        AnyExcceed = true;
                        stsLow = true;
                        break;
                    }

                    else if (DataCode == "FW_ROB" && decimal.Parse(dr["FW_ROB"].ToString()) < datavalue)
                    {
                        AnyExcceed = true;
                        stsLow = true;
                        break;

                    }




                }

                if (dr["ExceedWind"].ToString() == "1")

                    AnyExcceed = true;
                if (dr["ExceedSpeed"].ToString() == "1")
                    AnyExcceed = true;
                if (dr["ExceedHO"].ToString() == "1")
                    AnyExcceed = true;
                if (dr["ExceedDO"].ToString() == "1")
                    AnyExcceed = true;


                if (AnyExcceed)
                {

                    strTable.Append("<tr class='RowStyle-css-dash' style='text-align: center;'>");
                    strTable.Append("<td style='text-align: left;border-style: solid;border-width:thin;border-color:gray'>");
                    strTable.Append(dr["VESSEL_NAME"].ToString());
                    strTable.Append("</td>");
                    if (dr["ExceedWind"].ToString() == "0")
                        strTable.Append("<td style='text-align: left;border-style: solid;border-width:thin;border-color:gray'>");
                    else
                        strTable.Append("<td style='text-align: left;color:red;border-style: solid;border-width:thin;border-color:gray'>");
                    strTable.Append(dr["WIND_FORCE"].ToString());
                    strTable.Append("</td>");

                    strTable.Append("<td style='text-align: left;border-style: solid;border-width:thin;border-color:gray'>");
                    strTable.Append(dr["INSTRUCTED_SPEED"].ToString());
                    strTable.Append("</td>");
                    if (dr["ExceedSpeed"].ToString() == "0")
                        strTable.Append("<td style='text-align: left;border-style: solid;border-width:thin;border-color:gray'>");
                    else
                        strTable.Append("<td style='text-align: left;color:red;border-style: solid;border-width:thin;border-color:gray'>");
                    strTable.Append(dr["AVERAGE_SPEED"].ToString());
                    strTable.Append("</td>");
                    strTable.Append("<td style='text-align: left;border-style: solid;border-width:thin;border-color:gray'>");
                    strTable.Append(dr["CP_HOCONS"].ToString());
                    strTable.Append("</td>");
                    if (dr["ExceedHO"].ToString() == "0")
                        strTable.Append("<td style='text-align: left;border-style: solid;border-width:thin;border-color:gray'>");
                    else
                        strTable.Append("<td style='text-align: left;color:red;border-style: solid;border-width:thin;border-color:gray'>");
                    strTable.Append(dr["ACTUAL_HO_CONSMP"].ToString());
                    strTable.Append("</td>");
                    strTable.Append("<td style='text-align: left;border-style: solid;border-width:thin;border-color:gray'>");
                    strTable.Append(dr["CP_DOCONS"].ToString());
                    strTable.Append("</td>");
                    if (dr["ExceedDO"].ToString() == "0")
                        strTable.Append("<td style='text-align: left;border-style: solid;border-width:thin;border-color:gray'>");
                    else
                        strTable.Append("<td style='text-align: left;color:red;border-style: solid;border-width: thin; border-color:gray'>");
                    strTable.Append(dr["ACTUAL_DO_CONSMP"].ToString());
                    strTable.Append("</td>");
                    strTable.Append("<td style='text-align: left;border-style: solid;border-width:thin;border-color:gray'>");
                    strTable.Append(dr["CP_FWCONS"].ToString());
                    strTable.Append("</td>");
                    if (stsLow)
                    {
                        strTable.Append("<td style='text-align: left;color:red;border-style: solid;border-width:thin;border-color:gray'>");
                        strTable.Append("Yes");
                    }
                    else
                    {
                        strTable.Append("<td style='text-align: left;border-style: solid;border-width:thin;border-color:gray'>");
                        strTable.Append("No");
                    }
                    strTable.Append("</td>");
                    strTable.Append("<td style='text-align: left;border-style: solid;border-width:thin;border-color:gray'>");
                    strTable.Append(dr["TELEGRAM_DATE"].ToString());
                    strTable.Append("</td>");
                    strTable.Append("</tr>");
                }
            }
        }
        else
            strTable.Append(" <tr style='text-align: center;'><td  colspan='10'>No record found !</td></tr>");

        strTable.Append("</table></div>");

        return strTable.ToString();
    }
    public class Dashboard
    {

        public string ACCESS { get; set; }
        public string Active_Status { get; set; }
        public string Snippet_ID { get; set; }
        public string Department_Color { get; set; }
        public string VALUE { get; set; }
        public string Snippet_Function_Name { get; set; }
        public string Auto_Refresh { get; set; }
        public string ForMyAction { get; set; }
        public string IsCountSpecific { get; set; }
        public string FromDay { get; set; }
        public string ToDay { get; set; }
        public string HeaderColourCss { get; set; }
        public string HeaderTitle { get; set; }

    }
    [WebMethod]
    public List<Dashboard> AsyncGet_DashboardSnippets(int User_ID, int? DepID)
    {
        DataTable dt_Vessel = BLL_Dashboard.Dash_Get_Snippet_Access_OnDashboard(User_ID, UDFLib.ConvertIntegerToNull(DepID));
        List<Dashboard> dataList = new List<Dashboard>();

        foreach (DataRow dtrow in dt_Vessel.Rows)
        {

            Dashboard details = new Dashboard();

            details.ACCESS = dtrow[0].ToString();
            details.Active_Status = dtrow[1].ToString();
            details.Snippet_ID = dtrow[4].ToString();
            details.Department_Color = dtrow[5].ToString();
            details.VALUE = dtrow[6].ToString();
            details.Snippet_Function_Name = dtrow[7].ToString();
            details.Auto_Refresh = dtrow[8].ToString();
            details.ForMyAction = dtrow[9].ToString();
            details.IsCountSpecific = dtrow[11].ToString();
            details.FromDay = dtrow[12].ToString();
            details.ToDay = dtrow[13].ToString();
            details.HeaderColourCss = dtrow[14].ToString();
            details.HeaderTitle = dtrow[15].ToString();

            dataList.Add(details);

        }
        return dataList;

    }
    [WebMethod]
    public string AsyncGet_DashboardLayout(int User_ID, int DepID)
    {
        return BLL_Dashboard.Dash_GET_DashBoard_LayoutByUser(User_ID, DepID);
    }
    [WebMethod]
    public string[] Get_Performance_Manager(int UserID, int Days)
    {
        UDCHyperLink alink = new UDCHyperLink();
        alink.Target = "_blank";
        alink.NaviagteURL = "../Technical/worklist/SupdtInspReport.aspx";
        alink.QueryStringDataColumn = new string[] { "SchDetailId" };
        alink.QueryStringText = new string[] { "SchDetailId" };

        Dictionary<string, UDCHyperLink> diclink = new Dictionary<string, UDCHyperLink>();

        diclink.Add("Report", alink);
        DataSet ds = BLL_Dashboard.Dash_GET_PerformacneManager(UserID, Days);
        DataTable htmlTbl = ds.Tables[0];
        string htmlstring = UDFLib.CreateHtmlTableFromDataTable(htmlTbl,
            new string[] { "Vessel", "NCR<br>Pending", "NCR<br>Overdue", "Survey<br>Overdue", "Officer<br>Perfm.", "Officer<br>Exp.", "PSC<br>Defcn.", "PMS<br>Overdue" },//,"Pending(Non-NCR)","Pending(NCR)"
           new string[] { "Vessel_Name", "NCR_Pending", "NCR_Overdue", "Survey_overdue", "Officer_Performance", "Officer_Experience", "PSC_Deficiencies", "PMS_Overdue" },//, "PendingNonNCRJobs", "PendingNCRJobs" 
           diclink,
            new Dictionary<string, UDCToolTip>(),
           new string[] { "center", "center", "center" },
           "tbl-common-Css",
           "hdr-center-Css",
           "row-Per-Css");
        string lstupdatedon = ds.Tables[1].Rows[0][0].ToString();
        string[] retstring = { htmlstring, lstupdatedon };
        return retstring;
    }
    [WebMethod]
    public string getWorklist_Incident_180days()
    {
        UDCHyperLink alink = new UDCHyperLink();
        alink.Target = "_blank";
        alink.NaviagteURL = "../Technical/worklist/ViewJob.aspx";
        alink.QueryStringDataColumn = new string[] { "OFFICE_ID", "VESSEL_ID", "WORKLIST_ID" };
        alink.QueryStringText = new string[] { "OFFID", "VID", "WLID" };

        Dictionary<string, UDCHyperLink> diclink = new Dictionary<string, UDCHyperLink>();

        diclink.Add("JOB_DESCRIPTION", alink);

        return UDFLib.CreateHtmlTableFromDataTable(BLL_Dashboard.Dash_Get_WorkList_Incident_180days(),
                new string[] { "Vessel", "Code", "Job Description", "Date Raised", "Expected Compln", "Date Completed" },
                new string[] { "VESSEL_SHORT_NAME", "WLID_DISPLAY", "JOB_DESCRIPTION", "DATE_RAISED", "DATE_ESTMTD_CMPLTN", "DATE_COMPLETED" },
                diclink,
                new Dictionary<string, UDCToolTip>(),
                new string[] { },
                "tbl-common-Css",
                "hdr-common-Css",
                "row-common-Css");
    }
    [WebMethod]
    public string getWorklist_NearMiss_180days()
    {
        UDCHyperLink alink = new UDCHyperLink();
        alink.Target = "_blank";
        alink.NaviagteURL = "../Technical/worklist/ViewJob.aspx";
        alink.QueryStringDataColumn = new string[] { "OFFICE_ID", "VESSEL_ID", "WORKLIST_ID" };
        alink.QueryStringText = new string[] { "OFFID", "VID", "WLID" };

        Dictionary<string, UDCHyperLink> diclink = new Dictionary<string, UDCHyperLink>();

        diclink.Add("JOB_DESCRIPTION", alink);

        return UDFLib.CreateHtmlTableFromDataTable(BLL_Dashboard.Dash_Get_WorkList_NearMiss_180days(),
                new string[] { "Vessel", "Code", "Job Description", "Date Raised", "Expected Compln", "Date Completed" },
                new string[] { "VESSEL_SHORT_NAME", "WLID_DISPLAY", "JOB_DESCRIPTION", "DATE_RAISED", "DATE_ESTMTD_CMPLTN", "DATE_COMPLETED" },
                diclink,
                new Dictionary<string, UDCToolTip>(),
                new string[] { },
                "tbl-common-Css",
                "hdr-common-Css",
                "row-common-Css");
    }
    [WebMethod]
    public void AsyncInsert_DashboardLayout(int User_ID, string Layout, int DepID)
    {
        BLL_Dashboard.Dash_Insert_Dashboard_LayoutByUser(User_ID, Layout, DepID);

    }
    [WebMethod]
    public string Get_OpsWorklistOverdue(int UserID)
    {
        // Dictionary<string, UDCHyperLink> diclink = new Dictionary<string, UDCHyperLink>();
        return UDFLib.CreateHtmlTableFromDataTable(BLL_Dashboard.Dash_Get_OpsWorklistOverdue(UserID),
             new string[] { "Vessel", "Task", "Expected Completion", "Created By" },
             new string[] { "VESSEL_SHORT_NAME", "JOB_DESCRIPTION", "DATE_ESTMTD_CMPLTN", "Created_By_Name" },
            //diclink,
            //new Dictionary<string, UDCToolTip>(),
             new string[] { "left", "left", "left", "left" },
            "tbl-common-Css",
               "hdr-common-Css",
               "row-common-Css");
    }
    [WebMethod]
    public string Get_MyOperationWorklist(int UserID)
    {
        //Dictionary<string, UDCHyperLink> diclink = new Dictionary<string, UDCHyperLink>();
        return UDFLib.CreateHtmlTableFromDataTable(BLL_Dashboard.Dash_Get_MyOperationWorklist(UserID),
             new string[] { "Vessel", "Task", "Expected Completion", "Created By" },
             new string[] { "VESSEL_SHORT_NAME", "JOB_DESCRIPTION", "DATE_ESTMTD_CMPLTN", "Created_By_Name" },
            //  diclink,
            // new Dictionary<string, UDCToolTip>(),
             new string[] { "left", "left", "left", "left" },
             "tbl-common-Css",
               "hdr-common-Css",
               "row-common-Css");
    }
    [WebMethod]
    public string AsyncUpdate_DashboardSnippetColor(int User_ID, string SnippetID, string color)
    {
        return BLL_Dashboard.Dash_Update_SnippetColor(User_ID, SnippetID, color);
    }
    [WebMethod]
    public string AsyncUpdate_DashboardSnippetTitle(int User_ID, string SnippetID, string Title)
    {
        return BLL_Dashboard.Dash_Update_SnippetTitle(User_ID, SnippetID, Title);
    }

    [WebMethod]
    public void AsyncInsert_DefaultDashboard(int User_ID)
    {
        BLL_Dashboard.Dash_Update_DefaultDashboard(User_ID);
    }
    #region----------------Send Mail--------------------

    [WebMethod]
    public string Mail_CrewOnboardListRankWise(int UserID)
    {
        return BLL_Dashboard.Mail_CrewOnboardListRankWise(UserID, GetHTMLTable_CrewOnboardListRankWise(UserID));
    }

    #endregion

    [WebMethod]
    public string getRest_Hour_Data(int UserID, string DateFormat)
    {

        UDCHyperLink formStaffcodelink = new UDCHyperLink("", "../crew/CrewDetails.aspx", new string[] { "ID" }, new string[] { "CREWID" }, "");
        UDCHyperLink ruleLink = new UDCHyperLink("", "../QMS/RestHours/RestHour_Report.aspx", new string[] { "ID", "Vessel_ID", "CREWID", "RestHourDate" }, new string[] { "ID", "Vessel_ID", "CREWID", "RestHourDate" }, "");
        Dictionary<string, UDCHyperLink> dickLink = new Dictionary<string, UDCHyperLink>();
        dickLink.Add("StaffName", formStaffcodelink);
        dickLink.Add("RuleDescription", ruleLink);
        UDCToolTip udcToolTip = new UDCToolTip("RuleDescription", "RuleDescription", true);
        Dictionary<string, UDCToolTip> dicToolTip = new Dictionary<string, UDCToolTip>();
        dicToolTip.Add("RuleDescription", udcToolTip);
        DataTable dt = new DataTable();
        dt = BLL_Dashboard.Dash_Get_RestHourSnippet(UserID);

        ChangeColumnDataType(dt, "RestHourDate", typeof(string));

        foreach (DataRow row in dt.Rows)
            row["RestHourDate"] = UDFLib.ConvertUserDateFormat(Convert.ToString(row["RestHourDate"]), DateFormat);

        return UDFLib.CreateHtmlTableFromDataTable(dt,
                 new string[] { "Vessel", "Staff Name", "Rank", "Date", "Violated Rule" },
                new string[] { "Vessel", "StaffName", "Rank", "RestHourDate", "RuleDescription" },
                dickLink,
               dicToolTip,
                  new string[] { "left", "left", "left", "center" },
                "tbl-common-Css",
                "hdr-common-Css",
                "row-common-Css");
    }

    [WebMethod]
    public string asyncGet_Crew_Information(string CrewID, string date)
    {
        System.Text.StringBuilder info = new System.Text.StringBuilder();
        DataTable dtinfo = BLL_Dashboard.Get_Crew_Information(UDFLib.ConvertToInteger(CrewID), UDFLib.ConvertDateToNull(DateTime.Now.ToString()));

        string _redColorCode = "#ffa07a";
        string _BdrRedColorCode = "#f78b60";

        string _yellowColorCode = "#fffacd";
        string _BdrYellowColorCode = " #f3ea9a";


        string _greenColorCode = "#98FB98";
        string _BdrGreenColorCode = "#82eb82";

        string _bdrColor = "#C6C6C6";
        string _color = "#ffa07a";
        string _greyColorCode = "#D3D3D3";
        if (dtinfo.Rows.Count > 0)
        {
            info.Append("<div class='clsHitmap'>");
            info.Append("<style type='text/css'> .counterStyle { width: 20px; height: 20px; border-radius: 50%; border-collapse:collapse;font-size: 8px;text-align: center;vertical-align: middle;color: black; display: table-cell;cursor: pointer; } .counterStyle a{text-decoration:none;}");
            info.Append(".textStyle{ font-weight: bold; font-size: 11px;color: #092B4C; text-align: center;}.textNameStyle{font-size: 11px; color: #0D3E6E; line-height: 10px; text-align: left;}  </style>");
            info.Append("<table cellpadding='1px'> <tr align='center'> ");
            // Heat map should be generated only for crew members, and not office users.
            if (dtinfo.Rows[0]["USERTYPE"].ToString() != "OFFICE USER")
            {

                if (dtinfo.Rows[0]["Contract_Color"].ToString() == "GREEN")
                {
                    _color = _greenColorCode;
                    _bdrColor = _BdrGreenColorCode;
                }
                else if (dtinfo.Rows[0]["Contract_Color"].ToString() == "YELLOW")
                {
                    _color = "";
                    _color = _yellowColorCode;
                    _bdrColor = "";
                    _bdrColor = _BdrYellowColorCode;
                }
                else if (dtinfo.Rows[0]["Contract_Color"].ToString() == "RED")
                {
                    _color = "";
                    _color = _redColorCode;
                    _bdrColor = "";
                    _bdrColor = _BdrRedColorCode;
                }
                else
                {
                    _color = "";
                    _color = "#ffa07a";
                    _bdrColor = "";
                    _bdrColor = "#f78b60";
                }
                if (!string.IsNullOrEmpty(dtinfo.Rows[0]["VOYAGECOUNT"].ToString()))
                    info.Append(" <td style='width:33%;border:none;'><div id='ContractCount' style='background-color:" + _color + ";border:1px solid " + _bdrColor + ";'   class='counterStyle'  onmouseover='Get_Crew_Information_ToolTip(" + CrewID + " ,event,this);'  >" + dtinfo.Rows[0]["VOYAGECOUNT"].ToString() + "</div></td>");
                else
                    info.Append(" <td style='width:33%;border:none;'><div id='ContractCount' style='background-color:" + _color + ";border:1px solid " + _bdrColor + ";'   class='counterStyle'  onmouseover='Get_Crew_Information_ToolTip(" + CrewID + " ,event,this);'  >0</div></td>");
                //Evaluation Count
                if (dtinfo.Rows[0]["AVG_PER_COLOR"].ToString() == "GREEN")
                {
                    _color = "";
                    _color = _greenColorCode;
                    _bdrColor = "";
                    _bdrColor = _BdrGreenColorCode;
                }
                else if (dtinfo.Rows[0]["AVG_PER_COLOR"].ToString() == "YELLOW")
                {
                    _color = "";
                    _color = _yellowColorCode;
                    _bdrColor = "";
                    _bdrColor = _BdrYellowColorCode;
                }
                else if (dtinfo.Rows[0]["AVG_PER_COLOR"].ToString() == "RED")
                {
                    _color = "";
                    _color = _redColorCode;
                    _bdrColor = "";
                    _bdrColor = _BdrRedColorCode;
                }
                // For NTBR/InActive crew members , evaluation circle colour is grey and inside tooltip ,evaluation ranking shows N/A
                else if (dtinfo.Rows[0]["AVG_PER_COLOR"].ToString() == "GREY")
                {
                    _color = "";
                    _color = _greyColorCode;
                    _bdrColor = "";
                    _bdrColor = "#C6C6C6";
                }
                else
                {
                    _color = "";
                    _color = "#ffa07a";
                    _bdrColor = "";
                    _bdrColor = "#f78b60";
                }
                if (!string.IsNullOrEmpty(dtinfo.Rows[0]["EVALUATIONAVG"].ToString()))
                    info.Append(" <td style='width:33%;border:none;'><div id='ContractCount' style='background-color:" + _color + ";border:1px solid " + _bdrColor + ";'    class='counterStyle'  onmouseover='Get_Crew_Information_ToolTip(" + CrewID + " ,event,this);'    >" + dtinfo.Rows[0]["EVALUATIONAVG"].ToString() + "</div></td>");
                else
                    info.Append(" <td style='width:33%;border:none;'><div id='ContractCount' style='background-color:" + _color + ";border:1px solid " + _bdrColor + ";'    class='counterStyle'  onmouseover='Get_Crew_Information_ToolTip(" + CrewID + " ,event,this);'    >0</div></td>");
                //Card
                if (dtinfo.Rows[0]["CARDTYPE"].ToString() == "YELLOW CARD")
                {
                    _color = "";
                    _color = _yellowColorCode;
                    _bdrColor = "";
                    _bdrColor = _BdrYellowColorCode;
                }
                else if (dtinfo.Rows[0]["CARDTYPE"].ToString() == "RED CARD")
                {
                    _color = "";
                    _color = _redColorCode;
                    _bdrColor = "";
                    _bdrColor = _BdrRedColorCode;
                }
                else
                {
                    _color = "";
                    _color = _greenColorCode;
                    _bdrColor = "";
                    _bdrColor = _BdrGreenColorCode;
                }

                info.Append(" <td style='width:33%;border:none;'><div id='ContractCount' style='background-color:" + _color + ";border:1px solid " + _bdrColor + ";'  class='counterStyle' onmouseover='Get_Crew_Information_ToolTip(" + CrewID + " ,event,this);'     ></div></td>");

                info.Append(" </tr></table> ");

            }
            else
            {
                info.Append(" <td style='width:33%;border:none;'></td>");
                info.Append(" </tr></table> ");
            }

        }
        return info.ToString();

    }

    [WebMethod]
    public string Get_Crw_RefusedToSignEval(string DateFormat)
    {
        DataTable dt = BLL_Dashboard.Get_Crw_RefusedToSignEval();
        System.Text.StringBuilder info = new System.Text.StringBuilder();
        Boolean blnRowExits = false;


        if (dt.Rows.Count > 0)
        {
            dt.DefaultView.Sort = "Date desc";
            dt = dt.DefaultView.ToTable();

            info.Append("<table id='__tbl_remark' CELLPADDING='2' CELLSPACING='0'  style='border-collapse:collapse;' width='620px'>");
            info.Append("<tr>");
            info.Append("<td  rowspan='2' colspan='2' class='hdr-common-Css' style='Text-align:center;border-bottom:1px solid #006699;border-right:1px solid grey;'>Crew name</td>");
            info.Append("<td  width='auto' rowspan='2' class='hdr-common-Css' style='Text-align:center;border-bottom:1px solid #006699;border-right:1px solid grey;'>Rank</td>");
            info.Append("<td  width='auto' rowspan='2' class='hdr-common-Css' style='Text-align:center;border-bottom:1px solid #006699;border-right:1px solid grey;  '>Vessel</td>");
            info.Append("<td  width='auto' colspan='3' class='hdr-common-Css' style='Text-align:center;border-bottom:1px solid grey;border-right:1px solid grey;'>Evaluation </td>");
            info.Append("<td  width='auto' rowspan='2' class='hdr-common-Css' style='Text-align:center;border-bottom:1px solid #006699;border-right:1px solid grey;'>Evaluator</td>");
            info.Append("<td  width='auto' rowspan='2' class='hdr-common-Css' style='Text-align:center;border-bottom:1px solid #006699;'>Action</td>");
            info.Append("</tr>");

            info.Append("<tr>");
            info.Append("<td  class='hdr-common-Css' style='border-right:1px solid grey;Text-align:center'>Date</td>");
            info.Append("<td  width='auto' class='hdr-common-Css' style='Text-align:center;border-right:1px solid grey;'>Score</td>");
            info.Append("<td  width='auto' class='hdr-common-Css' style='border-right:1px solid grey;Text-align:center'>Avg.</td>");
            info.Append("</tr>");

            foreach (DataRow dr in dt.Rows)
            {
                string strName = string.Empty;

                if (dr["Crew Name"].ToString().Length > 13)
                {
                    strName = dr["Crew Name"].ToString().Substring(0, 13) + "...";
                }
                else
                    strName = dr["Crew Name"].ToString();


                if (Convert.ToString(dr["Date_Of_RecordRemoval"]) == "")// Show Red button on First time pg Load
                {
                    info.Append("<tr>");
                    info.Append("<td class='row-common-Css' style='line-height:19px;text-align:left;' width='130px'>");
                    info.Append("<a ID='CrewID' href='../Crew/CrewDetails.aspx?ID=" + dr["CrewID"].ToString() + "' runat='server' Target='_blank' title='" + dr["Crew Name"].ToString() + "'>" + strName + "&nbsp; " + "</a>");
                    info.Append("</td>");
                    info.Append("<td class='row-common-Css' style='line-height:19px;text-align:left;' width='80px'>");
                    info.Append(asyncGet_Crew_Information(Convert.ToString(dr["CrewID"]), Convert.ToString(DateTime.Now)));
                    info.Append("</td>");
                    info.Append("<td class='row-common-Css' width='60px'>" + dr["Rank"] + "</td>");
                    info.Append("<td class='row-common-Css' width='50px'>" + dr["Vessel"] + "</td>");
                    info.Append("<td class='row-common-Css' width='100px'>");
                    info.Append(Convert.ToString(dr["Date"]) != "" ? UDFLib.ConvertUserDateFormat(Convert.ToString(dr["Date"]), DateFormat) : dr["Date"]);
                    info.Append("</td>");
                    info.Append("<td class='row-common-Css' width='40px'>" + dr["Score"] + "</td>");
                    info.Append("<td class='row-common-Css' width='40px'>" + dr["Avg"] + "</td>");
                    info.Append("<td class='row-common-Css' width='80px'>");
                    info.Append("<a ID='CrewID' href='../Crew/CrewDetails.aspx?ID=" + dr["Evaluator_CrewDtlID"].ToString() + "' runat='server' Target='_blank'>" + dr["Evaluator"] + "</a>");
                    info.Append("</td>");
                    info.Append("<td class='row-common-Css' width='40px'><a id='lnkAction' Target='_blank' href='../CrewEvaluation/DoEvaluation.aspx?CrewID=" + dr["CrewID"] + "&EID=" + dr["EID"] + "&DTLID=" + dr["DTLID"] + "&M=" + dr["DueDate"].ToString().Replace("/", "-") + "&SchID=" + dr["Schedule_ID"] + "&DshBrd=1'><img src='../Images/redeye.png'/></a></td>");
                    info.Append("</tr>");

                    blnRowExits = true;
                }
                else
                {
                    if (Convert.ToDateTime(dr["Date_Of_RecordRemoval"]) > DateTime.Now)// if already clicked then show green Button 
                    {
                        info.Append("<tr>");
                        info.Append("<td class='row-common-Css' style='line-height:19px;text-align:left;' width='130px'>");
                        info.Append("<a ID='CrewID' href='../Crew/CrewDetails.aspx?ID=" + dr["CrewID"].ToString() + "' runat='server' Target='_blank' title='" + dr["Crew Name"].ToString() + "'>" + strName + "&nbsp; " + "</a>");
                        info.Append("</td>");
                        info.Append("<td class='row-common-Css' style='line-height:19px;text-align:left;' width='80px'>");
                        info.Append(asyncGet_Crew_Information(Convert.ToString(dr["CrewID"]), Convert.ToString(DateTime.Now)));
                        info.Append("</td>");
                        info.Append("<td class='row-common-Css' width='60px'>" + dr["Rank"] + "</td>");
                        info.Append("<td class='row-common-Css' width='50px'>" + dr["Vessel"] + "</td>");
                        info.Append("<td class='row-common-Css'width='100px'>");
                        info.Append(Convert.ToString(dr["Date"]) != "" ? UDFLib.ConvertUserDateFormat(Convert.ToString(dr["Date"]), DateFormat) : dr["Date"]);
                        info.Append("</td>");
                        info.Append("<td class='row-common-Css' width='40px'>" + dr["Score"] + "</td>");
                        info.Append("<td class='row-common-Css' width='40px'>" + dr["Avg"] + "</td>");
                        info.Append("<td class='row-common-Css' width='80px'>");
                        info.Append("<a ID='CrewID' href='../Crew/CrewDetails.aspx?ID=" + dr["Evaluator_CrewDtlID"].ToString() + "' runat='server' Target='_blank'>" + dr["Evaluator"] + "</a>");
                        info.Append("</td>");
                        info.Append("<td class='row-common-Css' width='40px'><a id='lnkAction' Target='_blank' href='../CrewEvaluation/DoEvaluation.aspx?CrewID=" + dr["CrewID"] + "&EID=" + dr["EID"] + "&DTLID=" + dr["DTLID"] + "&M=" + dr["DueDate"].ToString().Replace("/", "-") + "&SchID=" + dr["Schedule_ID"] + "&DshBrd=1'><img src='../Images/blackeye.png'/></a></td>");
                        info.Append("</tr>");
                        blnRowExits = true;
                    }
                    
                    
                }


            }

            if (blnRowExits == false)
            {
                info.Append("<tr><td colspan='9'><span style='color:maroon;padding:2px'> No record found !</span></td></tr>");
            }

            info.Append("</table>");
        }
        else
        {
            info.Append("<div><span style='color:maroon;padding:2px'> No record found !</span></div>");
        }
        return info.ToString();
    }

    public static void InsertDel_ActionDetails(int CrewId, int ScheduleID, int Del)
    {
        System.Text.StringBuilder info = new System.Text.StringBuilder();
        BLL_Dashboard.Dash_InsertDel_CrwAction_RefusedToSign(CrewId, ScheduleID, Del);

        //return info.ToString();
    }

    [WebMethod]
    public string Get_Vetting_Exp_In_Next_30Days(int UserID, string DateFormat)
    {
        UDCHyperLink alink = new UDCHyperLink();        
        alink.Target = "_blank";
        alink.NaviagteURL = "../Technical/Vetting/Vetting_Details.aspx";
        alink.QueryStringDataColumn = new string[] { "Vetting_ID" };
        alink.QueryStringText = new string[] { "Vetting_Id" };
        Dictionary<string, UDCHyperLink> diclink = new Dictionary<string, UDCHyperLink>();    
        diclink.Add("PLANNED_VETTING", alink);

        UDCHyperLink alink1 = new UDCHyperLink();
        alink1.Target = "_blank";
        alink1.NaviagteURL = "../Technical/Vetting/Vetting_Details.aspx";
        alink1.QueryStringDataColumn = new string[] { "LastVettingID" };
        alink1.QueryStringText = new string[] { "Vetting_Id" };      
        diclink.Add("LAST_VETTING", alink1);

        List<UDCAction> dicAction = new List<UDCAction>();
        dicAction.Add(new UDCAction(new string[][] { new string[] { "CreateNewVetting", "onclick" } }, "../Images/UplannedClock.png", new string[] { "Vetting_Type_ID" }));        
      
        DataTable dt = BLL_Dashboard.Dash_Get_Vetting_Exp_In_Next_30Days(UserID);
        UDFLib.ChangeColumnDataType(dt, "LAST_VETTING", typeof(string));
        UDFLib.ChangeColumnDataType(dt, "EXPIRATION_DATE", typeof(string));
        UDFLib.ChangeColumnDataType(dt, "PLANNED_VETTING", typeof(string));       
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            dt.Rows[i]["LAST_VETTING"] = UDFLib.ConvertUserDateFormat(Convert.ToString(dt.Rows[i]["LAST_VETTING"]), DateFormat);
            dt.Rows[i]["EXPIRATION_DATE"] = UDFLib.ConvertUserDateFormat(Convert.ToString(dt.Rows[i]["EXPIRATION_DATE"]), DateFormat);
            dt.Rows[i]["PLANNED_VETTING"] = UDFLib.ConvertUserDateFormat(Convert.ToString(dt.Rows[i]["PLANNED_VETTING"]), DateFormat);          
        }

        return UDFLib.CreateHtmlTableFromDataTable(dt,
                new string[] { "Vessel Name", "Vetting Type", "Last Vetting", "Expiration Date", "Planned Vetting","Action"},
                new string[] { "VESSEL_NAME", "VETTING_TYPE", "LAST_VETTING", "EXPIRATION_DATE", "PLANNED_VETTING"},
                diclink,
                dicAction,
                new Dictionary<string, UDCToolTip>(),
                new string[] { },
                "tbl-common-Css",
                "hdr-common-Css",
                "row-common-Css",
                "row-common-Css");
        
    }

    [WebMethod]
    public string Get_Exp_Failed_Vetting_Insp(int UserID, string DateFormat)
    {
        UDCHyperLink alink = new UDCHyperLink();     
        alink.Target = "_blank";
        alink.NaviagteURL = "../Technical/Vetting/Vetting_Details.aspx";
        alink.QueryStringDataColumn = new string[] { "Vetting_ID" };
        alink.QueryStringText = new string[] { "Vetting_Id" };
        Dictionary<string, UDCHyperLink> diclink = new Dictionary<string, UDCHyperLink>();        
        diclink.Add("PLANNED_VETTING", alink);

        UDCHyperLink alink1 = new UDCHyperLink();
        alink1.Target = "_blank";
        alink1.NaviagteURL = "../Technical/Vetting/Vetting_Details.aspx";
        alink1.QueryStringDataColumn = new string[] { "LastVettingID" };
        alink1.QueryStringText = new string[] { "Vetting_Id" };
        diclink.Add("LAST_VETTING", alink1); 
      
        List<UDCAction> dicAction = new List<UDCAction>();      
        dicAction.Add(new UDCAction(new string[][] { new string[] { "CreateNewVetting", "onclick" } }, "../Images/UplannedClock.png", new string[] { "Vetting_Type_ID" }));

        DataTable dt = BLL_Dashboard.Dash_Get_Exp_Failed_Vetting_Insp(UserID);
        UDFLib.ChangeColumnDataType(dt, "LAST_VETTING", typeof(string));      
        UDFLib.ChangeColumnDataType(dt, "PLANNED_VETTING", typeof(string));
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            dt.Rows[i]["LAST_VETTING"] = UDFLib.ConvertUserDateFormat(Convert.ToString(dt.Rows[i]["LAST_VETTING"]), DateFormat);
            dt.Rows[i]["PLANNED_VETTING"] = UDFLib.ConvertUserDateFormat(Convert.ToString(dt.Rows[i]["PLANNED_VETTING"]), DateFormat);
        }
        return UDFLib.CreateHtmlTableFromDataTable(dt,
                new string[] { "Vessel Name", "Vetting Type", "Last Vetting", "Status", "Planned Vetting" ,"Action"},
                new string[] { "VESSEL_NAME", "VETTING_TYPE", "LAST_VETTING", "STATUS", "PLANNED_VETTING" },
                diclink,
                dicAction,
                new Dictionary<string, UDCToolTip>(),
                new string[] { "left", "left", "left", "left", "left", "left" },
                "tbl-common-Css",
                "hdr-common-Css",
                "row-common-Css",
                "row-common-Css");

    }

    #region for PO and Invoice Sinppet
    [WebMethod]
    public string asyncGet_POAndInvoice_SummarySnippet()
    {

        DataTable dt = BLL_Dashboard.Get_POAndInvoice_SummarySnippet().Tables[0];
        System.Text.StringBuilder info = new System.Text.StringBuilder();

        if (dt.Rows.Count > 0)
        {

            dt = dt.DefaultView.ToTable();

            info.Append("<table id='__tbl_Summary' CELLPADDING='2' CELLSPACING='0'  style='border-collapse:collapse;table-layout:fixed;' width='444px'>");
            info.Append("<tr>");
            info.Append("<td  class='hdr-common-Css' style='Text-align:left;border-bottom:1px solid #006699;border-right:1px solid grey;'>Stage</td>");
            info.Append("<td  width='auto' class='hdr-common-Css' style='Text-align:left;border-bottom:1px solid #006699;border-right:1px solid grey;word-wrap:break-word;'>VER</td>");
            info.Append("<td  width='auto' class='hdr-common-Css' style='Text-align:left;border-bottom:1px solid #006699;border-right:1px solid grey;word-wrap:break-word;'>APV</td>");
            info.Append("<td  width='auto' class='hdr-common-Css' style='Text-align:left;border-bottom:1px solid grey;border-right:1px solid grey;word-wrap:break-word;'>OVD</td>");
            info.Append("<td  width='auto' class='hdr-common-Css' style='Text-align:left;border-bottom:1px solid #006699;border-right:1px solid grey;word-wrap:break-word;'>RWK</td>");
            info.Append("<td  width='auto' class='hdr-common-Css' style='Text-align:left;border-bottom:1px solid #006699;border-right:1px solid grey;word-wrap:break-word;'>DISP</td>");
            info.Append("<td  width='auto' class='hdr-common-Css' style='Text-align:left;border-bottom:1px solid #006699;border-right:1px solid grey;word-wrap:break-word;'>OHLD</td>");
            info.Append("<td  width='auto' class='hdr-common-Css' style='Text-align:left;border-bottom:1px solid #006699;border-right:1px solid grey;word-wrap:break-word;'>ADV</td>");
            info.Append("<td  width='auto' class='hdr-common-Css' style='Text-align:left;border-bottom:1px solid #006699;border-right:1px solid grey;word-wrap:break-word;'>WHLD</td>");
            info.Append("</tr>");

            foreach (DataRow dr in dt.Rows)
            {



                info.Append("<tr>");
                info.Append("<td class='row-common-Css' width='148px'>" + dr["Stage"] + "</td>");
                info.Append("<td class='row-common-Css' width='37px'>" + dr["Verification"] + "</td>");
                info.Append("<td class='row-common-Css' width='37px'>" + dr["Approval"] + "</td>");
                info.Append("<td class='row-common-Css' width='37px'>" + dr["Overdue"] + "</td>");
                info.Append("<td class='row-common-Css' width='37px'>" + dr["Rework"] + "</td>");
                info.Append("<td class='row-common-Css' width='37px'>" + dr["Dispute"] + "</td>");
                info.Append("<td class='row-common-Css' width='37px'>" + dr["OnHold"] + "</td>");
                info.Append("<td class='row-common-Css' width='37px'>" + dr["Advance"] + "</td>");
                info.Append("<td class='row-common-Css' width='37px'>" + dr["Withhold"] + "</td>");
                info.Append("</tr>");


            }


            info.Append("</table>");
        }
        else
        {
            info.Append("<table id='__tbl_Summary' CELLPADDING='2' CELLSPACING='0'  style='border-collapse:collapse;table-layout:fixed;' width='444px'>");
            info.Append("<tr>");
            info.Append("<td>No records found</td>");
            info.Append("</tr>");
            info.Append("</table>");

        }

        return info.ToString();



    }

    [WebMethod]
    public string Get_Pending_POApprovals_Snippet(int UserID, string DateFormat)
    {

        DataTable dt = BLL_Dashboard.Get_Pending_POApprovals_Snippet(UserID);
        System.Text.StringBuilder info = new System.Text.StringBuilder();

        if (dt.Rows.Count > 0)
        {
            dt.DefaultView.Sort = "Date desc";
            dt = dt.DefaultView.ToTable();

            info.Append("<table id='__tbl_PO' CELLPADDING='2' CELLSPACING='0'  style='border-collapse:collapse;' width='444px'>");
            info.Append("<tr>");
            info.Append("<td  class='hdr-common-Css' style='Text-align:left;border-bottom:1px solid #006699;border-right:1px solid grey;word-wrap:break-word;'>REQ Number</td>");
            info.Append("<td  width='auto' class='hdr-common-Css' style='Text-align:left;border-bottom:1px solid #006699;border-right:1px solid grey;word-wrap:break-word;'>PO issued BY</td>");
            info.Append("<td  width='auto' class='hdr-common-Css' style='Text-align:left;border-bottom:1px solid #006699;border-right:1px solid grey;word-wrap:break-word;'>Date</td>");
            info.Append("<td  width='auto' class='hdr-common-Css' style='Text-align:left;border-bottom:1px solid grey;border-right:1px solid grey;word-wrap:break-word;'>Supplier Name</td>");
            info.Append("<td  width='auto' class='hdr-common-Css' style='Text-align:left;border-bottom:1px solid #006699;border-right:1px solid grey;word-wrap:break-word;'>PO Value</td>");
            info.Append("</tr>");

            foreach (DataRow dr in dt.Rows)
            {


                string strName = string.Empty;
                string strType = string.Empty;

                if (dr["REQ_Number"].ToString().Length > 0)
                {
                    strName = dr["REQ_Number"].ToString();
                }


                if (dr["LinkType"].ToString().Length > 0)
                {
                    strType = dr["LinkType"].ToString();
                }


                info.Append("<tr>");
                info.Append("<td class='row-common-Css' style='line-height:19px;text-align:left;' width='90px'>");
               
                if (strType == "OCA")
                {
                    info.Append("<a ID='OCAREQID' href='../account/OCA.ASPX?PageName=PO_LOG/Main.asp" + "&PO_Number=" + dr["REQ_Number"] + "' runat='server' Target='_blank' title='" + dr["REQ_Number"].ToString() + "'>" + strName + "&nbsp; " + "</a>");
                }
                if (strType == "JIBE")
                {
                    info.Append("<a ID='JIBEREQID' href='../Purchase/QuatationEvalution.aspx?Requisitioncode=" + dr["REQ_Number"] + "&Document_Code=" + dr["DOCUMENT_CODE"] + "&Vessel_Code=" + dr["Vessel_Code"] + "&QUOTATION_CODE=" + dr["QUOTATION_CODE"] + "' runat='server' Target='_blank' title='" + dr["REQ_Number"].ToString() + "'>" + strName + "&nbsp; " + "</a>");
                }
                info.Append("</td>");
                info.Append("<td class='row-common-Css' width='120px'>" + dr["PO_issued_BY"] + "</td>");
                info.Append("<td class='row-common-Css' width='84px'>");
                info.Append(Convert.ToString(dr["Date"]) != "" ? UDFLib.ConvertUserDateFormat(Convert.ToString(dr["Date"]), DateFormat) : dr["Date"]);
                info.Append("</td>");
                info.Append("<td class='row-common-Css' width='120px'>" + dr["Supplier_Name"] + "</td>");

                info.Append("<td class='row-common-Css' width='30px'>" + dr["PO_Value"] + "</td>");
                info.Append("</tr>");


            }


            info.Append("</table>");
        }
        else
        {
            info.Append("<table id='__tbl_PO' CELLPADDING='2' CELLSPACING='0'  style='border-collapse:collapse;table-layout:fixed;' width='444px'>");
            info.Append("<tr>");
            info.Append("<td>No records found</td>");
            info.Append("</tr>");
            info.Append("</table>");

        }

        return info.ToString();

    }

    [WebMethod]
    public string Get_Pending_InvoiceVerification_Snippet(int UserID, string DateFormat)
    {

        DataTable dt = BLL_Dashboard.Get_Pending_InvoiceVerification_Snippet(UserID);
        System.Text.StringBuilder info = new System.Text.StringBuilder();

        if (dt.Rows.Count > 0)
        {
            dt.DefaultView.Sort = "Payment_Due_Date desc";
            dt = dt.DefaultView.ToTable();

            info.Append("<table id='__tbl_Invoice_V' CELLPADDING='2' CELLSPACING='0'  style='border-collapse:collapse;' width='430px'>");
            info.Append("<tr>");
            info.Append("<td  class='hdr-common-Css' style='Text-align:left;border-bottom:1px solid #006699;border-right:1px solid grey;word-wrap:break-word;'>PO Number</td>");
            info.Append("<td  width='auto' class='hdr-common-Css' style='Text-align:left;border-bottom:1px solid #006699;border-right:1px solid grey;word-wrap:break-word;'>Supplier Name</td>");
            info.Append("<td  width='auto' class='hdr-common-Css' style='Text-align:left;border-bottom:1px solid #006699;border-right:1px solid grey;word-wrap:break-word;'>PO Value</td>");
            info.Append("<td  width='auto' class='hdr-common-Css' style='Text-align:left;border-bottom:1px solid grey;border-right:1px solid grey;word-wrap:break-word;'>Invoice no.</td>");
            info.Append("<td  width='auto' class='hdr-common-Css' style='Text-align:left;border-bottom:1px solid #006699;border-right:1px solid grey;word-wrap:break-word;'>Invoice Value</td>");
            info.Append("<td  width='auto' class='hdr-common-Css' style='Text-align:left;border-bottom:1px solid #006699;border-right:1px solid grey;word-wrap:break-word;'>Payment Due Date</td>");
            info.Append("</tr>");

            foreach (DataRow dr in dt.Rows)
            {

                string todaysdate = DateTime.Today.ToString("dd/MMMM/yyyy");
                string Overdue = "";

                string PaymentDuedate = dr["Payment_Due_Date"].ToString();
                if (PaymentDuedate != "")
                {
                    PaymentDuedate = Convert.ToDateTime(PaymentDuedate).ToString("dd/MMMM/yyyy");
                    if (Convert.ToDateTime(PaymentDuedate) < Convert.ToDateTime(todaysdate))
                    {
                        Overdue = "Yes";
                    }
                }
               

                string strName = string.Empty;

                if (dr["PO_Number"].ToString().Length > 0)
                {
                    strName = dr["PO_Number"].ToString();
                }

                info.Append("<tr>");
                info.Append("<td class='row-common-Css' style='line-height:19px;text-align:left;' width='106px'>");
                
                if (dr["Submitted_OnLine"].ToString() == "Yes")
                {
                    info.Append("<a ID='OCAREQID' href='../account/OCA.ASPX?PageName=PO_LOG/Process_Supplier_Online_Invoice.asp" + "&Invoice_Number=" + dr["Invoice_no"] + "' runat='server' Target='_blank' title='" + dr["PO_Number"].ToString() + "'>" + strName + "&nbsp; " + "</a>");
                }
                else
                {
                    info.Append("<a ID='OCAREQID' href='../account/OCA.ASPX?PageName=PO_LOG/Main.asp" + "&Invoice_Number=" + dr["Invoice_no"] + "' runat='server' Target='_blank' title='" + dr["PO_Number"].ToString() + "'>" + strName + "&nbsp; " + "</a>");
                }
                info.Append("</td>");
                info.Append("<td class='row-common-Css' width='140px'>" + dr["Supplier_Name"] + "</td>");
                info.Append("<td class='row-common-Css' width='30px'>" + dr["PO_Value"] + "</td>");

                info.Append("<td class='row-common-Css' width='40px'>" + dr["Invoice_no"] + "</td>");
                info.Append("<td class='row-common-Css' width='30px'>" + dr["Invoice_Value"] + "</td>");

                if (Overdue == "Yes")
                {
                    info.Append("<td class='row-common-Css' width='84px' style='color:white; background-color:red;font-weight :normal;'>");
                    info.Append(Convert.ToString(dr["Payment_Due_Date"]) != "" ? UDFLib.ConvertUserDateFormat(Convert.ToString(dr["Payment_Due_Date"]), DateFormat) : dr["Payment_Due_Date"]);
                    info.Append("</td>");
                }
                else
                {
                    info.Append("<td class='row-common-Css' width='84px'>");
                    info.Append(Convert.ToString(dr["Payment_Due_Date"]) != "" ? UDFLib.ConvertUserDateFormat(Convert.ToString(dr["Payment_Due_Date"]), DateFormat) : dr["Payment_Due_Date"]);
                    info.Append("</td>");
                }
                
                info.Append("</tr>");


            }


            info.Append("</table>");
        }
        else
        {
            info.Append("<table id='__tbl_Invoice_V' CELLPADDING='2' CELLSPACING='0'  style='border-collapse:collapse;table-layout:fixed;' width='430px'>");
            info.Append("<tr>");
            info.Append("<td>No records found</td>");
            info.Append("</tr>");
            info.Append("</table>");

        }

        return info.ToString();

    }

    [WebMethod]
    public string Get_Pending_InvoiceApprovals_Snippet(int UserID, string DateFormat)
    {

        DataTable dt = BLL_Dashboard.Get_Pending_InvoiceApprovals_Snippet(UserID);
        System.Text.StringBuilder info = new System.Text.StringBuilder();

        if (dt.Rows.Count > 0)
        {
            dt.DefaultView.Sort = "Payment_Due_Date desc";
            dt = dt.DefaultView.ToTable();

            info.Append("<table id='__tbl_Invoice_A' CELLPADDING='2' CELLSPACING='0'  style='border-collapse:collapse;table-layout:fixed;' width='430px'>");
            info.Append("<tr>");
            info.Append("<td  class='hdr-common-Css' style='Text-align:left;border-bottom:1px solid #006699;border-right:1px solid grey;word-wrap:break-word;'>PO Number</td>");
            info.Append("<td  width='auto' class='hdr-common-Css' style='Text-align:left;border-bottom:1px solid #006699;border-right:1px solid grey;word-wrap:break-word;'>Invoice Type</td>");
            info.Append("<td  width='auto' class='hdr-common-Css' style='Text-align:left;border-bottom:1px solid #006699;border-right:1px solid grey;word-wrap:break-word;'>Supplier Name</td>");          
            info.Append("<td  width='auto' class='hdr-common-Css' style='Text-align:left;border-bottom:1px solid #006699;border-right:1px solid grey;word-wrap:break-word;'>PO Value</td>");
            info.Append("<td  width='auto' class='hdr-common-Css' style='Text-align:left;border-bottom:1px solid grey;border-right:1px solid grey;word-wrap:break-word;'>Invoice no.</td>");
            info.Append("<td  width='auto' class='hdr-common-Css' style='Text-align:left;border-bottom:1px solid #006699;border-right:1px solid grey;word-wrap:break-word;'>Invoice Value</td>");
            info.Append("<td  width='auto' class='hdr-common-Css' style='Text-align:left;border-bottom:1px solid #006699;border-right:1px solid grey;word-wrap:break-word;'>Payment Due Date</td>");
            info.Append("<td  width='auto' class='hdr-common-Css' style='Text-align:left;border-bottom:1px solid #006699;border-right:1px solid grey;word-wrap:break-word;'>Stage</td>");
            info.Append("</tr>");

            foreach (DataRow dr in dt.Rows)
            {

                string todaysdate = DateTime.Today.ToString("dd/MMMM/yyyy");
                string Overdue = "";

                string PaymentDuedate = dr["Payment_Due_Date"].ToString();
                if (PaymentDuedate != "")
                {
                    PaymentDuedate = Convert.ToDateTime(PaymentDuedate).ToString("dd/MMMM/yyyy");
                    if (Convert.ToDateTime(PaymentDuedate) < Convert.ToDateTime(todaysdate))
                    {
                        Overdue = "Yes";
                    }
                }

                string strName = string.Empty;

                if (dr["PO_Number"].ToString().Length > 0)
                {
                    strName = dr["PO_Number"].ToString();
                }

                info.Append("<tr>");
                info.Append("<td class='row-common-Css' style='line-height:19px;text-align:left;' width='90px'>");
                
                if (dr["Stage"].ToString() == "Pending Invoice Approval")
                {
                    info.Append("<a ID='OCAREQID' href='../account/OCA.ASPX?PageName=PO_LOG/Invoice_Pending_Approval.asp" + "&Invoice_Number=" + dr["Invoice_no"] + "' runat='server' Target='_blank' title='" + dr["PO_Number"].ToString() + "'>" + strName + "&nbsp; " + "</a>");
                }
                if (dr["Stage"].ToString() == "Pending Invoice Final Approval")
                {
                    info.Append("<a ID='OCAREQID' href='../account/OCA.ASPX?PageName=PO_LOG/INVOICE_FINAL_APPROVAL.asp" + "&Invoice_Number=" + dr["Invoice_no"] + "' runat='server' Target='_blank' title='" + dr["PO_Number"].ToString() + "'>" + strName + "&nbsp; " + "</a>");
                }
                if (dr["Stage"].ToString() == "Pending Payment Approval")
                {
                    info.Append("<a ID='OCAREQID' href='../account/OCA.ASPX?PageName=PO_LOG/Invoice_Payment_Approval_main.asp" + "&Invoice_Number=" + dr["Invoice_no"] + "' runat='server' Target='_blank' title='" + dr["PO_Number"].ToString() + "'>" + strName + "&nbsp; " + "</a>");
                }
                info.Append("</td>");
                info.Append("<td class='row-common-Css' width='30px'>" + dr["Invoice_Type"] + "</td>");
                info.Append("<td class='row-common-Css' width='90px'>" + dr["Supplier_Name"] + "</td>");
                info.Append("<td class='row-common-Css' width='30px'>" + dr["PO_Value"] + "</td>");

                info.Append("<td class='row-common-Css' width='30px'>" + dr["Invoice_no"] + "</td>");
                info.Append("<td class='row-common-Css' width='30px'>" + dr["Invoice_Value"] + "</td>");

                if (Overdue == "Yes")
                {
                    info.Append("<td class='row-common-Css' width='50px' style='color:white; background-color:red;font-weight :normal;'>");
                    info.Append(Convert.ToString(dr["Payment_Due_Date"]) != "" ? UDFLib.ConvertUserDateFormat(Convert.ToString(dr["Payment_Due_Date"]), DateFormat) : dr["Payment_Due_Date"]);
                    info.Append("</td>");
                }
                else
                {
                    info.Append("<td class='row-common-Css' width='50px'>");
                    info.Append(Convert.ToString(dr["Payment_Due_Date"]) != "" ? UDFLib.ConvertUserDateFormat(Convert.ToString(dr["Payment_Due_Date"]), DateFormat) : dr["Payment_Due_Date"]);
                    info.Append("</td>");
                }

                info.Append("<td class='row-common-Css' width='90px'>" + dr["Stage"] + "</td>");

                info.Append("</tr>");


            }


            info.Append("</table>");
        }
        else
        {
            info.Append("<table id='__tbl_Invoice_A' CELLPADDING='2' CELLSPACING='0'  style='border-collapse:collapse;' width='430px'>");
            info.Append("<tr>");
            info.Append("<td>No records found</td>");
            info.Append("</tr>");
            info.Append("</table>");

        }

        return info.ToString();

    }

   
    #endregion

}
public class CustomDDLProperties
{
    public string DataValue { get; set; }
    public string DataText { get; set; }

}

