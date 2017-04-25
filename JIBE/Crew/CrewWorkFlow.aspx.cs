using System;
using System.Web.UI.WebControls;
using SMS.Business.Crew;
using System.Data;

public partial class Crew_CrewWorkFlow : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (GetSessionUserID() == 0)
            Response.Redirect("~/account/login.aspx");

        if (!IsPostBack)
        {
            if (GetCrewID() != 0)
            {
                Get_CrewWorkflow(GetCrewID());
                Load_CrewPersonalDetails(GetCrewID());
            }
        }
    }

    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }

    private int GetCrewID()
    {
        if (Request.QueryString["CrewID"] != null)
            return int.Parse(Request.QueryString["CrewID"].ToString());
        else
            return 0;
    }

    protected void Get_CrewWorkflow(int CrewID)
    {
        try
        {
            DataSet ds = new BLL_Crew_CrewDetails().Get_CrewWorkflow(CrewID,GetSessionUserID());
            ds.Relations.Add(new DataRelation("Parent", ds.Tables[0].Columns["ID"], ds.Tables[1].Columns["PID"]));
            ds.Tables[1].TableName = "Child";
            rpt1.DataSource = ds;
            rpt1.DataBind();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    protected void rpt1_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView dv = e.Item.DataItem as DataRowView;
        if (dv != null)
        {
            Repeater nestedRepeater = e.Item.FindControl("rpt2") as Repeater;
            if (nestedRepeater != null)
            {
                nestedRepeater.DataSource = dv.CreateChildView("Parent");
                nestedRepeater.DataBind();
            }
        }
    }

    protected void Load_CrewPersonalDetails(int ID)
    {
        try
        {
            DataTable dt = new BLL_Crew_CrewDetails().Get_CrewPersonalDetailsByID(ID);
            if (dt.Rows.Count > 0)
            {
                lblStaffName.Text = dt.Rows[0]["STAFF_FULLNAME"].ToString();
                lblStaffCode.Text = dt.Rows[0]["STAFF_CODE"].ToString();
                lblRank.Text = dt.Rows[0]["RANK_NAME"].ToString();
                hdnCrewrank.Value = dt.Rows[0]["CurrentRankID"].ToString();
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
}