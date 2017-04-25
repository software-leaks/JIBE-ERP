using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Properties;
using SMS.Business.Crew;
using SMS.Business.Infrastructure;

/// <summary>
/// Crew Setting is configuration screen which will have access only to User with Admin access
/// </summary>
public partial class Infrastructure_Libraries_CrewInterviewSettings : System.Web.UI.Page
{
    BLL_Crew_Admin objCrewAdmin = new BLL_Crew_Admin();
    BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
    BLL_Infra_Rank objRank = new BLL_Infra_Rank();
    UserAccess objUA = new UserAccess();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (GetSessionUserID() == 0)
            Response.Redirect("~/account/login.aspx");
        else
            UserAccessValidation();

        if (!IsPostBack)
        {
            DataTable dt = objCrewAdmin.GetInterviewSettings();
            Load_RankList();
            if (dt != null && dt.Rows.Count > 0)
            {
                chkInterview_Mandatory.Checked = Convert.ToBoolean(dt.Rows[0]["Interview_Mandatory"]);
                chkRejectedCheck.Checked = Convert.ToBoolean(dt.Rows[0]["Check_Rejected_Interview"]);
            }
            DataTable dtRankList = objRank.Get_RankMandatoryList();
            gvRankList.DataSource = dtRankList;
            gvRankList.DataBind();

            DataTable dtWages = objCrewAdmin.GetWagesSettings();
            if (dtWages != null && dtWages.Rows.Count > 0)
            {
                chkNationality.Checked = Convert.ToBoolean(dtWages.Rows[0]["NationalityConsidered"]);
                chkRankScale.Checked = Convert.ToBoolean(dtWages.Rows[0]["RankScaleConsidered"]);
                chkVesselFlag.Checked = Convert.ToBoolean(dtWages.Rows[0]["VesselFlagConsidered"]);
            }


            DataTable dtMandatorySettings = objCrewAdmin.GetMandatorySettings();
            if (dtMandatorySettings != null && dtMandatorySettings.Rows.Count > 0)
            {
                chkNOK.Checked = Convert.ToBoolean(dtMandatorySettings.Rows[0]["Value"]);
                chkCrewPhoto.Checked = Convert.ToBoolean(dtMandatorySettings.Rows[1]["Value"]);
                chkBankAccDetails.Checked = Convert.ToBoolean(dtMandatorySettings.Rows[2]["Value"]);
                chkSeniority.Checked = Convert.ToBoolean(dtMandatorySettings.Rows[3]["Value"]);
                ddlRank.SelectedValue = dtMandatorySettings.Rows[4]["Value"].ToString();
                chkLeaveWithhold.Checked = Convert.ToBoolean(dtMandatorySettings.Rows[5]["Value"]);

                DataRow[] dr = dtMandatorySettings.Select("key_Name='EvaluationDigitalSignature'");
                if (dr.Length > 0)
                {
                    chkEvalSign.Checked = Convert.ToBoolean(dtMandatorySettings.Rows[6]["Value"]);
                }
            }

            DataTable dtDocument = objCrewAdmin.GetDocumentSettings();
            if (dtDocument != null && dtDocument.Rows.Count > 0)
            {
                if (Convert.ToBoolean(dtDocument.Rows[0]["VesselFlagConsidered"]) == true)
                    rdbConsider.SelectedValue = "VesselFlag";
                else
                    rdbConsider.SelectedValue = "Vessel";

                chkSTCWDeck.Checked = Convert.ToBoolean(dtDocument.Rows[0]["STCW_Deck_Considered"]);
                chkSTCWEngine.Checked = Convert.ToBoolean(dtDocument.Rows[0]["STCW_Engine_Considered"]);
            }

            DataTable dtSeniorityReset = objCrewAdmin.GetSeniorityResetSettings();
            if (dtSeniorityReset != null && dtSeniorityReset.Rows.Count > 0)
            {
                if (Convert.ToBoolean(dtSeniorityReset.Rows[0]["AutomaticResetConsidered"]) == true)
                {
                    chkSeniorityReset.Checked = true;
                    ddlSeniorityYear.Enabled = true;
                    ddlSeniorityYear.SelectedValue = dtSeniorityReset.Rows[0]["ResetYears"].ToString();
                }
                else
                {
                    chkSeniorityReset.Checked = false;
                    ddlSeniorityYear.Enabled = false;
                }
            }
            else
            {
                chkSeniorityReset.Checked = false;
                ddlSeniorityYear.Enabled = false;
            }
        }
    }
    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
            Response.Redirect("~/default.aspx?msgid=1");

        if (objUA.Admin == 0)
        {
            btnSave.Enabled = false;
            btnSaveWages.Enabled = false;
            btnSaveRank.Enabled = false;
            btnSaveDocument.Enabled = false;
        }
    }
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
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
    public void Load_RankList()
    {
        ddlRank.DataSource = objCrewAdmin.Get_RankList();
        ddlRank.DataBind();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        int responseid = objCrewAdmin.SaveInterviewSettings(chkInterview_Mandatory.Checked, chkRejectedCheck.Checked);
        if (responseid == 1)
        {
            string js = "alert('Interview Setting Updated Successfully');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "status", js, true);
        }
    }
    protected void btnSaveRank_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("RankId", typeof(string));
        dt.Columns.Add("Mandatory", typeof(string));

        int Mandatory;
        int RankId;
        foreach (GridViewRow dr in gvRankList.Rows)
        {
            RankId = int.Parse(((Label)dr.FindControl("lblRankId")).Text.ToString());
            Mandatory = ((CheckBox)dr.FindControl("chkSelected")).Checked == true ? 1 : 0;
            dt.Rows.Add(RankId, Mandatory);
        }
        int status = objRank.INS_RankMandatoryList(dt, Convert.ToInt32(Session["USERID"]));
        if (status > 0)
        {
            string js = "alert('Reference Setting Updated Successfully');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "status", js, true);
        }
    }



    protected void btnSaveWages_Click(object sender, EventArgs e)
    {
        int responseid = objCrewAdmin.SaveWagesSettings(chkNationality.Checked, chkRankScale.Checked, chkVesselFlag.Checked);
        if (responseid > 0)
        {
            string js = "alert('Wages Setting Updated Successfully');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "status", js, true);
        }
    }

    protected void btnSaveMandatorySettings_Click(object sender, EventArgs e)
    {
        string js = "";
        int responseid = objCrewAdmin.SaveMandatorySettings(chkNOK.Checked, chkCrewPhoto.Checked, chkBankAccDetails.Checked, chkSeniority.Checked, chkLeaveWithhold.Checked, int.Parse(ddlRank.SelectedValue), chkEvalSign.Checked);
        if (responseid > 0)
            js = "alert('Mandatory Setting Updated Successfully');";
        else if (responseid == -1)
            js = "alert('There is no entry for Leave Wages');";
        else if (responseid == -2)
            js = "alert('There is no entry for Leave Wages to be considered at MOC');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "status", js, true);
    }

    protected void btnSaveEvaluatedSignature_Click(object sender, EventArgs e)
    {
        string js = "";
        int responseid = objCrewAdmin.SaveMandatorySettings(chkNOK.Checked, chkCrewPhoto.Checked, chkBankAccDetails.Checked, chkSeniority.Checked, chkLeaveWithhold.Checked, int.Parse(ddlRank.SelectedValue), chkEvalSign.Checked);
        if (responseid > 0)
            js = "alert('Evaluation Setting Updated Successfully');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "status", js, true);
    }

    /// <summary>
    /// Document setting configues whether Vessel / Vessel flag should be considered which creating documents
    /// </summary>
    protected void btnSaveDocument_Click(object sender, EventArgs e)
    {
        bool VesselFlagConsidered = false;
        bool VesselConsidered = false;
        if (rdbConsider.SelectedValue.ToString() == "VesselFlag")
        {
            VesselFlagConsidered = true;
            VesselConsidered = false;
        }
        else
        {
            VesselFlagConsidered = false;
            VesselConsidered = true;
        }
        int responseid = objCrewAdmin.SaveDocumentSettings(VesselFlagConsidered, VesselConsidered, chkSTCWDeck.Checked, chkSTCWEngine.Checked);
        if (responseid == 1)
        {
            string js = "alert('Document Setting Updated Successfully');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "status", js, true);
        }
    }
    /// <summary>
    /// Reset Seniority configures whether Daemon should reset the crew seniority if not sailed for particular time period
    /// </summary>
    protected void btnSaveSeniorityResetYear_Click(object sender, EventArgs e)
    {
        int responseid = objCrewAdmin.SaveSeniorityResetSettings(chkSeniorityReset.Checked, int.Parse(ddlSeniorityYear.SelectedValue.ToString()), Convert.ToInt32(Session["USERID"]));
        if (responseid == 1)
        {
            string js = "alert('Seniority Reset Setting Updated Successfully');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "status", js, true);
        }
    }
    protected void chkSeniorityReset_CheckedChanged(object sender, EventArgs e)
    {
        ddlSeniorityYear.Enabled = chkSeniorityReset.Checked;
        if (chkSeniorityReset.Checked == false)
            ddlSeniorityYear.SelectedValue = "0";
        else
            ddlSeniorityYear.Enabled = true;
    }


}