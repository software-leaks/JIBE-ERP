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


public partial class Surveys_SurveyandCertificateSettings : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (UDFLib.GetSessionUserID() == 0)
            Response.Redirect("~/account/login.aspx?ReturnUrl=" + Request.RawUrl.ToString());
        UserAccessValidation();
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