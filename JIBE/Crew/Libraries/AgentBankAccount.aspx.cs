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


public partial class AgentBankAccount : System.Web.UI.Page
{

    BLL_Infra_Currency objBLLCurrency = new BLL_Infra_Currency();
    BLL_Crew_Admin objBLL = new BLL_Crew_Admin();

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


            BindCurrencyDLL();
            BindManningOffice();
            BindAgentBank();
        }

    }




    protected void BindCurrencyDLL()
    {

        DataTable dt = objBLLCurrency.Get_CurrencyList();

        ddlCurrency.DataSource = dt;
        ddlCurrency.DataTextField = "Currency_Code";
        ddlCurrency.DataValueField = "Currency_ID";
        ddlCurrency.DataBind();
        ddlCurrency.Items.Insert(0, new ListItem("-SELECT-", "0"));


        ddlCurrencyFilter.DataSource = dt;
        ddlCurrencyFilter.DataTextField = "Currency_Code";
        ddlCurrencyFilter.DataValueField = "Currency_ID";
        ddlCurrencyFilter.DataBind();
        ddlCurrencyFilter.Items.Insert(0, new ListItem("-ALL-", "0"));

    }

    protected void BindManningOffice()
    {
        BLL_Crew_CrewDetails objBLLCrew = new BLL_Crew_CrewDetails();
        int UserCompanyID = UDFLib.ConvertToInteger(Session["USERCOMPANYID"].ToString());
        DataTable dt = objBLLCrew.Get_ManningAgentList(UserCompanyID);

        ddlManningOffice.DataSource = dt;
        ddlManningOffice.DataTextField = "COMPANY_NAME";
        ddlManningOffice.DataValueField = "ID";
        ddlManningOffice.DataBind();
        ddlManningOffice.Items.Insert(0, new ListItem("-SELECT-", "0"));


        ddlManningOfficeFilter.DataSource = dt;
        ddlManningOfficeFilter.DataTextField = "COMPANY_NAME";
        ddlManningOfficeFilter.DataValueField = "ID";
        ddlManningOfficeFilter.DataBind();
        ddlManningOfficeFilter.Items.Insert(0, new ListItem("-ALL-", "0"));
    
    }

    public void BindAgentBank()
    {

        int rowcount = ucCustomPagerItems.isCountRecord;

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

        DataTable dt = objBLL.Agent_Bank_Account_Search(txtfilter.Text != "" ? txtfilter.Text : null, UDFLib.ConvertIntegerToNull(ddlCurrencyFilter.SelectedValue), UDFLib.ConvertIntegerToNull(ddlManningOfficeFilter.SelectedValue),
            sortbycoloumn, sortdirection
             , ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);


        if (ucCustomPagerItems.isCountRecord == 1)
        {
            ucCustomPagerItems.CountTotalRec = rowcount.ToString();
            ucCustomPagerItems.BuildPager();
        }

        if (dt.Rows.Count > 0)
        {
             
            gvAgentBank.DataSource = dt;
            gvAgentBank.DataBind();
        }
        else
        {
            gvAgentBank.DataSource = dt;
            gvAgentBank.DataBind();
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
        this.SetFocus("ctl00_MainContent_txtBeneficiary");
        HiddenFlag.Value = "Add";
        OperationMode = "Add Agent Bank Account";

        ClearField();

        string JoiningType = String.Format("showModal('divadd',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "JoiningType", JoiningType, true);
    }

    protected void ClearField()
    {
        txtAgentBankAccountID.Text = "";
        txtBeneficiary.Text = "";
        txtAccountNo.Text = "";
        txtBankName.Text = "";
        txtBankAddress.Text = "";
        txtSwiftCode.Text = "";
        txtBankCode.Text = "";
        txtBranchCode.Text = "";
        ddlCurrency.SelectedValue = "0";
        ddlManningOffice.SelectedValue = "0";
    }

    protected void btnsave_Click(object sender, EventArgs e)
    {

        if (HiddenFlag.Value == "Add")
        {
            int retval = objBLL.Insert_Agent_Bank_Account(txtBeneficiary.Text,txtBankName.Text,txtBankAddress.Text,txtAccountNo.Text,txtSwiftCode.Text,txtBankCode.Text
                                            ,txtBranchCode.Text,UDFLib.ConvertIntegerToNull(ddlCurrency.SelectedValue),Convert.ToInt32(ddlManningOffice.SelectedValue), Convert.ToInt32(Session["USERID"]));
        }
        else
        {
            int retval = objBLL.Edit_Agent_Bank_Account(UDFLib.ConvertIntegerToNull(txtAgentBankAccountID.Text.Trim()),txtBeneficiary.Text,txtBankName.Text,txtBankAddress.Text,txtAccountNo.Text,txtSwiftCode.Text,txtBankCode.Text
                                            , txtBranchCode.Text, UDFLib.ConvertIntegerToNull(ddlCurrency.SelectedValue), Convert.ToInt32(ddlManningOffice.SelectedValue), Convert.ToInt32(Session["USERID"]));
        }

        BindAgentBank();

        string hidemodal = String.Format("hideModal('divadd')");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hidemodal", hidemodal, true);

    }

    protected void onUpdate(object source, CommandEventArgs e)
    {

        HiddenFlag.Value = "Edit";
        OperationMode = "Edit Agent Bank Account";

        DataTable dt = new DataTable();
        dt = objBLL.Get_Agent_Bank_Account_List(Convert.ToInt32(e.CommandArgument.ToString()));



        txtAgentBankAccountID.Text = dt.Rows[0]["ID"].ToString();
        txtBeneficiary.Text = dt.Rows[0]["Beneficiary"].ToString();
        txtAccountNo.Text = dt.Rows[0]["Acc_NO"].ToString();
        txtBankName.Text = dt.Rows[0]["Bank_Name"].ToString();
        txtBankAddress.Text = dt.Rows[0]["Bank_Address"].ToString();
        txtSwiftCode.Text = dt.Rows[0]["SwiftCode"].ToString();
        txtBankCode.Text = dt.Rows[0]["BANK_CODE"].ToString();
        txtBranchCode.Text = dt.Rows[0]["BRANCH_CODE"].ToString();
        ddlCurrency.SelectedValue = dt.DefaultView[0]["ACCOUNT_CURR"].ToString() != "" ? dt.DefaultView[0]["ACCOUNT_CURR"].ToString() : "0";
       
        if (ddlManningOffice.Items.FindByValue(dt.DefaultView[0]["MO_ID"].ToString()) != null)
            ddlManningOffice.SelectedValue = dt.DefaultView[0]["MO_ID"].ToString() != "" ? dt.DefaultView[0]["MO_ID"].ToString() : "0";
        else
            ddlManningOffice.SelectedValue = "0";

        string InfoDiv = "Get_Record_Information_Details('CRW_LIB_Agent_Bank_Account','ID=" + txtAgentBankAccountID.Text + "')";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "InfoDiv", InfoDiv, true);
      
        string AddSingoffReasonmodal = String.Format("showModal('divadd',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddSingoffReasonmodal", AddSingoffReasonmodal, true);

    }

    protected void onDelete(object source, CommandEventArgs e)
    {

        int retval = objBLL.Delete_Agent_Bank_Account(Convert.ToInt32(e.CommandArgument.ToString()), Convert.ToInt32(Session["USERID"]));

        BindAgentBank();

    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        BindAgentBank();
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        txtfilter.Text = "";
        ddlCurrencyFilter.SelectedValue = "0";
        ddlManningOfficeFilter.SelectedValue = "0";
        BindAgentBank();
    }

    protected void ImgExpExcel_Click(object sender, EventArgs e)
    {
        int rowcount = ucCustomPagerItems.isCountRecord;

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

        DataTable dt = objBLL.Agent_Bank_Account_Search(txtfilter.Text != "" ? txtfilter.Text : null, UDFLib.ConvertIntegerToNull(ddlCurrencyFilter.SelectedValue),
            UDFLib.ConvertIntegerToNull(ddlManningOfficeFilter.SelectedValue)
            , sortbycoloumn, sortdirection, null, null, ref  rowcount);



        string[] HeaderCaptions = { "Manning Office","Beneficiary", "A/c No.", "Bank Currency", "Swift Code", "Bank Code", "Branch Code", "Bank Address" };
        string[] DataColumnsName = { "COMPANY_NAME", "Beneficiary", "Acc_NO", "Currency_Code", "SwiftCode", "BANK_CODE", "BRANCH_CODE", "Bank_Address" };

        GridViewExportUtil.ShowExcel(dt, HeaderCaptions, DataColumnsName, "AgentBankAccount", "Agent Bank Account");

    }

    protected void gvAgentBank_RowDataBound(object sender, GridViewRowEventArgs e)
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

    protected void gvAgentBank_Sorting(object sender, GridViewSortEventArgs se)
    {
        ViewState["SORTBYCOLOUMN"] = se.SortExpression;

        if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
            ViewState["SORTDIRECTION"] = 1;
        else
            ViewState["SORTDIRECTION"] = 0;

        BindAgentBank();
    }
}