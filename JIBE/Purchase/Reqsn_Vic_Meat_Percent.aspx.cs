using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.PURC;
using System.Data;

public partial class Purchase_Reqsn_Vic_Meat_Percent : System.Web.UI.Page
{
    MergeGridviewHeader_Info objVicRate = new MergeGridviewHeader_Info();
    protected void Page_Load(object sender, EventArgs e)
    {
        objVicRate.AddMergedColumns(new int[] { 0, 1, 2, 3 }, "Dry", "HeaderStyle-css HeaderStyle-css-center");
        objVicRate.AddMergedColumns(new int[] { 4, 5, 6, 7 }, "Fresh", "HeaderStyle-css HeaderStyle-css-center");

        DataSet dsProv = BLL_PURC_Provision.Get_Calculate_Victualling_Rate(Convert.ToInt32(Request.QueryString["VESSEL_ID"]), Request.QueryString["REQSN_CODE"].ToString(), UDFLib.ConvertStringToNull(Request.QueryString["ORDER_CODE"]), Convert.ToInt32(Session["USERID"]));

        gvVictuallingRate.DataSource = dsProv.Tables["VICRATE"];
        gvVictuallingRate.DataBind();

        if (dsProv.Tables["MEAT"].Rows.Count > 0)
        {
            lblMeatMaxLimit.Text = Convert.ToString(dsProv.Tables["MEAT"].Rows[0]["MEAT_LIMIT"]);
            lblMeatCurrentQty.Text = Convert.ToString(dsProv.Tables["MEAT"].Rows[0]["CURRENT_MEAT_QTY"]);

        }

        gvPercentage.DataSource = BLL_PURC_Provision.Get_Reqsn_Items_Percent(Request.QueryString["REQSN_CODE"].ToString(), UDFLib.ConvertStringToNull(Request.QueryString["ORDER_CODE"]), "PROVI");
        gvPercentage.DataBind();

    }
    protected void gvVictuallingRate_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            MergeGridviewHeader.SetProperty(objVicRate);

            e.Row.SetRenderMethodDelegate(MergeGridviewHeader.RenderHeader);
            ViewState["DynamicHeaderCSS"] = "HeaderStyle-css";
        }
    }
}