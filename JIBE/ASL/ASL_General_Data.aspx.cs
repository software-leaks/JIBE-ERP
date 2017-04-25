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
using System.Configuration;

public partial class ASL_ASL_General_Data : System.Web.UI.Page
{
    BLL_Infra_Currency objBLLCurrency = new BLL_Infra_Currency();
    BLL_Infra_Country objBLLCountry = new BLL_Infra_Country();
    UserAccess objUA = new UserAccess();
    UserAccess objUAType = new UserAccess();
    public string Supplier = null;
    public Boolean uaEditFlag = false;
    public Boolean uaDeleteFlage = false;
    public Boolean ChangeRequest = false;
    public Boolean Registered = false;
    public string OperationMode = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        UserAccessValidation();
        //UserAccessTypeValidation();
        if (!IsPostBack)
        {
            ViewState["Registered"] = 0;
            ViewState["ChangeRequest"] = 0;
            ViewState["Registered"] = 0;
            //txtSupplierCode.Text = "00000";
            txtSupplierCode.Text = GetSuppID();
            rdbInvoiceStatus.SelectedValue = "Yes";
            rdbdirectinvoice.SelectedValue = "Yes";
            rdbPaymentHistory.SelectedValue = "Yes";
            rdbSendPo.SelectedValue = "Yes";
            rdbPaymentPriority.SelectedValue = "Normal";
          
            
            //string SupplierID = GetSuppID();
            BindService();
            
            BindCurrencyDLL();
            BindOwnerShip();
            BindPaymentInterval();
            BindPaymenterms();
            BindSubType();
            BindCountry();
            BindSupplierDetails();
           
            //BindRemarksGrid();
        }
    }
    protected void BindProperties()
    {
        DataSet ds_Depart = BLL_ASL_Supplier.Get_Supplier_Properties(txtSupplierCode.Text.ToString(), GetSessionUserID());

        gvProperties.DataSource = ds_Depart.Tables[0];
        gvProperties.DataTextField = "Property_Description";
        gvProperties.DataValueField = "Property_ID";
        gvProperties.DataBind();
        
        for (int i = 0; i < ds_Depart.Tables[0].Rows.Count; i++)
        {
            if (ds_Depart.Tables[0].Rows[i]["Applicable_Flag"].ToString() == "1")
            {
                ListItem list1 = gvProperties.Items.FindByValue(ds_Depart.Tables[0].Rows[i]["Property_ID"].ToString());
                if (list1 != null)
                    list1.Selected = true;
            }
        }

    }
    protected void BindRemarksGrid()
    {
        string SuppID = UDFLib.ConvertStringToNull(txtSupplierCode.Text.ToString());
        DataTable dt = BLL_ASL_Supplier.Get_Supplier_Remarks(SuppID);
        //gvRemarks.DataSource = dt;
        //gvRemarks.DataBind();

    }
    protected void UserAccessTypeValidation(string Variable_Code)
    {
        int CurrentUserID = GetSessionUserID();
        //string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        BLL_TypeManagement objType = new BLL_TypeManagement();
        string Variable_Type = "Supplier_Type";
        string Approver_Type = null;
        objUAType = objType.Get_UserTypeAccess(CurrentUserID, Variable_Type, Variable_Code, Approver_Type);
        if (objUAType.Add == 0)
        {
            btnSave.Enabled = false;
            btnSubmit.Enabled = false;
        }
        else
        {
            btnSave.Enabled = true;
            btnSubmit.Enabled = true;
        }
    }
    protected void BindCountry()
    {

        DataTable dt = objBLLCountry.Get_CountryList();

        ddlCountry.DataSource = dt;
        ddlCountry.DataTextField = "Country";
        ddlCountry.DataValueField = "ID";
        ddlCountry.DataBind();
        ddlCountry.Items.Insert(0, new ListItem("-SELECT-", "0"));

    }
    protected void BindScope()
    {
        DataSet ds = BLL_ASL_Supplier.Get_Supplier_Scope(UDFLib.ConvertStringToNull(txtSupplierCode.Text.ToString()));
        //Session["dtScope"] = dt;
        ddlScope.DataSource = ds.Tables[0];
        ddlScope.DataTextField = "Scope_Name";
        ddlScope.DataValueField = "Scope_ID";
        ddlScope.DataBind();
        chkScope.DataSource = ds.Tables[1];
        chkScope.DataTextField = "Scope_Name";
        chkScope.DataValueField = "Scope_ID";
        chkScope.DataBind();
        Session["dtScope"] = ds.Tables[1];
        int i = 0;
        foreach (ListItem chkitem in chkScope.Items)
        {
            chkitem.Selected = true;
            i++;
        }

    }
    protected void BindPort()
    {

        DataSet ds = BLL_ASL_Supplier.Get_Supplier_PORT(UDFLib.ConvertStringToNull(txtSupplierCode.Text.ToString()));
        //Session["dtPort"] = dt;
        ddlPort.DataSource = ds.Tables[0];
        ddlPort.DataTextField = "PORT_NAME";
        ddlPort.DataValueField = "PORT_ID";
        ddlPort.DataBind();
        chkPort.DataSource = ds.Tables[1];
        chkPort.DataTextField = "PORT_NAME";
        chkPort.DataValueField = "PORT_ID";
        chkPort.DataBind();

        Session["dtPort"] = ds.Tables[1];
        int i = 0;
        foreach (ListItem chkitem in chkPort.Items)
        {
            chkitem.Selected = true;
            i++;
        }
    }
    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
        {
            pnlGeneral.Visible = false;
            lblMsg.Text = "You don't have sufficient previlege to access the requested information.";
        }
        else
        {
            pnlGeneral.Visible = true;
        }
        if (objUA.Add == 0)
        {
            btnSave.Enabled = false;
            btnSubmit.Enabled = false;
        }
        else
        {
            btnSave.Enabled = true;
            btnSubmit.Enabled = true;
        }
        //if (objUA.Edit == 1)
        //    uaEditFlag = true;
        //else
        //    // btnsave.Visible = false;

        //    if (objUA.Delete == 1) uaDeleteFlage = true;

    }
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }
    /// <summary>
    /// Bind ASL Record .
    /// modify text - Add new field to Bind.
    /// Date - 06/12/2016
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void BindSupplierDetails()
    {
        try
        {
            //string SupplierID = GetSuppID();
            //ViewState["ReturnSupplierCode"] = SupplierID;
            BindScope();
            BindPort();
            BindProperties();
            DataSet ds = BLL_ASL_Supplier.Get_Supplier_General_Data_List(UDFLib.ConvertStringToNull(txtSupplierCode.Text.ToString()));
            if (ds.Tables[2].Rows.Count > 0)
            {
                divAutoPayment.Visible = true;
                lblAutoPayment.ForeColor = System.Drawing.Color.Blue;
                DataRow dr = ds.Tables[2].Rows[0];
                lblSource.Text = dr["Pay_From_Account"].ToString();
                lblPaymentMode.Text = dr["Payment_Mode"].ToString();
                lblPayment.Text = dr["Payment_Currency"].ToString();
                lblReceivingAmt.Text = dr["Receiving_Account_Number"].ToString();
                lblBankCode.Text = dr["Receiving_Bank_Code"].ToString();
                lblBranch.Text = dr["Receiving_Branch_Code"].ToString();
                lblBene.Text = dr["Receiving_Beneficiary_Name"].ToString();
                lblSwift.Text = dr["SWIFT_IBAN_CODE"].ToString();
                lblDesti.Text = dr["Destination_ABA"].ToString();
                lblABA.Text = dr["Intermediary_ABA"].ToString();
                lblBankState.Text = dr["Bank_State_Code"].ToString();
            }
            else
            {
                divAutoPayment.Visible = false;
                lblAutoPayment.Text = "No Auto Payment Setup";
                lblAutoPayment.ForeColor = System.Drawing.Color.Red;
                //lblAutoPayment.Visible = "No Auto Payment Setup";

            }
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                BindType(1);
                
                ViewState["ReturnSupplierID"] = dr["Supp_ID"].ToString();
                lblSupplierCode.Text = dr["Supp_ID"].ToString();
                Session["Supplier_Name"] = dr["Register_Name"].ToString();
                lblstatus.Text = dr["Supp_Status"].ToString();
                txtCompanyResgName.Text = dr["Register_Name"].ToString();
                ddlCountry.SelectedValue = dr["COUNTRYID"].ToString();
                txtSuppAddress.Text = dr["Address"].ToString();
                txtshortName.Text = dr["Supplier_Short_Name"].ToString();
                Session["Supplier_Type"] = dr["Supp_Type"].ToString();
                lblSupplierType.Text =  "(" + dr["Supp_Type"].ToString() + ")";
                ddlType.SelectedValue = dr["Supp_Type"].ToString();
                ddlCurrency.SelectedValue = dr["Supplier_Currency"].ToString();
                ddlgroup.SelectedValue = dr["Supp_Service"].ToString();
                ddlownerShip.SelectedValue = dr["Ownership_Type"].ToString();
                txtCompanyEmail.Text = dr["Email"].ToString();
                txtTelephone.Text = dr["Phone"].ToString();
                txtCity.Text = dr["CITY"].ToString();
                txtFaxNumber.Text = dr["Fax"].ToString();
                txtNamePIC1.Text = dr["Contact_1"].ToString();
                txtNamePIC2.Text = dr["Contact_2"].ToString();
                txtEmailPIC1.Text = dr["Contact_1_Email"].ToString();
                txtEmailPIC2.Text = dr["Contact_2_Email"].ToString();
                txtPhonePIC1.Text = dr["Contact_1_Phone"].ToString();
                txtPhonePIC2.Text = dr["Contact_2_Phone"].ToString();
                Payment_Instructions.Text = dr["Payment_Instructions"].ToString();
                Payment_Notifications.Text = dr["Payment_Notifications"].ToString();
                txtBizCorporation.Text = dr["Setup_Year"].ToString();
                txtTaxRate.Text = dr["GST_Rate"].ToString();
                txtTerms.Text = dr["Payment_Terms"].ToString();
                txtSupplierDesc.Text = dr["Supplier_Description"].ToString();
                ddlSubType.SelectedValue = dr["Counterparty_Type"].ToString();
                //ddlSupplier.SelectedValue = dr["Supp_ID"].ToString();
                lbldate.Text = dr["CREATEDDATE"].ToString();
                lblCreatedBY.Text = dr["CREATEDNAME"].ToString();
                lblUpdatedby.Text = dr["UPDATEDNAME"].ToString();
                lblUpdatedDate.Text = dr["UPDATEDDATE"].ToString();
                txtTaxAccNumber.Text = dr["Tax_Account_Number"].ToString();
                rdbInvoiceStatus.SelectedValue = dr["Invoice_Status_Enabled"].ToString();
                rdbdirectinvoice.SelectedValue = dr["Invoice_Upload_Enabled"].ToString();
                rdbPaymentHistory.SelectedValue = dr["Payment_History_Enabled"].ToString();
                rdbSendPo.SelectedValue = dr["Auto_Send_PO"].ToString();
                rdbPaymentPriority.SelectedValue = dr["Payment_Priority"].ToString();
                ddlPaymentInterval.SelectedValue = dr["Payment_Interval"].ToString();
                ddlPaymentTerms.SelectedValue = dr["Payment_Terms_Days"].ToString();
                txtCompany_Reg_No.Text = dr["Company_Reg_No"].ToString();
                txtGST_Registration_No.Text = dr["GST_Registration_No"].ToString();
                txtWithholding_Tax_Rate.Text = dr["Withholding_Tax_Rate"].ToString();
                txtPassString.Text = dr["Passstring"].ToString();

                txtShipSmart_Supplier_Code.Text = dr["ShipSmart_Supplier_Code"].ToString();
                txtSupplier_Short_Code.Text = dr["Supplier_Short_Code"].ToString();

                if(dr["last_Payment_Date"].ToString() != "")
                {
                    DateTime dt = Convert.ToDateTime(dr["last_Payment_Date"].ToString());
                    lblLastPayment.Text = dt.ToString("dd-MMM-yyyy");
                }

                UserAccessTypeValidation(Session["Supplier_Type"].ToString());
                if (dr["RowNo"].ToString() == "0")
                {
                    btnSave.Enabled = true;
                }
                else
                {
                    if (dr["Evaluation_Status"].ToString() == "Rework")
                    {
                        EnableDisable(true);
                    }
                    else
                    {
                        EnableDisable(false);
                    }
               }
                
                if (dr["Record_Status"].ToString() == "VERIFIED")
                {
                    if (objUAType.Admin == 1)
                    {
                        btnUnverify.Visible = true;
                    }
                    else
                    {
                        btnUnverify.Visible = false;
                    }
                }
                else
                {
                    btnUnverify.Visible = false;
                }
                if (dr["Record_Status"].ToString() != "")
                {

                    ViewState["Registered"] = 1;
                    //btnRegisteredData.Enabled = true;

                }
                else
                {
                    ViewState["Registered"] = 1;
                    //btnRegisteredData.Enabled = false;
                    //btnSubmit.Enabled = true;
                }
                //dvbutton.Visible = true;
                
                
            }
            else
            {
                BindType(2);
                clearControl();
            }
         
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
        
    }
    private void EnableDisable(bool blnFlag)
    {
        txtCompanyResgName.Enabled = blnFlag;
        txtSuppAddress.Enabled = blnFlag;
        ddlCountry.Enabled = blnFlag;
        txtshortName.Enabled = blnFlag;
        ddlType.Enabled = blnFlag;
        txtCity.Enabled = blnFlag;
        ddlCurrency.Enabled = blnFlag;
        ddlgroup.Enabled = blnFlag;
        ddlownerShip.Enabled = blnFlag;
        txtBizCorporation.Enabled = blnFlag;
        txtCompanyEmail.Enabled = blnFlag;
        txtTelephone.Enabled = blnFlag;
        txtFaxNumber.Enabled = blnFlag;
        txtNamePIC1.Enabled = blnFlag;

        txtEmailPIC1.Enabled = blnFlag;
        txtPhonePIC1.Enabled = blnFlag;
        txtNamePIC2.Enabled = blnFlag;
        ddlSubType.Enabled = blnFlag;
        txtSupplierDesc.Enabled = blnFlag;
        txtEmailPIC2.Enabled = blnFlag;
        txtPhonePIC2.Enabled = blnFlag;
        txtTerms.Enabled = blnFlag;
        txtTaxAccNumber.Enabled = blnFlag;
        txtTaxRate.Enabled = blnFlag;
        Payment_Instructions.Enabled = blnFlag;
        Payment_Notifications.Enabled = blnFlag;
        ddlPaymentInterval.Enabled = blnFlag;
        ddlPaymentTerms.Enabled = blnFlag;
        rdbdirectinvoice.Enabled = blnFlag;
        rdbInvoiceStatus.Enabled = blnFlag;
        rdbPaymentHistory.Enabled = blnFlag;
        rdbPaymentPriority.Enabled = blnFlag;
        rdbSendPo.Enabled = blnFlag;
        btnSave.Enabled = blnFlag;
        //btnSubmit.Enabled = blnFlag;
        ddlScope.Enabled = blnFlag;
        ddlPort.Enabled = blnFlag;
        chkPort.Enabled = blnFlag;
        chkScope.Enabled = blnFlag;
        btnScopeAdd.Enabled = blnFlag;
        btnPortAdd.Enabled = blnFlag;
        gvProperties.Enabled = blnFlag;
        txtWithholding_Tax_Rate.Enabled = blnFlag;
        txtGST_Registration_No.Enabled = blnFlag;
        txtCompany_Reg_No.Enabled = blnFlag;
        txtShipSmart_Supplier_Code.Enabled = blnFlag;
        txtSupplier_Short_Code.Enabled = blnFlag;

    }
    protected void BindPaymentInterval()
    {
        DataTable dt = BLL_ASL_Supplier.Get_ASL_System_Parameter(36, null, UDFLib.ConvertToInteger(GetSessionUserID()));

        ddlPaymentInterval.DataSource = dt;
        ddlPaymentInterval.DataValueField = "Code";
        ddlPaymentInterval.DataTextField = "Name";
        ddlPaymentInterval.DataBind();
        ddlPaymentInterval.SelectedValue = "41";
    }
    protected void BindPaymenterms()
    {
        DataTable dt = BLL_ASL_Supplier.Get_ASL_System_Parameter(43, null, UDFLib.ConvertToInteger(GetSessionUserID()));

        ddlPaymentTerms.DataSource = dt;
        ddlPaymentTerms.DataValueField = "Code";
        ddlPaymentTerms.DataTextField = "Name";
        ddlPaymentTerms.DataBind();
        ddlPaymentTerms.SelectedValue = "44";
    }
    protected void BindOwnerShip()
    {
        DataTable dt = BLL_ASL_Supplier.Get_ASL_System_Parameter(26, null, UDFLib.ConvertToInteger(GetSessionUserID()));

        ddlownerShip.DataSource = dt;
        ddlownerShip.DataValueField = "Code";
        ddlownerShip.DataTextField = "Name";
        ddlownerShip.DataBind();
        ddlownerShip.SelectedValue = "30";
    }
    protected void BindSubType()
    {
        int Type = 0;
        DataTable dt = BLL_ASL_Supplier.Get_ASL_System_Parameter(Type, null, UDFLib.ConvertToInteger(GetSessionUserID()));

        ddlSubType.DataSource = dt;
        ddlSubType.DataValueField = "Name";
        ddlSubType.DataTextField = "Description";
        ddlSubType.DataBind();
        ddlSubType.Items.Insert(0, new ListItem("-SELECT-", "0"));
    }
    protected void BindType(int Type)
    {
        DataTable dt = BLL_ASL_Supplier.Get_ASL_System_Parameter(Type, null, UDFLib.ConvertToInteger(GetSessionUserID()));

        ddlType.DataSource = dt;
        ddlType.DataValueField = "Description";
        ddlType.DataTextField = "Name";
        ddlType.DataBind();
        ddlType.Items.Insert(0, new ListItem("-SELECT-", "0"));
    }
    protected void BindService()
    {
        int rowcount = 0;
        BLL_ASL_Lib objBLL = new BLL_ASL_Lib();
        DataTable dt = objBLL.SupplierService_Search(null, null, 1, 1, 1000, ref  rowcount);
        ddlgroup.DataSource = dt;
        ddlgroup.DataTextField = "SERVICE_NAME";
        ddlgroup.DataValueField = "ID";
        ddlgroup.DataBind();
        ddlgroup.Items.Insert(0, new ListItem("-No Group-", "0"));
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
    public string GetAddEvalID()
    {
        try
        {
                return "10";
        }
        catch { return "0"; }
    }
    public string GetLastEvalID()
    {
        try
        {
            return "11";
        }
        catch { return "0"; }
    }
    public string GetSuppID()
    {
        try
        {
            if (txtSupplierCode.Text.Trim() != "")
            {
                txtSupplierCode.Text = Supplier;
                return txtSupplierCode.Text.ToString();
            }
            else if (Request.QueryString["Supp_ID"] != null)
            {
                return Request.QueryString["Supp_ID"].ToString();
            }
           
            else
                return "00000";
        }
        catch { return "00000"; }
    }

   
   
    protected void SaveSupplierScope()
    {
        DataTable dtScope = new DataTable();
        dtScope.Columns.Add("PKID");
        dtScope.Columns.Add("FKID");
        dtScope.Columns.Add("Value");

        foreach (ListItem chkitem in chkScope.Items)
        {
            DataRow dr = dtScope.NewRow();
            dr["FKID"] = chkitem.Selected == true ? 1 : 0;
            dr["Value"] = chkitem.Value; 
            dtScope.Rows.Add(dr);
        }
        BLL_ASL_Supplier.ASL_Supplier_Scope_Insert(UDFLib.ConvertStringToNull(txtSupplierCode.Text.ToString()), dtScope, UDFLib.ConvertToInteger(GetSessionUserID()));
    }

    protected void SaveSupplierPort()
    {

        DataTable dtService = new DataTable();
        dtService.Columns.Add("PKID");
        dtService.Columns.Add("FKID");
        dtService.Columns.Add("Value");

        foreach (ListItem chkitem in chkPort.Items)
        {
            DataRow dr = dtService.NewRow();
            dr["FKID"] = chkitem.Value;
            dr["Value"] = chkitem.Selected == true ? 1 : 0;
            dtService.Rows.Add(dr);
        }
        BLL_ASL_Supplier.ASL_Supplier_Port_Insert(UDFLib.ConvertStringToNull(txtSupplierCode.Text.ToString()), dtService, UDFLib.ConvertToInteger(GetSessionUserID()));
    }
    protected void clearControl()
    {
        txtCompanyResgName.Text = "";
        txtSuppAddress.Text = "";
        ddlCountry.SelectedValue = "0";
        txtshortName.Text = "";
        ddlType.SelectedValue = "0";
        txtCity.Text = "";
        ddlgroup.SelectedValue = "0";
        ddlCurrency.SelectedValue = "0";
        btnSubmit.Enabled = true;
        ddlownerShip.SelectedValue = "30";
        txtBizCorporation.Text = "";
        txtCompanyEmail.Text = "";
        txtTelephone.Text = "";
        txtFaxNumber.Text = "";
        txtNamePIC1.Text = "";
        //dvbutton.Visible = false;
        txtEmailPIC1.Text = "";
        txtPhonePIC1.Text = "";
        txtNamePIC2.Text = "";
        ddlSubType.SelectedValue = "0";
        txtSupplierDesc.Text = "";
        txtEmailPIC2.Text = "";
        txtPhonePIC2.Text = "";
        txtTerms.Text = "";
        ViewState["ReturnSupplierID"] = "0";
        txtSupplierCode.Text = "00000";
        txtTaxRate.Text = "0.00";
        Payment_Instructions.Text = "";
        Payment_Notifications.Text = "";
        DataTable dtScope = (DataTable)Session["dtScope"];
        dtScope.Clear();
        chkScope.DataSource = dtScope;
        chkScope.DataBind();
        btnScopeRemove.Visible = false;
        DataTable dtPort = (DataTable)Session["dtPort"];
        dtPort.Clear();
        chkPort.DataSource = dtPort;
        chkPort.DataBind();
        btnPortRemove.Visible = false;
        lbldate.Text = "";
        lblCreatedBY.Text = "";
        lblUpdatedby.Text = "";
        lblUpdatedDate.Text = "";
        txtTaxAccNumber.Text = "";
        txtWithholding_Tax_Rate.Text = "";
        txtGST_Registration_No.Text = "";
        txtCompany_Reg_No.Text = "";
        gvProperties.ClearSelection();

    }
    protected void btnRegisteredData_Click(object sender, EventArgs e)
    {
        string SupplierID = txtSupplierCode.Text.ToString().ToString();
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "javascript:window.open('ASL_Data_Entry.aspx?Supp_ID=" + SupplierID + "');", true);
    }
   /// <summary>
   /// Save record into database.
   /// modify text - Add new field to insert .
   /// Date - 06/12/2016
   /// </summary>
   /// <param name="sender"></param>
   /// <param name="e"></param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {

            string SupplierCode = txtSupplierCode.Text.ToString();
        string RetValue = BLL_ASL_Supplier.Supplier_General_Data_Insert(UDFLib.ConvertIntegerToNull(ViewState["ReturnSupplierID"].ToString())
           , UDFLib.ConvertStringToNull(SupplierCode), UDFLib.ConvertStringToNull(ddlType.SelectedValue), UDFLib.ConvertStringToNull(lblstatus.Text)
           , txtCompanyResgName.Text.Trim(), txtshortName.Text.Trim(), UDFLib.ConvertIntegerToNull(ddlgroup.SelectedValue), txtSuppAddress.Text.Trim(),
           UDFLib.ConvertStringToNull(ddlCountry.SelectedItem.Text.ToString()), txtCity.Text.Trim(), txtTelephone.Text.Trim(), txtFaxNumber.Text.Trim(), txtCompanyEmail.Text.Trim(),
           UDFLib.ConvertStringToNull(ddlCurrency.SelectedValue), txtNamePIC1.Text.Trim(), txtNamePIC2.Text.Trim(), txtEmailPIC1.Text.Trim(), txtEmailPIC2.Text.Trim(), txtPhonePIC1.Text.Trim(),
           txtPhonePIC2.Text.Trim(), UDFLib.ConvertIntegerToNull(ddlownerShip.SelectedValue), Payment_Instructions.Text.Trim()
           , Payment_Notifications.Text.Trim(), txtBizCorporation.Text.Trim(), rdbInvoiceStatus.SelectedValue, rdbdirectinvoice.SelectedValue, rdbPaymentHistory.SelectedValue, rdbSendPo.SelectedValue, rdbPaymentPriority.SelectedValue,
             UDFLib.ConvertIntegerToNull(ddlPaymentInterval.SelectedValue), UDFLib.ConvertIntegerToNull(ddlPaymentTerms.SelectedValue),
           txtTerms.Text.Trim(), UDFLib.ConvertDecimalToNull(txtTaxRate.Text.Trim()), "DRAFT", UDFLib.ConvertToInteger(GetSessionUserID()), UDFLib.ConvertIntegerToNull(ddlCountry.SelectedValue), txtSupplierDesc.Text.Trim(),
           ddlSubType.SelectedValue, txtTaxAccNumber.Text.Trim(), txtCompany_Reg_No.Text, txtGST_Registration_No.Text, UDFLib.ConvertDecimalToNull(txtWithholding_Tax_Rate.Text),txtSupplier_Short_Code.Text.Trim(),txtShipSmart_Supplier_Code.Text.Trim());
        //
        if (RetValue == "00000")
        {
            string message = "alert('Supplier Name Already Existed.')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
        else
        {
            ViewState["ReturnSupplierID"] = RetValue;
            txtSupplierCode.Text = RetValue;
            SaveSupplierScope();
            SaveSupplierPort();
            SaveSupplierProperties();
           
            string message = "alert('Supplier data saved as a Draft.')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            string msgDraft = String.Format("parent.ReloadParent_ByButtonID();");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgDraft", msgDraft, true);
            //BindSupplierDetails();
        }
        //Response.Redirect("../ASL/ASL_General_Data.aspx?Supp_ID=RetValue");
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    /// <summary>
    /// Save record into database.
    /// modify text - Add new field to insert .
    /// Date - 06/12/2016
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
         try
        {
            string SupplierCode = txtSupplierCode.Text.ToString();
        string RetValue = BLL_ASL_Supplier.Supplier_General_Data_Insert(UDFLib.ConvertIntegerToNull(ViewState["ReturnSupplierID"].ToString())
           , UDFLib.ConvertStringToNull(SupplierCode), UDFLib.ConvertStringToNull(ddlType.SelectedValue), UDFLib.ConvertStringToNull(lblstatus.Text)
           , txtCompanyResgName.Text.Trim(), txtshortName.Text.Trim(), UDFLib.ConvertIntegerToNull(ddlgroup.SelectedValue), txtSuppAddress.Text.Trim(),
          UDFLib.ConvertStringToNull(ddlCountry.SelectedItem.Text.ToString()), txtCity.Text.Trim(), txtTelephone.Text.Trim(), txtFaxNumber.Text.Trim(), txtCompanyEmail.Text.Trim(),
           UDFLib.ConvertStringToNull(ddlCurrency.SelectedValue), txtNamePIC1.Text.Trim(), txtNamePIC2.Text.Trim(), txtEmailPIC1.Text.Trim(), txtEmailPIC2.Text.Trim(), txtPhonePIC1.Text.Trim(),
           txtPhonePIC2.Text.Trim(), UDFLib.ConvertIntegerToNull(ddlownerShip.SelectedValue), Payment_Instructions.Text.Trim()
           , Payment_Notifications.Text.Trim(), txtBizCorporation.Text.Trim(), rdbInvoiceStatus.SelectedValue, rdbdirectinvoice.SelectedValue, rdbPaymentHistory.SelectedValue, rdbSendPo.SelectedValue, rdbPaymentPriority.SelectedValue,
             UDFLib.ConvertIntegerToNull(ddlPaymentInterval.SelectedValue), UDFLib.ConvertIntegerToNull(ddlPaymentTerms.SelectedValue),
           txtTerms.Text.Trim(), UDFLib.ConvertDecimalToNull(txtTaxRate.Text), "SENT", UDFLib.ConvertToInteger(GetSessionUserID()), UDFLib.ConvertIntegerToNull(ddlCountry.SelectedValue),
           txtSupplierDesc.Text.Trim(), ddlSubType.SelectedValue, txtTaxAccNumber.Text.Trim(), txtCompany_Reg_No.Text.Trim(), txtGST_Registration_No.Text.Trim(), UDFLib.ConvertDecimalToNull(txtWithholding_Tax_Rate.Text.Trim()),txtSupplier_Short_Code.Text.Trim(),txtShipSmart_Supplier_Code.Text.Trim());
        //

        if (RetValue != "00000")
        {
            ViewState["ReturnSupplierID"] = RetValue;

            txtSupplierCode.Text = RetValue;
            
            SaveSupplierScope();
            SaveSupplierPort();
            SaveSupplierProperties();
            string message = "alert('Supplier data submitted and email send to supplier.')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            //BindSupplierDetails();
            string msgDraft = String.Format("parent.ReloadParent_ByButtonID();");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgDraft", msgDraft, true);
        }
        {
            string message = "alert('Supplier Name Already Existed.')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
        }
         catch (Exception ex)
         {
             UDFLib.WriteExceptionLog(ex);
         }
    }
    protected void SaveSupplierProperties()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("PKID");
        dt.Columns.Add("FKID");
        dt.Columns.Add("Value");
       foreach (ListItem chkitem in gvProperties.Items)
        {
                    DataRow dr = dt.NewRow();
                    dr["FKID"] = chkitem.Value; 
                    dr["Value"] = chkitem.Selected == true ? 1 : 0;
                    dt.Rows.Add(dr);
            
        }
        BLL_ASL_Supplier.ASL_Supplier_Properties_Insert(UDFLib.ConvertStringToNull(txtSupplierCode.Text.ToString()), dt, UDFLib.ConvertToInteger(GetSessionUserID()));
       
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        DataTable dt = (DataTable)Session["dtScope"];
        DataRow dr = dt.NewRow();

        dr["ID"] = 0;
        dr["Scope_ID"] = ddlScope.SelectedValue;
        dr["Scope_Name"] = ddlScope.SelectedItem.Text;
        dt.Rows.Add(dr);

        ddlScope.Items.RemoveAt(ddlScope.SelectedIndex);

        Session["dtScope"] = dt;
        chkScope.DataSource = dt;
        chkScope.DataValueField = "Scope_ID";
        chkScope.DataTextField = "Scope_Name";
        chkScope.DataBind();
        //chk1.SelectedItem.Selected = true;
        int i = 0;
        if (chkScope.Items.Count > 0)
        {
            foreach (ListItem chkitem in chkScope.Items)
            {
                chkitem.Selected = true;
                i++;
            }
           
        }

    }
    protected void btnRemove_Click(object sender, EventArgs e)
    {
        DataTable dt = (DataTable)Session["dtScope"];
        BindScope();

        dt.Clear();
        Session["dt"] = dt;
        chkScope.DataSource = dt;

        chkScope.DataBind();

        btnScopeRemove.Visible = false;
    }
    protected void btnPortAdd_Click(object sender, EventArgs e)
    {
        DataTable dt = (DataTable)Session["dtPort"];
        DataRow dr = dt.NewRow();


        dr["PORT_ID"] = ddlPort.SelectedValue;
        dr["PORT_NAME"] = ddlPort.SelectedItem.Text;
        dt.Rows.Add(dr);

        ddlPort.Items.RemoveAt(ddlPort.SelectedIndex);

        Session["dtPort"] = dt;
        chkPort.DataSource = dt;
        chkPort.DataValueField = "PORT_ID";
        chkPort.DataTextField = "PORT_NAME";
        chkPort.DataBind();
        //chk1.SelectedItem.Selected = true;
        int i = 0;
        if (chkPort.Items.Count > 0)
        {
            foreach (ListItem chkitem in chkPort.Items)
            {
                chkitem.Selected = true;
                i++;
            }
          
        }
    }
    protected void btnPortRemove_Click(object sender, EventArgs e)
    {
        DataTable dt = (DataTable)Session["dtPort"];
        BindPort();

        dt.Clear();
        Session["dt"] = dt;
        chkPort.DataSource = dt;

        chkPort.DataBind();

        btnPortRemove.Visible = false;
    }
   
    protected void btnInvoiceStatus_Click(object sender, EventArgs e)
    {
        string OCA_URL = null;
        if (!Request.Url.AbsoluteUri.Contains(ConfigurationManager.AppSettings["OCA_APP_URL"]))
        {
            OCA_URL = ConfigurationManager.AppSettings["OCA_APP_URL"];
        }
        string PassString = txtPassString.Text.ToString().ToString();
        string OCA_URL1 = OCA_URL +"/PO_LOG/Supplier_Online_Invoice_Status_V2.asp?P=" + PassString + "";

        ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "javascript:window.open('"+ OCA_URL1 +"');", true);
       
        //Response.Write(OCA_URL +"/PO_LOG/CSupplier_Online_Invoice_Status_V2.asp?P=" + PassString + "");
    }
    protected void btnUnverify_Click(object sender, EventArgs e)
    {
         string SupplierCode = txtSupplierCode.Text.ToString();
        string Status = "Unverify";
        string Action_On_Data_Form = "DRAFT";
        int RetValue = BLL_ASL_Supplier.Supplier_Update_Unverify(
            UDFLib.ConvertIntegerToNull(ViewState["ReturnSupplierID"].ToString()), 
            UDFLib.ConvertStringToNull(SupplierCode),
            Action_On_Data_Form, 
            Status, 
            UDFLib.ConvertIntegerToNull(GetSessionUserID()));
        BindSupplierDetails();
        string message = "alert('Supplier Registered Data Form unverify and send mail to supplier.')";
        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
    }

    //protected void ddlSupplier_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //   //Supplier = ddlSupplier.SelectedValue;
    //    txtSupplierCode.Text = ddlSupplier.SelectedValue;
    //    BindSupplierDetails();
    //    //Response.Redirect("../ASL/ASL_General_Data.aspx?Supp_ID=" + Supplier + "");
    //    //Request.QueryString["Supp_ID"] = ddlSupplier.SelectedValue;
    //}
    //protected void btnChangeRequest_Click(object sender, EventArgs e)
    //{
    //    //string SupplierID = GetSuppID();
    //    int RetValue = BLL_ASL_Supplier.Supplier_Insert_Rework(UDFLib.ConvertIntegerToNull(ViewState["ReturnSupplierID"].ToString())
    //       , UDFLib.ConvertStringToNull(txtSupplierCode.Text.Trim()), "DRAFT", UDFLib.ConvertToInteger(GetSessionUserID()));
    //ViewState["ReturnSupplierCode"] = RetValue;
    //hdnRetSupplierCode.Value = RetValue;
    //    BindSupplierDetails();
    //    string message = "alert('Supplier data send to ASL admin for approval to unlock Supplier Registered Form.')";
    //    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
    //}
    //protected void btnChangeRequestApprove_Click(object sender, EventArgs e)
    //{
    //    //string SupplierID = GetSuppID();
    //    int RetValue = BLL_ASL_Supplier.Supplier_Insert_Rework(UDFLib.ConvertIntegerToNull(ViewState["ReturnSupplierID"].ToString())
    //       , UDFLib.ConvertStringToNull(txtSupplierCode.Text.Trim()), "FINALIZED", UDFLib.ConvertToInteger(GetSessionUserID()));

    //    BindSupplierDetails();
    //    string message = "alert('Supplier Registered Form unlocked and send to supplier.')";
    //    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
    //}
    //protected void TabSCM_ActiveTabChanged(object sender, EventArgs e)
    //{ //protected void btnOnlineInvoice_Click(object sender, EventArgs e)
    //{
    //Response.Redirect("../ASL/ASL_General_Data.aspx?Supp_ID=RetValue");
    //clearControl();
    //}
    //    try
    //    {
    //        BindTab();
    //    }
    //    catch { }
    //    {
    //    }
    //btnSubmit.Enabled = false;
    //}//EnableDisable(false);
    //protected void BindTab()
    //{
    //if (Request.QueryString["Type"] == "0")
    //{
    //    //TabSCM.ActiveTabIndex = 6;
    //    //BindTab();
    //    ViewState["ChangeRequest"] = 1;
    //}
    //else
    //{
    //    //string tabid = TabSCM.ActiveTab.ID;
    //    //string tabindex = TabSCM.ActiveTab.TabIndex.ToString();

    //    ViewState["ChangeRequest"] = 1;
    //}
    //BindTab();
    //if (ds.Tables[1].Rows.Count > 0)
    //{
    //    ddlSupplier.DataSource = ds.Tables[1];
    //    ddlSupplier.DataValueField = "Supplier_Code";
    //    ddlSupplier.DataTextField = "Supplier_Name";
    //    ddlSupplier.DataBind();
    //    ddlSupplier.Items.Insert(0, new ListItem("-Add New Contact-", "00000"));
    //    ddlSupplier.SelectedValue = txtSupplierCode.Text.ToString();
    //}
    //if (ds.Tables[2].Rows.Count > 0)
    //{
    //    if (ds.Tables[2].Rows[0]["Evaluation_Status"].ToString() == "Rework")
    //    {
    //        EnableDisable(true);
    //    }
    //    else
    //    {
    //        EnableDisable(false);
    //    }
    //}
    //else
    //{
    //    EnableDisable(true);
    //}
    // dvbutton.Visible = true;
    //string SupplierID = GetSuppID();
    //if (TabSCM.ActiveTabIndex == 0)
    //{
    //    if (ViewState["Registered"].ToString() == "1")
    //    {
    //        iFrame1.Attributes["src"] = "ASL_Evalution.aspx?Supp_ID=" + txtSupplierCode.Text.ToString() + "&ID=" + GetAddEvalID() + "";
    //    }
    //}
    //else if (TabSCM.ActiveTabIndex == 1)
    //{
    //    if (ViewState["Registered"].ToString() == "1")
    //    {
    //        iFrame1.Attributes["src"] = "ASL_Evalution.aspx?Supp_ID=" + txtSupplierCode.Text.ToString() + "&ID=" + GetLastEvalID() + "";
    //    }
    //}
    //else if (TabSCM.ActiveTabIndex == 2)
    //{
    //    if (ViewState["Registered"].ToString() == "1")
    //    {
    //        iFrame1.Attributes["src"] = "ASL_Supplier_Document.aspx?Supp_ID=" + txtSupplierCode.Text.ToString() + "";
    //    }
    //}
    //else if (TabSCM.ActiveTabIndex == 3)
    //{
    //    if (ViewState["Registered"].ToString() == "1")
    //    {
    //        iFrame1.Attributes["src"] = "ASL_Supplier_Remarks.aspx?Supp_ID=" + txtSupplierCode.Text.ToString() + "";
    //     }
    //}
    //else if (TabSCM.ActiveTabIndex == 4)
    //{
    //    if (ViewState["Registered"].ToString() == "1")
    //    {
    //        iFrame1.Attributes["src"] = "ASL_Supplier_SimilerName.aspx?Supp_ID=" + txtSupplierCode.Text.ToString() + "";
    //     }
    //}

    //else if (TabSCM.ActiveTabIndex == 5)
    //{
    //    if (ViewState["Registered"].ToString() == "1")
    //    {
    //        iFrame1.Attributes["src"] = "ASL_Email_Template.aspx?Supp_ID=" + txtSupplierCode.Text.ToString() + "";
    //     }
    //}
    //else if (TabSCM.ActiveTabIndex == 6)
    //{
    //    if (ViewState["ChangeRequest"].ToString() == "1")
    //    {
    //        //string ChangeRequestID = "0"; string Type = "0";
    //        iFrame1.Attributes["src"] = "ASL_CR.aspx?Supp_ID=" + txtSupplierCode.Text.ToString() + "";
    //       // iFrame1.Attributes["src"] = "ASL_CR.aspx?Supp_ID=" + txtSupplierCode.Text.ToString() + "&ID=" + ChangeRequestID + "&Type=" + Type + "";
    //    }
    //    else
    //    {
    //        iFrame1.Attributes["src"] = "";
    //    }
    //}
    //else if (TabSCM.ActiveTabIndex == 7)
    //{
    //    if (ViewState["Registered"].ToString() == "1")
    //    {
    //        iFrame1.Attributes["src"] = "ASL_Change_Request_History.aspx?Supp_ID=" + txtSupplierCode.Text.ToString() + "";
    //    }
    //}
    //else if (TabSCM.ActiveTabIndex == 8)
    //{
    //    if (ViewState["Registered"].ToString() == "1")
    //    {
    //        iFrame1.Attributes["src"] = "ASL_Payment_History.aspx?Supp_ID=" + txtSupplierCode.Text.ToString() + "";
    //    }
    //}
    //else if (TabSCM.ActiveTabIndex == 9)
    //{
    //    if (ViewState["Registered"].ToString() == "1")
    //    {
    //        iFrame1.Attributes["src"] = "ASL_PO_Invoice.aspx?Supp_ID=" + txtSupplierCode.Text.ToString() + "";
    //    }
    //}
    //else if (TabSCM.ActiveTabIndex == 10)
    //{
    //    if (ViewState["Registered"].ToString() == "1")
    //    {
    //        iFrame1.Attributes["src"] = "ASL_Invoice_WIP.aspx?Supp_ID=" + txtSupplierCode.Text.ToString() + "";
    //    }
    //}
    //else if (TabSCM.ActiveTabIndex == 11)
    //{
    //    if (ViewState["Registered"].ToString() == "1")
    //    {
    //        iFrame1.Attributes["src"] = "ASL_InvoiceStatus.aspx?Supp_ID=" + txtSupplierCode.Text.ToString() + "";
    //    }
    //}
    //else if (TabSCM.ActiveTabIndex == 12)
    //{
    //    if (ViewState["Registered"].ToString() == "1")
    //    {
    //        iFrame1.Attributes["src"] = "ASL_OutStanding.aspx?Supp_ID=" + txtSupplierCode.Text.ToString() + "";
    //    }
    //}
    //else if (TabSCM.ActiveTabIndex == 13)
    //{
    //    if (ViewState["Registered"].ToString() == "1")
    //    {
    //        iFrame1.Attributes["src"] = "ASL_Supplier_Statistics.aspx?Supp_ID=" + txtSupplierCode.Text.ToString() + "";
    //    }
    //}
    //}
    protected void btnGet_Click(object sender, EventArgs e)
    {
        BindSupplierDetails();
    }

}