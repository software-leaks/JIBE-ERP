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
    Decimal TotReportUSDValue = 0;
   
    MergeGridviewHeader_Info objChangeReqstMerge = new MergeGridviewHeader_Info();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           // chkPO.Checked = true;
            //Load_VesselList();
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
    public void BindDropDownList()
    {
        DataTable dt = objBLL.Get_VesselList(0, 0, 0, "", Convert.ToInt32(GetCompanyID()));


        ddlVessel.DataSource = dt;
        ddlVessel.DataTextField = "VESSEL_NAME";
        ddlVessel.DataValueField = "VESSEL_ID";
        ddlVessel.DataBind();
        ddlVessel.Items.Insert(0, new ListItem("-All Vessels-", "0"));

        DataSet ds = BLL_POLOG_Register.POLOG_Get_Type(UDFLib.ConvertToInteger(GetSessionUserID()), "ALL_SUPPLIER");

        ddlSupplier.DataSource = ds.Tables[0];
        ddlSupplier.DataTextField = "Supplier_Name";
        ddlSupplier.DataValueField = "Supplier_Code";
        ddlSupplier.DataBind();
        ddlSupplier.Items.Insert(0, new ListItem("-All Suppliers-", "0"));
    }

    public void BindGridView()
    {
        try
        {

            int rowcount = ucCustomPagerItems.isCountRecord;
            string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
            int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());
           

              DataSet ds = BLL_POLOG_Register.POLOG_Get_Stale_PO_Report(UDFLib.ConvertStringToNull(ddlSupplier.SelectedValue),
                                        UDFLib.ConvertIntegerToNull(ddlVessel.SelectedValue), UDFLib.ConvertStringToNull(ddlAging.SelectedValue),ddlView.SelectedValue, 
                                         UDFLib.ConvertIntegerToNull(GetSessionUserID()), sortbycoloumn, sortdirection
                                , ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);
         
            if (ucCustomPagerItems.isCountRecord == 1)
            {
                ucCustomPagerItems.CountTotalRec = rowcount.ToString();
                ucCustomPagerItems.BuildPager();
            }

            if (ds.Tables[0].Rows.Count > 0)
            {
                divPendingInvoice.Visible = true;
                gvPurchase.DataSource = ds.Tables[0];
                gvPurchase.DataBind();
                Label lblReportUSDValue1 = (Label)gvPurchase.FooterRow.FindControl("lblReportUSDValue1");


                lblReportUSDValue1.Text = ds.Tables[1].Rows[0]["PO_USD_Value"].ToString();
               
            }
            else
            {
                divPendingInvoice.Visible = false;
                gvPurchase.DataSource = ds.Tables[0];
                gvPurchase.DataBind();
                gvPurchase.EmptyDataText = "NO RECORDS FOUND";
            }
        }
        catch { }
        {
        }
    }
    protected void gvPurchase_Sorting(object sender, GridViewSortEventArgs se)
    {
        ViewState["SORTBYCOLOUMN"] = se.SortExpression;

        if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
            ViewState["SORTDIRECTION"] = 1;
        else
            ViewState["SORTDIRECTION"] = 0;
        BindGridView();
    }
    protected void ImgExpExcel_Click(object sender, EventArgs e)
    {

        int rowcount = ucCustomPagerItems.isCountRecord;

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());



        DataSet ds = BLL_POLOG_Register.POLOG_Get_Stale_PO_Report(UDFLib.ConvertStringToNull(ddlSupplier.SelectedValue),
                                  UDFLib.ConvertIntegerToNull(ddlVessel.SelectedValue), UDFLib.ConvertStringToNull(ddlAging.SelectedValue),ddlView.SelectedValue, 
                                   UDFLib.ConvertIntegerToNull(GetSessionUserID()), sortbycoloumn, sortdirection
                          , null, null, ref  rowcount);
        //"PO Date","Amount", "Cur", "USD Amt", "Supplier Code", "Supplier Name", "Created By", "Approved By"
        //"Line_Date",,"Line_Amount","Line_Currency","Report_USD_Value","Supplier_Code","Supplier_Name","Created_by", "Line_Status_Updated_by"
        string[] HeaderCaptions = { "Vessel Name", "PO Code", "Aging Days", "PO Date", "Amount", "Cur", "USD Amt", "Supplier Code", "Supplier Name", "Created By", "Approved By" };
        string[] DataColumnsName = { "Vessel_Name", "Office_Ref_Code","Laspe_Day","Line_Date","Line_Amount","Line_Currency","Report_USD_Value","Supplier_Code","Supplier_Name","Created_by", "Line_Status_Updated_by" };

        GridViewExportUtil.ShowExcel(ds.Tables[0], HeaderCaptions, DataColumnsName, "Stale PO Report", "Stale PO Report", "");

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
            TotReportUSDValue += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Report_USD_Value"));
          
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lblReportUSDValue = (Label)e.Row.FindControl("lblReportUSDValue");
            lblReportUSDValue.Text = TotReportUSDValue.ToString();
        }
    }
    protected void btnGet_Click(object sender, ImageClickEventArgs e)
    {
        BindGridView();
    }                                                                                                                                                                  
    protected void ImageRefresh_Click(object sender, ImageClickEventArgs e)
    {
        ddlSupplier.SelectedValue = "0";
        ddlVessel.SelectedValue = "0";
        ddlAging.SelectedValue = "30";                
        ddlView.SelectedValue = "No";
        divPendingInvoice.Visible = false; 
        //BindGridView();                                                                                                                                        
    }

    protected void onDelete(object source, CommandEventArgs e)                             
    {         
        try
        {
            string Type = "Close";
            int retval = BLL_POLOG_Register.POLOG_Update_PODeatils(UDFLib.ConvertIntegerToNull(e.CommandArgument.ToString()), Type, UDFLib.ConvertIntegerToNull(Session["USERID"].ToString()));
            InsertAuditTrail("Close PO", "ClosePO", e.CommandArgument.ToString());
            BindGridView();
        }
        catch { }
        {                                                              
        }                                                                                                                                                                                                                                                                                                                                                                                                          

                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                      
    }
    protected void InsertAuditTrail(string Action, string Description,string SupplyID)
    {
        try
        {
            int RetAuditVal = BLL_POLOG_Register.POLOG_Insert_Transactionlog(UDFLib.ConvertStringToNull(SupplyID), Action, Description, UDFLib.ConvertToInteger(GetSessionUserID()));
        }
        catch { }
        {

        }
    }
}