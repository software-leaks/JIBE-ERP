using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Technical_Worklist_Worklist_Quality : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Server.Transfer("Worklist.aspx?NCR=1");
    }
}