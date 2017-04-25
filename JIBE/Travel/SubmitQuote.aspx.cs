//System libararies
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//Custom defined libararies
using SMS.Business;
using SMS.Business.TRAV;
using SMS.Business.Infrastructure;
using System.Configuration;

public partial class SubmitQuote : System.Web.UI.Page
{
    protected int RequestID, AgentID;
   protected string CurrentStatus;
 
    protected void Page_Load(object sender, EventArgs e)
    {
        UserAccessValidation();
      

        try
        {
            if (string.IsNullOrWhiteSpace(Request.QueryString["SUPPLIER_ID"]))
                Response.Redirect("RequestListAgent.aspx");

            RequestID = Convert.ToInt32(Request.QueryString["RequestID"]);
            AgentID = Convert.ToInt32(Request.QueryString["SUPPLIER_ID"]);

            if (!IsPostBack)
            {
                ViewState["SUPPCURRENCY"] = null;

                GetTravelReqeustByID();
                GetQuotations();
                ltRequest.Text = "Requisition No. " + RequestID.ToString();

                BLL_Infra_Currency objCurr = new BLL_Infra_Currency();
                cmbCurrency.DataTextField = "Currency_Code";
                cmbCurrency.DataValueField = "Currency_Code";
                cmbCurrency.DataSource = objCurr.Get_CurrencyList();
                cmbCurrency.DataBind();
                ListItem lis = new ListItem("-Select Currency-", "");
                cmbCurrency.Items.Insert(0, lis);
                //cmbCurrency.SelectedIndex = 0;


                if (ViewState["SUPPCURRENCY"].ToString() != "" || ViewState["SUPPCURRENCY"] != null)
                    cmbCurrency.SelectedValue = ViewState["SUPPCURRENCY"].ToString();

                for (int time = 0; time < 24; time++)
                {

                    cmbArrHours.Items.Add(new ListItem((time < 10) ? "0" + time.ToString() : time.ToString(), time.ToString()));
                    cmbDepHours.Items.Add(new ListItem((time < 10) ? "0" + time.ToString() : time.ToString(), time.ToString()));
                    cmbHours.Items.Add(new ListItem((time < 10) ? "0" + time.ToString() : time.ToString(), time.ToString()));

                }

                for (int mins = 0; mins < 60; mins += 5)
                {
                    cmbMins.Items.Add(new ListItem((mins < 10) ? "0" + mins.ToString() : mins.ToString(), mins.ToString()));
                    cmbDepMins.Items.Add(new ListItem((mins < 10) ? "0" + mins.ToString() : mins.ToString(), mins.ToString()));
                    cmbArrMins.Items.Add(new ListItem((mins < 10) ? "0" + mins.ToString() : mins.ToString(), mins.ToString()));
                }

            }
            CurrentStatus =Convert.ToString(ViewState["CurrentStatus"]);
        }
        catch { }
    }
    public SMS.Properties.UserAccess objUA = new SMS.Properties.UserAccess();
    protected void UserAccessValidation()
    {
        int CurrentUserID = Convert.ToInt32(Session["userid"]);
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());
        SMS.Business.Infrastructure.BLL_Infra_UserCredentials objUser = new SMS.Business.Infrastructure.BLL_Infra_UserCredentials();

        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
            Response.Redirect("~/default.aspx?msgid=1");

        if (objUA.Add == 0)
        {
            btnAddQuotation.Visible = false;

        }
        if (objUA.Edit == 0)
        {
            cmdSaveFlight.Visible = false;
            cmdUpdateFlight.Visible = false;


        }
        if (objUA.Approve == 0)
        {

        }
        if (objUA.Delete == 0)
        {


        }


    }

    protected void GetTravelReqeustByID()
    {
        BLL_TRV_TravelRequest treq = new BLL_TRV_TravelRequest();
        try
        {
            ViewState["CurrentStatus"] = treq.GetCurrentStatus(RequestID, AgentID).ToString().ToUpper();
            if (Convert.ToString(ViewState["CurrentStatus"]) == "CLOSED")
            {
                Response.Write("<center style='color:red;'>Travel request has been close...</center>");
                Response.End();
            }

            if (Convert.ToString(ViewState["CurrentStatus"]) == "NA")
            {
                Response.Write("<center style='color:red;'>Unkonwn request status, please contact XT...</center>");
                Response.End();
            }

            if (Convert.ToString(ViewState["CurrentStatus"]) == "QUOTE RECEIVED" || Convert.ToString(ViewState["CurrentStatus"]) == "NEW" || Convert.ToString(ViewState["CurrentStatus"]) == "RFQ SENT")
                btnAddQuotation.Enabled = true;
            else
                btnAddQuotation.Enabled = false;

            DataSet ds = new DataSet();
            objRequestFlights.SelectParameters["RequestID"].DefaultValue = RequestID.ToString();

            ds = treq.GetTravelRequestByID(RequestID, 1);

            lblRequestorName.Text = ds.Tables[0].Rows[0]["created_By"].ToString();
            lblRequestorEmail.Text = ds.Tables[0].Rows[0]["MailID"].ToString();
            lblRequestorMobile.Text = ds.Tables[0].Rows[0]["Mobile_Number"].ToString();

            hdf_CompanyName.Value = ds.Tables[0].Rows[0]["Company_Name"].ToString();

            lblSeamanStatus.Text = ds.Tables[0].Rows[0]["isSeaman"].ToString() == "Y" ? "SEAMAN TICKET" : "NOT A SEAMAN TICKET";
            lblSeamanStatus.ForeColor = ds.Tables[0].Rows[0]["isSeaman"].ToString() == "Y" ? System.Drawing.Color.Blue : System.Drawing.Color.Red;
            hdfIsSeaman.Value = ds.Tables[0].Rows[0]["isSeaman"].ToString();
            rptRequest.DataSource = ds;
            rptRequest.DataBind();

            rptPax.DataSource = ds.Tables[1];
            rptPax.DataBind();


        }
        catch { }
        finally { treq = null; }
    }

    protected void GetQuotations()
    {
        BLL_TRV_QuoteRequest qr = new BLL_TRV_QuoteRequest();
        DataSet ds = new DataSet();
        try
        {
            ds = qr.GetQuotation(AgentID, RequestID);
            rptParent.DataSource = ds;
            rptParent.DataBind();

            ViewState["SUPPCURRENCY"] = ds.Tables[2].Rows[0]["Supplier_Currency"].ToString();
            ltQuotationDate.Text = ds.Tables[3].Rows[0]["QuoteBy_date"].ToString();

        }
        catch { }
        finally { qr = null; }
    }

    protected void rptRequest_OnItemDataBound(object source, RepeaterItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                System.Data.DataRowView drv = (System.Data.DataRowView)e.Item.DataItem;
                ViewState["ApprovedQuoteID"] = UDFLib.ConvertToInteger(drv["ApprovedQuoteID"].ToString());
                ((Image)e.Item.FindControl("imgRemark")).Attributes.Add("Title", "cssbody=[dvbdy1] cssheader=[dvhdr1] header=[" + drv["remarks"].ToString() + "]");
            }

        }
        catch { }
    }

    protected void rptParent_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        try
        {
            BLL_TRV_QuoteRequest Qr = new BLL_TRV_QuoteRequest();
            if (e.CommandName.ToUpper() == "SENDQUOTE")
            {

                Qr.SendQuotaion(Convert.ToInt32(e.CommandArgument), Convert.ToInt32(Session["USERID"].ToString()));
                Qr = null;
                GetQuotations();
            }
            if (e.CommandName == "DELETEQUOTE")
            {
                Qr.DeleteQuotation(Convert.ToInt32(e.CommandArgument), Convert.ToInt32(Session["userid"]));
                GetQuotations();
            }
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

                string strName = drv.Row["quotedate"].ToString();
                cmbCurrency.SelectedValue = drv.Row["currency"].ToString();

                if (!String.IsNullOrEmpty(strName))
                {
                    if (Convert.ToString(ViewState["CurrentStatus"]) == "APPROVED" && Convert.ToInt32(ViewState["ApprovedQuoteID"]) == Convert.ToInt32(drv.Row["id"].ToString()))
                    {
                        ((Button)e.Item.FindControl("cmdSendQuote")).Text = "Quote approved, Please issue ticket";
                        ((Button)e.Item.FindControl("cmdSendQuote")).Enabled = false;
                    }
                    else
                    {
                        ((Button)e.Item.FindControl("cmdSendQuote")).Text = "Quote Sent on " + strName;
                        ((Button)e.Item.FindControl("cmdSendQuote")).Enabled = false;
                    }
                    if (Convert.ToString(ViewState["CurrentStatus"]) == "TICKET ISSUED" || Convert.ToString(ViewState["CurrentStatus"]) == "ISSUED" || Convert.ToString(ViewState["CurrentStatus"]) == "CLOSED" || Convert.ToString(ViewState["CurrentStatus"]) == "CANCELLED" || Convert.ToString(ViewState["CurrentStatus"]) == "REFUND" )
                    {
                        ((Button)e.Item.FindControl("cmdSendQuote")).Text = "Request Status - " + Convert.ToString(ViewState["CurrentStatus"]);
                        ((Button)e.Item.FindControl("cmdSendQuote")).Enabled = false;
                    }
                }

               if (String.IsNullOrEmpty(strName) && (Convert.ToString(ViewState["CurrentStatus"]) == "QUOTE RECEIVED" || Convert.ToString(ViewState["CurrentStatus"]) == "NEW" || Convert.ToString(ViewState["CurrentStatus"]) == "RFQ SENT"))
                   ((Button)e.Item.FindControl("cmdSendQuote")).Enabled = true;
                else
                   ((Button)e.Item.FindControl("cmdSendQuote")).Enabled = false;
            }
        }
        catch { }
    }

    protected void rptChild_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        int id;
        try
        {
            if (e.CommandName == "removeflight")
            {
                BLL_TRV_QuoteRequest qr = new BLL_TRV_QuoteRequest();
                id = Convert.ToInt32(e.CommandArgument);
                qr.DeleteQuoteFlight(id, Convert.ToInt32(Session["USERID"].ToString()));
                qr = null;
                GetQuotations();
            }
        }
        catch { }
    }

    protected void rptChild_OnItemDataBound(object source, RepeaterItemEventArgs e)
    {
        BLL_TRV_TravelRequest treq = new BLL_TRV_TravelRequest();
        try
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                System.Data.DataRow drv = (System.Data.DataRow)e.Item.DataItem;
                if (Convert.ToString(ViewState["CurrentStatus"]) == "APPROVED" && Convert.ToInt32(ViewState["ApprovedQuoteID"]) == Convert.ToInt32(drv["quoteid"].ToString()))
                {
                    ((Image)e.Item.FindControl("imgTicket")).Attributes.Add("Title", "cssbody=[dvbdy1] cssheader=[dvhdr1] header=[" + drv["flightRemarks"].ToString() + "]");
                }
                ((Image)e.Item.FindControl("imgflightRemark")).Attributes.Add("Title", "cssbody=[dvbdy1] cssheader=[dvhdr1] header=[" + drv["flightRemarks"].ToString() + "]");
            }
        }
        catch { }
        finally { treq = null; }
    }

    protected void cmdSave_Click(object sender, EventArgs e)
    {
        //BLL_TRV_QuoteRequest Qr = new BLL_TRV_QuoteRequest();
        //int QuoteID;
        //try
        //{
        //    if (txtFare.Text.Trim() != "" && txtTax.Text.Trim() != "" && txtTicketDeadline.Text.Trim() != ""
        //        && txtGDSLocator.Text.Trim() != "")
        //    {
        //        QuoteID = Qr.AddQuotation(RequestID, AgentID, txtGDSLocator.Text.Trim(),
        //                Convert.ToDateTime(txtTicketDeadline.Text.Trim()),
        //                Convert.ToDecimal(txtFare.Text.Trim()), Convert.ToDecimal(txtTax.Text.Trim()),
        //                txtPNRText.Text.Trim(), cmbCurrency.SelectedValue, Convert.ToInt32(Session["USERID"].ToString()),
        //                cmbHours.SelectedItem.Text, cmbMins.SelectedItem.Text);

        //        //Qr.AddQuotationFlights(QuoteID, txtAirlineLocator.Text, txtFrom.Text, txtTo.Text,
        //        //        "", txtFlight.Text, cmbTravelClass.SelectedValue.ToString(),
        //        //        Convert.ToDateTime(txtDeparureDate.Text), Convert.ToDateTime(txtArrivalDate.Text),
        //        //        cmdFlightStatus.SelectedValue, txtQuoteRemark.Text, Convert.ToInt32(Session["USERID"].ToString()));

        //        GetQuotations();
        //        ClearBoxes();
        //    }

        //}
        //catch { throw; }
        //finally { Qr = null; }
    }

    protected void cmdUpdateQuote_Click(object source, EventArgs e)
    {
        string msgmodal = "";
        BLL_TRV_QuoteRequest Qr = new BLL_TRV_QuoteRequest();
        int QuoteID = Convert.ToInt32(Request.Form["hdQuoteID"].ToString());
        try
        {
            if (txtFare.Text.Trim() != "" && txtTax.Text.Trim() != "" && txtTicketDeadline.Text.Trim() != ""
                && txtGDSLocator.Text.Trim() != "")
            {
                Qr.UpdateQuotation(QuoteID, txtGDSLocator.Text.Trim(), Convert.ToDecimal(txtFare.Text.Trim()),
                    Convert.ToDecimal(txtTax.Text.Trim()), Convert.ToDateTime(txtTicketDeadline.Text.Trim()),
                    cmbCurrency.SelectedValue, Convert.ToInt32(Session["USERID"].ToString()), txtPNRText.Text,
                    Convert.ToInt32(cmbHours.SelectedValue), Convert.ToInt32(cmbMins.SelectedValue));
                GetTravelReqeustByID();
                GetQuotations();
                ClearBoxes();
            }



            msgmodal = String.Format("hideModal('dvNewQuotation');");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "closedvNewQuotation", msgmodal, true);


        }
        catch { throw; }
        finally { Qr = null; }
    }

    protected void cmdSaveFlight_Click(object source, EventArgs e)
    {

        //string msgmodal = "";
        //BLL_TRV_QuoteRequest Qr = new BLL_TRV_QuoteRequest();
        //int QuoteID = Convert.ToInt32(Request.Form["hdQuoteID"].ToString());
        //try
        //{
        //    if (txtAirlineLocator.Text.Trim() != "" && txtFlight.Text.Trim() != "" && txtDeparureDate.Text.Trim() != ""
        //        && txtFrom.Text.Trim() != "" && txtTo.Text.Trim() != "")
        //    {
        //        Qr.AddQuotationFlights(QuoteID, txtAirlineLocator.Text.Trim(), txtFrom.Text.Trim(), txtTo.Text.Trim(),
        //           "", txtFlight.Text.Trim(), cmbTravelClass.SelectedValue, Convert.ToDateTime(txtDeparureDate.Text.Trim()),
        //         UDFLib.ConvertDateToNull(txtArrivalDate.Text.Trim()), cmbFlightStatus.SelectedValue,
        //           txtFlightRemark.Text.Trim(), Convert.ToInt32(Session["USERID"].ToString()),
        //          cmbArrHours.SelectedItem.Text, cmbArrMins.SelectedItem.Text,
        //           cmbDepHours.SelectedItem.Text, cmbDepMins.SelectedItem.Text);


        //        GetTravelReqeustByID();
        //        GetQuotations();
        //        ClearBoxes();
        //    }

        //    msgmodal = String.Format("hideModal('dvQuote');");
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "closedvQuote", msgmodal, true);

        //}
        //catch { throw; }
        //finally { Qr = null; }
    }

    protected void cmdUpdateFlight_Click(object source, EventArgs e)
    {
        string msgmodal = "";
        BLL_TRV_QuoteRequest Qr = new BLL_TRV_QuoteRequest();
        int FlightID = Convert.ToInt32(Request.Form["hdFlightID"].ToString());
        try
        {

            if (txtAirlineLocator.Text.Trim() != "" && txtFlight.Text.Trim() != "" && txtDeparureDate.Text.Trim() != "" &&
                txtFrom.Text.Trim() != "" && txtTo.Text.Trim() != "")
            {
                Qr.UpdateQuoteFlight(FlightID, txtAirlineLocator.Text.Trim(), txtFrom.Text.Trim(), txtTo.Text.Trim(),
                   "", txtFlight.Text.Trim(), cmbTravelClass.SelectedValue, Convert.ToDateTime(txtDeparureDate.Text.Trim()),
                   Convert.ToDateTime(txtArrivalDate.Text.Trim()), cmbFlightStatus.SelectedValue,
                   txtFlightRemark.Text.Trim(), Convert.ToInt32(Session["USERID"].ToString()),
                   cmbArrHours.SelectedItem.Text, cmbArrMins.SelectedItem.Text,
                   cmbDepHours.SelectedItem.Text, cmbDepMins.SelectedItem.Text);

                GetQuotations();
                ClearBoxes();
            }


            msgmodal = String.Format("hideModal('dvQuote');");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "closedvQuoteedit", msgmodal, true);
        }
        catch { }
        finally { Qr = null; }
    }

    private void ClearBoxes()
    {
        try
        {
            txtFlight.Text = ""; txtFrom.Text = ""; txtTo.Text = "";
            txtDeparureDate.Text = ""; txtArrivalDate.Text = ""; txtAirlineLocator.Text = ""; txtFlightRemark.Text = "";
            txtFare.Text = ""; txtTax.Text = ""; txtGDSLocator.Text = ""; txtTicketDeadline.Text = "";
            cmbDepMins.SelectedValue = "00"; cmbDepHours.SelectedValue = "00";
            cmbArrHours.SelectedValue = "00"; cmbArrMins.SelectedValue = "00";
            cmbHours.SelectedValue = "00"; cmbMins.SelectedValue = "00";
        }
        catch { }
    }



    protected void btnAddQuotation_Click(object sender, EventArgs e)
    {

        var url = "AddQuotation.aspx?requestid=" + RequestID + "&currency=" + cmbCurrency.SelectedValue + "&IsSeaman=" + hdfIsSeaman.Value + "&SUPPLIER_ID=" + Request.QueryString["SUPPLIER_ID"];


        string msg1 = string.Format("OpenPopupWindow('AddQuotation', 'Add Quotation','" + url + "', 'popup', 650, 1000, null, null, true, false, true, newQuote_Closed);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg1", msg1, true);

    }


    protected void btnHiddenSubmit_Click(object sender, EventArgs e)
    {
        GetQuotations();
    }


}