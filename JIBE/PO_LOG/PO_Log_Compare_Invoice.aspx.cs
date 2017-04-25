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
using SMS.Business.ASL;
using SMS.Business.Infrastructure;
using SMS.Properties;
using SMS.Business.POLOG;
using System.IO;


public partial class PO_LOG_PO_Log_Compare_Invoice : System.Web.UI.Page
{
    BLL_Infra_UserCredentials objBLLUser = new BLL_Infra_UserCredentials();
    UserAccess objUA = new UserAccess();
    public string AmendType = null;
    public string GeneralType = null;
    public string GreenType = null;
    public string YellowType = null;
    public string RedType = null;
    public Boolean uaEditFlag = true;
    public Boolean uaDeleteFlage = true;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["PageName"] != null)
            {
                if (Request.QueryString["PageName"] == "InvoiceApproval")
                {
                    dvWithhold.Visible = false;
                    btnPaymentApprove.Visible = false;
                    btnInvoiceApprove.Visible = true;
                    btnPaymentApprove.Visible = false;
                    btnFinalApprove.Visible = false;
                }
                else if (Request.QueryString["PageName"] == "PaymentApproval")
                {
                    dvWithhold.Visible = true;
                    btnInvoiceApprove.Visible = false;
                    btnPaymentApprove.Visible = true;
                    btnFinalApprove.Visible = false;
                }
                else if (Request.QueryString["PageName"] == "FinalApproval")
                {
                    dvWithhold.Visible = false;
                    btnInvoiceApprove.Visible = false;
                    btnPaymentApprove.Visible = false;
                    btnFinalApprove.Visible = true;
                }
                else
                {
                    dvWithhold.Visible = false;
                    btnInvoiceApprove.Visible = false;
                    btnPaymentApprove.Visible = false;
                    btnFinalApprove.Visible = false;
                }
            }
            else
            {
                dvWithhold.Visible = false;

            }
            if (Request.QueryString["Invoice_ID"] != null)
            {
                txtPOCode.Text = Request.QueryString["Supply_ID"].ToString();
                txtInvoiceCode.Text = Request.QueryString["Invoice_ID"].ToString();
                BindUser();
                BindRemarks(UDFLib.ConvertStringToNull(txtInvoiceCode.Text));
                BindGrid();
                BindDuplicateInvoice(UDFLib.ConvertStringToNull(txtInvoiceCode.Text));
                BindDeliveryGrid(UDFLib.ConvertStringToNull(txtInvoiceCode.Text));
            }
           
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
        //if (objUA.Edit == 1)
        //    uaEditFlag = true;
        //else
        //    // btnsave.Visible = false;

        //    if (objUA.Delete == 1) uaDeleteFlage = true;

    }
    protected void BindUser()
    {
        try
        {
            DataTable dt = objBLLUser.Get_UserList_ApprovalLimit();
            ddlRework.DataSource = dt;
            ddlRework.DataTextField = "UserName";
            ddlRework.DataValueField = "UserID";
            ddlRework.DataBind();
            ddlRework.Items.Insert(0, new ListItem("-Select-", "0"));
        }
        catch { }
        {
        }
    }
    protected void BindRemarks(string InvoiceID)
    {
        try
        {
            DataSet ds = BLL_POLOG_Register.POLOG_Get_Remarks_ByInvoiceID(InvoiceID);
            gvRemarks.DataSource = ds.Tables[0];
            gvRemarks.DataBind();
        }
        catch { }
            {
            }
    }
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }
    public string GetSuppID()
    {
        try
        {

            if (Request.QueryString["Supp_ID"] != null)
            {
                return Request.QueryString["Supp_ID"].ToString();
            }

            else
                return "0";
        }
        catch { return "0"; }
    }
    protected void btnRemarks_Click(object sender, EventArgs e)
    {
        String Remarks_Type = "Invoice";
        int RetValue = BLL_POLOG_Register.POLOG_Insert_Remarks(UDFLib.ConvertIntegerToNull(txtRemarksID.Text), UDFLib.ConvertIntegerToNull(txtInvoiceCode.Text),
            txtRemarks.Text.Trim(), Remarks_Type, UDFLib.ConvertToInteger(Session["UserID"].ToString()));
        txtRemarks.Text = "";
        BindRemarks(UDFLib.ConvertStringToNull(txtInvoiceCode.Text));
    }
    //protected void SaveRemarks()
    //{
    //    ChkType();
    //    //string SuppID = UDFLib.ConvertStringToNull(Request.QueryString["Supp_ID"]);
    //    int RetValue = BLL_Invoice.POLOG_INS_Remarks(UDFLib.ConvertToInteger(ViewState["ReturnSupplierID"]), UDFLib.ConvertIntegerToNull(txtPOID.Text), AmendType, GeneralType, GreenType, YellowType, RedType, txtRemarks.Text,
    //          UDFLib.ConvertToInteger(Session["UserID"].ToString()));
    //    BindRemarks();
    //    ClearControl();
    //}
    protected void ClearControl()
    {
        txtRemarks.Text = "";
        ViewState["ReturnSupplierID"] = 0;
        btnRemarks.Text = "Add Remarks";

    }
    protected void btnGeneral_Click(object s, CommandEventArgs e)
    {
        string[] arg = e.CommandArgument.ToString().Split(',');
        int? RemarksID = UDFLib.ConvertIntegerToNull(arg[0]);
        string Remarks_Action = "General";
        int RetValue = BLL_POLOG_Register.POLOG_Update_Remarks(UDFLib.ConvertIntegerToNull(RemarksID), UDFLib.ConvertIntegerToNull(txtInvoiceCode.Text), Remarks_Action, UDFLib.ConvertToInteger(Session["UserID"].ToString()));
        BindRemarks(UDFLib.ConvertStringToNull(txtInvoiceCode.Text));
    }
    protected void btnWarning_Click(object s, CommandEventArgs e)
    {
        string[] arg = e.CommandArgument.ToString().Split(',');
        int? RemarksID = UDFLib.ConvertIntegerToNull(arg[0]);
        string Remarks_Action = "Yellow Card";
        int RetValue = BLL_POLOG_Register.POLOG_Update_Remarks(UDFLib.ConvertIntegerToNull(RemarksID), UDFLib.ConvertIntegerToNull(txtInvoiceCode.Text), Remarks_Action, UDFLib.ConvertToInteger(Session["UserID"].ToString()));
        BindRemarks(UDFLib.ConvertStringToNull(txtInvoiceCode.Text));

    }
    protected void btnRed_Click(object s, CommandEventArgs e)
    {
        string[] arg = e.CommandArgument.ToString().Split(',');
        int? RemarksID = UDFLib.ConvertIntegerToNull(arg[0]);
        string Remarks_Action = "Red Card";
        int RetValue = BLL_POLOG_Register.POLOG_Update_Remarks(UDFLib.ConvertIntegerToNull(RemarksID), UDFLib.ConvertIntegerToNull(txtInvoiceCode.Text), Remarks_Action, UDFLib.ConvertToInteger(Session["UserID"].ToString()));
        BindRemarks(UDFLib.ConvertStringToNull(txtInvoiceCode.Text));
    }
    protected void BindGrid()
    {
        DataTable dt = BLL_POLOG_Register.POLOG_Get_Invoice(UDFLib.ConvertIntegerToNull(txtPOCode.Text.ToString()));

        if (dt.Rows.Count > 0)
        {
            divPendingPO.Visible = true;
            gvInvoice.DataSource = dt;
            gvInvoice.DataBind();
            BindAttachment(UDFLib.ConvertStringToNull(dt.Rows[0]["Invoice_ID"].ToString()));
            BindPoPreview(UDFLib.ConvertStringToNull(dt.Rows[0]["Supply_ID"].ToString()));
        }
        else
        {
            divPendingPO.Visible = false;
            gvInvoice.DataSource = dt;
            gvInvoice.DataBind();
        }
    }
    protected void BindAttachment(string Invoice_ID)
    {
        try
        {
            string InvoiceType = "Invoice";
            DataSet ds = BLL_POLOG_Invoice.POLOG_Get_Invoice_Attachments(Invoice_ID, InvoiceType);
            if (ds.Tables[0].Rows.Count > 0)
            {
                //string FilePath = ds.Tables[0].Rows[0]["FILE_NAME"].ToString();
                string SavePath = ("../Uploads/Files_Uploaded");
                string File_ID = ds.Tables[0].Rows[0]["Id"].ToString(); 
                //string filePath = "../Uploads/POLog/" + ds.Tables[0].Rows[0]["FILE_NAME"].ToString();
                File_ID = File_ID.PadLeft(8, '0');
                string F1 = Mid(File_ID, 0, 2);
                string F2 = Mid(File_ID, 2, 2);
                string F3 = Mid(File_ID, 4, 2);
               // string filePath = SavePath + "/" + F1 + "/" + F2 + "/" + F3 + "/" + ds.Tables[0].Rows[0]["File_Path"].ToString();
                string filePath = SavePath + "\\" + F1 + "\\" + F2 + "\\" + F3 + "\\" + File_ID + "" + Path.GetExtension(ds.Tables[0].Rows[0]["FILE_NAME"].ToString());
                if (filePath != "")
                {
                    Random r = new Random();
                    string ver = r.Next().ToString();
                    IframeInvoice.Attributes.Add("src", filePath + "?ver=" + ver);
                }
            }
            if (ds.Tables[1].Rows.Count > 0)
            {
                lblPO.Text = ds.Tables[1].Rows[0]["Po_Remarks"].ToString();
            }
            if (ds.Tables[2].Rows.Count > 0)
            {
                lblInvoice.Text = ds.Tables[2].Rows[0]["Invoice_Remarks"].ToString();
            }
        }
        catch { }
        {
        }
    }
    public static string Mid(string param, int startIndex, int length)
    {
        //start at the specified index in the string ang get N number of
        //characters depending on the lenght and assign it to a variable
        string result = param.Substring(startIndex, length);
        //return the result of the operation
        return result;
    }
    protected void BindPoPreview(string Supply_ID)
    {
        //string FilePath = "../PO_LOG/PO_Log_Preview.aspx";
        //this.IframePO.Attributes["src"] = "../PO_LOG/PO_Log_Preview.aspx?SUPPLY_ID=" + Supply_ID + "";
        this.IframePO.Attributes["src"] = "PO_Log_Preview.aspx?SUPPLY_ID=" + Supply_ID + "";
        //IframePO.Attributes.Add("src", FilePath + "?SUPPLY_ID=" + Supply_ID);
    }
    protected void btnView_Click(object sender, CommandEventArgs e)
    {
        string[] arg = e.CommandArgument.ToString().Split(',');
        txtInvoiceCode.Text = UDFLib.ConvertStringToNull(arg[0]);
        string InvoiceStatus = UDFLib.ConvertStringToNull(arg[1]);
        string PaymentStatus = UDFLib.ConvertStringToNull(arg[2]);
        string Dispute_Flag = UDFLib.ConvertStringToNull(arg[3]);
        BindAttachment(UDFLib.ConvertStringToNull(txtInvoiceCode.Text));
        BindRemarks(UDFLib.ConvertStringToNull(txtInvoiceCode.Text));
        //BindGrid(InvoiceID);
        BindDuplicateInvoice(UDFLib.ConvertStringToNull(txtInvoiceCode.Text));
        BindDeliveryGrid(UDFLib.ConvertStringToNull(txtInvoiceCode.Text));
        if (Dispute_Flag == "YES")
        {
            btnDispute.Text = "Un-Dispute";
        }
        else
        {
            btnDispute.Text = "Dispute";
        }
        if (InvoiceStatus == "Verified")
        {
            btnInvoiceApprove.Enabled = true;
            btnInvoiceApprove.Text = "Approve Invoice";
            if (Dispute_Flag == "YES")
            {
                btnInvoiceApprove.Enabled = false;
            }
        }
        else if (InvoiceStatus == "Approved")
        {
            if (Request.QueryString["PageName"] == "FinalApproval")
            {
                btnInvoiceApprove.Visible = false;
                btnPaymentApprove.Visible = false;
                btnFinalApprove.Visible = true;
                if (PaymentStatus == "")
                {
                    btnFinalApprove.Visible = true;
                    btnFinalApprove.Text = "Final UnApproval";
                }
                if (Dispute_Flag == "YES")
                {
                    btnFinalApprove.Enabled = false;
                }
                else
                {
                    btnFinalApprove.Enabled = true;
                }
            }
            else if (Request.QueryString["PageName"] == "PaymentApproval")
            {
                if (PaymentStatus == "")
                {
                    btnPaymentApprove.Text = "Approve Payment";
                    dvWithhold.Visible = true;
                }
                else if (PaymentStatus == "Approved")
                {
                    btnPaymentApprove.Text = "Un-Approve Payment";
                    dvWithhold.Visible = false;
                }
                if (Dispute_Flag == "YES")
                {
                    btnPaymentApprove.Enabled = false;
                }
                else
                {
                    btnPaymentApprove.Enabled = true;
                }
            }
            else if (Request.QueryString["PageName"] == "InvoiceApproval")
            {
                btnInvoiceApprove.Text = "UnApprove Invoice";
                if (PaymentStatus == "")
                {
                    btnInvoiceApprove.Enabled = true;
                }
                else
                {
                    btnInvoiceApprove.Enabled = false;
                }
                if (Dispute_Flag == "YES")
                {
                    btnInvoiceApprove.Enabled = false;
                }
                else
                {
                    btnInvoiceApprove.Enabled = true;
                }
            }

        }
        else
        {
            btnDispute.Enabled = true;
            btnInvoiceApprove.Enabled = false;
        }
        
        foreach (GridViewRow row in gvInvoice.Rows)
        {
            String ID = gvInvoice.DataKeys[row.RowIndex].Values[0].ToString();
            if (txtInvoiceCode.Text == ID)
            {
                row.BackColor = System.Drawing.Color.Yellow;
            }
            else
            {
                row.BackColor = System.Drawing.Color.White;
            }
        }
        

    }
    protected void BindDuplicateInvoice(string InvoiceID)
    {
        try
        {
            DataTable dt = BLL_POLOG_Register.POLOG_Get_DuplicateInvoice(InvoiceID);

            if (dt.Rows.Count > 0)
            {
                lblDuplicateInvoice.Text = "Duplicated invoice Reference Found";
                divDuplicateInvoice.Visible = true;
                gvDuplieCateinvoice.DataSource = dt;
                gvDuplieCateinvoice.DataBind();
            }
            else
            {
                divDuplicateInvoice.Visible = false;
                gvDuplieCateinvoice.DataSource = dt;
                gvDuplieCateinvoice.DataBind();
            }
        }
        catch { }
        {
        }
    }
    protected void BindDeliveryGrid(string InvoiceID)
    {
        try
        {
            DataTable dt = BLL_POLOG_Register.POLOG_Get_Delivery_Invoice(InvoiceID);

            if (dt.Rows.Count > 0)
            {
                lblDelivery.Text = "Confirmed deliveries record Found.";
                divDeliveryDetails.Visible = true;
                gvDeliveryDetails.DataSource = dt;
                gvDeliveryDetails.DataBind();
            }
            else
            {
                lblDelivery.Visible = true;
                lblDelivery.Text = "No confirmed deliveries matched to this invoice.";
                divDeliveryDetails.Visible = false;
                gvDeliveryDetails.DataSource = dt;
                gvDeliveryDetails.DataBind();
            }

        }
        catch { }
        {
        }
    }
    protected void gvRemarks_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //string ColorCode = DataBinder.Eval(e.Row.DataItem, "COLOR_CODE").ToString();
            //e.Row.BackColor = System.Drawing.Color.Yellow;
            string Invoice_ID = DataBinder.Eval(e.Row.DataItem, "Invoice_ID").ToString();
            string Supply_ID = DataBinder.Eval(e.Row.DataItem, "Supply_ID").ToString();
          
            if (Invoice_ID != "")
            {
                e.Row.BackColor = System.Drawing.Color.Yellow;
            }
            else
            {
                e.Row.BackColor = System.Drawing.Color.White;
            }
           
            string GeneralType = DataBinder.Eval(e.Row.DataItem, "GeneralType").ToString();
            string YellowType = DataBinder.Eval(e.Row.DataItem, "YellowType").ToString();
            string RedType = DataBinder.Eval(e.Row.DataItem, "RedType").ToString();
            Button btnGeneral = (Button)e.Row.FindControl("btnGeneral");
            Button btnWarning = (Button)e.Row.FindControl("btnWarning");
            Button btnRed = (Button)e.Row.FindControl("btnRed");
            if (GeneralType != "")
            {
                btnGeneral.BackColor = System.Drawing.Color.Blue;
            }
            else
            {
                btnGeneral.BackColor = System.Drawing.Color.White;
            }
            if (YellowType != "")
            {
                btnWarning.BackColor = System.Drawing.Color.Orange;
            }
            else
            {
                btnWarning.BackColor = System.Drawing.Color.White;
            }
            if (RedType != "")
            {
                btnRed.BackColor = System.Drawing.Color.Red;
            }
            else
            {
                btnRed.BackColor = System.Drawing.Color.White;
            }
        }
    }
    protected void gvInvoice_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string ID = DataBinder.Eval(e.Row.DataItem, "Invoice_ID").ToString();
            Label lblInvoiceStatus = (Label)e.Row.FindControl("lblInvoice_Status");
            Label lblPayment_Approved_Date = (Label)e.Row.FindControl("lblPayment_Approved_Date");
            Label lblDispute = (Label)e.Row.FindControl("lblDispute_Status");
          
            if (DataBinder.Eval(e.Row.DataItem, "Invoice_Status").ToString() == "Approved")
            {
                e.Row.Cells[6].BackColor = System.Drawing.Color.Green;
                lblInvoiceStatus.ForeColor = System.Drawing.Color.White;
            }
            else
            {
                //e.Row.Cells[6].BackColor = System.Drawing.Color.White;
                lblInvoiceStatus.ForeColor = System.Drawing.Color.Black;
            }
            if (DataBinder.Eval(e.Row.DataItem, "Dispute_Flag").ToString() == "YES")
            {
                e.Row.Cells[11].BackColor = System.Drawing.Color.Red;
                lblDispute.ForeColor = System.Drawing.Color.White;
            }
            if (ID == txtInvoiceCode.Text)
            {
                e.Row.BackColor = System.Drawing.Color.Yellow;
                if (DataBinder.Eval(e.Row.DataItem, "Urgent_Flag").ToString() == "YES")
                {
                    btnUrgent.ForeColor = System.Drawing.Color.Red;
                    btnUrgent.Text = "Not Urgent";
                }
                else
                {
                    btnUrgent.ForeColor = System.Drawing.Color.Blue;
                    btnUrgent.Text = "Urgent";
                }
                if (DataBinder.Eval(e.Row.DataItem, "Dispute_Flag").ToString() == "YES")
                {
                    btnDispute.Text = "Un-Dispute";
                }
                else
                {
                    btnDispute.Text = "Dispute";
                }
                if (DataBinder.Eval(e.Row.DataItem, "Invoice_Status").ToString() == "Verified")
                {
                    btnInvoiceApprove.Enabled = true;
                    btnInvoiceApprove.Text = "Approve Invoice";
                    if (DataBinder.Eval(e.Row.DataItem, "Dispute_Flag").ToString() == "YES")
                    {
                        btnInvoiceApprove.Enabled = false;
                    }
                }
                else if (DataBinder.Eval(e.Row.DataItem, "Invoice_Status").ToString() == "Approved")
                {
                    if (Request.QueryString["PageName"] == "FinalApproval")
                    {
                        btnInvoiceApprove.Visible = false;
                        btnPaymentApprove.Visible = false;
                        btnFinalApprove.Visible = true;
                        if (DataBinder.Eval(e.Row.DataItem, "Payment_Status").ToString() == "")
                        {
                            btnFinalApprove.Visible = true;
                            btnFinalApprove.Text = "Final UnApproval";
                        }
                        if (DataBinder.Eval(e.Row.DataItem, "Dispute_Flag").ToString() == "YES")
                        {
                            btnFinalApprove.Enabled = false;
                        }
                        else
                        {
                            btnFinalApprove.Enabled = true;
                        }
                    }
                    else if (Request.QueryString["PageName"] == "PaymentApproval")
                    {
                        if (DataBinder.Eval(e.Row.DataItem, "Payment_Status").ToString() == "")
                        {
                            btnPaymentApprove.Text = "Approve Payment";
                            dvWithhold.Visible = true;
                        }
                        else if (DataBinder.Eval(e.Row.DataItem, "Payment_Status").ToString() == "Approved")
                        {
                            btnPaymentApprove.Text = "Un-Approve Payment";
                            dvWithhold.Visible = false;
                        }
                        if (DataBinder.Eval(e.Row.DataItem, "Dispute_Flag").ToString() == "YES")
                        {
                            btnPaymentApprove.Enabled = false;
                        }
                        else
                        {
                            btnPaymentApprove.Enabled = true;
                        }
                    }
                    else if (Request.QueryString["PageName"] == "InvoiceApproval")
                    {
                        btnInvoiceApprove.Text = "UnApprove Invoice";
                        if (DataBinder.Eval(e.Row.DataItem, "Payment_Status").ToString() == "")
                        {
                            btnInvoiceApprove.Enabled = true;
                        }
                        else
                        {
                            btnInvoiceApprove.Enabled = false;
                        }
                        if (DataBinder.Eval(e.Row.DataItem, "Dispute_Flag").ToString() == "YES")
                        {
                            btnInvoiceApprove.Enabled = false;
                        }
                        else
                        {
                            btnInvoiceApprove.Enabled = true;
                        }
                    }
                }
                else
                {
                   // e.Row.Cells[8].BackColor = System.Drawing.Color.White;
                    lblPayment_Approved_Date.ForeColor = System.Drawing.Color.Black;
                    btnInvoiceApprove.Enabled = false;
                }
            }
            
        }
    }
    
    protected void btnRework_Click(object sender, EventArgs e)
    {
        string Status = "Rework";
        string InvStatus = "Rework";
        Update_Invoice(Status, InvStatus);
        BindGrid();

        int RetAuditVal = BLL_POLOG_Register.POLOG_Insert_Transactionlog(UDFLib.ConvertStringToNull(txtInvoiceCode.Text), "Rework Invoice", "ReworkInvoice", UDFLib.ConvertToInteger(GetSessionUserID()));
        //int retval = BLL_Invoice.POLog_Update_Invoice(UDFLib.ConvertIntegerToNull(txtInvoiceCode.Text), 0, InvStatus,
        //                                                          Status, UDFLib.ConvertToInteger(GetSessionUserID()));
    }
    protected void Update_Invoice(string Status,string InvStatus)
    {
        try
        {
            int retval = BLL_POLOG_Register.POLog_Update_Invoice(UDFLib.ConvertStringToNull(txtInvoiceCode.Text), 0, UDFLib.ConvertIntegerToNull(ddlRework.SelectedValue), InvStatus,
                                                                          Status, UDFLib.ConvertToInteger(GetSessionUserID()));
        }
        catch{}
        {
        }
       
    }
  
    protected void btnUrgent_Click(object sender, EventArgs e)
    {
        string Status = null;
        string InvStatus = null;
        if (btnUrgent.Text == "Urgent")
        {
            Status = "Urgent";
            InvStatus = "Urgent";
            int RetAuditVal = BLL_POLOG_Register.POLOG_Insert_Transactionlog(UDFLib.ConvertStringToNull(txtInvoiceCode.Text), "NON Urgent Invoice", "UNUrgentInvoice", UDFLib.ConvertToInteger(GetSessionUserID()));
        }
        else
        {
            Status = "UNUrgent";
            InvStatus = "UNUrgent";
            int RetAuditVal = BLL_POLOG_Register.POLOG_Insert_Transactionlog(UDFLib.ConvertStringToNull(txtInvoiceCode.Text), "Urgent Invoice", "UrgentInvoice", UDFLib.ConvertToInteger(GetSessionUserID()));
        }
        Update_Invoice(Status, InvStatus);
        BindGrid();
    }
    protected void btnDispute_Click(object sender, EventArgs e)
    {
        try
        {
            String msg2 = null;
            string Status = "";
            string InvStatus = "";
            if (btnDispute.Text == "Dispute")
            {
                Status = "Dispute";
                InvStatus = "Dispute";
                int RetAuditVal = BLL_POLOG_Register.POLOG_Insert_Transactionlog(UDFLib.ConvertStringToNull(txtInvoiceCode.Text), "Dispute Invoice", "DisputeInvoice", UDFLib.ConvertToInteger(GetSessionUserID()));
            }
            else if (btnDispute.Text == "Un-Dispute")
            {
                Status = "UnDispute";
                InvStatus = "UnDispute";
                int RetAuditVal = BLL_POLOG_Register.POLOG_Insert_Transactionlog(UDFLib.ConvertStringToNull(txtInvoiceCode.Text), "NON Dispute Invoice", "UnDisputeInvoice", UDFLib.ConvertToInteger(GetSessionUserID()));
            }

            Update_Invoice(Status, InvStatus);
            if (Status == "Dispute")
            {
                msg2 = String.Format("alert('Invoice  Disputed.')");
            }
            else
            {
                msg2 = String.Format("alert('Invoice  UnDisputed.')");
            }
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg2, true);
            BindGrid();
        }
        catch { }
        {
        }
    }
    protected void btnWithhold_Click(object sender, EventArgs e)
    {
        try
        {
            string Invoice_Type = "WITHHOLD";
            string Withhold_Mode = "WITHHOLD";
            String msg2 = null;
            int retval = BLL_POLOG_Register.POLog_Insert_Withhold_Invoice(UDFLib.ConvertStringToNull(txtInvoiceCode.Text), UDFLib.ConvertIntegerToNull(txtPOCode.Text), 0, Invoice_Type
                                                                          , UDFLib.ConvertDecimalToNull(txtAmount.Text), txtReason.Text, UDFLib.ConvertToInteger(GetSessionUserID()), Withhold_Mode,null);
            if (retval > 0)
            {
                txtReason.Text = "";
                txtAmount.Text = "";
                int RetAuditVal = BLL_POLOG_Register.POLOG_Insert_Transactionlog(UDFLib.ConvertStringToNull(txtInvoiceCode.Text), "Withhold Invoice", "WithholdInvoice", UDFLib.ConvertToInteger(GetSessionUserID()));
                 msg2 = String.Format("alert('Invoice amount Submitted for Withhold.')");
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg2, true);
                BindGrid();
            }
            else
            {
                msg2 = String.Format("alert('Withhold amount Cant be more then Invoice amount.')");
            }
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg2, true);
        }
        catch{}
        {
        }

        
    }
    protected void btnExit_Click(object sender, EventArgs e)
    {
        string msgDraft = String.Format("parent.ReloadParent_ByButtonID();");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgDraft", msgDraft, true);
    }
    protected void btnInvoiceApprove_Click(object sender, EventArgs e)
    {
        try
        {
            string Status = "";
            string InvStatus = "";
            if (btnInvoiceApprove.Text == "Approve Invoice")
            {
                Status = "Approved";
                InvStatus = "Approved";
                int RetAuditVal = BLL_POLOG_Register.POLOG_Insert_Transactionlog(UDFLib.ConvertStringToNull(txtInvoiceCode.Text), "Approved Invoice", "ApprovedInvoice", UDFLib.ConvertToInteger(GetSessionUserID()));
            }
            else if (btnInvoiceApprove.Text == "Un-Approve Invoice")
            {
                Status = "UnApproved";
                InvStatus = "UnApproved";
                int RetAuditVal = BLL_POLOG_Register.POLOG_Insert_Transactionlog(UDFLib.ConvertStringToNull(txtInvoiceCode.Text), "UNApproved Invoice", "UNApprovedInvoice", UDFLib.ConvertToInteger(GetSessionUserID()));
            }
            Update_Invoice(Status, InvStatus);
            BindGrid();

        }
        catch { }
        {
        }
    }
    protected void btnPaymentApprove_Click(object sender, EventArgs e)
    {
        try
        {
            string Status = "";
            string InvStatus = "";
            int Type = 1;
            if (btnPaymentApprove.Text == "Approve Payment")
            {
                Status = "Approved";
                InvStatus = "Approved";
                int RetAuditVal = BLL_POLOG_Register.POLOG_Insert_Transactionlog(UDFLib.ConvertStringToNull(txtInvoiceCode.Text), "Approved Payment", "ApprovedPayment", UDFLib.ConvertToInteger(GetSessionUserID()));
            }
            else if (btnPaymentApprove.Text == "Un-Approve Payment")
            {
                Status = "UnApproved";
                InvStatus = "UnApproved";
                int RetAuditVal = BLL_POLOG_Register.POLOG_Insert_Transactionlog(UDFLib.ConvertStringToNull(txtInvoiceCode.Text), "UNApproved Payment", "UNApprovedPayment", UDFLib.ConvertToInteger(GetSessionUserID()));
            }
            
            Update_Payment(InvStatus, Status);
            BindGrid();

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
       
        DataRow dr = dt.NewRow();
        dr["PKID"] = i + 1;
        dr["ChkValue"] = 1;
        dr["Invoice_ID"] = txtInvoiceCode.Text.ToString();
        dt.Rows.Add(dr);
        int Ret = BLL_POLOG_Register.POLog_Update_Payment_Invoice("0", InvStatus, Status, UDFLib.ConvertIntegerToNull(GetSessionUserID()), dt);         
     
    }
    protected void btnFinalApprove_Click(object sender, EventArgs e)
    {
        try
        {
            string Status = "";
            string InvStatus = "";
            if (btnPaymentApprove.Text == "Final Approve")
            {
                Status = "Approved";
                InvStatus = "Approved";
                int RetAuditVal = BLL_POLOG_Register.POLOG_Insert_Transactionlog(UDFLib.ConvertStringToNull(txtInvoiceCode.Text), "Final Approval", "FinalApproval", UDFLib.ConvertToInteger(GetSessionUserID()));
            }
            else if (btnPaymentApprove.Text == "Final Un-Approve")
            {
                Status = "UnApproved";
                InvStatus = "UnApproved";
                int RetAuditVal = BLL_POLOG_Register.POLOG_Insert_Transactionlog(UDFLib.ConvertStringToNull(txtInvoiceCode.Text), "Final UNApproval", "UNFinalApproval", UDFLib.ConvertToInteger(GetSessionUserID()));
            }

            Update_Final_Invoice(InvStatus, Status);
            BindGrid();

        }
        catch { }
        {
        }
       
    }
    protected void Update_Final_Invoice(string InvStatus, string Status)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("PKID");
        dt.Columns.Add("ChkValue");
        dt.Columns.Add("Invoice_ID");
        int i = 0;

        DataRow dr = dt.NewRow();
        dr["PKID"] = i + 1;
        dr["ChkValue"] = 1;
        dr["Invoice_ID"] = txtInvoiceCode.Text.ToString();
        dt.Rows.Add(dr);
        int Ret = BLL_POLOG_Register.POLog_Update_Invoice(dt, InvStatus, Status, UDFLib.ConvertToInteger(GetSessionUserID()));
        //int retval = BLL_POLOG_Register.POLog_Update_Invoice(dt, InvStatus, Status, UDFLib.ConvertToInteger(GetSessionUserID()));
    }
   
}