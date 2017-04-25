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
using System.Globalization;
using SMS.Properties;

public partial class Company : System.Web.UI.Page
{
    IFormatProvider iFormatProvider = CultureInfo.GetCultureInfo("en-GB");
    BLL_Infra_Company objBLL = new BLL_Infra_Company();
    BLL_Infra_Country objBLLCountry = new BLL_Infra_Country();
    BLL_Infra_Currency objBLLCurrency = new BLL_Infra_Currency();
    UserAccess objUA = new UserAccess();

    public string OperationMode = "";
    public Boolean uaEditFlag = false;
    public Boolean uaDeleteFlage = false;

    protected void Page_Load(object sender, EventArgs e)
    {
       // UserAccessValidation();
        if (!IsPostBack)
        {
            ViewState["SORTDIRECTION"] = null;
            ViewState["SORTBYCOLOUMN"] = null;

           // ucCustomPagerItems.PageSize = 15;

            //BindCompany();
            BindCountryDLL();
            BindCurrencyDLL();

            BindCompanyDLL();
            BindCompanyTypeDLL();
            BindCompanyRelationDLL();

        }
    }

    public void BindCompany()
    {
    //    int rowcount = ucCustomPagerItems.isCountRecord;

    //    string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
    //    int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

    //    DataTable dt = objBLL.SearchCompany(txtfilter.Text != "" ? txtfilter.Text : null, UDFLib.ConvertIntegerToNull(ddlCompanyTypeFilter.SelectedValue), UDFLib.ConvertIntegerToNull(ddlCountryIncorpFilter.SelectedValue)
    //        , UDFLib.ConvertIntegerToNull(ddlCurrencyFilter.SelectedValue), UDFLib.ConvertIntegerToNull(ddlCountryFilter.SelectedValue), sortbycoloumn, sortdirection
    //      , ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);



    //    if (ucCustomPagerItems.isCountRecord == 1)
    //    {
    //        ucCustomPagerItems.CountTotalRec = rowcount.ToString();
    //        ucCustomPagerItems.BuildPager();
    //    }


    //    if (dt.Rows.Count > 0)
    //    {
    //        GridViewCompany.DataSource = dt;
    //        GridViewCompany.DataBind();
    //    }
    //    else
    //    {
    //        GridViewCompany.DataSource = dt;
    //        GridViewCompany.DataBind();
    //    }


    }

    protected void UserAccessValidation()
    {
        //int CurrentUserID = GetSessionUserID();
        //string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        //BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
        //objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        //if (objUA.View == 0)
        //    Response.Redirect("~/default.aspx?msgid=1");

        //if (objUA.Add == 0) ImgAdd.Visible = false;
        //if (objUA.Edit == 1)
        //    uaEditFlag = true;
        //else
        //    btnsave.Visible = false;
        //if (objUA.Delete == 1) uaDeleteFlage = true;

    }

    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }

    protected void BindCountryDLL()
    {

        DataTable dt = objBLLCountry.Get_CountryList();

        ddlAddressCountry.DataSource = dt;
        ddlAddressCountry.DataTextField = "Country";
        ddlAddressCountry.DataValueField = "ID";
        ddlAddressCountry.DataBind();
        ddlAddressCountry.Items.Insert(0, new ListItem("-SELECT-", "0"));

        //ddlCountryFilter.DataSource = dt;
        //ddlCountryFilter.DataTextField = "Country";
        //ddlCountryFilter.DataValueField = "ID";
        //ddlCountryFilter.DataBind();
        //ddlCountryFilter.Items.Insert(0, new ListItem("-ALL-", "0"));


        ddlCountryIncorp.DataSource = dt;
        ddlCountryIncorp.DataTextField = "Country";
        ddlCountryIncorp.DataValueField = "ID";
        ddlCountryIncorp.DataBind();
        ddlCountryIncorp.Items.Insert(0, new ListItem("-SELECT-", "0"));


        //ddlCountryIncorpFilter.DataSource = dt;
        //ddlCountryIncorpFilter.DataTextField = "Country";
        //ddlCountryIncorpFilter.DataValueField = "ID";
        //ddlCountryIncorpFilter.DataBind();
        //ddlCountryIncorpFilter.Items.Insert(0, new ListItem("-ALL-", "0"));

    }

    protected void BindCurrencyDLL()
    {

        DataTable dt = objBLLCurrency.Get_CurrencyList();

        ddlCurrency.DataSource = dt;
        ddlCurrency.DataTextField = "Currency_Code";
        ddlCurrency.DataValueField = "Currency_ID";
        ddlCurrency.DataBind();
        ddlCurrency.Items.Insert(0, new ListItem("-SELECT-", "0"));


        //ddlCurrencyFilter.DataSource = dt;
        //ddlCurrencyFilter.DataTextField = "Currency_Code";
        //ddlCurrencyFilter.DataValueField = "Currency_ID";
        //ddlCurrencyFilter.DataBind();
        //ddlCurrencyFilter.Items.Insert(0, new ListItem("-ALL-", "0"));

    }

    protected void BindCompanyTypeDLL()
    {

        DataTable dt = objBLL.Get_CompanyTypeList();

        ddlCompanyType.Items.Clear();
        ddlCompanyType.DataSource = dt;
        ddlCompanyType.DataTextField = "Company_Type";
        ddlCompanyType.DataValueField = "ID";
        ddlCompanyType.DataBind();
        ddlCompanyType.Items.Insert(0, new ListItem("-SELECT-", "0"));


        //ddlCompanyTypeFilter.Items.Clear();
        //ddlCompanyTypeFilter.DataSource = dt;
        //ddlCompanyTypeFilter.DataTextField = "Company_Type";
        //ddlCompanyTypeFilter.DataValueField = "ID";
        //ddlCompanyTypeFilter.DataBind();
        //ddlCompanyTypeFilter.Items.Insert(0, new ListItem("-ALL-", "0"));


    }

    protected void BindCompanyDLL()
    {
        ddlParentCompany.DataTextField = "Company_Name";
        ddlParentCompany.DataValueField = "ID";
        ddlParentCompany.DataSource = objBLL.Get_CompanyListMini(UDFLib.ConvertToInteger(Session["USERCOMPANYID"]));
        ddlParentCompany.DataBind();
        ddlParentCompany.Items.Insert(0, new ListItem("-SELECT-", "0"));

    }

    protected void BindCompanyRelationDLL()
    {
        ddlRelation.DataTextField = "Relationship_Name";
        ddlRelation.DataValueField = "ID";
        ddlRelation.DataSource = objBLL.Get_CompanyRelationType(UDFLib.ConvertToInteger(Session["USERID"]));
        ddlRelation.DataBind();
        ddlRelation.Items.Insert(0, new ListItem("-SELECT-", "0"));
    }

    protected void GridViewCompany_RowDataBound(object sender, GridViewRowEventArgs e)
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

    protected void GridViewCompany_Sorting(object sender, GridViewSortEventArgs se)
    {

        ViewState["SORTBYCOLOUMN"] = se.SortExpression;

        if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
            ViewState["SORTDIRECTION"] = 1;
        else
            ViewState["SORTDIRECTION"] = 0;

       // BindCompany();

    }

    protected void ImgAdd_Click(object sender, EventArgs e)
    {
    //    //HiddenFlag.Value = "Add";

    //    OperationMode = "Add Company";

    //    txtCompanyID.Text = "";

    //    ddlCompanyType.SelectedValue = "0";
    //    ddlAddressCountry.SelectedValue = "0";
    //    ddlCurrency.SelectedValue = "0";
    //    ddlCountryIncorp.SelectedValue = "0";
    //    ddlParentCompany.SelectedValue = "0";
    //    ddlRelation.SelectedValue = "0";


    //    ddlParentCompany.Enabled = false;
    //    ddlRelation.Enabled = false;

    //    td_Relation.Visible = false;
    //    td_ParentCompany.Visible = false;


    //    txtCompCode.Text = "";
    //    txtCompName.Text = "";
    //    txtShortName.Text = "";
    //    txtRegNo.Text = "";
    //    txtDtIncorp.Text = "";
    //    txtAddrerss.Text = "";
    //    txtEmail.Text = "";
    //    txtPhone.Text = "";


    //    ddlParentCompany.Enabled = true;
    //    ddlRelation.Enabled = true;

    //    td_Relation.Visible = true;
    //    td_ParentCompany.Visible = true;



    //    string AddCompmodal = String.Format("showModal('divadd',false);");
    //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddCompmodal", AddCompmodal, true);
    }

    protected void onUpdate(object source, CommandEventArgs e)
    {

        //HiddenFlag.Value = "Edit";
        //OperationMode = "Edit Company";
        OperationMode = "Register Company";

        DataTable dt = new DataTable();
        dt = objBLL.Get_CompanyList();
        dt.DefaultView.RowFilter = "ID= '" + e.CommandArgument.ToString() + "'";

        txtCompanyID.Text = dt.DefaultView[0]["ID"].ToString();

        ddlCompanyType.SelectedValue = dt.DefaultView[0]["Company_TypeID"].ToString() != "" ? dt.DefaultView[0]["Company_TypeID"].ToString() : "0";
        ddlAddressCountry.SelectedValue = dt.DefaultView[0]["Country"].ToString() != "" ? dt.DefaultView[0]["Country"].ToString() : "0";
        ddlCurrency.SelectedValue = dt.DefaultView[0]["Base_Curr"].ToString() != "" ? dt.DefaultView[0]["Base_Curr"].ToString() : "0";
        ddlCountryIncorp.SelectedValue = dt.DefaultView[0]["Country_Of_Incorp"].ToString() != "" ? dt.DefaultView[0]["Country_Of_Incorp"].ToString() : "0";


        // if company type = Manning Agent then as per Bikash

        if (dt.DefaultView[0]["Company_TypeID"].ToString() == "3")
        {
            ddlParentCompany.SelectedValue = dt.DefaultView[0]["Parent_Company_ID"].ToString() != "" ? dt.DefaultView[0]["Parent_Company_ID"].ToString() : "0";
            ddlRelation.SelectedValue = dt.DefaultView[0]["Relation"].ToString() != "" ? dt.DefaultView[0]["Relation"].ToString() : "0";
        }


        ddlParentCompany.Enabled = false;
        ddlRelation.Enabled = false;

        td_Relation.Visible = false;
        td_ParentCompany.Visible = false;


        txtCompCode.Text = dt.DefaultView[0]["Company_Code"].ToString();
        txtCompName.Text = dt.DefaultView[0]["Company_Name"].ToString();
        txtShortName.Text = dt.DefaultView[0]["Short_Name"].ToString();
        txtRegNo.Text = dt.DefaultView[0]["Reg_Number"].ToString();
        txtDtIncorp.Text = dt.DefaultView[0]["Date_Of_Incorp"].ToString();
        txtAddrerss.Text = dt.DefaultView[0]["Address"].ToString();
        txtEmail.Text = dt.DefaultView[0]["Email1"].ToString();
        txtPhone.Text = dt.DefaultView[0]["phone1"].ToString();

        txtEmail2.Text = dt.DefaultView[0]["Email2"].ToString();
        txtPhone2.Text = dt.DefaultView[0]["Phone2"].ToString();

        txtFax1.Text = dt.DefaultView[0]["Fax1"].ToString();
        txtFax2.Text = dt.DefaultView[0]["Fax2"].ToString();


        string InfoDiv = "Get_Record_Information_Details('LIB_COMPANY','ID=" + txtCompanyID.Text + "')";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "InfoDiv", InfoDiv, true);


        string Companymodal = String.Format("showModal('divadd',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Companymodal", Companymodal, true);



    }

    protected void onDelete(object source, CommandEventArgs e)
    {

    //    int retval = objBLL.DeleteCompany_DL(Convert.ToInt32(e.CommandArgument.ToString()), UDFLib.ConvertToInteger(Session["UserID"]));
    //    BindCompany();
    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {

    //    BindCompany();

    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {


    //    txtfilter.Text = "";
    //    ddlCompanyTypeFilter.SelectedValue = "0";
    //    ddlCountryIncorpFilter.SelectedValue = "0";
    //    ddlCurrencyFilter.SelectedValue = "0";
    //    ddlCountryFilter.SelectedValue = "0";

    //    BindCompany();

    }

    protected void btnsave_Click(object sender, EventArgs e)
    {

        //if (HiddenFlag.Value == "Add")
        //{

            int responseid = objBLL.InsertCompany(int.Parse(txtCompCode.Text.Trim()), int.Parse(ddlCompanyType.SelectedValue), txtCompName.Text.Trim(), txtRegNo.Text.Trim()
              , UDFLib.ConvertDateToNull(txtDtIncorp.Text.Trim()), UDFLib.ConvertToInteger(ddlCountryIncorp.SelectedValue), UDFLib.ConvertIntegerToNull(ddlCurrency.SelectedValue), txtAddrerss.Text.Trim()
              , UDFLib.ConvertIntegerToNull(ddlAddressCountry.SelectedValue), txtEmail.Text.Trim(), txtPhone.Text.Trim()
              , UDFLib.ConvertToInteger(Session["UserID"]), txtShortName.Text, txtEmail2.Text, txtPhone2.Text, txtFax1.Text, txtFax2.Text);


            if (responseid != -1)
            {
                objBLL.InsertCompanyReletionship(UDFLib.ConvertToInteger(ddlParentCompany.SelectedValue), responseid, UDFLib.ConvertToInteger(ddlRelation.SelectedValue), UDFLib.ConvertToInteger(Session["UserID"]));
            }
            else
            {
                string js1 = "alert('Company name already exists');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUser", js1, true);
            }

        //}
        //else
        //{
        //    int responseid = objBLL.EditCompany(Convert.ToInt32(txtCompanyID.Text), Convert.ToInt32(txtCompCode.Text), Convert.ToInt32(ddlCompanyType.SelectedValue)
        //        , txtCompName.Text, txtShortName.Text, txtRegNo.Text, txtDtIncorp.Text, UDFLib.ConvertIntegerToNull(ddlCountryIncorp.SelectedValue), UDFLib.ConvertIntegerToNull(ddlCurrency.SelectedValue)
        //        , txtAddrerss.Text, UDFLib.ConvertIntegerToNull(ddlAddressCountry.SelectedValue), txtEmail.Text, txtPhone.Text, txtEmail2.Text, txtPhone2.Text, txtFax1.Text, txtFax2.Text, UDFLib.ConvertToInteger(Session["UserID"]));

        //}

        //BindCompany();

        string hidemodal = String.Format("hideModal('divadd')");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hidemodal", hidemodal, true);

    }

    protected void ImgExpExcel_Click(object sender, EventArgs e)
    {


    //    int rowcount = ucCustomPagerItems.isCountRecord;

    //    string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
    //    int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

    //    DataTable dt = objBLL.SearchCompany(txtfilter.Text != "" ? txtfilter.Text : null, UDFLib.ConvertIntegerToNull(ddlCompanyTypeFilter.SelectedValue), UDFLib.ConvertIntegerToNull(ddlCountryIncorpFilter.SelectedValue)
    //        , UDFLib.ConvertIntegerToNull(ddlCurrencyFilter.SelectedValue), UDFLib.ConvertIntegerToNull(ddlCountryFilter.SelectedValue), sortbycoloumn, sortdirection
    //      , null, null, ref  rowcount);


    //    string[] HeaderCaptions = { "Code", "Company Type", "Name", "Short Name", "Reg No", "Date Of Incorp", "Country of Incorp", "Currency", "Email", "Phone" };
    //    string[] DataColumnsName = { "Company_Code", "Company_Type", "Company_Name", "Short_Name", "Reg_Number", "Date_Of_Incorp", "COUNTRY_INCORP", "Currency_code", "Email1", "Phone1" };

    //    GridViewExportUtil.ShowExcel(dt, HeaderCaptions, DataColumnsName, "Company", "Company", "");

   }


    public void btnHiddenSubmit_Click(object sender, EventArgs e)
    {
        BindCompanyTypeDLL();
    }

    protected void ddlCompanyType_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (Convert.ToInt32(ddlCompanyType.SelectedValue) == 9)
        //{
        //    ddlParentCompany.SelectedValue = "1";
        //}

    }
}
