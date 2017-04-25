using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using SMS.Business.Infrastructure;
using SMS.Properties;
using System.Data;
using System.Web.UI.HtmlControls;
using SMS.Business.SLC;
using System;
using System.Web.Security;
using SMS.Business.PURC;

public partial class SlopChest_SlopChest_ConsumptionReport : System.Web.UI.Page
{
    BLL_SLC_ConsumptionReport objBLL = new BLL_SLC_ConsumptionReport();
     
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["SORTDIRECTION"] = null;
            ViewState["SORTBYCOLOUMN"] = null;

            ucCustomPagerItems.PageSize = 10;


            this.BindItem();

            BindConsumption();
            HiddenFlag.Value = "Add";

        }
    }

    protected void GvSCDisplayConsumption_RowDataBound(object sender, GridViewRowEventArgs e)
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
    }

    protected void GvSCDisplayConsumption_Sorting(object sender, GridViewSortEventArgs se)
    {
        ViewState["SORTBYCOLOUMN"] = se.SortExpression;

        if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
            ViewState["SORTDIRECTION"] = 1;
        else
            ViewState["SORTDIRECTION"] = 0;

        BindConsumption();
    }




    public void BindItem()
    {
        DataTable dtItem = objBLL.SelectItems();
        ddItem.Items.Clear();
        ddItem.DataSource = dtItem;
        ddItem.DataTextField = "ITEM";
        ddItem.DataValueField = "ITEM_ID";
        ddItem.DataBind();
        ListItem li = new ListItem("--SELECT--", "0");
        ddItem.Items.Insert(0, li);
    }

    public void BindConsumption()
    {
        int rowcount = ucCustomPagerItems.isCountRecord;

        //int comID = UDFLib.ConvertToInteger(Session["USERCOMPANYID"]);

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

        //DataTable dt = objBLL.SearchCompany(txtfilter.Text != "" ? txtfilter.Text : null, UDFLib.ConvertIntegerToNull(ddlCompanyTypeFilter.SelectedValue), UDFLib.ConvertIntegerToNull(ddlCountryIncorpFilter.SelectedValue)
        //    , UDFLib.ConvertIntegerToNull(ddlCurrencyFilter.SelectedValue), UDFLib.ConvertIntegerToNull(ddlCountryFilter.SelectedValue), sortbycoloumn, sortdirection
        //  , ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);

        DataTable dt = objBLL.Get_SlopChestConsumptionReport(txtDate.Text != "" ? txtDate.Text:null, txtCrew.Text.Trim() != "" ? txtCrew.Text.Trim() : null, UDFLib.ConvertIntegerToNull(ddItem.SelectedValue),
            ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount); //UDFLib.ConvertIntegerToNull(ddlYearFilter.SelectedItem.Text)

        if (ucCustomPagerItems.isCountRecord == 1)
        {
            ucCustomPagerItems.CountTotalRec = rowcount.ToString();
            ucCustomPagerItems.BuildPager();
        }


        if (dt.Rows.Count != 0)
        {
            
            GvSCDisplayConsumption.DataSource = dt;
            GvSCDisplayConsumption.DataBind();
        }
        else
        {
            GvSCDisplayConsumption.DataSource = dt;
            GvSCDisplayConsumption.DataBind();

        }


    }

    protected void btnFilter_Click(object sender, ImageClickEventArgs e)
    {
        BindConsumption();
    }
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        txtDate.Text = "";
        txtCrew.Text = "";
        ddItem.SelectedIndex = 0;
        BindConsumption();

    }

    protected void ImgExpExcel_Click(object sender, ImageClickEventArgs e)
    {

        int rowcount = int.Parse(ucCustomPagerItems.CountTotalRec);

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());



        DataTable dt = objBLL.Get_SlopChestConsumptionReport(txtDate.Text != "" ? txtDate.Text : null, txtCrew.Text.Trim() != "" ? txtCrew.Text.Trim() : null, UDFLib.ConvertIntegerToNull(ddItem.SelectedValue), 1, rowcount, ref  rowcount);


        string[] HeaderCaptions = { "Date", "Crew", "Item Description", "Item Price", "Quantity", "Final Amount" };
        string[] DataColumnsName = { "DATE", "CrewDetail", "ITEM", "PRICE", "QUANTITY", "FINAL_AMOUNT" };

        GridViewExportUtil.ShowExcel(dt, HeaderCaptions, DataColumnsName, "SlopChest Consumpsion Report", "SlopChest Consumpsion Report", "");

    }
}