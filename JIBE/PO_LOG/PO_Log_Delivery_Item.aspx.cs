using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.POLOG;
using Telerik.Web.UI;
using System.Web.Caching;
using System.Text;

public partial class PO_LOG_PO_Log_Delivery_Item : System.Web.UI.Page
{
    protected DataTable dtGridItems;
    decimal total_Qty = 0;
 
    string clientID;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //if (!String.IsNullOrEmpty(Request.QueryString["Delivery_ID"].ToString()) && !String.IsNullOrEmpty(Request.QueryString["Supply_ID"].ToString()))
            //{
                txtDeliveryID.Text = GetDelivery_ID();
                txtSupply_ID.Text = GetSUPPLY_ID();
                txtDeliveryDate.Text = DateTime.Now.ToShortDateString();
                //txtStatus.Text = Request.QueryString["Status"].ToString();
                BindPortCall();
                BindDeliveryDetails();
                BindDeliveryItem();
            //}
        }
    }
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }
    public string GetSUPPLY_ID()
    {
        try
        {

            if (Request.QueryString["Supply_ID"] != null)
            {
                return Request.QueryString["Supply_ID"].ToString();
            }

            else
                return "0";
        }
        catch { return "0"; }
    }
    public string GetDelivery_ID()
    {
        try
        {

            if (Request.QueryString["Delivery_ID"] != "null")
            {
                return Request.QueryString["Delivery_ID"].ToString();
            }

            else
                return "0";
        }
        catch { return "0"; }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string Action_By_Button = "OPEN";
        string DeliveryStatus = "OPEN";
        //Save(Action_By_Button, DeliveryStatus);
        string retval = BLL_POLOG_Delivery.POLOG_Insert_Delivery_Details(UDFLib.ConvertStringToNull(txtDeliveryID.Text.ToString()), UDFLib.ConvertIntegerToNull(txtSupply_ID.Text.ToString()),
         Convert.ToDateTime(txtDeliveryDate.Text.Trim()), txtLocation.Text.Trim(), UDFLib.ConvertIntegerToNull(ddlPortCall.SelectedValue), txtRemarks.Text.Trim(), Action_By_Button, DeliveryStatus, UDFLib.ConvertToInteger(GetSessionUserID()));
        txtDeliveryID.Text = Convert.ToString(retval);
        saveval();
        InsertAuditTrail("New Delivery", "NewDelivery");
        string msg2 = String.Format("alert('Delivery Record Saved.')");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg2, true);
        BindDeliveryDetails();
        BindDeliveryItem();
    }
    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        string Action_By_Button = "CONFIRMED";
        string DeliveryStatus = "CONFIRMED";

        string retval = BLL_POLOG_Delivery.POLOG_Insert_Delivery_Details(UDFLib.ConvertStringToNull(txtDeliveryID.Text.ToString()), UDFLib.ConvertIntegerToNull(txtSupply_ID.Text.ToString()),
        Convert.ToDateTime(txtDeliveryDate.Text.Trim()), txtLocation.Text.Trim(), UDFLib.ConvertIntegerToNull(ddlPortCall.SelectedValue), txtRemarks.Text.Trim(), Action_By_Button, DeliveryStatus, UDFLib.ConvertToInteger(GetSessionUserID()));
       
        txtDeliveryID.Text = retval;
        saveval();
        InsertAuditTrail("Confirm Delivery", "ConfirmDelivery");
        string msg2 = String.Format("alert('Delivery Record Confirmed.')");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg2, true);
        BindDeliveryDetails();
        BindDeliveryItem();
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        string Action_By_Button = "DELETED";
        string DeliveryStatus = "DELETED";
        int retval = BLL_POLOG_Delivery.POLOG_Delete_Delivery_Details(UDFLib.ConvertStringToNull(txtDeliveryID.Text.ToString()), UDFLib.ConvertIntegerToNull(txtSupply_ID.Text.ToString()), Action_By_Button, DeliveryStatus, UDFLib.ConvertToInteger(GetSessionUserID()));
        InsertAuditTrail("Delete Delivery", "DeleteDelivery");
        string msgDraft = String.Format("parent.ReloadParent_ByButtonID();");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgDraft", msgDraft, true);
    }

    private void BindDeliveryDetails()
    {
        string Status = null;

        DataSet ds = BLL_POLOG_Delivery.POLOG_Get_Delivery_Details(UDFLib.ConvertStringToNull(txtDeliveryID.Text.ToString()), UDFLib.ConvertToInteger(txtSupply_ID.Text.ToString()), Status, UDFLib.ConvertToInteger(GetSessionUserID()));

        if (ds.Tables[0].Rows.Count > 0)
        {
            txtDeliveryID.Text = ds.Tables[0].Rows[0]["Delivery_ID"].ToString();
            txtDeliveryDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["Delivery_Date"].ToString()).ToString("dd/MM/yyyy");
            txtLocation.Text = ds.Tables[0].Rows[0]["Delivery_Location"].ToString();
            ddlPortCall.SelectedValue = ds.Tables[0].Rows[0]["Port_Call_ID"].ToString();
            txtRemarks.Text = ds.Tables[0].Rows[0]["Delivery_Remarks"].ToString();

            lblCreatedBy.Text = ds.Tables[0].Rows[0]["Created_By"].ToString();
            lblCreateddate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["Created_Date"].ToString()).ToString("dd MMM yyyy"); 
            lblDelveryID.Text = ds.Tables[0].Rows[0]["Delivery_ID"].ToString();
            if (ds.Tables[0].Rows[0]["Delivery_Status"].ToString() == "OPEN")
            {
                btnConfirm.Enabled = true;
                btnDelete.Enabled = true;
                btnSave.Enabled = true;
                btnUnlock.Visible = false;

                gvDeliveryItem.MasterTableView.Columns[7].Visible = true;
            }
            else  if (ds.Tables[0].Rows[0]["Delivery_Status"].ToString() == "CONFIRMED")
            {
                btnUnlock.Visible = true;
                btnSave.Enabled = false;
                btnDelete.Enabled = false;
                btnConfirm.Enabled = false;
                gvDeliveryItem.MasterTableView.Columns[7].Visible = false;
            }
            else if (ds.Tables[0].Rows[0]["Delivery_Status"].ToString() == "DELETED")
            {
                btnUnlock.Visible = false;
                btnSave.Enabled = false;
                btnDelete.Enabled = false;
                btnConfirm.Enabled = false;
                gvDeliveryItem.MasterTableView.Columns[7].Visible = false;
            }

            lblCreatedBy.Visible = true;
            lblCreateddate.Visible = true;
            lblDelveryID.Visible = true;
            Label1.Visible = true;
            lblCreated.Visible = true;
            Label2.Visible = true;
        }
       
    }
    protected void BindPortCall()
    {
        DataTable dt = BLL_POLOG_Register.POLOG_Get_Port_Call(UDFLib.ConvertStringToNull(txtDeliveryID.Text.ToString()), UDFLib.ConvertToInteger(txtSupply_ID.Text.ToString()), 0, 1);

        if (dt.Rows.Count > 0)
        {
            ddlPortCall.DataSource = dt;
            ddlPortCall.DataTextField = "PORT_NAME";
            ddlPortCall.DataValueField = "Port_Call_ID";
            ddlPortCall.DataBind();
            ddlPortCall.Items.Insert(0, new ListItem("-Select-", "0"));
        }
    }
    protected void BindDeliveryItem()
    {
        string type = "Edit";
        DataSet ds = BLL_POLOG_Delivery.POLOG_Get_Delivery_Item_Details(UDFLib.ConvertStringToNull(txtDeliveryID.Text.ToString()), UDFLib.ConvertToInteger(Request.QueryString["Supply_ID"].ToString()), type, UDFLib.ConvertToInteger(GetSessionUserID()));
        if (ds.Tables[0].Rows.Count > 0)
        {
            dtGridItems = ds.Tables[0];
            gvDeliveryItem.DataSource = ds.Tables[0];
            gvDeliveryItem.DataBind();
        }
        ViewState["dtGridItems"] = dtGridItems;
    }
    
    
    protected void onDelete(object source, CommandEventArgs e)
    {
        HiddenField lblgrdID = (gvDeliveryItem.FindControl("lblID") as HiddenField);
        string[] arg = e.CommandArgument.ToString().Split(',');
        int? ItemID = UDFLib.ConvertIntegerToNull(arg[0]);
        int retval = BLL_POLOG_Delivery.POLOG_Delete_Delivery_Item(Convert.ToInt32(ItemID), Convert.ToInt32(GetSessionUserID()));
        BindDeliveryItem();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindDeliveryItem();
    }
    protected void btnUnlock_Click(object sender, EventArgs e)
    {
        string Action_By_Button = "OPEN";
        string DeliveryStatus = "OPEN";

        string retval = BLL_POLOG_Delivery.POLOG_Insert_Delivery_Details(UDFLib.ConvertStringToNull(txtDeliveryID.Text.ToString()), UDFLib.ConvertIntegerToNull(txtSupply_ID.Text.ToString()),
         Convert.ToDateTime(txtDeliveryDate.Text.Trim()), txtLocation.Text.Trim(), UDFLib.ConvertIntegerToNull(ddlPortCall.SelectedValue), txtRemarks.Text.Trim(), Action_By_Button, DeliveryStatus, UDFLib.ConvertToInteger(GetSessionUserID()));
        InsertAuditTrail("Unlock Delivery", "UnlockDelivery");
        txtDeliveryID.Text = Convert.ToString(retval);
        BindDeliveryDetails();
    }
    protected void gvDeliveryItem_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //ImageButton UpdateButton = (ImageButton)e.Row.FindControl("ImgUpdate");
            ImageButton DeleteButton = (ImageButton)e.Row.FindControl("ImgDelete");
            if (DataBinder.Eval(e.Row.DataItem, "PO_Item_ID").ToString() == "0")
            {
                //UpdateButton.Visible = true;
                DeleteButton.Visible = true;
            }
            else
            {
                //UpdateButton.Visible = false;
                DeleteButton.Visible = false;
            }
        }
    }
    private DataTable GetAddTable()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("Delivery_Item_ID", typeof(Int32));
        dt.Columns.Add("Order_Item_ID", typeof(Int32));
        dt.Columns.Add("Delivered_Item_Name", typeof(string));
        dt.Columns.Add("POQty", typeof(string));
        dt.Columns.Add("PO_Unit", typeof(Double));
        dt.Columns.Add("POprice", typeof(string));
        dt.Columns.Add("POCurrency", typeof(Double));
        dt.Columns.Add("DeliveryQty", typeof(string));
        dt.Columns.Add("Delivered_Item_Unit", typeof(Double));
        dt.Columns.Add("DeliveryPrice", typeof(string));
        dt.Columns.Add("DCurrency", typeof(Double));
        dt.Columns.Add("Delivered_Item_Description", typeof(string));
        dt.Columns.Add("Invoiced", typeof(string));
        dt.AcceptChanges();
        for (int i = 1; i <= 1; i++)
        {
            DataRow dr = dt.NewRow();
            dr[0] = i;
            dr[1] = i;
            dt.Rows.Add(dr);

        }
        dt.AcceptChanges();
        return dt;
    }

    protected void btnAddNewItem_Click(object s, EventArgs e)
    {
        dtGridItems = (DataTable)ViewState["dtGridItems"];
        int RowID = 0;
        foreach (GridDataItem item in gvDeliveryItem.MasterTableView.Items)
        {
            dtGridItems.Rows[RowID]["Delivery_Item_ID"] = ((HiddenField)item.FindControl("lblID")).Value;
            dtGridItems.Rows[RowID]["Order_Item_ID"] = ((HiddenField)item.FindControl("lblOrderItemID")).Value;
            dtGridItems.Rows[RowID]["Delivered_Item_Name"] = ((Label)item.FindControl("lblPODesc")).Text;
            dtGridItems.Rows[RowID]["POQty"] = ((Label)item.FindControl("lblPOQty")).Text;
            dtGridItems.Rows[RowID]["PO_Unit"] = ((Label)item.FindControl("lblUnit")).Text;
            dtGridItems.Rows[RowID]["POprice"] = ((Label)item.FindControl("lblPOprice")).Text;
            dtGridItems.Rows[RowID]["POCurrency"] = ((Label)item.FindControl("lblPOCurrency")).Text;
            dtGridItems.Rows[RowID]["DeliveryQty"] = ((TextBox)item.FindControl("txtDeliveryQty")).Text;
            dtGridItems.Rows[RowID]["Delivered_Item_Unit"] = ((Label)item.FindControl("lblDUnit")).Text;
            dtGridItems.Rows[RowID]["DeliveryPrice"] = ((TextBox)item.FindControl("txtDeliveryprice")).Text;
            dtGridItems.Rows[RowID]["DCurrency"] = ((Label)item.FindControl("lblDCurrency")).Text;

            dtGridItems.Rows[RowID]["Delivered_Item_Description"] = ((TextBox)item.FindControl("txtItem_Description")).Text;
            dtGridItems.Rows[RowID]["Invoiced"] = ((Label)item.FindControl("lblInvoiced")).Text;
            //TextBox txtDeliveryprice = ((TextBox)item.FindControl("txtDeliveryprice"));
            //txtDeliveryprice.Enabled = true;

            RowID++;
        }

        DataRow dr = dtGridItems.NewRow();
        if (gvDeliveryItem.Items.Count == 0)
        {
            dr[0] = 1;
            dr[1] = 0;
        }
        else
        {
            dr[0] = UDFLib.ConvertToInteger(((HiddenField)gvDeliveryItem.Items[gvDeliveryItem.Items.Count - 1].FindControl("lblID")).Value) + 1;
            dr[1] = 0;
            dr[2] = "Non PO Item";
            dr[3] = 0;
            dr[4] = ((Label)gvDeliveryItem.Items[gvDeliveryItem.Items.Count - 1].FindControl("lblUnit")).Text;
            dr[5] = 0;
            dr[6] = ((Label)gvDeliveryItem.Items[gvDeliveryItem.Items.Count - 1].FindControl("lblPOCurrency")).Text;
            dr[7] = 0;
            dr[8] = ((Label)gvDeliveryItem.Items[gvDeliveryItem.Items.Count - 1].FindControl("lblUnit")).Text;
            dr[9] = 0;
            dr[10] = ((Label)gvDeliveryItem.Items[gvDeliveryItem.Items.Count - 1].FindControl("lblPOCurrency")).Text;
        }
        dtGridItems.Rows.Add(dr);
        gvDeliveryItem.DataSource = dtGridItems;
        gvDeliveryItem.DataBind();
        //gvDeliveryItem.MasterTableView.Columns[8].Visible = false;
        ViewState["dtGridItems"] = dtGridItems;


    }
    protected void gvDeliveryItem_ItemDataBound(object sender, GridItemEventArgs e)
    {
        foreach (GridDataItem dataItem in gvDeliveryItem.MasterTableView.Items)
        {
            TextBox txtDeliveryQty = (TextBox)(dataItem.FindControl("txtDeliveryQty") as TextBox);
            TextBox txtPrice = (TextBox)(dataItem.FindControl("txtDeliveryprice") as TextBox);
            HiddenField lblOrderItemID = (dataItem.FindControl("lblOrderItemID") as HiddenField);
            ImageButton btnDelete = (ImageButton)(dataItem.FindControl("ImgDelete") as ImageButton);
            txtDeliveryQty.Attributes.Add("onKeydown", "return MaskMoney(event)");
            txtPrice.Attributes.Add("onKeydown", "return MaskMoney(event)");
            if (lblOrderItemID.Value == "0")
            {
                btnDelete.Visible = true;
                txtPrice.Enabled = true;
            }
            else
            {
                btnDelete.Visible = false;
                 txtPrice.Enabled = false;
            
            }

        }

        if (e.Item is GridDataItem)
        {
            GridDataItem dataItem = e.Item as GridDataItem;
            if ((dataItem["DeliveryQty"].FindControl("txtDeliveryQty") as TextBox).Text != "")
            {
                total_Qty += decimal.Parse((dataItem["DeliveryQty"].FindControl("txtDeliveryQty") as TextBox).Text);
            }

            //if ((dataItem["DeliveryPrice"].FindControl("txtDeliveryprice") as TextBox).Text != "")
            //{
            //    total_Qty += decimal.Parse((dataItem["DeliveryPrice"].FindControl("txtDeliveryprice") as TextBox).Text);
            //}
        
        }
        else if (e.Item is GridFooterItem)
        {
            GridFooterItem footer = (GridFooterItem)e.Item;
            (footer["DeliveryQty"].FindControl("lblQty") as Label).Text = total_Qty.ToString();
            clientID = (footer["DeliveryQty"].FindControl("lblQty") as Label).ClientID;


            //(footer["DeliveryPrice"].FindControl("lblTotalPrice") as Label).Text = total_Qty.ToString();
            //clientID = (footer["DeliveryPrice"].FindControl("lblTotalPrice") as Label).ClientID;
        }
    }

    private void saveval()
    {
        StringBuilder strIDVals = new StringBuilder();
        StringBuilder strItemdesciption = new StringBuilder();
        StringBuilder stritemUnits = new StringBuilder();
        StringBuilder strItemRequestQty = new StringBuilder();
        StringBuilder strItemComments = new StringBuilder();
        StringBuilder strUnitPrice = new StringBuilder();
        StringBuilder strDiscount = new StringBuilder();
        StringBuilder strSuppCode = new StringBuilder();
        StringBuilder strBgtCode = new StringBuilder();
        StringBuilder ItemRefCode = new StringBuilder();

        int i = 0;

        DataTable dtExtraItems = new DataTable();
        dtExtraItems.Columns.Add("pkid");
        dtExtraItems.Columns.Add("Order_Item_ID");
        dtExtraItems.Columns.Add("POQty");
        dtExtraItems.Columns.Add("POPrice");
        dtExtraItems.Columns.Add("DeliveryQty");
        dtExtraItems.Columns.Add("DeliveryPrice");
        dtExtraItems.Columns.Add("Delivered_Unit");
        dtExtraItems.Columns.Add("Currency");
        dtExtraItems.Columns.Add("Item_Description");
        dtExtraItems.Columns.Add("Delivered_Item_Description");
        dtExtraItems.Columns.Add("Invoiced");



        // dtExtraItems.Columns.Add("BGT_CODE");

        int inc = 1;
        foreach (GridDataItem dataItem in gvDeliveryItem.MasterTableView.Items)
        {
           
            HiddenField lblgrdID = (dataItem.FindControl("lblID") as HiddenField);
            HiddenField lblOrderItemID = (dataItem.FindControl("lblOrderItemID") as HiddenField);
            Label lblPOQty = (dataItem.FindControl("lblPOQty") as Label);
            Label lblPOprice = (dataItem.FindControl("lblPOprice") as Label);
            TextBox txtDeliveryQty = (dataItem.FindControl("txtDeliveryQty") as TextBox);
            TextBox txtDeliveryprice = (dataItem.FindControl("txtDeliveryprice") as TextBox);
            Label lblUnit = (dataItem.FindControl("lblUnit") as Label);
            Label lblPOCurrency = (dataItem.FindControl("lblPOCurrency") as Label);
            Label lblPODesc = (dataItem.FindControl("lblPODesc") as Label);
            TextBox txtItem_Description = (dataItem.FindControl("txtItem_Description") as TextBox);
            Label lblInvoiced = (dataItem.FindControl("lblInvoiced") as Label);

            //if (txtgrdItemReqQty.Text.Length > 0 && txtItemDescription.Text.Length > 0)
            //{
                DataRow dritem = dtExtraItems.NewRow();
                dritem["pkid"] = lblgrdID.Value;
                dritem["Order_Item_ID"] = lblOrderItemID.Value;
                dritem["POQty"] = lblPOQty.Text.ToString();
                dritem["POPrice"] = lblPOprice.Text.ToString();
                dritem["DeliveryQty"] = (txtDeliveryQty.Text.Trim() == "" ? "0" : txtDeliveryQty.Text.Trim());
                dritem["DeliveryPrice"] = txtDeliveryprice.Text.ToString();
                dritem["Delivered_Unit"] = lblUnit.Text.ToString();
                dritem["Currency"] = lblPOCurrency.Text.ToString();
                dritem["Item_Description"] = lblPODesc.Text.ToString();
                dritem["Delivered_Item_Description"] = txtItem_Description.Text.ToString();
                dritem["Invoiced"] = lblInvoiced.Text.ToString();
                dtExtraItems.Rows.Add(dritem);
                inc++;

            //}
        }

        int retval = 0;

        if (dtExtraItems.Rows.Count > 0)
        {
            retval = BLL_POLOG_Delivery.POLog_Insert_Delivery_Item(UDFLib.ConvertStringToNull(txtDeliveryID.Text.ToString()), UDFLib.ConvertIntegerToNull(txtSupply_ID.Text.ToString()), dtExtraItems, UDFLib.ConvertIntegerToNull(Session["USERID"].ToString()));

        }
        //else
        //{
        //    string msg2 = String.Format("alert('Please Select Atleast one row.')");
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg2, true);
        //}


    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        //string msgDraft = String.Format("parent.ReloadParent_ByButtonID();");
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgDraft", msgDraft, true);
        string msgDraft = String.Format("window.parent.location.reload(true);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgDraft", msgDraft, true);
    }
    protected void InsertAuditTrail(string Action, string Description)
    {
        try
        {
            int RetAuditVal = BLL_POLOG_Register.POLOG_Insert_Transactionlog(UDFLib.ConvertStringToNull(txtDeliveryID.Text), Action, Description, UDFLib.ConvertToInteger(GetSessionUserID()));
        }
        catch { }
        {

        }
    }
}