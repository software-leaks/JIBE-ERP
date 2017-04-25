using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.Crew;


public partial class CrewEvaluation_SelectCriteria : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Load_CategoryList();

            Bind_AssignedCriteria();
            Bind_UnAssignedCriteria();

            Load_Gradings();
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
        Bind_UnAssignedCriteria();
    }
    protected void txtfilter_TextChanged(object sender, EventArgs e)
    {
        Bind_UnAssignedCriteria();
    }
    protected void ddlGradingType_SelectedIndexChanged(object sender, EventArgs e)
    {
        int Grade_ID = UDFLib.ConvertToInteger(ddlGradingType.SelectedValue);
        DataTable dt = BLL_Crew_Evaluation.Get_GradingOptions(Grade_ID);
        rdoGradings.DataSource = dt;
        rdoGradings.DataBind();
    }

    protected void Bind_AssignedCriteria()
    {
        int Evaluation_ID = UDFLib.ConvertToInteger(Request.QueryString["EID"].ToString());
        DataTable dt = BLL_Crew_Evaluation.Get_Assigned_CriteriaList(Evaluation_ID, txtfilter.Text, UDFLib.ConvertToInteger(ddlCategory.SelectedValue));


        DataView dataView = new DataView(dt);

        if (ViewState["sortExpression"] != null)
            dataView.Sort = ViewState["sortExpression"].ToString();

        GridView_AssignedCriteria.DataSource = dataView;
        GridView_AssignedCriteria.DataBind();

    }
    protected void Bind_UnAssignedCriteria()
    {
        int Evaluation_ID = UDFLib.ConvertToInteger(Request.QueryString["EID"].ToString());
        DataTable dt = BLL_Crew_Evaluation.Get_UnAssigned_CriteriaList(Evaluation_ID, txtfilter.Text, UDFLib.ConvertToInteger(ddlCategory.SelectedValue));


        DataView dataView = new DataView(dt);

        if (ViewState["sortExpression"] != null)
            dataView.Sort = ViewState["sortExpression"].ToString();

        GridView_UnAssignedCriteria.DataSource = dataView;
        GridView_UnAssignedCriteria.DataBind();



    }

    protected void GridView_AssignedCriteria_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int ID = UDFLib.ConvertToInteger(DataBinder.Eval(e.Row.DataItem, "Grading_Type").ToString());
                int Grade_Type = UDFLib.ConvertToInteger(DataBinder.Eval(e.Row.DataItem, "Grade_Type").ToString());

                CheckBoxList rdo = (CheckBoxList)e.Row.FindControl("chkOptions");
                DataTable dt = BLL_Crew_Evaluation.Get_GradingOptions(ID);

                rdo.DataSource = dt;
                rdo.DataBind();
                int Evaluation_ID = UDFLib.ConvertToInteger(Request.QueryString["EID"].ToString());
                int Criteria_ID = UDFLib.ConvertToInteger(DataBinder.Eval(e.Row.DataItem, "Criteria_ID").ToString());
                DataTable dtOptions = BLL_Crew_Evaluation.Get_MandatoryGrades(Evaluation_ID, Criteria_ID);

                foreach (ListItem chki in rdo.Items)
                {
                    dtOptions.DefaultView.RowFilter = "OptionText= '" + chki.Text + "'";
                    if (dtOptions.DefaultView.Count > 0)
                        chki.Selected = true;
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
    protected void GridView_AssignedCriteria_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int ID = UDFLib.ConvertToInteger(GridView_AssignedCriteria.DataKeys[e.RowIndex].Value.ToString());
        int Evaluation_ID = UDFLib.ConvertToInteger(Request.QueryString["EID"].ToString());

        BLL_Crew_Evaluation.Remove_Criteria_FromEvaluation(ID,Evaluation_ID, GetSessionUserID());
        Bind_AssignedCriteria();
        Bind_UnAssignedCriteria();
    }
    protected void GridView_AssignedCriteria_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int Evaluation_ID = UDFLib.ConvertToInteger(Request.QueryString["EID"].ToString());
        int Criteria_ID = 0;
        GridViewRow r = (GridViewRow)((ImageButton)e.CommandSource).Parent.Parent;
        int SelectedRowIndex = r.RowIndex;

        if (e.CommandName == "MoveUp")
        {
            Criteria_ID = int.Parse(e.CommandArgument.ToString());
            BLL_Crew_Evaluation.Swap_Criteria_Sort_Order(Evaluation_ID, Criteria_ID, 1);
            if (SelectedRowIndex >= 1)
                SelectedRowIndex -= 1;
            
        }
        else if (e.CommandName == "MoveDown")
        {
            Criteria_ID = int.Parse(e.CommandArgument.ToString());
            BLL_Crew_Evaluation.Swap_Criteria_Sort_Order(Evaluation_ID, Criteria_ID, -1);
            if (SelectedRowIndex < GridView_AssignedCriteria.Rows.Count - 1)
                SelectedRowIndex += 1;
        }

        Bind_AssignedCriteria();
        GridView_AssignedCriteria.SelectedIndex = SelectedRowIndex;
    }
    protected void GridView_UnAssignedCriteria_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int ID = UDFLib.ConvertToInteger(DataBinder.Eval(e.Row.DataItem, "Grading_Type").ToString());
                int Grade_Type = UDFLib.ConvertToInteger(DataBinder.Eval(e.Row.DataItem, "Grade_Type").ToString());

                CheckBoxList rdo = (CheckBoxList)e.Row.FindControl("chkOptions");
                DataTable dt = BLL_Crew_Evaluation.Get_GradingOptions(ID);
                

                rdo.DataSource = dt;
                rdo.DataBind();

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

    protected void btnsave_Click(object sender, EventArgs e)
    {
        int ID = BLL_Crew_Evaluation.INSERT_Criteria(txtCriteria.Text.Trim(), UDFLib.ConvertToInteger(ddlCatName.SelectedValue), UDFLib.ConvertToInteger(ddlGradingType.SelectedValue), GetSessionUserID());
        txtCriteria.Text = "";
        txtfilter.Text = "";
        Bind_UnAssignedCriteria();
    }
    protected void btnSaveAndClose_Click(object sender, EventArgs e)
    {
        int ID = BLL_Crew_Evaluation.INSERT_Criteria(txtCriteria.Text.Trim(), UDFLib.ConvertToInteger(ddlCatName.SelectedValue), UDFLib.ConvertToInteger(ddlGradingType.SelectedValue), GetSessionUserID());
        txtCriteria.Text = "";
        txtfilter.Text = "";
        Bind_UnAssignedCriteria();

        string js = "closeDivAddNewCriteria();";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", js, true);

    }
    protected void btnClearFilter_Click(object sender, EventArgs e)
    {

        txtfilter.Text = "";
        Bind_UnAssignedCriteria();

    }
    
    protected void lnkAddToEvaluation_Click(object sender, EventArgs e)
    {
        int Evaluation_ID = UDFLib.ConvertToInteger(Request.QueryString["EID"].ToString());

        foreach (GridViewRow row in GridView_UnAssignedCriteria.Rows)
        {
            CheckBox chkSelect = (CheckBox)row.FindControl("chkSelect");
            int Criteria_ID = UDFLib.ConvertToInteger(GridView_UnAssignedCriteria.DataKeys[row.RowIndex].Value.ToString());

            if (chkSelect != null)
            {
                if (chkSelect.Checked == true)
                {
                    DataTable dtOptionValue = new DataTable();
                    dtOptionValue.Columns.Add(new DataColumn("OptionText", typeof(string)));
                    CheckBoxList chk = (CheckBoxList)row.FindControl("chkOptions");
                    foreach (ListItem chki in chk.Items)
                    {
                        if (chki.Selected == true)
                            dtOptionValue.Rows.Add(chki.Text);
                    }
                    BLL_Crew_Evaluation.Add_MandatoryGrades_ToEvaluation(Criteria_ID, Evaluation_ID, GetSessionUserID(), dtOptionValue);                   
                    BLL_Crew_Evaluation.Add_Criteria_ToEvaluation(Criteria_ID, Evaluation_ID, GetSessionUserID());
                }
            }

        }
        Bind_AssignedCriteria();
        Bind_UnAssignedCriteria();
    }
    protected void lnkGoBackToEvaluation_Click(object sender, EventArgs e)
    {
        Response.Redirect("EvaluationPlanning.aspx");

    }
}