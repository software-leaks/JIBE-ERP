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
    decimal TotPOUSDValue = 0;
    decimal TotInvUSDAmt = 0;
    decimal TotPaidUSDAmt = 0;
    decimal TotOutPOCurAmt = 0;
   
    MergeGridviewHeader_Info objChangeReqstMerge = new MergeGridviewHeader_Info();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int year = DateTime.Now.Year;
            DateTime firstDay = new DateTime(year, 1, 1);
            //DateTime lastDay = new DateTime(year, 12, 31);
            txtFromDate.Text = firstDay.ToString("dd/MM/yyyy");
            txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            chkApproved.Checked = false;
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
        //if (dt.Rows.Count > 0)
        //{
           // for (int i = 0; i < 1; i++)
            //{
            //    ddlVessel.SelectItems(new string[] { dt.Rows[i]["VESSEL_ID"].ToString() });
           // }

        //}

        Session["sVesselCode"] = ddlVessel.SelectedValues;
    }
   
    protected void BindDropDownList()
    {
        DataSet ds = BLL_POLOG_Register.POLOG_Get_Type(UDFLib.ConvertToInteger(GetSessionUserID()), "ALL_SUPPLIER");

        ddlSupplier.DataSource = ds.Tables[0];
        ddlSupplier.DataTextField = "Supplier_Name";
        ddlSupplier.DataValueField = "Supplier_Code";
        ddlSupplier.DataBind();
        ddlSupplier.Items.Insert(0, new ListItem("-All Suppliers-", "0"));


        DataSet dsAccountType = BLL_POLOG_Register.POLOG_Get_Type(UDFLib.ConvertToInteger(GetSessionUserID()), "Account_Type");
        ddlAccountType.DataSource = dsAccountType.Tables[0];
        ddlAccountType.DataTextField = "VARIABLE_NAME";
        ddlAccountType.DataValueField = "VARIABLE_CODE";
        ddlAccountType.DataBind();
        //if (dsAccountType.Tables[0].Rows.Count > 0)
        //{
           // for (int i = 0; i < 1; i++)
            //{
                //ddlAccountType.SelectItems(new string[] { dsAccountType.Tables[0].Rows[i]["VARIABLE_CODE"].ToString() });
            //}

       // }

        Session["sAccountType"] = ddlAccountType.SelectedValues;
        DataSet dsAccClassification = BLL_POLOG_Register.POLOG_Get_Type(UDFLib.ConvertToInteger(GetSessionUserID()), "Account_Classification");
        ddlAccClassification.DataSource = dsAccClassification.Tables[0];
        ddlAccClassification.DataTextField = "VARIABLE_NAME";
        ddlAccClassification.DataValueField = "VARIABLE_CODE";
        ddlAccClassification.DataBind();
       // if (dsAccClassification.Tables[0].Rows.Count > 0)
       // {
            //for (int i = 0; i < 1; i++)
            //{
               // ddlAccClassification.SelectItems(new string[] { dsAccClassification.Tables[0].Rows[i]["VARIABLE_CODE"].ToString() });
           //}

        //}

        Session["sAccClassification"] = ddlAccClassification.SelectedValues;
        DataSet dsPOTYPE = BLL_POLOG_Register.POLOG_Get_Type(UDFLib.ConvertToInteger(GetSessionUserID()), "PO_TYPE");

        lblPOType.Text = dsPOTYPE.Tables[2].Rows[0]["User_Access"].ToString();
    }
    protected void ddlVessel_SelectedIndexChanged()
    {
        Session["sVesselCode"] = "0";
        Session["sVesselCode"] = ddlVessel.SelectedValues;
        if (Session["sVesselCode"] == "0")
        {
            Session["sVesselCode"] = "0";
        }
        BindGridView();
    }
    protected void ddlAccountType_SelectedIndexChanged()
    {
        Session["sAccountType"] = "0";
        Session["sAccountType"] = ddlAccountType.SelectedValues;
        if (Session["sAccountType"] == "0")
        {
            Session["sAccountType"] = "0";
        }
        BindGridView();
    }
    protected void ddlAccClassification_SelectedIndexChanged()
    {
        Session["sAccClassification"] = "0";
        Session["sAccClassification"] = ddlAccClassification.SelectedValues;
        if (Session["sAccClassification"] == "0")
        {
            Session["sAccClassification"] = "0";
        }
        BindGridView();
    }
    public void BindGridView()
    {
        //try
        //{
            objChangeReqstMerge.AddMergedColumns(new int[] { 5,6 }, "PO", "HeaderStyle-center");
           
            ChkStatus();
            int rowcount = ucCustomPagerItems.isCountRecord;
            string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
            int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());
            if (Session["sVesselCode"] == "0") { Session["sVesselCode"] = ddlVessel.SelectedValues; }
            if (Session["sAccountType"] == "0") { Session["sAccountType"] = ddlAccountType.SelectedValues; }
            if (Session["sAccClassification"] == "0") { Session["sAccClassification"] = ddlAccClassification.SelectedValues; }
           
               DataSet ds = BLL_POLOG_Register.POLOG_Get_Purchase_Report(UDFLib.ConvertStringToNull(ddlSupplier.SelectedValue),
                                        (DataTable)Session["sVesselCode"],(DataTable)Session["sAccountType"],(DataTable)Session["sAccClassification"], CurrStatus,
                                        txtFromDate.Text != "" ? Convert.ToDateTime(txtFromDate.Text) : Convert.ToDateTime("01/01/1900"), txtToDate.Text != "" ? Convert.ToDateTime(txtToDate.Text) : Convert.ToDateTime("01/01/1900"),
                                        UDFLib.ConvertIntegerToNull(GetSessionUserID()), sortbycoloumn, sortdirection
                                , ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);
           

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
                Label lblTotOutPOCurAmt1 = (Label)gvPurchase.FooterRow.FindControl("lblTotOutPOCurAmt1");
                Label lblTotPOUSDValue1 = (Label)gvPurchase.FooterRow.FindControl("lblTotPOUSDValue1");
                Label lblTotInvUSDAmt1 = (Label)gvPurchase.FooterRow.FindControl("lblTotInvUSDAmt1");
                Label lblTotPaidUSDAmt1 = (Label)gvPurchase.FooterRow.FindControl("lblTotPaidUSDAmt1");
                lblTotOutPOCurAmt1.Text = ds.Tables[1].Rows[0]["OutStanding_USD_Amount"].ToString();
                lblTotPOUSDValue1.Text = ds.Tables[1].Rows[0]["PO_USD_Value"].ToString();
                lblTotInvUSDAmt1.Text = ds.Tables[1].Rows[0]["Invoice_USD_Amount"].ToString();
                lblTotPaidUSDAmt1.Text = ds.Tables[1].Rows[0]["Paid_USD_Amount"].ToString();
            }
            else
            {
                divPurchase.Visible = false;
                gvPurchase.DataSource = ds.Tables[0];
                gvPurchase.DataBind();
                gvPurchase.EmptyDataText = "NO RECORDS FOUND";
            }
       // }
        //catch { }
        //{
        //}
    }
    protected void ChkStatus()
    {
        if (chkApproved.Checked == true)
        {
            CurrStatus = "APPROVED";
        }
        if (chkApproval.Checked == true)
        {
            if (CurrStatus == "")
            {
                CurrStatus = "FOR APPROVAL";
            }
            else
            {
                CurrStatus = CurrStatus + "," + "FOR APPROVAL";
            }
        }
        if (chkUnApproved.Checked == true)
        {
            if (CurrStatus == "")
            {
                CurrStatus = "UNAPPROVED";
            }
            else
            {
                CurrStatus = CurrStatus + "," + "UNAPPROVED";
            }
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
            TotPOUSDValue += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "PO_USD_Value"));
            TotInvUSDAmt += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Invoice_USD_Amount"));
            TotPaidUSDAmt += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Paid_USD_Amount"));
            TotOutPOCurAmt += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "OutStanding_USD_Amount"));
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lblTotPOUSDValue = (Label)e.Row.FindControl("lblTotPOUSDValue");
            Label lblTotInvUSDAmt = (Label)e.Row.FindControl("lblTotInvUSDAmt");
            Label lblTotPaidUSDAmt = (Label)e.Row.FindControl("lblTotPaidUSDAmt");
            Label lblTotOutPOCurAmt = (Label)e.Row.FindControl("lblTotOutPOCurAmt");
            Label lblTotOutPOCurAmt1 = (Label)e.Row.FindControl("lblTotOutPOCurAmt1");
            Label lblTotPOUSDValue1 = (Label)e.Row.FindControl("lblTotPOUSDValue1");
            Label lblTotInvUSDAmt1 = (Label)e.Row.FindControl("lblTotInvUSDAmt1");
            Label lblTotPaidUSDAmt1 = (Label)e.Row.FindControl("lblTotPaidUSDAmt1");
            lblTotPOUSDValue.Text = TotPOUSDValue.ToString();
            lblTotInvUSDAmt.Text = TotInvUSDAmt.ToString();
            lblTotPaidUSDAmt.Text = TotPaidUSDAmt.ToString();
            lblTotOutPOCurAmt.Text = TotOutPOCurAmt.ToString();
            lblTotOutPOCurAmt1.Text = TotOutPOCurAmt.ToString();
            lblTotPOUSDValue1.Text = TotPOUSDValue.ToString();
            lblTotInvUSDAmt1.Text = TotInvUSDAmt.ToString();
            lblTotPaidUSDAmt1.Text = TotOutPOCurAmt.ToString();
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
        ddlVessel.ClearSelection();
        ddlAccountType.ClearSelection();
        ddlAccClassification.ClearSelection();
        Session["sAccClassification"] = "0";
        Session["sAccountType"] = "0";
        Session["sVesselCode"] = "0";
        chkApproved.Checked = false;
        chkApproval.Checked = false;
        chkUnApproved.Checked = false;
        Load_VesselList();
        BindDropDownList();
        divPurchase.Visible = false;
    }
    protected void ImgExpExcel_Click(object sender, EventArgs e)
    {

        int rowcount = ucCustomPagerItems.isCountRecord;

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

        ChkStatus();

        if (Session["sVesselCode"] == "0") { Session["sVesselCode"] = ddlVessel.SelectedValues; }
        if (Session["sAccountType"] == "0") { Session["sAccountType"] = ddlAccountType.SelectedValues; }
        if (Session["sAccClassification"] == "0") { Session["sAccClassification"] = ddlAccClassification.SelectedValues; }


        DataSet ds = BLL_POLOG_Register.POLOG_Get_Purchase_Report(UDFLib.ConvertStringToNull(ddlSupplier.SelectedValue),
                                         (DataTable)Session["sVesselCode"], (DataTable)Session["sAccountType"], (DataTable)Session["sAccClassification"], CurrStatus,
                                         txtFromDate.Text != "" ? Convert.ToDateTime(txtFromDate.Text) : Convert.ToDateTime("01/01/1900"), txtToDate.Text != "" ? Convert.ToDateTime(txtToDate.Text) : Convert.ToDateTime("01/01/1900"),
                                         UDFLib.ConvertIntegerToNull(GetSessionUserID()), sortbycoloumn, sortdirection
                                 , null, null, ref  rowcount);


        string[] HeaderCaptions = { "PO Status", "PO Date", "PO Code", "ship Ref Code", "Acct", "Amount", "Cur", "PO USD Value", "Invoice USD Amount", "Paid USD Amount","Outstanding PO Amount", "Supplier Name"};
        string[] DataColumnsName = { "Line_Status", "Line_Date", "Office_Ref_Code", "ship_Ref_Code", "Account_Classification", "Line_Amount", "Line_Currency", "PO_USD_Value", "Invoice_USD_Amount", "Paid_USD_Amount", "OutStanding_USD_Amount", "Supplier_Name" };

        GridViewExportUtil.ShowExcel(ds.Tables[0], HeaderCaptions, DataColumnsName, "Purchasing_Report", "Purchasing Report", "");

    }
    protected void ImgCompare_Click(object s, CommandEventArgs e)
    {
        //PO_LOG/Compare_PO_Invoice.asp?Invoice_ID=02695&Supply_ID=3587
        string OCA_URL = null;
        if (!Request.Url.AbsoluteUri.Contains(ConfigurationManager.AppSettings["OCA_APP_URL"]))
        {
            OCA_URL = ConfigurationManager.AppSettings["OCA_APP_URL"];
        }
        string[] arg = e.CommandArgument.ToString().Split(',');

        string Invoice_ID = arg[0];
        string Supply_ID = arg[1];
        string OCA_URL1 = OCA_URL + "/PO_LOG/Compare_PO_Invoice.asp?Invoice_ID=" + Invoice_ID + "&Supply_ID=" + Supply_ID + "";

        ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "javascript:window.open('" + OCA_URL1 + "');", true);

    }
}