using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using SMS.Business.Infrastructure;
using SMS.Properties;
using SMS.Business.ASL;


public partial class ASL_ASL_Change_Request_Index : System.Web.UI.Page
{
   
    UserAccess objUA = new UserAccess();
    UserAccess objUAType = new UserAccess();
    //private long lCurrentRecord = 0;
    //private long lRecordsPerRow = 200;
   
    protected void Page_Load(object sender, EventArgs e)
    {
        //UserAccessValidation();
        if (!IsPostBack)
        {
            UserAccessTypeValidation();
            BindGrid();
        }
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
    protected void btnGet_Click(object sender, EventArgs e)
    {
        BindGrid();
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
    public string GetSuppID()
    {
        try
        {
            if (Request.QueryString["Supplier_code"] != null)
            {
                return Request.QueryString["Supplier_code"].ToString();
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
   
    public void BindGrid()
    {
        try
        {

            string SupplierID = GetSuppID();
            int ID = GetChangeRequestID();
            DataSet ds = BLL_ASL_Supplier.Get_ChangeRequest_Search(UDFLib.ConvertStringToNull(SupplierID), ID, GetSessionUserID());
            lblSupplierCode.Text = GetSuppID();
            hdnCRStatus.Value = ds.Tables[0].Rows[0]["Check_Status"].ToString();
            hdnCRStatus1.Value = ds.Tables[0].Rows[0]["Check_Status1"].ToString();
            hdnCRID.Value = ds.Tables[0].Rows[0]["CRID"].ToString();
            Session["dsCR"] = ds;
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                ds.Relations.Add(new DataRelation("NestedCat", ds.Tables[1].Columns["ID"], ds.Tables[0].Columns["ID"]));

                ds.Tables[1].TableName = "Members";

                rpt1.DataSource = ds.Tables[1];
                rpt1.DataBind();
                if (dr["Check_Status"].ToString() == "SUBMITTED")
                {
                    //btnSaveDraft.Visible = false;
                    btnRecallDraft.Visible = false;
                    btnRecallApprove.Visible = false;
                    btnSubmitRequest.Visible = false;
                    lblSubmittedBY.Text = dr["Login_Name"].ToString();
                    lblSubmitteddate.Text = dr["Verified_Date"].ToString();
                    if (UDFLib.ConvertToInteger(GetSessionUserID()) == UDFLib.ConvertToInteger(dr["Verified_By"].ToString()))
                    {
                        btnRecallRequest.Enabled = true;
                        btnRecallRequest.Visible = true;
                        trSubmitted.Visible = true;
                    }
                    else
                    {
                        btnRecallRequest.Enabled = false;
                        btnRecallRequest.Visible = true;
                        btnApprove.Visible = true;
                        btnReject.Visible = true;
                        btnFinalApprove.Visible = true;
                    }
                    //Check Approver
                    if (objUAType.Approve == 1)
                    {
                        //SetControl(true);
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
                    btnSubmitRequest.Visible = false;
                    btnRecallRequest.Visible = false;
                    lblSubmittedBY.Text = dr["Login_Name"].ToString();
                    lblSubmitteddate.Text = dr["Verified_Date"].ToString();
                    if (objUAType.Approve == 1)
                    {
                       // SetControl(true);
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
        catch (Exception ex)
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
    protected void rpt1_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
    {
        
        DataRowView dv = e.Item.DataItem as DataRowView;
        //Image ImgCard = ((ImageButton)e.Item.FindControl("ImgView"));
        RequiredFieldValidator val = (RequiredFieldValidator)e.Item.FindControl("RequiredFieldValidator1");
        RequiredFieldValidator val1 = (RequiredFieldValidator)e.Item.FindControl("RequiredFieldValidator2");
        if (dv != null)
        {
            Repeater nestedRepeater = e.Item.FindControl("rpt2") as Repeater;
            if (nestedRepeater != null)
            {

                nestedRepeater.DataSource = dv.CreateChildView("NestedCat");
                nestedRepeater.DataBind();
            }
        }
        
        if (e.Item.ItemType == ListItemType.Item ||
            e.Item.ItemType == ListItemType.AlternatingItem)
        {
            ((RequiredFieldValidator)e.Item.FindControl("RequiredFieldValidator1")).ValidationGroup = ((DropDownList)e.Item.FindControl("ddlApprover")).UniqueID;
            ((RequiredFieldValidator)e.Item.FindControl("RequiredFieldValidator2")).ValidationGroup = ((DropDownList)e.Item.FindControl("ddlFinalApprover")).UniqueID;
            Label lblAction = (Label)e.Item.FindControl("lblAction");
            DropDownList ddlApprover = (DropDownList)e.Item.FindControl("ddlApprover");
            DropDownList ddlFinalApprover = (DropDownList)e.Item.FindControl("ddlFinalApprover");
            Label lblGroup_Name = (Label)e.Item.FindControl("lblGroup_Name");
            Label lblGroupID = (Label)e.Item.FindControl("lblGroupID");
            HiddenField hdnApproverID = (HiddenField)e.Item.FindControl("hdnApproverID");
            HiddenField hdnFinalApproverID = (HiddenField)e.Item.FindControl("hdnFinal_ApproverID");
            string Approver_Type = "ChangeRequest";
            DataSet ds = BLL_ASL_Supplier.Get_Supplier_CR_ApproverList(UDFLib.ConvertToInteger(GetSessionUserID()), GetSessionSupplierType(), Approver_Type, lblGroupID.Text.ToString());
            //DataTable dt = objPortCall.Get_PortCall_PortList(0, UDFLib.ConvertIntegerToNull(DataBinder.Eval(e.Item.DataItem, "Vessel_ID").ToString()), 1);
            if (ds.Tables[1].Rows.Count > 0)
            {

                ddlApprover.DataSource = ds.Tables[1];
                ddlApprover.DataTextField = "User_name";
                ddlApprover.DataValueField = "ApproveID";
                ddlApprover.DataBind();
                ddlApprover.Items.Insert(0, new ListItem("SELECT", "0"));
            }
            else
            {
                ddlApprover.Items.Insert(0, new ListItem("SELECT", "0"));
            }
            if (ds.Tables[0].Rows.Count > 0)
            {

                ddlFinalApprover.DataSource = ds.Tables[0];
                ddlFinalApprover.DataTextField = "User_name";
                ddlFinalApprover.DataValueField = "ApproveID";
                ddlFinalApprover.DataBind();
                ddlFinalApprover.Items.Insert(0, new ListItem("SELECT", "0"));

            }
            else
            {
               ddlFinalApprover.Items.Insert(0, new ListItem("SELECT", "0"));
            }
            ddlApprover.SelectedValue = DataBinder.Eval(e.Item.DataItem, "ApproverID").ToString();
            ddlFinalApprover.SelectedValue = DataBinder.Eval(e.Item.DataItem, "Final_ApproverID").ToString();
            if (hdnCRStatus1.Value.ToString() == "0")
            {
                 lblAction.Visible = false;
                 ddlApprover.Enabled = true;
                 ddlFinalApprover.Enabled = true;
            }
            else 
            {
                lblAction.Visible = true;
                ddlApprover.Enabled = false;
                ddlFinalApprover.Enabled = false;
            }
            
        }

        if (rpt1 != null && rpt1.Items.Count < 1)
        {
            if (e.Item.ItemType == ListItemType.Footer)
            {
                // Show the Error Label (if no data is present).
                Label lblErrorMsg = e.Item.FindControl("lblErrorMsg") as Label;
                if (lblErrorMsg != null)
                {
                    lblErrorMsg.Visible = true;
                }
            }
        }
    }

    /// <summary>
    /// Check all label is appear in proper name or not,because getting error while assign value to label.
    /// JIT NO- 11685/Modified By - 9th Nov
    /// </summary>
    /// <param name="Sender"></param>
    /// <param name="e"></param>


    protected void rpt2_ItemDataBound(Object Sender, RepeaterItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.Item ||
            e.Item.ItemType == ListItemType.AlternatingItem)
            {
               
                CheckBox chk1 = (CheckBox)e.Item.FindControl("chk");
                HiddenField hdnFieldName = (HiddenField)e.Item.FindControl("hdnFieldName");
                HiddenField hdnSendforapprove = (HiddenField)e.Item.FindControl("hdnSendforapprove");
                HiddenField hdnSendFinalApprove = (HiddenField)e.Item.FindControl("hdnSendFinalApprove");
                Label lblCurrentValue = (Label)e.Item.FindControl("lblCurrentValue");
                Label lblNew_Value = (Label)e.Item.FindControl("lblNewValue");
                Label lblReason_For_Change = (Label)e.Item.FindControl("lblReason_For_Change");
                Label lblDescription = (Label)e.Item.FindControl("lblFieldDesc");

                string CurrentValue = DataBinder.Eval(e.Item.DataItem, "CurrentValue").ToString();
                string New_Value = DataBinder.Eval(e.Item.DataItem, "New_Value").ToString();
                string Reason_For_Change = DataBinder.Eval(e.Item.DataItem, "Reason_For_Change").ToString();
                string Description = DataBinder.Eval(e.Item.DataItem, "Column_Description").ToString();
                CurrentValue = CurrentValue.Replace("\n", "<br />");
                lblCurrentValue.Text = CurrentValue;
                New_Value = New_Value.Replace("\n", "<br />");
                lblNew_Value.Text = New_Value;
                Reason_For_Change = Reason_For_Change.Replace("\n", "<br />");
                lblReason_For_Change.Text = Reason_For_Change;
                Description = Description.Replace("\n", "<br />");
                lblDescription.Text = Description;
                //foreach (RepeaterItem item in rpt1.Items)
                //{
                    if (hdnCRStatus.Value.ToString() == "SUBMITTED")
                    {
                       // lblAction.Visible = true;

                        if (hdnSendforapprove.Value == "1")
                        {
                            chk1.Visible = true;
                        }
                        else
                        {
                            chk1.Visible = false;
                        }
                    }
                    else if (hdnCRStatus.Value.ToString() == "APPROVED")
                    {
                       // lblAction.Visible = true;

                        if (hdnSendFinalApprove.Value == "1")
                        {
                            chk1.Visible = true;
                        }
                        else
                        {
                            chk1.Visible = false;
                        }
                    }
                   
            }
          
        }
        catch (Exception ex) { UDFLib.WriteExceptionLog(ex); }

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
            //Response.Redirect("../ASL/ASL_CR.aspx?Supplier_code=" + UDFLib.ConvertStringToNull(Request.QueryString["Supplier_code"].ToString()) + "");
            //BindChangeRequest();
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
            int RetValue = SubmitRequest(CR_Status, Action_On_Data_Form);
            if (RetValue == -2)
            {
            }
            else  if (RetValue == 0)
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
                //Response.Redirect("../ASL/ASL_CR.aspx?Supplier_code=" + UDFLib.ConvertStringToNull(Request.QueryString["Supplier_code"].ToString()) + "");
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
                string message = "alert('Cant Recall some column already approved.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                string msgDraft = String.Format("parent.ReloadParent_ByButtonID();");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgDraft", msgDraft, true);
            }
            else
            {
                string message = "alert('Change Request Recalled.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                string msgDraft = String.Format("parent.ReloadParent_ByButtonID();");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgDraft", msgDraft, true);
            }

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
                string message = "alert('Change Request Recalled.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                string msgDraft = String.Format("parent.ReloadParent_ByButtonID();");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgDraft", msgDraft, true);
            }

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

        }
        catch { }
        {
        }

    }
    protected int Submit(string CR_Status, string Action_On_Data_Form)
    {
        DataTable dtcr = new DataTable();
        dtcr.Columns.Add("PKID");
        dtcr.Columns.Add("Field_Name");
        dtcr.Columns.Add("Old_Value");
        dtcr.Columns.Add("New_Value");
        dtcr.Columns.Add("Reason_For_Change");
        dtcr.Columns.Add("Approver");
        dtcr.Columns.Add("Final_Approver");

        foreach (RepeaterItem item in rpt1.Items)
        {
            DropDownList ddlApprover = item.FindControl("ddlApprover") as DropDownList;
            DropDownList ddlFinalApprover = item.FindControl("ddlFinalApprover") as DropDownList;
            Repeater nestedRepeater = item.FindControl("rpt2") as Repeater;
            foreach (RepeaterItem nestedItem in nestedRepeater.Items)
            {
                CheckBox chkIsActive = nestedItem.FindControl("chk") as CheckBox;
                Label lblFields_Name = nestedItem.FindControl("lblFieldsName") as Label;
                Label lblCurrentValue = nestedItem.FindControl("lblCurrentValue") as Label;
                Label lblNew_Value = nestedItem.FindControl("lblNewValue") as Label;
                Label lblReason_For_Change = nestedItem.FindControl("lblReason_For_Change") as Label;

                if (chkIsActive.Checked == true && chkIsActive != null)
                {
                    DataRow dr = dtcr.NewRow();
                    dr["PKID"] = 0;
                    dr["Field_Name"] = lblFields_Name.Text;
                    dr["Old_Value"] = lblCurrentValue.Text;
                    dr["New_Value"] = lblNew_Value.Text;
                    dr["Reason_For_Change"] = lblReason_For_Change.Text;
                    dr["Approver"] = ddlApprover.SelectedValue;
                    dr["Final_Approver"] = ddlFinalApprover.SelectedValue;
                    dtcr.Rows.Add(dr);
                }
            }
        }
        int RetValue = 0;
        if (dtcr.Rows.Count > 0)
        {
            RetValue = BLL_ASL_Supplier.Supplier_ChangeRequest_Status_Update(UDFLib.ConvertIntegerToNull(hdnCRID.Value), UDFLib.ConvertStringToNull(lblSupplierCode.Text), dtcr, CR_Status, Action_On_Data_Form, UDFLib.ConvertToInteger(GetSessionUserID()));
            hdnCRID.Value = Convert.ToString(RetValue);
            if (CR_Status == "EFFECTED")
            {
                SaveChildtable(CR_Status,Action_On_Data_Form);
            }
            return RetValue;
        }
        else
        {
            return RetValue;
        }
       
    }
    protected int Save(string CR_Status, string Action_On_Data_Form)
    {
        //int Str = 1;
        DataTable dtcr = new DataTable();
        dtcr.Columns.Add("PKID");
        dtcr.Columns.Add("Field_Name");
        dtcr.Columns.Add("Old_Value");
        dtcr.Columns.Add("New_Value");
        dtcr.Columns.Add("Reason_For_Change");
        dtcr.Columns.Add("Approver");
        dtcr.Columns.Add("Final_Approver");

        foreach (RepeaterItem item in rpt1.Items)
        {

            DropDownList ddlApprover = item.FindControl("ddlApprover") as DropDownList;
            DropDownList ddlFinalApprover = item.FindControl("ddlFinalApprover") as DropDownList;
            Label lblGroupName = item.FindControl("lblGroup_Desc") as Label;
            Repeater nestedRepeater = item.FindControl("rpt2") as Repeater;
            //if (ddlApprover.SelectedValue != "0") 
            //{
            //    if (ddlFinalApprover.SelectedValue != "0")
            //    {
                    foreach (RepeaterItem nestedItem in nestedRepeater.Items)
                    {
                        CheckBox chkIsActive = nestedItem.FindControl("chk") as CheckBox;
                        Label lblFields_Name = nestedItem.FindControl("lblFieldsName") as Label;
                        Label lblCurrentValue = nestedItem.FindControl("lblCurrentValue") as Label;
                        Label lblNew_Value = nestedItem.FindControl("lblNewValue") as Label;
                        Label lblReason_For_Change = nestedItem.FindControl("lblReason_For_Change") as Label;
                        DataRow dr = dtcr.NewRow();
                        dr["PKID"] = 0;
                        dr["Field_Name"] = lblFields_Name.Text;
                        dr["Old_Value"] = lblCurrentValue.Text;
                        dr["New_Value"] = lblNew_Value.Text;
                        dr["Reason_For_Change"] = lblReason_For_Change.Text;
                        dr["Approver"] = ddlApprover.SelectedValue;
                        dr["Final_Approver"] = ddlFinalApprover.SelectedValue;
                        dtcr.Rows.Add(dr);

                    }
                //}
                //else
                //{
                //    string ErrorMsg =  lblGroupName.Text + " " + "Final Approver Name is mandatory.";
                //    Str = -2;
                //    string msg2 = String.Format("alert(' " + ErrorMsg + " ')");
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg2, true);
                //}
            //}
            //else
            //{
            //    string ErrorMsg = lblGroupName.Text + " " + "Approver Name is mandatory.";
            //    Str = -2;
            //    string msg2 = String.Format("alert(' " + ErrorMsg + " ')");
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg2, true);
            //}
        }
        //if (Str == 1)
        //{
            int RetValue = 0;
            if (dtcr.Rows.Count > 0)
            {
                RetValue = BLL_ASL_Supplier.Supplier_ChangeRequest_Status_Update(UDFLib.ConvertIntegerToNull(hdnCRID.Value), UDFLib.ConvertStringToNull(lblSupplierCode.Text), dtcr, CR_Status, Action_On_Data_Form, UDFLib.ConvertToInteger(GetSessionUserID()));
                hdnCRID.Value = Convert.ToString(RetValue);

                return RetValue;
            }
            else
            {
                return RetValue;
            }
        //}
        //else
        //{
        //    int RetValue =-2;
        //    return RetValue;
        //}
        
    }
    protected int SubmitRequest(string CR_Status, string Action_On_Data_Form)
    {
        int Str = 1;
        DataTable dtcr = new DataTable();
        dtcr.Columns.Add("PKID");
        dtcr.Columns.Add("Field_Name");
        dtcr.Columns.Add("Old_Value");
        dtcr.Columns.Add("New_Value");
        dtcr.Columns.Add("Reason_For_Change");
        dtcr.Columns.Add("Approver");
        dtcr.Columns.Add("Final_Approver");

        foreach (RepeaterItem item in rpt1.Items)
        {

            DropDownList ddlApprover = item.FindControl("ddlApprover") as DropDownList;
            DropDownList ddlFinalApprover = item.FindControl("ddlFinalApprover") as DropDownList;
            Label lblGroupName = item.FindControl("lblGroup_Desc") as Label;
            Repeater nestedRepeater = item.FindControl("rpt2") as Repeater;
            if (ddlApprover.SelectedValue != "0")
            {
                if (ddlFinalApprover.SelectedValue != "0")
                {
                    foreach (RepeaterItem nestedItem in nestedRepeater.Items)
                    {
                        CheckBox chkIsActive = nestedItem.FindControl("chk") as CheckBox;
                        Label lblFields_Name = nestedItem.FindControl("lblFieldsName") as Label;
                        Label lblCurrentValue = nestedItem.FindControl("lblCurrentValue") as Label;
                        Label lblNew_Value = nestedItem.FindControl("lblNewValue") as Label;
                        Label lblReason_For_Change = nestedItem.FindControl("lblReason_For_Change") as Label;
                        DataRow dr = dtcr.NewRow();
                        dr["PKID"] = 0;
                        dr["Field_Name"] = lblFields_Name.Text;
                        dr["Old_Value"] = lblCurrentValue.Text;
                        dr["New_Value"] = lblNew_Value.Text;
                        dr["Reason_For_Change"] = lblReason_For_Change.Text;
                        dr["Approver"] = ddlApprover.SelectedValue;
                        dr["Final_Approver"] = ddlFinalApprover.SelectedValue;
                        dtcr.Rows.Add(dr);

                    }
                }
                else
                {
                    string ErrorMsg = lblGroupName.Text + " " + "Final Approver Name is mandatory.";
                    Str = -2;
                    string msg2 = String.Format("alert(' " + ErrorMsg + " ')");
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg2, true);
                }
            }
            else
            {
                string ErrorMsg = lblGroupName.Text + " " + "Approver Name is mandatory.";
                Str = -2;
                string msg2 = String.Format("alert(' " + ErrorMsg + " ')");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg2, true);
            }
        }
        if (Str == 1)
        {
            int RetValue = 0;
            if (dtcr.Rows.Count > 0)
            {
                RetValue = BLL_ASL_Supplier.Supplier_ChangeRequest_Status_Update(UDFLib.ConvertIntegerToNull(hdnCRID.Value), UDFLib.ConvertStringToNull(lblSupplierCode.Text), dtcr, CR_Status, Action_On_Data_Form, UDFLib.ConvertToInteger(GetSessionUserID()));
                hdnCRID.Value = Convert.ToString(RetValue);

                return RetValue;
            }
            else
            {
                return RetValue;
            }
        }
        else
        {
            int RetValue = -2;
            return RetValue;
        }

    }
    protected void SaveChildtable(string CR_Status, string Action_On_Data_Form)
    {
        try
        {
            BLL_ASL_Supplier.ASL_CR_Supplier_Child_Insert(UDFLib.ConvertIntegerToNull(hdnCRID.Value), UDFLib.ConvertStringToNull(lblSupplierCode.Text),CR_Status,Action_On_Data_Form, UDFLib.ConvertToInteger(GetSessionUserID()));

        }
        catch { }
            {
            }
    }
    
}