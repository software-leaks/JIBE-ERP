using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ChildPopUp : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
         
    }
    protected void btnRefresh_Click(object sender, EventArgs e)
    {

        String script = String.Format("javascript:parent.fnReloadParent();");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "script", script, true);

    }
}