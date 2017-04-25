using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Technical;
using SMS.Business.Infrastructure;
using SMS.Properties;
using System.Configuration;
using System.Text;
using System.Data;
using SMS.Business.PURC;
public partial class Infrastructure_DaemonSettings_DaemonSetting : System.Web.UI.Page
{
    BLL_Infra_UserCredentials objBLLUser = new BLL_Infra_UserCredentials();
    BLL_Tec_Worklist objBLLWL = new BLL_Tec_Worklist();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["SORTDIRECTION"] = null;
            ViewState["SORTBYCOLOUMN"] = null;
            UserAccessValidation();
            LoadCombo();
            Load_Current_Schedules();

            New_Schedule_Settings();

        }

        string js = "initScript();";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "initScript_", js, true);
        if (ViewState["PopupOpen"] != null)
            if (ViewState["PopupOpen"].ToString() == "OPEN")
            {
                //   MPE.Show();
            }

    }
    /// <summary>
    /// Fill values in Combobox and Checkbox list
    /// </summary>
    protected void LoadCombo()
    {
        DataSet ds = objBLLWL.GetAllWorklistType();
        rdbJObType.DataTextField = "Worklist_Type_Display";
        rdbJObType.DataValueField = "Worklist_Type";
        rdbJObType.DataSource = ds.Tables[0]; 
        rdbJObType.DataBind();
        rdbJObType.Items.Insert(0, new ListItem("All", "All"));
    }
    protected void BindSuppliersList()
    {
        chklstSuppliers.ClearSelection();
        chklstSuppliers.DataSource = BLL_PURC_Common.Get_SupplierList(null, null);
        chklstSuppliers.DataTextField = "fullname";
        chklstSuppliers.DataValueField = "SUPPLIER";
        chklstSuppliers.DataBind();
       // chklstSuppliers.Items.Insert(0, new ListItem("- ALL -", "0"));
    }

    protected void GetSelectedSuppliers(int ScheduleID)
    {
        if (chkSuppliers.Checked == true)
        {
            DataTable dt = BLL_Infra_DaemonSettings.GetSelectedSuppliers(ScheduleID);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    for (int i = 1; i < chklstSuppliers.Items.Count; i++)
                    {
                        if (row["SUPPLIER"].ToString() == chklstSuppliers.Items[i].Value)
                        {
                            chklstSuppliers.Items[i].Selected = true;
                            break;
                        }
                    }
                }
            }
        }
    }
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }

    protected void UserAccessValidation()
    {
        UserAccess objUA = new UserAccess();

        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);


        if (objUA.View == 0)
            Response.Redirect("~/default.aspx?msgid=1");
        if (objUA.Add == 0)
        {

        }
        if (objUA.Edit == 0)
        {
            btnSaveSchedule.Enabled = false;
            btnSaveScheduleAndClose.Enabled = false;
        }
        if (objUA.Delete == 0)
        {

        }

        if (objUA.Approve == 0)
        {

        }
        if (objUA.Admin == 0 || objUA.Admin == null)
        {
            ViewState["IsAdmin"] = 0;
            chkSysAlert.Visible = false;
            spnSysAlert.Visible = false;
            rdoRoutineType.Items[0].Enabled = false;
            rdoRoutineType.Items[1].Enabled = false;

        }
        else
        {
            ViewState["IsAdmin"] = 1;
            chkSysAlert.Visible = true;
            spnSysAlert.Visible = true;
            rdoRoutineType.Items[0].Enabled = true;
            rdoRoutineType.Items[1].Enabled = true;
        }

    }

    protected void New_Schedule_Settings()
    {

        ViewState["ScheduleID"] = null;

        ViewState["ScheduleID"] = "0";
        int iDay = Convert.ToInt32(DateTime.Today.DayOfWeek);
        chkWeekDays.Items[iDay].Selected = true;
        txtStartDate.Text = DateTime.Today.ToString("dd/MM/yy hh:mm");

        txtScheduleName.Text = "";
        txtDesc.Text = "";
        rdoFrequency.SelectedValue = "1";
        txtDaily.Text = "1";
        txtWeek.Text = "1";

        if (ViewState["IsAdmin"].ToString() == "1")
        {


            rdoRoutineType.SelectedValue = "1";
            rdoSubRoutineType.SelectedValue = "1";
            pnlSubRoutineType_Workflow.Visible = false;
            pnlSubRoutineType_DBRoutine.Visible = true;
        }
        else
        {

            rdoRoutineType.SelectedValue = "3";
            rdoRoutineType_SelectedIndexChanged(null, null);


        }




        txtMailTo.Text = "";
        txtMailCC.Text = "";
        txtSubject.Text = "";
        txtMailHeader.Text = "";
        txtMailFooter.Text = "";

        rdoDailyFrequency.SelectedValue = "1";
        ddlDailyOccursAt_H.SelectedValue = "12";
        ddlDailyOccursAt_M.SelectedValue = "1";

        txtDailyOccursValue.Text = "10";
        ddlDailyOccursType.SelectedValue = "2";
        chkWeekDays.ClearSelection();

        txtRoutineMethod.Text = "";

        Load_SavedProcedures();

        rdbJObCreatedLast.Checked = true;
        rdbJObType.SelectedIndex = 0;
        //rdbAllJobs.Checked = true;
        txtJCDFrom.Text = "";
        txtJCDTo.Text = "";
        chkJobCompletedIn.Checked = false;
        chkPMS.Checked = false;
        chkShowAllPending.Checked = false;

        ddlJobCL.SelectedIndex = 0;
        ddlJobCompletdIn.SelectedIndex = 0;
        ddlPMS.SelectedIndex = 0;
        ddlPrefFor.SelectedIndex = 0;
        ddlReportBy.SelectedIndex = 0;
        chkSysAlert.Checked = false;

        rdoPOCreationLast.Checked = true;
        ddlPOCreatedLast.SelectedIndex = 0;
        txtPOFrom.Text = "";
        txtPOTo.Text = "";
        chkSuppliers.Checked = true;
        chklstSuppliers.Enabled = true;
        BindSuppliersList();
        //for (int i = 1; i < chklstSuppliers.Items.Count; i++)
        //{
        //    if (chklstSuppliers.Items[i].Selected == true)
        //    {
        //        chklstSuppliers.Items[i].Selected = false;
        //    }
        //}
        ddlPOPrefrences.SelectedIndex = 0;
        //  UpdatePanel_Routine.Update();

    }

    protected void Load_Schedule(int ScheduleID)
    {
        ViewState["ScheduleID"] = ScheduleID;

        int Me = 0;
        DataSet ds = BLL_Infra_DaemonSettings.Get_Schedule_Details(ScheduleID, GetSessionUserID());
        DataTable dtSchedule = ds.Tables[0];
        DataTable dtSettings = ds.Tables[1];

        if (dtSchedule.Rows.Count > 0)
        {
            ViewState["ScheduleID"] = dtSchedule.Rows[0]["ScheduleID"].ToString();
            txtScheduleName.Text = dtSchedule.Rows[0]["Schedule_Name"].ToString();
            txtDesc.Text = dtSchedule.Rows[0]["Schedule_Desc"].ToString();
            txtStartDate.Text = dtSchedule.Rows[0]["Start_Date"].ToString();
            txtEndDate.Text = dtSchedule.Rows[0]["End_Date"].ToString();
            rdoFrequency.SelectedValue = dtSchedule.Rows[0]["FrequencyType"].ToString();
            txtDaily.Text = dtSchedule.Rows[0]["Frequency"].ToString();
            txtWeek.Text = dtSchedule.Rows[0]["Frequency"].ToString();
            rdoFrequency_SelectedIndexChanged(null, null);

            rdoRoutineType.SelectedValue = dtSchedule.Rows[0]["RoutineType"].ToString();

            if (Convert.ToInt32(dtSchedule.Rows[0]["SubRoutineType"]) > 0)
            {
                try
                {
                    rdoSubRoutineType.SelectedValue = dtSchedule.Rows[0]["SubRoutineType"].ToString();
                }
                catch (Exception ex) { rdoSubRoutineType.SelectedIndex = -1; }
            }
            else
                rdoSubRoutineType.SelectedIndex = -1;

            rdoRoutineType_SelectedIndexChanged(null, null);


        }

        if (dtSettings.Rows.Count > 0)
        {
            txtMailTo.Text = GetSettingString(dtSettings, "MailTo", "");
            txtMailCC.Text = GetSettingString(dtSettings, "MailCC", "");
            txtSubject.Text = GetSettingString(dtSettings, "Subject", "");
            txtMailHeader.Text = GetSettingString(dtSettings, "MailHeader", "");
            txtMailFooter.Text = GetSettingString(dtSettings, "MailFooter", "");
            txtRoutineMethod.Text = GetSettingString(dtSettings, "SYSTEMPROCEDURE", "").ToString();

            if (GetSettingValue(dtSettings, "SYSALERT", 0) == 1)
            {
                chkSysAlert.Checked = true;
            }
            else
            {
                chkSysAlert.Checked = false;
            }
            ddlSavedQuery.SelectedValue = GetSettingString(dtSettings, "DataProcedure", "0");
            if (rdoFrequency.SelectedValue == "1")
            {
                int DAILY_OCCURANCE_TYPE = GetSettingValue(dtSettings, "DAILY OCCURANCE TYPE", 1);
                if (DAILY_OCCURANCE_TYPE == 1)
                {
                    rdoDailyFrequency.SelectedValue = "1";
                    ddlDailyOccursAt_H.SelectedValue = GetSettingValue(dtSettings, "DAILY ONCE HOUR", 0).ToString();
                    ddlDailyOccursAt_M.SelectedValue = GetSettingValue(dtSettings, "DAILY ONCE MINUTE", 0).ToString();

                }
                if (DAILY_OCCURANCE_TYPE == 2)
                {
                    rdoDailyFrequency.SelectedValue = "2";
                    txtDailyOccursValue.Text = GetSettingValue(dtSettings, "DAILY REPEAT VALUE", 0).ToString();
                    ddlDailyOccursType.SelectedValue = GetSettingValue(dtSettings, "DAILY REPEAT TYPE", 0).ToString();
                }


            }
            if (rdoFrequency.SelectedValue == "2")
            {
                DataTable dtWeekdays = GetSettingTable(dtSettings, "WEEKDAYS");

                chkWeekDays.ClearSelection();
                foreach (DataRow dr in dtWeekdays.Rows)
                {
                    chkWeekDays.Items[UDFLib.ConvertToInteger(dr["key_value_int"]) - 1].Selected = true;
                }
            }
            if (dtSchedule.Rows[0]["RoutineType"].ToString() == "3")
            {
                if (GetSettingValue(dtSettings, "JOBCREATEDINDAYS", 0) == 0)
                {
                    txtJCDFrom.Text = GetSettingDateValue(dtSettings, "JOBCREATEDFROM", "");
                    txtJCDTo.Text = GetSettingDateValue(dtSettings, "JOBCREATEDTO", "");
                    rdbJobCreatedBetween.Checked = true;
                }
                else
                {
                    rdbJObCreatedLast.Checked = true;
                    ddlJobCL.SelectedValue = GetSettingValue(dtSettings, "JOBCREATEDINDAYS", 0).ToString();
                }







                chkShowAllPending.Checked = Convert.ToBoolean(GetSettingValue(dtSettings, "SHOWALLPENDINGJOBS", 0));
                chkJobCompletedIn.Checked = Convert.ToBoolean(GetSettingValue(dtSettings, "SHOWJOBCOMPLETED", 0));
                ddlJobCompletdIn.SelectedValue = GetSettingValue(dtSettings, "SHOWJOBCOMPLETEDINDAYS", 0).ToString();
                string ncrcheck = GetSettingString(dtSettings, "JOBTYPE", ""); ;
                try
                {
                    rdbJObType.SelectedValue = ncrcheck;
                }
                catch (Exception)
                {

                    rdbJObType.SelectedIndex = 0;
                }
                
                
                chkPMS.Checked = Convert.ToBoolean(GetSettingValue(dtSettings, "PMSJOB", 0));



                ddlPMS.SelectedValue = GetSettingString(dtSettings, "PMSJOBINDAYS", "");
                ddlReportBy.SelectedValue = GetSettingString(dtSettings, "SENDBY", "");
                // ddlJobCL.SelectedValue = GetSettingValue(dtSettings, "JOBCREATEDINDAYS", 0).ToString();
                Me = GetSettingValue(dtSettings, "SAVEPREFFORME", 0);


                if (Me != 0)
                {
                    ddlPrefFor.SelectedValue = "ME";
                }
                else
                {
                    ddlPrefFor.SelectedValue = "MYDEPARTMENT";
                }

            }
            if (dtSchedule.Rows[0]["RoutineType"].ToString() == "4")
            {
                if (GetSettingValue(dtSettings, "POCREATEDINDAYS", 0) == 0)
                {
                    txtPOFrom.Text = GetSettingDateValue(dtSettings, "POCREATEDFROM", "");
                    txtPOTo.Text = GetSettingDateValue(dtSettings, "POCREATEDTO", "");
                    rdoPOCreationBetween.Checked = true;
                    rdoPOCreationLast.Checked = false;
                }
                else
                {
                    rdoPOCreationBetween.Checked = false;
                    rdoPOCreationLast.Checked = true;
                    ddlPOCreatedLast.SelectedValue = GetSettingValue(dtSettings, "POCREATEDINDAYS", 0).ToString();
                }

                chkSuppliers.Checked = Convert.ToBoolean(GetSettingValue(dtSettings, "SELECTEDSUPPLIER", 0));
                BindSuppliersList();
                if (Convert.ToBoolean(GetSettingValue(dtSettings, "SELECTEDSUPPLIER", 0)) == true)
                {
                    GetSelectedSuppliers(ScheduleID);
                    chklstSuppliers.Enabled = true;
                }
                else
                {
                    chklstSuppliers.Enabled = false;
                }
               
                Me = GetSettingValue(dtSettings, "SAVEPREFFORME", 0);
                

                if (Me != 0)
                {
                    ddlPOPrefrences.SelectedValue = "ME";
                }
                else
                {
                    ddlPOPrefrences.SelectedValue = "MYDEPARTMENT";
                }




            }

        }


        if (Session["USERID"].ToString() != dtSchedule.Rows[0]["Created_By"].ToString())
        {

            ddlPrefFor.Enabled = false;
        }

        if (Convert.ToInt32(ViewState["IsAdmin"]) == 0)
        {
            if (ddlPrefFor.SelectedValue != "MYDEPARTMENT")
            {
                if (Session["USERID"].ToString() != dtSchedule.Rows[0]["Created_By"].ToString())
                {
                    btnSaveSchedule.Enabled = false;
                    btnSaveScheduleAndClose.Enabled = false;
                }

            }
        }
        string js = "showModal('dvWizardDialog');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "hideModalJS", js, true);

    }

    protected void Toogle_Schedule(int ScheduleID)
    {
        int Res = BLL_Infra_DaemonSettings.Toggle_Schedule(ScheduleID, GetSessionUserID());
        if (Res > 0)
        {
            Load_Current_Schedules();
        }
    }

    protected void Pause_Schedule(int ScheduleID)
    {
        int Res = BLL_Infra_DaemonSettings.Pause_Schedule(ScheduleID, GetSessionUserID());
        if (Res > 0)
        {
            Load_Current_Schedules();
        }
    }

    protected void Run_Schedule(int ScheduleID)
    {
        int Res = BLL_Infra_DaemonSettings.Run_Schedule(ScheduleID, GetSessionUserID());
        if (Res > 0)
        {
            Load_Current_Schedules();
        }

    }
    
    protected void Execure_Schedule_Now(int ScheduleID)
    {
        //Daemon Grid load everytime click on RUN_NOW button
        BLL_Infra_DaemonSettings.Execure_Schedule_Now(ScheduleID, GetSessionUserID());
        Load_Current_Schedules();
    }
    protected void Load_Current_Schedules()
    {
        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        string sortdirection = (ViewState["SORTDIRECTION"] == null) ? "" : (ViewState["SORTDIRECTION"].ToString() == "1") ? " ASC" : " DESC";

        string Sort_Expression = sortbycoloumn + sortbycoloumn;

        int Frequency_Type = UDFLib.ConvertToInteger(ddlFrequencyType.SelectedValue);
        int Last_Run_Success = -1;
        int Status = -1;
        string SearchText = txtSearchText.Text;
        int ShowDept = 0;
        if (chkOnlyDept.Checked)
        {
            ShowDept = 1;
        }
        //int DeptId = Convert.ToInt32(Session["USERDEPARTMENTID"]);
        int DeptId = UDFLib.ConvertToInteger(Session["USERDEPARTMENTID"]);


        int is_Fetch_Count = ucCustomPagerAllStatus.isCountRecord;
        DataSet ds = BLL_Infra_DaemonSettings.Get_Current_Schedules(Frequency_Type, Last_Run_Success, Status, SearchText, GetSessionUserID(), ShowDept, DeptId, Convert.ToInt32(ViewState["IsAdmin"]), ucCustomPagerAllStatus.CurrentPageIndex, ucCustomPagerAllStatus.PageSize, ref is_Fetch_Count);


        grdSchedules.DataSource = ds.Tables[0];
        grdSchedules.DataBind();

        ucCustomPagerAllStatus.CountTotalRec = is_Fetch_Count.ToString();
        ucCustomPagerAllStatus.BuildPager();

    }

    protected void btnCreateNewSchedule_Click(object sender, EventArgs e)
    {
        New_Schedule_Settings();

        string js = "showModal('dvWizardDialog');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "dvWizardDialog_", js, true);

    }

    protected void ImgBtnRefresh_Click(object sender, EventArgs e)
    {
        Load_Current_Schedules();
    }

    protected void btnRunProcess_Click(object sender, EventArgs e)
    {
        BLL_Infra_DaemonSettings.Execute_Daemon_Process();
        Load_Current_Schedules();
    }

    protected void grdSchedules_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.ToUpper() == "EDIT_SCHEDULE")
        {
            int ScheduleID = UDFLib.ConvertToInteger(e.CommandArgument);
            Load_Schedule(ScheduleID);
        }
        if (e.CommandName.ToUpper() == "PAUSE_SCHEDULE")
        {
            int ScheduleID = UDFLib.ConvertToInteger(e.CommandArgument);
            Pause_Schedule(ScheduleID);
        }
        if (e.CommandName.ToUpper() == "RUN_SCHEDULE")
        {
            int ScheduleID = UDFLib.ConvertToInteger(e.CommandArgument);
            Run_Schedule(ScheduleID);
        }
        if (e.CommandName.ToUpper() == "RUN_NOW")
        {
            int ScheduleID = UDFLib.ConvertToInteger(e.CommandArgument);
            Execure_Schedule_Now(ScheduleID);
        }
        if (e.CommandName.ToUpper() == "TOGGLE_SCHDULE")
        {
            int ScheduleID = UDFLib.ConvertToInteger(e.CommandArgument);
            Toogle_Schedule(ScheduleID);
        }

    }

    protected void grdSchedules_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string Last_Run_Result = DataBinder.Eval(e.Row.DataItem, "Last_Run_Result").ToString();
            int Last_Run_Success = UDFLib.ConvertToInteger(DataBinder.Eval(e.Row.DataItem, "Last_Run_Success").ToString());

            if (Last_Run_Result != "")
            {
                Last_Run_Result = Last_Run_Result.Replace("\n", "<br>");
                Image imgLast_Run_Result = (Image)e.Row.FindControl("imgLast_Run_Result");
                if (imgLast_Run_Result != null)
                {
                    imgLast_Run_Result.ImageUrl = (Last_Run_Success == 1) ? "~/images/wizard/smile-icon.png" : "~/images/wizard/sad-icon.png";
                    imgLast_Run_Result.Attributes.Add("Title", "cssbody=[dvbdy1] cssheader=[dvhdr1] header=[Last Run Result] body=[" + Last_Run_Result + "]");
                    imgLast_Run_Result.Visible = true;
                }
            }

            string PREFDEPT = DataBinder.Eval(e.Row.DataItem, "PREFDEPT").ToString();
            string PREFUSER = DataBinder.Eval(e.Row.DataItem, "PREFUSER").ToString();
            string SYSALERT = DataBinder.Eval(e.Row.DataItem, "SYSALERT").ToString();
            if (SYSALERT.Trim().Length > 0)
            {
                if (SYSALERT != "0")
                {
                    Label lblDept = (Label)e.Row.FindControl("lblDept");
                    lblDept.Attributes.Add("style", "font-size: 8px; vertical-align: middle;  text-align: center; width: 50px; line-height: 10px;background: #0066FF; color: white; padding:2");
                    lblDept.Text = "SYS";
                }

            }
            if (PREFDEPT.Trim().Length > 0)
            {
                Label lblDept = (Label)e.Row.FindControl("lblDept");
                lblDept.Attributes.Add("style", "font-size: 8px; vertical-align: middle;  text-align: center; width: 50px; line-height: 10px;background: #34AD00; color: white; padding:2");
                lblDept.Text = "DEPT";
                if (SYSALERT.Trim().Length > 0)
                {
                    if (SYSALERT != "0")
                    {

                        lblDept.Attributes.Add("style", "font-size: 8px; vertical-align: middle;  text-align: center; width: 50px; line-height: 10px;background: #0066FF; color: white; padding:2");
                        lblDept.Text = "DEPT-SYS";
                    }

                }
            }
            if (PREFUSER.Trim().Length > 0)
            {
                Label lblDept = (Label)e.Row.FindControl("lblDept");
                lblDept.Attributes.Add("style", "font-size: 8px; vertical-align: middle;  text-align: center; width: 40px; line-height: 10px;background: #FF0066; color: white;padding:2");
                lblDept.Text = "ME";

                if (SYSALERT.Trim().Length > 0)
                {
                    if (SYSALERT != "0")
                    {
                        lblDept.Attributes.Add("style", "font-size: 8px; vertical-align: middle;  text-align: center; width: 50px; line-height: 10px;background: #0066FF; color: white; padding:2");
                        lblDept.Text = "ME-SYS";
                    }


                }
            }



        }
    }

    protected void grdSchedules_Sorting(object sender, GridViewSortEventArgs e)
    {
        ViewState["SORTBYCOLOUMN"] = e.SortExpression;

        if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
            ViewState["SORTDIRECTION"] = 1;
        else
            ViewState["SORTDIRECTION"] = 0;

        Load_Current_Schedules();
    }

    protected void rdoFrequency_SelectedIndexChanged(object sender, EventArgs e)
    {
        pnlDaily.Visible = false;
        pnlWeekly.Visible = false;
        pnlStartMonth.Visible = false;
        pnlEndMonth.Visible = false;

        switch (rdoFrequency.SelectedValue)
        {
            case "1":
                pnlDaily.Visible = true;
                break;
            case "2":
                pnlWeekly.Visible = true;
                break;
            case "3":
                pnlStartMonth.Visible = true;
                break;
            case "4":
                pnlEndMonth.Visible = true;
                break;
        }
    }

    protected void rdoRoutineType_SelectedIndexChanged(object sender, EventArgs e)
    {
        pnlSubRoutineType_DBRoutine.Visible = false;
        pnlSubRoutineType_SystemRoutine.Visible = false;
        pnlSubRoutineType_Workflow.Visible = false;
        pnlSubRoutineType_PO.Visible = false;
        pnlSavedQuery.Visible = true;
        lblDatabaseProcedure.Visible = true;

        switch (rdoRoutineType.SelectedValue)
        {
            case "1":
                pnlSubRoutineType_DBRoutine.Visible = true;
                break;
            case "2":
                rdoSubRoutineType.SelectedIndex = 0;
                pnlSubRoutineType_SystemRoutine.Visible = true;
                pnlSavedQuery.Visible = false;
                lblDatabaseProcedure.Visible = false;
                break;
            case "3":
                pnlSavedQuery.Visible = false;
                lblDatabaseProcedure.Visible = false;
                ImageButton1.Visible = false;

                pnlSubRoutineType_Workflow.Visible = true;

                break;
            case "4":
                pnlSavedQuery.Visible = false;
                lblDatabaseProcedure.Visible = false;
                ImageButton1.Visible = false;
                pnlSubRoutineType_PO.Visible = true;
                break;
        }
        UpdatePanel2.Update();
    }

    protected void rdoSubRoutineType_SelectedIndexChanged(object sender, EventArgs e)
    {
        pnlTask_SendMail.Visible = false;
        pnlTask_DashboardAlert.Visible = false;
        pnlTask_SMSAlert.Visible = false;


        switch (rdoSubRoutineType.SelectedValue)
        {
            case "1":
                pnlTask_SendMail.Visible = true;
                break;
            case "2":
                pnlTask_DashboardAlert.Visible = true;
                break;
            case "3":
                pnlTask_SMSAlert.Visible = true;
                break;
        }
    }

    protected void Load_SavedProcedures()
    {
        DataTable dt = BLL_Infra_DaemonSettings.Get_DatabaseProcedures();

        ddlSavedQuery.DataSource = dt;
        ddlSavedQuery.DataTextField = "ObjectName";
        ddlSavedQuery.DataValueField = "ObjectName";
        ddlSavedQuery.DataBind();
        ddlSavedQuery.Items.Insert(0, new ListItem("Select Procedure", "0"));
    }



    protected void btnReloadSavedProcedures_Click(object sender, EventArgs e)
    {
        Load_SavedProcedures();
    }

    private DataTable NewDaemonSettingTable()
    {
        DataTable dtSettings = new DataTable();
        dtSettings.Columns.Add("ScheduleID", typeof(int));
        dtSettings.Columns.Add("Key_Name", typeof(string));
        dtSettings.Columns.Add("Key_Value_String", typeof(string));
        dtSettings.Columns.Add("Key_Value_Date", typeof(DateTime));
        dtSettings.Columns.Add("Key_Value_Int", typeof(int));
        return dtSettings;
    }

    private DataTable NewDaemonScheduleTable()
    {
        DataTable dtSchedule = new DataTable();

        dtSchedule.Columns.Add("ScheduleID", typeof(int));
        dtSchedule.Columns.Add("Schedule_Name", typeof(String));
        dtSchedule.Columns.Add("Schedule_Desc", typeof(String));
        dtSchedule.Columns.Add("Start_Date", typeof(DateTime));
        dtSchedule.Columns.Add("End_Date", typeof(DateTime));
        dtSchedule.Columns.Add("FrequencyType", typeof(int));
        dtSchedule.Columns.Add("Frequency", typeof(int));
        dtSchedule.Columns.Add("RoutineType", typeof(int));
        dtSchedule.Columns.Add("SubRoutineType", typeof(int));
        dtSchedule.Columns.Add("Last_Run_Start");
        dtSchedule.Columns.Add("Last_Run_End");
        dtSchedule.Columns.Add("Last_Run_Result");
        dtSchedule.Columns.Add("Last_Run_Success");
        dtSchedule.Columns.Add("Created_By");
        dtSchedule.Columns.Add("Date_Of_Creation");
        dtSchedule.Columns.Add("Modified_By");
        dtSchedule.Columns.Add("Date_Of_Modification");
        dtSchedule.Columns.Add("Deleted_By");
        dtSchedule.Columns.Add("Date_Of_Deletion");

        return dtSchedule;
    }

    protected void btnSaveSchedule_Click(object sender, EventArgs e)
    {
        if (Save_Schedule() == true)
        {
            string js1 = "alert('Schedular Details Saved.');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "script1", js1, true);
            Load_Current_Schedules();
        }
    }

    protected void btnSaveScheduleAndClose_Click(object sender, EventArgs e)
    {
        if (Save_Schedule() == true)
        {
            string js1 = "alert('Schedular Details Saved.');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "script1", js1, true);
            Load_Current_Schedules();

            string js = "hideModal('dvWizardDialog');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "hideModalJS", js, true);
        }
    }

    protected bool ValidateAll()
    {
        string js = "";

        if (txtScheduleName.Text == "")
        {
            js = "alert('Please provide schedular name.');";
        }







        switch (rdoRoutineType.SelectedValue)
        {
            case "1":
                // Database Routine
                switch (rdoSubRoutineType.SelectedValue)
                {
                    case "1":
                        //Send Mail-Tabular Data
                        if (txtMailTo.Text == "")
                            js = "alert('Please enter Mail To and Subject.');";
                        else if (ddlSavedQuery.SelectedValue == "0")
                            js = "alert('Please select Database Procedure.');";

                        break;
                    case "2":
                        //Dashboard Alert

                        break;
                    case "3":
                        //SMS Alert

                        break;
                }
                break;
        }

        if (js == "")
            return true;
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ValidateAllJS", js, true);
            return false;
        }

    }

    protected bool Save_Schedule()
    {
        try
        {
            if (ValidateAll() == true)
            {

                DataTable dtSchedule = NewDaemonScheduleTable();
                DataTable dtSettings = NewDaemonSettingTable();

                Populate_Schedular_Table(dtSchedule);
                Populate_Settings_Table(dtSettings);

                int Res = BLL_Infra_DaemonSettings.Save_Schedule(dtSchedule, dtSettings, GetSessionUserID());
                if (Res == -1)
                {
                    string js = "alert('Schedule name already exists.');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script1", js, true);
                    return false;
                }
                else if (Res > 0)
                {
                    ViewState["ScheduleID"] = Res.ToString();
                    

                    return true;
                }
                else
                    return false;

            }
            else
                return false;
        }
        catch (Exception ex)
        {
            string js = "alert('" + ex.Message.Replace("'", "") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "script1", js, true);
            return false;
        }
    }

    protected void Populate_Schedular_Table(DataTable dtSchedule)
    {
        int ScheduleID = UDFLib.ConvertToInteger(ViewState["ScheduleID"]);

        dtSchedule.Rows.Add(
            ScheduleID,
            txtScheduleName.Text,
            txtDesc.Text,
            txtStartDate.Text,
            UDFLib.ConvertDateToNull(txtEndDate.Text),
            rdoFrequency.SelectedValue,
            GetFrequencyValue(),
            rdoRoutineType.SelectedValue,
            GetSubRoutineType());


    }

    protected void Populate_Settings_Table(DataTable dtSettings)
    {
        int ScheduleID = UDFLib.ConvertToInteger(ViewState["ScheduleID"]);
        if (chkSysAlert.Visible)
        {
            if (chkSysAlert.Checked)
            {
                dtSettings.Rows.Add(ScheduleID, "SYSALERT", null, null, 1);
            }
            else
            {
                dtSettings.Rows.Add(ScheduleID, "SYSALERT", null, null, 0);
            }

        }
        else
        {
            dtSettings.Rows.Add(ScheduleID, "SYSALERT", null, null, 0);
        }
        switch (rdoFrequency.SelectedValue)
        {

            case "1":
                // DAILY
                dtSettings.Rows.Add(ScheduleID, "DAILY OCCURANCE TYPE", "Occurs once OR repetedly", null, rdoDailyFrequency.SelectedValue);

                if (rdoDailyFrequency.SelectedValue == "1")
                {
                    dtSettings.Rows.Add(ScheduleID, "DAILY ONCE HOUR", "Occurs once at this hour", null, ddlDailyOccursAt_H.SelectedValue);
                    dtSettings.Rows.Add(ScheduleID, "DAILY ONCE MINUTE", "Occurs once at this minute", null, ddlDailyOccursAt_M.SelectedValue);
                }
                else
                {
                    dtSettings.Rows.Add(ScheduleID, "DAILY REPEAT VALUE", "Occurs Every", null, UDFLib.ConvertToInteger(txtDailyOccursValue.Text));
                    dtSettings.Rows.Add(ScheduleID, "DAILY REPEAT TYPE", "Occurs Every H/M/S", null, ddlDailyOccursType.SelectedValue);
                }
                break;

            case "2":
                // Weekly
                foreach (ListItem li in chkWeekDays.Items)
                {
                    if (li.Selected == true)
                    {
                        dtSettings.Rows.Add(ScheduleID, "WEEKDAYS", li.Text, null, li.Value);
                    }
                }
                break;
        }


        switch (rdoRoutineType.SelectedValue)
        {
            case "1":
                // Database Routine

                switch (rdoSubRoutineType.SelectedValue)
                {
                    case "1":
                        //Send Mail Tabular Data
                        dtSettings.Rows.Add(ScheduleID, "MAILTO", txtMailTo.Text);
                        dtSettings.Rows.Add(ScheduleID, "MAILCC", txtMailCC.Text);
                        dtSettings.Rows.Add(ScheduleID, "SUBJECT", txtSubject.Text);
                        dtSettings.Rows.Add(ScheduleID, "MAILHEADER", txtMailHeader.Text);
                        dtSettings.Rows.Add(ScheduleID, "MAILFOOTER", txtMailFooter.Text);
                        dtSettings.Rows.Add(ScheduleID, "DATAPROCEDURE", ddlSavedQuery.SelectedValue);
                        break;
                }

                break;
            case "2":
                // System Routine
                switch (rdoSubRoutineType.SelectedValue)
                {
                    case "1":
                        //Send Mail Tabular Data
                        dtSettings.Rows.Add(ScheduleID, "MAILTO", txtMailTo.Text);
                        dtSettings.Rows.Add(ScheduleID, "MAILCC", txtMailCC.Text);
                        dtSettings.Rows.Add(ScheduleID, "SUBJECT", txtSubject.Text);
                        dtSettings.Rows.Add(ScheduleID, "MAILHEADER", txtMailHeader.Text);
                        dtSettings.Rows.Add(ScheduleID, "MAILFOOTER", txtMailFooter.Text);
                        dtSettings.Rows.Add(ScheduleID, "SYSTEMPROCEDURE", txtRoutineMethod.Text);
                        break;
                }


                break;
            case "3":
                DateTime? JDCF = null;
                DateTime? JDCFTo = null;
                if (txtJCDFrom.Text.Trim().Length != 0)
                    JDCF = UDFLib.ConvertDateToNull(txtJCDFrom.Text);
                if (txtJCDTo.Text.Trim().Length != 0)
                    JDCFTo = UDFLib.ConvertDateToNull(txtJCDTo.Text);




                if (rdbJobCreatedBetween.Checked)
                {




                    dtSettings.Rows.Add(ScheduleID, "JOBCREATEDINDAYS", null, null, null);
                    dtSettings.Rows.Add(ScheduleID, "JOBCREATEDFROM", null, JDCF, null);
                    dtSettings.Rows.Add(ScheduleID, "JOBCREATEDTO", null, JDCFTo, null);
                }
                else
                {
                    dtSettings.Rows.Add(ScheduleID, "JOBCREATEDINDAYS", null, null, ddlJobCL.SelectedValue);
                    dtSettings.Rows.Add(ScheduleID, "JOBCREATEDFROM", null, JDCF, null);
                    dtSettings.Rows.Add(ScheduleID, "JOBCREATEDTO", null, JDCFTo, null);
                }



                dtSettings.Rows.Add(ScheduleID, "SHOWALLPENDINGJOBS", null, null, chkShowAllPending.Checked ? 1 : 0);
                dtSettings.Rows.Add(ScheduleID, "SHOWJOBCOMPLETED", null, null, chkJobCompletedIn.Checked ? 1 : 0);
                dtSettings.Rows.Add(ScheduleID, "SHOWJOBCOMPLETEDINDAYS", null, null, ddlJobCompletdIn.SelectedValue);
                string JOBTYPE = rdbJObType.SelectedValue.ToString();
                

                dtSettings.Rows.Add(ScheduleID, "JOBTYPE", JOBTYPE, null, null);
                dtSettings.Rows.Add(ScheduleID, "PMSJOB", null, null, chkPMS.Checked ? 1 : 0);
                dtSettings.Rows.Add(ScheduleID, "PMSJOBINDAYS", ddlPMS.SelectedValue, null, null);
                dtSettings.Rows.Add(ScheduleID, "SENDBY", ddlReportBy.SelectedValue, null, null);




                if (ddlPrefFor.SelectedValue == "ME")
                {
                    dtSettings.Rows.Add(ScheduleID, "SAVEPREFFORME", null, null, Convert.ToInt32(Session["USERID"]));
                    dtSettings.Rows.Add(ScheduleID, "SAVEPREFFORDEPT", null, null, null);
                }
                else
                {
                    dtSettings.Rows.Add(ScheduleID, "SAVEPREFFORDEPT", null, null, Convert.ToInt32(Session["USERDEPARTMENTID"]));
                    dtSettings.Rows.Add(ScheduleID, "SAVEPREFFORME", null, null, null);
                }







                dtSettings.Rows.Add(ScheduleID, "MAILTO", txtMailTo.Text);
                dtSettings.Rows.Add(ScheduleID, "MAILCC", txtMailCC.Text);
                dtSettings.Rows.Add(ScheduleID, "SUBJECT", txtSubject.Text);
                dtSettings.Rows.Add(ScheduleID, "MAILHEADER", txtMailHeader.Text);
                dtSettings.Rows.Add(ScheduleID, "MAILFOOTER", txtMailFooter.Text);

                break;
            case "4":
                DateTime? POFD = null;
                DateTime? POTD = null;
                if (txtPOFrom.Text.Trim().Length != 0)
                    POFD = UDFLib.ConvertDateToNull(txtPOFrom.Text);
                if (txtPOTo.Text.Trim().Length != 0)
                    POTD = UDFLib.ConvertDateToNull(txtPOTo.Text);

                if (rdoPOCreationBetween.Checked)
                {
                    dtSettings.Rows.Add(ScheduleID, "POCREATEDINDAYS", null, null, null);
                    dtSettings.Rows.Add(ScheduleID, "POCREATEDFROM", null, POFD, null);
                    dtSettings.Rows.Add(ScheduleID, "POCREATEDTO", null, POTD, null);
                }
                else
                {
                    dtSettings.Rows.Add(ScheduleID, "POCREATEDINDAYS", null, null, ddlPOCreatedLast.SelectedValue);
                    dtSettings.Rows.Add(ScheduleID, "POCREATEDFROM", null, POFD, null);
                    dtSettings.Rows.Add(ScheduleID, "POCREATEDTO", null, POTD, null);
                }


                dtSettings.Rows.Add(ScheduleID, "SELECTEDSUPPLIER", null, null, chkSuppliers.Checked ? 1 : 0);
                // add supplier to table
                DataTable dtSupplier = new DataTable();
                dtSupplier.Columns.Add("SUPPLIER", typeof(String));
                if (chkSuppliers.Checked == true)
                {
                    for (int i = 1; i < chklstSuppliers.Items.Count; i++)
                    {
                        if (chklstSuppliers.Items[i].Selected == true)
                        {
                            dtSupplier.Rows.Add(chklstSuppliers.Items[i].Value);
                        }
                    }
                }
                BLL_Infra_DaemonSettings.UpdateSupplierList(dtSupplier, ScheduleID , GetSessionUserID());

                if (ddlPOPrefrences.SelectedValue == "ME")
                {
                    dtSettings.Rows.Add(ScheduleID, "SAVEPREFFORME", null, null, Convert.ToInt32(Session["USERID"]));
                    dtSettings.Rows.Add(ScheduleID, "SAVEPREFFORDEPT", null, null, null);
                }
                else
                {
                    dtSettings.Rows.Add(ScheduleID, "SAVEPREFFORDEPT", null, null, Convert.ToInt32(Session["USERDEPARTMENTID"]));
                    dtSettings.Rows.Add(ScheduleID, "SAVEPREFFORME", null, null, null);
                }

                dtSettings.Rows.Add(ScheduleID, "MAILTO", txtMailTo.Text);
                dtSettings.Rows.Add(ScheduleID, "MAILCC", txtMailCC.Text);
                dtSettings.Rows.Add(ScheduleID, "SUBJECT", txtSubject.Text);
                dtSettings.Rows.Add(ScheduleID, "MAILHEADER", txtMailHeader.Text);
                dtSettings.Rows.Add(ScheduleID, "MAILFOOTER", txtMailFooter.Text);

                break;

        }
    }

    private int GetFrequencyValue()
    {
        int RetValue = 0;
        switch (rdoFrequency.SelectedValue)
        {
            case "1":
                // Daily
                RetValue = UDFLib.ConvertToInteger(txtDaily.Text);
                break;
            case "2":
                // Weekly
                RetValue = UDFLib.ConvertToInteger(txtWeek.Text);
                break;
            case "3":
                // Start of each month
                RetValue = 1;
                break;
            case "4":
                // End of each month
                RetValue = 1;
                break;
        }
        return RetValue;
    }

    private int GetSubRoutineType()
    {
        int RetValue = 0;
        switch (rdoRoutineType.SelectedValue)
        {
            case "1":
                // DB Routine
                RetValue = UDFLib.ConvertToInteger(rdoSubRoutineType.SelectedValue);
                break;
            case "2":
                // System Routine
                RetValue = 1;
                break;
            case "3":
                // Workflow Based
                RetValue = 0;
                break;
            case "4":
                // PO Based
                RetValue = 4;
                break;
        }
        return RetValue;
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
                    Value = Convert.ToDateTime(drsSet[0]["Key_Value_Date"]).ToString("dd/MM/yyyy");
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

    public DataTable GetSettingTable(DataTable dtSettings, string Key)
    {

        string expression = "Key_Name='" + Key + "'";
        string sortOrder = "Key_Name";

        DataView dv = new DataView(dtSettings, expression, sortOrder, DataViewRowState.CurrentRows);
        return dv.ToTable("Settings");
    }

    protected void txtSearchText_TextChanged(object sender, EventArgs e)
    {
        Load_Current_Schedules();
        string js = "$('#dvSchedules span').highlight([\"" + txtSearchText.Text + "\"]);";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "highlighttext", js, true);
    }
    protected void btnAddTo_Click(object sender, EventArgs e)
    {
        BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
        txtSelectedIDsTo.Text = "";
        string To = "";

        foreach (ListItem li in lstUsers.Items)
        {
            if (li.Selected)
            {
                DataTable dt = objUser.Get_UserDetails(int.Parse(li.Value));
                if (dt.Rows.Count > 0)
                {
                    if (To.Length > 0 && dt.Rows[0]["MailID"].ToString() != "")
                        To += ";";
                    To += dt.Rows[0]["MailID"].ToString();
                }
            }
        }
        txtMailTo.Text += ";" + To;
        //if (IsPostBack)
        //{
        //    ucEmailAttachment1.Register_JS_Attach();
        //}
        UpdatePanel2.Update();
        objUser = null;
    }
    protected void btnCC_Click(object sender, EventArgs e)
    {
        BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
        txtSelectedIDsCC.Text = "";
        string CC = "";

        foreach (ListItem li in lstUsers.Items)
        {
            if (li.Selected)
            {
                DataTable dt = objUser.Get_UserDetails(int.Parse(li.Value));
                if (dt.Rows.Count > 0)
                {
                    if (CC.Length > 0 && dt.Rows[0]["MailID"].ToString() != "")
                        CC += ";";
                    CC += dt.Rows[0]["MailID"].ToString();
                }
            }
        }
        txtMailCC.Text += ";" + CC;
        objUser = null;
        //if (IsPostBack)
        //{
        //    ucEmailAttachment1.Register_JS_Attach();
        //}
        UpdatePanel2.Update();
    }
    protected void btnOk1_Click(object sender, EventArgs e)
    {
        // MPE.Hide();
        ViewState["PopupOpen"] = "";
    }


    protected void btnOpenReport_Click(object sender, EventArgs e)
    {
        Save_Schedule();
        Load_Current_Schedules();
        if (ViewState["ScheduleID"] == null)
        {
            string js = "ald()";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ValidateAllJS", js, true);
            UpdatePanel_Routine.UpdateMode = UpdatePanelUpdateMode.Conditional;
            UpdatePanel_Routine.Update();
            return;
        }




        DataTable dt = BLL_Infra_DaemonSettings.Generate_Report(Convert.ToInt32(ViewState["ScheduleID"]));
        string htmlString = dt.Rows[0].ItemArray[0].ToString();

        string filepath = Server.MapPath("~/Uploads/Temp/");

        string filename = "WorkListReport" + DateTime.Now.Day + DateTime.Now.Month + DateTime.Now.Second + DateTime.Now.Millisecond + ".pdf";

        string filenamewithpath = filepath + filename;


        EO.Pdf.HtmlToPdf.ConvertHtml(htmlString, filenamewithpath);

        ResponseHelper.Redirect("~/Uploads/Temp/" + filename, "blank", "");

    }


    protected void chkOnlyDept_CheckedChanged(object sender, EventArgs e)
    {
        Load_Current_Schedules();
    }

    protected void btnPOReport_Click(object sender, EventArgs e)
    {
        Save_Schedule();
        Load_Current_Schedules();
        if (ViewState["ScheduleID"] == null)
        {
            string js = "ald()";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ValidateAllJS", js, true);
            UpdatePanel_Routine.UpdateMode = UpdatePanelUpdateMode.Conditional;
            UpdatePanel_Routine.Update();
            return;
        }

        DataTable dt = BLL_Infra_DaemonSettings.Generate_PO_Report(Convert.ToInt32(ViewState["ScheduleID"]));
        string htmlString = dt.Rows[0].ItemArray[0].ToString();

        System.IO.StreamWriter excelDoc;
        excelDoc = new System.IO.StreamWriter(System.IO.Path.Combine(Server.MapPath("~") + @"\" + "Uploads/Purchase/", "POReport_" + DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year + "_" + DateTime.Now.Hour + "h" + DateTime.Now.Minute + "m" + DateTime.Now.Second + "s" + ".xls"));
        excelDoc.Write(htmlString);
        excelDoc.Close();
        excelDoc.Dispose();
        ResponseHelper.Redirect("~/Uploads/Purchase/" + "POReport_" + DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year + "_" + DateTime.Now.Hour + "h" + DateTime.Now.Minute + "m" + DateTime.Now.Second + "s" + ".xls", "blank", "");
    }
    protected void chkSuppliers_CheckedChanged(object sender, EventArgs e)
    {
        if (chkSuppliers.Checked == true)
            chklstSuppliers.Enabled = true;
        else
            chklstSuppliers.Enabled = false;
    }
}
