using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.INFRA;
using SMS.Business.POLOG;


public partial class PO_LOG_Batch_Payment_Setup_Entry : System.Web.UI.Page
{
    BLL_Infra_Approval_Group_Department objBllGroup = new BLL_Infra_Approval_Group_Department();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindDropDownlist();
            if (!String.IsNullOrEmpty(Request.QueryString["Payment_ID"].ToString()))
            {
                BindPaymentDeatils(UDFLib.ConvertStringToNull(Request.QueryString["Payment_ID"].ToString()));
            }
        }
    }
    protected void BindDropDownlist()
    {
        try
        {
            DataSet ds = BLL_POLOG_Register.POLOG_Get_Type(UDFLib.ConvertToInteger(Session["UserID"].ToString()), "PO_TYPE");
            if (ds.Tables[17].Rows.Count > 0)
            {
                ddlSupplierName.DataSource = ds.Tables[17];
                ddlSupplierName.DataTextField = "Supplier_Name";
                ddlSupplierName.DataValueField = "Supplier_Code";
                ddlSupplierName.DataBind();
                ddlSupplierName.Items.Insert(0, new ListItem("-Select-", "0"));
            }
            if (ds.Tables[18].Rows.Count > 0)
            {
                ddlCountry.DataSource = ds.Tables[18];
                ddlCountry.DataTextField = "Country";
                ddlCountry.DataValueField = "Country_Code";
                ddlCountry.DataBind();
                ddlCountry.Items.Insert(0, new ListItem("-Select-", "0"));
            }
            if (ds.Tables[19].Rows.Count > 0)
            {
                ddlState.DataSource = ds.Tables[19];
                ddlState.DataTextField = "Variable_Name";
                ddlState.DataValueField = "Variable_Code";
                ddlState.DataBind();
                ddlState.Items.Insert(0, new ListItem("-Select-", "0"));
            }
        }
        catch { }
        {
        }
    }
    void BindPaymentDeatils(string Payment_ID)
    {
        try
        {
            //Change And Enter One Type
            int Type = 1;
            DataSet ds = BLL_POLOG_Register.POLOG_Get_Batch_Payment_Setup(Payment_ID);

            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlPaymentType.SelectedValue = ds.Tables[0].Rows[0]["Account_Type_Code"].ToString();
                txtBankName.Text = ds.Tables[0].Rows[0]["Receiving_Bank_Name"].ToString();
                ddlSupplierName.SelectedValue = ds.Tables[0].Rows[0]["Supplier_Code"].ToString();
                ddlPaymentCurrency.SelectedValue = ds.Tables[0].Rows[0]["Payment_Currency"].ToString();
                ddlCountry.SelectedValue = ds.Tables[0].Rows[0]["Receiving_Bank_Country_Code"].ToString();
                ddlState.SelectedValue = ds.Tables[0].Rows[0]["Bank_State_Code"].ToString();
                txtSwiftCode.Text = ds.Tables[0].Rows[0]["SWIFT_IBAN_Code"].ToString();
                txtABANumber.Text = ds.Tables[0].Rows[0]["Destination_ABA"].ToString();
                txtBankCode.Text = ds.Tables[0].Rows[0]["Receiving_Bank_Code"].ToString();
                txtBranchCode.Text = ds.Tables[0].Rows[0]["Receiving_Branch_Code"].ToString();
                txtAccountNumber.Text = ds.Tables[0].Rows[0]["Receiving_Account_Number"].ToString();
                txtBeneficiary.Text = ds.Tables[0].Rows[0]["Receiving_Beneficiary_Name"].ToString();
                ddlPaymentAccount.SelectedValue = ds.Tables[0].Rows[0]["Payment_Account_ID"].ToString();
                ddlPayMode.SelectedValue = ds.Tables[0].Rows[0]["Payment_Mode"].ToString();
                txtPaymentID.Text = ds.Tables[0].Rows[0]["Payment_Mode_ID"].ToString();
            }
            //BindDepartmentList(txtGroupID.Text, ddlPOType.SelectedValue);
        }
        catch { }
        {
        }
    }

   
    protected void btnDraft_Click(object sender, EventArgs e)
    {
        string Status = "DRAFT";
        Save(Status);
    }

    protected void Save(string Status)
    {
        try
        {
            string responseid = BLL_POLOG_Register.POLOG_INS_Batch_Payment(UDFLib.ConvertStringToNull(txtPaymentID.Text), ddlSupplierName.SelectedValue, ddlPaymentType.SelectedValue, ddlPaymentCurrency.SelectedValue,
                                                      txtBankName.Text, ddlCountry.SelectedValue, ddlState.SelectedValue, txtSwiftCode.Text.Trim(), txtABANumber.Text.Trim(), txtBankCode.Text.Trim(), txtBranchCode.Text.Trim(), txtAccountNumber.Text.Trim(),
                                                      txtBeneficiary.Text.Trim(), ddlPaymentAccount.SelectedValue, ddlPayMode.SelectedValue, UDFLib.ConvertIntegerToNull(Session["USERID"]), "DRAFT");
            txtPaymentID.Text = Convert.ToString(responseid);
            if (responseid != "0")
            {
                string msgDraft = String.Format("parent.ReloadParent_ByButtonID();");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgDraft", msgDraft, true);
            }
            else
            {

            }
        }
        catch { }
        {
        }
    }


    protected void btLock_Click(object sender, EventArgs e)
    {
        string Status = "LOCKED";
        Save(Status);
    }
    protected void btnUnLock_Click(object sender, EventArgs e)
    {
        string Status = "UNLOCKED";
        Save(Status);
    }
}