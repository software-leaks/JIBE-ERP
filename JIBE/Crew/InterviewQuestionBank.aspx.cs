using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.Crew;
using SMS.Business.Infrastructure;
using SMS.Properties;

public partial class Crew_InterviewQuestionBank : System.Web.UI.Page
{
    
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            UserAccessValidation();
            Load_Gradings();
            Load_CategoryList(ddlCategory);
            Load_CategoryList(ddlCategoryFilter);
            Bind_QuestionBank();
            Load_JQuery_Thesaurus();
        }
    }
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }
    protected void UserAccessValidation()
    {
        UserAccess objUA = new UserAccess();

        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
            Response.Redirect("~/default.aspx");

        if (objUA.Add == 0)
        {
            lnkAddNewQuestion.Visible = false;
        }
        if (objUA.Edit == 0)
        {
            GridView_Criteria.Columns[GridView_Criteria.Columns.Count - 3].Visible = false;
        }
        if (objUA.Delete == 0)
        {
            GridView_Criteria.Columns[GridView_Criteria.Columns.Count - 2].Visible = false;
        }

        if (objUA.Approve == 0)
        {
        }

    }

    protected void Load_CategoryList(DropDownList ddlCategory)
    {
        DataTable dt = BLL_Crew_Interview.Get_CategoryList(txtfilter.Text);
        ddlCategory.DataSource = dt;
        ddlCategory.DataBind();
        ddlCategory.Items.Insert(0, new ListItem("-Select All-", "0"));


    }
    protected void ddlCategoryFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        Bind_QuestionBank();
    }
    
    protected void Bind_QuestionBank()
    {
        DataTable dt = BLL_Crew_Interview.Get_CriteriaList(txtfilter.Text, UDFLib.ConvertToInteger(ddlCategoryFilter.SelectedValue));
        
        DataView dataView = new DataView(dt);
        if (ViewState["sortExpression"] != null)
            dataView.Sort = ViewState["sortExpression"].ToString();

        GridView_Criteria.DataSource = dataView;
        GridView_Criteria.DataBind();

    }
    protected void txtfilter_TextChanged(object sender, EventArgs e)
    {
        Bind_QuestionBank();
        string js = "$('#dvInterviewSheet span').highlight([\"" + txtfilter.Text + "\"]);";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "highlighttext", js, true);
    }
    protected void GridView_Criteria_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int ID = UDFLib.ConvertToInteger(GridView_Criteria.DataKeys[e.RowIndex].Value.ToString());

        BLL_Crew_Interview.DELETE_Criteria(ID, GetSessionUserID());
        Bind_QuestionBank();
    }
    protected void GridView_Criteria_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int ID = UDFLib.ConvertToInteger(GridView_Criteria.DataKeys[e.RowIndex].Value.ToString());
        string Criteria = e.NewValues["Question"].ToString();
        string Answer = "";
        if (e.NewValues["Answer"] != null)
        {
             Answer = e.NewValues["Answer"].ToString();
        }
        
        

        int Cat_ID = 0;
        int Grading_Type = UDFLib.ConvertToInteger(e.NewValues["Grading_Type"].ToString());

        DropDownList ddlCategory = (DropDownList)GridView_Criteria.Rows[e.RowIndex].FindControl("ddlCategory");
        if (ddlCategory != null)
        {
            Cat_ID = UDFLib.ConvertToInteger(ddlCategory.SelectedValue);
        }

        BLL_Crew_Interview.UPDATE_Criteria(ID, Criteria,Answer, Cat_ID, Grading_Type, GetSessionUserID());

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
                if (rdo != null)
                {
                    DataTable dt = BLL_Crew_Interview.Get_GradingOptions(ID);
                    rdo.DataSource = dt;
                    rdo.DataBind();
                }
                DropDownList ddlCategory_Name = (DropDownList)e.Row.FindControl("ddlCategory");
                if (ddlCategory_Name != null)
                {
                    DataTable dtCat = BLL_Crew_Interview.Get_CategoryList("");
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


    protected void Load_Gradings()
    {
        DataTable dt = BLL_Crew_Interview.Get_GradingList();
        ddlGradingType.DataSource = dt;
        ddlGradingType.DataBind();
        ddlGradingType.Items.Insert(0, new ListItem("-Select-", "0"));
    }
    protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
    {


    }
    protected void ddlGradingType_SelectedIndexChanged(object sender, EventArgs e)
    {
        int Grade_ID = UDFLib.ConvertToInteger(ddlGradingType.SelectedValue);
        DataTable dt = BLL_Crew_Interview.Get_GradingOptions(Grade_ID);
        rdoGradings.DataSource = dt;
        rdoGradings.DataBind();
    }
    
    protected void btnsave_Click(object sender, EventArgs e)
    {
        int Ret = Create_Interview_Criteria();
        if (Ret > 0)
        {
            Bind_QuestionBank();
        }
    }
    protected void btnSaveAndClose_Click(object sender, EventArgs e)
    {

        int Ret = Create_Interview_Criteria();
        if (Ret >0)
        {
            Bind_QuestionBank();

            string js = "closeDivAddNewCriteria();";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "closeQ", js, true);
        }
    }
    protected int Create_Interview_Criteria()
    {
        int Ret = 1;
        string js = "";

        if (txtCriteria.Text == "")
        {
            js = "alert('Enter Question');";

        }
        else if (ddlCategory.SelectedValue == "0")
        {
            js = "alert('Select Category');";

        }
        else if (ddlType.SelectedValue == "0")
        {
            js = "alert('Select question type');";

        }
        else if (ddlGradingType.SelectedValue == "0")
        {
            js = "alert('Select grading type');";

        }
        if (js == "")
        {
            Ret = BLL_Crew_Interview.INSERT_Criteria(txtCriteria.Text.Trim(), txtAnswer.Text.Trim(), UDFLib.ConvertToInteger(ddlType.SelectedValue), UDFLib.ConvertToInteger(ddlCategory.SelectedValue), UDFLib.ConvertToInteger(ddlGradingType.SelectedValue), GetSessionUserID());
            if (Ret > 0)
            {
                txtCriteria.Text = "";
                txtAnswer.Text = "";

                js = "alert('Interview criteria added');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", js, true);
            }
        }
        else
        {
            Ret = 0;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", js, true);

        }
        return Ret;
    }

    protected void rdoGradeType_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (rdoGradeType.SelectedValue == "1")
        //{
        //    pnlSubjective.Visible = false;
        //}
        //else
        //{
        //    pnlSubjective.Visible = true;
        //}
    }
    //protected void ddlDivisions_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    decimal min = UDFLib.ConvertToDecimal(ddlMin.SelectedValue);
    //    decimal max = UDFLib.ConvertToDecimal(ddlMax.SelectedValue);
    //    int Division = UDFLib.ConvertToInteger(ddlDivisions.SelectedValue) - 1;

    //    //rdoOptions.Items.Clear();

    //    DataTable dt = new DataTable("Options");
    //    dt.Columns.Add("OptionText", typeof(string));
    //    dt.Columns.Add("OptionValue", typeof(string));

    //    if (min >= 0 && max > 0 && Division > 0)
    //    {
    //        decimal increment = (max - min) / Division;

    //        while (min <= max)
    //        {
    //            //rdoOptions.Items.Add(new ListItem(min.ToString(), min.ToString()));

    //            DataRow dr1 = dt.NewRow();
    //            dr1[0] = min.ToString();
    //            dr1[1] = min.ToString();
    //            dt.Rows.Add(dr1);

    //            min += increment;
    //        }

    //        rptOptions.DataSource = dt;
    //        rptOptions.DataBind();
    //    }
    //}

    //protected void btnSaveGrade_Click(object sender, EventArgs e)
    //{
    //    int ID = BLL_Crew_Interview.INSERT_Grading(txtGrade.Text.Trim(), UDFLib.ConvertToInteger(rdoGradeType.SelectedValue), UDFLib.ConvertToInteger(ddlMin.SelectedValue), UDFLib.ConvertToInteger(ddlMax.SelectedValue), UDFLib.ConvertToInteger(ddlDivisions.SelectedValue), GetSessionUserID());

    //    txtGrade.Text = "";
    //    for (int i = 0; i < rptOptions.Items.Count; i++)
    //    {
    //        TextBox txtValue = (TextBox)rptOptions.Items[i].FindControl("txtValue");
    //        TextBox txtText = (TextBox)rptOptions.Items[i].FindControl("txtText");

    //        if (txtText != null && txtValue != null)
    //        {
    //            BLL_Crew_Interview.INSERT_GradingOption(ID, txtText.Text.Trim(), UDFLib.ConvertToDecimal(txtValue.Text), GetSessionUserID());
    //        }
    //    }
    //    Load_GradingList();
    //    Load_Gradings();
    //}
    //protected void btnSaveAndCloseGrade_Click(object sender, EventArgs e)
    //{
    //    btnSaveGrade_Click(null, null);

    //    string js = "closeDivAddNewGrade();";
    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", js, true);

    //}


    protected void Load_JQuery_Thesaurus()
    {
        string Project = "CREW";
        string Module = "INTERVIEW";
        string js = "";
        int Count = 0;

        DataTable dt = BLL_Infra_Thesaurus.Thesaurus_Tags(Project, Module, GetSessionUserID());
        foreach (DataRow dr in dt.Rows)
        {
            js += "$('span:contains(\"" + dr["Title"].ToString() + "\")').Thesaurus({lists:\"" + dr["Title"].ToString() + "|Alll\"});";
            Count++;
        }
        if (Count > 0)
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Thesaurus12", js, true);
    }
}
