using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using SMS.Business.INFRA;
using System.Web.Services;
using System.Data;
using SMS.Business.Infrastructure;
using System;
using SMS.Properties;
using System.IO;
using System.Globalization;
using System.Web.UI.WebControls;
using SMS.Business.LMS;

public partial class JibeWebService
{
     

    #region . Equipment statistics

    [WebMethod]
    public string Get_Function_Tree_Videos(string id)
    {


        DataTable dt = BLL_LMS_Training.GET_VESSEL_VIDEOS(null);

        return ConvertDataTabletoJson(dt);

    }

    


       [WebMethod]
    public string Get_Video_FileName(string id)
    {

       string FileName = BLL_LMS_Training.GET_VideoFileName(UDFLib.ConvertIntegerToNull(id));

       return FileName;
       

    }



       [WebMethod]
       public string[] ayncLoadDrillCalendar(string pStartDate, string Vessel_ID)
       {
           //int StartMont = Convert.ToInt32(pStartMont);
           //int Year = Convert.ToInt32(pYear);
           DateTime pxStartDate = Convert.ToDateTime(pStartDate);
           List<string> MonthName = new List<string>();
           List<int> MonthDays = new List<int>();
           DateTime lStartDate = new DateTime(pxStartDate.Year, pxStartDate.Month, 1);
           DataSet ds = BLL_LMS_Training.GET_YEARLY_DRILL_REPORT(lStartDate.Month, lStartDate.Year, Convert.ToInt32(Vessel_ID));
           Table DrillTable = new Table();
           DrillTable.CellPadding = 0;
           DrillTable.CellSpacing = 0;
           Table DrillTable0 = new Table();
           DrillTable0.CellPadding = 0;
           DrillTable0.CellSpacing = 0;
           TableRow MonthRow;
           TableRow MonthProgramRow;
           TableCell monthCell;
           TableCell programHeadCell = new TableCell();
           MonthRow = new TableRow();
           programHeadCell.Text = "Drill Name";
           programHeadCell.CssClass = "ProgramHeadStyle";
           MonthRow.Cells.Add(programHeadCell);
           string infoText = lStartDate.ToString("MMMM", new CultureInfo("en-GB")) + " " + lStartDate.Year + " to " + lStartDate.AddMonths(11).ToString("MMMM", new CultureInfo("en-GB")) + " " + lStartDate.AddMonths(11).Year;
           for (int i = 0; i < 12; i++)
           {
               //MonthName.Add(lStartDate.AddMonths(i).ToString("MMMM", new CultureInfo("en-GB")));
               monthCell = new TableCell();
               monthCell.Text = lStartDate.AddMonths(i).ToString("MMM/yy", new CultureInfo("en-GB"));
               //monthCell.ColumnSpan = DateTime.DaysInMonth(l.Year, l.Month);
               monthCell.CssClass = "MonthStyle";
               MonthRow.Cells.Add(monthCell);
           }
           DrillTable0.Rows.Add(MonthRow);

           List<int> usedPrograms = new List<int>();
           foreach (DataRow item in ds.Tables[0].Rows)
           {
               string lProgramName = item["ProgramName"].ToString();
               int lProgram_ID = Convert.ToInt32(item["Program_ID"]);
               if (!usedPrograms.Contains(lProgram_ID))
               {
                   usedPrograms.Add(lProgram_ID);
               }
               else
               {
                   continue;
               }
               //DataRow[] drs = ds.Tables[0].Select("Program_ID=" + lProgram_ID);
               TableCell ProgramCell = new TableCell();
               ProgramCell.Text = lProgramName;
               ProgramCell.CssClass = "ProgramCellStyle";
               MonthProgramRow = new TableRow();
               MonthProgramRow.Cells.Add(ProgramCell);
               for (int i = 0; i < 12; i++)
               {
                   DataRow[] drs = ds.Tables[0].Select("Program_ID=" + lProgram_ID + " and MONTHINDEX=" + lStartDate.AddMonths(i).Month);
                   TableCell MonthProgramCell = new TableCell();
                   if (drs.Length > 0)
                   {
                       bool fStatus = true;
                       foreach (DataRow StatusRow in drs)
                       {
                           if (StatusRow["Status"].ToString() == "0")
                           {
                               fStatus = false;
                               break;
                           }
                       }
                       if (fStatus)
                       {
                           MonthProgramCell.CssClass = "MonthProgramCellSyle1";
                           MonthProgramCell.ToolTip = "Completed Drill (All drills are Completed)";
                       }

                       else
                       {
                           if (Convert.ToDateTime(drs[0]["Sch_Date"]).Date <= DateTime.Now.Date)
                           {
                               MonthProgramCell.CssClass = "MonthProgramCellSyle2";
                               MonthProgramCell.ToolTip = "Overdue Drill";
                           }
                           else
                           {
                               MonthProgramCell.CssClass = "MonthProgramCellSyle3";
                               MonthProgramCell.ToolTip = "Planned Drill";
                           }
                       }
                       MonthProgramCell.Text = "**";
                       System.Text.StringBuilder info = new System.Text.StringBuilder();
                       info.Append("<div style='margin:5px;border-radius:5px'>");
                       info.Append("<table cellpadding='3px' >");
                   }
                   else
                   {
                       MonthProgramCell.CssClass = "MonthProgramCellSyle0";
                   }
                   MonthProgramRow.Cells.Add(MonthProgramCell);

               }
               DrillTable.Rows.Add(MonthProgramRow);
           }

           DrillTable.ID = "t01";
           DrillTable0.ID = "t00";
           string newt, newt0 = "";
           using (StringWriter sw = new StringWriter())
           {

               DrillTable.RenderControl(new HtmlTextWriter(sw));
               newt = sw.ToString();
           }
           using (StringWriter sw = new StringWriter())
           {

               DrillTable0.RenderControl(new HtmlTextWriter(sw));
               newt0 = sw.ToString();
           }
           String[] result = { newt, infoText, newt0 };
           return result;
       }

    
    #endregion

     

}
