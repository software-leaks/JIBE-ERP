using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ucFBMAttachment : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }



    public void btnLoadFiles_Click(object s, EventArgs e)
    {
        UploadCompleted(s, e);
    }


    public delegate void UploadCompleteCommandEventHandler(object sender, EventArgs e);

    public event UploadCompleteCommandEventHandler UploadCompleted;

    public void Register_JS_Attach()
    {
        string js = "SetParameters();";
        ScriptManager.RegisterStartupScript(this, this.GetType(), DateTime.Now.ToString(), js, true);
    }



}