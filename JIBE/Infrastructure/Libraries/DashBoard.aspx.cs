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


public partial class Infrastructure_Libraries_DashBoard : System.Web.UI.Page
{
    BLL_Infra_DashBoard objBLL = new BLL_Infra_DashBoard();
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
            FillDDLOfficeDept();

            BindDashBoardSnippet();
        }

    }

    public void FillDDLOfficeDept()
    {
        try
        {

            int companyid = Convert.ToInt32(Session["USERCOMPANYID"].ToString());
            DataTable dtOfficeDept = objBLLComp.Get_CompanyDepartmentList(companyid);

            ddl_Department_Filter.Items.Clear();
            ddl_Department_Filter.DataSource = dtOfficeDept;
            ddl_Department_Filter.DataTextField = "VALUE";
            ddl_Department_Filter.DataValueField = "ID";
            ddl_Department_Filter.DataBind();


            ddlDepartment_Add.Items.Clear();
            ddlDepartment_Add.DataSource = dtOfficeDept;
            ddlDepartment_Add.DataTextField = "VALUE";
            ddlDepartment_Add.DataValueField = "ID";
            ddlDepartment_Add.DataBind();

            ListItem li = new ListItem("--SELECT ALL--", "0");
            ListItem liSelect = new ListItem("SELECT", "0");
            ddl_Department_Filter.Items.Insert(0, li);

            ddlDepartment_Add.Items.Insert(0, liSelect);

        }
        catch (Exception ex)
        {

        }
    }


    public void BindDashBoardSnippet()
    {

        int rowcount = ucCustomPagerItems.isCountRecord;

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());


        int? AutoRefreshFlage = null;
        if (chkAutoRefresh_Filter.Checked == true)
            AutoRefreshFlage = 1;

        DataTable dt = objBLL.Search_DashBoardSnippet(txtfilter.Text != "" ? txtfilter.Text : null, UDFLib.ConvertIntegerToNull(ddl_Department_Filter.SelectedValue), AutoRefreshFlage, sortbycoloumn, sortdirection
             , ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);


        if (ucCustomPagerItems.isCountRecord == 1)
        {
            ucCustomPagerItems.CountTotalRec = rowcount.ToString();
            ucCustomPagerItems.BuildPager();
        }

        if (dt.Rows.Count > 0)
        {
            gvSnippet.DataSource = dt;
            gvSnippet.DataBind();
        }
        else
        {
            gvSnippet.DataSource = dt;
            gvSnippet.DataBind();
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

        if (objUA.Add == 0) ImgAdd.Visible = false;
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
        this.SetFocus("ctl00_MainContent_txtSnippetID");
        HiddenFlag.Value = "Add";
        OperationMode = "Add Snippet";

        ClearField();

        string AddSnippet = String.Format("showModal('divadd',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Snippet", AddSnippet, true);

    }

    protected void ClearField()
    {

        txtID.Text = "";
        txtSnippetID.Text = "";
        txtSnippetName.Text = "";
        txtSnippetFunctionName.Text = "";
        chkAutoRefresh_Add.Checked = false;
        ddlDepartment_Add.SelectedValue = "0";

    }

    protected void btnsave_Click(object sender, EventArgs e)
    {

        int? AutoRefreshFlage = null;
        if (chkAutoRefresh_Add.Checked)
            AutoRefreshFlage = 1;

        if (HiddenFlag.Value == "Add")
        {
            int retval = BLL_Infra_DashBoard.Insert_DashBoardSnippet(txtSnippetID.Text, txtSnippetName.Text, txtSnippetFunctionName.Text
                , UDFLib.ConvertIntegerToNull(ddlDepartment_Add.SelectedValue), ddlDepartmentColor.SelectedValue, AutoRefreshFlage, Convert.ToInt32(Session["USERID"]));
        }
        else
        {
            int retval = BLL_Infra_DashBoard.Edit_DashBoardSnippet(Convert.ToInt32(txtID.Text.Trim()), txtSnippetID.Text, txtSnippetName.Text, txtSnippetFunctionName.Text
                , UDFLib.ConvertIntegerToNull(ddlDepartment_Add.SelectedValue), ddlDepartmentColor.SelectedValue, AutoRefreshFlage, Convert.ToInt32(Session["USERID"]));
        }

        BindDashBoardSnippet();

        string hidemodal = String.Format("hideModal('divadd')");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hidemodal", hidemodal, true);

    }

    protected void onUpdate(object source, CommandEventArgs e)
    {

        HiddenFlag.Value = "Edit";
        OperationMode = "Edit Snippet";

        DataTable dt = new DataTable();
        dt = BLL_Infra_DashBoard.List_DashBoardSnippet(Convert.ToInt32(e.CommandArgument.ToString()));

        txtID.Text = dt.Rows[0]["ID"].ToString();
        txtSnippetID.Text = dt.Rows[0]["Snippet_ID"].ToString();
        txtSnippetName.Text = dt.Rows[0]["Snippet_Name"].ToString();
        txtSnippetFunctionName.Text = dt.Rows[0]["Snippet_Function_Name"].ToString();

        if (ddlDepartment_Add.Items.FindByValue(dt.Rows[0]["Department_ID"].ToString()) != null)
            ddlDepartment_Add.SelectedValue = dt.Rows[0]["Department_ID"].ToString() != "" ? dt.Rows[0]["Department_ID"].ToString() : "0";
        else
            ddlDepartment_Add.SelectedValue = "0";

        //ddlDepartment_Add.SelectedValue = dt.Rows[0]["Department_ID"].ToString() != "" ? dt.Rows[0]["Department_ID"].ToString() : "0";
        ddlDepartmentColor.SelectedValue = dt.Rows[0]["Department_Color"].ToString() != "" ? dt.Rows[0]["Department_Color"].ToString() : "0";
        chkAutoRefresh_Add.Checked = dt.Rows[0]["Auto_Refresh"].ToString() == "1" ? true : false;


        string InfoDiv = "Get_Record_Information_Details('INF_LIB_Dash_Board_Snippet','ID=" + txtID.Text + "')";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "InfoDiv", InfoDiv, true);


        string AddOfficeDeptmodal = String.Format("showModal('divadd',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddOfficeDeptmodal", AddOfficeDeptmodal, true);

    }

    protected void onDelete(object source, CommandEventArgs e)
    {

        int retval = BLL_Infra_DashBoard.Delete_DashBoardSnippet(Convert.ToInt32(e.CommandArgument.ToString()), Convert.ToInt32(Session["USERID"]));

        BindDashBoardSnippet();

    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        BindDashBoardSnippet();
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        txtfilter.Text = "";
        chkAutoRefresh_Filter.Checked = false;
        ddl_Department_Filter.SelectedValue = "0";

        BindDashBoardSnippet();
    }

    protected void ImgExpExcel_Click(object sender, EventArgs e)
    {

        try
        {


            int rowcount = ucCustomPagerItems.isCountRecord;

            string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
            int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

            int? AutoRefreshFlage = null;
            if (chkAutoRefresh_Add.Checked)
                AutoRefreshFlage = 1;

            DataTable dt = objBLL.Search_DashBoardSnippet(txtfilter.Text != "" ? txtfilter.Text : null, UDFLib.ConvertIntegerToNull(ddl_Department_Filter.SelectedValue), AutoRefreshFlage, sortbycoloumn, sortdirection
                 , null, null, ref  rowcount);

            string[] HeaderCaptions = { "Snippet ID", "Name", "Function Name", "Department", "Department color", "Auto Refresh" };
            string[] DataColumnsName = { "Snippet_ID", "Snippet_Name", "Snippet_Function_Name", "Department", "Department_Color", "Auto_Refresh" };

            string FileName = "DashBoardSnippet" + "_" + DateTime.Now.ToString("yyyy-MM-dd HH.mm.ss"); //+ ".xls";
            string FilePath = Server.MapPath(@"~/Uploads\\Temp\\"); string ExportTempFilePath = Server.MapPath(@"~/Uploads\\ExportTemplete\\");
           

            /* commented by pranali_01042016_jit_8219_asmmt giving error while exporting replaced with below method.
              GridViewExportUtil.Export_To_Excel_Interop(dt, HeaderCaptions, DataColumnsName, "DashBoardSnippet", FilePath + FileName, ExportTempFilePath, @"~\\Uploads\\Temp\\" + FileName);
             */

            GridViewExportUtil.ShowExcel(dt, HeaderCaptions, DataColumnsName, FileName,FileName, "");

            // GridViewExportUtil.ShowExcel(dt, HeaderCaptions, DataColumnsName, "DashBoardSnippet", "Dash Board Snippet", "");

        }
        catch (Exception ex)
        {
           // lblErrorMsg.Text = Convert.ToString(ex.InnerException.Message.ToString());
        }
    }

    protected void gvSnippet_RowDataBound(object sender, GridViewRowEventArgs e)
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

    protected void gvSnippet_Sorting(object sender, GridViewSortEventArgs se)
    {
        ViewState["SORTBYCOLOUMN"] = se.SortExpression;

        if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
            ViewState["SORTDIRECTION"] = 1;
        else
            ViewState["SORTDIRECTION"] = 0;

        BindDashBoardSnippet();
    }
}