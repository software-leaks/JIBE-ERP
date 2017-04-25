using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.Infrastructure;

public partial class Account_OCA : System.Web.UI.Page
{
    BLL_Infra_UserCredentials objBLL = new BLL_Infra_UserCredentials();
    /// <summary>
    /// Modified By Alok - 19/10/2016
    /// Change Query string to PKID to Session ID
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {

        String str = Request.QueryString["PageName"].ToString();
        string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority;
        Response.Redirect(baseUrl + "/" + System.Configuration.ConfigurationManager.AppSettings["OCA_APP_NAME"].ToString() + "?SessionID=" + Session["Session"] + "&PageName=" + str);
        //string OCA_URL = baseUrl + "/" + System.Configuration.ConfigurationManager.AppSettings["OCA_APP_NAME"].ToString() + "?SessionID=" + Session["Session"] + "&PageName=" + str;
        //Response.Write(baseUrl + "/" + System.Configuration.ConfigurationManager.AppSettings["OCA_APP_NAME"].ToString() + "?SessionID=" + Session["Session"] + "&PageName=" + str);
       // ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + OCA_URL + "');", true);
        //ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "javascript:window.open('" + OCA_URL + "');", true);
    }
    private int GetSessionID()
    {
        if (Session["Session"] != null)
            return int.Parse(Session["Session"].ToString());
        else
            return 0;
    }
}