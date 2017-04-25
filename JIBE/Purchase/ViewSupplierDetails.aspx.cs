using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using   SMS.Business.PURC ;
//using ClsBLLTechnical;

public partial class Technical_INV_ViewSupplierDetails : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {


            ShowSupplierDetails();
        }
    }

    private void ShowSupplierDetails()
    {
        try
        {
            DataTable dtSupplierData = new DataTable();
            using( BLL_PURC_Purchase  objTechService = new  BLL_PURC_Purchase ())
            {
                dtSupplierData = objTechService.ViewSupplierDetails(Request.QueryString["SupplierCode"].ToString());
                 

                txtSupplierCode.Text = Convert.ToString(dtSupplierData.Rows[0]["Supplier"]);
                txtSupplierName.Text = Convert.ToString(dtSupplierData.Rows[0]["Short_Name"]);
                txtSupplierType.Text = Convert.ToString(dtSupplierData.Rows[0]["Supplier_Type"]);
                txtFullName.Text = Convert.ToString(dtSupplierData.Rows[0]["Full_Name"]);
                txtSupplierCategory.Text = Convert.ToString(dtSupplierData.Rows[0]["Supplier_Category"]);
                txtCompanyType.Text = Convert.ToString(dtSupplierData.Rows[0]["COMPANY_TYPE"]);
                txtCompanyStatus.Text = Convert.ToString(dtSupplierData.Rows[0]["COMPANY_STATUS"]);
                txtCompanyCurrency.Text = Convert.ToString(dtSupplierData.Rows[0]["CURRENCY"]);
                
                txtAddress1.Text = Convert.ToString(dtSupplierData.Rows[0]["Address_1"]);
                txtAddress2.Text = Convert.ToString(dtSupplierData.Rows[0]["Address_2"]);
                txtAddress3.Text = Convert.ToString(dtSupplierData.Rows[0]["Address_3"]);
                txtAddress4.Text = Convert.ToString(dtSupplierData.Rows[0]["Address_4"]);
                txtAddress5.Text = Convert.ToString(dtSupplierData.Rows[0]["Address_5"]);

                txtCity.Text = Convert.ToString(dtSupplierData.Rows[0]["City"]);
                txtCountry.Text = Convert.ToString(dtSupplierData.Rows[0]["Country"]);
                txtPostal.Text = Convert.ToString(dtSupplierData.Rows[0]["Postal_Code"]);
                txtPhone.Text = Convert.ToString(dtSupplierData.Rows[0]["MakerPhone"]);
                txtExtension.Text = Convert.ToString(dtSupplierData.Rows[0]["MakerTELEX"]);
                txtEmail.Text = Convert.ToString(dtSupplierData.Rows[0]["MakerEmail"]);
                txtFax.Text = Convert.ToString(dtSupplierData.Rows[0]["MakerFax"]);

                txtBankName.Text = Convert.ToString(dtSupplierData.Rows[0]["BANK_NAME"]);
                txtBranch.Text = Convert.ToString(dtSupplierData.Rows[0]["BANK_BRANCH"]);
                txtBankAccNumber.Text = Convert.ToString(dtSupplierData.Rows[0]["BANK_ACCOUNT"]);
                txtBankAddress.Text = Convert.ToString(dtSupplierData.Rows[0]["BANK_ADDRESS"]);
                txtBankCity.Text = Convert.ToString(dtSupplierData.Rows[0]["BANK_CITY"]);
                txtBankCountry.Text = Convert.ToString(dtSupplierData.Rows[0]["BANK_COUNTRY"]); 
            }
        }
        catch (Exception ex)
        {
            //.WriteError(this.GetType().Name.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), ex);
        }

    }
}
