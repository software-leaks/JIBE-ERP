using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Reflection;
using System.Data;
using SMS.Properties;
using SMS.Business.JRA;

public partial class JRA_Libraries_WorkCategory : System.Web.UI.Page
{
    #region Page Load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            UserAccessValidation();
            Load_CategoryList(null,0);


            //GroupGridviewHeader();
            ucCustomPagerItems.PageSize = 10;
            Search_WorkCategory();
            
        }
    }
    #endregion

    #region General Functions

    protected void UserAccessValidation()
    {
        SMS.Properties.UserAccess objUA = new SMS.Properties.UserAccess();

        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        SMS.Business.Infrastructure.BLL_Infra_UserCredentials objUser = new SMS.Business.Infrastructure.BLL_Infra_UserCredentials();
        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
            Response.Redirect("~/default.aspx?msgid=1");

        if (objUA.Add == 0)
        {
            lnkAddNewCategory.Visible = false;
            
            //lnkAddNewGrade.Visible = false;
        }
        if (objUA.Edit == 0)
        {
            GridView_Category.Columns[GridView_Category.Columns.Count - 2].Visible = false;
            
            //GridView_Grading.Columns[GridView_Grading.Columns.Count - 2].Visible = false;

        }
        if (objUA.Delete == 0)
        {
            GridView_Category.Columns[GridView_Category.Columns.Count - 1].Visible = false;
           
            //GridView_Grading.Columns[GridView_Grading.Columns.Count - 1].Visible = false;
        }
        if (objUA.Approve == 0)
        {
        }

    }

    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }

    protected void Load_CategoryList(int? ParentCateID,int Mode)
    {
        JRA_Lib ObjJRALib = new JRA_Lib();
        ObjJRALib.Mode = Mode;
        ObjJRALib.Work_Categ_Parent_ID =ParentCateID;

        DataTable dt=BLL_JRA_Work_Category.JRA_GET_WORK_CATEGORY_LIST(ObjJRALib);
        ddlParentCat.DataSource = dt;
        ddlParentCat.DataTextField = Convert.ToString(dt.Columns["Work_Category_Name"]);
        ddlParentCat.DataValueField = Convert.ToString(dt.Columns["Work_Categ_ID"]);
        ddlParentCat.DataBind();
        ddlParentCat.Items.Insert(0, new ListItem("-Select-", "0"));
        ddlFiter.DataSource = dt;
        ddlFiter.DataTextField = Convert.ToString(dt.Columns["Work_Category_Name"]);
        ddlFiter.DataValueField = Convert.ToString(dt.Columns["Work_Categ_ID"]);
        ddlFiter.DataBind();
        ddlFiter.Items.Insert(0, new ListItem("-Select-", "0"));
       
    }
    protected void Search_WorkCategory()
    {
        int rowcount = ucCustomPagerItems.isCountRecord;
        int SearchRows = 0;
        int TotalRows = 0;

        JRA_Lib JRALib_Data = new JRA_Lib();
        JRALib_Data.SearchText = txtfilter.Text.Trim();
        JRALib_Data.SearchCate = ddlFiter.SelectedValue == "0" ? null : ddlFiter.SelectedValue;
        JRALib_Data.PageNumber = ucCustomPagerItems.CurrentPageIndex;
        JRALib_Data.PageSize = ucCustomPagerItems.PageSize;

        DataTable dt = BLL_JRA_Work_Category.JRA_SEARCH_WORK_CATEGORY(JRALib_Data);
        GridView_Category.DataSource = dt;
        GridView_Category.DataBind();

        if (dt.Rows.Count > 0)
        {
            SearchRows=Convert.ToInt32(dt.Rows[0]["SearchRows"]);
            TotalRows = Convert.ToInt32(dt.Rows[0]["TotalRows"]);
        }
        
        if (ucCustomPagerItems.isCountRecord == 1)
        {
            rowcount = SearchRows;
            ucCustomPagerItems.CountTotalRec = Convert.ToString(TotalRows);
            ucCustomPagerItems.BuildPager();
        }
    }

    
    private void Load_WorkCategory()
    {
        JRA_Lib JRALib_Data = new JRA_Lib();
        JRALib_Data.Type_ID = 0;
        DataTable dt = BLL_JRA_Work_Category.JRA_GET_TYPE_LIST(JRALib_Data);
       
    }

    private void SaveWorkCategory(string DB_Mode)
    {
        JRA_Lib JRALib_Data = new JRA_Lib();
        JRALib_Data.UserID = Convert.ToInt32(Session["USERID"]);
        JRALib_Data.Work_Categ_ID = Convert.ToInt32(0);
        JRALib_Data.Work_Categ_Parent_ID = Convert.ToInt32(ddlParentCat.SelectedValue);
        JRALib_Data.Work_Categ_Value = txtCatVal.Text.Trim();
        JRALib_Data.Work_Category_Name = txtCatName.Text.Trim();
        JRALib_Data.DB_Mode = DB_Mode;


        int result = BLL_JRA_Work_Category.JRA_INS_WorkCategory(JRALib_Data);
        if (result == 999)//CHECK FOR DUPLICATE WORK CATEGORY NAME
        {
            string js = "";
            js = "alert('Work Category Name already exists.');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", js, true);
        }
    }

    private bool ValidateCategory(string CatVal, string CatName)
    {
        string js = "";
        bool Validate = true;
        float testfloat;

        try
        {
            if (CatVal.Trim() == "")
            {
                js = "alert('Enter Category Code');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", js, true);
                Validate = false;
            }
            if (float.TryParse(CatVal.Trim(), out testfloat) == false)
            {
                js = "alert('Category Code not valid');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", js, true);
                Validate = false;
            }

            if (CatName.Trim() == "")
            {
                js = "alert('Enter Category Name');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", js, true);
                Validate = false;
            }
            if (ddlParentCat.SelectedIndex == 0)
            {

                int testInt;
                bool res = int.TryParse(CatVal.Trim(), out testInt);
                if (!(res && testInt > 0))
                {
                    js = "alert('Category Code not valid');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", js, true);
                    Validate = false;
                }
                else
                {
                    JRA_Lib ObjJRALib = new JRA_Lib();
                    ObjJRALib.Mode = 0;
                    ObjJRALib.Work_Categ_Parent_ID = null;

                    DataTable dt = BLL_JRA_Work_Category.JRA_GET_WORK_CATEGORY_LIST(ObjJRALib);
                    if (dt.Select("Work_Categ_Value='" + CatVal.Trim() + "'").Length > 0)
                    {
                        js = "alert('Category value already exists');";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", js, true);
                        Validate = false;
                    }
                }


            }
            else
            {
                if (!CatVal.Contains('.'))
                {
                    js = "alert('Category Code not valid');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", js, true);
                    Validate = false;
                }
                JRA_Lib ObjJRALib = new JRA_Lib();
                ObjJRALib.Mode = 0;
                ObjJRALib.Work_Categ_Parent_ID = null;

                DataTable dt = BLL_JRA_Work_Category.JRA_GET_WORK_CATEGORY_LIST(ObjJRALib);
                string parentcatvalue = dt.Select("Work_Categ_ID=" + ddlParentCat.SelectedValue.ToString())[0]["Work_Categ_Value"].ToString();
                if (Math.Truncate(testfloat).ToString() != parentcatvalue)
                {
                    js = "alert('Category Code not valid');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", js, true);
                    Validate = false;
                }
                if (CatVal.Trim().Contains('.'))
                {
                    if (Convert.ToInt32(CatVal.Trim().Split('.')[1]) == 0)
                    {
                        js = "alert('Category Code not valid');";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", js, true);
                        Validate = false;
                    }
                }
                ObjJRALib = new JRA_Lib();
                ObjJRALib.Mode = 1;
                ObjJRALib.Work_Categ_Parent_ID = Convert.ToInt32(ddlParentCat.SelectedValue);
                DataTable dt1 = BLL_JRA_Work_Category.JRA_GET_WORK_CATEGORY_LIST(ObjJRALib);
                if (dt1.Select("Work_Categ_Value='" + CatVal.Trim() + "'").Length > 0)
                {
                    js = "alert('Category value already exists');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", js, true);
                    Validate = false;
                }
            }
        }
        catch(Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
        return Validate;
    }

    private void ClearWorkCategory()
    {
        ddlParentCat.SelectedIndex = -1;
        txtCatVal.Text = string.Empty;
        txtCatName.Text = string.Empty;
    }

    private void GroupGridviewHeader()
    {
        GridViewHelper helper = new GridViewHelper(GridView_Category);
        helper.RegisterGroup("Parent", true, true);
        helper.GroupHeader += new GroupEvent(helper_GroupHeader);
    }
    #endregion

    #region Gridview Events
    protected void GridView_Category_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int ID = UDFLib.ConvertToInteger(GridView_Category.DataKeys[e.Row.RowIndex].Value.ToString());

            if ((e.Row.RowState & DataControlRowState.Edit) > 0)
            {
                e.Row.Cells[1].Enabled = false;
                e.Row.Cells[0].Enabled = false;
                if (((HiddenField)e.Row.FindControl("hdnParent_Work_Categ_ID")).Value == string.Empty)
                {
                    ((TextBox)e.Row.FindControl("txtCategory_Name")).ReadOnly = true;
                    ((TextBox)e.Row.FindControl("txtWork_Categ_Value")).ReadOnly = true;

                }
                else
                {
                    ((TextBox)e.Row.FindControl("txtCategory_Name")).ReadOnly = false;
                    ((TextBox)e.Row.FindControl("txtWork_Categ_Value")).ReadOnly = false;
                }
            }
        }
    }
    protected void GridView_Category_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        JRA_Lib JRALib_Data = new JRA_Lib();
        int ID = UDFLib.ConvertToInteger(GridView_Category.DataKeys[e.RowIndex].Value.ToString());
        JRALib_Data.Work_Categ_ID = ID;
        JRALib_Data.UserID = Convert.ToInt32(Session["USERID"]);
        JRALib_Data.DB_Mode = "D";
        int result = BLL_JRA_Work_Category.JRA_INS_WorkCategory(JRALib_Data);
        //GroupGridviewHeader();
        Search_WorkCategory();
        Load_CategoryList(null, 0);
    }
    
    protected void GridView_Category_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        JRA_Lib JRALib_Data = new JRA_Lib();
        try
        {
            GridViewRow row = (GridViewRow)GridView_Category.Rows[e.RowIndex];
            int ID = UDFLib.ConvertToInteger(GridView_Category.DataKeys[e.RowIndex].Values[0].ToString());
            GridView_Category.EditIndex = -1;
            JRALib_Data.UserID = Convert.ToInt32(Session["USERID"]);
            JRALib_Data.Work_Categ_ID = Convert.ToInt32(ID);
            JRALib_Data.Work_Categ_Parent_ID = Convert.ToInt32(((HiddenField)row.FindControl("hdnParent_Work_Categ_ID")).Value);
            JRALib_Data.Work_Category_Name = (((TextBox)row.FindControl("txtCategory_Name")).Text.Trim());
            JRALib_Data.Work_Categ_Value = (((TextBox)row.FindControl("txtWork_Categ_Value")).Text.Trim());
            JRALib_Data.DB_Mode = "U";
          














                int result = BLL_JRA_Work_Category.JRA_INS_WorkCategory(JRALib_Data);
              
                Search_WorkCategory();
                Load_CategoryList(null, 0);
            
        }
        catch
        {
        }
        
    }
    protected void GridView_Category_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {

            GridView_Category.EditIndex = e.NewEditIndex;
            //GroupGridviewHeader();
             Search_WorkCategory();
            //Load_CategoryList(null, 0);
        }
        catch
        {
        }
    }
    protected void GridView_Category_RowCancelEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {
            GridView_Category.EditIndex = -1;
            //GroupGridviewHeader();
            Search_WorkCategory();
           // Load_CategoryList(null, 0);
        }
        catch
        {
        }
    }

    #endregion

    #region Control Events
    protected void txtfilter_TextChanged(object sender, EventArgs e)
    {
        //GroupGridviewHeader();
        Search_WorkCategory();
        Load_CategoryList(null, 0);
    }

    protected void btnsave_Click(object sender, EventArgs e)
    {
        if (ValidateCategory(txtCatVal.Text.Trim(),txtCatName.Text.Trim()) == true)
        {
            SaveWorkCategory("A");
            ClearWorkCategory();
            Load_CategoryList(null, 0);
            Search_WorkCategory();
            Load_CategoryList(null, 0);
        }
    }
    protected void btnSaveAndClose_Click(object sender, EventArgs e)
    {
        if (ValidateCategory(txtCatVal.Text.Trim(), txtCatName.Text.Trim()) == true)
        {
            SaveWorkCategory("A");
            ClearWorkCategory();
            string hidemodal = String.Format("hideModal('dvAddNewCategory')");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hidemodal", hidemodal, true);
            //GroupGridviewHeader();
            Search_WorkCategory();
            Load_CategoryList(null, 0);
        }
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        string hidemodal = String.Format("hideModal('dvAddNewCategory')");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hidemodal", hidemodal, true);
        //GroupGridviewHeader();
        Search_WorkCategory();
        Load_CategoryList(null, 0);
    }
   
    protected void btnClearFilter_Click(object sender, EventArgs e)
    {

    }
   


    protected void rdoGradeType_SelectedIndexChanged(object sender, EventArgs e)
    {
    }

    protected void ddlParentCat_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (ddlParentCat.SelectedValue != "--Select--")
        //{
        //    Load_CategoryList(Convert.ToInt32(ddlParentCat.SelectedValue), 1);
        //}
    }
    protected void ddlDivisions_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    private void helper_GroupHeader(string groupName, object[] values, GridViewRow row)
    {
        if (groupName == "Parent")
        {
            row.BackColor = System.Drawing.Color.GhostWhite;
            row.Cells[0].Font.Bold = true;
        }
    }
    protected void btnFilter_Click(object sender, ImageClickEventArgs e)
    {
        //GroupGridviewHeader();
        Search_WorkCategory();
        //Load_CategoryList(null, 0);
    }
    #endregion

}