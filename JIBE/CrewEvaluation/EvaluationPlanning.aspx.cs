using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Crew;
using System.Data;
using SMS.Business.Infrastructure;
public partial class CrewEvaluation_EvaluationPlanning : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            UserAccessValidation();
            Bind_Evaluations();
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
            lnkAddNewEvaluation.Visible = false;
            GridView_Evaluation.Columns[GridView_Evaluation.Columns.Count - 3].Visible = false;
            GridView_Evaluation.Columns[GridView_Evaluation.Columns.Count - 4].Visible = false;
        }
        if (objUA.Edit == 0)
        {
            GridView_Evaluation.Columns[GridView_Evaluation.Columns.Count - 2].Visible = false;

        }
        if (objUA.Delete == 0)
        {
            GridView_Evaluation.Columns[GridView_Evaluation.Columns.Count - 1].Visible = false;

        }
        if (objUA.Approve == 0)
        {
        }

    }
    protected void chkOffice_CheckedChanged(object sender, EventArgs e)
    {
        if (chkOffice.Checked == true)
        {
            lblEvaluator.Visible = true;
            lblNotify.Visible = true;
            ddlEvaluator.Visible = true;
            txtNotify.Visible = true;
            lblDays.Visible = true;
            lblnman.Visible = true;
            lbleman.Visible = true;
            BLL_Infra_UserCredentials objUCred = new BLL_Infra_UserCredentials();
            DataTable dtUser = objUCred.Get_UserList(Convert.ToInt32(Session["USERCOMPANYID"]));
            dtUser = dtUser.Select("User_Type='OFFICE USER'").CopyToDataTable();
            ddlEvaluator.DataSource = dtUser;
            ddlEvaluator.DataTextField = "USERNAME";
            ddlEvaluator.DataValueField = "USERID";
            ddlEvaluator.DataBind();

            ListItem lst = new ListItem();
            lst.Text = "--SELECT--";
            lst.Value = "-1";
            ddlEvaluator.Items.Insert(0, lst);
        }

        else
        {
            lblEvaluator.Visible = false;
            lblNotify.Visible = false;
            ddlEvaluator.Visible = false;
            txtNotify.Visible = false;
            lblDays.Visible = false;
            lblnman.Visible = false;
            lbleman.Visible = false;
            ddlEvaluator.Items.Clear();
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
    protected void btnClearFilter_Click(object sender, EventArgs e)
    {

        txtfilter.Text = "";
        Bind_Evaluations();

    }

    protected void Bind_Evaluations()
    {
        DataTable dt = BLL_Crew_Evaluation.Get_Evaluations(txtfilter.Text != "" ? txtfilter.Text : null);
       
        DataView dataView = new DataView(dt);

        if (ViewState["sortExpression"] != null)
            dataView.Sort = ViewState["sortExpression"].ToString();

        GridView_Evaluation.DataSource = dataView;
        GridView_Evaluation.DataBind();

    }
    public void BindEvaluationLocation(int EvaluationID)
    {
        DataTable dt = BLL_Crew_Evaluation.Get_EvaluationLocation(EvaluationID);


        if (dt.Rows.Count > 0)
        {
            BLL_Infra_UserCredentials objUCred = new BLL_Infra_UserCredentials();
            DataTable dtUser = objUCred.Get_UserList(Convert.ToInt32(Session["USERCOMPANYID"]));
            dtUser = dtUser.Select("User_Type='OFFICE USER'").CopyToDataTable();
            ddlEvaluator.DataSource = dtUser;
            ddlEvaluator.DataTextField = "USERNAME";
            ddlEvaluator.DataValueField = "USERID";
            ddlEvaluator.DataBind();

            ListItem lst = new ListItem();
            lst.Text = "--SELECT--";
            lst.Value = "-1";
            ddlEvaluator.Items.Insert(0, lst);
            ddlEvaluator.Visible = true;
            txtNotify.Visible = true;
            txtNotify.Text = "";
            lblEvaluator.Visible = true;
            lblNotify.Visible = true;
            lblDays.Visible = true;
            lblnman.Visible = true;
            lbleman.Visible = true;

            chkOffice.Checked = true;
            ddlEvaluator.SelectedValue = dt.Rows[0]["EvaluatorID"].ToString();
            txtNotify.Text = dt.Rows[0][1].ToString();
        }

        else
        {
            chkOffice.Checked = false;
            ddlEvaluator.Visible = false;
            txtNotify.Visible = false;
            txtNotify.Text = "";
            lblEvaluator.Visible = false;
            lblNotify.Visible = false;
            lblDays.Visible = false;
            lblnman.Visible = false;
            lbleman.Visible = false;
        }
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
        //string Evaluation_No = e.NewValues["Evaluation_No"].ToString();
        //int Evaluation_Type = UDFLib.ConvertToInteger(e.NewValues["Evaluation_Type"].ToString());
        //int Rank_ID = UDFLib.ConvertToInteger(e.NewValues["Rank"].ToString());

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
    protected void GridView_Evaluation_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                int Evaluation_ID = UDFLib.ConvertToInteger(DataBinder.Eval(e.Row.DataItem, "Evaluation_ID").ToString());
                int FrequencyMonths = UDFLib.ConvertToInteger(DataBinder.Eval(e.Row.DataItem, "Evaluation_ID").ToString());

                //Label lblFrequency = (Label)e.Row.FindControl("lblFrequency");

                //string Frequency = "";

                //DataTable dt = BLL_Crew_Evaluation.Get_EvaluationMonths(Evaluation_ID);
                //foreach (DataRow dr in dt.Rows)
                //{
                //    if (Frequency != "")
                //        Frequency += ", ";

                //    Frequency += dr["MonthName"].ToString();
                //}
                //if (Frequency == "")
                //{
                //    Frequency = "Every " + DataBinder.Eval(e.Row.DataItem, "FrequencyMonths").ToString() + " Months";
                //}
                //lblFrequency.Text = Frequency;
            }

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

    protected int Save()
    {
        if (txtEvaluation_Name.Text.Equals(""))
        {
            string js = "alert('Please enter Evaluation Name.');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", js, true);
            return 0;
        }
        else
        {
            int Evaluation_ID = BLL_Crew_Evaluation.INSERT_Evaluation(txtEvaluation_Name.Text.Trim(), GetSessionUserID());
            Bind_Evaluations();

            string js = "alert('Evaluation Created.');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", js, true);
            return 1;
        }
    }
    protected void btnSaveEvaluation_Click(object sender, EventArgs e)
    {
        int saveStatus = Save();
        if (saveStatus == 1)
        {
            txtEvaluation_Name.Text = "";
        }

    }
    protected void btnSaveAndCloseEvaluation_Click(object sender, EventArgs e)
    {
        int saveStatus = Save();
        if (saveStatus == 1)
        {
            txtEvaluation_Name.Text = "";
            string js = "closeDiv('dvAddNewEvaluation');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert4", js, true);
        }

    }
    protected void btnSelectCriteria_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        Response.Redirect("SelectCriteria.aspx?EID=" + btn.CommandArgument.ToString());
    }

    protected void RankList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (((GridView)sender).ID == "GridView_Selected")
            GridView_NotSelected.SelectedIndex = -1;
        else
            GridView_Selected.SelectedIndex = -1;

        int RankID = UDFLib.ConvertToInteger(((GridView)sender).SelectedValue);

        Load_MonthsAndRules_ForRank(RankID);
    }

    protected void btnScheduling_Click(object sender, EventArgs e)
    {
        try
        {
            int EID = UDFLib.ConvertToInteger(((Button)sender).CommandArgument);
            BindEvaluationLocation(EID);
            foreach (ListItem li in lstSelectedMonth.Items)
            {
                li.Selected = false;
            }
            foreach (ListItem li in lstSelectedRules.Items)
            {
                li.Selected = false;
            }

            if (EID > 0)
            {
                ddlEvaluations.SelectedValue = EID.ToString();
                ddlEvaluations.Enabled = false;
                Bind_Evaluation_Ranks(EID);
            }

            if (GridView_Selected.Rows.Count > 0)
            {
                GridView_Selected.SelectedIndex = 0;
                int RankID = UDFLib.ConvertToInteger(GridView_Selected.SelectedValue);
                Load_MonthsAndRules_ForRank(RankID);
            }
            string js = "showDiv('dvEvaluationScheduling');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Showdiv", js, true);
        }
        catch { }

    }

    private int Update_Evaluation_Planning()
    {
        int Evaluation_ID = UDFLib.ConvertToInteger(ddlEvaluations.SelectedValue);
        int RankID = (GridView_NotSelected.SelectedIndex >= 0) ? UDFLib.ConvertToInteger(GridView_NotSelected.SelectedValue) : UDFLib.ConvertToInteger(GridView_Selected.SelectedValue);
        
        int SelectedMonthCount = 0;
        int SelectedRulesCount = 0;
        int ReturnValue = 0;
        
        if (RankID > 0)
        {
            foreach (ListItem liMon in lstSelectedMonth.Items)
            {
                if (liMon.Selected == true)
                {
                    BLL_Crew_Evaluation.UPDATE_EvaluationMonths(Evaluation_ID, RankID, UDFLib.ConvertToInteger(liMon.Value), 1, GetSessionUserID());
                    SelectedMonthCount++;
                }
                else
                {
                    BLL_Crew_Evaluation.UPDATE_EvaluationMonths(Evaluation_ID, RankID, UDFLib.ConvertToInteger(liMon.Value), 0, GetSessionUserID());
                }
            }
            foreach (ListItem liRule in lstSelectedRules.Items)
            {
                if (liRule.Selected == true)
                {
                    BLL_Crew_Evaluation.UPDATE_EvaluationRules(Evaluation_ID, RankID, UDFLib.ConvertToInteger(liRule.Value), 1, GetSessionUserID());
                    SelectedRulesCount++;
                }
                else
                {
                    BLL_Crew_Evaluation.UPDATE_EvaluationRules(Evaluation_ID, RankID, UDFLib.ConvertToInteger(liRule.Value), 0, GetSessionUserID());
                }
            }

            if (SelectedMonthCount > 0 || SelectedRulesCount > 0)
            {                
                ReturnValue =  1;
            }
            else
            {
                string js2 = "alert('Please select applicable month or rule.');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert2", js2, true);
            }
        }
        else
        {
            string js3 = "alert('Please select rank.');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert3", js3, true);
        }

        return ReturnValue;
    }
    
    protected void btnSaveSchedule_Click(object sender, EventArgs e)
    {
        int Evaluation_ID = UDFLib.ConvertToInteger(ddlEvaluations.SelectedValue);
        int RankID = (GridView_NotSelected.SelectedIndex >= 0) ? UDFLib.ConvertToInteger(GridView_NotSelected.SelectedValue) : UDFLib.ConvertToInteger(GridView_Selected.SelectedValue);

        int Ret = Update_Evaluation_Planning();

        if (Ret == 1)
        {
            string js1 = "alert('Evaluation Planning Updated.');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert1", js1, true);
            
            Bind_Evaluation_Ranks(Evaluation_ID);

            if (GridView_Selected.Rows.Count > 0)
            {
                GridView_Selected.SelectedIndex = GridView_Selected.DataKeys.IndexOf(RankID);
                Load_MonthsAndRules_ForRank(RankID);
            }
          //  if (chkOffice.Checked == true)
            //{
            int chkStatus = 0;
            if (chkOffice.Checked == true)
            {
                chkStatus = 1;
                BLL_Crew_Evaluation.INSERT_EvaluationLocation(Evaluation_ID, UDFLib.ConvertToInteger(ddlEvaluator.SelectedValue), UDFLib.ConvertToInteger(txtNotify.Text), GetSessionUserID(), chkStatus);
            }
            else
            {
                chkStatus = 0;
                BLL_Crew_Evaluation.INSERT_EvaluationLocation(Evaluation_ID, -1, UDFLib.ConvertToInteger(txtNotify.Text), GetSessionUserID(), chkStatus);
            }
             
           // }
        }
    }
    protected void btnSaveAndCloseSchedule_Click(object sender, EventArgs e)
    {
        int Evaluation_ID = UDFLib.ConvertToInteger(ddlEvaluations.SelectedValue);
        int RankID = (GridView_NotSelected.SelectedIndex >= 0) ? UDFLib.ConvertToInteger(GridView_NotSelected.SelectedValue) : UDFLib.ConvertToInteger(GridView_Selected.SelectedValue);

        int Ret = Update_Evaluation_Planning();

        if (Ret == 1)
        {
            int chkStatus = 0;
            if (chkOffice.Checked == true)
            {
                chkStatus = 1;
                BLL_Crew_Evaluation.INSERT_EvaluationLocation(Evaluation_ID, UDFLib.ConvertToInteger(ddlEvaluator.SelectedValue), UDFLib.ConvertToInteger(txtNotify.Text), GetSessionUserID(), chkStatus);
            }
            else
            {
                chkStatus = 0;
                BLL_Crew_Evaluation.INSERT_EvaluationLocation(Evaluation_ID, -1, UDFLib.ConvertToInteger(txtNotify.Text), GetSessionUserID(), chkStatus);
            }
              
            string js = "alert('Evaluation Planning Updated.');closeDiv('dvEvaluationScheduling');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "SaveAndClose", js, true);
        }
    }


    protected void GridView_Selected_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int RankID = UDFLib.ConvertToInteger(GridView_Selected.DataKeys[e.RowIndex].Value.ToString());
        int Evaluation_ID = UDFLib.ConvertToInteger(ddlEvaluations.SelectedValue);

        if (RankID > 0)
        {
            foreach (ListItem liMon in lstSelectedMonth.Items)
            {
                BLL_Crew_Evaluation.UPDATE_EvaluationMonths(Evaluation_ID, RankID, UDFLib.ConvertToInteger(liMon.Value), 0, GetSessionUserID());
            }
            foreach (ListItem liRule in lstSelectedRules.Items)
            {
                BLL_Crew_Evaluation.UPDATE_EvaluationRules(Evaluation_ID, RankID, UDFLib.ConvertToInteger(liRule.Value), 0, GetSessionUserID());
            }

            string js = "alert('Evaluation Planned Updated');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert2", js, true);


            Bind_Evaluation_Ranks(Evaluation_ID);

            if (GridView_Selected.Rows.Count > 0)
            {
                GridView_Selected.SelectedIndex = 1;
                RankID = UDFLib.ConvertToInteger(GridView_Selected.SelectedValue);
                Load_MonthsAndRules_ForRank(RankID);
            }
        }
    }

    protected void Load_MonthsAndRules_ForRank(int RankID)
    {
        try
        {
            string EID = ddlEvaluations.SelectedValue;

            DataTable dtMonths = BLL_Crew_Evaluation.Get_EvaluationMonths(int.Parse(EID), RankID);
            DataTable dtRules = BLL_Crew_Evaluation.Get_EvaluationRules(int.Parse(EID), RankID);

            foreach (ListItem li in lstSelectedMonth.Items)
            {
                li.Selected = false;
            }
            foreach (ListItem li in lstSelectedRules.Items)
            {
                li.Selected = false;
            }
            foreach (DataRow dr in dtMonths.Rows)
            {
                foreach (ListItem li in lstSelectedMonth.Items)
                {
                    if (li.Value == dr["monthno"].ToString() && dr["active_status"].ToString() == "1")
                        li.Selected = true;
                }
                //lstSelectedMonth.Items[int.Parse(dr["monthno"].ToString()) - 1].Selected = dr["active_status"].ToString() == "1" ? true : false;
            }

            foreach (DataRow dr in dtRules.Rows)
            {
                foreach (ListItem li in lstSelectedRules.Items)
                {
                    if (li.Value == dr["RuleID"].ToString() && dr["active_status"].ToString() == "1")
                        li.Selected = true;
                }
                //lstSelectedRules.Items[int.Parse(dr["RuleID"].ToString()) - 1].Selected = dr["active_status"].ToString() == "1" ? true : false;
            }
        }
        catch { }
    }

    protected void Bind_Evaluation_Ranks(int EID)
    {
        DataTable dtSelected = BLL_Crew_Evaluation.Get_EvaluationSelectedRanks(UDFLib.ConvertToInteger(EID), GetSessionUserID());
        DataTable dtUnSelected = BLL_Crew_Evaluation.Get_EvaluationUnSelectedRanks(UDFLib.ConvertToInteger(EID), GetSessionUserID());

        GridView_Selected.SelectedIndex = -1;
        GridView_NotSelected.SelectedIndex = -1;
        
        GridView_Selected.DataSource = dtSelected;
        GridView_Selected.DataBind();

        GridView_NotSelected.DataSource = dtUnSelected;
        GridView_NotSelected.DataBind();
    }
}

public static class WebControlsEx
{
    public static int IndexOf(this DataKeyArray dataKeyArray, object value)
    {
        if (dataKeyArray.Count < 1) throw new InvalidOperationException("DataKeyArray contains no elements.");
        var keys = dataKeyArray.Cast<DataKey>().ToList();
        var key = keys.SingleOrDefault(k => k.Value.Equals(value));
        if (key == null) return -1;
        return keys.IndexOf(key);
    }
}