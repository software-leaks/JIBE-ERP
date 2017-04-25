using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.ServiceModel.Activation;
using System.Data;
using DALJibe;
using System.Configuration;


namespace SMS.DPLService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "JibeDPLService" in code, svc and config file together.
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class JibeDPLService : IJibeDPLService
    {
        //Just a sample Method
        public List<Coordinates> GetGeneral()
        {
            Coordinates co = new Coordinates();
            List<Coordinates> lco = new List<Coordinates>();
            co.Latitude = 21.0000;
            co.Longitude = 78.0000;
            lco.Add(co);

            co = new Coordinates();
            co.Latitude = 13.7500;
            co.Longitude = 100.4833;
            lco.Add(co);

            return lco;

        }
        //Just a sample Method - End
        public List<Ports> Get_PortList()
        {
            List<Ports> Pt = new List<Ports>();
            try
            {

                DataSet dsData = JiBeDAL.GetPortList();
                DataTable dtport = dsData.Tables[0];

                foreach (DataRow dr in dtport.Rows)
                {
                    if (!string.IsNullOrEmpty(Convert.ToString(dr["PORT_ID"]).Trim()) && !string.IsNullOrEmpty(Convert.ToString(dr["PORT_NAME"]).Trim()) && !string.IsNullOrEmpty(Convert.ToString(dr["PORT_LON"]).Trim()) && !string.IsNullOrEmpty(Convert.ToString(dr["PORT_LAT"]).Trim()))
                    {
                        Ports pt = new Ports();
                        pt.PORT_ID = Convert.ToInt32(dr["PORT_ID"]);
                        pt.PORT_NAME = Convert.ToString(dr["PORT_NAME"]);
                        //if (!string.IsNullOrEmpty(Convert.ToString(dr["BP_CODE"])))
                        //    pt.BP_CODE = Convert.ToString(dr["BP_CODE"]);
                        //else
                        //    pt.BP_CODE = "Unknown";
                        ////double Lon = JiBeGeneral.ConvertLatLon(Convert.ToString(dr["PORT_LON"]));
                        //pt.PORT_LON = 0.00;
                        ////double Lat = JiBeGeneral.ConvertLatLon(Convert.ToString(dr["PORT_LAT"]));
                        //pt.PORT_LAT = 0.00;
                        //if (!string.IsNullOrEmpty(Convert.ToString(dr["OCEAN"])))
                        //    pt.OCEAN = Convert.ToString(dr["OCEAN"]);
                        //else
                        //    pt.OCEAN = "Unknown";

                        //if (!string.IsNullOrEmpty(Convert.ToString(dr["UTC"])))
                        //    pt.UTC = Convert.ToString(dr["UTC"]);
                        //else
                        //    pt.UTC = "Unknown";
                        //if (!string.IsNullOrEmpty(Convert.ToString(dr["Country_ID"])))
                        //    pt.Country_ID = Convert.ToInt32(dr["Country_ID"]);
                        //else
                        //    pt.Country_ID = 0;

                        //if (!string.IsNullOrEmpty(Convert.ToString(dr["Country_Name"])))
                        //    pt.Country_Name = Convert.ToString(dr["Country_Name"]);
                        //else
                        //    pt.Country_Name ="Unknown";


                        Pt.Add(pt);
                    }

                }
            }
            catch (Exception ex)
            {
                string s = ex.Message;
            }

            return Pt;
        }


        public List<PiracyArea> Get_PiracyAreaList()
        {
            List<PiracyArea> Pc = new List<PiracyArea>();
            try
            {

                DataSet dsData = JiBeDAL.Get_PiracyAreaList();
                DataTable dtpiracyarea = dsData.Tables[0];
                foreach (DataRow dr in dtpiracyarea.Rows)
                {
                    PiracyArea pa = new PiracyArea();
                    pa.AreaID = Convert.ToInt32(dr["AreaID"]);
                    pa.VertexID = Convert.ToInt32(dr["VertexID"]);
                    pa.Latitude = Convert.ToDecimal(dr["Latitude"]);
                    pa.Longitude = Convert.ToDecimal(dr["Longitude"]);
                    if (!string.IsNullOrEmpty(Convert.ToString(dr["Created_By"])))
                        pa.Created_By = Convert.ToInt32(dr["Created_By"]);
                    else
                        pa.Created_By = null;
                    pa.Date_Of_Creatation = Convert.ToDateTime(dr["Date_Of_Creatation"]);
                    if (!string.IsNullOrEmpty(Convert.ToString(dr["Modified_By"])))
                        pa.Modified_By = Convert.ToInt32(dr["Modified_By"]);
                    else
                        pa.Modified_By = null;

                    if (!string.IsNullOrEmpty(Convert.ToString(dr["Date_Of_Modification"])))
                        pa.Date_Of_Modification = Convert.ToDateTime(dr["Date_Of_Modification"]);
                    else
                        pa.Date_Of_Modification = null;

                    if (!string.IsNullOrEmpty(Convert.ToString(dr["Deleted_By"])))
                        pa.Deleted_By = Convert.ToInt32(dr["Deleted_By"]);
                    else
                        pa.Deleted_By = null;

                    if (!string.IsNullOrEmpty(Convert.ToString(dr["Date_Of_Deletion"])))
                        pa.Date_Of_Deletion = Convert.ToDateTime(dr["Date_Of_Deletion"]);
                    else
                        pa.Date_Of_Deletion = null;
                    pa.Active_Status = Convert.ToInt32(dr["Active_Status"]);

                    Pc.Add(pa);

                }
            }
            catch (Exception ex)
            {
                string s = ex.Message;
            }

            return Pc;
        }

        public List<VesselLocation> Get_VesselCurrentLocation(string VesselID, string FleetID, string TelegramType, string BaseURL, string UserID)
        {

            List<VesselLocation> VL = new List<VesselLocation>();
            try
            {
                BaseURL = BaseURL.Replace("@@@", "/");
                BaseURL = BaseURL.Replace("@@", ":");
                BaseURL = BaseURL.Replace("@", "http://");


                DataSet dsData = JiBeDAL.Get_VesselCurrentLocation(Convert.ToInt32(VesselID), Convert.ToInt32(FleetID), TelegramType, Convert.ToInt32(UserID));
                DataTable dtvessellocation = dsData.Tables[0];

                foreach (DataRow dr in dtvessellocation.Rows)
                {
                    if (!string.IsNullOrEmpty(Convert.ToString(dr["Vessel_ID"])))
                    {
                        VesselLocation vl = new VesselLocation();
                        vl.Vessel_ID = Convert.ToInt32(dr["Vessel_ID"]);
                        vl.Vessel_Name = Convert.ToString(dr["Vessel_Name"]);
                        vl.Vessel_Short_Name = Convert.ToString(dr["Vessel_Short_Name"]);
                        string Latitude = JiBeGeneral.Conv_Deg2Decimal_new(Convert.ToDouble(dr["Latitude_Degrees"]), Convert.ToDouble(dr["Latitude_Minutes"]), Convert.ToDouble(dr["Latitude_Seconds"]), Convert.ToString(dr["LATITUDE_N_S"]));
                        vl.Latitude = Convert.ToDouble(Latitude);
                        string Longitude = JiBeGeneral.Conv_Deg2Decimal_new(Convert.ToDouble(dr["Longitude_Degrees"]), Convert.ToDouble(dr["Longitude_Minutes"]), Convert.ToDouble(dr["Longitude_Seconds"]), Convert.ToString(dr["Longitude_E_W"]));
                        vl.Longitude = Convert.ToDouble(Longitude);
                        vl.LonDir = Convert.ToString(dr["Longitude_E_W"]);
                        vl.LatDir = Convert.ToString(dr["LATITUDE_N_S"]);
                        string strVesselCourse = "0";
                        if (!string.IsNullOrEmpty(Convert.ToString(dr["Vessel_Course"])))
                        {
                            strVesselCourse = Convert.ToString(dr["Vessel_Course"]);
                            int index = strVesselCourse.IndexOf(".");
                            if (index > 0)
                                strVesselCourse = strVesselCourse.Substring(0, index);
                        }
                        vl.VesselCourse = strVesselCourse;
                        string content = "<div style=\"text-align:left;\"><span style=\"color: #66777C;font-weight:bold;\">" + Convert.ToString(dr["Vessel_Name"]) + "</span><br/><table width=\"300px\">";
                        content += "<tr><td width=\"120px\">ReportDate:</td><td width=\"180px\"><span style=\"color: blue;font-weight:bold;\">" + Convert.ToString(dr["infodate"]) + "</span></td></tr>";
                        content += "<tr><td>Location:</td><td>" + Convert.ToString(dr["Location_Name"]) + "</td></tr>";
                        content += "<tr><td>Latitude:</td><td>" + Convert.ToString(dr["Latitude_Degrees"]) + " " + Convert.ToString(dr["Latitude_Minutes"]) + " " + Convert.ToString(dr["Latitude_Seconds"]) + " " + Convert.ToString(dr["LATITUDE_N_S"]) + "</td></tr>";
                        content += "<tr><td>Longitude:</td><td>" + Convert.ToString(dr["Longitude_Degrees"]) + " " + Convert.ToString(dr["Longitude_Minutes"]) + " " + Convert.ToString(dr["Longitude_Seconds"]) + " " + Convert.ToString(dr["Longitude_E_W"]) + "</td></tr>";
                        content += "<tr><td>Course:</td><td>" + Convert.ToString(dr["Vessel_Course"]) + "</td></tr>";
                        content += "<tr><td>Wind Direction/Force:</td><td>" + Convert.ToString(dr["Wind_Direction"]) + "/" + Convert.ToString(dr["Wind_Force"]) + "</td></tr>";
                        content += "<tr><td>Average speed:</td><td>" + Convert.ToString(dr["AVERAGE_SPEED"]) + "</td></tr>";
                        string PName = Convert.ToString(dr["PORT_NAME"]);
                        if (string.IsNullOrEmpty(PName))
                        {
                            PName = "Not avialble";
                        }
                        string ETADate = "-";
                        string ETATime = "-";

                        if (!string.IsNullOrEmpty(Convert.ToString(dr["ETA_Next_Port"])))
                        {
                            ETADate = (Convert.ToDateTime(dr["ETA_Next_Port"])).ToString("dd MMM yyyy");
                            ETATime = (Convert.ToDateTime(dr["ETA_Next_Port"])).ToString("HH:mm");
                        }
                        content += "<tr><td><span font-size: x-small;\">Next Port/ETA:</td><td>" + PName + "/" + ETADate + "&nbsp;&nbsp;" + ETATime + "</span></td></tr>";
                        content += "<tr><td><a href='" + BaseURL + "/Crew/CrewList_PhotoView.aspx?vcode=" + dr["Vessel_Short_Name"].ToString() + "' target='_blank'>Crew List</a></td>";
                        content += "<td><a href='" + BaseURL + "/Operations/NoonReport.aspx?LastNoon=" + dr["Vessel_Short_Name"].ToString() + "&ID=" + dr["PKID"].ToString() + "' target='_blank'>Last Noon</a></td></tr>";
                        content += "</table></div>";

                        vl.ToolTipContent = content;
                        vl.SLat = Convert.ToInt32(vl.Latitude).ToString();
                        vl.SLon = Convert.ToInt32(vl.Longitude).ToString();
                        vl.PKID = Convert.ToInt32(dr["PKID"]);
                        VL.Add(vl);
                    }

                }


            }
            catch (Exception ex)
            {
                string s = ex.Message;
            }

            return VL;
        }

        public List<Fleet> Get_FleetList(string UserCompanyID, string VesselManager)
        {
            List<Fleet> Flt = new List<Fleet>();
            try
            {

                DataSet dsData = JiBeDAL.Get_FleetList(Convert.ToInt32(UserCompanyID), Convert.ToInt32(VesselManager));
                DataTable dtfleet = dsData.Tables[0];
                foreach (DataRow dr in dtfleet.Rows)
                {
                    Fleet fl = new Fleet();
                    fl.FleetCode = Convert.ToInt32(dr["fleetcode"]);
                    fl.Code = Convert.ToInt32(dr["code"]);
                    fl.FleetName = Convert.ToString(dr["fleetname"]);
                    fl.Name = Convert.ToString(dr["name"]);
                    fl.Super_MailID = Convert.ToString(dr["Super_MailID"]);
                    fl.TechTeam_MailID = Convert.ToString(dr["TechTeam_MailID"]);
                    fl.Vessel_Owner = Convert.ToInt32(dr["Vessel_Owner"]);
                    fl.Vessel_Manager = Convert.ToInt32(dr["Vessel_Manager"]);
                    Flt.Add(fl);

                }
            }
            catch (Exception ex)
            {
                string s = ex.Message;
            }

            return Flt;
        }

        public List<Vessel> Get_VesselList(string FleetID, string UserCompanyID, string IsVessel)
        {
            List<Vessel> Vsl = new List<Vessel>();
            try
            {

                DataSet dsData = JiBeDAL.Get_VesselList(Convert.ToInt32(FleetID), 0, 0, "", Convert.ToInt32(UserCompanyID), Convert.ToInt32(IsVessel));
                DataTable dtvessel = dsData.Tables[0];
                foreach (DataRow dr in dtvessel.Rows)
                {
                    Vessel ov = new Vessel();
                    ov.Vessel_ID = Convert.ToInt32(dr["Vessel_ID"]);
                    string CheckCode = Convert.ToString(dr["Vessel_Code"]);
                    if (!string.IsNullOrEmpty(CheckCode))
                        ov.Vessel_Code = Convert.ToInt32(dr["Vessel_Code"]);
                    else
                        ov.Vessel_Code = 0;
                    ov.Vessel_Short_Name = Convert.ToString(dr["Vessel_Short_Name"]);
                    ov.Vessel_Name = Convert.ToString(dr["Vessel_Name"]);
                    //ov.Vessel_Manager = Convert.ToInt32(dr["Vessel_Manager"]);
                    //ov.VesselManager = Convert.ToString(dr["VesselManager"]);
                    ov.FleetCode = Convert.ToInt32(dr["FleetCode"]);
                    ov.Vessel_email = Convert.ToString(dr["Vessel_email"]);
                    ov.FleetName = Convert.ToString(dr["FleetName"]);
                    ov.Vessel_Flag = Convert.ToInt32(dr["Vessel_Flag"]);
                    ov.Flag_Name = Convert.ToString(dr["Flag_Name"]);

                    Vsl.Add(ov);

                }
            }
            catch (Exception ex)
            {
                string s = ex.Message;
            }

            return Vsl;
        }
        public List<Vessel> Get_UserVesselList(string FleetID, string UserCompanyID, string IsVessel, string UserID)
        {
            List<Vessel> Vsl = new List<Vessel>();
            try
            {

                DataSet dsData = JiBeDAL.Get_UserVesselList(Convert.ToInt32(FleetID), 0, 0, "", Convert.ToInt32(UserCompanyID), Convert.ToInt32(IsVessel), Convert.ToInt32(UserID));
                DataTable dtvessel = dsData.Tables[0];
                foreach (DataRow dr in dtvessel.Rows)
                {
                    Vessel ov = new Vessel();
                    ov.Vessel_ID = Convert.ToInt32(dr["Vessel_ID"]);
                    string CheckCode = Convert.ToString(dr["Vessel_Code"]);
                    if (!string.IsNullOrEmpty(CheckCode))
                        ov.Vessel_Code = Convert.ToInt32(dr["Vessel_Code"]);
                    else
                        ov.Vessel_Code = 0;
                    ov.Vessel_Short_Name = Convert.ToString(dr["Vessel_Short_Name"]);
                    ov.Vessel_Name = Convert.ToString(dr["Vessel_Name"]);
                    //ov.Vessel_Manager = Convert.ToInt32(dr["Vessel_Manager"]);
                    //ov.VesselManager = Convert.ToString(dr["VesselManager"]);
                    ov.FleetCode = Convert.ToInt32(dr["FleetCode"]);
                    ov.Vessel_email = Convert.ToString(dr["Vessel_email"]);
                    ov.FleetName = Convert.ToString(dr["FleetName"]);
                    ov.Vessel_Flag = Convert.ToInt32(dr["Vessel_Flag"]);
                    ov.Flag_Name = Convert.ToString(dr["Flag_Name"]);

                    Vsl.Add(ov);

                }
            }
            catch (Exception ex)
            {
                string s = ex.Message;
            }

            return Vsl;
        }

        public List<PortList> Get_NearByPorts(string Longitude, string Latitude, string LongDir, string LatDir)
        {
            List<PortList> Pt = new List<PortList>();
            try
            {

                //longitude += ".00";
                //latitude += ".00";
                DataTable dtport = JiBeDAL.Get_Ports_NearVessel_DL(Longitude, Latitude, LongDir, LatDir);
                foreach (DataRow dr in dtport.Rows)
                {
                    if (!string.IsNullOrEmpty(Convert.ToString(dr["PORT_LON"])) && !string.IsNullOrEmpty(Convert.ToString(dr["PORT_LAT"])))
                    {
                        PortList pt = new PortList();
                        pt.PORT_ID = Convert.ToInt32(dr["PORT_ID"]);
                        pt.PORT_NAME = Convert.ToString(dr["PORT_NAME"]);
                        pt.BP_CODE = Convert.ToString(dr["BP_CODE"]);
                        double Lon = JiBeGeneral.ConvertLatLon(Convert.ToString(dr["PORT_LON"]));
                        pt.PORT_LON = Lon;
                        double Lat = JiBeGeneral.ConvertLatLon(Convert.ToString(dr["PORT_LAT"]));
                        pt.PORT_LAT = Lat;

                        pt.OCEAN = Convert.ToString(dr["OCEAN"]);
                        pt.UTC = Convert.ToString(dr["UTC"]);
                        pt.Country_ID = Convert.ToInt32(dr["Country_ID"]);
                        pt.Country_Name = Convert.ToString(dr["PORT_COUNTRY"]);
                        DataTable dtLastPortCall = JiBeDAL.Get_LastPortCallDetails(Convert.ToInt32(dr["PORT_ID"]));
                        if (dtLastPortCall != null && dtLastPortCall.Rows.Count > 0)
                        {
                            pt.ToolTipContent = "<div style=\"text-align:left;\"><span style=\"color: #66777C;font-weight:bold;\">" + Convert.ToString(dr["PORT_NAME"]) + "</span><br/><table width=\"300px\">";
                            pt.ToolTipContent += "<tr><td width=\"120px\">Country:</td><td width=\"180px\">" + Convert.ToString(dr["PORT_COUNTRY"]) + "</td></tr>";
                            pt.ToolTipContent += "<tr><td width=\"120px\">Latitude:</td><td width=\"180px\">" + Convert.ToString(dr["PORT_LAT"]) + "</td></tr>";
                            pt.ToolTipContent += "<tr><td width=\"120px\">Longitude:</td><td width=\"180px\">" + Convert.ToString(dr["PORT_LON"]) + "</td></tr>";
                            pt.ToolTipContent += "<tr><td colspan=\"2\"><fieldset><legend>Last port call made by vessel- " + Convert.ToString(dtLastPortCall.Rows[0]["Vessel_Name"]) + "</legend>";
                            pt.ToolTipContent += "<table><tr><td>Arrival:</td><td>" + Convert.ToString(dtLastPortCall.Rows[0]["Arrival"]) + "</td></tr>";
                            pt.ToolTipContent += "<tr><td>Departure:</td><td>" + Convert.ToString(dtLastPortCall.Rows[0]["Departure"]) + "</td></tr>";
                            pt.ToolTipContent += "</table></fieldset></td></tr></table></div>";
                        }
                        else
                        {
                            pt.ToolTipContent = "<div style=\"text-align:left;\"><span style=\"color: #66777C;font-weight:bold;\">" + Convert.ToString(dr["PORT_NAME"]) + "</span><br/><table width=\"300px\">";
                            pt.ToolTipContent += "<tr><td width=\"120px\">Country:</td><td width=\"180px\">" + Convert.ToString(dr["PORT_COUNTRY"]) + "</td></tr>";
                            pt.ToolTipContent += "<tr><td width=\"120px\">Latitude:</td><td width=\"180px\">" + Convert.ToString(dr["PORT_LAT"]) + "</td></tr>";
                            pt.ToolTipContent += "<tr><td width=\"120px\">Longitude:</td><td width=\"180px\">" + Convert.ToString(dr["PORT_LON"]) + "</td></tr>";
                            pt.ToolTipContent += "<tr><td colspan=\"2\"><fieldset><legend>Last port call details</legend>";
                            pt.ToolTipContent += "<table><tr><td colspan=\"2\">No Port Calls Details available.</td></tr>";
                            pt.ToolTipContent += "</table></fieldset></td></tr></table></div>";
                        }
                        Pt.Add(pt);
                    }

                }
            }
            catch (Exception ex)
            {
                string s = ex.Message;
            }

            return Pt;
        }

        public List<PortList> Get_SelectedPort(string PortID)
        {
            List<PortList> Pt = new List<PortList>();
            try
            {


                DataTable dtport = JiBeDAL.Get_PortDetailsByID_DL(int.Parse(PortID));


                foreach (DataRow dr in dtport.Rows)
                {
                    if (!string.IsNullOrEmpty(Convert.ToString(dr["PORT_LON"])) && !string.IsNullOrEmpty(Convert.ToString(dr["PORT_LAT"])))
                    {

                        string ConversionValue = Convert.ToString(dr["PORT_LON"]);
                        string LatConversionValue = Convert.ToString(dr["PORT_LAT"]);

                        if (ConversionValue.Contains("'") && LatConversionValue.Contains("'"))
                        {
                            PortList pt = new PortList();
                            //JiBeGeneral.Conv_Deg2Decimal_new(Convert.ToDouble(dr["Longitude_Degrees"]), Convert.ToDouble(dr["Longitude_Minutes"]), Convert.ToDouble(dr["Longitude_Seconds"]), Convert.ToString(dr["Longitude_E_W"]));
                            double longDegrees = 0;
                            double longminutes = 0;
                            string longdirections = "";

                            double latDegrees = 0;
                            double latminutes = 0;
                            string latdirections = "";


                            pt.PORT_ID = Convert.ToInt32(dr["PORT_ID"]);
                            pt.PORT_NAME = Convert.ToString(dr["PORT_NAME"]);
                            pt.BP_CODE = Convert.ToString(dr["BP_CODE"]);

                            //Getting Longitude

                            if (ConversionValue.Contains("'"))
                                ConversionValue = ConversionValue.Replace("'", "-");
                            string[] words = ConversionValue.Split('-');
                            for (int i = 0; i < words.Length; i++)
                            {
                                if (i == 0)
                                    longDegrees = Convert.ToDouble(words[i]);
                                if (i == 1)
                                    longminutes = Convert.ToDouble(words[i]);
                                if (i == 2)
                                    longdirections = words[i];
                            }

                            string Longitude = JiBeGeneral.Conv_Deg2Decimal_new(longDegrees, longminutes, 0, longdirections);
                            pt.PORT_LON = Convert.ToDouble(Longitude);
                            //Getting Longitude


                            //Getting Latitude

                            if (LatConversionValue.Contains("'"))
                                LatConversionValue = LatConversionValue.Replace("'", "-");
                            string[] latwords = LatConversionValue.Split('-');
                            for (int i = 0; i < latwords.Length; i++)
                            {
                                if (i == 0)
                                    latDegrees = Convert.ToDouble(latwords[i]);
                                if (i == 1)
                                    latminutes = Convert.ToDouble(latwords[i]);
                                if (i == 2)
                                    latdirections = latwords[i];
                            }

                            string Latitude = JiBeGeneral.Conv_Deg2Decimal_new(latDegrees, latminutes, 0, latdirections);
                            pt.PORT_LAT = Convert.ToDouble(Latitude);
                            //Getting Latitude

                            pt.OCEAN = Convert.ToString(dr["OCEAN"]);
                            pt.UTC = Convert.ToString(dr["UTC"]);
                            pt.Country_ID = Convert.ToInt32(dr["Country_ID"]);
                            pt.Country_Name = Convert.ToString(dr["PORT_COUNTRY"]);

                            DataTable dtLastPortCall = JiBeDAL.Get_LastPortCallDetails(Convert.ToInt32(dr["PORT_ID"]));
                            if (dtLastPortCall != null && dtLastPortCall.Rows.Count > 0)
                            {
                                pt.ToolTipContent = "<div style=\"text-align:left;\"><span style=\"color: #66777C;font-weight:bold;\">" + Convert.ToString(dr["PORT_NAME"]) + "</span><br/><table width=\"300px\">";
                                pt.ToolTipContent += "<tr><td width=\"120px\">Country:</td><td width=\"180px\">" + Convert.ToString(dr["PORT_COUNTRY"]) + "</td></tr>";

                                pt.ToolTipContent += "<tr><td width=\"120px\">Latitude:</td><td width=\"180px\">" + Convert.ToString(dr["PORT_LAT"]) + "</td></tr>";
                                pt.ToolTipContent += "<tr><td width=\"120px\">Longitude:</td><td width=\"180px\">" + Convert.ToString(dr["PORT_LON"]) + "</td></tr>";
                                pt.ToolTipContent += "<tr><td colspan=\"2\"><fieldset><legend>Last port call made by vessel- " + Convert.ToString(dtLastPortCall.Rows[0]["Vessel_Name"]) + "</legend>";
                                pt.ToolTipContent += "<table><tr><td>Arrival:</td><td>" + Convert.ToString(dtLastPortCall.Rows[0]["Arrival"]) + "</td></tr>";
                                pt.ToolTipContent += "<tr><td>Departure:</td><td>" + Convert.ToString(dtLastPortCall.Rows[0]["Departure"]) + "</td></tr>";
                                pt.ToolTipContent += "</table></fieldset></td></tr></table></div>";

                            }
                            else
                            {
                                pt.ToolTipContent = "<div style=\"text-align:left;\"><span style=\"color: #66777C;font-weight:bold;\">" + Convert.ToString(dr["PORT_NAME"]) + "</span><br/><table width=\"300px\">";
                                pt.ToolTipContent += "<tr><td width=\"120px\">Country:</td><td width=\"180px\">" + Convert.ToString(dr["PORT_COUNTRY"]) + "</td></tr>";
                                pt.ToolTipContent += "<tr><td width=\"120px\">Latitude:</td><td width=\"180px\">" + Convert.ToString(dr["PORT_LAT"]) + "</td></tr>";
                                pt.ToolTipContent += "<tr><td width=\"120px\">Longitude:</td><td width=\"180px\">" + Convert.ToString(dr["PORT_LON"]) + "</td></tr>";
                                pt.ToolTipContent += "<tr><td colspan=\"2\"><fieldset><legend>Last port call details</legend>";
                                pt.ToolTipContent += "<table><tr><td colspan=\"2\">No Port Calls Details available.</td></tr>";
                                pt.ToolTipContent += "</table></fieldset></td></tr></table></div>";

                            }
                            Pt.Add(pt);
                        }

                    }

                }
            }
            catch (Exception ex)
            {
                string s = ex.Message;
            }

            return Pt;
        }

        public List<Route> Get_VesselRoute(string VesselID, string FleetID, string FromDate, string ToDate, string TelegramType)
        {
            List<Route> Pt = new List<Route>();
            try
            {
                DateTime? dtFromDate = null;
                DateTime? dtToDate = null;
                string Day = "";
                string Month = "";
                string Year = "";
                string[] DateArray;

                if (!string.IsNullOrEmpty(FromDate))
                {
                    DateArray = FromDate.Split('_');
                    int i = 0;
                    foreach (string word in DateArray)
                    {
                        if (i == 0)
                            Day = word;
                        if (i == 1)
                            Month = word;
                        if (i == 2)
                            Year = word;
                        i++;
                    }
                    FromDate = Year + "/" + Month + "/" + Day;
                    Day = "";
                    Month = "";
                    Year = "";
                    DateArray = null;
                    dtFromDate = Convert.ToDateTime(FromDate);
                }
                if (!string.IsNullOrEmpty(ToDate))
                {
                    DateArray = ToDate.Split('_');
                    int i = 0;
                    foreach (string word in DateArray)
                    {
                        if (i == 0)
                            Day = word;
                        if (i == 1)
                            Month = word;
                        if (i == 2)
                            Year = word;
                        i++;
                    }
                    ToDate = Year + "/" + Month + "/" + Day;
                    Day = "";
                    Month = "";
                    Year = "";
                    DateArray = null;
                    dtToDate = Convert.ToDateTime(ToDate);
                }
                DataTable dtport = JiBeDAL.Get_TelegramData_Route_DL(int.Parse(VesselID), int.Parse(FleetID), dtFromDate, dtToDate, TelegramType);

                foreach (DataRow dr in dtport.Rows)
                {

                    Route pt = new Route();
                    pt.Vessel_ID = Convert.ToInt32(dr["Vessel_ID"]);
                    string Latitude = JiBeGeneral.Conv_Deg2Decimal_new(Convert.ToDouble(dr["Latitude_Degrees"]), Convert.ToDouble(dr["Latitude_Minutes"]), Convert.ToDouble(dr["Latitude_Seconds"]), Convert.ToString(dr["LATITUDE_N_S"]));
                    pt.Latitude = Latitude.Remove(6);
                    //pt.Latitude = Latitude;
                    string Longitude = JiBeGeneral.Conv_Deg2Decimal_new(Convert.ToDouble(dr["Longitude_Degrees"]), Convert.ToDouble(dr["Longitude_Minutes"]), Convert.ToDouble(dr["Longitude_Seconds"]), Convert.ToString(dr["Longitude_E_W"]));
                    pt.Longitude = Longitude.Remove(6);
                    pt.Next_Port = Convert.ToInt32(dr["Next_Port"]);
                    pt.PORT_NAME = Convert.ToString(dr["PORT_NAME"]);
                    //pt.Longitude = Longitude;
                    Pt.Add(pt);

                }
            }
            catch (Exception ex)
            {
                string s = ex.Message;
            }

            return Pt;
        }

        public List<VesselArrivalDepartureByPort> Get_PortArrivalDepatureByVesselRoute(string VesselID, string FleetID, string FromDate, string ToDate, string PortID, string TelegramType)
        {
            List<VesselArrivalDepartureByPort> Pt = new List<VesselArrivalDepartureByPort>();
            try
            {
                DateTime? dtFromDate = null;
                DateTime? dtToDate = null;
                string Day = "";
                string Month = "";
                string Year = "";
                string[] DateArray;

                if (!string.IsNullOrEmpty(FromDate))
                {
                    DateArray = FromDate.Split('_');
                    int i = 0;
                    foreach (string word in DateArray)
                    {
                        if (i == 0)
                            Day = word;
                        if (i == 1)
                            Month = word;
                        if (i == 2)
                            Year = word;
                        i++;
                    }
                    FromDate = Year + "/" + Month + "/" + Day;
                    Day = "";
                    Month = "";
                    Year = "";
                    DateArray = null;
                    dtFromDate = Convert.ToDateTime(FromDate);
                }
                if (!string.IsNullOrEmpty(ToDate))
                {
                    DateArray = ToDate.Split('_');
                    int i = 0;
                    foreach (string word in DateArray)
                    {
                        if (i == 0)
                            Day = word;
                        if (i == 1)
                            Month = word;
                        if (i == 2)
                            Year = word;
                        i++;
                    }
                    ToDate = Year + "/" + Month + "/" + Day;
                    Day = "";
                    Month = "";
                    Year = "";
                    DateArray = null;
                    dtToDate = Convert.ToDateTime(ToDate);
                }

                //DataTable dtport = JiBeDAL.Get_PortDetailsByID_DL(int.Parse(PortID));
                DataTable dtportcalls = JiBeDAL.Get_PortArrivalDepatureByVesselRoute(int.Parse(VesselID), int.Parse(FleetID), dtFromDate, dtToDate, int.Parse(PortID), TelegramType);

                double latdegrees = 0;
                double latminutes = 0;

                string latdirections = "";
                double londegrees = 0;
                double lonminutes = 0;

                string londirections = "";
                string[] parts = null;
                if (dtportcalls.Rows.Count > 0)
                {
                    for (int i = 0; i < dtportcalls.Rows.Count; i++)
                    {
                        string content = "";
                        VesselArrivalDepartureByPort pt = new VesselArrivalDepartureByPort();

                        if (!string.IsNullOrEmpty(Convert.ToString(dtportcalls.Rows[i]["PORT_ID"])) && !string.IsNullOrEmpty(Convert.ToString(dtportcalls.Rows[i]["PORT_NAME"])))
                        {
                            string portlat = Convert.ToString(dtportcalls.Rows[i]["PORT_LAT"]);
                            string portlon = Convert.ToString(dtportcalls.Rows[i]["PORT_LON"]);
                            if (!string.IsNullOrEmpty(portlat) && !string.IsNullOrEmpty(portlon))
                            {
                                pt.PORT_ID = Convert.ToInt32(dtportcalls.Rows[i]["PORT_ID"]);

                                parts = portlat.Split('-');

                                for (int v = 0; v < parts.Length; v++)
                                {
                                    if (v == 0)
                                        latdegrees = Convert.ToDouble(parts[v]);
                                    else
                                        latdirections = parts[v];
                                }
                                parts = latdirections.Split('\'');
                                for (int t = 0; t < parts.Length; t++)
                                {
                                    if (t == 0)
                                        latminutes = Convert.ToDouble(parts[t]);
                                    else
                                        latdirections = parts[t];
                                }


                                parts = portlon.Split('-');

                                for (int v = 0; v < parts.Length; v++)
                                {
                                    if (v == 0)
                                        londegrees = Convert.ToDouble(parts[v]);
                                    else
                                        londirections = parts[v];
                                }
                                parts = londirections.Split('\'');
                                for (int t = 0; t < parts.Length; t++)
                                {
                                    if (t == 0)
                                        lonminutes = Convert.ToDouble(parts[t]);
                                    else
                                        londirections = parts[t];
                                }
                                string Latitude = JiBeGeneral.Conv_Deg2Decimal_new(latdegrees, latminutes, 0, latdirections);
                                pt.PORT_LAT = Latitude;
                                string Longitude = JiBeGeneral.Conv_Deg2Decimal_new(londegrees, lonminutes, 0, londirections);
                                pt.PORT_LON = Longitude;
                                pt.PORT_NAME = Convert.ToString(dtportcalls.Rows[i]["PORT_NAME"]);

                                content = "<div style=\"text-align:left;\"><span style=\"color: #66777C;font-weight:bold;\">" + Convert.ToString(dtportcalls.Rows[i]["PORT_NAME"]) + "</span><br/><table width=\"300px\">";
                                content += "<tr><td width=\"120px\">Country:</td><td width=\"180px\">" + Convert.ToString(dtportcalls.Rows[i]["PORT_COUNTRY"]) + "</td></tr>";
                                content += "<tr><td width=\"120px\">Longitude:</td><td width=\"180px\">" + Convert.ToString(dtportcalls.Rows[i]["PORT_LON"]) + "</td></tr>";
                                content += "<tr><td width=\"120px\">Latitude:</td><td width=\"180px\">" + Convert.ToString(dtportcalls.Rows[i]["PORT_LAT"]) + "</td></tr>";
                                content += "<tr><td width=\"120px\" colspan=\"2\"><fieldset><legend>Port Call Details</legend><br/>";
                                content += "<table><tr><td width=\"120px\">Arrival:</td><td>" + Convert.ToString(dtportcalls.Rows[i]["Arrival"]) + "</td></tr>";
                                content += "<tr><td width=\"120px\">Depature:</td><td>" + Convert.ToString(dtportcalls.Rows[i]["Departure"]) + "</td></tr>";
                                content += "</table></fieldset></td></tr></table></div>";
                                pt.Content = content;
                                if (!string.IsNullOrEmpty(Convert.ToString(dtportcalls.Rows[i]["Arrival"])))
                                    pt.Arrival = Convert.ToString(dtportcalls.Rows[i]["Arrival"]);
                                else
                                    pt.Arrival = "";
                                if (!string.IsNullOrEmpty(Convert.ToString(dtportcalls.Rows[i]["Departure"])))
                                    pt.Departure = Convert.ToString(dtportcalls.Rows[i]["Departure"]);
                                else
                                    pt.Departure = "";
                            }

                            Pt.Add(pt);
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                string s = ex.Message;
            }

            return Pt;

        }

        public List<VesselLocation> PGET_VesselCurrentLocation(VesselCurrentLocation rVesselCurrentLocation)
        {

            List<VesselLocation> VL = new List<VesselLocation>();
            try
            {
                DataSet dsData = JiBeDAL.Get_VesselCurrentLocation(Convert.ToInt32(rVesselCurrentLocation.VesselID), Convert.ToInt32(rVesselCurrentLocation.FleetID), rVesselCurrentLocation.TelegramType, Convert.ToInt32(rVesselCurrentLocation.UserID));
                DataTable dtvessellocation = dsData.Tables[0];

                foreach (DataRow dr in dtvessellocation.Rows)
                {
                    if (!string.IsNullOrEmpty(Convert.ToString(dr["Vessel_ID"])))
                    {
                        VesselLocation vl = new VesselLocation();
                        vl.Vessel_ID = Convert.ToInt32(dr["Vessel_ID"]);
                        vl.Vessel_Name = Convert.ToString(dr["Vessel_Name"]);
                        vl.Vessel_Short_Name = Convert.ToString(dr["Vessel_Short_Name"]);
                        string Latitude = JiBeGeneral.Conv_Deg2Decimal_new(Convert.ToDouble(dr["Latitude_Degrees"]), Convert.ToDouble(dr["Latitude_Minutes"]), Convert.ToDouble(dr["Latitude_Seconds"]), Convert.ToString(dr["LATITUDE_N_S"]));
                        vl.Latitude = Convert.ToDouble(Latitude);
                        string Longitude = JiBeGeneral.Conv_Deg2Decimal_new(Convert.ToDouble(dr["Longitude_Degrees"]), Convert.ToDouble(dr["Longitude_Minutes"]), Convert.ToDouble(dr["Longitude_Seconds"]), Convert.ToString(dr["Longitude_E_W"]));
                        vl.Longitude = Convert.ToDouble(Longitude);
                        vl.LonDir = Convert.ToString(dr["Longitude_E_W"]);
                        vl.LatDir = Convert.ToString(dr["LATITUDE_N_S"]);
                        string strVesselCourse = "0";
                        if (!string.IsNullOrEmpty(Convert.ToString(dr["Vessel_Course"])))
                        {
                            strVesselCourse = Convert.ToString(dr["Vessel_Course"]);
                            int index = strVesselCourse.IndexOf(".");
                            if (index > 0)
                                strVesselCourse = strVesselCourse.Substring(0, index);
                        }
                        vl.VesselCourse = strVesselCourse;
                        string content = "<div style=\"text-align:left;\"><span style=\"color: #66777C;font-weight:bold;\">" + Convert.ToString(dr["Vessel_Name"]) + "</span><br/><table width=\"300px\">";
                        content += "<tr><td width=\"120px\">ReportDate:</td><td width=\"180px\"><span style=\"color: blue;font-weight:bold;\">" + Convert.ToString(dr["infodate"]) + "</span></td></tr>";
                        content += "<tr><td>Location:</td><td>" + Convert.ToString(dr["Location_Name"]) + "</td></tr>";
                        content += "<tr><td>Latitude:</td><td>" + Convert.ToString(dr["Latitude_Degrees"]) + " " + Convert.ToString(dr["Latitude_Minutes"]) + " " + Convert.ToString(dr["Latitude_Seconds"]) + " " + Convert.ToString(dr["LATITUDE_N_S"]) + "</td></tr>";
                        content += "<tr><td>Longitude:</td><td>" + Convert.ToString(dr["Longitude_Degrees"]) + " " + Convert.ToString(dr["Longitude_Minutes"]) + " " + Convert.ToString(dr["Longitude_Seconds"]) + " " + Convert.ToString(dr["Longitude_E_W"]) + "</td></tr>";
                        content += "<tr><td>Course:</td><td>" + Convert.ToString(dr["Vessel_Course"]) + "</td></tr>";
                        content += "<tr><td>Wind Direction/Force:</td><td>" + Convert.ToString(dr["Wind_Direction"]) + "/" + Convert.ToString(dr["Wind_Force"]) + "</td></tr>";
                        content += "<tr><td>Average speed:</td><td>" + Convert.ToString(dr["AVERAGE_SPEED"]) + "</td></tr>";
                        string PName = Convert.ToString(dr["PORT_NAME"]);
                        if (string.IsNullOrEmpty(PName))
                        {
                            PName = "Not avialble";
                        }
                        string ETADate = "-";
                        string ETATime = "-";

                        if (!string.IsNullOrEmpty(Convert.ToString(dr["ETA_Next_Port"])))
                        {
                            ETADate = (Convert.ToDateTime(dr["ETA_Next_Port"])).ToString("dd MMM yyyy");
                            ETATime = (Convert.ToDateTime(dr["ETA_Next_Port"])).ToString("HH:mm");
                        }
                        content += "<tr><td><span font-size: x-small;\">Next Port/ETA:</td><td>" + PName + "/" + ETADate + "&nbsp;&nbsp;" + ETATime + "</span></td></tr>";
                        content += "<tr><td><a href='" + rVesselCurrentLocation.BaseUrl + "Crew/CrewList_PhotoView.aspx?vcode=" + dr["Vessel_Short_Name"].ToString() + "' target='_blank'>Crew List</a></td>";
                        content += "<td><a href='" + rVesselCurrentLocation.BaseUrl + "Operations/NoonReport.aspx?LastNoon=" + dr["Vessel_Short_Name"].ToString() + "&ID=" + dr["PKID"].ToString() + "' target='_blank'>Last Noon</a></td></tr>";
                        content += "</table></div>";

                        vl.ToolTipContent = content;
                        vl.SLat = Convert.ToInt32(vl.Latitude).ToString();
                        vl.SLon = Convert.ToInt32(vl.Longitude).ToString();
                        vl.PKID = Convert.ToInt32(dr["PKID"]);
                        VL.Add(vl);
                    }

                }


            }
            catch (Exception ex)
            {
                string s = ex.Message;
            }

            return VL;
        }
    }
}
