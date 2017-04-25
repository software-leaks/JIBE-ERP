using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Infrastructure;
using SMS.Business.Crew;
using System.Data;

public partial class Crew_EditEvent : System.Web.UI.Page
{
    BLL_Infra_Country objCountry = new BLL_Infra_Country();
    BLL_Crew_CrewDetails objCrew = new BLL_Crew_CrewDetails();
    BLL_Crew_Admin objCrewAdmin = new BLL_Crew_Admin();
    BLL_Infra_VesselLib objVessel = new BLL_Infra_VesselLib();
    public string DateFormat = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        DateFormat = UDFLib.GetDateFormat();
        CalendarExtender1.Format = DateFormat;
        if (!IsPostBack)
        {
            try
            {
                int EventID = UDFLib.ConvertToInteger(Request.QueryString["EventID"].ToString());
                int Vessel_ID = 0;

                UserAccessValidation();

                hdnEditEventID.Value = EventID.ToString();

                DataTable dt = objCrew.Get_EventDetails(EventID);

                if (dt.Rows.Count > 0)
                {
                    txtEditEventDate.Text = UDFLib.ConvertUserDateFormat(Convert.ToString(dt.Rows[0]["Event_date"]));
                    txtEventRemark.Text = dt.Rows[0]["EventRemark"].ToString();
                    Vessel_ID = UDFLib.ConvertToInteger(dt.Rows[0]["Vessel_ID"].ToString());
                    lblPort.Text = dt.Rows[0]["Port_Name"].ToString();

                    Load_PortCalls(Vessel_ID, dt.Rows[0]["Event_date"].ToString());
                    int i = 0;
                    foreach (GridViewRow gr in gvPortCalls.Rows)
                    {
                        if (((HiddenField)gr.FindControl("hdnPortID")).Value == dt.Rows[0]["PortID"].ToString())
                        {
                            gvPortCalls.SelectedIndex = i;
                            break;
                        }
                        i++;
                    }

                }
            }
            catch
            {
                Response.Redirect("~/crew/crewlist.aspx");
            }
        }
        
    }

    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        SMS.Properties.UserAccess objUA = new SMS.Properties.UserAccess();
        SMS.Business.Infrastructure.BLL_Infra_UserCredentials objUser = new SMS.Business.Infrastructure.BLL_Infra_UserCredentials();
        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
            Response.Redirect("~/default.aspx?msgid=1");

        if (objUA.Add == 0)
        {
            btnSaveEventEdit.Enabled = false;
        }

        if (objUA.Edit == 0)
        {
            btnSaveEventEdit.Enabled = false;
        }
        if (objUA.Delete == 0)
        {

        }
        if (objUA.Approve == 0)
        {

        }
    }

    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }

    protected void Load_PortCalls(int Vessel_ID, string EventDate)
    {
        gvPortCalls.DataSource = objCrew.Get_PortCall_List(Vessel_ID, EventDate);
        gvPortCalls.DataBind();
    }
    protected void btnSaveEventEdit_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtEditEventDate.Text != "")
            {
                txtEditEventDate.Text = UDFLib.ConvertToDefaultDt(txtEditEventDate.Text);
                int EventID = int.Parse(hdnEditEventID.Value);
                int Port_Call_ID = 0;

                if(gvPortCalls.SelectedValue != null)
                    Port_Call_ID = UDFLib.ConvertToInteger(gvPortCalls.SelectedValue.ToString()); ;
                
                int Port_ID = 0;
                if (gvPortCalls.SelectedRow != null)
                {
                    HiddenField hdnPortID = (HiddenField)gvPortCalls.SelectedRow.FindControl("hdnPortID");
                    if (hdnPortID != null)
                        Port_ID = UDFLib.ConvertToInteger(hdnPortID.Value);
                }

                string Remark = "-";
                if (txtEventRemark.Text != "")
                    Remark = txtEventRemark.Text;
                

                GridViewRow gr = gvPortCalls.SelectedRow;
                if (gr != null)
                {
                    string Arrival = ((Label)(gr.FindControl("lblArrival"))).Text;
                    string Departure = ((Label)(gr.FindControl("lblDeparture"))).Text;
                    string EventDate = txtEditEventDate.Text;


                    if (DateTime.Parse(UDFLib.ConvertToDefaultDt(EventDate)) < DateTime.Parse(UDFLib.ConvertToDefaultDt(Arrival)) || DateTime.Parse(UDFLib.ConvertToDefaultDt(EventDate)) > DateTime.Parse(UDFLib.ConvertToDefaultDt(Departure)))
                    {
                        string js = "alert('Event date is not within the vessels arrival and departure dates for the selected port call.');";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUser", js, true);
                    }
                    else
                    {
                        objCrew.UPDATE_CrewChangeEvent(EventID, UDFLib.ConvertToDefaultDt(txtEditEventDate.Text), Port_ID, Remark, GetSessionUserID(), Port_Call_ID);

                        string js = "alert('Event Updated.');window.close();";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "wclose", js, true);
                    }

                }
                else
                {
                    objCrew.UPDATE_CrewChangeEvent(EventID, UDFLib.ConvertToDefaultDt(txtEditEventDate.Text), Port_ID, Remark, GetSessionUserID(), Port_Call_ID);
                    
                    string js = "alert('Event Updated.');window.close();";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "wclose", js, true);

                }
                
            }
            else if (gvPortCalls.SelectedIndex <  0)
            {
                string js = "alert('Select Event Port');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUser", js, true);
            }
        }
        catch
        {
        }
    }
    protected void btnCloseEventEdit_Click(object sender, EventArgs e)
    {
        string js = "window.close();";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "wclose", js, true);
        
    }

    protected void gvPortCalls_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow gr = gvPortCalls.SelectedRow;

        string Arrival    = ((Label)(gr.FindControl("lblArrival"))).Text;
        string Departure = ((Label)(gr.FindControl("lblDeparture"))).Text;
        string EventDate = txtEditEventDate.Text;

        btnSaveEventEdit.Enabled = true;
        try
        {
            if(DateTime.Parse(UDFLib.ConvertToDefaultDt(EventDate)) < DateTime.Parse(UDFLib.ConvertToDefaultDt(Arrival))  || DateTime.Parse(UDFLib.ConvertToDefaultDt(EventDate)) > DateTime.Parse(UDFLib.ConvertToDefaultDt(Departure)) )
            {
                btnSaveEventEdit.Enabled = false;

                string js = "alert('Event date is not within the vessels arrival and departure dates for the selected port call.');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUser", js, true);

            }
        }
        catch { }
    }
    
}