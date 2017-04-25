using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;

 

/// <summary>
/// Summary description for ExcelReportGeneration
/// </summary>
/// 
namespace CrystalReportsFile
{
    public class ExcelReportGeneration
    {

        private Microsoft.Office.Interop.Excel.Application ExcelApp;
        private Microsoft.Office.Interop.Excel.Workbook objBook;
        private Microsoft.Office.Interop.Excel.Worksheet objSheet;
        private Microsoft.Office.Interop.Excel.Range range;
        //private Excel.Range workSheet_range = null;
        //string strAttendeeList, strAbsenteeList, strCopiesToList;
        //int totActionItems;
        //object oMissing, oTemplate;
        //private string strTitle;
        string sheet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";


        public ExcelReportGeneration()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public void CreateFile(System.Data.DataSet ds,string Dept_Code)
        {

            object missing = System.Reflection.Missing.Value;
            object fileName = "normal.dot";
            object newTemplate = false;
            object docType = 0;
            object isVisible = true;

            ExcelApp = new Microsoft.Office.Interop.Excel.ApplicationClass();
            objBook = ExcelApp.Workbooks.Add(missing);
            objSheet = (Microsoft.Office.Interop.Excel.Worksheet)objBook.Sheets["Sheet1"];
            //objSheet.Name = "It's Me";
            int i = 0;
            //objSheet.get_Range("A1", "A" + (dt.Rows.Count + 2).ToString()).WrapText = true;  
            //range = objSheet.get_Range(sheet[i].ToString() + "1", sheet[i].ToString() + "1");
            //range.Merge(2);
            int k = 0;
            foreach (DataColumn column in ds.Tables[0].Columns)
            {
                i++;
                if (i <= ds.Tables[0].Columns.Count - 1)
                {
                    if (i % 2 == 1 && i > 2)
                    {

                        if (i != 3 && i <= ds.Tables[0].Columns.Count - 6)
                        {
                            range = objSheet.get_Range(sheet[i - 1].ToString() + "1", sheet[i].ToString() + "1");
                            range.Merge(2);
                            if (ds.Tables[3].Rows.Count > k)
                            {
                                range.Value2 = "Received: " + ds.Tables[3].Rows[k].ItemArray[0].ToString();
                                k++;
                            }
                            else
                                range.Value2 = "Received: ";
                        }
                        else if (i != 3 && i == ds.Tables[0].Columns.Count - 5)
                        {
                            range = objSheet.get_Range(sheet[i - 1].ToString() + "1", sheet[i].ToString() + "1");
                            range.Merge(2);
                            range.Value2 = "Total Received";
                        }
                        else if (i != 3 && i == ds.Tables[0].Columns.Count - 3)
                        {
                            range = objSheet.get_Range(sheet[i].ToString() + "1", sheet[i + 1].ToString() + "1");
                            range.Merge(2);
                            range.Value2 = "Closing";
                        }
                        else if (i == 3)
                        {
                            range = objSheet.get_Range(sheet[i - 1].ToString() + "1", sheet[i].ToString() + "1");
                            range.Merge(2);
                            range.Value2 = "Opening";
                        }

                    }

                    if (i == 1)
                    {
                        range = objSheet.get_Range(sheet[i - 1].ToString() + "1", sheet[i - 1].ToString() + "2");
                        range.Merge(1);
                        objSheet.Cells[2, i] = column.ColumnName.Replace("_", " ").ToUpper();
                    }
                    else if (i == 2)
                    {
                        range = objSheet.get_Range(sheet[i - 1].ToString() + "1", sheet[i - 1].ToString() + "2");
                        range.Merge(1);
                        objSheet.Cells[2, i] = "Unit";
                    }
                    else if (i == ds.Tables[0].Columns.Count - 3)
                    {
                        range = objSheet.get_Range(sheet[i - 1].ToString() + "1", sheet[i - 1].ToString() + "2");
                        range.Merge(1);
                        objSheet.Cells[2, i] = "Unit Price";
                    }
                    else
                    {
                        if (i % 2 == 1 && i <= ds.Tables[0].Columns.Count - 4)
                        {
                            range = objSheet.get_Range(sheet[i - 1].ToString() + "1", Type.Missing);
                            range.NumberFormat = "###00";
                            objSheet.Cells[2, i] = "Qty";
                        }
                        else if (i % 2 == 0 && i <= ds.Tables[0].Columns.Count - 3)
                        {
                            range = objSheet.get_Range(sheet[i - 1].ToString() + "1", Type.Missing);
                            range.NumberFormat = "$00.00";

                            objSheet.Cells[2, i] = "Amount";

                        }
                        else if (i == ds.Tables[0].Columns.Count - 2)
                        {
                            objSheet.Cells[2, i] = "Qty";
                            range = objSheet.get_Range(sheet[i - 1].ToString() + "1", Type.Missing);
                            range.NumberFormat = "###00";
                        }
                        else if (i == ds.Tables[0].Columns.Count - 1)
                        {
                            objSheet.Cells[2, i] = "Amount";
                            range = objSheet.get_Range(sheet[i - 1].ToString() + "1", Type.Missing);
                            range.NumberFormat = "$00.00";
                        }
                    }
                    if (i == 1)
                        SetColumnWidth(objSheet, i, 30);
                    else
                        SetColumnWidth(objSheet, i, 10);

                    if (i == 1)
                        SetTwoDecimalPlace(objSheet, i, 30);
                    else
                        SetTwoDecimalPlace(objSheet, i, 10);
                }

            }
            i = i - 1;
            range = objSheet.get_Range(sheet[i].ToString() + "1", sheet[i + 1].ToString() + "1");
            range.Merge(2);
            range.Value2 = "consumed";
            SetColumnWidth(objSheet, i + 1, 6);
            SetColumnWidth(objSheet, i + 2, 6);


            SetTwoDecimalPlace(objSheet, i + 1, 10);
            SetTwoDecimalPlace(objSheet, i + 2, 10);

            objSheet.Cells[2, i + 2] = "Amount";
            objSheet.Cells[2, i + 1] = "Qty";
            i = 2;
            decimal Total_bal = 0;
            decimal Total_Recived = 0;
            decimal Total_CBal = 0;
            decimal Total_consumed = 0;
            System.Data.DataTable dt = new System.Data.DataTable();
            dt = ds.Tables[0];

            foreach (DataRow drSubcatalog in ds.Tables[2].Rows)
            {

                i++;
                dt.DefaultView.RowFilter = "subsystem_code='" + drSubcatalog.ItemArray[1].ToString() + "'";
                //objSheet.get_Range("A1", "A" + (dt.Rows.Count + 2).ToString()).WrapText = true;
                range = objSheet.get_Range("A" + i.ToString(), "E" + i.ToString());
                range.Merge(2);
                range.Value2 = "Sub CataLogue: " + drSubcatalog.ItemArray[2].ToString();
                range.Font.Bold = true;

                //objSheet.Cells[i, 1] = drSubcatalog.ItemArray[2].ToString();

                foreach (DataRowView dr in dt.DefaultView)
                {
                    i++;

                    for (int j = 0; j < ds.Tables[0].Columns.Count - 1; j++)
                    {
                        objSheet.Cells[i, j + 1] = dr[j].ToString();

                    }
                    objSheet.Cells[i, dt.Columns.Count] = (Convert.ToDecimal(dr[ds.Tables[0].Columns.Count - 6].ToString()) + Convert.ToDecimal(dr[2].ToString()) - Convert.ToDecimal(dr[ds.Tables[0].Columns.Count - 3].ToString())).ToString();
                    objSheet.Cells[i, dt.Columns.Count + 1] = (Convert.ToDecimal(dr[ds.Tables[0].Columns.Count - 5].ToString()) + Convert.ToDecimal(dr[3].ToString()) - Convert.ToDecimal(dr[ds.Tables[0].Columns.Count - 2].ToString())).ToString();
                    Total_bal = Total_bal + Convert.ToDecimal(dr[3].ToString());
                    Total_Recived = Total_Recived + Convert.ToDecimal(dr[ds.Tables[0].Columns.Count - 5].ToString());
                    Total_CBal = Total_CBal + Convert.ToDecimal(dr[ds.Tables[0].Columns.Count - 2].ToString());
                    //Total_consumed = Total_consumed + Convert.ToDecimal(dr.ItemArray[3].ToString());
                }

            }
            Total_consumed = Total_bal + Total_Recived - Total_CBal;
            i = i + 2;
            objSheet.Cells[i, 4] = Total_bal.ToString();
            objSheet.Cells[i, dt.Columns.Count - 4] = Total_Recived.ToString();
            objSheet.Cells[i, dt.Columns.Count - 1] = Total_CBal.ToString();
            objSheet.Cells[i, dt.Columns.Count + 1] = Total_consumed.ToString();


            objSheet.get_Range("A1", "A" + (ds.Tables[0].Rows.Count + 2).ToString()).WrapText = true;


            objSheet.get_Range("A1", sheet[ds.Tables[0].Columns.Count].ToString() + (ds.Tables[0].Rows.Count + 15).ToString()).Font.Name = "Arial";
            objSheet.get_Range("A1", sheet[ds.Tables[0].Columns.Count].ToString() + (ds.Tables[0].Rows.Count + 15).ToString()).Font.Size = "8.25";
            objSheet.get_Range("A1", sheet[ds.Tables[0].Columns.Count].ToString() + "2").Font.Bold = true;
            objSheet.get_Range("A1", sheet[ds.Tables[0].Columns.Count].ToString() + "2");
            objSheet.get_Range("A1", sheet[ds.Tables[0].Columns.Count].ToString() + (ds.Tables[0].Rows.Count + 15).ToString()).Font.Name = "Arial";
            objSheet.get_Range("A1", sheet[ds.Tables[0].Columns.Count].ToString() + (ds.Tables[0].Rows.Count + 15).ToString()).Font.Name = "Arial";
            //objSheet.get_Range("A1", sheet[ds.Tables[0].Columns.Count + 1].ToString() + (dt.Rows.Count + 2).ToString()).
            objSheet.get_Range("A1", sheet[ds.Tables[0].Columns.Count].ToString() + "2").BorderAround(Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous, Microsoft.Office.Interop.Excel.XlBorderWeight.xlMedium,
                          Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic, Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic);
            //  objSheet.get_Range("A1", sheet[dt.Columns.Count - 1].ToString() + "2")
            objSheet.PageSetup.LeftHeader =HttpContext.Current.Session["Company_Name_GL"] + " \n\n" + ds.Tables[4].Rows[0][2].ToString().ToUpper();
            objSheet.PageSetup.RightHeader = "C-2\n\n Date: " + DateTime.Now.ToString("dd/MM/yyyy");
            string File_Name = "";
         
            if (Dept_Code == "BS")
            {
                objSheet.PageSetup.CenterHeader = "\n BOND INVENTORY";
                File_Name = "Bond_Inv_" + DateTime.Now.Month + "_" + DateTime.Now.ToString("yyMMddHHMMss") + ".xls";
            }
            else
            {
                objSheet.PageSetup.CenterHeader = "\n PROVISION INVENTORY";
                File_Name = "Pro_Inv_" + DateTime.Now.Month + "_" + DateTime.Now.ToString("yyMMddHHMMss") + ".xls";
            }

            objSheet.PageSetup.LeftMargin = 0.5;
            //objSheet.PageSetup.BottomMargin = 2.5;
            objSheet.PageSetup.RightMargin = 0.5;
            objSheet.PageSetup.HeaderMargin = 2.0;
            objSheet.PageSetup.FooterMargin = 2.0;

            objSheet.PageSetup.CenterFooter = "";
            objSheet.PageSetup.LeftFooter = "CH COOK \nOriginal: " + HttpContext.Current.Session["Company_Name_GL"]  + " \nCopy: Ship's File";
            objSheet.PageSetup.RightFooter = ds.Tables[4].Rows[0][2].ToString() + "                                            ";
            objSheet.PageSetup.PrintGridlines = true;

            objSheet.PageSetup.Orientation = Microsoft.Office.Interop.Excel.XlPageOrientation.xlLandscape;

            //objSheet.PageSetup.PrintHeadings=true;
            objSheet.PageSetup.PrintTitleRows = "A1:H2";
            ExcelApp.Visible = true;
            objSheet.PrintPreview(true);



            //if (!Directory.Exists(LibClass.ExcelCleanup.GetDrive() + @"PURC/Inventory"))
            //{
            //    Directory.CreateDirectory(LibClass.ExcelCleanup.GetDrive() + @"PURC/Inventory");
            //}
            //string Path = "";
            //Path = LibClass.ExcelCleanup.GetDrive() + @"PURC\Inventory\" + File_Name;


            //objBook.SaveAs(Path, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, true);

            //System.Diagnostics.Process.Start(LibClass.ExcelCleanup.GetDrive() + @"PURC\Inventory");

            objBook.Close(null, null, null);
            ExcelApp.Workbooks.Close();
            ExcelApp.Quit();
            Marshal.ReleaseComObject(ExcelApp);
            Marshal.ReleaseComObject(objSheet);
            Marshal.ReleaseComObject(objBook);
            //LibClass.ExcelCleanup.ExcelProcessKill();


        }
        public void SetColumnWidth(Worksheet ws, int col, int width)
        {
            ((Range)ws.Cells[1, col]).EntireColumn.ColumnWidth = width;
        }

        public void SetTwoDecimalPlace(Worksheet ws, int col, int width)
        {
            ((Range)ws.Cells[1, col]).EntireColumn.NumberFormat = "####0.00";
        }
    

    }
}



 
