using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.Technical;
using SMS.Properties;
using SMS.Business.Infrastructure;

public partial class ERLogThreshold : System.Web.UI.Page
{
    BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
    public UserAccess objUA = new UserAccess();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            UserAccessValidation();

            if (Request.QueryString["VESSELID"] != null)
            {
                ViewState["VESSELID"] = Request.QueryString["VESSELID"].ToString();              
            }
            if (Request.QueryString["LOGID"] != null)
            {
                ViewState["logid"]= Request.QueryString["LOGID"].ToString();
                BindViews();
            }
            if (Request.QueryString["ViewID"] != null)
            {
                ViewTabDetails(int.Parse(Request.QueryString["ViewID"].ToString()));
                ViewState["ViewID"] = Request.QueryString["ViewID"].ToString(); 
            }
        }

    }

    private void BindViews()
    {
        //DataTable dt = BLL_Tec_ErLog.ErLog_ME_00_Get(int.Parse(ViewState["logid"].ToString()));
        DataTable dt = BLL_Tec_ErLog.ErLog_ThresHold_Main_EDIT(int.Parse(Request.QueryString["VESSELID"].ToString()));
        if (dt.Rows.Count > 0)
        {
            DataRow dr = dt.Rows[0];

            ///           FUEL OIL DAILY ACCOUNT (MT)

            txtFODA_HO_ROB_PNN.Text = dr["FODA_HO_ROB_PNN_Min"].ToString();
            txtFODA_DO_ROB_PNN.Text = dr["FODA_DO_ROB_PNN_Min"].ToString();
            txtFODA_GO_ROB_PNN.Text = dr["FODA_GO_ROB_PNN_Min"].ToString();
            txtFODA_HO_CONS_ME.Text = dr["FODA_HO_CONS_ME_Min"].ToString();
            txtFODA_DO_CONS_ME.Text = dr["FODA_DO_CONS_ME_Min"].ToString();
            txtFODA_GO_CONS_ME.Text = dr["FODA_GO_CONS_ME_Min"].ToString();
            txtFODA_HO_CONS_AE.Text = dr["FODA_HO_CONS_AE_Min"].ToString();
            txtFODA_DO_CONS_AE.Text = dr["FODA_DO_CONS_AE_Min"].ToString();
            txtFODA_GO_CONS_AE.Text = dr["FODA_GO_CONS_AE_Min"].ToString();
            txtFODA_HO_CONS_BLR.Text = dr["FODA_HO_CONS_BLR_Min"].ToString();
            txtFODA_DO_CONS_BLR.Text = dr["FODA_DO_CONS_BLR_Min"].ToString();
            txtFODA_GO_CONS_BLR.Text = dr["FODA_GO_CONS_BLR_Min"].ToString();
            txtFODA_HO_CONS_TC.Text = dr["FODA_HO_CONS_TC_Min"].ToString();
            txtFODA_DO_CONS_TC.Text = dr["FODA_DO_CONS_TC_Min"].ToString();
            txtFODA_GO_CONS_TC.Text = dr["FODA_GO_CONS_TC_Min"].ToString();
            txtFODA_HO_CONS_HTG.Text = dr["FODA_HO_CONS_HTG_Min"].ToString();
            txtFODA_DO_CONS_HTG.Text = dr["FODA_DO_CONS_HTG_Min"].ToString();
            txtFODA_GO_CONS_HTG.Text = dr["FODA_GO_CONS_HTG_Min"].ToString();
            txtFODA_HO_CONS_TTL.Text = dr["FODA_HO_CONS_TTL_Min"].ToString();
            txtFODA_DO_CONS_TTL.Text = dr["FODA_DO_CONS_TTL_Min"].ToString();
            txtFODA_GO_CONS_TTL.Text = dr["FODA_GO_CONS_TTL_Min"].ToString();
            txtFODA_HO_RCVD.Text = dr["FODA_HO_RCVD_Min"].ToString();
            txtFODA_DO_RCVD.Text = dr["FODA_DO_RCVD_Min"].ToString();
            txtFODA_GO_RCVD.Text = dr["FODA_GO_RCVD_Min"].ToString();
            txtFODA_HO_AMEND.Text = dr["FODA_HO_AMEND_Min"].ToString();
            txtFODA_DO_AMEND.Text = dr["FODA_DO_AMEND_Min"].ToString();
            txtFODA_GO_AMEND.Text = dr["FODA_GO_AMEND_Min"].ToString();
            txtFODA_HO_ROB.Text = dr["FODA_HO_ROB_Min"].ToString();
            txtFODA_DO_ROB.Text = dr["FODA_DO_ROB_Min"].ToString();
            txtFODA_GO_ROB.Text = dr["FODA_GO_ROB_Min"].ToString();

            txtFODA_HO_ROB_PNN_max.Text = dr["FODA_HO_ROB_PNN_Max"].ToString();
            txtFODA_DO_ROB_PNN_Max.Text = dr["FODA_DO_ROB_PNN_Max"].ToString();
            txtFODA_GO_ROB_PNN_Max.Text = dr["FODA_GO_ROB_PNN_Max"].ToString();
            txtFODA_HO_CONS_ME_Max.Text = dr["FODA_HO_CONS_ME_Max"].ToString();
            txtFODA_DO_CONS_ME_Max.Text = dr["FODA_DO_CONS_ME_Max"].ToString();
            txtFODA_GO_CONS_ME_Max.Text = dr["FODA_GO_CONS_ME_Max"].ToString();
            txtFODA_HO_CONS_AE_Max.Text = dr["FODA_HO_CONS_AE_Max"].ToString();
            txtFODA_DO_CONS_AE_Max.Text = dr["FODA_DO_CONS_AE_Max"].ToString();
            txtFODA_GO_CONS_AE_Max.Text = dr["FODA_GO_CONS_AE_Max"].ToString();
            txtFODA_HO_CONS_BLR_Max.Text = dr["FODA_HO_CONS_BLR_Max"].ToString();
            txtFODA_DO_CONS_BLR_Max.Text = dr["FODA_DO_CONS_BLR_Max"].ToString();
            txtFODA_GO_CONS_BLR_Max.Text = dr["FODA_GO_CONS_BLR_Max"].ToString();
            txtFODA_HO_CONS_TC_Max.Text = dr["FODA_HO_CONS_TC_Max"].ToString();
            txtFODA_DO_CONS_TC_Max.Text = dr["FODA_DO_CONS_TC_Max"].ToString();
            txtFODA_GO_CONS_TC_Max.Text = dr["FODA_GO_CONS_TC_Max"].ToString();
            txtFODA_HO_CONS_HTG_Max.Text = dr["FODA_HO_CONS_HTG_Max"].ToString();
            txtFODA_DO_CONS_HTG_Max.Text = dr["FODA_DO_CONS_HTG_Max"].ToString();
            txtFODA_GO_CONS_HTG_Max.Text = dr["FODA_GO_CONS_HTG_Max"].ToString();
            txtFODA_HO_CONS_TTL_Max.Text = dr["FODA_HO_CONS_TTL_Max"].ToString();
            txtFODA_DO_CONS_TTL_Max.Text = dr["FODA_DO_CONS_TTL_Max"].ToString();
            txtFODA_GO_CONS_TTL_Max.Text = dr["FODA_GO_CONS_TTL_Max"].ToString();
            txtFODA_HO_RCVD_Max.Text = dr["FODA_HO_RCVD_Max"].ToString();
            txtFODA_DO_RCVD_Max.Text = dr["FODA_DO_RCVD_Max"].ToString();
            txtFODA_GO_RCVD_Max.Text = dr["FODA_GO_RCVD_Max"].ToString();
            txtFODA_HO_AMEND_Max.Text = dr["FODA_HO_AMEND_Max"].ToString();
            txtFODA_DO_AMEND_Max.Text = dr["FODA_DO_AMEND_Max"].ToString();
            txtFODA_GO_AMEND_Max.Text = dr["FODA_GO_AMEND_Max"].ToString();
            txtFODA_HO_ROB_Max.Text = dr["FODA_HO_ROB_Max"].ToString();
            txtFODA_DO_ROB_Max.Text = dr["FODA_DO_ROB_Max"].ToString();
            txtFODA_GO_ROB_Max.Text = dr["FODA_GO_ROB_Max"].ToString();

            ///  FRESH WATER DAILY ACCOUNT

            txtFWDA_POT_ROB_PNN.Text =  dr["FWDA_POT_ROB_PNN_Min"].ToString();
            txtFWDA_WASHP_ROB_PNN.Text = dr["FWDA_WASHP_ROB_PNN_Min"].ToString();
            txtFWDA_WASHS_ROB_PNN.Text = dr["FWDA_WASHS_ROB_PNN_Min"].ToString();
            txtFWDA_DISTL_ROB_PNN.Text = dr["FWDA_DISTL_ROB_PNN_Min"].ToString();
            txtFWDA_POT_PROD.Text = dr["FWDA_POT_PROD_Min"].ToString();
            txtFWDA_WASHP_PROD.Text = dr["FWDA_WASHP_PROD_Min"].ToString();
            txtFWDA_WASHS_PROD.Text = dr["FWDA_WASHS_PROD_Min"].ToString();
            txtFWDA_DISTL_PROD.Text = dr["FWDA_DISTL_PROD_Min"].ToString();
            txtFWDA_POT_RCVD.Text = dr["FWDA_POT_RCVD_Min"].ToString();
            txtFWDA_WASHP_RCVD.Text = dr["FWDA_WASHP_RCVD_Min"].ToString();
            txtFWDA_WASHS_RCVD.Text = dr["FWDA_WASHS_RCVD_Min"].ToString();
            txtFWDA_DISTL_RCVD.Text = dr["FWDA_DISTL_RCVD_Min"].ToString();
            txtFWDA_POT_CNSMP.Text = dr["FWDA_POT_CNSMP_Min"].ToString();
            txtFWDA_WASHP_CNSMP.Text = dr["FWDA_WASHP_CNSMP_Min"].ToString();
            txtFWDA_WASHS_CNSMP.Text = dr["FWDA_WASHS_CNSMP_Min"].ToString();
            txtFWDA_DISTL_CNSMP.Text = dr["FWDA_DISTL_CNSMP_Min"].ToString();
            txtFWDA_POT_ROB.Text = dr["FWDA_POT_ROB_Min"].ToString();
            txtFWDA_WASHP_ROB.Text = dr["FWDA_WASHP_ROB_Min"].ToString();
            txtFWDA_WASHS_ROB.Text = dr["FWDA_WASHS_ROB_Min"].ToString();
            txtFWDA_DISTL_ROB.Text = dr["FWDA_DISTL_ROB_Min"].ToString();

            txtFWDA_POT_ROB_PNN_Max.Text = dr["FWDA_POT_ROB_PNN_MAX"].ToString();
            txtFWDA_WASHP_ROB_PNN_Max.Text = dr["FWDA_WASHP_ROB_PNN_MAX"].ToString();
            txtFWDA_WASHS_ROB_PNN_Max.Text = dr["FWDA_WASHS_ROB_PNN_Max"].ToString();
            txtFWDA_DISTL_ROB_PNN_Max.Text = dr["FWDA_DISTL_ROB_PNN_Max"].ToString();
            txtFWDA_POT_PROD_Max.Text = dr["FWDA_POT_PROD_Max"].ToString();
            txtFWDA_WASHP_PROD_Max.Text = dr["FWDA_WASHP_PROD_Max"].ToString();
            txtFWDA_WASHS_PROD_Max.Text = dr["FWDA_WASHS_PROD_Max"].ToString();
            txtFWDA_DISTL_PROD_Max.Text = dr["FWDA_DISTL_PROD_Max"].ToString();
            txtFWDA_POT_RCVD_Max.Text = dr["FWDA_POT_RCVD_Max"].ToString();
            txtFWDA_WASHP_RCVD_max.Text = dr["FWDA_WASHP_RCVD_Max"].ToString();
            txtFWDA_WASHS_RCVD_Max.Text = dr["FWDA_WASHS_RCVD_Max"].ToString();
            txtFWDA_DISTL_RCVD_Max.Text = dr["FWDA_DISTL_RCVD_Max"].ToString();
            txtFWDA_POT_CNSMP_Max.Text = dr["FWDA_POT_CNSMP_Max"].ToString();
            txtFWDA_WASHP_CNSMP_max.Text = dr["FWDA_WASHP_CNSMP_Max"].ToString();
            txtFWDA_WASHS_CNSMP_Max.Text = dr["FWDA_WASHS_CNSMP_Max"].ToString();
            txtFWDA_DISTL_CNSMP_Max.Text = dr["FWDA_DISTL_CNSMP_Max"].ToString();
            txtFWDA_POT_ROB_max.Text = dr["FWDA_POT_ROB_Max"].ToString();
            txtFWDA_WASHP_ROB_Max.Text = dr["FWDA_WASHP_ROB_Max"].ToString();
            txtFWDA_WASHS_ROB_Max.Text = dr["FWDA_WASHS_ROB_Max"].ToString();
            txtFWDA_DISTL_ROB_Max.Text = dr["FWDA_DISTL_ROB_Max"].ToString();

            // WORKING HOURS

            txtWRKHRS_ME_NN_Min.Text = dr["WRKHRS_ME_NN_Min"].ToString();
            txtWRKHRS_AE1_NN_min.Text = dr["WRKHRS_AE1_NN_Min"].ToString();
            txtWRKHRS_AE2_NN_min.Text = dr["WRKHRS_AE2_NN_Min"].ToString();
            txtWRKHRS_AE3_NN_min.Text = dr["WRKHRS_AE3_NN_Min"].ToString();
            txtWRKHRS_AE4_NN_min.Text = dr["WRKHRS_AE4_NN_MIN"].ToString();
            txtWRKHRS_TA_NN_Min.Text = dr["WRKHRS_TA_NN_Min"].ToString();
            txtWRKHRS_SG_NN_Min.Text = dr["WRKHRS_SG_NN_Min"].ToString();
            txtWRKHRS_ME_NN_max.Text =  dr["WRKHRS_ME_NN_Max"].ToString();
            txtWRKHRS_AE1_NN_Max.Text =  dr["WRKHRS_AE1_NN_Max"].ToString();
            txtWRKHRS_AE2_NN_Max.Text =  dr["WRKHRS_AE2_NN_Max"].ToString();
            txtWRKHRS_AE3_NN_Max.Text =  dr["WRKHRS_AE3_NN_Max"].ToString();
            txtWRKHRS_AE4_NN_Max.Text =  dr["WRKHRS_AE4_NN_Max"].ToString();
            txtWRKHRS_TA_NN_Max.Text =  dr["WRKHRS_TA_NN_Max"].ToString();
            txtWRKHRS_SG_NN_Max.Text =  dr["WRKHRS_SG_NN_Max"].ToString();
            //txtWRKHRS_ME_TTL.Text =  dr["WRKHRS_ME_TTL"].ToString();
            //txtWRKHRS_AE1_TTL.Text =  dr["WRKHRS_AE1_TTL"].ToString();
            //txtWRKHRS_AE2_TTL.Text =  dr["WRKHRS_AE2_TTL"].ToString();
            //txtWRKHRS_AE3_TTL.Text =  dr["WRKHRS_AE3_TTL"].ToString();
            //txtWRKHRS_AE4_TTL.Text =  dr["WRKHRS_AE4_TTL"].ToString();
            //txtWRKHRS_TA_TTL.Text =  dr["WRKHRS_TA_TTL"].ToString();
            //txtWRKHRS_SG_TTL.Text =  dr["WRKHRS_SG_TTL"].ToString();

            // DAILY PERFORMANCE

            txtDP_WIND_FORCE.Text =  dr["DP_WIND_FORCE_Min"].ToString();
            txtDP_WIND_DIR.Text =  dr["DP_WIND_DIR_Min"].ToString();
            txtDP_SEA_COND.Text =  dr["DP_SEA_COND_Min"].ToString();
            txtDP_SWELL.Text =  dr["DP_SWELL_Min"].ToString();
            txtDP_SWELL_DIR.Text =  dr["DP_SWELL_DIR_Min"].ToString();
            txtDP_CURR.Text =  dr["DP_CURR_Min"].ToString();
            txtDP_REVS_NTN.Text =  dr["DP_REVS_NTN_Min"].ToString();
            txtDP_ENG_DIST.Text =  dr["DP_ENG_DIST_Min"].ToString();
            txtDP_OBS_DIST.Text =  dr["DP_OBS_DIST_Min"].ToString();
            txtDP_TTL_DIST.Text =  dr["DP_TTL_DIST_Min"].ToString();
            txtDP_HRS_FUL_SPD.Text =  dr["DP_HRS_FUL_SPD_Min"].ToString();
            txtDP_AVG_RPM.Text =  dr["DP_AVG_RPM_Min"].ToString();
            txtDP_SLIP.Text =  dr["DP_SLIP_Min"].ToString();
            txtDP_DIST_TOGO.Text =  dr["DP_DIST_TOGO_Min"].ToString();
            txtDP_HRS_RED_SPD.Text =  dr["DP_HRS_RED_SPD_Min"].ToString();
            txtDP_HRS_STOPD.Text =  dr["DP_HRS_STOPD_Min"].ToString();
            txtDP_OBS_SPD.Text =  dr["DP_OBS_SPD_Min"].ToString();
            txtETA.Text =  dr["ETA_Min"].ToString();

            //   LUBRICATING OIL DAILY ACCOUNT (Ltr)

            txtLODA_MECC_ROB_PNN.Text =  dr["LODA_MECC_ROB_PNN_Min"].ToString();
            txtLODA_MECYL_ROB_PNN.Text = dr["LODA_MECYL_ROB_PNN_Min"].ToString();
            txtLODA_AECC_ROB_PNN.Text = dr["LODA_AECC_ROB_PNN_Min"].ToString();
            txtLODA_MECC_RCVD.Text = dr["LODA_MECC_RCVD_Min"].ToString();
            txtLODA_MECYL_RCVD.Text = dr["LODA_MECYL_RCVD_Min"].ToString();
            txtLODA_AECC_RCVD.Text = dr["LODA_AECC_RCVD_Min"].ToString();
            txtLODA_MECC_CNSMP.Text = dr["LODA_MECC_CNSMP_Min"].ToString();
            txtLODA_MECYL_CNSMP.Text = dr["LODA_MECYL_CNSMP_Min"].ToString();
            txtLODA_AECC_CNSMP.Text = dr["LODA_AECC_CNSMP_Min"].ToString();
            txtLODA_MECC_ROB.Text = dr["LODA_MECC_ROB_Min"].ToString();
            txtLODA_MECYL_ROB.Text = dr["LODA_MECYL_ROB_Min"].ToString();
            txtLODA_AECC_ROB.Text = dr["LODA_AECC_ROB_Min"].ToString();

            txtLODA_MECC_ROB_PNN_Max.Text = dr["LODA_MECC_ROB_PNN_Max"].ToString();
            txtLODA_MECYL_ROB_PNN_Max.Text = dr["LODA_MECYL_ROB_PNN_Max"].ToString();
            txtLODA_AECC_ROB_PNN_max.Text = dr["LODA_AECC_ROB_PNN_Max"].ToString();
            txtLODA_MECC_RCVD_Max.Text = dr["LODA_MECC_RCVD_Max"].ToString();
            txtLODA_MECYL_RCVD_Max.Text = dr["LODA_MECYL_RCVD_Max"].ToString();
            txtLODA_AECC_RCVD_Max.Text = dr["LODA_AECC_RCVD_Max"].ToString();
            txtLODA_MECC_CNSMP_Max.Text = dr["LODA_MECC_CNSMP_Max"].ToString();
            txtLODA_MECYL_CNSMP_Max.Text = dr["LODA_MECYL_CNSMP_Max"].ToString();
            txtLODA_AECC_CNSMP_Max.Text = dr["LODA_AECC_CNSMP_Max"].ToString();
            txtLODA_MECC_ROB_Max.Text = dr["LODA_MECC_ROB_Max"].ToString();
            txtLODA_MECYL_ROB_Max.Text = dr["LODA_MECYL_ROB_Max"].ToString();
            txtLODA_AECC_ROB_Max.Text = dr["LODA_AECC_ROB_Max"].ToString();    
        }
    }

    private void ViewTabDetails(int ids)
    {
        if (ids == 1)
        {
            trfoda.Visible = true;
            trdp.Visible = false;
            trworkingh.Visible = false;
            trloda.Visible = false;
            trfwda.Visible = false;
        }
        if (ids == 2)
        {
            trfoda.Visible = false;
            trdp.Visible = false;
            trworkingh.Visible = false;
            trloda.Visible = false;
            trfwda.Visible = true;
        }
        if (ids == 3)
        {
            trfoda.Visible = false;
            trdp.Visible = false;
            trworkingh.Visible = true;
            trloda.Visible = false;
            trfwda.Visible = false;
        }
        if (ids == 4)
        {
            trfoda.Visible = false;
            trdp.Visible = false;
            trworkingh.Visible = false;
            trloda.Visible = true;
            trfwda.Visible = false;
        }
        if (ids == 5)
        {
            trfoda.Visible = false;
            trdp.Visible = true;
            trworkingh.Visible = false;
            trloda.Visible = false;
            trfwda.Visible = false;
        }
        
    }
    private void UpdateERLog(int ids)
    {
        int updateRecords;
        if (ids == 1)
        {
            updateRecords = BLL_Tec_ErLog.ErLog_FODA_THRESHOLD_Update(int.Parse(ViewState["logid"].ToString()), UDFLib.ConvertIntegerToNull(ViewState["VESSELID"].ToString()), UDFLib.ConvertDecimalToNull(txtFODA_HO_ROB_PNN.Text), UDFLib.ConvertDecimalToNull(txtFODA_HO_CONS_ME.Text), UDFLib.ConvertDecimalToNull(txtFODA_HO_CONS_AE.Text), UDFLib.ConvertDecimalToNull(txtFODA_HO_CONS_BLR.Text),
                UDFLib.ConvertDecimalToNull(txtFODA_HO_CONS_TC.Text), UDFLib.ConvertDecimalToNull(txtFODA_HO_CONS_HTG.Text), UDFLib.ConvertDecimalToNull(txtFODA_HO_CONS_TTL.Text), UDFLib.ConvertDecimalToNull(txtFODA_HO_RCVD.Text), UDFLib.ConvertDecimalToNull(txtFODA_HO_AMEND.Text),
                UDFLib.ConvertDecimalToNull(txtFODA_HO_ROB.Text), UDFLib.ConvertDecimalToNull(txtFODA_DO_ROB_PNN.Text), UDFLib.ConvertDecimalToNull(txtFODA_DO_CONS_ME.Text), UDFLib.ConvertDecimalToNull(txtFODA_DO_CONS_AE.Text), UDFLib.ConvertDecimalToNull(txtFODA_DO_CONS_BLR.Text),
                UDFLib.ConvertDecimalToNull(txtFODA_DO_CONS_TC.Text), UDFLib.ConvertDecimalToNull(txtFODA_DO_CONS_HTG.Text), UDFLib.ConvertDecimalToNull(txtFODA_DO_CONS_TTL.Text), UDFLib.ConvertDecimalToNull(txtFODA_DO_RCVD.Text), UDFLib.ConvertDecimalToNull(txtFODA_DO_AMEND.Text),
                UDFLib.ConvertDecimalToNull(txtFODA_DO_ROB.Text), UDFLib.ConvertDecimalToNull(txtFODA_GO_ROB_PNN.Text), UDFLib.ConvertDecimalToNull(txtFODA_GO_CONS_ME.Text), UDFLib.ConvertDecimalToNull(txtFODA_GO_CONS_AE.Text), UDFLib.ConvertDecimalToNull(txtFODA_GO_CONS_BLR.Text),
                UDFLib.ConvertDecimalToNull(txtFODA_GO_CONS_TC.Text), UDFLib.ConvertDecimalToNull(txtFODA_GO_CONS_HTG.Text), UDFLib.ConvertDecimalToNull(txtFODA_GO_CONS_TTL.Text), UDFLib.ConvertDecimalToNull(txtFODA_GO_RCVD.Text), UDFLib.ConvertDecimalToNull(txtFODA_GO_AMEND.Text),
                UDFLib.ConvertDecimalToNull(txtFODA_GO_ROB.Text), UDFLib.ConvertDecimalToNull(txtFODA_HO_ROB_PNN_max.Text), UDFLib.ConvertDecimalToNull(txtFODA_HO_CONS_ME_Max.Text), UDFLib.ConvertDecimalToNull(txtFODA_HO_CONS_AE_Max.Text), UDFLib.ConvertDecimalToNull(txtFODA_HO_CONS_BLR_Max.Text),
                UDFLib.ConvertDecimalToNull(txtFODA_HO_CONS_TC_Max.Text), UDFLib.ConvertDecimalToNull(txtFODA_HO_CONS_HTG_Max.Text), UDFLib.ConvertDecimalToNull(txtFODA_HO_CONS_TTL_Max.Text), UDFLib.ConvertDecimalToNull(txtFODA_HO_RCVD_Max.Text), UDFLib.ConvertDecimalToNull(txtFODA_HO_AMEND_Max.Text),
                UDFLib.ConvertDecimalToNull(txtFODA_HO_ROB_Max.Text), UDFLib.ConvertDecimalToNull(txtFODA_DO_ROB_PNN_Max.Text), UDFLib.ConvertDecimalToNull(txtFODA_DO_CONS_ME_Max.Text), UDFLib.ConvertDecimalToNull(txtFODA_DO_CONS_AE_Max.Text), UDFLib.ConvertDecimalToNull(txtFODA_DO_CONS_BLR_Max.Text),
                UDFLib.ConvertDecimalToNull(txtFODA_DO_CONS_TC_Max.Text), UDFLib.ConvertDecimalToNull(txtFODA_DO_CONS_HTG_Max.Text), UDFLib.ConvertDecimalToNull(txtFODA_DO_CONS_TTL_Max.Text), UDFLib.ConvertDecimalToNull(txtFODA_DO_RCVD_Max.Text), UDFLib.ConvertDecimalToNull(txtFODA_DO_AMEND_Max.Text),
                UDFLib.ConvertDecimalToNull(txtFODA_DO_ROB_Max.Text), UDFLib.ConvertDecimalToNull(txtFODA_GO_ROB_PNN_Max.Text), UDFLib.ConvertDecimalToNull(txtFODA_GO_CONS_ME_Max.Text), UDFLib.ConvertDecimalToNull(txtFODA_GO_CONS_AE_Max.Text), UDFLib.ConvertDecimalToNull(txtFODA_GO_CONS_BLR_Max.Text),
                UDFLib.ConvertDecimalToNull(txtFODA_GO_CONS_TC_Max.Text), UDFLib.ConvertDecimalToNull(txtFODA_GO_CONS_HTG_Max.Text), UDFLib.ConvertDecimalToNull(txtFODA_GO_CONS_TTL_Max.Text), UDFLib.ConvertDecimalToNull(txtFODA_GO_RCVD_Max.Text), UDFLib.ConvertDecimalToNull(txtFODA_GO_AMEND_Max.Text),
                UDFLib.ConvertIntegerToNull(txtFODA_GO_ROB_Max.Text), Convert.ToInt32(Session["USERID"]));
            string js = "alert('Changes are updated ');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Update", js, true);
        }         
        if (ids == 2)
        {

           updateRecords= BLL_Tec_ErLog.ErLog_FWDa_THRESHOLD_Update(int.Parse(ViewState["logid"].ToString()),UDFLib.ConvertIntegerToNull(ViewState["VESSELID"].ToString()), UDFLib.ConvertDecimalToNull(txtFWDA_POT_ROB_PNN.Text), UDFLib.ConvertDecimalToNull(txtFWDA_POT_PROD.Text), UDFLib.ConvertDecimalToNull(txtFWDA_POT_RCVD.Text), UDFLib.ConvertDecimalToNull(txtFWDA_POT_CNSMP.Text), UDFLib.ConvertDecimalToNull(txtFWDA_POT_ROB.Text),
                UDFLib.ConvertDecimalToNull(txtFWDA_WASHP_ROB_PNN.Text), UDFLib.ConvertDecimalToNull(txtFWDA_WASHP_PROD.Text), UDFLib.ConvertDecimalToNull(txtFWDA_WASHP_RCVD.Text), UDFLib.ConvertDecimalToNull(txtFWDA_WASHP_CNSMP.Text), UDFLib.ConvertDecimalToNull(txtFWDA_WASHP_ROB.Text),
                UDFLib.ConvertDecimalToNull(txtFWDA_WASHS_ROB_PNN.Text), UDFLib.ConvertDecimalToNull(txtFWDA_WASHS_PROD.Text), UDFLib.ConvertDecimalToNull(txtFWDA_WASHS_RCVD.Text), UDFLib.ConvertDecimalToNull(txtFWDA_WASHS_CNSMP.Text), UDFLib.ConvertDecimalToNull(txtFWDA_WASHS_ROB.Text),
                UDFLib.ConvertDecimalToNull(txtFWDA_DISTL_ROB_PNN.Text), UDFLib.ConvertDecimalToNull(txtFWDA_DISTL_PROD.Text), UDFLib.ConvertDecimalToNull(txtFWDA_DISTL_RCVD.Text), UDFLib.ConvertDecimalToNull(txtFWDA_DISTL_CNSMP.Text), UDFLib.ConvertDecimalToNull(txtFWDA_DISTL_ROB.Text),
                 UDFLib.ConvertDecimalToNull(txtFWDA_POT_ROB_PNN_Max.Text), UDFLib.ConvertDecimalToNull(txtFWDA_POT_PROD_Max.Text), UDFLib.ConvertDecimalToNull(txtFWDA_POT_RCVD_Max.Text), UDFLib.ConvertDecimalToNull(txtFWDA_POT_CNSMP_Max.Text), UDFLib.ConvertDecimalToNull(txtFWDA_POT_ROB_max.Text),
                UDFLib.ConvertDecimalToNull(txtFWDA_WASHP_ROB_PNN_Max.Text), UDFLib.ConvertDecimalToNull(txtFWDA_WASHP_PROD_Max.Text), UDFLib.ConvertDecimalToNull(txtFWDA_WASHP_RCVD_max.Text), UDFLib.ConvertDecimalToNull(txtFWDA_WASHP_CNSMP_max.Text), UDFLib.ConvertDecimalToNull(txtFWDA_WASHP_ROB_Max.Text),
                UDFLib.ConvertDecimalToNull(txtFWDA_WASHS_ROB_PNN_Max.Text), UDFLib.ConvertDecimalToNull(txtFWDA_WASHS_PROD_Max.Text), UDFLib.ConvertDecimalToNull(txtFWDA_WASHS_RCVD_Max.Text), UDFLib.ConvertDecimalToNull(txtFWDA_WASHS_CNSMP_Max.Text), UDFLib.ConvertDecimalToNull(txtFWDA_WASHS_ROB_Max.Text),
                UDFLib.ConvertDecimalToNull(txtFWDA_DISTL_ROB_PNN_Max.Text), UDFLib.ConvertDecimalToNull(txtFWDA_DISTL_PROD_Max.Text), UDFLib.ConvertDecimalToNull(txtFWDA_DISTL_RCVD_Max.Text), UDFLib.ConvertDecimalToNull(txtFWDA_DISTL_CNSMP_Max.Text), UDFLib.ConvertDecimalToNull(txtFWDA_DISTL_ROB_Max.Text), Convert.ToInt32(Session["USERID"]));
           string js = "alert('Changes are updated ');";
           ScriptManager.RegisterStartupScript(this, this.GetType(), "Update", js, true);
        }
        if (ids == 3)
        {
            updateRecords = BLL_Tec_ErLog.ErLog_WRKHRS_THRESHOLD_Update(int.Parse(ViewState["logid"].ToString()),UDFLib.ConvertIntegerToNull(ViewState["VESSELID"].ToString()), null,UDFLib.ConvertDecimalToNull(txtWRKHRS_ME_NN_Min.Text),null,null, UDFLib.ConvertDecimalToNull(txtWRKHRS_AE1_NN_min.Text),null,null, UDFLib.ConvertDecimalToNull(txtWRKHRS_AE2_NN_min.Text)
                ,null,null,UDFLib.ConvertDecimalToNull(txtWRKHRS_AE3_NN_min.Text),null,null,UDFLib.ConvertDecimalToNull(txtWRKHRS_AE4_NN_min.Text), null,null, UDFLib.ConvertDecimalToNull(txtWRKHRS_TA_NN_Min.Text),null,null,UDFLib.ConvertDecimalToNull(txtWRKHRS_SG_NN_Min.Text)
                ,null,null,UDFLib.ConvertDecimalToNull(txtWRKHRS_ME_NN_max.Text),null,null,UDFLib.ConvertDecimalToNull(txtWRKHRS_AE1_NN_Max.Text),null,null, UDFLib.ConvertDecimalToNull(txtWRKHRS_AE2_NN_Max.Text),null,null, UDFLib.ConvertDecimalToNull(txtWRKHRS_AE3_NN_Max.Text)
                , null, null, UDFLib.ConvertDecimalToNull(txtWRKHRS_AE4_NN_Max.Text), null, null, UDFLib.ConvertDecimalToNull(txtWRKHRS_TA_NN_Max.Text), null, null, UDFLib.ConvertDecimalToNull(txtWRKHRS_SG_NN_Max.Text), null, Convert.ToInt32(Session["USERID"]));
            string js = "alert('Changes are updated ');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Update", js, true);
        }
        if (ids == 4)
        {
            updateRecords = BLL_Tec_ErLog.ErLog_LODA_THRESHOLD_Update(int.Parse(ViewState["logid"].ToString()),UDFLib.ConvertIntegerToNull(ViewState["VESSELID"].ToString()), UDFLib.ConvertDecimalToNull(txtLODA_MECC_ROB_PNN.Text), UDFLib.ConvertDecimalToNull(txtLODA_MECC_RCVD.Text), UDFLib.ConvertDecimalToNull(txtLODA_MECC_CNSMP.Text),
                UDFLib.ConvertDecimalToNull(txtLODA_MECC_ROB.Text),UDFLib.ConvertDecimalToNull(txtLODA_MECYL_ROB_PNN.Text), UDFLib.ConvertDecimalToNull(txtLODA_MECYL_RCVD.Text), UDFLib.ConvertDecimalToNull(txtLODA_MECYL_CNSMP.Text)
                , UDFLib.ConvertDecimalToNull(txtLODA_MECYL_ROB.Text),UDFLib.ConvertDecimalToNull(txtLODA_AECC_ROB_PNN.Text), UDFLib.ConvertDecimalToNull(txtLODA_AECC_RCVD.Text), UDFLib.ConvertDecimalToNull(txtLODA_AECC_CNSMP.Text),
                UDFLib.ConvertDecimalToNull(txtLODA_AECC_ROB.Text), UDFLib.ConvertDecimalToNull(txtLODA_MECC_ROB_PNN_Max.Text), UDFLib.ConvertDecimalToNull(txtLODA_MECC_RCVD_Max.Text), UDFLib.ConvertDecimalToNull(txtLODA_MECC_CNSMP_Max.Text),
                UDFLib.ConvertDecimalToNull(txtLODA_MECC_ROB_Max.Text), UDFLib.ConvertDecimalToNull(txtLODA_MECYL_ROB_PNN_Max.Text), UDFLib.ConvertDecimalToNull(txtLODA_MECYL_RCVD_Max.Text), UDFLib.ConvertDecimalToNull(txtLODA_MECYL_CNSMP_Max.Text),
                UDFLib.ConvertDecimalToNull(txtLODA_MECYL_ROB_Max.Text), UDFLib.ConvertDecimalToNull(txtLODA_AECC_ROB_PNN_max.Text), UDFLib.ConvertDecimalToNull(txtLODA_AECC_RCVD_Max.Text), UDFLib.ConvertDecimalToNull(txtLODA_AECC_CNSMP_Max.Text),
                UDFLib.ConvertDecimalToNull(txtLODA_AECC_ROB_Max.Text), Convert.ToInt32(Session["USERID"]));
            string js = "alert('Changes are updated ');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Update", js, true);
        }
        if (ids == 5)
        {
            //updateRecords = BLL_Tec_ErLog.(int.Parse(ViewState["logid"].ToString()), UDFLib.ConvertIntegerToNull(txtDP_WIND_FORCE.Text), txtDP_WIND_DIR.Text, txtDP_SEA_COND.Text, UDFLib.ConvertIntegerToNull(txtDP_SWELL.Text), txtDP_SWELL_DIR.Text, txtDP_CURR.Text, UDFLib.ConvertIntegerToNull(txtDP_REVS_NTN.Text)
            //     , UDFLib.ConvertIntegerToNull(txtDP_AVG_RPM.Text), UDFLib.ConvertIntegerToNull(txtDP_ENG_DIST.Text), UDFLib.ConvertIntegerToNull(txtDP_OBS_DIST.Text), UDFLib.ConvertIntegerToNull(txtDP_TTL_DIST.Text), UDFLib.ConvertIntegerToNull(txtDP_HRS_FUL_SPD.Text), UDFLib.ConvertIntegerToNull(txtDP_SLIP.Text),
            //     UDFLib.ConvertIntegerToNull(txtDP_DIST_TOGO.Text), txtDP_HRS_RED_SPD.Text, UDFLib.ConvertIntegerToNull(txtDP_HRS_STOPD.Text), UDFLib.ConvertIntegerToNull(txtDP_OBS_SPD.Text), UDFLib.ConvertDateToNull(txtETA.Text),"", 1);
            //string js = "alert('Changes are updated ');";
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "Update", js, true);
        }

           
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (ViewState["ViewID"] != null && ViewState["logid"] != null )
        {
            UpdateERLog(int.Parse(ViewState["ViewID"].ToString()));  
        }

    }
    protected void UserAccessValidation()
    {

        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        objUA = objUser.Get_UserAccessForPage(Convert.ToInt32(Session["USERID"]), PageURL);
        if (objUA.Edit == 0)
        {
            btnSave.Enabled = false; 
            
        }

        if (objUA.View == 0)
        {
            Response.Redirect("~/crew/default.aspx?msgid=1");
        }

        if (objUA.Add == 0)
        {

        }
    }
}