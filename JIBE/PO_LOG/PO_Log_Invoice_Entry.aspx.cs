using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.Infrastructure;
using SMS.Business.POLOG;
using System.IO;
using SMS.Properties;
using EO.Pdf;
using System.Drawing;
using ClsBLLTechnical;
using SMS.Business.Crew;
using SMS.Business.Infrastructure;
using System.Text;
using System.Xml.Linq;
using Telerik.Web.UI;
using System.Web.Caching;

public partial class PO_LOG_PO_Log_Invoice_Entry : System.Web.UI.Page
{
    BLL_Infra_Currency objBLLCurrency = new BLL_Infra_Currency();
    int i = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!String.IsNullOrEmpty(Request.QueryString["Invoice_ID"].ToString()) && !String.IsNullOrEmpty(Request.QueryString["SUPPLY_ID"].ToString()))
            {
                txtInvoiceID.Text = Request.QueryString["Invoice_ID"].ToString();
                InsertAuditTrail();
                BindType();
                BindCurrencyDLL();
                BindPODetails();
                BindInvoice();
                BindReqsnAttchment();
                //BindDuplicateInvoice(UDFLib.ConvertStringToNull(txtInvoiceID.Text));
                //BindDeliveryGrid(UDFLib.ConvertStringToNull(txtInvoiceID.Text));
            }
        }
    }
    protected void InsertAuditTrail()
    {
        try
        {
            string Action = "View";
            string Description = "ViewInvoice";
            int RetAuditVal = BLL_POLOG_Register.POLOG_Insert_Transactionlog(UDFLib.ConvertStringToNull(txtInvoiceID.Text), Action, Description, UDFLib.ConvertToInteger(GetSessionUserID()));
        }
        catch { }
        {

        }
    }
    public Int64 GetSupplyID()
    {
        try
        {
            if (Request.QueryString["SUPPLY_ID"] != null)
            {
                return Int64.Parse(Request.QueryString["SUPPLY_ID"].ToString());
            }

            else
                return 0;
        }
        catch { return 0; }
    }
    protected void BindPODetails()
    {
        try
        {
            DataSet ds = BLL_POLOG_Register.POLOG_Get_PO_Deatils(UDFLib.ConvertIntegerToNull(GetSupplyID()), null, 0);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                lblpaymentTerms.Text = dr["PaymentTermsDays"].ToString();
                lblGstRate.Text = dr["GST_Rate"].ToString();
                ddlCurrency.SelectedValue = dr["Line_Currency"].ToString();
            }
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
    protected void BindType()
    {
        DataSet ds = BLL_POLOG_Register.POLOG_Get_Type(UDFLib.ConvertToInteger(GetSessionUserID()),"PO_TYPE");

        ddlType.DataSource = ds.Tables[4];
        ddlType.DataTextField = "VARIABLE_NAME";
        ddlType.DataValueField = "VARIABLE_CODE";
        ddlType.DataBind();
        ddlType.Items.Insert(0, new ListItem("-Select-", "0"));
    }
    private string GetSessionUserName()
    {
        if (Session["USERNAME"] != null)
            return Session["USERNAME"].ToString();
        else
            return "0";
    }
    protected void BindCurrencyDLL()
    {
        DataTable dt = objBLLCurrency.Get_CurrencyList();
        ddlCurrency.DataSource = dt;
        ddlCurrency.DataTextField = "Currency_Code";
        ddlCurrency.DataValueField = "Currency_Code";
        ddlCurrency.DataBind();
        ddlCurrency.Items.Insert(0, new ListItem("-SELECT-", "0"));

    }
    protected void BindInvoice()
    {
        DataSet ds = BLL_POLOG_Invoice.POLOG_Get_Invoice_Details(UDFLib.ConvertStringToNull(txtInvoiceID.Text), GetSessionUserID());

        if (ds.Tables[0].Rows.Count > 0)
        {
            lblInvoiceID.Text = ds.Tables[0].Rows[0]["Invoice_ID"].ToString();
            lblJournal.Text = ds.Tables[0].Rows[0]["Journal_ID"].ToString();
            txtInvoiceID.Text = ds.Tables[0].Rows[0]["Invoice_ID"].ToString();

            ddlType.SelectedValue = ds.Tables[0].Rows[0]["Invoice_Type"].ToString();
            txtInvoiceDate.Text = ds.Tables[0].Rows[0]["Invoice_Date"].ToString();
            txtReferance.Text = ds.Tables[0].Rows[0]["Invoice_Reference"].ToString();
            txtReceivedDate.Text = ds.Tables[0].Rows[0]["Received_Date"].ToString();
            txtInvoiceValue.Text = ds.Tables[0].Rows[0]["Invoice_Amount"].ToString();
            ddlCurrency.SelectedValue = ds.Tables[0].Rows[0]["Invoice_Currency"].ToString();
            txtGST.Text = ds.Tables[0].Rows[0]["Invoice_GST_Amount"].ToString();
            txtDueDate.Text = ds.Tables[0].Rows[0]["Invoice_Due_Date"].ToString();
            txtInvStatus.Text = ds.Tables[0].Rows[0]["Invoice_Status"].ToString();
            txtPaymentDate.Text = ds.Tables[0].Rows[0]["Payment_Due_Date"].ToString();
            txtRemarks.Text = ds.Tables[0].Rows[0]["Discrepancies_Remarks"].ToString();
            lblVerificationDate.Text = ds.Tables[0].Rows[0]["Verified_Date"].ToString();
            if (ds.Tables[0].Rows[0]["Verified_Date"].ToString() == "")
            {
                lblPendingApproval.Text = "Pending Approval By :";
                //lblApproval.Text = "Approval Limit has not assigned for this Invoice.";
                lblApproval.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                lblPendingApproval.Text = "Pending approval by :";
                if (ds.Tables[0].Rows[0]["Req_Type"].ToString() == "OXP")
                {
                    lblApproval.Text = "Invoices for Owners Expenses are approved in the Charter Hire Module. Please inform Chartering Dept.";
                }
                else
                {
                    if (ds.Tables[0].Rows[0]["Invoice_Approvers"].ToString() != "")
                    {
                        lblApproval.Text = ds.Tables[0].Rows[0]["Invoice_Approvers"].ToString();
                    }
                    else
                    {
                        lblApproval.Text = "Approval Limit has not assigned for this Invoice.";
                    }
                    lblApproval.ForeColor = System.Drawing.Color.Red;
                }

            }
            //lblApprovalDate.Text = ds.Tables[0].Rows[0]["Approved_Date"].ToString();
            lblverification.Text = ds.Tables[0].Rows[0]["Verify_By"].ToString();
            
            lblAction.Text = ds.Tables[0].Rows[0]["Action_By"].ToString();
            if (ds.Tables[0].Rows[0]["Urgent_Flag"].ToString() == "YES")
            {
                lblUrgent.ForeColor = System.Drawing.Color.Red;
                lblUrgent.Text = ds.Tables[0].Rows[0]["Urgent_Flag"].ToString();
                btnUrgent.Text = "Not Urgent";
            }
            else
            {
                lblUrgent.ForeColor = System.Drawing.Color.Blue;
                lblUrgent.Text = "No";
                btnUrgent.Text = "Urgent";
            }
            lblPayment.Text = ds.Tables[0].Rows[0]["Payment_Priority"].ToString();
            btnCancel.Visible = true;
            btnVerified.Visible = true;
            btnAttachment.Visible = true;
            if (ds.Tables[0].Rows[0]["Invoice_Status"].ToString() == "Received")
            {
                //btnSave.Enabled = false;
                btnCancel.Enabled = true;
                btnUnVerified.Visible = false;
                btnVerified.Visible = true;
                btnApprove.Visible = false;
                btnUnApprove.Visible = false;
            }
            if (ds.Tables[0].Rows[0]["Invoice_Status"].ToString() == "Verified")
            {
                if (ds.Tables[0].Rows[0]["Invoice_Type"].ToString() == "ADVANCE")
                {
                    ddlType.Enabled = false;
                }
                EnableDisable(false);
                btnCTM.Visible = true;
                if (ds.Tables[0].Rows[0]["For_Approver"].ToString() == "Yes")
                {
                    btnApprove.Visible = true;
                    btnUnVerified.Visible = false;
                    btnCancel.Visible = false;
                    btnVerified.Visible = false;
                    btnSave.Visible = false;
                    btnUnApprove.Visible = false;
                }
                if (ds.Tables[0].Rows[0]["Verified_By"].ToString() != "")
                {
                    btnUnVerified.Visible = true;
                    btnCancel.Visible = false;
                    btnVerified.Visible = false;
                    btnSave.Visible = false;
                }
                else
                {
                    btnUnVerified.Visible = false;
                    btnCancel.Visible = true;
                    btnVerified.Visible = true;
                    btnSave.Visible = true;
                }
            }
            if (ds.Tables[0].Rows[0]["Invoice_Status"].ToString() == "Approved")
            {
                lblPendingApproval.Text = "Approved By :";
                lblApproval.Text = ds.Tables[0].Rows[0]["Approve_By"].ToString();
                lblApprovalDate.Text = ds.Tables[0].Rows[0]["Approved_Date"].ToString();
                btnApprove.Visible = false;
                btnUnVerified.Visible = false;
                btnCancel.Visible = false;
                btnVerified.Visible = false;
                btnSave.Visible = false;
                btnCTM.Visible = false;
                btnUrgent.Visible = false;
                btnAttachment.Visible = false;
                btnUnApprove.Visible = false;
                EnableDisable(false);
            }
            if (ds.Tables[0].Rows[0]["Invoice_Status"].ToString() == "Approved" && ds.Tables[0].Rows[0]["Approve_By"].ToString() == GetSessionUserName())
            {
                btnUnApprove.Visible = true; 
            }
            if (ds.Tables[0].Rows[0]["Invoice_Status"].ToString() == "CANCELLED")
            {
                btnApprove.Enabled = false;
                btnUnVerified.Enabled = false;
                btnCancel.Enabled = false;
                btnVerified.Enabled = false;
                btnSave.Enabled = false;
                btnUnApprove.Enabled = true;
                lblHeadermsg.Visible = true;
                lblHeadermsg.Text = "Invoice Cancelled";
            }
        }
    }
    private void EnableDisable(bool blnFlag)
    {
        ddlType.Enabled = blnFlag;
        ddlCurrency.Enabled = blnFlag;
        txtInvoiceDate.Enabled = blnFlag;
        txtReferance.Enabled = blnFlag;
        txtReceivedDate.Enabled = blnFlag;
        txtInvoiceValue.Enabled = blnFlag;
        txtGST.Enabled = blnFlag;
        txtDueDate.Enabled = blnFlag;
        txtInvStatus.Enabled = blnFlag;
        txtPaymentDate.Enabled = blnFlag;
        txtRemarks.Enabled = blnFlag;
    }

    protected int Checkvalue(decimal InvoiceValue,decimal GSTValue, string InvoiceType)
    {
         if (InvoiceType == "CREDIT")
        {
            if (InvoiceValue > 0)
            {
               
                i = 1;
            }
            if (GSTValue > 0)
            {
                i = 1;
            }
        }
        else
        {
            if (InvoiceValue < 0)
            {
               
                i = 2;
            }
            if (GSTValue < 0)
            {
                i = 2;
            }
        }
         return i;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        i = Checkvalue(Convert.ToDecimal(txtInvoiceValue.Text),Convert.ToDecimal(txtGST.Text), ddlType.SelectedValue);
        if (i == 0)
        {
            string Status = "Received";
            string InvStatus = "Received";
            //txtInvoiceID.Text = "000000";
            string retval = BLL_POLOG_Invoice.POLog_Insert_Invoice(UDFLib.ConvertStringToNull(txtInvoiceID.Text), UDFLib.ConvertIntegerToNull(GetSupplyID()),
                                         UDFLib.ConvertStringToNull(ddlType.SelectedValue), UDFLib.ConvertDateToNull(txtInvoiceDate.Text.Trim()), txtReferance.Text.Trim(),
                                         UDFLib.ConvertDateToNull(txtReceivedDate.Text.Trim()), UDFLib.ConvertDecimalToNull(txtInvoiceValue.Text.Trim()), ddlCurrency.SelectedValue, UDFLib.ConvertDecimalToNull(txtGST.Text.Trim()),
                                         UDFLib.ConvertDateToNull(txtDueDate.Text.Trim()), InvStatus, UDFLib.ConvertDateToNull(txtPaymentDate.Text.Trim()), txtRemarks.Text.Trim(), Status,
                                         UDFLib.ConvertToInteger(GetSessionUserID()));

            txtInvoiceID.Text = Convert.ToString(retval);
            BindInvoice();

            string Action = "New Invoice";
            string Description = "NewInvoice";
            int RetAuditVal = BLL_POLOG_Register.POLOG_Insert_Transactionlog(UDFLib.ConvertStringToNull(lblInvoiceID.Text), Action, Description, UDFLib.ConvertToInteger(GetSessionUserID()));

            string message = "alert('Invoice Saved.')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
        else if (i == 1)
        {
            string message = "alert('Invoice and GST value should be negative for credit note Invoice Type.')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
        else if (i == 2)
        {
            string message = "alert('Invoice and GST value should be positive.')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    protected void btnVerified_Click(object sender, EventArgs e)
    {
        i = Checkvalue(Convert.ToDecimal(txtInvoiceValue.Text), Convert.ToDecimal(txtGST.Text), ddlType.SelectedValue);
        if (i == 0)
        {
            string Status = "Verified";
            string InvStatus = "Verified";
            string retval = BLL_POLOG_Invoice.POLog_Insert_Invoice(UDFLib.ConvertStringToNull(txtInvoiceID.Text), UDFLib.ConvertIntegerToNull(GetSupplyID()),
                                        UDFLib.ConvertStringToNull(ddlType.SelectedValue), UDFLib.ConvertDateToNull(txtInvoiceDate.Text.Trim()), txtReferance.Text.Trim(),
                                        UDFLib.ConvertDateToNull(txtReceivedDate.Text.Trim()), UDFLib.ConvertDecimalToNull(txtInvoiceValue.Text.Trim()), ddlCurrency.SelectedValue, UDFLib.ConvertDecimalToNull(txtGST.Text.Trim()),
                                        UDFLib.ConvertDateToNull(txtDueDate.Text.Trim()), InvStatus, UDFLib.ConvertDateToNull(txtPaymentDate.Text.Trim()), txtRemarks.Text.Trim(), Status,
                                        UDFLib.ConvertToInteger(GetSessionUserID()));
            //string msgDraft = String.Format("parent.ReloadParent_ByButtonID();");
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgDraft", msgDraft, true);
            if (retval != "0")
            {
                txtInvoiceID.Text = Convert.ToString(retval);
                BindInvoice();
                int RetAuditVal = BLL_POLOG_Register.POLOG_Insert_Transactionlog(UDFLib.ConvertStringToNull(lblInvoiceID.Text), "Verified Invoice", "VerifiedInvoice", UDFLib.ConvertToInteger(GetSessionUserID()));
                string message = "alert('Invoice Verified.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            }
            else
            {
                string message = "alert('Invoice Cant be verified before uploading attachment.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            }
            
        }
        else if (i == 1)
        {
            string message = "alert('Invoice and GST value should be negative for credit note Invoice Type.')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
        else if (i == 2)
        {
            string message = "alert('Invoice and GST value should be positive.')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    protected void btnUnVerified_Click(object sender, EventArgs e)
    {
        i = Checkvalue(Convert.ToDecimal(txtInvoiceValue.Text), Convert.ToDecimal(txtGST.Text), ddlType.SelectedValue);
        if (i == 0)
        {
            string Status = "Received";
            string InvStatus = "Received";
        string retval = BLL_POLOG_Invoice.POLog_Insert_Invoice(UDFLib.ConvertStringToNull(txtInvoiceID.Text), UDFLib.ConvertIntegerToNull(GetSupplyID()),
                                     UDFLib.ConvertStringToNull(ddlType.SelectedValue), UDFLib.ConvertDateToNull(txtInvoiceDate.Text.Trim()), txtReferance.Text.Trim(),
                                     UDFLib.ConvertDateToNull(txtReceivedDate.Text.Trim()), UDFLib.ConvertDecimalToNull(txtInvoiceValue.Text.Trim()), ddlCurrency.SelectedValue, UDFLib.ConvertDecimalToNull(txtGST.Text.Trim()),
                                     UDFLib.ConvertDateToNull(txtDueDate.Text.Trim()), InvStatus, UDFLib.ConvertDateToNull(txtPaymentDate.Text.Trim()), txtRemarks.Text.Trim(), Status,
                                     UDFLib.ConvertToInteger(GetSessionUserID()));
        txtInvoiceID.Text = Convert.ToString(retval);
        BindInvoice();
        int RetAuditVal = BLL_POLOG_Register.POLOG_Insert_Transactionlog(UDFLib.ConvertStringToNull(lblInvoiceID.Text), "UNVerified Invoice", "UNVerifiedInvoice", UDFLib.ConvertToInteger(GetSessionUserID()));
        string message = "alert('Invoice UN-Verify.')";
        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

        
             }
        else if (i == 1)
        {
            string message = "alert('Invoice and GST value should be negative for credit note Invoice Type.')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
        else if (i == 2)
        {
            string message = "alert('Invoice and GST value should be positive.')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
        
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        i = Checkvalue(Convert.ToDecimal(txtInvoiceValue.Text), Convert.ToDecimal(txtGST.Text), ddlType.SelectedValue);
        if (i == 0)
        {
        string Status = "CANCELLED";
        string InvStatus = "CANCELLED";
        string retval = BLL_POLOG_Invoice.POLog_Insert_Invoice(UDFLib.ConvertStringToNull(txtInvoiceID.Text), UDFLib.ConvertIntegerToNull(GetSupplyID()),
                                    UDFLib.ConvertStringToNull(ddlType.SelectedValue), UDFLib.ConvertDateToNull(txtInvoiceDate.Text.Trim()), txtReferance.Text.Trim(),
                                    UDFLib.ConvertDateToNull(txtReceivedDate.Text.Trim()), UDFLib.ConvertDecimalToNull(txtInvoiceValue.Text.Trim()), ddlCurrency.SelectedValue, UDFLib.ConvertDecimalToNull(txtGST.Text.Trim()),
                                    UDFLib.ConvertDateToNull(txtDueDate.Text.Trim()), InvStatus, UDFLib.ConvertDateToNull(txtPaymentDate.Text.Trim()), txtRemarks.Text.Trim(), Status,
                                    UDFLib.ConvertToInteger(GetSessionUserID()));
        txtInvoiceID.Text = Convert.ToString(retval);
        int RetAuditVal = BLL_POLOG_Register.POLOG_Insert_Transactionlog(UDFLib.ConvertStringToNull(lblInvoiceID.Text), "Cancelled Invoice", "CancelledInvoice", UDFLib.ConvertToInteger(GetSessionUserID()));
        string message = "alert('Invoice Cancelled.')";
        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        string msgDraft = String.Format("parent.ReloadParent_ByButtonID();");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgDraft", msgDraft, true);
           }
        else if (i == 1)
        {
            string message = "alert('Invoice and GST value should be negative for credit note Invoice Type.')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
        else if (i == 2)
        {
            string message = "alert('Invoice and GST value should be positive.')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        i = Checkvalue(Convert.ToDecimal(txtInvoiceValue.Text), Convert.ToDecimal(txtGST.Text), ddlType.SelectedValue);
        if (i == 0)
        {
            string Status = "Approved";
            string InvStatus = "Approved";
        string retval = BLL_POLOG_Invoice.POLog_Insert_Invoice(UDFLib.ConvertStringToNull(txtInvoiceID.Text), UDFLib.ConvertIntegerToNull(GetSupplyID()),
                                    UDFLib.ConvertStringToNull(ddlType.SelectedValue), UDFLib.ConvertDateToNull(txtInvoiceDate.Text.Trim()), txtReferance.Text.Trim(),
                                    UDFLib.ConvertDateToNull(txtReceivedDate.Text.Trim()), UDFLib.ConvertDecimalToNull(txtInvoiceValue.Text.Trim()), ddlCurrency.SelectedValue, UDFLib.ConvertDecimalToNull(txtGST.Text.Trim()),
                                    UDFLib.ConvertDateToNull(txtDueDate.Text.Trim()), InvStatus, UDFLib.ConvertDateToNull(txtPaymentDate.Text.Trim()), txtRemarks.Text.Trim(), Status,
                                    UDFLib.ConvertToInteger(GetSessionUserID()));
        txtInvoiceID.Text = Convert.ToString(retval);
        int RetAuditVal = BLL_POLOG_Register.POLOG_Insert_Transactionlog(UDFLib.ConvertStringToNull(lblInvoiceID.Text), "Approved Invoice", "ApprovedInvoice", UDFLib.ConvertToInteger(GetSessionUserID()));
        //string msgDraft = String.Format("parent.ReloadParent_ByButtonID();");
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgDraft", msgDraft, true);
        BindInvoice();
        string message = "alert('Invoice Approved.')";
        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        
             }
        else if (i == 1)
        {
            string message = "alert('Invoice and GST value should be negative for credit note Invoice Type.')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
        else if (i == 2)
        {
            string message = "alert('Invoice and GST value should be positive.')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    protected void btnUnApprove_Click(object sender, EventArgs e)
    {
        i = Checkvalue(Convert.ToDecimal(txtInvoiceValue.Text), Convert.ToDecimal(txtGST.Text), ddlType.SelectedValue);
        if (i == 0)
        {
            string Status = "Verified";
            string InvStatus = "Verified";
        string retval = BLL_POLOG_Invoice.POLog_Insert_Invoice(UDFLib.ConvertStringToNull(txtInvoiceID.Text), UDFLib.ConvertIntegerToNull(GetSupplyID()),
                                    UDFLib.ConvertStringToNull(ddlType.SelectedValue), UDFLib.ConvertDateToNull(txtInvoiceDate.Text.Trim()), txtReferance.Text.Trim(),
                                    UDFLib.ConvertDateToNull(txtReceivedDate.Text.Trim()), UDFLib.ConvertDecimalToNull(txtInvoiceValue.Text.Trim()), ddlCurrency.SelectedValue, UDFLib.ConvertDecimalToNull(txtGST.Text.Trim()),
                                    UDFLib.ConvertDateToNull(txtDueDate.Text.Trim()), InvStatus, UDFLib.ConvertDateToNull(txtPaymentDate.Text.Trim()), txtRemarks.Text.Trim(), Status,
                                    UDFLib.ConvertToInteger(GetSessionUserID()));
        txtInvoiceID.Text = Convert.ToString(retval);
        int RetAuditVal = BLL_POLOG_Register.POLOG_Insert_Transactionlog(UDFLib.ConvertStringToNull(lblInvoiceID.Text), "UNApproved Invoice", "UNApprovedInvoice", UDFLib.ConvertToInteger(GetSessionUserID()));
        //string msgDraft = String.Format("parent.ReloadParent_ByButtonID();");
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgDraft", msgDraft, true);
        BindInvoice();
        string message = "alert('Invoice UN_Approve.')";
        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        
        }
        else if (i == 1)
        {
            string message = "alert('Invoice Value should be negative for credit note Invoice Type.')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
        else if (i == 2)
        {
            string message = "alert('Invoice and GST value should be positive.')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    protected void btnAttachment_Click(object sender, EventArgs e)
    {
        string InvoiceAttachment = String.Format("showModal('dvAttachment',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "InvoiceAttachment", InvoiceAttachment, true);
    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        try
        {

            string DocType = "Invoice";
            string savePath = Server.MapPath("\\" + System.Configuration.ConfigurationManager.AppSettings["APP_NAME"].ToString() + "\\Uploads\\Files_Uploaded");
            Guid GUID = Guid.NewGuid();

            string Flag_Attach = GUID.ToString() + Path.GetExtension(FileUpload1.FileName);



            string FileID = BLL_POLOG_Register.POLOG_Insert_AttachedFile(UDFLib.ConvertStringToNull(lblInvoiceID.Text), Path.GetExtension(FileUpload1.FileName),
                UDFLib.Remove_Special_Characters(Path.GetFileName(FileUpload1.FileName)), Flag_Attach, DocType, Convert.ToInt16(GetSessionUserID()));

            FileID = FileID.PadLeft(8, '0');
            string F1 = Mid(FileID, 0, 2);
            string F2 = Mid(FileID, 2, 2);
            string F3 = Mid(FileID, 4, 2);
            if (!Directory.Exists(savePath + "\\" + F1 + "\\" + F2 + "\\" + F3))
            {
                Directory.CreateDirectory(savePath + "\\" + F1 + "\\" + F2 + "\\" + F3);
            }

            string filePath = savePath + "\\" + F1 + "\\" + F2 + "\\" + F3 + "\\" + FileID + "" + Path.GetExtension(FileUpload1.FileName);
            FileUpload1.SaveAs(filePath);
            int RetAuditVal = BLL_POLOG_Register.POLOG_Insert_Transactionlog(UDFLib.ConvertStringToNull(lblInvoiceID.Text), "Uploaded Invoice", "UploadedInvoice", UDFLib.ConvertToInteger(GetSessionUserID()));
            BindReqsnAttchment();
        }
        catch (Exception ex)
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
    public void BindReqsnAttchment()
    {
        string DocType = "Invoice";
        DataTable dtAttachment = BLL_POLOG_Register.POLOG_Get_Attachments(UDFLib.ConvertStringToNull(lblInvoiceID.Text), DocType);

        if (dtAttachment.Rows.Count > 0)
        {
            string FilePath = dtAttachment.Rows[0]["File_Path"].ToString();
            divAttachment.Visible = true;
            gvReqsnAttachment.DataSource = dtAttachment;
            gvReqsnAttachment.DataBind();
            BindAttchement(hdnFilePath.Value);
        }
        else
        {
            divAttachment.Visible = false;
            gvReqsnAttachment.DataSource = dtAttachment;
            gvReqsnAttachment.DataBind();
        }
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        BindReqsnAttchment();
    }
    public void imgbtnDelete_Click(object s, EventArgs e)
    {
        try
        {
            string[] arg = (((ImageButton)s).CommandArgument.Split(new char[] { ',' }));
            int res = BLL_POLOG_Register.POLOG_Delete_Attachments(arg[0].ToString(), arg[1].ToString(), Convert.ToInt16(GetSessionUserID()));
            //string SavePath = Server.MapPath("\\" + System.Configuration.ConfigurationManager.AppSettings["APP_NAME"].ToString() + "\\Uploads\\Files_Uploaded");
            //string SavePath = ("../Uploads/Files_Uploaded");
            //string File_ID = arg[0];
            //File_ID = File_ID.PadLeft(8, '0');
            //string F1 = Mid(File_ID, 0, 2);
            //string F2 = Mid(File_ID, 2, 2);
            //string F3 = Mid(File_ID, 4, 2);
            //string filePath = SavePath + "\\" + F1 + "\\" + F2 + "\\" + F3 + "\\" + arg[2];
            //if (File.Exists(filePath))
            //{
            //    File.Delete(filePath);
            //}
          
         
          

        }
        catch
        { }
    }
    protected void ImgView_Click(object s, CommandEventArgs e)
    {
        string[] arg = e.CommandArgument.ToString().Split(',');
        string FilePath = UDFLib.ConvertStringToNull(arg[0]);
        iFrame1.Attributes["src"] =   FilePath;
        BindAttchement(FilePath);
       
        //ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "javascript:window.open('" + FilePath + "');", true);
    }
    protected void BindAttchement(string FilePath)
    {
        if (FilePath != "")
        {
            divIframe.Visible = true;
            Random r = new Random();
            string ver = r.Next().ToString();
            iFrame1.Attributes.Add("src", FilePath + "?ver=" + ver);
        }
    }
    protected void gvReqsnAttachment_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ImageButton btnImg = (ImageButton)e.Row.FindControl("ImgView");
            ImageButton imgDownload = (ImageButton)e.Row.FindControl("imgDownload");

            string SavePath = ("../Uploads/Files_Uploaded");
            string File_ID = DataBinder.Eval(e.Row.DataItem, "Id").ToString();
            File_ID = File_ID.PadLeft(8, '0');
            string F1 = Mid(File_ID, 0, 2);
            string F2 = Mid(File_ID, 2, 2);
            string F3 = Mid(File_ID, 4, 2);
            string filePath = SavePath + "/" + F1 + "/" + F2 + "/" + F3 + "/" + DataBinder.Eval(e.Row.DataItem, "File_Path").ToString();
            btnImg.CommandArgument = filePath;
            imgDownload.CommandArgument = filePath;
            hdnFilePath.Value = filePath;

        }
    }
    protected void ImgDownload_Click(object s, CommandEventArgs e)
    {
        string[] arg = e.CommandArgument.ToString().Split(',');
        string FilePath = UDFLib.ConvertStringToNull(arg[0]);
     
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "javascript:window.open('" + FilePath + "');", true);
    }
    protected void btnUrgent_Click(object sender, EventArgs e)
    {
        string Status = null;
        string InvStatus = null;
        if (btnUrgent.Text == "Urgent")
        {
             Status = "Urgent";
             InvStatus = "Urgent";
             int RetAuditVal = BLL_POLOG_Register.POLOG_Insert_Transactionlog(UDFLib.ConvertStringToNull(lblInvoiceID.Text), "NON Urgent Invoice", "UNUrgentInvoice", UDFLib.ConvertToInteger(GetSessionUserID()));
        }
        else
        {
            Status = "UNUrgent";
            InvStatus = "UNUrgent";
            int RetAuditVal = BLL_POLOG_Register.POLOG_Insert_Transactionlog(UDFLib.ConvertStringToNull(lblInvoiceID.Text), "Urgent Invoice", "UrgentInvoice", UDFLib.ConvertToInteger(GetSessionUserID()));
        }
        Update_Invoice(Status, InvStatus);
        
    }
    protected void Update_Invoice(string Status, string InvStatus)
    {
        try
        {
            int retval = BLL_POLOG_Register.POLog_Update_Invoice(UDFLib.ConvertStringToNull(txtInvoiceID.Text), 0, 0, InvStatus,
                                                                          Status, UDFLib.ConvertToInteger(GetSessionUserID()));
            string msg2 = String.Format("alert('Invoice marked as Urgent.')");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg2, true);
            BindInvoice();
        }
        catch { }
        {
        }

    }
    protected void btnCTM_Click(object sender, EventArgs e)
    {
        try
        {
            string InvStatus = "CTM";
            int retval = BLL_POLOG_Invoice.POLog_Update_CTM_Invoice(UDFLib.ConvertStringToNull(txtInvoiceID.Text), InvStatus, UDFLib.ConvertToInteger(GetSessionUserID()));
            int RetAuditVal = BLL_POLOG_Register.POLOG_Insert_Transactionlog(UDFLib.ConvertStringToNull(lblInvoiceID.Text), "CTM Invoice", "CTMInvoice", UDFLib.ConvertToInteger(GetSessionUserID()));
            string message = "alert('Invoice and Payment Updated Successfully of CTM Transaction.')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            BindInvoice();
        }
        catch { }
        {
        }
    }
  
    protected void btnInvoiceRevoke_Click(object sender, EventArgs e)
    {
        try
        {
            string RevokeType = "InvoiceApproval";
            int retval = BLL_POLOG_Invoice.POLog_Revoke_Payment_Invoice(UDFLib.ConvertStringToNull(txtInvoiceID.Text), RevokeType, UDFLib.ConvertToInteger(GetSessionUserID()));

            string message = "alert('Invoice and Payment Updated Successfully of CTM Transaction.')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            BindInvoice();
        }
        catch { }
        {
        }
    }
    protected void btnPaymentRevoke_Click(object sender, EventArgs e)
    {
        try
        {
            string RevokeType = "PaymentApproval";
            int retval = BLL_POLOG_Invoice.POLog_Revoke_Payment_Invoice(UDFLib.ConvertStringToNull(txtInvoiceID.Text), RevokeType, UDFLib.ConvertToInteger(GetSessionUserID()));

            string message = "alert('Invoice and Payment Updated Successfully of CTM Transaction.')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            BindInvoice();
        }
        catch { }
        {
        }
    }
}