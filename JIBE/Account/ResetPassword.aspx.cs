using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Infrastructure;
using SMS.Data;
using System.Web.Security;

public partial class ResetPassword : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
      
        lblresetMessage.Text = "";
    }

    public void btnResetpassword_Click(object s, EventArgs e)
    {
        BLL_Infra_UserCredentials objBLL = new BLL_Infra_UserCredentials();
        string tempPwd = Membership.GeneratePassword(6,1);
        if (objBLL.UPD_Reset_Password(txtResetEmailid.Text.Trim(), DMS.DES_Encrypt_Decrypt.Encrypt(tempPwd), tempPwd) == 1)
        {
            lblresetMessage.Text = "Your password has been reset successfully. <br><br> An email with a temporary password will be sent shortly. ";
            btnResetpassword.Enabled = false;

        }
        else
            lblresetMessage.Text = "No account found with that email address. <br><br> Please enter your email address correctly or contact the jibe support at : support@jibe.com.sg";

        
    }

    public void btnclosediv_Click(object s, EventArgs e)
    {
        Response.Redirect("~/Account/login.aspx");
        txtResetEmailid.Text = "";
        lblresetMessage.Text = "";
    }
}