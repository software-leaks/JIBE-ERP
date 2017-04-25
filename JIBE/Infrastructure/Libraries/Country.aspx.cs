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

public partial class Country : System.Web.UI.Page
{
   
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
            BindCountry();
        }

    }



    public void BindCountry()
    {

        int rowcount = ucCustomPagerItems.isCountRecord;
        //int? isfavorite = null; if (ddlFavorite.SelectedValue != "2") isfavorite = Convert.ToInt32(ddlFavorite.SelectedValue.ToString());
        //int? countrycode = null; if (ddlSearchCountry.SelectedValue != "0") countrycode = Convert.ToInt32(ddlSearchCountry.SelectedValue.ToString());

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());


        DataTable dt = objBLLCountry.SearchCountry(txtfilter.Text != "" ? txtfilter.Text : null,sortbycoloumn, sortdirection
             , ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);


        if (ucCustomPagerItems.isCountRecord == 1)
        {
            ucCustomPagerItems.CountTotalRec = rowcount.ToString();
            ucCustomPagerItems.BuildPager();
        }


        if (dt.Rows.Count > 0)
        {
            GridViewCountry.DataSource = dt;
            GridViewCountry.DataBind();
        }
        else
        {
            GridViewCountry.DataSource = dt;
            GridViewCountry.DataBind();
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
        this.SetFocus("ctl00_MainContent_txtCountry");
        HiddenFlag.Value = "Add";

        OperationMode = "Add Country";

        txtCountryID.Text = "";
        txtCountry.Text = "";
        txtISOCode.Text = "";
      

        string AddCountrymodal = String.Format("showModal('divadd',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddCountrymodal", AddCountrymodal, true);
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {


        if (HiddenFlag.Value == "Add")
        {
            int responseid = objBLLCountry.InsertCountry(txtCountry.Text.Trim(), txtISOCode.Text.Trim(), Convert.ToInt32(Session["USERID"]));
        }
        else
        {
            int responseid = objBLLCountry.EditCountry(Convert.ToInt32(txtCountryID.Text), txtCountry.Text.Trim(), txtISOCode.Text.Trim(), Convert.ToInt32(Session["USERID"]));
        
        }

        BindCountry();

        string hidemodal = String.Format("hideModal('divadd')");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hidemodal", hidemodal, true);

    }




    protected void onUpdate(object source, CommandEventArgs e)
    {

        HiddenFlag.Value = "Edit";
        OperationMode = "Edit Country";

        DataTable dt = new DataTable();
        dt = objBLLCountry.Get_CountryList();
        dt.DefaultView.RowFilter = "ID= '" + e.CommandArgument.ToString() + "'";

        txtCountryID.Text = dt.DefaultView[0]["ID"].ToString();
        txtCountry.Text = dt.DefaultView[0]["COUNTRY"].ToString();
        txtISOCode.Text = dt.DefaultView[0]["ISO_Code"].ToString();



        string InfoDiv = "Get_Record_Information_Details('LIB_Country','ID=" + txtCountryID.Text + "')";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "InfoDiv", InfoDiv, true);



        string Countrymodal = String.Format("showModal('divadd',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Countrymodal", Countrymodal, true);
    }


    protected void onDelete(object source, CommandEventArgs e)
    {
        int retval = objBLLCountry.DeleteCountry(Convert.ToInt32(e.CommandArgument.ToString()), Convert.ToInt32(Session["USERID"]));
        BindCountry();
    
    
    }


    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        txtfilter.Text = "";
        BindCountry();

    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {

        BindCountry();
    }



    protected void ImgExpExcel_Click(object sender, EventArgs e)
    {
       
        
        int rowcount = ucCustomPagerItems.isCountRecord;
        //int? isfavorite = null; if (ddlFavorite.SelectedValue != "2") isfavorite = Convert.ToInt32(ddlFavorite.SelectedValue.ToString());
        //int? countrycode = null; if (ddlSearchCountry.SelectedValue != "0") countrycode = Convert.ToInt32(ddlSearchCountry.SelectedValue.ToString());

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());


        DataTable dt = objBLLCountry.SearchCountry(txtfilter.Text != "" ? txtfilter.Text : null, sortbycoloumn, sortdirection
             , null, null, ref  rowcount);


        string[] HeaderCaptions = { "Country", "ISO Code"};
        string[] DataColumnsName = { "Country_Name", "ISO_Code" };

        GridViewExportUtil.ShowExcel(dt, HeaderCaptions, DataColumnsName, "Country", "Country", "");

    }

    protected void GridViewCountry_RowDataBound(object sender, GridViewRowEventArgs e)
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


    protected void GridViewCountry_Sorting(object sender, GridViewSortEventArgs se)
    {
        ViewState["SORTBYCOLOUMN"] = se.SortExpression;

        if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
            ViewState["SORTDIRECTION"] = 1;
        else
            ViewState["SORTDIRECTION"] = 0;

        BindCountry();
    }

}
