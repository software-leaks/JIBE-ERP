using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Infrastructure;
using SMS.Business.CP;
using System.Data;
using System.Web.UI.HtmlControls;
using SMS.Properties;

public partial class CP_Owners_Expenses : System.Web.UI.Page
{
    UserAccess objUA = new UserAccess();
    public string Invoice_ID = "";
    public int CPID = 0;
    public Boolean uaEditFlag = true;//Test default true
    public Boolean uaDeleteFlage = true;
    public bool HideMatched = true;
    BLL_CP_CharterParty objCP = new BLL_CP_CharterParty();
    BLL_CP_HireInvoice objHireInv = new BLL_CP_HireInvoice();
    BLL_Infra_UserCredentials objUserBLL = new BLL_Infra_UserCredentials();
    protected void Page_Load(object sender, EventArgs e)
    {
       // UserAccessValidation();
        if (!IsPostBack)
        {
            BindOwnerExp();

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
            if (objUA.Delete == 1) uaDeleteFlage = true;

    }
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }

    protected void BindOwnerExp()
    {
        HideMatched = Convert.ToBoolean(ViewState["HideMatched"]);
        DataTable dt = objCP.Get_Owner_Expenses(UDFLib.ConvertIntegerToNull(Session["CPID"]), HideMatched, GetSessionUserID()).Tables[1];

        gvOwnerExpenses.DataSource = dt;
        gvOwnerExpenses.DataBind();
        ltmessage.Visible = false;

    }

    protected void ClearData()
    {

        ltmessage.Text = "";
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        SaveData();
        BindOwnerExp();
    }
    protected void btnSaveClose_Click(object sender, EventArgs e)
    {
        SaveData();
        this.ClientScript.RegisterClientScriptBlock(this.GetType(), "Close", "window.close();", true);
    }

    protected void SaveData()
    {
        int res = 1;
        try
        {
            //res = objCP.INS_Remittance(UDFLib.ConvertIntegerToNull(ViewState["CPID"]), UDFLib.ConvertIntegerToNull(ddlUser.SelectedValue),
            //    txtRemittance.Text, GetSessionUserID());
            ClearData();
            if (res == 1)
                ltmessage.Text = "Remark added successfully.";
              

        }
        catch { }
    }

    protected void ibtnUnmatch_Click(object source, CommandEventArgs e)
    {
        DataTable dt = new DataTable();
        try
        {
            Invoice_ID = e.CommandArgument.ToString();
            ViewState["Invoice_ID"] = Invoice_ID;

            objHireInv.Upd_Inv_Approve(UDFLib.ConvertToInteger(Session["CPID"]),1, Invoice_ID, GetSessionUserID());

            BindOwnerExp();


        }
        catch { }

    }


    protected void ibtnApprove_Click(object source, CommandEventArgs e)
    {
        DataTable dt = new DataTable();
        try
        {
            Invoice_ID = e.CommandArgument.ToString();
            ViewState["Invoice_ID"] = Invoice_ID;

            objHireInv.Upd_Inv_Approve(UDFLib.ConvertToInteger(Session["CPID"]), 2, Invoice_ID, GetSessionUserID());

            BindOwnerExp();


        }
        catch { }

    }

    protected void ibtnUnApprove_Click(object source, CommandEventArgs e)
    {
        DataTable dt = new DataTable();
        try
        {
            Invoice_ID = e.CommandArgument.ToString();
            ViewState["Invoice_ID"] = Invoice_ID;

            objHireInv.Upd_Inv_Approve(UDFLib.ConvertToInteger(Session["CPID"]),3, Invoice_ID, GetSessionUserID());

            BindOwnerExp();


        }
        catch { }

    }
    



    protected void gvOwnerExpenses_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvOwnerExpenses.PageIndex = e.NewPageIndex;
        if (ViewState["HideMatched"] != null)
        {
            HideMatched = Convert.ToBoolean(ViewState["HideMatched"]);
            BindOwnerExp();
        }
    }
    protected void btnHideShow_Click(object sender, EventArgs e)
    {
        HideMatched =Convert.ToBoolean(ViewState["HideMatched"]);
        if (HideMatched)
        {

            HideMatched = false;
            ViewState["HideMatched"] = HideMatched;
            BindOwnerExp();
            btnHideShow.Text = "Show All";
        }
        else
        {
            HideMatched = true;
            ViewState["HideMatched"] = HideMatched;
            BindOwnerExp();
            btnHideShow.Text = "Hide Mathched";
        }
       
    }
    protected void gvOwnerExpenses_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                GridViewRow gvr = e.Row;
                string IsApprover = "";
                string InvStatus = "";
                ImageButton ibtnApprove = (ImageButton)gvr.FindControl("ibtnApprove");
                ImageButton ibtnUnApprove = (ImageButton)gvr.FindControl("ibtnUnApprove");
                ImageButton ibtnCompare = (ImageButton)gvr.FindControl("ibtnCompare");
                ImageButton ibtnUnmatch = (ImageButton)gvr.FindControl("ibtnUnmatch");
                HiddenField hdnapprover_UserID = (HiddenField)gvr.FindControl("hdnapprover_UserID");
                HiddenField hdnIsApprover = (HiddenField)gvr.FindControl("hdnIsApprover");
                HiddenField hdnJournalId = (HiddenField)gvr.FindControl("hdnJournalId");
                
                Label lblInvStatus = (Label)gvr.FindControl("lblInvStatus");
                Label lblAllocated = (Label)gvr.FindControl("lblAllocated");
                double AllocatedAmt= Convert.ToDouble(lblAllocated.Text);
                InvStatus = lblInvStatus.Text;
                IsApprover =hdnIsApprover.Value;

                if (IsApprover == "1")
                {
                    if (InvStatus == "Verified")
                    {
                        ibtnApprove.Visible = true;
                        ibtnUnApprove.Visible = false;
                        ibtnCompare.Visible = true;
                    }
                    else if ((AllocatedAmt == 0 && InvStatus == "Approved") || (AllocatedAmt == 0 && (hdnJournalId.Value == null || hdnJournalId.Value == "0")))
                    {
                        ibtnUnApprove.Visible = true;
                    }
                    else if (InvStatus.ToUpper() == "OFFSET")
                        ibtnUnmatch.Visible = true;
                }
            }
        }
        catch { }
    }

}
   
