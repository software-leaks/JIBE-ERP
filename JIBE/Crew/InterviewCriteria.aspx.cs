using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.Crew;


public partial class Crew_InterviewCriteria : System.Web.UI.Page
{
   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Load_Gradings();
            Load_CategoryList(ddlCategory);
            Load_CategoryList(ddlCategoryFilter);
            Bind_AssignedCriteria();
            Bind_UnAssignedCriteria();
            UserAccessValidation();
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
        SMS.Properties.UserAccess objUA = new SMS.Properties.UserAccess();

        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        SMS.Business.Infrastructure.BLL_Infra_UserCredentials objUser = new SMS.Business.Infrastructure.BLL_Infra_UserCredentials();
        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
            Response.Redirect("~/default.aspx?msgid=1");

        if (objUA.Add == 0)
        {
            //lnkAddNewCategory.Visible = false;
            //lnkAddNewType.Visible = false;
            //lnkAddNewGrade.Visible = false;
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


    protected void txtfilter_TextChanged(object sender, EventArgs e)
    {
        Bind_UnAssignedCriteria();
    }
    protected void ddlCategoryFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        Bind_UnAssignedCriteria();
    }

    protected void Bind_AssignedCriteria()
    {
        try
        {
            int IQID = UDFLib.ConvertToInteger(Request.QueryString["IQID"].ToString());
            int RankID = UDFLib.ConvertToInteger(Request.QueryString["RankID"].ToString());

            DataTable dt = BLL_Crew_Interview.Get_InterviewQuestions(IQID, RankID);

            GridView_AssignedCriteria.DataSource = dt;
            GridView_AssignedCriteria.DataBind();
        }
        catch
        {

        }

    }
    protected void Bind_UnAssignedCriteria()
    {
        try
        {
            int IQID = UDFLib.ConvertToInteger(Request.QueryString["IQID"].ToString());
            int RankID = UDFLib.ConvertToInteger(Request.QueryString["RankID"].ToString());
            string SearchText = txtfilter.Text;
            int CategoryID = UDFLib.ConvertToInteger(ddlCategoryFilter.SelectedValue);


            DataTable dt = BLL_Crew_Interview.Get_InterviewQuestions_UnAssigned(IQID, RankID,CategoryID,SearchText);

            DataView dataView = new DataView(dt);

            if (ViewState["sortExpression"] != null)
                dataView.Sort = ViewState["sortExpression"].ToString();

            GridView_UnAssignedCriteria.DataSource = dataView;
            GridView_UnAssignedCriteria.DataBind();

        }
        catch { }

    }
    protected void Load_CategoryList(DropDownList ddlCategory)
    {
        DataTable dt = BLL_Crew_Interview.Get_CategoryList("");
        ddlCategory.DataSource = dt;
        ddlCategory.DataBind();
        ddlCategory.Items.Insert(0, new ListItem("-Select All-", "0"));


    }

    protected void GridView_AssignedCriteria_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int iQuestionID = UDFLib.ConvertToInteger(DataBinder.Eval(e.Row.DataItem, "QID").ToString());
                int Grading_Type = UDFLib.ConvertToInteger(DataBinder.Eval(e.Row.DataItem, "Grading_Type").ToString());

                RadioButtonList rdo = (RadioButtonList)e.Row.FindControl("rdoOptions");
                TextBox txtAnswer = (TextBox)e.Row.FindControl("txtAnswer");
                TextBox txtRemarks = (TextBox)e.Row.FindControl("txtRemarks");
                if (rdo != null)
                {
                    DataTable dt = BLL_Crew_Interview.Get_GradingOptions(Grading_Type);
                    rdo.DataSource = dt;
                    rdo.DataBind();
                }

            }
        }
        catch
        {
        }
    }
    protected void GridView_AssignedCriteria_Sorting(object sender, GridViewSortEventArgs e)
    {
        Session["pageindex"] = GridView_AssignedCriteria.PageIndex;

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

        Bind_AssignedCriteria();
    }
    protected void GridView_AssignedCriteria_Sorted(object sender, EventArgs e)
    {
        GridView_AssignedCriteria.PageIndex = Convert.ToInt32(Session["pageindex"]);

    }

    protected void GridView_AssignedCriteria_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int SelectedRowIndex =0;
        int IQID = 0;
        int QID = 0;
        int RankID = 0;

        if (e.CommandName == "MoveUp")
        {
            GridViewRow r = (GridViewRow)((ImageButton)e.CommandSource).Parent.Parent;
            SelectedRowIndex = r.RowIndex;
            IQID = UDFLib.ConvertToInteger(Request.QueryString["IQID"].ToString());
            QID = UDFLib.ConvertToInteger(GridView_AssignedCriteria.DataKeys[SelectedRowIndex].Values[0].ToString());
            RankID = UDFLib.ConvertToInteger(GridView_AssignedCriteria.DataKeys[SelectedRowIndex].Values[1].ToString());

            BLL_Crew_Interview.Swap_InterviewQuestion_Sort_Order(IQID, RankID, QID, 1, GetSessionUserID());
            if (SelectedRowIndex >= 1)
                SelectedRowIndex -= 1;
            Bind_AssignedCriteria();
            GridView_AssignedCriteria.SelectedIndex = SelectedRowIndex;
        }
        else if (e.CommandName == "MoveDown")
        {
            GridViewRow r = (GridViewRow)((ImageButton)e.CommandSource).Parent.Parent;
            SelectedRowIndex = r.RowIndex;
            IQID = UDFLib.ConvertToInteger(Request.QueryString["IQID"].ToString());
            QID = UDFLib.ConvertToInteger(GridView_AssignedCriteria.DataKeys[SelectedRowIndex].Values[0].ToString());
            RankID = UDFLib.ConvertToInteger(GridView_AssignedCriteria.DataKeys[SelectedRowIndex].Values[1].ToString());

            BLL_Crew_Interview.Swap_InterviewQuestion_Sort_Order(IQID, RankID, QID, -1, GetSessionUserID());
            if (SelectedRowIndex < GridView_AssignedCriteria.Rows.Count - 1)
                SelectedRowIndex += 1;
            Bind_AssignedCriteria();
            GridView_AssignedCriteria.SelectedIndex = SelectedRowIndex;
        }
        else if (e.CommandName == "RemoveCriteria")
        {
            GridViewRow r = (GridViewRow)((ImageButton)e.CommandSource).Parent.Parent;
            SelectedRowIndex = r.RowIndex;
            IQID = UDFLib.ConvertToInteger(Request.QueryString["IQID"].ToString());
            QID = UDFLib.ConvertToInteger(GridView_AssignedCriteria.DataKeys[SelectedRowIndex].Values[0].ToString());
            RankID = UDFLib.ConvertToInteger(GridView_AssignedCriteria.DataKeys[SelectedRowIndex].Values[1].ToString());

            BLL_Crew_Interview.Remove_QuestionFromInterview(QID, IQID, RankID, GetSessionUserID());
            Bind_UnAssignedCriteria();
            Bind_AssignedCriteria();
            GridView_AssignedCriteria.SelectedIndex = SelectedRowIndex;
        }
    }

    protected void GridView_UnAssignedCriteria_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int iQuestionID = UDFLib.ConvertToInteger(DataBinder.Eval(e.Row.DataItem, "ID").ToString());
                int Grading_Type = UDFLib.ConvertToInteger(DataBinder.Eval(e.Row.DataItem, "Grading_Type").ToString());

                RadioButtonList rdo = (RadioButtonList)e.Row.FindControl("rdoOptions");
                TextBox txtAnswer = (TextBox)e.Row.FindControl("txtAnswer");
                TextBox txtRemarks = (TextBox)e.Row.FindControl("txtRemarks");

                if (rdo != null)
                {
                    DataTable dt = BLL_Crew_Interview.Get_GradingOptions(Grading_Type);
                    rdo.DataSource = dt;
                    rdo.DataBind();
                }

            }
        }
        catch
        {
        }
    }
    protected void GridView_UnAssignedCriteria_Sorting(object sender, GridViewSortEventArgs e)
    {
        Session["pageindex"] = GridView_UnAssignedCriteria.PageIndex;

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

        Bind_AssignedCriteria();
    }
    protected void GridView_UnAssignedCriteria_Sorted(object sender, EventArgs e)
    {
        GridView_AssignedCriteria.PageIndex = Convert.ToInt32(Session["pageindex"]);

    }

    protected void btnClearFilter_Click(object sender, EventArgs e)
    {

        txtfilter.Text = "";
        Bind_UnAssignedCriteria();

    }

    protected void btnsave_Click(object sender, EventArgs e)
    {
        int Ret = Create_Interview_Criteria();
        if (Ret > 0)
        {
            Bind_UnAssignedCriteria();
        }
    }
    protected void btnSaveAndClose_Click(object sender, EventArgs e)
    {

        int Ret = Create_Interview_Criteria();
        if (Ret > 0)
        {
            Bind_UnAssignedCriteria();

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

    protected void lnkAddToInterview_Click(object sender, EventArgs e)
    {
        int IQID = UDFLib.ConvertToInteger(Request.QueryString["IQID"].ToString());
        int RankID = UDFLib.ConvertToInteger(Request.QueryString["RankID"].ToString());

        foreach (GridViewRow row in GridView_UnAssignedCriteria.Rows)
        {
            CheckBox chkSelect = (CheckBox)row.FindControl("chkSelect");
            int QID = UDFLib.ConvertToInteger(GridView_UnAssignedCriteria.DataKeys[row.RowIndex].Value.ToString());

            if (chkSelect != null)
            {
                if (chkSelect.Checked == true)
                {
                    BLL_Crew_Interview.Add_QuestionToInterview(QID, RankID, IQID, GetSessionUserID());
                }
            }

        }
        Bind_AssignedCriteria();
        Bind_UnAssignedCriteria();
    }
    protected void lnkGoBackToInterview_Click(object sender, EventArgs e)
    {
        Response.Redirect("InterviewAdmin.aspx");

    }

    protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
    {


    }

    protected void Load_Gradings()
    {
        DataTable dt = BLL_Crew_Interview.Get_GradingList();
        ddlGradingType.DataSource = dt;
        ddlGradingType.DataBind();
        ddlGradingType.Items.Insert(0, new ListItem("-Select-", "0"));
    }
    protected void ddlGradingType_SelectedIndexChanged(object sender, EventArgs e)
    {
        int Grade_ID = UDFLib.ConvertToInteger(ddlGradingType.SelectedValue);
        DataTable dt = BLL_Crew_Interview.Get_GradingOptions(Grade_ID);
        rdoGradings.DataSource = dt;
        rdoGradings.DataBind();
    }

    
    //protected void Load_GradingList()
    //{
    //    DataTable dt = BLL_Crew_Interview.Get_GradingList();
    //    //GridView_Grading.DataSource = dt;
    //    //GridView_Grading.DataBind();
    //}
    //protected void rdoGradeType_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (rdoGradeType.SelectedValue == "1")
    //    {
    //        pnlSubjective.Visible = false;
    //    }
    //    else
    //    {
    //        pnlSubjective.Visible = true;
    //    }
    //}
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
    //}
    //protected void btnSaveAndCloseGrade_Click(object sender, EventArgs e)
    //{
    //    btnSaveGrade_Click(null, null);

    //    string js = "closeDivAddNewGrade();";
    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", js, true);

    //}

}