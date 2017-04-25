using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.SLC;

public partial class Purchase_SlopChestDetails : System.Web.UI.Page
{
    BLL_SLC_Admin objBLL = new BLL_SLC_Admin();
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void BindCompany()
    {
        int rowcount = ucCustomPagerItems.isCountRecord;

        int comID = UDFLib.ConvertToInteger(Session["USERCOMPANYID"]);

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

        //DataTable dt = objBLL.SearchCompany(txtfilter.Text != "" ? txtfilter.Text : null, UDFLib.ConvertIntegerToNull(ddlCompanyTypeFilter.SelectedValue), UDFLib.ConvertIntegerToNull(ddlCountryIncorpFilter.SelectedValue)
        //    , UDFLib.ConvertIntegerToNull(ddlCurrencyFilter.SelectedValue), UDFLib.ConvertIntegerToNull(ddlCountryFilter.SelectedValue), sortbycoloumn, sortdirection
        //  , ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);

        DataTable dt = objBLL.Get_SlopchestIndex(UDFLib.ConvertIntegerToNull(ddlVesselFilter.SelectedValue), UDFLib.ConvertIntegerToNull(ddlYearFilter.SelectedItem.Text), UDFLib.ConvertIntegerToNull(ddlMonth.SelectedValue),
            ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);



        if (ucCustomPagerItems.isCountRecord == 1)
        {
            ucCustomPagerItems.CountTotalRec = rowcount.ToString();
            ucCustomPagerItems.BuildPager();
        }


        if (dt.Rows.Count > 0)
        {
            GridViewSlopchest.DataSource = dt;
            GridViewSlopchest.DataBind();
        }
        else
        {
            GridViewSlopchest.DataSource = dt;
            GridViewSlopchest.DataBind();
        }


    }
    protected void btnRefresh_Click(object sender, EventArgs e)
    {

    }
    protected void btnFilter_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void GridViewSlopchest_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void GridViewSlopchest_Sorting(object sender, GridViewSortEventArgs e)
    {

    }
    protected void ImgExpExcel_Click(object sender, ImageClickEventArgs e)
    {

    }
}