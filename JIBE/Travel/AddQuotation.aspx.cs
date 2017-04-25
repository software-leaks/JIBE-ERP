using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//custome defined libararies
using SMS.Business.TRAV;
using System.Text;
using SMS.Business.Infrastructure;

using System.IO;
using System.Threading.Tasks;


using SMS.Properties;
//public class AirDistance
//{
//    public int processingDurationMillis { get; set; }
//    public bool authorisedAPI { get; set; }
//    public bool success { get; set; }
//    public string airline { get; set; }
//    public string errorMessage { get; set; }
//    public string distance { get; set; }
//    public string units { get; set; }
//}

public class IATAList
{
    public string fromIATA { get; set; }
    public string toIATA { get; set; }
}
public partial class Travel_AddQuotation : System.Web.UI.Page
{
    //BLL_Infra_AirportDistance objAdis = new BLL_Infra_AirportDistance();
    //AirDistance dis = new AirDistance();
    public List<IATAList> dtIata = new List<IATAList>();

   
    int RequestID, AgentID;
    protected void Page_Load(object sender, EventArgs e)
    {
        lblerrorMsg.Visible = false;
        try
        {
            lblSeamanStatus.Text = UDFLib.ConvertStringToNull(Request.QueryString["IsSeaman"]) != null ? "SEAMAN TICKET" : "NOT A SEAMAN TICKET";
            lblSeamanStatus.ForeColor = UDFLib.ConvertStringToNull(Request.QueryString["IsSeaman"]) != null ? System.Drawing.Color.Blue : System.Drawing.Color.Red;
           
          

            if (Request.QueryString["RequestID"] != null)
            {
                RequestID = Convert.ToInt32(Request.QueryString["RequestID"].ToString());


            }
            else
            {
                Response.Write("<center style='color:red;'>REQUEST_ID_ERROR<br />There is an error while displaying this page, Please contact XT for more information.</center>");
                Response.End();
            }


            AgentID = Convert.ToInt32(Request.QueryString["SUPPLIER_ID"]);

            if (!Page.IsPostBack)
            {
                for (int time = 0; time < 24; time++)
                {


                    cmbHours.Items.Add(new ListItem((time < 10) ? "0" + time.ToString() : time.ToString(), time.ToString()));
                }

                for (int mins = 0; mins < 60; mins ++)
                {
                    cmbMins.Items.Add(new ListItem((mins < 10) ? "0" + mins.ToString() : mins.ToString(), mins.ToString()));

                }


                BLL_Infra_Currency objCurr = new BLL_Infra_Currency();
                cmbCurrency.DataTextField = "Currency_Code";
                cmbCurrency.DataValueField = "Currency_Code";
                cmbCurrency.DataSource = objCurr.Get_CurrencyList();
                cmbCurrency.DataBind();
                ListItem lis = new ListItem("-Select Currency-", "");
                cmbCurrency.Items.Insert(0, lis);

                if (!string.IsNullOrEmpty(Request.QueryString["QuoteID"]))
                {
                    Bind_Quote_Details();
                }
                else
                {
                    cmbCurrency.SelectedValue = Request.QueryString["currency"];

                    MakeFlightList();
                }


            }
        }
        catch { }
    }


    private DataTable CreateDtFlight()
    {
        DataTable dtFlight = new DataTable();

        DataColumn pkCol = new DataColumn("ID");
        dtFlight.Columns.Add(pkCol);
        dtFlight.Columns.Add("Flight");
        dtFlight.Columns.Add("From");
        dtFlight.Columns.Add("To");
        dtFlight.Columns.Add("FlightStatus");
        dtFlight.Columns.Add("DepartureDate");
        dtFlight.Columns.Add("DepHour");
        dtFlight.Columns.Add("DepMin");
        dtFlight.Columns.Add("ArrivalDate");
        dtFlight.Columns.Add("ArrHour");
        dtFlight.Columns.Add("ArrMin");
        dtFlight.Columns.Add("Locator");
        dtFlight.Columns.Add("TravelClass");
        dtFlight.Columns.Add("Remarks");
        dtFlight.PrimaryKey = new DataColumn[] { pkCol };
        return dtFlight;
    }




    protected void GrdFlight_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {

            if (GrdFlight.Rows.Count > 1)
            {
                DataTable dtFlight = CreateDtFlight(); ;

                DataRow dr = null;

                foreach (GridViewRow grFlt in GrdFlight.Rows)
                {
                    dr = dtFlight.NewRow();

                    dr["ID"] = GrdFlight.DataKeys[grFlt.RowIndex].Values["ID"].ToString();
                    dr["Flight"] = (grFlt.FindControl("txtFlight") as TextBox).Text;
                    dr["From"] = (grFlt.FindControl("txtFrom") as TextBox).Text;
                    dr["To"] = (grFlt.FindControl("txtTo") as TextBox).Text;
                    dr["FlightStatus"] = (grFlt.FindControl("cmbFlightStatus") as DropDownList).SelectedValue;
                    dr["DepartureDate"] = (grFlt.FindControl("txtDeparureDate") as TextBox).Text;
                    dr["DepHour"] = (grFlt.FindControl("cmbDepHours") as DropDownList).SelectedItem.Value;
                    dr["DepMin"] = (grFlt.FindControl("cmbDepMins") as DropDownList).SelectedItem.Value;
                    dr["ArrivalDate"] = (grFlt.FindControl("txtArrivalDate") as TextBox).Text;
                    dr["ArrHour"] = (grFlt.FindControl("cmbArrHours") as DropDownList).SelectedItem.Value;
                    dr["ArrMin"] = (grFlt.FindControl("cmbArrMins") as DropDownList).SelectedItem.Value;
                    dr["Locator"] = (grFlt.FindControl("txtAirlineLocator") as TextBox).Text;
                    dr["Remarks"] = (grFlt.FindControl("txtFlightRemark") as TextBox).Text;
                    dr["TravelClass"] = (grFlt.FindControl("cmbTravelClass") as DropDownList).SelectedValue;

                    dtFlight.Rows.Add(dr);
                }

                dr = dtFlight.Rows.Find(e.Keys["ID"].ToString());

                if (dr != null)
                {
                    if (!dr["id"].ToString().Contains("-"))
                    {
                        BLL_TRV_QuoteRequest qr = new BLL_TRV_QuoteRequest();
                        qr.DeleteQuoteFlight(Convert.ToInt32(dr["id"]), Convert.ToInt32(Session["USERID"].ToString()));
                    }
                    dtFlight.Rows.Remove(dr);
                }

                GrdFlight.DataSource = dtFlight;
                GrdFlight.DataBind();
            }
            //ViewState["DataTable"] = dtFlight;
        }
        catch { }
    }

    protected void cmdAddFlight_Click(object source, EventArgs e)
    {
        AddToDataTable();

    }

    private void MakeFlightList()
    {
        try
        {
            BLL_TRV_QuoteRequest Qr = new BLL_TRV_QuoteRequest();


            DataTable dtFlight = CreateDtFlight();
            DataRow dr = dtFlight.NewRow();
            DataSet dsReqst = Qr.Get_Request_Ticket_Details(Convert.ToInt32(Request.QueryString["RequestID"].ToString()));

            dr["ID"] = System.Guid.NewGuid().ToString();
            dr["FlightStatus"] = "Confirm";
            if (dsReqst.Tables[0].Rows.Count > 0)
            {
                dr["DepartureDate"] = dsReqst.Tables[0].Rows[0]["DepartureDate"].ToString();
                dr["TravelClass"] = dsReqst.Tables[0].Rows[0]["classOfTravel"].ToString();
                dr["ArrivalDate"] = dsReqst.Tables[0].Rows[0]["DepartureDate"].ToString();
            }

            if (dsReqst.Tables[1].Rows.Count > 0)
            {
                dr["From"] = dsReqst.Tables[1].Rows[0]["travelOrigin"].ToString();
                dr["To"] = dsReqst.Tables[1].Rows[0]["travelDestination"].ToString();
            }

            dr["TravelClass"] = "Economy";
            dr["FlightStatus"] = "Confirm";
            dr["ArrHour"] = "0";
            dr["ArrMin"] = "0";
            dr["DepHour"] = "0";
            dr["DepMin"] = "0";
            dtFlight.Rows.Add(dr);
            // ViewState["DataTable"] = dtFlight;
            GrdFlight.DataSource = dtFlight;
            GrdFlight.DataBind();

        }
        catch { }
    }

    private void AddToDataTable()
    {
        try
        {
            DataTable dtFlight = CreateDtFlight();

            DataRow dr = null;

            foreach (GridViewRow grFlt in GrdFlight.Rows)
            {
                dr = dtFlight.NewRow();

                dr["ID"] = GrdFlight.DataKeys[grFlt.RowIndex].Values["ID"].ToString();
                dr["Flight"] = (grFlt.FindControl("txtFlight") as TextBox).Text;
                dr["From"] = (grFlt.FindControl("txtFrom") as TextBox).Text;
                dr["To"] = (grFlt.FindControl("txtTo") as TextBox).Text;
                dr["FlightStatus"] = (grFlt.FindControl("cmbFlightStatus") as DropDownList).SelectedValue;
                dr["DepartureDate"] = (grFlt.FindControl("txtDeparureDate") as TextBox).Text;
                dr["DepHour"] = (grFlt.FindControl("cmbDepHours") as DropDownList).SelectedItem.Value;
                dr["DepMin"] = (grFlt.FindControl("cmbDepMins") as DropDownList).SelectedItem.Value;
                dr["ArrivalDate"] = (grFlt.FindControl("txtArrivalDate") as TextBox).Text;
                dr["ArrHour"] = (grFlt.FindControl("cmbArrHours") as DropDownList).SelectedItem.Value;
                dr["ArrMin"] = (grFlt.FindControl("cmbArrMins") as DropDownList).SelectedItem.Value;
                dr["Locator"] = (grFlt.FindControl("txtAirlineLocator") as TextBox).Text;
                dr["Remarks"] = (grFlt.FindControl("txtFlightRemark") as TextBox).Text;
                dr["TravelClass"] = (grFlt.FindControl("cmbTravelClass") as DropDownList).SelectedValue;

                dtFlight.Rows.Add(dr);
            }

            dr = dtFlight.NewRow();
            dr["ID"] = System.Guid.NewGuid().ToString();
            dr["TravelClass"] = "Economy";
            dr["FlightStatus"] = "Confirm";
            dr["ArrHour"] = "0";
            dr["ArrMin"] = "0";
            dr["DepHour"] = "0";
            dr["DepMin"] = "0";
            dtFlight.Rows.Add(dr);

            GrdFlight.DataSource = dtFlight;
            GrdFlight.DataBind();

            // ViewState["DataTable"] = dtFlight;

        }
        catch { }
    }



    protected void cmdSave_Click(object sender, EventArgs e)
    {
        BLL_TRV_QuoteRequest Qr = new BLL_TRV_QuoteRequest();
        IATAList lst = new IATAList();
        int QuoteID = 0; ;
        try
        {


            if (txtFare.Text.Trim() != "" && txtTax.Text.Trim() != "" && txtTicketDeadline.Text.Trim() != "" && txtGDSLocator.Text.Trim() != "")
            {
                if (!string.IsNullOrEmpty(Request.QueryString["QuoteID"]))
                    QuoteID = Convert.ToInt32(Request.QueryString["QuoteID"]);

                QuoteID = Qr.AddQuotation(RequestID, AgentID, txtGDSLocator.Text.Trim(),
                        Convert.ToDateTime(txtTicketDeadline.Text.Trim()),
                        Convert.ToDecimal(txtFare.Text.Trim()), Convert.ToDecimal(txtTax.Text.Trim()),
                        txtPNRText.Text.Trim(), cmbCurrency.SelectedValue, Convert.ToInt32(Session["USERID"].ToString()),
                        cmbHours.SelectedItem.Text, cmbMins.SelectedItem.Text, QuoteID,
                        UDFLib.ConvertDecimalToNull(txtbaggageallowances.Text),
                        UDFLib.ConvertDecimalToNull(txtDateChangeAllow.Text),
                        UDFLib.ConvertDecimalToNull(txtCancellationCharge.Text));
                int cnt=0;
                foreach (GridViewRow grFlt in GrdFlight.Rows)
                {

                    int Flight_ID = GrdFlight.DataKeys[grFlt.RowIndex].Values["ID"].ToString().Contains("-") ? 0 : Convert.ToInt32(GrdFlight.DataKeys[grFlt.RowIndex].Values["ID"].ToString());

                    Qr.AddQuotationFlights(QuoteID,
                        (grFlt.FindControl("txtAirlineLocator") as TextBox).Text,
                       (grFlt.FindControl("txtFrom") as TextBox).Text,
                        (grFlt.FindControl("txtTo") as TextBox).Text,
                        "",
                         (grFlt.FindControl("txtFlight") as TextBox).Text,
                       (grFlt.FindControl("cmbTravelClass") as DropDownList).SelectedValue,
                        Convert.ToDateTime((grFlt.FindControl("txtDeparureDate") as TextBox).Text),
                        Convert.ToDateTime((grFlt.FindControl("txtArrivalDate") as TextBox).Text),
                        (grFlt.FindControl("cmbFlightStatus") as DropDownList).SelectedValue,
                       (grFlt.FindControl("txtFlightRemark") as TextBox).Text,
                        UDFLib.ConvertToInteger(Session["USERID"].ToString()),
                       (grFlt.FindControl("cmbArrHours") as DropDownList).SelectedItem.Value,
                        (grFlt.FindControl("cmbArrMins") as DropDownList).SelectedItem.Value,
                        (grFlt.FindControl("cmbDepHours") as DropDownList).SelectedItem.Value,
                        (grFlt.FindControl("cmbDepMins") as DropDownList).SelectedItem.Value,
                        Flight_ID);

                 
                  
                    lst.fromIATA=(grFlt.FindControl("txtFrom") as TextBox).Text;
                    lst.toIATA= (grFlt.FindControl("txtTo") as TextBox).Text;



              
                   
                    

                    cnt++;
                }
              

            }

            string js = "parent.newQuote_Closed();parent.hideModal('dvPopUp');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "closewindow", js, true);


           
        }
        catch { throw; }
        finally { Qr = null; }
    }
    //public void GetAirportDistance(string FromIata, string ToIata)
    //{

    //    using (var client = new HttpClient())
    //    {



    //        client.BaseAddress = new Uri("https://airport.api.aero/");
    //        client.DefaultRequestHeaders.Accept.Clear();
    //        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-javascript"));

    //        // HTTP GET
    //        HttpResponseMessage response = client.GetAsync("airport/distance/" + FromIata + "/" + ToIata + "?user_key=936403a9f70ff2d3d785913a3d618227&units=km ").Result;

    //        if (response.IsSuccessStatusCode)
    //        {
    //            // Task<Stream> str = response.Content.ReadAsStreamAsync();
    //            //Console.WriteLine("{0}\t", str.ToString());
    //            dis = JsonConvert.DeserializeObject<AirDistance>(response.Content.ReadAsStringAsync().Result.Split('(')[1].Split(')')[0]);
    //        }


    //        // return dis.distance ;
    //    }
    //}
    protected void GrdFlight_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            try
            {
                (e.Row.FindControl("cmbDepHours") as DropDownList).Items.FindByValue(DataBinder.Eval(e.Row.DataItem, "DepHour").ToString()).Selected = true;
                (e.Row.FindControl("cmbDepMins") as DropDownList).Items.FindByValue(DataBinder.Eval(e.Row.DataItem, "DepMin").ToString()).Selected = true;
                (e.Row.FindControl("cmbArrHours") as DropDownList).Items.FindByValue(DataBinder.Eval(e.Row.DataItem, "ArrHour").ToString()).Selected = true;
                (e.Row.FindControl("cmbArrMins") as DropDownList).Items.FindByValue(DataBinder.Eval(e.Row.DataItem, "ArrMin").ToString()).Selected = true;
            }
            catch { }
        }
    }



    protected void btnFillFlightDataForAmadeus_Click(object s, EventArgs e)
    {
        try
        {
            string js = "HideInsertDataFromAgents();";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "closewindow-overlay", js, true);

            string[] arrFlightDetails = txtAmadeusContent.Text.Split('\n');

            StringBuilder strFlight = new StringBuilder(), strFrom = new StringBuilder(), strTo = new StringBuilder(), StrDeptDt = new StringBuilder(), strArrDt = new StringBuilder(), strClass = new StringBuilder(), strStatus = new StringBuilder();
            StringBuilder DepDT_Time = new StringBuilder();
            StringBuilder ArrDT_Time = new StringBuilder();
            StringBuilder Locator = new StringBuilder();

            DataTable dtFlight = CreateDtFlight();

            DataRow dr = null;

            foreach (GridViewRow grFlt in GrdFlight.Rows)
            {
                if ((grFlt.FindControl("txtFlight") as TextBox).Text.Trim() != "")
                {
                    dr = dtFlight.NewRow();

                    dr["ID"] = GrdFlight.DataKeys[grFlt.RowIndex].Values["ID"].ToString();
                    dr["Flight"] = (grFlt.FindControl("txtFlight") as TextBox).Text;
                    dr["From"] = (grFlt.FindControl("txtFrom") as TextBox).Text;
                    dr["To"] = (grFlt.FindControl("txtTo") as TextBox).Text;
                    dr["FlightStatus"] = (grFlt.FindControl("cmbFlightStatus") as DropDownList).SelectedValue;
                    dr["DepartureDate"] = (grFlt.FindControl("txtDeparureDate") as TextBox).Text;
                    dr["DepHour"] = (grFlt.FindControl("cmbDepHours") as DropDownList).SelectedItem.Value;
                    dr["DepMin"] = (grFlt.FindControl("cmbDepMins") as DropDownList).SelectedItem.Value;
                    dr["ArrivalDate"] = (grFlt.FindControl("txtArrivalDate") as TextBox).Text;
                    dr["ArrHour"] = (grFlt.FindControl("cmbArrHours") as DropDownList).SelectedItem.Value;
                    dr["ArrMin"] = (grFlt.FindControl("cmbArrMins") as DropDownList).SelectedItem.Value;
                    dr["Locator"] = (grFlt.FindControl("txtAirlineLocator") as TextBox).Text;
                    dr["Remarks"] = (grFlt.FindControl("txtFlightRemark") as TextBox).Text;
                    dr["TravelClass"] = (grFlt.FindControl("cmbTravelClass") as DropDownList).SelectedValue;

                    dtFlight.Rows.Add(dr);
                }
            }


            foreach (string strSingleFlight in arrFlightDetails)
            {
                strFlight.Clear();
                strFrom.Clear();
                strTo.Clear();
                StrDeptDt.Clear();
                strArrDt.Clear();
                strClass.Clear();
                strStatus.Clear();
                DepDT_Time.Clear();
                ArrDT_Time.Clear();
                Locator.Clear();

                if (strSingleFlight.Trim() != "")
                {
                    int i = 0;
                    foreach (char ch in strSingleFlight.Trim())
                    {
                        if (i < 8)
                        {
                            strFlight.Append(ch);
                        }
                        else if (i > 8 && i < 14)
                        {
                            StrDeptDt.Append(ch);
                        }
                        else if (i > 16 && i < 20)
                        {
                            strFrom.Append(ch);
                        }
                        else if (i > 19 && i < 23)
                        {
                            strTo.Append(ch);
                        }
                        else if (i == 23)
                        {
                            string fltsts = strSingleFlight[24].ToString() + strSingleFlight[25].ToString();
                            if (fltsts == "HK")
                                strStatus.Append("Confirm");
                            else
                                strStatus.Append("Waitlist");
                        }
                        else if (i > 28 && i < 33)
                        {
                            DepDT_Time.Append(ch);
                        }
                        else if (i > 33 && i < 38)
                        {
                            ArrDT_Time.Append(ch);
                        }
                        else if (i > 39 && i < 45)
                        {
                            strArrDt.Append(ch);
                        }
                        else if (i == 47)
                        {
                            if (ch.ToString() == "E")
                                strClass.Append("Economy");
                        }
                        else if (i > 49 && i <= 59)
                        {
                            Locator.Append(ch);
                        }


                        i++;
                    }

                    // create a new row in datatable
                    dr = dtFlight.NewRow();
                    dr["ID"] = System.Guid.NewGuid().ToString();
                    dr["TravelClass"] = strClass.ToString();
                    dr["FlightStatus"] = strStatus.ToString();
                    dr["ArrHour"] = int.Parse(ArrDT_Time.ToString().Substring(0, 2));
                    dr["ArrMin"] = int.Parse(ArrDT_Time.ToString().Substring(2, 2));
                    dr["DepHour"] = int.Parse(DepDT_Time.ToString().Substring(0, 2));
                    dr["DepMin"] = int.Parse(DepDT_Time.ToString().Substring(2, 2));
                    dr["Flight"] = strFlight.ToString();
                    dr["From"] = strFrom.ToString();
                    dr["To"] = strTo.ToString();
                    dr["DepartureDate"] = StrDeptDt.ToString().Substring(0, 2) + "-" + StrDeptDt.ToString().Substring(2, 3) + "-" + DateTime.Now.Year.ToString();
                    dr["ArrivalDate"] = strArrDt.ToString().Substring(0, 2) + "-" + strArrDt.ToString().Substring(2, 3) + "-" + DateTime.Now.Year.ToString();
                    dr["Locator"] = Locator.ToString();

                    dtFlight.Rows.Add(dr);
                }

            }

            if (dtFlight.Rows.Count > 0)
            {
                GrdFlight.DataSource = dtFlight;
                GrdFlight.DataBind();
            }
        }
        catch
        {
            MakeFlightList();
            lblerrorMsg.Visible = true;
            lblerrorMsg.Text = "Segment related data population failed. Please enter data in correct format or enter segment data manually.";

        }
    }


    protected void Bind_Quote_Details()
    {
        BLL_TRV_QuoteRequest objRFQ = new BLL_TRV_QuoteRequest();
        DataSet dsQuoteDetails = objRFQ.Get_Quotation_Details(Convert.ToInt32(Request.QueryString["RequestID"]), Convert.ToInt32(Request.QueryString["QuoteID"]));

        if (dsQuoteDetails.Tables.Count > 1)
        {

            dsQuoteDetails.Tables[1].PrimaryKey = new DataColumn[] { dsQuoteDetails.Tables[1].Columns["id"] };

            GrdFlight.DataSource = dsQuoteDetails.Tables[1];
            GrdFlight.DataBind();

        }
        if (dsQuoteDetails.Tables[0].Rows.Count > 0)
        {
            txtGDSLocator.Text = dsQuoteDetails.Tables[0].Rows[0]["GDSLocator"].ToString();
            txtTicketDeadline.Text = dsQuoteDetails.Tables[0].Rows[0]["TicketingDeadline"].ToString();
            cmbHours.Items.FindByValue(dsQuoteDetails.Tables[0].Rows[0]["TimeHours"].ToString()).Selected = true;
            cmbMins.Items.FindByValue(dsQuoteDetails.Tables[0].Rows[0]["TimeMins"].ToString()).Selected = true;
            txtFare.Text = dsQuoteDetails.Tables[0].Rows[0]["Fare"].ToString();
            txtTax.Text = dsQuoteDetails.Tables[0].Rows[0]["Tax"].ToString();
            txtPNRText.Text = dsQuoteDetails.Tables[0].Rows[0]["Remarks"].ToString();
            cmbCurrency.Items.FindByValue(dsQuoteDetails.Tables[0].Rows[0]["currency"].ToString()).Selected = true;
            txtbaggageallowances.Text = dsQuoteDetails.Tables[0].Rows[0]["Baggage_Charge"].ToString();
            txtCancellationCharge.Text = dsQuoteDetails.Tables[0].Rows[0]["Cancellation_Charge"].ToString();
            txtDateChangeAllow.Text = dsQuoteDetails.Tables[0].Rows[0]["Date_Change_Charge"].ToString();


        }
    }
}