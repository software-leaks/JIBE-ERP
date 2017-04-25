using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Crew;
using System.Data;

public partial class Crew_ViewEvent : System.Web.UI.Page
{

    BLL_Crew_CrewDetails objCrew = new BLL_Crew_CrewDetails();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int EventID = UDFLib.ConvertToInteger(Request.QueryString["ID"]);
            
            Bind_ChangeEvent(EventID);
        }
    }

    protected void Bind_ChangeEvent(int EventID)
    {

        DataSet ds = objCrew.Get_CrewChangeEvent(EventID, GetSessionUserID());
        UDFLib.AddParentTable(ds.Tables[0], "Events", new string[] { "PKID" },
        new string[] { "Vessel_short_name", "Event_Date", "Port_Name", "Event_Status", "EventRemark" }, "EventMembers");


        rpt1.DataSource = ds;
        rpt1.DataMember = "Events";
        rpt1.DataBind();

        

    }
    protected int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }
    protected void rpt2_ItemCommand(Object Sender, RepeaterCommandEventArgs e)
    {
        
    }
    protected void rpt2_ItemDataBound(Object Sender, RepeaterItemEventArgs e)
    {
        try
        {
            string CrewID = Request.QueryString["CrewID"];

            DataRow dr = (DataRow)e.Item.DataItem;

            if (dr["CrewID_On"].ToString() == CrewID)
            {
                HyperLink lnkStaffCode_on = (HyperLink)e.Item.FindControl("lnkStaffCode_on");
                if (lnkStaffCode_on != null)
                {
                    lnkStaffCode_on.BackColor = System.Drawing.Color.Yellow;
                }

            }
            if (dr["CrewID_Off"].ToString() == CrewID)
            {
                HyperLink lnkStaffCode_off = (HyperLink)e.Item.FindControl("lnkStaffCode_off");
                if (lnkStaffCode_off != null)
                {                    
                    lnkStaffCode_off.BackColor = System.Drawing.Color.Yellow;
                }
                
            }
            
        }
        catch { }

    }
    protected void rpt1_ItemCommand(Object Sender, RepeaterCommandEventArgs e)
    {
        try
        {
        }
        catch
        {
        }
    }
    protected void rpt1_ItemDataBound(Object Sender, RepeaterItemEventArgs e)
    {
      

    }

}