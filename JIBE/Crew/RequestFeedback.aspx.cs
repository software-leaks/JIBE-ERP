using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.Crew;
using SMS.Business.Infrastructure;
using SMS.Properties;
using System.Configuration;

public partial class Crew_RequestFeedback : System.Web.UI.Page
{
    BLL_Crew_CrewDetails objCrew = new BLL_Crew_CrewDetails();
    BLL_Infra_VesselLib objVessel = new BLL_Infra_VesselLib();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            UserAccessValidation();
            Load_UserList();

            FillGridViewAfterSearch();

        }

        //string msg1 = String.Format("$('.sailingInfo').SailingInfo();");
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgshowdetails", msg1, true);
    }

    protected void Load_UserList()
    {
        BLL_Infra_UserCredentials objInfra = new BLL_Infra_UserCredentials();

        int UserCompanyID = int.Parse(getSessionString("USERCOMPANYID"));
        DataTable dtUsers = objInfra.Get_UserList(UserCompanyID);
        GridView_UserList.DataSource = dtUsers;
        GridView_UserList.DataBind();
        //lstUser.Items.Insert(0, new ListItem("-SELECT ALL -", "0"));
    }

    protected string getSessionString(string ID)
    {
        string ret = "";
        if (Session[ID] != null)
        {
            ret = Session[ID].ToString();
        }
        else
        {
            Response.Redirect("~/Account/Login.aspx");
        }

        return ret;
    }

    public void FillGridViewAfterSearch()
    {
        int FleetID = 0;
        int VesselID = 0;
        int Nationality = 0;
        int RankID = 0;
        string SearchText = "";
        int Status = 0;
        int CalculatedStatus = 0;

        int ManningOfficeID = 0;
        int COCDueIn = 0;
        string JoiningFrom = "";
        string JoiningTo = "";

        VesselID = UDFLib.ConvertToInteger(Request.QueryString["vid"]);
        FleetID = UDFLib.ConvertToInteger(Request.QueryString["flt"]);
        Nationality = UDFLib.ConvertToInteger(Request.QueryString["nat"]);
        RankID = UDFLib.ConvertToInteger(Request.QueryString["rank"]);
        SearchText = Request.QueryString["search"];
        Status = UDFLib.ConvertToInteger(Request.QueryString["st"]);
        CalculatedStatus = UDFLib.ConvertToInteger(Request.QueryString["cst"]);

        ManningOfficeID = UDFLib.ConvertToInteger(Request.QueryString["mo"]);
        COCDueIn = UDFLib.ConvertToInteger(Request.QueryString["coc"]);
        JoiningFrom = Request.QueryString["jFrom"];
        JoiningTo = Request.QueryString["jTo"];
        int VesselOwnerID = 0;


        DataTable dtFilters = new DataTable();
        dtFilters.Columns.Add("VesselManager", typeof(int));
        dtFilters.Columns.Add("Fleet", typeof(int));
        dtFilters.Columns.Add("Vessel", typeof(int));
        dtFilters.Columns.Add("RankID", typeof(int));
        dtFilters.Columns.Add("Nationality", typeof(int));
        dtFilters.Columns.Add("Status", typeof(int));
        dtFilters.Columns.Add("CalculatedStatus", typeof(int));

        dtFilters.Columns.Add("ManningOfficeID", typeof(int));
        dtFilters.Columns.Add("EOCDueIn", typeof(int));
        dtFilters.Columns.Add("JoiningDateFrom", typeof(String));
        dtFilters.Columns.Add("JoiningDateTo", typeof(String));
        dtFilters.Columns.Add("SearchText", typeof(String));

        DateTime dtFrom = DateTime.Parse("1900/01/01");
        DateTime dtTo = DateTime.Parse("2900/01/01");

        dtFilters.Rows.Add(VesselOwnerID, FleetID, VesselID, RankID, Nationality, Status,CalculatedStatus, ManningOfficeID, COCDueIn, dtFrom.ToString("yyyy/MM/dd"), dtTo.ToString("yyyy/MM/dd"), SearchText);

        int PAGE_SIZE = ucCustomPager_CrewList.PageSize;
        int PAGE_INDEX = ucCustomPager_CrewList.CurrentPageIndex;

        int SelectRecordCount = ucCustomPager_CrewList.isCountRecord;

        DataTable dt = BLL_Crew_CrewList.Get_Crewlist_Index(dtFilters, GetSessionUserID(), PAGE_SIZE, PAGE_INDEX, ref SelectRecordCount);

        if (ucCustomPager_CrewList.isCountRecord == 1)
        {
            ucCustomPager_CrewList.CountTotalRec = SelectRecordCount.ToString();
            ucCustomPager_CrewList.BuildPager();
        }

        GridView_CrewList.DataSource = dt;
        GridView_CrewList.DataBind();        
    }

    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }

    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
        UserAccess objUA = new UserAccess();

        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
            Response.Redirect("~/default.aspx?msgid=1");

        if (objUA.Add == 0)
        {
        }
        if (objUA.Edit == 0)
        {
        }
        if (objUA.Delete == 0)
        {
        }
        if (objUA.Approve == 0)
        {
        }
        //-- MANNING OFFICE LOGIN --
       
    }

    protected void btnSendRequest_Click(object sender, EventArgs e)
    {
        SendMail_FeedbackRequest();
    }

    protected void SendMail_FeedbackRequest()
    {
        string msgTo = "";
        string msgCC = "";
        string msgBCC = "";
        string msgBody = "";
        string msgSubject = "Request for Feedback";
        int usercount = 0;

        foreach (GridViewRow row in GridView_UserList.Rows)
        {
            CheckBox chkSelect = (CheckBox)row.Cells[0].FindControl("chkSelect");
            HiddenField hdnMailID = (HiddenField)row.Cells[0].FindControl("hdnEmailID");
            Label lblUserName = (Label)row.FindControl("lblUserName");
            if (chkSelect != null && hdnMailID != null)
            {
                if (chkSelect.Checked==true && hdnMailID.Value != "")
                {
                    if (msgTo.Length > 0)
                        msgTo += ";";

                    msgTo += hdnMailID.Value;
                    usercount++;
                    msgBody = "Dear " + lblUserName.Text + ",<br><br>";
                }
            }
        }

        if (usercount > 1)
            msgBody = "Dear All,<br><br>";
        
        if (usercount > 0)
        {
            msgBody += "Please give your feedback for the following sea staff:";
            msgBody += "<br><br>";
            msgBody += Get_CrewListTable();
            msgBody += "<br>";
            msgBody += "Best Regards,<br>";
            msgBody += Session["USERFULLNAME"].ToString();

            int MsgID = objCrew.Send_CrewNotification(0, 0, 0, 10, msgTo, msgCC, msgBCC, msgSubject, msgBody, "", "MAIL", "", GetSessionUserID(), "READY");
            if (MsgID > 0)
            {
                string js = "alert('Feedback Request Sent to the selected user(s).');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "CrewNotification" + MsgID, js, true);
            }
        }
    }

    protected string Get_CrewListTable()
    {
        string msgBody="<table style='border:1px solid gray;'>";
        int count =1;

        msgBody += "<tr style='border:1px solid gray;background-color:gray;'><td></td><td>Vessel</td><td>Staff Code</td><td>Rank</td><td>Staff Name</td></tr>";

        foreach (GridViewRow row in GridView_CrewList.Rows)
        {

            CheckBox chkSelect = (CheckBox ) row.Cells[0].FindControl("chkSelect");
            HiddenField hdnID = (HiddenField)row.Cells[0].FindControl("hdnCrewID");
            HiddenField hdnStaffCode = (HiddenField)row.Cells[0].FindControl("hdnStaffCode");
            
            Label lblONBD = (Label)row.FindControl("lblONBD");
            Label lblRank = (Label)row.FindControl("lblRank");
            Label lblSTAFF_NAME = (Label)row.FindControl("lblSTAFF_NAME");
            
            if (chkSelect != null && hdnID != null)
            {
                if (chkSelect.Checked == true)
                {
                    msgBody += "<tr style='border:1px solid gray;background-color:#EFF5FB;'>";
                    msgBody += "<td>" + count.ToString() + "</td>";
                    msgBody += "<td>" + lblONBD.Text + "</td>";

                    string querystring = UDFLib.Encrypt("id=" + hdnID.Value + "&Tab=8"  );
                    msgBody += "<td><a href='http://" + Request.ServerVariables["SERVER_NAME"].ToString() + "/" + ConfigurationManager.AppSettings["APP_NAME"].ToUpper() + "/crew/crewdetails.aspx" + querystring + "'>" + hdnStaffCode.Value + "</a></td>";
                    
                    msgBody += "<td>" + lblRank.Text + "</td>";
                    msgBody += "<td>" + lblSTAFF_NAME.Text + "</td>";
                    msgBody += "</tr>";
                    count++;
                }
            }
        }
        msgBody += "</table>";
        return msgBody;
    }
}