using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using SMS.Business.QMS;

public partial class ChangePassword : System.Web.UI.Page
{
    BLL_QMS_Document objQMS = new BLL_QMS_Document();

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void ChangePassWord_Click(object sender, EventArgs e)
    {

        String msg = "";
        dvMessage.Text = "";
        if (newpassword.Text.Trim() != "")
        {
            if (confirmnewpassword.Text.Trim() == newpassword.Text.Trim())
            {
                objQMS.UpdatePwd(Convert.ToInt32(Session["USERID"]), currentpassword.Text.Trim(), newpassword.Text.Trim());
                msg = String.Format("myMessage('Password sucessfully changed.')");
            }
            else
                msg = String.Format("myMessage('New password & confrim password should be same.')");
        }
        else
            msg = String.Format("myMessage('Please enter the existing your password.')");
      
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg, true);
    }
}
