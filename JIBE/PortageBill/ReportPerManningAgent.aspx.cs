using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using SMS.Business.Infrastructure;
using SMS.Business.PortageBill;
using System.IO;
using AjaxControlToolkit4;
using SMS.Business.Crew;

public partial class PortageBill_ReportPerManningAgent : System.Web.UI.Page
{
    BLL_Infra_VesselLib objVessel = new BLL_Infra_VesselLib();
    BLL_Crew_Admin objBLL = new BLL_Crew_Admin();
    decimal TotalAmount = 0;
    int? CrewID = null;
    int? AllotmentID = null;
    protected void Page_Load(object sender, EventArgs e)
    {

        UserAccessValidation();

        

        if (Session["USERID"] == null)
            Response.Redirect("~/account/login.aspx");

      
            if (!IsPostBack)
            {
                
                ViewState["PBMonth"] = "0";
                ViewState["PBYear"] = "0";
                ViewState["PBMonthPrev"] = "0";
                ViewState["PBYearPrev"] = "0";

                Load_FleetList();
                Load_VesselList();
                Load_Years();
                Load_ManningAgent();
                Load_BankNames();

                BLL_Crew_CrewDetails objCrew = new BLL_Crew_CrewDetails();
                ddlCountry.DataSource = objCrew.Get_CrewNationality(GetSessionUserID());
                ddlCountry.DataTextField = "COUNTRY";
                ddlCountry.DataValueField = "ID";
                ddlCountry.DataBind();
                ddlCountry.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));
                ddlCountry.SelectedIndex = 0;

                
                string Month = DateTime.Today.Month.ToString();
                string Year = DateTime.Today.Year.ToString();
                try
                {
                    ddlYear.Items.FindByValue(Year).Selected = true;
                    ddlMonth.Items.FindByValue(Month).Selected = true;
                }
                catch { }
                Load_Allotments();
        }
            //string msg1 = String.Format("$('.sailingInfo').SailingInfo();");
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgshowdetails", msg1, true);
    }
    public SMS.Properties.UserAccess objUA = new SMS.Properties.UserAccess();
    protected void UserAccessValidation()
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
            gvAllotments.Columns[gvAllotments.Columns.Count - 3].Visible = false;
            

        }
        if (objUA.Approve == 0)
        {

        }
        if (objUA.Delete == 0)
        {
            gvAllotments.Columns[gvAllotments.Columns.Count - 2].Visible = false;

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
    public void Load_VesselList()
    {
        int Fleet_ID = int.Parse(ddlFleet.SelectedValue);
        int UserCompanyID = UDFLib.ConvertToInteger(Session["USERCOMPANYID"].ToString());
        int Vessel_Manager = UDFLib.ConvertToInteger(Session["USERCOMPANYID"].ToString());

        ddlVessel.DataSource = objVessel.Get_VesselList(Fleet_ID, 0, Vessel_Manager, "", UserCompanyID);

        ddlVessel.DataTextField = "VESSEL_NAME";
        ddlVessel.DataValueField = "VESSEL_ID";
        ddlVessel.DataBind();
        ddlVessel.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));
        //ddlVessel.SelectedIndex = 0;
    }
    protected void Load_ManningAgent()
    {
        BLL_Crew_CrewDetails objBLLCrew = new BLL_Crew_CrewDetails();
        int UserCompanyID = UDFLib.ConvertToInteger(Session["USERCOMPANYID"].ToString());
        DataTable dt = objBLLCrew.Get_ManningAgentList(UserCompanyID);

        ddlManningAgent.DataSource = dt;
        ddlManningAgent.DataTextField = "COMPANY_NAME";
        ddlManningAgent.DataValueField = "ID";
        ddlManningAgent.DataBind();
        ddlManningAgent.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));
    }
    protected void Load_BankNames()
    {
        DataTable dt = objBLL.Get_MOBankAccountList_ByManningID(Convert.ToInt32(ddlManningAgent.SelectedValue));

        ListItem liAll = new ListItem("--SELECT ALL--", "0", true);
        DDLBank.Items.Clear();

        if (ddlManningAgent.SelectedValue != "0")
        {
            DDLBank.DataSource = dt;
            DDLBank.DataTextField = "MO_Account";
            DDLBank.DataValueField = "ID";
            DDLBank.DataBind();
        }

        DDLBank.Items.Insert(0, liAll);
    }
    protected void Load_Years()
    {
        int Y = DateTime.Today.Year;

        for (int i = 0; i < 10; i++)
        {
            ddlYear.Items.Add(new ListItem((Y - i).ToString(), (Y - i).ToString()));
        }
        ddlYear.Items.Insert(0, new ListItem("-Select-", "0"));
    }
    protected void Load_Allotments()
    {
        //int? AmountValue = chkAmountIsGreaterthanZero.Checked == true ? UDFLib.ConvertIntegerToNull(1) : null;
        gvAllotments.PageSize = int.Parse(ddlPageSize.SelectedValue);

        DataTable dt = BLL_PB_PortageBill.Get_PerMOAllotments(int.Parse(ddlFleet.SelectedValue)
                                                         , int.Parse(ddlVessel.SelectedValue)
                                                         , ddlMonth.SelectedValue
                                                         , ddlYear.SelectedValue
                                                         , int.Parse(ddlManningAgent.SelectedValue)
                                                         , int.Parse(DDLBank.SelectedValue)
                                                         , CrewID
                                                         , Convert.ToInt32(ddlCountry.SelectedValue));
        gvAllotments.DataSource = dt;
        gvAllotments.DataBind();

        lblRecordCount.Text = dt.Rows.Count.ToString();
        if (gvAllotments.PageCount > 0)
            lblPageStatus.Text = (gvAllotments.PageIndex + 1).ToString() + " of " + gvAllotments.PageCount.ToString();
        else
            lblPageStatus.Text = "0 of 0";
        UpdatePanel2.Update();

    }
    protected void ddlFleet_SelectedIndexChanged(object sender, EventArgs e)
    {
        Load_VesselList();
    }
    protected void ddlManningAgent_SelectedIndexChanged(object sender, EventArgs e)
    {
        Load_BankNames();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Load_Allotments();
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
    protected void gvAllotments_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ((Label)e.Row.FindControl("lblkAccountInfo")).Attributes.Add("onmousemove", "js_ShowToolTip('" + UDFLib.ReplaceSpecialCharacter(DataBinder.Eval(e.Row.DataItem, "Bank_Details").ToString()) + "',event,this)");
                
            string approval_status = DataBinder.Eval(e.Row.DataItem, "approval_status").ToString();
            string AllotmentID = DataBinder.Eval(e.Row.DataItem, "AllotmentID").ToString();
            decimal Amount = UDFLib.ConvertToDecimal(DataBinder.Eval(e.Row.DataItem, "Amount").ToString());


            TotalAmount = TotalAmount + Amount;
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lblAmountTotal = (Label)e.Row.FindControl("lblAmountTotal");
            if (lblAmountTotal != null)
            {
                lblAmountTotal.Text = TotalAmount.ToString("0.00");
                TotalAmount = 0;
            }
        }

    }
    protected void gvAllotments_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvAllotments.PageIndex = e.NewPageIndex;
        Load_Allotments();

    }
    protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        Load_Allotments();
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        ddlFleet.SelectedValue = "0";
        ddlVessel.SelectedValue = "0";
        ddlManningAgent.SelectedValue = "0";
        DDLBank.SelectedValue = "0";
        ddlCountry.SelectedValue = "0";
        string Month = DateTime.Today.Month.ToString();
        string Year = DateTime.Today.Year.ToString();
        try
        {
            ddlYear.ClearSelection();
            ddlMonth.ClearSelection();
            ddlYear.Items.FindByValue(Year).Selected = true;
            ddlMonth.Items.FindByValue(Month).Selected = true;
        }
        catch { }
        Load_Allotments();
    }
    protected void btnExpToExl_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = BLL_PB_PortageBill.Get_PerMOAllotments(int.Parse(ddlFleet.SelectedValue)
                                                              , int.Parse(ddlVessel.SelectedValue)
                                                              , ddlMonth.SelectedValue
                                                              , ddlYear.SelectedValue
                                                              , int.Parse(ddlManningAgent.SelectedValue)
                                                              , int.Parse(DDLBank.SelectedValue)
                                                              , CrewID
                                                              , Convert.ToInt32(ddlCountry.SelectedValue));

            decimal iAmount = 0;
            foreach (DataRow row in dt.Rows)
            {
                iAmount = iAmount + UDFLib.ConvertToDecimal(row["Amount"]);
            }

            string[] HeaderCaptions = { "Vessel", "Staff Code", "Name", "Rank", "Seaman ID", "Manning Agent", "Account No.", "Beneficiary", "Bank Name", "PBDate" ,"Amount", "Currency"};
            string[] DataColumnsName = { "vessel_short_name", "STAFF_CODE", "Staff_fullName", "Rank_Short_Name", "Seaman_Book_Number", "Company_Name", "BankAccId", "Beneficiary", "Bank_Name", "PBill_Date", "Amount", "Currency" };

            GridViewExportUtil.ShowExcel(dt, HeaderCaptions, DataColumnsName, "ReportperManningAgent", "Report-per Manning Agent- Total Amount: " + iAmount.ToString("0.00"), "");
        }
        catch (Exception ex)
        {
            
        }
   }
}