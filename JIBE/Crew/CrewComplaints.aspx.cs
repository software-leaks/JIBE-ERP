using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.Crew;
using System.IO;
using System.Text;

public partial class Crew_CrewComplaints : System.Web.UI.Page
{
    BLL_Crew_CrewDetails objBLLCrew = new BLL_Crew_CrewDetails();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["USERID"] == null)
            {
                Response.Write("Session Expired!!");
                Response.End();
            }
            else
            {
                Load_CrewComplaints();
            }
        }
    }
    public int GetCrewID()
    {
        try
        {
            if (Request.QueryString["ID"] != null)
            {
                return int.Parse(Request.QueryString["ID"].ToString());
            }
            else
                return 0;
        }
        catch { return 0; }
    }

    protected void Load_CrewComplaints()
    {
        int CrewID = GetCrewID();
        DataSet ds = objBLLCrew.Get_CrewComplaints(CrewID, int.Parse(Session["USERID"].ToString()));

        UDFLib.AddParentTable(ds.Tables[0], "Complaints", new string[] { "WORKLIST_ID", "VESSEL_ID" },
        new string[] { "VESSEL_NAME", "JOB_DESCRIPTION", "STATUS" }, "EscLog");

        UDFLib.AddParentTable(ds.Tables[1], "MyComplaints", new string[] { "WORKLIST_ID", "VESSEL_ID" },
        new string[] { "VESSEL_NAME", "JOB_DESCRIPTION", "STATUS" }, "MyLog");


        rptComplaintsToMe.DataSource = ds;
        rptComplaintsToMe.DataMember = "Complaints";
        rptComplaintsToMe.DataBind();

        rptMyComplaints.DataSource = ds;
        rptMyComplaints.DataMember = "MyComplaints";
        rptMyComplaints.DataBind();

    }
}