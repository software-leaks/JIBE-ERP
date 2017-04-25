using System;
using System.Data;
using System.Collections.Specialized;
using SMS.Business.Reports;


public partial class PortageBill_PhoneCardReports_ReportsView : System.Web.UI.Page
{
    string _reportfile = "";
    string _filename = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        string[] reportparameters = Request.QueryString.AllKeys;
        NameValueCollection nvc = new NameValueCollection();


        if (!IsPostBack)
        {
            foreach (string s in reportparameters)
                nvc.Add(s, Request.QueryString[s]);

            Session["REPORTPARAMETERS"] = nvc;
        }
        BindReport();
    }

    public void BindReport()
    {
        try
        {
            CommonReports rpt = new CommonReports();
            DataTable dt = GetReportData();

            UDFLib.ChangeColumnDataType(dt, "PBILL_Date", typeof(string));

            for (int i = 0; i < dt.Rows.Count; i++)
                dt.Rows[i]["PBILL_DATE"] = UDFLib.ConvertUserDateFormat(Convert.ToString(dt.Rows[i]["PBILL_DATE"]));

            rpt.ResourceName = _reportfile;
            rpt.SetDataSource(dt);
            rpt.Site = this.Site;

            CrystalReportViewer1.Visible = true;
            CrystalReportViewer1.ReportSource = rpt;

            CrystalReportViewer1.DisplayToolbar = true;
            CrystalReportViewer1.HasPrintButton = false;
            CrystalReportViewer1.HasExportButton = false;
            CrystalReportViewer1.HasToggleGroupTreeButton = false;
            CrystalReportViewer1.DisplayGroupTree = false;

        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    private DataTable GetReportData()
    {
        NameValueCollection nvc = (NameValueCollection)Session["REPORTPARAMETERS"];
        DataTable dt = new DataTable();
        return GetReportData(nvc, ref _reportfile, ref _filename);
    }
    public static DataTable GetReportData(NameValueCollection nvc, ref string reportfile, ref string filename)
    {
        return PhoneCardReports.GetReportData(nvc, ref reportfile, ref filename);
        throw new Exception("Report application not implemented correctly. Requires Code to be checked and corrected");
    }
}