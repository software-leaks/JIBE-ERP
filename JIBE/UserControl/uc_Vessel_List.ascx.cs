using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.Infrastructure;
using System.ComponentModel;

public partial class UserControl_uc_Vessel_List : System.Web.UI.UserControl
{
    BLL_Infra_VesselLib objBLLVessel = new BLL_Infra_VesselLib();

    public delegate void SelectedIndexChangedEventHandler();

    public event SelectedIndexChangedEventHandler SelectedIndexChanged;


    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnSelect_Click(object sender, EventArgs e)
    {
        try
        {
            pnlSearch.Visible = false;

            if (lstVesselList.SelectedItem != null)
            {
                hdn_SelectedText.Value = lstVesselList.SelectedItem.Text;
                hdn_SelectedValue.Value = lstVesselList.SelectedItem.Value;

                txtSelectedVessel.Text = hdn_SelectedText.Value;

                if (SelectedIndexChanged != null)
                    SelectedIndexChanged();

                //txtSelectedVessel.Text = hdn_SelectedText.Value;
                //txtSearchVesselList.Focus();
            }
        }
        catch { }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string SearchText = txtSearchVesselList.Text;
        DataTable dt = objBLLVessel.Get_VesselList(0, 0, UDFLib.ConvertToInteger(Session["USERCOMPANYID"]), SearchText, UDFLib.ConvertToInteger(Session["USERCOMPANYID"]));
        lstVesselList.DataSource = dt;
        lstVesselList.DataBind();
        lstVesselList.Items.Insert(0, new ListItem("--VESSELS--", "0"));
    }
    protected void lstVesselList_SelectedIndexChanged(object sender, EventArgs e)
    {
        pnlSearch.Visible = false;

        if (lstVesselList.SelectedItem != null)
        {
            hdn_SelectedText.Value = lstVesselList.SelectedItem.Text;
            hdn_SelectedValue.Value = lstVesselList.SelectedItem.Value;

            txtSelectedVessel.Text = hdn_SelectedText.Value;

            if (SelectedIndexChanged != null)
                SelectedIndexChanged();
        }
    }
    protected void btnSearchVessel_Click(object sender, EventArgs e)
    {
        pnlSearch.Visible = true;
        DataTable dt = objBLLVessel.Get_VesselList(0, 0, UDFLib.ConvertToInteger(Session["USERCOMPANYID"]), "", UDFLib.ConvertToInteger(Session["USERCOMPANYID"]));
        lstVesselList.DataSource = dt;
        lstVesselList.DataBind();
        lstVesselList.Items.Insert(0, new ListItem("--VESSELS--", "0"));
        
    }

    protected void txtSearchVesselList_TextChanged(object sender, EventArgs e)
    {
        string SearchText = txtSearchVesselList.Text;
        DataTable dt = objBLLVessel.Get_VesselList(0, 0, UDFLib.ConvertToInteger(Session["USERCOMPANYID"]), SearchText, UDFLib.ConvertToInteger(Session["USERCOMPANYID"]));
        lstVesselList.DataSource = dt;
        lstVesselList.DataBind();
        lstVesselList.Items.Insert(0, new ListItem("--VESSELS--", "0"));
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {
        txtSearchVesselList.Text = "";
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
                    DataTable dtVessel = objBLLVessel.GetVesselDetails_ByID(int.Parse(hdn_SelectedValue.Value));
                    if (dtVessel.Rows.Count > 0)
                    {
                        txtSelectedVessel.Text = dtVessel.Rows[0]["Vessel_NAME"].ToString();
                    }
                }
                else
                {
                    txtSelectedVessel.Text = "-Select-";
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

        get { return txtSelectedVessel.Width.ToString(); }

        set
        {
            try
            {
                if (int.Parse(value.Replace("px", "")) > 30)
                {
                    txtSelectedVessel.Width = int.Parse(value.Replace("px", ""));
                }
            }
            catch { }
        }
    }
    public bool Enabled
    {
        set { lblvessel.Enabled = value; }
        get { return lblvessel.Enabled; }
    }
}