using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Infrastructure;
using SMS.Business.PURC;
using SMS.Business.PMS;
using System.Text;
using System.Data;

public partial class Technical_PMS_PMSCopyJobs : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {

            /* To values settings */

            if (Request.QueryString["VesselCode"] != null && Request.QueryString["VesselCode"] != "")
            {
                ViewState["ToVesselCode"] = Request.QueryString["VesselCode"].ToString();
                BindToVesselDLL();
                DDLToVessel.SelectedValue = Request.QueryString["VesselCode"].ToString();
            }
            else
            {
                ViewState["ToVesselCode"] = 0;
                BindToVesselDLL();
            }


            if (Request.QueryString["DeptCode"] != null && Request.QueryString["DeptCode"] != "")
            {
                ViewState["ToDeptCode"] = Request.QueryString["DeptCode"].ToString();
                BindToDepartmentDLL();
                DDLToDept.SelectedValue = Request.QueryString["DeptCode"].ToString();
            }
            else
            {
                ViewState["ToDeptCode"] = null;
                BindToDepartmentDLL();
            }


            if (Request.QueryString["SystemCode"] != null && Request.QueryString["SystemCode"] != "")
            {
                ViewState["ToSystemCode"] = Request.QueryString["SystemCode"].ToString();
                BindToCatalogueDLL();
                DDLToSystem.SelectedValue = Request.QueryString["SystemCode"].ToString();
            }
            else
            {
                ViewState["ToSystemCode"] = null;
                BindToCatalogueDLL();
            }



            if (Request.QueryString["SubSystemID"] != null && Request.QueryString["SubSystemID"] != "")
            {
                BindToSubCatalogueDLL();
                DDLToSubsystem.SelectedValue = Request.QueryString["SubSystemID"].ToString();
            }
            else
            {
                BindToSubCatalogueDLL();
            }




            BindFromVesselDLL();


            if (Session["PreValueFromVesselCode"] != null)
            {
                DDLFromVessel.SelectedValue = Session["PreValueFromVesselCode"].ToString();
            }
            else
            {
                /* By Default TASANEE (vesselid =4) should select (Template Vessel) */
                DDLFromVessel.SelectedValue = "4";
            }

            


            if (Request.QueryString["DeptCode"] != null && Request.QueryString["DeptCode"] != "")
            {
                ViewState["FromDeptCode"] = Request.QueryString["DeptCode"].ToString();
                BindFromDepartmentDLL();

             


            }
            else
            {
                ViewState["FromDeptCode"] = null;
                BindFromDepartmentDLL();

            }

            BindFromCatalogueDLL();

            ViewState["FromSystemCode"] = null;
            GetJobCount();

            lblMsg.Text = "";




        }

        lblMsg.Text = "";


    }


    private void BindFromVesselDLL()
    {
        BLL_Infra_VesselLib objVsl = new BLL_Infra_VesselLib();
        DataTable dtVessel = objVsl.Get_VesselList(0, 0, Convert.ToInt32(Session["USERCOMPANYID"].ToString()), "", Convert.ToInt32(Session["USERCOMPANYID"].ToString()));
        DDLFromVessel.Items.Clear();
        DDLFromVessel.DataSource = dtVessel;
        DDLFromVessel.DataTextField = "Vessel_name";
        DDLFromVessel.DataValueField = "Vessel_id";
        DDLFromVessel.DataBind();
        ListItem li = new ListItem("--SELECT ALL--", "0");
        DDLFromVessel.Items.Insert(0, li);
    }

    private void BindToVesselDLL()
    {
        BLL_Infra_VesselLib objVsl = new BLL_Infra_VesselLib();
        DataTable dtVessel = objVsl.Get_VesselList(0, 0, Convert.ToInt32(Session["USERCOMPANYID"].ToString()), "", Convert.ToInt32(Session["USERCOMPANYID"].ToString()));
        DDLToVessel.Items.Clear();
        DDLToVessel.DataSource = dtVessel;
        DDLToVessel.DataTextField = "Vessel_name";
        DDLToVessel.DataValueField = "Vessel_id";
        DDLToVessel.DataBind();
        ListItem li = new ListItem("--SELECT ALL--", "0");
        DDLToVessel.Items.Insert(0, li);
    }

    private void BindFromDepartmentDLL()
    {
        try
        {
            using (BLL_PURC_Purchase objBLLPURC = new BLL_PURC_Purchase())
            {
                DataTable dtDepartment = new DataTable();
                dtDepartment = objBLLPURC.SelectDepartment();
                dtDepartment.DefaultView.RowFilter = "Form_Type='SP'";

                DDLFromDept.Items.Clear();
                DDLFromDept.DataSource = dtDepartment;
                DDLFromDept.AppendDataBoundItems = true;
                DDLFromDept.DataTextField = "Name_Dept";
                DDLFromDept.DataValueField = "Code";
                DDLFromDept.DataBind();
                DDLFromDept.Items.Insert(0, new ListItem("--SELECT ALL--", "0"));

            }

        }
        catch (Exception ex)
        {

        }

    }

    private void BindToDepartmentDLL()
    {
        try
        {
            using (BLL_PURC_Purchase objBLLPURC = new BLL_PURC_Purchase())
            {
                DataTable dtDepartment = new DataTable();
                dtDepartment = objBLLPURC.SelectDepartment();
                dtDepartment.DefaultView.RowFilter = "Form_Type='SP'";

                DDLToDept.Items.Clear();
                DDLToDept.DataSource = dtDepartment;
                DDLToDept.AppendDataBoundItems = true;
                DDLToDept.DataTextField = "Name_Dept";
                DDLToDept.DataValueField = "Code";
                DDLToDept.DataBind();
                DDLToDept.Items.Insert(0, new ListItem("--SELECT ALL--", "0"));

            }

        }
        catch (Exception ex)
        {

        }

    }

    private void BindFromCatalogueDLL()
    {

        using (BLL_PURC_Purchase objBLLPurc = new BLL_PURC_Purchase())
        {

            string vesselcode = (ViewState["FromVesselCode"] == null) ? null : (ViewState["FromVesselCode"].ToString());


            string deptcode = (ViewState["FromDeptCode"] == null) ? null : (ViewState["FromDeptCode"].ToString());

            DataSet ds = objBLLPurc.LibraryCatalogueSearch(null, txtFromMachinery.Text.Trim(), "SP", DDLFromDept.SelectedValue, Int32.Parse(DDLFromVessel.SelectedValue), "", 1, null, null, 1, 500, 1);

            if (ds.Tables[0].Rows.Count > 0)
            {
                DDLFromSystem.Items.Clear();
                DDLFromSystem.DataSource = ds.Tables[0];
                DDLFromSystem.DataTextField = "System_Description";
                DDLFromSystem.DataValueField = "System_Code";
                DDLFromSystem.DataBind();
                ListItem li = new ListItem("--SELECT ALL--", "0");
                DDLFromSystem.Items.Insert(0, li);
            }
        }
    }

    private void BindToCatalogueDLL()
    {

        using (BLL_PURC_Purchase objBLLPurc = new BLL_PURC_Purchase())
        {

            string vesselcode = (ViewState["ToVesselCode"] == null) ? null : (ViewState["ToVesselCode"].ToString());

            string deptcode = (ViewState["ToDeptCode"] == null) ? null : (ViewState["ToDeptCode"].ToString());

            DataSet ds = objBLLPurc.LibraryCatalogueSearch(null, txtToMachinery.Text.Trim(), "SP", deptcode, Int32.Parse(DDLToVessel.SelectedValue), "", 1, null, null, 1, 500, 1);

            if (ds.Tables[0].Rows.Count > 0)
            {
                DDLToSystem.Items.Clear();
                DDLToSystem.DataSource = ds.Tables[0];
                DDLToSystem.DataTextField = "System_Description";
                DDLToSystem.DataValueField = "System_Code";
                DDLToSystem.DataBind();
                ListItem li = new ListItem("--SELECT ALL--", "0");
                DDLToSystem.Items.Insert(0, li);
            }
        }
    }

    public void BindToSubCatalogueDLL()
    {

        using (BLL_PURC_Purchase objBLLPurc = new BLL_PURC_Purchase())
        {


            DataSet ds = objBLLPurc.LibrarySubCatalogueSearch(DDLToSystem.SelectedValue, "", null, 1, null, null, 1, 1000, 1);

            if (ds.Tables[0].Rows.Count > 0)
            {
                DDLToSubsystem.Items.Clear();
                DDLToSubsystem.DataSource = ds.Tables[0];
                DDLToSubsystem.DataTextField = "Subsystem_Description";
                DDLToSubsystem.DataValueField = "ID";
                DDLToSubsystem.DataBind();
                ListItem li = new ListItem("--SELECT ALL--", "0");
                DDLToSubsystem.Items.Insert(0, li);
            }
            ViewState["dtToSubsystem"] = ds.Tables[0];

        }
    }

    public void BindFromSubCatalogueDLL()
    {

        using (BLL_PURC_Purchase objBLLPurc = new BLL_PURC_Purchase())
        {

            string systemcode = (ViewState["FromSystemCode"] == null) ? null : (ViewState["FromSystemCode"].ToString());

            DataSet ds = objBLLPurc.LibrarySubCatalogueSearch(systemcode, "", null, 1, null, null, 1, 1000, 1);

            if (ds.Tables[0].Rows.Count > 0)
            {
                DDLFromSubsystem.Items.Clear();
                DDLFromSubsystem.DataSource = ds.Tables[0];
                DDLFromSubsystem.DataTextField = "Subsystem_Description";
                DDLFromSubsystem.DataValueField = "ID";
                DDLFromSubsystem.DataBind();
                ListItem li = new ListItem("--SELECT ALL--", "0");
                DDLFromSubsystem.Items.Insert(0, li);
            }

            ViewState["dtFromSubsystem"] = ds.Tables[0];
        }
    }

    protected void DDLFromVessel_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["FromVesselCode"] = DDLFromVessel.SelectedValue.ToString();

        Session["PreValueFromVesselCode"] = DDLFromVessel.SelectedValue.ToString();

        BindFromCatalogueDLL();
        DDLFromDept.SelectedValue = "0";
        DDLFromSystem.SelectedValue = "0";
        DDLFromSubsystem.SelectedValue = "0";
        txtFromMachinery.Text = "";
        lblMsg.Text = "";

    }

    protected void DDLToVessel_SelectedIndexChanged(object sender, EventArgs e)
    {

        ViewState["ToVesselCode"] = DDLToVessel.SelectedValue.ToString();
        BindToCatalogueDLL();
        DDLToDept.SelectedValue = "0";
        DDLToSystem.SelectedValue = "0";
        DDLToSubsystem.SelectedValue = "0";
        txtToMachinery.Text = "";
        lblMsg.Text = "";

    }

    protected void DDLFromDept_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["FromDeptCode"] = DDLFromDept.SelectedValue;
      

        BindFromCatalogueDLL();

        DDLFromSystem.SelectedValue = "0";
        DDLFromSubsystem.SelectedValue = "0";
        txtFromMachinery.Text = "";
        lblMsg.Text = "";
    }

    protected void DDLToDept_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["ToDeptCode"] = DDLToDept.SelectedValue;
        BindToCatalogueDLL();

        DDLToSystem.SelectedValue = "0";
        DDLToSubsystem.SelectedValue = "0";
        txtToMachinery.Text = "";
        lblMsg.Text = "";
    }

    protected void DDLFromSystem_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["FromSystemCode"] = DDLFromSystem.SelectedValue;
        BindFromSubCatalogueDLL();

        DDLFromSubsystem.SelectedValue = "0";
        txtFromMachinery.Text = "";
        lblMsg.Text = "";

        GetJobCount();


    }

    protected void DDLToSystem_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["ToSystemCode"] = DDLToSystem.SelectedValue;
        BindToSubCatalogueDLL();
        DDLToSubsystem.SelectedValue = "0";
        txtToMachinery.Text = "";
        lblMsg.Text = "";
        GetJobCount();


    }

    protected void DDLFromSubsystem_SelectedIndexChanged(object sender, EventArgs e)
    {

        GetJobCount();

    }

    protected void DDLToSubsystem_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetJobCount();
    }

    protected void GetJobCount()
    {

        //From Job Count
        DataTable dtToSection = GetJobToCopy(true);
        lblToJobCount.Text = dtToSection.Rows.Count.ToString();


        //To Job Count

        DataTable dtFromSection = GetJobToCopy(false);
        lblFromJobCount.Text = dtFromSection.Rows.Count.ToString();


    }

    protected DataTable GetJobToCopy(Boolean blnOverWriteFlag)
    {

        BLL_PMS_Library_Jobs objJobs = new BLL_PMS_Library_Jobs();
        DataSet ds = new DataSet();

        if (blnOverWriteFlag)
        {
            int? isactivestatus = 1;
            ds = objJobs.LibraryJobGetToCopy(DDLToSystem.SelectedValue, UDFLib.ConvertIntegerToNull(DDLToSubsystem.SelectedValue), UDFLib.ConvertIntegerToNull(DDLToVessel.SelectedValue), isactivestatus);
        }
        else
        {
            int? isactivestatus = 1;
            ds = objJobs.LibraryJobGetToCopy(DDLFromSystem.SelectedValue, UDFLib.ConvertIntegerToNull(DDLFromSubsystem.SelectedValue), UDFLib.ConvertIntegerToNull(DDLFromVessel.SelectedValue), isactivestatus);
        }

        return ds.Tables[0];

    }

    protected void btnOverwrite_Click(object sender, EventArgs e)
    {



        BLL_PMS_Library_Jobs objJobs = new BLL_PMS_Library_Jobs();

        /* 1. Get All the existing job  pass overwrite flag to 'ture' for fatching exsisting job fro  To { selected vessel , system ,subsystem } criteria */
        DataTable dtExistingJob = GetJobToCopy(true);

        /* 2. Delete all the job Office :: Active status would be 0    Vessel :: Need to Send the delete Query to Delete paranently from the ship */

        foreach (DataRow dr in dtExistingJob.Rows)
        {
            objJobs.LibraryJobOverWrite(Convert.ToInt32(Session["userid"].ToString()), Convert.ToInt32(dr["ID"].ToString()));
        }

        /*3. Get All the Jobs that need to copy from {selected vessel , system ,subsystem }  To { selected vessel , system ,subsystem }  pass overwrite flag 'false' */

        DataTable dtJobsToCopy = GetJobToCopy(false);

        /*4. Insert the data into TEC_LIB_JOBS table */
        CopyJobsFromVesselToVessel(dtJobsToCopy, "Overwrite");


    }

    protected void btnAppend_Click(object sender, EventArgs e)
    {

        /* 1. Get All the existing job  pass overwrite flag to 'ture' for fatching exsisting record */
        DataTable dtJobsToCopy = GetJobToCopy(false);

        /* 2. Insert the data into TEC_LIB_JOBS table */
        CopyJobsFromVesselToVessel(dtJobsToCopy, "Appended");





    }
   
    /// <summary>
    /// Copy jobs from vessel to vessel : changes done by reshma added parameter Is_RaMandatory & Is_RAApproval
    /// </summary>
    /// <param name="dtJobsToCopy"> copy jobs to datatable</param>
    /// <param name="action"></param>
    protected void CopyJobsFromVesselToVessel(DataTable dtJobsToCopy, string action)
    {

        BLL_PMS_Library_Jobs objJobs = new BLL_PMS_Library_Jobs();
        BLL_PURC_Purchase objBLLPurc = new BLL_PURC_Purchase();

        string systemcode = (ViewState["ToSystemCode"] == null) ? null : (ViewState["ToSystemCode"].ToString());

        DataSet ds = objBLLPurc.LibrarySubCatalogueSearch(DDLToSystem.SelectedValue, "", null, null, null, null, 1, 1000, 1);
        DataTable dtToSubsystem = ds.Tables[0];

        int count = 0;
               
        foreach (DataRow dr in dtJobsToCopy.Rows)
        {
            int? IsSafetyAlarm = null;
            int? IsCalibration = null;
            int? Is_RAMandatory = null;                 
            int? Is_RAApproval = null;                  
            if (DDLFromSubsystem.SelectedValue != "0")
            {
                
                if (!string.IsNullOrEmpty(Convert.ToString(dr["IsSafetyAlarm"])))
                    IsSafetyAlarm = int.Parse(Convert.ToString(dr["IsSafetyAlarm"]));
                if (!string.IsNullOrEmpty(Convert.ToString(dr["IsCalibration"])))
                    IsCalibration = int.Parse(Convert.ToString(dr["IsCalibration"]));

                if (!string.IsNullOrEmpty(Convert.ToString(dr["IsRAMandatory"])))                
                    Is_RAMandatory = int.Parse(Convert.ToString(dr["IsRAMandatory"]));

                if (!string.IsNullOrEmpty(Convert.ToString(dr["IsRAApproval"])))                 
                    Is_RAApproval = int.Parse(Convert.ToString(dr["IsRAApproval"]));

                objJobs.LibraryJobSaveFromOtherVessel(Convert.ToInt32(Session["userid"].ToString())
                     , DDLToSystem.SelectedValue
                     , Convert.ToInt32(DDLToSubsystem.SelectedValue)
                     , Convert.ToInt32(DDLToVessel.SelectedValue)
                     , Convert.ToInt32(dr["Department_ID"].ToString()), Convert.ToInt32(dr["Rank_ID"].ToString()), dr["Job_Title"].ToString()
                     , dr["Job_Description"].ToString(), Convert.ToInt32(dr["Frequency"].ToString()), Convert.ToInt32(dr["Frequency_Type"].ToString())
                     , Convert.ToInt32(dr["CMS"].ToString()), Convert.ToInt32(dr["Critical"].ToString()), IsSafetyAlarm, IsCalibration, Is_RAMandatory, Is_RAApproval);  

                

                count++;

            }
            else
            {

                foreach (DataRow drtosubsystem in dtToSubsystem.Rows)
                {
                    if (!string.IsNullOrEmpty(Convert.ToString(dr["IsSafetyAlarm"])))
                        IsSafetyAlarm = int.Parse(Convert.ToString(dr["IsSafetyAlarm"]));
                    if (!string.IsNullOrEmpty(Convert.ToString(dr["IsCalibration"])))
                        IsCalibration = int.Parse(Convert.ToString(dr["IsCalibration"]));

                    if (!string.IsNullOrEmpty(Convert.ToString(dr["IsRAMandatory"])))             
                        Is_RAMandatory = int.Parse(Convert.ToString(dr["IsRAMandatory"]));

                    if (!string.IsNullOrEmpty(Convert.ToString(dr["IsRAApproval"])))               
                        Is_RAApproval = int.Parse(Convert.ToString(dr["IsRAApproval"]));

                    if (dr["Subsystem_Code"].ToString() == drtosubsystem["Subsystem_Code"].ToString())
                    {

                        objJobs.LibraryJobSaveFromOtherVessel(Convert.ToInt32(Session["userid"].ToString())
                             , DDLToSystem.SelectedValue
                             , Convert.ToInt32(drtosubsystem["ID"].ToString())
                             , Convert.ToInt32(DDLToVessel.SelectedValue)
                             , Convert.ToInt32(dr["Department_ID"].ToString()), Convert.ToInt32(dr["Rank_ID"].ToString()), dr["Job_Title"].ToString()
                             , dr["Job_Description"].ToString(), Convert.ToInt32(dr["Frequency"].ToString()), Convert.ToInt32(dr["Frequency_Type"].ToString())
                             , Convert.ToInt32(dr["CMS"].ToString()), Convert.ToInt32(dr["Critical"].ToString()), IsSafetyAlarm, IsCalibration, Is_RAMandatory, Is_RAApproval);           

                        count++;
                    }
                }

            }

        }

        String script = String.Format("parent.RefreshFromchild();parent.hideModal('dvCopyJobsPopUp');");

        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", script, true);

        //  String script = String.Format("alert('" + count + " : Jobs has been " + action + "  sucessfully.');parent.RefreshFromchild();parent.hideModal('dvCopyJobsPopUp');");

    }

    protected void imgFromMachinerySearch_Click(object sender, ImageClickEventArgs e)
    {
        BindFromCatalogueDLL();
    }

    protected void imgToMachinerySearch_Click(object sender, ImageClickEventArgs e)
    {
        BindToCatalogueDLL();
    }



}