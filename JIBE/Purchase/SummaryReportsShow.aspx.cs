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
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.ReportSource;
using System.Data.SqlClient;
using System.Data.Common;
using SMS.Business.PURC;
using SMS.Properties;
using ClsBLLTechnical;

 
 


public partial class Technical_INV_SummaryReportsShow : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string strRptPath = Server.MapPath(".");
        string Selection = Request.QueryString["RptType"].ToString();
        DataSet dsReqSumm;
        TechnicalBAL objtechBAL;

        ConnectionInfo cInfo = new ConnectionInfo();
        TableLogOnInfo logOnInfo = new TableLogOnInfo();

        string connstring = System.Configuration.ConfigurationManager.ConnectionStrings["smsconn"].ToString();
        string[] conn = connstring.ToString().Split(';');
        string[] serverInfo = conn[0].ToString().Split('=');
        string[] DbInfo = conn[1].ToString().Split('=');
        string[] userInfo = conn[2].ToString().Split('=');
        string[] passwordInfo = conn[3].ToString().Split('=');

        cInfo.ServerName = serverInfo[1].ToString();
        cInfo.DatabaseName = DbInfo[1].ToString();
        cInfo.UserID = userInfo[1].ToString();
        cInfo.Password = passwordInfo[1].ToString();

        switch (Selection)
        {
            //For Requistion Summary
            case "ReqSumry":

                objtechBAL = new TechnicalBAL();
                dsReqSumm = new DataSet();
                dsReqSumm = objtechBAL.GetRequisitionSummary(Request.QueryString["REQUISITION_CODE"].ToString(), Request.QueryString["document_code"].ToString(), Request.QueryString["Vessel_Code"].ToString());
                ReportDocument rptSumryDoc = new ReportDocument();
                rptSumryDoc.Load(strRptPath + "\\RptRequisitionSummary.rpt");
                rptSumryDoc.OpenSubreport("Items").SetDataSource(dsReqSumm.Tables[1]);
                rptSumryDoc.SetDataSource(dsReqSumm.Tables[0]);
                SummaryReportViewer.ReportSource = rptSumryDoc;
                SummaryReportViewer.DisplayGroupTree = false;
                SummaryReportViewer.DisplayToolbar = true;
                
                break;

            //For Quotation  Summary
            case "QtnSumry":

                objtechBAL = new TechnicalBAL();
                dsReqSumm = new DataSet();
                dsReqSumm = objtechBAL.GetRequQuotationSummary(Request.QueryString["REQUISITION_CODE"].ToString(), Request.QueryString["document_code"].ToString(), Request.QueryString["Vessel_Code"].ToString(), Request.QueryString["QUOTATION_CODE"].ToString());

                ReportDocument rptSumryDocQtn = new ReportDocument();
                rptSumryDocQtn.Load(strRptPath + "\\RptQuotationSummary.rpt");
                rptSumryDocQtn.OpenSubreport("RptSubRFQSent").SetDataSource(dsReqSumm.Tables[3]);
                rptSumryDocQtn.OpenSubreport("RptSubQuotationReceived").SetDataSource(dsReqSumm.Tables[2]);
                rptSumryDocQtn.SetDataSource(dsReqSumm.Tables[0]);
                SummaryReportViewer.ReportSource = rptSumryDocQtn;
                SummaryReportViewer.DisplayGroupTree = false;
                SummaryReportViewer.DisplayToolbar = true;

                break;

            //For Delivery Order Summary
            case "DelvSumry":
                objtechBAL = new TechnicalBAL();
                dsReqSumm = new DataSet();
                dsReqSumm = objtechBAL.GetDeliveryOrderSummary(Request.QueryString["REQUISITION_CODE"].ToString(), Request.QueryString["document_code"].ToString(), Request.QueryString["Vessel_Code"].ToString(), Request.QueryString["DELIVERY_CODE"].ToString());

                ReportDocument rptSumryDelv = new ReportDocument();
                rptSumryDelv.Load(strRptPath + "\\RptDeliverOrderSummary.rpt");
                rptSumryDelv.OpenSubreport("RptSubDeliverOrderSummaryItem").SetDataSource(dsReqSumm.Tables[2]);
                rptSumryDelv.OpenSubreport("RptSubDeliverOrderSummaryAttachment").SetDataSource(dsReqSumm.Tables[1]);
                rptSumryDelv.SetDataSource(dsReqSumm.Tables[0]);
                SummaryReportViewer.ReportSource = rptSumryDelv;
                SummaryReportViewer.DisplayGroupTree = false;
                SummaryReportViewer.DisplayToolbar = true;

                break;
              
               
        }

    }
    protected void SummaryReportViewer_Init(object sender, EventArgs e)
    {

    }
}
