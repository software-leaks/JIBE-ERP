using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Properties;

public partial class Travel_TravelDashBoard : System.Web.UI.Page
{
    UserAccess objUA = new UserAccess();
    protected void Page_Load(object sender, EventArgs e)
    {
        UserAccessValidation();
        if (!IsPostBack)
        {
            string Show_Dashboard = String.Format("Refresh()");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", Show_Dashboard, true);
        }
    }
    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());
        SMS.Business.Infrastructure.BLL_Infra_UserCredentials objUser = new SMS.Business.Infrastructure.BLL_Infra_UserCredentials();
       
        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
            Response.Redirect("~/default.aspx?msgid=1");
        if (objUA.Edit == 0)
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
    protected void btnRefresh_Click(object s, EventArgs e)
    {
        string Show_Dashboard = String.Format("Refresh()");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", Show_Dashboard, true);
    }
}