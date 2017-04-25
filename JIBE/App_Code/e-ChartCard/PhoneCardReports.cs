using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Specialized;
using System.Data;
using SMS.Business.PortageBill;

/// <summary>
/// Summary description for PhoneCardReports
/// </summary>
/// 
namespace SMS.Business.Reports
{
    public class PhoneCardReports
    {
        public PhoneCardReports()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public static DataTable GetReportData(NameValueCollection nvc, ref string reportfile, ref string filename)
        {
            if (nvc["reportcode"].ToString().Equals("CONSUMPTIONBYVESSEL"))
                return PhoneCardReports.ConsumptionGetVessel(nvc, ref reportfile, ref filename);
            if (nvc["reportcode"].ToString().Equals("CONSUMPTIONBYCREW"))
                return PhoneCardReports.ConsumptionGetCrew(nvc, ref reportfile, ref filename);
            if (nvc["reportcode"].ToString().Equals("CONSUMPTIONBYMONTH"))
                return PhoneCardReports.ConsumptionGetMonth(nvc, ref reportfile, ref filename);           
            ///To Do: Add other reporting functions required for other reports.

            throw new Exception("Report Function not implemented correctly. Requires Code to be checked and corrected");
        }
        public static DataTable ConsumptionGetVessel(NameValueCollection nvc, ref string reportfile, ref string filename)
        {
            DataTable dt = new DataTable();


            if (nvc["vesselid"] != string.Empty)
            {
                dt = BLL_PB_PhoneCard.PhoneCard_Consumption_GetCardByVessl(int.Parse(nvc["vesselid"].ToString()));
            }            
            reportfile = HttpContext.Current.Server.MapPath("VesselConsumptionReport.rpt");
            filename = "";// HttpContext.Current.Server.MapPath("../Attachments/Purchase/OrderForm/") + dt.Rows[0]["FLDFORMNO"].ToString().Replace("/", "-") + dt.Rows[0]["FLDVENDORCODE"].ToString().Replace("/", "-") + DateTime.Now.ToString("yyyyMMdd") + ".pdf";

            return dt;
        }
        public static DataTable ConsumptionGetCrew(NameValueCollection nvc, ref string reportfile, ref string filename)
        {
            DataTable dt = new DataTable();
           
            if (nvc["quotationid"] != string.Empty)
            {
                dt = BLL_PB_PhoneCard.PhoneCard_Consumption_GetCardByCrew(int.Parse(nvc["crewid"].ToString()));
            }
            reportfile = HttpContext.Current.Server.MapPath("CrewConsumptionReport.rpt");
            filename = "";// HttpContext.Current.Server.MapPath("../Attachments/Purchase/OrderForm/") + dt.Rows[0]["FLDFORMNO"].ToString().Replace("/", "-") + dt.Rows[0]["FLDVENDORCODE"].ToString().Replace("/", "-") + DateTime.Now.ToString("yyyyMMdd") + ".pdf";

            return dt;
        }
        public static DataTable ConsumptionGetMonth(NameValueCollection nvc, ref string reportfile, ref string filename)
        {
            DataTable dt = new DataTable();

            if (nvc["quotationid"] != string.Empty)
            {
                dt = BLL_PB_PhoneCard.PhoneCard_Consumption_GetCardByMonth(nvc["month"].ToString(), nvc["year"].ToString());
            }
            reportfile = HttpContext.Current.Server.MapPath("MonthConsumptionReport.rpt");
            filename = "";// HttpContext.Current.Server.MapPath("../Attachments/Purchase/OrderForm/") + dt.Rows[0]["FLDFORMNO"].ToString().Replace("/", "-") + dt.Rows[0]["FLDVENDORCODE"].ToString().Replace("/", "-") + DateTime.Now.ToString("yyyyMMdd") + ".pdf";

            return dt;
        }

      
    }
}