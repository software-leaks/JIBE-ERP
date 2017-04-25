using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Infrastructure;

using System.Data;
using SMS.Business.PURC;

using SMS.Business.Crew;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;

public partial class Infrastructure_DashBoard_CommonNew : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        UserAccessValidation();
        if (Session["USERID"] != null)
        {
            hdnUserID.Value = Session["USERID"].ToString();
            hdfUserdepartmentid.Value = Convert.ToString(Session["USERDEPARTMENTID"]);
            hdfUserCompanyID.Value = Convert.ToString(Session["USERCOMPANYID"]);
        }
        else
            Response.Redirect("~/default.aspx?msgid=1");
    }

    public SMS.Properties.UserAccess objUA = new SMS.Properties.UserAccess();
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


        }
        if (objUA.Approve == 0)
        {

        }
        if (objUA.Delete == 0)
        {


        }


    }
}