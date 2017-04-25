using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Infrastructure;
using System.Data;
using System.ComponentModel;

public partial class UserControl_ctlCityList : System.Web.UI.UserControl
{
    BLL_Infra_City objBLLCity = new BLL_Infra_City();

    protected void Page_Load(object sender, EventArgs e)
    {
        txtSearchCityList.Focus();
    }
    protected void btnSelect_Click(object sender, EventArgs e)
    {
        pnlSearch.Visible = false;

        if (lstCityList.SelectedItem != null)
        {
            hdn_SelectedText.Value = lstCityList.SelectedItem.Text;
            hdn_SelectedValue.Value = lstCityList.SelectedItem.Value;

            txtSelectedCity.Text = hdn_SelectedText.Value;
        }
    }



    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string SearchText = txtSearchCityList.Text;
        DataTable dt = objBLLCity.Get_CityList_Mini(SearchText);
        lstCityList.DataSource = dt;
        lstCityList.DataBind();
        lstCityList.Items.Insert(0, new ListItem("-Select-", "0"));
    }
    protected void lstCityList_SelectedIndexChanged(object sender, EventArgs e)
    {
        pnlSearch.Visible = false;

        if (lstCityList.SelectedItem != null)
        {
            hdn_SelectedText.Value = lstCityList.SelectedItem.Text;
            hdn_SelectedValue.Value = lstCityList.SelectedItem.Value;

            txtSelectedCity.Text = hdn_SelectedText.Value;
        }
    }
    protected void btnSearchCity_Click(object sender, EventArgs e)
    {
        pnlSearch.Visible = true;
        DataTable dt = objBLLCity.Get_CityList_Mini();
        lstCityList.DataSource = dt;
        lstCityList.DataBind();
        lstCityList.Items.Insert(0,new ListItem("-Select-","0"));


    }

    protected void txtSearchCityList_TextChanged(object sender, EventArgs e)
    {
        string SearchText = txtSearchCityList.Text;
        DataTable dt = objBLLCity.Get_CityList_Mini(SearchText);
        lstCityList.DataSource = dt;
        lstCityList.DataBind();
        lstCityList.Items.Insert(0,new ListItem("-Select-", "0"));
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {
        txtSearchCityList.Text="";
        pnlSearch.Visible = false;
    }
    
    public string SelectedText
    {
        get { return hdn_SelectedText.Value; }
    }
    
    [BindableAttribute(true)]
    public string SelectedValue
    {

        get { return hdn_SelectedValue.Value; }

        set
        {
            try
            {
                hdn_SelectedValue.Value = value;

                if (hdn_SelectedValue.Value != "")
                {
                    DataTable dtCity = objBLLCity.Get_CityDetailsByID(int.Parse(hdn_SelectedValue.Value));
                    if (dtCity.Rows.Count > 0)
                    {
                        txtSelectedCity.Text = dtCity.Rows[0]["CITYNAME"].ToString();
                    }
                }
            }
            catch { }
        }
    }
    public string TargetControl
    {

        get { return hdn_TargetControlID.Value; }

        set { hdn_TargetControlID.Value = value;}
    }
    public string Width
    {

        get { return txtSelectedCity.Width.ToString(); }

        set {
            try
            {
                if(int.Parse(value.Replace("px", "") ) > 30)
                {
                    txtSelectedCity.Width = int.Parse(value.Replace("px", ""));
                }
            }
            catch { }
        }
    }

}