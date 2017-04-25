using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.Crew;

public partial class Crew_Crew_ToolTip : System.Web.UI.Page
{
    BLL_Crew_CrewDetails objCrew = new BLL_Crew_CrewDetails();
    public string DFormat = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        string CrewID = Request.QueryString["CrewID"];
        DFormat = Convert.ToString(Session["User_DateFormat"]);
        if (CrewID != null)
            Load_CrewPhotoToolTip(UDFLib.ConvertToInteger(CrewID));

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
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }
    protected void Load_CrewPhotoToolTip(int CrewID)
    {
        DataSet ds = objCrew.Get_CrewInfo_ToolTip(CrewID, GetSessionUserID());

        if (ds.Tables[0].Rows.Count > 0)
        {
            // last voyage
            lblLastVessel.Text = ds.Tables[0].Rows[0]["Vessel_Short_Name"].ToString();
            lblLastSignedOn.Text = UDFLib.ConvertUserDateFormat(ds.Tables[0].Rows[0]["Sign_On_Date"].ToString(), DFormat);
            lblLastSignedOff.Text = UDFLib.ConvertUserDateFormat(ds.Tables[0].Rows[0]["Sign_Off_Date"].ToString(), DFormat); 
        }

        // last two remarks
        GridView_CrewRemarks.DataSource = ds.Tables[1];
        GridView_CrewRemarks.DataBind();

        //Card Status
        if (ds.Tables[2].Rows.Count > 0)
        {
            lblCardStatus.Text = ds.Tables[2].Rows[0]["cardtype"].ToString() + " " + ds.Tables[2].Rows[0]["cardstatus"].ToString();
            imgCardStatus.ImageUrl = "../images/" + ds.Tables[2].Rows[0]["cardtype"].ToString().Replace(" ", "") + "_" + ds.Tables[2].Rows[0]["cardstatus"].ToString() + ".png";
            imgCardStatus.Visible = true;
        }

        Load_CrewComplaints(CrewID);

    }

    protected void Load_CrewComplaints(int CrewID)
    {

        DataSet ds = objCrew.Get_CrewComplaints(CrewID, int.Parse(Session["USERID"].ToString()));

        UDFLib.AddParentTable(ds.Tables[0], "Complaints", new string[] { "WORKLIST_ID", "VESSEL_ID" },
        new string[] { "VESSEL_NAME", "JOB_DESCRIPTION", "STATUS" }, "EscLog");

        UDFLib.AddParentTable(ds.Tables[1], "MyComplaints", new string[] { "WORKLIST_ID", "VESSEL_ID" },
        new string[] { "VESSEL_NAME", "JOB_DESCRIPTION", "STATUS" }, "MyLog");


        rptMyComplaints.DataSource = ds;
        rptMyComplaints.DataMember = "MyComplaints";
        rptMyComplaints.DataBind();


    }

    

}