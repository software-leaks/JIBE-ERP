using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using SMS.Business.Infrastructure;
using SMS.Business.ASL;
using SMS.Business.POLOG;
using SMS.Properties;

public partial class PO_LOG_Approval_Setting : System.Web.UI.Page
{
    MergeGridviewHeader_Info objChangeReqstMerge = new MergeGridviewHeader_Info();
    MergeGridviewHeader_Info objChangeReqstMerge1 = new MergeGridviewHeader_Info();
    UserAccess objUA = new UserAccess();
    public string OperationMode = "";
    public Boolean uaEditFlag = true;
    public Boolean uaDeleteFlage = false;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                //UserAccessValidation();
                BindDropDownlist();
                BindPOType();
                BindPOGroup();
            }

        }
        catch (Exception ex)
        {
            string js = "alert('" + ex.Message + "')";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUser", js, true);
        }

    }
    protected void BindPOType()
    {
        try
        {
            DataSet ds = BLL_POLOG_Register.POLOG_Get_Type(UDFLib.ConvertToInteger(Session["UserID"].ToString()), "PO_TYPE");
            if (ds.Tables[1].Rows.Count > 0)
            {
                ddlPOType.DataSource = ds.Tables[1];
                ddlPOType.DataTextField = "VARIABLE_NAME";
                ddlPOType.DataValueField = "VARIABLE_CODE";
                ddlPOType.DataBind();
                ddlPOType.Items.Insert(0, new ListItem("-Select-", "0"));

                ddlPOTypefilter.DataSource = ds.Tables[1];
                ddlPOTypefilter.DataTextField = "VARIABLE_NAME";
                ddlPOTypefilter.DataValueField = "VARIABLE_CODE";
                ddlPOTypefilter.DataBind();
                ddlPOTypefilter.Items.Insert(0, new ListItem("-Select-", "0"));
            }
            else
            {
                ddlPOType.Items.Insert(0, new ListItem("-Select-", "0"));
                ddlPOTypefilter.Items.Insert(0, new ListItem("-Select-", "0"));
            }
            //
          
        }
        catch { }
        {
        }
    }
    public void BindDropDownlist()
    {
        try
        {
            DataSet ds1 = BLL_POLOG_Register.POLOG_Get_AccountClassification(UDFLib.ConvertIntegerToNull(GetSessionUserID()), ddlPOTypefilter.SelectedValue);
            ddlAccClassifictaion.DataSource = ds1.Tables[0];
            ddlAccClassifictaion.DataTextField = "VARIABLE_NAME";
            ddlAccClassifictaion.DataValueField = "VARIABLE_CODE";
            ddlAccClassifictaion.DataBind();
            ddlAccClassifictaion.Items.Insert(0, new ListItem("-All-", "0"));
        }
        catch { }
        {
        }
    }
    protected void BindPOGroup()
    {
        int rowcount = ucCustomPagerItems.isCountRecord;

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

        ViewState["VARIABLE_CODE"] = "";
        DataSet ds = BLL_POLOG_Lib.Get_Approval_Group(UDFLib.ConvertStringToNull(txtSearchGroup.Text), UDFLib.ConvertStringToNull(ddlPOTypefilter.SelectedValue), UDFLib.ConvertStringToNull(ddlAccClassifictaion.SelectedValue), GetSessionUserID(),sortbycoloumn, sortdirection
             , ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);

        if (ucCustomPagerItems.isCountRecord == 1)
        {
            ucCustomPagerItems.CountTotalRec = rowcount.ToString();
            ucCustomPagerItems.BuildPager();
        }

        if (ds.Tables[0].Rows.Count > 0)
        {
            gvGroup.DataSource = ds.Tables[0];
            gvGroup.DataBind();
            gvGroup.Rows[0].BackColor = System.Drawing.Color.Yellow;
            //Session["Approval_Group_Code"] = ds.Tables[0].Rows[0]["Approval_Group_Code"].ToString();
            txtGroupCode.Text = ds.Tables[0].Rows[0]["Approval_Group_Code"].ToString();
            txtPOType.Text = ds.Tables[0].Rows[0]["VARIABLE_CODE"].ToString();
            //Session["Approval_Group_Code"] = ds.Tables[0].Rows[0]["Approval_Group_Code"].ToString();
            BindDepartmentList(txtGroupCode.Text.ToString(), txtPOType.Text.ToString(), GetSessionUserID());
            BindLimit(txtGroupCode.Text.ToString());
        }
        else
        {
            gvGroup.DataSource = null;
            gvGroup.DataBind();
            //txtGroupCode.Text = ds.Tables[0].Rows[0]["Approval_Group_Code"].ToString();
            //BindDepartmentList(ds.Tables[0].Rows[0]["Approval_Group_Code"].ToString(), ds.Tables[0].Rows[0]["VARIABLE_CODE"].ToString(), GetSessionUserID());
            //BindLimit(ds.Tables[0].Rows[0]["Approval_Group_Code"].ToString());
        }
    }
    protected void BindDepartmentList(string GroupID, string PO_Type,int userid )
    {
        try
        {
            DataSet ds_Depart = BLL_POLOG_Lib.Get_Approval_Group_Item(GroupID, UDFLib.ConvertStringToNull(PO_Type));
            gvAccount.DataSource = ds_Depart.Tables[0];
            gvAccount.DataBind();

            String User_Type = "OFFICE USER";
            DataSet ds = BLL_POLOG_Lib.POLOG_Get_Approval_Group_User(UDFLib.ConvertStringToNull(GroupID), User_Type);


            gvUser.DataSource = ds.Tables[0];
            gvUser.DataBind();
            


        }
        catch { }
        {
        }
    }
    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        SMS.Properties.UserAccess objUA = new SMS.Properties.UserAccess();
        BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();

        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.IsAdmin == 0)
        {
            if (objUA.View == 0)
                Response.Redirect("~/default.aspx?msgid=1");

            if (objUA.Add == 0)
            {
                Response.Redirect("~/default.aspx?msgid=2");
            }
            if (objUA.Edit == 0)
            {
                Response.Redirect("~/default.aspx?msgid=3");
            }
            if (objUA.Delete == 0)
            {
                Response.Redirect("~/default.aspx?msgid=4");
            }
            if (objUA.Approve == 0)
            {
                Response.Redirect("~/default.aspx?msgid=5");
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

    //private void GenerateUL(DataSet ds)
    //{
    //    //DataRow[] drs = ds.Tables["0"].Select("Menu_Type is null");
    //    DataRow[] drs = ds.Tables[0].Select("Menu_Type is null");
    //    int meni = 0;
    //    int child = 0;
    //    ViewState["VARIABLE_CODE"] = "";
    //    foreach (DataRow dr in drs)
    //    {
    //        TreeNode mi = new TreeNode(dr["VARIABLE_NAME"].ToString().Trim(), dr["VARIABLE_CODE"].ToString());
            
    //        if (dr["VARIABLE_CODE"].ToString() != ViewState["VARIABLE_CODE"].ToString())
    //        {
    //            TreeView1.Nodes.Add(mi);
    //            ViewState["VARIABLE_CODE"] = dr["VARIABLE_CODE"].ToString();
    //            DataRow[] drInners = ds.Tables[0].Select("VARIABLE_CODE ='" + dr["VARIABLE_CODE"].ToString() + "' ");
    //            if (drInners.Length != 0)
    //            {
    //                child = 0;
    //                foreach (DataRow drInner in drInners)
    //                {
    //                    TreeNode miner;
    //                    miner = new TreeNode(drInner["Approval_Group_Name"].ToString().Trim(), drInner["Approval_Group_Code"].ToString());

    //                    TreeView1.Nodes[meni].ChildNodes.Add(miner);
    //                    TreeView1.Nodes[meni].CollapseAll();
    //                    child++;
    //                }
    //            }
    //            meni++;
    //        }
    //    }
  
    //}


    //protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
    //{
    //    Load_POType();
    //}

    //protected void Load_POType()
    //{

    //    if (TreeView1.SelectedNode != null)
    //    {
    //        //string[] nodevalue1 = TreeView1.SelectedNode.Parent.
    //        string[] NodeValue = TreeView1.SelectedNode.Value.Split(',');
    //        string variable_Type = UDFLib.ConvertStringToNull(NodeValue[0]);
    //        string userid = "";
            
    //        BindDepartmentList(variable_Type, userid, Convert.ToInt32(Session["USERID"].ToString()));
            
    //    }

    //}
    protected void gvGroup_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblVARIABLE_NAME = (Label)e.Row.FindControl("lblVARIABLE_NAME");
            Label lblVARIABLE_CODE = (Label)e.Row.FindControl("lblVARIABLE_CODE");
            Label lbltxt = (Label)e.Row.FindControl("lbltxt");
            Label lbltxt1 = (Label)e.Row.FindControl("lbltxt1");
            string VARIABLE_CODE = DataBinder.Eval(e.Row.DataItem, "VARIABLE_CODE").ToString();
           
            if (VARIABLE_CODE.ToString() == ViewState["VARIABLE_CODE"].ToString())
            {
                lblVARIABLE_NAME.Visible = false;
                lblVARIABLE_CODE.Visible = false;
                lbltxt.Visible = false;
                lbltxt1.Visible = false;
            }
            else
            {
                ViewState["VARIABLE_CODE"] = VARIABLE_CODE.ToString();
            }
        }
    }
    protected void onUpdate(object source, CommandEventArgs e)
    {

        HiddenFlag.Value = "Edit";
        OperationMode = "Edit Approval Group";
        lblMsg.Visible = false;
        DataTable dt = new DataTable();
        dt = BLL_POLOG_Lib.Get_Approval_Group_List(Convert.ToString(e.CommandArgument.ToString()));
        //dt.DefaultView.RowFilter = "Group_ID= '" + e.CommandArgument.ToString() + "'";

        txtGroupID.Text = dt.Rows[0]["Approval_Group_Code"].ToString();
        txtGroupName.Text = dt.Rows[0]["Approval_Group_Name"].ToString();
        ddlPOType.SelectedValue = dt.Rows[0]["Req_Type"].ToString();
        string AddUserTypemodal = String.Format("showModal('divadd',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddGroupmodal", AddUserTypemodal, true);

    }
    protected void onView(object source, CommandEventArgs e)
    {

        HiddenFlag.Value = "View";
        OperationMode = "View Approval Group";
        string[] arg = e.CommandArgument.ToString().Split(',');
        //string Group_Code = arg[0].ToString();
        txtGroupCode.Text = arg[0].ToString();
        txtPOType.Text = arg[1].ToString();
        //Session["Approval_Group_Code"] = arg[0].ToString();
        BindDepartmentList(txtGroupCode.Text.ToString(), txtPOType.Text.ToString(), GetSessionUserID());
        BindLimit(txtGroupCode.Text.ToString());
        foreach (GridViewRow row in gvGroup.Rows)
        {
            String ID = gvGroup.DataKeys[row.RowIndex].Values[1].ToString();

            if (txtGroupCode.Text.ToString() == ID)
            {
                row.BackColor = System.Drawing.Color.Yellow;
            }
            else
            {
                row.BackColor = System.Drawing.Color.White;
            }
        }
    }

    protected void ClearField()
    {
        txtGroupName.Text = "";
        txtGroupID.Text = "";
        lblMsg.Text = "";
    }
    protected void btnFilter_Click(object sender, EventArgs e)
    {
       
        BindPOGroup();
    }
    protected void gvAccount_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string Active_Status = DataBinder.Eval(e.Row.DataItem, "Active_Status").ToString();
            string Selected = DataBinder.Eval(e.Row.DataItem, "Selected").ToString();
            CheckBox chkselect = (CheckBox)e.Row.FindControl("chkSelect");
            if (Selected.ToString() == "1")
            {
                chkselect.Enabled = false;
                chkselect.Checked = false;
            }
            if (Active_Status.ToString() == "1")
            {
                chkselect.Enabled = true;
                e.Row.Cells[0].BackColor = System.Drawing.Color.Yellow;
                chkselect.Checked = true;
            }
           
        }
    }
    protected void gvUser_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblUserName = (Label)e.Row.FindControl("lblUser_Name");
            CheckBox chkselectCreator = (CheckBox)e.Row.FindControl("chkInvoice_Creator");
            CheckBox chkselectApprover = (CheckBox)e.Row.FindControl("chkInvoice_Approver");
            if (chkselectCreator.Checked == true)
            {
                lblUserName.Font.Bold = true;
                //e.Row.Cells[0].BackColor = System.Drawing.Color.Yellow;
                //e.Row.BackColor = System.Drawing.Color.Yellow;
            }
            if (chkselectApprover.Checked == true)
            {
                lblUserName.Font.Bold = true;
                e.Row.Cells[0].BackColor = System.Drawing.Color.Yellow;
                //e.Row.BackColor = System.Drawing.Color.Yellow;
            }
        }
    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
        string retval = "";
        if (HiddenFlag.Value == "Add")
        {
            retval = BLL_POLOG_Lib.INS_Approval_Group(txtGroupID.Text.Trim(), txtGroupName.Text.Trim(), ddlPOType.SelectedValue, Convert.ToInt32(Session["USERID"]));
        }
        else
        {
            retval = BLL_POLOG_Lib.INS_Approval_Group(txtGroupID.Text.Trim(), txtGroupName.Text.Trim(), ddlPOType.SelectedValue, Convert.ToInt32(Session["USERID"]));
        }
        if (retval == "" || retval == null)
        {
            lblMsg.Visible = true;
            lblMsg.Text = "Group Name already exists!.";
            string SupplierScope = String.Format("showModal('divadd',false);");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "GroupName", SupplierScope, true);
        }
        else
        {
            BindPOGroup();

            string hidemodal = String.Format("hideModal('divadd')");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hidemodal", hidemodal, true);
        }

    }
    protected void btnAddGroup_Click(object sender, EventArgs e)
    {
        this.SetFocus("ctl00_MainContent_txtGroupName");
        HiddenFlag.Value = "Add";
        OperationMode = "Add Group Name";

        ClearField();

        string SupplierScope = String.Format("showModal('divadd',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "GroupName", SupplierScope, true);
    }
   
    protected void btnSaveItem_Click(object sender, EventArgs e)
    {
        int Retval =  SaveDepartmentList();
        if (Retval >= 0)
        {
            string message = "alert('Changes Saved and Updated')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
        BindDepartmentList(txtGroupCode.Text.ToString(), txtPOType.Text.ToString(), GetSessionUserID());
        BindLimit(txtGroupCode.Text.ToString());
    }
    protected int SaveDepartmentList()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("PKID");
        dt.Columns.Add("FKID");
        dt.Columns.Add("Value");
        //int iInvoiceCreator = 0;
        //int iInvoiceApprover = 0;
        //int? iUserID = 0;
        foreach (GridViewRow row in gvAccount.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                if (((CheckBox)row.FindControl("chkSelect")).Checked == true)
                {
                    DataRow dr = dt.NewRow();
                    dr["FKID"] = gvAccount.DataKeys[row.RowIndex].Value.ToString();
                    dr["Value"] = (((CheckBox)row.FindControl("chkSelect")).Checked == true) ? 1 : 0;
                     dt.Rows.Add(dr);
                }
            }
        }
        DataTable dtUser = new DataTable();
        dtUser.Columns.Add("PKID");
        dtUser.Columns.Add("UserID");
        dtUser.Columns.Add("InvoiceCreator");
        dtUser.Columns.Add("InvoiceApprover");
        foreach (GridViewRow row in gvUser.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                if (((CheckBox)row.FindControl("chkInvoice_Creator")).Checked == true || ((CheckBox)row.FindControl("chkInvoice_Approver")).Checked == true)
                {
                    DataRow dr = dtUser.NewRow();
                    dr["UserID"] = UDFLib.ConvertIntegerToNull(gvUser.DataKeys[row.RowIndex].Value.ToString());
                    dr["InvoiceCreator"] = (((CheckBox)row.FindControl("chkInvoice_Creator")).Checked == true) ? 1 : 0;
                    dr["InvoiceApprover"] = (((CheckBox)row.FindControl("chkInvoice_Approver")).Checked == true) ? 1 : 0;
                    dtUser.Rows.Add(dr);
                }
            }

        }
       int retval =  BLL_POLOG_Lib.INS_Approval_Group_Item(UDFLib.ConvertStringToNull(txtGroupCode.Text.ToString()), dt, dtUser, GetSessionUserID());
       return retval;
    }
    public void BindLimit(string Group_Code)
    {
        objChangeReqstMerge.AddMergedColumns(new int[] { 2, 3, 4,5 }, "Approver", "HeaderStyle-center");
       
        DataTable dt = BLL_POLOG_Lib.POLOG_Get_Approval_Limit_Search(Group_Code, GetSessionUserID());
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
    protected void gvLimit_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            MergeGridviewHeader.SetProperty(objChangeReqstMerge);
            e.Row.SetRenderMethodDelegate(MergeGridviewHeader.RenderHeader);
        }
    }
    protected void onUpdate1(object source, CommandEventArgs e)
    {

        HiddenFlag.Value = "Edit";
        OperationMode = "Edit Approval Group";
        lblMsg.Visible = false;
        DataTable dt = new DataTable();
        dt = BLL_POLOG_Lib.Get_Approval_Group_List(Convert.ToString(e.CommandArgument.ToString()));
        //dt.DefaultView.RowFilter = "Group_ID= '" + e.CommandArgument.ToString() + "'";

        txtGroupID.Text = dt.Rows[0]["Approval_Group_Code"].ToString();
        txtGroupName.Text = dt.Rows[0]["Approval_Group_Name"].ToString();
        ddlPOType.SelectedValue = dt.Rows[0]["Req_Type"].ToString();
        string AddUserTypemodal = String.Format("showModal('divadd',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddGroupmodal", AddUserTypemodal, true);

    }
    protected void ddlPOTypefilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindDropDownlist();
    }
}