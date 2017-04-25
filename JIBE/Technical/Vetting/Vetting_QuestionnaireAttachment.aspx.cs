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
using AjaxControlToolkit4;
using System.IO;

public partial class Technical_Vetting_Vetting_QuestionnaireAttachment : System.Web.UI.Page
{
    BLL_VET_Questionnaire objBLLQuest = new BLL_VET_Questionnaire();
    UserAccess objUA = new UserAccess();
    BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
  
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (GetSessionUserID() == 0)
                Response.Redirect("~/account/login.aspx");

            if (!IsPostBack)
            {
                if (Request.QueryString["Questionnaire_ID"] != null)
                {
                    string Questionnaire_Id = Request.QueryString["Questionnaire_ID"].ToString();
                    ViewState["Questionnaire_Id"] = Questionnaire_Id;
                }
                VET_Get_QuestionnaireAttachment();
            }
        }
        
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }

    }

    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }
   
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        try
        {
            if (FileUpload1.HasFile)
            {
                var FileExtension = "." + Path.GetExtension(FileUpload1.PostedFile.FileName).Substring(1);
                string sPath = Server.MapPath("\\" + System.Configuration.ConfigurationManager.AppSettings["APP_NAME"].ToString() + "\\uploads\\Vetting\\VetQAtt\\");
                Guid GUID = Guid.NewGuid();
                string AttachPath = "VET_" + GUID.ToString() + FileExtension;
                FileUpload1.SaveAs(sPath + AttachPath);

                objBLLQuest.VET_Ins_QuestionnaireAttachment(UDFLib.ConvertToInteger(ViewState["Questionnaire_Id"].ToString()), Path.GetFileName(FileUpload1.PostedFile.FileName), AttachPath, Convert.ToInt32(Session["USERID"].ToString()));
                string jsSqlError3 = "alert('Attachment saved.');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "jsSqlError3", jsSqlError3, true);
                VET_Get_QuestionnaireAttachment();
           }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }

    /// <summary>
    /// Bind uploaded questionnire attachment to gridview
    /// </summary>
    public void VET_Get_QuestionnaireAttachment()
    {
        try
        {
            DataTable dt = objBLLQuest.VET_Get_QuestionnaireAttachment(UDFLib.ConvertToInteger(ViewState["Questionnaire_Id"].ToString()));
            DataView dvImage = dt.DefaultView;


            gvAttachment.DataSource = dt.DefaultView;
            gvAttachment.DataBind();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }   
}