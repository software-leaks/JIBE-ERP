using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.Operation;
using SMS.Business.Technical;
using SMS.Business.Infrastructure;
using SMS.Properties;
public partial class ERLogBookThresHold : System.Web.UI.Page
{
    BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
    BLL_Infra_VesselLib objVessel = new BLL_Infra_VesselLib();
    public UserAccess objUA = new UserAccess();

    /// <summary>
    /// Modified by Anjali DT:16-06-2016 JIT:10048
    /// Loading event of page
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        UserAccessValidation();
        lblLogId.Attributes.Add("style", "visibility:hidden");
        if (!IsPostBack)
        {
            Load_VesselList();

            if (Request.QueryString["LOGID"] != null)
            {
                lblLogId.Text = Request.QueryString["LOGID"].ToString();
            }
            else
            {
                lblLogId.Text = null;
            }
            if (Request.QueryString["VESSELID"] != null)
            {
                ViewState["VESSELID"] = Request.QueryString["VESSELID"].ToString();
            }
            else
            {
                ViewState["VESSELID"] = null;
            }

            if (ViewState["VESSELID"] != null)
            {
                BindViews();
            }


            BindAlertSetting();
        }
        // Added by Anjali DY:16-06-2016 JIT:10048 || To register javascript function 'WebForm_OnSubmit' on submit click.
        Page.ClientScript.RegisterOnSubmitStatement(this.GetType(), "val", "WebForm_OnSubmit();");

    }
    private void BindAlertSetting()
    {
        DataTable dt = BLL_Tec_ErLog.Get_Followup_Settings().Tables[0];

        if (dt != null)
        {
            if (dt.Rows.Count > 0)
            {
                txtMailTo.Text = dt.Rows[0]["MAILTO"].ToString();

                if (dt.Rows[0]["LOGMAIL"].ToString() == "1")
                    chkEmail.Checked = true;
                else
                    chkEmail.Checked = false;


                if (dt.Rows[0]["LOGDASH"].ToString() == "1")
                    chkDesktopAlert.Checked = true;
                else
                    chkDesktopAlert.Checked = false;


                if (dt.Rows[0]["FOLLOWUPMAIL"].ToString() == "1")
                    chkEmailRe.Checked = true;
                else
                    chkEmailRe.Checked = false;


                if (dt.Rows[0]["FOLLOWUPDASH"].ToString() == "1")
                    chkDesktopAlertRe.Checked = true;
                else
                    chkDesktopAlertRe.Checked = false;


                if (dt.Rows[0]["ALERTTOMASTER"].ToString() == "1")
                    chkMaster.Checked = true;
                else
                    chkMaster.Checked = false;



                if (dt.Rows[0]["ALERTTOCE"].ToString() == "1")
                    chkCe.Checked = true;
                else
                    chkCe.Checked = false;

            }
        }
    }
    private void BindViews(int? Threshold_Id = null)
    {
        DataSet ds = BLL_Tec_ErLog.Get_ErLogBookThresHold_EDIT(int.Parse(ViewState["VESSELID"].ToString()), UDFLib.ConvertIntegerToNull(lblLogId.Text), Threshold_Id);
        ds.Tables[0].Rows[0]["Vessel_Id"] = UDFLib.ConvertIntegerToNull(ViewState["VESSELID"]);

        if (Threshold_Id == null)
        {
            DDLVersion.DataSource = ds.Tables[1];
            DDLVersion.DataTextField = "Version";
            DDLVersion.DataValueField = "ID";
            DDLVersion.DataBind();

            if (ds.Tables[1].Rows.Count == 0)
            {
                DDLVersion.Enabled = false;
                DDLVersion.Items.Insert(0, new ListItem("-No Records found-", "0"));
            }
            else
            {
                DDLVersion.Enabled = true;
            }
        }


        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlVesselMain.SelectedValue = ds.Tables[0].Rows[0]["Vessel_Id"].ToString();
            FormView1.DataSource = ds.Tables[0];
            FormView1.DataBind();

            if (ds.Tables[0].Rows[0]["Active_Status"].ToString() == "0")
            {
                btnAddTo.Enabled = false;
                btnCopy.Enabled = false;
                //  btnOK.Enabled = false;
                btnSave.Enabled = false;
            }

        }
        if (Convert.ToString(ds.Tables[0].Rows[0]["ID"]) != "")//if ds doesn't have rows throwing an error
        {
            DDLVersion.SelectedValue = ds.Tables[0].Rows[0]["ID"].ToString();
        }
    }
    public void Load_VesselList()
    {

        int UserCompanyID = UDFLib.ConvertToInteger(Session["USERCOMPANYID"].ToString());
        int Vessel_Manager = UDFLib.ConvertToInteger(Session["USERCOMPANYID"].ToString());
        if (Session["UTYPE"].ToString() == "VESSEL MANAGER")
            Vessel_Manager = UserCompanyID;

        ddlVessel.DataSource = objVessel.Get_VesselList(0, 0, Vessel_Manager, "", UserCompanyID);

        ddlVessel.DataTextField = "VESSEL_NAME";
        ddlVessel.DataValueField = "VESSEL_ID";
        ddlVessel.DataBind();
        ddlVessel.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));


        ddlVesselMain.DataSource = objVessel.Get_VesselList(0, 0, Vessel_Manager, "", UserCompanyID);

        ddlVesselMain.DataTextField = "VESSEL_NAME";
        ddlVesselMain.DataValueField = "VESSEL_ID";
        ddlVesselMain.DataBind();
        ddlVesselMain.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));


        ddlVessel.SelectedIndex = 0;
        ddlVesselMain.SelectedIndex = 0;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {

        Label lblthresHoldID = (Label)FormView1.Row.Cells[0].FindControl("lblThresHoldId");
        TextBox txt01ME_TEMP_EXH_Min = (TextBox)FormView1.Row.Cells[0].FindControl("txtME_TEMP_EXH_Min");
        TextBox txt01ME_TEMP_EXH_Max = (TextBox)FormView1.Row.Cells[0].FindControl("txtME_TEMP_EXH_Max");
        TextBox txt01METB_T_EXH_INLET_MIN = (TextBox)FormView1.Row.Cells[0].FindControl("txtMETB_T_EXH_INLET_MIN");
        TextBox txt01METB_T_EXH_INLET_MAX = (TextBox)FormView1.Row.Cells[0].FindControl("txtMETB_T_EXH_INLET_MAX");
        TextBox txt01METB_T_EXH_OUTLET_MIN = (TextBox)FormView1.Row.Cells[0].FindControl("txtMETB_T_EXH_OUTLET_MIN");
        TextBox txt01METB_T_EXH_OUTLET_Max = (TextBox)FormView1.Row.Cells[0].FindControl("txtMETB_T_EXH_OUTLET_Max");
        TextBox txt01METB_T_EXH_AIR_IN_MIN = (TextBox)FormView1.Row.Cells[0].FindControl("txtMETB_T_EXH_AIR_IN_MIN");
        TextBox txt01METB_T_EXH_AIR_OUT_MIN = (TextBox)FormView1.Row.Cells[0].FindControl("txtMETB_T_EXH_AIR_OUT_MIN");
        TextBox txt01METB_T_EXH_AIR_IN_Max = (TextBox)FormView1.Row.Cells[0].FindControl("txtMETB_T_EXH_AIR_IN_Max");
        TextBox txt01METB_T_EXH_AIR_OUT_Max = (TextBox)FormView1.Row.Cells[0].FindControl("txtMETB_T_EXH_AIR_OUT_Max");
        TextBox txt01METB_T_SCAVENGE_MIN = (TextBox)FormView1.Row.Cells[0].FindControl("txtMETB_T_SCAVENGE_MIN");
        TextBox txt01METB_T_SCAVENGE_Max = (TextBox)FormView1.Row.Cells[0].FindControl("txtMETB_T_SCAVENGE_Max");
        TextBox txt01METB_T_LO_B_MIN = (TextBox)FormView1.Row.Cells[0].FindControl("txtMETB_T_LO_B_MIN");
        TextBox txt01METB_T_LO_B_MAX = (TextBox)FormView1.Row.Cells[0].FindControl("txtMETB_T_LO_B_MAX");
        TextBox txt01METB_T_LO_T_Min = (TextBox)FormView1.Row.Cells[0].FindControl("txtMETB_T_LO_T_Min");
        TextBox txt01METB_T_LO_T_Max = (TextBox)FormView1.Row.Cells[0].FindControl("txtMETB_T_LO_T_Max");
        TextBox txt01METB_P_PD_AC_Min = (TextBox)FormView1.Row.Cells[0].FindControl("txtMETB_P_PD_AC_Min");
        TextBox txt01METB_P_PD_AC_Max = (TextBox)FormView1.Row.Cells[0].FindControl("txtMETB_P_PD_AC_Max");
        TextBox txt01ME_MB_IN_Min = (TextBox)FormView1.Row.Cells[0].FindControl("txtME_MB_IN_Min");
        TextBox txt01ME_MB_IN_Max = (TextBox)FormView1.Row.Cells[0].FindControl("txtME_MB_IN_Max");
        TextBox txt01ME_MB_OUT_Min = (TextBox)FormView1.Row.Cells[0].FindControl("txtME_MB_OUT_Min");
        TextBox txt01ME_MB_OUT_Max = (TextBox)FormView1.Row.Cells[0].FindControl("txtME_MB_OUT_Max");
        TextBox txt01ME_JC_IN_Min = (TextBox)FormView1.Row.Cells[0].FindControl("txtME_JC_IN_Min");
        TextBox txt01ME_JC_IN_Max = (TextBox)FormView1.Row.Cells[0].FindControl("txtME_JC_IN_Max");
        TextBox txt01ME_JC_OUT_Min = (TextBox)FormView1.Row.Cells[0].FindControl("txtME_JC_OUT_Min");
        TextBox txt01ME_JC_OUT_Max = (TextBox)FormView1.Row.Cells[0].FindControl("txtME_JC_OUT_Max");
        TextBox txt01ME_PC_IN_Min = (TextBox)FormView1.Row.Cells[0].FindControl("txtME_PC_IN_Min");
        TextBox txt01ME_PC_IN_Max = (TextBox)FormView1.Row.Cells[0].FindControl("txtME_PC_IN_Max");
        TextBox txt01ME_PC_OUT_Min = (TextBox)FormView1.Row.Cells[0].FindControl("txtME_PC_OUT_Min");
        TextBox txt01ME_PC_OUT_Max = (TextBox)FormView1.Row.Cells[0].FindControl("txtME_PC_OUT_Max");
        TextBox txt01ME_FUEL_OIL_Min = (TextBox)FormView1.Row.Cells[0].FindControl("txtME_FUEL_OIL_Min");
        TextBox txt01ME_FUEL_OIL_Max = (TextBox)FormView1.Row.Cells[0].FindControl("txtME_FUEL_OIL_Max");
        TextBox txt01ME_JC_FW_IN_min = (TextBox)FormView1.Row.Cells[0].FindControl("txtME_JC_FW_IN_min");
        TextBox txt01ME_JC_FW_IN_Max = (TextBox)FormView1.Row.Cells[0].FindControl("txtME_JC_FW_IN_Max");
        TextBox txt01ME_JC_FW_OUT_Min = (TextBox)FormView1.Row.Cells[0].FindControl("txtME_JC_FW_OUT_Min");
        TextBox txt01ME_JC_FW_OUT_Max = (TextBox)FormView1.Row.Cells[0].FindControl("txtME_JC_FW_OUT_Max");
        TextBox txt01ME_LC_LO_IN_min = (TextBox)FormView1.Row.Cells[0].FindControl("txtME_LC_LO_IN_min");
        TextBox txt01ME_LC_LO_IN_Max = (TextBox)FormView1.Row.Cells[0].FindControl("txtME_LC_LO_IN_Max");
        TextBox txt01ME_LC_LO_OUT_Min = (TextBox)FormView1.Row.Cells[0].FindControl("txtME_LC_LO_OUT_Min");
        TextBox txt01ME_LC_LO_OUT_Max = (TextBox)FormView1.Row.Cells[0].FindControl("txtME_LC_LO_OUT_Max");
        TextBox txt01ME_PC_LO_IN_min = (TextBox)FormView1.Row.Cells[0].FindControl("txtME_PC_LO_IN_min");
        TextBox txt01ME_PC_LO_IN_Max = (TextBox)FormView1.Row.Cells[0].FindControl("txtME_PC_LO_IN_Max");
        TextBox txt01ME_PC_LO_OUT_min = (TextBox)FormView1.Row.Cells[0].FindControl("txtME_PC_LO_OUT_min");
        TextBox txt01ME_PC_LO_OUT_Max = (TextBox)FormView1.Row.Cells[0].FindControl("txtME_PC_LO_OUT_Max");
        TextBox txt01ME_P_JACKET_WATER_min = (TextBox)FormView1.Row.Cells[0].FindControl("txtME_P_JACKET_WATER_min");
        TextBox txt01ME_P_JACKET_WATER_Max = (TextBox)FormView1.Row.Cells[0].FindControl("txtME_P_JACKET_WATER_Max");
        TextBox txt01ME_P_BEARING_XND_LO_Min = (TextBox)FormView1.Row.Cells[0].FindControl("txtME_P_BEARING_XND_LO_Min");
        TextBox txt01ME_P_BEARING_XND_LO_Max = (TextBox)FormView1.Row.Cells[0].FindControl("txtME_P_BEARING_XND_LO_Max");
        TextBox txt01ME_P_CAMSHAFT_LO_Min = (TextBox)FormView1.Row.Cells[0].FindControl("txtME_P_CAMSHAFT_LO_Min");
        TextBox txt01ME_P_CAMSHAFT_LO_Max = (TextBox)FormView1.Row.Cells[0].FindControl("txtME_P_CAMSHAFT_LO_Max");
        TextBox txt01ME_P_FV_COOLING_Min = (TextBox)FormView1.Row.Cells[0].FindControl("txtME_P_FV_COOLING_Min");
        TextBox txt01ME_P_FV_COOLING_Max = (TextBox)FormView1.Row.Cells[0].FindControl("txtME_P_FV_COOLING_Max");
        TextBox txt01ME_P_FUEL_OIL_min = (TextBox)FormView1.Row.Cells[0].FindControl("txtME_P_FUEL_OIL_min");
        TextBox txt01ME_P_FUEL_OIL_Max = (TextBox)FormView1.Row.Cells[0].FindControl("txtME_P_FUEL_OIL_Max");
        TextBox txt01ME_P_PISTON_COOLING_Min = (TextBox)FormView1.Row.Cells[0].FindControl("txtME_P_PISTON_COOLING_Min");
        TextBox txt01ME_P_PISTON_COOLING_Max = (TextBox)FormView1.Row.Cells[0].FindControl("txtME_P_PISTON_COOLING_Max");
        TextBox txt01ME_P_CONTROL_AIR_min = (TextBox)FormView1.Row.Cells[0].FindControl("txtME_P_CONTROL_AIR_min");
        TextBox txt01ME_P_CONTROL_AIR_Max = (TextBox)FormView1.Row.Cells[0].FindControl("txtME_P_CONTROL_AIR_Max");
        TextBox txt01REF_MEAT_TEMP_Min = (TextBox)FormView1.Row.Cells[0].FindControl("txtREF_MEAT_TEMP_Min");
        TextBox txt01REF_MEAT_TEMP_Max = (TextBox)FormView1.Row.Cells[0].FindControl("txtREF_MEAT_TEMP_Max");
        TextBox txt01REF_FISH_TEMP_Min = (TextBox)FormView1.Row.Cells[0].FindControl("txtREF_FISH_TEMP_Min");
        TextBox txt01REF_FISH_TEMP_Max = (TextBox)FormView1.Row.Cells[0].FindControl("txtREF_FISH_TEMP_Max");
        TextBox txt01REF_VEG_LOBBY_TEMP_Min = (TextBox)FormView1.Row.Cells[0].FindControl("txtREF_VEG_LOBBY_TEMP_Min");
        TextBox txt01REF_VEG_LOBBY_TEMP_Max = (TextBox)FormView1.Row.Cells[0].FindControl("txtREF_VEG_LOBBY_TEMP_Max");
        TextBox txt01FWGEN_VACCUM_Min = (TextBox)FormView1.Row.Cells[0].FindControl("txtFWGEN_VACCUM_Min");
        TextBox txt01FWGEN_VACCUM_Max = (TextBox)FormView1.Row.Cells[0].FindControl("txtFWGEN_VACCUM_Max");
        TextBox txt01FWGEN_SHELL_TEMP_Min = (TextBox)FormView1.Row.Cells[0].FindControl("txtFWGEN_SHELL_TEMP_Min");
        TextBox txt01FWGEN_SHELL_TEMP_Max = (TextBox)FormView1.Row.Cells[0].FindControl("txtFWGEN_SHELL_TEMP_Max");
        TextBox txt01FWGEN_SALINITY_PPM_Min = (TextBox)FormView1.Row.Cells[0].FindControl("txtFWGEN_SALINITY_PPM_Min");
        TextBox txt01FWGEN_SALINITY_PPM_Max = (TextBox)FormView1.Row.Cells[0].FindControl("txtFWGEN_SALINITY_PPM_Max");
        TextBox txt01PUR_HO_TEMP_Min = (TextBox)FormView1.Row.Cells[0].FindControl("txtPUR_HO_TEMP_Min");
        TextBox txt01PUR_HO_TEMP_Max = (TextBox)FormView1.Row.Cells[0].FindControl("txtPUR_HO_TEMP_Max");
        TextBox txt01PUR_LO_TEMP_Min = (TextBox)FormView1.Row.Cells[0].FindControl("txtPUR_LO_TEMP_Min");
        TextBox txt01PUR_LO_TEMP_Max = (TextBox)FormView1.Row.Cells[0].FindControl("txtPUR_LO_TEMP_Max");
        TextBox txt01MISC_THRUST_BRG_TEMP_Min = (TextBox)FormView1.Row.Cells[0].FindControl("txtMISC_THRUST_BRG_TEMP_Min");
        TextBox txt01MISC_THRUST_BRG_TEMP_Max = (TextBox)FormView1.Row.Cells[0].FindControl("txtMISC_THRUST_BRG_TEMP_Max");
        TextBox txt01MISC_INTERM_BRG_TEMP_Min = (TextBox)FormView1.Row.Cells[0].FindControl("txtMISC_INTERM_BRG_TEMP_Min");
        TextBox txt01MISC_INTERM_BRG_TEMP_Max = (TextBox)FormView1.Row.Cells[0].FindControl("txtMISC_INTERM_BRG_TEMP_Max");
        TextBox txt01MISC_STERN_TUBE_OIL_TEMP_Min = (TextBox)FormView1.Row.Cells[0].FindControl("txtMISC_STERN_TUBE_OIL_TEMP_Min");
        TextBox txt01MISC_STERN_TUBE_OIL_TEMP_Max = (TextBox)FormView1.Row.Cells[0].FindControl("txtMISC_STERN_TUBE_OIL_TEMP_Max");
        TextBox txt01MISC_HO_SETT_1_Min = (TextBox)FormView1.Row.Cells[0].FindControl("txtMISC_HO_SETT_1_Min");
        TextBox txt01MISC_HO_SETT_1_Max = (TextBox)FormView1.Row.Cells[0].FindControl("txtMISC_HO_SETT_1_Max");
        TextBox txt01MISC_HO_SETT_2_Min = (TextBox)FormView1.Row.Cells[0].FindControl("txtMISC_HO_SETT_2_Min");
        TextBox txt01MISC_HO_SETT_2_Max = (TextBox)FormView1.Row.Cells[0].FindControl("txtMISC_HO_SETT_2_Max");
        TextBox txt01MISC_HO_SERV_1_Min = (TextBox)FormView1.Row.Cells[0].FindControl("txtMISC_HO_SERV_1_Min");
        TextBox txt01MISC_HO_SERV_1_Max = (TextBox)FormView1.Row.Cells[0].FindControl("txtMISC_HO_SERV_1_Max");
        TextBox txt01MISC_HO_SERV_2_Min = (TextBox)FormView1.Row.Cells[0].FindControl("txtMISC_HO_SERV_2_Min");
        TextBox txt01MISC_HO_SERV_2_Max = (TextBox)FormView1.Row.Cells[0].FindControl("txtMISC_HO_SERV_2_Max");
        TextBox txt01GE_TEMP_EXH_MAX_Min = (TextBox)FormView1.Row.Cells[0].FindControl("txtGE_TEMP_EXH_MAX_Min");
        TextBox txt01GE_TEMP_EXH_MAX_Max = (TextBox)FormView1.Row.Cells[0].FindControl("txtGE_TEMP_EXH_MAX_Max");
        TextBox txt01GE_TEMP_EXH_MIN_Min = (TextBox)FormView1.Row.Cells[0].FindControl("txtGE_TEMP_EXH_MIN_Min");
        TextBox txt01GE_TEMP_EXH_MIN_Max = (TextBox)FormView1.Row.Cells[0].FindControl("txtGE_TEMP_EXH_MIN_Max");
        TextBox txt01GE_TEMP_CW_IN_Min = (TextBox)FormView1.Row.Cells[0].FindControl("txtGE_TEMP_CW_IN_Min");
        TextBox txt01GE_TEMP_CW_IN_Max = (TextBox)FormView1.Row.Cells[0].FindControl("txtGE_TEMP_CW_IN_Max");
        TextBox txt01GE_TEMP_CW_OUT_Min = (TextBox)FormView1.Row.Cells[0].FindControl("txtGE_TEMP_CW_OUT_Min");
        TextBox txt01GE_TEMP_CW_OUT_Max = (TextBox)FormView1.Row.Cells[0].FindControl("txtGE_TEMP_CW_OUT_Max");
        TextBox txt01GE_TEMP_LO_IN_Min = (TextBox)FormView1.Row.Cells[0].FindControl("txtGE_TEMP_LO_IN_Min");
        TextBox txt01GE_TEMP_LO_IN_Max = (TextBox)FormView1.Row.Cells[0].FindControl("txtGE_TEMP_LO_IN_Max");
        TextBox txt01GE_TEMP_LO_OUT_Min = (TextBox)FormView1.Row.Cells[0].FindControl("txtGE_TEMP_LO_OUT_Min");
        TextBox txt01GE_TEMP_LO_OUT_Max = (TextBox)FormView1.Row.Cells[0].FindControl("txtGE_TEMP_LO_OUT_Max");
        TextBox txt01GE_PRESS_LO_Min = (TextBox)FormView1.Row.Cells[0].FindControl("txtGE_PRESS_LO_Min");
        TextBox txt01GE_PRESS_LO_Max = (TextBox)FormView1.Row.Cells[0].FindControl("txtGE_PRESS_LO_Max");
        TextBox txt01GE_PRESS_CW_Min = (TextBox)FormView1.Row.Cells[0].FindControl("txtGE_PRESS_CW_Min");
        TextBox txt01GE_PRESS_CW_Max = (TextBox)FormView1.Row.Cells[0].FindControl("txtGE_PRESS_CW_Max");
        TextBox txt01SG_LO_PRESS_Min = (TextBox)FormView1.Row.Cells[0].FindControl("txtSG_LO_PRESS_Min");
        TextBox txt01SG_LO_PRESS_Max = (TextBox)FormView1.Row.Cells[0].FindControl("txtSG_LO_PRESS_Max");
        TextBox txt01SG_LO_TEMP_Min = (TextBox)FormView1.Row.Cells[0].FindControl("txtSG_LO_TEMP_Min");
        TextBox txt01SG_LO_TEMP_Max = (TextBox)FormView1.Row.Cells[0].FindControl("txtSG_LO_TEMP_Max");
        TextBox txt01CREATED_BY = (TextBox)FormView1.Row.Cells[0].FindControl("txtCREATED_BY");
        TextBox txt01CREATED_DATE = (TextBox)FormView1.Row.Cells[0].FindControl("txtCREATED_DATE");
        TextBox txt01MODIFIED_BY = (TextBox)FormView1.Row.Cells[0].FindControl("txtMODIFIED_BY");
        TextBox txt01MODIFIED_DATE = (TextBox)FormView1.Row.Cells[0].FindControl("txtMODIFIED_DATE");
        TextBox txt01DELETED_BY = (TextBox)FormView1.Row.Cells[0].FindControl("txtDELETED_BY");
        TextBox txt01DELETED_DATE = (TextBox)FormView1.Row.Cells[0].FindControl("txtDELETED_DATE");
        TextBox txt01ACTIVE_STATUS = (TextBox)FormView1.Row.Cells[0].FindControl("txtACTIVE_STATUS");

        int i = BLL_Tec_ErLog.Inser_Update_ErLogBookThresHold(UDFLib.ConvertIntegerToNull(lblthresHoldID.Text), int.Parse(ViewState["VESSELID"].ToString()), UDFLib.ConvertDecimalToNull(txt01ME_TEMP_EXH_Min.Text), UDFLib.ConvertDecimalToNull(txt01ME_TEMP_EXH_Max.Text),
            UDFLib.ConvertDecimalToNull(txt01METB_T_EXH_INLET_MIN.Text), UDFLib.ConvertDecimalToNull(txt01METB_T_EXH_INLET_MAX.Text), UDFLib.ConvertDecimalToNull(txt01METB_T_EXH_OUTLET_MIN.Text),
            UDFLib.ConvertDecimalToNull(txt01METB_T_EXH_OUTLET_Max.Text), UDFLib.ConvertDecimalToNull(txt01METB_T_EXH_AIR_IN_MIN.Text), UDFLib.ConvertDecimalToNull(txt01METB_T_EXH_AIR_OUT_MIN.Text),
            UDFLib.ConvertDecimalToNull(txt01METB_T_EXH_AIR_IN_Max.Text), UDFLib.ConvertDecimalToNull(txt01METB_T_EXH_AIR_OUT_Max.Text), UDFLib.ConvertDecimalToNull(txt01METB_T_SCAVENGE_MIN.Text),
            UDFLib.ConvertDecimalToNull(txt01METB_T_SCAVENGE_Max.Text), UDFLib.ConvertDecimalToNull(txt01METB_T_LO_B_MIN.Text), UDFLib.ConvertDecimalToNull(txt01METB_T_LO_B_MAX.Text),
            UDFLib.ConvertDecimalToNull(txt01METB_T_LO_T_Min.Text), UDFLib.ConvertDecimalToNull(txt01METB_T_LO_T_Max.Text), UDFLib.ConvertDecimalToNull(txt01METB_P_PD_AC_Min.Text),
            UDFLib.ConvertDecimalToNull(txt01METB_P_PD_AC_Max.Text), UDFLib.ConvertDecimalToNull(txt01ME_MB_IN_Min.Text), UDFLib.ConvertDecimalToNull(txt01ME_MB_IN_Max.Text),
            UDFLib.ConvertDecimalToNull(txt01ME_MB_OUT_Min.Text), UDFLib.ConvertDecimalToNull(txt01ME_MB_OUT_Max.Text), UDFLib.ConvertDecimalToNull(txt01ME_JC_IN_Min.Text),
            UDFLib.ConvertDecimalToNull(txt01ME_JC_IN_Max.Text), UDFLib.ConvertDecimalToNull(txt01ME_JC_OUT_Min.Text), UDFLib.ConvertDecimalToNull(txt01ME_JC_OUT_Max.Text),
            UDFLib.ConvertDecimalToNull(txt01ME_PC_IN_Min.Text), UDFLib.ConvertDecimalToNull(txt01ME_PC_IN_Max.Text), UDFLib.ConvertDecimalToNull(txt01ME_PC_OUT_Min.Text),
            UDFLib.ConvertDecimalToNull(txt01ME_PC_OUT_Max.Text), UDFLib.ConvertDecimalToNull(txt01ME_FUEL_OIL_Min.Text), UDFLib.ConvertDecimalToNull(txt01ME_FUEL_OIL_Max.Text),
            UDFLib.ConvertDecimalToNull(txt01ME_JC_FW_IN_min.Text), UDFLib.ConvertDecimalToNull(txt01ME_JC_FW_IN_Max.Text), UDFLib.ConvertDecimalToNull(txt01ME_JC_FW_OUT_Min.Text),
            UDFLib.ConvertDecimalToNull(txt01ME_JC_FW_OUT_Max.Text), UDFLib.ConvertDecimalToNull(txt01ME_LC_LO_IN_min.Text), UDFLib.ConvertDecimalToNull(txt01ME_LC_LO_IN_Max.Text),
            UDFLib.ConvertDecimalToNull(txt01ME_LC_LO_OUT_Min.Text), UDFLib.ConvertDecimalToNull(txt01ME_LC_LO_OUT_Max.Text), UDFLib.ConvertDecimalToNull(txt01ME_PC_LO_IN_min.Text),
            UDFLib.ConvertDecimalToNull(txt01ME_PC_LO_IN_Max.Text), UDFLib.ConvertDecimalToNull(txt01ME_PC_LO_OUT_min.Text), UDFLib.ConvertDecimalToNull(txt01ME_PC_LO_OUT_Max.Text),
            UDFLib.ConvertDecimalToNull(txt01ME_P_JACKET_WATER_min.Text), UDFLib.ConvertDecimalToNull(txt01ME_P_JACKET_WATER_Max.Text), UDFLib.ConvertDecimalToNull(txt01ME_P_BEARING_XND_LO_Min.Text),
            UDFLib.ConvertDecimalToNull(txt01ME_P_BEARING_XND_LO_Max.Text), UDFLib.ConvertDecimalToNull(txt01ME_P_CAMSHAFT_LO_Min.Text), UDFLib.ConvertDecimalToNull(txt01ME_P_CAMSHAFT_LO_Max.Text),
            UDFLib.ConvertDecimalToNull(txt01ME_P_FV_COOLING_Min.Text), UDFLib.ConvertDecimalToNull(txt01ME_P_FV_COOLING_Max.Text), UDFLib.ConvertDecimalToNull(txt01ME_P_FUEL_OIL_min.Text),
            UDFLib.ConvertDecimalToNull(txt01ME_P_FUEL_OIL_Max.Text), UDFLib.ConvertDecimalToNull(txt01ME_P_PISTON_COOLING_Min.Text), UDFLib.ConvertDecimalToNull(txt01ME_P_PISTON_COOLING_Max.Text),
            UDFLib.ConvertDecimalToNull(txt01ME_P_CONTROL_AIR_min.Text), UDFLib.ConvertDecimalToNull(txt01ME_P_CONTROL_AIR_Max.Text), UDFLib.ConvertDecimalToNull(txt01REF_MEAT_TEMP_Min.Text),
            UDFLib.ConvertDecimalToNull(txt01REF_MEAT_TEMP_Max.Text), UDFLib.ConvertDecimalToNull(txt01REF_FISH_TEMP_Min.Text), UDFLib.ConvertDecimalToNull(txt01REF_FISH_TEMP_Max.Text),
            UDFLib.ConvertDecimalToNull(txt01REF_VEG_LOBBY_TEMP_Min.Text), UDFLib.ConvertDecimalToNull(txt01REF_VEG_LOBBY_TEMP_Max.Text), UDFLib.ConvertDecimalToNull(txt01FWGEN_VACCUM_Min.Text),
            UDFLib.ConvertDecimalToNull(txt01FWGEN_VACCUM_Max.Text), UDFLib.ConvertDecimalToNull(txt01FWGEN_SHELL_TEMP_Min.Text), UDFLib.ConvertDecimalToNull(txt01FWGEN_SHELL_TEMP_Max.Text),
            UDFLib.ConvertDecimalToNull(txt01FWGEN_SALINITY_PPM_Min.Text), UDFLib.ConvertDecimalToNull(txt01FWGEN_SALINITY_PPM_Max.Text), UDFLib.ConvertDecimalToNull(txt01PUR_HO_TEMP_Min.Text),
            UDFLib.ConvertDecimalToNull(txt01PUR_HO_TEMP_Max.Text), UDFLib.ConvertDecimalToNull(txt01PUR_LO_TEMP_Min.Text), UDFLib.ConvertDecimalToNull(txt01PUR_LO_TEMP_Max.Text),
            UDFLib.ConvertDecimalToNull(txt01MISC_THRUST_BRG_TEMP_Min.Text), UDFLib.ConvertDecimalToNull(txt01MISC_THRUST_BRG_TEMP_Max.Text), UDFLib.ConvertDecimalToNull(txt01MISC_INTERM_BRG_TEMP_Min.Text),
            UDFLib.ConvertDecimalToNull(txt01MISC_INTERM_BRG_TEMP_Max.Text), UDFLib.ConvertDecimalToNull(txt01MISC_STERN_TUBE_OIL_TEMP_Min.Text), UDFLib.ConvertDecimalToNull(txt01MISC_STERN_TUBE_OIL_TEMP_Max.Text),
            UDFLib.ConvertDecimalToNull(txt01MISC_HO_SETT_1_Min.Text), UDFLib.ConvertDecimalToNull(txt01MISC_HO_SETT_1_Max.Text), UDFLib.ConvertDecimalToNull(txt01MISC_HO_SETT_2_Min.Text),
            UDFLib.ConvertDecimalToNull(txt01MISC_HO_SETT_2_Max.Text), UDFLib.ConvertDecimalToNull(txt01MISC_HO_SERV_1_Min.Text), UDFLib.ConvertDecimalToNull(txt01MISC_HO_SERV_1_Max.Text),
            UDFLib.ConvertDecimalToNull(txt01MISC_HO_SERV_2_Min.Text), UDFLib.ConvertDecimalToNull(txt01MISC_HO_SERV_2_Max.Text), UDFLib.ConvertDecimalToNull(txt01GE_TEMP_EXH_MAX_Min.Text),
            UDFLib.ConvertDecimalToNull(txt01GE_TEMP_EXH_MAX_Max.Text), UDFLib.ConvertDecimalToNull(txt01GE_TEMP_EXH_MIN_Min.Text), UDFLib.ConvertDecimalToNull(txt01GE_TEMP_EXH_MIN_Max.Text),
            UDFLib.ConvertDecimalToNull(txt01GE_TEMP_CW_IN_Min.Text), UDFLib.ConvertDecimalToNull(txt01GE_TEMP_CW_IN_Max.Text), UDFLib.ConvertDecimalToNull(txt01GE_TEMP_CW_OUT_Min.Text),
            UDFLib.ConvertDecimalToNull(txt01GE_TEMP_CW_OUT_Max.Text), UDFLib.ConvertDecimalToNull(txt01GE_TEMP_LO_IN_Min.Text), UDFLib.ConvertDecimalToNull(txt01GE_TEMP_LO_IN_Max.Text),
            UDFLib.ConvertDecimalToNull(txt01GE_TEMP_LO_OUT_Min.Text), UDFLib.ConvertDecimalToNull(txt01GE_TEMP_LO_OUT_Max.Text), UDFLib.ConvertDecimalToNull(txt01GE_PRESS_LO_Min.Text),
            UDFLib.ConvertDecimalToNull(txt01GE_PRESS_LO_Max.Text), UDFLib.ConvertDecimalToNull(txt01GE_PRESS_CW_Min.Text), UDFLib.ConvertDecimalToNull(txt01GE_PRESS_CW_Max.Text),
            UDFLib.ConvertDecimalToNull(txt01SG_LO_PRESS_Min.Text), UDFLib.ConvertDecimalToNull(txt01SG_LO_PRESS_Max.Text), UDFLib.ConvertDecimalToNull(txt01SG_LO_TEMP_Min.Text),
            UDFLib.ConvertDecimalToNull(txt01SG_LO_TEMP_Max.Text), Convert.ToInt32(Session["USERID"].ToString()));
        string js = "alertmessage(1);";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Update", js, true);
        lblLogId.Text = null;
        lblthresHoldID.Text = null;
        BindViews(null);
    }
    protected void UserAccessValidation()
    {

        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        objUA = objUser.Get_UserAccessForPage(Convert.ToInt32(Session["USERID"]), PageURL);
        if (objUA.Edit == 0)
        {
            btnSave.Enabled = false;
            FormView1.Enabled = false;
        }

        if (objUA.View == 0)
        {
            Response.Redirect("~/crew/default.aspx?msgid=1");
        }

        if (objUA.Add == 0)
        {

        }
    }
    protected void btnCopy_Click(object sender, EventArgs e)
    {

        int copyfromvessel = int.Parse(ddlVessel.SelectedValue);
        string js = "";
        int i = BLL_Tec_ErLog.CopyERLogBookThreshold(int.Parse(ViewState["VESSELID"].ToString()), copyfromvessel, int.Parse(Session["USERID"].ToString()));
        if (i <= 0)
        {
            js = "alertmessage(0);";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Update", js, true);
            return;
        }

        js = "alert('Threshold Values are copied successfully from " + ddlVessel.SelectedItem.Text + " to  " + ddlVesselMain.SelectedItem.Text + "!');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Update", js, true);
        lblLogId.Text = null;

        BindViews(null);
    }
    protected void btnThresholdActionSetting_Click(object sender, EventArgs e)
    {

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
        //  UpdatePanel2.Update();
        objUser = null;
        string js = "showPopup('ThresholdActionSettingDew');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Update", js, true);
    }
    protected void btnSaveAction_Click(object sender, EventArgs e)
    {
        BLL_Tec_ErLog.Update_Followup_Settings(txtMailTo.Text, chkEmail.Checked ? 1 : 0, chkDesktopAlert.Checked ? 1 : 0, chkEmailRe.Checked ? 1 : 0, chkDesktopAlertRe.Checked ? 1 : 0, chkMaster.Checked ? 1 : 0, chkCe.Checked ? 1 : 0, Convert.ToInt32(Session["USERID"]));
    }
    protected void btnCloseAction_Click(object sender, EventArgs e)
    {

    }
    protected void ddlVesselMain_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlVesselMain.SelectedIndex == 0)
            return;

        ViewState["VESSELID"] = ddlVesselMain.SelectedValue;
        lblLogId.Text = null;
        BindViews();
    }

    protected void DDLVersion_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DDLVersion.SelectedIndex > 0)
        {
            btnSave.Enabled = false;
            btnCopy.Enabled = false;
        }
        else
        {
            btnSave.Enabled = true;
            btnCopy.Enabled = true;
        }
        BindViews(UDFLib.ConvertIntegerToNull(DDLVersion.SelectedValue));
    }
}