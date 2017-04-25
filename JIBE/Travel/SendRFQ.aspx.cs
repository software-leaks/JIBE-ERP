using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
//Custome Libraries
using SMS.Business.TRAV;
using SMS.Business.PURC;

public partial class SendRFQ : System.Web.UI.Page
{
    int RequestID;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Request.QueryString["requestid"] != null)
            {



                RequestID = Convert.ToInt32(Request.QueryString["requestid"].ToString());
                ltRequestID.Text = RequestID.ToString();
                if (!IsPostBack)
                {
                    txtQuoteBy.Text = System.DateTime.Now.ToString("dd-MMM-yyyy");
                    BindSupplier();
                    BindRequest();
                }
            }
        }
        catch { throw; }
    }

    protected void BindRequest()
    {
        try
        {
            BLL_TRV_TravelRequest treq = new BLL_TRV_TravelRequest();
            DataSet ds = new DataSet();
            ds = treq.GetTravelRequestByID(RequestID, 0);
            rptParent.DataSource = ds;
            rptParent.DataBind();

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["CurrentStatus"].ToString().ToUpper() == "APPROVED" || ds.Tables[0].Rows[0]["CurrentStatus"].ToString().ToUpper() == "REFUND PENDING")
                {
                    cdmSend.Visible = false;
                }
            }
        }
        catch { }
    }

    protected void sendRFQ(object source, EventArgs e)
    {
        string strData;
        string[] Agents;
        int i;
        try
        {
            strData = Request.Form["chkSelect"];
            Agents = strData.Split(new char[] { ',' });

            for (i = 0; i < Agents.Length; i++)
            {
                if (txtQuoteBy.Text.Trim() != "")
                {
                    BLL_TRV_QuoteRequest RFQ = new BLL_TRV_QuoteRequest();
                    if (RFQ.AddRequestForQuote(Convert.ToInt32(RequestID),
                            Convert.ToInt32(Agents[i]), txtQuoteBy.Text +" "+ ddlDepHours1.SelectedItem.Text +":"+ddlDepMins1.SelectedItem.Text, Convert.ToInt32(Session["USERID"].ToString())))
                    {
                        //BLL_TRV_TravelRequest TRequest = new BLL_TRV_TravelRequest();
                        //BLL_TRV_Supplier objSupplier = new BLL_TRV_Supplier();
                        //int result;
                        //string msg = "We have posted a travel request for your attention. please have a look and provide your best offer for the same. Regards, SeaChange";
                        //result = TRequest.SaveEmail("REQUEST FOR QUOTATION", objSupplier.GetSupplierEmail(Convert.ToInt32(Agents[i])),
                        //    "", msg, Convert.ToInt32(Session["USERID"]));
                        //TRequest = null;
                    }
                    RFQ = null;
                }
            }
            Response.Write("<script type='text/javascript'>alert('Request for quotation has been sent to " + i.ToString() + " AGENTS'); parent.CloseWindow('SendRFQWID');</script>");

            string js = "parent.SendRFQ_Closed();parent.hideModal('dvPopUp');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "closewindow", js, true);

        }
        catch { }
    }

    protected void rptParent_OnItemDataBound(object source, RepeaterItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                System.Data.DataRowView drv = (System.Data.DataRowView)e.Item.DataItem;

                ((Image)e.Item.FindControl("imgRemark")).Attributes.Add("Title", "cssbody=[dvbdy1] cssheader=[dvhdr1] header=[" + drv["remarks"].ToString() + "]");
            }
        }
        catch { }
    }
    protected void rptChild_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        int id;
        try
        {
            if (e.CommandName == "removepax")
            {
                BLL_TRV_TravelRequest TRequest = new BLL_TRV_TravelRequest();
                id = Convert.ToInt32(e.CommandArgument);
                TRequest.RemovePaxFromTravelRequest(id, Convert.ToInt32(Session["USERID"].ToString()));
                TRequest = null;
                BindRequest();
            }
        }
        catch { }
    }

    protected void GrdSupplier_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string RFQSent = DataBinder.Eval(e.Row.DataItem, "RFQSent").ToString();
                if (RFQSent != "0")
                    e.Row.Cells[e.Row.Cells.Count - 1].Text = "RFQ Sent";
            }
        }
        catch { }
    }

    protected void btnSearchSupplier_Click(object s, EventArgs e)
    {
        BindSupplier();
    }

    private void BindSupplier()
    {
        GrdSupplier.DataSource = BLL_TRV_Supplier.Get_SupplierList(UDFLib.ConvertStringToNull(txtSupplierSearch.Text), RequestID);
        GrdSupplier.DataBind();
    }

}