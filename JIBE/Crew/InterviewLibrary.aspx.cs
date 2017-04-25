using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.Crew;


public partial class Crew_InterviewLibrary : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            UserAccessValidation();
            Load_CategoryList();
            //Load_TypeList();
            Load_GradingList();
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
            //lnkAddNewType.Visible = false;
            lnkAddNewGrade.Visible = false;
        }
        if (objUA.Edit == 0)
        {
            GridView_Category.Columns[GridView_Category.Columns.Count - 3].Visible = false;
            //GridView_EvaluationType.Columns[GridView_EvaluationType.Columns.Count - 2].Visible = false;
            GridView_Grading.Columns[GridView_Grading.Columns.Count - 3].Visible = false;
            
        }
        if (objUA.Delete == 0)
        {
            GridView_Category.Columns[GridView_Category.Columns.Count - 2].Visible = false;
            //GridView_EvaluationType.Columns[GridView_EvaluationType.Columns.Count - 1].Visible = false;
            GridView_Grading.Columns[GridView_Grading.Columns.Count - 2].Visible = false;
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

    protected void Load_CategoryList()
    {
        DataTable dt = BLL_Crew_Interview.Get_CategoryList(txtfilter.Text);
        GridView_Category.DataSource = dt;
        GridView_Category.DataBind();
    }
    protected void GridView_Category_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int ID = UDFLib.ConvertToInteger(GridView_Category.DataKeys[e.RowIndex].Value.ToString());

        BLL_Crew_Interview.DELETE_Category(ID, GetSessionUserID());
        Load_CategoryList();
    }
    protected void GridView_Category_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int ID = UDFLib.ConvertToInteger(GridView_Category.DataKeys[e.RowIndex].Value.ToString());
        string Category_Name = e.NewValues["Category_Name"].ToString();

        BLL_Crew_Interview.UPDATE_Category(ID, Category_Name, GetSessionUserID());
        GridView_Category.EditIndex = -1;
        Load_CategoryList();
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
    protected void txtfilter_TextChanged(object sender, EventArgs e)
    {
        Load_CategoryList();
    }

    protected void btnsave_Click(object sender, EventArgs e)
    {
        int ID = BLL_Crew_Interview.INSERT_Category(txtCatName.Text.Trim(), GetSessionUserID());
        txtCatName.Text = "";
        txtfilter.Text = "";
        Load_CategoryList();
    }
    protected void btnSaveAndClose_Click(object sender, EventArgs e)
    {
        int ID = BLL_Crew_Interview.INSERT_Category(txtCatName.Text.Trim(), GetSessionUserID());
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
        DataTable dt = BLL_Crew_Interview.Get_GradingList();
        GridView_Grading.DataSource = dt;
        GridView_Grading.DataBind();
    }
    protected void GridView_Grading_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int ID = UDFLib.ConvertToInteger(GridView_Grading.DataKeys[e.RowIndex].Value.ToString());

        BLL_Crew_Interview.DELETE_Grading(ID, GetSessionUserID());
        Load_GradingList();
    }
    protected void GridView_Grading_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int ID = UDFLib.ConvertToInteger(GridView_Grading.DataKeys[e.RowIndex].Value.ToString());
        string Grade_Name = e.NewValues["Grade_Name"].ToString();
        int Grade_Type = UDFLib.ConvertToInteger(e.NewValues["Grade_Type"].ToString());
        int Min = UDFLib.ConvertToInteger(e.NewValues["Min"].ToString());
        int Max = UDFLib.ConvertToInteger(e.NewValues["Max"].ToString());
        int Divisions = UDFLib.ConvertToInteger(e.NewValues["Divisions"].ToString());

        BLL_Crew_Interview.UPDATE_Grading(ID, Grade_Name, Grade_Type, Min, Max, Divisions, GetSessionUserID());
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
                DataTable dt = BLL_Crew_Interview.Get_GradingOptions(ID);

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
        int ID = BLL_Crew_Interview.INSERT_Grading(txtGrade.Text.Trim(), UDFLib.ConvertToInteger(rdoGradeType.SelectedValue), UDFLib.ConvertToInteger(ddlMin.SelectedValue), UDFLib.ConvertToInteger(ddlMax.SelectedValue), UDFLib.ConvertToInteger(ddlDivisions.SelectedValue), GetSessionUserID());

        txtGrade.Text = "";
        for (int i = 0; i < rptOptions.Items.Count; i++)
        {
            TextBox txtValue = (TextBox)rptOptions.Items[i].FindControl("txtValue");
            TextBox txtText = (TextBox)rptOptions.Items[i].FindControl("txtText");

            if (txtText != null && txtValue != null)
            {
                BLL_Crew_Interview.INSERT_GradingOption(ID, txtText.Text.Trim(), UDFLib.ConvertToDecimal(txtValue.Text), GetSessionUserID());
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


    //protected void Load_TypeList()
    //{
    //    DataTable dt = BLL_Crew_Interview.Get_EvaluationTypeList();
    //    GridView_EvaluationType.DataSource = dt;
    //    GridView_EvaluationType.DataBind();
    //}
    //protected void GridView_EvaluationType_RowDeleting(object sender, GridViewDeleteEventArgs e)
    //{
    //    int ID = UDFLib.ConvertToInteger(GridView_EvaluationType.DataKeys[e.RowIndex].Value.ToString());

    //    BLL_Crew_Interview.DELETE_EvaluationType(ID, GetSessionUserID());
    //    Load_TypeList();
    //}
    //protected void GridView_EvaluationType_RowUpdating(object sender, GridViewUpdateEventArgs e)
    //{
    //    int ID = UDFLib.ConvertToInteger(GridView_EvaluationType.DataKeys[e.RowIndex].Value.ToString());
    //    string Evaluation_Type = e.NewValues["Evaluation_Type"].ToString();

    //    BLL_Crew_Interview.UPDATE_EvaluationType(ID, Evaluation_Type, GetSessionUserID());
    //    GridView_EvaluationType.EditIndex = -1;
    //    Load_TypeList();
    //}
    //protected void GridView_EvaluationType_RowEditing(object sender, GridViewEditEventArgs e)
    //{
    //    try
    //    {
    //        GridView_EvaluationType.EditIndex = e.NewEditIndex;
    //        Load_TypeList();

    //    }
    //    catch
    //    {
    //    }
    //}
    //protected void GridView_EvaluationType_RowCancelEdit(object sender, GridViewCancelEditEventArgs e)
    //{
    //    try
    //    {
    //        GridView_EvaluationType.EditIndex = -1;
    //        Load_TypeList();

    //    }
    //    catch
    //    {
    //    }


    //}

    //protected void btnSaveEvaType_Click(object sender, EventArgs e)
    //{
    //    int ID = BLL_Crew_Interview.INSERT_EvaluationType(txtEvaluationType.Text.Trim(), GetSessionUserID());
    //    txtEvaluationType.Text = "";

    //    Load_TypeList();
    //}
    //protected void btnSaveAndCloseEvaType_Click(object sender, EventArgs e)
    //{

    //    btnSaveEvaType_Click(null, null);
    //    string js = "closeDiv('dvAddNewType');";
    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", js, true);

    //}

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
}