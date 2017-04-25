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

public partial class Invoice_Final_Invoice_Approver : System.Web.UI.Page
{
    UserAccess objUA = new UserAccess();
    public string OperationMode = "";
    MergeGridviewHeader_Info objChangeReqstMerge = new MergeGridviewHeader_Info();
    //MergeGridviewHeader_Payment objChangeReqstMerge1 = new MergeGridviewHeader_Payment();
    public Boolean uaEditFlag = false;
    public Boolean uaDeleteFlage = false;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        UserAccessValidation();
        if (!IsPostBack)
        {
            Load_dropdownlist();
            BindType();
            BindPendingInvoiceGrid();
            divApprovedPayment.Visible = false;
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
      
    }
    protected DataTable ChkType()
    {

        DataTable dtType = new DataTable();
        dtType.Columns.Add("PKID");
        dtType.Columns.Add("FKID");
        dtType.Columns.Add("Value");
        foreach (ListItem chkitem in chkType.Items)
        {

            DataRow dr = dtType.NewRow();
            if (chkitem.Selected == true)
            {
                dr["FKID"] = chkitem.Selected == true ? 1 : 0;
                dr["Value"] = chkitem.Value;
                dtType.Rows.Add(dr);
            }

        }

        return dtType;
    }
    protected void BindPendingInvoiceGrid()
    {
        try
        {
            objChangeReqstMerge.AddMergedColumns(new int[] { 2, 3, 4 }, "PO", "HeaderStyle-center");
            objChangeReqstMerge.AddMergedColumns(new int[] { 5, 6, 7, 8, 9, 10, 11, 12, 13 }, "Invoice", "HeaderStyle-center");
            objChangeReqstMerge.AddMergedColumns(new int[] { 14, 15 }, "Invoice Approved", "HeaderStyle-center");

            int rowcount = ucCustomPager1.isCountRecord;
            string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
            int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());
            DataTable dtType = ChkType();
            string InvoiceStatus = null;

            DataSet ds = BLL_POLOG_Invoice.POLOG_Get_Final_Invoice_Search(UDFLib.ConvertStringToNull(ddlSupplier.SelectedValue),
                                    UDFLib.ConvertIntegerToNull(ddlVessel.SelectedValue), InvoiceStatus, dtType, UDFLib.ConvertIntegerToNull(GetSessionUserID()), sortbycoloumn, sortdirection
                            , ucCustomPager1.CurrentPageIndex, ucCustomPager1.PageSize, ref  rowcount);


            if (ucCustomPager1.isCountRecord == 1)
            {
                ucCustomPager1.CountTotalRec = rowcount.ToString();
                ucCustomPager1.BuildPager();
            }

            if (ds.Tables[0].Rows.Count > 0)
            {
                divApprovedPayment.Visible = false;
                divPendingInvoice.Visible = true;
                btnApprove.Visible = true;
                gvPendingInvoice.DataSource = ds.Tables[0];
                gvPendingInvoice.DataBind();
            }
            else
            {
                divApprovedPayment.Visible = false;
                divPendingInvoice.Visible = false;
                gvPendingInvoice.DataSource = ds.Tables[0];
                gvPendingInvoice.DataBind();
            }

        }
        catch { }
        {
        }
    }
    protected void BindApprovedInvoiceGrid()
    {
        try
        {
            int rowcount = ucCustomPager2.isCountRecord;
            objChangeReqstMerge.AddMergedColumns(new int[] { 2, 3, 4 }, "PO", "HeaderStyle-center");
            objChangeReqstMerge.AddMergedColumns(new int[] { 5, 6, 7, 8, 9, 10, 11, 12,13 }, "Invoice", "HeaderStyle-center");
            objChangeReqstMerge.AddMergedColumns(new int[] { 14, 15 }, "Final Approver", "HeaderStyle-center");

            DataTable dtType = ChkType();
            string InvoiceStatus = "Approved";


            string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
            int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());
            string InvoiceAmt = (ViewState["InvoiceAmt"] == null) ? null : (ViewState["InvoiceAmt"].ToString());

            DataSet ds = BLL_POLOG_Invoice.POLOG_Get_Final_Invoice_Search(UDFLib.ConvertStringToNull(ddlSupplier.SelectedValue),
                UDFLib.ConvertIntegerToNull(ddlVessel.SelectedValue), InvoiceStatus, dtType, UDFLib.ConvertIntegerToNull(GetSessionUserID()), sortbycoloumn, sortdirection
                             , ucCustomPager2.CurrentPageIndex, ucCustomPager2.PageSize, ref  rowcount);


            if (ucCustomPager2.isCountRecord == 1)
            {
                ucCustomPager2.CountTotalRec = rowcount.ToString();
                ucCustomPager2.BuildPager();
            }
            if (ds.Tables[0].Rows.Count > 0)
            {
                btnUnApprove.Visible = true;
                divApprovedPayment.Visible = true;
                divPendingInvoice.Visible = false;
                gvApprovedInvoice.DataSource = ds.Tables[0];
                gvApprovedInvoice.DataBind();
            }
            else
            {
                divApprovedPayment.Visible = false;
                btnUnApprove.Visible = false;
                divPendingInvoice.Visible = false;
                gvApprovedInvoice.DataSource = ds.Tables[0];
                gvApprovedInvoice.DataBind();
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

    protected void gvPendingInvoice_RowDataBound(object sender, GridViewRowEventArgs e)
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
                e.Row.Cells[19].BackColor = System.Drawing.Color.Red;
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
            //Button ViewRemarks = (Button)e.Row.FindControl("btnViewRemarks");
            LinkButton lblPO = (LinkButton)e.Row.FindControl("lblPOCode");
            Label lblUrgent = (Label)e.Row.FindControl("lblUrgent");
            Label lblUrgentFlag = (Label)e.Row.FindControl("lblUrgentFlag");
            Label lblDispute = (Label)e.Row.FindControl("lblDispute");
            Button btnDispute = (Button)e.Row.FindControl("btnApprovedDispute");
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
    protected void gvPendingInvoice_Sorting(object sender, GridViewSortEventArgs se)
    {
        ViewState["SORTBYCOLOUMN"] = se.SortExpression;

        if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
            ViewState["SORTDIRECTION"] = 1;
        else
            ViewState["SORTDIRECTION"] = 0;
        BindPendingInvoiceGrid();
    }
    protected void gvApprovedInvoice_Sorting(object sender, GridViewSortEventArgs se)
    {
        ViewState["SORTBYCOLOUMN"] = se.SortExpression;

        if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
            ViewState["SORTDIRECTION"] = 1;
        else
            ViewState["SORTDIRECTION"] = 0;
        BindPendingInvoiceGrid();
    }
   
    
   
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        try
        {
            string InvStatus = "Approved";
            string Status = "Approved";
            Update_Invoice(InvStatus, Status);
            String msg2 = String.Format("alert('Invoices  Approved.')");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg2, true);
            BindPendingInvoiceGrid();
        }
        catch { }
        {
        }
    }

    protected void Update_Invoice(string InvStatus, string Status)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("PKID");
        dt.Columns.Add("ChkValue");
        dt.Columns.Add("Invoice_ID");
        int i = 0;
        if (InvStatus == "Approved")
        {
            foreach (GridViewRow row in gvPendingInvoice.Rows)
            {
                bool result = ((CheckBox)row.FindControl("chkInvoice")).Checked;
                if (result)
                {
                    DataRow dr = dt.NewRow();
                    dr["PKID"] = i + 1;
                    dr["ChkValue"] = 1;
                    dr["Invoice_ID"] = gvPendingInvoice.DataKeys[row.RowIndex].Value.ToString();
                    dt.Rows.Add(dr);
                }

            }
        }
        else
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
        int retval = BLL_POLOG_Register.POLog_Update_Invoice(dt, InvStatus, Status, UDFLib.ConvertToInteger(GetSessionUserID()));
        
        
    }

    protected void btnUnApprove_Click(object sender, EventArgs e)
    {
        try
        {
            string InvStatus = "UNApproved";
            string Status = "UNApproved";
            Update_Invoice(InvStatus, Status);
            String msg2 = String.Format("alert('Invoices  UN-Approved.')");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg2, true);
            BindApprovedInvoiceGrid();
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
        BindPendingInvoiceGrid();
        //BindPaymentApprovedGrid(); 
    }
    protected void Update_Invoice(string InvoiceID, string Status, string InvStatus)
    {
        try
        {
            int retval = BLL_POLOG_Register.POLog_Update_Invoice(UDFLib.ConvertStringToNull(InvoiceID), 0, UDFLib.ConvertIntegerToNull(0), InvStatus,
                                                                          Status, UDFLib.ConvertToInteger(GetSessionUserID()));
            //BindPendingInvoiceGrid();
         
        }
        catch { }
        {
        }

    }

    protected void btnApprovedDispute_Click(object s, CommandEventArgs e)
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
        BindApprovedInvoiceGrid(); ;
       
    }

    protected void btnApprovedInvoice_Click1(object sender, EventArgs e)
    {
        BindApprovedInvoiceGrid();
    }
    protected void btnPendingInvoice_Click(object sender, EventArgs e)
    {
        BindPendingInvoiceGrid();
    }
}