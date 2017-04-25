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
using System.Collections.Generic;


public partial class PO_LOG_Invoice_Payment_Approval : System.Web.UI.Page
{
    UserAccess objUA = new UserAccess();
    string clientID;
    public string OperationMode = "";
    public static Dictionary<int, string> MergedColumnCss = new Dictionary<int, string>();
    MergeGridviewHeader_Info objChangeReqstMerge = new MergeGridviewHeader_Info();
    //MergeGridviewHeader_Payment objChangeReqstMerge1 = new MergeGridviewHeader_Payment();
    public Boolean uaEditFlag = false;
    public Boolean uaDeleteFlage = false;
    int totalClean = 0, TotalDirty = 0, TotalDirectInv = 0, TotalLatePO = 0, TotalReworkIN = 0, TotalReworkOut = 0, TotalOthers = 0, TotalInvoice = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        UserAccessValidation();
        this.Form.DefaultButton = this.btnGet.UniqueID;
        if (!IsPostBack)
        {
            string Amount_Status = "LESSTHANUSD1000";
            ViewState["InvoiceWorkflow"] = "Clean" + "_" + Amount_Status; ;
            Load_dropdownlist();
            BindType();
            BindInvoiceCount();
            BindPaymentSchedule();
            BindInvoiceApprovedGrid();
            divApprovedPayment.Visible = false;
            dvWithhold.Visible = false;
        }
        
    }
    public void Load_dropdownlist()
    {
        string SearchType = "PAYMENT";
        DataSet ds = BLL_POLOG_Register.POLOG_Get_Supplier_InvoiceWise(UDFLib.ConvertIntegerToNull(GetSessionUserID()), SearchType);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlSupplier.DataSource = ds.Tables[0];
            ddlSupplier.DataTextField = "Supplier_Name";
            ddlSupplier.DataValueField = "Supplier_Code";
            ddlSupplier.DataBind();
            ddlSupplier.Items.Insert(0, new ListItem("-All-", "0"));
        }
        else
        {
            ddlSupplier.Items.Insert(0, new ListItem("-All-", "0"));
        }
        if (ds.Tables[1].Rows.Count > 0)
        {
            ddlVessel.DataSource = ds.Tables[1];
            ddlVessel.DataTextField = "Vessel_Name";
            ddlVessel.DataValueField = "Vessel_ID";
            ddlVessel.DataBind();
            ddlVessel.Items.Insert(0, new ListItem("-All-", "0"));
        }
        else
        {
            ddlVessel.Items.Insert(0, new ListItem("-All-", "0"));
        }
        if (ds.Tables[2].Rows.Count > 0)
        {
            ddlOwner.DataSource = ds.Tables[2];
            ddlOwner.DataTextField = "Owner_Name";
            ddlOwner.DataValueField = "Owner_Code";
            ddlOwner.DataBind();
            ddlOwner.Items.Insert(0, new ListItem("-All-", "0"));
        }
        else
        {
            ddlOwner.Items.Insert(0, new ListItem("-All-", "0"));
        }
    }
    protected void BindInvoiceCount()
    {
        try
        {
            DataTable dt = BLL_POLOG_Register.POLOG_Get_Approved_Invoice_Count(UDFLib.ConvertStringToNull(ddlSupplier.SelectedValue),
                UDFLib.ConvertIntegerToNull(ddlVessel.SelectedValue), UDFLib.ConvertStringToNull(ddlOwner.SelectedValue),chkUrgent.Checked ? 1: 0, UDFLib.ConvertIntegerToNull(GetSessionUserID()));

            if (dt.Rows.Count > 0)
            {
                divInvoiceCount.Visible = true;
                gvInviceCount.DataSource = dt;
                gvInviceCount.DataBind();
            }
            else
            {
                divInvoiceCount.Visible = false;
                gvInviceCount.DataSource = dt;
                gvInviceCount.DataBind();
            }
        }
        catch { }
        {
        }
    }
    
    public void BindPaymentSchedule()
    {
        try
        {
            DataSet ds = BLL_POLOG_Register.POLOG_Get_Payment_Schedule_Amount(UDFLib.ConvertStringToNull(ddlSupplier.SelectedValue),
                UDFLib.ConvertIntegerToNull(ddlVessel.SelectedValue), UDFLib.ConvertStringToNull(ddlOwner.SelectedValue), chkUrgent.Checked ? 1 : 0, UDFLib.ConvertIntegerToNull(GetSessionUserID()));
          
            if (ds.Tables[0].Rows.Count > 0)
            {
                table1.Visible = true;
                gvPaymentApproved.DataSource = ds.Tables[0];
                gvPaymentApproved.DataBind();
                gvPaymentSchedule.DataSource = ds.Tables[1];
                gvPaymentSchedule.DataBind();
            }
            else
            {
                table1.Visible = false;
                gvPaymentApproved.DataSource = ds.Tables[0];
                gvPaymentApproved.DataBind();
                gvPaymentSchedule.DataSource = ds.Tables[1];
                gvPaymentSchedule.DataBind();
            }
        }
        catch { }
        {
        }
    }
    protected void BindInvoiceApprovedGrid()
    {
        try
        {
            objChangeReqstMerge.AddMergedColumns(new int[] { 2, 3, 4 }, "PO", "HeaderStyle-center");
            objChangeReqstMerge.AddMergedColumns(new int[] { 5, 6, 7, 8, 9, 10, 11, 12, 13 }, "Invoice", "HeaderStyle-center");
            objChangeReqstMerge.AddMergedColumns(new int[] { 14, 15 }, "Invoice Approved", "HeaderStyle-center");

            int rowcount = ucCustomPager1.isCountRecord;
            string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
            int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());
            string InvoiceStatus = (ViewState["InvoiceStatus"] == null) ? null : (ViewState["InvoiceStatus"].ToString());
            string InvoiceWorkflow = (ViewState["InvoiceWorkflow"] == null) ? null : (ViewState["InvoiceWorkflow"].ToString());
            //string InvoiceWorkflow = (ViewState["InvoiceWorkflow"] == null) ? null : (ViewState["InvoiceWorkflow"].ToString());
           
            DataSet ds = BLL_POLOG_Register.POLOG_Get_Approved_Invoice_Search(UDFLib.ConvertStringToNull(ddlSupplier.SelectedValue),
                UDFLib.ConvertIntegerToNull(ddlVessel.SelectedValue), UDFLib.ConvertStringToNull(ddlOwner.SelectedValue), chkUrgent.Checked ? 1 : 0,InvoiceStatus,InvoiceWorkflow, UDFLib.ConvertIntegerToNull(GetSessionUserID()), sortbycoloumn, sortdirection
                             , ucCustomPager1.CurrentPageIndex, ucCustomPager1.PageSize, ref  rowcount);


            if (ucCustomPager1.isCountRecord == 1)
            {
                ucCustomPager1.CountTotalRec = rowcount.ToString();
                ucCustomPager1.BuildPager();
            }

            if (ds.Tables[0].Rows.Count > 0)
            {
                divApprovedInvoice.Visible = true;
                btnApprove.Visible = true;
                //btnApproveAll.Visible = true;
                //btnRework.Visible = true;
                //btnRefresh.Visible = true;
                //btnUnApprove.Visible = false;
                gvApprovedInvoice.DataSource = ds.Tables[0];
                gvApprovedInvoice.DataBind();
            }
            else
            {
                divApprovedInvoice.Visible = false;
                gvApprovedInvoice.DataSource = ds.Tables[0];
                gvApprovedInvoice.DataBind();
            }
          
        }
        catch { }
        {
        }
    }
    protected void BindPaymentApprovedGrid()
    {
        try
        {
            int rowcount = ucCustomPager2.isCountRecord;
            objChangeReqstMerge.AddMergedColumns(new int[] { 2, 3, 4 }, "PO", "HeaderStyle-center");
            //objChangeReqstMerge.AddMergedColumns(new int[] { 5, 6 }, "Total Invoice Value & Count", "HeaderStyle-center");
            objChangeReqstMerge.AddMergedColumns(new int[] { 5, 6,7, 8, 9,10, 11,12,13 }, "Invoice", "HeaderStyle-center");
            objChangeReqstMerge.AddMergedColumns(new int[] { 14, 15 }, "Payment Approved", "HeaderStyle-center");

            string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
            int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());
            string InvoiceAmt = (ViewState["InvoiceAmt"] == null) ? null : (ViewState["InvoiceAmt"].ToString());
            string InvoiceStatus = (ViewState["InvoiceStatus"] == null) ? null : (ViewState["InvoiceStatus"].ToString());
            string PaymentStatus = (ViewState["PaymentStatus"] == null) ? null : (ViewState["PaymentStatus"].ToString());

            DataTable dt = BLL_POLOG_Register.POLOG_Get_Approved_Payment_Invoice_Search(UDFLib.ConvertStringToNull(ddlSupplier.SelectedValue),
                UDFLib.ConvertIntegerToNull(ddlVessel.SelectedValue), UDFLib.ConvertStringToNull(ddlOwner.SelectedValue), chkUrgent.Checked ? 1 : 0, InvoiceStatus, PaymentStatus, UDFLib.ConvertIntegerToNull(GetSessionUserID()), sortbycoloumn, sortdirection
                             , ucCustomPager2.CurrentPageIndex, ucCustomPager2.PageSize, ref  rowcount);


            if (ucCustomPager2.isCountRecord == 1)
            {
                ucCustomPager2.CountTotalRec = rowcount.ToString();
                ucCustomPager2.BuildPager();
            }
            if (dt.Rows.Count > 0)
            {
                btnUnApprove.Visible = true;
                divApprovedPayment.Visible = true;
                gvApprovedPayment.DataSource = dt;
                gvApprovedPayment.DataBind();
            }
            else
            {
                divApprovedPayment.Visible = false;
                btnUnApprove.Visible = false;
                gvApprovedPayment.DataSource = dt;
                gvApprovedPayment.DataBind();
            }
           
        }
        catch { }
        {
        }
    }
    protected void BindType()
    {
        try
        {
            DataSet ds = BLL_POLOG_Register.POLOG_Get_Type(UDFLib.ConvertToInteger(Session["UserID"].ToString()), "PO_TYPE");
            chkType.DataSource = ds.Tables[0];
            chkType.DataTextField = "VARIABLE_NAME";
            chkType.DataValueField = "VARIABLE_CODE";
            chkType.DataBind();

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                chkType.Items[i].Selected = true;
                string color = ds.Tables[0].Rows[i]["COLOR_CODE"].ToString();
                chkType.Items[i].Attributes.Add("style", "background-color: " + color + ";");
                //chkType.Attributes.Add("style", "background-color: " + color + ";");
            }
        }
        catch { }
        {
        }
    }
    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
            Response.Redirect("~/default.aspx?msgid=1");

        if (objUA.Add == 0)
        {

        }
        if (objUA.Edit == 1)
            uaEditFlag = true;
        else
            // btnsave.Visible = false;

            if (objUA.Delete == 1) uaDeleteFlage = true;

    }
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }
    protected void btnGet_Click(object sender, EventArgs e)
    {
         ViewState["InvoiceStatus"] = null;
        BindInvoiceCount();
        BindPaymentSchedule();
        BindInvoiceApprovedGrid();
        divApprovedPayment.Visible = false;
        dvWithhold.Visible = false;
    }
    protected void gvApprovedInvoice_RowDataBound(object sender, GridViewRowEventArgs e)
    {
       if (e.Row.RowType == DataControlRowType.Header)
        {
            MergeGridviewHeader.SetProperty(objChangeReqstMerge);
            e.Row.SetRenderMethodDelegate(MergeGridviewHeader.RenderHeader);
        }
       if (e.Row.RowType == DataControlRowType.DataRow)
       {

           string ColorCode = DataBinder.Eval(e.Row.DataItem, "COLOR_CODE").ToString();
           System.Drawing.Color col = System.Drawing.ColorTranslator.FromHtml(ColorCode);
           
           DateTime Invoice_Date = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "Invoice_Date").ToString());
           DateTime PO_Date = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "Created_Date").ToString());
           DateTime Payment_Due_Date = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "Payment_Due_Date").ToString());
           Button ViewRemarks = (Button)e.Row.FindControl("btnViewRemarks");
           Label lblUrgent = (Label)e.Row.FindControl("lblUrgent");
           Label lblUrgentFlag = (Label)e.Row.FindControl("lblUrgentFlag");
           LinkButton lblSupplier = (LinkButton)e.Row.FindControl("lbl_SupplierName");
           LinkButton lblPO = (LinkButton)e.Row.FindControl("lblPOCode");
           Label lblInvoiceStatus = (Label)e.Row.FindControl("lblInvoice_Status");
           CheckBox chkinvoice = (CheckBox)e.Row.FindControl("chkInvoice");
           Label lblDispute = (Label)e.Row.FindControl("lblDispute");
           Button btnDispute = (Button)e.Row.FindControl("btnDispute");
           Label lblDays = (Label)e.Row.FindControl("lblDays");
           Label lblPaymentDate = (Label)e.Row.FindControl("lblPaymentDate");
           Label lblInvoice_Value = (Label)e.Row.FindControl("lblInvoice_Value");
           Label lblInvoice_Currency = (Label)e.Row.FindControl("lblInvoice_Currency");
           if (Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Invoice_Value")) > Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "PO_Amount")))
           {
               lblInvoice_Value.ForeColor = System.Drawing.Color.Red;
           }
           else
           {
               lblInvoice_Value.ForeColor = System.Drawing.Color.Black;
           }
           if (DataBinder.Eval(e.Row.DataItem, "CURRENCY").ToString() != DataBinder.Eval(e.Row.DataItem, "Invoice_Currency").ToString())
           {
               e.Row.Cells[13].BackColor = System.Drawing.Color.Violet;
           }
           else
           {
               e.Row.Cells[13].BackColor = System.Drawing.Color.White;
           }
           if (DataBinder.Eval(e.Row.DataItem, "Supplier_Currency").ToString() != DataBinder.Eval(e.Row.DataItem, "CURRENCY").ToString())
           {
               if (DataBinder.Eval(e.Row.DataItem, "Supplier_Currency") != null)
               {
                   e.Row.Cells[4].BackColor = System.Drawing.Color.Violet;
               }
           }
           else
           {
               e.Row.Cells[4].BackColor = System.Drawing.Color.White;
           }
           if (DataBinder.Eval(e.Row.DataItem, "Urgent_Flag").ToString() == "URGENT")
           {
               lblUrgent.Visible = true;
               lblUrgentFlag.Visible = true;
           }
           else
           {
               lblUrgent.Visible = false;
               lblUrgentFlag.Visible = false;
           }
           int result = DateTime.Compare(PO_Date, Invoice_Date);
           if (result > 0)
           {
               e.Row.Cells[2].BackColor = System.Drawing.Color.Red;
               lblPO.ForeColor = System.Drawing.Color.White;
           }
           else
           {
               e.Row.Cells[1].BackColor = col;
           }
           DateTime TDate = System.DateTime.Now;
           int Diff = DateTime.Compare(Payment_Due_Date, TDate);
           if (Diff > 0)
           {
               System.TimeSpan diffResult = Payment_Due_Date - TDate;
               int differenceInDays = diffResult.Days;
               lblDays.Text = differenceInDays + " " + "Days";
               lblDays.ForeColor = System.Drawing.Color.Black;
               lblPaymentDate.ForeColor = System.Drawing.Color.Black;
           }
           else
           {
               lblPaymentDate.ForeColor = System.Drawing.Color.Red;
           }
           if (DataBinder.Eval(e.Row.DataItem, "Invoice_Status").ToString() == "Approved")
           {
               e.Row.Cells[11].BackColor = System.Drawing.Color.Green;
               lblInvoiceStatus.ForeColor = System.Drawing.Color.White;
               chkinvoice.Enabled = true;
               lblDispute.Text = "No Dispute";
               lblDispute.ForeColor = System.Drawing.Color.Green;
           }
           else if (DataBinder.Eval(e.Row.DataItem, "Dispute_Flag").ToString() == "YES")
           {
               e.Row.Cells[11].BackColor = System.Drawing.Color.White;
               lblInvoiceStatus.ForeColor = System.Drawing.Color.Black;
               chkinvoice.Enabled = false;
               btnDispute.Text = "Un-Dispute";
               lblDispute.Text = DataBinder.Eval(e.Row.DataItem, "Dispute_Flag").ToString();
               lblDispute.ForeColor = System.Drawing.Color.White;
               e.Row.Cells[18].BackColor = System.Drawing.Color.Red;
               btnApprove.Enabled = false;
           }
           else
           {
               e.Row.Cells[11].BackColor = System.Drawing.Color.White;
               lblInvoiceStatus.ForeColor = System.Drawing.Color.Black;
               lblDispute.Text = "No Dispute";
               lblDispute.ForeColor = System.Drawing.Color.Green;
               btnDispute.Text = "Dispute";
               chkinvoice.Enabled = true;
           }
           if (DataBinder.Eval(e.Row.DataItem, "Dispute_Flag").ToString() == "YES")
           {
               chkinvoice.Enabled = false;
               btnDispute.Text = "Un-Dispute";
               lblDispute.Text = DataBinder.Eval(e.Row.DataItem, "Dispute_Flag").ToString();
               lblDispute.ForeColor = System.Drawing.Color.White;
               e.Row.Cells[19].BackColor = System.Drawing.Color.Red;
           }
          
       }
    }
    protected void gvApprovedPayment_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            MergeGridviewHeader.SetProperty(objChangeReqstMerge);
            e.Row.SetRenderMethodDelegate(MergeGridviewHeader.RenderHeader);
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string ColorCode = DataBinder.Eval(e.Row.DataItem, "COLOR_CODE").ToString();
            System.Drawing.Color col = System.Drawing.ColorTranslator.FromHtml(ColorCode);
            DateTime Invoice_Date = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "Invoice_Date").ToString());
            DateTime PO_Date = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "Created_Date").ToString());
            //Button ViewRemarks = (Button)e.Row.FindControl("btnViewRemarks");
            LinkButton lblPO = (LinkButton)e.Row.FindControl("lblPOCode");
            Label lblUrgent = (Label)e.Row.FindControl("lblUrgent");
            Label lblUrgentFlag = (Label)e.Row.FindControl("lblUrgentFlag");
            Label lblDispute = (Label)e.Row.FindControl("lblDispute");
            Button btnDispute = (Button)e.Row.FindControl("btnDispute");
            int result = DateTime.Compare(PO_Date, Invoice_Date);
            if (result > 0)
            {
                e.Row.Cells[2].BackColor = System.Drawing.Color.Red;
                lblPO.ForeColor = System.Drawing.Color.White;
            }
            else
            {
                e.Row.Cells[1].BackColor = col;
            }
            if (DataBinder.Eval(e.Row.DataItem, "Urgent_Flag").ToString() == "URGENT")
            {
                lblUrgent.Visible = true;
                lblUrgentFlag.Visible = true;
            }
            else
            {
                lblUrgent.Visible = false;
                lblUrgentFlag.Visible = false;
            }
            if (DataBinder.Eval(e.Row.DataItem, "Dispute_Flag").ToString() == "YES")
            {
                
                btnDispute.Text = "Un-Dispute";
                lblDispute.Text = DataBinder.Eval(e.Row.DataItem, "Dispute_Flag").ToString();
                lblDispute.ForeColor = System.Drawing.Color.White;
                e.Row.Cells[19].BackColor = System.Drawing.Color.Red;
            }
            Label lblInvoice_Value = (Label)e.Row.FindControl("lblInvoice_Value");
            Label lblInvoice_Currency = (Label)e.Row.FindControl("lblInvoice_Currency");
            if (Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Invoice_Value")) > Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "PO_Amount")))
            {
                lblInvoice_Value.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                lblInvoice_Value.ForeColor = System.Drawing.Color.Black;
            }
            if (DataBinder.Eval(e.Row.DataItem, "CURRENCY").ToString() != DataBinder.Eval(e.Row.DataItem, "Invoice_Currency").ToString())
            {
                e.Row.Cells[13].BackColor = System.Drawing.Color.Violet;
            }
            else
            {
                e.Row.Cells[13].BackColor = System.Drawing.Color.White;
            }
            if (DataBinder.Eval(e.Row.DataItem, "Supplier_Currency") != DataBinder.Eval(e.Row.DataItem, "CURRENCY"))
            {
                if (DataBinder.Eval(e.Row.DataItem, "Supplier_Currency") != null)
                {
                    e.Row.Cells[4].BackColor = System.Drawing.Color.Violet;
                }
            }
            else
            {
                e.Row.Cells[4].BackColor = System.Drawing.Color.White;
            }
        }
    }
    protected void gvApprovedInvoice_Sorting(object sender, GridViewSortEventArgs se)
    {
        ViewState["SORTBYCOLOUMN"] = se.SortExpression;

        if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
            ViewState["SORTDIRECTION"] = 1;
        else
            ViewState["SORTDIRECTION"] = 0;
        BindInvoiceApprovedGrid();
    }
    protected void gvApprovedPayment_Sorting(object sender, GridViewSortEventArgs se)
    {
        ViewState["SORTBYCOLOUMN"] = se.SortExpression;

        if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
            ViewState["SORTDIRECTION"] = 1;
        else
            ViewState["SORTDIRECTION"] = 0;
        BindInvoiceApprovedGrid();
    }
    protected void btnClean_Click(object s, CommandEventArgs e)
    {
        string[] arg = e.CommandArgument.ToString().Split(',');
        string Amount_Status = null;
        String ID = UDFLib.ConvertStringToNull(arg[0]);
        if (ID == "1")
        {
            Amount_Status = "LESSTHANUSD1000";
        }
        else if (ID == "2")
        {
            Amount_Status = "BETWEENUSD1000ANDUSD10000";
        }
        else if (ID == "3")
        {
            Amount_Status = "BETWEENUSD10000ANDUSD25000";
        }
        else if (ID == "4")
        {
            Amount_Status = "GREATERTHANUSD25000";
        }
        ViewState["InvoiceWorkflow"] = "Clean" + "_" + Amount_Status;
        ViewState["InvoiceStatus"] = null;
        BindInvoiceApprovedGrid();
        //BindPaymentApprovedGrid();
        divApprovedPayment.Visible = false;
        dvWithhold.Visible = false;
    }
    protected void btnDirty_Click(object s, CommandEventArgs e)
    {
        string[] arg = e.CommandArgument.ToString().Split(',');
        string Amount_Status = null;
        String ID = UDFLib.ConvertStringToNull(arg[0]);
        if (ID == "1")
        {
            Amount_Status = "LESSTHANUSD1000";
        }
        else if (ID == "2")
        {
            Amount_Status = "BETWEENUSD1000ANDUSD10000";
        }
        else if (ID == "3")
        {
            Amount_Status = "BETWEENUSD10000ANDUSD25000";
        }
        else if (ID == "4")
        {
            Amount_Status = "GREATERTHANUSD25000";
        }
        ViewState["InvoiceWorkflow"] = "Dirty" + "_" + Amount_Status;
        BindInvoiceApprovedGrid();
        ViewState["InvoiceStatus"] = null;
        //BindPaymentApprovedGrid();
        divApprovedPayment.Visible = false;
        dvWithhold.Visible = false;
    }
    protected void btnDIRECTINV_Click(object s, CommandEventArgs e)
    {
        string[] arg = e.CommandArgument.ToString().Split(',');
        string Amount_Status = null;
        String ID = UDFLib.ConvertStringToNull(arg[0]);
        if (ID == "1")
        {
            Amount_Status = "LESSTHANUSD1000";
        }
        else if (ID == "2")
        {
            Amount_Status = "BETWEENUSD1000ANDUSD10000";
        }
        else if (ID == "3")
        {
            Amount_Status = "BETWEENUSD10000ANDUSD25000";
        }
        else if (ID == "4")
        {
            Amount_Status = "GREATERTHANUSD25000";
        }
        ViewState["InvoiceWorkflow"] = "DIRECTINV" + "_" + Amount_Status;
        BindInvoiceApprovedGrid();
        ViewState["InvoiceStatus"] = null;
        //BindPaymentApprovedGrid();
        divApprovedPayment.Visible = false;
        dvWithhold.Visible = false;
    }
    protected void btnLATEPO_Click(object s, CommandEventArgs e)
    {
        string[] arg = e.CommandArgument.ToString().Split(',');
        string Amount_Status = null;
        String ID = UDFLib.ConvertStringToNull(arg[0]);
        if (ID == "1")
        {
            Amount_Status = "LESSTHANUSD1000";
        }
        else if (ID == "2")
        {
            Amount_Status = "BETWEENUSD1000ANDUSD10000";
        }
        else if (ID == "3")
        {
            Amount_Status = "BETWEENUSD10000ANDUSD25000";
        }
        else if (ID == "4")
        {
            Amount_Status = "GREATERTHANUSD25000";
        }
        ViewState["InvoiceWorkflow"] = "LATEPO" + "_" + Amount_Status;
        BindInvoiceApprovedGrid();
        ViewState["InvoiceStatus"] = null;
        //BindPaymentApprovedGrid();
        divApprovedPayment.Visible = false;
        dvWithhold.Visible = false;
    }
    protected void btnREWORKIN_Click(object s, CommandEventArgs e)
    {
        string[] arg = e.CommandArgument.ToString().Split(',');
        string Amount_Status = null;
        String ID = UDFLib.ConvertStringToNull(arg[0]);
        if (ID == "1")
        {
            Amount_Status = "LESSTHANUSD1000";
        }
        else if (ID == "2")
        {
            Amount_Status = "BETWEENUSD1000ANDUSD10000";
        }
        else if (ID == "3")
        {
            Amount_Status = "BETWEENUSD10000ANDUSD25000";
        }
        else if (ID == "4")
        {
            Amount_Status = "GREATERTHANUSD25000";
        }
        ViewState["InvoiceWorkflow"] = "REWORKIN" + "_" + Amount_Status;
        BindInvoiceApprovedGrid();
        ViewState["InvoiceStatus"] = null;
        //BindPaymentApprovedGrid();
        divApprovedPayment.Visible = false;
        dvWithhold.Visible = false;
    }
    protected void btnREWORKOUT_Click(object s, CommandEventArgs e)
    {
        string[] arg = e.CommandArgument.ToString().Split(',');
        string Amount_Status = null;
        String ID = UDFLib.ConvertStringToNull(arg[0]);
        if (ID == "1")
        {
            Amount_Status = "LESSTHANUSD1000";
        }
        else if (ID == "2")
        {
            Amount_Status = "BETWEENUSD1000ANDUSD10000";
        }
        else if (ID == "3")
        {
            Amount_Status = "BETWEENUSD10000ANDUSD25000";
        }
        else if (ID == "4")
        {
            Amount_Status = "GREATERTHANUSD25000";
        }
        ViewState["InvoiceWorkflow"] = "REWORKOUT" + "_" + Amount_Status;
        BindInvoiceApprovedGrid();
        ViewState["InvoiceStatus"] = null;
        //BindPaymentApprovedGrid();
        divApprovedPayment.Visible = false;
        dvWithhold.Visible = false;
    }
    protected void btnOTHERS_Click(object s, CommandEventArgs e)
    {
        string[] arg = e.CommandArgument.ToString().Split(',');
        string Amount_Status = null;
        String ID = UDFLib.ConvertStringToNull(arg[0]);
        if (ID == "1")
        {
            Amount_Status = "LESSTHANUSD1000";
        }
        else if (ID == "2")
        {
            Amount_Status = "BETWEENUSD1000ANDUSD10000";
        }
        else if (ID == "3")
        {
            Amount_Status = "BETWEENUSD10000ANDUSD25000";
        }
        else if (ID == "4")
        {
            Amount_Status = "GREATERTHANUSD25000";
        }
        //ViewState["InvoiceAmt"] = UDFLib.ConvertStringToNull(arg[0]);
        ViewState["InvoiceWorkflow"] = "OTHERS" + "_" + Amount_Status;
        BindInvoiceApprovedGrid();
        ViewState["InvoiceStatus"] = null;
        //BindPaymentApprovedGrid();
        divApprovedPayment.Visible = false;
        dvWithhold.Visible = false;

    }
    protected void btnApprovedInvoice_Click(object sender, EventArgs e)
    {
        try
        {
            ViewState["PaymentStatus"] = "Approved";
            BindPaymentApprovedGrid();
            divApprovedInvoice.Visible = false;
            dvWithhold.Visible = false;
        }
        catch { }
        {
        }
    }
    protected void btnDisputedInvoice_Click(object sender, EventArgs e)
    {
        try
        {
            ViewState["InvoiceStatus"] = "Dispute";
            ViewState["InvoiceWorkflow"] = null;
            BindInvoiceApprovedGrid();
            divApprovedPayment.Visible = false;
            dvWithhold.Visible = false;
        }
        catch { }
        {
        }
    }
    protected void btnAdvanceRequest_Click(object sender, EventArgs e)
    {
        try
        {
            ViewState["InvoiceStatus"] = "Advance";
            ViewState["InvoiceWorkflow"] = null;
            BindInvoiceApprovedGrid();
            divApprovedPayment.Visible = false;
            dvWithhold.Visible = false;
        }
        catch { }
        {
        }
    }
    protected void btnPaymentPriority_Click(object sender, EventArgs e)
    {
        try
        {
            ViewState["InvoiceStatus"] = "PaymentPriority";
            ViewState["InvoiceWorkflow"] = null;
            BindInvoiceApprovedGrid();
            divApprovedPayment.Visible = false;
            dvWithhold.Visible = false;
        }
        catch { }
        {
        }
    }
    protected void btnWithHoldList_Click(object sender, EventArgs e)
    {
        try
        {
            ViewState["InvoiceStatus"] = "WithHold";
            //ViewState["InvoiceWorkflow"] = null;
            dvWithhold.Visible = true;
            divApprovedPayment.Visible = false;
            divApprovedInvoice.Visible = false;
            BindWithholdInvoice();
            
        }
        catch { }
        {
        }
    }
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        try
        {
            
            string InvStatus = "Approved";
            string Status = "Approved";
            Update_Payment(InvStatus, Status);
            String msg2 = String.Format("alert('Invoices  Approved.')");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg2, true);
            BindInvoiceApprovedGrid();
        }
        catch { }
        {
        }
    }
    protected void btnApproveAll_Click(object sender, EventArgs e)
    {
        try
        {
           
            string InvStatus = "Approved";
            string Status = "Approved";
            Update_Payment(InvStatus, Status);
            BindInvoiceApprovedGrid();
        }
        catch { }
        {
        }
    }
    protected void btnRework_Click(object sender, EventArgs e)
    {
        try
        {
            int Type = 1;
            string InvStatus = "Rework";
            string Status = "Rework";
            Update_Payment(InvStatus, Status);
            BindInvoiceApprovedGrid();
        }
        catch { }
        {
        }
    }
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        try
        {
            BindInvoiceApprovedGrid();
        }
        catch { }
        {
        }
    }
    protected void Update_Payment(string InvStatus, string Status)
    {
        

        DataTable dt = new DataTable();
        dt.Columns.Add("PKID");
        dt.Columns.Add("ChkValue");
        dt.Columns.Add("Invoice_ID");
        int i = 0;
        if (InvStatus == "Approved")
        {
            foreach (GridViewRow row in gvApprovedInvoice.Rows)
            {
                bool result = ((CheckBox)row.FindControl("chkInvoice")).Checked;
                if (result)
                {
                    DataRow dr = dt.NewRow();
                    dr["PKID"] = i + 1;
                    dr["ChkValue"] = 1;
                    dr["Invoice_ID"] = gvApprovedInvoice.DataKeys[row.RowIndex].Value.ToString();
                    dt.Rows.Add(dr);
                }

            }
        }
        else
        {
            foreach (GridViewRow row in gvApprovedPayment.Rows)
            {
                bool result = ((CheckBox)row.FindControl("chkInvoice")).Checked;
                if (result)
                {
                    DataRow dr = dt.NewRow();
                    dr["PKID"] = i + 1;
                    dr["ChkValue"] = 1;
                    dr["Invoice_ID"] = gvApprovedPayment.DataKeys[row.RowIndex].Value.ToString();
                    dt.Rows.Add(dr);
                }

            }
        }
        int Ret = BLL_POLOG_Register.POLog_Update_Payment_Invoice("0", InvStatus, Status, UDFLib.ConvertIntegerToNull(GetSessionUserID()),dt);
       // ViewState["PaymentStatus"] = "Approved";

      

        BindInvoiceCount();
        BindPaymentSchedule();

    }
    
    protected void btnUnApprove_Click(object sender, EventArgs e)
    {
        try
        {
            string InvStatus = "UNApproved";
            string Status = "UNApproved";
            Update_Payment(InvStatus, Status);
            String msg2 = String.Format("alert('Invoices  UN-Approved.')");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg2, true);
            BindPaymentApprovedGrid(); 
        }
        catch { }
        {
        }

    }
    protected void btnDispute_Click(object s, CommandEventArgs e)
    {
        string Status = "Dispute";
        String msg2 = null;
        string InvStatus = "Dispute";
        string[] arg = e.CommandArgument.ToString().Split(',');
        string InvoiceID = UDFLib.ConvertStringToNull(arg[0]);
        string Dispute_Flag = UDFLib.ConvertStringToNull(arg[1]);
        if (Dispute_Flag == "YES")
        {
            Status = "UnDispute";
            InvStatus = "UnDispute";
        }
        else
        {
            Status = "Dispute";
            InvStatus = "Dispute";
        }
        Update_Invoice(InvoiceID, Status, InvStatus);
        if (Status == "Dispute")
        {
            msg2 = String.Format("alert('Invoice  Disputed.')");
        }
        else
        {
            msg2 = String.Format("alert('Invoice  UnDisputed.')");
        }
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg2, true);
        BindInvoiceApprovedGrid();
        //BindPaymentApprovedGrid(); 
    }
    protected void Update_Invoice(string InvoiceID,string Status, string InvStatus)
    {
        try
        {
            int retval = BLL_POLOG_Register.POLog_Update_Invoice(UDFLib.ConvertStringToNull(InvoiceID), 0, UDFLib.ConvertIntegerToNull(0), InvStatus,
                                                                          Status, UDFLib.ConvertToInteger(GetSessionUserID()));
            BindInvoiceCount();
            BindPaymentSchedule();
            //BindInvoiceApprovedGrid();
            //BindPaymentApprovedGrid(); 
        }
        catch { }
        {
        }

    }
    protected void gvInviceCount_ItemDataBound(object sender, GridItemEventArgs e)
    {
        //foreach (GridDataItem dataItem in gvInviceCount.MasterTableView.Items)
        //{
        //    //TextBox txtLT = (TextBox)(dataItem.FindControl("txtRequest_Qty") as TextBox);
        //    //TextBox txtUNt = (TextBox)(dataItem.FindControl("txtUnitPrice") as TextBox);
        //    //TextBox txtDis = (TextBox)(dataItem.FindControl("txtDiscount") as TextBox);
        //    HiddenField lblID = (HiddenField)(dataItem.FindControl("lblID") as HiddenField);
        //    ImageButton btnDelete = (ImageButton)(dataItem.FindControl("ImgDelete") as ImageButton);
           

        //}

        if (e.Item is GridDataItem)
        {
            GridDataItem dataItem = e.Item as GridDataItem;
            if ((dataItem["Clean"].FindControl("btnClean") as Button).Text != "")
            {
                totalClean += int.Parse((dataItem["Clean"].FindControl("btnClean") as Button).Text);
            }
            if ((dataItem["Dirty"].FindControl("btnDirty") as Button).Text != "")
            {
                TotalDirty += int.Parse((dataItem["Dirty"].FindControl("btnDirty") as Button).Text);
            }
            if ((dataItem["DIRECTINV"].FindControl("btnDIRECTINV") as Button).Text != "")
            {
                TotalDirectInv += int.Parse((dataItem["DIRECTINV"].FindControl("btnDIRECTINV") as Button).Text);
            }
            if ((dataItem["LATEPO"].FindControl("btnLATEPO") as Button).Text != "")
            {
                TotalLatePO += int.Parse((dataItem["LATEPO"].FindControl("btnLATEPO") as Button).Text);
            }
            if ((dataItem["REWORKIN"].FindControl("btnREWORKIN") as Button).Text != "")
            {
                TotalReworkIN += int.Parse((dataItem["REWORKIN"].FindControl("btnREWORKIN") as Button).Text);
            }
            if ((dataItem["REWORKOUT"].FindControl("btnREWORKOUT") as Button).Text != "")
            {
                TotalReworkOut += int.Parse((dataItem["REWORKOUT"].FindControl("btnREWORKOUT") as Button).Text);
            }
            if ((dataItem["OTHERS"].FindControl("btnOTHERS") as Button).Text != "")
            {
                TotalOthers += int.Parse((dataItem["OTHERS"].FindControl("btnOTHERS") as Button).Text);
            }
            TotalInvoice = totalClean + TotalDirty + TotalDirectInv + TotalLatePO + TotalReworkIN + TotalReworkOut + TotalOthers;
        }
        else if (e.Item is GridFooterItem)
        {
            GridFooterItem footer = (GridFooterItem)e.Item;
            (footer["Clean"].FindControl("lblTotal_Clean") as Label).Text = totalClean.ToString();
            clientID = (footer["Clean"].FindControl("lblTotal_Clean") as Label).ClientID;

            (footer["Dirty"].FindControl("lblTotal_Dirty") as Label).Text = TotalDirty.ToString();
            clientID = (footer["Dirty"].FindControl("lblTotal_Dirty") as Label).ClientID;

            (footer["DIRECTINV"].FindControl("lblTotal_Direct") as Label).Text = TotalDirectInv.ToString();
            clientID = (footer["DIRECTINV"].FindControl("lblTotal_Direct") as Label).ClientID;

            (footer["LATEPO"].FindControl("lblTotal_LatePO") as Label).Text = TotalLatePO.ToString();
            clientID = (footer["LATEPO"].FindControl("lblTotal_LatePO") as Label).ClientID;

            (footer["REWORKIN"].FindControl("lblTotal_ReworkIN") as Label).Text = TotalReworkIN.ToString();
            clientID = (footer["REWORKIN"].FindControl("lblTotal_ReworkIN") as Label).ClientID;

            (footer["REWORKOUT"].FindControl("lblTotal_ReworkOUT") as Label).Text = TotalReworkOut.ToString();
            clientID = (footer["REWORKOUT"].FindControl("lblTotal_ReworkOUT") as Label).ClientID;

            (footer["OTHERS"].FindControl("lblTotal_Others") as Label).Text = TotalOthers.ToString();
            clientID = (footer["OTHERS"].FindControl("lblTotal_Others") as Label).ClientID;

            (footer["Report_Name"].FindControl("lblTotal_Invoice") as Label).Text = "Total" + "  " + TotalInvoice.ToString() + "  " + "Invoices";
            clientID = (footer["OTHERS"].FindControl("lblTotal_Others") as Label).ClientID;
        }
    }

    protected void BindWithholdInvoice()
    {
        try
        {
            int rowcount = ucCustomPager3.isCountRecord;
            string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
            int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());
            string InvoiceStatus = (ViewState["InvoiceStatus"] == null) ? null : (ViewState["InvoiceStatus"].ToString());

            DataSet ds = BLL_POLOG_Register.POLOG_Get_Withhold_Invoice_Search(UDFLib.ConvertStringToNull(ddlSupplier.SelectedValue),
                UDFLib.ConvertIntegerToNull(ddlVessel.SelectedValue), UDFLib.ConvertStringToNull(ddlOwner.SelectedValue), chkUrgent.Checked ? 1 : 0, InvoiceStatus,  UDFLib.ConvertIntegerToNull(GetSessionUserID()), sortbycoloumn, sortdirection
                             , ucCustomPager3.CurrentPageIndex, ucCustomPager3.PageSize, ref  rowcount);


            if (ucCustomPager3.isCountRecord == 1)
            {
                ucCustomPager3.CountTotalRec = rowcount.ToString();
                ucCustomPager3.BuildPager();
            }

            if (ds.Tables[0].Rows.Count > 0)
            {
                dvWithhold.Visible = true;
                gvPOWithhold.DataSource = ds.Tables[0];
                gvPOWithhold.DataBind();
                txtPOCode.Text = ds.Tables[0].Rows[0]["SUPPLY_ID"].ToString();
                BindInvoice(ds.Tables[0].Rows[0]["SUPPLY_ID"].ToString());
                gvPOWithhold.Rows[0].BackColor = System.Drawing.Color.Yellow;
            }
            else
            {
                dvWithhold.Visible = false;
                gvPOWithhold.DataSource = ds.Tables[0];
                gvPOWithhold.DataBind();
            }

        }
        catch { }
        {
        }
    }
    protected void btnPaymentDispute_Click(object s, CommandEventArgs e)
    {
        string Status = "Dispute";
        String msg2 = null;
        string InvStatus = "Dispute";
        string[] arg = e.CommandArgument.ToString().Split(',');
        string InvoiceID = UDFLib.ConvertStringToNull(arg[0]);
        string Dispute_Flag = UDFLib.ConvertStringToNull(arg[1]);
        if (Dispute_Flag == "YES")
        {
            Status = "UnDispute";
            InvStatus = "UnDispute";
        }
        else
        {
            Status = "Dispute";
            InvStatus = "Dispute";
        }
        Update_Invoice(InvoiceID, Status, InvStatus);
        if (Status == "Dispute")
        {
            msg2 = String.Format("alert('Invoice  Disputed.')");
        }
        else
        {
            msg2 = String.Format("alert('Invoice  UnDisputed.')");
        }
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg2, true);
        //BindInvoiceApprovedGrid();
        BindPaymentApprovedGrid(); 
    }
    protected void BindInvoice(string SupplyID)
    {
        objChangeReqstMerge.AddMergedColumns(new int[] { 0,1,2,3,4,5,6,7}, "Invoice Details", "HeaderStyle-center");
        objChangeReqstMerge.AddMergedColumns(new int[] { 8,9,10 }, "Payment", "HeaderStyle-center");
        DataTable dt = BLL_POLOG_Register.POLOG_Get_Invoice(UDFLib.ConvertIntegerToNull(SupplyID.ToString()));

        if (dt.Rows.Count > 0)
        {
            gvInvoiceWithhold.DataSource = dt;
            gvInvoiceWithhold.DataBind();
        }
        else
        {
            gvInvoiceWithhold.DataSource = dt;
            gvInvoiceWithhold.DataBind();
        }
    }
    protected void btnView_Click(object s, CommandEventArgs e)
    {
        string[] arg = e.CommandArgument.ToString().Split(',');
       string Supply_ID = UDFLib.ConvertStringToNull(arg[0]);
       txtPOCode.Text = UDFLib.ConvertStringToNull(arg[0]);
       txtInvoiceCurrency.Text = UDFLib.ConvertStringToNull(arg[1]);
       BindInvoice(Supply_ID);

       foreach (GridViewRow row in gvPOWithhold.Rows)
        {
            //storyGridView.DataKeys[row.RowIndex].Values[0].ToString();
            string ID = gvPOWithhold.DataKeys[row.RowIndex].Values[0].ToString();
            if (txtPOCode.Text == ID)
            {
                row.BackColor = System.Drawing.Color.Yellow;
            }
            else
            {
                row.BackColor = System.Drawing.Color.White;
            }
        }

    }
    protected void btnPayAmt_Click(object s, CommandEventArgs e)
    {
        
        string[] arg = e.CommandArgument.ToString().Split(',');
        string Supply_ID = UDFLib.ConvertStringToNull(arg[0]);
        txtPOCode.Text = UDFLib.ConvertStringToNull(arg[0]);
        txtInvoiceCurrency.Text = UDFLib.ConvertStringToNull(arg[1]);
        txtNetAmount.Text = UDFLib.ConvertStringToNull(arg[2]);
        BindInvoice(Supply_ID);

        this.SetFocus("ctl00_MainContent_txtAmount");
        OperationMode = "Pay Amount";
        ClearField();
        foreach (GridViewRow row in gvPOWithhold.Rows)
        {
            //storyGridView.DataKeys[row.RowIndex].Values[0].ToString();
            string ID = gvPOWithhold.DataKeys[row.RowIndex].Values[0].ToString();
            if (txtPOCode.Text == ID)
            {
                row.BackColor = System.Drawing.Color.Yellow;
            }
            else
            {
                row.BackColor = System.Drawing.Color.White;
            }
        }

        string PayAmount = String.Format("showModal('divaddAmount',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PayAmount", PayAmount, true);

    }
    protected void ClearField()
    {
        txtAmount.Text = "";
    }
    protected void btnPayAmount_Click(object sender, EventArgs e)
    {
        try
        {
            string Invoice_Type = "WITHHOLD";
            string Withhold_Mode = "UnWithHold";
            String msg2 = null;
            int retval = 0;

            if (UDFLib.ConvertDecimalToNull(txtNetAmount.Text) * -1 >= UDFLib.ConvertDecimalToNull(txtAmount.Text))
            {

                 retval = BLL_POLOG_Register.POLog_Insert_Withhold_Invoice(UDFLib.ConvertStringToNull(txtInvoiceCode.Text), UDFLib.ConvertIntegerToNull(txtPOCode.Text), 0, Invoice_Type
                                                                              , UDFLib.ConvertDecimalToNull(txtAmount.Text), "", UDFLib.ConvertToInteger(GetSessionUserID()), Withhold_Mode, txtInvoiceCurrency.Text);
            }
            else
            {
                msg2 = String.Format("alert('UnWithhold amount cant be greater then Withhold amount.')");
            }
            if (retval > 0)
            {
               
                BindWithholdInvoice();
                BindInvoice(txtPOCode.Text);
                foreach (GridViewRow row in gvPOWithhold.Rows)
                {
                    //storyGridView.DataKeys[row.RowIndex].Values[0].ToString();
                    string ID = gvPOWithhold.DataKeys[row.RowIndex].Values[0].ToString();
                    if (txtPOCode.Text == ID)
                    {
                        row.BackColor = System.Drawing.Color.Yellow;
                    }
                    else
                    {
                        row.BackColor = System.Drawing.Color.White;
                    }
                }
            }
            else
            {
                 msg2 = String.Format("alert('UnWithhold amount cant be greater then Invoice amount.')");
            }
           
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg2, true);
            string hidemodal = String.Format("hideModal('divadd')");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hidemodal", hidemodal, true);
        }
        catch { }
            {
            }
    }
    protected void gvPOWithhold_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            if (gvPOWithhold.Rows.Count > 0) 
                gvPOWithhold.Rows[0].RowState = DataControlRowState.Selected;
        }
    }
    protected void gvInvoiceWithhold_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            MergeGridviewHeader.SetProperty(objChangeReqstMerge);
            e.Row.SetRenderMethodDelegate(MergeGridviewHeader.RenderHeader);
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string Supply_ID = DataBinder.Eval(e.Row.DataItem, "Supply_ID").ToString();

            if (DataBinder.Eval(e.Row.DataItem, "Invoice_Type").ToString() == "WITHHOLD")
            {
                e.Row.BackColor = System.Drawing.Color.Yellow;
            }
        }
    }
}

