using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Infrastructure;
using System.Data;
using System.ComponentModel;
using System.Globalization;
using SMS.Business.PURC;
using SMS.Business.Crew;
using SMS.Business.Technical;
using SMS.Properties;
using System.IO;
using AjaxControlToolkit4;
using SMS.Business.PMS;


public partial class Technical_PMS_PMSAddAdhocJob : System.Web.UI.Page
{
    IFormatProvider iFormatProvider = CultureInfo.GetCultureInfo("en-GB");
    BLL_Tec_Worklist objBLL = new BLL_Tec_Worklist();
    BLL_Infra_VesselLib objBLLVessel = new BLL_Infra_VesselLib();
    BLL_Infra_UserCredentials objBLLUser = new BLL_Infra_UserCredentials();
    BLL_Crew_CrewDetails objBLLCrew = new BLL_Crew_CrewDetails();
    BLL_PURC_Purchase objBLLPurc = new BLL_PURC_Purchase();
    BLL_PMS_Job_Status objjob = new BLL_PMS_Job_Status();


    UserAccess objUA = new UserAccess();
    int isFileupload = 0;
    string JobHistoryID = "";
    string RAMandatory = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!AjaxFileUpload1.IsInFileUploadPostBack)
        {

            if (Session["USERID"] == null)
                Response.Redirect("~/account/login.aspx");
            UserAccessValidation();
            if (!IsPostBack == true)
            {

                ViewState["Job_History_Id"] = Request.QueryString["Job_History_Id"];
                ViewState["VESSEL_ID"] = Request.QueryString["VESSEL_ID"];
                ViewState["OFFICE_ID"] = Request.QueryString["OFFICE_ID"];
                ViewState["Job_Status"] = Request.QueryString["Job_Status"];
                ViewState["JOB_ID"] = Request.QueryString["JOB_ID"];
                ViewState["OD_DAYS"] = "";
                if (Request.QueryString["OD_DAYS"] != null)
                {
                    ViewState["OD_DAYS"] = Request.QueryString["OD_DAYS"];
                }

                Session["Non_PMS_Job_History_Id"] = "";
                Session["Non_PMS_Office_ID"] = "";
                Session["Non_PMS_VESSEL_ID"] = "";

                Session["Non_PMS_Job_History_Id"] = Request.QueryString["Job_History_Id"];
                Session["Non_PMS_Office_ID"] = Request.QueryString["OFFICE_ID"];
                Session["Non_PMS_VESSEL_ID"] = Request.QueryString["VESSEL_ID"];


                BindJobDoneHistoryList();
                BindRequisitionList();
                BindJobeDetailsMain();
                //ViewState["IsRAMandatory"] = Request.QueryString["IsRAMandatory"];

                //Added by reshma RA :To get job history id
                if (Request.QueryString["JOB_HISTORY_ID"]!=null)
                { 
                    JobHistoryID = Request.QueryString["JOB_HISTORY_ID"]; 
                }
               
                BindRAFormList(UDFLib.ConvertToInteger(JobHistoryID));

                if (Request.QueryString["IsRAMandatory"] != null)
                {
                    RAMandatory = Request.QueryString["IsRAMandatory"];

                    if (RAMandatory == "YES")
                    {
                       
                        rbtMRA.Items[0].Selected = true;
                    }
                    else
                    {
                        rbtMRA.Items[1].Selected = true;
                    }
                }


                if (!string.IsNullOrEmpty(Convert.ToString(ViewState["JOB_ID"])))
                {
                    if (int.Parse(Convert.ToString(ViewState["JOB_ID"])) > 0)
                        btnSave.Enabled = false;
                    else
                        btnSave.Enabled = true;
                }
                else

                    btnSave.Enabled = true;


            }

        }

        string scrRegGra = "RegisterGralerry();SetSafetyCalibration();";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "scrRegGra", scrRegGra, true);


    }


    protected void BindPSCSIRE()
    {
        DataSet ds = objBLL.TEC_WL_Get_ActivePSCSIRE();
        if (ds.Tables.Count > 0)
        {


            ddlPSCSIRE.DataSource = ds.Tables[0].DefaultView;
            ddlPSCSIRE.DataTextField = "PSC_SIRE";
            ddlPSCSIRE.DataValueField = "ID";
            ddlPSCSIRE.DataBind();
            ddlPSCSIRE.Items.Insert(0, "--SELECT--");
            ddlPSCSIRE.SelectedIndex = 0;

        }
    }
    protected void BindCategory()
    {
        DataSet ds = objBLL.TEC_Get_ActiveCategory();
        DataTable dt = new DataTable();
        dt.Columns.Add("ID");
        dt.Columns.Add("Category_Name");
        dt.Columns.Add("Category_Type");
        dt.Columns.Add("Active_Status");
        DataRow[] dr = ds.Tables[0].Select(" Category_Type= 'Primary'");
        for (int i = 0; i < dr.Length; i++)
        {
            dt.Rows.Add(dr[i].ItemArray[0].ToString(), dr[i].ItemArray[1].ToString(), dr[i].ItemArray[2].ToString(), dr[i].ItemArray[3].ToString());
        }

        ddlPrimary.DataSource = dt;
        ddlPrimary.DataTextField = "Category_Name";
        ddlPrimary.DataValueField = "ID";
        ddlPrimary.DataBind();
        dt.Rows.Clear();
        ddlPrimary.Items.Insert(0, "--SELECT--");
        DataRow[] dr1 = ds.Tables[0].Select(" Category_Type= 'Secondary'");
        for (int i = 0; i < dr1.Length; i++)
        {
            dt.Rows.Add(dr1[i].ItemArray[0].ToString(), dr1[i].ItemArray[1].ToString(), dr1[i].ItemArray[2].ToString(), dr1[i].ItemArray[3].ToString());
        }

        ddlSecondary.DataSource = dt;
        ddlSecondary.DataTextField = "Category_Name";
        ddlSecondary.DataValueField = "ID";
        ddlSecondary.DataBind();
        ddlSecondary.Items.Insert(0, "--SELECT--");
    }

    public void BindJobeDetailsMain()
    {

        BindPmsJobAttachment();
        Load_FleetList();
        Load_VesselList();
        BindPIC();
        BindDeptInOffice();
        BindDeptOnShip();
        BindAssigner();

        BindJobPriority();
        Bindfunction();
        BindCategory();
        BindPSCSIRE();
        BindUnit();
        BindEffect();
        lblODDays.Text = ViewState["OD_DAYS"].ToString();
        txtFollowupDate.Text = DateTime.Today.ToString("dd/MM/yyyy");

        txtRaisedOn.Text = DateTime.Today.ToString("dd/MM/yyyy");

        rdoDeferToDrydock.Items[1].Selected = true;
        if (objBLL.IsVessel(UDFLib.ConvertToInteger(ViewState["VESSEL_ID"])) == 1)
            ddlPIC.Enabled = true;
        else
            ddlPIC.Enabled = true;


        if (ViewState["Job_History_Id"] == null)
        {
            //-- Add new Job
            divAttandRemarks.Visible = false;
            divPmsJobDetails.Visible = false;
            txtJobStatus.Text = "PENDING";
            txtJobType.Text = "NON PMS JOB";
            btnRework.Enabled = false;
            btnVerify.Enabled = false;
            ddlPriority.SelectedValue = "1";
        }
        else
        {
            divAttandRemarks.Visible = true;
            divPmsJobDetails.Visible = true;
            BindJobDetails();
        }


    }

    protected void BindPmsJobAttachment()
    {

        BLL_PMS_Library_Jobs objjobs = new BLL_PMS_Library_Jobs();
        DataTable dt = objjobs.LibraryGetJobInstructionAttachment(UDFLib.ConvertToInteger(ViewState["Vessel_ID"]), UDFLib.ConvertToInteger(ViewState["JOB_ID"]));

        gvPMSJobAttachment.DataSource = dt;
        gvPMSJobAttachment.DataBind();

    }

    /// <summary>
    /// To bind a function on selection of vessel.
    /// </summary>
    public void Bindfunction()
    {
        try
        {
            reset_ddl();

            DataTable dt = objBLLPurc.LibraryGetSystemParameterList("115", "");
            ddlFunction.Items.Clear();
            ddlFunction.DataSource = dt;
            ddlFunction.DataValueField = "CODE";
            ddlFunction.DataTextField = "DESCRIPTION";
            ddlFunction.DataBind();
            ddlFunction.Items.Insert(0, new ListItem("-SELECT-", "0"));
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    /// <summary>
    /// To bind system to dropdown on selection of function 
    /// </summary>
    public void BindSystem_Location()
    {
        try
        {
            DataTable dt = objBLLPurc.GET_SYSTEM_LOCATION(UDFLib.ConvertToInteger(ddlFunction.SelectedValue), UDFLib.ConvertToInteger(ViewState["VESSEL_ID"]));

            ddlSysLocation.Items.Clear();
            ddlSysLocation.DataSource = dt;
            ddlSysLocation.DataValueField = "AssginLocationID";
            ddlSysLocation.DataTextField = "LocationName";
            ddlSysLocation.DataBind();
            ddlSysLocation.Items.Insert(0, new ListItem("-SELECT-", "0"));
            if (ddlSysLocation.SelectedIndex == -1)
                ddlSysLocation.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }

    }

    /// <summary>
    /// To bind sub-system to dropdown on selection of system.
    /// </summary>
    public void BindSubSystem_Location()
    {
        try
        {
            if (ddlSysLocation.SelectedValue != "0")
            {
                DataTable dt = objBLLPurc.GET_SUBSYTEMSYSTEM_LOCATION(ddlSysLocation.SelectedValue.ToString().Split(',')[1], null, UDFLib.ConvertToInteger(ViewState["VESSEL_ID"]));

                ddlSubSystem_location.Items.Clear();
                ddlSubSystem_location.DataSource = dt;
                ddlSubSystem_location.DataValueField = "AssginLocationID";
                ddlSubSystem_location.DataTextField = "LocationName";
                ddlSubSystem_location.DataBind();
                ddlSubSystem_location.Items.Insert(0, new ListItem("-SELECT-", "0"));
                if (ddlSubSystem_location.SelectedIndex == -1)
                    ddlSubSystem_location.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }

    }
    /// <summary>
    /// Added on:27-07-2016|| To reset drop down to default values.
    /// </summary>
    private void reset_ddl()
    {
        try
        {
            DataTable dtemt = new DataTable();

            DataColumn workCol = dtemt.Columns.Add("AssginLocationID", typeof(Int32));

            dtemt.Columns.Add("LocationName", typeof(String));
            DataRow drsys = dtemt.NewRow();
            drsys["AssginLocationID"] = 0;
            drsys["LocationName"] = "--SELECT --";
            dtemt.Rows.InsertAt(drsys, 0);
            ddlSysLocation.DataSource = dtemt;
            ddlSysLocation.DataValueField = "AssginLocationID";
            ddlSysLocation.DataTextField = "LocationName";
            ddlSysLocation.DataBind();

            DataTable dts = new DataTable();
            DataColumn workCols = dts.Columns.Add("AssginLocationID", typeof(String));

            dts.Columns.Add("LocationName", typeof(String));
            DataRow drsyss = dts.NewRow();
            drsyss["AssginLocationID"] = "0,0";
            drsyss["LocationName"] = "--SELECT --";
            dts.Rows.InsertAt(drsyss, 0);
            ddlSubSystem_location.DataSource = dts;
            ddlSubSystem_location.DataValueField = "AssginLocationID";
            ddlSubSystem_location.DataTextField = "LocationName";
            ddlSubSystem_location.DataBind();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    public void BindJobDoneHistoryList()
    {

        DataTable dt = objBLLPurc.GET_Job_Done_History_List(UDFLib.ConvertToInteger(ViewState["JOB_ID"]), UDFLib.ConvertToInteger(ViewState["VESSEL_ID"]));

        cmbJobLastDone.Items.Clear();
        cmbJobLastDone.DataSource = dt;
        cmbJobLastDone.DataValueField = "JOB_HISTORY_ID";
        cmbJobLastDone.DataTextField = "Date_Done";
        cmbJobLastDone.DataBind();
        cmbJobLastDone.Items.Insert(0, new ListItem("-View Job Histroy-", "0"));

    }
    public void BindRequisitionList()
    {
        if (ddlVessel.SelectedIndex > 0)
        {
            DataTable dt = objBLLPurc.GetRequisitionList(UDFLib.ConvertToInteger(ddlVessel.SelectedValue));

            cmbRequisition.Items.Clear();
            cmbRequisition.DataSource = dt;
            cmbRequisition.DataValueField = "REQUISITION_CODE";
            cmbRequisition.DataTextField = "Display_Code";
            cmbRequisition.DataBind();
            cmbRequisition.Items.Insert(0, new ListItem("-Select Requisition-", "0"));
        }
        else
        {
            cmbRequisition.Items.Insert(0, new ListItem("-Select Requisition-", "0"));
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
            Response.Redirect("~/default.aspx?msgid=2");
            //pnlAddAttachment.Enabled = false;
        }
        if (objUA.Edit == 0)
        {
            Response.Redirect("~/default.aspx?msgid=3");
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

    protected void Load_FleetList()
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
        int Vessel_Manager = UDFLib.ConvertToInteger(Session["USERCOMPANYID"].ToString());

        if (Session["UTYPE"].ToString() == "VESSEL MANAGER")
            Vessel_Manager = UserCompanyID;

        ddlVessel.DataSource = objBLLVessel.Get_VesselList(Fleet_ID, 0, Vessel_Manager, "", UserCompanyID);

        ddlVessel.DataTextField = "VESSEL_NAME";
        ddlVessel.DataValueField = "VESSEL_ID";
        ddlVessel.DataBind();
        ddlVessel.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));
        ddlVessel.SelectedIndex = 0;
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

            ddlInspector.DataSource = objBLLUser.Get_UserList(iCompID);
            ddlInspector.DataTextField = "UserName";
            ddlInspector.DataValueField = "UserID";
            ddlInspector.DataBind();
            ddlInspector.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));

        }
        catch (Exception)
        {
            ////.WriteError(this.GetType().Name.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), ex);
        }
    }

    protected void BindDeptOnShip()
    {
        try
        {

            ddlOnShip.DataSource = objBLL.Get_Dept_OnShip();
            ddlOnShip.DataTextField = "value";
            ddlOnShip.DataValueField = "id";
            ddlOnShip.DataBind();
            ddlOnShip.Items.Insert(0, new ListItem("-Select-", "0"));
        }
        catch (Exception ex)
        {

        }
    }

    protected void BindDeptInOffice()
    {
        try
        {

            ddlInOffice.DataSource = objBLL.Get_Dept_InOffice();
            ddlInOffice.DataTextField = "value";
            ddlInOffice.DataValueField = "id";
            ddlInOffice.DataBind();
            ddlInOffice.Items.Insert(0, new ListItem("-Select-", "0"));
        }
        catch (Exception ex)
        {

        }
    }

    protected void BindJobPriority()
    {
        try
        {

            ddlPriority.DataSource = objBLL.Get_JobPriority();
            ddlPriority.DataTextField = "value";
            ddlPriority.DataValueField = "id";
            ddlPriority.DataBind();
            //  ddlPriority.Items.Insert(0, new ListItem("-Select-", "0"));
        }
        catch (Exception ex)
        {

        }
    }

    protected void BindAssigner()
    {
        try
        {

            ddlAssignedBy.DataSource = objBLL.Get_Assigner();
            ddlAssignedBy.DataTextField = "value";
            ddlAssignedBy.DataValueField = "id";
            ddlAssignedBy.DataBind();
            ddlAssignedBy.Items.Insert(0, new ListItem("-Select-", "0"));
        }
        catch (Exception ex)
        {

        }
    }

    protected void BindJobDetails()
    {


        DataTable dt = objBLLPurc.GET_Adhoc_Job_Details(UDFLib.ConvertToInteger(ViewState["Job_History_Id"])
                     , UDFLib.ConvertToInteger(ViewState["VESSEL_ID"])
                     , UDFLib.ConvertToInteger(ViewState["OFFICE_ID"])
                     , UDFLib.ConvertToInteger(ViewState["JOB_ID"]));


        if (dt.Rows.Count > 0)
        {
            hlnkRequisition.Text = Convert.ToString(dt.Rows[0]["REQUISITION_CODE"]);
            hlnkRequisition.NavigateUrl = "~/purchase/RequisitionSummary.aspx?REQUISITION_CODE=" + Convert.ToString(dt.Rows[0]["REQUISITION_CODE"]) + "&Document_Code=" + Convert.ToString(dt.Rows[0]["DOCUMENT_CODE"]) + "&Vessel_Code=" + Convert.ToString(dt.Rows[0]["Vessel_ID"]) + "&Dept_Code=" + Convert.ToString(dt.Rows[0]["DEPARTMENT"]);


            if (ViewState["Job_History_Id"] == null)
                divAttandRemarks.Visible = false;
            else
                divAttandRemarks.Visible = true;

            DataRow dr = dt.Rows[0];

            if (dr["Job_Status"].ToString() == "PENDING")
            {
                btnRework.Enabled = false;
                btnVerify.Enabled = false;
            }

            if (dr["Job_Status"].ToString() == "COMPLETED")
            {
                btnSave.Enabled = false;
                btnRework.Enabled = true;
                btnVerify.Enabled = true;
            }

            if (dr["Job_Status"].ToString() == "REWORKED")
            {
                btnRework.Enabled = false;
                btnSave.Enabled = false;
                btnVerify.Enabled = false;
            }

            imgAttachmentButton.Visible = true;
            imgRemarkButton.Visible = true;

            if (dr["Job_Status"].ToString() == "VERIFY")
            {
                btnSave.Enabled = false;
                btnRework.Enabled = false;
                btnVerify.Enabled = false;

                imgAttachmentButton.Visible = false;
                imgRemarkButton.Visible = false;

            }

            ddlPrimary.SelectedValue = dr["Primary_Category"].ToString();
            ddlSecondary.SelectedValue = dr["Secondary_Category"].ToString();

            ddlFleet.SelectedValue = dr["FleetCode"].ToString();
            ddlFleet.Enabled = false;
            ddlVessel.SelectedValue = dr["Vessel_id"].ToString();
            ddlVessel.Enabled = false;
            ddlFunction.SelectedValue = dr["Function_ID"].ToString();
            txtJobCode.Text = dr["Job_Code"].ToString();
            txtJobCardNo.Text = dr["Job_card_No"].ToString();
            cmbRequisition.SelectedValue = dr["REQUISITION_CODE"].ToString();
            if (dr["Job_id"].ToString() == "0")
            {
                txtJobType.Text = "NON PMS JOB";
                divPmsJobDetails.Visible = false;
            }
            else
            {
                txtJobType.Text = "PMS JOB";
                divPmsJobDetails.Visible = true;
            }


            txtDepartment.Text = dr["DepartmentName"].ToString();
            txtRank.Text = dr["RankShortName"].ToString();
            txtFrequencyType.Text = dr["FrequencyName"].ToString();
            txtcms.Text = dr["CMS"].ToString();
            txtCritical.Text = dr["CRITICAL"].ToString();

            txtJobStatus.Text = dr["Job_Status"].ToString();
            txtJobTitle.Text = dr["Job_Short_Description"].ToString();
            txtJobDescription.Text = dr["Job_Long_Description"].ToString();


            ddlOnShip.SelectedValue = dr["Dept_On_Ship"].ToString() == "" ? "0" : dr["Dept_On_Ship"].ToString();
            ddlInOffice.SelectedValue = dr["Dept_on_Office"].ToString() == "" ? "0" : dr["Dept_on_Office"].ToString();

            ddlPIC.SelectedValue = dr["PIC"].ToString() == "" ? "0" : dr["PIC"].ToString();
            ddlPSCSIRE.SelectedValue = dr["PSC_SIRE"].ToString() == "" ? "0" : dr["PSC_SIRE"].ToString();

            if (dr["ISVESSEL"].ToString() == "1")
            {
                ddlPIC.Enabled = true;
            }

            if (dr["Defer_to_DryDock"].ToString() == "0")
                rdoDeferToDrydock.Items[1].Selected = true;
            else
                rdoDeferToDrydock.Items[0].Selected = true;

            BindSystem_Location();
            ddlSysLocation.SelectedValue = dr["SystemLocationID"].ToString();
            BindSubSystem_Location();

            //string[] Array = dr["SubSystemLocationID"].ToString().Split(',');

            //ddlSubSystem_location.SelectedValue = Array[0] + ',' + Array[1] + ',' + Array[2];
            ddlSubSystem_location.SelectedValue = dr["SubSystemLocationID"].ToString();

            ddlAssignedBy.SelectedValue = dr["Assigner"].ToString() == "" ? "0" : dr["Assigner"].ToString();
            ddlInspector.SelectedValue = dr["Inspector"].ToString() == "" ? "0" : dr["Inspector"].ToString();
            ddlPriority.SelectedValue = dr["Priority"].ToString() == "" ? "1" : dr["Priority"].ToString();
            txtInspectionDate.Text = dr["Inspection_Date"].ToString();
            txtRaisedOn.Text = dr["Created_On"].ToString();
            txtExpectedComp.Text = dr["Expected_completion"].ToString();
            txtCompletedOn.Text = dr["Completed_on"].ToString();
            chkSafetyAlarm.Checked = false;
            chkCalibration.Checked = false;
            if (dr["IsSafetyAlarm"].ToString() == "1")
                chkSafetyAlarm.Checked = true;
            if (dr["IsCalibration"].ToString() == "1")
                chkCalibration.Checked = true;


            if (dr["IsFunctional"].ToString() == "1")
                rbtnFunctional.Items[0].Selected = true;
            else
                rbtnFunctional.Items[1].Selected = true;


            ddlEffect.SelectedValue = dr["Effect"].ToString();
            txtbxSetPointDecimal.Text = dr["SetPointDecimal"].ToString();
            ddlUnit.SelectedValue = dr["SetPointUnit"].ToString();

            if (dr["JobWorkType"].ToString() == "1")
                rbtnJobProcess.Items[0].Selected = true;
            else
                rbtnJobProcess.Items[1].Selected = true;

            string CREATED_DATE = dr["Completed_on"].ToString() == "" ? "" : "<br>" + dr["Completed_on"].ToString();
            string MODIFIED_DATE = dr["Verified_On"].ToString() == "" ? "" : "<br>" + dr["Verified_On"].ToString();

            if (dr["Created_By_CrewID"].ToString() != "")
            {
                lnkCreatedBy.Text = "Created By: " + dr["Created_By_Name"].ToString() + CREATED_DATE;
                lnkCreatedBy.NavigateUrl = "~/crew/crewdetails.aspx?ID=" + dr["Created_By_CrewID"].ToString();
                lnkCreatedBy.Target = "_blank";
                imgCreatedBy.ImageUrl = "http://" + HttpContext.Current.Request.Url.Host + HttpContext.Current.Request.ApplicationPath + "/uploads/CrewImages/" + dr["PhotoUrl1"].ToString();
                pnlCreatedByInfo.Visible = true;
                lnkCreatedBy.Visible = true;
                imgCreatedBy.Visible = true;
            }
            else
            {
                lnkCreatedBy.Visible = false;
                imgCreatedBy.Visible = false;
            }
            if (dr["Verified_By_CrewID"].ToString() != "")
            {
                lnkVerifiedBy.Text = "Last Modified By: " + dr["Verified_By_Name"].ToString() + MODIFIED_DATE;
                lnkVerifiedBy.NavigateUrl = "~/crew/crewdetails.aspx?ID=" + dr["Verified_By_CrewID"].ToString();
                lnkVerifiedBy.Target = "_blank";
                imgVerifiedBy.ImageUrl = "http://" + HttpContext.Current.Request.Url.Host + HttpContext.Current.Request.ApplicationPath + "/uploads/CrewImages/" + dr["PhotoUrl2"].ToString();
                pnlCreatedByInfo.Visible = true;
                lnkVerifiedBy.Visible = true;
                imgVerifiedBy.Visible = true;
            }
            else
            {
                lnkVerifiedBy.Visible = false;
                imgVerifiedBy.Visible = false;
            }
        }

        BindAttachment();
        BindFollowsup();
    }

    protected void ddlFleet_SelectedIndexChanged(object sender, EventArgs e)
    {

        Load_VesselList();
    }

    protected void ddlVessel_SelectedIndexChanged(object sender, EventArgs e)
    {

        try
        {


            ViewState["VESSEL_ID"] = ddlVessel.SelectedValue;

            if (objBLL.IsVessel(int.Parse(ViewState["VESSEL_ID"].ToString())) == 0)
            {
                ddlPIC.Enabled = true;
                ddlPIC.SelectedIndex = 0;

                ddlAssignedBy.SelectedIndex = 0;

            }
            else
            {


                ddlPIC.Enabled = true;
                ddlPIC.SelectedIndex = 0;
                ddlAssignedBy.SelectedValue = "8";

               

                

            }
            reset_ddl();
            ddlFunction.SelectedIndex = 0;
            BindRequisitionList();

        }
        catch { }

    }
    
    protected void btnSaveAndClose_OnClick(object sender, EventArgs e)
    {
        try
        {

            int VESSEL_ID = int.Parse(ddlVessel.SelectedValue);

            string FOLLOWUP = txtMessage.Text;
            int CREATED_BY = int.Parse(Session["USERID"].ToString());




            txtMessage.Text = "";

            string js = "CloseFollowupDiv();";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "closeDiv", js, true);


        }
        catch (Exception ex)
        {
            string js = "alert('Error in saving data!! Error: " + ex.Message + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUser", js, true);

        }
    }

    private void LoadFollowUps(int OFFICE_ID, int WORKLIST_ID, int VESSEL_ID)
    {

    }

    protected void btnUpdateJob_OnClick(object sender, EventArgs e)
    {




        //Response.Redirect("../Job List/ViewJob.aspx");
        //fillvalue(Convert.ToInt32(hdnJobID.Value));
    }


    public static long GetFileSize(string file)
    {
        FileInfo info = new FileInfo(file);
        long SIZE_BYTES = info.Length;
        //long SIZE_KB = SIZE_BYTES / 1000;
        return SIZE_BYTES;
    }

    protected void ddlFunction_SelectedIndexChanged(object sender, EventArgs e)
    {
        reset_ddl(); // To reset system and subsystem dropdown to default values.
        if (ddlFunction.SelectedIndex != 0)
        {
            BindSystem_Location();
        }
        ddlSubSystem_location.SelectedIndex = 0;

    }

    protected void ddlSystemLocation_SelectedIndexChanged(object sender, EventArgs e)
    {

        BindSubSystem_Location();
    }

    protected void btnRemakrs_Click(object sender, EventArgs e)
    {

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {


            // rdoDeferToDrydock.Items[1].Selected = true;

            int Return_ID = 0;

            int retval = objBLLPurc.INSERT_ADHOC_JOB(UDFLib.ConvertToInteger(ViewState["Job_History_Id"]), UDFLib.ConvertToInteger(ViewState["OFFICE_ID"]), UDFLib.ConvertToInteger(ViewState["VESSEL_ID"])
                , UDFLib.ConvertToInteger(ddlFunction.SelectedValue)
                , UDFLib.ConvertToInteger(ddlSysLocation.SelectedValue.ToString().Split(',')[0])
                , UDFLib.ConvertToInteger(ddlSysLocation.SelectedValue.ToString().Split(',')[1])
                , UDFLib.ConvertToInteger(ddlSubSystem_location.SelectedValue.ToString().Split(',')[1])
                , UDFLib.ConvertToInteger(ddlSubSystem_location.SelectedValue.ToString().Split(',')[0])
                , txtJobTitle.Text, txtJobDescription.Text
                , "PENDING", null
                , Convert.ToInt32(Session["userid"])
                , UDFLib.ConvertIntegerToNull(ddlPIC.SelectedValue)
                , UDFLib.ConvertIntegerToNull(ddlAssignedBy.SelectedValue)
                , UDFLib.ConvertIntegerToNull(rdoDeferToDrydock.Items[0].Selected == true ? 1 : 0), ddlPriority.SelectedValue.ToString()
                , UDFLib.ConvertIntegerToNull(ddlInspector.SelectedValue)
                , UDFLib.ConvertDateToNull(txtInspectionDate.Text), UDFLib.ConvertIntegerToNull(ddlOnShip.SelectedValue)
                , UDFLib.ConvertIntegerToNull(ddlInOffice.SelectedValue)
                , UDFLib.ConvertDateToNull(txtExpectedComp.Text)
                , UDFLib.ConvertDateToNull(txtCompletedOn.Text)
                , UDFLib.ConvertToInteger(ddlPrimary.SelectedValue)
                , UDFLib.ConvertToInteger(ddlSecondary.SelectedValue)
                , UDFLib.ConvertToInteger(ddlPSCSIRE.SelectedValue)
                , cmbRequisition.SelectedValue
                , UDFLib.ConvertIntegerToNull(chkSafetyAlarm.Checked == true ? 1 : 0)
                , UDFLib.ConvertIntegerToNull(chkCalibration.Checked == true ? 1 : 0)
                , UDFLib.ConvertIntegerToNull(rbtnFunctional.Items[0].Selected == true ? 1 : 0)
                , ddlEffect.SelectedValue
                , UDFLib.ConvertToDecimal(txtbxSetPointDecimal.Text.Trim())
                , int.Parse(ddlUnit.SelectedValue)
                , UDFLib.ConvertIntegerToNull(rbtnJobProcess.Items[0].Selected == true ? 1 : 0)
                , ref Return_ID
                );

            ViewState["Job_History_Id"] = Return_ID;
            Session["Non_PMS_Job_History_Id"] = Return_ID;

            Session["Non_PMS_VESSEL_ID"] = ViewState["VESSEL_ID"];
            ViewState["OFFICE_ID"] = 1;
            Session["Non_PMS_Office_ID"] = ViewState["OFFICE_ID"];


            string js = "alert('Saved sucessfully!!');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "savingJob", js, true);

            BindJobDetails();




        }
        catch (Exception ex)
        {
            string js = "alert('Error in saving data!! Error: " + ex.Message + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "errorsaving", js, true);
        }
    }

    protected void btnRework_Click(object sender, EventArgs e)
    {

        int retval = objBLLPurc.Rework_Adhoc_Job(UDFLib.ConvertToInteger(ViewState["Job_History_Id"])
                     , UDFLib.ConvertToInteger(ViewState["VESSEL_ID"])
                     , UDFLib.ConvertToInteger(ViewState["OFFICE_ID"])
                     , Convert.ToInt32(Session["userid"]));


        string js = "alert('Job has been reworked.');this.close();";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "ReworkJob", js, true);



    }

    protected void btnVerify_Click(object sender, EventArgs e)
    {

        int retval = objBLLPurc.Verify_By_office_Adhoc_Job(UDFLib.ConvertToInteger(ViewState["Job_History_Id"])
                    , UDFLib.ConvertToInteger(ViewState["VESSEL_ID"])
                    , UDFLib.ConvertToInteger(ViewState["OFFICE_ID"])
                    , Convert.ToInt32(Session["userid"]));


        string js = "alert('Job has been Verified.');this.close();";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "VerifyJob", js, true);


    }

    protected void btnSaveFollowUpAndClose_OnClick(object sender, EventArgs e)
    {
        try
        {

            if (txtMessage.Text.Trim() != "")
            {

                int newFollowupID = objBLLPurc.INSERT_JOB_HISTORY_REMARKS(UDFLib.ConvertToInteger(ViewState["VESSEL_ID"])
                    , UDFLib.ConvertToInteger(ViewState["OFFICE_ID"])
                    , UDFLib.ConvertToInteger(ViewState["Job_History_Id"])
                    , txtMessage.Text, Convert.ToInt32(Session["USERID"].ToString()), 1);
            }


            BindFollowsup();
        }
        catch (Exception ex)
        {
            string js = "alert('Error in saving data!! Error: " + ex.Message + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "errorsaving", js, true);
        }

    }

    private void BindFollowsup()
    {

        DataTable dt = objBLLPurc.GET_JOB_HISTORY_REMARKS(UDFLib.ConvertToInteger(ViewState["VESSEL_ID"])
                , null
                , UDFLib.ConvertToInteger(ViewState["Job_History_Id"]), UDFLib.ConvertToInteger(ViewState["OFFICE_ID"]));

        grdFollowUps.DataSource = dt;
        grdFollowUps.DataBind();


    }

    private void BindAttachment()
    {
        DataTable dt = objBLLPurc.GET_JOB_DONE_ATTACHMENT(UDFLib.ConvertToInteger(ViewState["VESSEL_ID"])
                , null
                , UDFLib.ConvertToInteger(ViewState["Job_History_Id"]), UDFLib.ConvertToInteger(ViewState["OFFICE_ID"]));




        DataView dvImage = dt.DefaultView;
        dvImage.RowFilter = "Is_Image='1' ";

        ListView1.DataSource = dvImage;
        ListView1.DataBind();
        ListView2.DataSource = dvImage;
        ListView2.DataBind();
        hidenTotalrecords.Value = dvImage.Count.ToString();
        HCurrentIndex.Value = "0";
        if (dvImage.Count == 0)
        {
            tdg.Visible = false;
        }
        else
        {
            tdg.Visible = true;
        }

        dt.DefaultView.RowFilter = "Is_Image='0'  ";
        gvAttachments.DataSource = dt.DefaultView;
        gvAttachments.DataBind();




    }

    protected void AjaxFileUpload1_OnUploadComplete(object sender, AjaxFileUploadEventArgs file)
    {
        try
        {

            Byte[] fileBytes = file.GetContents();
            string sPath = Server.MapPath("\\" + System.Configuration.ConfigurationManager.AppSettings["APP_NAME"].ToString() + "\\uploads\\PmsJobs");
            Guid GUID = Guid.NewGuid();

            string Guid_File_Attach = GUID.ToString() + Path.GetExtension(file.FileName);

            int FileID = objBLLPurc.INSERT_JOB_DONE_ATTACHMENT(UDFLib.ConvertToInteger(Session["Non_PMS_VESSEL_ID"]), UDFLib.ConvertToInteger(Session["Non_PMS_Job_History_Id"])
                , 1, Path.GetFileName(file.FileName), "PMS_" + Guid_File_Attach, file.FileSize, UDFLib.ConvertToInteger(Session["USERID"]), UDFLib.ConvertToInteger(Session["Non_PMS_Office_ID"]));

            string FullFilename = Path.Combine(sPath, "PMS_" + GUID.ToString() + Path.GetExtension(file.FileName));

            FileStream fileStream = new FileStream(FullFilename, FileMode.Create, FileAccess.ReadWrite);
            fileStream.Write(fileBytes, 0, fileBytes.Length);
            fileStream.Close();

            BindAttachment();

            string scrRegGra = "RegisterGralerry();";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "scrRegGra", scrRegGra, true);



        }
        catch (Exception ex)
        {

        }

    }

    protected void btnLoadFiles_Click(object sender, EventArgs e)
    {

        BindAttachment();
    }

    protected void gvAttachments_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    protected void ddlSystemLocation_SelectedIndexChanged1(object sender, EventArgs e)
    {

    }

    protected void ddlSysLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindSubSystem_Location();
    }

    protected void cmbJobLastDone_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (cmbJobLastDone.SelectedValue != "0")
        {

            ViewState["Job_History_Id"] = UDFLib.ConvertIntegerToNull(cmbJobLastDone.SelectedValue.ToString().Split(',')[0]);
            ViewState["OFFICE_ID"] = UDFLib.ConvertToInteger(cmbJobLastDone.SelectedValue.ToString().Split(',')[1]);
            ViewState["Job_Status"] = "COMPLETED";
            ViewState["JOB_ID"] = UDFLib.ConvertToInteger(cmbJobLastDone.SelectedValue.ToString().Split(',')[2]);
            ViewState["VESSEL_ID"] = UDFLib.ConvertToInteger(cmbJobLastDone.SelectedValue.ToString().Split(',')[5]);


            Session["Non_PMS_Job_History_Id"] = ViewState["Job_History_Id"];
            Session["Non_PMS_Office_ID"] = ViewState["OFFICE_ID"];
            Session["Non_PMS_VESSEL_ID"] = ViewState["VESSEL_ID"];


            BindJobeDetailsMain();

            //_SysLocation_ID = UDFLib.ConvertIntegerToNull(cmbJobLastDone.SelectedValue.ToString().Split(',')[3]);
            //_SubSysLocation_ID = UDFLib.ConvertIntegerToNull(cmbJobLastDone.SelectedValue.ToString().Split(',')[4]);
            //_OperationMode = "View";
        }

    }

    protected void BindUnit()
    {
        DataTable dt = objBLLPurc.LibraryGetAlarmUnit();
        ddlUnit.Items.Clear();
        ddlUnit.DataSource = dt;
        ddlUnit.DataValueField = "UnitID";
        ddlUnit.DataTextField = "UnitName";
        ddlUnit.DataBind();
        ddlUnit.Items.Insert(0, new ListItem("-SELECT-", "0"));

    }
    protected void BindEffect()
    {
        DataTable dt = objBLLPurc.LibraryGetAlarmEffect();
        ddlEffect.Items.Clear();
        ddlEffect.DataSource = dt;
        ddlEffect.DataValueField = "EffectID";
        ddlEffect.DataTextField = "EffectName";
        ddlEffect.DataBind();
        ddlEffect.Items.Insert(0, new ListItem("-SELECT-", "0"));

    }
   
    /// <summary>
    ///  Added by reshma RA : To bind RA forms
    /// </summary>
    /// <param name="JobHistoryID"> job history id for specific job</param>
    protected void BindRAFormList(int JobHistoryID)
    {
        DataTable dt = objjob.PMS_DTL_RAFormsByJobID(JobHistoryID);
        if (dt.Rows.Count == 0)
        {
            if (pnlRAForms.Visible == true)
            {
                pnlRAForms.Visible = false;
            }
        }
        else
        {
            if (pnlRAForms.Visible == false)
            {
                pnlRAForms.Visible = true;
            }
        }

        dlRAForms.DataSource = dt;
        dlRAForms.DataBind();
        updRAForms.Update();
    }
}