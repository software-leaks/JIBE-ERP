using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;

using System.Text;
using System.Runtime.InteropServices;

using System.Diagnostics;


/// <summary>
/// Summary description for GridViewExportUtil
/// </summary>
public class GridViewExportUtil : System.Web.UI.Page
{
    static Microsoft.Office.Interop.Excel.Workbook ExlWrkBook;
    static Microsoft.Office.Interop.Excel.Worksheet ExlWrkSheet;

    public GridViewExportUtil()
    {

    }


    public static void Export(string fileName, GridView gv)
    {
        Export(fileName, gv, "");
    }

    public static void Export(string fileName, GridView gv, string css)
    {
        string style = @"<style> TD { mso-number-format:\@; } </style>";
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", fileName));
        HttpContext.Current.Response.ContentType = "application/ms-excel";
        
        HtmlForm frm = new HtmlForm();

        gv.Parent.Controls.Add(frm);// .Parent.Controls.Add(frm);

        frm.Attributes["runat"] = "server";
        frm.Controls.Add(gv);

        using (StringWriter sw = new StringWriter())
        {
            using (HtmlTextWriter htw = new HtmlTextWriter(sw))
            {
                //  Create a form to contain the grid
                Table table = new Table();
                table.BorderStyle = BorderStyle.Solid;
                table.GridLines = gv.GridLines;


                //  add the header row to the table

                if (gv.HeaderRow != null)
                {
                    GridViewExportUtil.PrepareControlForExport(gv.HeaderRow);
                    //gv.HeaderRow.BackColor = System.Drawing.Color.SkyBlue;

                    for (int iCol = 0; iCol < gv.HeaderRow.Cells.Count; iCol++)
                    {
                        gv.HeaderRow.Cells[iCol].BackColor = System.Drawing.Color.SkyBlue;
                    }

                    for (int iCol = gv.Columns.Count - 1; iCol >= 0; iCol--)
                    {
                        if (gv.Columns[iCol].Visible == false)
                            gv.HeaderRow.Cells.RemoveAt(iCol);
                    }
                    table.Rows.Add(gv.HeaderRow);

                }

                //  add each of the data rows to the table
                foreach (GridViewRow row in gv.Rows)
                {
                    GridViewExportUtil.PrepareControlForExport(row);

                    for (int iCol = gv.Columns.Count - 1; iCol >= 0; iCol--)
                    {
                        if (gv.Columns[iCol].Visible == false)
                            row.Cells.RemoveAt(iCol);
                    }
                    table.Rows.Add(row);
                }

                //  add the footer row to the table
                if (gv.FooterRow != null)
                {
                    GridViewExportUtil.PrepareControlForExport(gv.FooterRow);
                    table.Rows.Add(gv.FooterRow);
                }
                //  render the table into the htmlwriter
                table.RenderControl(htw);

                //  render the htmlwriter into the response
                HttpContext.Current.Response.Write(style);
                HttpContext.Current.Response.Write(css);
                HttpContext.Current.Response.Write(sw.ToString());
                HttpContext.Current.Response.End();


            }
        }
    }

    public static void PrepareControlForExport(Control control)
    {
        for (int i = 0; i < control.Controls.Count; i++)
        {
            Control current = control.Controls[i];
            if (current is LinkButton)
            {
                control.Controls.Remove(current);
                control.Controls.AddAt(i, new LiteralControl((current as LinkButton).Text));
            }
            else if (current is ImageButton)
            {
                control.Controls.Remove(current);
                control.Controls.AddAt(i, new LiteralControl((current as ImageButton).AlternateText));
            }
            else if (current is HyperLink)
            {
                control.Controls.Remove(current);
                control.Controls.AddAt(i, new LiteralControl((current as HyperLink).Text));
            }
            else if (current is DropDownList)
            {
                control.Controls.Remove(current);
                control.Controls.AddAt(i, new LiteralControl((current as DropDownList).SelectedItem.Text));
            }
            else if (current is CheckBox)
            {
                control.Controls.Remove(current);
                control.Controls.AddAt(i, new LiteralControl((current as CheckBox).Checked ? "True" : "False"));
            }
            else if (current is Image)
            {
                control.Controls.Remove(current);
                //control.Controls.AddAt(i, new LiteralControl((current as Image).AlternateText));
            }
            else if (current is HiddenField)
            {
                control.Controls.Remove(current);
                //control.Controls.AddAt(i, new LiteralControl((current as Image).AlternateText));
            }

            else if (current is Label)
            {
                control.Controls.Remove(current);
                control.Controls.AddAt(i, new LiteralControl((current as Label).Text));
            }

            if (current.HasControls())
            {
                GridViewExportUtil.PrepareControlForExport(current);

            }
        }
    }

    public static void ShowExcel(DataTable dtexportdata, string[] HeaderCaptions, string[] DataColumnsName, string FileName, string FileHeaderName)
    {
        try
        {

            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + FileName + ".xls");
            HttpContext.Current.Response.ContentType = "application/ms-excel";
            HttpContext.Current.Response.Write("<TABLE BORDER='0' CELLPADDING='2' CELLSPACING='0' width='100%'>");
            HttpContext.Current.Response.Write("<tr>");
            HttpContext.Current.Response.Write("<td style='text-align:center;' colspan='" + (DataColumnsName.Length).ToString() + "'><h3>" + FileHeaderName + "</h3></td>");
            HttpContext.Current.Response.Write("</tr>");
            HttpContext.Current.Response.Write("</TABLE>");
            HttpContext.Current.Response.Write("<br />");


            HttpContext.Current.Response.Write("<TABLE BORDER='1' CELLPADDING='2' CELLSPACING='2' width='100%'>");
            HttpContext.Current.Response.Write("<tr style='background-color: #F2F2F2;'>");
            for (int i = 0; i < HeaderCaptions.Length; i++)
            {
                HttpContext.Current.Response.Write("<td width='20%'>");
                HttpContext.Current.Response.Write("<b>" + HeaderCaptions[i] + "</b>");
                HttpContext.Current.Response.Write("</td>");
            }
            HttpContext.Current.Response.Write("</tr>");
            foreach (DataRow dr in dtexportdata.Rows)
            {
                HttpContext.Current.Response.Write("<tr>");
                for (int i = 0; i < DataColumnsName.Length; i++)
                {
                    HttpContext.Current.Response.Write("<td>");
                    HttpContext.Current.Response.Write(dr[DataColumnsName[i]]);
                    HttpContext.Current.Response.Write("</td>");
                }
                HttpContext.Current.Response.Write("</tr>");
            }
            HttpContext.Current.Response.Write("</TABLE>");
            HttpContext.Current.Response.End();

        }
        catch (System.Threading.ThreadAbortException lException)
        {

        }
        catch (Exception ex)
        {

        }
    }


    public static void ShowExcel(DataTable dtexportdata, string[] HeaderCaptions, string[] DataColumnsName, string FileName, string FileHeaderName, string HtmlFilterTable)
    {
        try
        {

            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + FileName.Replace(" ","_") + ".xls");
            HttpContext.Current.Response.ContentType = "application/ms-excel";
            HttpContext.Current.Response.Write("<TABLE BORDER='0' width='100%'>");
            HttpContext.Current.Response.Write("<tr>");
            HttpContext.Current.Response.Write("<td colspan='" + (DataColumnsName.Length).ToString() + "'><h3>" + FileHeaderName + "</h3></td>");
            HttpContext.Current.Response.Write("</tr>");
            HttpContext.Current.Response.Write("</TABLE>");
            HttpContext.Current.Response.Write("<TABLE BORDER='1' width='100%'>");
            HttpContext.Current.Response.Write("<tr>");
            for (int i = 0; i < HeaderCaptions.Length; i++)
            {
                HttpContext.Current.Response.Write("<td>");
                HttpContext.Current.Response.Write("<b>" + HeaderCaptions[i] + "</b>");
                HttpContext.Current.Response.Write("</td>");
            }
            HttpContext.Current.Response.Write("</tr>");
            foreach (DataRow dr in dtexportdata.Rows)
            {
                HttpContext.Current.Response.Write("<tr>");
                for (int i = 0; i < DataColumnsName.Length; i++)
                {
                    HttpContext.Current.Response.Write("<td>");
                    HttpContext.Current.Response.Write(dr[DataColumnsName[i]]);
                    HttpContext.Current.Response.Write("</td>");
                }
                HttpContext.Current.Response.Write("</tr>");
            }
            HttpContext.Current.Response.Write("</TABLE>");
            HttpContext.Current.Response.End();

        }
        catch (System.Threading.ThreadAbortException lException)
        {

        }
        catch (Exception ex)
        {

        }
    }

    public void ExportGridviewToExcel(GridView GridViewexp)
    {

        // Reference your own GridView here

        string filename = String.Format("Quotations_{0}_{1}_{2}.xls", DateTime.Today.Day.ToString(),
            DateTime.Today.Month.ToString(), DateTime.Today.Year.ToString());

        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + filename);
        HttpContext.Current.Response.Charset = "";

        // SetCacheability doesn't seem to make a difference (see update)
        HttpContext.Current.Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);

        HttpContext.Current.Response.ContentType = "application/vnd.xls";

        System.IO.StringWriter stringWriter = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter htmlWriter = new HtmlTextWriter(stringWriter);

        // Replace all gridview controls with literals
        GridViewexp.BackColor = System.Drawing.Color.WhiteSmoke;
        GridViewRow grh = GridViewexp.HeaderRow;
        grh.BackColor = System.Drawing.Color.Teal;
        grh.ForeColor = System.Drawing.Color.White;

        foreach (TableCell cl in grh.Cells)
        {
            PrepareControlForExport_GridView(cl);
            cl.BackColor = System.Drawing.Color.Teal;
            cl.ForeColor = System.Drawing.Color.White;

        }


        foreach (GridViewRow gr in GridViewexp.Rows)
        {
            gr.Cells[0].Visible = true;
            foreach (TableCell cl in gr.Cells)
            {
                PrepareControlForExport_GridView(cl);

            }
        }


        System.Web.UI.HtmlControls.HtmlForm form
            = new System.Web.UI.HtmlControls.HtmlForm();
        Controls.Add(form);
      
        form.Controls.Add(GridViewexp);
        form.RenderControl(htmlWriter);

        HttpContext.Current.Response.Write(stringWriter.ToString());
        HttpContext.Current.Response.End();


    }

    public static void PrepareControlForExport_GridView(Control control)
    {
        for (int i = 0; i < control.Controls.Count; i++)
        {
            Control current = control.Controls[i];
            if (current is LinkButton)
            {
                TableCell cl = control as TableCell;
                cl.Text = (current as LinkButton).Text;
            }
            else if (current is ImageButton)
            {
                TableCell cl = control as TableCell;
                cl.Text = (current as ImageButton).ToolTip;
            }
            else if (current is HyperLink)
            {
                TableCell cl = control as TableCell;
                cl.Text = (current as HyperLink).Text;
            }
            else if (current is DropDownList)
            {
                TableCell cl = control as TableCell;
                cl.Text = (current as DropDownList).Items.Count > 0 ? (current as DropDownList).SelectedItem.Text : ""; ;
            }
            else if (current is CheckBox)
            {

                current.Visible = false;
            }
            else if (current is TextBox)
            {
                TableCell cl = control as TableCell;
                cl.Text = (current as TextBox).Text;

            }
            else if (current is Image)
            {
                TableCell cl = control as TableCell;
                cl.Text = (current as Image).AlternateText;

            }
            else if (current is Label)
            {
                TableCell cl = control as TableCell;
                cl.Text = (current as Label).Text;

            }
           

        }
    }

    public static void ExportToExcel(DataTable dtexportdata, string[] HeaderCaptions, string[] DataColumnsName, string FileName, string FileHeaderName)
    {
        try
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + FileName + ".xls");
            HttpContext.Current.Response.ContentType = "application/ms-excel";
            HttpContext.Current.Response.Write("<TABLE BORDER='0' CELLPADDING='2' CELLSPACING='0' width='100%'>");
            HttpContext.Current.Response.Write("<tr>");
            HttpContext.Current.Response.Write("<td style='text-align:center;' colspan='" + (DataColumnsName.Length).ToString() + "'><h3>" + FileHeaderName + "</h3></td>");
            HttpContext.Current.Response.Write("</tr>");
            HttpContext.Current.Response.Write("</TABLE>");
            HttpContext.Current.Response.Write("<br />");


            HttpContext.Current.Response.Write("<TABLE BORDER='1' CELLPADDING='2' CELLSPACING='2' width='100%'>");
            HttpContext.Current.Response.Write("<tr style='background-color: #F2F2F2;'>");
            for (int i = 0; i < HeaderCaptions.Length; i++)
            {
                HttpContext.Current.Response.Write("<td width='250' >");
                HttpContext.Current.Response.Write("<b>" + HeaderCaptions[i] + "</b>");
                HttpContext.Current.Response.Write("</td>");
            }
            HttpContext.Current.Response.Write("</tr>");
            foreach (DataRow dr in dtexportdata.Rows)
            {
                HttpContext.Current.Response.Write("<tr>");
                for (int i = 0; i < DataColumnsName.Length; i++)
                {
                    HttpContext.Current.Response.Write("<td>");
                    HttpContext.Current.Response.Write(dr[DataColumnsName[i]]);
                    HttpContext.Current.Response.Write("</td>");
                }
                HttpContext.Current.Response.Write("</tr>");
            }
            HttpContext.Current.Response.Write("</TABLE>");
            HttpContext.Current.Response.End();

        }
        catch (System.Threading.ThreadAbortException lException)
        {

        }
        catch (Exception ex)
        {

        }
    }


    public static void SetGridPrintOptions(string gridview, string strPrintHeader, string[] alCaptions, string[] alColumns, DataSet ds)
    {
        if (HttpContext.Current.Session["PRINTOPTIONS"] == null)
            HttpContext.Current.Session["PRINTOPTIONS"] = gridview;
        else
        {
            string[] arrGridView = HttpContext.Current.Session["PRINTOPTIONS"].ToString().Split(',');
            if (arrGridView.Length > 4)
            {
                foreach (string s in arrGridView)
                {
                    HttpContext.Current.Session[s] = null;
                    HttpContext.Current.Session[s + "PRINTHEADER"] = null;
                    HttpContext.Current.Session[s + "PRINTCAPTIONS"] = null;
                    HttpContext.Current.Session[s + "PRINTCOLUMNS"] = null;
                    HttpContext.Current.Session[s + "PRINTDATA"] = null;
                }
            }

            HttpContext.Current.Session["PRINTOPTIONS"] = HttpContext.Current.Session["PRINTOPTIONS"].ToString() + "," + gridview;
        }

        HttpContext.Current.Session[gridview] = gridview;
        HttpContext.Current.Session[gridview + "PRINTHEADER"] = strPrintHeader;
        HttpContext.Current.Session[gridview + "PRINTCAPTIONS"] = alCaptions;
        HttpContext.Current.Session[gridview + "PRINTCOLUMNS"] = alColumns;
        HttpContext.Current.Session[gridview + "PRINTDATA"] = ds;
    }


    public static void Show_CSV_TO_Excel(DataTable dtexportdata, string[] HeaderCaptions, string[] DataColumnsName, string FileName, string FileHeaderName, string HtmlFilterTable)
    {
        try
        {


            StringBuilder sbHeaderContent = new StringBuilder();
            StringBuilder sbFileContent = new StringBuilder();

            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + FileName + ".xls");
            HttpContext.Current.Response.ContentType = "text/csv";


            sbHeaderContent.Append(FileHeaderName);
            sbHeaderContent.AppendLine();
            sbHeaderContent.AppendLine();

            for (int i = 0; i < HeaderCaptions.Length; i++)
            {
                sbHeaderContent.Append(HeaderCaptions[i].ToString() + Convert.ToChar(9));
            }
            sbHeaderContent.AppendLine();

            foreach (DataRow dr in dtexportdata.Rows)
            {
                sbFileContent.AppendLine();

                for (int i = 0; i < DataColumnsName.Length; i++)
                {
                    sbFileContent.Append(dr[DataColumnsName[i]].ToString() + Convert.ToChar(9));
                }
            }

            HttpContext.Current.Response.Write(sbHeaderContent.ToString() + sbFileContent.ToString());

            HttpContext.Current.Response.End();

        }
        catch (System.Threading.ThreadAbortException lException)
        {

        }
        catch (Exception ex)
        {

        }
    }

    public static void Export_To_Excel_Interop(DataTable dtexportdata, string[] HeaderCaptions, string[] DataColumnsName, string FileHeader, string FullFileName, string ExportTempFilePath, string RelativeName = "")
    {
        object Opt = Type.Missing;
        Microsoft.Office.Interop.Excel.Application ExlApp = new Microsoft.Office.Interop.Excel.Application();


        try
        {


            ExlWrkBook = ExlApp.Workbooks.Open(ExportTempFilePath + "\\BlankExcelFormat.xls", 0,
                                                       true,
                                                       5,
                                                       "",
                                                       "",
                                                       true,
                                                       Microsoft.Office.Interop.Excel.XlPlatform.xlWindows,
                                                       "\t",
                                                       false,
                                                       false,
                                                       0,
                                                       true,
                                                       1,
                                                       0);
            ExlWrkSheet = (Microsoft.Office.Interop.Excel.Worksheet)ExlWrkBook.ActiveSheet;


            ExlWrkSheet.Cells[1, 1] = FileHeader;
            string Exlhead = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            Microsoft.Office.Interop.Excel.Range rangeTitle = ExlWrkSheet.get_Range("A1", Exlhead[HeaderCaptions.Length - 1] + "1");
            rangeTitle.Cells.Merge();
            // rangeTitle.Cells.Interior.Color = System.Drawing.Color.Gray;



            int irow = 2, icol = 1;
            for (int i = 0; i < HeaderCaptions.Length; i++)
            {
                ExlWrkSheet.Cells[irow, icol] = Convert.ToString(HeaderCaptions[i]);
                Microsoft.Office.Interop.Excel.Range rangHeader = ExlWrkSheet.get_Range(Exlhead[i] + "2", Exlhead[i] + "2");


                ExlWrkSheet.Columns[i + 1].ColumnWidth = HeaderCaptions[i].Length + 10;
                ExlWrkSheet.Cells[irow, icol].Font.FontStyle = "Bold";
                ExlWrkSheet.Cells[irow, icol].Font.Size = 12;
                //ExlWrkSheet.Cells[irow, icol].Font.ColorIndex = 9;
                // ExlWrkSheet.Cells[irow, icol].Interior.Color = System.Drawing.Color.LightGray;

                icol++;
            }

            irow = 3; icol = 1;
            foreach (DataRow dr in dtexportdata.Rows)
            {
                for (int i = 0; i < DataColumnsName.Length; i++)
                {
                    ExlWrkSheet.Cells[irow, icol] = Convert.ToString(dr[DataColumnsName[i]]);

                    icol++;
                }

                irow++;
                icol = 1;
            }

            Microsoft.Office.Interop.Excel.Range range = ExlWrkSheet.get_Range("A2", Exlhead[DataColumnsName.Length - 1] + (irow - 1).ToString());
            range.Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            range.WrapText = true;

            ExlWrkBook.SaveAs(FullFileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, true);
            ResponseHelper.Redirect(RelativeName, "_self", "");


        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            ExlWrkBook.Close(null, null, null);
            ExlApp.Workbooks.Close();
            ExlApp.Quit();
            Marshal.ReleaseComObject(ExlApp);
            Marshal.ReleaseComObject(ExlWrkSheet);
            Marshal.ReleaseComObject(ExlWrkBook);

        }
    }

}
