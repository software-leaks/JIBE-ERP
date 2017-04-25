using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.ComponentModel;
using System.Web.UI.WebControls;

public partial class UserControl_ucUploadOpsWorklistAttachment : System.Web.UI.UserControl
{

    public delegate void UploadCompleteCommandEventHandler();
    public event UploadCompleteCommandEventHandler UploadCompleted;

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


    protected void btnLoadFiles_Click(object Sender, EventArgs e)
    {
        if(UploadCompleted != null)
            UploadCompleted();
    }
    public void InitControl()
    {
        try
        {
            string js = "try{SetParameters();}catch(ex){}";
            ScriptManager.RegisterStartupScript(this, this.GetType(), DateTime.Now.ToString(), js, true);
        }
        catch{}
    }
}