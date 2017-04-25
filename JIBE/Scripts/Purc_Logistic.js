var lastExecutor = null;
function Onfail() { }

function ASync_Get_Approval(Log_ID, evt, objthis) {

    if (lastExecutor != null)
        lastExecutor.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../WebService.asmx', 'Purc_Get_Approval_History', false, { "Log_ID": Log_ID }, onSuccessASync_Get_Approval, Onfail, new Array(evt, objthis));
    lastExecutor = service.get_executor();

}



function onSuccessASync_Get_Approval(retVal, eventArgs) {

    js_ShowToolTip(retVal, eventArgs[0], eventArgs[1]);
}



function ASync_Get_Log_Remark(Log_ID, evt, objthis, isClicked, Remark_Type) {

    if (isClicked.toString() == "1") {
        document.getElementById("dvInsRemark").style.display = "block";
        document.getElementById('hdf_Log_ID').value = Log_ID;

    }

    if (!Remark_Type)
        Remark_Type = 0;

    if (lastExecutor != null)
        lastExecutor.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../WebService.asmx', 'Get_Log_Remark', false, { "Log_ID": Log_ID, "Remark_Type": Remark_Type }, onSuccessASync_Get_Log_Remark, Onfail, new Array(evt, objthis, isClicked));
    lastExecutor = service.get_executor();

}


function onSuccessASync_Get_Log_Remark(retVal, eventArgs) {

    if (eventArgs[2].toString() == "1") {

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

function ASync_Ins_Log_Remark(evt, objthis) {

    var Log_ID = document.getElementById('hdf_Log_ID').value;
    var User_ID = document.getElementById("ctl00_MainContent_hdf_User_ID").value;
    var Remark_Type = "1";
    var Remark = document.getElementById('txtNewRemark').value
    document.getElementById("dvInsRemark").style.display = "none";

    if (lastExecutor != null)
        lastExecutor.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../WebService.asmx', 'Ins_Log_Remark', false, { "Remark_Type": Remark_Type, "Remark": Remark, "User_ID": User_ID, "Log_ID": Log_ID }, onSuccessASync_Ins_Log_Remark, Onfail, new Array(evt, objthis));
    lastExecutor = service.get_executor();

}


function onSuccessASync_Ins_Log_Remark(retVal, eventArgs) {
    document.getElementById("dvInsRemark").style.display = "none";

}