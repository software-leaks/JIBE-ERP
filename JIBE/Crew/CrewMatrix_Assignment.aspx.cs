using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.Infrastructure;
using SMS.Business.Crew;
using SMS.Properties;
using System.IO;
using System.Web.UI.HtmlControls;

public partial class Crew_CrewMatrix_Assignment : System.Web.UI.Page
{
    BLL_Crew_CrewDetails objCrew = new BLL_Crew_CrewDetails();
    BLL_Crew_Admin objCrewAdmin = new BLL_Crew_Admin();
    BLL_Crew_Contract objBLLInfra = new BLL_Crew_Contract();
    BLL_Infra_VesselLib objVessel = new BLL_Infra_VesselLib();
    BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
    UserAccess objUA = new UserAccess();
    public int OnSignnerCrewId = 0, OffSignerCrewId = 0, ApproveRights = 0, VesselId = 0;
    int RankId = 0;
    public string DateFormat = "";
    #region PageLoad
    protected void Page_Load(object sender, EventArgs e)
    {
        DateFormat = UDFLib.GetDateFormat();//Get User date format
        CalendarExtender5.Format = DateFormat;
        if (GetSessionUserID() == 0)
        {
            string Host = Request.Url.AbsoluteUri.ToString().Substring(0, Request.Url.AbsoluteUri.ToString().ToLower().IndexOf("/crew/")) + "/";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "redirecttologin", "window.parent.location = '" + Host + "Account/Login.aspx';", true);
            return;
        }
        else
            UserAccessValidation();

        if (Request.QueryString.Count != 0)
        {
            if (Request.QueryString["OnSignnerCrewId"] != null)
                OnSignnerCrewId = UDFLib.ConvertToInteger(Request.QueryString["OnSignnerCrewId"].ToString());
            if (Request.QueryString["OffSignnerCrewId"] != null)
                OffSignerCrewId = UDFLib.ConvertToInteger(Request.QueryString["OffSignnerCrewId"].ToString());
            if (Request.QueryString["VesselId"] != null)
                VesselId = UDFLib.ConvertToInteger(Request.QueryString["VesselId"].ToString());
            if (Request.QueryString["RankId"] != null)
                RankId = UDFLib.ConvertToInteger(Request.QueryString["RankId"].ToString());
            ViewState["RankId"] = RankId;

            //Depending upon client configuration whether to consider Rank Scale or not.Depending upon Vessel flag, Rank Scale & Nationality wages are defined 
            DataTable dtWages = objCrewAdmin.GetWagesSettings();
            if (dtWages != null && dtWages.Rows.Count > 0)
            {
                if (Convert.ToBoolean(dtWages.Rows[0]["RankScaleConsidered"]) == true)
                {
                    ViewState["RankScaleConsidered"] = 1;
                    gvSignOnCrew.Columns[gvSignOnCrew.Columns.Count - 4].Visible = true;
                }
                else
                {
                    ViewState["RankScaleConsidered"] = 0;
                    gvSignOnCrew.Columns[gvSignOnCrew.Columns.Count - 4].Visible = false;
                }
                if (Convert.ToBoolean(dtWages.Rows[0]["NationalityConsidered"]) == true)
                    ViewState["NationalityConsidered"] = 1;
                else
                    ViewState["NationalityConsidered"] = 0;
            }

            if (Request.QueryString["Method"] == "CheckAssign")
            {
                string VesselName = "";
                DataTable dtCrewId = new DataTable();
                dtCrewId.Columns.Add("CrewId");

                DataRow dr1 = dtCrewId.NewRow();
                dr1["CrewId"] = OnSignnerCrewId;
                dtCrewId.Rows.Add(dr1);

                CheckAssignment(OffSignerCrewId, OnSignnerCrewId, ref VesselName);
                return;
            }
            if (Request.QueryString["Method"] == "DeleteAssign")
            {
                DeleteAssignment(OnSignnerCrewId);
                return;
            }
        }
        if (!IsPostBack)
        {
            DataTable dtPersonalDetails = objCrew.Get_CrewPersonalDetailsByID(OnSignnerCrewId);
            gvSignOnCrew.DataSource = dtPersonalDetails;
            gvSignOnCrew.DataBind();
            ViewState["Staff_Nationality"] = int.Parse(dtPersonalDetails.Rows[0]["Staff_Nationality"].ToString());
            DataSet ds = BLL_Crew_CrewList.Get_VesselTypeForCrewMatrix(VesselId);
            DataTable dtEventDate = ds.Tables[1];
            string PortName = "";
            foreach (DataRow item in dtEventDate.Rows)
            {
                PortName = Convert.ToString(item["PORT_NAME"]);
                if (PortName != "")
                {
                    if (PortName.Split('-').Length == 2)
                        item["PORT_NAME"] = UDFLib.ConvertUserDateFormat(PortName.Split('-')[0]) + "  " + PortName.Split('-')[1];
                    else
                        item["PORT_NAME"] = UDFLib.ConvertUserDateFormat(Convert.ToString(item["PORT_NAME"]));
                }
            }

            ddlEvents.DataSource = dtEventDate;
            ddlEvents.DataBind();
            ddlEvents.Items.Insert(0, new ListItem("- SELECT -", "0"));
            ddlEvents.SelectedIndex = 0;
        }

        //disable history date
        CalendarExtender5.StartDate = DateTime.Now;
    }
    #endregion
    #region Grid Event
    protected void gvSelectedONSigners_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int CrewID = UDFLib.ConvertToInteger(DataBinder.Eval(e.Row.DataItem, "ID").ToString());
            //int Rank_ID = UDFLib.ConvertToInteger(DataBinder.Eval(e.Row.DataItem, "Rank_ID").ToString());
            int Rank_ID = UDFLib.ConvertToInteger(ViewState["RankId"].ToString());

            //-- Nationality Check -- //
            int JoiningRank = 0;
            string Rank_Name = "";

            DropDownList ddlRank = (DropDownList)e.Row.FindControl("ddlRank");
            ddlRank.SelectedValue = Rank_ID.ToString();
            DropDownList ddlRankScale = (DropDownList)e.Row.FindControl("ddlRankScale");
            if (ddlRank != null)
            {
                JoiningRank = UDFLib.ConvertToInteger(ddlRank.SelectedValue);
                Rank_Name = ddlRank.SelectedItem.Text;
                if (ddlRankScale != null && JoiningRank > 0 && ViewState["RankScaleConsidered"] != null)
                {
                    if (ViewState["RankScaleConsidered"].Equals(1))
                    {
                        DataTable dtRankScale;
                        //Only RankScale with wages are populated in RankScale dropdown
                        if (ViewState["NationalityConsidered"].Equals(1))
                        {
                            int Staff_Nationality;
                            if (ViewState["Staff_Nationality"] != null)
                            {
                                Staff_Nationality = int.Parse(ViewState["Staff_Nationality"].ToString());
                            }
                            else
                            {
                                DataTable dtPersonalDetails = objCrew.Get_CrewPersonalDetailsByID(CrewID);
                                Staff_Nationality = int.Parse(dtPersonalDetails.Rows[0]["Staff_Nationality"].ToString());
                            }
                            dtRankScale = objCrewAdmin.Get_RankScaleListForWages(JoiningRank, Staff_Nationality);
                        }
                        else
                        {
                            dtRankScale = objCrewAdmin.Get_RankScaleListForWages(JoiningRank, 0);
                        }
                        ddlRankScale.DataSource = dtRankScale;
                        ddlRankScale.DataTextField = "RankScaleName";
                        ddlRankScale.DataValueField = "ID";
                        ddlRankScale.DataBind();
                        ddlRankScale.Items.Insert(0, new ListItem("-SELECT-", "0"));

                        //User with Approve rights will be able to change Rank Scale

                        DataTable dtRank = objCrew.Get_GET_CrewRankScale(CrewID);
                        int RankId1 = int.Parse(dtRank.Rows[0]["RankId"].ToString());
                        if (JoiningRank == RankId1)
                        {
                            int RankScaleId = int.Parse(dtRank.Rows[0]["RankScaleId"].ToString());
                            if (RankId1 > 0)
                            {
                                ddlRank.Enabled = false;
                                if (RankScaleId > 0)
                                {
                                    ddlRankScale.SelectedValue = RankScaleId.ToString();
                                    ddlRankScale.Enabled = false;
                                }
                            }
                        }

                        if (ApproveRights == 1)
                        {
                            ddlRankScale.Enabled = true;
                        }
                    }

                }
                int EventID = UDFLib.ConvertToInteger(ddlEvents.SelectedValue);
                int NationalityCheck = objCrew.NationalityCheck_NewJoiner(VesselId, OnSignnerCrewId, JoiningRank, GetSessionUserID(), EventID, 0);

                Button btnSendForApproval = (Button)e.Row.FindControl("btnSendForApproval");

                if (NationalityCheck <= 0)
                {
                    btnAssign.Enabled = false;
                    int UserCompanyID = UDFLib.ConvertToInteger(getSessionString("USERCOMPANYID"));
                    int Vessel_Manager = UDFLib.ConvertToInteger(getSessionString("USERCOMPANYID"));

                    if (Session["UTYPE"].ToString() == "VESSEL MANAGER")
                        Vessel_Manager = UserCompanyID;
                    DataTable dtVessel = objVessel.Get_VesselList(0, 0, Vessel_Manager, "", UserCompanyID);
                    dtVessel.PrimaryKey = new DataColumn[] { dtVessel.Columns["VESSEL_ID"] };
                    DataRow dr = dtVessel.Rows.Find(VesselId);
                    string Vessel_Name = dr["VESSEL_NAME"].ToString();
                    lblAppVessel.Text = Vessel_Name;

                    lblAppRank.Text = Rank_Name;

                    if (btnSendForApproval != null)
                        btnSendForApproval.Attributes.Add("onclick", "showNationalityApproval(" + VesselId + "," + EventID + "," + CrewID + "," + Rank_ID + "," + JoiningRank + ",'" + Vessel_Name + "','" + Rank_Name + "');return false;");

                    string js = "alert('There are 2 or more staffs on-board on this vessel, for the same rank/rank category for some of the selected staff for this event.\\nPlease click [Send for Approval] button to take approval.');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUser2", js, true);
                }
                else
                {
                    if (btnSendForApproval != null)
                        btnSendForApproval.Visible = false;
                    gvSignOnCrew.Columns[gvSignOnCrew.Columns.Count - 1].Visible = false;
                    btnAssign.Enabled = true;
                }
            }
        }
    }
    #endregion
    #region Retrive Session value
    /// <summary>
    /// To get current session values
    /// </summary>
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
    protected int GetSessionUserID()
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

        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
        {
            divEvent.Visible = false;
            gvSignOnCrew.Visible = false;
            lblMsg.Text = "You don't have sufficient privilege to access the requested information.";
        }

        if (objUA.Add == 0)
        {
            btnAssign.Enabled = false;
        }

        if (objUA.Approve == 1)
        {
            ApproveRights = 1;
        }

    }
    #endregion
    #region Check and Delete Assignment
    /// <summary>
    /// Check whether Crew has any open assignment , whether crew is having any voyage created without SignOn Date(either through Crew Planning/Event Planner /Voyage created directly
    /// </summary>
    /// <param name="OffSignerCrewId">Crew who is </param>
    /// <param name="OnSignnerCrewId"></param>
    private void CheckAssignment(int OffSignerCrewId, int OnSignnerCrewId, ref string VesselName)
    {
        try
        {
            int Res = objCrew.Check_CrewAssignment(OffSignerCrewId, OnSignnerCrewId, ref VesselName);

            Response.Clear();
            Response.Write(Res.ToString() + "|" + VesselName);
            Response.End();
        }
        catch (Exception ex)
        {

        }
    }
    /// <summary>
    /// Will delete the current open assignment for the onsigner
    /// </summary>
    /// <param name="OnSignnerCrewId"></param>
    private void DeleteAssignment(int OnSignnerCrewId)
    {
        try
        {
            int Res;
            Res = objCrew.Delete_CrewOpenAssignment(OnSignnerCrewId, GetSessionUserID());

            Response.Clear();
            Response.Write(Res);
            Response.End();
        }
        catch (Exception)
        {

        }
    }
    #endregion
    #region Click events

    /// <summary>
    /// Add crew to event as addition staff
    /// </summary>
    protected void AddCrewToEvent()
    {
        int retval = 0;
        try
        {
            int EventID = UDFLib.ConvertToInteger(ddlEvents.SelectedValue);

            if (EventID > 0)
            {
                int iCrewID = 0;
                int iJoining_Rank = 0;
                string COC_Date = "";
                string Joining_Date = "";
                int VoyID = 0;
                int RankId = 0;
                int RankScaleId = 0;

                iCrewID = UDFLib.ConvertToInteger(((HiddenField)gvSignOnCrew.Rows[0].FindControl("hdnCrewID")).Value);

                iJoining_Rank = UDFLib.ConvertToInteger(((DropDownList)gvSignOnCrew.Rows[0].FindControl("ddlRank")).SelectedValue);
                Joining_Date = UDFLib.ConvertToDefaultDt(((TextBox)gvSignOnCrew.Rows[0].FindControl("txtJoinDate")).Text).ToString();
                COC_Date = UDFLib.ConvertToDefaultDt(((TextBox)gvSignOnCrew.Rows[0].FindControl("txtCOCDate")).Text).ToString();
                RankId = UDFLib.ConvertToInteger(((DropDownList)gvSignOnCrew.Rows[0].FindControl("ddlRank")).SelectedValue);

                if (Joining_Date != "" && RankId > 0)
                {
                    if (UDFLib.ConvertToInteger(ViewState["RankScaleConsidered"]) == 1)
                    {
                        RankScaleId = UDFLib.ConvertToInteger(((DropDownList)gvSignOnCrew.Rows[0].FindControl("ddlRankScale")).SelectedValue);
                    }
                    if (UDFLib.ConvertToInteger(ViewState["RankScaleConsidered"]) == 0 || RankScaleId > 0)
                        retval = objCrew.AddCrewTo_CrewChangeEvent(EventID, iCrewID, 1, 0, GetSessionUserID(), Joining_Date, COC_Date, iJoining_Rank, 0, RankScaleId);
                    else
                    {
                        string js = "Please select Wage Rank Scale.";
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
                    if (ViewState["RankScaleConsidered"] != null && UDFLib.ConvertToInteger(ViewState["RankScaleConsidered"]) == 1)
                    {
                        RankScaleId = UDFLib.ConvertToInteger(((DropDownList)gvSignOnCrew.Rows[0].FindControl("ddlRankScale")).SelectedValue);
                        if (RankScaleId == 0)
                            js1 += "Select Rank Scale.";
                    }
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "SaveAddError", "alert('" + js1 + "');", true);
                }

                if (retval > 0)
                {
                    string js = "alert('The Selected Crew member assigned to the selected vessel.');hideModal('divVesselType');parent.hideModal('dvPopupFrame1');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script1", js, true);

                }
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    /// <summary>
    /// A mail is send to Approver to Approve/Reject for Staff with same nationality should be allowed on vessel 
    /// </summary>
    protected void btnNationalityApproval_Click(object sender, EventArgs e)
    {
        int retval = objCrew.NationalityCheck_SendForApproval(VesselId, OnSignnerCrewId, 0, RankId, txtAppRequest.Text, GetSessionUserID(), 0, 0);
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
    /// <summary>
    /// Default EOC months is    set in Contract period library page according to Ranks.If not set txtCOCDate will be blanck
    /// </summary>
    protected void txtJoiningDate_TextChanged(object sender, EventArgs e)
    {
        int Joining_Rank = UDFLib.ConvertToInteger(((DropDownList)gvSignOnCrew.Rows[0].FindControl("ddlRank")).SelectedValue);
        string Joining_Date = ((TextBox)gvSignOnCrew.Rows[0].FindControl("txtJoinDate")).Text;
        try
        {
            DateTime.Parse(UDFLib.ConvertToDefaultDt(Joining_Date));
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "$('#gvSignOnCrew_ctl02_txtCOCDate').val('');alert('Enter valid Joining Date" + UDFLib.DateFormatMessage() + "');", true);
            return;
        }

        if (Joining_Rank > 0)
        {
            if (Joining_Date != "")
            {
                int Days = 0;

                try
                {
                    DataTable dt = new DataTable();
                    dt = objBLLInfra.Get_Crew_Contract_Period_List(Joining_Rank);

                    if (dt.Rows.Count > 0 && dt.Rows[0]["Days"].ToString() != "")
                        Days = int.Parse(dt.Rows[0]["Days"].ToString());

                    if (Days > 0)
                    {
                        ((TextBox)gvSignOnCrew.Rows[0].FindControl("txtCOCDate")).Text = DateTime.Parse(UDFLib.ConvertToDefaultDt(Joining_Date)).AddDays(Days).ToString(DateFormat);
                    }
                }
                catch
                {
                    string js = "parent.ShowNotification('Alert','Enter valid Joining Date" + UDFLib.DateFormatMessage() + "',true)";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", js, true);
                }
            }
        }
    }
    /// <summary>
    ///  Create New Event for that vessel without port name or add crew to existing event
    /// </summary>
    protected void btnAssign_Click(object sender, EventArgs e)
    {
        DataTable dtCrewId = new DataTable();
        dtCrewId.Columns.Add("CrewId");

        DataRow dr1 = dtCrewId.NewRow();
        dr1["CrewId"] = OnSignnerCrewId;
        dtCrewId.Rows.Add(dr1);

        DataTable dtVesselType = objCrew.CheckVesselTypeForCrew(dtCrewId, VesselId);
        if (dtVesselType.Rows.Count > 0 && !dtVesselType.Rows[0][0].ToString().Equals(""))
        {
            rdbVesselTypeAssignmentList.SelectedValue = "1";
            lblConfirmationTitle.Text = dtVesselType.Rows[0]["StaffNames"].ToString() + " does not have the required vessel type assignment.Choose if you want to add " + dtVesselType.Rows[0]["VesselType"].ToString() + " to his vessel type list,or to assign him one time only";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "script1", "showModal('divVesselType');", true);
        }
        else
        {
            Assign();
        }
    }
    protected void Assign()
    {
        if (txtEventDate.Text.ToString() != "")
        {
            int EventID = objCrew.CREATE_CrewChangeEvent(VesselId, UDFLib.ConvertToDefaultDt(txtEventDate.Text).ToString(), 0, GetSessionUserID(), 0);
            if (EventID > 0)
            {
                DataSet ds = BLL_Crew_CrewList.Get_VesselTypeForCrewMatrix(VesselId);
                DataTable dtEventDate = ds.Tables[1];
                ddlEvents.DataSource = dtEventDate;
                ddlEvents.DataBind();
                ddlEvents.Items.Insert(0, new ListItem("- SELECT -", "0"));
                ddlEvents.SelectedValue = EventID.ToString();

                txtEventDate.Text = "";

                AddCrewToEvent();
            }
            else if (EventID == -1)
            {
                string js = "alert('Event date is not inside the vessel arrival and departure dates!!');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUser", js, true);
            }
        }
        else
        {
            AddCrewToEvent();
        }
    }
    public void btnAssignVesselType_Click(object sender, EventArgs e)
    {
        try
        {
            int VesselTypeAssignment = int.Parse(rdbVesselTypeAssignmentList.SelectedValue);
            int Result = 0;
            DataTable dtCrewId = new DataTable();
            dtCrewId.Columns.Add("CrewId");

            DataRow dr1 = dtCrewId.NewRow();
            dr1["CrewId"] = UDFLib.ConvertToInteger(((HiddenField)gvSignOnCrew.Rows[0].FindControl("hdnCrewID")).Value);
            dtCrewId.Rows.Add(dr1);

            if (VesselTypeAssignment == 1)
            {
                objCrew.CRW_INS_AddVesselTye(dtCrewId, VesselId, GetSessionUserID(), ref Result);
            }
            Assign();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    /// <summary>
    ///Close the popup and return to main screen
    /// <summary>
    protected void btnCancelAdditional_Click(object sender, EventArgs e)
    {
        string js = "parent.hideModal('dvPopupFrame1');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "script1", js, true);
    }
    #endregion
}