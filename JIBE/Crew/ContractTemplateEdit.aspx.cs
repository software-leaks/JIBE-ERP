using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.Crew;
using SMS.Business.Infrastructure;
using System.IO;

public partial class ContractTemplateEdit : System.Web.UI.Page
{

    BLL_Crew_CrewDetails objBLLCrew = new BLL_Crew_CrewDetails();
    BLL_Infra_VesselLib objVessel = new BLL_Infra_VesselLib();


    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["USERID"] == null)
            Response.Redirect("~/account/Login.aspx");
        
        UserAccessValidation();

        if (!IsPostBack)
        {
            int Vessel_Flag = UDFLib.ConvertToInteger(getQueryString("flag"));
            int UserCompanyID = UDFLib.ConvertToInteger(Session["USERCOMPANYID"].ToString());

            DataTable dtFlag = objVessel.Get_VesselFlagDetails(Vessel_Flag , UserCompanyID);

            if (dtFlag.Rows.Count > 0)
            {
                txtTemplateName.Text = dtFlag.Rows[0]["flag_name"].ToString();
            }

            txtTemplateBody.Height = 445;

            //DataTable dt = objBLLCrew.Get_ContractTemplate(Vessel_Flag, UserCompanyID);

            // Updated Method by passing ContractId
            int ContractId = 0;
            DataTable dt = objBLLCrew.Get_ContractTemplate(ContractId);
            if (dt.Rows.Count > 0)
            {
                txtTemplateBody.Text = dt.Rows[0]["template_text"].ToString();
            }
            else
            {
                string filePath = Server.MapPath("template.txt");
                StreamReader streamReader = new StreamReader(filePath);
                string text = streamReader.ReadToEnd();
                streamReader.Close();

                txtTemplateBody.Text = text;
            }
        }
    }
    private string getQueryString(string QueryField)
    {
        try
        {
            if (Request.QueryString[QueryField] != null && Request.QueryString[QueryField].ToString() != "")
            {
                return Request.QueryString[QueryField].ToString();
            }
            else
                return "";
        }
        catch
        {
            return "";
        }
    }

    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        SMS.Properties.UserAccess objUA = new SMS.Properties.UserAccess();
        BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
            Response.Redirect("~/default.aspx?msgid=1");

        if (objUA.Add == 0)
        {
        }

        if (objUA.Edit == 0)
        {
        }
        if (objUA.Delete == 0)
        {
        }
        if (objUA.Approve == 0)
        {
        }
    }
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }

    protected void btnSaveTemplate_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtTemplateName.Text == "")
            {
                string js = "alert('Enter template name');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "errorMail", js, true);
                return;
            }
            else if (txtTemplateBody.Text == "")
            {
                string js = "alert('Enter template body');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "errorMail", js, true);
                return;
            }
            else
            {

                objBLLCrew.Save_Contract_Template(UDFLib.ConvertToInteger(getQueryString("flag")), txtTemplateName.Text, txtTemplateBody.Text, GetSessionUserID());
                
                string js = "alert('Contract Template Saved.');window.open('','_self','');window.close();";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "SUCCESS", js, true);
                
            }
        }
        catch (Exception ex)
        {
            string js = "alert('Error saving contract template" + ex.Message.Replace("'", "") + "');window.close();";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "mailerror", js, true);
        }
    }
}