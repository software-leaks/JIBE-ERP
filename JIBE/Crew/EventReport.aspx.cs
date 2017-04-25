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

public partial class Crew_EventReport : System.Web.UI.Page
{
    BLL_Infra_Country objCountry = new BLL_Infra_Country();
    BLL_Crew_CrewDetails objCrew = new BLL_Crew_CrewDetails();
    BLL_Crew_Admin objCrewAdmin = new BLL_Crew_Admin();
    BLL_Infra_VesselLib objVessel = new BLL_Infra_VesselLib();
    int RankScaleConsidered = 0;
    public UserAccess objUA = new UserAccess();
    public string DateFormatMessage = "";
    protected void Page_Load(object sender, EventArgs e)
    {   
        if (GetSessionUserID() == 0)
            Response.Redirect("~/account/login.aspx");

        CalendarExtender5.Format = Convert.ToString(Session["User_DateFormat"]);
        CalendarExtender2.Format = Convert.ToString(Session["User_DateFormat"]);
        DateFormatMessage = UDFLib.DateFormatMessage();

        UserAccessValidation();

        DataTable dtWages = objCrewAdmin.GetWagesSettings();
        if (dtWages != null && dtWages.Rows.Count > 0)
        {
            if (Convert.ToBoolean(dtWages.Rows[0]["RankScaleConsidered"]) == true)
            {
                RankScaleConsidered = 1;
                gvSelectedONSigners.Columns[gvSelectedONSigners.Columns.Count - 5].Visible = true;
            }
            else
            {
                gvSelectedONSigners.Columns[gvSelectedONSigners.Columns.Count - 5].Visible = false;
            }
        }

        if (!IsPostBack)
        {
            Load_FleetList();
            Load_VesselList();
            Load_VesselList_OnSigners();
            Bind_Assignments();

        }
        string js = "$('.vesselinfo').InfoBox();";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "initscript", js, true);
    }

    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();


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
            gvCrewChangeEvent.Columns[gvCrewChangeEvent.Columns.Count - 1].Visible = false;
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

    protected void Load_FleetList()
    {
        int UserCompanyID = UDFLib.ConvertToInteger(Session["USERCOMPANYID"].ToString());
        ddlFleet.DataSource = objVessel.GetFleetList(UserCompanyID);
        ddlFleet.DataTextField = "NAME";
        ddlFleet.DataValueField = "CODE";
        ddlFleet.DataBind();
        ddlFleet.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));

    }
    public void Load_VesselList()
    {
        int Fleet_ID = int.Parse(ddlFleet.SelectedValue);
        int Vessel_Manager = UDFLib.ConvertToInteger(getSessionString("USERCOMPANYID"));
        int UserCompanyID = UDFLib.ConvertToInteger(getSessionString("USERCOMPANYID"));

        ddlVessel.DataSource = objVessel.Get_VesselList(Fleet_ID, 0, Vessel_Manager, "", UserCompanyID);
        ddlVessel.DataTextField = "VESSEL_NAME";
        ddlVessel.DataValueField = "VESSEL_ID";
        ddlVessel.DataBind();
        ddlVessel.Items.Insert(0, new ListItem("-SELECT-", "0"));
        ddlVessel.SelectedIndex = 0;
    }
    public void Load_VesselList_OnSigners()
    {
        int Fleet_ID = 0;
        int Vessel_Manager = UDFLib.ConvertToInteger(getSessionString("USERCOMPANYID"));
        int UserCompanyID = UDFLib.ConvertToInteger(getSessionString("USERCOMPANYID"));

        ddlVesselName.DataSource = objVessel.Get_VesselList(Fleet_ID, 0, Vessel_Manager, "", UserCompanyID);
        ddlVesselName.DataTextField = "VESSEL_NAME";
        ddlVesselName.DataValueField = "VESSEL_ID";
        ddlVesselName.DataBind();
        ddlVesselName.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));
        ddlVesselName.SelectedIndex = 0;
    }

    protected void Load_CrewList_OnSigners()
    {
        try
        {
            int Event_VesselID = int.Parse(ddlVessel.SelectedValue);
            int VesselID = int.Parse(ddlVesselName.SelectedValue);
            int PAGE_SIZE = ucCustomPager_OnSigners.PageSize;
            int PAGE_INDEX = ucCustomPager_OnSigners.CurrentPageIndex;
            int SelectRecordCount = ucCustomPager_OnSigners.isCountRecord;
            int EventID = UDFLib.ConvertToInteger(hdnEventID.Value);

            DataTable dt = BLL_Crew_CrewList.Get_Crewlist_OnSigners(txtSearchCrew_OnSigners.Text, VesselID, GetSessionUserID(), Event_VesselID, EventID, PAGE_SIZE, PAGE_INDEX, ref SelectRecordCount);

            if (ucCustomPager_OnSigners.isCountRecord == 1)
            {
                ucCustomPager_OnSigners.CountTotalRec = SelectRecordCount.ToString();
                ucCustomPager_OnSigners.BuildPager();
            }

            gvCrewList_OnSigner.DataSource = dt;
            gvCrewList_OnSigner.DataBind();
        }
        catch
        { }
    }
    protected void Load_CrewList_OffSigners()
    {
        try
        {
            int VesselID = int.Parse(ddlVessel.SelectedValue);
            int PAGE_SIZE = ucCustomPager_OffSigners.PageSize;
            int PAGE_INDEX = ucCustomPager_OffSigners.CurrentPageIndex;

            int SelectRecordCount = ucCustomPager_OffSigners.isCountRecord;
            DataTable dt = BLL_Crew_CrewList.Get_Crewlist_OffSigners(VesselID, txtSearchCrew_OffSigners.Text, GetSessionUserID(), PAGE_SIZE, PAGE_INDEX, ref SelectRecordCount);

            if (ucCustomPager_OffSigners.isCountRecord == 1)
            {
                ucCustomPager_OffSigners.CountTotalRec = SelectRecordCount.ToString();
                ucCustomPager_OffSigners.BuildPager();
            }

            gvCrewList_OffSigner.DataSource = dt;
            gvCrewList_OffSigner.DataBind();
        }
        catch
        { }
    }

    protected void ddlFleet_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Load_VesselList();
            Bind_Assignments();
            Bind_ChangeEvent();
        }
        catch (Exception ex)
        {
            string js3 = "alert('" + ex.Message.Replace("'", "") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "SaveAddError", js3, true);

        }

    }
    protected void ddlVessel_SelectedIndexChanged(object sender, EventArgs e)
    {
        Bind_Assignments();
        Bind_ChangeEvent();

    }

    protected void ddlVesselName_SelectedIndexChanged(object sender, EventArgs e)
    {
        ucCustomPager_OnSigners.isCountRecord = 1;
        Load_CrewList_OnSigners();
    }

    protected void gvCrewChangeEvent_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        objCrew.Delete_CrewAssignment(int.Parse(e.Keys[0].ToString()), GetSessionUserID());
        Bind_Assignments();
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

                if (EventID_ON != "0")
                {
                    CheckBox chkON = ((CheckBox)e.Row.FindControl("chkSelect_ON"));
                    if (chkON != null)
                    {
                        chkON.Visible = false;
                    }
                }
                if (EventID_OFF != "0")
                {
                    CheckBox chkOFF = ((CheckBox)e.Row.FindControl("chkSelect_OFF"));
                    if (chkOFF != null)
                    {
                        chkOFF.Visible = false;
                    }

                }
            }
        }
    }
    protected void chkSelect_ON_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chk = (CheckBox)sender;
        if (chk.Checked == true)
        {
            string clientID = chk.ClientID.Replace("chkSelect_ON", "chkSelect_OFF");
            string js = "selectCheckBox('" + clientID + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "checkBoxselect", js, true);
        }
    }

    protected void txtSearchCrew_OnSigners_TextChanged(object sender, EventArgs e)
    {
        ucCustomPager_OnSigners.isCountRecord = 1;
        Load_CrewList_OnSigners();
    }
    protected void txtSearchCrew_OffSigners_TextChanged(object sender, EventArgs e)
    {
        Load_CrewList_OffSigners();
    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        string CrewID = "";
        string CrewName = "";
        string StaffCode = "";
        string Rank = "";
        string RankID = "";

        int Count = 0;
        int EventID = 0;

        string js;

        lblMsg.Text = "";
        btnSaveAdditional.Enabled = true;

        DataTable dtON = new DataTable();
        dtON.Columns.Add("ID", typeof(String));
        dtON.Columns.Add("STAFF_CODE", typeof(String));
        dtON.Columns.Add("RANK_SHORT_NAME", typeof(String));
        dtON.Columns.Add("RANK_ID", typeof(String));
        dtON.Columns.Add("STAFF_NAME", typeof(String));

        DataTable dtOff = new DataTable();
        dtOff.Columns.Add("ID", typeof(String));
        dtOff.Columns.Add("STAFF_CODE", typeof(String));
        dtOff.Columns.Add("RANK_SHORT_NAME", typeof(String));
        dtOff.Columns.Add("RANK_ID", typeof(String));
        dtOff.Columns.Add("STAFF_NAME", typeof(String));


        if (hdnEventID.Value != "")
        {
            EventID = UDFLib.ConvertToInteger(hdnEventID.Value);

            if (EventID > 0)
            {
                foreach (GridViewRow row in gvCrewList_OnSigner.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        if (((CheckBox)row.FindControl("chkSelect")).Checked == true)
                        {
                            CrewID = ((HiddenField)row.FindControl("hdnCrewID")).Value;
                            CrewName = ((Label)row.FindControl("lblSTAFFNAME")).Text;
                            StaffCode = ((HiddenField)row.FindControl("hdnStaffCode")).Value;
                            Rank = ((Label)row.FindControl("lblRank")).Text;
                            RankID = ((HiddenField)row.FindControl("hdnRankID")).Value;

                            dtON.Rows.Add(CrewID, StaffCode, Rank, RankID, CrewName);

                            Count += 1;
                        }
                    }
                }
                gvSelectedONSigners.DataSource = dtON;
                gvSelectedONSigners.DataBind();

                foreach (GridViewRow row in gvCrewList_OffSigner.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        if (((CheckBox)row.FindControl("chkSelect")).Checked == true)
                        {
                            CrewID = ((HiddenField)row.FindControl("hdnCrewID")).Value;
                            CrewName = ((Label)row.FindControl("lblSTAFFNAME")).Text;
                            StaffCode = ((HiddenField)row.FindControl("hdnStaffCode")).Value;
                            Rank = ((Label)row.FindControl("lblRank")).Text;
                            RankID = ((HiddenField)row.FindControl("hdnRankID")).Value;

                            dtOff.Rows.Add(CrewID, StaffCode, Rank, RankID, CrewName);
                            Count += 1;
                        }
                    }
                }
                gvSelectedOffSigners.DataSource = dtOff;
                gvSelectedOffSigners.DataBind();

                if (Count > 0)
                {
                    pnlViewSelectedCrew.Visible = true;
                    UpdatePanel_AdditionalSelected.Update();
                }
            }

            js = "showModal('dvViewSelectedCrew');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "showselected", js, true);
        }
    }
    protected void btnSaveAdditional_Click(object sender, EventArgs e)
    {
        int retval = 0;
        try
        {

            if (hdnEventID.Value != "")
            {
                int EventID = UDFLib.ConvertToInteger(hdnEventID.Value);

                if (EventID > 0)
                {
                    int iCrewID = 0;
                    int iJoining_Rank = 0;
                    string COC_Date = "";
                    string Joining_Date = "";
                    int VoyID = 0;
                    int RankId = 0;
                    int RankScaleId = 0;
                    foreach (GridViewRow row in gvSelectedONSigners.Rows)
                    {
                        iCrewID = UDFLib.ConvertToInteger(((HiddenField)row.FindControl("hdnCrewID")).Value);

                        iJoining_Rank = UDFLib.ConvertToInteger(((DropDownList)row.FindControl("ddlRank")).SelectedValue);

                        Joining_Date = ((TextBox)row.FindControl("txtJoinDate")).Text;
                        COC_Date = ((TextBox)row.FindControl("txtCOCDate")).Text;

                        if (!UDFLib.DateCheck(Joining_Date))
                        {
                            string js3 = "alert('Enter valid Joining Date" + DateFormatMessage + "');";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "searchError", js3, true);
                            return;
                        }

                        if (!UDFLib.DateCheck(COC_Date))
                        {
                            string js3 = "alert('Enter valid EOC Date" + DateFormatMessage + "');";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "searchError", js3, true);
                            return;
                        }
                        Joining_Date = UDFLib.ConvertToDefaultDt(Joining_Date);
                        COC_Date = UDFLib.ConvertToDefaultDt(COC_Date);
                        VoyID = UDFLib.ConvertToInteger(((DropDownList)row.FindControl("ddlVoyages")).SelectedValue);
                        RankId = UDFLib.ConvertToInteger(((DropDownList)row.FindControl("ddlRank")).SelectedValue);


                        if (Joining_Date != "" && RankId > 0)
                        {
                            if (RankScaleConsidered == 1)
                            {
                                RankScaleId = UDFLib.ConvertToInteger(((DropDownList)row.FindControl("ddlRankScale")).SelectedValue);
                            }
                            if (RankScaleConsidered == 0 || RankScaleId > 0)
                                retval = objCrew.AddCrewTo_CrewChangeEvent(EventID, iCrewID, 1, 0, GetSessionUserID(), Joining_Date, COC_Date, iJoining_Rank, VoyID, RankScaleId);
                            else
                            {
                                string js = "Select Rank Scale.";
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "SaveAddError", "alert('" + js + "');", true);
                            }
                        }
                        else
                        {
                            string js1 = "";
                            if (Joining_Date == "")
                                js1 = "Select Contract Date.";
                            if (RankId == 0)
                                js1 += "Select Joining Rank.";
                            if (RankScaleConsidered == 1)
                            {
                                RankScaleId = UDFLib.ConvertToInteger(((DropDownList)row.FindControl("ddlRankScale")).SelectedValue);
                                if (RankScaleId == 0)
                                    js1 += "Select Rank Scale.";
                            }
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "SaveAddError", "alert('" + js1 + "');", true);
                        }
                    }

                    foreach (GridViewRow row in gvSelectedOffSigners.Rows)
                    {
                        iCrewID = UDFLib.ConvertToInteger(((HiddenField)row.FindControl("hdnCrewID")).Value);
                        VoyID = UDFLib.ConvertToInteger(((DropDownList)row.FindControl("ddlSOffVoyages")).SelectedValue);

                        retval = objCrew.AddCrewTo_CrewChangeEvent(EventID, iCrewID, 0, 0, GetSessionUserID());
                    }

                    if (retval > 0)
                    {
                        Bind_Assignments();
                        Bind_ChangeEvent();

                        pnlViewSelectedCrew.Visible = false;
                        UpdatePanel_AdditionalSelected.Update();

                        pnlAdditionalCrew.Visible = false;
                        UpdatePanelAdditionalCrew.Update();

                        pnlAssignments.Visible = true;
                        UpdatePanelAssignments.Update();

                        pnlCrewChangeEvent.Visible = true;
                        UpdatePanelEvents.Update();

                        string js2 = "hideModal('dvViewSelectedCrew');";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "hideselected", js2, true);

                    }

                }
            }

        }
        catch (Exception ex)
        {
            string js3 = "alert('" + ex.Message.Replace("'", "") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "SaveAddError", js3, true);

        }
    }

    protected void btnCancelAdditional_Click(object sender, EventArgs e)
    {
        pnlAdditionalCrew.Visible = false;
        UpdatePanelAdditionalCrew.Update();

        pnlViewSelectedCrew.Visible = false;
        UpdatePanel_AdditionalSelected.Update();

        pnlAssignments.Visible = true;
        UpdatePanelAssignments.Update();

        pnlCrewChangeEvent.Visible = true;
        UpdatePanelEvents.Update();

        string js2 = "hideModal('dvViewSelectedCrew');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "hideselected", js2, true);

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        pnlAdditionalCrew.Visible = false;
        pnlAssignments.Visible = true;
        UpdatePanelAssignments.Update();
        pnlCrewChangeEvent.Visible = true;
        UpdatePanelEvents.Update();
    }

    protected void btnCancelEvent_Click(object sender, EventArgs e)
    {

        DataTable dtON = new DataTable();
        dtON.Columns.Add("ID", typeof(String));
        dtON.Columns.Add("STAFF_CODE", typeof(String));
        dtON.Columns.Add("RANK_SHORT_NAME", typeof(String));
        dtON.Columns.Add("RANK_ID", typeof(String));
        dtON.Columns.Add("STAFF_NAME", typeof(String));

        DataTable dtOff = new DataTable();
        dtOff.Columns.Add("ID", typeof(String));
        dtOff.Columns.Add("STAFF_CODE", typeof(String));
        dtOff.Columns.Add("RANK_SHORT_NAME", typeof(String));
        dtOff.Columns.Add("RANK_ID", typeof(String));
        dtOff.Columns.Add("STAFF_NAME", typeof(String));

        gvSelectedONSigners.DataSource = dtON;
        gvSelectedONSigners.DataBind();

        gvSelectedOffSigners.DataSource = dtOff;
        gvSelectedOffSigners.DataBind();


        //pnlViewSelectedCrew.Visible = false;

    }

    protected void btnSaveEventEdit_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtEditEventDate.Text != "" && ddlEditEventPort.SelectedIndex > 0 && ddlVessel.SelectedIndex > 0)
            {
                int EventID = int.Parse(hdnEditEventID.Value);
                int Port_Call_ID = 0;

                string Remark = "-";
                if (txtEventRemark.Text != "")
                    Remark = txtEventRemark.Text;
                objCrew.UPDATE_CrewChangeEvent(EventID, txtEditEventDate.Text, int.Parse(ddlEditEventPort.SelectedValue), Remark, GetSessionUserID(), Port_Call_ID);

                pnlEditEvent.Visible = false;
                Bind_Assignments();
                Bind_ChangeEvent();
            }
            else if (txtEditEventDate.Text == "")
            {
                string js = "alert('Select Event Date');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUser", js, true);
            }
            else if (ddlEditEventPort.SelectedIndex == 0)
            {
                string js = "alert('Select Event Port');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUser", js, true);
            }
        }
        catch
        {
        }
    }
    protected void btnCloseEventEdit_Click(object sender, EventArgs e)
    {
        pnlEditEvent.Visible = false;
    }
    protected void RemoveCrewFromEvent(int EventID, int CrewID)
    {
        objCrew.RemoveCrewFrom_CrewChangeEvent(EventID, CrewID, GetSessionUserID());
    }
    protected void rpt2_ItemCommand(Object Sender, RepeaterCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "RemoveCrewFromEvent")
            {
                int EventID = 0; int CrewID = 0;
                string arg = e.CommandArgument.ToString();
                if (arg.IndexOf(",") > 0)
                {
                    EventID = int.Parse(arg.Split(',')[0]);
                    CrewID = int.Parse(arg.Split(',')[1]);
                }
                RemoveCrewFromEvent(EventID, CrewID);
                Bind_Assignments();
                Bind_ChangeEvent();
            }
            else if (e.CommandName == "AddCrewToEvent")
            {
                //pnlCrewList.Visible = true;
                pnlAssignments.Visible = false;
                pnlCrewChangeEvent.Visible = false;
            }
        }
        catch { }

    }
    protected void rpt2_ItemDataBound(Object Sender, RepeaterItemEventArgs e)
    {
        try
        {
            DataRow dr = (DataRow)e.Item.DataItem;
            if (dr[3].ToString() == "0")
            {
                if (e.Item.FindControl("ImgBtnRemove_On") != null)
                    ((ImageButton)e.Item.FindControl("ImgBtnRemove_On")).Visible = false;
                if (e.Item.FindControl("ImgBtnRemove_Off") != null)
                    ((ImageButton)e.Item.FindControl("ImgBtnRemove_Off")).Visible = false;
            }
            else
            {
                if (dr["ION"].ToString() == "1" && objUA.Delete == 1)
                    ((ImageButton)e.Item.FindControl("ImgBtnRemove_On")).Visible = true;

                if (dr["IOFF"].ToString() == "1" && objUA.Delete == 1)
                    ((ImageButton)e.Item.FindControl("ImgBtnRemove_Off")).Visible = true;
            }

        }
        catch { }

    }
    protected void rpt1_ItemCommand(Object Sender, RepeaterCommandEventArgs e)
    {
        string js = "";
        try
        {
            hdnEventID.Value = e.CommandArgument.ToString();
            int EventID = 0;
            int AssignmentID = 0;
            int CrewID_ON = 0;
            int CrewID_OFF = 0;

            if (e.CommandArgument.ToString() != "")
            {
                EventID = UDFLib.ConvertToInteger(e.CommandArgument.ToString());

                if (e.CommandName == "AddCrewToEvent")
                {
                    txtSearchCrew_OnSigners.Text = "";
                    txtSearchCrew_OffSigners.Text = "";

                    int VesselID = 0;
                    if (ddlVessel.SelectedIndex > 0)
                        VesselID = int.Parse(ddlVessel.SelectedValue);

                    ucCustomPager_OnSigners.isCountRecord = 1;
                    Load_CrewList_OnSigners();
                    Load_CrewList_OffSigners();

                    pnlAdditionalCrew.Visible = true;
                    UpdatePanelAdditionalCrew.Update();

                    pnlAssignments.Visible = false;
                    UpdatePanelAssignments.Update();




                    pnlCrewChangeEvent.Visible = false;
                    UpdatePanelEvents.Update();
                }
                else if (e.CommandName == "AddAssignmentToEvent")
                {
                    EventID = UDFLib.ConvertToInteger(e.CommandArgument.ToString());

                    if (EventID > 0)
                    {
                        foreach (GridViewRow gvRow in gvCrewChangeEvent.Rows)
                        {
                            CheckBox chkSel_ON = (CheckBox)gvRow.FindControl("chkSelect_ON");
                            CheckBox chkSel_OFF = (CheckBox)gvRow.FindControl("chkSelect_OFF");

                            if (chkSel_ON != null)
                            {
                                if (chkSel_ON.Checked == true)
                                {
                                    AssignmentID = int.Parse(((HiddenField)gvRow.FindControl("hdnPKID")).Value);
                                    CrewID_ON = int.Parse(((HiddenField)gvRow.FindControl("hdnCrewID_ON")).Value);
                                    int RetVal = objCrew.AddCrewTo_CrewChangeEvent(EventID, CrewID_ON, 1, AssignmentID, GetSessionUserID());
                                    if (RetVal == -4)
                                    {
                                        js = "alert('On signer event can not be created. Either Passport or Seamanbook is expiring within a year.');";
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUserEventEdit", js, true);
                                    }
                                }
                            }
                            if (chkSel_OFF != null)
                            {
                                if (chkSel_OFF.Checked == true)
                                {
                                    AssignmentID = int.Parse(((HiddenField)gvRow.FindControl("hdnPKID")).Value);
                                    CrewID_OFF = int.Parse(((HiddenField)gvRow.FindControl("hdnCrewID_OFF")).Value);
                                    objCrew.AddCrewTo_CrewChangeEvent(EventID, CrewID_OFF, 0, AssignmentID, GetSessionUserID());
                                }
                            }
                            Bind_Assignments();
                            Bind_ChangeEvent();
                        }
                    }


                }
                else if (e.CommandName == "NotifyManningAgent")
                {
                    SendMail_NotifyManningOffice(EventID, GetSessionUserID());

                }
                else if (e.CommandName == "NotifyMaster")
                {
                    SendMail_NotifyMaster(EventID, GetSessionUserID());
                }
                else if (e.CommandName == "FinalPlan")
                {
                    SendMail_FinalCrewChangePlan(EventID, GetSessionUserID());
                }
                else if (e.CommandName == "EditEvent")
                {
                    DataTable dt = objCrew.Get_EventDetails(EventID);
                    if (dt.Rows.Count > 0)
                    {
                        int CreatedBy_CompanyID = UDFLib.ConvertToInteger(dt.Rows[0]["CompanyID"].ToString());
                        if (CreatedBy_CompanyID == UDFLib.ConvertToInteger(Session["USERCOMPANYID"].ToString()))
                        {
                            ResponseHelper.Redirect("EditEvent.aspx?EventID=" + EventID, "_blank", "");
                        }
                        else
                        {
                            js = "alert('You are not authorised to edit this event');";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUserEventEdit", js, true);
                        }
                    }
                }
                else if (e.CommandName == "CloseEvent")
                {
                    int Ret_Close = objCrew.CloseEvent(EventID, GetSessionUserID());
                    if (Ret_Close == -1)
                    {
                        js = "alert('Select the port call before closing the event!!');";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUserEventClose", js, true);
                    }
                    else if (Ret_Close == -2)
                    {
                        js = "alert('You are not authorised to close this event');";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUserEventClose", js, true);
                        Bind_ChangeEvent();
                    }
                    else if (Ret_Close == -3)
                    {
                        js = "alert('Event can not be closed as there is one or more travel requests pending for one or more staffs');";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUserEventClose", js, true);
                        Bind_ChangeEvent();
                    }
                    else if (Ret_Close == 1)
                    {
                        js = "alert('Event closed successfully');";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUserEventClose", js, true);
                        Bind_ChangeEvent();
                    }
                }
                else if (e.CommandName == "DeleteEvent")
                {
                    int RetVal = objCrew.DeleteEvent(EventID, GetSessionUserID());

                    if (RetVal == -1)
                    {
                        js = "alert('You are not authorised to delete this event!!');";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUserEventDeleted", js, true);
                    }
                    else if (RetVal == 1)
                    {
                        SendMail_NotifyManningOffice_OnEventDelete(EventID, GetSessionUserID());
                        js = "alert('Event and its members are deleted');";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUserEventDeleted", js, true);
                        Bind_Assignments();
                        Bind_ChangeEvent();
                    }
                    else if (RetVal == 0)
                    {
                        js = "alert('Event deleted');";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUserEventDeleted", js, true);
                        Bind_Assignments();
                        Bind_ChangeEvent();
                    }

                }
                else if (e.CommandName == "NewFlightRequest")
                {
                    ResponseHelper.Redirect("~/Travel/NewRequest.aspx?EventID=" + e.CommandArgument, "_blank", "");

                }
            }
        }
        catch
        {
        }
    }
    protected void rpt1_ItemDataBound(Object Sender, RepeaterItemEventArgs e)
    {
        DataRowView dr = (DataRowView)e.Item.DataItem;

        if (dr.Row["Event_Status"].ToString() == "0")
        {
            ((LinkButton)e.Item.FindControl("btnCloseEvent")).Visible = false;
            ((LinkButton)e.Item.FindControl("btnEditEvent")).Visible = false;
            ((LinkButton)e.Item.FindControl("btnAddCrewToEvent")).Visible = false;
            ((LinkButton)e.Item.FindControl("btnAddAssignmentToEvent")).Visible = false;

            ((LinkButton)e.Item.FindControl("btnNotifyMO")).Visible = false;
            ((LinkButton)e.Item.FindControl("btnNotifyMaster")).Visible = false;
            ((LinkButton)e.Item.FindControl("btnNotifyFinalCrewChange")).Visible = false;

            ((ImageButton)e.Item.FindControl("btnDelete")).Visible = false;
            ((ImageButton)e.Item.FindControl("ImgFlightRequest")).Visible = false;

        }
        if (dr.Row["EventRemark"].ToString() == "")
        {
            ((Image)e.Item.FindControl("imgEventRemark")).Visible = false;
        }
        else
        {
            ((Image)e.Item.FindControl("imgEventRemark")).ToolTip = dr.Row["EventRemark"].ToString().Replace("\n", "<br>");

        }
        if (Session["UTYPE"].ToString() != "OFFICE USER")
        {
            // Remove flight booking icon
            ((ImageButton)e.Item.FindControl("ImgFlightRequest")).Visible = false;
        }
        string js = "$('.remarks').tooltip();";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "ToolTip", js, true);


    }

    protected void SendMail_NotifyManningOffice(int EventID, int SessionUserID)
    {

        string Vessel_Name = " _ _ _ _ ";
        string Port_Name = " _ _ _ _ ";
        string Event_Date = " _ _ _ _ ";

        string msgTo = "";
        string msgCC = "";
        string msgBCC = "";

        string msgBody = "";
        string msgSubject = "";

        DataSet ds = objCrew.Get_MailDetailsForEventID(EventID, 5);
        DataTable dtMain = ds.Tables[0];
        DataTable dtDetail = ds.Tables[1];

        foreach (DataRow dr in dtMain.Rows)
        {
            msgTo = dr["Email1"].ToString();
            msgCC = "";
            Vessel_Name = dr["Vessel_Name"].ToString();
            Port_Name = dr["Port_Name"].ToString();
            Event_Date = dr["Event_Date"].ToString();

            if (Event_Date != "")
            {
                Event_Date = DateTime.Parse(Event_Date).ToString("dd/MM/yyyy");
            }

            int ManningOfficeID = int.Parse(dr["ManningOfficeID"].ToString());
            string Filter = "ON_OFF = 1 AND ManningOfficeID = " + ManningOfficeID;
            dtDetail.Select("");
            DataRow[] drCrewDetails = dtDetail.Select(Filter, "staff_name");
            int count = drCrewDetails.Length;

            if (count > 0)
            {
                msgSubject = "M V " + Vessel_Name.ToUpper() + " - INTENDED CREW CHANGE PLAN - " + Port_Name.ToUpper() + " - " + Event_Date;

                msgBody = " ----   System Notification: Please do not reply to this mail.   ----";
                msgBody += "<br><br>";

                //msgBody += "Sub: Intended crew change plan for M V " + Vessel_Name.ToUpper() + " at " + Port_Name.ToUpper() + " on " + Event_Date + "<br>";

                msgBody += "Please ensure joining ship staff personal documents and statutory licences and certificates are valid for his contract duration plus 2 months buffer. ";
                msgBody += "If you are not sure about compliance liaise with CREW department.<br>";

                msgBody += "Kindly ensure that all officers who joins must carry their uniform <br>";
                msgBody += "Dress Code [ only for officers ] <br>";
                msgBody += "-  White Shirt ( Half or Full Sleeves ).<br>";
                msgBody += "-  Trousers - Black or Navy Blue <br>";
                msgBody += "-  Black shoes<br>";
                msgBody += "-  Respective epaulets as per their rank.<br>";
                msgBody += "Please note there is NO Uniform allowance paid , please make this to joiners very clear, we don’t want any queries about this from vessel, please ensure they carry the above Uniform.<br>";


                msgBody += "<br><br><b>On Signers:" + count + "</b><br>===============<br>";

                count = 1;
                foreach (DataRow drDetail in drCrewDetails)
                {
                    msgBody += count + ".<br>-----------------------------------------------------<br>";
                    msgBody += "Staff Code:     " + drDetail["staff_code"].ToString().ToUpper() + "<br>";
                    msgBody += "First Name:     " + drDetail["staff_name"].ToString().ToUpper() + "<br>";
                    msgBody += "SurName:        " + drDetail["staff_Surname"].ToString().ToUpper() + "<br>";
                    msgBody += "Rank:           " + drDetail["rank_short_name"].ToString().ToUpper() + "<br>";
                    msgBody += "Nationality:    " + drDetail["Nationality"].ToString().ToUpper() + "<br>";
                    msgBody += "Passport No:    " + drDetail["Passport_Number"].ToString().ToUpper() + "<br>";

                    msgBody += "PP Issue Date:  " + drDetail["Passport_Issue_Date"].ToString().ToUpper() + "<br>";
                    msgBody += "PP Expiry Date: " + drDetail["Passport_Expiry_Date"].ToString().ToUpper() + "<br>";
                    msgBody += "Place of Issue: " + drDetail["Passport_PlaceOf_Issue"].ToString().ToUpper() + "<br>";
                    msgBody += "Seaman Book No: " + drDetail["Seaman_Book_Number"].ToString().ToUpper() + "<br>";
                    msgBody += "S/B Issue Date: " + drDetail["Seaman_Book_Issue_date"].ToString().ToUpper() + "<br>";
                    msgBody += "S/B Expiry Date:" + drDetail["Seaman_Book_Expiry_Date"].ToString().ToUpper() + "<br>";
                    msgBody += "Place of Issue: " + drDetail["Seaman_Book_PlaceOf_Issue"].ToString().ToUpper() + "<br>";

                    msgBody += "D.O.B:          " + drDetail["Staff_Birth_Date"].ToString().ToUpper() + "<br>";
                    msgBody += "P.O.B:          " + drDetail["Staff_Born_Place"].ToString().ToUpper() + "<br>";
                    msgBody += "<br>";
                    count++;

                    //string Email = drDetail["ManningOfficeEmail"].ToString().ToUpper();
                    //if (ManningOfficeEmail.IndexOf(Email) < 0)
                    //{
                    //    if (ManningOfficeEmail.Length > 0)
                    //        ManningOfficeEmail += ";";
                    //    ManningOfficeEmail += Email;
                    //}
                }

                //if (ManningOfficeEmail.Length > 0)
                //{

                //    if (msgTo.Length > 0)
                //        msgTo += ";";
                //        msgTo += ManningOfficeEmail;
                //}
                msgBody += "We will revert with the Opening Crew Change Plan message in due course.";
                msgBody += "<br>";

                int MsgID = objCrew.Send_CrewNotification(0, ManningOfficeID, EventID, 5, msgTo, msgCC, msgBCC, msgSubject, msgBody, "", "MAIL", "", GetSessionUserID(), "DRAFT");
                if (MsgID > 0)
                {
                    string js = "window.open('EmailEditor.aspx?ID=" + MsgID + "');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "NotifyManningOffice" + ManningOfficeID, js, true);
                }
            }
        }

    }
    protected void SendMail_NotifyMaster(int EventID, int SessionUserID)
    {

        string msgTo = "";
        string msgCC = "";
        string msgBCC = "";

        string Vessel_Name = " _ _ _ _ ";
        string Port_Name = " _ _ _ _ ";
        string Event_Date = " _ _ _ _ ";
        string Capt_Name = "";

        DataSet ds = objCrew.Get_MailDetailsForEventID(EventID, 6);
        DataTable dtMain = ds.Tables[0];
        DataTable dtDetail = ds.Tables[1];

        if (dtMain.Rows.Count > 0)
        {
            DataRow dr = dtMain.Rows[0];

            msgTo = dr["vessel_email"].ToString();

            Vessel_Name = dr["Vessel_Name"].ToString();
            Port_Name = dr["Port_Name"].ToString();
            Event_Date = dr["Event_Date"].ToString();
            if (Event_Date != "")
            {
                Event_Date = DateTime.Parse(Event_Date).ToString("dd/MM/yyyy");
            }

            string msgSubject = "M V " + Vessel_Name.ToUpper() + " - INTENDED CREW CHANGE PLAN - " + Port_Name.ToUpper() + " - " + Event_Date;

            string msgBody = " ----   System Notification: Please do not reply to this mail.   ----";
            msgBody += "<br><br>";

            //msgBody += "Sub: Intended crew change plan for M V " + Vessel_Name.ToUpper() + " at " + Port_Name.ToUpper() + " on " + Event_Date + "<br>";

            msgBody += "Dear Capt. " + Capt_Name + ",<br>";

            msgBody += "<br>Please find hereunder intended crew change plan at " + Port_Name.ToUpper() + " around " + Event_Date + "<br>";

            string Filter = "ON_OFF = 1";
            DataRow[] drCrewDetails = dtDetail.Select(Filter, "staff_name");
            int count = drCrewDetails.Length;

            msgBody += "<br><br><b>On Signers: " + count + "</b><br>";
            count = 1;
            foreach (DataRow drDetail in drCrewDetails)
            {
                msgBody += count + ".<br>-----------------------------------------------------<br>";
                msgBody += "Staff Code:     " + drDetail["staff_code"].ToString().ToUpper() + "<br>";
                msgBody += "First Name:     " + drDetail["staff_name"].ToString().ToUpper() + "<br>";
                msgBody += "SurName:        " + drDetail["staff_Surname"].ToString().ToUpper() + "<br>";
                msgBody += "Rank:           " + drDetail["rank_short_name"].ToString().ToUpper() + "<br>";
                msgBody += "Nationality:    " + drDetail["Nationality"].ToString().ToUpper() + "<br>";
                msgBody += "Passport No:    " + drDetail["Passport_Number"].ToString().ToUpper() + "<br>";
                msgBody += "PP Issue Date:  " + drDetail["Passport_Issue_Date"].ToString().ToUpper() + "<br>";
                msgBody += "PP Expiry Date: " + drDetail["Passport_Expiry_Date"].ToString().ToUpper() + "<br>";
                msgBody += "Place of Issue: " + drDetail["Passport_PlaceOf_Issue"].ToString().ToUpper() + "<br>";
                msgBody += "Seaman Book No: " + drDetail["Seaman_Book_Number"].ToString().ToUpper() + "<br>";
                msgBody += "S/B Issue Date: " + drDetail["Seaman_Book_Issue_date"].ToString().ToUpper() + "<br>";
                msgBody += "S/B Expiry Date:" + drDetail["Seaman_Book_Expiry_Date"].ToString().ToUpper() + "<br>";
                msgBody += "Place of Issue: " + drDetail["Seaman_Book_PlaceOf_Issue"].ToString().ToUpper() + "<br>";

                msgBody += "D.O.B:          " + drDetail["Staff_Birth_Date"].ToString().ToUpper() + "<br>";
                msgBody += "P.O.B:          " + drDetail["Staff_Born_Place"].ToString().ToUpper() + "<br>";
                msgBody += "<br>";
                count++;
            }

            Filter = "ON_OFF = 0";
            dtDetail.Select("");
            drCrewDetails = dtDetail.Select(Filter, "staff_name");
            count = drCrewDetails.Length;

            msgBody += "<br><b>Off Signers:" + count + "</b><br>";
            count = 1;
            foreach (DataRow drDetail in drCrewDetails)
            {
                msgBody += count + ".<br>-----------------------------------------------------<br>";
                msgBody += "Staff Code:     " + drDetail["staff_code"].ToString().ToUpper() + "<br>";
                msgBody += "First Name:     " + drDetail["staff_name"].ToString().ToUpper() + "<br>";
                msgBody += "SurName:        " + drDetail["staff_Surname"].ToString().ToUpper() + "<br>";
                msgBody += "Rank:           " + drDetail["rank_short_name"].ToString().ToUpper() + "<br>";
                msgBody += "Nationality:    " + drDetail["Nationality"].ToString().ToUpper() + "<br>";
                msgBody += "Passport No:    " + drDetail["Passport_Number"].ToString().ToUpper() + "<br>";
                msgBody += "PP Issue Date:  " + drDetail["Passport_Issue_Date"].ToString().ToUpper() + "<br>";
                msgBody += "PP Expiry Date: " + drDetail["Passport_Expiry_Date"].ToString().ToUpper() + "<br>";
                msgBody += "Place of Issue: " + drDetail["Passport_PlaceOf_Issue"].ToString().ToUpper() + "<br>";
                msgBody += "Seaman Book No: " + drDetail["Seaman_Book_Number"].ToString().ToUpper() + "<br>";
                msgBody += "S/B Issue Date: " + drDetail["Seaman_Book_Issue_date"].ToString().ToUpper() + "<br>";
                msgBody += "S/B Expiry Date:" + drDetail["Seaman_Book_Expiry_Date"].ToString().ToUpper() + "<br>";
                msgBody += "Place of Issue: " + drDetail["Seaman_Book_PlaceOf_Issue"].ToString().ToUpper() + "<br>";

                msgBody += "D.O.B:          " + drDetail["Staff_Birth_Date"].ToString().ToUpper() + "<br>";
                msgBody += "P.O.B:          " + drDetail["Staff_Born_Place"].ToString().ToUpper() + "<br>";
                msgBody += "<br>";
                count++;
            }


            msgBody += "<b>Remarks:-</b><br>";

            msgBody += "Will arrange On Signers to board the vessel upon berthing " + Port_Name.ToUpper() + " , off Signers to disembark only after completion of cargo operations in " + Port_Name.ToUpper() + ".<br>";

            msgBody += "Kindly confirm the above plan is in order and keep us updated with vessel itinerary in order to arrange crew change accordingly.<br>";


            int MsgID = objCrew.Send_CrewNotification(0, 0, EventID, 6, msgTo, msgCC, msgBCC, msgSubject, msgBody, "", "MAIL", "", GetSessionUserID(), "DRAFT");
            if (MsgID > 0)
            {
                string js = "window.open('EmailEditor.aspx?ID=" + MsgID + "');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "NotifyMaster" + MsgID, js, true);
            }
        }
    }
    protected void SendMail_FinalCrewChangePlan(int EventID, int SessionUserID)
    {

        string Vessel_Name = " _ _ _ _ ";
        string Port_Name = " _ _ _ _ ";
        string Event_Date = " _ _ _ _ ";

        string msgTo = "";
        string msgCC = "";
        string msgBCC = "";

        DataSet ds = objCrew.Get_MailDetailsForEventID(EventID, 7);
        DataTable dtMain = ds.Tables[0];
        DataTable dtDetail = ds.Tables[1];

        if (dtMain.Rows.Count > 0)
        {
            DataRow dr = dtMain.Rows[0];

            msgTo = dr["vessel_email"].ToString();

            Vessel_Name = dr["Vessel_Name"].ToString();
            Port_Name = dr["Port_Name"].ToString();
            Event_Date = dr["Event_Date"].ToString();
            if (Event_Date != "")
            {
                Event_Date = DateTime.Parse(Event_Date).ToString("dd/MM/yyyy");
            }
            string msgSubject = "M V " + Vessel_Name.ToUpper() + " - CREW CHANGE PLAN - " + Port_Name.ToUpper() + " - " + Event_Date;

            string msgBody = " ----   System Notification: Please do not reply to this mail.   ----";
            msgBody += "<br><br>";

            //msgBody += "Sub: M V " + Vessel_Name.ToUpper() + " - CREW CHANGE PLAN - " + Port_Name.ToUpper() + " - " + Event_Date + "<br>";

            msgBody += "<b>GENERAL INSTRUCTIONS:</b><br>";

            msgBody += "<br>1) On signers personal details and flight details will be advised to the local agent by respective Manning offices. Local Agent will send Manning offices OK2B / LG messages.";
            msgBody += "<br>2) Off signers flight details will be advised to Master & Local agent shortly by us.";
            msgBody += "<br>3) Agents RIC : Kindly keep us closely updated with vessel berthing prospects and ETD. Kindly liaise with master for any additional off signers details required.";

            msgBody += @"<br>4) Master RIC : Kindly liaise with local agents for smooth crew change, please send the personal details and copies of passport and seamen book to local agents ( if required ), 
                       	Please send the required details to crew department to update sign on /off to MPA upon completion of this crew change.
       		[ sign on – ref no as per MPA agreement / Name / Rank / Nationality / DOB / PP NO / NOK name / NOK relationship / S/On date / Place Engaged ]
       		[ Sign off – Ref no as per MPA agreement / Name / Rank / S/Off date / Place Discharged .]";

            msgBody += "<br>5) Kindly acknowledge.";

            msgBody += "<br><br>Kindly keep all correspondences regarding this crew change copied to the manager";

            msgBody += "<br><br><b>EVENT DETAILS :</b>";

            string Filter = "ON_OFF = 1";
            DataRow[] drCrewDetails = dtDetail.Select(Filter, "staff_name");
            int count = drCrewDetails.Length;

            msgBody += "<br><br>On Signers: " + count + "<br>================<br>";
            count = 1;
            string ManningOfficeEmail = "";

            foreach (DataRow drDetail in drCrewDetails)
            {
                msgBody += count + ".<br>-----------------------------------------------------<br>";
                msgBody += "Staff Code:     " + drDetail["staff_code"].ToString().ToUpper() + "<br>";
                msgBody += "First Name:     " + drDetail["staff_name"].ToString().ToUpper() + "<br>";
                msgBody += "SurName:        " + drDetail["staff_Surname"].ToString().ToUpper() + "<br>";
                msgBody += "Rank:           " + drDetail["rank_short_name"].ToString().ToUpper() + "<br>";
                msgBody += "Nationality:    " + drDetail["Nationality"].ToString().ToUpper() + "<br>";
                msgBody += "Passport No:    " + drDetail["Passport_Number"].ToString().ToUpper() + "<br>";
                msgBody += "PP Issue Date:  " + drDetail["Passport_Issue_Date"].ToString().ToUpper() + "<br>";
                msgBody += "PP Expiry Date: " + drDetail["Passport_Expiry_Date"].ToString().ToUpper() + "<br>";
                msgBody += "Place of Issue: " + drDetail["Passport_PlaceOf_Issue"].ToString().ToUpper() + "<br>";
                msgBody += "Seaman Book No: " + drDetail["Seaman_Book_Number"].ToString().ToUpper() + "<br>";
                msgBody += "S/B Issue Date: " + drDetail["Seaman_Book_Issue_date"].ToString().ToUpper() + "<br>";
                msgBody += "S/B Expiry Date:" + drDetail["Seaman_Book_Expiry_Date"].ToString().ToUpper() + "<br>";
                msgBody += "Place of Issue: " + drDetail["Seaman_Book_PlaceOf_Issue"].ToString().ToUpper() + "<br>";

                msgBody += "D.O.B:          " + drDetail["Staff_Birth_Date"].ToString().ToUpper() + "<br>";
                msgBody += "P.O.B:          " + drDetail["Staff_Born_Place"].ToString().ToUpper() + "<br>";
                msgBody += "<br>";
                count++;

                string Email = drDetail["ManningOfficeEmail"].ToString().ToUpper();
                if (ManningOfficeEmail.IndexOf(Email) < 0)
                {
                    if (ManningOfficeEmail.Length > 0)
                        ManningOfficeEmail += ";";
                    ManningOfficeEmail += Email;
                }
            }

            if (ManningOfficeEmail.Length > 0)
            {
                if (msgTo.Length > 0)
                    msgTo += ";";
                msgTo += ManningOfficeEmail;
            }

            Filter = "ON_OFF = 0";
            dtDetail.Select("");
            drCrewDetails = dtDetail.Select(Filter, "staff_name");
            count = drCrewDetails.Length;

            msgBody += "<br>Off Signers:" + count + "<br>==================<br>";
            count = 1;
            ManningOfficeEmail = "";
            foreach (DataRow drDetail in drCrewDetails)
            {
                msgBody += count + ".<br>-----------------------------------------------------<br>";
                msgBody += "Staff Code:     " + drDetail["staff_code"].ToString().ToUpper() + "<br>";
                msgBody += "First Name:     " + drDetail["staff_name"].ToString().ToUpper() + "<br>";
                msgBody += "SurName:        " + drDetail["staff_Surname"].ToString().ToUpper() + "<br>";
                msgBody += "Rank:           " + drDetail["rank_short_name"].ToString().ToUpper() + "<br>";
                msgBody += "Nationality:    " + drDetail["Nationality"].ToString().ToUpper() + "<br>";
                msgBody += "Passport No:    " + drDetail["Passport_Number"].ToString().ToUpper() + "<br>";
                msgBody += "PP Issue Date:  " + drDetail["Passport_Issue_Date"].ToString().ToUpper() + "<br>";
                msgBody += "PP Expiry Date: " + drDetail["Passport_Expiry_Date"].ToString().ToUpper() + "<br>";
                msgBody += "Place of Issue: " + drDetail["Passport_PlaceOf_Issue"].ToString().ToUpper() + "<br>";
                msgBody += "Seaman Book No: " + drDetail["Seaman_Book_Number"].ToString().ToUpper() + "<br>";
                msgBody += "S/B Issue Date: " + drDetail["Seaman_Book_Issue_date"].ToString().ToUpper() + "<br>";
                msgBody += "S/B Expiry Date:" + drDetail["Seaman_Book_Expiry_Date"].ToString().ToUpper() + "<br>";
                msgBody += "Place of Issue: " + drDetail["Seaman_Book_PlaceOf_Issue"].ToString().ToUpper() + "<br>";

                msgBody += "D.O.B:          " + drDetail["Staff_Birth_Date"].ToString().ToUpper() + "<br>";
                msgBody += "P.O.B:          " + drDetail["Staff_Born_Place"].ToString().ToUpper() + "<br>";
                msgBody += "<br>";
                count++;
                //string Email = drDetail["ManningOfficeEmail"].ToString().ToUpper();
                //if (ManningOfficeEmail.IndexOf(Email) < 0)
                //{
                //    if (ManningOfficeEmail.Length > 0)
                //        ManningOfficeEmail += ";";
                //    ManningOfficeEmail += Email;
                //}
            }

            //if (ManningOfficeEmail.Length > 0)
            //{
            //    if (msgTo.Length > 0)
            //        msgTo += ";";
            //    msgTo += ManningOfficeEmail;
            //}

            msgBody += "<b>LOCAL AGENT DETAILS :</b><br><br>";

            int MsgID = objCrew.Send_CrewNotification(0, 0, EventID, 7, msgTo, msgCC, msgBCC, msgSubject, msgBody, "", "MAIL", "", GetSessionUserID(), "DRAFT");
            if (MsgID > 0)
            {
                string js = "window.open('EmailEditor.aspx?ID=" + MsgID + "');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "NotifyMaster" + MsgID, js, true);
            }
        }


    }
    protected void ImgReloadEvent_Click(object sender, EventArgs e)
    {
        try
        {
            Bind_ChangeEvent();
        }
        catch
        {
        }
    }

    protected void Bind_Assignments()
    {
        int VesselID = 0;
        if (ddlVessel.SelectedIndex > 0)
            VesselID = int.Parse(ddlVessel.SelectedValue);

        DataTable dt = objCrew.Get_CrewAssignments(VesselID, GetSessionUserID());
        gvCrewChangeEvent.DataSource = dt;
        gvCrewChangeEvent.DataBind();
        UpdatePanelAssignments.Update();

    }
    protected void Bind_ChangeEvent()
    {

        int FleetID = 0;
        int VesselID = 0;
        int EventStatus = -1;
        string FromDate = txtEventFromDate.Text;
        string ToDate = txtEventToDate.Text;

        if (txtEventFromDate.Text != "")
            FromDate = UDFLib.ConvertToDate(Convert.ToString(txtEventFromDate.Text), UDFLib.GetDateFormat()).ToString();

        if (txtEventToDate.Text != "")
            ToDate = UDFLib.ConvertToDate(Convert.ToString(txtEventToDate.Text), UDFLib.GetDateFormat()).ToString();


        string SearchText = txtSearchText.Text;

        FleetID = int.Parse(ddlFleet.SelectedValue);
        VesselID = int.Parse(ddlVessel.SelectedValue);
        EventStatus = int.Parse(ddlEventStatus.SelectedValue);
        int PortID = UDFLib.ConvertToInteger(ctlSignOnPort.SelectedValue);

        DataSet ds = objCrew.Get_CrewChangeEvents(FleetID, VesselID, EventStatus, PortID, FromDate, ToDate, SearchText, GetSessionUserID());

        UDFLib.AddParentTable(ds.Tables[0], "Events", new string[] { "PKID" },
        new string[] { "Vessel_Short_Name", "Event_Date", "Port_Name", "Event_Status", "EventRemark", "CreatedBy", "CompanyID", "ONBD_Count", "SEQ", "SEQ_Remarks", "SEQ_Class", "ClosedBy", "Date_Of_Creatation", "DateClosed" }, "EventMembers");

        rpt1.DataSource = ds;
        rpt1.DataMember = "Events";
        rpt1.DataBind();

        if (ddlVessel.SelectedIndex > 0)
        {

            lblEventVessel.Text = ddlVessel.SelectedItem.Text;
        }
        else
        {
            if (ddlFleet.SelectedIndex > 0)
                lblEventVessel.Text = ddlFleet.SelectedItem.Text;
            else
                lblEventVessel.Text = "All Vessels";
        }

        pnlCrewChangeEvent.Visible = true;
        UpdatePanelEvents.Update();
    }

    protected void btnReload_Click(object sender, EventArgs e)
    {
        Bind_Assignments();
        Bind_ChangeEvent();
    }
    protected void ddlEventStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        Bind_ChangeEvent();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (txtEventFromDate.Text.Trim() != "" && !UDFLib.DateCheck(txtEventFromDate.Text))
        {
            string js3 = "alert('Enter valid From Date" + DateFormatMessage + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "searchError", js3, true);
            return;
        }

        if (txtEventToDate.Text.Trim() != "" && !UDFLib.DateCheck(txtEventToDate.Text))
        {

            string js3 = "alert('Enter valid To Date" + DateFormatMessage + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "searchError", js3, true);
            return;
        }
        Bind_Assignments();
        Bind_ChangeEvent();
    }
    protected void SendMail_NotifyManningOffice_OnEventDelete(int EventID, int SessionUserID)
    {

        string Vessel_Name = " _ _ _ _ ";
        string Port_Name = " _ _ _ _ ";
        string Event_Date = " _ _ _ _ ";

        string msgTo = "";
        string msgCC = "";
        string msgBCC = "";

        string msgBody = "";
        string msgSubject = "";

        DataSet ds = objCrew.Get_MailDetailsForEventID(EventID, 5);
        DataTable dtMain = ds.Tables[0];
        DataTable dtDetail = ds.Tables[1];

        foreach (DataRow dr in dtMain.Rows)
        {
            msgTo = dr["Email1"].ToString();
            msgCC = "";
            Vessel_Name = dr["Vessel_Name"].ToString();
            Port_Name = dr["Port_Name"].ToString();
            Event_Date = dr["Event_Date"].ToString();

            if (Event_Date != "")
            {
                Event_Date = DateTime.Parse(Event_Date).ToString("dd/MM/yyyy");
            }

            int ManningOfficeID = int.Parse(dr["ManningOfficeID"].ToString());
            string Filter = "ON_OFF = 1 AND ManningOfficeID = " + ManningOfficeID;
            dtDetail.Select("");
            DataRow[] drCrewDetails = dtDetail.Select(Filter, "staff_name");
            int count = drCrewDetails.Length;

            if (count > 0)
            {
                msgSubject = "M V " + Vessel_Name.ToUpper() + " - CREW CHANGE PLAN CANCELLED - " + Port_Name.ToUpper() + " - " + Event_Date;

                msgBody = " ----   System Notification: Please do not reply to this mail.   ----";
                msgBody += "<br><br>";

                msgBody += "Please note that the intended crew change plan is cancelled for the following staffs";

                msgBody += "<br><br><b>On Signers:" + count + "</b><br>===============<br>";

                count = 1;
                foreach (DataRow drDetail in drCrewDetails)
                {
                    msgBody += count + ".<br>-----------------------------------------------------<br>";
                    msgBody += "Staff Code:     " + drDetail["staff_code"].ToString().ToUpper() + "<br>";
                    msgBody += "First Name:     " + drDetail["staff_name"].ToString().ToUpper() + "<br>";
                    msgBody += "SurName:        " + drDetail["staff_Surname"].ToString().ToUpper() + "<br>";
                    msgBody += "Rank:           " + drDetail["rank_short_name"].ToString().ToUpper() + "<br>";
                    msgBody += "Nationality:    " + drDetail["Nationality"].ToString().ToUpper() + "<br>";
                    msgBody += "Passport No:    " + drDetail["Passport_Number"].ToString().ToUpper() + "<br>";

                    msgBody += "PP Issue Date:  " + drDetail["Passport_Issue_Date"].ToString().ToUpper() + "<br>";
                    msgBody += "PP Expiry Date: " + drDetail["Passport_Expiry_Date"].ToString().ToUpper() + "<br>";
                    msgBody += "Place of Issue: " + drDetail["Passport_PlaceOf_Issue"].ToString().ToUpper() + "<br>";
                    msgBody += "Seaman Book No: " + drDetail["Seaman_Book_Number"].ToString().ToUpper() + "<br>";
                    msgBody += "S/B Issue Date: " + drDetail["Seaman_Book_Issue_date"].ToString().ToUpper() + "<br>";
                    msgBody += "S/B Expiry Date:" + drDetail["Seaman_Book_Expiry_Date"].ToString().ToUpper() + "<br>";
                    msgBody += "Place of Issue: " + drDetail["Seaman_Book_PlaceOf_Issue"].ToString().ToUpper() + "<br>";

                    msgBody += "D.O.B:          " + drDetail["Staff_Birth_Date"].ToString().ToUpper() + "<br>";
                    msgBody += "P.O.B:          " + drDetail["Staff_Born_Place"].ToString().ToUpper() + "<br>";
                    msgBody += "<br>";
                    count++;

                }

                msgBody += "We will revert with the Updated Crew Change Plan message in due course.";
                msgBody += "<br>";

                int MsgID = objCrew.Send_CrewNotification(0, ManningOfficeID, EventID, 5, msgTo, msgCC, msgBCC, msgSubject, msgBody, "", "MAIL", "", GetSessionUserID(), "DRAFT");
                if (MsgID > 0)
                {
                    string js = "window.open('EmailEditor.aspx?ID=" + MsgID + "');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "NotifyManningOffice" + ManningOfficeID, js, true);
                }
            }
        }

    }

    protected void gvSelectedONSigners_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int CrewID = UDFLib.ConvertToInteger(DataBinder.Eval(e.Row.DataItem, "ID").ToString());
            int Vessel_ID = UDFLib.ConvertToInteger(ddlVessel.SelectedValue);
            int Rank_ID = UDFLib.ConvertToInteger(DataBinder.Eval(e.Row.DataItem, "Rank_ID").ToString());

            DataTable dt = objCrew.Get_VoyagesForSignOnEvent(CrewID, Vessel_ID, GetSessionUserID());

            DropDownList ddlVoyages = (DropDownList)e.Row.FindControl("ddlVoyages");
            if (ddlVoyages != null)
            {
                ddlVoyages.DataSource = dt;
                ddlVoyages.DataBind();
            }

            //-- Nationality Check -- //
            int JoiningRank = 0;
            string Rank_Name = "";

            DropDownList ddlRank = (DropDownList)e.Row.FindControl("ddlRank");
            DropDownList ddlRankScale = (DropDownList)e.Row.FindControl("ddlRankScale");
            if (ddlRank != null)
            {
                JoiningRank = UDFLib.ConvertToInteger(ddlRank.SelectedValue);
                Rank_Name = ddlRank.SelectedItem.Text;
                if (ddlRankScale != null && JoiningRank > 0 && RankScaleConsidered == 1)
                {
                    DataTable dtRankScale = objCrewAdmin.Get_RankScaleList(JoiningRank);
                    ddlRankScale.DataSource = dtRankScale;
                    ddlRankScale.DataTextField = "RankScaleName";
                    ddlRankScale.DataValueField = "ID";
                    ddlRankScale.DataBind();
                    ddlRankScale.Items.Insert(0, new ListItem("-SELECT-", "0"));
                }
            }


            string Vessel_Name = ddlVessel.SelectedItem.Text;
            int EventID = UDFLib.ConvertToInteger(hdnEventID.Value);
            int NationalityCheck = objCrew.NationalityCheck_NewJoiner(Vessel_ID, CrewID, JoiningRank, GetSessionUserID(), EventID, 0);

            Button btnSendForApproval = (Button)e.Row.FindControl("btnSendForApproval");

            if (NationalityCheck <= 0)
            {
                btnSaveAdditional.Enabled = false;

                if (btnSendForApproval != null)
                    btnSendForApproval.Attributes.Add("onclick", "showNationalityApproval(" + Vessel_ID + "," + EventID + "," + CrewID + "," + Rank_ID + "," + JoiningRank + ",'" + Vessel_Name + "','" + Rank_Name + "');return false;");

                lblMsg.Text = "<font color=black>There are 2 or more staffs are on-board this vessel, for the same rank/rank category for some of the selected staff for this event. <br>Please click [Send for Approval] button to take approval.</font>";

            }
            else
            {
                if (btnSendForApproval != null)
                    btnSendForApproval.Visible = false;
                btnSaveAdditional.Enabled = true;
                lblMsg.Text = "";
            }
        }
    }

    protected void gvSelectedOffSigners_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int CrewID = UDFLib.ConvertToInteger(DataBinder.Eval(e.Row.DataItem, "ID").ToString());
            int Vessel_ID = UDFLib.ConvertToInteger(ddlVessel.SelectedValue);

            DataTable dt = objCrew.Get_VoyagesForSignOffEvent(CrewID, Vessel_ID, GetSessionUserID());

            DropDownList ddlSOffVoyages = (DropDownList)e.Row.FindControl("ddlSOffVoyages");
            if (ddlSOffVoyages != null)
            {
                ddlSOffVoyages.DataSource = dt;
                ddlSOffVoyages.DataBind();
            }
        }
    }

    protected void gvCrewList_OnSigner_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string InvalidText = DataBinder.Eval(e.Row.DataItem, "InvalidText").ToString();
            if (InvalidText.Length > 0)
            {
                ((CheckBox)e.Row.FindControl("chkSelect")).Visible = false;
                ImageButton ImgInvalid = (ImageButton)e.Row.FindControl("ImgInvalid");
                if (ImgInvalid != null)
                {
                    ImgInvalid.Visible = true;
                    ImgInvalid.Attributes.Add("Title", "cssbody=[dvbdy1] cssheader=[dvhdr1] header=[Missing Data/ Validation] body=[" + InvalidText + "]");
                }
            }
        }
    }

    protected void btnNationalityApproval_Click(object sender, EventArgs e)
    {
        int Vessel_ID = UDFLib.ConvertToInteger(hdnAppVesselID.Value);
        int CrewID = UDFLib.ConvertToInteger(hdnAppCrewID.Value);
        int CurrentRank_ID = UDFLib.ConvertToInteger(hdnAppCurrentRankID.Value);
        int JoiningRank_ID = UDFLib.ConvertToInteger(hdnAppJoiningRankID.Value);
        int EventID = UDFLib.ConvertToInteger(hdnAppEventID.Value);

        int retval = objCrew.NationalityCheck_SendForApproval(Vessel_ID, CrewID, CurrentRank_ID, JoiningRank_ID, txtAppRequest.Text, GetSessionUserID(), EventID, 0);
        if (retval > 0)
        {
            string js = "alert('Approval has been sent');hideModal('dvNationalityApproval');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "script1", js, true);

        }
        if (retval == -1)
        {
            string js2 = "alert('A previous request for approval has been pending for the same');hideModal('dvNationalityApproval');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "script2", js2, true);

        }
    }

    protected void ddlRank_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RankScaleConsidered == 1)
        {
            DropDownList ddl = (DropDownList)sender;

            GridViewRow row = (GridViewRow)ddl.Parent.Parent;

            int idx = row.RowIndex;

            DropDownList ddlRank = (DropDownList)row.Cells[3].FindControl("ddlRank");
            int RankId = int.Parse(ddlRank.SelectedValue);

            DropDownList ddlRankScale = (DropDownList)row.Cells[4].FindControl("ddlRankScale");

            DataTable dt = objCrewAdmin.Get_RankScaleList(RankId);
            ddlRankScale.DataSource = dt;
            ddlRankScale.DataTextField = "RankScaleName";
            ddlRankScale.DataValueField = "ID";
            ddlRankScale.DataBind();
            ddlRankScale.Items.Insert(0, new ListItem("-SELECT-", "0"));
        }
    }
}