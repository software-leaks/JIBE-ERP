using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using System.Data;
using ClsBLLTechnical;
using CrystalDecisions.Shared;
using   SMS.Business.PURC ;



public partial class Technical_INV_ReqPoolReportShow : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string pathrptPO = Server.MapPath(".");
        DataSet dsrpt;
        DataSet  dsReqSumm;
        ReportDocument objrptPO = new ReportDocument();
        TechnicalBAL objbal = new TechnicalBAL();

        //  Session["sType"]
        string Selection = Session["sType"].ToString();

        string strRptPath = Server.MapPath(".");

        switch (Selection)
        {
            case "NRQ" :
                using ( BLL_PURC_Purchase  objTechService = new  BLL_PURC_Purchase ())
                {
                    dsrpt = objTechService.GetReqItemsPreview(Request.QueryString["Requisitioncode"].ToString(), Request.QueryString["Vessel_Code"].ToString(), Request.QueryString["Document_Code"].ToString());
                     
                }
                ConnectionInfo cInfo2 = new ConnectionInfo();
                TableLogOnInfo logOnInfo2 = new TableLogOnInfo();
                string connstring = System.Configuration.ConfigurationManager.ConnectionStrings["smsconn"].ToString();
                string[] conn2 = connstring.ToString().Split(';');
                string[] serverInfo2 = conn2[0].ToString().Split('=');
                string[] DbInfo2 = conn2[1].ToString().Split('=');
                string[] userInfo2 = conn2[2].ToString().Split('=');
                string[] passwordInfo2 = conn2[3].ToString().Split('=');

                cInfo2.ServerName = serverInfo2[1].ToString();
                cInfo2.DatabaseName = DbInfo2[1].ToString();
                cInfo2.UserID = userInfo2[1].ToString();
                cInfo2.Password = passwordInfo2[1].ToString();

                CrystalReportsFile.RequisitionItemsPreview ReqItmPreview = new CrystalReportsFile.RequisitionItemsPreview();

                foreach (CrystalDecisions.CrystalReports.Engine.Table reportTable in ReqItmPreview.Database.Tables)
                {
                    logOnInfo2 = reportTable.LogOnInfo;
                    logOnInfo2.ConnectionInfo = cInfo2;
                    reportTable.ApplyLogOnInfo(logOnInfo2);
                }
                ReqItmPreview.SetDataSource(dsrpt.Tables[0]);
                CrystalReportViewerPOAPR.ReportSource = ReqItmPreview;
                CrystalReportViewerPOAPR.DisplayToolbar = true;
                break;
 
            case "PFA":
                dsrpt = objbal.GetPMS_Report_POApproval(Session["sVesselCode"].ToString(), Session["sDeptCode"].ToString());

                ConnectionInfo cInfo = new ConnectionInfo();
                TableLogOnInfo logOnInfo = new TableLogOnInfo();
                connstring = System.Configuration.ConfigurationManager.ConnectionStrings["smsconn"].ToString();
                string[] conn = connstring.ToString().Split(';');
                string[] serverInfo = conn[0].ToString().Split('=');
                string[] DbInfo = conn[1].ToString().Split('=');
                string[] userInfo = conn[2].ToString().Split('=');
                string[] passwordInfo = conn[3].ToString().Split('=');

                cInfo.ServerName = serverInfo[1].ToString();
                cInfo.DatabaseName = DbInfo[1].ToString();
                cInfo.UserID = userInfo[1].ToString();
                cInfo.Password = passwordInfo[1].ToString();

               
                CrystalReports.REQDeliveryStatus objREQDeliveryStatus = new CrystalReports.REQDeliveryStatus();

                foreach (CrystalDecisions.CrystalReports.Engine.Table reportTable in objREQDeliveryStatus.Database.Tables)
                {
                    logOnInfo = reportTable.LogOnInfo;
                    logOnInfo.ConnectionInfo = cInfo;
                    reportTable.ApplyLogOnInfo(logOnInfo);
                }
                objREQDeliveryStatus.SetDataSource(dsrpt.Tables[0]);
                CrystalReportViewerPOAPR.ReportSource = objREQDeliveryStatus;
                CrystalReportViewerPOAPR.DisplayToolbar = true;                
                break;
            
            case "DVS":
                dsrpt = objbal.GetPMS_Report_DeliveryStatus(Session["sVesselCode"].ToString(), Session["sDeptCode"].ToString());


                ConnectionInfo cInfo1 = new ConnectionInfo();
                TableLogOnInfo logOnInfo1 = new TableLogOnInfo();
                string connstring1 = System.Configuration.ConfigurationManager.ConnectionStrings["smsconn"].ToString();
                string[] conn1 = connstring1.ToString().Split(';');
                string[] serverInfo1 = conn1[0].ToString().Split('=');
                string[] DbInfo1 = conn1[1].ToString().Split('=');
                string[] userInfo1 = conn1[2].ToString().Split('=');
                string[] passwordInfo1 = conn1[3].ToString().Split('=');

                cInfo1.ServerName = serverInfo1[1].ToString();
                cInfo1.DatabaseName = DbInfo1[1].ToString();
                cInfo1.UserID = userInfo1[1].ToString();
                cInfo1.Password = passwordInfo1[1].ToString();

                CrystalReports.POApproval objPOApproval = new CrystalReports.POApproval();

                foreach (CrystalDecisions.CrystalReports.Engine.Table reportTable in objPOApproval.Database.Tables)
                {
                    logOnInfo1 = reportTable.LogOnInfo;
                    logOnInfo1.ConnectionInfo = cInfo1;
                    reportTable.ApplyLogOnInfo(logOnInfo1);
                }
                objPOApproval.SetDataSource(dsrpt.Tables[0]);
                CrystalReportViewerPOAPR.ReportSource = objPOApproval;
                CrystalReportViewerPOAPR.DisplayToolbar = true; 

                //objrptPO.Load(pathrptPO + @"\REQDeliveryStatus.rpt");
                //objrptPO.SetDataSource(dsrpt);
                //CrystalReportViewerPOAPR.ReportSource = objrptPO;
                //CrystalReportViewerPOAPR.DataBind();
                break;

            case "ARQ":

                objbal = new TechnicalBAL();
                dsReqSumm = new DataSet();
                dsReqSumm = objbal.GetReqsnOrderApprovalSummary(Request.QueryString["Requisitioncode"].ToString(), Request.QueryString["Document_Code"].ToString(), Request.QueryString["Vessel_Code"].ToString(), Request.QueryString["ORDER_CODE"].ToString(), Request.QueryString["ORDER_SUPPLIER"].ToString());
                ReportDocument rptSumryDoc = new ReportDocument();
                rptSumryDoc.Load(strRptPath + "\\RptRequisitionApproval.rpt");
                rptSumryDoc.SetDataSource(dsReqSumm.Tables[0]);
                CrystalReportViewerPOAPR.ReportSource = rptSumryDoc;
              
                CrystalReportViewerPOAPR.DisplayToolbar = true;

                break;

               

        }
    }
}
