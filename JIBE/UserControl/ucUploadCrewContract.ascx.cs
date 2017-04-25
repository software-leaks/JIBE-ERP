using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.ComponentModel;
using System.Web.UI.WebControls;

public partial class UserControl_ucUploadCrewContract : System.Web.UI.UserControl
{

    public delegate void UploadCompleteCommandEventHandler();
    public event UploadCompleteCommandEventHandler UploadCompleted;

    [BindableAttribute(true)]
    public int CrewID
    {
        get { return UDFLib.ConvertToInteger(HiddenField_CrewID.Value); }
        set { HiddenField_CrewID.Value = value.ToString(); }
    }

    [BindableAttribute(true)]
    public int VoyageID
    {
        get { return UDFLib.ConvertToInteger(HiddenField_VoyageID.Value); }
        set { HiddenField_VoyageID.Value = value.ToString(); }
    }

    [BindableAttribute(true)]
    public int UserID
    {
        get { return UDFLib.ConvertToInteger(HiddenField_UserID.Value); }
        set { HiddenField_UserID.Value = value.ToString(); }
    }

    //[BindableAttribute(true)]
    //public int DocTypeID
    //{
    //    get { return UDFLib.ConvertToInteger(HiddenField_StageID.Value); }
    //    set { HiddenField_StageID.Value = value.ToString(); }
    //}

    [BindableAttribute(true)]
    public int StageID
    {
        get { return UDFLib.ConvertToInteger(HiddenField_StageID.Value); }
        set { HiddenField_StageID.Value = value.ToString(); }
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