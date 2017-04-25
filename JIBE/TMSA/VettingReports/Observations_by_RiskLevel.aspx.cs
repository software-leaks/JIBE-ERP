using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data.SqlClient;
using System.Configuration;
using System.Web.Services;
using SMS.Business.TMSA;
using SMS.Business.Infrastructure;
using System.Web.Script.Serialization;
using System.Web.Script.Serialization;
using System.ServiceModel.Web;
using System.Text;
using System.IO;
using System.Data;

public partial class TMSA_VettingReports_Observations_by_RiskLevel : System.Web.UI.Page
{
     BLL_TMSA_KPI objKPI = new BLL_TMSA_KPI();
    BLL_TMSA_Vetting_Reports objVTReports = new BLL_TMSA_Vetting_Reports();


    protected void Page_Load(object sender, EventArgs e)
    {

    }

    /// <summary>
    /// Event to export the tabular data to excel sheet
    /// Created By: Harshal
    /// Created On: 18-MAR-2017
    /// </summary>
    protected void btnExportToExcel_Click(object sender, ImageClickEventArgs e)
    {

        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=ObservationByRiskLevel.xls");
        HttpContext.Current.Response.ContentType = "application/ms-excel";

        string Year = hdnYear.Value;
        string VesselID = hdnVessel.Value;
        string FleetID = hdnFleet.Value;
        string RiskLevel = hdnRisk.Value;
        string VettingTypeID = hdnVettingType.Value;
        string ObvTypeID = hdnObvType.Value;
        string CategoryID = hdnCategory.Value;

        //int columnCnt, columnCnt1, columnCnt2, columnCnt3;
        int[] columnCnt = new int[4];

        //Fetch  Observation by Risk Level with count grid1
        DataTable dtRawdata;
        dtRawdata = objVTReports.GetRiskLevelObservation(Year, VesselID, FleetID, RiskLevel, VettingTypeID, ObvTypeID, CategoryID).Tables[0];

        string[] Pkey_cols = new string[] { "Year" };
        string[] Hide_cols = new string[] { "ID", "Year" };
        DataTable dt = objKPI.PivotTable("Risk_Level", "Rec_Count", "", Pkey_cols, Hide_cols, dtRawdata);
        columnCnt[0] = dt.Columns.Count;
        string[] columnNames = dt.Columns.Cast<DataColumn>()
                             .Select(x => x.ColumnName)
                             .ToArray();

        //Fetch  Observation by Risk Level Count for Vessels with yearwise grid2
        DataTable dtRawdata1;
        dtRawdata1 = objVTReports.GetRiskLevelObservationgrid(Year, VesselID, FleetID, RiskLevel, VettingTypeID, ObvTypeID, CategoryID).Tables[0];

        string[] Pkey_cols1 = new string[] { "Vessel_ID" };
        string[] Hide_cols1 = new string[] { "Vessel_ID" };
        DataTable dt1 = objKPI.PivotTable("Year", "Rec_Count", "", Pkey_cols1, Hide_cols1, dtRawdata1);
        columnCnt[1] = dt1.Columns.Count;
        string[] columnNames1 = dt1.Columns.Cast<DataColumn>()
                            .Select(x => x.ColumnName)
                            .ToArray();



        //Fetch  Observation by Risk Level Count for Vessel With yearwise grid3
        DataTable dtRawdata2;
        dtRawdata2 = objVTReports.GetRiskLevelObservationgrid(Year, VesselID, FleetID, RiskLevel, VettingTypeID, ObvTypeID, CategoryID).Tables[0];

        string[] Pkey_cols2 = new string[] { "Vessel_ID" };
        string[] Hide_cols2 = new string[] { "Vessel_ID" };
        DataTable dt2 = objKPI.PivotTable("Year", "Rec_Count", "", Pkey_cols2, Hide_cols2, dtRawdata2);
        columnCnt[2] = dt2.Columns.Count;
        string[] columnNames2 = dt2.Columns.Cast<DataColumn>()
                            .Select(x => x.ColumnName)
                            .ToArray();


        int maxColumnCnt = columnCnt.Max();

        //Observation by Risk Level1 for grid1 Count TABLE
        HttpContext.Current.Response.Write("<TABLE BORDER='1' CELLPADDING='2' CELLSPACING='0' width='100%'>");
        HttpContext.Current.Response.Write("<tr>");
        HttpContext.Current.Response.Write("<td style='text-align:center;' colspan='" + maxColumnCnt + "'><h3>Observations by Risk Level</h3></td>");
        HttpContext.Current.Response.Write("</tr>");
        HttpContext.Current.Response.Write("</TABLE>");
        HttpContext.Current.Response.Write("<TABLE BORDER='1' CELLPADDING='2' CELLSPACING='2' width='100%'>");
        HttpContext.Current.Response.Write("<tr style='background-color: #F2F2F2;'>");
        for (int i = 0; i < columnNames.Length; i++)
        {
            HttpContext.Current.Response.Write("<td width='20%'>");
            HttpContext.Current.Response.Write("<b>" + columnNames[i] + "</b>");
            HttpContext.Current.Response.Write("</td>");
        }
        HttpContext.Current.Response.Write("</tr>");
        foreach (DataRow dr in dt.Rows)
        {
            HttpContext.Current.Response.Write("<tr>");
            for (int i = 0; i < columnNames.Length; i++)
            {
                HttpContext.Current.Response.Write("<td>");
                HttpContext.Current.Response.Write(dr[columnNames[i]]);
                HttpContext.Current.Response.Write("</td>");
            }
            HttpContext.Current.Response.Write("</tr>");
        }
        HttpContext.Current.Response.Write("</TABLE>");
        HttpContext.Current.Response.Write("<br />");




        //Observation by Risk Level with vessels for grid2 TABLE YearWise
        HttpContext.Current.Response.Write("<TABLE BORDER='1' CELLPADDING='2' CELLSPACING='0' width='100%'>");
        HttpContext.Current.Response.Write("<tr>");
        HttpContext.Current.Response.Write("<td style='text-align:center;' colspan='" + columnCnt[1] + "'><h3>High Risk by Vessel</h3></td>");
        HttpContext.Current.Response.Write("</tr>");
        HttpContext.Current.Response.Write("</TABLE>");
        HttpContext.Current.Response.Write("<TABLE BORDER='1' CELLPADDING='2' CELLSPACING='2' width='100%'>");
        HttpContext.Current.Response.Write("<tr style='background-color: #F2F2F2;'>");
        for (int i = 0; i < columnNames1.Length; i++)
        {
            HttpContext.Current.Response.Write("<td width='20%'>");
            HttpContext.Current.Response.Write("<b>" + columnNames1[i] + "</b>");
            HttpContext.Current.Response.Write("</td>");
        }
        HttpContext.Current.Response.Write("</tr>");
        foreach (DataRow dr in dt1.Rows)
        {
            HttpContext.Current.Response.Write("<tr>");
            for (int i = 0; i < columnNames1.Length; i++)
            {
                HttpContext.Current.Response.Write("<td style='text-align:center;'>");
                HttpContext.Current.Response.Write(dr[columnNames1[i]]);
                HttpContext.Current.Response.Write("</td>");
            }
            HttpContext.Current.Response.Write("</tr>");
        }
        HttpContext.Current.Response.Write("</TABLE>");
        HttpContext.Current.Response.Write("<br />");

        HttpContext.Current.Response.Write("</TABLE>");
        HttpContext.Current.Response.Write("<br />");


        //Observation by Risk Level with Fleets TABLE YearWise
        HttpContext.Current.Response.Write("<TABLE BORDER='1' CELLPADDING='2' CELLSPACING='0' width='100%'>");
        HttpContext.Current.Response.Write("<tr>");
        HttpContext.Current.Response.Write("<td style='text-align:center;' colspan='" + columnCnt[2] + "'><h3>Medium Risk by Vessel</h3></td>");
        HttpContext.Current.Response.Write("</tr>");
        HttpContext.Current.Response.Write("</TABLE>");
        HttpContext.Current.Response.Write("<TABLE BORDER='1' CELLPADDING='2' CELLSPACING='2' width='100%'>");
        HttpContext.Current.Response.Write("<tr style='background-color: #F2F2F2;'>");
        for (int i = 0; i < columnNames2.Length; i++)
        {
            HttpContext.Current.Response.Write("<td width='20%'>");
            HttpContext.Current.Response.Write("<b>" + columnNames2[i] + "</b>");
            HttpContext.Current.Response.Write("</td>");
        }
        HttpContext.Current.Response.Write("</tr>");
        foreach (DataRow dr in dt2.Rows)
        {
            HttpContext.Current.Response.Write("<tr>");
            for (int i = 0; i < columnNames2.Length; i++)
            {
                HttpContext.Current.Response.Write("<td style='text-align:center;'>");
                HttpContext.Current.Response.Write(dr[columnNames2[i]]);
                HttpContext.Current.Response.Write("</td>");
            }
            HttpContext.Current.Response.Write("</tr>");
        }
        HttpContext.Current.Response.Write("</TABLE>");
        HttpContext.Current.Response.Write("<br />");

        HttpContext.Current.Response.Write("</TABLE>");
        HttpContext.Current.Response.Write("<br />");


        HttpContext.Current.Response.End();
        GridViewExportUtil.ExportToExcel(dt2, columnNames, columnNames, "Observation_by_RiskLevel", "Observation by RiskLevel");


    }

}