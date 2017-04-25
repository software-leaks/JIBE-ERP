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
using SMS.Business.ASL;
using SMS.Business.Infrastructure;
using SMS.Properties;


public partial class ASL_Libraries_System_Variable : System.Web.UI.Page
{
    BLL_SysVariable objBLLSysVariable = new BLL_SysVariable();
    BLL_Infra_Country objBLLCountry = new BLL_Infra_Country();
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

            Load_VariableType();
            BindSysVariableGrid();
        }

    }
    protected void Load_VariableType()
    {
        DataTable dt = objBLLSysVariable.Get_SysVariable("");
        if (dt.Rows.Count > 0)
        {
            ddlVariableType.DataSource = dt;
            ddlVariableType.DataTextField = "VARIABLETYPE";
            ddlVariableType.DataValueField = "VARIABLE_TYPE";
            ddlVariableType.DataBind();
            ddlVariableType.Items.Insert(0, new ListItem("-Select-", "0"));

            ddlVariableTypeFilter.DataSource = dt;
            ddlVariableTypeFilter.DataTextField = "VARIABLETYPE";
            ddlVariableTypeFilter.DataValueField = "VARIABLE_TYPE";
            ddlVariableTypeFilter.DataBind();
            ddlVariableTypeFilter.Items.Insert(0, new ListItem("-ALL-", "0"));


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

    public void BindSysVariableGrid()
    {

        int rowcount = ucCustomPagerItems.isCountRecord;

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());


        DataTable dt = objBLLSysVariable.Get_SysVariable_Search(txtNameFilter.Text != "" ? txtNameFilter.Text : null, UDFLib.ConvertStringToNull(ddlVariableTypeFilter.SelectedValue), sortbycoloumn, sortdirection
            , ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);


        if (ucCustomPagerItems.isCountRecord == 1)
        {
            ucCustomPagerItems.CountTotalRec = rowcount.ToString();
            ucCustomPagerItems.BuildPager();
        }

        if (dt.Rows.Count > 0)
        {
            gvVariable.DataSource = dt;
            gvVariable.DataBind();
        }
        else
        {
            gvVariable.DataSource = dt;
            gvVariable.DataBind();
        }

    }

    protected void btnsave_Click(object sender, EventArgs e)
    {
        int retval;
        if (HiddenFlag.Value == "Add")
        {
             retval = objBLLSysVariable.Insert_SysVariable(UDFLib.ConvertStringToNull(ddlVariableType.SelectedValue), txtName.Text.Trim()
                , txtCode.Text.Trim(), txtValue.Text.Trim(), txtColorCode.Text.Trim(), Convert.ToInt32(Session["USERID"]));
        }
        else
        {
            retval = objBLLSysVariable.Edit_SysVariable(UDFLib.ConvertIntegerToNull(txtVariableID.Text), UDFLib.ConvertStringToNull(ddlVariableType.SelectedValue), txtName.Text.Trim()
                , txtCode.Text.Trim(), txtValue.Text.Trim(), txtColorCode.Text.Trim(), Convert.ToInt32(Session["USERID"]));
        }
        if (retval >= 1)
        {
            BindSysVariableGrid();
            string hidemodal = String.Format("hideModal('divadd')");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hidemodal", hidemodal, true);
        }
        else
        {
          lblMsg.Text = "Variable Code already exists.";
        }
       

     
    }

    protected void ImgAdd_Click(object sender, EventArgs e)
    {
        this.SetFocus("ctl00_MainContent_txtName");
        HiddenFlag.Value = "Add";
        txtCode.Enabled = true;
        lblCode.Visible = true;
        OperationMode = "Add System Variable";

        ClearFields();

        string AddPort = String.Format("showModal('divadd',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddPort", AddPort, true);


    }

    protected void ImgExpExcel_Click(object sender, EventArgs e)
    {

        int rowcount = ucCustomPagerItems.isCountRecord;

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

        DataTable dt = objBLLSysVariable.Get_SysVariable_Search(txtNameFilter.Text != "" ? txtNameFilter.Text : null, UDFLib.ConvertStringToNull(ddlVariableTypeFilter.SelectedValue), sortbycoloumn, sortdirection
          , ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);

        string[] HeaderCaptions = { "Variable Type", "Variable Name", "Variable Code", "Variable Value", "Color Code" };
        string[] DataColumnsName = { "Variable_Type", "Variable_Name", "Variable_Code", "VARIABLE_VALUE", "COLOR_CODE" };

        GridViewExportUtil.ShowExcel(dt, HeaderCaptions, DataColumnsName, "System Variable", "System Variable", "");
    }

    public void ClearFields()
    {
        ddlVariableType.SelectedIndex = 0;
        txtName.Text = "";
        txtCode.Text = "";
        lblMsg.Visible = false;
    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {

        BindSysVariableGrid();

    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        ddlVariableTypeFilter.SelectedIndex = 0;
        txtNameFilter.Text = "";

        BindSysVariableGrid();
    }

    protected void onDelete(object source, CommandEventArgs e)
    {
        int retval = objBLLSysVariable.Delete_SysVariable(Convert.ToInt32(e.CommandArgument.ToString()), Convert.ToInt32(Session["USERID"].ToString()));
        BindSysVariableGrid();
    }

    protected void onUpdate(object source, CommandEventArgs e)
    {
        HiddenFlag.Value = "Edit";
        OperationMode = "Edit System Variable";
        txtCode.Enabled = false;
        lblCode.Visible = false;
        lblMsg.Visible = false;
        DataTable dt = new DataTable();
        dt = objBLLSysVariable.Get_SysVariableList((Convert.ToInt32(e.CommandArgument.ToString())));

        txtVariableID.Text = dt.Rows[0]["ID"].ToString();
        txtName.Text = dt.Rows[0]["VARIABLE_NAME"].ToString();

        ddlVariableType.SelectedValue = dt.Rows[0]["VARIABLE_TYPE"].ToString();
        txtCode.Text = dt.Rows[0]["VARIABLE_CODE"].ToString();


        txtValue.Text = dt.Rows[0]["VARIABLE_VALUE"].ToString();
        txtColorCode.Text = dt.Rows[0]["COLOR_CODE"].ToString();
        //ClearFields();
        //string InfoDiv = "Get_Record_Information_Details('SYS_VARIABLES','ID=" + txtVariableID.Text + "')";
        //ScriptManager.RegisterStartupScript(this, this.GetType(), "InfoDiv", InfoDiv, true);



        string AddAirPort = String.Format("showModal('divadd',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddAirPort", AddAirPort, true);

    }

    protected void gvVariable_RowDataBound(object sender, GridViewRowEventArgs e)
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

    protected void gvVariable_Sorting(object sender, GridViewSortEventArgs se)
    {
        ViewState["SORTBYCOLOUMN"] = se.SortExpression;

        if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
            ViewState["SORTDIRECTION"] = 1;
        else
            ViewState["SORTDIRECTION"] = 0;

        BindSysVariableGrid();
    }
}