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
using System.IO;
using System.Runtime.InteropServices;

/// <summary>
/// Summary description for ExportQuatationEval
/// </summary>
/// 

namespace PO_RFQ_Generate
{
    public class ExportQuatationEval
    {
        //private readonly int iRowS = 8;
        Application oXL;
        _Workbook oWB;
        _Worksheet oSheet;
        Range oRng;
        string sheetval = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        public void createTableInExcel(System.Data.DataTable dtRequsition, System.Data.DataTable dtSupplier, System.Data.DataTable dtEvalItems, string FileName)
        {

            //try
            //{
                string strCurrentDir = ExcelCleanup.GetDrive() + "PURC\\QtnEval\\";

                if (!Directory.Exists(strCurrentDir))
                {
                    Directory.CreateDirectory(strCurrentDir);
                }


                oXL = new Application();
                oXL.Visible = false;
                //Get a new workbook.
                oWB = (_Workbook)(oXL.Workbooks.Add(System.Reflection.Missing.Value));
                oSheet = (_Worksheet)oWB.ActiveSheet;
                //System.Data.DataTable dtGridData=ds.Tables[0];

                //Requistion Data
                oRng = oSheet.get_Range("A2", "B2");
                oRng.Merge(2); 
                oRng.Value2="Requisition No:";
                oRng.Font.Bold = true; 
                //oSheet.Cells[2, 1] = "Requisition No:";
                //oSheet.get_Range("A2", "A2").Font.Bold = true;
                oSheet.Cells[2, 3] = dtRequsition.Rows[0]["REQUISITION_CODE"].ToString();


                oRng = oSheet.get_Range("A3", "B3");
                oRng.Merge(2);
                oRng.Value2 = "Catalogue Name :";
                oRng.Font.Bold = true; 
                //oSheet.Cells[3, 1] = "Catalogue Name :";
                //oSheet.get_Range("A3", "A3").Font.Bold = true;
                oSheet.Cells[3, 3] = dtRequsition.Rows[0]["SYSTEM_Description"].ToString();

                oRng = oSheet.get_Range("A4", "B4");
                oRng.Merge(2);
                oRng.Value2 = "Department Name :";
                oRng.Font.Bold = true; 

                //oSheet.Cells[4, 1] = "Department Name :";
                //oSheet.get_Range("A4", "A4").Font.Bold = true;
                oSheet.Cells[4, 3] = dtRequsition.Rows[0]["TOTAL_ITEMS"].ToString();

                oRng = oSheet.get_Range("A5", "B5");
                oRng.Merge(2);
                oRng.Value2 = "ToTal Items :"; 
                oRng.Font.Bold = true; 

                //oSheet.Cells[5, 1] = "ToTal Items :";
                //oSheet.get_Range("A5", "A5").Font.Bold = true;
                oSheet.Cells[5,3] = dtRequsition.Rows[0]["TOTAL_ITEMS"].ToString();
                oRng = oSheet.get_Range("A6", "B6");
                oRng.Merge(2);
                oRng.Value2 = "Qtn. Due Date :";
                oRng.Font.Bold = true; 
                //oSheet.Cells[6, 1] = "Date :";
                //oSheet.get_Range("A6", "A6").Font.Bold = true;
                oSheet.Cells[6, 3] = dtRequsition.Rows[0]["requestion_Date"].ToString();
                oSheet.get_Range("A1","C9").Font.Name = "Arial";
                oSheet.get_Range("A1", "C9").Font.Size = "8.25";

                //supplier Data 
                int iRow = 3;

                if (dtSupplier.Rows.Count > 0)
                {
                    oSheet.Cells[2, 4] = "Supplier Code";
                    oSheet.Cells[2, 5] = "Supplier Name";
                    oSheet.Cells[2, 6] = "Qtn Recived";
                    oSheet.Cells[2, 7] = "Select Supp.";
                    oSheet.Cells[2, 8] = "Items";
                    oSheet.Cells[2, 9] = "Amount";
                    oSheet.Cells[2, 10] = "Discount";
                    oSheet.Cells[2, 11] = "Surcharge";
                    oSheet.Cells[2, 12] = "Vat";
                    oSheet.Cells[2, 13] = "Total Amount";
                    //oSheet.Cells[2, 8] = "Is Qtn Received";
                    //oSheet.Cells[2, 9] = "Is Evaluated";
                    //oSheet.Cells[2, 10] = "Discount(%)";
                    //oSheet.Cells[2, 11] = "Currency";
                    //oSheet.Cells[2, 12] = "Exchange Rate";
                    //oSheet.Cells[2, 13] = "Total Items";
                    //oSheet.Cells[2, 14] = "Amount";
                    //oSheet.Cells[2, 15] = "Grand Total";

                    //for (int j = 0; j < dtSupplier.Columns.Count; j++)
                    //{
                    //    oSheet.Cells[iRow - 1, j + 4] = dtSupplier.Columns[j].ColumnName;
                    //}

                    oSheet.get_Range("D" + (iRow - 1).ToString(), "Q" + (iRow - 1).ToString()).Font.Bold = true;
                    oSheet.get_Range("D" + (iRow - 1).ToString(), "Q" + (iRow - 1).ToString()).Font.Name = "Arial";
                    oSheet.get_Range("D" + (iRow - 1).ToString(), "Q" + (iRow - 1).ToString()).Font.Size = "8.25";

                    //For each row, print the values of each column.

                    for (int rowNo = 0; rowNo < dtSupplier.Rows.Count; rowNo++)
                    {
                        //for (int colNo = 0; colNo < dtSupplier.Columns.Count; colNo++)
                        //{
                        //    oSheet.Cells[iRow, colNo + 4] = dtSupplier.Rows[rowNo][colNo].ToString();
                        //}
                        oSheet.Cells[iRow, 4] = dtSupplier.Rows[rowNo]["Supplier"].ToString();//[colNo].ToString()"Supplier Code";
                        oSheet.Cells[iRow, 5] = dtSupplier.Rows[rowNo]["short_Name"].ToString(); //"Supplier Name";
                        oSheet.Cells[iRow, 6] = dtSupplier.Rows[rowNo]["status"].ToString(); //"Qtn Recived";
                        oSheet.Cells[iRow, 7] = dtSupplier.Rows[rowNo]["Req_status"].ToString(); //"Select Supp.";
                        oSheet.Cells[iRow, 8] = dtSupplier.Rows[rowNo]["Items"].ToString(); //"Items";
                        oSheet.Cells[iRow, 9] = dtSupplier.Rows[rowNo]["Amount"].ToString(); //"Amount";
                        oSheet.Cells[iRow, 10] = dtSupplier.Rows[rowNo]["Discount"].ToString(); //"Discount";
                        oSheet.Cells[iRow, 11] = dtSupplier.Rows[rowNo]["Surcharges"].ToString();// "Surcharge(%)";
                        oSheet.Cells[iRow, 12] = dtSupplier.Rows[rowNo]["Vat"].ToString(); //"Vat(%)";
                        oSheet.Cells[iRow, 13] = dtSupplier.Rows[rowNo]["GrandTotal"].ToString();// "Total Amount";
                        oSheet.get_Range("A" + (iRow).ToString(), "Z" + (iRow).ToString()).Font.Name = "Arial";
                        oSheet.get_Range("A" + (iRow).ToString(), "Z" + (iRow ).ToString()).Font.Size = "8.25";
                        iRow++;
                    }
                }

                //Quatation Evalution Data 

                iRow = iRow + dtSupplier.Rows.Count + 6;
                if (dtEvalItems.Rows.Count > 0)
                {
                    oSheet.Cells[iRow - 1, 1] = "Sr. No.";
                    oSheet.Cells[iRow - 1, 2] = "Item Name";
                    oSheet.Cells[iRow - 1, 3] = "Reqst. qty";
                    int k = 4;
                    for (int j = 9; j < dtEvalItems.Columns.Count-1; j+=7)
                    {
                        string col_name=dtEvalItems.Columns[j].ColumnName.ToString(); 
                        col_name=col_name.Substring(0,col_name.Length -5);
                        dtSupplier.DefaultView.RowFilter = "Supplier='" + col_name + "'";
                        oRng = oSheet.get_Range(sheetval[k-1] + "" + (iRow - 1).ToString(), sheetval[k + 1] + "" + (iRow - 1).ToString());//, sheetval[k + 2] + (iRow - 1));
                        oRng.Merge(true);                        
                        oRng.Value2 = dtSupplier.DefaultView[0]["short_Name"].ToString();
                   
                       
                        oSheet.Cells[iRow , k] = "Rate";
                        oSheet.Cells[iRow, k+1] = "Discount";
                        oSheet.Cells[iRow , k + 2] = "Select";
                        k = k + 3;
                    }
                    oSheet.get_Range("A" + (iRow - 1).ToString(), "Z" + (iRow - 1).ToString()).Font.Bold = true;
                    oSheet.get_Range("A" + (iRow - 1).ToString(), "Z" + (iRow - 1).ToString()).Font.Name = "Arial";
                    oSheet.get_Range("A" + (iRow - 1).ToString(), "Z" + (iRow - 1).ToString()).Font.Size = "8.25";

                    // For each row, print the values of each column.
                    int l = 4;
                    iRow++;
                    iRow++;
                    for (int rowNo = 0; rowNo < dtEvalItems.Rows.Count; rowNo++)
                    {
                        l = 4;
                        oSheet.Cells[iRow - 1, 1] = dtEvalItems.Rows[rowNo]["ITEM_SERIAL_NO"].ToString();
                        oSheet.Cells[iRow - 1,2] = dtEvalItems.Rows[rowNo]["ITEM_SHORT_DESC"].ToString();
                        oSheet.Cells[iRow - 1, 3] = dtEvalItems.Rows[rowNo]["QUOTED_QTY"].ToString();
                        for (int colNo = 9; colNo < dtEvalItems.Columns.Count-1; colNo+=7)
                        {
                            oSheet.Cells[iRow - 1, l] = dtEvalItems.Rows[rowNo][colNo].ToString();
                            oSheet.Cells[iRow - 1, l + 1] = dtEvalItems.Rows[rowNo][colNo+2].ToString();
                            oSheet.Cells[iRow - 1, l + 2] = dtEvalItems.Rows[rowNo][colNo+4].ToString(); ;
                          //  oSheet.Cells[iRow, colNo + 1] = dtEvalItems.Rows[rowNo][colNo].ToString();
                            l =l + 3;
                        }
                        oSheet.get_Range("A" + (iRow - 1).ToString(), "Z" + (iRow - 1).ToString()).Font.Name = "Arial";
                        oSheet.get_Range("A" + (iRow - 1).ToString(), "Z" + (iRow - 1).ToString()).Font.Size = "8.25";
                        iRow++;
                    }

                }



                oRng = oSheet.get_Range("A1", "IV1");
                oRng.EntireColumn.AutoFit();
                oXL.Visible = false;
                oXL.UserControl = false;
                string strFile = FileName + DateTime.Now.ToString("yyMMddHHMMss") + ".xls";//+
                oWB.SaveAs(strCurrentDir + strFile, XlFileFormat.xlWorkbookNormal, null, null, false, false, XlSaveAsAccessMode.xlShared, false, false, null, null, null);



                // Need all following code to clean up and remove all references!!!
                oWB.Close(null, null, null);
                oXL.Workbooks.Close();
                oXL.Quit();
                Marshal.ReleaseComObject(oRng);
                Marshal.ReleaseComObject(oXL);
                Marshal.ReleaseComObject(oSheet);
                Marshal.ReleaseComObject(oWB);

                oRng = null;
                oXL = null;
                oSheet = null;
                oWB = null;

                GC.Collect();

                ExcelCleanup.ExcelProcessKill();
                ExcelCleanup.GetDrive();



            //}

            //catch (Exception Ex)
            //{

            //}
            //finally
            //{
            //    // Need all following code to clean up and remove all references!!!
            //    oWB.Close(null, null, null);
            //    oXL.Workbooks.Close();
            //    oXL.Quit();
            //    Marshal.ReleaseComObject(oRng);
            //    Marshal.ReleaseComObject(oXL);
            //    Marshal.ReleaseComObject(oSheet);
            //    Marshal.ReleaseComObject(oWB);

            //    oRng = null;
            //    oXL = null;
            //    oSheet = null;
            //    oWB = null;

            //    GC.Collect();

            //    ExcelCleanup.ExcelProcessKill();
            //    ExcelCleanup.GetDrive();

            //}

        }

    #region "Export To Excel with Dataset"

        public void createDataInExcel(DataSet ds, string FileName_Supplier, int SRow, int SCol)
        {
            Application oXL;
            _Workbook oWB;
            _Worksheet oSheet;
            Range oRng;
            string strCurrentDir = "c:\\";
            try
            {

                oXL = new Application();
                oXL.Visible = false;
                //Get a new workbook.
                oWB = (_Workbook)(oXL.Workbooks.Add(System.Reflection.Missing.Value));
                oSheet = (_Worksheet)oWB.ActiveSheet;
                //System.Data.DataTable dtGridData=ds.Tables[0];


                //Requistion Data

                //supplier Data 



                //Quatation Evalution Data 

                int iRow = 10;
                if (ds.Tables[0].Rows.Count > 0)
                {

                    for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                    {
                        oSheet.Cells[9, j + 1] = ds.Tables[0].Columns[j].ColumnName;
                    }
                    // For each row, print the values of each column.
                    for (int rowNo = 0; rowNo < ds.Tables[0].Rows.Count; rowNo++)
                    {
                        for (int colNo = 0; colNo < ds.Tables[0].Columns.Count; colNo++)
                        {
                            oSheet.Cells[iRow, colNo + 1] = ds.Tables[0].Rows[rowNo][colNo].ToString();
                        }
                        iRow++;
                    }

                }
                oRng = oSheet.get_Range("A1", "IV1");
                oRng.EntireColumn.AutoFit();
                oXL.Visible = false;
                oXL.UserControl = false;
                string strFile = FileName_Supplier + DateTime.Now.ToString("yyMMddHHMMss") + ".xls";//+
                oWB.SaveAs(strCurrentDir + strFile, XlFileFormat.xlWorkbookNormal, null, null, false, false, XlSaveAsAccessMode.xlShared, false, false, null, null, null);



                // Need all following code to clean up and remove all references!!!
                oWB.Close(null, null, null);
                oXL.Workbooks.Close();
                oXL.Quit();
                Marshal.ReleaseComObject(oRng);
                Marshal.ReleaseComObject(oXL);
                Marshal.ReleaseComObject(oSheet);
                Marshal.ReleaseComObject(oWB);

                //Response.Redirect("http://" + strMachineName + "/" + "ViewNorthWindSample/reports/" + strFile);
            }

            catch //(Exception theException)
            {

            }

        }

    #endregion

    }

}