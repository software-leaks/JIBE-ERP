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
using SMS.Business.ASL;
using SMS.Business.Infrastructure;
using SMS.Properties;
using SMS.Business.POLOG;
using System.IO;
using System.Drawing;


public partial class PO_Log_Online_Invoice : System.Web.UI.Page
{
    BLL_Infra_UserCredentials objBLLUser = new BLL_Infra_UserCredentials();
    UserAccess objUA = new UserAccess();
    public string AmendType = null;
    public string GeneralType = null;
    public string GreenType = null;
    public string YellowType = null;
    public string RedType = null;
    public Boolean uaEditFlag = true;
    public Boolean uaDeleteFlage = true;
    MergeGridviewHeader_Info objChangeReqstMerge = new MergeGridviewHeader_Info();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindOnlineInvoice();
            BindVariables();
            //gvOnlineInvoice.SelectedIndex = 0;
            pagePostBack();

        } 
    }

    protected void BindVariables()
    {

        DataSet ds = BLL_POLOG_Register.POLOG_Get_Type(UDFLib.ConvertToInteger(GetSessionUserID()),"PO_TYPE");

       cblVariables.DataSource = ds.Tables[1];
       cblVariables.DataTextField = "Variable_Name";
       cblVariables.DataValueField = "Variable_Code";
       cblVariables.DataBind();


       cblVariableInvoice.DataSource = ds.Tables[0];
       cblVariableInvoice.DataTextField = "Variable_Name";
       cblVariableInvoice.DataValueField = "Variable_Code";
       cblVariableInvoice.DataBind();
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
   
    protected void BindRemarks(string InvoiceID)
    {
        try
        {
            DataSet ds = BLL_POLOG_Register.POLOG_Get_Remarks_ByInvoiceID(InvoiceID);
            //gvRemarks.DataSource = ds.Tables[0];
            //gvRemarks.DataBind();
        }
        catch { }
            {
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
   

    protected void ClearControl()
    {
        txtRemarks.Text = "";
        ViewState["ReturnSupplierID"] = 0;
        

    }
    protected void btnGeneral_Click(object s, CommandEventArgs e)
    {
        string[] arg = e.CommandArgument.ToString().Split(',');
        int? RemarksID = UDFLib.ConvertIntegerToNull(arg[0]);
        string Remarks_Action = "General";
        int RetValue = BLL_POLOG_Register.POLOG_Update_Remarks(UDFLib.ConvertIntegerToNull(RemarksID), UDFLib.ConvertIntegerToNull(txtInvoiceCode.Text), Remarks_Action, UDFLib.ConvertToInteger(Session["UserID"].ToString()));
        BindRemarks(UDFLib.ConvertStringToNull(txtInvoiceCode.Text));
    }
    protected void btnWarning_Click(object s, CommandEventArgs e)
    {
        string[] arg = e.CommandArgument.ToString().Split(',');
        int? RemarksID = UDFLib.ConvertIntegerToNull(arg[0]);
        string Remarks_Action = "Yellow Card";
        int RetValue = BLL_POLOG_Register.POLOG_Update_Remarks(UDFLib.ConvertIntegerToNull(RemarksID), UDFLib.ConvertIntegerToNull(txtInvoiceCode.Text), Remarks_Action, UDFLib.ConvertToInteger(Session["UserID"].ToString()));
        BindRemarks(UDFLib.ConvertStringToNull(txtInvoiceCode.Text));

    }
    protected void btnRed_Click(object s, CommandEventArgs e)
    {
        string[] arg = e.CommandArgument.ToString().Split(',');
        int? RemarksID = UDFLib.ConvertIntegerToNull(arg[0]);
        string Remarks_Action = "Red Card";
        int RetValue = BLL_POLOG_Register.POLOG_Update_Remarks(UDFLib.ConvertIntegerToNull(RemarksID), UDFLib.ConvertIntegerToNull(txtInvoiceCode.Text), Remarks_Action, UDFLib.ConvertToInteger(Session["UserID"].ToString()));
        BindRemarks(UDFLib.ConvertStringToNull(txtInvoiceCode.Text));
    }


    protected void imgfilter_Click(object sender, ImageClickEventArgs e)
    {

        BindOnlineInvoice();
    }
    protected void btnRefresh_Click(object sender, ImageClickEventArgs e)
    {
        txtfilter.Text = "";
        BindOnlineInvoice();
    }

    protected void BindOnlineInvoice()
    {
        try
        {
            objChangeReqstMerge.AddMergedColumns(new int[] { 2, 3, 4 , 5, 6, 7}, "PO Details", "HeaderStyle-center");
            objChangeReqstMerge.AddMergedColumns(new int[] { 9, 10, 11, 12, 13,14 }, "Online Invoice", "HeaderStyle-center");
            int rowcount = ucCustomPager1.isCountRecord;

            string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
            int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

            DataSet ds = BLL_POLOG_Invoice.POLOG_Get_PedingOnline_Invoice(txtfilter.Text.Trim() != "" ? txtfilter.Text.Trim() : null, GetSessionUserID(),
                 sortbycoloumn, sortdirection
                , ucCustomPager1.CurrentPageIndex, ucCustomPager1.PageSize, ref  rowcount);

            if (ucCustomPager1.isCountRecord == 1)
            {
                ucCustomPager1.CountTotalRec = rowcount.ToString();
                ucCustomPager1.BuildPager();
            }

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvOnlineInvoice.DataSource = ds.Tables[0];
                gvOnlineInvoice.DataBind();
            }
            else
            {
                 gvOnlineInvoice.DataSource =null;
                gvOnlineInvoice.DataBind();
                gvExistingInvoices.DataSource = null;
                gvExistingInvoices.DataBind();
                gvDuplieCateinvoice.DataSource = null;
                gvDuplieCateinvoice.DataBind();
            }
            
             
              

        }
        catch { }
        {
        }
    }




    protected void ibtnView_Click(object sender, EventArgs e)
    {
        gvOnlineInvoice.Rows[0].BackColor = Color.White;
        GridViewRow row = (GridViewRow)((ImageButton)sender).NamingContainer;
        int Supplier_ID = Convert.ToInt32(gvOnlineInvoice.DataKeys[row.RowIndex].Values[1]);
        gvOnlineInvoice.SelectedIndex = row.RowIndex;
        ViewState["Supplier_Id"] = Supplier_ID;
        ViewState["Invoice_Id"] = Supplier_ID;
        HiddenSupplyId.Value = Supplier_ID.ToString();
        hiddenKeyId.Value = gvOnlineInvoice.DataKeys[row.RowIndex].Values[3].ToString();
        ViewState["Invoice_ref"] = (gvOnlineInvoice.DataKeys[row.RowIndex].Values[2]);
        //BindOnlineInvoice();
        txtInvRef.Text = ViewState["Invoice_ref"].ToString();
        BindInvoiceDetails();
        txtGSTAmount.Text = ((Label)row.FindControl("lblGST")).Text;
        txtInvoiceAmount.Text = ((Label)row.FindControl("lblAmount")).Text;
        if (!string.IsNullOrEmpty(((Label)row.FindControl("lblPayment_DueDate")).Text))
        {
            txtDuedate.Text = Convert.ToDateTime(((Label)row.FindControl("lblPayment_DueDate")).Text).ToString("dd-MM-yyyy");
        }
        else
        {
            txtDuedate.Text = "";
        }
        if (!string.IsNullOrEmpty(((Label)row.FindControl("lblInvoice_Date")).Text))
        {
            txtInvoiceDate.Text = Convert.ToDateTime(((Label)row.FindControl("lblInvoice_Date")).Text).ToString("dd-MM-yyyy");
        }
        else
        {
            txtInvoiceDate.Text = "";
        }
        txtRemarks.Text = ((Label)row.FindControl("lblInvoice_Remarks")).Text;
        if (!string.IsNullOrEmpty(gvOnlineInvoice.DataKeys[row.RowIndex].Values[4].ToString()))
        {
            string[] Invoice_flag = gvOnlineInvoice.DataKeys[row.RowIndex].Values[4].ToString().Split(',');
            foreach (string seatNumber in Invoice_flag)
            {
                ListItem chkItem = cblVariableInvoice.Items.FindByValue(seatNumber);
                chkItem.Selected = true;
            }
        }
        else
        {
            for (int i = 0; i < cblVariableInvoice.Items.Count; i++)
            {
                cblVariableInvoice.Items[i].Selected = false;
            }
        }
       
        string savePath = "../Uploads/Files_Uploaded/";
        string FilePath = savePath  + hiddenKeyId.Value+"." + gvOnlineInvoice.DataKeys[row.RowIndex].Values[5].ToString();
        //string js = "previewDocument('" + FilePath + "');";
        //ScriptManager.RegisterStartupScript(this, this.GetType(), "loadfile", js, true);
        IframeInvoice.Attributes["src"] = FilePath;
        IframePO.Attributes["src"] = "PO_Log_Preview.aspx?Supply_ID='" + HiddenSupplyId.Value + "'";
        BindOnlineInvoice();
    }

    protected void pagePostBack()
    {
       
        GridViewRow row = (GridViewRow)(gvOnlineInvoice.Rows[0]);
     
        int Supplier_ID = Convert.ToInt32(gvOnlineInvoice.DataKeys[row.RowIndex].Values[1]);
        gvOnlineInvoice.SelectedIndex = row.RowIndex;
        ViewState["Supplier_Id"] = Supplier_ID;
        ViewState["Invoice_Id"] = Supplier_ID;
        HiddenSupplyId.Value = Supplier_ID.ToString();
        hiddenKeyId.Value = gvOnlineInvoice.DataKeys[row.RowIndex].Values[3].ToString();
        ViewState["Invoice_ref"] = (gvOnlineInvoice.DataKeys[row.RowIndex].Values[2]);
        //BindOnlineInvoice();
        txtInvRef.Text = ViewState["Invoice_ref"].ToString();
        BindInvoiceDetails();
        txtGSTAmount.Text = ((Label)row.FindControl("lblGST")).Text;
        txtInvoiceAmount.Text = ((Label)row.FindControl("lblAmount")).Text;
        if (!string.IsNullOrEmpty(((Label)row.FindControl("lblPayment_DueDate")).Text))
        {
            txtDuedate.Text = Convert.ToDateTime(((Label)row.FindControl("lblPayment_DueDate")).Text).ToString("dd-MM-yyyy");
        }
        else
        {
            txtDuedate.Text = "";
        }
        if (!string.IsNullOrEmpty(((Label)row.FindControl("lblInvoice_Date")).Text))
        {
            txtInvoiceDate.Text = Convert.ToDateTime(((Label)row.FindControl("lblInvoice_Date")).Text).ToString("dd-MM-yyyy");
        }
        else
        {
            txtInvoiceDate.Text = "";
        }
        txtRemarks.Text = ((Label)row.FindControl("lblInvoice_Remarks")).Text;
        if (!string.IsNullOrEmpty(gvOnlineInvoice.DataKeys[row.RowIndex].Values[4].ToString()))
        {
            string[] Invoice_flag = gvOnlineInvoice.DataKeys[row.RowIndex].Values[4].ToString().Split(',');
            foreach (string seatNumber in Invoice_flag)
            {
                ListItem chkItem = cblVariableInvoice.Items.FindByValue(seatNumber);
                chkItem.Selected = true;
            }
        }
        else
        {
            for (int i = 0; i < cblVariableInvoice.Items.Count; i++)
            {
                cblVariableInvoice.Items[i].Selected = false;
            }
        }

        string savePath = "../Uploads/Files_Uploaded/";
        string FilePath = savePath + hiddenKeyId.Value + "." + gvOnlineInvoice.DataKeys[row.RowIndex].Values[5].ToString();
        //string js = "previewDocument('" + FilePath + "');";
        //ScriptManager.RegisterStartupScript(this, this.GetType(), "loadfile", js, true);
        IframeInvoice.Attributes["src"] = FilePath;
        IframePO.Attributes["src"] = "PO_Log_Preview.aspx?Supply_ID='" + HiddenSupplyId.Value + "'";
     
    }
    protected void BindInvoiceDetails()
    {
        try
        {
            int rowcount = ucCustomPagerItems.isCountRecord;

            string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
            int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

            DataSet ds = BLL_POLOG_Invoice.POLOG_Get_Invoice_List(null, UDFLib.ConvertIntegerToNull(ViewState["Supplier_Id"]), GetSessionUserID(),
                 sortbycoloumn, sortdirection
                , ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);
            if (ucCustomPagerItems.isCountRecord == 1)
            {
                ucCustomPagerItems.CountTotalRec = rowcount.ToString();
                ucCustomPagerItems.BuildPager();
            }
            if (ds.Tables[0].Rows.Count > 0)
            {
                divExistingInvoices.Visible = true;
                gvExistingInvoices.DataSource = ds.Tables[0];
                gvExistingInvoices.DataBind();

                gvExistingInvoices.Rows[0].BackColor = System.Drawing.Color.Yellow;
            }
            else
            {
                divExistingInvoices.Visible = false;
                gvExistingInvoices.DataSource = ds.Tables[0];
                gvExistingInvoices.DataBind();
            }

        }
        catch { }
        {
        }
    }


   

    protected void BindDuplicateInvoice(string InvoiceID)
    {
        try
        {
            DataTable dt = BLL_POLOG_Register.POLOG_Get_DuplicateInvoice(InvoiceID);

            if (dt.Rows.Count > 0)
            {
                lblDuplicateInvoice.Text = "Duplicated invoice Reference Found";
                divDuplicateInvoice.Visible = true;
                gvDuplieCateinvoice.DataSource = dt;
                gvDuplieCateinvoice.DataBind();
            }
            else
            {
                divDuplicateInvoice.Visible = false;
                gvDuplieCateinvoice.DataSource = dt;
                gvDuplieCateinvoice.DataBind();
            }
        }
        catch { }
        {
        }
    }
    protected void BindDeliveryGrid(string InvoiceID)
    {
        try
        {
            DataTable dt = BLL_POLOG_Register.POLOG_Get_Delivery_Invoice(InvoiceID);

            if (dt.Rows.Count > 0)
            {
                lblDelivery.Text = "Confirmed deliveries record Found.";
                divExistingInvoices.Visible = true;
                gvExistingInvoices.DataSource = dt;
                gvExistingInvoices.DataBind();
            }
            else
            {
                lblDelivery.Visible = true;
                lblDelivery.Text = "No confirmed deliveries matched to this invoice.";
                divExistingInvoices.Visible = false;
                gvExistingInvoices.DataSource = dt;
                gvExistingInvoices.DataBind();
            }

        }
        catch { }
        {
        }
    }
    protected void gvRemarks_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //string ColorCode = DataBinder.Eval(e.Row.DataItem, "COLOR_CODE").ToString();
            //e.Row.BackColor = System.Drawing.Color.Yellow;
            string Invoice_ID = DataBinder.Eval(e.Row.DataItem, "Invoice_ID").ToString();
            string Supply_ID = DataBinder.Eval(e.Row.DataItem, "Supply_ID").ToString();
          
            if (Invoice_ID != "")
            {
                e.Row.BackColor = System.Drawing.Color.Yellow;
            }
            else
            {
                e.Row.BackColor = System.Drawing.Color.White;
            }
           
            string GeneralType = DataBinder.Eval(e.Row.DataItem, "GeneralType").ToString();
            string YellowType = DataBinder.Eval(e.Row.DataItem, "YellowType").ToString();
            string RedType = DataBinder.Eval(e.Row.DataItem, "RedType").ToString();
            Button btnGeneral = (Button)e.Row.FindControl("btnGeneral");
            Button btnWarning = (Button)e.Row.FindControl("btnWarning");
            Button btnRed = (Button)e.Row.FindControl("btnRed");
            if (GeneralType != "")
            {
                btnGeneral.BackColor = System.Drawing.Color.Blue;
            }
            else
            {
                btnGeneral.BackColor = System.Drawing.Color.White;
            }
            if (YellowType != "")
            {
                btnWarning.BackColor = System.Drawing.Color.Orange;
            }
            else
            {
                btnWarning.BackColor = System.Drawing.Color.White;
            }
            if (RedType != "")
            {
                btnRed.BackColor = System.Drawing.Color.Red;
            }
            else
            {
                btnRed.BackColor = System.Drawing.Color.White;
            }
        }
    }

    
    
    
  
   
  
   
    protected void btnExit_Click(object sender, EventArgs e)
    {
        string msgDraft = String.Format("parent.ReloadParent_ByButtonID();");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgDraft", msgDraft, true);
    }
  
   

    protected void gvOnlineInvoice_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.Header)
        {
            MergeGridviewHeader.SetProperty(objChangeReqstMerge);
            e.Row.SetRenderMethodDelegate(MergeGridviewHeader.RenderHeader);
        }
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    if (gvOnlineInvoice.Rows.Count > 0) gvOnlineInvoice.Rows[0].RowState = DataControlRowState.Selected;
            
        //}
     


    }

    protected string getvalues(CheckBoxList cbl)
    {
        string flag = String.Empty;

        for (int i = 0; i < cbl.Items.Count; i++)
        {
            if (cbl.Items[i].Selected)
            {
                flag += cbl.Items[i].Value + ",";
            }
        }

        flag = flag.TrimEnd(',');
        return flag;
    }
    protected void btnUpdateInvoice_Click(object sender, EventArgs e)
    {

        string flag = getvalues(cblVariableInvoice);
        string Action = "Update Invoice Data";
        BLL_POLOG_Invoice.POLOG_Update_Invoice_Data(Action, hiddenKeyId.Value, txtInvRef.Text, txtInvoiceDate.Text, Convert.ToDouble(txtInvoiceAmount.Text), txtDuedate.Text, txtTo.Text, txtfrom.Text, txtRemarks.Text, flag, Convert.ToDouble(50), ViewState["Supplier_Id"].ToString(), null, null, GetSessionUserID());
        string msg2 = String.Format("alert('Invoice data updated successfully')");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg2, true);
    }
    protected void btnVerify_Click(object sender, EventArgs e)
    {
        string flag = getvalues(cblVariableInvoice);
        string Action = "Verified and Import";
        BLL_POLOG_Invoice.POLOG_Update_Invoice_Data(Action, hiddenKeyId.Value, txtInvRef.Text, txtInvoiceDate.Text, Convert.ToDouble(txtInvoiceAmount.Text), txtDuedate.Text, txtTo.Text, txtfrom.Text, txtRemarks.Text, flag, Convert.ToDouble(50), ViewState["Supplier_Id"].ToString(), null, null, GetSessionUserID());
        string msg2 = String.Format("alert('Invoice data verified successfully')");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg2, true);
    }
    protected void btnDeleteInvoice_Click(object sender, EventArgs e)
    {
        string Action = "Delete Invoice";
        BLL_POLOG_Invoice.POLOG_Update_Invoice_Data(Action, hiddenKeyId.Value, txtInvRef.Text, txtInvoiceDate.Text, Convert.ToDouble(txtInvoiceAmount.Text), txtDuedate.Text, txtTo.Text, txtfrom.Text, txtRemarks.Text, "", Convert.ToDouble(50), ViewState["Supplier_Id"].ToString(), null, null, GetSessionUserID());
        string msg2 = String.Format("alert('Invoice data deleted successfully')");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg2, true);
    }
    protected void btnRejectInvoice_Click(object sender, EventArgs e)
    {
        string flag = getvalues(cblVariables);
        string Action = "Reject Invoice";
        BLL_POLOG_Invoice.POLOG_Update_Invoice_Data(Action, hiddenKeyId.Value, txtInvRef.Text, txtInvoiceDate.Text, Convert.ToDouble(txtInvoiceAmount.Text), txtDuedate.Text, txtTo.Text, txtfrom.Text, txtRemarks.Text, "", Convert.ToDouble(50), ViewState["Supplier_Id"].ToString(), flag, txtRejectionRemark.Text, GetSessionUserID());
        string msg2 = String.Format("alert('Invoice data rejected successfully')");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg2, true);
    }
    protected void gvOnlineInvoice_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //if (e.CommandName == "VIEW")
        //{
        //    ImageButton lnkView = (ImageButton)e.CommandSource;
        //   HiddenSupplyId.Value = lnkView.CommandArgument;
        //   //IframePO.Page("PO_Log_Preview.aspx?Supply_ID='"+HiddenSupplyId.Value+"'");
        //   IframePO.Attributes["src"] = "PO_Log_Preview.aspx?Supply_ID='" + HiddenSupplyId.Value + "'";
        //}
    }
    protected void cblVariables_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cblVariables.SelectedIndex != -1)
        {
            txtRejectionRemark.Enabled = true;
            btnRejectInvoice.Enabled = true;
            lblText.Enabled = true;
        }
        else
        {
            txtRejectionRemark.Enabled = false;
            btnRejectInvoice.Enabled = false;
            lblText.Enabled = false;
        }
    }
}