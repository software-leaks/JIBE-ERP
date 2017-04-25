using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.Operation;
using SMS.Business.OPS;

public partial class Operations_Departure_Report_Chem : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["TelegramID"] != null && Request.QueryString["VesselID"] != null && Request.QueryString["ReportType"] != null)
            {
                string[] filters = Request.QueryString["filters"].Split('~');
                DataTable dtReports = BLL_AXSG_OPS_VoyageReport_Chem.Get_VoyageReportIndex_AXSG(Request.QueryString["ReportType"].ToString(), Convert.ToInt32(Request.QueryString["VesselID"].ToString()), Convert.ToInt32(filters[2]), filters[3], filters[4], Convert.ToInt32(filters[5]));
                dtReports.PrimaryKey = new DataColumn[] { dtReports.Columns["TELEGRAM_ID"] };
                DataRow dr = dtReports.Rows.Find(decimal.Parse(Request.QueryString["TelegramID"].ToString()));
                BindReport(dr);
                BindFormViewWithData(Convert.ToInt32(Request.QueryString["VesselID"].ToString()), decimal.Parse(Request.QueryString["TelegramID"].ToString()));
            }
        }
        else
        {

        }
    }
    protected void BindReport(DataRow dr)
    {
        string TelegramID = dr != null ? dr["Telegram_ID"].ToString() : Request.QueryString["TelegramID"];
        string VesselID = dr != null ? dr["Vessel_ID"].ToString() : Request.QueryString["VesselID"];
        string ReportType = dr != null ? dr["Telegram_Type"].ToString() : Request.QueryString["ReportType"];

        string fullType = ReportType == "N" ? "NOON" : (ReportType == "A" ? "ARR" : (ReportType == "D" ? "DEP" : "PRP"));
        //lblPageTitle.Text = ReportType == "N" ? "Noon Report" : (ReportType == "A" ? "Arrival Report" : (ReportType == "D" ? "Departure Report" : "Purple Report"));

        if (!string.IsNullOrWhiteSpace(TelegramID) && !string.IsNullOrWhiteSpace(VesselID))
        {
            if (dr != null)
            {
                lblVessel.Text = dr["VESSEL_NAME"].ToString();
                hplcrewlist.NavigateUrl = "~/crew/CrewList_Print.aspx?vcode=" + Convert.ToString(dr["Vessel_Short_Name"]);
                Page.Title = dr["Vessel_Short_Name"].ToString() + " / " + fullType + " / " + dr["STRTELEGRAM_DATE"].ToString();
            }
            String msgretv = String.Format("showdetails('" + ReportType + "'," + TelegramID + "); LoadRecordInfo(" + TelegramID + ");");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgret6v", msgretv, true);
        }
    }
    protected void BindFormViewWithData(int VesselID, decimal TelegramID)
    {
        DataSet ds = BLL_AXSG_OPS_VoyageReport_Chem.Get_DailyNoonReport_DL_AXSG(VesselID, TelegramID);
        DataTable dt = ds.Tables[0];

        //COP
        string cophh = Convert.ToString(dt.Rows[0]["SSP_HH"]);
        if (!string.IsNullOrEmpty(cophh))
        {
            if (cophh.Length == 1)
                dt.Rows[0]["SSP_HH"] = "0" + dt.Rows[0]["SSP_HH"].ToString();
        }

        string copmm = Convert.ToString(dt.Rows[0]["SSP_HH"]);
        if (!string.IsNullOrEmpty(copmm))
        {
            if (copmm.Length == 1)
                dt.Rows[0]["SSP_HH"] = "0" + dt.Rows[0]["SSP_HH"].ToString();
        }

        //SBE
        string sbehh = Convert.ToString(dt.Rows[0]["SBE_HH"]);
        if (!string.IsNullOrEmpty(sbehh))
        {
            if (sbehh.Length == 1)
                dt.Rows[0]["SBE_HH"] = "0" + dt.Rows[0]["SBE_HH"].ToString();
        }

        string sbemi = Convert.ToString(dt.Rows[0]["SBE_MI"]);
        if (!string.IsNullOrEmpty(sbemi))
        {
            if (sbemi.Length == 1)
                dt.Rows[0]["SBE_MI"] = "0" + dt.Rows[0]["SBE_MI"].ToString();
        }


        //POB
        string pobhh = Convert.ToString(dt.Rows[0]["POB_HH"]);
        if (!string.IsNullOrEmpty(pobhh))
        {
            if (pobhh.Length == 1)
                dt.Rows[0]["POB_HH"] = "0" + dt.Rows[0]["POB_HH"].ToString();
        }

        string pobmi = Convert.ToString(dt.Rows[0]["POB_MI"]);
        if (!string.IsNullOrEmpty(pobmi))
        {
            if (pobmi.Length == 1)
                dt.Rows[0]["POB_MI"] = "0" + dt.Rows[0]["POB_MI"].ToString();
        }

        //ETA Next Port
        string etahh = Convert.ToString(dt.Rows[0]["ETA_Next_Port_HH"]);
        if (!string.IsNullOrEmpty(etahh))
        {
            if (etahh.Length == 1)
                dt.Rows[0]["ETA_Next_Port_HH"] = "0" + dt.Rows[0]["ETA_Next_Port_HH"].ToString();
        }
        string etami = Convert.ToString(dt.Rows[0]["ETA_Next_Port_MI"]);
        if (!string.IsNullOrEmpty(etami))
        {
            if (etami.Length == 1)
                dt.Rows[0]["ETA_Next_Port_MI"] = "0" + dt.Rows[0]["ETA_Next_Port_MI"].ToString();
        }

        if (string.IsNullOrEmpty(Convert.ToString(dt.Rows[0]["HO_BunkerSampleLandingStatus"])))
            dt.Rows[0]["HO_BunkerSampleLandingStatus"] = "-";
        if (string.IsNullOrEmpty(Convert.ToString(dt.Rows[0]["DieselOil_BunkerSampleLandingStatus"])))
            dt.Rows[0]["DieselOil_BunkerSampleLandingStatus"] = "-";
        if (string.IsNullOrEmpty(Convert.ToString(dt.Rows[0]["LubeOil_BunkerSampleLandingStatus"])))
            dt.Rows[0]["LubeOil_BunkerSampleLandingStatus"] = "-";

        dt.AcceptChanges();
        fvdepature.DataSource = dt;
        fvdepature.DataBind();

        if (dt.Rows[0]["Master"].ToString() != "" && dt.Rows[0]["Master"].ToString() != "0")
        {
            lnkMaster.Text = dt.Rows[0]["MasterDetail"].ToString();
            lnkMaster.NavigateUrl = "~/crew/crewdetails.aspx?ID=" + dt.Rows[0]["Master"].ToString();
            lnkMaster.Target = "_blank";
            imgMaster.ImageUrl = "../uploads/CrewImages/" + dt.Rows[0]["PhotoUrl1"].ToString();
            lnkMaster.Visible = true;
            imgMaster.Visible = true;
        }
        else
        {
            lnkMaster.Visible = false;
            imgMaster.Visible = false;
        }

        if (dt.Rows[0]["ChiefEngineer"].ToString() != "" && dt.Rows[0]["ChiefEngineer"].ToString() != "0")
        {
            lnkChiefEngineer.Text = dt.Rows[0]["CEDetail"].ToString();
            lnkChiefEngineer.NavigateUrl = "~/crew/crewdetails.aspx?ID=" + dt.Rows[0]["ChiefEngineer"].ToString();
            lnkChiefEngineer.Target = "_blank";
            imgChiefEngineer.ImageUrl = "../uploads/CrewImages/" + dt.Rows[0]["PhotoURL2"].ToString();
            lnkChiefEngineer.Visible = true;
            imgChiefEngineer.Visible = true;
        }
        else
        {
            lnkChiefEngineer.Visible = false;
            imgChiefEngineer.Visible = false;
        }
        
    }
}