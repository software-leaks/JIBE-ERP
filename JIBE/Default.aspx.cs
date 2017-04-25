using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

public partial class _Default : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["USERID"] == null)
            {
                Session.Abandon();
                Response.Redirect("~/Account/Login.aspx");
            }

            if (Request.QueryString["msgid"] != null && Request.QueryString["msgid"] != "")
            {
                int MsgID = int.Parse(Request.QueryString["msgid"]);

                lblMsg.Text = getMessage(MsgID);
            }
            else
            {
                //Response.Redirect("~/Infrastructure/DashBoard_Common.aspx");
                string DefaultURL = ConfigurationManager.AppSettings["DeafaultURL"];
                Response.Redirect(DefaultURL);
            }

        }
    }

    protected string getMessage(int MsgID)
    {
        string msg = "";

        switch (MsgID)
        {
            case 2:
                msg = "You are redirected to this page as some information was missing to access the requested page.";
                break;
            case 1:
                msg = "You are redirected to this page as you don't have sufficient privilege to access the requested page.";
                break;
            default:
                msg = "You are redirected to this page as you don't have sufficient privilege to access the requested page.";
                break;

        }

        return msg;
    }

    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }

    private string getSessionString(string SessionField)
    {
        try
        {
            if (Session[SessionField] != null && Session[SessionField].ToString() != "")
            {
                return Session[SessionField].ToString();
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

        //-- MANNING OFFICE LOGIN --
        if (getSessionString("USERCOMPANYID") != "1")
        {
            pnlCalendar.Visible = false;
            pnlCompInfo.Visible = true;
        }
        else
        {
            pnlCalendar.Visible = true;
            pnlCompInfo.Visible = false;
        }
    }
}
