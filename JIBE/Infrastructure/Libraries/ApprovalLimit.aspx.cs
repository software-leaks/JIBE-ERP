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
using System.Collections.Generic;
using SMS.Business.Infrastructure;
using SMS.Properties;

public partial class ApprovalLimit : System.Web.UI.Page
{
    BLL_Infra_UserType objBLL = new BLL_Infra_UserType();
    BLL_Infra_Country objBLLCountry = new BLL_Infra_Country();
    BLL_Infra_UserCredentials objBLLUser = new BLL_Infra_UserCredentials();
    MergeGridviewHeader_Info objChangeReqstMerge = new MergeGridviewHeader_Info();
    BLL_Infra_Approval_Limit objBLLAppLimit = new BLL_Infra_Approval_Limit();
    UserAccess objUA = new UserAccess();
    int AmountApplicable = 0;

    DataTable dtUserApprovalList = new DataTable();
    public string OperationMode = "";
    public Boolean uaEditFlag = false;
    public Boolean uaDeleteFlage = false;


    protected void Page_Load(object sender, EventArgs e)
    {
        UserAccessValidation();
        if (!IsPostBack)
        {
            ViewState["SORTDIRECTION"] = null;
            ViewState["SORTBYCOLOUMN"] = null;

            ucCustomPagerItems.PageSize = 20;

            Load_ApprovalGroup();
            Load_UserList();
            Load_ApprovalType();
            BindUserApproval();
        }
        objChangeReqstMerge.AddMergedColumns(new int[] { 2, 3 }, "Approvel Limit", "HeaderStyle-css");
        objChangeReqstMerge.AddMergedColumns(new int[] {4, 5, 6, 7 }, "Approver Type", "HeaderStyle-css");
        
    }



    public void Load_ApprovalGroup()
    {
        DataTable dt = objBLLAppLimit.Get_Approval_Group();


        DDLGroupFilter.DataSource = dt;
        DDLGroupFilter.DataTextField = "Group_Name";
        DDLGroupFilter.DataValueField = "Group_ID";
        DDLGroupFilter.DataBind();
        DDLGroupFilter.Items.Insert(0, new ListItem("-ALL-", "0"));
        

        DDLGroup.DataSource = dt;
        DDLGroup.DataTextField = "Group_Name";
        DDLGroup.DataValueField = "Group_ID";
        DDLGroup.DataBind();
        DDLGroup.Items.Insert(0, new ListItem("-Select-", "0"));
        
    }

    public void Load_ApprovalType()
    {
        BLL_Infra_Approval_Type objBLLAppType = new BLL_Infra_Approval_Type();
        DataTable dt = objBLLAppType.Get_Approval_Type(null);
        ddlType.DataSource = dt;
        ddlType.DataTextField = "TYPE_VALUE";
        ddlType.DataValueField = "TYPE_KEY";
        ddlType.DataBind();
        ddlType.Items.Insert(0, new ListItem("-Select-", "0"));
    }
    public void Load_UserList()
    {

        DataTable dt = objBLLUser.Get_UserList_ApprovalLimit();

        DDLUserFilter.DataSource = dt;
        DDLUserFilter.DataTextField = "UserName";
        DDLUserFilter.DataValueField = "UserID";
        DDLUserFilter.DataBind();
        DDLUserFilter.Items.Insert(0, new ListItem("-ALL-", "0"));
    }

    public void BindUserApproval()
    {

        int rowcount = ucCustomPagerItems.isCountRecord;
      
        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());


        DataTable dt = objBLLAppLimit.Get_Approval_Limit_Search(txtfilter.Text != "" ? txtfilter.Text : null,UDFLib.ConvertIntegerToNull(DDLGroupFilter.SelectedValue)
            ,  UDFLib.ConvertIntegerToNull(DDLUserFilter.SelectedValue), sortbycoloumn, sortdirection
             , ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);


        if (ucCustomPagerItems.isCountRecord == 1)
        {
            ucCustomPagerItems.CountTotalRec = rowcount.ToString();
            ucCustomPagerItems.BuildPager();
        }

        gvUserType.DataSource = dt;
        gvUserType.DataBind();
        
  

    }

    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
            Response.Redirect("~/default.aspx?msgid=1");

        if (objUA.Add == 0)ImgAdd.Visible = false;
        if (objUA.Edit == 1)
            uaEditFlag = true;
        else
            btnsave.Visible = false;
        if (objUA.Delete == 1) uaDeleteFlage = true;

    }

    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }

   

    protected void ImgAdd_Click(object sender, EventArgs e)
    {
        this.SetFocus("ctl00_MainContent_DDLGroup");
        HiddenFlag.Value = "Add";

        OperationMode = "Add Approval Limit";
        clearcontrol();
        

        string AddUserTypemodal = String.Format("showModal('divadd',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddUserTypemodal", AddUserTypemodal, true);
    }
    protected void clearcontrol()
    {
        DDLGroup.Enabled = true;
        ddlType.Enabled = true;

        DDLGroup.SelectedValue = "0";
        ddlType.SelectedValue = "0";
        gvUserApprovalList.DataSource = null;
        gvUserApprovalList.DataBind();
    }
    protected void btnsave_Click(object sender, EventArgs e)
    {

        DataTable dt = new DataTable();
        dt.Columns.Add("User_ID", typeof(Int16));
        dt.Columns.Add("MAX_APPROVAL_LIMIT", typeof(decimal));
       
        int  UserId;
        int UserSelectedCount = 0;
        decimal MAX_APPROVAL_LIMIT;
        bool Selected;
        string js = "";
        string ApproverType = ddlType.SelectedValue;
        ArrayList ApproverAmount = new ArrayList();
        AmountApplicable = objBLLAppLimit.CheckAmountApplicable(ddlType.SelectedValue.ToString(), AmountApplicable);
        foreach (GridViewRow dr in gvUserApprovalList.Rows)
        {
            Selected = ((CheckBox)dr.FindControl("chkSelected")).Checked;
            if (Selected == true)
            {
                UserSelectedCount++;
                UserId = int.Parse(((Label)dr.FindControl("lblUserId")).Text.ToString());
                if (AmountApplicable == 1)
                {
                    if (((TextBox)dr.FindControl("txtAmount")).Text.ToString() != "")
                    {
                        MAX_APPROVAL_LIMIT = decimal.Parse(((TextBox)dr.FindControl("txtAmount")).Text.ToString());
                        if (ApproverType == "AdvanceApprover")
                        {
                            if (ApproverAmount.Contains(MAX_APPROVAL_LIMIT))
                            {
                                js = "alert('There can be only one approver with specific amount');";
                                break;
                            }
                            ApproverAmount.Add(MAX_APPROVAL_LIMIT);
                        }
                    }
                    else
                        MAX_APPROVAL_LIMIT = 0;
                    if (MAX_APPROVAL_LIMIT == 0)
                    {
                        js = "alert('Approval Limit is mandatory');";
                        break;
                    }
                }
                else
                {
                    MAX_APPROVAL_LIMIT = 0;
                }
                dt.Rows.Add(UserId, MAX_APPROVAL_LIMIT);
            }
        }

        if (UserSelectedCount == 0)
        {
            js = "alert('Select user for approval');";
        }
        if (js == "")
        {
            int responseid = objBLLAppLimit.INS_Approval_Limit(UDFLib.ConvertIntegerToNull(DDLGroup.SelectedValue), ddlType.SelectedValue.ToString(), dt, Convert.ToInt32(Session["USERID"]));
            BindUserApproval();
            string hidemodal = String.Format("hideModal('divadd')");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hidemodal", hidemodal, true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "status", js, true);
            string AddUserTypemodal = String.Format("showModal('divadd',false);");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddUserTypemodal", AddUserTypemodal, true);
        }
    }

    protected void onUpdate(object source, CommandEventArgs e)
    {

        HiddenFlag.Value = "Edit";
        OperationMode = "Edit Approval Limit";

        string[] arg = e.CommandArgument.ToString().Split(',');

        DDLGroup.SelectedValue = PreventUnlistedValueError(DDLGroup, UDFLib.ConvertStringToNull(arg[1]));
        ddlType.SelectedValue  = PreventUnlistedValueError(ddlType,UDFLib.ConvertStringToNull(arg[2]));

        DDLGroup.Enabled = false;
        ddlType.Enabled = false;

        AmountApplicable = objBLLAppLimit.CheckAmountApplicable(ddlType.SelectedValue.ToString(), AmountApplicable);
        BindUserApprovalList(ddlType.SelectedValue,int.Parse(DDLGroup.SelectedValue));

        string AddUserTypemodal = String.Format("showModal('divadd',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddUserTypemodal", AddUserTypemodal, true);

    }

    protected void onDelete(object source, CommandEventArgs e)
    {
        string[] DelParms = Convert.ToString(e.CommandArgument).Split(',');

        int retval = objBLLAppLimit.Del_Approval_Limit(Convert.ToInt32(DelParms[0]),Convert.ToInt32(DelParms[1]),Convert.ToInt32(Session["USERID"]));
        BindUserApproval();


    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        BindUserApproval();
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        txtfilter.Text = "";
        txtApprovarID.Text = "";
        DDLGroupFilter.SelectedValue = "0";
        DDLUserFilter.SelectedValue = "0";
        //txtApprovalLimit.Text = "";

       BindUserApproval();
    }


    protected void ImgExpExcel_Click(object sender, EventArgs e)
    {

        int rowcount = ucCustomPagerItems.isCountRecord;
        
        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());


        DataTable dt = objBLLAppLimit.Get_Approval_Limit_Search(txtfilter.Text != "" ? txtfilter.Text : null, UDFLib.ConvertIntegerToNull(DDLGroupFilter.SelectedValue)
           , UDFLib.ConvertIntegerToNull(DDLUserFilter.SelectedValue), sortbycoloumn, sortdirection
            , null, null, ref  rowcount);

        string[] HeaderCaptions = { "Group Name", "User Name" ,"Approval Limit"};
        string[] DataColumnsName = { "Group_Name", "UserName", "MAX_APPROVAL_LIMIT" };

        GridViewExportUtil.ShowExcel(dt, HeaderCaptions, DataColumnsName, "ApprovalLimit", "Approval Limit", "");

    }


    protected void gvUserType_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.Header)
        {
            if (ViewState["SORTBYCOLOUMN"] != null)
            {
                HtmlImage img = (HtmlImage)e.Row.FindControl(ViewState["SORTBYCOLOUMN"].ToString());
                if (img != null)
                {
                    if (ViewState["SORTDIRECTION"] == null || ViewState["SORTDIRECTION"].ToString() == "0")
                        img.Src = "~/purchase/Image/arrowUp.png";
                    else
                        img.Src = "~/purchase/Image/arrowDown.png";

                    img.Visible = true;
                }
            }
         }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes["onmouseover"] = "this.style.cursor='hand';this.style.backgroundColor='#FEECEC';";
            e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';this.style.color='black';this.style.backgroundColor=''";
            string MAX_APPROVAL_LIMIT = DataBinder.Eval(e.Row.DataItem, "MAX_APPROVAL_LIMIT").ToString();
            Label lblMAX_APPROVAL_LIMIT = (Label)e.Row.FindControl("lblMAX_APPROVAL_LIMIT");
            if ( MAX_APPROVAL_LIMIT == "0.0000" )
            {
                lblMAX_APPROVAL_LIMIT.Text = "";
            }
        }

    }
   
    protected void gvUserType_Sorting(object sender, GridViewSortEventArgs se)
    {
        ViewState["SORTBYCOLOUMN"] = se.SortExpression;

        if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
            ViewState["SORTDIRECTION"] = 1;
        else
            ViewState["SORTDIRECTION"] = 0;

         BindUserApproval();
    }

    protected void BindUserApprovalList(string ApprovalType,int Group_ID)
    {
        try
        {
            dtUserApprovalList = objBLLAppLimit.Get_UserListApprovalLimit(ApprovalType, Group_ID);
            if (dtUserApprovalList.Rows.Count > 0)
                gvUserApprovalList.DataSource = dtUserApprovalList;
            else
                gvUserApprovalList.DataSource = null;
            gvUserApprovalList.DataBind();

            string AddUserTypemodal = String.Format("showModal('divadd',false);");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddUserTypemodal", AddUserTypemodal, true);
        }
        catch
        {
        }
    }

    protected void ddlType_OnSelectedIndexChanged(object sender, EventArgs e)
    {        
        AmountApplicable = objBLLAppLimit.CheckAmountApplicable(ddlType.SelectedValue.ToString(), AmountApplicable);
        BindUserApprovalList(ddlType.SelectedValue.ToString(), int.Parse(DDLGroup.SelectedValue.ToString()));
    }
    protected void gvUserApprovalList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TextBox txtAmount = (TextBox)e.Row.FindControl("txtAmount");
            if (AmountApplicable == 0)
                txtAmount.Visible = false;
            else
                txtAmount.Visible = true; 
        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
            Label lbl = (Label)e.Row.FindControl("lblAmount");
            if (AmountApplicable == 0)
                lbl.Visible = false;
            else
                lbl.Visible = true;
        }
    }
    protected void DDLGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        AmountApplicable = objBLLAppLimit.CheckAmountApplicable(ddlType.SelectedValue.ToString(), AmountApplicable);
        BindUserApprovalList(ddlType.SelectedValue.ToString(), int.Parse(DDLGroup.SelectedValue.ToString()));
    }
    
    protected string PreventUnlistedValueError(DropDownList li, string val)
    {
        if (li.Items.FindByValue(val) == null)
        {

            li.SelectedValue = "0";
            val = "0";

        }
        return val;
    }
}