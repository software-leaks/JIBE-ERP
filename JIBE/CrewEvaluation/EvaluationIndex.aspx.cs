using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Crew;
using System.Data;

public partial class CrewEvaluation_EvaluationIndex : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Bind_Evaluations();
        }
    }


    protected void ddlEvaluationType_SelectedIndexChanged(object sender, EventArgs e)
    {
        Bind_Evaluations();
    }
    protected void txtfilter_TextChanged(object sender, EventArgs e)
    {
        Bind_Evaluations();
    }

    protected void Bind_Evaluations()
    {
        DataTable dt = BLL_Crew_Evaluation.Get_Evaluations();

        DataView dataView = new DataView(dt);

        if (ViewState["sortExpression"] != null)
            dataView.Sort = ViewState["sortExpression"].ToString();

        GridView_Evaluation.DataSource = dataView;
        GridView_Evaluation.DataBind();

    }
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }

    protected void GridView_Evaluation_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int ID = UDFLib.ConvertToInteger(GridView_Evaluation.DataKeys[e.RowIndex].Value.ToString());

        BLL_Crew_Evaluation.DELETE_Evaluation(ID, GetSessionUserID());
        Bind_Evaluations();
    }
    protected void GridView_Evaluation_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int ID = UDFLib.ConvertToInteger(GridView_Evaluation.DataKeys[e.RowIndex].Value.ToString());
        string Evaluation_Name = e.NewValues["Evaluation_Name"].ToString();

        BLL_Crew_Evaluation.UPDATE_Evaluation(ID, Evaluation_Name, GetSessionUserID());

        GridView_Evaluation.EditIndex = -1;
        Bind_Evaluations();
    }
    protected void GridView_Evaluation_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            GridView_Evaluation.EditIndex = e.NewEditIndex;
            Bind_Evaluations();

        }
        catch
        {
        }
    }
    protected void GridView_Evaluation_RowCancelEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {
            GridView_Evaluation.EditIndex = -1;
            Bind_Evaluations();

        }
        catch
        {
        }
    }

    protected void GridView_Evaluation_Sorting(object sender, GridViewSortEventArgs e)
    {
        Session["pageindex"] = GridView_Evaluation.PageIndex;

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

        Bind_Evaluations();
    }
    protected void GridView_Evaluation_Sorted(object sender, EventArgs e)
    {
        GridView_Evaluation.PageIndex = Convert.ToInt32(Session["pageindex"]);

    }


    protected void btnsave_Click(object sender, EventArgs e)
    {

        int Evaluation_ID = BLL_Crew_Evaluation.INSERT_Evaluation(txtEvaluation_Name.Text.Trim(), GetSessionUserID());

        string js = "alert('Evaluation Planned.');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", js, true);

        Bind_Evaluations();
    }
    protected void btnSaveAndClose_Click(object sender, EventArgs e)
    {
        btnsave_Click(null, null);

        string js = "closeDiv('dvAddNewEvaluation');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", js, true);

    }
    protected void btnClearFilter_Click(object sender, EventArgs e)
    {

        txtfilter.Text = "";
        Bind_Evaluations();

    }

    protected void btnSelectCriteria_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        Response.Redirect("SelectCriteria.aspx?EID=" + btn.CommandArgument.ToString());
    }

}