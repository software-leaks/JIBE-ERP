using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.Infrastructure;
using SMS.Business.ASL;
using SMS.Properties;
using System.Text;

public partial class ASL_Evalution : System.Web.UI.Page
{
    UserAccess objUA = new UserAccess();
    UserAccess objUAType = new UserAccess();
    public Boolean uaEditFlag = true;
    public Boolean uaDeleteFlage = true;
    BLL_Infra_UserCredentials objBLLUser = new BLL_Infra_UserCredentials();
    public string str;
    public string ViewType;

    #region PageLoad
    /// <summary>
    /// Change done in user access validation checking,Its inside postback before it was outside of postback
    /// JIt- 11730/08/11/16
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!IsPostBack)
        {
            UserAccessValidation();
            ViewState["ReturnEvalutionID"] = 0;
            BindProposeStatus();
            BindScope();
            BindPort();
            BindSupplier();
        }
    }
    
    protected void BindApproverList(string Supplier_Type)
    {
        try
        {
            string Approver_Type = "Evaluation";
            DataSet ds = BLL_ASL_Supplier.Get_Supplier_ApproverList(UDFLib.ConvertToInteger(GetSessionUserID()), Supplier_Type, Approver_Type);

            ddlFinalApproverName.DataSource = ds.Tables[0];
            ddlFinalApproverName.DataValueField = "ApproveID";
            ddlFinalApproverName.DataTextField = "User_name";
            ddlFinalApproverName.DataBind();
            ddlFinalApproverName.Items.Insert(0, new ListItem("SELECT", "0"));


            ddlApproverName.DataSource = ds.Tables[1];
            ddlApproverName.DataValueField = "ApproveID";
            ddlApproverName.DataTextField = "User_name";
            ddlApproverName.DataBind();
            ddlApproverName.Items.Insert(0, new ListItem("SELECT", "0"));
        }
        catch
        {
        }
    }
    protected void BindSupplier()
    {
        try
        {
            int? EvalID = UDFLib.ConvertIntegerToNull(GetEvaluation_ID());
            string Supp_ID = GetSuppID();
            txtSupplier_Code.Text = Supp_ID;
            DataSet ds = BLL_ASL_Supplier.Get_Supplier_Evaluation_List(UDFLib.ConvertStringToNull(Supp_ID), EvalID);
            if (ds.Tables[2].Rows.Count > 0)
            {
                txtSupplierCode.Text = ds.Tables[2].Rows[0]["Supplier_Code"].ToString();
                txtType.Text = ds.Tables[2].Rows[0]["Supp_Type"].ToString();
                txtSubType.Text = ds.Tables[2].Rows[0]["Counterparty_Type"].ToString();
                txtStatus.Text = ds.Tables[2].Rows[0]["Supp_Status"].ToString();
                txtCompanyName.Text = ds.Tables[2].Rows[0]["Register_Name"].ToString();
                txtIncorporation.Text = ds.Tables[2].Rows[0]["Setup_Year"].ToString();
                txtAddress.Text = ds.Tables[2].Rows[0]["Address"].ToString();
                txtPhone.Text = ds.Tables[2].Rows[0]["Phone"].ToString();
                txtEmail.Text = ds.Tables[2].Rows[0]["Email"].ToString();
                txtFax.Text = ds.Tables[2].Rows[0]["Fax"].ToString();
                txtPICName.Text = ds.Tables[2].Rows[0]["Contact_1"].ToString();
                txtPICEmail.Text = ds.Tables[2].Rows[0]["Contact_1_Email"].ToString();
                txtPICPhone.Text = ds.Tables[2].Rows[0]["Contact_1_Phone"].ToString();
                txtRegistedData.Text = ds.Tables[2].Rows[0]["Record_Status"].ToString();
                txtSupplierDesc.Text = ds.Tables[2].Rows[0]["Supplier_Description"].ToString();
                if (ds.Tables[2].Rows[0]["Record_Status"].ToString() != "")
                {
                    btnRegisteredData.Visible = true;
                }
                else
                {
                    btnRegisteredData.Visible = false;
                }
                UserAccessTypeValidation(txtType.Text);
                BindApproverList(txtType.Text);
            }
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Evaluation_Status"].ToString() == "Approved")
                {
                    ViewState["ReturnEvalutionID"] = 0;
                    ClearControl();
                }
                else if (ds.Tables[0].Rows[0]["Evaluation_Status"].ToString() == "Rejected")
                {
                    ViewState["ReturnEvalutionID"] = 0;
                    ClearControl();
                }
                else
                {
                    ViewState["ReturnEvalutionID"] = ds.Tables[0].Rows[0]["Evaluation_ID"].ToString();
                    if (ds.Tables[0].Rows[0]["Supp_Status"].ToString() != "")
                    {
                        ddlProposedStatus.SelectedValue = ds.Tables[0].Rows[0]["Supp_Status"].ToString();
                    }
                    txtSupplierDesc.Text = ds.Tables[0].Rows[0]["Supplier_Description"].ToString();
                    ddlforPeriod.SelectedValue = ds.Tables[0].Rows[0]["Approval_Period"].ToString();
                    txtRemarks.Text = ds.Tables[0].Rows[0]["Justification_Remarks"].ToString();
                    txtVerificationRemrks.Text = ds.Tables[0].Rows[0]["Verifier_Remarks"].ToString();
                    txtApprovalRemrks.Text = ds.Tables[0].Rows[0]["Approver_Remarks"].ToString();

                    txtCreatedDate.Text = ds.Tables[0].Rows[0]["Created_Date"].ToString();
                    txtApprovedDate.Text = ds.Tables[0].Rows[0]["Approved_Date"].ToString();
                    txtFinalApprovedDate.Text = ds.Tables[0].Rows[0]["FinalApproved_Date"].ToString();
                    if (ddlApproverName.Items.FindByValue(ds.Tables[0].Rows[0]["SentForApproval"].ToString()) == null)
                    {
                        ddlApproverName.SelectedValue = "0";
                    }
                    else
                    {
                        ddlApproverName.SelectedValue = ds.Tables[0].Rows[0]["SentForApproval"].ToString();
                    }
                    if (ddlFinalApproverName.Items.FindByValue(ds.Tables[0].Rows[0]["SentForfinalApproval"].ToString()) == null)
                    {
                        ddlFinalApproverName.SelectedValue = "0";
                    }
                    else
                    {
                        ddlFinalApproverName.SelectedValue = ds.Tables[0].Rows[0]["SentForfinalApproval"].ToString();
                    }
                    lblEvalStatus.Text = ds.Tables[0].Rows[0]["Evaluation_Status"].ToString();
                    lblCreatedby.Text = ds.Tables[0].Rows[0]["Created_Name"].ToString();
                    txtEvalID.Text = ds.Tables[0].Rows[0]["Evaluation_ID"].ToString();
                    string str = ds.Tables[0].Rows[0]["Urgent_Flag"].ToString();
                    if (str == "Yes")
                    {
                        chkUrgent.Checked = true;
                    }
                    else
                    {
                        chkUrgent.Checked = false;
                    }
                    btnSaveEvalution.Visible = true;
                    btnVerify.Visible = true;
                }
                Visibelbutton(false);
                if (ds.Tables[0].Rows[0]["Evaluation_Status"].ToString() == "Rework")
                {
                    if (UDFLib.ConvertToInteger(GetSessionUserID()) == UDFLib.ConvertToInteger(ds.Tables[0].Rows[0]["Created_By"].ToString()))
                    {
                        EnableDisable(true);
                        btnSaveEvalution.Visible = true;
                        btnVerify.Visible = true;
                        txtRemarks.Enabled = true;
                        txtApprovalRemrks.Enabled = false;
                        txtVerificationRemrks.Enabled = false;
                    }
                }
                else if (ds.Tables[0].Rows[0]["Evaluation_Status"].ToString() == "DRAFT")
                {
                    if (UDFLib.ConvertToInteger(GetSessionUserID()) == UDFLib.ConvertToInteger(ds.Tables[0].Rows[0]["Created_By"].ToString()))
                    {
                        btnDelete.Visible = true;
                    }
                    else
                    {
                        btnDelete.Visible = false;
                    }
                    EnableDisable(true);
                    txtApprovalRemrks.Enabled = false;
                    txtVerificationRemrks.Enabled = false;
                    txtRemarks.Enabled = true;
                    btnSaveEvalution.Visible = true;
                    btnVerify.Visible = true;
                    btnRecallVerification.Visible = false;
                    btnRework.Visible = false;
                    btnRejected.Visible = false;
                    btnRecallApproval.Visible = false;
                    btnApproval.Visible = false;
                    btnRecallApproval.Visible = false;

                }
                else if (ds.Tables[0].Rows[0]["Evaluation_Status"].ToString() == "Approval")
                {
                    btnDelete.Visible = false;
                    btnSaveEvalution.Visible = false;
                    btnVerify.Visible = false;
                    if (UDFLib.ConvertToInteger(GetSessionUserID()) == UDFLib.ConvertToInteger(ds.Tables[0].Rows[0]["Created_By"].ToString()))
                    {
                        EnableDisable(false);
                        btnRecallVerification.Visible = true;
                        //if (UDFLib.ConvertToInteger(GetSessionUserID()) == UDFLib.ConvertToInteger(ds.Tables[0].Rows[0]["SentForApproval"].ToString()))
                        //{
                        //    btnRecallVerification.Visible = true;
                        //}
                        //else
                        //{
                        //    btnRecallVerification.Visible = true;
                        //}
                    }
                    else
                    {
                        EnableDisable(false);
                        btnRecallVerification.Visible = true;
                        btnforApproval.Visible = true;
                        btnRework.Visible = true;
                        btnforApproval.Enabled = false;
                        btnRework.Enabled = false;
                    }
                    if (UDFLib.ConvertToInteger(GetSessionUserID()) == UDFLib.ConvertToInteger(ds.Tables[0].Rows[0]["SentForApproval"].ToString()))
                    {
                        txtVerificationRemrks.Enabled = true;
                        btnforApproval.Visible = true;
                        btnRework.Visible = true;
                        ddlApproverName.Enabled = false;
                        btnRecallApproval.Visible = false;
                        btnforApproval.Enabled = true;
                        btnRework.Enabled = true;
                       
                    }
                    else
                    {
                        btnforApproval.Visible = true;
                        btnRework.Visible = true;
                        btnforApproval.Enabled = false;
                        btnRework.Enabled = false;
                    }
                  
                }
                else if (ds.Tables[0].Rows[0]["Evaluation_Status"].ToString() == "FinalApproval")
                {
                    btnDelete.Visible = false;
                    btnSaveEvalution.Visible = false;
                    btnVerify.Visible = false;
                    btnRecallVerification.Visible = false;
                    if (UDFLib.ConvertToInteger(GetSessionUserID()) == UDFLib.ConvertToInteger(ds.Tables[0].Rows[0]["Created_By"].ToString()))
                    {
                        EnableDisable(false);
                    }
                    else
                    {
                        EnableDisable(false);
                        btnRecallApproval.Visible = false;
                    }
                    if (UDFLib.ConvertToInteger(GetSessionUserID()) == UDFLib.ConvertToInteger(ds.Tables[0].Rows[0]["SentForApproval"].ToString()))
                    {
                        EnableDisable(false);
                        btnRecallApproval.Visible = true;
                        btnforApproval.Visible = false;
                        btnRework.Visible = false;
                    }
                    else
                    {
                        btnApproval.Visible = true;
                        btnRejected.Visible = true;
                        btnApproval.Enabled = false;
                        btnRejected.Enabled = false;
                    }
                    if (UDFLib.ConvertToInteger(GetSessionUserID()) == UDFLib.ConvertToInteger(ds.Tables[0].Rows[0]["SentForfinalApproval"].ToString()))
                    {
                        //EnableDisable(true);
                        txtApprovalRemrks.Enabled = true;
                        btnApproval.Visible = true;
                        btnApproval.Enabled = true;
                        btnRejected.Enabled = true;
                        btnRejected.Visible = true;
                        //btnRecallApproval.Visible = false;
                    }
                    else
                    {
                        btnApproval.Visible = true;
                        btnRejected.Visible = true;
                        btnApproval.Enabled = false;
                        btnRejected.Enabled = false;
                    }
                }


            }
            else
            {
                ClearControl();

            }
            if (ds.Tables[1].Rows.Count > 0)
            {
                dvEvalProgress.Visible = true;
                gvEvalProgress.DataSource = ds.Tables[1];
                gvEvalProgress.DataBind();
            }
            else
            {
                dvEvalProgress.Visible = false;
                gvEvalProgress.DataSource = ds.Tables[1];
                gvEvalProgress.DataBind();
            }
        }
        catch { }
            {
            }
    }
    private void Visibelbutton(bool blnFlag)
    {
        btnSaveEvalution.Visible = true;
        btnVerify.Visible = true;
        btnRecallVerification.Visible = false;
        btnforApproval.Visible = false;
        btnRework.Visible = false;
        btnRecallApproval.Visible = false;
        btnRejected.Visible = false;
        btnApproval.Visible = false;
        btnDelete.Visible = false;
    }
    private void EnableDisable(bool blnFlag)
    {
        ddlProposedStatus.Enabled = blnFlag;
        ddlforPeriod.Enabled = blnFlag;
        txtSupplierDesc.Enabled = blnFlag;
        txtRemarks.Enabled = blnFlag;
        txtVerificationRemrks.Enabled = blnFlag;
        txtApprovalRemrks.Enabled = blnFlag;
        ddlFinalApproverName.Enabled = blnFlag;
        ddlApproverName.Enabled = blnFlag;
        lblEvalStatus.Enabled = blnFlag;
        lblCreatedby.Enabled = blnFlag;
        ddlPort.Enabled = blnFlag;
        ddlScope.Enabled = blnFlag;
        btnScopeAdd.Enabled = blnFlag;
        btnPortAdd.Enabled = blnFlag;
        btnScopeRemove.Enabled = blnFlag;
        btnPortRemove.Enabled = blnFlag;
        chkScope.Enabled = blnFlag;
        chkPort.Enabled = blnFlag;
        chkUrgent.Enabled = blnFlag;
    }
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }
    protected void BindProposeStatus()
    {
        ddlProposedStatus.DataSource = null;
        DataTable dt = BLL_ASL_Supplier.Get_ASL_System_Parameter(22, "", UDFLib.ConvertToInteger(GetSessionUserID()));

        ddlProposedStatus.DataSource = dt;
        ddlProposedStatus.DataTextField = "Name";
        ddlProposedStatus.DataValueField = "Name";
        ddlProposedStatus.DataBind();
    }
    #endregion

    #region Button Click
    //Button click Save AS Draft Mode 
    protected void btnSaveEvalution_Click(object sender, EventArgs e)
    {
        SaveEvaluation("DRAFT");
        //SendEmail("DRAFT");
        string message = "alert('Evaluation Record Saved as a Draft.')";
        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        //BindSupplier();
        string msgDraft = String.Format("parent.ReloadParent_ByButtonID();");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgDraft", msgDraft, true);

    }
   
    /// <summary>
    /// Send For Final Approval
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>

    protected void btnforApproval_Click(object sender, EventArgs e)
    {
        btnforApproval.Enabled = false;
        string ApproveStatus = "Approved";
        string RejectStatus = "Rejected";
        string Approve = DMS.DES_Encrypt_Decrypt.Encrypt("i=" + ViewState["ReturnEvalutionID"].ToString() + "&j=" + ddlFinalApproverName.SelectedValue + "&k=" + ApproveStatus + "");
        string Reject = DMS.DES_Encrypt_Decrypt.Encrypt("i=" + ViewState["ReturnEvalutionID"].ToString() + "&j=" + ddlFinalApproverName.SelectedValue + "&k=" + RejectStatus + "");

        SaveFinalEvaluation("FinalApproval", Approve, Reject);
        SendEmail("FinalApproval", Approve, Reject);
        string message = "alert('Evaluation Record Send for Final Approval.')";
        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        string msgDraft = String.Format("parent.ReloadParent_ByButtonID();");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgDraft", msgDraft, true);
        btnforApproval.Enabled = true;
    }

    //Verify and Send For Approval
    protected void btnVerify_Click(object sender, EventArgs e)
    {
        btnVerify.Enabled = false;
        SaveEvaluation("Approval");
        SendEmail("Approval", null, null);
        string message = "alert('Evaluation Record Send for Approval.')";
        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        //BindSupplier();
        // Response.Redirect("../ASL/ASL_Evalution.aspx?Supp_ID=" + UDFLib.ConvertStringToNull(Request.QueryString["Supp_ID"].ToString()) + "&ID=" + str + "'");
        string msgDraft = String.Format("parent.ReloadParent_ByButtonID();");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgDraft", msgDraft, true);
        btnVerify.Enabled = true;
    }
    //Approved
    protected void btnApproval_Click(object sender, EventArgs e)
    {
        btnApproval.Enabled = false;
        ApproveEvaluation("Approved");
        SendEmail("Approved", null, null);
        string message = "alert('Evaluation Record has been Approved.')";
        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        string msgDraft = String.Format("parent.ReloadParent_ByButtonID();");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgDraft", msgDraft, true);
        btnApproval.Enabled = true;
    }
    //Rework
    protected void btnRework_Click(object sender, EventArgs e)
    {
        btnRework.Enabled = false;
        ApproveEvaluation("Rework");
        SendEmail("Rework", null, null);
        string message = "alert('Evaluation Record send for Rework.')";
        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        string msgDraft = String.Format("parent.ReloadParent_ByButtonID();");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgDraft", msgDraft, true);
        btnRework.Enabled = true;
    }
    //Reject
    protected void btnRejected_Click(object sender, EventArgs e)
    {
        btnRejected.Enabled = false;
        ApproveEvaluation("Rejected");
        SendEmail("Rejected",null,null);
        string message = "alert('Evaluation Record has been Rejected.')";
        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        string msgDraft = String.Format("parent.ReloadParent_ByButtonID();");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgDraft", msgDraft, true);
        btnRejected.Enabled = true;
    }
    //Recall Verification
    protected void btnRecallVerification_Click(object sender, EventArgs e)
    {
        btnRecallVerification.Enabled = false;
        SaveEvaluation("DRAFT");
        SendEmail("RecallApproval", null, null);
        string message = "alert('Evaluation Record has been recalled from Approval.')";
        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        string msgDraft = String.Format("parent.ReloadParent_ByButtonID();");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgDraft", msgDraft, true);
        btnRecallVerification.Enabled = true;

    }
    //Recall Approval
    protected void btnRecallApproval_Click(object sender, EventArgs e)
    {
        btnRecallApproval.Enabled = false;
        SaveEvaluation("Approval");
        SendEmail("RecallFinalApproval", null, null);
        //BindSupplier();
        string message = "alert('Evaluation Record has been recalled from Final Approval.')";
        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        string msgDraft = String.Format("parent.ReloadParent_ByButtonID();");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgDraft", msgDraft, true);
        btnRecallApproval.Enabled = true;

    }
    //Delete
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            int RetValue = BLL_ASL_Supplier.Evaluation_Data_Delete(UDFLib.ConvertIntegerToNull(ViewState["ReturnEvalutionID"].ToString()), UDFLib.ConvertStringToNull(Request.QueryString["Supp_ID"].ToString()), UDFLib.ConvertToInteger(GetSessionUserID()));
            string message = "alert('Evaluation Record Deleted.')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            //BindSupplier();
            string msgDraft = String.Format("parent.ReloadParent_ByButtonID();");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgDraft", msgDraft, true);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    protected void btnRegisteredData_Click(object sender, EventArgs e)
    {
        string SupplierID = GetSuppID();
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "javascript:window.open('ASL_Data_Entry.aspx?Supp_ID=" + SupplierID + "');", true);
    }


    protected void btnAdd_Click(object sender, EventArgs e)
    {
        DataTable dt = (DataTable)Session["dtEvalScope"];
        DataRow dr = dt.NewRow();


        dr["Scope_ID"] = ddlScope.SelectedValue;
        dr["Scope_Name"] = ddlScope.SelectedItem.Text;
        dt.Rows.Add(dr);

        ddlScope.Items.RemoveAt(ddlScope.SelectedIndex);

        Session["dtEvalScope"] = dt;
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
        DataTable dt = (DataTable)Session["dtEvalScope"];
        BindScope();

        dt.Clear();
        Session["dt"] = dt;
        chkScope.DataSource = dt;

        chkScope.DataBind();

        btnScopeRemove.Visible = false;
    }
    protected void btnPortAdd_Click(object sender, EventArgs e)
    {
        DataTable dt = (DataTable)Session["dtEvalPort"];
        DataRow dr = dt.NewRow();


        dr["PORT_ID"] = ddlPort.SelectedValue;
        dr["PORT_NAME"] = ddlPort.SelectedItem.Text;
        dt.Rows.Add(dr);

        ddlPort.Items.RemoveAt(ddlPort.SelectedIndex);

        Session["dtEvalPort"] = dt;
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
        DataTable dt = (DataTable)Session["dtEvalPort"];
        BindPort();

        dt.Clear();
        Session["dt"] = dt;
        chkPort.DataSource = dt;

        chkPort.DataBind();

        btnPortRemove.Visible = false;
    }

    #endregion



    #region Function
    //Save Evaluation AS Draft
    protected void SaveEvaluation(string EvaluationStatus)
    {
        try
        {
            string str1 = "";
            if (chkUrgent.Checked == true)
            {str1 = "Yes";}
            else{str1 = "No";}
            int RetValue = BLL_ASL_Supplier.Evaluation_Data_Insert(UDFLib.ConvertIntegerToNull(ViewState["ReturnEvalutionID"].ToString())
               , UDFLib.ConvertStringToNull(GetSuppID()), EvaluationStatus, UDFLib.ConvertStringToNull(ddlProposedStatus.SelectedItem),
               UDFLib.ConvertIntegerToNull(ddlforPeriod.SelectedValue), txtRemarks.Text, txtVerificationRemrks.Text, txtApprovalRemrks.Text,
               UDFLib.ConvertToInteger(GetSessionUserID()), UDFLib.ConvertToInteger(ddlApproverName.SelectedValue), UDFLib.ConvertToInteger(ddlFinalApproverName.SelectedValue), str1, null, null, txtSupplierDesc.Text.Trim());

            ViewState["ReturnEvalutionID"] = RetValue;
            SaveSupplierScope();
            SaveSupplierPort();
           
        }
        catch { }
        {

        }
    }
    //Approve Evaluation
    protected void ApproveEvaluation(string EvaluationStatus)
    {
        try
        {
            string str1 = "";
            if (chkUrgent.Checked == true)
            {str1 = "Yes";}else{str1 = "No";}
            int RetValue = BLL_ASL_Supplier.Evaluation_Data_Insert(UDFLib.ConvertIntegerToNull(ViewState["ReturnEvalutionID"].ToString())
               , UDFLib.ConvertStringToNull(Request.QueryString["Supp_ID"].ToString()), EvaluationStatus, UDFLib.ConvertStringToNull(ddlProposedStatus.SelectedItem),
               UDFLib.ConvertIntegerToNull(ddlforPeriod.SelectedValue), txtRemarks.Text, txtVerificationRemrks.Text, txtApprovalRemrks.Text,
               UDFLib.ConvertToInteger(GetSessionUserID()), UDFLib.ConvertToInteger(ddlApproverName.SelectedValue), UDFLib.ConvertToInteger(ddlFinalApproverName.SelectedValue), str1, null, null, txtSupplierDesc.Text.Trim());
            SaveSupplierScope();
            SaveSupplierPort();
          
        }
        catch { }
        {

        }
    }
    //Final Approval Funcation
    protected void SaveFinalEvaluation(string EvaluationStatus, string Approve, string Reject)
    {
        try
        {
            string str1 = "";
            if (chkUrgent.Checked == true)
            { str1 = "Yes"; }
            else { str1 = "No"; }

            int RetValue = BLL_ASL_Supplier.Evaluation_Data_Insert(UDFLib.ConvertIntegerToNull(ViewState["ReturnEvalutionID"].ToString())
               , UDFLib.ConvertStringToNull(GetSuppID()), EvaluationStatus, UDFLib.ConvertStringToNull(ddlProposedStatus.SelectedItem),
               UDFLib.ConvertIntegerToNull(ddlforPeriod.SelectedValue), txtRemarks.Text, txtVerificationRemrks.Text, txtApprovalRemrks.Text,
               UDFLib.ConvertToInteger(GetSessionUserID()), UDFLib.ConvertToInteger(ddlApproverName.SelectedValue), UDFLib.ConvertToInteger(ddlFinalApproverName.SelectedValue), str1, Approve, Reject, txtSupplierDesc.Text.Trim());

            ViewState["ReturnEvalutionID"] = RetValue;
            SaveSupplierScope();
            SaveSupplierPort();


        }
        catch { }
        {

        }
    }

    //Send Mail
    protected void SendEmail(string Evaluationstatus, string Approve, string Reject)
    {
        try
        {
            int RetValue = BLL_ASL_Supplier.Evaluation_Send_Email(UDFLib.ConvertIntegerToNull(ViewState["ReturnEvalutionID"].ToString()), UDFLib.ConvertStringToNull(GetSuppID()), Evaluationstatus,
                         UDFLib.ConvertIntegerToNull(GetSessionUserID()), Approve, Reject);
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    protected void BindScope()
    {
        DataSet ds = BLL_ASL_Supplier.Get_Supplier_Scope(UDFLib.ConvertStringToNull(GetSuppID()));
        //Session["dtEvalScope"] = dt;
        ddlScope.DataSource = ds.Tables[0];
        ddlScope.DataTextField = "Scope_Name";
        ddlScope.DataValueField = "Scope_ID";
        ddlScope.DataBind();
        chkScope.DataSource = ds.Tables[1];
        chkScope.DataTextField = "Scope_Name";
        chkScope.DataValueField = "Scope_ID";
        chkScope.DataBind();
        Session["dtEvalScope"] = ds.Tables[1];
        int i = 0;
        foreach (ListItem chkitem in chkScope.Items)
        {
            chkitem.Selected = true;
            i++;
        }

    }
    protected void BindPort()
    {

        DataSet ds = BLL_ASL_Supplier.Get_Supplier_PORT(UDFLib.ConvertStringToNull(GetSuppID()));
        //Session["dtEvalPort"] = dt;
        ddlPort.DataSource = ds.Tables[0];
        ddlPort.DataTextField = "PORT_NAME";
        ddlPort.DataValueField = "PORT_ID";
        ddlPort.DataBind();
        chkPort.DataSource = ds.Tables[1];
        chkPort.DataTextField = "PORT_NAME";
        chkPort.DataValueField = "PORT_ID";
        chkPort.DataBind();

        Session["dtEvalPort"] = ds.Tables[1];
        int i = 0;
        foreach (ListItem chkitem in chkPort.Items)
        {
            chkitem.Selected = true;

            i++;
        }
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
        BLL_ASL_Supplier.ASL_Supplier_Scope_Insert(UDFLib.ConvertStringToNull(UDFLib.ConvertStringToNull(Request.QueryString["Supp_ID"].ToString())), dtScope, UDFLib.ConvertToInteger(GetSessionUserID()));
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
        BLL_ASL_Supplier.ASL_Supplier_Port_Insert(UDFLib.ConvertStringToNull(UDFLib.ConvertStringToNull(Request.QueryString["Supp_ID"].ToString())), dtService, UDFLib.ConvertToInteger(GetSessionUserID()));
    }
    #endregion

    #region control function
    protected void gvEvalProgress_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblPropose_Status = (Label)e.Row.FindControl("lblPropose_Status");
            string str = lblPropose_Status.Text;
            if (str == "Approval")
            {
                e.Row.Cells[3].BackColor = System.Drawing.Color.Green;
                lblPropose_Status.ForeColor = System.Drawing.Color.White;
            }
            string ID = DataBinder.Eval(e.Row.DataItem, "EvalID").ToString();
            if (ID == txtEvalID.Text)
            {
                e.Row.BackColor = System.Drawing.Color.Yellow;
            }
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
    public int GetEvalID()
    {
        try
        {
            if (Session["EvalID"] != null)
            {
                return int.Parse(Session["EvalID"].ToString());
            }

            else
                return 0;
        }
        catch { return 0; }
    }
    public int GetEvaluation_ID()
    {
        try
        {
            if (Request.QueryString["Evaluation_ID"] != null)
            {
                return int.Parse(Request.QueryString["Evaluation_ID"]);
            }

            else
                return 0;
        }
        catch { return 0; }
    }
    private string GetSessionSupplierType()
    {
        if (Session["Supplier_Type"] != null)
            return Session["Supplier_Type"].ToString();
        else
            return null;
    }
    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
        {
            pnlEvaluation.Visible = false;
            lblMsg.Text = "You don't have sufficient previlege to access the requested information.";
        }
        else
        {
            pnlEvaluation.Visible = true;
        }

        if (objUA.Add == 0)
        {
            btnSaveEvalution.Enabled = false;
            btnVerify.Enabled = false;
        }
        if (objUA.Approve == 1)
        {
            btnforApproval.Enabled = true;
            btnRejected.Enabled = true;
            btnRework.Enabled = true;
            btnApproval.Enabled = true;
        }
        else
        {
            btnforApproval.Enabled = false;
            btnRejected.Enabled = false;
            btnRework.Enabled = false;
            btnApproval.Enabled = false;
        }
        if (objUA.Edit == 1)
            uaEditFlag = true;
        if (objUA.Delete == 1)
        {
            uaDeleteFlage = true;
        }
        else
        {
            btnDelete.Visible = false;
        }

    }
    protected void UserAccessTypeValidation(string Supplier_Type)
    {
        int CurrentUserID = GetSessionUserID();
        //string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        BLL_TypeManagement objType = new BLL_TypeManagement();
        string Variable_Type = "Supplier_Type";
        string Approver_Type = "Evaluation";
        objUAType = objType.Get_UserTypeAccess(CurrentUserID, Variable_Type, Supplier_Type, Approver_Type);


        if (objUAType.Add == 0)
        {
            btnSaveEvalution.Enabled = false;
            btnVerify.Enabled = false;
        }
        if (objUAType.Approve == 1)
        {
            btnforApproval.Enabled = true;
            btnRejected.Enabled = true;
            btnRework.Enabled = true;
            btnApproval.Enabled = true;
        }
        else
        {
            btnforApproval.Enabled = false;
            btnRejected.Enabled = false;
            btnRework.Enabled = false;
            btnApproval.Enabled = false;
        }
        if (objUAType.Delete == 1)
        {
            uaDeleteFlage = true;
            //btnDelete.Visible = true;
        }
        else
        {
            btnDelete.Visible = false;
        }

    }
    
    protected void ClearControl()
    {
        ViewState["ReturnEvalutionID"] = 0;
        ddlFinalApproverName.SelectedValue = "0";
        ddlApproverName.SelectedValue = "0";
        chkUrgent.Checked = false;
        ddlforPeriod.SelectedValue = "30";
        //ddlProposedStatus.SelectedValue = "23";
        btnSaveEvalution.Visible = true;
        btnVerify.Visible = true;
        txtVerificationRemrks.Text = "";
        txtRemarks.Text = "";
        txtApprovalRemrks.Text = "";
        lblEvalStatus.Text = "";
        lblCreatedby.Text = "";
        btnforApproval.Visible = false;
        btnDelete.Visible = false;
        btnRejected.Visible = false;
        btnRework.Visible = false;
        //btnVerify.Visible = false;
        btnApproval.Visible = false;
        btnRecallVerification.Visible = false;
        btnRecallApproval.Visible = false;
        txtRemarks.Enabled = true;

    }
    #endregion

    protected void BindRemarksGrid()
    {
        string SuppID = UDFLib.ConvertStringToNull(Request.QueryString["Supp_ID"]);
        //int? SuppID = 2737;
        DataTable dt = BLL_ASL_Supplier.Get_Supplier_Remarks(SuppID);
        gvRemarks.DataSource = dt;
        gvRemarks.DataBind();

    }
    protected void btnRemarks_Click(object sender, EventArgs e)
    {
        if (btnRemarks.Text == "Supplier Remarks")
        {
            divRemarks.Visible = true;
            BindRemarksGrid();
            btnRemarks.Text = "Hide Supplier Remarks";
        }
        else
        {
            divRemarks.Visible = false;
            btnRemarks.Text = "Supplier Remarks";
        }
    }
 
    protected void lbtnDelete_Click(object s, CommandEventArgs e)
    {
        string[] arg = e.CommandArgument.ToString().Split(',');
        int? RemarksID = UDFLib.ConvertIntegerToNull(arg[0]);

        int RetValue = BLL_ASL_Supplier.Delete_Supplier_Remarks(RemarksID, UDFLib.ConvertToInteger(Session["UserID"].ToString()));
        BindRemarksGrid();

    }
   
    protected void gvRemarks_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ImageButton EditButton = (ImageButton)e.Row.FindControl("lbtnEdit");
            ImageButton DeleteButton = (ImageButton)e.Row.FindControl("lbtnDelete");

            ImageButton AmendButton = (ImageButton)e.Row.FindControl("ImgAmend");
            ImageButton GeneralButton = (ImageButton)e.Row.FindControl("imgGeneral");
            ImageButton WarningButton = (ImageButton)e.Row.FindControl("imgWarning");
            ImageButton RedButton = (ImageButton)e.Row.FindControl("imageRed");
            Label lblRemarks = (Label)e.Row.FindControl("lblRemarks");

            string Remarks = DataBinder.Eval(e.Row.DataItem, "REMRAKS").ToString();
            Remarks = Remarks.Replace("\n", "<br />");
            lblRemarks.Text = Remarks;

            if (DataBinder.Eval(e.Row.DataItem, "GeneralType").ToString() != "")
            {
                GeneralButton.Visible = true;
            }
            if (DataBinder.Eval(e.Row.DataItem, "AmendType").ToString() != "")
            {
                AmendButton.Visible = true;
            }
            if (DataBinder.Eval(e.Row.DataItem, "YellowType").ToString() != "")
            {
                WarningButton.Visible = true;
            }
            if (DataBinder.Eval(e.Row.DataItem, "RedType").ToString() != "")
            {
                RedButton.Visible = true;
            }
            //if (DataBinder.Eval(e.Row.DataItem, "RType").ToString() == "PO")
            //{
                //EditButton.Visible = false;
                //DeleteButton.Visible = false;
            //}
        }
    }

    
    protected void SearchBindGrid()
    {
        try
        {
           int Lastyear = 0;
            int year = 0;
            string SupplierID = GetSuppID();
            DataSet ds = BLL_ASL_Supplier.Get_Supplier_Statistics(UDFLib.ConvertStringToNull(SupplierID));
            if (ds.Tables[1].Rows.Count > 0)
            {
                //gvStatistics.DataSource = ds.Tables[0];
                //gvStatistics.DataBind();
                year = Convert.ToInt32(DateTime.Now.Year.ToString());
                Lastyear = Convert.ToInt16(ds.Tables[2].Rows[0]["Lastyear"].ToString());

                StringBuilder sb = new StringBuilder();
                sb.Append("<table Style=\"border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; color: Black;\" Cellpadding=5 Cellspacing=1 \">");
                sb.Append("<tr class='HeaderStyle-css' Style=\"Font-Size:14px;font-weight:bold;\" Bgcolor=\"E8E8E8\">" + "<td Style=\"Text-Align:center;\" Colspan=11>Statistics</td>");
                for (int J = Lastyear; J <= year; J++)
                {
                    sb.Append("<td Style=\"Text-Align:Center;\"  Colspan=11>" + J + "</td>");
                }
                sb.Append("<td Colspan=11>Total Value</td>");
                sb.Append("</tr>");

                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    sb.Append("<tr class='AlternatingRowStyle-css' Style=\"Font-Size:11px;\">");
                    for (int j = 1; j < ds.Tables[1].Columns.Count; j++)
                    {
                        sb.Append("<td Style=\"Text-Align:Right;\" Colspan=11>" + ds.Tables[1].Rows[i][j].ToString() + "</td>");
                    }

                    sb.Append("</tr>");
                }
                sb.Append("</table>");
                ltSupplierStatistics.Text = sb.ToString();

            }
            
        }
        catch { }
        {
        }

    }
    protected void btnStatistics_Click(object sender, EventArgs e)
    {
        if (btnStatistics.Text == "Statistics")
        {
            divStatistics.Visible = true;
            SearchBindGrid();
            btnStatistics.Text = "Hide Statistics";
        }
        else
        {
            divStatistics.Visible = false;
            btnStatistics.Text = "Statistics";
        }
    }
}