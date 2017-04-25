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

public partial class ASL_ASL_Evalution_History : System.Web.UI.Page
{
    UserAccess objUA = new UserAccess();
    protected void Page_Load(object sender, EventArgs e)
    {
        UserAccessValidation();
        if (!IsPostBack)
        {
            ViewState["ReturnEvalutionID"] = 0;
            
            //BindProposeStatus();
            BindScope();
            BindPort();
            BindLastSupplier(GetEvaluation_ID());
        }
    }
    protected void BindScope()
    {
        DataSet ds = BLL_ASL_Supplier.Get_Supplier_Scope(UDFLib.ConvertStringToNull(GetSuppID()));
        
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

    protected void BindApproverList(string Supplier_Type)
    {
        try
        {
            //DataSet ds = BLL_ASL_Supplier.Get_Supplier_ApproverList(UDFLib.ConvertToInteger(GetSessionUserID()), Supplier_Type);

            //ddlFinalApproverName.DataSource = ds.Tables[0];
            //ddlFinalApproverName.DataValueField = "ApproveID";
            //ddlFinalApproverName.DataTextField = "User_name";
            //ddlFinalApproverName.DataBind();
            //ddlFinalApproverName.Items.Insert(0, new ListItem("SELECT", "0"));


            //ddlApproverName.DataSource = ds.Tables[1];
            //ddlApproverName.DataValueField = "ApproveID";
            //ddlApproverName.DataTextField = "User_name";
            //ddlApproverName.DataBind();
            //ddlApproverName.Items.Insert(0, new ListItem("SELECT", "0"));
        }
        catch
        {
        }
    }
    //protected void BindProposeStatus()
    //{
    //    DataTable dt = BLL_ASL_Supplier.Get_ASL_System_Parameter(22, "", UDFLib.ConvertToInteger(GetSessionUserID()));

    //    ddlProposedStatus.DataSource = dt;
    //    ddlProposedStatus.DataTextField = "Name";
    //    ddlProposedStatus.DataValueField = "Name";
    //    ddlProposedStatus.DataBind();
    //}
    protected void BindLastSupplier(int Evaluation_ID)
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

            if (ds.Tables[2].Rows[0]["Record_Status"].ToString() != "")
            {
                btnRegisteredData.Visible = true;
            }
            else
            {
                btnRegisteredData.Visible = false;
            }
           
        }
        if (ds.Tables[0].Rows.Count > 0)
        {
            txtSupplierDesc.Text = ds.Tables[0].Rows[0]["Supplier_Description"].ToString();
            ViewState["ReturnEvalutionID"] = ds.Tables[0].Rows[0]["Evaluation_ID"].ToString();
            txtProposedStatus.Text = ds.Tables[0].Rows[0]["Supp_Status"].ToString();
            txtPeriod.Text = ds.Tables[0].Rows[0]["Period"].ToString();
            txtRemarks.Text = ds.Tables[0].Rows[0]["Justification_Remarks"].ToString();
            txtVerificationRemrks.Text = ds.Tables[0].Rows[0]["Verifier_Remarks"].ToString();
            txtApprovalRemrks.Text = ds.Tables[0].Rows[0]["Approver_Remarks"].ToString();

            txtCreatedDate.Text = ds.Tables[0].Rows[0]["Created_Date"].ToString();
            txtApprovedDate.Text = ds.Tables[0].Rows[0]["Approved_Date"].ToString();
            txtFinalApprovedDate.Text = ds.Tables[0].Rows[0]["FinalApproved_Date"].ToString();
            txtFinalApproverName.Text = ds.Tables[0].Rows[0]["ForFinalApproval"].ToString();
            txtApproverName.Text = ds.Tables[0].Rows[0]["ForApproval"].ToString();
           
            lblEvalStatus.Text = ds.Tables[0].Rows[0]["Evaluation_Status"].ToString();
            lblCreatedby.Text = ds.Tables[0].Rows[0]["Created_Name"].ToString(); 
        }

        if (ds.Tables[1].Rows.Count > 0)
        {
           
            gvEvalHistory.DataSource = ds.Tables[1];
            gvEvalHistory.DataBind();
            gvEvalHistory.SelectedIndex = 0;
        }
        else
        {
           
            gvEvalHistory.DataSource = ds.Tables[1];
            gvEvalHistory.DataBind();
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
    private string GetSessionSupplierType()
    {
        if (Session["Supplier_Type"] != null)
            return Session["Supplier_Type"].ToString();
        else
            return null;
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
        {
            pnlEvaluation.Visible = false;
            lblMsg.Text = "You don't have sufficient previlege to access the requested information.";
        }
        else
        {
            pnlEvaluation.Visible = true;
        }
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
    protected void btnRegisteredData_Click(object sender, EventArgs e)
    {
        string SupplierID = GetSuppID();
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "javascript:window.open('ASL_Data_Entry.aspx?Supp_ID=" + SupplierID + "');", true);
    }
    protected void btnView_Click(object s, CommandEventArgs e)
    {
        string[] arg = e.CommandArgument.ToString().Split(',');
        //int? EvalationID =
        ViewState["EvalID"] = arg[1];
        txtEvalID.Text = arg[1];
        foreach (GridViewRow row in gvEvalHistory.Rows)
        {
            //storyGridView.DataKeys[row.RowIndex].Values[0].ToString();
            int ID = Convert.ToInt16(gvEvalHistory.DataKeys[row.RowIndex].Values[1].ToString());
            if (Convert.ToInt16(txtEvalID.Text) == ID)
            {
                row.BackColor = System.Drawing.Color.Yellow;
            }
            else
            {
                row.BackColor = System.Drawing.Color.White;
            }
        }
        BindEvaluation();
    }

    protected void BindEvaluation()
    {
        int? EvalID = UDFLib.ConvertIntegerToNull(ViewState["EvalID"]);
        string Supp_ID = GetSuppID();
        DataSet ds = BLL_ASL_Supplier.Get_Supplier_Evaluation_List(UDFLib.ConvertStringToNull(Supp_ID), EvalID);

        if (ds.Tables[0].Rows.Count > 0)
        {

            ViewState["ReturnEvalutionID"] = ds.Tables[0].Rows[0]["Evaluation_ID"].ToString();
            txtProposedStatus.Text = ds.Tables[0].Rows[0]["Supp_Status"].ToString();
            txtPeriod.Text = ds.Tables[0].Rows[0]["Period"].ToString();
            txtRemarks.Text = ds.Tables[0].Rows[0]["Justification_Remarks"].ToString();
            txtVerificationRemrks.Text = ds.Tables[0].Rows[0]["Verifier_Remarks"].ToString();
            txtApprovalRemrks.Text = ds.Tables[0].Rows[0]["Approver_Remarks"].ToString();
            txtCreatedDate.Text = ds.Tables[0].Rows[0]["Created_Date"].ToString();
            txtApprovedDate.Text = ds.Tables[0].Rows[0]["Approved_Date"].ToString();
            txtFinalApprovedDate.Text = ds.Tables[0].Rows[0]["FinalApproved_Date"].ToString();
            txtFinalApproverName.Text = ds.Tables[0].Rows[0]["ForFinalApproval"].ToString();
            txtApproverName.Text = ds.Tables[0].Rows[0]["ForApproval"].ToString();
            // ddlFinalApproverName.SelectedValue = ds.Tables[0].Rows[0]["SentForfinalApproval"].ToString();
            lblEvalStatus.Text = ds.Tables[0].Rows[0]["Evaluation_Status"].ToString();
            lblCreatedby.Text = ds.Tables[0].Rows[0]["Created_Name"].ToString();
          

        }
       
    }
}