using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.Infrastructure;
using SMS.Business.POLOG;
using System.IO;
using SMS.Properties;
using System.Drawing;

public partial class PO_LOG_Invoice_Listing : System.Web.UI.Page
{
    BLL_Infra_Currency objBLLCurrency = new BLL_Infra_Currency();
    public string OperationMode = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        //UserAccessValidation();
        if (!IsPostBack)
        {
            txtPOCode.Text = GetSupplyID();
            BindOnlineInvoice();

            BindInvoiceDetails();
            //BindTransferCost();
            BindTransferCost2();
            BindPODetails();

            BindDuplicateInvoice(UDFLib.ConvertStringToNull(txtInvoiceID.Text));
            BindDeliveryGrid(UDFLib.ConvertStringToNull(txtInvoiceID.Text));
        }
    }
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }
    public string GetSupplyID()
    {
        try
        {
            if (Request.QueryString["SUPPLY_ID"] != null)
            {
                return Request.QueryString["SUPPLY_ID"].ToString();
            }

            else
                return "0";
        }
        catch { return "0"; }
    }
    protected void BindOnlineInvoice()
    {
        try
        {
            int rowcount = ucCustomPager1.isCountRecord;

            string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
            int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

            DataSet ds = BLL_POLOG_Invoice.POLOG_Get_Online_Invoice(txtfilter.Text.Trim() != "" ? txtfilter.Text.Trim() : null, UDFLib.ConvertIntegerToNull(txtPOCode.Text), GetSessionUserID(),
                 sortbycoloumn, sortdirection
                , ucCustomPager1.CurrentPageIndex, ucCustomPager1.PageSize, ref  rowcount);

            if (ucCustomPager1.isCountRecord == 1)
            {
                ucCustomPager1.CountTotalRec = rowcount.ToString();
                ucCustomPager1.BuildPager();
            }

            if (ds.Tables[0].Rows.Count > 0)
            {
                divOnlineInvoice.Visible = true;
                gvOnlineInvoice.DataSource = ds.Tables[0];
                gvOnlineInvoice.DataBind();
            }
            else
            {
                divOnlineInvoice.Visible = false;
                gvOnlineInvoice.DataSource = ds.Tables[0];
                gvOnlineInvoice.DataBind();
            }
        }
        catch { }
        {
        }
    }
    protected void BindTransferCost()
    {
        try
        {
            int rowcount = ucCustomPagerItems.isCountRecord;

            string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
            int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());
            string Invoice_ID = invHidden.Value;

            //change
            //Invoice_ID = "73612";
            //txtPOCode.Text = "19779";

            DataSet ds = BLL_POLOG_Invoice.POLOG_Get_Invoice_Transfer_Cost(Invoice_ID, UDFLib.ConvertIntegerToNull(txtPOCode.Text), GetSessionUserID());

            if (ds.Tables[0].Rows.Count > 0)
            {
                dvTransferCost.Visible = true;
                gvTransferCost.DataSource = ds.Tables[0];
                gvTransferCost.DataBind();
                gvTransferCost.Visible = true;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", InvoiceAmount.Value, true);
            }
            else
            {
                //gvTransferCost.Visible = false;
                gvTransferCost.DataSource = null;
                gvTransferCost.DataBind();
            }

        }
        catch { }
        {
        }
    }
    protected void BindTransferCost2()
    {
        try
        {
            int rowcount = ucCustomPagerItems.isCountRecord;

            string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
            int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());
            string Invoice_ID = ViewState["TId"].ToString();

            //change
            //Invoice_ID = "73612";
            //txtPOCode.Text = "19779";

            DataSet ds = BLL_POLOG_Invoice.POLOG_Get_Invoice_Transfer_Cost(Invoice_ID, UDFLib.ConvertIntegerToNull(txtPOCode.Text), GetSessionUserID());

            if (ds.Tables[0].Rows.Count > 0)
            {
                dvTransferCost.Visible = true;
                gvTransferCost.DataSource = ds.Tables[0];
                gvTransferCost.DataBind();
                gvTransferCost.Visible = true;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", InvoiceAmount.Value, true);
            }
            else
            {
                //gvTransferCost.Visible = false;
                gvTransferCost.DataSource = null;
                gvTransferCost.DataBind();
            }

        }
        catch { }
        {
        }
    }
    protected void BindInvoiceDetails()
    {
        try
        {
            int rowcount = ucCustomPagerItems.isCountRecord;

            string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
            int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

            DataSet ds = BLL_POLOG_Invoice.POLOG_Get_Invoice_List(txtfilter.Text.Trim() != "" ? txtfilter.Text.Trim() : null, UDFLib.ConvertIntegerToNull(txtPOCode.Text), GetSessionUserID(),
                 sortbycoloumn, sortdirection
                , ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);
            if (ucCustomPagerItems.isCountRecord == 1)
            {
                ucCustomPagerItems.CountTotalRec = rowcount.ToString();
                ucCustomPagerItems.BuildPager();
            }
            if (ds.Tables[0].Rows.Count > 0)
            {
                DivInvoiceDeatils.Visible = true;
                gvInvoiceDetails.DataSource = ds.Tables[0];
                gvInvoiceDetails.DataBind();
                txtInvoiceID.Text = ds.Tables[0].Rows[0]["Invoice_ID"].ToString();
                Session["Invoice_Amount"] = ds.Tables[0].Rows[0]["Invoice_Amount"].ToString();
                gvInvoiceDetails.Rows[0].BackColor = System.Drawing.Color.Yellow;
                BindTransferCost();
                ViewState["TId"] = txtInvoiceID.Text;

            }
            else
            {
                DivInvoiceDeatils.Visible = false;
                gvInvoiceDetails.DataSource = ds.Tables[0];
                gvInvoiceDetails.DataBind();
            }

        }
        catch { }
        {
        }
    }
    protected void BindPODetails()
    {
        try
        {
            DataSet ds = BLL_POLOG_Register.POLOG_Get_PO_Deatils(UDFLib.ConvertIntegerToNull(txtPOCode.Text.ToString()), null, 0);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                lblSupplyID.Text = dr["SUPPLY_ID"].ToString();
                lblPOCode.Text = dr["Office_Ref_Code"].ToString();
                lblPoAmount.Text = dr["Total_Amt"].ToString();
                lblDays.Text = dr["PaymentTermsDays"].ToString();
                lblSuppliername.Text = dr["Supplier_name"].ToString();
                if (dr["PO_Closed_Date"].ToString() != "")
                {
                    lblPOClose.Visible = true;
                    ImgAdd.Enabled = false;
                    //btnAdvanceRequest.Enabled = false;
                }
                else
                {
                    lblPOClose.Visible = false;
                    ImgAdd.Enabled = true;
                    //btnAdvanceRequest.Enabled = true;
                }

            }
        }
        catch { }
        {
        }

    }
    protected void btnFilter_Click(object sender, EventArgs e)
    {
        BindInvoiceDetails();

    }

    protected void btnDelete_Click(object s, CommandEventArgs e)
    {
        string[] arg = e.CommandArgument.ToString().Split(',');
        txtInvoiceID.Text = UDFLib.ConvertStringToNull(arg[0]);
        int retval = BLL_POLOG_Invoice.POLog_Delete_Invoice(UDFLib.ConvertStringToNull(txtInvoiceID.Text), UDFLib.ConvertIntegerToNull(txtPOCode.Text), UDFLib.ConvertToInteger(GetSessionUserID()));
        BindInvoiceDetails();

        string Action = "Delete Invoice";
        string Description = "DeleteInvoice";
        int RetAuditVal = BLL_POLOG_Register.POLOG_Insert_Transactionlog(UDFLib.ConvertStringToNull(txtInvoiceID.Text), Action, Description, UDFLib.ConvertToInteger(GetSessionUserID()));
    }

    protected void imgfilter_Click(object sender, ImageClickEventArgs e)
    {
        //BindOnlineInvoice();
        BindInvoiceDetails();
    }
    protected void btnRefresh_Click(object sender, ImageClickEventArgs e)
    {
        txtfilter.Text = "";
        BindInvoiceDetails();
    }
    public void lbtnTransfer_Click(object sender, CommandEventArgs e)
    {
        //  GridViewRow row = gvInvoiceDetails.SelectedRow;

        BindTransferCost();
        string[] arg = e.CommandArgument.ToString().Split(',');
        txtInvoiceID.Text = arg[0];
        var link = (ImageButton)sender;
        GridViewRow row = (GridViewRow)link.NamingContainer;

        int rowindex = row.RowIndex;
        //gvInvoiceDetails.Rows[rowindex].BackColor = System.Drawing.Color.Yellow;


    }
    protected void gvInvoiceDetails_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<int> li = new List<int>();
        for (int i = 0; i < gvInvoiceDetails.Rows.Count; i++)
        {
            li.Add(i);


        }

        {
            gvInvoiceDetails.Rows[gvInvoiceDetails.SelectedIndex].BackColor = System.Drawing.Color.Yellow;
            li.Remove(gvInvoiceDetails.SelectedIndex);
            for (int i = 0; i < li.ToArray().Length; i++)
            {
                gvInvoiceDetails.Rows[li.ToArray()[i]].BackColor = System.Drawing.Color.White;
            }

        }



        //Accessing TemplateField Column controls
        txtInvoiceID.Text = (gvInvoiceDetails.SelectedRow.FindControl("Invoice_Id") as Label).Text;
        invHidden.Value = (gvInvoiceDetails.SelectedRow.FindControl("Invoice_Id") as Label).Text;
        BindTransferCost();
    }
    protected void gvInvoiceDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //try
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {

        //        Label Invoice_Id = (Label)e.Row.FindControl("Invoice_Id");
        //        invHidden.Value = Invoice_Id.Text;
        //    }
        //}
        //catch
        //{
        //}
    }
    double sum = 0;
    public void btnDeleteCost_Click(object sender, CommandEventArgs e)
    {
        string[] arg = e.CommandArgument.ToString().Split(',');

        BLL_POLOG_Invoice.POLOG_Del_Transfer_Cost(Convert.ToInt32(UDFLib.ConvertStringToNull(arg[0])), Convert.ToInt32(GetSessionUserID()));
        string msg2 = String.Format("alert('Transfer Cost Deleted.')");
        BindTransferCost();
      
    }
    protected void BindDuplicateInvoice(string InvoiceID)
    {
        try
        {
            DataTable dt = BLL_POLOG_Register.POLOG_Get_DuplicateInvoice(InvoiceID);

            if (dt.Rows.Count > 0)
            {
                lblDuplicateInvoice.Text = "Duplicated invoice Reference Found";
                divDuplicateInvoice.Visible = true;
                gvDuplieCateinvoice.DataSource = dt;
                gvDuplieCateinvoice.DataBind();
            }
            else
            {
                divDuplicateInvoice.Visible = false;
                gvDuplieCateinvoice.DataSource = dt;
                gvDuplieCateinvoice.DataBind();
            }
        }
        catch { }
        {
        }
    }
    protected void BindDeliveryGrid(string InvoiceID)
    {
        try
        {
            DataTable dt = BLL_POLOG_Register.POLOG_Get_Delivery_Invoice(InvoiceID);

            if (dt.Rows.Count > 0)
            {
                lblDelivery.Text = "Confirmed deliveries record Found.";
                divDeliveryDetails.Visible = true;
                gvDeliveryDetails.DataSource = dt;
                gvDeliveryDetails.DataBind();
            }
            else
            {
                lblDelivery.Visible = true;
                lblDelivery.Text = "No confirmed deliveries matched to this invoice.";
                divDeliveryDetails.Visible = false;
                gvDeliveryDetails.DataSource = dt;
                gvDeliveryDetails.DataBind();
            }

        }
        catch { }
        {
        }
    }
}