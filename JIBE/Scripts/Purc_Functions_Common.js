var lastExecutor = null;

function GetRemarkAll(doccode, userid, IsMasterPage, evt) { //use to display headers and data in horizontal format.
    var ev = null;
    if (evt != null)
        ev = evt;
    else
        ev = window.event;

    async_Reqsn_Remarks_View_Add(doccode, '0', ev);

    document.getElementById('hdfDocumentCode').value = doccode;
    document.getElementById('hdfUserID').value = userid;

    //    Async_getPurchaseRemarksAll(doccode, '0');

    //    var dvremark = document.getElementById("dvPruchremarkMain");
    //    dvremark.style.display = "block";
    //    dvremark.style.left = (ev.x - 710) + "px";
    //    dvremark.style.top = ev.y + "px";

    if (IsMasterPage == null || IsMasterPage == "")
        setTimeout("SetPageSize(" + ev.y + ",dvPruchremarkMain)", 400);

}

function GetRemarkToolTip(doccode, IsMasterPage, evt) {

    var ev = null;
    if (evt != null)
        ev = evt;
    else
        ev = window.event;

    var dvremark = document.getElementById("dvPurchaseRemark");
    dvremark.innerHTML = "";

    Async_getPurchaseRemarks(doccode, '0');


    dvremark.style.display = "block";
    dvremark.style.left = (ev.x - 200) + "px";
    dvremark.style.top = ev.y + "px";


    if (IsMasterPage == null || IsMasterPage == "") {
        setTimeout("SetPageSize(" + ev.y + ",dvPurchaseRemark)", 400);
    }
    else {

        setTimeout("ResizePageHeight(" + ev.y + ",dvPurchaseRemark)", 400);
    }

}



function CloseRemarkAll() {
    document.getElementById("dvPruchremarkMain").style.display = "none";
}

function CloseRemarkToolTip() {

    document.getElementById("dvPurchaseRemark").style.display = "none";

}

function SavePurcReamrk() {
    var DocCode = document.getElementById('hdfDocumentCode').value;
    var userid = document.getElementById('hdfUserID').value;
    var Remark = document.getElementById('newasynctxtRemark').value.replace("'", " ").replace("&", " ");

    Async_InsPurchaseRemarks(DocCode, userid, Remark, '304');
    // document.getElementById("dvPruchremarkMain").style.display = "none";
    Close_Reqsn_Remarks_View_Add();

}

function GetRemark_ByRemarkType(RemarkType, DocCode) {

    var ev = window.event;

    Async_getPurchaseRemarks(DocCode, RemarkType);
    document.getElementById("dvPurchaseRemark").style.display = "block";
    document.getElementById("dvPurchaseRemark").style.left = (ev.x - 260) + "px";
    document.getElementById("dvPurchaseRemark").style.top = ev.y + "px";
}

function SetPageSize(y, div) {

    var pageHeight = document.body.scrollHeight;
    var height = div.clientHeight;
    if (pageHeight < (height + y)) {
        parent.ResizeFromChild(height + y, '1');
    }

}


function Async_Get_SupplierDetails_ByCode(SuppCode) {

    var url = "../webservice.asmx/Purc_Get_SupplierDetails_ByCode";
    var params = 'SuppCode=' + SuppCode;


    obj = new AsyncResponse(url, params, response_getSupplierDetails);
    obj.getResponse();
}


function Async_Get_Supplier_Status(SuppCode) {

    var url = "../webservice.asmx/Purc_Get_Supplier_Status";
    var params = 'SuppCode=' + SuppCode;


    obj = new AsyncResponse(url, params, response_GetSupplierStatus);
    obj.getResponse();
}


String.prototype.strim = function () {
    return this.replace(/^[\s]+/, '').replace(/[\s]+$/, '').replace(/[\s]{2,}/, '').replace('&nbsp;', '');
}

function DisplayActionInHeader(ctlToolTips, gridid) {
    if (ctlToolTips) {
        document.getElementById("lblActionDisplayText").innerHTML = ctlToolTips;
    }

}


function ResizePageHeight(y, div) {

    var pageHeight = document.body.scrollHeight;
    var height = div.clientHeight;
    if (pageHeight < (height + y)) {
        document.getElementById("dv_MainContent_master").style.minHeight = height + y + 5 + 'px';
    }


}

function Async_Get_Reqsn_Validity(ReqsnCode) {

    return true;

    //    var url = "../webservice.asmx/Purc_Get_Check_ReqsnValidity";
    //    var params = 'ReqsnCode=' + ReqsnCode;


    //    obj = new AsyncResponse(url, params, response_Get_Reqsn_Validity);
    //    obj.getResponse();
}


function response_Get_Reqsn_Validity(retval) {

    var ar, arS;

    if (retval.indexOf('Working') >= 0) { return; }
    try {

        retval = clearXMLTags(retval);

        if (retval.indexOf('ERROR:', 0) >= 0 || retval.indexOf('error', 0) >= 0) {

            return;
        }

        if (retval == 0) {
            alert("Request can not be completed because this requisition is not active into system!");
            return false;
        }


    }

    catch (ex) { alert(ex.message); }

}

function RefreshPendingDetails() {

    //window.opener.location.reload();
    window.opener.location = window.opener.location.href;

}



function asyncGetMachinery_Popup(systemid, evt, objthis) {

    if (lastExecutor != null)
        lastExecutor.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../../webservice.asmx', 'asyncGetMachinery_Popup', false, { "systemid": systemid }, onSuccessASync_asyncGetMachinery_Popup, Onfail, new Array(evt, objthis));

    lastExecutor = service.get_executor();
}




function onSuccessASync_asyncGetMachinery_Popup(retVal, eventArgs) {
    js_ShowToolTip("<div style='width:300px;'>" + retVal + "<div>", eventArgs[0], eventArgs[1]);
}


function Onfail(msg) {

    //      alert(msg._message);
}


function Get_Reqsn_ProcessingTime_By_Reqsn(Requisition_Code, evt, objthis, isclicked, pageheader) {

    if (lastExecutor != null)
        lastExecutor.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../JibeWebService.asmx', 'Get_Reqsn_Processing_Time_BY_Reqsn', false, { "Requisition_Code": Requisition_Code }, onSuccess_Get_Reqsn_ProcessingTime_By_Reqsn, Onfail, new Array(evt, objthis, isclicked, pageheader));

    lastExecutor = service.get_executor();

}

function onSuccess_Get_Reqsn_ProcessingTime_By_Reqsn(retVal, Args) {
    if (Args[2].toString() == "0")
        js_ShowToolTip(retVal, Args[0], Args[1], Args[3]);
    else
        js_ShowToolTip_Fixed(retVal, Args[0], Args[1], Args[3]);
}


function Get_ToopTipsForQtnRecve(DocumentCode, evt, objthis) {

    if (lastExecutor != null)
        lastExecutor.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../JibeWebService.asmx', 'async_GetToopTipsForQtnRecve', false, { "DocumentCode": DocumentCode }, onSuccess_Get_ToopTipsForQtnRecve, Onfail, new Array(evt, objthis));

    lastExecutor = service.get_executor();

}

function onSuccess_Get_ToopTipsForQtnRecve(retVal, Args) {

    js_ShowToolTip(retVal, Args[0], Args[1]);

}


function Get_ToopTipsForQtnSent(DocumentCode, evt, objthis) {

    if (lastExecutor != null)
        lastExecutor.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../JibeWebService.asmx', 'async_GetToopTipsForQtnSent', false, { "DocumentCode": DocumentCode }, onSuccess_Get_ToopTipsForQtnSent, Onfail, new Array(evt, objthis));

    lastExecutor = service.get_executor();

}

function onSuccess_Get_ToopTipsForQtnSent(retVal, Args) {

    js_ShowToolTip(retVal, Args[0], Args[1]);

}
function Get_ToopTipsForQtnDeclined(DocumentCode, evt, objthis) {

    if (lastExecutor != null)
        lastExecutor.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../JibeWebService.asmx', 'async_Get_ToopTipsForQtnDeclined', false, { "DocumentCode": DocumentCode }, onSuccess_Get_ToopTipsForQtnDeclined, Onfail, new Array(evt, objthis));

    lastExecutor = service.get_executor();

}

function onSuccess_Get_ToopTipsForQtnDeclined(retVal, Args) {

    js_ShowToolTip(retVal, Args[0], Args[1]);

}

function Get_ToopTipsForQtnINProgress(DocumentCode, evt, objthis) {

    if (lastExecutor != null)
        lastExecutor.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../JibeWebService.asmx', 'async_GetToopTipsForQtnINProgress', false, { "DocumentCode": DocumentCode }, onSuccess_ToopTipsForQtnINProgress, Onfail, new Array(evt, objthis));

    lastExecutor = service.get_executor();

}

function onSuccess_ToopTipsForQtnINProgress(retVal, Args) {

    js_ShowToolTip(retVal, Args[0], Args[1]);

}


var lastExecutor_Remark = null;
function async_Reqsn_Remarks_View_Add(DocCode, RemarkType, evt) {

    document.getElementById('hdfDocumentCode').value = DocCode;

    //create controls
    try {
        var objdivAddReqsnRemark = document.getElementById("__divAddReqsnRemark");
        if (objdivAddReqsnRemark != null)
            document.body.removeChild(objdivAddReqsnRemark);
    } catch (ex) { }
    var divMsg_Obj = document.getElementById("__divAddReqsnRemark");

    if (divMsg_Obj == null) {

        var __divMsg = document.createElement("div");
        __divMsg.id = "__divAddReqsnRemark";
        __divMsg.style.fontFamily = 'Tahoma,verdana';
        __divMsg.style.position = "absolute";
        __divMsg.style.display = 'block';
        __divMsg.style.zIndex = "99990";
        __divMsg.style.border = '4px solid #FFA500';
        __divMsg.style.padding = '0px';
        __divMsg.style.width = '500px';
        __divMsg.style.fontSize = '11px';
        __divMsg.style.color = '#000000';
        __divMsg.style.background = '#F5F5F5';
        __divMsg.style.borderRadius = "7px";
        __divMsg.style.MozBorderRadius = "7px";
        __divMsg.style.WebkitBorderRadius = "7px";

        __divMsg.innerHTML = "<div style='text-align:left;font-weight:bold;font-size:12px;padding:3px;background-color:#FFA500;color:black;font-family:Tahoma,verdana'>Remarks </div> <div style='margin:3px'> <div style='width:100%;height:100px;overflow-y:scroll;' id='__divViewRemark'> </div> <div  style='width:100%'  id='__divAddRemark'><br> <B> Enter Remark :</b><br> <textarea style='width:99%' maxlength='1000' id='newasynctxtRemark' rows='4' ></textarea> </div> <div style='text-align:center;margin:10px'> <button style='height:25px;width:70px;background-color:#4DD2FF;font-size:12px' onclick='SavePurcReamrk()' type='button'>Save</button> &nbsp;&nbsp;&nbsp; <button style='height:25px;width:70px;background-color:#4DD2FF;font-size:12px' onclick='Close_Reqsn_Remarks_View_Add()'  type='button'>Close</button> </div> </div>  ";
        document.body.appendChild(__divMsg);


    }


    document.getElementById("__divViewRemark").innerHTML = "loading...";
    document.getElementById('newasynctxtRemark').value = "";
    SetPosition_Relative(evt, '__divAddReqsnRemark');

    //

    if (lastExecutor_Remark != null)
        lastExecutor_Remark.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../JibeWebService.asmx', 'async_GetReqsn_Remarks', false, { "DocumentCode": DocCode, "RemarkType": RemarkType }, onSuccess_GetReqsn_Remarks, Onfail, new Array(evt));

    lastExecutor_Remark = service.get_executor();
}


function onSuccess_GetReqsn_Remarks(retVal, Args) {

    document.getElementById("__divViewRemark").innerHTML = retVal;
    $("#__tbl_remark").scrollableTable({ type: "th" });

}

function Close_Reqsn_Remarks_View_Add() {

    var objdivAddReqsnRemark = document.getElementById("__divAddReqsnRemark");
    document.body.removeChild(objdivAddReqsnRemark);
}


function ShowReqsnAttachment(queryString) {
  
    parent.OpenPopupWindowBtnID('ReqsnAttachment_ID','Requisition Attachments', 'ReqsnAttachment.aspx?' + queryString, 'popup', 600, 800, null, null, false, false, true, false, 'id');

}

