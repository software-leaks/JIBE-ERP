var lastExecutor = null;


function GetOverdueBreakdown(Charter_Id, IsMasterPage, evt, objthis, pageheader) {

    var ev = null;
    if (evt != null)
        ev = evt;
    else
        ev = window.event;

    var dvremark = document.getElementById("dvOverdueRemark");
    dvremark.innerHTML = "";

    async_GetOutstandingRemarks(Charter_Id, ev, objthis, pageheader);




}




function CloseRemarkAll() {
    document.getElementById("dvOverdueRemark").style.display = "none";
}

function CloseRemarkToolTip() {

    document.getElementById("dvOverdueRemark").style.display = "none";

}





function SetPageSize(y, div) {

    var pageHeight = document.body.scrollHeight;
    var height = div.clientHeight;
    if (pageHeight < (height + y)) {
        parent.ResizeFromChild(height + y, '1');
    }

}



function ResizePageHeight(y, div) {

    var pageHeight = document.body.scrollHeight;
    var height = div.clientHeight;
    if (pageHeight < (height + y)) {
        document.getElementById("dv_MainContent_master").style.minHeight = height + y + 5 + 'px';
    }


}


var lastExecutor_Remark = null;
function async_GetOutstandingRemarks(Charter_Id, evt, objthis, pageheader) {


    if (lastExecutor_Remark != null)
        lastExecutor_Remark.abort();
    var service = Sys.Net.WebServiceProxy.invoke('../JibeCPService.asmx', 'async_GetOutstandingRemarks', false, { "Charter_ID": Charter_Id }, onSucc_LoadFunction, null, new Array(evt, 'dvOverdueRemark', objthis, pageheader));

    lastExecutor_Remark = service.get_executor();
}


function onSucc_LoadFunction(retval, eventArgs) {


//        document.getElementById(eventArgs[1]).innerHTML = retval;
//        var ev = eventArgs[0];
//        var dvremark = document.getElementById(eventArgs[1]);
//        dvremark.style.display = "block";
//        dvremark.style.left = (ev.x - 200) + "px";
//        dvremark.style.top = ev.y + "px";


    js_ShowToolTip(retval, eventArgs[0], eventArgs[2], eventArgs[3]);

}

