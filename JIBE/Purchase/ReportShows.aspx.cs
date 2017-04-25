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



public partial class Technical_INV_ReportShows : System.Web.UI.Page
{

    ParameterDiscreteValue crtParamDiscreteValue = new ParameterDiscreteValue();
    ParameterField crtParamField = new ParameterField();
    ParameterFields crtParamFields = new ParameterFields();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {

                string strRptPath = Server.MapPath(".");
                DataTable dtbl = new DataTable();
                DataTable dataforDisplay = new DataTable();
                dataforDisplay = (DataTable)Session["ReportData"];
                ConnectionInfo cInfo = new ConnectionInfo();
                TableLogOnInfo logOnInfo = new TableLogOnInfo();



                //string connstring = System.Configuration.ConfigurationManager.ConnectionStrings["elogcon"].ToString();
                //cInfo.ServerName = "elogsrv";
                //cInfo.DatabaseName = "eLog";
                //cInfo.UserID = "sa";
                //cInfo.Password = "eLog!234";


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


                int value = Int32.Parse(Request.QueryString["ReportType"].ToString());

                switch (value)
                {
                    case 1:

                        CrystalReportsFile.C1_BondItemOwner objC1_BondItemOwner = new CrystalReportsFile.C1_BondItemOwner();

                        foreach (CrystalDecisions.CrystalReports.Engine.Table reportTable in objC1_BondItemOwner.Database.Tables)
                        {
                            logOnInfo = reportTable.LogOnInfo;
                            logOnInfo.ConnectionInfo = cInfo;
                            reportTable.ApplyLogOnInfo(logOnInfo);
                        }
                        objC1_BondItemOwner.SetDataSource(dataforDisplay);
                        CrystalReportViewer.ReportSource = objC1_BondItemOwner;
                        CrystalReportViewer.DisplayToolbar = true;
                        break;


                    case 2:

                        CrystalReportsFile.C1_BondItemCharter objC1_BondItemCharter = new CrystalReportsFile.C1_BondItemCharter();

                        foreach (CrystalDecisions.CrystalReports.Engine.Table reportTable in objC1_BondItemCharter.Database.Tables)
                        {
                            logOnInfo = reportTable.LogOnInfo;
                            logOnInfo.ConnectionInfo = cInfo;
                            reportTable.ApplyLogOnInfo(logOnInfo);
                        }
                        objC1_BondItemCharter.SetDataSource(dataforDisplay);
                        CrystalReportViewer.ReportSource = objC1_BondItemCharter;
                        CrystalReportViewer.DisplayToolbar = true;
                        break;


                    case 3:


                        DataSet dsProvisionOwner = (DataSet)Session["ProvisionOwner"];
                        CrystalReportsFile.C1_ProvisionItemOwner objC1_ProvisionItemOwner = new CrystalReportsFile.C1_ProvisionItemOwner();
                        foreach (CrystalDecisions.CrystalReports.Engine.Table reportTable in objC1_ProvisionItemOwner.Database.Tables)
                        {
                            logOnInfo = reportTable.LogOnInfo;
                            logOnInfo.ConnectionInfo = cInfo;
                            reportTable.ApplyLogOnInfo(logOnInfo);
                        }

                        if (dsProvisionOwner.Tables[1].Rows.Count != 0)
                            objC1_ProvisionItemOwner.DataDefinition.FormulaFields[0].Text = dsProvisionOwner.Tables[1].Rows[0][0].ToString();

                        objC1_ProvisionItemOwner.SetDataSource(dsProvisionOwner.Tables[0]);
                        CrystalReportViewer.ReportSource = objC1_ProvisionItemOwner;
                        CrystalReportViewer.DisplayToolbar = true;
                        break;


                    case 4:

                        DataSet dsProvisionCharter = (DataSet)Session["ProvisionCharters"];
                        CrystalReportsFile.C1_ProvisionItemCharter objC1_ProvisionItemCharter = new CrystalReportsFile.C1_ProvisionItemCharter();
                        foreach (CrystalDecisions.CrystalReports.Engine.Table reportTable in objC1_ProvisionItemCharter.Database.Tables)
                        {
                            logOnInfo = reportTable.LogOnInfo;
                            logOnInfo.ConnectionInfo = cInfo;
                            reportTable.ApplyLogOnInfo(logOnInfo);
                        }

                        if (dsProvisionCharter.Tables[1].Rows.Count != 0)
                            objC1_ProvisionItemCharter.DataDefinition.FormulaFields[0].Text = dsProvisionCharter.Tables[1].Rows[0][1].ToString();

                        objC1_ProvisionItemCharter.SetDataSource(dsProvisionCharter.Tables[0]);
                        CrystalReportViewer.ReportSource = objC1_ProvisionItemCharter;
                        CrystalReportViewer.DisplayToolbar = true;
                        break;

                    case 5:

                        DataSet dsC2 = (DataSet)Session["DataSetC2"];

                        CrystalReportsFile.C2_MandaysReports objC2_MandaysReports = new CrystalReportsFile.C2_MandaysReports();
                        foreach (CrystalDecisions.CrystalReports.Engine.Table reportTable in objC2_MandaysReports.Database.Tables)
                        {
                            logOnInfo = reportTable.LogOnInfo;
                            logOnInfo.ConnectionInfo = cInfo;
                            reportTable.ApplyLogOnInfo(logOnInfo);
                        }

                        // c2Report.Load(@"D:\Projects\elog_client_wcf\Technical\INV\C2_MandaysReports.rpt");

                        ReportDocument c2Report = new ReportDocument();
                        c2Report.Load(strRptPath + "\\C2_MandaysReports.rpt");

                        if (dsC2.Tables.Count > 0)
                        {

                            if (dsC2.Tables[0].Rows.Count > 0)
                            {
                                c2Report.SetDataSource(dsC2.Tables[0]);
                                decimal total_val = 0;

                                foreach (DataRow dr in dsC2.Tables[0].Rows)
                                {
                                    total_val = total_val + Convert.ToDecimal(dr.ItemArray[5].ToString());
                                }

                                if (dsC2.Tables[1].Rows.Count > 0)
                                {
                                    c2Report.OpenSubreport("ConsumptionReports").SetDataSource(dsC2.Tables[1]);
                                    decimal Mans = 0;
                                    foreach (DataRow dr in dsC2.Tables[1].Rows)
                                    {
                                        Mans = Mans + Convert.ToDecimal(dr.ItemArray[3].ToString());
                                    }
                                    c2Report.DataDefinition.FormulaFields[1].Text = Mans.ToString();// ds.Tables[1].Rows[0].ItemArray[3].ToString();


                                    if (dsC2.Tables[2].Rows.Count > 0)
                                    {
                                        decimal total_con = 0;
                                        if (dsC2.Tables[2].Rows.Count > 0)
                                        {
                                            c2Report.DataDefinition.FormulaFields[3].Text = dsC2.Tables[2].Rows[0].ItemArray[0].ToString();
                                            total_con = Convert.ToDecimal(dsC2.Tables[2].Rows[0].ItemArray[0].ToString());
                                        }
                                        else
                                        {
                                            c2Report.DataDefinition.FormulaFields[3].Text = "0";
                                        }
                                        c2Report.DataDefinition.FormulaFields[2].Text = (total_con / Convert.ToDecimal(c2Report.DataDefinition.FormulaFields[1].Text)).ToString(); //Convert.ToDecimal(ds.Tables[1].Rows[0].ItemArray[3].ToString())).ToString();

                                        ////objC2_MandaysReports.OpenSubreport("ConsumptionReports").SetDataSource(dsC2.Tables[1]);
                                        ////objC2_MandaysReports.SetDataSource(dsC2.Tables[0]);
                                        CrystalReportViewer.ReportSource = c2Report;
                                        //CrystalReportViewer.ReportSource = objC2_MandaysReports;
                                        CrystalReportViewer.DisplayToolbar = true;
                                    }
                                }
                            }
                        }
                        break;


                    case 6:

                        DataSet dsD11 = (DataSet)Session["DatasetD11"];

                        CrystalReportsFile.D11_Report objD11_Report = new CrystalReportsFile.D11_Report();

                        foreach (CrystalDecisions.CrystalReports.Engine.Table reportTable in objD11_Report.Database.Tables)
                        {
                            logOnInfo = reportTable.LogOnInfo;
                            logOnInfo.ConnectionInfo = cInfo;
                            reportTable.ApplyLogOnInfo(logOnInfo);
                        }   
                        //objD11_Report.SetDataSource(dsD11.Tables[0]);

                        ReportDocument D11Report = new ReportDocument();


                        D11Report.Load(strRptPath + "\\D11_reports.rpt");

                        D11Report.OpenSubreport("A3Provision").SetDataSource(dsD11.Tables[1]);
                        D11Report.OpenSubreport("B3Bond").SetDataSource(dsD11.Tables[2]);
                        D11Report.OpenSubreport("LessBond").SetDataSource(dsD11.Tables[4]);
                        D11Report.OpenSubreport("LessProvision").SetDataSource(dsD11.Tables[3]);



                        //objD11_Report.Subreports[0].SetDataSource(dsD11.Tables[1]);
                        //objD11_Report.Subreports[1].SetDataSource(dsD11.Tables[2]);
                        //objD11_Report.Subreports[2].SetDataSource(dsD11.Tables[4]);
                        //objD11_Report.Subreports[3].SetDataSource(dsD11.Tables[3]);

                        if (dsD11.Tables[0].Rows.Count > 0)
                        {
                            D11Report.DataDefinition.FormulaFields[0].Text = dsD11.Tables[0].Rows[0].ItemArray[0].ToString();
                            D11Report.DataDefinition.FormulaFields[2].Text = dsD11.Tables[0].Rows[0].ItemArray[1].ToString();
                            //D11Report.DataDefinition.FormulaFields[4].Text = dsD11.Tables[0].Rows[0].ItemArray[3].ToString();
                            D11Report.DataDefinition.FormulaFields[7].Text = dsD11.Tables[0].Rows[0].ItemArray[2].ToString();
                        }

                        decimal total_amount = 0;
                        foreach (DataRow dr in dsD11.Tables[1].Rows)
                        {
                            total_amount = total_amount + Convert.ToDecimal(dr.ItemArray[4].ToString());
                        }
                        D11Report.DataDefinition.FormulaFields[2].Text = (Convert.ToDecimal(D11Report.DataDefinition.FormulaFields[0].Text) + total_amount).ToString();
                        // D11Report.DataDefinition.FormulaFields[7].Text = (Convert.ToDecimal(D11Report.DataDefinition.FormulaFields[2].Text) - Convert.ToDecimal(D11Report.DataDefinition.FormulaFields[4].Text)).ToString();


                        if (dsD11.Tables[11].Rows.Count > 0)
                        {
                            D11Report.DataDefinition.FormulaFields[7].Text = dsD11.Tables[11].Rows[1].ItemArray[0].ToString();
                            D11Report.DataDefinition.FormulaFields[4].Text = dsD11.Tables[11].Rows[0].ItemArray[0].ToString();

                        }
                        else
                        {
                            D11Report.DataDefinition.FormulaFields[7].Text = "0";
                            D11Report.DataDefinition.FormulaFields[4].Text = "0";

                        }


                        if (dsD11.Tables[7].Rows.Count > 0)
                        {
                            D11Report.DataDefinition.FormulaFields[1].Text = dsD11.Tables[7].Rows[0].ItemArray[3].ToString();
                        }
                        else
                        {
                            D11Report.DataDefinition.FormulaFields[1].Text = "0";
                        }



                        if (dsD11.Tables[8].Rows.Count > 0)
                        {
                            D11Report.DataDefinition.FormulaFields[3].Text = (Convert.ToDecimal(dsD11.Tables[8].Rows[0].ItemArray[1].ToString()) + Convert.ToDecimal(D11Report.DataDefinition.FormulaFields[1].Text)).ToString();
                        }
                        else
                        {
                            D11Report.DataDefinition.FormulaFields[3].Text = D11Report.DataDefinition.FormulaFields[1].Text;
                        }


                        if (dsD11.Tables[9].Rows.Count > 0)
                        {
                            D11Report.DataDefinition.FormulaFields[5].Text = dsD11.Tables[9].Rows[0].ItemArray[1].ToString();
                            D11Report.DataDefinition.FormulaFields[6].Text = (Convert.ToDecimal(D11Report.DataDefinition.FormulaFields[3].Text) - Convert.ToDecimal(dsD11.Tables[9].Rows[0].ItemArray[1].ToString())).ToString();
                        }
                        else
                        {
                            D11Report.DataDefinition.FormulaFields[5].Text = "0";
                            D11Report.DataDefinition.FormulaFields[6].Text = D11Report.DataDefinition.FormulaFields[3].Text;
                        }

                        decimal mandays = 0;
                        foreach (DataRow dr in dsD11.Tables[3].Rows)
                        {
                            if (dr.ItemArray[3].ToString() != "")
                                mandays = mandays + Convert.ToDecimal(dr.ItemArray[3].ToString());


                        }
                        D11Report.DataDefinition.FormulaFields[12].Text = (Convert.ToDouble((mandays * 5))).ToString();


                        foreach (DataRow dr in dsD11.Tables[6].Rows)
                        {
                            if (dr[0].ToString() == "Charterer Account")
                            {
                                D11Report.DataDefinition.FormulaFields[9].Text = dr[1].ToString();
                            }
                            else if (dr[0].ToString() == "Owner Account")
                            {
                                D11Report.DataDefinition.FormulaFields[10].Text = dr[1].ToString();
                            }
                        }



                        //if (dsD11.Tables[6].Rows.Count > 0)
                        //{
                        //    D11Report.DataDefinition.FormulaFields[9].Text = dsD11.Tables[6].Rows[0].ItemArray[1].ToString();
                        //    if (dsD11.Tables[6].Rows.Count > 1)
                        //    {
                        //        D11Report.DataDefinition.FormulaFields[10].Text = dsD11.Tables[6].Rows[1].ItemArray[1].ToString();
                        //    }
                        //}(Convert.ToDecimal(rpt.DataDefinition.FormulaFields[4].Text) / mandays).ToString();

                        D11Report.DataDefinition.FormulaFields[8].Text = (Convert.ToDecimal(D11Report.DataDefinition.FormulaFields[4].Text) / mandays).ToString();
                        D11Report.DataDefinition.FormulaFields[11].Text = mandays.ToString("####0.0");



                        if (dsD11.Tables[6].Rows.Count > 1)
                            D11Report.DataDefinition.FormulaFields[12].Text = (Convert.ToDouble(mandays * 5) + Convert.ToDouble(D11Report.DataDefinition.FormulaFields[9].Text) + Convert.ToDouble(D11Report.DataDefinition.FormulaFields[10].Text)).ToString();

                        D11Report.DataDefinition.FormulaFields[13].Text = "0.00";
                        D11Report.DataDefinition.FormulaFields[14].Text = "0.00";



                        if (dsD11.Tables[10].Rows.Count > 0)
                        {
                            foreach (DataRow dr in dsD11.Tables[10].Rows)
                            {
                                if (dr[0].ToString() == "PR")
                                {
                                    D11Report.DataDefinition.FormulaFields[13].Text = dr[1].ToString();
                                }
                                else if (dr[0].ToString() == "BS")
                                {
                                    D11Report.DataDefinition.FormulaFields[14].Text = dr[1].ToString();
                                }
                            }
                        }

                        D11Report.DataDefinition.FormulaFields[2].Text = (Convert.ToDecimal(D11Report.DataDefinition.FormulaFields[2].Text) + Convert.ToDecimal(D11Report.DataDefinition.FormulaFields[13].Text)).ToString();
                        D11Report.DataDefinition.FormulaFields[3].Text = (Convert.ToDecimal(D11Report.DataDefinition.FormulaFields[3].Text) + Convert.ToDecimal(D11Report.DataDefinition.FormulaFields[14].Text)).ToString();
                        D11Report.DataDefinition.FormulaFields[6].Text = (Convert.ToDecimal(D11Report.DataDefinition.FormulaFields[6].Text) + Convert.ToDecimal(D11Report.DataDefinition.FormulaFields[14].Text)).ToString();

                        D11Report.SetDataSource(dsD11.Tables[5]);
                        CrystalReportViewer.ReportSource = D11Report;
                        CrystalReportViewer.DisplayToolbar = true;
                        break;
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message.ToString());
            }
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {

        Response.Redirect("ReportPanel.aspx");
    }
}
