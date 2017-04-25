using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Globalization;
using SMS.Business.INFRA;
using SMS.Business.Infrastructure;
using SMS.Business.Technical;
using SMS.Properties;
using System.Drawing;

public partial class Technical_Worklist_SupInspCalendar : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (UDFLib.GetSessionUserID() == 0)
            Response.Redirect("~/account/login.aspx?ReturnUrl="+Request.RawUrl.ToString());
        
        UserAccessValidation();

        hfcompanyid.Value = Session["USERCOMPANYID"].ToString();
        if (Session["COMPANYTYPE"].ToString().ToUpper() != "SURVEYOR")
        {
            lblFleet.Visible = false;
            ddlVessel_Manager.Visible = false;
            ddlVessel_Manager.Visible = false;
        }
        if (IsPostBack)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ddd", "LastLoad();", true);
        }
        else
        {
            //int UserCompanyID = UDFLib.ConvertToInteger(Session["USERCOMPANYID"].ToString());
            ddlVessel_Manager.DataSource = new BLL_Infra_Company().Get_Company_Parent_Child(1, 0, 0).Tables[0];
            ddlVessel_Manager.DataTextField = "COMPANY_NAME";
            ddlVessel_Manager.DataValueField = "ID";
            ddlVessel_Manager.DataBind();
            ddlVessel_Manager.Items.Insert(0, new ListItem("-Select All-", "0"));
        }
    }

    /// <summary>
    /// Check Access rights
    /// </summary>
    protected void UserAccessValidation()
    {
        if (new BLL_Infra_UserCredentials().Get_UserAccessForPage(UDFLib.GetSessionUserID(), UDFLib.GetPageURL(Request.Path.ToUpper())).View == 0)
            Response.Redirect("~/crew/default.aspx?msgid=1");
    }
}