using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Infrastructure;
using SMS.Business.PortageBill;
using System.Data;
using System.Text;


public partial class PortageBill_CTMIndex : System.Web.UI.Page
{
    BLL_Infra_VesselLib objVessel = new BLL_Infra_VesselLib();
    int ApproveRights = 0; public string TodayDateFormat = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        calFrom.Format = calTo.Format = UDFLib.GetDateFormat();
       
        TodayDateFormat = UDFLib.DateFormatMessage();
        if (!IsPostBack)
        {
            UserAccessValidation();
            FillDDL();
            string stsClientID = "";

            if (UDFLib.ConvertToInteger(Request.QueryString["CTMAPPROVAL"]) == 1)
            {
                // ViewState["CTM_STATUS"] = "APPROVAL";
                ViewState["CTM_STATUS"] = "SENTTOOFFICE";
                ViewState["PendingWithMe"] = true;
                stsClientID = lnkMenuAPPROVAL.ClientID;
            }
            else
            {
                ViewState["CTM_STATUS"] = "0";
                stsClientID = lnkMenuALL.ClientID;
                ViewState["PendingWithMe"] = false;
            }

            Load_CTM_Requests();

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "selectNode", "selMe('" + stsClientID + "');", true);
        }
    }
    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        SMS.Properties.UserAccess objUA = new SMS.Properties.UserAccess();
        SMS.Business.Infrastructure.BLL_Infra_UserCredentials objUser = new SMS.Business.Infrastructure.BLL_Infra_UserCredentials();
        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
            Response.Redirect("~/default.aspx?msgid=1");

        if (objUA.Add == 0)
        {
            Response.Redirect("~/default.aspx?msgid=1");
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

    public void FillDDL()
    {

        try
        {

            BLL_Infra_VesselLib objVsl = new BLL_Infra_VesselLib();

            DataTable FleetDT = objVsl.GetFleetList(Convert.ToInt32(Session["USERCOMPANYID"].ToString()));
            ddlFleet.DataSource = FleetDT;
            ddlFleet.DataTextField = "Name";
            ddlFleet.DataValueField = "code";
            ddlFleet.DataBind();


            DataTable dtVessel = objVsl.Get_VesselList(0, 0, Convert.ToInt32(Session["USERCOMPANYID"].ToString()), "", Convert.ToInt32(Session["USERCOMPANYID"].ToString()));

            ddlVessel.DataSource = dtVessel;
            ddlVessel.DataTextField = "Vessel_name";
            ddlVessel.DataValueField = "Vessel_id";
            ddlVessel.DataBind();





        }
        catch (Exception ex)
        {

        }
        finally
        {

        }
    }

    protected void Load_CTM_Requests()
    {
        try
        {


            bool PendingWith = false;
            int? PendingWithUserID = null;

            if (Boolean.TryParse(Convert.ToString(ViewState["PendingWithMe"]), out PendingWith))
            {
                if (PendingWith == true)
                    PendingWithUserID = PendingWith ? UDFLib.ConvertIntegerToNull(Session["userid"]) : null;
            }

            if (ViewState["CTM_STATUS"].ToString() == "SENTTOOFFICE")
            {
                if (Boolean.TryParse(Convert.ToString(ViewState["PendingWithMe"]), out PendingWith))
                {
                    PendingWithUserID = PendingWith ? UDFLib.ConvertIntegerToNull(Session["userid"]) : null;
                }
            }
            int rowcount = ucCustomPagerItems.isCountRecord;
            DataTable dt = BLL_PB_PortageBill.Get_CTM_Requests(ddlFleet.SelectedValues, ddlVessel.SelectedValues, txtFromDate.Text == "" ? "" : UDFLib.ConvertToDate(txtFromDate.Text, UDFLib.GetDateFormat()).ToShortDateString(), txtToDate.Text == "" ? "" : UDFLib.ConvertToDate(txtToDate.Text, UDFLib.GetDateFormat()).ToShortDateString()
                , UDFLib.ConvertStringToNull(ViewState["CTM_STATUS"]), UDFLib.ConvertStringToNull(txtSearch.Text), PendingWithUserID
                    , ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);
            gvCTMRequests.DataSource = dt;
            gvCTMRequests.DataBind();


            ucCustomPagerItems.CountTotalRec = rowcount.ToString();
            ucCustomPagerItems.BuildPager();


            if (ViewState["CTM_STATUS"].ToString() != "SENTTOOFFICE") //Pending with me
                gvCTMRequests.Columns[UDFLib.FindGridColumnIndex(gvCTMRequests.Columns, "ApproverName")].Visible = false;
            else
                gvCTMRequests.Columns[UDFLib.FindGridColumnIndex(gvCTMRequests.Columns, "ApproverName")].Visible = true;

            if (ViewState["CTM_STATUS"].ToString() == "APPROVED")
            {
                gvCTMRequests.Columns[UDFLib.FindGridColumnIndex(gvCTMRequests.Columns, "ApprovedAmt")].Visible = false;
                gvCTMRequests.Columns[UDFLib.FindGridColumnIndex(gvCTMRequests.Columns, "ApprovedOn")].Visible = false;
            }
            else
            {
                gvCTMRequests.Columns[UDFLib.FindGridColumnIndex(gvCTMRequests.Columns, "ApprovedAmt")].Visible = true;
                gvCTMRequests.Columns[UDFLib.FindGridColumnIndex(gvCTMRequests.Columns, "ApprovedOn")].Visible = true;
            }

            if (ViewState["CTM_STATUS"].ToString() == "ACKVESSEL")
            {
                gvCTMRequests.Columns[UDFLib.FindGridColumnIndex(gvCTMRequests.Columns, "ReceivedAmt")].Visible = true;//Received ON
                gvCTMRequests.Columns[UDFLib.FindGridColumnIndex(gvCTMRequests.Columns, "DateReceived")].Visible = true;//Received Amount
            }
            else
            {
                gvCTMRequests.Columns[UDFLib.FindGridColumnIndex(gvCTMRequests.Columns, "ReceivedAmt")].Visible = false;
                gvCTMRequests.Columns[UDFLib.FindGridColumnIndex(gvCTMRequests.Columns, "DateReceived")].Visible = false;
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Load_CTM_Requests();
    }

    protected void btnClearFilter_Click(object sender, EventArgs e)
    {
        txtFromDate.Text = "";
        txtToDate.Text = "";
        txtSearch.Text = "";

        ddlFleet.ClearSelection();
        ddlVessel.ClearSelection();

        Load_CTM_Requests();
    }


    protected void gvCTMRequests_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ViewDetails")
        {
            string[] Args = e.CommandArgument.ToString().Split(new char[] { ',' });
            ResponseHelper.Redirect("CTMRequest.aspx?ID=" + Args[0] + "&Vessel_ID=" + Args[1], "_blank", "");
        }
    }

    protected void DDLFleet_SelectedIndexChanged()
    {
        BLL_Infra_VesselLib objVsl = new BLL_Infra_VesselLib();
        StringBuilder sbFilterFlt = new StringBuilder();
        string VslFilter = "";
        foreach (DataRow dr in ddlFleet.SelectedValues.Rows)
        {
            sbFilterFlt.Append(dr[0]);
            sbFilterFlt.Append(",");
        }

        DataTable dtVessel = objVsl.Get_VesselList(0, 0, Convert.ToInt32(Session["USERCOMPANYID"].ToString()), "", Convert.ToInt32(Session["USERCOMPANYID"].ToString()));

        if (sbFilterFlt.Length > 1)
        {
            sbFilterFlt.Remove(sbFilterFlt.Length - 1, 1);
            VslFilter = string.Format("fleetCode in (" + sbFilterFlt.ToString() + ")");
            dtVessel.DefaultView.RowFilter = VslFilter;
        }

        ddlVessel.DataSource = dtVessel;
        ddlVessel.DataTextField = "Vessel_name";
        ddlVessel.DataValueField = "Vessel_id";
        ddlVessel.DataBind();
        Load_CTM_Requests();

    }


    protected void DDLVessel_SelectedIndexChanged()
    {
        Load_CTM_Requests();
    }


    protected void NavMenu_Click(object sender, EventArgs e)
    {
        string value = ((LinkButton)sender).CommandArgument;

        ViewState["CTM_STATUS"] = value;

        if (value != "SENTTOOFFICE")
            ViewState["PendingWithMe"] = false;
        else
            ViewState["PendingWithMe"] = true;

        txtSelMenu.Value = ((LinkButton)sender).ClientID;
        Load_CTM_Requests();
    }

    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "selectNode", "selMe('" + txtSelMenu.Value + "');", true);
        }
    }

    protected void chkPendingWith_CheckedChanged(object s, EventArgs e)
    {
        ViewState["PendingWithMe"] = (s as CheckBox).Checked;
        Load_CTM_Requests();
    }
}