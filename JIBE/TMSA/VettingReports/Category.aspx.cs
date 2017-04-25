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

public partial class TMSA_VettingReports_Category : System.Web.UI.Page
{
    BLL_TMSA_KPI objKPI = new BLL_TMSA_KPI();
    BLL_TMSA_Vetting_Reports objVTReports = new BLL_TMSA_Vetting_Reports();

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    /// <summary>
    /// Event to export the tabular data to excel sheet
    /// Created By: Krishnapriya
    /// Created On: 07-MAR-2017
    /// </summary>
    protected void btnExportToExcel_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=ObservationByCategory.xls");
            HttpContext.Current.Response.ContentType = "application/ms-excel";

            string Year = hdnYear.Value;
            string [] yearArr = Year.Split(',');
            Array.Reverse(yearArr);
            string VesselID = hdnVessel.Value;
            string VettingTypeID = hdnVettingType.Value;
            string ObvTypeID = hdnObvType.Value;
            string CategoryID = hdnCategory.Value;
            string FleetID = hdnFleet.Value;
            //int columnCnt, columnCnt1, columnCnt2, columnCnt3;
            int[] columnCnt = new int[4];

            //Fetch  Observation by Vessel Count
            DataTable dtRawdata;
            dtRawdata = objVTReports.GetObservationByCategoryCount(VesselID, Year, VettingTypeID, CategoryID, ObvTypeID, FleetID).Tables[0];

            string[] Pkey_cols = new string[] { "Year" };
            string[] Hide_cols = new string[] { "CategoryID", "ID", "Year" };
            DataTable dt = objKPI.PivotTable("CategoryName", "Rec_Count", "", Pkey_cols, Hide_cols, dtRawdata);
            columnCnt[0] = dt.Columns.Count;
            string[] columnNames = dt.Columns.Cast<DataColumn>()
                                 .Select(x => x.ColumnName)
                                 .ToArray();

            //Fetch  Observation by Fleet Count
            DataTable dtRawdata1;
            DataTable dt1 = new DataTable();
            dtRawdata1 = objVTReports.GetMultipleYearCategoryCount(VesselID, Year, VettingTypeID, CategoryID, ObvTypeID, FleetID).Tables[0];

            string[] Pkey_cols1 = new string[] { "Year" };
            string[] Hide_cols1 = new string[] { "ID" };
            dt1 = objKPI.PivotTable("Category_Name", "Rec_Count", "", Pkey_cols, Hide_cols, dtRawdata1);
            dt1.Columns.Add("Year", typeof(System.String)).SetOrdinal(0);

            for (int j = 0; j < dt1.Rows.Count; j++ )
            {
                for (int i = 0; i < yearArr.Length; i++)
                {
                    if(i==j)
                        dt1.Rows[j]["Year"] = yearArr[i];
                }
            }         

            columnCnt[1] = dt1.Columns.Count;
            string[] columnNames1 = dt1.Columns.Cast<DataColumn>()
                                .Select(x => x.ColumnName)
                                .ToArray();

            //Fetch  Observation by Vessel  for Categories
            DataTable dtRawdata2;
            dtRawdata2 = objVTReports.GetVesselObservationCntByCategory(VesselID, Year, VettingTypeID, CategoryID, ObvTypeID, FleetID).Tables[0];

            string[] Pkey_cols2 = new string[] { "ID" };
            string[] Hide_cols2 = new string[] { "ID","Category_ID" };
            DataTable dt2 = objKPI.PivotTable("CategoryName", "Rec_Count", "", Pkey_cols2, Hide_cols2, dtRawdata2);
            columnCnt[2] = dt2.Columns.Count;
            string[] columnNames2 = dt2.Columns.Cast<DataColumn>()
                                .Select(x => x.ColumnName)
                                .ToArray();


            //Fetch  Observation by Fleet for Categories
            DataTable dtRawdata3;
            dtRawdata3 = objVTReports.GetFleetObservationCntByCategory(VesselID, Year, VettingTypeID, CategoryID, ObvTypeID, FleetID).Tables[0];

            string[] Pkey_cols3 = new string[] { "FleetCode" };
            string[] Hide_cols3 = new string[] { "FleetCode","ID" };
            DataTable dt3 = objKPI.PivotTable("CategoryName", "Rec_Count", "", Pkey_cols3, Hide_cols3, dtRawdata3);
            columnCnt[3] = dt3.Columns.Count;
            string[] columnNames3 = dt3.Columns.Cast<DataColumn>()
                                .Select(x => x.ColumnName)
                                .ToArray();


            int maxColumnCnt = columnCnt.Max();
            //Observation by Vessel Count TABLE
            HttpContext.Current.Response.Write("<TABLE BORDER='1' CELLPADDING='2' CELLSPACING='0' width='100%'>");
            HttpContext.Current.Response.Write("<tr>");
            HttpContext.Current.Response.Write("<td style='text-align:center;' colspan='" + columnCnt[0] + "'><h3>Observations by Category</h3></td>");
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

            //Observation by Fleet Count TABLE

            HttpContext.Current.Response.Write("<TABLE BORDER='1' CELLPADDING='2' CELLSPACING='0' width='100%'>");
            HttpContext.Current.Response.Write("<tr>");
            HttpContext.Current.Response.Write("<td style='text-align:center;' colspan='" + columnCnt[1] + "'><h3>Avg. Observation per Vetting</h3></td>");
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

            ////Observation by Fleet  for Categories
            HttpContext.Current.Response.Write("<TABLE BORDER='1' CELLPADDING='2' CELLSPACING='0' width='100%'>");
            HttpContext.Current.Response.Write("<tr>");
            HttpContext.Current.Response.Write("<td style='text-align:center;' colspan='" + columnCnt[2] + "'><h3>Categories by Fleet</h3></td>");
            HttpContext.Current.Response.Write("</tr>");
            HttpContext.Current.Response.Write("</TABLE>");
            HttpContext.Current.Response.Write("<TABLE BORDER='1' CELLPADDING='2' CELLSPACING='2' width='100%'>");
            HttpContext.Current.Response.Write("<tr style='background-color: #F2F2F2;'>");
            for (int i = 0; i < columnNames3.Length; i++)
            {
                HttpContext.Current.Response.Write("<td width='20%'>");
                HttpContext.Current.Response.Write("<b>" + columnNames3[i] + "</b>");
                HttpContext.Current.Response.Write("</td>");
            }
            HttpContext.Current.Response.Write("</tr>");
            foreach (DataRow dr in dt3.Rows)
            {
                HttpContext.Current.Response.Write("<tr>");
                for (int i = 0; i < columnNames3.Length; i++)
                {
                    HttpContext.Current.Response.Write("<td>");
                    HttpContext.Current.Response.Write(dr[columnNames3[i]]);
                    HttpContext.Current.Response.Write("</td>");
                }
                HttpContext.Current.Response.Write("</tr>");
            }
            HttpContext.Current.Response.Write("</TABLE>");
            HttpContext.Current.Response.Write("<br />");

            //Observation by Vessel  for Categories
            HttpContext.Current.Response.Write("<TABLE BORDER='1' CELLPADDING='2' CELLSPACING='0' width='100%'>");
            HttpContext.Current.Response.Write("<tr>");
            HttpContext.Current.Response.Write("<td style='text-align:center;' colspan='" + columnCnt[3] + "'><h3>Categories by Vessel</h3></td>");
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
                    HttpContext.Current.Response.Write("<td>");
                    HttpContext.Current.Response.Write(dr[columnNames2[i]]);
                    HttpContext.Current.Response.Write("</td>");
                }
                HttpContext.Current.Response.Write("</tr>");
            }
            HttpContext.Current.Response.Write("</TABLE>");
            HttpContext.Current.Response.Write("<br />");



            HttpContext.Current.Response.End();
            //GridViewExportUtil.ExportToExcel(dt1, columnNames, columnNames, "Observation_by_Vessel", "Observation by Vessel");
        }

        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
}