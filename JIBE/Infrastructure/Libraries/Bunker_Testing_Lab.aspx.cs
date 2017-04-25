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
using System.Text.RegularExpressions;
using SMS.Business.Infrastructure;
using System.Globalization;
using SMS.Properties;


public partial class Infrastructure_Libraries_Bunker_Testing_Lab : System.Web.UI.Page
{
    public string OperationMode = "";
    BLL_Infra_Company objBLL = new BLL_Infra_Company();
    protected void Page_Load(object sender, EventArgs e)
    {
        UserAccessValidation();
        if (!IsPostBack)
        {
            ViewState["SORTDIRECTION"] = null;
            ViewState["SORTBYCOLOUMN"] = null;

            ucCustomPagerItems.PageSize = 20;
            Load_CountryList();
            Bind_LOTestingLabs();
        }

    }

    public void Bind_LOTestingLabs()
    {

        int rowcount = ucCustomPagerItems.isCountRecord;

        int countrycode = 0;
        if (ddlSearchCountry.SelectedValue != "0") countrycode = Convert.ToInt32(ddlSearchCountry.SelectedValue.ToString());

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());


        DataTable dt = objBLL.Get_BunkerTestingLabList(txtfilter.Text, countrycode, GetSessionUserID(), sortbycoloumn, sortdirection, ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);
        GridView_Labs.DataSource = dt;
        GridView_Labs.DataBind();

    }

    protected void UserAccessValidation()
    {
        UserAccess objUA = new UserAccess();
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        SMS.Business.Infrastructure.BLL_Infra_UserCredentials objUser = new SMS.Business.Infrastructure.BLL_Infra_UserCredentials();
        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
            Response.Redirect("~/default.aspx?msgid=1");

        if (objUA.Add == 0)
        {
            ImgAdd.Visible = false;
        }
        if (objUA.Edit == 0)
        {
            GridView_Labs.Columns[GridView_Labs.Columns.Count - 2].Visible = false;
        }
        if (objUA.Delete == 0)
        {
            GridView_Labs.Columns[GridView_Labs.Columns.Count - 1].Visible = false;
        }
        if (objUA.Approve == 0)
        {

        }

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
        BLL_Infra_Country objBLLCountry = new BLL_Infra_Country();
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
        this.SetFocus("ctl00_MainContent_txtLabName");
        HiddenFlag.Value = "Add";

        OperationMode = "Add Bunker Testing Lab";

        txtLabName.Text = "";
        txtAddress.Text = "";
        txtPhone.Text = "";
        txtEmail.Text = "";
        ddlAddCountry.SelectedValue = "0";


        string js = String.Format("showModal('divadd',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddDivModal", js, true);
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        int res_id = 0;
        string js = "";

        if ( HiddenFlag.Value.Equals("Edit"))
            OperationMode = "Edit Bunker Testing Lab";
        else
            OperationMode = "Add Bunker Testing Lab";

        if (txtEmail.Text.Trim() != "" && !checkEmail(txtEmail.Text))
        {
            js = String.Format("alert('Please enter Valid E-mail');showModal('divadd',false);");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "js", js, true);
        }

        if (js == "")
        {
            if (HiddenFlag.Value == "Add")
            {
                res_id = objBLL.Insert_Bunker_Testing_Lab(txtLabName.Text.Trim(), txtAddress.Text.Trim(), txtEmail.Text.Trim(), txtPhone.Text.Trim(), int.Parse(ddlAddCountry.SelectedValue), GetSessionUserID());

            }
            else
            {
                res_id = objBLL.Update_Bunker_Testing_Lab(UDFLib.ConvertToInteger(Selected_ID.Value), txtLabName.Text.Trim(), txtAddress.Text.Trim(), txtEmail.Text.Trim(), txtPhone.Text.Trim(), UDFLib.ConvertToInteger(ddlAddCountry.SelectedValue), GetSessionUserID());
            }
            if (res_id > 0)
            {
                Bind_LOTestingLabs();
                string hidemodal = String.Format("hideModal('divadd')");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hidemodal", hidemodal, true);
            }
            else if (res_id == -1)
            {
                string showmodal = String.Format("alert('Lab Name already exist.');showModal('divadd',false)");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showmodal", showmodal, true);
            }

        }
    }
    protected bool checkEmail(string Value_)
    {
        Regex reg = new Regex(@"^([\w-]+(?:\.[\w-]+)*)@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z][A-Z]{2})?)$", RegexOptions.IgnoreCase);

        if (reg.IsMatch(Value_))
            return true;
        else
            return false;
    }
    protected void onUpdate(object source, CommandEventArgs e)
    {
        HiddenFlag.Value = "Edit";
        OperationMode = "Edit Bunker Testing Lab";

        DataTable dt = new DataTable();
        Selected_ID.Value = e.CommandArgument.ToString();
        dt = objBLL.Get_BunkerTestingLabByID(UDFLib.ConvertToInteger(e.CommandArgument.ToString()), GetSessionUserID());
        if (dt.Rows.Count > 0)
        {
            txtLabName.Text = dt.Rows[0]["COMPANY_NAME"].ToString();
            txtAddress.Text = dt.Rows[0]["ADDRESS"].ToString();
            txtEmail.Text = dt.Rows[0]["EMAIL1"].ToString();
            txtPhone.Text = dt.Rows[0]["PHONE1"].ToString();
            if (dt.Rows[0]["Country"].ToString() != null && dt.Rows[0]["Country"].ToString() != "")
            {
                ddlAddCountry.SelectedValue = dt.Rows[0]["Country"].ToString();
            }
            string js = String.Format("showModal('divadd',false);");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Deptmodal", js, true);
        }
    }

    protected void onDelete(object source, CommandEventArgs e)
    {
        int retval = objBLL.DeleteCompany_DL(Convert.ToInt32(e.CommandArgument.ToString()), GetSessionUserID());
        Bind_LOTestingLabs();
    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        Bind_LOTestingLabs();
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        txtfilter.Text = "";
        ddlSearchCountry.SelectedValue = "0";

        Bind_LOTestingLabs();

    }


    protected void ImgExpExcel_Click(object sender, EventArgs e)
    {

        int rowcount = ucCustomPagerItems.isCountRecord;

        int countrycode = 0;
        if (ddlSearchCountry.SelectedValue != "0") countrycode = Convert.ToInt32(ddlSearchCountry.SelectedValue.ToString());

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());


        DataTable dt = objBLL.Get_BunkerTestingLabList(txtfilter.Text, countrycode, GetSessionUserID(), sortbycoloumn, sortdirection, ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);

        string[] HeaderCaptions = { "ID", "Lab Name", "Country", "Address", "EMail", "Phone" };
        string[] DataColumnsName = { "ID", "Company_Name", "Country", "Address", "EMail1", "Phone1" };

        GridViewExportUtil.ShowExcel(dt, HeaderCaptions, DataColumnsName, "Bunker Testing Labs", "Bunker Testing Labs", "");

    }


    protected void GridView_Labs_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.Header)
        {
            if (ViewState["SORTBYCOLOUMN"] != null)
            {
                Image img = (Image)e.Row.FindControl("img" + ViewState["SORTBYCOLOUMN"].ToString());
                if (img != null)
                {
                    if (ViewState["SORTDIRECTION"] == null || ViewState["SORTDIRECTION"].ToString() == "0")
                        img.ImageUrl = "~/Images/arrowUp.png";
                    else
                        img.ImageUrl = "~/Images/arrowDown.png";

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


    protected void GridView_Labs_Sorting(object sender, GridViewSortEventArgs se)
    {
        ViewState["SORTBYCOLOUMN"] = se.SortExpression;

        if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
            ViewState["SORTDIRECTION"] = 1;
        else
            ViewState["SORTDIRECTION"] = 0;

        Bind_LOTestingLabs();
    }
}
