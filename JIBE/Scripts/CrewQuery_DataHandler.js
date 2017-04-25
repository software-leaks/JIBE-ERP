var lastExecutor_WebServiceProxy = null;
var evt;
function showQueryAttachments(event, VID, QID) {
    evt = event;
    if (lastExecutor_WebServiceProxy != null)
        lastExecutor_WebServiceProxy.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../JibeWebService.asmx', 'Get_CrewQuery_Attachments', false, { "QueryID": QID, "VesselID": VID, "UserID": 0 }, showQueryAttachments_onSuccess, showQueryAttachments_onFail);
    lastExecutor_WebServiceProxy = service.get_executor();

}
function showQueryAttachments_onSuccess(retval) {
    js_ShowToolTip_Fixed(retval , evt, null, "Attachments");
}
function showQueryAttachments_onFail(err_) {
    alert(err_._message);
}
//--------------

function AddFollowUp(VID, QID) {
    $('[id$=HiddenField_QueryID]').val(QID);
    $('[id$=HiddenField_Vessel_ID]').val(VID);
    showModal('dvPopup', true);
}

function showQueryFollowups(event, VID, QID) {
    var src = event.srcElement;
    var pos = $(src).offset();
    var width = $(src).width();

    var url = 'CrewQuery_FollowUp.aspx?VID=' + VID + '&QID=' + QID;

    $('#iframeFollowups').load(function () { this.style.height = (this.contentWindow.document.body.offsetHeight) + 'px'; });
    $('#iframeFollowups').attr("src", url);
    $('#dialog').show();
    $("#dialog").css({ "left": (pos.left - 600) + "px", "top": pos.top + "px", "width": 600 });
}

//function showQueryFollowups(event, VID, QID) {
//    evt = event;
//    if (lastExecutor_WebServiceProxy != null)
//        lastExecutor_WebServiceProxy.abort();

//    var service = Sys.Net.WebServiceProxy.invoke('../JibeWebService.asmx', 'Get_CrewQuery_Followups', false, { "QueryID": QID, "VesselID": VID, "UserID": 0 }, showQueryFollowups_onSuccess, showQueryFollowups_onFail);
//    lastExecutor_WebServiceProxy = service.get_executor();

//}
//function showQueryFollowups_onSuccess(retval) {
//    js_ShowToolTip(retval, evt, null, "Followups");
//}
//function showQueryFollowups_onFail(err_) {
//    alert(err_._message);
//}

//-- claim attachments---
function showClaimAttachments(event, VID, QID, CID) {
    evt = event;
    if (lastExecutor_WebServiceProxy != null)
        lastExecutor_WebServiceProxy.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../JibeWebService.asmx', 'Get_Claim_Attachments', false, { "QueryID": QID, "VesselID": VID, "ClaimID": CID, "UserID": 0 }, showClaimAttachments_onSuccess, showClaimAttachments_onFail);
    lastExecutor_WebServiceProxy = service.get_executor();

}

function showClaimAttachments_onSuccess(retval) {
    js_ShowToolTip_Fixed(retval , evt, null, "Attachments");
}
function showClaimAttachments_onFail(err_) {
    alert(err_._message);
}