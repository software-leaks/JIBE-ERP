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


public partial class PO_LOG_PO_Log_Admin : System.Web.UI.Page
{
    BLL_Infra_UserCredentials objBLLUser = new BLL_Infra_UserCredentials();
    BLL_Infra_VesselLib objBLL = new BLL_Infra_VesselLib();
    BLL_Infra_Currency objBLLCurrency = new BLL_Infra_Currency();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["Supply_ID"] != null)
            {
               txtSupplyID.Text = Request.QueryString["Supply_ID"].ToString();
                //txtSupplyID.Text = "19755";
                BindDropDownList();
                Load_VesselList();
                BindCurrencyDLL();
                BindPODetails();
            }
        }
    }
    protected void BindPODetails()
    {
        try
        {
            DataSet ds = BLL_POLOG_Register.POLOG_Get_PO_Deatils(UDFLib.ConvertIntegerToNull(txtSupplyID.Text.ToString()), null, UDFLib.ConvertIntegerToNull(GetSessionUserID()));

            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                txtSupplyID.Text = dr["SUPPLY_ID"].ToString();
                ddlPOType.SelectedValue = dr["Req_Type"].ToString();
                BindAccountClassification(ddlPOType.SelectedValue);
                txtRemarks.Text = dr["Req_Description"].ToString();

                if (ddlVessel.Items.FindByValue(dr["Vessel_ID"].ToString()) != null)
                {
                    ddlVessel.SelectedValue = dr["Vessel_ID"].ToString();
                }
                if (ddlPOType.SelectedValue == "CPY")
                {
                    BindCharterParty(UDFLib.ConvertIntegerToNull(ddlVessel.SelectedValue));
                }
                ddlCurrency.SelectedValue = dr["Line_Currency"].ToString();
                ddlAccountType.SelectedValue = dr["Account_Type"].ToString();
                ddlAccClassifictaion.SelectedValue = dr["Account_Classification"].ToString();
                if (ddlSupplier.Items.FindByValue(dr["Supplier_Code"].ToString()) == null)
                {
                    ddlSupplier.Items.RemoveAt(0);
                    ddlSupplier.Items.Insert(0, new ListItem("-Invalid Supplier-", "0"));
                }
                else
                {
                    ddlSupplier.SelectedValue = dr["Supplier_Code"].ToString();
                }
                ddlOwnerCode.SelectedValue = dr["Owner_Code"].ToString();
                ddlCharterParty.SelectedValue = dr["Charter_ID"].ToString();
                ddlTerms.SelectedValue = dr["Payment_Terms_Days"].ToString();
                rdbPayment.SelectedValue = dr["Payment_Priority"].ToString();
                ddlIssueBy.SelectedValue = dr["Issued_By_Company"].ToString();
                if (!string.IsNullOrEmpty(dr["Closed_By"].ToString()) && !string.IsNullOrEmpty(dr["PO_closed_Date"].ToString()))
                {
                    btnClose.Enabled = false;
                    btnUnClose.Enabled = true;
                }
                else
                {
                    btnClose.Enabled = true;
                    btnUnClose.Enabled = false;
                }

            }
        }
        catch { }
        {
        }
    }
    private void BindAccountClassification(string PO_Type)
    {
        DataSet ds = BLL_POLOG_Register.POLOG_Get_AccountClassification(UDFLib.ConvertIntegerToNull(GetSessionUserID()), PO_Type);


        ddlAccClassifictaion.DataSource = ds.Tables[0];
        ddlAccClassifictaion.DataTextField = "VARIABLE_NAME";
        ddlAccClassifictaion.DataValueField = "VARIABLE_CODE";
        ddlAccClassifictaion.DataBind();
        ddlAccClassifictaion.Items.Insert(0, new ListItem("-Select-", "0"));
    }
    private int GetCompanyID()
    {
        if (Session["USERCOMPANYID"] != null)
            return int.Parse(Session["USERCOMPANYID"].ToString());
        else
            return 0;
    }
    public void Load_VesselList()
    {
        DataTable dt = objBLL.Get_VesselList(0, 0, 0, "", Convert.ToInt32(GetCompanyID()));


        ddlVessel.DataSource = dt;
        ddlVessel.DataTextField = "VESSEL_NAME";
        ddlVessel.DataValueField = "VESSEL_ID";
        ddlVessel.DataBind();
        ddlVessel.Items.Insert(0, new ListItem("-Select-", "0"));
    }
    protected void BindCharterParty(int? Vessel_ID)
    {
        DataSet ds = BLL_POLOG_Register.POLOG_Get_CharterParty(Vessel_ID);

        ddlCharterParty.DataSource = ds.Tables[0];
        ddlCharterParty.DataTextField = "Charter_Name";
        ddlCharterParty.DataValueField = "CharterID";
        ddlCharterParty.DataBind();
        ddlCharterParty.Items.Insert(0, new ListItem("-Select-", "0"));
    }
    public string GetPOType()
    {
        try
        {
            if (Request.QueryString["POType"] != null)
            {
                return Request.QueryString["POType"].ToString();
            }

            else
                return "";
        }
        catch { return ""; }
    }
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }
    private string GetSessionUserName()
    {
        if (Session["USERNAME"] != null)
            return Session["USERNAME"].ToString();
        else
            return "0";
    }
    protected void BindDropDownList()
    {
        DataSet ds = BLL_POLOG_Register.POLOG_Get_Type(UDFLib.ConvertToInteger(GetSessionUserID()),"PO_TYPE");

        if (ds.Tables[14].Rows.Count > 0)
        {
            ddlPOType.DataSource = ds.Tables[14];
            ddlPOType.DataTextField = "VARIABLE_NAME";
            ddlPOType.DataValueField = "VARIABLE_CODE";
            ddlPOType.DataBind();
        }
        else
        {
            ddlPOType.Items.Insert(0, new ListItem("-Select-", "0"));
        }
        ddlAccClassifictaion.DataSource = ds.Tables[3];
        ddlAccClassifictaion.DataTextField = "VARIABLE_NAME";
        ddlAccClassifictaion.DataValueField = "VARIABLE_CODE";
        ddlAccClassifictaion.DataBind();
        ddlAccClassifictaion.Items.Insert(0, new ListItem("-Select-", "00000"));


        ddlSupplier.DataSource = ds.Tables[5];
        ddlSupplier.DataTextField = "Supplier_Name";
        ddlSupplier.DataValueField = "Supplier_Code";
        ddlSupplier.DataBind();
        ddlSupplier.Items.Insert(0, new ListItem("-Select-", "00000"));

        ddlAccountType.DataSource = ds.Tables[2];
        ddlAccountType.DataTextField = "VARIABLE_NAME";
        ddlAccountType.DataValueField = "VARIABLE_CODE";
        ddlAccountType.DataBind();
        ddlAccountType.Items.Insert(0, new ListItem("-Select-", "0"));


        ddlOwnerCode.DataSource = ds.Tables[7];
        ddlOwnerCode.DataTextField = "Supplier_Name";
        ddlOwnerCode.DataValueField = "Supplier_Code";
        ddlOwnerCode.DataBind();
        ddlOwnerCode.Items.Insert(0, new ListItem("-Select-", "0"));

        ddlTerms.DataSource = ds.Tables[15];
        ddlTerms.DataTextField = "Name";
        ddlTerms.DataValueField = "Value";
        ddlTerms.DataBind();
        ddlTerms.Items.Insert(0, new ListItem("-Select-", "0"));

        ddlIssueBy.DataSource = ds.Tables[16];
        ddlIssueBy.DataTextField = "Supplier_Name";
        ddlIssueBy.DataValueField = "Supplier_Code";
        ddlIssueBy.DataBind();
        ddlIssueBy.Items.Insert(0, new ListItem("-Select-", "0"));

    }
    protected void BindCurrencyDLL()
    {
        DataTable dt = objBLLCurrency.Get_CurrencyList();
        ddlCurrency.DataSource = dt;
        ddlCurrency.DataTextField = "Currency_Code";
        ddlCurrency.DataValueField = "Currency_Code";
        ddlCurrency.DataBind();
        ddlCurrency.Items.Insert(0, new ListItem("-Select-", "0"));

    }
    protected void ddlVessel_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindCharterParty(UDFLib.ConvertIntegerToNull(ddlVessel.SelectedValue));
    }
    protected void ddlPOType_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet ds = BLL_POLOG_Register.POLOG_Get_AccountClassification(UDFLib.ConvertIntegerToNull(GetSessionUserID()), ddlPOType.SelectedValue);


        ddlAccClassifictaion.DataSource = ds.Tables[0];
        ddlAccClassifictaion.DataTextField = "VARIABLE_NAME";
        ddlAccClassifictaion.DataValueField = "VARIABLE_CODE";
        ddlAccClassifictaion.DataBind();
        ddlAccClassifictaion.Items.Insert(0, new ListItem("-Select-", "0"));
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
           
            string Hide = "No";
            if(chkHide.Checked == true){Hide = "Yes";}
            else{Hide = "No";}
            string Action_On_Data_Form = null;
            string Status = null;
            int Retval = BLL_POLOG_Register.POLOG_Update_PO_Admin(UDFLib.ConvertIntegerToNull(txtSupplyID.Text.ToString()), UDFLib.ConvertIntegerToNull(ddlVessel.SelectedValue), UDFLib.ConvertStringToNull(ddlSupplier.SelectedValue),
                UDFLib.ConvertIntegerToNull(ddlTerms.SelectedValue), UDFLib.ConvertStringToNull(ddlCurrency.SelectedValue), UDFLib.ConvertStringToNull(ddlIssueBy.SelectedValue),
                UDFLib.ConvertStringToNull(ddlCharterParty.SelectedValue), UDFLib.ConvertStringToNull(ddlAccClassifictaion.SelectedValue), UDFLib.ConvertStringToNull(ddlAccountType.SelectedValue), UDFLib.ConvertStringToNull(ddlPOType.SelectedValue),
                UDFLib.ConvertStringToNull(ddlOwnerCode.SelectedValue), UDFLib.ConvertStringToNull(rdbPayment.SelectedValue), Hide, txtRemarks.Text.ToString(), Action_On_Data_Form, Status, UDFLib.ConvertIntegerToNull(Session["USERID"].ToString()));
            string script = "alert(\"PO Log details have been successfully updated\");";
            ScriptManager.RegisterStartupScript(this, GetType(),
                                  "ServerControlScript", script, true);
        }
        catch { }
        {
        }
    }
    protected void btnAdminDelete_Click(object sender, EventArgs e)
    {
        try
        {
            string POStatus = "DELETED";
            int retval = BLL_POLOG_Register.POLOG_Delete_PO(UDFLib.ConvertIntegerToNull(txtSupplyID.Text.ToString()), POStatus, UDFLib.ConvertIntegerToNull(Session["USERID"].ToString()));
            InsertAuditTrail("Delete PO", "DeletePO");
        }
        catch { }
        {
        }
   
    }
    protected void btnCalculate_Click(object sender, EventArgs e)
    {
        try
        {
            int retval = BLL_POLOG_Register.POLOG_Calculate_Outstanding(UDFLib.ConvertIntegerToNull(txtSupplyID.Text.ToString()), UDFLib.ConvertIntegerToNull(Session["USERID"].ToString()));
        }
        catch { }
        {
        }
    }
    protected void btnUnClose_Click(object sender, EventArgs e)
    {
        try
        {
            string Type = "UnClose";
           
            int retval = BLL_POLOG_Register.POLOG_Update_PODeatils(UDFLib.ConvertIntegerToNull(txtSupplyID.Text.ToString()), Type, txtCloseDate.Text, UDFLib.ConvertIntegerToNull(Session["USERID"].ToString()));
            btnClose.Enabled = true;
            btnUnClose.Enabled = false;
            txtCloseDate.Text = null;
            InsertAuditTrail("UnClose PO", "UNClosePO");
            BindPODetails();
        }
        catch { }
        {
        }
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
         try
        {
            string Type = "Close";
            
            int retval = BLL_POLOG_Register.POLOG_Update_PODeatils(UDFLib.ConvertIntegerToNull(txtSupplyID.Text.ToString()), Type, txtCloseDate.Text,  UDFLib.ConvertIntegerToNull(Session["USERID"].ToString()));

            InsertAuditTrail("Close PO", "ClosePO");
            BindPODetails(); btnUnClose.Enabled = true;
            btnClose.Enabled = false;
           
        }
         catch { }
         {
         }
    }
    protected void InsertAuditTrail(string Action, string Description)
    {
        try
        {
            int RetAuditVal = BLL_POLOG_Register.POLOG_Insert_Transactionlog(UDFLib.ConvertStringToNull(txtSupplyID.Text), Action, Description, UDFLib.ConvertToInteger(GetSessionUserID()));
        }
        catch { }
        {

        }
    }
   
    protected void btnExit_Click(object sender, EventArgs e)
    {
        this.ClientScript.RegisterClientScriptBlock(this.GetType(), "Close", "window.close()", true);
    }
}