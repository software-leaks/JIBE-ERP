using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Operation;
using System.Data;

public partial class Operations_VoyageReportDetails : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["id"] != null)
            {
                string[] filters = Request.QueryString["filters"].Split('~');
                DataTable dtReports = BLL_OPS_VoyageReports.Get_VoyageReportIndex(filters[0], Convert.ToInt32(filters[1]), Convert.ToInt32(filters[2]), filters[3], filters[4], Convert.ToInt32(filters[5]));
                dtReports.PrimaryKey = new DataColumn[] { dtReports.Columns["pkid"] };
                ctlRecordNavigationReport.InitRecords(dtReports);
                DataRow dr = dtReports.Rows.Find(Int32.Parse(Request.QueryString["id"]));
                ctlRecordNavigationReport.CurrentIndex = dtReports.Rows.IndexOf(dr);

                BindReport(dr);
            }
        }
    }

    protected void BindReport(DataRow dr)
    {
       string PKID = dr != null ? dr["pkid"].ToString() :Request.QueryString["id"];
       string ReportType = dr != null ? dr["Telegram_Type"].ToString() : Request.QueryString["ReportType"];
       string fullType = ReportType == "N" ? "NOON" : (ReportType == "A" ? "ARR" : (ReportType=="D"?"DEP":"PRP"));
       lblPageTitle.Text = ReportType == "N" ? "Noon Report" : (ReportType == "A" ? "Arrival Report" : (ReportType == "D" ? "Departure Report" : "Purple Report"));

        if (!string.IsNullOrWhiteSpace(PKID))
        {
            if (dr != null)
            {
                lblVessel.Text = dr["VESSEL_NAME"].ToString();
                hplcrewlist.NavigateUrl = "~/crew/CrewList_Print.aspx?vcode=" + Convert.ToString(dr["Vessel_Short_Name"]);
                Page.Title = dr["Vessel_Short_Name"].ToString() + " / " + fullType + " / " + dr["STRTELEGRAM_DATE"].ToString();
            }
            String msgretv = String.Format("showdetails('" + ReportType + "'," + PKID + "); LoadRecordInfo("+PKID+");");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgret6v", msgretv, true);
        }

    }

}