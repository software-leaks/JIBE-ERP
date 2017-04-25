using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.PURC;
using SMS.Business.Infrastructure;
using System.Data;
using System.Web.UI.HtmlControls;
using SMS.Properties;

public partial class Infrastructure_Libraries_Supplier_Makers : System.Web.UI.Page
{

  
    public string OperationMode = "";
    public Boolean uaEditFlag = false;
    public Boolean uaDeleteFlage = false;

    UserAccess objUA = new UserAccess();

    protected void Page_Load(object sender, EventArgs e)
    {

        UserAccessValidation();
        if (!IsPostBack)
        {
            BLL_Infra_Country objCN = new BLL_Infra_Country();

            DataTable dt = objCN.Get_CountryList();
        
            ddlCountry_AV.DataTextField = "COUNTRY";
            ddlCountry_AV.DataValueField = "COUNTRY";
            ddlCountry_AV.DataSource = dt;
            ddlCountry_AV.DataBind();
            ddlCountry_AV.Items.Insert(0, new ListItem("-SELECT-", "0"));


            BindMakerGrid();
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

    public void BindMakerGrid()
    {

        BLL_Infra_Supplier objSupp = new BLL_Infra_Supplier();

        int rowcount = ucCustomPagerItems.isCountRecord;

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());


        DataTable dt = objSupp.Get_Suppliers_List_Search(txtfilter.Text !="" ? txtfilter.Text : null , sortbycoloumn, sortdirection, null, null, ref  rowcount);


        if (ucCustomPagerItems.isCountRecord == 1)
        {
            ucCustomPagerItems.CountTotalRec = rowcount.ToString();
            ucCustomPagerItems.BuildPager();
        }

        if (dt.Rows.Count > 0)
        {
            gvSuppliers.DataSource = dt;
            gvSuppliers.DataBind();
        }
        else
        {
            gvSuppliers.DataSource = dt;
            gvSuppliers.DataBind();
        }
    }

    protected void btnFilter_Click(object s, EventArgs e)
    {

        BindMakerGrid();

    }
   
    protected void btnRefresh_Click(object s, EventArgs e)
    {
        txtfilter.Text = "";
        BindMakerGrid();
    }

    protected void onUpdate(object s, CommandEventArgs e)
    {
        BLL_Infra_Supplier objSupp = new BLL_Infra_Supplier();
        HiddenFlag.Value = "Edit";

        OperationMode = "Edit Maker";

        DataTable dtSuppDtl = objSupp.Get_Suppliers_Details(Convert.ToInt32(e.CommandArgument.ToString()));

        ViewState["SUPPLIER_ID"] = dtSuppDtl.Rows[0]["SUPPLIER_ID"].ToString();
        txtAddress_AV.Text = dtSuppDtl.Rows[0]["Address_1"].ToString();
        txtCreationDate_AV.Text = dtSuppDtl.Rows[0]["Created_Date"].ToString();
        txtEmail_AV.Text = dtSuppDtl.Rows[0]["Email1"].ToString();
        txtFax_AV.Text = dtSuppDtl.Rows[0]["Fax1"].ToString();
        txtMakerCode_AV.Text = dtSuppDtl.Rows[0]["Supplier_Code"].ToString();
        txtMakerName_AV.Text = dtSuppDtl.Rows[0]["Supplier_Name"].ToString();
        txtPhone_AV.Text = dtSuppDtl.Rows[0]["Phone1"].ToString();
        ddlCountry_AV.ClearSelection();
        ListItem list = ddlCountry_AV.Items.FindByValue(dtSuppDtl.Rows[0]["Country"].ToString());
        if (list != null)
            list.Selected = true;

        txtCreationDate_AV.Enabled = false;

        string AddMaker = String.Format("showModal('divadd',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddMaker", AddMaker, true);
    }

    protected void lbtnDelete_Click(object s, CommandEventArgs e)
    {

        //BLL_Infra_Supplier objSupp = new BLL_Infra_Supplier();
        //objSupp.DELETE_Supplier(Convert.ToInt32(e.CommandArgument.ToString()));

        BindMakerGrid();
    }

    protected void btnsave_Click(object s, EventArgs e)
    {

        BLL_Infra_Supplier objSupp = new BLL_Infra_Supplier();

        if (HiddenFlag.Value == "Add")
        {
            objSupp.Ins_Supplier_Details(Session["USERID"].ToString(), txtMakerName_AV.Text, txtAddress_AV.Text, ddlCountry_AV.SelectedValue, txtEmail_AV.Text, txtPhone_AV.Text, txtFax_AV.Text, "", txtMakerCode_AV.Text);
        }
        else 
        {
            objSupp.Upd_Suppliers_Details(Convert.ToInt32(ViewState["SUPPLIER_ID"].ToString()), Session["USERID"].ToString(), txtMakerName_AV.Text, txtAddress_AV.Text, ddlCountry_AV.SelectedValue, txtEmail_AV.Text, txtPhone_AV.Text, txtFax_AV.Text, "");
        }


        string hideMaker = String.Format("hideModal('divadd')");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hideMaker", hideMaker, true);

        BindMakerGrid();
    }

    protected void lnkAddNew_Click(object s, EventArgs e)
    {
        HiddenFlag.Value = "Add";
        OperationMode = "Add Maker";

        this.SetFocus("ctl00_MainContent_txtMakerName_AV");

        txtAddress_AV.Text = "";
        txtCreationDate_AV.Text = "";
        txtEmail_AV.Text = "";
        txtFax_AV.Text = "";
        txtMakerCode_AV.Text = "";
        txtMakerName_AV.Text = "";
        txtPhone_AV.Text = "";

        BLL_Infra_Supplier objSupp = new BLL_Infra_Supplier();
        txtMakerCode_AV.Text = objSupp.Get_Next_MakerCode();



        string AddMaker = String.Format("showModal('divadd',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddMaker", AddMaker, true);

    }


    protected void ImgExpExcel_Click(object sender, EventArgs e)
    {

        BLL_Infra_Supplier objSupp = new BLL_Infra_Supplier();

        int rowcount = ucCustomPagerItems.isCountRecord;

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());


        DataTable dt = objSupp.Get_Suppliers_List_Search(txtfilter.Text != "" ? txtfilter.Text : null, sortbycoloumn, sortdirection, null, null, ref  rowcount);

        string[] HeaderCaptions = { "Code", "Name", "Country", "Email","Address", "Phone", "Fax"};
        string[] DataColumnsName = { "Supplier_Code", "Supplier_Name", "Country", "Email", "Address", "Phone", "fax" };

        GridViewExportUtil.ShowExcel(dt, HeaderCaptions, DataColumnsName, "Maker", "Maker", "");

    }

  
    protected void gvSuppliers_RowDataBound(object sender, GridViewRowEventArgs e)
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
            
            
            Label lblAddress = (Label)e.Row.FindControl("lblAddress");
            Label lblCountry = (Label)e.Row.FindControl("lblCountry");


            if (DataBinder.Eval(e.Row.DataItem, "FullAddress").ToString().Length > 20)
            {
                lblAddress.Text = lblAddress.Text + "..";
                lblAddress.Attributes.Add("Title", "cssbody=[dvbdy1] cssheader=[dvhdr1] header=[Address] body=[" + DataBinder.Eval(e.Row.DataItem, "FullAddress").ToString() + "]");
            }

            if (DataBinder.Eval(e.Row.DataItem, "FullCountryName").ToString().Length > 20)
            {
                lblCountry.Text = lblCountry.Text + "..";
                lblCountry.Attributes.Add("Title", "cssbody=[dvbdy1] cssheader=[dvhdr1] header=[Full Name] body=[" + DataBinder.Eval(e.Row.DataItem, "FullCountryName").ToString() + "]");
            }
         
        }

    }

    protected void gvSuppliers_Sorting(object sender, GridViewSortEventArgs se)
    {

        ViewState["SORTBYCOLOUMN"] = se.SortExpression;

        if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
            ViewState["SORTDIRECTION"] = 1;
        else
            ViewState["SORTDIRECTION"] = 0;

        BindMakerGrid();

    }

}