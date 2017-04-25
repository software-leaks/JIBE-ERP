using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Infrastructure;
using System.Data;
using SMS.Business.PURC;
using System.ComponentModel;


public partial class UserControl_ctlVesselLocationList : System.Web.UI.UserControl
{
    BLL_PURC_Purchase objBLLPurc = new BLL_PURC_Purchase();

    public event EventHandler TextChangedEvent;

    protected void Page_Load(object sender, EventArgs e)
    {
        txtSearchVesselLocation.Focus();
        
    }
    protected void btnSelect_Click(object sender, EventArgs e)
    {
        pnlSearch.Visible = false;

        if (lstVesselLocationList.SelectedItem != null)
        {
            hdn_SelectedText.Value = lstVesselLocationList.SelectedItem.Text;
            hdn_SelectedValue.Value = lstVesselLocationList.SelectedItem.Value;

            txtSelectedVesselLocation.Text = hdn_SelectedText.Value;
        }
    }



    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string SearchText = txtSearchVesselLocation.Text;
        DataTable dt = objBLLPurc.LibraryGetSystemParameterList("1", txtSearchVesselLocation.Text); 
        lstVesselLocationList.DataSource = dt;
        lstVesselLocationList.DataBind();
        lstVesselLocationList.Items.Insert(0, new ListItem("-Select-", "0"));
    }
    protected void lstVesselLocationList_SelectedIndexChanged(object sender, EventArgs e)
    {
        pnlSearch.Visible = false;

        if (lstVesselLocationList.SelectedItem != null)
        {
            hdn_SelectedText.Value = lstVesselLocationList.SelectedItem.Text;
            hdn_SelectedValue.Value = lstVesselLocationList.SelectedItem.Value;

            txtSelectedVesselLocation.Text = hdn_SelectedText.Value;

            ChangedEvent(e);
        }
    }
    protected void btnSearchPort_Click(object sender, EventArgs e)
    {
        pnlSearch.Visible = true;
        DataTable dt = objBLLPurc.LibraryGetSystemParameterList("1", txtSearchVesselLocation.Text); 
        lstVesselLocationList.DataSource = dt;
        lstVesselLocationList.DataBind();
        lstVesselLocationList.Items.Insert(0, new ListItem("-Select-", "0"));


    }

    protected void txtSearchVesselLocation_TextChanged(object sender, EventArgs e)
    {
        string SearchText = txtSearchVesselLocation.Text;
        DataTable dt = objBLLPurc.LibraryGetSystemParameterList("1", txtSearchVesselLocation.Text); 
        lstVesselLocationList.DataSource = dt;
        lstVesselLocationList.DataBind();
        lstVesselLocationList.Items.Insert(0, new ListItem("-Select-", "0"));

        ChangedEvent(e);
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {
        txtSearchVesselLocation.Text = "";
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
                    DataTable dtPort = objBLLPurc.LibraryGetSystemParameterList("1", hdn_SelectedValue.Value);
                    if (dtPort.Rows.Count > 0)
                    {
                        txtSelectedVesselLocation.Text = dtPort.Rows[0]["DESCRIPTION"].ToString();
                    }
                }
                else
                {
                    txtSelectedVesselLocation.Text = "";
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

        get { return txtSelectedVesselLocation.Width.ToString(); }

        set
        {
            try
            {
                if (int.Parse(value.Replace("px", "")) > 30)
                {
                    txtSelectedVesselLocation.Width = int.Parse(value.Replace("px", ""));
                }
            }
            catch { }
        }
    }

     

    protected void ChangedEvent(EventArgs e)
    {
        if (TextChangedEvent != null)
            TextChangedEvent(this, e);
    }


    


}