using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Infrastructure;
using System.Data;

public partial class Supplier_Maker_Add : System.Web.UI.Page
{
    public string OperationMode = "";
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["SUPPLIER_ID"]))
            {
                HiddenFlag.Value = "Edit";
                OperationMode = "Edit";
            }
            else
            {
                HiddenFlag.Value = "Add";
                OperationMode = "ADD";
            }

            BindCountryDLL();
            NextMakerCode();
        }

    }



    public void BindCountryDLL()
    {

        BLL_Infra_Country objCN = new BLL_Infra_Country();

        DataTable dt = objCN.Get_CountryList();

        ddlCountry_AV.DataTextField = "COUNTRY";
        ddlCountry_AV.DataValueField = "COUNTRY";
        ddlCountry_AV.DataSource = dt;
        ddlCountry_AV.DataBind();
        ddlCountry_AV.Items.Insert(0, new ListItem("-SELECT-", "0"));
    
    
    }




    public void NextMakerCode()
    {
        BLL_Infra_Supplier objSupp = new BLL_Infra_Supplier();
        txtMakerCode_AV.Text = objSupp.Get_Next_MakerCode();
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
            objSupp.Upd_Suppliers_Details(Convert.ToInt32(ViewState["SUPPLIER_ID"].ToString()), Session["USERFULLNAME"].ToString(), txtMakerName_AV.Text, txtAddress_AV.Text, ddlCountry_AV.SelectedValue, txtEmail_AV.Text, txtPhone_AV.Text, txtFax_AV.Text, "");
        }

        String script = String.Format("parent.RefreshMakerFromChild();parent.hideModal('dvIframe');");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", script, true);
    }

}