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
using SMS.Business.INFRA;
using SMS.Business.Infrastructure;
using SMS.Properties;
using SMS.Business.POLOG;

public partial class PO_LOG_POLOG_Limit_Entry : System.Web.UI.Page
{
    BLL_Infra_UserType objBLL = new BLL_Infra_UserType();
    BLL_Infra_UserCredentials objBLLUser = new BLL_Infra_UserCredentials();
    //BLL_Infra_Approval_Limit objBLLAppLimit = new BLL_Infra_Approval_Limit();
    //BLL_Infra_Approval_Group_Department objBllGroup = new BLL_Infra_Approval_Group_Department();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Load_ApprovalGroup();
            //Load_UserList();
            //BindPoApprover();
            if (!String.IsNullOrEmpty(Request.QueryString["Limit_ID"].ToString()))
            {
                txtLimit.Text = Request.QueryString["Limit_ID"].ToString();
                BindGroupDeatils(UDFLib.ConvertStringToNull(Request.QueryString["Limit_ID"].ToString()));
            }
        }
    }
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }
    public void Load_ApprovalGroup()
    {
        int Type = 0;
        string Group_Code = null;
        DataSet ds2 = BLL_POLOG_Register.POLOG_Get_AccountClassification(UDFLib.ConvertIntegerToNull(GetSessionUserID()), null);
        DDLGroup.DataSource = ds2.Tables[1];
        DDLGroup.DataTextField = "Approval_Group_Name";
        DDLGroup.DataValueField = "Approval_Group_Code";
        DDLGroup.DataBind();
        DDLGroup.Items.Insert(0, new ListItem("-Select-", "0"));

    }


    public void Load_UserList()
    {

        DataTable dt = objBLLUser.Get_UserList_ApprovalLimit();

        ddlAdvanceApprover.DataSource = dt;
        ddlAdvanceApprover.DataTextField = "UserName";
        ddlAdvanceApprover.DataValueField = "UserID";
        ddlAdvanceApprover.DataBind();
        ddlAdvanceApprover.Items.Insert(0, new ListItem("-Select-", "0"));

    }
    void BindGroupDeatils(string Limit_ID)
    {
        try
        {
           
            DataSet ds = BLL_POLOG_Lib.POLOG_Get_Approval_Limit(Limit_ID);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DDLGroup.SelectedValue = ds.Tables[0].Rows[0]["Approval_Group_Code"].ToString();
                txtLimit.Text = ds.Tables[0].Rows[0]["Approval_Limit_ID"].ToString();
                txtMinApprovalLimit.Text = ds.Tables[0].Rows[0]["Min_Amt"].ToString();
                txtMaxApprovalLimit.Text = ds.Tables[0].Rows[0]["Max_Amt"].ToString();
                BindApprovalGroupuser(Limit_ID, DDLGroup.SelectedValue);
                ddlAdvanceApprover.SelectedValue = ds.Tables[0].Rows[0]["USER_ID"].ToString();
            }
            
            if (DDLGroup.SelectedIndex > 0)
            {
                BindGroup();
            }

        }
        catch { }
        {
        }
    }
    protected void BindGroup()
    {
        try
        {
            int rowcount = ucCustomPagerItems.isCountRecord;

            string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
            int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());


            DataTable dt = BLL_POLOG_Lib.POLOG_Get_Approval_Limit_Search(null, UDFLib.ConvertStringToNull(DDLGroup.SelectedValue)
                , 0, sortbycoloumn, sortdirection
                 , ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);


            if (ucCustomPagerItems.isCountRecord == 1)
            {
                ucCustomPagerItems.CountTotalRec = rowcount.ToString();
                ucCustomPagerItems.BuildPager();
            }


            if (dt.Rows.Count > 0)
            {
                gvLimit.DataSource = dt;
                gvLimit.DataBind();
            }
            else
            {
                gvLimit.DataSource = dt;
                gvLimit.DataBind();
            }
        }
        catch { }
        {
        }
    }
    protected void BindApprovalGroupuser(string Limit_ID,string Group_Code)
    {
        try
        {
            DataSet ds1 = BLL_POLOG_Lib.POLOG_Get_Approval_Limit(Limit_ID, UDFLib.ConvertStringToNull(Group_Code));

            if (ds1.Tables[0].Rows.Count > 0)
            {
                gvUser.DataSource = ds1.Tables[0];
                gvUser.DataBind();
                lblPOType.Text = ds1.Tables[0].Rows[0]["PO_Name"].ToString();
                ddlAdvanceApprover.Items.Clear();
                ddlAdvanceApprover.DataSource = ds1.Tables[0];
                ddlAdvanceApprover.DataTextField = "User_Name";
                ddlAdvanceApprover.DataValueField = "UserID";
                ddlAdvanceApprover.DataBind();
                ddlAdvanceApprover.Items.Insert(0, new ListItem("-Select-", "0"));
            }
            else
            {
                gvUser.DataSource = ds1.Tables[0];
                gvUser.DataBind();
                ddlAdvanceApprover.Items.Insert(0, new ListItem("-Select-", "0"));
            }
        }
        catch { }
        {
        }
    }
   
    protected void btnDraft_Click(object sender, EventArgs e)
    {
        string responseid = BLL_POLOG_Lib.POLOG_INS_Approval_Limit(UDFLib.ConvertStringToNull(txtLimit.Text), UDFLib.ConvertStringToNull(DDLGroup.SelectedValue), UDFLib.ConvertIntegerToNull(ddlAdvanceApprover.SelectedValue),
              UDFLib.ConvertDecimalToNull(txtMinApprovalLimit.Text), UDFLib.ConvertDecimalToNull(txtMaxApprovalLimit.Text), UDFLib.ConvertIntegerToNull(GetSessionUserID()));
        if (responseid != "0")
        {
            
            txtLimit.Text = Convert.ToString(responseid);
            SaveApprover();
            string msgDraft = String.Format("parent.ReloadParent_ByButtonID();");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgDraft", msgDraft, true);
        }
        else
        {
            string message = "alert('Approval limit already exist for this amount.')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);  
        }
    }
    protected void SaveApprover()
    {
        DataTable dtUser = new DataTable();
        dtUser.Columns.Add("PKID");
        dtUser.Columns.Add("UserID");
        dtUser.Columns.Add("POApprover");
        dtUser.Columns.Add("InvoiceApprover");
        dtUser.Columns.Add("InvoiceFinalApprover");
        foreach (GridViewRow row in gvUser.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                //if (((CheckBox)row.FindControl("chkInvoice_Creator")).Checked == true && ((CheckBox)row.FindControl("chkInvoice_Approver")).Checked == true)
                //{
                    DataRow dr = dtUser.NewRow();
                    dr["UserID"] = UDFLib.ConvertIntegerToNull(gvUser.DataKeys[row.RowIndex].Value.ToString());
                    dr["POApprover"] = (((CheckBox)row.FindControl("chkPO_Approver")).Checked == true) ? 1 : 0;
                    dr["InvoiceApprover"] = (((CheckBox)row.FindControl("chkInvoice_Approver")).Checked == true) ? 1 : 0;
                    dr["InvoiceFinalApprover"] = (((CheckBox)row.FindControl("chkFinalInvoice_Approver")).Checked == true) ? 1 : 0;
                    dtUser.Rows.Add(dr);
                //}
            }

        }
        BLL_POLOG_Lib.POLOG_INS_Approval_Limit_Approver(UDFLib.ConvertStringToNull(txtLimit.Text.ToString()), dtUser, UDFLib.ConvertIntegerToNull(GetSessionUserID()));
    }

    protected void DDLGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindApprovalGroupuser(null,UDFLib.ConvertStringToNull(DDLGroup.SelectedValue));
        if (DDLGroup.SelectedIndex > 0)
        {
            BindGroup();
        }
    }
    protected void btnEdit_Click(object sender, CommandEventArgs e)
    {
        string[] arg = e.CommandArgument.ToString().Split(',');
        string Limit_ID = UDFLib.ConvertStringToNull(arg[0]);
        foreach (GridViewRow row in gvLimit.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                string LimitID = UDFLib.ConvertStringToNull(gvLimit.DataKeys[row.RowIndex].Value.ToString());
                if (LimitID == Limit_ID)
                {
                    row.BackColor = System.Drawing.Color.Yellow;
                }
                else
                {
                    row.BackColor = System.Drawing.Color.White;
                }
            }
        }
        BindApprovalLimitDeatils(UDFLib.ConvertStringToNull(Limit_ID));
    }
    protected void gvLimit_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
           
            string ID = DataBinder.Eval(e.Row.DataItem, "Limit_ID").ToString();
            if (ID == txtLimit.Text)
            {
                e.Row.BackColor = System.Drawing.Color.Yellow;
            }
        }
    }
    void BindApprovalLimitDeatils(string Limit_ID)
    {
        try
        {
          
            DataSet ds = BLL_POLOG_Lib.POLOG_Get_Approval_Limit(Limit_ID);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DDLGroup.SelectedValue = ds.Tables[0].Rows[0]["Approval_Group_Code"].ToString();
                txtLimit.Text = ds.Tables[0].Rows[0]["Approval_Limit_ID"].ToString();
                txtMinApprovalLimit.Text = ds.Tables[0].Rows[0]["Min_Amt"].ToString();
                txtMaxApprovalLimit.Text = ds.Tables[0].Rows[0]["Max_Amt"].ToString();
                BindApprovalGroupuser(Limit_ID, DDLGroup.SelectedValue);
                ddlAdvanceApprover.SelectedValue = ds.Tables[0].Rows[0]["USER_ID"].ToString();
            }
           

        }
        catch { }
        {
        }
    }
}