using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.Crew;


public partial class CrewEvaluation_Criteria : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            UserAccessValidation();
            Load_CategoryList();

            Bind_QuestionBank();

            Load_Gradings();
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
            lnkAddNewQuestion.Visible = false;
        }
        if (objUA.Edit == 0)
        {
            GridView_Criteria.Columns[GridView_Criteria.Columns.Count - 2].Visible = false;

        }
        if (objUA.Delete == 0)
        {
            GridView_Criteria.Columns[GridView_Criteria.Columns.Count - 1].Visible = false;

        }
        if (objUA.Approve == 0)
        {
        }

    }

    protected void Load_CategoryList()
    {
        DataTable dt = BLL_Crew_Evaluation.Get_CategoryList(txtfilter.Text);
        ddlCategory.DataSource = dt;
        ddlCategory.DataBind();
        ddlCategory.Items.Insert(0, new ListItem("-Select All-", "0"));


        ddlCatName.DataSource = dt;
        ddlCatName.DataBind();

    }

    protected void Load_Gradings()
    {
        DataTable dt = BLL_Crew_Evaluation.Get_GradingList();
        ddlGradingType.DataSource = dt;
        ddlGradingType.DataBind();
        ddlGradingType.Items.Insert(0, new ListItem("-Select-", "0"));
    }

    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        Bind_QuestionBank();
    }
    protected void txtfilter_TextChanged(object sender, EventArgs e)
    {
        Bind_QuestionBank();
    }

    protected void Bind_QuestionBank()
    {
        DataTable dt = BLL_Crew_Evaluation.Get_CriteriaList(txtfilter.Text, UDFLib.ConvertToInteger(ddlCategory.SelectedValue));


        DataView dataView = new DataView(dt);

        if (ViewState["sortExpression"] != null)
            dataView.Sort = ViewState["sortExpression"].ToString();

        GridView_Criteria.DataSource = dataView;
        GridView_Criteria.DataBind();

    }
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }

    protected void GridView_Criteria_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int ID = UDFLib.ConvertToInteger(GridView_Criteria.DataKeys[e.RowIndex].Value.ToString());

        BLL_Crew_Evaluation.DELETE_Criteria(ID, GetSessionUserID());
        Bind_QuestionBank();
    }
    protected void GridView_Criteria_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int ID = UDFLib.ConvertToInteger(GridView_Criteria.DataKeys[e.RowIndex].Value.ToString());
        string Criteria = e.NewValues["Criteria"].ToString();
        int Cat_ID = 0;
        int Grading_Type = UDFLib.ConvertToInteger(e.NewValues["Grading_Type"].ToString());

        DropDownList ddlCategory_Name = (DropDownList)GridView_Criteria.Rows[e.RowIndex].FindControl("ddlCategory_Name");
        if (ddlCategory_Name != null)
        {
            Cat_ID = UDFLib.ConvertToInteger(ddlCategory_Name.SelectedValue);
        }

        BLL_Crew_Evaluation.UPDATE_Criteria(ID, Criteria, Cat_ID, Grading_Type, GetSessionUserID());

        GridView_Criteria.EditIndex = -1;
        Bind_QuestionBank();
    }
    protected void GridView_Criteria_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            GridView_Criteria.EditIndex = e.NewEditIndex;
            Bind_QuestionBank();

        }
        catch
        {
        }
    }
    protected void GridView_Criteria_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int ID = UDFLib.ConvertToInteger(DataBinder.Eval(e.Row.DataItem, "Grading_Type").ToString());
                int Grade_Type = UDFLib.ConvertToInteger(DataBinder.Eval(e.Row.DataItem, "Grade_Type").ToString());

                RadioButtonList rdo = (RadioButtonList)e.Row.FindControl("rdoOptions");
                DataTable dt = BLL_Crew_Evaluation.Get_GradingOptions(ID);

                DropDownList ddlCategory_Name = (DropDownList)e.Row.FindControl("ddlCategory_Name");
                if (ddlCategory_Name != null)
                {
                    DataTable dtCat = BLL_Crew_Evaluation.Get_CategoryList("");
                    ddlCategory_Name.DataSource = dtCat;
                    ddlCategory_Name.DataBind();
                    ddlCategory_Name.SelectedValue = DataBinder.Eval(e.Row.DataItem, "Category_ID").ToString();
                }

                if (Grade_Type == 2)
                {
                    TextBox txtAnswer = new TextBox();
                    txtAnswer.Text = "Subjective Type";
                    txtAnswer.Enabled = false;
                    e.Row.Cells[3].Controls.Add(txtAnswer);
                }
                rdo.DataSource = dt;
                rdo.DataBind();
            }

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
            Bind_QuestionBank();

        }
        catch
        {
        }


    }


    protected void GridView_Criteria_Sorting(object sender, GridViewSortEventArgs e)
    {
        Session["pageindex"] = GridView_Criteria.PageIndex;

        if (ViewState["sortDirection"] == null)
        {
            ViewState["sortDirection"] = SortDirection.Ascending;
            ViewState["sortExpression"] = e.SortExpression;
        }
        else
        {
            if ((SortDirection)ViewState["sortDirection"] == SortDirection.Ascending)
            {
                ViewState["sortDirection"] = SortDirection.Descending;
                ViewState["sortExpression"] = e.SortExpression + " DESC";
            }
            else
            {
                ViewState["sortDirection"] = SortDirection.Ascending;
                ViewState["sortExpression"] = e.SortExpression;
            }

        }

        Bind_QuestionBank();
    }
    protected void GridView_Criteria_Sorted(object sender, EventArgs e)
    {
        GridView_Criteria.PageIndex = Convert.ToInt32(Session["pageindex"]);

    }

    protected void btnsave_Click(object sender, EventArgs e)
    {
        int ID = BLL_Crew_Evaluation.INSERT_Criteria(txtCriteria.Text.Trim(), UDFLib.ConvertToInteger(ddlCatName.SelectedValue), UDFLib.ConvertToInteger(ddlGradingType.SelectedValue), GetSessionUserID());
        txtCriteria.Text = "";
        txtfilter.Text = "";
        Bind_QuestionBank();
    }
    protected void btnSaveAndClose_Click(object sender, EventArgs e)
    {
        int ID = BLL_Crew_Evaluation.INSERT_Criteria(txtCriteria.Text.Trim(), UDFLib.ConvertToInteger(ddlCatName.SelectedValue), UDFLib.ConvertToInteger(ddlGradingType.SelectedValue), GetSessionUserID());
        txtCriteria.Text = "";

        txtfilter.Text = "";
        Bind_QuestionBank();

        string js = "closeDiv('dvAddNewCriteria');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", js, true);

    }
    protected void btnClearFilter_Click(object sender, EventArgs e)
    {

        txtfilter.Text = "";
        Bind_QuestionBank();

    }

    protected void ddlGradingType_SelectedIndexChanged(object sender, EventArgs e)
    {
        int Grade_ID = UDFLib.ConvertToInteger(ddlGradingType.SelectedValue);
        DataTable dt = BLL_Crew_Evaluation.Get_GradingOptions(Grade_ID);
        rdoGradings.DataSource = dt;
        rdoGradings.DataBind();
    }

}