//<![CDATA[
/*
    common.js

    Common routines useful throughout my JavaScript code base.
*/

function documentPath()
{
  // Returns the "path" section of the document's URL. Examples:
  //  URL = http://www.domain.com/file.ext            =>  documentPath = /
  //  URL = http://www.domain.com/dir1/dir2/file.ext  =>  documentPath = /dir1/dir2/

  var path = window.location.pathname;

  // Find the last forward-slash, separating the source file name from its dir path
  return path.slice(0, path.search(/\/[^\/]*$/) + 1);
};
//alert(document.URL + '\n' + window.location.host + '  ' + documentPath());

function xmlRequestor()
{
  try
  {
    if (typeof ActiveXObject != "undefined")
    {
      return new ActiveXObject("Microsoft.XMLHTTP")
    }
    else if (window.XMLHttpRequest)
    {
      return new XMLHttpRequest()
    }
  }
  catch(a)
  {
  }
  return null
};
var newRequestor = function()
{
  request = new xmlRequestor;
};
var request;
newRequestor();

function importXML(xmlText)
{
  // Cross-browser function to get a DOM objectfrom an XML string

  if (typeof ActiveXObject != "undefined" &&
      typeof GetObject != "undefined")
  {
    var xmlDom = new ActiveXObject("Microsoft.XMLDOM");
    xmlDom.loadXML(xmlText);
    return xmlDom;
  }
  
  if (typeof DOMParser != "undefined")
  {
    return(new DOMParser).parseFromString(xmlText, "text/xml");
  }
  
  return null;
}

if (!Array.prototype.indexOf)
{
  Array.prototype.indexOf = function(target)
  {
    // A useful extension to the built-in JS Array type: find the index of the
    // given element. Reurns -1 if not found.
  
    for (var n = 0; n < this.length; n++)
      if (this[n] == target)
        return n;
  
    return -1;
  }
};

if (!Array.prototype.forEach)
{
  Array.prototype.forEach = function(fun /*, thisp*/)
  {
    var len = this.length;
    if (typeof fun != "function")
      throw new TypeError();

    var thisp = arguments[1];
    for (var i = 0; i < len; i++)
    {
      if (i in this)
        fun.call(thisp, this[i], i, this);
    }
  };
};

if (!Array.prototype.filter)
{
  Array.prototype.filter = function(fun /*, thisp*/)
  {
    var len = this.length;
    if (typeof fun != "function")
      throw new TypeError();

    var res = new Array();
    var thisp = arguments[1];
    for (var i = 0; i < len; i++)
    {
      if (i in this)
      {
        var val = this[i]; // in case fun mutates this
        if (fun.call(thisp, val, i, this))
          res.push(val);
      }
    }

    return res;
  };
};

if (!Array.prototype.merge)
{
  Array.prototype.merge = function (otherArray)
  {
    for (var n = 0; n < otherArray.length; n++)
      this.push(otherArray[n]);
  };
};


if (!String.prototype.trim)
{
  String.prototype.trim = function()
  {
    return this.replace(/^ +/, '').replace(/ +$/, '');
  };
};
if (!String.prototype.pack)
{
  String.prototype.pack = function()
  {
    return this.replace(/\s+/g, ' ');
  };
};
if (!String.prototype.htmlEntities)
{
  String.prototype.htmlEntities = function () 
  {
    return this.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
  };
};
if (!String.prototype.capitalize)
{
  String.prototype.capitalize = function () 
  {
    return this.substr(0, 1).toUpperCase() + this.substr(1).toLowerCase();
  };
};
if (!String.prototype.capitalizeAll)
{
  String.prototype.capitalizeAll = function () 
  {
    var words = this.split(' ');
  
    var result =  words[0].capitalize();
    for (var w = 1; w < words.length; w++) 
      result += ' ' + words[w].capitalize();
  
    return result;
  };
};

function propertyCount(object)
{
  var result = 0;
  for (var property in object)
    result++;

  return result;
};

function limit(a, lower, upper)
{
  // Handy little function to limit a number (a) to be within specified bounds
  //
  //  Example:
  //    lat = limit(lat, -90, 90);
  
  if (lower != null)
    a = Math.max(a, lower);

  if (upper != null)
    a = Math.min(a, upper);
    
  return a;
};

function addEventHandler(element, event, func, phase)
{
  /* A generic, cross-browser technique for adding an event handler to a DOM element
     without disturbing any other handlers that have been tied to it, by any means.
     
     Parms:
     
      element - the DOM element to attach to. Either the node itself (including "window") or a 
                string containing the ID of the element.
                
      event - the event name as a string, either as "load", "onload", or "onLoad" (case-insensitive)
      
      func - the function to assign to the event. Can be a closure.
      
      phase - string, 'capturing' or 'bubbling'. Optional, defaults to 'capturing'. Not
              applicable to IE<7 and some older browsers.
              
     Examples:
      addEventHandler(window, 'load', init);
      addEventHandler(window, 'unload', GUnload);
      addEventHandler(listName, 'onClick', function() {renamePoint(id, 'list')});
  */
  
  // Normalize parms

  if ((typeof element) == 'string')
    element = document.getElementById(element);
  if (!element ||
      (element == undefined))
    return;

  event = event.toLowerCase();
  if (event.substr(0, 2) == 'on')
  {
    var longEvent  = event;
    var shortEvent = event.substr(2);
  }
  else
  {
    var longEvent  = 'on' + event;
    var shortEvent = event;
  }

  var useCapture = (phase == 'bubbling');
  
  // Attach the event
  
  if ((event != 'click') &&
      element.addEventListener)
    // W3C DOM Level 2 compliant
    element.addEventListener(shortEvent, func, useCapture);
    
  else if ((event != 'click') &&
           element.attachEvent)
    // Most versions of IE
    element.attachEvent(longEvent, func);
    
  else
  {
    // Older browsers
    //  - Note that the handler does not get an event object parm - not sure if this is an issue
    //    with the browser versions that will see this code block or not.
    var oldHandler = eval('element.' + longEvent);

    if ((typeof oldHandler) == 'function') 
      eval('element.' + longEvent + ' = function() {return (oldHandler() && func());}');
    else 
      eval('element.' + longEvent + ' = function() {return func();}');
  }
};

function removeEventHandler(element, event, func, phase)
{
  
  // Normalize parms

  if ((typeof element) == 'string')
    element = document.getElementById(element);
  if (!element ||
      (element == undefined))
    return;

  event = event.toLowerCase();
  if (event.substr(0, 2) == 'on')
  {
    var longEvent  = event;
    var shortEvent = event.substr(2);
  }
  else
  {
    var longEvent  = 'on' + event;
    var shortEvent = event;
  }

  var useCapture = (phase == 'bubbling');
  
  // Attach the event
  
  if (element.removeEventListener)
    // W3C DOM Level 2 compliant
    element.removeEventListener(shortEvent, func, useCapture);
    
  else if (element.detachEvent)
    // IE 5 & 6
    element.detachEvent(longEvent, func);
    
  else
  {
    // Older browsers
    //  - Note that this just removes ALL handlers from the event. Not ideal, but what's the alternative?
    eval('element.' + longEvent + ' = null');
  }
};

var _mSecPerDay = 1000 * 60 * 60 * 24;
var yesterday = new Date() - _mSecPerDay;

function isPast(date)
{
  // Utility function called to disable past dates in the popup calendar
  return (date < yesterday);
};

var arVersion = navigator.appVersion.split("MSIE");
var ieVersion = parseFloat(arVersion[1]);
function fixPNG(myImage) 
{
  /* Work around IE5/6's inability to natively display alpha-channel PNGs

      To use, call with the image element as a parameter.
  
      titleDiv.style.filter = "progid:DXImageTransform.Microsoft.AlphaImageLoader(src='images/title.png', sizingMethod='scale')";
      
  */
  if ((ieVersion >= 5.5) && 
      (ieVersion < 7) && 
      (document.body.filters)) 
  {
    var imgID = (myImage.id) ? "id='" + myImage.id + "' " : "";
    var imgClass = (myImage.className) ? "class='" + myImage.className + "' " : "";
    var imgTitle = (myImage.title) ? 
	                "title='" + myImage.title  + "' " : "title='" + myImage.alt + "' ";
    var imgStyle = "display:inline-block;" + myImage.style.cssText;
    var strNewHTML = "<span " + imgID + imgClass + imgTitle
                   + " style=\"" + "width:" + myImage.width 
                   + "px; height:" + myImage.height 
                   + "px;" + imgStyle + ";"
                   + "filter:progid:DXImageTransform.Microsoft.AlphaImageLoader"
                   + "(src=\'" + myImage.src + "\', sizingMethod='scale');\"></span>";
    myImage.outerHTML = strNewHTML;
  }
};
function getTarget(event)
{
  // A cross-browser-compatible function to return the target of an event
  if (event.target)
    // Mozilla
    return event.target;
  else if (event.srcElement)
    // IE
    return event.srcElement;
  else
    return null;
};

function cloneObject(what) {
    for (i in what) {
        this[i] = what[i];
    }
};

function getText(xmlNode)
{
  // A cross-browser-compatible function to return the inner text of the given
  // XML node. For example, if the node was "<foo>bar</foo>", would return "bar".

  if (xmlNode.text)
    return xmlNode.text;
    
  if (xmlNode.data)
    return xmlNode.data;
    
  else if (xmlNode.innerText)
    return xmlNode.innerText;

  else if (xmlNode.textContent)
    return xmlNode.textContent;

  else if (xmlNode.firstChild)
    return getText(xmlNode.firstChild);

  else if (xmlNode.length > 0)
    return getText(xmlNode[0]);

  else
    return '';
};

function getChildNodeByName(xmlNode, target)
{
  for (var m = 0; m < xmlNode.childNodes.length; m++)
    if (xmlNode.childNodes[m].nodeName == target)
      return xmlNode.childNodes[m];
  
  return null;
};

function getURLParm(name)
{
  // thanks to http://www.netlobo.com/url_query_string_javascript.html
  name = name.replace(/[\[]/,"\\\[").replace(/[\]]/,"\\\]");
  var regexS = "[\\?&]"+name+"=([^&#]*)";
  var regex = new RegExp( regexS );
  var results = regex.exec( window.location.href );
  if( results == null )
    return "";
  else
    return results[1];
};

function getCookie(name) 
{
  // thanks to http://www.quirksmode.org/js/cookies.html
	var nameEQ = name + "=";
	var ca = document.cookie.split(';');
	for (var i = 0; i < ca.length; i++) 
	{
		var c = ca[i];
		while (c.charAt(0) == ' ') 
		  c = c.substring(1, c.length);

		if (c.indexOf(nameEQ) == 0)
		  return c.substring(nameEQ.length, c.length);
	}
	return null;
}

function rad2deg(rad)
{
  return rad * 180 / Math.PI;
};

function deg2rad(deg)
{
  return deg * Math.PI / 180;
};

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

function sign(number)
{
  if (number > 0)
    return 1;
  else
    return -1;
};

function zeroPad(number, len)
{
  // Left-pad the given number with zeros into a string of the given length.
  //
  //  Notes:  - Input must be >= 0.
  //          - Length is to the left of the deciml, so zeroPad(2.17, 3) => 002.17

  var padding = '00000000000000000000000000000000000000000000000000000000000000000000';
  
  if (number <= 0)
    return padding.substring(0, len);
  else
    return padding.substring(0, len - (Math.floor(Math.log(number)/Math.log(10)) + 1)) + String(number);
};

function numberFormat(number, places)
{
  // Format the given number as a string with the supplied number of decimal places
  //
  //  Example:  numberFormat(45.3, 2) => '45.30'
  
  if (places == undefined)
    places = 2;

  return number.toFixed(places);// Math.round(number * Math.pow(10, places)) / Math.pow(10, places);
}

function cookieDate(date)
{
  // Return a date/time string in the format expected by document.cookie: Wdy, DD-Mon-YY HH:MM:SS GMT
  switch (date.getUTCDay())
  {
    case 0: {result = 'Sun'; break;}
    case 1: {result = 'Mon'; break;}
    case 2: {result = 'Tue'; break;}
    case 3: {result = 'Wed'; break;}
    case 4: {result = 'Thu'; break;}
    case 5: {result = 'Fri'; break;}
    case 6: {result = 'Sat'; break;}
  } 
  
  result += ', ' + zeroPad(date.getUTCDate(), 2);

  switch (date.getUTCMonth())
  {
    case 0:  {result += '-Jan-'; break;}
    case 1:  {result += '-Feb-'; break;}
    case 2:  {result += '-Mar-'; break;}
    case 3:  {result += '-Apr-'; break;}
    case 4:  {result += '-May-'; break;}
    case 5:  {result += '-Jun-'; break;}
    case 6:  {result += '-Jul-'; break;}
    case 7:  {result += '-Aug-'; break;}
    case 8:  {result += '-Sep-'; break;}
    case 9:  {result += '-Oct-'; break;}
    case 10: {result += '-Nov-'; break;}
    case 11: {result += '-Dec-'; break;}
  } 

  result += String(date.getUTCFullYear()).substr(2, 2) + ' ' +
            zeroPad(date.getUTCHours(), 2) + ':' +
            zeroPad(date.getUTCMinutes(), 2) + ':' +
            zeroPad(date.getUTCSeconds(), 2) + ' GMT';
            
  return result;
};

function formatDate(date, options)
{
  // Convert a date as a string with format "YYYY/MM/DD".
  // Options:
  //  delimiter - string, default '/'. Supply as '' to suppress.
  //  offset    - minutes offset from GMT. Integer, defaults to machine local time offset.

  if (typeof(options) == 'string')
  {
    // Backwards-compatibility
    var delimiter = options;
    options = new Array();
    options.delimiter = delimiter;
  }
  else if (!options)
    options = new Array();

  if (options.delimiter == undefined)
    options.delimiter = '/';  // default delimiter
    
  if (options.offset != undefined)
    date = new Date(Number(date) + (date.getTimezoneOffset() - options.offset) * 60 * 1000);
    
  // Year is easy.
  var result = String(date.getFullYear()) + options.delimiter;

  // Month and day are a bit more hassle, just because I need to pad it with 0.

  // Months are funny because JS treats them as 0-based
  result += zeroPad(date.getMonth() + 1, 2) + options.delimiter;

  // Days are, more intuitively, 1-based
  result += zeroPad(date.getDate(), 2);

  return result;
};

function formatTime(date, options)
{
  // Convert a date to a string with format "HH:MM[:SS]"
  // Options:
  //  delimiter - string, default ':'. Supply as '' to suppress.
  //  offset    - minutes offset from GMT. Integer, defaults to machine local time offset.
  //  seconds   - whether to include the seconds field. Boolean, default true.
  //  military  - true (default) => '22:37', false => '10:37 PM'
  //  padHours  - true => '03:42', false => '3:42'. Default is equal to 'military' option.

  if (typeof(options) == 'string')
  {
    // Backwards-compatibility
    var delimiter = options;
    options = new Array();
    options.delimiter = delimiter;
  }
  else if (!options)
    options = new Array();
  
  if (options.delimiter == undefined)
    options.delimiter = ':';  // default delimiter

  if (options.offset != undefined)
    date = new Date(Number(date) + (date.getTimezoneOffset() - options.offset) * 60 * 1000);

  if (options.seconds == false)
    // Add 29.999 seconds to round to the nearest minute
    date = new Date(Number(date) + 29999);

  // Hours
  if (options.military == false)
  {
    if (date.getHours() == 0)
      var hour = 12;
    else if (date.getHours() <= 12)
      var hour = date.getHours();
    else
      var hour = date.getHours() - 12;

    if (options.padHours == true)
      result = zeroPad(hour, 2);
    else
      result = hour;
  } 
  else
  {
    if (options.padHours == false)
      result = date.getHours();
    else
      result = zeroPad(date.getHours(), 2);
  }
   
  // Minutes
  result += options.delimiter + zeroPad(date.getMinutes(), 2);

  // Seconds
  if (options.seconds != false)
    result += options.delimiter + zeroPad(date.getSeconds(), 2);


  if (options.military == false)
  {
    if (date.getHours() < 12)
      result += ' AM';
    else
      result += ' PM';
  }

  return result;
};

function formatDelta(milliseconds)
{
  // Generate a string describing the amount of time between two timestamps
  // 
  //  Typically called as formatDelta(time2 - time1)

  var minutes = Math.round(Math.abs(milliseconds) / (60 * 1000));
  
  if (minutes == 1)
  {
    var result = '1 minute';
  }
  else if (minutes <= 60)
  {
    var result = minutes + ' minutes';
  }
  else
  {
    var hours = Math.floor(minutes / 60);
    minutes = (minutes % 60);
    
    if (hours < 24)
      var result = hours + 'h, ' + minutes + 'm';
    else
      var result = Math.floor(hours / 24) + 'd, ' + (hours % 24) + 'h, ' + minutes + 'm';
  }
  
  return result;
};

function validEMail(test)
{
  var regExp = new RegExp('[-!#$%&\'*+\\./0-9=?A-Z^_`a-z{|}~]+' +
                          '@' +
                          '[-!#$%&\'*+\\/0-9=?A-Z^_`a-z{|}~]+\.' +
                          '[-!#$%&\'*+\\./0-9=?A-Z^_`a-z{|}~]+');
  return (test.search(regExp) > -1);
};

function getTop(element)
{
  if (element.offsetParent)
    return element.offsetTop + getTop(element.offsetParent);
  else
    return element.offsetTop;
};

function getLeft(element)
{
  if (element.offsetParent)
    return element.offsetLeft + getLeft(element.offsetParent);
  else
    return element.offsetLeft;
};

// Two generic routines to return the value of a form field

function valueOfField(element, form)
{
  var n;

  if (!element)
    return null;

  if (!form)
  {
    for (n = 0; n < document.forms.length; n++)
      if (element == document.forms[n].elements[element.name])
      {
        form = document.forms[n];
        break;
      }
  }
  if (!form)
    return null;
  
  var type = element.type;
  
  if (!type &&
      (element.length > 0))
  {
    for (n = 0; n < element.length; n++)
      if (element[n].checked)
        return element[n].value;
  }
  else
  switch (type)
  {
    case 'button':
    case 'file':
    case 'hidden':
    case 'password':
    case 'reset':
    case 'submit':
    case 'text':
    case 'textarea':
      return element.value;

    case 'checkbox':
      if (element.checked)
        return element.value;
      else
        return null;

    case 'radio':
    {
      var radios = document.getElementsByName(element.name);
      for (n = 0; n < radios.length; n++)
        if (radios[n].checked)
          return radios[n].value;

      return null;
    }

    case 'select':
    case 'select-one':
    {
      n = element.selectedIndex;
      if ((n >= 0) &&
          (n < element.length))
        return element.options[n].value;
      else
        return null;
    } 
  }

  return null;
};

function valueByName(fieldName, form)
{

  var element;
  var n;
  
  if (form)
    element = form.elements[fieldName];
  else
  {
    for (n = 0; n < document.forms.length; n++)
    {
      element = document.forms[n].elements[fieldName];
      if (element)
      {
        form = document.forms[n];
        break;
      }
    }
  }

  return valueOfField(element, form);
};

function validCC(cardType, cardNumber)
{
  // Perform checkdigit and checksum validation on a card number

  var useAlternate;
  var digit;
  var digitSum = 0;
  var prefix;

  if (!cardType || !cardNumber)
    return false;
  
  //	Should be just numeric digits.  Reject if not.
  if (isNaN(Number(cardNumber)))
    return false;
  
  // Validate number length and prefix
  switch (cardType)
  {
    case 'CA':
    case 'MC':
    {
      // MasterCard
      if (cardNumber.length != 16)
        return false;
  
      prefix = parseInt(cardNumber.slice(0, 2), 10);
      if ((prefix < 51) || (prefix > 55))
        return false;

      break;
    }

    case 'VI':
    {
      // Visa
      if ((cardNumber.length != 13) && (cardNumber.length != 16))
        return false;
  
      prefix = cardNumber.charAt(0);
      if (prefix != '4')
        return false;

      break;
    }

    case 'AX':
    {
      // Amex
      if (cardNumber.length != 15)
        return false;
  
      prefix = cardNumber.slice(0, 2);
      if ((prefix != '34') && (prefix != '37'))
        return false;

      break;
    }

    case 'DS':
    {
      // Discover
      if (cardNumber.length != 16)
        return false;
  
      prefix = cardNumber.slice(0, 4);
      if (prefix != '6011')
        return false;

      break;
    }

    case 'DI':
    case 'DC':
    case 'CB':
    {
      // Diners Club / Carte Blanche
      if (cardNumber.length != 14)
        return false;
  
      prefix = parseInt(cardNumber.slice(0, 3))  ;
      if ((prefix < 300) || (prefix > 305))
      {
        prefix = cardNumber.slice(0, 2);
        if ((prefix != '36') && (prefix != '38'))
          return false;
      }

      break;
    }
  }

  //	Determine (from length and card type) if this number should use the 
  //  alternate first-digit processing method.
  if ((cardNumber.length == 16) || (cardNumber.length == 14))
    useAlternate = true;
  
  else if ((cardNumber.length == 13) || (cardNumber.length == 15))
    useAlternate = false;
  
  else
    //	Other lengths are invalid
    return false;
  
  //	Sum loop
  for (i = 0; i < cardNumber.length; i++)
  {
    digit = parseInt(cardNumber.charAt(i));
    
    if (isNaN(digit))
      return false;
    
    if (useAlternate)
    {
      //	Alternate-digit processing
      digit = digit * 2;
      
      if (digit >= 10)
        digit = Math.floor(digit / 10) + (digit % 10);
    }
    useAlternate = !useAlternate;
  
    digitSum += digit;
  }
  
  //	Check the sum
  if ((digitSum % 10) > 0)
    return false;
  
  //	Sum OK
  return true;
};

function windowHeight()
{
  var myHeight = 0;
  if (typeof(window.innerWidth) == 'number') 
  {
    //Non-IE
    myHeight = window.innerHeight;
  } 
  else if (document.documentElement && 
           (document.documentElement.clientWidth || document.documentElement.clientHeight)) 
  {
    //IE 6+ in 'standards compliant mode'
    myHeight = document.documentElement.clientHeight;
  } 
  else if (document.body && 
           (document.body.clientWidth || document.body.clientHeight)) 
  {
    //IE 4 compatible
    myHeight = document.body.clientHeight;
  }
  
  return myHeight;
};

function windowWidth()
{
  var myWidth = 0;
  if (document.documentElement && 
           (document.documentElement.clientWidth || document.documentElement.clientHeight)) 
  {
    // IE 6+ in 'standards compliant mode', among others
    myWidth = document.documentElement.clientWidth;
  } 
  else if (document.body && 
           (document.body.clientWidth || document.body.clientHeight)) 
  {
    // IE 4 compatible
    myWidth = document.body.clientWidth;
  }
  else if (typeof(window.innerWidth) == 'number') 
  {
    // Older non-IE
    myWidth = window.innerWidth;
  } 

  return myWidth;
};

function elementWidth(element)
{
  if (element.innerWidth)
  	return element.innerWidth;
  else
    return element.clientWidth;
};

function elementHeight(element)
{
  if (element.innerHeight)
  	return element.innerHeight;
  else
    return element.clientHeight;
};

function setOpacity(div, opacity)
{
  if (!div)
    return;

  opacity = limit(opacity, 0, 100);
  
  if (typeof(div.style.filter) == 'string')
    div.style.filter = 'alpha(opacity:' + opacity + ')';

  if (typeof(div.style.KHTMLOpacity) == 'string')
    div.style.KHTMLOpacity = opacity / 100;

  if (typeof(div.style.MozOpacity) == 'string')
    div.style.MozOpacity = opacity / 100;

  if (typeof(div.style.opacity) == 'string')
    div.style.opacity = opacity / 100;
};

function getScrollXY() 
{
  // Cross-browser function that returns the scroll postion of the window.
  // From http://www.howtocreate.co.uk/tutorials/javascript/browserwindow
  var scrOfX = 0, scrOfY = 0;
  if( typeof( window.pageYOffset ) == 'number' ) {
    //Netscape compliant
    scrOfY = window.pageYOffset;
    scrOfX = window.pageXOffset;
  } else if( document.body && ( document.body.scrollLeft || document.body.scrollTop ) ) {
    //DOM compliant
    scrOfY = document.body.scrollTop;
    scrOfX = document.body.scrollLeft;
  } else if( document.documentElement && ( document.documentElement.scrollLeft || document.documentElement.scrollTop ) ) {
    //IE6 standards compliant mode
    scrOfY = document.documentElement.scrollTop;
    scrOfX = document.documentElement.scrollLeft;
  }
  return {x: scrOfX, y: scrOfY};
};

function smoothScrollTo(y)
{
  var scrollPos = getScrollXY();
  if (scrollPos.y > y)
  {
    window.scrollBy(0, -5);
    setTimeout('smoothScrollTo(' + y + ')', 3);
  }
};

function isWithin(descendant, ancestor)
{
  if (!descendant.parentNode)
    return false;
  else if (descendant.parentNode == ancestor)
    return true;
  else
    return isWithin(descendant.parentNode, ancestor)
};
//]]>