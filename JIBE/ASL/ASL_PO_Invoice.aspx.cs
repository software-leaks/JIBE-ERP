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

public partial class ASL_ASL_PO_Invoice : System.Web.UI.Page
{
    UserAccess objUA = new UserAccess();
    public Boolean uaEditFlag = true;
    public Boolean uaDeleteFlage = true;
    protected void Page_Load(object sender, EventArgs e)
    {
        UserAccessValidation();
        if (!IsPostBack)
        {
            Load_VesselList();
            BindGrid();
        }
    }
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
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
    private int GetCompanyID()
    {
        if (Session["USERCOMPANYID"] != null)
            return int.Parse(Session["USERCOMPANYID"].ToString());
        else
            return 0;
    }
    public void Load_VesselList()
    {
        BLL_Infra_VesselLib objBLL = new BLL_Infra_VesselLib();
        DataTable dt = objBLL.Get_VesselList(0, 0, 0, "", Convert.ToInt32(GetCompanyID()));

        ddlVessel.DataSource = dt;
        ddlVessel.DataTextField = "VESSEL_NAME";
        ddlVessel.DataValueField = "VESSEL_ID";
        ddlVessel.DataBind();
        ddlVessel.Items.Insert(0, new ListItem("-All Vessels-", "0"));

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
    protected void BindGrid()
    {
        lblSuppliername.Text = GetSessionSupplierName();
        lblSupplierCode.Text = GetSuppID();
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
        DataSet ds = BLL_ASL_Supplier.Get_Supplier_POInvoiceHistory(SupplierID, UDFLib.ConvertIntegerToNull(ddlVessel.SelectedValue), UDFLib.ConvertStringToNull(ddlVessel.SelectedItem), ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);
        if (ucCustomPagerItems.isCountRecord == 1)
        {
            //ucCustomPagerItems.CountTotalRec = ds.Tables[1].Rows[0]["RowCount"].ToString();
            ucCustomPagerItems.CountTotalRec = rowcount.ToString();
            ucCustomPagerItems.BuildPager();
        }

        if (ds.Tables[0].Rows.Count > 0)
        {
            gvPOInvoice.DataSource = ds;
            gvPOInvoice.DataBind();
        }
        else
        {
            gvPOInvoice.DataSource = ds;
            gvPOInvoice.DataBind();
        }
    }

    protected void btnfilter_Click(object sender, EventArgs e)
    {
        SearchBindGrid();
    }
}