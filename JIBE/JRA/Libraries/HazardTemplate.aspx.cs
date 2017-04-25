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
using System.Globalization;
using System.Collections.Generic;
using System.Collections;
public partial class JRA_Libraries_HazardTemplate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        UserAccessValidation();

        try
        {
            GridViewHelper helper = new GridViewHelper(gvHazard);
            helper.RegisterGroup("Work_Category_Name", true, true);
            helper.GroupHeader += new GroupEvent(helper_GroupHeader);
        }
        catch (Exception)
        {


        }


        if (!IsPostBack)
        {
            ViewState["SORTDIRECTION"] = null;
            ViewState["SORTBYCOLOUMN"] = null;

            ucCustomPagerItems.PageSize = 20;
            BindCombos();

            if (Request.QueryString !=null )
            {
                ddlChildWorkCateg.SelectedValue = Request.QueryString["DocID"];
                BindHazardTemplateGrid();
            }
            else
            {
                BindHazardTemplateGrid();
            }
          

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
        ddlParentWorkCateg.Items.Insert(0, new ListItem("-Select All-", "0"));

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
        ddlChildWorkCateg.Items.Insert(0, new ListItem("-Select All-", "0"));
        ddlParentWorkCateg.SelectedIndex = 0;
        ddlChildWorkCateg.SelectedIndex = 0;

        ddlWorkCategory.DataSource = dtAllChilds;
        ddlWorkCategory.DataTextField = "Work_Category_Display";
        ddlWorkCategory.DataValueField = "Work_Categ_ID";
        ddlWorkCategory.DataBind();
        ddlWorkCategory.Items.Insert(0, new ListItem("-Select All-", "0"));
        ddlWorkCategory.SelectedIndex = 0;
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
    protected void ddlParentWorkCateg_SelectedIndexChanged(object sender, EventArgs e)
    {
        JRA_Lib lObjWC = new JRA_Lib();
        if (ddlParentWorkCateg.SelectedIndex <= 0)
        {
            lObjWC.Work_Categ_Parent_ID = null;
            lObjWC.Mode = 3;
        }
        else
        {
            lObjWC.Work_Categ_Parent_ID = UDFLib.ConvertToInteger(ddlParentWorkCateg.SelectedValue);
            lObjWC.Mode = 1;
        }


        DataTable dtChilds = BLL_JRA_Work_Category.JRA_GET_WORK_CATEGORY_LIST(lObjWC);


        DataView dv;
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
        ddlChildWorkCateg.Items.Insert(0, new ListItem("-Select All-", "0"));
        ddlChildWorkCateg.SelectedIndex = 0;
        BindHazardTemplateGrid();
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
        }
        if (objUA.Edit == 1)
            uaEditFlag = true;
        else
            btnsave.Visible = false;
        if (objUA.Delete == 1) uaDeleteFlage = true;

    }

    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }

    public void BindHazardTemplateGrid()
    {

        int rowcount = ucCustomPagerItems.isCountRecord;

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        string sortdirection = null;
        if (ViewState["SORTDIRECTION"] != null)
            sortdirection = ViewState["SORTDIRECTION"].ToString();


        DataTable dt = BLL_JRA_Hazards.GET_HAZARD_TEMPLATE_LIST(
            null,
            ddlChildWorkCateg.SelectedIndex <= 0 ? null : ddlChildWorkCateg.SelectedValue,
            "",
            ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, sortbycoloumn,
            sortdirection, ref rowcount).Tables[0];


        if (ucCustomPagerItems.isCountRecord == 1)
        {
            ucCustomPagerItems.CountTotalRec = rowcount.ToString();
            ucCustomPagerItems.BuildPager();
        }

        if (dt.Rows.Count > 0)
        {
            gvHazard.DataSource = dt;
            gvHazard.DataBind();
        }
        else
        {
            gvHazard.DataSource = dt;
            gvHazard.DataBind();
        }

    }


    protected void btnUpd_OnClick(object sender, EventArgs e)
    {
        HiddenFlag.Value = "Edit";
        OperationMode = "Edit Hazard Template";

        int cnt = 0;
        DataSet dt = BLL_JRA_Hazards.GET_HAZARD_TEMPLATE_LIST(hfHzID.Value, null, null, null, null, null, null, ref cnt);

        txtHazardID.Text = dt.Tables[0].Rows[0]["Hazard_ID"].ToString();
        txtHazardDesc.Text = dt.Tables[0].Rows[0]["Hazard_Description"].ToString();
        txtControlMeasure.Text = dt.Tables[0].Rows[0]["Control_Measure"].ToString();
        txtInitiakRisk.Text = dt.Tables[0].Rows[0]["Initial_Risk"].ToString();
        txtInitiakRiskValue.Text = dt.Tables[0].Rows[0]["Initial_Risk_Value"].ToString();
        ddlSeverity.SelectedValue = dt.Tables[0].Rows[0]["Severity_ID"].ToString();
        ddlLikelihood.SelectedValue = dt.Tables[0].Rows[0]["Likelihood_ID"].ToString();
        txtAdditionalCntrolMeasure.Text = dt.Tables[0].Rows[0]["Additional_Control_Measures"].ToString();
        ddlWorkCategory.SelectedValue = dt.Tables[0].Rows[0]["Work_Categ_ID"].ToString();
        ddlModifiedRisk.SelectedValue = dt.Tables[0].Rows[0]["Modified_Risk_Value"].ToString();

        string AddHazardTemplate = String.Format("showModal('divadd',true);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddHazardTemplate", AddHazardTemplate, true);
        BindHazardTemplateGrid();
    }
    protected void btnsave_Click(object sender, EventArgs e)
    {



        if (HiddenFlag.Value == "Add")
        {

            BLL_JRA_Hazards.INSUPD_HAZARD_TRMPLATE(null, UDFLib.ConvertToInteger(ddlWorkCategory.SelectedValue), txtHazardDesc.Text, txtControlMeasure.Text, UDFLib.ConvertToInteger(ddlSeverity.SelectedValue), UDFLib.ConvertToInteger(ddlLikelihood.SelectedValue), UDFLib.ConvertToInteger(txtInitiakRiskValue.Text), txtAdditionalCntrolMeasure.Text, ddlModifiedRisk.SelectedItem.Value, GetSessionUserID());

        }
        else
        {
            BLL_JRA_Hazards.INSUPD_HAZARD_TRMPLATE(UDFLib.ConvertToInteger(txtHazardID.Text), UDFLib.ConvertToInteger(ddlWorkCategory.SelectedValue), txtHazardDesc.Text, txtControlMeasure.Text, UDFLib.ConvertToInteger(ddlSeverity.SelectedValue), UDFLib.ConvertToInteger(ddlLikelihood.SelectedValue), UDFLib.ConvertToInteger(txtInitiakRiskValue.Text), txtAdditionalCntrolMeasure.Text, ddlModifiedRisk.SelectedItem.Value, GetSessionUserID());


        }

        BindHazardTemplateGrid();

        string hidemodal = String.Format("hideModal('divadd')");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hidemodal", hidemodal, true);
    }

    protected void ImgAdd_Click(object sender, EventArgs e)
    {
        //this.SetFocus("ctl00_MainContent_txtPortName");
        HiddenFlag.Value = "Add";

        OperationMode = "Add Hazard Template";

        ClearFields();

        string AddPort = String.Format("showModal('divadd',true);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddPort", AddPort, true);

        BindHazardTemplateGrid();
    }

    protected void ImgExpExcel_Click(object sender, EventArgs e)
    {

        int rowcount = ucCustomPagerItems.isCountRecord;

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        string sortdirection = null;
        if (ViewState["SORTDIRECTION"] != null)
            sortdirection = ViewState["SORTDIRECTION"].ToString();



        DataTable dt = BLL_JRA_Hazards.GET_HAZARD_TEMPLATE_LIST(
            null,
            ddlChildWorkCateg.SelectedIndex <= 0 ? null : ddlChildWorkCateg.SelectedValue,
            "",
            ucCustomPagerItems.CurrentPageIndex, null, sortbycoloumn,
            sortdirection, ref rowcount).Tables[0];

        string Header = (ddlChildWorkCateg.SelectedIndex > 0 ? ddlChildWorkCateg.SelectedItem.Text : "") + " Hazard Template";

        string[] HeaderCaptions = { "Work Category", "Hazard Description", "Control Measure", "Severity", "Likelihood", "Initial Risk", "Additional Control Measures", "Modified Risk" };
        string[] DataColumnsName = { "Work_Category_Name", "Hazard_Description", "Control_Measure", "Severity", "Likelihood", "Initial_Risk", "Additional_Control_Measures", "Modified_Risk" };

        GridViewExportUtil.ShowExcel(dt, HeaderCaptions, DataColumnsName, "HazardTemplate", Header, "");
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
        ddlWorkCategory.SelectedIndex = 0;



    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {

        BindHazardTemplateGrid();

    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {

        ClearFilter();
        BindHazardTemplateGrid();
    }

    protected void ClearFilter()
    {

        JRA_Lib lObjWC = new JRA_Lib();
        lObjWC.Work_Categ_Parent_ID = null;
        lObjWC.Mode = 3;
        DataTable dtAllChilds = BLL_JRA_Work_Category.JRA_GET_WORK_CATEGORY_LIST(lObjWC);




        DataTable dtAllChildsCloned = dtAllChilds.Clone();
        dtAllChildsCloned.Columns["Work_Categ_Value"].DataType = typeof(float);
        foreach (DataRow row in dtAllChilds.Rows)
        {
            dtAllChildsCloned.ImportRow(row);
        }





        DataView dv = dtAllChildsCloned.DefaultView;
        dv.Sort = "Work_Categ_Value";
        dtAllChilds = dv.ToTable();
        ddlChildWorkCateg.DataSource = dtAllChilds;
        ddlChildWorkCateg.DataTextField = "Work_Category_Display";
        ddlChildWorkCateg.DataValueField = "Work_Categ_ID";
        ddlChildWorkCateg.DataBind();
        ddlChildWorkCateg.Items.Insert(0, new ListItem("-Select All-", "0"));
        ddlParentWorkCateg.SelectedIndex = 0;
        ddlChildWorkCateg.SelectedIndex = 0;
    }

    protected void onDelete(object source, CommandEventArgs e)
    {

    }
    protected void btnDel_OnClick(object sender, EventArgs e)
    {
        if (hfAns.Value.ToString() == "true")
        {
            BLL_JRA_Hazards.DEL_HAZARD_TRMPLATE(Convert.ToInt32(hfHzID.Value), Convert.ToInt32(Session["USERID"].ToString()));


        }
        BindHazardTemplateGrid();

    }

    protected void onUpdate(object source, CommandEventArgs e)
    {


    }

    protected void gvHazard_RowDataBound(object sender, GridViewRowEventArgs e)
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

    }

    protected void gvHazard_Sorting(object sender, GridViewSortEventArgs se)
    {
        ViewState["SORTBYCOLOUMN"] = se.SortExpression;

        if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
            ViewState["SORTDIRECTION"] = 1;
        else
            ViewState["SORTDIRECTION"] = 0;

        BindHazardTemplateGrid();
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
        }
        else
        {
            txtInitiakRiskValue.Text = "";
            txtInitiakRisk.Text = "";
        }
        string AddPort = String.Format("showModal('divadd',true);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddPort", AddPort, true);
        BindHazardTemplateGrid();
    }

    private void helper_GroupHeader(string groupName, object[] values, GridViewRow row)
    {
        if (groupName == "Work_Category_Name")
        {
            row.BackColor = System.Drawing.Color.LightGray;
            row.Cells[0].HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Left;
            row.Cells[0].Text = "&nbsp;&nbsp;" + row.Cells[0].Text;
            row.Cells[0].ForeColor = System.Drawing.Color.Black;
            row.Cells[0].Font.Bold = true;

        }

    }
}