using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using SMS.Business.Crew;
using SMS.Business.Infrastructure;
using SMS.Properties;

public partial class Crew_CrewDetails_CrewComplaints : System.Web.UI.Page
{
    BLL_Crew_CrewDetails objBLLCrew = new BLL_Crew_CrewDetails();
    BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
    UserAccess objUA = new UserAccess();

    protected override void OnInit(EventArgs e)
    {
        try
        {
            base.Page.Header.Controls.Add(SetUserStyle.AddThemeInHeader());
            base.OnInit(e);
        }
        catch { }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            UserAccessValidation();
            int CrewID = UDFLib.ConvertToInteger(Request.QueryString["CrewID"]);
            if (objUA.View == 1)
            {
                pnlViewCrewComplaints.Visible = true;
                Load_CrewComplaints(CrewID);
            }
        }
    }

    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
            lblMsg.Text = "You don't have sufficient privilege to access the requested information.";

        if (objUA.Add == 0)
        {
        }
        if (objUA.Edit == 0)
        {
        }
        if (objUA.Delete == 0)
        {
        }
        if (objUA.Approve == 0)
        {
        }
        //-- MANNING OFFICE LOGIN --

    }
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }
    public int GetCrewID()
    {
        try
        {
            if (Request.QueryString["CrewID"] != null)
            {
                return int.Parse(Request.QueryString["CrewID"].ToString());
            }
            else
                return 0;
        }
        catch { return 0; }
    }

    protected void Load_CrewComplaints(int CrewID)
    {
        try
        {


            DataSet ds = objBLLCrew.Get_CrewComplaints(CrewID, int.Parse(Session["USERID"].ToString()));

            UDFLib.AddParentTable(ds.Tables[0], "Complaints", new string[] { "WORKLIST_ID", "VESSEL_ID" },
            new string[] { "VESSEL_NAME", "JOB_DESCRIPTION", "STATUS" }, "EscLog");

            UDFLib.AddParentTable(ds.Tables[1], "MyComplaints", new string[] { "WORKLIST_ID", "VESSEL_ID" },
            new string[] { "VESSEL_NAME", "JOB_DESCRIPTION", "STATUS" }, "MyLog");


            rptComplaintsToMe.DataSource = ds;
            rptComplaintsToMe.DataMember = "Complaints";
            rptComplaintsToMe.DataBind();

            rptMyComplaints.DataSource = ds;
            if (ds.Tables["Table1"].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables["Table1"].Rows)
                {
                    if (!string.IsNullOrEmpty(dr["ESCALATED_ON"].ToString()))
                    {
                        dr["ESCALATED_ON"] = UDFLib.ConvertUserDateFormat(Convert.ToString(dr["ESCALATED_ON"].ToString()), UDFLib.GetDateFormat());
                    }
                }
            }
            rptMyComplaints.DataMember = "MyComplaints";
            rptMyComplaints.DataBind();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
}