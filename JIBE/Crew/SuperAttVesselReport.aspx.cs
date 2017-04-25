using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Infrastructure;
using SMS.Business.Crew;
using System.Data;

public partial class Crew_SuperAttVesselReport : System.Web.UI.Page
{
    BLL_Infra_VesselLib objVessel = new BLL_Infra_VesselLib();
    BLL_Crew_CrewDetails objCrew = new BLL_Crew_CrewDetails();
    public string DateFormatMessage = "";

    decimal DueTotal = 0;
    decimal ApprovedTotal = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["USERID"] == null)
            Response.Redirect("~/account/Login.aspx");

        CalendarExtender5.Format = Convert.ToString(Session["User_DateFormat"]);
        CalendarExtender1.Format = Convert.ToString(Session["User_DateFormat"]);
        DateFormatMessage = UDFLib.DateFormatMessage();

        if (!IsPostBack)
        {
            UserAccessValidation();
            Load_FleetList();
            Load_VesselList();
            Load_Report();
        }
    }

    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        SMS.Properties.UserAccess objUA = new SMS.Properties.UserAccess();
        SMS.Business.Infrastructure.BLL_Infra_UserCredentials objUser = new SMS.Business.Infrastructure.BLL_Infra_UserCredentials();
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

    }

    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
        {
            Response.Redirect("~/account/Login.aspx");
            return 0;
        }
    }

    public void Load_FleetList()
    {
        int UserCompanyID = UDFLib.ConvertToInteger(Session["USERCOMPANYID"].ToString());
        ddlFleet.DataSource = objVessel.GetFleetList(UserCompanyID);
        ddlFleet.DataTextField = "NAME";
        ddlFleet.DataValueField = "CODE";
        ddlFleet.DataBind();
        ddlFleet.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));
    }

    /// <summary>
    /// Get Session data
    /// </summary>
    /// <param name="SessionField"></param>
    /// <returns></returns>
    private string getSessionString(string SessionField)
    {
        try
        {
            if (Session[SessionField] != null && Session[SessionField].ToString() != "")
            {
                return Session[SessionField].ToString();
            }
            else
                return "";
        }
        catch
        {
            return "";
        }
    }
    public void Load_VesselList()
    {
        int Fleet_ID = UDFLib.ConvertToInteger(ddlFleet.SelectedValue);
        int UserCompanyID = UDFLib.ConvertToInteger(getSessionString("USERCOMPANYID"));
        DataTable dt = objVessel.Get_VesselList(Fleet_ID, 0, 0, "", UserCompanyID);

        ddlVessel.DataSource = dt;
        ddlVessel.DataTextField = "VESSEL_NAME";
        ddlVessel.DataValueField = "VESSEL_ID";
        ddlVessel.DataBind();
        ddlVessel.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));
        ddlVessel.SelectedIndex = 0;
    }

    protected void ddlFleet_SelectedIndexChanged(object sender, EventArgs e)
    {
        Load_VesselList();
        Load_Report();
    }

    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        if (txtSignOnFrom.Text != "")
        {
            if (!UDFLib.DateCheck(txtSignOnFrom.Text))
            {
                string js = "alert('Enter valid Sign-On From Date" + DateFormatMessage + "');";  
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUser", js, true);
                return;
            }
        }

        if (txtSignOnTo.Text != "")
        {
            if (!UDFLib.DateCheck(txtSignOnTo.Text))
            {
                string js = "alert('Enter valid Sign-On To Date" + DateFormatMessage + "');";  
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUser", js, true);
                return;
            }
        }

        Load_Report();
    }

    protected void BtnClearFilter_Click(object sender, EventArgs e)
    {
        ddlFleet.SelectedIndex = 0;
        ddlVessel.SelectedIndex = 0;
        txtSignOnFrom.Text = "";
        txtSignOnTo.Text = "";
        txtSearch.Text = "";
        Load_Report();
    }

    protected void GridView_Crew_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string Remarks;

        Image ImgRemarks = (Image)e.Row.FindControl("ImgRemarks");
        if (ImgRemarks != null)
        {
            Remarks = "Approved By:" + DataBinder.Eval(e.Row.DataItem, "ApprovedBy").ToString() + "<br>Date : " + DataBinder.Eval(e.Row.DataItem, "Approved_Date", "{0:dd/MM/yyyy}").ToString() + "<br>";
            if (DataBinder.Eval(e.Row.DataItem, "Remarks") != null)
            {
                Remarks += "Remarks: " + DataBinder.Eval(e.Row.DataItem, "Remarks").ToString().Replace("\n", "<br>");
            }
            ImgRemarks.Attributes.Add("Title", "cssbody=[dvbdy1] cssheader=[dvhdr1] header=[Remarks:] body=[" + Remarks + "]");
        }

        Label lblDueAmt = (Label)e.Row.FindControl("lblDueAmt");
        if (lblDueAmt != null)
        {
            DueTotal += UDFLib.ConvertToDecimal(lblDueAmt.Text);
        }
        HiddenField hdnApprovedAmt = (HiddenField)e.Row.FindControl("hdnApprovedAmt");
        if (hdnApprovedAmt != null)
        {
            ApprovedTotal += UDFLib.ConvertToDecimal(hdnApprovedAmt.Value);
        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lblDueTotal = (Label)e.Row.FindControl("lblDueTotal");
            if (lblDueTotal != null)
            {
                lblDueTotal.Text = DueTotal.ToString();
            }
            Label lblApprovedTotal = (Label)e.Row.FindControl("lblApprovedTotal");
            if (lblApprovedTotal != null)
            {
                lblApprovedTotal.Text = ApprovedTotal.ToString();
            }
        }
        if (DataBinder.Eval(e.Row.DataItem, "Joining_Type") != null)
        {
            int Joining_Type = UDFLib.ConvertToInteger(DataBinder.Eval(e.Row.DataItem, "Joining_Type").ToString());
            if (Joining_Type == 4)
            {
                e.Row.CssClass = "Promoted-Voyage-Row";
            }
        }
    }

    protected void Load_Report()
    {
        int FleetCode = UDFLib.ConvertToInteger(ddlFleet.SelectedValue);
        int VesselID = UDFLib.ConvertToInteger(ddlVessel.SelectedValue);

        string Sign_On_From = "";
        string Sign_On_To = "";

        if (txtSignOnFrom.Text != "")
            Sign_On_From = UDFLib.ConvertToDate(Convert.ToString(txtSignOnFrom.Text), UDFLib.GetDateFormat()).ToString();

        if (txtSignOnTo.Text != "")
            Sign_On_To = UDFLib.ConvertToDate(Convert.ToString(txtSignOnTo.Text), UDFLib.GetDateFormat()).ToString();

        string SearchString = txtSearch.Text;

        int PAGE_SIZE = ucCustomPager_CrewList.PageSize;
        int PAGE_INDEX = ucCustomPager_CrewList.CurrentPageIndex;
        int SelectRecordCount = ucCustomPager_CrewList.isCountRecord;
        int TotalDays = 0;

        DataTable dt = BLL_Crew_CrewList.Get_Super_Att_Vessel_Report(FleetCode, VesselID, Sign_On_From, Sign_On_To, SearchString, GetSessionUserID(), PAGE_SIZE, PAGE_INDEX, ref SelectRecordCount, ref TotalDays);

        if (ucCustomPager_CrewList.isCountRecord == 1)
        {
            ucCustomPager_CrewList.CountTotalRec = SelectRecordCount.ToString();
            ucCustomPager_CrewList.BuildPager();
        }
        GridView_Crew.DataSource = dt;
        GridView_Crew.DataBind();
        lblGrandTotal.Text = TotalDays.ToString();
    }

    protected void ImgExportToExcel_Click(object sender, EventArgs e)
    {
        string Sign_On_From = "";
        string Sign_On_To = "";
        string SearchString = txtSearch.Text;
        int TotalDays = 0;
        int PAGE_SIZE = -1;
        int PAGE_INDEX = -1;
        int SelectRecordCount = 0;

        int FleetCode = UDFLib.ConvertToInteger(ddlFleet.SelectedValue);
        int VesselID = UDFLib.ConvertToInteger(ddlVessel.SelectedValue);
        
        if (txtSignOnFrom.Text != "")
            Sign_On_From = UDFLib.ConvertToDate(Convert.ToString(txtSignOnFrom.Text), UDFLib.GetDateFormat()).ToString();

        if (txtSignOnTo.Text != "")
            Sign_On_To = UDFLib.ConvertToDate(Convert.ToString(txtSignOnTo.Text), UDFLib.GetDateFormat()).ToString(); 

        DataTable dt = BLL_Crew_CrewList.Get_Super_Att_Vessel_Report(FleetCode, VesselID, Sign_On_From, Sign_On_To, SearchString, GetSessionUserID(), PAGE_SIZE, PAGE_INDEX, ref SelectRecordCount, ref TotalDays);
        dt.Columns.Add("Joining_Date1");
        dt.Columns.Add("Sign_On_Date1");
        dt.Columns.Add("Sign_Off_Date1");

        foreach (DataRow dr in dt.Rows)
        {
            if (dr["Joining_Date"].ToString() != "" && dr["Joining_Date"] != null)
            {
                dr["Joining_Date1"] ="&nbsp;"+ UDFLib.ConvertUserDateFormat(Convert.ToString(dr["Joining_Date"]), Convert.ToString(Session["User_DateFormat"]));
            }
            if (dr["Sign_On_Date"].ToString() != "" && dr["Sign_On_Date"] != null)
            {
                dr["Sign_On_Date1"] = "&nbsp;" + UDFLib.ConvertUserDateFormat(Convert.ToString(dr["Sign_On_Date"]));
            }

            if (dr["Sign_Off_Date"].ToString() != "" && dr["Sign_Off_Date"] != null)
            {
                dr["Sign_Off_Date1"] = "&nbsp;" + UDFLib.ConvertUserDateFormat(Convert.ToString(dr["Sign_Off_Date"]));
            }
            
        }
        dt.AcceptChanges();
        
        string[] HeaderCaptions = { "Vessel", "S/Code", "Name", "Rank", "Contract Date", "S/On Date", "S/Off Date", "Days ONBD" };
        string[] DataColumnsName = { "Vessel_Short_Name", "Staff_Code", "Staff_Name", "Rank_Short_Name", "Joining_Date1", "Sign_On_Date1", "Sign_Off_Date1", "DaysOnBoard" };


        GridViewExportUtil.ExportToExcel(dt, HeaderCaptions, DataColumnsName, "Superintendents_attending_vessels.xls", "Superintendents attending vessels Report");
    }
}

