using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Operation;

public partial class Operations_PurpleReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        fvPurpleReport.DataSource = BLL_OPS_VoyageReports.Get_Purplefinder_Position(UDFLib.ConvertToInteger(Request.QueryString["id"].ToString()));
        fvPurpleReport.DataBind();
    }

    protected void lbtncrwlist_Click(object s, EventArgs e)
    {
        ResponseHelper.Redirect("~/crew/CrewList_Print.aspx?vcode=" + ((LinkButton)s).CommandArgument, "blank", "");
    }
}