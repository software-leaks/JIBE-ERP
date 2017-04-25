using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Infrastructure;
using System.Data;
using System.ComponentModel;
using SMS.Business.Crew;

public partial class UserControl_ctlAirPortList : System.Web.UI.UserControl
{

    BLL_Crew_CrewDetails objBLLCrew = new BLL_Crew_CrewDetails();

    public delegate void SelectedIndexChangedEventHandler();

    public event SelectedIndexChangedEventHandler SelectedIndexChanged;


    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void SearchAirport()
    {

        hdn_SelectedText.Value = "";
        hdn_SelectedValue.Value = "";
        string SearchText = txtSearchAirPortList.Text;

        if (txtSearchAirPortList.Text.Trim() != "")
        {
            DataTable dt = objBLLCrew.Get_AirportList(0, "", SearchText, "");
            if (dt.Rows.Count > 0)
            {

                txtSearchAirPortList.Text = dt.Rows[0]["AirPortName"].ToString();
                txtSearchAirPortList.ToolTip = dt.Rows[0]["AirportNameWithIttaCode"].ToString();
                hdn_SelectedText.Value = dt.Rows[0]["IATA_CODE"].ToString();
                hdn_SelectedValue.Value = dt.Rows[0]["ID"].ToString();

                if (SelectedIndexChanged != null)
                    SelectedIndexChanged();

                pnlAirportList.Visible = false;
            }
            else
            {
                Load_Continent("");
                pnlAirportList.Visible = true;
            }
        }
        else
        {
            txtSearchAirPortList.Text = string.Empty;
            pnlAirportList.Visible = false;
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {

        SearchAirport();
    }

    protected void txtSearchAirPortList_TextChanged(object sender, EventArgs e)
    {
        SearchAirport();

    }

    protected void btnClose_Click(object sender, EventArgs e)
    {
        pnlAirportList.Visible = false;

    }

    public string SelectedText
    {
        get { return txtSearchAirPortList.Text; }
    }

    [BindableAttribute(true)]
    public string Text
    {

        get
        {
            if (hdn_SelectedText.Value != "")
                return hdn_SelectedText.Value;
            else
                return txtSearchAirPortList.Text;
        }
        set
        {
            txtSearchAirPortList.Text = value;
        }

    }

    public string ToolTip
    {
        get { return txtSearchAirPortList.ToolTip; }
        set { txtSearchAirPortList.ToolTip = value; }
    }

    public string AirportName
    {
        get
        {
            return txtSearchAirPortList.Text;
        }

    }

    [BindableAttribute(true)]
    public string SelectedValue
    {
        get { return hdn_SelectedValue.Value; }
        set
        {
            try
            {

                if (value != "" && value != "0")
                {
                    DataTable dtPort = objBLLCrew.Get_AirportList(UDFLib.ConvertToInteger(value));

                    if (dtPort.Rows.Count > 0)
                    {
                        txtSearchAirPortList.Text = dtPort.Rows[0]["AirPortName"].ToString();
                        hdn_SelectedText.Value = dtPort.Rows[0]["IATA_CODE"].ToString();
                        hdn_SelectedValue.Value = value;
                    }
                }
                else
                {
                    txtSearchAirPortList.Text = "";
                    hdn_SelectedValue.Value = "0";
                    hdn_SelectedText.Value = "";

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

        get { return txtSearchAirPortList.Width.ToString(); }

        set
        {
            try
            {
                if (int.Parse(value.Replace("px", "")) > 30)
                {
                    txtSearchAirPortList.Width = int.Parse(value.Replace("px", ""));
                }
            }
            catch { }
        }
    }

    protected void txtContinent_TextChanged(object sender, EventArgs e)
    {
        Load_Continent(txtContinent.Text);
    }

    protected void txtCountry_TextChanged(object sender, EventArgs e)
    {
        Load_Country(lstContinent.SelectedValue, txtCountry.Text);
    }

    protected void txtMunicipality_TextChanged(object sender, EventArgs e)
    {
        int CountryID = 0;
        if (lstCountry.SelectedValue != null && lstCountry.SelectedValue != "")
            CountryID = int.Parse(lstCountry.SelectedValue);
        Load_Municipality(CountryID, txtMunicipality.Text);
    }

    protected void txtAirportName_TextChanged(object sender, EventArgs e)
    {
        string Municipality = "";
        if (lstMunicipality.SelectedValue != null)
            Municipality = lstMunicipality.SelectedValue;

        if (txtAirportName.Text.Length > 2)
            Load_Airports(Municipality, txtAirportName.Text);

    }

    protected void lstContinent_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (lstContinent.SelectedValue != null && lstContinent.SelectedValue != "")
        {
            Load_Country(lstContinent.SelectedValue, txtCountry.Text);
        }
    }

    protected void lstCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (lstCountry.SelectedValue != null && lstCountry.SelectedValue != "")
        {
            Load_Municipality(int.Parse(lstCountry.SelectedValue), txtMunicipality.Text);
            Load_Airports(int.Parse(lstCountry.SelectedValue), txtAirportName.Text);
        }
    }

    protected void lstMunicipality_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (lstMunicipality.SelectedValue != null && lstMunicipality.SelectedValue != "")
        {
            Load_Airports(lstMunicipality.SelectedValue, txtAirportName.Text);
        }
    }

    private void Load_Continent(string SearchString)
    {
        try
        {
            DataTable dt = objBLLCrew.Get_Airport_Continents(SearchString);
            lstContinent.DataSource = dt;
            lstContinent.DataTextField = "Continent";
            lstContinent.DataValueField = "Continent";
            lstContinent.DataBind();
        }
        catch { }
    }

    private void Load_Country(string Continent, string SearchCountry)
    {
        try
        {
            DataTable dt = objBLLCrew.Get_Airport_Countries(Continent, SearchCountry);
            lstCountry.DataSource = dt;
            lstCountry.DataTextField = "Country_Name";
            lstCountry.DataValueField = "Country_ID";
            lstCountry.DataBind();
        }
        catch { }
    }

    private void Load_Municipality(int Country, string SearchCity)
    {
        try
        {
            DataTable dt = objBLLCrew.Get_Airport_Cities(Country, SearchCity);
            lstMunicipality.DataSource = dt;
            lstMunicipality.DataTextField = "Municipality";
            lstMunicipality.DataTextField = "Municipality";
            lstMunicipality.DataBind();
        }
        catch { }
    }

    private void Load_Airports(string City, string SearchAirport)
    {
        try
        {
            DataTable dt = objBLLCrew.Get_AirportList(City, SearchAirport);
            lstAirport_IataCode.Items.Clear();
            lstAirport.Items.Clear();

            if (SearchAirport != "")
            {
                DataView view1 = new DataView(dt);
                view1.RowFilter = "IATA_CODE='" + SearchAirport + "'";
                lstAirport_IataCode.DataSource = view1;
                lstAirport_IataCode.DataTextField = "AirportName";
                lstAirport_IataCode.DataValueField = "ID";
                lstAirport_IataCode.DataBind();

                DataView view2 = new DataView(dt);
                view2.RowFilter = "isnull(IATA_CODE,'') <> '" + SearchAirport + "'";
                lstAirport.DataSource = view2;
                lstAirport.DataTextField = "AirportName";
                lstAirport.DataValueField = "ID";
                lstAirport.DataBind();
            }
            else
            {
                lstAirport.DataSource = dt;
                lstAirport.DataTextField = "AirportName";
                lstAirport.DataValueField = "ID";
                lstAirport.DataBind();
            }
        }
        catch { }
    }

    private void Load_Airports(int Country, string SearchAirport)
    {
        try
        {
            DataTable dt = objBLLCrew.Get_AirportList(Country, SearchAirport);
            lstAirport.DataSource = dt;
            lstAirport.DataTextField = "AirportName";
            lstAirport.DataValueField = "ID";
            lstAirport.DataBind();
        }
        catch { }
    }

    private void Load_Airports(int Country, string City, string SearchAirport)
    {
        try
        {
            DataTable dt = objBLLCrew.Get_AirportList(Country, City, SearchAirport);
            lstAirport.DataSource = dt;
            lstAirport.DataTextField = "AirportName";
            lstAirport.DataValueField = "ID";
            lstAirport.DataBind();
        }
        catch { }
    }

    protected void btnSelectAirport_Click(object sender, EventArgs e)
    {
        if (lstAirport_IataCode.SelectedValue != null && lstAirport_IataCode.SelectedValue != "")
        {
            hdn_SelectedValue.Value = lstAirport_IataCode.SelectedValue;
            hdn_SelectedText.Value = lstAirport_IataCode.SelectedItem.Text;

            // txtSearchAirPortList.Text = lstAirport_IataCode.SelectedItem.Text;
            GetAirPortWithIttaCode(hdn_SelectedValue.Value.ToString());

        }
        else if (lstAirport.SelectedValue != null && lstAirport.SelectedValue != "")
        {
            hdn_SelectedValue.Value = lstAirport.SelectedValue;
            hdn_SelectedText.Value = lstAirport.SelectedItem.Text;

            // txtSearchAirPortList.Text = lstAirport.SelectedItem.Text;
            GetAirPortWithIttaCode(hdn_SelectedValue.Value.ToString());
        }


        pnlAirportList.Visible = false;
        lstContinent.Items.Clear();
        lstCountry.Items.Clear();
        lstMunicipality.Items.Clear();
        lstAirport.Items.Clear();
        lstAirport_IataCode.Items.Clear();
        txtContinent.Text = "";
        txtCountry.Text = "";
        txtMunicipality.Text = "";
        txtAirportName.Text = "";


    }

    protected void GetAirPortWithIttaCode(string AirportID)
    {

        DataTable dtPort = objBLLCrew.Get_AirportList(Convert.ToInt32(AirportID));

        if (dtPort.Rows.Count > 0)
        {
            txtSearchAirPortList.Text = dtPort.Rows[0]["AirPortName"].ToString();
            txtSearchAirPortList.ToolTip = dtPort.Rows[0]["AirportNameWithIttaCode"].ToString();
        }

    }

    protected void btnCloseAirport_Click(object sender, EventArgs e)
    {

        pnlAirportList.Visible = false;
        lstContinent.Items.Clear();
        lstCountry.Items.Clear();
        lstMunicipality.Items.Clear();
        lstAirport.Items.Clear();
        txtContinent.Text = "";
        txtCountry.Text = "";
        txtMunicipality.Text = "";
        txtAirportName.Text = "";
    }


}