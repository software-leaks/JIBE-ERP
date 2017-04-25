using System;
using System.Web;
using System.Data;
using System.Web.Services;
using SMS.Business.TRAV;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Net;
using SMS.Business.Infrastructure;
using SMS.Properties;
using System.Web.Script.Serialization;

public partial class JibeWebService
{

    
    [WebMethod]
    public int UPD_Quote_Send_For_Approval(int QuoteID, int Status, int RequestID, int UserID)
    {
        BLL_TRV_TravelRequest objtr = new BLL_TRV_TravelRequest();
        return objtr.UPD_Quote_Send_For_Approval(QuoteID, Status, RequestID, UserID);

    }

    [WebMethod]
    public int Get_Quote_Count_Approval(int RequestID)
    {
        BLL_TRV_TravelRequest objtr = new BLL_TRV_TravelRequest();
        return objtr.Get_Quote_Count_Approval(RequestID);
    }
    public class tktBooked
    {

        public string MonthName { get; set; }

        public decimal? avgtkt { get; set; }

        public int? vslcount { get; set; }

        public int? totalticket { get; set; }

    }
    [WebMethod]
    public List<tktBooked> AsyncGet_tktBooked()
    {
        BLL_TRV_TravelRequest objtr = new BLL_TRV_TravelRequest();
        DataTable dt_tktBooked = objtr.Get_tktBooked();
        List<tktBooked> dataList = new List<tktBooked>();

        foreach (DataRow dtrow in dt_tktBooked.Rows)
        {

            tktBooked details = new tktBooked();

            details.MonthName = dtrow[1].ToString();

            details.avgtkt = UDFLib.ConvertDecimalToNull(dtrow[7]);

            details.vslcount = UDFLib.ConvertIntegerToNull(dtrow[5]);

            details.totalticket = UDFLib.ConvertIntegerToNull(dtrow[4]);

            dataList.Add(details);

        }
        return dataList;
    }
    public class tktByVessel
    {

        public string Vessel_Name { get; set; }

        public int? Count { get; set; }

    }
    [WebMethod]
    public List<tktByVessel> AsyncGet_tktByVessel()
    {
        BLL_TRV_TravelRequest objtr = new BLL_TRV_TravelRequest();
        DataTable dt_tktByVessel = objtr.Get_tktByVessel();
        List<tktByVessel> dataList = new List<tktByVessel>();

        foreach (DataRow dtrow in dt_tktByVessel.Rows)
        {

            tktByVessel details = new tktByVessel();

            details.Vessel_Name = dtrow[0].ToString();

            details.Count = UDFLib.ConvertIntegerToNull(dtrow[2]);

            dataList.Add(details);

        }
        return dataList;
    }
    public class AvgPricePerTicket
    {

        public string MonthName { get; set; }

        public decimal? avgAmt { get; set; }

    }
    [WebMethod]
    public List<AvgPricePerTicket> AsyncGet_AvgPricePerTicket()
    {
        BLL_TRV_TravelRequest objtr = new BLL_TRV_TravelRequest();
        DataTable dt_AvgPricePerTicket = objtr.Get_AvgPricePerTicket();
        List<AvgPricePerTicket> dataList = new List<AvgPricePerTicket>();

        foreach (DataRow dtrow in dt_AvgPricePerTicket.Rows)
        {

            AvgPricePerTicket details = new AvgPricePerTicket();

            details.MonthName = dtrow[1].ToString();

            details.avgAmt = UDFLib.ConvertDecimalToNull(dtrow[4]);

            dataList.Add(details);

        }
        return dataList;
    }
    public class TotalAmount
    {

        public string MonthName { get; set; }

        public decimal? totalAmount { get; set; }

    }
    [WebMethod]
    public List<TotalAmount> AsyncGet_TotalAmount()
    {
        BLL_TRV_TravelRequest objtr = new BLL_TRV_TravelRequest();
        DataTable dt_TotalAmount = objtr.Get_TotalAmount();
        List<TotalAmount> dataList = new List<TotalAmount>();

        foreach (DataRow dtrow in dt_TotalAmount.Rows)
        {

            TotalAmount details = new TotalAmount();

            details.MonthName = dtrow[1].ToString();

            details.totalAmount = UDFLib.ConvertDecimalToNull(dtrow[4]);

            dataList.Add(details);

        }
        return dataList;     
    }
}
