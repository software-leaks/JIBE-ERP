using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Properties;
using SMS.Business.Crew;
using SMS.Business.Infrastructure;
using SMS.Business.Operations;

public partial class Operations_OPS_Rank_Config_Settings : System.Web.UI.Page
{
    BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
    UserAccess objUserAcess = new UserAccess();
    BLL_Crew_Admin objCrewAdmin = new BLL_Crew_Admin();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindDropdownCrewRank();
            SetDropdownValue();
        }
    }
    #region General Methods.
    protected void UserAccessValidation()
    {

        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        objUserAcess = objUser.Get_UserAccessForPage(Convert.ToInt32(Session["USERID"]), PageURL);

        if (objUserAcess.View == 0)
        {
            Response.Redirect("~/default.aspx?msgid=1");
        }
        if (objUserAcess.Edit == 0)
        {
            //btnSave.Enabled = false;
            btnAddRank.Enabled = false;
        }
    }
    
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }
    #endregion

    #region Operation Crew Settings
    /// <summary>
    /// Bind Dropdownlist data.
    /// </summary>

    private void BindDropdownCrewRank()
    {
        DataTable dtRanks = objCrewAdmin.Get_RankList();
        if (dtRanks.Rows.Count > 0)
        {
            ddlMasterRank.DataSource = dtRanks;
            ddlMasterRank.DataTextField = "Rank_Short_Name";
            ddlMasterRank.DataValueField = "id";
            ddlMasterRank.DataBind();
            ddlMasterRank.Items.Insert(0, "-Select-");

            ddlCERank.DataSource = dtRanks;
            ddlCERank.DataTextField = "Rank_Short_Name";
            ddlCERank.DataValueField = "id";
            ddlCERank.DataBind();
            ddlCERank.Items.Insert(0, "-Select-");
        }
        ddlMasterRank.SelectedIndex = 0;
        ddlCERank.SelectedIndex = 0;
    }
    /// <summary>
    /// Save button event.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAddRank_Click(object sender, EventArgs e)
    {
        BLL_OPS_Admin objOpsAdmin = new BLL_OPS_Admin();
        try
        {
            if (ddlMasterRank.SelectedValue != "-Select-" && ddlCERank.SelectedValue != "-Select-") 
            {
                int MasterRankID = Convert.ToInt32(ddlMasterRank.SelectedValue == "-Select-" ? "0" : ddlMasterRank.SelectedValue);
                int CERankID = Convert.ToInt32(ddlCERank.SelectedValue == "-Select-" ? "0" : ddlCERank.SelectedValue);
                int responseid = objOpsAdmin.INS_Rank_Voyage_Report(MasterRankID, CERankID,"A", GetSessionUserID());
                string js = "alert('Data Added Successfully..');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "status", js, true);
                ClearControls();
                SetDropdownValue();
            }
            else
            {
                if (ddlMasterRank.SelectedValue == "-Select-")
                {
                    string js = "alert('Select Master Rank');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "status", js, true);
                    ddlMasterRank.Focus();
                }
                if (ddlCERank.SelectedValue == "-Select-")
                {
                    string js = "alert('Select Chief Engineer Rank');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "status", js, true);
                    ddlCERank.Focus();
                }
            }
        }
        catch(Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    /// <summary>
    /// To set Saved value to dropdownlist
    /// </summary>
    private void SetDropdownValue()
    {
        BLL_OPS_Admin objOpsAdmin = new BLL_OPS_Admin();
        try
        {
            DataTable dt = objOpsAdmin.Get_Rank_Voyage_Config();
            if (dt.Rows.Count > 0)
            {
                ddlMasterRank.SelectedValue = Convert.ToString(dt.Rows[0]["Mst_RankID"]);
                ddlCERank.SelectedValue = Convert.ToString(dt.Rows[0]["CE_RankID"]);
            }
            else
            {
                ddlMasterRank.SelectedIndex = 0;
                ddlCERank.SelectedIndex = 0;
            }
        }
        catch(Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }

    }
    /// <summary>
    /// To clear conrols after saving.
    /// </summary>
    private void ClearControls()
    {
        ddlMasterRank.SelectedIndex = 0;
        ddlCERank.SelectedIndex = 0;
    }
    #endregion
   
}