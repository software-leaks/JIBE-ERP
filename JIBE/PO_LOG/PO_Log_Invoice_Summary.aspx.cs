using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Telerik.Web.UI;
using SMS.Business.Infrastructure;
using SMS.Properties;
using SMS.Business.POLOG;

public partial class PO_LOG_PO_Log_Purchasing_Report : System.Web.UI.Page
{
    UserAccess objUA = new UserAccess();
    BLL_Infra_VesselLib objBLL = new BLL_Infra_VesselLib();
    public string CurrStatus = null;
    public string POStatus = null;
    public string UnpaidInvoiceStatus = null;
    public string InvoiceStatus = null;
    Decimal TotPOUSDValue = 0;
    Decimal TotInvoiceUSDValue = 0;
    
    MergeGridviewHeader_Info objChangeReqstMerge = new MergeGridviewHeader_Info();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //ViewState["SupplyID"] = "";
            int year = DateTime.Now.Year;
            DateTime firstDay = new DateTime(year, 1, 1);
            //DateTime lastDay = new DateTime(year, 12, 31);
            txtFromDate.Text = firstDay.ToString("dd/MM/yyyy");
            txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

            txtFromPayment.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtToPayment.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtFromPayment.Enabled = false;
            txtToPayment.Enabled = false;
            chkPO.Checked = false;
            Load_VesselList();
            BindDropDownList();
            //BindGridView();
        }
    }
   
    private int GetCompanyID()
    {
        if (Session["USERCOMPANYID"] != null)
            return int.Parse(Session["USERCOMPANYID"].ToString());
        else
            return 0;
    }
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }
    public void Load_VesselList()
    {
        DataTable dt = objBLL.Get_VesselList(0, 0, 0, "", Convert.ToInt32(GetCompanyID()));


        ddlVessel.DataSource = dt;
        ddlVessel.DataTextField = "VESSEL_NAME";
        ddlVessel.DataValueField = "VESSEL_ID";
        ddlVessel.DataBind();
        ddlVessel.Items.Insert(0, new ListItem("-All Vessels-", "0"));
        
    }
   
    protected void BindDropDownList()
    {
        DataSet ds = BLL_POLOG_Register.POLOG_Get_Type(UDFLib.ConvertToInteger(GetSessionUserID()), "ALL_SUPPLIER");

        ddlSupplier.DataSource = ds.Tables[0];
        ddlSupplier.DataTextField = "Supplier_Name";
        ddlSupplier.DataValueField = "Supplier_Code";
        ddlSupplier.DataBind();
        ddlSupplier.Items.Insert(0, new ListItem("-All Suppliers-", "0"));

        DataSet dsOwner = BLL_POLOG_Register.POLOG_Get_Type(UDFLib.ConvertToInteger(GetSessionUserID()), "OWNER");
        ddlOwner.DataSource = dsOwner.Tables[0];
        ddlOwner.DataTextField = "Supplier_Name";
        ddlOwner.DataValueField = "Supplier_Code";
        ddlOwner.DataBind();
        ddlOwner.Items.Insert(0, new ListItem("-All Owners-", "0"));

      
    }
  
    public void BindGridView()
    {
        try
        {
           
            objChangeReqstMerge.AddMergedColumns(new int[] { 0,1,2,3,4,5 }, "PO", "HeaderStyle-center");
            objChangeReqstMerge.AddMergedColumns(new int[] { 6,7,8,9,10,11,12 }, "Invoice", "HeaderStyle-center");
            objChangeReqstMerge.AddMergedColumns(new int[] { 13,14,15 }, "Payment", "HeaderStyle-center");
            ViewState["SupplyID"] = "";
            ChkStatus();
            int rowcount = ucCustomPagerItems.isCountRecord;
            string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
            int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());
            DataSet ds = new DataSet();
            if (POStatus != null || UnpaidInvoiceStatus != null || InvoiceStatus != null)
            {
                ds = BLL_POLOG_Register.POLOG_Get_Invoice_Summary_Report(UDFLib.ConvertStringToNull(ddlSupplier.SelectedValue),
                    UDFLib.ConvertIntegerToNull(ddlVessel.SelectedValue), UDFLib.ConvertStringToNull(ddlOwner.SelectedValue), POStatus, UnpaidInvoiceStatus, InvoiceStatus, txtFromDate.Text != "" ? Convert.ToDateTime(txtFromDate.Text) : Convert.ToDateTime("01/01/1900")
                    , txtToDate.Text != "" ? Convert.ToDateTime(txtToDate.Text) : Convert.ToDateTime("01/01/1900"),
                      txtFromPayment.Text != "" ? Convert.ToDateTime(txtFromPayment.Text) : Convert.ToDateTime("01/01/1900"), txtToPayment.Text != "" ? Convert.ToDateTime(txtToPayment.Text) : Convert.ToDateTime("01/01/1900"), UDFLib.ConvertIntegerToNull(GetSessionUserID()), sortbycoloumn, sortdirection
                                  , ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);
            }
            //}

            if (ucCustomPagerItems.isCountRecord == 1)
            {
                ucCustomPagerItems.CountTotalRec = rowcount.ToString();
                ucCustomPagerItems.BuildPager();
            }

            if (ds.Tables[0].Rows.Count > 0)
            {
                divPurchase.Visible = true;
                gvPurchase.DataSource = ds.Tables[0];
                gvPurchase.DataBind();
                Label lblPOUSDAmount1 = (Label)gvPurchase.FooterRow.FindControl("lblPOUSDAmount1");
                Label lblInvBookValue1 = (Label)gvPurchase.FooterRow.FindControl("lblInvBookValue1");

                lblPOUSDAmount1.Text = ds.Tables[1].Rows[0]["PO_USD_Value"].ToString();
                lblInvBookValue1.Text = ds.Tables[2].Rows[0]["Invoice_USD_Amount"].ToString();
            }
            else
            {
                divPurchase.Visible = false;
                gvPurchase.DataSource = ds.Tables[0];
                gvPurchase.DataBind();
                gvPurchase.EmptyDataText = "NO RECORDS FOUND";
            }
        }
        catch { }
        {
        }
    }
    protected void ChkStatus()
    {
        if (chkPO.Checked == true)
        {
            POStatus = "YES";
            //UnpaidInvoiceStatus = null;
            //InvoiceStatus = null;
        }
        else
        {
            POStatus = null;
        }
        if (chkUnpaidInvoice.Checked == true)
        {
            //POStatus = null;
            //InvoiceStatus = null;
            UnpaidInvoiceStatus = "YES";
        }
        else
        {
            UnpaidInvoiceStatus = null;
        }
        if (chkInvoice.Checked == true)
        {
            //POStatus = null;
            //UnpaidInvoiceStatus = null;
            InvoiceStatus = "YES";
        }
        else
        {
            InvoiceStatus = null;
        }

    }
    protected void gvPurchase_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            MergeGridviewHeader.SetProperty(objChangeReqstMerge);
            e.Row.SetRenderMethodDelegate(MergeGridviewHeader.RenderHeader);
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label Line_Date = (Label)e.Row.FindControl("lblLine_Date");
            Label Ref_Code = (Label)e.Row.FindControl("lblOffice_Ref_Code");
            Label Display_Name = (Label)e.Row.FindControl("lblSupplier_Display_Name");
            Label Line_Amount = (Label)e.Row.FindControl("lblLine_Amount");
            Label Invoice_Ref = (Label)e.Row.FindControl("lblInvoice_Ref");
            Label USD_Amount = (Label)e.Row.FindControl("lblPO_USD_Amount");
            TotInvoiceUSDValue += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Invoice_Book_Value"));
            string SupplyID = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Supply_ID"));
            if (SupplyID == ViewState["SupplyID"].ToString())
            {
                Line_Date.Visible = false;
                Ref_Code.Visible = false;
                Display_Name.Visible = false;
                Line_Amount.Visible = false;
                Invoice_Ref.Visible = false;
                USD_Amount.Visible = false;

            }
            else
            {
                TotPOUSDValue += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "PO_USD_Amount"));
                
            }
            ViewState["SupplyID"] = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Supply_ID"));
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            //MergeGridviewHeader.SetProperty(objChangeReqstMerge);
            //e.Row.SetRenderMethodDelegate(MergeGridviewHeader.RenderHeader);
            Label lblPOUSDAmount = (Label)e.Row.FindControl("lblPOUSDAmount");
            lblPOUSDAmount.Text = TotPOUSDValue.ToString();
            Label lblInvBookValue = (Label)e.Row.FindControl("lblInvBookValue");
            lblInvBookValue.Text = TotInvoiceUSDValue.ToString();
        }
    }
    protected void btnGet_Click(object sender, ImageClickEventArgs e)
    {
        BindGridView();
    }
    protected void ImageRefresh_Click(object sender, ImageClickEventArgs e)
    {
        int year = DateTime.Now.Year;
        DateTime firstDay = new DateTime(year, 1, 1);
        //DateTime lastDay = new DateTime(year, 12, 31);
        txtFromDate.Text = firstDay.ToString("dd/MM/yyyy");
        txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

        txtFromPayment.Text = DateTime.Now.ToString("dd/MM/yyyy");
        txtToPayment.Text = DateTime.Now.ToString("dd/MM/yyyy");
        ddlSupplier.SelectedValue = "0";
      
        ddlVessel.SelectedValue = "0";
        ddlOwner.SelectedValue = "0";
      
        chkPO.Checked = true;
        chkUnpaidInvoice.Checked = false;
        chkInvoice.Checked = false;
        divPurchase.Visible = false;
        //BindGridView();
    }
    protected void ImgExpExcel_Click(object sender, EventArgs e)
    {
  
        int rowcount = ucCustomPagerItems.isCountRecord;
        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());
        ChkStatus();
         DataSet ds = new DataSet();
         if (POStatus != null || UnpaidInvoiceStatus != null || InvoiceStatus != null)
         {
              ds = BLL_POLOG_Register.POLOG_Get_Invoice_Summary_Report(UDFLib.ConvertStringToNull(ddlSupplier.SelectedValue),
                 UDFLib.ConvertIntegerToNull(ddlVessel.SelectedValue), UDFLib.ConvertStringToNull(ddlOwner.SelectedValue), POStatus, UnpaidInvoiceStatus, InvoiceStatus, txtFromDate.Text != "" ? Convert.ToDateTime(txtFromDate.Text) : Convert.ToDateTime("01/01/1900")
                 , txtToDate.Text != "" ? Convert.ToDateTime(txtToDate.Text) : Convert.ToDateTime("01/01/1900"),
                   txtFromPayment.Text != "" ? Convert.ToDateTime(txtFromPayment.Text) : Convert.ToDateTime("01/01/1900"), txtToPayment.Text != "" ? Convert.ToDateTime(txtToPayment.Text) : Convert.ToDateTime("01/01/1900"), UDFLib.ConvertIntegerToNull(GetSessionUserID()), sortbycoloumn, sortdirection
                               , null, null, ref  rowcount);
         }


        string[] HeaderCaptions = { "PO Date", "PO Code", "Supplier Name", "Amount", "Cur", "USD Amt", "Invoice Date", "Invoice Due Date", "Invoice Reference", "Invoice Status", "Invoice Amount", "Invoice Currency", "USD Amount", "Payment Mode", "Payment Date", "Payment Status" };
        string[] DataColumnsName = { "Line_Date", "Office_Ref_Code", "Supplier_Display_Name", "Line_Amount", "Line_Currency", "PO_USD_Amount", "Invoice_Date", "Invoice_Due_Date", "Invoice_Reference", "Invoice_Status", "Invoice_Amount", "Invoice_Currency", "Invoice_Book_Value", "Payment_Mode", "PAYMENT_DATE", "Invoice_PAYMENT_STATUS" };

        GridViewExportUtil.ShowExcel(ds.Tables[0], HeaderCaptions, DataColumnsName, "Invoice_Summary_Report", "Invoice Summary Report", "");

    }
    protected void chkInvoice_CheckedChanged(object sender, EventArgs e)
    {
        if(chkInvoice.Checked == true)
        {
            txtFromPayment.Enabled = true;
            txtToPayment.Enabled = true;
        }
        else
        {
            txtFromPayment.Enabled = false;
            txtToPayment.Enabled = false;
        }
    }
}