using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Crew;
using System.Data;

public partial class Crew_InterviewAdmin : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            UserAccessValidation();
            Load_RankList();
            Bind_Interviews();
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
            lnkAddNewInterview.Visible = false;
            GridView_Interview.Columns[GridView_Interview.Columns.Count - 4].Visible = false;
            //GridView_Interview.Columns[GridView_Interview.Columns.Count - 4].Visible = false;
        }
        if (objUA.Edit == 0)
        {
            GridView_Interview.Columns[GridView_Interview.Columns.Count - 3].Visible = false;

        }
        if (objUA.Delete == 0)
        {
            GridView_Interview.Columns[GridView_Interview.Columns.Count - 2].Visible = false;

        }
        if (objUA.Approve == 0)
        {
        }

    }

    public void Load_RankList()
    {
        BLL_Crew_Admin objCrewAdmin = new BLL_Crew_Admin();
        DataTable dt = objCrewAdmin.Get_RankList();

        ddlRank.DataSource = dt;
        ddlRank.DataTextField = "Rank_Short_Name";
        ddlRank.DataValueField = "ID";
        ddlRank.DataBind();
        ddlRank.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));
        ddlRank.SelectedIndex = 0;

        ddlRank1.DataSource = dt;
        ddlRank1.DataTextField = "Rank_Short_Name";
        ddlRank1.DataValueField = "ID";
        ddlRank1.DataBind();
        ddlRank1.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));
        ddlRank1.SelectedIndex = 0;
    }    

    protected void ddlInterviewType_SelectedIndexChanged(object sender, EventArgs e)
    {
        Bind_Interviews();
    }
    protected void ddlRank_SelectedIndexChanged(object sender, EventArgs e)
    {
        Bind_Interviews();
    }
    protected void Bind_Interviews()
    {
        DataTable dt = BLL_Crew_Interview.Get_Interviews(int.Parse(ddlRank1.SelectedValue), GetSessionUserID());

        DataView dataView = new DataView(dt);

        if (ViewState["sortExpression"] != null)
            dataView.Sort = ViewState["sortExpression"].ToString();

        GridView_Interview.DataSource = dataView;
        GridView_Interview.DataBind();

        ddlInterviewNames.DataSource = dataView;
        ddlInterviewNames.DataBind();

    }
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }

    protected void GridView_Interview_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int IQID = UDFLib.ConvertToInteger(GridView_Interview.DataKeys[e.RowIndex].Values[0].ToString());
        int RankID = UDFLib.ConvertToInteger(GridView_Interview.DataKeys[e.RowIndex].Values[1].ToString());

        BLL_Crew_Interview.Delete_Interview(IQID,RankID, GetSessionUserID());
        Bind_Interviews();
    }
    protected void GridView_Interview_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int IQID = UDFLib.ConvertToInteger(GridView_Interview.DataKeys[e.RowIndex].Values[0].ToString());
        int RankID = UDFLib.ConvertToInteger(GridView_Interview.DataKeys[e.RowIndex].Values[1].ToString());

        string Interview_Name = e.NewValues["Interview_Name"].ToString();
        BLL_Crew_Interview.UPDATE_Interview(IQID, Interview_Name, RankID, GetSessionUserID());

        GridView_Interview.EditIndex = -1;
        Bind_Interviews();
    }
    protected void GridView_Interview_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            GridView_Interview.EditIndex = e.NewEditIndex;
            Bind_Interviews();

        }
        catch
        {
        }
    }
    protected void GridView_Interview_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                int IQID = UDFLib.ConvertToInteger(DataBinder.Eval(e.Row.DataItem, "IQID").ToString());
                int FrequencyMonths = UDFLib.ConvertToInteger(DataBinder.Eval(e.Row.DataItem, "IQID").ToString());

            }

        }
        catch
        {
        }
    }
    protected void GridView_Interview_RowCancelEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {
            GridView_Interview.EditIndex = -1;
            Bind_Interviews();

        }
        catch
        {
        }
    }

    protected void GridView_Interview_Sorting(object sender, GridViewSortEventArgs e)
    {
        Session["pageindex"] = GridView_Interview.PageIndex;

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

        Bind_Interviews();
    }
    protected void GridView_Interview_Sorted(object sender, EventArgs e)
    {
        GridView_Interview.PageIndex = Convert.ToInt32(Session["pageindex"]);

    }

    protected void btnSaveInterview_Click(object sender, EventArgs e)
    {
        Create_Interview();
    }
    protected void btnSaveAndCloseInterview_Click(object sender, EventArgs e)
    {
        if (Create_Interview() == 1)
        {
            string js = "closeDiv('dvAddNewInterview');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert4", js, true);
        }

    }

    protected int Create_Interview()
    {
        int Ret = 1;
        string js = "";

        int RankID = UDFLib.ConvertToInteger(ddlRank.SelectedValue);
        string InterviewType = ddlInterviewType.SelectedValue;
        if (txtInterview_Name.Text == "")
        {
            Ret = 0;
            js = "alert('Enter Interview Questionnaire Name.');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", js, true);
        }
        else if (RankID == 0)
        {
            Ret = 0;
            js = "alert('Select Rank Name.');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", js, true);
        }
        else if (InterviewType.Equals("0"))
        {
            Ret = 0;
            js = "alert('Select Interview Type.');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", js, true);

        }
        else
        {
            Ret = 1;
            int CopyFrom = UDFLib.ConvertToInteger(ddlInterviewNames.SelectedValue);
            int IQID = BLL_Crew_Interview.INS_Interview(txtInterview_Name.Text.Trim(), RankID, CopyFrom, GetSessionUserID(), InterviewType);

            if (IQID > 0)
            {
                if (InterviewType == "Interview")
                    js = "alert('Interview Created.');";
                else
                    js = "alert('Briefing Created.');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", js, true);

                Bind_Interviews();
            }
        }
        return Ret;
    }
    protected void btnSelectCriteria_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        GridViewRow r = (GridViewRow)((Button)sender).Parent.Parent;

        string IQID = GridView_Interview.DataKeys[r.RowIndex].Values[0].ToString();
        string RankID = GridView_Interview.DataKeys[r.RowIndex].Values[1].ToString();

        Response.Redirect("InterviewCriteria.aspx?IQID=" + IQID + "&RankID=" + RankID);
    }
}