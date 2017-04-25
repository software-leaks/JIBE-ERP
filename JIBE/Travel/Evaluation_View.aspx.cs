//System libararies
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
//Custom libararies
using SMS.Business.TRAV;
using SMS.Business.Infrastructure;
using System.Configuration;
using System.Web.UI.HtmlControls;
using SMS.Business.PURC;
using System.IO;




public partial class Evaluation : System.Web.UI.Page
{
    string LastTravelAgent = "";
    protected string currentstatus = "";
    protected int requestID, paxcount, userPreference = 0;
    int QuotationApproved = 0;
    public int IsApproving = 0;


    public SMS.Properties.UserAccess objUA = new SMS.Properties.UserAccess();
    protected void Page_Load(object sender, EventArgs e)
    {
        objUA.Add = 1;
        objUA.Approve = 1;
        objUA.Delete = 1;
        objUA.Edit = 1;
        objUA.View = 1;



        if (Request.QueryString["requestid"] != null)
        {
            try
            {
                requestID = Convert.ToInt32(Request.QueryString["requestid"].ToString());
                ltRequestid.Text = requestID.ToString();
                if (!IsPostBack)
                {
                    BLL_TRV_TravelRequest objT = new BLL_TRV_TravelRequest();
                    DataTable dtsts = objT.Get_Request_ApprovalStatus(requestID);
                    if (dtsts.Rows.Count > 0)
                    {
                        if (dtsts.Rows[0]["Sent_For_Approval"].ToString() == "1" && dtsts.Rows[0]["currentStatus"].ToString() == "QUOTE RECEIVED")
                        {
                            IsApproving = 1;
                        }
                    }

                    ViewState["ChepeastAmount"] = null;

                    GetTravelRequestDetails();

                    GetCheapestOptions();

                    GetQuotationForEvaluation();
                    BLL_TRV_QuoteRequest objAppHis = new BLL_TRV_QuoteRequest();

                    gvApprovals.DataSource = objAppHis.Get_Approval_History(requestID);
                    gvApprovals.DataBind();






                }




            }
            catch { }
        }
    }




    protected void GetTravelRequestDetails()
    {
        BLL_TRV_TravelRequest treq = new BLL_TRV_TravelRequest();
        try
        {
            DataSet ds = new DataSet();
            ds = treq.GetTravelRequestByID(requestID, 0);
            lblSeamanStatus.Text = ds.Tables[0].Rows[0]["isSeaman"].ToString() == "Y" ? "SEAMAN TICKET" : "NOT A SEAMAN TICKET";
            lblSeamanStatus.ForeColor = ds.Tables[0].Rows[0]["isSeaman"].ToString() == "Y" ? System.Drawing.Color.Blue : System.Drawing.Color.Red;

            rptParent.DataSource = ds;
            rptParent.DataBind();
        }
        catch { }
        finally { treq = null; }
    }

    protected void GetQuotationForEvaluation()
    {
        BLL_TRV_QuoteRequest QR = new BLL_TRV_QuoteRequest();
        try
        {
            DataSet ds = new DataSet();
            ds = QR.GetQuotationForEvaluation(requestID);
            rptQuotes.DataSource = ds;
            rptQuotes.DataBind();

            hdf_No_of_Quotation.Value = ds.Tables[0].Rows.Count.ToString();

        }
        catch { }
        finally { QR = null; }
    }

    protected void GetCheapestOptions()
    {

        BLL_TRV_QuoteRequest QR = new BLL_TRV_QuoteRequest();
        try
        {
            DataSet ds = new DataSet();
            ds = QR.GetCheapestOptions(requestID);
            ViewState["ChepeastAmount"] = ds.Tables[0].Rows[0]["USD_Total_Amount"].ToString();
            hdf_Cheapest_Totalamount.Value = ds.Tables[0].Rows[0]["USD_Total_Amount"].ToString();

        }
        catch { }
        finally { QR = null; }

    }


    protected void rptParent_OnItemDataBound(object source, RepeaterItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                System.Data.DataRowView drv = (System.Data.DataRowView)e.Item.DataItem;
                paxcount = Convert.ToInt32(drv.Row["PaxCount"].ToString());
                currentstatus = drv.Row["CurrentStatus"].ToString().ToUpper();
                if (currentstatus == "APPROVED")
                {
                   
                    QuotationApproved = 1;
                }
                if (currentstatus == "TICKET ISSUED")
                {
                   
                    QuotationApproved = 1;
                }

                ((Image)e.Item.FindControl("imgRemark")).Attributes.Add("Title", "cssbody=[dvbdy1] cssheader=[dvhdr1] header=[" + drv["remarks"].ToString() + "]");
                if (drv.Row["User_Preferred_QuoteId"] != null)
                    userPreference = UDFLib.ConvertToInteger(drv.Row["User_Preferred_QuoteId"].ToString());
            }

        }
        catch { }
    }
    
    protected void rptQuotes_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        try
        {
            if (e.CommandName.ToUpper() == "RESETQUOTE")
            {
                BLL_TRV_TravelRequest TRequest = new BLL_TRV_TravelRequest();
                int MailID = TRequest.ResetQuotation(requestID, 0, Convert.ToInt32(e.CommandArgument));
                TRequest = null;
                string UploadFilePath = ConfigurationManager.AppSettings["TRV_UPLOAD_PATH"];

                string URL = String.Format("window.open('../crew/EmailEditor.aspx?ID=+" + MailID.ToString() + @"&FILEPATH=" + UploadFilePath.Remove(UploadFilePath.Length - 1, 1).Replace(@"\", @"\\") + "');");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "k" + MailID.ToString(), URL, true);
                GetQuotationForEvaluation();
            }

            if (e.CommandName.ToUpper() == "PREFERENCE")
            {
                BLL_TRV_QuoteRequest Qr = new BLL_TRV_QuoteRequest();
                Qr.UpdateUserPreference(requestID, Convert.ToInt32(e.CommandArgument), Convert.ToInt32(Session["USERID"]));
                Qr = null;
                GetTravelRequestDetails();
                GetQuotationForEvaluation();
            }

        }
        catch { }
    }
    int rownumber = 2;
    protected void rptQuotes_OnItemDataBound(object source, RepeaterItemEventArgs e)
    {
        try
        {
            decimal amount;
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                System.Data.DataRowView drv = (System.Data.DataRowView)e.Item.DataItem;


                if (rownumber % 2 == 0)
                    (e.Item.FindControl("datarow") as HtmlTableRow).Attributes.Add("class", "RowStyle-css");
                else
                    (e.Item.FindControl("datarow") as HtmlTableRow).Attributes.Add("class", "AlternatingRowStyle-css");

                rownumber++;

                amount = Convert.ToDecimal(drv.Row["totalamount"]);

                ((Label)e.Item.FindControl("lblGrandTotal")).Text = (paxcount * amount).ToString();
                ((Label)e.Item.FindControl("lblUSDRate")).Text = System.Math.Round(paxcount * UDFLib.ConvertToDecimal(drv["USDRate"].ToString()), 2).ToString();


                if (((Label)e.Item.FindControl("lblGrandTotal_usd")).Text == ViewState["ChepeastAmount"].ToString())
                {
                    ((Label)e.Item.FindControl("lblGrandTotal_usd")).BackColor = System.Drawing.Color.Yellow;

                }

                if (!String.IsNullOrEmpty(drv.Row["user_preference_By"].ToString()))
                {
                    ((Button)e.Item.FindControl("cmdUserPref")).Text = "Prefered By: " + drv["user_preference_By"].ToString();
                    ((Button)e.Item.FindControl("cmdUserPref")).Enabled = false;
                }

                if (LastTravelAgent == drv["Short_Name"].ToString())
                {
                    ((Label)e.Item.FindControl("lblAgentName")).Text = "";
                    ((Button)e.Item.FindControl("imgReset")).Visible = false;
                }
                LastTravelAgent = drv["Short_Name"].ToString();

                if (QuotationApproved == 1)
                {
                    ((Button)e.Item.FindControl("cmdUserPref")).Enabled = false;

                    ((Button)e.Item.FindControl("imgReset")).Enabled = false;
                }

            }
        }
        catch { }
    }

    protected void rptFlights_OnItemDataBound(object source, RepeaterItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                System.Data.DataRow drv = (System.Data.DataRow)e.Item.DataItem;
                ((Image)e.Item.FindControl("imgFlightRemark")).Attributes.Add("Title", "cssbody=[dvbdy1] cssheader=[dvhdr1] header=[" + drv["remarks"].ToString() + "]");
            }
        }
        catch { }
    }



    protected void btnSendForApproval_Click(object s, EventArgs e)
    {


    }

    protected void btnRefreshpage_Click(object s, EventArgs e)
    {
        GetTravelRequestDetails();

        GetCheapestOptions();

        GetQuotationForEvaluation();
    }

    protected void btnReworkPIC_Click(object s, EventArgs e)
    {

    }


    protected void btnSendForApprovalByPIC_Click(object s, EventArgs e)
    {
    }

}