using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.PURC;
using System.Collections;
using System.IO;
using System.Data;
using System.Runtime.InteropServices;
using ExcelNS = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop;
using SMS.Properties;
using SMS.Business.Infrastructure;


public partial class Purchase_CTP_Contract_List : System.Web.UI.Page
{
    public UserAccess objUA = new UserAccess();
    MergeGridviewHeader_Info objContractList = new MergeGridviewHeader_Info();

    protected void Page_Load(object sender, EventArgs e)
    {
        UserAccessValidation();
        lblmsg.Text = "";
        objContractList.AddMergedColumns(new int[] { 6, 7 }, "No. of items", "HeaderStyle-css");


        if (!IsPostBack)
        {
            // first check for supplier's log-in

            string s = UDFLib.ConvertStringToNull(1);

            if (string.IsNullOrWhiteSpace(Convert.ToString(Session["SUPPCODE"])) && UDFLib.ConvertToInteger(Session["userid"]) == 0)
            {
                Server.Transfer("../FileNotFound.aspx");
            }
            //else if (Request.QueryString["__supplier_`_code"] != null)
            //{
            //    Session["userid"] = Request.QueryString["__supplier_`_code"];
            //}
            uc_Purc_DepartmentListctp.SelectedValue = "0";
            ctlPortListctp.SelectedValue = "0";
           
            if (!string.IsNullOrWhiteSpace(Convert.ToString(Session["SUPPCODE"])) && Convert.ToString(Session["SUPPCODE"])!="0")
            {
                uc_SupplierListCTP.SelectedValue = Convert.ToString(Session["SUPPCODE"]);
                lblsupp.Enabled = false;
                hlnkCreateNewContract.Visible = false;
                btnCompare.Visible = false;
                gvContractList.ShowFooter = false;
                chkQtnStatus.Visible = false;
                chkContractStatus.Visible = false;
            }
            else
            {
                uc_SupplierListCTP.SelectedValue = "0";
            }
            BindDataItems();
        }
    }

    protected void UserAccessValidation()
    {
        int CurrentUserID = Convert.ToInt32(Session["userid"]);
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());
        BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();

        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
            Response.Redirect("~/default.aspx?msgid=1");

        if (objUA.Add == 0)
        {
            hlnkCreateNewContract.Visible = false;
        }
        if (objUA.Edit == 0)
        {


        }
        if (objUA.Approve == 0)
        {

        }
        if (objUA.Delete == 0)
        {

            // You don't have sufficient previlege to access the requested page.
        }


    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindDataItems();
    }
    protected void btnClearFilter_Click(object sender, EventArgs e)
    {
        uc_Purc_DepartmentListctp.SelectedValue = "0";

        if (string.IsNullOrWhiteSpace(Convert.ToString(Session["SUPPCODE"])))
        {
            uc_SupplierListCTP.SelectedValue = "0";
            chkQtnStatus.ClearSelection();
            chkContractStatus.SelectedValue = "1";
            //chkContractStatus.ClearSelection();
        }
        ctlPortListctp.SelectedValue = "0";
        txtEffdtFrom.Text = "";
        txtEffdtTo.Text = "";
        txtItemSearch.Text = "";


        BindDataItems();
    }

    protected void BindDataItems()
    {
        int is_Fetch_Count = ucCustomPagerctp.isCountRecord;
        gvContractList.DataSource = BLL_PURC_CTP.Get_Ctp_Contract_List(UDFLib.ConvertStringToNull(uc_SupplierListCTP.SelectedValue),
                                                                     UDFLib.ConvertIntegerToNull(uc_Purc_DepartmentListctp.SelectedValue),
                                                                     UDFLib.ConvertDateToNull(txtEffdtFrom.Text),
                                                                     UDFLib.ConvertDateToNull(txtEffdtTo.Text),
                                                                     UDFLib.ConvertStringToNull(chkQtnStatus.SelectedValue),
                                                                     UDFLib.ConvertIntegerToNull(chkContractStatus.SelectedValue),
                                                                     UDFLib.ConvertIntegerToNull(ctlPortListctp.SelectedValue),
                                                                     UDFLib.ConvertStringToNull(txtItemSearch.Text),
                                                                     ucCustomPagerctp.CurrentPageIndex,
                                                                     ucCustomPagerctp.PageSize,
                                                                     ref is_Fetch_Count);
        gvContractList.DataBind();

        if (ucCustomPagerctp.isCountRecord == 1)
        {
            ucCustomPagerctp.CountTotalRec = is_Fetch_Count.ToString();
            ucCustomPagerctp.BuildPager();
        }

    }

    protected void gvContractList_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            MergeGridviewHeader.SetProperty(objContractList);

            e.Row.SetRenderMethodDelegate(MergeGridviewHeader.RenderHeader);
            ViewState["DynamicHeaderCSS"] = "HeaderStyle-css";
        }


    }

    protected void btnWebRFQ_Click(object s, EventArgs e)
    {

        CTP_RFQ_Mail objmail = new CTP_RFQ_Mail();
        try
        {
            objmail.SendMailToSupplier(Convert.ToInt32(((ImageButton)s).CommandArgument), 2, this.Page);
        }
        catch (Exception ex)
        {
            lblmsg.Text = ex.Message;
        }
    }

    protected void btnExportEcelRFQ_Click(object s, EventArgs e)
    {
        try
        {
            CTP_RFQ_Mail objmail = new CTP_RFQ_Mail();
            objmail.SendMailToSupplier(Convert.ToInt32(((ImageButton)s).CommandArgument), 1, this.Page);
        }
        catch (Exception ex)
        {
            lblmsg.Text = ex.Message;
        }
    }

    public static object Opt = Type.Missing;
    public static Microsoft.Office.Interop.Excel.Application ExlApp;
    public static Microsoft.Office.Interop.Excel.Workbook ExlWrkBook;
    public static Microsoft.Office.Interop.Excel.Worksheet ExlWrkSheet;

    protected void btnUpload_Click(object sender, EventArgs e)
    {




        try
        {


            if (FileUpload1.HasFile)
            {
                //FileUpload1.
                string strLocalPath = FileUpload1.PostedFile.FileName;

                string FileName = Path.GetFileName(strLocalPath);

                if (Path.GetExtension(FileName).ToUpper() == ".XLS")
                {
                    try
                    {

                        FileUpload1.PostedFile.SaveAs(Server.MapPath("TempUpload\\" + FileName));


                        string strPath = Server.MapPath("TempUpload\\" + FileName).ToString();


                        //string[] arrfn = FileName.Split('\\');
                        // string strPath = Server.MapPath("SendRFQ") + "\\" + arrfn[arrfn.Length - 1];

                        ExlApp = new Microsoft.Office.Interop.Excel.Application();
                        ExlWrkBook = ExlApp.Workbooks.Open(strPath,
                                                                  0,
                                                                  true,
                                                                  5,
                                                                  "", "",
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


                        int Quotation_ID = UDFLib.ConvertToInteger(((ExcelNS.Range)ExlWrkSheet.Cells[6, 15]).Value2);
                        int QuotationIDrow = Convert.ToInt32(hdf_QuotationID.Value);
                        if (Quotation_ID == QuotationIDrow)
                        {

                            DataTable dtCharges = new DataTable();
                            dtCharges.Columns.Add("Currency");
                            dtCharges.Columns.Add("Truck_Charge");
                            dtCharges.Columns.Add("Barge_Charge");
                            dtCharges.Columns.Add("Freight_Charge");
                            dtCharges.Columns.Add("Pkg_Hld_Charge");
                            dtCharges.Columns.Add("Other_Charge");
                            dtCharges.Columns.Add("Vat");
                            dtCharges.Columns.Add("Discount");


                            DataRow drCharges = dtCharges.NewRow();
                            drCharges["Currency"] = UDFLib.ConvertIntegerToNull(((ExcelNS.Range)ExlWrkSheet.Cells[5, 3]).Value2);
                            drCharges["Truck_Charge"] = UDFLib.ConvertDecimalToNull(((ExcelNS.Range)ExlWrkSheet.Cells[9, 3]).Value2);
                            drCharges["Barge_Charge"] = UDFLib.ConvertDecimalToNull(((ExcelNS.Range)ExlWrkSheet.Cells[9, 9]).Value2);
                            drCharges["Freight_Charge"] = UDFLib.ConvertDecimalToNull(((ExcelNS.Range)ExlWrkSheet.Cells[5, 9]).Value2);
                            drCharges["Pkg_Hld_Charge"] = UDFLib.ConvertDecimalToNull(((ExcelNS.Range)ExlWrkSheet.Cells[7, 9]).Value2);
                            drCharges["Other_Charge"] = UDFLib.ConvertDecimalToNull(((ExcelNS.Range)ExlWrkSheet.Cells[8, 9]).Value2);
                            drCharges["Vat"] = UDFLib.ConvertDecimalToNull(((ExcelNS.Range)ExlWrkSheet.Cells[9, 7]).Value2);
                            drCharges["Discount"] = UDFLib.ConvertDecimalToNull(((ExcelNS.Range)ExlWrkSheet.Cells[8, 3]).Value2);

                            dtCharges.Rows.Add(drCharges);

                            DataTable dtItemPrice = new DataTable();
                            dtItemPrice.Columns.Add("pkid", typeof(int));
                            dtItemPrice.Columns.Add("rate", typeof(decimal));
                            dtItemPrice.Columns.Add("discount", typeof(decimal));
                            dtItemPrice.Columns.Add("unit", typeof(string));
                            dtItemPrice.Columns.Add("supp_remark");

                            DataRow drNew;
                            int i = 15;
                            while (((ExcelNS.Range)ExlWrkSheet.Cells[i, 1]).Value2 != null)
                            {
                                double value = 0;
                                if (double.TryParse(((ExcelNS.Range)ExlWrkSheet.Cells[i, 1]).Value2.ToString(), out value))
                                {
                                    drNew = dtItemPrice.NewRow();
                                    drNew["pkid"] = ((ExcelNS.Range)ExlWrkSheet.Cells[i, 4]).Value2.ToString();


                                    if (((ExcelNS.Range)ExlWrkSheet.Cells[i, 6]).Value2 != null)
                                    {
                                        drNew["unit"] = ((ExcelNS.Range)ExlWrkSheet.Cells[i, 6]).Value2.ToString();
                                    }
                                    else
                                    {
                                        drNew["unit"] = "";
                                    }

                                    if (((ExcelNS.Range)ExlWrkSheet.Cells[i, 8]).Value2 != null)
                                    {
                                        drNew["rate"] = ((ExcelNS.Range)ExlWrkSheet.Cells[i, 8]).Value2.ToString();
                                    }
                                    else
                                    {
                                        drNew["rate"] = "0";
                                    }
                                    if (((ExcelNS.Range)ExlWrkSheet.Cells[i, 9]).Value2 != null)
                                    {
                                        drNew["discount"] = ((ExcelNS.Range)ExlWrkSheet.Cells[i, 9]).Value2.ToString();
                                    }
                                    else
                                    {
                                        drNew["discount"] = "0";
                                    }

                                    if (((ExcelNS.Range)ExlWrkSheet.Cells[i, 12]).Value2 != null)
                                    {
                                        drNew["supp_remark"] = ((ExcelNS.Range)ExlWrkSheet.Cells[i, 12]).Value2.ToString();
                                    }
                                    else
                                    {
                                        drNew["supp_remark"] = "";
                                    }

                                    dtItemPrice.Rows.Add(drNew);

                                }
                                i = i + 3;
                            }
                            dtItemPrice.AcceptChanges();

                            if (dtItemPrice.Rows.Count > 0)
                            {
                                if (dtItemPrice.Select("rate > 0").Length > 0)
                                {

                                    BLL_PURC_CTP.Upd_Ctp_Items_Price(Quotation_ID, dtItemPrice, Convert.ToInt32(Session["userid"].ToString()), 1, dtCharges);
                                    BindDataItems();
                                }
                            }
                        }
                        else
                        {

                        }
                    }
                    catch { }
                    finally
                    {
                        ExlWrkBook.Close(null, null, null);
                        //ExlApp.Workbooks.Close();
                        ExlApp.Quit();
                        Marshal.ReleaseComObject(ExlApp);
                        Marshal.ReleaseComObject(ExlWrkSheet);
                        Marshal.ReleaseComObject(ExlWrkBook);
                    }

                }
                else
                {
                    String msg = String.Format("alert('The uploaded file do not belong to the selected supplier');");
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msginvalidSupplier", msg, true);
                }
            }

        }
        catch (Exception ex)
        {
            String msg = String.Format("alert('The uploaded file do not belong to the selected supplier');");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg, true);

        }

    }

    protected void btnCompare_Click(object sender, EventArgs e)
    {
        string strIDs = "";
        CheckBox chkComp = new CheckBox();
        string Catalogue = "";

        foreach (GridViewRow gr in gvContractList.Rows)
        {
            chkComp = (CheckBox)gr.FindControl("chkCompare");
            if (chkComp.Checked == true)
            {


                Catalogue = (Catalogue == "") ? chkComp.ToolTip.Split('_')[1] : Catalogue;

                if (Catalogue == chkComp.ToolTip.Split('_')[1])
                {
                    strIDs += chkComp.ToolTip.Split('_')[0] + "_";
                    Catalogue = chkComp.ToolTip.Split('_')[1];
                }
                else
                {
                    String msg = String.Format("alert('please select qtn from same dept');");

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgerroe", msg, true);
                    return;
                }
            }
        }

        if (strIDs.Length > 0)
        {
            ResponseHelper.Redirect("CTP_Quotation_Evaluation.aspx?quotation_ids=" + strIDs + "&Catalogue=" + Catalogue, "blank", "");
        }
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        int isfetch = 0;
       
        DataTable dtCtpList = BLL_PURC_CTP.Get_Ctp_Contract_List(UDFLib.ConvertStringToNull(uc_SupplierListCTP.SelectedValue),
                                                                      UDFLib.ConvertIntegerToNull(uc_Purc_DepartmentListctp.SelectedValue),
                                                                      UDFLib.ConvertDateToNull(txtEffdtFrom.Text),
                                                                      UDFLib.ConvertDateToNull(txtEffdtTo.Text),
                                                                      UDFLib.ConvertStringToNull(chkQtnStatus.SelectedValue),
                                                                      UDFLib.ConvertIntegerToNull(chkContractStatus.SelectedValue),
                                                                      UDFLib.ConvertIntegerToNull(ctlPortListctp.SelectedValue),
                                                                      UDFLib.ConvertStringToNull(txtItemSearch.Text),
                                                                      null,
                                                                      null,
                                                                      ref isfetch);

        string[] HeaderCaptions = new string[] { "Contract Code", "Effective Date", "Supplier Name", "Dept Name", "Catalogue Name", "Approved Item", "Not approved items", "Approved By", "Approved Date", "Status" };
        string[] DataColumnsName = new string[] { "QTN_Contract_Code", "Effective_Date", "Full_NAME", "Dept_Name", "System_Description", "APPROVED_ITEM_COUNT", "NOT_APPROVED_ITEM_COUNT", "First_Name", "Approved_Date", "QTN_STS" };
        string FileHeaderName = "Contract List";
        string FileName = "Contract_List";
        GridViewExportUtil.ShowExcel(dtCtpList, HeaderCaptions, DataColumnsName, FileName, FileHeaderName);


    }


    protected void imgDeleteCTP_Click(object s, EventArgs e)
    {
        string[] prm = (s as ImageButton).CommandArgument.Split(',');
        int sts = BLL_PURC_CTP.UPD_Ctp_Delete_Contract(UDFLib.ConvertToInteger(prm[0]), UDFLib.ConvertToInteger(prm[1]), Convert.ToInt32(Session["userid"]));
        if (sts > 0)
        {
            BindDataItems();
        }
    }
    
   
}

