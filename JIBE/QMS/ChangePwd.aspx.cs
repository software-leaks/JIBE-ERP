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


public partial class ChangePwd : System.Web.UI.Page
{
    BLL_QMS_Document objQMS = new BLL_QMS_Document(); 

    protected void Page_Load(object sender, EventArgs e)
    {
      
    }

    protected void ChangePassword2_ChangedPassword(object sender, EventArgs e)
    {
    }
    protected void ChangePasswordPushButton_Click(object sender, EventArgs e)
    {
        objQMS.UpdatePwd(Convert.ToInt32(Session["USERID"]), ChangePassword2.CurrentPassword.ToString(), ChangePassword2.NewPassword.ToString());

        String msg = String.Format("myMessage('Password sucessfully changed.')");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg, true);

    }
    protected void CancelPushButton_Click(object sender, EventArgs e)
    {
       
    }
}
