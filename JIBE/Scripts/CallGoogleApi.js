var smaptype = "SATELLITE";
var baseurl;
var routeportsurl;
var vesselcurrentLocation;
var fleeturl;
var vesselurl;
var porturl;
var nearbyporturl;
var portchangeurl;
var viewrouteurl;
var telegramtype;
var len = 0;
var infowindow = new google.maps.InfoWindow();
var iline;
var plotline = [];
var VesselRoute;
var PTLongitude;
var wcf;
var PTLatitude;
var PTLatDir;
var PTLonDir;
var fleetID = 0;
var ilocation;
var VesselID = 0;
var plotPoints = [];
var PriracyArea;
var contentType = "application/x-www-form-urlencoded; charset=utf-8";
var VesselLocatedCount;
var porticon;
var portmarker = [];
var pmarker;
var infowindowNearestports = new google.maps.InfoWindow();
var applicationurl = "";

var i = 0;
var map;
var mapProp;
var marker;
var markers = [];
var jcoordinates = null;
var cloudLayer;
var weatherLayer;
var DayNightLastStatus = false;
var AutoRefreshStatus;

//function to set Day Night Settings
function LoadDayNight() {
    document.getElementById('lblLatLon').innerHTML = "-,-";
    smaptype = map.getMapTypeId();
    var isDayNight;
    if (document.getElementById("cbxAutoRefresh").checked == true) {
        isDayNight = false;
        if (document.getElementById("cbxDayNight").checked == false) {

            isDayNight = false;
        }

    }
    else {
        isDayNight = document.getElementById("cbxDayNight").checked;
    }

    if (isDayNight == true) {
        new DayNightOverlay({
            map: map
        });
    }
    else {
        createmap();
        PlotGraph();
        PlotVesselLocation();

        if ($(document.getElementById("ddlPorts")).val() > 0)
            GetSelectdPorts();


        var isNearPorts = document.getElementById("cbxNearByPorts").checked;
        if (isNearPorts == true) {
            GetNearByPorts();
        }

        if (document.getElementById("cbxClouds").checked == true)
            LoadWeather();

        if ($(document.getElementById("txtFromDate")).val() != "" && $(document.getElementById("txtToDate")).val() != "")
            ViewRoute();

        if (document.getElementById("cbxAutoRefresh").checked == true) {
            if (document.getElementById("cbxDayNight").checked == true) {
                new DayNightOverlay({
                    map: map
                });
            }
        }


        google.maps.event.addListener(map, 'mousemove', function (event) {

            getLocalAndGMT(event);
        });
    }

    if (smaptype == "SATELLITE" || smaptype == "satellite") {

        map.setMapTypeId(google.maps.MapTypeId.SATELLITE);
    }
    if (smaptype == "ROADMAP" || smaptype == "roadmap") {
        map.setMapTypeId(google.maps.MapTypeId.ROADMAP);
    }
}
//function to set Day Night Settings


//Functions to plot clouds and remove cloud effect
function LoadWeather() {

    var isCloud = document.getElementById("cbxClouds").checked;
    if (isCloud == true) {
        cloudLayer = new google.maps.weather.CloudLayer();
        cloudLayer.setMap(map);
    }
    else {
        UnloadWeather();
    }

}
function UnloadWeather() {
    cloudLayer.setMap(null);
}
//Functions to plot clouds and remove cloud effect ----END



//function to create map using google map
function createmap() {
    mapProp = {
        center: new google.maps.LatLng('20.0000', '47.0000'), zoom: 3,
        mapTypeId: google.maps.MapTypeId.SATELLITE
    };

    map = new google.maps.Map(document.getElementById("googleMap"), mapProp);
};
//function to create map using google map

//function to plot piracy area
function PlotGraph() {
    var plotgraphurl = baseurl + "JibeDPLService.svc/GetPiracyAreaList/";
    $.ajax({
        url: plotgraphurl,
        type: "GET",
        crossDomain: true,
        contentType: contentType,
        cache: false,
        dataType: "json",
        success: OnSuccess,
        error: function (xhr) {
            alert(xhr.responseText);
            if (xhr.responseText) {
                var err = xhr.responseText;
                if (err)
                    error(err);

                error({ Message: "Unknown server error." })
            }
            return;
        }
    });
}

function OnSuccess(data, status, jqXHR) {
    var len = 0;
    var isCheck = document.getElementById("chkbxPiracyArea").checked;
    if (isCheck == true) {
        plotPoints = [];
        PriracyArea = null;

        $.each(data, function (key, value) {
            len = value;
        });
        for (i = 0; i < len.length; i++) {
            var jsonData = JSON.stringify(len);
            var objData = $.parseJSON(jsonData);
            ilocation = new google.maps.LatLng(objData[i].Latitude, objData[i].Longitude);
            plotPoints[i] = ilocation;

        }
        PriracyArea = new google.maps.Polygon({
            path: plotPoints,
            strokeColor: "#D64035",
            strokeOpacity: 0.8,
            strokeWeight: 2,
            fillColor: "#D64035",
            fillOpacity: 0.4
        });
        AddPolygon();
    }
}

function AddPolygon() {
    PriracyArea.setMap(map);
}

function DeletePolygon() {


    var isCheck = document.getElementById("chkbxPiracyArea").checked;
    if (isCheck == false) {
        PriracyArea.setMap(null);
        DeleteMarkers();
        PlotVesselLocation();
    }
    else {
        DeleteMarkers();
        PlotGraph();
        PlotVesselLocation();
    }

}
//function to plot piracy area



//function plot Vessel Current Location

function PlotVesselLocation() {
    var AspRadio = document.getElementById('<%= rbtnReportType.ClientID %>');
    var AspRadio_ListItem = document.getElementsByTagName('input');

    for (var i = 0; i < AspRadio_ListItem.length; i++) {

        if (AspRadio_ListItem[i].checked) {
            telegramtype = AspRadio_ListItem[i].value;
            break;
        }

    }

    var rVesselCurrentLocation = {
        "VesselID": VesselID,
        "FleetID": fleetID,     
        "TelegramType": telegramtype,
        "BaseUrl": baseurl,
        "UserID": sessionuseruserid
        
    };
    
    //vesselcurrentLocation = baseurl + "JibeDPLService.svc/GetVesselCurrentLocation/vesselid/" + VesselID + "/fleetid/" + fleetID + "/telegramtype/" + telegramtype + "/baseurl/" + applicationurl + "/userid/" + sessionuseruserid;
    vesselcurrentLocation = baseurl + "JibeDPLService.svc/PGetVesselCurrentLocation/";
    $.ajax({
        type: "POST",
        url: vesselcurrentLocation,
        data: JSON.stringify(rVesselCurrentLocation),
        contentType: "application/json; charset=utf-8",
        cache: false,
        dataType: "json",
        processdata: true,
        async: false,
        success: OnSuccess2,
        error: function (xhr) {
            alert(xhr.responseText);
            if (xhr.responseText) {
                var err = xhr.responseText;
                if (err)
                    error(err);

                error({ Message: "Unknown server error." })
            }
            return;
        }
    });
}

function OnSuccess2(data, status, jqXHR) {


    google.maps.Polygon.prototype.Contains = function (point) {
        // ray casting alogrithm http://rosettacode.org/wiki/Ray-casting_algorithm
        var crossings = 0,
            path = this.getPath();

        // for each edge
        for (var i = 0; i < path.getLength(); i++) {
            var a = path.getAt(i),
                j = i + 1;
            if (j >= path.getLength()) {
                j = 0;
            }
            var b = path.getAt(j);
            if (rayCrossesSegment(point, a, b)) {
                crossings++;
            }
        }

        // odd number of crossings?
        return (crossings % 2 == 1);

        function rayCrossesSegment(point, a, b) {
            var px = point.lng(),
                py = point.lat(),
                ax = a.lng(),
                ay = a.lat(),
                bx = b.lng(),
                by = b.lat();
            if (ay > by) {
                ax = b.lng();
                ay = b.lat();
                bx = a.lng();
                by = a.lat();
            }
            if (py == ay || py == by) py += 0.00000001;
            if ((py > by || py < ay) || (px > Math.max(ax, bx))) return false;
            if (px < Math.min(ax, bx)) return true;

            var red = (ax != bx) ? ((by - ay) / (bx - ax)) : Infinity;
            var blue = (ax != px) ? ((py - ay) / (px - ax)) : Infinity;
            return (blue >= red);
        }
    };




    $.each(data, function (key, value) {
        len = value;
    });

    VesselLocatedCount = len.length;
    if (VesselLocatedCount > 0) {
        GetNearByPorts();
        document.getElementById("cbxNearByPorts").disabled = false;
        document.getElementById("cbxNearByPorts").checked = false;
    }
    else {
        document.getElementById("cbxNearByPorts").disabled = true;
        document.getElementById("cbxNearByPorts").checked = false;
        deletePortMarkers();
    }

    document.getElementById('lblTotalShip').innerHTML = "Total Ships Show on Map: " + len.length;

    if (len.length > 0)
        DeleteMarkers();



    var iconBase = baseurl + 'Images/';
    for (i = 0; i < len.length; i++) {
        var icon;
        var jsonData = JSON.stringify(len);
        var objData = $.parseJSON(jsonData);
        var shipIconPath = "";



        if (objData[i].VesselCourse > 0 && objData[i].VesselCourse <= 10)
            shipIconPath = iconBase + "shipicon_10.png";
        if (objData[i].VesselCourse > 10 && objData[i].VesselCourse <= 30)
            shipIconPath = iconBase + "shipicon_30.png";
        if (objData[i].VesselCourse > 30 && objData[i].VesselCourse <= 60)
            shipIconPath = iconBase + "shipicon_60.png";
        if (objData[i].VesselCourse > 90 && objData[i].VesselCourse <= 120)
            shipIconPath = iconBase + "shipicon_120.png";
        if (objData[i].VesselCourse > 120 && objData[i].VesselCourse <= 150)
            shipIconPath = iconBase + "shipicon_150.png";
        if (objData[i].VesselCourse > 180 && objData[i].VesselCourse <= 210)
            shipIconPath = iconBase + "shipicon_210.png";
        if (objData[i].VesselCourse > 210 && objData[i].VesselCourse <= 240)
            shipIconPath = iconBase + "shipicon_240.png";
        if (objData[i].VesselCourse > 270 && objData[i].VesselCourse <= 300)
            shipIconPath = iconBase + "shipicon_300.png";
        if (objData[i].VesselCourse > 300 && objData[i].VesselCourse <= 330)
            shipIconPath = iconBase + "shipicon_330.png";
        if (objData[i].VesselCourse == 0)
            shipIconPath = iconBase + "shipicon_360.png";
        if (objData[i].VesselCourse > 60 && objData[i].VesselCourse <= 90)
            shipIconPath = iconBase + "shipicon_90.png";
        if (objData[i].VesselCourse > 330 && objData[i].VesselCourse <= 360)
            shipIconPath = iconBase + "shipicon_360.png";
        if (objData[i].VesselCourse > 150 && objData[i].VesselCourse <= 180)
            shipIconPath = iconBase + "shipicon_180.png";
        if (objData[i].VesselCourse > 240 && objData[i].VesselCourse <= 270)
            shipIconPath = iconBase + "shipicon_270.png";
        icon = {
            //Existing Icons
            url: shipIconPath, // url
            origin: new google.maps.Point(0, 0), // origin
            anchor: new google.maps.Point(0, 0) // anchor

        };
        nearbyporturl = baseurl + "JibeDPLService.svc/GetNearByPorts/longitude/" + Number(objData[i].SLon) + "/latitude/" + Number(objData[i].SLat) + "/longdir/" + objData[i].LonDir + "/latdir/" + objData[i].LatDir;

        ilocation = new google.maps.LatLng(objData[i].Latitude, objData[i].Longitude);
        if (PriracyArea != null && PriracyArea.Contains(ilocation) && document.getElementById("chkbxPiracyArea").checked == true) {
            marker = new google.maps.Marker({
                position: ilocation,
                animation: google.maps.Animation.BOUNCE,
                draggable: false,
                icon: icon,
                map: map

            });
        }
        else {
            marker = new google.maps.Marker({
                position: ilocation,
                draggable: false,
                icon: icon,
                map: map
            });
        }
        google.maps.event.addListener(marker, 'mouseover', (function (marker, i) {
            return function () {
                infowindow.setContent(objData[i].ToolTipContent);
                infowindow.open(map, marker);
            }
        })(marker, i));
        markers.push(marker);
    }

    if (len.length == 1) {

        if ((map.getBounds().contains(markers[0].getPosition()))) {
            map.setCenter(marker.getPosition());
        }
        else {
            var center = new google.maps.LatLng('20.0000', '47.0000');
            map.setCenter(center);

        }
    }
    else {
        var center = new google.maps.LatLng('20.0000', '47.0000');
        map.setCenter(center);
    }


}

function DeleteMarkers() {
    //Loop through all the markers and remove

    for (var i = 0; i < markers.length; i++) {
        markers[i].setMap(null);
    }
    markers = [];

}
//function plot Vessel Current Location

//function get fleet for dropdown population
function GetFleets() {
    $.ajax({
        url: fleeturl,
        type: "GET",
        crossDomain: true,
        contentType: contentType,
        cache: false,
        dataType: "json",
        success: OnFleetSuccess,
        error: function (xhr) {
            alert(xhr.responseText);
            if (xhr.responseText) {
                var err = xhr.responseText;
                if (err)
                    error(err);

                error({ Message: "Unknown server error." })
            }
            return;
        }
    });
}
function OnFleetSuccess(data, status, jqXHR) {

    var len = 0;

    $.each(data, function (key, value) {
        len = value;
    });



    var Dropdown = document.getElementById("ddlFleet");
    var opt = 0;
    var el = document.createElement("option");
    el.text = "--SELECT FLEET--";
    el.value = "0";
    Dropdown.appendChild(el);

    for (i = 0; i < len.length; i++) {
        var jsonData = JSON.stringify(len);
        var objData = $.parseJSON(jsonData);
        var el = document.createElement("option");
        el.text = objData[i].Name;
        el.value = objData[i].Code;
        Dropdown.appendChild(el);
    }
}
//function get fleet for dropdown population - END

//function related to Port Markers
function deletePortMarkers() {
    //Loop through all the markers and remove

    if (portmarker != null) {

        for (var i = 0; i < portmarker.length; i++) {

            portmarker[i].setMap(null);
        }
        portmarker = [];
    }
};
//function related to Port Markers




//function related to Vessel Dropdown population
function GetVessel() {

    $.ajax({
        url: vesselurl,
        type: "GET",
        crossDomain: true,
        contentType: contentType,
        cache: false,
        dataType: "json",
        success: OnVesselSuccess,
        error: function (xhr) {
            alert(xhr.responseText);
            if (xhr.responseText) {
                var err = xhr.responseText;
                if (err)
                    error(err);

                error({ Message: "Unknown server error." })
            }
            return;
        }
    });
}
function OnVesselSuccess(data, status, jqXHR) {
    var urltopage = "";
    var len = 0;
    $(document.getElementById("ddlVessel")).empty();

    var Dropdown = document.getElementById("ddlVessel");
    var opt = 0;
    var el = document.createElement("option");
    el.text = "--SELECT VESSEL--";
    el.value = "0";
    Dropdown.appendChild(el);
    $.each(data, function (key, value) {
        len = value;
    });
    for (i = 0; i < len.length; i++) {
        var jsonData = JSON.stringify(len);
        var objData = $.parseJSON(jsonData);
        var el = document.createElement("option");
        el.text = objData[i].Vessel_Name;
        el.value = objData[i].Vessel_ID;
        Dropdown.appendChild(el);
    }



    $(document.getElementById("ddlVessel")).change(function () {
        document.getElementById('lblRouteDistance').innerHTML = "-";
        deleteRoutePortMarkers();
        VesselID = "";
        VesselID = $(this).val();

        urltopage = "";
        if (VesselID > 0) {

            DeleteRoute();
            DeleteMarkers();
            PlotVesselLocation();
            deletePortMarkers();

            if (VesselLocatedCount > 0) {

                document.getElementById("cbxNearByPorts").disabled = false;
                document.getElementById("cbxNearByPorts").checked = false;

            }
            else {
                document.getElementById("cbxNearByPorts").disabled = true;
                document.getElementById("cbxNearByPorts").checked = false;
            }
            urltopage = baseurl + "Operations/Default.aspx?v='" + fleetID + "," + VesselID + "'";
            $(document.getElementById("hlnkDPLDetails")).attr("href", urltopage);


        }
        else {

            DeleteRoute();
            DeleteMarkers();
            VesselID = 0;
            PlotVesselLocation();
            deletePortMarkers();
            $(document.getElementById("cbxNearByPorts")).disabled = true;
            $(document.getElementById("cbxNearByPorts")).checked = false;
            $(document.getElementById("hlnkDPLDetails")).attr("href", baseurl + "Operations/Default.aspx?v='0,0");

        }
    });


}
//function related to Vessel Dropdown population ---END


//function related to Ports Dropdown population 
function GetPorts() {
    $.ajax({
        url: porturl,
        type: "GET",
        crossDomain: true,
        contentType: contentType,
        cache: false,
        dataType: "json",
        success: OnPortSuccess,
        error: function (xhr) {
            alert(xhr.responseText);
            if (xhr.responseText) {
                var err = xhr.responseText;
                if (err)
                    error(err);

                error({ Message: "Unknown server error." })
            }
            return;
        }
    });
}
function OnPortSuccess(data, status, jqXHR) {
    var len = 0;

    var Dropdown = document.getElementById("ddlPorts");
    var opt = 0;
    var el = document.createElement("option");
    el.text = "--SELECT PORT--";
    el.value = "0";
    Dropdown.appendChild(el);
    $.each(data, function (key, value) {
        len = value;
    });
    for (i = 0; i < len.length; i++) {
        var jsonData = JSON.stringify(len);
        var objData = $.parseJSON(jsonData);
        var el = document.createElement("option");
        el.text = objData[i].PORT_NAME;
        el.value = objData[i].PORT_ID;
        Dropdown.appendChild(el);
    }

}
//function related to Ports Dropdown population -- END

//function to plot selected port
function GetSelectdPorts() {

    deleteSlectedPortMarkers();

    if ($(document.getElementById("ddlPorts")).val() > 0) {
        deleteSlectedPortMarkers();
    }
    else
        alert("To view a port. Please select one.");
    $.ajax({
        url: portchangeurl,
        type: "GET",
        crossDomain: true,
        contentType: contentType,
        cache: false,
        dataType: "json",
        success: OnGetSelectedPortsSuccess,
        error: function (xhr) {
            alert(xhr.responseText);
            if (xhr.responseText) {
                var err = xhr.responseText;
                if (err)
                    error(err);

                error({ Message: "Unknown server error." })
            }
            return;
        }
    });
}

var infowindowSelectedports = new google.maps.InfoWindow();
var spmarker;
var spmarkers = [];
function OnGetSelectedPortsSuccess(data, status, jqXHR) {

    var len = 0;
    $.each(data, function (key, value) {
        len = value;
    });

    for (i = 0; i < len.length; i++) {
        var jsonData = JSON.stringify(len);
        var objData = $.parseJSON(jsonData);

        ilocation = new google.maps.LatLng(objData[i].PORT_LAT, objData[i].PORT_LON);
        spmarker = new google.maps.Marker({
            position: ilocation,
            map: map
        });

        google.maps.event.addListener(spmarker, 'mouseover', (function (spmarker, i) {
            return function () {
                infowindowSelectedports.setContent(objData[i].ToolTipContent);
                infowindowSelectedports.open(map, spmarker);
            }
        })(spmarker, i));
        spmarkers.push(spmarker);
    }
    if (len.length == 1) {

        if ((map.getBounds().contains(spmarkers[0].getPosition()))) {
            map.setCenter(spmarkers[0].getPosition());

        }
        else {
            var center = new google.maps.LatLng('20.0000', '47.0000');
            map.setCenter(center);

        }
    }
    else {
        var center = new google.maps.LatLng('20.0000', '47.0000');
        map.setCenter(center);
    }

}

function deleteSlectedPortMarkers() {
    if (spmarkers != null) {
        for (var i = 0; i < spmarkers.length; i++) {
            spmarkers[i].setMap(null);
        }
        spmarkers = [];
    }
}
//function to plot selected port --END




//Main Function
var sessionuseruserid = 0;
window.onload = function () {


    var sessionusercompanyid = "0";
    if ($(document.getElementById("hdfUserCompanyID")).val() != "")
        sessionusercompanyid = $(document.getElementById("hdfUserCompanyID")).val();

    if ($(document.getElementById("hdfUserID")).val() != "")
        sessionuseruserid = $(document.getElementById("hdfUserID")).val();
    $.support.cors = true;
    var geturl = window.location.protocol + "//" + window.location.host;

    var pathArray = window.location.pathname.split('/');

    var newPathname = "";
    var ppath = "";
    for (i = 0; i < pathArray.length; i++) {
        if (i == 1) {
            newPathname += "/";
            newPathname += pathArray[i] + "/";
            ppath = "/" + pathArray[i];

        }
    }

    applicationurl = geturl + '' + ppath;
    applicationurl = applicationurl.replace("http://", "@");
    applicationurl = applicationurl.replace(":", "@@");
    applicationurl = applicationurl.replace(new RegExp(/\//g), "@@@");
    applicationurl = applicationurl.replace("http:", "@");

    createmap();
    $("#pageloaddiv").fadeOut(20000);
    baseurl = location.protocol + '//' + location.host + '' + newPathname;

    fleeturl = baseurl + "JibeDPLService.svc/GetFleetList/usercompanyid/" + sessionusercompanyid + "/vesselmanager/0";
    PlotGraph();
    PlotVesselLocation();
    GetFleets();
    vesselurl = baseurl + "JibeDPLService.svc/GetUserVesselList/fleetid/0/usercompanyid/" + sessionusercompanyid + "/isvessel/1/userid/" + sessionuseruserid;
    GetVessel();


    $(document.getElementById("ddlFleet")).change(function () {
        fleetID = "";
        fleetID = $(this).val();
        DeleteMarkers();

        if (fleetID == 0) {

            deletePortMarkers();
            document.getElementById("cbxNearByPorts").disabled = true;
            document.getElementById("cbxNearByPorts").checked = false;
            DeleteRoute();
            vesselurl = baseurl + "JibeDPLService.svc/GetVesselList/fleetid/0/usercompanyid/" + sessionusercompanyid + "/isvessel/1";
            GetVessel();
            VesselID = 0;
            PlotVesselLocation();

        }
        else {
            vesselurl = baseurl + "JibeDPLService.svc/GetVesselList/fleetid/" + fleetID + "/usercompanyid/" + sessionusercompanyid + "/isvessel/1";
            deletePortMarkers();
            DeleteRoute();
            GetVessel();
            PlotVesselLocation();
        }
    });
    porturl = baseurl + "JibeDPLService.svc/GetPortList/"
    GetPorts();

    $(document.getElementById("ddlPorts")).change(function () {
        portchangeurl = baseurl + "JibeDPLService.svc/GetSelectedPort/portid/" + $(document.getElementById("ddlPorts")).val();

        GetSelectdPorts();
    });

    $(document.getElementById("rbtnReportType")).change(function () {
        var AspRadio = document.getElementById('<%= rbtnReportType.ClientID %>');
        var AspRadio_ListItem = document.getElementsByTagName('input');

        for (var i = 0; i < AspRadio_ListItem.length; i++) {

            if (AspRadio_ListItem[i].checked) {
                Source = AspRadio_ListItem[i].value;
                telegramtype = AspRadio_ListItem[i].value;
                break;
            }

        }

        if (Source == "N") {
            DeleteRoute();
            deleteRoutePortMarkers();
            deletePortMarkers();
            DeleteMarkers();
            deleteSlectedPortMarkers();
            PlotVesselLocation();
            if (VesselLocatedCount > 0) {
                $(document.getElementById("cbxNearByPorts")).disabled = false;
            }

            if (document.getElementById("cbxNearByPorts").checked == true) {
                GetNearByPorts();
            }
            else {
                deletePortMarkers();
            }
            if ($(document.getElementById("ddlPorts")).val() > 0)
                GetSelectdPorts();
            if (document.getElementById("cbxClouds").checked == true)
                LoadWeather();

            if ($(document.getElementById("txtFromDate")).val() != "" && $(document.getElementById("txtToDate")).val() != "")
                ViewRoute();

            var checkdata = document.getElementById('lblTotalShip').innerHTML;
            if (checkdata != "") {
                if (checkdata.indexOf('0') > -1) {
                    document.getElementById("cbxNearByPorts").disabled = false;
                    document.getElementById("cbxNearByPorts").checked = false;
                    deletePortMarkers();
                }
                else {
                    document.getElementById("cbxNearByPorts").disabled = true;
                    GetNearByPorts();
                }

            }
        }
        if (Source == "P") {

            VesselLocatedCount = 0;
            DeleteRoute();
            deleteRoutePortMarkers();
            deletePortMarkers();
            DeleteMarkers();
            //deleteSlectedPortMarkers();
            document.getElementById("cbxNearByPorts").checked = false;
        }

    });
    google.maps.event.addListener(map, 'mousemove', function (event) {
        getLocalAndGMT(event);
    });

}
//Main Function - END






//function to Get nearest port to a vessel
function GetNearByPorts() {
    if (VesselID > 0) {

        var CHeck = document.getElementById("cbxNearByPorts").checked;

        if (CHeck == false) {
            deletePortMarkers();
        }
        else {
            if (VesselLocatedCount > 0)
                GetNearestPorts();
        }
    }
    else {
        deletePortMarkers();
    }
}

function GetNearestPorts() {

    $.ajax({

        url: nearbyporturl,
        type: "GET",
        crossDomain: true,
        contentType: contentType,
        cache: false,
        dataType: "json",
        success: OnNearestPorts,
        error: function (xhr) {
            alert(xhr.responseText);
            if (xhr.responseText) {
                var err = xhr.responseText;
                if (err)
                    error(err);

                error({ Message: "Unknown server error." })
            }
            return;
        }
    });

}


function OnNearestPorts(data, status, jqXHR) {
    var len = 0;
    deletePortMarkers();
    $.each(data, function (key, value) {
        len = value;
    });
    for (i = 0; i < len.length; i++) {
        var jsonData = JSON.stringify(len);
        var objData = $.parseJSON(jsonData);
        ilocation = new google.maps.LatLng(objData[i].PORT_LAT, objData[i].PORT_LON);
        pmarker = new google.maps.Marker({
            position: ilocation,
            map: map
        });

        google.maps.event.addListener(pmarker, 'mouseover', (function (pmarker, i) {
            return function () {
                infowindowNearestports.setContent(objData[i].ToolTipContent);
                infowindowNearestports.open(map, pmarker);
            }
        })(pmarker, i));
        portmarker.push(pmarker);

    }
}
//function to Get nearest port to a vessel ---END



//function to get vessel Route
var FromDate;
var ToDate;
var Source;
function ViewRoute() {
    FromDate = "";
    ToDate = "";
    document.getElementById("txtFromDate")
    FromDate = $(document.getElementById("txtFromDate")).val();
    ToDate = $(document.getElementById("txtToDate")).val();
    deleteRoutePortMarkers();
    if (FromDate != "" && ToDate != "") {
        //        ViewRoute
        if (VesselID != "") {
            if (VesselRoute != "") {
                DeleteRoute();
            }
            var AspRadio = document.getElementById('<%= rbtnReportType.ClientID %>');
            var AspRadio_ListItem = document.getElementsByTagName('input');

            for (var i = 0; i < AspRadio_ListItem.length; i++) {

                if (AspRadio_ListItem[i].checked) {
                    Source = AspRadio_ListItem[i].value;
                    telegramtype = AspRadio_ListItem[i].value;
                    break;
                }

            }

            var ReplaceFromDate = FromDate.replace(new RegExp(/\//g), "_");

            var ReplaceToDate = ToDate.replace(new RegExp(/\//g), "_");
            var routeSource = "A";
            viewrouteurl = baseurl + "JibeDPLService.svc/GetVesselRoute/vesselid/" + VesselID + "/fleetid/" + fleetID + "/fromdate/" + ReplaceFromDate + "/todate/" + ReplaceToDate + "/telegramtype/" + Source;
            GetRoute();
        }
        else {

            DeleteRoute();
            document.getElementById('lblRouteDistance').innerHTML = "-";
            alert("Please select a vessel");
        }
    }
    else {

        DeleteRoute();
        if (FromDate == "" || ToDate == "")
            alert("From and To fields cannot blank.");


    }
}

function GetRoute() {

    $.ajax({
        url: viewrouteurl,
        type: "GET",
        crossDomain: true,
        contentType: contentType,
        cache: false,
        dataType: "json",
        success: OnViewRouteSucess,
        error: function (xhr) {
            alert(xhr.responseText);
            if (xhr.responseText) {
                var err = xhr.responseText;
                if (err)
                    error(err);

                error({ Message: "Unknown server error." })
            }
            return;
        }
    });

}
var PortAD = [];
function isInArray(array, search) {
    return array.indexOf(search) >= 0;
}
function OnViewRouteSucess(data, status, jqXHR) {
    var len = 0;
    var AD = 0;
    DeleteRoute();
    $.each(data, function (key, value) {
        len = value;
    });
    plotline = [];
    if (len.length > 0) {

        for (i = 0; i < len.length; i++) {

            var jsonData = JSON.stringify(len);
            var objData = $.parseJSON(jsonData);

            iline = new google.maps.LatLng(Number(objData[i].Latitude), Number(objData[i].Longitude));
            plotline[i] = iline;
            var resultAD = isInArray(PortAD, objData[i].Next_Port);
            if (resultAD == false) {
                PortAD[AD] = objData[i].Next_Port;
                AD++;
            }
        }


        VesselRoute = new google.maps.Polyline({
            path: plotline,
            strokeColor: "#0000FF",
            strokeOpacity: 0.8,
            strokeWeight: 2
        });
        AddRoute();

    }
    else {
        DeleteRoute();
        alert("No route detail available for the dates");
    }
    GetRoutePorts();
}

function AddRoute() {
    if (VesselRoute != null) {
        var heading = "";
        heading = google.maps.geometry.spherical.computeLength(VesselRoute.getPath());
        heading = heading * 0.000621371192;
        heading = heading * 0.868976;
        VesselRoute.setMap(map);
        document.getElementById('lblRouteDistance').innerHTML = heading;
    }


}
function DeleteRoute() {
    if (VesselRoute != null) {
        VesselRoute.setMap(null);
    }
}
//function to get vessel ROute --END

function HideShowDiv() {
    var buttontext = $(document.getElementById("lbtnSettings")).val();

    if (buttontext == "Hide Settings") {
        $(document.getElementById("divsettings")).hide();
        document.getElementById('divsettings').style.display = "none";
        $('#grid-container').css({ "height": "860px" });
        $('.map_width').css({ "height": "860px" });
        $(document.getElementById("lbtnSettings")).attr('value', 'Show Settings');
        $(document.getElementById("imgTopPin")).attr('src', '../../Images/floatingfooter_butn_active.png');
    }
    else {
        $(document.getElementById("divsettings")).show();
        document.getElementById('divsettings').style.display = 'block';
        $('#grid-container').css({ "height": "690px" });
        $('.map_width').css({ "height": "690px" });
        $(document.getElementById("lbtnSettings")).attr('value', 'Hide Settings');
        $(document.getElementById("imgTopPin")).attr('src', '../../Images/floatingfooter_butn.png');
    }
}


//Function to get Ports in between Vessel Route

function GetRoutePorts() {
    var newTelegramType = "A";
    //    for (var i = 0; i < PortAD.length; i++) {
    var datefrom = $(document.getElementById("txtFromDate")).val();
    var dateto = $(document.getElementById("txtToDate")).val();
    var ReplaceFromDate = datefrom.replace(new RegExp(/\//g), "_");
    var ReplaceToDate = dateto.replace(new RegExp(/\//g), "_");
    routeportsurl = "";
    routeportsurl = baseurl + "JibeDPLService.svc/GetPortArrivalDepatureByVesselRoute/vesselid/" + VesselID + "/fleetid/" + fleetID + "/fromdate/" + ReplaceFromDate + "/todate/" + ReplaceToDate + "/portid/" + PortAD[0] + "/telegramtype/" + newTelegramType;

    $.ajax({
        url: routeportsurl,
        type: "GET",
        crossDomain: true,
        contentType: contentType,
        cache: false,
        dataType: "json",
        success: OnRotePortsSucess,
        error: function (xhr) {
            alert(xhr.responseText);
            if (xhr.responseText) {
                var err = xhr.responseText;
                if (err)
                    error(err);

                error({ Message: "Unknown server error." })
            }
            return;
        }
    });
    //    }
}
var routeportmarker;
var routeportmarkers = [];
var infowindowrouteports = new google.maps.InfoWindow();
var iRoutePorts;
function OnRotePortsSucess(data, status, jqXHR) {
    var len = 0;
    $.each(data, function (key, value) {
        len = value;
    });

    for (i = 0; i < len.length; i++) {

        var jsonData = JSON.stringify(len);
        var objData = $.parseJSON(jsonData);
        var iconBase = baseurl + 'Images/';


        var myPosition = new google.maps.LatLng(objData[i].PORT_LAT, objData[i].PORT_LON);

        //        if (google.maps.geometry.poly.isLocationOnEdge(myPosition, VesselRoute, 5)) {

        iRoutePorts = new google.maps.LatLng(objData[i].PORT_LAT, objData[i].PORT_LON);

        routeportmarker = new google.maps.Marker({
            position: iRoutePorts,
            icon: iconBase + "Port.gif",
            draggable: false,
            optimized: true,
            animation: google.maps.Animation.DROP,
            map: map
        });

        google.maps.event.addListener(routeportmarker, 'mouseover', (function (routeportmarker, i) {
            return function () {
                infowindowrouteports.setContent(objData[i].Content);
                infowindowrouteports.open(map, routeportmarker);
            }
        })(routeportmarker, i));
        routeportmarkers.push(routeportmarker);
    }
    //    }

}


function deleteRoutePortMarkers() {
    if (routeportmarkers != null) {
        for (var i = 0; i < routeportmarkers.length; i++) {
            routeportmarkers[i].setMap(null);
        }
        routeportmarkers = [];

    }
}
//Function to get Ports in between Vessel Route

var IsAutoRefresh;
//function to call set auto-refresh Cloud and Day Night Settings
function AutoRefresh() {
    if (document.getElementById("cbxClouds").checked == true || document.getElementById("cbxDayNight").checked == true) {
        if (document.getElementById("btnAutoRefresh").value == "Auto-Refresh ON") {
            document.getElementById("cbxAutoRefresh").checked = true;
            document.getElementById("btnAutoRefresh").value = "Auto-Refresh OFF";
        }
        else {
            document.getElementById("cbxAutoRefresh").checked = false;
            clearInterval(countdownTimer);
            document.getElementById("btnAutoRefresh").value = "Auto-Refresh ON";
            document.getElementById('countdown').innerHTML = "Auto Refresh is OFF";
        }

    }

    var isAutoRefresh = document.getElementById("cbxAutoRefresh").checked;
    if (isAutoRefresh == true) {
        if (document.getElementById("cbxClouds").checked == true || document.getElementById("cbxDayNight").checked == true) {
            seconds = 900;
            countdownTimer = setInterval('secondPassed()', 1000);
        }
        else {
            clearInterval(countdownTimer);
            document.getElementById('countdown').innerHTML = "Auto Refresh is OFF";
            document.getElementById("btnAutoRefresh").value = "Auto-Refresh ON";
        }
    }
    else {
        clearInterval(countdownTimer);
        document.getElementById('countdown').innerHTML = "Auto Refresh is OFF";
        document.getElementById("btnAutoRefresh").value = "Auto-Refresh ON";
    }
}
//function to call set auto-refresh Cloud and Day Night Settings

//function to Set CountDown Timer
var seconds = 900;
var countdownTimer;
function secondPassed() {
    var minutes = Math.round((seconds - 30) / 60);
    var remainingSeconds = seconds % 60;
    if (remainingSeconds < 10) {
        remainingSeconds = "0" + remainingSeconds;
    }
    document.getElementById('countdown').innerHTML = minutes + ":" + remainingSeconds;

    if (seconds == 0) {
        clearInterval(countdownTimer);
        seconds = 900;
        countdownTimer = setInterval('secondPassed()', 1000);
        LoadDayNight();
    }
    else {
        seconds--;
    }
}
//function to set CountDown Timer


//function to get Local time and GMT time difference
function getLocalAndGMT(event) {
    var time_Destination;
    var latDegree;
    var latMin;
    var latSec;
    var latDir;

    var longDegree;
    var longMin;
    var longSec;
    var longDir;

    var orgLatValue;
    var orgLongValue;

    var latLngStrF = event.latLng.lat().toFixed(14) + ',' + event.latLng.lng().toFixed(14);

    orgLatValue = event.latLng.lat().toFixed(14);
    orgLongValue = event.latLng.lng().toFixed(14);

    /*********************/
    //the following logic is used for converting the decimal lat/long to Degree, Mins, Secs, Direction

    if (orgLatValue.substr(0, 1) == "-") {
        latDir = "S";
        orgLatValue = orgLatValue.substr(1, orgLatValue.length - 1);
    }

    else {
        latDir = "N";
        orgLatValue = orgLatValue;
    }

    if (orgLongValue.substr(0, 1) == "-") {
        longDir = "W"; //old value E
        orgLongValue = orgLongValue.substr(1, orgLongValue.length - 1);
    }
    else {
        longDir = "E"; //old value W
        orgLongValue = orgLongValue;
    }

    // degrees = degrees
    orgLatValue = orgLatValue.split(".");
    latDegree = orgLatValue[0];

    orgLongValue = orgLongValue.split(".");
    longDegree = orgLongValue[0];

    // * 60 = mins
    ddLatRemainder = ("0." + orgLatValue[1]) * 60;
    var latMinVals = ddLatRemainder.toString().split(".");
    latMin = latMinVals[0];

    ddLongRemainder = ("0." + orgLongValue[1]) * 60;
    var longMinVals = ddLongRemainder.toString().split(".");
    longMin = longMinVals[0];

    // * 60 again = secs
    ddLatMinRemainder = ("0." + latMinVals[1]) * 60;
    latSec = Math.round(ddLatMinRemainder);

    ddLongMinRemainder = ("0." + longMinVals[1]) * 60;
    longSec = Math.round(ddLongMinRemainder);

    /***********************/
    //lat, long

    latLngStrF = latDegree + " " + latMin + " " + latSec + " " + latDir + ", " + longDegree + " " + longMin + " " + longSec + " " + longDir;
    document.getElementById('lblLatLon').innerHTML = latLngStrF;


    //the following code is used to show the exact time for the particular location with longitude difference
    //for this we need to take base latitude, longitude for particular local region which is from regional settings
    //so Singapore is located on 104 E Longitude.
    var baseLongitude = 77;
    var dest_Longitude = event.latLng.lng().toFixed(14);
    var timediference_degres;
    var timediference_mins;
    var timediference_hours;
    var timedifernce_secs;
    var date_Local = new Date();
    var time_local = date_Local.getTime();
    var hours_sec = date_Local.getHours();
    var min_sec = date_Local.getMinutes();
    var sec_sec = date_Local.getSeconds();
    var localtime_secs;
    var time_in_string;
    var time_gmt;
    var gmt_string;
    //handle the round off for mins exceeds the 60 units. in this case add 1 unit to the hour part.
    var time_round_mins;
    var min_part;
    var hour_part_cal;
    var strtmeround;
    var sreqstring;
    var sminmodulo;
    var disp_hour;
    var amorpm;

    //determine the sign of the place with respect to longitude and gmt
    if (baseLongitude > dest_Longitude) {
        //it's means we need to subtract the time

        //find the time difference in hours
        timediference_degres = (baseLongitude) - (dest_Longitude); //these are in degrees
        timediference_mins = (timediference_degres) * 4;
        timediference_hours = (timediference_mins) / 60;
        //time_Destination=(time_local)-(timediference_hours);
        //it's better to take the time difference in the seconds for calculation
        timedifernce_secs = (timediference_mins) * 60;
        //convert local time into sec's
        localtime_secs = ((hours_sec) * 3600) + (min_sec * 60) + (sec_sec);
        time_Destination = (localtime_secs) - (timedifernce_secs);
        time_Destination = (time_Destination) / 3600;
        time_round_mins = time_Destination.toString().split(".");  //0 is having hours part, 1 is having mins part
        min_part;
        hour_part_cal;
        strtmeround = time_round_mins[1] + '';
        sreqstring = strtmeround.substring(0, 2);
        sminmodulo = (sreqstring) % 60;
        if (sminmodulo == sreqstring) {
            min_part = sreqstring;
            hour_part_cal = 0;
        }
        else {
            hour_part_cal = 1;
            min_part = sminmodulo;
        }
        disp_hour = Number(time_round_mins[0]) + Number(hour_part_cal);
        if (Number(disp_hour) < 12) {
            amorpm = "AM";
        }
        else {
            disp_hour = Number(disp_hour) - 12;
            amorpm = "PM";
        }
        if (Number(disp_hour) == 0) {
            disp_hour = "12";
        }
        time_in_string = disp_hour + "." + min_part + " " + amorpm;
        time_gmt = dest_Longitude / 15;
        ///////////
        var gmtime_full = dest_Longitude / 15;
        var GMT_hours = gmtime_full.toString().split(".");
        var GMT_hourmodulo = "0";
        GMT_hourmodulo = GMT_hours[1] % 60;
        if (GMT_hourmodulo == 1) {
            GMT_hours = Number(GMT_hours[0]) + 1;

        }
        else {
            GMT_hours = Number(GMT_hours[0]) + 0;
        }
        gmt_string = GMT_hours + "." + GMT_hourmodulo;
    }
    else {
        //it's means we need to add the time
        timediference_degres = Math.abs((baseLongitude) - (dest_Longitude)); //these are in degrees
        timediference_mins = (timediference_degres) * 4;
        timediference_hours = (timediference_mins) / 60;
        //time_Destination=(time_local)-(timediference_hours);
        //it's better to take the time difference in the seconds for calculation
        timedifernce_secs = (timediference_mins) * 60;
        //convert local time into sec's
        localtime_secs = ((hours_sec) * 3600) + (min_sec * 60) + (sec_sec);
        time_Destination = (localtime_secs) + (timedifernce_secs);
        time_Destination = (time_Destination) / 3600;
        //round off the mins 60 mins to 1 hour to the hour part
        //orgLatValue = orgLatValue.split(".");
        time_round_mins = time_Destination.toString().split(".");  //0 is having hours part, 1 is having mins part
        min_part;
        hour_part_cal;
        strtmeround = time_round_mins[1] + '';
        sreqstring = strtmeround.substring(0, 2);
        sminmodulo = (sreqstring) % 60;
        if (sminmodulo == sreqstring) {
            min_part = sreqstring;
            hour_part_cal = 0;
        }
        else {
            hour_part_cal = 1;
            min_part = sminmodulo;
        }
        disp_hour = Number(time_round_mins[0]) + Number(hour_part_cal);
        if (Number(disp_hour) < 12) {
            amorpm = "AM";
        }
        else {
            disp_hour = Number(disp_hour) - 12;
            amorpm = "PM";
        }

        if (Number(disp_hour) == 0) {
            disp_hour = "12";
        }
        time_in_string = disp_hour + "." + min_part + " " + amorpm;
        time_gmt = dest_Longitude / 15;
        var gmtime_full = dest_Longitude / 15;
        var GMT_hours = gmtime_full.toString().split(".");
        var GMT_hourmodulo = "0";
        GMT_hourmodulo = GMT_hours[1] % 60;
        if (GMT_hourmodulo == 1) {
            GMT_hours = Number(GMT_hours[0]) + 1;
        }
        else {
            GMT_hours = Number(GMT_hours[0]) + 0;
        }

        gmt_string = GMT_hours + "." + GMT_hourmodulo;

    }
    document.getElementById('lblLocalTime').innerHTML = time_in_string;
    document.getElementById('lblGMT').innerHTML = gmt_string;
    lastPoint = event;
}
//function to get Local time and GMT time difference