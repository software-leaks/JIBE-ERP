using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Infrastructure;
using System.Data;
using System.ComponentModel;

public partial class UserControl_ctlPortList : System.Web.UI.UserControl
{
    BLL_Infra_Port objBLLPort = new BLL_Infra_Port();

    public delegate void SelectedIndexChangedEventHandler();

    public event SelectedIndexChangedEventHandler SelectedIndexChanged;


    protected void Page_Load(object sender, EventArgs e)
    {
        if (txtSelectedPort.Text.Trim() == "")
            txtSelectedPort.Text = "-Select Port-";
    }
    protected void btnSelect_Click(object sender, EventArgs e)
    {
        try
        {
            pnlSearch.Visible = false;

            if (lstPortList.SelectedItem != null)
            {
                hdn_SelectedText.Value = lstPortList.SelectedItem.Text;
                hdn_SelectedValue.Value = lstPortList.SelectedItem.Value;

                txtSelectedPort.Text = hdn_SelectedText.Value;

                if (SelectedIndexChanged != null)
                    SelectedIndexChanged();

                //txtSelectedPort.Text = hdn_SelectedText.Value;
                //txtSearchPortList.Focus();
            }
        }
        catch { }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string SearchText = txtSearchPortList.Text;
        DataTable dt = objBLLPort.Get_PortList_Mini(SearchText);
        lstPortList.DataSource = dt;
        lstPortList.DataBind();
        lstPortList.Items.Insert(0, new ListItem("-Select Port-", "0"));
    }
    protected void lstPortList_SelectedIndexChanged(object sender, EventArgs e)
    {
        pnlSearch.Visible = false;

        if (lstPortList.SelectedItem != null)
        {
            hdn_SelectedText.Value = lstPortList.SelectedItem.Text;
            hdn_SelectedValue.Value = lstPortList.SelectedItem.Value;

            txtSelectedPort.Text = hdn_SelectedText.Value;

            if (SelectedIndexChanged != null)
                SelectedIndexChanged();
        }
    }
    protected void btnSearchPort_Click(object sender, EventArgs e)
    {
        pnlSearch.Visible = true;
        DataTable dt = objBLLPort.Get_PortList_Mini();
        lstPortList.DataSource = dt;
        lstPortList.DataBind();
        lstPortList.Items.Insert(0, new ListItem("-Select Port-", "0"));


    }

    protected void txtSearchPortList_TextChanged(object sender, EventArgs e)
    {
        string SearchText = txtSearchPortList.Text;
        DataTable dt = objBLLPort.Get_PortList_Mini(SearchText);
        lstPortList.DataSource = dt;
        lstPortList.DataBind();
        lstPortList.Items.Insert(0, new ListItem("-Select Port-", "0"));
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {
        txtSearchPortList.Text = "";
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

                if (hdn_SelectedValue.Value != "" && hdn_SelectedValue.Value != "0")
                {
                    DataTable dtPort = objBLLPort.Get_PortDetailsByID(int.Parse(hdn_SelectedValue.Value));
                    if (dtPort.Rows.Count > 0)
                    {
                        txtSelectedPort.Text = dtPort.Rows[0]["PORT_NAME"].ToString();
                    }
                }
                else
                {
                    txtSelectedPort.Text = "-Select Port-";
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

        get { return txtSelectedPort.Width.ToString(); }

        set
        {
            try
            {
                if (int.Parse(value.Replace("px", "")) > 30)
                {
                    txtSelectedPort.Width = int.Parse(value.Replace("px", ""));
                }
            }
            catch { }
        }
    }

}