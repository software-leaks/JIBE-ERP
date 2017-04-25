using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.Operation;
using SMS.Business.Technical;
using System.IO;
using SMS.Business.Infrastructure;
using SMS.Properties;
using SMS.Business.Operations;
using System.Drawing;
using System.Diagnostics;
using EO.Pdf;
public partial class DeckLogBookDetails : System.Web.UI.Page
{
    int newtab = 0;
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/";
            hdnBaseURL.Value = baseUrl;
            if (Request.QueryString["DeckLogBookID"] != null)
            {
                hdnVesselID.Value = Request.QueryString["Vessel_ID"].ToString();
                hdnLogBookID.Value = Request.QueryString["DeckLogBookID"].ToString();

                BindDeckLogBookHeaderDetails();
                BindDeckLogBookDetails();
            }
            ViewState["newtabnumber"] = newtab;
        }
    }


    private void BindDeckLogBookHeaderDetails()
    {
        DataTable dt = BLL_OPS_DeckLog.Get_DeckLogBook_List(int.Parse(Request.QueryString["DeckLogBookID"].ToString()), int.Parse(Request.QueryString["Vessel_ID"].ToString()));
        if (dt.Rows.Count > 0)
        {

            lblVessel.Text = dt.Rows[0]["Vessel_Name"].ToString();
            lblFrom.Text = dt.Rows[0]["From_Port"].ToString();
            lblTo.Text = dt.Rows[0]["To_Port"].ToString();
            lblReportDate.Text = dt.Rows[0]["Report_Date"].ToString();
            // lblTo.Text = dt.Rows[0]["Remarks"].ToString();
            txtAdditionalRemark.Text = dt.Rows[0]["Remarks"].ToString();
            lblCurrentSetDrift.Text = dt.Rows[0]["Current_Set_Drift"].ToString();

            lblTotalEngineRevs.Text = dt.Rows[0]["Total_Engine_Revs"].ToString();

            lbltrueCourseNoon.Text = dt.Rows[0]["True_Course_Made_To_Noon"].ToString();

            lblDaysRun.Text = dt.Rows[0]["DistNauticalMiles_DaysRun"].ToString();
            lblTotal.Text = dt.Rows[0]["Dis_Nau_Miles_Total"].ToString();

            lbllati_Acnt.Text = dt.Rows[0]["Latitude_ByAcnt"].ToString();
            lbllati_Obs.Text = dt.Rows[0]["Latitude_Obs"].ToString();

            lblLong_Acnt.Text = dt.Rows[0]["Longitude_ByAcnt"].ToString();
            lblLong_Obs.Text = dt.Rows[0]["Longitude_Obs"].ToString();

            lblSteeringDay.Text = dt.Rows[0]["SteamingTime_Day"].ToString();
            lblSteeringDayToal.Text = dt.Rows[0]["SteamingTime_Total"].ToString();

            lblSpeedAvg.Text = dt.Rows[0]["Speed_Average"].ToString();
            lblSpeedTotalAvg.Text = dt.Rows[0]["Speed_TotalAvg"].ToString();

            lblDistperLog.Text = dt.Rows[0]["Distance_Per_Log"].ToString();
            lblErrPercent.Text = dt.Rows[0]["Error_Percent"].ToString();

            lblMagneticVariation.Text = dt.Rows[0]["MagneticVariation"].ToString();
            if (dt.Rows[0]["FromMidNightTill"].ToString()!="")
            lblFromMidnight.Text =  dt.Rows[0]["FromMidNightTill"].ToString();
            if (dt.Rows[0]["TillMidNight"].ToString() != "")
            lblTillMidnight.Text = dt.Rows[0]["TillMidNight"].ToString();

            txtSickList.Text = dt.Rows[0]["Sick_List"].ToString();

            //if (dt.Rows[0]["VslMasterID"].ToString() != "" && dt.Rows[0]["VslMasterID"].ToString() != "0")
            //{
            //    lnkMaster.Text = dt.Rows[0]["VslMasterName"].ToString();
            //    lnkMaster.NavigateUrl = "~/crew/crewdetails.aspx?ID=" + dt.Rows[0]["VslMasterID"].ToString();
            //    lnkMaster.Target = "_blank";
            //    imgMaster.ImageUrl = hdnBaseURL.Value + "uploads/CrewImages/" + dt.Rows[0]["PhotoUrl1"].ToString();
            //    lnkMaster.Visible = true;
            //    imgMaster.Visible = true;
            //}
            //else
            //{
            //    lnkMaster.Visible = false;
            //    imgMaster.Visible = false;
            //}

            //if (dt.Rows[0]["VslChiefOfficerID"].ToString() != "" && dt.Rows[0]["VslChiefOfficerID"].ToString() != "0")
            //{
            //    lnkChiefOfficer.Text = dt.Rows[0]["VslChiefOfficerName"].ToString();
            //    lnkChiefOfficer.NavigateUrl = "~/crew/crewdetails.aspx?ID=" + dt.Rows[0]["VslChiefOfficerID"].ToString();
            //    lnkChiefOfficer.Target = "_blank";
            //    imgChiefOfficer.ImageUrl = hdnBaseURL.Value + "uploads/CrewImages/" + dt.Rows[0]["PhotoUrl2"].ToString();
            //    lnkChiefOfficer.Visible = true;
            //    imgChiefOfficer.Visible = true;
            //}
            //else
            //{
            //    lnkChiefOfficer.Visible = false;
            //    imgChiefOfficer.Visible = false;
            //}

        }

    }


    private void BindDeckLogBookDetails()
    {
        DataTable dt = BLL_OPS_DeckLog.Get_DeckLogBook_Details(int.Parse(Request.QueryString["DeckLogBookID"].ToString()), int.Parse(Request.QueryString["Vessel_ID"].ToString()));
        if (dt.Rows.Count > 0)
        {

            dt.DefaultView.RowFilter = "Log_Hours_ID < 13";
            rpDeckLogBook01.DataSource = dt;
            rpDeckLogBook01.DataBind();

            dt.DefaultView.RowFilter = "Log_Hours_ID > 12";
            rpDeckLogBook02.DataSource = dt;
            rpDeckLogBook02.DataBind();
        }

        DataTable dt_wheel = BLL_OPS_DeckLog.Get_DeckLogBook_Wheel_Look_Out_Details(int.Parse(Request.QueryString["DeckLogBookID"].ToString()), int.Parse(Request.QueryString["Vessel_ID"].ToString()));
        if (dt.Rows.Count > 0)
        {
            rpWheelLookOut.DataSource = dt_wheel;
            rpWheelLookOut.DataBind();
        }

        DataTable dt_Wanter_Hold = BLL_OPS_DeckLog.Get_DeckLogBook_Water_In_Hold_Details(int.Parse(Request.QueryString["DeckLogBookID"].ToString()), int.Parse(Request.QueryString["Vessel_ID"].ToString()));
        if (dt_Wanter_Hold.Rows.Count > 0)
        {
            rpWaterInHold.DataSource = dt_Wanter_Hold;
            rpWaterInHold.DataBind();
        }

        DataTable dt_Wanter_Tank = BLL_OPS_DeckLog.Get_DeckLogBook_Water_In_Tank_Details(int.Parse(Request.QueryString["DeckLogBookID"].ToString()), int.Parse(Request.QueryString["Vessel_ID"].ToString()));
        if (dt_Wanter_Tank.Rows.Count > 0)
        {
            rpWaterInTank.DataSource = dt_Wanter_Tank;
            rpWaterInTank.DataBind();
        }


        DataTable dt_Incident = BLL_OPS_DeckLog.Get_DeckLogBook_Incident_Report_Search(int.Parse(Request.QueryString["DeckLogBookID"].ToString()), int.Parse(Request.QueryString["Vessel_ID"].ToString()));
        if (dt_Incident.Rows.Count > 0)
        {
            rpIncidentReport.DataSource = dt_Incident;
            rpIncidentReport.DataBind();

        }
    }


    protected void btnHiddenSubmit_Click(object sender, EventArgs e)
    {
        BindDeckLogBookDetails();
    }

    protected void BtnPrintPDF_Click(object sender, EventArgs e)
    {

        EO.Pdf.HtmlToPdf.Options.PageSize = new SizeF(33.1f, 23.4f);

        PdfDocument doc = new PdfDocument();

        string GUID = Guid.NewGuid().ToString();
        string filePath = Server.MapPath("~/Uploads/Reports/" + GUID + ".pdf");
        //string FileName = "~/Uploads/Reports/" + GUID + ".pdf";

        EO.Pdf.Runtime.AddLicense("p+R2mbbA3bNoqbTC4KFZ7ekDHuio5cGz4aFZpsKetZ9Zl6TNHuig5eUFIPGe" +
      "tcznH+du5PflEuCG49jjIfewwO/o9dB2tMDAHuig5eUFIPGetZGb566l4Of2" +
      "GfKetZGbdePt9BDtrNzCnrWfWZekzRfonNzyBBDInbW6yuCwb6y9xtyxdabw" +
      "+g7kp+rp2g+9RoGkscufdePt9BDtrNzpz+eupeDn9hnyntzCnrWfWZekzQzr" +
      "peb7z7iJWZekscufWZfA8g/jWev9ARC8W7zTv/vjn5mkBxDxrODz/+ihb6W0" +
      "s8uud4SOscufWbOz8hfrqO7CnrWfWZekzRrxndz22hnlqJfo8h8=");
        // HtmlToPdf.Options.AfterRenderPage = new EO.Pdf.PdfPageEventHandler(On_AfterRenderPage);
        EO.Pdf.HtmlToPdf.Options.FooterHtmlFormat = "<div style='text-align:right; font-family:calibri; font-size:12px'>Page {page_number} of {total_pages}</div>";

        HtmlToPdf.Options.AutoFitX = HtmlToPdfAutoFitMode.None;
        //HtmlToPdf.Options.AutoFitY = HtmlToPdfAutoFitMode.None;


        // HtmlToPdf.Options.AutoAdjustForDPI=true;
        //  HtmlToPdf.Options.PageSize = EO.Pdf.PdfPageSizes.Letter;
        string TemplateText = hdnContent.Value;

        HtmlToPdf.ConvertHtml(TemplateText, filePath);

        newtab = UDFLib.ConvertToInteger(ViewState["newtabnumber"]);
        newtab++;
        ViewState["newtabnumber"] = newtab;
        ScriptManager.RegisterStartupScript(this, this.GetType(), "hideText", "window.open('../../Uploads/Reports/" + GUID + ".pdf','INSPRPT" + newtab + "');", true);  // (  this.GetType(), "OpenWindow", "window.open('../../Uploads/InspectionReport.pdf','_newtab');", true);
        //Response.Redirect("../../Uploads/Reports/" + GUID + ".pdf");

    }

}