using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;

using SMS.Business.Infrastructure;
using SMS.Business.PURC;
using SMS.Business.Crew;
using SMS.Business.PMS;
using System.Text;
using SMS.Properties;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;
using AjaxControlToolkit4;
using System.Configuration;


public partial class PMSLibraryJobs : System.Web.UI.Page
{
    BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
    UserAccess objUserAcess = new UserAccess();
    
    protected void Page_Load(object sender, EventArgs e)
    {

        UserAccessValidation();

        if (!IsPostBack)
        {
            BLL_PURC_Purchase objBLLPurc1 = new BLL_PURC_Purchase();
            DDLDepartment.Items.Clear();
            DDLDepartment.DataSource = objBLLPurc1.LibraryGetSystemParameterList("115", null);

            DDLDepartment.DataBind();
            DDLDepartment.Items.Insert(0, new ListItem("--SELECT ALL--", "0"));

            BindFleetDLL();
            DDLFleet.SelectedValue = Session["USERFLEETID"].ToString();
            BindVesselDDL();

            BindDepartmentByST_SP();

            BindSupplierDetails();
            BindSubCatSupplierDetails();
            BindAccountCode();

            BindSubCatAccountCode();

            BindPMSDepartmentDDL();
            BindFrequencyTypeDDL();

            BindRankDDL();

            //BindCatalogFunction();

            ViewState["DeptType"] = null;
            ViewState["DeptCode"] = null;

            //To display Active Records
            ViewState["ActiveStatus"] = 1;
            ViewState["VesselCode"] = 0;

            ViewState["SystemCode"] = null;
            ViewState["SubSystemCode"] = null;

            ViewState["SystemId"] = null;
            ViewState["SubSystemId"] = null;
            hdnSubSysID.Value = "";
            ViewState["JobId"] = null;

            ViewState["VesselCodeEditMode"] = null;
            ViewState["SubSystemIdForCopyJob"] = null;

            ViewState["CATALOGUESORTBYCOLOUMN"] = null;
            ViewState["CATALOGUESORTDIRECTION"] = null;
            ViewState["SUBCATALOGUESORTBYCOLOUMN"] = null;
            ViewState["SUBCATALOGUESORTDIRECTION"] = null;

            ViewState["ITEMSORTDIRECTION"] = null;
            ViewState["ITEMSORTBYCOLOUMN"] = null;

            ViewState["LastCatalogueSelectedRow"] = null;

            ViewState["CatlogueOperationMode"] = "ADD";
            hdfCatlogueOperationMode.Value = "ADD";

            ViewState["SubCatlogueOperationMode"] = "ADD";
            hdnSubcatOperationMode.Value = "ADD";
            ViewState["JobsOperationMode"] = "ADD";
            hdfJobsOperationMode.Value = "ADD";

            ucCustomPagerItems.PageSize = 14;

            btnDivAddLocation.Enabled = false;

            btnCatalogueAdd.Enabled = false;
            btnCatalogueSave.Enabled = false;
            btnSystemLinkRunHourSettings.Enabled = false;
            btnSubSystemLinkRunHourSettings.Enabled = false;
            btnSaveSubCatalogue.Enabled = false;
            btnAddNewSubCatalogue.Enabled = false;
            btnSubSystemLinkRunHourSettings.Enabled = false;
            btnSystemLinkRunHourSettings.Enabled = false;
            btnSubCatDivAddLocation.Enabled = false;

            btnAttach.Enabled = false;
            btnAddNewJob.Enabled = false;
            btnSaveJob.Enabled = false;

            //lstFrequency.SelectedValue = "2485";
            // lstDepartment.SelectedValue = "2488";

            //ddlAccountCode.SelectedValue = "6100";

            //ddlSubCatAccountCode.SelectedValue = "6100";

            optCMS.SelectedValue = "0";
            optCritical.SelectedValue = "0";

            //divAddLocation.Visible = false;

            AssignSPMModuleID();
        }

        lblCatalogueErrorMsg.Text = "";
        lblSubCatalogErrorMsg.Text = "";
        lblItemErrorMsg.Text = "";


    }

    protected void AssignSPMModuleID()
    {

        // Get the Absolute path of the url
        System.IO.FileInfo oInfo = new System.IO.FileInfo(System.Web.HttpContext.Current.Request.Url.AbsolutePath);

        // Get the Page Name
        SqlDataReader dr = BLL_Infra_Common.Get_SPM_Module_ID(oInfo.Name);

        if (dr.HasRows)
        {
            dr.Read();
            string ModuleID = dr["SPM_Module_ID"].ToString();
            //Assign the module ID into the Feeb back button.

            /* if ModuleID is Empty it means there is no  value of  SPM_Module_ID  coloumn in Lib_Menu table
                In this case Feedback would be record under 'COMMON' module ID = 13  under SPM bug traker. 
             */

        }


    }


    protected void UserAccessValidation()
    {

        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        objUserAcess = objUser.Get_UserAccessForPage(Convert.ToInt32(Session["USERID"]), PageURL);

        if (objUserAcess.View == 0)
        {

            Response.Redirect("~/crew/default.aspx?msgid=1");
        }

        if (objUserAcess.Add == 0)
        {
            //catalogue
            btnCatalogueAdd.Enabled = false;
            //btnDivAddLocation.Enabled = false;
            btnCatalogueSave.Enabled = false;
            btnSystemLinkRunHourSettings.Enabled = false;

            //Sub catalogue
            btnAddNewSubCatalogue.Enabled = false;
            btnSaveSubCatalogue.Enabled = false;
            btnSubSystemLinkRunHourSettings.Enabled = false;
            btnSubCatDivAddLocation.Enabled = false;

            
            //job
            btnAddNewJob.Enabled = false;
            btnSaveJob.Enabled = false;
            btnCopyJobs.Enabled = false;
            btnMoveJobs.Enabled = false;
            btnAttach.Enabled = false;

        }

    }



    public void BindCatalogue()
    {
        using (BLL_PURC_Purchase objBLLPurc = new BLL_PURC_Purchase())
        {
            string vesselcode = (ViewState["VesselCode"] == null) ? null : (ViewState["VesselCode"].ToString());
            string deptcode = (ViewState["DeptCode"] == null) ? null : (ViewState["DeptCode"].ToString());

            int? depttype = (DDLDepartment.SelectedValue == "0") ? null : UDFLib.ConvertIntegerToNull(DDLDepartment.SelectedValue);

            int? isactivestatus = null;
            if (ViewState["ActiveStatus"] != null)
                isactivestatus = Convert.ToInt32(ViewState["ActiveStatus"]);

            string sortbycoloumn = (ViewState["CATALOGUESORTBYCOLOUMN"] == null) ? null : (ViewState["CATALOGUESORTBYCOLOUMN"].ToString());
            int? sortdirection = null;

            if (ViewState["CATALOGUESORTDIRECTION"] != null)
                sortdirection = Int32.Parse(ViewState["CATALOGUESORTDIRECTION"].ToString());

            DataSet ds = objBLLPurc.LibraryCatalogueSearch_PMS(null, txtSearchCatalogue.Text.Trim(), "SP", depttype, Int32.Parse(DDLFleet.SelectedValue), Int32.Parse(vesselcode), "", isactivestatus, sortbycoloumn, sortdirection, 1, 500, 1);

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvCatalogue.DataSource = ds.Tables[0];
                gvCatalogue.DataBind();

                if (ViewState["SystemId"] == null)
                {
                    ViewState["SystemId"] = ds.Tables[0].Rows[0]["ID"].ToString();
                    ViewState["SystemCode"] = ds.Tables[0].Rows[0]["System_Code"].ToString();
                    ViewState["VesselCodeEditMode"] = ds.Tables[0].Rows[0]["Vessel_Code"].ToString();
                    ViewState["DeptCode"] = ds.Tables[0].Rows[0]["Dept1"].ToString();

                    gvCatalogue.SelectedIndex = 0;

                }
                if ((String)ViewState["CatlogueOperationMode"] != "ADD")
                {
                    BindCatalogueList(ViewState["SystemId"].ToString());
                    BindCatalogAssingLocationDLL();

                    if (objUserAcess.Add == 1)
                    {
                        btnDivAddLocation.Enabled = true;
                    }
                }


                if (objUserAcess.Add == 1)
                {
                    btnCatalogueAdd.Enabled = true;
                    btnCatalogueSave.Enabled = true;
                    btnSaveSubCatalogue.Enabled = true;
                    btnAddNewSubCatalogue.Enabled = true;
                    btnSystemLinkRunHourSettings.Enabled = true;
                    btnSubSystemLinkRunHourSettings.Enabled = true;
                   // btnSubCatDivAddLocation.Enabled = true;
                }


               // SetCatalogueRowSelection();

            }
            else
            {

                gvCatalogue.DataSource = ds.Tables[0];
                gvCatalogue.DataBind();
                ViewState["SystemId"] = null;
                ViewState["SystemCode"] = null;
                ViewState["CatlogueOperationMode"] = "ADD";
                hdfCatlogueOperationMode.Value = "ADD";
                ddlAccountCode.SelectedValue = "6100";
             //   btnDivAddLocation.Enabled = false;
                btnSaveSubCatalogue.Enabled = false;
                btnAddNewSubCatalogue.Enabled = false;
            //    btnSubCatDivAddLocation.Enabled = false;
                btnAddNewJob.Enabled = false;
                btnSaveJob.Enabled = false;
               // btnAttach.Enabled = false;
            }
        }

        
    }

    public void BindSubCatalogue()
    {

        using (BLL_PURC_Purchase objBLLPurc = new BLL_PURC_Purchase())
        {

            string systemcode = (ViewState["SystemCode"] == null) ? null : (ViewState["SystemCode"].ToString());
            int? isactivestatus = null;
            if (ViewState["ActiveStatus"] != null)
                isactivestatus = Convert.ToInt32(ViewState["ActiveStatus"]);

            string sortbycoloumn = (ViewState["SUBCATALOGUESORTBYCOLOUMN"] == null) ? null : (ViewState["SUBCATALOGUESORTBYCOLOUMN"].ToString());
            int? sortdirection = null;

            if (ViewState["SUBCATALOGUESORTDIRECTION"] != null)
                sortdirection = Int32.Parse(ViewState["SUBCATALOGUESORTDIRECTION"].ToString());


            DataSet ds = objBLLPurc.LibrarySubCatalogueSearch(systemcode, txtSearchSubCatalogue.Text.Trim(), null, isactivestatus, sortbycoloumn, sortdirection, 1, 1000, 1);


            //if (chkRunHourS.Checked == true && txtSubCatalogueName.Text == "GENERAL")
            //    chkCopyRunHourSS.Checked = true;
            //else
            //    chkCopyRunHourSS.Checked = false;

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvSubCatalogue.DataSource = ds.Tables[0];
                gvSubCatalogue.DataBind();

              
                //if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["Copy_Run_Hour"])))
                //{
                //    if (ds.Tables[0].Rows[0]["Copy_Run_Hour"].ToString().Trim() == "1")
                //        chkCopyRunHourSS.Checked = true;
                //    else
                //        chkCopyRunHourSS.Checked = false;
                //}
                //else
                //    chkCopyRunHourSS.Checked = false;
                if ((String)ViewState["lstSubCatVesselLocation"] != "ADD")
                {
                    if (ViewState["SubSystemId"] != null && ViewState["SubSystemId"].ToString() != "")
                    {
                        BindSubCatalogueList(ViewState["SubSystemId"].ToString());
                    }
                   

                    if (objUserAcess.Add == 1)
                    {
                        btnSubCatDivAddLocation.Enabled = true;
                    }
                }
                if (objUserAcess.Add == 1)
                {

                    btnSaveSubCatalogue.Enabled = true;
                    btnAddNewSubCatalogue.Enabled = true;
                   // btnSubCatDivAddLocation.Enabled = true;
                    btnAddNewJob.Enabled = true;
                    btnSaveJob.Enabled = true;
                    //btnAttach.Enabled = true;
                }


               // SetSubCatalogueRowSelection();
            }
            else
            {
                gvSubCatalogue.DataSource = ds.Tables[0];
                gvSubCatalogue.DataBind();


                //ClearSubCatalogueFields();
                ViewState["SubSystemId"] = null;
                hdnSubSysID.Value = "";
                ViewState["SubSystemCode"] = null;
                ViewState["SubCatlogueOperationMode"] = "ADD";
                hdnSubcatOperationMode.Value = "ADD";
                // ddlSubCatAccountCode.SelectedValue = "6100";

                btnAddNewJob.Enabled = false;
                btnSaveJob.Enabled = false;
                //btnAttach.Enabled = false;
            }

        }
    }

    public void BindJobs()
    {

        BLL_PMS_Library_Jobs objJobs = new BLL_PMS_Library_Jobs();

        int rowcount = ucCustomPagerItems.isCountRecord;

        //  string vesselcode = (ViewState["VesselCode"] == null) ? null : (ViewState["VesselCode"].ToString());


        int? vesselcode = null;
        if (ViewState["VesselCodeEditMode"] != null)
            vesselcode = Convert.ToInt32(ViewState["VesselCodeEditMode"]);

        int? systemid = null;
        if (ViewState["SystemId"] != null)
            systemid = Convert.ToInt32(ViewState["SystemId"].ToString());

        int? subsystemid = null;
        if (ViewState["SubSystemId"] != null)
            subsystemid = Convert.ToInt32(ViewState["SubSystemId"].ToString());

        int? isactivestatus = null;
        if (ViewState["ActiveStatus"] != null)
            isactivestatus = Convert.ToInt32(ViewState["ActiveStatus"]);

        string sortbycoloumn = (ViewState["ITEMSORTBYCOLOUMN"] == null) ? null : (ViewState["ITEMSORTBYCOLOUMN"].ToString());
        int? sortdirection = null;

        if (ViewState["ITEMSORTDIRECTION"] != null)
            sortdirection = Int32.Parse(ViewState["ITEMSORTDIRECTION"].ToString());

        DataSet ds = objJobs.LibraryJobSearch(systemid, subsystemid, vesselcode, null, null, txtSearchItemName.Text.Trim()
            , isactivestatus, sortbycoloumn
            , sortdirection, ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref rowcount);

        if (ucCustomPagerItems.isCountRecord == 1)
        {
            ucCustomPagerItems.CountTotalRec = rowcount.ToString();
            ucCustomPagerItems.BuildPager();
        }

        if (ds.Tables[0].Rows.Count > 0)
        {
            gvJobs.DataSource = ds.Tables[0];
            gvJobs.DataBind();

            //if (ViewState["JobId"] == null)
            //{
            //    ViewState["JobId"] = ds.Tables[0].Rows[0]["ID"].ToString();
            //    //ViewState["VesselCodeEditMode"] = ds.Tables[0].Rows[0]["Vessel_ID"].ToString();
            //    gvJobs.SelectedIndex = 0;
            //}

            //SetJobRowSelection();
            
            if (objUserAcess.Add == 1)
            {
                btnAddNewJob.Enabled = true;
                btnSaveJob.Enabled = true;
               // btnAttach.Enabled = false;
            }
            // BindPmsJobAttachment();
            if ((String)ViewState["JobsOperationMode"] != "ADD")
            {
                  if (objUserAcess.Add == 1)
                  {
                      btnAttach.Enabled = true;
                  }
            }


        }
        else
        {

            gvJobs.DataSource = ds.Tables[0];
            gvJobs.DataBind();
            ViewState["JobId"] = null;
            ViewState["JobsOperationMode"] = "ADD";
            hdfJobsOperationMode.Value = "ADD";
        }

    }

    public void BindCatalogueList(string systemid)
    {
        BLL_PURC_Purchase objBLLPurc = new BLL_PURC_Purchase();
        DataSet ds = objBLLPurc.LibraryCatalogueList(systemid);

        if (ds.Tables[0].Rows.Count > 0)
        {
            DataRow dr = ds.Tables[0].Rows[0];
            txtCatalogueCode.Text = dr["System_Code"].ToString();
            txtCatalogueSetInstalled.Text = dr["Set_Instaled"].ToString();
            txtCatalogName.Text = dr["System_Description"].ToString();
            txtCatalogueParticular.Text = dr["System_Particulars"].ToString();
            ddlCalalogueMaker.SelectedValue = dr["Maker"].ToString() != "" ? dr["Maker"].ToString() : "0";
            ddlCatalogDept.SelectedValue = dr["Dept1"].ToString() != "" ? dr["Dept1"].ToString() : "0";
            txtCatalogueModel.Text = dr["Module_Type"].ToString();
            txtCatalogueSerialNumber.Text = dr["Serial_Number"].ToString();
            chkRunHourS.Checked = dr["Run_Hour"].ToString() == "1" ? true : false;
            chkCriticalS.Checked = dr["Critical"].ToString() == "1" ? true : false;
            if (chkRunHourS.Checked == true)
                ViewState["SystemRHrs"] = 1;
            else
                ViewState["SystemRHrs"] = 0;

            if ((dr["Functions"].ToString() != "") && (dr["Functions"].ToString() != "0"))
            {
                ddlCatalogFunction.SelectedValue = dr["Functions"].ToString();
            }
            else
            {
                ddlCatalogFunction.SelectedValue = "";
            }

            if ((dr["ACCOUNT_CODE"].ToString() != "") && (dr["ACCOUNT_CODE"].ToString() != "0"))
            {
                ddlAccountCode.SelectedValue = dr["ACCOUNT_CODE"].ToString();
            }
            else
            {
                ddlAccountCode.SelectedValue = "0";
            }

            lblCatalogueCreatedBy.Text = dr["CREATEDBY"].ToString();
            lblCatalogueModifiedby.Text = dr["MODIFIEDBY"].ToString();
            lblCatalogueDeletedby.Text = dr["DELETEDBY"].ToString();

        }

    }

    public void BindSubCatalogueList(string subsystemid)
    {


        BLL_PURC_Purchase objBLLPurc = new BLL_PURC_Purchase();
        DataSet ds = objBLLPurc.LibrarySubCatalogueList(subsystemid);
        if (ds.Tables[0].Rows.Count > 0)
        {
            DataRow dr = ds.Tables[0].Rows[0];

            txtSubCatalogueCode.Text = dr["Subsystem_Code"].ToString();
            txtSubCatalogueName.Text = dr["Subsystem_Description"].ToString();
            txtSubCatalogueParticulars.Text = dr["Subsystem_Particulars"].ToString();

            txtSubCatModel.Text = Convert.ToString(dr["Module_Type"]);
            txtSubCatSerialNo.Text = Convert.ToString(dr["Serial_Number"]);
            chkRunHourSS.Checked = dr["Run_Hour"].ToString() == "1" ? true : false;
            chkCriticalSS.Checked = dr["Critical"].ToString() == "1" ? true : false;
            chkCopyRunHourSS.Checked = dr["Copy_Run_Hour"].ToString() == "1" ? true : false;
            if (chkRunHourS.Checked == true)
            {
                if (chkRunHourSS.Checked == true)
                {
                    chkCopyRunHourSS.Enabled = true;
                }
            }
            ListItem limaker = ddlSubCatMaker.Items.FindByValue(Convert.ToString(dr["Maker"]));
            if (limaker != null)
            {
                ddlSubCatMaker.ClearSelection();
                limaker.Selected = true;
            }
            else
                ddlSubCatMaker.SelectedValue = "0";

            lstSubCatVesselLocation.DataSource = objBLLPurc.GET_SUBSYTEMSYSTEM_ASSIGNED_LOCATION(Convert.ToString(ViewState["SystemId"]), int.Parse(subsystemid), Convert.ToInt32(ViewState["VesselCodeEditMode"]));
            lstSubCatVesselLocation.DataTextField = "LocationName";
            lstSubCatVesselLocation.DataValueField = "ID";
            lstSubCatVesselLocation.DataBind();

            lblSubCatalogueCreatedBy.Text = dr["CREATEDBY"].ToString();
            lblSubCatalogueModifiedby.Text = dr["MODIFIEDBY"].ToString();
            lblSubCatalogueDeletedBy.Text = dr["DELETEDBY"].ToString();

        }
    }

    public void BindJobList(int jobid)
    {

        BLL_PMS_Library_Jobs objJob = new BLL_PMS_Library_Jobs();
        DataSet ds = objJob.LibraryJobList(jobid);
        if (ds.Tables[0].Rows.Count > 0)
        {
            DataRow dr = ds.Tables[0].Rows[0];

            txtJobTitle.Text = dr["Job_Title"].ToString();
            txtjobDescription.Text = dr["Job_Description"].ToString();

            lstDepartment.SelectedValue = dr["Department_ID"].ToString();

            if ((dr["Rank_ID"].ToString() != "0") && (dr["Rank_ID"].ToString() != ""))
            {
                ddlRank.SelectedValue = dr["Rank_ID"].ToString();
            }

            txtFrequency.Text = dr["Frequency"].ToString();
            lstFrequency.SelectedValue = dr["Frequency_Type"].ToString();

            optCMS.SelectedValue = dr["CMS"].ToString();
            optCritical.SelectedValue = dr["Critical"].ToString();

            if (dr["Is_Tech_Required"].ToString() == "1")
                chkIsTechRequired.Checked = true;
            else
                chkIsTechRequired.Checked = false;

            lblItemCreatedBy.Text = dr["CREATEDBY"].ToString();
            lblItemmodifiedby.Text = dr["MODIFIEDBY"].ToString();
            lblItemDeletedBy.Text = dr["DELETEDBY"].ToString();

            txtJobCode.Text = dr["Job_Code"].ToString();
        }

        BindPmsJobAttachment();

    }

    protected void btnRefreshPMSJobAttachment_Click(object s, EventArgs e)
    {
        BindPmsJobAttachment();
    }

    public void BindFleetDLL()
    {
        try
        {
            BLL_Infra_VesselLib objVsl = new BLL_Infra_VesselLib();

            DataTable FleetDT = objVsl.GetFleetList(Convert.ToInt32(Session["USERCOMPANYID"].ToString()));
            DDLFleet.Items.Clear();
            DDLFleet.DataSource = FleetDT;
            DDLFleet.DataTextField = "Name";
            DDLFleet.DataValueField = "code";
            DDLFleet.DataBind();
            ListItem li = new ListItem("--SELECT ALL--", "0");
            DDLFleet.Items.Insert(0, li);
        }
        catch (Exception ex)
        {

        }
    }

    public void BindVesselDDL()
    {
        try
        {

            BLL_Infra_VesselLib objVsl = new BLL_Infra_VesselLib();

            DataTable dtVessel = objVsl.Get_VesselList(UDFLib.ConvertToInteger(DDLFleet.SelectedValue), 0, Convert.ToInt32(Session["USERCOMPANYID"].ToString()), "", Convert.ToInt32(Session["USERCOMPANYID"].ToString()));
            DDLVessel.Items.Clear();
            DDLVessel.DataSource = dtVessel;
            DDLVessel.DataTextField = "Vessel_name";
            DDLVessel.DataValueField = "Vessel_id";
            DDLVessel.DataBind();
            ListItem li = new ListItem("--SELECT ALL--", "0");
            DDLVessel.Items.Insert(0, li);
        }
        catch (Exception ex)
        {

        }
    }

    private void BindAccountCode()
    {
        using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
        {
            DataTable dtAccountCode = new DataTable();
            dtAccountCode = objTechService.SelectBudgetCode().Tables[0];
            ddlAccountCode.DataTextField = "Budget_Name";
            ddlAccountCode.DataValueField = "Budget_Code";
            ddlAccountCode.DataSource = dtAccountCode;
            ddlAccountCode.DataBind();
            ddlAccountCode.Items.Insert(0, new ListItem("SELECT", "0"));
        }
    }

    private void BindSubCatAccountCode()
    {

        using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
        {

            //DataTable dtAccountCode = new DataTable();
            //dtAccountCode = objTechService.SelectBudgetCode().Tables[0];
            //ddlSubCatAccountCode.DataTextField = "Budget_Name";
            //ddlSubCatAccountCode.DataValueField = "Budget_Code";
            //ddlSubCatAccountCode.DataSource = dtAccountCode;
            //ddlSubCatAccountCode.DataBind();
            //ddlSubCatAccountCode.Items.Insert(0, new ListItem("SELECT", "0"));

        }
    }


    private void BindPMSDepartmentDDL()
    {
        try
        {
            BLL_PMS_Library_Jobs obj = new BLL_PMS_Library_Jobs();

            DataTable dt = obj.LibraryGetPMSSystemParameterList("2487", "");

            lstDepartment.DataSource = dt;
            lstDepartment.DataValueField = "Code";
            lstDepartment.DataTextField = "Name";

            lstDepartment.DataBind();
        }
        catch
        {
        }
    }

    private void BindFrequencyTypeDDL()
    {
        try
        {
            BLL_PMS_Library_Jobs obj = new BLL_PMS_Library_Jobs();
            DataTable dt = obj.LibraryGetPMSSystemParameterList("2491", "");
            lstFrequency.DataSource = dt;
            lstFrequency.DataTextField = "Name";
            lstFrequency.DataValueField = "Code";
            lstFrequency.DataBind();
        }
        catch
        {
        }

    }

    private void BindRankDDL()
    {
        try
        {
            BLL_Crew_Admin objCrewAdmin = new BLL_Crew_Admin();
            DataTable dtRank = new DataTable();
            dtRank = objCrewAdmin.Get_RankList();

            ddlRank.DataTextField = "Rank_Name";
            ddlRank.DataValueField = "ID";
            ddlRank.DataSource = dtRank;
            ddlRank.DataBind();
            ddlRank.Items.Insert(0, new ListItem("SELECT", "0"));

        }
        catch
        {
        }

    }

    private void BindCatalogAssingLocationDLL()
    {
        if ((String)ViewState["CatlogueOperationMode"] == "EDIT")
        {
            BLL_PMS_Library_Jobs objJob = new BLL_PMS_Library_Jobs();
            DataTable dt = objJob.LibraryGetCatalogueLocationAssign(ViewState["SystemCode"].ToString(), Convert.ToInt32(ViewState["VesselCodeEditMode"].ToString()));
            if (dt.Rows.Count > 0)
            {
                lstcatalogLocation.DataTextField = "LocationName";
                lstcatalogLocation.DataValueField = "AssginLocationID";
                lstcatalogLocation.DataSource = dt;
                lstcatalogLocation.DataBind();
            }
            else
            {
                lstcatalogLocation.Items.Clear();
            }
        }
    }


    private void BindCatalogueDepartment()
    {
        using (BLL_PURC_Purchase objBLLPURC = new BLL_PURC_Purchase())
        {
            DataTable dtDepartment = new DataTable();
            dtDepartment = objBLLPURC.SelectDepartment();
            ddlCatalogDept.DataTextField = "Name_Dept";
            ddlCatalogDept.DataValueField = "Code";
            ddlCatalogDept.DataSource = dtDepartment;
            ddlCatalogDept.DataBind();
            ddlCatalogDept.Items.Insert(0, new ListItem("SELECT", "0"));
        }

    }

    private void BindDepartmentByST_SP()
    {
        try
        {
            using (BLL_PURC_Purchase objBLLPURC = new BLL_PURC_Purchase())
            {
                DataTable dtDepartment = new DataTable();
                dtDepartment = objBLLPURC.SelectDepartment();

                dtDepartment.DefaultView.RowFilter = "Form_Type='SP'";
                ddlCatalogDept.Items.Clear();
                ddlCatalogDept.DataSource = dtDepartment;
                ddlCatalogDept.AppendDataBoundItems = true;
                ddlCatalogDept.DataTextField = "Name_Dept";
                ddlCatalogDept.DataValueField = "Code";
                ddlCatalogDept.DataBind();
                ddlCatalogDept.Items.Insert(0, new ListItem("SELECT", "0"));



            }

        }
        catch (Exception ex)
        {

        }

    }

    protected void BindSupplierDetails()
    {
        using (BLL_PURC_Purchase objsupplier = new BLL_PURC_Purchase())
        {
            DataTable dt = objsupplier.SelectSupplier();
            dt.DefaultView.RowFilter = "SUPPLIER_CATEGORY='M'";
            ddlCalalogueMaker.DataTextField = "SUPPLIER_NAME";
            ddlCalalogueMaker.DataValueField = "SUPPLIER";
            ddlCalalogueMaker.DataSource = dt.DefaultView.ToTable();
            ddlCalalogueMaker.DataBind();
            ddlCalalogueMaker.Items.Insert(0, new ListItem("SELECT", "0"));
        }
    }


    protected void BindSubCatSupplierDetails()
    {
        using (BLL_PURC_Purchase objsupplier = new BLL_PURC_Purchase())
        {
            DataTable dt = objsupplier.SelectSupplier();
            dt.DefaultView.RowFilter = "SUPPLIER_CATEGORY='M'";
            ddlSubCatMaker.DataTextField = "SUPPLIER_NAME";
            ddlSubCatMaker.DataValueField = "SUPPLIER";
            ddlSubCatMaker.DataSource = dt.DefaultView.ToTable();
            ddlSubCatMaker.DataBind();
            ddlSubCatMaker.Items.Insert(0, new ListItem("SELECT", "0"));
        }
    }


    protected void ddldisplayRecordType_SelectedIndexChanged(object sender, EventArgs e)
    {

        string activestatus = ddldisplayRecordType.SelectedValue.ToString();

        if (activestatus == "2")
            ViewState["ActiveStatus"] = null;
        else
            ViewState["ActiveStatus"] = ddldisplayRecordType.SelectedValue.ToString();
    }

    protected void DDLFleet_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindVesselDDL();
    }

    protected void optList_SelectedIndexChanged(object sender, EventArgs e)
    {
        ucCustomPagerItems.isCountRecord = 1;

        hdfDeptType.Value = "SP";

        BindDepartmentByST_SP();

        gvCatalogue.DataSource = null;
        gvCatalogue.DataBind();

        gvSubCatalogue.DataSource = null;
        gvSubCatalogue.DataBind();

        gvJobs.DataSource = null;
        gvJobs.DataBind();

        ClearCatalogueFields();
        ClearSubCatalogueFields();
        ClearJobsFields();
    }

    private void ShowNoRecordsFound(DataTable dt, GridView gv)
    {
        try
        {
            dt.Rows.Add(dt.NewRow());
            gv.DataSource = dt;
            gv.DataBind();

            int colcount = gv.Columns.Count;
            gv.Rows[0].Cells.Clear();
            gv.Rows[0].Cells.Add(new TableCell());
            gv.Rows[0].Cells[0].ColumnSpan = colcount;
            gv.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
            gv.Rows[0].Cells[0].ForeColor = System.Drawing.Color.Red;
            gv.Rows[0].Cells[0].Font.Bold = true;
            gv.Rows[0].Cells[0].Text = "NO RECORDS FOUND";
            gv.Rows[0].Attributes["onclick"] = "";
        }
        catch (Exception ex)
        {

        }
    }

    protected void btnCatalogueAdd_Click(object sender, EventArgs e)
    {
        ViewState["CatlogueOperationMode"] = "ADD";
        hdfCatlogueOperationMode.Value = "ADD";

        ViewState["NEXTSYSTEMID"] = GetNextSystemCode();

        txtCatalogueCode.Text = ViewState["NEXTSYSTEMID"].ToString();
        chkSubSysAdd.Checked = true;

        ClearCatalogueFields();
     
    }

    protected string GetNextSystemCode()
    {

        BLL_PURC_Purchase objBLLPurc = new BLL_PURC_Purchase();
        DataTable dt = objBLLPurc.LibraryGetNextSystemCode();
        return dt.Rows[0][0].ToString();
    }


    private void ClearCatalogueFields()
    {

        //txtCatalogueCode.Text = "";
        txtCatalogueSetInstalled.Text = "";
        txtCatalogName.Text = "";
        txtCatalogueModel.Text = "";
        txtCatalogueSerialNumber.Text = "";
        txtCatalogueParticular.Text = "";
        ddlCalalogueMaker.SelectedValue = "0";
        ddlCatalogDept.SelectedValue = "0";

        ddlAccountCode.SelectedValue = "6100";

        lstcatalogLocation.Items.Clear();
        ddlCatalogFunction.SelectedValue = "";

        lblCatalogueCreatedBy.Text = "";
        lblCatalogueDeletedby.Text = "";

        lblCatalogueModifiedby.Text = "";


        ViewState["CatlogueOperationMode"] = "ADD";
        ViewState["NEXTSYSTEMID"] = GetNextSystemCode();
        txtCatalogueCode.Text = ViewState["NEXTSYSTEMID"].ToString();

        chkSubSysAdd.Checked = true;
        //ddlcatalogLocation.SelectedValue = "0";
        //gvCatalogue.SelectedIndex = 0;


    }

    private void ClearSubCatalogueFields()
    {
        txtSubCatalogueCode.Text = "";
        txtSubCatalogueName.Text = "";
        txtSubCatalogueParticulars.Text = "";
        lblSubCatalogueCreatedBy.Text = "";
        lblSubCatalogueModifiedby.Text = "";
        lblSubCatalogueDeletedBy.Text = "";
        ddlSubCatMaker.SelectedValue = "0";
        txtSubCatModel.Text = "";
        txtSubCatSerialNo.Text = "";
        lstSubCatVesselLocation.Items.Clear();


    }

    private void ClearJobsFields()
    {

        txtJobTitle.Text = "";
        txtjobDescription.Text = "";
        txtJobCode.Text = "";
        chkIsTechRequired.Checked = false;
        txtFrequency.Text = "";
        ddlRank.SelectedValue = "0";
        optCMS.SelectedValue = "0";
        optCritical.SelectedValue = "0";

        lstFrequency.SelectedIndex = -1;
        lstDepartment.SelectedIndex = -1;

        //lstFrequency.SelectedValue = "2485";
        //lstDepartment.SelectedValue = "2488";


        //Comment the code as values previous entries of below field should be preserved as per (Satvinder sir on  date : 30/03/12.) 

        //txtFrequency.Text = "";
        // lstFrequency.SelectedValue = "2485";
        // lstDepartment.SelectedValue = "2488";
        //optCMS.SelectedValue = "0";
        //optCritical.SelectedValue = "0";
        //ddlRank.SelectedValue = "0";

        lblItemCreatedBy.Text = "";
        lblItemmodifiedby.Text = "";
        lblItemDeletedBy.Text = "";
        ViewState["JobId"] = "0";
        gvPMSJobAttachment.DataSource = null;
        gvPMSJobAttachment.DataBind();


    }

    protected void btnCatalogueSave_Click(object sender, EventArgs e)
    {

        try
        {

            BLL_PURC_Purchase objBLLPurc = new BLL_PURC_Purchase();

            int? AddSubSysFlag = null; if (chkSubSysAdd.Checked == true) AddSubSysFlag = 1;



            if ((String)ViewState["CatlogueOperationMode"] == "EDIT")
            {
                int val = objBLLPurc.LibraryCatalogueUpdate(Convert.ToInt32(Session["userid"].ToString()), Convert.ToInt32(ViewState["SystemId"].ToString())
                    , txtCatalogueCode.Text.Trim(), txtCatalogName.Text.Trim(), txtCatalogueParticular.Text.Trim(), General.GetNullableString(ddlCalalogueMaker.SelectedValue)
                    , txtCatalogueSetInstalled.Text.Trim(), txtCatalogueModel.Text.Trim(), ddlCatalogDept.SelectedValue, ViewState["VesselCodeEditMode"].ToString()
                    , ddlCatalogFunction.SelectedValue, ddlAccountCode.SelectedValue, txtCatalogueSerialNumber.Text.Trim(), chkRunHourS.Checked ? 1 : 0, chkCriticalS.Checked ? 1 : 0);
            }

            if ((String)ViewState["CatlogueOperationMode"] == "ADD")
            {


                int val = objBLLPurc.LibraryCatalogueSave(Convert.ToInt32(Session["userid"].ToString()), txtCatalogueCode.Text.Trim(), txtCatalogName.Text.Trim()
                     , txtCatalogueParticular.Text.Trim(), General.GetNullableString(ddlCalalogueMaker.SelectedValue), txtCatalogueSetInstalled.Text.Trim()
                     , txtCatalogueModel.Text.Trim(), ddlCatalogDept.SelectedValue, DDLVessel.SelectedValue.ToString()
                     , ddlCatalogFunction.SelectedValue, ddlAccountCode.SelectedValue, AddSubSysFlag, txtCatalogueSerialNumber.Text.Trim(), 1, chkRunHourS.Checked ? 1 : 0, chkCriticalS.Checked ? 1 : 0);

                ViewState["SystemId"] = val;
            }

            BindCatalogue();
            BindSubCatalogue();

            //BindEmptySubCatalogGrid();

            BindEmptyJobGrid();

            ViewState["CatlogueOperationMode"] = "ADD";
            hdfCatlogueOperationMode.Value = "ADD";
            ClearCatalogueFields();

            ViewState["NEXTSYSTEMID"] = GetNextSystemCode();
            txtCatalogueCode.Text = ViewState["NEXTSYSTEMID"].ToString();

            //UpdCatalogueEntry.Update();
            UpdCatelogueGrid.Update();

         //   UpdSubCatalogueEntry.Update();
            UpdSubCatelogueGrid.Update();

            UpdJobEntry.Update();
            UpdJobsGrid.Update();

        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    protected void btnAddNewSubCatalogue_Click(object sender, EventArgs e)
    {
        ViewState["SubCatlogueOperationMode"] = "ADD";
        hdnSubcatOperationMode.Value = "ADD";
        ClearSubCatalogueFields();
        chkRunHourSS.Enabled = true;

    }

    protected void btnSaveSubCatalogue_Click(object sender, EventArgs e)
    {
        try
        {

            BLL_PURC_Purchase objBLLPurc = new BLL_PURC_Purchase();

            if ((String)ViewState["SubCatlogueOperationMode"] == "EDIT")
            {
                int val = objBLLPurc.LibrarySubCatalogueUpdate(Convert.ToInt32(Session["userid"].ToString()), Convert.ToInt32(ViewState["SubSystemId"].ToString()), ViewState["SystemCode"].ToString(), txtSubCatalogueCode.Text.Trim()
                    , txtSubCatalogueName.Text.Trim(), txtSubCatalogueParticulars.Text.Trim(), ddlSubCatMaker.SelectedValue, txtSubCatModel.Text.Trim(), txtSubCatSerialNo.Text.Trim(), chkRunHourSS.Checked ? 1 : 0, chkCriticalSS.Checked ? 1 : 0, chkCopyRunHourSS.Checked ? 1 : 0);
            }

            if ((String)ViewState["SubCatlogueOperationMode"] == "ADD")
            {
                //int val = objBLLPurc.LibrarySubCatalogueSave(Convert.ToInt32(Session["userid"].ToString()), ViewState["SystemCode"].ToString(), txtSubCatalogueCode.Text
                //      , txtSubCatalogueName.Text, txtSubCatalogueParticulars.Text);


                int val = objBLLPurc.LibrarySubCatalogueSave(Convert.ToInt32(Session["userid"].ToString()), ViewState["SystemCode"].ToString(), txtSubCatalogueCode.Text.Trim()
                     , txtSubCatalogueName.Text.Trim(), txtSubCatalogueParticulars.Text.Trim(), ddlSubCatMaker.SelectedValue, txtSubCatModel.Text.Trim(), txtSubCatSerialNo.Text.Trim(), 1, chkRunHourSS.Checked ? 1 : 0, chkCriticalSS.Checked ? 1 : 0, chkCopyRunHourSS.Checked ? 1 : 0);


                ViewState["SubSystemId"] = val;
                hdnSubSysID.Value = UDFLib.ConvertStringToNull(val);
            }


            BindSubCatalogue();
            BindEmptyJobGrid();

            ViewState["SubCatlogueOperationMode"] = "ADD";
            hdnSubcatOperationMode.Value = "ADD";
            ClearSubCatalogueFields();

            //UpdSubCatalogueEntry.Update();
            UpdSubCatelogueGrid.Update();

            UpdJobEntry.Update();
            UpdJobsGrid.Update();

        }
        catch (Exception ex)
        {
            lblSubCatalogErrorMsg.Text = ex.ToString();
        }

    }

    protected void btnAddNewJob_Click(object sender, EventArgs e)
    {
        ViewState["JobsOperationMode"] = "ADD";
        hdfJobsOperationMode.Value = "ADD";
        ClearJobsFields();
    }

    protected void btnSaveJob_Click(object sender, EventArgs e)
    {
        try
        {
            BLL_PMS_Library_Jobs objjobs = new BLL_PMS_Library_Jobs();

            if ((String)ViewState["JobsOperationMode"] == "EDIT")
            {
                int val = objjobs.LibraryJobUpdate(Convert.ToInt32(Session["userid"].ToString()), Convert.ToInt32(ViewState["JobId"].ToString())
                    , Convert.ToInt32(ViewState["SystemId"].ToString()), Convert.ToInt32(ViewState["SubSystemId"].ToString())
                    , Convert.ToInt32(ViewState["VesselCodeEditMode"].ToString())
                    , Convert.ToInt32(lstDepartment.SelectedValue), Convert.ToInt32(ddlRank.SelectedValue), txtJobTitle.Text.Trim(), txtjobDescription.Text.Trim()
                    , Convert.ToInt32(txtFrequency.Text.Trim())
                    , Convert.ToInt32(lstFrequency.SelectedValue)
                    , Convert.ToInt32(optCMS.SelectedValue), Convert.ToInt32(optCritical.SelectedValue), UDFLib.ConvertStringToNull(txtJobCode.Text.Trim())
                    , chkIsTechRequired.Checked == true ? 1 : 0);
            }

            if ((String)ViewState["JobsOperationMode"] == "ADD")
            {
                int val = objjobs.LibraryJobSave(Convert.ToInt32(Session["userid"].ToString())
                    , Convert.ToInt32(ViewState["SystemId"].ToString()), Convert.ToInt32(ViewState["SubSystemId"].ToString())
                    , Convert.ToInt32(ViewState["VesselCodeEditMode"].ToString())
                    , Convert.ToInt32(lstDepartment.SelectedValue), Convert.ToInt32(ddlRank.SelectedValue), txtJobTitle.Text.Trim(), txtjobDescription.Text.Trim()
                    , Convert.ToInt32(txtFrequency.Text.Trim())
                    , Convert.ToInt32(lstFrequency.SelectedValue)
                    , Convert.ToInt32(optCMS.SelectedValue), Convert.ToInt32(optCritical.SelectedValue), UDFLib.ConvertStringToNull(txtJobCode.Text.Trim())
                    , chkIsTechRequired.Checked == true ? 1 : 0);

                ViewState["JobId"] = val;
            }


            BindJobs();

            ViewState["JobsOperationMode"] = "ADD";
            hdfJobsOperationMode.Value = "ADD";

            ClearJobsFields();

            UpdJobEntry.Update();
            UpdJobsGrid.Update();

        }
        catch (Exception ex)
        {
            lblItemErrorMsg.Text = ex.ToString();
        }
    }

    protected void gvCatalogue_SelectedIndexChanging(object sender, GridViewSelectEventArgs se)
    {
        ucCustomPagerItems.isCountRecord = 1;
        gvCatalogue.SelectedIndex = se.NewSelectedIndex;

        chkSubSysAdd.Checked = false;
        ViewState["SystemId"] = ((Label)gvCatalogue.Rows[se.NewSelectedIndex].FindControl("lblSystemId")).Text;
        ViewState["SystemCode"] = ((Label)gvCatalogue.Rows[se.NewSelectedIndex].FindControl("lblSystemCode")).Text;
        ViewState["VesselCodeEditMode"] = ((Label)gvCatalogue.Rows[se.NewSelectedIndex].FindControl("lblVesselCode")).Text;
        ViewState["DeptCode"] = ((Label)gvCatalogue.Rows[se.NewSelectedIndex].FindControl("lblDeptCode")).Text;

        ViewState["SubSystemId"] = null;
        hdnSubSysID.Value = "";
        ViewState["JobId"] = null;

        ViewState["CatlogueOperationMode"] = "EDIT";
        hdfCatlogueOperationMode.Value = "EDIT";

        ViewState["SubCatlogueOperationMode"] = "ADD";
        hdnSubcatOperationMode.Value = "ADD";
        ViewState["JobsOperationMode"] = "ADD";
        hdfJobsOperationMode.Value = "ADD";


        BindCatalogueList(ViewState["SystemId"].ToString());
        BindCatalogAssingLocationDLL();

        BindCatalogue();
        BindSubCatalogue();
        BindJobs();
        SetCatalogueRowSelection();
        ClearSubCatalogueFields();
        ClearJobsFields();

        UpdJobsGrid.Update();
        UpdJobEntry.Update();
    //    UpdCatalogueEntry.Update();
        UpdSubCatelogueGrid.Update();
  //      UpdSubCatalogueEntry.Update();
        UpdJobsGrid.Update();
        UpdJobEntry.Update();



        string jsCatalogGridScroll = ScriptSaveCatalogGridScroll();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "ScriptSaveCatalogGridScroll", jsCatalogGridScroll, true);

    }

    private void BindEmptyJobGrid()
    {

        int rowcount = 0;

        BLL_PMS_Library_Jobs objJobs = new BLL_PMS_Library_Jobs();
        DataSet ds = objJobs.LibraryJobSearch(null, null, null, null, null, null
              , null, null
              , null, 0, 1, ref rowcount);


        gvJobs.DataSource = ds.Tables[0];
        gvJobs.DataBind();
        ucCustomPagerItems.Visible = false;

        ClearSubCatalogueFields();
        ClearJobsFields();

    }

    private void BindEmptySubCatalogGrid()
    {

        BLL_PURC_Purchase objBLLPurc = new BLL_PURC_Purchase();
        DataSet ds = objBLLPurc.LibrarySubCatalogueSearch("-#001###", "-1#", null, -1, null, null, 0, 0, 0);

        gvSubCatalogue.DataSource = ds.Tables[0];
        gvSubCatalogue.DataBind();

    }

    protected void gvCatalogue_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblCalogueActiveSatus = (Label)e.Row.FindControl("lblCalogueActiveSatus");
            LinkButton lnkSystemName = (LinkButton)e.Row.FindControl("lnbSystemName");
            ImageButton ImgCatalogueRestore = (ImageButton)e.Row.FindControl("ImgCatalogueRestore");

            ImageButton ImgCatalogueDelete = (ImageButton)e.Row.FindControl("ImgCatalogueDelete");

            Label lblMaker = (Label)e.Row.FindControl("lblMaker");
            Label lblModel = (Label)e.Row.FindControl("lblModel");
            Label lblParticulars = (Label)e.Row.FindControl("lblParticulars");

            if (lblMaker.Text != "" || lblModel.Text != "" || lblParticulars.Text != "")
                lnkSystemName.Attributes.Add("Title", "cssbody=[dvbdy1] cssheader=[dvhdr1] header=[Maker/ Model / Particulars] body=[" + lblMaker.Text + " /  " + lblModel.Text + " / " + lblParticulars.Text + "]");

            Int64 result = 0;
            if (Int64.TryParse(lblCalogueActiveSatus.Text, out result))
            {
                e.Row.ForeColor = (result == 0) ? System.Drawing.Color.Red : System.Drawing.Color.Black;
                lnkSystemName.ForeColor = (result == 0) ? System.Drawing.Color.Red : System.Drawing.Color.Black;

                ImgCatalogueRestore.Visible = (result == 0) ? true : false;
                ImgCatalogueDelete.Visible = (result == 0) ? false : true;

            }

            if (objUserAcess.Delete == 1 && result == 1)
            {
                ImgCatalogueDelete.Visible = true;
            }
            else
            {
                ImgCatalogueDelete.Visible = false;
            }
        }



        if (e.Row.RowType == DataControlRowType.Header)
        {
            if (ViewState["CATALOGUESORTBYCOLOUMN"] != null)
            {
                HtmlImage img = (HtmlImage)e.Row.FindControl(ViewState["CATALOGUESORTBYCOLOUMN"].ToString());
                if (img != null)
                {
                    if (ViewState["CATALOGUESORTDIRECTION"] == null || ViewState["CATALOGUESORTDIRECTION"].ToString() == "0")
                        img.Src = "~/purchase/Image/arrowDown.png";
                    else
                        img.Src = "~/purchase/Image/arrowUp.png";

                    img.Visible = true;
                }
            }
        }



    }

    protected void gvSubCatalogue_SelectedIndexChanging(object sender, GridViewSelectEventArgs se)
    {
        ucCustomPagerItems.isCountRecord = 1;

        gvSubCatalogue.SelectedIndex = se.NewSelectedIndex;

        ViewState["LastSubCatalogueSelectedRow"] = se.NewSelectedIndex;

        ViewState["SubSystemId"] = ((Label)gvSubCatalogue.Rows[se.NewSelectedIndex].FindControl("lblSubSystemID")).Text;
        hdnSubSysID.Value = ((Label)gvSubCatalogue.Rows[se.NewSelectedIndex].FindControl("lblSubSystemID")).Text;
        ViewState["SubSystemCode"] = ((Label)gvSubCatalogue.Rows[se.NewSelectedIndex].FindControl("lblSubSystemCode")).Text;
        ViewState["SubSystemIdForCopyJob"] = ((Label)gvSubCatalogue.Rows[se.NewSelectedIndex].FindControl("lblSubSystemID")).Text;

        string IsGeneral = ((LinkButton)gvSubCatalogue.Rows[se.NewSelectedIndex].FindControl("lblSubCatalogueName")).Text;
        chkRunHourSS.Checked = false;
        chkCopyRunHourSS.Checked = false;
        if (IsGeneral.ToLower() == "general")
        {

            chkRunHourSS.Enabled = false;
            chkCopyRunHourSS.Enabled = false;
        }
        else
            chkRunHourSS.Enabled = true;


        // ViewState["JobId"] = null;

        ViewState["SubCatlogueOperationMode"] = "EDIT";
        hdnSubcatOperationMode.Value = "EDIT";
        ViewState["JobsOperationMode"] = "ADD";
        hdfJobsOperationMode.Value = "ADD";
        SetSubCatalogueRowSelection();
        if (ViewState["SubSystemId"] == null)
        {
            gvSubCatalogue.SelectedIndex = 0;
        }
        if ((String)ViewState["SubCatlogueOperationMode"] != "ADD")
        {
            BindSubCatalogueList(ViewState["SubSystemId"].ToString());
            gvSubCatalogue.SelectedIndex = Convert.ToInt32(ViewState["LastSubCatalogueSelectedRow"].ToString());

            if (objUserAcess.Add == 1)
            {
                btnSubCatDivAddLocation.Enabled = true;
            }
        }



        // BindSubCatalogueList(ViewState["SubSystemId"].ToString());

        BindSubCatalogue();
        BindJobs();

        UpdJobEntry.Update();
        UpdJobsGrid.Update();
       // UpdSubCatalogueEntry.Update();

        ClearJobsFields();

        string jsSubCatalogGridScroll = ScriptSaveSubCatalogGridScroll();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "ScriptSaveCatalogGridScroll", jsSubCatalogGridScroll, true);



    }

    protected void gvSubCatalogue_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblSubCalogueActiveSatus = (Label)e.Row.FindControl("lblSubCalogueActiveSatus");
            LinkButton lnkSubCatalogueName = (LinkButton)e.Row.FindControl("lblSubCatalogueName");
            ImageButton ImgSubCatalogueRestore = (ImageButton)e.Row.FindControl("ImgSubCatalogueRestore");
            ImageButton ImgSubCatalogueDelete = (ImageButton)e.Row.FindControl("ImgSubCatalogueDelete");

            Label lblSubsystemParticulars = (Label)e.Row.FindControl("lblSubsystemParticulars");


            if (lblSubsystemParticulars.Text != "")
                lnkSubCatalogueName.Attributes.Add("Title", "cssbody=[dvbdy1] cssheader=[dvhdr1] header=[Particulars] body=[" + lblSubsystemParticulars.Text + "]");

            Int64 result = 0;
            if (Int64.TryParse(lblSubCalogueActiveSatus.Text, out result))
            {
                e.Row.ForeColor = (result == 0) ? System.Drawing.Color.Red : System.Drawing.Color.Black;
                lnkSubCatalogueName.ForeColor = (result == 0) ? System.Drawing.Color.Red : System.Drawing.Color.Black;

                ImgSubCatalogueRestore.Visible = (result == 0) ? true : false;
                ImgSubCatalogueDelete.Visible = (result == 0) ? false : true;


            }

            if (objUserAcess.Delete == 1 && result == 1)
            {
                ImgSubCatalogueDelete.Visible = true;
            }
            else
            {
                ImgSubCatalogueDelete.Visible = false;
            }

        }


        if (e.Row.RowType == DataControlRowType.Header)
        {
            if (ViewState["SUBCATALOGUESORTBYCOLOUMN"] != null)
            {
                HtmlImage img = (HtmlImage)e.Row.FindControl(ViewState["SUBCATALOGUESORTBYCOLOUMN"].ToString());
                if (img != null)
                {
                    if (ViewState["SUBCATALOGUESORTDIRECTION"] == null || ViewState["SUBCATALOGUESORTDIRECTION"].ToString() == "0")
                        img.Src = "~/purchase/Image/arrowDown.png";
                    else
                        img.Src = "~/purchase/Image/arrowUp.png";

                    img.Visible = true;
                }
            }
        }
    }

    protected void gvJobs_SelectedIndexChanging(object sender, GridViewSelectEventArgs se)
    {
        gvJobs.SelectedIndex = se.NewSelectedIndex;

        ViewState["LastJobSelectedRow"] = se.NewSelectedIndex;

        ViewState["JobId"] = ((Label)gvJobs.Rows[se.NewSelectedIndex].FindControl("lblJobID")).Text;
        ViewState["VesselCodeEditMode"] = ((Label)gvJobs.Rows[se.NewSelectedIndex].FindControl("lblVesselID")).Text;


        BindJobList(Convert.ToInt32(ViewState["JobId"].ToString()));


        ViewState["CatlogueOperationMode"] = "EDIT";
        hdfCatlogueOperationMode.Value = "EDIT";

        ViewState["SubCatlogueOperationMode"] = "EDIT";
        hdnSubcatOperationMode.Value = "EDIT";
        ViewState["JobsOperationMode"] = "EDIT";
        hdfJobsOperationMode.Value = "EDIT";

        BindJobs();

        UpdJobEntry.Update();
    }

    protected void gvJobs_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblItemActiveSatus = (Label)e.Row.FindControl("lblItemActiveSatus");
            LinkButton lblJobTitle = (LinkButton)e.Row.FindControl("lblJobTitle");
            ImageButton ImgItemRestore = (ImageButton)e.Row.FindControl("ImgItemRestore");
            ImageButton ImgItemDelete = (ImageButton)e.Row.FindControl("ImgItemDelete");
            Label lblJobDescription = (Label)e.Row.FindControl("lblJobDescription");

            Label lblCMS = (Label)e.Row.FindControl("lblCMS");
            Label lblCritical = (Label)e.Row.FindControl("lblCritical");

            if (lblJobDescription.Text != "")
                lblJobTitle.Attributes.Add("Title", "cssbody=[dvbdy1] cssheader=[dvhdr1] header=[Description] body=[" + lblJobDescription.Text + "]");

            Int64 result = 0;
            if (Int64.TryParse(lblItemActiveSatus.Text, out result))
            {
                e.Row.ForeColor = (result == 0) ? System.Drawing.Color.Red : System.Drawing.Color.Black;
                lblJobTitle.ForeColor = (result == 0) ? System.Drawing.Color.Red : System.Drawing.Color.Black;

                ImgItemRestore.Visible = (result == 0) ? true : false;
                ImgItemDelete.Visible = (result == 0) ? false : true;
            }


            if (objUserAcess.Delete == 1 && result == 1)
            {
                ImgItemDelete.Visible = true;
            }
            else
            {
                ImgItemDelete.Visible = false;
            }



            if (lblCMS.Text == "Y")
            {
                e.Row.Cells[7].BackColor = System.Drawing.Color.Green;
                e.Row.Cells[7].ForeColor = System.Drawing.Color.White;
                e.Row.Cells[7].Font.Bold = true;
            }
            if (lblCritical.Text == "Y")
            {
                e.Row.Cells[8].BackColor = System.Drawing.Color.Red;
                e.Row.Cells[8].ForeColor = System.Drawing.Color.White;
                e.Row.Cells[8].Font.Bold = true;
            }
        }

        if (e.Row.RowType == DataControlRowType.Header)
        {
            if (ViewState["ITEMSORTBYCOLOUMN"] != null)
            {
                HtmlImage img = (HtmlImage)e.Row.FindControl(ViewState["ITEMSORTBYCOLOUMN"].ToString());
                if (img != null)
                {
                    if (ViewState["ITEMSORTDIRECTION"] == null || ViewState["ITEMSORTDIRECTION"].ToString() == "0")
                        img.Src = "~/purchase/Image/arrowDown.png";
                    else
                        img.Src = "~/purchase/Image/arrowUp.png";

                    img.Visible = true;
                }
            }
        }


    }

    protected void imgCatalogueSearch_Click(object sender, ImageClickEventArgs e)
    {

        BindCatalogue();

        BindEmptySubCatalogGrid();
        BindEmptyJobGrid();


        ClearCatalogueFields();
        ClearSubCatalogueFields();
        ClearJobsFields();


        UpdCatelogueGrid.Update();
    //    UpdCatalogueEntry.Update();

        UpdSubCatelogueGrid.Update();
       // UpdSubCatalogueEntry.Update();
        UpdJobsGrid.Update();
        UpdJobEntry.Update();

    }

    protected void imgSubCatalogueSearch_Click(object sender, ImageClickEventArgs e)
    {

        BindSubCatalogue();

        ClearSubCatalogueFields();
        BindEmptyJobGrid();
        ClearJobsFields();


        UpdSubCatelogueGrid.Update();
        //UpdSubCatalogueEntry.Update();
        UpdJobEntry.Update();
        UpdJobsGrid.Update();

    }

    protected void imgItemSearch_Click(object sender, ImageClickEventArgs e)
    {

        BindJobs();
        ClearJobsFields();

        UpdJobsGrid.Update();
        UpdJobEntry.Update();

    }

    protected void ImgJobDelete_Click(object sender, CommandEventArgs e)
    {
        BLL_PMS_Library_Jobs objjob = new BLL_PMS_Library_Jobs();
        int retval = objjob.LibraryJobDelete(Convert.ToInt32(Convert.ToInt32(Session["userid"].ToString())), Convert.ToInt32(e.CommandArgument.ToString()));

        BindJobs();

        ClearJobsFields();

        UpdJobEntry.Update();
        UpdJobsGrid.Update();
    }

    protected void ImgJobRestore_Click(object sender, CommandEventArgs e)
    {
        BLL_PMS_Library_Jobs objjob = new BLL_PMS_Library_Jobs();
        int retval = objjob.LibraryJobRestore(Convert.ToInt32(Session["userid"].ToString()), Convert.ToInt32(e.CommandArgument.ToString()));

        BindJobs();

        ClearJobsFields();

        UpdJobEntry.Update();
        UpdJobsGrid.Update();
    }

    protected void ImgCatalogueDelete_Click(object sender, CommandEventArgs e)
    {
        using (BLL_PURC_Purchase objBLLPurc = new BLL_PURC_Purchase())
        {
            int retval = objBLLPurc.LibraryCatalogueDelete(Convert.ToInt32(Session["userid"].ToString()), Convert.ToInt32(e.CommandArgument.ToString()));
        }

        BindCatalogue();

        BindEmptySubCatalogGrid();
        BindEmptyJobGrid();


        ClearCatalogueFields();
        ClearSubCatalogueFields();
        ClearJobsFields();


        btnSaveSubCatalogue.Enabled = false;
        btnAddNewSubCatalogue.Enabled = false;
        //btnSubCatDivAddLocation.Enabled = false;

        btnSaveJob.Enabled = false;
        btnAddNewJob.Enabled = false;
       // btnAttach.Enabled = false;

        UpdCatelogueGrid.Update();
       // UpdCatalogueEntry.Update();

        UpdSubCatelogueGrid.Update();
      //  UpdSubCatalogueEntry.Update();

        UpdJobsGrid.Update();
        UpdJobEntry.Update();

    }

    protected void ImgCatalogueRestore_Click(object sender, CommandEventArgs e)
    {
        using (BLL_PURC_Purchase objBLLPurc = new BLL_PURC_Purchase())
        {
            int retval = objBLLPurc.LibraryCatalogueRestore(Convert.ToInt32(Session["userid"].ToString()), Convert.ToInt32(e.CommandArgument.ToString()));
        }

        BindCatalogue();

        BindEmptySubCatalogGrid();
        BindEmptyJobGrid();

        ClearCatalogueFields();
        ClearSubCatalogueFields();
        ClearJobsFields();

        UpdCatelogueGrid.Update();
     //   UpdCatalogueEntry.Update();

        UpdSubCatelogueGrid.Update();
  //      UpdSubCatalogueEntry.Update();

        UpdJobsGrid.Update();
        UpdJobEntry.Update();

    }

    protected void ImgSubCatalogueDelete_Click(object sender, CommandEventArgs e)
    {
        using (BLL_PURC_Purchase objBLLPurc = new BLL_PURC_Purchase())
        {
            int retval = objBLLPurc.LibrarySubCatalogueDelete(Convert.ToInt32(Session["userid"].ToString()), Convert.ToInt32(e.CommandArgument.ToString()));
        }

        BindSubCatalogue();

        BindEmptyJobGrid();

        ClearSubCatalogueFields();
        ClearJobsFields();

        UpdSubCatelogueGrid.Update();
      //  UpdSubCatalogueEntry.Update();

        UpdJobsGrid.Update();
        UpdJobEntry.Update();

    }

    protected void ImgSubCatalogueRestore_Click(object sender, CommandEventArgs e)
    {
        using (BLL_PURC_Purchase objBLLPurc = new BLL_PURC_Purchase())
        {
            int retval = objBLLPurc.LibrarySubCatalogueRestore(Convert.ToInt32(Session["userid"].ToString()), Convert.ToInt32(e.CommandArgument.ToString()));
        }

        BindSubCatalogue();

        BindEmptyJobGrid();

        ClearSubCatalogueFields();
        ClearJobsFields();

        UpdSubCatelogueGrid.Update();
        //UpdSubCatalogueEntry.Update();

        UpdJobsGrid.Update();
        UpdJobEntry.Update();
    }

    protected void btnRetrieve_Click(object sender, EventArgs e)
    {

        if (objUserAcess.Add == 1)
        {
           // btnDivAddLocation.Enabled = true;
            btnCatalogueAdd.Enabled = true;
            btnCatalogueSave.Enabled = true;
            btnSaveSubCatalogue.Enabled = true;
            btnAddNewSubCatalogue.Enabled = true;
         //   btnSubCatDivAddLocation.Enabled = true;
            btnSubSystemLinkRunHourSettings.Enabled = true;
            btnSystemLinkRunHourSettings.Enabled = true;
        }





        ViewState["SystemId"] = null;
        ViewState["SubSystemId"] = null;
        hdnSubSysID.Value = "";
        ViewState["JobId"] = null;


        ViewState["VesselCodeEditMode"] = null;
        ViewState["DeptCode"] = null;
        ViewState["SystemCode"] = null;
        ViewState["SubSystemCode"] = null;


        ucCustomPagerItems.isCountRecord = 1;
        string vesselcode = DDLVessel.SelectedValue.ToString();

        ViewState["VesselCode"] = DDLVessel.SelectedValue.ToString();
        ddlAccountCode.SelectedValue = "6100";


        ViewState["CatlogueOperationMode"] = "ADD";
        hdfCatlogueOperationMode.Value = "ADD";
        ViewState["SubCatlogueOperationMode"] = "ADD";
        hdnSubcatOperationMode.Value = "ADD";
        ViewState["JobsOperationMode"] = "ADD";
        hdfJobsOperationMode.Value = "ADD";


        ViewState["NEXTSYSTEMID"] = GetNextSystemCode();
        txtCatalogueCode.Text = ViewState["NEXTSYSTEMID"].ToString();


        BindCatalogue();
        BindEmptySubCatalogGrid();
        BindEmptyJobGrid();



        btnAddNewJob.Enabled = false;
        btnSaveJob.Enabled = false;
    //    btnAttach.Enabled = false;

        //btnDivAddLocation.Enabled = false;


        ClearCatalogueFields();
        ClearSubCatalogueFields();
        ClearJobsFields();

        UpdCatelogueGrid.Update();
       // UpdCatalogueEntry.Update();

        UpdSubCatelogueGrid.Update();
       // UpdSubCatalogueEntry.Update();

        UpdJobsGrid.Update();
        UpdJobEntry.Update();

    }

    protected void btnCopyJobs_Click(object sender, EventArgs e)
    {

        if ((ViewState["VesselCodeEditMode"] == null))
        {
            string msg1 = string.Format("document.getElementById('iFrmCopyJobs').src ='../PMS/PMSCopyJobs.aspx';showModal('dvCopyJobsPopUp');");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg1", msg1, true);
        }
        else
        {
            string msg2 = string.Format("document.getElementById('iFrmCopyJobs').src ='../PMS/PMSCopyJobs.aspx?VesselCode=" + ViewState["VesselCodeEditMode"] + "&SystemCode=" + ViewState["SystemCode"] + "&SystemID=" + ViewState["SystemId"] + "&SubSystemID=" + ViewState["SubSystemId"] + "&SubSystemCode=" + ViewState["SubSystemCode"] + "&DeptCode=" + ViewState["DeptCode"] + "';showModal('dvCopyJobsPopUp');");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg2", msg2, true);
        }
    }

    protected void btnAttach_Click(object sender, EventArgs e)
    {
      
        if (UDFLib.ConvertToInteger(ViewState["JobId"]) > 0)
        {
            string msg4 = string.Format("document.getElementById('iFrmCopyJobs').src ='../PMS/PMSJobsAttachment.aspx?VesselCode=" + ViewState["VesselCodeEditMode"] + "&JobId=" + ViewState["JobId"] + "';showModal('dvCopyJobsPopUp');");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg4", msg4, true);
        }
        else
        {
            string msg4 = string.Format("alert('Please select or save job !');");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "jobsavemsg", msg4, true);
        }
    }


    protected void btnMoveJobs_Click(object sender, EventArgs e)
    {

        if (isJobSelect())
        {
            string msg3 = string.Format("document.getElementById('iFrmCopyJobs').src ='../PMS/PMSMoveJobs.aspx?VesselCode=" + DDLVessel.SelectedValue + "&SystemCode=" + ViewState["SystemCode"] + "&SystemID=" + ViewState["SystemId"] + "&SubSystemID=" + ViewState["SubSystemId"] + "&SubSystemCode=" + ViewState["SubSystemCode"] + "&DeptCode=" + ViewState["DeptCode"] + "';showModal('dvCopyJobsPopUp');");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg3", msg3, true);
        }
        else
        {
            string jsAlert = "alert('Please select job/s to move.');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "jsAlert", jsAlert, true);
        }

    }


    protected Boolean isJobSelect()
    {
        // Declare data table to select the jobsId to Move and assign into a sesseion.

        DataTable dtMoveJob = new DataTable();
        DataColumn dtclm = new DataColumn("JobID");
        dtMoveJob.Columns.Add(dtclm);
        DataRow dr;

        int rowscount = gvJobs.Rows.Count;
        for (int i = 0; i < rowscount; i++)
        {
            CheckBox chk = (CheckBox)gvJobs.Rows[i].FindControl("chkJob");
            Label lblMoveJobID = (Label)gvJobs.Rows[i].FindControl("lblJobID");
            dr = dtMoveJob.NewRow();

            if (chk.Checked)
            {
                dr["JobID"] = lblMoveJobID.Text;
                dtMoveJob.Rows.Add(dr);
            }
        }

        Session["dtMoveJob"] = dtMoveJob;

        if (dtMoveJob.Rows.Count > 0)
            return true;
        else
            return false;

    }



    protected void SelChkJob_OnCheckedChanged(object sender, EventArgs e)
    {
        int rowscount = gvJobs.Rows.Count;

        for (int i = 0; i < rowscount; i++)
        {

            CheckBox chk = (CheckBox)gvJobs.Rows[i].FindControl("chkJob");
            Label lblMoveJobID = (Label)gvJobs.Rows[i].FindControl("lblJobID");

            if (SelChkJob.Checked)
            {
                chk.Checked = true;
                SelChkJob.Text = "UnCheck ALL";
            }
            else
            {
                chk.Checked = false;
                SelChkJob.Text = "Check ALL";
            }
        }
        UpdJobsGrid.Update();

    }



    protected void imgDeleteAssignLoc_click(object sender, ImageClickEventArgs e)
    {
        BLL_PMS_Library_Jobs objjobs = new BLL_PMS_Library_Jobs();
        if ((String)ViewState["CatlogueOperationMode"] == "EDIT")
        {

            if (lstcatalogLocation.SelectedValue != "")
            {
                objjobs.LibraryCatalogueLocationAssignDelete(Convert.ToInt32(Session["userid"].ToString()), Convert.ToInt32(lstcatalogLocation.SelectedValue), Convert.ToInt32(ViewState["VesselCodeEditMode"].ToString()));
                BindCatalogue();
            }

        }

        if ((String)ViewState["CatlogueOperationMode"] == "ADD")
        {
            if (lstcatalogLocation.SelectedIndex != -1)
                lstcatalogLocation.Items.RemoveAt(lstcatalogLocation.SelectedIndex);
        }

        //if (Convert.ToString(ViewState["SubCatlogueOperationMode"]) == "EDIT")
        //{
        //    objjobs.LibraryCatalogueLocationAssignDelete(Convert.ToInt32(Session["userid"].ToString()), Convert.ToInt32(lstSubCatVesselLocation.SelectedValue), Convert.ToInt32(ViewState["VesselCodeEditMode"].ToString()));
        //    BindSubCatalogue();
        //}

        //if ((String)ViewState["SubCatlogueOperationMode"] == "ADD")
        //{
        //    if (lstSubCatVesselLocation.SelectedIndex != -1)
        //        lstSubCatVesselLocation.Items.RemoveAt(lstSubCatVesselLocation.SelectedIndex);
        //}
       // UpdCatalogueEntry.Update();

    }


    protected void ImgSubSystemDelAssVslLocation_click(object sender, ImageClickEventArgs e)
    {
        BLL_PMS_Library_Jobs objjobs = new BLL_PMS_Library_Jobs();

        if (Convert.ToString(ViewState["SubCatlogueOperationMode"]) == "EDIT")
        {
            objjobs.LibraryCatalogueLocationAssignDelete(Convert.ToInt32(Session["userid"].ToString()), Convert.ToInt32(lstSubCatVesselLocation.SelectedValue), Convert.ToInt32(ViewState["VesselCodeEditMode"].ToString()));
            BindSubCatalogue();
        }

        if ((String)ViewState["SubCatlogueOperationMode"] == "ADD")
        {
            if (lstSubCatVesselLocation.SelectedIndex != -1)
                lstSubCatVesselLocation.Items.RemoveAt(lstSubCatVesselLocation.SelectedIndex);
        }

      //  UpdSubCatalogueEntry.Update();
    }



    protected void btnDivAddLocation_Click(object sender, EventArgs e)
    {
        lblErrMsg.Text = "";
        this.SetFocus("txtSearchLocation");
        ViewState["ISSYSTEMLOCATION"] = "1";
        string AssginLocmodal = String.Format("showModal('divAddLocation',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ApprmodalUser", AssginLocmodal, true);

        BindCatalogueAssignLocation();


    }

    protected void btnSubCatDivAddLocation_Click(object sender, EventArgs e)
    {

        lblErrMsg.Text = "";
        this.SetFocus("txtSearchLocation");
        ViewState["ISSYSTEMLOCATION"] = "0";
        string AssginLocmodal = String.Format("showModal('divAddLocation',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ApprmodalUser", AssginLocmodal, true);

        BindCatalogueAssignLocation();

    }

    protected void gvCatalogue_Sorting(object sender, GridViewSortEventArgs se)
    {
        ViewState["CATALOGUESORTBYCOLOUMN"] = se.SortExpression;

        if (ViewState["CATALOGUESORTDIRECTION"] != null && ViewState["CATALOGUESORTDIRECTION"].ToString() == "0")
            ViewState["CATALOGUESORTDIRECTION"] = 1;
        else
            ViewState["CATALOGUESORTDIRECTION"] = 0;

        BindCatalogue();

    }

    protected void gvSubCatalogue_Sorting(object sender, GridViewSortEventArgs se)
    {
        ViewState["SUBCATALOGUESORTBYCOLOUMN"] = se.SortExpression;

        if (ViewState["SUBCATALOGUESORTDIRECTION"] != null && ViewState["SUBCATALOGUESORTDIRECTION"].ToString() == "0")
            ViewState["SUBCATALOGUESORTDIRECTION"] = 1;
        else
            ViewState["SUBCATALOGUESORTDIRECTION"] = 0;

        BindSubCatalogue();

    }

    protected void gvJobs_Sorting(object sender, GridViewSortEventArgs se)
    {
        ViewState["ITEMSORTBYCOLOUMN"] = se.SortExpression;

        if (ViewState["ITEMSORTDIRECTION"] != null && ViewState["ITEMSORTDIRECTION"].ToString() == "0")
            ViewState["ITEMSORTDIRECTION"] = 1;
        else
            ViewState["ITEMSORTDIRECTION"] = 0;

        BindJobs();

    }

    private void SetCatalogueRowSelection()
    {
        gvCatalogue.SelectedIndex = -1;

        for (int i = 0; i < gvCatalogue.Rows.Count; i++)
        {
            if (gvCatalogue.DataKeys[i].Value.ToString().Equals(ViewState["SystemId"].ToString()))
            {
                gvCatalogue.SelectedIndex = i;
                ViewState["SystemId"] = ((Label)gvCatalogue.Rows[i].FindControl("lblSystemId")).Text;
                ViewState["SystemCode"] = ((Label)gvCatalogue.Rows[i].FindControl("lblSystemCode")).Text;
                ViewState["VesselCodeEditMode"] = ((Label)gvCatalogue.Rows[i].FindControl("lblVesselCode")).Text;
                ViewState["DeptCode"] = ((Label)gvCatalogue.Rows[i].FindControl("lblDeptCode")).Text;
                ViewState["SubSystemId"] = null;
                hdnSubSysID.Value = "";
                ViewState["JobId"] = null;


            }
        }
    }

    private void SetSubCatalogueRowSelection()
    {
        gvSubCatalogue.SelectedIndex = -1;

        for (int i = 0; i < gvSubCatalogue.Rows.Count; i++)
        {
            if (gvSubCatalogue.DataKeys[i].Value.ToString().Equals(ViewState["SubSystemId"].ToString()))
            {
                gvSubCatalogue.SelectedIndex = i;

                ViewState["SubSystemId"] = ((Label)gvSubCatalogue.Rows[i].FindControl("lblSubSystemID")).Text;
                //hdnSubSysID.Value = ((Label)gvSubCatalogue.Rows[i].FindControl("lblSubSystemID")).Text;
                ViewState["SubSystemCode"] = ((Label)gvSubCatalogue.Rows[i].FindControl("lblSubSystemCode")).Text;
                ViewState["SubSystemIdForCopyJob"] = ((Label)gvSubCatalogue.Rows[i].FindControl("lblSubSystemID")).Text;

                // ViewState["JobId"] = null;
            }
        }
    }

    private void SetJobRowSelection()
    {
        gvJobs.SelectedIndex = -1;
        for (int i = 0; i < gvJobs.Rows.Count; i++)
        {
            if (gvJobs.DataKeys[i].Value.ToString().Equals(ViewState["JobId"].ToString()))
            {
                gvJobs.SelectedIndex = i;
            }
        }
    }

    //protected void lnbHome_Click(object sender, EventArgs e)
    //{
    //    Response.Redirect("~/crew/default.aspx");
    //}


    #region  Search/Add Location popup

    public void BindCatalogueAssignLocation()
    {
        BLL_PMS_Library_Jobs objJobs = new BLL_PMS_Library_Jobs();
        int rowcount = ucCustomPagerItems.isCountRecord;

        string sortbycoloumn = (ViewState["ITEMSORTBYCOLOUMN"] == null) ? null : (ViewState["ITEMSORTBYCOLOUMN"].ToString());

        int? sortdirection = null;
        if (ViewState["ITEMSORTDIRECTION"] != null)
            sortdirection = Int32.Parse(ViewState["ITEMSORTDIRECTION"].ToString());


        DataSet ds = objJobs.LibraryCatalogueLocationAssignSearch(ViewState["SystemCode"].ToString(), Convert.ToString(ViewState["SubSystemId"]), txtSearchLocation.Text, Convert.ToInt32(ViewState["VesselCodeEditMode"].ToString()), sortbycoloumn
            , sortdirection, ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref rowcount);


        if (ucCustomPagerItems.isCountRecord == 1)
        {
            ucCustomPagerItems.CountTotalRec = rowcount.ToString();
            ucCustomPagerItems.BuildPager();
        }

        if (ds.Tables[0].Rows.Count > 0)
        {
            gvLocation.DataSource = ds.Tables[0];
            gvLocation.DataBind();
        }
        else
        {
            gvLocation.DataSource = ds.Tables[0];
            gvLocation.DataBind();
        }
    }


    protected void imgLocationSearch_Click(object sender, ImageClickEventArgs e)
    {
        string AssginLocmodal = String.Format("showModal('divAddLocation',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ApprmodalUser", AssginLocmodal, true);
        //divAddLocation.Visible = true;

        BindCatalogueAssignLocation();


    }

    public void btnDivLocationSave_Click(object sender, EventArgs e)
    {

        string AssginLocSavemodal = String.Format("showModal('divAddLocation',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AssginLocSavemodal", AssginLocSavemodal, true);


        using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
        {
            LocationData objLocDo = new LocationData();

            // In PURC_LIB_SYSTEM_PARAMETERS  table for location  'ParentType = 1'

            objLocDo.ParentType = "1";
            objLocDo.ShortCode = txtLoc_ShortCode.Text;
            objLocDo.ShortDiscription = txtLoc_Description.Text;
            objLocDo.LongDiscription = "";
            objLocDo.CurrentUser = Session["userid"].ToString();
            objLocDo.NoOfLoc = "1";
            int retVal = objTechService.SaveLocation(objLocDo);

        }

        txtLoc_ShortCode.Text = "";
        txtLoc_Description.Text = "";

        BindCatalogueAssignLocation();
    }



    public void btnDivSave_click(object sender, EventArgs e)
    {

        Boolean blnRecSel = false;

        DataTable dt = new DataTable();
        dt.Columns.Add("LocationID");
        dt.Columns.Add("LocationName");


        string systemcode = ViewState["SystemId"].ToString();
        string subsystemcode = ViewState["ISSYSTEMLOCATION"].ToString() == "0" ? Convert.ToString(ViewState["SubSystemId"]) : null;

        foreach (GridViewRow gr in gvLocation.Rows)
        {
            CheckBox chkAssignLoc = (CheckBox)gr.FindControl("chkDivAssingLoc");

            if (chkAssignLoc.Checked == true && chkAssignLoc.Enabled == true)
            {
                blnRecSel = true;
                string Locationcode = ((Label)gr.FindControl("lblDivLocationCode")).Text;
                string LocationName = ((Label)gr.FindControl("lblDivLocationName")).Text;

                string Category_Code = ((CheckBox)gr.FindControl("chkIsSpare")).Checked ? "SP" : "AC";

                BLL_PMS_Library_Jobs objJobs = new BLL_PMS_Library_Jobs();
                int retval = objJobs.LibraryCatalogueLocationAssignSave(Convert.ToInt32(Session["userid"].ToString())
                    , systemcode, subsystemcode, Convert.ToInt32(Locationcode), Convert.ToInt32(ViewState["VesselCodeEditMode"].ToString()), Category_Code);



                //AddDataTempLocation(Locationcode, LocationName, dt); 
            }
        }

        if (!blnRecSel)
        {

            lblErrMsg.Text = "Please select location/s to assign";

            string AssginLocmodal = String.Format("showModal('divAddLocation',false);");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ApprmodalUser", AssginLocmodal, true);
        }
        else
        {
            BindCatalogue();
            BindSubCatalogue();
           
         //   UpdSubCatalogueEntry.Update();
          //  UpdCatalogueEntry.Update();
          
                          
            
         
            lblErrMsg.Text = "";
            string AssginLocmodalHide = String.Format("hideModal('divAddLocation');");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", AssginLocmodalHide, true);
        }

    }

    public void btnDivCancel_click(object sender, EventArgs e)
    {


        string AssginLocmodalHide = String.Format("hideModal('divAddLocation');");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", AssginLocmodalHide, true);

        //UpdCatalogueEntry.Update();

    }

    public void AddDataTempLocation(string LocationID, string LocationName, DataTable dt)
    {

        DataRow dr;
        dr = dt.NewRow();

        dr["LocationID"] = LocationID;
        dr["LocationName"] = LocationName;

        dt.Rows.Add(dr);

        ViewState["TempDtLocation"] = dt;

    }

    protected void gvLocation_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {

    }

    protected void gvLocation_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            if (((CheckBox)e.Row.FindControl("chkDivAssingLoc")).Checked)
            {
                ((Label)e.Row.FindControl("lblDivLocationCode")).ForeColor = System.Drawing.Color.Silver;
                ((Label)e.Row.FindControl("lblDivLocationCode")).Font.Bold = true;

                ((Label)e.Row.FindControl("lblDivShortCode")).ForeColor = System.Drawing.Color.Silver;
                ((Label)e.Row.FindControl("lblDivShortCode")).Font.Bold = true;

                ((Label)e.Row.FindControl("lblDivLocationName")).ForeColor = System.Drawing.Color.Silver;
                ((Label)e.Row.FindControl("lblDivLocationName")).Font.Bold = true;

                ((Label)e.Row.FindControl("lblDivMachinery")).ForeColor = System.Drawing.Color.Silver;
                ((Label)e.Row.FindControl("lblDivMachinery")).Font.Bold = true;
            }
        }
    }

    protected void gvLocation_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }

    protected void gvLocation_Sorting(object sender, GridViewSortEventArgs e)
    {


    }


    #endregion


    private string ScriptSaveCatalogGridScroll()
    {
        //HiddenField1.Value is set in the getScrollPosition() javascript function
        //with the onscroll event on the divHolder in the markup
        StringBuilder strScript = new StringBuilder();

        //strScript.Append("<script language=javascript>");
        strScript.Append("var obj = document.getElementById('DivCatalogGridHolder');");
        strScript.Append("var objHidden = document.getElementById('" + hdfCatelogScrollPos.ClientID + "');");

        strScript.Append("objHidden.value");
        strScript.Append(" = " + hdfCatelogScrollPos.Value + ";");

        strScript.Append("obj.scrollTop=objHidden.value;");
        strScript.Append("objHidden.value = obj.scrollTop;");
        //strScript.Append("alert(obj.scrollTop);");
        //strScript.Append("</script>");
        return strScript.ToString();
    }

    private string ScriptSaveSubCatalogGridScroll()
    {
        //HiddenField1.Value is set in the getScrollPosition() javascript function
        //with the onscroll event on the divHolder in the markup
        StringBuilder strScript = new StringBuilder();

        //strScript.Append("<script language=javascript>");
        strScript.Append("var obj = document.getElementById('DivSubCatalogGridHolder');");
        strScript.Append("var objHidden = document.getElementById('" + hdfSubCatelogScrollPos.ClientID + "');");

        strScript.Append("objHidden.value");
        strScript.Append(" = " + hdfSubCatelogScrollPos.Value + ";");

        strScript.Append("obj.scrollTop=objHidden.value;");
        strScript.Append("objHidden.value = obj.scrollTop;");
        //strScript.Append("alert(obj.scrollTop);");
        //strScript.Append("</script>");
        return strScript.ToString();
    }

    protected void btnHiddenSubmit_Click(object sender, EventArgs e)
    {

        BindJobs();
        //BindPmsJobAttachment();


        SelChkJob.Checked = false;
        SelChkJob.Text = "Check ALL";


        UpdJobEntry.Update();
        UpdJobsGrid.Update();
    }

    protected void btnMakerHiddenSubmit_Click(object sender, EventArgs e)
    {
        BindSupplierDetails();
        //UpdCatalogueEntry.Update();
    }




    /* Attachment on job*/





    protected void imgbtnDeleteAssembly_Click(object s, CommandEventArgs e)
    {

        BLL_PMS_Library_Jobs objjobs = new BLL_PMS_Library_Jobs();
        int indel = objjobs.LibraryDeleteJobInstructionAttachment(e.CommandArgument.ToString(), Convert.ToInt32(Session["userid"].ToString()));

        BindPmsJobAttachment();

    }


    protected void BindPmsJobAttachment()
    {

        BLL_PMS_Library_Jobs objjobs = new BLL_PMS_Library_Jobs();
        DataTable dt = objjobs.LibraryGetJobInstructionAttachment(UDFLib.ConvertToInteger(ViewState["VesselCodeEditMode"].ToString()), UDFLib.ConvertToInteger(ViewState["JobId"].ToString()));

        gvPMSJobAttachment.DataSource = dt;
        gvPMSJobAttachment.DataBind();

    }


    protected void btnLoadFiles_Click(object sender, EventArgs e)
    {
        //LoadFiles(null, null);
    }

    protected void btnSystemLinkRunHourSettings_Click(object sender, EventArgs e)
    {


        if (!string.IsNullOrEmpty(txtCatalogueCode.Text))
        {
            BLL_PMS_Library_Jobs objjobs = new BLL_PMS_Library_Jobs();
            DataTable dtSystemExists = objjobs.PMS_Get_CheckIfSystemExits(int.Parse(txtCatalogueCode.Text.Trim()));
            if (dtSystemExists != null && dtSystemExists.Rows.Count > 0)
                Response.Redirect("CopyRunHour.aspx?sourceid=" + txtCatalogueCode.Text.Trim() + "&&systemtype=S");
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "LinkSystemRHrs", "LinkSystemRHrs();", true);
            }

        }
    }
    protected void btnSubSystemLinkRunHourSettings_Click(object sender, EventArgs e)
    {

        if (!string.IsNullOrEmpty(hdnSubSysID.Value))
        {
            BLL_PMS_Library_Jobs objjobs = new BLL_PMS_Library_Jobs();
            DataTable dtSubSystemExists = objjobs.PMS_Get_CheckIfSubSystemExits(hdnSubSysID.Value);
            if (dtSubSystemExists != null && dtSubSystemExists.Rows.Count > 0)
                Response.Redirect("CopyRunHour.aspx?sourceid=" + hdnSubSysID.Value + "&&systemtype=SS");
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "LinkSubSystemRHrs", "LinkSubSystemRHrs();", true);
            }
        }
    }

    protected void lstFrequency_SelectedIndexChanged(object sender, EventArgs e)
    {
        int SystemID = 0;
        int SubSystemID = 0;
        if (!string.IsNullOrEmpty(txtCatalogueCode.Text))
            SystemID = int.Parse(txtCatalogueCode.Text.Trim());
        if (!string.IsNullOrEmpty(ViewState["SubSystemId"].ToString()))
            SubSystemID = int.Parse(ViewState["SubSystemId"].ToString());

        if (SystemID > 0 && SubSystemID > 0)
        {
            BLL_PMS_Library_Jobs objjobs = new BLL_PMS_Library_Jobs();
            int Result = objjobs.PMS_GET_IsJobRunHourBased(SystemID, SubSystemID, int.Parse(DDLVessel.SelectedValue));
            if (lstFrequency.SelectedValue == "2486")
            {
                if (Result == 0)
                {

                    if (txtSubCatalogueName.Text.Trim().ToLower() != "general")
                    {
                        lstFrequency.SelectedValue = "2485";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "RunHourFailed", "RunHourFailed();", true);
                    }
                    if (txtSubCatalogueName.Text.Trim().ToLower() == "general" && chkRunHourS.Checked == false)
                    {
                        lstFrequency.SelectedValue = "2485";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "RunHourFailed", "RunHourFailed();", true);
                    }
                }
            }
        }
    }
    protected void chkRunHourSS_CheckedChanged(object sender, EventArgs e)
    {

        if (chkRunHourSS.Checked == true)
        {
            if (chkRunHourS.Checked == true)
                chkCopyRunHourSS.Enabled = true;
        }
        else
        {
            chkCopyRunHourSS.Enabled = false;
            chkCopyRunHourSS.Checked = false;
        }
    }

    
}