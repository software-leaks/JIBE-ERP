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

using SMS.Business.Infrastructure;
using SMS.Properties;


public partial class ShipDepartment : System.Web.UI.Page
{

    BLL_Infra_ShipDepartment objBLL = new BLL_Infra_ShipDepartment();

    BLL_Infra_Country objBLLCountry = new BLL_Infra_Country();
    BLL_Infra_Company objBLLComp = new BLL_Infra_Company();
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
            FillVesselManagerDLL();

            BindShipDepartment();
        }

    }

    public void FillVesselManagerDLL()
    {

        /* Company Type  5 = VESSEL MANAGER  */

        DataTable dtComptype = objBLLComp.Get_CompanyListByType(5, UDFLib.ConvertToInteger(Session["USERCOMPANYID"].ToString()));

        ddl_Vessel_Manager_Add.DataSource = dtComptype;
        ddl_Vessel_Manager_Add.DataTextField = "COMPANY_NAME";
        ddl_Vessel_Manager_Add.DataValueField = "ID";
        ddl_Vessel_Manager_Add.DataBind();
        ddl_Vessel_Manager_Add.Items.Insert(0, new ListItem("-Select-", "0"));

        ddl_Vessel_Manager_Filter.DataSource = dtComptype;
        ddl_Vessel_Manager_Filter.DataTextField = "COMPANY_NAME";
        ddl_Vessel_Manager_Filter.DataValueField = "ID";
        ddl_Vessel_Manager_Filter.DataBind();
        ddl_Vessel_Manager_Filter.Items.Insert(0, new ListItem("-ALL-", "0"));

    }

    public void BindShipDepartment()
    {

        int rowcount = ucCustomPagerItems.isCountRecord;

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

        DataTable dt = objBLL.SearchShipDepartment(txtfilter.Text != "" ? txtfilter.Text : null, UDFLib.ConvertIntegerToNull(ddl_Vessel_Manager_Filter.SelectedValue), sortbycoloumn, sortdirection
             , ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);


        if (ucCustomPagerItems.isCountRecord == 1)
        {
            ucCustomPagerItems.CountTotalRec = rowcount.ToString();
            ucCustomPagerItems.BuildPager();
        }

        if (dt.Rows.Count > 0)
        {
            gvOfficeDept.DataSource = dt;
            gvOfficeDept.DataBind();
        }
        else
        {
            gvOfficeDept.DataSource = dt;
            gvOfficeDept.DataBind();
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
        this.SetFocus("ctl00_MainContent_txtFlagName");
        HiddenFlag.Value = "Add";
        OperationMode = "Add Department";

        ClearField();

        string OfficeDept = String.Format("showModal('divadd',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "OfficeDept", OfficeDept, true);
    }

    protected void ClearField()
    {

        txtDeptID.Text = "";
        txtDepartment.Text = "";
        ddl_Vessel_Manager_Add.SelectedValue = "0";
    }

    protected void btnsave_Click(object sender, EventArgs e)
    {

        if (HiddenFlag.Value == "Add")
        {
            int retval = objBLL.InsertShipDepartment(txtDepartment.Text, UDFLib.ConvertIntegerToNull(ddl_Vessel_Manager_Add.SelectedValue), Convert.ToInt32(Session["USERID"]));
        }
        else
        {
            int retval = objBLL.EditShipDepartment(Convert.ToInt32(txtDeptID.Text.Trim()), txtDepartment.Text, UDFLib.ConvertIntegerToNull(ddl_Vessel_Manager_Add.SelectedValue), Convert.ToInt32(Session["USERID"]));
        }

        BindShipDepartment();

        string hidemodal = String.Format("hideModal('divadd')");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hidemodal", hidemodal, true);

    }

    protected void onUpdate(object source, CommandEventArgs e)
    {

        HiddenFlag.Value = "Edit";
        OperationMode = "Edit Department";

        DataTable dt = new DataTable();
        dt = objBLL.Get_ShipDepartment_List(Convert.ToInt32(e.CommandArgument.ToString()));


        txtDeptID.Text = dt.Rows[0]["ID"].ToString();
        txtDepartment.Text = dt.Rows[0]["VALUE"].ToString();
        ddl_Vessel_Manager_Add.SelectedValue = dt.Rows[0]["CompanyID"].ToString();



        string InfoDiv = "Get_Record_Information_Details('INF_LIB_ONSHIP_DEPT','ID=" + txtDeptID.Text + "')";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "InfoDiv", InfoDiv, true);

        string AddOfficeDeptmodal = String.Format("showModal('divadd',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddOfficeDeptmodal", AddOfficeDeptmodal, true);

    }

    protected void onDelete(object source, CommandEventArgs e)
    {

        int retval = objBLL.DeleteShipDepartment(Convert.ToInt32(e.CommandArgument.ToString()), Convert.ToInt32(Session["USERID"]));

        BindShipDepartment();

    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        BindShipDepartment();
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        txtfilter.Text = "";
        ddl_Vessel_Manager_Filter.SelectedValue = "0";

        BindShipDepartment();
    }

    protected void ImgExpExcel_Click(object sender, EventArgs e)
    {
        int rowcount = ucCustomPagerItems.isCountRecord;

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

        DataTable dt = objBLL.SearchShipDepartment(txtfilter.Text != "" ? txtfilter.Text : null, UDFLib.ConvertIntegerToNull(ddl_Vessel_Manager_Filter.SelectedValue)
            , sortbycoloumn, sortdirection, null, null, ref  rowcount);

        string[] HeaderCaptions = { "Department", "Vessel Manager" };
        string[] DataColumnsName = { "VALUE", "VesselManager" };

        GridViewExportUtil.ShowExcel(dt, HeaderCaptions, DataColumnsName, "ShipDepartment", "Ship Department", "");

    }

    protected void gvOfficeDept_RowDataBound(object sender, GridViewRowEventArgs e)
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

    protected void gvOfficeDept_Sorting(object sender, GridViewSortEventArgs se)
    {
        ViewState["SORTBYCOLOUMN"] = se.SortExpression;

        if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
            ViewState["SORTDIRECTION"] = 1;
        else
            ViewState["SORTDIRECTION"] = 0;

        BindShipDepartment();
    }
}