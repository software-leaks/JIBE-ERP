using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using SMS.Business.Infrastructure;
using System.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Services;

public partial class Infrastructure_Menu_UserMenuAssignmentNew : System.Web.UI.Page
{



    protected void Page_Load(object sender, EventArgs e)
    {
        string strConn = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
        try
        {
            if (!IsPostBack)
            {
                UserAccessValidation();
            }

        }
        catch (Exception ex)
        {
            string js = "alert('" + ex.Message + "')";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUser", js, true);
        }

    }

    protected void UserAccessValidation()
    {

        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        SMS.Properties.UserAccess objUA = new SMS.Properties.UserAccess();
        BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();

        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.IsAdmin == 0)
        {
            if (objUA.View == 0)
                Response.Redirect("~/default.aspx?msgid=1");

            if (objUA.Add == 0)
            {
                Response.Redirect("~/default.aspx?msgid=2");
            }
            if (objUA.Edit == 0)
            {
                Response.Redirect("~/default.aspx?msgid=3");
            }
            if (objUA.Delete == 0)
            {
                Response.Redirect("~/default.aspx?msgid=4");
            }
            if (objUA.Approve == 0)
            {
                Response.Redirect("~/default.aspx?msgid=5");
            }
        }

    }
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }




}