using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Infrastructure;
using System.Data;
using System.Text;

using System.Web.UI.HtmlControls;
using SMS.Properties;

using System.Configuration;
using System.Net;
using System.IO;
using SMS.Business.Inspection;
using System.Diagnostics;
using System.Collections;
public partial class Technical_Worklist_SuperintendentInspection : System.Web.UI.Page
{

    UserAccess objUA = new UserAccess();
    BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
    MergeGridviewHeader_Info objMegeHead = new MergeGridviewHeader_Info();
    BLL_Infra_VesselLib objVsl = new BLL_Infra_VesselLib();
    BLL_Infra_Port objInfra = new BLL_Infra_Port();
    BLL_INSP_Checklist objBllChecklist = new BLL_INSP_Checklist();
    BLL_Tec_Inspection objInsp = new BLL_Tec_Inspection();
    string APP_URL = ConfigurationManager.AppSettings["APP_URL"].ToString();
    public int InspectionId = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (UDFLib.GetSessionUserID() == 0)
            Response.Redirect("~/account/login.aspx?ReturnUrl=" + Request.Path.ToString());


        UserAccessValidation();
        objMegeHead.AddMergedColumns(new int[] { 1, 2 }, "Inspection", "HeaderStyle-css HeadetTHStyle");
        objMegeHead.AddMergedColumns(new int[] { 8, 9, 10 }, "Duration", "HeaderStyle-css HeadetTHStyle");
        if (!IsPostBack)
        {
            if (Session["COMPANYTYPE"].ToString().ToUpper() == "SURVEYOR")
            {
                lblFleet.Text = "Company :";
                DDLFleet.Visible = false;
                ddlVessel_Manager.Visible = true;
            }

            FillDDL();
            if (Request.QueryString.Count == 3)
            {
                txtFromDate.Text = Convert.ToDateTime(Request.QueryString[0]).ToString("dd/MMM/yy");
                txtToDate.Text = Convert.ToDateTime(Request.QueryString[1]).ToString("dd/MMM/yy");
                DDLVessel.SelectedValue = Request.QueryString[2];
            }

            string CompanyID = Session["USERCOMPANYID"].ToString();
            // removed DisplayReportIcon

            if (Request.QueryString.Count > 0 && Request.QueryString["InspectionId"] != "")
            {
                InspectionId = Convert.ToInt16(Request.QueryString["InspectionId"]);
            }
            Load_Current_Schedules();
        }

    }


    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
            Response.Redirect("~/default.aspx?msgid=1");

        if (objUA.Add == 0)
        {
            btnScheduleInspection.Enabled = false;

        }

        ViewState["del"] = objUA.Delete;
        ViewState["edit"] = objUA.Edit;

    }
    protected bool GetAccessInfo(string s, string d, string c)
    {
        try
        {
            if (s == "d")
            {
                if (ViewState["del"].ToString() == "0")
                {
                    return false;
                }
                else
                {
                    if (c == "1")
                    {
                        if (d.ToString().Length > 0)
                        {
                            return false;
                        }
                    }
                    return true;
                }
            }
            if (s == "e")
            {
                if (ViewState["edit"].ToString() == "0")
                {
                    return false;
                }
                else
                {
                    if (c == "1")
                    {
                        if (d.ToString().Length > 0)
                        {
                            return false;
                        }
                    }
                    return true;
                }
            }

            return true;
        }
        catch (Exception)
        {

            return true;
        }

    }
    protected void DDLInspector_SelectedIndexChanged(object sender, EventArgs e)
    {
        Load_Current_Schedules();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Load_Current_Schedules();
    }


    protected void DDLFleet_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            int Vessel_Manager = 0;
            if (Session["COMPANYTYPE"].ToString().ToUpper() == "SURVEYOR")
            {

                Vessel_Manager = Convert.ToInt32(ddlVessel_Manager.SelectedValue);
            }

            DataTable dtVessel = objVsl.Get_VesselList(UDFLib.ConvertToInteger(DDLFleet.SelectedValue), 0, Vessel_Manager, "", Convert.ToInt32(Session["USERCOMPANYID"].ToString()));
            DDLVessel.Items.Clear();
            DDLVessel.DataSource = dtVessel;
            DDLVessel.DataTextField = "Vessel_name";
            DDLVessel.DataValueField = "Vessel_id";
            DDLVessel.DataBind();
            ListItem li = new ListItem("--SELECT--", "0");
            DDLVessel.Items.Insert(0, li);

            Load_Current_Schedules();

        }
        catch (Exception ex)
        {

        }
    }
    protected void DDLVessel_SelectedIndexChanged(object sender, EventArgs e)
    {
        Load_Current_Schedules();
    }

    protected void gvInspecrionSchedule_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {


            if (e.CommandName == "onDelete")
            {
                objInsp.Del_ScheduledInspection(UDFLib.ConvertIntegerToNull(e.CommandArgument.ToString()), UDFLib.ConvertIntegerToNull(Session["USERID"]));
                Load_Current_Schedules();
            }
            if (e.CommandName == "ReOpen")
            {
                ViewState["SchDetailId"] = UDFLib.ConvertToInteger(e.CommandArgument.ToString().Split(';')[0]); ;
            }
            if (e.CommandName.ToUpper() == "EDIT_SCHEDULE")
            {

            }
            if (e.CommandName.ToUpper() == "INSPUPD")
            {
                if (ViewState["edit"].ToString() == "0")
                    btnUpdateInspection.Enabled = false;

                ViewState["ShowImages"] = 0;
                ViewState["SchDetailId"] = 1;
                string js;

                DataTable dtSup = objInsp.Get_Supritendent_Users(null, UDFLib.ConvertToInteger(e.CommandArgument.ToString().Split(';')[8]));

                if (dtSup != null)
                {
                    if (dtSup.Rows.Count > 0)
                    {



                        DDLInspectorA.DataSource = dtSup;
                        DDLInspectorA.DataTextField = "Name";
                        DDLInspectorA.DataValueField = "UserID";
                        DDLInspectorA.DataBind();
                        DDLInspectorA.Items.Insert(0, new ListItem("--SELECT--", null));
                        txtInsectionDate.Text = DateTime.Now.ToString("dd/MMM/yy");


                        ViewState["ScheduleID"] = UDFLib.ConvertToInteger(e.CommandArgument.ToString().Split(';')[0]);
                        ViewState["SchDetailId"] = UDFLib.ConvertToInteger(e.CommandArgument.ToString().Split(';')[2]);
                        try
                        {
                            try
                            {
                                DDLInspectorA.SelectedValue = e.CommandArgument.ToString().Split(';')[1];
                            }
                            catch (Exception)
                            {

                                DDLInspectorA.SelectedValue = e.CommandArgument.ToString().Split(';')[4];
                            }
                        }
                        catch (Exception)
                        {

                            DDLInspectorA.SelectedIndex = 0;
                        }

                        try
                        {
                            txtInsectionDate.Text = Convert.ToDateTime(e.CommandArgument.ToString().Split(';')[5]).ToString("dd/MMM/yy");
                        }
                        catch (Exception)
                        {

                            txtInsectionDate.Text = Convert.ToDateTime(e.CommandArgument.ToString().Split(';')[6]).ToString("dd/MMM/yy");
                        }

                        try
                        {
                            hdfNewinspectionPortid.Value = "";
                            DataSet dtAssignPortList = objInsp.INSP_Get_InspectionPort(UDFLib.ConvertToInteger(e.CommandArgument.ToString().Split(';')[2]));
                            DataTable dtPort = objInfra.Get_PortList();

                            DDLPort.DataSource = dtPort;
                            DDLPort.DataValueField = "PORT_ID";
                            DDLPort.DataTextField = "PORT_NAME";
                            DDLPort.DataBind();
                            DDLPort.Items.Insert(0, new ListItem("-SELECT-", null));

                            if (dtAssignPortList.Tables[0].Rows.Count > 0)
                            {
                                try
                                {
                                    DDLPort.SelectedValue = dtAssignPortList.Tables[0].Rows[0]["PortID"].ToString();
                                }
                                catch { }
                            }
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }


                        txtDurJobsU.Text = e.CommandArgument.ToString().Split(';')[7].ToString();
                        txtDuration.Text = e.CommandArgument.ToString().Split(';')[7].ToString();
                        DateTime dt = Convert.ToDateTime(txtInsectionDate.Text);
                        dt = dt.AddDays(UDFLib.ConvertToDouble(txtDuration.Text) - 1);
                        txtCompletionDate.Text = dt.ToString("dd/MMM/yy");
                        txtOnboard.Text = "";
                        txtOnShore.Text = "";
                        txtDuration.Text = "";
                        rdbCompleted.Checked = false;
                        rdbPlanned.Checked = true;

                        js = " $('#divAddSystem').prop('title', 'Update Inspection');showModal('dvUpdateInspe',false);";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "showModalJS", js, true);

                        js = " $('#dvUpdateInspe').prop('title', 'Update Inspection');";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "", js, true);

                        js = "handleStatus();";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "handleModal", js, true);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }

    }
    public void FillDDL()
    {
        try
        {

            BLL_Infra_InspectionType onjInsp = new BLL_Infra_InspectionType();
            DataTable FleetDT = objVsl.GetFleetList(Convert.ToInt32(Session["USERCOMPANYID"].ToString()));
            DDLFleet.DataSource = FleetDT;
            DDLFleet.DataTextField = "Name";
            DDLFleet.DataValueField = "code";
            DDLFleet.DataBind();
            ListItem li = new ListItem("--SELECT ALL--", "0");
            DDLFleet.Items.Insert(0, li);

            BLL_Infra_Company objInfra = new BLL_Infra_Company();
            ddlVessel_Manager.DataSource = objInfra.Get_Company_Parent_Child(1, 0, 0);
            ddlVessel_Manager.DataTextField = "COMPANY_NAME";
            ddlVessel_Manager.DataValueField = "ID";
            ddlVessel_Manager.DataBind();
            ddlVessel_Manager.Items.Insert(0, new ListItem("-Select All-", "0"));



            DataTable dtVessel = objVsl.Get_VesselList(0, 0, Convert.ToInt32(Session["USERCOMPANYID"].ToString()), "", Convert.ToInt32(Session["USERCOMPANYID"].ToString()));
            DDLVessel.DataSource = dtVessel;
            DDLVessel.DataTextField = "Vessel_name";
            DDLVessel.DataValueField = "Vessel_id";
            DDLVessel.DataBind();
            DDLVessel.Items.Insert(0, new ListItem("--SELECT--", null));





            DataTable dtSup = objInsp.Get_Supritendent_Users(null, null);
            DDLInspector.DataSource = dtSup;
            DDLInspector.DataTextField = "Name";
            DDLInspector.DataValueField = "UserID";
            DDLInspector.DataBind();
            DDLInspector.Items.Insert(0, new ListItem("--SELECT--", null));

            DDLInspectorA.DataSource = dtSup;
            DDLInspectorA.DataTextField = "Name";
            DDLInspectorA.DataValueField = "UserID";
            DDLInspectorA.DataBind();
            DDLInspectorA.Items.Insert(0, new ListItem("--SELECT--", null));

            DataTable dtInsp = onjInsp.Get_InspectionTypeList();


            ddlInspectionType.DataSource = dtInsp;
            ddlInspectionType.DataTextField = "InspectionTypeName";
            ddlInspectionType.DataValueField = "InspectionTypeId";
            ddlInspectionType.DataBind();
            ddlInspectionType.Items.Insert(0, new ListItem("--SELECT--", null));

        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    protected void Load_Current_Schedules()
    {
        try
        {
            string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
            string sortdirection = (ViewState["SORTDIRECTION"] == null) ? "" : (ViewState["SORTDIRECTION"].ToString() == "1") ? " DESC" : "ASC";
            string Sort_Expression = sortbycoloumn + sortbycoloumn;

            string Frequency_Type = ddlScheduleType.SelectedIndex <= 0 ? null : ddlScheduleType.SelectedValue;
            int Last_Run_Success = -1;
            string Status = null;
            string SearchText = "";
            int? InspectionType = null;
            int? OverDue = null;
            if (ddlStatus.SelectedIndex > 0)
            {
                Status = ddlStatus.SelectedValue;
                if (ddlStatus.SelectedValue == "Overdue")
                {
                    Status = "Pending";
                    OverDue = 1;
                }
            }
            if (ddlInspectionType.SelectedIndex > 0)
            {
                InspectionType = UDFLib.ConvertToInteger(ddlInspectionType.SelectedValue);
            }

            if (ViewState["SORTBYCOLOUMN"] == null)
                ViewState["SORTBYCOLOUMN"] = "Schedule_Date";
            if (ViewState["SORTDIRECTION"] == null)
                ViewState["SORTDIRECTION"] = "desc";

            string sortexp = ViewState["SORTBYCOLOUMN"].ToString() + " " + ViewState["SORTDIRECTION"].ToString();
            int is_Fetch_Count = ucCustomPagerItems.isCountRecord;
            DataSet ds = objInsp.Get_Current_Schedules(InspectionId, Frequency_Type, Last_Run_Success, Status, SearchText, GetSessionUserID(), ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref is_Fetch_Count, UDFLib.ConvertIntegerToNull(DDLVessel.SelectedIndex == 0 ? null : DDLVessel.SelectedValue), UDFLib.ConvertIntegerToNull(DDLInspector.SelectedIndex == 0 ? null : DDLInspector.SelectedValue), UDFLib.ConvertDateToNull(txtFromDate.Text), UDFLib.ConvertDateToNull(txtToDate.Text), InspectionType, OverDue, sortexp, UDFLib.ConvertIntegerToNull(DDLFleet.SelectedIndex == 0 ? null : DDLFleet.SelectedValue));

            tbl_Inspection.Visible = false;
            if (ds != null)
            {
                if (InspectionId != 0)
                {
                    tbl_Inspection.Visible = true;
                    lblCertificateName.Text = Convert.ToString(Request.QueryString["CertificateName"]);
                }

                gvInspecrionSchedule.DataSource = ds.Tables[0];
                gvInspecrionSchedule.DataBind();

                ucCustomPagerItems.CountTotalRec = is_Fetch_Count.ToString();
                ucCustomPagerItems.BuildPager();
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    protected void grdJoblist_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {
            hdnFlagCheck.Value = "false";
            DataTable dt = Session["TaskTable"] as DataTable;
            if (dt != null)
            {
                dt.DefaultView.Sort = e.SortExpression + " " + GetSortDirection(e.SortExpression);
                grdJoblist.DataSource = Session["TaskTable"];
                grdJoblist.DataBind();
            }
        }
        catch (Exception)
        {
        }
    }
    protected void grdJoblist_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdJoblist.PageIndex = e.NewPageIndex;

    }
    protected void UpdateWorklistChecklist()
    {
        try
        {


            DataTable dtInpectionSchedule = ((DataTable)Session["dtInpectionSchedule"]);

            int InspDetId = 0;

            foreach (GridViewRow item in grdJoblist.Rows)
            {
                if (dtInpectionSchedule != null)
                {
                    if (dtInpectionSchedule.Rows.Count > 0)
                    {
                        DataRow[] dr = dtInpectionSchedule.Select("WORKLIST_ID='" + grdJoblist.DataKeys[item.RowIndex][0].ToString() + "' and VESSEL_ID='" + grdJoblist.DataKeys[item.RowIndex][1].ToString() + "' and OFFICE_ID='" + grdJoblist.DataKeys[item.RowIndex][2].ToString() + "'");
                        if (((CheckBox)grdJoblist.Rows[item.RowIndex].FindControl("checkRow")).Checked)
                        {
                            if (dr.Length > 0)
                            {


                                dr[0]["WORKLIST_ID"] = grdJoblist.DataKeys[item.RowIndex][0].ToString();
                                dr[0]["VESSEL_ID"] = grdJoblist.DataKeys[item.RowIndex][1].ToString();
                                dr[0]["OFFICE_ID"] = grdJoblist.DataKeys[item.RowIndex][2].ToString();
                                dr[0]["InspectionDetailId"] = ViewState["SchDetailId"];
                            }
                            else
                            {
                                dtInpectionSchedule.Rows.Add(grdJoblist.DataKeys[item.RowIndex][0], grdJoblist.DataKeys[item.RowIndex][1], grdJoblist.DataKeys[item.RowIndex][2], ViewState["SchDetailId"]);
                            }
                        }
                        else
                        {
                            if (dr.Length > 0)
                            {
                                dtInpectionSchedule.Rows.Remove(dr[0]);
                            }


                        }
                    }
                    else
                    {
                        if (((CheckBox)grdJoblist.Rows[item.RowIndex].FindControl("checkRow")).Checked)
                        {
                            dtInpectionSchedule.Rows.Add(grdJoblist.DataKeys[item.RowIndex][0], grdJoblist.DataKeys[item.RowIndex][1], grdJoblist.DataKeys[item.RowIndex][2], ViewState["SchDetailId"]);
                        }

                    }
                }

            }
            Session["dtInpectionSchedule"] = dtInpectionSchedule;
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
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
                imgRemarks.Attributes.Add("onmouseover", "showFollowups(" + Vessel_ID + "," + Worklist_ID + "," + WL_Office_ID + ",this)");
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
    protected void Search_Worklist()
    {
        try
        {
            UpdateWorklistChecklist();
            DataTable dtFilter = new DataTable();
            dtFilter.Columns.Add("PRM_NAME", typeof(string));
            dtFilter.Columns.Add("PRM_VALUE", typeof(object));

            dtFilter.Rows.Add(new object[] { "@FLEET_ID", UDFLib.ConvertIntegerToNull(null) });
            dtFilter.Rows.Add(new object[] { "@VESSEL_ID", UDFLib.ConvertIntegerToNull(ViewState["VID"]) });
            dtFilter.Rows.Add(new object[] { "@ASSIGNOR", UDFLib.ConvertIntegerToNull(null) });
            dtFilter.Rows.Add(new object[] { "@DEPT_SHIP", UDFLib.ConvertIntegerToNull(null) });
            dtFilter.Rows.Add(new object[] { "@DEPT_OFFICE", UDFLib.ConvertIntegerToNull(null) });
            dtFilter.Rows.Add(new object[] { "@PRIORITY", UDFLib.ConvertIntegerToNull(null) });
            dtFilter.Rows.Add(new object[] { "@CATEGORY_NATURE", UDFLib.ConvertIntegerToNull(null) });
            dtFilter.Rows.Add(new object[] { "@CATEGORY_PRIMARY", UDFLib.ConvertIntegerToNull(null) });
            dtFilter.Rows.Add(new object[] { "@CATEGORY_SECONDARY", UDFLib.ConvertIntegerToNull(null) });
            dtFilter.Rows.Add(new object[] { "@CATEGORY_MINOR", null, });
            dtFilter.Rows.Add(new object[] { "@JOB_DESCRIPTION", UDFLib.ConvertStringToNull(null) });
            dtFilter.Rows.Add(new object[] { "@JOB_STATUS", UDFLib.ConvertStringToNull(rblJobStaus.SelectedValue) });
            dtFilter.Rows.Add(new object[] { "@JOB_TYPE", UDFLib.ConvertIntegerToNull(null) });
            dtFilter.Rows.Add(new object[] { "@PIC", UDFLib.ConvertIntegerToNull(null) });
            dtFilter.Rows.Add(new object[] { "@JOB_MODIFIED_IN", UDFLib.ConvertIntegerToNull(null) });
            dtFilter.Rows.Add(new object[] { "@DATE_RAISED_FROM", UDFLib.ConvertDateToNull(null) });
            dtFilter.Rows.Add(new object[] { "@DATE_RAISED_TO", UDFLib.ConvertDateToNull(null) });
            dtFilter.Rows.Add(new object[] { "@DATE_CMPLTN_FROM", UDFLib.ConvertDateToNull(null) });
            dtFilter.Rows.Add(new object[] { "@DATE_CMPLTN_TO", UDFLib.ConvertDateToNull(null) });
            dtFilter.Rows.Add(new object[] { "@DEFER_TO_DD", (null) });
            dtFilter.Rows.Add(new object[] { "@SENT_TO_SHIP", (null) });
            dtFilter.Rows.Add(new object[] { "@HAVING_REQ_NO", (null) });
            dtFilter.Rows.Add(new object[] { "@FLAGGED_FOR_MEETING", (null) });
            dtFilter.Rows.Add(new object[] { "@INSPECTOR", UDFLib.ConvertIntegerToNull(null) });
            dtFilter.Rows.Add(new object[] { "@PAGE_INDEX", ucCustomPagerctp.CurrentPageIndex });
            dtFilter.Rows.Add(new object[] { "@PAGE_SIZE", ucCustomPagerctp.PageSize });

            int Record_Count = 0;

            DataTable taskTable = objInsp.Get_WorkList_Index(dtFilter, ref Record_Count);

            grdJoblist.DataSource = taskTable;
            grdJoblist.DataBind();

            ucCustomPagerctp.CountTotalRec = Record_Count.ToString();
            ucCustomPagerctp.BuildPager();

            DataTable dtPKIDs = taskTable.DefaultView.ToTable(true, new string[] { "WORKLIST_ID", "VESSEL_ID", "OFFICE_ID" });
            dtPKIDs.PrimaryKey = new DataColumn[] { dtPKIDs.Columns["WORKLIST_ID"], dtPKIDs.Columns["VESSEL_ID"], dtPKIDs.Columns["OFFICE_ID"] };
            Session["WORKLIST_PKID_NAV"] = dtPKIDs;

            lblRecordCount.Text = Record_Count.ToString();

        }
        catch (Exception ex)
        {
            ////.WriteError(this.GetType().Name.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), ex);
            string js = "alert('Error in loading data!! Error: " + UDFLib.ReplaceSpecialCharacter(ex.Message) + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }

    protected void grdJoblist_RowCommand(object sender, GridViewCommandEventArgs e)
    {


    }
    protected void Filter_Changed(object sender, EventArgs e)
    {
        hdnFlagCheck.Value = "false";
        Search_Worklist();
    }

    protected bool SelectCheckbox(string WORKLIST_ID, string VESSEL_ID, string OFFICE_ID)
    {
        DataTable dtInpectionSchedule = ((DataTable)Session["dtInpectionSchedule"]);

        if (dtInpectionSchedule != null)
            if (dtInpectionSchedule.Rows.Count > 0)
                if (dtInpectionSchedule.Select("WORKLIST_ID='" + WORKLIST_ID + "' and VESSEL_ID='" + VESSEL_ID + "' and OFFICE_ID='" + OFFICE_ID + "'").Length > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
        return false;

    }

    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }
    protected void PopulateInspectionTable(DataTable dtInpectionSchedule)
    {

    }
    public int GetSettingValue(DataTable dtSettings, string Key, int DefaultValue)
    {
        int Value = DefaultValue;

        DataRow[] drsSet = dtSettings.Select("Key_Name='" + Key + "'");
        if (drsSet.Length > 0)
        {
            try
            {
                Value = Convert.ToInt32(drsSet[0]["Key_Value_Int"]);
            }
            catch (Exception)
            {

                return Value;
            }

        }
        return Value;
    }
    public string GetSettingDateValue(DataTable dtSettings, string Key, string DefaultValue)
    {
        string Value = DefaultValue;

        DataRow[] drsSet = dtSettings.Select("Key_Name='" + Key + "'");
        if (drsSet.Length > 0)
        {
            if (drsSet[0]["Key_Value_Date"].ToString().Trim().Count() != 0)
            {
                try
                {
                    Value = Convert.ToDateTime(drsSet[0]["Key_Value_Date"]).ToString("dd/MMM/yy");
                }
                catch (Exception)
                {
                    return Value;
                }

            }


        }
        return Value;
    }
    public string GetSettingString(DataTable dtSettings, string Key, string DefaultValue)
    {
        string Value = DefaultValue;

        DataRow[] drsSet = dtSettings.Select("Key_Name='" + Key + "'");
        if (drsSet.Length > 0)
        {
            Value = drsSet[0]["Key_Value_String"].ToString();
        }
        return Value;
    }




    protected void btnAssign_Click(object sender, EventArgs e)
    {
        try
        {
            UpdateWorklistChecklist();
            DataTable dtInpectionSchedule = ((DataTable)Session["dtInpectionSchedule"]);
            objInsp.Save_InspectionWorklist(dtInpectionSchedule, GetSessionUserID());
            Load_Current_Schedules();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertjs", "alert('Worklist assigned successfully.');", true);
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    protected void btnAssignandClose_Click(object sender, EventArgs e)
    {

        UpdateWorklistChecklist();
        DataTable dtInpectionSchedule = ((DataTable)Session["dtInpectionSchedule"]);

        if (dtInpectionSchedule.Rows.Count == 0)
        {
            DataRow lRow = dtInpectionSchedule.NewRow();
            lRow["WORKLIST_ID"] = -1;
            lRow["InspectionDetailId"] = ViewState["SchDetailId"];
            dtInpectionSchedule.Rows.Add(lRow);
        }

        objInsp.Save_InspectionWorklist(dtInpectionSchedule, GetSessionUserID());
        string js = "alert('Worklist assigned successfully.');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertjs", js, true);
        Load_Current_Schedules();
        js = "hideModal('dvWorklist');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "hideModalJS", js, true);
    }

    protected void btnGenerateReport_Click(object sender, EventArgs e)
    {
        string url = "window.open('SupdtInspReport.aspx?SchDetailId=" + Convert.ToInt32(ViewState["SchDetailId"]) + "&ShowImages=" + Convert.ToInt32(ViewState["ShowImages"]) + "');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "script1", url, true);
    }
    protected void txtToDate_TextChanged(object sender, EventArgs e)
    {
        Load_Current_Schedules();
    }
    protected void txtFromDate_TextChanged(object sender, EventArgs e)
    {
        Load_Current_Schedules();
    }
    protected void btnUpdateInspection_Click(object sender, EventArgs e)
    {
        try
        {


            string js;

            if (UDFLib.ConvertDateToNull(txtInsectionDate.Text) == null)
            {
                js = "handleStatus();alert('Select valid date.');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "hideModalJS", js, true);
                return;
            }
            if (DDLInspectorA.SelectedIndex == 0)
            {
                js = "handleStatus();alert('Select valid inspector.');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "hideModalJS", js, true);
                return;
            }
            int onboard = 0;
            int onshore = 0;
            string Status = "Pending";


            if (rdbPlanned.Checked)
            {
                if (UDFLib.ConvertIntegerToNull(txtDurJobsU.Text) == null)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "", "handleStatus();alert('Duration is not valid.');", true);
                    return;
                }
                if (UDFLib.ConvertIntegerToNull(txtDurJobsU.Text) <= 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "", "handleStatus();alert('Duration cannot be smaller than or equal to zero.');", true);
                    return;
                }

            }
            if (rdbCompleted.Checked)
            {
                if (UDFLib.ConvertDateToNull(txtCompletionDate.Text) != null)
                {
                    if (UDFLib.ConvertDateToNull(txtCompletionDate.Text).Value.Date < UDFLib.ConvertDateToNull(txtInsectionDate.Text).Value.Date)
                    {

                        txtCompletionDate.Text = "";
                        txtOnboard.Text = "";
                        txtOnShore.Text = "";
                        txtDuration.Text = "";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "", "handleStatus();alert('Completion date can not be lesser than the scheduled date.');", true);
                        return;
                    }
                    txtDuration.Text = (UDFLib.ConvertDateToNull(txtCompletionDate.Text).Value.Date.AddDays(1) - UDFLib.ConvertDateToNull(txtInsectionDate.Text).Value.Date).Days.ToString();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "", "alert('Completion date can not be empty.');handleStatus();", true);
                    return;
                }

                Status = "Completed";

                try
                {
                    onboard = Convert.ToInt32(txtOnboard.Text);
                }
                catch (Exception)
                {

                    js = "handleStatus();alert('Onboard duration is not valid.');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "hideModalJS", js, true);
                    return;
                }

                try
                {
                    onshore = Convert.ToInt32(txtOnShore.Text);
                }
                catch (Exception)
                {

                    js = "handleStatus();alert('Onshore duration is not valid.');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "hideModalJS", js, true);
                    return;
                }

                int duration = Convert.ToInt32(txtDuration.Text);

                if (duration != (onboard + onshore))
                {
                    js = "handleStatus();alert('Sum of onboard value and onshore should match duration value.');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "hideModalJS", js, true);
                    return;
                }
                if ((Convert.ToInt32(txtOnShore.Text) < 0) || (Convert.ToInt32(txtOnboard.Text) < 0))
                {
                    js = "handleStatus();alert('Onboard value or onshore value can not be negative.');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "hideModalJS", js, true);
                    return;
                }

                if (UDFLib.ConvertDateToNull(txtCompletionDate.Text).Value.Date > DateTime.Now.Date)
                {
                    js = "handleStatus();alert('Completion date cannot be a future date.');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", js, true);
                    return;
                }
            }

            if (DDLPort.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "handleStatus();alert('No ports selected.');", true);
                return;
            }


            objInsp.Update_Inspection(UDFLib.ConvertToInteger(ViewState["SchDetailId"]), UDFLib.ConvertToInteger(DDLInspectorA.SelectedValue), UDFLib.ConvertDateToNull(txtInsectionDate.Text), Status, GetSessionUserID(), onboard, onshore, UDFLib.ConvertDateToNull(txtCompletionDate.Text), UDFLib.ConvertIntegerToNull(txtDurJobsU.Text));

            DataTable dtPortList = NewDaemonPortList();
            dtPortList.Rows.Add(DDLPort.SelectedValue.ToString());

            foreach (string newPortid in hdfNewinspectionPortid.Value.Split(','))
            {
                if (UDFLib.ConvertToInteger(newPortid) > 0 && dtPortList.Rows.Find(UDFLib.ConvertToInteger(newPortid)) == null)
                    dtPortList.Rows.Add((dtPortList.NewRow()["PortID"] = newPortid));
            }


            objInsp.INSP_InsertUpdate_InspectionPort(dtPortList, UDFLib.ConvertToInteger(ViewState["SchDetailId"]), GetSessionUserID());
            js = "handleStatus();alert('Inspection updated successfully.');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "hideModalJS", js, true);

            js = "handleStatus();hideModal('dvUpdateInspe');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "dvUpdateInspe", js, true);
            Load_Current_Schedules();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }


    protected bool GetUpdateEnabled(int Length)
    {
        if (Length == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    protected void gvInspecrionSchedule_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //code removed for dynamic icon check in view history
            if (((Label)e.Row.FindControl("lblInspectionDate")).Text.Trim().Length == 0 || ((Label)e.Row.FindControl("lblInspectionDate")).Text.Trim() == "&nbsp;")
            {
                if (DateTime.Now > Convert.ToDateTime(((LinkButton)e.Row.FindControl("lblInspectionScheduleDate")).Text))
                    e.Row.Cells[1].CssClass = "OverDueCell";
            }
            else
            {
                Label lnkInspector = e.Row.FindControl("lnkInspector") as Label;
                lnkInspector.Text = ((Label)e.Row.FindControl("lblActualInspectorName")).Text;
            }

            if (((Label)e.Row.FindControl("lblActualInspectorName")).Text.Trim().Length != 0)// || ((Label)e.Row.FindControl("lblActualInspectorName")).Text.Trim() != "&nbsp;")
            {
                Label lnkInspector = e.Row.FindControl("lnkInspector") as Label;
                lnkInspector.Text = ((Label)e.Row.FindControl("lblActualInspectorName")).Text;
            }

            /// If scheduld is 'One time ' don't allow to change.
            LinkButton lnkbtn = e.Row.FindControl("lnkEdit") as LinkButton;
            if (lnkbtn.Text == "Onetime")
                lnkbtn.OnClientClick = "return false";

        }
        if (e.Row.RowIndex == gvInspecrionSchedule.Rows.Count - 1)
        {

            gvInspecrionSchedule.Columns[4].Visible = false;
            gvInspecrionSchedule.Columns[5].Visible = false;
        }


        if (e.Row.RowType == DataControlRowType.Header)
        {
            if (ViewState["SORTBYCOLOUMN"] != null)
            {
                HtmlImage img = (HtmlImage)e.Row.FindControl(ViewState["SORTBYCOLOUMN"].ToString());
                if (img != null)
                {
                    if (ViewState["SORTDIRECTION"] == null || ViewState["SORTDIRECTION"].ToString() == "desc")
                        img.Src = "~/Images/arrowDown.png";
                    else
                        img.Src = "~/Images/arrowUp.png";

                    img.Visible = true;
                }
            }
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtFromDate.Text = "";
        txtToDate.Text = "";
        DDLFleet.SelectedIndex = 0;
        DDLVessel.SelectedIndex = 0;
        DDLInspector.SelectedIndex = 0;
        ddlInspectionType.SelectedIndex = 0;
        ddlStatus.SelectedIndex = 0;
        ddlScheduleType.SelectedIndex = 0;
        Load_Current_Schedules();
    }
    protected void rdbPlanned_CheckedChanged(object sender, EventArgs e)
    {

    }
    protected void txtCompletionDate_TextChanged(object sender, EventArgs e)
    {
        if (rdbCompleted.Checked)
        {

            if (UDFLib.ConvertDateToNull(txtCompletionDate.Text) != null)
            {
                if (UDFLib.ConvertDateToNull(txtCompletionDate.Text).Value.Date < UDFLib.ConvertDateToNull(txtInsectionDate.Text).Value.Date)
                {

                    txtCompletionDate.Text = "";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "", "alert('Completion date can not be smaller than the scheduled Date')", true);
                    return;
                }
                txtDuration.Text = (UDFLib.ConvertDateToNull(txtCompletionDate.Text).Value.Date.AddDays(1) - UDFLib.ConvertDateToNull(txtInsectionDate.Text).Value.Date).Days.ToString();
            }
        }



    }
    protected void gvInspecrionSchedule_Sorting(object sender, GridViewSortEventArgs se)
    {

        ViewState["SORTBYCOLOUMN"] = se.SortExpression;

        if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "asc")
            ViewState["SORTDIRECTION"] = "desc";
        else
            ViewState["SORTDIRECTION"] = "asc";

        Load_Current_Schedules();

    }

    protected void gvInspecrionSchedule_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {

            if (e.Row.RowType == DataControlRowType.Header)
            {
                MergeGridviewHeader.SetProperty(objMegeHead);

                e.Row.SetRenderMethodDelegate(MergeGridviewHeader.RenderHeader);
                ViewState["DynamicHeaderCSS"] = "HeaderStyle-css-2";
            }

        }
        catch { }
    }


    protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        Load_Current_Schedules();
    }
    protected void ddlScheduleType_SelectedIndexChanged(object sender, EventArgs e)
    {
        Load_Current_Schedules();
    }

    protected void ddlInspectionType_SelectedIndexChanged(object sender, EventArgs e)
    {
        Load_Current_Schedules();
    }


    private DataTable NewDaemonPortList()
    {
        DataTable dtPort = new DataTable();

        dtPort.Columns.Add("PortID", typeof(int));
        dtPort.PrimaryKey = new DataColumn[] { dtPort.Columns["PortID"] };
        return dtPort;
    }


    protected void BtnOpen_Click(object sender, EventArgs e)
    {
        try
        {
            objInsp.INSP_Update_InspectionForReOpen(UDFLib.ConvertToInteger(ViewState["SchDetailId"]), GetSessionUserID(), txtReason.Text);
            txtReason.Text = "";
            Load_Current_Schedules();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
}