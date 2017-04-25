using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.Infrastructure;
using SMS.Business.FMS;
using SMS.Properties;
using System.Drawing;

public partial class FMS_FMSSchedule : System.Web.UI.Page
{

    UserAccess objUA = new UserAccess();
    BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
    BLL_Infra_VesselLib objVsl = new BLL_Infra_VesselLib();

    List<string> strDocIDList = new List<string>();
    BLL_FMS_Document objInsp = new BLL_FMS_Document();
    TreeNode Tempnode = new TreeNode();
     protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            UserAccessValidation();
            if (!IsPostBack)
            {
                LoadData();
                if (Request.QueryString["SchID"] != null && Request.QueryString["SchID"].ToString() != "")
                {
                    Load_Schedule(UDFLib.ConvertToInteger(Request.QueryString["DocID"].ToString()), UDFLib.ConvertToInteger(Request.QueryString["SchID"]));

                }

                string originalPath = Request.UrlReferrer.AbsolutePath.ToString();
                string parentDirectory = UDFLib.GetPageURL(originalPath);
                DataSet ds = objInsp.FMS_Get_SchTree(GetSessionUserID(), 0, parentDirectory);
                if (ds.Tables.Count > 0)
                {
                    recursiveTree(ds.Tables[0], 0, null);
               
                }
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    protected void LoadData()
    {
        try
        {
            DataTable FleetDT = objVsl.GetFleetList(Convert.ToInt32(Session["USERCOMPANYID"].ToString()));
            ddlFleet.DataSource = FleetDT;
            ddlFleet.DataTextField = "Name";
            ddlFleet.DataValueField = "code";
            ddlFleet.DataBind();
            ddlFleet.Items.Insert(0, new ListItem("- ALL -", "0"));

            DataSet dsVessel = objInsp.FMS_Get_VesselSchInfo(UDFLib.ConvertToInteger(Request.QueryString["DocId"].ToString()), UDFLib.ConvertToInteger(Session["USERCOMPANYID"].ToString()), null); /* This Function in DAL is to be used*/

            if (dsVessel.Tables.Count > 0)
            {
                gvVesselSch.DataSource = dsVessel.Tables[0];

                gvVesselSch.DataBind();
            }
            txtStartDate.Enabled = true;


            rdoFrequency.Enabled = true;

            ViewState["ScheduleID"] = 0;
            txtStartDate.Text = DateTime.Now.Date.ToString("dd/MMM/yy");
            txtEndDate.Text = "";
            txtOneTime.Text = DateTime.Now.Date.ToString("dd/MMM/yy");
            chkMonthWise.ClearSelection();
            chkWeekDays.ClearSelection();
            txtWeek.Text = "1";

            rdoFrequency.SelectedIndex = 0;

            ddlDuration.SelectedValue = "7";
            rdoFrequency_SelectedIndexChanged(null, null);



            Guid l = Guid.NewGuid();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

   protected void recursiveTree(DataTable dtmenu, int ID, TreeNode meni)
    {
        try
        {
            string SelectedDocID = Request.QueryString["DocID"].ToString();

            DataRow[] drInners;
            if (ID.ToString() == "0")
                drInners = dtmenu.Select("ParentID =0");
            else
                drInners = dtmenu.Select("ParentID ='" + ID.ToString() + "' ");

            if (drInners.Length != 0)
            {

                foreach (DataRow drInner in drInners)
                {
                    TreeNode miner;
                    miner = new TreeNode(drInner["LogFileID"].ToString().Trim(), drInner["ID"].ToString());
                    if (Convert.ToString(drInner["NodeType"]) == "1")
                    {
                        miner.ShowCheckBox = false;
                    }
                    miner.SelectAction = TreeNodeSelectAction.None;
                    if (meni == null)
                    {
                        trvFile.Nodes.Add(miner);
                    }
                    else
                    {
                        meni.ChildNodes.Add(miner);
                        meni.CollapseAll();
                    }

                    int ID1 = Convert.ToInt32(drInner["ID"].ToString());

                    if (miner.Value == SelectedDocID)
                    {
                        Tempnode = miner;
                    }

                    recursiveTree(dtmenu, ID1, miner);


                }



            }

            ExpandToRoot(Tempnode);
            Tempnode.Checked = true;
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }

    }

  
   private void ExpandToRoot(TreeNode node)
   {
       try
       {
           node.Expand();
           if (node.Parent != null)
           {

               ExpandToRoot(node.Parent);
           }
       }
       catch (Exception ex)
       {
           UDFLib.WriteExceptionLog(ex);
       }

    
   }
   protected void UserAccessValidation()
    {
        try
        {
            int CurrentUserID = GetSessionUserID();
            string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

            objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

            if (objUA.View == 0)
            {
                lblMessage.Text = "you don't have sufficient previlege to access the requested page. ";
                dvInspectionScheduling.Visible = false;
            }
            if (objUA.Add == 0)
            {
            }
            ViewState["del"] = objUA.Delete;
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }
    
    protected void btnSaveAndClose_Click(object sender, EventArgs e)
    {
        try
        {
            //This code is commented by Pranav Sakpal on 22-06-2016 implementing NA feature again at SP leve
            //if (rdoFrequency.SelectedValue == "NA")
            //{
            //    Drop_OldSchedule();
            //}

            if (Save_Schedule() == true)
            {
                string js1 = "alert('Schedular Details Saved.');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script1", js1, true);
              
                string js2 = "parent.UpdatePage(); ";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "hidePopup", js2, true);
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
   
    private DataTable NewDaemonScheduleTable()
    {
        DataTable dtSchedule = new DataTable();

        dtSchedule.Columns.Add("ScheduleID", typeof(int));
        dtSchedule.Columns.Add("Schedule_Name", typeof(string));
        dtSchedule.Columns.Add("Schedule_Desc", typeof(string));
        dtSchedule.Columns.Add("Start_Date", typeof(DateTime));
        dtSchedule.Columns.Add("End_Date", typeof(DateTime));
        dtSchedule.Columns.Add("FrequencyType", typeof(string));
        dtSchedule.Columns.Add("Frequency", typeof(int));
        dtSchedule.Columns.Add("Vessel_ID", typeof(int));
        dtSchedule.Columns.Add("DocumentID", typeof(int));
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

    /// <summary>
    /// function is use to bind forms schedule details
    /// </summary>
    /// <param name="DocID">Selected Form ID</param>
    /// <param name="SchID">Schedule ID were Form is schedule to the vessel</param>
    protected void Load_Schedule(int DocID,int SchID)
    {
        try
        {

            ViewState["AddEditFlag"] = "Edit";

            txtStartDate.Enabled = true;


            rdoFrequency.Enabled = true;

            ViewState["ScheduleID"] = 0;
            txtStartDate.Text = DateTime.Now.Date.ToString("dd/MMM/yy");
            txtEndDate.Text = "";
            txtOneTime.Text = DateTime.Now.Date.ToString("dd/MMM/yy");
            chkMonthWise.ClearSelection();
            chkWeekDays.ClearSelection();
            txtWeek.Text = "1";

            rdoFrequency.SelectedIndex = 0;
            txtRemark.Text = "";
            ddlDuration.SelectedValue = "7";
            txtDueDay.Text = "";
            txtDueDay.Enabled = false;
            chkEOM.Checked = true;
            rdoFrequency_SelectedIndexChanged(null, null);

            int Me = 0;
            DataSet ds = objInsp.Get_Schedule_Details(UDFLib.ConvertToInteger(DocID), UDFLib.ConvertToInteger(SchID));
            DataTable dtSchedule = ds.Tables[0];
            DataTable dtSettings = ds.Tables[1];


            if (dtSchedule.Rows.Count > 0)
            {
                if (dtSchedule.Rows[0]["FrequencyType"].ToString() != "NA")
                {
                    rdoFrequency_SelectedIndexChanged(null, null);
                    ViewState["ScheduleID"] = dtSchedule.Rows[0]["Schedule_ID"].ToString();
                    ViewState["PreFrequency"] = dtSchedule.Rows[0]["FrequencyType"].ToString();
                    rdoFrequency.SelectedValue = dtSchedule.Rows[0]["FrequencyType"].ToString();
                    rdoFrequency_SelectedIndexChanged(null, null);
                  //  if (UDFLib.ConvertDateToNull(dtSchedule.Rows[0]["End_Date"]) != null)
                    if (UDFLib.ConvertDateToNull(dtSchedule.Rows[0]["Start_Date"]) != null)      
                        txtStartDate.Text = UDFLib.ConvertDateToNull(dtSchedule.Rows[0]["Start_Date"]).Value.ToString("dd/MMM/yy");
                    if (UDFLib.ConvertDateToNull(dtSchedule.Rows[0]["End_Date"]) != null)
                        txtEndDate.Text = UDFLib.ConvertDateToNull(dtSchedule.Rows[0]["End_Date"]).Value.ToString("dd/MMM/yy");                   

                    txtWeek.Text = dtSchedule.Rows[0]["Frequency"].ToString();
                    txtRemark.Text = dtSchedule.Rows[0]["Schedule_Desc"].ToString();
                    
                }
                else
                {
                    rdoFrequency.SelectedValue = dtSchedule.Rows[0]["FrequencyType"].ToString();
                    txtRemark.Text = dtSchedule.Rows[0]["Schedule_Desc"].ToString();
                    ViewState["PreFrequency"] = dtSchedule.Rows[0]["FrequencyType"].ToString();
                    rdoFrequency_SelectedIndexChanged(null, null);
                }


            }
            
            if (dtSettings.Rows.Count > 0)
            {

                if (rdoFrequency.SelectedValue == "Onetime")
                {
                    txtOneTime.Text = GetSettingDateValue(dtSettings, "OneTime", "");

                }
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

                    string strDueDay = GetSettingString(dtSettings, "DueDay", "EOM");
                    chkMonthWise.ClearSelection();
                    foreach (DataRow dr in dtMonthWise.Rows)
                    {
                        chkMonthWise.Items[UDFLib.ConvertToInteger(dr["key_value_int"]) - 1].Selected = true;
                    }

                    if (strDueDay == "EOM")
                    {
                        chkEOM.Checked = true;
                        txtDueDay.Enabled = false;
                    }
                    else
                    {
                        chkEOM.Checked = false;
                        txtDueDay.Text = strDueDay;
                        txtDueDay.Enabled = true;
                    }


                }
                if (rdoFrequency.SelectedValue == "Duration")
                {
                    ddlDuration.SelectedValue = GetSettingTable(dtSettings, "Duration").Rows[0]["key_value_int"].ToString();
                }


            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
           
        }
    }
    protected void Drop_OldSchedule()
    {
        try
        {
            DataSet ds = objInsp.Get_Schedule_Details(UDFLib.ConvertToInteger(Request.QueryString["DocId"].ToString()), UDFLib.ConvertToInteger(Request.QueryString["SchID"].ToString()));
            DataTable dtSchedule = ds.Tables[0];


            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                objInsp.FMS_Update_DocumentSchedule(UDFLib.ConvertToInteger(ds.Tables[0].Rows[i]["Schedule_ID"].ToString()), GetSessionUserID());
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
                
                Populate_Schedular_Table(dtSchedule);
                Populate_Settings_Table(dtSettings);
                int Res;
              
               Res = objInsp.Save_Schedule(dtSchedule, dtSettings, GetSessionUserID(), UDFLib.ConvertToInteger(Request.QueryString["DocId"].ToString()));
          
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
                    return false;

            }
            else
                return false;
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            return false;
        }
    }
    /// <summary>
    /// This function will validate all controls.
    /// </summary>
    /// <returns></returns>
    protected bool ValidateAll()
    {
        try
        {
            string js = "";
            bool iChecked = false;

            if (rdoFrequency.SelectedIndex == 2)
            {

                DateTime temp;
                if (!DateTime.TryParse(txtOneTime.Text, out temp))
                {
                    js = "alert('Schedule Date is required field!');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script1", js, true);
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
                        break;
                    }
                }
                if (lFlag)
                {
                    js = "alert('Select at least one day of the week!');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script1", js, true);
                    return false;
                }
                try
                {
                    int week = Convert.ToInt32(txtWeek.Text);
                    if (week < 1)
                    {
                        js = "alert('Invalid value in Week Frequency!');";
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

                    js = "alert('Select Start Date!');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script1", js, true);
                    return false;
                }
                if (UDFLib.ConvertDateToNull(txtEndDate.Text) != null)
                {

                    if (UDFLib.ConvertDateToNull(txtEndDate.Text) < UDFLib.ConvertDateToNull(txtStartDate.Text))
                    {
                        js = "alert('End Date cannot be lesser than Start Date!');";
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
                    js = "alert('Select atleast one Month!');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script1", js, true);
                    return false;
                }
                try
                {
                    DateTime dt = Convert.ToDateTime(txtStartDate.Text);
                }
                catch (Exception)
                {

                    js = "alert('Select Start Date!');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script1", js, true);
                    return false;
                }
                if (UDFLib.ConvertDateToNull(txtEndDate.Text) != null)
                {

                    if (UDFLib.ConvertDateToNull(txtEndDate.Text) < UDFLib.ConvertDateToNull(txtStartDate.Text))
                    {
                        js = "alert('End Date cannot be lesser than Start Date!');";
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

                    js = "alert('Select Start Date!');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script1", js, true);
                    return false;
                }
                if (UDFLib.ConvertDateToNull(txtEndDate.Text) != null)
                {

                    if (UDFLib.ConvertDateToNull(txtEndDate.Text) < UDFLib.ConvertDateToNull(txtStartDate.Text))
                    {
                        js = "alert('End Date cannot be lesser than Start Date!');";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script1", js, true);
                        return false;
                    }
                }
            }
            int CheckCount = 0;
            for (int i = 0; i < gvVesselSch.Rows.Count; i++)
            {
                HiddenField hdnVesselID = ((HiddenField)gvVesselSch.Rows[i].Cells[0].FindControl("hdnVesselID"));
                CheckBox chkSch = ((CheckBox)gvVesselSch.Rows[i].Cells[4].FindControl("chkSch"));
                if (chkSch.Checked == true)
                {
                    CheckCount++;
                }
            }

            if (CheckCount == 0)
            {
                string jsSch1 = "alert('Please select atleast one vessel for scheduling');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "jsSch1", jsSch1, true);
                return false;
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            return false;
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
            case "NA":
                // End of each month
                RetValue = 0;
                break;
        }
        return RetValue;
    }
    /// <summary>
    /// This function will update datatable which will be used to pass in SP - This will add valid data related to schecdule is added to datatable.
    /// </summary>
    /// <param name="dtSchedule">This parameter is use to add schedule details to this.</param>
    protected void Populate_Schedular_Table(DataTable dtSchedule)
    {
        try
        {
            int ScheduleID = UDFLib.ConvertToInteger(ViewState["ScheduleID"]);//This is to add schedule id

            foreach (TreeNode node in trvFile.CheckedNodes)
            {
                if (node.Checked == true)
                {
                    for (int i = 0; i < gvVesselSch.Rows.Count; i++)
                    {
                        HiddenField hdnVesselID = ((HiddenField)gvVesselSch.Rows[i].Cells[0].FindControl("hdnVesselID"));
                        CheckBox chkSch = ((CheckBox)gvVesselSch.Rows[i].Cells[4].FindControl("chkSch"));
                        if (chkSch.Checked == true)
                        {
                            string endDate = txtEndDate.Text;
                            switch (rdoFrequency.SelectedValue)
                            {
                                case "Onetime":
                                  
                                    dtSchedule.Rows.Add(
                                    null,
                                     txtRemark.Text,
                                    txtRemark.Text,
                                    txtOneTime.Text,
                                    UDFLib.ConvertDateToNull(endDate),
                                    rdoFrequency.SelectedValue,
                                    GetFrequencyValue(),
                                    hdnVesselID.Value == "0" ? 0 : UDFLib.ConvertToInteger(hdnVesselID.Value),
                                    Convert.ToInt32(node.Value));
                                    break;
                                case "Weekly":
                                    dtSchedule.Rows.Add(
                                   null,
                                    txtRemark.Text,
                                   txtRemark.Text,
                                   txtStartDate.Text,
                                   UDFLib.ConvertDateToNull(endDate),
                                   rdoFrequency.SelectedValue,
                                   GetFrequencyValue(),
                                   hdnVesselID.Value == "0" ? 0 : UDFLib.ConvertToInteger(hdnVesselID.Value),
                                   Convert.ToInt32(node.Value));
                                  
                                    break;
                                case "Monthwise":
                                    dtSchedule.Rows.Add(
                                    null,
                                     txtRemark.Text,
                                    txtRemark.Text,
                                    txtStartDate.Text,
                                    UDFLib.ConvertDateToNull(endDate),
                                    rdoFrequency.SelectedValue,
                                    GetFrequencyValue(),
                                    hdnVesselID.Value == "0" ? 0 : UDFLib.ConvertToInteger(hdnVesselID.Value),
                                    Convert.ToInt32(node.Value));
                                    break;
                                case "Duration":

                                    dtSchedule.Rows.Add(
                                    null,
                                     txtRemark.Text,
                                    txtRemark.Text,
                                    txtStartDate.Text,
                                    UDFLib.ConvertDateToNull(endDate),
                                    rdoFrequency.SelectedValue,
                                    GetFrequencyValue(),
                                    hdnVesselID.Value == "0" ? 0 : UDFLib.ConvertToInteger(hdnVesselID.Value),
                                    Convert.ToInt32(node.Value));                                  
                                    break;
                                case "NA"://This case is for NONE selection this will add none frequency type and valid parameters to datatable to update in SP.

                                    dtSchedule.Rows.Add(
                                    ScheduleID,
                                     txtRemark.Text,
                                    txtRemark.Text,
                                    txtOneTime.Text,
                                    UDFLib.ConvertDateToNull(endDate),
                                    rdoFrequency.SelectedValue,
                                    GetFrequencyValue(),
                                    hdnVesselID.Value == "0" ? 0 : UDFLib.ConvertToInteger(hdnVesselID.Value),
                                    Convert.ToInt32(node.Value));
                                    break;
                            
                            }
                           

                        }


                    }
                }
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }

    
    }
    /// <summary>
    /// This function will check rdoFrequency control properties which is property is selected.
    /// That will be added to setting table.
    /// </summary>
    /// <param name="dtSettings">This is input datatable to set property selection in it </param>
    protected void Populate_Settings_Table(DataTable dtSettings)
    {
        try
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
                    // Monthwise
                    foreach (ListItem li in chkMonthWise.Items)
                    {
                        if (li.Selected == true)
                        {
                            dtSettings.Rows.Add(ScheduleID, "MONTHWISE", li.Text, null, li.Value);
                        }
                    }

                    if (chkEOM.Checked == true)
                    {
                        dtSettings.Rows.Add(ScheduleID, "DueDay", "EOM", null, null);
                    }
                    else if (chkEOM.Checked == false)
                    {
                        dtSettings.Rows.Add(ScheduleID, "DueDay", txtDueDay.Text, null, null);
                    }

                    break;
                case "Duration":
                    // One Time

                    dtSettings.Rows.Add(ScheduleID, "Duration", null, null, UDFLib.ConvertIntegerToNull(ddlDuration.SelectedValue));

                    break;

                case "NA":
                    // This will remove all schedules from status table in office and vessel and this will update schedule frequency type to NA.

                    dtSettings.Rows.Add(ScheduleID, "NA", null, null, null);

                    break;
            }


            dtSettings.Rows.Add(ScheduleID, "EmailDays", null, null);
            dtSettings.Rows.Add(ScheduleID, "ShowImages", null, null);
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }


    }

    protected void ClearFields()
    {
        txtEndDate.Text = "";
        chkMonthWise.ClearSelection();
        chkWeekDays.ClearSelection();
        txtDueDay.Text = "";
        chkEOM.Checked = true;
        txtDueDay.Enabled = false;
        txtStartDate.Text = DateTime.Now.ToString("dd/MMM/yy");

    }
    protected void rdoFrequency_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            pnlOneTime.Visible = false;
            pnlWeekly.Visible = false;
            pnlStartMonth.Visible = false;
            pnlEndMonth.Visible = false;
            pnlMonthWise.Visible = false;
            pnlDuration.Visible = false;
            dvRange.Visible = true;
            ClearFields();

            switch (rdoFrequency.SelectedValue)
            {
                case "Onetime":
                    pnlOneTime.Visible = true;
                    dvRange.Visible = false;
                    lblMandetory.Visible = false;
                    break;
                case "Weekly":
                    pnlWeekly.Visible = true;
                    lblMandetory.Visible = false;
                    break;
                case "Monthwise":
                    pnlMonthWise.Visible = true;
                    lblMandetory.Visible = false;
                    break;
                case "Duration":
                    pnlDuration.Visible = true;
                    ddlDuration.SelectedValue = "90";
                    lblMandetory.Visible = false;
                    break;
                case "NA":
                    dvRange.Visible = false;
                    lblMandetory.Visible = true;                  
                    if( ViewState["PreFrequency"].ToString()!="NA")
                    txtRemark.Text = "";
                    break;

            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    protected void ddlFleet_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DataSet dsVessel = objInsp.FMS_Get_VesselSchInfo(UDFLib.ConvertToInteger(Request.QueryString["DocId"].ToString()), UDFLib.ConvertToInteger(Session["USERCOMPANYID"].ToString()), UDFLib.ConvertToInteger(ddlFleet.SelectedValue)); /* This Function in DAL is to be used*/
            if (dsVessel.Tables.Count > 0)
            {
                gvVesselSch.DataSource = dsVessel.Tables[0];

                gvVesselSch.DataBind();
            }
        
            string jsTree = "LoadTree();";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "jsTree", jsTree, true);
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    protected void chkAssignAllVessel_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            CheckBox chk = (CheckBox)sender;
            if (chk.Checked == true)
            {
                for (int i = 0; i < gvVesselSch.Rows.Count; i++)
                {
                    CheckBox chkAssign = ((CheckBox)gvVesselSch.Rows[i].Cells[3].FindControl("chkVesselAssign"));
                    chkAssign.Checked = true;
                }
            }
            else
            {
                LoadData();
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    protected void chkSchAllVessel_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            CheckBox chk = (CheckBox)sender;
            if (chk.Checked == true)
            {
                for (int i = 0; i < gvVesselSch.Rows.Count; i++)
                {
                    CheckBox chkSch = ((CheckBox)gvVesselSch.Rows[i].Cells[3].FindControl("chkSch"));
                    chkSch.Checked = true;
                }
            }
            else
            {
                LoadData();
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
  
    protected void BtnVesselAssign_Click(object sender, EventArgs e)
    {
        try
        {   // hdfAlertFlag value set 0 when user click on canel button and set 1 for ok button.
            if (hdfAlertFlag.Value == "0")
            {
                LoadData();
            }
            else
            {
                int CheckCount = 0;
                string CurrDocID = UDFLib.ConvertStringToNull(Request.QueryString["DocId"].ToString());

                foreach (TreeNode node in trvFile.CheckedNodes)
                {
                    if (node.Checked == true)
                    {
                        int DocID = UDFLib.ConvertToInteger(node.Value);
                        
                        if (DocID > 0)
                        {
                            for (int i = 0; i < gvVesselSch.Rows.Count; i++)
                            {
                                //This HiddenField is added to get database values of assignment.
                                HiddenField hfAssign = ((HiddenField)gvVesselSch.Rows[i].Cells[3].FindControl("hdnAssignedCheck"));

                                CheckBox chkAssignCheck = ((CheckBox)gvVesselSch.Rows[i].Cells[3].FindControl("chkVesselAssign"));
                                //This will check checkbox is checked or un-checked if checked then this will set 1 for checkCurrVal 
                                int checkCurrVal = 0;
                                if (chkAssignCheck.Checked == true)
                                {
                                    checkCurrVal = 1;
                                }


                                if (DocID == Convert.ToInt32(CurrDocID))
                                {
                                   // This will check for difference hfAssign HiddenField value is set while binding and if checkbox value changed then we will found difference it this and then only we will give call to SP ie. BAL function.
                                    if (Convert.ToInt32(hfAssign.Value.ToString()) != checkCurrVal)
                                    {
                                        HiddenField hdnVesselID = ((HiddenField)gvVesselSch.Rows[i].Cells[0].FindControl("hdnVesselID"));
                                        int VesselID = UDFLib.ConvertToInteger(hdnVesselID.Value);


                                        objInsp.FMS_Insert_AssignFormToVessel(DocID, VesselID, GetSessionUserID(), checkCurrVal);
                                        CheckCount++;
                                    }
                                }
                                else
                                {

                                    if (Convert.ToInt32(hfAssign.Value.ToString()) != checkCurrVal )//|| checkCurrVal==1
                                    {                                        
                                        HiddenField hdnVesselID = ((HiddenField)gvVesselSch.Rows[i].Cells[0].FindControl("hdnVesselID"));
                                        int VesselID = UDFLib.ConvertToInteger(hdnVesselID.Value);


                                        objInsp.FMS_Insert_AssignFormToVessel(DocID, VesselID, GetSessionUserID(), checkCurrVal);
                                        CheckCount++;
                                    }
                                    else if (checkCurrVal==1)//|| checkCurrVal==1
                                    {
                                        HiddenField hdnVesselID = ((HiddenField)gvVesselSch.Rows[i].Cells[0].FindControl("hdnVesselID"));
                                        int VesselID = UDFLib.ConvertToInteger(hdnVesselID.Value);


                                        objInsp.FMS_Insert_AssignFormToVessel(DocID, VesselID, GetSessionUserID(), checkCurrVal);
                                        CheckCount++;
                                    }
                                  
                                }

                            }

                         
                        }
                    }
                }

                if (CheckCount == 0)
                {
                    string jsAsgn = "alert('Please select atleast one vessel to which form to be assigned');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "jsAsgn", jsAsgn, true);
                }
                else
                {
                    //This added when un-ussigned then parent page will be refreshed.
                    string js2 = "parent.UpdatePage(); ";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "hidePopup", js2, true);
                }

            }

        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }

    }
    protected void gvVesselSch_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            CheckBox chk = (CheckBox)gvVesselSch.HeaderRow.Cells[4].FindControl("chkSchAllVessel");
            chk.Checked = false;
            int DocID = UDFLib.ConvertToInteger(e.CommandArgument.ToString().Split(',')[2]);
            int SchID = UDFLib.ConvertToInteger(e.CommandArgument.ToString().Split(',')[1]);
            int RowIndex = UDFLib.ConvertToInteger(e.CommandArgument.ToString().Split(',')[3]);



            Load_Schedule(DocID, SchID);


            pnlShedule.Visible = true;

        
            HighlightSelectedRow(RowIndex);
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }

    }


    protected void HighlightSelectedRow(int RowIndex)
    {
        try
        {
            for (int i = 0; i < gvVesselSch.Rows.Count; i++)
            {
                GridViewRow gvr = gvVesselSch.Rows[i];
                CheckBox chkSch = (CheckBox)gvr.Cells[4].FindControl("chkSch");


                if (RowIndex == i)
                {
                    //Apply Yellow color to selected Row
                    gvr.BackColor = ColorTranslator.FromHtml("#FFFFCC");
                    chkSch.Checked = true;
                }
                else
                {
                    //Apply White color to rest of rows
                    gvr.BackColor = ColorTranslator.FromHtml("#FFFFFF");
                    chkSch.Checked = false;
                }
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    protected void chkEOM_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            if (chkEOM.Checked == true)
            {
                txtDueDay.Enabled = false;
                txtDueDay.Text = "";
            }
            else
            {
                txtDueDay.Enabled = true;
                txtDueDay.Text = "";
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
}