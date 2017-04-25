using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using SMS.Business.Crew;
using SMS.Business.Infrastructure;
using SMS.Properties;
using System.Text.RegularExpressions;

public partial class Crew_CrewDetails_Prejoining : System.Web.UI.Page
{
    BLL_Crew_CrewDetails objBLLCrew = new BLL_Crew_CrewDetails();
    BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
    BLL_Infra_VesselLib objVessel = new BLL_Infra_VesselLib();
    UserAccess objUA = new UserAccess();
    public string TodayDateFormat = "";
    public string DateFormat = "";

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
        try
        {
            if (Request.QueryString["Page"] == null)
            {
                flstPreviousOperator.Attributes.Remove("style");
                flstPreJoiningExp.Attributes.Remove("style");
                flstPreJoiningExpInterview.Attributes.Remove("style");
            }

            TodayDateFormat = UDFLib.DateFormatMessage();
            DateFormat = UDFLib.GetDateFormat();

            if (!IsPostBack)
            {
                if (GetSessionUserID() == 0)
                {
                    lblMsg.Text = "Session Expired!! Log-out and log-in again.";
                }
                else
                {
                    CalendarExtender4.Format = CalendarExtender3.Format = DateFormat;

                    AjaxControlToolkit4.CalendarExtender calEx = GridView_PreJoiningExp.FindControl("CalendarExtender6") as AjaxControlToolkit4.CalendarExtender;

                    if (calEx != null)
                        calEx.Format = DateFormat;

                    UserAccessValidation();
                    int CrewID = UDFLib.ConvertToInteger(Request.QueryString["ID"]);

                    if (objUA.View == 1)
                    {
                        pnlView_PreJoining.Visible = true;
                        pnlView_PreviousContacts.Visible = true;
                        Load_PreJoiningExpFromInterview(CrewID);
                        Load_CrewPreviousContactDetails(CrewID);
                        Load_CrewPersonalDetails(CrewID);
                    }

                    DataTable dtVesselType = objVessel.Get_VesselTypeList();
                    ddlPJVesselType.DataSource = dtVesselType;
                    ddlPJVesselType.DataTextField = "VesselTypes";
                    ddlPJVesselType.DataValueField = "VesselTypes";
                    ddlPJVesselType.DataBind();
                    ddlPJVesselType.Items.Insert(0, new ListItem("-SELECT-", "0"));
                    ddlPJVesselType.SelectedIndex = 0;

                    int UserCompanyID = UDFLib.ConvertToInteger(getSessionString("USERCOMPANYID"));
                    DataTable dtvessels = objVessel.Get_VesselListPreJoining(UserCompanyID);
                    ViewState["DtVessel"] = dtvessels;
                    ddlVessel.DataSource = dtvessels;
                    ddlVessel.DataTextField = "VESSEL_NAME";
                    ddlVessel.DataValueField = "VESSEL_ID";
                    ddlVessel.DataBind();
                    ddlVessel.Items.Insert(0, new ListItem("-SELECT-", "0"));
                    ddlVessel.SelectedIndex = 0;

                    Session["VesselManager"] = Convert.ToString(dtvessels.Rows[0]["VesselManager"]);
                    hdnVesselManager.Value = Convert.ToString(dtvessels.Rows[0]["VesselManager"]);
                }
                if (Session["UTYPE"].ToString() == "MANNING AGENT")
                    chkCurrentOperator.Visible = false;
                else
                    chkCurrentOperator.Visible = true;
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
            lblMsg.Text = "You don't have sufficient privilege to access the requested information.";

        if (objUA.Add == 0)
        {
            ImgAdd_PreJoiningExp.Visible = false;
        }
        if (objUA.Edit == 0)
        {
            lnkPreviousContacts.Visible = false;

            GridView_PreJoiningExp.Columns[GridView_PreJoiningExp.Columns.Count - 2].Visible = false;

        }
        if (objUA.Delete == 0)
        {
            GridView_PreJoiningExp.Columns[GridView_PreJoiningExp.Columns.Count - 1].Visible = false;

        }
        if (objUA.Approve == 0)
        {
        }
        //-- MANNING OFFICE LOGIN --

    }
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }
    public int GetCrewID()
    {
        try
        {
            if (Request.QueryString["ID"] != null)
            {
                return int.Parse(Request.QueryString["ID"].ToString());
            }
            else
                return 0;
        }
        catch { return 0; }
    }

    protected void Load_PreJoiningExpFromInterview(int ID)
    {
        DataTable dt = objBLLCrew.Get_CrewPreJoiningExp_FromInterview(ID);
        if (dt.Rows.Count > 0)
        {
            lblPreJoiningExperiences.Text = dt.Rows[0][0].ToString();
        }

    }

    protected void ImgReloadPreJoining_Click(object sender, ImageClickEventArgs e)
    {
        GridView_PreJoiningExp.DataBind();
        Load_CrewPreviousContactDetails(GetCrewID());
    }
    protected void ImgAdd_PreJoiningExp_Click(object sender, ImageClickEventArgs e)
    {
        ClearPreJoiningDetails();
        hdnVesselManager.Value = Convert.ToString(Session["VesselManager"]);
        string AddPreJoining = String.Format("showModal('dvAddPreJoiningExp',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddPreJoining", AddPreJoining, true);
    }
    /// <summary>
    /// Save / Update Pre Joining Experience details
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSavePreJoiningExp_Click(object sender, EventArgs e)
    {
        try
        {
            int BHP = 0, GRT = 0;

            DateTime? ToDate, FromDate;
            if (txtPJDateTo.Text != "" && txtPJDateFrom.Text != "")
            {
                try
                {
                    ToDate = DateTime.Parse(UDFLib.ConvertToDefaultDt(txtPJDateTo.Text));
                    FromDate = DateTime.Parse(UDFLib.ConvertToDefaultDt(txtPJDateFrom.Text));
                }
                catch
                {
                    lblMsgPreJoining.Text = "Enter valid Period of Service From or Period of Service To Date" + TodayDateFormat;
                    CurrentOperatorCheck();
                    string AddPreJoining = String.Format("showModal('dvAddPreJoiningExp',false);");
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddPreJoining", AddPreJoining, true);
                    return;
                }
            }
            else
            {
                lblMsgPreJoining.Text = "Enter Period of Service From and Period of Service To Date";
                CurrentOperatorCheck();
                string AddPreJoining = String.Format("showModal('dvAddPreJoiningExp',false);");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddPreJoining", AddPreJoining, true);
                return;
            }
            int CrewID = GetCrewID();
            if (CrewID > 0)
            {
                foreach (GridViewRow row in GridView_PreJoiningExp.Rows)
                {
                    Label PreJoiningExpId = row.FindControl("lblPreJoiningExpId") as Label;
                    if (!PreJoiningExpId.Text.Equals(ViewState["PreJoiningId"]))
                    {
                        Label a = row.FindControl("lblDate_To") as Label;
                        DateTime? LastToDate = UDFLib.ConvertToDate(a.Text);
                        if (FromDate < LastToDate)
                        {
                            Label b = row.FindControl("lblDate_From") as Label;
                            DateTime? LastFromDate = UDFLib.ConvertToDate(b.Text);
                            if (FromDate >= LastFromDate)
                            {
                                lblMsgPreJoining.Text = "Two Different working experience cannot be on same date";
                                string AddPreJoining = String.Format("showModal('dvAddPreJoiningExp',false);");
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddPreJoining", AddPreJoining, true);
                                return;
                            }
                            if (ToDate >= LastFromDate)
                            {
                                lblMsgPreJoining.Text = "Two Different working experience cannot be on same date";
                                string AddPreJoining = String.Format("showModal('dvAddPreJoiningExp',false);");
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddPreJoining", AddPreJoining, true);
                                return;
                            }
                        }
                    }
                }
                if (txtCompanyName.Text.Trim() == "")
                {
                    lblMsgPreJoining.Text = "Company Name can not be blank";
                    CurrentOperatorCheck();
                    string AddPreJoining = String.Format("showModal('dvAddPreJoiningExp',false);");
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddPreJoining", AddPreJoining, true);

                }
                else if (txtPJVessel.Text.Trim() == "")
                {
                    lblMsgPreJoining.Text = "Vessel Name can not be blank";
                    CurrentOperatorCheck();
                    string AddPreJoining = String.Format("showModal('dvAddPreJoiningExp',false);");
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddPreJoining", AddPreJoining, true);

                }
                else if (txtPJFlag.Text.Trim() == "")
                {
                    lblMsgPreJoining.Text = "Flag can not be blank";
                    CurrentOperatorCheck();
                    string AddPreJoining = String.Format("showModal('dvAddPreJoiningExp',false);");
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddPreJoining", AddPreJoining, true);

                }
                else if (ToDate < FromDate)
                {
                    lblMsgPreJoining.Text = "To Date can not be less than From date";
                    CurrentOperatorCheck();
                    string AddPreJoining = String.Format("showModal('dvAddPreJoiningExp',false);");
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddPreJoining", AddPreJoining, true);

                }
                else
                {
                    if (txtGRT.Text.Trim() != "")
                    {
                        try
                        {
                            GRT = int.Parse(txtGRT.Text);
                        }
                        catch
                        {
                            lblMsgPreJoining.Text = "GRT value should be numeric";
                            CurrentOperatorCheck();
                            string AddPreJoining = String.Format("showModal('dvAddPreJoiningExp',false);");
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddPreJoining", AddPreJoining, true);
                            return;
                        }
                    }

                    if (txtMEBHP.Text.Trim() != "")
                    {
                        try
                        {
                            BHP = int.Parse(txtMEBHP.Text);
                        }
                        catch
                        {
                            lblMsgPreJoining.Text = "BHP value should be numeric";
                            CurrentOperatorCheck();
                            string AddPreJoining = String.Format("showModal('dvAddPreJoiningExp',false);");
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddPreJoining", AddPreJoining, true);
                            return;
                        }
                    }
                    int result;
                    string VesselType = ddlPJVesselType.SelectedValue == "0" ? "" : ddlPJVesselType.SelectedItem.ToString();
                    if (ViewState["PreJoiningId"] == null)
                    {
                        result = objBLLCrew.INS_CrewPreJoiningExp(CrewID, txtPJVessel.Text, txtPJFlag.Text, VesselType, txtPJDWT.Text, GRT, txtCompanyName.Text, UDFLib.ConvertToInteger(ddPJRank.SelectedValue), UDFLib.ConvertToDefaultDt(txtPJDateFrom.Text), UDFLib.ConvertToDefaultDt(txtPJDateTo.Text), 0, 0, int.Parse(Session["USERID"].ToString()), txtMEModel.Text, BHP, UDFLib.ConvertIntegerToNull(UDFLib.ConvertStringToNull(hdnVesselManager.Value)));
                        if (result == 1)
                            UDFLib.GetException("SuccessMessage/SaveMessage");
                    }
                    else
                    {
                        objBLLCrew.UPDATE_CrewPreJoiningExp(int.Parse(ViewState["PreJoiningId"].ToString()), txtPJVessel.Text, txtPJFlag.Text, VesselType, txtPJDWT.Text, txtGRT.Text.Trim(), txtCompanyName.Text, UDFLib.ConvertToInteger(ddPJRank.SelectedValue), UDFLib.ConvertToDefaultDt(txtPJDateFrom.Text), UDFLib.ConvertToDefaultDt(txtPJDateTo.Text), int.Parse(Session["USERID"].ToString()), txtMEModel.Text, BHP, UDFLib.ConvertIntegerToNull(UDFLib.ConvertStringToNull(hdnVesselManager.Value)));
                        result = 1;
                        UDFLib.GetException("SuccessMessage/UpdateMessage");
                    }

                    if (result == 1)
                    {
                        ClearPreJoiningDetails();
                        GridView_PreJoiningExp.DataBind();
                        string hidemodal = String.Format("hideModal('dvAddPreJoiningExp')");
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hidemodal", hidemodal, true);
                    }
                    else
                    {
                        UDFLib.GetException("FailureMessage/DataSaveMessage");
                        lblMsgPreJoining.Text = "Unable to add Pre-Joining details !!";
                        string AddPreJoining = String.Format("showModal('dvAddPreJoiningExp',false);");
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddPreJoining", AddPreJoining, true);
                    }

                }

            }
            else
            {
                lblMsgPreJoining.Text = "First save crew details !!";
                string AddPreJoining = String.Format("showModal('dvAddPreJoiningExp',false);");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddPreJoining", AddPreJoining, true);
            }
        }
        catch (Exception ex)
        {
            lblMsgPreJoining.Text = "Unable to add Pre-Joining details !! " + ex.Message;
            string AddPreJoining = String.Format("showModal('dvAddPreJoiningExp',false);");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddPreJoining", AddPreJoining, true);
        }
    }
    protected void ClearPreJoiningDetails()
    {
        txtPJVessel.Enabled = true;
        txtCompanyName.Enabled = true;
        ddlPJVesselType.Enabled = true;
        txtCompanyName.Text = "";
        txtPJVessel.Text = "";
        txtPJFlag.Text = "";
        txtPJDWT.Text = "";
        txtPJDateFrom.Text = "";
        txtPJDateTo.Text = "";
        txtGRT.Text = "";
        txtMEModel.Text = "";
        txtMEBHP.Text = "";
        lblMsgPreJoining.Text = "";
        ddlPJVesselType.SelectedIndex = 0;
        ddPJRank.SelectedIndex = 0;
        ddlVessel.SelectedValue = "0";
        chkCurrentOperator.Checked = false;
        ddlVessel.Attributes.Add("style", "display:none;");
        tdVessel.Attributes.Add("style", "display:none;");
        hdnVesselManager.Value = "";
        ViewState["PreJoiningId"] = null;
    }
    protected void btnClosePreJoining_Click(object sender, EventArgs e)
    {
        ClearPreJoiningDetails();
        string hidemodal = String.Format("hideModal('dvAddPreJoiningExp')");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hidemodal", hidemodal, true);
    }

    protected void Load_CrewPreviousContactDetails(int CrewID)
    {
        DataTable dt = objBLLCrew.Get_CrewPreviousContactDetails(CrewID);
        GridView_PreviousContacts.DataSource = dt;
        GridView_PreviousContacts.DataBind();

    }
    protected void lnkEditPreviousContacts_Click(object sender, EventArgs e)
    {
        int iNewCrewID = GetCrewID();
        Load_CrewPreviousContactDetails(iNewCrewID);
        Load_CrewPersonalDetails(iNewCrewID);
        string AddPreJoining = String.Format("showModal('dvEdpreviousContact',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddPreJoining", AddPreJoining, true);
    }
    protected bool checkEmail(string Value_)
    {
        Regex reg = new Regex(@"^([\w-]+(?:\.[\w-]+)*)@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z][A-Z]{2})?)$", RegexOptions.IgnoreCase);

        if (reg.IsMatch(Value_))
            return true;
        else
            return false;
    }
    protected void btnSavePreviousContacts_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsgEditPreContact.Text = "";
            int iNewCrewID = GetCrewID();
            if (iNewCrewID > 0)
            {
                lblMsg.Text = "";
                if (rdoMultiNationalCrew.SelectedValue == "1" && txtNationalities.Text == "")
                {
                    lblMsgEditPreContact.Text = "Enter State Nationalities";
                }
                if ((txt14a_email.Text != "" && checkEmail(txt14a_email.Text) == false) ||
                    (txt14b_email.Text != "" && checkEmail(txt14b_email.Text) == false) ||
                    (txt14c_email.Text != "" && checkEmail(txt14c_email.Text) == false) ||
                    (txt14d_email.Text != "" && checkEmail(txt14d_email.Text) == false))
                {
                    lblMsgEditPreContact.Text = "Enter valid E-mail";
                }
                objBLLCrew.UPDATE_MultinationalCrewInfo(iNewCrewID, UDFLib.ConvertToInteger(rdoMultiNationalCrew.SelectedValue), txtNationalities.Text, GetSessionUserID());

                if (txt14a_name.Text != "")
                {
                    int id = 0;
                    if (txt14a_ID.Text != "")
                    {
                        id = Convert.ToInt32(txt14a_ID.Text);
                    }
                    objBLLCrew.INS_CrewPreviousContacts(id, iNewCrewID, txt14a_name.Text, txt14a_pic.Text, txt14a_tel.Text, txt14a_fax.Text, txt14a_email.Text, GetSessionUserID());
                }
                if (txt14b_name.Text != "")
                {
                    int id = 0;
                    if (txt14b_ID.Text != "")
                    {
                        id = Convert.ToInt32(txt14b_ID.Text);
                    }
                    objBLLCrew.INS_CrewPreviousContacts(id, iNewCrewID, txt14b_name.Text, txt14b_pic.Text, txt14b_tel.Text, txt14b_fax.Text, txt14b_email.Text, GetSessionUserID());
                }
                if (txt14c_name.Text != "")
                {
                    int id = 0;
                    if (txt14c_ID.Text != "")
                    {
                        id = Convert.ToInt32(txt14c_ID.Text);
                    }
                    objBLLCrew.INS_CrewPreviousContacts(id, iNewCrewID, txt14c_name.Text, txt14c_pic.Text, txt14c_tel.Text, txt14c_fax.Text, txt14c_email.Text, GetSessionUserID());
                }
                if (txt14d_name.Text != "")
                {
                    int id = 0;
                    if (txt14d_ID.Text != "")
                    {
                        id = Convert.ToInt32(txt14d_ID.Text);
                    }
                    objBLLCrew.INS_CrewPreviousContacts(id, iNewCrewID, txt14d_name.Text, txt14d_pic.Text, txt14d_tel.Text, txt14d_fax.Text, txt14d_email.Text, GetSessionUserID());
                }
                if (lblMsgEditPreContact.Text == "")
                {
                    lblMultinationalcrew.Text = (rdoMultiNationalCrew.SelectedValue.ToString() == "1") ? "YES" : "NO";
                    lblNationalities.Text = txtNationalities.Text;
                    lblMsgEditPreContact.Text = "";
                    Load_CrewPreviousContactDetails(iNewCrewID);
                    ClearFields();
                    string hidemodal = String.Format("hideModal('dvEdpreviousContact')");
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hidemodal", hidemodal, true);
                }
                else
                {
                    string AddPreJoining = String.Format("showModal('dvEdpreviousContact',false);");
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddPreJoining", AddPreJoining, true);
                    return;
                }

            }
            else
            {
                lblMsgEditPreContact.Text = "First save crew details !!";
                string AddPreJoining = String.Format("showModal('dvEdpreviousContact',false);");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddPreJoining", AddPreJoining, true);
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    protected void ClearFields()
    {
        txt14a_name.Text = "";
        txt14a_pic.Text = "";
        txt14a_tel.Text = "";
        txt14a_fax.Text = "";
        txt14a_email.Text = "";
        txt14b_name.Text = "";
        txt14b_pic.Text = "";
        txt14b_tel.Text = "";
        txt14b_fax.Text = "";
        txt14b_email.Text = "";
        txt14c_name.Text = "";
        txt14c_pic.Text = "";
        txt14c_tel.Text = "";
        txt14c_fax.Text = "";
        txt14c_email.Text = "";
        txt14d_name.Text = "";
        txt14d_pic.Text = "";
        txt14d_tel.Text = "";
        txt14d_fax.Text = "";
        txt14d_email.Text = "";
        txtNationalities.Text = "";

        txt14a_ID.Text = "";
        txt14b_ID.Text = "";
        txt14c_ID.Text = "";
        txt14d_ID.Text = "";
    }
    protected void btnCancelPreviousContacts_Click(object sender, EventArgs e)
    {
        lblMsgEditPreContact.Text = "";
        ClearFields();
        string hidemodal = String.Format("hideModal('dvEdpreviousContact')");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hidemodal", hidemodal, true);

    }
    protected void GridView_PreviousContacts_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string name = DataBinder.Eval(e.Row.DataItem, "name").ToString();
            string pic = DataBinder.Eval(e.Row.DataItem, "PIC").ToString();
            string tele = DataBinder.Eval(e.Row.DataItem, "Telephone").ToString();
            string fax = DataBinder.Eval(e.Row.DataItem, "fax").ToString();
            string email = DataBinder.Eval(e.Row.DataItem, "Email").ToString();
            string remark = DataBinder.Eval(e.Row.DataItem, "Remark").ToString();
            string RefChk = DataBinder.Eval(e.Row.DataItem, "RefChkd").ToString();
            string id = DataBinder.Eval(e.Row.DataItem, "id").ToString();

            switch (e.Row.RowIndex)
            {
                case 0:
                    txt14a_name.Text = name;
                    txt14a_pic.Text = pic;
                    txt14a_tel.Text = tele;
                    txt14a_fax.Text = fax;
                    txt14a_email.Text = email;
                    txt14a_ID.Text = id;
                    break;
                case 1:
                    txt14b_name.Text = name;
                    txt14b_pic.Text = pic;
                    txt14b_tel.Text = tele;
                    txt14b_fax.Text = fax;
                    txt14b_email.Text = email;
                    txt14b_ID.Text = id;
                    break;
                case 2:
                    txt14c_name.Text = name;
                    txt14c_pic.Text = pic;
                    txt14c_tel.Text = tele;
                    txt14c_fax.Text = fax;
                    txt14c_email.Text = email;
                    txt14c_ID.Text = id;
                    break;
                case 3:
                    txt14d_name.Text = name;
                    txt14d_pic.Text = pic;
                    txt14d_tel.Text = tele;
                    txt14d_fax.Text = fax;
                    txt14d_email.Text = email;
                    txt14d_ID.Text = id;
                    break;
            }
        }
    }
    protected void Load_CrewPersonalDetails(int ID)
    {
        DataTable dt = objBLLCrew.Get_CrewPersonalDetailsByID(ID);
        if (dt.Rows.Count > 0)
        {
            txtNationalities.Text = dt.Rows[0]["MultinationalcrewNationalities"].ToString();

            if (dt.Rows[0]["Multinationalcrew"].ToString() == "1")
            {
                lblMultinationalcrew.Text = "YES";
                rdoMultiNationalCrew.SelectedIndex = 0;
            }
            else
            {
                lblMultinationalcrew.Text = "NO";
                rdoMultiNationalCrew.SelectedIndex = 1;
            }
            lblNationalities.Text = dt.Rows[0]["MultinationalcrewNationalities"].ToString();

        }

    }
    protected void ddlVessel_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlVessel.Attributes.Add("style", "display:block;");
            tdVessel.Attributes.Add("style", "display:block;");
            txtPJVessel.Enabled = false;
            ddlPJVesselType.Enabled = false;
            txtCompanyName.Enabled = false;

            if (ddlVessel.SelectedValue.ToString() == "0")
            {
                txtPJVessel.Text = string.Empty;
                ddlPJVesselType.SelectedValue = "0";
            }
            else
            {
                txtPJVessel.Text = ddlVessel.SelectedItem.Text;
                DataRow[] drVsl = (ViewState["DtVessel"] as DataTable).Select(" Vessel_ID='" + ddlVessel.SelectedValue.ToString() + "'");

                if (drVsl.Length > 0)
                {
                    txtCompanyName.Text = drVsl[0]["VesselManager"].ToString();
                    ddlPJVesselType.SelectedValue = drVsl[0]["VesselTypes"].ToString() == "" ? "0" : drVsl[0]["VesselTypes"].ToString();
                    hdnVesselManager.Value = drVsl[0]["Vessel_Manager"].ToString();
                }
            }
            string AddCompmodal = String.Format("showModal('dvAddPreJoiningExp',false);");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddCompmodal", AddCompmodal, true);
        }
        catch (Exception ex)
        {
        }
    }
    protected void CurrentOperatorCheck()
    {
        if (chkCurrentOperator.Checked == true)
        {
            ddlVessel.Attributes.Add("style", "display:block;");
            tdVessel.Attributes.Add("style", "display:block;");
            txtPJVessel.Enabled = false;
            txtCompanyName.Enabled = false;
            ddlPJVesselType.Enabled = false;
        }
        else
        {
            ddlVessel.Attributes.Add("style", "display:none;");
            tdVessel.Attributes.Add("style", "display:none;");
            ddlPJVesselType.Enabled = true;
            txtPJVessel.Enabled = true;
            txtCompanyName.Enabled = true;
        }
    }
    protected void chkCurrentOperator_CheckedChanged(object sender, EventArgs e)
    {
        if (chkCurrentOperator.Checked == true)
        {
            txtPJVessel.Text = "";
            txtCompanyName.Text = "";
            ddlVessel.Attributes.Add("style", "display:block;");
            tdVessel.Attributes.Add("style", "display:block;");
            ddlVessel.SelectedValue = "0";
            ddlPJVesselType.SelectedValue = "0";
            ddlPJVesselType.Enabled = false;
            txtPJVessel.Enabled = false;
            txtCompanyName.Enabled = false;
        }
        else
        {
            ddlVessel.Attributes.Add("style", "display:none;");
            tdVessel.Attributes.Add("style", "display:none;");
            ddlPJVesselType.Enabled = true;
            txtPJVessel.Enabled = true;
            txtCompanyName.Enabled = true;
            txtCompanyName.Text = "";
            hdnVesselManager.Value = "";
        }
        string AddCompmodal = String.Format("showModal('dvAddPreJoiningExp',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddCompmodal", AddCompmodal, true);
    }
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

    protected void Validate_Prejoining(object sender, ObjectDataSourceMethodEventArgs e)
    {
        try
        {
            DateTime? ToDate = Convert.ToDateTime(e.InputParameters["Date_To"].ToString());
            DateTime? FromDate = Convert.ToDateTime(e.InputParameters["Date_From"].ToString());
            foreach (GridViewRow row in GridView_PreJoiningExp.Rows)
            {
                if (!(row.RowState.ToString()).Contains("Edit"))
                {
                    Label a = row.FindControl("lblDate_To") as Label;
                    DateTime LastToDate = Convert.ToDateTime(a.Text);
                    if (FromDate < LastToDate)
                    {
                        Label b = row.FindControl("lblDate_From") as Label;
                        DateTime LastFromDate = Convert.ToDateTime(b.Text);
                        if (FromDate >= LastFromDate)
                        {
                            Label1.Text = "Two Different working experience cannot be on same date";
                            e.Cancel = true;

                            return;
                        }
                        if (ToDate >= LastFromDate)
                        {
                            Label1.Text = "Two Different working experience cannot be on same date";
                            e.Cancel = true;
                            return;
                        }
                    }
                }
            }
            if (ToDate < FromDate)
            {
                Label1.Text = "To Date can not be less than From date";
                e.Cancel = true;
                return;
            }
            Label1.Text = "";
        }
        catch (Exception ex)
        {
            Label1.Text = "Unable to update Pre-Joining details !! " + ex.Message;
            e.Cancel = true;
        }
    }
    /// <summary>
    /// Pop up will open in edit mode
    /// </summary>
    /// <param name="source"></param>
    /// <param name="e"></param>
    protected void EditPreJoining(object source, CommandEventArgs e)
    {
        try
        {
            DataTable dt = objBLLCrew.Get_PreJoiningExpCrewWise(Convert.ToInt32(e.CommandArgument.ToString()));
            ViewState["PreJoiningId"] = e.CommandArgument.ToString();
            if (dt.Rows.Count > 0)
            {
                lblMsgPreJoining.Text = "";
                hdnVesselManager.Value = Convert.ToString(Session["VesselManager"]);
                txtCompanyName.Text = dt.Rows[0]["CompanyName"].ToString();
                txtPJVessel.Text = dt.Rows[0]["Vessel_Name"].ToString();
                txtPJFlag.Text = dt.Rows[0]["Flag"].ToString();
                txtPJDWT.Text = dt.Rows[0]["DWT"].ToString();
                txtPJDateFrom.Text = UDFLib.ConvertUserDateFormat(Convert.ToString(dt.Rows[0]["Date_From"]));
                txtPJDateTo.Text = UDFLib.ConvertUserDateFormat(Convert.ToString(dt.Rows[0]["Date_To"]));
                txtGRT.Text = dt.Rows[0]["GRT"].ToString();
                txtMEModel.Text = dt.Rows[0]["ME_MakeModel"].ToString();
                txtMEBHP.Text = dt.Rows[0]["ME_BHP"].ToString();
                ddlPJVesselType.SelectedValue = dt.Rows[0]["Vessel_Type"].ToString() == "" ? "0" : dt.Rows[0]["Vessel_Type"].ToString();
                ddPJRank.SelectedValue = dt.Rows[0]["Rank"].ToString();
                ddlVessel.SelectedIndex = dt.Rows[0]["CompanyID"].ToString() == "0" ? 0 : ddlVessel.Items.IndexOf(ddlVessel.Items.FindByText(dt.Rows[0]["Vessel_Name"].ToString()));
                chkCurrentOperator.Checked = dt.Rows[0]["CompanyID"].ToString() == "0" ? false : true;
                if (chkCurrentOperator.Checked == true)
                {
                    ddlVessel.Attributes.Add("style", "display:block;");
                    tdVessel.Attributes.Add("style", "display:block;");
                    ddlPJVesselType.Enabled = false;
                    txtPJVessel.Enabled = false;
                    txtCompanyName.Enabled = false;
                }
                else
                {
                    ddlVessel.Attributes.Add("style", "display:none;");
                    tdVessel.Attributes.Add("style", "display:none;");
                    ddlPJVesselType.Enabled = true;
                    txtPJVessel.Enabled = true;
                    txtCompanyName.Enabled = true;
                }
            }
            string AddCompmodal = String.Format("showModal('dvAddPreJoiningExp',false);");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddCompmodal", AddCompmodal, true);
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
}