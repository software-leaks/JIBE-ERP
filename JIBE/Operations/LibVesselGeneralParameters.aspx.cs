using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.OPS.Operations;
using System.Data;
using SMS.Business.Infrastructure;
using SMS.Data.OPS.Operations;

public partial class Operations_LibVesselGeneralParameters : System.Web.UI.Page
{
    BLL_OPS_VesselParameters objBLLVesselParameter = new BLL_OPS_VesselParameters();
    DAL_OPS_VesselParameters objVesselParameter = new DAL_OPS_VesselParameters();
    BLL_Infra_VesselLib objVessel = new BLL_Infra_VesselLib();
    public Boolean uaEditFlag = false;
    public Boolean uaDeleteFlage = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Load_FleetList();
            Load_VesselList();
            LoadVessel();
            BindVesselParameters();
        }
    }

    public void Load_FleetList()
    {
        int UserCompanyID = UDFLib.ConvertToInteger(Session["USERCOMPANYID"].ToString());
        ddlFleet.DataSource = objVessel.GetFleetList(UserCompanyID);
        ddlFleet.DataTextField = "NAME";
        ddlFleet.DataValueField = "CODE";
        ddlFleet.DataBind();
        ddlFleet.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));
    }
    public void Load_VesselList()
    {

        int Fleet_ID = int.Parse(ddlFleet.SelectedValue);
        int UserCompanyID = UDFLib.ConvertToInteger(Session["USERCOMPANYID"].ToString());

        ddlVesselList.DataSource = objVessel.Get_VesselList(Fleet_ID, 0, UserCompanyID, "", UserCompanyID);

        ddlVesselList.DataTextField = "VESSEL_NAME";
        ddlVesselList.DataValueField = "VESSEL_ID";
        ddlVesselList.DataBind();
        ddlVesselList.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));
    }

    public void LoadVessel()
    {
        int Fleet_ID = int.Parse(ddlFleet.SelectedValue);
        int UserCompanyID = UDFLib.ConvertToInteger(Session["USERCOMPANYID"].ToString());

        ddlVessel.DataSource = objVessel.Get_VesselList(Fleet_ID, 0, UserCompanyID, "", UserCompanyID);

        ddlVessel.DataTextField = "VESSEL_NAME";
        ddlVessel.DataValueField = "VESSEL_ID";
        ddlVessel.DataBind();
        ddlVessel.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));
    }




    protected void btnSave_Click(object sender, EventArgs e)
    {
        string js;
        if (!string.IsNullOrEmpty(Convert.ToString(HiddenFlag.Value)))
        {
            int responseid = objBLLVesselParameter.UPD_VesselParameter_DL(Convert.ToInt32(HiddenFlag.Value), int.Parse(ddlVessel.SelectedValue), decimal.Parse(txtPropellorPitch.Text.Trim()), decimal.Parse(txtbxQmcr.Text.Trim()), decimal.Parse(txtbxRPM_Max.Text.Trim()), decimal.Parse(txtMCR_Power.Text.Trim()), decimal.Parse(txtMCR_Power_Engine.Text.Trim()), decimal.Parse(txtSFOC.Text.Trim()), decimal.Parse(txtCylOil.Text.Trim()), Convert.ToInt32(Session["USERID"].ToString()));
            if (responseid > 0)
            {
                js = "alert('Vessel Parameters updated successfully.');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", js, true);
            }
            else
            {
                js = "alert('Unable to update.');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", js, true);
            }
        }
        else
        {
            ddlVessel.Enabled = true;
            int responseid = objBLLVesselParameter.INS_VesselParameter_DL(int.Parse(ddlVessel.SelectedValue), decimal.Parse(txtPropellorPitch.Text.Trim()), decimal.Parse(txtbxQmcr.Text.Trim()), decimal.Parse(txtbxRPM_Max.Text.Trim()), decimal.Parse(txtMCR_Power.Text.Trim()), decimal.Parse(txtMCR_Power_Engine.Text.Trim()), decimal.Parse(txtSFOC.Text.Trim()), decimal.Parse(txtCylOil.Text.Trim()), Convert.ToInt32(Session["USERID"]));
            if (responseid > 0)
            {
                js = "alert('Type Created');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", js, true);
                string hidemodal = String.Format("hideModal('divadd')");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hidemodal", hidemodal, true);
                BindVesselParameters();
            }
            else
            {
                js = "alert('Parameter for selected vessel already exist');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", js, true);
            }
        }
        BindVesselParameters();
        HiddenFlag.Value = "";
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        BindVesselParameters();
        string hidemodal = String.Format("hideModal('divadd')");
        HiddenFlag.Value = "";
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hidemodal", hidemodal, true);
       
    }
    protected void BindVesselParameters()
    {
        int PAGE_SIZE = ucCustomPagerItems.PageSize;
        int START_INDEX = ucCustomPagerItems.CurrentPageIndex;
        int TOTAL_COUNT = ucCustomPagerItems.isCountRecord;
        // DataTable dt = objBLLVesselParameter.Get_VesselParameter_DL(int.Parse(ddlVesselList.SelectedValue), START_INDEX, PAGE_SIZE, ref TOTAL_COUNT);
        DataTable dt = objVesselParameter.Get_VesselParameter_DL(int.Parse(ddlVesselList.SelectedValue), START_INDEX, PAGE_SIZE, ref TOTAL_COUNT);

        gvVesselGeneralParameters.DataSource = dt;
        gvVesselGeneralParameters.DataBind();
        if (dt != null && dt.Rows.Count > 0)
        {
            if (ucCustomPagerItems.isCountRecord == 1)
            {
                ucCustomPagerItems.CountTotalRec = TOTAL_COUNT.ToString();
                ucCustomPagerItems.BuildPager();
            }
        }
    }

    protected void ImgAdd_Click(object sender, ImageClickEventArgs e)
    {
        ddlVessel.SelectedIndex = 0;
        txtbxQmcr.Text = "";
        txtbxRPM_Max.Text = "";
        txtPropellorPitch.Text = "";
        //Added for Chemical Tanker
        txtCylOil.Text = "";
        txtMCR_Power.Text = "";
        txtMCR_Power_Engine.Text = "";
        txtSFOC.Text = "";
        //end
        ddlVessel.Enabled = true;
        
        HiddenFlag.Value = "";
        string AddApprovalTypemodal = String.Format("showModal('divadd',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddApprovalTypemodal", AddApprovalTypemodal, true);
    }
    protected void ImgExpExcel_Click(object sender, ImageClickEventArgs e)
    {


        //int rowcount = ucCustomPagerItems.isCountRecord;
        DataTable dt = objBLLVesselParameter.Get_VesselParameter_DL(int.Parse(ddlVesselList.SelectedValue));

        string[] HeaderCaptions = { "Vessel Name", "Propellor Pitch", "Qmcr", "RPM Max", "M.E.Rated Power", " MCR Power For Engine Curve", "M.E SFOC", "Fuel Dependent/RPM Dependent" };
        string[] DataColumnsName = { "Vessel_Name", "Propeller_Pitch", "Qmcr", "RPM_Max", "Abs_eng_rat_power", "Eng_rat_power", "SFOC", "Cyl_oil_calc_mothod" };

        GridViewExportUtil.ShowExcel(dt, HeaderCaptions, DataColumnsName, "VesselGeneralParameters", "Vessel General Parameters", "");
    }
    protected void onDelete(object source, CommandEventArgs e)
    {
        int retval = objBLLVesselParameter.DEL_VesselParameter_DL(Convert.ToInt32(e.CommandArgument.ToString()), Convert.ToInt32(Session["USERID"].ToString()));
        BindVesselParameters();
    }
    protected void onUpdate(object source, CommandEventArgs e)
    {
        HiddenFlag.Value = e.CommandArgument.ToString();
        string js;
        DataTable dt = new DataTable();
        dt = objBLLVesselParameter.Get_VesselParameter_By_ID_DL((Convert.ToInt32(e.CommandArgument.ToString())));
        txtPropellorPitch.Text = dt.Rows[0]["Propeller_Pitch"].ToString();
        txtbxQmcr.Text = dt.Rows[0]["Qmcr"].ToString();
        txtbxRPM_Max.Text = dt.Rows[0]["RPM_Max"].ToString();
        //Added for Chemical Tanker
        txtMCR_Power.Text = Convert.ToString(dt.Rows[0]["Abs_eng_rat_power"]);
        txtMCR_Power_Engine.Text = Convert.ToString(dt.Rows[0]["Eng_rat_power"]);
        txtSFOC.Text = Convert.ToString(dt.Rows[0]["SFOC"]);
        txtCylOil.Text = Convert.ToString(dt.Rows[0]["Cyl_oil_calc_mothod"]);
        //Added for Chemical Tanker
        ddlVessel.SelectedValue = dt.Rows[0]["VesselID"].ToString();
        ddlVessel.Enabled = false;

        string AddApprovalTypemodal = String.Format("showModal('divadd',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddApprovalTypemodal", AddApprovalTypemodal, true);
    }
    protected void btnRefresh_Click(object sender, ImageClickEventArgs e)
    {
        ddlVesselList.SelectedIndex = 0;
        ddlFleet.SelectedIndex = 0;
        BindVesselParameters();
    }
    protected void btnFilter_Click(object sender, ImageClickEventArgs e)
    {
        BindVesselParameters();
    }

    protected void ddlFleet_SelectedIndexChanged(object sender, EventArgs e)
    {
        Load_VesselList();
        BindVesselParameters();
    }
    protected void ddlVesselList_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindVesselParameters();
    }

}