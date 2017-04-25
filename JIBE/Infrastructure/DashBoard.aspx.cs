using System;
using System.Web;
using System.Web.UI;
using SMS.Business.Infrastructure;
using SMS.Business.Crew;

public partial class Infrastructure_DashBoard : System.Web.UI.Page
{
    /// <summary>
    /// Loading event of page.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            UserAccessValidation();
            if (Session["USERID"] != null)
            {
                hdnUserID.Value = Session["USERID"].ToString();
                hdfUserdepartmentid.Value = Request.QueryString["DepID"] == null ? "0" : Request.QueryString["DepID"].ToString();

                if (Session["USERDEPARTMENTID"] != null)
                {
                    hdnDept.Value = Convert.ToString(Session["USERDEPARTMENTID"]);// Added by Anjali DT:02-07-2016. To fetch logged in user's department for some of snippets like 'Pending NCR' etc.
                }
                hdfUserCompanyID.Value = Convert.ToString(Session["USERCOMPANYID"]);
                if (hdfUserdepartmentid.Value != "0")
                    btnSetDefault.Visible = false;
            }
            else
                Response.Redirect("~/default.aspx?msgid=1");
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    /// <summary>
    /// To check acces for logged in user.
    /// like Add,Edit ,delete access for requested page.
    /// </summary>
    public SMS.Properties.UserAccess objUA = new SMS.Properties.UserAccess();
    protected void UserAccessValidation()
    {
        int CurrentUserID = Convert.ToInt32(Session["userid"]);
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());
        SMS.Business.Infrastructure.BLL_Infra_UserCredentials objUser = new SMS.Business.Infrastructure.BLL_Infra_UserCredentials();

        try
        {
            objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL + hdfUserdepartmentid.Value);
            if (objUA != null)
            {
                if (objUA.Admin == 0)
                {
                    btnSetDefault.Visible = false;
                }
            }

        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

}