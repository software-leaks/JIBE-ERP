var lastExecutor = null;

function Onfail(msg) {

          alert(msg._message);
}


function Get_CaptCash_Items_Attachments(CaptCash_Details_ID, Vessel_ID, evt, objthis, isclicked, pageheader) {

    if (lastExecutor != null)
        lastExecutor.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../JibeWebService.asmx', 'asyncGet_CaptCash_Items_Attachments', false, { "Vessel_ID": Vessel_ID,"CaptCash_Details_ID": CaptCash_Details_ID }, onSuccess_CaptCash_Items_Attachments, Onfail, new Array(evt, objthis, isclicked, pageheader));

    lastExecutor = service.get_executor();

}

function onSuccess_CaptCash_Items_Attachments(retVal, Args) {

    js_ShowToolTip_Fixed(retVal, Args[0], Args[1], Args[3]);
}


var lastExecutorWF = null;


function asyncGet_Welfare_Details_Documents(Welfare_Details_ID, Vessel_ID, evt, objthis, isclicked, pageheader) {
    

    if (lastExecutorWF != null)
        lastExecutorWF.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../JibeWebService.asmx', 'asyncGet_Welfare_Details_Documents', false, { "Welfare_Details_ID": Welfare_Details_ID, "Vessel_ID": Vessel_ID }, onSuccess_Welfare_Details_Documents, Onfail, new Array(evt, objthis, isclicked, pageheader));

    lastExecutorWF = service.get_executor();

}

function onSuccess_Welfare_Details_Documents(retVal, Args) {

    js_ShowToolTip_Fixed(retVal, Args[0], Args[1], Args[3]);
}

var lastExecutorPB = null;


function asyncGet_Portage_Bill_Attachments(Vessel_ID, month, year, Doc_type, evt, objthis, isclicked, pageheader) {


    if (lastExecutorPB != null)
        lastExecutorPB.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../JibeWebService.asmx', 'asyncGet_Portage_Bill_Attachments', false, { "Vessel_ID": Vessel_ID, "month": month, "year": year, "Doc_type": Doc_type }, onSuccess_asyncGet_Portage_Bill_Attachments, Onfail, new Array(evt, objthis, isclicked, pageheader));

    lastExecutorPB = service.get_executor();

}

function onSuccess_asyncGet_Portage_Bill_Attachments(retVal, Args) {

    js_ShowToolTip_Fixed(retVal, Args[0], Args[1], Args[3]);
}