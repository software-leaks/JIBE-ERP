using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.Infrastructure;
using SMS.Business.ASL;
using System.IO;
using SMS.Properties;
using System.Configuration;

public partial class ASL_ASL_Invoice_WIP : System.Web.UI.Page
{
    UserAccess objUA = new UserAccess();
    public Boolean uaEditFlag = true;
    public Boolean uaDeleteFlage = true;
    protected void Page_Load(object sender, EventArgs e)
    {
        UserAccessValidation();
        if (!IsPostBack)
        {
            lblSuppliername.Text = GetSessionSupplierName();
            lblSupplierCode.Text = GetSuppID();
            SearchBindGrid();
        }
    }
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }
    private string GetSessionSupplierName()
    {
        try
        {
            if (Request.QueryString["Supplier_Name"] != null)
            {
                return Request.QueryString["Supplier_Name"].ToString();
            }

            else
                return "0";
        }
        catch { return "0"; }
    }
    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
        {
            pnl.Visible = false;
            lblMsg.Text = "You don't have sufficient previlege to access the requested information.";
        }
        else
        {
            pnl.Visible = true;
        }
      
        if (objUA.Add == 0)
        {
            //ImgAdd.Visible = false;
        }
        if (objUA.Edit == 1)
            uaEditFlag = true;
        //else
        //    btnsave.Visible = false;

        if (objUA.Delete == 1) uaDeleteFlage = true;

    }
    protected void BindGrid()
    {
        
            SearchBindGrid();
      

    }
    public string GetSuppID()
    {
        try
        {
            if (Request.QueryString["Supp_ID"] != null)
            {
                return Request.QueryString["Supp_ID"].ToString();
            }

            else
                return "0";
        }
        catch { return "0"; }
    }
    protected void SearchBindGrid()
    {
        int rowcount = ucCustomPagerItems.isCountRecord;
        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());
        string SupplierID = GetSuppID();
        //int Type = 0;
        DataSet ds = BLL_ASL_Supplier.Get_Supplier_POInvoiceWIP(SupplierID, UDFLib.ConvertIntegerToNull(GetSessionUserID()), ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);
        if (ucCustomPagerItems.isCountRecord == 1)
        {
            //ucCustomPagerItems.CountTotalRec = ds.Tables[1].Rows[0]["RowCount"].ToString();
            ucCustomPagerItems.CountTotalRec = rowcount.ToString();
            ucCustomPagerItems.BuildPager();
        }

        if (ds.Tables[0].Rows.Count > 0)
        {
            gvPOInvoiceWIP.DataSource = ds;
            gvPOInvoiceWIP.DataBind();
        }
        else
        {
            gvPOInvoiceWIP.DataSource = ds;
            gvPOInvoiceWIP.DataBind();
        }
        if (ds.Tables[1].Rows.Count > 0)
        {
            txtPasskey.Text = ds.Tables[1].Rows[0]["Passkey"].ToString();
            //txtPasskey.Text = "fa574eba-215a-4f3f-be75-9b2b44058b8b";
        }
    }
    protected void btnInvoice_Click(object sender, CommandEventArgs e)
    {

        string[] arg = e.CommandArgument.ToString().Split(',');
       string Supplier_Code = arg[0];
       string SUPPLY_ID = arg[1];
       string Invoice_ID = arg[2];
       //string SUPPLY_ID = "1104461";
       //string Invoice_ID = "28226";http://192.168.0.100/OCA
       string passkey = txtPasskey.Text.ToString();

       string OCA_URL = null;
       if (!Request.Url.AbsoluteUri.Contains(ConfigurationManager.AppSettings["OCA_APP_URL"]))
       {
           OCA_URL = ConfigurationManager.AppSettings["OCA_APP_URL"];
       }
       string OCA_URL1 = OCA_URL + "/PO_LOG/Compare_PO_Invoice.asp?Invoice_ID=" + Invoice_ID + "&Supply_ID=" + SUPPLY_ID + "&pk=" + passkey + "";

       ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "javascript:window.open('" + OCA_URL1 + "');", true); 

       //ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "javascript:window.open('http://seachange.jibe.com.sg/demoasp/PO_LOG/Compare_PO_Invoice.asp?Invoice_ID=" + Invoice_ID + "&Supply_ID=" + SUPPLY_ID + "');", true);
       //ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "javascript:window.open('http://seachange.jibe.com.sg/demoasp/PO_LOG/Compare_PO_Invoice.asp?Invoice_ID=" + Invoice_ID + "&Supply_ID=" + SUPPLY_ID + "&pk=" + passkey + "');", true);
       //ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "javascript:window.open('http://192.168.0.100/OCA/PO_LOG/Compare_PO_Invoice.asp?Invoice_ID=" + Invoice_ID + "&Supply_ID=" + SUPPLY_ID + "&pk=" + passkey + "');", true);
       //ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "javascript:window.open('http://192.168.0.100/OCA/PO_LOG/Compare_PO_Invoice.asp?Invoice_ID=" + Invoice_ID + "&Supply_ID=" + SUPPLY_ID + "');", true);
       //Response.Write("http://seachange.jibe.com.sg/Demoasp/PO_LOG/Compare_PO_Invoice.asp?Invoice_ID=" + Invoice_ID + "&Supply_ID=" + SUPPLY_ID + "&pk=" + passkey + "");
       //Response.Write("http://192.168.0.100/OCA/PO_LOG/Compare_PO_Invoice.asp?Invoice_ID=" + Invoice_ID + "&Supply_ID=" + SUPPLY_ID + "&pk=" + passkey + "");
    }
}