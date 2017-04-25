using System;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

// DEPENDENCIES : ucCustomDropDownList.js,JSCustomFilterControls.js,CssCustomFilterControls.css,jquery-1.5.2.min.js or above,UserDefinedFunction.cs

public partial class UserControl_ucCustomDropDownList : System.Web.UI.UserControl
{
    public delegate void ApplySearchEventHandler();

    public event ApplySearchEventHandler ApplySearch;

    #region ------------- Property----------------

    public string DataTextField
    {
        set { CheckBoxListItems.DataTextField = value; }
        get { return CheckBoxListItems.DataTextField; }
    }

    public string DataValueField
    {
        set { CheckBoxListItems.DataValueField = value; }
    }

    public DataTable DataSource
    {
        set
        {
            if (!UseJavaScriptForControlAction)
            {
                Session[("DDlCheckBoxListItemsSN" + ID)] = value;
            }
            CheckBoxListItems.DataSource = value; CheckBoxListItems.DataBind(); chkSelectAll.Checked = false;

            if (!UseInHeader)
            {
                Css_CollapseExpand_td = "ddl-imgCollapseExpandDDL-td-css-white";
            }
        }

    }

    public DataTable SelectedValues
    {
        get
        {
            DataTable dtValues = new DataTable();
            dtValues.Columns.Add("SelectedValue");

            DataRow dr = null;
            foreach (ListItem liItems in CheckBoxListItems.Items)
            {
                if (liItems.Selected)
                {
                    dr = dtValues.NewRow();
                    dr["SelectedValue"] = liItems.Value.ToString();
                    dtValues.Rows.Add(dr);
                }

            }

            return dtValues;
        }


    }

    public DataTable SelectedTexts
    {
        get
        {

            DataTable dtValues = new DataTable();
            dtValues.Columns.Add("SelectedText");
            DataRow dr = null;
            foreach (ListItem liItems in CheckBoxListItems.Items)
            {
                if (liItems.Selected)
                {
                    dr = dtValues.NewRow();
                    dr["SelectedText"] = liItems.Text.ToString();
                    dtValues.Rows.Add(dr);
                }

            }


            return dtValues;
        }
    }

    public int Width
    {
        set
        {
            hdf_Width.Value = Convert.ToString(value);

            pnlsearchSection.Width = value;

            if (Convert.ToInt32(value) > 0)
            {
                plnDropDownList.Attributes.Add("style", "min-width:" + Convert.ToString((Convert.ToInt32(value) - 4)) + "px;overflow-x:hidden");

            }

            //else
            //    plnDropDownList.Attributes.Add("style", "min-width:260px");
        }

        get
        {
            return Convert.ToInt32(hdf_Width.Value);
        }
    }

    public int Height
    {
        set
        {
            if (UDFLib.ConvertToInteger(value) > 0)
                plnDropDownList.Height = value;
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

    public bool UseJavaScriptForControlAction
    {
        get { return Convert.ToBoolean(hdf_UseSession.Value); }
        set { hdf_UseSession.Value = Convert.ToString(value); }
    }

    public bool Collapsed
    {
        get { return Convert.ToBoolean(hdf_Collapsed.Value); }
        set { hdf_Collapsed.Value = Convert.ToString(value); }

    }
    public bool FocusThis
    {
        get { return Convert.ToBoolean(hdf_Focus.Value); }
        set { hdf_Focus.Value = Convert.ToString(value); }

    }

    public bool HideOnApplyFilter
    {
        get { return Convert.ToBoolean(hdf_HideOnApplyFilter.Value); }
        set { hdf_HideOnApplyFilter.Value = Convert.ToString(value); }

    }

    public bool UseInHeader
    {
        get { return Convert.ToBoolean(hdf_UseInHeader.Value); }
        set { hdf_UseInHeader.Value = Convert.ToString(value); }

    }

    public string Css_TextBoxSearch_td
    {
        get { return hdf_txtSearchItems_td_css.Value; }
        set { hdf_txtSearchItems_td_css.Value = value; }
    }

    public string Css_TextBoxSearch
    {
        get { return hdf_txtSearchItems_css.Value; }
        set { hdf_txtSearchItems_css.Value = value; }
    }

    public string Css_CollapseExpand_td
    {
        get { return hdf_imgCollapseExpandDDL_td_css.Value; }
        set { hdf_imgCollapseExpandDDL_td_css.Value = value; }
    }

    public string Css_Footer_td
    {
        get { return hdf_footer_td_css.Value; }
        set { hdf_footer_td_css.Value = value; }
    }

    public string Css_Watermark
    {
        get { return hdf_WatermarkCssClass.Value; }
        set { hdf_WatermarkCssClass.Value = value; }
    }

    public string Css_pnlSearchSection
    {
        get { return hdf_tbl_pnlsearchSection.Value; }
        set { hdf_tbl_pnlsearchSection.Value = value; }
    }

    public string Css_pnlListSection
    {
        get { return hdf_tbl_pnlListSection.Value; }
        set { hdf_tbl_pnlListSection.Value = value; }
    }


    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        //PostBackTrigger pst = new PostBackTrigger();
        //pst.ControlID = "btnApplyFilter";

        if (!IsPostBack)
        {
            if (UseJavaScriptForControlAction)
            {
                chkSelectAll.AutoPostBack = false;
                chkSelectAll.Attributes.Add("onclick", " iterateCheckBoxListUserCintrol('" + chkSelectAll.ClientID + "','" + CheckBoxListItems.ClientID + "');");
                imgAsc.Attributes.Add("onclick", "sortcheckboxlistUserControl('" + CheckBoxListItems.ClientID + "');return false;");
                imgDesc.Attributes.Add("onclick", "sortcheckboxlistUserControl('" + CheckBoxListItems.ClientID + "');return false;");
                txtSearchItems.Attributes.Add("onkeyup", "findcheckBoxListUserControl('" + CheckBoxListItems.ClientID + "','" + txtSearchItems.ClientID + "',event);");
                txtSearchItems.Attributes.Add("onmousedown", "ExpandOnSearchClick('" + pnlListSection.ClientID + "','" + imgCollapseExpandDDL.ClientID + "');");
                btnsearchList.Attributes.Add("onclick", "javascript:return false;");
                btnApplyFilter.Attributes.Add("onclick", "cleartextBoxSearch('" + txtSearchItems.ClientID + "');");
            }
            else
            {
                chkSelectAll.AutoPostBack = true;
            }


            imgCollapseExpandDDL.Attributes.Add("onclick", "ShowHideListBoxItems_dll('" + pnlListSection.ClientID + "','" + imgCollapseExpandDDL.ClientID + "');");

            if (Width == 0)
            {
                plnDropDownList.Attributes.Add("style", "min-width:256px");
                pnlsearchSection.Attributes.Add("style", "width:260px;");
            }

        }


        if (!UseInHeader)
        {
            // CollapsiblePanelExtenderDropDownList.Collapsed = true;
            // CollapsiblePanelExtenderDropDownList.ClientState = "true";
            Style = "display:block";
            Css_pnlSearchSection = "tbl-pnlsearchSection-ucListBox-white";
            Css_pnlListSection = "tbl-pnlListSection-ucListBox-white";
            Css_TextBoxSearch_td = "ddl-txtSearchItems-td-css-white";
            Css_TextBoxSearch = "ddl-txtSearchItems-css-white";

            if (CheckBoxListItems.SelectedIndex > -1)
                Css_CollapseExpand_td = "ddl-imgCollapseExpandDDL-td-css-white-OnFilterApplied";
            else
                Css_CollapseExpand_td = "ddl-imgCollapseExpandDDL-td-css-white";

            Css_Footer_td = "ddl-footer-td-css-white";
            Css_Watermark = "watermarked-white";

            string strjFunClose = String.Format("ShowHideListBoxItems_dll_postback('" + pnlListSection.ClientID + "','" + imgCollapseExpandDDL.ClientID + "');");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowHideListBoxItems" + this.ID, strjFunClose, true);

        }
        else
        {
            txtSearchItems.Focus();
        }

        txtSearchItems.CssClass = Css_TextBoxSearch;
        //xtBoxWatermarkExtendertxtSearchItems.WatermarkCssClass = Css_Watermark;


    }

    #region------------  Metods-------------

    // default button makes postback(for enter key)
    protected void txtSearchItems_TextChanged(object sender, EventArgs e)
    {
        if (!UseJavaScriptForControlAction)
        {

            DataTable dtItems = (DataTable)Session[("DDlCheckBoxListItemsSN" + ID)];
            string RowFilter = String.Format(DataTextField + " LIKE '%" + UDFLib.EscapeLikeValue(txtSearchItems.Text) + "%'");
            dtItems.DefaultView.RowFilter = RowFilter;
            CheckBoxListItems.DataSource = dtItems.DefaultView;
            CheckBoxListItems.DataBind();
            UpdatePanelheckBoxListItems.Update();
        }
    }

    protected void btnApplyFilter_Click(object sender, EventArgs e)
    {
        if (UseInHeader)
        {
            string strjFunClose = String.Format("HideCustomFilterUserControl()");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "close" + this.ID, strjFunClose, true);
        }
        //DataTable dtValues = new DataTable();
        //dtValues.Columns.Add("SelectedValue");

        //DataRow dr = null;
        //foreach (ListItem liItems in CheckBoxListItems.Items)
        //{
        //    if (liItems.Selected)
        //    {
        //        dr = dtValues.NewRow();
        //        dr["SelectedValue"] = liItems.Value.ToString();
        //        dtValues.Rows.Add(dr);
        //    }

        //}

      

        if (ApplySearch != null)
            ApplySearch();
    }

    // in use when useSession is true (selection using jquery)
    protected void chkSelectAll_CheckedChanged(object sender, EventArgs e)
    {
        foreach (ListItem liItems in CheckBoxListItems.Items)
        {
            //JIT: JIT/JIBE/9192	
            //Mdf By:Nilesh Pawar Mdf on:17/06/2016
            if (!chkSelectAll.Checked && liItems.Selected == false)
                liItems.Selected = chkSelectAll.Checked;

            //End

        }
        UpdatePanelheckBoxListItems.Update();

    }
    // in use when useSession is true (sorting using jquery)
    protected void imgAsc_Click(object sender, ImageClickEventArgs e)
    {
        if (!UseJavaScriptForControlAction)
        {
            DataTable dtItems = (DataTable)Session[("DDlCheckBoxListItemsSN" + ID)];
            string RowFilter = String.Format(DataTextField + " LIKE '%" + UDFLib.EscapeLikeValue(txtSearchItems.Text) + "%'");
            dtItems.DefaultView.RowFilter = RowFilter;
            dtItems.DefaultView.Sort = String.Format(DataTextField + " ASC");
            CheckBoxListItems.DataSource = dtItems.DefaultView;
            CheckBoxListItems.DataBind();
            UpdatePanelheckBoxListItems.Update();
        }
    }
    // in use when useSession is true (sorting using jquery)
    protected void imgDesc_Click(object sender, ImageClickEventArgs e)
    {
        if (!UseJavaScriptForControlAction)
        {
            DataTable dtItems = (DataTable)Session[("DDlCheckBoxListItemsSN" + ID)];
            string RowFilter = String.Format(DataTextField + " LIKE '%" + UDFLib.EscapeLikeValue(txtSearchItems.Text) + "%'");
            dtItems.DefaultView.RowFilter = RowFilter;
            dtItems.DefaultView.Sort = String.Format(DataTextField + " DESC");
            CheckBoxListItems.DataSource = dtItems.DefaultView;
            CheckBoxListItems.DataBind();
            UpdatePanelheckBoxListItems.Update();
        }
    }

    public void Select(string sValue)
    {
        ListItem liitem = CheckBoxListItems.Items.FindByValue(sValue);
        if (liitem != null)
            liitem.Selected = true;

    }

    public void SelectItems(string[] arrValue)
    {
        foreach (string svl in arrValue)
        {
            ListItem liitem = CheckBoxListItems.Items.FindByValue(svl);
            if (liitem != null)
                liitem.Selected = true;
        }
    }


    public void ClearSelection()
    {
        CheckBoxListItems.ClearSelection();
        chkSelectAll.Checked = false;
        if (!UseInHeader)
        {
            Css_CollapseExpand_td = "ddl-imgCollapseExpandDDL-td-css-white";
        }
    }

    #endregion






}