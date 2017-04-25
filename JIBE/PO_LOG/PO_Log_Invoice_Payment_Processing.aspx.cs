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

using SMS.Business.Infrastructure;
using SMS.Properties;
using SMS.Business.POLOG;


public partial class PO_LOG_PO_Log_Invoice_Payment_Processing : System.Web.UI.Page
{
    BLL_Infra_Currency objBLLCurrency = new BLL_Infra_Currency();
    BLL_Infra_VesselLib objBLL = new BLL_Infra_VesselLib();
    BLL_Infra_Port objBLLPort = new BLL_Infra_Port();
    UserAccess objUA = new UserAccess();
    public double total = 0.00;
    String Currency;
    public Boolean uaEditFlag = false;
    public Boolean uaDeleteFlage = false;
    MergeGridviewHeader_Info objChangeReqstMerge = new MergeGridviewHeader_Info();
    protected void Page_Load(object sender, EventArgs e)
    {
        UserAccessValidation();
        if (!IsPostBack)
        {
            BindCurrency();
            BindType();
            BindApprovedPaymentInvoice();
        }
    }
    protected void BindCurrency()
    {

        DataTable dt = objBLLCurrency.Get_CurrencyList();

        ddlCurrency.DataSource = dt;
        ddlCurrency.DataTextField = "Currency_Code";
        ddlCurrency.DataValueField = "Currency_Code";
        ddlCurrency.DataBind();
        ddlCurrency.Items.Insert(0, new ListItem("-SELECT-", "0"));

    }
   
    protected void BindApprovedPaymentInvoice()
    {
        try
        {
            int rowcount = ucCustomPagerItems.isCountRecord;
            DataTable dtType = new DataTable();
            objChangeReqstMerge.AddMergedColumns(new int[] { 4,5 }, "Payment Planned", "HeaderStyle-center");
            objChangeReqstMerge.AddMergedColumns(new int[] { 6,7 }, "Priority", "HeaderStyle-center");
            objChangeReqstMerge.AddMergedColumns(new int[] { 8,9 }, "Overdue", "HeaderStyle-center");
            objChangeReqstMerge.AddMergedColumns(new int[] { 10,11 }, "0 To 7 Days", "HeaderStyle-center");
            objChangeReqstMerge.AddMergedColumns(new int[] { 12,13 }, "8 to 14 Days", "HeaderStyle-center");
            objChangeReqstMerge.AddMergedColumns(new int[] { 14,15 }, "After 15 Days", "HeaderStyle-center");

            string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
            int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());
            Int16 AutoCheck = 0;
           if(chkAuto.Checked == true)
            {
                AutoCheck = 1;
            }
            else{
                AutoCheck = 0;
            }
            DataTable dt = BLL_POLOG_Register.POLOG_Get_Payment_Processing_Invoice_Search(UDFLib.ConvertStringToNull(ddlCurrency.SelectedValue), Convert.ToInt16(AutoCheck), UDFLib.ConvertIntegerToNull(GetSessionUserID()), sortbycoloumn, sortdirection
                             , ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);


            if (ucCustomPagerItems.isCountRecord == 1)
            {
                ucCustomPagerItems.CountTotalRec = rowcount.ToString();
                ucCustomPagerItems.BuildPager();
            }

            //if (dt.Rows.Count > 0)
            //{
                divApprovedinvoice.Visible = true;
                gvApprovedPaymentinvoice.DataSource = dt;
                gvApprovedPaymentinvoice.DataBind();
                gvApprovedPaymentinvoice.Rows[0].BackColor = System.Drawing.Color.Yellow;
                BindSupplierInvoice(dt.Rows[0]["Supplier_Code"].ToString());
                BindSupplierDetails(dt.Rows[0]["Supplier_Code"].ToString());
                txtSupplierCode.Text = dt.Rows[0]["Supplier_Code"].ToString();
            //}
            //else
            //{
            //    divApprovedinvoice.Visible = false;
            //    gvApprovedPaymentinvoice.DataSource = dt;
            //    gvApprovedPaymentinvoice.DataBind();
            //}
        }
        catch { }
        {
        }
    }
    protected void BindSupplierInvoice(string Supplier_Code)
    {
        string PayMode = null;
        DataSet ds = BLL_POLOG_Register.POLOG_Get_Payment_Supplier_Invoice(Supplier_Code,PayMode, GetSessionUserID());
        if (ds.Tables[0].Rows.Count > 0)
        {
            gvPaymentDetails.DataSource = ds;
            gvPaymentDetails.DataBind();
            gvApprovedInvoice.DataSource = ds;
            gvApprovedInvoice.DataBind();
        }
        if (ds.Tables[1].Rows.Count > 0)
        {
            txtSupplierName.Text = ds.Tables[1].Rows[0]["Supplier_Name"].ToString();
            txtPaymentTerms.Text = ds.Tables[1].Rows[0]["Payment_Terms"].ToString();
            txtAddress.Text = ds.Tables[1].Rows[0]["Address"].ToString();
            txtPayment.Text = ds.Tables[1].Rows[0]["Payment_Instructions"].ToString();
            txtCity.Text = ds.Tables[1].Rows[0]["City"].ToString();
            txtpaymentnotification.Text = ds.Tables[1].Rows[0]["Payment_Notifications"].ToString();
            txtEmail.Text = ds.Tables[1].Rows[0]["Email"].ToString();
            txtPhone.Text = ds.Tables[1].Rows[0]["Phone"].ToString();
            txtFax.Text = ds.Tables[1].Rows[0]["Fax"].ToString();
           
        }
        if (ds.Tables[2].Rows.Count > 0)
        {
            gvNewPayment.DataSource = ds.Tables[2];
            gvNewPayment.DataBind();
            if (ds.Tables[2].Rows[0]["PAYMENT_STATUS"].ToString() == "OPEN")
            {
                gvNewPayment.Rows[0].BackColor = System.Drawing.Color.Yellow;
                 txtPaymodeID.Text= ds.Tables[2].Rows[0]["PAYMENT_ID"].ToString();
                 txtPayment_Year.Text = ds.Tables[2].Rows[0]["PAYMENT_YEAR"].ToString();
                GetPaymentDetails(txtPaymodeID.Text,txtPayment_Year.Text);
                btnUpdate.Enabled = true;
                btnlink.Enabled = true;
            }
        }

    }
    protected void PaymentDet()
    {
        DataSet ds = BLL_POLOG_Register.POLOG_Get_Payment_Details(txtPaymodeID.Text.ToString(), txtSupplierCode.Text.ToString(), UDFLib.ConvertIntegerToNull(txtPayment_Year.Text), GetSessionUserID());
        if (ds.Tables[0].Rows.Count > 0)
        {
            txtPaymentAmount.Text = ds.Tables[0].Rows[0]["PAYMENT_AMOUNT"].ToString();
            txtBankRef.Text = ds.Tables[0].Rows[0]["BANK_REFERENCE"].ToString();
            txtPayDate.Text = ds.Tables[0].Rows[0]["PAYMENT_DATE"].ToString();
            ddlPayMode.SelectedValue = ds.Tables[0].Rows[0]["PAYMENT_MODE"].ToString();
            rdbPaymode.SelectedValue = ds.Tables[0].Rows[0]["Payment_Status"].ToString();
            ddlAccount.Text = ds.Tables[0].Rows[0]["Bank_Account_ID"].ToString();
            txtBankAmt.Text = ds.Tables[0].Rows[0]["Bank_Amount"].ToString();
            txtBankCharge.Text = ds.Tables[0].Rows[0]["Bank_Charges"].ToString();
            txtRemarks.Text = ds.Tables[0].Rows[0]["PAYMENT_REMARKS"].ToString();
            txtJournal.Text = ds.Tables[0].Rows[0]["Journal_ID"].ToString();
            txtPaymodeID.Text = ds.Tables[0].Rows[0]["PAYMENT_ID"].ToString();
            txtPayment_Year.Text = ds.Tables[0].Rows[0]["PAYMENT_YEAR"].ToString();

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

    protected void BindType()
    {
        DataSet ds = BLL_POLOG_Register.POLOG_Get_Type(UDFLib.ConvertToInteger(GetSessionUserID()), "Payment_Mode");

        ddlPayMode.DataSource = ds.Tables[11];
        ddlPayMode.DataTextField = "VARIABLE_NAME";
        ddlPayMode.DataValueField = "VARIABLE_CODE";
        ddlPayMode.DataBind();
        ddlPayMode.Items.Insert(0, new ListItem("-Select-", "0"));

        rdbPaymode.DataSource = ds.Tables[12];
        rdbPaymode.DataTextField = "VARIABLE_NAME";
        rdbPaymode.DataValueField = "VARIABLE_CODE";
        rdbPaymode.DataBind();
        rdbPaymode.Items.Insert(0, new ListItem("CANCEL", "CANCEL"));

        ddlAccount.DataSource = ds.Tables[13];
        ddlAccount.DataTextField = "Account_Name";
        ddlAccount.DataValueField = "Account_ID";
        ddlAccount.DataBind();
        ddlAccount.Items.Insert(0, new ListItem("-Select-", "0"));
    }
    protected void btnGet_Click(object sender, EventArgs e)
    {
        BindApprovedPaymentInvoice();
    }
    protected void gvApprovedPaymentinvoice_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            MergeGridviewHeader.SetProperty(objChangeReqstMerge);
            e.Row.SetRenderMethodDelegate(MergeGridviewHeader.RenderHeader);
        }
         if (e.Row.RowType == DataControlRowType.DataRow)
         {
             DateTime Last_Payment_Date;
            // string ColorCode = DataBinder.Eval(e.Row.DataItem, "COLOR_CODE").ToString();
             //System.Drawing.Color col = System.Drawing.ColorTranslator.FromHtml(ColorCode);
             string PaymentDate = DataBinder.Eval(e.Row.DataItem, "Last_Payment_Date").ToString();
             Label lblLastPayment_Date = (Label)e.Row.FindControl("lblLPayment");
             if (PaymentDate != "" && PaymentDate != null )
             {
                  Last_Payment_Date = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "Last_Payment_Date").ToString());
                  int result = DateTime.Compare(Last_Payment_Date, System.DateTime.Now);
                  if (result > 0)
                  {
                      e.Row.Cells[2].BackColor = System.Drawing.Color.Green;
                      lblLastPayment_Date.ForeColor = System.Drawing.Color.White;
                  }
                  else
                  {
                      e.Row.Cells[2].BackColor = System.Drawing.Color.Red;
                      lblLastPayment_Date.ForeColor = System.Drawing.Color.White;

                  }
             }
             else
             {
                 Last_Payment_Date = System.DateTime.Now;
             }
             //DateTime PO_Date = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "Last_Payment_Date").ToString());
             //Button ViewRemarks = (Button)e.Row.FindControl("btnViewRemarks");
           
             
  
         }
    }
    protected void gvApprovedPaymentinvoice_Sorting(object sender, GridViewSortEventArgs se)
    {
        ViewState["SORTBYCOLOUMN"] = se.SortExpression;

        if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
            ViewState["SORTDIRECTION"] = 1;
        else
            ViewState["SORTDIRECTION"] = 0;
        BindApprovedPaymentInvoice();
    }
    protected void gvPaymentDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //ImageButton ImgStatus = (ImageButton)e.Row.FindControl("ImgView");
            Label lblStatus = (Label)e.Row.FindControl("lblUrgency");
            Currency = DataBinder.Eval(e.Row.DataItem, "Invoice_Currency").ToString();
            CheckBox chk = (CheckBox)e.Row.FindControl("chkInvoice");
            if (DataBinder.Eval(e.Row.DataItem, "Urgency").ToString() == "ASAP")
            {
                lblStatus.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                lblStatus.ForeColor = System.Drawing.Color.Black;
            }
            if (DataBinder.Eval(e.Row.DataItem, "Payment_Status").ToString() == "OPEN")
            {
                chk.Checked = true;
                e.Row.BackColor = System.Drawing.Color.Yellow;
                //chk.Enabled = false;
            }
            else
            {
                chk.Checked = false;
                e.Row.BackColor = System.Drawing.Color.White;
                //chk.Enabled = true;
            }
            Label Invoice_Value = (Label)e.Row.FindControl("lblInvoice_Value");
            double InvoiceValue = Convert.ToDouble(Invoice_Value.Text);
            total = total + InvoiceValue;
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[5].Text = total.ToString();
            e.Row.Cells[6].Text = Currency;
        }

    }

    protected void BindSupplierDetails(string Supplier_Code)
    {
        string PayMode = null;
        DataSet ds = BLL_POLOG_Register.POLOG_Get_Payment_Supplier_Invoice(Supplier_Code, PayMode, GetSessionUserID());
        //if (ds.Tables[0].Rows.Count > 0)
        //{
        //    gvPaymentDetails.DataSource = ds.Tables[0];
        //    gvPaymentDetails.DataBind();
            //if (ds.Tables[0].Rows[0]["PAYMENT_STATUS"].ToString() == "OPEN")
            //{
            //    txtPaymodeID.Text = ds.Tables[0].Rows[0]["PAYMENT_ID"].ToString();
            //    txtPayment_Year.Text = ds.Tables[0].Rows[0]["PAYMENT_YEAR"].ToString();
            //    GetPaymentDetails();
            //}
        //}
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        try
        {
            btnUpdate.Enabled = true;
            btnlink.Enabled = true;
            GetPaymentDetails();
        }
        catch { }
        {
        }
    }
    protected void GetPaymentDetails()
    {
        //string Payment_ID = null;
        //int Payment_Year = 0;
        //string Supplier_Code = null;
        DataSet ds = BLL_POLOG_Register.POLOG_Get_Payment_Details(txtPaymodeID.Text.ToString(), txtSupplierCode.Text.ToString(), UDFLib.ConvertIntegerToNull(txtPayment_Year.Text), GetSessionUserID());
        if (ds.Tables[0].Rows.Count > 0)
        {
            txtPaymentAmount.Text = ds.Tables[0].Rows[0]["PAYMENT_AMOUNT"].ToString();
            txtBankRef.Text = ds.Tables[0].Rows[0]["BANK_REFERENCE"].ToString();
            txtPayDate.Text = ds.Tables[0].Rows[0]["PAYMENT_DATE"].ToString();
            ddlPayMode.SelectedValue = ds.Tables[0].Rows[0]["PAYMENT_MODE"].ToString();
            rdbPaymode.SelectedValue = ds.Tables[0].Rows[0]["Payment_Status"].ToString();
            ddlAccount.Text = ds.Tables[0].Rows[0]["Bank_Account_ID"].ToString();
            txtBankAmt.Text = ds.Tables[0].Rows[0]["Bank_Amount"].ToString();
            txtBankCharge.Text = ds.Tables[0].Rows[0]["Bank_Charges"].ToString();
            txtRemarks.Text = ds.Tables[0].Rows[0]["PAYMENT_REMARKS"].ToString();
            txtJournal.Text = ds.Tables[0].Rows[0]["Journal_ID"].ToString();
            txtPaymodeID.Text = ds.Tables[0].Rows[0]["PAYMENT_ID"].ToString();
            txtPayment_Year.Text = ds.Tables[0].Rows[0]["PAYMENT_YEAR"].ToString();
          
        }
        if (ds.Tables[1].Rows.Count > 0)
        {
            gvNewPayment.DataSource = ds.Tables[1];
            gvNewPayment.DataBind();
        }
        BindLinkInvoice(txtPaymodeID.Text, UDFLib.ConvertToInteger(txtPayment_Year.Text));
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            string Retval = BLL_POLOG_Register.POLOG_Insert_Payment_Details(txtPaymodeID.Text, UDFLib.ConvertToInteger(txtPayment_Year.Text), txtSupplierCode.Text, UDFLib.ConvertDecimalToNull(txtPaymentAmount.Text), txtBankRef.Text,
                                                                          UDFLib.ConvertDateToNull(txtPayDate.Text), ddlPayMode.SelectedValue, ddlAccount.SelectedValue,
                                                                          UDFLib.ConvertDecimalToNull(txtBankAmt.Text), UDFLib.ConvertDecimalToNull(txtBankCharge.Text), rdbPaymode.SelectedValue, txtRemarks.Text, GetSessionUserID());
            GetPaymentDetails();
        }
        catch { }
            {
            }
    }
    protected void btnlink_Click(object sender, EventArgs e)
    {
        try
        {

            DataTable dt = new DataTable();
            dt.Columns.Add("PKID");
            dt.Columns.Add("ChkValue");
            dt.Columns.Add("Invoice_ID");
            int i = 0;

            foreach (GridViewRow row in gvPaymentDetails.Rows)
            {
                bool result = ((CheckBox)row.FindControl("chkInvoice")).Checked;
                if (result)
                {
                    DataRow dr = dt.NewRow();
                    dr["PKID"] = i + 1;
                    dr["ChkValue"] = 1;
                    dr["Invoice_ID"] = gvPaymentDetails.DataKeys[row.RowIndex].Value.ToString();
                    dt.Rows.Add(dr);
                }

            }
            string Retval = BLL_POLOG_Register.POLOG_Link_Payment_Invoice(txtPaymodeID.Text, txtSupplierCode.Text, UDFLib.ConvertToInteger(txtPayment_Year.Text), dt, GetSessionUserID());
            //DataSet dt = BLL_POLOG_Register.POLOG_Link_Payment_Invoice(Invoice_ID, PayMode, GetSessionUserID());
            //if (dt.Tables[0].Rows.Count > 0)
            //{
            //    gvPaymentDetails.DataSource = dt;
            //    gvPaymentDetails.DataBind();
            //}
            BindLinkInvoice(txtPaymodeID.Text, UDFLib.ConvertToInteger(txtPayment_Year.Text));
        }
        catch { }
        {
        }
    }
    protected void btnView_Click(object sender, CommandEventArgs e)
    {
        string[] arg = e.CommandArgument.ToString().Split(',');
        txtSupplierCode.Text = UDFLib.ConvertStringToNull(arg[0]);
        //txtInvoiceCode.Text = UDFLib.ConvertStringToNull(arg[1]);
        //string InvoiceStatus = UDFLib.ConvertStringToNull(arg[1]);


        BindSupplierInvoice(txtSupplierCode.Text);
        BindSupplierDetails(txtSupplierCode.Text);

        foreach (GridViewRow row in gvApprovedPaymentinvoice.Rows)
        {
            String ID = gvApprovedPaymentinvoice.DataKeys[row.RowIndex].Values[0].ToString();
            if (txtSupplierCode.Text == ID)
            {
                row.BackColor = System.Drawing.Color.Yellow;
            }
            else
            {
                row.BackColor = System.Drawing.Color.White;
            }
        }


    }
    protected void ImgPaymentView_Click(object sender, CommandEventArgs e)
    {
        try
        {
            string[] arg = e.CommandArgument.ToString().Split(',');
            txtPaymodeID.Text = UDFLib.ConvertStringToNull(arg[0]);
            txtPayment_Year.Text = UDFLib.ConvertStringToNull(arg[1]);
            btnUpdate.Enabled = true;
            btnlink.Enabled = true;
            GetPaymentDetails(txtPaymodeID.Text, txtPayment_Year.Text);
        }
        catch { }
        {
        }


    }
    protected void GetPaymentDetails(string Payment_ID, string Payment_Year)
    {
        DataSet ds = BLL_POLOG_Register.POLOG_Get_Payment_Details(Payment_ID, txtSupplierCode.Text.ToString(), UDFLib.ConvertIntegerToNull(Payment_Year), GetSessionUserID());
        if (ds.Tables[0].Rows.Count > 0)
        {
            btnUpdate.Enabled = true;
            txtPaymentAmount.Text = ds.Tables[0].Rows[0]["PAYMENT_AMOUNT"].ToString();
            txtBankRef.Text = ds.Tables[0].Rows[0]["BANK_REFERENCE"].ToString();
            txtPayDate.Text = ds.Tables[0].Rows[0]["PAYMENT_DATE"].ToString();
            ddlPayMode.SelectedValue = ds.Tables[0].Rows[0]["PAYMENT_MODE"].ToString();
            rdbPaymode.SelectedValue = ds.Tables[0].Rows[0]["Payment_Status"].ToString();
            ddlAccount.Text = ds.Tables[0].Rows[0]["Bank_Account_ID"].ToString();
            txtBankAmt.Text = ds.Tables[0].Rows[0]["Bank_Amount"].ToString();
            txtBankCharge.Text = ds.Tables[0].Rows[0]["Bank_Charges"].ToString();
            txtRemarks.Text = ds.Tables[0].Rows[0]["PAYMENT_REMARKS"].ToString();
            txtJournal.Text = ds.Tables[0].Rows[0]["Journal_ID"].ToString();
            txtPaymodeID.Text = ds.Tables[0].Rows[0]["PAYMENT_ID"].ToString();
            txtPayment_Year.Text = ds.Tables[0].Rows[0]["PAYMENT_YEAR"].ToString();
            lblTotalAmount.Text = ds.Tables[0].Rows[0]["PAYMENT_AMOUNT"].ToString();
            lblCurrency.Text = ds.Tables[0].Rows[0]["PAYMENT_CURRENCY"].ToString();
            lblPaymentID.Text = ds.Tables[0].Rows[0]["PAYMENT_ID"].ToString();
            lblSupplierName.Text = ds.Tables[0].Rows[0]["Supplier_Name"].ToString();
            if (rdbPaymode.SelectedValue == "PAID")
            {
                gvInvoice.Columns[4].Visible = false;
            }
            
        }
        if (ds.Tables[2].Rows.Count > 0)
        {
            gvInvoice.DataSource = ds.Tables[2];
            gvInvoice.DataBind();
        }
    }
    protected void BindLinkInvoice(string Payment_ID,int Payment_Year)
    {

        DataSet ds = BLL_POLOG_Register.POLOG_Get_Payment_Details(Payment_ID, txtSupplierCode.Text.ToString(), UDFLib.ConvertIntegerToNull(Payment_Year), GetSessionUserID());
        if (ds.Tables[2].Rows.Count > 0)
        {
            gvInvoice.DataSource = ds.Tables[2];
            gvInvoice.DataBind();
        }
    }
    protected void btnDelete_Click(object sender, CommandEventArgs e)
    {
        string[] arg = e.CommandArgument.ToString().Split(',');
        string Retval = BLL_POLOG_Register.POLOG_Delete_Payment_Invoice(UDFLib.ConvertStringToNull(arg[0]), txtSupplierCode.Text.ToString(), UDFLib.ConvertIntegerToNull(arg[1]), GetSessionUserID());

    }
    protected void gvInvoice_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ImageButton btnRemove = (ImageButton)e.Row.FindControl("btnDelete");
            if (rdbPaymode.SelectedValue == "PAID")
            {
                btnRemove.Visible = false;
            }
            
        }
    }
}