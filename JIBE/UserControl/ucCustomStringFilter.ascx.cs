using System;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

// DEPENDENCIES : JSCustomFilterControls.js,CssCustomFilterControls.css,jquery-1.5.2.min.js or above,UserDefinedFunction.cs

public partial class ucCustomStringFilter : System.Web.UI.UserControl
{
    public delegate void ApplySearchEventHandler();

    public event ApplySearchEventHandler ApplySearch;

    #region ------------- Property----------------



    public string FilterText
    {
        get { return txtSearchItems.Text; }
        set { txtSearchItems.Text = value; }
    }

    public string FilterType
    {
        get { return ListBoxItems.SelectedValue == "nofilter" ? null : ListBoxItems.SelectedValue; }
    }

    public int Width
    {
        set { pnlListSection.Width = value; pnlsearchSection.Width = value; plnStringFilter.Width = (Convert.ToInt32(value) - 6); }
    }

    public int Height
    {
        set
        {
            if (UDFLib.ConvertToInteger(value) > 0)
                plnStringFilter.Height = value;
        }
    }

    public string CssClass
    {
        get { return hdf_CssClass.Value; }
        set { hdf_CssClass.Value = value; }
    }

    public string Style
    {
        get { return hdf_Style_Style.Value; }
        set { hdf_Style_Style.Value = value; }

    }



    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {

        txtSearchItems.Focus();
        txtSearchItems.Attributes.Add("onkeydown", "clearSelectionOnTextChanged('" + ListBoxItems.ClientID + "',event)");

    }

    #region------------  Metods-------------




    public void Select(string sValue)
    {
        ListItem liitem = ListBoxItems.Items.FindByValue(sValue);
        if (liitem != null)
            liitem.Selected = true;

    }

    #endregion

    protected void ListBoxItems_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ListBoxItems.SelectedValue == "nofilter")
        {
            txtSearchItems.Text = "";

        }
        string strjFunClose = String.Format("HideCustomFilterUserControl()");
        ScriptManager.RegisterStartupScript(this, this.GetType(), "close" + this.ID, strjFunClose, true);
        ApplySearch();
    }
}