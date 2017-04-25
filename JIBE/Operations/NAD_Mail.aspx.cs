using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Crew;
using System.Net;
using System.Text;
using SMS.Business.Operation;
using System.Data;

public partial class Operations_NAD_Mail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DataTable dtReports = BLL_OPS_VoyageReports.Get_Telegram_ToMail();
        WebClient myClient = new WebClient();
        BLL_Crew_CrewDetails objMail = new BLL_Crew_CrewDetails();
        string MailHeader = "";

        foreach (DataRow dr in dtReports.Rows)
        {
            string ReportPageHTML = null;
            byte[] requestHTML;
            string currentPageUrl = Request.Url.ToString();
            UTF8Encoding utf8 = new UTF8Encoding();
            MailHeader = dr["Vessel_Name"].ToString() + " / " + DateTime.Now.ToString("yy") + DateTime.Now.ToString("MM") + DateTime.Now.ToString("dd") + " / ";

            if (dr["Telegram_Type"].ToString().ToUpper() == "N")
            {
                currentPageUrl = "http://" + HttpContext.Current.Request.Url.Host + HttpContext.Current.Request.ApplicationPath + "/Operations/NoonReport.aspx?id=" + dr["PKID"].ToString();
                MailHeader += "NOON";
            }
            else if (dr["Telegram_Type"].ToString().ToUpper() == "D")
            {
                currentPageUrl = "http://" + HttpContext.Current.Request.Url.Host + HttpContext.Current.Request.ApplicationPath + "/Operations/DepartureReport.aspx?id=" + dr["PKID"].ToString();
                MailHeader += "DEPARTURE";
            }
            else if (dr["Telegram_Type"].ToString().ToUpper() == "A")
            {
                currentPageUrl = "http://" + HttpContext.Current.Request.Url.Host + HttpContext.Current.Request.ApplicationPath + "/Operations/ArrivalReport.aspx?id=" + dr["PKID"].ToString();
                MailHeader += "ARRIVAL";
            }

            requestHTML = myClient.DownloadData(currentPageUrl);
            ReportPageHTML = utf8.GetString(requestHTML);

            if (dr["Telegram_Type"].ToString().ToUpper() == "N")
            {
                #region Noon report
                ReportPageHTML = ReportPageHTML.Replace("class='leafTD-data-left'", "style='width: 140px; height: 20px; padding: 0px 0px 0px 2px;  background-color: #cce499;text-align: center;'");
                ReportPageHTML = ReportPageHTML.Replace("class='leafTD-data'", "style='width: 140px;     height: 20px;    padding: 0px 0px 0px 0px; background-color: #cce499;text-align: left;'");
                ReportPageHTML = ReportPageHTML.Replace("class='leafTD-header-midsec'", "style=' width: 170px;height: 20px;padding: 0px 0px 0px 0px;text-align: left;'");
                ReportPageHTML = ReportPageHTML.Replace("class='leafTD-header'", "style=' width: 120px; height: 20px; padding: 0px 0px 0px 0px;text-align: left;'");
                ReportPageHTML = ReportPageHTML.Replace("class='leafTD-data-midsec'", "style='width: 115px; height: 20px; padding: 0px 0px 0px 0px; background-color: #cce499;text-align: right;'");
                ReportPageHTML = ReportPageHTML.Replace("class='leafTD-data-consmp'", "style='width: 120px;height: 20px;padding: 0px 0px 0px 0px; background-color: #cce499;text-align: right;border-right: solid 1px white; white-space: normal; line-height: normal;letter-spacing: normal;'");
                ReportPageHTML = ReportPageHTML.Replace("class='leafTR'", "style=' border-bottom-style: solid;border-bottom-color: White;border-bottom-width: 1px;'");
                #endregion noon report
            }
            else
            {

                #region Departure Report
                ReportPageHTML = ReportPageHTML.Replace("class='leafTR'", "style='border-bottom-style: solid; border-bottom-color: White; border-bottom-width: 1px;'");
                ReportPageHTML = ReportPageHTML.Replace("class='leafTD-header'", "style='width: 80px;height: 10px; padding: 0px 0px 0px 10px;text-align: left;'");
                ReportPageHTML = ReportPageHTML.Replace("class='leafTD-data'", "style='width: 100px;height: 10px; padding: 0px 0px 0px 0px;background-color: #cce499;text-align: left;'");
                #endregion
            }
            string[] strArray = ReportPageHTML.Split(new string[] { "<div" }, StringSplitOptions.None);
            ReportPageHTML = "<div " + strArray[8] + "<div> <div " + strArray[9];


           



        }
    }
}