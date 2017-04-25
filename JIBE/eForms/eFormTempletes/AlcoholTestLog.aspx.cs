using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.eForms;


public partial class AlcoholTestLog : System.Web.UI.Page
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

            Load_BilgeAlarmMonthlyTestingReport_Details(Dtl_Report_ID, Vessel_ID);
        }
    }

    private void Load_BilgeAlarmMonthlyTestingReport_Details( int Main_Report_ID, int Vessel_ID)
    {
        try
        {

            DataSet ds = BLL_DANFormO2AlcoholTestLog.Get_DNA_Alcohol_Test_Log(Main_Report_ID, Vessel_ID);

            GridView_CargoHoldBilgeAlarm.DataSource = ds.Tables[0];
            GridView_CargoHoldBilgeAlarm.DataBind();

            if (ds.Tables[1].Rows.Count > 0)
            {
                lblReportDate.Text = ds.Tables[1].Rows[0]["ReportDate"].ToString();
                lblVesselName.Text = ds.Tables[1].Rows[0]["Vessel_Name"].ToString();
            }
          
        }
        catch
        {

        }
    }
}