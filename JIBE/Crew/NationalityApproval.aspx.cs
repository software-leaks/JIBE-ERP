using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.Crew;

public partial class Crew_NationalityApproval : System.Web.UI.Page
{
    BLL_Crew_CrewDetails objCrew = new BLL_Crew_CrewDetails();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Load_ApprovalRequests();
        }
    }
    private int GetSessionUserID()
    {
        return UDFLib.ConvertToInteger(Session["USERID"]);
    }
    protected void Load_ApprovalRequests()
    {
        int RequestID = UDFLib.ConvertToInteger(Request.QueryString["RequestID"]);
        DataTable dt = objCrew.Get_NationalityCheck_Approvals(GetSessionUserID(), RequestID);

        GridView1.DataSource = dt;
        GridView1.DataBind();

    }

    protected void btnApprove_Click(object sender, CommandEventArgs e)
    {
        GridViewRow gvr = (sender as Button).Parent.Parent as GridViewRow ;
        int ID = UDFLib.ConvertToInteger( e.CommandArgument.ToString());
        string Remarks = (gvr.FindControl("txtApproverRemarks") as TextBox).Text;

        int Res = objCrew.NationalityCheck_Approval(ID, Remarks, 1, GetSessionUserID());

        Load_ApprovalRequests();
    }

    protected void btnReject_Click(object sender, CommandEventArgs e)
    {
        GridViewRow gvr = (sender as Button).Parent.Parent as GridViewRow;
        int ID = UDFLib.ConvertToInteger(e.CommandArgument.ToString());
        string Remarks = (gvr.FindControl("txtApproverRemarks") as TextBox).Text;

        int Res = objCrew.NationalityCheck_Approval(ID, Remarks, -1, GetSessionUserID());

        Load_ApprovalRequests();
    }
}