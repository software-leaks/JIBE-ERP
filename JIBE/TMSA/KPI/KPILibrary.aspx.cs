using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.Crew;
using SMS.Business.Infrastructure;
using SMS.Business.TMSA;


public partial class KPILibrary : System.Web.UI.Page
{
    BLL_TMSA_KPI objKPI = new BLL_TMSA_KPI();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            UserAccessValidation();
            Load_CategoryList();
            Load_Units();
            Load_Intervals();
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
            lnkAddNewType.Visible = false;
            lnkAddNewGrade.Visible = false;
        }
        if (objUA.Edit == 0)
        {
            //GridView_Category.Columns[GridView_Category.Columns.Count - 2].Visible = false;
            //GridView_EvaluationType.Columns[GridView_EvaluationType.Columns.Count - 2].Visible = false;
            //GridView_Grading.Columns[GridView_Grading.Columns.Count - 2].Visible = false;

        }
        if (objUA.Delete == 0)
        {
            //GridView_Category.Columns[GridView_Category.Columns.Count - 1].Visible = false;
            //GridView_EvaluationType.Columns[GridView_EvaluationType.Columns.Count - 1].Visible = false;
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

    protected void Load_CategoryList()
    {

        DataTable dt = objKPI.Get_CategoryList(txtfilter.Text);
        GridView_Category.DataSource = dt;
        GridView_Category.DataBind();
    }

    protected void Load_Units()
    {

        DataTable dt = objKPI.Get_Units(txtUnitSearch.Text);
        GridViewUnits.DataSource = dt;
        GridViewUnits.DataBind();
    }

    protected void Load_Intervals()
    {

        DataTable dt = objKPI.Get_Intervals(txtIntervalSearch.Text);
        GridViewIntervals.DataSource = dt;
        GridViewIntervals.DataBind();
    }

    protected void GridView_Category_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int ID = UDFLib.ConvertToInteger(GridView_Category.DataKeys[e.RowIndex].Value.ToString());

        objKPI.DELETE_Category(ID, GetSessionUserID());
        Load_CategoryList();
    }
    protected void GridView_Category_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int ID = UDFLib.ConvertToInteger(GridView_Category.DataKeys[e.RowIndex].Value.ToString());
        string Category_Name = e.NewValues["Category_Name"].ToString();

        objKPI.UPDATE_Category(ID, Category_Name, GetSessionUserID());
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
        int ID = objKPI.INSERT_Category_DL(txtCatName.Text.Trim(), GetSessionUserID());
        
        txtCatName.Text = "";
        txtfilter.Text = "";
        Load_CategoryList();


    }


    protected void btnSaveAndClose_Click(object sender, EventArgs e)
    {

        int ID = objKPI.INSERT_Category_DL(txtCatName.Text.Trim(), GetSessionUserID());
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
    protected void txtUnitSearch_TextChanged(object sender, EventArgs e)
    {
        Load_Units();
    }



    protected void txtInterval_TextChanged(object sender, EventArgs e)
    {
        Load_Intervals();
    }
    protected void btnsaveUnits_Click(object sender, EventArgs e)
    {
        int ID = objKPI.INSERT_Unit_DL(txtUnits.Text.Trim(), GetSessionUserID());
    
        txtUnits.Text = "";
        txtUnitSearch.Text = "";
        Load_Units();


    }
    protected void btnSaveAndCloseUnits_Click(object sender, EventArgs e)
    {

        int ID = objKPI.INSERT_Unit_DL(txtUnits.Text.Trim(), GetSessionUserID());
        txtUnits.Text = "";
        txtUnitSearch.Text = "";
        Load_Units();

        string js = "closeDiv('dvAddNewType');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", js, true);



    }







    protected void GridViewUnits_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int ID = UDFLib.ConvertToInteger(GridViewUnits.DataKeys[e.RowIndex].Value.ToString());
        string Unit_Name = e.NewValues["Unit_Name"].ToString();

        objKPI.UPDATE_Unit(ID, Unit_Name, GetSessionUserID());
        GridViewUnits.EditIndex = -1;
        Load_Units();
    }
    protected void GridViewUnits_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int ID = UDFLib.ConvertToInteger(GridViewUnits.DataKeys[e.RowIndex].Value.ToString());

        objKPI.DELETE_Unit(ID, GetSessionUserID());
        Load_Units();
    }
    protected void GridViewUnits_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            GridViewUnits.EditIndex = e.NewEditIndex;
            Load_Units();

        }
        catch
        {
        }
    }
    protected void GridViewUnits_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {
            GridViewUnits.EditIndex = -1;
            Load_Units();

        }
        catch
        {
        }
    }



    protected void btnsaveIntervals_Click(object sender, EventArgs e)
    {
        int ID = objKPI.INSERT_Interval_DL(txtIntervals.Text.Trim(), GetSessionUserID());
        // int ID = objKPI.INSERT_Category(txtCatName.Text.Trim(), GetSessionUserID());
        txtIntervals.Text = "";
        txtIntervalSearch.Text = "";
        Load_Intervals();


    }
    protected void btnSaveAndCloseIntervals_Click(object sender, EventArgs e)
    {

        int ID = objKPI.INSERT_Interval_DL(txtIntervals.Text.Trim(), GetSessionUserID());
        txtIntervals.Text = "";
        txtIntervalSearch.Text = "";
        Load_Intervals();

        string js = "closeDiv('dvAddNewGrade');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", js, true);


        
    }


    protected void btnunitSearch_click(object sender, EventArgs e)
    {
        Load_Intervals();
    }




    protected void GridViewIntervals_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int ID = UDFLib.ConvertToInteger(GridViewIntervals.DataKeys[e.RowIndex].Value.ToString());
        string Interval_Name = e.NewValues["Interval_Name"].ToString();

        objKPI.UPDATE_Interval(ID, Interval_Name, GetSessionUserID());
        GridViewIntervals.EditIndex = -1;
        Load_Intervals();
    }
    protected void GridViewIntervals_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int ID = UDFLib.ConvertToInteger(GridViewIntervals.DataKeys[e.RowIndex].Value.ToString());

        objKPI.DELETE_Interval(ID, GetSessionUserID());
        Load_Intervals();
    }
    protected void GridViewIntervals_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            GridViewIntervals.EditIndex = e.NewEditIndex;
            Load_Intervals();

        }
        catch
        {
        }
    }
    protected void GridViewIntervals_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {
            GridViewIntervals.EditIndex = -1;
            Load_Intervals();

        }
        catch
        {
        }
    }
}