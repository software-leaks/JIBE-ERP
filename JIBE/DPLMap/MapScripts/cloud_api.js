/*
  cloud_api.js
  
  Copyright 2007 Udell Enterprises, Inc
*/

/**
 * The daylightMap global namespace
 *
 * @constructor
 */
var daylightMap = window.daylightMap || {};

//code is commented by subhas
if (location.hostname == 'dev.daylightmap.com')
  daylightMap._serverDomain = 'http://dev.daylightmap.com/';
else
  daylightMap._serverDomain = 'http://www.daylightmap.com/';

/**
 * The daylightMap.CloudLayer object adds clouds (both imagery and, optionally, icons) to a GMap
 *
 * @constructor
 */
daylightMap.CloudLayer = function ()
{
//  debugger;
  this.projection = new GMercatorProjection(daylightMap.CloudLayer._maxZoom);
  this.cloudNexus = {};
  this.cloudMgr = true;  //false
  this.disableIcons = false;
  this.cloudactive = true;// true ksc comment
};

/*
 *  Class properties and methods
 */


daylightMap.CloudLayer._maxZoom = 20;//25//5; //ksc code changes
daylightMap.CloudLayer.tileDir = 'tiles';

//daylightMap.CloudLayer.iconNames = ['cloud_light', 'cloud_medium', 'cloud_heavy',
//  'tropical_depression', 'tropical_storm', 'category_1', 'category_2', 'category_3', 'category_4', 'category_5'];

//daylightMap.CloudLayer.icons = new function () 
//{
//  var name;
// var simag=null;
//  
//  for (var i = 0; i < daylightMap.CloudLayer.iconNames.length; i++) 
//  {
//    name = daylightMap.CloudLayer.iconNames[i];
//    this[name] = new GIcon();
//    //this[name].image            = 'images/ACS.png';
//    this[name].image            = 'images/' + name + '.png';
//    simag=this[name].image+'\n'+simag;
//    this[name].iconSize         = new GSize(31, 31);
//    this[name].iconAnchor       = new GPoint(15, 15);
//    this[name].infoWindowAnchor = new GPoint(15, 14);
//  }
// };

/*
 * Object methods
 */
 
daylightMap.CloudLayer.prototype = new GTileLayer(new GCopyrightCollection(''));

daylightMap.CloudLayer.prototype.isPng = function () 
{
  return true;
};

daylightMap.CloudLayer.prototype.addToMap = function (newMap)
{
  // Add the cloud layer to the supplied GMap2 object
  // var cldhk=  document.getElementById('chck_showclouds');
 
  this.map = newMap;
  var types = this.map.getMapTypes();
  for (var i = 0; i < types.length; i++)
  {
    //this.addToMapType(types[i]);
   //this.addToMapType(G_SATELLITE_MAP);
  }
  this.addToMapType(G_SATELLITE_MAP);
  this.addToMapType(G_HYBRID_MAP);
  
  

  //this.cloudMgr = new MarkerManager(this.map);

  var cloudLayer = this;
  //GEvent.addListener(MMMMMMMMMMAp, 'moveend',        function () {cloudLayer.mapMove()});
  //GEvent.addListener(MMMMMMMMMMAp, 'maptypechanged', function () {cloudLayer.mapTypeChanged()});
 
};

daylightMap.CloudLayer.prototype.addToMapType = function (mapType)
{
  // Add the cloud layer to a single map type (like G_SATELLITE_MAP)
  
  mapType.getTileLayers().splice(1, 0, this);//1,0
  
  //mapType.getTileLayers().splice(0, 0, this);//1,0
};

daylightMap.CloudLayer.prototype.getTileUrl = function (point, zoom)
{
  var result = '';
   if (!this.cloudactive)
    return result;

  if ((this.map.getCurrentMapType() != G_NORMAL_MAP) &&
      (this.map.getZoom() <= daylightMap.CloudLayer._maxZoom))
      {
    result = daylightMap._serverDomain + 'clouds/' + daylightMap.CloudLayer.tileDir + 
             '/ir_' + zoom + '_' + point.x + '_' + point.y + '.png';


     //alert(result);
    //result='ir_2_0_3.png';
   //result='http://www.daylightmap.com/clouds/tiles/ir_2_0_3.png';
      }
   
          
  else if (this.map.getZoom() > 1)
    result = daylightMap._serverDomain + 'clouds/map_tile.png.php?z=' + 
             zoom + '&x=' + point.x + '&y=' + point.y;
  //alert(result);
  
  return result;
};

daylightMap.CloudLayer.prototype.getCopyright = function(bounds, zoom) 
{
  var copyright = '<a href="http://www.DaylightMap.com/clouds" target="_blank">DaylightMap</a>';
  copyright='';//ksc comment
  return {prefix: '', copyrightTexts:[copyright]};
};

//daylightMap.CloudLayer.prototype.refresh = function ()
//{
//  // Refresh the cloud layer & icons

//  if (!this.showIcons())
//  {
//    this.cloudNexus = {};
//    this.cloudMgr.clearMarkers();
//  }

//  // This Zoom kludge refreshes all map tiles and triggers a mapMove
//  if (this.map)
//    this.map.setZoom(this.map.getZoom());
//};

//daylightMap.CloudLayer.prototype.showIcons = function ()
//{
//  if (this.disableIcons) 
//    return false;
//  else
//    switch (this.map.getCurrentMapType()) 
//    {
////ksc comment
////      case G_SATELLITE_MAP:
////      case G_PHYSICAL_MAP:

//        case G_SATELLITE_MAP:
//        case G_HYBRID_MAP:
//        return false;

//      default:
//        return true;
//    }
//};

daylightMap.CloudLayer.prototype.mapTypeChanged = function ()
{
  if (!this.showIcons())
  {
    this.cloudNexus = {};
    this.cloudMgr.clearMarkers();
  }
};

daylightMap.CloudLayer.prototype.mapMove = function ()
{
  if (this.showIcons())
    this.loadCloudIcons();
};

//daylightMap.CloudLayer.prototype.loadCloudIcons = function ()
//{
//  var cloudLayer = this;
//  var bounds = this.map.getBounds();
//  var baseURL = "http://www.daylightmap.com/clouds/";//correct
//  //var baseURL = "http://www.daylightmap.com/clouds/images";
//  var url = baseURL + 'cloud_icons.kml.php?Z=' + this.map.getZoom() + '&BBOX=' + bounds.getSouthWest().lng() + ',' + 
//    bounds.getSouthWest().lat() + ',' + bounds.getNorthEast().lng() + ',' + bounds.getNorthEast().lat();
//   //alert(url);
//   //debugger;
// GDownloadUrl(url, function (text) {cloudLayer.receiveCloudIcons(text)});  //correct

//};

if (!GPoint.distanceFrom)
{
  GPoint.prototype.distanceFrom = function (other)
  {
    return Math.sqrt(Math.pow(this.x - other.x, 2) + Math.pow(this.y - other.y, 2));
  }
}

daylightMap.CloudLayer.prototype.receiveCloudIcons = function (kmlText)
{
  var zoom = this.map.getZoom();
  var coordStr, coords, latLon, style, marker, title, descr;
  var placemarks = GXml.parse(kmlText).getElementsByTagName('Placemark');
  
  for (var i = placemarks.length - 1; i >= 0; i--) 
  {
    coordStr = GXml.value(placemarks[i].getElementsByTagName('coordinates')[0]);
    coords = coordStr.split(',');
    latLon = coordStr.replace(/,.$/, '');
    if (!this.cloudNexus[latLon])
    {
      title = GXml.value(placemarks[i].getElementsByTagName('name')[0]);
      style = GXml.value(placemarks[i].getElementsByTagName('styleUrl')[0]).substr(1);
      descr = placemarks[i].getElementsByTagName('description');

      marker = new GMarker(new GLatLng(parseFloat(coords[0]), parseFloat(coords[1])), 
        {title: title, 
         clickable: (descr.length > 0),
         icon: daylightMap.CloudLayer.icons[style]});

      if (descr.length)
      {
        descr = GXml.value(descr[0]).replace('<![CDATA[', '').replace(']]>', '');
        descr = descr.replace(/<a /g, '<a target="wunderground" ');
        descr = '<h3>' + title.substr(0, title.indexOf(':')) + '</h3>' + descr;
        marker.bindInfoWindowHtml(descr);
      }

      if (coords[2] >= 0)
        this.cloudMgr.addMarker(marker, parseInt(coords[2]));
      else
        this.cloudMgr.addMarker(marker, zoom);

      this.cloudNexus[latLon] = marker;
    }
  }
      
      ///////////////code for testing ksc
//      try{
//      coords[2]="0";
//      descr="hai";
//      marker=new GMarker(new GLatLng(4.31667,99.31667), 
//      {title: "rough", 
//         clickable: (descr.length > 0),
//         icon: daylightMap.CloudLayer.icons["face"]});
//          
//      
//          
//      if (coords[2] >= 0)
//        this.cloudMgr.addMarker(marker, parseInt(coords[2]));
//      else
//        this.cloudMgr.addMarker(marker, zoom);

//      this.cloudNexus[latLon] = marker;
//      
//      }
//      catch(e)
//      {
//        alert(e);
//      }
      ////////////////code for testing ksc
  
  
};
