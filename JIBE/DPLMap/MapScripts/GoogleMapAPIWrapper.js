//code Written by Subhas.. for ploting the MMMMMMMMMMAp objects on the world MMMMMMMMMMAp
//if any comments contact me on  subhaschbose@hotmail.com
// JScript File
function IgnoreZeroLatLongs(bIgnore)
{
        //Check if there is any visible pushpin on MMMMMMMMMMAp.
        var cnt = 0;
        bounds = new GLatLngBounds();
        for(var i=0;i<marker_Collection.getLength();i++)
        {
            var ignoremarker_ship = false;
            var point1 = marker_Collection.marker_Collection[i].getPoint();
            if(bIgnore)
            {
                if((point1.x==0) && (point1.y==0))
                {
                    ignoremarker_ship = true;
                }
            }
            if(!ignoremarker_ship)
            {
                if(!marker_Collection.marker_Collection[i].isHidden())
                {
                    bounds.extend(marker_Collection.marker_Collection[i].getPoint());
                    //Increment visible pushpin count
                    cnt++;
                }
            }
            
        }
        var iZoomLevel = MMMMMMMMMMAp.getBoundsZoomLevel(bounds);
        var point = bounds.getCenter();
        
        MMMMMMMMMMAp.setZoom(iZoomLevel);
        MMMMMMMMMMAp.setCenter(point);
    
}

  function ShowFullScreenMap()
  {
 
      var objButton = document.getElementById('btnFullScreen');
      if(objButton.value=='Full Screen')
      {
        var objMap = document.getElementById('GoogleMap_Div');
        var objDiv = document.getElementById('GoogleMap_Div_Container');
        objMap.style.width='100%';
        objMap.style.height='100%';

        objDiv.style.position='absolute';
        objDiv.style.left='0px';
        objDiv.style.top='0px';
        objDiv.style.width='99%';
        objDiv.style.height='95%';
        objDiv.style.backgroundColor='LightGrey';

        objButton.value='Close Fullscreen';
        DrawGoogleMap();
      }
      else
      {
        var objMap = document.getElementById('GoogleMap_Div');
        var objDiv = document.getElementById('GoogleMap_Div_Container');
        objMap.style.width='<%=GoogleMapObject.Width %>';
        obj.style.height='<%=GoogleMapObject.Height %>';

        objDiv.style.position='';
        objDiv.style.left='';
        objDiv.style.top='';
        objDiv.style.width='';
        objDiv.style.height='';

        objButton.value='Full Screen';
        //DrawGoogleMap();
      }
}

var MMMMMMMMMMAp;
var trafficInfo = null;

//function fListeners()
//{
//    this.listeners = new Array();
//    this.getLength = function() { return this.listeners.length; };
//    this.pushValue = function(v) { this.listeners.push(v); }
//    this.getValue = function(i)  { return this.listeners[i]; }
//}
function fmarker_Collection()
{
    this.marker_Collection = new Array();
    this.getLength = function() { return this.marker_Collection.length; };
    this.pushValue = function(v) { this.marker_Collection.push(v); }
    this.getValue = function(i)  { return this.marker_Collection[i]; }
    this.getLastValue = function()  { return this.marker_Collection[this.marker_Collection.length-1]; }
    this.getValueById = function(ID)  {
                                        var i;
                                        for(i=0;i<this.marker_Collection.length;i++)
                                        {
                                            if(this.marker_Collection[i].value==ID)
                                            {
                                                //alert('marker_ship found : '+this.marker_Collection[i].value);
                                                return this.marker_Collection[i];
                                            }
                                        } 
                                        return null; 
                                      }
    this.removeValueById = function(ID)  {
                                        var i;
                                        for(i=0;i<this.marker_Collection.length;i++)
                                        {
                                            if(this.marker_Collection[i].value==ID)
                                            {
                                                //alert('marker_ship found : '+this.marker_Collection[i].value);
                                                this.marker_Collection.splice(i,1);
                                                //alert('changed marker_ship removed');

                                            }
                                        } 
                                        return null; 
                                      }
}

function fPolylines()
{
    this.polylines = new Array();
    this.polylinesID = new Array();
    this.getLength = function() { return this.polylines.length; };
    this.pushValue = function(v,ID) {  this.polylines.push(v); this.polylinesID.push(ID); }
    this.getValue = function(i)  { return this.polylines[i]; }
    this.getLastValue = function()  { return this.polylines[this.polylines.length-1]; }
    this.getValueById = function(ID)  {
                                        var i;
                                        for(i=0;i<this.polylinesID.length;i++)
                                        {
                                            if(this.polylinesID[i]==ID)
                                            {
                                               // alert('polyline found : '+this.polylines[i].value);
                                                return this.polylines[i];
                                            }
                                        }
                                        return null;
                                      }
  this.removeValueById = function(ID) {
                                        var i;
                                        for(i=0;i<this.polylinesID.length;i++)
                                        {
                                            if(this.polylinesID[i]==ID)
                                            {
                                                this.polylines.splice(i,1);
                                                this.polylinesID.splice(i,1);
                                            }
                                        }
                                        return null;
                                      }
}

function fPolygons()
{
    this.polygons = new Array();
    this.polygonsID = new Array();
    this.getLength = function() { return this.polygons.length; };
    this.pushValue = function(v,ID) {  this.polygons.push(v); this.polygonsID.push(ID); }
    this.getValue = function(i)  { return this.polygons[i]; }
    this.getLastValue = function()  { return this.polygons[this.polygons.length-1]; }
    this.getValueById = function(ID)  {
                                        var i;
                                        for(i=0;i<this.polygonsID.length;i++)
                                        {
                                            if(this.polygonsID[i]==ID)
                                            {
                                                return this.polygons[i];
                                            }
                                        } 
                                        return null;
                                      }
    this.removeValueById = function(ID)  {
                                        var i;
                                        for(i=0;i<this.polygonsID.length;i++)
                                        {
                                            if(this.polygonsID[i]==ID)
                                            {
                                                this.polygons.splice(i,1);
                                                this.polygonsID.splice(i,1);
                                            }
                                        } 
                                        return null; 
                                      }
}

if (GBrowserIsCompatible())
{
MMMMMMMMMMAp = new GMap2(document.getElementById("GoogleMap_Div"));
var marker_Collection = new fmarker_Collection();
var polylines = new fPolylines();
var polygons = new fPolygons();
//var myEventListeners = new fListeners();
                
var heighchange;

function Createmarker_ship(point,icon1,InfoHTML,bDraggable,sTitle)
{
    var marker_ship;
        marker_ship = new GMarker(point,{icon:icon1,draggable:bDraggable,title: sTitle});
        
              //debugger;
      
        try
        {
        heighchange=document.getElementById('lbl_heightwidth').innerText;
        }
        catch(e)
        {
         heighchange="";
        }
        
        
    if(InfoHTML!='')
    {
        
        //InfoHTML = '<div class="infoWindow" style="width: 204px;height:128px">' + InfoHTML + '</div>';  //min width is 204 px,   204px;height:50px
          
          if(heighchange!=".")
          {
          InfoHTML = '<div style="width: 204px;height:175px">' + InfoHTML + '</div>';
          }
          else
          {
           InfoHTML = '<div style="width: 204px;height:95px">' + InfoHTML + '</div>';
          }
  
       
       GEvent.addListener(marker_ship, "mouseover", function() { this.openInfoWindowHtml(InfoHTML); });
     
        
    }
        //GEvent.addListener(marker_ship, "dragend", function() {  GService.SetLatLon(this.value,this.getLatLng().y,this.getLatLng().x);RaiseEvent('PushpinMoved',this.value);  });
        
       
    return marker_ship;
}

function OpenInfoWindow(id,InfoHTML)
{
    
    var marker_ship = marker_Collection.getValueById(id);
    if(marker_ship!=null)
    {
        marker_ship.openInfoWindowHtml(InfoHTML);
    }
}

function CreatePolyline(points,color,width,isgeodesic)
{
    var polyline;
    if(!isgeodesic)
    {
        polyline = new GPolyline(points,color,width);
    }
    else
    {
        var polyOptions = {geodesic:true};
        polyline = new GPolyline(points,color,width,1,polyOptions);
    }
    return polyline;
}

function CreatePolygon(points,strokecolor,strokeweight,strokeopacity,fillcolor,fillopacity)
{
    var polygon;
    
    var polygon = new GPolygon(points,strokecolor,strokeweight,strokeopacity,fillcolor,fillopacity);
    return polygon;
}

function fGetGoogleObject(result_ksc, userContext)
{
    MMMMMMMMMMAp.setCenter(new GLatLng(result_ksc.CenterPoint.Latitude, result_ksc.CenterPoint.Longitude), result_ksc.ZoomLevel);
    
    if(result_ksc.ShowMapTypesControl)
    {
        MMMMMMMMMMAp.addControl(new GMapTypeControl());
    }

    if(result_ksc.ShowZoomControl)
    {
        MMMMMMMMMMAp.addControl(new GLargeMapControl());
    }
    
    
    MMMMMMMMMMAp.setMapType(eval(result_ksc.MapType));
    
    var i;
    if(marker_Collection!=null)
    {
        for(i=0;i<marker_Collection.getLength();i++)
        {
            var cmark = marker_Collection.getValue(i);
            if(cmark !=null)
            {
                    MMMMMMMMMMAp.removeOverlay(cmark);
            }
        }
    }
//    if(myEventListeners!=null)
//    {
//        for(i=0;i<myEventListeners.getLength();i++)
//        {
//            var lisnr = myEventListeners.getValue(i);
//            if(lisnr!=null)
//            {
//                GEvent.removeListener(lisnr);
//            }
//        }
//    }  
    marker_Collection = new fmarker_Collection();
//    myEventListeners = new fListeners();

    for(i=0;i<result_ksc.Points.length;i++)
    {
        var myIcon_google;

        var myPoint = new GLatLng(result_ksc.Points[i].Latitude, result_ksc.Points[i].Longitude);
        
        myIcon_google = null;
        if(result_ksc.Points[i].IconImage!='')
        {
            myIcon_google = new GIcon(G_DEFAULT_ICON);
            marker_shipOptions = { icon:myIcon_google };
            
            myIcon_google.iconSize = new GSize(result_ksc.Points[i].IconImageWidth,result_ksc.Points[i].IconImageHeight);
            myIcon_google.image = "ShipImages/" +result_ksc.Points[i].IconImage;
            myIcon_google.shadow = result_ksc.Points[i].IconShadowImage;
            myIcon_google.shadowSize = new GSize(result_ksc.Points[i].IconShadowWidth, result_ksc.Points[i].IconShadowHeight);
            myIcon_google.iconAnchor =  new GPoint(result_ksc.Points[i].IconAnchor_posX, result_ksc.Points[i].IconAnchor_posY);
            myIcon_google.infoWindowAnchor = new GPoint(result_ksc.Points[i].InfoWindowAnchor_posX, result_ksc.Points[i].InfoWindowAnchor_posY);
        }
       
        var marker_ship = Createmarker_ship(myPoint,myIcon_google,result_ksc.Points[i].InfoHTML,result_ksc.Points[i].Draggable,result_ksc.Points[i].ToolTip);
        marker_ship.value = result_ksc.Points[i].ID;
        marker_Collection.pushValue(marker_ship);
        MMMMMMMMMMAp.addOverlay(marker_Collection.getLastValue());
    }
    //Add polylines
   // alert('adding polyline');

    polylines = new fPolylines();
    for(i=0;i<result_ksc.Polylines.length;i++)
    {
	 var polypoints = new Array();
	 var j;
	 for(j=0;j<result_ksc.Polylines[i].Points.length;j++)
 	 {
	 	polypoints.push(new GLatLng(result_ksc.Polylines[i].Points[j].Latitude, result_ksc.Polylines[i].Points[j].Longitude));
	 }
        var polyline = CreatePolyline(polypoints,result_ksc.Polylines[i].ColorCode,result_ksc.Polylines[i].Width,result_ksc.Polylines[i].Geodesic);
        polylines.pushValue(polyline,result_ksc.Polylines[i].ID);
        MMMMMMMMMMAp.addOverlay(polylines.getLastValue());
    }
// var polypoints = new Array();
// polypoints.push(new GLatLng(43.65669, -79.44268));
// polypoints.push(new GLatLng(43.66619, -79.44268));
// var poly = CreatePolyline(polypoints,"#66FF00",10,true);
// MMMMMMMMMMAp.addOverlay(poly);

// var polypoints = new Array();
// polypoints.push(new GLatLng(43.65669, -79.44268));
// polypoints.push(new GLatLng(43.66619, -79.44268));
// polypoints.push(new GLatLng(43.67619, -79.44268));
// var directions = new GDirections(MMMMMMMMMMAp,document.getElementById("directions_canvas")); 
//Clear the mapa nd directions of any old information
//directions.clear();

//Load the MMMMMMMMMMAp and directions from the specified waypoints
//directions.loadFromWaypoints(polypoints);


    polygons = new fPolygons();
    for(i=0;i<result_ksc.Polygons.length;i++)
    {
	 var polypoints = new Array();
	 var j;
	 for(j=0;j<result_ksc.Polygons[i].Points.length;j++)
 	 {
	 	polypoints.push(new GLatLng(result_ksc.Polygons[i].Points[j].Latitude, result_ksc.Polygons[i].Points[j].Longitude));
	 }
        var polygon = CreatePolygon(polypoints,result_ksc.Polygons[i].StrokeColor,result_ksc.Polygons[i].StrokeWeight,result_ksc.Polygons[i].StrokeOpacity,result_ksc.Polygons[i].FillColor,result_ksc.Polygons[i].FillOpacity);
        polygons.pushValue(polygon,result_ksc.Polygons[i].ID);
        MMMMMMMMMMAp.addOverlay(polygons.getLastValue());
    }

    
    if(result_ksc.ShowTraffic)
    {
        trafficInfo = new GTrafficOverlay();
        MMMMMMMMMMAp.addOverlay(trafficInfo);
    }
    if(result_ksc.AutomaticBoundaryAndZoom)
    {
        RecenterAndZoom(true,result_ksc);
    }

}

///////////////////////////////////////////////////////////////////////////////////////////////////////////////////

//ksc code



//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////





function DrawGoogleMap()
{
 
  
//  MMMMMMMMMMAp=new GMap2(document.getElementById("GoogleMap_Div"));
//  MMMMMMMMMMAp.setCenter(new GLatLng(80, 80), 1);
    //GService.GetGoogleObject(fGetGoogleObject); 
    
//    if (GBrowserIsCompatible()) {
//        var point;
//        var map=new GMap2(document.getElementById("GoogleMap_Div"));
//        map.addControl(new GOverviewMapControl());

//        map.enableDoubleClickZoom();
//        map.enableScrollWheelZoom();
//        map.addControl(new GMapTypeControl());
//        map.addControl(new GSmallMapControl());

//        map.setCenter(new GLatLng(18.4419,72.1419),1);                
//        map.setMapType(G_HYBRID_MAP); 
//          
//        showPinMark(map);   
//      }

}//draw google MMMMMMMMMMAp 

    function showPinMark(map)
    { 
        var marker = getMyMarker();
        map.addOverlay(marker);          
    }

    function getMyMarker(){
        var popText = "We are here..."
        var marker = new GMarker(new GLatLng(18.4419,72.1419));
        GEvent.addListener(marker, "click", function() {marker.openInfoWindowHtml(popText);}); 
        marker.openInfoWindowHtml(popText);        
        return marker;
    }
    
    function getPortMarkers(){
        
        var arrPorts = eval("[{},{},{}]");
    
        var popText = "We are here..."
        var marker = new GMarker(new GLatLng(18.4419,72.1419));
        GEvent.addListener(marker, "click", function() {marker.openInfoWindowHtml(popText);}); 
        marker.openInfoWindowHtml(popText);        
        return marker;
        
        
    }
 
function fGetGoogleObjectOptimized(result_ksc, userContext)
{
    if(result_ksc.RecenterMap)
    {
        MMMMMMMMMMAp.setCenter(new GLatLng(result_ksc.CenterPoint.Latitude, result_ksc.CenterPoint.Longitude), result_ksc.ZoomLevel);
        GService.RecenterMapComplete();
    }
    
    MMMMMMMMMMAp.setMapType(eval(result_ksc.MapType));

    if(result_ksc.ShowTraffic)
    {
        trafficInfo = new GTrafficOverlay();
        MMMMMMMMMMAp.addOverlay(trafficInfo);
    }
    else
    {
        if(trafficInfo!=null)
        {
            MMMMMMMMMMAp.removeOverlay(trafficInfo);
            trafficInfo = null;
        }
    }

    var i;
    for(i=0;i<result_ksc.Points.length;i++)
    {
        //Create icon
        var myIcon_google;

        var myPoint = new GLatLng(result_ksc.Points[i].Latitude, result_ksc.Points[i].Longitude);
        
        myIcon_google = null;
        if(result_ksc.Points[i].IconImage!='')
        {
            myIcon_google = new GIcon(G_DEFAULT_ICON);
            marker_shipOptions = { icon:myIcon_google };

            myIcon_google.iconSize = new GSize(result_ksc.Points[i].IconImageWidth,result_ksc.Points[i].IconImageHeight);
            myIcon_google.image = result_ksc.Points[i].IconImage;
            //myIcon_google.shadow = result_ksc.Points[i].IconShadowImage;
            myIcon_google.shadow ="";
            myIcon_google.shadowSize = new GSize(0, 0); 
            //myIcon_google.shadowSize = new GSize(result_ksc.Points[i].IconShadowWidth, result_ksc.Points[i].IconShadowHeight);
            myIcon_google.iconAnchor =  new GPoint(result_ksc.Points[i].IconAnchor_posX, result_ksc.Points[i].IconAnchor_posY);
            myIcon_google.infoWindowAnchor = new GPoint(result_ksc.Points[i].InfoWindowAnchor_posX, result_ksc.Points[i].InfoWindowAnchor_posY);

        }
        //Existing marker_ship, but changed.
        if(result_ksc.Points[i].PointStatus=='C')
        {
            var marker_ship = marker_Collection.getValueById(result_ksc.Points[i].ID);
            if(marker_ship!=null)
            {
                marker_Collection.removeValueById(result_ksc.Points[i].ID);
                MMMMMMMMMMAp.removeOverlay(marker_ship);
            }
            var marker_ship = Createmarker_ship(myPoint,myIcon_google,result_ksc.Points[i].InfoHTML,result_ksc.Points[i].Draggable,result_ksc.Points[i].ToolTip);
            marker_ship.value = result_ksc.Points[i].ID;
            marker_Collection.pushValue(marker_ship);
            MMMMMMMMMMAp.addOverlay(marker_Collection.getLastValue());
        }
        //New marker_ship
        if(result_ksc.Points[i].PointStatus=='N')
        {
            var marker_ship = Createmarker_ship(myPoint,myIcon_google,result_ksc.Points[i].InfoHTML,result_ksc.Points[i].Draggable,result_ksc.Points[i].ToolTip);
            marker_ship.value = result_ksc.Points[i].ID;
            marker_Collection.pushValue(marker_ship);
            MMMMMMMMMMAp.addOverlay(marker_Collection.getLastValue());
        }
        //Existing marker_ship, but deleted.
        if(result_ksc.Points[i].PointStatus=='D')
        {
            var marker_ship = marker_Collection.getValueById(result_ksc.Points[i].ID);
            if(marker_ship!=null)
            {
                marker_Collection.removeValueById(result_ksc.Points[i].ID);
                MMMMMMMMMMAp.removeOverlay(marker_ship);
            }
        }
    }
    
    //Get Polylines
    for(i=0;i<result_ksc.Polylines.length;i++)
    {
        //Existing marker_ship, but changed.
        
        if(result_ksc.Polylines[i].LineStatus=='C')
        {
        
            var polyline = polylines.getValueById(result_ksc.Polylines[i].ID);
            if(polyline!=null)
            {
                polylines.removeValueById(result_ksc.Polylines[i].ID);
                MMMMMMMMMMAp.removeOverlay(polyline);
            }
	        var polypoints = new Array();
	        var j;
	        for(j=0;j<result_ksc.Polylines[i].Points.length;j++)
 	        {
	 	        polypoints.push(new GLatLng(result_ksc.Polylines[i].Points[j].Latitude, result_ksc.Polylines[i].Points[j].Longitude));
	        }
            var polyline = CreatePolyline(polypoints,result_ksc.Polylines[i].ColorCode,result_ksc.Polylines[i].Width,result_ksc.Polylines[i].Geodesic);
            polylines.pushValue(polyline,result_ksc.Polylines[i].ID);
            MMMMMMMMMMAp.addOverlay(polylines.getLastValue());
        }
        //New marker_ship

        if(result_ksc.Polylines[i].LineStatus=='N')
        {
	        var polypoints = new Array();
	        var j;
	        for(j=0;j<result_ksc.Polylines[i].Points.length;j++)
 	        {
	 	        polypoints.push(new GLatLng(result_ksc.Polylines[i].Points[j].Latitude, result_ksc.Polylines[i].Points[j].Longitude));
	        }
            var polyline = CreatePolyline(polypoints,result_ksc.Polylines[i].ColorCode,result_ksc.Polylines[i].Width,result_ksc.Polylines[i].Geodesic);
            polylines.pushValue(polyline,result_ksc.Polylines[i].ID);
            MMMMMMMMMMAp.addOverlay(polylines.getLastValue());
        }
        //Existing marker_ship, but deleted.
        if(result_ksc.Polylines[i].LineStatus=='D')
        {
            var polyline = polylines.getValueById(result_ksc.Polylines[i].ID);
            if(polyline!=null)
            {
                polylines.removeValueById(result_ksc.Polylines[i].ID);
                MMMMMMMMMMAp.removeOverlay(polyline);
            }
        }
    }
    
        //Get Polygons
    for(i=0;i<result_ksc.Polygons.length;i++)
    {
        //Existing marker_ship, but changed.

        if(result_ksc.Polygons[i].Status=='C')
        {
        
            var polygon = polygons.getValueById(result_ksc.Polygons[i].ID);
            if(polygon!=null)
            {
                polygons.removeValueById(result_ksc.Polygons[i].ID);
                MMMMMMMMMMAp.removeOverlay(polygon);
            }
	        var polypoints = new Array();
	        var j;
	        for(j=0;j<result_ksc.Polygons[i].Points.length;j++)
 	        {
	 	        polypoints.push(new GLatLng(result_ksc.Polygons[i].Points[j].Latitude, result_ksc.Polygons[i].Points[j].Longitude));
	        }
            var polygon = CreatePolygon(polypoints,result_ksc.Polygons[i].StrokeColor,result_ksc.Polygons[i].StrokeWeight,result_ksc.Polygons[i].StrokeOpacity,result_ksc.Polygons[i].FillColor,result_ksc.Polygons[i].FillOpacity);
            polygons.pushValue(polygon,result_ksc.Polygons[i].ID);
            MMMMMMMMMMAp.addOverlay(polygons.getLastValue());
        }
        //New marker_ship

        if(result_ksc.Polygons[i].Status=='N')
        {
	        var polypoints = new Array();
	        var j;
	        for(j=0;j<result_ksc.Polygons[i].Points.length;j++)
 	        {
	 	        polypoints.push(new GLatLng(result_ksc.Polygons[i].Points[j].Latitude, result_ksc.Polygons[i].Points[j].Longitude));
	        }
            var polygon = CreatePolygon(polypoints,result_ksc.Polygons[i].StrokeColor,result_ksc.Polygons[i].StrokeWeight,result_ksc.Polygons[i].StrokeOpacity,result_ksc.Polygons[i].FillColor,result_ksc.Polygons[i].FillOpacity);
            polygons.pushValue(polygon,result_ksc.Polygons[i].ID);
            MMMMMMMMMMAp.addOverlay(polygons.getLastValue());
        }
        //Existing marker_ship, but deleted.
        if(result_ksc.Polygons[i].Status=='D')
        {
            var polygon = polygons.getValueById(result_ksc.Polygons[i].ID);
            if(polygon!=null)
            {
                polygons.removeValueById(result_ksc.Polygons[i].ID);
                MMMMMMMMMMAp.removeOverlay(polygon);
            }
        }
    }
    if(result_ksc.AutomaticBoundaryAndZoom)
    {
        RecenterAndZoom(true,result_ksc);
    }
}
}

////////////aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa

//This function causes Recentering of MMMMMMMMMMAp. It finds all visible marker_Collection on MMMMMMMMMMAp and decides center point and zoom level based on these marker_Collection.
function RecenterAndZoom(bRecenter,result_ksc)
{
    if(bRecenter)
    {
        //Check if there is any visible pushpin on MMMMMMMMMMAp.
        var cnt = 0;
        bounds = new GLatLngBounds();
        var objIgnore = document.getElementById('chkIgnoreZero');
        var bIgnore = false;
        if(objIgnore!=null)
        {
           bIgnore  = objIgnore.checked;
        }    
        bIgnore = result_ksc.IgnoreZeroLatLngs;
        for(var i=0;i<marker_Collection.getLength();i++)
        {
            var ignoremarker_ship = false;
            if(bIgnore)
            {
                var point1 = marker_Collection.marker_Collection[i].getPoint();
                if((point1.x==0) && (point1.y==0))
                {
                    ignoremarker_ship = true;
                }
            }
            if(!ignoremarker_ship)
            {
                if(!marker_Collection.marker_Collection[i].isHidden())
                {
                    bounds.extend(marker_Collection.marker_Collection[i].getPoint());
                    //Increment visible pushpin count
                    cnt++;
                }
            }
            
        }
        var iZoomLevel = MMMMMMMMMMAp.getBoundsZoomLevel(bounds);
        var point = bounds.getCenter();
        
        if(iZoomLevel>14)
        {
            iZoomLevel = 10; //14 
        }
        
        if(cnt<=0)
        {
            point = new GLatLng(result_ksc.CenterPoint.Latitude,result_ksc.CenterPoint.Longitude);
            iZoomLevel =result_ksc.ZoomLevel;
        }
        MMMMMMMMMMAp.setZoom(iZoomLevel);
        MMMMMMMMMMAp.setCenter(point);
    }
}
function endRequestHandler(sender, args)
{
//    GService.GetOptimizedGoogleObject(fGetGoogleObjectOptimized);
}
//function pageLoad()
//{
////    if(!Sys.WebForms.PageRequestManager.getInstance().get_isInAsyncPostBack())
////        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(endRequestHandler);
// 
//}













