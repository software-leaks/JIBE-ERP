using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace SMS.DPLService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IJibeDPLService" in both code and config file together.
    [ServiceContract]
    public interface IJibeDPLService
    {
        //General Test Method
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/GetGeneral/", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        List<Coordinates> GetGeneral();

        //Method to Get Port List
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/GetPortList/", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        List<Ports> Get_PortList();

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/GetPiracyAreaList/", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        List<PiracyArea> Get_PiracyAreaList();

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/GetVesselCurrentLocation/vesselid/{VesselID}/fleetid/{FleetID}/telegramtype/{TelegramType}/baseurl/{BaseURL}/userid/{UserID}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        List<VesselLocation> Get_VesselCurrentLocation(string VesselID, string FleetID, string TelegramType, string baseurl, string UserID);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/GetFleetList/usercompanyid/{UserCompanyID}/vesselmanager/{VesselManager}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        List<Fleet> Get_FleetList(string UserCompanyID, string VesselManager);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/GetVesselList/fleetid/{FleetID}/usercompanyid/{UserCompanyID}/isvessel/{IsVessel}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        List<Vessel> Get_VesselList(string FleetID, string UserCompanyID, string IsVessel);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/GetNearByPorts/longitude/{Longitude}/latitude/{Latitude}/longdir/{LongDir}/latdir/{LatDir}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        List<PortList> Get_NearByPorts(string Longitude, string Latitude, string LongDir, string LatDir);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/GetSelectedPort/portid/{PortID}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        List<PortList> Get_SelectedPort(string PortID);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/GetVesselRoute/vesselid/{VesselID}/fleetid/{FleetID}/fromdate/{FromDate}/todate/{ToDate}/telegramtype/{TelegramType}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        List<Route> Get_VesselRoute(string VesselID, string FleetID, string FromDate, string ToDate, string TelegramType);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/GetPortArrivalDepatureByVesselRoute/vesselid/{VesselID}/fleetid/{FleetID}/fromdate/{FromDate}/todate/{ToDate}/portid/{PortID}/telegramtype/{TelegramType}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        List<VesselArrivalDepartureByPort> Get_PortArrivalDepatureByVesselRoute(string VesselID, string FleetID, string FromDate, string ToDate, string PortID, string TelegramType);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/GetUserVesselList/fleetid/{FleetID}/usercompanyid/{UserCompanyID}/isvessel/{IsVessel}/userid/{UserID}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        List<Vessel> Get_UserVesselList(string FleetID, string UserCompanyID, string IsVessel, string UserID);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/PGetVesselCurrentLocation/", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedResponse)]
        List<VesselLocation> PGET_VesselCurrentLocation(VesselCurrentLocation rVesselCurrentLocation);

    }
    #region DataContract
    //DataContract
    [DataContract]
    public class Coordinates
    {
        private double _latitude = 0;
        private double _longitude = 0;
        [DataMember]
        public double Latitude
        {
            get { return _latitude; }
            set { _latitude = value; }
        }
        [DataMember]
        public double Longitude
        {
            get { return _longitude; }
            set { _longitude = value; }
        }
    }

    [DataContract]
    public class Ports
    {
        private int _port_id;
        private string _port_name;
        [DataMember]
        public int PORT_ID
        {
            get { return _port_id; }
            set { _port_id = value; }
        }
        [DataMember]
        public string PORT_NAME
        {
            get { return _port_name; }
            set { _port_name = value; }
        }
    }
    [DataContract]
    public class PortList
    {
        private int _port_id;
        private string _port_name;
        private string _bp_code;
        private double _port_lon;
        private double _port_lat;
        private string _ocean;
        private string _utc;
        private int _country_id;
        private string _country_name;
        private string _tooltipcontent;

        [DataMember]
        public int PORT_ID
        {
            get { return _port_id; }
            set { _port_id = value; }
        }
        [DataMember]
        public string PORT_NAME
        {
            get { return _port_name; }
            set { _port_name = value; }
        }
        [DataMember]
        public string BP_CODE
        {
            get { return _bp_code; }
            set { _bp_code = value; }
        }
        [DataMember]
        public double PORT_LON
        {
            get { return _port_lon; }
            set { _port_lon = value; }
        }
        [DataMember]
        public double PORT_LAT
        {
            get { return _port_lat; }
            set { _port_lat = value; }
        }
        [DataMember]
        public string OCEAN
        {
            get { return _ocean; }
            set { _ocean = value; }
        }
        [DataMember]
        public string UTC
        {
            get { return _utc; }
            set { _utc = value; }
        }

        [DataMember]
        public int Country_ID
        {
            get { return _country_id; }
            set { _country_id = value; }
        }
        [DataMember]
        public string Country_Name
        {
            get { return _country_name; }
            set { _country_name = value; }
        }

        [DataMember]
        public string ToolTipContent
        {
            get { return _tooltipcontent; }
            set { _tooltipcontent = value; }
        }

    }

    [DataContract]
    public class PiracyArea
    {
        private int _areaid;
        private int _vertexid;
        private decimal _latitude;
        private decimal _longitude;
        private int? _createdby;
        private DateTime? _date_of_creatation;
        private int? _modified_by;
        private DateTime? _date_of_modification;
        private int? _deleted_by;
        private DateTime? _date_of_deletion;
        private int _active_status;

        [DataMember]
        public int AreaID
        {
            get { return _areaid; }
            set { _areaid = value; }
        }
        [DataMember]
        public int VertexID
        {
            get { return _vertexid; }
            set { _vertexid = value; }
        }
        [DataMember]
        public decimal Latitude
        {
            get { return _latitude; }
            set { _latitude = value; }
        }
        [DataMember]
        public decimal Longitude
        {
            get { return _longitude; }
            set { _longitude = value; }
        }
        [DataMember]
        public int? Created_By
        {
            get { return _createdby; }
            set { _createdby = value; }
        }
        [DataMember]
        public DateTime? Date_Of_Creatation
        {
            get { return _date_of_creatation; }
            set { _date_of_creatation = value; }
        }

        [DataMember]
        public int? Modified_By
        {
            get { return _modified_by; }
            set { _modified_by = value; }
        }

        [DataMember]
        public DateTime? Date_Of_Modification
        {
            get { return _date_of_modification; }
            set { _date_of_modification = value; }
        }

        [DataMember]
        public int? Deleted_By
        {
            get { return _deleted_by; }
            set { _deleted_by = value; }
        }

        [DataMember]
        public DateTime? Date_Of_Deletion
        {
            get { return _date_of_deletion; }
            set { _date_of_deletion = value; }
        }

        [DataMember]
        public int Active_Status
        {
            get { return _active_status; }
            set { _active_status = value; }
        }
    }

    [DataContract]
    public class VesselLocation
    {
        private string _slat;
        private string _slon;
        private int _vessel_id;
        private string _vessel_name;
        private string _vessel_short_name;
        private double _latitude;
        private double _longitude;
        private string _tooltipcontent;
        private string _londir;
        private string _latdir;
        private int _pkid;
        private string _vessel_course = "";
        [DataMember]
        public string VesselCourse
        {
            get { return _vessel_course; }
            set { _vessel_course = value; }
        }
        [DataMember]
        public int Vessel_ID
        {
            get { return _vessel_id; }
            set { _vessel_id = value; }
        }
        [DataMember]
        public string Vessel_Name
        {
            get { return _vessel_name; }
            set { _vessel_name = value; }
        }
        [DataMember]
        public string Vessel_Short_Name
        {
            get { return _vessel_short_name; }
            set { _vessel_short_name = value; }
        }

        [DataMember]
        public double Latitude
        {
            get { return _latitude; }
            set { _latitude = value; }
        }

        [DataMember]
        public double Longitude
        {
            get { return _longitude; }
            set { _longitude = value; }
        }
        [DataMember]
        public string ToolTipContent
        {
            get { return _tooltipcontent; }
            set { _tooltipcontent = value; }
        }
        [DataMember]
        public string LonDir
        {
            get { return _londir; }
            set { _londir = value; }
        }
        [DataMember]
        public string LatDir
        {
            get { return _latdir; }
            set { _latdir = value; }
        }
        [DataMember]
        public int PKID
        {
            get { return _pkid; }
            set { _pkid = value; }
        }

        [DataMember]
        public string SLon
        {
            get { return _slon; }
            set { _slon = value; }
        }
        [DataMember]
        public string SLat
        {
            get { return _slat; }
            set { _slat = value; }
        }
    }

    [DataContract]
    public class Fleet
    {
        private int _fleetcode = 0;
        private int _code = 0;
        private string _fleetname = "";
        private string _name = "";
        private string _super_mailid = "";
        private string _techteam_mailid = "";
        private int _vessel_owner = 0;
        private int _vessel_manager = 0;

        [DataMember]
        public int FleetCode
        {
            get { return _fleetcode; }
            set { _fleetcode = value; }
        }

        [DataMember]
        public int Code
        {
            get { return _code; }
            set { _code = value; }
        }
        [DataMember]
        public string FleetName
        {
            get { return _fleetname; }
            set { _fleetname = value; }
        }
        [DataMember]
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        [DataMember]
        public string Super_MailID
        {
            get { return _super_mailid; }
            set { _super_mailid = value; }
        }
        [DataMember]
        public string TechTeam_MailID
        {
            get { return _techteam_mailid; }
            set { _techteam_mailid = value; }
        }
        [DataMember]
        public int Vessel_Owner
        {
            get { return _vessel_owner; }
            set { _vessel_owner = value; }
        }
        [DataMember]
        public int Vessel_Manager
        {
            get { return _vessel_manager; }
            set { _vessel_manager = value; }
        }

    }

    [DataContract]
    public class Vessel
    {
        private int _vessel_id = 0;
        private int? _vessel_code = null;
        private string _vessel_short_name = "";
        private string _vessel_name = "";
        private int _vessel_manager = 0;
        private string _vesselmanager = "";
        private int _fleetcode = 0;
        private string _vessel_email = "";
        private string _fleetname = "";
        private int _vessel_flag = 0;
        private string _flag_name = "";


        [DataMember]
        public int Vessel_ID
        {
            get { return _vessel_id; }
            set { _vessel_id = value; }
        }
        [DataMember]
        public int? Vessel_Code
        {
            get { return _vessel_code; }
            set { _vessel_code = value; }
        }

        [DataMember]
        public string Vessel_Short_Name
        {
            get { return _vessel_short_name; }
            set { _vessel_short_name = value; }
        }

        [DataMember]
        public string Vessel_Name
        {
            get { return _vessel_name; }
            set { _vessel_name = value; }
        }

        [DataMember]
        public int Vessel_Manager
        {
            get { return _vessel_manager; }
            set { _vessel_manager = value; }
        }
        [DataMember]
        public string VesselManager
        {
            get { return _vesselmanager; }
            set { _vesselmanager = value; }
        }
        [DataMember]
        public int FleetCode
        {
            get { return _fleetcode; }
            set { _fleetcode = value; }
        }
        [DataMember]
        public string Vessel_email
        {
            get { return _vessel_email; }
            set { _vessel_email = value; }
        }
        [DataMember]
        public string FleetName
        {
            get { return _fleetname; }
            set { _fleetname = value; }
        }

        [DataMember]
        public int Vessel_Flag
        {
            get { return _vessel_flag; }
            set { _vessel_flag = value; }
        }

        [DataMember]
        public string Flag_Name
        {
            get { return _flag_name; }
            set { _flag_name = value; }
        }

    }

    [DataContract]
    public class Route
    {
        private int _vessel_id = 0;
        private string _latitude;
        private string _longitude;
        private int _next_port;
        private string _port_name;

        [DataMember]
        public int Vessel_ID
        {
            get { return _vessel_id; }
            set { _vessel_id = value; }
        }
        [DataMember]
        public string Latitude
        {
            get { return _latitude; }
            set { _latitude = value; }
        }

        [DataMember]
        public string Longitude
        {
            get { return _longitude; }
            set { _longitude = value; }
        }

        [DataMember]
        public int Next_Port
        {
            get { return _next_port; }
            set { _next_port = value; }
        }
        [DataMember]
        public string PORT_NAME
        {
            get { return _port_name; }
            set { _port_name = value; }
        }
    }


    [DataContract]
    public class VesselArrivalDepartureByPort
    {
        private int _port_id = 0;
        private string _port_name;
        private string _port_lon;
        private string _port_lat;
        private string _arrival;
        private string _departure;
        private string _content;
        private string _country_name;

        [DataMember]
        public int PORT_ID
        {
            get { return _port_id; }
            set { _port_id = value; }
        }
        [DataMember]
        public string PORT_NAME
        {
            get { return _port_name; }
            set { _port_name = value; }
        }

        [DataMember]
        public string PORT_LON
        {
            get { return _port_lon; }
            set { _port_lon = value; }
        }
        [DataMember]
        public string PORT_LAT
        {
            get { return _port_lat; }
            set { _port_lat = value; }
        }

        [DataMember]
        public string Arrival
        {
            get { return _arrival; }
            set { _arrival = value; }
        }
        [DataMember]
        public string Departure
        {
            get { return _departure; }
            set { _departure = value; }
        }
        [DataMember]
        public string Content
        {
            get { return _content; }
            set { _content = value; }
        }
        [DataMember]
        public string Country_Name
        {
            get { return _country_name; }
            set { _country_name = value; }
        }
    }

    [DataContract]
    public class VesselCurrentLocation
    {
        public string _vesselid = "";
        public string _fleetid = "";
        public string _telegramtype = "";
        public string _baseurl = "";
        public string _userid = "";


        [DataMember]
        public string VesselID
        {
            get { return _vesselid; }
            set { _vesselid = value; }
        }

        [DataMember]
        public string FleetID
        {
            get { return _fleetid; }
            set { _fleetid = value; }
        }

        [DataMember]
        public string TelegramType
        {
            get { return _telegramtype; }
            set { _telegramtype = value; }
        }

        [DataMember]
        public string BaseUrl
        {
            get { return _baseurl; }
            set { _baseurl = value; }
        }

        [DataMember]
        public string UserID
        {
            get { return _userid; }
            set { _userid = value; }
        }
    }
    #endregion

}
