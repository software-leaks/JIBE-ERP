using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using SMS.Business.TRAV;

public partial class Travel_eTicketlist : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            GetETickets();
        }
        catch { }
    }

    protected void GetETickets()
    {
        BLL_TRV_TravelRequest TRequest = new BLL_TRV_TravelRequest();
        try
        {
            grdTickets.DataSource = TRequest.Get_ETicket_By_RequestID(UDFLib.ConvertToInteger(Request.QueryString["requestid"].ToString()));
            grdTickets.DataBind();
        }
        catch { }
        finally { TRequest = null; }
    }
}