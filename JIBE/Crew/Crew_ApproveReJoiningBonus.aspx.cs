using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Crew;
using SMS.Business.Infrastructure;
using SMS.Properties;
using System.Data;

public partial class Crew_Crew_ApproveReJoiningBonus : System.Web.UI.Page
{
    IFormatProvider iFormatProvider = new System.Globalization.CultureInfo("en-GB", true);
    UserAccess objUA = new UserAccess();
    BLL_Crew_CrewDetails objCrew = new BLL_Crew_CrewDetails();
    BLL_Infra_VesselLib objVessel = new BLL_Infra_VesselLib();
    public string TodayDateFormat = "";
    public string DFormat = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        calFrom.Format = Convert.ToString(Session["User_DateFormat"]);
        DFormat = Convert.ToString(Session["User_DateFormat"]);

        TodayDateFormat = UDFLib.DateFormatMessage();

        CalendarExtender1.Format = Convert.ToString(Session["User_DateFormat"]);
        CalendarExtender2.Format = Convert.ToString(Session["User_DateFormat"]);
        CalendarExtender3.Format = Convert.ToString(Session["User_DateFormat"]);

        objAdmin.Value = "0";
        objEdit.Value = "0";
        UserAccessValidation();
        if (!IsPostBack)
        {
            ViewState["SORTDIRECTION"] = null;
            ViewState["SORTBYCOLOUMN"] = null;

            ucCustomPagerItems.PageSize = 20;

            Load_RankList();
            Load_VesselList();
            BindRJBGrid();
        }

    }
    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
            Response.Redirect("~/default.aspx?msgid=1");

        if (objUA.Edit == 1)
        {
            objEdit.Value = "1";
        }
        if (objUA.Approve == 1)
        {
            objAdmin.Value = "1";
        }
    }
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }
    private int GetSessionUserCompanyID()
    {
        if (Session["USERCOMPANYID"] != null)
            return int.Parse(Session["USERCOMPANYID"].ToString());
        else
            return 0;
    }
    protected void Load_RankList()
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
    public void Load_VesselList()
    {
        int UserCompanyID = GetSessionUserCompanyID();

        ddlCurrentVessel.DataSource = objVessel.Get_VesselList(0, 0, 0, "", UserCompanyID);

        ddlCurrentVessel.DataTextField = "VESSEL_NAME";
        ddlCurrentVessel.DataValueField = "VESSEL_ID";
        ddlCurrentVessel.DataBind();
        ddlCurrentVessel.Items.Insert(0, new ListItem("-SELECT-", "0"));
        ddlCurrentVessel.SelectedIndex = 0;
    }
    protected void BindRJBGrid()
    {
        DateTime dtFrom = DateTime.Parse("1900/01/01");
        DateTime dtTo = DateTime.Parse("2900/01/01");

        if (txtSearchJoinFromDate.Text != "")
            dtFrom = UDFLib.ConvertToDate(txtSearchJoinFromDate.Text.ToString(), UDFLib.GetDateFormat());

        if (txtSearchJoinToDate.Text != "")
            dtTo = UDFLib.ConvertToDate(txtSearchJoinToDate.Text.ToString(), UDFLib.GetDateFormat());

        DateTime dtExpectFrom = DateTime.Parse("1900/01/01");
        DateTime dtExpectTo = DateTime.Parse("2900/01/01");

        if (txtExpectedFromDate.Text != "")
            dtExpectFrom = UDFLib.ConvertToDate(txtExpectedFromDate.Text.ToString(), UDFLib.GetDateFormat());

        if (txtExpectedToDate.Text != "")
            dtExpectTo = UDFLib.ConvertToDate(txtExpectedToDate.Text.ToString(), UDFLib.GetDateFormat());

        int PAGE_SIZE = ucCustomPagerItems.PageSize;
        int PAGE_INDEX = ucCustomPagerItems.CurrentPageIndex;

        int SelectRecordCount = ucCustomPagerItems.isCountRecord;

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

        DataTable dt = objCrew.Get_RJBList_Index(UDFLib.ConvertIntegerToNull(ddlRank.SelectedValue), UDFLib.ConvertIntegerToNull(ddlStaus.SelectedValue), dtFrom.ToString("yyyy/MM/dd"), dtTo.ToString("yyyy/MM/dd"),
                                                 UDFLib.ConvertIntegerToNull(ddlCurrentVessel.SelectedValue), dtExpectFrom.ToString("yyyy/MM/dd"), dtExpectTo.ToString("yyyy/MM/dd"), txtSearchText.Text.Trim(),
                                                 GetSessionUserID(), PAGE_SIZE, PAGE_INDEX, ref SelectRecordCount, sortbycoloumn, sortdirection);


        GridView1.DataSource = dt;
        GridView1.DataBind();

        if (ucCustomPagerItems.isCountRecord == 1)
        {
            ucCustomPagerItems.CountTotalRec = SelectRecordCount.ToString();
            ucCustomPagerItems.BuildPager();
        }
    }
    protected void GridView1_Sorting(object sender, GridViewSortEventArgs se)
    {
        ViewState["SORTBYCOLOUMN"] = se.SortExpression;

        if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
            ViewState["SORTDIRECTION"] = 1;
        else
            ViewState["SORTDIRECTION"] = 0;

        BindRJBGrid();
    }
    protected void onUpdate(object source, CommandEventArgs e)
    {
        hdnID.Value = e.CommandArgument.ToString().Split(',')[0];
        hdnCrewID.Value = e.CommandArgument.ToString().Split(',')[1];
        ReloadRJBDetail(Convert.ToInt32(hdnID.Value));

        string AlertMsg = String.Format("showModal('dvExport',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AlertMsg", AlertMsg, true);
    }
    protected void btnStatus_OnClick(object sender, EventArgs e)
    {
        string hidemodal = "";
        if ((lblEntitled.Text.Trim() == "NO" || UDFLib.ConvertDecimalToNull(txtActAmount.Text.Trim()) != UDFLib.ConvertDecimalToNull(lblRJBAmount.Text.Trim())) && txtRemark.Text.Trim() == "")
        {
            hidemodal = String.Format("alert('If the Crew is not Entitled for Rejoining Bonus or Actual amount is different from Calculated then the Remark is mandatory!');showModal('dvExport',false);");
        }
        else
        {
            int Reslt = objCrew.Update_RJBApproval(Convert.ToInt32(hdnStatus.Value), Convert.ToInt32(hdnID.Value), GetSessionUserID(), UDFLib.ConvertDecimalToNull(txtActAmount.Text), txtRemark.Text.Trim(), hdnStatus.Value == "1" ? "APPROVE" : "REJECT");
            //BindRJBGrid();
            hidemodal = String.Format("hideModal('dvExport');$('#BtnSearch').click();");
        }
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hidemodal", hidemodal, true);
    }
    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        BindRJBGrid();
    }
    protected void btnClearFilter_Click(object sender, EventArgs e)
    {
        ddlRank.SelectedValue = "0";
        ddlStaus.SelectedValue = "0";
        txtSearchJoinFromDate.Text = "";
        txtSearchJoinToDate.Text = "";
        txtExpectedFromDate.Text = "";
        txtExpectedToDate.Text = "";
        txtSearchText.Text = "";
        ddlCurrentVessel.SelectedValue = "0";
        BindRJBGrid();
    }
    //protected void btnSave_Click(object sender, EventArgs e)
    //{
    //    int Reslt = objCrew.Update_RJBAmount(Convert.ToDecimal(txtActAmount.Text), Convert.ToInt32(hdnID.Value), GetSessionUserID());
    //    BindRJBGrid();

    //    string AlertMsg = String.Format("showModal('dvExport',false);");
    //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AlertMsg", AlertMsg, true);
    //}
    protected void btnSave_Click(object sender, EventArgs e)
    {
        int Reslt = objCrew.Update_RJBApproval(0, Convert.ToInt32(hdnID.Value), GetSessionUserID(), UDFLib.ConvertDecimalToNull(txtActAmount.Text), txtRemark.Text.Trim(), "SAVE");
        BindRJBGrid();
        ReloadRJBDetail(Convert.ToInt32(hdnID.Value));
        string AlertMsg = String.Format("showModal('dvExport',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AlertMsg", AlertMsg, true);
    }
    protected void btnRework_Click(object sender, EventArgs e)
    {
        int Reslt = objCrew.Update_RJBApproval(0, Convert.ToInt32(hdnID.Value), GetSessionUserID(), UDFLib.ConvertDecimalToNull(txtActAmount.Text), "", "REWORK");
        BindRJBGrid();
        ReloadRJBDetail(Convert.ToInt32(hdnID.Value));
        txtRemark.Text = "";
        string AlertMsg = String.Format("showModal('dvExport',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AlertMsg", AlertMsg, true);
    }
    protected void ReloadRJBDetail(int ID)
    {
        lblmsg.Text = "";
        DataTable dt = new DataTable();
        dt = objCrew.Get_RJBList_Index(ID);

        txtRemark.Text = "";
        if (dt.Rows.Count > 0)
        {
            lblName.Text = dt.Rows[0]["Crew_Name"].ToString();
            lblRank.Text = dt.Rows[0]["Rank_Name"].ToString();
            lblVessel.Text = dt.Rows[0]["Vessel_Name"].ToString();
            lblSignOffDate.Text = UDFLib.ConvertUserDateFormat(dt.Rows[0]["Sign_Off_Date"].ToString(), DFormat);
            lblReadinessDate.Text = UDFLib.ConvertUserDateFormat(dt.Rows[0]["Readiness_Date"].ToString(), DFormat);
            lblLastContractDuration.Text = dt.Rows[0]["LastContractDuration"].ToString();
            lblVacationDuration.Text = dt.Rows[0]["VacationDuration"].ToString();
            lblRJBAmount.Text = dt.Rows[0]["CalculatedRJBAmount"].ToString();
            lblEntitled.Text = dt.Rows[0]["IsEntitled"].ToString();
            lblApproved.Text = dt.Rows[0]["IsApproved"].ToString();
            txtActAmount.Text = dt.Rows[0]["SavedRJBAmount"].ToString();
            txtRemark.Text = dt.Rows[0]["Remark"].ToString();
            if (objCrew.CheckCrewStatus(Convert.ToInt32(hdnCrewID.Value)) == 0)
            {
                if (dt.Rows[0]["IsApproved"].ToString() == "PENDING")
                {
                    if (objEdit.Value == "1")
                        btnSave.Visible = true;
                    else
                        btnSave.Visible = false;

                    if (objAdmin.Value == "1")
                    {
                        btnApprove.Visible = true;
                        btnReject.Visible = true;
                    }
                    else
                    {
                        btnApprove.Visible = false;
                        btnReject.Visible = false;
                    }
                    btnRework.Visible = false;
                    txtActAmount.Enabled = true;
                    //chkAmt.Enabled = true;
                    lnkResetAmount.Visible = true;
                    txtRemark.Enabled = true;
                }
                else
                {
                    btnSave.Visible = false;
                    btnApprove.Visible = false;
                    btnReject.Visible = false;
                    if (objAdmin.Value == "1")
                        btnRework.Visible = true;
                    else
                        btnRework.Visible = false;
                    txtActAmount.Enabled = false;
                    lnkResetAmount.Visible = false;
                    // chkAmt.Enabled = false;
                    txtRemark.Enabled = false;
                }
            }
            else
            {
                btnSave.Visible = false;
                btnApprove.Visible = false;
                btnReject.Visible = false;
                btnRework.Visible = false;
                txtActAmount.Enabled = false;
                lnkResetAmount.Visible = false;
                //chkAmt.Enabled = false;
                txtRemark.Enabled = false;
                lblmsg.Text = "This Crew has already signed the Contract!";
            }

        }
    }
    protected void chkAmt_CheckedChanged(object sender, EventArgs e)
    {
        //if (chkAmt.Checked == true)
        txtActAmount.Text = lblRJBAmount.Text;
        //chkAmt.Checked = false;
        string AlertMsg = String.Format("showModal('dvExport',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AlertMsg", AlertMsg, true);
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblIsEntitled = (Label)e.Row.FindControl("lblIsEntitled");
            if (lblIsEntitled.Text == "YES")
                lblIsEntitled.ForeColor = System.Drawing.Color.Green;
            else
                lblIsEntitled.ForeColor = System.Drawing.Color.Blue;
        }
    }
}