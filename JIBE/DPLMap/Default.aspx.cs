using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Collections.Specialized;
using System.Web.Configuration;
using System.Drawing;
using System.Drawing.Text;
using SMS.Data;
using SMS.Business.Infrastructure;
using SMS.Business.Operation;


public partial class Samples_MapWithSatelliteView : System.Web.UI.Page
{

    BLL_Infra_Port objPort = new BLL_Infra_Port();
    BLL_Infra_VesselLib objVessel = new BLL_Infra_VesselLib();
    string strReportType = "N";

    protected void Page_Load(object sender, EventArgs e)
    {
        strReportType = rbtnNoonReport.Checked == true ? "N" : (rbtnPurpleReport.Checked == true ? "P" : "N");

        if (!IsPostBack)
        {
            Load_FleetList();
            Load_VesselList();

         

            mapLoad(false);
        }


    }

    public void Load_FleetList()
    {
        int UserCompanyID = UDFLib.ConvertToInteger(Session["USERCOMPANYID"].ToString());
        ddlTechmanager.DataSource = objVessel.GetFleetList(UserCompanyID);
        ddlTechmanager.DataTextField = "NAME";
        ddlTechmanager.DataValueField = "CODE";
        ddlTechmanager.DataBind();
        ddlTechmanager.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));
    }
    public void Load_VesselList()
    {
        int Fleet_ID = int.Parse(ddlTechmanager.SelectedValue);
        int UserCompanyID = UDFLib.ConvertToInteger(Session["USERCOMPANYID"].ToString());
        int Vessel_Manager = UDFLib.ConvertToInteger(Session["USERCOMPANYID"].ToString());

        ddl_veslist.DataSource = objVessel.Get_VesselList(Fleet_ID, 0, Vessel_Manager, "", UserCompanyID);

        ddl_veslist.DataTextField = "VESSEL_NAME";
        ddl_veslist.DataValueField = "VESSEL_ID";
        ddl_veslist.DataBind();
        ddl_veslist.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));
        ddl_veslist.SelectedIndex = 0;
    }

    protected void ddlTechmanager_SelectedIndexChanged(object sender, EventArgs e)
    {
        Load_VesselList();
        int Vessel_ID = int.Parse(ddl_veslist.SelectedValue);
        int Fleet_ID = int.Parse(ddlTechmanager.SelectedValue);

        mapLoad(false);
    }

    protected void ddl_veslist_SelectedIndexChanged(object sender, EventArgs e)
    {
        //GoogleMapForASPNet1.GoogleMapObject.Points.Clear();
        int Vessel_ID = int.Parse(ddl_veslist.SelectedValue);
        if (Vessel_ID > 0)
        {
            btn_nearestport.Visible = true;
            tblVesselRoute.Visible = true;
            if (txtFromDT.Text.Trim() == "")
            {
                txtFromDT.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtTODT.Text = DateTime.Now.ToString("dd/MM/yyyy");
            }
        }
        else
        {
            btn_nearestport.Visible = false;
            tblVesselRoute.Visible = false;
        }

        mapLoad(false);
    }

    public void mapLoad(bool isRoute)
    {
        string map_type = "";
        int i = 0;
        string strInfoHtml = "";
        int Vessel_Count = 0;
        int Err_Vessel_Count = 0;

        string Err_Vessel_Msg = "<table>";

        lbtn_error.Visible = false;

        int Vessel_ID = int.Parse(ddl_veslist.SelectedValue);
        int Fleet_ID = int.Parse(ddlTechmanager.SelectedValue);

        try
        {
            DataTable dtData = new DataTable();

            if (isRoute)
            {

                GoogleMapForASPNet1.GoogleMapObject.Points.Clear();
                dtData = BLL_OPS_DPL.Get_TelegramData_Route(Vessel_ID, Fleet_ID, Convert.ToDateTime(txtFromDT.Text), Convert.ToDateTime(txtTODT.Text), strReportType);

                // GoogleMapForASPNet1.GoogleMapObject.ZoomLevel = 5;
            }
            else
            {
                GoogleMapForASPNet1.GoogleMapObject.AutomaticBoundaryAndZoom = false;
                GoogleMapForASPNet1.GoogleMapObject.Points.Clear();
                dtData = BLL_OPS_DPL.Get_TelegramData(Vessel_ID, Fleet_ID, strReportType);
                GoogleMapForASPNet1.GoogleMapObject.ZoomLevel = 3;
            }

            //You must specify Google Map API Key for this component. You can obtain this key from http://code.google.com/apis/maps/signup.html
            //For samples to run properly, set GoogleAPIKey in Web.Config file

            //  GoogleMapForASPNet1.GoogleMapObject.APIKey = ConfigurationManager.AppSettings["GoogleAPIKey"];
            GoogleMapForASPNet1.GoogleMapObject.Width = "100%"; // You can also specify percentage(e.g. 80%) here
            GoogleMapForASPNet1.GoogleMapObject.Height = "850px";


            map_type = htxt_sat_nor_hybrid.Text.ToString();

            if (map_type == "satmap")
                GoogleMapForASPNet1.GoogleMapObject.MapType = GoogleMapType.SATELLITE_MAP;

            if (map_type == "hybridmap")
                GoogleMapForASPNet1.GoogleMapObject.MapType = GoogleMapType.HYBRID_MAP;

            if (map_type == "normalmap")
                GoogleMapForASPNet1.GoogleMapObject.MapType = GoogleMapType.NORMAL_MAP;

            if (map_type == "")
                GoogleMapForASPNet1.GoogleMapObject.MapType = GoogleMapType.NORMAL_MAP;

            GoogleMapForASPNet1.GoogleMapObject.MapType = GoogleMapType.HYBRID_MAP;
            GoogleMapForASPNet1.GoogleMapObject.Polylines.Clear();

            if (dtData.Rows.Count > 0)
            {
                StringBuilder sb = new StringBuilder();


                GooglePoint[] gp = new GooglePoint[dtData.Rows.Count];


                int AreaID = 0;
                int VertexID = 0;
                GooglePolygon[] PiracyArea = new GooglePolygon[2];
                
                #region -- Draw Piracy Area --
                if (chkPiracyArea.Checked == true)
                {
                    GoogleMapForASPNet1.GoogleMapObject.Polygons.Clear();

                    DataTable dt = BLL_OPS_DPL.Get_PiracyArea();

                    if (dt.Rows.Count > 0)
                    {
                        
                        GooglePolygon PG = new GooglePolygon();
                        PG.FillColor = "#FF0000";
                        PG.FillOpacity = 0.3;
                        PG.StrokeColor = "#FF0000";
                        PG.StrokeOpacity = 1;
                        PG.StrokeWeight = 1;

                        AreaID = UDFLib.ConvertToInteger(dt.Rows[0]["AreaID"].ToString());

                        GooglePoint GP1 = new GooglePoint();
                        GP1.ID = "GP" + VertexID.ToString();
                        GP1.Latitude = UDFLib.ConvertToDouble(dt.Rows[0]["Latitude"].ToString());
                        GP1.Longitude = UDFLib.ConvertToDouble(dt.Rows[0]["Longitude"].ToString());
                                               

                        foreach (DataRow dr in dt.Rows)
                        {
                            VertexID++;

                            if (AreaID != UDFLib.ConvertToInteger(dr["AreaID"].ToString()))
                            {
                                PG.ID = "PG" + AreaID.ToString();
                                PG.Points.Add(GP1);
                                GoogleMapForASPNet1.GoogleMapObject.Polygons.Add(PG);
                                PiracyArea[AreaID-1] = PG;


                                PG = new GooglePolygon();
                                PG.FillColor = "#FF0000";
                                PG.FillOpacity = 0.3;
                                PG.StrokeColor = "#FF0000";
                                PG.StrokeOpacity = 1;
                                PG.StrokeWeight = 1;

                                AreaID = UDFLib.ConvertToInteger(dr["AreaID"].ToString());

                                GP1 = new GooglePoint();
                                GP1.ID = "GP" + VertexID.ToString();
                                GP1.Latitude = UDFLib.ConvertToDouble(dr["Latitude"].ToString());
                                GP1.Longitude = UDFLib.ConvertToDouble(dr["Longitude"].ToString());

                            }

                            GooglePoint GP = new GooglePoint();
                            GP.ID = "GP" + VertexID.ToString();
                            GP.Latitude = UDFLib.ConvertToDouble(dr["Latitude"].ToString());
                            GP.Longitude =  UDFLib.ConvertToDouble(dr["Longitude"].ToString());

                            PG.Points.Add(GP);
                        }

                        PG.ID = "PG" + AreaID.ToString();
                        PG.Points.Add(GP1);
                        GoogleMapForASPNet1.GoogleMapObject.Polygons.Add(PG);
                        PiracyArea[AreaID - 1] = PG;
                    }
                }
                else
                {
                    GoogleMapForASPNet1.GoogleMapObject.Polygons.Clear();
                }
                #endregion

                if (!isRoute)
                {
                    #region ---------Draw all vessels icons and baloons--------

                    foreach (DataRow dr in dtData.Rows)
                    {
                        Vessel_Count++;

                        gp[i] = new GooglePoint();

                        //string sangle = dr[5].ToString();
                        string sangle = dr["Vessel_Course"].ToString();


                        CreateImage(dr["Vessel_Short_Name"].ToString());



                        string latitude = ""; string longitude = "";
                        string longi_pos1; string longi_pos2; string lati_pos1; string lati_pos2;
                        decimal lat_degree = 0; decimal lat_min = 0; decimal lat_sec = 0; string slat_dir = "";
                        decimal long_degree = 0; decimal long_min = 0; decimal long_sec = 0; string slong_dir = "";


                        try
                        {
                            lat_degree = UDFLib.ConvertToDecimal(dr["Latitude_Degrees"].ToString());
                            lat_min = UDFLib.ConvertToDecimal(dr["Latitude_Minutes"].ToString());
                            lat_sec = UDFLib.ConvertToDecimal(dr["Latitude_Seconds"].ToString());
                            slat_dir = dr["LATITUDE_N_S"].ToString();
                            if (slat_dir != "N" && slat_dir != "S")
                                slat_dir = "N";

                            latitude = Conv_Deg2Decimal_new(Convert.ToDouble(lat_degree), Convert.ToDouble(lat_min), Convert.ToDouble(lat_sec), slat_dir.ToString());

                            long_degree = UDFLib.ConvertToDecimal(dr["Longitude_Degrees"].ToString());
                            long_min = UDFLib.ConvertToDecimal(dr["Longitude_Minutes"].ToString());
                            long_sec = UDFLib.ConvertToDecimal(dr["Longitude_Seconds"].ToString());
                            slong_dir = dr["Longitude_E_W"].ToString();
                            if (slong_dir != "E" && slong_dir != "W")
                                slat_dir = "E";

                            longitude = Conv_Deg2Decimal_new(Convert.ToDouble(long_degree), Convert.ToDouble(long_min), Convert.ToDouble(long_sec), slong_dir.ToString());

                            if (Convert.ToString(dr["Telegram_Type"]).ToUpper() == "N")
                                gp[i].IconImage = dr["Vessel_Short_Name"].ToString() + ".png";

                            else if (Convert.ToString(dr["Telegram_Type"]).ToUpper() == "P")
                                gp[i].IconImage = dr["Vessel_Short_Name"].ToString() + "-P" + ".png";
                            
                            
                            gp[i].Latitude = double.Parse(latitude);
                            gp[i].Longitude = double.Parse(longitude);


                            // - check if in piracy area
                            bool iPointInPolygon = PointInPolygon(gp[i], PiracyArea);
                            if (iPointInPolygon == true)
                            {
                                GooglePoint shipInPiracy = new GooglePoint();
                                shipInPiracy.Latitude = double.Parse(latitude);
                                shipInPiracy.Longitude = double.Parse(longitude);
                                shipInPiracy.IconImage = "../Images/star.gif";
                                //shipInPiracy.IconImageHeight = 20;
                                //shipInPiracy.IconImageWidth = 20;
                                GoogleMapForASPNet1.GoogleMapObject.Points.Add(shipInPiracy);
                            }



                            string simagename = "";
                            if (sangle == "" || sangle == null)
                            {
                                simagename = "boat_.png";
                            }

                            else
                            {

                                Double angle = Convert.ToDouble(sangle);

                                if ((angle >= 0) && (angle <= 90))
                                    simagename = "UpSide45_f_.png";

                                if ((angle >= 90) && (angle <= 180))
                                    simagename = "UpSide_135_.png";

                                if ((angle >= 180) && (angle <= 270))
                                    simagename = "Down_225_.png";

                                if ((angle >= 270) && (angle <= 360))
                                    simagename = "Down_315_.png";

                            }



                            strInfoHtml = "<table border=0 cellspacing=0 cellpadding=0 style='font-size:10px;font-family:Verdana'>";
                            strInfoHtml += "<tr><td colspan=2 style='font-weight:bold;'>" + dr["Vessel_Name"].ToString() + "&nbsp;&nbsp;<img src='images/" + simagename + "'>" + "</td></tr>";
                            strInfoHtml += "<tr><td style='width:80px'>Report Date:</td><td style='color:blue;font-weight:bold'>" + dr["infodate"].ToString() + "</td></tr>";
                            strInfoHtml += "<tr><td style='width:80px'>Location:</td><td>" + dr["Location_Name"].ToString() + "</td></tr>";
                            strInfoHtml += "<tr><td style='width:80px'>Latitude:</td><td>" + dr["Latitude_Degrees"].ToString() + " " + dr["Latitude_Minutes"].ToString() + " " + dr["Latitude_Seconds"].ToString() + " " + dr["LATITUDE_N_S"].ToString() + "</td></tr>";
                            strInfoHtml += "<tr><td style='width:80px'>Longitude:</td><td>" + dr["Longitude_Degrees"].ToString() + " " + dr["Longitude_Minutes"].ToString() + " " + dr["Longitude_Seconds"].ToString() + " " + dr["Longitude_E_W"].ToString() + "</td></tr>";
                            strInfoHtml += "<tr><td style='width:80px'>Course:</td><td>" + sangle.ToString() + "</td></tr>";
                            strInfoHtml += "<tr><td style='width:80px'>Wind Direction/ Force:</td><td>" + dr["Wind_Direction"].ToString() + "/" + dr["Wind_Force"].ToString() + "</td></tr>";
                            strInfoHtml += "<tr><td style='width:80px'>Average speed:</td><td>" + dr["AVERAGE_SPEED"].ToString() + " knts</td></tr>";
                            strInfoHtml += "<tr><td style='width:80px'>Next port/<br>ETA:</td><td>" + dr["PORT_NAME"].ToString() + "<br>" + dr["etanextport"].ToString() + "</td></tr>";
                            strInfoHtml += "<tr><td style='width:80px'></td><td></td></tr>";

                            string CrewLink = "<a href='" + ConfigurationManager.AppSettings["APP_URL"].ToString() + "Crew/CrewList_PhotoView.aspx?vcode=" + dr["Vessel_Short_Name"].ToString() + "' target='_blank'>Crew List</a>";
                            string NoonLink = "<a href='" + ConfigurationManager.AppSettings["APP_URL"].ToString() + "Operations/NoonReport.aspx?LastNoon=" + dr["Vessel_Short_Name"].ToString() + "&ID=" + dr["PKID"].ToString() + "' target='_blank'>Last Noon</a>";

                           


                            strInfoHtml += "<td>" + CrewLink + "</td><td>" + NoonLink + "</td>";
                            strInfoHtml += "</tr>";
                            strInfoHtml += "</table>";

                            gp[i].InfoHTML = strInfoHtml.ToString();

                            GoogleMapForASPNet1.GoogleMapObject.Points.Add(gp[i]);


                            i++;

                        }
                        catch
                        {
                            Err_Vessel_Count++;

                            lbtn_error.Visible = true;

                            lati_pos1 = "1";
                            lati_pos2 = "2";
                            longi_pos1 = "1";
                            longi_pos2 = "2";

                            gp[i].Latitude = double.Parse(lati_pos1 + lati_pos2);
                            gp[i].Longitude = double.Parse(longi_pos1 + longi_pos2);

                            Err_Vessel_Msg += "<tr><td>" + dr[1].ToString() + "</td><td>" + dr[2].ToString() + "</td><td>Latitude:" + UDFLib.ConvertToInteger(latitude.ToString()) + "</td><td>Longitude:" + longitude.ToString() + "</td></tr>";


                            continue;
                        }

                    } //End For



                    if (Err_Vessel_Msg == "<table>")
                        Err_Vessel_Msg = "";
                    else
                        Err_Vessel_Msg += "</table>";

                    if (Err_Vessel_Count > 0)
                    {
                        lblVessels.ForeColor = System.Drawing.Color.Red;
                        lblVessels.Text = "Total Ships: " + Vessel_Count.ToString() + " (" + Err_Vessel_Count.ToString() + " Ship(s) found with Error!!)";

                        lblLoadingIssues.Text = Err_Vessel_Msg;
                    }
                    else
                    {
                        lblVessels.ForeColor = System.Drawing.Color.Blue;
                        lblVessels.Text = "Total Ships: " + Vessel_Count.ToString();
                    }
                    #endregion                       
                }
                else
                {
                    #region ---------------- Route----------


                    double lat_pos_g = 0;
                    double log_pos_g = 0;

                    GooglePolyline objPolyLine = new GooglePolyline();

                    if (strReportType == "N")
                        objPolyLine.ColorCode = "#7FFF00";
                    else
                        objPolyLine.ColorCode = "#FF00FF";

                    objPolyLine.Width = 1;
                    objPolyLine.Geodesic = true;

                    foreach (DataRow dr in dtData.Rows)
                    {
                        Vessel_Count++;

                        gp[i] = new GooglePoint();

                        //string sangle = dr[5].ToString();
                        string sangle = dr["Vessel_Course"].ToString();

                        CreateImage(dr["Vessel_Short_Name"].ToString());


                        string latitude = ""; string longitude = "";
                        string longi_pos1; string longi_pos2; string lati_pos1; string lati_pos2;
                        decimal lat_degree = 0; decimal lat_min = 0; decimal lat_sec = 0; string slat_dir = "";
                        decimal long_degree = 0; decimal long_min = 0; decimal long_sec = 0; string slong_dir = "";

                        try
                        {
                            lat_degree = UDFLib.ConvertToDecimal(dr["Latitude_Degrees"].ToString());
                            lat_min = UDFLib.ConvertToDecimal(dr["Latitude_Minutes"].ToString());
                            lat_sec = UDFLib.ConvertToDecimal(dr["Latitude_Seconds"].ToString());
                            slat_dir = dr["LATITUDE_N_S"].ToString();
                            if (slat_dir != "N" && slat_dir != "S")
                                slat_dir = "N";

                            latitude = Conv_Deg2Decimal_new(Convert.ToDouble(lat_degree), Convert.ToDouble(lat_min), Convert.ToDouble(lat_sec), slat_dir.ToString());

                            long_degree = UDFLib.ConvertToDecimal(dr["Longitude_Degrees"].ToString());
                            long_min = UDFLib.ConvertToDecimal(dr["Longitude_Minutes"].ToString());
                            long_sec = UDFLib.ConvertToDecimal(dr["Longitude_Seconds"].ToString());
                            slong_dir = dr["Longitude_E_W"].ToString();
                            if (slong_dir != "E" && slong_dir != "W")
                                slat_dir = "E";

                            longitude = Conv_Deg2Decimal_new(Convert.ToDouble(long_degree), Convert.ToDouble(long_min), Convert.ToDouble(long_sec), slong_dir.ToString());

                            //else if (Convert.ToString(dr["Telegram_Type"]).ToUpper() == "P")
                            //    gp[i].IconImage = dr["Vessel_Short_Name"].ToString() + "-P" + ".png";

                            gp[i].Latitude = double.Parse(latitude);
                            gp[i].Longitude = double.Parse(longitude);

                            string simagename = "";
                            if (sangle == "" || sangle == null)
                            {
                                simagename = "boat_.png";
                            }

                            else
                            {
                                Double angle = Convert.ToDouble(sangle);

                                if ((angle >= 0) && (angle <= 90))
                                    simagename = "UpSide45_f_.png";

                                if ((angle >= 90) && (angle <= 180))
                                    simagename = "UpSide_135_.png";

                                if ((angle >= 180) && (angle <= 270))
                                    simagename = "Down_225_.png";

                                if ((angle >= 270) && (angle <= 360))
                                    simagename = "Down_315_.png";

                            }

                            strInfoHtml = "<table border=0 cellspacing=0 cellpadding=0 style='font-size:10px;font-family:Verdana'>";
                            strInfoHtml += "<tr><td colspan=2 style='font-weight:bold;'>" + dr["Vessel_Name"].ToString() + "&nbsp;&nbsp;<img src='images/" + simagename + "'>" + "</td></tr>";
                            strInfoHtml += "<tr><td style='width:80px'>Report Date:</td><td style='color:blue;font-weight:bold'>" + dr["infodate"].ToString() + "</td></tr>";
                            strInfoHtml += "<tr><td style='width:80px'>Location:</td><td>" + dr["Location_Name"].ToString() + "</td></tr>";
                            strInfoHtml += "<tr><td style='width:80px'>Latitude:</td><td>" + dr["Latitude_Degrees"].ToString() + " " + dr["Latitude_Minutes"].ToString() + " " + dr["Latitude_Seconds"].ToString() + " " + dr["LATITUDE_N_S"].ToString() + "</td></tr>";
                            strInfoHtml += "<tr><td style='width:80px'>Longitude:</td><td>" + dr["Longitude_Degrees"].ToString() + " " + dr["Longitude_Minutes"].ToString() + " " + dr["Longitude_Seconds"].ToString() + " " + dr["Longitude_E_W"].ToString() + "</td></tr>";
                            strInfoHtml += "<tr><td style='width:80px'>Course:</td><td>" + sangle.ToString() + "</td></tr>";
                            strInfoHtml += "<tr><td style='width:80px'>Wind Direction/ Force:</td><td>" + dr["Wind_Direction"].ToString() + "/" + dr["Wind_Force"].ToString() + "</td></tr>";
                            strInfoHtml += "<tr><td style='width:80px'>Average speed:</td><td>" + dr["AVERAGE_SPEED"].ToString() + " knts</td></tr>";
                            strInfoHtml += "<tr><td style='width:80px'>Next port/<br>ETA:</td><td>" + dr["PORT_NAME"].ToString() + "<br>" + dr["etanextport"].ToString() + "</td></tr>";
                            strInfoHtml += "<tr><td style='width:80px'></td><td></td></tr>";

                            string ReportName = rbtnNoonReport.Checked == true ? "Noon Report" : (rbtnPurpleReport.Checked == true ? "PurpleFinder" : "Noon Report");
                            string CrewLink = "<a href='" + ConfigurationManager.AppSettings["APP_URL"].ToString() + "Crew/CrewListHistory.aspx?VesselID=" + dr["Vessel_ID"].ToString() + "&AsofDate=" + dr["Telegram_Date"].ToString() + "' target='_blank'>Crew List</a>";
                            string NoonLink = "";

                            if (strReportType == "N")
                                NoonLink = "<a href='" + ConfigurationManager.AppSettings["APP_URL"].ToString() + "Operations/NoonReport.aspx?id=" + dr["PKID"].ToString() + "' target='_blank'>" + ReportName + "</a>";
                            else
                                NoonLink = "<a href='" + ConfigurationManager.AppSettings["APP_URL"].ToString() + "Operations/PurpleReport.aspx?id=" + dr["PKID"].ToString() + "' target='_blank'>" + ReportName + "</a>";

                            strInfoHtml += "<td>" + CrewLink + "</td><td>" + NoonLink + "</td>";
                            strInfoHtml += "</tr>";
                            strInfoHtml += "</table>";

                            gp[i].InfoHTML = strInfoHtml.ToString();

                            objPolyLine.Points.Add(gp[i]);

                            if (i == 0)
                            {
                                //gp[i].IconImage = "red-button.png";

                                lat_pos_g = double.Parse(latitude);
                                log_pos_g = double.Parse(longitude);
                                GoogleMapForASPNet1.GoogleMapObject.Points.Add(gp[i]);
                            }
                            else
                            {
                                gp[i].IconImage = "number_" + Convert.ToDateTime(dr["Telegram_Date"]).Day.ToString() + ".png";

                                GoogleMapForASPNet1.GoogleMapObject.Points.Add(gp[i]);
                            }

                            i++;

                        }
                        catch
                        {
                            Err_Vessel_Count++;

                            lbtn_error.Visible = true;

                            lati_pos1 = "1";
                            lati_pos2 = "2";
                            longi_pos1 = "1";
                            longi_pos2 = "2";

                            gp[i].Latitude = double.Parse(lati_pos1 + lati_pos2);
                            gp[i].Longitude = double.Parse(longi_pos1 + longi_pos2);

                            Err_Vessel_Msg += "<tr><td>" + dr[1].ToString() + "</td><td>" + dr[2].ToString() + "</td><td>Latitude:" + UDFLib.ConvertToInteger(latitude.ToString()) + "</td><td>Longitude:" + longitude.ToString() + "</td></tr>";


                            continue;
                        }

                    }


                    GoogleMapForASPNet1.GoogleMapObject.Polylines.Add(objPolyLine);
                    GoogleMapForASPNet1.GoogleMapObject.CenterPoint.Latitude = lat_pos_g;
                    GoogleMapForASPNet1.GoogleMapObject.CenterPoint.Longitude = log_pos_g;
                    GoogleMapForASPNet1.GoogleMapObject.AutomaticBoundaryAndZoom = true;

                    #endregion
                }


            }
            else
            {
                lblVessels.Text = "Total Ships: " + Err_Vessel_Count.ToString();
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }

    //protected void Load_PortList()
    //{
    //    drp_port.DataSource = objPort.Get_PortList();
    //    drp_port.DataTextField = "Port_Name";
    //    drp_port.DataValueField = "Port_ID";
    //    drp_port.DataBind();
    //    drp_port.Items.Insert(0,new ListItem("-Select Port -","0"));

    //}


    protected void chkPiracyArea_CheckedChanged(object sender, EventArgs e)
    {       
        mapLoad(false);
    }

    protected void ctlPort_SelectedIndexChanged()
    {
        if (ctlPort.SelectedValue != "0")
        {
            mapLoad(false);
            show_selecport(int.Parse(ctlPort.SelectedValue));
        }
        else
        {
            GoogleMapForASPNet1.GoogleMapObject.Points.Clear();
        }
    }

    protected void GoogleMapForASPNet1_Load(object sender, EventArgs e)
    {
        //GoogleMapForASPNet1.GoogleMapObject.Height = "768px";
        //GoogleMapForASPNet1.GoogleMapObject.Width = "100%";
        //GoogleMapForASPNet1.GoogleMapObject.ZoomLevel = 2;
    }

    public string Conv_Deg2Decimal_new(double degree, double mins, double secs, string directions)
    {

        string inputval = "";
        string input = inputval;
        string sign = "";

        double sd = 0;



        if ((directions.ToUpper() == "S") || (directions.ToUpper() == "W"))
        {
            sign = "-";
        }


        sd = (degree) + (mins / 60) + (secs / 3600);

        if (sign == "-")
        {
            sd = sd * (-1);
        }


        sd = Math.Round(sd, 6);
        string sdnew_other = Convert.ToString(sd);
        string sdnew1_other = "";
        sdnew1_other = string.Format("{0:0.000000}", sd);
        return sdnew1_other;


    }

    protected void lbtn_voyage_Click(object sender, EventArgs e)
    {
        ResponseHelper.Redirect("PassageInfo.aspx", "blank", "");
    }

    protected void lbtnviewdpl_Click(object sender, EventArgs e)
    {
        string techmanager = ddlTechmanager.SelectedItem.Text.ToString();
        string veselname = ddl_veslist.SelectedValue.ToString();
        string qstring = techmanager + "," + veselname;
       
        ResponseHelper.Redirect(ConfigurationManager.AppSettings["APP_URL"].ToString() + "Operations/Default.aspx?v='" + qstring + "'", "_blank", "");

    }





    protected void lnkHome_Click(object sender, EventArgs e)
    {

       // ResponseHelper.Redirect(ConfigurationManager.AppSettings["APP_URL"].ToString() + "Infrastructure/DashBoard_Common.aspx", "_blank", "");
    
        Response.Redirect(ConfigurationManager.AppSettings["DeafaultURL"]);
    }


    private void CreateImage(string sImageText)
    {
        string filePath = "";
        string Filename = "";
        if (strReportType == "N")
        {
            filePath = Server.MapPath("ShipImages/" + sImageText + ".png");
            Filename = sImageText;
        }
        else
        {
            filePath = Server.MapPath("ShipImages/" + sImageText + "-P.png");
            Filename = sImageText + "-P";
        }

        if (!File.Exists(filePath))
        {
            Bitmap bmpImage = new Bitmap(1, 1);

            int iWidth = 0; int iHeight = 0;
            Font MyFont = new Font("Verdana", 6, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            Graphics MyGraphics = Graphics.FromImage(bmpImage);

            // Resize the bitmap, this is where the bitmap is sized.
            iWidth = (int)MyGraphics.MeasureString(sImageText, MyFont).Width;
            iHeight = (int)MyGraphics.MeasureString(sImageText, MyFont).Height;

            bmpImage = new Bitmap(bmpImage, new Size(iWidth, iHeight));

            MyGraphics = Graphics.FromImage(bmpImage);
            if (strReportType == "N")
                MyGraphics.Clear(Color.Cyan);
            else if (strReportType == "P")
                MyGraphics.Clear(Color.Purple);

            MyGraphics.TextRenderingHint = TextRenderingHint.AntiAlias;
            if (strReportType == "N")
                MyGraphics.DrawString(Filename, MyFont, new SolidBrush(Color.Black), 0, 0);
            else if (strReportType == "P")
                MyGraphics.DrawString(Filename, MyFont, new SolidBrush(Color.White), 0, 0);

            MyGraphics.Flush();

            WriteImage(Filename, bmpImage);
        }
    }

    public void WriteImage(string filename, Bitmap bmpimage)
    {
        string filePath = Server.MapPath("ShipImages/" + filename + ".png");

        MemoryStream ms = new MemoryStream();
        bmpimage.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
        File.WriteAllBytes(filePath, ms.ToArray());
    }

    public string convdegtodecimal_all(string inputval)
    {
        string temp = inputval.Replace("'", "");
        string input = temp;

        double sd = 0.0;
        double min = 0.0;
        double sec = 0.0;
        double deg = 0.0;
        string direction = input.Substring((input.Length - 1), 1);
        string sign = "";
        string Temp = "";
        string S = input.Substring(input.Length - 1);
        input = input.Remove(input.Length - 1).Trim();

        try
        {
            if ((direction.ToUpper() == "S") || (direction.ToUpper() == "W"))
            {
                sign = "-";
            }


            //changes for etc data handling

            if (input.Length == 5 && (input.IndexOf(" ") == -1))
            {
                int _indx = input.IndexOf("-");
                deg = Convert.ToDouble(input.Substring(0, _indx).ToString());
                min = Convert.ToDouble(input.Substring(_indx + 1, (input.Length - (_indx + 1))).ToString());

                min = min / ((double)60);

                sd = deg + min;

                if (!(string.IsNullOrEmpty(sign)))
                {
                    sd = sd * (-1);
                }
                sd = Math.Round(sd, 6);

                string sdnew_other = Convert.ToString(sd);
                string sdnew1_other = "";
                sdnew1_other = string.Format("{0:0.000000}", sd);
                return sdnew1_other;


            }



            if (input.Length == 4 && (input.IndexOf(" ") == -1))
            {
                int _indx = input.IndexOf("-");
                deg = Convert.ToDouble(input.Substring(0, _indx).ToString());
                min = Convert.ToDouble(input.Substring(_indx + 1, (input.Length - (_indx + 1))).ToString());

                min = min / ((double)60);

                sd = deg + min;

                if (!(string.IsNullOrEmpty(sign)))
                {
                    sd = sd * (-1);
                }
                sd = Math.Round(sd, 6);

                string sdnew_other = Convert.ToString(sd);
                string sdnew1_other = "";
                sdnew1_other = string.Format("{0:0.000000}", sd);
                return sdnew1_other;


            }

            if (input.Length == 6 && (input.IndexOf(" ") == -1))
            {
                int _indx = input.IndexOf("-");
                deg = Convert.ToDouble(input.Substring(0, _indx).ToString());
                min = Convert.ToDouble(input.Substring(_indx + 1, (input.Length - (_indx + 1))).ToString());

                min = min / ((double)60);

                sd = deg + min;

                if (!(string.IsNullOrEmpty(sign)))
                {
                    sd = sd * (-1);
                }
                sd = Math.Round(sd, 6);

                string sdnew_other = Convert.ToString(sd);
                string sdnew1_other = "";
                sdnew1_other = string.Format("{0:0.000000}", sd);
                return sdnew1_other;


            }


            string[] value = new string[10];
            string[] arr = input.Split(new char[] { ' ' });
            if (arr.Length == 1)
            {
                arr = input.Split(new char[] { '.' });
            }

            if (arr.Length == 1)
            {
                string t = input.Substring(0, 2).ToString();
                value[0] = input.Substring(0, 1).ToString();
                value[1] = input.Substring(2, 1).ToString();
            }
            else
            {
                for (int k = 0; k < arr.Length; k++)
                {
                    value[k] = arr[k];
                }
            }
            if (min == 0.0)
            {
                min = Convert.ToDouble(value[1]);
            }
            int j = 0;
            if (arr.Length == 2)
            {
                Temp = "00.000" + S;

            }
            if (Temp.Length > 0)
            {
                string[] arr1 = Temp.Split(new char[] { '.' });
                sec = Convert.ToDouble(arr1[0]);
                deg = Convert.ToDouble(value[0]);
                min = min / ((double)60);
                sec = sec / ((double)3600);
                sd = deg + min + sec;
            }
            else
            {
                // string[] arr1 = Temp.Split(new char[] { '.' });
                sec = Convert.ToDouble(value[2]);
                deg = Convert.ToDouble(value[0]);
                min = min / ((double)60);
                sec = sec / ((double)3600);
                sd = deg + min + sec;
            }


            if (!(string.IsNullOrEmpty(sign)))
            {
                sd = sd * (-1);
            }
            sd = Math.Round(sd, 6);
            string sdnew = Convert.ToString(sd);
            string sdnew1 = "";
            sdnew1 = string.Format("{0:0.000000}", sd);
            return sdnew1;

        }

        catch (Exception ex)
        {
            // return "24.333";
            return "";
        }

    }

    public void show_selecport(int PortID)
    {

        string str_longitude;
        string str_latitude;
        //GoogleMapForASPNet1.GoogleMapObject.Points.Clear();

        DataTable dtPort = objPort.Get_PortDetailsByID(PortID);

        if (dtPort.Rows.Count > 0)
        {
            str_longitude = dtPort.Rows[0]["Port_Lon"].ToString();
            str_latitude = dtPort.Rows[0]["Port_Lat"].ToString();

            string latitude;
            string longitude;

            try
            {
                GoogleMapForASPNet1.GoogleMapObject.APIKey = ConfigurationManager.AppSettings["GoogleAPIKey"];
                GoogleMapForASPNet1.GoogleMapObject.ZoomLevel = 2;
                GoogleMapForASPNet1.GoogleMapObject.AutomaticBoundaryAndZoom = false;
                //GoogleMapForASPNet1.GoogleMapObject.MapType = GoogleMapType.NORMAL_MAP;

                GooglePoint gp = new GooglePoint();

                gp.IconImage = "../Images/port.gif";
                gp.IconImageWidth = 15;
                gp.IconImageHeight = 15;

                latitude = convdegtodecimal_all(str_latitude);
                longitude = convdegtodecimal_all(str_longitude);

                gp.Latitude = double.Parse(latitude);
                gp.Longitude = double.Parse(longitude);

                gp.InfoHTML = "Port Name: " + dtPort.Rows[0]["Port_Name"].ToString() + "<br>Country: " + dtPort.Rows[0]["Port_Country"].ToString() + "<br>Longitude: " + str_longitude + "<br>Latitude: " + str_latitude;


                GoogleMapForASPNet1.GoogleMapObject.Points.Add(gp);


            }

            catch (Exception ex)
            {

            }

        }


    }

    //protected void drp_port_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (drp_port.SelectedIndex > 0)
    //        show_selecport(int.Parse(drp_port.SelectedValue));
    //}

    //protected void chk_port_CheckedChanged(object sender, EventArgs e)
    //{
    //    if (chk_port.Checked == true && drp_port.Items.Count <= 1)
    //    {
    //        Load_PortList();
    //    }
    //}

    protected void btn_nearestport_Click(object sender, EventArgs e)
    {
        if (ddl_veslist.SelectedIndex > 0)
            Show_Nearest_Ports();

    }

    public void Show_Nearest_Ports()
    {
        //GoogleMapForASPNet1.GoogleMapObject.Points.Clear();

        string shipname = ddl_veslist.SelectedItem.Text.ToString();
        string longitude_sel = "";
        string latitude_sel = "";
        string longdire_sel = "";
        string latdir_sel = "";


        int Vessel_ID = int.Parse(ddl_veslist.SelectedValue);
        int Fleet_ID = int.Parse(ddlTechmanager.SelectedValue);

        DataTable dtData = BLL_OPS_DPL.Get_TelegramData(Vessel_ID, Fleet_ID, strReportType);
        if (dtData.Rows.Count > 0)
        {
            longitude_sel = dtData.Rows[0]["Longitude_Degrees"].ToString();
            latitude_sel = dtData.Rows[0]["Latitude_Degrees"].ToString();
            longdire_sel = dtData.Rows[0]["Longitude_E_W"].ToString();
            latdir_sel = dtData.Rows[0]["LATITUDE_N_S"].ToString();
        }

        DataTable ds_ship_ports = BLL_OPS_DPL.Get_Ports_NearVessel(longitude_sel, latitude_sel, longdire_sel, latdir_sel);

        if (ds_ship_ports.Rows.Count > 0)
        {
            StringBuilder sb = new StringBuilder();
            GoogleMapForASPNet1.GoogleMapObject.AutomaticBoundaryAndZoom = true;
            GooglePoint[] gp = new GooglePoint[ds_ship_ports.Rows.Count];

            int i = 0;

            foreach (DataRow dr_port in ds_ship_ports.Rows)
            {
                string latitude = "";
                string longitude = "";


                latitude = convdegtodecimal_all(dr_port["Port_Lat"].ToString());
                longitude = convdegtodecimal_all(dr_port["Port_Lon"].ToString());

                if (latitude != "" && longitude != "")
                {

                    gp[i] = new GooglePoint();

                    gp[i].IconImage = "../Images/port.gif";
                    gp[i].IconImageWidth = 15;
                    gp[i].IconImageHeight = 15;


                    gp[i].Latitude = double.Parse(latitude);
                    gp[i].Longitude = double.Parse(longitude);

                    gp[i].InfoHTML = "Port Name: " + dr_port["PORT_NAME"].ToString() + "<br>Country: " + dr_port["PORT_COUNTRY"].ToString() + "<br>Longitude: " + dr_port["PORT_LON"].ToString() + "<br>Latitude: " + dr_port["PORT_LAT"].ToString();


                    GoogleMapForASPNet1.GoogleMapObject.Points.Add(gp[i]);
                    i++;
                }
                else
                {
                    continue;
                }
            }
        }


    }

    protected void btnGetVesselRoute_Click(object s, EventArgs e)
    {
        mapLoad(true);
    }
    protected void rbtnNoonReport_CheckedChanged(object sender, EventArgs e)
    {
        mapLoad(false);

    }
    protected void rbtnPurpleReport_CheckedChanged(object sender, EventArgs e)
    {
        mapLoad(false);
    }

    static bool PointInPolygon(GooglePoint p, GooglePolygon[] poly)
    {
        bool inside = false;
        int polyid = 0;

        try
        {
            if (poly == null || poly.Length == 0)
            {
                return inside;
            }
            if (poly[polyid] == null || poly[polyid].Points.Count < 3)
            {
                return inside;
            }
            int i, j, nvert = poly[polyid].Points.Count;
            for (i = 0, j = nvert - 1; i < nvert; j = i++)
            {
                if (((poly[polyid].Points[i].Latitude > p.Latitude) != (poly[polyid].Points[j].Latitude > p.Latitude)) &&
                 (p.Longitude < (poly[polyid].Points[j].Longitude - poly[polyid].Points[i].Longitude) * (p.Latitude - poly[polyid].Points[i].Latitude) / (poly[polyid].Points[j].Latitude - poly[polyid].Points[i].Latitude) + poly[polyid].Points[i].Longitude))
                    inside = !inside;
            }

        }
        catch { }        
        return inside;
    }

    

}
