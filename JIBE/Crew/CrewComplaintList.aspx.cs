using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.Crew;

public partial class Crew_CrewComplaintList : System.Web.UI.Page
{
    BLL_Crew_CrewDetails objBLLCrew = new BLL_Crew_CrewDetails();
    protected void Page_Load(object sender, EventArgs e)
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
                Load_CrewComplaints();
            }
        }
    }
    
    protected void Load_CrewComplaints()
    {

        DataSet ds = objBLLCrew.Get_CrewComplaintList(1, int.Parse(Request.QueryString["USERID"].ToString()));
               
        rptComplaintsToDPA.DataSource = ds.Tables[0];
        rptComplaintsToDPA.DataBind();


        DataSet dsPending = objBLLCrew.Get_CrewComplaintList(0, int.Parse(Request.QueryString["USERID"].ToString()));

        rptAllComplaints.DataSource = dsPending.Tables[0];
        rptAllComplaints.DataBind();
        

    }
}