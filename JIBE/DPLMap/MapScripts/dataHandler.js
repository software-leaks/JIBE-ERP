// JScript File
//*****************************************************************************************
//  GET HelloWorld
//*****************************************************************************************
    var arExp;
    
    function Async_getHelloWorld(){
        //var url =  "http://localhost/dpl_map/wsDPLMap.asmx/HelloWorld"
        var url =  "http://localhost/dpl_map/wsDPLMap.asmx/getShipImages"
        var params = '';
        var obj = new AsyncResponse(url, params, response_getShipImages)
        obj.getResponse();
    }
    function response_getShipImages(retval){
        var ar,arS;
        var data;
        
        try{
            //if (retval.indexOf('Working')>=0){return;}
            var retval = clearXMLTags(retval);
            var arIn = eval(retval);
           
           
            // Load images using array
            
                       
            if(arIn.length > 0)
                alert(arIn[0].Vessel_Name);            
        }
        catch(ex){alert(ex.message)}          
    }
       function clearXMLTags(retval){
        try{
            retval = retval.replace('<?xml version="1.0" encoding="utf-8"?>','');            retval = retval.replace('<string xmlns="http://tempuri.org/">','');            retval = retval.replace('</string>','');
        }
        catch(ex){}
        return retval;
    }