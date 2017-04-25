using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MERunningHoursMonthlyReport;

public partial class eForms_eFormTempletes_MERunningHoursMonthlyReport : System.Web.UI.Page
{
    int Form_ID;
    int Dtl_Report_ID;
    int Vessel_ID;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Form_ID = UDFLib.ConvertToInteger(Request.QueryString["Form_ID"]);
            Dtl_Report_ID = UDFLib.ConvertToInteger(Request.QueryString["DtlID"]);
            Vessel_ID = UDFLib.ConvertToInteger(Request.QueryString["VID"]);

            Load_ME_Report_Details(Dtl_Report_ID, Vessel_ID);
        }
    }
    private void Load_ME_Report_Details(int Report_ID, int Vessel_ID)
    {
        DataSet ds = BLL_FRM_MERunningHoursMonthlyReport.Get_ME_RHrs_ReportDetails(Report_ID, Vessel_ID);

        frmMain.DataSource = ds.Tables[0];
        frmMain.DataBind();

        DataTable dtItems = ds.Tables[1];
        GridView GridView_RunningHours = (GridView)frmMain.FindControl("GridView_RunningHours");

        if (dtItems.Rows.Count > 0)
        {
            GridView_RunningHours.DataSource = dtItems;
            GridView_RunningHours.DataBind();
        }

        
    }
}