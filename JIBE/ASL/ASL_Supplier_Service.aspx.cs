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

public partial class ASL_ASL_Supplier_Service : System.Web.UI.Page
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

            BindSupplierService();
        }

    }



    public void BindSupplierService()
    {

        int rowcount = ucCustomPagerItems.isCountRecord;

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

        DataTable dt = objBLL.SupplierService_Search(txtfilter.Text != "" ? txtfilter.Text : null, sortbycoloumn, sortdirection
             , ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);


        if (ucCustomPagerItems.isCountRecord == 1)
        {
            ucCustomPagerItems.CountTotalRec = rowcount.ToString();
            ucCustomPagerItems.BuildPager();
        }

        if (dt.Rows.Count > 0)
        {
            gvSupplierService.DataSource = dt;
            gvSupplierService.DataBind();
        }
        else
        {
            gvSupplierService.DataSource = dt;
            gvSupplierService.DataBind();
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
    /// <summary>
    /// Change name Supplier service to supplier group
    /// Modified By - Alok 
    /// Date - 05/07/2016
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ImgAdd_Click(object sender, EventArgs e)
    {
        try
        {
            this.SetFocus("ctl00_MainContent_txtSupplierService");
            HiddenFlag.Value = "Add";
            OperationMode = "Add Supplier Group";

            ClearField();

            string SupplierService = String.Format("showModal('divadd',false);");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "SupplierService", SupplierService, true);
        }
        catch { }
        {
        }
    }

    protected void ClearField()
    {
        txtSupplierServiceID.Text = "";
        txtSupplierService.Text = "";
        lblMsg.Text = "";
      
    }

    protected void btnsave_Click(object sender, EventArgs e)
    {
        int retval = 0;
        if (HiddenFlag.Value == "Add")
        {
             retval = objBLL.InsertSupplierService(txtSupplierService.Text.Trim(), Convert.ToInt32(Session["USERID"]));
        }
        else
        {
            retval = objBLL.EditSupplierService(Convert.ToInt32(txtSupplierServiceID.Text.Trim()), txtSupplierService.Text.Trim(), Convert.ToInt32(Session["USERID"]));
        }
        if (retval == 0)
        {
            lblMsg.Text = "Supplier Service already exists!.";
            string SupplierService = String.Format("showModal('divadd',false);");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "SupplierService", SupplierService, true);
        }
        else
        {
            BindSupplierService();

            string hidemodal = String.Format("hideModal('divadd')");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hidemodal", hidemodal, true);
        }

    }

    protected void onUpdate(object source, CommandEventArgs e)
    {

        HiddenFlag.Value = "Edit";
        OperationMode = "Edit Supplier Service";

        DataTable dt = new DataTable();
        dt = objBLL.Get_SupplierService_List(Convert.ToInt32(e.CommandArgument.ToString()));
        lblMsg.Text = "";

        txtSupplierServiceID.Text = dt.Rows[0]["ID"].ToString();
        txtSupplierService.Text = dt.Rows[0]["SERVICE_NAME"].ToString();




        string InfoDiv = "Get_Record_Information_Details('ASL_LIB_SUPPLIER_SERVICE','ID=" + txtSupplierServiceID.Text + "')";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "InfoDiv", InfoDiv, true);


        string AddSingoffReasonmodal = String.Format("showModal('divadd',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddSingoffReasonmodal", AddSingoffReasonmodal, true);

    }

    protected void onDelete(object source, CommandEventArgs e)
    {

        int retval = objBLL.DeleteSupplierService(Convert.ToInt32(e.CommandArgument.ToString()), Convert.ToInt32(Session["USERID"]));

        BindSupplierService();

    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        BindSupplierService();
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        txtfilter.Text = "";
        BindSupplierService();
    }

    protected void ImgExpExcel_Click(object sender, EventArgs e)
    {
        int rowcount = ucCustomPagerItems.isCountRecord;

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

        DataTable dt = objBLL.SupplierService_Search(txtfilter.Text != "" ? txtfilter.Text : null, sortbycoloumn, sortdirection
            , null, null, ref  rowcount);



        string[] HeaderCaptions = { "Supplier GroupID", "Supplier GroupName" };
        string[] DataColumnsName = { "ID", "SERVICE_NAME", };

        GridViewExportUtil.ShowExcel(dt, HeaderCaptions, DataColumnsName, "Supplier Group", "Supplier Group", "");

    }

    protected void gvSupplierService_RowDataBound(object sender, GridViewRowEventArgs e)
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

    protected void gvSupplierService_Sorting(object sender, GridViewSortEventArgs se)
    {
        ViewState["SORTBYCOLOUMN"] = se.SortExpression;

        if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
            ViewState["SORTDIRECTION"] = 1;
        else
            ViewState["SORTDIRECTION"] = 0;

        BindSupplierService();
    }
}