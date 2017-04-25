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
//using SMS.Business.Technical;
using SMS.Business.Inspection;
public partial class ChecklistIndex : System.Web.UI.Page
{
    IFormatProvider iFormatProvider = CultureInfo.GetCultureInfo("en-GB");
    BLL_INSP_Checklist objBl = new BLL_INSP_Checklist();
    BLL_Infra_VesselLib objBLLVesslType = new BLL_Infra_VesselLib();

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

            ucCustomPagerItems.PageSize = 15;

            BindCompany();
            BindChecklistTypeDLL();

            BindVesselTypeDLL();


        }
        ImageButton lbButton = this.btnFilter;

        ContentPlaceHolder contentPlaceHolder = (ContentPlaceHolder)Master.FindControl("MainContent");
        if (contentPlaceHolder != null)
        {
            contentPlaceHolder.Page.Form.DefaultButton = lbButton.UniqueID;
        }

    }

    public void BindCompany()
    {
        int rowcount = ucCustomPagerItems.isCountRecord;

        int comID = UDFLib.ConvertToInteger(Session["USERCOMPANYID"]);

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

        //DataTable dt = objBLL.SearchCompany_verify(txtfilter.Text != "" ? txtfilter.Text : null, UDFLib.ConvertIntegerToNull(ddlCompanyTypeFilter.SelectedValue), UDFLib.ConvertIntegerToNull(ddlCountryIncorpFilter.SelectedValue)
        //   , UDFLib.ConvertIntegerToNull(ddlCurrencyFilter.SelectedValue), UDFLib.ConvertIntegerToNull(ddlCountryFilter.SelectedValue), comID.ToString(), sortbycoloumn, sortdirection
        // , ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);

        DataTable dtAllCHK = objBl.Get_CheckListAll(txtfilter.Text != "" ? txtfilter.Text : null, UDFLib.ConvertIntegerToNull(ddlCheklistFilter.SelectedValue), UDFLib.ConvertIntegerToNull(ddlVesselTypeFilter.SelectedValue), sortbycoloumn, sortdirection
         , ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);

        GridViewCompany.DataSource = dtAllCHK;
        GridViewCompany.DataBind();

        ucCustomPagerItems.CountTotalRec = rowcount.ToString();
        ucCustomPagerItems.BuildPager();

        //if (ucCustomPagerItems.isCountRecord == 1)
        //{
        //    ucCustomPagerItems.CountTotalRec = rowcount.ToString();
        //    ucCustomPagerItems.BuildPager();
        //}


        //if (dtAllCHK.Rows.Count > 0)
        //{
        //    GridViewCompany.DataSource = dtAllCHK;
        //    GridViewCompany.DataBind();
        //}
        //else
        //{
        //    GridViewCompany.DataSource = dtAllCHK;
        //    GridViewCompany.DataBind();
        //}


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
        //else
        //btnsave.Visible = false;
        // if (objUA.Delete == 1) uaDeleteFlage = true;

    }

    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }

    protected void BindChecklistTypeDLL()
    {

        DataTable dt = objBl.Get_CheckListType(UDFLib.ConvertIntegerToNull(""));

        ddlCheklistFilter.DataSource = dt;
        ddlCheklistFilter.DataTextField = "Category_Name";
        ddlCheklistFilter.DataValueField = "ID";
        ddlCheklistFilter.DataBind();
        ddlCheklistFilter.Items.Insert(0, new ListItem("-ALL-", "0"));

    }



    protected void BindVesselTypeDLL()
    {

        DataTable dt = objBLLVesslType.Get_VesselType();


        ddlVesselTypeFilter.Items.Clear();
        ddlVesselTypeFilter.DataSource = dt;
        ddlVesselTypeFilter.DataTextField = "VesselTypes";
        ddlVesselTypeFilter.DataValueField = "ID";
        ddlVesselTypeFilter.DataBind();
        ddlVesselTypeFilter.Items.Insert(0, new ListItem("-ALL-", "0"));


    }



    protected void GridViewCompany_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            System.Data.DataRowView dr = (System.Data.DataRowView)e.Row.DataItem;
            //bool aVar = Convert.ToBoolean( dr["Verifiedval"].ToString());
            //ImgNotVerify

            //ImageButton img = (ImageButton)e.Row.FindControl("ImgVerified");
            //ImageButton img1 = (ImageButton)e.Row.FindControl("ImgUnverified");
            //if (img != null && img1!=null)
            //{
            //    if (aVar == true)
            //    {   //img.ImageUrl = "~/Images/Allot-Flag-Completed.PNG";
            //        img.Visible = true;
            //        img1.Visible =false ;
            //    }
            //    else
            //    {   // img.ImageUrl = "~/Images/Allot-Flag-Active.PNG";
            //        img.Visible = false;
            //        img1.Visible =true ;
            //    }

            //}
        }

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

        BindCompany();

    }

    protected void ImgAdd_Click(object sender, EventArgs e)
    {
        HiddenFlag.Value = "Add";

        OperationMode = "Add Company";

        Response.Redirect("~/Technical/Inspection/CheckList.aspx");

        //string AddCompmodal = String.Format("showModal('divadd',false);");
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddCompmodal", AddCompmodal, true);
    }

    protected void onUpdate(object source, CommandEventArgs e)
    {

        HiddenFlag.Value = "Edit";
        OperationMode = "Edit Company";

        Response.Redirect("~/Technical/Inspection/CheckList.aspx?CHKID=" + e.CommandArgument.ToString() + "");

        //DataTable dt = new DataTable();
        //dt = objBLL.Get_CompanyList();
        //dt.DefaultView.RowFilter = "ID= '" + e.CommandArgument.ToString() + "'";




        //string InfoDiv = "Get_Record_Information_Details('LIB_COMPANY','ID=" + txtCompanyID.Text + "')";
        //ScriptManager.RegisterStartupScript(this, this.GetType(), "InfoDiv", InfoDiv, true);


        //string Companymodal = String.Format("showModal('divadd',false);");
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Companymodal", Companymodal, true);

    }



    protected void onDelete(object source, CommandEventArgs e)
    {

        //int retval = objBLL.DeleteCompany_DL(Convert.ToInt32(e.CommandArgument.ToString()), UDFLib.ConvertToInteger(Session["UserID"]));
        BindCompany();
    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {

        BindCompany();

    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        txtfilter.Text = "";
        ddlVesselTypeFilter.SelectedValue = "0";
        ddlCheklistFilter.SelectedValue = "0";


        BindCompany();

    }


    protected void ImgExpExcel_Click(object sender, EventArgs e)
    {
        //int rowcount = ucCustomPagerItems.isCountRecord;

        //string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        //int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

        //DataTable dt = objBLL.SearchCompany(txtfilter.Text != "" ? txtfilter.Text : null, UDFLib.ConvertIntegerToNull(ddlCompanyTypeFilter.SelectedValue), UDFLib.ConvertIntegerToNull(ddlCountryIncorpFilter.SelectedValue)
        //    , UDFLib.ConvertIntegerToNull(ddlCurrencyFilter.SelectedValue), UDFLib.ConvertIntegerToNull(ddlCountryFilter.SelectedValue), sortbycoloumn, sortdirection
        //  , null, null, ref  rowcount);


        //string[] HeaderCaptions = { "Code", "Company Type", "Name", "Short Name", "Reg No", "Date Of Incorp", "Country of Incorp", "Currency", "Email", "Phone" };
        //string[] DataColumnsName = { "Company_Code", "Company_Type", "Company_Name", "Short_Name", "Reg_Number", "Date_Of_Incorp", "COUNTRY_INCORP", "Currency_code", "Email1", "Phone1" };

        //GridViewExportUtil.ShowExcel(dt, HeaderCaptions, DataColumnsName, "Company", "Company", "");

    }


    public void btnHiddenSubmit_Click(object sender, EventArgs e)
    {
        //BindCompanyTypeDLL();
        BindVesselTypeDLL();
    }
    protected void ddlVesselTypeFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindCompany();
    }
    protected void ddlCheklistFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindCompany();
    }
}
