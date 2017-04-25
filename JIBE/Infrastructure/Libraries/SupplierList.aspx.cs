using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using SMS.Business.PURC;
using SMS.Business.Infrastructure;
using System.Data;
using System.Web.UI.HtmlControls;
using SMS.Properties;

public partial class SupplierList : System.Web.UI.Page
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
            BindCurrencyDLL();
        }

    }

    protected void BindCurrencyDLL()
    {
        BLL_Infra_Currency objBLLCurrency = new BLL_Infra_Currency();
        DataTable dt = objBLLCurrency.Get_CurrencyList();

        ddlCurrency.DataSource = dt;
        ddlCurrency.DataTextField = "Currency_Code";
        ddlCurrency.DataValueField = "Currency_Code";
        ddlCurrency.DataBind();
        ddlCurrency.Items.Insert(0, new ListItem("-SELECT-", "0"));       

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

        BLL_Infra_SupplierList objSupp = new BLL_Infra_SupplierList();

        int rowcount = ucCustomPagerItems.isCountRecord;

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());


        DataTable dt = objSupp.Get_Suppliers_List_Search(txtfilter.Text !="" ? txtfilter.Text : null , sortbycoloumn, sortdirection, ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);


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
        BLL_Infra_SupplierList objSupp = new BLL_Infra_SupplierList();
        HiddenFlag.Value = "Edit";

        OperationMode = "Edit Supplier";

        DataTable dtSuppDtl = objSupp.Get_Suppliers_Details(Convert.ToInt32(e.CommandArgument.ToString()));

        ViewState["SUPPLIER_ID"] = dtSuppDtl.Rows[0]["SUPPLIER_ID"].ToString();

        txtAddress_AV.Text = dtSuppDtl.Rows[0]["ADDRESS_1"].ToString();
        txtAddress2_AV.Text = dtSuppDtl.Rows[0]["ADDRESS_2"].ToString();

        txtCreationDate_AV.Text = dtSuppDtl.Rows[0]["Created_Date"].ToString();

        txtEmail_AV.Text = dtSuppDtl.Rows[0]["Email1"].ToString();
        txtEmail2_AV.Text = dtSuppDtl.Rows[0]["Email2"].ToString();

        txtFax_AV.Text = dtSuppDtl.Rows[0]["Fax1"].ToString();
        txtFax2_AV.Text = dtSuppDtl.Rows[0]["Fax2"].ToString();

        txtPhone_AV.Text = dtSuppDtl.Rows[0]["PHONE1"].ToString();
        txtPhone2_AV.Text = dtSuppDtl.Rows[0]["PHONE2"].ToString();

        txtMakerCode_AV.Text = dtSuppDtl.Rows[0]["Supplier_Code"].ToString();
        txtMakerName_AV.Text = dtSuppDtl.Rows[0]["Full_NAME"].ToString();
        txtShortName.Text = Convert.ToString(dtSuppDtl.Rows[0]["Supplier_Name"]);

        txtTelex_AV.Text = dtSuppDtl.Rows[0]["TELEX1"].ToString();
        txtTelex2_AV.Text = dtSuppDtl.Rows[0]["TELEX2"].ToString();

        ddlCountry_AV.ClearSelection();
        ListItem list = ddlCountry_AV.Items.FindByValue(dtSuppDtl.Rows[0]["Country"].ToString());
        if (list != null)
            list.Selected = true;
        txtCreationDate_AV.Enabled = false;

        ddlSupplierType.SelectedValue = dtSuppDtl.Rows[0]["supplier_Type"].ToString();
        //if(dtSuppDtl.Rows[0]["Supplier_Currency"].ToString()!="")
        //    ddlCurrency.SelectedValue = dtSuppDtl.Rows[0]["Supplier_Currency"].ToString();
        if (ddlCurrency.Items.FindByValue(dtSuppDtl.Rows[0]["Supplier_Currency"].ToString()) != null)
            ddlCurrency.SelectedValue = dtSuppDtl.Rows[0]["Supplier_Currency"].ToString() != "" ? dtSuppDtl.Rows[0]["Supplier_Currency"].ToString() : "0";
        else
            ddlCurrency.SelectedValue = "0";
        string[] scoplist = dtSuppDtl.Rows[0]["supplier_scope"].ToString().Split(',') ;
        cblSupplier.ClearSelection();
        for (int i = 0; i < scoplist.Length - 1;i++ )
        {
            ListItem list1 = cblSupplier.Items.FindByValue(scoplist[i].Replace("'",""));
            if (list1 != null)
                list1.Selected = true;
        }

        


        string AddMaker = String.Format("showModal('divadd',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddMaker", AddMaker, true);
    }

    protected void lbtnDelete_Click(object s, CommandEventArgs e)
    {

        BLL_Infra_SupplierList objSupp = new BLL_Infra_SupplierList();
        objSupp.Del_Supplier_Details(Convert.ToInt32(e.CommandArgument.ToString()), Session["USERID"].ToString());

        BindMakerGrid();
    }

    protected void btnsave_Click(object s, EventArgs e)
    {

        BLL_Infra_SupplierList objSupp = new BLL_Infra_SupplierList();
        if (ValidateScope())
        {

            if (HiddenFlag.Value == "Add")
            {
                objSupp.Ins_Supplier_Details(Session["USERID"].ToString(), txtMakerName_AV.Text, txtAddress_AV.Text, txtAddress2_AV.Text, ddlCountry_AV.SelectedValue, txtEmail_AV.Text, txtEmail2_AV.Text
                    , txtPhone_AV.Text, txtPhone2_AV.Text, txtFax_AV.Text, txtFax2_AV.Text, txtTelex_AV.Text, txtTelex2_AV.Text, "", txtMakerCode_AV.Text, ddlSupplierType.SelectedValue, SupplierScope(), ddlCurrency.SelectedItem.Text,txtShortName.Text.Trim());
            }
            else
            {
                objSupp.Upd_Suppliers_Details(Convert.ToInt32(ViewState["SUPPLIER_ID"].ToString()), Session["USERID"].ToString(), txtMakerName_AV.Text, txtAddress_AV.Text, txtAddress2_AV.Text, ddlCountry_AV.SelectedValue
                    , txtEmail_AV.Text, txtEmail2_AV.Text, txtPhone_AV.Text, txtPhone2_AV.Text, txtFax_AV.Text, txtFax2_AV.Text, txtTelex_AV.Text, txtTelex2_AV.Text, "", txtMakerCode_AV.Text, ddlSupplierType.SelectedValue, SupplierScope(), ddlCurrency.SelectedItem.Text,txtShortName.Text.Trim());
            }
            
            string hideMaker = String.Format("hideModal('divadd')");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hideMaker", hideMaker, true);
            ClearControls();
            BindMakerGrid();
        }
        else
        {
            string hideMaker = String.Format("hideModal('divadd')");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hideMaker", hideMaker, true);
        }


       
    }

    private string SupplierScope()
    {
        string supplierScope = "";
        foreach (ListItem ls in cblSupplier.Items)
        {
            if (ls.Selected == true)
                supplierScope = supplierScope + "'" + ls.Value + "',";
        }
        return supplierScope;
    }

    private bool ValidateScope()
    {
        int i = 0;
        foreach (ListItem ls in cblSupplier.Items)
        {
            if (ls.Selected == true)
                i++;
        }
        if (i == 0)
            return false;
        else
            return true;
    }



    protected void lnkAddNew_Click(object s, EventArgs e)
    {
        HiddenFlag.Value = "Add";
        OperationMode = "Add Supplier";

        this.SetFocus("ctl00_MainContent_txtMakerName_AV");

        txtAddress_AV.Text = "";
        txtAddress2_AV.Text = "";

        txtCreationDate_AV.Text = "";
        
        txtEmail_AV.Text = "";
        txtEmail2_AV.Text = "";

        txtFax_AV.Text = "";
        txtFax2_AV.Text = "";

        txtMakerCode_AV.Text = "";
        txtMakerName_AV.Text = "";

        txtShortName.Text = string.Empty;

        txtPhone_AV.Text = "";
        txtPhone2_AV.Text = "";

        txtTelex_AV.Text = "";
        txtTelex2_AV.Text = "";


        //BLL_Infra_Supplier objSupp = new BLL_Infra_Supplier();
        //txtMakerCode_AV.Text = objSupp.Get_Next_MakerCode();



        string AddMaker = String.Format("showModal('divadd',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddMaker", AddMaker, true);

    }


    protected void ImgExpExcel_Click(object sender, EventArgs e)
    {

        //BLL_Infra_Supplier objSupp = new BLL_Infra_Supplier();
        BLL_Infra_SupplierList objSupp = new BLL_Infra_SupplierList();

        int rowcount = ucCustomPagerItems.isCountRecord;

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());


        //DataTable dt = objSupp.Get_Suppliers_List_Search(txtfilter.Text != "" ? txtfilter.Text : null, sortbycoloumn, sortdirection, ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);
        DataTable dt = objSupp.Get_Suppliers_List_Search(txtfilter.Text != "" ? txtfilter.Text : null, sortbycoloumn, sortdirection, null, null, ref  rowcount);

        string[] HeaderCaptions = { "Code", "Name","Currency", "Country", "Email","Address", "Phone", "Fax","Telex"};
        string[] DataColumnsName = { "Supplier_Code", "Supplier_Name", "Supplier_Currency", "Country", "Email", "Address_1", "Phone", "fax", "TELEX" };

        GridViewExportUtil.ShowExcel(dt, HeaderCaptions, DataColumnsName, "Supplier", "Supplier", "");

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


            if (DataBinder.Eval(e.Row.DataItem, "ADDRESS_1").ToString().Length > 20)
            {
                lblAddress.Text = lblAddress.Text + "..";
                lblAddress.Attributes.Add("Title", "cssbody=[dvbdy1] cssheader=[dvhdr1] header=[Address] body=[" + DataBinder.Eval(e.Row.DataItem, "ADDRESS_1").ToString() + "]");
            }

            if (DataBinder.Eval(e.Row.DataItem, "COUNTRY").ToString().Length > 20)
            {
                lblCountry.Text = lblCountry.Text + "..";
                lblCountry.Attributes.Add("Title", "cssbody=[dvbdy1] cssheader=[dvhdr1] header=[Full Name] body=[" + DataBinder.Eval(e.Row.DataItem, "COUNTRY").ToString() + "]");
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

    private void ClearControls()
    {
        cblSupplier.ClearSelection();
        ddlCountry_AV.SelectedIndex = 0;
        ddlCurrency.SelectedIndex = 0;
        ddlSupplierType.SelectedIndex = 0;

    }

}