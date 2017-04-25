using System;
using System.Web.UI.WebControls;
using AjaxControlToolkit.HTMLEditor;
using System.Text.RegularExpressions;

public partial class UserControlEditor : System.Web.UI.UserControl
{

    public event EventHandler FileUploadEvent;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
              
        }
    }

    public ActiveModeType ActiveMode
    {
        set { ucCustomEditor.ActiveMode = value; }
        get { return ucCustomEditor.ActiveMode; }
    }

    public bool PrevMode
    {
        set { ucCustomEditor.PrevMode = value; }        
    }
    public bool DesgMode
    {
        set { ucCustomEditor.DesgMode = value; }
    }
    public bool HTMLMode
    {
        set { ucCustomEditor.HTMLMode = value; }
    }
    public bool PictureButton
    {
        set { ucCustomEditor.PictureButton = value; }
    }
    public Unit Height
    {
        set { ucCustomEditor.Height = value; }
    }
    public Unit Width
    {
        set { ucCustomEditor.Width = value; }
    }
    public string Content
    {
        set { ucCustomEditor.Content = value; }
        get { return ucCustomEditor.Content; }
    }
    protected void OnFileUploadEvent(EventArgs e)
    {      
        if (FileUploadEvent != null)
            FileUploadEvent(this, e);
    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {   
        OnFileUploadEvent(e);        
    }

    public string TextOnly
    {
        get
        {            
            return Regex.Replace(ucCustomEditor.Content, @"<(.|\n)*?>", string.Empty);
        }        
    }
}

