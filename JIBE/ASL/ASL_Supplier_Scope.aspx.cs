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


public partial class ASL_ASL_Supplier_Scope : System.Web.UI.Page
{
    BLL_ASL_Lib objBLL = new BLL_ASL_Lib();

    UserAccess objUA = new UserAccess();

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

            BindSupplierScope();
        }

    }



    public void BindSupplierScope()
    {

        int rowcount = ucCustomPagerItems.isCountRecord;

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

        DataTable dt = objBLL.SupplierScope_Search(txtfilter.Text != "" ? txtfilter.Text : null, sortbycoloumn, sortdirection
             , ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);


        if (ucCustomPagerItems.isCountRecord == 1)
        {
            ucCustomPagerItems.CountTotalRec = rowcount.ToString();
            ucCustomPagerItems.BuildPager();
        }

        if (dt.Rows.Count > 0)
        {
            gvSupplierScope.DataSource = dt;
            gvSupplierScope.DataBind();
        }
        else
        {
            gvSupplierScope.DataSource = dt;
            gvSupplierScope.DataBind();
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
        this.SetFocus("ctl00_MainContent_txtSupplierScope");
        HiddenFlag.Value = "Add";
        OperationMode = "Add Supplier Scope";

        ClearField();

        string SupplierScope = String.Format("showModal('divadd',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "SupplierScope", SupplierScope, true);
    }

    protected void ClearField()
    {
        txtSupplierScope.Text = "";
        txtScopeID.Text = "";
        lblMsg.Text = "";
    }

    protected void btnsave_Click(object sender, EventArgs e)
    {
        int retval = 0;
        if (HiddenFlag.Value == "Add")
        {
             retval = objBLL.InsertSupplierScope(txtSupplierScope.Text.Trim(), Convert.ToInt32(Session["USERID"]));
        }
        else
        {
             retval = objBLL.EditSupplierScope(Convert.ToInt32(txtScopeID.Text.Trim()), txtSupplierScope.Text.Trim(), Convert.ToInt32(Session["USERID"]));
        }
        if (retval == 0)
        {
            lblMsg.Text = "Supplier Scope already exists!.";
            string SupplierScope = String.Format("showModal('divadd',false);");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "SupplierScope", SupplierScope, true);
        }
        else
        {
            BindSupplierScope();

            string hidemodal = String.Format("hideModal('divadd')");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hidemodal", hidemodal, true);
        }

    }

    protected void onUpdate(object source, CommandEventArgs e)
    {

        HiddenFlag.Value = "Edit";
        OperationMode = "Edit Supplier Scope";

        DataTable dt = new DataTable();
        dt = objBLL.Get_SupplierScope_List(Convert.ToInt32(e.CommandArgument.ToString()));

        lblMsg.Text = "";
        txtScopeID.Text = dt.Rows[0]["ID"].ToString();
        txtSupplierScope.Text = dt.Rows[0]["SCOPE_NAME"].ToString();




        string InfoDiv = "Get_Record_Information_Details('ASL_LIB_SUPPLIER_SCOPE','ID=" + txtScopeID.Text + "')";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "InfoDiv", InfoDiv, true);


        string AddSingoffReasonmodal = String.Format("showModal('divadd',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddSingoffReasonmodal", AddSingoffReasonmodal, true);

    }

    protected void onDelete(object source, CommandEventArgs e)
    {

        int retval = objBLL.DeleteSupplierScope(Convert.ToInt32(e.CommandArgument.ToString()), Convert.ToInt32(Session["USERID"]));

        BindSupplierScope();

    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        BindSupplierScope();
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        txtfilter.Text = "";
        BindSupplierScope();
    }

    protected void ImgExpExcel_Click(object sender, EventArgs e)
    {
        int rowcount = ucCustomPagerItems.isCountRecord;

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

        DataTable dt = objBLL.SupplierScope_Search(txtfilter.Text != "" ? txtfilter.Text : null, sortbycoloumn, sortdirection
            , null, null, ref  rowcount);



        string[] HeaderCaptions = { "Supplier ScopeID", "Supplier ScopeName" };
        string[] DataColumnsName = { "ID", "SCOPE_NAME", };

        GridViewExportUtil.ShowExcel(dt, HeaderCaptions, DataColumnsName, "Supplier Scope", "Supplier Scope", "");

    }

    protected void gvSupplierScope_RowDataBound(object sender, GridViewRowEventArgs e)
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
        }

    }

    protected void gvSupplierScope_Sorting(object sender, GridViewSortEventArgs se)
    {
        ViewState["SORTBYCOLOUMN"] = se.SortExpression;

        if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
            ViewState["SORTDIRECTION"] = 1;
        else
            ViewState["SORTDIRECTION"] = 0;

        BindSupplierScope();
    }
}