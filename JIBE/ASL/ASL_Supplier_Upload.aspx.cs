using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.Infrastructure;
using SMS.Business.ASL;
using System.IO;
using SMS.Properties;


public partial class ASL_ASL_Supplier_Upload : System.Web.UI.Page
{
    UserAccess objUA = new UserAccess();
    public Boolean uaEditFlag = true;
    public Boolean uaDeleteFlage = true;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtFileID.Text = GetFileID();
            BindGrid();
        }
    }

    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }
    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
        {
            pnl.Visible = false;
            lblMsg.Text = "You don't have sufficient previlege to access the requested information.";
        }
        else
        {
            pnl.Visible = true;
        }

        if (objUA.Add == 0)
        {
            //ImgAdd.Visible = false;
        }
        if (objUA.Edit == 1)
            uaEditFlag = true;
        //else
        //    btnsave.Visible = false;

        if (objUA.Delete == 1) uaDeleteFlage = true;

    }

    protected void BindGrid()
    {
        string Supplier_Code = GetSupplierCode();
        string Supp_ID = GetSuppID();
        string File_ID = GetFileID();
        // int? SuppID = UDFLib.ConvertIntegerToNull(Request.QueryString["Supp_ID"]);
        DataSet ds = BLL_ASL_Supplier.Get_Supplier_Upload_Document(UDFLib.ConvertStringToNull(Supplier_Code), UDFLib.ConvertStringToNull(Supp_ID), "INVOICE", UDFLib.ConvertStringToNull(txtFileID.Text.Trim()));

        if (ds.Tables[0].Rows.Count > 0)
        {
            lblVessel.Text = ds.Tables[0].Rows[0]["Vessel_Name"].ToString();
            lblRef.Text = ds.Tables[0].Rows[0]["Office_Ref_Code"].ToString();
            lblDated.Text = ds.Tables[0].Rows[0]["LineDate"].ToString();
            lblAmount.Text = ds.Tables[0].Rows[0]["Line_Amount"].ToString();
        }
        if (ds.Tables[1].Rows.Count > 0)
        {
            lblFileName.Text = ds.Tables[1].Rows[0]["File_Name"].ToString();
            txtInvioceRef.Text = ds.Tables[1].Rows[0]["Invoice_Reference"].ToString();
            txtInvoiceDate.Text = ds.Tables[1].Rows[0]["Invoice_Date"].ToString();
            txtInvoiceAmount.Text = ds.Tables[1].Rows[0]["Invoice_Amount"].ToString();

            txtTaxAmount.Text = ds.Tables[1].Rows[0]["Invoice_GST_Amount"].ToString();
            txtDueDate.Text = ds.Tables[1].Rows[0]["Invoice_Due_Date"].ToString();
            txtRemarks.Text = ds.Tables[1].Rows[0]["Invoice_Remarks"].ToString();
            lblInvoiceReferance.Text = ds.Tables[1].Rows[0]["Invoice_Reference"].ToString();
            lblDate.Text = ds.Tables[1].Rows[0]["Invoice_Date"].ToString();
            lblInvoiceAmount.Text = ds.Tables[1].Rows[0]["Invoice_Amount"].ToString();
            lblRemarks.Text = ds.Tables[1].Rows[0]["Invoice_Rejection_Remarks"].ToString();

            string FilePath = ds.Tables[1].Rows[0]["File_Path"].ToString();
            string FileLocation = HttpContext.Current.Server.MapPath("../Uploads/ASL/Invoice/" + FilePath);
            if (FilePath != "")
            {
                divIframe.Visible = true;
                iFrame1.Attributes["src"] = @"../Uploads/ASL/Invoice/" + FilePath;
            }
            if (ds.Tables[1].Rows[0]["File_Status"].ToString() == "SUBMITTED")
            {
                DivStep4.Visible = true;
                btnSubmit.Enabled = false;
                btnUploadInvoice.Enabled = false;
                btnRecallInvoice.Enabled = true;
            }
            if (ds.Tables[1].Rows[0]["File_Status"].ToString() == "REJECTED")
            {
                lblReject.Visible = true;
                lblNotReject.Visible = false;
                if (ds.Tables[2].Rows.Count > 0)
                {
                    lblRejectReason.Text = ds.Tables[2].Rows[0]["Variable_Name"].ToString();
                }
            }
            else
            {
                lblReject.Visible = false;
                lblNotReject.Visible = true;
            }
        }
        string Type = GetInvoiceType();
        if (Type == "1")
        {
            DivStep3.Visible = true;
        }
        else
        {
            Divstep1.Visible = true;
        }
    }
    public string GetInvoiceType()
    {
        try
        {
            if (Request.QueryString["Type"] != null)
            {
                return Request.QueryString["Type"].ToString();
            }

            else
                return "0";
        }
        catch { return "0"; }
    }
    public string GetSuppID()
    {
        try
        {
            if (Request.QueryString["Supply_ID"] != null)
            {
                return Request.QueryString["Supply_ID"].ToString();
            }

            else
                return "0";
        }
        catch { return "0"; }
    }
    public string GetSupplierCode()
    {
        try
        {
            if (Request.QueryString["SupplierCode"] != null)
            {
                return Request.QueryString["SupplierCode"].ToString();
            }

            else
                return "0";
        }
        catch { return "0"; }
    }
    public string GetFileID()
    {
        try
        {
            if (txtFileID.Text.Trim() != "")
            {
                return txtFileID.Text.ToString();
            }
            if (Request.QueryString["File_ID"] != null)
            {
                return Request.QueryString["File_ID"].ToString();
            }

            else
                return "0";
        }
        catch { return "0"; }
    }
    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        Divstep2.Visible = true;
    }
    protected int InvoiceUpload(string FileStatus, string Action_On_Data_Form)
    {
        int ret = 0;
        try
        {
            
            if (FileUploader.HasFile)
            {
                if (FileUploader.FileName != "")
                {
                    string strLocalPath = FileUploader.PostedFile.FileName;
                    string FileName = Path.GetFileName(strLocalPath);
                    string FileExtension = Path.GetExtension(strLocalPath);
                    int TYPE = 0;
                    string InvoiceType = "INVOICE";
                    Guid FileGuid = System.Guid.NewGuid();

                    FileUploader.PostedFile.SaveAs(Server.MapPath(@"~/Uploads\\ASL\\Invoice\\" + FileGuid + FileExtension));

                     ret = BLL_ASL_Supplier.Supplier_Upload_Document_Insert(Convert.ToInt16(txtFileID.Text.Trim()), UDFLib.ConvertStringToNull(GetSupplierCode()), UDFLib.ConvertStringToNull(GetSuppID()), FileName, FileGuid + FileExtension, FileExtension
                            , txtInvioceRef.Text.Trim(), UDFLib.ConvertDateToNull(txtInvoiceDate.Text), UDFLib.ConvertDecimalToNull(txtInvoiceAmount.Text), UDFLib.ConvertDecimalToNull(txtTaxAmount.Text),
                            txtRemarks.Text.Trim(), UDFLib.ConvertDateToNull(txtDueDate.Text), FileStatus,Action_On_Data_Form,InvoiceType, UDFLib.ConvertIntegerToNull(Session["userid"]), TYPE);
                }
            }
            else if (lblFileName.Text != "")
            {
                string FileName = "";
                string FileExtension = "";
                int TYPE = 0;
                string InvoiceType = "INVOICE";
                Guid FileGuid = System.Guid.NewGuid();
                ret = BLL_ASL_Supplier.Supplier_Upload_Document_Insert(Convert.ToInt16(txtFileID.Text.Trim()), UDFLib.ConvertStringToNull(GetSupplierCode()), UDFLib.ConvertStringToNull(GetSuppID()), FileName, "", FileExtension
                       , txtInvioceRef.Text.Trim(), UDFLib.ConvertDateToNull(txtInvoiceDate.Text), UDFLib.ConvertDecimalToNull(txtInvoiceAmount.Text), UDFLib.ConvertDecimalToNull(txtTaxAmount.Text), txtRemarks.Text.Trim(),
                       UDFLib.ConvertDateToNull(txtDueDate.Text), FileStatus, Action_On_Data_Form, InvoiceType, UDFLib.ConvertIntegerToNull(Session["userid"]), TYPE);
            }
            return ret;
        }

        catch { return ret;  }
        {
        }
    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        string FileStatus = null;
        string Action_On_Data_Form = "UPLOAD";
        int ret = InvoiceUpload(FileStatus,Action_On_Data_Form);
        if (ret >= 1)
        {
            txtFileID.Text = Convert.ToString(ret);
            string message = "alert('Invoice Uploaded Successfully.')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            BindGrid();
            DivStep3.Visible = true;
        }
    }
    protected void btnUploadInvoice_Click(object sender, EventArgs e)
    {
        string FileStatus = null;
        string Action_On_Data_Form = "UPLOAD";
        int ret = InvoiceUpload(FileStatus, Action_On_Data_Form);
        if (ret >= 1)
        {
            txtFileID.Text = Convert.ToString(ret);
            string message = "alert('Invoice Uploaded Successfully.')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            DivStep3.Visible = true;
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string FileStatus = "SUBMITTED";
        string Action_On_Data_Form = "UPLOAD";
        int ret = InvoiceUpload(FileStatus, Action_On_Data_Form);
        if (ret >= 1)
        {
            txtFileID.Text = Convert.ToString(ret);
            string message = "alert('Invoice Uploaded Successfully.')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            DivStep3.Visible = true;
        }
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        string FileStatus = "DELETED";
        string Action_On_Data_Form = "UPLOAD";
        int ret = InvoiceUpload(FileStatus, Action_On_Data_Form);
        if (ret >= 1)
        {
            txtFileID.Text = Convert.ToString(ret);

            string message = "alert('Invoice Deleted Successfully.')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            BindGrid();
            Divstep1.Visible = true;
            Divstep2.Visible = true;
            DivStep3.Visible = true;
            DivStep4.Visible = true;
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        this.ClientScript.RegisterClientScriptBlock(this.GetType(), "Close", "window.close()", true);
    }
    protected void btnRecallInvoice_Click(object sender, EventArgs e)
    {
        string FileStatus = null;
        string Action_On_Data_Form = "RECALL";
        int ret = InvoiceUpload(FileStatus, Action_On_Data_Form);
        if (ret >= 1)
        {
            txtFileID.Text = Convert.ToString(ret);
            //string message = "alert('Credit Note Uploaded Successfully.')";
            //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            DivStep3.Visible = true;
            DivStep4.Visible = false;
            btnSubmit.Enabled = true;
            btnUploadInvoice.Enabled = true;
            btnRecallInvoice.Enabled = false;
        }
    }
    protected void btnExit_Click(object sender, EventArgs e)
    {
        this.ClientScript.RegisterClientScriptBlock(this.GetType(), "Close", "window.close()", true);
    }
}