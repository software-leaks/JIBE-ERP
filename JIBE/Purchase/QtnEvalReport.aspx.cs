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


public partial class Technical_INV_QtnEvalReport : System.Web.UI.Page
{
    ParameterFields paramFields;
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!IsPostBack)
        //{
        try
        {
            bindReports();
        }
        catch//(Exception ex)
        {
        }
       // }
    }

    private void bindReports()
     {

        string strRptPath = Server.MapPath(".");
        DataTable dtbl = new DataTable();
 
        // DataSet ds = new DataSet();
        DataTable ReqInfo = new DataTable();
        ReqInfo = (DataTable)Session["Tabl1"];
        DataTable IntemInfo = new DataTable();
        IntemInfo = (DataTable)Session["Tabl2"];
        DataTable SupInfo = new DataTable();
        SupInfo = (DataTable)Session["Tabl3"];


      
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

        DataTable dtitem = new DataTable();
        dtitem = ceateTable(IntemInfo, SupInfo);

      
        CrystalReportsFile.QuotationEval objQtyReport= new CrystalReportsFile.QuotationEval();

        foreach (CrystalDecisions.CrystalReports.Engine.Table reportTable in objQtyReport.Database.Tables)
        {
            logOnInfo = reportTable.LogOnInfo;
            logOnInfo.ConnectionInfo = cInfo;
            reportTable.ApplyLogOnInfo(logOnInfo);
        }
        objQtyReport.Load(strRptPath + "\\QuotationEval.rpt");        
        //objQtyReport.DataDefinition.FormulaFields[1].Text = ReqInfo.Rows[0]["REQUISITION_CODE"].ToString();
        //objQtyReport.DataDefinition.FormulaFields[2].Text = ReqInfo.Rows[0]["REQUISITION_CODE"].ToString();
        //objQtyReport.DataDefinition.FormulaFields[3].Text = ReqInfo.Rows[0]["SYSTEM_Description"].ToString();
        //objQtyReport.DataDefinition.FormulaFields[4].Text = ReqInfo.Rows[0]["TOTAL_ITEMS"].ToString();
        //objQtyReport.DataDefinition.FormulaFields[5].Text = ReqInfo.Rows[0]["requestion_Date"].ToString();
        //objQtyReport.DataDefinition.FormulaFields[6].Text = ReqInfo.Rows[0]["REQUISITION_CODE"].ToString();
        objQtyReport.OpenSubreport("SubReportsForSupp").SetDataSource(SupInfo);
        objQtyReport.OpenSubreport("RequsitionDetail").SetDataSource(ReqInfo);
        objQtyReport.SetDataSource(dtitem);
        CrystalReportViewer.ReportSource = objQtyReport;
        CrystalReportViewer.ParameterFieldInfo = paramFields;
        CrystalReportViewer.DisplayToolbar = true;
     

    }

    private DataTable ceateTable(DataTable IntemInfo, DataTable SupInfo)
    {
        int columnCount=0;
      //  CrystalReportViewer.ParameterFieldInfo.Clear();
        ReportDocument reportDocument;
     

        ParameterField paramField;
        ParameterDiscreteValue paramDiscreteValue;

        reportDocument = new ReportDocument();
        paramFields = new ParameterFields();

        DataTable dt = new DataTable();
        dt = IntemInfo.Clone();
        if (dt.Columns.Count > 12)
        {
            for (int i = dt.Columns.Count - 1; i >= 12; i--)
            {
                dt.Columns.RemoveAt(i); 
            }
        }
        DataTable dt1 = new DataTable();
        SupInfo.DefaultView.RowFilter = "Req_status='True'";
        if (IntemInfo.Columns.Count > 12 && IntemInfo.Columns.Count <= 23)
        {
            
            dt.Columns.Add("Rate1", typeof(Double));
            dt.Columns.Add("Discount1", typeof(Double));
            dt.Columns.Add("Amount1", typeof(Double));
            dt.Columns.Add("Select1", typeof(string));           
            foreach (DataRow dr in IntemInfo.Rows)
            {
                
                DataRow dr1 = dt.NewRow();
                dr1[0] = dr[0].ToString();
                dr1[1] = dr[1].ToString();
                dr1[2] = dr[2].ToString();
                dr1[3] = dr[3].ToString();
                dr1[4] = dr[4].ToString();
                dr1[5] = dr[5].ToString();
                dr1[6] = dr[6].ToString();
                dr1[7] = dr[7].ToString();
                dr1[8] = dr[8].ToString();
                dr1[9] = dr[9].ToString();
                dr1[10] = dr[10].ToString();
                dr1[11] = dr[11].ToString();
                dr1[12] = UDFLib.ConvertToDecimal(dr[12].ToString());
                dr1["Rate1"] = UDFLib.ConvertToDecimal(dr[13].ToString());
                dr1["Discount1"] = UDFLib.ConvertToDecimal(dr[15].ToString());
                dr1["Amount1"] = UDFLib.ConvertToDecimal(dr[19].ToString());
                dr1["Select1"] = dr[17].ToString();
                dt.Rows.Add(dr1); 
            }
            dt.AcceptChanges();
            columnCount = 1;
            paramField = new ParameterField();
            paramField.Name = "supplier1";
            paramDiscreteValue = new ParameterDiscreteValue();
            paramDiscreteValue.Value = SupInfo.DefaultView[0][1].ToString();
            paramField.CurrentValues.Add(paramDiscreteValue);
            //Add the paramField to paramFields
            paramFields.Add(paramField);
            paramField = new ParameterField();
            paramField.Name = "Rate1";
            paramDiscreteValue = new ParameterDiscreteValue();
            paramDiscreteValue.Value = "Unit Price";
            paramField.CurrentValues.Add(paramDiscreteValue);
            //Add the paramField to paramFields
            paramFields.Add(paramField);
            paramField = new ParameterField();
            paramField.Name = "Discount1";
            paramDiscreteValue = new ParameterDiscreteValue();
            paramDiscreteValue.Value = "Discount";
            paramField.CurrentValues.Add(paramDiscreteValue);
            //Add the paramField to paramFields
            paramFields.Add(paramField);
            paramField = new ParameterField();
            paramField.Name = "Amount1";
            paramDiscreteValue = new ParameterDiscreteValue();
            paramDiscreteValue.Value = "Amount";
            paramField.CurrentValues.Add(paramDiscreteValue);
            //Add the paramField to paramFields
            paramFields.Add(paramField);
            paramField = new ParameterField();
            paramField.Name = "Select1";
            paramDiscreteValue = new ParameterDiscreteValue();
            paramDiscreteValue.Value = "Select";
            paramField.CurrentValues.Add(paramDiscreteValue);
            //Add the paramField to paramFields
            paramFields.Add(paramField);
            
        }
        if (IntemInfo.Columns.Count > 23 && IntemInfo.Columns.Count <= 30)
        {
            dt.Columns.Add("Rate1", typeof(Double));
            dt.Columns.Add("Discount1", typeof(Double));
            dt.Columns.Add("Amount1", typeof(Double));
            dt.Columns.Add("Select1", typeof(Boolean));
            //dt.Columns.Add("supplier1", typeof(Double));
            dt.Columns.Add("Rate2", typeof(Double));
            dt.Columns.Add("Discount2", typeof(Double));
            dt.Columns.Add("Amount2", typeof(Double));
            dt.Columns.Add("Select2", typeof(Boolean));
           // dt.Columns.Add("supplier2", typeof(Double));
            foreach (DataRow dr in IntemInfo.Rows)
            {

                DataRow dr1 = dt.NewRow();
                dr1[0] = dr[0].ToString();
                dr1[1] = dr[1].ToString();
                dr1[2] = dr[2].ToString();
                dr1[3] = dr[3].ToString();
                dr1[4] = dr[4].ToString();
                dr1[5] = dr[5].ToString();
                dr1[6] = dr[6].ToString();
                dr1[7] = dr[7].ToString();
                dr1[8] = dr[8].ToString();
                dr1[9] = dr[9].ToString();
                dr1[10] = dr[10].ToString();
                dr1[11] = dr[11].ToString();
                dr1[12] = UDFLib.ConvertToDecimal(dr[12].ToString());
                dr1["Rate1"] =UDFLib.ConvertToDecimal( dr[13].ToString());
                dr1["Discount1"] = dr[15].ToString();
                dr1["Amount1"] = dr[19].ToString();
                dr1["Select1"] = dr[17].ToString();
                dr1["Rate2"] = dr[22].ToString();
                dr1["Discount2"] = dr[24].ToString();
                dr1["Amount2"] = dr[28].ToString();
                dr1["Select2"] = dr[26].ToString();
                dt.Rows.Add(dr1);
            }
            dt.AcceptChanges();
            columnCount = 2;
            paramField = new ParameterField();
            paramField.Name = "supplier1";
            paramDiscreteValue = new ParameterDiscreteValue();
            paramDiscreteValue.Value = SupInfo.DefaultView[0][1].ToString();
            paramField.CurrentValues.Add(paramDiscreteValue);
            //Add the paramField to paramFields
            paramFields.Add(paramField);
            paramField = new ParameterField();
            paramField.Name = "Rate1";
            paramDiscreteValue = new ParameterDiscreteValue();
            paramDiscreteValue.Value = "Unit Price";
            paramField.CurrentValues.Add(paramDiscreteValue);
            //Add the paramField to paramFields
            paramFields.Add(paramField);
            paramField = new ParameterField();
            paramField.Name = "Discount1";
            paramDiscreteValue = new ParameterDiscreteValue();
            paramDiscreteValue.Value = "Discount";
            paramField.CurrentValues.Add(paramDiscreteValue);
            //Add the paramField to paramFields
            paramFields.Add(paramField);
            paramField = new ParameterField();
            paramField.Name = "Amount1";
            paramDiscreteValue = new ParameterDiscreteValue();
            paramDiscreteValue.Value = "Amount";
            paramField.CurrentValues.Add(paramDiscreteValue);
            //Add the paramField to paramFields
            paramFields.Add(paramField);
            paramField = new ParameterField();
            paramField.Name = "Select1";
            paramDiscreteValue = new ParameterDiscreteValue();
            paramDiscreteValue.Value = "Select";
            paramField.CurrentValues.Add(paramDiscreteValue);
            //Add the paramField to paramFields
            paramFields.Add(paramField);

            paramField = new ParameterField();
            paramField.Name = "Supplier2";
            paramDiscreteValue = new ParameterDiscreteValue();
            paramDiscreteValue.Value = SupInfo.DefaultView[1][1].ToString();
            paramField.CurrentValues.Add(paramDiscreteValue);
            //Add the paramField to paramFields
            paramFields.Add(paramField);
            paramField = new ParameterField();
            paramField.Name = "Rate2";
            paramDiscreteValue = new ParameterDiscreteValue();
            paramDiscreteValue.Value = "Unit Price";
            paramField.CurrentValues.Add(paramDiscreteValue);
            //Add the paramField to paramFields
            paramFields.Add(paramField);
            paramField = new ParameterField();
            paramField.Name = "Discount2";
            paramDiscreteValue = new ParameterDiscreteValue();
            paramDiscreteValue.Value = "Discount";
            paramField.CurrentValues.Add(paramDiscreteValue);
            //Add the paramField to paramFields
            paramFields.Add(paramField);
            paramField = new ParameterField();
            paramField.Name = "Amount2";
            paramDiscreteValue = new ParameterDiscreteValue();
            paramDiscreteValue.Value = "Amount";
            paramField.CurrentValues.Add(paramDiscreteValue);
            //Add the paramField to paramFields
            paramFields.Add(paramField);
            paramField = new ParameterField();
            paramField.Name = "Select2";
            paramDiscreteValue = new ParameterDiscreteValue();
            paramDiscreteValue.Value = "Select";
            paramField.CurrentValues.Add(paramDiscreteValue);
            //Add the paramField to paramFields
            paramFields.Add(paramField);


        }
        if (IntemInfo.Columns.Count >30 && IntemInfo.Columns.Count <= 39)
        {
            dt.Columns.Add("Rate1", typeof(Double));
            dt.Columns.Add("Discount1", typeof(Double));
            dt.Columns.Add("Amount1", typeof(Double));
            dt.Columns.Add("Select1", typeof(Boolean));
            dt.Columns.Add("Rate2", typeof(Double));
            dt.Columns.Add("Discount2", typeof(Double));
            dt.Columns.Add("Amount2", typeof(Double));
            dt.Columns.Add("Select2", typeof(Boolean));
            dt.Columns.Add("Rate3", typeof(Double));
            dt.Columns.Add("Discount3", typeof(Double));
            dt.Columns.Add("Amount3", typeof(Double));
            dt.Columns.Add("Select3", typeof(Boolean));

            foreach (DataRow dr in IntemInfo.Rows)
            {

                DataRow dr1 = dt.NewRow();
                dr1[0] = dr[0].ToString();
                dr1[1] = dr[1].ToString();
                dr1[2] = dr[2].ToString();
                dr1[3] = dr[3].ToString();
                dr1[4] = dr[4].ToString();
                dr1[5] = dr[5].ToString();
                dr1[6] = dr[6].ToString();
                dr1[7] = dr[7].ToString();
                dr1[8] = dr[8].ToString();
                dr1[9] = dr[9].ToString();
                dr1[10] = dr[10].ToString();
                dr1[11] = dr[11].ToString();
                dr1[12] = dr[12].ToString();
                dr1["Rate1"] = dr[13].ToString();
                dr1["Discount1"] = dr[15].ToString();
                dr1["Amount1"] = dr[19].ToString();
                dr1["Select1"] = dr[17].ToString();
                dr1["Rate2"] = dr[22].ToString();
                dr1["Discount2"] = dr[24].ToString();
                dr1["Amount2"] = dr[28].ToString();
                dr1["Select2"] = dr[26].ToString();
                dr1["Rate3"] = dr[31].ToString();
                dr1["Discount3"] = dr[33].ToString();
                dr1["Amount3"] = dr[37].ToString();
                dr1["Select3"] = dr[35].ToString();
                dt.Rows.Add(dr1);
            }
            dt.AcceptChanges();
            columnCount = 3;
            paramField = new ParameterField();
            paramField.Name = "supplier1";
            paramDiscreteValue = new ParameterDiscreteValue();
            paramDiscreteValue.Value = SupInfo.DefaultView[0][1].ToString(); 
            paramField.CurrentValues.Add(paramDiscreteValue);
            //Add the paramField to paramFields
            paramFields.Add(paramField);
            paramField = new ParameterField();
            paramField.Name = "Rate1";
            paramDiscreteValue = new ParameterDiscreteValue();
            paramDiscreteValue.Value = "Unit Price";
            paramField.CurrentValues.Add(paramDiscreteValue);
            //Add the paramField to paramFields
            paramFields.Add(paramField);
            paramField = new ParameterField();
            paramField.Name = "Discount1";
            paramDiscreteValue = new ParameterDiscreteValue();
            paramDiscreteValue.Value = "Discount";
            paramField.CurrentValues.Add(paramDiscreteValue);
            //Add the paramField to paramFields
            paramFields.Add(paramField);
            paramField = new ParameterField();
            paramField.Name = "Amount1";
            paramDiscreteValue = new ParameterDiscreteValue();
            paramDiscreteValue.Value = "Amount";
            paramField.CurrentValues.Add(paramDiscreteValue);
            //Add the paramField to paramFields
            paramFields.Add(paramField);
            paramField = new ParameterField();
            paramField.Name = "Select1";
            paramDiscreteValue = new ParameterDiscreteValue();
            paramDiscreteValue.Value = "Select";
            paramField.CurrentValues.Add(paramDiscreteValue);
            //Add the paramField to paramFields
            paramFields.Add(paramField);

            paramField = new ParameterField();
            paramField.Name = "supplier2";
            paramDiscreteValue = new ParameterDiscreteValue();
            paramDiscreteValue.Value = SupInfo.DefaultView[1][1].ToString();
            paramField.CurrentValues.Add(paramDiscreteValue);
            //Add the paramField to paramFields
            paramFields.Add(paramField);
            paramField = new ParameterField();
            paramField.Name = "Rate2";
            paramDiscreteValue = new ParameterDiscreteValue();
            paramDiscreteValue.Value = "Unit Price";
            paramField.CurrentValues.Add(paramDiscreteValue);
            //Add the paramField to paramFields
            paramFields.Add(paramField);
            paramField = new ParameterField();
            paramField.Name = "Discount2";
            paramDiscreteValue = new ParameterDiscreteValue();
            paramDiscreteValue.Value = "Discount";
            paramField.CurrentValues.Add(paramDiscreteValue);
            //Add the paramField to paramFields
            paramFields.Add(paramField);
            paramField = new ParameterField();
            paramField.Name = "Amount2";
            paramDiscreteValue = new ParameterDiscreteValue();
            paramDiscreteValue.Value = "Amount";
            paramField.CurrentValues.Add(paramDiscreteValue);
            //Add the paramField to paramFields
            paramFields.Add(paramField);
            paramField = new ParameterField();
            paramField.Name = "Select2";
            paramDiscreteValue = new ParameterDiscreteValue();
            paramDiscreteValue.Value = "Select";
            paramField.CurrentValues.Add(paramDiscreteValue);
            //Add the paramField to paramFields
            paramFields.Add(paramField);
            paramField = new ParameterField();

            paramField.Name = "Supplier3";
            paramDiscreteValue = new ParameterDiscreteValue();
            paramDiscreteValue.Value = SupInfo.DefaultView[2][1].ToString();
            paramField.CurrentValues.Add(paramDiscreteValue);
            //Add the paramField to paramFields
            paramFields.Add(paramField);
            paramField = new ParameterField();
            paramField.Name = "Rate3";
            paramDiscreteValue = new ParameterDiscreteValue();
            paramDiscreteValue.Value = "Unit Price";
            paramField.CurrentValues.Add(paramDiscreteValue);
            //Add the paramField to paramFields
            paramFields.Add(paramField);
            paramField = new ParameterField();
            paramField.Name = "Discount3";
            paramDiscreteValue = new ParameterDiscreteValue();
            paramDiscreteValue.Value = "Discount";
            paramField.CurrentValues.Add(paramDiscreteValue);
            //Add the paramField to paramFields
            paramFields.Add(paramField);
            paramField = new ParameterField();
            paramField.Name = "Amount3";
            paramDiscreteValue = new ParameterDiscreteValue();
            paramDiscreteValue.Value = "Amount";
            paramField.CurrentValues.Add(paramDiscreteValue);
            //Add the paramField to paramFields
            paramFields.Add(paramField);
            paramField = new ParameterField();
            paramField.Name = "Select3";
            paramDiscreteValue = new ParameterDiscreteValue();
            paramDiscreteValue.Value = "Select";
            paramField.CurrentValues.Add(paramDiscreteValue);
            //Add the paramField to paramFields
            paramFields.Add(paramField);
        }
        if (IntemInfo.Columns.Count > 39 )
        {

        }
        for (int i = columnCount; i < 3; i++)
        {
            int count;
            count = i + 1;
            paramField = new ParameterField();
            paramField.Name = "supplier" + count;
            paramDiscreteValue = new ParameterDiscreteValue();
            paramDiscreteValue.Value = "";
            paramField.CurrentValues.Add(paramDiscreteValue);
            //Add the paramField to paramFields
            paramFields.Add(paramField);
            paramField = new ParameterField();
            paramField.Name = "Rate" + count;
            paramDiscreteValue = new ParameterDiscreteValue();
            paramDiscreteValue.Value = "";
            paramField.CurrentValues.Add(paramDiscreteValue);
            //Add the paramField to paramFields
            paramFields.Add(paramField);
            paramField = new ParameterField();
            paramField.Name = "Discount" + count;
            paramDiscreteValue = new ParameterDiscreteValue();
            paramDiscreteValue.Value = "";
            paramField.CurrentValues.Add(paramDiscreteValue);
            //Add the paramField to paramFields
            paramFields.Add(paramField);
            paramField = new ParameterField();
            paramField.Name = "Amount" + count;
            paramDiscreteValue = new ParameterDiscreteValue();
            paramDiscreteValue.Value = "";
            paramField.CurrentValues.Add(paramDiscreteValue);
            //Add the paramField to paramFields
            paramFields.Add(paramField);
            paramField = new ParameterField();
            paramField.Name = "Select" + count;
            paramDiscreteValue = new ParameterDiscreteValue();
            paramDiscreteValue.Value = "";
            paramField.CurrentValues.Add(paramDiscreteValue);
            //Add the paramField to paramFields
            paramFields.Add(paramField);
        }


       // paramField = new ParameterField();
       // paramField.Name = "@m_Parameters";
       // paramDiscreteValue = new ParameterDiscreteValue();
       // paramDiscreteValue.Value = "Customer Code";
       // paramField.CurrentValues.Add(paramDiscreteValue);
       // //Add the paramField to paramFields
       // paramFields.Add(paramField);
       //CrystalReportViewer.ParameterFieldInfo.Clear();
        //CrystalReportViewer.ParameterFieldInfo = paramFields;
        return dt;
    }
   
}
