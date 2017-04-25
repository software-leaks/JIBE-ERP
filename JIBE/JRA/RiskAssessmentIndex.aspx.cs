 

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

public partial class JRA_RiskAssessmentIndex : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        UserAccessValidation();
        if (!IsPostBack)
        {
            ViewState["SORTDIRECTION"] = null;
            ViewState["SORTBYCOLOUMN"] = null;

            ucCustomPagerItems.PageSize = 20;
            BindCombos();
             
            BindAssessmentTemplateGrid();
        }
    }
    protected void BindCombos()
    {
        JRA_Lib lObjWC = new JRA_Lib();
        lObjWC.Work_Categ_Parent_ID = null;
        lObjWC.Mode = 0;
        DataTable dt = BLL_JRA_Work_Category.JRA_GET_WORK_CATEGORY_LIST(lObjWC);
        DataView dv = dt.DefaultView;

        DataTable dtCloned = dt.Clone();
        dtCloned.Columns["Work_Categ_Value"].DataType = typeof(float);
        foreach (DataRow row in dt.Rows)
        {
            dtCloned.ImportRow(row);
        }
        dv = dtCloned.DefaultView;


        dv.Sort = "Work_Categ_Value";
        dt = dv.ToTable();
        ddlParentWorkCateg.DataSource = dt;
        ddlParentWorkCateg.DataTextField = "Work_Category_Display";
        ddlParentWorkCateg.DataValueField = "Work_Categ_ID";
        ddlParentWorkCateg.DataBind();
        ddlParentWorkCateg.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));

        lObjWC = new JRA_Lib();
        lObjWC.Work_Categ_Parent_ID = null;
        lObjWC.Mode = 3;
        DataTable dtAllChilds = BLL_JRA_Work_Category.JRA_GET_WORK_CATEGORY_LIST(lObjWC);

        DataTable dtAllChildsCloned = dtAllChilds.Clone();
        dtAllChildsCloned.Columns["Work_Categ_Value"].DataType = typeof(float);
        foreach (DataRow row in dtAllChilds.Rows)
        {
            dtAllChildsCloned.ImportRow(row);
        }

        dv = dtAllChildsCloned.DefaultView;
        dv.Sort = "Work_Categ_Value";
        dtAllChilds = dv.ToTable();
        ddlChildWorkCateg.DataSource = dtAllChilds;
        ddlChildWorkCateg.DataTextField = "Work_Category_Display";
        ddlChildWorkCateg.DataValueField = "Work_Categ_ID";
        ddlChildWorkCateg.DataBind();
        ddlChildWorkCateg.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));

        BLL_Infra_VesselLib objVsl = new BLL_Infra_VesselLib();
        DataTable dtVessel = objVsl.Get_VesselList(0, 0, Convert.ToInt32(Session["USERCOMPANYID"].ToString()), "", Convert.ToInt32(Session["USERCOMPANYID"].ToString()));
        DDLVessel.DataSource = dtVessel;
        DDLVessel.DataTextField = "Vessel_name";
        DDLVessel.DataValueField = "Vessel_id";
        DDLVessel.DataBind();
        DDLVessel.Items.Insert(0, new ListItem("--SELECT ALL--", null));
    }
    protected void ddlParentWorkCateg_SelectedIndexChanged(object sender, EventArgs e)
    {
        JRA_Lib lObjWC = new JRA_Lib();
        lObjWC.Work_Categ_Parent_ID = UDFLib.ConvertToInteger(ddlParentWorkCateg.SelectedValue);
        lObjWC.Mode = 1;
        DataTable dtChilds = BLL_JRA_Work_Category.JRA_GET_WORK_CATEGORY_LIST(lObjWC);



        DataView dv = dtChilds.DefaultView;

        DataTable dtChildsCloned = dtChilds.Clone();
        dtChildsCloned.Columns["Work_Categ_Value"].DataType = typeof(float);
        foreach (DataRow row in dtChilds.Rows)
        {
            dtChildsCloned.ImportRow(row);
        }
        dv = dtChildsCloned.DefaultView;





      
        dv.Sort = "Work_Categ_Value";
        dtChilds = dv.ToTable();
        ddlChildWorkCateg.DataSource = dtChilds;
        ddlChildWorkCateg.DataTextField = "Work_Category_Display";
        ddlChildWorkCateg.DataValueField = "Work_Categ_ID";
        ddlChildWorkCateg.DataBind();
        ddlChildWorkCateg.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));
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

        //if (objUA.Add == 0)
        //{
        //    ImgAdd.Visible = false;
        //}
        //if (objUA.Edit == 1)
        //    uaEditFlag = true;
        //else
        //    btnsave.Visible = false;
        //if (objUA.Delete == 1) uaDeleteFlage = true;

    }

    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }

    public void BindAssessmentTemplateGrid()
    {

        int rowcount = ucCustomPagerItems.isCountRecord;

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        string sortdirection = null; 
        if (ViewState["SORTDIRECTION"] != null)
            sortdirection =  ViewState["SORTDIRECTION"].ToString();


        DataTable dt = BLL_JRA_Hazards.GET_ASSESSMENT_SEARCH(DDLVessel.SelectedIndex<=0?null:DDLVessel.SelectedValue,
            null,
            ddlChildWorkCateg.SelectedIndex <= 0 ? null : ddlChildWorkCateg.SelectedValue,ddlAssessmentStatus.SelectedIndex<=0?null:ddlAssessmentStatus.SelectedValue.ToString(),
            UDFLib.ConvertDateToNull(txtFromDate.Text),UDFLib.ConvertDateToNull(txtToDate.Text),
            "",GetSessionUserID(),
            ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, sortbycoloumn, 
            sortdirection, ref rowcount).Tables[0];


        if (ucCustomPagerItems.isCountRecord == 1)
        {
            ucCustomPagerItems.CountTotalRec = rowcount.ToString();
            ucCustomPagerItems.BuildPager();
        }

        if (dt.Rows.Count > 0)
        {
            gvAssessment.DataSource = dt;
            gvAssessment.DataBind();
        }
        else
        {
            gvAssessment.DataSource = dt;
            gvAssessment.DataBind();
        }
       
    }

 

     

    protected void ImgExpExcel_Click(object sender, EventArgs e)
    {

        int rowcount = ucCustomPagerItems.isCountRecord;

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        string sortdirection = null;
        if (ViewState["SORTDIRECTION"] != null)
            sortdirection = ViewState["SORTDIRECTION"].ToString();



        DataTable dt = BLL_JRA_Hazards.GET_ASSESSMENT_SEARCH(DDLVessel.SelectedIndex <= 0 ? null : DDLVessel.SelectedValue,
            null,
            ddlChildWorkCateg.SelectedIndex <= 0 ? null : ddlChildWorkCateg.SelectedValue, ddlAssessmentStatus.SelectedIndex <= 0 ? null : ddlAssessmentStatus.SelectedValue.ToString(),
            UDFLib.ConvertDateToNull(txtFromDate.Text), UDFLib.ConvertDateToNull(txtToDate.Text),
            "",GetSessionUserID(),
            null, null, sortbycoloumn,
            sortdirection, ref rowcount).Tables[0];

        string Header = (ddlChildWorkCateg.SelectedIndex > 0 ? ddlChildWorkCateg.SelectedItem.Text : "") + " Risk Assessment";

        string[] HeaderCaptions = { "Vessel Name  ", "Assessment No.", "Job Assessed", "Current Assessment Date", "Last Assessment Date", "Assessment Status",  };
        string[] DataColumnsName = { "Vessel_Name", "Work_Categ_Value", "Work_Category_Name", "Current_Assessment_Date", "Last_Assessment_Date", "Assessment_Status" };

        GridViewExportUtil.ShowExcel(dt, HeaderCaptions, DataColumnsName, "JobRiskAssessmentIndex", Header, "");
    }

    

    protected void btnFilter_Click(object sender, EventArgs e)
    {

        BindAssessmentTemplateGrid();

    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
       
        ClearFilter();
        BindAssessmentTemplateGrid();
    }

    protected void ClearFilter()
    {
        JRA_Lib lObjWC = new JRA_Lib();
        lObjWC.Work_Categ_Parent_ID = null;
        lObjWC.Mode = 3;
        DataTable dtAllChilds = BLL_JRA_Work_Category.JRA_GET_WORK_CATEGORY_LIST(lObjWC);

        ddlChildWorkCateg.DataSource = dtAllChilds;
        ddlChildWorkCateg.DataTextField = "Work_Category_Display";
        ddlChildWorkCateg.DataValueField = "Work_Categ_ID";
        ddlChildWorkCateg.DataBind();
        ddlChildWorkCateg.Items.Insert(0, new ListItem("-Select All-", "0"));
        ddlParentWorkCateg.SelectedIndex = 0;
        ddlChildWorkCateg.SelectedIndex = 0;
        ddlParentWorkCateg.SelectedIndex = 0;
        ddlAssessmentStatus.SelectedIndex = 0;
        txtFromDate.Text = "";
        txtToDate.Text = "";
        DDLVessel.SelectedIndex = 0;
    }

    protected void onDelete(object source, CommandEventArgs e)
    {
        BLL_JRA_Hazards.DEL_HAZARD_TRMPLATE(Convert.ToInt32(e.CommandArgument.ToString()), Convert.ToInt32(Session["USERID"].ToString()));
         
        BindAssessmentTemplateGrid();
    }

    protected void onUpdate(object source, CommandEventArgs e)
    {
        //HiddenFlag.Value = "Edit";
        //OperationMode = "Edit Hazard Template";

        //int cnt = 0;
        //DataSet dt = BLL_JRA_Hazards.GET_HAZARD_TEMPLATE_LIST(e.CommandArgument.ToString(), null, null, null, null, null, null,ref cnt);

        //txtHazardID.Text = dt.Tables[0].Rows[0]["Hazard_ID"].ToString();
        //txtHazardDesc.Text = dt.Tables[0].Rows[0]["Hazard_Description"].ToString();
        //txtControlMeasure.Text = dt.Tables[0].Rows[0]["Control_Measure"].ToString();
        //txtInitiakRisk.Text = dt.Tables[0].Rows[0]["Initial_Risk"].ToString();
        //txtInitiakRiskValue.Text = dt.Tables[0].Rows[0]["Initial_Risk_Value"].ToString();
        //ddlSeverity.SelectedValue = dt.Tables[0].Rows[0]["Severity_ID"].ToString();
        //ddlLikelihood.SelectedValue = dt.Tables[0].Rows[0]["Likelihood_ID"].ToString();
        //txtAdditionalCntrolMeasure.Text = dt.Tables[0].Rows[0]["Additional_Control_Measures"].ToString();
        //ddlWorkCategory.SelectedValue = dt.Tables[0].Rows[0]["Work_Categ_ID"].ToString();
        //ddlModifiedRisk.SelectedValue = dt.Tables[0].Rows[0]["Modified_Risk_Value"].ToString();

        txtRemark.Text = "";
        hfAssessentKey.Value = e.CommandArgument.ToString();
        string AddHazardTemplate = String.Format("showModal('divadd',true);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddHazardTemplate", AddHazardTemplate, true);

    }

    protected void gvAssessment_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.Header)
        {
            if (ViewState["SORTBYCOLOUMN"] != null)
            {
                HtmlImage img = (HtmlImage)e.Row.FindControl(ViewState["SORTBYCOLOUMN"].ToString());
                if (img != null)
                {
                    if (ViewState["SORTDIRECTION"] == null || ViewState["SORTDIRECTION"].ToString() == "1")
                        img.Src = "~/purchase/Image/arrowDown.png";
                    else
                        img.Src = "~/purchase/Image/arrowUp.png";

                    img.Visible = true;
                }
            }
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes["onmouseover"] = "this.style.cursor='hand';this.style.backgroundColor='#FEECEC';";
            e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';this.style.color='black';this.style.backgroundColor=''";
        }

    }

    protected void gvAssessment_Sorting(object sender, GridViewSortEventArgs se)
    {
        ViewState["SORTBYCOLOUMN"] = se.SortExpression;

        if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "1")
            ViewState["SORTDIRECTION"] = 0;
        else
            ViewState["SORTDIRECTION"] = 1;

        BindAssessmentTemplateGrid();
    }



    protected void bntApprove_Click(object sender, EventArgs e)
    {
        int Assement_ID = UDFLib.ConvertToInteger(hfAssessentKey.Value.ToString().Split(';')[0]);
        int Vessel_ID = UDFLib.ConvertToInteger(hfAssessentKey.Value.ToString().Split(';')[1]);
        int Work_Categ_ID = UDFLib.ConvertToInteger(hfAssessentKey.Value.ToString().Split(';')[2]);
        hfAssessentKey.Value = null;
        BLL_JRA_Hazards.UPD_ASSESSMENT_STATUS(Assement_ID, Work_Categ_ID, Vessel_ID, "Approved", txtRemark.Text, GetSessionUserID());
        txtRemark.Text = "";
        string AddHazardTemplate = String.Format("hideModal('divadd',true);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddHazardTemplate", AddHazardTemplate, true);
        BindAssessmentTemplateGrid();
    }
    protected void btnRework_Click(object sender, EventArgs e)
    {
        int Assement_ID = UDFLib.ConvertToInteger(hfAssessentKey.Value.ToString().Split(';')[0]);
        int Vessel_ID = UDFLib.ConvertToInteger(hfAssessentKey.Value.ToString().Split(';')[1]);
        int Work_Categ_ID = UDFLib.ConvertToInteger(hfAssessentKey.Value.ToString().Split(';')[2]); 
        BLL_JRA_Hazards.UPD_ASSESSMENT_STATUS(Assement_ID, Work_Categ_ID, Vessel_ID, "Rework", txtRemark.Text, GetSessionUserID());
        txtRemark.Text = ""; 
        string AddHazardTemplate = String.Format("hideModal('divadd',true);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddHazardTemplate", AddHazardTemplate, true);
        BindAssessmentTemplateGrid();
    }
    protected void btnCloese_Click(object sender, EventArgs e)
    {
        hfAssessentKey.Value = null;
        txtRemark.Text = "";
        string AddHazardTemplate = String.Format("hideModal('divadd',true);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddHazardTemplate", AddHazardTemplate, true);
      
    }
}