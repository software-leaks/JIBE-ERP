
var baseurl;
var servicebaseurl = "http://localhost:6930/JIBE/JibeDPLService.svc/";
var iconBase = "http://localhost:6930/JIBE/DPLMAP/Images/";
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
var VesselLocatedCount = 0;
var porticon;
var portmarker = [];
var pmarker;
var infowindowNearestports = new google.maps.InfoWindow();


var i = 0;
var map;
var mapProp;
var marker;
var markers = [];
var wcfurl = servicebaseurl + "GetPiracyAreaList/";
var jcoordinates = null;
var cloudLayer;
var weatherLayer;

//function to set Day Night Settings
function LoadDayNight() {

    var isDayNight = document.getElementById("cbxDayNight").checked;
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
        google.maps.event.addListener(map, 'mousemove', function (event) {

            document.getElementById('lblLatLon').innerHTML = event.latLng.lat() + ', ' + event.latLng.lng();
        });

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
        mapTypeId: google.maps.MapTypeId.ROADMAP
    };

    map = new google.maps.Map(document.getElementById("googleMap"), mapProp);

};
//function to create map using google map

//function to plot piracy area
function PlotGraph() {
    $.ajax({
        url: wcfurl,
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
        // PriracyArea.setMap(map);
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

    vesselcurrentLocation = baseurl + "JibeDPLService.svc/GetVesselCurrentLocation/vesselid/" + VesselID + "/fleetid/" + fleetID + "/telegramtype/" + telegramtype;
    $.ajax({
        url: vesselcurrentLocation,
        type: "GET",
        crossDomain: true,
        contentType: contentType,
        cache: false,
        dataType: "json",
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
    if (VesselLocatedCount > 0)
        GetNearByPorts();
    else
        deletePortMarkers();

    document.getElementById('lblTotalShip').innerHTML = "Total Ships Show on Map: " + len.length;
    for (i = 0; i < len.length; i++) {
        var jsonData = JSON.stringify(len);
        var objData = $.parseJSON(jsonData);
        var icon = {
            url: iconBase + "shipicon.png", // url
            scaledSize: new google.maps.Size(40, 40), // scaled size
            origin: new google.maps.Point(0, 0), // origin
            anchor: new google.maps.Point(0, 0) // anchor
        };
        var icon2 = {
            url: iconBase + "redshipico.png", // url
            scaledSize: new google.maps.Size(40, 40), // scaled size
            origin: new google.maps.Point(0, 0), // origin
            anchor: new google.maps.Point(0, 0) // anchor
        };


        nearbyporturl = baseurl + "JibeDPLService.svc/GetNearByPorts/longitude/" + Number(objData[i].SLon) + "/latitude/" + Number(objData[i].SLat) + "/longdir/" + objData[i].LonDir + "/latdir/" + objData[i].LatDir;

        ilocation = new google.maps.LatLng(objData[i].Latitude, objData[i].Longitude);
        if (PriracyArea != null && PriracyArea.Contains(ilocation) && document.getElementById("chkbxPiracyArea").checked == true) {
            marker = new google.maps.Marker({
                position: ilocation,
                animation: google.maps.Animation.BOUNCE,
                icon: icon2,
                map: map
            });
        }
        else {
            marker = new google.maps.Marker({
                position: ilocation,
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
            VesselID = 0;
            fleetID = 0;
            DeleteMarkers();
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
$(document).ready(function () {
    $.support.cors = true;
    createmap();
    baseurl = location.protocol + '//' + location.host + '/Jibe/';
    fleeturl = baseurl + "JibeDPLService.svc/GetFleetList/usercompanyid/1/vesselmanager/0";
    PlotGraph();
    PlotVesselLocation();
    GetFleets();
    //    var ddlfleet = document.getElementById("ddlFleet");

    $(document.getElementById("ddlFleet")).change(function () {
        fleetID = "";
        fleetID = $(this).val();
        DeleteMarkers();
        if (fleetID == 0) {

            deletePortMarkers();
            document.getElementById("cbxNearByPorts").disabled = true;
            document.getElementById("cbxNearByPorts").checked = false;
            $(document.getElementById("ddlVessel")).empty();
            VesselID = 0;
            DeleteRoute()
            PlotVesselLocation();

        }
        else {
            vesselurl = baseurl + "JibeDPLService.svc/GetVesselList/fleetid/" + fleetID + "/usercompanyid/1/isvessel/1";
            deletePortMarkers();
            DeleteRoute();
            GetVessel();
            PlotVesselLocation();
        }
    });
    porturl = baseurl + "JibeDPLService.svc/GetPortList/"
    GetPorts();

    $(document.getElementById("ddlPorts")).change(function () {
        portchangeurl = baseurl + "JibeDPLService.svc/GetSelectedPort/portid/" + $(document.getElementById("ddlPorts")).val()
        GetSelectdPorts();
    });

    $(document.getElementById("rbtnReportType")).change(function () {
        deletePortMarkers();
        DeleteMarkers();
        PlotVesselLocation();
        
    });
    google.maps.event.addListener(map, 'mousemove', function (event) {

        document.getElementById('lblLatLon').innerHTML = event.latLng.lat() + ', ' + event.latLng.lng();
    });
});
//Main Function - END






//function to Get nearest port to a vessel
function GetNearByPorts() {
    if (VesselID > 0) {

        var CHeck = document.getElementById("cbxNearByPorts").checked;

        if (CHeck == false) {
            deletePortMarkers();
        }
        else {
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

    if (FromDate != "" && ToDate != "") {

        if (VesselID != "" && fleetID != "") {
            if (VesselRoute != null) {
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
            viewrouteurl = baseurl + "JibeDPLService.svc/GetVesselRoute/vesselid/" + VesselID + "/fleetid/" + fleetID + "/fromdate/" + ReplaceFromDate + "/todate/" + ReplaceToDate + "/telegramtype/" + Source;
            GetRoute();
        }
        else {

            DeleteRoute();
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
function OnViewRouteSucess(data, status, jqXHR) {
    var len = 0;

    $.each(data, function (key, value) {
        len = value;
    });
    if (len.length > 0) {

        for (i = 0; i < len.length; i++) {
            var jsonData = JSON.stringify(len);
            var objData = $.parseJSON(jsonData);
            iline = new google.maps.LatLng(Number(objData[i].Latitude), Number(objData[i].Longitude));
            plotline[i] = iline;
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
}
function AddRoute() {
    VesselRoute.setMap(map);
}
function DeleteRoute() {
    if (VesselRoute != null)
        VesselRoute.setMap(null);
}
//function to get vessel ROute --END

    