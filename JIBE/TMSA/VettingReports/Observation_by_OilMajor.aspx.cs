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


public partial class TMSA_VettingReports_Observation_by_OilMajor : System.Web.UI.Page
{
    BLL_TMSA_KPI objKPI = new BLL_TMSA_KPI();
    BLL_TMSA_Vetting_Reports objVTReports = new BLL_TMSA_Vetting_Reports();

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    /// <summary>
    /// Event to export the tabular data to excel sheet
    /// Created By: Harshal
    /// Created On: 17-MAR-2017
    /// </summary>
    protected void btnExportToExcel_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=ObservationbyOilMajor.xls");
            HttpContext.Current.Response.ContentType = "application/ms-excel";

            string Year = hdnYear.Value;
            string VesselID = hdnVessel.Value;           
            string CategoryID = hdnCategory.Value;
            string VettingTypeID = hdnVettingType.Value;
            string ObvTypeID = hdnObvType.Value;
            string FleetID = hdnFleet.Value;
            string OilID = hdnOilMajor.Value;
            int[] columnCnt = new int[4];

            //Fetch  Observation by OilMajorCount Yearwise 
            DataTable dtRawdata;
            dtRawdata = objVTReports.GetVesselwiseOilMajors(Year, VesselID, CategoryID, VettingTypeID, ObvTypeID, FleetID,OilID).Tables[0];

            string[] Pkey_cols = new string[] { "Year" };
            string[] Hide_cols = new string[] { "ID", "Year" };
            DataTable dt = objKPI.PivotTable("OilMajorName", "Rec_Count", "", Pkey_cols, Hide_cols, dtRawdata);
            columnCnt[0] = dt.Columns.Count;
            string[] columnNames = dt.Columns.Cast<DataColumn>()
                                 .Select(x => x.ColumnName)
                                 .ToArray();

            //Fetch  Observation by OilMajorName Count yearwise
            DataTable dtRawdata1;
            dtRawdata1 = objVTReports.GetOilMajorCountYearwise(Year, VesselID, CategoryID, VettingTypeID, ObvTypeID, FleetID,OilID).Tables[0];

            string[] Pkey_cols1 = new string[] { "OilMajorID" };
            string[] Hide_cols1 = new string[] { "OilMajorID" };
            DataTable dt1 = objKPI.PivotTable("Year", "Rec_Count", "", Pkey_cols1, Hide_cols1, dtRawdata1);
            columnCnt[1] = dt1.Columns.Count;
            string[] columnNames1 = dt1.Columns.Cast<DataColumn>()
                                .Select(x => x.ColumnName)
                                .ToArray();

            int maxColumnCnt = columnCnt.Max();
            //Observation by Oil Major Count TABLE
            HttpContext.Current.Response.Write("<TABLE BORDER='1' CELLPADDING='2' CELLSPACING='0' width='100%'>");
            HttpContext.Current.Response.Write("<tr>");
            HttpContext.Current.Response.Write("<td style='text-align:center;' colspan='" + maxColumnCnt + "'><h3>Observations by Oil Major</h3></td>");
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

            //Observation by Oil MajorName TABLE YearWise

            HttpContext.Current.Response.Write("<TABLE BORDER='1' CELLPADDING='2' CELLSPACING='0' width='100%'>");
            HttpContext.Current.Response.Write("<tr>");
            HttpContext.Current.Response.Write("<td style='text-align:center;' colspan='" + columnCnt[1] + "'><h3>Observations by Oil Major</h3></td>");
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
                    HttpContext.Current.Response.Write("<td>");
                    HttpContext.Current.Response.Write(dr[columnNames1[i]]);
                    HttpContext.Current.Response.Write("</td>");
                }
                HttpContext.Current.Response.Write("</tr>");
            }
            HttpContext.Current.Response.Write("</TABLE>");
            HttpContext.Current.Response.Write("<br />");
            
            HttpContext.Current.Response.Write("</TABLE>");
            HttpContext.Current.Response.Write("<br />");

            HttpContext.Current.Response.End();
            GridViewExportUtil.ExportToExcel(dt1, columnNames, columnNames, "Observation_by_OilMajor", "Observation by OilMajor");
        }

        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }


}