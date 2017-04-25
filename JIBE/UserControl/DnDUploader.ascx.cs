using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.ComponentModel;
using System.Web.UI.WebControls;

public partial class UserControl_DnDUploader : System.Web.UI.UserControl
{
    public delegate void UploadCompleteCommandEventHandler(string Message, EventArgs e);
    public event UploadCompleteCommandEventHandler UploadCompleted;

    public delegate void UploadFailedCommandEventHandler(string Message, EventArgs e);
    public event UploadFailedCommandEventHandler UploadFailed;

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    public void btnUploadCompleted_Click(object s, EventArgs e)
    {
        try
        {
            string Msg = "Upload Completed";
            UploadCompleted(Msg, e);
        }
        catch { }
    }
    public void btnUploadFailed_Click(object s, EventArgs e)
    {
        try            
        {
            string Msg = "Upload Failed";
            UploadFailed(Msg, e);
        }
        catch { }
    }
    [BindableAttribute(true)]
    public int Worklist_ID
    {
        get { return UDFLib.ConvertToInteger(HiddenField_Worklist_ID.Value); }
        set { HiddenField_Worklist_ID.Value = value.ToString(); }
    }

    [BindableAttribute(true)]
    public int Vessel_ID
    {
        get { return UDFLib.ConvertToInteger(HiddenField_Vessel_ID.Value); }
        set { HiddenField_Vessel_ID.Value = value.ToString(); }
    }

    [BindableAttribute(true)]
    public int WL_Office_ID
    {
        get { return UDFLib.ConvertToInteger(HiddenField_WL_Office_ID.Value); }
        set { HiddenField_WL_Office_ID.Value = value.ToString(); }
    }


    [BindableAttribute(true)]
    public int UserID
    {
        get { return UDFLib.ConvertToInteger(HiddenField_UserID.Value); }
        set { HiddenField_UserID.Value = value.ToString(); }
    }

    [BindableAttribute(true)]
    public string FileUploadPath
    {
        get { return HiddenField_UploadPath.Value; }
        set { HiddenField_UploadPath.Value = value; }
    }
    

    public string UploadedFilename
    {
        get { return HiddenField_FileName.Value; }
    }

    public void InitControl()
    {
        try
        {
            string js = "try{Initialize_dropZone();}catch(ex){}";
            ScriptManager.RegisterStartupScript(this, this.GetType(), DateTime.Now.ToString(), js, true);
        }
        catch { }
    }
}