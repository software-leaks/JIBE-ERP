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

public partial class Technical_WorklistOld : System.Web.UI.Page
{
    ControlParameter pr = new ControlParameter();
    BLL_Tec_Worklist objBLL = new BLL_Tec_Worklist();
    BLL_Infra_VesselLib objBLLVessel = new BLL_Infra_VesselLib();
    BLL_Infra_UserCredentials objBLLUser = new BLL_Infra_UserCredentials();
    BLL_Crew_CrewDetails objCrew = new BLL_Crew_CrewDetails();

    UserAccess objUA = new UserAccess();

    protected void Page_Load(object sender, EventArgs e)
    {
        string toDate = Convert.ToString(DateTime.Now.ToString("dd/MM/yyyy"));
        string fromDate = "01/01/2010"; //Convert.ToString(DateTime.Now.AddMonths(-6).ToString("dd/MM/yyyy"));

        txtFollowupDate.Text = toDate;

        if (Session["USERID"] == null)
            Response.Redirect("~/account/login.aspx");
        
        if (!IsPostBack == true)
        {
            txtFromDate.Text = fromDate;
            txtToDate.Text = toDate;
            //Coming from WORKLIST --> Quality
            if (Request.QueryString["NCR"] != null)
            {
                rblJobType.SelectedValue = "-1";
                //rblJobType.Items[1].Selected = true;
            }

            BindPIC();
            Load_FleetList();
            Load_VesselList();
            BindNature();
            BindNatureForDropDown();
            ddlfill();
            Load_InspectorList();

            filter_grid();
            
            UserAccessValidation();
        }
        if (IsPostBack)
        {
            if (hdnFlagCheck.Value == "true")
            {
                string js = "divCategorylink()";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "showDiv", js, true);
            }
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

    }

    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }

    private string GetQuery()
    {
        string Modified_in_Days="0";
        try
        {
            int Days = 0;

            if (txtModifiedInDays.Text != "")
            {
                Days = int.Parse(txtModifiedInDays.Text);//generates error if not a number
                Modified_in_Days = txtModifiedInDays.Text;
            }
        }
        catch
        {
            string js = "alert('Please enter numeric value in Modified in Days field!!');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }

        string SQL = @"SELECT     
                        WL.WORKLIST_ID, 
                        WL.VESSEL_ID,                         
                        WL.JOB_DESCRIPTION, 
                        WL.ASSIGNOR, 
                        WL.DATE_RAISED, 
                        WL.DATE_ESTMTD_CMPLTN, 
                        WL.DATE_COMPLETED, 
                        WL.PRIORITY, 
                        WL.NCR,NCR_YN = CASE NCR WHEN 1 THEN 'YES' ELSE 'NO' END, 
                        WL.NCR_NUM, 
                        WL.NCR_YEAR, 
                        WL.DEPT_SHIP, 
                        WL.DEPT_OFFICE, 
                        WL.CATEGORY_NATURE, 
                        WL.CATEGORY_PRIMARY, 
                        WL.CATEGORY_SECONDARY, 
                        WL.CATEGORY_MINOR, 
                        WL.DEFER_TO_DD, 
                        WL.PIC, WL.REQSN_MSG_REF, 
                        WL.CREATED_DATE, 
                        WL.MODIFIED_DATE, 
                        WL.CREATED_BY, 
                        WL.OFFICE_ID, 
                        TEC_LIB_ASSIGNER.VALUE AS AssignorName, 
                        TEC_LIB_PRIORITY.VALUE AS PRIORITY, 
                        INF_LIB_INOFFICE_DEPT.VALUE AS INOFFICE_DEPT, 
                        INF_LIB_ONSHIP_DEPT.VALUE AS ONSHIP_DEPT, 
                        LIB_VESSELS.Vessel_Short_Name, 
                        LIB_VESSELS.Vessel_Name, 
                        LIB_VESSELS.Vessel_Code, 
                        LIB_USER.USER_NAME,
                        LIB_VESSELS.ISVESSEL,
                        (case when cast( getdate() - WL.MODIFIED_DATE as int) <= 3 then 1 else 0 end) as MODIFIED, isnull(FLAG_Tech_Meeting,0) as FLAG_Tech_Meeting, 
                        Flagged_By ,
                        ISNULL((SELECT     COUNT(0) AS Expr1 FROM TEC_WORKLIST_ATTACHMENTS
                              WHERE ACTIVE_STATUS=1 AND (Vessel_ID = WL.Vessel_ID) AND (Worklist_ID = WL.WORKLIST_ID) AND (WL_OFFICE_ID = WL.OFFICE_ID)), 0) AS AttachmentCount

                FROM         INF_LIB_ONSHIP_DEPT RIGHT OUTER JOIN
                      INF_LIB_INOFFICE_DEPT RIGHT OUTER JOIN
                      TEC_WORKLIST_MAIN AS WL LEFT OUTER JOIN
                      LIB_USER ON WL.PIC = LIB_USER.USERID LEFT OUTER JOIN
                      LIB_VESSELS ON WL.VESSEL_ID = LIB_VESSELS.Vessel_ID ON INF_LIB_INOFFICE_DEPT.ID = WL.DEPT_OFFICE ON 
                      INF_LIB_ONSHIP_DEPT.ID = WL.DEPT_SHIP LEFT OUTER JOIN
                      TEC_LIB_PRIORITY ON WL.PRIORITY = TEC_LIB_PRIORITY.ID LEFT OUTER JOIN
                      TEC_LIB_ASSIGNER ON WL.ASSIGNOR = TEC_LIB_ASSIGNER.ID
                WHERE WL.CATEGORY_NATURE <> 2492 ";


        //Response.Write(SQL);

        StringBuilder strBldQuery = new StringBuilder(SQL);
        if (Convert.ToInt32(ddlFleet.SelectedValue) != 0)
            strBldQuery.Append(" and LIB_VESSELS.FleetCode  =" + ddlFleet.SelectedValue);

        if (Convert.ToInt32(ddlVessels.SelectedValue) != 0)
            strBldQuery.Append(" and WL.VESSEL_ID  =" + ddlVessels.SelectedValue);

        if (Convert.ToInt32(ddldeptShip.SelectedValue) != 0)
            strBldQuery.Append(" and WL.DEPT_SHIP =" + ddldeptShip.SelectedValue);

        if (Convert.ToInt32(ddlOffice.SelectedValue) != 0)
            strBldQuery.Append(" and WL.DEPT_OFFICE =" + ddlOffice.SelectedValue);

        if (Convert.ToInt32(ddlPriority.SelectedValue) != 0)
            strBldQuery.Append(" and WL.priority =" + ddlPriority.SelectedValue);

        if (Convert.ToInt32(ddlNature.SelectedValue) != 0)
            strBldQuery.Append(" and WL.CATEGORY_NATURE =" + ddlNature.SelectedValue);

        if (Convert.ToString(ddlPrimary.SelectedValue) != "" && Convert.ToInt32(ddlPrimary.SelectedValue) != 0)
            strBldQuery.Append(" and WL.CATEGORY_PRIMARY =" + ddlPrimary.SelectedValue);

        if (Convert.ToString(ddlSecondary.SelectedValue) != "" && Convert.ToInt32(ddlSecondary.SelectedValue) != 0)
            strBldQuery.Append(" and WL.CATEGORY_SECONDARY =" + ddlSecondary.SelectedValue);

        if (rblJobStaus.Items[1].Selected == true)
            strBldQuery.Append(" and isnull(WL.DATE_COMPLETED,'1/1/1900 12:00:00 AM') != '1/1/1900 12:00:00 AM'");

        if (rblJobStaus.Items[2].Selected == true)
        {
            strBldQuery.Append(" and isnull(WL.DATE_COMPLETED,'1/1/1900 12:00:00 AM') = '1/1/1900 12:00:00 AM'");
        }

        //...............Start From & End Date Checking.........ddldeptShip_SelectedIndexChanged.........
        string mDdayFrom_Temp = txtFromDate.Text.ToString();
        string mDdayTo_Temp = txtToDate.Text.ToString();

        if (txtFromDate.Text != "" && txtToDate.Text != "")
            strBldQuery.Append(" and  WL.DATE_RAISED  BETWEEN convert(datetime, '" + mDdayFrom_Temp.ToString() + "', 103) and  convert(datetime, '" + mDdayTo_Temp + "', 103)");

        if (txtExpectedCompFrom.Text != "" && txtExpectedCompTo.Text != "")
            strBldQuery.Append(" and  WL.DATE_ESTMTD_CMPLTN  BETWEEN convert(datetime, '" + txtExpectedCompFrom.Text + "', 103) and  convert(datetime, '" + txtExpectedCompTo.Text + "', 103)");

        //...............End


        if (Convert.ToInt32(ddlAssignor.SelectedValue) != 0)
            strBldQuery.Append(" and WL.ASSIGNOR =" + ddlAssignor.SelectedValue);

        if (chkDrydock.Checked == true)
            strBldQuery.Append(" and  WL.DEFER_TO_DD= '-1'");
        //else
        //    strBldQuery.Append(" and WL.DEFER_TO_DD = 0");

        if (chkSentToShip.Checked == true)
            strBldQuery.Append(" AND  WORKLIST_ID = 0 ");
        else
            strBldQuery.Append(" AND   WORKLIST_ID > 0 ");

        if (txtJobCode.Text != "")
            strBldQuery.Append(" AND  WL.WORKLIST_ID = '" + txtJobCode.Text + "'");

        if (txtDescription.Text != "")
            strBldQuery.Append(" AND WL.JOB_DESCRIPTION LIKE '%" + txtDescription.Text + "%'");


        if (rblJobType.SelectedItem.Text.ToUpper() != "ALL")
            strBldQuery.Append(" and WL.NCR =" + rblJobType.SelectedValue);

        if (ddlPIC.SelectedValue != "0")
            strBldQuery.Append(" AND WL.PIC =" + ddlPIC.SelectedValue);

        if (Modified_in_Days != "0")
            strBldQuery.Append(" AND cast( getdate() - WL.MODIFIED_DATE as int) <= " + Modified_in_Days);

        if (chkRequisition.Checked == true)
            strBldQuery.Append(" AND (WL.REQSN_MSG_REF IS NOT NULL and LEN(REQSN_MSG_REF) > 0) ");

        if (chkFlaggedJobs.Checked == true)
            strBldQuery.Append(" AND WL.FLAG_Tech_Meeting = 1 ");

        if (Convert.ToInt32(ddlInspector.SelectedValue) != 0)
            strBldQuery.Append(" and WL.INSPECTOR  =" + ddlInspector.SelectedValue);
        
        strBldQuery.Append(" ORDER BY MODIFIED DESC, LIB_VESSELS.Vessel_Code, WL.WORKLIST_ID");

        //Response.Write (strBldQuery.ToString());
        return strBldQuery.ToString();

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

    protected void filter_grid()
    {
        try
        {

            string strCommand = GetQuery();

            Session["strQuery"] = strCommand;

            DataSet dtsSearchResult = objBLL.Get_FilterWorklist(strCommand);

            DataTable taskTable = new DataTable("TaskList");
            taskTable = dtsSearchResult.Tables[0];

            lblRecordCount.Text = dtsSearchResult.Tables[0].Rows.Count.ToString();

            Session["TaskTable"] = taskTable;

            grdJoblist.DataSource = dtsSearchResult;
            grdJoblist.DataBind();

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
        filter_grid();
    }
    
    protected void ddlNature_SelectedIndexChanged(object sender, EventArgs e)
    {
        hdnFlagCheck.Value = "false";
        BindPrimaryByNatureID(Convert.ToInt32(ddlNature.SelectedValue));
        filter_grid();
    }
    
    protected void ddlPrimary_SelectedIndexChanged(object sender, EventArgs e)
    {
        hdnFlagCheck.Value = "false";
        BindSecondaryByPrimaryID(Convert.ToInt32(ddlPrimary.SelectedValue));
        filter_grid();
    }
    
    protected void ddlSecondary_SelectedIndexChanged(object sender, EventArgs e)
    {
        hdnFlagCheck.Value = "false";
        BindMinorBySecondatyID(Convert.ToInt32(ddlSecondary.SelectedValue));
        filter_grid();
    }
     
    public void Load_FleetList()
    {
        int UserCompanyID = UDFLib.ConvertToInteger(Session["USERCOMPANYID"].ToString());
        ddlFleet.DataSource = objBLLVessel.GetFleetList(UserCompanyID);
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

        ddlVessels.DataSource = objBLLVessel.Get_VesselList(Fleet_ID, 0, Vessel_Manager, "", UserCompanyID);

        ddlVessels.DataTextField = "VESSEL_NAME";
        ddlVessels.DataValueField = "VESSEL_ID";
        ddlVessels.DataBind();
        ddlVessels.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));
        ddlVessels.SelectedIndex = 0;
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
            hdnFlagCheck.Value = "false";
            //Retrieve the table from the session object.
            DataTable dt = Session["TaskTable"] as DataTable;
            if (dt != null)
            {
                //Sort the data.
                dt.DefaultView.Sort = e.SortExpression + " " + GetSortDirection(e.SortExpression);
                grdJoblist.DataSource = Session["TaskTable"];
                grdJoblist.DataBind();
            }
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
            string msgSubject = "";
            string msgTo = "";
            string msgCC = "";
            string msgBCC = "";

            

            DataSet dtsJobDetails = objBLL.Get_JobDetails_ByID(OFFICE_ID, WORKLIST_ID, VESSEL_ID);

            if (dtsJobDetails.Tables[0].Rows.Count > 0)
            {
                if (dtsJobDetails.Tables[0].Rows[0]["IsVessel"].ToString() == "1")
                    msgSubject = "Worklist Job: " + dtsJobDetails.Tables[0].Rows[0]["WORKLIST_ID"].ToString() + "/" + dtsJobDetails.Tables[0].Rows[0]["VESSEL_SHORT_NAME"].ToString();
                else
                    msgSubject = "Worklist Job: " + dtsJobDetails.Tables[0].Rows[0]["WORKLIST_ID"].ToString() + "/" + dtsJobDetails.Tables[0].Rows[0]["VESSEL_SHORT_NAME"].ToString() + "/PIC:" + dtsJobDetails.Tables[0].Rows[0]["PIC_Name"].ToString();

                string msgBody = "";

                msgBody += "Vessel: " + dtsJobDetails.Tables[0].Rows[0]["vessel_name"].ToString();
                msgBody += "<br>Job Code: " + dtsJobDetails.Tables[0].Rows[0]["WORKLIST_ID"].ToString();
                msgBody += "<br>Description: " + dtsJobDetails.Tables[0].Rows[0]["JOB_DESCRIPTION"].ToString();


                msgBody += "<br><br><a href='http://" + Request.ServerVariables["SERVER_NAME"].ToString() + "/" + ConfigurationManager.AppSettings["APP_NAME"].ToUpper() + "/Technical/worklist/ViewJob.aspx?OFFID=" + OFFICE_ID.ToString() + "&WLID=" + WORKLIST_ID.ToString() + "&VID=" + VESSEL_ID.ToString() + "'>View Job Details</a>";

                msgBody += "<br><br>Regards,<br>" + Session["USERFULLNAME"].ToString();

                int MsgID = objCrew.Send_CrewNotification(0, 0, 0, 5, msgTo, msgCC, msgBCC, msgSubject, msgBody, "", "MAIL", "", GetSessionUserID(), "DRAFT");
                if (MsgID > 0)
                {
                    string js = "window.open('../../Crew/EmailEditor.aspx?ID=" + MsgID + "&Discard=1');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "SendMail" + MsgID, js, true);
                }


            }

        }
        catch (Exception ex)
        {
            string js = "alert('Error in loading data!! Error: " + ex.Message + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "errorloadingedit", js, true);

        }
    }
    
    protected void EditJob(object sender, EventArgs e)
    {
        hdnFlagCheck.Value = "false";
        //filter_grid();
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

        filter_grid();
    }
    protected void ImgBtnClearFilter_Click(object sender, EventArgs e)
    {
        string toDate = Convert.ToString(DateTime.Now.ToString("dd/MM/yyyy"));
        string fromDate = Convert.ToString(DateTime.Now.AddMonths(-6).ToString("dd/MM/yyyy"));


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
        txtJobCode.Text = string.Empty;

        rblJobStaus.SelectedIndex = 2;
        rblJobType.SelectedIndex = 0;
        chkDrydock.Checked = false;

        hdnFlagCheck.Value = "false";
        txtExpectedCompTo.Text = "";
        txtExpectedCompFrom.Text = "";


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

        filter_grid();
    }

    protected void ImgBtnSearch_Click(object sender, EventArgs e)    
    {
        filter_grid();
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
        string js = "";

        if (ddlReports.SelectedValue == "0")
        {
            js = "window.open('../reports/reportJobList.aspx?Export=1');";

        }
        else
        {
            js = "window.open('../reports/reportJobProgress.aspx?Export=1');";

        }

        ScriptManager.RegisterStartupScript(this, this.GetType(), "showReport2", js, true);
    }
    

    protected void grdJoblist_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdJoblist.PageIndex = e.NewPageIndex;
        filter_grid();
        //lblPageStatus.Text = (GridView1.PageIndex + 1).ToString() + " of " + GridView1.PageCount.ToString();
    }

    protected void grdJoblist_RowDataBound(object sender, GridViewRowEventArgs e)
    {
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
            filter_grid();
        }
        else if (e.CommandName.ToUpper() == "ADD_FOLLOWUP")
        {
            string[] arg = e.CommandArgument.ToString().Split(',');

            int Vessel_ID = UDFLib.ConvertToInteger(arg[0]);
            int Worklist_ID = UDFLib.ConvertToInteger(arg[1]);
            int Office_ID = UDFLib.ConvertToInteger(arg[2]);

            hdnVesselID.Value = Vessel_ID.ToString();
            hdnWorklistlID.Value = Worklist_ID.ToString();
            hdnOfficeID.Value = Office_ID.ToString();

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
        filter_grid();
    }
    protected void btnSaveFollowUpAndClose_OnClick(object sender, EventArgs e)
    {
        try
        {
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
}