using System;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.Infrastructure;

// DEPENDENCIES : ucCustomDropDownList.js,JSCustomFilterControls.js,CssCustomFilterControls.css,jquery-1.5.2.min.js or above,UserDefinedFunction.cs

public partial class UserControl_ucAsyncDropDownList : System.Web.UI.UserControl
{

    BLL_Infra_Port objBLLPort = new BLL_Infra_Port();

    #region ------------- Property----------------


    public string Web_Method_URL
    {
        set { hdf_webMethod_name.Value = value; }
        get { return hdf_webMethod_name.Value; }
    }
    public string SelectedValue
    {
        get
        {
            return Convert.ToString(hdf_selected_value.Value);
        }
        set
        {
            try
            {
                hdf_selected_value.Value = Convert.ToString(value);

                if (!string.IsNullOrWhiteSpace(hdf_selected_value.Value) && hdf_selected_value.Value == "0")
                {

                    hdf_selected_text.Value = "-- SELECT --";
                    txtSelectedPortName.Text = "-- SELECT --";
                }

                if (!string.IsNullOrWhiteSpace(SelectedValue) && SelectedValue != "0")
                {
                    string funSelectedtext = String.Format("load_selected_Text('" + CheckBoxListItems.ClientID + "','" + txtSearchItems.ClientID + "','" + Web_Method_URL + "','" + SelectedValue + "','" + hdf_selected_text.ClientID + "','" + txtSelectedPortName.ClientID + "','" + hdf_extra_search.ClientID + "');");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "loadselectedtext" + this.ID, funSelectedtext, true);
                }

            }
            catch { }
        }



    }

    public string SelectedText
    {
        get
        {
            return Convert.ToString(txtSelectedPortName.Text);
        }
    }

    public string FilterText
    {
        get { return Convert.ToString(hdf_extra_search.Value); }
        set { hdf_extra_search.Value = Convert.ToString(value); }
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
                pnltextbox_search.Attributes.Add("style", "min-width:" + Convert.ToString((Convert.ToInt32(value) - 4)) + "px;overflow-x:hidden");

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
            {
                plnDropDownList.Height = value;

            }
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

    public bool Enable
    {
        get { return pnlsearchSection.Enabled; }
        set
        {
            try
            {
                pnlsearchSection.Enabled = value;
                if (value == false)
                    imgCollapseExpandDDL.Attributes.Add("disabled", "disabled");
                if (value == true)
                {
                    imgCollapseExpandDDL.Attributes.Remove("disabled");
                }
            }
            catch { }

        }
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(Web_Method_URL))
        {

            if (!IsPostBack)
            {

                txtSearchItems.Attributes.Add("onkeyup", "LoadPortList_txtsearch_Press_enter(event,'" + CheckBoxListItems.ClientID + "','" + txtSearchItems.ClientID + "','" + hdf_webMethod_name.Value + "','" + hdf_extra_search.ClientID + "');");

                imgCollapseExpandDDL.Attributes.Add("onclick", "ShowHideListBoxItems_Async_dll('" + pnlListSection.ClientID + "','" + imgCollapseExpandDDL.ClientID + "','" + CheckBoxListItems.ClientID + "','" + txtSearchItems.ClientID + "','" + hdf_webMethod_name.Value + "','" + hdf_extra_search.ClientID + "');");

                CheckBoxListItems.Attributes.Add("onchange", "setSelectedPortlistItems('" + hdf_selected_value.ClientID + "','" + hdf_selected_text.ClientID + "','" + txtSelectedPortName.ClientID + "','" + CheckBoxListItems.ClientID + "');");

                if (Width == 0)
                {
                    plnDropDownList.Attributes.Add("style", "min-width:256px");
                    pnlsearchSection.Attributes.Add("style", "width:260px;");
                }

               
            }

            txtSelectedPortName.Text = hdf_selected_text.Value;

            Style = "display:block";
            Css_pnlSearchSection = "tbl-pnlsearchSection-ucListBox-white";
            Css_pnlListSection = "tbl-pnlListSection-ucListBox-white";
            Css_TextBoxSearch_td = "ddl-txtSearchItems-td-css-white";
            Css_TextBoxSearch = "ddl-txtSearchItems-css-white";

            //if (CheckBoxListItems.SelectedIndex > -1)
            //    Css_CollapseExpand_td = "ddl-imgCollapseExpandDDL-td-css-white-OnFilterApplied";
            //else
            Css_CollapseExpand_td = "ddl-imgCollapseExpandDDL-td-css-white";

            Css_Footer_td = "ddl-footer-td-css-white";
            Css_Watermark = "watermarked-white";

            string strjFunClose = String.Format("ShowHideListBoxItems_Async_dll_postback('" + pnlListSection.ClientID + "','" + imgCollapseExpandDDL.ClientID + "');");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowHideListBoxItems" + this.ID, strjFunClose, true);


            //txtSearchItems.CssClass = Css_TextBoxSearch;
            TextBoxWatermarkExtendertxtSearchItems.WatermarkCssClass = Css_Watermark;
        }

    }


}