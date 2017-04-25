
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;

using SMS.Business.Infrastructure;
using SMS.Business.Technical;
using SMS.Business.Crew;
using SMS.Properties;
using System.Configuration;
using System.IO;
using DocumentFormat.OpenXml.Spreadsheet;
using SpreadsheetLight;
using System.Net;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
public partial class Technical_Worklist : System.Web.UI.Page
{
    ControlParameter pr = new ControlParameter();
    BLL_Tec_Worklist objBLL = new BLL_Tec_Worklist();
    BLL_Infra_VesselLib objBLLVessel = new BLL_Infra_VesselLib();
    BLL_Infra_UserCredentials objBLLUser = new BLL_Infra_UserCredentials();
    BLL_Crew_CrewDetails objCrew = new BLL_Crew_CrewDetails();

    UserAccess objUA = new UserAccess();

    protected void Page_Load(object sender, EventArgs e)
    {
        string toDate = "";//Convert.ToString(DateTime.Now.ToString("dd/MM/yyyy"));
        string fromDate = "";// "01/01/2010"; //Convert.ToString(DateTime.Now.AddMonths(-6).ToString("dd/MM/yyyy"));

        txtFollowupDate.Text = Convert.ToString(DateTime.Now.ToString("dd/MM/yyyy"));

        ViewState["VesselId"] = null;
        ViewState["SchDetailId"] = null;
        ViewState["ActualDate"] = null;
        ViewState["NCRFlag"] = null;

        if (Session["USERID"] == null)
            Response.Redirect("~/account/login.aspx");

        if (!IsPostBack == true)
        {
            ViewState["SORTDIRECTION"] = null;
            ViewState["SORTBYCOLOUMN"] = null;
            if (Session["COMPANYTYPE"] != null && Session["COMPANYTYPE"].ToString().ToUpper() == "SURVEYOR")
            {
                lblFleet.Text = "Company :";
                ddlFleet.Visible = false;
                ddlVessel_Manager.Visible = true;
            }
            UserAccessValidation();
            txtFromDate.Text = fromDate;
            txtToDate.Text = toDate;
            //Coming from WORKLIST --> Quality
            if (Request.QueryString["NCR"] != null)
            {
                rblJobType.SelectedValue = "NCR";
                //rblJobType.Items[1].Selected = true;
            }
            if (Request.QueryString["Job_card_No"] != null)
            {
                txtDescription.Text = Request.QueryString["Job_card_No"].ToString();
                //rblJobType.Items[1].Selected = true;
            }


            if (Request.QueryString["VesselID"] != null)
            {
                ViewState["VesselId"] = Request.QueryString["VesselID"];
                //rblJobType.Items[1].Selected = true;
            }

            if (Request.QueryString["SchDetailId"] != null)
            {
                ViewState["SchDetailId"] = Request.QueryString["SchDetailId"];
                //rblJobType.Items[1].Selected = true;

                DataTable dtname = objBLL.Get_InspectorName_ByInspectionID(Convert.ToInt32(ViewState["SchDetailId"].ToString()));
                if (dtname.Rows.Count > 0)
                {
                    lblInspectorNameValue.Text = dtname.Rows[0][0].ToString();
                }

                lblInspectorNameValue.Visible = true;
                lblInspectionDateValue.Visible = true;
                lblInspectorName.Visible = true;
                lblInspectionDate.Visible = true;
                tblInspectionDetail.Attributes.Add("bgcolor", "yellow");
            }
            else
            {
                lblInspectorNameValue.Visible = false;
                lblInspectionDateValue.Visible = false;
                lblInspectorName.Visible = false;
                lblInspectionDate.Visible = false;
            }

            if (Request.QueryString["ActualDate"] != null)
            {
                ViewState["ActualDate"] = Request.QueryString["ActualDate"];
                //rblJobType.Items[1].Selected = true;

                lblInspectionDateValue.Text = ViewState["ActualDate"].ToString();
            }

            if (Request.QueryString["NCRFlag"] != null)
            {
                ViewState["NCRFlag"] = Request.QueryString["NCRFlag"];
                //rblJobType.SelectedValue = ViewState["NCRFlag"];
                //ViewState["NCRFlag"] = Request.QueryString["NCRFlag"];
                //rblJobType.Items[1].Selected = true;
            }

            BindJobType();
            BindPIC();
            Load_FleetList();
            Load_VesselList();
            BindNature();
            BindNatureForDropDown();
            ddlfill();
            Load_InspectorList();
            CheckSettingforFunctions();
            Search_Worklist();


        }
        if (IsPostBack)
        {

            if (hdnFlagCheck.Value == "true")
            {
                string js = "divCategorylink()";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "showDiv", js, true);
            }
        }
        string jsv = "VesselChange();";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "VesselChange", jsv, true);
    }
    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);


        if (objUA.View == 0)
            Response.Redirect("~/default.aspx?msgid=1");

        if (objUA.Add == 0)
        {
            ImgBtnAddNewJob.Visible = false;
            grdJoblist.Columns[grdJoblist.Columns.Count - 1].Visible = false;
        }
        if (objUA.Edit == 0)
        {
            grdJoblist.Columns[grdJoblist.Columns.Count - 3].Visible = false;
        }
        if (objUA.Delete == 0)
        {

        }
        if (objUA.Approve == 0)
        {

        }
        if (objUA.Admin == 1)
        {
            ViewState["WLADMIN"] = 1;
        }
        else
        {
            ViewState["WLADMIN"] = 0;
        }

    }

    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }

    protected void BindNatureForDropDown()
    {
        try
        {

            ddlNature.DataTextField = "Name";
            ddlNature.DataValueField = "Code";
            ddlNature.DataSource = objBLL.GetAllNature();
            ddlNature.DataBind();

            ddlNature.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));
            ddlPrimary.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));
            ddlSecondary.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));

            if (hdnNature.Value == "0")
            {
                ddlNature.SelectedValue = Convert.ToString(0);
            }
            else
            {
                ddlNature.SelectedValue = Convert.ToString(hdnNature.Value);
                BindPrimaryByNatureID(Convert.ToInt32(hdnNature.Value));
            }

        }
        catch (Exception)
        {
            ////.WriteError(this.GetType().Name.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), ex);
        }
    }

    protected void ddlfill()
    {
        // //.WriteError(this.GetType().Name.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), ex);
        try
        {

            ddldeptShip.DataTextField = "Value";
            ddldeptShip.DataValueField = "ID";
            ddldeptShip.DataSource = objBLL.Get_Dept_OnShip();
            ddldeptShip.DataBind();
            ddldeptShip.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));

            ddlOffice.DataTextField = "Value";
            ddlOffice.DataValueField = "ID";
            ddlOffice.DataSource = objBLL.Get_Dept_InOffice();
            ddlOffice.DataBind();
            ddlOffice.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));

            ddlPriority.DataTextField = "Value";
            ddlPriority.DataValueField = "ID";
            ddlPriority.DataSource = objBLL.Get_JobPriority();
            ddlPriority.DataBind();
            ddlPriority.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));

            ddlAssignor.DataTextField = "Value";
            ddlAssignor.DataValueField = "ID";
            ddlAssignor.DataSource = objBLL.Get_Assigner();
            ddlAssignor.DataBind();
            ddlAssignor.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));


            //ddlReports.Items.Insert(0, new ListItem("-Select-", "0"));
        }
        catch (Exception)
        {
            ////.WriteError(this.GetType().Name.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), ex);
        }
    }

    protected void Search_Worklist()
    {
        try
        {
            string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
            string sortdirection = null;
            if (ViewState["SORTDIRECTION"] != null)
                sortdirection = ViewState["SORTDIRECTION"].ToString();

            string jsc = "DivClicked(-1);";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "clear", jsc, true);
            if (hf1.Value != "")
            {
                LoadWorklistCutom(Convert.ToInt32(hf1.Value));
                return;
            }
            DataTable dtStatus = new DataTable();
            dtStatus.Columns.Add("All", typeof(int));
            dtStatus.Columns.Add("Pending", typeof(int));
            dtStatus.Columns.Add("Completed", typeof(int));
            dtStatus.Columns.Add("Reworked", typeof(int));
            dtStatus.Columns.Add("Verified", typeof(int));
            dtStatus.Columns.Add("Overdue", typeof(int));

            //string JobStaus ="";

            dtStatus.Rows.Add(rblJobStaus.Items[0].Selected == true ? 1 : 0,
                rblJobStaus.Items[1].Selected == true ? 1 : 0,
                rblJobStaus.Items[2].Selected == true ? 1 : 0,
                rblJobStaus.Items[3].Selected == true ? 1 : 0,
                rblJobStaus.Items[4].Selected == true ? 1 : 0,
                rblJobStaus.Items[5].Selected == true ? 1 : 0);

            //for (int i = 0; i < rblJobStaus.Items.Count; i++)
            //{
            //    if (rblJobStaus.Items[i].Selected == true)
            //    {
            //        JobStaus += "''" + rblJobStaus.Items[i].Value + "'',";
            //    }
            //}
            //JobStaus += "''''" ;
            DataTable dtFilter = new DataTable();
            dtFilter.Columns.Add("PRM_NAME", typeof(string));
            dtFilter.Columns.Add("PRM_VALUE", typeof(object));

            dtFilter.Rows.Add(new object[] { "@FLEET_ID", UDFLib.ConvertIntegerToNull(ddlFleet.SelectedValue) });
            dtFilter.Rows.Add(new object[] { "@VESSEL_ID", UDFLib.ConvertIntegerToNull(ddlVessels.SelectedValue) });
            dtFilter.Rows.Add(new object[] { "@ASSIGNOR", UDFLib.ConvertIntegerToNull(ddlAssignor.SelectedValue) });
            dtFilter.Rows.Add(new object[] { "@DEPT_SHIP", UDFLib.ConvertIntegerToNull(ddldeptShip.SelectedValue) });
            dtFilter.Rows.Add(new object[] { "@DEPT_OFFICE", UDFLib.ConvertIntegerToNull(ddlOffice.SelectedValue) });
            dtFilter.Rows.Add(new object[] { "@PRIORITY", UDFLib.ConvertIntegerToNull(ddlPriority.SelectedValue) });
            dtFilter.Rows.Add(new object[] { "@CATEGORY_NATURE", UDFLib.ConvertIntegerToNull(ddlNature.SelectedValue) });
            dtFilter.Rows.Add(new object[] { "@CATEGORY_PRIMARY", UDFLib.ConvertIntegerToNull(ddlPrimary.SelectedValue) });
            dtFilter.Rows.Add(new object[] { "@CATEGORY_SECONDARY", UDFLib.ConvertIntegerToNull(ddlSecondary.SelectedValue) });
            dtFilter.Rows.Add(new object[] { "@CATEGORY_MINOR", null, });
            dtFilter.Rows.Add(new object[] { "@JOB_DESCRIPTION", UDFLib.ConvertStringToNull(txtDescription.Text) });
            dtFilter.Rows.Add(new object[] { "@JOB_STATUS", UDFLib.ConvertStringToNull(rblJobStaus.SelectedValue) });

            dtFilter.Rows.Add(new object[] { "@dtJOB_Status", dtStatus });

            dtFilter.Rows.Add(new object[] { "@JOB_TYPE", 0 });
            dtFilter.Rows.Add(new object[] { "@PIC", UDFLib.ConvertIntegerToNull(ddlPIC.SelectedValue) });
            dtFilter.Rows.Add(new object[] { "@JOB_MODIFIED_IN", UDFLib.ConvertIntegerToNull(txtModifiedInDays.Text) });
            dtFilter.Rows.Add(new object[] { "@DATE_RAISED_FROM", UDFLib.ConvertDateToNull(txtFromDate.Text) });
            dtFilter.Rows.Add(new object[] { "@DATE_RAISED_TO", UDFLib.ConvertDateToNull(txtToDate.Text) });
            dtFilter.Rows.Add(new object[] { "@DATE_CMPLTN_FROM", UDFLib.ConvertDateToNull(txtExpectedCompFrom.Text) });
            dtFilter.Rows.Add(new object[] { "@DATE_CMPLTN_TO", UDFLib.ConvertDateToNull(txtExpectedCompTo.Text) });
            dtFilter.Rows.Add(new object[] { "@DEFER_TO_DD", (chkDrydock.Checked == true ? UDFLib.ConvertIntegerToNull(1) : null) });
            dtFilter.Rows.Add(new object[] { "@SENT_TO_SHIP", (chkSentToShip.Checked == true ? UDFLib.ConvertIntegerToNull(1) : null) });
            dtFilter.Rows.Add(new object[] { "@HAVING_REQ_NO", (chkRequisition.Checked == true ? UDFLib.ConvertIntegerToNull(1) : null) });
            dtFilter.Rows.Add(new object[] { "@FLAGGED_FOR_MEETING", (chkFlaggedJobs.Checked == true ? UDFLib.ConvertIntegerToNull(1) : null) });
            dtFilter.Rows.Add(new object[] { "@INSPECTOR", UDFLib.ConvertIntegerToNull(ddlInspector.SelectedValue) });
            dtFilter.Rows.Add(new object[] { "@PAGE_INDEX", ucCustomPagerctp.CurrentPageIndex });
            dtFilter.Rows.Add(new object[] { "@PAGE_SIZE", ucCustomPagerctp.PageSize });
            if (Session["COMPANYTYPE"].ToString().ToUpper() == "SURVEYOR" && ddlVessel_Manager.SelectedIndex > 0)
            {
                dtFilter.Rows.Add(new object[] { "@Company_ID", ddlVessel_Manager.SelectedValue });
            }
            dtFilter.Rows.Add(new object[] { "@WL_TYPE", rblJobType.SelectedValue });
            if (ViewState["SchDetailId"] != null)
            {
                dtFilter.Rows.Add(new object[] { "@InspectionID", Convert.ToInt32(ViewState["SchDetailId"].ToString()) });
            }
            //if (ViewState["ActualDate"] != null)
            //{
            //    dtFilter.Rows.Add(new object[] { "@ActualDate", Convert.ToDateTime(ViewState["ActualDate"].ToString()) });
            //}
            dtFilter.Rows.Add(new object[] { "@SortBy", sortbycoloumn });
            dtFilter.Rows.Add(new object[] { "@SORT_DIRECTION", sortdirection });
            int Record_Count = 0;

            DataTable taskTable = objBLL.Get_WorkList_Index(dtFilter, ref Record_Count);

            grdJoblist.DataSource = taskTable;
            grdJoblist.DataBind();

            ucCustomPagerctp.CountTotalRec = Record_Count.ToString();
            ucCustomPagerctp.BuildPager();

            DataTable dtPKIDs = taskTable.DefaultView.ToTable(true, new string[] { "WORKLIST_ID", "VESSEL_ID", "OFFICE_ID" });
            dtPKIDs.PrimaryKey = new DataColumn[] { dtPKIDs.Columns["WORKLIST_ID"], dtPKIDs.Columns["VESSEL_ID"], dtPKIDs.Columns["OFFICE_ID"] };
            Session["WORKLIST_PKID_NAV"] = dtPKIDs;

            lblRecordCount.Text = Record_Count.ToString();

            string js1 = "toggleSerachPostBack();restConditionCheckbox();";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "View", js1, true);
        }
        catch (Exception ex)
        {
            ////.WriteError(this.GetType().Name.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), ex);
            string js = "alert('Error in loading data!! Error: " + UDFLib.ReplaceSpecialCharacter(ex.Message) + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
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

    protected void ddlFleet_SelectedIndexChanged(object sender, EventArgs e)
    {
        Load_VesselList();
    }

    protected void ddlNature_SelectedIndexChanged(object sender, EventArgs e)
    {
        hdnFlagCheck.Value = "false";
        BindPrimaryByNatureID(Convert.ToInt32(ddlNature.SelectedValue));
        //Search_Worklist();
        string js1 = "toggleSerachPostBack();restConditionCheckbox();";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "View", js1, true);
    }

    protected void ddlPrimary_SelectedIndexChanged(object sender, EventArgs e)
    {
        hdnFlagCheck.Value = "false";
        BindSecondaryByPrimaryID(Convert.ToInt32(ddlPrimary.SelectedValue));
        //Search_Worklist();
        string js1 = "toggleSerachPostBack();restConditionCheckbox();";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "View", js1, true);
    }

    protected void ddlSecondary_SelectedIndexChanged(object sender, EventArgs e)
    {
        hdnFlagCheck.Value = "false";
        BindMinorBySecondatyID(Convert.ToInt32(ddlSecondary.SelectedValue));
        //Search_Worklist();
        string js1 = "toggleSerachPostBack();restConditionCheckbox();";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "View", js1, true);
    }

    public void Load_FleetList()
    {
        int UserCompanyID = UDFLib.ConvertToInteger(Session["USERCOMPANYID"].ToString());
        ddlFleet.DataSource = objBLLVessel.GetFleetList(UserCompanyID);
        ddlFleet.DataTextField = "NAME";
        ddlFleet.DataValueField = "CODE";
        ddlFleet.DataBind();
        ddlFleet.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));
        ddlFleet.SelectedValue = UDFLib.ConvertToInteger(Session["USERFLEETID"]).ToString();
        BLL_Infra_Company objInfra = new BLL_Infra_Company();
        ddlVessel_Manager.DataSource = objInfra.Get_Company_Parent_Child(1, 0, 0);
        ddlVessel_Manager.DataTextField = "COMPANY_NAME";
        ddlVessel_Manager.DataValueField = "ID";
        ddlVessel_Manager.DataBind();
        ddlVessel_Manager.Items.Insert(0, new ListItem("-Select All-", "0"));
    }
    public void Load_VesselList()
    {
        int Fleet_ID = int.Parse(ddlFleet.SelectedValue);
        int UserCompanyID = UDFLib.ConvertToInteger(Session["USERCOMPANYID"].ToString());
        int Vessel_Manager = 0;

        if (Session["COMPANYTYPE"] != null && Session["COMPANYTYPE"].ToString().ToUpper() == "SURVEYOR")
        {

            Vessel_Manager = Convert.ToInt32(ddlVessel_Manager.SelectedValue);
        }

        ddlVessels.DataSource = objBLLVessel.Get_VesselList(Fleet_ID, 0, Vessel_Manager, "", UserCompanyID);

        ddlVessels.DataTextField = "VESSEL_NAME";
        ddlVessels.DataValueField = "VESSEL_ID";
        ddlVessels.DataBind();
        ddlVessels.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));
        ddlVessels.SelectedIndex = 0;

        if (ViewState["VesselId"] != null)
        {
            ddlVessels.SelectedValue = ViewState["VesselId"].ToString();
        }
    }
    protected void BindPIC()
    {
        try
        {
            int iCompID = int.Parse(Session["USERCOMPANYID"].ToString());

            ddlPIC.DataSource = objBLLUser.Get_UserList(iCompID);
            ddlPIC.DataTextField = "UserName";
            ddlPIC.DataValueField = "UserID";
            ddlPIC.DataBind();
            ddlPIC.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));
        }
        catch (Exception)
        {
            ////.WriteError(this.GetType().Name.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), ex);
        }
    }
    protected void Load_InspectorList()
    {
        ddlInspector.DataSource = objBLL.Get_InspectorList();
        ddlInspector.DataTextField = "UserName";
        ddlInspector.DataValueField = "UserID";
        ddlInspector.DataBind();
        ddlInspector.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));

    }

    protected void BindPrimaryByNatureID(Int32 i32NatureID)
    {
        try
        {

            ddlSecondary.Items.Clear();
            ddlPrimary.DataTextField = "Name";
            ddlPrimary.DataValueField = "Code";
            ddlPrimary.DataSource = objBLL.GetPrimaryByNatureID(i32NatureID);
            ddlPrimary.DataBind();
            ddlPrimary.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));
            ddlSecondary.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));

            if (hdnPrimary.Value == "0")
            {
                ddlPrimary.SelectedValue = Convert.ToString(0);
            }
            else
            {
                ddlPrimary.SelectedValue = Convert.ToString(hdnPrimary.Value);
                BindSecondaryByPrimaryID(Convert.ToInt32(hdnPrimary.Value));

            }
        }
        catch (Exception)
        {
            ////.WriteError(this.GetType().Name.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), ex);
        }
    }
    protected void BindSecondaryByPrimaryID(Int32 i32PrimaryID)
    {
        try
        {

            //ddlSecondary.Enabled = true;
            ddlSecondary.Items.Clear();
            ddlSecondary.DataTextField = "Name";
            ddlSecondary.DataValueField = "Code";
            ddlSecondary.DataSource = objBLL.GetSecondaryByPrimaryID(i32PrimaryID);
            ddlSecondary.DataBind();

            ddlSecondary.Items.Insert(0, new ListItem("-Select-", "0"));

            if (hdnSecondary.Value == "0")
            {
                ddlSecondary.SelectedValue = Convert.ToString(0);
            }
            else
            {
                ddlSecondary.SelectedValue = Convert.ToString(hdnSecondary.Value);
            }

        }
        catch (Exception)
        {
            ////.WriteError(this.GetType().Name.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), ex);
        }
    }
    protected void BindMinorBySecondatyID(Int32 i32SecondaryID)
    {
        try
        {
            ddlMinor.Items.Clear();
            ddlMinor.DataTextField = "Name";
            ddlMinor.DataValueField = "Code";
            ddlMinor.DataSource = objBLL.GetMinorBySecondaryID(i32SecondaryID);
            ddlMinor.DataBind();

            ddlMinor.Items.Insert(0, new ListItem("-Select-", "0"));

            if (hdnMinor.Value == "0")
            {
                ddlMinor.SelectedValue = Convert.ToString(0);
            }
            else
            {
                ddlMinor.SelectedValue = Convert.ToString(hdnMinor.Value);
            }

        }
        catch (Exception)
        {
            ////.WriteError(this.GetType().Name.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), ex);
        }
    }

    protected void grdJoblist_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {
            //hdnFlagCheck.Value = "false";
            ////Retrieve the table from the session object.
            //DataTable dt = Session["TaskTable"] as DataTable;
            //if (dt != null)
            //{
            //    //Sort the data.
            //    dt.DefaultView.Sort = e.SortExpression + " " + GetSortDirection(e.SortExpression);
            //    grdJoblist.DataSource = Session["TaskTable"];
            //    grdJoblist.DataBind();
            //}
            ViewState["SORTBYCOLOUMN"] = e.SortExpression;
            if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "1")
                ViewState["SORTDIRECTION"] = 0;
            else
                ViewState["SORTDIRECTION"] = 1;
            Search_Worklist();
        }
        catch (Exception)
        {
            ////.WriteError(this.GetType().Name.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), ex);
        }
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
    private void BindNature()
    {
        try
        {
            DataSet dts = objBLL.GetAllNature();
            if (dts.Tables[0].Rows.Count > 0)
            {
                // Bind the Nature List Box
                lbNature.DataSource = dts;
                lbNature.DataTextField = "Name";
                lbNature.DataValueField = "Code";
                lbNature.DataBind();
            }

        }
        catch (Exception ex)
        {
            string js = "alert('" + ex.Message.Replace("'", "") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }
    private void BindPrimary(int intNature)
    {
        try
        {

            // Bind the Nature List Box
            DataSet dts = objBLL.GetPrimaryByNatureID(intNature);
            if (dts.Tables[0].Rows.Count > 0)
            {
                lbPrimary.DataSource = dts;
                lbPrimary.DataTextField = "Name";
                lbPrimary.DataValueField = "Code";
                lbPrimary.DataBind();
                lbPrimary.SelectedIndex = 0;

                txtNature.Text = Convert.ToString(lbNature.SelectedItem.Text.ToString());

                BindSecondary(Convert.ToInt32(lbPrimary.SelectedValue)); // Bind the Secondary In Office ComboBox By Primary Id
            }
            else
            {
                txtNature.Text = Convert.ToString(lbNature.SelectedItem.Text.ToString());
                lbPrimary.Items.Clear();
                lbSecondary.Items.Clear();
                txtPrimary.Text = "";
                txtSecondary.Text = "";
            }

        }
        catch (Exception)
        {

        }
    }
    private void BindSecondary(int intPrimary)
    {
        try
        {

            DataSet dts = objBLL.GetSecondaryByPrimaryID(intPrimary);
            if (dts.Tables[0].Rows.Count > 0)
            {
                lbSecondary.DataSource = dts;
                lbSecondary.DataTextField = "Name";
                lbSecondary.DataValueField = "Code";
                lbSecondary.DataBind();
                lbSecondary.SelectedIndex = 0;

                txtPrimary.Text = Convert.ToString(lbPrimary.SelectedItem.Text.ToString());
                txtSecondary.Text = Convert.ToString(lbSecondary.SelectedItem.Text.ToString());

            }
            else
            {
                txtPrimary.Text = Convert.ToString(lbPrimary.SelectedItem.Text.ToString());
                lbSecondary.Items.Clear();
                lbMinor.Items.Clear();

                txtSecondary.Text = "";
            }

        }
        catch (Exception)
        {

        }
    }
    private void BindMinor(int intSecondary)
    {
        try
        {

            DataSet dts = objBLL.GetMinorBySecondaryID(intSecondary);
            if (dts.Tables[0].Rows.Count > 0)
            {
                lbMinor.DataSource = dts;
                lbMinor.DataTextField = "Name";
                lbMinor.DataValueField = "Code";
                lbMinor.DataBind();
                //if (lbMinor.Items.Count > 0)
                //    lbMinor.SelectedIndex = 0;


            }
            else
            {
                lbMinor.Items.Clear();
                txtMinor.Text = "";
            }
            txtPrimary.Text = Convert.ToString(lbPrimary.SelectedItem.Text.ToString());
            txtSecondary.Text = Convert.ToString(lbSecondary.SelectedItem.Text.ToString());
            txtMinor.Text = Convert.ToString(lbMinor.SelectedItem.Text.ToString());

        }
        catch (Exception)
        {

        }
    }

    protected void lbNature_SelectedIndexChanged(object sender, EventArgs e)
    {
        hdnFlagCheck.Value = "true";
        BindPrimary(Convert.ToInt32(lbNature.SelectedValue)); // Bind the Primary In Office ComboBox By Nature Id
    }

    protected void lbPrimary_SelectedIndexChanged(object sender, EventArgs e)
    {
        hdnFlagCheck.Value = "true";
        BindSecondary(Convert.ToInt32(lbPrimary.SelectedValue)); // Bind the Secondary In Office ComboBox By Primary Id
    }

    protected void lbSecondary_SelectedIndexChanged(object sender, EventArgs e)
    {
        hdnFlagCheck.Value = "true";
        BindMinor(Convert.ToInt32(lbSecondary.SelectedValue)); // Bind the Secondary In Office ComboBox By Primary Id
    }

    protected void lbMinor_SelectedIndexChanged(object sender, EventArgs e)
    {
        hdnFlagCheck.Value = "true";
        txtMinor.Text = lbMinor.SelectedItem.Text.ToString();
    }

    protected void Send_Mail_Job_Details(int OFFICE_ID, int WORKLIST_ID, int VESSEL_ID)
    {
        try
        {
            int MsgID = objBLL.Create_Mail_Job_Details(OFFICE_ID, WORKLIST_ID, VESSEL_ID);
            if (MsgID > 0)
            {
                string js = "window.open('../../Crew/EmailEditor.aspx?ID=" + MsgID + "&Discard=1');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "SendMail" + MsgID, js, true);
            }
        }
        catch (Exception ex)
        {
            string js = "alert('Error in loading data!! Error: " + ex.Message + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "errorloadingedit", js, true);

        }
    }

    //protected void Send_Mail_Job_Details(int OFFICE_ID, int WORKLIST_ID, int VESSEL_ID)
    //{
    //    try
    //    {
    //        string msgSubject = "";
    //        string msgTo = "";
    //        string msgCC = "";
    //        string msgBCC = "";



    //        DataSet dtsJobDetails = objBLL.Get_JobDetails_ByID(OFFICE_ID, WORKLIST_ID, VESSEL_ID);

    //        if (dtsJobDetails.Tables[0].Rows.Count > 0)
    //        {
    //            if (dtsJobDetails.Tables[0].Rows[0]["IsVessel"].ToString() == "1")
    //                msgSubject = "Worklist Job: " + dtsJobDetails.Tables[0].Rows[0]["WORKLIST_ID"].ToString() + "/" + dtsJobDetails.Tables[0].Rows[0]["VESSEL_SHORT_NAME"].ToString();
    //            else
    //                msgSubject = "Worklist Job: " + dtsJobDetails.Tables[0].Rows[0]["WORKLIST_ID"].ToString() + "/" + dtsJobDetails.Tables[0].Rows[0]["VESSEL_SHORT_NAME"].ToString() + "/PIC:" + dtsJobDetails.Tables[0].Rows[0]["PIC_Name"].ToString();

    //            string msgBody = "";

    //            msgBody += "Vessel: " + dtsJobDetails.Tables[0].Rows[0]["vessel_name"].ToString();
    //            msgBody += "<br>Job Code: " + dtsJobDetails.Tables[0].Rows[0]["WORKLIST_ID"].ToString();
    //            msgBody += "<br>Description: " + dtsJobDetails.Tables[0].Rows[0]["JOB_DESCRIPTION"].ToString();


    //            msgBody += "<br><br><a href='http://" + Request.ServerVariables["SERVER_NAME"].ToString() + "/" + ConfigurationManager.AppSettings["APP_NAME"].ToUpper() + "/Technical/worklist/ViewJob.aspx?OFFID=" + OFFICE_ID.ToString() + "&WLID=" + WORKLIST_ID.ToString() + "&VID=" + VESSEL_ID.ToString() + "'>View Job Details</a>";

    //            msgBody += "<br><br>Regards,<br>" + Session["USERFULLNAME"].ToString();

    //            int MsgID = objCrew.Send_CrewNotification(0, 0, 0, 5, msgTo, msgCC, msgBCC, msgSubject, msgBody, "", "MAIL", "", GetSessionUserID(), "DRAFT");
    //            if (MsgID > 0)
    //            {
    //                string js = "window.open('../../Crew/EmailEditor.aspx?ID=" + MsgID + "&Discard=1');";
    //                ScriptManager.RegisterStartupScript(this, this.GetType(), "SendMail" + MsgID, js, true);
    //            }


    //        }

    //    }
    //    catch (Exception ex)
    //    {
    //        string js = "alert('Error in loading data!! Error: " + ex.Message + "');";
    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "errorloadingedit", js, true);

    //    }
    //}

    protected void EditJob(object sender, EventArgs e)
    {
        hdnFlagCheck.Value = "false";
        //Search_Worklist();
        Server.Transfer("addnewjob.aspx?JID=");
    }

    protected void btnSelectAndClose_OnClick(object sender, EventArgs e)
    {
        hdnNature.Value = Convert.ToString(lbNature.SelectedValue);
        hdnPrimary.Value = Convert.ToString(lbPrimary.SelectedValue);
        hdnSecondary.Value = Convert.ToString(lbSecondary.SelectedValue);
        hdnMinor.Value = Convert.ToString(lbMinor.SelectedValue);

        if (hdnNature.Value.ToString() != "")
        {
            ddlNature.SelectedValue = hdnNature.Value;
            BindPrimaryByNatureID(int.Parse(hdnNature.Value));
        }
        if (hdnPrimary.Value.ToString() != "")
        {
            ddlPrimary.SelectedValue = hdnPrimary.Value;
            BindSecondaryByPrimaryID(int.Parse(hdnPrimary.Value));
        }
        if (hdnSecondary.Value.ToString() != "")
        {
            ddlSecondary.SelectedValue = hdnSecondary.Value;
            BindMinorBySecondatyID(int.Parse(hdnSecondary.Value));
        }
        if (hdnMinor.Value.ToString() != "")
        {
            ddlMinor.SelectedValue = hdnMinor.Value;
        }

        Search_Worklist();
    }

    protected void ImgBtnClearFilter_Click(object sender, EventArgs e)
    {
        hf1.Value = "";
        string toDate = "";//Convert.ToString(DateTime.Now.ToString("dd/MM/yyyy"));
        string fromDate = ""; // Convert.ToString(DateTime.Now.AddMonths(-6).ToString("dd/MM/yyyy"));
        chkRequisition.Checked = false;
        chkDrydock.Checked = false;
        chkFlaggedJobs.Checked = false;
        chkSentToShip.Checked = false;
        ddlFleet.SelectedIndex = 0;
        ddlVessels.SelectedIndex = 0;
        ddldeptShip.SelectedIndex = 0;
        ddlOffice.SelectedIndex = 0;
        ddlAssignor.SelectedIndex = 0;

        ddlNature.SelectedIndex = 0;
        ddlPrimary.SelectedIndex = 0;
        ddlSecondary.SelectedIndex = 0;
        ddlMinor.SelectedIndex = 0;

        txtFromDate.Text = fromDate;
        txtToDate.Text = toDate;

        txtDescription.Text = string.Empty;
        //txtJobCode.Text = string.Empty;

        rblJobStaus.SelectedValue = "PENDING";
        rblJobType.SelectedIndex = 0;
        chkDrydock.Checked = false;

        hdnFlagCheck.Value = "false";
        txtExpectedCompTo.Text = "";
        txtExpectedCompFrom.Text = "";

        ddlPIC.SelectedIndex = 0;
        ddlInspector.SelectedIndex = 0;
        txtModifiedInDays.Text = "";

        if (lbPrimary.Items.Count > 0)
        {
            lbPrimary.Items.Clear();
            txtPrimary.Text = "";
        }
        if (lbSecondary.Items.Count > 0)
        {
            lbSecondary.Items.Clear();
            txtSecondary.Text = "";
        }
        if (ddlPriority.Items.Count > 0)
        {
            ddlPriority.SelectedIndex = 0;
        }

        // Request.QueryString.Remove("?VesselID=");
        if (Request.QueryString["SchDetailId"] != null)
        {
            tblInspectionDetail.Attributes.Add("bgcolor", "inherit");
            Response.Redirect("~/Technical/worklist/Worklist.aspx");


            //tblInspectionDetail.BgColor=new Color()
        }
        ucCustomPagerctp.CurrentPageIndex = 1;
        Search_Worklist();
    }

    protected void ImgBtnSearch_Click(object sender, EventArgs e)
    {
        hf1.Value = "";

        ViewState["VesselId"] = null;
        ViewState["SchDetailId"] = null;
        ViewState["ActualDate"] = null;
        ViewState["NCRFlag"] = null;

        Search_Worklist();
    }

    protected void ImgBtnReport_Click(object sender, ImageClickEventArgs e)
    {
        string js = "";

        if (ddlReports.SelectedValue == "0")
        {
            js = "window.open('../reports/reportJobList.aspx');";

        }
        else
        {
            js = "window.open('../reports/reportJobProgress.aspx');";

        }

        ScriptManager.RegisterStartupScript(this, this.GetType(), "showReport", js, true);
    }

    protected void ImgExportToExcel_Click(object sender, ImageClickEventArgs e)
    {


        ExportExcel(true);




    }



    protected void grdJoblist_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdJoblist.PageIndex = e.NewPageIndex;
        Search_Worklist();
        //lblPageStatus.Text = (GridView1.PageIndex + 1).ToString() + " of " + GridView1.PageCount.ToString();
    }

    protected void grdJoblist_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            if (ViewState["SORTBYCOLOUMN"] != null)
            {
                HtmlImage img = (HtmlImage)e.Row.FindControl(ViewState["SORTBYCOLOUMN"].ToString());
                if (img != null)
                {
                    if (ViewState["SORTDIRECTION"] == null || ViewState["SORTDIRECTION"].ToString() == "1")
                        img.Src = "~/purchase/Image/arrowDown.png";
                    else
                        img.Src = "~/purchase/Image/arrowUp.png";

                    img.Visible = true;
                }
            }
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string AttachmentCount = DataBinder.Eval(e.Row.DataItem, "AttachmentCount").ToString();
            string Worklist_ID = DataBinder.Eval(e.Row.DataItem, "Worklist_ID").ToString();
            string Vessel_ID = DataBinder.Eval(e.Row.DataItem, "Vessel_ID").ToString();
            string WL_Office_ID = DataBinder.Eval(e.Row.DataItem, "Office_ID").ToString();

            ImageButton imgRemarks = (ImageButton)e.Row.FindControl("imgRemarks");
            if (imgRemarks != null)
            {
                imgRemarks.Attributes.Add("onmouseover", "showFollowups(" + Vessel_ID + "," + Worklist_ID + "," + WL_Office_ID + ")");
                imgRemarks.Attributes.Add("onmouseout", "closeDiv('dialog')");
            }

            Image ImgAttachment = (Image)(e.Row.FindControl("ImgAttachment"));
            if (ImgAttachment != null)
            {
                if (AttachmentCount == "0")
                    ImgAttachment.Visible = false;
                else
                    ImgAttachment.Attributes.Add("onclick", "showDialog('Attachments.aspx?vid=" + Vessel_ID + "&wlid=" + Worklist_ID + "&wl_off_id=" + WL_Office_ID + "');");

            }
            if (ViewState["WLADMIN"].ToString() != "1")
            {
                ImageButton imgDel = e.Row.Cells[18].FindControl("ImgDelete") as ImageButton;
                imgDel.Visible = false;
            }

            Image ImgLocations = (Image)(e.Row.FindControl("ImgLocations"));

            if (!string.IsNullOrEmpty(Convert.ToString(grdJoblist.DataKeys[e.Row.RowIndex].Values[0])))
            {         
                ImgLocations.Attributes.Add("onmouseover", "abcxx(" + Worklist_ID + "," + WL_Office_ID + "," + Vessel_ID + ",event,this)");
                ImgLocations.Visible = true;
            }
            else
            {               
                    ImgLocations.Visible = false;
             
            }

            Image ImgJobAddedFrom = (Image)(e.Row.FindControl("ImgJobAddedFrom"));

            int _result = objBLL.IsJobAssignedInInspection((int)DataBinder.Eval(e.Row.DataItem, "Vessel_ID"), (int)DataBinder.Eval(e.Row.DataItem, "Office_ID"), (int)DataBinder.Eval(e.Row.DataItem, "Worklist_ID"));

            //if (!string.IsNullOrEmpty(DataBinder.Eval(e.Row.DataItem, "Activity_ID").ToString()))
            if (_result > 0)
            {

                ImgJobAddedFrom.ImageUrl = "~/Images/mob.png";
            }
            else
            {
                ImgJobAddedFrom.Visible = false;

            }

        }

    }
    protected void grdJoblist_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.ToUpper() == "VIEWDETAILS")
        {
            Int32 i32JobID = Convert.ToInt32(e.CommandArgument);
            Response.Redirect("ViewJob.aspx?JID=" + i32JobID);
        }
        else if (e.CommandName.ToUpper() == "EMAILJOB")
        {
            // OFFICE_ID , WLID , VESSEL_ID 
            string[] arg = e.CommandArgument.ToString().Split(',');
            Send_Mail_Job_Details(UDFLib.ConvertToInteger(arg[0]), UDFLib.ConvertToInteger(arg[1]), UDFLib.ConvertToInteger(arg[2]));
        }
        else if (e.CommandName.ToUpper() == "FLAGJOBFORMEETING")
        {
            // OFFICE_ID , WLID , VESSEL_ID , FLAG 
            string[] arg = e.CommandArgument.ToString().Split(',');
            objBLL.UPDATE_Tech_Meeting_Flag(UDFLib.ConvertToInteger(arg[2]), UDFLib.ConvertToInteger(arg[1]), UDFLib.ConvertToInteger(arg[0]), UDFLib.ConvertToInteger(arg[3]), GetSessionUserID());
            Search_Worklist();
        }
        else if (e.CommandName.ToUpper() == "ADD_FOLLOWUP")
        {
            txtMessage.Text = "";
            string[] arg = e.CommandArgument.ToString().Split(',');

            int Vessel_ID = UDFLib.ConvertToInteger(arg[0]);
            int Worklist_ID = UDFLib.ConvertToInteger(arg[1]);
            int Office_ID = UDFLib.ConvertToInteger(arg[2]);

            string WORKLIST_STATUS = UDFLib.ConvertStringToNull(arg[3]);

            hdnVesselID.Value = Vessel_ID.ToString();
            hdnWorklistlID.Value = Worklist_ID.ToString();
            hdnOfficeID.Value = Office_ID.ToString();

            //Do not add followup unless job id is created at vessel
            if (Worklist_ID > 0)
                btnSaveFollowUpAndClose.Enabled = true;
            else
                btnSaveFollowUpAndClose.Enabled = false;


            if (WORKLIST_STATUS.ToUpper() == "COMPLETED" || WORKLIST_STATUS.ToUpper() == "VERIFIED")
            {
                btnSaveFollowUpAndClose.Enabled = false;
            }

            if (Vessel_ID > 0)
            {
                string js = "showModal('dvAddFollowUp');$('#dialog').hide();";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "View", js, true);
            }

        }


    }
    protected void Filter_Changed(object sender, EventArgs e)
    {
        hdnFlagCheck.Value = "false";
        string js1 = "toggleSerachPostBack();restConditionCheckbox();";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "View", js1, true);
        // Search_Worklist();
    }
    protected void btnSaveFollowUpAndClose_OnClick(object sender, EventArgs e)
    {
        try
        {
            if (txtMessage.Text.Trim().Length == 0)
            {
                string OpenFollowupDiv = "alert('Message description is mandatory field!');showModal('dvAddFollowUp');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OpenFollowupDiv", OpenFollowupDiv, true);
                return;
            }
            int iJob_OfficeID = int.Parse(hdnOfficeID.Value);
            int Worklist_ID = int.Parse(hdnWorklistlID.Value);
            int VESSEL_ID = int.Parse(hdnVesselID.Value);

            string FOLLOWUP = txtMessage.Text;
            int CREATED_BY = int.Parse(Session["USERID"].ToString());
            int TOSYNC = 1;

            int newFollowupID = objBLL.Insert_Followup(iJob_OfficeID, Worklist_ID, VESSEL_ID, FOLLOWUP, CREATED_BY, TOSYNC);

            //LoadFollowUps(iJob_OfficeID, VESSEL_ID, Worklist_ID);
        }
        catch (Exception ex)
        {
            string js = "alert('Error in saving data!! Error: " + ex.Message + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "errorsaving", js, true);
        }

    }

    //protected void ImgExportToExcel_Click(object sender, ImageClickEventArgs e)
    //{
    //    try
    //    {
    //        string strCommand = "";

    //        if (ddlReports.SelectedValue == "0")
    //        {
    //            if (Session["strQuery"] != null)
    //                strCommand = Session["strQuery"].ToString();

    //            DataTable dtSearchResult = objBLL.Get_FilterWorklist(strCommand).Tables[0];
    //        }
    //        else
    //        {
    //            js = "window.open('../reports/reportJobProgress.aspx');";

    //        }
    //        string[] strHeaderCaptions = { "Vessel", "Survey/Certificate Category", "Survey/Certificate Name", "Remarks", "Make/Model", "Issue Date", "Expiry Date", "Reminder", "Reminder Remarks" };
    //        string[] strDataColNames = { "Vessel_Name", "Survey_Category", "Survey_Cert_Name", "Survey_Cert_remarks", "EquipmentType", "DateOfIssue", "DateOfExpiry", "FollowupReminderDt", "FollowupReminder" };
    //        string FileName = "SurveyList.xls";
    //        string Title = "Survey List";

    //        GridViewExportUtil.ShowExcel(ds.Tables[0], strHeaderCaptions, strDataColNames, FileName, Title);
    //    }
    //    catch
    //    {
    //    }
    //}

    //private void LoadFollowUps(int OFFICE_ID, int VESSEL_ID, int WORKLIST_ID)
    //{
    //    DataTable dtFollowUps = objBLL.Get_FollowupList(OFFICE_ID, VESSEL_ID, WORKLIST_ID);

    //    grdFollowUps.DataSource = dtFollowUps;
    //    grdFollowUps.DataBind();
    //}
    protected void imgExpWithoutImg_Click(object sender, ImageClickEventArgs e)
    {
        ExportExcel(false);
    }
    protected void ExportExcel(bool img)
    {
        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        string sortdirection = null;
        if (ViewState["SORTDIRECTION"] != null)
            sortdirection = ViewState["SORTDIRECTION"].ToString();
        if (img)
        {

            int rowcount = ucCustomPagerctp.isCountRecord;
            int UserCompanyID = UDFLib.ConvertToInteger(Session["USERCOMPANYID"].ToString());

            DataTable dtStatus = new DataTable();
            dtStatus.Columns.Add("All", typeof(int));
            dtStatus.Columns.Add("Pending", typeof(int));
            dtStatus.Columns.Add("Completed", typeof(int));
            dtStatus.Columns.Add("Reworked", typeof(int));
            dtStatus.Columns.Add("Verified", typeof(int));
            dtStatus.Columns.Add("Overdue", typeof(int));

            //string JobStaus ="";

            dtStatus.Rows.Add(rblJobStaus.Items[0].Selected == true ? 1 : 0,
                rblJobStaus.Items[1].Selected == true ? 1 : 0,
                rblJobStaus.Items[2].Selected == true ? 1 : 0,
                rblJobStaus.Items[3].Selected == true ? 1 : 0,
                rblJobStaus.Items[4].Selected == true ? 1 : 0,
                rblJobStaus.Items[5].Selected == true ? 1 : 0);

            //for (int i = 0; i < rblJobStaus.Items.Count; i++)
            //{
            //    if (rblJobStaus.Items[i].Selected == true)
            //    {
            //        JobStaus += "''" + rblJobStaus.Items[i].Value + "'',";
            //    }
            //}
            //JobStaus += "''''" ;
            DataTable dtFilter = new DataTable();
            dtFilter.Columns.Add("PRM_NAME", typeof(string));
            dtFilter.Columns.Add("PRM_VALUE", typeof(object));

            dtFilter.Rows.Add(new object[] { "@FLEET_ID", UDFLib.ConvertIntegerToNull(ddlFleet.SelectedValue) });
            dtFilter.Rows.Add(new object[] { "@VESSEL_ID", UDFLib.ConvertIntegerToNull(ddlVessels.SelectedValue) });
            dtFilter.Rows.Add(new object[] { "@ASSIGNOR", UDFLib.ConvertIntegerToNull(ddlAssignor.SelectedValue) });
            dtFilter.Rows.Add(new object[] { "@DEPT_SHIP", UDFLib.ConvertIntegerToNull(ddldeptShip.SelectedValue) });
            dtFilter.Rows.Add(new object[] { "@DEPT_OFFICE", UDFLib.ConvertIntegerToNull(ddlOffice.SelectedValue) });
            dtFilter.Rows.Add(new object[] { "@PRIORITY", UDFLib.ConvertIntegerToNull(ddlPriority.SelectedValue) });
            dtFilter.Rows.Add(new object[] { "@CATEGORY_NATURE", UDFLib.ConvertIntegerToNull(ddlNature.SelectedValue) });
            dtFilter.Rows.Add(new object[] { "@CATEGORY_PRIMARY", UDFLib.ConvertIntegerToNull(ddlPrimary.SelectedValue) });
            dtFilter.Rows.Add(new object[] { "@CATEGORY_SECONDARY", UDFLib.ConvertIntegerToNull(ddlSecondary.SelectedValue) });
            dtFilter.Rows.Add(new object[] { "@CATEGORY_MINOR", null, });
            dtFilter.Rows.Add(new object[] { "@JOB_DESCRIPTION", UDFLib.ConvertStringToNull(txtDescription.Text) });
            dtFilter.Rows.Add(new object[] { "@JOB_STATUS", UDFLib.ConvertStringToNull(rblJobStaus.SelectedValue) });

            dtFilter.Rows.Add(new object[] { "@dtJOB_Status", dtStatus });

            dtFilter.Rows.Add(new object[] { "@JOB_TYPE", 0 });
            dtFilter.Rows.Add(new object[] { "@PIC", UDFLib.ConvertIntegerToNull(ddlPIC.SelectedValue) });
            dtFilter.Rows.Add(new object[] { "@JOB_MODIFIED_IN", UDFLib.ConvertIntegerToNull(txtModifiedInDays.Text) });
            dtFilter.Rows.Add(new object[] { "@DATE_RAISED_FROM", UDFLib.ConvertDateToNull(txtFromDate.Text) });
            dtFilter.Rows.Add(new object[] { "@DATE_RAISED_TO", UDFLib.ConvertDateToNull(txtToDate.Text) });
            dtFilter.Rows.Add(new object[] { "@DATE_CMPLTN_FROM", UDFLib.ConvertDateToNull(txtExpectedCompFrom.Text) });
            dtFilter.Rows.Add(new object[] { "@DATE_CMPLTN_TO", UDFLib.ConvertDateToNull(txtExpectedCompTo.Text) });
            dtFilter.Rows.Add(new object[] { "@DEFER_TO_DD", (chkDrydock.Checked == true ? UDFLib.ConvertIntegerToNull(1) : null) });
            dtFilter.Rows.Add(new object[] { "@SENT_TO_SHIP", (chkSentToShip.Checked == true ? UDFLib.ConvertIntegerToNull(1) : null) });
            dtFilter.Rows.Add(new object[] { "@HAVING_REQ_NO", (chkRequisition.Checked == true ? UDFLib.ConvertIntegerToNull(1) : null) });
            dtFilter.Rows.Add(new object[] { "@FLAGGED_FOR_MEETING", (chkFlaggedJobs.Checked == true ? UDFLib.ConvertIntegerToNull(1) : null) });
            dtFilter.Rows.Add(new object[] { "@INSPECTOR", UDFLib.ConvertIntegerToNull(ddlInspector.SelectedValue) });
            dtFilter.Rows.Add(new object[] { "@PAGE_INDEX", null });
            dtFilter.Rows.Add(new object[] { "@PAGE_SIZE", null });

            if (Session["COMPANYTYPE"].ToString().ToUpper() == "SURVEYOR" && ddlVessel_Manager.SelectedIndex > 0)
            {
                dtFilter.Rows.Add(new object[] { "@Company_ID", ddlVessel_Manager.SelectedValue });
            }

            dtFilter.Rows.Add(new object[] { "@WL_TYPE", rblJobType.SelectedValue });
            dtFilter.Rows.Add(new object[] { "@SortBy", sortbycoloumn });
            dtFilter.Rows.Add(new object[] { "@SORT_DIRECTION", sortdirection });
            int Record_Count = 0;
            DataTable taskTable = null;
            if (hf1.Value != "")
            {
                taskTable = objBLL.Get_WorkList_Index_Filter(Convert.ToInt32(hf1.Value), 1, 10000, ref Record_Count, sortbycoloumn, sortdirection).Tables[0];
                string SetCurButton = "SetCurButton();";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "SetCurButton", SetCurButton, true);
            }
            else
            {
                taskTable = objBLL.Get_WorkList_Index(dtFilter, ref Record_Count);
            }

            if (Record_Count == 0)
            {
                string js = "alert('No records found!');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "View", js, true);
                return;
            }


            DataTable WLIDS = new DataTable();

            WLIDS.Columns.Add("WORKLIST_ID", typeof(int));
            WLIDS.Columns.Add("VESSEL_ID", typeof(string));
            WLIDS.Columns.Add("OFFICE_ID", typeof(string));


            foreach (DataRow item in taskTable.Rows)
            {
                WLIDS.Rows.Add(Convert.ToInt32(item["WORKLIST_ID"]), Convert.ToInt32(item["VESSEL_ID"]), Convert.ToInt32(item["OFFICE_ID"]));
            }







            DataSet ds = objBLL.Generate_Excel_Report(WLIDS);






            string htmlout = ds.Tables[0].Rows[0].ItemArray[0].ToString();
            string preferences = ds.Tables[1].Rows[0].ItemArray[0].ToString();
            string summery = ds.Tables[2].Rows[0].ItemArray[0].ToString();
            string exceltable = ds.Tables[3].Rows[0].ItemArray[0].ToString();




            string fileNamewithpath = "";
            string filename = "";
            string folder = AppDomain.CurrentDomain.BaseDirectory + "Uploads\\Technical";


            string[] BaseDirectory = AppDomain.CurrentDomain.BaseDirectory.Split('\\');
            string domainname = BaseDirectory[BaseDirectory.Length - 2];

            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);



            string preferenceswithoutvalue = HtmlParser.StripTagsCharArray(preferences);
            DataSet dataSet = HtmlTableParser.ParseDataSet(exceltable);
            DataSet summeryset = HtmlTableParser.ParseDataSet(summery);
            SLDocument sl = new SLDocument();
            sl.SetCellValue("A1", "Worklist Report");
            SLStyle style = sl.CreateStyle();
            style.Font.FontName = "Tahoma";
            style.Font.FontSize = 22;
            style.Font.Bold = true;
            style.Font.FontColor = System.Drawing.Color.White;
            style.Fill.SetPattern(PatternValues.Solid, System.Drawing.Color.FromArgb(0, 153, 253), System.Drawing.Color.FromArgb(0, 153, 253));
            style.Alignment.Horizontal = HorizontalAlignmentValues.Center;
            style.Alignment.Vertical = VerticalAlignmentValues.Center;
            sl.SetCellStyle("A1", style);
            sl.MergeWorksheetCells("A1", "L2");
            sl.SetCellValue("K3", "Job Type");
            sl.SetCellValue("L3", "Count");


            style = sl.CreateStyle();
            style.Font.FontName = "Tahoma";
            style.Font.Bold = true;
            style.Alignment.Horizontal = HorizontalAlignmentValues.Center;
            //  sl.SetCellStyle("A4", style);
            // sl.MergeWorksheetCells("A4", "B4");

            style = sl.CreateStyle();
            style.Font.FontName = "Tahoma";
            //  preferenceswithoutvalue = preferenceswithoutvalue.Remove(0, 14);
            sl.SetCellValue("A5", preferenceswithoutvalue);
            //style.Alignment.Horizontal = HorizontalAlignmentValues.Center;
            //style.Alignment.Vertical = VerticalAlignmentValues.Center;
            //style.Alignment.JustifyLastLine = true;
            style.SetWrapText(true);
            sl.SetCellStyle("A5", style);
            //  sl.MergeWorksheetCells("A5", "H6");

            sl.SetCellValue("A8", "ReportDate :-");
            sl.SetCellValue("A9", "Total Jobs :-");
            style = sl.CreateStyle();
            style.Font.FontName = "Tahoma";
            style.Font.Bold = true;
            sl.SetCellStyle("A8", style);
            sl.SetCellStyle("A9", style);
            sl.MergeWorksheetCells("A8", "B8");
            sl.MergeWorksheetCells("A9", "B9");
            sl.SetCellValue("C8", DateTime.Now.ToString("dd/MMM/yyyy"));

            SLStyle th = sl.CreateStyle();
            th.Font.FontName = "Tahoma";
            th.Font.Bold = true;
            th.Fill.SetPattern(PatternValues.Solid, System.Drawing.Color.FromArgb(93, 123, 157), System.Drawing.Color.FromArgb(93, 123, 157));
            th.Border.SetBottomBorder(BorderStyleValues.Thin, SLThemeColorIndexValues.Dark1Color);
            th.Border.SetTopBorder(BorderStyleValues.Thin, SLThemeColorIndexValues.Dark1Color);
            th.Border.SetLeftBorder(BorderStyleValues.Thin, SLThemeColorIndexValues.Dark1Color);
            th.Border.SetRightBorder(BorderStyleValues.Thin, SLThemeColorIndexValues.Dark1Color);
            th.Font.FontColor = System.Drawing.Color.White;
            th.SetWrapText(false);


            style = sl.CreateStyle();
            style.Font.FontName = "Tahoma";
            style.Border.SetBottomBorder(BorderStyleValues.Thin, SLThemeColorIndexValues.Dark1Color);
            style.Border.SetTopBorder(BorderStyleValues.Thin, SLThemeColorIndexValues.Dark1Color);
            style.Border.SetLeftBorder(BorderStyleValues.Thin, SLThemeColorIndexValues.Dark1Color);
            style.Border.SetRightBorder(BorderStyleValues.Thin, SLThemeColorIndexValues.Dark1Color);
            style.Fill.SetPattern(PatternValues.Solid, System.Drawing.Color.FromArgb(239, 246, 252), System.Drawing.Color.FromArgb(239, 246, 252));




            sl.SetCellValue("A11", "Sl.No.");
            sl.SetCellValue("B11", "Vessel");
            sl.SetCellValue("C11", "Code");
            sl.SetCellValue("D11", "Job Description");
            sl.SetCellValue("E11", "Assignor");
            sl.SetCellValue("F11", "PIC");
            sl.SetCellValue("G11", "Date Raised");
            sl.SetCellValue("H11", "Office Dept.");
            sl.SetCellValue("I11", "Vessel Dept.");
            sl.SetCellValue("J11", "Due Date");
            sl.SetCellValue("K11", "Done Date");
            sl.SetCellValue("L11", "Type");


            int i = 4;
            foreach (DataRow row in summeryset.Tables[0].Rows)
            {
                if (!row.ItemArray[0].ToString().Contains("PMS"))
                {
                    sl.SetCellValue("K" + i, row.ItemArray[0].ToString());
                    sl.SetCellValue("L" + i, Convert.ToInt32(row.ItemArray[1]));
                    sl.SetCellStyle("K" + i, style);
                    sl.SetCellStyle("L" + i, style);
                    i++;
                }

            }


            sl.SetCellStyle("A11", th);
            sl.SetCellStyle("B11", th);
            sl.SetCellStyle("C11", th);
            sl.SetCellStyle("D11", th);
            sl.SetCellStyle("E11", th);
            sl.SetCellStyle("F11", th);
            sl.SetCellStyle("G11", th);
            sl.SetCellStyle("H11", th);
            sl.SetCellStyle("I11", th);
            sl.SetCellStyle("J11", th);
            sl.SetCellStyle("K11", th);
            sl.SetCellStyle("L11", th);
            sl.SetCellStyle("K3", th);
            sl.SetCellStyle("L3", th);


            i = 12;
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {

                if (row.ItemArray[1].ToString().StartsWith("<img"))
                    continue;

                SLStyle astyle = style.Clone();
                astyle.Alignment.Horizontal = HorizontalAlignmentValues.Center;
                sl.SetCellStyle("A" + i, astyle);
                sl.SetCellStyle("B" + i, style);
                sl.SetCellStyle("C" + i, astyle);
                sl.SetCellStyle("D" + i, style);
                sl.SetCellStyle("E" + i, style);
                sl.SetCellStyle("F" + i, style);
                sl.SetCellStyle("G" + i, style);
                sl.SetCellStyle("H" + i, style);
                sl.SetCellStyle("I" + i, style);
                sl.SetCellStyle("J" + i, style);
                sl.SetCellStyle("K" + i, style);
                sl.SetCellStyle("L" + i, style);


                sl.SetCellValue("A" + i, row.ItemArray[0].ToString().Replace("&nbsp;", " "));
                sl.SetCellValue("C" + i, row.ItemArray[2].ToString().Replace("&nbsp;", " "));
                if (row.ItemArray[3].ToString().StartsWith("<a"))
                {
                    sl.SetCellValue("D" + i, HtmlParser.StripTagsCharArray(row.ItemArray[3].ToString()));
                    HtmlTag tag;
                    HtmlParser parse = new HtmlParser(row.ItemArray[3].ToString());
                    while (parse.ParseNext("a", out tag))
                    {
                        // See if this anchor links to us
                        string value;
                        if (tag.Attributes.TryGetValue("href", out value))
                        {

                            sl.InsertHyperlink("D" + i, SLHyperlinkTypeValues.Url, value);
                            SLStyle style2 = sl.CreateStyle();
                            style.Font.FontName = "Tahoma";
                            style2.SetFontColor(System.Drawing.Color.Blue);
                            sl.SetCellStyle("D" + i, style2);
                        }
                    }
                }
                bool imgt = false;

                if (row.ItemArray[1].ToString().StartsWith("<img"))
                {

                    imgt = true;
                    HtmlTag tag;
                    HtmlParser parse = new HtmlParser(row.ItemArray[1].ToString());
                    int k = 0;
                    while (parse.ParseNext("img", out tag))
                    {
                        SLComment comm;
                        // See if this anchor links to us
                        string value;
                        if (tag.Attributes.TryGetValue("src", out value))
                        {

                            string imagefilename = Path.GetFileName(value);
                            comm = sl.CreateComment();
                            try
                            {
                                using (WebClient Client = new WebClient())
                                {
                                    // value = value.Replace(@"/\\", @"/");
                                    // value = value.Replace(@"\", @"/"); 
                                    // Client.DownloadFile(value, Path.Combine(folder, imagefilename));
                                    if (File.Exists(Path.Combine(folder, imagefilename)))
                                    {
                                        comm.Fill.SetPictureFill(Path.Combine(folder, imagefilename), 0, 0, 0, 0, 0);
                                    }
                                    else
                                    {
                                        comm.Fill.SetPictureFill(Path.Combine(folder, "no-image-found.jpg"), 0, 0, 0, 0, 0);
                                    }


                                }
                            }
                            catch (Exception)
                            {

                                comm.SetText("Image not found!");
                            }
                            // this is one of those methods that don't quite match completely...
                            // the first 2 zeroes are OffsetX and OffsetY, which aren't used, so just set them as zero.
                            // 33 means 33%, so the picture will be tiled approximately 3 times (100 / 33) horizontally.
                            // 50 means 50%, so it will be tiled 2 times (100 / 50) vertically.
                            // You can ignore RectangleAlignmentValues and TileFlipValues (for now?)
                            // The last zero is the transparency. 
                            comm.Width = 400;
                            comm.Height = 200;
                            if (k == 0)
                            {
                                SLStyle imstyle = style.Clone();
                                imstyle.Fill.SetPattern(PatternValues.Solid, System.Drawing.Color.Lime, System.Drawing.Color.Lime);
                                imstyle.Font.FontColor = System.Drawing.Color.Purple;
                                sl.SetCellValue("B" + i, "Image");
                                sl.InsertComment("B" + i, comm);
                                sl.SetCellStyle("B" + i, imstyle);

                            }
                            else
                            {
                                SLStyle imstyle = style.Clone();
                                imstyle.Fill.SetPattern(PatternValues.Solid, System.Drawing.Color.Lime, System.Drawing.Color.Lime);
                                imstyle.Font.FontColor = System.Drawing.Color.Purple;
                                sl.SetCellValue("C" + i, "Image");
                                sl.InsertComment("C" + i, comm);
                                sl.SetCellStyle("C" + i, imstyle);
                            }

                            k++;
                        }

                    }

                }
                else
                {
                    sl.SetCellValue("B" + i, row.ItemArray[1].ToString());
                }
                sl.SetCellValue("E" + i, row.ItemArray[4].ToString().Replace("&nbsp;", " "));
                sl.SetCellValue("F" + i, row.ItemArray[5].ToString().Replace("&nbsp;", " "));
                sl.SetCellValue("G" + i, row.ItemArray[6].ToString().Replace("&nbsp;", " "));
                sl.SetCellValue("H" + i, row.ItemArray[7].ToString().Replace("&nbsp;", " "));
                sl.SetCellValue("I" + i, row.ItemArray[8].ToString().Replace("&nbsp;", " "));
                sl.SetCellValue("J" + i, row.ItemArray[9].ToString().Replace("&nbsp;", " "));
                sl.SetCellValue("K" + i, row.ItemArray[10].ToString().Replace("&nbsp;", " "));
                sl.SetCellValue("L" + i, row.ItemArray[12].ToString().Replace("&nbsp;", " "));

                i++;
            }
            sl.SetCellValue("C9", sl.GetCellValueAsInt32("L4") + sl.GetCellValueAsInt32("L5"));
            sl.AutoFitColumn("A");
            sl.AutoFitColumn("B");
            sl.AutoFitColumn("C");
            sl.AutoFitColumn("D", 50);
            sl.AutoFitColumn("E");
            sl.AutoFitColumn("F");
            sl.AutoFitColumn("G");
            sl.AutoFitColumn("H");
            sl.AutoFitColumn("I");
            sl.AutoFitColumn("J");
            sl.AutoFitColumn("K");
            sl.AutoFitColumn("L");

            filename = "WorkListReport" + DateTime.Now.Day + DateTime.Now.Month + DateTime.Now.Second + DateTime.Now.Millisecond + ".xlsx";
            fileNamewithpath = folder + "/" + filename;
            sl.SaveAs(fileNamewithpath);

            ResponseHelper.Redirect("/" + domainname + "/Uploads/Technical/" + filename, "blank", "");


        }
        else
        {


            int rowcount = ucCustomPagerctp.isCountRecord;
            int UserCompanyID = UDFLib.ConvertToInteger(Session["USERCOMPANYID"].ToString());

            DataTable dtStatus = new DataTable();
            dtStatus.Columns.Add("All", typeof(int));
            dtStatus.Columns.Add("Pending", typeof(int));
            dtStatus.Columns.Add("Completed", typeof(int));
            dtStatus.Columns.Add("Reworked", typeof(int));
            dtStatus.Columns.Add("Verified", typeof(int));
            dtStatus.Columns.Add("Overdue", typeof(int));

            //string JobStaus ="";

            dtStatus.Rows.Add(rblJobStaus.Items[0].Selected == true ? 1 : 0,
                rblJobStaus.Items[1].Selected == true ? 1 : 0,
                rblJobStaus.Items[2].Selected == true ? 1 : 0,
                rblJobStaus.Items[3].Selected == true ? 1 : 0,
                rblJobStaus.Items[4].Selected == true ? 1 : 0,
                rblJobStaus.Items[5].Selected == true ? 1 : 0);

            //for (int i = 0; i < rblJobStaus.Items.Count; i++)
            //{
            //    if (rblJobStaus.Items[i].Selected == true)
            //    {
            //        JobStaus += "''" + rblJobStaus.Items[i].Value + "'',";
            //    }
            //}
            //JobStaus += "''''" ;
            DataTable dtFilter = new DataTable();
            dtFilter.Columns.Add("PRM_NAME", typeof(string));
            dtFilter.Columns.Add("PRM_VALUE", typeof(object));

            dtFilter.Rows.Add(new object[] { "@FLEET_ID", UDFLib.ConvertIntegerToNull(ddlFleet.SelectedValue) });
            dtFilter.Rows.Add(new object[] { "@VESSEL_ID", UDFLib.ConvertIntegerToNull(ddlVessels.SelectedValue) });
            dtFilter.Rows.Add(new object[] { "@ASSIGNOR", UDFLib.ConvertIntegerToNull(ddlAssignor.SelectedValue) });
            dtFilter.Rows.Add(new object[] { "@DEPT_SHIP", UDFLib.ConvertIntegerToNull(ddldeptShip.SelectedValue) });
            dtFilter.Rows.Add(new object[] { "@DEPT_OFFICE", UDFLib.ConvertIntegerToNull(ddlOffice.SelectedValue) });
            dtFilter.Rows.Add(new object[] { "@PRIORITY", UDFLib.ConvertIntegerToNull(ddlPriority.SelectedValue) });
            dtFilter.Rows.Add(new object[] { "@CATEGORY_NATURE", UDFLib.ConvertIntegerToNull(ddlNature.SelectedValue) });
            dtFilter.Rows.Add(new object[] { "@CATEGORY_PRIMARY", UDFLib.ConvertIntegerToNull(ddlPrimary.SelectedValue) });
            dtFilter.Rows.Add(new object[] { "@CATEGORY_SECONDARY", UDFLib.ConvertIntegerToNull(ddlSecondary.SelectedValue) });
            dtFilter.Rows.Add(new object[] { "@CATEGORY_MINOR", null, });
            dtFilter.Rows.Add(new object[] { "@JOB_DESCRIPTION", UDFLib.ConvertStringToNull(txtDescription.Text) });
            dtFilter.Rows.Add(new object[] { "@JOB_STATUS", UDFLib.ConvertStringToNull(rblJobStaus.SelectedValue) });

            dtFilter.Rows.Add(new object[] { "@dtJOB_Status", dtStatus });

            dtFilter.Rows.Add(new object[] { "@JOB_TYPE", 0 });
            dtFilter.Rows.Add(new object[] { "@PIC", UDFLib.ConvertIntegerToNull(ddlPIC.SelectedValue) });
            dtFilter.Rows.Add(new object[] { "@JOB_MODIFIED_IN", UDFLib.ConvertIntegerToNull(txtModifiedInDays.Text) });
            dtFilter.Rows.Add(new object[] { "@DATE_RAISED_FROM", UDFLib.ConvertDateToNull(txtFromDate.Text) });
            dtFilter.Rows.Add(new object[] { "@DATE_RAISED_TO", UDFLib.ConvertDateToNull(txtToDate.Text) });
            dtFilter.Rows.Add(new object[] { "@DATE_CMPLTN_FROM", UDFLib.ConvertDateToNull(txtExpectedCompFrom.Text) });
            dtFilter.Rows.Add(new object[] { "@DATE_CMPLTN_TO", UDFLib.ConvertDateToNull(txtExpectedCompTo.Text) });
            dtFilter.Rows.Add(new object[] { "@DEFER_TO_DD", (chkDrydock.Checked == true ? UDFLib.ConvertIntegerToNull(1) : null) });
            dtFilter.Rows.Add(new object[] { "@SENT_TO_SHIP", (chkSentToShip.Checked == true ? UDFLib.ConvertIntegerToNull(1) : null) });
            dtFilter.Rows.Add(new object[] { "@HAVING_REQ_NO", (chkRequisition.Checked == true ? UDFLib.ConvertIntegerToNull(1) : null) });
            dtFilter.Rows.Add(new object[] { "@FLAGGED_FOR_MEETING", (chkFlaggedJobs.Checked == true ? UDFLib.ConvertIntegerToNull(1) : null) });
            dtFilter.Rows.Add(new object[] { "@INSPECTOR", UDFLib.ConvertIntegerToNull(ddlInspector.SelectedValue) });
            dtFilter.Rows.Add(new object[] { "@PAGE_INDEX", null });
            dtFilter.Rows.Add(new object[] { "@PAGE_SIZE", null });
            if (Session["COMPANYTYPE"].ToString().ToUpper() == "SURVEYOR" && ddlVessel_Manager.SelectedIndex > 0)
            {
                dtFilter.Rows.Add(new object[] { "@Company_ID", ddlVessel_Manager.SelectedValue });
            }

            dtFilter.Rows.Add(new object[] { "@WL_TYPE", rblJobType.SelectedValue });
            dtFilter.Rows.Add(new object[] { "@SortBy", sortbycoloumn });
            dtFilter.Rows.Add(new object[] { "@SORT_DIRECTION", sortdirection });
            int Record_Count = 0;
            DataTable taskTable = null;
            if (hf1.Value != "")
            {
                taskTable = objBLL.Get_WorkList_Index_Filter(Convert.ToInt32(hf1.Value), 1, 10000, ref Record_Count, sortbycoloumn, sortdirection).Tables[0];

            }
            else
            {
                taskTable = objBLL.Get_WorkList_Index(dtFilter, ref Record_Count);
            }


            if (Record_Count == 0)
            {
                string js = "alert('No records found!');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "View", js, true);
                return;
            }
            taskTable.Columns.Add("NCRS", typeof(string));

            taskTable.Columns.Add("DATE_RAISEDS", typeof(string));
            taskTable.Columns.Add("DATE_ESTMTD_CMPLTNS", typeof(string));
            if (!taskTable.Columns.Contains("DATE_COMPLETEDS"))
                taskTable.Columns.Add("DATE_COMPLETEDS", typeof(string));
            foreach (DataRow item in taskTable.Rows)
            {
                if (item["NCR"].ToString() == "NCR")
                {
                    item["NCRS"] = "YES";
                }
                else
                {
                    item["NCRS"] = "NO";
                }

                if (item["DATE_RAISED"].ToString().Trim() != "")
                {
                    item["DATE_RAISEDS"] = Convert.ToDateTime(item["DATE_RAISED"].ToString()).ToString("dd/MMM/yyyy");
                }
                if (item["DATE_ESTMTD_CMPLTN"].ToString().Trim() != "")
                {
                    item["DATE_ESTMTD_CMPLTNS"] = Convert.ToDateTime(item["DATE_ESTMTD_CMPLTN"].ToString()).ToString("dd/MMM/yyyy");
                }
                if (item["DATE_COMPLETED"].ToString().Trim() != "")
                {
                    item["DATE_COMPLETEDS"] = Convert.ToDateTime(item["DATE_COMPLETED"].ToString()).ToString("dd/MMM/yyyy");
                }

            }





            string[] HeaderCaptions = { "Vessel", "Code", "JOB DESCRIPTION", "Assignor", "PIC", "DATE RAISED", "Office Dept", "Vessel Dept", "Expected Compln", "Completed", "Type", "Status" };
            string[] DataColumnsName = { "Vessel_Short_Name", "WLID_DISPLAY", "JOB_DESCRIPTION", "AssignorName", "USER_NAME", "DATE_RAISEDS", "INOFFICE_DEPT", "ONSHIP_DEPT", "DATE_ESTMTD_CMPLTNS", "DATE_COMPLETEDS", "Type", "WORKLIST_STATUS" };

            GridViewExportUtil.ShowExcel(taskTable, HeaderCaptions, DataColumnsName, "WorkList", "Work List", "");
        }


    }
    protected void ProgramDelete(object source, CommandEventArgs e)
    {
        objBLL.Upd_Worklist_Status(Convert.ToInt32(e.CommandArgument.ToString().Split(';')[1]), Convert.ToInt32(e.CommandArgument.ToString().Split(';')[0]), Convert.ToInt32(e.CommandArgument.ToString().Split(';')[2]), Convert.ToInt32(Session["USERID"]), "DELETED", "REMOVED");
        Search_Worklist();
    }
    protected void BindJobType()
    {
        try
        {
            //int iCompID = int.Parse(Session["USERCOMPANYID"].ToString());

            rblJobType.DataSource = objBLL.GetAllWorklistType();
            rblJobType.DataTextField = "Worklist_Type_Display";
            rblJobType.DataValueField = "Worklist_Type";
            rblJobType.DataBind();
            rblJobType.Items.Insert(0, new ListItem("ALL", ""));

            if (ViewState["NCRFlag"] == null)
                rblJobType.SelectedIndex = 0;
            else
            {
                if (ViewState["NCRFlag"].ToString() == "NCR")
                {
                    rblJobType.SelectedValue = "NCR";
                }
                else
                {
                    rblJobType.SelectedValue = ViewState["NCRFlag"].ToString();
                }
            }
        }
        catch (Exception)
        {
            ////.WriteError(this.GetType().Name.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), ex);
        }
    }


    protected void btnHidden_OnClick(object sender, EventArgs e)
    {
        try
        {
            hf1.Value = "0";
            Clear();
            LoadWorklistCutom(0);

        }
        catch (Exception ex)
        {
            string js = "alert('Error in saving data!! Error: " + ex.Message + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "errorsaving", js, true);
        }

    }
    protected void btnHidden1_OnClick(object sender, EventArgs e)
    {
        try
        {
            hf1.Value = "1";
            Clear();
            LoadWorklistCutom(1);

        }
        catch (Exception ex)
        {
            string js = "alert('Error in saving data!! Error: " + ex.Message + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "errorsaving", js, true);
        }

    }
    protected void btnHidden2_OnClick(object sender, EventArgs e)
    {
        try
        {
            hf1.Value = "2";
            Clear();
            LoadWorklistCutom(2);

        }
        catch (Exception ex)
        {
            string js = "alert('Error in saving data!! Error: " + ex.Message + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "errorsaving", js, true);
        }

    }
    protected void btnHidden3_OnClick(object sender, EventArgs e)
    {
        try
        {
            hf1.Value = "3";
            Clear();
            LoadWorklistCutom(3);

        }
        catch (Exception ex)
        {
            string js = "alert('Error in saving data!! Error: " + ex.Message + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "errorsaving", js, true);
        }

    }
    protected void btnHidden4_OnClick(object sender, EventArgs e)
    {
        try
        {
            hf1.Value = "4";
            Clear();
            LoadWorklistCutom(4);

        }
        catch (Exception ex)
        {
            string js = "alert('Error in saving data!! Error: " + ex.Message + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "errorsaving", js, true);
        }

    }
    protected void LoadWorklistCutom(int type)
    {
        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        string sortdirection = null;
        if (ViewState["SORTDIRECTION"] != null)
            sortdirection = ViewState["SORTDIRECTION"].ToString();
        int Record_Count = 0;
        //DataSet ds = objBLL.Get_WorkList_Index_Filter(Convert.ToInt32(type), ucCustomPagerctp.isCountRecord, ucCustomPagerctp.PageSize, ref Record_Count, sortbycoloumn, sortdirection);
        DataSet ds = objBLL.Get_WorkList_Index_Filter(Convert.ToInt32(type), ucCustomPagerctp.CurrentPageIndex, ucCustomPagerctp.PageSize, ref Record_Count, sortbycoloumn, sortdirection);

        
        DataTable taskTable = ds.Tables[0];

        grdJoblist.DataSource = taskTable;
        grdJoblist.DataBind();

        ucCustomPagerctp.CountTotalRec = Record_Count.ToString();
        ucCustomPagerctp.BuildPager();

        DataTable dtPKIDs = taskTable.DefaultView.ToTable(true, new string[] { "WORKLIST_ID", "VESSEL_ID", "OFFICE_ID" });
        dtPKIDs.PrimaryKey = new DataColumn[] { dtPKIDs.Columns["WORKLIST_ID"], dtPKIDs.Columns["VESSEL_ID"], dtPKIDs.Columns["OFFICE_ID"] };
        Session["WORKLIST_PKID_NAV"] = dtPKIDs;

        lblRecordCount.Text = Record_Count.ToString();
        string SetCurButton = "SetCurButton();restConditionCheckbox();";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "SetCurButton", SetCurButton, true);
    }
    protected void Clear()
    {
        string toDate = Convert.ToString(DateTime.Now.ToString("dd/MM/yyyy"));
        string fromDate = Convert.ToString(DateTime.Now.AddMonths(-6).ToString("dd/MM/yyyy"));
        ddlFleet.SelectedIndex = 0;
        ddlVessels.SelectedIndex = 0;
        ddldeptShip.SelectedIndex = 0;
        ddlOffice.SelectedIndex = 0;
        ddlAssignor.SelectedIndex = 0;

        ddlNature.SelectedIndex = 0;
        ddlPrimary.SelectedIndex = 0;
        ddlSecondary.SelectedIndex = 0;
        ddlMinor.SelectedIndex = 0;

        txtFromDate.Text = fromDate;
        txtToDate.Text = toDate;

        txtDescription.Text = string.Empty;
        //txtJobCode.Text = string.Empty;

        rblJobStaus.SelectedValue = "PENDING";
        rblJobType.SelectedIndex = 0;
        chkDrydock.Checked = false;

        hdnFlagCheck.Value = "false";
        txtExpectedCompTo.Text = "";
        txtExpectedCompFrom.Text = "";
        chkRequisition.Checked = false;
        chkDrydock.Checked = false;
        chkFlaggedJobs.Checked = false;
        chkSentToShip.Checked = false;
        if (lbPrimary.Items.Count > 0)
        {
            lbPrimary.Items.Clear();
            txtPrimary.Text = "";
        }
        if (lbSecondary.Items.Count > 0)
        {
            lbSecondary.Items.Clear();
            txtSecondary.Text = "";
        }
        if (ddlPriority.Items.Count > 0)
        {
            ddlPriority.SelectedIndex = 0;
        }
    }

    //Added by anjali Dt:22-02-2016

    private void CheckSettingforFunctions()
    {
        DataTable _settingTable;
        DataRow[] foundRows;

        try
        {
            _settingTable = objBLL.GetSettingforFunctions();


            if (_settingTable.Rows.Count > 0)
            {
                foundRows = _settingTable.Select("Settings_Key = 'View Functions To Jobs'");

                if (foundRows.Length > 0)
                {
                    if (_settingTable.Rows[0]["Settings_Value"].ToString() == "0")           //Convert datatype bit to varchar  
                   
                    //if ((bool)foundRows[0]["Settings_Value"] == false)
                    //if (false == false)
                    {
                        grdJoblist.Columns[20].Visible = false;
                    }
                   
                }

            }

        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    //Added by anjali Dt:22-02-2016

}