using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Infrastructure;
using System.Data;
using System.ComponentModel;
using SMS.Business.PURC;

public partial class UserControl_ctlFunctionList : System.Web.UI.UserControl
{
    BLL_PURC_Purchase objBLLPurc = new BLL_PURC_Purchase();

    protected void Page_Load(object sender, EventArgs e)
    {
        txtSearchFunction.Focus();
        
    }
    protected void btnSelect_Click(object sender, EventArgs e)
    {
        pnlSearch.Visible = false;

        if (lstFunctionList.SelectedItem != null)
        {
            hdn_SelectedText.Value = lstFunctionList.SelectedItem.Text;
            hdn_SelectedValue.Value = lstFunctionList.SelectedItem.Value;

            txtSelectedFunction.Text = hdn_SelectedText.Value;
        }
    }



    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string SearchText = txtSearchFunction.Text;
        DataTable dt = objBLLPurc.LibraryGetSystemParameterList("115", txtSearchFunction.Text);  
        lstFunctionList.DataSource = dt;
        lstFunctionList.DataBind();
        lstFunctionList.Items.Insert(0, new ListItem("-Select-", "0"));
    }
    protected void lstFunctionList_SelectedIndexChanged(object sender, EventArgs e)
    {
        pnlSearch.Visible = false;

        if (lstFunctionList.SelectedItem != null)
        {
            hdn_SelectedText.Value = lstFunctionList.SelectedItem.Text;
            hdn_SelectedValue.Value = lstFunctionList.SelectedItem.Value;

            txtSelectedFunction.Text = hdn_SelectedText.Value;
        }
    }
    protected void btnSearchPort_Click(object sender, EventArgs e)
    {
        pnlSearch.Visible = true;
        DataTable dt = objBLLPurc.LibraryGetSystemParameterList("115", txtSearchFunction.Text); 
        lstFunctionList.DataSource = dt;
        lstFunctionList.DataBind();
        lstFunctionList.Items.Insert(0, new ListItem("-Select-", "0"));


    }

    protected void txtSearchFunction_TextChanged(object sender, EventArgs e)
    {
        string SearchText = txtSearchFunction.Text;
        DataTable dt = objBLLPurc.LibraryGetSystemParameterList("115", txtSearchFunction.Text); 
        lstFunctionList.DataSource = dt;
        lstFunctionList.DataBind();
        lstFunctionList.Items.Insert(0, new ListItem("-Select-", "0"));
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {
        txtSearchFunction.Text = "";
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
                    DataTable dtPort = objBLLPurc.LibraryGetSystemParameterList("115", hdn_SelectedValue.Value);
                    if (dtPort.Rows.Count > 0)
                    {
                        txtSelectedFunction.Text = dtPort.Rows[0]["DESCRIPTION"].ToString();
                    }
                }
                else
                {
                    txtSelectedFunction.Text = "";
                }
            }
            catch { }
        }
    }
    public string TargetControl
    {

        get { return hdn_TargetControlID.Value; }

        set { hdn_TargetControlID.Value = value; }
    }
    public string Width
    {

        get { return txtSelectedFunction.Width.ToString(); }

        set
        {
            try
            {
                if (int.Parse(value.Replace("px", "")) > 30)
                {
                    txtSelectedFunction.Width = int.Parse(value.Replace("px", ""));
                }
            }
            catch { }
        }
    }
   
}