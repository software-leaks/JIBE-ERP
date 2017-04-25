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
using System.Text;
using System.Globalization;

using SMS.Business.Infrastructure;




public partial class EditVesselDetails : System.Web.UI.Page
{
    BLL_Infra_VesselLib objBLL = new BLL_Infra_VesselLib();
    IFormatProvider iFormatProvider = CultureInfo.GetCultureInfo("en-GB");

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            StringBuilder transmittedQueryStrings = new StringBuilder();
            string x = "";
            string y = "";
            foreach (string key in Request.QueryString.AllKeys)
            {
                if (transmittedQueryStrings.Length > 0)
                    transmittedQueryStrings.Append("&amp;");
                transmittedQueryStrings.Append(string.Format("{0}={1}", key, Request.QueryString[key]));
                x = string.Format("{0}", key).ToUpper();
                y = string.Format("{0}", Request.QueryString[key]).ToUpper();
            }

            if (x == "ADDIDD")
            {
                ViewVesselDetails(System.Convert.ToInt32(y.ToString().Trim()));

            }
            if (x == "VIEWID")
            {
                ViewVesselDetails(System.Convert.ToInt32(y.ToString().Trim()));

            }
        }
    }

    public void ViewVesselDetails(int Key)
    {
        try
        {

            DataTable dtVessel = objBLL.GetVesselDetails_ByID(Key);

            lblShipName.Text = dtVessel.Rows[0]["Vessel_Name"].ToString();

            txtExName.Text = dtVessel.Rows[0]["Vessel_Ex_Name1"].ToString();
            txtVeseelCode.Text = dtVessel.Rows[0]["Vessel_Code"].ToString();
            txtAccCode.Text = dtVessel.Rows[0]["BANK_ACCOUNT_ID"].ToString();
            txtVesselOwner.Text = dtVessel.Rows[0]["Vessel_Owner"].ToString();
            txtOperator.Text = dtVessel.Rows[0]["Vessel_Operator"].ToString();
            DDLFlag.SelectedItem.Text = dtVessel.Rows[0]["Vessel_Flag"].ToString();
            txtCallSign.Text = dtVessel.Rows[0]["Vessel_Call_sign"].ToString();
            txtIMO.Text = dtVessel.Rows[0]["Vessel_IMO_No"].ToString();
            txtOffcialNo.Text = dtVessel.Rows[0]["Vessel_Official_No"].ToString();

            DDLClass.SelectedItem.Text = dtVessel.Rows[0]["Vessel_Class"].ToString();
            txtClassNo.Text = dtVessel.Rows[0]["Vessel_Class_No"].ToString();
            txtServSpeed.Text = dtVessel.Rows[0]["Vessel_Serv_Speed"].ToString();
            DDLVesselType.SelectedItem.Text = dtVessel.Rows[0]["Vessel_Type"].ToString();
            DDLTankSize.SelectedItem.Text = dtVessel.Rows[0]["Vessel_Size"].ToString();

            if (dtVessel.Rows[0]["Vessel_Keel_laid_Date"].ToString() != "")
            {               
                txtKeellaiddt.Text = dtVessel.Rows[0]["Vessel_Keel_laid_Date"].ToString().ToString(iFormatProvider);
            }
            if (dtVessel.Rows[0]["Vessel_Delvry_Date"].ToString() != "")
            {
                 txtDlvrydt.Text = dtVessel.Rows[0]["Vessel_Delvry_Date"].ToString().ToString(iFormatProvider);
            }

           
            txtyard.Text = dtVessel.Rows[0]["Vessel_Yard"].ToString();
            txtHullNo.Text = dtVessel.Rows[0]["Vessel_Hull_No"].ToString();
            DDLHullType.SelectedItem.Text = dtVessel.Rows[0]["Vessel_Hull_Type"].ToString();
            txtLength.Text = dtVessel.Rows[0]["Vessel_Length_OA"].ToString();
            txtBpLength.Text = dtVessel.Rows[0]["Vessel_Length_BP"].ToString();
            txtDepth.Text = dtVessel.Rows[0]["Vessel_Depth"].ToString();
            txtBreadth.Text = dtVessel.Rows[0]["Vessel_Breadth"].ToString();
            txtMastTopfrmKeel.Text = dtVessel.Rows[0]["Vessel_Mast_Top_Keel"].ToString();
            txtMMSI.Text = dtVessel.Rows[0]["Vessel_MMSI_No"].ToString();
            txtCargoTkCap.Text = dtVessel.Rows[0]["Vessel_Cargo_Tk_Cap"].ToString();
            txtSloptkCap.Text = dtVessel.Rows[0]["vessel_Slop_Tk_Cap"].ToString();
            txtBallastTkCap.Text = dtVessel.Rows[0]["Vessel_Ballast_Tk_Cap"].ToString();
            txtDw_Trop.Text = dtVessel.Rows[0]["Dwt_Tropical"].ToString();
            txtDw_Summ.Text = dtVessel.Rows[0]["Dwt_Summer"].ToString();
            txt_Dw_wint.Text = dtVessel.Rows[0]["Dwt_winter"].ToString();
            txtDw_Ballast.Text = dtVessel.Rows[0]["Dwt_Ballast"].ToString();
            txtDisp_Trop.Text = dtVessel.Rows[0]["Disp_Tropical"].ToString();
            txtDisp_summ.Text = dtVessel.Rows[0]["Disp_Summer"].ToString();
            txtDisp_Winter.Text = dtVessel.Rows[0]["Disp_Winter"].ToString();
            txtDisp_Ballast.Text = dtVessel.Rows[0]["Disp_Ballasr"].ToString();
            txtdrft_trop.Text = dtVessel.Rows[0]["Draft_Tropical"].ToString();
            txtdrft_summ.Text = dtVessel.Rows[0]["Draft_Summer"].ToString();
            txtdrft_wint.Text = dtVessel.Rows[0]["Draft_winter"].ToString();
            txtdrft_Ballast.Text = dtVessel.Rows[0]["Draft_Ballast"].ToString();
            txgrttInter.Text = dtVessel.Rows[0]["Grt_International"].ToString();
            txtGrtSuez.Text = dtVessel.Rows[0]["Grt_Suez"].ToString();
            txtGrtpanama.Text = dtVessel.Rows[0]["Grt_Panama"].ToString();
            txtNrtInter.Text = dtVessel.Rows[0]["NRT_International"].ToString();
            txtNrtSuez.Text = dtVessel.Rows[0]["NRT_Suez"].ToString();
            txtNrtPanama.Text = dtVessel.Rows[0]["NRT_Panama"].ToString();
            txtLwtInter.Text = dtVessel.Rows[0]["LWT_International"].ToString();
            txtLwtSuez.Text = dtVessel.Rows[0]["LWT_Suez"].ToString();
            txtLwtPanama.Text = dtVessel.Rows[0]["LWT_Panama"].ToString();
            txtME.Text = dtVessel.Rows[0]["Vessel_MainEngine"].ToString();
            txtAuxBoil.Text = dtVessel.Rows[0]["Vessel_Aux_Boiler"].ToString();
            txtAuxKw.Text = dtVessel.Rows[0]["Vessel_ABLR_Cap"].ToString();
            txtMCR.Text = dtVessel.Rows[0]["Vessel_ME_MCR"].ToString();
            txtNCR.Text = dtVessel.Rows[0]["Vessel_ME_NCR"].ToString();
            txtCopCap.Text = dtVessel.Rows[0]["Vessel_Cops_Cap"].ToString();
            txtAunEng.Text = dtVessel.Rows[0]["Aux_Engine"].ToString();
            DDLDeckMech.SelectedItem.Text = dtVessel.Rows[0]["Vessel_Deck_Mache"].ToString();
            txtAuxKw.Text = dtVessel.Rows[0]["Vessel_AE_KW"].ToString();
            txtturbEng.Text = dtVessel.Rows[0]["Vessel_Turb_Genrt"].ToString();
            txtturbengKw.Text = dtVessel.Rows[0]["Vessel_TG_KW"].ToString();
            if (dtVessel.Rows[0]["Dry_dock_Last"].ToString() != "")
            {
                txtDrtLast.Text = (DateTime.Parse(dtVessel.Rows[0]["Dry_dock_Last"].ToString())).ToShortDateString(); ;
            }
            if (dtVessel.Rows[0]["Dry_dock_Next"].ToString() != "")
            {
                txtDryNext.Text = (DateTime.Parse(dtVessel.Rows[0]["Dry_dock_Next"].ToString())).ToShortDateString(); ;
            }
            if (dtVessel.Rows[0]["Dry_dock_Latest"].ToString() != "")
            {
                txtDryLatest.Text = (DateTime.Parse(dtVessel.Rows[0]["Dry_dock_Latest"].ToString())).ToShortDateString();
            }
            if (dtVessel.Rows[0]["Spl_Svry_Last"].ToString() != "")
            {
                txtSplLast.Text = (DateTime.Parse(dtVessel.Rows[0]["Spl_Svry_Last"].ToString())).ToShortDateString(); ;
            }
            if (dtVessel.Rows[0]["Spl_Svry_Next"].ToString() != "")
            {
                txtSplNext.Text = (DateTime.Parse(dtVessel.Rows[0]["Spl_Svry_Next"].ToString())).ToShortDateString(); ;
            }
            if (dtVessel.Rows[0]["Spl_Svry_Latest"].ToString() != "")
            {
                txtSplLatest.Text = (DateTime.Parse(dtVessel.Rows[0]["Spl_Svry_Latest"].ToString())).ToShortDateString(); ;
            }
            if (dtVessel.Rows[0]["Tailshft_Svry_Last"].ToString() != "")
            {
                txtTailLast.Text = (DateTime.Parse(dtVessel.Rows[0]["Tailshft_Svry_Last"].ToString())).ToShortDateString(); ;
            }
            if (dtVessel.Rows[0]["Tailshft_Svry_Next"].ToString() != "")
            {
                txtTailNext.Text = (DateTime.Parse(dtVessel.Rows[0]["Tailshft_Svry_Next"].ToString())).ToShortDateString(); ;
            }
            if (dtVessel.Rows[0]["Tailshft_Svry_Latest"].ToString() != "")
            {
                txtTailLatest.Text = (DateTime.Parse(dtVessel.Rows[0]["Tailshft_Svry_Latest"].ToString())).ToShortDateString(); ;
            }

            ShipImg.ImageUrl = "ShipImage/" + dtVessel.Rows[0]["Vessel_Image"].ToString();
            Session["Shipimage"] = dtVessel.Rows[0]["Vessel_Image"].ToString();
            TankImg.ImageUrl = "shipLayout/" + dtVessel.Rows[0]["Vessel_Tank_Image"].ToString();
            Session["TankImg"] = dtVessel.Rows[0]["Vessel_Tank_Image"].ToString();
            
            txtemail.Text = dtVessel.Rows[0]["Vessel_email"].ToString();
        }
        catch (Exception ex)
        {
            //string js = ex.Message;
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string filepath = "";
        string Shipimage = "";
        if (ShipImageUpload.HasFile)
        {
            filepath = Server.MapPath("shipImage\\" + txtVeseelCode.Text + ShipImageUpload.FileName);
            ShipImageUpload.SaveAs(filepath);
            Shipimage = txtVeseelCode.Text + ShipImageUpload.FileName;
        }
        else
        {
            Shipimage = Session["Shipimage"].ToString();
        }
        string TankImagePath = "";
        string TankImage = "";
        if (TankImageUpload.HasFile)
        {
            TankImagePath = Server.MapPath("ShipLayout\\" + txtVeseelCode.Text + TankImageUpload.FileName);
            TankImageUpload.SaveAs(TankImagePath);
            TankImage = txtVeseelCode.Text + TankImageUpload.FileName;
        }
        else
        {
            TankImage = Session["TankImg"].ToString();
        }


        try
        {
            //IeLogService_INFClient ObjElog = Global.Client;

            //Vessel objNewVessel = new Vessel();

            //objNewVessel.EXNAME1 = txtExName.Text;
            //objNewVessel.EXNAME2 = txtExName.Text;
            //objNewVessel.EXNAME3 = txtExName.Text;
            //objNewVessel.EXNAME4 = txtExName.Text;
            //objNewVessel.VESSELCODE = txtVeseelCode.Text;
            //objNewVessel.ACC_CODE = int.Parse(txtAccCode.Text);
            //objNewVessel.OWNER = txtVesselOwner.Text;
            //objNewVessel.OPERATOR = txtOperator.Text;
            //objNewVessel.FLAG = DDLFlag.SelectedItem.Text;
            //objNewVessel.CALLSIGN = txtCallSign.Text;
            //objNewVessel.IMONO = int.Parse(txtIMO.Text);
            //objNewVessel.OFCNO = int.Parse(txtOffcialNo.Text);
            //objNewVessel.CLASSNAME = DDLClass.SelectedItem.Text;
            //objNewVessel.CLASSNO = txtClassNo.Text;
            //objNewVessel.SERVSPEED = float.Parse(txtServSpeed.Text);
            //objNewVessel.VESSELTYPE = DDLVesselType.SelectedItem.Text;
            //objNewVessel.SIZE = DDLTankSize.SelectedItem.Text;
            //objNewVessel.KEELDT = txtKeellaiddt.Text.ToString().Trim();
            //objNewVessel.DLVRYDT = txtDlvrydt.Text.ToString().Trim();
            //objNewVessel.VESSELYARD = txtyard.Text;
            //objNewVessel.HULLNO = int.Parse(txtHullNo.Text);
            //objNewVessel.HULLLTYPE = DDLHullType.SelectedItem.Text;
            //objNewVessel.LENGTHOA = float.Parse(txtLength.Text);
            //objNewVessel.LENGTHBP = float.Parse(txtBpLength.Text);
            //objNewVessel.DEPTH = float.Parse(txtDepth.Text);
            //objNewVessel.BREADTH = float.Parse(txtBreadth.Text);
            //objNewVessel.MASTKEEL = float.Parse(txtMastTopfrmKeel.Text);
            //objNewVessel.MMISNO = int.Parse(txtMMSI.Text);
            //objNewVessel.CARGOTKCAP = float.Parse(txtCargoTkCap.Text);
            //objNewVessel.SLOPTKCAP = float.Parse(txtSloptkCap.Text);
            //objNewVessel.BALLASTTKCAP = float.Parse(txtBallastTkCap.Text);
            //objNewVessel.DW_TROP = float.Parse(txtDw_Trop.Text);
            //objNewVessel.DW_SUMM = (float)Convert.ToSingle(txtDw_Summ.Text);
            //objNewVessel.DW_WINT = float.Parse(txt_Dw_wint.Text);
            //objNewVessel.DW_BALLAST = float.Parse(txtDw_Ballast.Text);
            //objNewVessel.DISP_TROP = float.Parse(txtDisp_Trop.Text);
            //objNewVessel.DISP_SUMM = float.Parse(txtDisp_summ.Text);
            //objNewVessel.DISP_WINT = float.Parse(txtDisp_Winter.Text);
            //objNewVessel.DISP_BALLAST = float.Parse(txtDisp_Ballast.Text);
            //objNewVessel.DRFT_TROP = float.Parse(txtdrft_trop.Text);
            //objNewVessel.DRFT_SUMM = float.Parse(txtdrft_summ.Text);
            //objNewVessel.DRFT_WINT = float.Parse(txtdrft_wint.Text);
            //objNewVessel.DRFT_BALLAST = float.Parse(txtdrft_Ballast.Text);
            //objNewVessel.GRT_INTER = float.Parse(txgrttInter.Text);
            //objNewVessel.GRT_SUEZ = float.Parse(txtGrtSuez.Text);
            //objNewVessel.GRT_PANAMA = float.Parse(txtGrtpanama.Text);
            //objNewVessel.NRT_INTER = float.Parse(txtNrtInter.Text);
            //objNewVessel.NRT_SUEZ = float.Parse(txtNrtSuez.Text);
            //objNewVessel.NRT_PANAM = float.Parse(txtNrtPanama.Text);
            //objNewVessel.LWT_INTER = float.Parse(txtLwtInter.Text);
            //objNewVessel.LWT_SUEZ = float.Parse(txtLwtSuez.Text);
            //objNewVessel.LWT_PANAMA = float.Parse(txtLwtPanama.Text);
            //objNewVessel.AUX_BOILER = txtME.Text;
            //objNewVessel.AUX_BOILER = txtAuxBoil.Text;
            //objNewVessel.AE_KW = txtAuxKw.Text;
            //objNewVessel.ME_MCR = txtMCR.Text;
            //objNewVessel.ME_NCR = txtNCR.Text;
            //objNewVessel.COP_CAP = txtCopCap.Text;
            //objNewVessel.AUX_ENGINE = txtAunEng.Text;
            //objNewVessel.DECK_MECH = DDLDeckMech.SelectedItem.Text;
            //objNewVessel.AE_KW = txtAuxKw.Text;
            //objNewVessel.TURB_GENT = txtturbEng.Text;
            //objNewVessel.TG_KW = txtturbengKw.Text;
            //objNewVessel.DRY_LAST = txtDrtLast.Text;
            //objNewVessel.DRY_NEXT = txtDryNext.Text.ToString().Trim();
            //objNewVessel.DRY_LATEST = txtDryLatest.Text.ToString().Trim();
            //objNewVessel.SPL_LAST = txtSplLast.Text.ToString().Trim();
            //objNewVessel.SPL_NEXT = txtSplNext.Text.ToString().Trim();
            //objNewVessel.SPL_LATEST = txtSplLatest.Text.ToString().Trim();
            //objNewVessel.TAIL_LAST = txtTailLast.Text.ToString().Trim();
            //objNewVessel.TAIL_NEXT = txtTailNext.Text.ToString().Trim();
            //objNewVessel.TAIL_LATEST = txtTailLatest.Text.ToString().Trim();
            //objNewVessel.SHIPIMAGE = Shipimage;
            //objNewVessel.TANKIMAGE = TankImage;
            //objNewVessel.EMAIL = txtemail.Text;


            objBLL.AddVesselDetails(txtExName.Text, txtExName.Text, txtExName.Text, txtExName.Text, txtVeseelCode.Text, int.Parse(txtAccCode.Text), txtVesselOwner.Text, txtOperator.Text,
                             DDLFlag.SelectedItem.Text, txtCallSign.Text, int.Parse(txtIMO.Text), int.Parse(txtOffcialNo.Text), DDLClass.SelectedItem.Text,
                             txtClassNo.Text, float.Parse(txtServSpeed.Text), DDLVesselType.SelectedItem.Text, DDLTankSize.SelectedItem.Text, txtKeellaiddt.Text.ToString().Trim(),
                             txtDlvrydt.Text.ToString().Trim(), txtyard.Text, int.Parse(txtHullNo.Text), DDLHullType.SelectedItem.Text, float.Parse(txtLength.Text), float.Parse(txtBpLength.Text),
                             float.Parse(txtDepth.Text), float.Parse(txtBreadth.Text), float.Parse(txtMastTopfrmKeel.Text), int.Parse(txtMMSI.Text), float.Parse(txtCargoTkCap.Text), float.Parse(txtSloptkCap.Text),
                             float.Parse(txtBallastTkCap.Text), float.Parse(txtDw_Trop.Text), (float)Convert.ToSingle(txtDw_Summ.Text), float.Parse(txt_Dw_wint.Text), float.Parse(txtDw_Ballast.Text), float.Parse(txtDisp_Trop.Text),
                             float.Parse(txtDisp_summ.Text), float.Parse(txtDisp_Winter.Text), float.Parse(txtDisp_Ballast.Text), float.Parse(txtdrft_trop.Text), float.Parse(txtdrft_summ.Text), float.Parse(txtdrft_wint.Text),
                             float.Parse(txtdrft_Ballast.Text), float.Parse(txgrttInter.Text), float.Parse(txtGrtSuez.Text), float.Parse(txtGrtpanama.Text), float.Parse(txtNrtInter.Text), float.Parse(txtNrtSuez.Text),
                             float.Parse(txtNrtPanama.Text), float.Parse(txtLwtInter.Text), float.Parse(txtLwtSuez.Text), float.Parse(txtLwtPanama.Text), txtME.Text, txtAuxBoil.Text, txtAuxKw.Text, txtMCR.Text, txtNCR.Text, txtCopCap.Text,
                             txtAunEng.Text, DDLDeckMech.SelectedItem.Text, txtAuxKw.Text, txtturbEng.Text, txtturbengKw.Text, txtDrtLast.Text.ToString().Trim(), txtDryNext.Text.ToString().Trim(), txtDryLatest.Text.ToString().Trim(),
                             txtSplLast.Text.ToString().Trim(), txtSplNext.Text.ToString().Trim(), txtSplLatest.Text.ToString().Trim(), txtTailLast.Text.ToString().Trim(), txtTailNext.Text.ToString().Trim(), txtTailLatest.Text.ToString().Trim(), Shipimage, TankImage, 2, txtemail.Text);


            EmptyTexbox(this);
        }
        catch (Exception ex)
        {
        }

    }

    public void EmptyTexbox(Control parent)
    {
        foreach (Control c in parent.Controls)
        {
            if (c.Controls.Count > 0)
            {
                EmptyTexbox(c);
            }
            else
            {

                if (c is TextBox)
                {
                    ((TextBox)c).Text = "";

                }
                if (c is DropDownList)
                {
                    ((DropDownList)c).SelectedIndex = 0;
                }
            }
        }
    }
}







