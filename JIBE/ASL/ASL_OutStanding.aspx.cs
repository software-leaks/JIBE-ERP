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

public partial class ASL_ASL_OutStanding : System.Web.UI.Page
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
            BindPOOutstandingGrid();
            BindOutstandingGrid();
            //BindGrid();
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
    protected void BindPOOutstandingGrid()
    {
        int rowcount = ucCustomPagerItems.isCountRecord;
        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());
        string SupplierID = GetSuppID();
        DataSet ds = BLL_ASL_Supplier.Get_Supplier_PO_OutStanding(SupplierID, ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);
        if (ucCustomPagerItems.isCountRecord == 1)
        {
            ucCustomPagerItems.CountTotalRec = ds.Tables[1].Rows[0]["RowCount"].ToString();
            //ucCustomPagerItems.CountTotalRec = rowcount.ToString();
            ucCustomPagerItems.BuildPager();
        }

        if (ds.Tables[0].Rows.Count > 0)
        {
            gvPOOutstanding.DataSource = ds.Tables[0];
            gvPOOutstanding.DataBind();
        }
        else
        {
            gvPOOutstanding.DataSource = ds.Tables[0];
            gvPOOutstanding.DataBind();
        }
        if (gvPOOutstanding.PageIndex == 0)
        {
            var lastRow = gvPOOutstanding.Rows[gvPOOutstanding.Rows.Count - 1];
            lastRow.FindControl("ImgCreditView").Visible = false;
            
        }
    }

   

    protected void BindOutstandingGrid()
    {
        int rowcount = ucCustomPager1.isCountRecord;
        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());
        string SupplierID = GetSuppID();
        DataSet ds = BLL_ASL_Supplier.Get_Supplier_PO_OutStanding(SupplierID, ucCustomPagerItems.CurrentPageIndex, ucCustomPager1.PageSize, ref  rowcount);
        if (ucCustomPager1.isCountRecord == 1)
        {
            ucCustomPager1.CountTotalRec = ds.Tables[3].Rows[0]["RowCount"].ToString();
            //ucCustomPager1.CountTotalRec = rowcount.ToString();
            ucCustomPager1.BuildPager();
        }

        if (ds.Tables[0].Rows.Count > 0)
        {
            gvOutstanding.DataSource = ds.Tables[2];
            gvOutstanding.DataBind();
        }
        else
        {
            gvOutstanding.DataSource = ds.Tables[2];
            gvOutstanding.DataBind();
        }
        
    }

   
}