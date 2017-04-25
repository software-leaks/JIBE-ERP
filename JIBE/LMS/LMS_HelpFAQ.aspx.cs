using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Properties;
using SMS.Business.Infrastructure;

public partial class LMS_LMS_HelpFAQ : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
         
        UserAccessValidation();
        hdnCompanyID.Value = Request.QueryString["USERCOMPANYID"];
        HdnValue.Value = Request.QueryString["USERID"];
        hdnUserName.Value = Request.QueryString["USERNAME"];
        if (Request.QueryString["FAQID"] != null)
        {
            string URL = "Expand(" + Request.QueryString["FAQID"] + ");";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "keyLoad", URL, true);
        }
        else
        {
            string URL1 = "Play1('" + Request.QueryString["src"] + "','" + Request.QueryString["Item_name"] + "');";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "keyLoad", URL1, true);
        }
        
    }

    protected void UserAccessValidation()
    {
        UserAccess objUA = new UserAccess();
        BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();

        int CurrentUserID = UDFLib.ConvertToInteger(Session["UserID"]);
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
        {
            Response.Redirect("~/default.aspx?msgid=1");

        }

    }
}