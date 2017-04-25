using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.Crew;
using SMS.Business.Inspection;

public partial class CrewEvaluation_Criteria : System.Web.UI.Page
{
    BLL_INSP_Checklist objBLL = new BLL_INSP_Checklist();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["SORTDIRECTION"] = null;
            ViewState["SORTBYCOLOUMN"] = null;

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
        ///DataTable dt = objBLL.Get_Search_CheckListType(txtfilter.Text);
        DataTable dt = objBLL.Get_Search_CheckListCategory(txtfilter.Text);

        ddlCategory.DataSource = dt;
        ddlCategory.DataBind();

        ddlCatName.DataSource = dt;
        ddlCatName.DataBind();


    }

    protected void Load_Gradings()
    {
        DataTable dt = objBLL.Get_Grades(UDFLib.ConvertIntegerToNull(""));
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
        int rowcount = ucCustomPagerItems.isCountRecord;

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

        DataTable dt = objBLL.Get_QuestionList(txtfilter.Text != "" ? txtfilter.Text : null, UDFLib.ConvertToInteger(ddlCategory.SelectedValue), sortbycoloumn, sortdirection
            , ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);


        DataView dataView = new DataView(dt);

        if (ViewState["sortExpression"] != null)
            dataView.Sort = ViewState["sortExpression"].ToString();

        GridView_Criteria.DataSource = dataView;
        GridView_Criteria.DataBind();

        ucCustomPagerItems.CountTotalRec = rowcount.ToString();
        ucCustomPagerItems.BuildPager();

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

        objBLL.DELETE_Question(ID, GetSessionUserID());
        Bind_QuestionBank();
    }
    protected void GridView_Criteria_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            if (UpdateQuestions(e))
            {

                GridView_Criteria.EditIndex = -1;
                Bind_QuestionBank();
            }
        }
        catch (Exception ex)
        {
            string jsSqlError2 = "alert('" + ex.Message + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "jsSqlError2", jsSqlError2, true);
        }
    }

    /// <summary>
    /// Added by Anjali DT : 17-May-2016 JIT:9635 
    /// </summary>
    /// <param name="e"></param>
    /// <returns></returns>
    private bool UpdateQuestions(GridViewUpdateEventArgs e)
    {
        try
        {
            DropDownList ddlCate = (DropDownList)GridView_Criteria.Rows[e.RowIndex].FindControl("ddlCategory_Name");
            DropDownList ddlGrade = (DropDownList)GridView_Criteria.Rows[e.RowIndex].FindControl("ddlGradingType");
            TextBox txtQestion = (TextBox)GridView_Criteria.Rows[e.RowIndex].FindControl("txtCriteria");

            if ((txtQestion.Text.Trim() == "") || (ddlCate.SelectedIndex <= 0) || (ddlGrade.SelectedIndex < 0))
            {
                string js = "alert('All fields are mandatory.');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", js, true);
                return false;

            }

            else
            {
                int ID = UDFLib.ConvertToInteger(GridView_Criteria.DataKeys[e.RowIndex].Value.ToString());
                string Criteria = e.NewValues["Description"].ToString();
                int Cat_ID = 0;
                int Grading_Type = UDFLib.ConvertToInteger(e.NewValues["Grading_Type"].ToString());

                DropDownList ddlCategory_Name = (DropDownList)GridView_Criteria.Rows[e.RowIndex].FindControl("ddlCategory_Name");
                if (ddlCategory_Name != null)
                {
                    Cat_ID = UDFLib.ConvertToInteger(ddlCategory_Name.SelectedValue);
                }

                objBLL.UPDATE_Question(ID, Criteria, Cat_ID, Grading_Type, GetSessionUserID());
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

        return true;
    }
    protected void GridView_Criteria_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            GridView_Criteria.EditIndex = e.NewEditIndex;

            Bind_QuestionBank();

        }
        catch (Exception ex)
        {
            string jsSqlError2 = "alert('" + ex.Message + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "jsSqlError2", jsSqlError2, true);
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
                DataTable dt = objBLL.Get_GradingOptions(ID);

                if (Grade_Type == 2)
                {
                    TextBox txtAnswer = new TextBox();
                    txtAnswer.Text = "Subjective Type";
                    txtAnswer.Enabled = false;
                    e.Row.Cells[3].Controls.Add(txtAnswer);
                }
                if ((e.Row.RowState & DataControlRowState.Edit) > 0)
                {
                }
                else
                {
                    rdo.DataSource = dt;
                    rdo.DataBind();
                }


            }

        }
        catch (Exception ex)
        {
            string jsSqlError2 = "alert('" + ex.Message + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "jsSqlError2", jsSqlError2, true);
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

    private bool Validatequestions()
    {
        try
        {
            if (txtCriteria.Text.Trim() == "")
            {
                string js = "alert('All fields are mandatory.');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", js, true);
                return false; ;
            }
            if (UDFLib.ConvertToInteger(ddlCatName.SelectedValue) == 0)
            {
                string js = "alert('All fields are mandatory.');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", js, true);
                return false;
            }
            if (UDFLib.ConvertToInteger(ddlGradingType.SelectedValue) == 0)
            {
                string js = "alert('All fields are mandatory.');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", js, true);
                return false;
            }
        }
        catch (Exception ex)
        {
            string jsSqlError2 = "alert('" + ex.Message + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "jsSqlError2", jsSqlError2, true);
        }
        return true;

    }

    private void SaveQuestions()
    {
        try
        {
            int ID = objBLL.INSERT_Question(txtCriteria.Text.Trim(), UDFLib.ConvertToInteger(ddlCatName.SelectedValue), UDFLib.ConvertToInteger(ddlGradingType.SelectedValue), GetSessionUserID());

            if (ID > 0)
            {
                txtCriteria.Text = "";
                txtfilter.Text = "";
                ddlCatName.SelectedIndex = 0;
                ddlGradingType.SelectedIndex = 0;
                trGradings.Visible = false;

                string jsPrompt2 = "alert(Question saved successfully.')";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "jsPrompt2", jsPrompt2, true);
                Bind_QuestionBank();
            }
        }
        catch (Exception ex)
        {
            string jsSqlError2 = "alert('" + ex.Message + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "jsSqlError2", jsSqlError2, true);
        }

    }
    protected void btnsave_Click(object sender, EventArgs e)
    {

        if (Validatequestions())
        {
            SaveQuestions();
        }

    }
    protected void btnSaveAndClose_Click(object sender, EventArgs e)
    {
        if (Validatequestions())
        {
            SaveQuestions();
            string js = "closeDiv('dvAddNewCriteria');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", js, true);
        }

    }
    protected void btnClearFilter_Click(object sender, EventArgs e)
    {

        txtfilter.Text = "";
        Bind_QuestionBank();

    }

    protected void ddlGradingType_SelectedIndexChanged(object sender, EventArgs e)
    {
        int Grade_ID = UDFLib.ConvertToInteger(ddlGradingType.SelectedValue);

        DataTable dt = objBLL.Get_GradingOptions(Grade_ID);
        rdoGradings.DataSource = dt;
        rdoGradings.DataBind();

        trGradings.Visible = true;
    }


    protected void lnkAddNewQuestion_Click(object sender, EventArgs e)
    {
        txtCriteria.Text = "";
        txtfilter.Text = "";
        ddlCatName.SelectedIndex = 0;
        ddlGradingType.SelectedIndex = 0;
        trGradings.Visible = false;
        Bind_QuestionBank();
    }
}