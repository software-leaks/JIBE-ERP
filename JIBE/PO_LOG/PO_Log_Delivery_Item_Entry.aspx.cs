using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.POLOG;

public partial class PO_LOG_PO_Log_Delivery_Item_Entry : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!String.IsNullOrEmpty(Request.QueryString["ID"].ToString()) && !String.IsNullOrEmpty(Request.QueryString["DeliveryID"].ToString()))
            {
                txtDeliveryUnit.Text = "PC";
                txtDeliveryID.Text = Request.QueryString["DeliveryID"].ToString();
                BindDeliveryItem();
            }
        }
    }
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }
    private void BindDeliveryItem()
    {
        DataSet ds = BLL_POLOG_Delivery.POLOG_Get_Delivery_Item_Details(UDFLib.ConvertToInteger(Request.QueryString["ID"].ToString()), UDFLib.ConvertToInteger(txtDeliveryID.Text));

        if (ds.Tables[0].Rows.Count > 0)
        {
            txtDelivery_ItemID.Text = ds.Tables[0].Rows[0]["Delivery_Item_ID"].ToString();
           // txtDeliveryID.Text = ds.Tables[0].Rows[0]["ID"].ToString();
            txtName.Text = ds.Tables[0].Rows[0]["Delivered_Item_Name"].ToString();
            txtPoQty.Text = ds.Tables[0].Rows[0]["POQty"].ToString();
            txtPoPrice.Text = ds.Tables[0].Rows[0]["POprice"].ToString();
            txtConQty.Text = ds.Tables[0].Rows[0]["DeliveryQty"].ToString();
            txtConPrice.Text = ds.Tables[0].Rows[0]["DeliveryPrice"].ToString();
            txtRemarks.Text = ds.Tables[0].Rows[0]["Delivered_Item_Description"].ToString();

            lblPOUnit.Text = ds.Tables[0].Rows[0]["Item_Unit"].ToString();
            lblPOCurrency.Text = ds.Tables[0].Rows[0]["Currency"].ToString();
            txtDeliveryUnit.Text = ds.Tables[0].Rows[0]["Delivered_Item_Unit"].ToString();
            lblDeliveryCurrency.Text = ds.Tables[0].Rows[0]["Currency"].ToString(); ;
            

        }
        else
        {
            txtName.Text = "Non PO Item";
            txtPoQty.Text = "0.00";
            txtPoPrice.Text = "0.00";
        }
       
    }
   
    protected void btnDraft_Click(object sender, EventArgs e)
    {
        try
        {
            //string Confirm_Unit = "PC";
            int retval = BLL_POLOG_Delivery.POLog_Insert_Delivery_Item(UDFLib.ConvertIntegerToNull(Request.QueryString["ID"].ToString()), UDFLib.ConvertIntegerToNull(txtDeliveryID.Text),
                txtName.Text.Trim(), UDFLib.ConvertDecimalToNull(txtPoQty.Text.Trim()), UDFLib.ConvertDecimalToNull(txtPoPrice.Text.Trim()), txtDeliveryUnit.Text,
                 UDFLib.ConvertDecimalToNull(txtConQty.Text.Trim()), UDFLib.ConvertDecimalToNull(txtConPrice.Text.Trim()), txtRemarks.Text.Trim(), UDFLib.ConvertToInteger(GetSessionUserID()));


            string msgDraft = String.Format("parent.ReloadParent_ByButtonID();");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgDraft", msgDraft, true);

        }
        catch
        {
        }
    }
}