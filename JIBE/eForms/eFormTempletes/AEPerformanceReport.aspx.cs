using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using AEPerformanceReport;

public partial class eForms_eFormTempletes_AEPerformanceReport : System.Web.UI.Page
{
    int Form_ID;
    int Dtl_Report_ID;
    int Vessel_ID;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Form_ID = UDFLib.ConvertToInteger(Request.QueryString["Form_ID"]);
            Dtl_Report_ID = UDFLib.ConvertToInteger(Request.QueryString["DtlID"]);
            Vessel_ID = UDFLib.ConvertToInteger(Request.QueryString["VID"]); 
            
            Load_AE_Perf_Report_Details(Dtl_Report_ID, Vessel_ID);
        }
    }

    private void Load_AE_Perf_Report_Details(int Report_ID, int Vessel_ID)
    {
        DataSet ds = BLL_FRM_AEPerformanceReport.Get_AEPerformanceReport(Report_ID, Vessel_ID);
        
        frmMain.DataSource = ds.Tables[2];
        frmMain.DataBind();

        AEPerfReport_Engine1.Show_Engine_Record(1, Dtl_Report_ID, Vessel_ID);
        AEPerfReport_Engine2.Show_Engine_Record(2, Dtl_Report_ID, Vessel_ID);
        AEPerfReport_Engine3.Show_Engine_Record(3, Dtl_Report_ID, Vessel_ID);
        AEPerfReport_Engine4.Show_Engine_Record(4, Dtl_Report_ID, Vessel_ID);

    }
}