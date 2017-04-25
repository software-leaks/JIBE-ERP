using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using SMS.Business.Infrastructure;
using SMS.Properties;
using System.Data;
using System.Web.UI.HtmlControls;
using SMS.Business.SLC;

public partial class Purchase_SlopChestIndex : System.Web.UI.Page
{
    IFormatProvider iFormatProvider = CultureInfo.GetCultureInfo("en-GB");
    BLL_Infra_VesselLib objVsl = new BLL_Infra_VesselLib();
    BLL_SLC_Admin objBLL = new BLL_SLC_Admin();
    UserAccess objUA = new UserAccess();

    public string OperationMode = "";
    public Boolean uaEditFlag = false;
    public Boolean uaDeleteFlage = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        //UserAccessValidation();

        if (!IsPostBack)
        {
            ViewState["SORTDIRECTION"] = null;
            ViewState["SORTBYCOLOUMN"] = null;

            BindVessel();
            BindYear();
            BindMonths();
            BindCompany();

        }
    }

    public void BindVessel()
    {
        DataTable dtVessel = objVsl.Get_VesselList(0, 0, 0, "", Convert.ToInt32(Session["USERCOMPANYID"].ToString()));
        ddlVesselFilter.Items.Clear();
        ddlVesselFilter.DataSource = dtVessel;
        ddlVesselFilter.DataTextField = "Vessel_name";
        ddlVesselFilter.DataValueField = "Vessel_id";
        ddlVesselFilter.DataBind();
        ListItem li = new ListItem("--SELECT--", "0");
        ddlVesselFilter.Items.Insert(0, li);
    }

    public void BindYear()
    {
        int Year_Value = DateTime.Now.Year;

        ddlYearFilter.DataTextField = "Year";
        ddlYearFilter.DataValueField = "Year_Value";
        ddlYearFilter.DataBind();
        ListItem li = new ListItem("--SELECT--", "0");        
        ddlYearFilter.Items.Insert(0, li);
        int j =1;
        for (int i = Year_Value - 5; i < Year_Value + 5; i++)
        {

            li = new ListItem(i.ToString(), j.ToString());
            ddlYearFilter.Items.Insert(j, li);
            j++;
        }


    }

    public void BindMonths()
    {
        ddlMonth.DataTextField = "Month";
        ddlMonth.DataValueField = "Month_Value";
        ddlMonth.DataBind();
        ListItem li = new ListItem("--SELECT--", "0");
        ddlMonth.Items.Insert(0, li);
        
        li = new ListItem("JAN","1");
        ddlMonth.Items.Insert(1, li);
        
        li = new ListItem("FEB", "2");
        ddlMonth.Items.Insert(2, li);
        
        li = new ListItem("MAR", "3");
        ddlMonth.Items.Insert(3, li);

        li = new ListItem("APR", "4");
        ddlMonth.Items.Insert(4, li);

        li = new ListItem("MAY", "5");
        ddlMonth.Items.Insert(5, li);

        li = new ListItem("JUN", "6");
        ddlMonth.Items.Insert(6, li);

        li = new ListItem("JUL", "7");
        ddlMonth.Items.Insert(7, li);
        
        li = new ListItem("AUG", "8");
        ddlMonth.Items.Insert(8, li);

        li = new ListItem("SEP", "9");
        ddlMonth.Items.Insert(9, li);

        li = new ListItem("OCT", "10");
        ddlMonth.Items.Insert(10, li);

        li = new ListItem("NOV", "11");
        ddlMonth.Items.Insert(11, li);

        li = new ListItem("DEC", "12");
        ddlMonth.Items.Insert(12, li);
        
      

        
    }

    public void BindCompany()
    {
        int rowcount = ucCustomPagerItems.isCountRecord;

        int comID = UDFLib.ConvertToInteger(Session["USERCOMPANYID"]);

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

        //DataTable dt = objBLL.SearchCompany(txtfilter.Text != "" ? txtfilter.Text : null, UDFLib.ConvertIntegerToNull(ddlCompanyTypeFilter.SelectedValue), UDFLib.ConvertIntegerToNull(ddlCountryIncorpFilter.SelectedValue)
        //    , UDFLib.ConvertIntegerToNull(ddlCurrencyFilter.SelectedValue), UDFLib.ConvertIntegerToNull(ddlCountryFilter.SelectedValue), sortbycoloumn, sortdirection
        //  , ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);

        DataTable dt = objBLL.Get_SlopchestIndex(UDFLib.ConvertIntegerToNull(ddlVesselFilter.SelectedValue), UDFLib.ConvertIntegerToNull(ddlYearFilter.SelectedItem.Text), UDFLib.ConvertIntegerToNull(ddlMonth.SelectedValue),
            ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);



        if (ucCustomPagerItems.isCountRecord == 1)
        {
            ucCustomPagerItems.CountTotalRec = rowcount.ToString();
            ucCustomPagerItems.BuildPager();
        }


        if (dt.Rows.Count > 0)
        {
            GridViewSlopchest.DataSource = dt;
            GridViewSlopchest.DataBind();
        }
        else
        {
            GridViewSlopchest.DataSource = dt;
            GridViewSlopchest.DataBind();
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

        //if (objUA.Add == 0) ImgAdd.Visible = false;
        if (objUA.Edit == 1)
            uaEditFlag = true;
        //else
        //    btnsave.Visible = false;
        if (objUA.Delete == 1) uaDeleteFlage = true;

    }

    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }


    protected void GridViewSlopchest_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    System.Data.DataRowView dr = (System.Data.DataRowView)e.Row.DataItem;
        //    bool aVar = Convert.ToBoolean(dr["Verifiedval"].ToString());
        //    ImgNotVerify

        //    ImageButton img = (ImageButton)e.Row.FindControl("ImgVerified");
        //    ImageButton img1 = (ImageButton)e.Row.FindControl("ImgUnverified");
        //    if (img != null && img1 != null)
        //    {
        //        if (aVar == true)
        //        {   //img.ImageUrl = "~/Images/Allot-Flag-Completed.PNG";
        //            img.Visible = true;
        //            img1.Visible = false;
        //        }
        //        else
        //        {   // img.ImageUrl = "~/Images/Allot-Flag-Active.PNG";
        //            img.Visible = false;
        //            img1.Visible = true;
        //        }

        //    }
        //}

        //if (e.Row.RowType == DataControlRowType.Header)
        //{
        //    if (ViewState["SORTBYCOLOUMN"] != null)
        //    {
        //        HtmlImage img = (HtmlImage)e.Row.FindControl(ViewState["SORTBYCOLOUMN"].ToString());
        //        if (img != null)
        //        {
        //            if (ViewState["SORTDIRECTION"] == null || ViewState["SORTDIRECTION"].ToString() == "0")
        //                img.Src = "~/purchase/Image/arrowUp.png";
        //            else
        //                img.Src = "~/purchase/Image/arrowDown.png";

        //            img.Visible = true;
        //        }
        //    }
        //}



        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    e.Row.Attributes["onmouseover"] = "this.style.cursor='hand';this.style.backgroundColor='#FEECEC';";
        //    e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';this.style.color='black';this.style.backgroundColor=''";
        //}

    }

    protected void GridViewSlopchest_Sorting(object sender, GridViewSortEventArgs se)
    {

        ViewState["SORTBYCOLOUMN"] = se.SortExpression;

        if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
            ViewState["SORTDIRECTION"] = 1;
        else
            ViewState["SORTDIRECTION"] = 0;

        BindCompany();

    }

    protected void ImgAdd_Click(object sender, EventArgs e)
    {
        HiddenFlag.Value = "Add";

        OperationMode = "Add Company";

        string AddCompmodal = String.Format("showModal('divadd',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddCompmodal", AddCompmodal, true);
    }

    protected void onUpdate(object source, CommandEventArgs e)
    {

        HiddenFlag.Value = "Edit";
        OperationMode = "Edit Company";

        //string InfoDiv = "Get_Record_Information_Details('LIB_COMPANY','ID=" + txtCompanyID.Text + "')";
        //ScriptManager.RegisterStartupScript(this, this.GetType(), "InfoDiv", InfoDiv, true);


        string Companymodal = String.Format("showModal('divadd',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Companymodal", Companymodal, true);



    }

 
    protected void btnFilter_Click(object sender, EventArgs e)
    {

        BindCompany();

    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        ddlMonth.SelectedIndex = 0;
        ddlVesselFilter.SelectedIndex = 0;
        ddlYearFilter.SelectedIndex = 0;
               
        BindCompany();

    }

    protected void btnsave_Click(object sender, EventArgs e)
    {

        //if (HiddenFlag.Value == "Add")
        //{

        //    int responseid = objBLL.InsertCompany(int.Parse(txtCompCode.Text.Trim()), int.Parse(ddlCompanyType.SelectedValue), txtCompName.Text.Trim(), txtRegNo.Text.Trim()
        //      , UDFLib.ConvertDateToNull(txtDtIncorp.Text.Trim()), UDFLib.ConvertToInteger(ddlCountryIncorp.SelectedValue), UDFLib.ConvertIntegerToNull(ddlCurrency.SelectedValue), txtAddrerss.Text.Trim()
        //      , UDFLib.ConvertIntegerToNull(ddlAddressCountry.SelectedValue), txtEmail.Text.Trim(), txtPhone.Text.Trim()
        //      , UDFLib.ConvertToInteger(Session["UserID"]), txtShortName.Text, txtEmail2.Text, txtPhone2.Text, txtFax1.Text, txtFax2.Text);


        //    if (responseid != -1)
        //    {
        //        objBLL.InsertCompanyReletionship(UDFLib.ConvertToInteger(ddlParentCompany.SelectedValue), responseid, UDFLib.ConvertToInteger(ddlRelation.SelectedValue), UDFLib.ConvertToInteger(Session["UserID"]));
        //    }
        //    else
        //    {
        //        string js1 = "alert('Company name already exists');";
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUser", js1, true);
        //    }

        //}
        //else
        //{
        //    int responseid = objBLL.EditCompany(Convert.ToInt32(txtCompanyID.Text), Convert.ToInt32(txtCompCode.Text), Convert.ToInt32(ddlCompanyType.SelectedValue)
        //        , txtCompName.Text, txtShortName.Text, txtRegNo.Text, txtDtIncorp.Text, UDFLib.ConvertIntegerToNull(ddlCountryIncorp.SelectedValue), UDFLib.ConvertIntegerToNull(ddlCurrency.SelectedValue)
        //        , txtAddrerss.Text, UDFLib.ConvertIntegerToNull(ddlAddressCountry.SelectedValue), txtEmail.Text, txtPhone.Text, txtEmail2.Text, txtPhone2.Text, txtFax1.Text, txtFax2.Text, UDFLib.ConvertToInteger(Session["UserID"]));

        //}

        BindCompany();

        string hidemodal = String.Format("hideModal('divadd')");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hidemodal", hidemodal, true);

    }

    protected void ImgExpExcel_Click(object sender, EventArgs e)
    {


        int rowcount = ucCustomPagerItems.isCountRecord;

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

        //DataTable dt = objBLL.SearchCompany(txtfilter.Text != "" ? txtfilter.Text : null, UDFLib.ConvertIntegerToNull(ddlCompanyTypeFilter.SelectedValue), UDFLib.ConvertIntegerToNull(ddlCountryIncorpFilter.SelectedValue)
        //    , UDFLib.ConvertIntegerToNull(ddlCurrencyFilter.SelectedValue), UDFLib.ConvertIntegerToNull(ddlCountryFilter.SelectedValue), sortbycoloumn, sortdirection
        //  , null, null, ref  rowcount);


        string[] HeaderCaptions = { "Code", "Company Type", "Name", "Short Name", "Reg No", "Date Of Incorp", "Country of Incorp", "Currency", "Email", "Phone" };
        string[] DataColumnsName = { "Company_Code", "Company_Type", "Company_Name", "Short_Name", "Reg_Number", "Date_Of_Incorp", "COUNTRY_INCORP", "Currency_code", "Email1", "Phone1" };

       // GridViewExportUtil.ShowExcel(dt, HeaderCaptions, DataColumnsName, "Company", "Company", "");

    }

    
  
    
}