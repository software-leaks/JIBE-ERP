using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using System.Text;
using SMS.Business.Infrastructure;
using SMS.Business.PURC;
using System.Web.UI.HtmlControls;
using SMS.Business.PMS;
using SMS.Business.Crew;
using SMS.Business.VET;
using SMS.Properties;
using System.Reflection;

public partial class Technical_Vetting_Vetting_AddQuestion : System.Web.UI.Page
{
    BLL_Infra_VesselLib objBLL = new BLL_Infra_VesselLib();
    BLL_VET_VettingLib objBLLVetLib = new BLL_VET_VettingLib();
    UserAccess objUA = new UserAccess();
    BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
    BLL_VET_Questionnaire objBLLQuest = new BLL_VET_Questionnaire();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            if (GetSessionUserID() == 0)
                Response.Redirect("~/account/login.aspx");

            if (!IsPostBack)
            {
                if (!string.IsNullOrWhiteSpace(Convert.ToString(Request.QueryString["Question_ID"])))
                {
                    int QuestId = UDFLib.ConvertToInteger(Request.QueryString["Question_ID"].ToString());
                    ViewState["QuestId"] = QuestId.ToString();
                }

                if (!string.IsNullOrEmpty(Convert.ToString(Request.QueryString["Questionnaire_ID"])))
                {
                    int Questionnaire_Id = UDFLib.ConvertToInteger(Request.QueryString["Questionnaire_ID"].ToString());
                    ViewState["Questionnaire_Id"] = Questionnaire_Id.ToString();
                }

                if (!string.IsNullOrWhiteSpace(Convert.ToString(Request.QueryString["Addmode"])))
                {
                    string Mode = Request.QueryString["Addmode"].ToString();
                    ViewState["Mode"] = Mode.ToString();
                    if (ViewState["Mode"].ToString() == "Add")
                    {

                        txtLevl3.Enabled = false;
                        txtLevl4.Enabled = false;
                    }
                }
                if (!string.IsNullOrWhiteSpace(Convert.ToString(Request.QueryString["Section"])))
                {
                    txtSection.Text = Request.QueryString["Section"].ToString();
                }
                if (!string.IsNullOrWhiteSpace(Convert.ToString(Request.QueryString["Level_1"])))
                {
                    txtLevl2.Text = Request.QueryString["Level_1"].ToString();
                    ViewState["txtLevl2"] = txtLevl2.Text;
                }
                if (!string.IsNullOrWhiteSpace(Convert.ToString(Request.QueryString["Level_2"])))
                {
                    txtLevl3.Text = Request.QueryString["Level_2"].ToString();
                    ViewState["txtLevl3"] = txtLevl3.Text;
                }
                if (!string.IsNullOrWhiteSpace(Convert.ToString(Request.QueryString["Level_3"])))
                {
                    txtLevl4.Text = Request.QueryString["Level_3"].ToString();
                    ViewState["txtLevl4"] = txtLevl4.Text;
                }
                if (!string.IsNullOrWhiteSpace(Convert.ToString(Request.QueryString["Question"])))
                {
                    txtQuestion.Text = Request.QueryString["Question"].ToString();
                }
                if (!string.IsNullOrWhiteSpace(Convert.ToString(Request.QueryString["Remarks"])))
                {
                    txtRemarks.Text = Request.QueryString["Remarks"].ToString();
                }
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }

    }

    /// <summary>
    /// Method is used to get login user id
    /// </summary>
    /// <returns>retrun user id</returns>
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }

    /// <summary>
    /// Method is used to insert or update section and question
    /// </summary>
    public void VET_Ins_SectionAndQuestion()
    {
        try
        {


            if (ViewState["Mode"].ToString() == "Add")
            {

                DataTable dtQuestNo = objBLLQuest.VET_Get_ExistQuestionNo(UDFLib.ConvertToInteger(ViewState["Questionnaire_Id"]), UDFLib.ConvertIntegerToNull(txtSection.Text.Trim()), UDFLib.ConvertIntegerToNull(txtLevl2.Text.Trim()), UDFLib.ConvertIntegerToNull(txtLevl3.Text.Trim()), UDFLib.ConvertIntegerToNull(txtLevl4.Text.Trim()),"Add",null);
                if (dtQuestNo.Rows.Count > 0)
                {
                    string jsSqlError3 = "OnLoadEnable();alert('Question number already exist.');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "jsSqlError3", jsSqlError3, true);
                    
                }
                else
                {
                   int res= objBLLQuest.VET_Ins_SectionAndQuestion(UDFLib.ConvertToInteger(ViewState["Questionnaire_Id"].ToString()), lblQuestionText.Text.Trim(), UDFLib.ConvertIntegerToNull(txtSection.Text.Trim()), UDFLib.ConvertIntegerToNull(txtLevl2.Text.Trim()), UDFLib.ConvertIntegerToNull(txtLevl3.Text.Trim()), UDFLib.ConvertIntegerToNull(txtLevl4.Text.Trim()), txtQuestion.Text.Trim(), txtRemarks.Text.Trim(), UDFLib.ConvertToInteger(Session["USERID"].ToString()));
                   if (res > 0)
                   {
                       string jsSqlError2 = "alert('Question saved successfully.');";
                       ScriptManager.RegisterStartupScript(this, this.GetType(), "jsSqlError2", jsSqlError2, true);
                       ClearFields();
                   }
                }
            }
            else if (ViewState["Mode"].ToString() == "Edit")
            {
                DataTable dtQuestNo = objBLLQuest.VET_Get_ExistQuestionNo(UDFLib.ConvertToInteger(ViewState["Questionnaire_Id"]), UDFLib.ConvertIntegerToNull(txtSection.Text.Trim()), UDFLib.ConvertIntegerToNull(txtLevl2.Text.Trim()), UDFLib.ConvertIntegerToNull(txtLevl3.Text.Trim()), UDFLib.ConvertIntegerToNull(txtLevl4.Text.Trim()), "Edit", UDFLib.ConvertToInteger(ViewState["QuestId"].ToString()));
                if (dtQuestNo.Rows.Count > 0)
                {
                    string jsSqlError3 = "OnLoadEnable();alert('Question number already exist.');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "jsSqlError3", jsSqlError3, true);
                    
                }
                else
                {
                    int res = objBLLQuest.VET_Upd_QuestionAndSection(UDFLib.ConvertToInteger(ViewState["QuestId"].ToString()), UDFLib.ConvertIntegerToNull(txtSection.Text.Trim()), UDFLib.ConvertIntegerToNull(txtLevl2.Text.Trim()), UDFLib.ConvertIntegerToNull(txtLevl3.Text.Trim()), UDFLib.ConvertIntegerToNull(txtLevl4.Text.Trim()), txtQuestion.Text.Trim(), txtRemarks.Text.Trim(), Convert.ToInt32(Session["USERID"].ToString()));
                    if (res > 0)
                    {
                        string jsSqlError5 = "alert('Question Updated successfully.');";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "jsSqlError3", jsSqlError5, true);
                        ClearFields();
                        ClearQueryString();
                    }
                }

            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    protected void btnSaveAndAdd_Click(object sender, EventArgs e)
    {
     try
        {

        VET_Ins_SectionAndQuestion();       
       
        string js2 = "parent.UpdateGridQuestionNo(); ";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "hidePopup", js2, true);
        }
     catch (Exception ex)
     {
         UDFLib.WriteExceptionLog(ex);
     }

    }

    /// <summary>
    /// Method is used to clear controls
    /// </summary>
    public void ClearFields()
    {
        txtSection.Text = "";
        txtLevl2.Text = "";
        txtLevl3.Text = "";
        txtLevl4.Text = "";
        txtQuestion.Text = "";
        txtRemarks.Text = "";


    }

    protected void btnSaveAndClose_Click(object sender, EventArgs e)
    {
        try
        {
            VET_Ins_SectionAndQuestion();
            ClearFields();
            string js2 = "parent.UpdatePageQuestion(); ";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "hidePopup", js2, true);
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            string js2 = "parent.UpdatePageQuestion(); ";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "hidePopup", js2, true);
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }
    
    /// <summary>
    /// Method is used to clear query string
    /// </summary>
    private void ClearQueryString()
    {
        try
        {
        PropertyInfo isreadonly = typeof(System.Collections.Specialized.NameValueCollection).GetProperty("IsReadOnly", BindingFlags.Instance | BindingFlags.NonPublic);
        isreadonly.SetValue(this.Request.QueryString, false, null);
        this.Request.QueryString.Set("Addmode", "Add");
        ViewState["Mode"] = Request.QueryString["Addmode"].ToString();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }

    }
}