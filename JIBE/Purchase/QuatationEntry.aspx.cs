using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using SMS.Business.PURC;
using Telerik.Web.UI;
using System.Text;
using System.Data.OleDb;
using Exel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop;
using System.IO;
using ClsBLLTechnical;
using System.Runtime.InteropServices;
using System.Diagnostics;


public partial class Technical_INV_QuatationEntry : System.Web.UI.Page
{
    ArrayList SheetList = new ArrayList();

    public static object Opt = Type.Missing;
    public static Microsoft.Office.Interop.Excel.Application ExlApp;
    public static Microsoft.Office.Interop.Excel.Workbook ExlWrkBook;
    public static Microsoft.Office.Interop.Excel.Worksheet ExlWrkSheet;
    //public string strPath = "";
    //public string FileName=""; 

    private decimal Exchange_rate = 1;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            BindRequisitionInfo();
            BindSupplierDDLForQuotation();

        }
    }

    private void BindRequisitionInfo()
    {

        try
        {
            DataTable dtReqInfo = new DataTable();
            using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
            {
                dtReqInfo = objTechService.SelectRequistionToSupplier(Request.QueryString["Requisitioncode"].ToString(), Request.QueryString["Document_Code"].ToString());
                lblReqNo.Text = dtReqInfo.DefaultView[0]["REQUISITION_CODE"].ToString();
                lblVessel.Text = dtReqInfo.DefaultView[0]["Vessel_Name"].ToString();
                lblCatalog.Text = dtReqInfo.DefaultView[0]["SYSTEM_Description"].ToString();
                lblToDate.Text = dtReqInfo.DefaultView[0]["requestion_Date"].ToString();
                lblTotalItem.Text = dtReqInfo.DefaultView[0]["TOTAL_ITEMS"].ToString();
                lblReqNo.NavigateUrl = "RequisitionSummary.aspx?REQUISITION_CODE=" + Request.QueryString["Requisitioncode"].ToString() + "&Document_Code=" + Request.QueryString["Document_Code"].ToString() + "&Vessel_Code=" + Request.QueryString["Vessel_Code"].ToString() + "&Dept_Code=" + dtReqInfo.DefaultView[0]["DEPARTMENT"].ToString() + "&" + 1.ToString();

            }

        }
        catch (Exception ex)
        {
            //.WriteError(this.GetType().Name.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), ex);
        }
        finally
        {

        }

    }

    protected void BindSupplierDDLForQuotation()
    {
        try
        {


            using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
            {
                DataTable dtSuppForQuatation = objTechService.GetSupplierForQuatation(Request.QueryString["Requisitioncode"].ToString(), Request.QueryString["Vessel_Code"].ToString(), Request.QueryString["Document_Code"].ToString());
                //dtSuppForQuatation.DefaultView.RowFilter = "REQUISITION_CODE ='" + Request.QueryString["Requisitioncode"].ToString() + "'";
                DDLSupplier.DataSource = dtSuppForQuatation.DefaultView;
                DDLSupplier.DataTextField = "SHORT_NAME";
                DDLSupplier.DataValueField = "QUOTATION_SUPPLIER";
                DDLSupplier.DataBind();
                ViewState["QuotationCode"] = dtSuppForQuatation.DefaultView[0][0].ToString();


            }
        }
        catch (Exception ex)
        {
            //.WriteError(this.GetType().Name.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), ex);

        }
        finally
        {
        }
    }
    Hashtable myHashtable;
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        int rowcnt = 0;
        System.Data.DataTable dtItems = new DataTable();
        rgdQuoUpload.DataSource = null;
        rgdQuoUpload.DataBind();

        ViewState["dtQuItem"] = null;
        try
        {
            CheckExcellProcesses();
            lblErrorMsg.Text = "";
            string _QuotationCode = DDLSupplier.SelectedValue.Split(new char[] { '~' })[1];
            if (FileUpload1.HasFile)
            {
                //FileUpload1.
                string strLocalPath = FileUpload1.PostedFile.FileName;

                string FileName = Path.GetFileName(strLocalPath);
                FileUpload1.PostedFile.SaveAs(Server.MapPath("TempUpload\\" + FileName));


                string strPath = Server.MapPath("TempUpload\\" + FileName).ToString();

                string filte = "Quotation_code='" + _QuotationCode + "' and Vessel_Code ='" + Request.QueryString["Vessel_Code"].ToString() + "' and Supplier_code='" + DDLSupplier.SelectedValue.Split(new char[] { '~' })[0] + "'";
                //   ViewState["strPath"] = FileUpload1.PostedFile.FileName;
                ViewState["strPath"] = strPath;


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
                //ExlWrkSheet.Cells.Insertr  

                //MessageBox.Show(ExlWrkSheet.Name);
                DataSet ds = new DataSet();
                System.Data.DataTable dt = new System.Data.DataTable();
                dt.Columns.Add("Quotation_code", typeof(string));
                dt.Columns.Add("Quotation_date", typeof(string));
                dt.Columns.Add("Requisition_No", typeof(string));
                dt.Columns.Add("Vessel_Code", typeof(string));
                dt.Columns.Add("System_code", typeof(string));
                dt.Columns.Add("Department", typeof(string));
                dt.Columns.Add("Quotated_Currency", typeof(string));
                dt.Columns.Add("supplier_code", typeof(string));
                dt.Columns.Add("Document_code", typeof(string));
                dt.Columns.Add("Exchange_Rate", typeof(string));
                dt.Columns.Add("Supplier_Remark", typeof(string));
                dt.Columns.Add("Discount", typeof(string));
                dt.Columns.Add("Vat", typeof(string));
                dt.Columns.Add("Trans_Freight_Cost", typeof(string));
                dt.Columns.Add("Pkg_handing_Charges", typeof(string));
                dt.Columns.Add("OtherCharge", typeof(string));
                dt.Columns.Add("TruckCharge", typeof(string));
                dt.Columns.Add("BargeCharge", typeof(string));
                dt.Columns.Add("ReasonForPkg", typeof(string));
                dt.Columns.Add("ReasonForOther", typeof(string));
                dt.AcceptChanges();
                DataRow dr = dt.NewRow();
                string quotecode=Convert.ToString(((Exel.Range)ExlWrkSheet.Cells[6, 13]).Value2);
                if (_QuotationCode == Convert.ToString(((Exel.Range)ExlWrkSheet.Cells[6, 13]).Value2))
                {
                    string tran_cost=Convert.ToString(((Exel.Range)ExlWrkSheet.Cells[5, 9]).Value2);
                    string pkg_cost = Convert.ToString(((Exel.Range)ExlWrkSheet.Cells[7, 9]).Value2);
                    string reason =  Convert.ToString(((Exel.Range)ExlWrkSheet.Cells[7, 12]).Value2);
                    if (tran_cost != string.Empty && pkg_cost != string.Empty && reason!=string.Empty)
                    {
                        dr["Quotation_code"] = Convert.ToString(((Exel.Range)ExlWrkSheet.Cells[6, 13]).Value2);
                        dr["Quotation_date"] = Convert.ToString(((Exel.Range)ExlWrkSheet.Cells[5, 13]).Value2);
                        dr["Requisition_No"] = Convert.ToString(((Exel.Range)ExlWrkSheet.Cells[2, 3]).Value2);
                        dr["Vessel_Code"] = Convert.ToString(((Exel.Range)ExlWrkSheet.Cells[1, 13]).Value2);
                        dr["System_code"] = Convert.ToString(((Exel.Range)ExlWrkSheet.Cells[3, 13]).Value2);
                        dr["Department"] = "";// ((Exel.Range)ExlWrkSheet.Cells[3, 13]).Value2.ToString();
                        dr["Quotated_Currency"] = Convert.ToString(((Exel.Range)ExlWrkSheet.Cells[5, 3]).Value2);
                        dr["supplier_code"] = Convert.ToString(((Exel.Range)ExlWrkSheet.Cells[7, 13]).Value2);
                        //dr["Exchange_Rate"] = ((Exel.Range)ExlWrkSheet.Cells[6, 3]).Value2.ToString();
                        dr["Document_code"] = Convert.ToString(((Exel.Range)ExlWrkSheet.Cells[2, 13]).Value2);

                        dr["Supplier_Remark"] = Convert.ToString(((Exel.Range)ExlWrkSheet.Cells[7, 3]).Value2);

                        // dr["Supplier_Remark"] = ((Exel.Range)ExlWrkSheet.Cells[10, 3]).Value2.ToString();
                        dr["Discount"] = UDFLib.ConvertToDecimal(((Exel.Range)ExlWrkSheet.Cells[8, 3]).Value2);
                        dr["Vat"] = UDFLib.ConvertToDecimal(((Exel.Range)ExlWrkSheet.Cells[9, 7]).Value2);
                        dr["Trans_Freight_Cost"] = UDFLib.ConvertToDecimal(((Exel.Range)ExlWrkSheet.Cells[5, 9]).Value2);
                        dr["Pkg_handing_Charges"] = UDFLib.ConvertToDecimal(((Exel.Range)ExlWrkSheet.Cells[7, 9]).Value2);

                        dr["OtherCharge"] = UDFLib.ConvertToDecimal(((Exel.Range)ExlWrkSheet.Cells[8, 9]).Value2);

                        dr["ReasonForOther"] = Convert.ToString(((Exel.Range)ExlWrkSheet.Cells[8, 12]).Value2);

                        dr["ReasonForPkg"] = ((Exel.Range)ExlWrkSheet.Cells[7, 12]).Value2 == null ? "" : ((Exel.Range)ExlWrkSheet.Cells[7, 12]).Value2.ToString();

                        dr["TruckCharge"] = UDFLib.ConvertToDecimal(((Exel.Range)ExlWrkSheet.Cells[9, 3]).Value2);
                        dr["BargeCharge"] = UDFLib.ConvertToDecimal(((Exel.Range)ExlWrkSheet.Cells[9, 9]).Value2);

                        dt.Rows.Add(dr);
                        dt.AcceptChanges();

                        dt.DefaultView.RowFilter = filte;
                        if (dt.DefaultView.Count > 0)
                        {

                            dtItems = CheckPrice();
                            
                            var querye = from res in dtItems.AsEnumerable()
                                         where res.Field<string>("Unit_Price") == ""
                                         select res;
                            int iCount = querye.Count();

                            if (dtItems.Rows.Count != iCount)
                            {
                                txtCurrency.Text = dt.Rows[0]["Quotated_Currency"].ToString();
                                txtRequi.Text = dt.Rows[0]["Supplier_Remark"].ToString();
                                txtDiscount.Text = dt.Rows[0]["Discount"].ToString();
                                txtVat.Text = dt.Rows[0]["Vat"].ToString();
                                txtTrsportCost.Text = dt.Rows[0]["Trans_Freight_Cost"].ToString();
                                txtPkgCharges.Text = dt.Rows[0]["Pkg_handing_Charges"].ToString();
                                txtAdditionlachrgs.Text = dt.Rows[0]["OtherCharge"].ToString();
                                txtReasonPKG.Text = dt.Rows[0]["ReasonForPkg"].ToString();
                                txtBarge.Text = dt.Rows[0]["BargeCharge"].ToString();
                                txtReasonOther.Text = dt.Rows[0]["ReasonForOther"].ToString();
                                txtTruck.Text = dt.Rows[0]["TruckCharge"].ToString();
                                Bindgrid(dtItems);
                                btnSave.Enabled = true;

                            }
                            else
                            {
                                String msg1 = String.Format("alert('Enter atleast one item Unit Price.');");
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg1", msg1, true);
                                lblErrorMsg.Text = "Enter atleast one item Unit Price.";
                                btnSave.Enabled = false;
                               
                            }
                        }
                        else
                        {
                            String msg = String.Format("alert('The uploaded excel file do not belong to the selected supplier');");
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg, true);
                            lblErrorMsg.Text = "The uploaded Excel file do not belong to the selected supplier";
                            btnSave.Enabled = false;
                            //KillExcel();
                        }

                    }
                    else
                    {
                        if (Convert.ToString(((Exel.Range)ExlWrkSheet.Cells[5, 9]).Value2) == string.Empty)
                        {
                            String msg = String.Format("alert('Transportation/Freight Cost can not be blank');");
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg, true);
                            lblErrorMsg.Text = "Transportation/Freight Cost can not be blank";
                            btnSave.Enabled = false;

                        }
                        else if(Convert.ToString(((Exel.Range)ExlWrkSheet.Cells[7, 9]).Value2) == string.Empty)
                        {
                            String msg = String.Format("alert('Pkg and handling charges can not be blank.');");
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg, true);
                            lblErrorMsg.Text = "Pkg and handling charges can not be blank.";
                            btnSave.Enabled = false;

                        }
                        else if (Convert.ToString(((Exel.Range)ExlWrkSheet.Cells[7, 12]).Value2) == string.Empty)
                        {
                            String msg = String.Format("alert('Reason for Pkg and Handling Charges.');");
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg, true);
                            lblErrorMsg.Text = "Reason for Pkg and Handling Charges.";
                            btnSave.Enabled = false;
                        }
                    }
                }
                else
                {
                    String msg = String.Format("alert('The uploaded excel file do not belong to the selected supplier');");
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg, true);
                    lblErrorMsg.Text = "The uploaded Excel file do not belong to the selected supplier";
                    btnSave.Enabled = false;
                }

            }
            else
            {
                lblErrorMsg.Text = "There is no file to upload.";
                //KillExcel();
            }

        }
        catch (Exception ex)
        {
            String msg = String.Format("alert('The uploaded file do not belong to the selected supplier');");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg, true);

            lblErrorMsg.Text = ex.ToString();
            //KillExcel();
        }
        finally
        {
            //ExlWrkBook.Close(null, null, null);
            //ExlApp.Workbooks.Close();
            //ExlApp.Quit();
            KillExcel();
            //Marshal.ReleaseComObject(ExlApp);
            //Marshal.ReleaseComObject(ExlWrkSheet);
            //Marshal.ReleaseComObject(ExlWrkBook);
        }
    }

    private void Bindgrid(DataTable dtItems)
    {

        try
        {

            if (dtItems.DefaultView.Count > 0)
            {
                rgdQuoUpload.DataSource = dtItems;
                rgdQuoUpload.DataBind();

                ViewState["dtQuItem"] = dtItems;
            }
            else
            {
                lblErrorMsg.Text = "Please Check Your Excel file";
                btnSave.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            //.WriteError(this.GetType().Name.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), ex);
        }
        finally
        {
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



    protected void rgdQuatationEntry_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            foreach (TableCell cell in e.Item.Cells)
            {
                cell.ToolTip = cell.Text != "&nbsp;" ? cell.Text : null;
            }
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["strPath"] != null)
            {
                lblErrorMsg.Text = "";
                CheckExcellProcesses();
                string _QuotationCode = DDLSupplier.SelectedValue.Split(new char[] { '~' })[1];

                StringBuilder strQuory = new StringBuilder();
                strQuory.Append("begin try begin tran DECLARE @ifExist int = 0 ,@QuotationCode varchar(30) ='' ,@ItemType int =0 ,@ItemRefCode varchar(30)='0' ");

                Exchange_rate = 1;

                string strPath = Path.GetDirectoryName((String)ViewState["strPath"].ToString());
                string FileName = Path.GetFileName((String)ViewState["strPath"].ToString());
                string strPath1 = Server.MapPath("TempUpload\\" + FileName).ToString();
                ExlApp = new Microsoft.Office.Interop.Excel.Application();
                ExlWrkBook = ExlApp.Workbooks.Open(strPath1,
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
                if (rgdQuoUpload.MasterTableView.Items.Count != 0)
                {

                    foreach (GridDataItem dataItem in rgdQuoUpload.MasterTableView.Items)
                    {

                        strQuory.Append(" Update dbo.PURC_Dtl_Quoted_Prices Set ");
                        strQuory.Append("QUOTED_RATE='");
                        //strQuory.Append(dr["QUOTED_RATE"].ToString());
                        if (dataItem["Unit_Price"].Text == "&nbsp;")
                        {
                            strQuory.Append("0");
                        }
                        else
                        {
                            strQuory.Append((Convert.ToDecimal(dataItem["Unit_Price"].Text)).ToString());
                        }
                        strQuory.Append("', QUOTED_DISCOUNT='");
                        if (dataItem["Discount"].Text == "")
                        {
                            strQuory.Append("0");
                        }
                        else
                        {
                            strQuory.Append(dataItem["Discount"].Text);
                        }


                        strQuory.Append("', SURCHARGES='");
                        //if (txtSurcharge.Text.ToString() == "")
                        //{
                        strQuory.Append("0");
                        //}
                        //else
                        //{
                        //    strQuory.Append(txtSurcharge.Text.ToString());
                        //}
                        //strQuory.Append(dr["SURCHARGES%"].ToString());
                        strQuory.Append("', Vat='");
                        if (txtVat.Text.ToString() == "")
                        {
                            strQuory.Append("0");
                        }
                        else
                        {
                            strQuory.Append(txtVat.Text.ToString());
                        }
                        strQuory.Append("', QUOTED_Price='");
                        strQuory.Append(dataItem["Total_Price"].Text);
                        strQuory.Append("',QUOTED_CURRENCY='");
                        strQuory.Append(txtCurrency.Text);
                        strQuory.Append("',QUOTATION_REMARKS='");
                        if (dataItem["Supplier_Remarks"].Text == "" || dataItem["Supplier_Remarks"].Text == "&nbsp;")
                        {
                            strQuory.Append("");
                        }
                        else
                        {
                            strQuory.Append(dataItem["Supplier_Remarks"].Text.Trim().Replace("'", ""));
                        }
                        strQuory.Append("',Lead_Time='");
                        strQuory.Append(dataItem["LeadTime"].Text);
                        //strQuory.Append(dataItem["column"].Text);

                        strQuory.Append("',Item_Type=154");
                        //if (dataItem["Item_Type"].Text == "Original")
                        //{
                        //    strQuory.Append("ORG");
                        //}
                        //else if (dataItem["Item_Type"].Text == "Imitation")
                        //{
                        //    strQuory.Append("IMT");
                        //}
                        //else
                        //{
                        //    strQuory.Append(dataItem["Item_Type"].Text);
                        //}
                        strQuory.Append(",Additional_Charges='");
                        strQuory.Append(txtAdditionlachrgs.Text);
                        //  strQuory.Append(txtCurrency.Text);
                        strQuory.Append("',Quotation_Status='R',SYNC_FLAG='0',Date_Of_Modified=getdate() where [QUOTATION_CODE]='");
                        strQuory.Append(_QuotationCode);
                        strQuory.Append("' and  [SUPPLIER_CODE]='");
                        strQuory.Append(DDLSupplier.SelectedValue.Split(new char[] { '~' })[0]);
                        strQuory.Append("' and [ITEM_REF_CODE]='");
                        strQuory.Append(dataItem["ITEM_REF_CODE"].Text);
                        strQuory.Append("' and DOCUMENT_CODE='");
                        strQuory.Append(Request.QueryString["Document_Code"].ToString());
                        strQuory.Append("' and Vessel_Code='");
                        strQuory.Append(Request.QueryString["Vessel_Code"].ToString());
                        strQuory.Append("' ");

                        // insert into PURC_DTL_QuotedPrices_ItemType

                        strQuory.Append(@" 
                                         
                                          SELECT  @QuotationCode='" + _QuotationCode + @"' , @ItemType =154  ,@ItemRefCode = '" + dataItem["ITEM_REF_CODE"].Text + @"'            
                                          SET @ifExist=(SELECT COUNT(0) from PURC_DTL_QuotedPrices_ItemType where Quotation_Code=@QuotationCode and Item_Ref_Code=@ItemRefCode  and Item_Type=@ItemType)
                                        IF(isnull(@ifExist,0)!=0)
	                                        BEGIN
	
		                                        UPDATE PURC_DTL_QuotedPrices_ItemType set Quoted_Rate= isnull(" + UDFLib.ConvertToDecimal(dataItem["Unit_Price"].Text.Trim()) + @",0)
		                                        WHERE Quotation_Code=@QuotationCode and Item_Ref_Code=@ItemRefCode  and Item_Type=@ItemType
	                                        END
                                        ELSE 
	                                        BEGIN
		                                        IF(isnull(" + UDFLib.ConvertToDecimal(dataItem["Unit_Price"].Text.Trim()) + @",0) > 0)
			                                        BEGIN
				                                        INSERT INTO PURC_DTL_QuotedPrices_ItemType(ID,Quotation_Code,Item_Ref_Code,Item_Type,Quoted_Rate,Date_Of_Creation)
				                                        SELECT isnull(MAX(id),0)+1,@QuotationCode,@ItemRefCode,@ItemType,isnull(" + (dataItem["Unit_Price"].Text.Trim() == "&nbsp;" ? "0" : dataItem["Unit_Price"].Text.Trim()) + @",0),GETDATE() FROM PURC_DTL_QuotedPrices_ItemType
			                                        END
	                                        END  
                                            	
                                        ");

                    }

                    strQuory.Append("update dbo.PURC_Dtl_REQSN set Currency='");
                    strQuory.Append(txtCurrency.Text);
                    strQuory.Append("',PREVIOUS_EXCHANGE_RATE=1");//the actual value will be saved on po approval 
                    if (((Exel.Range)ExlWrkSheet.Cells[7, 3]).Value2.ToString().Trim().Replace("'", "").Length > 250)
                    {
                        lblErrorMsg.Text = "Length of Supplier Quotation Reference should be less than 250 .";
                        strQuory.Clear();
                        return;
                    }
                    else
                    {
                        strQuory.Append(",Supplier_Quotation_Reference='");
                        strQuory.Append(((Exel.Range)ExlWrkSheet.Cells[7, 3]).Value2.ToString().Trim().Replace("'", ""));
                    }

                    strQuory.Append("',Freight_Cost='");
                    strQuory.Append(UDFLib.ConvertToDecimal(((Exel.Range)ExlWrkSheet.Cells[5, 9]).Value2).ToString());
                    strQuory.Append("',Packing_Handling_Charges='");
                    strQuory.Append(UDFLib.ConvertToDecimal(((Exel.Range)ExlWrkSheet.Cells[7, 9]).Value2).ToString());
                    strQuory.Append("',REBATE='");
                    strQuory.Append(UDFLib.ConvertToDecimal(((Exel.Range)ExlWrkSheet.Cells[9, 9]).Value2).ToString());


                    strQuory.Append("',Truck_Cost='");
                    strQuory.Append(txtTruck.Text != "" ? UDFLib.ConvertToDecimal(txtTruck.Text).ToString() : "0");
                    strQuory.Append("',Barge_Workboat_Cost='");
                    strQuory.Append(txtBarge.Text != "" ? UDFLib.ConvertToDecimal(txtBarge.Text).ToString() : "0");
                    strQuory.Append("',Other_Charges='");
                    strQuory.Append(txtAdditionlachrgs.Text != "" ? UDFLib.ConvertToDecimal(txtAdditionlachrgs.Text).ToString() : "0");
                    strQuory.Append("',REASON_TRANS_PKG='");
                    strQuory.Append(txtReasonPKG.Text.Trim().Replace("'", ""));
                    strQuory.Append("',Other_Charges_Reason='");
                    strQuory.Append(txtReasonOther.Text.Trim().Replace("'", ""));
                    strQuory.Append("',QUOTATION_COMMENTS='");
                    strQuory.Append(txtRequi.Text.Trim().Replace("'", ""));
                    strQuory.Append("',DISCOUNT='");
                    strQuory.Append(txtDiscount.Text != "" ? txtDiscount.Text : "0");
                    strQuory.Append("' , Quotation_Status='F' ,Quotation_Status_Date=getdate() where REQUISITION_CODE='");
                    strQuory.Append(Request.QueryString["Requisitioncode"].ToString());
                    strQuory.Append("' and QUOTATION_CODE='");
                    strQuory.Append(_QuotationCode);
                    strQuory.Append("' and QUOTATION_SUPPLIER='");
                    strQuory.Append(DDLSupplier.SelectedValue.Split(new char[] { '~' })[0]);
                    strQuory.Append("' and DOCUMENT_CODE='");
                    strQuory.Append(Request.QueryString["Document_Code"].ToString());
                    strQuory.Append("' and Vessel_Code='");
                    strQuory.Append(Request.QueryString["Vessel_Code"].ToString());
                    strQuory.Append("' ");
                    strQuory.Append("commit tran end try begin catch  DECLARE @ErrorMessage NVARCHAR(4000), @ErrorSeverity INT , @ErrorState INT;SELECT  @ErrorMessage = ERROR_MESSAGE(),  @ErrorSeverity = ERROR_SEVERITY(),  @ErrorState = ERROR_STATE();  RAISERROR (@ErrorMessage,@ErrorSeverity, @ErrorState ); rollback tran end catch");

                    using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
                    {
                        if (strQuory.Length > 10)
                        {
                            KillExcel();
                            TechnicalBAL objBAL = new TechnicalBAL();
                            string FinalQuery = strQuory.ToString();
                            int valRet = objBAL.ExecuteQuery(FinalQuery);


                            //int valRet = objTechService.ExequetString(FinalQuery);


                            //string strDestiFileName =Server.MapPath("UploddQuot") + @"\";

                            //Copy the uploaded file into the server.

                            if (System.IO.File.Exists(Server.MapPath("UploadQuot\\" + FileName)) == false)
                            {
                                //FileStream fs = File.Move(Server.MapPath("TempUpload\\" + FileName), Server.MapPath("UploadQuot\\" + FileName));
                                File.Move(Server.MapPath("TempUpload\\" + FileName), Server.MapPath("UploadQuot\\" + FileName));
                                //fs.Close();
                            }

                            string strQuoUpdPath = Server.MapPath("UploadQuot\\").ToString();

                            objTechService.SaveAttachedFileInfo(Request.QueryString["Vessel_Code"].ToString(), Request.QueryString["Requisitioncode"].ToString(), DDLSupplier.SelectedValue.Split(new char[] { '~' })[0], "QUpld", FileName, "UploadQuot/" + FileName, Session["userid"].ToString(), 0);

                            lblErrorMsg.Text = "Quotation has been uploaded sucessfully";

                            String script = String.Format("alert('Quotation has been uploaded sucessfully.');window.close();");
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", script, true);
                        }
                    }





                }
                else
                {
                    lblErrorMsg.Text = "There is no data to upload, Please check the uploaded file.";
                }
            }
            else
            {
                lblErrorMsg.Text = "There is no data to upload, Please check the uploaded file.";
            }
        }
        catch (Exception ex)
        {
            lblErrorMsg.Text = ex.Message;
        }

        finally
        {
            RefreshGrid();

        }

    }

    private void RefreshGrid()
    {
        if (ViewState["dtQuItem"] != null)
        {
            DataTable dt = (DataTable)ViewState["dtQuItem"];
            dt.Rows.Clear();
            rgdQuoUpload.DataSource = dt;
            rgdQuoUpload.DataBind();

            txtCurrency.Text = "";
            txtDiscount.Text = "";

            txtRequi.Text = "";
            //  txtSurcharge.Text = "";

            txtVat.Text = "";
            KillExcel();
        }

    }
    #region Check for Blank Unit Price 
    /// <summary>
    /// THIS WILL VALIDATE PRICE COLOUMN FOR EXCEL 
    /// IT WILL RETURN EXCEL WORKLIST AS DATATABLE.
    /// </summary>
    /// <returns></returns>
    private DataTable CheckPrice()
    {
        DataTable dtItems = new DataTable();
        try
        {
            string strLocalPath = FileUpload1.PostedFile.FileName;
            
            string FileName = Path.GetFileName(strLocalPath);
            FileUpload1.PostedFile.SaveAs(Server.MapPath("TempUpload\\" + FileName));


            string strPath = Server.MapPath("TempUpload\\" + FileName).ToString();

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

            dtItems.Columns.Add("Item_ref_code", typeof(string));
            dtItems.Columns.Add("Short_desc", typeof(string));
            dtItems.Columns.Add("Long_desc", typeof(string));
            dtItems.Columns.Add("Item_comments", typeof(string));
            dtItems.Columns.Add("Unit", typeof(string));
            dtItems.Columns.Add("Request_Qty", typeof(decimal));
            dtItems.Columns.Add("Unit_Price", typeof(string));
            dtItems.Columns.Add("Discount", typeof(decimal));
            dtItems.Columns.Add("Total_Price", typeof(decimal));
            dtItems.Columns.Add("Supplier_Remarks", typeof(string));
            dtItems.Columns.Add("LeadTime", typeof(string));
            dtItems.Columns.Add("Item_Type", typeof(string));
            dtItems.AcceptChanges();
            int i = 15;

            while (((Exel.Range)ExlWrkSheet.Cells[i, 1]).Value2 != null)
            {
                double value = 0;
                if (double.TryParse(((Exel.Range)ExlWrkSheet.Cells[i, 1]).Value2.ToString(), out value))
                {

                    DataRow drNew = dtItems.NewRow();
                    drNew["Item_ref_code"] = ((Exel.Range)ExlWrkSheet.Cells[i, 4]).Value2.ToString();

                    drNew["Short_desc"] = Convert.ToString(((Exel.Range)ExlWrkSheet.Cells[i, 5]).Value2);


                    drNew["Long_desc"] = Convert.ToString(((Exel.Range)ExlWrkSheet.Cells[i + 1, 5]).Value2);


                    drNew["Item_comments"] = Convert.ToString(((Exel.Range)ExlWrkSheet.Cells[i + 2, 5]).Value2).Trim().Replace("'", "");


                    drNew["Unit"] = Convert.ToString(((Exel.Range)ExlWrkSheet.Cells[i, 6]).Value2);


                    drNew["Request_Qty"] = UDFLib.ConvertToDecimal(((Exel.Range)ExlWrkSheet.Cells[i, 7]).Value2);


                    drNew["Unit_Price"] = Convert.ToString(((Exel.Range)ExlWrkSheet.Cells[i, 8]).Value2);//UDFLib.ConvertToDecimal(((Exel.Range)ExlWrkSheet.Cells[i, 8]).Value2);


                    drNew["Discount"] = UDFLib.ConvertToDecimal(((Exel.Range)ExlWrkSheet.Cells[i, 9]).Value2);


                    drNew["Total_Price"] = UDFLib.ConvertToDecimal(((Exel.Range)ExlWrkSheet.Cells[i, 11]).Value2);


                    drNew["Supplier_Remarks"] = Convert.ToString(((Exel.Range)ExlWrkSheet.Cells[i, 12]).Value2);


                    drNew["LeadTime"] = UDFLib.ConvertToDecimal(((Exel.Range)ExlWrkSheet.Cells[i, 10]).Value2);


                    dtItems.Rows.Add(drNew);
                 
                }
                i = i + 3;
            }
        }
        catch(Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
        return dtItems;
    }

    #endregion
}
