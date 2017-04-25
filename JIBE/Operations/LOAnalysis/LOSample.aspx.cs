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
using System.IO;

public partial class Operations_LOSample : System.Web.UI.Page
{
    BLL_Infra_VesselLib objVessel = new BLL_Infra_VesselLib();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["USERID"] == null)
            Response.Redirect("~/account/login.aspx");

        if (!IsPostBack)
        {
            Load_FleetList();
            ddlFleet.SelectedValue = Session["USERFLEETID"].ToString();
            Load_VesselList();
            Load_BunkerSampleAnalysis();
            Load_LOSupplierList();
            Load_LOLabList();
            UserAccessValidation();
        }

        string js = "positionOffllandPanel();";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "js", js, true);
    }
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return UDFLib.ConvertToInteger(Session["USERID"]);
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
            Response.Redirect("~/default.aspx");

        if (objUA.Add == 0 || objUA.Edit == 0)
            GridView_Bunker.Columns[GridView_Bunker.Columns.Count - 1].Visible = false;

    }
    public void Load_FleetList()
    {
        int UserCompanyID = UDFLib.ConvertToInteger(Session["USERCOMPANYID"]);
        ddlFleet.DataSource = objVessel.GetFleetList(UserCompanyID);
        ddlFleet.DataTextField = "NAME";
        ddlFleet.DataValueField = "CODE";
        ddlFleet.DataBind();
        ddlFleet.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));
    }
    public void Load_VesselList()
    {
        int Fleet_ID = int.Parse(ddlFleet.SelectedValue);
        int UserCompanyID = UDFLib.ConvertToInteger(Session["USERCOMPANYID"]);
        int Vessel_Manager = UDFLib.ConvertToInteger(Session["USERCOMPANYID"]);

        if (Convert.ToString(Session["UTYPE"]) == "VESSEL MANAGER")
            Vessel_Manager = UserCompanyID;

        ddlVessel.DataSource = objVessel.Get_VesselList(Fleet_ID, 0, Vessel_Manager, "", UserCompanyID);

        ddlVessel.DataTextField = "VESSEL_NAME";
        ddlVessel.DataValueField = "VESSEL_ID";
        ddlVessel.DataBind();
        ddlVessel.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));
        ddlVessel.SelectedIndex = 0;
    }
    public void Load_LOSupplierList()
    {
        DataTable dt = BLL_OPS_BunkerAnalysis.Get_LOSupplierList(GetSessionUserID());
        ddlBunkerSupplier.DataSource = dt;
        ddlBunkerSupplier.DataBind();
    }

    public void Load_LOLabList()
    {
        int rowcount = 0;
        DataTable dt = BLL_OPS_BunkerAnalysis.Get_LOTestingLabList("", 0, GetSessionUserID(), null, null, 0, 0, ref  rowcount);
       
        ddlLabList.DataSource = dt;
        ddlLabList.DataBind();
    }

    protected void ddlFleet_SelectedIndexChanged(object sender, EventArgs e)
    {
        Load_VesselList();
        Load_BunkerSampleAnalysis();
    }
    protected void ddlVessel_SelectedIndexChanged(object sender, EventArgs e)
    {
        Load_BunkerSampleAnalysis();
    }
    protected void ddlLabList_SelectedIndexChanged(object sender, EventArgs e)
    {
        Load_BunkerSampleAnalysis();
    }
    protected void ddlBunkerSupplier_SelectedIndexChanged(object sender, EventArgs e)
    {
        Load_BunkerSampleAnalysis();
    }

    protected void txtAirwayBill_TextChanged(object sender, EventArgs e)
    {
        Load_BunkerSampleAnalysis();
    }

    protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        Load_BunkerSampleAnalysis();
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
        txtAirwayBill.Text = "";

        if (ddlBunkerSupplier.Items.Count > 0)
            ddlBunkerSupplier.SelectedIndex = 0;

        if (ddlLabList.Items.Count > 0)
            ddlLabList.SelectedIndex = 0;
        ctlBunkerPort.SelectedValue = "0";
        ddlStatus.SelectedValue = "0";

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
            string Bunkering_Date = "";
            string AirwayBill = "";

            int ID = UDFLib.ConvertToInteger(GridView_Bunker.DataKeys[e.RowIndex].Values[0].ToString());

            if (e.NewValues["Bunkering_Date"] != null)
                Bunkering_Date = e.NewValues["Bunkering_Date"].ToString();

            int PortID = UDFLib.ConvertToInteger(e.NewValues["Bunkering_Port"].ToString());
            int Bunker_SupplierID = UDFLib.ConvertToInteger(e.NewValues["Bunker_SupplierID"].ToString());
            int LabID = UDFLib.ConvertToInteger(e.NewValues["LabID"].ToString());
            int VesselID = UDFLib.ConvertToInteger(GridView_Bunker.DataKeys[e.RowIndex].Values[1].ToString());

            if (e.NewValues["AirwayBill"] != null)
                AirwayBill = e.NewValues["AirwayBill"].ToString();
            int Status = UDFLib.ConvertToInteger(e.NewValues["StatusID"].ToString());


            DataSet ds = BLL_OPS_BunkerAnalysis.UPDATE_BunkerAnalysis(ID, VesselID, Bunkering_Date, PortID, Bunker_SupplierID, LabID, AirwayBill, Status, GetSessionUserID());

            if (ds.Tables.Count > 0)
            {

                if (Status == 2)
                {
                    if (ds.Tables.Count > 1)
                    {
                        foreach (DataRow item in ds.Tables[ds.Tables.Count - 1].Rows)
                        {
                            if (File.Exists(Server.MapPath("~/Uploads/BunkerAnalysis/") + item["File_Name"].ToString()))
                            {
                                if (!File.Exists(Server.MapPath("~/Uploads/Technical/") + item["File_Name"].ToString()))
                                {
                                    File.Copy(Server.MapPath("~/Uploads/BunkerAnalysis/") + item["File_Name"].ToString(), Server.MapPath("~/Uploads/Technical/") + item["File_Name"].ToString());
                                }
                            }
                        }
                    }
                }
            }
            else
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
            GridViewRow row = GridView_Bunker.Rows[e.NewEditIndex];
            if (row != null)
            {
                DropDownList grdddlBunkerSupplier = (DropDownList)row.FindControl("ddlBunkerSupplier");
                DropDownList grdddlLabName = (DropDownList)row.FindControl("ddlLabName");
                if (grdddlBunkerSupplier != null)
                {
                    DataRowView dataRow = (DataRowView)row.DataItem;
                    string BunkerSupplier = ((HiddenField)row.FindControl("hdnBunkerSupplier")).Value.Trim();
                    grdddlBunkerSupplier.SelectedValue = PreventUnlistedValueError(grdddlBunkerSupplier, BunkerSupplier);
                }
                if (grdddlLabName != null)
                {
                    DataRowView dataRow = (DataRowView)row.DataItem;
                    string Lab = ((HiddenField)row.FindControl("hdnLabID")).Value.Trim();
                    grdddlLabName.SelectedValue = PreventUnlistedValueError(grdddlLabName, Lab);
                }

            }
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
        int Bunker_Supplier = UDFLib.ConvertToInteger(ddlBunkerSupplier.SelectedValue);
        int LabID = UDFLib.ConvertToInteger(ddlLabList.SelectedValue);
        int Status = UDFLib.ConvertToInteger(ddlStatus.SelectedValue);
        string DateFrom = txtDateFrom.Text;
        string DateTo = txtDateTo.Text;

        
        int PAGE_SIZE = ucCustomPager.PageSize;
        int PAGE_INDEX = ucCustomPager.CurrentPageIndex;
        int SelectRecordCount = ucCustomPager.isCountRecord;

        DataTable dt = BLL_OPS_BunkerAnalysis.Get_LOAnalysisReport(FleetCode, Vessel_ID, Bunker_Supplier, LabID, Status, DateFrom, DateTo, GetSessionUserID(), UDFLib.ConvertToInteger( ctlBunkerPort.SelectedValue), txtAirwayBill.Text, PAGE_SIZE, PAGE_INDEX, ref SelectRecordCount);

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

        if (e.CommandName == "SendMailToVessel")
        {
            SendMail_ToVessel(Sample_ID);
        }

        if (e.CommandName == "SendMailToInternal")
        {
            SendMail_ToInternal(Sample_ID);
        }

    }

    protected void SendMail_ToVessel(int Sample_ID)
    {
        int RetID = BLL_OPS_BunkerAnalysis.SendMail_ToVessel(Sample_ID, GetSessionUserID());
        if (RetID > 0)
        {
            ResponseHelper.Redirect("~/Crew/EmailEditor.aspx?Discard=1&ID=" + RetID.ToString(), "_blank", null);
        }
    }
    protected void SendMail_ToInternal(int Sample_ID)
    {
        int RetID = BLL_OPS_BunkerAnalysis.SendMail_ToInternal(Sample_ID, GetSessionUserID());
        if (RetID > 0)
        {
            ResponseHelper.Redirect("~/Crew/EmailEditor.aspx?Discard=1&ID=" + RetID.ToString(), "_blank", null);
        }
    }
    #region To Prevent Error if value doesn't exists in table
    protected string PreventUnlistedValueError(DropDownList li, string val)
    {
        if (li.Items.FindByValue(val) == null)
        {

            li.SelectedValue = "0";
            val = "0";

        }
        return val;
    }
    #endregion

}