using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Crew;
using SMS.Properties;

public partial class UserControl_ucPortCalls : System.Web.UI.UserControl
{
    public delegate void SelectEventHandler(PortCalls obj);

    public event SelectEventHandler Select;

    protected void Page_Load(object sender, EventArgs e)
    {
       
    }

    public void BindPortCalls(int Vessel_ID)
    {
        BLL_Crew_CrewDetails objCrew = new BLL_Crew_CrewDetails();
        gvPortCalls.DataSource = objCrew.Get_PortCall_List(Vessel_ID);
        gvPortCalls.DataBind();
    }

    public void lnkSelect_Click(object s, EventArgs e)
    {
        GridViewRow gr = (GridViewRow)((ImageButton)s).Parent.Parent;
      
        PortCalls objp = new PortCalls();
        objp.Arrival_Date = UDFLib.ConvertDateToNull((gr.FindControl("lblArrival") as Label).Text);
        objp.Charterers_Agent_Code = ((Label)gr.FindControl("lblCharterers_Agent")).ToolTip; ;
        objp.Charterers_Agent_Name = ((Label)gr.FindControl("lblCharterers_Agent")).Text; ;
        objp.Departure_Date = UDFLib.ConvertDateToNull((gr.FindControl("lblDeparture") as Label).Text); 
        objp.Owners_Agent_Code = ((Label)gr.FindControl("lblOwners_Agent")).ToolTip;
        objp.Owners_Agent_Name = ((Label)gr.FindControl("lblOwners_Agent")).Text; ;
        objp.Port_Call_ID = UDFLib.ConvertToInteger(gvPortCalls.DataKeys[gr.RowIndex].Values["Port_Call_ID"]);
        objp.Port_Name = (gr.FindControl("lblPort_Name") as Label).Text;
        objp.Port_ID =UDFLib.ConvertToInteger((gr.FindControl("hdnPortID") as HiddenField).Value);

        Select(objp);


    }
}