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
using SMS.Business.PortageBill;
using System.Xml;
using System.Xml.Xsl;


public partial class Crew_ContractTemplates : System.Web.UI.Page
{
    BLL_Crew_CrewDetails objBLLCrew = new BLL_Crew_CrewDetails();
    BLL_Infra_VesselLib objVessel = new BLL_Infra_VesselLib();
    BLL_PortageBill objPB = new BLL_PortageBill();
    BLL_Crew_Contract objBLLCrewContract = new BLL_Crew_Contract();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["USERID"] == null)
            Response.Redirect("~/account/Login.aspx");
        
        UserAccessValidation();

        if (!IsPostBack)
        {
            rpt1.DataSource = objBLLCrewContract.Get_ContractTemplateList();
            rpt1.DataBind();

            rptSideLetters.DataSource = objBLLCrew.Get_SideLetter_Template(UDFLib.ConvertToInteger(Session["USERCOMPANYID"].ToString()),0);
            rptSideLetters.DataBind();
 
            txtTemplateBody.config.toolbar = new object[]
            {
                new object[] { "Preview"},
                new object[] {"Source"},
                new object[] { "Cut", "Copy", "Paste", "PasteText", "-", "Print", "SpellChecker", "Scayt" },
                new object[] { "Undo", "Redo", "-", "Find", "Replace", "-", "SelectAll", "RemoveFormat" },
                new object[] { "Bold", "Italic", "Underline", "Strike", "-", "Subscript", "Superscript" },
                new object[] { "NumberedList", "BulletedList", "-", "Outdent", "Indent", "Blockquote" },
                new object[] { "JustifyLeft", "JustifyCenter", "JustifyRight", "JustifyBlock" },
                "/",
                new object[] { "HorizontalRule", "Smiley", "PageBreak"},
                new object[] { "Styles", "Format", "Font", "FontSize" },
                new object[] { "TextColor", "BGColor" },
                new object[] { "Maximize", "ShowBlocks"},
                new object[] { "Image"}
            };
            txtTemplateBody.Height = 600;
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

    protected void btnSaveTemplate_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtTemplateName.Text.Trim() == "")
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
                objBLLCrew.Save_Contract_Template(UDFLib.ConvertToInteger(hdnContractId.Value), txtTemplateName.Text.Trim(), txtTemplateBody.Text, GetSessionUserID());
                string js = "alert('Contract Template Saved.');";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "SUCCESS", js, true);
            }
        }
        catch (Exception ex)
        {
            string js = "alert('Error saving contract template" + ex.Message.Replace("'", "") + "');window.close();";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "mailerror", js, true);
        }
    }
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
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

    protected void lnkEdit_Click(object sender, EventArgs e)
    {
        pnlContractTemplate.Visible = true;
        pnlSideletter.Visible = false;
        int UserCompanyID = UDFLib.ConvertToInteger(Session["USERCOMPANYID"].ToString());
        string arg = ((LinkButton)(sender)).CommandArgument.ToString();
        if (((LinkButton)(sender)).Text.ToString() == "ADD")
        {
            txtTemplateName.Enabled = true;
            txtTemplateName.Text = "";
        }
        else
        {
            txtTemplateName.Enabled = false;
        }
        int ContractId = UDFLib.ConvertToInteger(arg);
        hdnContractId.Value = ContractId.ToString();
        txtTemplateBody.Text = Load_ContractTemplate(ContractId);
    }

    protected string Load_ContractTemplate(int ContractId)
    {
        string TemplateText = "";

        DataTable dt =  objBLLCrewContract.Get_ContractTemplate(ContractId);
        if (dt.Rows.Count > 0)
        {
            txtTemplateName.Text = dt.Rows[0]["Template_Name"].ToString(); ;
            TemplateText = dt.Rows[0]["template_text"].ToString();
            TemplateText = TemplateText.Replace("<p></p>", "");
        }
        else
        {
            string filePath = Server.MapPath("template.txt");
            StreamReader streamReader = new StreamReader(filePath);
            string text = streamReader.ReadToEnd();
            streamReader.Close();

            DataTable dtTemplate = objBLLCrewContract.Get_ContractList(ContractId);
            txtTemplateName.Text = dtTemplate.Rows[0]["Contract_Name"].ToString();

            objBLLCrew.Save_Contract_Template(ContractId, txtTemplateName.Text, text, GetSessionUserID());
            TemplateText = Load_ContractTemplate(ContractId);
        }
        return TemplateText;
    }

    protected string Load_SideLetter_Template(int TemplateID, int UserCompanyID)
    {
        string TemplateText = "";        

        DataTable dt = objBLLCrew.Get_SideLetter_Template(UserCompanyID, TemplateID);
        if (dt.Rows.Count > 0)
        {
            TemplateText = dt.Rows[0]["template_text"].ToString();
            TemplateText = TemplateText.Replace("<p></p>", "");
        }
        else
        {
            //string filePath = Server.MapPath("template.txt");
            //StreamReader streamReader = new StreamReader(filePath);
            //string text = streamReader.ReadToEnd();
            //streamReader.Close();
            //objBLLCrew.Save_Sideletter_Template(TemplateID, txtTemplateName.Text, text, GetSessionUserID());

        }
        return TemplateText;
    }

    protected void lnkEditSideLetter_Click(object sender, EventArgs e)
    {
        pnlContractTemplate.Visible = false;
        pnlSideletter.Visible = true;
        
        int TemplateID = UDFLib.ConvertToInteger(((LinkButton)(sender)).CommandArgument.ToString().Split(',')[0]);
        string Template_Name = ((LinkButton)(sender)).CommandArgument.ToString().Split(',')[1];

        int UserCompanyID = UDFLib.ConvertToInteger(Session["USERCOMPANYID"].ToString());
        
        hdnSideletterID.Value = TemplateID.ToString();
        txtSideletter.Text = Template_Name;

        txtTemplateBody.Text = Load_SideLetter_Template(TemplateID, UserCompanyID);        
    }

    protected void btnSaveSideletterTemplate_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtSideletter.Text == "")
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

                objBLLCrew.Save_Sideletter_Template(UDFLib.ConvertToInteger(hdnSideletterID.Value), txtSideletter.Text, txtTemplateBody.Text, GetSessionUserID());

                string js = "alert('Side Letter Template Saved.');";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "SUCCESS1", js, true);

            }
        }
        catch (Exception ex)
        {
            string js = "alert('Error saving Side Letter template" + ex.Message.Replace("'", "") + "');window.close();";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "mailerror1", js, true);
        }
    }
    protected void ImgAttDelete_Click(object sender, CommandEventArgs e)
    {
        string[] cmdargs = e.CommandArgument.ToString().Split(',');
    }
}

