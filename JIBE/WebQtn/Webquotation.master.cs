using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using BLLQuotation;

public partial class WebQuotation_Webquotation : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
             lbluname.Text = (string)Session["SuppName"].ToString();
            
        }

    }
    protected void logoutmed(object sender, EventArgs e)
    {
    
        FormsAuthentication.SignOut();
        Session.RemoveAll();
        Response.Redirect("webqtn/SupplierLogin.aspx");  
         
       
    }
}
