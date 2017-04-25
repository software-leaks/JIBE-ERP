﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using SMS.Data;
using SMS.Business.INFRA.Infrastructure;

public partial class Infrastructure_Libraries_Drill_Questionnaire : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            UserAccessValidation();
            Bind_Grading();
            Bind_Question();
            Get_Grading_ForQuestions();
        }
    }

    protected void UserAccessValidation()
    {
        SMS.Properties.UserAccess objUA = new SMS.Properties.UserAccess();

        int CurrentUserID = Convert.ToInt32(Session["USERID"]);
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        SMS.Business.Infrastructure.BLL_Infra_UserCredentials objUser = new SMS.Business.Infrastructure.BLL_Infra_UserCredentials();
        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
            Response.Redirect("~/default.aspx?msgid=1");

        if (objUA.Add == 0)
        {
            lnkAddNewQuest.Visible = false;
        }
        if (objUA.Edit == 0)
        {
            grdQuestion.Columns[grdQuestion.Columns.Count - 2].Visible = false;

        }
        if (objUA.Delete == 0)
        {
            grdQuestion.Columns[grdQuestion.Columns.Count - 1].Visible = false;

        }
        if (objUA.Approve == 0)
        {
        }

    }
    private void Bind_Grading()
    {
        DataTable dt = BLL_Infra_DrillQuestionnaire.Get_GradingList();
        grdGrading.DataSource = dt;
        grdGrading.DataBind();
    }
    private void Bind_Question()
    {
        DataTable dtQuest = BLL_Infra_DrillQuestionnaire.Get_QuestionList();
        grdQuestion.DataSource = dtQuest;
        grdQuestion.DataBind();
    }
    private void Get_Grading_ForQuestions()
    {
        DataTable dtGrad = BLL_Infra_DrillQuestionnaire.Get_GradingList();
        ddlGradingType.DataSource = dtGrad;
        ddlGradingType.DataTextField = Convert.ToString(dtGrad.Columns["Grade_Name"]);
        ddlGradingType.DataValueField = Convert.ToString(dtGrad.Columns["ID"]);
        ddlGradingType.DataBind();
        ddlGradingType.Items.Insert(0, new ListItem("-Select-", "0"));

    }
    private DataTable GetGradingOptions(int Grade_ID)
    {

        DataTable dt = BLL_Infra_DrillQuestionnaire.Get_GradingOptions(Grade_ID);
        return dt;
    }
    private void ClearControls_Grade()
    {
        txtGrade.Text = string.Empty;
        ddlDivisions.SelectedIndex = 0;
        ddlMin.SelectedIndex = 0;
        ddlMax.SelectedIndex = 1;
        rptOptions.DataSource = null;
        rptOptions.DataBind();
    }
    private void ClearControls_Question()
    {
        txtQuestion.Text = string.Empty;
        ddlGradingType.SelectedIndex = 0;
        rdoGradings.Items.Clear();
        

    }
    private DataTable ValidateMin_Max()
    {
        DataTable dt = new DataTable("Options");
        dt.Columns.Add("OptionText", typeof(string));
        dt.Columns.Add("OptionValue", typeof(string));
        decimal min = UDFLib.ConvertToDecimal(ddlMin.SelectedValue);
        decimal max = UDFLib.ConvertToDecimal(ddlMax.SelectedValue);
        int Division = UDFLib.ConvertToInteger(ddlDivisions.SelectedValue) - 1;
        if (min > max)
        {
            string js = "ShowAlert();";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", js, true);
         
        }
        if (min >= 0 && max > 0 && Division > 0)
        {
            decimal increment = (max - min) / Division;

            while (min <= max)
            {
                DataRow dr1 = dt.NewRow();
                dr1[0] = min.ToString();
                dr1[1] = min.ToString();
                dt.Rows.Add(dr1);

                min += increment;
            }
        }
        return dt;
    }

    #region Controls Events
    protected void btnSaveGrade_Click(object sender, EventArgs e)
    {
        string Mode = "A";
        DataTable dt=ValidateMin_Max();
        if (dt.Rows.Count==0)
        {
            return;
        }
        int ID = BLL_Infra_DrillQuestionnaire.INS_UPD_DEL_Grading(null, txtGrade.Text.Trim(), 1, UDFLib.ConvertToInteger(ddlMin.SelectedValue), UDFLib.ConvertToInteger(ddlMax.SelectedValue), UDFLib.ConvertToInteger(ddlDivisions.SelectedValue), Convert.ToInt32(Session["USERID"]), Mode);

        for (int i = 0; i < rptOptions.Items.Count; i++)
        {
            TextBox txtValue = (TextBox)rptOptions.Items[i].FindControl("txtValue");
            TextBox txtText = (TextBox)rptOptions.Items[i].FindControl("txtText");

            if (txtText != null && txtValue != null)
            {
                BLL_Infra_DrillQuestionnaire.INS_UPD_DEL_GradingOption(ID, txtText.Text.Trim(), UDFLib.ConvertToDecimal(txtValue.Text), Convert.ToInt32(Session["USERID"]));
            }
        }
        Bind_Grading();
        Get_Grading_ForQuestions();
        ClearControls_Grade();
    }
    protected void btnSaveAndCloseGrade_Click(object sender, EventArgs e)
    {
        btnSaveGrade_Click(null, null);
        string js = "closeDiv('dvAddNewGrade');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", js, true);

    }

    protected void btnSaveAndAdd_Click(object sender, EventArgs e)
    {
        int ID = BLL_Infra_DrillQuestionnaire.INS_UPD_DEL_Question(null, txtQuestion.Text.Trim(), Convert.ToInt32(ddlGradingType.SelectedValue), Convert.ToInt32(Session["USERID"]), "A");
        txtQuestion.Text = "";
        Bind_Question();
        Bind_Grading();
        ClearControls_Question();
    }
    protected void btnSaveAndClose_Click(object sender, EventArgs e)
    {
        btnSaveAndAdd_Click(null, null);
        string js = "closeDiv('dvAddNewCriteria');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", js, true);
    }

    #endregion Controls Events

    protected void ddlDivisions_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        if (ddlDivisions.SelectedValue != "0")
        {
            DataTable dt = ValidateMin_Max();
            rptOptions.DataSource = dt;
            rptOptions.DataBind();

        }
        else
        {
            rptOptions.DataSource = null;
            rptOptions.DataBind();
        }

        string js1 = "showDiv('dvAddNewGrade');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", js1, true);
    }
    //protected void rdoGradeType_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    //if (rdoGradeType.SelectedValue == "1")
    //    //{
    //    //    pnlSubjective.Visible = false;
    //    //    txtSubjectiveText.Text = "";

    //    //}
    //    //else
    //    //{
    //    //    pnlSubjective.Visible = true;
    //    //    txtSubjectiveText.Text = "";

    //    //}
    //}
    protected void ddlGradingType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlGradingType.SelectedValue != "0")
        {
            DataTable dtGradOption = GetGradingOptions(UDFLib.ConvertToInteger(ddlGradingType.SelectedValue));
            rdoGradings.DataSource = dtGradOption;
            rdoGradings.DataTextField = Convert.ToString(dtGradOption.Columns["optiontext"]);
            rdoGradings.DataValueField = Convert.ToString(dtGradOption.Columns["optionvalue"]);
            rdoGradings.DataBind();
        }
    }
    protected void grdGrading_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int ID = UDFLib.ConvertToInteger(grdGrading.DataKeys[e.Row.RowIndex].Value.ToString());

                Label lblGrdUsed = (Label)e.Row.FindControl("lblGradeIsUsed");
                ImageButton LinkButton1del =(ImageButton)e.Row.FindControl("LinkButton1del");
                if (lblGrdUsed.Text == "TRUE")
                {
                    LinkButton1del.Visible = false;
                }
                else
                {
                    LinkButton1del.Visible = true;
                }
                RadioButtonList rdoOptions = (RadioButtonList)e.Row.FindControl("rdoOptions");
                DataTable dtGradOption = GetGradingOptions(ID);

                rdoOptions.DataSource = dtGradOption;
                rdoOptions.DataTextField = Convert.ToString(dtGradOption.Columns["optiontext"]);
                rdoOptions.DataValueField = Convert.ToString(dtGradOption.Columns["optionvalue"]);
                rdoOptions.DataBind();


            }
        }
        catch
        {
        }
    }
    protected void grdGrading_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grdGrading.EditIndex = -1;
        Bind_Grading();

    }
    protected void grdGrading_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int ID = UDFLib.ConvertToInteger(grdGrading.DataKeys[e.RowIndex].Value.ToString());
        GridViewRow row = grdGrading.Rows[e.RowIndex];
        Label lblGrdUsed = (Label)row.FindControl("lblGradeIsUsed");
        string Mode = "D";
        BLL_Infra_DrillQuestionnaire.INS_UPD_DEL_Grading(ID, "", null, null, null, null, Convert.ToInt32(Session["USERID"]), Mode);
        Bind_Grading();
        Get_Grading_ForQuestions();
        Bind_Question();
    }
    protected void grdGrading_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grdGrading.EditIndex = e.NewEditIndex;
        Bind_Grading();
    }
    protected void grdGrading_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int ID = UDFLib.ConvertToInteger(grdGrading.DataKeys[e.RowIndex].Value.ToString());
        string Grade_Name = e.NewValues["Grade_Name"].ToString();
        int Grade_Type = 1;//UDFLib.ConvertToInteger(e.NewValues["Grade_Type"].ToString());
        int Min = UDFLib.ConvertToInteger(e.NewValues["Min"].ToString());
        int Max = UDFLib.ConvertToInteger(e.NewValues["Max"].ToString());

        if (Min > Max)
        {
            string js = "alert('Max Point should be Greater than Min Point');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", js, true);
            return;
        }

        int Divisions = UDFLib.ConvertToInteger(e.NewValues["Divisions"].ToString());

        BLL_Infra_DrillQuestionnaire.INS_UPD_DEL_Grading(ID, Grade_Name, Grade_Type, Min, Max, Divisions, Convert.ToInt32(Session["USERID"]), "M");
        grdGrading.EditIndex = -1;
        Bind_Grading();
    }

    protected void grdQuestion_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int ID = UDFLib.ConvertToInteger(DataBinder.Eval(e.Row.DataItem, "Grading_Type").ToString());
            int Grade_Type = UDFLib.ConvertToInteger(DataBinder.Eval(e.Row.DataItem, "Grade_Type").ToString());

            RadioButtonList rdo = (RadioButtonList)e.Row.FindControl("rdoOptions");
       
            if (rdo != null)
            {
                DataTable dtrdo = GetGradingOptions(ID);
                rdo.DataSource = dtrdo;
                rdo.DataBind();
            }

            if ((e.Row.RowState & DataControlRowState.Edit) > 0)
            {
                //DropDownList ddlgrdGradingType = (DropDownList)e.Row.FindControl("ddlGradingType");
                //DataTable dt = BLL_Infra_DrillQuestionnaire.Get_GradingList();
                //ddlgrdGradingType.DataSource = dt;
                //ddlgrdGradingType.DataBind();
            }
        }
    }
    protected void grdQuestion_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grdQuestion.EditIndex = e.NewEditIndex;
        Bind_Question();
    }
    protected void grdQuestion_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int ID = UDFLib.ConvertToInteger(grdQuestion.DataKeys[e.RowIndex].Value.ToString());
        string Question = e.NewValues["Question"].ToString();

        int Grading_Type = UDFLib.ConvertToInteger(e.NewValues["Grading_Type"].ToString());

        BLL_Infra_DrillQuestionnaire.INS_UPD_DEL_Question(ID, Question, Grading_Type, Convert.ToInt32(Session["USERID"]), "M");

        grdQuestion.EditIndex = -1;
        Bind_Question();
        Bind_Grading();
    }
    protected void grdQuestion_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int ID = UDFLib.ConvertToInteger(grdQuestion.DataKeys[e.RowIndex].Value.ToString());
        string Mode = "D";
        BLL_Infra_DrillQuestionnaire.INS_UPD_DEL_Question(ID, "", null, Convert.ToInt32(Session["USERID"]), Mode);
        Bind_Question();
        Bind_Grading();
    }
    protected void grdQuestion_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grdQuestion.EditIndex = -1;
        Bind_Question();
    }
}