using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Properties;
using SMS.Business.Crew;
using SMS.Business.Survey;
using SMS.Business.Infrastructure;
using System.Data;
using System.Web.UI.HtmlControls;

public partial class SurveyCertificateLib : System.Web.UI.Page
{

    BLL_SURV_Survey objBLL = new BLL_SURV_Survey();
    UserAccess objUA = new UserAccess();

    public string OperationMode = "";
    public Boolean uaEditFlag = false;
    public Boolean uaDeleteFlage = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (GetSessionUserID() == 0)
                Response.Redirect("~/account/login.aspx");

            UserAccessValidation();
            if (MainDiv.Visible)
            {
                if (!IsPostBack)
                {
                    ViewState["SORTDIRECTION"] = null;
                    ViewState["SORTBYCOLOUMN"] = null;
                    Load_MainCategoryList(ddlMainCategory);
                    Load_CategoryList(ddlMainCategory, ddlCategoryFilter);
                    ucCustomPagerItems.PageSize = 100;

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Filter", "Filter();", true);
                   // BindSurveyCertificate();
                }
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    public void Load_MainCategoryList(DropDownList ddl)
    {
        try
        {
            DataTable dtMainCategory = objBLL.Get_Survey_MainCategoryList();

            ddl.DataSource = dtMainCategory;
            ddl.DataTextField = "Survey_Category";
            ddl.DataValueField = "Id";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("-SELECT-", "0"));
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    public void Load_CategoryList(DropDownList ddl_Parent, DropDownList ddl)
    {
        try
        {
            ddl.Items.Clear();
            ddl.DataSource = objBLL.Get_Survey_CategoryList_ByMainCategoryId(int.Parse(ddl_Parent.SelectedValue.ToString()));
            ddl.DataTextField = "Survey_Category";
            ddl.DataValueField = "ID";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("-SELECT-", "0"));
            ddl.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }

    }
    public void BindSurveryCategoryDLL()
    {


        DataTable dtComptype = objBLL.Get_Survay_CategoryList();

        ddlSurvey_Category.DataSource = dtComptype;
        ddlSurvey_Category.DataTextField = "Survey_Category";
        ddlSurvey_Category.DataValueField = "ID";
        ddlSurvey_Category.DataBind();
        ddlSurvey_Category.Items.Insert(0, new ListItem("-Select-", "0"));

        ddlCategoryFilter.DataSource = dtComptype;
        ddlCategoryFilter.DataTextField = "Survey_Category";
        ddlCategoryFilter.DataValueField = "ID";
        ddlCategoryFilter.DataBind();
        ddlCategoryFilter.Items.Insert(0, new ListItem("-ALL-", "0"));


    }


    protected void ddlMainCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        Load_CategoryList(ddlMainCategory, ddlCategoryFilter);
    }
    protected void ddlSurvey_MainCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        Load_CategoryList(ddlSurvey_MainCategory, ddlSurvey_Category);
        string AddCertificatemodal = String.Format("showModal('divadd',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddCertificatemodal", AddCertificatemodal, true);
    }

    /// <summary>
    /// to bind saved /updated survey certificate to grid view.
    /// </summary>
    public void BindSurveyCertificate()
    {
        try
        {
            int rowcount = ucCustomPagerItems.isCountRecord;

            string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
            int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

            DataTable dt = objBLL.Get_Survey_Certificate_Search(txtfilter.Text != "" ? txtfilter.Text : null, UDFLib.ConvertIntegerToNull(ddlMainCategory.SelectedValue), UDFLib.ConvertIntegerToNull(ddlCategoryFilter.SelectedValue), sortbycoloumn, sortdirection
                 , ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);

            if (ucCustomPagerItems.isCountRecord == 1)
            {
                ucCustomPagerItems.CountTotalRec = rowcount.ToString();
                ucCustomPagerItems.BuildPager();
            }
            GridView_Certificate.DataSource = dt;
            GridView_Certificate.DataBind();
            if (dt.Rows.Count > 0)
                ImgExpExcel.Visible = true;
            else
                ImgExpExcel.Visible = false;

        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
        {
            MainDiv.Visible = false;
            AccessMsgDiv.Visible = true;
        }
        else
        {
            MainDiv.Visible = true;
            AccessMsgDiv.Visible = false;
        }

        if (objUA.Add == 0) ImgAdd.Visible = false;
        if (objUA.Edit == 1)
            uaEditFlag = true;
        if (objUA.Delete == 1) uaDeleteFlage = true;

    }

    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }

    protected void ImgAdd_Click(object sender, EventArgs e)
    {
        btnsave.Visible = true;
        this.SetFocus("ctl00_MainContent_txtCertificateName");
        HiddenFlag.Value = "Add";
        OperationMode = "Add Certificate";

        ClearField();
        Load_MainCategoryList(ddlSurvey_MainCategory);
        Load_CategoryList(ddlSurvey_MainCategory, ddlSurvey_Category);

        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddCertificatemodal", "showModal('divadd',false);", true);
    }

    protected void ClearField()
    {
        txtCertificateID.Text = "";
        txtCertificateName.Text = "";
        txtTerm.Text = "";
        txtSurvey_Cert_remarks.Text = "";
        txtGraceRange.Text = "";
        chkInspectionRequired.Checked = false;
        chkAlert.Checked = false;
    }

    /// <summary>
    /// Modified by Anjali DT:10-06-2016
    ///  To save /Update survey certificate to DB.
    /// </summary>

    protected void btnsave_Click(object sender, EventArgs e)
    {
        try
        {
            string EditCertificate = "";


            if (HiddenFlag.Value == "Add")
            {
                int responseid = objBLL.INSERT_Survey_Certificate(txtCertificateName.Text.Trim(), UDFLib.ConvertToInteger(ddlSurvey_Category.SelectedValue), GetSessionUserID(), UDFLib.ConvertToInteger(txtTerm.Text), txtSurvey_Cert_remarks.Text
                    , chkAlert.Checked == true ? 1 : 0, UDFLib.ConvertToInteger(txtGraceRange.Text), chkInspectionRequired.Checked);

                if (responseid == 1)
                {
                    EditCertificate = String.Format("alert('Certificate is saved Successfully.');showModal('divadd',false);");
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "EditCertificatemodal", EditCertificate, true);
                    string hidemodal = String.Format("hideModal('divadd')");
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hidemodal", hidemodal, true);
                }
                else if (responseid == 3)
                {
                    EditCertificate = String.Format("alert('Certificate name already exists in the selected category.');showModal('divadd',false);");
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "EditCertificatemodal", EditCertificate, true);
                    string hidemodal = String.Format("showModal('divadd')");
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hidemodal", hidemodal, true);
                }
                else
                {
                    EditCertificate = String.Format("alert('There is a problem while saving certificate. ');showModal('divadd',false);");
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "EditCertificatemodal", EditCertificate, true);
                    string hidemodal = String.Format("showModal('divadd')");
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hidemodal", hidemodal, true);
                }
            }
            else
            {
                int responseid = objBLL.UPDATE_Survey_Certificate(Convert.ToInt32(txtCertificateID.Text.Trim()), txtCertificateName.Text.Trim(), UDFLib.ConvertToInteger(ddlSurvey_Category.SelectedValue), GetSessionUserID()
                    , UDFLib.ConvertToInteger(txtTerm.Text), txtSurvey_Cert_remarks.Text, chkAlert.Checked == true ? 1 : 0, UDFLib.ConvertToInteger(txtGraceRange.Text), chkInspectionRequired.Checked);

                if (responseid == 0)
                {
                    EditCertificate = String.Format("alert('Certificate is updated successfully.');showModal('divadd',false);");
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "EditCertificatemodal", EditCertificate, true);
                    string hidemodal = String.Format("hideModal('divadd')");
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hidemodal", hidemodal, true);
                }
                else if (responseid == 2)
                {
                    EditCertificate = String.Format("alert('Certificate name already exists in the selected category.');showModal('divadd',false);");
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "EditCertificatemodal", EditCertificate, true);
                    string hidemodal = String.Format("hideModal('divadd')");
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hidemodal", hidemodal, true);
                }
                else
                {
                    EditCertificate = String.Format("alert('There is a problem while updating certificate.');showModal('divadd',false);");
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "EditCertificatemodal", EditCertificate, true);
                    string hidemodal = String.Format("showModal('divadd')");
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hidemodal", hidemodal, true);
                }
            }

            BindSurveyCertificate();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    protected void onUpdate(object source, CommandEventArgs e)
    {
        try
        {
            if (uaEditFlag == true)
                btnsave.Visible = true;
            else
                btnsave.Visible = false;
            HiddenFlag.Value = "Edit";
            OperationMode = "Edit Survey Certificate";

            ClearField();
            Load_MainCategoryList(ddlSurvey_MainCategory);


            DataTable dt = new DataTable();
            dt = objBLL.Get_Survey_Certificate_List_By_SurvID(Convert.ToInt32(e.CommandArgument.ToString()));

            if (dt.Rows.Count > 0)
            {
                txtCertificateID.Text = dt.Rows[0]["Surv_ID"].ToString();
                txtCertificateName.Text = dt.Rows[0]["Survey_Cert_Name"].ToString();
                ddlSurvey_MainCategory.SelectedValue = dt.Rows[0]["MainCategoryId"].ToString() != "" ? dt.Rows[0]["MainCategoryId"].ToString() : "0";
                Load_CategoryList(ddlSurvey_MainCategory, ddlSurvey_Category);
                ddlSurvey_Category.SelectedValue = dt.Rows[0]["Survey_Category_ID"].ToString() != "" ? dt.Rows[0]["Survey_Category_ID"].ToString() : "0";

                txtTerm.Text = "";
                if (dt.Rows[0]["Term"].ToString() != "0")
                    txtTerm.Text = dt.Rows[0]["Term"].ToString();

                txtSurvey_Cert_remarks.Text = dt.Rows[0]["Survey_Cert_remarks"].ToString();
                chkInspectionRequired.Checked = Convert.ToBoolean(dt.Rows[0]["InspectionRequired"]);

                if (dt.Rows[0]["Alert_Insurance"].ToString() == "1")
                    chkAlert.Checked = true;
                else
                    chkAlert.Checked = false;

                txtGraceRange.Text = "";
                if (dt.Rows[0]["GraceRange"].ToString() != "0")
                    txtGraceRange.Text = dt.Rows[0]["GraceRange"].ToString();

                string EditCertificatemodal = String.Format("showModal('divadd',false);");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "EditCertificatemodal", EditCertificatemodal, true);
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    protected void onDelete(object source, CommandEventArgs e)
    {
        try
        {
            int retval = objBLL.DELETE_Survey_Certificate(Convert.ToInt32(e.CommandArgument.ToString()), Convert.ToInt32(Session["USERID"]));
            BindSurveyCertificate();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        BindSurveyCertificate();
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        txtfilter.Text = "";
        ddlCategoryFilter.SelectedValue = "0";
        ddlMainCategory.SelectedValue = "0";
        ucCustomPagerItems.CurrentPageIndex = 1;
        BindSurveyCertificate();
    }

    protected void ImgExpExcel_Click(object sender, EventArgs e)
    {
        try
        {
            int rowcount = ucCustomPagerItems.isCountRecord;

            string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
            int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

            DataTable dt = objBLL.Get_Survey_Certificate_Search(txtfilter.Text != "" ? txtfilter.Text : null, UDFLib.ConvertIntegerToNull(ddlMainCategory.SelectedValue), UDFLib.ConvertIntegerToNull(ddlCategoryFilter.SelectedValue), sortbycoloumn, sortdirection
                 , null, null, ref  rowcount);


            string[] HeaderCaptions = { "Name", "Main Category", "Sub Category", "Remark", "Term", "Alert(Insurance)", "Range(Months)", "Inspection Required?" };
            string[] DataColumnsName = { "Survey_Cert_Name", "Survey_MainCategory", "Survey_Category", "Survey_Cert_remarks", "Term", "Alert_Insurance_Excel", "GraceRange", "InspectionRequired_Excel" };

            GridViewExportUtil.ShowExcel(dt, HeaderCaptions, DataColumnsName, "Survey Certificate", "SurveyCertificate", "");
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    protected void GridView_Certificate_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
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
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    protected void GridView_Certificate_Sorting(object sender, GridViewSortEventArgs se)
    {
        ViewState["SORTBYCOLOUMN"] = se.SortExpression;

        if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
            ViewState["SORTDIRECTION"] = 1;
        else
            ViewState["SORTDIRECTION"] = 0;

        BindSurveyCertificate();
    }
}