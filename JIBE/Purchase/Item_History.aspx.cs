using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.PURC;
using System.Data;
using SMS.Business.Infrastructure;

public partial class Purchase_Item_History : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                DataTable dtItem = BLL_PURC_Common.GET_ItemDetails_ByItemRefCode(Request.QueryString["item_ref_code"]);
                BLL_Infra_VesselLib objVSL = new BLL_Infra_VesselLib();
                DataTable dtVsl = objVSL.GetVesselDetails_ByID(Convert.ToInt32(Request.QueryString["vessel_code"]));
                lblcataloguename.Text = dtItem.Rows[0]["system_description"].ToString();
                lblsubcataloguename.Text = dtItem.Rows[0]["subsystem_description"].ToString();
                lblItemRefCode.Text = dtItem.Rows[0]["short_description"].ToString();
                lblLongDesc.Text = Convert.ToString(dtItem.Rows[0]["Long_Description"]);
                lblvesselname.Text = dtVsl.Rows[0]["vessel_name"].ToString();

                gvItemHistory.DataSource = BLL_PURC_Common.GET_Item_History(Request.QueryString["vessel_code"], Request.QueryString["item_ref_code"]);
                gvItemHistory.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
    }
    protected void gvItemHistory_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (DataControlRowType.DataRow == e.Row.RowType)
        {
            DataList rptr = (DataList)e.Row.FindControl("rptQuoted");
            string Document = ((Label)e.Row.FindControl("lblReqsnNO")).ToolTip;
            rptr.DataSource = BLL_PURC_Common.Get_QuotedRates_ByItem(Document, Request.QueryString["item_ref_code"]);
            rptr.DataBind();
        }
    }
    protected void rptQuoted_ItemDataBound(object sender, DataListItemEventArgs e)
    {

        if (e.Item.ItemType.ToString() == "Item" || e.Item.ItemType.ToString() == "AlternatingItem")
        {
            if (((Label)e.Item.FindControl("lblsupp")).Text.Length > 15)
            {
                ((Label)(e.Item.FindControl("lblsupp"))).Attributes.Add("Title", "cssbody=[dvbdy1] cssheader=[dvhdr1] header=[" + ((Label)e.Item.FindControl("lblsupp")).Text + "]");
                ((Label)e.Item.FindControl("lblsupp")).Text = ((Label)e.Item.FindControl("lblsupp")).Text.Substring(0, 13) + " ..";
            }
        }
    }
}