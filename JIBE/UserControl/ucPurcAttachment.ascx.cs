using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ucPurcAttachment : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {


    }


    public string ReqsnNumber
    {
        get { return HiddenField_Reqsn.Value; }
        set { HiddenField_Reqsn.Value = value; }
    }

    public string VesselID
    {
        get { return HiddenField_VesselID.Value; }
        set { HiddenField_VesselID.Value = value; }
    }

    public string UserID
    {
        get { return HiddenField_UserID.Value; }
        set { HiddenField_UserID.Value = value; }
    }

    public string FileUploadPath
    {
        get { return HiddenField_DocumentUploadPath.Value; }
        set { HiddenField_DocumentUploadPath.Value = value; }
    }


    public string SuppCode
    {
        get { return HiddenField_SuppCode.Value; }
        set { HiddenField_SuppCode.Value = value; }
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

