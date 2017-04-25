using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.PURC;
using System.Data;

public partial class Purchase_View_QuestionAnswers : System.Web.UI.Page
{
    BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindPurcQuestion();
            if (Request.QueryString["Mode"] == "View")
            {
                grdQuestion.Enabled = false;
                btnSave.Enabled = false;
            }
        }
    }
    #region Purchase Question and Answers
    private void BindPurcQuestion()
    {
        BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase();
        try
        {
            string FormType=GetFormType();
            DataSet dt = objTechService.Get_Purc_Questions(Request.QueryString["DocumentCode"], FormType);

            grdQuestion.DataSource = dt;
            grdQuestion.DataBind();

        }
        catch
        {
        }
    }
    private string GetFormType()
    {
        BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase();
        DataSet ds = objTechService.GET_PURC_DEP_ON_DOCCODE(Request.QueryString["DocumentCode"]);
        string DeptType = Convert.ToString(ds.Tables[0].Rows[0]["DEPARTMENT"]);//Convert.ToString(Request.QueryString["DocumentCode"]).Split('-')[1];
        DataTable dtDept = objTechService.SelectDepartment();
        var q = from a in dtDept.AsEnumerable()
                where a.Field<string>("code") == DeptType
                select new { Form_Type = a.Field<string>("Form_Type")};
        return q.Select(a => a.Form_Type).Single();

    }
    protected void grdQuestion_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblQuestID = (Label)e.Row.FindControl("lblQuestID");
            Label lblQuestion = (Label)e.Row.FindControl("lblQuestion");
            Label lblGradeType = (Label)e.Row.FindControl("lblGradeType");
            TextBox txtDescriptive = (TextBox)e.Row.FindControl("txtDescriptive");
            DropDownList ddlAnswers = (DropDownList)e.Row.FindControl("ddlAnswers");
            Label lblAns=(Label)e.Row.FindControl("lblAns");
            switch (lblGradeType.Text)
            {
                case "1":
                    ddlAnswers.Visible = true;
                    txtDescriptive.Visible = false;
                    break;
                case "2":
                    ddlAnswers.Visible = false;
                    txtDescriptive.Visible = true;
                    break;
            }

            DataSet ds = objTechService.Get_Purc_Questions_Options(Convert.ToInt32(lblQuestID.Text));
            ddlAnswers.DataSource = ds.Tables[0];
            ddlAnswers.DataTextField = "OptionText";
            ddlAnswers.DataValueField = "OPTIONS_ID";
            ddlAnswers.DataBind();
            ddlAnswers.Items.Insert(0, "--SELECT--");
            if (lblAns.Text != "0")
            {
                ddlAnswers.Items.FindByValue((e.Row.FindControl("lblAns") as Label).Text).Selected = true;
            }
        }
    }
    #endregion
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {

            DataTable dtQuest= new DataTable();
            dtQuest.Columns.Add("QUESTION_ID");
            dtQuest.Columns.Add("OPTIONS_ID");
            DataRow dr =null;
            foreach (GridViewRow gridRow in grdQuestion.Rows)
            {
                Label lblQID = (Label)gridRow.FindControl("lblQuestID");
                DropDownList ddl = (DropDownList)gridRow.FindControl("ddlAnswers");
                TextBox txtc = (TextBox)gridRow.FindControl("txtDescriptive");
                if (ddl.SelectedValue != "")
                {
                    dr = dtQuest.NewRow();
                    dr["QUESTION_ID"] = UDFLib.ConvertIntegerToNull(lblQID.Text);
                    dr["OPTIONS_ID"] = UDFLib.ConvertIntegerToNull(ddl.SelectedValue);
                    dtQuest.Rows.Add(dr);
                }
            }
            int retval = objTechService.Insert_Purc_Question("", Convert.ToString(Request.QueryString["DocumentCode"]), Convert.ToInt32(Session["userid"]), dtQuest,0,0);
            if (retval == 1)
            {
                string message1 = "alert('Questions Added succesfully..');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "requisition", message1, true);

            }
        }
        catch
        {
        }


    }
}