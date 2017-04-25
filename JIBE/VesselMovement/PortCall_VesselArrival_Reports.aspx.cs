using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Text;
using System.IO;
using System.Web.UI.WebControls;
using SMS.Business.Infrastructure;
using SMS.Business.VM;
using System.Data;
using System.Web.UI.HtmlControls;
using SMS.Properties;

public partial class VesselMovement_PortCall_Vessel_Reports : System.Web.UI.Page
{
    int VesselId = 0;
    BLL_VM_PortCall objPortCall = new BLL_VM_PortCall();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            if (Request.QueryString["ID"].ToString() != null)
            {
                VesselId = Convert.ToInt32(Request.QueryString["ID"].ToString());
                BindArrivalReport(VesselId);
            }
       
        }
    }


    protected  void  BindArrivalReport(int? VesselId)
    {
        DataTable dt = objPortCall.Get_Port_Call_Arrival_Report(VesselId);

        if (dt.Rows.Count > 0)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<table Style=\"Font-Family:Tahoma;Font-Size:11px;\" Cellpadding=3 Cellspacing=1 border=0 Bgcolor=Black\">");
            sb.Append("<tr Bgcolor=\"E8E8E8\">" + "<td Colspan=11>&nbsp;</td>");
            sb.Append("<td ColSpan=\"3\" Style=\"Text-Align:Center;\">ROB</td>");
            sb.Append("<td>&nbsp;</td>");
            sb.Append("<td ColSpan=\"3\" Style=\"Text-Align:Center;\">HO Cons</td>");
            sb.Append("<td>&nbsp;</td>");
            sb.Append("<td ColSpan=\"3\" Style=\"Text-Align:Center;\">DO Cons</td>");
            sb.Append("<td>&nbsp;</td>");
            sb.Append("<td>&nbsp;</td>");
            sb.Append("</tr>");
            sb.Append("<tr Bgcolor=\"E8E8E8\">");
            sb.Append("<td Style=\"Text-Align:Center;\">Reported on</td>");
            sb.Append("<td Style=\"Text-Align:Center;\">Type</td>");
            sb.Append("<td  Style=\"Text-Align:Center;\" Colspan=\"2\">Port / Position</td>");
            sb.Append("<td Style=\"Text-Align:Center;\">Arrival</td>");
            sb.Append("<td Rowspan=\"200\" Style=\"Background:white;\">&nbsp;</td>");
            sb.Append("<td Style=\"Text-Align:Center;\">Departure</td>");
            sb.Append("<td Style=\"Text-Align:Center;\">Next Port</td>");
            sb.Append("<td Style=\"Text-Align:Center;\">ETA Next Port</td>");
            sb.Append(" <td Rowspan=\"200\" Style=\"Background:white;\">&nbsp;</td>");
            sb.Append("<td Style=\"Text-Align:Center;\">Avg Spd</td>");
            sb.Append("<td Style=\"Text-Align:Center;Width:30px;\">FW</td>");
            sb.Append("<td Style=\"Text-Align:Center;Width:30px;\">HO</td>");
            sb.Append("<td Style=\"Text-Align:Center;Width:30px;\">DO</td>");
            sb.Append("<td Rowspan=\"200\" Style=\"Background:white;\">&nbsp;</td>");
            sb.Append("<td Style=\"Text-Align:Center;Width:30px;\">ME</td>");
            sb.Append("<td Style=\"Text-Align:Center;Width:30px;\">AE</td>");
            sb.Append("<td Style=\"Text-Align:Center;Width:30px;\">Blr</td>");
            sb.Append("<td Rowspan=\"200\" Style=\"Background:white;\">&nbsp;</td>");
            sb.Append("<td Style=\"Text-Align:Center;Width:30px;\">ME</td>");
            sb.Append("<td Style=\"Text-Align:Center;Width:30px;\">AE</td>");
            sb.Append("<td Style=\"Text-Align:Center;Width:30px;\">Blr</td>");
            sb.Append("<td Rowspan=\"200\" Style=\"Background:white;\">&nbsp;</td>");
            sb.Append("<td Style=\"Text-Align:Center;\">Remarks</td></tr>");

            string position = "";
            string Port_Name = "";
            string Next_Port = "";
            string TrColor = "";
            string Average_Speed = "";
            string FW_ROB = "";
            string HO_ROB = "";
            string DO_ROB = "";

            string LatMin = "";
            string LatDeg = "";
            string LongMin = "";
            string LongDeg = "";
            string dtArrival = "";
            string dtDeparture = "";
            string dtETA = "";
            foreach (DataRow dr in dt.Rows)
            {
                Port_Name = dr["CurrentPort"].ToString();
                Next_Port = dr["NextPort"].ToString();
                if (dr["Telegram_Type"].ToString() == "N")
                {
                    if (dr["Longitude_Degrees"] != null && dr["Longitude_Degrees"].ToString() != "" )
                        LongDeg = Convert.ToInt32(dr["Longitude_Degrees"]).ToString();
                    if (dr["Longitude_Minutes"] != null && dr["Longitude_Minutes"].ToString() != "")
                        LongMin =  Convert.ToInt32(dr["Longitude_Minutes"]).ToString();
                    if (dr["Latitude_Degrees"] != null && dr["Latitude_Degrees"].ToString() !="")
                        LatDeg =  Convert.ToInt32(dr["Latitude_Degrees"]).ToString();
                    if (dr["Latitude_Minutes"] != null && dr["Latitude_Minutes"] != DBNull.Value)
                        LatMin = Convert.ToInt32(dr["Latitude_Minutes"]).ToString();

                    position = LongDeg + "'" + LongMin + " " + dr["Longitude_E_W"].ToString() + "/" + LatDeg + "'" + LatMin + " " + dr["Latitude_N_S"].ToString();
                }
                else
                    position = Port_Name;

                if (dr["Telegram_Type"].ToString() == "A")
                    TrColor = "Yellow";
                else
                    TrColor = "White";
                if (dr["Average_Speed"] != null)
                    Average_Speed = dr["Average_Speed"].ToString();

                if (dr["FW_ROB"] != null || dr["FW_ROB"].ToString() != "0")
                    FW_ROB = dr["FW_ROB"].ToString();

                if (dr["HO_ROB"] != null || dr["HO_ROB"].ToString() != "0")
                    HO_ROB = dr["HO_ROB"].ToString();

                if (dr["DO_ROB"] != null || dr["DO_ROB"].ToString() != "0")
                    DO_ROB = dr["DO_ROB"].ToString();
                sb.Append("<tr Bgcolor=\"" + TrColor +"\">");
                sb.Append("<td>" + DateTime.Parse(dr["Telegram_Date"].ToString()).ToString("dd-MMM-yyyy") + "</td>");

                sb.Append("<td Style=\"Text-Align:Center;\">" + dr["Telegram_Type"].ToString() + "</td>");

                sb.Append("<td Style=\"Text-Align:Center;\">" + position + "</td>");

                if (dr["Telegram_Type"].ToString() == "A")
                    sb.Append("<td>&nbsp; </td>");
                else
                    sb.Append("<td Style=\"Text-Align:Center;\">" + dr["Location_Code"].ToString() + "</td>");

                if(dr["ESP"] != DBNull.Value)
                    dtArrival = Convert.ToDateTime(dr["ESP"]).ToString("dd/MM/yyyy HHmm");
                if (dr["SSP"] != DBNull.Value)
                    dtDeparture = Convert.ToDateTime(dr["SSP"]).ToString("dd/MM/yyyy HHmm");
                if (dr["ETA_Next_Port"] != DBNull.Value)
                    dtETA = Convert.ToDateTime(dr["ETA_Next_Port"]).ToString("dd/MM/yyyy HHmm");

                sb.Append("<td Style=\"Text-Align:Center;\">" + dtArrival + "</td>");
                sb.Append("<td Style=\"Text-Align:Center;\">" + dtDeparture + "</td>");
                sb.Append("<td Style=\"Text-Align:Center;\">" + Next_Port + "</td>");
                sb.Append("<td Style=\"Text-Align:Center;\">" + dtETA + "</td>");

                sb.Append("<td Style=\"Text-Align:Center;\">" + Average_Speed + "</td>");

                sb.Append("<td Style=\"Text-Align:Center;\">" + FW_ROB + "</td>");
                sb.Append("<td Style=\"Text-Align:Center;\">" + HO_ROB + "</td>");
                sb.Append("<td Style=\"Text-Align:Center;\">" + DO_ROB + "</td>");

                sb.Append("<td Style=\"Text-Align:Center;\">" + dr["ME_HOCons"].ToString() + "</td>");
                sb.Append("<td Style=\"Text-Align:Center;\">" + dr["AE_HOCons"].ToString() + "</td>");
                sb.Append("<td Style=\"Text-Align:Center;\">" + dr["Blr_HOCons"].ToString() + "</td>");

                sb.Append("<td Style=\"Text-Align:Center;\">" + dr["ME_DOCons"].ToString() + "</td>");
                sb.Append("<td Style=\"Text-Align:Center;\">" + dr["AE_DOCons"].ToString() + "</td>");
                sb.Append("<td Style=\"Text-Align:Center;\">" + dr["Blr_DOCons"].ToString() + "</td>");

                sb.Append("<td Width=\"300px;\">" + dr["Remarks"].ToString() + "</td>");
                sb.Append("</tr>");
            }

            sb.Append("</table>");

            ltVesselReport.Text = sb.ToString();
        }
        else
            ltVesselReport.Text = "No records found.";
    }
}