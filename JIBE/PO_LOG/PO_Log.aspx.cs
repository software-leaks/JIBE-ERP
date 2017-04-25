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

public partial class PO_LOG_PO_Log : System.Web.UI.Page
{
    BLL_Infra_UserCredentials objBLLUser = new BLL_Infra_UserCredentials();
    BLL_Infra_VesselLib objBLL = new BLL_Infra_VesselLib();
    BLL_Infra_Currency objBLLCurrency = new BLL_Infra_Currency();
    BLL_Infra_Country objBLLCountry = new BLL_Infra_Country();
    UserAccess objUA = new UserAccess();
    public string OperationMode = "";
    public Boolean ApprovalFlag = false;
    decimal total_Qty = 0;
    decimal total_Price = 0;
    decimal total_Discount = 0;
    string clientID;
    protected DataTable dtGridItems;
    protected void Page_Load(object sender, EventArgs e)
    {
        //UserAccessValidation();
        if (!IsPostBack)
        {

            txtSupplyID.Text = "00000";
            txtPOType.Text = "";
            txtSupplyID.Text = GetSUPPLY_ID();
            txtPOType.Text = GetPOType();
            BindDropDownList();
            Load_VesselList();
            BindCurrencyDLL();
            BindPODetails();
        }

    }
    //Fill Account Classification According PO Type
    private void BindAccountClassification(string PO_Type)
    {
        DataSet ds = BLL_POLOG_Register.POLOG_Get_AccountClassification(UDFLib.ConvertIntegerToNull(GetSessionUserID()), PO_Type);


        ddlAccClassifictaion.DataSource = ds.Tables[0];
        ddlAccClassifictaion.DataTextField = "VARIABLE_NAME";
        ddlAccClassifictaion.DataValueField = "VARIABLE_CODE";
        ddlAccClassifictaion.DataBind();
        ddlAccClassifictaion.Items.Insert(0, new ListItem("-Select-", "0"));
    }
    //Check and Get Company ID from Session 
    private int GetCompanyID()
    {
        if (Session["USERCOMPANYID"] != null)
            return int.Parse(Session["USERCOMPANYID"].ToString());
        else
            return 0;
    }
    //Check and Get Company ID from Session 
    public void Load_VesselList()
    {
        DataTable dt = objBLL.Get_VesselList(0, 0, 0, "", Convert.ToInt32(GetCompanyID()));


        ddlVessel.DataSource = dt;
        ddlVessel.DataTextField = "VESSEL_NAME";
        ddlVessel.DataValueField = "VESSEL_ID";
        ddlVessel.DataBind();
        ddlVessel.Items.Insert(0, new ListItem("-Select-", "0"));
    }
    //Check User Access Right
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
    //Fill All DropDown List Like PO Type,Supplier,Account Type,Agent,Owner Code,Discount Type
    protected void BindDropDownList()
    {
        DataSet ds = BLL_POLOG_Register.POLOG_Get_Type(UDFLib.ConvertToInteger(GetSessionUserID()),"PO_Type");

        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            if (ds.Tables[0].Rows[i]["VARIABLE_CODE"].ToString() == txtPOType.Text)
            {
                lblPoType.Text = ds.Tables[0].Rows[i]["VARIABLE_NAME"].ToString();
            }
        }

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


        ddlAgent.DataSource = ds.Tables[6];
        ddlAgent.DataTextField = "Supplier_Name";
        ddlAgent.DataValueField = "Supplier_Code";
        ddlAgent.DataBind();
        ddlAgent.Items.Insert(0, new ListItem("-Select-", "0"));

        ddlOwnerCode.DataSource = ds.Tables[7];
        ddlOwnerCode.DataTextField = "Supplier_Name";
        ddlOwnerCode.DataValueField = "Supplier_Code";
        ddlOwnerCode.DataBind();
        ddlOwnerCode.Items.Insert(0, new ListItem("-Select-", "0"));


        ddlDiscountType.DataSource = ds.Tables[9];
        ddlDiscountType.DataTextField = "VARIABLE_NAME";
        ddlDiscountType.DataValueField = "VARIABLE_CODE";
        ddlDiscountType.DataBind();
        //ddlDiscountType.Items.Insert(0, new ListItem("-Select-", "0"));

    }
    //Fill Currency
    protected void BindCurrencyDLL()
    {
        DataTable dt = objBLLCurrency.Get_CurrencyList();
        ddlCurrency.DataSource = dt;
        ddlCurrency.DataTextField = "Currency_Code";
        ddlCurrency.DataValueField = "Currency_Code";
        ddlCurrency.DataBind();
        ddlCurrency.Items.Insert(0, new ListItem("-Select-", "0"));

    }
    //Check and Get UserID from Session 
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
    //Check and Get PO Type from Request Query String 
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
    //Check and Get PO Code from Request Query String 
    public string GetSUPPLY_ID()
    {
        try
        {

            if (Request.QueryString["SUPPLY_ID"] != null)
            {
                return Request.QueryString["SUPPLY_ID"].ToString();
            }

            else
                return "0";
        }
        catch { return "0"; }
    }
    //Fill Charter Party
    protected void BindCharterParty(int? Vessel_ID)
    {
        DataSet ds = BLL_POLOG_Register.POLOG_Get_CharterParty(Vessel_ID);

        ddlCharterParty.DataSource = ds.Tables[0];
        ddlCharterParty.DataTextField = "Charter_Name";
        ddlCharterParty.DataValueField = "CharterID";
        ddlCharterParty.DataBind();
        ddlCharterParty.Items.Insert(0, new ListItem("-Select-", "0"));
    }
    //Fill Port Call
    protected void BindPortCall(int? Vessel_ID)
    {
        int? Supply_ID = 0;
        DataTable dt = BLL_POLOG_Register.POLOG_Get_Port_Call(null, Supply_ID, Vessel_ID, 0);

        ddlPortCall.DataSource = dt;
        ddlPortCall.DataTextField = "PORT_NAME";
        ddlPortCall.DataValueField = "Port_Call_ID_Ref";
        ddlPortCall.DataBind();
        ddlPortCall.Items.Insert(0, new ListItem("-Select-", "0"));
    }
    //Get PO Details by passing PO Code 
    protected void BindPODetails()
    {
        try
        {

            DataSet ds = BLL_POLOG_Register.POLOG_Get_PO_Deatils(UDFLib.ConvertIntegerToNull(txtSupplyID.Text.ToString()), UDFLib.ConvertStringToNull(txtPOType.Text.ToString()), UDFLib.ConvertIntegerToNull(GetSessionUserID()));

            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                txtPOCode.Text = dr["REQ_ID"].ToString();
                lblPoType.Text = dr["VARIABLE_NAME"].ToString();
                txtSupplyID.Text = dr["SUPPLY_ID"].ToString();
                txtPOType.Text = dr["Req_Type"].ToString();
                BindAccountClassification(txtPOType.Text);

                txtReferance.Text = dr["Office_Ref_Code"].ToString();
                txtShipReferance.Text = dr["Ship_Ref_Code"].ToString();
                txtPort.Text = dr["Req_Port"].ToString();
                txtETA.Text = dr["VESSEL_ETA_ETD"].ToString();
                txtUrgency.Text = dr["URGENCY"].ToString();
                txtSuppRef.Text = dr["Supplier_Ref_Code"].ToString();
                txtRemarks.Text = dr["Req_Description"].ToString();

                if (ddlVessel.Items.FindByValue(dr["Vessel_ID"].ToString()) != null)
                {
                    ddlVessel.SelectedValue = dr["Vessel_ID"].ToString();
                }
                if (txtPOType.Text == "CPY")
                {
                    trCharterParty.Visible = true;
                    BindCharterParty(UDFLib.ConvertIntegerToNull(ddlVessel.SelectedValue));
                }
                else if (txtPOType.Text == "APO")
                {
                    trPortCall.Visible = true;
                    BindPortCall(UDFLib.ConvertIntegerToNull(ddlVessel.SelectedValue));
                }
                else if (txtPOType.Text == "MGT")
                {
                    trOwner.Visible = true;
                    //BindCharterParty(UDFLib.ConvertIntegerToNull(ddlVessel.SelectedValue));
                }
                else
                {
                    trCharterParty.Visible = false;
                    trPortCall.Visible = false;
                    trOwner.Visible = false;
                }
                ddlCurrency.SelectedValue = dr["Line_Currency"].ToString();
                if (ddlPortCall.Items.FindByValue(dr["Connecting_Port_ID"].ToString()) != null)
                {
                    ddlPortCall.SelectedValue = dr["Connecting_Port_ID"].ToString();
                }
                if (ddlAgent.Items.FindByValue(dr["Agent_Code"].ToString()) != null)
                {
                    ddlAgent.SelectedValue = dr["Agent_Code"].ToString();
                }
                //ddlAgent.SelectedValue = dr["Agent_Code"].ToString();
                ddlAccountType.SelectedValue = dr["Account_Type"].ToString();
                ddlAccClassifictaion.SelectedValue = dr["Account_Classification"].ToString();
                if (ddlSupplier.Items.FindByValue(dr["Supplier_Code"].ToString()) == null)
                {
                    ddlSupplier.Items.RemoveAt(0);
                    ddlSupplier.Items.Insert(0, new ListItem("-Invalid Supplier-", "0"));
                    trSupplierMsg.Visible = true;
                }
                else
                {
                    trSupplierMsg.Visible = false;
                    ddlSupplier.SelectedValue = dr["Supplier_Code"].ToString();
                    if (dr["Line_Status"].ToString() == "APPROVED")
                    {
                        if (dr["Auto_Send_PO"].ToString() == "Yes")
                        {
                            trIssue.Visible = false;
                        }
                        else
                        {
                            trIssue.Visible = true;
                        }
                    }
                    else
                    {
                        trIssue.Visible = false;
                    }
                }
                if (dr["Supplier_Code"].ToString() == null || dr["Supplier_Code"].ToString() == "0")
                {
                    btnSupplierRemarks.Visible = false;
                }
                else
                {
                    if (ddlSupplier.SelectedIndex > 0)
                    {
                        btnSupplierRemarks.Visible = true;
                    }
                }
                ddlOwnerCode.SelectedValue = dr["Owner_Code"].ToString();
                ddlCharterParty.SelectedValue = dr["Charter_ID"].ToString();
                lblReport_USD_Value.Text = dr["Report_USD_Value"].ToString();
                lblPaypent.Text = dr["Payment_Priority"].ToString();
                lblPaymentTerms.Text = dr["PaymentTermsDays"].ToString();
                lblCreatedBY.Text = dr["Created_by"].ToString();
                lblcreated_by.Text = dr["Created_by"].ToString();
                lbldate.Text = dr["Created_Date"].ToString();
                lblPo_Used_Value.Text = dr["ExchangeAmount"].ToString();


                lblReport_USD_Value.Visible = true;
                lblReprtValue.Visible = true;
                lblPaymentTerms.Visible = true;
                lblPayment_Terms.Visible = true;
                //btnAddItem.Visible = true;
                btnAttachDocs.Visible = true;
                txtCurrChange.Text = dr["Exch_rate"].ToString();
                txtExchangeRate.Text = dr["Exch_rate"].ToString();
                lblPo_Used_Value.Text = dr["ExchangeAmount"].ToString();
                lblExchangeRate.Text = dr["ExchangeAmount"].ToString();
                lblCurrentCur.Text = dr["Line_Currency"].ToString();
                lblTotalUnit.Text = dr["Line_Currency"].ToString();
                lblDiscAmtUnit.Text = dr["Line_Currency"].ToString();
                lblTotalpricecur.Text = dr["Line_Currency"].ToString();
                //lblExchCur.Text = dr["Line_Currency"].ToString();
                string ColorCode = dr["COLOR_CODE"].ToString();
                System.Drawing.Color col = System.Drawing.ColorTranslator.FromHtml(ColorCode);
                PODiv.Attributes["background-color"] = ColorCode;
                PODiv.Style["background-color"] = ColorCode;
                //Payment Priroty
                if (dr["Payment_Priority"].ToString() != "")
                {
                    lblPaymentPriority.Visible = true;
                    lblPaypent.Visible = true;
                }
                btnTranscLog.Visible = true;
                BindItems();
                ControlVisible(false);
                EnableDisable(true);
                //Check When PO In Draft
                if (dr["Line_Status"].ToString() == "CANCELLED")
                {
                    EnableDisableControls(false);
                    EnableDisable(false);
                    btnSave.Enabled = false;
                }
                else
                {

                if (dr["Line_Status"].ToString() == "")
                {
                    Common();

                    btnVerify.Visible = false;
                    btnApprove.Visible = false;
                    btnUndoApproval.Visible = false;
                }
                else
                {
                    Common();
                    lblAction.Text = dr["APPROVER"].ToString();
                    lbllineDate.Text = dr["Line_Date"].ToString();
                    lblRequest.Text = dr["Total_Amt"].ToString();
                }
                //Check When PO In Approval
                if (dr["Line_Status"].ToString() == "FOR APPROVAL")
                {
                    EnableDisable(false);
                    btnSave.Enabled = false;
                    trWorkFlow.Visible = false;
                    trApprove.Visible = false;
                    lblApprovalmsg.Visible = false;
                    if (lblcreated_by.Text == GetSessionUserName())
                    {
                        btnRecall.Visible = true;
                        btnDelete.Visible = true;
                        btnClose.Visible = false;
                        btnRecall.Visible = true;
                    }

                    if (dr["For_Action_By"].ToString() != "" && (dr["For_Action_By"].ToString() == GetSessionUserName()))
                    {
                        btnVerify.Visible = true;
                        trSubmission.Visible = true;
                        trApprove.Visible = false;
                    }
                    if (dr["APPROVER"].ToString() != "" && (dr["APPROVER"].ToString() == GetSessionUserName()))
                    {
                        btnApprove.Visible = true;
                        trSubmission.Visible = true;
                        trApprove.Visible = false;
                        btnClose.Visible = true;
                    }

                }
                //Check When PO In Verified
                if (dr["Line_Status"].ToString() == "Verified")
                {
                    btnSave.Enabled = false;
                    trWorkFlow.Visible = false;
                    trApprove.Visible = false;
                    lblApprovalmsg.Visible = false;
                    if (dr["For_Action_By"].ToString() != "" && (dr["For_Action_By"].ToString() == GetSessionUserName()))
                    {
                        btnVerify.Visible = true;
                        trSubmission.Visible = true;
                        trWorkFlow.Visible = false;
                        trApprove.Visible = false;
                    }
                    if (dr["APPROVER"].ToString() != "" && (dr["APPROVER"].ToString() == GetSessionUserName()))
                    {
                        btnApprove.Visible = true;
                        trSubmission.Visible = true;
                        trWorkFlow.Visible = false;
                        trApprove.Visible = false;
                    }
                    if (Convert.ToInt16(lblcreated_by.Text) == GetSessionUserID())
                    {
                        btnRecall.Visible = true;
                    }

                }

                if (dr["Line_Status"].ToString() == "APPROVED")
                {
                    if (dr["Line_Status"].ToString() == "APPROVED" && (dr["APPROVER"].ToString() == GetSessionUserName()))
                    {
                        btnUndoApproval.Visible = true;
                    }
                    else
                    {
                        btnUndoApproval.Visible = false;
                    }
                    trWorkFlow.Visible = false;
                    trApprove.Visible = false;
                    btnNotify.Visible = true;
                    btnPreview.Visible = true;
                    btnSave.Enabled = false;
                    lblApproved.Visible = true;
                    lblApprovedBy.Visible = true;
                    lblApprovedBy.Text = dr["Approved_Name"].ToString();
                    lblApprovalmsg.Visible = false;
                    EnableDisable(false);
                    EnableDisableControls(false);
                }
              

                if (dr["PO_Closed_Date"].ToString() == "")
                {
                    if (dr["Line_Status"].ToString() == "DRAFT")
                    {
                        EnableDisable(true);
                        btnSave.Enabled = true;
                    }
                    btnClose.Visible = true;
                    btnUnClose.Visible = false;
                    lblPoClosed.Visible = false;
                }
                else
                {
                    btnAttachDocs.Enabled = false;
                    EnableDisable(false);
                    btnApprove.Visible = false;
                    lblPoClosed.Visible = true;
                    btnUnClose.Visible = true;
                    btnClose.Visible = false;
                }
                }
            }
            else
            {
                clearControl();
            }

        }
        catch { }
        {
        }

    }
    private void Common()
    {
        //Check Approval Limit Set or not Set
        if (ApprovalFlag == true)
        {
            if (lblPo_Used_Value.Text != "0.00")
            {
                trmsg.Visible = true;
                trWorkFlow.Visible = false;
                trApprove.Visible = false;
                trSubmission.Visible = false;
            }
        }
        else
        {
            if (lblPo_Used_Value.Text != "0.00")
            {
                trmsg.Visible = false;
                trWorkFlow.Visible = true;
                trApprove.Visible = true;
                trSubmission.Visible = false;
            }
        }
        //Check Created By
        if (lblCreatedBY.Text != GetSessionUserName())
        {
            EnableDisable(false);
            EnableDisableControls(false);
        }
        else
        {
            btnDelete.Visible = true;
            btnApproval.Visible = true;
        }
    }
    private void EnableDisableControls(bool blnFlag)
    {
        ddlVessel.Enabled = blnFlag;
        txtShipReferance.Enabled = blnFlag;
        txtPort.Enabled = blnFlag;
        txtETA.Enabled = blnFlag;
        txtUrgency.Enabled = blnFlag;
        ddlAgent.Enabled = blnFlag;
        ddlCurrency.Enabled = blnFlag;
        txtCurrChange.Enabled = blnFlag;
        ddlAccountType.Enabled = blnFlag;
        ddlCharterParty.Enabled = blnFlag;
        ddlAccClassifictaion.Enabled = blnFlag;
        ddlSupplier.Enabled = blnFlag;
        txtSuppRef.Enabled = blnFlag;
        txtRemarks.Enabled = blnFlag;
        ddlPortCall.Enabled = blnFlag;
        ddlOwnerCode.Enabled = blnFlag;
    }
    private void ControlVisible(bool blnFlag)
    {
        trSubmission.Visible = blnFlag;
        trmsg.Visible = blnFlag;
        trWorkFlow.Visible = blnFlag;
        trApprove.Visible = blnFlag;
        btnPreview.Visible = blnFlag;
        btnVerify.Visible = blnFlag;
        btnDelete.Visible = blnFlag;
        btnNotify.Visible = blnFlag;
        btnApproval.Visible = blnFlag;
        btnUndoApproval.Visible = blnFlag;
        btnApprove.Visible = blnFlag;
        btnRecall.Visible = blnFlag;
    }
    private void EnableDisable(bool blnFlag)
    {
        btnHide.Enabled = blnFlag;
        btnItemSave.Enabled = blnFlag;
    }
    //Fill Verifier and Approver User
    protected void BindApproval()
    {

        DataSet ds = BLL_POLOG_Register.POLOG_Get_Verifier_Approver(ddlAccClassifictaion.SelectedValue, txtPOType.Text, UDFLib.ConvertDecimalToNull(lblExchangeRate.Text), UDFLib.ConvertIntegerToNull(GetSessionUserID()));


        if (ds.Tables[1].Rows.Count > 0)
        {
            ApprovalFlag = false;
            ddlApproval.DataSource = ds.Tables[1];
            ddlApproval.DataTextField = "User_name";
            ddlApproval.DataValueField = "UserID";
            ddlApproval.DataBind();
            ddlApproval.Items.Insert(0, new ListItem("-Select-", "0"));
        }
        else
        {
            ApprovalFlag = true;
            lblApprovalmsg.Text = "Approval Limit has not define for this amount.";
        }
        if (ds.Tables[2].Rows.Count > 0)
        {
            for (int i = 0; i < ds.Tables[2].Rows.Count; i++)
            {
                DataView dv = new DataView(ds.Tables[0]);
                dv.RowFilter = "Approval_Limit_ID='" + ds.Tables[2].Rows[i][0].ToString() + "'";
                DropDownList ddl = new DropDownList();
                ddl.ID = "drp" + "" + ds.Tables[2].Rows[i][0].ToString();
                ddl.Controls.Clear();
                pnlInfo.Controls.Clear();
                foreach (DataRowView drv in dv)
                {
                    ddl.Items.Add(new ListItem(drv["User_name"].ToString(), drv["UserID"].ToString()));
                    pnlInfo.Controls.Add(ddl);
                }
            }

        }
        else
        {
            DropDownList ddl = new DropDownList();
            ddl.ID = "drp" + "" + "1";
            ddl.Items.Add(new ListItem("-Select-", ""));
            pnlInfo.Controls.Add(ddl);
        }
        Literal lt = new Literal();
        lt.Text = "<br />";
        pnlInfo.Controls.Add(lt);


    }



    protected void clearControl()
    {
        btnTranscLog.Visible = false;
        btnAttachDocs.Visible = false;
        //btnFolder.Visible = false;
        //btnSupplierRemarks.Visible = false;
        //btnAdvanceReq.Visible = false;
        btnRemarks.Visible = false;
        btnNotify.Visible = false;
        btnViewLimit.Visible = false;
    }

    protected void Save(string Status, string Action_On_Data_Form)
    {
        int RetValue = BLL_POLOG_Register.POLOG_Insert_Update_PO(UDFLib.ConvertIntegerToNull(txtPOCode.Text)
              , UDFLib.ConvertStringToNull(txtPOType.Text), UDFLib.ConvertIntegerToNull(txtSupplyID.Text)
              , txtReferance.Text.Trim(), txtShipReferance.Text.Trim(), UDFLib.ConvertIntegerToNull(ddlVessel.SelectedValue),
              txtPort.Text.Trim(), UDFLib.ConvertStringToNull(txtETA.Text.Trim()),
              txtUrgency.Text.Trim(), txtCurrChange.Text.Trim(), txtSuppRef.Text.Trim(), txtRemarks.Text.Trim(),
              UDFLib.ConvertStringToNull(ddlCurrency.SelectedValue), UDFLib.ConvertStringToNull(ddlAgent.SelectedValue),
              UDFLib.ConvertStringToNull(ddlAccountType.SelectedValue),
              UDFLib.ConvertStringToNull(ddlAccClassifictaion.SelectedValue), UDFLib.ConvertStringToNull(ddlSupplier.SelectedValue),
              UDFLib.ConvertStringToNull(ddlOwnerCode.SelectedValue),
              UDFLib.ConvertStringToNull(ddlCharterParty.SelectedValue),
                0,
                UDFLib.ConvertIntegerToNull(ddlApproval.SelectedValue),
               Action_On_Data_Form, Status,
               UDFLib.ConvertIntegerToNull(GetSessionUserID()), UDFLib.ConvertStringToNull(ddlPortCall.SelectedValue));

        txtPOCode.Text = Convert.ToString(RetValue);

    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        BindItem();
    }

    protected void gvItem_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            if (ddlCurrency.SelectedIndex > 0)
            {
                ((Label)e.Row.FindControl("lblPriceCurrency")).Text = ddlCurrency.SelectedValue;
            }
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            string Status = "DRAFT";
            Save(Status, "DRAFT");
            BindPODetails();
            string message = "alert('PO Saved.')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            //string msgDraft = String.Format("parent.ReloadParent_ByButtonID();");
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgDraft", msgDraft, true);

        }
        catch { }
        { }
    }

    protected void btnItemSave_Click(object sender, EventArgs e)
    {
        try
        {
            string HideText = null;
            if (btnHide.Text == "Hide")
            {
                HideText = null;
            }
            else
            {
                HideText = "YES";
            }
            saveval();
            int retval = BLL_POLOG_Register.POLOG_Update_Amount(UDFLib.ConvertIntegerToNull(txtSupplyID.Text.ToString()), UDFLib.ConvertDecimalToNull(txtDiscount.Text), ddlDiscountType.SelectedValue, UDFLib.ConvertDecimalToNull(lblDiscAmt.Text), HideText, UDFLib.ConvertIntegerToNull(Session["USERID"].ToString()));
            //BindItem();
            BindPODetails();
            //string msgDraft = String.Format("parent.ReloadParent_ByButtonID();");
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgDraft", msgDraft, true);
        }
        catch { }
        { }
    }
    protected void btnHide_Click(object sender, EventArgs e)
    {
        try
        {
            string HideText = null;
            if (btnHide.Text == "Hide")
            {
                HideText = "YES";
            }
            else
            {
                HideText = null;
            }
            int retval = BLL_POLOG_Register.POLOG_Update_Amount(UDFLib.ConvertIntegerToNull(txtSupplyID.Text.ToString()), UDFLib.ConvertDecimalToNull(txtDiscount.Text), ddlDiscountType.SelectedValue, UDFLib.ConvertDecimalToNull(lblDiscAmt.Text), HideText, UDFLib.ConvertIntegerToNull(Session["USERID"].ToString()));
            BindItems();
        }
        catch { }
        {
        }
    }
    protected void ddlDiscountType_SelectedIndexChanged(object sender, EventArgs e)
    {
        //decimal Totalprice = 0;
        try
        {
            if (ddlDiscountType.Text == "Percent")
            {
                lblDiscAmt.Text = Convert.ToString((Convert.ToDecimal(lblTotalPrice.Text) * Convert.ToDecimal(txtDiscount.Text)) / 100);
                txtTotalPrice.Text = Convert.ToString(Convert.ToDecimal(lblTotalPrice.Text) - Convert.ToDecimal(lblDiscAmt.Text));
                lblExchangeRate.Text = Convert.ToString((Convert.ToDecimal(txtTotalPrice.Text) * Convert.ToDecimal(txtExchangeRate.Text)));
            }
            else if (ddlDiscountType.Text == "Lumpsum")
            {
                lblDiscAmt.Text = Convert.ToString(Convert.ToDecimal(txtDiscount.Text));
                txtTotalPrice.Text = Convert.ToString(Convert.ToDecimal(lblTotalPrice.Text) - Convert.ToDecimal(txtDiscount.Text));
                lblExchangeRate.Text = Convert.ToString((Convert.ToDecimal(txtTotalPrice.Text) * Convert.ToDecimal(txtExchangeRate.Text)));
            }
            else
            {
                if (lblDiscAmt.Text != "")
                {
                    lblDiscAmt.Text = "0.0000";
                    txtTotalPrice.Text = Convert.ToString(Convert.ToDecimal(lblTotalPrice.Text) - Convert.ToDecimal(lblDiscAmt.Text));
                    lblExchangeRate.Text = Convert.ToString((Convert.ToDecimal(txtTotalPrice.Text) * Convert.ToDecimal(txtExchangeRate.Text)));
                }
                else
                {
                    lblDiscAmt.Text = "0.0000";
                    txtTotalPrice.Text = Convert.ToString(Convert.ToDecimal(lblTotalPrice.Text));
                    lblExchangeRate.Text = Convert.ToString((Convert.ToDecimal(txtTotalPrice.Text) * Convert.ToDecimal(txtExchangeRate.Text)));
                }
            }
        }
        catch { }
        {
        }


    }
    protected void txtDiscount_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlDiscountType.Text == "Percent")
            {
                lblDiscAmt.Text = Convert.ToString((Convert.ToDecimal(lblTotalPrice.Text) * Convert.ToDecimal(txtDiscount.Text)) / 100);
                txtTotalPrice.Text = Convert.ToString(Convert.ToDecimal(lblTotalPrice.Text) - Convert.ToDecimal(lblDiscAmt.Text));
                lblExchangeRate.Text = Convert.ToString((Convert.ToDecimal(txtTotalPrice.Text) * Convert.ToDecimal(txtExchangeRate.Text)));
            }
            else if (ddlDiscountType.Text == "Lumpsum")
            {
                lblDiscAmt.Text = Convert.ToString(Convert.ToDecimal(txtDiscount.Text));
                txtTotalPrice.Text = Convert.ToString(Convert.ToDecimal(lblTotalPrice.Text) - Convert.ToDecimal(txtDiscount.Text));
                lblExchangeRate.Text = Convert.ToString((Convert.ToDecimal(txtTotalPrice.Text) * Convert.ToDecimal(txtExchangeRate.Text)));
            }
            else
            {
                if (lblDiscAmt.Text != "")
                {
                    lblDiscAmt.Text = "0.0000";
                    txtTotalPrice.Text = Convert.ToString(Convert.ToDecimal(lblTotalPrice.Text) - Convert.ToDecimal(lblDiscAmt.Text));
                    lblExchangeRate.Text = Convert.ToString((Convert.ToDecimal(txtTotalPrice.Text) * Convert.ToDecimal(txtExchangeRate.Text)));
                }
                else
                {
                    lblDiscAmt.Text = "0.0000";
                    txtTotalPrice.Text = Convert.ToString(Convert.ToDecimal(lblTotalPrice.Text));
                    lblExchangeRate.Text = Convert.ToString((Convert.ToDecimal(txtTotalPrice.Text) * Convert.ToDecimal(txtExchangeRate.Text)));
                }
            }
        }
        catch { }
        {
        }
    }





    //Close PO
    protected void btnClose_Click(object sender, EventArgs e)
    {
        try
        {
            string Type = "Close";
            int retval = BLL_POLOG_Register.POLOG_Update_PODeatils(UDFLib.ConvertIntegerToNull(txtSupplyID.Text.ToString()), Type, UDFLib.ConvertIntegerToNull(Session["USERID"].ToString()));
            InsertAuditTrail("Close PO", "ClosePO");
            BindPODetails();
            //string msgDraft = String.Format("parent.ReloadParent_ByButtonID();");
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgDraft", msgDraft, true);
        }
        catch { }
        { }
    }
    //Approve PO
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        try
        {
            string Type = "Approve";
            int retval = BLL_POLOG_Register.POLOG_Update_PODeatils(UDFLib.ConvertIntegerToNull(txtSupplyID.Text.ToString()), Type, UDFLib.ConvertIntegerToNull(Session["USERID"].ToString()));
            InsertAuditTrail("Approve PO", "ApprovePO");
            BindPODetails();

        }
        catch { }
        { }
    }
    //Verify PO
    protected void btnVerify_Click(object sender, EventArgs e)
    {
        try
        {
            string Type = "Verify";
            int retval = BLL_POLOG_Register.POLOG_Update_PODeatils(UDFLib.ConvertIntegerToNull(txtSupplyID.Text.ToString()), Type, UDFLib.ConvertIntegerToNull(Session["USERID"].ToString()));
            InsertAuditTrail("Verify PO", "VerifyPO");
            BindPODetails();

        }
        catch { }
        { }
    }
    //Open PO for user 
    protected void btnUnClose_Click(object sender, EventArgs e)
    {
        try
        {
            string Type = "UnClose";
            int retval = BLL_POLOG_Register.POLOG_Update_PODeatils(UDFLib.ConvertIntegerToNull(txtSupplyID.Text.ToString()), Type, UDFLib.ConvertIntegerToNull(Session["USERID"].ToString()));
            InsertAuditTrail("UnClose PO", "UNClosePO");
            BindPODetails();

            //string msgDraft = String.Format("parent.ReloadParent_ByButtonID();");
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgDraft", msgDraft, true);
        }
        catch { }
        { }
    }
    //Submit For Approval
    protected void btnApproval_Click(object sender, EventArgs e)
    {
        try
        {
            string POStatus = "Approval";
            int retval = BLL_POLOG_Register.POLOG_Send_For_Approval(UDFLib.ConvertIntegerToNull(txtSupplyID.Text.ToString()), UDFLib.ConvertIntegerToNull(ddlApproval.SelectedValue), POStatus, UDFLib.ConvertIntegerToNull(Session["USERID"].ToString()));
            InsertAuditTrail("Submit for Approval", "SubmitPO");
            BindPODetails();

        }
        catch { }
        { }
    }
    //Selected Index for Current Exchange Rate
    protected void ddlCurrency_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt = BLL_POLOG_Register.POLOG_Current_Currency_Exchangerate(ddlCurrency.SelectedValue);
        if (dt.Rows.Count > 0)
        {
            txtCurrChange.Text = dt.Rows[0]["Exch_rate"].ToString();
        }
        BindApproval();
    }
    //Recall PO from Approval
    protected void btnRecall_Click(object sender, EventArgs e)
    {
        try
        {
            string POStatus = "Recall";
            int retval = BLL_POLOG_Register.POLOG_Send_For_Approval(UDFLib.ConvertIntegerToNull(txtSupplyID.Text.ToString()), UDFLib.ConvertIntegerToNull(ddlApproval.SelectedValue), POStatus, UDFLib.ConvertIntegerToNull(Session["USERID"].ToString()));
            InsertAuditTrail("Recall PO", "RecallPO");
            BindPODetails();


        }
        catch { }
        { }
    }
    //Insert All Transcation LOG
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
    //Delete PO
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {

            string POStatus = "DELETED";
            int retval = BLL_POLOG_Register.POLOG_Delete_PO(UDFLib.ConvertIntegerToNull(txtSupplyID.Text.ToString()), POStatus, UDFLib.ConvertIntegerToNull(Session["USERID"].ToString()));
            InsertAuditTrail("Delete PO", "DeletePO");
            BindPODetails();
            //string msgDraft = String.Format("parent.ReloadParent_ByButtonID();");
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgDraft", msgDraft, true);

        }
        catch { }
        { }
    }
    //Un Approve PO
    protected void btnUndoApproval_Click(object sender, EventArgs e)
    {
        try
        {
            string POStatus = "UnApprove";
            int retval = BLL_POLOG_Register.POLOG_Send_For_Approval(UDFLib.ConvertIntegerToNull(txtSupplyID.Text.ToString()), UDFLib.ConvertIntegerToNull(ddlApproval.SelectedValue), POStatus, UDFLib.ConvertIntegerToNull(Session["USERID"].ToString()));
            InsertAuditTrail("Undo Approval PO", "UndoApproval");
            BindPODetails();


        }
        catch { }
        { }
    }
    //Fill Charter Part And Port Call by passing VesselID
    protected void ddlVessel_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BindCharterParty(UDFLib.ConvertIntegerToNull(ddlVessel.SelectedValue));
            BindPortCall(UDFLib.ConvertIntegerToNull(ddlVessel.SelectedValue));
            BindApproval();
        }
        catch { }
        { }
    }
    //Save PO Item 
    private void saveval()
    {
        StringBuilder strIDVals = new StringBuilder();
        StringBuilder strItemdesciption = new StringBuilder();
        StringBuilder stritemUnits = new StringBuilder();
        StringBuilder strItemRequestQty = new StringBuilder();
        StringBuilder strItemComments = new StringBuilder();
        StringBuilder strUnitPrice = new StringBuilder();
        StringBuilder strDiscount = new StringBuilder();
        StringBuilder strSuppCode = new StringBuilder();
        StringBuilder strBgtCode = new StringBuilder();
        StringBuilder ItemRefCode = new StringBuilder();

        int i = 0;

        DataTable dtExtraItems = new DataTable();
        dtExtraItems.Columns.Add("pkid");
        dtExtraItems.Columns.Add("Item_Code");
        dtExtraItems.Columns.Add("Item_Short_Desc");
        dtExtraItems.Columns.Add("ORDER_QTY");
        dtExtraItems.Columns.Add("ORDER_PRICE");
        dtExtraItems.Columns.Add("Item_Discount");
        dtExtraItems.Columns.Add("Unit");
        dtExtraItems.Columns.Add("Item_Long_Desc");



        // dtExtraItems.Columns.Add("BGT_CODE");

        int inc = 1;
        foreach (GridDataItem dataItem in rgdItems.MasterTableView.Items)
        {
            HiddenField lblgrdID = (dataItem.FindControl("lblID") as HiddenField);
            TextBox txtgrdItem_Code = (dataItem.FindControl("txtItem_Code") as TextBox);
            TextBox txtgrdItemReqQty = (dataItem.FindControl("txtRequest_Qty") as TextBox);
            TextBox txtgrdItemComent = (dataItem.FindControl("txtItem_Comments") as TextBox);
            TextBox txtItemDescription = (dataItem.FindControl("txtItem_Description") as TextBox);
            TextBox txtUnit = (dataItem.FindControl("cmbUnitnPackage") as TextBox);
            TextBox txtDiscount = (dataItem.FindControl("txtDiscount") as TextBox);
            TextBox txtUnitPrice = (dataItem.FindControl("txtUnitPrice") as TextBox);
            //string BgtCode = (dataItem.FindControl("ddlBudgetCode") as DropDownList).SelectedValue;

            if (txtgrdItemReqQty.Text.Length > 0 && txtItemDescription.Text.Length > 0)
            {
                DataRow dritem = dtExtraItems.NewRow();
                dritem["pkid"] = lblgrdID.Value;
                dritem["Item_Code"] = (txtgrdItem_Code.Text.Replace(",", " "));
                dritem["Item_Short_Desc"] = (txtItemDescription.Text.Replace(",", " "));
                dritem["ORDER_QTY"] = (txtgrdItemReqQty.Text.Trim() == "" ? "0" : txtgrdItemReqQty.Text.Trim());
                dritem["ORDER_PRICE"] = (txtUnitPrice.Text.Trim() == "" ? "0" : txtUnitPrice.Text.Trim());
                dritem["Item_Discount"] = (txtDiscount.Text.Trim() == "" ? "0" : txtDiscount.Text.Trim());
                dritem["Unit"] = (txtUnit.Text.Trim() == "" ? "" : txtUnit.Text.Trim());
                dritem["Item_Long_Desc"] = (txtgrdItemComent.Text.Replace(",", " "));
                dtExtraItems.Rows.Add(dritem);
                inc++;

            }
        }

        int retval = 0;

        if (dtExtraItems.Rows.Count > 0)
        {
            retval = BLL_POLOG_Register.POLOG_Insert_Update_POItem(UDFLib.ConvertIntegerToNull(txtSupplyID.Text.ToString()), dtExtraItems, UDFLib.ConvertIntegerToNull(GetSessionUserID()));
            //BindItems();
            //string msgDraft = String.Format("parent.ReloadParent_ByButtonID();");
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgDraft", msgDraft, true);
            //lblError.Text = "Item(s) saved successfully.";
            //BindItems();
            //UpdGrid.Update();
        }
        else
        {
            //lblError.Text = "Please provide Item description.";
        }


    }

    protected void rgdItems_ItemDataBound(object sender, GridItemEventArgs e)
    {
        foreach (GridDataItem dataItem in rgdItems.MasterTableView.Items)
        {
            TextBox txtLT = (TextBox)(dataItem.FindControl("txtRequest_Qty") as TextBox);
            TextBox txtUNt = (TextBox)(dataItem.FindControl("txtUnitPrice") as TextBox);
            TextBox txtDis = (TextBox)(dataItem.FindControl("txtDiscount") as TextBox);
            HiddenField lblID = (HiddenField)(dataItem.FindControl("lblID") as HiddenField);
            ImageButton btnDelete = (ImageButton)(dataItem.FindControl("ImgDelete") as ImageButton);
            txtLT.Attributes.Add("onKeydown", "return MaskMoney(event)");
            txtUNt.Attributes.Add("onKeydown", "return MaskMoney(event)");
            txtDis.Attributes.Add("onKeydown", "return MaskMoney(event)");
            if (lblID.Value == "0")
            {
                btnDelete.Visible = false;
            }

        }

        if (e.Item is GridDataItem)
        {
            GridDataItem dataItem = e.Item as GridDataItem;
            if ((dataItem["Request_Qty"].FindControl("txtRequest_Qty") as TextBox).Text != "")
            {
                total_Qty += decimal.Parse((dataItem["Request_Qty"].FindControl("txtRequest_Qty") as TextBox).Text);
            }
            if ((dataItem["UnitPrice"].FindControl("txtUnitPrice") as TextBox).Text != "")
            {
                total_Price += decimal.Parse((dataItem["UnitPrice"].FindControl("txtUnitPrice") as TextBox).Text);
            }
            if ((dataItem["Discount"].FindControl("txtDiscount") as TextBox).Text != "")
            {
                total_Discount += decimal.Parse((dataItem["Discount"].FindControl("txtDiscount") as TextBox).Text);
            }
        }
        else if (e.Item is GridFooterItem)
        {
            GridFooterItem footer = (GridFooterItem)e.Item;
            (footer["Request_Qty"].FindControl("lblQty") as Label).Text = total_Qty.ToString();
            clientID = (footer["Request_Qty"].FindControl("lblQty") as Label).ClientID;

            (footer["UnitPrice"].FindControl("lblUnitPrice") as Label).Text = total_Price.ToString();
            clientID = (footer["UnitPrice"].FindControl("lblUnitPrice") as Label).ClientID;

            (footer["Discount"].FindControl("lblDiscount") as Label).Text = total_Discount.ToString();
            clientID = (footer["Discount"].FindControl("lblDiscount") as Label).ClientID;
        }
    }
    //Create blank rows in Item Gridview
    private DataTable GetAddTable()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("ID", typeof(Int32));
        dt.Columns.Add("Srno", typeof(Int32));
        dt.Columns.Add("Item_Code", typeof(string));
        dt.Columns.Add("Item_Short_Desc", typeof(string));
        dt.Columns.Add("Unit", typeof(string));
        dt.Columns.Add("ORDER_QTY", typeof(Double));
        dt.Columns.Add("ORDER_PRICE", typeof(Double));
        dt.Columns.Add("Item_Discount", typeof(Double));
        dt.Columns.Add("Item_Long_Desc", typeof(string));
        dt.AcceptChanges();
        for (int i = 1; i <= 1; i++)
        {
            DataRow dr = dt.NewRow();
            dr[0] = i;
            dr[1] = i;
            dt.Rows.Add(dr);

        }
        dt.AcceptChanges();
        return dt;
    }
    //Add new PO Item
    protected void btnAddNewItem_Click(object s, EventArgs e)
    {
        dtGridItems = (DataTable)ViewState["dtGridItems"];
        int RowID = 0;
        foreach (GridDataItem item in rgdItems.MasterTableView.Items)
        {
            dtGridItems.Rows[RowID]["Srno"] = ((Label)item.FindControl("lblSrno")).Text;
            dtGridItems.Rows[RowID]["ID"] = ((HiddenField)item.FindControl("lblID")).Value;
            dtGridItems.Rows[RowID]["Unit"] = ((TextBox)item.FindControl("cmbUnitnPackage")).Text;
            dtGridItems.Rows[RowID]["Item_Code"] = ((TextBox)item.FindControl("txtItem_Code")).Text;
            dtGridItems.Rows[RowID]["Item_Short_Desc"] = ((TextBox)item.FindControl("txtItem_Description")).Text;
            dtGridItems.Rows[RowID]["ORDER_QTY"] = ((TextBox)item.FindControl("txtRequest_Qty")).Text == "" ? Convert.DBNull : Convert.ToDecimal(((TextBox)item.FindControl("txtRequest_Qty")).Text);

            dtGridItems.Rows[RowID]["ORDER_PRICE"] = ((TextBox)item.FindControl("txtUnitPrice")).Text == "" ? Convert.DBNull : Convert.ToDecimal(((TextBox)item.FindControl("txtUnitPrice")).Text);
            dtGridItems.Rows[RowID]["Item_Discount"] = ((TextBox)item.FindControl("txtDiscount")).Text == "" ? Convert.DBNull : Convert.ToDecimal(((TextBox)item.FindControl("txtDiscount")).Text);

            dtGridItems.Rows[RowID]["Item_Long_Desc"] = ((TextBox)item.FindControl("txtItem_Comments")).Text;

            RowID++;
        }

        DataRow dr = dtGridItems.NewRow();
        if (rgdItems.Items.Count == 0)
        {
            dr[0] = 1;
            dr[1] = 0;
        }
        else
        {
            dr[0] = UDFLib.ConvertToInteger(((Label)rgdItems.Items[rgdItems.Items.Count - 1].FindControl("lblSrno")).Text) + 1;
            dr[1] = 0;
        }
        dtGridItems.Rows.Add(dr);
        rgdItems.DataSource = dtGridItems;
        rgdItems.DataBind();
        //rgdItems.MasterTableView.Columns[8].Visible = false;
        ViewState["dtGridItems"] = dtGridItems;


    }
    //Delete PO Item
    protected void onDelete(object source, CommandEventArgs e)
    {
        HiddenField lblgrdID = (rgdItems.FindControl("lblID") as HiddenField);
        string[] arg = e.CommandArgument.ToString().Split(',');
        int? ItemID = UDFLib.ConvertIntegerToNull(arg[0]);
        //dtGridItems.Rows[RowID]["ID"] = ((HiddenField)item.FindControl("lblID")).Value;
        int retval = BLL_POLOG_Register.POLOG_Delete_Item(Convert.ToInt32(ItemID), Convert.ToInt32(Session["USERID"].ToString()));
        BindItems();
    }
    //PO Item Bind 
    private void BindItems()
    {
        try
        {
            DataSet ds = BLL_POLOG_Register.POLOG_Get_Item_List(UDFLib.ConvertIntegerToNull(txtSupplyID.Text.ToString()));
            if (ds.Tables[0].Rows.Count > 0)
            {
                dtGridItems = ds.Tables[0];
                rgdItems.DataSource = ds;
                rgdItems.DataBind();
                rgdItems.MasterTableView.Columns[7].Visible = true;
            }
            else
            {
                dtGridItems = GetAddTable();
                rgdItems.DataSource = dtGridItems;
                rgdItems.DataBind();
                rgdItems.MasterTableView.Columns[7].Visible = false;
                //rgdItems.MasterTableView.Columns[9].Visible = false;

            }
            ViewState["dtGridItems"] = dtGridItems;
            if (ds.Tables[1].Rows.Count > 0)
            {
                lblTotalPrice.Text = ds.Tables[1].Rows[0]["Line_Amount"].ToString();
                txtDiscount.Text = ds.Tables[1].Rows[0]["Discount"].ToString();
                lblDiscAmt.Text = ds.Tables[1].Rows[0]["DiscountAmount"].ToString();
                if (ds.Tables[1].Rows[0]["Discount_Type"].ToString() != "0")
                {
                    ddlDiscountType.SelectedValue = ds.Tables[1].Rows[0]["Discount_Type"].ToString();
                }
                txtTotalPrice.Text = ds.Tables[1].Rows[0]["TotalAmount"].ToString();
                lblExchangeRate.Text = ds.Tables[1].Rows[0]["ExchangeAmount"].ToString();
                lblPo_Used_Value.Text = ds.Tables[1].Rows[0]["ExchangeAmount"].ToString();
                lblPOAmount.Visible = true;
                lblPo_Used_Value.Visible = true;
                if (txtPOType.Text == "SP")
                {
                    if (ds.Tables[1].Rows[0]["Hide_PO_Value"].ToString() == "YES")
                    {
                        btnHide.Text = "Hide";
                        lblAlert.Text = "PO order quantity and price is shown to supplier.";
                        lblAlert1.Text = "Convert this to Service PO by Hiding.";

                    }
                    else
                    {
                        btnHide.Text = "UnHide";
                        lblAlert.Text = "PO Value is hidden from Supplier.";
                        lblAlert1.Text = "Convert this to Normal PO by unhiding. ";
                    }
                }
                else
                {
                    if (ds.Tables[1].Rows[0]["Hide_PO_Value"].ToString() == "YES")
                    {
                        btnHide.Text = "UnHide";
                        lblAlert.Text = "PO Value is hidden from Supplier.";
                        lblAlert1.Text = "Convert this to Normal PO by unhiding. ";
                    }
                    else
                    {
                        btnHide.Text = "Hide";
                        lblAlert.Text = "PO order quantity and price is shown to supplier.";
                        lblAlert1.Text = "Convert this to Service PO by Hiding.";
                    }
                }

                //BindApproval();
            }
            BindApproval();

        }
        catch (Exception ex)
        {
            //lblError.Text = ex.ToString();
            //.WriteError(this.GetType().Name.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), ex);
        }
        finally
        {

        }

    }
    //HTML file Export To PDF
    protected void DownloadFiles(object sender, EventArgs e)
    {
        try
        {

            EO.Pdf.HtmlToPdf.Options.PageSize = new SizeF(8.27f, 11.69f);

            PdfDocument doc = new PdfDocument();

            string GUID = Guid.NewGuid().ToString();
            string filePath = Server.MapPath("~/Uploads/POLog/" + GUID + ".pdf");
            //string FileName = "~/Uploads/Reports/" + GUID + ".pdf";

            EO.Pdf.Runtime.AddLicense("p+R2mbbA3bNoqbTC4KFZ7ekDHuio5cGz4aFZpsKetZ9Zl6TNHuig5eUFIPGe" +
          "tcznH+du5PflEuCG49jjIfewwO/o9dB2tMDAHuig5eUFIPGetZGb566l4Of2" +
          "GfKetZGbdePt9BDtrNzCnrWfWZekzRfonNzyBBDInbW6yuCwb6y9xtyxdabw" +
          "+g7kp+rp2g+9RoGkscufdePt9BDtrNzpz+eupeDn9hnyntzCnrWfWZekzQzr" +
          "peb7z7iJWZekscufWZfA8g/jWev9ARC8W7zTv/vjn5mkBxDxrODz/+ihb6W0" +
          "s8uud4SOscufWbOz8hfrqO7CnrWfWZekzRrxndz22hnlqJfo8h8=");
            // HtmlToPdf.Options.AfterRenderPage = new EO.Pdf.PdfPageEventHandler(On_AfterRenderPage);
            EO.Pdf.HtmlToPdf.Options.FooterHtmlFormat = "<div style='text-align:right; font-family:Tahoma; font-size:12px'>Page {page_number} of {total_pages}</div>";

            HtmlToPdf.Options.AutoFitX = HtmlToPdfAutoFitMode.None;
            //HtmlToPdf.Options.AutoFitY = HtmlToPdfAutoFitMode.None;


            // HtmlToPdf.Options.AutoAdjustForDPI=true;
            //  HtmlToPdf.Options.PageSize = EO.Pdf.PdfPageSizes.Letter;
            //string TemplateText = ltPODetails.Text;// hdnContent.Value;s
            string TemplateText = hdnPODetails.Value.ToString(); ;
            HtmlToPdf.ConvertHtml(TemplateText, filePath);
            int newtab = 0;
            //newtab = UDFLib.ConvertToInteger(ViewState["newtabnumber"]);
            //newtab++;
            //ViewState["newtabnumber"] = newtab;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "hideText", "window.open('~/Uploads/POLog/" + GUID + ".pdf','INSPRPT" + newtab + "');", true);  // (  this.GetType(), "OpenWindow", "window.open('../../Uploads/InspectionReport.pdf','_newtab');", true);
            Response.Redirect("~/Uploads/POLog/" + GUID + ".pdf");
        }
        catch
        { }

    }

    protected void BindItem()
    {
        //DataSet ds = BLL_POLOG_Register.POLOG_Get_Item_List(UDFLib.ConvertIntegerToNull(txtSupplyID.Text.ToString()));
        //if (ds.Tables[0].Rows.Count > 0)
        //{
        //    //tblItem.Visible = true;
        //    //gvItem.DataSource = ds.Tables[0];
        //    //gvItem.DataBind();
        //}
        //else
        //{
        //    //tblItem.Visible = false;
        //    //gvItem.DataSource = ds.Tables[0];
        //    //gvItem.DataBind();
        //}
        //if (ds.Tables[1].Rows.Count > 0)
        //{
        //    lblTotalPrice.Text = ds.Tables[1].Rows[0]["Line_Amount"].ToString();
        //    txtDiscount.Text = ds.Tables[1].Rows[0]["Discount"].ToString();
        //    lblDiscAmt.Text = ds.Tables[1].Rows[0]["DiscountAmount"].ToString();
        //    if (ds.Tables[1].Rows[0]["Discount_Type"].ToString() != "0")
        //    {
        //        ddlDiscountType.SelectedValue = ds.Tables[1].Rows[0]["Discount_Type"].ToString();
        //    }
        //    txtTotalPrice.Text = ds.Tables[1].Rows[0]["TotalAmount"].ToString();
        //    lblExchangeRate.Text = ds.Tables[1].Rows[0]["ExchangeAmount"].ToString();
        //    lblPo_Used_Value.Text = ds.Tables[1].Rows[0]["ExchangeAmount"].ToString();
        //    lblPOAmount.Visible = true;
        //    lblPo_Used_Value.Visible = true;
        //    if (txtPOType.Text == "SP")
        //    {
        //        if (ds.Tables[1].Rows[0]["Hide_PO_Value"].ToString() == "YES")
        //        {
        //            btnHide.Text = "Hide";
        //            lblAlert.Text = "PO order quantity and price is shown to supplier.";
        //            lblAlert1.Text = "Convert this to Service PO by Hiding.";

        //        }
        //        else
        //        {
        //            btnHide.Text = "UnHide";
        //            lblAlert.Text = "PO Value is hidden from Supplier.";
        //            lblAlert1.Text = "Convert this to Normal PO by unhiding. ";
        //        }
        //    }
        //    else
        //    {
        //        if (ds.Tables[1].Rows[0]["Hide_PO_Value"].ToString() == "YES")
        //        {
        //            btnHide.Text = "UnHide";
        //            lblAlert.Text = "PO Value is hidden from Supplier.";
        //            lblAlert1.Text = "Convert this to Normal PO by unhiding. ";
        //        }
        //        else
        //        {
        //            btnHide.Text = "Hide";
        //            lblAlert.Text = "PO order quantity and price is shown to supplier.";
        //            lblAlert1.Text = "Convert this to Service PO by Hiding.";
        //        }
        //    }

        //    //BindApproval();
        //}
        //BindApproval();

    }
    protected void btnIssue_Click(object sender, EventArgs e)
    {
        try
        {
            string Type = "Issue";
            int retval = BLL_POLOG_Register.POLOG_Update_PODeatils(UDFLib.ConvertIntegerToNull(txtSupplyID.Text.ToString()), Type, UDFLib.ConvertIntegerToNull(Session["USERID"].ToString()));
            InsertAuditTrail("Issue PO", "IssuePO");
            BindPODetails();

        }
        catch { }
        { }
    }
}