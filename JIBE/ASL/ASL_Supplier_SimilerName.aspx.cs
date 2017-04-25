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

public partial class ASL_ASL_Supplier_SimilerName : System.Web.UI.Page
{
    UserAccess objUA = new UserAccess();

    public Boolean uaEditFlag = false;
    public Boolean uaDeleteFlage = false;
    protected void Page_Load(object sender, EventArgs e)
    {
        UserAccessValidation();
        if (!IsPostBack)
        {
            BindGrid();
        }
    }
    protected void btnfilter_Click(object sender, EventArgs e)
    {
        SearchBindGrid();
    }
    protected void BindGrid()
    {
       // int? SuppID = UDFLib.ConvertIntegerToNull(Request.QueryString["Supp_ID"]);
       //string SupplierID = GetSuppID();
       //DataSet ds = BLL_ASL_Supplier.Get_Supplier_General_Data_List(UDFLib.ConvertStringToNull(SupplierID));
       // if (ds.Tables[0].Rows.Count > 0)
       // {
       //     DataRow dr = ds.Tables[0].Rows[0];
        //}
            txtSearch.Text = GetSessionSupplierName();
            //lblSuppliername.Text = GetSessionSupplierName();
            SearchBindGrid();
        

    }
    private string GetSessionSupplierName()
    {
        if (Session["Supplier_Name"] != null)
            return Session["Supplier_Name"].ToString();
        else
            return null;
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
            pnlSimilerName.Visible = false;
            lblMsg.Text = "You don't have sufficient previlege to access the requested information.";
        }
        else
        {
            pnlSimilerName.Visible = true;
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
        //lblSuppliername.Text = txtSearch.Text;
        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());
        string SupplierID = GetSuppID();
        DataTable dttext = new DataTable();
        dttext = SearchString();

        DataTable dt = BLL_ASL_Supplier.Get_Supplier_Search_SimilerName(txtSearch.Text != "" ? txtSearch.Text : null, dttext, SupplierID
            , ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);
        //DataTable dt = BLL_ASL_Supplier.Get_Supplier_Search_SimilerName(txtSearch.Text != "" ? txtSearch.Text : null
        //    , ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);
        if (ucCustomPagerItems.isCountRecord == 1)
        {
            ucCustomPagerItems.CountTotalRec = rowcount.ToString();
            ucCustomPagerItems.BuildPager();
        }

        if (dt.Rows.Count > 0)
        {
            gvSupplierName.DataSource = dt;
            gvSupplierName.DataBind();
        }
        else
        {
            gvSupplierName.DataSource = dt;
            gvSupplierName.DataBind();
        }
    }
    protected DataTable SearchString()
    {
        char[] delimiterChars = { '/',',','.',' ','(',')','-','_','\t' };
        DataTable dt = new DataTable();
        dt.Columns.Add("PKID");
        dt.Columns.Add("FKID");
        dt.Columns.Add("Value");
        string xSupplier_Name;
        string Search = txtSearch.Text.Trim();

        xSupplier_Name = Search.Replace("PTE", "");
        xSupplier_Name = Search.Replace("LTD", ""); //Replace(xSupplier_Name, "LTD", "");
        xSupplier_Name = Search.Replace("SERVICES", ""); //Replace(xSupplier_Name, "SERVICES", "");

        string[] SearchText = xSupplier_Name.Split(delimiterChars);
        if (SearchText.Length > 0)
        {
            for (int i = 0; i < SearchText.Length; i++)
            {
                DataRow dr = dt.NewRow();
                dr["PKID"] = 0;
                dr["Value"] = SearchText[i];
                dt.Rows.Add(dr);

            }
        }
        else
        {
            DataRow dr = dt.NewRow();
            dr["PKID"] = 0;
            dr["Value"] = "";
            dt.Rows.Add(dr);
        }
        return dt;
    }
    protected void gvSupplierName_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label ASL_Status = (Label)e.Row.FindControl("lblASL_Status");
            string str = ASL_Status.Text;
            if (str == "Blacklist")
            {
                e.Row.Cells[3].BackColor = System.Drawing.Color.Red;
           
            }
            if (str == "Expired")
            {
                e.Row.Cells[3].BackColor = System.Drawing.Color.Yellow;

            }
            if (str == "Approved")
            {
                e.Row.Cells[3].BackColor = System.Drawing.Color.Green;

            }

        }
        
    }
}