using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;
using SMS.Business.Infrastructure;
using SMS.Properties;
using SMS.Business.Technical;
using SMS.Business.ASL;

public partial class ASL_ASL_Supplier_Approver : System.Web.UI.Page
{
    BLL_ASL_Lib objBLL = new BLL_ASL_Lib();

    UserAccess objUA = new UserAccess();

    public string OperationMode = "";

    public Boolean uaEditFlag = false;
    public Boolean uaDeleteFlage = false;


    protected void Page_Load(object sender, EventArgs e)
    {
        this.Form.DefaultButton = this.btnFilter.UniqueID;
        UserAccessValidation();
        if (!IsPostBack)
        {
            ViewState["SORTDIRECTION"] = null;
            ViewState["SORTBYCOLOUMN"] = null;

            ucCustomPagerItems.PageSize = 20;
            BindApproverList();
            BindSupplierApprover();
        }

    }



    public void BindSupplierApprover()
    {
        try
        {
            int rowcount = ucCustomPagerItems.isCountRecord;

            string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
            int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

            DataTable dt = objBLL.SupplierApprover_Search(UDFLib.ConvertStringToNull(txtSearch.Text), UDFLib.ConvertStringToNull(ddlFilter.SelectedValue),UDFLib.ConvertStringToNull(ddlFilterApproverType.SelectedValue), sortbycoloumn, sortdirection
                 , ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);


            if (ucCustomPagerItems.isCountRecord == 1)
            {
                ucCustomPagerItems.CountTotalRec = rowcount.ToString();
                ucCustomPagerItems.BuildPager();
            }

            if (dt.Rows.Count > 0)
            {
                gvSupplierApprover.DataSource = dt;
                gvSupplierApprover.DataBind();
            }
            else
            {
                gvSupplierApprover.DataSource = dt;
                gvSupplierApprover.DataBind();
            }
        }
        catch { }
        {
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

        if (objUA.Add == 0)
        {
            ImgAdd.Visible = false;
        }
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
        this.SetFocus("ctl00_MainContent_ddlUserName");
        HiddenFlag.Value = "Add";
        OperationMode = "Add Supplier Approver";
        //BindUserList();
        ClearField();

        string SupplierScope = String.Format("showModal('divadd',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "SupplierScope", SupplierScope, true);
    }

    protected void ClearField()
    {
        ddlUserName.SelectedValue = "0";
        txtApproverID.Text = "";
        chkApprover.Checked = false;
        ddlApproverType.SelectedValue = "0";
        ddlGroupName.SelectedValue = "0";
        chkFinalApprover.Checked = false;
        ddlGroupName.Enabled = true;
        lblmsg.Text = "";
    }
    protected void BindApproverList()
    {
        try
        {

            DataSet ds = objBLL.Get_Supplier_ApproverUserList(UDFLib.ConvertToInteger(Session["UserID"].ToString()));

            ddlUserName.DataSource = ds.Tables[0];
            ddlUserName.DataValueField = "UserID";
            ddlUserName.DataTextField = "User_name";
            ddlUserName.DataBind();
            ddlUserName.Items.Insert(0, new ListItem("--SELECT--", "0"));

            ddlGroupName.DataSource = ds.Tables[1];
            ddlGroupName.DataValueField = "Group_Name";
            ddlGroupName.DataTextField = "Group_Description";
            ddlGroupName.DataBind();
            ddlGroupName.Items.Insert(0, new ListItem("--SELECT--", "0"));

            ddlFilter.DataSource = ds.Tables[1];
            ddlFilter.DataValueField = "Group_Name";
            ddlFilter.DataTextField = "Group_Description";
            ddlFilter.DataBind();
            ddlFilter.Items.Insert(0, new ListItem("--All--", "0"));

        }
        catch
        {
        }
    }

    protected void btnsave_Click(object sender, EventArgs e)
    {
        string retval = "";
        if (HiddenFlag.Value == "Add")
        {
            retval = objBLL.InsertSupplierApprover(UDFLib.ConvertIntegerToNull(ddlUserName.SelectedValue), chkApprover.Checked ? 1 : 0, chkFinalApprover.Checked ? 1 : 0, Convert.ToInt32(Session["USERID"]), ddlApproverType.SelectedValue, ddlGroupName.SelectedValue);
        }
        else
        {
            retval = objBLL.EditSupplierApprover(Convert.ToInt32(txtApproverID.Text.Trim()), UDFLib.ConvertIntegerToNull(ddlUserName.SelectedValue), chkApprover.Checked ? 1 : 0, chkFinalApprover.Checked ? 1 : 0, Convert.ToInt32(Session["USERID"]), ddlApproverType.SelectedValue, ddlGroupName.SelectedValue);
        }
        if (retval != "1")
        {
        //    string message = "alert(retval)";
        //    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            lblmsg.Text = retval;
            string AddSingoffReasonmodal = String.Format("showModal('divadd',false);");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddSingoffReasonmodal", AddSingoffReasonmodal, true);
        }
        else
        {
            //string MSG = ddlUserName.SelectedItem.Text + ' ' + "User Already Existed.";
            //string message = "alert('User Already Existed.')";
            //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
           
            BindSupplierApprover();
            string hidemodal = String.Format("hideModal('divadd')");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hidemodal", hidemodal, true);
        }
       
       

     
       
    }

    protected void onUpdate(object source, CommandEventArgs e)
    {

        HiddenFlag.Value = "Edit";
        OperationMode = "Edit Supplier Approver";
       // BindUserList();
        DataTable dt = new DataTable();
        dt = objBLL.Get_SupplierApprover_List(Convert.ToInt32(e.CommandArgument.ToString()));
        lblmsg.Text = "";
        if (dt.Rows[0]["Approver"].ToString() == "0")
        {
            chkApprover.Checked = false;
        }
        else
        {
            chkApprover.Checked = true;
        }
        if (dt.Rows[0]["Final_Approver"].ToString() == "0")
        {
            chkFinalApprover.Checked = false;
        }
        else
        {
            chkFinalApprover.Checked = true;
        }

        txtApproverID.Text = dt.Rows[0]["ID"].ToString();
        if (ddlUserName.Items.FindByValue(dt.Rows[0]["ApproveID"].ToString()) != null)
        {
            ddlUserName.SelectedValue = dt.Rows[0]["ApproveID"].ToString();
        }
        
        
        //chkFinalApprover.Checked = Convert.ToBoolean(dt.Rows[0]["Final_Approver"].ToString());
        if (dt.Rows[0]["Approver_Type"].ToString() == "Evaluation")
        {
            ddlGroupName.Enabled = false;
        }
        else
        {
            ddlGroupName.Enabled = true;
        }
        if (ddlGroupName.Items.FindByValue(dt.Rows[0]["CR_Group_Name"].ToString()) != null)
        {
            ddlGroupName.SelectedValue = dt.Rows[0]["CR_Group_Name"].ToString();
        }
        //ddlGroupName.SelectedValue = dt.Rows[0]["CR_Group_Name"].ToString();
        ddlApproverType.SelectedValue = dt.Rows[0]["Approver_Type"].ToString();

        // string InfoDiv = "Get_Record_Information_Details('ASL_LIB_SUPPLIER_APPROVER','ID=" + txtApproverID.Text + "')";
        //ScriptManager.RegisterStartupScript(this, this.GetType(), "InfoDiv", InfoDiv, true);


        string AddSingoffReasonmodal = String.Format("showModal('divadd',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddSingoffReasonmodal", AddSingoffReasonmodal, true);

    }

    protected void onDelete(object source, CommandEventArgs e)
    {

        int retval = objBLL.DeleteSupplierApprover(Convert.ToInt32(e.CommandArgument.ToString()), GetSessionUserID());
        if (retval > 0)
        {
            BindSupplierApprover();
        }
        else
        {
            string msg2 = String.Format("alert('Approver name currently useing in Supplier Evaluation so cant delete.')");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg2, true);
        }
    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        BindSupplierApprover();
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        txtSearch.Text = "";
        ddlFilter.SelectedValue = "0";
        ddlFilterApproverType.SelectedValue = "0";
        BindSupplierApprover();
    }

    protected void ImgExpExcel_Click(object sender, EventArgs e)
    {
        int rowcount = ucCustomPagerItems.isCountRecord;

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

        DataTable dt = objBLL.SupplierApprover_Search(UDFLib.ConvertStringToNull(txtSearch.Text), UDFLib.ConvertStringToNull(ddlFilter.SelectedValue), UDFLib.ConvertStringToNull(ddlFilterApproverType.SelectedValue), sortbycoloumn, sortdirection
            , null, null, ref  rowcount);



        string[] HeaderCaptions = { "Approver ID", "Approver Name", "Approver", "Final Approver" };
        string[] DataColumnsName = { "ID", "User_Name", "Approver", "Final_Approver" };

        GridViewExportUtil.ShowExcel(dt, HeaderCaptions, DataColumnsName, "Supplier Approver", "Supplier Approver", "");

    }

    protected void gvSupplierApprover_RowDataBound(object sender, GridViewRowEventArgs e)
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
            Label lblApprover = (Label)e.Row.FindControl("lblApprover");
            Label lblFinalApprover = (Label)e.Row.FindControl("lblFinalApprover");
            if (DataBinder.Eval(e.Row.DataItem, "Approver").ToString() == "Yes")
            {
                lblApprover.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                lblApprover.ForeColor = System.Drawing.Color.Red;
            }
            if (DataBinder.Eval(e.Row.DataItem, "Final_Approver").ToString() == "Yes")
            {
                lblFinalApprover.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                lblFinalApprover.ForeColor = System.Drawing.Color.Red;
            }

            e.Row.Attributes["onmouseover"] = "this.style.cursor='hand';this.style.backgroundColor='#FEECEC';";
            e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';this.style.color='black';this.style.backgroundColor=''";
        }

    }

    protected void gvSupplierApprover_Sorting(object sender, GridViewSortEventArgs se)
    {
        ViewState["SORTBYCOLOUMN"] = se.SortExpression;

        if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
            ViewState["SORTDIRECTION"] = 1;
        else
            ViewState["SORTDIRECTION"] = 0;

        BindSupplierApprover();
    }
    protected void ddlApproverType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlApproverType.SelectedValue == "Evaluation")
        {
            ddlGroupName.SelectedValue = "0";
            ddlGroupName.Enabled = false;
        }
        else
        {
            ddlGroupName.SelectedValue = "0";
            ddlGroupName.Enabled = true;
        }
        string AddSingoffReasonmodal = String.Format("showModal('divadd',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddSingoffReasonmodal", AddSingoffReasonmodal, true);
    }

    protected void btnGroup_Click(object sender, EventArgs e)
    {
        OperationMode = "ASL Column Group Realationship";
        BindColumnGroupDetails();
        string SupplierScope = String.Format("showModal('divReport',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "SupplierScope", SupplierScope, true);
    }
    protected void BindColumnGroupDetails()
    {
        
        DataSet ds = objBLL.Get_SupplierColumnGroupDetails(GetSessionUserID());
        gvGroupColumn.DataSource = ds.Tables[0];
        gvGroupColumn.DataBind();
    }
}