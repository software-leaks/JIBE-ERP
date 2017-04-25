using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using SMS.Business.Infrastructure;
using System.Text;
public partial class Sys_parameters_SystemParameter : System.Web.UI.Page
{

    //UserRights_BL bal = new UserRights_BL();

    /* Create the object for accessing the services
     *  this object is mainly used for accessing the service routines
     *   */
    //IeLogService_INFClient objsys = Global.Client;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["System_parameter"] = "1";
            BLL_Infra_SysParamater objsys = new BLL_Infra_SysParamater();

            try
            {
                GetModules();
                // SysPar_BAL obj = new SysPar_BAL();

                ddlModuleTable.DataSource = objsys.GetModulesTable();
                ddlModuleTable.DataTextField = "name";
                ddlModuleTable.DataValueField = "name";
                ddlModuleTable.DataBind();
            }
            catch (Exception ex)
            {
                //ErrorLogHandler.WriteError(this.GetType().Name.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), ex);
            }

        }

        else
        {
            CallProperListUpdate(Request.Params.Get("__EVENTTARGET"));
        }

    }

    private void CallProperListUpdate(string id)
    {
        switch (id)
        {
            case "ctl00$MainContent$lsbModule":
                GetList(1);
                lsbModule.Focus();
                break;
            case "ctl00$MainContent$LsbLevel1":
                GetList(2);
                break;
            case "ctl00$MainContent$LsbLevel2":
                GetList(3);
                break;
            case "ctl00$MainContent$LsbLevel3":
                GetList(4);
                break;
            case "ctl00$MainContent$LsbLevel4":
                GetList(5);
                break;
            case "ctl00$MainContent$LsbLevel5":
                GetList(6);
                break;
            case "ctl00$MainContent$LsbLevel6":
                GetList(7);
                break;
            default:
                break;
        }


    }

    private void GetModules()
    {
        BLL_Infra_SysParamater objsys = new BLL_Infra_SysParamater();

        try
        {
            //SysPar_BAL obj = new SysPar_BAL();
            lsbModule.DataSource = objsys.GetModules(Int32.Parse(ddlModule.SelectedValue));
            lsbModule.DataTextField = "module";
            lsbModule.DataValueField = "tablename";
            lsbModule.DataBind();

            LsbLevel1.Items.Clear();
            LsbLevel2.Items.Clear();
            LsbLevel3.Items.Clear();
            LsbLevel4.Items.Clear();
            LsbLevel5.Items.Clear();
            LsbLevel6.Items.Clear();
        }
        catch (Exception ex)
        {
            // ErrorLogHandler.WriteError(this.GetType().Name.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), ex);
        }


    }

    private Control getPostBackControlName()
    {
        Control control = null;
        //first we will check the "__EVENTTARGET" because if post back made by       the controls
        //which used "_doPostBack" function also available in Request.Form collection.
        string ctrlname = Page.Request.Params["__EVENTTARGET"];
        if (ctrlname != null && ctrlname != String.Empty)
        {
            control = Page.FindControl(ctrlname);
        }
        // if __EVENTTARGET is null, the control is a button type and we need to
        // iterate over the form collection to find it
        else
        {
            string ctrlStr = String.Empty;
            Control c = null;
            foreach (string ctl in Page.Request.Form)
            {
                //handle ImageButton they having an additional "quasi-property" in their Id which identifies
                //mouse x and y coordinates
                if (ctl.EndsWith(".x") || ctl.EndsWith(".y"))
                {
                    ctrlStr = ctl.Substring(0, ctl.Length - 2);
                    c = Page.FindControl(ctrlStr);
                }
                else
                {
                    c = Page.FindControl(ctl);
                }
                if (c is System.Web.UI.WebControls.Button || c is System.Web.UI.WebControls.ImageButton)
                {
                    control = c;
                    break;
                }
            }
        }
        return control;
    }

    private ListBox GetSelectedListBox()
    {
        ListBox lb = null;
        ArrayList ar = new ArrayList();
        foreach (Control cl in tblPanel.Controls[0].Controls)
        {
            if (cl.GetType().FullName == "System.Web.UI.WebControls.ListBox")
                ar.Add(cl);
        }
        //ar.RemoveAt(0);
        ar.Reverse();

        foreach (object obj in ar)
        {
            ListBox ls = (ListBox)obj;
            if (ls.Items.Count > 0)
                if (ls.SelectedIndex != -1)
                {
                    if (ls.ID == "lsbModule")
                    {
                        txtParent.Text = "";
                        Session["addParentID"] = "";
                        Session["currentSelectedLB"] = "0";
                    }
                    else
                    {
                        txtParent.Text = ls.SelectedItem.Text;
                        Session["addParentID"] = ls.SelectedValue;
                        Session["currentSelectedLB"] = ls.ID.Substring(ls.ID.Length - 1);
                    }
                    lb = ls;
                    break;
                }
        }
        GC.Collect();
        return lb;

    }

    private void EnabledisableAllControl(bool flag)
    {
        if (flag)
        {
            lsbModule.Enabled = false;
            LsbLevel1.Enabled = false;
            LsbLevel2.Enabled = false;
            LsbLevel3.Enabled = false;
            LsbLevel4.Enabled = false;
            LsbLevel5.Enabled = false;
            LsbLevel6.Enabled = false;
            ddlModule.Enabled = false;
            ddlActive1.Enabled = false;
            ddlActive2.Enabled = false;
            ddlActive3.Enabled = false;
            ddlActive4.Enabled = false;
            ddlActive5.Enabled = false;
            ddlActive6.Enabled = false;
            btnAdd.Enabled = false;
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;
            btnExport.Enabled = false;
            btnAddModule.Enabled = false;


        }
        else
        {
            lsbModule.Enabled = true;
            LsbLevel1.Enabled = true;
            LsbLevel2.Enabled = true;
            LsbLevel3.Enabled = true;
            LsbLevel4.Enabled = true;
            LsbLevel5.Enabled = true;
            LsbLevel6.Enabled = true;
            ddlModule.Enabled = true;
            ddlActive1.Enabled = true;
            ddlActive2.Enabled = true;
            ddlActive3.Enabled = true;
            ddlActive4.Enabled = true;
            ddlActive5.Enabled = true;
            ddlActive6.Enabled = true;
            btnAdd.Enabled = true;
            btnEdit.Enabled = true;
            btnDelete.Enabled = true;
            btnExport.Enabled = true;
            btnAddModule.Enabled = true;

        }
    }

    private void GetList(int lstBoxNo)
    {

        BLL_Infra_SysParamater objsys = new BLL_Infra_SysParamater();

        try
        {
            //SysPar_BAL obj = new SysPar_BAL();
            switch (lstBoxNo)
            {
                case 1:

                    LsbLevel1.DataSource = objsys.GetListValues(lsbModule.SelectedValue, "", ddlActive1.SelectedValue);
                    LsbLevel1.DataTextField = "name";
                    LsbLevel1.DataValueField = "code";
                    LsbLevel1.DataBind();
                    LsbLevel1.Focus();
                    LsbLevel2.Items.Clear();
                    //LsbLevel2.DataSource = null;
                    //LsbLevel2.DataBind();
                    LsbLevel3.Items.Clear();
                    //LsbLevel3.DataSource = null;
                    //LsbLevel3.DataBind();
                    LsbLevel4.Items.Clear();
                    //LsbLevel4.DataSource = null;
                    //LsbLevel4.DataBind();
                    LsbLevel5.Items.Clear();
                    //LsbLevel5.DataSource = null;
                    //LsbLevel5.DataBind();
                    LsbLevel6.Items.Clear();
                    //LsbLevel6.DataSource = null;
                    //LsbLevel6.DataBind();
                    break;
                case 2:
                    LsbLevel2.DataSource = objsys.GetListValues(lsbModule.SelectedValue, LsbLevel1.SelectedValue, ddlActive2.SelectedValue);
                    LsbLevel2.DataTextField = "name";
                    LsbLevel2.DataValueField = "code";
                    LsbLevel2.DataBind();
                    LsbLevel2.Focus();
                    LsbLevel3.Items.Clear();
                    LsbLevel4.Items.Clear();
                    LsbLevel5.Items.Clear();
                    LsbLevel6.Items.Clear();
                    /*LsbLevel3.Items.Clear();
                    LsbLevel3.DataSource = null;
                    LsbLevel3.DataBind();
                    LsbLevel4.Items.Clear();
                    LsbLevel4.DataSource = null;
                    LsbLevel4.DataBind();
                    LsbLevel5.Items.Clear();
                    LsbLevel5.DataSource = null;
                    LsbLevel5.DataBind();
                    LsbLevel6.Items.Clear();
                    LsbLevel6.DataSource = null;
                    LsbLevel6.DataBind();*/
                    break;
                case 3:
                    LsbLevel3.DataSource = objsys.GetListValues(lsbModule.SelectedValue, LsbLevel2.SelectedValue, ddlActive3.SelectedValue);
                    LsbLevel3.DataTextField = "name";
                    LsbLevel3.DataValueField = "code";
                    LsbLevel3.DataBind();
                    LsbLevel3.Focus();
                    LsbLevel4.Items.Clear();
                    LsbLevel5.Items.Clear();
                    LsbLevel6.Items.Clear();
                    /*LsbLevel4.Items.Clear();
                    LsbLevel4.DataSource = null;
                    LsbLevel4.DataBind();
                    LsbLevel5.Items.Clear();
                    LsbLevel5.DataSource = null;
                    LsbLevel5.DataBind();
                    LsbLevel6.Items.Clear();
                    LsbLevel6.DataSource = null;
                    LsbLevel6.DataBind();*/
                    break;
                case 4:
                    LsbLevel4.DataSource = objsys.GetListValues(lsbModule.SelectedValue, LsbLevel3.SelectedValue, ddlActive4.SelectedValue);
                    LsbLevel4.DataTextField = "name";
                    LsbLevel4.DataValueField = "code";
                    LsbLevel4.DataBind();
                    LsbLevel4.Focus();
                    LsbLevel5.Items.Clear();
                    LsbLevel6.Items.Clear();
                    /*LsbLevel5.Items.Clear();
                    LsbLevel5.DataSource = null;
                    LsbLevel5.DataBind();
                    LsbLevel6.Items.Clear();
                    LsbLevel6.DataSource = null;
                    LsbLevel6.DataBind();*/
                    break;
                case 5:
                    LsbLevel5.DataSource = objsys.GetListValues(lsbModule.SelectedValue, LsbLevel4.SelectedValue, ddlActive5.SelectedValue);
                    LsbLevel5.DataTextField = "name";
                    LsbLevel5.DataValueField = "code";
                    LsbLevel5.DataBind();
                    LsbLevel5.Focus();
                    LsbLevel6.Items.Clear();
                    /*LsbLevel6.DataSource = null;
                    LsbLevel6.DataBind();*/
                    break;
                case 6:
                    LsbLevel6.DataSource = objsys.GetListValues(lsbModule.SelectedValue, LsbLevel5.SelectedValue, ddlActive6.SelectedValue);
                    LsbLevel6.DataTextField = "name";
                    LsbLevel6.DataValueField = "code";
                    LsbLevel6.Focus();
                    LsbLevel6.DataBind();

                    break;
                default:
                    break;

            }
        }
        catch (Exception ex)
        {
            //ErrorLogHandler.WriteError(this.GetType().Name.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), ex);
        }


    }

    protected void ddlModule_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetModules();
        //GetList(1);
        //lsbModule.Focus();

    }

    protected void ddlActive1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (lsbModule.Items.Count > 0 && lsbModule.SelectedIndex != -1)
            GetList(1);
    }

    protected void ddlActive2_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (LsbLevel1.Items.Count > 0 && LsbLevel1.SelectedIndex != -1)
            GetList(2);
    }

    protected void ddlActive3_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (LsbLevel2.Items.Count > 0 && LsbLevel2.SelectedIndex != -1)
            GetList(3);
    }

    protected void ddlActive4_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (LsbLevel3.Items.Count > 0 && LsbLevel3.SelectedIndex != -1)
            GetList(4);
    }

    protected void ddlActive5_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (LsbLevel4.Items.Count > 0 && LsbLevel4.SelectedIndex != -1)
            GetList(5);
    }

    protected void ddlActive6_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (LsbLevel5.Items.Count > 0 && LsbLevel5.SelectedIndex != -1)
            GetList(6);
    }

    protected void btnsaveRecords_Click(object sender, EventArgs e)
    {
        BLL_Infra_SysParamater objService = new BLL_Infra_SysParamater();

        try
        {
            if (btnAdd.Enabled == true)
            {
                int i = 0; bool blnExec = false;

                if (rbActiveYes.Checked)
                    i = 1;

                StringBuilder sbQuery = new StringBuilder();

                /* On the basis of the Module the entries will be inserted on Correspondence 'System parameter table' .*/

                switch (lsbModule.SelectedValue.Trim().ToUpper())
                {

                    case "PURC_LIB_SYSTEM_PARAMETERS":

                        if (txtParent.Text == "")
                            sbQuery.Append(" insert into " + lsbModule.SelectedValue + "(code,Parent_type,Short_Code,Description,Created_By,Date_Of_Created,Active_Status) ( select isnull(max(Code),0)+1 ,0,'" + txtName.Text + "','" + txtDescription.Text + "'," + Session["userid"].ToString() + ",GetDate()," + i.ToString() + " from " + lsbModule.SelectedValue + ")");
                        else
                            sbQuery.Append(" insert into " + lsbModule.SelectedValue + "(code,Parent_type,Short_Code,Description,Created_By,Date_Of_Created,Active_Status)  ( select isnull(max(Code),0)+1 ," + Session["addParentID"].ToString() + ",'" + txtName.Text + "','" + txtDescription.Text + "'," + Session["userid"].ToString() + ",GetDate()," + i.ToString() + " from " + lsbModule.SelectedValue + ")");


                        sbQuery.Append(@"   DECLARE @Code INT ,@PkCondition VARCHAR(400) ,@TableName VARCHAR(100),@VSlcount INT ,@VSLID VARCHAR(25)
                                    SELECT @Code = ISNULL(MAX(CODE),0) FROM " + lsbModule.SelectedValue + @"
                                    SET @PkCondition = 'CODE=''' + cast(@Code as varchar) + '''' 
                                    SET @TableName='" + lsbModule.SelectedValue + @"'
                                    EXEC SYNC_SP_DataSynch_MultiPK_DataLog @TableName, @PkCondition, 0
                                ");


                        blnExec = true;
                        break;

                    case "LIB_VESSELLIB_SYSTEMS_PARAMETERS":
                    case "FBM_LIB_SYSTEMS_PARAMETERS":
                    case "ACC_LIB_SYSTEMS_PARAMETERS":
                    case "TEC_LIB_SYSTEMS_PARAMETERS":
                    case "OPS_LIB_SYSTEMS_PARAMETERS":

                        if (txtParent.Text == "")
                            sbQuery.Append(" insert into " + lsbModule.SelectedValue + "(CODE,PARENT_CODE,NAME,DESCRIPTION,Created_By,DATE_OF_CREATION,ACTIVE_STATUS) ( select isnull(max(Code),0)+1 ,0,'" + txtName.Text + "','" + txtDescription.Text + "'," + Session["userid"].ToString() + ",GetDate()," + i.ToString() + " from " + lsbModule.SelectedValue + ")");
                        else
                            sbQuery.Append(" insert into " + lsbModule.SelectedValue + "(CODE,PARENT_CODE,NAME,DESCRIPTION,Created_By,DATE_OF_CREATION,ACTIVE_STATUS)  ( select isnull(max(Code),0)+1 ," + Session["addParentID"].ToString() + ",'" + txtName.Text + "','" + txtDescription.Text + "'," + Session["userid"].ToString() + ",GetDate()," + i.ToString() + " from " + lsbModule.SelectedValue + ")");

                        sbQuery.Append(@"   DECLARE @Code INT ,@PkCondition VARCHAR(400) ,@TableName VARCHAR(100),@VSlcount INT ,@VSLID VARCHAR(25)
                                    SELECT @Code = ISNULL(MAX(CODE),0) FROM " + lsbModule.SelectedValue + @"
                                    SET @PkCondition = 'CODE=''' + cast(@Code as varchar) + '''' 
                                    SET @TableName='" + lsbModule.SelectedValue + @"'
                                    EXEC SYNC_SP_DataSynch_MultiPK_DataLog @TableName, @PkCondition, 0
                                ");


                        blnExec = true;
                        break;


                    case "QMS_SYSTEMPARAMETERS":
                    case "SEP_TASK_SYSTEMPARAMETERS":

                        if (txtParent.Text == "")
                            sbQuery.Append(" insert into " + lsbModule.SelectedValue + "(Prarent_Code,Name,Description,Active_Status) values (0,'" + txtName.Text + "','" + txtDescription.Text + "'," + i.ToString() + ")");
                        else
                            sbQuery.Append(" insert into " + lsbModule.SelectedValue + "(Prarent_Code,Name,Description,Active_Status) values (" + Session["addParentID"].ToString() + ",'" + txtName.Text + "','" + txtDescription.Text + "'," + i.ToString() + ")");

                        sbQuery.Append(@"   DECLARE @Code INT ,@PkCondition VARCHAR(400) ,@TableName VARCHAR(100),@VSlcount INT ,@VSLID VARCHAR(25)
                                    SELECT @Code = ISNULL(MAX(ID),0) FROM " + lsbModule.SelectedValue + @"
                                    SET @PkCondition = 'ID=''' + cast(@Code as varchar) + '''' 
                                    SET @TableName='" + lsbModule.SelectedValue + @"'
                                    EXEC SYNC_SP_DataSynch_MultiPK_DataLog @TableName, @PkCondition, 0
                                ");


                        blnExec = true;
                        break;

                    default:

                        if (txtParent.Text == "")
                            sbQuery.Append(" insert into " + lsbModule.SelectedValue + "(code,Parent_type,Short_Code,Description,Created_By,Date_Of_Created,Active_Status) ( select isnull(max(Code),0)+1 ,0,'" + txtName.Text + "','" + txtDescription.Text + "'," + Session["userid"].ToString() + ",GetDate()," + i.ToString() + " from " + lsbModule.SelectedValue + ")");
                        else
                            sbQuery.Append(" insert into " + lsbModule.SelectedValue + "(code,Parent_type,Short_Code,Description,Created_By,Date_Of_Created,Active_Status)  ( select isnull(max(Code),0)+1 ," + Session["addParentID"].ToString() + ",'" + txtName.Text + "','" + txtDescription.Text + "'," + Session["userid"].ToString() + ",GetDate()," + i.ToString() + " from " + lsbModule.SelectedValue + ")");


                        sbQuery.Append(@"   DECLARE @Code INT ,@PkCondition VARCHAR(400) ,@TableName VARCHAR(100),@VSlcount INT ,@VSLID VARCHAR(25)
                                    SELECT @Code = ISNULL(MAX(CODE),0) FROM " + lsbModule.SelectedValue + @"
                                    SET @PkCondition = 'CODE=''' + cast(@Code as varchar) + '''' 
                                    SET @TableName='" + lsbModule.SelectedValue + @"'
                                    EXEC SYNC_SP_DataSynch_MultiPK_DataLog @TableName, @PkCondition, 0
                                ");

                        blnExec = true;
                        break;
                }


                if (blnExec)
                {

                    int result = objService.insertSysparameter(sbQuery.ToString());

                    if (txtParent.Text == "")
                    {
                        GetList(1);
                    }
                    else
                    {
                        GetList(Int32.Parse(Session["currentSelectedLB"].ToString()) + 1);
                    }
                    EnabledisableAllControl(false);
                    if (result == 1)
                        ScriptManager.RegisterClientScriptBlock(tblPanel, this.GetType(), "Success", "alert('System parameter Successfully added');", true);
                    else
                        ScriptManager.RegisterClientScriptBlock(tblPanel, this.GetType(), "Failed", "alert('An error occured while adding please try later');", true);
                }

            }
            else //if (btnEdit.Enabled == true)
            {
                //Listparameter lb = new Listparameter();
                //if (rbActiveYes.Checked == true)
                //    lb.Active = 1;
                //else
                //    lb.Active = 0;
                //ListBox ls = GetSelectedListBox();
                //lb.ChildId = Int32.Parse(ls.SelectedValue);
                //lb.Description = txtDescription.Text;
                //lb.Name = txtName.Text;
                //Session["upObj"] = lb;
                //Session["ParentTableName"] = lsbModule.SelectedValue;
                ////ScriptManager.RegisterClientScriptBlock(tblPanel, this.GetType(), "alert", "alert('Step2: Please select Parent system parameter from  the child');", true);
                //EnabledisableAllControl(false);
                //btnAdd.Enabled = false;
                //btnDelete.Enabled = false;
                //btnExport.Enabled = false;
                //btnAddModule.Enabled = false;
                //btnEdit.Enabled = true;


                int active_status = 1;

                BLL_Infra_SysParamater objsys = new BLL_Infra_SysParamater();
                if (rbActiveYes.Checked == true)
                    active_status = 1;
                else
                    active_status = 0;


                int result = objsys.updateSysParam(Convert.ToInt32(txtCode.Text), txtName.Text, Convert.ToInt32(txtParentType.Text), txtDescription.Text, active_status, lsbModule.SelectedValue, Int32.Parse(Session["userid"].ToString()));
                Session["System_parameter"] = "1";

                GetList(Int32.Parse(Session["currentSelectedLB"].ToString()));

                //GetList(Int32.Parse(Session["currentSelectedLB"].ToString()) + 1);


                EnabledisableAllControl(false);
                btnAdd.Enabled = true;
                btnDelete.Enabled = true;
                btnExport.Enabled = true;
                btnAddModule.Enabled = true;
                btnEdit.Enabled = true;


                if (result == 1)
                    ScriptManager.RegisterClientScriptBlock(tblPanel, this.GetType(), "Success", "alert('System Parameter Successfully Updated');", true);
                else
                    ScriptManager.RegisterClientScriptBlock(tblPanel, this.GetType(), "Failed", "alert('An error occured while adding please try later');", true);

                return;


            }

            txtModule.Text = "";
            txtName.Text = "";
            txtParent.Text = "";
            txtDescription.Text = "";
        }
        catch (Exception ex)
        {
            //ErrorLogHandler.WriteError(this.GetType().Name.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), ex);
        }


    }

    protected void btncancel_Click(object sender, EventArgs e)
    {
        Session["System_parameter"] = "1";
        EnabledisableAllControl(false);
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {


        txtName.Text = "";
        txtDescription.Text = "";

        ListBox ls = GetSelectedListBox();
        if (ls == null)
        {
            ScriptManager.RegisterClientScriptBlock(tblPanel, this.GetType(), "alert", "alert('Please select a system parameter from listboxes');", true);
            return;
        }
        if (ls.ID == "LsbLevel6")
        {
            ScriptManager.RegisterClientScriptBlock(tblPanel, this.GetType(), "alert", "alert('Maximum level Reached no further addition is possible');", true);
            return;
        }
        if (lsbModule.ID == ls.ID)
        {
            txtParent.Text = "";
            txtParent.Visible = false;
        }
        else
        {
            txtParent.Visible = true;
            txtParent.Enabled = false;
        }
        txtModule.Visible = false;
        EnabledisableAllControl(true);
        btnAdd.Enabled = true;
        ScriptManager.RegisterClientScriptBlock(tblPanel, this.GetType(), "showDialog", "showDialog();", true);


    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {

        BLL_Infra_SysParamater objsys = new BLL_Infra_SysParamater();

        try
        {
            if (Session["System_parameter"].ToString() == "1")
            {
                ListBox ls = GetSelectedListBox();
                if (ls == null)
                {
                    ScriptManager.RegisterClientScriptBlock(tblPanel, this.GetType(), "alert", "alert('Step1: select the Child System parameter press Update Button modify name and description');", true);
                    return;
                }
                if (lsbModule.ID == ls.ID)
                {
                    txtParent.Text = "";
                    txtParent.Visible = false;
                }
                else
                {
                    txtParent.Visible = true;
                }
                //SysPar_BAL obj = new SysPar_BAL();
                DataSet ds = objsys.GetDetailOfSystemParameter(Int32.Parse(ls.SelectedValue), lsbModule.SelectedValue);

                switch (lsbModule.SelectedValue.Trim().ToUpper())
                {

                    case "PURC_LIB_SYSTEM_PARAMETERS":

                        txtCode.Text = ds.Tables[0].Rows[0]["Code"].ToString();

                        if (ds.Tables[0].Rows[0]["Parent_Code"].ToString() != "")
                            txtParentType.Text = ds.Tables[0].Rows[0]["Parent_Code"].ToString();
                        else
                            txtParentType.Text = "0";


                        txtName.Text = ds.Tables[0].Rows[0]["name"].ToString();
                        txtDescription.Text = ds.Tables[0].Rows[0]["description"].ToString();
                        if (ds.Tables[0].Rows[0]["Active_Status"].ToString().ToLower() == "1")
                            rbActiveYes.Checked = true;
                        else
                            RbActiveNo.Checked = false;

                        break;

                    case "LIB_VESSELLIB_SYSTEMS_PARAMETERS":
                    case "FBM_LIB_SYSTEMS_PARAMETERS":
                    case "ACC_LIB_SYSTEMS_PARAMETERS":
                    case "TEC_LIB_SYSTEMS_PARAMETERS":

                        txtCode.Text = ds.Tables[0].Rows[0]["Code"].ToString();

                        if (ds.Tables[0].Rows[0]["Parent_Code"].ToString() != "")
                            txtParentType.Text = ds.Tables[0].Rows[0]["Parent_Code"].ToString();
                        else
                            txtParentType.Text = "0";


                        txtName.Text = ds.Tables[0].Rows[0]["name"].ToString();

                        txtDescription.Text = ds.Tables[0].Rows[0]["description"].ToString();
                        if (ds.Tables[0].Rows[0]["Active_Status"].ToString().ToLower() == "1")
                            rbActiveYes.Checked = true;
                        else
                            RbActiveNo.Checked = false;

                        break;

                    case "QMS_SYSTEMPARAMETERS":
                    case "SEP_TASK_SYSTEMPARAMETERS":

                        txtCode.Text = ds.Tables[0].Rows[0]["Code"].ToString();

                        if (ds.Tables[0].Rows[0]["Parent_Code"].ToString() != "")
                            txtParentType.Text = ds.Tables[0].Rows[0]["Parent_Code"].ToString();
                        else
                            txtParentType.Text = "0";


                        txtName.Text = ds.Tables[0].Rows[0]["name"].ToString();
                        txtDescription.Text = ds.Tables[0].Rows[0]["description"].ToString();
                        if (ds.Tables[0].Rows[0]["Active_Status"].ToString().ToLower() == "1")
                            rbActiveYes.Checked = true;
                        else
                            RbActiveNo.Checked = false;

                        break;

                    default:

                        txtCode.Text = ds.Tables[0].Rows[0]["Code"].ToString();

                        if (ds.Tables[0].Rows[0]["Parent_Code"].ToString() != "")
                            txtParentType.Text = ds.Tables[0].Rows[0]["Parent_Code"].ToString();
                        else
                            txtParentType.Text = "0";


                        txtName.Text = ds.Tables[0].Rows[0]["name"].ToString();
                        txtDescription.Text = ds.Tables[0].Rows[0]["description"].ToString();
                        if (ds.Tables[0].Rows[0]["Active_Status"].ToString().ToLower() == "1")
                            rbActiveYes.Checked = true;
                        else
                            RbActiveNo.Checked = false;
                        break;
                }

                txtParent.Visible = false;
                txtModule.Visible = false;
                EnabledisableAllControl(true);
                btnEdit.Enabled = false;
                Session["System_parameter"] = "2";
                ScriptManager.RegisterClientScriptBlock(tblPanel, this.GetType(), "showDialog", "showDialog();", true);

                ViewState["OperationMode"] = "Edit";


                return;

            }


            //if (Session["System_parameter"].ToString() == "2")
            //{

            //    Listparameter ls = (Listparameter)Session["upObj"];
            //    ListBox lb = GetSelectedListBox();
            //    if (lb == null)
            //        return;
            //    if (Session["ParentTableName"].ToString() != lsbModule.SelectedValue)
            //    {
            //        ScriptManager.RegisterClientScriptBlock(tblPanel, this.GetType(), "alert", "alert('Parent module of System Parameter cannot be changed');", true);
            //        Session["System_parameter"] = "1";
            //        btnAdd.Enabled = true;
            //        btnExport.Enabled = true;
            //        btnDelete.Enabled = true;
            //        btnAddModule.Enabled = true;
            //        return;
            //    }
            //    int num;
            //    bool isnumeric = Int32.TryParse(lb.SelectedValue, out num);
            //    if (isnumeric)
            //        ls.Parent = Int32.Parse(lb.SelectedValue);
            //    else
            //        ls.Parent = -1;
            //    //SysPar_BAL obj = new SysPar_BAL();
            //    int result = objsys.updateSysParam(ls.ChildId, ls.Name, ls.Parent, ls.Description, ls.Active, lsbModule.SelectedValue, Int32.Parse(Session["userid"].ToString()));
            //    Session["System_parameter"] = "1";
            //    //GetList(Int32.Parse(Session["currentSelectedLB"].ToString()));
            //    GetList(Int32.Parse(Session["currentSelectedLB"].ToString()) + 1);
            //    btnAdd.Enabled = true;
            //    btnExport.Enabled = true;
            //    btnDelete.Enabled = true;
            //    btnAddModule.Enabled = true;
            //    if (result == 1)
            //        ScriptManager.RegisterClientScriptBlock(tblPanel, this.GetType(), "Success", "alert('System Parameter Successfully Updated');", true);
            //    else
            //        ScriptManager.RegisterClientScriptBlock(tblPanel, this.GetType(), "Failed", "alert('An error occured while adding please try later');", true);

            //    return;

            //}


        }
        catch (Exception ex)
        {
            //ErrorLogHandler.WriteError(this.GetType().Name.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), ex);
        }


    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        BLL_Infra_SysParamater objsys = new BLL_Infra_SysParamater();

        try
        {
            ListBox lb = GetSelectedListBox();
            if (lb == null)
            {
                ScriptManager.RegisterClientScriptBlock(tblPanel, this.GetType(), "alert", "alert('Please select a system parameter from listboxes');", true);
                return;
            }
            if (lb.ID == lsbModule.ID)
            {
                ScriptManager.RegisterClientScriptBlock(tblPanel, this.GetType(), "alert", "alert('Modules can be only deleted through Module edit screen');", true);
                return;
            }

            // string query = "update " + lsbModule.SelectedValue + " set Active_Status = 0 where Code =" + lb.SelectedValue + ";";
            // int result = objsys.deleteSysparameter(query);

            StringBuilder sbquery = new StringBuilder();

            sbquery.Append("update " + lsbModule.SelectedValue + " set Active_Status = 0 where Code =" + lb.SelectedValue + " ");
            sbquery.Append(@"  DECLARE @Code INT ,@PkCondition VARCHAR(400) ,@TableName VARCHAR(100),@VSlcount INT ,@VSLID VARCHAR(25)  
                                    SET @Code=" + lb.SelectedValue + @"  
                                    SET @PkCondition = 'CODE=''' + cast(@Code as varchar) + '''' 
                                    SET @TableName='" + lsbModule.SelectedValue + @"'
                                    EXEC SYNC_SP_DataSynch_MultiPK_DataLog @TableName, @PkCondition, 0
                                ");

            int result = objsys.deleteSysparameter(sbquery.ToString());

            GetList(Int32.Parse(Session["currentSelectedLB"].ToString()));
            if (result == 1)
                ScriptManager.RegisterClientScriptBlock(tblPanel, this.GetType(), "Success", "alert('System parameter Successfully Deleted');", true);
            else
                ScriptManager.RegisterClientScriptBlock(tblPanel, this.GetType(), "Failed", "alert('An error occured while Deleting please try later');", true);
        }
        catch (Exception ex)
        {
            //ErrorLogHandler.WriteError(this.GetType().Name.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), ex);
        }


    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        int i = 0;
        foreach (Control cl in tblPanel.Controls[0].Controls)
        {
            if (cl.GetType().FullName == "System.Web.UI.WebControls.ListBox")
            {
                ListBox lb = (ListBox)cl;
                if (lb.Items.Count > 0)
                    i++;
            }
        }
        if (i == 0)
        {
            return;
        }
        DataTable dt = new DataTable();
        for (int j = 0; j < i; j++)
        {
            DataColumn dc;
            if (j == 0)
                dc = new DataColumn("Module");
            else
                dc = new DataColumn("Level" + j.ToString());
            dt.Columns.Add(dc);
        }
        DataRow dr = dt.NewRow();
        int k = 0; ListBox temp = null;
        if (k < i)
        {
            if (lsbModule.SelectedIndex != -1)
                dr[0] = lsbModule.SelectedItem.Text;
            else
                dr[0] = lsbModule.Items[0].Text;
            temp = lsbModule;
            k++;
        }
        if (k < i)
        {
            if (LsbLevel1.SelectedIndex != -1)
                dr[1] = LsbLevel1.SelectedItem.Text;
            else
                dr[1] = LsbLevel1.Items[0].Text;
            temp = LsbLevel1;
            k++;
        }
        if (k < i)
        {
            if (LsbLevel2.SelectedIndex != -1)
                dr[2] = LsbLevel2.SelectedItem.Text;
            else
                dr[2] = LsbLevel2.Items[0].Text;
            temp = LsbLevel2;
            k++;
        }
        if (k < i)
        {
            if (LsbLevel3.SelectedIndex != -1)
                dr[3] = LsbLevel3.SelectedItem.Text;
            else
                dr[3] = LsbLevel3.Items[0].Text;
            temp = LsbLevel3;
            k++;
        }
        if (k < i)
        {
            if (LsbLevel4.SelectedIndex != -1)
                dr[4] = LsbLevel4.SelectedItem.Text;
            else
                dr[4] = LsbLevel4.Items[0].Text;
            temp = LsbLevel4;
            k++;
        }
        if (k < i)
        {
            if (LsbLevel5.SelectedIndex != -1)
                dr[5] = LsbLevel5.SelectedItem.Text;
            else
                dr[5] = LsbLevel5.Items[0].Text;
            temp = LsbLevel5;
            k++;
        }
        if (k < i)
        {
            if (LsbLevel6.SelectedIndex != -1)
                dr[6] = LsbLevel6.SelectedItem.Text;
            else
                dr[6] = LsbLevel6.Items[0].Text;
            temp = LsbLevel6;
            k++;
        }
        dt.Rows.Add(dr);
        bool flag = false;
        if (temp.SelectedIndex == -1)
            flag = true;
        string compare = dt.Rows[0][i - 1].ToString();
        foreach (ListItem item in temp.Items)
        {
            if (flag)
            {
                flag = false;
                continue;
            }
            if (compare == item.Text)
                continue;
            dr = dt.NewRow();
            dr[i - 1] = item.Text;
            dt.Rows.Add(dr);
        }
        ExportToSpreadsheet(dt, "activityLog");



    }

    protected void ExportToSpreadsheet(DataTable table, string name)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel";
        Response.Charset = "";
        this.EnableViewState = false;
        System.IO.StringWriter oStringWriter = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter oHtmlTextWriter = new System.Web.UI.HtmlTextWriter(oStringWriter);
        DataGrid dg = new DataGrid();
        dg.DataSource = table;
        dg.DataBind();
        dg.RenderControl(oHtmlTextWriter);
        Response.Write(oStringWriter.ToString());
        Response.End();
        GC.Collect();
    }

    protected void btnAddModule_Click(object sender, EventArgs e)
    {
        txtModulename.Text = "";


        EnabledisableAllControl(true);

        ScriptManager.RegisterClientScriptBlock(tblPanel, this.GetType(), "showModuleDialog", "showModuleDialog();", true);
    }

    protected void btnModCancel_Click(object sender, EventArgs e)
    {
        txtModulename.Text = "";
        Session["System_parameter"] = "1";
        EnabledisableAllControl(false);

    }

    protected void btnModOK_Click(object sender, EventArgs e)
    {
        //SysPar_BAL obj = new SysPar_BAL();
        BLL_Infra_SysParamater objService = new BLL_Infra_SysParamater();
        {
            try
            {
                int status = 0;
                if (mActiveYes.Checked)
                    status = 1;
                int stat = objService.insertSysModule(txtModulename.Text, ddlModuleTable.SelectedValue, Int32.Parse(Session["userid"].ToString()), status);
                EnabledisableAllControl(false);
                GetModules();
                if (stat == -1)
                    ScriptManager.RegisterClientScriptBlock(tblPanel, this.GetType(), "Success", "alert('Module Successfully added');", true);
                else
                    ScriptManager.RegisterClientScriptBlock(tblPanel, this.GetType(), "Failed", "alert('An error occured while adding please try later');", true);

            }
            catch (Exception ex)
            {
                // ErrorLogHandler.WriteError(this.GetType().Name.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), ex);
            }

        }

    }


    #region Search functionality

    protected void imgModuleSearch_Click(object sender, EventArgs e)
    {

        BLL_Infra_SysParamater objsys = new BLL_Infra_SysParamater();

        DataSet ds = objsys.GetModules(Int32.Parse(ddlModule.SelectedValue));

        if (txtSearchModule.Text != "")
        {
            ds.Tables[0].DefaultView.RowFilter = "module Like '%" + txtSearchModule.Text.Trim() + "%'";
            lsbModule.DataSource = ds.Tables[0].DefaultView.ToTable();
        }
        else
        {
            lsbModule.DataSource = ds.Tables[0];
        }

        lsbModule.DataTextField = "module";
        lsbModule.DataValueField = "tablename";
        lsbModule.DataBind();

        LsbLevel1.Items.Clear();
        LsbLevel2.Items.Clear();
        LsbLevel3.Items.Clear();
        LsbLevel4.Items.Clear();
        LsbLevel5.Items.Clear();
        LsbLevel6.Items.Clear();

    }


    protected void imgLevel1Search_Click(object sender, EventArgs e)
    {

        BLL_Infra_SysParamater objsys = new BLL_Infra_SysParamater();

        DataSet ds = objsys.GetListValues(lsbModule.SelectedValue, "", ddlActive1.SelectedValue);

        if (txtSearchLevel1.Text != "")
        {
            ds.Tables[0].DefaultView.RowFilter = "name Like '%" + txtSearchLevel1.Text.Trim() + "%'";
            LsbLevel1.DataSource = ds.Tables[0].DefaultView.ToTable();
        }
        else
        {
            LsbLevel1.DataSource = ds.Tables[0];
        }

        LsbLevel1.DataTextField = "name";
        LsbLevel1.DataValueField = "code";
        LsbLevel1.DataBind();
        LsbLevel1.Focus();



      
        LsbLevel2.Items.Clear();
        LsbLevel3.Items.Clear();
        LsbLevel4.Items.Clear();
        LsbLevel5.Items.Clear();
        LsbLevel6.Items.Clear();





    }
    protected void imgLevel2Search_Click(object sender, EventArgs e)
    {

        BLL_Infra_SysParamater objsys = new BLL_Infra_SysParamater();
        DataSet ds = objsys.GetListValues(lsbModule.SelectedValue, LsbLevel1.SelectedValue, ddlActive2.SelectedValue);


        if (txtSearchLevel2.Text != "")
        {
            ds.Tables[0].DefaultView.RowFilter = "name Like '%" + txtSearchLevel2.Text.Trim() + "%'";
            LsbLevel2.DataSource = ds.Tables[0].DefaultView.ToTable();
        }
        else
        {
            LsbLevel2.DataSource = ds.Tables[0];
        }

        LsbLevel2.DataTextField = "name";
        LsbLevel2.DataValueField = "code";
        LsbLevel2.DataBind();
        LsbLevel2.Focus();

 
        LsbLevel3.Items.Clear();
        LsbLevel4.Items.Clear();
        LsbLevel5.Items.Clear();
        LsbLevel6.Items.Clear();


    }

    protected void imgLevel3Search_Click(object sender, EventArgs e)
    {

        BLL_Infra_SysParamater objsys = new BLL_Infra_SysParamater();


        DataSet ds = objsys.GetListValues(lsbModule.SelectedValue, LsbLevel2.SelectedValue, ddlActive3.SelectedValue);

        if (txtSearchLevel3.Text != "")
        {
            ds.Tables[0].DefaultView.RowFilter = "name Like '%" + txtSearchLevel3.Text.Trim() + "%'";
            LsbLevel3.DataSource = ds.Tables[0].DefaultView.ToTable();
        }
        else
        {
            LsbLevel3.DataSource = ds.Tables[0];
        }

        LsbLevel3.DataTextField = "name";
        LsbLevel3.DataValueField = "code";
        LsbLevel3.DataBind();
        LsbLevel3.Focus();

      
        LsbLevel4.Items.Clear();
        LsbLevel5.Items.Clear();
        LsbLevel6.Items.Clear();

    }

    protected void imgLevel4Search_Click(object sender, EventArgs e)
    {

        BLL_Infra_SysParamater objsys = new BLL_Infra_SysParamater();

        DataSet ds = objsys.GetListValues(lsbModule.SelectedValue, LsbLevel3.SelectedValue, ddlActive4.SelectedValue);

        if (txtSearchLevel4.Text != "")
        {
            ds.Tables[0].DefaultView.RowFilter = "name Like '%" + txtSearchLevel4.Text.Trim() + "%'";
            LsbLevel4.DataSource = ds.Tables[0].DefaultView.ToTable();
        }
        else
        {
            LsbLevel4.DataSource = ds.Tables[0];
        }

        LsbLevel4.DataTextField = "name";
        LsbLevel4.DataValueField = "code";
        LsbLevel4.DataBind();
        LsbLevel4.Focus();


   
        LsbLevel5.Items.Clear();
        LsbLevel6.Items.Clear();


    }

    protected void imgLevel5Search_Click(object sender, EventArgs e)
    {

        BLL_Infra_SysParamater objsys = new BLL_Infra_SysParamater();

        DataSet ds = objsys.GetListValues(lsbModule.SelectedValue, LsbLevel4.SelectedValue, ddlActive5.SelectedValue);

        if (txtSearchLevel5.Text != "")
        {
            ds.Tables[0].DefaultView.RowFilter = "name Like '%" + txtSearchLevel5.Text.Trim() + "%'";
            LsbLevel5.DataSource = ds.Tables[0].DefaultView.ToTable();
        }
        else
        {
            LsbLevel5.DataSource = ds.Tables[0];
        }

        LsbLevel5.DataTextField = "name";
        LsbLevel5.DataValueField = "code";
        LsbLevel5.DataBind();
        LsbLevel5.Focus();

        LsbLevel6.Items.Clear();




    }

    protected void imgLevel6Search_Click(object sender, EventArgs e)
    {

        BLL_Infra_SysParamater objsys = new BLL_Infra_SysParamater();

        DataSet ds = objsys.GetListValues(lsbModule.SelectedValue, LsbLevel5.SelectedValue, ddlActive6.SelectedValue);

        if (txtSearchLevel6.Text != "")
        {
            ds.Tables[0].DefaultView.RowFilter = "name Like '%" + txtSearchLevel6.Text.Trim() + "%'";
            LsbLevel6.DataSource = ds.Tables[0].DefaultView.ToTable();
        }
        else
        {
            LsbLevel6.DataSource = ds.Tables[0];
        }

        LsbLevel6.DataTextField = "name";
        LsbLevel6.DataValueField = "code";
        LsbLevel6.DataBind();
        LsbLevel6.Focus();


    }
     

    #endregion


}


[Serializable]
class Listparameter
{
    public Listparameter()
    {
        parent = 0;
        childId = 0;

        description = null;
        active = 1;
        name = "";

    }

    public Listparameter(int Parent, int NameId, string Name, string Description, int Active)
    {
        parent = Parent;
        childId = ChildId;
        active = Active;
        description = Description;
        name = Name;

    }

    private int parent;
    private int active;
    private string name;

    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    public int Active
    {
        get { return active; }
        set { active = value; }
    }

    public int Parent
    {
        get { return parent; }
        set { parent = value; }
    }

    private int childId;

    public int ChildId
    {
        get { return childId; }
        set { childId = value; }
    }

    private string description;

    public string Description
    {
        get { return description; }
        set { description = value; }
    }

}
