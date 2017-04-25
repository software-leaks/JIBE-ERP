using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Infrastructure;
using SMS.Business.Crew;
using System.Data;

public partial class CrewDisbursement_DisbUpdateFee : System.Web.UI.Page
{
    BLL_Infra_VesselLib objVessel = new BLL_Infra_VesselLib();
    BLL_Crew_CrewDetails objCrew = new BLL_Crew_CrewDetails();
    BLL_Crew_Admin objBLL = new BLL_Crew_Admin();

    decimal DueTotal = 0;
    decimal ApprovedTotal = 0;
    public string TodayDateFormat = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        CalendarExtender1.Format = Convert.ToString(Session["User_DateFormat"]);
        CalendarExtender3.Format = Convert.ToString(Session["User_DateFormat"]);
        CalendarExtender4.Format = Convert.ToString(Session["User_DateFormat"]);
        CalendarExtender5.Format = Convert.ToString(Session["User_DateFormat"]);
        
        TodayDateFormat = UDFLib.DateFormatMessage();
        if (!IsPostBack)
        {
            UserAccessValidation();
            Load_FleetList();
            Load_VesselList();
            Load_RankCategory();
            Load_ManningAgentList();
            Load_DisbFeeTypes();
            Load_CrewProcessingFee();

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
            GridView_Crew.Columns[GridView_Crew.Columns.Count - 1].Visible = false;
        }
        if (objUA.Edit == 0)
        {


        }
        if (objUA.Delete == 0)
        {
        }
        if (objUA.Approve == 0)
        {
            GridView_Crew.Columns[GridView_Crew.Columns.Count - 2].Visible = false;
            GridView_Crew.Columns[GridView_Crew.Columns.Count - 5].Visible = false;
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
        int Fleet_ID = UDFLib.ConvertToInteger(ddlFleet.SelectedValue);
        DataTable dt = BLL_Crew_Disbursement.Get_VesselList_CrewFee(GetSessionUserID(), Fleet_ID);

        ddlVessel.DataSource = dt;
        ddlVessel.DataTextField = "VESSEL_NAME";
        ddlVessel.DataValueField = "VESSEL_ID";
        ddlVessel.DataBind();
        ddlVessel.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));
        ddlVessel.SelectedIndex = 0;
    }

    public void Load_ManningAgentList()
    {
        int UserCompanyID = 0;
        if (Session["USERCOMPANYID"].ToString() != "")
        {
            UserCompanyID = int.Parse(Session["USERCOMPANYID"].ToString());
        }
        ddlManningOffice.DataSource = objCrew.Get_ManningAgentList(UserCompanyID);
        ddlManningOffice.DataTextField = "COMPANY_NAME";
        ddlManningOffice.DataValueField = "ID";
        ddlManningOffice.DataBind();
        ddlManningOffice.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));
    }

    protected void Load_DisbFeeTypes()
    {
        DataTable dt = BLL_Crew_Disbursement.Get_MODisbursementFeeTypes();
        ddlFeeType.DataSource = dt;
        ddlFeeType.DataTextField = "FeeType";
        ddlFeeType.DataValueField = "FeeTypeID";
        ddlFeeType.DataBind();
        ddlFeeType.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));

        DataView dv = new DataView(dt, "FeeTypeID not in (1,2)", "FeeType", DataViewRowState.OriginalRows);
        ddlFeeType_AddNew.DataSource = dv;
        ddlFeeType_AddNew.DataTextField = "FeeType";
        ddlFeeType_AddNew.DataValueField = "FeeTypeID";
        ddlFeeType_AddNew.DataBind();
        ddlFeeType_AddNew.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));
    }
    protected void Load_RankCategory()
    {
        DataTable dt = objBLL.Get_RankCategories();

        ddlRankCategory.DataSource = dt;
        ddlRankCategory.DataTextField = "category_name";
        ddlRankCategory.DataValueField = "id";
        ddlRankCategory.DataBind();
        ddlRankCategory.Items.Insert(0, new ListItem("-Select-", "0"));
    }
    protected void ddlFleet_SelectedIndexChanged(object sender, EventArgs e)
    {
        Load_VesselList();
    }

    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        if (ValidateDateFormat() == true)
        {
            try
            {
                Load_CrewProcessingFee();
            }
            catch (Exception ex)
            {
                UDFLib.WriteExceptionLog(ex);
            }

        }
    }
    protected void BtnClearFilter_Click(object sender, EventArgs e)
    {
        ddlManningOffice.SelectedIndex = 0;
        ddlFleet.SelectedIndex = 0;
        ddlVessel.SelectedIndex = 0;
        ddlRankCategory.SelectedIndex = 0;
        ddlStatus.SelectedIndex = 0;
        ddlFeeType.SelectedIndex = 0;
        txtSignOnFrom.Text = "";
        txtSignOnTo.Text = "";
        txtApprovedFrom.Text = "";
        txtApprovedTo.Text = "";
        Load_CrewProcessingFee();
    }

    protected void GridView_Crew_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string Remarks;

        Image ImgRemarks = (Image)e.Row.FindControl("ImgRemarks");
        if (ImgRemarks != null)
        {
            Remarks = "Approved By:" + DataBinder.Eval(e.Row.DataItem, "ApprovedBy").ToString() + "<br>Date : " + DataBinder.Eval(e.Row.DataItem, "Approved_Date", "{0:dd/MM/yyyy}").ToString() + "<br>";
            if (DataBinder.Eval(e.Row.DataItem, "Remarks") != null)
            {
                Remarks += "Remarks: " + DataBinder.Eval(e.Row.DataItem, "Remarks").ToString().Replace("\n", "<br>");
            }
            ImgRemarks.Attributes.Add("Title", "cssbody=[dvbdy1] cssheader=[dvhdr1] header=[Remarks:] body=[" + Remarks + "]");

        }

        Label lblDueAmt = (Label)e.Row.FindControl("lblDueAmt");
        if (lblDueAmt != null)
        {
            DueTotal += UDFLib.ConvertToDecimal(lblDueAmt.Text);
        }
        HiddenField hdnApprovedAmt = (HiddenField)e.Row.FindControl("hdnApprovedAmt");
        if (hdnApprovedAmt != null)
        {
            ApprovedTotal += UDFLib.ConvertToDecimal(hdnApprovedAmt.Value);
        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lblDueTotal = (Label)e.Row.FindControl("lblDueTotal");
            if (lblDueTotal != null)
            {
                lblDueTotal.Text = DueTotal.ToString();
            }
            Label lblApprovedTotal = (Label)e.Row.FindControl("lblApprovedTotal");
            if (lblApprovedTotal != null)
            {
                lblApprovedTotal.Text = ApprovedTotal.ToString();
            }
        }
        if (DataBinder.Eval(e.Row.DataItem, "Joining_Type") != null)
        {
            int Joining_Type = UDFLib.ConvertToInteger(DataBinder.Eval(e.Row.DataItem, "Joining_Type").ToString());
            if (Joining_Type == 4)
            {
                e.Row.CssClass = "Promoted-Voyage-Row";
            }
        }
    }
    protected void GridView_Crew_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            string Remarks = null;
            int ID = UDFLib.ConvertToInteger(GridView_Crew.DataKeys[e.RowIndex].Value.ToString());
            int CrewID = UDFLib.ConvertToInteger(e.NewValues["CrewID"].ToString());
            int Approved_YesNo = UDFLib.ConvertToInteger(e.NewValues["Approved_YesNo"].ToString());
            string Approved_Date = DateTime.Now.ToString("yyyy/MM/dd");

            if (e.NewValues["Remarks"] != null)
                Remarks = e.NewValues["Remarks"].ToString();


            int RetVal = BLL_Crew_Disbursement.UPDATE_ApprovedStatus(ID, CrewID, Approved_YesNo, Approved_Date, GetSessionUserID(), Remarks);

            if (RetVal == 0)
            {
                string js = "alert('Processing Fee is not yet set for the Manning Office');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "status", js, true);
            }

            GridView_Crew.EditIndex = -1;
            Load_CrewProcessingFee();
        }
        catch { }
    }
    protected void GridView_Crew_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            GridView_Crew.EditIndex = e.NewEditIndex;
            Load_CrewProcessingFee();
        }
        catch
        {
        }
    }
    protected void GridView_Crew_RowCancelEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {
            GridView_Crew.EditIndex = -1;
            Load_CrewProcessingFee();
        }
        catch
        {
        }
    }

    protected void Load_CrewProcessingFee()
    {
        int FleetCode = UDFLib.ConvertToInteger(ddlFleet.SelectedValue);
        int VesselID = UDFLib.ConvertToInteger(ddlVessel.SelectedValue);
        int ManningOfficeID = UDFLib.ConvertToInteger(ddlManningOffice.SelectedValue);
        int Rank_Category = UDFLib.ConvertToInteger(ddlRankCategory.SelectedValue);
        int Crew_Status = UDFLib.ConvertToInteger(ddlStatus.SelectedValue);
        int Fee_Type = UDFLib.ConvertToInteger(ddlFeeType.SelectedValue);
        int Approved_Status = UDFLib.ConvertToInteger(rdoApprovedStatus.SelectedValue);
        string Sign_On_From = txtSignOnFrom.Text;
        string Sign_On_To = txtSignOnTo.Text;
        string Approved_From = txtApprovedFrom.Text;
        string Approved_To = txtApprovedTo.Text;
        string SearchText = txtSearchText.Text;

        int PAGE_SIZE = ucCustomPager_CrewList.PageSize;
        int PAGE_INDEX = ucCustomPager_CrewList.CurrentPageIndex;
        int SelectRecordCount = ucCustomPager_CrewList.isCountRecord;
        decimal GrandTotal = 0;

        DataTable dt = BLL_Crew_Disbursement.Get_AllCrewFeeStatus(FleetCode, VesselID, ManningOfficeID, Rank_Category, GetSessionUserID(), Crew_Status, Fee_Type, Approved_Status, UDFLib.ConvertToDefaultDt(Sign_On_From), UDFLib.ConvertToDefaultDt(Sign_On_To), UDFLib.ConvertToDefaultDt(Approved_From), UDFLib.ConvertToDefaultDt(Approved_To), PAGE_SIZE, PAGE_INDEX, ref SelectRecordCount, ref GrandTotal, int.Parse(ddlMonth.SelectedValue), int.Parse(ddlYear.SelectedValue), SearchText);

        lblGrandTotal.Text = GrandTotal.ToString();

        if (ucCustomPager_CrewList.isCountRecord == 1)
        {
            ucCustomPager_CrewList.CountTotalRec = SelectRecordCount.ToString();
            ucCustomPager_CrewList.BuildPager();
        }

        GridView_Crew.DataSource = dt;
        GridView_Crew.DataBind();
    }

    protected void ImgExportToExcel_Click(object sender, EventArgs e)
    {
        try
        {
            int FleetCode = UDFLib.ConvertToInteger(ddlFleet.SelectedValue);
            int VesselID = UDFLib.ConvertToInteger(ddlVessel.SelectedValue);
            int ManningOfficeID = UDFLib.ConvertToInteger(ddlManningOffice.SelectedValue);
            int Rank_Category = UDFLib.ConvertToInteger(ddlRankCategory.SelectedValue);
            int Crew_Status = UDFLib.ConvertToInteger(ddlStatus.SelectedValue);
            int Fee_Type = UDFLib.ConvertToInteger(ddlFeeType.SelectedValue);
            int Approved_Status = UDFLib.ConvertToInteger(rdoApprovedStatus.SelectedValue);
            string Sign_On_From = txtSignOnFrom.Text;
            string Sign_On_To = txtSignOnTo.Text;
            string Approved_From = txtApprovedFrom.Text;
            string Approved_To = txtApprovedTo.Text;
            string SearchText = txtSearchText.Text;

            string[] HeaderCaptions = { "Manning Office", "Vessel", "S/Code", "Name", "Rank", "S/On Date", "S/Off Date", "Fee Type", "Due Date", "Due Amt", "Approved Amt", "Approved By", "Approved Date", "Remarks" };
            string[] DataColumnsName = { "manning_Office", "Vessel_Short_Name", "Staff_Code", "Staff_FullName", "Rank_Short_Name", "Sign_On_Date1", "Sign_Off_Date1", "FeeTypeName", "Due_Date1", "Due_Amount", "Approved_Amount", "ApprovedBy", "Approved_Date1", "Remarks" };


            int PAGE_SIZE = 0;
            int PAGE_INDEX = 0;
            int SelectRecordCount = 0;
            decimal GrandTotal = 0;

            DataTable dt = BLL_Crew_Disbursement.Get_AllCrewFeeStatus(FleetCode, VesselID, ManningOfficeID, Rank_Category, GetSessionUserID(), Crew_Status, Fee_Type, Approved_Status, UDFLib.ConvertToDefaultDt(Sign_On_From), UDFLib.ConvertToDefaultDt(Sign_On_To), UDFLib.ConvertToDefaultDt(Approved_From), UDFLib.ConvertToDefaultDt(Approved_To), UDFLib.ConvertIntegerToNull(PAGE_SIZE), UDFLib.ConvertIntegerToNull(PAGE_INDEX), ref SelectRecordCount, ref GrandTotal, int.Parse(ddlMonth.SelectedValue), int.Parse(ddlYear.SelectedValue), SearchText);

            DataTable dt1 = new DataTable();
            dt.Columns.Add("Sign_On_Date1", typeof(string));
            dt.Columns.Add("Sign_Off_Date1", typeof(string));
            dt.Columns.Add("Due_Date1", typeof(string));
            dt.Columns.Add("Approved_Date1", typeof(string));

            foreach (DataRow dr in dt.Rows)
            {
                dr["Sign_On_Date1"] = "&nbsp;" + (Convert.ToString(dr["Sign_On_Date"])) != "" ? UDFLib.ConvertUserDateFormat(Convert.ToString(dr["Sign_On_Date"])) : "";
                dr["Sign_Off_Date1"] = "&nbsp;" + (Convert.ToString(dr["Sign_Off_Date"])) != "" ? UDFLib.ConvertUserDateFormat(Convert.ToString(dr["Sign_Off_Date"])) : "";
                dr["Due_Date1"] = "&nbsp;" + (Convert.ToString(dr["Due_Date"])) != "" ? UDFLib.ConvertUserDateFormat(Convert.ToString((dr["Due_Date"]))) : "";
                dr["Approved_Date1"] = "&nbsp;" + (Convert.ToString(dr["Approved_Date"])) != "" ? UDFLib.ConvertUserDateFormat(Convert.ToString(dr["Approved_Date"])) : "";              
            }


            GridViewExportUtil.ExportToExcel(dt, HeaderCaptions, DataColumnsName, "AgencyFee.xls", "Agency Fee Export");
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }

    }

    protected void btnAddNew_Click(object sender, EventArgs e)
    {

    }

    protected void GridView_Crew_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "AddNew")
            {
                int ID = UDFLib.ConvertToInteger(e.CommandArgument.ToString());
                DataTable dt = BLL_Crew_Disbursement.Get_MODisbursementFeeDetails(ID, GetSessionUserID());
                if (dt.Rows.Count > 0)
                {
                    lblRank.Text = dt.Rows[0]["Rank_Short_Name"].ToString();
                    lblStaffCode.Text = dt.Rows[0]["Staff_Code"].ToString();
                    lblStaffName.Text = dt.Rows[0]["StaffName"].ToString();
                    lblManningOffice.Text = dt.Rows[0]["Company_Name"].ToString();
                    lblVessel.Text = dt.Rows[0]["Vessel_Name"].ToString();

                    Vessel_ID.Value = dt.Rows[0]["VesselID"].ToString();
                    CrewID.Value = dt.Rows[0]["CrewID"].ToString();
                    VoyageID.Value = dt.Rows[0]["VoyageID"].ToString();
                    ManningOfficeID.Value = dt.Rows[0]["ManningOfficeID"].ToString();


                    ddlMonth_AddNew.SelectedIndex = 0;
                    ddlYear_AddNew.SelectedIndex = 0;
                    txtAmount_AddNew.Text = "";
                    txtRemarks.Text = "";

                    string js = "openPopup_AddNewDisb();";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "showPopup", js, true);
                }

            }
        }
        catch { }
    }

    protected void btnSaveNew_Click(object sender, EventArgs e)
    {
        try
        {
            string Due_Date = ddlYear_AddNew.SelectedValue + "/" + ddlMonth_AddNew.SelectedValue + "/" + DateTime.DaysInMonth(UDFLib.ConvertToInteger(ddlYear_AddNew.SelectedValue), UDFLib.ConvertToInteger(ddlMonth_AddNew.SelectedValue)).ToString();
            decimal Amount = UDFLib.ConvertToDecimal(txtAmount_AddNew.Text);
            string Remarks = txtRemarks.Text;

            int Res = BLL_Crew_Disbursement.AddNew_ProcessingFee(UDFLib.ConvertToInteger(CrewID.Value), UDFLib.ConvertToInteger(Vessel_ID.Value), UDFLib.ConvertToInteger(VoyageID.Value), UDFLib.ConvertToInteger(ManningOfficeID.Value), UDFLib.ConvertToInteger(ddlFeeType_AddNew.SelectedValue), Amount, Due_Date, GetSessionUserID(), Remarks);

            if (Res == 1)
            {
                Load_CrewProcessingFee();
                string js = "alert('Fee added successfully.');hideModal('dvAddNewDisb');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg1", js, true);
            }
        }
        catch (Exception ex)
        {
            string js = "alert('" + ex.Message.Replace("'", "") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg2", js, true);
        }
    }

    protected Boolean ValidateDateFormat()
    {
        Boolean ret = true;
        string msg = "";
        if (txtSignOnFrom.Text.Trim() != "")
        {
            try
            {

                if (!UDFLib.DateCheck(txtSignOnFrom.Text))
                {
                    ret = false;
                    msg += "Enter Valid Sign On Date"+TodayDateFormat;
                }
                else
                {
                    DateTime dt = DateTime.Parse(UDFLib.ConvertToDefaultDt(txtSignOnFrom.Text.Trim()));
                }
            }
            catch
            {
                ret = false;
                msg += "Enter Valid Sign On Date" + TodayDateFormat;
            }
        }
        if (txtApprovedFrom.Text.Trim() != "")
        {
            try
            {
                if (!UDFLib.DateCheck(txtApprovedFrom.Text))
                {
                    ret = false;
                    msg += "Enter Valid Approved From Date" + TodayDateFormat;
                }
                else
                {
                    DateTime dt = DateTime.Parse(UDFLib.ConvertToDefaultDt(txtApprovedFrom.Text.Trim()));
                }
            }
            catch
            {
                ret = false;
                msg += "Enter Valid Approved From Date" + TodayDateFormat;
            }
        }
        if (txtSignOnTo.Text.Trim() != "")
        {
            try
            {
                if (!UDFLib.DateCheck(txtSignOnTo.Text))
                {
                    ret = false;
                    msg = "Enter Valid Sign On To Date" + TodayDateFormat;
                }
                else
                {
                    DateTime dt = DateTime.Parse(UDFLib.ConvertToDefaultDt(txtSignOnTo.Text.Trim()));
                }
            }
            catch
            {
                ret = false;
                msg = "Enter Valid Sign On To Date" + TodayDateFormat;
            }
        }

        if (txtApprovedTo.Text.Trim() != "")
        {
            try
            {
                if (!UDFLib.DateCheck(txtApprovedTo.Text))
                {
                    ret = false;
                    msg = "Enter Valid Approved To Date" + TodayDateFormat;
                }
                else
                {
                    DateTime dt = DateTime.Parse(UDFLib.ConvertToDefaultDt(txtApprovedTo.Text.Trim()));
                }
            }
            catch
            {
                ret = false;
                msg = "Enter Valid Approved To Date" + TodayDateFormat;
            }
        }
        
        if (msg != "")
        {
            string js = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msgValidation", js, true);
        }
        return ret;
    }


}

