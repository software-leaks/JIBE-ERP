using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.Infrastructure;
using SMS.Properties;
using SMS.Business.Inspection;
public partial class Technical_Worklist_ScheduleInspection : System.Web.UI.Page
{

    UserAccess objUA = new UserAccess();
    BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
    BLL_Infra_VesselLib objVsl = new BLL_Infra_VesselLib();
    BLL_INSP_Checklist objBllChecklist = new BLL_INSP_Checklist();
    BLL_Infra_Port objInfra = new BLL_Infra_Port();
    BLL_Tec_Inspection objInsp = new BLL_Tec_Inspection();
    int ReturnInspectionID = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (GetSessionUserID() == 0)
        {
            dvInspectionScheduling.Visible = false;
            lblMessage.Text = "Session expired!! Please log out and login again";
        }
        else
        {
            UserAccessValidation();
            if (!IsPostBack)
            {
                if (Session["COMPANYTYPE"].ToString() == "Surveyor")
                {
                    DDLFleetP.Visible = false;
                    lblFleet.Visible = false;
                    DDLCompany.Visible = true;
                    lblCompany.Visible = true;
                }
                else
                {
                    DDLFleetP.Visible = true;
                    lblFleet.Visible = true;
                    DDLCompany.Visible = false;
                    lblCompany.Visible = false;
                }
                LoadData();
                if (Request.QueryString["ScheduleID"] != null && Request.QueryString["SchDetailId"] != null)
                {
                    int ScheduleID = UDFLib.ConvertToInteger(Request.QueryString["ScheduleID"].ToString());
                    int InspectionID = UDFLib.ConvertToInteger(Request.QueryString["SchDetailId"].ToString());
                    Load_Schedule(ScheduleID, InspectionID);
                    DDLFleetP.Enabled = false;
                    DDLVesselP.Enabled = false;
                    DDLCompany.Enabled = false;
                    txtStartDate.Enabled = false;
                    txtOneTime.Enabled = false;
                }
                else
                {
                    DDLInspectorP.Items.Insert(0, new ListItem("-- Select --", null));
                }

                ///Inspection calendar
                if (Request.QueryString["Page"] != null && Request.QueryString["VesselID"] != null)
                {
                    DDLVesselP.SelectedValue = Convert.ToString(Request.QueryString["VesselID"]);
                    DDLVessselP_SelectedIndexChanged(null, null);
                    DDLInspectorP.SelectedValue = "-- Select --";
                    DDLVesselP.Enabled = false;
                    DDLFleetP.Enabled = false;
                    rdoFrequency.SelectedValue = "Onetime";
                    rdoFrequency_SelectedIndexChanged(null, null);
                }
            }
        }
    }
    protected void LoadData()
    {
        try
        {


            ViewState["AddEditFlag"] = "Add";

            grvChecklist.DataSource = null;
            grvChecklist.DataBind();

            tblChecklist.Visible = false;
            BLL_Infra_Company objCom = new BLL_Infra_Company();

            DataSet dtCompany = objCom.Get_Company_Parent_Child(1, 0, 0); ;
            DDLCompany.DataSource = dtCompany.Tables[0];
            DDLCompany.DataTextField = "Company_Name";
            DDLCompany.DataValueField = "ID";
            DDLCompany.DataBind();
            DDLCompany.Items.Insert(0, new ListItem("-- ALL --", null));


            BLL_Infra_InspectionType onjInsp = new BLL_Infra_InspectionType();
            DataTable FleetDT = objVsl.GetFleetList(Convert.ToInt32(Session["USERCOMPANYID"].ToString()));
            DDLFleetP.DataSource = FleetDT;
            DDLFleetP.DataTextField = "Name";
            DDLFleetP.DataValueField = "code";
            DDLFleetP.DataBind();
            DDLFleetP.Items.Insert(0, new ListItem("-- ALL --", null));


            DataTable dtVessel = objVsl.Get_VesselList(0, 0, 0, "", Convert.ToInt32(Session["USERCOMPANYID"].ToString()));
            DDLVesselP.DataSource = dtVessel;
            DDLVesselP.DataTextField = "Vessel_name";
            DDLVesselP.DataValueField = "Vessel_id";
            DDLVesselP.DataBind();
            DDLVesselP.Items.Insert(0, new ListItem("-- Select --", null));


            DataTable dtInsp = onjInsp.Get_InspectionTypeList();
            ddlInspectionTypeP.DataSource = dtInsp;
            ddlInspectionTypeP.DataTextField = "InspectionTypeName";
            ddlInspectionTypeP.DataValueField = "InspectionTypeId";
            ddlInspectionTypeP.DataBind();
            ddlInspectionTypeP.Items.Insert(0, new ListItem("--SELECT--", null));

            DataTable dtPort = objInfra.Get_PortList();

            if (dtPort.Rows.Count > 0)
            {
                drpPort.DataSource = dtPort;
                drpPort.DataValueField = "PORT_ID";
                drpPort.DataTextField = "PORT_NAME";
                drpPort.DataBind();
            }
            drpPort.Items.Insert(0, new ListItem() { Value = "0", Text = "-Select-" });

            txtStartDate.Enabled = true;
            DDLFleetP.Enabled = true;
            DDLVesselP.Enabled = true;
            ddlInspectionTypeP.Enabled = true;
            rdoFrequency.Enabled = true;
            drpPort.Visible = true;
            ViewState["ScheduleID"] = 0;
            ViewState["ScheduleID"] = "0";
            txtStartDate.Text = DateTime.Now.Date.ToString("dd/MMM/yy");
            txtEndDate.Text = "";
            txtOneTime.Text = DateTime.Now.Date.ToString("dd/MMM/yy");
            chkMonthWise.ClearSelection();
            chkWeekDays.ClearSelection();
            txtWeek.Text = "1";
            chkSendEmail.Checked = false;
            chkImages.Checked = false;
            txtInspRemark.Text = "";
            DDLFleetP.SelectedIndex = 0;
            DDLVesselP.SelectedIndex = 0;
            DDLInspectorP.SelectedIndex = 0;
            ddlDaysBefore.SelectedIndex = 6;
            rdoFrequency.SelectedIndex = 0;
            ddlInspectionTypeP.SelectedIndex = 0;
            ddlDuration.SelectedValue = "7";
            rdoFrequency_SelectedIndexChanged(null, null);
            txtDurJobs.Text = "1";
            Guid l = Guid.NewGuid();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    protected void DDLFleetP_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {


            DataTable dtVessel = objVsl.Get_VesselList(UDFLib.ConvertToInteger(DDLFleetP.SelectedValue), 0, Convert.ToInt32(Session["USERCOMPANYID"].ToString()), "", Convert.ToInt32(Session["USERCOMPANYID"].ToString()));
            DDLVesselP.Items.Clear();
            DDLVesselP.DataSource = dtVessel;
            DDLVesselP.DataTextField = "Vessel_name";
            DDLVesselP.DataValueField = "Vessel_id";
            DDLVesselP.DataBind();
            ListItem li = new ListItem("-- Select --", "0");
            DDLVesselP.Items.Insert(0, li);

            DataTable dtSup = objInsp.Get_Supritendent_Users(DDLFleetP.SelectedIndex == 0 ? null : UDFLib.ConvertIntegerToNull(DDLFleetP.SelectedValue), null);
            DDLInspectorP.DataSource = dtSup;
            DDLInspectorP.DataTextField = "Name";
            DDLInspectorP.DataValueField = "UserID";
            DDLInspectorP.DataBind();
            DDLInspectorP.Items.Insert(0, new ListItem("-- Select --", null));
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    protected void DDLCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DataTable dtVessel;

            if (DDLCompany.SelectedValue == "-- ALL --")
            {
                dtVessel = objVsl.Get_VesselList(UDFLib.ConvertToInteger(DDLFleetP.SelectedValue), 0, 0, "", 0);
            }
            else
            {
                dtVessel = objVsl.Get_VesselList(UDFLib.ConvertToInteger(DDLFleetP.SelectedValue), 0, Convert.ToInt32(DDLCompany.SelectedValue), "", Convert.ToInt32(DDLCompany.SelectedValue));
            }
            DDLVesselP.Items.Clear();
            DDLVesselP.DataSource = dtVessel;
            DDLVesselP.DataTextField = "Vessel_name";
            DDLVesselP.DataValueField = "Vessel_id";
            DDLVesselP.DataBind();
            ListItem li = new ListItem("-- Select --", "0");
            DDLVesselP.Items.Insert(0, li);

            DataTable dtSup = objInsp.TEC_WL_Get_Supritendent_UsersByCompanyID(DDLCompany.SelectedIndex == 0 ? null : UDFLib.ConvertIntegerToNull(DDLCompany.SelectedValue), null);
            DDLInspectorP.DataSource = dtSup;
            DDLInspectorP.DataTextField = "Name";
            DDLInspectorP.DataValueField = "UserID";
            DDLInspectorP.DataBind();
            DDLInspectorP.Items.Insert(0, new ListItem("-- Select --", null));
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    protected void ddlPort_SelectedIndex()
    {
        // DataTable dt = new DataTable();
        //dt.Columns.Add("ScheduleID");
        //dt.Columns.Add("PortID");
    }
    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
        {
            // Response.Redirect("~/default.aspx?msgid=1");

            lblMessage.Text = "you don't have sufficient previlege to access the requested page.";
            dvInspectionScheduling.Visible = false;
        }
        if (objUA.Add == 0)
        {
            // btnScheduleInspection.Enabled = false;

        }
        ViewState["del"] = objUA.Delete;

    }
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }
    protected void DDLVessselP_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DataTable dtSup = objInsp.Get_Supritendent_Users(DDLFleetP.SelectedIndex == 0 ? null : UDFLib.ConvertIntegerToNull(DDLFleetP.SelectedValue), DDLVesselP.SelectedIndex == 0 ? null : UDFLib.ConvertIntegerToNull(DDLVesselP.SelectedValue));
            DDLInspectorP.DataSource = dtSup;
            DDLInspectorP.DataTextField = "Name";
            DDLInspectorP.DataValueField = "UserID";
            DDLInspectorP.DataBind();
            DDLInspectorP.Items.Insert(0, new ListItem("-- Select --", null));

            grvChecklist.DataSource = null;
            grvChecklist.DataBind();


            if (ViewState["AddEditFlag"].ToString() == "Edit")
            {
                DataTable dtchecklist = objBllChecklist.Get_ChecklistsEdit(DDLVesselP.SelectedIndex == 0 ? null : UDFLib.ConvertIntegerToNull(DDLVesselP.SelectedValue), Convert.ToInt32(ViewState["ScheduleID"].ToString()));
                ViewState["ChecklistCount"] = dtchecklist.Rows.Count;
                if (dtchecklist.Rows.Count > 0)
                {
                    grvChecklist.DataSource = dtchecklist;
                    grvChecklist.DataTextField = "CheckList_Name";
                    grvChecklist.DataValueField = "Checklist_ID";
                    grvChecklist.DataBind();
                    //grvChecklist.Visible = true;
                    tblChecklist.Visible = true;


                    for (int i = 0; i < grvChecklist.Items.Count; i++)
                    {
                        if (dtchecklist.Rows[i]["Active_Status"].ToString() == "1")
                            grvChecklist.Items[i].Selected = true;
                    }
                }
            }
            else if (ViewState["AddEditFlag"].ToString() == "Add")//if (hdnScheduleID.Value == "Add")
            {
                DataTable dtchecklist = objBllChecklist.Get_Checklists(DDLVesselP.SelectedIndex == 0 ? null : UDFLib.ConvertIntegerToNull(DDLVesselP.SelectedValue));

                ViewState["ChecklistCount"] = dtchecklist.Rows.Count;
                if (dtchecklist.Rows.Count > 0)
                {
                    int count = 0;

                    grvChecklist.DataSource = dtchecklist;
                    grvChecklist.DataTextField = "CheckList_Name";
                    grvChecklist.DataValueField = "Checklist_ID";
                    grvChecklist.DataBind();
                    tblChecklist.Visible = true;

                }
                else
                {
                    tblChecklist.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    protected void btnSaveInspectinAndClose_Click(object sender, EventArgs e)
    {
        if (Save_Schedule() == true)
        {
            if (Request.QueryString["Page"] != null && Request.QueryString["VesselID"] != null)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "BindDate", "parent.BindDate('" + UDFLib.ConvertDateToNull(txtOneTime.Text).Value.ToString("dd/MM/yyyy") + "','" + ReturnInspectionID + "','" + UDFLib.ConvertUserDateFormat(DateTime.Parse(txtOneTime.Text).ToShortDateString()) + "');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "hidePopup", "parent.UpdatePage(); alert('Scheduler details saved.');", true);
            }
        }

    }
    protected void btnSaveInspection_Click(object sender, EventArgs e)
    {
        if (Save_Schedule() == true)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "script1", "alert('Inspection details saved.');", true);
        }
    }

    private DataTable NewDaemonScheduleTable()
    {
        DataTable dtSchedule = new DataTable();

        dtSchedule.Columns.Add("ScheduleID", typeof(int));
        dtSchedule.Columns.Add("Schedule_Name", typeof(String));
        dtSchedule.Columns.Add("Schedule_Desc", typeof(String));
        dtSchedule.Columns.Add("Start_Date", typeof(DateTime));
        dtSchedule.Columns.Add("End_Date", typeof(DateTime));
        dtSchedule.Columns.Add("FrequencyType", typeof(string));
        dtSchedule.Columns.Add("Frequency", typeof(int));
        dtSchedule.Columns.Add("InspectorID", typeof(int));
        dtSchedule.Columns.Add("Vessel_ID", typeof(int));
        dtSchedule.Columns.Add("InspectionTypeId", typeof(int));
        dtSchedule.Columns.Add("DurJobs", typeof(int));
        dtSchedule.Columns.Add("Created_By");
        dtSchedule.Columns.Add("Date_Of_Creation");
        dtSchedule.Columns.Add("Modified_By");
        dtSchedule.Columns.Add("Date_Of_Modification");
        dtSchedule.Columns.Add("Deleted_By");
        dtSchedule.Columns.Add("Date_Of_Deletion");


        return dtSchedule;
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
    protected void Load_Schedule(int ScheduleID, int SchDetailId)
    {
        try
        {

            ViewState["AddEditFlag"] = "Edit";

            DataTable dtVessel = objVsl.Get_VesselList(0, 0, Convert.ToInt32(0), "", Convert.ToInt32(Session["USERCOMPANYID"].ToString()));
            DDLVesselP.DataSource = dtVessel;
            DDLVesselP.DataTextField = "Vessel_name";
            DDLVesselP.DataValueField = "Vessel_id";
            DDLVesselP.DataBind();
            DDLVesselP.Items.Insert(0, new ListItem("-- Select --", null));
            ViewState["ScheduleID"] = ScheduleID;
            DDLFleetP.SelectedIndex = 0;
            DDLCompany.SelectedIndex = 0;
            int Me = 0;
            DataSet ds = objInsp.Get_Schedule_Details(ScheduleID, GetSessionUserID(), SchDetailId, 0);
            if (ds != null)
            {
                DataTable dtSchedule = ds.Tables[0];
                DataTable dtSettings = ds.Tables[1];
                DataTable dtInpectionSchedule = ds.Tables[2];

                Session["dtInpectionSchedule"] = dtInpectionSchedule;

                if (dtSchedule.Rows.Count > 0)
                {
                    ViewState["ScheduleID"] = dtSchedule.Rows[0]["ScheduleID"].ToString();

                    txtStartDate.Text = UDFLib.ConvertDateToNull(dtSchedule.Rows[0]["Start_Date"]).Value.ToString("dd/MMM/yy");
                    if (UDFLib.ConvertDateToNull(dtSchedule.Rows[0]["End_Date"]) != null)
                        txtEndDate.Text = UDFLib.ConvertDateToNull(dtSchedule.Rows[0]["End_Date"]).Value.ToString("dd/MMM/yy");
                    rdoFrequency.SelectedValue = dtSchedule.Rows[0]["FrequencyType"].ToString();

                    txtWeek.Text = dtSchedule.Rows[0]["Frequency"].ToString();
                    rdoFrequency_SelectedIndexChanged(null, null);
                    txtInspRemark.Text = dtSchedule.Rows[0]["Schedule_Desc"].ToString();
                    DDLVesselP.SelectedValue = dtSchedule.Rows[0]["Vessel_ID"].ToString();
                    DDLVessselP_SelectedIndexChanged(null, null);
                    try
                    {
                        DDLInspectorP.SelectedValue = dtSchedule.Rows[0]["InspectorID"].ToString();
                    }
                    catch (Exception)
                    {
                        DDLInspectorP.SelectedIndex = 0;
                    }

                    try
                    {
                        if (dtSchedule.Rows[0]["InspectionTypeId"].ToString().Trim().Length > 0)
                            ddlInspectionTypeP.SelectedValue = dtSchedule.Rows[0]["InspectionTypeId"].ToString();
                    }
                    catch (Exception)
                    {
                        ddlInspectionTypeP.SelectedIndex = 0;
                        ddlInspectionTypeP.Enabled = true;
                    }
                    txtDurJobs.Text = dtSchedule.Rows[0]["DurJobs"].ToString();
                }

                if (dtSettings.Rows.Count > 0)
                {

                    if (rdoFrequency.SelectedValue == "Onetime")
                        txtOneTime.Text = GetSettingDateValue(dtSettings, "OneTime", "");

                    if (rdoFrequency.SelectedValue == "Weekly")
                    {
                        DataTable dtWeekdays = GetSettingTable(dtSettings, "WEEKDAYS");

                        chkWeekDays.ClearSelection();
                        foreach (DataRow dr in dtWeekdays.Rows)
                        {
                            chkWeekDays.Items[UDFLib.ConvertToInteger(dr["key_value_int"]) - 1].Selected = true;
                        }
                    }
                    if (rdoFrequency.SelectedValue == "Monthwise")
                    {
                        DataTable dtMonthWise = GetSettingTable(dtSettings, "MONTHWISE");

                        chkMonthWise.ClearSelection();
                        foreach (DataRow dr in dtMonthWise.Rows)
                        {
                            chkMonthWise.Items[UDFLib.ConvertToInteger(dr["key_value_int"]) - 1].Selected = true;
                        }
                    }
                    if (rdoFrequency.SelectedValue == "Duration")
                    {
                        ddlDuration.SelectedValue = GetSettingTable(dtSettings, "Duration").Rows[0]["key_value_int"].ToString();
                    }

                    DataTable dtEmailDays = GetSettingTable(dtSettings, "EmailDays");
                    if (dtEmailDays.Rows.Count > 0)
                        if (UDFLib.ConvertToInteger(dtEmailDays.Rows[0]["key_value_int"]) > 0)
                        {
                            chkSendEmail.Checked = true;
                            ddlDaysBefore.SelectedValue = UDFLib.ConvertToInteger(dtEmailDays.Rows[0]["key_value_int"]).ToString();
                        }
                        else
                        {
                            chkSendEmail.Checked = false;
                        }

                    DataTable dtImages = GetSettingTable(dtSettings, "ShowImages");
                    if (dtImages.Rows.Count > 0)
                        if (UDFLib.ConvertToInteger(dtImages.Rows[0]["key_value_int"]) == 1)
                            chkImages.Checked = true;
                        else
                            chkImages.Checked = false;

                    DataSet dsPorts = objInsp.INSP_Get_InspectionPort(SchDetailId);

                    if (dsPorts.Tables[0].Rows.Count > 0)
                    {
                        try
                        {
                            drpPort.SelectedValue = dsPorts.Tables[0].Rows[0]["PortID"].ToString();
                        }
                        catch { }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    public DataTable GetSettingTable(DataTable dtSettings, string Key)
    {

        string expression = "Key_Name='" + Key + "'";
        string sortOrder = "Key_Name";

        DataView dv = new DataView(dtSettings, expression, sortOrder, DataViewRowState.CurrentRows);
        return dv.ToTable("Settings");
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
    protected bool Save_Schedule()
    {
        try
        {
            if (ValidateAll() == true)
            {

                DataTable dtSchedule = NewDaemonScheduleTable();
                DataTable dtSettings = NewDaemonSettingTable();
                DataTable dtChecklist = NewDaemonChecklist();
                DataTable dtPortList = NewDaemonPortList();
                Populate_Schedular_Table(dtSchedule);
                Populate_Settings_Table(dtSettings);
                dtSettings.DefaultView.RowFilter = "Key_Name='Onetime'";

                if (dtSettings.DefaultView.ToTable().Rows.Count > 0)
                {
                    string strdate = Convert.ToDateTime(dtSettings.DefaultView.ToTable().Rows[0]["Key_Value_Date"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");

                    int response1 = 0;
                    int response = objInsp.CheckExists_Schedule(Convert.ToInt32(dtSchedule.Rows[0]["InspectorID"].ToString()), strdate, Convert.ToInt32(dtSchedule.Rows[0]["DurJobs"].ToString()), GetSessionUserID(), response1);
                    if (response == -5)
                    {
                        string js = "alert('Schedule already exists.');";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script1", js, true);
                        return false;
                    }
                }

                Populate_Checklist_Table(dtChecklist);
                dtPortList.Rows.Add(drpPort.SelectedValue.ToString());
                int Res;

                if (Request.QueryString["ScheduleID"] != null)
                {
                    Res = objInsp.Save_Schedule(dtSchedule, dtSettings, dtChecklist, GetSessionUserID(), UDFLib.ConvertToInteger(Request.QueryString["SchDetailId"].ToString()), dtPortList);
                }
                else
                {
                    Res = objInsp.Save_Schedule(dtSchedule, dtSettings, dtChecklist, GetSessionUserID(), null, dtPortList);
                    if (Request.QueryString["Page"] != null && Request.QueryString["VesselID"] != null && Request.QueryString["Surv_Details_ID"] != null && Request.QueryString["Surv_Vessel_ID"] != null && Request.QueryString["OfficeID"] != null)
                    {
                        if (Res > 0)
                        {
                            objInsp = new BLL_Tec_Inspection();
                            objInsp.SurveyRenewalInspection(UDFLib.ConvertToInteger(Request.QueryString["Surv_Details_ID"]), UDFLib.ConvertToInteger(Request.QueryString["Surv_Vessel_ID"]), UDFLib.ConvertToInteger(Request.QueryString["VesselID"]), UDFLib.ConvertToInteger(Request.QueryString["OfficeID"]), Res, ref ReturnInspectionID);
                        }
                    }
                }
                if (Res == -1)
                {
                    string js = "alert('Schedule name already exists.');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script1", js, true);
                    return false;
                }
                else if (Res > 0)
                {
                    ViewState["ScheduleID"] = Res.ToString();
                    ViewState["ScheduleID"] = ViewState["ScheduleID"];
                    return true;
                }
                else
                    return false; // procedure 

            }
            else
                return false; // Validation false 
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            return false;
        }
    }

    protected void Populate_Checklist_Table(DataTable dtChecklist)
    {
        int ScheduleID = UDFLib.ConvertToInteger(ViewState["ScheduleID"]);

        for (int i = 0; i < grvChecklist.Items.Count; i++)
        {
            if (grvChecklist.Items[i].Selected == true)
                dtChecklist.Rows.Add(grvChecklist.Items[i].Value, "1");
            else
                dtChecklist.Rows.Add(grvChecklist.Items[i].Value, "0");
        }
    }
    private DataTable NewDaemonPortList()
    {
        DataTable dtPort = new DataTable();

        dtPort.Columns.Add("PID", typeof(int));
        return dtPort;
    }
    private DataTable NewDaemonChecklist()
    {
        DataTable dtChecklist = new DataTable();
        dtChecklist.Columns.Add("ID", typeof(int));
        dtChecklist.Columns.Add("VALUE", typeof(string));
        return dtChecklist;
    }

    /// <summary>
    /// To validate all controls before save.
    /// </summary>
    /// <returns>True : if validation successeds || False : if validation fail.</returns>
    protected bool ValidateAll()
    {
        string js = "";
        try
        {
            if (DDLVesselP.SelectedIndex == 0)
            {
                js = "alert('Vessel is required field.');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script1", js, true);
                return false;
            }

            if (UDFLib.ConvertIntegerToNull(txtDurJobs.Text) == null)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "alert('Inspection duration is not Valid.');", true);
                return false;
            }
            if (UDFLib.ConvertIntegerToNull(txtDurJobs.Text) <= 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "alert('Inspection duration cannot be smaller than or equal to zero.');", true);
                return false;
            }
            if (DDLInspectorP.SelectedIndex == 0)
            {
                js = "alert('Inspector is required field.');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script1", js, true);
                return false;
            }
            if (ddlInspectionTypeP.SelectedIndex == 0)
            {
                js = "alert('Inspection type is required field.');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script1", js, true);
                return false;
            }
            if (rdoFrequency.SelectedIndex == 2)
            {
                DateTime temp;
                if (txtOneTime.Text.Trim()=="")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script1", "alert('Schedule date is required field.');", true);
                    return false;
                }
                if (!DateTime.TryParse(txtOneTime.Text, out temp))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script1", "$('#txtOneTime').focus();alert('Invalid Schedule Date');", true);
                    return false;
                }
                if (drpPort.SelectedIndex == 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script1", "alert('Select Port');", true);
                    return false;
                }
            }

            if (rdoFrequency.SelectedIndex == 0)
            {
                bool lFlag = true;
                foreach (ListItem li in chkWeekDays.Items)
                {
                    if (li.Selected == true)
                    {
                        lFlag = false;
                    }
                }
                if (lFlag)
                {
                    js = "alert('Select at least one day of the week.');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script1", js, true);
                    return false;
                }
                try
                {
                    int week = Convert.ToInt32(txtWeek.Text);
                    if (week < 1)
                    {
                        js = "alert('Invalid value in week frequency.');";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script1", js, true);
                        return false;
                    }
                }
                catch (Exception)
                {

                    js = "alert('Invalid value in Week Frequency!');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script1", js, true);
                    return false;
                }
                try
                {
                    DateTime dt = Convert.ToDateTime(txtStartDate.Text);
                }
                catch (Exception)
                {

                    js = "alert('Select start date.');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script1", js, true);
                    return false;
                }
                if (UDFLib.ConvertDateToNull(txtEndDate.Text) != null)
                {

                    if (UDFLib.ConvertDateToNull(txtEndDate.Text) < UDFLib.ConvertDateToNull(txtStartDate.Text))
                    {
                        js = "alert('End date cannot be lesser than start date.');";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script1", js, true);
                        return false;
                    }
                }

            }
            if (rdoFrequency.SelectedIndex == 1)
            {
                bool lFlag = true;
                foreach (ListItem li in chkMonthWise.Items)
                {
                    if (li.Selected == true)
                    {
                        lFlag = false;
                    }
                }
                if (lFlag)
                {
                    js = "alert('Select atleast one month.');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script1", js, true);
                    return false;
                }
                try
                {
                    DateTime dt = Convert.ToDateTime(txtStartDate.Text);
                }
                catch (Exception)
                {

                    js = "alert('Select start date.');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script1", js, true);
                    return false;
                }
                if (UDFLib.ConvertDateToNull(txtEndDate.Text) != null)
                {

                    if (UDFLib.ConvertDateToNull(txtEndDate.Text) < UDFLib.ConvertDateToNull(txtStartDate.Text))
                    {
                        js = "alert('End date cannot be lesser than start date.');";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script1", js, true);
                        return false;
                    }
                }


            }
            if (rdoFrequency.SelectedIndex == 3)
            {
                try
                {
                    DateTime dt = Convert.ToDateTime(txtStartDate.Text);
                }
                catch (Exception)
                {

                    js = "alert('Select start date.');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script1", js, true);
                    return false;
                }
                if (UDFLib.ConvertDateToNull(txtEndDate.Text) != null)
                {

                    if (UDFLib.ConvertDateToNull(txtEndDate.Text) < UDFLib.ConvertDateToNull(txtStartDate.Text))
                    {
                        js = "alert('End date cannot be lesser than start date.');";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script1", js, true);
                        return false;
                    }
                }
            }

        }
        catch (Exception ex)
        {
            string jsSqlError2 = "alert('" + ex.Message + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "jsSqlError2", jsSqlError2, true);
        }
        return true;
    }
    private int GetFrequencyValue()
    {
        int RetValue = 0;
        switch (rdoFrequency.SelectedValue)
        {
            case "Onetime":
                // Daily
                RetValue = 1;
                break;
            case "Weekly":
                // Weekly
                RetValue = UDFLib.ConvertToInteger(txtWeek.Text);
                break;
            case "Monthwise":
                // Start of each month
                RetValue = 1;
                break;
            case "Duration":
                // End of each month
                RetValue = 1;
                break;
        }
        return RetValue;
    }

    protected void Populate_Schedular_Table(DataTable dtSchedule)
    {
        int ScheduleID = UDFLib.ConvertToInteger(ViewState["ScheduleID"]);

        dtSchedule.Rows.Add(
            ScheduleID,
         DDLInspectorP.SelectedItem.Text,
            txtInspRemark.Text,
            txtStartDate.Text,
            UDFLib.ConvertDateToNull(txtEndDate.Text),
            rdoFrequency.SelectedValue,
            GetFrequencyValue(),
            DDLInspectorP.SelectedIndex == 0 ? 0 : UDFLib.ConvertToInteger(DDLInspectorP.SelectedValue),
            DDLVesselP.SelectedIndex == 0 ? 0 : UDFLib.ConvertToInteger(DDLVesselP.SelectedValue), ddlInspectionTypeP.SelectedIndex == 0 ? 0 : UDFLib.ConvertToInteger(ddlInspectionTypeP.SelectedValue), UDFLib.ConvertToInteger(txtDurJobs.Text)
            );


    }

    protected void Populate_Settings_Table(DataTable dtSettings)
    {
        int ScheduleID = UDFLib.ConvertToInteger(ViewState["ScheduleID"]);


        switch (rdoFrequency.SelectedValue)
        {

            case "Onetime":
                // One Time

                dtSettings.Rows.Add(ScheduleID, "OneTime", null, UDFLib.ConvertDateToNull(txtOneTime.Text), null);

                break;

            case "Weekly":
                // Weekly
                foreach (ListItem li in chkWeekDays.Items)
                {
                    if (li.Selected == true)
                    {
                        dtSettings.Rows.Add(ScheduleID, "WEEKDAYS", li.Text, null, li.Value);
                    }
                }
                break;
            case "Monthwise":
                // Weekly
                foreach (ListItem li in chkMonthWise.Items)
                {
                    if (li.Selected == true)
                    {
                        dtSettings.Rows.Add(ScheduleID, "MONTHWISE", li.Text, null, li.Value);
                    }
                }
                break;
            case "Duration":
                // One Time

                dtSettings.Rows.Add(ScheduleID, "Duration", null, null, UDFLib.ConvertIntegerToNull(ddlDuration.SelectedValue));

                break;
        }


        dtSettings.Rows.Add(ScheduleID, "EmailDays", null, null, ddlDaysBefore.SelectedIndex < 0 ? 0 : UDFLib.ConvertToInteger(ddlDaysBefore.SelectedValue));
        dtSettings.Rows.Add(ScheduleID, "ShowImages", null, null, chkImages.Checked == true ? 1 : 0);


    }
    protected void rdoFrequency_SelectedIndexChanged(object sender, EventArgs e)
    {
        pnlOneTime.Visible = false;
        pnlWeekly.Visible = false;
        pnlStartMonth.Visible = false;
        pnlEndMonth.Visible = false;
        pnlMonthWise.Visible = false;
        pnlDuration.Visible = false;
        dvRange.Visible = true;
        drpPort.ClearSelection();

        switch (rdoFrequency.SelectedValue)
        {
            case "Onetime":
                pnlOneTime.Visible = true;
                dvRange.Visible = false;
                break;
            case "Weekly":
                pnlWeekly.Visible = true;
                break;
            case "Monthwise":
                pnlMonthWise.Visible = true;
                break;
            case "Duration":
                pnlDuration.Visible = true;
                ddlDuration.SelectedValue = "90";
                break;

        }
    }

}