using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using System.Data;
using SMS.Business.Operation;

public partial class Operations_CR_VoyageReportView : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        

        ReportDocument reportDocumentObj = new ReportDocument();
        reportDocumentObj.Load(Server.MapPath("~/operations/CR_VoyageReport.rpt"));
        
        DataTable dtreport = BLL_OPS_VoyageReports.Get_VoyageReport(Request.QueryString["REPORTTYPE"], Convert.ToInt32(Request.QueryString["VESSELID"]), Convert.ToInt32(Request.QueryString["LOCATIONCODE"]), Request.QueryString["FROMDT"], Request.QueryString["TODT"], Convert.ToInt32(Request.QueryString["FLEETID"]));
        reportDocumentObj.SetDataSource(dtreport);
        CrystalReportViewer.ReportSource = reportDocumentObj;
        CrystalReportViewer.DisplayToolbar = true;
        CrystalReportViewer.DisplayGroupTree = false;
      
     
      
    }
}