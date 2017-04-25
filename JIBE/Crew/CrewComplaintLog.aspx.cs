using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.Crew;

public partial class Crew_CrewComplaintLog : System.Web.UI.Page
{
    BLL_Crew_CrewDetails objBLLCrew = new BLL_Crew_CrewDetails();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["USERID"] == null)
        {
            lblMsg.Text = "Session Expired!! Log-out and log-in again.";
        }
        else
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["USERID"] == null)
                {
                    //Response.Write("Session Expired!!");
                    Response.End();
                }
                else
                {
                    Load_CrewComplaintsLog();
                }
            }
        }
    }

    
    protected void Load_CrewComplaintsLog()
    {
        int Worklist_ID = UDFLib.ConvertToInteger(Request.QueryString["WLID"].ToString());
        int Vessel_ID = UDFLib.ConvertToInteger(Request.QueryString["VID"].ToString());
        int USERID = UDFLib.ConvertToInteger(Request.QueryString["USERID"].ToString());

        DataSet ds = objBLLCrew.Get_CrewComplaintLog(Worklist_ID, Vessel_ID, USERID);

        rptComplaintsToDPA.DataSource = ds.Tables[0];
        rptComplaintsToDPA.DataBind();



    }
}