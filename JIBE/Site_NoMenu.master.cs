using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Data.SqlClient;
using SMS.Business.Infrastructure;
using SMS.Properties;

using System.Configuration;

public partial class SiteMaster : System.Web.UI.MasterPage
{
    UserAccess objUA = new UserAccess();
    BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();

    protected void Page_Load(object sender, EventArgs e)
    {
        string APP_NAME = ConfigurationManager.AppSettings["APP_NAME"].ToString();
        DynamicLink.Href = "/" + APP_NAME + "/Styles/" + Convert.ToString(Session["USERSTYLE"]);
        hdfuserDefaultTheme.Value = Convert.ToString(Session["USERSTYLE"]);

        UserAccessValidation();

    }


    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }
    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();
        if (CurrentUserID > 0)
        {
            string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

            objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);
            if (objUA.View == 0 && objUA.Menu_Code > 0)
                Response.Redirect("~/default.aspx?msgid=1");
        }
    }

    


    protected void LogoutMe(object sender, EventArgs e)
    {
        if (Session["USERID"] != null)
        {
            BLL_Infra_UserCredentials objBLL = new BLL_Infra_UserCredentials();
            try
            {
                objBLL.End_Session(int.Parse(Session["USERID"].ToString()));
            }
            catch { }
            finally { objBLL = null; }
        }
        FormsAuthentication.SignOut();
        Session.RemoveAll();
        Session.Abandon();
    }

    protected void ScriptManager1_AsyncPostBackError(object sender, AsyncPostBackErrorEventArgs e)
    {
        string js = "alert('" + e.Exception.Message + "');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "masterpageexpmsg", js, true);
    }
}
