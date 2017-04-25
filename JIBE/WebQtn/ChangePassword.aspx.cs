using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLLQuotation;
public partial class ChangePassword : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void ChangePasswordPushButton_Click(object sender, EventArgs e)
    {
        clsQuotationBLL objqt=new clsQuotationBLL();
        int sts = objqt.UpdChangePassword(Session["SuppCode"].ToString(), Changepwd.CurrentPassword, Changepwd.NewPassword);
       if (sts == 1)
       {
           Session["pwd"] = Changepwd.NewPassword.ToString(); 
           lblmsg.Text = "Password changed.";
           Response.Redirect("WebQuotationDetails.aspx");

       }
       else
           lblmsg.Text = "Old password is not matching ";
    }
    protected void CancelPushButton_Click(object sender, EventArgs e)
    {
        if (Session["USERID"] != null && Session["UTYPE"] != null)
        {
            string strUserID = "";
            string strUserType = "";

            strUserID = Session["USERID"].ToString();
            strUserType = Session["UTYPE"].ToString();

            if (strUserType.ToUpper() == "SUPPLIER".ToUpper())
            {
                Response.Redirect("~/webqtn/WebQuotationDetails.aspx");
            }
        }
    }
}
