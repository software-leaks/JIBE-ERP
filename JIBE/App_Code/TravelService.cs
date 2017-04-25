using System;
using System.Data;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
//custome defined libaries
using SMS.Business.TRAV;
using SMS.Properties;
using SMS.Business.Crew;

/// <summary>
/// Summary description for TravelService
/// </summary>
[WebService(Namespace = "http://www.codeproject.com/soap/MTOMWebServices.asp")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class TravelService : System.Web.Services.WebService
{
    public TravelService()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string GetQuoteAgent(int RequestId, int Quoted)
    {
        BLL_TRV_TravelRequest TRequest = new BLL_TRV_TravelRequest();
        DataTable dt;
        StringBuilder sTable = new StringBuilder();
        try
        {
            dt = TRequest.GetQuoteAgents(RequestId, Quoted);
            return UDFLib.CreateHtmlTableFromDataTable(dt, new string[] { }, new string[] { "full_name" }, "Name :");

        }
        catch { throw; }
        finally { TRequest = null; }
    }


    [WebMethod]
    public string GetVesselPortCall(int Vessel_ID)
    {
        BLL_Crew_CrewDetails objCrew = new BLL_Crew_CrewDetails();
        DataTable dt;
        StringBuilder sTable = new StringBuilder();
        try
        {
            dt = objCrew.Get_PortCall_List(Vessel_ID);
            return UDFLib.CreateHtmlTableFromDataTable(dt, new string[] { "Port Name", "Arrival", "Departure", "Owners Agent", "Charterers Agent" }, new string[] { "Port_Name", "Arrival", "Departure", "Owners_Agent", "Charterers_Agent" }, "Vessel Port Call");
        }
        catch { throw; }
        finally { dt = null; }
    }






    [WebMethod]
    public string GetPaxsName(int RequestId)
    {
        BLL_TRV_TravelRequest TRequest = new BLL_TRV_TravelRequest();
        DataTable dt;
        StringBuilder shtmlTable = new StringBuilder();
        try
        {
            dt = TRequest.Get_Pax_Users(RequestId);
            return UDFLib.CreateHtmlTableFromDataTable(dt, new string[] { "Code", "Name", "Rank", "Passport No.", "Seaman Book No." }, new string[] { "Staff_Code", "name", "Rank_Short_Name", "Passport_Number", "Seaman_Book_Number" }, "Pax Details:");


        }
        catch { throw; }
        finally { TRequest = null; }
    }

    [WebMethod]
    public string GetRoutInfo(int RequestId)
    {
        BLL_TRV_TravelRequest TRequest = new BLL_TRV_TravelRequest();
        DataTable dt;
        StringBuilder shtmlTable = new StringBuilder();
        try
        {
            dt = TRequest.GetRoutInfo(RequestId);
            return UDFLib.CreateHtmlTableFromDataTable(dt, new string[] { "From", "To", "Name" }, new string[] { "RouteFrom", "RouteTo", "Name" }, "Route / Person Information:");


        }
        catch { throw; }
        finally { TRequest = null; }
    }



    [WebMethod]
    public string CheckPersonTravelWithinFiveDays(string staffids)
    {

        BLL_TRV_TravelRequest TRequest = new BLL_TRV_TravelRequest();
        DataTable dt = TRequest.GetPersonNameToTravelWithinfiveDays(staffids);
        return UDFLib.CreateHtmlTableFromDataTable(dt, new string[] { "Name", "Route", "Date/Time", "Travelled" }, new string[] { "NAME", "F_ROUTE", "DEPARTURE_DATE", "ISTRAVELLED" }, "");

        //if (dt.Rows.Count > 0)
        //    return dt.Rows[0][0].ToString();
        //else
        //    return "";
    }

    [WebMethod]
    public string Get_Pending_Manager_Approval(string RequestID)
    {
        BLL_TRV_QuoteRequest TRequest = new BLL_TRV_QuoteRequest();
        return UDFLib.CreateHtmlTableFromDataTable(TRequest.Get_Pending_Manager_Approval(int.Parse(RequestID)), new string[] { "Pending With" }, new string[] { "name" }, "");
    }

    [WebMethod]
    public string[] GetAirlineList(string prefixText, int count)
    {
        BLL_TRV_Airline al = new BLL_TRV_Airline();
        DataTable dt;
        List<string> RetVal = new List<string>();

        try
        {
            dt = al.GetAirline(prefixText).Tables[0];

            dt.Rows.Cast<System.Data.DataRow>().Take(count);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                RetVal.Add(dt.Rows[i]["Airline_code"].ToString() + " - " + dt.Rows[i]["Airline_name"].ToString());
            }
            return RetVal.ToArray();
        }
        catch { throw; }
        finally { al = null; }
    }

    [WebMethod]
    public String[] GetAirportList(String prefixText, Int32 count)
    {
        BLL_TRV_Airport ap = new BLL_TRV_Airport();
        DataTable dt;
        List<string> RetVal = new List<string>();

        try
        {
            dt = ap.GetAirPort(prefixText).Tables[0];
            dt.Rows.Cast<System.Data.DataRow>().Take(count);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                RetVal.Add(dt.Rows[i]["iata_code"].ToString());
            }

            return RetVal.ToArray();
        }
        catch { throw; }
        finally { ap = null; }
    }

    [WebMethod]
    public string UPD_Request_Vessel(string ReqID, string VesselID, string UserID)
    {
        BLL_TRV_TravelRequest TRequest = new BLL_TRV_TravelRequest();
        return TRequest.UPD_Request_Vessel(int.Parse(ReqID), int.Parse(VesselID), int.Parse(UserID)).ToString();
    }

    [WebMethod]
    public string UPD_Request_DepartDate(string ReqID, string DeptDate, string UserID)
    {
        BLL_TRV_TravelRequest TRequest = new BLL_TRV_TravelRequest();
        return TRequest.UPD_Request_DeptDate(int.Parse(ReqID), DeptDate, int.Parse(UserID));
    }

    [WebMethod]
    public string asyncGetRemarks(string Request_ID, string Agent_ID)
    {
        BLL_TRV_TravelRequest TRequest = new BLL_TRV_TravelRequest();
        return UDFLib.CreateHtmlTableFromDataTable(TRequest.GetRemarks(int.Parse(Request_ID), int.Parse(Agent_ID)).Tables[0], new string[] { "User", "Date", "Remark" }, new string[] { "remark_By", "remarkDate", "remark" }, "");

    }

    [WebMethod]
    public string asyncAddRemarks(string Request_ID, string Remark, string UserID, string Agent_ID, string RemarkAgentIDs)
    {
        BLL_TRV_TravelRequest TRequest = new BLL_TRV_TravelRequest();
        return TRequest.AddRemarks(int.Parse(Request_ID), Remark, int.Parse(UserID), int.Parse(Agent_ID), RemarkAgentIDs).ToString();
    }

    [WebMethod]
    public string asyncGet_Agents_By_Request(string Request_ID)
    {
        BLL_TRV_QuoteRequest objQtnReq = new BLL_TRV_QuoteRequest();
        return UDFLib.CreateHtmlCheckBoxList("htmlchkremark", "hdfhtmlchkremarkcount", objQtnReq.Get_Agents_By_Request(int.Parse(Request_ID)), "SHORT_NAME", "id", "horizontal", 2, "htmlchkremark-css");
    }

    [WebMethod]
    public string asyncGet_PODetails_ByReqID(string Request_ID, string Supplier_Code)
    {
        BLL_TRV_QuoteRequest TRequest = new BLL_TRV_QuoteRequest();
        return UDFLib.CreateHtmlTableFromDataTable(TRequest.Get_PODetails_ByReqID(Request_ID, UDFLib.ConvertStringToNull(Supplier_Code)), new string[] { "PO", "Item Description", "Currency", "Amount" }, new string[] { "ORDER_CODE", "ITEM_SHORT_DESC", "CURRENCY", "AMOUNT" }, new string[] { "left", "left", "center", "right" }, "");
    }
    [WebMethod]
    public int asyncGet_Marked_IsTravelled(int Request_ID)
    {
        BLL_TRV_TravelRequest TRequest = new BLL_TRV_TravelRequest();
        return TRequest.Get_Marked_IsTravelled(Request_ID);
    }

}
