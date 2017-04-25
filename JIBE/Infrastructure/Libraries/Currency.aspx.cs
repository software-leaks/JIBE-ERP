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

public partial class Currency : System.Web.UI.Page
{
    BLL_Infra_Currency objBLL = new BLL_Infra_Currency();
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
            //txtCurrencyDesc.Attributes.Add("maxlength", txtCurrencyDesc.MaxLength.ToString());
            ViewState["SORTDIRECTION"] = null;
            ViewState["SORTBYCOLOUMN"] = null;

            ucCustomPagerItems.PageSize = 20;
            Load_CountryList();
            BindCurrency();
        }

    }

    public void BindCurrency()
    {
       
        int rowcount = ucCustomPagerItems.isCountRecord;
        int? isfavorite = null; if (ddlFavorite.SelectedValue != "2") isfavorite = Convert.ToInt32(ddlFavorite.SelectedValue.ToString());
        int? countrycode = null; if (ddlSearchCountry.SelectedValue != "0") countrycode = Convert.ToInt32(ddlSearchCountry.SelectedValue.ToString());

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());


        DataTable dt = objBLL.SearchCurrency(txtfilter.Text != "" ? txtfilter.Text : null, countrycode, isfavorite, sortbycoloumn, sortdirection
             , ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);


        if (ucCustomPagerItems.isCountRecord == 1)
        {
            ucCustomPagerItems.CountTotalRec = rowcount.ToString();
            ucCustomPagerItems.BuildPager();
        }


        if (dt.Rows.Count > 0)
        {
            GridViewcurrency.DataSource = dt;
            GridViewcurrency.DataBind();
        }
        else
        {
            GridViewcurrency.DataSource = dt;
            GridViewcurrency.DataBind();
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

    protected void Load_CountryList()
    {
        DataTable dt = objBLLCountry.Get_CountryList();

        ddlAddCountry.DataSource = dt;
        ddlAddCountry.DataTextField = "Country";
        ddlAddCountry.DataValueField = "ID";
        ddlAddCountry.DataBind();

        ddlSearchCountry.DataSource = dt;
        ddlSearchCountry.DataTextField = "Country";
        ddlSearchCountry.DataValueField = "ID";
        ddlSearchCountry.DataBind();
    }

    protected void ImgAdd_Click(object sender, EventArgs e)
    {
        this.SetFocus("ctl00_MainContent_txtCurrencyCode");
        HiddenFlag.Value = "Add";

        OperationMode = "Add Currency";

        txtCurrencyID.Text = "";
        txtCurrencyCode.Text = "";
        txtCurrencyDesc.Text = "";
        ddlAddCountry.SelectedValue = "0";
        chkfav.Checked = false;

        string AddDeptmodal = String.Format("showModal('divadd',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddDeptmodal", AddDeptmodal, true);
    }

    protected void btnsave_Click(object sender, EventArgs e)
    {


        if (HiddenFlag.Value == "Add")
        {
            int responseid = objBLL.InsertCurrency(txtCurrencyCode.Text.Trim(), txtCurrencyDesc.Text.Trim(), int.Parse(ddlAddCountry.SelectedValue), chkfav.Checked==true ?true : false, Convert.ToInt32(Session["USERID"]));

        }
        else
        {
            int responseid = objBLL.EditCurrency(txtCurrencyCode.Text, txtCurrencyDesc.Text, int.Parse(ddlAddCountry.SelectedValue), int.Parse(txtCurrencyID.Text), chkfav.Checked == true ? true : false, Convert.ToInt32(Session["USERID"]));

        }

        BindCurrency();
        string hidemodal = String.Format("hideModal('divadd')");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hidemodal", hidemodal, true);
        
    }

    protected void onUpdate(object source, CommandEventArgs e)
    {
    
            HiddenFlag.Value = "Edit";
            OperationMode = "Edit Currency";

            DataTable dt  = new DataTable();
            dt = objBLL.Get_CurrencyList();
            dt.DefaultView.RowFilter = "Currency_ID= '" + e.CommandArgument.ToString() + "'";
            txtCurrencyID.Text = dt.DefaultView[0]["Currency_ID"].ToString();
            txtCurrencyCode.Text = dt.DefaultView[0]["Currency_Code"].ToString();
            txtCurrencyDesc.Text = dt.DefaultView[0]["Currency_Discription"].ToString();
            ddlAddCountry.SelectedValue = dt.DefaultView[0]["Country"].ToString();

            chkfav.Checked = dt.DefaultView[0]["Favorite"].ToString() == "True" ? true : false;



            string InfoDiv = "Get_Record_Information_Details('LIB_CURRENCY','Currency_ID=" + txtCurrencyID.Text + "')";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "InfoDiv", InfoDiv, true);


            string Deptmodal = String.Format("showModal('divadd',false);");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Deptmodal", Deptmodal, true);

    }

    protected void onDelete(object source, CommandEventArgs e)
    { 
    
        int retval = objBLL.DeleteCurrency(Convert.ToInt32(e.CommandArgument.ToString()), Convert.ToInt32(Session["USERID"]));
        BindCurrency();

    
    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        BindCurrency();
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        txtfilter.Text = "";
        ddlSearchCountry.SelectedValue = "0";
        ddlFavorite.SelectedValue = "2";

        BindCurrency();
    
    }


    protected void ImgExpExcel_Click(object sender, EventArgs e)
    {

        int rowcount = ucCustomPagerItems.isCountRecord;
        int? isfavorite = null; if (ddlFavorite.SelectedValue != "2") isfavorite = Convert.ToInt32(ddlFavorite.SelectedValue.ToString());
        int? countrycode = null; if (ddlSearchCountry.SelectedValue != "0") countrycode = Convert.ToInt32(ddlSearchCountry.SelectedValue.ToString());

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());


        DataTable dt = objBLL.SearchCurrency(txtfilter.Text != "" ? txtfilter.Text : null, countrycode, isfavorite, sortbycoloumn, sortdirection
             , null, null, ref  rowcount);

        string[] HeaderCaptions = { "Code", "Description" , "Country", "Favorite"};
        string[] DataColumnsName = { "Currency_Code", "Currency_Discription", "Country_Name", "Favorite" };

        GridViewExportUtil.ShowExcel(dt, HeaderCaptions, DataColumnsName, "Currency", "Currency", "");

    }


    protected void GridViewcurrency_RowDataBound(object sender, GridViewRowEventArgs e)
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


    protected void GridViewcurrency_Sorting(object sender, GridViewSortEventArgs se)
    {
        ViewState["SORTBYCOLOUMN"] = se.SortExpression;

        if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
            ViewState["SORTDIRECTION"] = 1;
        else
            ViewState["SORTDIRECTION"] = 0;

        BindCurrency();
    }
}
