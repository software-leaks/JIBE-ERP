using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.eForms;


public partial class eForms_eFormTempletes_BilgeAlarmMonthlyTestingReport : System.Web.UI.Page
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
            DataSet dsmain = BilgeAlarmMonthlyTestingReport.BLL_FRM_BilgeAlarmMonthlyTestingReport.Get_BilgeAlarmMonthlyTestingReport_Main(Main_Report_ID, Vessel_ID);
            
            if (dsmain.Tables[0].Rows.Count > 0)
            {
                //txtOtherAlarmSensors.Text = dsmain.Tables[0].Rows[0]["Cargo_hold_blidge_Alarm_sensors"].ToString();
                //txtEngineAlarmSensors.Text = dsmain.Tables[0].Rows[0]["Engine_room_blidge_Alarm_sensors"].ToString();
                //txtCargoAlarmSensors.Text = dsmain.Tables[0].Rows[0]["Other_blidge_Alarm_sensors"].ToString();
                //txtNote.Text = dsmain.Tables[0].Rows[0]["Bilge_Alarm_Note"].ToString();

                lblReportDate.Text = dsmain.Tables[0].Rows[0]["ReportDate"].ToString();
                lblVesselName.Text = dsmain.Tables[0].Rows[0]["Vessel_Name"].ToString();

                //txtMonth.Text = dsmain.Tables[0].Rows[0]["Report_Month"].ToString();
                //txtYear.Text = dsmain.Tables[0].Rows[0]["Report_Year"].ToString();

                //gEditReport.Visibility = Visibility.Visible;
                //dtpReportDate.Visibility = Visibility.Hidden;
                //txtReportDate.Visibility = Visibility.Hidden;
            }

            DataSet ds = BilgeAlarmMonthlyTestingReport.BLL_FRM_BilgeAlarmMonthlyTestingReport.Get_BilgeAlarmMonthlyTestingReport_Details(Main_Report_ID, Vessel_ID);
            
            //this.DataContext = ds.Tables[0];
            GridView_CargoHoldBilgeAlarm.DataSource = ds.Tables[0];
            GridView_CargoHoldBilgeAlarm.DataBind();

            GridView_ERBilgeAlarm.DataSource = ds.Tables[1];
            GridView_ERBilgeAlarm.DataBind();

            GridView_OtherBilgeAlarm.DataSource = ds.Tables[2];
            GridView_OtherBilgeAlarm.DataBind();
        }
        catch
        {

        }
    }
}