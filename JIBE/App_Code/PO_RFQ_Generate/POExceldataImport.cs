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
using System.Runtime.InteropServices;
using System.IO;

/// <summary>
/// Summary description for POExceldataImport
/// </summary>
/// 
namespace PO_RFQ_Generate
{
    public class POExceldataImport
    {
        public POExceldataImport()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static object Opt = Type.Missing;
        public static Microsoft.Office.Interop.Excel.Application ExlApp;
        public static Microsoft.Office.Interop.Excel.Workbook ExlWrkBook;
        public static Microsoft.Office.Interop.Excel.Worksheet ExlWrkSheet;
        //private Microsoft.Office.Interop.Excel.Range range;

        public void WriteExcell(DataSet ds, string Requisition, string FileName, string strSavePath)
        {

            string path = System.AppDomain.CurrentDomain.BaseDirectory + @"Technical\ExcelFile\PO_FormatFile.xls";

            ExlApp = new Microsoft.Office.Interop.Excel.Application();
            try
            {
                ExlWrkBook = ExlApp.Workbooks.Open(path, 0,
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

                ExlWrkSheet.Cells[2, 3] = ds.Tables[0].Rows[0].ItemArray[1].ToString();
                ExlWrkSheet.Cells[2, 6] = ds.Tables[0].Rows[0].ItemArray[0].ToString();
                ExlWrkSheet.Cells[3, 6] = DateTime.Now.ToString("dd/MM/yyyy");
                ExlWrkSheet.Cells[3, 3] = DateTime.Now.ToString("dd/MM/yyyy");
                ExlWrkSheet.Cells[4, 3] = ds.Tables[0].Rows[0].ItemArray[8].ToString();
                ExlWrkSheet.Cells[5, 3] = ds.Tables[0].Rows[0].ItemArray[11].ToString();
                //ExlWrkSheet.Cells[3, 4] = "";
                //ExlWrkSheet.Cells[1, 11] = ds.Tables[2].Rows[0].ItemArray[0].ToString();
                ExlWrkSheet.Cells[2, 11] = "'" + ds.Tables[0].Rows[0].ItemArray[1].ToString() + "'";
                ExlWrkSheet.Cells[3, 11] = "'" + ds.Tables[0].Rows[0].ItemArray[3].ToString() + "'";
                ExlWrkSheet.Cells[4, 11] = ds.Tables[0].Rows[0].ItemArray[2].ToString();
                ExlWrkSheet.Cells[5, 11] = ds.Tables[0].Rows[0].ItemArray[4].ToString();
                ExlWrkSheet.Cells[4, 3] = ds.Tables[0].Rows[0].ItemArray[8].ToString();
                ExlWrkSheet.Cells[8, 3] = ds.Tables[0].Rows[0].ItemArray[9].ToString();
                ExlWrkSheet.Cells[9, 3] = ds.Tables[0].Rows[0].ItemArray[7].ToString();
                ExlWrkSheet.Cells[6, 11] = ds.Tables[0].Rows[0].ItemArray[6].ToString();
                int i = 11;
                if (ds.Tables[2].Rows.Count > 0)
                {
                    ExlWrkSheet.Cells[1, 3] = ds.Tables[2].Rows[0].ItemArray[1].ToString();
                    ExlWrkSheet.Cells[1, 11] = ds.Tables[2].Rows[0].ItemArray[0].ToString();

                }
                //ExlWrkSheet.get_Range("A15", "I" + (ds.Tables[1].Rows.Count *3).ToString()).Insert(Microsoft.Office.Interop.Excel.XlInsertShiftDirection.xlShiftDown, Type.Missing);

                foreach (DataRow dr in ds.Tables[1].Rows)
                {
                    ExlWrkSheet.Cells[i, 1] = dr[0].ToString();
                    ExlWrkSheet.Cells[i, 2] = dr[1].ToString();
                    ExlWrkSheet.Cells[i, 3] = dr[4].ToString();
                    ExlWrkSheet.Cells[i + 1, 3] = dr[5].ToString();
                    ExlWrkSheet.Cells[i + 2, 3] = dr[8].ToString();
                    ExlWrkSheet.Cells[i, 4] = dr[6].ToString();
                    ExlWrkSheet.Cells[i, 5] = dr[7].ToString();
                    ExlWrkSheet.Cells[i, 6] = dr[9].ToString();
                    ExlWrkSheet.Cells[i, 7] = dr[10].ToString();
                    //ExlWrkSheet.Cells[i, 6] = dr[8].ToString();
                    ExlWrkSheet.Cells[i, 9] = dr[11].ToString();
                    i = i + 3;
                }
                ExlWrkSheet.Cells[1637, 7] = ds.Tables[0].Rows[0].ItemArray[13].ToString();
                ExlWrkSheet.Cells[1639, 5] = ds.Tables[0].Rows[0].ItemArray[12].ToString();
                ExlWrkSheet.Cells[1640, 5] = ds.Tables[1].Rows[0].ItemArray[13].ToString();
                ExlWrkSheet.Cells[1641, 5] = ds.Tables[1].Rows[0].ItemArray[12].ToString();
                ExlWrkSheet.get_Range("A" + (ds.Tables[1].Rows.Count * 3 + 11).ToString(), "I1633").Delete(Microsoft.Office.Interop.Excel.XlDirection.xlUp);
                //ExlWrkSheet.get_Range("F11", "G" + (ds.Tables[1].Rows.Count * 3 + 10).ToString()).Locked = false;
                //ExlWrkSheet.get_Range("I11", "I" + (ds.Tables[1].Rows.Count * 3 + 10).ToString()).Locked = false;
                ExlWrkSheet.get_Range("G9", "G9").Locked = false;
                ExlWrkSheet.get_Range("G9", "G9").NumberFormat = "#0.00";
                ExlWrkSheet.get_Range("J1", "K5").EntireColumn.Hidden = true;
                ExlWrkSheet.Protect("tessmave", Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, true, Type.Missing, Type.Missing);

                if (!Directory.Exists(PO_RFQ_Generate.ExcelCleanup.GetDrive() + @"PURC/PO"))
                {
                    Directory.CreateDirectory(PO_RFQ_Generate.ExcelCleanup.GetDrive() + @"PURC/PO");
                }
                //string Path = "";

                //Path = @".\PURC\PO_Excel\" + Supplier;

                ExlWrkBook.SaveAs(strSavePath + FileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, true);




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
}