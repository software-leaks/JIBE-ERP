$(document).ready(function () {
    $('.draggable').draggable();
    $('body').click(function () { $('.toolBox').hide(); });
    $('#dialog').dialog({
        autoOpen: false,
        modal: true,
        width: 800,
        buttons: {
            "Close": function () {
                $(this).dialog("close");
            },
            "Reload": function () {
                var url = "ViewEvent.aspx?id=" + $('#dialog').attr('alt') + "&rnd=" + Math.random();

                $("#dialog").dialog({ title: 'Loading Data ...' });

                $.get(url, function (data) {
                    $('#dialog').html(data);
                    $("#dialog").dialog({ title: 'Event Details' });
                });
            }
        }
    });
   
    

});


function RegisterHideTool() {

    document.body.onmouseover = hideToolBox;
}



function bindClientEvents() {

    $('.Agent-overout').mouseover(function (evt) { var ev = (window.event) ? event : evt; var Reqid = $(this).find('#dhnReqID').val(); var Quoted = $(this).find('#hdnQuoted').val(); GetQuoteAgents(Reqid, Quoted); $('#dvQuoteAgents').show(); SetPosition_Relative(ev, 'dvQuoteAgents'); }).mouseout(function () { $('#dvQuoteAgents').hide(); });
}

function bindPaxsName() {
    $('.PaxCount-overout').mouseover(function (evt) { var Reqid = $(this).find('#hdfReqID').val(); GetPaxsName(Reqid); $('#dvGetPaxName').show(); SetPosition_Relative(evt, 'dvGetPaxName'); }).mouseout(function () { $('#dvGetPaxName').hide(); });
}



function bindRoutInfo() {
    $('.Rout-overout').mouseover(function (evt) { var Reqid = $(this).find('#hdfRoutReqID').val(); GetRoutInfo(Reqid); $('#dvGetRountInfo').show(); SetPosition_Relative(evt, 'dvGetRountInfo'); }).mouseout(function () { $('#dvGetRountInfo').hide(); });
}



function bindVesselPortCall() {
    $('.PortCall-overout').mouseover(function (evt) { var Reqid = $(this).find('#hdfVesselID').val(); GetVesselPortCall(Reqid); $('#dvGetVesselPortCall').show(); SetPosition_Relative(evt, 'dvGetVesselPortCall'); }).mouseout(function () { $('#dvGetVesselPortCall').hide(); });
}





function AddNewPax(id, OptionExpiry) {
    if (OptionExpiry != '') {
        alert("Can not add passenger after quotation has been received!!!");
        return false;
    }
    else {
        var url = 'AddNewPax.aspx?requestid=' + id;
        OpenPopupWindow('AddPaxWID', 'Add Passenger', url, 'popup', 500, 1200, null, null, true, false, true, AddPax_Closed);
    }
}
function AddPax_Closed() {

    return true;
}

function SendRFQ_Closed() {

    /* Refresh the page */

   // window.location.reload(true);

    //            var menuId = document.getElementById("<%=lnkMenu1.ClientID%>").id;
    //            $('#' + menuId).trigger('click');
    //return true;

    __doPostBack('ctl00$MainContent$lnkMenu' + currentlink, '')
    return true;
}

function SendRFQ(ReqID) {
    // var url = 'SendRFQ.aspx?requestid=' + ReqID;
    // OpenPopupWindow('SendRFQWID', 'Send RFQ', url, 'popup', 600, 1200, null, null, true, false, true, SendRFQ_Closed);


    var url = 'SendRFQ.aspx?requestid=' + ReqID;
    document.getElementById("iFrmPopup").src = url;

    showModal('dvPopUp', true, SendRFQ_Closed);
    

}


function UploadeTicket(ReqID) {

    var url = 'IssueTicket.aspx?requestid=' + ReqID;
    document.getElementById("iFrmPopup").src = url;

    showModal('dvPopUp', true, "");

}



var lastExecutor = null;
function RequestUserPreference(ReqID) {

    if (lastExecutor != null)
        lastExecutor.abort();

    var UserID = '<%=GetSessionUserID().ToString() %>'
    var service = Sys.Net.WebServiceProxy.invoke('../WebService.asmx', 'RequestUserPreference', false, { "RequestID": ReqID, "UserID": UserID }, RequestUser_onSuccess, RequestUser_onFail);
    lastExecutor = service.get_executor();
}
function RequestUser_onFail(retVal) {
    alert(retVal.message);
}
function RequestUser_onSuccess(retVal) {

    alert('success');

}

function RequestApproval(ReqID) {
    if (lastExecutor != null)
        lastExecutor.abort();

    var UserID = '<%=GetSessionUserID().ToString() %>'
    var service = Sys.Net.WebServiceProxy.invoke('../WebService.asmx', 'Get_ApprovalUserList', false, { "RequestID": ReqID, "UserID": UserID }, RequestApproval_onSuccess, RequestApproval_onFail);
    lastExecutor = service.get_executor();
}
function RequestApproval_onFail(retVal) {

    alert(retVal._message);
}
function RequestApproval_onSuccess(retVal) {

}



function openEvaluation(ReqID) {
    var url = 'Evaluation.aspx?requestid=' + ReqID;

    window.open(url, 'Evaluation');

}
function openAttachments(ReqID,ReqSts) {


    //            var url = 'Attachment.aspx?atttype=DOCUMENT&requestid=' + ReqID;
    //            OpenPopupWindow('AttachmentWID', 'Attachments', url, 'popup', 550, 900, null, null, true, false, true, Attachment_Closed);

    var url = 'Attachment.aspx?atttype=DOCUMENT&requestid=' + ReqID+'&ReqSts='+ReqSts;

    document.getElementById("iFrmPopup").src = url;

    showModal('dvPopUp', true, null);

}
function Attachment_Closed() {

    return true;
}
function openRemarks(ReqID) {
    var url = 'Remarks.aspx?requestid=' + ReqID;

    OpenPopupWindow('RemarksWID', 'Remarks', url, 'popup', 550, 900, null, null, true, false, true, Remarks_Closed);

}
function Remarks_Closed() {

    return true;
}

function GetQuoteAgents(id, Quoted) {

    TravelService.GetQuoteAgent(id, Quoted, onGetQuoteAgents);
}
function onGetQuoteAgents(result) {
    $('#dvQuoteAgents').html(result);

}


function GetPaxsName(rqstid) {
    TravelService.GetPaxsName(rqstid, onGetPaxNames);
}

function onGetPaxNames(PaxNameresult) {
    var dvGetPaxName = document.getElementById("dvGetPaxName");
    dvGetPaxName.innerHTML = PaxNameresult;
}

function GetRoutInfo(rqstid) {
    TravelService.GetRoutInfo(rqstid, onGetRoutInfo);
}

function onGetRoutInfo(RoutInforesult) {
    var dvRoutInfo = document.getElementById("dvGetRountInfo");
    dvRoutInfo.innerHTML = RoutInforesult;
}


function GetVesselPortCall(Vessel_ID) {
    TravelService.GetVesselPortCall(Vessel_ID, onGetVesselPortCall);
}

function onGetVesselPortCall(VesselPortCallresult) {
    var dvRoutInfo = document.getElementById("dvGetVesselPortCall");
    dvRoutInfo.innerHTML = VesselPortCallresult;
}


var objPreviousOption = null;

function showToolBox(obj) {

    hideToolBox();
    objPreviousOption = obj;

    var toolid = obj.id.replace('imgToolBox', 'dvToolBox');
    document.getElementById(toolid).style.display = 'block';
    $(obj).css({ 'borderStyle': 'solid', 'borderColor': 'Red', 'borderWidth': '1px' });
    $(document.getElementById(toolid)).css({ 'borderStyle': 'solid', 'borderColor': 'Red', 'borderWidth': '1px' });
   
}
function hideToolBox() {
    
    $('.toolBox').hide();

    if (objPreviousOption != null) {
        var toolid = objPreviousOption.id.replace('imgToolBox', 'dvToolBox');
        $(objPreviousOption).css({ 'borderStyle': 'solid', 'borderColor': 'transparent', 'borderWidth': '1px' });
        $(document.getElementById(toolid)).css({ 'borderStyle': 'solid', 'borderColor': 'gray', 'borderWidth': '1px' });
    }
}



function showEvent(ID, CrewID) {
    var url = "../CREW/ViewEvent.aspx?id=" + ID + "&CrewID=" + CrewID + "&rnd=" + Math.random();

    //-- show dialog --
    $('#dialog').dialog('open');

    //-- load event data --
    $.get(url, function (data) {
        $('#dialog').html(data);
    });

    //-- remember event id --
    $('#dialog').attr('alt', ID + "&CrewID=" + CrewID);
}


var SrcID = null;
var VesselName = null;
var RequestID_vsl = null;

function ShowdvChangeVessel(objthis, evt, ReqID) {

    RequestID_vsl = ReqID;
    document.getElementById('dvChangeVesselName').style.display = "block";
    SrcID = objthis.id;
    SetPosition_Relative(evt, 'dvChangeVesselName');
}



function OnRefund(id) {


    $.alerts.okButton = " Yes ";
    $.alerts.cancelButton = " No ";

    var strMsg = "This will put the request for REFUND !" + "\n\n"
                             + "Do you want to continue ?";

    var aa = jConfirm(strMsg, ' Confirmation Required !', function (r) {

        if (r) {

            var postBackstr = "__doPostBack('" + id.replace(/_/gi, '$') + "','" + id.replace(/_/gi, '$') + "_Click')";
            window.setTimeout(postBackstr, 0, 'JavaScript');
            return true;


        }
        else {
            return false;
        }
    }

            );

    return false;
}

function ShowdvChangeDeptDate(objthis, evt, ReqID) {

    document.getElementById("<%=txtChangeDeptdate.ClientID%>").value = "";
    RequestID_vsl = ReqID;
    document.getElementById('dvChangeDeptDate').style.display = "block";
    SrcID = objthis.id;
    SetPosition_Relative(evt, 'dvChangeDeptDate');
}

function ASync_ChangeDeptDate() {

    if (lastExecutor_Vsl != null)
        lastExecutor_Vsl.abort();

    var Userid = document.getElementById("<%=hdf_UserID.ClientID%>").value;
    var ChangeDeptdate = document.getElementById("<%=txtChangeDeptdate.ClientID%>").value;

    var service = Sys.Net.WebServiceProxy.invoke('../TravelService.asmx', 'UPD_Request_DepartDate', false, { "ReqID": RequestID_vsl, "DeptDate": ChangeDeptdate, "UserID": Userid }, onSuccessASync_ChangeDeptDate, OnfailChangeDeptDate);
    lastExecutor_Vsl = service.get_executor();

}

function OnfailChangeDeptDate() {
    alert('Error while changing date !');
    document.getElementById('dvChangeDeptDate').style.display = "none";
}

function onSuccessASync_ChangeDeptDate(retVal) {
    if (retVal != "0") {
        document.getElementById('dvChangeDeptDate').style.display = "none";
        document.getElementById(SrcID).innerHTML = retVal;

    }

}

var strmanagerApproverNames = "";


function CheckForPendingManagerApproval(id) {
   
    var Request_ID = document.getElementById("ctl00_MainContent_hdnRequestID_App").value;

    AsyncGetPendingManagerApproverName(Request_ID);
    var fun = "PendingManagerApproval('" + id + "')";
    setTimeout(fun, 500);
    return false;
}

function AsyncGetPendingManagerApproverName(Request_ID) {
   
    if (lastExecutor_Vsl != null)
        lastExecutor_Vsl.abort();
    var service = Sys.Net.WebServiceProxy.invoke('../TravelService.asmx', 'Get_Pending_Manager_Approval', false, { "RequestID": Request_ID }, onSuccessManagerApproverName, onfail_name);
    lastExecutor_Vsl = service.get_executor();


}

function onfail_name() {
}

function onSuccessManagerApproverName(Retval) {
  
    strmanagerApproverNames = Retval;
}

function PendingManagerApproval(id) {


    var findstr = strmanagerApproverNames.indexOf("No record found !");

    if (findstr < 0) {

        $.alerts.okButton = " Yes ";
        $.alerts.cancelButton = " No ";

        var strMsg = "Following manager's approval are pending :" + "\n\n"
                                       + strmanagerApproverNames + "\n\n"
                                       + "Do you want to continue with sending approval  ?";

        var aa = jConfirm(strMsg, ' Confirmation Required !', function (r) {

            if (r) {


                var postBackstr = "__doPostBack('" + id.replace(/_/gi, '$') + "','" + id.replace(/_/gi, '$') + "_Click')";
                window.setTimeout(postBackstr, 0, 'JavaScript');

                blnRetVal = true;
                return true;

            }
            else {
                blnRetVal = false;
                return false;
            }
        }

            );
        blnRetVal = false;
        return false;

    }
    else {

        var postBackstr = "__doPostBack('" + id.replace(/_/gi, '$') + "','" + id.replace(/_/gi, '$') + "_Click')";
        window.setTimeout(postBackstr, 0, 'JavaScript');

        return true;
    }

    return false;
}


function Showdiv_rollback(id) {
    document.getElementById("ctl00_MainContent_lblReqestIDRollback").innerHTML = 'Travel Request ID : '+id.toString(); 
    document.getElementById("ctl00_MainContent_hdf_TRID_Rollback").value = id;
    showModal('RollbackTR');
    return false;
}