using System;
using System.Data;
using System.Collections;
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

using System.Diagnostics;


/// <summary>
/// Summary description for ExceldataImport
/// </summary>
/// 
namespace PO_RFQ_Generate
{

    public class ExceldataImport : Page
    {
        public static object Opt = Type.Missing;
        public static Microsoft.Office.Interop.Excel.Application ExlApp;
        public static Microsoft.Office.Interop.Excel.Workbook ExlWrkBook;
        public static Microsoft.Office.Interop.Excel.Worksheet ExlWrkSheet;
        //private Microsoft.Office.Interop.Excel.Range range;
        public ExceldataImport()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        Hashtable myHashtable;
        public void WriteExcell(DataSet ds, string Requisition, string FileName, string strSavePath, string FilePath)
        {
            CheckExcellProcesses();
            //Pick up the RFQ format file
            //   string strPath= Server.MapPath(".") + "\Technical\ExcelFile\RFQ_FormatFile.xls";
            //      string path = System.AppDomain.CurrentDomain.BaseDirectory + @"RFQ_FormatFile.xls";
            string path = FilePath + @"RFQ_FormatFile.xls";


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

                ExlWrkSheet.Cells[1, 3] = ds.Tables[2].Rows[0]["Vessel_name"].ToString();
                ExlWrkSheet.Cells[2, 3] = ds.Tables[0].Rows[0]["QUOTATION_CODE"].ToString();
                ExlWrkSheet.Cells[3, 3] = ds.Tables[0].Rows[0]["Quotation_Due_Date"].ToString();
                ExlWrkSheet.Cells[4, 3] = ds.Tables[0].Rows[0]["SHORT_NAME"].ToString();
                ExlWrkSheet.Cells[11, 3] = ds.Tables[0].Rows[0]["System_Description"].ToString();
                ExlWrkSheet.Cells[6, 12] = ds.Tables[0].Rows[0]["BUYER_COMMENTS"].ToString();
                ExlWrkSheet.Cells[7, 3] = ds.Tables[0].Rows[0]["SHORT_NAME"].ToString();
                ExlWrkSheet.Cells[1, 13] = ds.Tables[0].Rows[0]["Vessel_code"].ToString();
                ExlWrkSheet.Cells[2, 13] = ds.Tables[0].Rows[0]["DOCUMENT_CODE"].ToString();
                ExlWrkSheet.Cells[3, 13] = ds.Tables[0].Rows[0]["ITEM_SYSTEM_CODE"].ToString();
                ExlWrkSheet.Cells[5, 13] = DateTime.Now.ToString("yyyy/MM/dd");
                ExlWrkSheet.Cells[6, 13] = ds.Tables[0].Rows[0]["Quotation_CODE"].ToString();
                ExlWrkSheet.Cells[7, 13] = ds.Tables[0].Rows[0]["QUOTATION_SUPPLIER"].ToString();
                ExlWrkSheet.Cells[10, 3] = ds.Tables[0].Rows[0]["Name_Dept"].ToString();

                if (ds.Tables[4].Rows.Count > 0)
                {
                    ExlWrkSheet.Cells[10, 7] = ds.Tables[4].Rows[0]["MechInfo"].ToString();
                    ExlWrkSheet.Cells[11, 7] = ds.Tables[4].Rows[0]["Model_Type"].ToString();
                    ExlWrkSheet.Cells[12, 7] = ds.Tables[4].Rows[0]["MakerName"].ToString()
                                                + ' ' + ds.Tables[4].Rows[0]["MakerAddress"].ToString()
                                                + ' ' + ds.Tables[4].Rows[0]["MakerCity"].ToString()
                                                + ' ' + ds.Tables[4].Rows[0]["MakerEmail"].ToString()
                                                + ' ' + ds.Tables[4].Rows[0]["MakerCONTACT"].ToString()
                                                + ' ' + ds.Tables[4].Rows[0]["MakerPhone"].ToString()
                                                + ' ' + ds.Tables[4].Rows[0]["MakerFax"].ToString()
                                                + ' ' + ds.Tables[4].Rows[0]["MakerTELEX"].ToString()
                                                + ' ' + ds.Tables[4].Rows[0]["System_Serial_Number"].ToString();

                }



                int i = 15;
                foreach (DataRow dr in ds.Tables[1].Rows)
                {
                    ExlWrkSheet.Cells[i, 1] = dr[0].ToString();

                    ExlWrkSheet.Cells[i, 2] = dr[10].ToString();
                    ExlWrkSheet.Cells[i, 3] = dr[9].ToString();

                    ExlWrkSheet.Cells[i, 4] = dr[1].ToString();
                    ExlWrkSheet.Cells[i, 5] = dr[4].ToString();
                    ExlWrkSheet.Cells[i + 1, 5] = dr[5].ToString();
                    ExlWrkSheet.Cells[i + 2, 5] = dr[8].ToString();
                    ExlWrkSheet.Cells[i, 6] = dr[6].ToString();
                    ExlWrkSheet.Cells[i, 7] = dr[7].ToString();
                    // value will come in case of CTP
                    if (!dr.IsNull("quoted_rate"))
                        ExlWrkSheet.Cells[i, 8] = dr["quoted_rate"].ToString();
                    if (!dr.IsNull("quoted_discount"))
                        ExlWrkSheet.Cells[i, 9] = dr["quoted_discount"].ToString();


                    i = i + 3;
                }


                ExlWrkSheet.get_Range("A" + (ds.Tables[1].Rows.Count * 3 + 15).ToString(), "N1639").Delete(Microsoft.Office.Interop.Excel.XlDirection.xlUp);
                ExlWrkSheet.Cells[ds.Tables[1].Rows.Count * 3 + 15, 1] = ds.Tables[3].Rows[0]["LegalTerm"].ToString();

                //ExlWrkSheet.get_Range("F15", "H" + (ds.Tables[1].Rows.Count * 3 + 14).ToString()).Locked = false;
                //ExlWrkSheet.get_Range("J15", "J" + (ds.Tables[1].Rows.Count * 3 + 14).ToString()).Locked = false;

                //ExlWrkSheet.get_Range("I7", "I9").Locked = false;
                //ExlWrkSheet.get_Range("I10", "K11").Locked = false;



                ExlWrkSheet.get_Range("G9", "G9").NumberFormat = "#0.00";
                //ExlWrkSheet.get_Range("I5", "I6").Locked = false;
                //ExlWrkSheet.get_Range("L1", "M10").EntireColumn.Hidden = true;

                ExlWrkSheet.get_Range("M1", "M10").EntireColumn.Hidden = true;

                ExlWrkSheet.Protect("tessmave", Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, true, Type.Missing, Type.Missing);


                ExlWrkBook.SaveAs(strSavePath + FileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, true);
                string destFile = Server.MapPath("../Uploads/Purchase") + "\\" + FileName; ;
                File.Copy(strSavePath + FileName, destFile, true);

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
                KillExcel();

            }



        }

        private void CheckExcellProcesses()
        {
            Process[] AllProcesses = Process.GetProcessesByName("excel");
            myHashtable = new Hashtable();
            int iCount = 0;

            foreach (Process ExcelProcess in AllProcesses)
            {
                myHashtable.Add(ExcelProcess.Id, iCount);
                iCount = iCount + 1;
            }
        }

        private void KillExcel()
        {
            Process[] AllProcesses = Process.GetProcessesByName("excel");

            // check to kill the right process
            foreach (Process ExcelProcess in AllProcesses)
            {
                if (myHashtable.ContainsKey(ExcelProcess.Id) == false)
                    ExcelProcess.Kill();
            }

            AllProcesses = null;
        }
    }

}

