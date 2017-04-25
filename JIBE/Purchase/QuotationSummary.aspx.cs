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
using System.Data.SqlClient;
using System.Text;
using ClsBLLTechnical;
using EO.Pdf;
using EO.Pdf.Acm;
using System.Drawing;
using BLLQuotation;


public partial class Technical_INV_QuotationSummary : System.Web.UI.Page
{
    protected void Page_Init(object source, System.EventArgs e)
    {
        ViewState["QuotationItemsDetails"] = false;
        ViewState["QuotationItemsDetailsDisp"] = false;
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            BindQuotationSummary();
        }

    }

    /// <summary>
    /// This Method is used to clear text value
    /// </summary>
    /// <param name="Quotation_Code"></param>
    public void cleartext()
    {
        try
        {

                lblBargeWorkboatCost.Text = "";
                lblTruckCost.Text = "";
                lblOtherCost.Text = "";
                lblROC.Text = "";
                lblTruckingFreightCost.Text = "";
                lblPKGHandlingCost.Text = "";
                lblTotalDiscount.Text = "";
                lblSQR.Text = "";
                lblSRemark.Text = "";
                lblReasonPHC.Text = "";
                lblVATGST.Text = "";
                lblTotalPrice.Text = "";
                lblDiscountPrice.Text = "";
                lblDiscountTotalPrice.Text = "";
                lblGrandTotal.Text = "";


        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }


    }
    /// <summary>
    /// This Method is used to bind additional details RFQ RECIEVE
    /// </summary>
    /// <param name="Quotation_Code"></param>
    public void BindAdditionalDetails(string Quotation_Code)
    {
        try
        {
            TechnicalBAL objtechBAL = new TechnicalBAL();
            clsQuotationBLL objQuoBLL = new clsQuotationBLL();

            DataSet dsReqSumm = new DataSet();
            dsReqSumm = objtechBAL.GetRequQuotationSummary(Request.QueryString["REQUISITION_CODE"].ToString(), Request.QueryString["document_code"].ToString(), Request.QueryString["Vessel_Code"].ToString(), Quotation_Code.ToString());


            if (dsReqSumm.Tables[5].Rows.Count > 0)
            {
                lblMaker.Text = Convert.ToString(dsReqSumm.Tables[5].Rows[0]["Maker"]);
                lblSystemParticulars.Text = Convert.ToString(dsReqSumm.Tables[5].Rows[0]["Particulars"]);
                lblModel.Text = Convert.ToString(dsReqSumm.Tables[5].Rows[0]["Model"]);
                lblSerialNumber.Text = Convert.ToString(dsReqSumm.Tables[5].Rows[0]["System_Serial_Number"]);
                lblBargeWorkboatCost.Text = Convert.ToString(dsReqSumm.Tables[5].Rows[0]["Barge_Workboat_Cost"]);
                lblTruckCost.Text = Convert.ToString(dsReqSumm.Tables[5].Rows[0]["Truck_Cost"]);
                lblOtherCost.Text = Convert.ToString(dsReqSumm.Tables[5].Rows[0]["Other_Charges"]);
                lblROC.Text = Convert.ToString(dsReqSumm.Tables[5].Rows[0]["Other_Charges_Reason"]);
                lblTruckingFreightCost.Text = Convert.ToString(dsReqSumm.Tables[5].Rows[0]["Freight_Cost"]);
                lblPKGHandlingCost.Text = Convert.ToString(dsReqSumm.Tables[5].Rows[0]["Packing_Handling_Charges"]);

                lblTotalDiscount.Text = Convert.ToString(dsReqSumm.Tables[5].Rows[0]["DISCOUNT"]);
                lblSQR.Text = Convert.ToString(dsReqSumm.Tables[5].Rows[0]["Supplier_Quotation_Reference"]);
                lblSRemark.Text = Convert.ToString(dsReqSumm.Tables[5].Rows[0]["QUOTATION_REMARKS"]);
                lblReasonPHC.Text = Convert.ToString(dsReqSumm.Tables[5].Rows[0]["REASON_TRANS_PKG"]);
                lblAccountCode.Text = Convert.ToString(dsReqSumm.Tables[5].Rows[0]["ACCOUNT_CODE"]);
                

            }
            if (dsReqSumm.Tables[6].Rows.Count > 0)
            {

                lblVATGST.Text = Convert.ToString(dsReqSumm.Tables[6].Rows[0]["Vat"]);

            }
            if (dsReqSumm.Tables[7].Rows.Count > 0)
            {


                lblTotalPrice.Text = Math.Round(Convert.ToDecimal(dsReqSumm.Tables[7].Rows[0]["TotalPrice"]), 2).ToString();

               


            }

            CalculateDiscount();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }


    }
    /// <summary>
    /// This method is used to Calculate Discount Price and Total Price.
    /// </summary>
    public void CalculateDiscount()
    {
        try
        {
           
            string DiscountTotalPrice = ((UDFLib.ConvertToDecimal(lblTotalPrice.Text)) * (UDFLib.ConvertToDecimal(lblTotalDiscount.Text)) / 100).ToString();
            lblDiscountTotalPrice.Text = Math.Round(Convert.ToDecimal(DiscountTotalPrice), 2).ToString();

            string vat = ((UDFLib.ConvertToDecimal(lblTotalPrice.Text)) * (UDFLib.ConvertToDecimal(lblVATGST.Text)) / 100).ToString();
            lblVATGST.Text = Math.Round(Convert.ToDecimal(vat), 2).ToString();

           
            string TotalPricewithVATAmountandDiscount = ((UDFLib.ConvertToDecimal(lblTotalPrice.Text)) - (UDFLib.ConvertToDecimal(lblDiscountTotalPrice.Text)) + (UDFLib.ConvertToDecimal(lblVATGST.Text))).ToString();

            string GrandTotal = ((UDFLib.ConvertToDecimal(TotalPricewithVATAmountandDiscount)) + (UDFLib.ConvertToDecimal(lblTruckingFreightCost.Text)) + (UDFLib.ConvertToDecimal(lblPKGHandlingCost.Text)) + (UDFLib.ConvertToDecimal(lblTruckCost.Text)) + (UDFLib.ConvertToDecimal(lblBargeWorkboatCost.Text)) + (UDFLib.ConvertToDecimal(lblOtherCost.Text))).ToString();
            lblGrandTotal.Text = Math.Round(Convert.ToDecimal(GrandTotal), 2).ToString();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }

    }
    /// <summary>
    /// Used to bind repeter of RFQ Sent,Quotation Recieve and Attachment
    /// </summary>

    public void BindQuotationSummary()
    {
        try
        {
            TechnicalBAL objtechBAL = new TechnicalBAL();
            DataSet dsReqSumm = new DataSet();
            dsReqSumm = objtechBAL.GetRequQuotationSummary(Request.QueryString["REQUISITION_CODE"].ToString(), Request.QueryString["document_code"].ToString(), Request.QueryString["Vessel_Code"].ToString(), Request.QueryString["QUOTATION_CODE"].ToString());
            if (dsReqSumm.Tables[0].Rows.Count > 0)
            {
                lblCatalog.Text = Convert.ToString(dsReqSumm.Tables[0].Rows[0]["Catalog"]);
                lblReqNo.Text = Convert.ToString(dsReqSumm.Tables[0].Rows[0]["RequistionCode"]);
                lblTotalItem.Text = Convert.ToString(dsReqSumm.Tables[0].Rows[0]["TotalItems"]);
                lblToDate.Text = Convert.ToString(dsReqSumm.Tables[0].Rows[0]["ToDate"]);
                lblVessel.Text = Convert.ToString(dsReqSumm.Tables[0].Rows[0]["VesselName"]);
                txtComments.Text = Convert.ToString(dsReqSumm.Tables[0].Rows[0]["ReqComents"]);

                //--File Attachements.
                rpAttachment.DataSource = dsReqSumm.Tables[1];
                rpAttachment.DataBind();

                //----Quotation Sent


                RepeaterRfqSent.DataSource = dsReqSumm.Tables[3];
                RepeaterRfqSent.DataBind();

                //----Quotation Received


                RepeaterQtnRcv.DataSource = dsReqSumm.Tables[2];
                RepeaterQtnRcv.DataBind();


                foreach (RepeaterItem item in RepeaterQtnRcv.Items)
                {

                    if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                    {
                        System.Web.UI.HtmlControls.HtmlTableCell tdOrderLabel = (System.Web.UI.HtmlControls.HtmlTableCell)item.FindControl("tdOrderLabel");

                        tdOrderLabel.Visible = false;
                    }
                }



                ViewState["QuotationItemsDetails"] = true;
                ViewState["QuotationItemsDetails"] = dsReqSumm.Tables[4];
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }


    protected void rpAttachment_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            ResponseHelper.Redirect("SummaryReportsShow.aspx?REQUISITION_CODE=" + Request.QueryString["REQUISITION_CODE"].ToString() + "&document_code=" + Request.QueryString["document_code"].ToString() + "&Vessel_Code=" + Request.QueryString["Vessel_Code"].ToString() + "&RptType=QtnSumry" + "&QUOTATION_CODE=" + Request.QueryString["QUOTATION_CODE"].ToString(), "Blank", "");
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    /// <summary>
    /// Command is used to view Sent RFQ
    /// </summary>
    /// <param name="source"></param>
    /// <param name="e"></param>
    protected void onSelectRFQSent(object source, CommandEventArgs e)
    {
        try
        {
            string[] strArgs = e.CommandArgument.ToString().Split('~');
            string Quotation_Code = strArgs[1];
            lblDisplayType.Text = "Item Sent To :";
            lblSupplier.Text = strArgs[2];

            DataTable itemsDetails = new DataTable();

            itemsDetails = (DataTable)(ViewState["QuotationItemsDetails"]);
            itemsDetails.DefaultView.RowFilter = "QUOTATION_CODE ='" + Quotation_Code + "'";

            ViewState["QuotationItemsDetailsDisp"] = true;
            ViewState["QuotationItemsDetailsDisp"] = itemsDetails.DefaultView.ToTable();

            tblItemsDetail.Visible = true;
            RepeaterItemDetails.Visible = true;
            RepeaterSupItemsRcv.Visible = false;


            BuildGrid();
            int count = UDFLib.ConvertToInteger(lblCount.Text);
            tblQuotationDetails.Visible = true;
            divSystemParticulars.Visible = true;
            if (count > 0)
            {
                pnlGrandTotal.Visible = false;
                BindAdditionalDetails(Quotation_Code);
                cleartext();

                
            }
            else
            {
                pnlGrandTotal.Visible = false;
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }


    }


    /// <summary>
    /// Method is used to display Items Details
    /// </summary>
    private void BuildGrid()
    {
        
        DataTable QuotationItemsDetailsDisp = ((DataTable)(ViewState["QuotationItemsDetailsDisp"])).Clone();


        int count = ((DataTable)(ViewState["QuotationItemsDetailsDisp"])).Rows.Count;
        lblCount.Text = count.ToString();


        try
        {

             for (int i = 0; i <  ((DataTable)(ViewState["QuotationItemsDetailsDisp"])).Rows.Count; i++)
            {
            
                QuotationItemsDetailsDisp.Rows.Add();
                QuotationItemsDetailsDisp.Rows[QuotationItemsDetailsDisp.Rows.Count - 1]["Drawing_Number"] = ((DataTable)(ViewState["QuotationItemsDetailsDisp"])).Rows[i]["Drawing_Number"];
                QuotationItemsDetailsDisp.Rows[QuotationItemsDetailsDisp.Rows.Count - 1]["Part_Number"] = ((DataTable)(ViewState["QuotationItemsDetailsDisp"])).Rows[i]["Part_Number"];
                QuotationItemsDetailsDisp.Rows[QuotationItemsDetailsDisp.Rows.Count - 1]["ITEM_SERIAL_NO"] = ((DataTable)(ViewState["QuotationItemsDetailsDisp"])).Rows[i]["ITEM_SERIAL_NO"];
                QuotationItemsDetailsDisp.Rows[QuotationItemsDetailsDisp.Rows.Count - 1]["ITEM_SHORT_DESC"] = ((DataTable)(ViewState["QuotationItemsDetailsDisp"])).Rows[i]["ITEM_SHORT_DESC"];
                QuotationItemsDetailsDisp.Rows[QuotationItemsDetailsDisp.Rows.Count - 1]["QUOTED_QTY"] = ((DataTable)(ViewState["QuotationItemsDetailsDisp"])).Rows[i]["QUOTED_QTY"];
                QuotationItemsDetailsDisp.Rows[QuotationItemsDetailsDisp.Rows.Count - 1]["Unit_and_Packings"] = ((DataTable)(ViewState["QuotationItemsDetailsDisp"])).Rows[i]["Unit_and_Packings"];
                QuotationItemsDetailsDisp.Rows[QuotationItemsDetailsDisp.Rows.Count - 1]["ITEM_COMMENT"] = ((DataTable)(ViewState["QuotationItemsDetailsDisp"])).Rows[i]["ITEM_COMMENT"];
            }
        }
        catch
        {
            QuotationItemsDetailsDisp.Rows.RemoveAt(QuotationItemsDetailsDisp.Rows.Count - 1);
        }

        RepeaterItemDetails.DataSource = QuotationItemsDetailsDisp.DefaultView;
        RepeaterItemDetails.DataBind();

    }

    /// <summary>
    /// Command is used to view recieved Quotations
    /// </summary>
    /// <param name="source"></param>
    /// <param name="e"></param>
    protected void onSelectQtnRcv(object source, CommandEventArgs e)
    {
        try
        {
            string[] strArgs = e.CommandArgument.ToString().Split('~');
            string Quotation_Code = strArgs[1];
            lblDisplayType.Text = "Item Received From :";
            lblSupplier.Text = strArgs[2];

            DataTable itemsDetails = new DataTable();
            itemsDetails.Columns.Add("ITEM_SERIAL_NO");
            itemsDetails.Columns.Add("Drawing_Number");
            itemsDetails.Columns.Add("Part_Number");
            itemsDetails.Columns.Add("ITEM_SHORT_DESC");
            itemsDetails.Columns.Add("QUOTED_QTY");
            itemsDetails.Columns.Add("OFFERED_QTY");
            itemsDetails.Columns.Add("Unit_and_Packings");
            itemsDetails.Columns.Add("QUOTED_RATE");
            itemsDetails.Columns.Add("QUOTED_DISCOUNT");
            itemsDetails.Columns.Add("Item_Type");
            itemsDetails.Columns.Add("Lead_Time");
            itemsDetails.Columns.Add("QUOTATION_REMARKS");

            DataRow[] foundRows;
            foundRows = ((DataTable)(ViewState["QuotationItemsDetails"])).Select("QUOTATION_CODE='" + Quotation_Code + "' and Quotation_Status = 'R'  ");
            foreach (DataRow d in foundRows)
            {
                itemsDetails.Rows.Add();
                itemsDetails.Rows[itemsDetails.Rows.Count - 1]["ITEM_SERIAL_NO"] = d["ITEM_SERIAL_NO"];
                itemsDetails.Rows[itemsDetails.Rows.Count - 1]["Drawing_Number"] = d["Drawing_Number"];
                itemsDetails.Rows[itemsDetails.Rows.Count - 1]["Part_Number"] = d["Part_Number"];
                itemsDetails.Rows[itemsDetails.Rows.Count - 1]["ITEM_SHORT_DESC"] = d["ITEM_SHORT_DESC"];
                itemsDetails.Rows[itemsDetails.Rows.Count - 1]["QUOTED_QTY"] = d["QUOTED_QTY"];
                itemsDetails.Rows[itemsDetails.Rows.Count - 1]["OFFERED_QTY"] = d["OFFERED_QTY"];
                itemsDetails.Rows[itemsDetails.Rows.Count - 1]["QUOTED_RATE"] = d["QUOTED_RATE"];
                itemsDetails.Rows[itemsDetails.Rows.Count - 1]["QUOTED_DISCOUNT"] = d["QUOTED_DISCOUNT"];
                itemsDetails.Rows[itemsDetails.Rows.Count - 1]["Item_Type"] = d["Item_Type"];
                itemsDetails.Rows[itemsDetails.Rows.Count - 1]["Unit_and_Packings"] = d["Unit_and_Packings"];
                itemsDetails.Rows[itemsDetails.Rows.Count - 1]["Lead_Time"] = d["Lead_Time"];
                itemsDetails.Rows[itemsDetails.Rows.Count - 1]["QUOTATION_REMARKS"] = d["QUOTATION_REMARKS"];
            }

            RepeaterSupItemsRcv.DataSource = itemsDetails;
            RepeaterSupItemsRcv.DataBind();

            tblItemsDetail.Visible = true;
            RepeaterItemDetails.Visible = false;
            RepeaterSupItemsRcv.Visible = true;

            tblQuotationDetails.Visible = true;
            divSystemParticulars.Visible = true;

            if (itemsDetails.Rows.Count > 0)
            {
                pnlGrandTotal.Visible = true;
                BindAdditionalDetails(Quotation_Code);
            }
            else
            {
                pnlGrandTotal.Visible = false;
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    /// <summary>
    /// Export To PDF All Quotation Summary
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>

    protected void btnExporttoPDF_Click(object sender, EventArgs e)
    {

        try
        {
            lblRFQSent.Visible = true;
            lblRD.Visible = true;

            lblQRF.Visible = true;

            lblQD.Visible = true;

            lblID.Visible = true;
            lblAF.Visible = true;


            System.Web.UI.HtmlControls.HtmlTableCell thViewQuotationSent = (System.Web.UI.HtmlControls.HtmlTableCell)RepeaterRfqSent.Controls[0].Controls[0].FindControl("thViewQuotationSent");
            thViewQuotationSent.Visible = false;


            System.Web.UI.HtmlControls.HtmlTableCell thViewQuotation = (System.Web.UI.HtmlControls.HtmlTableCell)RepeaterQtnRcv.Controls[0].Controls[0].FindControl("thViewQuotation");
            thViewQuotation.Visible = false;

            foreach (RepeaterItem item in RepeaterQtnRcv.Items)
            {

                if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                {
                    System.Web.UI.HtmlControls.HtmlTableCell tdViewQuotation = (System.Web.UI.HtmlControls.HtmlTableCell)item.FindControl("tdViewQuotation");
                    ImageButton BtnViewQtnRFQ = (ImageButton)item.FindControl("imgBtnViewQtnRFQ");
                    BtnViewQtnRFQ.Visible = tdViewQuotation.Visible = false;
                }
            }

            foreach (RepeaterItem item in RepeaterRfqSent.Items)
            {

                if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                {
                    System.Web.UI.HtmlControls.HtmlTableCell tdViewQuotationSent = (System.Web.UI.HtmlControls.HtmlTableCell)item.FindControl("tdViewQuotationSent");
                    ImageButton imgViewQuotationSent = (ImageButton)item.FindControl("imgViewQuotationSent");
                    imgViewQuotationSent.Visible = tdViewQuotationSent.Visible = false;
                }
            }


            btnExporttoPDF.Visible = false;



            EO.Pdf.HtmlToPdf.Options.PageSize = new SizeF(17.0f, 17.69f);
            PdfDocument doc = new PdfDocument();



            EO.Pdf.Runtime.AddLicense("p+R2mbbA3bNoqbTC4KFZ7ekDHuio5cGz4aFZpsKetZ9Zl6TNHuig5eUFIPGe" +
        "tcznH+du5PflEuCG49jjIfewwO/o9dB2tMDAHuig5eUFIPGetZGb566l4Of2" +
        "GfKetZGbdePt9BDtrNzCnrWfWZekzRfonNzyBBDInbW6yuCwb6y9xtyxdabw" +
        "+g7kp+rp2g+9RoGkscufdePt9BDtrNzpz+eupeDn9hnyntzCnrWfWZekzQzr" +
        "peb7z7iJWZekscufWZfA8g/jWev9ARC8W7zTv/vjn5mkBxDxrODz/+ihb6W0" +
        "s8uud4SOscufWbOz8hfrqO7CnrWfWZekzRrxndz22hnlqJfo8h8=");
            EO.Pdf.HtmlToPdf.Options.HeaderHtmlFormat = "<div style='text-align:top; font-family:Tahoma; font-size:10px;'></div>";
            EO.Pdf.HtmlToPdf.Options.VisibleElementIds = "divMain";
            HtmlToPdf.Options.AutoFitX = HtmlToPdfAutoFitMode.ScaleToFit;

            ASPXToPDF1.RenderAsPDF("QuotationSummary.pdf");
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
       

    }
/// <summary>
/// Item Data Bound to color the approved Quotations
/// </summary>
/// <param name="sender"></param>
/// <param name="e"></param>
    protected void RepeaterQtnRcv_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                HtmlTableRow tr = (HtmlTableRow)e.Item.FindControl("trHeader");



                string orderCode = ((Label)e.Item.FindControl("OrderLabel")).Text;

                if (orderCode == "A")
                {
                    tr.Attributes.Add("style", "background-color:#0DF063;");

                }

            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }

    }
}
