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


public partial class Technical_PMS_PMSMoveJobs : System.Web.UI.Page
{

    public BLL_PMS_Library_Jobs objJobs = new BLL_PMS_Library_Jobs();
    public BLL_PURC_Purchase objBLLPurc = new BLL_PURC_Purchase();


    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {

            /* To values settings */

            if (Request.QueryString["VesselCode"] != null && Request.QueryString["VesselCode"] != "")
            {


                BindToVesselDLL();
                DDLToVessel.SelectedValue = Request.QueryString["VesselCode"].ToString();


                BindFromVesselDLL();
                DDLFromVessel.SelectedValue = Request.QueryString["VesselCode"].ToString();

            }
            else
            {

                BindToVesselDLL();
            }



            if (Request.QueryString["DeptCode"] != null && Request.QueryString["DeptCode"] != "")
            {
                BindToDepartmentDLL();
                DDLToDept.SelectedValue = Request.QueryString["DeptCode"].ToString();

                BindFromDepartmentDLL();
                DDLFromDept.SelectedValue = Request.QueryString["DeptCode"].ToString();
            }
            else
            {
                BindToDepartmentDLL();
                BindFromDepartmentDLL();
            }


            if (Request.QueryString["SystemCode"] != null && Request.QueryString["SystemCode"] != "")
            {
                BindFromCatalogueDLL();
                DDLFromSystem.SelectedValue = Request.QueryString["SystemCode"].ToString();
                BindFromSubCatalogueDLL();

                BindToCatalogueDLL();
                BindToSubCatalogueDLL();
            }
            else
            {
                BindToCatalogueDLL();
                BindFromCatalogueDLL();

                BindToSubCatalogueDLL();
                BindFromSubCatalogueDLL();
            }


            if (Request.QueryString["SubSystemID"] != null && Request.QueryString["SubSystemID"] != "")
            {
                BindToSubCatalogueDLL();

                BindFromSubCatalogueDLL();
                DDLFromSubsystem.SelectedValue = Request.QueryString["SubSystemID"].ToString();
            }
            else
            {
                BindToSubCatalogueDLL();
                BindFromSubCatalogueDLL();
            }

            GetJobCountFrom();
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
            DataSet ds = objBLLPurc.LibraryCatalogueSearch(null, txtToMachinery.Text.Trim(), "SP", DDLToDept.SelectedValue, Int32.Parse(DDLToVessel.SelectedValue), "", 1, null, null, 1, 500, 1);

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


            DataSet ds = objBLLPurc.LibrarySubCatalogueSearch(DDLToSystem.SelectedValue, "", null, null, null, null, 1, 1000, 1);

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
        }
    }

    public void BindFromSubCatalogueDLL()
    {

        using (BLL_PURC_Purchase objBLLPurc = new BLL_PURC_Purchase())
        {
            DataSet ds = objBLLPurc.LibrarySubCatalogueSearch(DDLFromSystem.SelectedValue, "", null, null, null, null, 1, 1000, 1);

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
        }
    }

    protected void DDLFromDept_SelectedIndexChanged(object sender, EventArgs e)
    {

        BindFromCatalogueDLL();

        DDLFromSystem.SelectedValue = "0";
        DDLFromSubsystem.SelectedValue = "0";
        txtFromMachinery.Text = "";
        lblMsg.Text = "";
    }

    protected void DDLToDept_SelectedIndexChanged(object sender, EventArgs e)
    {

        BindToCatalogueDLL();

        DDLToSystem.SelectedValue = "0";
        DDLToSubsystem.SelectedValue = "0";
        txtToMachinery.Text = "";
        lblMsg.Text = "";
    }

    protected void DDLFromSystem_SelectedIndexChanged(object sender, EventArgs e)
    {

        BindFromSubCatalogueDLL();

        DDLFromSubsystem.SelectedValue = "0";
        txtFromMachinery.Text = "";
        lblMsg.Text = "";

        GetJobCountFrom();


    }

    protected void DDLToSystem_SelectedIndexChanged(object sender, EventArgs e)
    {

        BindToSubCatalogueDLL();
        DDLToSubsystem.SelectedValue = "0";
        txtToMachinery.Text = "";
        lblMsg.Text = "";

        GetJobCountTo();

    }

    protected void DDLFromSubsystem_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetJobCountFrom();
    }

    protected void DDLToSubsystem_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetJobCountTo();
    }


    protected DataTable GetJobCountTo()
    {

        int? isactivestatus = 1;
        DataSet ds = objJobs.LibraryJobGetToMove(UDFLib.ConvertStringToNull(DDLToSystem.SelectedValue), UDFLib.ConvertIntegerToNull(DDLToSubsystem.SelectedValue)
            , UDFLib.ConvertIntegerToNull(DDLToVessel.SelectedValue), isactivestatus);

        lblToJobCount.Text = ds.Tables[0].Rows.Count.ToString();
        return ds.Tables[0];

    }


    protected DataTable GetJobCountFrom()
    {
        int? isactivestatus = 1;
        DataSet ds = objJobs.LibraryJobGetToMove(UDFLib.ConvertStringToNull(DDLFromSystem.SelectedValue), UDFLib.ConvertIntegerToNull(DDLFromSubsystem.SelectedValue)
             , UDFLib.ConvertIntegerToNull(DDLFromVessel.SelectedValue), isactivestatus);

        lblFromJobCount.Text = ds.Tables[0].Rows.Count.ToString();
        return ds.Tables[0];

    }



    protected void MoveJobsToOtherSubsystem(DataTable dtJobsToMove, string action)
    {


        foreach (DataRow dr in dtJobsToMove.Rows)
        {

            objJobs.LibraryJobMoveFromOtherSubSystem(Convert.ToInt32(Session["userid"].ToString()), DDLToSystem.SelectedValue, Convert.ToInt32(DDLToSubsystem.SelectedValue), Convert.ToInt32(dr.ItemArray[0].ToString()));
        }

        String script = String.Format("parent.RefreshFromchild();parent.hideModal('dvMoveJobsPopUp'); "); /*dvCopyJobsPopUp is replaced with dvMoveJobsPopUp*/
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", script, true);
    }


    protected void btnMoveJob_Click(object sender, EventArgs e)
    {
        /* 1. Get All the job  to move to other Subsystem */

        DataTable dtMoveJob = (DataTable)Session["dtMoveJob"];

        /* 2. Update Table TEC_LIB_JOBS and  TEC_DTL_JOBS_HISTORY table  to update the sytem ID and subsystem ID */

        MoveJobsToOtherSubsystem(dtMoveJob, "");
    }


    protected void btnSelectSystem_Click(object sender, ImageClickEventArgs e)
    {

        DDLToSystem.SelectedValue = DDLFromSystem.SelectedValue;
        BindToSubCatalogueDLL();
        UpdPnlTo.Update();

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