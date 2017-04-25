using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class MyMessageBox : System.Web.UI.UserControl
{
    #region Properties
    public bool ShowCloseButton { get; set; }
    
    #endregion

    #region Load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (ShowCloseButton)
            CloseButton.Attributes.Add("onclick", "document.getElementById('" + MessageBox.ClientID + "').style.display = 'none'");
    }
    #endregion

    #region Wrapper methods
    public void ShowError(string message)
    {
        Show(MessageType.Error, message);
    }

    public void ShowInfo(string message)
    {
        Show(MessageType.Info, message);
    }

    public void ShowSuccess(string message)
    {
        Show(MessageType.Success, message);
    }

    public void ShowWarning(string message)
    {
        Show(MessageType.Warning, message);
    } 
    #endregion

    #region Show control
    public void Show(MessageType messageType, string message)
    {
        CloseButton.Visible = ShowCloseButton;
        litMessage.Text = message;

        MessageBox.CssClass = messageType.ToString().ToLower();
        this.Visible = true;
    } 
    #endregion

    #region Enum
    public enum MessageType
    {
        Error = 1,
        Info = 2,
        Success = 3,
        Warning = 4
    } 
    #endregion
}

