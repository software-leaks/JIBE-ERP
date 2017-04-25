using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using SMS.Business.Operation;


public partial class Operations_BunkerReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["BunkerReportId"] != null)
            {
                string[] filters = Request.QueryString["filters"].Split('~');
                DataTable dtReports = BLL_OPS_VoyageReports.Get_BunkerReportIndex(Convert.ToInt32(filters[0]), Convert.ToInt32(filters[1]), filters[2], filters[3], Convert.ToInt32(filters[4]));
                dtReports.PrimaryKey = new DataColumn[] { dtReports.Columns["BUNKER_REPORT_ID"] };
                ctlRecordNavigationReport.InitRecords(dtReports);
                DataRow dr = dtReports.Rows.Find(Int32.Parse(Request.QueryString["BunkerReportId"]));
                ctlRecordNavigationReport.CurrentIndex = dtReports.Rows.IndexOf(dr);

                BindReport(dr);
            }
        }
    }
    protected void BindReportDr(DataRow dr)
    {
        BindReport(dr);
    }
    public void BindReport(DataRow dr)
    {
        int BunkerReportId = dr != null ? Convert.ToInt32(dr["BUNKER_REPORT_ID"]) : Convert.ToInt32(Request.QueryString["BunkerReportId"]);
        int VesselId = dr != null ? Convert.ToInt32(dr["Vessel_Id"]) : Convert.ToInt32(Request.QueryString["VesselID"]);
        if (BunkerReportId != 0)
        {
            DataTable dtBunker = BLL_OPS_VoyageReports.Get_BunkerReport(BunkerReportId, VesselId);
            txtRemarks.Text = Convert.ToString(dtBunker.Rows[0]["REMARKS"]);
            fvBunker.DataSource = dtBunker;
            fvBunker.DataBind();
        }
    }
}