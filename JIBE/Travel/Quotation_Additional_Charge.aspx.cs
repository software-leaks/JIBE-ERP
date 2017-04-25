using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.TRAV;

public partial class Travel_Quotation_Additional_Charge : System.Web.UI.Page
{
    BLL_TRV_QuoteRequest obQuoteRequest = new BLL_TRV_QuoteRequest();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataSet dsItems = obQuoteRequest.Get_Quotation_Additional_Charge(UDFLib.ConvertToInteger(Request.QueryString["QuotationRequest_ID"]));
            DataTable dtItems = dsItems.Tables[0];
            if (Convert.ToInt32(dtItems.Rows[0]["id"]) == 0)
            {
                ViewState["IsNew"] = true;
                gvItemList.DataSource = dtItems;
                gvItemList.DataBind();

            }
            else
            {
                ViewState["IsNew"] = false;
                gvItemList.DataSource = dtItems;
                gvItemList.DataBind();

                string CalculateTotal = String.Format("CalculateTotal();");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "CalculateTotal", CalculateTotal, true);

            }
            string CurrentSts = dsItems.Tables[1].Rows[0]["currentStatus"].ToString();
            if (CurrentSts == "NEW" || CurrentSts == "RFQ SENT" || CurrentSts == "QUOTE RECEIVED")
            {
                btnSaveItemDetails.Enabled = true;

            }
            else
                btnSaveItemDetails.Enabled = false;
        }

    }
    protected void btnAddNewItem_Click(object s, EventArgs e)
    {
        DataTable dtGridItems = new DataTable();
        dtGridItems.Columns.Add("ID");
        dtGridItems.Columns.Add("item_name");
        dtGridItems.Columns.Add("amount");
        dtGridItems.Columns.Add("remark");

        DataRow dr = null;
        foreach (GridViewRow grItem in gvItemList.Rows)
        {
            dr = dtGridItems.NewRow();
            dr["ID"] = ((HiddenField)grItem.FindControl("hdfID")).Value;
            dr["item_name"] = ((TextBox)grItem.FindControl("txtItem")).Text;
            dr["amount"] = UDFLib.ConvertToDecimal(((TextBox)grItem.FindControl("txtAmount")).Text);
            dr["remark"] = ((TextBox)grItem.FindControl("txtRemark")).Text;
            dtGridItems.Rows.Add(dr);

        }

        dr = dtGridItems.NewRow();
        dr["ID"] = 0;
        dtGridItems.Rows.Add(dr);
        gvItemList.DataSource = dtGridItems;
        gvItemList.DataBind();

        string CalculateTotal = String.Format("CalculateTotal();");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "CalculateTotal", CalculateTotal, true);


    }
    protected void imgbtnDeleteitem_Click(object sender, EventArgs e)
    {
        if (gvItemList.Rows.Count > 1)
        {
            GridViewRow dritem = (GridViewRow)(sender as ImageButton).Parent.Parent;

            DataTable dtGridItems = new DataTable();
            dtGridItems.Columns.Add("ID");
            dtGridItems.Columns.Add("item_name");
            dtGridItems.Columns.Add("amount");
            dtGridItems.Columns.Add("remark");
            int RowID = 0;
            DataRow dr = null;
            foreach (GridViewRow grItem in gvItemList.Rows)
            {
                if (dritem.RowIndex != RowID)
                {

                    dr = dtGridItems.NewRow();
                    dr["ID"] = ((HiddenField)grItem.FindControl("hdfID")).Value;
                    dr["item_name"] = ((TextBox)grItem.FindControl("txtItem")).Text;
                    dr["amount"] = UDFLib.ConvertToDecimal(((TextBox)grItem.FindControl("txtAmount")).Text);
                    dr["remark"] = ((TextBox)grItem.FindControl("txtRemark")).Text;
                    dtGridItems.Rows.Add(dr);
                }

                RowID++;
            }

            //delete from database
            if (!Convert.ToBoolean(ViewState["IsNew"]))
            {
                obQuoteRequest.Upd_Quotation_Additional_Charge_Item(Convert.ToInt32((dritem.FindControl("hdfID") as HiddenField).Value), Convert.ToInt32(Session["USERID"]));

            }
            gvItemList.DataSource = dtGridItems;
            gvItemList.DataBind();

        }
        string CalculateTotal = String.Format("CalculateTotal();");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "CalculateTotal", CalculateTotal, true);
    }

    protected void btnSaveItemDetails_Click(object s, EventArgs e)
    {

        btnSaveItemDetails.Enabled = false;
        DataTable dtGridItems = new DataTable();
        dtGridItems.Columns.Add("PID");
        dtGridItems.Columns.Add("ID");
        dtGridItems.Columns.Add("item_name");
        dtGridItems.Columns.Add("amount");
        dtGridItems.Columns.Add("remark");
        DataRow dr = null;
        int RowID = 1;
        foreach (GridViewRow grItem in gvItemList.Rows)
        {
            dr = dtGridItems.NewRow();
            dr["PID"] = RowID++;
            dr["ID"] = ((HiddenField)grItem.FindControl("hdfID")).Value;
            dr["item_name"] = ((TextBox)grItem.FindControl("txtItem")).Text;
            dr["amount"] = UDFLib.ConvertToDecimal(((TextBox)grItem.FindControl("txtAmount")).Text);
            dr["remark"] = ((TextBox)grItem.FindControl("txtRemark")).Text;
            dtGridItems.Rows.Add(dr);
        }

        obQuoteRequest.Upd_Quotation_Additional_Charge(UDFLib.ConvertToInteger(Request.QueryString["QuotationRequest_ID"]), dtGridItems, Convert.ToInt32(Session["USERID"].ToString()));


        string CalculateTotal = String.Format("parent.ReloadParent_ByButtonID();");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "CalculateTotal", CalculateTotal, true);
    }
}