using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.PURC;
using Telerik.Web.UI;
using ClsBLLTechnical;
using System.IO;
using System.Net;
using System.Collections;
using System.Collections.Generic;
using SMS.Business.Crew;
using SMS.Business.Infrastructure;
using System.Web;

public partial class Purchase_Quotation_Evaluation_Gridview : System.Web.UI.Page
{
    MergeGridviewHeader_Info objApprovalHist = new MergeGridviewHeader_Info();
    BLL_PURC_Purchase objHistory = new BLL_PURC_Purchase();
    string Requisitioncode = string.Empty;
    string Vessel_Code = string.Empty;
    string Document_Code = string.Empty;
    string User = string.Empty;
    public static Dictionary<string, string> dicTotalAmount = new Dictionary<string, string>();

    protected void Page_Load(object sender, EventArgs e)
    {
        Requisitioncode = Convert.ToString(Request.QueryString["Requisitioncode"]);
        Vessel_Code = Convert.ToString(Request.QueryString["Vessel_Code"]);
        Document_Code = Convert.ToString(Request.QueryString["Document_Code"]);
        User = Convert.ToString(Session["userid"]);
        if (!IsPostBack)
        {
            BindRequisitionInfo();

            gvApprovalHistory.DataSource = objHistory.Get_Approver_History(Requisitioncode);
            gvApprovalHistory.DataBind();
            hdfUserID.Value = User;
            btn_GetRemarks.Attributes.Add("onclick", "javascript:GetRemarkAll('" + Document_Code + "','" + User + "',null,event);return false;");
            rpAttachment.DataSource = objHistory.Purc_Get_Reqsn_Attachments(Requisitioncode, Convert.ToInt32(Vessel_Code));
            rpAttachment.DataBind();
            rpAttachment.DataSource = objHistory.Purc_Get_Reqsn_Attachments(Requisitioncode, Convert.ToInt32(Vessel_Code));
            rpAttachment.DataBind();
        }
    }

    //-- Events
    /// <summary>
    /// checks for the selected Items from the Quotation details Grid and Saves the quotation with the Seleclted items on "Save Quotation Evaluation" Button 
    /// else Gives an alert  to Select The Item To Save The Quotation
    /// </summary>
    protected void btnSaveEvaln_Click(object sender, EventArgs e)                                 //-- SaveApproved PO Quotation
    {
        int check = 0;
        String msg = string.Empty;
        using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
        {
            CheckBox Chk = new CheckBox();
            HiddenField hd = new HiddenField();
            foreach (GridViewRow gr in Grid_RqsnDtl.Rows)
            {
                Chk = (CheckBox)gr.FindControl("Chk_Item");

                if (Chk.Checked)
                {
                    check++;
                    hd = (HiddenField)gr.FindControl("HD_ITEMREF");
                    Label lbl_Qnty = (Label)gr.FindControl("lbl_Qnty");
                    objTechService.Save_Approved_PO(Convert.ToInt32(User), Document_Code, Convert.ToInt32(lbl_Qnty.Text.Split('.')[0]), hd.Value.ToString());
                }
            }

        }
        if (check > 0)
        {
            msg = String.Format("alert('Saved successfully');");
        }
        else { msg = String.Format("alert('Select The Item To Save The Quotation');"); }
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg, true);
    }  
    /// <summary>
    /// If Header Checkbox is Checked Will Check All the Checkbox in the Grid Else allow Selected Checks on gridItem Checkbox Click
    /// </summary>     
    protected void HeaderCheckChange(object sender, EventArgs e)                                  //-- Check All if header Checked and Vise-Versa
    {
        CheckBox Header_Checkbox = (CheckBox)sender;
        foreach (GridViewRow gvrow in Grid_RqsnDtl.Rows)
        {
            CheckBox Item_CheckBox = (CheckBox)gvrow.FindControl("Chk_Item");
            Item_CheckBox.Checked = Header_Checkbox.Checked;
        }
    }
    /// <summary>
    /// Bind ApprovalHistory Gridview
    /// </summary>
    protected void gvApprovalHistory_DataBound(object sender, EventArgs e)                        // -- DataBound  ApprovalHistory Gridview 
    {
        string HeaderToHide = "";
        string HeaderSupplier = "";

        var grh = gvApprovalHistory.HeaderRow;
        for (int i = 0; i < gvApprovalHistory.HeaderRow.Cells.Count; i++)
        {

            string Suppname = grh.Cells[i].Text.ToLower();
            if (Suppname.Contains("_hide"))
            {
                HeaderToHide += i.ToString() + ",";
                HeaderSupplier += grh.Cells[i].Text.Split(new char[] { '_' })[0] + ",";

                gvApprovalHistory.HeaderRow.Cells[i].Visible = false;
                foreach (GridViewRow gr in gvApprovalHistory.Rows)
                {
                    gr.Cells[i].Visible = false;
                }
            }
            if (Suppname.Contains("_amount"))
            {
                gvApprovalHistory.HeaderRow.Cells[i].Text = "Amount";
                gvApprovalHistory.HeaderRow.Cells[i].CssClass = "HeaderStyle-css";
                foreach (GridViewRow gr in gvApprovalHistory.Rows)
                {
                    gr.Cells[i].CssClass = "amount-css";
                    gr.Cells[0].CssClass = "text-css";
                    gr.Cells[gr.Cells.Count - 1].CssClass = "text-css";
                }

            }
            if (Suppname.Contains("_currency"))
            {
                gvApprovalHistory.HeaderRow.Cells[i].Text = "Currency";
                gvApprovalHistory.HeaderRow.Cells[i].CssClass = "HeaderStyle-css";
            }
        }


        string[] strColumnIndex = HeaderToHide.Split(new char[] { ',' });
        string[] strHeaderName = HeaderSupplier.Split(new char[] { ',' });
        int index = 0;
        foreach (string item in strColumnIndex)
        {
            if (item != "")
            {

                objApprovalHist.AddMergedColumns(new int[] { int.Parse(item) + 1, int.Parse(item) + 2 }, strHeaderName[index]);
            }
            index++;
        }

    }
  
    protected void gvApprovalHistory_RowCreated(object sender, GridViewRowEventArgs e)            //call the method for custom rendering the columns headers	on row created event
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            MergeGridviewHeader.SetProperty(objApprovalHist);

            e.Row.SetRenderMethodDelegate(MergeGridviewHeader.RenderHeader);

            ViewState["DynamicHeaderCSS"] = "HeaderStyle-css";
        }

    }
    protected void ddlReqsnType_SelectedIndexChanged(object s, EventArgs e)                   //-- call Fill_Budget() on ddlReqsnType dropdownlist change
    {
        Fill_Budget();

    }                    
    
    /// <summary>
    /// Request for Budget Limit Increase 
    /// </summary>
    protected void btnRequestAmount_Click(object sender, EventArgs e)
    {
        string OCA_URL = null;
        if (ddlBudgetCode.SelectedIndex > 0)
        {
            if (!Request.Url.AbsoluteUri.Contains(ConfigurationManager.AppSettings["OCA_APP_URL"]))
            {
                OCA_URL = ConfigurationManager.AppSettings["OCA_APP_URL"];
            }
            string OCA_URL1 = OCA_URL + "/Acct/Vessel_Budget_Utilization_Report.asp?Search_budget_Code=" + ddlBudgetCode.SelectedValue + "";

            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "javascript:window.open('" + OCA_URL1 + "');", true);
        }
        else
        {
            String msg = String.Format("alert('Please select Budget Code !');");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg, true);
        }
    }
    /// <summary>
    /// Approve PO , open POP-UP "divApprove" 
    /// </summary>
    protected void ApprovePO_Onclick(object sender, EventArgs e)                                 
    {
        try
        {

            lblErrorMsg.Text = "";
            decimal maxQuotaed_Amount = 0;
            hdfMaxQuotedAmount.Value = "0";
            hdfOrderAmounts.Value = "0";

            // check the validity of suppliers

            string SElectedSupplierToApprove = hdfSupplierBeingApproved.Value;
            string suppliers = "";
            string supplier_code = hdfSupplierBeingApproved.Value;
            decimal Supp_Tot_Amt = UDFLib.ConvertToDecimal(lbl_TotalAmount.Text);
            suppliers += " select '" + supplier_code + "' union"; //get the ASL_Status_Valid_till date for all supplier 

            if (SElectedSupplierToApprove.Contains(supplier_code)) // get the max quotation amount among quoted suppliers and those are under comparison section
            {
                if (Supp_Tot_Amt > maxQuotaed_Amount)
                {
                    maxQuotaed_Amount = Supp_Tot_Amt;
                }
            }

            //approved amount will includes existing POs's amount and current quotation's final amount(based on items selection) 
            if (Supp_Tot_Amt > 0)
            {
                hdfOrderAmounts.Value = (UDFLib.ConvertToDecimal(hdfOrderAmounts.Value) + UDFLib.ConvertToDecimal(lbl_TotalAmount.Text)).ToString();
            }


            // }
            hdfMaxQuotedAmount.Value = maxQuotaed_Amount.ToString();
            suppliers += " select '0'";
            DataTable dtSuppDate = BLL_PURC_Common.Get_Supplier_ValidDate(suppliers);
            string supplierNameNotValid = "";
            if (dtSuppDate.Rows.Count > 0)
            {
                foreach (DataRow dr in dtSuppDate.Rows)
                {
                    if (Convert.ToDateTime(dr["ASL_Status_Valid_till"]) < DateTime.Now)
                    {
                        if (SElectedSupplierToApprove.Contains(dr["SUPPLIER"].ToString()))// if supplier is in comaparison section 
                        {
                            supplierNameNotValid += dr["Full_NAME"].ToString() + ", ";
                        }
                    }
                }
            }

            if (supplierNameNotValid.Length > 0)
            {

                String msg = String.Format("alert('Supplier(s) Expired:  " + supplierNameNotValid + "');");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg45", msg, true);


            }
            else
            {
                TechnicalBAL objtechBAL = new TechnicalBAL();

                ViewState["BGTCODE"] = BLL_PURC_Common.Get_BGTCode_Reqsn(Requisitioncode);

                ddlReqsnType.ClearSelection();
                ListItem listreqstype = ddlReqsnType.Items.FindByValue(Convert.ToString(ViewState["SavedReqsnType"]));
                if (listreqstype != null)
                    listreqstype.Selected = true;
                Fill_Budget();
                String msgretv = String.Format("setTimeout(calculate,1000);");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgonFinal", msgretv, true);

                String msgmodal = String.Format("showModal('divApprove');");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgonFinalmodal", msgmodal, true);

            }

        }
        catch (Exception ex)
        {
            lblErrorMsg.Text = ex.Message + ex.StackTrace;
            UDFLib.WriteExceptionLog(ex);
        }
    }
    /// <summary>
    /// Rework To Supplier 
    /// </summary>
    protected void btnReworkToSupplier_Click(object s, EventArgs e)                               //-- Rework To Supplier
    {
        BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase();
        DataSet dsEmailInfo = new DataSet();
        string Supplier_code = hdfSuppCode.Value;
        dsEmailInfo = objTechService.GetRFQsuppInfoSendEmail(Supplier_code, hdfQTNCode.Value, Vessel_Code, Document_Code, User);

        int RetVal = objTechService.UpdateQuotForRework(Requisitioncode, Vessel_Code, Document_Code, Supplier_code, hdfQTNCode.Value);
        if (RetVal != 0)
        {
            BLL_PURC_Common.INS_Remarks(Document_Code, Convert.ToInt32(User), txtReworkToSupplier.Text, 300);
            string ServerIPAdd = ConfigurationManager.AppSettings["WebQuotSite"].ToString();
        }

    }
    /// <summary>
    /// Show  Pop up of Rework to supply
    /// </summary>
                        
    protected void onRework(object sender, ImageClickEventArgs e)                             
    {
        ImageButton btn = (ImageButton)sender;

        hdfSuppCode.Value = hdfSupplierBeingApproved.Value;

        String msg1 = String.Format("DivReworkSuppShow();");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg1, true);
    }
    /// <summary>
    /// Show Approve Popup
    /// </summary>
    protected void btnApprove_Click(object sender, EventArgs e)                                  
    {
        bool IsFinalAproved = false;
        try
        {
            string QuotationCode = "";
            string SupplierCode = "";

            DataTable dtQuotationList_ForTopApprover = new DataTable();
            dtQuotationList_ForTopApprover.Columns.Add("Qtncode");
            dtQuotationList_ForTopApprover.Columns.Add("amount");

            DataTable dtQuotationList = new DataTable();
            dtQuotationList.Columns.Add("Qtncode");
            dtQuotationList.Columns.Add("amount");

            DataTable dtBudgetCode = new DataTable();
            dtBudgetCode.Columns.Add("Qtncode");
            dtBudgetCode.Columns.Add("amount");
            TechnicalBAL objTechBAL = new TechnicalBAL();

            string[] Attchment = new string[10];


            // check for provision's approval limit for items
            bool isProvisionLimitsts = isProvisionLimitExceeding();


            //Get Approval Amount 

            BLL_PURC_Purchase objApproval = new BLL_PURC_Purchase();
            DataTable dtApproval = objApproval.Get_Approval_Limit(Convert.ToInt32(User), ViewState["Dept_Code"].ToString());
            if (dtApproval.Rows.Count < 1)
            {
                String msgApp = String.Format("alert('Approval limit does not exist for you.Please contact admin.');RefreshPendingDetails();window.close();");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg99011a", msgApp, true);
                //divApprove.Visible = false;
                String msgmodal = String.Format("hideModal('divApprove');");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgonFinalmodalhide", msgmodal, true);
                return;
            }

            decimal dblPOAprLimitAmt = decimal.Parse(dtApproval.Rows[0]["Approval_Limit"].ToString());
            if (dblPOAprLimitAmt < 1)
            {
                String msgApp = String.Format("alert('Approval limit does not exist for you.Please contact admin.');RefreshPendingDetails() ;window.close();");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg660081g", msgApp, true);
                String msgmodal = String.Format("hideModal('divApprove');");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgonFinalmodalhide", msgmodal, true);
                return;
            }

            TechnicalBAL objtechBAL = new TechnicalBAL();
            objtechBAL.Update_ReqsnType(Requisitioncode, UDFLib.ConvertToInteger(ddlReqsnType.SelectedValue), "", UDFLib.ConvertToInteger(User));

            decimal Supp_Total_Amount = 0;
            decimal approvedAmount = 0;
            dicTotalAmount.Clear();

            CheckBox Chk = new CheckBox();
            foreach (GridViewRow gr in Grid_RqsnDtl.Rows)
            {
                Chk = (CheckBox)gr.FindControl("Chk_Item");

                if (Chk.Checked)
                {
                    Label lbl_Qnty = (Label)gr.FindControl("lbl_TotalPrice");
                    approvedAmount = Convert.ToDecimal(lbl_Qnty.Text) + approvedAmount;
                }
            }


            // add the orderedamount from existing POs in approved amount(amount going to be approved)
            approvedAmount = approvedAmount + Convert.ToDecimal(hdfOrderAmounts.Value);

            Supp_Total_Amount = decimal.Parse(hdfMaxQuotedAmount.Value);


            SupplierCode = hdfSupplierBeingApproved.Value;
            QuotationCode = hdfQTNCode.Value;

            BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase();

            string retval = "TRUE";
            if (hdnBudgetCode.Value.ToString() == "1")
            {
                DataRow dtrow = dtBudgetCode.NewRow();
                dtrow[0] = QuotationCode;
                dtrow[1] = dicTotalAmount[QuotationCode].ToString();
                dtBudgetCode.Rows.Add(dtrow);
                retval = objTechService.Check_Update_BudgetCode(Requisitioncode, Vessel_Code, ddlBudgetCode.SelectedValue, dtBudgetCode);
            }

            if (retval == "TRUE")
            {
                if (Supp_Total_Amount > dblPOAprLimitAmt || approvedAmount > dblPOAprLimitAmt)// supplier code is zero in this case
                {
                    objApproval.POApproving(Requisitioncode, QuotationCode, "0", User, "", Vessel_Code, ddlBudgetCode.SelectedValue.ToString());

                    // store the qtn code and supp amount for top approver
                    DataRow dtrow = dtQuotationList_ForTopApprover.NewRow();
                    dtrow[0] = QuotationCode;
                    dtrow[1] = dicTotalAmount[QuotationCode].ToString();
                    dtQuotationList_ForTopApprover.Rows.Add(dtrow);
                }

                else if (Supp_Total_Amount <= dblPOAprLimitAmt && approvedAmount <= dblPOAprLimitAmt && (isProvisionLimitsts == false))//The actual approval 
                {
                    //qtnbased

                    DataRow dtrow = dtQuotationList.NewRow();
                    dtrow[0] = QuotationCode;
                    dtrow[1] = dicTotalAmount[QuotationCode].ToString();
                    dtQuotationList.Rows.Add(dtrow);

                    /// begin insert the records for final PO
                    DataTable dtReqInfo = new DataTable();
                    dtReqInfo = objTechService.SelectSupplierToSendOrderEval(Requisitioncode, Vessel_Code, QuotationCode);
                    SavePurchasedOrder(dtReqInfo.DefaultView.ToTable());

                    objTechService.POApproving(Requisitioncode, QuotationCode, SupplierCode, User, txtComment.Text, Vessel_Code, ddlBudgetCode.SelectedValue.ToString());


                    IsFinalAproved = true;

                }
            }
            else
            {
                //btnRequestAmount.Visible = true;
                String msg1 = String.Format("alert('A.Total Approval amount is greater than Budget limit,Please request for increase Budget limit.');");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg1, true);
                String msgmodal = String.Format("showModal('divApprove');");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgonFinalmodal", msgmodal, true);
            }
            //}
            // }

            if (IsFinalAproved)
            {
                //Requisition stage status update
                // BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase();
                //Check Budget Code and Update
                retval = "TRUE";
                if (hdnBudgetCode.Value.ToString() == "1")
                {
                    retval = objTechService.Check_Update_BudgetCode(Requisitioncode, Vessel_Code, ddlBudgetCode.SelectedValue, dtBudgetCode);
                }

                if (retval == "TRUE")
                {
                    //btnRequestAmount.Visible = false;
                    // SAVE APPROVAL
                    objTechBAL.InsertUserApprovalEntries(Requisitioncode, Document_Code, Vessel_Code, User, User, Supp_Total_Amount, approvedAmount, "0", txtComment.Text.Trim(), dtQuotationList);

                    BLL_PURC_Common.INS_Remarks(Document_Code, Convert.ToInt32(User), txtComment.Text.Trim(), 303);
                    objTechService.InsertRequisitionStageStatus(Requisitioncode, Vessel_Code, Document_Code, "RPO", " ", Convert.ToInt32(User), dtQuotationList);

                    String msg1 = String.Format("alert('Approved successfully.'); RefreshPendingDetails(); window.close();");
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg1, true);
                    return;
                }
                else
                {
                    //btnRequestAmount.Visible = true;
                    String msg1 = String.Format("alert('B.Total Approval amount is greater than Budget limit,Please request for increase Budget limit.');");
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg1, true);
                    String msgmodal = String.Format("showModal('divApprove');");
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgonFinalmodal", msgmodal, true);
                }
            }

            else
            {

                if (!isProvisionLimitsts)
                {

                    //check if only one approver is left then send him directly instead of prompting the current user to send and save the current approver's approval.
                    //  BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase();
                    retval = "TRUE";
                    if (hdnBudgetCode.Value.ToString() == "1")
                    {
                        retval = objTechService.Check_Update_BudgetCode(Requisitioncode, Vessel_Code, ddlBudgetCode.SelectedValue, dtBudgetCode);
                    }

                    if (retval == "TRUE")
                    {
                        //btnRequestAmount.Visible = false;
                        int ApproverCount = BLL_PURC_Common.CheckHierarchy_SendForApproval(Requisitioncode, Document_Code, Convert.ToInt32(Vessel_Code), User, Supp_Total_Amount, dblPOAprLimitAmt, dtQuotationList_ForTopApprover);
                        if (ApproverCount == 1)
                        {
                            BLL_PURC_Common.INS_Remarks(Document_Code, Convert.ToInt32(User), txtComment.Text.Trim(), 303);
                            String msg = String.Format("alert('Approved successfully but  total Approval amount is greater than your approval limit ,this Requisition is now being sent to your supirior for his approval.'); RefreshPendingDetails();window.close();");
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg16", msg, true);
                        }
                        else if (ApproverCount > 1)
                        {
                            BLL_PURC_Common.INS_Remarks(Document_Code, Convert.ToInt32(User), txtComment.Text.Trim(), 303);
                            String msg1 = String.Format("alert('Approved successfully but  total Approval amount is greater than your approval limit ,this Requisition is now being sent to your supirior for his approval ');");
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg1, true);
                            ucApprovalUser1.ReqsnCode = Requisitioncode;


                            ucApprovalUser1.FillUser();
                            dvSendForApproval.Visible = true;
                        }
                        else if (ApproverCount == 0)
                        {
                            BLL_PURC_Common.INS_Remarks(Document_Code, Convert.ToInt32(User), txtComment.Text.Trim(), 303);
                            String msg1 = String.Format("alert('Total Approval amount is greater than your approval limit and no approver found for the amount " + Supp_Total_Amount + " . Please contact your manager.' );");
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgCt0", msg1, true);

                        }
                        String msgmoda12l = String.Format("hideModal('divApprove');");
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgonFinalmodalhide", msgmoda12l, true);
                    }
                    else
                    {
                        //btnRequestAmount.Visible = true;
                        String msg1 = String.Format("alert('C.Total Approval amount is greater than Budget limit,Please request for increase Budget limit.');");
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg1, true);
                        String msgmodal = String.Format("showModal('divApprove');");
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgonFinalmodal", msgmodal, true);
                    }
                }
            }



            //divApprove.Visible = false;
            return;
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }




    } 
   /// <summary>
   /// Sent for Approval
   /// </summary>
    public void OnStsSaved(object s, EventArgs e)
    {
        try
        {
            TechnicalBAL objTechBAL = new TechnicalBAL();
            string QuotationCode = "";
            string SupplierCode = "";

            decimal Supp_Total_Amount = 0;
            decimal approvedAmount = 0;
            dicTotalAmount.Clear();

            DataTable dtQuotationList = new DataTable();
            dtQuotationList.Columns.Add("Qtncode");
            dtQuotationList.Columns.Add("amount");

            CheckBox Chk = new CheckBox();
            foreach (GridViewRow gr in Grid_RqsnDtl.Rows)
            {
                Chk = (CheckBox)gr.FindControl("Chk_Item");

                if (Chk.Checked)
                {
                    Label lbl_Qnty = (Label)gr.FindControl("lbl_TotalPrice");
                    approvedAmount = Convert.ToDecimal(lbl_Qnty.Text) + approvedAmount;
                }
            }


            approvedAmount = approvedAmount + Convert.ToDecimal(hdfOrderAmounts.Value);



            SupplierCode = hdfSupplierBeingApproved.Value;
            QuotationCode = hdfQTNCode.Value;


            Supp_Total_Amount = UDFLib.ConvertToDecimal(lbl_TotalAmount.Text);

            DataRow dtrow = dtQuotationList.NewRow();
            dtrow[0] = QuotationCode;
            dtrow[1] = dicTotalAmount[QuotationCode].ToString();
            dtQuotationList.Rows.Add(dtrow);

            // first send for approval 
            int stsEntry = objTechBAL.InsertUserApprovalEntries(Requisitioncode, Document_Code, Vessel_Code, User, ucApprovalUser1.ApproverID, 0, 0, "", ucApprovalUser1.Remark, dtQuotationList);

            // save the approval
            objTechBAL.InsertUserApprovalEntries(Requisitioncode, Document_Code, Vessel_Code, User, User, Supp_Total_Amount, approvedAmount, "0", txtComment.Text.Trim(), dtQuotationList);
            String msg1 = String.Format("alert('Sent successfully.'); RefreshPendingDetails();window.close();");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgSENT", msg1, true);
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }

    }
    protected void OnStsSavedSentToApprover(object s, EventArgs e)
    {
        try
        {

            TechnicalBAL objpurc = new TechnicalBAL();
            TechnicalBAL objTechBAL = new TechnicalBAL();

            //qtnbased
            DataTable dtQuotations = BLL_PURC_Common.PURC_GET_Quotation_ByReqsnCode(Requisitioncode);
            DataTable dtQuotationList = new DataTable();
            dtQuotationList.Columns.Add("Qtncode");
            dtQuotationList.Columns.Add("amount");

            foreach (DataRow dr in dtQuotations.Rows)
            {
                if (dr["active_PO"].ToString() == "0") // for those POs have been cancelled(means active_PO is zero)
                {
                    DataRow dtrow = dtQuotationList.NewRow();
                    dtrow[0] = dr["QUOTATION_CODE"].ToString();
                    dtrow[1] = "0";
                    dtQuotationList.Rows.Add(dtrow);
                }
            }

            // save the requested qty into order qty and order qty column on grid will be bibnded to order qty (change so store the updated qty by supp at the time of eval.) { this functionality has been implemented at rfq send stage}
            // code to update the order qty have been commented ,this is used to save bgt code only.

            BLL_PURC_Common.Update_OrderQty_From_ReqstQty(Requisitioncode, Document_Code, ddlBGTCodeToSuppdt.SelectedValue);

            int stsEntry = objTechBAL.InsertUserApprovalEntries(Requisitioncode, Document_Code, Vessel_Code, User, ucApprovalUserToSuppdt.ApproverID, 0, 0, "", ucApprovalUserToSuppdt.Remark, dtQuotationList);
            if (stsEntry > 0)
            {
                // dtQuotationList is passing but not using in sp ,all quotation will be set as senttosuppdt as true for this reqsn
                int res = objpurc.PURC_Update_SentToSupdt(0, 0, Requisitioncode, int.Parse(User), ucApprovalUserToSuppdt.Remark, dtQuotationList);

                if (res > 0)
                {
                    BLL_PURC_Purchase objPurc = new BLL_PURC_Purchase();
                    //Requisition stage status update
                    objPurc.InsertRequisitionStageStatus(Requisitioncode, Vessel_Code, Document_Code, "QEV", " ", Convert.ToInt32(User), dtQuotationList);
                    BLL_PURC_Common.INS_Remarks(Document_Code, Convert.ToInt32(User), ucApprovalUserToSuppdt.Remark, 302);

                    String msg = String.Format("alert('Sent successfully'); RefreshPendingDetails();window.close();");
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg, true);
                }
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }


    //Functions
    /// <summary>
    /// gets the requisition Data and Binds the Requisition Detail Gridview,requisition 
    /// </summary>
    private void BindRequisitionInfo()                                                         //-- Bind Grid_RqsnDtl Gridview
    {

        try
        {
            DataTable dtReqInfo = new DataTable();
            using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
            {

                dtReqInfo = objTechService.GeRequisitiontEvaluation(Requisitioncode, Document_Code);
                lbl_Vessel.Text = dtReqInfo.DefaultView[0]["Vessel_Name"].ToString();
                lblVesselCode.Value = dtReqInfo.DefaultView[0]["Vessel_Code"].ToString();
                lbl_Cat_Sys.Text = dtReqInfo.DefaultView[0]["SYSTEM_Description"].ToString();
                lbl_ReceivalDate.Text = dtReqInfo.DefaultView[0]["requestion_Date"].ToString();
                lblITEMSYSTEMCODE.Value = dtReqInfo.DefaultView[0]["ITEM_SYSTEM_CODE"].ToString();
                lbl_Req_Del_Date.Text = dtReqInfo.DefaultView[0]["RFQ_Date"].ToString();
                lbl_supplier.Text = dtReqInfo.DefaultView[0]["Supplier"].ToString();
                lbl_reqsnNo.Text = dtReqInfo.DefaultView[0]["REQUISITION_CODE"].ToString();
                lbl_ItemAmount.Text = dtReqInfo.DefaultView[0]["REQ_Total_ITEMS_Amount"].ToString();
                lbl_Discount.Text = dtReqInfo.DefaultView[0]["REQ_DISCOUNT"].ToString();
                lbl_Vat.Text = dtReqInfo.DefaultView[0]["REQ_VAT"].ToString();
                hdfSupplierBeingApproved.Value = dtReqInfo.DefaultView[0]["SUPPLIER_CODE"].ToString();
                ViewState["SavedReqsnType"] = Convert.ToString(dtReqInfo.DefaultView[0]["Reqsn_Type"]);
                lbl_withholdTax.Text = dtReqInfo.DefaultView[0]["REQ_Withholding_Tax_Rate"].ToString();
                hdfQTNCode.Value = dtReqInfo.DefaultView[0]["QUOTATION_CODE"].ToString();
                lbl_TotalAmount.Text = dtReqInfo.DefaultView[0]["Total_Pay_Amount"].ToString();
                lbl_Maker.Text = dtReqInfo.DefaultView[0]["Maker"].ToString();
                ViewState["Dept_Code"] = dtReqInfo.DefaultView[0]["DEPARTMENT"].ToString();
                lbl_AccountType.Text = dtReqInfo.DefaultView[0]["Account_Type"].ToString();

                Grid_RqsnDtl.DataSource = dtReqInfo;
                Grid_RqsnDtl.DataBind();

            }

        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }


    }
    /// <summary>
    /// Bind Budget Code Gropdownlist on "Approve PO" Button Click
    /// </summary>
    protected void Fill_Budget()         //Fill Dropdownlist
    {
        
        BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase();
        DataSet dtBgtCode = objTechService.SelectBudgetCode(Requisitioncode, Vessel_Code, Convert.ToInt32(ddlReqsnType.SelectedValue));
        try
        {

            if (dtBgtCode.Tables[0].Rows.Count > 0)
            {
                ddlBudgetCode.DataSource = dtBgtCode.Tables[0];
                ddlBudgetCode.DataTextField = "Budget_Name";
                ddlBudgetCode.DataValueField = "Linked_Budget_Code";
                ddlBudgetCode.DataBind();
                ddlBudgetCode.Items.Insert(0, new ListItem("Select", "0"));
                //Session["Budget_Code"] = "1";
                hdnBudgetCode.Value = "1";
                lblBudgetMsg.Text = "";
                btnRequestAmount.Visible = true;
            }
            else
            {
                ddlBudgetCode.DataSource = dtBgtCode.Tables[1];
                ddlBudgetCode.DataTextField = "Budget_Name";
                ddlBudgetCode.DataValueField = "Budget_Code";
                ddlBudgetCode.DataBind();
                ddlBudgetCode.Items.Insert(0, new ListItem("Select", "0"));
                //Session["Budget_Code"] = "0";
                hdnBudgetCode.Value = "0";
                lblBudgetMsg.Text = "Budget limit not defined for this vessel.";
                btnRequestAmount.Visible = false;
            }
            if (!string.IsNullOrWhiteSpace(Convert.ToString(ViewState["BGTCODE"])))
            {
                ddlBudgetCode.ClearSelection();
                ListItem libgt = ddlBudgetCode.Items.FindByValue(ViewState["BGTCODE"].ToString());
                if (libgt != null)
                    libgt.Selected = true;
            }
            else
            {
                ddlBudgetCode.SelectedIndex = 0;
            }
            txtComment.Text = "";
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }

    }                                                           
    protected bool isProvisionLimitExceeding()
    {
        bool isProvisionLimitExceeding = false;

        return isProvisionLimitExceeding;
    }
    /// <summary>
    ///Save the Purchase Order on "Save Quptation Evaluation"  Button Click
    /// </summary>
    
    private void SavePurchasedOrder(DataTable dtReqInfo)                                      //-- SAve Purchase Order
    {
        try
        {
            string strSupplier = dtReqInfo.Rows[0]["SUPPLIER"].ToString();

            if (strSupplier.Length > 0)
            {
                using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
                {
                    //qtnbased
                    // at the time of approval sentto suppdt will become 1 so that it will not come under RFQ/quotation stage and
                    // on roll back of a PO this column will be set to 0 (zero) for that quotation only

                    int RetVal = objTechService.InsertDataForPO(Requisitioncode, Vessel_Code, strSupplier, hdfQTNCode.Value, User, Document_Code);

                }
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
   

}


