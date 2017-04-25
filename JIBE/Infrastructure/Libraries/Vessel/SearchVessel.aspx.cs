using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Collections.Specialized;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using SMS.Business.Crew;
using SMS.Business.Infrastructure;
using SMS.Properties;

public partial class SearchVessel : System.Web.UI.Page
{
    BLL_Infra_VesselLib objBLL = new BLL_Infra_VesselLib();
    BLL_Infra_Company objBLLComp = new BLL_Infra_Company();
    BLL_Crew_CrewDetails objCrew = new BLL_Crew_CrewDetails();
    BLL_Infra_UploadFileSize objUploadFilesize = new BLL_Infra_UploadFileSize();

    UserAccess objUA = new UserAccess();

    public string OperationMode = "";
    public Boolean uaEditFlag = false;
    public Boolean uaDeleteFlage = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        UserAccessValidation();
        if (!IsPostBack)
        {
            try
            {

                ViewState["SORTDIRECTION"] = null;
                ViewState["SORTBYCOLOUMN"] = null;

                ucCustomPagerItems.PageSize = 20;
                FillDDLTYPE();
                FillDDL();
                Load_VesselType();
                Load_FleetList();
                Load_FleetList_AddVessel();
                Load_VesselList();
                if (Session["USERCOMPANYID"] != null)
                {
                    ddlFilterVesselManager.SelectedValue = Session["USERCOMPANYID"].ToString();
                }
                BindVesselFlagDDL();
                BindVesselGrid();
            }
            catch
            {
                throw;
            }
        }
    }

    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path);

        BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
            Response.Redirect("~/default.aspx?msgid=1");

        if (objUA.Add == 0) ImgAddVessel.Enabled = false;
        if (objUA.Edit == 1)
            uaEditFlag = true;
        else
            btnSaveAndAdd.Visible = false;
        if (objUA.Delete == 1) uaDeleteFlage = true;

        if (objUA.Admin == 1)
        {
            trShortName.Visible = true;
            btnSaveAndAdd.Visible = true;
        }
        else
        {
            trShortName.Visible = false;
            btnSaveAndAdd.Visible = false;
        }

        trShortName.Visible = true;
        btnSaveAndAdd.Visible = true;
        uaEditFlag = true;
    }

    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }

    public void BindVesselGrid()
    {

        int rowcount = ucCustomPagerItems.isCountRecord;

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

        DataTable dt = objBLL.SearchVessel(txtfilter.Text != "" ? txtfilter.Text : null, UDFLib.ConvertIntegerToNull(DDLFleet.SelectedValue), UDFLib.ConvertIntegerToNull(DDLVessel.SelectedValue)
            , UDFLib.ConvertIntegerToNull(ddlFilterVesselManager.SelectedValue), UDFLib.ConvertIntegerToNull(DDLVesselFlag.SelectedValue), UDFLib.ConvertIntegerToNull(ddlFilterVesselManager.SelectedValue), null, UDFLib.ConvertIntegerToNull(ddlvesselType.SelectedValue)
            , sortbycoloumn, sortdirection, ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);

        if (ucCustomPagerItems.isCountRecord == 1)
        {
            ucCustomPagerItems.CountTotalRec = rowcount.ToString();
            ucCustomPagerItems.BuildPager();
        }

        if (dt.Rows.Count > 0)
        {
            VesselGridView.DataSource = dt;
            VesselGridView.DataBind();
        }
        else
        {
            VesselGridView.DataSource = dt;
            VesselGridView.DataBind();
        }
    }
    public void Load_VesselType()
    {

        DataTable dtVesselType = objBLL.Get_VesselType();


        ddlvessel_AddType.DataSource = dtVesselType;
        ddlvessel_AddType.DataTextField = "VesselTypes";
        ddlvessel_AddType.DataValueField = "ID";
        ddlvessel_AddType.DataBind();
        ddlvessel_AddType.Items.Insert(0, new ListItem("-Select-", "0"));
    }
    public void FillDDLTYPE()
    {
        try
        {


            DataTable dtVesselType = objBLL.Get_VesselType();


            ddlvesselType.DataSource = dtVesselType;
            ddlvesselType.DataTextField = "VesselTypes";
            ddlvesselType.DataValueField = "ID";
            ddlvesselType.DataBind();
            ddlvesselType.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));


        }
        catch (Exception ex)
        {
        }
    }
    public void FillDDL()
    {
        try
        {

            /* Company Type  5 = VESSEL MANAGER  */
            DataTable dtComp = objBLLComp.Get_CompanyListByType(5, UDFLib.ConvertToInteger(Session["USERCOMPANYID"].ToString()));

            //int UserCompanyID = UDFLib.ConvertToInteger(Session["USERCOMPANYID"].ToString());
            //DataTable dtComp = objCrew.Get_VesselManagerList(int.Parse(Session["USERCOMPANYID"].ToString()));

            ddlFilterVesselManager.DataSource = dtComp;
            ddlFilterVesselManager.DataTextField = "COMPANY_NAME";
            ddlFilterVesselManager.DataValueField = "ID";
            ddlFilterVesselManager.DataBind();
            ddlFilterVesselManager.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));


            ddlVesselManager.DataSource = dtComp;
            ddlVesselManager.DataTextField = "COMPANY_NAME";
            ddlVesselManager.DataValueField = "ID";
            ddlVesselManager.DataBind();
            ddlVesselManager.Items.Insert(0, new ListItem("-Select-", "0"));

        }
        catch (Exception ex)
        {
        }
    }

    public void BindVesselFlagDDL()
    {



        DataTable dt = objBLL.Get_VesselFlagList(UDFLib.ConvertToInteger(Session["USERCOMPANYID"].ToString()));

        DDLVesselFlag.DataSource = dt;
        DDLVesselFlag.DataTextField = "flag_name";
        DDLVesselFlag.DataValueField = "vessel_flag";
        DDLVesselFlag.DataBind();
        DDLVesselFlag.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));



        ddlVesselFlage_AddVessel.DataSource = dt;
        ddlVesselFlage_AddVessel.DataTextField = "flag_name";
        ddlVesselFlage_AddVessel.DataValueField = "vessel_flag";
        ddlVesselFlage_AddVessel.DataBind();
        ddlVesselFlage_AddVessel.Items.Insert(0, new ListItem("-Select-", "0"));




    }

    protected void FillGridViewAfterSearch()
    {
        int UserCompanyID = UDFLib.ConvertToInteger(Session["USERCOMPANYID"].ToString());
        DataTable dt = objBLL.Get_VesselList(UDFLib.ConvertToInteger(DDLFleet.SelectedValue), UDFLib.ConvertToInteger(DDLVessel.SelectedValue), UDFLib.ConvertToInteger(ddlFilterVesselManager.SelectedValue), txtfilter.Text, UserCompanyID);
        VesselGridView.DataSource = dt;
        VesselGridView.DataBind();

    }

    protected void BtnSearchName_Click(object sender, EventArgs e)
    {
        FillGridViewAfterSearch();
    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {


        BindVesselGrid();

    }

    protected void DDLFleet_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Load_VesselList();

        }
        catch (Exception ex)
        {
        }
    }

    
    protected void btnsave_Click(object sender, EventArgs e)
    {
        //FilterRefreash();
        lblMessage.Text = "";
        string strLocalPath = FileUploader.PostedFile.FileName;
        
        string strVslImgPath = VesselImageUploader.PostedFile.FileName;
       
        string FileName = "";
        string VslImg_FileName = "";
        DataTable dt = new DataTable();
        dt = objUploadFilesize.Get_Module_FileUpload("INF_");
        string datasize = dt.Rows[0]["Size_KB"].ToString();

        /*Added BY PRANALI_07032015 TO SAVE VESSEL IMAGE ATTACHMENT IN TABLE AND UPLOADS/VESSELIMAGE FOLDER.*/
        #region GET VESSEL IMAGE FILE NAME.
        if (VesselImageUploader.HasFile)
        {
            VslImg_FileName = txtVesselShortName.Text + "_" + DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss") + "_" +"VesselImage"+ Path.GetExtension(strVslImgPath);
            VesselImageUploader.PostedFile.SaveAs(Server.MapPath("~/Uploads/VesselImage/" + VslImg_FileName));
        }
        #endregion

        #region FINAL SAVE CODE.
        int Retval = 0;
        if (HiddenFlag.Value == "Add")
        {           
            if (FileUploader.HasFile)
            {
                if (FileUploader.PostedFile.ContentLength < Int32.Parse(dt.Rows[0]["Size_KB"].ToString()) * 1024)
                {
                    FileName = txtVesselShortName.Text + "_" + DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss") + "_" + "PowerCurve" + Path.GetExtension(strLocalPath);

                    Retval = objBLL.INSERT_New_Vessel_WithImageName(null, txtVessel.Text, txtVesselShortName.Text, txtEmailID.Text, UDFLib.ConvertToInteger(ddlFleet_AddVessel.SelectedValue),
                        UDFLib.ConvertToInteger(ddlVesselManager.SelectedValue), UDFLib.ConvertDateToNull(dtTakeoverDate.Text),
                        UDFLib.ConvertDateToNull(dtHandoverDate.Text), UDFLib.ConvertIntegerToNull(ddlVesselFlage_AddVessel.SelectedValue), Convert.ToInt32(Session["UserID"].ToString()),
                        UDFLib.ConvertDecimalToNull(txtMinimumCTM.Text), chkSyncEnable.Checked == true ? "-1" : "0", FileName, (VslImg_FileName == "" ? null : VslImg_FileName), UDFLib.ConvertIntegerToNull(ddlvessel_AddType.SelectedValue), Convert.ToBoolean(chkIsVessel.Checked), txtImoNo.Text, txtCallSign.Text);
                    
                     FileUploader.PostedFile.SaveAs(Server.MapPath("~/Uploads/MEPowerCurve/" + FileName));
                }
                else
                {
                    lblMessage1.Text = datasize + " KB File size exceeds maximum limit";
                }
            }
            else
            {
                FileName = txtVesselShortName.Text + "_" + DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss") + "_" + "PowerCurve" + Path.GetExtension(strLocalPath); 
                Retval = objBLL.INSERT_New_Vessel_WithImageName(null, txtVessel.Text, txtVesselShortName.Text, txtEmailID.Text, UDFLib.ConvertToInteger(ddlFleet_AddVessel.SelectedValue),
                     UDFLib.ConvertToInteger(ddlVesselManager.SelectedValue), UDFLib.ConvertDateToNull(dtTakeoverDate.Text),
                     UDFLib.ConvertDateToNull(dtHandoverDate.Text), UDFLib.ConvertIntegerToNull(ddlVesselFlage_AddVessel.SelectedValue), Convert.ToInt32(Session["UserID"].ToString()),
                     UDFLib.ConvertDecimalToNull(txtMinimumCTM.Text), chkSyncEnable.Checked == true ? "-1" : "0", null, (VslImg_FileName == "" ? null : VslImg_FileName), UDFLib.ConvertIntegerToNull(ddlvessel_AddType.SelectedValue), Convert.ToBoolean(chkIsVessel.Checked), txtImoNo.Text, txtCallSign.Text);
            }
        }
        else
            if (FileUploader.HasFile)
            {
                if (FileUploader.PostedFile.ContentLength < Int32.Parse(dt.Rows[0]["Size_KB"].ToString()) * 1024)
                {
                    FileName = txtVesselShortName.Text + "_" + DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss") + "_" + "PowerCurve" + Path.GetExtension(strLocalPath);
                    if (!VesselImageUploader.HasFile)
                        VslImg_FileName = lnkVslImage.Text;
                    Retval = objBLL.UPDATE_Vessel_WithImageName(Convert.ToInt32(txtVesselID.Text), null, txtVessel.Text, txtVesselShortName.Text, txtEmailID.Text, Convert.ToInt32(ddlFleet_AddVessel.SelectedValue)
                     , Convert.ToInt32(ddlVesselManager.SelectedValue), UDFLib.ConvertDateToNull(dtTakeoverDate.Text),
                     UDFLib.ConvertDateToNull(dtHandoverDate.Text), UDFLib.ConvertIntegerToNull(ddlVesselFlage_AddVessel.SelectedValue), Convert.ToInt32(Session["UserID"].ToString()),
                     UDFLib.ConvertDecimalToNull(txtMinimumCTM.Text), chkSyncEnable.Checked == true ? "-1" : "0", FileName, (VslImg_FileName == "" ? null : VslImg_FileName), UDFLib.ConvertIntegerToNull(ddlvessel_AddType.SelectedValue),Convert.ToBoolean(chkIsVessel.Checked),txtImoNo.Text,txtCallSign.Text);
                    FileUploader.PostedFile.SaveAs(Server.MapPath("~/Uploads/MEPowerCurve/" + FileName));
                }
                else
                {
                    lblMessage1.Text = datasize + " KB File size exceeds maximum limit";
                }
            }
            else
            {
                FileName = lnkAttachment.Text;
                if (!VesselImageUploader.HasFile)
                VslImg_FileName = lnkVslImage.Text;

                Retval = objBLL.UPDATE_Vessel_WithImageName(Convert.ToInt32(txtVesselID.Text), null, txtVessel.Text, txtVesselShortName.Text, txtEmailID.Text, Convert.ToInt32(ddlFleet_AddVessel.SelectedValue)
                    , Convert.ToInt32(ddlVesselManager.SelectedValue), UDFLib.ConvertDateToNull(dtTakeoverDate.Text),
                    UDFLib.ConvertDateToNull(dtHandoverDate.Text), UDFLib.ConvertIntegerToNull(ddlVesselFlage_AddVessel.SelectedValue), Convert.ToInt32(Session["UserID"].ToString()),
                    UDFLib.ConvertDecimalToNull(txtMinimumCTM.Text), chkSyncEnable.Checked == true ? "-1" : "0", FileName, (VslImg_FileName == "" ? null : VslImg_FileName), UDFLib.ConvertIntegerToNull(ddlvessel_AddType.SelectedValue), Convert.ToBoolean(chkIsVessel.Checked), txtImoNo.Text, txtCallSign.Text);                
           }
           if (Retval > 0)
           {
               BindVesselGrid();
               string hidemodal = String.Format("hideModal('dvAddNewVessel')");
               ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hidemodal", hidemodal, true);
           }
           else
           {
                if (Retval == -1)
                    lblMessage1.Text = "Vessel name already exist.";
                if (Retval == -2)
                    lblMessage1.Text = "Vessel short name already exist.";
                if (Retval == -3)
                    lblMessage1.Text = "Vessel short name already exist for deleted vessel.(Vessel short name should be unique)";
                string AddNewVessel = String.Format("showModal('dvAddNewVessel',false);");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddNewVessel", AddNewVessel, true);
           }
        #endregion
    }    
      
 


    protected void btnSaveAndClose_Click(object sender, EventArgs e)
    {
        btnsave_Click(null, null);
        string js = "closeDiv('dvAddNewVessel');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "close", js, true);

    }

    protected void ddlFilterVesselManager_SelectedIndexChanged(object sender, EventArgs e)
    {
        Load_FleetList();
        Load_VesselList();

    }

    public void Load_FleetList()
    {

        DataTable dtfleet = objBLL.GetFleetList_ByID(null, UDFLib.ConvertIntegerToNull(ddlFilterVesselManager.SelectedValue));

        DDLFleet.DataSource = dtfleet;
        DDLFleet.DataTextField = "FleetName";
        DDLFleet.DataValueField = "FleetCode";
        DDLFleet.DataBind();
        DDLFleet.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));
    }

    public void Load_FleetList_AddVessel()
    {

        DataTable dtfleet = objBLL.GetFleetList_ByID(null, UDFLib.ConvertIntegerToNull(ddlVesselManager.SelectedValue));

        ddlFleet_AddVessel.DataSource = dtfleet;
        ddlFleet_AddVessel.DataTextField = "FleetName";
        ddlFleet_AddVessel.DataValueField = "FleetCode";
        ddlFleet_AddVessel.DataBind();
        ddlFleet_AddVessel.Items.Insert(0, new ListItem("-Select-", "0"));
    }

    public void Load_VesselList()
    {
        int Fleet_ID = int.Parse(DDLFleet.SelectedValue);
        int UserCompanyID = int.Parse(ddlFilterVesselManager.SelectedValue);
        int Vessel_Manager = int.Parse(ddlFilterVesselManager.SelectedValue);

        DDLVessel.DataSource = objBLL.Get_VesselList(Fleet_ID, 0, Vessel_Manager, "", UserCompanyID);

        DDLVessel.DataTextField = "VESSEL_NAME";
        DDLVessel.DataValueField = "VESSEL_ID";
        DDLVessel.DataBind();
        DDLVessel.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));
        DDLVessel.SelectedIndex = 0;
    }

    protected void ddlVesselManager_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlFilterVesselManager.SelectedIndex != -1)
        {
            Load_FleetList_AddVessel();
            string AddNewVessel = String.Format("showModal('dvAddNewVessel',false);");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddNewVessel", AddNewVessel, true);
        }

    }

    protected void ClearFields()
    {
        txtVessel.Text = "";
        txtVesselShortName.Text = "";
        txtEmailID.Text = "";
        ddlvessel_AddType.SelectedValue = null;
        ddlFleet_AddVessel.SelectedValue = "0";
        dtHandoverDate.Text = "";
        dtTakeoverDate.Text = "";
        ddlVesselFlage_AddVessel.SelectedValue = "0";
        chkSyncEnable.Checked = false;
        txtMinimumCTM.Text = "";
        txtCallSign.Text = "";
        txtImoNo.Text = "";
        lnkAttachment.Visible = false;
        ImgTempAttDelete.Visible = false;
        lblMessage1.Text = "";
        lnkVslImage.Visible = false;
        ImgVslImgDelete.Visible = false;    

    }

    protected void onUpdate(object source, CommandEventArgs e)
    {
        lblMessage1.Text = "";
        HiddenFlag.Value = "Edit";
        OperationMode = "Edit Vessel";
        DataTable dt = new DataTable();
        dt = objBLL.GetVesselDetails_ByID(Convert.ToInt32(e.CommandArgument.ToString()));

        ddlVesselManager.SelectedValue = dt.Rows[0]["Vessel_Manager"].ToString() != "" ? dt.Rows[0]["Vessel_Manager"].ToString() : "0";
        Load_FleetList_AddVessel();
        Load_VesselList();
        ddlFleet_AddVessel.SelectedValue = dt.Rows[0]["FleetCode"].ToString() != "" ? dt.Rows[0]["FleetCode"].ToString() : "0";
        ddlvessel_AddType.SelectedValue = dt.Rows[0]["Vessel_type"].ToString() != "" ? dt.Rows[0]["Vessel_type"].ToString() : "0";
        txtVesselID.Text = dt.Rows[0]["Vessel_ID"].ToString();
        txtVessel.Text = dt.Rows[0]["Vessel_Name"].ToString();
        txtVesselShortName.Text = dt.Rows[0]["Vessel_Short_Name"].ToString();
        txtEmailID.Text = dt.Rows[0]["Vessel_email"].ToString();
        dtTakeoverDate.Text = dt.Rows[0]["Takeover_Date"].ToString();
        dtHandoverDate.Text = dt.Rows[0]["Handover_Date"].ToString();
        txtMinimumCTM.Text = dt.Rows[0]["Min_CTM"].ToString();
        txtCallSign.Text = dt.Rows[0]["Vessel_Call_sign"].ToString();
        txtImoNo.Text = dt.Rows[0]["Vessel_IMO_No"].ToString();
        ddlVesselFlage_AddVessel.SelectedValue = dt.Rows[0]["L_Vessel_Flag"].ToString() != "" ? dt.Rows[0]["L_Vessel_Flag"].ToString() : "0";
        
        if (dt.Rows[0]["ODM_ENABLED"].ToString() == "-1")
            chkSyncEnable.Checked = true;
        else
            chkSyncEnable.Checked = false;

        lnkAttachment.Text = dt.Rows[0]["ME_Power_Curve"].ToString();

        //lnkAttachment.NavigateUrl = @"\" + System.Configuration.ConfigurationManager.AppSettings["APP_NAME"].ToString() + @"\Uploads\MEPowerCurve\" + lnkAttachment.Text;
        lnkAttachment.NavigateUrl = @"~\Uploads\MEPowerCurve\" + lnkAttachment.Text; //Added By JIT_1776_Pranali_17072015
       
        lnkVslImage.Text = dt.Rows[0]["Vessel_Image"].ToString();
        lnkVslImage.NavigateUrl = @"~\Uploads\VesselImage\" + lnkVslImage.Text;//Added By JIT_1776_Pranali_17072015
         
        //lnkVslImage.NavigateUrl = @"\" + System.Configuration.ConfigurationManager.AppSettings["APP_NAME"].ToString() + @"\Uploads\VesselImage\" + lnkVslImage.Text;
        if (lnkAttachment.Text != "")
        {
            lnkAttachment.Visible = true;
            ImgTempAttDelete.Visible = true;
        }
        else
        {
            lnkAttachment.Visible = false;
            ImgTempAttDelete.Visible = false;
        }

        if (lnkVslImage.Text != "")
        {
            lnkVslImage.Visible = true;
            ImgVslImgDelete.Visible = true;
        }
        else
        {
            lnkVslImage.Visible = false;
            ImgVslImgDelete.Visible = false;
        }
        chkIsVessel.Checked = Convert.ToBoolean(dt.Rows[0]["ISVESSEL"]);

        string InfoDiv = "Get_Record_Information_Details('LIB_VESSELS','Vessel_ID=" + txtVesselID.Text + "')";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "InfoDiv", InfoDiv, true);

        string AddNewVessel = String.Format("showModal('dvAddNewVessel',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddNewVessel", AddNewVessel, true);


    }

    protected void onDelete(object source, CommandEventArgs e)
    {
        objBLL.DeleteVessel(Convert.ToInt32(e.CommandArgument.ToString()), Convert.ToInt32(Session["USERID"].ToString()));
        BindVesselGrid();

    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {

        txtfilter.Text = "";

        DDLFleet.SelectedValue = "0";

        //DDLVessel.SelectedValue = "0";
        DDLVessel.SelectedValue = null;
        //DDLVesselFlag.SelectedValue = "0";
        DDLVesselFlag.SelectedValue = null;
        //ddlFilterVesselManager.SelectedValue = "1";
        ddlFilterVesselManager.SelectedValue = null;
        ddlvesselType.SelectedValue = null;
        // Load_FleetList();
        BindVesselGrid();
    }

    protected void ImgAddVessel_Click(object sender, EventArgs e)
    {
        lblMessage.Text = "";
        this.SetFocus("ctl00_MainContent_txtVessel");
        HiddenFlag.Value = "Add";

        OperationMode = "Add New Vessel";
        ClearFields();

        string AddNewVessel = String.Format("showModal('dvAddNewVessel',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddNewVessel", AddNewVessel, true);

    }

    protected void ImgExpExcel_Click(object sender, EventArgs e)
    {


        int rowcount = ucCustomPagerItems.isCountRecord;
        int UserCompanyID = UDFLib.ConvertToInteger(Session["USERCOMPANYID"].ToString());

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

        DataTable dt = objBLL.SearchVessel(txtfilter.Text != "" ? txtfilter.Text : null, UDFLib.ConvertIntegerToNull(DDLFleet.SelectedValue), UDFLib.ConvertIntegerToNull(DDLVessel.SelectedValue)
         , UDFLib.ConvertIntegerToNull(ddlFilterVesselManager.SelectedValue), UDFLib.ConvertIntegerToNull(DDLVesselFlag.SelectedValue), UDFLib.ConvertIntegerToNull(ddlFilterVesselManager.SelectedValue), null, UDFLib.ConvertIntegerToNull(ddlvesselType.SelectedValue)
            , sortbycoloumn, sortdirection, null, null, ref  rowcount);

        string[] HeaderCaptions = { "Name", "Short Name", "E-Mail", "Fleet", "Vessel Manager", "Vessel Flag", "Vessel Type","Min CTM","IMO No.","Call Sign", "Sync Flag" };
        string[] DataColumnsName = { "Vessel_Name", "Vessel_Short_Name", "Vessel_EMail", "FleetName", "VesselManager", "VesselTypes", "Flag_Name", "Min_CTM", "VESSEL_IMO_NO1", "VESSEL_CALL_SIGN1", "ODM_ENABLED" };

        GridViewExportUtil.ShowExcel(dt, HeaderCaptions, DataColumnsName, "Vessel", "Vessel", "");
    }

    protected void VesselGridView_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.Header)
        {
            if (ViewState["SORTBYCOLOUMN"] != null)
            {
                HtmlImage img = (HtmlImage)e.Row.FindControl(ViewState["SORTBYCOLOUMN"].ToString());
                if (img != null)
                {
                    if (ViewState["SORTDIRECTION"] == null || ViewState["SORTDIRECTION"].ToString() == "0")
                        img.Src = "~/purchase/Image/arrowUp.png";
                    else
                        img.Src = "~/purchase/Image/arrowDown.png";

                    img.Visible = true;
                }
            }
        }


        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes["onmouseover"] = "this.style.cursor='hand';this.style.backgroundColor='#FEECEC';";
            e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';this.style.color='black';this.style.backgroundColor=''";
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ImageButton ImgMEPCMAtt = (ImageButton)e.Row.FindControl("ImgMEPCMAtt");
            if (DataBinder.Eval(e.Row.DataItem, "ME_Power_Curve_Att").ToString() == "1")
            {
                ImgMEPCMAtt.Attributes.Add("onclick", "DocOpen('" + DataBinder.Eval(e.Row.DataItem, "ME_Power_Curve").ToString() + "'); return false;");
            }
        }

    }

    protected void VesselGridView_Sorting(object sender, GridViewSortEventArgs se)
    {

        ViewState["SORTBYCOLOUMN"] = se.SortExpression;

        if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
            ViewState["SORTDIRECTION"] = 1;
        else
            ViewState["SORTDIRECTION"] = 0;

        BindVesselGrid();

    }

    protected void ImgTempAttDelete_click(object sender, EventArgs e)
    {

        if (HiddenFlag.Value == "Edit")
        {
            if (lnkAttachment.Text != "")
            {
                if (File.Exists(Server.MapPath("~/Uploads/MEPowerCurve/" + lnkAttachment.Text)))
                    File.Delete(Server.MapPath("~/Uploads/MEPowerCurve/" + lnkAttachment.Text));
            }

            int Retval = objBLL.Delete_MEPowerCurveAttachment(Convert.ToInt32(txtVesselID.Text), Convert.ToInt32(Session["UserID"].ToString()));
        }

        lnkAttachment.Text = "";

        BindVesselGrid();
        string AddNewVessel = String.Format("showModal('dvAddNewVessel',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddNewVessel", AddNewVessel, true);
    }
    protected void ImgVslImgDelete_click(object sender, EventArgs e)
    {

        if (HiddenFlag.Value == "Edit")
        {
            if (lnkVslImage.Text != "")
            {
                if (File.Exists(Server.MapPath("~/Uploads/VesselImage/" + lnkVslImage.Text)))
                    File.Delete(Server.MapPath("~/Uploads/VesselImage/" + lnkVslImage.Text));
            }
            int Retval = objBLL.Delete_VesselImageAttachment(Convert.ToInt32(txtVesselID.Text), Convert.ToInt32(Session["UserID"].ToString()));
        }

        lnkVslImage.Text = "";
        ImgVslImgDelete.Visible = false;
        BindVesselGrid();
        string AddNewVessel = String.Format("showModal('dvAddNewVessel',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddNewVessel", AddNewVessel, true);
    }
    
}