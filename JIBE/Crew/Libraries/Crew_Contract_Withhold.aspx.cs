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
using SMS.Business.Crew;


public partial class Crew_Libraries_Crew_Contract_Withhold : System.Web.UI.Page
{
    BLL_Infra_Currency objBLLCurrency = new BLL_Infra_Currency();
    BLL_Crew_Contract objBLL = new BLL_Crew_Contract();

    UserAccess objUA = new UserAccess();
    public string TodayDateFormat = "";
    public string OperationMode = "";

    public Boolean uaEditFlag = false;
    public Boolean uaDeleteFlage = false;
    string DateFormat = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            txtEffectiveDate_CalendarExtender.Format = Convert.ToString(Session["User_DateFormat"]);
            TodayDateFormat = UDFLib.DateFormatMessage();
            DateFormat = Convert.ToString(Session["User_DateFormat"]);
            UserAccessValidation();
            if (!IsPostBack)
            {
                ViewState["SORTDIRECTION"] = null;
                ViewState["SORTBYCOLOUMN"] = null;

                ucCustomPagerItems.PageSize = 20;
                BindEntryType();
                BindCrewWithhold();
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }

    }

    protected void BindEntryType()
    {

        DataTable dt = objBLL.Get_Crew_Contract_Withhold_Entry_Type();

        ddlEntryType.DataSource = dt;
        ddlEntryType.DataTextField = "Name";
        ddlEntryType.DataValueField = "Code";
        ddlEntryType.DataBind();

        ddlEntryTypeFilter.DataSource = dt;
        ddlEntryTypeFilter.DataTextField = "Name";
        ddlEntryTypeFilter.DataValueField = "Code";
        ddlEntryTypeFilter.DataBind();

    }

    public void BindCrewWithhold()
    {

        int rowcount = ucCustomPagerItems.isCountRecord;

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

        DataTable dt = objBLL.Crew_Contract_Withhold_Search(txtfilter.Text != "" ? txtfilter.Text : null, "Percent"
             , UDFLib.ConvertIntegerToNull(ddlEntryTypeFilter.SelectedValue), sortbycoloumn, sortdirection
             , ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);


        if (ucCustomPagerItems.isCountRecord == 1)
        {
            ucCustomPagerItems.CountTotalRec = rowcount.ToString();
            ucCustomPagerItems.BuildPager();
        }

        if (dt.Rows.Count > 0)
        {

            gvCrewContractWithhold.DataSource = dt;
            gvCrewContractWithhold.DataBind();
        }
        else
        {
            gvCrewContractWithhold.DataSource = dt;
            gvCrewContractWithhold.DataBind();
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
        this.SetFocus("ctl00_MainContent_ddlEntryType");
        HiddenFlag.Value = "Add";
        OperationMode = "Add Crew Withhold";

        ClearField();

        string JoiningType = String.Format("showModal('divadd',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "JoiningType", JoiningType, true);
    }

    protected void ClearField()
    {
        txtContractNumber.Text = "";
        txtEffectiveDate.Text = "";
        txtAmount.Text = "";

    }

    protected void btnsave_Click(object sender, EventArgs e)
    {
        DateTime Dt_EffectiveDate = DateTime.Parse("1900/01/01");
        if (!UDFLib.DateCheck(txtEffectiveDate.Text))
        {
            string js = "alert('Enter valid  Date" + TodayDateFormat + "');" + String.Format("showModal('divadd',false);");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "DateFormat", js, true);
            return;
        }
        try
        {

            txtEffectiveDate.Text =  txtEffectiveDate.Text != "" ? UDFLib.ConvertToDefaultDt(txtEffectiveDate.Text) : txtEffectiveDate.Text;
            if (HiddenFlag.Value == "Add")
            {
                int retval = objBLL.Insert_Crew_Contract_Withhold(UDFLib.ConvertToInteger(txtContractNumber.Text), UDFLib.ConvertDecimalToNull(txtAmount.Text), txtWithholdType.Text
                    , UDFLib.ConvertToInteger(ddlEntryType.SelectedValue), UDFLib.ConvertDateToNull(txtEffectiveDate.Text), Convert.ToInt32(Session["USERID"]));
            }
            else
            {
                int retval = objBLL.Edit_Crew_Contract_Withhold(UDFLib.ConvertIntegerToNull(txtContractWithholdID.Text.Trim()), UDFLib.ConvertToInteger(txtContractNumber.Text)
                   , UDFLib.ConvertDecimalToNull(txtAmount.Text), txtWithholdType.Text
                   , UDFLib.ConvertToInteger(ddlEntryType.SelectedValue), UDFLib.ConvertDateToNull(txtEffectiveDate.Text), Convert.ToInt32(Session["USERID"]));
            }

            BindCrewWithhold();

            string hidemodal = String.Format("hideModal('divadd')");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hidemodal", hidemodal, true);
        }
        catch (Exception)
        {
            return;
        }

    }

    protected void onUpdate(object source, CommandEventArgs e)
    {

        HiddenFlag.Value = "Edit";
        OperationMode = "Edit Crew Withhold";

        DataTable dt = new DataTable();
        dt = objBLL.Get_Crew_Contract_Withhold_List(Convert.ToInt32(e.CommandArgument.ToString()));

        txtContractWithholdID.Text = dt.Rows[0]["ID"].ToString();
        ddlEntryType.SelectedValue = dt.DefaultView[0]["Entry_Type"].ToString() != "" ? dt.DefaultView[0]["Entry_Type"].ToString() : "0";
        txtWithholdType.Text = dt.DefaultView[0]["Withhold_Type"].ToString();
        txtEffectiveDate.Text =UDFLib.ConvertUserDateFormat(Convert.ToString( dt.Rows[0]["Effective_Date"]));
        txtAmount.Text = dt.Rows[0]["Withhold_Amount"].ToString();
        txtContractNumber.Text = dt.Rows[0]["Contract_Number"].ToString();

        string AddSingoffReasonmodalz = String.Format("showModal('divadd',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddSingoffReasonmodalz", AddSingoffReasonmodalz, true);

    }

    protected void onDelete(object source, CommandEventArgs e)
    {

        int retval = objBLL.Delete_Crew_Contract_Withhold(Convert.ToInt32(e.CommandArgument.ToString()), Convert.ToInt32(Session["USERID"]));

        BindCrewWithhold();

    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        BindCrewWithhold();
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        txtfilter.Text = "";
        BindCrewWithhold();
    }

    protected void ImgExpExcel_Click(object sender, EventArgs e)
    {
        int rowcount = ucCustomPagerItems.isCountRecord;

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());


        DataTable dt = objBLL.Crew_Contract_Withhold_Search(txtfilter.Text != "" ? txtfilter.Text : null, "Percent"
            , UDFLib.ConvertIntegerToNull(ddlEntryTypeFilter.SelectedValue), sortbycoloumn, sortdirection
            , null, null, ref  rowcount);
        UDFLib.ChangeColumnDataType(dt, "Effective_Date", typeof(string));
        foreach (DataRow row in dt.Rows)
        {
           if (!string.IsNullOrEmpty(row["Effective_Date"].ToString()))
               row["Effective_Date"] = "&nbsp;" + UDFLib.ConvertUserDateFormat(Convert.ToString(row["Effective_Date"]), UDFLib.GetDateFormat());
        }

        string[] HeaderCaptions = { "Contract No", "Withhold Amount", "Withhold Type", "Entry Type", "Effective Date" };
        string[] DataColumnsName = { "Contract_Number", "Withhold_Amount", "Withhold_Type", "Name", "Effective_Date" };

        GridViewExportUtil.ShowExcel(dt, HeaderCaptions, DataColumnsName, "Leave_Wages_Withhold_Rules", "Leave Wages Withhold Rules");

    }
    protected void gvCrewContractWithhold_RowDataBound(object sender, GridViewRowEventArgs e)
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

    protected void gvCrewContractWithhold_Sorting(object sender, GridViewSortEventArgs se)
    {
        ViewState["SORTBYCOLOUMN"] = se.SortExpression;

        if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
            ViewState["SORTDIRECTION"] = 1;
        else
            ViewState["SORTDIRECTION"] = 0;

        BindCrewWithhold();
    }
}