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
using PMSReports;
using SMS.Business.PURC;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;
using CrystalDecisions.Shared;
using ClsBLLTechnical;

public partial class Technical_INV_POPreview : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string QTORDCode = "";
        string DocCode = "";

        if (Request.QueryString["Quotation_code"] != null)
        {
            QTORDCode = Request.QueryString["Quotation_code"];
            DocCode = "0";
        }
        else if (Request.QueryString["ORDER_CODE"] != null)
        {
            QTORDCode = Request.QueryString["ORDER_CODE"];
            DocCode = "1";
        }

        using (BLL_PURC_Purchase Obj = new BLL_PURC_Purchase())
        {
            DataTable Dt = Obj.PODetails(Request.QueryString["RFQCODE"].ToString(), QTORDCode, "", DocCode, Request.QueryString["Vessel_Code"].ToString());

            GeneratePOAsPDF(Dt, "", Request.QueryString["RFQCODE"].ToString()+".pdf");
        }

    }

    protected void GeneratePOAsPDF(DataTable dt, string strPath, string FileName)
    {

        string repFilePath = Server.MapPath("POReport.rpt");
        using (ReportDocument repDoc = new ReportDocument())
        {
            repDoc.Load(repFilePath);

            repDoc.SetDataSource(dt);


            decimal Total_Price = 0;
            decimal Exchane_rate = 1;
            decimal Vat = 0;
            decimal sercharge = 0;
            decimal discount = 0;
            decimal withholding = 0;
            foreach (DataRow dr in dt.Rows)
            {
                Exchane_rate = Convert.ToDecimal(dr["EXCHANGE_RATE"].ToString());
                discount = Convert.ToDecimal(dr["DISCOUNT"].ToString());
                Vat = Convert.ToDecimal(dr["VAT"].ToString());
                sercharge = Convert.ToDecimal(dr["SURCHARGES"].ToString());

                Total_Price = Total_Price + (Convert.ToDecimal(dr["REQUESTED_QTY"].ToString()) * Convert.ToDecimal(dr["QUOTED_RATE"].ToString()) - (Convert.ToDecimal(dr["REQUESTED_QTY"].ToString()) * Convert.ToDecimal(dr["QUOTED_RATE"].ToString()) * Convert.ToDecimal(dr["QUOTED_DISCOUNT"].ToString()) / 100));
                if (dr["SURCHARGES"].ToString() == "Spares") { repDoc.ReportDefinition.Sections[1].SectionFormat.EnableSuppress = true; }
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
