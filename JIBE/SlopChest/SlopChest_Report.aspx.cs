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
using System.Text;
using System.IO;
using System.Web.Security;
using SMS.Business.PURC;

public partial class SlopChest_SlopChest_Report : System.Web.UI.Page
{
    BLL_SLC_Report objBLL = new BLL_SLC_Report();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.BindVessel();
            BindMonth();
            BindYear();
            BindConsumption();

        }
    }



    protected void ImgExpExcel_Click(object sender, EventArgs e)
    {


        Response.ContentType = "application/x-msexcel";
        Response.AddHeader("Content-Disposition", "attachment; filename=SlopChest_Report.xls");
        Response.Write("<x:Name><B>SlopChest Report<B></x:Name>");
        Response.ContentEncoding = Encoding.UTF8;
        StringWriter tw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(tw);
        tblreport.RenderControl(hw);
        Response.Write(tw.ToString());
        Response.End();



    }


    public void BindVessel()
    {
        DataTable dtVessel = objBLL.SelectVessel();
        ddVessel.Items.Clear();
        ddVessel.DataSource = dtVessel;
        ddVessel.DataTextField = "VesselName";
        ddVessel.DataValueField = "VesselCode";
        ddVessel.DataBind();
        ListItem li = new ListItem("--SELECT--", "0");
        ddVessel.Items.Insert(0, li);
    }

    public void BindYear()
    {


        int Year_Value = DateTime.Now.Year;

        ddYear.DataTextField = "Year";
        ddYear.DataValueField = "Year_Value";
        ddYear.DataBind();
        ListItem li = new ListItem("--SELECT--", "0");
        ddYear.Items.Insert(0, li);
        int j = 1;
        for (int i = Year_Value - 10; i < Year_Value + 10; i++)
        {

            li = new ListItem(i.ToString(), j.ToString());
            ddYear.Items.Insert(j, li);
            j++;
        }
    }

    public void BindMonth()
    {
        ddMonth.DataTextField = "Month";
        ddMonth.DataValueField = "Month_Value";
        ddMonth.DataBind();
        ListItem li = new ListItem("--SELECT--", "0");
        ddMonth.Items.Insert(0, li);

        li = new ListItem("JAN", "1");
        ddMonth.Items.Insert(1, li);

        li = new ListItem("FEB", "2");
        ddMonth.Items.Insert(2, li);

        li = new ListItem("MAR", "3");
        ddMonth.Items.Insert(3, li);

        li = new ListItem("APR", "4");
        ddMonth.Items.Insert(4, li);

        li = new ListItem("MAY", "5");
        ddMonth.Items.Insert(5, li);

        li = new ListItem("JUN", "6");
        ddMonth.Items.Insert(6, li);

        li = new ListItem("JUL", "7");
        ddMonth.Items.Insert(7, li);

        li = new ListItem("AUG", "8");
        ddMonth.Items.Insert(8, li);

        li = new ListItem("SEP", "9");
        ddMonth.Items.Insert(9, li);

        li = new ListItem("OCT", "10");
        ddMonth.Items.Insert(10, li);

        li = new ListItem("NOV", "11");
        ddMonth.Items.Insert(11, li);

        li = new ListItem("DEC", "12");
        ddMonth.Items.Insert(12, li);
    }



    public void BindConsumption()
    {

        DataTable dt = objBLL.Get_SlopChestReport(UDFLib.ConvertIntegerToNull(ddVessel.SelectedValue), UDFLib.ConvertIntegerToNull(ddYear.SelectedItem.Text), UDFLib.ConvertIntegerToNull(ddMonth.SelectedValue));//,ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount

        if (dt.Rows.Count != 0)
        {

            lblStockBF.Text = Convert.ToString(dt.Rows[0]["StockLastMonth"]);
            lblTotalPurchase.Text = Convert.ToString(dt.Rows[0]["Total_Purchase"]);
            lblSoldToCrew.Text = Convert.ToString(dt.Rows[0]["CrewAmount"]);
            lblRepresentationOwner.Text = Convert.ToString(dt.Rows[0]["OwnersAmount"]);
            lblRepresentationChaterer.Text = Convert.ToString(dt.Rows[0]["ChaterersAmount"]);
            lbltotal.Text = Convert.ToString(dt.Rows[0]["TotalStockOnBoard"]);
            lbltotaltakenout.Text = Convert.ToString(dt.Rows[0]["TotalTakenOut"]);
            lbltotalout.Text = Convert.ToString(dt.Rows[0]["TotalTakenOut"]);
            lblstockinhand.Text = Convert.ToString(dt.Rows[0]["TotalStockInHand"]);
        }
        else
        {
            lblStockBF.Text = "N/A";
            lblTotalPurchase.Text = "N/A";
            lblSoldToCrew.Text = "N/A";
            lblRepresentationOwner.Text = "N/A";
            lblRepresentationChaterer.Text = "N/A";
            lbltotal.Text = "N/A";
            lbltotaltakenout.Text = "N/A";
            lbltotalout.Text = "N/A";
            lblstockinhand.Text = "N/A";
        }


    }

    protected void btnFilter_Click(object sender, ImageClickEventArgs e)
    {
        BindConsumption();
    }
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        ddVessel.SelectedIndex = 0;
        ddYear.SelectedIndex = 0;
        ddMonth.SelectedIndex = 0;
        BindConsumption();

    }
}