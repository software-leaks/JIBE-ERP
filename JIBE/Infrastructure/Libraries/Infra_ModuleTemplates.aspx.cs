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


public partial class Infra_ModuleTemplates : System.Web.UI.Page
{
  
  //  BLL_Infra_Lib_ModuleTemp objBLLModuleTemplate = new BLL_Infra_Lib_ModuleTemp();

    public string OperationMode = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["USERID"] == null)
            Response.Redirect("~/account/Login.aspx");

        UserAccessValidation();

        if (!IsPostBack)
        {
            rpt1.DataSource = BLL_Infra_Lib_ModuleTemp.Get_ModuleList();
            rpt1.DataBind();
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
                if (MainView.ActiveViewIndex == 0)
                {
                    BLL_Infra_Lib_ModuleTemp.Save_Module_Template(UDFLib.ConvertToInteger(hdnModuleTypeID.Value), txtTemplateName.Text.Trim(), txtTemplateBody.Text, GetSessionUserID(), 0);
                    string js = "alert('Module Template Saved.');";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "SUCCESS", js, true);
                }
                if (MainView.ActiveViewIndex == 1)
                {
                    BLL_Infra_Lib_ModuleTemp.Save_Module_Template(UDFLib.ConvertToInteger(hdnModuleTypeID.Value), txtTemplateName.Text.Trim(), txtTemplateBody.Text, GetSessionUserID(), 1);
                    string js = "alert('Module Template Saved.');";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "SUCCESS", js, true);
                }
                if (MainView.ActiveViewIndex == 2)
                {
                    BLL_Infra_Lib_ModuleTemp.Save_Module_Template(UDFLib.ConvertToInteger(hdnModuleTypeID.Value), txtTemplateName.Text.Trim(), txtTemplateBody.Text, GetSessionUserID(), 2);
                    string js = "alert('Module Template Saved.');";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "SUCCESS", js, true);
                }

            }
        }
        catch (Exception ex)
        {
            string js = "alert('Error saving module template" + ex.Message.Replace("'", "") + "');window.close();";
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
                MainView.ActiveViewIndex = -1;
        pnlModuleTemplate.Visible = true;
        int UserCompanyID = UDFLib.ConvertToInteger(Session["USERCOMPANYID"].ToString());
        string[] arg = new string[2];
        arg = ((LinkButton)(sender)).CommandArgument.ToString().Split(';');
        int ModuletypeId = UDFLib.ConvertToInteger(((LinkButton)(sender)).CommandArgument.ToString().Split(',')[0]);
        int TemplateID = UDFLib.ConvertToInteger(((LinkButton)(sender)).CommandArgument.ToString().Split(',')[1]);

        if (((LinkButton)(sender)).Text.ToString() == "ADD")
        {
            txtTemplateName.Enabled = true;
            txtTemplateName.Text = "";
        }
        else
        {
            txtTemplateName.Enabled = false;
        }
       
        hdnModuleTypeID.Value = ModuletypeId.ToString();
        txtTemplateBody.Text = Load_ModuleTemplate(ModuletypeId);
        
    }

    protected string Load_ModuleTemplate(int ModuletypeId)
    {
        string TemplateText = "";
        ImageButton1.Visible = true;
        btnModuleEdit.Visible = false;
        txtModuleName.Enabled = false;
        DataTable dt = BLL_Infra_Lib_ModuleTemp.Get_ModuleTemplate(ModuletypeId);
        DataTable dtTemplate = BLL_Infra_Lib_ModuleTemp.Get_ModuleList(ModuletypeId);
        if (dt.Rows.Count > 0)
        {
            if (MainView.ActiveViewIndex == 0)
            {
                txtTemplateName.Text = dt.Rows[0]["Template_Name"].ToString(); ;
                TemplateText = dt.Rows[0]["template_text"].ToString();
                TemplateText = TemplateText.Replace("<p></p>", "");
            }
            if (MainView.ActiveViewIndex == 1)
            {
                txtTemplateName.Text = dt.Rows[0]["Template_Name"].ToString(); ;
                TemplateText = dt.Rows[0]["HeaderText"].ToString();
                TemplateText = TemplateText.Replace("<p></p>", "");
            }
            if (MainView.ActiveViewIndex == 2)
            {
                txtTemplateName.Text = dt.Rows[0]["Template_Name"].ToString(); ;
                TemplateText = dt.Rows[0]["FooterText"].ToString();
                TemplateText = TemplateText.Replace("<p></p>", "");
            }
            if (MainView.ActiveViewIndex == -1)// This will set for default
            {                
                txtTemplateName.Text = dt.Rows[0]["Template_Name"].ToString(); ;
                TemplateText = TemplateText = dt.Rows[0]["HeaderText"].ToString() + dt.Rows[0]["template_text"].ToString() + dt.Rows[0]["FooterText"].ToString();
                TemplateText = TemplateText.Replace("<p></p>", "");
            }
            txtModuleName.Text = dtTemplate.Rows[0]["ModuleTypeName"].ToString();
           // txtModuleName.Enabled = true;

        }
        else
        {
           
          
            txtTemplateName.Text = dtTemplate.Rows[0]["ModuleTypeName"].ToString();
            txtModuleName.Text = dtTemplate.Rows[0]["ModuleTypeName"].ToString();
           
        }
        return TemplateText;
    }
    


    protected void ImgAttDelete_Click(object sender, CommandEventArgs e)
    {
        string[] cmdargs = e.CommandArgument.ToString().Split(',');
    }
    protected void Tab1_Click(object sender, EventArgs e)
    {
        Tab1.CssClass = "Clicked";
        Tab2.CssClass = "Initial";
        Tab3.CssClass = "Initial";
        MainView.ActiveViewIndex = 0;
        // int ModuleID = UDFLib.ConvertToInteger(hdnModuleTypeID);
        txtTemplateBody.Text = Load_ModuleTemplate(UDFLib.ConvertToInteger(hdnModuleTypeID.Value));
    }
    protected void Tab2_Click(object sender, EventArgs e)
    {
        Tab1.CssClass = "Initial";
        Tab2.CssClass = "Clicked";
        Tab3.CssClass = "Initial";
        MainView.ActiveViewIndex = 1;
        // int ModuleID = UDFLib.ConvertToInteger(hdnModuleTypeID);
        txtTemplateBody.Text = Load_ModuleTemplate(UDFLib.ConvertToInteger(hdnModuleTypeID.Value));
    }
    protected void Tab3_Click(object sender, EventArgs e)
    {
        Tab1.CssClass = "Initial";
        Tab2.CssClass = "Initial";
        Tab3.CssClass = "Clicked";
        MainView.ActiveViewIndex = 2;
        // int ModuleID = UDFLib.ConvertToInteger(hdnModuleTypeID.Value);
        txtTemplateBody.Text = Load_ModuleTemplate(UDFLib.ConvertToInteger(hdnModuleTypeID.Value));
    }
    protected void ClearField()
    {
        txtModule.Text = "";
    }
    protected void ImgAdd_Click(object sender, ImageClickEventArgs e)
    {
        this.SetFocus("ctl00_MainContent_txtModule");
        HiddenFlag.Value = "Add";
        OperationMode = "Add New Module";

        ClearField();

        string AddModule = String.Format("showModal('divadd',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddModule", AddModule, true);
    }
    protected void btnsaveModule_Click(object sender, EventArgs e)
    {
        if (HiddenFlag.Value == "Add")
        {
            int retval = BLL_Infra_Lib_ModuleTemp.Save_Module(txtModule.Text, Convert.ToInt32(Session["USERID"]));
        }

        rpt1.DataSource = BLL_Infra_Lib_ModuleTemp.Get_ModuleList();
        rpt1.DataBind();

        string hidemodal = String.Format("hideModal('divadd')");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hidemodal", hidemodal, true);
    }
    protected void btnModuleEditSave_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (txtModuleName.Text.Trim() == "")
            {
                string js = "alert('Please Enter Module name');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "errorMail", js, true);
                return;
            }
         
            else
            {
                BLL_Infra_Lib_ModuleTemp.Update_ModuleName(UDFLib.ConvertToInteger(hdnModuleTypeID.Value), txtModuleName.Text.Trim(), GetSessionUserID());
                    string js = "alert('Module Updated..!.');";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "SUCCESS", js, true);
                    ImageButton1.Visible = true ;
                    btnModuleEdit.Visible = false;
                    txtModuleName.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            string js = "alert('Error editing module" + ex.Message.Replace("'", "") + "');window.close();";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "mailerror", js, true);
        }
    }
    protected void ImgDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (txtModuleName.Text.Trim() == "")
            {
                string js = "alert('Please Enter Module name');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "errorMail", js, true);
                return;
            }

            else
            {
                BLL_Infra_Lib_ModuleTemp.DELETE_Module(UDFLib.ConvertToInteger(hdnModuleTypeID.Value), txtModuleName.Text.Trim(), GetSessionUserID());
                string js = "alert('Module Deleted.');";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "SUCCESS", js, true);

                rpt1.DataSource = BLL_Infra_Lib_ModuleTemp.Get_ModuleList();
                rpt1.DataBind();

            }
        }
        catch (Exception ex)
        {
            string js = "alert('Error Deleting module" + ex.Message.Replace("'", "") + "');window.close();";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "mailerror", js, true);
        }
    }
    protected void btnModuleEdit_Click(object sender, ImageClickEventArgs e)
    {
        txtModuleName.Enabled = true;
        btnModuleEdit.Visible = true;
        ImageButton1.Visible = false;

    }

}

