function getOBJ(id) {
    var retOBJ = document.getElementById(id);
    if (!retOBJ)
        return null;
    else
        return retOBJ;
}

function toggleChild(oMe, childid) {
    var check = oMe.src.toLowerCase();
    //alert(childid);
    if (check.lastIndexOf("plus.png") != -1) {
        oMe.src = "images/minus.png"
        $('.child' + childid).show();
        $('.child' + childid).prev().show();
    }
    else {
        oMe.src = "images/plus.png"
        $('.child' + childid).hide();
        $('.child' + childid).prev().hide();
    }
}

//function toggleChild() {
//    oMe = window.event.srcElement

//    var childid = oMe.getAttribute("child", false);
//    var check = oMe.src.toLowerCase();

//    if (check.lastIndexOf("plus.png") != -1) {
//        oMe.src = "images/minus.png"
//        $('.' + childid).show();
//    }
//    else {
//        oMe.src = "images/plus.png"
//        $('.' + childid).hide();
//    }
//}

function openWindow(strURL, name, strType, strHeight, strWidth, qstring) {
    var newWin = null;
    if (newWin != null && !newWin.closed)
        newWin.close();
    var strOptions = "";
    if (strType == "console")
        strOptions = "resizable,height=" + strHeight + ",width=" + strWidth;
    if (strType == "fixed")
        strOptions = "status,height=" + strHeight + ",width=" + strWidth;
    if (strType == "elastic")
        strOptions = "toolbar,menubar,scrollbars," + "resizable,location,height=" + strHeight + ",width=" + strWidth;
    newWin = window.open(strURL + qstring.toString(), name, strOptions);
    newWin.focus();
}

function IsNumeric(id) {

    var obj = document.getElementById(id);
    if (isNaN(obj.value)) {
        obj.value = "";
        alert("Only number allowed !");
    }
}


var lastExecutor = null;
function Onfail(errmsg) { }

var _Request_ID = 0;
var _Agent_ID = 0;


function ASync_Get_Remark(Request_ID, Agent_ID, evt, objthis, isClicked) {


    if (isClicked.toString() == "1") {
        document.getElementById("dvInsRemark").style.display = "block";


    }

    _Request_ID = Request_ID;
    _Agent_ID = Agent_ID;

    if (lastExecutor != null)
        lastExecutor.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../TravelService.asmx', 'asyncGetRemarks', false, { "Request_ID": _Request_ID, "Agent_ID": _Agent_ID }, onSuccessASync_Get_Remark, Onfail, new Array(evt, objthis, isClicked));
    lastExecutor = service.get_executor();


}


function onSuccessASync_Get_Remark(retVal, eventArgs) {

    if (eventArgs[2].toString() == "1") {

        if (_Agent_ID == "0") {

            if (lastExecutor != null)
                lastExecutor.abort();

            var service = Sys.Net.WebServiceProxy.invoke('../TravelService.asmx', 'asyncGet_Agents_By_Request', false, { "Request_ID": _Request_ID }, onSuccessasyncGet_Agents_By_Request, Onfail, "");
            lastExecutor = service.get_executor();

        }
        document.getElementById("dvShowremark").innerHTML = retVal;

        document.getElementById('txtNewRemark').value = "";

        var __tbl_remark = document.getElementById("__tbl_remark");

        if (__tbl_remark != null) {
            __tbl_remark.style.width = '100%';
            __tbl_remark.style.border = '1px';
        }
        SetPosition_Relative(eventArgs[0], "dvInsRemark");


    }
    else {
        js_ShowToolTip(retVal, eventArgs[0], eventArgs[1]);
    }
}

function onSuccessasyncGet_Agents_By_Request(retval) {
    document.getElementById("dvReqAgentsRemark").innerHTML = retval;
}

function ASync_Ins_Remark(evt, objthis) {
    var RemarkAgentIDs = "";

    var User_ID = document.getElementById("ctl00_MainContent_hdf_UserID").value;

    var Remark = document.getElementById('txtNewRemark').value
    document.getElementById("dvInsRemark").style.display = "none";

    if (_Agent_ID == "0") {
        var hdfhtmlchkremarkcount = document.getElementById('hdfhtmlchkremarkcount').value;

        for (var i = 1; i <= parseInt(hdfhtmlchkremarkcount); i++) {

            var atid = 'htmlchkremark_' + i.toString();
            var objchkremark = document.getElementById(atid);
            if (objchkremark.checked)
                RemarkAgentIDs = RemarkAgentIDs + objchkremark.value + ",";
        }
    }

    if (lastExecutor != null)
        lastExecutor.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../TravelService.asmx', 'asyncAddRemarks', false, { "Request_ID": _Request_ID, "Remark": Remark, "UserID": User_ID, "Agent_ID": _Agent_ID, "RemarkAgentIDs": RemarkAgentIDs }, onSuccessASync_Ins_Remark, Onfail, new Array(evt, objthis));
    lastExecutor = service.get_executor();

}


function onSuccessASync_Ins_Remark(retVal, eventArgs) {
    document.getElementById("dvInsRemark").style.display = "none";

}

function RequiredValidator_DropDown(id, initialvalue, ErrorMsg) {
    var ddlobj = document.getElementById(id);
    var Datavalue = ddlobj.options[ddlobj.selectedIndex].value;
    if (Datavalue == initialvalue) {
        alert(ErrorMsg);
        return false;
    }

}


function asyncGet_PODetails_ByReqID(Request_ID, Supplier_Code, evt, objthis) {


    if (lastExecutor != null)
        lastExecutor.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../TravelService.asmx', 'asyncGet_PODetails_ByReqID', false, { "Request_ID": Request_ID, "Supplier_Code": Supplier_Code }, onSuccessasyncGet_PODetails_ByReqID, Onfail, new Array(evt, objthis));
    lastExecutor = service.get_executor();


}

function onSuccessasyncGet_PODetails_ByReqID(retVal, eventArgs) {
    js_ShowToolTip(retVal, eventArgs[0], eventArgs[1]);
}




function asyncGet_Marked_IsTravelled(Request_ID, objthis) {


    if (lastExecutor != null)
        lastExecutor.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../TravelService.asmx', 'asyncGet_Marked_IsTravelled', false, { "Request_ID": Request_ID }, onSucGet_Marked_IsTravelled, Onfail, new Array(objthis));
    lastExecutor = service.get_executor();


}

function onSucGet_Marked_IsTravelled(retVal, retarr) {
    if (retVal != 1) {
        alert('Departure date is in future . ');

        return false;
    }
    else {
        var id = retarr[0].id;
        var postBackstr = "__doPostBack('" + id.replace(/_/gi, '$') + "','" + id.replace(/_/gi, '$') + "_Click')";
        window.setTimeout(postBackstr, 0, 'JavaScript');
    }
}


function asyncUPD_Quote_Send_For_Approval(QuoteId, ths, RequestID, UserID) {

    var quote_sts = ths.checked == true ? 1 : 0;
    if (lastExecutor != null)
        lastExecutor.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../JibeWebService.asmx', 'UPD_Quote_Send_For_Approval', false, { "QuoteID": QuoteId, "Status": quote_sts, "RequestID": RequestID, "UserID": UserID }, SuccUPD_Quote_Send_For_Approval, Onfail, new Array(RequestID, quote_sts));
    lastExecutor = service.get_executor();

}

function SuccUPD_Quote_Send_For_Approval(retval, arrValues) {
    var stsqt = arrValues[1] == 1 ? true : false;

    if (parseFloat(arrValues[0].toString()) > 0) {

        $("#dvmaingrid input[type=checkbox]").attr("checked", stsqt);

    }
}


function asyncGet_Quote_Count_Approval(RequestID, dvModalPopID) {

    if (lastExecutor != null)
        lastExecutor.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../JibeWebService.asmx', 'Get_Quote_Count_Approval', false, { "RequestID": RequestID }, SuccasyncGet_Quote_Count_Approval, Onfail, new Array(RequestID, dvModalPopID));
    lastExecutor = service.get_executor();

}

function SuccasyncGet_Quote_Count_Approval(retval, arrPrm) {


    if (parseInt(retval) < 1) {
        var con = confirm('You have not selected quote for approver.Do you want to select the quote for approver ?');
        if (con) {

            if (arrPrm[1].toString() != 'dvSendForApprovalPIC') {
                var url = 'Evaluation.aspx?requestid=' + arrPrm[0].toString();

                window.open(url, 'Evaluation');
            }
            return false;

        }

    }
    else {
        showModal(arrPrm[1].toString());
        return false;
    }
}