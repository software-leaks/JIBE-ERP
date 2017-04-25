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


public partial class ASL_ASL_Change_Request : System.Web.UI.Page
{
    BLL_Infra_Currency objBLLCurrency = new BLL_Infra_Currency();
    BLL_Infra_Country objBLLCountry = new BLL_Infra_Country();
    UserAccess objUA = new UserAccess();
    UserAccess objUAType = new UserAccess();
    public string Supplier = null;
    public Boolean uaEditFlag = false;
    public Boolean uaDeleteFlage = false;
    #region PageLoad
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.ClientScript.RegisterStartupScript(this.GetType(), "", "myFunction();", true);
        if (!IsPostBack)
        {
            UserAccessValidation();
            UserAccessTypeValidation();
            //ViewState["ReturnCRID"] = 0;
            rdbInvoiceStatus.SelectedValue = "Yes";
            rdbdirectinvoice.SelectedValue = "Yes";
            rdbPaymentHistory.SelectedValue = "Yes";

            rdbPaymentPriority.SelectedValue = "Normal";

            BindType();
            BindSubType();
            BindCurrencyDLL();
            BindOwnerShip();
            BindPaymentInterval();
            BindPaymenterms();
            BindCountry();
            BindApproverList(GetSessionSupplierType());
            BindChangeRequest();

        }

    }
    protected void BindApproverList(string Supplier_Type)
    {
        try
        {
            string Approver_Type = "ChangeRequest";
            DataSet ds = BLL_ASL_Supplier.Get_Supplier_ApproverList(UDFLib.ConvertToInteger(GetSessionUserID()), Supplier_Type,Approver_Type);

            ddlAccFinalApprover.DataSource = ds.Tables[0];
            ddlAccFinalApprover.DataValueField = "ApproveID";
            ddlAccFinalApprover.DataTextField = "User_name";
            ddlAccFinalApprover.DataBind();
            ddlAccFinalApprover.Items.Insert(0, new ListItem("SELECT", "0"));


            ddlAccApprover.DataSource = ds.Tables[1];
            ddlAccApprover.DataValueField = "ApproveID";
            ddlAccApprover.DataTextField = "User_name";
            ddlAccApprover.DataBind();
            ddlAccApprover.Items.Insert(0, new ListItem("SELECT", "0"));

            ddlAdminFinalApprover.DataSource = ds.Tables[2];
            ddlAdminFinalApprover.DataValueField = "ApproveID";
            ddlAdminFinalApprover.DataTextField = "User_name";
            ddlAdminFinalApprover.DataBind();
            ddlAdminFinalApprover.Items.Insert(0, new ListItem("SELECT", "0"));


            ddlAdminApprover.DataSource = ds.Tables[3];
            ddlAdminApprover.DataValueField = "ApproveID";
            ddlAdminApprover.DataTextField = "User_name";
            ddlAdminApprover.DataBind();
            ddlAdminApprover.Items.Insert(0, new ListItem("SELECT", "0"));

            ddlGenFinalApprover.DataSource = ds.Tables[4];
            ddlGenFinalApprover.DataValueField = "ApproveID";
            ddlGenFinalApprover.DataTextField = "User_name";
            ddlGenFinalApprover.DataBind();
            ddlGenFinalApprover.Items.Insert(0, new ListItem("SELECT", "0"));


            ddlGenApprover.DataSource = ds.Tables[5];
            ddlGenApprover.DataValueField = "ApproveID";
            ddlGenApprover.DataTextField = "User_name";
            ddlGenApprover.DataBind();
            ddlGenApprover.Items.Insert(0, new ListItem("SELECT", "0"));
        }
        catch
        {
        }
    }
    private string GetSessionSupplierType()
    {
        if (Session["Supplier_Type"] != null)
        {
            return Session["Supplier_Type"].ToString();
        }
        else if (Request.QueryString["Supplier_Type"] != null)
        {
            return Request.QueryString["Supplier_Type"].ToString();
        }
        return null;
    }
    protected void UserAccessTypeValidation()
    {
        try
        {
            int CurrentUserID = GetSessionUserID();
            //string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

            BLL_TypeManagement objType = new BLL_TypeManagement();
            string Variable_Type = "Supplier_Type";
            string Approver_Type = "ChangeRequest";
            objUAType = objType.Get_UserTypeAccess(CurrentUserID, Variable_Type, GetSessionSupplierType(), Approver_Type);


            if (objUAType.Add == 0)
            {
                //btnSaveDraft.Enabled = false;
                btnSubmitRequest.Enabled = false;
            }
            if (objUAType.Approve == 1)
            {
                //btnSaveDraft.Enabled = true;
                btnFinalApprove.Visible = true;
                btnApprove.Visible = true;
                btnReject.Visible = true;
                btnFinalApprove.Enabled = true;
                btnApprove.Enabled = true;
                btnReject.Enabled = true;
            }
            else
            {
                btnFinalApprove.Enabled = false;
                btnApprove.Enabled = false;
                btnReject.Enabled = false;
            }
        }
        catch { }
        {
        }
    }
    protected void UserAccessValidation()
    {
        try
        {
            int CurrentUserID = GetSessionUserID();
            string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

            BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
            objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

            if (objUA.View == 0)
            {
                pnlChangeRequest.Visible = false;
                lblMsg.Text = "You don't have sufficient previlege to access the requested information.";
            }
            else
            {
                pnlChangeRequest.Visible = true;
            }

            if (objUA.Add == 0)
            {
                //btnSaveDraft.Enabled = false;
                btnSubmitRequest.Enabled = false;
            }
            if (objUA.Approve == 1)
            {
                //btnSaveDraft.Enabled = true;
                btnFinalApprove.Visible = true;
                btnApprove.Visible = true;
                btnReject.Visible = true;
            }
            else
            {
                btnFinalApprove.Visible = false;
                btnApprove.Visible = false;
                btnReject.Visible = false;
            }
            if (objUA.Edit == 1)
                uaEditFlag = true;
            //else
            // btnsave.Visible = false;

            if (objUA.Delete == 1) uaDeleteFlage = true;
        }
        catch { }
        {
        }
    }
    protected void BindChangeRequest()
    {
        try
        {
            string SupplierID = GetSuppID();
            int ID = GetChangeRequestID();
            DataSet ds = BLL_ASL_Supplier.Get_Supplier_Change_Request_List(UDFLib.ConvertStringToNull(SupplierID), ID, GetSessionUserID());
            Session["dsCR"] = ds;
            lblSupplierCode.Text = SupplierID;

            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    hdnCRID.Value = ds.Tables[0].Rows[i]["CRID"].ToString();
                    if (ds.Tables[0].Rows[i]["COLUMN_NAME"].ToString() == "Supplier_Name")
                    {
                        trAccount.Visible = true;
                        trRegname.Visible = true;
                        chkCname.Checked = false;
                        chkCname.Enabled = false;
                        hdnCName.Value = ds.Tables[0].Rows[i]["COLUMN_NAME"].ToString();
                        lblRegname.Text = ds.Tables[0].Rows[i]["Column_Description"].ToString();
                        lblRegname1.Text = ds.Tables[0].Rows[i]["CurrentValue"].ToString();
                        txtCompanyResgName.Text = ds.Tables[0].Rows[i]["New_Value"].ToString();
                        txtCNameReason.Text = ds.Tables[0].Rows[i]["Reason_For_Change"].ToString();
                        ddlAccApprover.SelectedValue = ds.Tables[0].Rows[i]["ApproverID"].ToString();
                        ddlAccFinalApprover.SelectedValue = ds.Tables[0].Rows[i]["Final_ApproverID"].ToString();
                        if (ds.Tables[0].Rows[i]["Status"].ToString() == "SUBMITTED")
                        {
                            if (ds.Tables[0].Rows[i]["Send_For_Approve"].ToString() == "1")
                            {
                                chkCname.Enabled = true; //trRegname.Visible = true;
                            }
                        }
                        else if (ds.Tables[0].Rows[i]["Status"].ToString() == "APPROVED")
                        {
                            if (ds.Tables[0].Rows[i]["Send_For_FinalApprove"].ToString() == "1")
                            {
                                chkCname.Enabled = true; //trRegname.Visible = true;
                            }

                        }
                        else
                        {
                            chkCname.Enabled = true; ///trRegname.Visible = true;
                        }
                    }
                    if (ds.Tables[0].Rows[i]["COLUMN_NAME"].ToString() == "Supplier_Type")
                    {
                        trAdmin.Visible = true;
                        trType.Visible = true;
                        chkType.Checked = false;
                        chkType.Enabled = false;
                        hdnType.Value = ds.Tables[0].Rows[i]["COLUMN_NAME"].ToString();
                        lblType.Text = ds.Tables[0].Rows[i]["Column_Description"].ToString();
                        lblType1.Text = ds.Tables[0].Rows[i]["CurrentValue"].ToString();
                        ddlType.SelectedValue = ds.Tables[0].Rows[i]["New_Value"].ToString();
                        txtTypeReason.Text = ds.Tables[0].Rows[i]["Reason_For_Change"].ToString();
                        ddlAdminApprover.SelectedValue = ds.Tables[0].Rows[i]["ApproverID"].ToString();
                        ddlAdminFinalApprover.SelectedValue = ds.Tables[0].Rows[i]["Final_ApproverID"].ToString();

                        ddlType.Items.Remove(ddlType.Items.FindByText(ds.Tables[0].Rows[i]["CurrentValue"].ToString()));
                        if (ds.Tables[0].Rows[i]["Status"].ToString() == "SUBMITTED") { if (ds.Tables[0].Rows[i]["Send_For_Approve"].ToString() == "1") { chkType.Enabled = true; } }
                        else if (ds.Tables[0].Rows[i]["Status"].ToString() == "APPROVED") { if (ds.Tables[0].Rows[i]["Send_For_FinalApprove"].ToString() == "1") { chkType.Enabled = true; } }
                        else
                        {
                            chkType.Enabled = true; //trType.Visible = true; 
                        }

                    }
                    if (ds.Tables[0].Rows[i]["COLUMN_NAME"].ToString() == "Counterparty_Type")
                    {
                        trAdmin.Visible = true;
                        trSubType.Visible = true;
                        chkSubType.Checked = false;
                        chkSubType.Enabled = false;
                        hdnSubType.Value = ds.Tables[0].Rows[i]["COLUMN_NAME"].ToString();
                        lblSubType.Text = ds.Tables[0].Rows[i]["Column_Description"].ToString();
                        lblSubType1.Text = ds.Tables[0].Rows[i]["CurrentValue"].ToString();
                        ddlSubType.SelectedValue = ds.Tables[0].Rows[i]["New_Value"].ToString();
                        txtSubTypeReason.Text = ds.Tables[0].Rows[i]["Reason_For_Change"].ToString();
                        ddlAdminApprover.SelectedValue = ds.Tables[0].Rows[i]["ApproverID"].ToString();
                        ddlAdminFinalApprover.SelectedValue = ds.Tables[0].Rows[i]["Final_ApproverID"].ToString();

                        ddlSubType.Items.Remove(ddlSubType.Items.FindByText(ds.Tables[0].Rows[i]["CurrentValue"].ToString()));
                        if (ds.Tables[0].Rows[i]["Status"].ToString() == "SUBMITTED") { if (ds.Tables[0].Rows[i]["Send_For_Approve"].ToString() == "1") { chkSubType.Enabled = true; } }
                        else if (ds.Tables[0].Rows[i]["Status"].ToString() == "APPROVED") { if (ds.Tables[0].Rows[i]["Send_For_FinalApprove"].ToString() == "1") { chkSubType.Enabled = true; } }
                        else
                        {
                            chkSubType.Enabled = true;// trSubType.Visible = true; 
                        }
                    }
                    if (ds.Tables[0].Rows[i]["COLUMN_NAME"].ToString() == "Tax_Account_Number")
                    {
                        trAdmin.Visible = true;
                        trTaxNumber.Visible = true;
                        chkTaxNumber.Checked = false;
                        chkTaxNumber.Enabled = false;
                        hdnTaxNumber.Value = ds.Tables[0].Rows[i]["COLUMN_NAME"].ToString();
                        lblTaxNumber.Text = ds.Tables[0].Rows[i]["Column_Description"].ToString();
                        lblTaxNumber1.Text = ds.Tables[0].Rows[i]["CurrentValue"].ToString();
                        txtTaxNumber.Text = ds.Tables[0].Rows[i]["New_Value"].ToString();
                        txtTaxNumberReason.Text = ds.Tables[0].Rows[i]["Reason_For_Change"].ToString();
                        ddlAdminApprover.SelectedValue = ds.Tables[0].Rows[i]["ApproverID"].ToString();
                        ddlAdminFinalApprover.SelectedValue = ds.Tables[0].Rows[i]["Final_ApproverID"].ToString();


                        if (ds.Tables[0].Rows[i]["Status"].ToString() == "SUBMITTED") { if (ds.Tables[0].Rows[i]["Send_For_Approve"].ToString() == "1") { chkTaxNumber.Enabled = true; } }
                        else if (ds.Tables[0].Rows[i]["Status"].ToString() == "APPROVED") { if (ds.Tables[0].Rows[i]["Send_For_FinalApprove"].ToString() == "1") { chkTaxNumber.Enabled = true; } }
                        else
                        {
                            chkTaxNumber.Enabled = true;// trSubType.Visible = true; 
                        }
                    }
                    if (ds.Tables[0].Rows[i]["COLUMN_NAME"].ToString() == "Address")
                    {
                        trGeneral.Visible = true;
                        trAddress.Visible = true;
                        chkAddress.Checked = false;
                        chkAddress.Enabled = false;
                        hdnSuppAddress.Value = ds.Tables[0].Rows[i]["COLUMN_NAME"].ToString();
                        lblAddress.Text = ds.Tables[0].Rows[i]["Column_Description"].ToString();
                        lblAddress1.Text = ds.Tables[0].Rows[i]["CurrentValue"].ToString();
                        txtSuppAddress.Text = ds.Tables[0].Rows[i]["New_Value"].ToString();
                        txtSuppAddressReason.Text = ds.Tables[0].Rows[i]["Reason_For_Change"].ToString();
                        ddlGenApprover.SelectedValue = ds.Tables[0].Rows[i]["ApproverID"].ToString();
                        ddlGenFinalApprover.SelectedValue = ds.Tables[0].Rows[i]["Final_ApproverID"].ToString();
                        if (ds.Tables[0].Rows[i]["Status"].ToString() == "SUBMITTED") { if (ds.Tables[0].Rows[i]["Send_For_Approve"].ToString() == "1") { chkAddress.Enabled = true; } }
                        else if (ds.Tables[0].Rows[i]["Status"].ToString() == "APPROVED") { if (ds.Tables[0].Rows[i]["Send_For_FinalApprove"].ToString() == "1") { chkAddress.Enabled = true; } }
                        else
                        {
                            chkAddress.Enabled = true;//trAddress.Visible = true;
                        }
                    }
                    if (ds.Tables[0].Rows[i]["COLUMN_NAME"].ToString() == "CountryID")
                    {
                        trGeneral.Visible = true;
                        trCountry.Visible = true;
                        chkCountry.Checked = false;
                        chkCountry.Enabled = false;
                        hdnCountry.Value = ds.Tables[0].Rows[i]["COLUMN_NAME"].ToString();
                        lblCountry.Text = ds.Tables[0].Rows[i]["Column_Description"].ToString();
                        lblCountry1.Text = ds.Tables[0].Rows[i]["CurrentValue"].ToString();
                        ddlCountry.SelectedValue = ds.Tables[0].Rows[i]["New_Value"].ToString();
                        txtCountryReason.Text = ds.Tables[0].Rows[i]["Reason_For_Change"].ToString();
                        ddlGenApprover.SelectedValue = ds.Tables[0].Rows[i]["ApproverID"].ToString();
                        ddlGenFinalApprover.SelectedValue = ds.Tables[0].Rows[i]["Final_ApproverID"].ToString();
                        ddlCountry.Items.Remove(ddlCountry.Items.FindByText(ds.Tables[0].Rows[i]["CurrentValue"].ToString()));

                        if (ds.Tables[0].Rows[i]["Status"].ToString() == "SUBMITTED") { if (ds.Tables[0].Rows[i]["Send_For_Approve"].ToString() == "1") { chkCountry.Enabled = true; } }
                        else if (ds.Tables[0].Rows[i]["Status"].ToString() == "APPROVED") { if (ds.Tables[0].Rows[i]["Send_For_FinalApprove"].ToString() == "1") { chkCountry.Enabled = true; } }
                        else
                        {
                            chkCountry.Enabled = true;//trCountry.Visible = true;
                        }
                    }
                    if (ds.Tables[0].Rows[i]["COLUMN_NAME"].ToString() == "City")
                    {
                        trGeneral.Visible = true;
                        trCity.Visible = true;
                        chkCity.Checked = false;
                        chkCity.Enabled = false;
                        hdnCity.Value = ds.Tables[0].Rows[i]["COLUMN_NAME"].ToString();
                        lblCity.Text = ds.Tables[0].Rows[i]["Column_Description"].ToString();
                        lblCity1.Text = ds.Tables[0].Rows[i]["CurrentValue"].ToString();
                        txtCity.Text = ds.Tables[0].Rows[i]["New_Value"].ToString();
                        txtCityReason.Text = ds.Tables[0].Rows[i]["Reason_For_Change"].ToString();
                        ddlGenApprover.SelectedValue = ds.Tables[0].Rows[i]["ApproverID"].ToString();
                        ddlGenFinalApprover.SelectedValue = ds.Tables[0].Rows[i]["Final_ApproverID"].ToString();
                        if (ds.Tables[0].Rows[i]["Status"].ToString() == "SUBMITTED") { if (ds.Tables[0].Rows[i]["Send_For_Approve"].ToString() == "1") { chkCity.Enabled = true; } }
                        else if (ds.Tables[0].Rows[i]["Status"].ToString() == "APPROVED") { if (ds.Tables[0].Rows[i]["Send_For_FinalApprove"].ToString() == "1") { chkCity.Enabled = true; } }
                        else
                        {
                            chkCity.Enabled = true;//trCity.Visible = true;
                        }
                    }
                    if (ds.Tables[0].Rows[i]["COLUMN_NAME"].ToString() == "Email")
                    {
                        trGeneral.Visible = true;
                        trEmail.Visible = true;
                        chkEmail.Checked = false;
                        chkEmail.Enabled = false;
                        hdnEmail.Value = ds.Tables[0].Rows[i]["COLUMN_NAME"].ToString();
                        lblEmail.Text = ds.Tables[0].Rows[i]["Column_Description"].ToString();
                        lblEmail1.Text = ds.Tables[0].Rows[i]["CurrentValue"].ToString();
                        txtEmail.Text = ds.Tables[0].Rows[i]["New_Value"].ToString();
                        txtEmailReason.Text = ds.Tables[0].Rows[i]["Reason_For_Change"].ToString();
                        ddlGenApprover.SelectedValue = ds.Tables[0].Rows[i]["ApproverID"].ToString();
                        ddlGenFinalApprover.SelectedValue = ds.Tables[0].Rows[i]["Final_ApproverID"].ToString();
                        if (ds.Tables[0].Rows[i]["Status"].ToString() == "SUBMITTED")
                        {
                            if (ds.Tables[0].Rows[i]["Send_For_Approve"].ToString() == "1")
                            {
                                //trEmail.Visible = true; 
                                chkEmail.Enabled = true;
                            }
                        }
                        else if (ds.Tables[0].Rows[i]["Status"].ToString() == "APPROVED")
                        {
                            if (ds.Tables[0].Rows[i]["Send_For_FinalApprove"].ToString() == "1")
                            {
                                //trEmail.Visible = true; 
                                chkEmail.Enabled = true;
                            }
                        }
                        else
                        {
                            //trEmail.Visible = true;
                            chkEmail.Enabled = true;
                        }
                    }
                    if (ds.Tables[0].Rows[i]["COLUMN_NAME"].ToString() == "Phone")
                    {
                        trGeneral.Visible = true;
                        trPhone.Visible = true;
                        chkPhone.Checked = false;
                        chkPhone.Enabled = false;
                        hdnPhone.Value = ds.Tables[0].Rows[i]["COLUMN_NAME"].ToString();
                        lblPhone.Text = ds.Tables[0].Rows[i]["Column_Description"].ToString();
                        lblPhone1.Text = ds.Tables[0].Rows[i]["CurrentValue"].ToString();
                        txtPhone.Text = ds.Tables[0].Rows[i]["New_Value"].ToString();
                        txtPhoneReason.Text = ds.Tables[0].Rows[i]["Reason_For_Change"].ToString();
                        ddlGenApprover.SelectedValue = ds.Tables[0].Rows[i]["ApproverID"].ToString();
                        ddlGenFinalApprover.SelectedValue = ds.Tables[0].Rows[i]["Final_ApproverID"].ToString();
                        if (ds.Tables[0].Rows[i]["Status"].ToString() == "SUBMITTED") { if (ds.Tables[0].Rows[i]["Send_For_Approve"].ToString() == "1") { chkPhone.Enabled = true; } }
                        else if (ds.Tables[0].Rows[i]["Status"].ToString() == "APPROVED") { if (ds.Tables[0].Rows[i]["Send_For_FinalApprove"].ToString() == "1") { chkPhone.Enabled = true; } }
                        else
                        {
                            chkPhone.Enabled = true;// trPhone.Visible = true; 
                        }
                    }
                    if (ds.Tables[0].Rows[i]["COLUMN_NAME"].ToString() == "Fax")
                    {
                        trGeneral.Visible = true;
                        trFax.Visible = true;
                        chkFax.Checked = false;
                        chkFax.Enabled = false;
                        hdnFax.Value = ds.Tables[0].Rows[i]["COLUMN_NAME"].ToString();
                        lblFax.Text = ds.Tables[0].Rows[i]["Column_Description"].ToString();
                        lblFax1.Text = ds.Tables[0].Rows[i]["CurrentValue"].ToString();
                        txtFax.Text = ds.Tables[0].Rows[i]["New_Value"].ToString();
                        txtFaxReason.Text = ds.Tables[0].Rows[i]["Reason_For_Change"].ToString();
                        ddlGenApprover.SelectedValue = ds.Tables[0].Rows[i]["ApproverID"].ToString();
                        ddlGenFinalApprover.SelectedValue = ds.Tables[0].Rows[i]["Final_ApproverID"].ToString();
                        if (ds.Tables[0].Rows[i]["Status"].ToString() == "SUBMITTED") { if (ds.Tables[0].Rows[i]["Send_For_Approve"].ToString() == "1") { chkFax.Enabled = true; } }
                        else if (ds.Tables[0].Rows[i]["Status"].ToString() == "APPROVED") { if (ds.Tables[0].Rows[i]["Send_For_FinalApprove"].ToString() == "1") { chkFax.Enabled = true; } }
                        else
                        {
                            trFax.Visible = true; chkFax.Enabled = true;
                        }
                    }
                    if (ds.Tables[0].Rows[i]["COLUMN_NAME"].ToString() == "Contact_1")
                    {
                        trGeneral.Visible = true;
                        trPICName.Visible = true;
                        chkPICName.Checked = false;
                        chkPICName.Enabled = false;
                        hdnPICName.Value = ds.Tables[0].Rows[i]["COLUMN_NAME"].ToString();
                        lblPICName.Text = ds.Tables[0].Rows[i]["Column_Description"].ToString();
                        lblPICName1.Text = ds.Tables[0].Rows[i]["CurrentValue"].ToString();
                        txtPICName.Text = ds.Tables[0].Rows[i]["New_Value"].ToString();
                        txtPICNameReason.Text = ds.Tables[0].Rows[i]["Reason_For_Change"].ToString();
                        ddlGenApprover.SelectedValue = ds.Tables[0].Rows[i]["ApproverID"].ToString();
                        ddlGenFinalApprover.SelectedValue = ds.Tables[0].Rows[i]["Final_ApproverID"].ToString();
                        if (ds.Tables[0].Rows[i]["Status"].ToString() == "SUBMITTED") { if (ds.Tables[0].Rows[i]["Send_For_Approve"].ToString() == "1") { chkPICName.Enabled = true; } }
                        else if (ds.Tables[0].Rows[i]["Status"].ToString() == "APPROVED") { if (ds.Tables[0].Rows[i]["Send_For_FinalApprove"].ToString() == "1") { chkPICName.Enabled = true; } }
                        else
                        {
                            trPICName.Visible = true; chkPICName.Enabled = true;
                        }
                    }
                    if (ds.Tables[0].Rows[i]["COLUMN_NAME"].ToString() == "Contact_1_Email")
                    {
                        trGeneral.Visible = true;
                        trPICEmail.Visible = true;
                        chkPICEmail.Checked = false;
                        chkPICEmail.Enabled = false;
                        hdnPICEmail.Value = ds.Tables[0].Rows[i]["COLUMN_NAME"].ToString();
                        lblPICEmail.Text = ds.Tables[0].Rows[i]["Column_Description"].ToString();
                        lblPICEmail1.Text = ds.Tables[0].Rows[i]["CurrentValue"].ToString();
                        txtPICEmail.Text = ds.Tables[0].Rows[i]["New_Value"].ToString();
                        txtPICEmailReason.Text = ds.Tables[0].Rows[i]["Reason_For_Change"].ToString();
                        ddlGenApprover.SelectedValue = ds.Tables[0].Rows[i]["ApproverID"].ToString();
                        ddlGenFinalApprover.SelectedValue = ds.Tables[0].Rows[i]["Final_ApproverID"].ToString();
                        if (ds.Tables[0].Rows[i]["Status"].ToString() == "SUBMITTED") { if (ds.Tables[0].Rows[i]["Send_For_Approve"].ToString() == "1") { chkPICEmail.Enabled = true; } }
                        else if (ds.Tables[0].Rows[i]["Status"].ToString() == "APPROVED") { if (ds.Tables[0].Rows[i]["Send_For_FinalApprove"].ToString() == "1") { chkPICEmail.Enabled = true; } }
                        else
                        {
                            trPICEmail.Visible = true; chkPICEmail.Enabled = true;
                        }
                    }
                    if (ds.Tables[0].Rows[i]["COLUMN_NAME"].ToString() == "Contact_1_Phone")
                    {
                        trGeneral.Visible = true;
                        trPICPhone1.Visible = true;
                        chkPICPhone.Checked = false;
                        chkPICPhone.Enabled = false;
                        hdnPICPhone.Value = ds.Tables[0].Rows[i]["COLUMN_NAME"].ToString();
                        lblPICPhone.Text = ds.Tables[0].Rows[i]["Column_Description"].ToString();
                        lblPICPhone1.Text = ds.Tables[0].Rows[i]["CurrentValue"].ToString();
                        txtPICPhone.Text = ds.Tables[0].Rows[i]["New_Value"].ToString();
                        txtPICPhoneReason.Text = ds.Tables[0].Rows[i]["Reason_For_Change"].ToString();
                        ddlGenApprover.SelectedValue = ds.Tables[0].Rows[i]["ApproverID"].ToString();
                        ddlGenFinalApprover.SelectedValue = ds.Tables[0].Rows[i]["Final_ApproverID"].ToString();
                        if (ds.Tables[0].Rows[i]["Status"].ToString() == "SUBMITTED") { if (ds.Tables[0].Rows[i]["Send_For_Approve"].ToString() == "1") { chkPICPhone.Enabled = true; } }
                        else if (ds.Tables[0].Rows[i]["Status"].ToString() == "APPROVED") { if (ds.Tables[0].Rows[i]["Send_For_FinalApprove"].ToString() == "1") { chkPICPhone.Enabled = true; } }
                        else
                        {
                            trPICPhone1.Visible = true; chkPICPhone.Enabled = true;
                        }
                    }
                    if (ds.Tables[0].Rows[i]["COLUMN_NAME"].ToString() == "Contact_2")
                    {
                        trGeneral.Visible = true;
                        trPICName2.Visible = true;
                        chkPICName2.Checked = false;
                        chkPICName2.Enabled = false;
                        hdnPICName2.Value = ds.Tables[0].Rows[i]["COLUMN_NAME"].ToString();
                        lblPICName2.Text = ds.Tables[0].Rows[i]["Column_Description"].ToString();
                        lblPICName21.Text = ds.Tables[0].Rows[i]["CurrentValue"].ToString();
                        txtPICName2.Text = ds.Tables[0].Rows[i]["New_Value"].ToString();
                        txtPICName2Reason.Text = ds.Tables[0].Rows[i]["Reason_For_Change"].ToString();
                        ddlGenApprover.SelectedValue = ds.Tables[0].Rows[i]["ApproverID"].ToString();
                        ddlGenFinalApprover.SelectedValue = ds.Tables[0].Rows[i]["Final_ApproverID"].ToString();
                        if (ds.Tables[0].Rows[i]["Status"].ToString() == "SUBMITTED") { if (ds.Tables[0].Rows[i]["Send_For_Approve"].ToString() == "1") { chkPICName2.Enabled = true; } }
                        else if (ds.Tables[0].Rows[i]["Status"].ToString() == "APPROVED") { if (ds.Tables[0].Rows[i]["Send_For_FinalApprove"].ToString() == "1") { chkPICName2.Enabled = true; } }
                        else
                        {
                            trPICName2.Visible = true; chkPICName2.Enabled = true;
                        }
                    }
                    if (ds.Tables[0].Rows[i]["COLUMN_NAME"].ToString() == "Contact_2_Email")
                    {
                        trGeneral.Visible = true;
                        trPICEmail2.Visible = true;
                        chkPICEmail2.Checked = false;
                        chkPICEmail2.Enabled = false;
                        hdnPICEmail2.Value = ds.Tables[0].Rows[i]["COLUMN_NAME"].ToString();
                        lblPICEmail2.Text = ds.Tables[0].Rows[i]["Column_Description"].ToString();
                        lblPICEmail21.Text = ds.Tables[0].Rows[i]["CurrentValue"].ToString();
                        txtPICEmail2.Text = ds.Tables[0].Rows[i]["New_Value"].ToString();
                        txtPICEmail2Reason.Text = ds.Tables[0].Rows[i]["Reason_For_Change"].ToString();
                        ddlGenApprover.SelectedValue = ds.Tables[0].Rows[i]["ApproverID"].ToString();
                        ddlGenFinalApprover.SelectedValue = ds.Tables[0].Rows[i]["Final_ApproverID"].ToString();
                        if (ds.Tables[0].Rows[i]["Status"].ToString() == "SUBMITTED") { if (ds.Tables[0].Rows[i]["Send_For_Approve"].ToString() == "1") { chkPICEmail2.Enabled = true; } }
                        else if (ds.Tables[0].Rows[i]["Status"].ToString() == "APPROVED") { if (ds.Tables[0].Rows[i]["Send_For_FinalApprove"].ToString() == "1") { chkPICEmail2.Enabled = true; } }
                        else
                        {
                            trPICEmail2.Visible = true; chkPICEmail2.Enabled = true;
                        }
                    }
                    if (ds.Tables[0].Rows[i]["COLUMN_NAME"].ToString() == "Contact_2_Phone")
                    {
                        trGeneral.Visible = true;
                        trPICPhone2.Visible = true;
                        chkPICPhone2.Checked = false;
                        chkPICPhone2.Enabled = false;
                        hdnPICPhone2.Value = ds.Tables[0].Rows[i]["COLUMN_NAME"].ToString();
                        lblPICPhone2.Text = ds.Tables[0].Rows[i]["Column_Description"].ToString();
                        lblPICPhone21.Text = ds.Tables[0].Rows[i]["CurrentValue"].ToString();
                        txtPICPhone2.Text = ds.Tables[0].Rows[i]["New_Value"].ToString();
                        txtPICPhone2Reason.Text = ds.Tables[0].Rows[i]["Reason_For_Change"].ToString();
                        ddlGenApprover.SelectedValue = ds.Tables[0].Rows[i]["ApproverID"].ToString();
                        ddlGenFinalApprover.SelectedValue = ds.Tables[0].Rows[i]["Final_ApproverID"].ToString();
                        if (ds.Tables[0].Rows[i]["Status"].ToString() == "SUBMITTED") { if (ds.Tables[0].Rows[i]["Send_For_Approve"].ToString() == "1") { chkPICPhone2.Enabled = true; } }
                        else if (ds.Tables[0].Rows[i]["Status"].ToString() == "APPROVED") { if (ds.Tables[0].Rows[i]["Send_For_FinalApprove"].ToString() == "1") { chkPICPhone2.Enabled = true; } }
                        else
                        {
                            trPICPhone2.Visible = true; chkPICPhone2.Enabled = true;
                        }
                    }
                    if (ds.Tables[0].Rows[i]["COLUMN_NAME"].ToString() == "Supplier_Short_Name")
                    {
                        trAdmin.Visible = true;
                        trShortname.Visible = true;
                        chkShortName.Checked = false;
                        chkShortName.Enabled = false;
                        hdnShortName.Value = ds.Tables[0].Rows[i]["COLUMN_NAME"].ToString();
                        lblShortname.Text = ds.Tables[0].Rows[i]["Column_Description"].ToString();
                        lblShortname1.Text = ds.Tables[0].Rows[i]["CurrentValue"].ToString();
                        txtShortName.Text = ds.Tables[0].Rows[i]["New_Value"].ToString();
                        txtShortNameReason.Text = ds.Tables[0].Rows[i]["Reason_For_Change"].ToString();
                        ddlAdminApprover.SelectedValue = ds.Tables[0].Rows[i]["ApproverID"].ToString();
                        ddlAdminFinalApprover.SelectedValue = ds.Tables[0].Rows[i]["Final_ApproverID"].ToString();
                        if (ds.Tables[0].Rows[i]["Status"].ToString() == "SUBMITTED") { if (ds.Tables[0].Rows[i]["Send_For_Approve"].ToString() == "1") { chkShortName.Enabled = true; } }
                        else if (ds.Tables[0].Rows[i]["Status"].ToString() == "APPROVED") { if (ds.Tables[0].Rows[i]["Send_For_FinalApprove"].ToString() == "1") { chkShortName.Enabled = true; } }
                        else
                        {
                            trShortname.Visible = true; chkShortName.Enabled = true;
                        }
                    }
                    if (ds.Tables[0].Rows[i]["COLUMN_NAME"].ToString() == "Supplier_Description")
                    {
                        trGeneral.Visible = true;
                        trSupplierDesc.Visible = true;
                        chkSupplierDesc.Checked = false;
                        chkSupplierDesc.Enabled = false;
                        hdnSupplierDesc.Value = ds.Tables[0].Rows[i]["COLUMN_NAME"].ToString();
                        lblSupplierDesc.Text = ds.Tables[0].Rows[i]["Column_Description"].ToString();
                        lblSupplierDesc1.Text = ds.Tables[0].Rows[i]["CurrentValue"].ToString();
                        txtSupplierDesc.Text = ds.Tables[0].Rows[i]["New_Value"].ToString();
                        txtSupplierDescReason.Text = ds.Tables[0].Rows[i]["Reason_For_Change"].ToString();

                        ddlGenApprover.SelectedValue = ds.Tables[0].Rows[i]["ApproverID"].ToString();
                        ddlGenFinalApprover.SelectedValue = ds.Tables[0].Rows[i]["Final_ApproverID"].ToString();
                        if (ds.Tables[0].Rows[i]["Status"].ToString() == "SUBMITTED") { if (ds.Tables[0].Rows[i]["Send_For_Approve"].ToString() == "1") { chkSupplierDesc.Enabled = true; } }
                        else if (ds.Tables[0].Rows[i]["Status"].ToString() == "APPROVED") { if (ds.Tables[0].Rows[i]["Send_For_FinalApprove"].ToString() == "1") { chkSupplierDesc.Enabled = true; } }
                        else
                        {
                            trSupplierDesc.Visible = true; chkSupplierDesc.Enabled = true;
                        }
                    }
                    if (ds.Tables[0].Rows[i]["COLUMN_NAME"].ToString() == "Payment_Instructions")
                    {
                        trAccount.Visible = true;
                        trPayment.Visible = true;
                        chkPayment.Checked = false;
                        chkPayment.Enabled = false;
                        hdnPayment.Value = ds.Tables[0].Rows[i]["COLUMN_NAME"].ToString();
                        lblPayment.Text = ds.Tables[0].Rows[i]["Column_Description"].ToString();
                        lblPayment1.Text = ds.Tables[0].Rows[i]["CurrentValue"].ToString();
                        txtPayment.Text = ds.Tables[0].Rows[i]["New_Value"].ToString();
                        txtBankReason.Text = ds.Tables[0].Rows[i]["Reason_For_Change"].ToString();
                        ddlAccApprover.SelectedValue = ds.Tables[0].Rows[i]["ApproverID"].ToString();
                        ddlAccFinalApprover.SelectedValue = ds.Tables[0].Rows[i]["Final_ApproverID"].ToString();
                        if (ds.Tables[0].Rows[i]["Status"].ToString() == "SUBMITTED") { if (ds.Tables[0].Rows[i]["Send_For_Approve"].ToString() == "1") { chkPayment.Enabled = true; } }
                        else if (ds.Tables[0].Rows[i]["Status"].ToString() == "APPROVED") { if (ds.Tables[0].Rows[i]["Send_For_FinalApprove"].ToString() == "1") { chkPayment.Enabled = true; } }
                        else
                        {
                            trPayment.Visible = true; chkPayment.Enabled = true;
                        }
                    }
                    if (ds.Tables[0].Rows[i]["COLUMN_NAME"].ToString() == "Payment_Notifications")
                    {
                        trAccount.Visible = true;
                        trPaymentEmail.Visible = true;
                        chkPaymentEmail.Checked = false;
                        chkPaymentEmail.Enabled = false;
                        hdnPaymentEmail.Value = ds.Tables[0].Rows[i]["COLUMN_NAME"].ToString();
                        lblPaymentEmail.Text = ds.Tables[0].Rows[i]["Column_Description"].ToString();
                        lblPaymentEmail1.Text = ds.Tables[0].Rows[i]["CurrentValue"].ToString();
                        txtPaymentEmail.Text = ds.Tables[0].Rows[i]["New_Value"].ToString();
                        txtPaymentEmailReason.Text = ds.Tables[0].Rows[i]["Reason_For_Change"].ToString();
                        ddlAccApprover.SelectedValue = ds.Tables[0].Rows[i]["ApproverID"].ToString();
                        ddlAccFinalApprover.SelectedValue = ds.Tables[0].Rows[i]["Final_ApproverID"].ToString();
                        if (ds.Tables[0].Rows[i]["Status"].ToString() == "SUBMITTED") { if (ds.Tables[0].Rows[i]["Send_For_Approve"].ToString() == "1") { chkPaymentEmail.Enabled = true; } }
                        else if (ds.Tables[0].Rows[i]["Status"].ToString() == "APPROVED") { if (ds.Tables[0].Rows[i]["Send_For_FinalApprove"].ToString() == "1") { chkPaymentEmail.Enabled = true; } }
                        else
                        {
                            trPaymentEmail.Visible = true; chkPaymentEmail.Enabled = true;
                        }
                    }
                    if (ds.Tables[0].Rows[i]["COLUMN_NAME"].ToString() == "Supplier_Currency")
                    {
                        trAdmin.Visible = true;
                        trCurrency.Visible = true;
                        chkCurrency.Checked = false;
                        chkCurrency.Enabled = false;
                        hdnCurrency.Value = ds.Tables[0].Rows[i]["COLUMN_NAME"].ToString();
                        lblCurrency.Text = ds.Tables[0].Rows[i]["Column_Description"].ToString();
                        lblCurrency1.Text = ds.Tables[0].Rows[i]["CurrentValue"].ToString();
                        ddlCurrency.SelectedValue = ds.Tables[0].Rows[i]["New_Value"].ToString();
                        txtCurrencyReason.Text = ds.Tables[0].Rows[i]["Reason_For_Change"].ToString();
                        ddlAdminApprover.SelectedValue = ds.Tables[0].Rows[i]["ApproverID"].ToString();
                        ddlAdminFinalApprover.SelectedValue = ds.Tables[0].Rows[i]["Final_ApproverID"].ToString();
                        ddlCurrency.Items.Remove(ddlCurrency.Items.FindByText(ds.Tables[0].Rows[i]["CurrentValue"].ToString()));

                        if (ds.Tables[0].Rows[i]["Status"].ToString() == "SUBMITTED") { if (ds.Tables[0].Rows[i]["Send_For_Approve"].ToString() == "1") { chkCurrency.Enabled = true; } }
                        else if (ds.Tables[0].Rows[i]["Status"].ToString() == "APPROVED") { if (ds.Tables[0].Rows[i]["Send_For_FinalApprove"].ToString() == "1") { chkCurrency.Enabled = true; } }
                        else
                        {
                            trCurrency.Visible = true; chkCurrency.Enabled = true;
                        }
                    }
                    if (ds.Tables[0].Rows[i]["COLUMN_NAME"].ToString() == "Payment_Terms")
                    {
                        trAccount.Visible = true;
                        trTerms.Visible = true;
                        chkTerms.Checked = false;
                        chkTerms.Enabled = false;
                        hdnTerms.Value = ds.Tables[0].Rows[i]["COLUMN_NAME"].ToString();
                        lblTerms.Text = ds.Tables[0].Rows[i]["Column_Description"].ToString();
                        lblTerms1.Text = ds.Tables[0].Rows[i]["CurrentValue"].ToString();
                        txtTerms.Text = ds.Tables[0].Rows[i]["New_Value"].ToString();
                        txtTermsReason.Text = ds.Tables[0].Rows[i]["Reason_For_Change"].ToString();
                        ddlAccApprover.SelectedValue = ds.Tables[0].Rows[i]["ApproverID"].ToString();
                        ddlAccFinalApprover.SelectedValue = ds.Tables[0].Rows[i]["Final_ApproverID"].ToString();
                        if (ds.Tables[0].Rows[i]["Status"].ToString() == "SUBMITTED") { if (ds.Tables[0].Rows[i]["Send_For_Approve"].ToString() == "1") { chkTerms.Enabled = true; } }
                        else if (ds.Tables[0].Rows[i]["Status"].ToString() == "APPROVED") { if (ds.Tables[0].Rows[i]["Send_For_FinalApprove"].ToString() == "1") { chkTerms.Enabled = true; } }
                        else
                        {
                            trTerms.Visible = true; chkTerms.Enabled = true;
                        }
                    }
                    if (ds.Tables[0].Rows[i]["COLUMN_NAME"].ToString() == "GST_Rate")
                    {
                        trGeneral.Visible = true;
                        trTaxRate.Visible = true;
                        chkTaxRate.Checked = false;
                        chkTaxRate.Enabled = false;
                        hdnTaxRate.Value = ds.Tables[0].Rows[i]["COLUMN_NAME"].ToString();
                        lblTaxRate.Text = ds.Tables[0].Rows[i]["Column_Description"].ToString();
                        lblTaxRate1.Text = ds.Tables[0].Rows[i]["CurrentValue"].ToString();
                        txtTaxRate.Text = ds.Tables[0].Rows[i]["New_Value"].ToString();
                        txtTaxRateReason.Text = ds.Tables[0].Rows[i]["Reason_For_Change"].ToString();
                        ddlGenApprover.SelectedValue = ds.Tables[0].Rows[i]["ApproverID"].ToString();
                        ddlGenFinalApprover.SelectedValue = ds.Tables[0].Rows[i]["Final_ApproverID"].ToString();
                        if (ds.Tables[0].Rows[i]["Status"].ToString() == "SUBMITTED") { if (ds.Tables[0].Rows[i]["Send_For_Approve"].ToString() == "1") { chkTaxRate.Enabled = true; } }
                        else if (ds.Tables[0].Rows[i]["Status"].ToString() == "APPROVED") { if (ds.Tables[0].Rows[i]["Send_For_FinalApprove"].ToString() == "1") { chkTaxRate.Enabled = true; } }
                        else
                        {
                            trTaxRate.Visible = true; chkTaxRate.Enabled = true;
                        }
                    }
                    if (ds.Tables[0].Rows[i]["COLUMN_NAME"].ToString() == "Ownership_Type")
                    {
                        trAdmin.Visible = true;
                        trOwnerShip.Visible = true;
                        chkOwnership.Checked = false;
                        chkOwnership.Enabled = false;
                        hdnownerShip.Value = ds.Tables[0].Rows[i]["COLUMN_NAME"].ToString();
                        lblOwnerShip.Text = ds.Tables[0].Rows[i]["Column_Description"].ToString();
                        lblOwnerShip1.Text = ds.Tables[0].Rows[i]["CurrentValue"].ToString();
                        ddlownerShip.Text = ds.Tables[0].Rows[i]["New_Value"].ToString();
                        txtownerShipReason.Text = ds.Tables[0].Rows[i]["Reason_For_Change"].ToString();
                        ddlAdminApprover.SelectedValue = ds.Tables[0].Rows[i]["ApproverID"].ToString();
                        ddlAdminFinalApprover.SelectedValue = ds.Tables[0].Rows[i]["Final_ApproverID"].ToString();
                        ddlownerShip.Items.Remove(ddlownerShip.Items.FindByText(ds.Tables[0].Rows[i]["CurrentValue"].ToString()));
                        if (ds.Tables[0].Rows[i]["Status"].ToString() == "SUBMITTED") { if (ds.Tables[0].Rows[i]["Send_For_Approve"].ToString() == "1") { trOwnerShip.Visible = true; chkOwnership.Enabled = true; } }
                        else if (ds.Tables[0].Rows[i]["Status"].ToString() == "APPROVED") { if (ds.Tables[0].Rows[i]["Send_For_FinalApprove"].ToString() == "1") { trOwnerShip.Visible = true; chkOwnership.Enabled = true; } }
                        else
                        {
                            trOwnerShip.Visible = true; chkOwnership.Enabled = true;
                        }
                    }
                    if (ds.Tables[0].Rows[i]["COLUMN_NAME"].ToString() == "Setup_Year")
                    {
                        trGeneral.Visible = true;
                        trbiz.Visible = true;
                        chkBiz.Checked = false;
                        chkBiz.Enabled = false;
                        hdnBiz.Value = ds.Tables[0].Rows[i]["COLUMN_NAME"].ToString();
                        lblBiz.Text = ds.Tables[0].Rows[i]["Column_Description"].ToString();
                        lblBiz1.Text = ds.Tables[0].Rows[i]["CurrentValue"].ToString();
                        txtbiz.Text = ds.Tables[0].Rows[i]["New_Value"].ToString();
                        txtbizReason.Text = ds.Tables[0].Rows[i]["Reason_For_Change"].ToString();
                        ddlGenApprover.SelectedValue = ds.Tables[0].Rows[i]["ApproverID"].ToString();
                        ddlGenFinalApprover.SelectedValue = ds.Tables[0].Rows[i]["Final_ApproverID"].ToString();
                        if (ds.Tables[0].Rows[i]["Status"].ToString() == "SUBMITTED") { if (ds.Tables[0].Rows[i]["Send_For_Approve"].ToString() == "1") { trbiz.Visible = true; chkBiz.Enabled = true; } }
                        else if (ds.Tables[0].Rows[i]["Status"].ToString() == "APPROVED") { if (ds.Tables[0].Rows[i]["Send_For_FinalApprove"].ToString() == "1") { trbiz.Visible = true; chkBiz.Enabled = true; } }
                        else
                        {
                            trbiz.Visible = true; chkBiz.Enabled = true;
                        }
                    }
                    if (ds.Tables[0].Rows[i]["COLUMN_NAME"].ToString() == "Invoice_Status_Enabled")
                    {
                        trAdmin.Visible = true;
                        trInvoiceStatus.Visible = true;
                        chkInvoiceStatus.Checked = false;
                        chkInvoiceStatus.Enabled = false;
                        hdnInvoiceStatus.Value = ds.Tables[0].Rows[i]["COLUMN_NAME"].ToString();
                        lblInvoiceStatus.Text = ds.Tables[0].Rows[i]["Column_Description"].ToString();
                        lblInvoiceStatus1.Text = ds.Tables[0].Rows[i]["CurrentValue"].ToString();
                        ddlAdminApprover.SelectedValue = ds.Tables[0].Rows[i]["ApproverID"].ToString();
                        ddlAdminFinalApprover.SelectedValue = ds.Tables[0].Rows[i]["Final_ApproverID"].ToString();
                        if (ds.Tables[0].Rows[i]["New_Value"].ToString() != "")
                        {
                            rdbInvoiceStatus.SelectedValue = ds.Tables[0].Rows[i]["New_Value"].ToString();
                        }
                        else
                        {
                            rdbInvoiceStatus.SelectedValue = ds.Tables[0].Rows[i]["CurrentValue"].ToString();
                        }
                        txtInvoiceStatusReason.Text = ds.Tables[0].Rows[i]["Reason_For_Change"].ToString();
                        if (ds.Tables[0].Rows[i]["Status"].ToString() == "SUBMITTED") { if (ds.Tables[0].Rows[i]["Send_For_Approve"].ToString() == "1") { trInvoiceStatus.Visible = true; chkInvoiceStatus.Enabled = true; } }
                        else if (ds.Tables[0].Rows[i]["Status"].ToString() == "APPROVED") { if (ds.Tables[0].Rows[i]["Send_For_FinalApprove"].ToString() == "1") { trInvoiceStatus.Visible = true; chkInvoiceStatus.Enabled = true; } }
                        else
                        {
                            trInvoiceStatus.Visible = true; chkInvoiceStatus.Enabled = true;
                        }
                    }
                    if (ds.Tables[0].Rows[i]["COLUMN_NAME"].ToString() == "Invoice_Upload_Enabled")
                    {
                        trAdmin.Visible = true;
                        trdirectinvoice.Visible = true;
                        chkDirectInvoice.Checked = false;
                        chkDirectInvoice.Enabled = false;
                        hdndirectinvoice.Value = ds.Tables[0].Rows[i]["COLUMN_NAME"].ToString();
                        lbldirectinvoice.Text = ds.Tables[0].Rows[i]["Column_Description"].ToString();
                        lbldirectinvoice1.Text = ds.Tables[0].Rows[i]["CurrentValue"].ToString();
                        rdbdirectinvoice.SelectedValue = ds.Tables[0].Rows[i]["New_Value"].ToString();
                        ddlAdminApprover.SelectedValue = ds.Tables[0].Rows[i]["ApproverID"].ToString();
                        ddlAdminFinalApprover.SelectedValue = ds.Tables[0].Rows[i]["Final_ApproverID"].ToString();
                        if (ds.Tables[0].Rows[i]["New_Value"].ToString() != "")
                        {
                            rdbdirectinvoice.SelectedValue = ds.Tables[0].Rows[i]["New_Value"].ToString();
                        }
                        else
                        {
                            rdbdirectinvoice.SelectedValue = ds.Tables[0].Rows[i]["CurrentValue"].ToString();
                        }
                        txtdirectinvoiceReason.Text = ds.Tables[0].Rows[i]["Reason_For_Change"].ToString();
                        if (ds.Tables[0].Rows[i]["Status"].ToString() == "SUBMITTED") { if (ds.Tables[0].Rows[i]["Send_For_Approve"].ToString() == "1") { trdirectinvoice.Visible = true; chkDirectInvoice.Enabled = true; } }
                        else if (ds.Tables[0].Rows[i]["Status"].ToString() == "APPROVED") { if (ds.Tables[0].Rows[i]["Send_For_FinalApprove"].ToString() == "1") { trdirectinvoice.Visible = true; chkDirectInvoice.Enabled = true; } }
                        else
                        {
                            trdirectinvoice.Visible = true; chkDirectInvoice.Enabled = true;
                        }
                    }
                    if (ds.Tables[0].Rows[i]["COLUMN_NAME"].ToString() == "Payment_Interval")
                    {
                        trAccount.Visible = true;
                        TRPaymenyInterval.Visible = true;
                        chkPaymentInterval.Checked = false;
                        chkPaymentInterval.Enabled = false;
                        hdnPaymentInterval.Value = ds.Tables[0].Rows[i]["COLUMN_NAME"].ToString();
                        lblPaymenyInterval.Text = ds.Tables[0].Rows[i]["Column_Description"].ToString();
                        lblPaymenyInterval1.Text = ds.Tables[0].Rows[i]["CurrentValue"].ToString();
                        ddlPaymentInterval.SelectedValue = ds.Tables[0].Rows[i]["New_Value"].ToString();
                        txtPaymentIntervalReason.Text = ds.Tables[0].Rows[i]["Reason_For_Change"].ToString();
                        ddlAccApprover.SelectedValue = ds.Tables[0].Rows[i]["ApproverID"].ToString();
                        ddlAccFinalApprover.SelectedValue = ds.Tables[0].Rows[i]["Final_ApproverID"].ToString();

                        ddlPaymentInterval.Items.Remove(ddlPaymentInterval.Items.FindByText(ds.Tables[0].Rows[i]["CurrentValue"].ToString()));

                        if (ds.Tables[0].Rows[i]["Status"].ToString() == "SUBMITTED") { if (ds.Tables[0].Rows[i]["Send_For_Approve"].ToString() == "1") { TRPaymenyInterval.Visible = true; chkPaymentInterval.Enabled = true; } }
                        else if (ds.Tables[0].Rows[i]["Status"].ToString() == "APPROVED") { if (ds.Tables[0].Rows[i]["Send_For_FinalApprove"].ToString() == "1") { TRPaymenyInterval.Visible = true; chkPaymentInterval.Enabled = true; } }
                        else
                        {
                            TRPaymenyInterval.Visible = true; chkPaymentInterval.Enabled = true;
                        }
                    }
                    if (ds.Tables[0].Rows[i]["COLUMN_NAME"].ToString() == "Payment_Priority")
                    {
                        trAccount.Visible = true;
                        TRPaymenypriority.Visible = true;
                        chkPaymentPriority.Checked = false;
                        chkPaymentPriority.Enabled = false;
                        hdnPaymentPriority.Value = ds.Tables[0].Rows[i]["COLUMN_NAME"].ToString();
                        lblPaymenypriority.Text = ds.Tables[0].Rows[i]["Column_Description"].ToString();
                        lblPaymenypriority1.Text = ds.Tables[0].Rows[i]["CurrentValue"].ToString();
                        ddlAccApprover.SelectedValue = ds.Tables[0].Rows[i]["ApproverID"].ToString();
                        ddlAccFinalApprover.SelectedValue = ds.Tables[0].Rows[i]["Final_ApproverID"].ToString();


                        if (ds.Tables[0].Rows[i]["New_Value"].ToString() != "")
                        {
                            rdbPaymentPriority.SelectedValue = ds.Tables[0].Rows[i]["New_Value"].ToString();
                        }
                        else
                        {
                            rdbPaymentPriority.SelectedValue = ds.Tables[0].Rows[i]["CurrentValue"].ToString();
                        }
                        txtPaymentPriorityReason.Text = ds.Tables[0].Rows[i]["Reason_For_Change"].ToString();
                        if (ds.Tables[0].Rows[i]["Status"].ToString() == "SUBMITTED") { if (ds.Tables[0].Rows[i]["Send_For_Approve"].ToString() == "1") { TRPaymenypriority.Visible = true; chkPaymentPriority.Enabled = true; } }
                        else if (ds.Tables[0].Rows[i]["Status"].ToString() == "APPROVED") { if (ds.Tables[0].Rows[i]["Send_For_FinalApprove"].ToString() == "1") { TRPaymenypriority.Visible = true; chkPaymentPriority.Enabled = true; } }
                        else
                        {
                            TRPaymenypriority.Visible = true; chkPaymentPriority.Enabled = true;
                        }
                    }
                    if (ds.Tables[0].Rows[i]["COLUMN_NAME"].ToString() == "Payment_Terms_Days")
                    {
                        trAccount.Visible = true;
                        TRPaymentTerms.Visible = true;
                        chkPaymentTerms.Checked = false;
                        chkPaymentTerms.Enabled = false;
                        hdnPaymentTerms.Value = ds.Tables[0].Rows[i]["COLUMN_NAME"].ToString();
                        lblPaymentTerms.Text = ds.Tables[0].Rows[i]["Column_Description"].ToString();
                        lblPaymentTerms1.Text = ds.Tables[0].Rows[i]["CurrentValue"].ToString();
                        ddlPaymentTerms.SelectedValue = ds.Tables[0].Rows[i]["New_Value"].ToString();
                        txtPaymentTermsReason.Text = ds.Tables[0].Rows[i]["Reason_For_Change"].ToString();
                        ddlAccApprover.SelectedValue = ds.Tables[0].Rows[i]["ApproverID"].ToString();
                        ddlAccFinalApprover.SelectedValue = ds.Tables[0].Rows[i]["Final_ApproverID"].ToString();
                        ddlPaymentTerms.Items.Remove(ddlPaymentTerms.Items.FindByText(ds.Tables[0].Rows[i]["CurrentValue"].ToString()));
                        if (ds.Tables[0].Rows[i]["Status"].ToString() == "SUBMITTED") { if (ds.Tables[0].Rows[i]["Send_For_Approve"].ToString() == "1") { TRPaymentTerms.Visible = true; chkPaymentTerms.Enabled = true; } }
                        else if (ds.Tables[0].Rows[i]["Status"].ToString() == "APPROVED") { if (ds.Tables[0].Rows[i]["Send_For_FinalApprove"].ToString() == "1") { TRPaymentTerms.Visible = true; chkPaymentTerms.Enabled = true; } }
                        else
                        {
                            TRPaymentTerms.Visible = true; chkPaymentTerms.Enabled = true;
                        }
                    }
                    if (ds.Tables[0].Rows[i]["COLUMN_NAME"].ToString() == "Payment_History_Enabled")
                    {
                        trAdmin.Visible = true;
                        TRPaymentHistory.Visible = true;
                        chkPaymentHistory.Checked = false;
                        chkPaymentHistory.Enabled = false;
                        hdnPaymentHistory.Value = ds.Tables[0].Rows[i]["COLUMN_NAME"].ToString();
                        lblPaymentHistory.Text = ds.Tables[0].Rows[i]["Column_Description"].ToString();
                        lblPaymentHistory1.Text = ds.Tables[0].Rows[i]["CurrentValue"].ToString();
                        ddlAdminApprover.SelectedValue = ds.Tables[0].Rows[i]["ApproverID"].ToString();
                        ddlAdminFinalApprover.SelectedValue = ds.Tables[0].Rows[i]["Final_ApproverID"].ToString();
                        if (ds.Tables[0].Rows[i]["New_Value"].ToString() != "")
                        {
                            rdbPaymentHistory.SelectedValue = ds.Tables[0].Rows[i]["New_Value"].ToString();
                        }
                        else
                        {
                            rdbPaymentHistory.SelectedValue = ds.Tables[0].Rows[i]["CurrentValue"].ToString();
                        }
                        txtPaymentHistoryReason.Text = ds.Tables[0].Rows[i]["Reason_For_Change"].ToString();
                        if (ds.Tables[0].Rows[i]["Status"].ToString() == "SUBMITTED") { if (ds.Tables[0].Rows[i]["Send_For_Approve"].ToString() == "1") { TRPaymentHistory.Visible = true; chkPaymentHistory.Enabled = true; } }
                        else if (ds.Tables[0].Rows[i]["Status"].ToString() == "APPROVED") { if (ds.Tables[0].Rows[i]["Send_For_FinalApprove"].ToString() == "1") { TRPaymentHistory.Visible = true; chkPaymentHistory.Enabled = true; } }
                        else
                        {
                            TRPaymentHistory.Visible = true; chkPaymentHistory.Enabled = true;
                        }
                    }
                    if (ds.Tables[0].Rows[i]["COLUMN_NAME"].ToString() == "Auto_Send_PO")
                    {
                        trAccount.Visible = true;
                        TRAutoSendPO.Visible = true;
                        chkAutoSendPO.Checked = false;
                        chkAutoSendPO.Enabled = false;
                        hdnAutoSendPO.Value = ds.Tables[0].Rows[i]["COLUMN_NAME"].ToString();
                        lblAutoSendPO.Text = ds.Tables[0].Rows[i]["Column_Description"].ToString();
                        lblAutoSendPO1.Text = ds.Tables[0].Rows[i]["CurrentValue"].ToString();
                        ddlAccApprover.SelectedValue = ds.Tables[0].Rows[i]["ApproverID"].ToString();
                        ddlAccFinalApprover.SelectedValue = ds.Tables[0].Rows[i]["Final_ApproverID"].ToString();
                        if (ds.Tables[0].Rows[i]["New_Value"].ToString() != "")
                        {
                            rdbAutoSendPO.SelectedValue = ds.Tables[0].Rows[i]["New_Value"].ToString();
                        }
                        else
                        {
                            rdbAutoSendPO.SelectedValue = ds.Tables[0].Rows[i]["CurrentValue"].ToString();
                        }
                        txtAutoSendPO.Text = ds.Tables[0].Rows[i]["Reason_For_Change"].ToString();
                        if (ds.Tables[0].Rows[i]["Status"].ToString() == "SUBMITTED") { if (ds.Tables[0].Rows[i]["Send_For_Approve"].ToString() == "1") { TRAutoSendPO.Visible = true; chkAutoSendPO.Enabled = true; } }
                        else if (ds.Tables[0].Rows[i]["Status"].ToString() == "APPROVED") { if (ds.Tables[0].Rows[i]["Send_For_FinalApprove"].ToString() == "1") { TRAutoSendPO.Visible = true; chkAutoSendPO.Enabled = true; } }
                        else
                        {
                            TRAutoSendPO.Visible = true; chkAutoSendPO.Enabled = true;
                        }
                    }
                    if (ds.Tables[0].Rows[i]["COLUMN_NAME"].ToString() == "Supplier_Scope")
                    {
                        trGeneral.Visible = true;
                        trScope.Visible = true;
                        chkScope1.Checked = false;
                        chkScope1.Enabled = false;
                        hdnScope.Value = ds.Tables[0].Rows[i]["COLUMN_NAME"].ToString();
                        lblScope.Text = ds.Tables[0].Rows[i]["Column_Description"].ToString();
                        lblScope1.Text = ds.Tables[0].Rows[i]["CurrentValue"].ToString();
                        //rdbPaymentHistory.SelectedValue = ds.Tables[0].Rows[i]["New_Value"].ToString();
                        txtScopeReason.Text = ds.Tables[0].Rows[i]["Reason_For_Change"].ToString();
                        ddlGenApprover.SelectedValue = ds.Tables[0].Rows[i]["ApproverID"].ToString();
                        ddlGenFinalApprover.SelectedValue = ds.Tables[0].Rows[i]["Final_ApproverID"].ToString();
                        BindScope(UDFLib.ConvertIntegerToNull(hdnCRID.Value));
                        if (ds.Tables[0].Rows[i]["Status"].ToString() == "SUBMITTED") { if (ds.Tables[0].Rows[i]["Send_For_Approve"].ToString() == "1") { trScope.Visible = true; chkScope1.Enabled = true; } }
                        else if (ds.Tables[0].Rows[i]["Status"].ToString() == "APPROVED") { if (ds.Tables[0].Rows[i]["Send_For_FinalApprove"].ToString() == "1") { trScope.Visible = true; chkScope1.Enabled = true; } }
                        else
                        {
                            trScope.Visible = true; chkScope1.Enabled = true;
                        }
                    }
                    if (ds.Tables[0].Rows[i]["COLUMN_NAME"].ToString() == "Supplier_Port")
                    {
                        trGeneral.Visible = true;
                        trPort.Visible = true;
                        chkPort1.Checked = false;
                        chkPort1.Enabled = false;
                        hdnPort.Value = ds.Tables[0].Rows[i]["COLUMN_NAME"].ToString();
                        lblPort.Text = ds.Tables[0].Rows[i]["Column_Description"].ToString();
                        lblPort1.Text = ds.Tables[0].Rows[i]["CurrentValue"].ToString();
                        //rdbPaymentHistory.SelectedValue = ds.Tables[0].Rows[i]["New_Value"].ToString();
                        txtPortReason.Text = ds.Tables[0].Rows[i]["Reason_For_Change"].ToString();
                        ddlGenApprover.SelectedValue = ds.Tables[0].Rows[i]["ApproverID"].ToString();
                        ddlGenFinalApprover.SelectedValue = ds.Tables[0].Rows[i]["Final_ApproverID"].ToString();
                        BindPort(UDFLib.ConvertIntegerToNull(hdnCRID.Value));
                        if (ds.Tables[0].Rows[i]["Status"].ToString() == "SUBMITTED") { if (ds.Tables[0].Rows[i]["Send_For_Approve"].ToString() == "1") { trPort.Visible = true; chkPort1.Enabled = true; } }
                        else if (ds.Tables[0].Rows[i]["Status"].ToString() == "APPROVED") { if (ds.Tables[0].Rows[i]["Send_For_FinalApprove"].ToString() == "1") { trPort.Visible = true; chkPort1.Enabled = true; } }
                        else
                        {
                            trPort.Visible = true; chkPort1.Enabled = true;
                        }
                    }

                }

                //&& (UDFLib.ConvertToInteger(GetSessionUserID()) == UDFLib.ConvertToInteger(ds.Tables[0].Rows[i]["Created_By"].ToString())
                if (dr["Check_Status"].ToString() == "SUBMITTED")
                {
                    //btnSaveDraft.Visible = false;
                    btnRecallDraft.Visible = false;
                    btnRecallApprove.Visible = false;
                    btnSubmitRequest.Visible = false;
                    ddlAccApprover.Enabled = false;
                    ddlAccFinalApprover.Enabled = false;
                    ddlAdminApprover.Enabled = false;
                    ddlAdminFinalApprover.Enabled = false;
                    ddlGenApprover.Enabled = false;
                    ddlGenFinalApprover.Enabled = false;
                    lblSubmittedBY.Text = dr["Login_Name"].ToString();
                    lblSubmitteddate.Text = dr["Verified_Date"].ToString();
                    if (UDFLib.ConvertToInteger(GetSessionUserID()) == UDFLib.ConvertToInteger(dr["Verified_By"].ToString()))
                    {
                        EnableDisable(false);
                        btnRecallRequest.Enabled = true;
                        btnRecallRequest.Visible = true;
                        trSubmitted.Visible = true;
                    }
                    else
                    {
                        SetControl(true);
                        btnRecallRequest.Enabled = false;
                        btnRecallRequest.Visible = true;
                        btnApprove.Visible = true;
                        btnReject.Visible = true;
                        btnFinalApprove.Visible = true;
                    }
                    //Check Approver
                    if (objUAType.Approve == 1)
                    {
                        SetControl(true);
                        btnApprove.Visible = true;
                        btnReject.Visible = true;
                        btnFinalApprove.Visible = false;
                        btnFinalApprove.Enabled = true;
                        btnApprove.Enabled = true;
                        btnReject.Enabled = true;
                    }
                    else
                    {
                        btnApprove.Enabled = false;
                        btnReject.Enabled = false;
                        btnFinalApprove.Enabled = false;
                    }

                }
                else if (dr["Check_Status"].ToString() == "APPROVED")
                {
                    btnRecallDraft.Visible = false;
                    ddlAccApprover.Enabled = false;
                    ddlAccFinalApprover.Enabled = false;
                    ddlAdminApprover.Enabled = false;
                    ddlAdminFinalApprover.Enabled = false;
                    ddlGenApprover.Enabled = false;
                    ddlGenFinalApprover.Enabled = false;
                    btnSubmitRequest.Visible = false;
                    btnRecallRequest.Visible = false;
                    lblSubmittedBY.Text = dr["Login_Name"].ToString();
                    lblSubmitteddate.Text = dr["Verified_Date"].ToString();
                    if (objUAType.Approve == 1)
                    {
                        SetControl(true);
                        btnApprove.Visible = false;
                        btnReject.Visible = true;
                        btnFinalApprove.Visible = true;
                        btnFinalApprove.Enabled = true;
                        btnApprove.Enabled = true;
                        btnReject.Enabled = true;
                    }
                    else
                    {
                        btnApprove.Enabled = false;
                        btnReject.Enabled = false;
                        btnFinalApprove.Enabled = false;
                    }

                }
                else
                {
                    //EnableDisable(true);
                    btnFinalApprove.Visible = false;
                    btnApprove.Visible = false;
                    btnReject.Visible = false;
                    btnRecallRequest.Enabled = false;

                }
            }
            else
            {
                //EnableDisable(true);
                btnFinalApprove.Visible = false;
                btnApprove.Visible = false;
                btnReject.Visible = false;
                btnRecallRequest.Enabled = false;
                if (objUA.Add == 1)
                {
                    //SetControl(true);
                    //btnSaveDraft.Visible = true;
                    btnSubmitRequest.Visible = true;
                }
                else
                {
                    //btnSaveDraft.Visible = false;
                    btnSubmitRequest.Visible = false;
                }
                if (objUAType.Add == 1)
                {
                    //SetControl(true);
                    //btnSaveDraft.Visible = true;
                    btnSubmitRequest.Visible = true;
                }
                else
                {
                    //btnSaveDraft.Visible = false;
                    btnSubmitRequest.Visible = false;
                }
            }


        }
        catch { }
        {
        }
    }
    protected void BindScope(int? CRID)
    {
        string SupplierID = GetSuppID();
        DataSet ds = BLL_ASL_Supplier.Get_Supplier_Scope_CR(UDFLib.ConvertStringToNull(SupplierID),UDFLib.ConvertIntegerToNull(CRID));
        //Session["dtCRScope"] = dt;
        ddlScope.DataSource = ds.Tables[0];
        ddlScope.DataTextField = "Scope_Name";
        ddlScope.DataValueField = "Scope_ID";
        ddlScope.DataBind();
        chkScope.DataSource = ds.Tables[1];
        chkScope.DataTextField = "Scope_Name";
        chkScope.DataValueField = "Scope_ID";
        chkScope.DataBind();
        Session["dtCRScope"] = ds.Tables[1];
        int i = 0;
        foreach (ListItem chkitem in chkScope.Items)
        {
            chkitem.Selected = true;
            i++;
        }

    }
    protected void BindPort(int? CRID)
    {
        string SupplierID = GetSuppID();
        DataSet ds = BLL_ASL_Supplier.Get_Supplier_PORT_CR(UDFLib.ConvertStringToNull(SupplierID), UDFLib.ConvertIntegerToNull(CRID));
        //Session["dtCRPort"] = dt;
        ddlPort.DataSource = ds.Tables[0];
        ddlPort.DataTextField = "PORT_NAME";
        ddlPort.DataValueField = "PORT_ID";
        ddlPort.DataBind();
        chkPort.DataSource = ds.Tables[1];
        chkPort.DataTextField = "PORT_NAME";
        chkPort.DataValueField = "PORT_ID";
        chkPort.DataBind();

        Session["dtCRPort"] = ds.Tables[1];
        int i = 0;
        foreach (ListItem chkitem in chkPort.Items)
        {
            chkitem.Selected = true;
            i++;
        }
    }

    private void EnableDisable(bool blnFlag)
    {
        txtCompanyResgName.Enabled = blnFlag;
        txtSuppAddress.Enabled = blnFlag;
        ddlCountry.Enabled = blnFlag;
        txtShortName.Enabled = blnFlag;
        ddlType.Enabled = blnFlag;
        ddlSubType.Enabled = blnFlag;
        txtCity.Enabled = blnFlag;
        ddlCurrency.Enabled = blnFlag;
        ddlownerShip.Enabled = blnFlag;
        txtSupplierDesc.Enabled = blnFlag;
        txtbiz.Enabled = blnFlag;
        txtEmail.Enabled = blnFlag;
        txtPhone.Enabled = blnFlag;
        txtFax.Enabled = blnFlag;
        txtPICName.Enabled = blnFlag;

        txtPICEmail.Enabled = blnFlag;
        txtPICPhone.Enabled = blnFlag;
        txtPICName2.Enabled = blnFlag;
        txtAutoSendPO.Enabled = blnFlag;
        txtPICEmail2.Enabled = blnFlag;
        txtPICPhone2.Enabled = blnFlag;
        txtTerms.Enabled = blnFlag;
        txtPayment.Enabled = blnFlag;
        txtTaxRate.Enabled = blnFlag;
        txtEmailReason.Enabled = blnFlag;
        txtBankReason.Enabled = blnFlag;
        txtPaymentEmail.Enabled = blnFlag;
        ddlCurrency.Enabled = blnFlag;
        ddlownerShip.Enabled = blnFlag;
        ddlPaymentInterval.Enabled = blnFlag;
        ddlPaymentTerms.Enabled = blnFlag;
        rdbdirectinvoice.Enabled = blnFlag;
        rdbInvoiceStatus.Enabled = blnFlag;
        rdbPaymentHistory.Enabled = blnFlag;
        rdbPaymentPriority.Enabled = blnFlag;

        txtCNameReason.Enabled = blnFlag;
        txtTypeReason.Enabled = blnFlag;
        txtSubTypeReason.Enabled = blnFlag;
        txtSupplierDescReason.Enabled = blnFlag;
        txtSuppAddressReason.Enabled = blnFlag;
        txtCountryReason.Enabled = blnFlag;
        txtCityReason.Enabled = blnFlag;
        txtEmailReason.Enabled = blnFlag;
        txtPhoneReason.Enabled = blnFlag;
        txtFaxReason.Enabled = blnFlag;
        txtPICNameReason.Enabled = blnFlag;
        txtPICEmailReason.Enabled = blnFlag;

        txtPICPhoneReason.Enabled = blnFlag;
        txtPICName2Reason.Enabled = blnFlag;
        txtPICEmail2Reason.Enabled = blnFlag;
        txtPICPhone2Reason.Enabled = blnFlag;
        txtBankReason.Enabled = blnFlag;

        txtShortNameReason.Enabled = blnFlag;
        txtPaymentEmailReason.Enabled = blnFlag;
        txtCurrencyReason.Enabled = blnFlag;
        txtTermsReason.Enabled = blnFlag;
        txtTaxRateReason.Enabled = blnFlag;
        txtownerShipReason.Enabled = blnFlag;
        txtbizReason.Enabled = blnFlag;
        txtInvoiceStatusReason.Enabled = blnFlag;
        txtdirectinvoiceReason.Enabled = blnFlag;
        txtPaymentIntervalReason.Enabled = blnFlag;
        txtPaymentPriorityReason.Enabled = blnFlag;
        txtPaymentTermsReason.Enabled = blnFlag;
        txtPaymentHistoryReason.Enabled = blnFlag;
        ddlPort.Enabled = blnFlag;
        ddlScope.Enabled = blnFlag;
        btnScopeAdd.Enabled = blnFlag;
        btnPortAdd.Enabled = blnFlag;
        chkPort.Enabled = blnFlag;
        chkScope.Enabled = blnFlag;
        txtScopeReason.Enabled = blnFlag;
        txtPortReason.Enabled = blnFlag;


    }
    private void SetControl(bool blnFlag)
    {
        DataSet ds = (DataSet)Session["dsCR"];
        if (ds.Tables[0].Rows.Count > 0)
        {
            tdHeader.Visible = blnFlag;
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (ds.Tables[0].Rows[i]["COLUMN_NAME"].ToString() == "Supplier_Name" && ds.Tables[0].Rows[i]["Reason_For_Change"].ToString() != "") { tdchkCname.Visible = blnFlag; }
                if (ds.Tables[0].Rows[i]["COLUMN_NAME"].ToString() == "Supplier_Type" && ds.Tables[0].Rows[i]["Reason_For_Change"].ToString() != "") { tdchkType.Visible = blnFlag; }
                if (ds.Tables[0].Rows[i]["COLUMN_NAME"].ToString() == "Address" && ds.Tables[0].Rows[i]["Reason_For_Change"].ToString() != "") { tdchkAddress.Visible = blnFlag; }
                if (ds.Tables[0].Rows[i]["COLUMN_NAME"].ToString() == "CountryID" && ds.Tables[0].Rows[i]["Reason_For_Change"].ToString() != "") { tdchkCountry.Visible = blnFlag; }
                if (ds.Tables[0].Rows[i]["COLUMN_NAME"].ToString() == "City" && ds.Tables[0].Rows[i]["Reason_For_Change"].ToString() != "") { tdchkCity.Visible = blnFlag; }
                if (ds.Tables[0].Rows[i]["COLUMN_NAME"].ToString() == "Email" && ds.Tables[0].Rows[i]["Reason_For_Change"].ToString() != "") { tdchkEmail.Visible = blnFlag; }
                if (ds.Tables[0].Rows[i]["COLUMN_NAME"].ToString() == "Phone" && ds.Tables[0].Rows[i]["Reason_For_Change"].ToString() != "") { tdchkPhone.Visible = blnFlag; }
                if (ds.Tables[0].Rows[i]["COLUMN_NAME"].ToString() == "Fax" && ds.Tables[0].Rows[i]["Reason_For_Change"].ToString() != "") { tdchkFax.Visible = blnFlag; }
                if (ds.Tables[0].Rows[i]["COLUMN_NAME"].ToString() == "Contact_1" && ds.Tables[0].Rows[i]["Reason_For_Change"].ToString() != "") { tdchkPICName.Visible = blnFlag; }
                if (ds.Tables[0].Rows[i]["COLUMN_NAME"].ToString() == "Contact_1_Email" && ds.Tables[0].Rows[i]["Reason_For_Change"].ToString() != "") { tdchkPICEmail.Visible = blnFlag; }
                if (ds.Tables[0].Rows[i]["COLUMN_NAME"].ToString() == "Contact_1_Phone" && ds.Tables[0].Rows[i]["Reason_For_Change"].ToString() != "") { tdchkPICPhone.Visible = blnFlag; }
                if (ds.Tables[0].Rows[i]["COLUMN_NAME"].ToString() == "Contact_2" && ds.Tables[0].Rows[i]["Reason_For_Change"].ToString() != "") { tdchkPICName2.Visible = blnFlag; }
                if (ds.Tables[0].Rows[i]["COLUMN_NAME"].ToString() == "Contact_2_Email" && ds.Tables[0].Rows[i]["Reason_For_Change"].ToString() != "") { tdchkPICEmail2.Visible = blnFlag; }
                if (ds.Tables[0].Rows[i]["COLUMN_NAME"].ToString() == "Contact_2_Phone" && ds.Tables[0].Rows[i]["Reason_For_Change"].ToString() != "") { tdchkPICPhone2.Visible = blnFlag; }
                if (ds.Tables[0].Rows[i]["COLUMN_NAME"].ToString() == "Supplier_Short_Name" && ds.Tables[0].Rows[i]["Reason_For_Change"].ToString() != "") { tdchkShortName.Visible = blnFlag; }
                if (ds.Tables[0].Rows[i]["COLUMN_NAME"].ToString() == "Payment_Instructions" && ds.Tables[0].Rows[i]["Reason_For_Change"].ToString() != "") { tdchkPayment.Visible = blnFlag; }
                if (ds.Tables[0].Rows[i]["COLUMN_NAME"].ToString() == "Payment_Notifications" && ds.Tables[0].Rows[i]["Reason_For_Change"].ToString() != "") { tdchkPaymentEmail.Visible = blnFlag; }
                if (ds.Tables[0].Rows[i]["COLUMN_NAME"].ToString() == "Supplier_Currency" && ds.Tables[0].Rows[i]["Reason_For_Change"].ToString() != "") { tdchkCurrency.Visible = blnFlag; }
                if (ds.Tables[0].Rows[i]["COLUMN_NAME"].ToString() == "Payment_Terms" && ds.Tables[0].Rows[i]["Reason_For_Change"].ToString() != "") { tdchkTerms.Visible = blnFlag; }
                if (ds.Tables[0].Rows[i]["COLUMN_NAME"].ToString() == "GST_Rate" && ds.Tables[0].Rows[i]["Reason_For_Change"].ToString() != "") { tdchkTaxRate.Visible = blnFlag; }
                if (ds.Tables[0].Rows[i]["COLUMN_NAME"].ToString() == "Ownership_Type" && ds.Tables[0].Rows[i]["Reason_For_Change"].ToString() != "") { tdchkOwnership.Visible = blnFlag; }
                if (ds.Tables[0].Rows[i]["COLUMN_NAME"].ToString() == "Setup_Year" && ds.Tables[0].Rows[i]["Reason_For_Change"].ToString() != "") { tdchkBiz.Visible = blnFlag; }
                if (ds.Tables[0].Rows[i]["COLUMN_NAME"].ToString() == "Invoice_Status_Enabled" && ds.Tables[0].Rows[i]["Reason_For_Change"].ToString() != "") { tdchkInvoiceStatus.Visible = blnFlag; }
                if (ds.Tables[0].Rows[i]["COLUMN_NAME"].ToString() == "Invoice_Upload_Enabled" && ds.Tables[0].Rows[i]["Reason_For_Change"].ToString() != "") { tdchkDirectInvoice.Visible = blnFlag; }
                if (ds.Tables[0].Rows[i]["COLUMN_NAME"].ToString() == "Payment_Interval" && ds.Tables[0].Rows[i]["Reason_For_Change"].ToString() != "") { tdchkPaymentInterval.Visible = blnFlag; }
                if (ds.Tables[0].Rows[i]["COLUMN_NAME"].ToString() == "Payment_Priority" && ds.Tables[0].Rows[i]["Reason_For_Change"].ToString() != "") { tdchkPaymentPriority.Visible = blnFlag; }
                if (ds.Tables[0].Rows[i]["COLUMN_NAME"].ToString() == "Payment_Terms_Days" && ds.Tables[0].Rows[i]["Reason_For_Change"].ToString() != "") { tdchkPaymentTerms.Visible = blnFlag; }
                if (ds.Tables[0].Rows[i]["COLUMN_NAME"].ToString() == "Payment_History_Enabled" && ds.Tables[0].Rows[i]["Reason_For_Change"].ToString() != "") { tdchkPaymentHistory.Visible = blnFlag; }
                if (ds.Tables[0].Rows[i]["COLUMN_NAME"].ToString() == "Supplier_Scope" && ds.Tables[0].Rows[i]["Reason_For_Change"].ToString() != "") { tdchkScope.Visible = blnFlag; }
                if (ds.Tables[0].Rows[i]["COLUMN_NAME"].ToString() == "Supplier_Port" && ds.Tables[0].Rows[i]["Reason_For_Change"].ToString() != "") { tdchkPort.Visible = blnFlag; }
                if (ds.Tables[0].Rows[i]["COLUMN_NAME"].ToString() == "Supplier_Description" && ds.Tables[0].Rows[i]["Reason_For_Change"].ToString() != "") { tdchkSupplierDesc.Visible = blnFlag; }
                if (ds.Tables[0].Rows[i]["COLUMN_NAME"].ToString() == "Counterparty_Type" && ds.Tables[0].Rows[i]["Reason_For_Change"].ToString() != "") { tdchkSubType.Visible = blnFlag; }
                if (ds.Tables[0].Rows[i]["COLUMN_NAME"].ToString() == "Tax_Account_Number" && ds.Tables[0].Rows[i]["Reason_For_Change"].ToString() != "") { tdTaxNumber.Visible = blnFlag; }
                if (ds.Tables[0].Rows[i]["COLUMN_NAME"].ToString() == "Auto_Send_PO" && ds.Tables[0].Rows[i]["Reason_For_Change"].ToString() != "") { tdAutoSendPO.Visible = blnFlag; }
            }
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
    public string GetChangeRequestType()
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
    public int GetChangeRequestID()
    {
        try
        {
            if (Request.QueryString["ID"] != null)
            {
                return int.Parse(Request.QueryString["ID"].ToString());
            }

            else
                return 0;
        }
        catch { return 0; }
    }
    #endregion
    #region FillDropDown
    protected void BindCountry()
    {
        DataTable dt = objBLLCountry.Get_CountryList();
        ddlCountry.DataSource = dt;
        ddlCountry.DataTextField = "Country";
        ddlCountry.DataValueField = "ID";

        ddlCountry.DataBind();
        ddlCountry.Items.Insert(0, new ListItem("No Change", "0"));
    }
    protected void BindType()
    {
        DataTable dt = BLL_ASL_Supplier.Get_ASL_System_Parameter(2, null, UDFLib.ConvertToInteger(GetSessionUserID()));
        ddlType.DataSource = dt;
        ddlType.DataValueField = "Description";
        ddlType.DataTextField = "Name";
        ddlType.DataBind();
        ddlType.Items.Insert(0, new ListItem("No Change", "0"));
    }
    protected void BindSubType()
    {
        DataTable dt = BLL_ASL_Supplier.Get_ASL_System_Parameter(0, null, UDFLib.ConvertToInteger(GetSessionUserID()));
        ddlSubType.DataSource = dt;
        ddlSubType.DataValueField = "Name";
        ddlSubType.DataTextField = "Description";
        ddlSubType.DataBind();
        ddlSubType.Items.Insert(0, new ListItem("No Change", "0"));
    }
    protected void BindPaymentInterval()
    {
        DataTable dt = BLL_ASL_Supplier.Get_ASL_System_Parameter(36, null, UDFLib.ConvertToInteger(GetSessionUserID()));

        ddlPaymentInterval.DataSource = dt;
        ddlPaymentInterval.DataValueField = "Code";
        ddlPaymentInterval.DataTextField = "Name";
        ddlPaymentInterval.DataBind();
        ddlPaymentInterval.Items.Insert(0, new ListItem("No Change", "0"));
    }
    protected void BindPaymenterms()
    {
        DataTable dt = BLL_ASL_Supplier.Get_ASL_System_Parameter(43, null, UDFLib.ConvertToInteger(GetSessionUserID()));

        ddlPaymentTerms.DataSource = dt;
        ddlPaymentTerms.DataValueField = "Code";
        ddlPaymentTerms.DataTextField = "Name";
        ddlPaymentTerms.DataBind();
        ddlPaymentTerms.Items.Insert(0, new ListItem("No Change", "0"));
    }
    protected void BindOwnerShip()
    {
        DataTable dt = BLL_ASL_Supplier.Get_ASL_System_Parameter(26, null, UDFLib.ConvertToInteger(GetSessionUserID()));

        ddlownerShip.DataSource = dt;
        ddlownerShip.DataValueField = "Code";
        ddlownerShip.DataTextField = "Name";
        ddlownerShip.DataBind();
        ddlownerShip.Items.Insert(0, new ListItem("No Change", "0"));
    }
    protected void BindCurrencyDLL()
    {

        DataTable dt = objBLLCurrency.Get_CurrencyList();

        ddlCurrency.DataSource = dt;
        ddlCurrency.DataTextField = "Currency_Code";
        ddlCurrency.DataValueField = "Currency_Code";
        ddlCurrency.DataBind();
        ddlCurrency.Items.Insert(0, new ListItem("No Change", "0"));

    }
    #endregion

    #region ControlClick
    protected int Save(string CR_Status, string Action_On_Data_Form)
    {
        string SupplierID = GetSuppID();

        DataTable dtcr = new DataTable();
        dtcr.Columns.Add("PKID");
        dtcr.Columns.Add("Field_Name");
        dtcr.Columns.Add("Old_Value");
        dtcr.Columns.Add("New_Value");
        dtcr.Columns.Add("Reason_For_Change");
        dtcr.Columns.Add("Approver");
        dtcr.Columns.Add("Final_Approver");

        if (txtCompanyResgName.Text.Trim() != lblRegname1.Text.ToString() && txtCNameReason.Text.Trim() != "")
        {
            DataRow dr = dtcr.NewRow();
            dr["Field_Name"] = hdnCName.Value.ToString();
            dr["Old_Value"] = lblRegname1.Text.ToString();
            dr["New_Value"] = txtCompanyResgName.Text.ToString();
            dr["Reason_For_Change"] = txtCNameReason.Text.ToString();
            dr["Approver"] = ddlAccApprover.SelectedValue.ToString();
            dr["Final_Approver"] = ddlAccFinalApprover.SelectedValue.ToString();
            dtcr.Rows.Add(dr);
        }
        if (ddlType.SelectedItem.Text != lblType1.Text.ToString() && txtTypeReason.Text.Trim() != "")
        {
            DataRow dr = dtcr.NewRow();
            dr["Field_Name"] = hdnType.Value.ToString();
            dr["Old_Value"] = lblType1.Text.ToString();
            dr["New_Value"] = ddlType.SelectedValue.ToString();
            dr["Reason_For_Change"] = txtTypeReason.Text.ToString();
            dr["Approver"] = ddlAdminApprover.SelectedValue.ToString();
            dr["Final_Approver"] = ddlAdminFinalApprover.SelectedValue.ToString();
            dtcr.Rows.Add(dr);
        }
        if (ddlSubType.SelectedItem.Text != lblSubType1.Text.ToString() && txtSubTypeReason.Text.Trim() != "")
        {
            DataRow dr = dtcr.NewRow();
            dr["Field_Name"] = hdnSubType.Value.ToString();
            dr["Old_Value"] = lblSubType1.Text.ToString();
            dr["New_Value"] = ddlSubType.SelectedValue.ToString();
            dr["Reason_For_Change"] = txtSubTypeReason.Text.ToString();

            dr["Approver"] = ddlAdminApprover.SelectedValue.ToString();
            dr["Final_Approver"] = ddlAdminFinalApprover.SelectedValue.ToString();
            dtcr.Rows.Add(dr);
        }
        if (txtTaxNumber.Text.Trim() != lblTaxNumber1.Text.ToString() && txtTaxNumberReason.Text.Trim() != "")
        {
            DataRow dr = dtcr.NewRow();
            dr["Field_Name"] = hdnTaxNumber.Value.ToString();
            dr["Old_Value"] = lblTaxNumber1.Text.ToString();
            dr["New_Value"] = txtTaxNumber.Text.ToString();
            dr["Reason_For_Change"] = txtTaxNumberReason.Text.ToString();

            dr["Approver"] = ddlAdminApprover.SelectedValue.ToString();
            dr["Final_Approver"] = ddlAdminFinalApprover.SelectedValue.ToString();
            dtcr.Rows.Add(dr);
        }
        if (txtSuppAddress.Text.Trim() != lblAddress1.Text.ToString() && txtSuppAddressReason.Text.Trim() != "")
        {
            DataRow dr = dtcr.NewRow();
            dr["Field_Name"] = hdnSuppAddress.Value.ToString();
            dr["Old_Value"] = lblAddress1.Text.ToString();
            dr["New_Value"] = txtSuppAddress.Text.ToString();
            dr["Reason_For_Change"] = txtSuppAddressReason.Text.ToString();
            dr["Approver"] = ddlGenApprover.SelectedValue.ToString();
            dr["Final_Approver"] = ddlGenFinalApprover.SelectedValue.ToString();
            dtcr.Rows.Add(dr);
        }
        if (ddlCountry.SelectedItem.Text != lblCountry1.Text.ToString() && txtCountryReason.Text.Trim() != "")
        {
            DataRow dr = dtcr.NewRow();
            dr["Field_Name"] = hdnCountry.Value.ToString();
            dr["Old_Value"] = lblCountry1.Text.ToString();
            dr["New_Value"] = ddlCountry.SelectedValue.ToString();
            dr["Reason_For_Change"] = txtCountryReason.Text.ToString();
            dr["Approver"] = ddlGenApprover.SelectedValue.ToString();
            dr["Final_Approver"] = ddlGenFinalApprover.SelectedValue.ToString();
            dtcr.Rows.Add(dr);
        }
        if (txtCity.Text.Trim() != lblCity1.Text.ToString() && txtCityReason.Text.Trim() != "")
        {
            DataRow dr = dtcr.NewRow();
            dr["Field_Name"] = hdnCity.Value.ToString();
            dr["Old_Value"] = lblCity1.Text.ToString();
            dr["New_Value"] = txtCity.Text.ToString();
            dr["Reason_For_Change"] = txtCityReason.Text.ToString();
            dr["Approver"] = ddlGenApprover.SelectedValue.ToString();
            dr["Final_Approver"] = ddlGenFinalApprover.SelectedValue.ToString();
            dtcr.Rows.Add(dr);
        }
        if (txtEmail.Text.Trim() != lblEmail1.Text.ToString() && txtEmailReason.Text.Trim() != "")
        {
            DataRow dr = dtcr.NewRow();
            dr["Field_Name"] = hdnEmail.Value.ToString();
            dr["Old_Value"] = lblEmail1.Text.ToString();
            dr["New_Value"] = txtEmail.Text.ToString();
            dr["Reason_For_Change"] = txtEmailReason.Text.ToString();
            dr["Approver"] = ddlGenApprover.SelectedValue.ToString();
            dr["Final_Approver"] = ddlGenFinalApprover.SelectedValue.ToString();
            dtcr.Rows.Add(dr);
        }
        if (txtPhone.Text.Trim() != lblPhone1.Text.ToString() && txtPhoneReason.Text.Trim() != "")
        {
            DataRow dr = dtcr.NewRow();
            dr["Field_Name"] = hdnPhone.Value.ToString();
            dr["Old_Value"] = lblPhone1.Text.ToString();
            dr["New_Value"] = txtPhone.Text.ToString();
            dr["Reason_For_Change"] = txtPhoneReason.Text.ToString();
            dr["Approver"] = ddlGenApprover.SelectedValue.ToString();
            dr["Final_Approver"] = ddlGenFinalApprover.SelectedValue.ToString();
            dtcr.Rows.Add(dr);
        }
        if (txtFax.Text.Trim() != lblFax1.Text.ToString() && txtFaxReason.Text.Trim() != "")
        {
            DataRow dr = dtcr.NewRow();
            dr["Field_Name"] = hdnFax.Value.ToString();
            dr["Old_Value"] = lblFax1.Text.ToString();
            dr["New_Value"] = txtFax.Text.ToString();
            dr["Reason_For_Change"] = txtFaxReason.Text.ToString();
            dr["Approver"] = ddlGenApprover.SelectedValue.ToString();
            dr["Final_Approver"] = ddlGenFinalApprover.SelectedValue.ToString();
            dtcr.Rows.Add(dr);
        }

        if (txtPICName.Text.Trim() != lblPICName1.Text.ToString() && txtPICNameReason.Text.Trim() != "")
        {
            DataRow dr = dtcr.NewRow();
            dr["Field_Name"] = hdnPICName.Value.ToString();
            dr["Old_Value"] = lblPICName1.Text.ToString();
            dr["New_Value"] = txtPICName.Text.ToString();
            dr["Reason_For_Change"] = txtPICNameReason.Text.ToString();
            dr["Approver"] = ddlGenApprover.SelectedValue.ToString();
            dr["Final_Approver"] = ddlGenFinalApprover.SelectedValue.ToString();
            dtcr.Rows.Add(dr);
        }
        if (txtPICEmail.Text.Trim() != lblPICEmail1.Text.ToString() && txtPICEmailReason.Text.Trim() != "")
        {
            DataRow dr = dtcr.NewRow();
            dr["Field_Name"] = hdnPICEmail.Value.ToString();
            dr["Old_Value"] = lblPICEmail1.Text.ToString();
            dr["New_Value"] = txtPICEmail.Text.ToString();
            dr["Reason_For_Change"] = txtPICEmailReason.Text.ToString();
            dr["Approver"] = ddlGenApprover.SelectedValue.ToString();
            dr["Final_Approver"] = ddlGenFinalApprover.SelectedValue.ToString();
            dtcr.Rows.Add(dr);
        }
        if (txtPICPhone.Text.Trim() != lblPICPhone1.Text.ToString() && txtPICPhoneReason.Text.Trim() != "")
        {
            DataRow dr = dtcr.NewRow();
            dr["Field_Name"] = hdnPICPhone.Value.ToString();
            dr["Old_Value"] = lblPICPhone1.Text.ToString();
            dr["New_Value"] = txtPICPhone.Text.ToString();
            dr["Reason_For_Change"] = txtPICPhoneReason.Text.ToString();
            dr["Approver"] = ddlGenApprover.SelectedValue.ToString();
            dr["Final_Approver"] = ddlGenFinalApprover.SelectedValue.ToString();
            dtcr.Rows.Add(dr);
        }
        if (txtPICName2.Text.Trim() != lblPICName21.Text.ToString() && txtPICName2Reason.Text.Trim() != "")
        {
            DataRow dr = dtcr.NewRow();
            dr["Field_Name"] = hdnPICName2.Value.ToString();
            dr["Old_Value"] = lblPICName21.Text.ToString();
            dr["New_Value"] = txtPICName2.Text.ToString();
            dr["Reason_For_Change"] = txtPICName2Reason.Text.ToString();
            dr["Approver"] = ddlGenApprover.SelectedValue.ToString();
            dr["Final_Approver"] = ddlGenFinalApprover.SelectedValue.ToString();
            dtcr.Rows.Add(dr);
        }
        if (txtPICEmail2.Text.Trim() != lblPICEmail21.Text.ToString() && txtPICEmail2Reason.Text.Trim() != "")
        {
            DataRow dr = dtcr.NewRow();
            dr["Field_Name"] = hdnPICEmail2.Value.ToString();
            dr["Old_Value"] = lblPICEmail21.Text.ToString();
            dr["New_Value"] = txtPICEmail2.Text.ToString();
            dr["Reason_For_Change"] = txtPICEmail2Reason.Text.ToString();
            dr["Approver"] = ddlGenApprover.SelectedValue.ToString();
            dr["Final_Approver"] = ddlGenFinalApprover.SelectedValue.ToString();
            dtcr.Rows.Add(dr);
        }
        if (txtPICPhone2.Text.Trim() != lblPICPhone21.Text.ToString() && txtPICPhone2Reason.Text.Trim() != "")
        {
            DataRow dr = dtcr.NewRow();
            dr["Field_Name"] = hdnPICPhone2.Value.ToString();
            dr["Old_Value"] = lblPICPhone21.Text.ToString();
            dr["New_Value"] = txtPICPhone2.Text.ToString();
            dr["Reason_For_Change"] = txtPICPhone2Reason.Text.ToString();
            dr["Approver"] = ddlGenApprover.SelectedValue.ToString();
            dr["Final_Approver"] = ddlGenFinalApprover.SelectedValue.ToString();
            dtcr.Rows.Add(dr);
        }
        if (txtPayment.Text.Trim() != lblPayment1.Text.ToString() && txtBankReason.Text.Trim() != "")
        {
            DataRow dr = dtcr.NewRow();
            dr["Field_Name"] = hdnPayment.Value.ToString();
            dr["Old_Value"] = lblPayment1.Text.ToString();
            dr["New_Value"] = txtPayment.Text.ToString();
            dr["Reason_For_Change"] = txtBankReason.Text.ToString();
            dr["Approver"] = ddlAccApprover.SelectedValue.ToString();
            dr["Final_Approver"] = ddlAccFinalApprover.SelectedValue.ToString();
            dtcr.Rows.Add(dr);
        }
        if (txtShortName.Text.Trim() != lblShortname1.Text.ToString() && txtShortNameReason.Text.Trim() != "")
        {
            DataRow dr = dtcr.NewRow();
            dr["Field_Name"] = hdnShortName.Value.ToString();
            dr["Old_Value"] = lblShortname1.Text.ToString();
            dr["New_Value"] = txtShortName.Text.ToString();
            dr["Reason_For_Change"] = txtShortNameReason.Text.ToString();
            dr["Approver"] = ddlAdminApprover.SelectedValue.ToString();
            dr["Final_Approver"] = ddlAdminFinalApprover.SelectedValue.ToString();
            dtcr.Rows.Add(dr);
        }
        if (txtSupplierDesc.Text.Trim() != lblSupplierDesc1.Text.ToString() && txtSupplierDescReason.Text.Trim() != "")
        {
            DataRow dr = dtcr.NewRow();
            dr["Field_Name"] = hdnSupplierDesc.Value.ToString();
            dr["Old_Value"] = lblSupplierDesc1.Text.ToString();
            dr["New_Value"] = txtSupplierDesc.Text.ToString();
            dr["Reason_For_Change"] = txtSupplierDescReason.Text.ToString();
            dr["Approver"] = ddlGenApprover.SelectedValue.ToString();
            dr["Final_Approver"] = ddlGenFinalApprover.SelectedValue.ToString();
            dtcr.Rows.Add(dr);
        }
        if (txtPaymentEmail.Text.Trim() != lblPaymentEmail1.Text.ToString() && txtPaymentEmailReason.Text.Trim() != "")
        {
            DataRow dr = dtcr.NewRow();
            dr["Field_Name"] = hdnPaymentEmail.Value.ToString();
            dr["Old_Value"] = lblPaymentEmail1.Text.ToString();
            dr["New_Value"] = txtPaymentEmail.Text.ToString();
            dr["Reason_For_Change"] = txtPaymentEmailReason.Text.ToString();
            dr["Approver"] = ddlAccApprover.SelectedValue.ToString();
            dr["Final_Approver"] = ddlAccFinalApprover.SelectedValue.ToString();
            dtcr.Rows.Add(dr);
        }
        if (ddlCurrency.SelectedItem.Text != lblCurrency1.Text.ToString() && txtCurrencyReason.Text.Trim() != "")
        {
            DataRow dr = dtcr.NewRow();
            dr["Field_Name"] = hdnCurrency.Value.ToString();
            dr["Old_Value"] = lblCurrency1.Text.ToString();
            dr["New_Value"] = ddlCurrency.SelectedValue.ToString();
            dr["Reason_For_Change"] = txtCurrencyReason.Text.ToString();
            dr["Approver"] = ddlAdminApprover.SelectedValue.ToString();
            dr["Final_Approver"] = ddlAdminFinalApprover.SelectedValue.ToString();
            dtcr.Rows.Add(dr);
        }
        if (txtTerms.Text.Trim() != lblTerms1.Text.ToString() && txtTermsReason.Text.Trim() != "")
        {
            DataRow dr = dtcr.NewRow();
            dr["Field_Name"] = hdnTerms.Value.ToString();
            dr["Old_Value"] = lblTerms1.Text.ToString();
            dr["New_Value"] = txtTerms.Text.ToString();
            dr["Reason_For_Change"] = txtTermsReason.Text.ToString();
            dr["Approver"] = ddlAccApprover.SelectedValue.ToString();
            dr["Final_Approver"] = ddlAccFinalApprover.SelectedValue.ToString();
            dtcr.Rows.Add(dr);
        }
        if (txtTaxRate.Text.Trim() != lblTaxRate1.Text.ToString() && txtTaxRateReason.Text.Trim() != "")
        {
            DataRow dr = dtcr.NewRow();
            dr["Field_Name"] = hdnTaxRate.Value.ToString();
            dr["Old_Value"] = lblTaxRate1.Text.ToString();
            dr["New_Value"] = txtTaxRate.Text.ToString();
            dr["Reason_For_Change"] = txtTaxRateReason.Text.ToString();
            dr["Approver"] = ddlGenApprover.SelectedValue.ToString();
            dr["Final_Approver"] = ddlGenFinalApprover.SelectedValue.ToString();
            dtcr.Rows.Add(dr);
        }
        if (ddlownerShip.SelectedItem.Text != lblOwnerShip1.Text.ToString() && txtownerShipReason.Text.Trim() != "")
        {
            DataRow dr = dtcr.NewRow();
            dr["Field_Name"] = hdnownerShip.Value.ToString();
            dr["Old_Value"] = lblOwnerShip1.Text.ToString();
            dr["New_Value"] = ddlownerShip.SelectedValue.ToString();
            dr["Reason_For_Change"] = txtownerShipReason.Text.ToString();
            dr["Approver"] = ddlAdminApprover.SelectedValue.ToString();
            dr["Final_Approver"] = ddlAdminFinalApprover.SelectedValue.ToString();
            dtcr.Rows.Add(dr);
        }
        if (txtbiz.Text.Trim() != lblBiz1.Text.ToString() && txtbizReason.Text.Trim() != "")
        {
            DataRow dr = dtcr.NewRow();
            dr["Field_Name"] = hdnBiz.Value.ToString();
            dr["Old_Value"] = lblBiz1.Text.ToString();
            dr["New_Value"] = txtbiz.Text.ToString();
            dr["Reason_For_Change"] = txtbizReason.Text.ToString();
            dr["Approver"] = ddlGenApprover.SelectedValue.ToString();
            dr["Final_Approver"] = ddlGenFinalApprover.SelectedValue.ToString();
            dtcr.Rows.Add(dr);
        }

        if (ddlPaymentInterval.SelectedItem.Text != lblPaymenyInterval1.Text.ToString() && txtPaymentIntervalReason.Text.Trim() != "")
        {
            DataRow dr = dtcr.NewRow();
            dr["Field_Name"] = hdnPaymentInterval.Value.ToString();
            dr["Old_Value"] = lblPaymenyInterval1.Text.ToString();
            dr["New_Value"] = ddlPaymentInterval.SelectedValue.ToString();
            dr["Reason_For_Change"] = txtPaymentIntervalReason.Text.ToString();
            dr["Approver"] = ddlAccApprover.SelectedValue.ToString();
            dr["Final_Approver"] = ddlAccFinalApprover.SelectedValue.ToString();
            dtcr.Rows.Add(dr);
        }
        if (ddlPaymentTerms.SelectedItem.Text.Trim() != lblPaymentTerms1.Text.ToString() && txtPaymentTermsReason.Text.Trim() != "")
        {
            DataRow dr = dtcr.NewRow();
            dr["Field_Name"] = hdnPaymentTerms.Value.ToString();
            dr["Old_Value"] = lblPaymentTerms1.Text.ToString();
            dr["New_Value"] = ddlPaymentTerms.SelectedValue.ToString();
            dr["Reason_For_Change"] = txtPaymentTermsReason.Text.ToString();
            dr["Approver"] = ddlAccApprover.SelectedValue.ToString();
            dr["Final_Approver"] = ddlAccFinalApprover.SelectedValue.ToString();
            dtcr.Rows.Add(dr);
        }

        //&& lblPaymenypriority1.Text.ToString() != ""  && lblInvoiceStatus1.Text.ToString() != "" && lbldirectinvoice1.Text.ToString() != "" && lblPaymentHistory1.Text.ToString() != ""
        if (rdbInvoiceStatus.SelectedValue != lblInvoiceStatus1.Text.ToString() && txtInvoiceStatusReason.Text.Trim() != "")
        {
            DataRow dr = dtcr.NewRow();
            dr["Field_Name"] = hdnInvoiceStatus.Value.ToString();
            dr["Old_Value"] = lblInvoiceStatus1.Text.ToString();
            dr["New_Value"] = rdbInvoiceStatus.SelectedValue;
            dr["Reason_For_Change"] = txtInvoiceStatusReason.Text.ToString();
            dr["Approver"] = ddlAdminApprover.SelectedValue.ToString();
            dr["Final_Approver"] = ddlAdminFinalApprover.SelectedValue.ToString();
            dtcr.Rows.Add(dr);
        }


        if (rdbdirectinvoice.SelectedValue != lbldirectinvoice1.Text.ToString() && txtdirectinvoiceReason.Text.Trim() != "")
        {
            DataRow dr = dtcr.NewRow();
            dr["Field_Name"] = hdndirectinvoice.Value.ToString();
            dr["Old_Value"] = lbldirectinvoice1.Text.ToString();
            dr["New_Value"] = rdbdirectinvoice.SelectedValue;
            dr["Reason_For_Change"] = txtdirectinvoiceReason.Text.ToString();
            dr["Approver"] = ddlAdminApprover.SelectedValue.ToString();
            dr["Final_Approver"] = ddlAdminFinalApprover.SelectedValue.ToString();
            dtcr.Rows.Add(dr);
        }

        if (rdbAutoSendPO.SelectedValue != lblAutoSendPO1.Text.ToString() && txtAutoSendPO.Text.Trim() != "")
        {
            DataRow dr = dtcr.NewRow();
            dr["Field_Name"] = hdnAutoSendPO.Value.ToString();
            dr["Old_Value"] = lblAutoSendPO1.Text.ToString();
            dr["New_Value"] = rdbAutoSendPO.SelectedValue;
            dr["Reason_For_Change"] = txtAutoSendPO.Text.ToString();
            dr["Approver"] = ddlAccApprover.SelectedValue.ToString(); ;
            dr["Final_Approver"] = ddlAccFinalApprover.SelectedValue.ToString(); ;
            dtcr.Rows.Add(dr);
        }
        if (rdbPaymentHistory.SelectedValue != lblPaymentHistory1.Text.ToString() && txtPaymentHistoryReason.Text.Trim() != "")
        {
            DataRow dr = dtcr.NewRow();
            dr["Field_Name"] = hdnPaymentHistory.Value.ToString();
            dr["Old_Value"] = lblPaymentHistory1.Text.ToString();
            dr["New_Value"] = rdbPaymentHistory.SelectedValue;
            dr["Reason_For_Change"] = txtPaymentHistoryReason.Text.ToString();
            dr["Approver"] = ddlAdminApprover.SelectedValue.ToString();
            dr["Final_Approver"] = ddlAdminFinalApprover.SelectedValue.ToString();
            dtcr.Rows.Add(dr);
        }


        if (rdbPaymentPriority.SelectedValue != lblPaymenypriority1.Text.ToString() && txtPaymentPriorityReason.Text.Trim() != "")
        {
            DataRow dr = dtcr.NewRow();
            dr["Field_Name"] = hdnPaymentPriority.Value.ToString();
            dr["Old_Value"] = lblPaymenypriority1.Text.ToString();
            dr["New_Value"] = rdbPaymentPriority.SelectedValue;
            dr["Reason_For_Change"] = txtPaymentPriorityReason.Text.ToString();
            dr["Approver"] = ddlAccApprover.SelectedValue.ToString();
            dr["Final_Approver"] = ddlAccFinalApprover.SelectedValue.ToString();
            dtcr.Rows.Add(dr);
        }

        if (txtScopeReason.Text.Trim() != "")
        {
            DataRow dr = dtcr.NewRow();
            dr["Field_Name"] = hdnScope.Value.ToString();
            dr["Old_Value"] = lblScope1.Text.ToString();
            //dr["New_Value"] = rdbPaymentPriority.SelectedValue;
            dr["Reason_For_Change"] = txtScopeReason.Text.ToString();
            dr["Approver"] = ddlGenApprover.SelectedValue.ToString();
            dr["Final_Approver"] = ddlGenFinalApprover.SelectedValue.ToString();
            dtcr.Rows.Add(dr);

        }
        if (txtPortReason.Text.Trim() != "")
        {
            DataRow dr = dtcr.NewRow();
            dr["Field_Name"] = hdnPort.Value.ToString();
            dr["Old_Value"] = lblPort1.Text.ToString();
            //dr["New_Value"] = rdbPaymentPriority.SelectedValue;
            dr["Reason_For_Change"] = txtPortReason.Text.ToString();
            dr["Approver"] = ddlGenApprover.SelectedValue.ToString();
            dr["Final_Approver"] = ddlGenFinalApprover.SelectedValue.ToString();
            dtcr.Rows.Add(dr);

        }

        int RetValue = 0;
        if (dtcr.Rows.Count > 0)
        {
            RetValue = BLL_ASL_Supplier.Supplier_ChangeRequest_Data_Insert(UDFLib.ConvertIntegerToNull(hdnCRID.Value), UDFLib.ConvertStringToNull(SupplierID), dtcr, CR_Status, Action_On_Data_Form, UDFLib.ConvertToInteger(GetSessionUserID()));
            hdnCRID.Value = Convert.ToString(RetValue);
            if (txtScopeReason.Text.Trim() != "" && Action_On_Data_Form != "RECALL")
            {
                SaveSupplierScope_CR(CR_Status, UDFLib.ConvertIntegerToNull(hdnCRID.Value));
            }
            if (txtPortReason.Text.Trim() != "" && Action_On_Data_Form != "RECALL")
            {
                SaveSupplierPort_CR(CR_Status, UDFLib.ConvertIntegerToNull(hdnCRID.Value));
            }
            return RetValue;
        }
        else
        {
            return RetValue;
        }

    }
    protected void btnSaveDraft_Click(object sender, EventArgs e)
    {
        try
        {
            string CR_Status = "DRAFT";
            string Action_On_Data_Form = "DRAFT";
            int RetValue = Save(CR_Status, Action_On_Data_Form);

            if (RetValue == 0)
            {
                string message = "alert('No Rows Selected For Change.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                btnApprove.Visible = false;
                btnReject.Visible = false;
                btnRecallRequest.Enabled = false;
            }
            else if (RetValue == -1)
            {
                string message = "alert('Supplier Name already existsed.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            }
            else
            {
                BLL_ASL_Supplier.ASL_Send_CR_mail(CR_Status, UDFLib.ConvertToInteger(GetSessionUserID()), UDFLib.ConvertStringToNull(GetSuppID()), UDFLib.ConvertIntegerToNull(hdnCRID.Value));
                string message = "alert('Change Request saved as a Draft.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                btnApprove.Visible = false;
                btnReject.Visible = false;
                btnRecallRequest.Enabled = false;
                string msgDraft = String.Format("parent.ReloadParent_ByButtonID();");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgDraft", msgDraft, true);
                //Response.Redirect("../ASL/ASL_CR.aspx?Supp_ID=" + UDFLib.ConvertStringToNull(Request.QueryString["Supp_ID"].ToString()) + "");
            }

        }
        catch { }
        {
        }

    }
    protected void btnSubmitRequest_Click(object sender, EventArgs e)
    {
        try
        {
            string CR_Status = "SUBMITTED";
            string Action_On_Data_Form = "SUBMITTED";
            int RetValue = Save(CR_Status, Action_On_Data_Form);
            if (RetValue == 0)
            {
                string message = "alert('No Rows Selected For Change.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                btnApprove.Visible = false;
                btnReject.Visible = false;
                btnRecallRequest.Enabled = false;
            }
            else if (RetValue == -1)
            {
                string message = "alert('Supplier Name already existsed.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            }
            else
            {
                BLL_ASL_Supplier.ASL_Send_CR_mail(CR_Status, UDFLib.ConvertToInteger(GetSessionUserID()), UDFLib.ConvertStringToNull(GetSuppID()), UDFLib.ConvertIntegerToNull(hdnCRID.Value));
                string message = "alert('Change request submitted and send for approval.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                string msgDraft = String.Format("parent.ReloadParent_ByButtonID();");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgDraft", msgDraft, true);
                //Response.Redirect("../ASL/ASL_CR.aspx?Supp_ID=" + UDFLib.ConvertStringToNull(Request.QueryString["Supp_ID"].ToString()) + "");
            }
            //BindChangeRequest();
        }
        catch { }
        {
        }
    }
    protected void btnRecallRequest_Click(object sender, EventArgs e)
    {
        try
        {
            string CR_Status = "DRAFT";
            string Action_On_Data_Form = "RECALLSubmit";
            int RetValue = Save(CR_Status, Action_On_Data_Form);
            if (RetValue == 0)
            {
                string message = "alert('No Rows Selected For Change.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            }
            else
            {
                //BLL_ASL_Supplier.ASL_Send_CR_mail("Recall", UDFLib.ConvertToInteger(GetSessionUserID()), UDFLib.ConvertStringToNull(GetSuppID()), UDFLib.ConvertIntegerToNull(hdnCRID.Value));
                string message = "alert('Change Request Recalled.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                string msgDraft = String.Format("parent.ReloadParent_ByButtonID();");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgDraft", msgDraft, true);
            }
            //Response.Redirect("../ASL/ASL_CR.aspx?Supp_ID=" + UDFLib.ConvertStringToNull(Request.QueryString["Supp_ID"].ToString()) + "");
            //BindChangeRequest();
        }
        catch { }
        {
        }
    }
    protected void btnRecallDraft_Click(object sender, EventArgs e)
    {
        try
        {
            string CR_Status = null;
            string Action_On_Data_Form = "RECALLDraft";
            int RetValue = Save(CR_Status, Action_On_Data_Form);
            if (RetValue == 0)
            {
                string message = "alert('No Rows Selected For Change.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            }
            else
            {
                //BLL_ASL_Supplier.ASL_Send_CR_mail("Recall", UDFLib.ConvertToInteger(GetSessionUserID()), UDFLib.ConvertStringToNull(GetSuppID()), UDFLib.ConvertIntegerToNull(hdnCRID.Value));
                string message = "alert('Change Request Recalled.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                string msgDraft = String.Format("parent.ReloadParent_ByButtonID();");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgDraft", msgDraft, true);
            }
            //Response.Redirect("../ASL/ASL_CR.aspx?Supp_ID=" + UDFLib.ConvertStringToNull(Request.QueryString["Supp_ID"].ToString()) + "");
            //BindChangeRequest();
        }
        catch { }
        {
        }
    }
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        try
        {
            string CR_Status = "APPROVED";
            string Action_On_Data_Form = "APPROVED";
            int RetValue = Submit(CR_Status, Action_On_Data_Form);
            //SaveSupplierScope();
            //SaveSupplierPort();
            if (RetValue == 0)
            {
                string message = "alert('No Rows Selected For Change.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            }
            else
            {
                BLL_ASL_Supplier.ASL_Send_CR_mail(CR_Status, UDFLib.ConvertToInteger(GetSessionUserID()), UDFLib.ConvertStringToNull(GetSuppID()), UDFLib.ConvertIntegerToNull(hdnCRID.Value));
                string message = "alert('Change Request Approved.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                string msgDraft = String.Format("parent.ReloadParent_ByButtonID();");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgDraft", msgDraft, true);
            }
            //Response.Redirect("../ASL/ASL_CR.aspx?Supp_ID=" + UDFLib.ConvertStringToNull(Request.QueryString["Supp_ID"].ToString()) + "");
            // BindChangeRequest();
        }
        catch { }
        {
        }
    }
    protected void btnReject_Click(object sender, EventArgs e)
    {
        try
        {
            string CR_Status = "REJECTED";
            string Action_On_Data_Form = "REJECTED";
            int RetValue = Submit(CR_Status, Action_On_Data_Form);
            //SaveSupplierScope();
            //SaveSupplierPort();
            if (RetValue == 0)
            {
                string message = "alert('No Rows Selected For Change.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            }
            else
            {
                //BLL_ASL_Supplier.ASL_Send_CR_mail(CR_Status, UDFLib.ConvertToInteger(GetSessionUserID()), UDFLib.ConvertStringToNull(GetSuppID()), UDFLib.ConvertIntegerToNull(hdnCRID.Value));
                string message = "alert('Change Request Rejected.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                string msgDraft = String.Format("parent.ReloadParent_ByButtonID();");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgDraft", msgDraft, true);
            }
            //Response.Redirect("../ASL/ASL_CR.aspx?Supp_ID=" + UDFLib.ConvertStringToNull(Request.QueryString["Supp_ID"].ToString()) + "");
            //BindChangeRequest();
        }
        catch { }
        {
        }

    }
    protected void SaveSupplierScope_CR(string CRStatus,int? CRID)
    {

        string SupplierID = GetSuppID();
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
        //return  dtScope;
        BLL_ASL_Supplier.ASL_Supplier_Scope_CR_Insert(UDFLib.ConvertStringToNull(SupplierID), dtScope, CRStatus, UDFLib.ConvertToInteger(GetSessionUserID()), UDFLib.ConvertIntegerToNull(CRID));
    }

    protected void SaveSupplierPort_CR(string CRStatus, int? CRID)
    {
        string SupplierID = GetSuppID();
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
        //return dtService;
        BLL_ASL_Supplier.ASL_Supplier_Port_CR_Insert(UDFLib.ConvertStringToNull(SupplierID), dtService, CRStatus, UDFLib.ConvertToInteger(GetSessionUserID()), UDFLib.ConvertIntegerToNull(CRID));
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        DataTable dt = (DataTable)Session["dtCRScope"];
        DataRow dr = dt.NewRow();


        dr["Scope_ID"] = ddlScope.SelectedValue;
        dr["Scope_Name"] = ddlScope.SelectedItem.Text;
        dt.Rows.Add(dr);

        ddlScope.Items.RemoveAt(ddlScope.SelectedIndex);

        Session["dtCRScope"] = dt;
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
                string str = null;
                str = chkScope.SelectedItem.Text + "," + str;
                hdnAddScope.Value = str;
                i++;
            }

        }

    }

    protected void btnPortAdd_Click(object sender, EventArgs e)
    {
        DataTable dt = (DataTable)Session["dtCRPort"];
        DataRow dr = dt.NewRow();


        dr["PORT_ID"] = ddlPort.SelectedValue;
        dr["PORT_NAME"] = ddlPort.SelectedItem.Text;
        dt.Rows.Add(dr);

        ddlPort.Items.RemoveAt(ddlPort.SelectedIndex);

        Session["dtCRPort"] = dt;
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
                string str = null;
                str = chkPort.SelectedItem.Text + "," + str;
                hdnAddPort.Value = str;
                i++;
            }

        }
    }

    protected int Submit(string CR_Status, string Action_On_Data_Form)
    {
        string SupplierID = GetSuppID();

        DataTable dtcr = new DataTable();
        dtcr.Columns.Add("PKID");
        dtcr.Columns.Add("Field_Name");
        dtcr.Columns.Add("Old_Value");
        dtcr.Columns.Add("New_Value");
        dtcr.Columns.Add("Reason_For_Change");
        dtcr.Columns.Add("Approver");
        dtcr.Columns.Add("Final_Approver");

        if (chkCname.Checked == true)
        {
            DataRow dr = dtcr.NewRow();
            dr["Field_Name"] = hdnCName.Value.ToString();
            dr["Old_Value"] = lblRegname1.Text.ToString();
            dr["New_Value"] = txtCompanyResgName.Text.ToString();
            dr["Reason_For_Change"] = txtCNameReason.Text.ToString();
            dr["Approver"] = ddlAccApprover.SelectedValue.ToString();
            dr["Final_Approver"] = ddlAccFinalApprover.SelectedValue.ToString();
            dtcr.Rows.Add(dr);
        }
        if (chkType.Checked == true)
        {
            DataRow dr = dtcr.NewRow();
            dr["Field_Name"] = hdnType.Value.ToString();
            dr["Old_Value"] = lblType1.Text.ToString();
            dr["New_Value"] = ddlType.SelectedValue.ToString();
            dr["Reason_For_Change"] = txtTypeReason.Text.ToString();
            dr["Approver"] = ddlAdminApprover.SelectedValue.ToString();
            dr["Final_Approver"] = ddlAdminFinalApprover.SelectedValue.ToString();
            dtcr.Rows.Add(dr);
        }
        if (chkSubType.Checked == true)
        {
            DataRow dr = dtcr.NewRow();
            dr["Field_Name"] = hdnSubType.Value.ToString();
            dr["Old_Value"] = lblSubType1.Text.ToString();
            dr["New_Value"] = ddlSubType.SelectedValue.ToString();
            dr["Reason_For_Change"] = txtSubTypeReason.Text.ToString();
            dr["Approver"] = ddlAdminApprover.SelectedValue.ToString();
            dr["Final_Approver"] = ddlAdminFinalApprover.SelectedValue.ToString();
            dtcr.Rows.Add(dr);
        }
        if (chkTaxNumber.Checked == true)
        {
            DataRow dr = dtcr.NewRow();
            dr["Field_Name"] = hdnTaxNumber.Value.ToString();
            dr["Old_Value"] = lblTaxNumber1.Text.ToString();
            dr["New_Value"] = txtTaxNumber.Text.ToString();
            dr["Reason_For_Change"] = txtTaxNumberReason.Text.ToString();
            dr["Approver"] = ddlAdminApprover.SelectedValue.ToString();
            dr["Final_Approver"] = ddlAdminFinalApprover.SelectedValue.ToString();
            dtcr.Rows.Add(dr);
        }
        if (chkAddress.Checked == true)
        {
            DataRow dr = dtcr.NewRow();
            dr["Field_Name"] = hdnSuppAddress.Value.ToString();
            dr["Old_Value"] = lblAddress1.Text.ToString();
            dr["New_Value"] = txtSuppAddress.Text.ToString();
            dr["Reason_For_Change"] = txtSuppAddressReason.Text.ToString();
            dr["Approver"] = ddlGenApprover.SelectedValue.ToString();
            dr["Final_Approver"] = ddlGenFinalApprover.SelectedValue.ToString();
            dtcr.Rows.Add(dr);
        }
        if (chkCountry.Checked == true)
        {
            DataRow dr = dtcr.NewRow();
            dr["Field_Name"] = hdnCountry.Value.ToString();
            dr["Old_Value"] = lblCountry1.Text.ToString();
            dr["New_Value"] = ddlCountry.SelectedValue.ToString();
            dr["Reason_For_Change"] = txtCountryReason.Text.ToString();

            dr["Approver"] = ddlGenApprover.SelectedValue.ToString();
            dr["Final_Approver"] = ddlGenFinalApprover.SelectedValue.ToString();
            dtcr.Rows.Add(dr);
        }
        if (chkCity.Checked == true)
        {
            DataRow dr = dtcr.NewRow();
            dr["Field_Name"] = hdnCity.Value.ToString();
            dr["Old_Value"] = lblCity1.Text.ToString();
            dr["New_Value"] = txtCity.Text.ToString();
            dr["Reason_For_Change"] = txtCityReason.Text.ToString();

            dr["Approver"] = ddlGenApprover.SelectedValue.ToString();
            dr["Final_Approver"] = ddlGenFinalApprover.SelectedValue.ToString();
            dtcr.Rows.Add(dr);
        }
        if (chkEmail.Checked == true)
        {
            DataRow dr = dtcr.NewRow();
            dr["Field_Name"] = hdnEmail.Value.ToString();
            dr["Old_Value"] = lblEmail1.Text.ToString();
            dr["New_Value"] = txtEmail.Text.ToString();
            dr["Reason_For_Change"] = txtEmailReason.Text.ToString();

            dr["Approver"] = ddlGenApprover.SelectedValue.ToString();
            dr["Final_Approver"] = ddlGenFinalApprover.SelectedValue.ToString();
            dtcr.Rows.Add(dr);
        }
        if (chkPhone.Checked == true)
        {
            DataRow dr = dtcr.NewRow();
            dr["Field_Name"] = hdnPhone.Value.ToString();
            dr["Old_Value"] = lblPhone1.Text.ToString();
            dr["New_Value"] = txtPhone.Text.ToString();
            dr["Reason_For_Change"] = txtPhoneReason.Text.ToString();

            dr["Approver"] = ddlGenApprover.SelectedValue.ToString();
            dr["Final_Approver"] = ddlGenFinalApprover.SelectedValue.ToString();
            dtcr.Rows.Add(dr);
        }
        if (chkFax.Checked == true)
        {
            DataRow dr = dtcr.NewRow();
            dr["Field_Name"] = hdnFax.Value.ToString();
            dr["Old_Value"] = lblFax1.Text.ToString();
            dr["New_Value"] = txtFax.Text.ToString();
            dr["Reason_For_Change"] = txtFaxReason.Text.ToString();

            dr["Approver"] = ddlGenApprover.SelectedValue.ToString();
            dr["Final_Approver"] = ddlGenFinalApprover.SelectedValue.ToString();
            dtcr.Rows.Add(dr);
        }
        if (chkPICName.Checked == true)
        {
            DataRow dr = dtcr.NewRow();
            dr["Field_Name"] = hdnPICName.Value.ToString();
            dr["Old_Value"] = lblPICName1.Text.ToString();
            dr["New_Value"] = txtPICName.Text.ToString();
            dr["Reason_For_Change"] = txtPICNameReason.Text.ToString();

            dr["Approver"] = ddlGenApprover.SelectedValue.ToString();
            dr["Final_Approver"] = ddlGenFinalApprover.SelectedValue.ToString();
            dtcr.Rows.Add(dr);
        }
        if (chkPICEmail.Checked == true)
        {
            DataRow dr = dtcr.NewRow();
            dr["Field_Name"] = hdnPICEmail.Value.ToString();
            dr["Old_Value"] = lblRegname1.Text.ToString();
            dr["New_Value"] = txtPICEmail.Text.ToString();
            dr["Reason_For_Change"] = txtPICEmailReason.Text.ToString();

            dr["Approver"] = ddlGenApprover.SelectedValue.ToString();
            dr["Final_Approver"] = ddlGenFinalApprover.SelectedValue.ToString();
            dtcr.Rows.Add(dr);
        }
        if (chkPICPhone.Checked == true)
        {
            DataRow dr = dtcr.NewRow();
            dr["Field_Name"] = hdnPICPhone.Value.ToString();
            dr["Old_Value"] = lblPICPhone1.Text.ToString();
            dr["New_Value"] = txtPICPhone.Text.ToString();
            dr["Reason_For_Change"] = txtPICPhoneReason.Text.ToString();

            dr["Approver"] = ddlGenApprover.SelectedValue.ToString();
            dr["Final_Approver"] = ddlGenFinalApprover.SelectedValue.ToString();
            dtcr.Rows.Add(dr);
        }
        if (chkPICName2.Checked == true)
        {
            DataRow dr = dtcr.NewRow();
            dr["Field_Name"] = hdnPICName2.Value.ToString();
            dr["Old_Value"] = lblPICName21.Text.ToString();
            dr["New_Value"] = txtPICName2.Text.ToString();
            dr["Reason_For_Change"] = txtPICName2Reason.Text.ToString();

            dr["Approver"] = ddlGenApprover.SelectedValue.ToString();
            dr["Final_Approver"] = ddlGenFinalApprover.SelectedValue.ToString();
            dtcr.Rows.Add(dr);
        }
        if (chkPICEmail2.Checked == true)
        {
            DataRow dr = dtcr.NewRow();
            dr["Field_Name"] = hdnPICEmail2.Value.ToString();
            dr["Old_Value"] = lblPICEmail21.Text.ToString();
            dr["New_Value"] = txtPICEmail2.Text.ToString();
            dr["Reason_For_Change"] = txtPICEmail2Reason.Text.ToString();

            dr["Approver"] = ddlGenApprover.SelectedValue.ToString();
            dr["Final_Approver"] = ddlGenFinalApprover.SelectedValue.ToString();
            dtcr.Rows.Add(dr);
        }
        if (chkPICPhone2.Checked == true)
        {
            DataRow dr = dtcr.NewRow();
            dr["Field_Name"] = hdnPICPhone2.Value.ToString();
            dr["Old_Value"] = lblPICPhone21.Text.ToString();
            dr["New_Value"] = txtPICPhone2.Text.ToString();
            dr["Reason_For_Change"] = txtPICPhone2Reason.Text.ToString();

            dr["Approver"] = ddlGenApprover.SelectedValue.ToString();
            dr["Final_Approver"] = ddlGenFinalApprover.SelectedValue.ToString();
            dtcr.Rows.Add(dr);
        }
        if (chkPayment.Checked == true)
        {
            DataRow dr = dtcr.NewRow();
            dr["Field_Name"] = hdnPayment.Value.ToString();
            dr["Old_Value"] = lblPayment1.Text.ToString();
            dr["New_Value"] = txtPayment.Text.ToString();
            dr["Reason_For_Change"] = txtBankReason.Text.ToString();
            dr["Approver"] = ddlAccApprover.SelectedValue.ToString();
            dr["Final_Approver"] = ddlAccFinalApprover.SelectedValue.ToString();
            dtcr.Rows.Add(dr);
        }
        if (chkShortName.Checked == true)
        {
            DataRow dr = dtcr.NewRow();
            dr["Field_Name"] = hdnShortName.Value.ToString();
            dr["Old_Value"] = lblShortname1.Text.ToString();
            dr["New_Value"] = txtShortName.Text.ToString();
            dr["Reason_For_Change"] = txtShortNameReason.Text.ToString();
            dr["Approver"] = ddlAdminApprover.SelectedValue.ToString();
            dr["Final_Approver"] = ddlAdminFinalApprover.SelectedValue.ToString();
            dtcr.Rows.Add(dr);
        }
        if (chkSupplierDesc.Checked == true)
        {
            DataRow dr = dtcr.NewRow();
            dr["Field_Name"] = hdnSupplierDesc.Value.ToString();
            dr["Old_Value"] = lblSupplierDesc1.Text.ToString();
            dr["New_Value"] = txtSupplierDesc.Text.ToString();
            dr["Reason_For_Change"] = txtSupplierDescReason.Text.ToString();
            dr["Approver"] = ddlGenApprover.SelectedValue.ToString();
            dr["Final_Approver"] = ddlGenFinalApprover.SelectedValue.ToString();
            dtcr.Rows.Add(dr);
        }
        if (chkPaymentEmail.Checked == true)
        {
            DataRow dr = dtcr.NewRow();
            dr["Field_Name"] = hdnPaymentEmail.Value.ToString();
            dr["Old_Value"] = lblPaymentEmail1.Text.ToString();
            dr["New_Value"] = txtPaymentEmail.Text.ToString();
            dr["Reason_For_Change"] = txtPaymentEmailReason.Text.ToString();
            dr["Approver"] = ddlAccApprover.SelectedValue.ToString();
            dr["Final_Approver"] = ddlAccFinalApprover.SelectedValue.ToString();
            dtcr.Rows.Add(dr);
        }
        if (chkCurrency.Checked == true)
        {
            DataRow dr = dtcr.NewRow();
            dr["Field_Name"] = hdnCurrency.Value.ToString();
            dr["Old_Value"] = lblCurrency1.Text.ToString();
            dr["New_Value"] = ddlCurrency.SelectedValue.ToString();
            dr["Reason_For_Change"] = txtCurrencyReason.Text.ToString();

            dr["Approver"] = ddlAdminApprover.SelectedValue.ToString();
            dr["Final_Approver"] = ddlAdminFinalApprover.SelectedValue.ToString();
            dtcr.Rows.Add(dr);
        }
        if (chkTerms.Checked == true)
        {
            DataRow dr = dtcr.NewRow();
            dr["Field_Name"] = hdnTerms.Value.ToString();
            dr["Old_Value"] = lblTerms1.Text.ToString();
            dr["New_Value"] = txtTerms.Text.ToString();
            dr["Reason_For_Change"] = txtTermsReason.Text.ToString();
            dr["Approver"] = ddlAccApprover.SelectedValue.ToString();
            dr["Final_Approver"] = ddlAccFinalApprover.SelectedValue.ToString();
            dtcr.Rows.Add(dr);
        }
        if (chkTaxRate.Checked == true)
        {
            DataRow dr = dtcr.NewRow();
            dr["Field_Name"] = hdnTaxRate.Value.ToString();
            dr["Old_Value"] = lblTaxRate1.Text.ToString();
            dr["New_Value"] = txtTaxRate.Text.ToString();
            dr["Reason_For_Change"] = txtTaxRateReason.Text.ToString();
            dr["Approver"] = ddlGenApprover.SelectedValue.ToString();
            dr["Final_Approver"] = ddlGenFinalApprover.SelectedValue.ToString();
            dtcr.Rows.Add(dr);
        }
        if (chkOwnership.Checked == true)
        {
            DataRow dr = dtcr.NewRow();
            dr["Field_Name"] = hdnownerShip.Value.ToString();
            dr["Old_Value"] = lblOwnerShip1.Text.ToString();
            dr["New_Value"] = ddlownerShip.SelectedValue.ToString();
            dr["Reason_For_Change"] = txtownerShipReason.Text.ToString();

            dr["Approver"] = ddlAdminApprover.SelectedValue.ToString();
            dr["Final_Approver"] = ddlAdminFinalApprover.SelectedValue.ToString();
            dtcr.Rows.Add(dr);
        }
        if (chkBiz.Checked == true)
        {
            DataRow dr = dtcr.NewRow();
            dr["Field_Name"] = hdnBiz.Value.ToString();
            dr["Old_Value"] = lblBiz1.Text.ToString();
            dr["New_Value"] = txtbiz.Text.ToString();
            dr["Reason_For_Change"] = txtbizReason.Text.ToString();
            dr["Approver"] = ddlGenApprover.SelectedValue.ToString();
            dr["Final_Approver"] = ddlGenFinalApprover.SelectedValue.ToString();
            dtcr.Rows.Add(dr);
        }
        if (chkPaymentInterval.Checked == true)
        {
            DataRow dr = dtcr.NewRow();
            dr["Field_Name"] = hdnPaymentInterval.Value.ToString();
            dr["Old_Value"] = lblPaymenyInterval1.Text.ToString();
            dr["New_Value"] = ddlPaymentInterval.SelectedValue.ToString();
            dr["Reason_For_Change"] = txtPaymentIntervalReason.Text.ToString();
            dr["Approver"] = ddlAccApprover.SelectedValue.ToString();
            dr["Final_Approver"] = ddlAccFinalApprover.SelectedValue.ToString();
            dtcr.Rows.Add(dr);
        }
        if (chkPaymentTerms.Checked == true)
        {
            DataRow dr = dtcr.NewRow();
            dr["Field_Name"] = hdnPaymentTerms.Value.ToString();
            dr["Old_Value"] = lblPaymentTerms1.Text.ToString();
            dr["New_Value"] = ddlPaymentTerms.SelectedValue.ToString();
            dr["Reason_For_Change"] = txtPaymentTermsReason.Text.ToString();
            dr["Approver"] = ddlAccApprover.SelectedValue.ToString();
            dr["Final_Approver"] = ddlAccFinalApprover.SelectedValue.ToString();
            dtcr.Rows.Add(dr);
        }
        if (chkInvoiceStatus.Checked == true)
        {
            DataRow dr = dtcr.NewRow();
            dr["Field_Name"] = hdnInvoiceStatus.Value.ToString();
            dr["Old_Value"] = lblInvoiceStatus1.Text.ToString();
            dr["New_Value"] = rdbInvoiceStatus.SelectedValue;
            dr["Reason_For_Change"] = txtInvoiceStatusReason.Text.ToString();
            dr["Approver"] = ddlAdminApprover.SelectedValue.ToString();
            dr["Final_Approver"] = ddlAdminFinalApprover.SelectedValue.ToString();
            dtcr.Rows.Add(dr);
        }
        if (chkDirectInvoice.Checked == true)
        {
            DataRow dr = dtcr.NewRow();
            dr["Field_Name"] = hdndirectinvoice.Value.ToString();
            dr["Old_Value"] = lbldirectinvoice1.Text.ToString();
            dr["New_Value"] = rdbdirectinvoice.SelectedValue;
            dr["Reason_For_Change"] = txtdirectinvoiceReason.Text.ToString();
            dr["Approver"] = ddlAdminApprover.SelectedValue.ToString();
            dr["Final_Approver"] = ddlAdminFinalApprover.SelectedValue.ToString();
            dtcr.Rows.Add(dr);
        }
        if (chkPaymentPriority.Checked == true)
        {
            DataRow dr = dtcr.NewRow();
            dr["Field_Name"] = hdnPaymentPriority.Value.ToString();
            dr["Old_Value"] = lblPaymenypriority1.Text.ToString();
            dr["New_Value"] = rdbPaymentPriority.SelectedValue;
            dr["Reason_For_Change"] = txtPaymentHistoryReason.Text.ToString();
            dr["Approver"] = ddlAccApprover.SelectedValue.ToString();
            dr["Final_Approver"] = ddlAccFinalApprover.SelectedValue.ToString();
            dtcr.Rows.Add(dr);
        }
        if (chkAutoSendPO.Checked == true)
        {
            DataRow dr = dtcr.NewRow();
            dr["Field_Name"] = hdnAutoSendPO.Value.ToString();
            dr["Old_Value"] = lblAutoSendPO1.Text.ToString();
            dr["New_Value"] = rdbAutoSendPO.SelectedValue;
            dr["Reason_For_Change"] = txtAutoSendPO.Text.ToString();
            dr["Approver"] = ddlAccApprover.SelectedValue.ToString();
            dr["Final_Approver"] = ddlAccFinalApprover.SelectedValue.ToString();
            dtcr.Rows.Add(dr);
        }
        if (chkPaymentHistory.Checked == true)
        {
            DataRow dr = dtcr.NewRow();
            dr["Field_Name"] = hdnPaymentHistory.Value.ToString();
            dr["Old_Value"] = lblPaymentHistory1.Text.ToString();
            dr["New_Value"] = rdbPaymentHistory.SelectedValue;
            dr["Reason_For_Change"] = txtPaymentPriorityReason.Text.ToString();
            dr["Approver"] = ddlAccApprover.SelectedValue.ToString();
            dr["Final_Approver"] = ddlAccFinalApprover.SelectedValue.ToString();
            dtcr.Rows.Add(dr);
        }
        if (chkScope1.Checked == true)
        {
            DataRow dr = dtcr.NewRow();
            dr["Field_Name"] = hdnScope.Value.ToString();
            dr["Old_Value"] = lblScope1.Text.ToString();
            dr["New_Value"] = null;// rdbPaymentPriority.SelectedValue;
            dr["Reason_For_Change"] = txtScopeReason.Text.ToString();
            dr["Approver"] = ddlGenApprover.SelectedValue.ToString();
            dr["Final_Approver"] = ddlGenFinalApprover.SelectedValue.ToString();
            dtcr.Rows.Add(dr);
        }
        if (chkPort1.Checked == true)
        {
            DataRow dr = dtcr.NewRow();
            dr["Field_Name"] = hdnPort.Value.ToString();
            dr["Old_Value"] = lblPort1.Text.ToString();
            dr["New_Value"] = null;//rdbPaymentPriority.SelectedValue;
            dr["Reason_For_Change"] = txtPortReason.Text.ToString();
            dr["Approver"] = ddlGenApprover.SelectedValue.ToString();
            dr["Final_Approver"] = ddlGenFinalApprover.SelectedValue.ToString();
            dtcr.Rows.Add(dr);
        }
        int RetValue = 0;
        if (dtcr.Rows.Count > 0)
        {
            RetValue = BLL_ASL_Supplier.Supplier_ChangeRequest_Data_Insert(UDFLib.ConvertIntegerToNull(hdnCRID.Value), UDFLib.ConvertStringToNull(SupplierID), dtcr, CR_Status, Action_On_Data_Form, UDFLib.ConvertToInteger(GetSessionUserID()));
            hdnCRID.Value = Convert.ToString(RetValue);
            //if (chkScope1.Checked == true && Action_On_Data_Form == "APPROVED")
            //{
            //    SaveSupplierScope_CR(CR_Status);
            //}
            //if (chkPort1.Checked == true && Action_On_Data_Form == "APPROVED")
            //{
            //    SaveSupplierPort_CR(CR_Status);
            //}
            if (chkScope1.Checked == true && Action_On_Data_Form == "EFFECTED")
            {
                SaveSupplierScope(UDFLib.ConvertIntegerToNull(hdnCRID.Value));
            }
            if (chkPort1.Checked == true && Action_On_Data_Form == "EFFECTED")
            {
                SaveSupplierPort(UDFLib.ConvertIntegerToNull(hdnCRID.Value));
            }
            return RetValue;
        }
        {
            return RetValue;
        }
    }

    protected void btnFinalApprove_Click(object sender, EventArgs e)
    {
        try
        {
            string CR_Status = "EFFECTED";
            string Action_On_Data_Form = "EFFECTED";
            int RetValue = Submit(CR_Status, Action_On_Data_Form);
            //SaveSupplierScope();
            //SaveSupplierPort();
            if (RetValue == 0)
            {
                string message = "alert('No Rows Selected For Change.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            }
            else
            {
                BLL_ASL_Supplier.ASL_Send_CR_mail(CR_Status, UDFLib.ConvertToInteger(GetSessionUserID()), UDFLib.ConvertStringToNull(GetSuppID()), UDFLib.ConvertIntegerToNull(hdnCRID.Value));
                string message = "alert('Change request Final Approved and Effected.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                string msgDraft = String.Format("parent.ReloadParent_ByButtonID();");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgDraft", msgDraft, true);
            }
            //Response.Redirect("../ASL/ASL_CR.aspx?Supp_ID=" + UDFLib.ConvertStringToNull(Request.QueryString["Supp_ID"].ToString()) + "");
            //BindChangeRequest();
        }
        catch { }
        {
        }
    }

    protected void SaveSupplierScope(int? CRID)
    {
        string SupplierID = GetSuppID();
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
        BLL_ASL_Supplier.ASL_Supplier_Scope_Insert(UDFLib.ConvertStringToNull(SupplierID), dtScope, UDFLib.ConvertToInteger(GetSessionUserID()));
    }

    protected void SaveSupplierPort(int? CRID)
    {
        string SupplierID = GetSuppID();
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
        BLL_ASL_Supplier.ASL_Supplier_Port_Insert(UDFLib.ConvertStringToNull(SupplierID), dtService, UDFLib.ConvertToInteger(GetSessionUserID()));
    }
    #endregion
}
  