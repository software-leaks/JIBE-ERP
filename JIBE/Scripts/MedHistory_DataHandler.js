var lastExecutor_WebServiceProxy = null;
var evt;
function showMedHistoryAttachments(event, VID, QID) {
    evt = event;
    if (lastExecutor_WebServiceProxy != null)
        lastExecutor_WebServiceProxy.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../JibeWebService.asmx', 'Get_MedHistory_Attachments', false, { "QueryID": QID, "VesselID": VID, "UserID": 0 }, showMedHistoryAttachments_onSuccess, showMedHistoryAttachments_onFail);
    lastExecutor_WebServiceProxy = service.get_executor();

}
function showMedHistoryAttachments_onSuccess(retval) {
    js_ShowToolTip_Fixed(retval , evt, null, "Attachments");
}
function showMedHistoryAttachments_onFail(err_) {
    alert(err_._message);
}
//--------------

function AddFollowUp(VID, QID) {
    $('[id$=HiddenField_QueryID]').val(QID);
    $('[id$=HiddenField_Vessel_ID]').val(VID);
    showModal('dvPopup', true);
}

function showMedHistoryFollowups(event, VID, QID) {
    var src = event.srcElement;
    var pos = $(src).offset();
    var width = $(src).width();

    var url = 'CrewQuery_FollowUp.aspx?VID=' + VID + '&QID=' + QID;

    $('#iframeFollowups').load(function () { this.style.height = (this.contentWindow.document.body.offsetHeight) + 'px'; });
    $('#iframeFollowups').attr("src", url);
    $('#dialog').show();
    $("#dialog").css({ "left": (pos.left - 600) + "px", "top": pos.top + "px", "width": 600 });
}

//-- claim attachments---
function showCostItemAttachments(event, CID, Case_ID, Vessel_ID, Office_ID) {
    evt = event;
    if (lastExecutor_WebServiceProxy != null)
        lastExecutor_WebServiceProxy.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../JibeWebService.asmx', 'Get_Med_CostItem_Attachments', false, { "Cost_Item_ID": CID, "Case_ID": Case_ID, "Vessel_ID": Vessel_ID, "Office_ID": Office_ID, "UserID": 0 }, showCostItemAttachments_onSuccess, showCostItemAttachments_onFail);
    lastExecutor_WebServiceProxy = service.get_executor();

}

function showCostItemAttachments_onSuccess(retval) {
    js_ShowToolTip_Fixed(retval , evt, null, "Attachments");
}
function showCostItemAttachments_onFail(err_) {
    alert(err_._message);
}