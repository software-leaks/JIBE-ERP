using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Infrastructure;
using System.Data;
using SMS.Business.Operation;
using SMS.Properties;

public partial class Operations_BunkerSampleLab : System.Web.UI.Page
{
    BLL_Infra_VesselLib objVessel = new BLL_Infra_VesselLib();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["USERID"] == null)
            Response.Redirect("~/account/login.aspx");

        if (!IsPostBack)
        {
            UserAccessValidation();
            Load_FleetList();
            Load_VesselList();
            Load_BunkerSampleAnalysis();
           
        }
    }
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
        {
            Response.Redirect("~/account/Login.aspx");
            return 0;
        }
    }
    protected void UserAccessValidation()
    {
        UserAccess objUA = new UserAccess();
        BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();

        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
        {
            Response.Redirect("~/default.aspx?msgid=1");
        }

        if (objUA.Add == 0 || objUA.Edit == 0)
            GridView_Bunker.Columns[GridView_Bunker.Columns.Count - 1].Visible = false;

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
        int Vessel_Manager = 1;

        if (Session["UTYPE"].ToString() == "VESSEL MANAGER")
            Vessel_Manager = UserCompanyID;

        ddlVessel.DataSource = objVessel.Get_VesselList(Fleet_ID, 0, Vessel_Manager, "", UserCompanyID);

        ddlVessel.DataTextField = "VESSEL_NAME";
        ddlVessel.DataValueField = "VESSEL_ID";
        ddlVessel.DataBind();
        ddlVessel.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));
        ddlVessel.SelectedIndex = 0;
    }

    protected void ddlFleet_SelectedIndexChanged(object sender, EventArgs e)
    {
        Load_VesselList();
    }
    protected void ctlBunkerPort_SelectedIndexChanged()
    {
        Load_BunkerSampleAnalysis();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Load_BunkerSampleAnalysis();
    }
    protected void BtnClearFilter_Click(object sender, EventArgs e)
    {
        ddlFleet.SelectedIndex = 0;
        Load_VesselList();
        ddlStatus.SelectedIndex = 0;
        txtDateFrom.Text = "";
        txtDateTo.Text = "";

        Load_BunkerSampleAnalysis();
    }

    protected void GridView_Bunker_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int statusid = UDFLib.ConvertToInteger(DataBinder.Eval(e.Row.DataItem, "statusid").ToString());
            if (statusid == 2)
            {
                Label lblStatus = (Label)e.Row.FindControl("lblStatus");
                if (lblStatus != null)
                {
                    lblStatus.BackColor = System.Drawing.Color.Red;
                    lblStatus.ForeColor = System.Drawing.Color.Yellow;
                }
            }
        }              
    }
    protected void GridView_Bunker_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            
            int ID = UDFLib.ConvertToInteger(GridView_Bunker.DataKeys[e.RowIndex].Value.ToString());
            
            int Status = UDFLib.ConvertToInteger(e.NewValues["StatusID"].ToString());
            int SampleReceived = Convert.ToInt32((bool)e.NewValues["SampleReceived_ByLab"]);

            int RetVal = BLL_OPS_BunkerAnalysis.UPDATE_BunkerAnalysisStatus(ID, Status, SampleReceived, GetSessionUserID());

            if (RetVal == 0)
            {
                string js = "alert('Not Updated !!');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "status", js, true);
            }

            GridView_Bunker.EditIndex = -1;
            Load_BunkerSampleAnalysis();
        }
        catch { }
    }
    protected void GridView_Bunker_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            GridView_Bunker.EditIndex = e.NewEditIndex;
            Load_BunkerSampleAnalysis();
        }
        catch
        {
        }
    }
    protected void GridView_Bunker_RowCancelEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {
            GridView_Bunker.EditIndex = -1;
            Load_BunkerSampleAnalysis();
        }
        catch
        {
        }
    }

    protected void Load_BunkerSampleAnalysis()
    {
        int FleetCode = UDFLib.ConvertToInteger(ddlFleet.SelectedValue);
        int Vessel_ID = UDFLib.ConvertToInteger(ddlVessel.SelectedValue);
        int Bunker_Supplier = 0; // UDFLib.ConvertToInteger(ddlBunkerSupplier.SelectedValue);
        int LabID = 0;

        int Status = UDFLib.ConvertToInteger(ddlStatus.SelectedValue);
        string DateFrom = txtDateFrom.Text;
        string DateTo = txtDateTo.Text;
        int PAGE_SIZE = ucCustomPager.PageSize;
        int PAGE_INDEX = ucCustomPager.CurrentPageIndex;
        int SelectRecordCount = ucCustomPager.isCountRecord;

        DataTable dt = BLL_OPS_BunkerAnalysis.Get_BunkerAnalysisReport(FleetCode, Vessel_ID, Bunker_Supplier, LabID, Status, DateFrom, DateTo, GetSessionUserID(), PAGE_SIZE, PAGE_INDEX, ref SelectRecordCount);

        GridView_Bunker.DataSource = dt;
        GridView_Bunker.DataBind();
        
        if (ucCustomPager.isCountRecord == 1)
        {
            ucCustomPager.CountTotalRec = SelectRecordCount.ToString();
            ucCustomPager.BuildPager();
        }
    }
    protected void ImgExportToExcel_Click(object sender, EventArgs e)
    {

        //int FleetCode = UDFLib.ConvertToInteger(ddlFleet.SelectedValue);
        //int Vessel_ID = UDFLib.ConvertToInteger(ddlVessel.SelectedValue);
        //int Bunker_Supplier = UDFLib.ConvertToInteger(ddlBunkerSupplier.SelectedValue);
        //int Status = UDFLib.ConvertToInteger(ddlStatus.SelectedValue);
        //string DateFrom = txtDateFrom.Text;
        //string DateTo = txtDateTo.Text;
        //int PAGE_SIZE = ucCustomPager.PageSize;
        //int PAGE_INDEX = ucCustomPager.CurrentPageIndex;
        //int SelectRecordCount = ucCustomPager.isCountRecord;

        //int PAGE_SIZE = 0;
        //int PAGE_INDEX = 0;
        //int SelectRecordCount = 0;
        //decimal GrandTotal = 0;
        
        //string[] HeaderCaptions = { "Manning Office", "Vessel", "S/Code", "Name", "Rank", "S/On Date", "S/Off Date", "Fee Type", "Due Date", "Due Amt", "Approved Amt", "Approved By", "Approved Date", "Remarks" };
        //string[] DataColumnsName = { "manning_Office", "Vessel_Short_Name", "Staff_Code", "Staff_FullName", "Rank_Short_Name", "Sign_On_Date", "Sign_Off_Date", "FeeTypeName", "Due_Date", "Due_Amount", "Approved_Amount", "ApprovedBy", "Approved_Date", "Remarks" };

        //DataTable dt = BLL_OPS_BunkerAnalysis.Get_BunkerAnalysisReport(FleetCode, Vessel_ID, Bunker_Supplier, Status, DateFrom, DateTo, GetSessionUserID(), PAGE_SIZE, PAGE_INDEX, ref SelectRecordCount);

        //GridViewExportUtil.ExportToExcel(dt, HeaderCaptions, DataColumnsName, "Agency Fee.xls", "Agency Fee Export");
        
    }

    protected void GridView_Bunker_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int Sample_ID = UDFLib.ConvertToInteger(e.CommandArgument.ToString());
        if (e.CommandName == "SendMailAcknowledge")
        {
            SendMail_Acknowledge(Sample_ID);
        }
    }

    protected void SendMail_Acknowledge(int Sample_ID)
    {
        int RetID = BLL_OPS_BunkerAnalysis.SendMail_AckSampleReceived(Sample_ID, GetSessionUserID());
        if (RetID > 0)
        {
            ResponseHelper.Redirect("~/Crew/EmailEditor.aspx?Discard=1&ID=" + RetID.ToString(), "_blank", null);
        }
    }
    //protected void SendMail_ToInternal(int Sample_ID)
    //{
    //    int RetID = BLL_OPS_BunkerAnalysis.SendMail_ToInternal(Sample_ID, GetSessionUserID());
    //    if (RetID > 0)
    //    {
    //        ResponseHelper.Redirect("~/Crew/EmailEditor.aspx?Discard=1&ID=" + RetID.ToString(), "_blank", null);
    //    }
    //}

}