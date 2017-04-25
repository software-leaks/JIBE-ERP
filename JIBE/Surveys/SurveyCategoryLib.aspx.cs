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


public partial class Surveys_SurveyCategoryLib : System.Web.UI.Page
{
    BLL_SURV_Survey objBLL = new BLL_SURV_Survey();
    UserAccess objUA = new UserAccess();

    public string OperationMode = "";
    public Boolean uaEditFlag = false;
    public Boolean uaDeleteFlage = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        UserAccessValidation();
        if (MainDiv.Visible)
        {
            if (!IsPostBack)
            {
                ViewState["SORTDIRECTION"] = null;
                ViewState["SORTBYCOLOUMN"] = null;

                ucCustomPagerItems.PageSize = 20;

                BindSurveyCategory();
            }
        }
    }
    public void Load_MainCategoryList()
    {
        DataTable dtContract = objBLL.Get_Survey_MainCategoryList();

        ddlMainCategory.DataSource = dtContract;
        ddlMainCategory.DataTextField = "Survey_Category";
        ddlMainCategory.DataValueField = "Id";
        ddlMainCategory.DataBind();
        ddlMainCategory.Items.Insert(0, new ListItem("-SELECT-", "0"));
    }

    public void BindSurveyCategory()
    {
        try
        {
            int rowcount = ucCustomPagerItems.isCountRecord;

            string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
            int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());


            DataTable dt = objBLL.Get_Survey_Category_Search(txtfilter.Text != "" ? txtfilter.Text : null, sortbycoloumn, sortdirection
                 , ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);

            if (ucCustomPagerItems.isCountRecord == 1)
            {
                ucCustomPagerItems.CountTotalRec = rowcount.ToString();
                ucCustomPagerItems.BuildPager();
            }

            GridView_Category.DataSource = dt;
            GridView_Category.DataBind();

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
        ddlCategoryType.SelectedIndex = 0;
        Load_MainCategoryList();
        ddlMainCategory.Enabled = false;
        lblMainCategoryMandatoryIcon.Visible = false;
        this.SetFocus("ctl00_MainContent_txtCatName");
        HiddenFlag.Value = "Add";
        OperationMode = "Add Category";
        ViewState["OperationMode"] = "Add Category";
        ClearField();
        ddlCategoryType.Enabled = true;
        string AddUserTypemodal = String.Format("showModal('divadd',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddUserTypemodal", AddUserTypemodal, true);
    }

    protected void ClearField()
    {
        txtCategoryID.Text = "";
        txtCatName.Text = "";
    }
    /// <summary>
    /// To save /update Category. 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnsave_Click(object sender, EventArgs e)
    {
        try
        {
            int MainCategoryId = 0; string EditCategory = "";

            if (ddlCategoryType.SelectedValue.ToString() == "Sub Category")
            {
                MainCategoryId = int.Parse(ddlMainCategory.SelectedValue.ToString());
                if (MainCategoryId == 0)
                {
                    if (ViewState["OperationMode"] != null)
                        OperationMode = ViewState["OperationMode"].ToString();
                    EditCategory = String.Format("alert('Main category is mandatory.');showModal('divadd',false);");
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "EditCategorymodal", EditCategory, true);
                }
            }
            if (EditCategory == "")
            {
                if (HiddenFlag.Value == "Add")
                {
                    int responseid = objBLL.INSERT_Survey_Category(MainCategoryId, txtCatName.Text.Trim(), GetSessionUserID());
                    if (responseid == 1 || responseid == -6)
                    {
                        EditCategory = String.Format("alert('Category is saved successfully.');showModal('divadd',false);");
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "EditCategorymodal", EditCategory, true);
                        string hidemodal = String.Format("hideModal('divadd')");
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hidemodal", hidemodal, true);
                    }
                    else if (responseid == 2)
                    {
                        EditCategory = String.Format("alert('Main category already exists.');showModal('divadd',false);");
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "EditCategorymodal", EditCategory, true);
                        string hidemodal = String.Format("showModal('divadd')");
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hidemodal", hidemodal, true);
                    }
                    else if (responseid == 3)
                    {
                        EditCategory = String.Format("alert('Sub sategory already exists in the selected Main Category.');showModal('divadd',false);");
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "EditCategorymodal", EditCategory, true);
                        string hidemodal = String.Format("showModal('divadd')");
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hidemodal", hidemodal, true);
                    }
                    else
                    {
                        EditCategory = String.Format("alert('There is a problem while saving Category. ');showModal('divadd',false);");
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "EditCategorymodal", EditCategory, true);
                        string hidemodal = String.Format("showModal('divadd')");
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hidemodal", hidemodal, true);
                    }


                }
                else
                {
                    int responseid = objBLL.UPDATE_Survey_Category(MainCategoryId, Convert.ToInt32(txtCategoryID.Text.Trim()), txtCatName.Text.Trim(), GetSessionUserID());
                    if (responseid == 1 || responseid == -6)
                    {
                        EditCategory = String.Format("alert('Category is updated successfully.');showModal('divadd',false);");
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "EditCategorymodal", EditCategory, true);

                        string hidemodal = String.Format("hideModal('divadd')");
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hidemodal", hidemodal, true);
                    }
                    else if (responseid == 2)
                    {
                        EditCategory = String.Format("alert('Main Category already exists.');showModal('divadd',false);");
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "EditCategorymodal", EditCategory, true);
                        string hidemodal = String.Format("showModal('divadd')");
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hidemodal", hidemodal, true);
                    }
                    else if (responseid == 3)
                    {
                        EditCategory = String.Format("alert('Sub-category name already exists in the selected Main Category.');showModal('divadd',false);");
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "EditCategorymodal", EditCategory, true);
                        string hidemodal = String.Format("showModal('divadd')");
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hidemodal", hidemodal, true);
                    }
                    else
                    {
                        EditCategory = String.Format("There is a problem while updating Category. ');showModal('divadd',false);");
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "EditCategorymodal", EditCategory, true);
                        string hidemodal = String.Format("showModal('divadd')");
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hidemodal", hidemodal, true);
                    }

                }

                BindSurveyCategory();

            }
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

            Load_MainCategoryList();
            HiddenFlag.Value = "Edit";
            OperationMode = "Edit Category";
            ViewState["OperationMode"] = "Edit Category";
            DataTable dt = new DataTable();
            dt = objBLL.Get_Survey_CategoryList(Convert.ToInt32(e.CommandArgument.ToString()));

            if (dt.Rows.Count > 0)
            {
                txtCategoryID.Text = dt.Rows[0]["ID"].ToString();
                txtCatName.Text = dt.Rows[0]["Survey_Category"].ToString();
                try
                {
                    ddlMainCategory.SelectedValue = dt.Rows[0]["MainCategoryId"].ToString();
                }
                catch { ddlMainCategory.SelectedIndex = 0; }

                if (dt.Rows[0]["MainCategoryId"].ToString() == "0")
                {
                    ddlCategoryType.SelectedValue = "Main Category";
                    ddlMainCategory.Enabled = false;
                    lblMainCategoryMandatoryIcon.Visible = false;
                }
                else
                {
                    ddlCategoryType.SelectedValue = "Sub Category";
                    ddlMainCategory.Enabled = true;
                    lblMainCategoryMandatoryIcon.Visible = true;
                }
                ddlCategoryType.Enabled = false;

                string EditCategorymodal = String.Format("showModal('divadd',false);");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "EditCategorymodal", EditCategorymodal, true);
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
            int retval = objBLL.DELETE_Survey_Category(Convert.ToInt32(e.CommandArgument.ToString()), Convert.ToInt32(Session["USERID"]));
            BindSurveyCategory();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        BindSurveyCategory();
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        txtfilter.Text = "";
        BindSurveyCategory();

    }

    protected void ImgExpExcel_Click(object sender, EventArgs e)
    {
        try
        {
            int rowcount = ucCustomPagerItems.isCountRecord;

            string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
            int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());


            DataTable dt = objBLL.Get_Survey_Category_Search(txtfilter.Text != "" ? txtfilter.Text : null, sortbycoloumn, sortdirection
                 , null, null, ref  rowcount);


            string[] HeaderCaptions = { "Main Category", "Sub Category" };
            string[] DataColumnsName = { "MainCategory", "Survey_Category" };

            GridViewExportUtil.ShowExcel(dt, HeaderCaptions, DataColumnsName, "Survey Category", "SurveyCategory", "");
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    protected void GridView_Category_RowDataBound(object sender, GridViewRowEventArgs e)
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

    protected void GridView_Category_Sorting(object sender, GridViewSortEventArgs se)
    {
        ViewState["SORTBYCOLOUMN"] = se.SortExpression;

        if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
            ViewState["SORTDIRECTION"] = 1;
        else
            ViewState["SORTDIRECTION"] = 0;

        BindSurveyCategory();
    }

    protected void ddlCategoryType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["OperationMode"] != null)
                OperationMode = ViewState["OperationMode"].ToString();
            if (ddlCategoryType.SelectedValue.ToString() == "Main Category")
            {
                ddlMainCategory.Enabled = false;
                ddlMainCategory.SelectedIndex = 0;
                lblMainCategoryMandatoryIcon.Visible = false;
            }
            else
            {
                ddlMainCategory.Enabled = true;
                lblMainCategoryMandatoryIcon.Visible = true;
            }
            string AddUserTypemodal = String.Format("showModal('divadd',false);");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddUserTypemodal", AddUserTypemodal, true);
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
}