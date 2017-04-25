using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Text;

using Telerik.Web.UI;

//using SMS.Business.PURC;
using SMS.Business.Infrastructure;
using SMS.Business.CP;
using SMS.Properties;

public partial class CP_Income_Matching : System.Web.UI.Page
{
    protected DataTable dtGridItems;
    UserAccess objUA = new UserAccess();
    public int CPID = 0;
    public int Charter_ID = 0;
    public int Inv_ID = 0;
    public int Remittance_Id = 0;
    public string Inv_ID_Ref = "";
    public string Remittance_Id_Ref = "";
    public int RcvInv_Id = 0;
    public double MatchAmt = 0.00;
    public double OutStandingAmt = 0.00;
    public double BilledAmt = 0.00;
    public string Type = "";
    public bool uaEditFlag = true;//Test default true
    public bool uaDeleteFlage = true;
    public bool HideMatched = true;
    BLL_CP_CharterParty objCP = new BLL_CP_CharterParty();
    BLL_CP_HireInvoice objHireInv = new BLL_CP_HireInvoice();
    BLL_Infra_UserCredentials objUserBLL = new BLL_Infra_UserCredentials();
    protected void Page_Load(object sender, EventArgs e)
    {
       // UserAccessValidation();
        if (!IsPostBack)
        {
            Session["HideMatched"] = HideMatched;
            Session["Inv_ID"] = null;
            BindIHire_Matching(HideMatched);
            BindITransactionHire(HideMatched);

        }

    }


    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
            Response.Redirect("~/default.aspx?msgid=1");


        if (objUA.Edit == 1)
            uaEditFlag = true;
        else
            // btnsave.Visible = false;
            if (objUA.Delete == 1) uaDeleteFlage = true;

    }
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }



    private void BindIHire_Matching(bool HideMatched)
    {
        CPID = Convert.ToInt32(Session["CPID"]);
        try
        {
            HideMatched = Convert.ToBoolean(Session["HideMatched"]);
            DataTable dt = objHireInv.Get_Hire_Matching_Inv(CPID, HideMatched);

            gvMatching.DataSource = dt;
            gvMatching.DataBind();
    
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
    private void BindITransactionHire(bool HideMatched)
    {
        CPID = Convert.ToInt32(Session["CPID"]);
        try
        {
            DataTable dt = objHireInv.Get_Transaction_Matching_Inv(CPID, HideMatched);


            gvTransaction.DataSource = dt;
            gvTransaction.DataBind();
      

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


    protected void btnSaveItem_Click(object sender, EventArgs e)
    {

        if (Session["CPID"] != null)
        {
            BindITransactionHire(Convert.ToBoolean(Session["HideMatched"]));
            BindIHire_Matching(Convert.ToBoolean(Session["HideMatched"]));
        }


    }
    private void SetSelectedRecord()
    {
        for (int i = 0; i < gvMatching.Rows.Count; i++)
        {
            RadioButton rb = (RadioButton)gvMatching.Rows[i].FindControl("rdInv");
            if (rb != null)
            {
                HiddenField hf = (HiddenField)gvTransaction.Rows[i].FindControl("hdnHireInvId");
                if (hf != null && Session["hdnHireInvId"] != null)
                {
                    if (hf.Value.Equals(Session["hdnHireInvId"].ToString()))
                    {
                        rb.Checked = true;
                        break;
                    }
                }
            }
        }
    }
    private void GetSelectedRecord()
    {
        for (int i = 0; i < gvMatching.Rows.Count; i++)
        {
            RadioButton rb = (RadioButton)gvMatching.Rows[i].FindControl("rdInv");
            if (rb != null)
            {
                if (rb.Checked)
                {
                    HiddenField hf = (HiddenField)gvMatching.Rows[i].FindControl("hdnHireInvId");
                    if (hf != null)
                    {
                        Session["hdnHireInvId"] = hf.Value;
                    }

                    break;
                }
            }
        }
    }

    private void SetSelectedTranRecord()
    {
        for (int i = 0; i < gvMatching.Rows.Count; i++)
        {
            RadioButton rb = (RadioButton)gvTransaction.Rows[i].FindControl("rdInv");
            if (rb != null)
            {
                HiddenField hf = (HiddenField)gvTransaction.Rows[i].FindControl("hdnHireInvId");
                if (hf != null && Session["hdnHireInvId"] != null)
                {
                    if (hf.Value.Equals(Session["hdnHireInvId"].ToString()))
                    {
                        rb.Checked = true;
                        break;
                    }
                }
            }
        }
    }
    private void GetSelectedTranReceipt()
    {
        for (int i = 0; i < gvTransaction.Rows.Count; i++)
        {
            RadioButton rb = (RadioButton)gvTransaction.Rows[i].FindControl("rdInv");
            if (rb != null)
            {
                if (rb.Checked)
                {
                    HiddenField hf = (HiddenField)gvTransaction.Rows[i].FindControl("hdnHireInvId");
                    if (hf != null)
                    {
                        Session["hdnHireInvId"] = hf.Value;
                    }

                    break;
                }
            }
        }
    }


    protected void onDelete(object source, CommandEventArgs e)
    {
        HiddenField hdnTPId = (gvMatching.FindControl("hdnHireInvId") as HiddenField);
        string[] arg = e.CommandArgument.ToString().Split(',');
        int? ItemID = UDFLib.ConvertIntegerToNull(arg[0]);
        objCP.DEL_Billing_Item(ItemID, GetSessionUserID());
      // BindItems(HideMatched);
    }



    protected void btnHideShow_Click(object sender, EventArgs e)
    {
        HideMatched = Convert.ToBoolean(Session["HideMatched"]);
        if (HideMatched)
        {
           
            HideMatched = false;
            Session["HideMatched"] = HideMatched;
            BindITransactionHire(HideMatched);
            BindIHire_Matching(HideMatched);

            btnHideShow.Text = "Hide Matched";
       }
        else
        {
            HideMatched = true;
            Session["HideMatched"] = HideMatched;
            BindITransactionHire(HideMatched);
            BindIHire_Matching(HideMatched);
            btnHideShow.Text = "Show Matched";
        }
    }

    protected void gvTransaction_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvTransaction.PageIndex = e.NewPageIndex;
        if (Session["HideMatched"] != null)
        {
            HideMatched = Convert.ToBoolean(Session["HideMatched"]);
            BindITransactionHire(HideMatched);
        }

    }
    protected void gvMatching_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvMatching.PageIndex = e.NewPageIndex;
        if (Session["HideMatched"] != null)
            HideMatched = Convert.ToBoolean(Session["HideMatched"]);
        BindIHire_Matching(HideMatched);
    }

    protected void gvInvoiceItems_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvInvoiceItems.PageIndex = e.NewPageIndex;

        BindInvItems();
    }
    


    protected void rdInv_CheckChanged(Object sender, EventArgs e)
    {
        RadioButton rdInv = (RadioButton)sender;
        GridViewRow gvrow = (GridViewRow)rdInv.NamingContainer;
        HiddenField hdnInv = (HiddenField)gvrow.FindControl("hdnHireInvId");
        HiddenField hdnInvoiceRef = (HiddenField)gvrow.FindControl("hdnInvoiceRef");
        if (rdInv.Checked)
        {
            Session["Inv_ID"] = hdnInv.Value;
            Session["Inv_Ref"] = hdnInvoiceRef.Value;
        }
    }


    protected void rdTran_CheckChanged(Object sender, EventArgs e)
    {
        RadioButton rdTran = (RadioButton)sender;
        GridViewRow gvrow = (GridViewRow)rdTran.NamingContainer;
        HiddenField hdnRemittanceId = (HiddenField)gvrow.FindControl("hdnRemittanceId");
        HiddenField hdnType = (HiddenField)gvrow.FindControl("hdnType");
        if (rdTran.Checked)
        {
            Session["Remittance_Id"] = hdnRemittanceId.Value;
            Session["Type"] = hdnType.Value;
        }
    }
    

    protected void ibtnView_Click( object sender, EventArgs e)
    {
        try
        {

            GridViewRow gvrow = (GridViewRow)((ImageButton)sender).NamingContainer;
            HiddenField hdnInv =(HiddenField) gvrow.FindControl("hdnHireInvId");
            HiddenField hdnInvoiceRef = (HiddenField)gvrow.FindControl("hdnInvoiceRef");
            
            HiddenField hdnOustStandingRemarks = (HiddenField)gvrow.FindControl("hdnOustStandingRemarks");
            Label lblOustandingAmt = (Label)gvrow.FindControl("lblOustandingAmt"); 
            if (hdnOustStandingRemarks.Value != null)
             txtOutStandingRemarks.Text = hdnOustStandingRemarks.Value;
            ltOutstandingRemarks.Text = "Remarks for Oustanding Amount:";
            gvMatching.SelectedIndex = gvrow.RowIndex;
            Inv_ID = Convert.ToInt32(hdnInv.Value);
            Session["Inv_ID"] = Inv_ID;
            Session["Inv_Ref"] = hdnInvoiceRef.Value;

            BindInvItems();
            BindOffSet(1, hdnInvoiceRef.Value);

        }
        catch { }

    }



    private void BindOffSet(int Type, string InvoiceRef)
    {
        try
        {
            DataTable dt = objHireInv.Get_Hire_OffSet_Inv(Type, InvoiceRef);

            gvOffset.DataSource = dt;
            gvOffset.DataBind();


        }
        catch (Exception ex)
        {

        }
        finally
        {

        }

    }



    protected void ibtnViewRemittance_Click(object sender, EventArgs e)
    {
        try
        {

            GridViewRow gvrow = (GridViewRow)((ImageButton)sender).NamingContainer;
            HiddenField hdnRemittanceId = (HiddenField)gvrow.FindControl("hdnRemittanceId");
            HiddenField hdnType = (HiddenField)gvrow.FindControl("hdnType");

            gvTransaction.SelectedIndex = gvrow.RowIndex;
            Session["Remittance_Id"] = hdnRemittanceId.Value;
            BindInvItems();
            if(hdnType.Value=="R")
                BindOffSet(2, hdnRemittanceId.Value);
            else if (hdnType.Value == "I")
                BindOffSet(3, hdnRemittanceId.Value);
        }
        catch { }

    }

    

    private void BindInvItems()
    {
        DataTable dt = objHireInv.GET_Hire_Invoice_Items(UDFLib.ConvertIntegerToNull(Session["CPID"]), UDFLib.ConvertIntegerToNull(Session["Inv_ID"]));
        if (dt.Rows.Count > 0)
        {
            gvInvoiceItems.DataSource = dt;
            gvInvoiceItems.DataBind();
            //rgdItems.ShowFooter = false;
        }
    }



    protected void gvMatching_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                GridViewRow gvr = e.Row;

                Label lblOustandingAmt = (Label)gvr.FindControl("lblOustandingAmt");
                Label lblBilledAmt = (Label)gvr.FindControl("lblBilledAmt");
                RadioButton rdInv = (RadioButton)gvr.FindControl("rdInv");
                BilledAmt = Convert.ToDouble(lblBilledAmt.Text);
                OutStandingAmt = Convert.ToDouble(lblOustandingAmt.Text);

                if (OutStandingAmt == 0 && BilledAmt != 0)
                    rdInv.Enabled = false;
                





            }
        }
        catch { }
    }
    

    
    protected void gvTransaction_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                GridViewRow gvr = e.Row;

                Label lblRemarks = (Label)gvr.FindControl("lblRemarks");
                Label lblOustandingAmt = (Label)gvr.FindControl("lblOustandingAmt");
                RadioButton rdInv = (RadioButton)gvr.FindControl("rdInv");
                string Remarks = DataBinder.Eval(e.Row.DataItem, "Remittance_Remarks").ToString();

                OutStandingAmt = Convert.ToDouble(lblOustandingAmt.Text);
                if (Remarks != null && Remarks != "0")
                {
                    if (Remarks.Length > 12)
                    {
                        lblRemarks.Text = Remarks.Substring(0, 12) + "...";

                    }
                    else
                        lblRemarks.Text = Remarks;

                    lblRemarks.ToolTip = Remarks;
                }

                if (OutStandingAmt == 0)

                    rdInv.Enabled = false;


            }
        }
        catch { }
    }
    protected void gvInvoiceReceived_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                GridViewRow gvr = e.Row;

                Label lblRemarks = (Label)gvr.FindControl("lblRemarks");

                string Remarks = DataBinder.Eval(e.Row.DataItem, "Remarks").ToString();


                if (Remarks != null && Remarks != "0")
                {
                    if (Remarks.Length > 12)
                    {
                        lblRemarks.Text = Remarks.Substring(0, 12) + "...";

                    }
                    else
                        lblRemarks.Text = Remarks;

                    lblRemarks.ToolTip = Remarks;
                }





            }
        }
        catch { }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if(txtOutStandingRemarks.Text != "" && Session["Inv_ID"] != null)
        {
            Inv_ID = Convert.ToInt32(Session["Inv_ID"]);
            int result = objHireInv.Upd_Outstandingremarks(Inv_ID, txtOutStandingRemarks.Text, Convert.ToInt32(Session["USERID"]));
            BindIHire_Matching(Convert.ToBoolean(ViewState["HideMatched"]));
        }
    }
    protected void btnMatch_Click(object sender, EventArgs e)
    {
        if (Session["Inv_Ref"] != null && Session["Remittance_Id"] != null && Session["Type"] != null)
        {
            Inv_ID_Ref = Session["Inv_Ref"].ToString();
            Remittance_Id_Ref = Session["Remittance_Id"].ToString();
            Charter_ID = Convert.ToInt32(Session["CPID"]);
            Type = Session["Type"].ToString();
            Inv_ID = Convert.ToInt32(Session["Inv_ID"]);
            MatchAmt = Convert.ToDouble(txtMatchAmt.Text);

            int Res = objHireInv.Upd_MatchAmt(Inv_ID, MatchAmt, Type, Charter_ID, Inv_ID_Ref, Remittance_Id_Ref, Convert.ToInt32(Session["USERID"]));

            if (Res == -1)
            {
                lblError.Text = "Matching amount canot be more than Outstanding !";
                lblError.Visible = true;
            }
            else if (Res == -2)
            {
                lblError.Text = "Matching amount canot be more than Offset !";
                lblError.Visible = true;
            }
            else
            {
                 int result;
                if(txtOutStandingRemarks.Text != "")
                    result = objHireInv.Upd_Outstandingremarks(Inv_ID, txtOutStandingRemarks.Text, Convert.ToInt32(Session["USERID"]));



                BindIHire_Matching(HideMatched);
                BindITransactionHire(HideMatched);
                lblError.Visible = false;
            }
           
        }


    }
}
   
