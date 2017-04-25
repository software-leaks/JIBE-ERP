/*

  daylight.js
  
  JavaScript interface to the DaylightMap.com tile server
  
  Copyright 2006-8 Udell Enterprises, Inc
  
  
  Free for low-traffic use. For more than 20,000 hits/month, see price
  guide at www.daylightmap.com/services.php
  
  
  Instructions for use:
  
  First, add the following line to the <head> section of your HTML:

    <script src="http://www.daylightmap.com/daylight_packed.js" 
            type="text/javascript"></script>

  Then in your JavaScript, after initializing your version 2 Google Map (using a 
  statement like map = new GMap2(document.getElementById('map'));), add the 
  following two lines of code:
  
      var daylight = new daylightMap.daylightLayer();
      daylight.addToMap(map);
  
  obviously, changing "map" to match your own GMap2 object name as needed. Note 
  also that this needs to occur before the first call to map.setCenter() in 
  your code.
  
  You can also add the daylight layer to a single map type (such as Satellite 
  or Hybrid) if desired by using
  
      daylight.addToMapType(G_SATELLITE_MAP);
  
  instead of the addToMap method. Any valid GMapType should work as a parameter.
  
  The daylightLayer object has the following properties you may find useful:
  
  when
      Date/time of the daylight plot. Type: Date. Default: current date/time

  opacity
      Effectively, the "darkness" of the shadow, where 0 is fully transparent (and 
      hence invisible) and 1 is fully opaque. Type: Numeric. Default: 0.5 for map 
      view, 0.8 for satellite/hybrid

  active
      Whether the daylight layer is displayed when the map is redrawn. Type: 
      Boolean. Default: true 

  cityLights
      Whether city lights are displayed on the night side. Type: Boolean. Default: 
      true

*/

var daylightMap = window.daylightMap || {};

daylightMap._nTileSize = 256;//256  /* Note: GMaps does not support other tile sizes (as of v2.38, 2 Mar 06) */
daylightMap._maxZoom = 20;
daylightMap._defaultMapOpacity = 0.5;
daylightMap._defaultSatelliteOpacity = 0.59;
daylightMap._nightMode = false;

// Init the pixel/degree array 
daylightMap._pixelsPerLonDegree = [];
do {
  daylightMap._pixelsPerLonDegree.push(daylightMap._nTileSize * Math.pow(2, daylightMap._pixelsPerLonDegree.length) / 360);
} while (daylightMap._pixelsPerLonDegree.length < daylightMap._maxZoom);

if (location.hostname == 'dev.daylightmap.com')
  daylightMap._serverDomain = 'http://dev.daylightmap.com/';
else
  daylightMap._serverDomain = 'http://www.daylightmap.com/';

daylightMap._clientTime = Number(new Date());
daylightMap._serverTime = daylightMap._clientTime;
document.write('<script src="' + daylightMap._serverDomain + 'tiles/current_unix_time.php?rand=' + Math.random() + '" type="text/javascript"></script>');
daylightMap._timeCorrection = 0;

function roundTo(number, places)
{
  // Rounds the given number to the specifed number of decimal places.
  //  Note that this is similar to the JS numer.toFixed(), but will accept a negative places parm,
  //  which some browsers will not for toFixed().
  //
  //  Examples: roundTo(3.1415926535, 4)  = 3.1416
  //            roundTo(314159.26535, -4) = 310000
  return Math.round(number * Math.pow(10, places)) / Math.pow(10, places);
};


daylightMap.daylightLayer = function()
{
  // daylightLayer object constructor
  
  this.map = null;

  // Set default values for major properties
  this.when = null;
  this.opacity = null;
  this.active = true;
  this.cityLights = null;

  // Internal properties
  this.lastZoom = -1;
  this.lastRefresh = null;

  // These properties are required by GMaps
  this.isPng = function() {return true};
  this.projection = new GMercatorProjection(daylightMap._maxZoom);

  daylightMap._timeCorrection = (daylightMap._serverTime - daylightMap._clientTime);
};

// Compatibility 

if (window.GMap2 &&
    GMap2.length)
  // Looks like we're running in the main Maps API, not as a mapplet
  daylightMap.daylightLayer.prototype = new GTileLayer(new GCopyrightCollection(''), 0, daylightMap._maxZoom);

if (!window.daylightLayer)
  // Backward compatibility with pre-namespace versions of DM
  window.daylightLayer = daylightMap.daylightLayer;

// daylightLayer methods

daylightMap.daylightLayer.prototype.setMap = function()
{
  try {
    if (!this.map)
      this.map = map; // try for a commonly-named global var
  } catch (e) {}
  
  return this.map;
};

daylightMap.daylightLayer.prototype.getCopyright = function(bounds, zoom) 
{
  var opacity = daylightMap._defaultMapOpacity;
  for (var i = 0; i < this.map.getMapTypes().length; i++)
    if (this.map.getMapTypes()[i].opacity)
    {
      opacity = this.map.getMapTypes()[i].opacity;
      break;
    }
  //G_NORMAL_MAP
  if ((this.map.getCurrentMapType() == G_SATELLITE_MAP) || (this.map.getCurrentMapType() == G_NORMAL_MAP) ||
      (this.map.getCurrentMapType() == G_HYBRID_MAP) ||
      (opacity > 0.5))
    var copyright = '<a href="http://www.DaylightMap.com" target="_blank" style="color: white">DaylightMap</a>';
  else
    var copyright = '<a href="http://www.DaylightMap.com" target="_blank">DaylightMap</a>';

//  return copyright; //GMap v2.44-
   //copyright='';
  return {prefix: '', copyrightTexts:[copyright]}; //GMap v2.45+
};

daylightMap.daylightLayer.prototype.getOpacity = function()
{
  if (this.opacity != null)
    // Opacity as been overridden to a specific value
    result = parseFloat(this.opacity);
  else
  {
    // Otherwise, default opacity is adaptive
    if (this.setMap())
    {
      if ((this.map.getCurrentMapType() == G_SATELLITE_MAP) || (this.map.getCurrentMapType() == G_NORMAL_MAP) ||
          (this.map.getCurrentMapType() == G_HYBRID_MAP))
        result = daylightMap._defaultSatelliteOpacity;
      else
        result = daylightMap._defaultMapOpacity;
    }
    else
    {
      if (this.getCityLights())
        result = daylightMap._defaultSatelliteOpacity;
      else
        result = (daylightMap._defaultSatelliteOpacity + daylightMap._defaultMapOpacity) / 2;
    }
  }

  return result.toPrecision(2);  // there's no need for any greater precision than this
};

daylightMap.daylightLayer.prototype.getCityLights = function(mapType)
{
  if (this.cityLights == false)
    // City lights have been specifically turned off
    return false;
  
  else if (this.cityLights == true)
    // City lights have been specifically turned on
    return true;

  else
  {
    // Default cases

    if (!this.setMap())
      return true;
      
    if (!mapType)
      mapType = this.map.getCurrentMapType();
      
    if (daylightMap._nightMode)
      return true;
    else if ((this.map.getCurrentMapType() == G_SATELLITE_MAP) ||
             (this.map.getCurrentMapType() == G_HYBRID_MAP))
      // City lights default to on for satellite-based views 
      return true;
    else
      // City lights default to off for map views
      return false;
  }
};

daylightMap.daylightLayer.prototype.addToMap = function(newMap)
{
  // Add the daylight layer to the supplied GMap2 object
  this.map = newMap;
  var types = this.map.getMapTypes();
  for (var i = 0; i < types.length; i++)
    this.addToMapType(types[i]);
};

daylightMap.daylightLayer.prototype.addToMapType = function(mapType)
{
  // Add the daylight layer to a single map type (like G_SATELLITE_MAP)
  mapType.getTileLayers().splice(1, 0, this);

  if (this.getCityLights(mapType))
    mapType.getMaximumResolution = function(){return 8;};
};

daylightMap.daylightLayer.prototype.refresh = function()
{
  // Refresh the daylight layer

  // This Zoom kludge refreshes all map tiles
  if (this.setMap())
  {
    this.lastZoom = -1;
    this.map.setZoom(this.map.getZoom());
  }
};

daylightMap.daylightLayer.prototype.getTileUrl = function(point, zoom)
{
  // Interface to the tile server
  //  tile_n_2_0_0_1178264700.png
  //  night_4_1_8.png

  if (!this.active)
    return '';

  if (daylightMap._nightMode)
    var tileName = 'tiles/night_png/night_';
  else
  {
    var tileName = 'tiles/cache/tile_';

    if (this.getCityLights())
      tileName += 'n_';
  }

  tileName += zoom + '_' + point.x + '_' + point.y;
  
  if (!daylightMap._nightMode)
  {
    // Establish the date/time to send
    if (this.when != null)
      var timestamp = this.when;
    else
    {
      if (zoom != this.lastZoom)
      {
        if (daylightMap._timeCorrection == 0)
          daylightMap._timeCorrection = (daylightMap._serverTime - daylightMap._clientTime);
  
        // Using current date/time - Round it to the nearest minute and correct to server time
        this.lastRefresh = new Date(60000 * ((Number(new Date()) + daylightMap._timeCorrection) / 60000).toFixed(0));
        this.lastZoom = zoom;
      }
      
      var timestamp = this.lastRefresh;
    }

    // Use some rounding based on zoom level to improve caching of tiles
    timestamp = roundTo(Number(timestamp) / 1000, Math.min(0, zoom - 3));

    tileName += '_' + timestamp;
  }

  tileName = daylightMap._serverDomain + tileName + '.png';

  if (typeof _IG_GetImageUrl == 'function')
    return _IG_GetImageUrl(tileName);
  else
    return tileName;
};

