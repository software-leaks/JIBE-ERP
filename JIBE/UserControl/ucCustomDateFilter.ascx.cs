using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

// DEPENDENCIES : JSCustomFilterControls.js,CssCustomFilterControls.css,jquery-1.5.2.min.js or above,UserDefinedFunction.cs

public partial class UserControl_ucCustomDateFilter : System.Web.UI.UserControl
{
    public delegate void ApplySearchEventHandler();

    public event ApplySearchEventHandler ApplySearch;

    #region ------------- Property----------------
   
    public string FilterType
    {
        get
        {         
           ListItem Selcteditem  = ListBoxItems.SelectedItem;
            if (Selcteditem == null)
                Selcteditem = lstboxSecindlist.SelectedItem;
            return Selcteditem.Value == "nofilter" ? null : Selcteditem.Value;

        }
    }

    public string FilterDateFrom
    {
        get { return txtFilterFrom.Text; }
        set { txtFilterFrom.Text = value; }
    }

    public string FilterDateTo
    {
        get { return txtFilterTo.Text; }
        set { txtFilterTo.Text = value; }
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

        string sDateFilterTypeSingle = String.Format("checkUcForDateTypeSelectionDateFilter('" + rbtnDateFilterTypeSingle.ClientID + "','" + rbtnDateFilterTypeDouble.ClientID + "','" + ListBoxItems.ClientID + "','" + txtFilterTo.ClientID + "');");
        ScriptManager.RegisterStartupScript(this, this.GetType(), "checksDateFilterTypeSingle" + this.ID, sDateFilterTypeSingle, true);


        rbtnDateFilterTypeSingle.Attributes.Add("onclick", "checkUcForDateTypeSelectionDateFilter('" + rbtnDateFilterTypeSingle.ClientID + "','" + rbtnDateFilterTypeDouble.ClientID + "','" + ListBoxItems.ClientID + "','" + txtFilterTo.ClientID + "');");
        rbtnDateFilterTypeDouble.Attributes.Add("onclick", "checkUcForDateTypeSelectionDateFilter('" + rbtnDateFilterTypeSingle.ClientID + "','" + rbtnDateFilterTypeDouble.ClientID + "','" + ListBoxItems.ClientID + "','" + txtFilterTo.ClientID + "');");

        txtFilterFrom.Attributes.Add("onchange", "clearSelectionOnTextChanged('" + ListBoxItems.ClientID + "',event)");
        txtFilterTo.Attributes.Add("onchange", "clearSelectionOnTextChanged('" + ListBoxItems.ClientID + "',event)");

        txtFilterFrom.Focus();
    }

    #region------------  Metods-------------


    public void Select(string sValue)
    {
        ListItem liitem = ListBoxItems.Items.FindByValue(sValue);
        if (liitem != null)
            liitem.Selected = true;

    }

    protected void ListBoxItems_SelectedIndexChanged(object sender, EventArgs e)
    {
        ListBox lisender = (sender as ListBox);

        if (lisender.ID == "ListBoxItems")
            lstboxSecindlist.ClearSelection();
        else
            ListBoxItems.ClearSelection();

        if (ListBoxItems.SelectedValue == "nofilter")
        {
            txtFilterFrom.Text = "";
            txtFilterTo.Text = "";

        }

        if (lstboxSecindlist.SelectedValue == "EqualToToday" || lstboxSecindlist.SelectedValue == "EqualToOrLessThanToday")
            txtFilterFrom.Text = DateTime.Now.ToString("dd/MM/yyyy");
        else if (lstboxSecindlist.SelectedValue == "EqualToTomorrow")
            txtFilterFrom.Text = DateTime.Now.AddDays(1).ToString("dd/MM/yyyy");


        string strjFunClose = String.Format("HideCustomFilterUserControl()");
        ScriptManager.RegisterStartupScript(this, this.GetType(), "close" + this.ID, strjFunClose, true);
        ApplySearch();
    }


    public void ClearFilter()
    {
        txtFilterFrom.Text = "";
        txtFilterTo.Text = "";
    }



    #endregion


}