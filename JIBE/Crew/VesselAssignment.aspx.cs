using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Crew;
using SMS.Business.Infrastructure;
using System.Data;
using SMS.Properties;

public partial class Crew_VesselAssignment : System.Web.UI.Page
{
    BLL_Infra_Country objCountry = new BLL_Infra_Country();
    BLL_Crew_CrewDetails objCrew = new BLL_Crew_CrewDetails();
    BLL_Crew_Admin objCrewAdmin = new BLL_Crew_Admin();
    BLL_Infra_VesselLib objVessel = new BLL_Infra_VesselLib();

    protected override void OnInit(EventArgs e)
    {
        try
        {
            base.Page.Header.Controls.Add(SetUserStyle.AddThemeInHeader());
            base.OnInit(e);
        }
        catch { }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        CalendarExtender1.Format = CalendarExtender4.Format = CalendarExtender2.Format = CalendarExtender3.Format = UDFLib.GetDateFormat();

        if (GetSessionUserID() == 0)
            Response.Redirect("~/account/login.aspx");




        if (!IsPostBack)
        {
            UserAccessValidation();

            txtFromDt.Text = DateTime.Today.ToString("dd/MM/yyyy");
            txtToDt.Text = DateTime.Today.AddMonths(3).ToString("dd/MM/yyyy");

            txtFromDt.Text = UDFLib.ConvertUserDateFormat(Convert.ToString(txtFromDt.Text));
            txtToDt.Text = UDFLib.ConvertUserDateFormat(Convert.ToString(txtToDt.Text));

            Load_ManningAgentList();
            Load_Nationality();
            Load_RankList();
            Load_FleetList();
            Load_VesselList();
            Load_VesselList_UA();
            Search_UnAssigned();
            Search_SigningOff();
            Bind_ChangeEvent();
            lblCurrentCrewList.Visible = false;

        }
        string js = "$('.vesselinfo').InfoBox();";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "initscript", js, true);

        //string msg1 = String.Format("$('.sailingInfo').SailingInfo();");
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgshowdetails", msg1, true);
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
            btnAssign.Enabled = false;
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

    protected int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }
    protected string getSessionString(string SessionField)
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

    protected void Load_ManningAgentList()
    {
        int UserCompanyID = 0;
        if (getSessionString("USERCOMPANYID") != "")
        {
            UserCompanyID = int.Parse(getSessionString("USERCOMPANYID"));
        }

        ddlManningOffice.DataSource = objCrew.Get_ManningAgentList(UserCompanyID);
        ddlManningOffice.DataTextField = "COMPANY_NAME";
        ddlManningOffice.DataValueField = "ID";
        ddlManningOffice.DataBind();
        ddlManningOffice.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));


    }
    protected void Load_Nationality()
    {
        DataTable dt = objCrew.Get_CrewNationality(GetSessionUserID());
        ddlNationality.DataSource = dt;
        ddlNationality.DataTextField = "COUNTRY";
        ddlNationality.DataValueField = "ID";
        ddlNationality.DataBind();
        ddlNationality.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));
        ddlNationality.SelectedIndex = 0;

        ddlNationality_SOff.DataSource = dt;
        ddlNationality_SOff.DataTextField = "COUNTRY";
        ddlNationality_SOff.DataValueField = "ID";
        ddlNationality_SOff.DataBind();
        ddlNationality_SOff.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));
        ddlNationality_SOff.SelectedIndex = 0;

    }
    protected void Load_RankList()
    {
        DataTable dt = objCrewAdmin.Get_RankList();

        ddlRank.DataSource = dt;
        ddlRank.DataTextField = "Rank_Short_Name";
        ddlRank.DataValueField = "ID";
        ddlRank.DataBind();
        ddlRank.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));
        ddlRank.SelectedIndex = 0;


        ddlRank_UA.DataSource = dt;
        ddlRank_UA.DataTextField = "Rank_Short_Name";
        ddlRank_UA.DataValueField = "ID";
        ddlRank_UA.DataBind();
        ddlRank_UA.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));
        ddlRank_UA.SelectedIndex = 0;

        ddlJoiningRank.DataSource = dt;
        ddlJoiningRank.DataTextField = "Rank_Short_Name";
        ddlJoiningRank.DataValueField = "ID";
        ddlJoiningRank.DataBind();
        ddlJoiningRank.Items.Insert(0, new ListItem("-SELECT-", "0"));
        ddlJoiningRank.SelectedIndex = 0;


        dt = null;
    }
    protected void Load_FleetList()
    {
        int UserCompanyID = UDFLib.ConvertToInteger(getSessionString("USERCOMPANYID"));
        ddlFleet.DataSource = objVessel.GetFleetList(UserCompanyID);
        ddlFleet.DataTextField = "NAME";
        ddlFleet.DataValueField = "CODE";
        ddlFleet.DataBind();
        ddlFleet.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));
    }
    public void Load_VesselList()
    {
        int Fleet_ID = int.Parse(ddlFleet.SelectedValue);
        int UserCompanyID = UDFLib.ConvertToInteger(getSessionString("USERCOMPANYID"));
        int Vessel_Manager = UDFLib.ConvertToInteger(getSessionString("USERCOMPANYID"));

        ddlVessel.DataSource = objVessel.Get_VesselList(Fleet_ID, 0, Vessel_Manager, "", UserCompanyID);

        ddlVessel.DataTextField = "VESSEL_SHORT_NAME";
        ddlVessel.DataValueField = "VESSEL_ID";
        ddlVessel.DataBind();
        ddlVessel.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));
        ddlVessel.SelectedIndex = 0;


    }
    protected void Load_VesselList_UA()
    {
        int UserCompanyID = UDFLib.ConvertToInteger(getSessionString("USERCOMPANYID"));
        int Vessel_Manager = UDFLib.ConvertToInteger(getSessionString("USERCOMPANYID"));

        ddlVessel_UA.DataSource = objVessel.Get_VesselList(0, 0, Vessel_Manager, "", UserCompanyID);
        ddlVessel_UA.DataTextField = "VESSEL_SHORT_NAME";
        ddlVessel_UA.DataValueField = "VESSEL_ID";
        ddlVessel_UA.DataBind();
        ddlVessel_UA.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));
        ddlVessel_UA.SelectedIndex = 0;

    }
    protected void ddlFleet_SelectedIndexChanged(object sender, EventArgs e)
    {
        Load_VesselList();
    }
    protected void ddlVessel_UA_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (int.Parse(ddlVessel_UA.SelectedValue) > 0 && UA_AvailableOptions.SelectedValue == "2")
            lblCurrentCrewList.Visible = true;
        else
            lblCurrentCrewList.Visible = false;

    }
    protected void UA_AvailableOptions_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (int.Parse(ddlVessel_UA.SelectedValue) > 0 && UA_AvailableOptions.SelectedValue == "2")
            lblCurrentCrewList.Visible = true;
        else
            lblCurrentCrewList.Visible = false;
    }
    protected void btnFindSignOffCrew_Click(object sender, EventArgs e)
    {
        if (!UDFLib.DateCheck(txtFromDt.Text))
        {            
            string js = "alert('Date is not in correct format.');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUser", js, true);
            return;
        }
        if (!UDFLib.DateCheck(txtToDt.Text))
        {
            string js = "alert('Date is not in correct format.');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUser", js, true);
            return;
        }
        Search_SigningOff();

        Bind_ChangeEvent();
    }
    protected void btnFindUnAssignedCrew_Click(object sender, EventArgs e)
    {
        if (!UDFLib.DateCheck(txtFromDt_UA.Text))
        {
            string js = "alert('Date is not in correct format.');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUser", js, true);
            return;
        }
        if (!UDFLib.DateCheck(txtToDt_UA.Text))
        {
            string js = "alert('Date is not in correct format.');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUser", js, true);
            return;
        }

        Search_UnAssigned();
    }
    protected void btnClearSearchUA_Click(object sender, EventArgs e)
    {
        try
        {
            ddlVessel.SelectedIndex = 0;
            ddlManningOffice.SelectedIndex = 0;
            ddlNationality.SelectedIndex = 0;
            ddlRank_UA.SelectedIndex = 0;
            txtFromDt_UA.Text = "";
            txtToDt_UA.Text = "";
            txtFreeText_UA.Text = "";
            ddlNationality.SelectedIndex = 0;
            Search_UnAssigned();

            gvUnAssignedCrew.SelectedIndex = -1;
        }
        catch
        {
        }
    }
    protected void btnClearSearch_Click(object sender, EventArgs e)
    {
        ddlVessel.SelectedIndex = 0;
        ddlFleet.SelectedIndex = 0;
        ddlNationality_SOff.SelectedIndex = 0;

        ddlRank.SelectedIndex = 0;
        txtFromDt.Text = "";
        txtToDt.Text = "";
        txtFreeText.Text = "";
        Search_SigningOff();
        Bind_ChangeEvent();

        gvSignOffCrew.SelectedIndex = -1;
    }

    protected void Search_UnAssigned()
    {
        lblStaffHistory.Text = "";

        int PAGE_SIZE = ucCustomPager_OnSigners.PageSize;
        int PAGE_INDEX = ucCustomPager_OnSigners.CurrentPageIndex;
        int SelectRecordCount = ucCustomPager_OnSigners.isCountRecord;

        DataTable dt = BLL_Crew_CrewList.Get_UnAssigned_CrewList(int.Parse(ddlManningOffice.SelectedValue), int.Parse(ddlNationality.SelectedValue), int.Parse(ddlRank_UA.SelectedValue),UDFLib.ConvertToDefaultDt(Convert.ToString(txtFromDt_UA.Text)), UDFLib.ConvertToDefaultDt(Convert.ToString(txtToDt_UA.Text)), txtFreeText_UA.Text, int.Parse(ddlVessel_UA.SelectedValue), int.Parse(UA_AvailableOptions.SelectedValue), GetSessionUserID(), PAGE_SIZE, PAGE_INDEX, ref SelectRecordCount);

        if (ucCustomPager_OnSigners.isCountRecord == 1)
        {
            ucCustomPager_OnSigners.CountTotalRec = SelectRecordCount.ToString();
            ucCustomPager_OnSigners.BuildPager();
        }

        gvUnAssignedCrew.DataSource = dt;
        gvUnAssignedCrew.DataBind();

        if (Session["UTYPE"].ToString() == "ADMIN")
        {
            if (dt.Rows.Count == 0)
            {
                dt = BLL_Crew_CrewList.Get_UnAssigned_CrewList_History(txtFreeText_UA.Text, UDFLib.ConvertToInteger(Session["USERCOMPANYID"].ToString()));
                if (dt.Rows.Count > 0)
                {
                    lblStaffHistory.Text = "<br><br>Please find the below information which can help finding the staff.<br>";
                    foreach (DataRow dr in dt.Rows)
                    {
                        lblStaffHistory.Text += "<br>" + dr[0];
                    }
                }
            }
        }
    }
    protected void Search_SigningOff()
    {
        int iVesselID = 0;
        if (ddlVessel.Items.Count > 0)
            iVesselID = int.Parse(ddlVessel.SelectedValue);

        int PAGE_SIZE = ucCustomPager_OffSigners.PageSize;
        int PAGE_INDEX = ucCustomPager_OffSigners.CurrentPageIndex;
        int SelectRecordCount = ucCustomPager_OffSigners.isCountRecord;

        DataTable dt = BLL_Crew_CrewList.Get_SigningOff_CrewList(int.Parse(ddlFleet.SelectedValue), iVesselID, int.Parse(ddlRank.SelectedValue), int.Parse(ddlNationality_SOff.SelectedValue), UDFLib.ConvertToDefaultDt(Convert.ToString(txtFromDt.Text)), UDFLib.ConvertToDefaultDt(Convert.ToString(txtToDt.Text)), txtFreeText.Text, GetSessionUserID(), PAGE_SIZE, PAGE_INDEX, ref SelectRecordCount);

        if (ucCustomPager_OffSigners.isCountRecord == 1)
        {
            ucCustomPager_OffSigners.CountTotalRec = SelectRecordCount.ToString();
            ucCustomPager_OffSigners.BuildPager();
        }

        gvSignOffCrew.DataSource = dt;
        gvSignOffCrew.DataBind();
    }
    protected void btnAssign_Click(object sender, EventArgs e)
    {
        try
        {
            lblMessage.Text = "";

            if (ddlJoiningRank.SelectedValue == "0")
            {
                string js = "alert('Select Joining Rank');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUser", js, true);
            }
            else
            {

                int id1 = 0;
                int id2 = 0;
                if (gvSignOffCrew.SelectedValue != null)
                    id1 = int.Parse(gvSignOffCrew.SelectedValue.ToString());
                if (gvUnAssignedCrew.SelectedValue != null)
                    id2 = int.Parse(gvUnAssignedCrew.SelectedValue.ToString());

                if (id1 == 0 || id2 == 0)
                {
                    string js = "alert('Select both On-Signer and Off-Signer crew for the assignment');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUser", js, true);
                }
                else
                {
                    gvSignOffCrew.SelectedIndex = -1;
                    gvUnAssignedCrew.SelectedIndex = -1;

                    int Res = objCrew.INS_CrewAssignment(id1, id2, int.Parse(ddlJoiningRank.SelectedValue), GetSessionUserID());

                    if (Res == 1)
                    {
                        lblMessage.Text = "Assignment saved for the selected staff.";
                        string js = "alert('Assignment saved for the selected staff');";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUser0", js, true);
                    }
                    if (Res == -1)
                    {
                        lblMessage.Text = "Assignment already exists for the selected staff.";
                        string js = "alert('Assignment already exists for the selected staff');";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUser1", js, true);
                    }
                    if (Res == -2)
                    {
                        lblMessage.Text = "Assignment can not be done as the ON-SIGNER  has an open assignment without SIGN-ON-DATE";
                        string js = "alert('Assignment can not be done as the ON-SIGNER  has an open assignment without SIGN-ON-DATE');";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUser2", js, true);
                    }
                    if (Res == -3)
                    {
                        lblMessage.Text = "The ON-SIGNER can not join this vessel as this is his first voyage and the ship does not allow new joiners as ratings.";
                        string js = "alert('The ON-SIGNER can not join this vessel as this is his first voyage and the ship does not allow new joiners as ratings.');";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUser2", js, true);
                    }
                    if (Res == -4)
                    {
                        hdnAppJoiningRankID.Value = ddlJoiningRank.SelectedValue;
                        lblAppRank.Text = ddlJoiningRank.SelectedItem.Text;


                        //lblMessage.Text = "<div style='border:1px solid gray;padding:5px;margin:10px;'>The ON-SIGNER can not join this vessel as there are already two or more staffs of the same nationality has been joined the vessel. <br>Please click to take approval.<input type=button value='Send for Approval' onclick=showNationalityApproval()></div>";
                        //string js = "alert('The ON-SIGNER can not join this vessel as there are already two or more staffs of the same nationality has been joined the vessel.');";
                        //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUser2", js, true);

                        lblMessage.Text = "The ON-SIGNER can not join this vessel as there are already two or more staffs of the same nationality has been joined the vessel";
                        string js = "showNationalityApproval();";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "scr_nationality", js, true);

                    }

                    Bind_ChangeEvent();
                    Search_UnAssigned();
                    Search_SigningOff();
                }
            }
        }
        catch
        { }
    }

    protected void Bind_ChangeEvent()
    {
        int VesselID = int.Parse(ddlVessel.SelectedValue);
        int RankID = int.Parse(ddlRank.SelectedValue);
        int Nationality = int.Parse(ddlNationality_SOff.SelectedValue);

        string SearchText = txtFreeText.Text;

        DataTable dt = objCrew.Get_CrewAssignments(VesselID, RankID, SearchText, Nationality, GetSessionUserID());
        gvCrewChangeEvent.DataSource = dt;
        gvCrewChangeEvent.DataBind();
    }
    protected void gvCrewChangeEvent_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        objCrew.Delete_CrewAssignment(int.Parse(e.Keys[0].ToString()), GetSessionUserID());
        Search_UnAssigned();
        Search_SigningOff();
        Bind_ChangeEvent();
    }
    protected void gvCrewChangeEvent_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string Company_Name = DataBinder.Eval(e.Row.DataItem, "Company_Name").ToString();
            string EventID_ON = DataBinder.Eval(e.Row.DataItem, "EventID_ON").ToString();
            string EventID_OFF = DataBinder.Eval(e.Row.DataItem, "EventID_OFF").ToString();

            if (Company_Name.Length > 15)
                Company_Name = Company_Name.Substring(0, 13) + "..";

            ((Label)e.Row.FindControl("lblCompany_Name")).Text = Company_Name;

            if (EventID_ON != "0" || EventID_OFF != "0")
            {
                e.Row.CssClass = "bgEventBlue";
                ImageButton btnDelete = (ImageButton)e.Row.FindControl("btnDelete");
                if (btnDelete != null)
                    btnDelete.Visible = false;
            }
        }
    }
    protected void gvUnAssignedCrew_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string Company_Name = DataBinder.Eval(e.Row.DataItem, "Company_Name").ToString();
            string InvalidText = DataBinder.Eval(e.Row.DataItem, "InvalidText").ToString();

            if (Company_Name.Length > 15)
                Company_Name = Company_Name.Substring(0, 13) + "..";

            if (InvalidText.Length > 0)
            {
                ((ImageButton)e.Row.FindControl("lnkSelect")).Visible = false;
                ImageButton ImgInvalid = (ImageButton)e.Row.FindControl("ImgInvalid");
                if (ImgInvalid != null)
                {
                    ImgInvalid.Visible = true;
                    ImgInvalid.Attributes.Add("Title", "cssbody=[dvbdy1] cssheader=[dvhdr1] header=[Missing Data] body=[" + InvalidText + "]");
                }
            }
            ((Label)e.Row.FindControl("lblVessel_CODE")).Text = Company_Name;
        }
    }
    protected void gvSignOffCrew_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string strRowId = DataBinder.Eval(e.Row.DataItem, "ID").ToString();
            string DaysLeft = DataBinder.Eval(e.Row.DataItem, "DaysLeft").ToString();
            try
            {
                if (int.Parse(DaysLeft) <= 30)
                {
                    e.Row.Cells[e.Row.Cells.Count - 2].CssClass = "bgPink";
                }
            }
            catch
            {
            }


        }
    }


    protected void ddlVessel_SelectedIndexChanged(object sender, EventArgs e)
    {
        int VesselID = int.Parse(ddlVessel.SelectedValue);
        if (VesselID > 0)
        {
            lblSEQ.Text = objCrew.Get_SEQAndONBD(VesselID);
        }
        else
            lblSEQ.Text = "";

    }

    protected void btnNationalityApproval_Click(object sender, EventArgs e)
    {
        int Vessel_ID = UDFLib.ConvertToInteger(hdnAppVesselID.Value);
        int CrewID_SigningOff = UDFLib.ConvertToInteger(hdnAppSOffCrewID.Value);
        int CrewID = UDFLib.ConvertToInteger(hdnAppCrewID.Value);
        int CurrentRank_ID = UDFLib.ConvertToInteger(hdnAppCurrentRankID.Value);
        int JoiningRank_ID = UDFLib.ConvertToInteger(hdnAppJoiningRankID.Value);

        int retval = 0;

        if (CrewID_SigningOff == 0)
        {
            string js1 = "alert('Please select signing-off staff')";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "script1", js1, true);
        }
        else if (CrewID == 0)
        {
            string js2 = "alert('Please select joining staff')";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "script2", js2, true);
        }
        else
            retval = objCrew.NationalityCheck_SendForApproval(Vessel_ID, CrewID, CurrentRank_ID, JoiningRank_ID, txtAppRequest.Text, GetSessionUserID(), 0, CrewID_SigningOff);


        ClearHiddenFields();

        if (retval > 0)
        {
            string js3 = "alert('Approval has been sent');hideModal('dvNationalityApproval');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "script3", js3, true);

        }
        if (retval == -1)
        {
            string js4 = "alert('A previous request for approval has been pending for the same');hideModal('dvNationalityApproval');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "script4", js4, true);
        }
    }

    protected void gvSignOffCrew_SelectedIndexChanged(object sender, EventArgs e)
    {
        hdnAppVesselID.Value = gvSignOffCrew.DataKeys[gvSignOffCrew.SelectedRow.RowIndex].Values["Vessel_ID"].ToString();
        hdnAppSOffCrewID.Value = gvSignOffCrew.DataKeys[gvSignOffCrew.SelectedIndex].Values["ID"].ToString();
        lblAppVessel.Text = gvSignOffCrew.DataKeys[gvSignOffCrew.SelectedIndex].Values["Vessel_Short_Name"].ToString();

    }
    protected void gvUnAssignedCrew_SelectedIndexChanged(object sender, EventArgs e)
    {
        hdnAppCrewID.Value = gvUnAssignedCrew.DataKeys[gvUnAssignedCrew.SelectedIndex].Values["ID"].ToString();
        hdnAppCurrentRankID.Value = gvUnAssignedCrew.DataKeys[gvUnAssignedCrew.SelectedIndex].Values["Rank_ID"].ToString();

    }

    protected void ClearHiddenFields()
    {
        hdnAppVesselID.Value = "";
        hdnAppSOffCrewID.Value = "";
        hdnAppCrewID.Value = "";
        hdnAppCurrentRankID.Value = "";
        hdnAppJoiningRankID.Value = "";
        lblMessage.Text = "";
    }

}