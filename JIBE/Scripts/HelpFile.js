



var lastExecutor = null;
function OnfailHelpPage() {
  
   
}

var _clt_id_page = "";

function set_ctl_id_page(__ctl_id_page) {
    _clt_id_page = __ctl_id_page;
}


var __isResponse = 1;
function asyncBindFAQList() {
    try {
    
        __isResponse = 0;

        if (lastExecutor != null)
            lastExecutor.abort();

      
        var _company = document.getElementById('ctl00_hdnCompanyID').value;

        //var Keywords = (document.getElementsByTagName("title")[0].innerText || document.getElementsByTagName("title")[0].textContent).replace(/ /gi, ","); 
        var Keywords = ""//$('meta[name=keywords]').attr("content");
        var AppName = document.getElementById('ctl00_hdnAppName').value;
        var appurl = document.getElementById('ctl00_hdnAppUrl').value; 
        if (Keywords == null)
            Keywords = "";



        var strurl = appurl  + '/JibeWebService.asmx';
        var splitword = (appurl  + '/').toUpperCase();
           var mainurl = window.location.href.toUpperCase();
           var searchurl = mainurl.split(splitword)[1];
           var service = Sys.Net.WebServiceProxy.invoke(strurl, 'Get_HelpPage', false, { "TableStyleCSS": "TableStyleCSS", "HeaderStyleCSS": "HeaderStyleCSS", "QuestionCSS": "QuestionCSS", "AnswerCSS": "AnswerCSS-Hide", "Keywords": Keywords, "CompanyID": _company, "VideoCSS": "VideoCSS","SearchUrl" : searchurl }, onSuccessasyncBindFAQList, OnfailHelpPage, new Array('t'));

        lastExecutor = service.get_executor();

    }
    catch (ex) {
        
        alert(ex._message);
    }
}

var evt1 = null;
var TargetctlID1 = null;
function onSuccessasyncBindFAQList(retVal, ev) {

    var targObj1 = document.getElementById(TargetctlID1);
    targObj1.style.display = "block"; 
    if ('pageX' in evt1) { // all browsers except IE before version 9
        pageX = evt1.pageX;
        pageY = evt1.pageY + 25;
    }
    else {  // IE before version 9
        pageX = evt1.clientX + document.documentElement.scrollLeft;
        pageY = evt1.clientY + document.documentElement.scrollTop + 25;
    }

    var targObj1 = document.getElementById(TargetctlID1);
    document.getElementById('dvasyncFAQList').innerHTML = retVal.split("~totalrecordfound~")[0];
	targObj1.style.left = (pageX - document.getElementById(TargetctlID1).clientWidth) + "px";
	targObj1.style.top =  hei;
	
    
  
  
  

    
}



function BrowseFAQ(FaqID, evt, objthis) {
    var UserID = document.getElementById('ctl00_HdnValue').value;
    var CompanyID = document.getElementById('ctl00_hdnCompanyID').value;
    var USERNAME = document.getElementById('ctl00_hdnUserName').value;
    var AppName = document.getElementById('ctl00_hdnAppName').value;
    window.open(window.location.origin + "/" + AppName + "/LMS/LMS_HelpFAQ.aspx?FAQID=" + FaqID + "&USERCOMPANYID=" + CompanyID + "&USERID=" + UserID + "&USERNAME=" + USERNAME, '_blank');
}

function showFAQModal(evt, TargetctlID) {

    document.getElementById('dvHelp').style.display = "block";
    SetPosition_FAQModal(evt, TargetctlID);
    asyncBindFAQList();
}

var hei = "";
function SetPosition_FAQModal(evt, TargetctlID) {

    var targObj = document.getElementById(TargetctlID);
    targObj.style.display = "none";  
    var pageX = 0;
    var pageY = 0;

    if ('pageX' in evt) { // all browsers except IE before version 9
        pageX = evt.pageX  ;
        pageY = evt.pageY+25;
    }
    else {  // IE before version 9
        pageX = evt.clientX + document.documentElement.scrollLeft - targObj.clientWidth-100;
        pageY = evt.clientY + document.documentElement.scrollTop+25;
    }

    var pageHeight = document.body.scrollHeight;
    var height = targObj.clientHeight;

    var pageWidth = document.body.scrollWidth;
    var width = targObj.clientWidth;

  

    if (pageHeight < (height + pageY)) {

        if (pageY - height < 0)
            hei = "10px";
        else
            hei = (pageY - height) + "px";
    }
    else {
        hei = (pageY + 5) + "px";
    }

    if (pageWidth < (width + pageX)) {

      // targObj.style.left = (pageX - (width - 350)) + "px";
      //  targObj.style.left = (pageX- 300)+ "px";
       
    }
    else {
     //   targObj.style.left = (pageX + 10) + "px";

    }
    evt1 = evt;
    TargetctlID1 = TargetctlID;
    
}

function hideFAQModal() {

    document.getElementById('dvHelp').style.display = "none";
}

function OpenVideo(src, Item_name, Item_Desc, Duration) {

    var UserID = document.getElementById('ctl00_HdnValue').value;
    var CompanyID = document.getElementById('ctl00_hdnCompanyID').value;
    var USERNAME = document.getElementById('ctl00_hdnUserName').value;
    var AppName = document.getElementById('ctl00_hdnAppName').value;
    window.open(window.location.origin + "/" + AppName + "/LMS/LMS_HelpFAQ.aspx?src=" + src + "&Item_name=" + Item_name + "&Item_Desc=" + Item_Desc + "&Duration=" + Duration + "&USERCOMPANYID=" + CompanyID + "&USERID=" + UserID + "&USERNAME=" + USERNAME, '_blank');

}