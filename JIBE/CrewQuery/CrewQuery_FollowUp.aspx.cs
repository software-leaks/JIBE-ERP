using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.Crew;

public partial class CrewQuery_CrewQuery_FollowUp : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Load_FollowUps();
        }
    }
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }
    protected void Load_FollowUps()
    {
        int QID = UDFLib.ConvertToInteger(Request.QueryString["QID"]);
        int VID = UDFLib.ConvertToInteger(Request.QueryString["VID"]);

        DataTable dt = BLL_Crew_Queries.Get_CrewQuery_Followups(QID, VID, GetSessionUserID());
        rptFollowUps.DataSource = dt;
        rptFollowUps.DataBind();
    }

}