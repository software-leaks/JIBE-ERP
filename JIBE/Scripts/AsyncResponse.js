
function AsyncResponse(surl, params, handler) {
    this.url = surl;
    this.params = params;
    var req
    function processRequest(){
       
       if (req.readyState == 4) {
           // only if "OK"
           if (req.status == 200 || req.status == 304) {
               handler(req.responseText);
           } else {
               handler(req.responseText);
           }
        }
        else if(req.readyState == 2) 
        {
            //handler ('Working...');        
        }
    }

    this.getResponse = function() {

        if (window.XMLHttpRequest) {
            req = new XMLHttpRequest();
        } else if (window.ActiveXObject) {
            req = new ActiveXObject("Microsoft.XMLHTTP");
        }       
        if (req) {
            req.open("POST", this.url, true);
            req.onreadystatechange = function() {processRequest();};
            
            req.setRequestHeader('Content-Type','application/x-www-form-urlencoded');
            req.setRequestHeader("Content-length", this.params.length);
            //req.setRequestHeader("Connection", "close");
            
            req.send(this.params);
            handler ('Working...');
        }
       
    }
}

function clearXMLTags(retval) {
    try {
        retval = retval.replace('<?xml version="1.0" encoding="utf-8"?>', '');
        retval = retval.replace('<string xmlns="http://tempuri.org/" />', '');
        retval = retval.replace('<string xmlns="http://tempuri.org/">', '');
        retval = retval.replace('</string>', '');
    }
    catch (ex) { alert(ex.message); }
    return retval;
}