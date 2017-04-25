using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControl_ucJQueryUpload : System.Web.UI.UserControl
{
    public delegate void UploadCompleteCommandEventHandler(object sender, EventArgs e);
    public event UploadCompleteCommandEventHandler UploadCompleted;

    protected void Page_Load(object sender, EventArgs e)
    {

    }    

    public void btnLoadFiles_Click(object s, EventArgs e)
    {
        UploadCompleted(s, e);
    }

    public void Init_InputFileUpload(string UploadPath, string JQueryUploadHandlerWithParamValue)
    {
        string js = "SetParameters('" + UploadPath + "','" + JQueryUploadHandlerWithParamValue + "');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "initjs", js, true);
    }

}