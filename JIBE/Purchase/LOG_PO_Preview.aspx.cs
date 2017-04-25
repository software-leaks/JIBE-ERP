using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.PURC;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

public partial class Purchase_LOG_PO_Preview : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       
       
            DataTable Dt = BLL_PURC_LOG.Get_Log_Raise_PO(Request.QueryString["ORDER_CODE"], Request.QueryString["LOG_ID"]);

            GeneratePOAsPDF(Dt, "", Request.QueryString["ORDER_CODE"].ToString() + ".pdf");




    }

    protected void GeneratePOAsPDF(DataTable dt, string strPath, string FileName)
    {

        string repFilePath = Server.MapPath("LOG_POReport.rpt");
        using (ReportDocument repDoc = new ReportDocument())
        {
            repDoc.Load(repFilePath);

            repDoc.SetDataSource(dt);


            decimal Total_Price = 0;
            decimal Exchane_rate = 1;
            decimal Vat = 0;
            decimal sercharge = 0;
            decimal discount = 0;
            foreach (DataRow dr in dt.Rows)
            {
                Exchane_rate = Convert.ToDecimal(dr["EXCHANGE_RATE"].ToString());
                discount = Convert.ToDecimal(dr["DISCOUNT"].ToString());
                Vat = Convert.ToDecimal(dr["VAT"].ToString());
                sercharge = Convert.ToDecimal(dr["SURCHARGES"].ToString());
                Total_Price = Total_Price + (Convert.ToDecimal(dr["REQUESTED_QTY"].ToString()) * Convert.ToDecimal(dr["QUOTED_RATE"].ToString()) - (Convert.ToDecimal(dr["REQUESTED_QTY"].ToString()) * Convert.ToDecimal(dr["QUOTED_RATE"].ToString()) * Convert.ToDecimal(dr["QUOTED_DISCOUNT"].ToString()) / 100));
            }
            repDoc.DataDefinition.FormulaFields[1].Text = (Total_Price / Exchane_rate).ToString();
            repDoc.DataDefinition.FormulaFields[2].Text = (Convert.ToDecimal(repDoc.DataDefinition.FormulaFields[1].Text) * discount / 100).ToString();
            repDoc.DataDefinition.FormulaFields[3].Text = ((Convert.ToDecimal(repDoc.DataDefinition.FormulaFields[1].Text) - (Convert.ToDecimal(repDoc.DataDefinition.FormulaFields[1].Text) * discount / 100)) * sercharge / 100).ToString();
            repDoc.DataDefinition.FormulaFields[4].Text = ((Convert.ToDecimal(repDoc.DataDefinition.FormulaFields[1].Text) - Convert.ToDecimal(repDoc.DataDefinition.FormulaFields[2].Text) + Convert.ToDecimal(repDoc.DataDefinition.FormulaFields[3].Text)) * Vat / 100).ToString();
            repDoc.DataDefinition.FormulaFields[0].Text = ((Convert.ToDecimal(repDoc.DataDefinition.FormulaFields[1].Text) - Convert.ToDecimal(repDoc.DataDefinition.FormulaFields[2].Text) + Convert.ToDecimal(repDoc.DataDefinition.FormulaFields[3].Text) + Convert.ToDecimal(repDoc.DataDefinition.FormulaFields[4].Text)).ToString());



            Response.Buffer = false;
            // Clear the response content and headers
            Response.ClearContent();
            Response.ClearHeaders();

            ExportOptions exp = new ExportOptions();
            exp.ExportFormatType = ExportFormatType.PortableDocFormat;
            PdfRtfWordFormatOptions pd = new PdfRtfWordFormatOptions();

            exp.ExportDestinationType = ExportDestinationType.NoDestination;
            exp.ExportFormatType = ExportFormatType.PortableDocFormat;
            exp.FormatOptions = pd;
            repDoc.ExportToHttpResponse(exp, Response, false, "ghjgj");
            repDoc.Close();
            repDoc.Dispose();
        }

    }

}