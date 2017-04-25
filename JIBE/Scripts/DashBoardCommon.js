function checkForMyAction(webpartid, retval) {

    if (ForMyActionLabels) {
        if (ForMyActionLabels.indexOf(webpartid) >= 0) {

            if (retval.length > 0 && retval.search(/no record found/i) < 0) {
                var ctlid = $('[id$=' + webpartid + ']').attr('id');

                $('#' + ctlid).addClass("ForMyAction");
            }
        }
    }
}


function onSucc_LoadFunction(retval, prm) {
    try {

        document.getElementById(prm[0]).innerHTML = retval;
        checkForMyAction(prm[1], retval);
    }
    catch (ex)
    { }
}
function onSucc_PerformanceManLoadFunction(retval, prm) {
    try {
        document.getElementById(prm[0]).innerHTML = retval[0];

        checkForMyAction(prm[1], retval[0]);
        document.getElementById("divPerManLastUpdated").innerText = retval[1];

        //  onLoad();
    }
    catch (ex)
    { }
}
function onSuccCompletedInsp_LoadFunction(retval, prm) {
    try {
        document.getElementById(prm[0]).innerHTML = retval;
        onLoad();
        checkForMyAction(prm[1], retval);

    }
    catch (ex)
    { }
}

function onSuccOverdueFileSchedules_LoadFunction(retval, prm) {
    try {
        document.getElementById(prm[0]).innerHTML = retval;
        onLoad();
        checkForMyAction(prm[1], retval);

    }
    catch (ex)
    { }
}

var lastExecutorOverdueFileSchApp = null;
function asyncGetOverdueFileSchedulesApproval() {

    try {
        var UserID = $('[id$=hdnUserID]').val();

        if (lastExecutorOverdueFileSchApp != null)
            lastExecutorOverdueFileSchApp.abort();

        var service = Sys.Net.WebServiceProxy.invoke('../JibeWebService.asmx', 'asyncGet_OverdueApproval_FileSchedules', false, { "UserID": UserID }, onSuccOverdueFileSchedules_LoadFunction, Onfail, new Array('dvWebPartOverdueFileScheduleApproval', 'lblWebPartOverdueFileScheduleApproval'));
        lastExecutorOverdueFileSchApp = service.get_executor();
    }
    catch (ex) {

        alert(ex);
    }
}

var lastExecutorOverdueFileSchRec = null;
function asyncGetOverdueFileSchedulesReceiving() {

    try {
        var UserID = $('[id$=hdnUserID]').val();

        if (lastExecutorOverdueFileSchRec != null)
            lastExecutorOverdueFileSchRec.abort();

        var service = Sys.Net.WebServiceProxy.invoke('../JibeWebService.asmx', 'asyncGet_OverdueReceiving_FileSchedules', false, { "UserID": UserID }, onSuccOverdueFileSchedules_LoadFunction, Onfail, new Array('dvWebPartOverdueFileScheduleReceiving', 'lblWebPartOverdueFileScheduleReceiving'));
        lastExecutorOverdueFileSchRec = service.get_executor();
    }
    catch (ex) {

        alert(ex);
    }
}
var lastExecutorIntv_By_UserID = null;
function load_PendingInterviewList_By_UserID() {
    var UserID = $('[id$=hdnUserID]').val();

    if (lastExecutorIntv_By_UserID != null)
        lastExecutorIntv_By_UserID.abort();

    var DateFormat = $('[id$=hdnDateFromatMasterPage]').val();
    var service = Sys.Net.WebServiceProxy.invoke('../JibeDashboardService.asmx', 'getPendingInterviewList_By_UserID', false, { "UserID": UserID, "DateFormat": DateFormat }, onSucc_LoadFunction, Onfail, new Array('dvInterviewSchedules_By_UserID', 'lblWebPartPendingInterviews_By_User'));
  

    lastExecutorIntv_By_UserID = service.get_executor();
}



var lastExecutorIntv_List = null;
function load_PendingInterviewList() {

    var UserID = $('[id$=hdnUserID]').val();

    if (lastExecutorIntv_List != null)
        lastExecutorIntv_List.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../JibeWebService.asmx', 'getPendingInterviewList', false, { "UserID": UserID }, onSucc_LoadFunction, Onfail, new Array('dvInterviewSchedules', 'lblWebPartAllPendingInterviews'));
    lastExecutorIntv_List = service.get_executor();
}

var lastExecutorPendingBriefing_List = null;
function load_PendingCrewBriefing() {

    var UserID = $('[id$=hdnUserID]').val();
    if (lastExecutorPendingBriefing_List != null) {
        lastExecutorPendingBriefing_List.abort();
    }
    var service = Sys.Net.WebServiceProxy.invoke('../JibeWebService.asmx', 'getPendingCrewBriefingList', false, { "UserID": UserID }, onSucc_LoadFunction, Onfail, new Array('dvWebPartPendingCrewBriefing', 'lblWebPartPendingCrewBriefing'));
}

function load_DocumentExpiryList() {

    var UserID = $('[id$=hdnUserID]').val();
    Async_getDocumentExpiryList(UserID);

}
function load_CrewChangeAlerts() {

    var UserID = $('[id$=hdnUserID]').val();
    Async_getCrewChangeAlerts(UserID);

}
function load_CrewUSVisaAlerts() {

    var UserID = $('[id$=hdnUserID]').val();
    Async_getUSVisaAlerts(UserID);

}
function load_Contract_ToSign_Alerts() {

    var UserID = $('[id$=hdnUserID]').val();
    Async_getContract_ToSign_Alerts(UserID);

}

function load_Contract_ToVerify_Alerts() {

    var UserID = $('[id$=hdnUserID]').val();
    Async_getContract_ToVerify_Alerts(UserID);

}
var lastExecutorCrewCpmplaints = null;
function load_CrewCpmplaints() {
    try {

        var UserID = $('[id$=hdnUserID]').val();

        if (lastExecutorCrewCpmplaints != null)
            lastExecutorCrewCpmplaints.abort();

        var service = Sys.Net.WebServiceProxy.invoke('../JibeWebService.asmx', 'AsyncGet_CrewCpmplaints', false, { "User_ID": UserID }, onSucc_LoadFunction, Onfail, new Array('dvCrewComplaints', 'lblWebPartCrewComplaints'));
        lastExecutorCrewCpmplaints = service.get_executor();

        // $('#dvCrewComplaints').load("../Crew/CrewComplaintList.aspx?userid=" + UserID + "&rnd=" + Math.random() + ' #dvCrewComplaintList');
        // checkForMyAction('lblWebPartCrewComplaints', retval);

    }
    catch (ex) { }

}



var lastExecutorReqsnCount = null;
function load_ReqsnCount() {
    try {

        var UserID = $('[id$=hdnUserID]').val();
        var CompanyID = $('[id$=hdfUserCompanyID]').val();
        if (lastExecutorReqsnCount != null)
            lastExecutorReqsnCount.abort();

        var service = Sys.Net.WebServiceProxy.invoke('../JibeWebService.asmx', 'AsyncGet_ReqsnCount', false, { "User_ID": UserID, "CompanyID": CompanyID }, onSucc_LoadFunction, Onfail, new Array('dvReqsnCount', 'lblWepPartReqsnCount'));
        lastExecutorReqsnCount = service.get_executor();

        //$('#dvReqsnCount').load("../Infrastructure/Snippets/Requisition_Count.aspx?userid=" + UserID + "&rnd=" + Math.random() + ' #dvReqsnCountMain');
        // checkForMyAction('lblWebPartCrewComplaints', retval);

    }
    catch (ex) { }

}
var lastExecutorRequisition_Processing_Time = null;
function load_Requisition_Processing_Time() {
    try {

        var UserID = $('[id$=hdnUserID]').val();
        var CompanyID = $('[id$=hdfUserCompanyID]').val();
        if (lastExecutorRequisition_Processing_Time != null)
            lastExecutorRequisition_Processing_Time.abort();

        var service = Sys.Net.WebServiceProxy.invoke('../JibeWebService.asmx', 'AsyncGet_Requisition_Processing_Time', false, { "User_ID": UserID, "CompanyID": CompanyID }, onSucc_LoadFunction, Onfail, new Array('dvReqsnProcessing', 'lblWebPartReqsnProcessing'));
        lastExecutorRequisition_Processing_Time = service.get_executor();

        //$('#dvReqsnProcessing').load("../Infrastructure/Snippets/Requisition_Processing_Time.aspx?userid=" + UserID + "&rnd=" + Math.random() + ' #ReqsnProcessingTime');
        // checkForMyAction('lblWebPartCrewComplaints', retval);

    }
    catch (ex) { }

}
var lastExecutorCrewEvaluations = null;
function load_CrewEvaluations() {

    try {

        var UserID = $('[id$=hdnUserID]').val();

        if (lastExecutorCrewEvaluations != null)
            lastExecutorCrewEvaluations.abort();

        var service = Sys.Net.WebServiceProxy.invoke('../JibeWebService.asmx', 'AsyncGet_CrewEvaluationsList', false, { "User_ID": UserID }, onSucc_LoadFunction, Onfail, new Array('dvEvaluations', 'lblWebPartEvaluations'));
        lastExecutorCrewEvaluations = service.get_executor();

        //$('#dvEvaluations').load("../Crewevaluation/CrewEvaluationList_DashBoard.aspx?rnd=" + Math.random() + ' #dvEvaluations_DashBoard');





        // checkForMyAction('lblWebPartEvaluations', retval);
    }
    catch (ex) { }

}
var lastExecutorCrewEvaluation_60Percent = null;
function load_CrewEvaluation_60Percent() {

    try {
        var UserID = $('[id$=hdnUserID]').val();

        if (lastExecutorCrewEvaluation_60Percent != null)
            lastExecutorCrewEvaluation_60Percent.abort();

        var service = Sys.Net.WebServiceProxy.invoke('../JibeWebService.asmx', 'AsyncGet_CrewEvaluation_60Percent', false, { "User_ID": UserID }, onSucc_LoadFunction, Onfail, new Array('dvWebPartEvaluationBelow60', 'lblWebPartEvaluationBelow60'));
        lastExecutorCrewEvaluation_60Percent = service.get_executor();

        //$('#dvWebPartEvaluationBelow60').load("snippets/Dash_CrewEvaluation_60Percent.aspx?rnd=" + Math.random() + ' #dvEvaluations_DashBoard');
        //checkForMyAction('lblWebPartEvaluationBelow60', retval);
    }
    catch (ex) { }

}
var lastExecutorDecklogAnomalies = null;
function load_DecklogAnomalies() {

    try {
        var UserID = $('[id$=hdnUserID]').val();

        if (lastExecutorDecklogAnomalies != null)
            lastExecutorDecklogAnomalies.abort();

        var service = Sys.Net.WebServiceProxy.invoke('../JibeWebService.asmx', 'AsyncGet_DecklogAnomalies', false, { "User_ID": UserID }, onSucc_LoadFunction, Onfail, new Array('dvWebPartDecklogAnomalies', 'lblWebPartDecklogAnomalies'));
        lastExecutorDecklogAnomalies = service.get_executor();

        //$('#dvWebPartDecklogAnomalies').load("snippets/Dash_Decklog_Anomalies.aspx?rnd=" + Math.random() + ' #dvDeckLog');
        //checkForMyAction('lblWebPartEvaluationBelow60', retval);
    }
    catch (ex) { }

}
var lastExecutorEnginelogAnomalies = null;
function load_EnginelogAnomalies() {

    try {
        var UserID = $('[id$=hdnUserID]').val();

        if (lastExecutorEnginelogAnomalies != null)
            lastExecutorEnginelogAnomalies.abort();

        var service = Sys.Net.WebServiceProxy.invoke('../JibeWebService.asmx', 'AsyncGet_EnginelogAnomalies', false, { "User_ID": UserID }, onSucc_LoadFunction, Onfail, new Array('dvWebPartEnginelogAnomalies', 'lblWebPartEnginelogAnomalies'));
        lastExecutorEnginelogAnomalies = service.get_executor();

        //$('#dvWebPartEnginelogAnomalies').load("snippets/Dash_Enginelog_Anomalies.aspx?rnd=" + Math.random() + ' #dvEngineLog');
        //checkForMyAction('lblWebPartEvaluationBelow60', retval);
    }
    catch (ex) { }

}
var lastExecutorCrewCardProposed = null;
function load_CrewCardProposed() {
    try {

        var UserID = $('[id$=hdnUserID]').val();

        if (lastExecutorCrewCardProposed != null)
            lastExecutorCrewCardProposed.abort();

        var service = Sys.Net.WebServiceProxy.invoke('../JibeWebService.asmx', 'AsyncGet_CrewCardProposed', false, { "User_ID": UserID }, onSucc_LoadFunction, Onfail, new Array('dvCrewCardProposed', 'lblWebPartCrewCardProposed'));
        lastExecutorCrewCardProposed = service.get_executor();

        // $('#dvCrewCardProposed').load("../Crew/CrewCardProposed_DashBoard.aspx?rnd=" + Math.random() + ' #dvCrewCardProposed_DashBoard');
        //checkForMyAction('lblWebPartCrewCardProposed', retval);
    }
    catch (ex) { }

}

//function load_PortCallsVessel() {
//    try {

//        // $('#dvPortCalls_Vessel').load("snippets/Dash_PortCalls_Vessel.aspx?rnd=" + Math.random() + ' #dvPortCalls_Vessel');

//        document.getElementById('imgPortCalls_Vessel').src = "Snippets/Dash_PortCalls_Vessel.aspx?rnd=" + Math.random();
//        //checkForMyAction('lblWebPartPortCallsVessel', retval);

//    }
//    catch (ex) { }

//}

//function load_PortCallsMonth() {
//    try {

//        // $('#dvPortCalls_Month').load("snippets/Dash_PortCall_Month.aspx?rnd=" + Math.random() + ' #dvPortCalls_Month');

//        document.getElementById('imgPortcalls_Month').src = "Snippets/Dash_PortCall_Month.aspx?rnd=" + Math.random();
//        // checkForMyAction('lblWebPartPortcallsMonth', retval);

//    }
//    catch (ex) { }

//}
var lastExecutorPortCallsVessel = null;
function load_PortCallsVessel() {
    try {
        var UserID = $('[id$=hdnUserID]').val();
        if (lastExecutorPortCallsVessel != null)
            lastExecutorPortCallsVessel.abort();

        var service = Sys.Net.WebServiceProxy.invoke('../JibeWebService.asmx', 'AsyncGet_PortCallsVessel', false, { "User_ID": UserID }, onSuccessasyncBindPortCallsVessel, Onfail, null);
        lastExecutorPortCallsVessel = service.get_executor();

        // $('#dvPortCalls_Vessel').load("snippets/Dash_PortCalls_Vessel.aspx?rnd=" + Math.random() + ' #dvPortCalls_Vessel');

        //document.getElementById('imgPortCalls_Vessel').src = "Snippets/Dash_PortCalls_Vessel.aspx?rnd=" + Math.random();//--commented to add google chart
        //checkForMyAction('lblWebPartPortCallsVessel', retval);

    }
    catch (ex) { }

}
function onSuccessasyncBindPortCallsVessel(retVal, ev) {

    __isResponse = 1;
    drawChartPortCallsVessel(retVal);
}
function drawChartPortCallsVessel(dataValues) {
    __isResponse = 1;

    var data = new google.visualization.DataTable();
    data.addColumn('string', 'VesselCode');
    data.addColumn('number', 'Port Count');

    for (var i = 0; i < dataValues.length; i++) {
        data.addRow([dataValues[i].VesselCode, dataValues[i].PortCount]);
    }

    var view = new google.visualization.DataView(data);
    view.setColumns([0, 1,
                       { calc: "stringify",
                           sourceColumn: 1,
                           type: "string",
                           role: "annotation"
                       }
                       ]);

    var options = { 'title': '',
        hAxis: { textStyle: {

            fontSize: 10

        }, slantedText: true, slantedTextAngle: 30, showTextEvery: 1
        },
        annotations: { alwaysOutside: true },
        vAxis: { minValue: 0 }
    };

    var chart = new google.visualization.ColumnChart(document.getElementById('dvWebPartPortCallsVessel'));

    chart.draw(view, options);
}
var lastExecutorPortCallsMonth = null;
function load_PortCallsMonth() {
    try {

        // $('#dvPortCalls_Month').load("snippets/Dash_PortCall_Month.aspx?rnd=" + Math.random() + ' #dvPortCalls_Month');

        //document.getElementById('imgPortcalls_Month').src = "Snippets/Dash_PortCall_Month.aspx?rnd=" + Math.random();//--commented to add google chart
        // checkForMyAction('lblWebPartPortcallsMonth', retval);
        var UserID = $('[id$=hdnUserID]').val();
        if (lastExecutorPortCallsMonth != null)
            lastExecutorPortCallsMonth.abort();

        var service = Sys.Net.WebServiceProxy.invoke('../JibeWebService.asmx', 'AsyncGet_PortCallsMonth', false, { "User_ID": UserID }, onSuccessasyncBindPortCallsMonth, Onfail, null);
        lastExecutorPortCallsMonth = service.get_executor();
    }
    catch (ex) { }

}
function onSuccessasyncBindPortCallsMonth(retVal, ev) {

    __isResponse = 1;
    drawChartPortCallsMonth(retVal);
}
function drawChartPortCallsMonth(dataValues) {
    __isResponse = 1;

    var data = new google.visualization.DataTable();
    data.addColumn('string', 'header');
    data.addColumn('number', 'Port Count');

    for (var i = 0; i < dataValues.length; i++) {
        data.addRow([dataValues[i].header, dataValues[i].PortCount]);
    }

    var view = new google.visualization.DataView(data);
    view.setColumns([0, 1,
                       { calc: "stringify",
                           sourceColumn: 1,
                           type: "string",
                           role: "annotation"
                       }
                       ]);

    var options = { 'title': 'Month-Year/Vessel Count',
        hAxis: { maxAlternation: 1, showTextEvery: 1, slantedText: true, slantedTextAngle: 30, textStyle: {

            fontSize: 10

        }
        },
        annotations: { alwaysOutside: true },
        vAxis: { minValue: 0 }
    };

    var chart = new google.visualization.ColumnChart(document.getElementById('dvWebPartPortcallsMonth'));

    chart.draw(view, options);
}
var lastExecutorshowEscalationLog = null;
function showEscalationLog(evt, wlid, vid, userid) {

    var oMe = evt.target || window.event.srcElement;
    var check = oMe.src.toLowerCase();
    //var url = "../Crew/CrewComplaintLog.aspx?wlid=" + wlid + "&vid=" + vid + "&userid=" + userid + "&rnd=" + Math.random();

    if (check.lastIndexOf("plus.png") != -1) {
        oMe.src = "../images/Minus.png"
        $('.' + wlid).show();
        var DepID;
        var Assignor = null;

        DepID = $('[id$=hdfUserdepartmentid]').val();
        if (lastExecutorshowEscalationLog != null)
            lastExecutorshowEscalationLog.abort();

        var service = Sys.Net.WebServiceProxy.invoke('../JibeWebService.asmx', 'asyncGet_EscalationLog', false, { "wlid": wlid, "vid": vid, "userid": userid }, onSucc_showEscalationLog, Onfail, new Array('dvLog' + wlid));
        lastExecutorshowEscalationLog = service.get_executor();

        // $.get(url, function (data) { $('#dvLog' + wlid).html(data); });
    }
    else {
        oMe.src = "../images/Plus.png"
        $('.' + wlid).hide();
    }
}
function onSucc_showEscalationLog(retVal, prm) {

    __isResponse = 1;
    document.getElementById(prm[0]).innerHTML = retVal;
}
var lastExecutor = null;
function Onfail(errmsg) {
    alert(errmsg);
}


var lastExecutorPendingNCRBYDept = null;
function asyncGetPendingNCRBYDept() {
    var DepID;
    var Assignor = null;

    DepID = $('[id$=hdfUserdepartmentid]').val();
    if (lastExecutorPendingNCRBYDept != null)
        lastExecutorPendingNCRBYDept.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../JibeWebService.asmx', 'asyncGet_Pending_NCR', false, { "Assignor": Assignor, "DepartmentID": DepID }, onSucc_LoadFunction, Onfail, new Array('dvPendingNCRByDept', 'lblWebPartPendingNCRBYDept'));
    lastExecutorPendingNCRBYDept = service.get_executor();
}


var lastExecutorPendingNCRALLDept = null;
function asyncGetPendingNCRALLDept() {
    var Assignor = null;

    if (lastExecutorPendingNCRALLDept != null)
        lastExecutorPendingNCRALLDept.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../JibeWebService.asmx', 'asyncGet_Pending_NCR_ALL_Dept', false, { "Assignor": Assignor }, onSucc_LoadFunction, Onfail, new Array('dvPendingNCRALLDept', 'lblWebPartPendingNCRALLDept'));
    lastExecutorPendingNCRALLDept = service.get_executor();
}


var lastExecutorAss = null;

function asyncGetPendingNCRByAssignor() {
    var DepID = null;
    var Assignor = 6;

    DepID = $('[id$=hdfUserdepartmentid]').val();

    if (lastExecutorAss != null)
        lastExecutorAss.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../JibeWebService.asmx', 'asyncGet_Pending_NCR', false, { "Assignor": Assignor, "DepartmentID": DepID }, onSucc_LoadFunction, Onfail, new Array('dvPendingNCRByAssignor', 'lblWebPartPendingNCRByAssignor'));
    lastExecutorAss = service.get_executor();
}


var lastExecutor_TravelPO = null;

function asyncGetPending_TravelPO() {

    var UserID = $('[id$=hdnUserID]').val();

    if (lastExecutor_TravelPO != null)
        lastExecutor_TravelPO.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../JibeWebService.asmx', 'asyncGet_Pending_Travel_PO', false, { "User_ID": UserID }, onSucc_LoadFunction, Onfail, new Array('dvPending_TravelPO', 'lblWebPartPendingTravelPO'));
    lastExecutor_TravelPO = service.get_executor();
}

var lastExecutor_Events_Done = null;

function asyncGet_Events_Done() {

    var UserID = $('[id$=hdnUserID]').val();

    if (lastExecutor_Events_Done != null)
        lastExecutor_Events_Done.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../JibeWebService.asmx', 'asyncGet_Events_Done', false, { "User_ID": UserID }, onSucc_LoadFunction, Onfail, new Array('dvEvents_Done', 'lblWebPartEvents_Done'));
    lastExecutor_Events_Done = service.get_executor();
}

var lastExecutor_LogisticPO = null;

function asyncGetPending_LogisticPO() {

    var UserID = $('[id$=hdnUserID]').val();

    if (lastExecutor_LogisticPO != null)
        lastExecutor_LogisticPO.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../JibeWebService.asmx', 'asyncGet_Pending_Logistic_PO', false, { "User_ID": UserID }, onSucc_LoadFunction, Onfail, new Array('dvPending_LogisticPO', 'lblWebPartPendingLogisticPO'));
    lastExecutor_LogisticPO = service.get_executor();
}


var lastExecutor_ReqsnPO = null;

function asyncGetPending_ReqsnPO() {

    var UserID = $('[id$=hdnUserID]').val();

    if (lastExecutor_ReqsnPO != null)
        lastExecutor_ReqsnPO.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../JibeWebService.asmx', 'asyncGet_Pending_Reqsn_PO', false, { "User_ID": UserID }, onSucc_LoadFunction, Onfail, new Array('dvPending_ReqsnPO', 'lblWebPartPendingForApproval'));
    lastExecutor_ReqsnPO = service.get_executor();
}


var lastExecutor_Provision_Last_Supplied = null;

function asyncGet_Provision_Last_Supplied() {


    if (lastExecutor_Provision_Last_Supplied != null)
        lastExecutor_Provision_Last_Supplied.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../JibeWebService.asmx', 'asyncGet_Provision_Last_Supplied', false, {}, onSucc_LoadFunction, Onfail, new Array('dvProvisionLastSupplied', 'lblWebPartSuppliedPROV'));
    lastExecutor_Provision_Last_Supplied = service.get_executor();
}


var lastExecutor_MyShortsCuts_Menu = null;

function asyncGet_MyShortsCuts_Menu() {
    var UserID = $('[id$=hdnUserID]').val();

    if (lastExecutor_MyShortsCuts_Menu != null)
        lastExecutor_MyShortsCuts_Menu.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../JibeWebService.asmx', 'asyncGet_User_Menu_Favourite', false, { 'UserID': UserID }, onSucc_LoadFunction, Onfail, new Array('dvMyShortsCuts_Menu', 'lblWebPartMyShortcuts'));
    lastExecutor_MyShortsCuts_Menu = service.get_executor();
}


var lastExecutor_WorkList = null;

function asyncGet_PendingWorkList() {
    var UserID = $('[id$=hdnUserID]').val();

    if (lastExecutor_WorkList != null)
        lastExecutor_WorkList.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../JibeWebService.asmx', 'asyncGet_PendingWorkList', false, { 'UserID': UserID }, onSucc_LoadFunction, Onfail, new Array('dvMyWork_List', 'lblWebpartPendingWorkList'));
    lastExecutor_WorkList = service.get_executor();
}


var lastExecutor_PendingWorkListVerification = null;

function asyncGet_PendingWorkListVerification() {
    var UserID = $('[id$=hdnUserID]').val();

    if (lastExecutor_PendingWorkListVerification != null)
        lastExecutor_PendingWorkListVerification.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../JibeWebService.asmx', 'asyncGet_PendingWorkListVerification', false, {}, onSucc_LoadFunction, Onfail, new Array('dvPending_WorkList_Verification', 'lblWebPartPendingWorkListVerification'));
    lastExecutor_PendingWorkListVerification = service.get_executor();
}


var lastExecutor_getWorklist_DueIn_7Days = null;

function asyncGet_getWorklist_DueIn_7Days() {
    var UserID = $('[id$=hdnUserID]').val();

    if (lastExecutor_getWorklist_DueIn_7Days != null)
        lastExecutor_getWorklist_DueIn_7Days.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../JibeWebService.asmx', 'getWorklist_DueIn_7Days', false, {}, onSucc_LoadFunction, Onfail, new Array('dvWebPartWorkListDue7Days', 'lblWebPartWorkListDue7Days'));
    lastExecutor_getWorklist_DueIn_7Days = service.get_executor();
}


var lastExecutor_CTMApproval = null;

function asyncGetPending_CTMApproval() {

    var UserID = $('[id$=hdnUserID]').val();

    if (lastExecutor_CTMApproval != null)
        lastExecutor_CTMApproval.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../JibeWebService.asmx', 'asyncGet_Pending_CTM_Approval', false, { "User_ID": UserID }, onSucc_LoadFunction, Onfail, new Array('dvWebpartPendingCTMApproval', 'lblWebPartPendingCTMApproval'));
    lastExecutor_CTMApproval = service.get_executor();
}


var lastExecutor_CTMConfirmation = null;

function asyncGet_CTM_Confirmation_Not_Received() {
    var UserID = $('[id$=hdnUserID]').val();

    if (lastExecutor_CTMConfirmation != null)
        lastExecutor_CTMConfirmation.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../JibeWebService.asmx', 'asyncGet_CTM_Confirmation_Not_Received', false, { "User_ID": UserID }, onSucc_LoadFunction, Onfail, new Array('dvWebPartCTMConfirmationFromVessel', 'lblWebPartCTMConfirmationFromVessel'));
    lastExecutor_CTMConfirmation = service.get_executor();
}


var lastExecutor_CylinderOilConsumption = null;

function asyncCylinder_Oil_Consumption() {

    var UserID = $('[id$=hdnUserID]').val();

    if (lastExecutor_CylinderOilConsumption != null)
        lastExecutor_CylinderOilConsumption.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../JibeWebService.asmx', 'asyncGet_Cylinder_Oil_Consumption', false, { "UserID": UserID }, onSucc_LoadFunction, Onfail, new Array('dvWebPartCylinderOilConsumption', 'lblWebPartCylinderOilConsumption'));
    lastExecutor_CylinderOilConsumption = service.get_executor();
}

var lastExecutorInspect_OverDue = null;
function asyncOverDue_Inspection() {

    var UserID = $('[id$=hdnUserID]').val();

    if (lastExecutorInspect_OverDue != null)
        lastExecutorInspect_OverDue.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../JibeWebService.asmx', 'asyncGet_OverDue_Inspection', false, { "UserID": UserID }, onSucc_LoadFunction, Onfail, new Array('dvWebPartInspectionOverdue', 'lblWebPartInspectionOverdue'));
    lastExecutorInspect_OverDue = service.get_executor();
}

var lastExecutorInspect_Due = null;
function asyncDueInMonth_Inspection() {

    var UserID = $('[id$=hdnUserID]').val();

    if (lastExecutorInspect_Due != null)
        lastExecutorInspect_Due.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../JibeWebService.asmx', 'asyncGet_DueInMonth_Inspection', false, { "UserID": UserID }, onSucc_LoadFunction, Onfail, new Array('dvWebPartInspectionDueInMonth', 'lblWebPartInspectionDueInMonth'));
    lastExecutorInspect_Due = service.get_executor();
}

var lastExecutorInspect_Completed = null;
function asyncCompleted_Inspection() {

    var UserID = $('[id$=hdnUserID]').val();

    if (lastExecutorInspect_Completed != null)
        lastExecutorInspect_Completed.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../JibeWebService.asmx', 'asyncGet_Completed_Inspection', false, { "UserID": UserID }, onSuccCompletedInsp_LoadFunction, Onfail, new Array('dvWebPartInspectionCompleted', 'lblWebPartInspectionCompleted'));
    lastExecutorInspect_Completed = service.get_executor();
}
var lastExecutorEvaluation_Feedback = null;
function asyncEvaluation_Feedback() {

    var UserID = $('[id$=hdnUserID]').val();

    if (lastExecutorEvaluation_Feedback != null)
        lastExecutorEvaluation_Feedback.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../JibeWebService.asmx', 'asyncGet_CrewEvaluation_Feedback', false, { "UserID": UserID }, onSucc_LoadFunction, Onfail, new Array('dvWebPartCrewEvaluationFeedback', 'lblWebPartCrewEvaluationFeedback'));
    lastExecutorEvaluation_Feedback = service.get_executor();
}
//-------Purchase Inventory Items Below Treshold ----------------//
var lastExecutorInvItems_BelowTreshold = null;
function asyncInvItems_BelowTreshold() {

    var UserID = $('[id$=hdnUserID]').val();

    if (lastExecutorInvItems_BelowTreshold != null)
        lastExecutorInvItems_BelowTreshold.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../JibeWebService.asmx', 'asyncGet_InvItems_BelowTreshold', false, { "UserID": UserID }, onSucc_LoadFunction, Onfail, new Array('dvWebPartBelowTresholdInventoryItems', 'lblWebPartBelowTresholdInventoryItems'));
    lastExecutorInvItems_BelowTreshold = service.get_executor();
}
//-------Operations My Worklist ----------------//
var lastExecutor_asyncGet_MyOperationWorklist = null;
function asyncGet_MyOperationWorklist() {
    var UserID = $('[id$=hdnUserID]').val();
    //alert();
    if (lastExecutor_asyncGet_MyOperationWorklist != null)
        lastExecutor_asyncGet_MyOperationWorklist.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../JibeWebService.asmx', 'Get_MyOperationWorklist', false, { 'UserID': UserID }, onSucc_LoadFunction, Onfail, new Array('dvMyOperationWorklist', 'server'));
    lastExecutor_asyncGet_MyOperationWorklist = service.get_executor();
}


//-------Operation Worklist DueIn7 Days ----------------//
var lastExecutor_asyncGet_OpsWorklistDueIn7Days = null;
function asyncGet_OpsWorklistDueIn7Days() {
    var UserID = $('[id$=hdnUserID]').val();
    //alert();
    if (lastExecutor_asyncGet_OpsWorklistDueIn7Days != null)
        lastExecutor_asyncGet_OpsWorklistDueIn7Days.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../JibeWebService.asmx', 'Get_OpsWorklistDueIn7Days', false, { 'UserID': UserID }, onSucc_LoadFunction, Onfail, new Array('dvWebPartOpsWorklistDueIn7Days', 'server'));
    lastExecutor_asyncGet_OpsWorklistDueIn7Days = service.get_executor();
}


//--asyncGet_OpsWorklistOverdue--
var lastExecutor_asyncGet_OpsWorklistOverdue = null;
function asyncGet_OpsWorklistOverdue() {
    var UserID = $('[id$=hdnUserID]').val();
    //alert();
    if (lastExecutor_asyncGet_OpsWorklistOverdue != null)
        lastExecutor_asyncGet_OpsWorklistOverdue.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../JibeWebService.asmx', 'Get_OpsWorklistOverdue', false, { 'UserID': UserID }, onSucc_LoadFunction, Onfail, new Array('dvWebPartOpsWorklistOverdue', 'server'));
    lastExecutor_asyncGet_OpsWorklistOverdue = service.get_executor();
}


// 

//--asyncGet_PMSOverdueJobs--
var lastExecutor_asyncGet_PMSOverdueJobs = null;
function asyncGet_PMSOverdueJobs() {
    var UserID = $('[id$=hdnUserID]').val();
    //alert();
    if (lastExecutor_asyncGet_PMSOverdueJobs != null)
        lastExecutor_asyncGet_PMSOverdueJobs.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../JibeWebService.asmx', 'Get_PMS_Overdue_Job', false, { 'User_ID': UserID }, onSucc_LoadFunction, Onfail, new Array('dvWebPartPMSOverdueJobs', 'lblWebPartPMSOverdueJobs'));
    lastExecutor_asyncGet_PMSOverdueJobs = service.get_executor();
}


// -- survey snippets -- //
//1//
var lastExecutor_asyncGet_Surv_DueinNext30Days = null;
function asyncGet_Surv_DueinNext30Days() {
    var UserID = $('[id$=hdnUserID]').val();
    if (lastExecutor_asyncGet_Surv_DueinNext30Days != null)
        lastExecutor_asyncGet_Surv_DueinNext30Days.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../JibeWebService.asmx', 'Get_Surv_DueinNext30Days', false, { 'UserID': UserID }, onSucc_LoadFunction, Onfail, new Array('dvWebSurvDueinNext30Days', 'lblWebPartSurvDueinNext30Days'));
    lastExecutor_asyncGet_Surv_DueinNext30Days = service.get_executor();
}


//2//
var lastExecutor_asyncGet_Surv_DueinNext7DaysAndOverdue = null;
function asyncGet_Surv_DueinNext7DaysAndOverdue() {
    var UserID = $('[id$=hdnUserID]').val();
    if (lastExecutor_asyncGet_Surv_DueinNext7DaysAndOverdue != null)
        lastExecutor_asyncGet_Surv_DueinNext7DaysAndOverdue.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../JibeWebService.asmx', 'Get_Surv_DueinNext7DaysAndOverdue', false, { 'UserID': UserID }, onSucc_LoadFunction, Onfail, new Array('dvSurvDueinNext7DaysAndOverdue', 'lblWebPartSurvDueinNext7DaysAndOverdue'));
    lastExecutor_asyncGet_Surv_DueinNext7DaysAndOverdue = service.get_executor();
}


//3//
var lastExecutor_asyncGet_Surv_PendingVerification = null;
function asyncGet_Surv_PendingVerification() {
    var UserID = $('[id$=hdnUserID]').val();
    if (lastExecutor_asyncGet_Surv_PendingVerification != null)
        lastExecutor_asyncGet_Surv_PendingVerification.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../JibeWebService.asmx', 'Get_Surv_PendingVerification', false, { 'UserID': UserID }, onSucc_LoadFunction, Onfail, new Array('dvSurvPendingVerification', 'lblWebPartSurvPendingVerification'));
    lastExecutor_asyncGet_Surv_PendingVerification = service.get_executor();
}


//4//
var lastExecutor_asyncGet_Surv_NA_PendingVerification = null;
function asyncGet_Surv_NA_PendingVerification() {
    var UserID = $('[id$=hdnUserID]').val();
    if (lastExecutor_asyncGet_Surv_NA_PendingVerification != null)
        lastExecutor_asyncGet_Surv_NA_PendingVerification.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../JibeWebService.asmx', 'Get_Surv_NA_PendingVerification', false, { 'UserID': UserID }, onSucc_LoadFunction, Onfail, new Array('dvSurvNAPendingVerification', 'lblWebPartSurvNAPendingVerification'));
    lastExecutor_asyncGet_Surv_NA_PendingVerification = service.get_executor();
}
//5// by vasu
var lastExecutor_asyncGet_Surv_ExpDateBydayCountFor90 = null;
function asyncGet_Surv_ExpDateBydayCountFor90() {
    var UserID = $('[id$=hdnUserID]').val();
    var ExpairyFromDays = $('[id$=hdnFromDays31]').val();
    var ExpairyToDays = $('[id$=hdnToDays90]').val(); 
    if (lastExecutor_asyncGet_Surv_ExpDateBydayCountFor90 != null)
        lastExecutor_asyncGet_Surv_ExpDateBydayCountFor90.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../JibeWebService.asmx', 'Get_Surv_ExpDateBydayCount', false, { 'UserID': UserID, 'ExpairyFromDaysCount': ExpairyFromDays, 'ExpairyToDaysCount': ExpairyToDays }, onSucc_LoadFunction, Onfail, new Array('dvSurvExpiryin31to90days', 'lblWebPartSurvExpiryin31to90days'));
    lastExecutor_asyncGet_Surv_ExpDateBydayCountFor90 = service.get_executor();
}
//6// by vasu
var lastExecutor_asyncGet_Surv_ExpDateBydayCountFor30 = null;
function asyncGet_Surv_ExpDateBydayCountFor30() {
    var UserID = $('[id$=hdnUserID]').val();
    var ExpairyFromDays = $('[id$=hdnFromDays8]').val();
    var ExpairyToDays = $('[id$=hdnToDays30]').val();
    if (lastExecutor_asyncGet_Surv_ExpDateBydayCountFor30 != null)
        lastExecutor_asyncGet_Surv_ExpDateBydayCountFor30.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../JibeWebService.asmx', 'Get_Surv_ExpDateBydayCount', false, { 'UserID': UserID, 'ExpairyFromDaysCount': ExpairyFromDays, 'ExpairyToDaysCount': ExpairyToDays }, onSucc_LoadFunction, Onfail, new Array('dvSurvExpiryin7to30days', 'lblWebPartSurvExpiryin7to30days'));
    lastExecutor_asyncGet_Surv_ExpDateBydayCountFor30 = service.get_executor();
}
//7// by vasu

var lastExecutor_asyncGet_Surv_ExpDateBydayCountFor7 = null;
function asyncGet_Surv_ExpDateBydayCountFor7() {
   
    var UserID = $('[id$=hdnUserID]').val();
    var ExpairyFromDays = $('[id$=hdnFromDays0]').val();
    var ExpairyToDays = $('[id$=hdnToDays7]').val();
    if (lastExecutor_asyncGet_Surv_ExpDateBydayCountFor7 != null)
        lastExecutor_asyncGet_Surv_ExpDateBydayCountFor7.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../JibeWebService.asmx', 'Get_Surv_ExpDateBydayCount', false, { 'UserID': UserID, 'ExpairyFromDaysCount': ExpairyFromDays, 'ExpairyToDaysCount': ExpairyToDays }, onSucc_LoadFunction, Onfail, new Array('divSurvExpiryinLessThen7days', 'lblWebPartSurvExpiryinLessThen7days'));
    lastExecutor_asyncGet_Surv_ExpDateBydayCountFor7 = service.get_executor();
}

var lastExecutor_asyncGetPendingCardApprovalList = null;
function asyncGetPendingCardApprovalList() {


    var UserID = $('[id$=hdnUserID]').val();

    if (lastExecutor_asyncGetPendingCardApprovalList != null)
        lastExecutor_asyncGetPendingCardApprovalList.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../JibeWebService.asmx', 'GetPendingCardApprovalList', false, { "UserID": UserID }, onSucc_LoadFunction, Onfail, new Array('dvWebPartPendingCardApproval', 'lblWebPartPendingCardApproval'));
    lastExecutor_asyncGetPendingCardApprovalList = service.get_executor();
}



var lastExecutor_asyncGet_PendingSupplierApprovalList = null;
function asyncGet_PendingSupplierApprovalList() {

    var UserID = $('[id$=hdnUserID]').val();

    if (lastExecutor_asyncGet_PendingSupplierApprovalList != null)
        lastExecutor_asyncGet_PendingSupplierApprovalList.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../JibeWebService.asmx', 'GetPendingSupplierApprovalList', false, { "UserID": UserID }, onSucc_LoadFunction, Onfail, new Array('dvWebPendingASLEvaluation', 'lblWebPendingASLEvaluation'));
    lastExecutor_asyncGet_PendingSupplierApprovalList = service.get_executor();
}

var lastExecutor_asyncGet_PendingInvoiceApproval = null;
function asyncGet_PendingInvoiceApproval() {

    var UserID = $('[id$=hdnUserID]').val();

    if (lastExecutor_asyncGet_PendingInvoiceApproval != null)
        lastExecutor_asyncGet_PendingInvoiceApproval.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../JibeWebService.asmx', 'GetPendingInvoiceApproval', false, { "UserID": UserID }, onSucc_LoadFunction, Onfail, new Array('dvWebPendingInvoiceApproval', 'lblWebPendingInvoiceApproval'));
    lastExecutor_asyncGet_PendingInvoiceApproval = service.get_executor();
}

var lastExecutor_asyncGet_OpexVesselReport = null;
function asyncGet_OpexVesselReport() {

    var UserID = $('[id$=hdnUserID]').val();

    if (lastExecutor_asyncGet_OpexVesselReport != null)
        lastExecutor_asyncGet_OpexVesselReport.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../JibeWebService.asmx', 'Get_Opex_Vessel_Report', false, { "UserID": UserID }, onSucc_LoadFunction, Onfail, new Array('dvWebOpexVesselReport', 'lblWebOpexVesselReport'));
    lastExecutor_asyncGet_OpexVesselReport = service.get_executor();
}

var lastExecutor_AsyncGet_VoyageAlert = null;
function AsyncGet_VoyageAlert() {

    var UserID = $('[id$=hdnUserID]').val();

    if (lastExecutor_AsyncGet_VoyageAlert != null)
        lastExecutor_AsyncGet_VoyageAlert.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../JibeWebService.asmx', 'AsyncGet_VoyageAlert', false, { "UserID": UserID }, onSucc_LoadFunction, Onfail, new Array('divVoyage', 'lblVoyage'));
    lastExecutor_AsyncGet_VoyageAlert = service.get_executor();
}


var lastExecutor_AsyncGet_CP_SnippetData = null;
function AsyncGet_CP_SnippetData() {

    var UserID = $('[id$=hdnUserID]').val();

    if (lastExecutor_AsyncGet_CP_SnippetData != null)
        lastExecutor_AsyncGet_CP_SnippetData.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../JibeWebService.asmx', 'Get_CP_SnippetData', false, { "UserID": UserID }, onSucc_LoadFunction, Onfail, new Array('dvCharterBook', 'lblCharterBook'));
    lastExecutor_AsyncGet_CP_SnippetData = service.get_executor();
}


var lastExecutor_asyncGet_CrewListByPerformance = null;
function asyncGet_CrewListByPerformance() {
    var UserID = $('[id$=hdnUserID]').val();
    if (lastExecutor_asyncGet_CrewListByPerformance != null)
        lastExecutor_asyncGet_CrewListByPerformance.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../JibeWebService.asmx', 'Get_CrewListByPerformance', false, { 'UserID': UserID }, onSucc_LoadFunction, Onfail, new Array('dvWebPartCrewPerformance', 'lblWebPartCrewPerformance'));
    lastExecutor_asyncGet_CrewListByPerformance = service.get_executor();
}

var lastExecutor_asyncGet_CrewEvaluationDueList = null;
function asyncGet_CrewEvaluationDueList() {
    var UserID = $('[id$=hdnUserID]').val();
    if (lastExecutor_asyncGet_CrewEvaluationDueList != null)
        lastExecutor_asyncGet_CrewEvaluationDueList.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../JibeWebService.asmx', 'Get_CrewEvaluationDueList', false, { 'UserID': UserID }, onSucc_LoadFunction, Onfail, new Array('dvWebPartCrewEvaluationDue', 'lblWebPartEvaluationDue'));
    lastExecutor_asyncGet_CrewEvaluationDueList = service.get_executor();
}

var lastExecutor_asyncGet_CrewListByPerformanceVerification = null;
function asyncGet_CrewListByPerformanceVerification() {
    var UserID = $('[id$=hdnUserID]').val();
    if (lastExecutor_asyncGet_CrewListByPerformanceVerification != null)
        lastExecutor_asyncGet_CrewListByPerformanceVerification.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../JibeWebService.asmx', 'Get_CrewListByPerformanceVerification', false, { 'UserID': UserID }, onSucc_LoadFunction, Onfail, new Array('dvWebPartCrewPerformanceVerification', 'lblWebPartCrewPerformanceVerification'));
    lastExecutor_asyncGet_CrewListByPerformanceVerification = service.get_executor();
}

var lastExecutorCrewOnboardList = null;
function asyncGet_CrewONBDList() {

    var UserID = $('[id$=hdnUserID]').val();

    if (lastExecutorCrewOnboardList != null)
        lastExecutorCrewOnboardList.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../JibeWebService.asmx', 'Get_CrewOnboardListRankWise', false, { "UserID": UserID }, onSucc_LoadFunction, Onfail, new Array('dvWebPartCrewONBDList', 'lblWebPartCrewONBDList'));
    lastExecutorCrewOnboardList = service.get_executor();
}

var lastExecutorCrewSeniorityReward = null;
function asyncGet_CrewSeniorityReward() {
    var UserID = $('[id$=hdnUserID]').val();

    if (lastExecutorCrewSeniorityReward != null)
        lastExecutorCrewSeniorityReward.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../JibeWebService.asmx', 'Get_CrewSeniorityReward', false, { "UserID": UserID }, onSucc_LoadFunction, Onfail, new Array('dvWebPartCrewSeniorityReward', 'lblWebPartCrewSeniorityReward'));
    lastExecutorCrewOnboardList = service.get_executor();
}
var lastExecutorPerformance_Manager = null;
function asyncGet_Performance_Manager() {

    var UserID = $('[id$=hdnUserID]').val();
    var Days = $('[id$=hdnDays]').val();
    if (lastExecutorPerformance_Manager != null)
        lastExecutorPerformance_Manager.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../JibeWebService.asmx', 'Get_Performance_Manager', false, { "UserID": UserID, "Days": Days }, onSucc_PerformanceManLoadFunction, Onfail, new Array('dvWebPartPerformanceManager', 'lblWebPartPerformanceManager'));
    lastExecutorPerformance_Manager = service.get_executor();
}
var lastExecutorWorklistIncident180Days = null;
function asyncGet_getWorklist_Incident_180Days() {

    var UserID = $('[id$=hdnUserID]').val();
    var Days = $('[id$=hdnDays]').val();
    if (lastExecutorWorklistIncident180Days != null)
        lastExecutorWorklistIncident180Days.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../JibeWebService.asmx', 'getWorklist_Incident_180days', false, {}, onSucc_LoadFunction, Onfail, new Array('dvWebPartWorklistIncident180Days', 'lblWebPartWorklistIncident180Days'));
    lastExecutorWorklistIncident180Days = service.get_executor();
}
var lastExecutorWorklistNearmiss180Days = null;
function asyncGet_getWorklist_Nearmiss_180Days() {

    var UserID = $('[id$=hdnUserID]').val();
    var Days = $('[id$=hdnDays]').val();
    if (lastExecutorWorklistNearmiss180Days != null)
        lastExecutorWorklistNearmiss180Days.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../JibeWebService.asmx', 'getWorklist_NearMiss_180days', false, {}, onSucc_LoadFunction, Onfail, new Array('dvWebPartWorklistNearmiss180Days', 'lblWebPartWorklistNearmiss180Days'));
    lastExecutorWorklistNearmiss180Days = service.get_executor();
}
/////------------------------------------------------------//////////

function refresh_snippet(_this) {

    var innerHtmlVerbs = _this.parentNode.innerHTML.toString();

    if (innerHtmlVerbs.indexOf('lblWebPartMyShortcuts') != -1) {
        asyncGet_MyShortsCuts_Menu();
    }

    else if (innerHtmlVerbs.indexOf('lblWebPartDocumentsexpire') != -1) {
        load_DocumentExpiryList();
    }
    else if (innerHtmlVerbs.indexOf('lblWebPartCrewChangeAlerts') != -1) {
        load_CrewChangeAlerts();
    }
    else if (innerHtmlVerbs.indexOf('lblWebPartEvaluations') != -1) {
        load_CrewEvaluations();
    }
    else if (innerHtmlVerbs.indexOf('lblWebPartSuppliedPROV') != -1) {
        asyncGet_Provision_Last_Supplied();
    }
    else if (innerHtmlVerbs.indexOf('lblWebPartPendingForApproval') != -1) {
        asyncGetPending_ReqsnPO();
    }
    else if (innerHtmlVerbs.indexOf('lblWebPartCrewComplaints') != -1) {
        load_CrewCpmplaints();
    }
    else if (innerHtmlVerbs.indexOf('lblWebPartPendingInterviews_By_User') != -1) {

        load_PendingInterviewList_By_UserID();
    }
    else if (innerHtmlVerbs.indexOf('lblWebPartAllPendingInterviews') != -1) {

        load_PendingInterviewList();
    }
    else if (innerHtmlVerbs.indexOf('lblWebPartPendingCrewBriefing') != -1) {
        load_PendingCrewBriefing();
    }
    else if (innerHtmlVerbs.indexOf('lblWebPartContractToSign') != -1) {
        load_Contract_ToSign_Alerts();
    }
    else if (innerHtmlVerbs.indexOf('lblWebPartContractToVerify') != -1) {
        load_Contract_ToVerify_Alerts();
    }
    else if (innerHtmlVerbs.indexOf('lblWebPartUSVisaAlert') != -1) {
        load_CrewUSVisaAlerts();
    }
    else if (innerHtmlVerbs.indexOf('lblWebPartCrewCardProposed') != -1) {
        load_CrewCardProposed();
    }
    else if (innerHtmlVerbs.indexOf('lblWebPartPendingNCRBYDept') != -1) {
        asyncGetPendingNCRBYDept();
    }
    else if (innerHtmlVerbs.indexOf('lblWebPartPendingNCRALLDept') != -1) {
        asyncGetPendingNCRALLDept();
    }
    else if (innerHtmlVerbs.indexOf('lblWebPartPendingNCRByAssignor') != -1) {
        asyncGetPendingNCRByAssignor();
    }

    else if (innerHtmlVerbs.indexOf('lblWebPartPendingTravelPO') != -1) {
        asyncGetPending_TravelPO();
    }
    else if (innerHtmlVerbs.indexOf('lblWebPartPendingLogisticPO') != -1) {
        asyncGetPending_LogisticPO();
    }
    else if (innerHtmlVerbs.indexOf('lblWebPartPortCallsVessel') != -1) {
        load_PortCallsVessel();
    }
    else if (innerHtmlVerbs.indexOf('lblWebPartPortcallsMonth') != -1) {
        load_PortCallsMonth();
    }
    else if (innerHtmlVerbs.indexOf('lblWebpartPendingWorkList') != -1) {

        asyncGet_PendingWorkList();
    }
    else if (innerHtmlVerbs.indexOf('lblWebPartPendingWorkListVerification') != -1) {
        asyncGet_PendingWorkListVerification();
    }
    else if (innerHtmlVerbs.indexOf('lblWebPartEvaluationBelow60') != -1) {
        load_CrewEvaluation_60Percent();
    }
    else if (innerHtmlVerbs.indexOf('lblWebPartEnginelogAnomalies') != -1) {
        load_DecklogAnomalies();
    }
    else if (innerHtmlVerbs.indexOf('lblWebPartEvaluationBelow60') != -1) {
        load_EnginelogAnomalies();
    }
    else if (innerHtmlVerbs.indexOf('lblWebPartWorkListDue7Days') != -1) {
        asyncGet_getWorklist_DueIn_7Days();
    }
    else if (innerHtmlVerbs.indexOf('lblWebPartPendingCTMApproval') != -1) {
        asyncGetPending_CTMApproval();
    }
    else if (innerHtmlVerbs.indexOf('lblWebPartCTMConfirmationFromVessel') != -1) {
        asyncGet_CTM_Confirmation_Not_Received();
    }
    else if (innerHtmlVerbs.indexOf('lblWebPartMyOperationWorklist') != -1) {
        asyncGet_MyOperationWorklist();
    }
    else if (innerHtmlVerbs.indexOf('lblWebPartOpsWorklistDueIn7Days') != -1) {
        asyncGet_OpsWorklistDueIn7Days();
    }
    else if (innerHtmlVerbs.indexOf('lblWebPartOpsWorklistOverdue') != -1) {
        asyncGet_OpsWorklistOverdue();
    }
    else if (innerHtmlVerbs.indexOf('lblWebPartPMSOverdueJobs') != -1) {
        asyncGet_PMSOverdueJobs();
    }
    else if (innerHtmlVerbs.indexOf('lblWebPartSurvDueinNext30Days') != -1) {
        asyncGet_Surv_DueinNext30Days();
    }
    else if (innerHtmlVerbs.indexOf('lblWebPartSurvDueinNext7DaysAndOverdue') != -1) {
        asyncGet_Surv_DueinNext7DaysAndOverdue();
    }
    else if (innerHtmlVerbs.indexOf('lblWebPartSurvPendingVerification') != -1) {
        asyncGet_Surv_PendingVerification();
    }
    else if (innerHtmlVerbs.indexOf('lblWebPartSurvNAPendingVerification') != -1) {
        asyncGet_Surv_NA_PendingVerification();
    }
        /* added by vasu */
    else if (innerHtmlVerbs.indexOf('lblWebPartSurvExpiryin31to90days') != -1) {
        asyncGet_Surv_ExpDateBydayCountFor90();
    }
    else if (innerHtmlVerbs.indexOf('lblWebPartSurvExpiryin7to30days') != -1) {
        asyncGet_Surv_ExpDateBydayCountFor30();
    }
    else if (innerHtmlVerbs.indexOf('lblWebPartSurvExpiryinLessThen7days') != -1) {
        asyncGet_Surv_ExpDateBydayCountFor7();
    }
    /* end */

    else if (innerHtmlVerbs.indexOf('lblWebPartCylinderOilConsumption') != -1) {
        asyncCylinder_Oil_Consumption();
    }
    else if (innerHtmlVerbs.indexOf('lblWebPartReqsnProcessing') != -1) {
        load_Requisition_Processing_Time();
    }
    else if (innerHtmlVerbs.indexOf('lblWepPartReqsnCount') != -1) {
        load_ReqsnCount();
    }
    else if (innerHtmlVerbs.indexOf('lblWebPartCrewPerformanceVerification') != -1) {
        asyncGet_CrewListByPerformanceVerification();
    }
    else if (innerHtmlVerbs.indexOf('lblWebPartCrewPerformance') != -1) {
        asyncGet_CrewListByPerformance();
    }
    else if (innerHtmlVerbs.indexOf('lblWebPartEvaluationDue') != -1) {
        asyncGet_CrewEvaluationDueList();
    }
    else if (innerHtmlVerbs.indexOf('lblWebPartInspectionOverdue') != -1) {
        asyncOverDue_Inspection();
    }
    else if (innerHtmlVerbs.indexOf('lblWebPartInspectionDueInMonth') != -1) {
        asyncDueInMonth_Inspection();
    }
    else if (innerHtmlVerbs.indexOf('lblWebPartInspectionCompleted') != -1) {
        asyncCompleted_Inspection();
    }
    else if (innerHtmlVerbs.indexOf('lblWebPartBelowTresholdInventoryItems') != -1) {
        asyncInvItems_BelowTreshold();
    }
    else if (innerHtmlVerbs.indexOf('lblWebPartPendingCardApproval') != -1) {
        asyncGetPendingCardApprovalList();
    }
    else if (innerHtmlVerbs.indexOf('lblWebPartCrewEvaluationFeedback') != -1) {
        asyncEvaluation_Feedback();
    }
    else if (innerHtmlVerbs.indexOf('lblWebPartCrewONBDList') != -1) {
        asyncGet_CrewONBDList();
    }


    else if (innerHtmlVerbs.indexOf('lblWebPartOverdueFileSchedule') != -1) {

        asyncGetOverdueFileSchedulesApproval();
    }
    else if (innerHtmlVerbs.indexOf('lblWebPartCrewSeniorityReward') != -1) {

        asyncGet_CrewSeniorityReward();
    }
    else if (innerHtmlVerbs.indexOf('lblWebPendingASLEvaluation') != -1) {

        asyncGet_PendingSupplierApprovalList();
    }
    else if (innerHtmlVerbs.indexOf('lblWebPendingInvoiceApproval') != -1) {

        asyncGet_PendingInvoiceApproval();
    }
    else if (innerHtmlVerbs.indexOf('lblWebOpexVesselReport') != -1) {

        asyncGet_OpexVesselReport();
    }


    else if (innerHtmlVerbs.indexOf('lblCharterBook') != -1) {

        AsyncGet_CP_SnippetData();
    }
    else if (innerHtmlVerbs.indexOf('lblVoyage') != -1) {

        AsyncGet_VoyageAlert();
    }
    else if (innerHtmlVerbs.indexOf('lblWebPartPerformanceManager') != -1) {
        asyncGet_Performance_Manager();
    }
    else if (innerHtmlVerbs.indexOf('lblWebPartWorklistIncident180Days') != -1) {
        asyncGet_getWorklist_Incident_180Days();
    }
    else if (innerHtmlVerbs.indexOf('lblWebPartWorklistNearmiss180Days') != -1) {
        asyncGet_getWorklist_Nearmiss_180Days();
    }

}
function SendMail(key) {
    if (key == 'lblWebPartCrewONBDList') {
        asyncMail_CrewONBDList();
    }
}

var lastExecutorMailCrewOnboardList = null;
function asyncMail_CrewONBDList() {

    var UserID = $('[id$=hdnUserID]').val();

    if (lastExecutorMailCrewOnboardList != null)
        lastExecutorMailCrewOnboardList.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../JibeWebService.asmx', 'Mail_CrewOnboardListRankWise', false, { "UserID": UserID }, onSucc_OpenMail, Onfail, null);
    lastExecutorMailCrewOnboardList = service.get_executor();
}
function onSucc_OpenMail(retval) {
    try {
        window.open("../Crew/EmailEditor.aspx?ID=" + retval, '_blank');
    }
    catch (ex)
    { }
}  