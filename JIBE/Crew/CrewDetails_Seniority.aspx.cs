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
using SMS.Business.PortageBill;

public partial class Crew_CrewDetails_Seniority : System.Web.UI.Page
{
    BLL_Crew_CrewDetails objBLLCrew = new BLL_Crew_CrewDetails();
    BLL_Crew_Seniority objBLLCrewSeniority = new BLL_Crew_Seniority();
    BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
    UserAccess objUA = new UserAccess();
    int CrewID = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        CalendarExtender2.Format = CalendarExtender1.Format = UDFLib.GetDateFormat();

        CrewID = UDFLib.ConvertToInteger(Request.QueryString["CrewID"]);
        if (!IsPostBack)
        {
            if (GetSessionUserID() == 0)
            {
                lblMsg.Text = "Session Expired!! Log-out and log-in again.";
            }
            else
            {
                UserAccessValidation();
                Load_RankList();
                txtCompanySeniorityDays.Enabled = false;
                txtCompanySeniorityYear.Enabled = false;
                txtRankSeniorityDays.Enabled = false;
                txtRankSeniorityYear.Enabled = false;
                ddlRank.Enabled = false;
                trCompanySeniorityRemarks.Visible = false;
                trRankSeniorityRemarks.Visible = false;
                trCompanyEffectiveDate.Visible = false;
                trRankEffectiveDate.Visible = false;
                DataTable dt = objBLLCrewSeniority.GET_CrewRewardedStatus(CrewID);
                if (dt.Rows.Count > 0)
                {
                    lnkCompanySeniorityReward.Text = "Rewarded Company Seniority For " + dt.Rows[0]["SeniorityYear"].ToString() + " Years";
                    lnkCompanySeniorityReward.NavigateUrl = "../crew/CrewCompanySeniorityReward.aspx?Staff_Code=" + dt.Rows[0]["Staff_Code"].ToString() + "&SeniorityYear=" + dt.Rows[0]["SeniorityYear"].ToString();
                }
                else
                {
                    lnkCompanySeniorityReward.Text = "";
                }
                DataSet ds = objBLLCrew.Get_CrewSeniorityDetails(CrewID);

                if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    ddlRank.SelectedValue = ds.Tables[0].Rows[0]["RankID"].ToString();
                    txtRankSeniorityDays.Text = ds.Tables[0].Rows[0]["SeniorityDays"].ToString();
                    txtRankSeniorityYear.Text = ds.Tables[0].Rows[0]["SeniorityYear"].ToString();
                }
                else
                {
                    txtRankSeniorityDays.Text = "";
                    txtRankSeniorityYear.Text = "";
                }
                if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
                {
                    txtCompanySeniorityDays.Text = ds.Tables[1].Rows[0]["SeniorityDays"].ToString();
                    txtCompanySeniorityYear.Text = ds.Tables[1].Rows[0]["SeniorityYear"].ToString();

                }
                else
                {
                    txtCompanySeniorityDays.Text = "";
                    txtCompanySeniorityYear.Text = "";
                }

                gvRankSeniority.DataSource = ds.Tables[2];
                gvRankSeniority.DataBind();

                gvCompanySeniority.DataSource = ds.Tables[3];
                gvCompanySeniority.DataBind();
            }
        }
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

        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
        {
            lblMsg.Text = "You don't have sufficient privilege to access the requested information.";
            pnlCompanySeniority.Visible = false;
            pnlRankSeniority.Visible = false;
        }
        if (objUA.Add == 0)
        {
            btnEditCompanySeniority.Enabled = false;
            btnEditRankSeniority.Enabled = false;
            btnReverseCompanySeniority.Enabled = false;
            btnReverseRankSeniority.Enabled = false;
        }
        if (objUA.Edit == 0)
        {
            btnEditCompanySeniority.Enabled = false;
            btnEditRankSeniority.Enabled = false;
            btnReverseCompanySeniority.Enabled = false;
            btnReverseRankSeniority.Enabled = false;
        }
        if (objUA.Delete == 0)
        {

        }
        if (objUA.Admin == 0)
        {
            btnEditCompanySeniority.Enabled = false;
            btnEditRankSeniority.Enabled = false;
            btnReverseCompanySeniority.Enabled = false;
            btnReverseRankSeniority.Enabled = false;
        }
        else
        {
            btnEditCompanySeniority.Enabled = true;
            btnEditRankSeniority.Enabled = true;
            btnReverseCompanySeniority.Enabled = true;
            btnReverseRankSeniority.Enabled = true;
        }
    }
    public void Load_RankList()
    {
        BLL_Crew_Admin objCrewAdmin = new BLL_Crew_Admin();
        DataTable dt = objCrewAdmin.Get_RankList();

        ddlRank.DataSource = dt;
        ddlRank.DataTextField = "Rank_Short_Name";
        ddlRank.DataValueField = "ID";
        ddlRank.DataBind();
        ddlRank.Items.Insert(0, new ListItem("-SELECT-", "0"));
        ddlRank.SelectedIndex = 0;
    }
    protected void btnEditCompanySeniority_Click(object sender, EventArgs e)
    {
        txtCompanySeniorityDays.Enabled = true;
        txtCompanySeniorityYear.Enabled = true;
        txtCompanySeniorityRemarks.Text = "";
        trCompanySeniorityRemarks.Visible = true;
        trCompanyEffectiveDate.Visible = true;
        btnEditCompanySeniority.Visible = false;
        btnReverseCompanySeniority.Visible = false;
    }
    protected void btnReverseCompanySeniority_Click(object sender, EventArgs e)
    {
        int CompanySeniorityYears = 0;
        int CompanySeniorityDays = 0;

        DataSet ds = objBLLCrew.Get_CrewSeniorityForReversing(CrewID);

        if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
        {
            CompanySeniorityDays = string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["SeniorityDays"])) ? 0 : Convert.ToInt32(ds.Tables[0].Rows[0]["SeniorityDays"]);
            CompanySeniorityYears = string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["SeniorityYear"])) ? 0 : Convert.ToInt32(ds.Tables[0].Rows[0]["SeniorityYear"]);
        }
        txtCompanySeniorityRemarks.Text = "";
        txtCompanySeniorityDays.Text = CompanySeniorityDays.ToString();
        txtCompanySeniorityYear.Text = CompanySeniorityYears.ToString();
        trCompanySeniorityRemarks.Visible = true;
        trCompanyEffectiveDate.Visible = true;
        btnEditCompanySeniority.Visible = false;
        btnReverseCompanySeniority.Visible = false;
    }
    protected void btnEditRankSeniority_Click(object sender, EventArgs e)
    {
        DataSet ds = objBLLCrew.Get_CrewSeniorityDetails(CrewID);
        if (ds.Tables[2] != null && ds.Tables[2].Rows.Count > 0)
            ddlRank.Enabled = false;
        else
            ddlRank.Enabled = true;
        txtRankSeniorityDays.Enabled = true;
        txtRankSeniorityYear.Enabled = true;
        txtRankSeniorityRemarks.Text = "";
        trRankSeniorityRemarks.Visible = true;
        trRankEffectiveDate.Visible = true;
        btnEditRankSeniority.Visible = false;
        btnReverseRankSeniority.Visible = false;
    }
    protected void btnReverseRankSeniority_Click(object sender, EventArgs e)
    {
        int RankSeniorityYears = 0;
        int RankSeniorityDays = 0;

        DataSet ds = objBLLCrew.Get_CrewSeniorityForReversing(CrewID);

        if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
        {
            RankSeniorityDays = string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["SeniorityDays"])) ? 0 : Convert.ToInt32(ds.Tables[0].Rows[0]["SeniorityDays"]);
            RankSeniorityYears = string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["SeniorityYear"])) ? 0 : Convert.ToInt32(ds.Tables[0].Rows[0]["SeniorityYear"]);
        }

        txtRankSeniorityDays.Text = RankSeniorityDays.ToString();
        txtRankSeniorityYear.Text = RankSeniorityYears.ToString();

        trRankSeniorityRemarks.Visible = true;
        trRankEffectiveDate.Visible = true;
        txtRankSeniorityRemarks.Text = "";
        btnEditRankSeniority.Visible = false;
        btnReverseRankSeniority.Visible = false;
    }
    protected void btnSaveCompanySeniority_Click(object sender, EventArgs e)
    {
        if (txtCompanyEffectiveDate.Text != "")
        {
            if (!UDFLib.DateCheck(txtCompanyEffectiveDate.Text))
            {
                string js = "alert('Enter valid Effective Date" + UDFLib.DateFormatMessage() + "');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", js, true);
                return;
            }
        }

        if (txtCompanySeniorityDays.Text.Trim() == "" && txtCompanySeniorityYear.Text.Trim() == "")
        {
            string js = "alert('Enter Company Seniority Years/Days');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", js, true);
        }
        else
        {
            int CompanySeniorityDays = 0, CompanySeniorityYears = 0;
            if (txtCompanySeniorityDays.Text.Trim() != "")
                CompanySeniorityDays = int.Parse(txtCompanySeniorityDays.Text.Trim());
            if (txtCompanySeniorityYear.Text.Trim() != "")
                CompanySeniorityYears = int.Parse(txtCompanySeniorityYear.Text.Trim());

            if (CompanySeniorityDays > 364)
            {
                string js = "alert('Seniority Days cannot be greater than 364');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", js, true);
            }
            else
            {
                string CompanyEffectiveDate = txtCompanyEffectiveDate.Text.Trim();
                if (CompanyEffectiveDate.Length > 0)
                {
                    try
                    {
                        DateTime Dt_CompanyEffectiveDate = UDFLib.ConvertToDate(CompanyEffectiveDate, UDFLib.GetDateFormat());
                        BLL_PortageBill.Update_CrewCompanySeniority(CrewID, CompanySeniorityYears, CompanySeniorityDays, txtCompanySeniorityRemarks.Text.Trim(), Dt_CompanyEffectiveDate, Convert.ToInt32(Session["userid"]));

                        txtCompanySeniorityDays.Enabled = false;
                        txtCompanySeniorityYear.Enabled = false;
                        trCompanyEffectiveDate.Visible = false;
                        trCompanySeniorityRemarks.Visible = false;

                        DataSet ds = objBLLCrew.Get_CrewSeniorityDetails(CrewID);

                        gvCompanySeniority.DataSource = ds.Tables[3];
                        gvCompanySeniority.DataBind();
                        btnEditCompanySeniority.Visible = true;
                        btnReverseCompanySeniority.Visible = true;
                    }
                    catch
                    {
                        string js = "alert('Invalid entry in Effective Date.');";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", js, true);
                    }
                }

            }
        }
    }
    protected void btnSaveRankSeniority_Click(object sender, EventArgs e)
    {
        if (txtRankEffectiveDate.Text != "")
        {
            if (!UDFLib.DateCheck(txtRankEffectiveDate.Text))
            {
                string js = "alert('Enter valid Effective Date" + UDFLib.DateFormatMessage() + "');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", js, true);
                return;
            }
        }
        if (txtRankSeniorityDays.Text.Trim() == "" && txtRankSeniorityYear.Text.Trim() == "")
        {
            string js = "alert('Enter Rank Seniority Years/Days');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", js, true);
        }
        else
        {
            int RankSeniorityDays = 0, RankSeniorityYears = 0;
            if (txtRankSeniorityDays.Text.Trim() != "")
                RankSeniorityDays = int.Parse(txtRankSeniorityDays.Text.Trim());
            if (txtRankSeniorityYear.Text.Trim() != "")
                RankSeniorityYears = int.Parse(txtRankSeniorityYear.Text.Trim());

            if (RankSeniorityDays > 364)
            {
                string js = "alert('Seniority Days cannot be greater than 364');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", js, true);
            }
            else
            {
                int RankId = int.Parse(ddlRank.SelectedValue.ToString());

                string RankEffectiveDate = txtRankEffectiveDate.Text.Trim();
                if (RankEffectiveDate.Length > 0)
                {
                    try
                    {
                        DateTime Dt_RankEffectiveDate = UDFLib.ConvertToDate(RankEffectiveDate, UDFLib.GetDateFormat());
                        BLL_PortageBill.Update_CrewRankSeniority(CrewID, RankId, RankSeniorityYears, RankSeniorityDays, 0, txtRankSeniorityRemarks.Text.Trim(), Dt_RankEffectiveDate, Convert.ToInt32(Session["userid"]));
                        ddlRank.Enabled = false;
                        txtRankSeniorityDays.Enabled = false;
                        txtRankSeniorityYear.Enabled = false;
                        trRankEffectiveDate.Visible = false;
                        trRankSeniorityRemarks.Visible = false;

                        DataSet ds = objBLLCrew.Get_CrewSeniorityDetails(CrewID);
                        gvRankSeniority.DataSource = ds.Tables[2];
                        gvRankSeniority.DataBind();
                        btnEditRankSeniority.Visible = true;
                        btnReverseRankSeniority.Visible = true;
                    }
                    catch
                    {
                        string js = "alert('Invalid entry in Effective Date.');";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", js, true);
                    }
                }
            }
        }
    }
    protected void btnCancelCompanySeniority_Click(object sender, EventArgs e)
    {
        txtCompanySeniorityDays.Enabled = false;
        txtCompanySeniorityYear.Enabled = false;
        btnEditCompanySeniority.Visible = true;
        btnReverseCompanySeniority.Visible = true;
        trCompanySeniorityRemarks.Visible = false;
        trCompanyEffectiveDate.Visible = false;
        DataSet ds = objBLLCrew.Get_CrewSeniorityDetails(CrewID);

        if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
        {
            txtCompanySeniorityDays.Text = ds.Tables[1].Rows[0]["SeniorityDays"].ToString();
            txtCompanySeniorityYear.Text = ds.Tables[1].Rows[0]["SeniorityYear"].ToString();
        }
    }
    protected void btnCancelRankSeniority_Click(object sender, EventArgs e)
    {
        ddlRank.Enabled = false;
        txtRankSeniorityDays.Enabled = false;
        txtRankSeniorityYear.Enabled = false;
        btnEditRankSeniority.Visible = true;
        btnReverseRankSeniority.Visible = true;
        trRankSeniorityRemarks.Visible = false;
        trRankEffectiveDate.Visible = false;
        DataSet ds = objBLLCrew.Get_CrewSeniorityDetails(CrewID);

        if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
        {
            txtRankSeniorityDays.Text = ds.Tables[0].Rows[0]["SeniorityDays"].ToString();
            txtRankSeniorityYear.Text = ds.Tables[0].Rows[0]["SeniorityYear"].ToString();
        }
    }
}