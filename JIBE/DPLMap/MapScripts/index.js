var mapDiv;

//var overview;
var daylight;
var daylightToggle;
var refresher;

//ksc comment
var cloudtoggle;
var clouds;

addEventHandler(window, 'load', windowLoad); //mousemove   load
addEventHandler(window, 'resize', windowResize);

//ksc comment

function windowLoad() {
    mapDiv = document.getElementById('GoogleMap_Div');
    MMMMMMMMMMAp = new GMap2(document.getElementById("GoogleMap_Div"));

    //windowResize();

    if (GBrowserIsCompatible()) {
        daylightToggle = document.getElementById('show_daylight');
        //    addEventHandler(daylightToggle, 'click', daylightClick);
        cloudtoggle = document.getElementById('chck_showclouds');
        //    addEventHandler(cloudtoggle, 'click', cloud_click);
        //MMMMMMMMMMAp = new GMap2(mapDiv, {mapTypes: [G_NORMAL_MAP, G_PHYSICAL_MAP, G_SATELLITE_MAP, G_HYBRID_MAP]});
        MMMMMMMMMMAp.enableDoubleClickZoom();
        //MMMMMMMMMMAp.enableScrollWheelZoom();







        G_HYBRID_MAP.getMinimumResolution =
      G_SATELLITE_MAP.getMinimumResolution =
      G_NORMAL_MAP.getMinimumResolution =
      G_PHYSICAL_MAP.getMinimumResolution =
      function () { return 1 }; //1

        G_SATELLITE_MAP.getMaximumResolution =
      G_PHYSICAL_MAP.getMaximumResolution =
      G_NORMAL_MAP.getMaximumResolution =
      G_HYBRID_MAP.getMaximumResolution =
      function () { return daylightMap.CloudLayer._maxZoom };

        G_HYBRID_MAP.getMaximumResolution =
      G_NORMAL_MAP.getMaximumResolution =
      G_SATELLITE_MAP.getMaximumResolution =
      G_PHYSICAL_MAP.getMaximumResolution =
      function () { return 12 }; //12

        var initLat = parseFloat(getParm('lat'));
        var initLon = parseFloat(getParm('lng'));
        if (isNaN(initLat) ||
        isNaN(initLon))
            var initCenter = new GLatLng(5, -110);
        else
            var initCenter = new GLatLng(initLat, initLon);

        var initZoom = parseInt(getParm('z'));
        if (isNaN(initZoom))
            var initZoom = 8; //2

        MMMMMMMMMMAp.checkResize();
        var initType = getParm('t');
        // The following looks wrong but it's actually OK, the real map type setting occurs a bit later
        if (initType == 'm')
            MMMMMMMMMMAp.setCenter(initCenter, initZoom, G_HYBRID_MAP);
        else
            MMMMMMMMMMAp.setCenter(initCenter, initZoom, G_NORMAL_MAP);

        if (getParm('n') != '')
            daylightToggle.checked = (getParm('n') == '1');
        else if (ieVersion < 7)
            daylightToggle.checked = false;

        if (daylightToggle.checked)
            refresher = window.setInterval('MMMMMMMMMMAp.setZoom(MMMMMMMMMMAp.getZoom())', 5 * 60 * 1000);
        else
            refresher = window.setInterval('MMMMMMMMMMAp.setZoom(MMMMMMMMMMAp.getZoom())', 180 * 60 * 1000);

        //    overview = new GOverviewMapControl();
        //    MMMMMMMMMMAp.addControl(overview);
        //    if (initZoom < 4)
        //      overview.hide(true);

        daylight = new daylightLayer();
        //    daylight.cityLights = false;
        //    daylight.opacity = 0.6;
        //    daylight.active = daylightToggle.checked;
        //    MMMMMMMMMMAp.addOverlay(new GTileLayerOverlay(daylight));

        //    MMMMMMMMMMAp.addOverlay(new GTileLayerOverlay(new cloudLayer()));



        /********** this code is added for dragzoom control ***************/
        //--Bikash--    
        ////       var boxStyleOpts = { opacity: .1, border: "2px solid yellow" };
        ////            var otherOpts = {
        ////             
        ////              buttonHTML: "<img src='dragimages/dragzoomoff.png' alt='Turn Drag Zoom On' title='Turn Drag Zoom On' />",
        ////              buttonZoomingHTML: "<img src='dragimages/dragzoomon.png' alt='Turn Drag Zoom Off' title='Turn Drag Zoom Off' />",
        ////              
        ////              buttonStartingStyle: {width: '15px', height: '15px'},
        ////              overlayRemoveTime: 0 };
        ////            MMMMMMMMMMAp.addControl(new DragZoomControl(boxStyleOpts, otherOpts, {}),
        ////                new GControlPosition(G_ANCHOR_TOP_LEFT, new GSize(48,7)));
        ////                MMMMMMMMMMAp.addControl(new GOverviewMapControl());

        /************* this code is added for dragzoom control ************/

        //daylight.addToMapType(G_SATELLITE_MAP);

        clouds = new daylightMap.CloudLayer();
        clouds.cloudactive = cloudtoggle.checked;
        clouds.addToMap(MMMMMMMMMMAp);

        daylight.active = daylightToggle.checked;
        daylight.addToMap(MMMMMMMMMMAp);


        //code is written for testing purpose  ksc comment
        var a = 10;
        var lastPoint;

        GEvent.addListener(MMMMMMMMMMAp, "mousemove", function (point) {  //click  mousemove

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

            var latLngStrF = point.lat().toFixed(14) + ',' + point.lng().toFixed(14);

            orgLatValue = point.lat().toFixed(14);
            orgLongValue = point.lng().toFixed(14);

            //var txtSearch=document.getElementById("ctl00_ContentPlaceHolder1_txtSearch").value;
            //content1

            //var txtSearch=document.getElementById("ctl00_txtSearch").value;

            //this block of code is used to get the current maptype and this information is used at server side coding

            if (MMMMMMMMMMAp.getCurrentMapType() == G_SATELLITE_MAP) {
                document.getElementById('htxt_sat_nor_hybrid').value = "satmap";
            }

            if (MMMMMMMMMMAp.getCurrentMapType() == G_HYBRID_MAP) {
                document.getElementById('htxt_sat_nor_hybrid').value = "hybridmap";
            }

            if (MMMMMMMMMMAp.getCurrentMapType() == G_NORMAL_MAP) {
                document.getElementById('htxt_sat_nor_hybrid').value = "normalmap";
            }


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



            document.getElementById("txthtmltemp").value = latLngStrF;

            //the following code is used to show the exact time for the particular location with longitude difference


            //for this we need to take base latitude, longitude for particular local region which is from regional settings


            //so Singapore is located on 104 E Longitude.

            var baseLongitude = 77;

            var dest_Longitude = point.lng().toFixed(14);

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
                ///////////

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

                //time_in_string=time_Destination.toLocaleString();

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




                //////////



                gmt_string = GMT_hours + "." + GMT_hourmodulo;

            }

            document.getElementById("txttime").value = time_in_string;
            document.getElementById("txtgmt").value = gmt_string;


            lastPoint = point;
        });   //mousemove listener is completed here

        //
        //code written for temporary purpose ksc comment

        switch (initType) {
            case 'm': MMMMMMMMMMAp.setMapType(G_NORMAL_MAP); break;
            case 'p': MMMMMMMMMMAp.setMapType(G_PHYSICAL_MAP); break;
            case 'h': MMMMMMMMMMAp.setMapType(G_HYBRID_MAP); break;
            default: MMMMMMMMMMAp.setMapType(G_HYBRID_MAP);
        }


        //ksc commented
        MMMMMMMMMMAp.addControl(new GScaleControl());
        MMMMMMMMMMAp.addControl(new GMapTypeControl());
        MMMMMMMMMMAp.addControl(new GLargeMapControl());

        GEvent.addListener(MMMMMMMMMMAp, 'moveend', mapMove);
        GEvent.addListener(MMMMMMMMMMAp, 'maptypechanged', mapTypeChanged);
        GService.GetGoogleObject(fGetGoogleObject);

        
    }

};

function windowResize() {
    //  mapDiv.style.width  = (windowWidth() - 402) + 'px';
    //  mapDiv.style.height = windowHeight() + 'px';
};

function daylightClick() {
    daylight.active = daylightToggle.checked;
    MMMMMMMMMMAp.setZoom(MMMMMMMMMMAp.getZoom());


    if (daylightToggle.checked) {
        refresher = window.setInterval('MMMMMMMMMMAp.setZoom(MMMMMMMMMMAp.getZoom())', 5 * 60 * 1000);
        setParm('n', '1');
    }
    else {
        refresher = window.setInterval('MMMMMMMMMMAp.setZoom(MMMMMMMMMMAp.getZoom())', 180 * 60 * 1000);
        setParm('n', '0'); //0
    }
};


function cloud_click() {
    clouds.cloudactive = cloudtoggle.checked;
    MMMMMMMMMMAp.setZoom(MMMMMMMMMMAp.getZoom());
    

    if (cloudtoggle.checked) {
        refresher = window.setInterval('MMMMMMMMMMAp.setZoom(MMMMMMMMMMAp.getZoom())', 5 * 60 * 1000);
        setParm('n', '1');
    }
    else {
        refresher = window.setInterval('MMMMMMMMMMAp.setZoom(MMMMMMMMMMAp.getZoom())', 180 * 60 * 1000);
        setParm('n', '0'); //0
    }
};

function showPiracyArea_click() {

    var x = new google.maps.LatLng(52.395715, 4.888916);
    var stavanger = new google.maps.LatLng(58.983991, 5.734863);
    var amsterdam = new google.maps.LatLng(52.395715, 4.888916);
    var london = new google.maps.LatLng(51.508742, -0.120850);

    var myTrip = [stavanger, amsterdam, london, stavanger];
    var flightPath = new google.maps.Polygon({
        path: myTrip,
        strokeColor: "#0000FF",
        strokeOpacity: 0.8,
        strokeWeight: 2,
        fillColor: "#0000FF",
        fillOpacity: 0.4
    });

    //MMMMMMMMMMAp.Polygons.Add(flightPath);
    flightPath.setMap(MMMMMMMMMMAp);
    
};

function getParm(name) {
    var result = getCookie(name);
    if (result == '')
        result = getURLParm(name);
    return result;
};

function setParm(name, value) {
    var nextYear = cookieDate(new Date(Number(new Date()) + 365 * _mSecPerDay)) + 'path=/clouds/';
    var expired = 'Sun, 24-Apr-05 00:00:00 GMT;path=/clouds/';

    if (value == null)
        document.cookie = name + '=;expires=' + expired;
    else
        document.cookie = name + '=' + value + ';expires=' + nextYear;
};

//function mapZoom(oldLevel, newLevel)
//{
//  var threshold = 4;
//  if ((oldLevel >= threshold) &&
//      (newLevel < threshold))
//    overview.hide();
//  else if ((oldLevel < threshold) &&
//           (newLevel >= threshold))
//    overview.show();
//};

function mapTypeChanged() {
    switch (MMMMMMMMMMAp.getCurrentMapType()) {
        case G_NORMAL_MAP: setParm('t', 'm'); break;
        case G_PHYSICAL_MAP: setParm('t', 'p'); break;
        case G_HYBRID_MAP: setParm('t', 'h'); break;
        default: setParm('t', null);
    }
};

function mapMove() {
    var center = MMMMMMMMMMAp.getCenter();

    setParm('lat', center.lat());
    setParm('lng', center.lng());
    setParm('z', MMMMMMMMMMAp.getZoom());
};

