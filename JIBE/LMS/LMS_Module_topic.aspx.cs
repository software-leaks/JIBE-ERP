using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.LMS;
using SMS.Properties;
using SMS.Business.Infrastructure;
using System.Web.UI.HtmlControls;
using System.IO;

public partial class LMS_LMS_Module_topic : System.Web.UI.Page
{
    public int UserAccess_Edit = 1;
    protected void Page_Load(object sender, EventArgs e)
    {
        UserAccessValidation();
        if (UDFLib.ConvertToInteger(Session["USERID"]) == 0)
            Response.Redirect("../Account/Login.aspx");


        if (!IsPostBack)
        {
            HdnValue.Value = Session["USERID"].ToString();
            //string URL = "asyncBindFAQList();";
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "keyLoad", URL, true);
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
        if (objUA.Add == 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "disableAdd", "$('#btnAddNewFAQ').hide();", true);
        }
        if (objUA.Edit == 0)
        {
            UserAccess_Edit = 0;
        }

    }
   
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        
              
    }
}


