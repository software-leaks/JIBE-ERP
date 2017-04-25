using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Infrastructure;
using SMS.Business.PortageBill;

public partial class PortageBill_InitializeOfficePortageBill : System.Web.UI.Page
{
    BLL_Infra_VesselLib objVessel = new BLL_Infra_VesselLib();
    public string TodayDateFormat = ""; 
    protected void Page_Load(object sender, EventArgs e)
    {
        TodayDateFormat = UDFLib.DateFormatMessage();
        if (!IsPostBack)
        {
            Load_VesselList();
            CalendarExtenderDt.Format = UDFLib.GetDateFormat();
          
        }
    }

    public void Load_VesselList()
    {
        
        int UserCompanyID = UDFLib.ConvertToInteger(Session["USERCOMPANYID"].ToString());
        int Vessel_Manager = UDFLib.ConvertToInteger(Session["USERCOMPANYID"].ToString());

        ddlVessel.DataSource = objVessel.Get_VesselList(0, 0, Vessel_Manager, "", UserCompanyID, 0);

        ddlVessel.DataTextField = "VESSEL_NAME";
        ddlVessel.DataValueField = "VESSEL_ID";
        ddlVessel.DataBind();
        ddlVessel.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));
        ddlVessel.SelectedIndex = 0;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            int Vessel_ID = int.Parse(ddlVessel.SelectedValue);
            DateTime? Date = UDFLib.ConvertDateToNull(txtInitialDate.Text);
            if (Date == null)
            {
                lblMessage.Text = "Please enter valid portage bill date"+TodayDateFormat;
                return;
            }

            int Key = BLL_PB_PortageBill.INS_Initial_Office_Portage_Bill_DL(Vessel_ID, Date, int.Parse(Session["USERID"].ToString()));
            if (Key == 1)
            {   
            lblMessage.Text = "Initialize office portage bill has been saved successfully.";                        
            }
            else
            {
                lblMessage.Text = "Initialize office portage bill already exists.";                        
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ddlVessel.SelectedIndex = 0;
        txtInitialDate.Text = "";
        lblMessage.Text = "";
    }
}