using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Operation;
using System.Data;

public partial class Operations_NoonReport_Port : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["TelegramID"] != null && Request.QueryString["VesselID"] != null && Request.QueryString["ReportType"] != null)
            {
                string[] filters = Request.QueryString["filters"].Split('~');
                DataTable dtReports = BLL_ASM_OPS_VoyageReport.Get_VoyageReportIndex_ASM(Request.QueryString["ReportType"].ToString(), Convert.ToInt32(Request.QueryString["VesselID"].ToString()), Convert.ToInt32(filters[2]), filters[3], filters[4], Convert.ToInt32(filters[5]));
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
        DataSet ds = BLL_ASM_OPS_VoyageReport.Get_DailyNoonReport_DL_ASM(VesselID, TelegramID);
        DataTable dt = ds.Tables[0];

        string telegramhour = Convert.ToString(dt.Rows[0]["Telegram_Date_HH"]);
        if (!string.IsNullOrEmpty(telegramhour))
        {
            if (telegramhour.Length == 1)
                dt.Rows[0]["Telegram_Date_HH"] = "0" + dt.Rows[0]["Telegram_Date_HH"].ToString();
        }

        string telegramminutes = Convert.ToString(dt.Rows[0]["Telegram_Date_MI"]);
        if (!string.IsNullOrEmpty(telegramminutes))
        {
            if (telegramminutes.Length == 1)
                dt.Rows[0]["Telegram_Date_MI"] = "0" + dt.Rows[0]["Telegram_Date_MI"].ToString();
        }

        string etbhours = Convert.ToString(dt.Rows[0]["ETB_HH"]);
        if (!string.IsNullOrEmpty(etbhours))
        {
            if (etbhours.Length == 1)
                dt.Rows[0]["ETB_HH"] = "0" + etbhours;
        }

        string etbmin = Convert.ToString(dt.Rows[0]["ETB_MI"]);
        if (!string.IsNullOrEmpty(etbmin))
        {
            if (etbmin.Length == 1)
                dt.Rows[0]["ETB_MI"] = "0" + etbmin;
        }

        string etshours = Convert.ToString(dt.Rows[0]["ETD_HH"]);
        if (!string.IsNullOrEmpty(etbhours))
        {
            if (etshours.Length == 1)
                dt.Rows[0]["ETD_HH"] = "0" + etshours;
        }

        string etsmin = Convert.ToString(dt.Rows[0]["ETD_MI"]);
        if (!string.IsNullOrEmpty(etbmin))
        {
            if (etsmin.Length == 1)
                dt.Rows[0]["ETD_MI"] = "0" + etsmin;
        }

        dt.AcceptChanges();
        fvnoonreport.DataSource = dt;
        fvnoonreport.DataBind();


        GridView gdShifting = (GridView)fvnoonreport.FindControl("gdShifting");
        if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
        {
            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                string fromshifttime = Convert.ToString(ds.Tables[1].Rows[i]["SHIFTFROMTIME"]);
                string fromtimehh = "";
                string fromtimemm = "";

                string[] fromtime = fromshifttime.Split(':');
                if (fromtime.Length > 0)
                {
                    fromtimehh = Convert.ToString(fromtime[0]);
                    fromtimemm = Convert.ToString(fromtime[1]);
                }
                if (!string.IsNullOrEmpty(fromtimehh))
                {
                    if (fromtimehh.Length == 1)
                        fromtimehh = "0" + fromtimehh;
                }
                if (!string.IsNullOrEmpty(fromtimemm))
                {
                    if (fromtimemm.Length == 1)
                        fromtimemm = "0" + fromtimemm;
                }
                fromshifttime = fromtimehh + ":" + fromtimemm;
                ds.Tables[1].Rows[i]["SHIFTFROMTIME"] = fromshifttime;



                string Toshifttime = Convert.ToString(ds.Tables[1].Rows[i]["SHIFTTILLTIME"]);
                string totimehh = "";
                string totimemm = "";

                string[] totime = Toshifttime.Split(':');
                if (totime.Length > 0)
                {
                    totimehh = Convert.ToString(totime[0]);
                    totimemm = Convert.ToString(totime[1]);
                }
                if (!string.IsNullOrEmpty(totimehh))
                {
                    if (totimehh.Length == 1)
                        totimehh = "0" + totimehh;
                }
                if (!string.IsNullOrEmpty(totimemm))
                {
                    if (totimemm.Length == 1)
                        totimemm = "0" + totimemm;
                }
                Toshifttime = totimehh + ":" + totimemm;
                ds.Tables[1].Rows[i]["SHIFTTILLTIME"] = Toshifttime;



                ds.Tables[1].AcceptChanges();

            }
            gdShifting.DataSource = ds.Tables[1];
            gdShifting.DataBind();
        }

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