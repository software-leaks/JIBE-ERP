using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using System.Data;
using SMS.Business.eForms;
using CrystalDecisions.Web;

public partial class eForms_eFormTempletes_DrillReportViewer : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        ReportDocument reportDocumentObj = new ReportDocument();
        reportDocumentObj.Load(Server.MapPath("~/eForms/eFormTempletes/DrillReport.rpt"));

        DataSet ds = BLL_FRM_LIB_DrillReport.Get_DRILL_REPORT_Log(Convert.ToInt32(Request.QueryString["Vessel_ID"]), Convert.ToInt32(Request.QueryString["Schedule_Id"]), Convert.ToInt32(Request.QueryString["Office_Id"]));





        reportDocumentObj.SetDataSource(ds);
        reportDocumentObj.Subreports["DrillReportDtl"].SetDataSource(ds.Tables[1]);
        reportDocumentObj.Subreports["DrillReportAns"].SetDataSource(ds.Tables[2]);
        reportDocumentObj.Database.Tables[0].SetDataSource(ds.Tables[0]);
        reportDocumentObj.Subreports["DrillReportDtl"].Database.Tables[0].SetDataSource(ds.Tables[1]);
        reportDocumentObj.Subreports["DrillReportAns"].Database.Tables[0].SetDataSource(ds.Tables[2]); 
        CrystalReportViewer1.ReportSource = reportDocumentObj;
        CrystalReportViewer1.DisplayToolbar = true;
        CrystalReportViewer1.DisplayGroupTree = false;

    }
}