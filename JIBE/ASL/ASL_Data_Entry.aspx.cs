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

public partial class ASL_Data_Entry : System.Web.UI.Page
{

    BLL_Infra_Currency objBLLCurrency = new BLL_Infra_Currency();
    BLL_Infra_Country objBLLCountry = new BLL_Infra_Country();



    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!IsPostBack)
        {
            ViewState["ReturnSupplierID"] = 0;
            BindCountryDLL();
            BindCurrencyDLL();

            BindSupplierDataList();
            BindAttachment();

        }

    }
    
    protected void BindProposedSupplierList()
    {
        try
        {

            DataTable dt = BLL_ASL_Supplier.Get_Proposed_Supplier_List(UDFLib.ConvertIntegerToNull(Request.QueryString["Supp_ID"].ToString()));

            if (dt.Rows.Count > 0)
            {
                txtCompanyResgName.Text = dt.Rows[0]["Supplier_Name"].ToString();

                txtNamePIC1.Text = dt.Rows[0]["PIC_NAME"].ToString();
                txtPhonePIC1.Text = dt.Rows[0]["Phone"].ToString();
                txtEmailPIC1.Text = dt.Rows[0]["Email"].ToString();
                txtSuppAddress.Text = dt.Rows[0]["Address"].ToString();

            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }

    }
    /// <summary>
    /// Method is used to get supplier Data
    /// </summary>
    protected void BindSupplierDataList()
    {
        try
        {

            BindAttachment();

            DataSet ds = BLL_ASL_Supplier.Get_Supplier_Data_List(UDFLib.ConvertStringToNull(Request.QueryString["Supp_ID"].ToString()));

            if (ds.Tables[0].Rows.Count > 0)
            {

                DataRow dr = ds.Tables[0].Rows[0];

                ViewState["ReturnSupplierID"] = dr["Supp_ID"].ToString();

                txtSupplierCode.Text = dr["Supp_ID"].ToString();

                txtCompanyResgName.Text = dr["Register_Name"].ToString();
                txtSuppAddress.Text = dr["Company_Address"].ToString();
                txtState.Text = dr["Address_State"].ToString();
                ddlCountry.SelectedValue = dr["Address_CountryID"].ToString();


                txtPostalCode.Text = dr["Address_Postal_Code"].ToString();

                txtTelephone1.Text = dr["PHONE1"].ToString();
                txtTelephone2.Text = dr["PHONE2"].ToString();
                txtAOHNumber1.Text = dr["Comp_AOH_NO1"].ToString();
                txtAOHNumber2.Text = dr["Comp_AOH_NO2"].ToString();
                txtFaxNumber1.Text = dr["FAX1"].ToString();
                txtFaxNumber2.Text = dr["FAX2"].ToString();

                txtCompanyEmail1.Text = dr["EMAIL1"].ToString();
                txtCompanyEmail2.Text = dr["EMAIL2"].ToString();
                txtCompanyWebSite.Text = dr["Comp_WebSite"].ToString();

                txtCompanyRegNo.Text = dr["Comp_Reg_No"].ToString();
                txtGstTaxRegNo.Text = dr["Comp_Tex_Reg_No"].ToString();
                txtISOCertification.Text = dr["Comp_ISO_No"].ToString();
                ddlBillingCurrency.SelectedValue = dr["Supplier_Currency"].ToString();

                ddlTitle1.SelectedValue = dr["PIC_NAME_Title1"].ToString();
                ddlTitle2.SelectedValue = dr["PIC_NAME_Title2"].ToString();

                txtNamePIC1.Text = dr["PIC_NAME1"].ToString();
                txtNamePIC2.Text = dr["PIC_NAME2"].ToString();
                txtDesignation1.Text = dr["PIC_Designation1"].ToString();
                txtDesignation2.Text = dr["PIC_Designation2"].ToString();
                txtEmailPIC1.Text = dr["PIC_Email1"].ToString();
                txtEmailPIC2.Text = dr["PIC_Email2"].ToString();
                txtPhonePIC1.Text = dr["PIC_Phone1"].ToString();
                txtPhonePIC2.Text = dr["PIC_Phone2"].ToString();
                txtMobileNo.Text = dr["Primary_Contact_Mobile"].ToString();
                txtMobileNo2.Text = dr["Secondary_Contact_Mobile"].ToString();
                txtScope.Text = dr["ScopeName"].ToString();
                txtBankName.Text = dr["Bank_Name"].ToString();
                txtBeneficiaryName.Text = dr["Beneficiary_Name"].ToString();
                txtBankAddress.Text = dr["Bank_Address"].ToString();
                txtBeneficiaryAddress.Text = dr["Beneficiary_Address"].ToString();
                txtBankCode.Text = dr["Bank_Code"].ToString();
                txtBeneficiaryAccount.Text = dr["Beneficiary_Account"].ToString();
                txtBranchCode.Text = dr["Branch_Code"].ToString();
                ddlAccountCurrency.SelectedValue = dr["BENEFICIARY_ACCOUNT_CURR"].ToString();

                txtSwiftCode.Text = dr["SWIFT_Code"].ToString();
                txtNotifypayment.Text = dr["Payment_Notification_Name"].ToString();
                txtIBANCode.Text = dr["IBAN_Code"].ToString();
                txtNotificationEmail.Text = dr["Payment_Notification_Email"].ToString();
                txtVerifiedName.Text = dr["Verified_Person"].ToString();
                ddlDesignation.SelectedValue = dr["Designation"].ToString();

                txtSkypeAdd.Text = dr["SkypeAddress"].ToString();
                txtSkypeAdd2.Text = dr["SkypeAddress2"].ToString();
                txtTaxAccNumber.Text = dr["Tax_Account_Number"].ToString();
                txtBankInfo.Text = dr["Other_Bank_Information"].ToString();
                

                lblsuppCode.Text = txtSupplierCode.Text.ToString();
                lblCompanyName.Text = txtCompanyResgName.Text.ToString();
                lblCompAdd.Text = txtSuppAddress.Text.ToString();
                lblstate.Text = txtState.Text.ToString();
                lblcountry.Text = ddlCountry.SelectedItem.Text.ToString();
                lblPostal.Text = txtPostalCode.Text.ToString();
                lblTele1.Text = txtTelephone1.Text.ToString();
                lblTele2.Text = txtTelephone2.Text.ToString();
                lblAOHNo1.Text = txtAOHNumber1.Text.ToString();
                lblAOHNo2.Text = txtAOHNumber2.Text.ToString();
                lblFax1.Text = txtFaxNumber1.Text.ToString();
                lblFax2.Text = txtFaxNumber2.Text.ToString();
                lblEmail1.Text = txtCompanyEmail1.Text.ToString();
                lblEmail2.Text = txtCompanyEmail2.Text.ToString();
                lblCompWebsite.Text = txtCompanyWebSite.Text.ToString();

                lblCompNo.Text = txtCompanyRegNo.Text.ToString();
                lblGSTNo.Text = txtGstTaxRegNo.Text.ToString();
                lblISo.Text = txtISOCertification.Text.ToString();
                lblBillCur.Text = ddlBillingCurrency.SelectedItem.Text.ToString();
                lblScope.Text = txtScope.Text.ToString();
                lblAddService.Text = txtAddService.Text.ToString();
                lblPICName1.Text = ddlTitle1.SelectedItem.Text.ToString() + "" + txtNamePIC1.Text.ToString();
                lblPICDesig1.Text = txtDesignation1.Text.ToString();
                lblPICTele1.Text = txtPhonePIC1.Text.ToString();
                lblPICMobile1.Text = txtMobileNo.Text.ToString();
                lblPICEmail1.Text = txtEmailPIC1.Text.ToString();
                lblPICSAdd1.Text = txtSkypeAdd.Text.ToString();
                lblPICname2.Text = ddlTitle2.SelectedItem.Text.ToString() + "" + txtNamePIC2.Text.ToString();
                lblPICDesignation2.Text = txtDesignation2.Text.ToString();
                lblPICTele2.Text = txtPhonePIC2.Text.ToString();
                lblPICMobile2.Text = txtMobileNo2.Text.ToString();
                lblPICEmail2.Text = txtEmailPIC2.Text.ToString();
                lblSkypeAdd2.Text = txtSkypeAdd2.Text.ToString();
                lblbankName.Text = txtBankName.Text.ToString();
                lblBename.Text = txtBeneficiaryName.Text.ToString();
                lblBankAddress.Text = txtBankAddress.Text.ToString();
                lblBAdd.Text = txtBeneficiaryAddress.Text.ToString();
                lblBankCode.Text = txtBankCode.Text.ToString();
                lblBenAcc.Text = txtBeneficiaryAccount.Text.ToString();
                lblBCode.Text = txtBranchCode.Text.ToString();
                lblAccountCurrency.Text = ddlAccountCurrency.SelectedItem.Text.ToString();
                lblSwiftCocde.Text = txtSwiftCode.Text.ToString();
                lblPaymentTo.Text = txtNotifypayment.Text.ToString();
                lblIBAnCode.Text = txtIBANCode.Text.ToString();
                lblEmail.Text = txtNotificationEmail.Text.ToString();
                lblOtherBankInfo.Text = txtBankInfo.Text.ToString();
                lblverify.Text = txtVerifiedName.Text.ToString();
                lblVDesignation.Text = ddlDesignation.SelectedItem.Text.ToString();
                lblTaxAccNumber.Text = txtTaxAccNumber.Text.ToString();


                if (dr["ACTION_ON_DATE_FROM"].ToString() == "VERIFIED")
                {
                    File_CompanyUpload.Enabled = true;
                    FileUploader.Enabled = true;
                    btnPrint.Enabled = true;
                    EnableDisable(false);
                }
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }

     
    }
    /// <summary>
    /// This method is used to enable and disable data after finalize and lock
    /// </summary>
    /// <param name="blnFlag"></param>
    private void EnableDisable(bool blnFlag)
    {
        try
        {
            txtCompanyResgName.Enabled = blnFlag;
            txtSuppAddress.Enabled = blnFlag;
            txtState.Enabled = blnFlag;
            ddlCountry.Enabled = blnFlag;
            txtScope.Enabled = blnFlag;
            txtAddService.Enabled = blnFlag;
            txtPostalCode.Enabled = blnFlag;
            txtTelephone1.Enabled = blnFlag;
            txtTelephone2.Enabled = blnFlag;
            txtAOHNumber1.Enabled = blnFlag;
            txtAOHNumber2.Enabled = blnFlag;
            txtFaxNumber1.Enabled = blnFlag;
            txtFaxNumber2.Enabled = blnFlag;
            txtCompanyEmail1.Enabled = blnFlag;
            txtCompanyEmail2.Enabled = blnFlag;
            txtCompanyWebSite.Enabled = blnFlag;
            txtMobileNo.Enabled = blnFlag;
            txtMobileNo2.Enabled = blnFlag;
            txtCompanyRegNo.Enabled = blnFlag;
            txtGstTaxRegNo.Enabled = blnFlag;
            txtISOCertification.Enabled = blnFlag;
            txtBankInfo.Enabled = blnFlag;
            ddlBillingCurrency.Enabled = blnFlag;
            ddlTitle1.Enabled = blnFlag;
            ddlTitle2.Enabled = blnFlag;

            txtNamePIC1.Enabled = blnFlag;
            txtNamePIC2.Enabled = blnFlag;
            txtDesignation1.Enabled = blnFlag;
            txtDesignation2.Enabled = blnFlag;
            txtEmailPIC1.Enabled = blnFlag;
            txtEmailPIC2.Enabled = blnFlag;
            txtPhonePIC1.Enabled = blnFlag;
            txtPhonePIC2.Enabled = blnFlag;


            txtBankName.Enabled = blnFlag;
            txtBeneficiaryName.Enabled = blnFlag;
            txtBankAddress.Enabled = blnFlag;
            txtBeneficiaryAddress.Enabled = blnFlag;
            txtBankCode.Enabled = blnFlag;
            txtBeneficiaryAccount.Enabled = blnFlag;
            txtBranchCode.Enabled = blnFlag;
            ddlAccountCurrency.Enabled = blnFlag;

            txtSwiftCode.Enabled = blnFlag;
            txtNotifypayment.Enabled = blnFlag;
            txtIBANCode.Enabled = blnFlag;
            txtNotificationEmail.Enabled = blnFlag;
            txtVerifiedName.Enabled = blnFlag;
            ddlDesignation.Enabled = blnFlag;

            btnFinalize.Enabled = blnFlag;
            btnSaveDraft.Enabled = blnFlag;
            txtSkypeAdd.Enabled = blnFlag;
            txtSkypeAdd2.Enabled = blnFlag;
            txtTaxAccNumber.Enabled = blnFlag;
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }

    }
    protected void BindCountryDLL()
    {
        try
        {
            DataTable dt = objBLLCountry.Get_CountryList();

            ddlCountry.DataSource = dt;
            ddlCountry.DataTextField = "Country";
            ddlCountry.DataValueField = "ID";
            ddlCountry.DataBind();
            ddlCountry.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-SELECT-", "0"));
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    protected void BindCurrencyDLL()
    {
        try
        {
            DataTable dt = objBLLCurrency.Get_CurrencyList();

            ddlBillingCurrency.DataSource = dt;
            ddlBillingCurrency.DataTextField = "Currency_Code";
            ddlBillingCurrency.DataValueField = "Currency_Code";
            ddlBillingCurrency.DataBind();
             
            ddlAccountCurrency.DataSource = dt;
            ddlAccountCurrency.DataTextField = "Currency_Code";
            ddlAccountCurrency.DataValueField = "Currency_Code";
            ddlAccountCurrency.DataBind();
            ddlAccountCurrency.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-SELECT-", "0"));
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }


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

    protected void BindAttachment()
    {
        try
        {
            DataSet ds = BLL_ASL_Supplier.Get_Supplier_Attachment(UDFLib.ConvertStringToNull(GetSuppID()));
            DataTable dt = ds.Tables[2];

            gvAttachment.DataSource = ds.Tables[0];
            gvAttachment.DataBind();
            gvCompanyAttachment.DataSource = ds.Tables[1];
            gvCompanyAttachment.DataBind();
            gvAttachmentnew.DataSource = ds.Tables[0];
            gvAttachmentnew.DataBind();
            gvCompanyAttachmentNew.DataSource = ds.Tables[1];
            gvCompanyAttachmentNew.DataBind();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    protected void btnSaveDraft_Click(object sender, EventArgs e)
    {
        try
        {
            int RetValue = BLL_ASL_Supplier.Supplier_Data_Insert(UDFLib.ConvertIntegerToNull(ViewState["ReturnSupplierID"].ToString())
                , UDFLib.ConvertStringToNull(Request.QueryString["Supp_ID"])
                , UDFLib.ConvertStringToNull(txtCompanyResgName.Text.Trim()), UDFLib.ConvertStringToNull(txtSuppAddress.Text.Trim()), UDFLib.ConvertStringToNull(txtState.Text.Trim()), UDFLib.ConvertStringToNull(ddlCountry.SelectedItem.Text), UDFLib.ConvertIntegerToNull(ddlCountry.SelectedValue), UDFLib.ConvertStringToNull(txtPostalCode.Text),
                UDFLib.ConvertStringToNull(txtTelephone1.Text.Trim()), UDFLib.ConvertStringToNull(txtTelephone2.Text.Trim()), UDFLib.ConvertStringToNull(txtAOHNumber1.Text.Trim()), UDFLib.ConvertStringToNull(txtAOHNumber2.Text.Trim()), UDFLib.ConvertStringToNull(txtFaxNumber1.Text.Trim()), UDFLib.ConvertStringToNull(txtFaxNumber2.Text.Trim()), UDFLib.ConvertStringToNull(txtCompanyEmail1.Text.Trim()), UDFLib.ConvertStringToNull(txtCompanyEmail2.Text.Trim())
                , UDFLib.ConvertStringToNull(txtCompanyWebSite.Text.Trim()), UDFLib.ConvertStringToNull(txtCompanyRegNo.Text.Trim()), UDFLib.ConvertStringToNull(txtGstTaxRegNo.Text.Trim()), UDFLib.ConvertStringToNull(txtISOCertification.Text.Trim()), UDFLib.ConvertStringToNull(ddlBillingCurrency.SelectedItem.Text), UDFLib.ConvertIntegerToNull(ddlBillingCurrency.SelectedValue)
                , ddlTitle1.SelectedValue, ddlTitle2.SelectedValue, UDFLib.ConvertStringToNull(txtNamePIC1.Text.Trim()), UDFLib.ConvertStringToNull(txtNamePIC2.Text.Trim()), UDFLib.ConvertStringToNull(txtDesignation1.Text.Trim()), UDFLib.ConvertStringToNull(txtDesignation2.Text.Trim())
                , UDFLib.ConvertStringToNull(txtEmailPIC1.Text.Trim()), UDFLib.ConvertStringToNull(txtEmailPIC2.Text.Trim()), UDFLib.ConvertStringToNull(txtPhonePIC1.Text.Trim()), UDFLib.ConvertStringToNull(txtPhonePIC2.Text.Trim()), UDFLib.ConvertStringToNull(txtBankName.Text.Trim()), UDFLib.ConvertStringToNull(txtBankAddress.Text.Trim()), UDFLib.ConvertStringToNull(txtBankCode.Text.Trim())
                , UDFLib.ConvertStringToNull(txtBranchCode.Text.Trim()), UDFLib.ConvertStringToNull(txtSwiftCode.Text.Trim()), UDFLib.ConvertStringToNull(txtIBANCode.Text.Trim()), UDFLib.ConvertStringToNull(txtBeneficiaryName.Text.Trim()), UDFLib.ConvertStringToNull(txtBeneficiaryAddress.Text.Trim()), UDFLib.ConvertStringToNull(txtBeneficiaryAccount.Text.Trim())
                , UDFLib.ConvertStringToNull(ddlAccountCurrency.SelectedValue), UDFLib.ConvertStringToNull(txtNotifypayment.Text.Trim()), UDFLib.ConvertStringToNull(txtNotificationEmail.Text.Trim()), UDFLib.ConvertStringToNull(txtVerifiedName.Text.Trim())
                , UDFLib.ConvertStringToNull(ddlDesignation.SelectedValue), "DRAFT", UDFLib.ConvertToInteger(Session["UserID"].ToString()), UDFLib.ConvertStringToNull(txtSkypeAdd.Text.Trim()), UDFLib.ConvertStringToNull(txtSkypeAdd2.Text.Trim()),
                UDFLib.ConvertStringToNull(txtMobileNo.Text.Trim()), UDFLib.ConvertStringToNull(txtMobileNo2.Text.Trim()), UDFLib.ConvertStringToNull(txtScope.Text.Trim()), UDFLib.ConvertStringToNull(txtAddService.Text.Trim()), UDFLib.ConvertStringToNull(txtBankInfo.Text.Trim()), UDFLib.ConvertStringToNull(txtTaxAccNumber.Text));

            ViewState["ReturnSupplierID"] = RetValue;

            BindSupplierDataList();

            string message = "alert('Supplier data saved as a Draft.')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    protected void btnFinalize_Click(object sender, EventArgs e)
    {
        try
        {
            int RetValue = BLL_ASL_Supplier.Supplier_Data_Insert(UDFLib.ConvertIntegerToNull(ViewState["ReturnSupplierID"].ToString())
                 , UDFLib.ConvertStringToNull(Request.QueryString["Supp_ID"])
                 , UDFLib.ConvertStringToNull(txtCompanyResgName.Text.Trim()), UDFLib.ConvertStringToNull(txtSuppAddress.Text.Trim()), UDFLib.ConvertStringToNull(txtState.Text.Trim()), UDFLib.ConvertStringToNull(ddlCountry.SelectedItem.Text), UDFLib.ConvertIntegerToNull(ddlCountry.SelectedValue), UDFLib.ConvertStringToNull(txtPostalCode.Text),
                 UDFLib.ConvertStringToNull(txtTelephone1.Text.Trim()), UDFLib.ConvertStringToNull(txtTelephone2.Text.Trim()), UDFLib.ConvertStringToNull(txtAOHNumber1.Text.Trim()), UDFLib.ConvertStringToNull(txtAOHNumber2.Text.Trim()), UDFLib.ConvertStringToNull(txtFaxNumber1.Text.Trim()), UDFLib.ConvertStringToNull(txtFaxNumber2.Text.Trim()), UDFLib.ConvertStringToNull(txtCompanyEmail1.Text.Trim()), UDFLib.ConvertStringToNull(txtCompanyEmail2.Text.Trim())
                 , UDFLib.ConvertStringToNull(txtCompanyWebSite.Text.Trim()), UDFLib.ConvertStringToNull(txtCompanyRegNo.Text.Trim()), UDFLib.ConvertStringToNull(txtGstTaxRegNo.Text.Trim()), UDFLib.ConvertStringToNull(txtISOCertification.Text.Trim()), UDFLib.ConvertStringToNull(ddlBillingCurrency.SelectedItem.Text), UDFLib.ConvertIntegerToNull(ddlBillingCurrency.SelectedValue)
                 , ddlTitle1.SelectedValue, ddlTitle2.SelectedValue, UDFLib.ConvertStringToNull(txtNamePIC1.Text.Trim()), UDFLib.ConvertStringToNull(txtNamePIC2.Text.Trim()), UDFLib.ConvertStringToNull(txtDesignation1.Text.Trim()), UDFLib.ConvertStringToNull(txtDesignation2.Text.Trim())
                 , UDFLib.ConvertStringToNull(txtEmailPIC1.Text.Trim()), UDFLib.ConvertStringToNull(txtEmailPIC2.Text.Trim()), UDFLib.ConvertStringToNull(txtPhonePIC1.Text.Trim()), UDFLib.ConvertStringToNull(txtPhonePIC2.Text.Trim()), UDFLib.ConvertStringToNull(txtBankName.Text.Trim()), UDFLib.ConvertStringToNull(txtBankAddress.Text.Trim()), UDFLib.ConvertStringToNull(txtBankCode.Text.Trim())
                 , UDFLib.ConvertStringToNull(txtBranchCode.Text.Trim()), UDFLib.ConvertStringToNull(txtSwiftCode.Text.Trim()), UDFLib.ConvertStringToNull(txtIBANCode.Text.Trim()), UDFLib.ConvertStringToNull(txtBeneficiaryName.Text.Trim()), UDFLib.ConvertStringToNull(txtBeneficiaryAddress.Text.Trim()), UDFLib.ConvertStringToNull(txtBeneficiaryAccount.Text.Trim())
                 , UDFLib.ConvertStringToNull(ddlAccountCurrency.SelectedValue), UDFLib.ConvertStringToNull(txtNotifypayment.Text.Trim()), UDFLib.ConvertStringToNull(txtNotificationEmail.Text.Trim()), UDFLib.ConvertStringToNull(txtVerifiedName.Text.Trim())
                 , UDFLib.ConvertStringToNull(ddlDesignation.SelectedValue), "VERIFIED", UDFLib.ConvertToInteger(Session["UserID"].ToString()), UDFLib.ConvertStringToNull(txtSkypeAdd.Text.Trim()), UDFLib.ConvertStringToNull(txtSkypeAdd2.Text.Trim()),
                 UDFLib.ConvertStringToNull(txtMobileNo.Text.Trim()), UDFLib.ConvertStringToNull(txtMobileNo2.Text.Trim()), UDFLib.ConvertStringToNull(txtScope.Text.Trim()), UDFLib.ConvertStringToNull(txtAddService.Text.Trim()), UDFLib.ConvertStringToNull(txtBankInfo.Text.Trim()), UDFLib.ConvertStringToNull(txtTaxAccNumber.Text));


            ViewState["ReturnSupplierID"] = RetValue;

            BindSupplierDataList();
            string message = "alert('Supplier data has been Verified.')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }

    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        try
        {
            if (FileUploader.HasFile)
            {
                if (FileUploader.FileName != "")
                {
                    string strLocalPath = FileUploader.PostedFile.FileName;
                    string FileName = Path.GetFileName(strLocalPath);
                    string FileExtension = Path.GetExtension(strLocalPath);
                    string TYPE = "00001";
                    string DocType = "SUPPLIER";
                    Guid FileGuid = System.Guid.NewGuid();
                    string Supplier_Code = UDFLib.ConvertStringToNull(GetSuppID());
                    string path = Server.MapPath(@"~/Uploads\\ASL\\");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    FileUploader.PostedFile.SaveAs(Server.MapPath(@"~/Uploads\\ASL\\" + FileGuid + FileExtension));


                    BLL_ASL_Supplier.Supplier_Attachment_Insert(UDFLib.ConvertStringToNull(GetSuppID()), FileName, FileGuid + FileExtension, FileExtension
                      , UDFLib.ConvertIntegerToNull(Session["userid"]), TYPE, DocType);

                    BindAttachment();

                }
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
   
    protected void btnCompanyUpload_Click(object sender, EventArgs e)
    {
        try
        {
            if (File_CompanyUpload.FileName != "")
            {
                string strLocalPath = File_CompanyUpload.PostedFile.FileName;
                string FileName = Path.GetFileName(strLocalPath);
                string FileExtension = Path.GetExtension(strLocalPath);
                string TYPE = "00002";
                string DocType = "SUPPLIER";
                Guid FileGuid = System.Guid.NewGuid();
                string path = Server.MapPath(@"~/Uploads\\ASL\\");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                File_CompanyUpload.PostedFile.SaveAs(Server.MapPath(@"~/Uploads\\ASL\\" + FileGuid + FileExtension));
                string Supplier_Code = UDFLib.ConvertStringToNull(GetSuppID());


                BLL_ASL_Supplier.Supplier_Attachment_Insert(UDFLib.ConvertStringToNull(GetSuppID()), FileName, FileGuid + FileExtension, FileExtension
                      , UDFLib.ConvertIntegerToNull(Session["userid"]), TYPE, DocType);

                BindAttachment();

            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    protected void ImgAttDelete_Click(object sender, CommandEventArgs e)
    {
        try
        {
            string[] cmdargs = e.CommandArgument.ToString().Split(',');

            string fileid = cmdargs[0].ToString();
            string fileName = cmdargs[1].ToString();

            string filepath = "../uploads/ASL/" + fileName;

            int retval = BLL_ASL_Supplier.Supplier_Attachment_Delete(Convert.ToInt32(fileid), UDFLib.ConvertStringToNull(GetSuppID()), UDFLib.ConvertIntegerToNull(Session["userid"]));


            if (File.Exists(Server.MapPath(filepath)))
                File.Delete(Server.MapPath(filepath));

            BindAttachment();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }

    }
    protected void ImgCompanyAttDelete_Click(object sender, CommandEventArgs e)
    {
        try
        {
            string[] cmdargs = e.CommandArgument.ToString().Split(',');

            string fileid = cmdargs[0].ToString();
            string fileName = cmdargs[1].ToString();

            string filepath = "../uploads/ASL/" + fileName;

            int retval = BLL_ASL_Supplier.Supplier_Attachment_Delete(Convert.ToInt32(fileid), UDFLib.ConvertStringToNull(GetSuppID()), UDFLib.ConvertIntegerToNull(Session["userid"]));


            if (File.Exists(Server.MapPath(filepath)))
                File.Delete(Server.MapPath(filepath));

            BindAttachment();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }

    }

   
}