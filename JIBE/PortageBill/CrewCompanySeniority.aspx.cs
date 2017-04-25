using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.PortageBill;
using System.IO;
using System.Data;
using SMS.Business.Infrastructure;
using SMS.Business.Crew;

public partial class PortageBill_CrewCompanySeniority : System.Web.UI.Page
{
    MergeGridviewHeader_Info objContractList = new MergeGridviewHeader_Info();
    protected void Page_Load(object sender, EventArgs e)
    {
        
        UserAccessValidation();
        objContractList.AddMergedColumns(new int[] { 4, 5, 6 }, "Company Seniority", "HeaderStyle-css HeaderStyle-css-center");
        objContractList.AddMergedColumns(new int[] { 7, 8, 9, 10 }, "Rank Seniority", "HeaderStyle-css HeaderStyle-css-center");

        if (!IsPostBack)
        {
            ucCustomPager.PageSize = 20;

            Load_RankList();
            Load_SeniorityRecords();
        }
        //string msg1 = String.Format("$('.sailingInfo').SailingInfo();");
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgshowdetails", msg1, true);
    }
    public void Load_RankList()
    {
        BLL_Crew_Admin objCrewAdmin = new BLL_Crew_Admin();
        ddlRank.DataSource = objCrewAdmin.Get_RankList();
        ddlRank.DataTextField = "Rank_Short_Name";
        ddlRank.DataValueField = "ID";
        ddlRank.DataBind();
        ddlRank.Items.Insert(0, new ListItem("-SELECT-", "0"));
        ddlRank.SelectedIndex = 0;
    }
    public SMS.Properties.UserAccess objUA = new SMS.Properties.UserAccess();
    protected void UserAccessValidation()
    {
        try
        {
            int CurrentUserID = Convert.ToInt32(Session["userid"]);
            string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());
            SMS.Business.Infrastructure.BLL_Infra_UserCredentials objUser = new SMS.Business.Infrastructure.BLL_Infra_UserCredentials();

            objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

            if (objUA.View == 0)
                Response.Redirect("~/default.aspx?msgid=1");

            if (objUA.Add == 0)
            {


            }
            if (objUA.Edit == 0)
            {
                gvSeniorityRecords.Columns[gvSeniorityRecords.Columns.Count - 3].Visible = false;

            }
            if (objUA.Approve == 0)
            {

            }
            if (objUA.Delete == 0)
            {
                gvSeniorityRecords.Columns[gvSeniorityRecords.Columns.Count - 2].Visible = false;

            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
        {
            Session.Abandon();
            Response.Redirect("~/Account/Login.aspx");
            return 0;
        }
    }
    protected void Load_SeniorityRecords()
    {
        try
        {
            int PAGE_SIZE = ucCustomPager.PageSize;
            int PAGE_INDEX = ucCustomPager.CurrentPageIndex;

            int SelectRecordCount = ucCustomPager.isCountRecord;

            string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
            int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

            int RankId = int.Parse(ddlRank.SelectedValue.ToString());
            string Status = ddlStaus.SelectedValue.ToString();
            int CompanySeniorityYear = int.Parse(ddlCompanySeniorityFilter.SelectedValue.ToString());
            DataTable dt = BLL_PortageBill.Get_CrewSeniorityRecords(RankId, Status, CompanySeniorityYear, txtSearchText.Text.Trim(), PAGE_SIZE, PAGE_INDEX, ref SelectRecordCount, sortbycoloumn, sortdirection);
            UDFLib.ChangeColumnDataType(dt, "RankEffective_date", typeof(string));
            UDFLib.ChangeColumnDataType(dt, "CompanyEffective_date", typeof(string));
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (!string.IsNullOrEmpty(dr["RankEffective_date"].ToString()))
                    {
                        dr["RankEffective_date"] = UDFLib.ConvertUserDateFormat(Convert.ToDateTime(dr["RankEffective_date"].ToString()).ToString("dd/MM/yyyy"), UDFLib.GetDateFormat());
                    }
                    if (!string.IsNullOrEmpty(dr["CompanyEffective_date"].ToString()))
                    {
                        dr["CompanyEffective_date"] = UDFLib.ConvertUserDateFormat(Convert.ToDateTime(dr["CompanyEffective_date"].ToString()).ToString("dd/MM/yyyy"), UDFLib.GetDateFormat());
                    }
                }
            }
            gvSeniorityRecords.DataSource = dt;
            gvSeniorityRecords.DataBind();
            if (ucCustomPager.isCountRecord == 1)
            {
                ucCustomPager.CountTotalRec = SelectRecordCount.ToString();
                ucCustomPager.BuildPager();
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }

    }
    protected void gvSeniorityRecords_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvSeniorityRecords.PageIndex = e.NewPageIndex;
        Load_SeniorityRecords();

    }
    protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        Load_SeniorityRecords();
    }
    protected void gvSeniorityRecords_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if ((e.Row.RowState & DataControlRowState.Edit) > 0)
                {
                    int RankID = 0, RankSeniorityId = 0;
                    Label lblRankID = ((Label)e.Row.FindControl("lblRankID"));
                    Label lblSenorityRankID = ((Label)e.Row.FindControl("lblSeconityRankID"));
                    if (lblSenorityRankID != null && lblSenorityRankID.Text != "")
                        RankSeniorityId = int.Parse(lblSenorityRankID.Text.ToString());

                    if (lblRankID != null)
                        RankID = lblRankID.Text == "" ? 0 : Convert.ToInt32(lblRankID.Text);
                    DropDownList ddl = ((DropDownList)e.Row.FindControl("ddlSeniorityRank"));
                    if (ddl != null)
                    {
                        if (ddl.SelectedValue != null)
                        {
                            if (RankSeniorityId > 0)
                                ddl.SelectedValue = RankSeniorityId.ToString();
                            else
                                ddl.SelectedValue = RankID.ToString();
                        }
                    }

                    AjaxControlToolkit4.CalendarExtender calender1 = ((AjaxControlToolkit4.CalendarExtender)e.Row.FindControl("CalendarExtender2"));
                    calender1.Format = UDFLib.GetDateFormat();
                    AjaxControlToolkit4.CalendarExtender calender2 = ((AjaxControlToolkit4.CalendarExtender)e.Row.FindControl("CalendarExtender1"));
                    calender2.Format = UDFLib.GetDateFormat();
                }
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    protected void gvSeniorityRecords_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvSeniorityRecords.EditIndex = e.NewEditIndex;
        Load_SeniorityRecords();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Load_SeniorityRecords();
    }
    protected void gvSeniorityRecords_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            lblmsg.Text = "";
            int CrewID = UDFLib.ConvertToInteger(gvSeniorityRecords.DataKeys[e.RowIndex].Values[0].ToString());
            int CompanySeniorityYear = UDFLib.ConvertToInteger(gvSeniorityRecords.DataKeys[e.RowIndex].Values[1].ToString());
            int CompanySeniorityDays = UDFLib.ConvertToInteger(gvSeniorityRecords.DataKeys[e.RowIndex].Values[2].ToString());
            int CompanySeniorityYear_New = UDFLib.ConvertToInteger(e.NewValues["CompanySeniorityYear"]);
            int CompanySeniorityDays_New = UDFLib.ConvertToInteger(e.NewValues["CompanySeniorityDays"]);
            string Remarks = "Updating Company Seniority first time for existing crews";
            DateTime CompanyEffectiveDate_New;

            if ((CompanySeniorityYear_New > 0 || CompanySeniorityDays_New > 0) && (CompanySeniorityYear != CompanySeniorityYear_New || CompanySeniorityDays != CompanySeniorityDays_New))
            {
                if (CompanySeniorityDays_New > 364)
                    lblmsg.Text = "Company Seniority Days cannot be greater than 364";
                if (e.NewValues["CompanyEffective_date"] != null)
                {
                    CompanyEffectiveDate_New = UDFLib.ConvertToDate(Convert.ToString(e.NewValues["CompanyEffective_date"]),UDFLib.GetDateFormat());

                    BLL_PortageBill.Update_CrewCompanySeniority(CrewID, CompanySeniorityYear_New, CompanySeniorityDays_New, Remarks, CompanyEffectiveDate_New, Convert.ToInt32(Session["userid"]));
                }
                else
                {
                    lblmsg.Text = "Company Effective Date is mandatory to update Company Seniority";
                }
            }

            int RankSeniorityYear = UDFLib.ConvertToInteger(gvSeniorityRecords.DataKeys[e.RowIndex].Values[3].ToString());
            int RankSeniorityDays = UDFLib.ConvertToInteger(gvSeniorityRecords.DataKeys[e.RowIndex].Values[4].ToString());

            int RankSeniorityYear_New = UDFLib.ConvertToInteger(e.NewValues["RankSeniorityYear"]);
            int RankSeniorityDays_New = UDFLib.ConvertToInteger(e.NewValues["RankSeniorityDays"]);

            Remarks = "Updating Rank Seniority first time for existing crews";

            if ((RankSeniorityYear_New > 0 || RankSeniorityDays_New > 0) && (RankSeniorityYear != RankSeniorityYear_New || RankSeniorityDays != RankSeniorityDays_New))
            {
                GridViewRow row = (GridViewRow)gvSeniorityRecords.Rows[e.RowIndex];
                int RankId1 = int.Parse(((DropDownList)row.FindControl("ddlSeniorityRank")).SelectedValue.ToString());

                if (RankSeniorityDays_New > 364)
                    lblmsg.Text = "Rank Seniority Days cannot be greater than 364";
                if (e.NewValues["RankEffective_date"] != null)
                {
                    BLL_PortageBill.Update_CrewRankSeniority(CrewID, RankId1, RankSeniorityYear_New, RankSeniorityDays_New, 0, Remarks, UDFLib.ConvertToDate(e.NewValues["RankEffective_date"].ToString(), UDFLib.GetDateFormat()), Convert.ToInt32(Session["userid"]));
                }
                else
                {
                    lblmsg.Text = "Rank Effective Date is mandatory to update Rank Seniority";
                }
            }
            if (lblmsg.Text.Trim() == "")
            {
                lblmsg.Visible = false;
                gvSeniorityRecords.EditIndex = -1;
                Load_SeniorityRecords();
            }
            else
            {
                lblmsg.Visible = true;
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    protected void gvSeniorityRecords_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvSeniorityRecords.EditIndex = -1;
        Load_SeniorityRecords();
        lblmsg.Text = "";
        lblmsg.Visible = false;
    }
    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                MergeGridviewHeader.SetProperty(objContractList);

                e.Row.SetRenderMethodDelegate(MergeGridviewHeader.RenderHeader);
                ViewState["DynamicHeaderCSS"] = "HeaderStyle-css-2";
            }

        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
}