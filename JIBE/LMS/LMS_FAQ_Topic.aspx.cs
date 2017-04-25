using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class LMS_LMS_FAQ_Topic : System.Web.UI.Page
{
    public int UserAccess_Edit = 1;
    public int UserAccess_Delete = 1;
    public SMS.Properties.UserAccess objUA = new SMS.Properties.UserAccess();
    protected void Page_Load(object sender, EventArgs e)
    {
        UserAccessValidation();
    }
    protected void UserAccessValidation()
    {
        int CurrentUserID = Convert.ToInt32(Session["userid"]);
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());
        SMS.Business.Infrastructure.BLL_Infra_UserCredentials objUser = new SMS.Business.Infrastructure.BLL_Infra_UserCredentials();

        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
            Response.Redirect("~/default.aspx?msgid=1");

        if (objUA.Add == 0)
        {

        }
        if (objUA.Edit == 0)
        {
            UserAccess_Edit = 0;
        }
        if (objUA.Approve == 0)
        {

        }
        if (objUA.Delete == 0)
        {
            UserAccess_Delete = 0;
        }
        if (objUA.Admin == 0)
        { }
    }
}