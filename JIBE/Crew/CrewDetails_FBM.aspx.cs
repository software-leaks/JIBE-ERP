using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Infrastructure;
using SMS.Properties;

public partial class Crew_CrewDetails_FBM : System.Web.UI.Page
{
    BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
    UserAccess objUA = new UserAccess();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        UserAccessValidation();    
    }
    protected void UserAccessValidation()
    {
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());
        objUA = objUser.Get_UserAccessForPage(Convert.ToInt32(Session["USERID"]), PageURL);     
        if (objUA.View == 0)
        {
            lblMsg.Text = "You don't have sufficient privilege to access the requested information.";
            pnlViewFBMInfo.Visible = false;
        }
    }


}