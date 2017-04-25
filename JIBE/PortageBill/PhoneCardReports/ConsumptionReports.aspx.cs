using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Infrastructure;
using System.Data;

public partial class PortageBill_PhoneCardReports_ConsumptionReports : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
        BLL_Infra_VesselLib objBLLVessel = new BLL_Infra_VesselLib();
        if (!IsPostBack)
        {

            DataTable dtvsl = objBLLVessel.Get_VesselList(0, 0, UDFLib.ConvertToInteger(Session["USERCOMPANYID"]), "", UDFLib.ConvertToInteger(Session["USERCOMPANYID"]));
            cmbVessel.DataSource = dtvsl;
            cmbVessel.DataBind();
            trCrewWice.Visible = false;
            trMonthWice.Visible = false;
            trVesselwice.Visible = true;
            BindYear();
            ddlMonth.SelectedValue = DateTime.Now.ToString("MM");
            ddlYear.SelectedValue = DateTime.Now.Year.ToString();

        }

    }

    private void BindYear()
    {
        ddlYear.Items.Clear();
        for (int i = 2001; i <= DateTime.Now.Year; i++)
        {
            ListItem li = new ListItem();
            li.Text = i.ToString();
            li.Value = i.ToString();
            ddlYear.Items.Add(li);

        }
    }
    protected void cmbVessel_OnDataBound(object source, EventArgs e)
    {

        cmbVessel.Items.Insert(0, new ListItem("-Select-", "0"));

    }

     protected void btnShowVesselConsumption_Click(object sender, EventArgs e)
    {
        ifMoreInfo.Attributes["src"] = "ReportsView.aspx?reportcode=CONSUMPTIONBYVESSEL&vesselid=" + cmbVessel.SelectedValue;
    }
     protected void btnShowCrewConsumption_Click(object sender, EventArgs e)
     {
         string  crewid = "0";
         crewid = ddlCrew.SelectedValue == "" ? "0" : ddlCrew.SelectedValue;


         ifMoreInfo.Attributes["src"] = "ReportsView.aspx?reportcode=CONSUMPTIONBYCREW&crewid=" + crewid;
     }
     protected void btnShowMonthConsumption_Click(object sender, EventArgs e)
     {
         ifMoreInfo.Attributes["src"] = "ReportsView.aspx?reportcode=CONSUMPTIONBYMONTH&month=" + ddlMonth.SelectedValue +"&year="+ ddlYear.SelectedValue ;
     }
     protected void rblReports_SelectedIndexChanged(object sender, EventArgs e)
     {
         if (rblReports.SelectedValue == "Vessel")
         {
             trCrewWice.Visible = false;
             trMonthWice.Visible = false;
             trVesselwice.Visible = true;
         }
         else if (rblReports.SelectedValue == "Crew")
         {
             trCrewWice.Visible = true ;
             trMonthWice.Visible = false;
             trVesselwice.Visible = false;
         }
         else if (rblReports.SelectedValue == "Month")
         {
             trCrewWice.Visible = false;
             trMonthWice.Visible = true ;
             trVesselwice.Visible = false;
         }
         else 
         {
             trCrewWice.Visible = false;
             trMonthWice.Visible = false;
             trVesselwice.Visible = true ;
         }
     }
}