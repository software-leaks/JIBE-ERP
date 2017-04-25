
var __app_name = location.pathname.split('/')[1];

// to call-> js_ShowToolTip(message,event,this)
function js_ShowToolTip(ctlToolTips, evt, objthis) {

    var browser =!! window.chrome;   // for chrome browser 
    var pageX = 0;
    var pageY = 0;

    if ('pageX' in evt) { // all browsers except IE before version 9
        pageX = evt.pageX;
          pageY = evt.pageY;

    }
    else {  // IE before version 9
        pageX = evt.clientX + document.documentElement.scrollLeft;
        pageY = evt.clientY + document.documentElement.scrollTop;
    }

    var vDocHeight = document.body.clientHeight;
   

    if (ctlToolTips) {

        var divMsg_Obj = document.getElementById("__divMsgTooltip");

        if (divMsg_Obj == null) {

            var __divMsg = document.createElement("div");
            __divMsg.id = "__divMsgTooltip";
            __divMsg.innerHTML = ctlToolTips;
            __divMsg.style.cssText = 'z-index:9999;color:#000;font-Family:arial;font-size:11px;background-color: #fff;position:absolute; display:none;margin-top: 10px; border:1px solid #dedede; padding:1px;box-shadow:0 3px 5px rgba(0, 0, 0, 0.2); display:block;'; 
            document.body.appendChild(__divMsg);
            if (ctlToolTips == "CallLoadingImage") {
                __divMsg.style.border = '0px';
                __divMsg.style.background = 'none';
                __divMsg.innerHTML = "<img src='../../Images/loader.gif'></img>";

            }
            else {
                document.body.onclick = js_HideTooltip;
            }
            if (browser == true) {
                var pageHeight = document.body.scrollHeight - 500;   // for chrome browser - 500 height from pageHeight
              
            }
            else 
            {
                var pageHeight = document.body.scrollHeight;
             }
            var height = __divMsg.clientHeight;

            var pageWidth = document.body.scrollWidth;
            var width = __divMsg.clientWidth;

         
            if (pageHeight < (height + pageY)) {
                if (pageY - height < 0)
                    __divMsg.style.top = "5px";
                else
                    __divMsg.style.top = (pageY - height) + "px";
            }
            else {
                //if ((pageHeight - vDocHeight) > 250) {
                if (vDocHeight > 1000) {

                    if ((evt.clientY + 300) > vDocHeight) {
                        __divMsg.style.top = (pageY - 120) + "px";
                    }
                    else {

                        __divMsg.style.top = (pageY - 5) + "px";
                    }
                }
                else {
                        __divMsg.style.top = (pageY - 5) + "px";
                   
                }

               
            }

            if (pageWidth < (width + pageX)) {

                __divMsg.style.left = (pageX - width) + "px";
            }
            else {
                __divMsg.style.left = (pageX - 1) + "px";
            }



        }
        else {


            divMsg_Obj.style.display = "block";
            divMsg_Obj.innerHTML = ctlToolTips;


            var pageHeight = document.body.scrollHeight;
            var height = divMsg_Obj.clientHeight;

            var pageWidth = document.body.scrollWidth;
            var width = divMsg_Obj.clientWidth;

            if (pageHeight < (height + pageY)) {
                if (pageY - height < 0)
                    divMsg_Obj.style.top = "5px";
                else
                    divMsg_Obj.style.top = (pageY - height) + "px";
            }
            else {

                divMsg_Obj.style.top = (pageY + 5) + "px";
            }

            if (pageWidth < (width + pageX)) {

                divMsg_Obj.style.left = (pageX - width) + "px";
            }
            else {
                divMsg_Obj.style.left = (pageX - 1) + "px";
            }


            document.body.onclick = js_HideTooltip;
        }
        if ('pageX' in evt) {
            if (objthis)
                objthis.setAttribute("onmouseout", "js_HideTooltip()");
        }
        else {
            if (objthis)
                objthis.attachEvent("onmouseout", js_HideTooltip);
        }
    }

}

function SetPosition_Relative(evt, TargetctlID) {

    var targObj = document.getElementById(TargetctlID);
    var pageX = 0;
    var pageY = 0;

    if ('pageX' in evt) { // all browsers except IE before version 9
        pageX = evt.pageX;
        pageY = evt.pageY;
    }
    else {  // IE before version 9
        pageX = evt.clientX + document.documentElement.scrollLeft;
        pageY = evt.clientY + document.documentElement.scrollTop;
    }

    var pageHeight = document.body.scrollHeight;
    var height = targObj.clientHeight;

    var pageWidth = document.body.scrollWidth;
    var width = targObj.clientWidth;

    if (pageHeight < (height + pageY)) {

        if (pageY - height < 0)
            targObj.style.top = "5px";
        else
            targObj.style.top = (pageY - height) + "px";
    }
    else {
        targObj.style.top = (pageY + 5) + "px";
    }

    if (pageWidth < (width + pageX)) {

        targObj.style.left = (pageX - (width + 10)) + "px";
    }
    else {
        targObj.style.left = (pageX + 10) + "px";
    }
}

function checkNumber(id) {

    var obj = document.getElementById(id);
    if (isNaN(obj.value)) {
        obj.value = 0;
        alert("Only number allowed !");
    }

}

function goDownUP(obthis, evt, txtid) {
    try {
        var newTextBoxID = "";
        var arrctl = obthis.id.split('_' + txtid)[0].split('ctl');
        var gridid = obthis.id.split('_' + txtid)[0];
        var colid = arrctl[arrctl.length - 1];


        if (evt.keyCode == 40 || evt.keyCode == 13) {
            newColId = (parseInt(colid, 10) + 1) < 10 ? "0" + (parseInt(colid, 10) + 1).toString() : (parseInt(colid, 10) + 1).toString()
            newTextBoxID = gridid.substring(0, gridid.length - colid.toString().length) + newColId + "_" + txtid;

            document.getElementById(newTextBoxID).focus();
        }
        else if (evt.keyCode == 38) {
            newColId = (parseInt(colid, 10) - 1) < 10 ? "0" + (parseInt(colid, 10) - 1).toString() : (parseInt(colid, 10) - 1).toString()
            newTextBoxID = gridid.substring(0, gridid.length - colid.toString().length) + newColId + "_" + txtid;

            document.getElementById(newTextBoxID).focus();
        }
    }
    catch (ex) {
    }

}

function PrintDiv(dvID) {

    var a = window.open('', '', 'left =' + screen.width + ',top=' + screen.height + ',width=0,height=0,toolbar=0,scrollbars=0,status=0');
    a.document.write(document.getElementById(dvID).innerHTML);
    a.document.close();
    a.focus();
    a.print();
    a.close();
    return false;
}



function js_ShowToolTip_Fixed(ctlToolTips, evt, objthis, pageheader) {

    var pageX = 0;
    var pageY = 0;

    if ('pageX' in evt) { // all browsers except IE before version 9
        pageX = evt.pageX;
        pageY = evt.pageY;
    }
    else {  // IE before version 9
        pageX = evt.clientX + document.documentElement.scrollLeft;
        pageY = evt.clientY + document.documentElement.scrollTop;
    }



    if (ctlToolTips) {

        var divMsg_Obj = document.getElementById("__divMsgTooltip_Fixed");
        var divMsg_FixedClose_Obj = document.getElementById("__divMsg_FixedClose");
        var divMsg_Contents_Obj = document.getElementById("__divMsg_Contents");





        if (divMsg_Obj == null) {

            var __divMsg = document.createElement("table");
            __divMsg.id = "__divMsgTooltip_Fixed";

            var trHD = document.createElement("tr");
            var trDT = document.createElement("tr");
            var tdHDepmty = document.createElement("td");
            var tdHDepmty2 = document.createElement("td");
            //            var tdHDclose = document.createElement("td");
            var tdDTData = document.createElement("td");
            var tdDTData2 = document.createElement("td");
            tdDTData2.style.width = '12px';
            tdDTData2.style.height = '10px';
            //            var tdDTepmty = document.createElement("td");
            tdHDepmty2.innerHTML = '<div style="position:absolute;top:-8px;right:2px"><img  src="/' + __app_name + '/images/close-tooltipjs.png" onclick="js_HideTooltip_Fixed()" style="cursor:pointer;position:absolute;right:0;" ></div>';
            tdHDepmty.align = 'right';

            tdDTData.id = '__divMsg_Contents';
            tdDTData.style.border = '2px solid #FFA500';
            tdDTData.style.borderRadius = "7px";
            tdDTData.style.MozBorderRadius = "7px";
            tdDTData.style.WebkitBorderRadius = "7px";
            tdDTData.style.background = '#F5F5F5';
            tdDTData.innerHTML = ctlToolTips;




            trHD.appendChild(tdHDepmty);
            trHD.appendChild(tdHDepmty2);
            //            trHD.appendChild(tdHDclose);
            trDT.appendChild(tdDTData);
            trDT.appendChild(tdDTData2);
            //            trDT.appendChild(tdDTepmty);
            __divMsg.appendChild(trHD);
            __divMsg.appendChild(trDT);
            __divMsg.cellPadding = '0';
            __divMsg.cellSpacing = '0';

            __divMsg.style.fontFamily = 'arial';
            __divMsg.style.position = "absolute";
            __divMsg.style.display = 'block';
            __divMsg.style.zIndex = "9999";

            __divMsg.style.fontSize = '11px';
            __divMsg.style.color = '#000000';




            //            __divMsg.style.left = (pageX - 1) + "px";
            //            __divMsg.style.top = (pageY + 5) + "px";

            document.body.appendChild(__divMsg);


            var pageHeight = document.body.scrollHeight;
            var height = __divMsg.clientHeight;

            var pageWidth = document.body.scrollWidth;
            var width = __divMsg.clientWidth;

            if (pageHeight < (height + pageY)) {

                if (pageY - height < 0)
                    __divMsg.style.top = "5px";
                else
                    __divMsg.style.top = (pageY - height) + "px";
            }
            else {
                __divMsg.style.top = (pageY + 5) + "px";
            }

            if (pageWidth < (width + pageX)) {

                __divMsg.style.left = (pageX - (width + 10)) + "px";
            }
            else {
                __divMsg.style.left = (pageX - 10) + "px";
            }



        }
        else {


            divMsg_Obj.style.display = "block";
            divMsg_Contents_Obj.innerHTML = ctlToolTips;
            var pageHeight = document.body.scrollHeight;
            var height = divMsg_Obj.clientHeight;

            var pageWidth = document.body.scrollWidth;
            var width = divMsg_Obj.clientWidth;

            if (pageHeight < (height + pageY)) {

                if (pageY - height < 0)
                    divMsg_Obj.style.top = "5px";
                else
                    divMsg_Obj.style.top = (pageY - height) + "px";
            }
            else {

                divMsg_Obj.style.top = (pageY + 5) + "px";
            }

            if (pageWidth < (width + pageX)) {

                divMsg_Obj.style.left = (pageX - (width + 10)) + "px";
            }
            else {
                divMsg_Obj.style.left = (pageX - 10) + "px";
            }


        }
        if ('pageX' in evt) {
            if (objthis)
                objthis.setAttribute("onmouseout", "js_HideTooltip()");

        }
        else {
            if (objthis)
                objthis.attachEvent("onmouseout", js_HideTooltip);


        }
    }

}

function js_HideTooltip() {

    var objrmk = document.getElementById("__divMsgTooltip");
    if (objrmk != null) {
        document.getElementById("__divMsgTooltip").innerHTML = "";
        document.getElementById("__divMsgTooltip").style.display = "none";
    }
}

function js_HideTooltip_Fixed() {

    var objrmk = document.getElementById("__divMsgTooltip_Fixed");
    if (objrmk != null) {
        document.getElementById("__divMsg_Contents").innerHTML = "";
        document.getElementById("__divMsgTooltip_Fixed").style.display = "none";
    }
}


function FormatAmount(nStr) {
    nStr += '';
    x = nStr.split('.');
    x1 = x[0];
    x2 = x.length > 1 ? '.' + x[1] : '';
    var rgx = /(\d+)(\d{3})/;
    while (rgx.test(x1)) {
        x1 = x1.replace(rgx, '$1' + ',' + '$2');
    }
    return x1 + x2;
}

var lastExecutor = null;
function Get_Record_Information(Table_Name, Where, evt, objthis) {

    if (lastExecutor != null)
        lastExecutor.abort();
    js_ShowToolTip('CallLoadingImage', evt, objthis);
  
    var DateFormat = "";
    if(document.getElementById('hdnDateFromatMasterPage')!=null)
        DateFormat = document.getElementById('hdnDateFromatMasterPage').value;
    try {
        if (DateFormat == "") {
            if (window.parent.document.getElementById('hdnDateFromatMasterPage') != null)       // added null check for handling undefined exception 
                DateFormat = window.parent.document.getElementById('hdnDateFromatMasterPage').value;
            else
                DateFormat = 'dd/MM/yyyy';                                         // Set default format if it was not set by master page
        }
    } catch (ex) {
        DateFormat = 'dd/MM/yyyy';    
    }
   
      
    var service = Sys.Net.WebServiceProxy.invoke('/' + __app_name + '/JibeWebService.asmx', 'asyncGet_Record_Information', false, { "Table_Name": Table_Name, "Where": Where, "DateFormat": DateFormat }, OnSuccGet_Record_Information, Onfail, new Array(evt, objthis));
    lastExecutor = service.get_executor();

}


function OnSuccGet_Record_Information(retval, prm) {
    try {
        var element = document.getElementById("__divMsgTooltip");
        element.outerHTML = "";
        delete element;
        js_ShowToolTip_Fixed(retval, prm[0], prm[1]);
    }
    catch (ex)
    { }
}

function Onfail(retval) {

}

var lastExecutorDetails = null;
function Get_Record_Information_Details(Table_Name, Where) {

    if (lastExecutorDetails != null)
        lastExecutorDetails.abort();
  
      var DateFormat = "";
    if(document.getElementById('hdnDateFromatMasterPage')!=null)
      DateFormat = document.getElementById('hdnDateFromatMasterPage').value;
    if (DateFormat == "") {
        if (window.parent.$("#hdnDateFromatMasterPage").val() != null)        // added null check for handling undefined exception 
            DateFormat = window.parent.$("#hdnDateFromatMasterPage").val();        
        else
            DateFormat = 'dd/MM/yyyy';                                        // Set default format if it was not set by master page
        
    }
 
    var service = Sys.Net.WebServiceProxy.invoke('/' + __app_name + '/JibeWebService.asmx', 'asyncGet_Record_Information', false, { "Table_Name": Table_Name, "Where": Where, "DateFormat": DateFormat }, OnSuccGet_Record_Information_Details, Onfail, new Array(null));
    lastExecutorDetails = service.get_executor();

}


function OnSuccGet_Record_Information_Details(retval, prm) {
    try {

        document.getElementById('dvRecordInformation').innerHTML = retval;
    }
    catch (ex)
    { }
}

try {
    (function ($) {
        $.fn.extend({ scrollableTable: function (opts) {
            var defaults = { cloneClass: "cloned", type: "thead" }, o = $.extend({}, defaults, opts); return this.each(function () {
                var $this = $(this), $clone = $this.clone(); if (o.type === "thead")
                    $this.find(o.type).remove(); else if (o.type === "th")
                    $this.find(o.type).parent("tr").remove(); $this.find("tr:first td").each(function (i) { var w = $(this).width(); $clone.find("tr:first th").each(function (j) { if (i === j) { $(this).width(w); } }); }); var width = $this.width(); $clone.addClass(o.cloneClass).width(width + 2).find("tr:not(:first)").remove().end().insertBefore($this.parent());
            });
        }
        });
    })(jQuery);
}
catch (e) {

}


function textBoxCheckMaxLength(txt, maxLen) {

    if (txt.value.length >= maxLen) {
        return false;

    }
    else
        return true;

}

// Added by Anjali.To retrive tool tip information......................

function Get_Record_Information_ToolTip(UserID, date, evt, objthis) {

    if (lastExecutor != null)
        lastExecutor.abort();
    js_ShowToolTip('Loading...', evt, objthis);

    var service = Sys.Net.WebServiceProxy.invoke('/' + __app_name + '/JibeWebService.asmx', 'asyncGet_Record_Information_ToolTip', false, { "UserID": UserID, "date": date }, OnSuccGet_Record_Information_details, Onfail, new Array(evt, objthis));
    lastExecutor = service.get_executor();

}

function OnSuccGet_Record_Information_details(retval, prm) {
    try {

        var element = document.getElementById("__divMsgTooltip");
        element.outerHTML = "";
        delete element;
        js_ShowToolTip(retval, prm[0], prm[1]);
    }
    catch (ex)
    { }
}
// Added By Someshwar
//debugger;
var lastCExecutorDetails = null;
function Get_CrewRecord_Information_Details(Table_Name, Where) {

    if (lastCExecutorDetails != null)
        lastCExecutorDetails.abort();

    var DateFormat = "";
    if (document.getElementById('hdnDateFromatMasterPage') != null)
        DateFormat = document.getElementById('hdnDateFromatMasterPage').value;
    if (DateFormat == "") {
        if (window.parent.$("#hdnDateFromatMasterPage").val() != null)        // added null check for handling undefined exception 
            DateFormat = window.parent.$("#hdnDateFromatMasterPage").val();
        else
            DateFormat = 'dd/MM/yyyy';                                        // Set default format if it was not set by master page

    }

    var service = Sys.Net.WebServiceProxy.invoke('/' + __app_name + '/JibeWebService.asmx', 'asyncGet_CrewRecord_Information', false, { "Table_Name": Table_Name, "Where": Where, "DateFormat": DateFormat }, OnSuccGet_CrewRecord_Information_Details, Onfail_CrewRecord_Info, new Array(null));
    lastCExecutorDetails = service.get_executor();

}


function OnSuccGet_CrewRecord_Information_Details(retval, prm) {
    try {

        document.getElementById('dvVETCrewInformation').innerHTML = retval;
    }
    catch (ex)
    { }
}
function Onfail_CrewRecord_Info(retval) {

}

// Added by Bikash Panigrahi

function Get_Crew_Information(CrewID, Date) {

    if (lastExecutorDetails != null)
        lastExecutorDetails.abort();

    var service = Sys.Net.WebServiceProxy.invoke('/' + __app_name + '/JibeWebService.asmx', 'asyncGet_Crew_Information', false, { "CrewID": CrewID}, OnSuccGet_Crew_Information, Onfail, new Array(null));
    lastExecutorDetails = service.get_executor();


}


function OnSuccGet_Crew_Information(retval, prm) {
    try {

        document.getElementById('dvCrewInformation').innerHTML = retval;
    }
    catch (ex)
    { }
}

function Get_Crew_Information_ToolTip(CrewID,  evt, objthis) {

    if (lastExecutor != null)
        lastExecutor.abort();
    js_ShowToolTip('Loading...', evt, objthis);

    var service = Sys.Net.WebServiceProxy.invoke('/' + __app_name + '/JibeWebService.asmx', 'asyncGet_Crew_Information_ToolTip', false, { "CrewID": CrewID  }, OnSuccGet_Record_Information_details, Onfail, new Array(evt, objthis));
    lastExecutor = service.get_executor();

}

function asyncGet_Vessel_Information_ToolTip(CrewID, evt, objthis) {

    if (lastExecutor != null)
        lastExecutor.abort();
    js_ShowToolTip('Loading...', evt, objthis);

    var service = Sys.Net.WebServiceProxy.invoke('/' + __app_name + '/JibeWebService.asmx', 'asyncGet_Vessel_Information_ToolTip', false, { "CrewID": CrewID }, OnSuccGet_Record_Information_details, Onfail, new Array(evt, objthis));
    lastExecutor = service.get_executor();
}


function CheckDateValid(date, strDateFormat)
{
  try {
    
   
     var currVal=date;
     
     if(currVal == '')
        return false;

     if (strDateFormat.toLowerCase() == 'dd-mm-yyyy')
        currVal=date.split('-')[1] + "/" + date.split('-')[0] + "/" + date.split('-')[2];
    else if (strDateFormat.toLowerCase() == 'mm-dd-yyyy')
       currVal=date.split('-')[0] + "/" + date.split('-')[1] + "/" + date.split('-')[2];
    else if (strDateFormat.toLowerCase() == 'dd-mmm-yyyy')
        currVal=ConvertMonthToDigit(date.split('-')[1]) + "/" + date.split('-')[0] + "/" + date.split('-')[2];
    else if (strDateFormat.toLowerCase() == 'yyyy-mm-dd')
         currVal=date.split('-')[1] + "/" + date.split('-')[2] + "/" + date.split('-')[0];
    else if (strDateFormat.toLowerCase() == 'mm/dd/yyyy')
        currVal=date.split('/')[0] + "/" + date.split('/')[1] + "/" + date.split('/')[2];
    else if (strDateFormat.toLowerCase() == 'dd/mm/yyyy')
        currVal=date.split('/')[1] + "/" + date.split('/')[0] + "/" + date.split('/')[2];
    
    var rxDatePattern = /^(\d{1,2})(\/|-)(\d{1,2})(\/|-)(\d{4})$/; //Declare Regex
    var dtArray = currVal.match(rxDatePattern); // is format OK?
    
    if (dtArray == null) 
        return false;
    
    //Checks for mm/dd/yyyy format.
    dtMonth = dtArray[1];
    dtDay= dtArray[3];
    dtYear = dtArray[5];        
    
    if (dtMonth < 1 || dtMonth > 12) 
        return false;
    else if (dtDay < 1 || dtDay> 31) 
        return false;
    else if ((dtMonth==4 || dtMonth==6 || dtMonth==9 || dtMonth==11) && dtDay ==31) 
        return false;
    else if (dtMonth == 2) 
    {
        var isleap = (dtYear % 4 == 0 && (dtYear % 100 != 0 || dtYear % 400 == 0));
        if (dtDay> 29 || (dtDay ==29 && !isleap)) 
                return false;
    }
    return true;
     }
     catch (e) 
      {
        return false;
      }
}

///Convert month string to numeric
function ConvertMonthToDigit(month) {
    if (month.toLowerCase() == 'jan' )
        return '01';
    else if (month.toLowerCase() == 'feb')
        return '02';
    else if (month.toLowerCase() == 'mar')
        return '03';
    else if (month.toLowerCase() == 'apr')
        return '04';
    else if (month.toLowerCase() == 'may') 
        return '05';
    else if (month.toLowerCase() == 'jun')
        return '06';
    else if (month.toLowerCase() == 'jul')
        return '07';
    else if (month.toLowerCase() == 'aug')
        return '08';
    else if (month.toLowerCase() == 'sep')
        return '09';
    else if (month.toLowerCase() == 'oct')
        return '10';
    else if (month.toLowerCase() == 'nov')
        return '11';
    else if (month.toLowerCase() == 'dec')
        return '12';
}


function DateAsFormat(date, strDateFormat) {
    if (strDateFormat.toLowerCase() == 'dd-mm-yyyy')
        return new Date(date.split('-')[2] + "-" + date.split('-')[1] + "-" + date.split('-')[0]);
    else if (strDateFormat.toLowerCase() == 'mm-dd-yyyy')
        return new Date(date.split('-')[2] + "-" + date.split('-')[0] + "-" + date.split('-')[1]);
    else if (strDateFormat.toLowerCase() == 'dd-mmm-yyyy')
        return new Date(date.split('-')[2] + "-" + ConvertMonthToDigit(date.split('-')[1]) + "-" + date.split('-')[0]);
    else if (strDateFormat.toLowerCase() == 'yyyy-mm-dd')
        return new Date(date.split('-')[0] + "-" + date.split('-')[1] + "-" + date.split('-')[2]);
    else if (strDateFormat.toLowerCase() == 'mm/dd/yyyy')
        return new Date(date.split('/')[2] + "-" + date.split('/')[0] + "-" + date.split('/')[1]);
    else if (strDateFormat.toLowerCase() == 'dd/mm/yyyy')
        return new Date(date.split('/')[2] + "-" + date.split('/')[1] + "-" + date.split('/')[0]);
}


///Checks whether enter date is valid or not
function IsInvalidDate(date, DateFormat) {
    if (parseInt(date.indexOf("-")>0)) {
        if (date.split('-').length>3 || date.split('-').length <3) {
          return true;
        }
    } 

    if (parseInt(date.indexOf("/")>0)) {
        if (date.split('/').length>3 || date.split('/').length<3) {
          return true;
        }
    }

    if (parseInt(date.indexOf("-")>-1) || parseInt(date.indexOf("/")>-1)) {
      return true;
    }

    if (isNaN(parseInt(date))) {
        return true;
    }

    if (!CheckDateValid(date, DateFormat)) {
        return true;
    }
    return false;
}

///alert if enter date is valid or not
function IsDateinFormat(date, DateFormat) {
    if (isNaN(DateAsFormat(date, DateFormat))) {
        alert('Invalid Date');
        return true;
    }
    else {
        return false;
    }
}


//converting date to dd//MM/yyyy format
function ConvertDt_oldFormat(strDate, strDateFormat) {
    strDate = DateAsFormat(strDate, strDateFormat);
    //var d = new Date();
    var curr_date = strDate.getDate();
    var curr_month = strDate.getMonth() + 1; //Months are zero based
    var curr_year = strDate.getFullYear();
    strDate = curr_date + "/" + curr_month + "/" + curr_year; //dd/MM/yyyy 
    //alert(strDate);
    return strDate;
}

function formatDateToString(date, strDateFormat) {
    // 01, 02, 03, ... 29, 30, 31
    var dd = (date.getDate() < 10 ? '0' : '') + date.getDate();
    // 01, 02, 03, ... 10, 11, 12
    var MM = ((date.getMonth() + 1) < 10 ? '0' : '') + (date.getMonth() + 1);
    // 1970, 1971, ... 2015, 2016, ...
    var yyyy = date.getFullYear();


    if (strDateFormat.toLowerCase() == 'dd-mm-yyyy')
        return (dd + "-" + MM + "-" + yyyy);
    else if (strDateFormat.toLowerCase() == 'mm-dd-yyyy')
        return (MM + "-" + dd + "-" + yyyy);
    else if (strDateFormat.toLowerCase() == 'dd-mmm-yyyy') {
        return (dd + "-" + getMonthName(MM) + "-" + yyyy);
    }
     else if (strDateFormat.toLowerCase() == 'yyyy-mm-dd')
         return (yyyy + "-" + MM + "-" + dd);
    else if (strDateFormat.toLowerCase() == 'mm/dd/yyyy')
        return (MM + "/" + dd + "/" + yyyy);
    else if (strDateFormat.toLowerCase() == 'dd/mm/yyyy')
        return (dd + "/" + MM + "/" + yyyy);
}

function getMonthName(month)
{
  if ( month.toLowerCase() == '01')
        return 'jan';
    else if (month.toLowerCase() == '02')
        return 'Feb';
    else if (month.toLowerCase() == '03')
        return 'Mar';
    else if (month.toLowerCase() == '04')
        return 'Apr';
    else if (month.toLowerCase() == '05')
        return 'May';
    else if (month.toLowerCase() == '06')
        return 'Jun';
    else if (month.toLowerCase() == '07')
        return 'Jul';
    else if (month.toLowerCase() == '08')
        return 'Aug';
    else if (month.toLowerCase() == '09')
        return 'Sep';
    else if (month.toLowerCase() == '10')
        return 'Oct';
   else if (month.toLowerCase() == '11')
        return 'Nov';
   else if (month.toLowerCase() == '12')
        return 'Dec';

}


 function USAddressValidation(Addressline1, Addressline2, City, State, ZipCode, Country,Type,CrewName,ClientName,Mode,StaffCode,DOB,AppliedRank) {
     try {
    js_ShowToolTip("<b>Validating US Address... </b><img src='../Images/loader.gif'>", evt, null); 
        } catch (e) { }
     
     var validate = true;
     var AddressMSg = "";

     if ($.trim(State).length > 2 || $.trim(State) < 2 || isNaN($.trim(State)) == false) {
            AddressMSg += "Enter 2 Characters State Code only\n";
             validate = false;
      }

      if ($.trim(ZipCode).length > 5 || $.trim(ZipCode).length < 5 || isNaN($.trim(ZipCode))) {
             AddressMSg += "Enter 5 digit Zip Code\n";
             validate = false;
       }

       if (validate) {
             var ReturnValue = CallAddressValidateService(Addressline1, Addressline2, City, State, ZipCode, Country,Type,CrewName,ClientName,Mode,StaffCode,DOB,AppliedRank);

             if (ReturnValue.split(',')[0].split(':')[1] == "Invalid") {
                 console.log("USAddressValidation:Invalid");
                 AddressMSg += ReturnValue.split(',')[1].split(':')[1];
             }
             else if (ReturnValue.split(',')[0].split(':')[1] == "ValidAddress") {
                 console.log("USAddressValidation:Valid");
                 AddressMSg += "Valid Address is:\n";
                 AddressMSg += ReturnValue.split(',')[1] + "\n";
                 AddressMSg += ReturnValue.split(',')[2] + "\n";
                 AddressMSg += ReturnValue.split(',')[3] + "\n";
                 AddressMSg += ReturnValue.split(',')[4] + "\n";
                 AddressMSg += ReturnValue.split(',')[5] + "\n";
             }
             else if (ReturnValue.split(',')[0].split(':')[1] == "Error")
             {
               AddressMSg +="Error";
               console.log("USAddressValidation:Error");
             }
      }

      $("#__divMsgTooltip").hide();
     $("#__divMsgTooltip").html('');

  return AddressMSg;
}

function CallAddressValidateService(AddressLine1, AddressLine2, City, State, ZipCode, Country,Type,CrewName,ClientName,Mode,StaffCode,DOB,AppliedRank) {
    var returnValue = "";
    Country="US";
    var Address = "{ AddressLine1: '" + $.trim(AddressLine2) + "', AddressLine2: '" + $.trim(AddressLine1) + "', City: '" + $.trim(City) + "', State: '" + $.trim(State) + "', ZipCode: '" + $.trim(ZipCode) + "', Country: '" + $.trim(Country) + "', Type: '"+$.trim(Type)+"', CrewName:'"+$.trim(CrewName)+"',ClientName: '"+$.trim(ClientName)+"', Mode:'"+Mode+"',StaffCode:'"+StaffCode+"',DOB:'"+DOB+"',AppliedRank:'"+AppliedRank+"' }";

    $.ajax({
        type: "POST",
        url:  "/"+__app_name +"/webservice.asmx/ValidateUSAddress",
        data: Address,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (r) {
            returnValue = r.d;
        },
        error: function (r) {
            //
        },
        failure: function (r) {
            //
        }
    });

    return returnValue;
}
