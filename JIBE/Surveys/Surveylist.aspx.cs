using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;

using SMS.Business.Infrastructure;
using SMS.Business.Survey;
using SMS.Properties;
using SMS.Business;

public partial class Surveylist : System.Web.UI.Page
{
    ControlParameter pr = new ControlParameter();
    BLL_SURV_Survey objBLL = new BLL_SURV_Survey();
    BLL_Infra_VesselLib objBLLVessel = new BLL_Infra_VesselLib();
    BLL_Infra_UserCredentials objBLLUser = new BLL_Infra_UserCredentials();

    string DFormat = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (GetSessionUserID() == 0)
            Response.Redirect("~/account/login.aspx?ReturnUrl=" + Request.RawUrl.ToString());

        try
        {
            UserAccessValidation();
            if (Request.QueryString.Count != 0)
            {
                int Surv_Vessel_ID = 0;
                int Vessel_ID = 0;

                if (Request.QueryString["Surv_Vessel_ID"] != null)
                    Surv_Vessel_ID = Convert.ToInt16(Request.QueryString["Surv_Vessel_ID"].ToString());
                if (Request.QueryString["Vessel_ID"] != null)
                    Vessel_ID = Convert.ToInt16(Request.QueryString["Vessel_ID"].ToString());

                if (Request.QueryString["Method"] == "CheckPreviousCertificate")
                {
                    CheckPreviousCertificate(Vessel_ID, Surv_Vessel_ID);
                    return;
                }
            }

            if (!IsPostBack)
            {
                DFormat = UDFLib.GetDateFormat();
                Load_FleetList();
                ddlFleetList.SelectedValue = Session["USERFLEETID"].ToString();
                Load_VesselList();
                Load_MainCategoryList();
                Load_CategoryList();
                Load_CertificateList(0);
                calExpToDate.Format = calExpFromDate.Format = calIssueFromDate.Format = calIssueToDate.Format = DFormat;

                txtIssueFromDate.Text = UDFLib.ConvertUserDateFormat("01/01/1990");
                txtIssueToDate.Text = UDFLib.ConvertUserDateFormat(DateTime.Today.ToShortDateString());
                txtExpFromDate.Text = UDFLib.ConvertUserDateFormat("01/01/1990");
                txtExpToDate.Text = UDFLib.ConvertUserDateFormat(DateTime.Today.AddYears(15).ToShortDateString());

                txtExpFromDate.Enabled = false;
                txtExpToDate.Enabled = false;

                ucCustomPager.PageSize = 20;
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    private void CheckPreviousCertificate(int Vessel_ID, int Surv_Vessel_ID)
    {
        int Res = objBLL.Chk_PreviousCertificate(Vessel_ID, Surv_Vessel_ID);
        Response.Clear();
        Response.Write(Res.ToString());
        HttpContext.Current.ApplicationInstance.CompleteRequest();// Response.End();
    }
    protected void UserAccessValidation()
    {
        UserAccess objUA = new UserAccess();
        objUA = new BLL_Infra_UserCredentials().Get_UserAccessForPage(GetSessionUserID(), UDFLib.GetPageURL(Request.Path.ToUpper()));
        if (objUA.View == 0)
            Response.Redirect("~/default.aspx?msgid=1");

        if (objUA.Add == 0)
            grdSurveylist.Columns[grdSurveylist.Columns.Count - 1].Visible = false;
    }

    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }

    public void Load_FleetList()
    {
        try
        {
            int UserCompanyID = UDFLib.ConvertToInteger(Session["USERCOMPANYID"].ToString());
            ddlFleetList.DataSource = objBLLVessel.GetFleetList(UserCompanyID);
            ddlFleetList.DataTextField = "NAME";
            ddlFleetList.DataValueField = "CODE";
            ddlFleetList.DataBind();
            ddlFleetList.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    public void Load_VesselList()
    {
        try
        {
            int Fleet_ID = int.Parse(ddlFleetList.SelectedValue);
            int UserCompanyID = UDFLib.ConvertToInteger(Session["USERCOMPANYID"].ToString());
            int Vessel_Manager = UDFLib.ConvertToInteger(Session["USERCOMPANYID"].ToString());

            if (Session["UTYPE"].ToString() == "VESSEL MANAGER")
                Vessel_Manager = UserCompanyID;

            ddlVessels.DataSource = objBLLVessel.Get_VesselList(Fleet_ID, 0, Vessel_Manager, "", UserCompanyID);

            ddlVessels.DataTextField = "VESSEL_NAME";
            ddlVessels.DataValueField = "VESSEL_ID";
            ddlVessels.DataBind();
            ddlVessels.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));
            ddlVessels.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    public void Load_MainCategoryList()
    {
        try
        {
            DataTable dtMainCategory = objBLL.Get_Survey_MainCategoryList();
            ddlMainCategory.DataSource = dtMainCategory;
            ddlMainCategory.DataTextField = "Survey_Category";
            ddlMainCategory.DataValueField = "Id";
            ddlMainCategory.DataBind();
            ddlMainCategory.Items.Insert(0, new ListItem("-SELECT-", "0"));
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    public void Load_CategoryList()
    {
        try
        {
            ddlCategory.DataSource = objBLL.Get_Survey_CategoryList_ByMainCategoryId(int.Parse(ddlMainCategory.SelectedValue.ToString()));
            ddlCategory.DataTextField = "Survey_Category";
            ddlCategory.DataValueField = "ID";
            ddlCategory.DataBind();
            ddlCategory.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));
            ddlCategory.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    public void Load_CertificateList(int CategoryID)
    {
        try
        {
            ddlSurvey.DataSource = objBLL.Get_SurvayCertificate_List(CategoryID);
            ddlSurvey.DataTextField = "Survey_Cert_Name";
            ddlSurvey.DataValueField = "Surv_ID";
            ddlSurvey.DataBind();
            ddlSurvey.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));
            ddlSurvey.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    public void Load_CertificateList()
    {
        int CategoryID = -1;

        DataTable dtCatIDList = new DataTable();
        dtCatIDList.Columns.Add("ID", typeof(string));

        if (ddlCategory.Items[0].Selected == true)
        {
            CategoryID = 0;
        }
        else
        {
            foreach (ListItem li in ddlCategory.Items)
            {
                if (li.Selected == true)
                {
                    DataRow dr = dtCatIDList.NewRow();
                    dr[0] = li.Value;
                    dtCatIDList.Rows.Add(dr);
                }
            }
        }
        int FleetID = int.Parse(ddlFleetList.SelectedValue);
        int VesselID = int.Parse(ddlVessels.SelectedValue);


        ddlSurvey.DataSource = objBLL.Get_SurvayCertificate_List(dtCatIDList, CategoryID, FleetID, VesselID);
        ddlSurvey.DataTextField = "Survey_Cert_Name";
        ddlSurvey.DataValueField = "Surv_ID";
        ddlSurvey.DataBind();
        ddlSurvey.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));
        ddlSurvey.SelectedIndex = 0;
        //Filter_Grid();
    }
    protected void Filter_Grid()
    {
        try
        {
            string CategoryID_Multi = "";
            int CategoryID = -1;

            if (ddlCategory.Items[0].Selected == true)
            {
                CategoryID = 0;
            }
            else
            {
                foreach (ListItem li in ddlCategory.Items)
                {
                    if (li.Selected == true)
                    {
                        if (CategoryID_Multi != "")
                            CategoryID_Multi += ",";

                        CategoryID_Multi += li.Value;
                    }
                }
            }

            int FleetID = int.Parse(ddlFleetList.SelectedValue);
            int VesselID = int.Parse(ddlVessels.SelectedValue);
            int CertificateID = int.Parse(ddlSurvey.SelectedValue);

            string IssueFrom = txtIssueFromDate.Text;
            string IssueTo = txtIssueToDate.Text;
            string ExpFrom = txtExpFromDate.Text;
            string ExpTo = txtExpToDate.Text;

            int ExpiryInDays = int.Parse(rdoExpiringIn.SelectedValue);
            int Verified = int.Parse(rdoVerified.SelectedValue);

            string SearchText = txtSeachText.Text;

            int PAGE_SIZE = ucCustomPager.PageSize;
            int PAGE_INDEX = ucCustomPager.CurrentPageIndex;

            int SelectRecordCount = ucCustomPager.isCountRecord;


            int MainCategoryId = 0;
            if (CategoryID == 0 && CategoryID_Multi == "")
                MainCategoryId = int.Parse(ddlMainCategory.SelectedValue.ToString());

            bool CheckAll = false;
            if (rdoExpiringIn.SelectedValue == "-2")
                CheckAll = true;

            DataSet ds = objBLL.Get_SurvayList(FleetID, VesselID, MainCategoryId, CategoryID, CertificateID, IssueFrom, IssueTo, ExpFrom, ExpTo, ExpiryInDays, Verified, SearchText, CheckAll, CategoryID_Multi, PAGE_SIZE, PAGE_INDEX, SelectRecordCount);

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ucCustomPager.isCountRecord == 1)
                    {
                        ucCustomPager.CountTotalRec = ds.Tables[1].Rows[0][0].ToString();
                        ucCustomPager.BuildPager();
                    }

                    grdSurveylist.DataSource = ds.Tables[0];
                    grdSurveylist.DataBind();
                }
                else
                {
                    ucCustomPager.CountTotalRec = "0";
                    ucCustomPager.BuildPager();

                    grdSurveylist.DataSource = ds.Tables[0];
                    grdSurveylist.DataBind();
                }
            }
            else
            {
                grdSurveylist.DataSource = null;
                grdSurveylist.DataBind();
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    public string GetImage(string strLastModifyDate)
    {
        string strReturn = "~/images/transparentgif.gif";
        DateTime dt1 = new DateTime(1, 1, 1);
        int iDay = Convert.ToInt16(DateTime.Now.Day);

        dt1 = Convert.ToDateTime(DateTime.Now);
        int iDays = 0;
        if (Convert.ToString(strLastModifyDate) != "")
        {
            DateTime dtLastModifyDate = Convert.ToDateTime(strLastModifyDate);
            TimeSpan dtDifference = dt1 - dtLastModifyDate;
            iDays = dtDifference.Days;
            if (iDays <= 3 && iDay >= 0)
            {
                strReturn = "~/images/exclamation.gif";
            }
            else
            {
                strReturn = "~/images/transparentgif.gif";
            }
        }
        return strReturn;
    }
    protected void rdoExpiringIn_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdoExpiringIn.SelectedValue == "-2")
        {
            pnlExpDate.Enabled = false;
            pnlIssueDate.Enabled = false;
        }
        else
        {
            pnlExpDate.Enabled = true;
            pnlIssueDate.Enabled = true;
        }

        if (int.Parse(rdoExpiringIn.SelectedValue) == 0)
        {
            txtExpFromDate.Enabled = true;
            txtExpToDate.Enabled = true;

            string js = "var offset = new Object();offset.x=0;offset.y=75; inlineMsg('dvExpiryDates', 'Select Expiry Dates', 3,offset);";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", js, true);
        }
        else
        {
            txtExpFromDate.Enabled = false;
            txtExpToDate.Enabled = false;
        }
    }

    protected void ddlFleetList_SelectedIndexChanged(object sender, EventArgs e)
    {
        Load_VesselList();
    }
    protected void ddlVessels_SelectedIndexChanged(object sender, EventArgs e)
    {
    }
    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        Load_CertificateList();
    }
    protected void ddlMainCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        Load_CategoryList();
    }

    private string GetSortDirection(string column)
    {
        // By default, set the sort direction to ascending.
        string sortDirection = "ASC";

        // Retrieve the last column that was sorted.
        string sortExpression = ViewState["SortExpression"] as string;

        if (sortExpression != null)
        {
            // Check if the same column is being sorted.
            // Otherwise, the default value can be returned.
            if (sortExpression == column)
            {
                string lastDirection = ViewState["SortDirection"] as string;
                if ((lastDirection != null) && (lastDirection == "ASC"))
                {
                    sortDirection = "DESC";
                }
            }
        }

        // Save new values in ViewState.
        ViewState["SortDirection"] = sortDirection;
        ViewState["SortExpression"] = column;

        return sortDirection;
    }
    protected void grdSurveylist_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {
            //Retrieve the table from the session object.
            DataTable dt = Session["TaskTable"] as DataTable;
            if (dt != null)
            {
                //Sort the data.
                dt.DefaultView.Sort = e.SortExpression + " " + GetSortDirection(e.SortExpression);
                grdSurveylist.DataSource = Session["TaskTable"];
                grdSurveylist.DataBind();
            }
        }
        catch (Exception ex)
        {
        }
    }
    protected void grdSurveylist_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.ToUpper() == "VIEWDETAILS")
        {
            Int32 i32JobID = Convert.ToInt32(e.CommandArgument);
            Response.Redirect("ViewJob.aspx?JID=" + i32JobID);
        }
        else if (e.CommandName.ToUpper() == "EDITJOB")
        {
            Int32 i32JobID = Convert.ToInt32(e.CommandArgument);
            Response.Redirect("addnewjob.aspx?JID=" + i32JobID);
        }
    }
    protected void grdSurveylist_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdSurveylist.PageIndex = e.NewPageIndex;
        Filter_Grid();
    }
    protected string CalculatedExpiryDate(string DateOfExpiry, string ExtensionDate, int GraceRange)
    {
        string RangeMax = "", MaxDate = "";
        if (DateOfExpiry != "")
        {
            MaxDate = DateOfExpiry;
            RangeMax = UDFLib.ConvertToDate(DateOfExpiry).AddDays((GraceRange * 30 / 2)).ToString(UDFLib.GetDateFormat());
            if (GraceRange > 0 && RangeMax != "")
            {
                MaxDate = UDFLib.ConvertToDate(MaxDate) > UDFLib.ConvertToDate(RangeMax) ? MaxDate : RangeMax;
            }
            if (ExtensionDate != "")
            {
                MaxDate = UDFLib.ConvertToDate(MaxDate) > UDFLib.ConvertToDate(ExtensionDate) ? MaxDate : ExtensionDate;
            }
        }
        return MaxDate;
    }
    protected void grdSurveylist_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {


            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string AttachmentCount = DataBinder.Eval(e.Row.DataItem, "AttachmentCount").ToString();
                string Surv_Details_ID = DataBinder.Eval(e.Row.DataItem, "Surv_Details_ID").ToString();
                string Vessel_ID = DataBinder.Eval(e.Row.DataItem, "Vessel_ID").ToString();
                string Surv_Vessel_ID = DataBinder.Eval(e.Row.DataItem, "Surv_Vessel_ID").ToString();
                string Surv_ID = DataBinder.Eval(e.Row.DataItem, "Surv_ID").ToString();
                string OfficeID = DataBinder.Eval(e.Row.DataItem, "OfficeID").ToString();

                string Verified = DataBinder.Eval(e.Row.DataItem, "Verified").ToString();

                string DateOfIssue = UDFLib.ConvertUserDateFormat(DataBinder.Eval(e.Row.DataItem, "DateOfIssue").ToString());
                string DateOfExpiry = UDFLib.ConvertUserDateFormat(DataBinder.Eval(e.Row.DataItem, "DateOfExpiry").ToString());
                string NoExpiry = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "NAExpiryDate"));
                string ExtensionDate = UDFLib.ConvertUserDateFormat(DataBinder.Eval(e.Row.DataItem, "ExtensionDate").ToString());
                string FollowupReminderDt = UDFLib.ConvertUserDateFormat(DataBinder.Eval(e.Row.DataItem, "FollowupReminderDt").ToString());


                
                string GraceDateRange = DataBinder.Eval(e.Row.DataItem, "GraceRange").ToString();
                int GraceRange = 0;
                if (GraceDateRange != "")
                    GraceRange = UDFLib.ConvertToInteger(GraceDateRange);
                string MaxDateRange = "";
                if (!DateOfExpiry.Equals(""))
                {
                    MaxDateRange = UDFLib.ConvertToDate(DateOfExpiry).AddDays((GraceRange * 30 / 2)).ToString(UDFLib.GetDateFormat());
                }
                string sCalculatedExpiryDate = CalculatedExpiryDate(DateOfExpiry, ExtensionDate, GraceRange);
                for (int ix = 0; ix < e.Row.Cells.Count - 3; ix++)
                {
                    e.Row.Cells[ix].Attributes.Add("onclick", "showDialog('SurveyDetails.aspx?vid=" + Vessel_ID + "&s_v_id=" + Surv_Vessel_ID + "&s_d_id=" + Surv_Details_ID + "&off_id=" + OfficeID + "');");
                }
                if (DateOfIssue != "")
                {
                    if (UDFLib.ConvertToDate(DateOfIssue) > DateTime.Today.AddDays(-30) && UDFLib.ConvertToDate(DateOfIssue) < DateTime.Today)
                        e.Row.Cells[6].CssClass = "Done30";
                }
                if (NoExpiry == "0" && sCalculatedExpiryDate != "")
                {
                    if (sCalculatedExpiryDate != "")
                    {
                        if (UDFLib.ConvertToDate(sCalculatedExpiryDate) < DateTime.Today.AddDays(1))
                            e.Row.Cells[10].CssClass = "Overdue";
                        else if (UDFLib.ConvertToDate(sCalculatedExpiryDate) < DateTime.Today.AddDays(90) && UDFLib.ConvertToDate(sCalculatedExpiryDate) > DateTime.Today.AddDays(30))
                            e.Row.Cells[10].CssClass = "Due30-90";
                        else if (UDFLib.ConvertToDate(sCalculatedExpiryDate) <= DateTime.Today.AddDays(30))
                            e.Row.Cells[10].CssClass = "Due0-30";
                    }
                }
                else if (NoExpiry == "-1")
                {
                    Label lbl = (Label)e.Row.FindControl("lblDateOfExpiry");
                    if (lbl != null)
                    {
                        lbl.Text = "N/A";
                        e.Row.Cells[10].CssClass = "NAExpiry";
                    }
                }

                //Reminder Date Color
                if (FollowupReminderDt != "")
                {
                    if (UDFLib.ConvertToDate(FollowupReminderDt) < DateTime.Today.AddDays(1))
                        e.Row.Cells[11].CssClass = "Overdue"; //"Overdue-Reminder";
                    else if (UDFLib.ConvertToDate(FollowupReminderDt) < DateTime.Today.AddDays(90) && UDFLib.ConvertToDate(FollowupReminderDt) > DateTime.Today.AddDays(30))
                        e.Row.Cells[11].CssClass = "Due30-90";
                    else if (UDFLib.ConvertToDate(FollowupReminderDt) < DateTime.Today.AddDays(30))
                        e.Row.Cells[11].CssClass = "Due0-30";
                    Label lblFollowupReminderDt = (Label)(e.Row.FindControl("lblFollowupReminderDt"));
                    lblFollowupReminderDt.Attributes.Add("Title", "cssbody=[dvbdy1] cssheader=[dvhdr1] header=[Remarks] body=[" + DataBinder.Eval(e.Row.DataItem, "FollowupReminder").ToString().Replace("\n", "<br>") + "]");
                }


                Image ImgAttachment = (Image)(e.Row.FindControl("ImgAttachment"));
                if (ImgAttachment != null)
                {
                    if (AttachmentCount == "0")
                        ImgAttachment.Visible = false;
                    else
                        ImgAttachment.Attributes.Add("onclick", "showDialog('Attachments.aspx?vid=" + Vessel_ID + "&s_v_id=" + Surv_Vessel_ID + "&s_d_id=" + Surv_Details_ID + "&off_id=" + OfficeID + "');");
                }
                Image ImgVerified = (Image)(e.Row.FindControl("ImgVerified"));
                if (ImgVerified != null)
                {
                    if (Verified == "0")
                        ImgVerified.Visible = false;
                }

                e.Row.Attributes["onmouseover"] = "this.style.cursor='hand';this.style.backgroundColor='#FEECEC';";
                e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';this.style.color='black';this.style.backgroundColor=''";
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        ucCustomPager.isCountRecord = 1;
        Filter_Grid();
    }

    protected void btnClearSearch_Click(object sender, EventArgs e)
    {
        try
        {


            ddlFleetList.SelectedValue = "0";
            Load_VesselList();

            ddlVessels.ClearSelection();
            ddlCategory.ClearSelection();
            ddlSurvey.ClearSelection();

            ddlMainCategory.ClearSelection();
            Load_CategoryList();

            txtIssueFromDate.Text = UDFLib.ConvertUserDateFormat("01/01/1990");
            txtIssueToDate.Text = UDFLib.ConvertUserDateFormat(DateTime.Today.ToShortDateString());
            txtExpFromDate.Text = UDFLib.ConvertUserDateFormat("01/01/1990");
            txtExpToDate.Text = UDFLib.ConvertUserDateFormat(DateTime.Today.AddYears(15).ToShortDateString());

            txtExpFromDate.Enabled = false;
            txtExpToDate.Enabled = false;

            rdoVerified.SelectedIndex = 0;
            rdoExpiringIn.SelectedIndex = 2;
            pnlExpDate.Enabled = true;
            pnlExpIn.Enabled = true;
            pnlIssueDate.Enabled = true;

            txtSeachText.Text = "";

            Filter_Grid();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        Filter_Grid();
    }

    protected void btnShowNASurveyList_Click(object sender, EventArgs e)
    {
        string Vessel_ID = ddlVessels.SelectedValue;
        string Surv_Vessel_ID = ddlSurvey.SelectedValue;
        string Cat_ID = "";


        if (ddlCategory.Items[0].Selected == true)
        {
            Cat_ID = "0";
        }
        else
        {
            foreach (ListItem li in ddlCategory.Items)
            {
                if (li.Selected)
                {
                    if (Cat_ID.Length > 0)
                        Cat_ID += ",";

                    Cat_ID += li.Value;
                }
            }
        }
        string js = "window.open('NASurveyList.aspx?vid=" + Vessel_ID + "&s_v_id=" + Surv_Vessel_ID + "&cat_id=" + Cat_ID + "');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "openwindow", js, true);
    }

    protected void ImgExportToExcel_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            string CategoryID_Multi = "";
            int CategoryID = -1;

            if (ddlCategory.Items[0].Selected == true)
            {
                CategoryID = 0;
            }
            else
            {
                foreach (ListItem li in ddlCategory.Items)
                {
                    if (li.Selected == true)
                    {
                        if (CategoryID_Multi != "")
                            CategoryID_Multi += ",";

                        CategoryID_Multi += li.Value;
                    }
                }
            }

            int FleetID = int.Parse(ddlFleetList.SelectedValue);
            int VesselID = int.Parse(ddlVessels.SelectedValue);
            int CertificateID = int.Parse(ddlSurvey.SelectedValue);

            string IssueFrom = txtIssueFromDate.Text;
            string IssueTo = txtIssueToDate.Text;
            string ExpFrom = txtExpFromDate.Text;
            string ExpTo = txtExpToDate.Text;

            int ExpiryInDays = int.Parse(rdoExpiringIn.SelectedValue);
            int Verified = int.Parse(rdoVerified.SelectedValue);
            string SearchText = txtSeachText.Text;
            int MainCategoryId = 0;
            if (CategoryID == 0 && CategoryID_Multi == "")
                MainCategoryId = int.Parse(ddlMainCategory.SelectedValue.ToString());

            bool CheckAll = false;
            if (rdoExpiringIn.SelectedValue == "-2")
            {
                CheckAll = true;
                ExpiryInDays = 60;
            }

            DataTable ds = objBLL.Get_SurvayList(FleetID, VesselID, MainCategoryId, CategoryID, CertificateID, IssueFrom, IssueTo, ExpFrom, ExpTo, ExpiryInDays, Verified, SearchText, CheckAll, CategoryID_Multi, null, null, 0).Tables[0];

            string[] strHeaderCaptions = { "Vessel", "Main Category", "Sub Category", "Survey/Certificate Name", "Remarks", "Make/Model", "Issue Date", "Expiry Date", "Range(Months)", "Extension Date", "Calculated Expiry Date", "Reminder", "Reminder Remarks" };
            string[] strDataColNames = { "Vessel_Name", "Survey_MainCategory", "Survey_Category", "Survey_Cert_Name", "Survey_Cert_remarks", "EquipmentType", "DateOfIssue", "DateOfExpiry", "GraceRange", "ExtensionDate", "CALCULATED_DATEOFEXPIRY", "FollowupReminderDt", "FollowupReminder" };
            string FileName = "SurveyList";
            string Title = "Survey List";

            ChangeColumnDataType(ds, "DateOfIssue", typeof(string));
            ChangeColumnDataType(ds, "DateOfExpiry", typeof(string));
            ChangeColumnDataType(ds, "FollowupReminderDt", typeof(string));
            ChangeColumnDataType(ds, "ExtensionDate", typeof(string));
            ChangeColumnDataType(ds, "CALCULATED_DATEOFEXPIRY", typeof(string));
            foreach (DataRow item in ds.Rows)
            {
                if (!string.IsNullOrEmpty(item["DateOfIssue"].ToString()))
                    item["DateOfIssue"] = "&nbsp;" + UDFLib.ConvertUserDateFormat(Convert.ToString(item["DateOfIssue"]), UDFLib.GetDateFormat());
                if (!string.IsNullOrEmpty(item["DateOfExpiry"].ToString()))
                    item["DateOfExpiry"] = "&nbsp;" + UDFLib.ConvertUserDateFormat(Convert.ToString(item["DateOfExpiry"]), UDFLib.GetDateFormat());
                if (!string.IsNullOrEmpty(item["FollowupReminderDt"].ToString()))
                    item["FollowupReminderDt"] = "&nbsp;" + UDFLib.ConvertUserDateFormat(Convert.ToString(item["FollowupReminderDt"]), UDFLib.GetDateFormat());
                if (!string.IsNullOrEmpty(item["ExtensionDate"].ToString()))
                    item["ExtensionDate"] = "&nbsp;" + UDFLib.ConvertUserDateFormat(Convert.ToString(item["ExtensionDate"]), UDFLib.GetDateFormat());
                if (!string.IsNullOrEmpty(item["CALCULATED_DATEOFEXPIRY"].ToString()))
                    item["CALCULATED_DATEOFEXPIRY"] = "&nbsp;" + UDFLib.ConvertUserDateFormat(Convert.ToString(item["CALCULATED_DATEOFEXPIRY"]), UDFLib.GetDateFormat());
            }
            GridViewExportUtil.ShowExcel(ds, strHeaderCaptions, strDataColNames, FileName, Title);
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }


    public static bool ChangeColumnDataType(DataTable table, string columnname, Type newtype)
    {
        if (table.Columns.Contains(columnname) == false)
            return false;

        DataColumn column = table.Columns[columnname];
        if (column.DataType == newtype)
            return true;

        try
        {
            DataColumn newcolumn = new DataColumn("temporary", newtype);
            table.Columns.Add(newcolumn);
            foreach (DataRow row in table.Rows)
            {
                try
                {
                    row["temporary"] = Convert.ChangeType(row[columnname], newtype);
                }
                catch
                {
                }
            }
            table.Columns.Remove(columnname);
            newcolumn.ColumnName = columnname;
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            return false;
        }

        return true;
    }
}