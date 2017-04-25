using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.JRA;
using SMS.Properties;
using System.Data;
using SMS.Business.Infrastructure;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class JRA_RiskAssessmentDetails : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!this.IsPostBack)
        {
            if (Request.QueryString["Assessment_ID"] != null)
            {
                hfAssessment_ID.Value = Request.QueryString["Assessment_ID"].ToString();
                hfVessel_ID.Value = Request.QueryString["Vessel_ID"].ToString();

                ViewState["SORTDIRECTION"] = null;
                ViewState["SORTBYCOLOUMN"] = null;

                ucCustomPagerItems.PageSize = 20;
                BindCombos();

                BindAssessmentDetails();
                //  UserAccessValidation();
            }
        }

    }
    protected void BindCombos()
    {


        DataTable dtAllChilds = BLL_JRA_Hazards.GET_WORK_CATEGORY_LIST(null, 3, 2);
        ddlWorkCategory.DataSource = dtAllChilds;
        ddlWorkCategory.DataTextField = "Work_Category_Display";
        ddlWorkCategory.DataValueField = "Work_Categ_ID";
        ddlWorkCategory.DataBind();
        ddlWorkCategory.Items.Insert(0, new ListItem("-Select All-", "0"));

        DataSet dsSev = BLL_JRA_Hazards.GET_TYPE("Severity");
        ddlSeverity.DataSource = dsSev.Tables[0];
        ddlSeverity.DataTextField = "Type_Display_Text";
        ddlSeverity.DataValueField = "Type_ID";
        ddlSeverity.DataBind();
        ddlSeverity.Items.Insert(0, new ListItem("-Select All-", "0"));

        DataSet dsLkhd = BLL_JRA_Hazards.GET_TYPE("Likelihood");
        ddlLikelihood.DataSource = dsLkhd.Tables[0];
        ddlLikelihood.DataTextField = "Type_Display_Text";
        ddlLikelihood.DataValueField = "Type_ID";
        ddlLikelihood.DataBind();
        ddlLikelihood.Items.Insert(0, new ListItem("-Select All-", "0"));

        DataSet dsModRis = BLL_JRA_Hazards.JRA_GET_MODIFIED_RISKS();
        ddlModifiedRisk.DataSource = dsModRis.Tables[0];
        ddlModifiedRisk.DataTextField = "Type_Display_Text";
        ddlModifiedRisk.DataValueField = "Type_ID";
        ddlModifiedRisk.DataBind();
        ddlModifiedRisk.Items.Insert(0, new ListItem("-Select All-", "0"));

    }

    UserAccess objUA = new UserAccess();

    public string OperationMode = "";

    public Boolean uaEditFlag = false;
    public Boolean uaDeleteFlage = false;



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
            ImgAdd.Visible = false;
            btnsave.Visible = false;

        }
        else
        {
            if (hfAssessment_Status.Value == "Approval Pending")
            {
                ImgAdd.Enabled = true;
                btnsave.Visible = true;
            }
            else
            {
                ImgAdd.Enabled = false;
                btnsave.Visible = false;
            }
        }

        if (objUA.Edit == 1)
        {
            uaEditFlag = true;
            btnsave.Visible = true;
            if (hfAssessment_Status.Value == "Approval Pending")
            {
                uaEditFlag = true;
                btnsave.Visible = true;
            }
            else
            {
                uaEditFlag = false;
                btnsave.Visible = false;
            }
        }
        else
        {
            uaEditFlag = false;
            btnsave.Visible = false;
        }



        if (objUA.Delete == 1)
            uaDeleteFlage = true;




    }

    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }

    public void BindAssessmentDetails()
    {
        try
        {       
        int rowcount = ucCustomPagerItems.isCountRecord;

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        string sortdirection = null;
        if (ViewState["SORTDIRECTION"] != null)
            sortdirection = ViewState["SORTDIRECTION"].ToString();


        DataSet ds = BLL_JRA_Hazards.GET_ASSESSMENT(UDFLib.ConvertToInteger(hfAssessment_ID.Value), UDFLib.ConvertToInteger(hfVessel_ID.Value), GetSessionUserID());

        txtVesselName.Text = ds.Tables[0].Rows[0]["Vessel_Name"].ToString();
        txtCurrentAssessmentDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["Current_Assessment_Date"].ToString()).ToString("dd/MMM/yyyy");
        txtLastAssessmentDate.Text = ds.Tables[0].Rows[0]["Last_Assessment_Date"].ToString().Trim() != "" ? Convert.ToDateTime(ds.Tables[0].Rows[0]["Last_Assessment_Date"].ToString()).ToString("dd/MMM/yyyy") : "";
        txtJobASsessed.Text = ds.Tables[0].Rows[0]["Work_Categ_Value"].ToString() + " " + ds.Tables[0].Rows[0]["Work_Category_Name"].ToString();
        // txtAssessmentNo.Text = ds.Tables[0].Rows[0]["Work_Categ_Value"].ToString();
        hfTblMode.Value = ds.Tables[0].Rows[0]["Mode"].ToString();
        hfWork_Categ_ID.Value = ds.Tables[0].Rows[0]["Work_Categ_ID"].ToString();
        hfAssessment_Status.Value = ds.Tables[0].Rows[0]["Assessment_Status"].ToString();
        lblApprovalStatus.Text = ds.Tables[0].Rows[0]["Assessment_Status"].ToString();
        if (ds.Tables[0].Rows[0]["ValidUser"].ToString() == "True")
        {
            imgBtnApproveReject.Visible = true;
            #region access Validation
            //---access Validation

            int CurrentUserID = GetSessionUserID();
            string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

            BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
            objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

            if (objUA.View == 0)
                Response.Redirect("~/default.aspx?msgid=1");



            if (objUA.Add == 0)
            {
                ImgAdd.Visible = false;
                btnsave.Visible = false;

            }
            else
            {
                if (hfAssessment_Status.Value == "Approval Pending")
                {
                    ImgAdd.Visible = true;
                    btnsave.Visible = true;
                }
                else
                {
                    ImgAdd.Visible = false;
                    btnsave.Visible = false;
                }
            }

            if (objUA.Edit == 1)
            {
                uaEditFlag = true;
                btnsave.Visible = true;
                if (hfAssessment_Status.Value == "Approval Pending")
                {
                    uaEditFlag = true;
                    btnsave.Visible = true;
                }
                else
                {
                    uaEditFlag = false;
                    btnsave.Visible = false;
                }
            }
            else
            {
                uaEditFlag = false;
                btnsave.Visible = false;
            }



            if (objUA.Delete == 1)
                uaDeleteFlage = true;

            //-------------
            #endregion
        }
        else
        {
            uaDeleteFlage = false;
            uaEditFlag = false;
            imgBtnApproveReject.Visible = false;
        }
        ViewState["Work_Categ_ID"] = ds.Tables[0].Rows[0]["Work_Categ_ID"].ToString();









        LoadFollowups();






        if (ds.Tables[1].Rows.Count > 0)
        {
            gvAssessmentDetails.DataSource = ds.Tables[1];
            gvAssessmentDetails.DataBind();
        }

        if (ds.Tables[2].Rows.Count > 0)
        {
            grdActionLog.DataSource = ds.Tables[2];
            grdActionLog.DataBind();
        }
        }
        catch (Exception)
        {
            
        }
    }

    protected void LoadFollowups()
    {
        txtFWRemark.Text = "";
        DataSet dsFW = BLL_JRA_Hazards.GET_REMARKS(UDFLib.ConvertToInteger(hfAssessment_ID.Value), UDFLib.ConvertToInteger(hfVessel_ID.Value));

        if (dsFW.Tables[0].Rows.Count > 0)
        {
            grdFollowUps.DataSource = dsFW.Tables[0];
            grdFollowUps.DataBind();
        }
    }

    protected void btnsave_Click(object sender, EventArgs e)
    {
        string ModifiedRiskColor = null;
        string ModifiedRiskValue = null;
        string ModifiedRisk = null;
        if (ddlModifiedRisk.SelectedIndex > 0)
        {
            DataSet dsModRis = BLL_JRA_Hazards.JRA_GET_MODIFIED_RISKS();
            ModifiedRiskColor = dsModRis.Tables[0].Select("Type_ID=" + ddlModifiedRisk.SelectedValue)[0]["Type_Color"].ToString();
            ModifiedRisk = ddlModifiedRisk.SelectedItem.Text;
            ModifiedRiskValue = ddlModifiedRisk.SelectedValue;
        }
        if (ddlSeverity.SelectedIndex > 0 && ddlLikelihood.SelectedIndex > 0)
        {
            DataSet dsSev = BLL_JRA_Hazards.GET_TYPE("Severity");
            DataSet dsLkhd = BLL_JRA_Hazards.GET_TYPE("Likelihood");
            string s = dsSev.Tables[0].Select("Type_ID=" + UDFLib.ConvertToInteger(ddlSeverity.SelectedValue))[0]["Type_Value"].ToString();
            string l = dsLkhd.Tables[0].Select("Type_ID=" + UDFLib.ConvertToInteger(ddlLikelihood.SelectedValue))[0]["Type_Value"].ToString();

            int Rating = UDFLib.ConvertToInteger(s) * UDFLib.ConvertToInteger(l);
            DataSet ds = BLL_JRA_Hazards.GET_RISK_RATINGS(Rating);
            txtInitiakRisk.Text = ds.Tables[0].Rows[0]["Type_Display_Text"].ToString();
            txtInitiakRiskValue.Text = ds.Tables[0].Rows[0]["Type_ID"].ToString();
            txtInitialRiskColor.Text = ds.Tables[0].Rows[0]["Type_Color"].ToString();
        }
        if (HiddenFlag.Value == "Add")
        {

            //BLL_JRA_Hazards.INSUPD_ASSESSMENT(UDFLib.ConvertToInteger(hfAssessment_ID.Value),
            //    null, UDFLib.ConvertToInteger(hfVessel_ID.Value),
            //    GetSessionUserID(), null, txtHazardDesc.Text,txtControlMeasure.Text,
            //    ddlSeverity.SelectedValue, ddlLikelihood.SelectedValue, ddlSeverity.SelectedItem.Text,
            //    ddlLikelihood.SelectedItem.Text, txtInitiakRisk.Text, txtInitiakRiskValue.Text, txtInitialRiskColor.Text,
            //    txtAdditionalCntrolMeasure.Text, ModifiedRisk, ModifiedRiskValue, ModifiedRiskColor, 1);
            BLL_JRA_Hazards.INSUPD_ASSESSMENT(UDFLib.ConvertToInteger(hfAssessment_ID.Value),
              null, UDFLib.ConvertToInteger(hfVessel_ID.Value),
               GetSessionUserID(), null, txtHazardDesc.Text, txtControlMeasure.Text,
               ddlSeverity.SelectedValue, ddlLikelihood.SelectedValue, ddlSeverity.SelectedItem.Text,
               ddlLikelihood.SelectedItem.Text, txtInitiakRisk.Text, txtInitiakRiskValue.Text, txtInitialRiskColor.Text,
               txtAdditionalCntrolMeasure.Text, ModifiedRisk, ModifiedRiskValue, ModifiedRiskColor, 1);
        }
        else
        {
            BLL_JRA_Hazards.INSUPD_ASSESSMENT(UDFLib.ConvertToInteger(hfAssessment_ID.Value),
                UDFLib.ConvertToInteger(ViewState["Assessment_Dtl_ID"]), UDFLib.ConvertToInteger(hfVessel_ID.Value),
                GetSessionUserID(), UDFLib.ConvertToInteger(ViewState["Hazard_ID"]), txtHazardDesc.Text, txtControlMeasure.Text,
                ddlSeverity.SelectedValue, ddlLikelihood.SelectedValue, ddlSeverity.SelectedItem.Text,
                ddlLikelihood.SelectedItem.Text, txtInitiakRisk.Text, txtInitiakRiskValue.Text, txtInitialRiskColor.Text,
                txtAdditionalCntrolMeasure.Text, ModifiedRisk, ModifiedRiskValue, ModifiedRiskColor, UDFLib.ConvertToInteger(ViewState["Office_ID"]));

        }

        BindAssessmentDetails();
        string hidemodal = String.Format("hideModal('divadd')");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hidemodal", hidemodal, true);
    }

    protected void ImgAdd_Click(object sender, EventArgs e)
    {
        //this.SetFocus("ctl00_MainContent_txtPortName");

        ddlWorkCategory.SelectedValue = ViewState["Work_Categ_ID"].ToString();
        if (hfTblMode.Value != "Edit")
        {
            BLL_JRA_Hazards.UPD_ASSESSMENT_STATUS(Convert.ToInt32(hfAssessment_ID.Value), Convert.ToInt32(hfWork_Categ_ID.Value), Convert.ToInt32(hfVessel_ID.Value), "Edit", "", GetSessionUserID());
            hfTblMode.Value = "Edit";
        }

        HiddenFlag.Value = "Add";

        OperationMode = "Add Hazard";

        ClearFields();

        string AddPort = String.Format("showModal('divadd',true);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddPort", AddPort, true);


    }

    protected void ImgExpExcel_Click(object sender, EventArgs e)
    {

        int rowcount = ucCustomPagerItems.isCountRecord;

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        string sortdirection = null;
        if (ViewState["SORTDIRECTION"] != null)
            sortdirection = ViewState["SORTDIRECTION"].ToString();



        //DataTable dt = BLL_JRA_Hazards.GET_HAZARD_TEMPLATE_LIST(
        //    null,
        //    ddlChildWorkCateg.SelectedIndex <= 0 ? null : ddlChildWorkCateg.SelectedValue,
        //    "",
        //    ucCustomPagerItems.CurrentPageIndex, null, sortbycoloumn,
        //    sortdirection, ref rowcount).Tables[0];

        //string Header = (ddlChildWorkCateg.SelectedIndex > 0 ? ddlChildWorkCateg.SelectedItem.Text : "") + " Hazard Template";

        //string[] HeaderCaptions = { "Hazard Description", "Control Measure", "Severity", "Likelihood", "Initial Risk", "Additional Control Measures", "Modified Risk" };
        //string[] DataColumnsName = { "Hazard_Description", "Control_Measure", "Severity", "Likelihood", "Initial_Risk", "Additional_Control_Measures", "Modified_Risk" };

        //GridViewExportUtil.ShowExcel(dt, HeaderCaptions, DataColumnsName, "HazardTemplate", Header, "");
    }

    public void ClearFields()
    {

        txtAdditionalCntrolMeasure.Text = "";
        txtControlMeasure.Text = "";
        txtHazardDesc.Text = "";
        txtHazardID.Text = "";
        txtInitiakRisk.Text = "";
        txtInitiakRiskValue.Text = "";
        ddlLikelihood.SelectedIndex = 0;
        ddlModifiedRisk.SelectedIndex = 0;
        ddlSeverity.SelectedIndex = 0;
        //ddlWorkCategory.SelectedIndex = 0;



    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {

        BindAssessmentDetails();

    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {

        ClearFilter();
        BindAssessmentDetails();
    }

    protected void ClearFilter()
    {

    }

    protected void onDelete(object source, CommandEventArgs e)
    {

        int AssessmentDtlID = UDFLib.ConvertToInteger(e.CommandArgument.ToString().Split(';')[0]);
        int OfficeID = UDFLib.ConvertToInteger(e.CommandArgument.ToString().Split(';')[1]);
        BLL_JRA_Hazards.DEL_ASSESSMENT_DETAILS(AssessmentDtlID, UDFLib.ConvertToInteger(hfVessel_ID.Value), OfficeID, GetSessionUserID());
        BindAssessmentDetails();
    }

    protected void onUpdate(object source, CommandEventArgs e)
    {
        ddlWorkCategory.SelectedValue = ViewState["Work_Categ_ID"].ToString();
        if (hfTblMode.Value != "Edit")
        {
            BLL_JRA_Hazards.UPD_ASSESSMENT_STATUS(Convert.ToInt32(hfAssessment_ID.Value), Convert.ToInt32(hfWork_Categ_ID.Value), Convert.ToInt32(hfVessel_ID.Value), "Edit", "", GetSessionUserID());
            hfTblMode.Value = "Edit";
        }


        HiddenFlag.Value = "Edit";
        OperationMode = "Edit Hazard Template";

        int cnt = 0;

        int AssessmentDtlID = UDFLib.ConvertToInteger(e.CommandArgument.ToString().Split(';')[0]);
        int OfficeID = UDFLib.ConvertToInteger(e.CommandArgument.ToString().Split(';')[1]);
        ViewState["Assessment_Dtl_ID"] = AssessmentDtlID;
        ViewState["Office_ID"] = OfficeID;


        DataSet dt = BLL_JRA_Hazards.GET_ASSESSMENT(UDFLib.ConvertToInteger(hfAssessment_ID.Value), UDFLib.ConvertToInteger(hfVessel_ID.Value), GetSessionUserID(), AssessmentDtlID, OfficeID);

        ViewState["Hazard_ID"] = txtHazardID.Text = dt.Tables[0].Rows[0]["Hazard_ID"].ToString();
        if (dt.Tables[0].Rows[0]["Hazard_ID"].ToString() == "")
        {
            txtHazardDesc.ReadOnly = false;
        }
        else
        {
            txtHazardDesc.ReadOnly = true;
        }
        txtHazardDesc.Text = dt.Tables[0].Rows[0]["Hazard_Description"].ToString();
        txtControlMeasure.Text = dt.Tables[0].Rows[0]["Control_Measure"].ToString();
        txtInitiakRisk.Text = dt.Tables[0].Rows[0]["Initial_Risk"].ToString();
        txtInitiakRiskValue.Text = dt.Tables[0].Rows[0]["Initial_Risk_Value"].ToString();
        txtAdditionalCntrolMeasure.Text = dt.Tables[0].Rows[0]["Additional_Control_Measures"].ToString();

        txtInitialRiskColor.Text = dt.Tables[0].Rows[0]["Initial_Risk_Color"].ToString();


        try
        {
            ddlSeverity.SelectedValue = dt.Tables[0].Rows[0]["Severity_ID"].ToString();
        }
        catch (Exception)
        {

            ddlSeverity.SelectedIndex = 0;
        }
        try
        {
            ddlLikelihood.SelectedValue = dt.Tables[0].Rows[0]["Likelihood_ID"].ToString();
        }
        catch (Exception)
        {

            ddlLikelihood.SelectedIndex = 0;
        }
        try
        {
            ddlWorkCategory.SelectedValue = dt.Tables[0].Rows[0]["Work_Categ_ID"].ToString();
        }
        catch (Exception)
        {

            ddlWorkCategory.SelectedIndex = 0;
        }



        try
        {
            ddlModifiedRisk.SelectedValue = dt.Tables[0].Rows[0]["Modified_Risk_Value"].ToString();
        }
        catch (Exception)
        {

            ddlModifiedRisk.SelectedIndex = 0;
        }







        string AddHazardTemplate = String.Format("showModal('divadd',true);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddHazardTemplate", AddHazardTemplate, true);

    }

    protected void gvAssessmentDetails_RowDataBound(object sender, GridViewRowEventArgs e)
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
            DataRowView rowView = (DataRowView)e.Row.DataItem;

            e.Row.Cells[4].BackColor = System.Drawing.Color.FromName(rowView["Initial_Risk_Color"].ToString());
            e.Row.Cells[4].ForeColor = System.Drawing.Color.Blue;
            e.Row.Cells[6].BackColor = System.Drawing.Color.FromName(rowView["Modified_Risk_Color"].ToString());
            e.Row.Cells[6].ForeColor = System.Drawing.Color.Blue;
        }

    }

    protected void gvAssessmentDetails_Sorting(object sender, GridViewSortEventArgs se)
    {
        ViewState["SORTBYCOLOUMN"] = se.SortExpression;

        if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
            ViewState["SORTDIRECTION"] = 1;
        else
            ViewState["SORTDIRECTION"] = 0;

        BindAssessmentDetails();
    }
    protected void ddlSeverity_SelectedIndexChanged(object sender, EventArgs e)
    {
        CalulateRisk();
    }
    protected void ddlLikelihood_SelectedIndexChanged(object sender, EventArgs e)
    {
        CalulateRisk();
    }

    protected void CalulateRisk()
    {
        if (ddlSeverity.SelectedIndex > 0 && ddlLikelihood.SelectedIndex > 0)
        {
            DataSet dsSev = BLL_JRA_Hazards.GET_TYPE("Severity");
            DataSet dsLkhd = BLL_JRA_Hazards.GET_TYPE("Likelihood");
            string s = dsSev.Tables[0].Select("Type_ID=" + UDFLib.ConvertToInteger(ddlSeverity.SelectedValue))[0]["Type_Value"].ToString();
            string l = dsLkhd.Tables[0].Select("Type_ID=" + UDFLib.ConvertToInteger(ddlLikelihood.SelectedValue))[0]["Type_Value"].ToString();

            int Rating = UDFLib.ConvertToInteger(s) * UDFLib.ConvertToInteger(l);
            DataSet ds = BLL_JRA_Hazards.GET_RISK_RATINGS(Rating);
            txtInitiakRisk.Text = ds.Tables[0].Rows[0]["Type_Display_Text"].ToString();
            txtInitiakRiskValue.Text = ds.Tables[0].Rows[0]["Type_ID"].ToString();
            txtInitialRiskColor.Text = ds.Tables[0].Rows[0]["Type_Color"].ToString();
        }
        else
        {
            txtInitiakRiskValue.Text = "";
            txtInitiakRisk.Text = "";
            txtInitialRiskColor.Text = "";
        }
        string AddPort = String.Format("showModal('divadd',true);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddPort", AddPort, true);

    }
    protected void btnSaveFollowup_Click(object sender, EventArgs e)
    {
        BLL_JRA_Hazards.Insert_REMARKS(UDFLib.ConvertToInteger(hfAssessment_ID.Value), UDFLib.ConvertToInteger(hfVessel_ID.Value), GetSessionUserID(), txtFWRemark.Text);

        string divFollow = String.Format("hideModal('divFollow',true);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "divFollow", divFollow, true);

        LoadFollowups();
    }


    protected void bntApprove_Click(object sender, EventArgs e)
    {


        BLL_JRA_Hazards.UPD_ASSESSMENT_STATUS(UDFLib.ConvertToInteger(hfAssessment_ID.Value), UDFLib.ConvertToInteger(hfWork_Categ_ID.Value), UDFLib.ConvertToInteger(hfVessel_ID.Value), "Approved", txtRemark.Text, GetSessionUserID());
        txtRemark.Text = "";
        string AddHazardTemplate = String.Format("hideModal('divApv',true);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddHazardTemplate", AddHazardTemplate, true);
        imgBtnApproveReject.Visible = false;
        BindAssessmentDetails();
    }
    protected void btnRework_Click(object sender, EventArgs e)
    {
        BLL_JRA_Hazards.UPD_ASSESSMENT_STATUS(UDFLib.ConvertToInteger(hfAssessment_ID.Value), UDFLib.ConvertToInteger(hfWork_Categ_ID.Value), UDFLib.ConvertToInteger(hfVessel_ID.Value), "Rework", txtRemark.Text, GetSessionUserID());
        txtRemark.Text = "";
        string AddHazardTemplate = String.Format("hideModal('divApv',true);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddHazardTemplate", AddHazardTemplate, true);
        imgBtnApproveReject.Visible = false;
        BindAssessmentDetails();
    }

    protected void imgBtnApproveReject_Click(object sender, EventArgs e)
    {
        string divApv = String.Format("showModal('divApv',true);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "divApv", divApv, true);
        BindAssessmentDetails();
    }
}