using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.Operations;

public partial class Operations_RatingLibrary : System.Web.UI.Page
{
    BLL_OPS_Admin OPS_Admin = new BLL_OPS_Admin();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            UserAccessValidation();
            Load_CategoryList();
            Load_CriteriaList(0);
            Load_GradingList();

            DataTable dt = OPS_Admin.Get_CategoryList("");

            ddlCategory.DataSource = dt;
            ddlCategory.DataTextField = "Category_Name";
            ddlCategory.DataValueField = "ID";
            ddlCategory.DataBind();
            ddlCategory.Items.Insert(0, new ListItem("-Select-", "0"));

            ddlCategory1.DataSource = dt;
            ddlCategory1.DataTextField = "Category_Name";
            ddlCategory1.DataValueField = "ID";
            ddlCategory1.DataBind();
            ddlCategory1.Items.Insert(0, new ListItem("-Select-", "0"));

            DataTable dtRatingType = OPS_Admin.Get_GradingList();            

            ddlRatingType.DataSource = dtRatingType;
            ddlRatingType.DataTextField = "Rating_Name";
            ddlRatingType.DataValueField = "ID";
            ddlRatingType.DataBind();
            ddlRatingType.Items.Insert(0, new ListItem("-Select-", "0"));
        }
    }


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
            lnkAddNewGrade.Visible = false;
        }
        if (objUA.Edit == 0)
        {
            GridView_Category.Columns[GridView_Category.Columns.Count - 2].Visible = false;
            GridView_Grading.Columns[GridView_Grading.Columns.Count - 2].Visible = false;

        }
        if (objUA.Delete == 0)
        {
            GridView_Category.Columns[GridView_Category.Columns.Count - 1].Visible = false;
            GridView_Grading.Columns[GridView_Grading.Columns.Count - 1].Visible = false;
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
    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        Load_CriteriaList(int.Parse(ddlCategory.SelectedValue.ToString()));
    }
    protected void Load_CategoryList()
    {
        DataTable dt = OPS_Admin.Get_CategoryList(txtfilter.Text);
        GridView_Category.DataSource = dt;
        GridView_Category.DataBind();
    }
    protected void Load_CriteriaList(int Category_ID)
    {
        DataTable dt = OPS_Admin.Get_CriteriaList(Category_ID);
        GridView_Criteria.DataSource = dt;
        GridView_Criteria.DataBind();
    }
    protected void GridView_Category_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int ID = UDFLib.ConvertToInteger(GridView_Category.DataKeys[e.RowIndex].Value.ToString());

        OPS_Admin.DELETE_Category(ID, GetSessionUserID());
        Load_CategoryList();
        Load_CriteriaList(0);
    }
    protected void GridView_Category_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int ID = UDFLib.ConvertToInteger(GridView_Category.DataKeys[e.RowIndex].Value.ToString());
        string Category_Name = e.NewValues["Category_Name"].ToString();
        int Category_Order_By = UDFLib.ConvertToInteger(e.NewValues["Category_Order_By"].ToString());
        OPS_Admin.UPDATE_Category(ID, Category_Name,Category_Order_By,0, GetSessionUserID());
        GridView_Category.EditIndex = -1;
        Load_CategoryList();
        Load_CriteriaList(0);
    }
    protected void GridView_Category_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            GridView_Category.EditIndex = e.NewEditIndex;
            Load_CategoryList();

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
            Load_CategoryList();

        }
        catch
        {
        }


    }
    protected void GridView_Criteria_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int ID = UDFLib.ConvertToInteger(GridView_Category.DataKeys[e.RowIndex].Value.ToString());

        OPS_Admin.DELETE_Category(ID, GetSessionUserID());
        Load_CriteriaList(0);
    }
    protected void GridView_Criteria_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int ID = UDFLib.ConvertToInteger(GridView_Criteria.DataKeys[e.RowIndex].Value.ToString());
        string Criteria_Name = e.NewValues["Criteria_Name"].ToString();
        int Criteria_Order_By = UDFLib.ConvertToInteger(e.NewValues["Category_Order_By"].ToString());
        int GradeType_Id = int.Parse(e.NewValues["Category_Order_By"].ToString());
        OPS_Admin.UPDATE_Category(ID, Criteria_Name, Criteria_Order_By, GradeType_Id, GetSessionUserID());
        GridView_Category.EditIndex = -1;
        Load_CriteriaList(0);
    }
    protected void GridView_Criteria_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            GridView_Criteria.EditIndex = e.NewEditIndex;
            Load_CriteriaList(0);
        }
        catch
        {
        }
    }
    protected void GridView_Criteria_RowCancelEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {
            GridView_Criteria.EditIndex = -1;
            Load_CriteriaList(0);
        }
        catch
        {
        }
    }
    protected void txtfilter_TextChanged(object sender, EventArgs e)
    {
        Load_CategoryList();
    }

    protected void btnsave_Click(object sender, EventArgs e)
    {
        int ID = OPS_Admin.INSERT_Category(null,txtCatName.Text.Trim(), int.Parse(txtCategoryOrder.Text.Trim()),null, GetSessionUserID());
        txtCatName.Text = "";
        txtfilter.Text = "";
        Load_CategoryList();
    }
    protected void btnSaveAndClose_Click(object sender, EventArgs e)
    {
        int ID = OPS_Admin.INSERT_Category(null, txtCatName.Text.Trim(), int.Parse(txtCategoryOrder.Text.Trim()), null, GetSessionUserID());
        txtCatName.Text = "";
        txtfilter.Text = "";
        Load_CategoryList();

        string js = "closeDiv('dvAddNewCategory');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", js, true);
    }
    protected void btnClearFilter_Click(object sender, EventArgs e)
    {

        txtfilter.Text = "";
        Load_CategoryList();

    }


    protected void Load_GradingList()
    {
        BLL_OPS_Admin OPS_Admin = new BLL_OPS_Admin();
        DataTable dt = OPS_Admin.Get_GradingList();
        GridView_Grading.DataSource = dt;
        GridView_Grading.DataBind();
    }
    protected void GridView_Grading_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int ID = UDFLib.ConvertToInteger(GridView_Grading.DataKeys[e.RowIndex].Value.ToString());
        BLL_OPS_Admin OPS_Admin = new BLL_OPS_Admin();
        OPS_Admin.DELETE_Grading(ID, GetSessionUserID());
        Load_GradingList();
    }
    protected void GridView_Grading_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int ID = UDFLib.ConvertToInteger(GridView_Grading.DataKeys[e.RowIndex].Value.ToString());
        string Grade_Name = e.NewValues["Rating_Name"].ToString();
        int Grade_Type = UDFLib.ConvertToInteger(e.NewValues["Grade_Type"].ToString());
        int Min = UDFLib.ConvertToInteger(e.NewValues["Min"].ToString());
        int Max = UDFLib.ConvertToInteger(e.NewValues["Max"].ToString());
        if (Min > Max)
        {
            string js = "alert('Max Value should be smaller than min Value');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", js, true);
            return;
        }
        int Divisions = UDFLib.ConvertToInteger(e.NewValues["Divisions"].ToString());
        BLL_OPS_Admin OPS_Admin = new BLL_OPS_Admin();
        OPS_Admin.UPDATE_Grading(ID, Grade_Name, Grade_Type, Min, Max, Divisions, GetSessionUserID());
        GridView_Grading.EditIndex = -1;
        Load_GradingList();
    }
    protected void GridView_Grading_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            GridView_Grading.EditIndex = e.NewEditIndex;
            Load_GradingList();

        }
        catch
        {
        }
    }
    protected void GridView_Grading_RowCancelEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {
            GridView_Grading.EditIndex = -1;
            Load_GradingList();

        }
        catch
        {
        }


    }
    protected void GridView_Grading_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int ID = UDFLib.ConvertToInteger(GridView_Grading.DataKeys[e.Row.RowIndex].Value.ToString());

                RadioButtonList rdo = (RadioButtonList)e.Row.FindControl("rdoOptions");
                BLL_OPS_Admin OPS_Admin = new BLL_OPS_Admin();
                DataTable dt = OPS_Admin.Get_GradingOptions(ID);

                rdo.DataSource = dt;
                rdo.DataBind();
            }

        }
        catch
        {
        }


    }
    protected void ddlDivisions_SelectedIndexChanged(object sender, EventArgs e)
    {
        decimal min = UDFLib.ConvertToDecimal(ddlMin.SelectedValue);
        decimal max = UDFLib.ConvertToDecimal(ddlMax.SelectedValue);
        int Division = UDFLib.ConvertToInteger(ddlDivisions.SelectedValue) - 1;

        //rdoOptions.Items.Clear();

        DataTable dt = new DataTable("Options");
        dt.Columns.Add("OptionText", typeof(string));
        dt.Columns.Add("OptionValue", typeof(string));

        if (min >= 0 && max > 0 && Division > 0)
        {
            decimal increment = (max - min) / Division;

            while (min <= max)
            {
                //rdoOptions.Items.Add(new ListItem(min.ToString(), min.ToString()));

                DataRow dr1 = dt.NewRow();
                dr1[0] = min.ToString();
                dr1[1] = min.ToString();
                dt.Rows.Add(dr1);

                min += increment;
            }

            rptOptions.DataSource = dt;
            rptOptions.DataBind();
        }
    }

    protected void btnSaveGrade_Click(object sender, EventArgs e)
    {
        int ID = OPS_Admin.INSERT_Grading(txtGrade.Text.Trim(), UDFLib.ConvertToInteger(rdoGradeType.SelectedValue), UDFLib.ConvertToInteger(ddlMin.SelectedValue), UDFLib.ConvertToInteger(ddlMax.SelectedValue), UDFLib.ConvertToInteger(ddlDivisions.SelectedValue), GetSessionUserID());

        txtGrade.Text = "";
        for (int i = 0; i < rptOptions.Items.Count; i++)
        {
            TextBox txtValue = (TextBox)rptOptions.Items[i].FindControl("txtValue");
            TextBox txtText = (TextBox)rptOptions.Items[i].FindControl("txtText");

            if (txtText != null && txtValue != null)
            {
                OPS_Admin.INSERT_GradingOption(ID, txtText.Text.Trim(), UDFLib.ConvertToDecimal(txtValue.Text), GetSessionUserID());
            }
        }
        Load_GradingList();
    }
    protected void btnSaveAndCloseGrade_Click(object sender, EventArgs e)
    {
        btnSaveGrade_Click(null, null);

        string js = "closeDiv('dvAddNewGrade');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", js, true);

    }

    protected void rdoGradeType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdoGradeType.SelectedValue == "1")
        {
            pnlSubjective.Visible = false;
        }
        else
        {
            pnlSubjective.Visible = true;
        }
    }

    protected void btnsaveCriteria_Click(object sender, EventArgs e)
    {
        int Category_Id = int.Parse(ddlCategory1.SelectedValue.ToString());
        int GradeType_Id = int.Parse(ddlRatingType.SelectedValue.ToString());
        int ID = OPS_Admin.INSERT_Category(Category_Id, txtCriteriaName.Text.Trim(), int.Parse(txtCriteriaDisplayOrder.Text.Trim()), GradeType_Id, GetSessionUserID());
      //  int ID = OPS_Admin.INSERT_Criteria(Category_Id, txtCriteriaName.Text.Trim(), int.Parse(txtCriteriaDisplayOrder.Text.Trim()), GradeType_Id, GetSessionUserID());
        txtCriteriaName.Text = "";
        txtCriteriaDisplayOrder.Text = "";
        ddlRatingType.SelectedIndex = 0;
        Load_CriteriaList(0);
    }
    protected void btnSaveAndCloseCriteria_Click(object sender, EventArgs e)
    {
        int Category_Id = int.Parse(ddlCategory1.SelectedValue.ToString());
        int GradeType_Id = int.Parse(ddlRatingType.SelectedValue.ToString());
        int ID = OPS_Admin.INSERT_Category(Category_Id, txtCriteriaName.Text.Trim(), int.Parse(txtCriteriaDisplayOrder.Text.Trim()), GradeType_Id, GetSessionUserID());
      //  int ID = OPS_Admin.INSERT_Criteria(Category_Id, txtCriteriaName.Text.Trim(), int.Parse(txtCriteriaDisplayOrder.Text.Trim()),GradeType_Id, GetSessionUserID());
        txtCriteriaName.Text = "";
        txtCriteriaDisplayOrder.Text = "";
        ddlRatingType.SelectedIndex = 0;
        Load_CriteriaList(0);

        string js = "closeDiv('dvAddNewCriteria');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", js, true);

    }
}