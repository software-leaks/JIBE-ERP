using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

//Custom defined libraries
using SMS.Business.Crew;
using SMS.Properties;
using SMS.Business;
using SMS.Business.TRAV;
using SMS.Business.Infrastructure;

public partial class RefundList : System.Web.UI.Page
{
    string status;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["UTYPE"].ToString() == "TRAVEL AGENT")
            {
                txtAmountReceived.Visible = false;
            }
            else
                txtAmountReceived.Visible = true;

            if (!IsPostBack) 
            {
             
                ViewState["Status"] = "REFUND PENDING";

              //  cmbFleet.SelectedValue = Session["USERFLEETID"].ToString();

                ListItem li = cmbFleet.Items.FindByValue(Convert.ToString(Session["USERFLEETID"]));
                if (li != null)
                    li.Selected = true;

                  //GetTravelRequests("");
                BindRefundList();
            }
        }
        catch { throw; }
    }


    protected void BindRefundList()
    { 
    
        int rowcount = ucCustomPagerItems.isCountRecord;

        string status = ViewState["Status"].ToString();
        BLL_TRV_Refund objRefund = new BLL_TRV_Refund();

        int VCode = 0;
        if (!String.IsNullOrEmpty(cmbVessel.SelectedValue))
            VCode = Convert.ToInt32(cmbVessel.SelectedValue);

        DataSet ds = new DataSet();
        ds = objRefund.GetRefund_RequestList(Convert.ToInt32(cmbFleet.SelectedValue), VCode, Convert.ToInt32(cmbSupplier.SelectedValue),
            txtSectorFrom.Text, txtSectorTo.Text, txtTrvDateFrom.Text, txtTrvDateTo.Text, status, txtPaxName.Text
            ,ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);

        if (ucCustomPagerItems.isCountRecord == 1)
        {
            ucCustomPagerItems.CountTotalRec = rowcount.ToString();
            ucCustomPagerItems.BuildPager();
        }

        rptParent.DataSource = ds;
        rptParent.DataBind();
    
    }
     

    protected void rptParent_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        try { }
        catch { }
    }

    protected void rptParent_OnItemDataBound(object source, RepeaterItemEventArgs e)
    {
        try { }
        catch { }
    }

    protected void rptChild_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        try { }
        catch { }
    }

    protected void cmdUpdteRefund_Click(object source, EventArgs e)
    {
        BLL_TRV_Refund objRefund = new BLL_TRV_Refund();
        try
        {
            objRefund.UpdateRefund(UDFLib.ConvertToInteger(hdRefundID.Value),
                UDFLib.ConvertToDecimal(txtNoShowAmount.Text),
                UDFLib.ConvertToDecimal(txtCancellationAmount.Text),
                UDFLib.ConvertToDecimal(txtRefundAmount.Text),
                UDFLib.ConvertToDecimal(txtAmountReceived.Text),
                txtRefundRemark.Text,
                UDFLib.ConvertToInteger(Session["USERID"].ToString()));
            txtNoShowAmount.Text = "";
            txtAmountReceived.Text = "";
            txtCancellationAmount.Text = "";
            txtRefundAmount.Text = "";
            txtRefundRemark.Text = "";
            string msgmodal = String.Format("hideModal('dvRefund');");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "refundclose", msgmodal, true);

           
            BindRefundList();

        }
        catch { }
        finally { objRefund = null; }
    }

    protected void cmbFleet_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            
            BindRefundList();

        }
        catch { }
    }

    protected void cmbVessel_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            
            BindRefundList();

        }
        catch { }
    }

    protected void cmbVessel_OnDataBound(object source, EventArgs e) { cmbVessel.Items.Insert(0, new ListItem("-Select All-", "0")); }

    protected void cmbSupplier_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
           
            BindRefundList();

        }
        catch { }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            ucCustomPagerItems.CurrentPageIndex = 1;
           
            BindRefundList();
            btnSearch.BackColor = System.Drawing.Color.LightSkyBlue;

        }
        catch { }
    }


}