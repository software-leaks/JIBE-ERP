using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Infrastructure;
using System.Web.Security;

public partial class Account_ChangePassword : System.Web.UI.Page
{
    /// <summary>
    /// Modified by Anjali DT:6-Jun-2016 JIT:9490 
    /// Loading event of page.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["msg"] != null)
            {
                lblMsg.Text = Request.QueryString["msg"].ToString();
            }
        }
        ///Added by Anjali DT:6-Jun-2016 JIT:9490 
        ///To enforce Office user to change password 
        ///1. When Office user not updated his/her password more than 180 days.
        ///2. For all users , if password is default password i.e 1234.
        ///
        Button btnCancel = (Button)ChangeUserPassword.ChangePasswordTemplateContainer.FindControl("CancelPushButton");
        if (Session["UTYPE"] != null && Session["PWD_Last_Updated_InDays"] != null)
        {
            if (Session["UTYPE"].ToString() == "OFFICE USER" && (int)Session["PWD_Last_Updated_InDays"] > 180)
            {

                btnCancel.Visible = false;
            }
            else if (Session["pwd"].ToString() == "1234" || Session["pwd"].ToString() == Convert.ToString((1234 + Convert.ToInt32(Session["USERID"]))))
            {
                btnCancel.Visible = false;
            }
            else
            {
                btnCancel.Visible = true;
            }
        }
    }
    protected void ChangePasswordPushButton_Click(object sender, EventArgs e)
    {
        string js = "";
        try
        {
            if (ChangeUserPassword.NewPassword == "1234")
            {
                js = "alert('Password can not be set to \"1234\"!');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "status", js, true);
            }
            else if (ChangeUserPassword.NewPassword == ChangeUserPassword.CurrentPassword)
            {
                js = "alert('New password should be different from the current password');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "status", js, true);
            }
            else
            {
                BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
                int sts = objUser.Update_UserPassword(Convert.ToInt32(Session["USERID"]), DMS.DES_Encrypt_Decrypt.Encrypt(ChangeUserPassword.CurrentPassword.Trim()), DMS.DES_Encrypt_Decrypt.Encrypt(ChangeUserPassword.NewPassword.Trim()));

                if (sts == 1)
                {
                    FormsAuthentication.SignOut();
                    Session.RemoveAll();
                    Session.Abandon();

                    js = "alert('Password has been changed successfully. Please log-in again.');window.location.href='Login.aspx';";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "status", js, true);
                }
                else if (sts == 0)
                {
                    lblMsg.Text = "Current password is incorrect!";
                }
            }

        }
        catch { }
    }
    protected void CancelPushButton_Click(object sender, EventArgs e)
    {
        if (Session["USERID"] != null && Session["UTYPE"] != null)
        {
            string strUserID = "";
            string strUserType = "";

            strUserID = Session["USERID"].ToString();
            strUserType = Session["UTYPE"].ToString();

            if (strUserType.ToUpper() == "TRAVEL AGENT".ToUpper())
            {
                FormsAuthentication.SetAuthCookie(strUserID, false);
                Response.Redirect("~/travel/RequestListAgent.aspx");
            }
            else
            {
                FormsAuthentication.RedirectFromLoginPage(strUserID, false);
            }
        }
    }
}
