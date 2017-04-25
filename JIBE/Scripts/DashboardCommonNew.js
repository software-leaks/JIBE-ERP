
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
        var strContent = '';
        if (document.getElementById(prm[0]).outerHTML.toString().trim() != "")
            strContent = document.getElementById(prm[0]).outerHTML;
        $("#" + prm[1]).jqxWindow('setContent', strContent + retval);

        $("#" + prm[1] + " .jqx-window-content .SnippetsLoader").remove();

        checkForMyAction(prm[1], retval);

        var id = $("[id$=hdnSnippetId]").val();
        if (id != '0') {
            try {
                var innerHtmlVerbs = $('#' + id)[0].parentElement.parentElement.children['0'].lastElementChild.innerHTML;
                $("#dvContent").html(innerHtmlVerbs);
            }
            catch (ex)
        { }
        }
    }
    catch (ex)
    { }
}
function onSucc_PerformanceManLoadFunction(retval, prm) {
    try {


        document.getElementById("divPerManLastUpdated").innerHTML = retval[1];


        var strContent = '';
        if (document.getElementById(prm[0]).outerHTML.toString().trim() != "")
            strContent = document.getElementById(prm[0]).outerHTML + document.getElementById("divPerManLastUpdated").outerHTML;
        $("#" + prm[1]).jqxWindow('setContent', strContent + retval[0]);

        checkForMyAction(prm[1], retval[0]);

        $("#" + prm[1] + " .jqx-window-content .SnippetsLoader").remove();
    }
    catch (ex)
    { }
}
function onSuccCompletedInsp_LoadFunction(retval, prm) {
    try {
        var strContent = '';
        if (document.getElementById(prm[0]).outerHTML.toString().trim() != "")
            strContent = document.getElementById(prm[0]).outerHTML;
        $("#" + prm[1]).jqxWindow('setContent', strContent + retval);

        onLoad();
        checkForMyAction(prm[1], retval);

        $("#" + prm[1] + " .jqx-window-content .SnippetsLoader").remove();
    }
    catch (ex)
    { }
}

function onSuccOverdueFileSchedules_LoadFunction(retval, prm) {

    try {
        var strContent = '';
        if (document.getElementById(prm[0]).outerHTML.toString().trim() != "")
            strContent = document.getElementById(prm[0]).outerHTML;
        $("#" + prm[1]).jqxWindow('setContent', strContent + retval);


        onLoad();
        checkForMyAction(prm[1], retval);
        $("#" + prm[1] + " .jqx-window-content .SnippetsLoader").remove();
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

        var service = Sys.Net.WebServiceProxy.invoke('../JibeDashboardService.asmx', 'asyncGet_OverdueApproval_FileSchedules', false, { "UserID": UserID }, onSuccOverdueFileSchedules_LoadFunction, Onfail, new Array('dvWebPartOverdueFileScheduleApproval', 'lblWebPartOverdueFileScheduleApproval'));
        lastExecutorOverdueFileSchApp = service.get_executor();
    }
    catch (ex) {

        //alert(ex);
    }
}

var lastExecutorOverdueFileSchRec = null;
function asyncGetOverdueFileSchedulesReceiving() {

    try {
        var UserID = $('[id$=hdnUserID]').val();

        if (lastExecutorOverdueFileSchRec != null)
            lastExecutorOverdueFileSchRec.abort();

        var service = Sys.Net.WebServiceProxy.invoke('../JibeDashboardService.asmx', 'asyncGet_OverdueReceiving_FileSchedules', false, { "UserID": UserID }, onSuccOverdueFileSchedules_LoadFunction, Onfail, new Array('dvWebPartOverdueFileScheduleReceiving', 'lblWebPartOverdueFileScheduleReceiving'));
        lastExecutorOverdueFileSchRec = service.get_executor();
    }
    catch (ex) {

        //alert(ex);
    }
}
var lastExecutorIntv_By_UserID = null;
function load_PendingInterviewList_By_UserID() {

    var UserID = $('[id$=hdnUserID]').val();

    if (lastExecutorIntv_By_UserID != null)
        lastExecutorIntv_By_UserID.abort();

    var DateFormat = $('[id$=hdnDateFromatMasterPage]').val();
    var service = Sys.Net.WebServiceProxy.invoke('../JibeDashboardService.asmx', 'getPendingInterviewList_By_UserID', false, { "UserID": UserID, "DateFormat": DateFormat.toString() }, onSucc_LoadFunction, Onfail, new Array('dvInterviewSchedules_By_UserID', 'lblWebPartPendingInterviews_By_User'));
  
    lastExecutorIntv_By_UserID = service.get_executor();
}



var lastExecutorIntv_List = null;
function load_PendingInterviewList() {

    var UserID = $('[id$=hdnUserID]').val();

    if (lastExecutorIntv_List != null)
        lastExecutorIntv_List.abort();
    var DateFormat = $('[id$=hdnDateFromatMasterPage]').val();
    var service = Sys.Net.WebServiceProxy.invoke('../JibeDashboardService.asmx', 'getPendingInterviewList', false, { "UserID": UserID, "DateFormat": DateFormat.toString() }, onSucc_LoadFunction, Onfail, new Array('dvInterviewSchedules', 'lblWebPartAllPendingInterviews'));
    lastExecutorIntv_List = service.get_executor();
}

var lastExecutorPendingBriefing_List = null;
function load_PendingCrewBriefing() {

    var UserID = $('[id$=hdnUserID]').val();
    var DateFormat = $('[id$=hdnDateFromatMasterPage]').val();
    if (lastExecutorPendingBriefing_List != null) {
        lastExecutorPendingBriefing_List.abort();
    }
    var service = Sys.Net.WebServiceProxy.invoke('../JibeDashboardService.asmx', 'getPendingCrewBriefingList', false, { "UserID": UserID, "DateFormat": DateFormat.toString() }, onSucc_LoadFunction, Onfail, new Array('dvWebPartPendingCrewBriefing', 'lblWebPartPendingCrewBriefing'));
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
        var DateFormat = $('[id$=hdnDateFromatMasterPage]').val();
        var service = Sys.Net.WebServiceProxy.invoke('../JibeDashboardService.asmx', 'AsyncGet_CrewCpmplaints', false, { "User_ID": UserID, "DateFormat": DateFormat.toString() }, onSucc_LoadFunction, Onfail, new Array('dvCrewComplaints', 'lblWebPartCrewComplaints'));
        lastExecutorCrewCpmplaints = service.get_executor();
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

        var service = Sys.Net.WebServiceProxy.invoke('../JibeDashboardService.asmx', 'AsyncGet_ReqsnCount', false, { "User_ID": UserID, "CompanyID": CompanyID }, onSucc_LoadFunction, Onfail, new Array('dvReqsnCount', 'lblWepPartReqsnCount'));
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

        var service = Sys.Net.WebServiceProxy.invoke('../JibeDashboardService.asmx', 'AsyncGet_Requisition_Processing_Time', false, { "User_ID": UserID, "CompanyID": CompanyID }, onSucc_LoadFunction, Onfail, new Array('dvReqsnProcessing', 'lblWebPartReqsnProcessing'));
        lastExecutorRequisition_Processing_Time = service.get_executor();
    }
    catch (ex) { }

}
var lastExecutorCrewEvaluations = null;
function load_CrewEvaluations() {

    try {

        var UserID = $('[id$=hdnUserID]').val();

        if (lastExecutorCrewEvaluations != null)
            lastExecutorCrewEvaluations.abort();
        var DateFormat = $('[id$=hdnDateFromatMasterPage]').val();
        var service = Sys.Net.WebServiceProxy.invoke('../JibeDashboardService.asmx', 'AsyncGet_CrewEvaluationsList', false, { "User_ID": UserID, "DateFormat": DateFormat.toString() }, onSucc_LoadFunction, Onfail, new Array('dvEvaluations', 'lblWebPartEvaluations'));
        lastExecutorCrewEvaluations = service.get_executor();
    }
    catch (ex) { }

}
var lastExecutorCrewEvaluation_60Percent = null;
function load_CrewEvaluation_60Percent() {

    try {
        var UserID = $('[id$=hdnUserID]').val();

        if (lastExecutorCrewEvaluation_60Percent != null)
            lastExecutorCrewEvaluation_60Percent.abort();
        var DateFormat = $('[id$=hdnDateFromatMasterPage]').val();
        var service = Sys.Net.WebServiceProxy.invoke('../JibeDashboardService.asmx', 'AsyncGet_CrewEvaluation_60Percent', false, { "User_ID": UserID, "DateFormat": DateFormat.toString() }, onSucc_LoadFunction, Onfail, new Array('dvWebPartEvaluationBelow60', 'lblWebPartEvaluationBelow60'));
        lastExecutorCrewEvaluation_60Percent = service.get_executor();
    }
    catch (ex) { }

}
var lastExecutorDecklogAnomalies = null;
function load_DecklogAnomalies() {

    try {
        var UserID = $('[id$=hdnUserID]').val();

        if (lastExecutorDecklogAnomalies != null)
            lastExecutorDecklogAnomalies.abort();

        var service = Sys.Net.WebServiceProxy.invoke('../JibeDashboardService.asmx', 'AsyncGet_DecklogAnomalies', false, { "User_ID": UserID }, onSucc_LoadFunction, Onfail, new Array('dvWebPartDecklogAnomalies', 'lblWebPartDecklogAnomalies'));
        lastExecutorDecklogAnomalies = service.get_executor();
    }
    catch (ex) { }

}
var lastExecutorEnginelogAnomalies = null;
function load_EnginelogAnomalies() {

    try {
        var UserID = $('[id$=hdnUserID]').val();

        if (lastExecutorEnginelogAnomalies != null)
            lastExecutorEnginelogAnomalies.abort();

        var service = Sys.Net.WebServiceProxy.invoke('../JibeDashboardService.asmx', 'AsyncGet_EnginelogAnomalies', false, { "User_ID": UserID }, onSucc_LoadFunction, Onfail, new Array('dvWebPartEnginelogAnomalies', 'lblWebPartEnginelogAnomalies'));
        lastExecutorEnginelogAnomalies = service.get_executor();
    }
    catch (ex) { }

}
var lastExecutorCrewCardProposed = null;
function load_CrewCardProposed() {
    try {

        var UserID = $('[id$=hdnUserID]').val();

        if (lastExecutorCrewCardProposed != null)
            lastExecutorCrewCardProposed.abort();
        var DateFormat = $('[id$=hdnDateFromatMasterPage]').val();
        var service = Sys.Net.WebServiceProxy.invoke('../JibeDashboardService.asmx', 'AsyncGet_CrewCardProposed', false, { "User_ID": UserID, "DateFormat": DateFormat.toString() }, onSucc_LoadFunction, Onfail, new Array('dvCrewCardProposed', 'lblWebPartCrewCardProposed'));
        lastExecutorCrewCardProposed = service.get_executor();
    }
    catch (ex) { }

}
var lastExecutorPortCallsVessel = null;
function load_PortCallsVessel() {
    try {
        var UserID = $('[id$=hdnUserID]').val();
        if (lastExecutorPortCallsVessel != null)
            lastExecutorPortCallsVessel.abort();

        var service = Sys.Net.WebServiceProxy.invoke('../JibeDashboardService.asmx', 'AsyncGet_PortCallsVessel', false, { "User_ID": UserID }, onSuccessasyncBindPortCallsVessel, Onfail, null);
        lastExecutorPortCallsVessel = service.get_executor();
    }
    catch (ex) { }

}
function onSuccessasyncBindPortCallsVessel(retVal, ev) {

    // __isResponse = 1;
    drawChartPortCallsVessel(retVal);
}
function drawChartPortCallsVessel(dataValues) {
    //  __isResponse = 1;
    try {
        var settings = {
            title: "Port Call Per Vessel",
            description: "Port calls per Vessel in last 60 days",
            showLegend: true,
            padding: {
                left: 5,
                top: 5,
                right: 5,
                bottom: 5
            },
            titlePadding: {
                left: 0,
                top: 0,
                right: 0,
                bottom: 10
            },
            source: dataValues,
            enableAnimations: true,
            xAxis:
            {
                dataField: 'VesselCode',
                displayText: 'Vessel',
                showGridLines: true

            },
            seriesGroups: [{
                type: 'column',
                valueAxis: {
                    description: 'Port Call Count',
                    gridLinesInterval: 1,
                    visible: true,
                    minValue: 0,
                    horizontalTextAlignment: 'right'
                },
                series: [{
                    dataField: 'PortCount',
                    displayText: 'Vessel',
                    toolTipFormatFunction: function (value, itemIndex, serie, group, categoryValue, categoryAxis) {
                        return '<DIV style="text-align:left";><b>Vessel Name: </b>' + categoryValue
                                                 + '<br /><b>Port Call Count:</b> ' + value + ' </DIV>'
                    },
                    labels: {
                        visible: true,
                        verticalAlignment: 'top',
                        offset: { x: 0, y: -20 }
                    }
                }]
            }]
        };
        $('#dvWebPartPortCallsVessel').jqxChart(settings);
    }
    catch (ex) { }
}
var lastExecutorPortCallsMonth = null;
function load_PortCallsMonth() {
    try {
        var UserID = $('[id$=hdnUserID]').val();
        if (lastExecutorPortCallsMonth != null)
            lastExecutorPortCallsMonth.abort();

        var service = Sys.Net.WebServiceProxy.invoke('../JibeDashboardService.asmx', 'AsyncGet_PortCallsMonth', false, { "User_ID": UserID }, onSuccessasyncBindPortCallsMonth, Onfail, null);
        lastExecutorPortCallsMonth = service.get_executor();
    }
    catch (ex) { }

}
function onSuccessasyncBindPortCallsMonth(retVal, ev) {

    //  __isResponse = 1;
    drawChartPortCallsMonth(retVal);
}
function drawChartPortCallsMonth(dataValues) {
    try {
        var settings = {
            title: "Port Call Per Month",
            description: "Port calls per Month in last 12 Months",
            showLegend: true,
            padding: {
                left: 5,
                top: 5,
                right: 5,
                bottom: 5
            },
            titlePadding: {
                left: 0,
                top: 0,
                right: 0,
                bottom: 10
            },
            source: dataValues,
            enableAnimations: true,
            xAxis:
            {
                dataField: 'header',
                displayText: 'Month-Year/Vessel Count',
                showGridLines: true,
                textRotationAngle: -25
            },
            seriesGroups: [{
                type: 'column',
                valueAxis: {
                    description: 'Port Call Count',
                    gridLinesInterval: 1,
                    visible: true,
                    minValue: 0,
//                    formatSettings: {
//                        decimalPlaces: 0
//                    },

                    horizontalTextAlignment: 'right'
                },
                series: [{
                    dataField: 'PortCount',
                    displayText: 'Month-Year/Vessel Count',
                    toolTipFormatFunction: function (value, itemIndex, serie, group, categoryValue, categoryAxis) {
                        return '<DIV style="text-align:left";><b>Month-Year/Vessel Count: </b>' + categoryValue
                                                 + '<br /><b>Port Call Count:</b> ' + value + ' </DIV>'
                    },
                    labels: {
                        visible: true,
                        verticalAlignment: 'top',
                        offset: { x: 0, y: -20 }
                    }

                }]
            }]
        };
        $('#dvWebPartPortcallsMonth').jqxChart(settings);
    }
    catch (ex) { }
}
var lastExecutorshowEscalationLog = null;
function showEscalationLog(evt, wlid, vid, userid) {

    var oMe = evt.target || window.event.srcElement;
    var check = oMe.src.toLowerCase();
  
    if (check.lastIndexOf("plus.png") != -1) {
        oMe.src = "../images/Minus.png"
        $('.' + wlid).show();
        var DepID;
        var Assignor = null;

        DepID = $('[id$=hdfUserdepartmentid]').val();
        if (lastExecutorshowEscalationLog != null)
            lastExecutorshowEscalationLog.abort();
        var DateFormat = $('[id$=hdnDateFromatMasterPage]').val();
        var service = Sys.Net.WebServiceProxy.invoke('../JibeDashboardService.asmx', 'asyncGet_EscalationLog', false, { "wlid": wlid, "vid": vid, "userid": userid, "DateFormat": DateFormat.toString() }, onSucc_showEscalationLog, Onfail, new Array('dvLog' + wlid));
        lastExecutorshowEscalationLog = service.get_executor();
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
    //alert(errmsg);
}


var lastExecutorPendingNCRBYDept = null;
function asyncGetPendingNCRBYDept() {
    var DepID;
    var Assignor = null;

    DepID = $('[id$=hdnDept]').val(); //To fetch logged in user's department.
    if (lastExecutorPendingNCRBYDept != null)
        lastExecutorPendingNCRBYDept.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../JibeDashboardService.asmx', 'asyncGet_Pending_NCR', false, { "Assignor": Assignor, "DepartmentID": DepID }, onSucc_LoadFunction, Onfail, new Array('dvPendingNCRByDept', 'lblWebPartPendingNCRBYDept'));
    lastExecutorPendingNCRBYDept = service.get_executor();
}


var lastExecutorPendingNCRALLDept = null;
function asyncGetPendingNCRALLDept() {
    var Assignor = null;

    if (lastExecutorPendingNCRALLDept != null)
        lastExecutorPendingNCRALLDept.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../JibeDashboardService.asmx', 'asyncGet_Pending_NCR_ALL_Dept', false, { "Assignor": Assignor }, onSucc_LoadFunction, Onfail, new Array('dvPendingNCRALLDept', 'lblWebPartPendingNCRALLDept'));
    lastExecutorPendingNCRALLDept = service.get_executor();
}


var lastExecutorAss = null;

function asyncGetPendingNCRByAssignor() {
    var DepID = null;
    var Assignor = 6;

    DepID = $('[id$=hdnDept]').val(); ///To fetch logged in user's department.

    if (lastExecutorAss != null)
        lastExecutorAss.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../JibeDashboardService.asmx', 'asyncGet_Pending_NCR', false, { "Assignor": Assignor, "DepartmentID": DepID }, onSucc_LoadFunction, Onfail, new Array('dvPendingNCRByAssignor', 'lblWebPartPendingNCRByAssignor'));
    lastExecutorAss = service.get_executor();
}


var lastExecutor_TravelPO = null;

function asyncGetPending_TravelPO() {

    var UserID = $('[id$=hdnUserID]').val();

    if (lastExecutor_TravelPO != null)
        lastExecutor_TravelPO.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../JibeDashboardService.asmx', 'asyncGet_Pending_Travel_PO', false, { "User_ID": UserID }, onSucc_LoadFunction, Onfail, new Array('dvPending_TravelPO', 'lblWebPartPendingTravelPO'));
    lastExecutor_TravelPO = service.get_executor();
}

var lastExecutor_Events_Done = null;

function asyncGet_Events_Done() {

    var UserID = $('[id$=hdnUserID]').val();

    if (lastExecutor_Events_Done != null)
        lastExecutor_Events_Done.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../JibeDashboardService.asmx', 'asyncGet_Events_Done', false, { "User_ID": UserID }, onSucc_LoadFunction, Onfail, new Array('dvEvents_Done', 'lblWebPartEvents_Done'));
    lastExecutor_Events_Done = service.get_executor();
}

var lastExecutor_LogisticPO = null;

function asyncGetPending_LogisticPO() {

    var UserID = $('[id$=hdnUserID]').val();

    if (lastExecutor_LogisticPO != null)
        lastExecutor_LogisticPO.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../JibeDashboardService.asmx', 'asyncGet_Pending_Logistic_PO', false, { "User_ID": UserID }, onSucc_LoadFunction, Onfail, new Array('dvPending_LogisticPO', 'lblWebPartPendingLogisticPO'));
    lastExecutor_LogisticPO = service.get_executor();
}


var lastExecutor_ReqsnPO = null;

function asyncGetPending_ReqsnPO() {

    var UserID = $('[id$=hdnUserID]').val();

    if (lastExecutor_ReqsnPO != null)
        lastExecutor_ReqsnPO.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../JibeDashboardService.asmx', 'asyncGet_Pending_Reqsn_PO', false, { "User_ID": UserID }, onSucc_LoadFunction, Onfail, new Array('dvPending_ReqsnPO', 'lblWebPartPendingForApproval'));
    lastExecutor_ReqsnPO = service.get_executor();
}


var lastExecutor_Provision_Last_Supplied = null;

function asyncGet_Provision_Last_Supplied() {


    if (lastExecutor_Provision_Last_Supplied != null)
        lastExecutor_Provision_Last_Supplied.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../JibeDashboardService.asmx', 'asyncGet_Provision_Last_Supplied', false, {}, onSucc_LoadFunction, Onfail, new Array('dvProvisionLastSupplied', 'lblWebPartSuppliedPROV'));
    lastExecutor_Provision_Last_Supplied = service.get_executor();
}


var lastExecutor_MyShortsCuts_Menu = null;

function asyncGet_MyShortsCuts_Menu() {
    var UserID = $('[id$=hdnUserID]').val();

    if (lastExecutor_MyShortsCuts_Menu != null)
        lastExecutor_MyShortsCuts_Menu.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../JibeDashboardService.asmx', 'asyncGet_User_Menu_Favourite', false, { 'UserID': UserID }, onSucc_LoadFunction, Onfail, new Array('dvMyShortsCuts_Menu', 'lblWebPartMyShortcuts'));
    lastExecutor_MyShortsCuts_Menu = service.get_executor();
}


var lastExecutor_WorkList = null;

function asyncGet_PendingWorkList() {
    var UserID = $('[id$=hdnUserID]').val();

    if (lastExecutor_WorkList != null)
        lastExecutor_WorkList.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../JibeDashboardService.asmx', 'asyncGet_PendingWorkList', false, { 'UserID': UserID }, onSucc_LoadFunction, Onfail, new Array('dvMyWork_List', 'lblWebpartPendingWorkList'));
    lastExecutor_WorkList = service.get_executor();
}


var lastExecutor_PendingWorkListVerification = null;

function asyncGet_PendingWorkListVerification() {
    var UserID = $('[id$=hdnUserID]').val();

    if (lastExecutor_PendingWorkListVerification != null)
        lastExecutor_PendingWorkListVerification.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../JibeDashboardService.asmx', 'asyncGet_PendingWorkListVerification', false, {}, onSucc_LoadFunction, Onfail, new Array('dvPending_WorkList_Verification', 'lblWebPartPendingWorkListVerification'));
    lastExecutor_PendingWorkListVerification = service.get_executor();
}


var lastExecutor_getWorklist_DueIn_7Days = null;

function asyncGet_getWorklist_DueIn_7Days() {
    var UserID = $('[id$=hdnUserID]').val();

    if (lastExecutor_getWorklist_DueIn_7Days != null)
        lastExecutor_getWorklist_DueIn_7Days.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../JibeDashboardService.asmx', 'getWorklist_DueIn_7Days', false, {}, onSucc_LoadFunction, Onfail, new Array('dvWebPartWorkListDue7Days', 'lblWebPartWorkListDue7Days'));
    lastExecutor_getWorklist_DueIn_7Days = service.get_executor();
}


var lastExecutor_CTMApproval = null;

function asyncGetPending_CTMApproval() {

    var UserID = $('[id$=hdnUserID]').val();

    if (lastExecutor_CTMApproval != null)
        lastExecutor_CTMApproval.abort();
    var DateFormat = $('[id$=hdnDateFromatMasterPage]').val();
    var service = Sys.Net.WebServiceProxy.invoke('../JibeDashboardService.asmx', 'asyncGet_Pending_CTM_Approval', false, { "User_ID": UserID, "DateFormat": DateFormat.toString() }, onSucc_LoadFunction, Onfail, new Array('dvWebpartPendingCTMApproval', 'lblWebPartPendingCTMApproval'));
    lastExecutor_CTMApproval = service.get_executor();
}


var lastExecutor_CTMConfirmation = null;

function asyncGet_CTM_Confirmation_Not_Received() {
    var UserID = $('[id$=hdnUserID]').val();

    if (lastExecutor_CTMConfirmation != null)
        lastExecutor_CTMConfirmation.abort();
    var DateFormat = $('[id$=hdnDateFromatMasterPage]').val();
    var service = Sys.Net.WebServiceProxy.invoke('../JibeDashboardService.asmx', 'asyncGet_CTM_Confirmation_Not_Received', false, { "User_ID": UserID, "DateFormat": DateFormat.toString() }, onSucc_LoadFunction, Onfail, new Array('dvWebPartCTMConfirmationFromVessel', 'lblWebPartCTMConfirmationFromVessel'));
    lastExecutor_CTMConfirmation = service.get_executor();
}


var lastExecutor_CylinderOilConsumption = null;

function asyncCylinder_Oil_Consumption() {

    var UserID = $('[id$=hdnUserID]').val();

    if (lastExecutor_CylinderOilConsumption != null)
        lastExecutor_CylinderOilConsumption.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../JibeDashboardService.asmx', 'asyncGet_Cylinder_Oil_Consumption', false, { "UserID": UserID }, onSucc_LoadFunction, Onfail, new Array('dvWebPartCylinderOilConsumption', 'lblWebPartCylinderOilConsumption'));
    lastExecutor_CylinderOilConsumption = service.get_executor();
}

var lastExecutorInspect_OverDue = null;
function asyncOverDue_Inspection() {

    var UserID = $('[id$=hdnUserID]').val();

    if (lastExecutorInspect_OverDue != null)
        lastExecutorInspect_OverDue.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../JibeDashboardService.asmx', 'asyncGet_OverDue_Inspection', false, { "UserID": UserID }, onSucc_LoadFunction, Onfail, new Array('dvWebPartInspectionOverdue', 'lblWebPartInspectionOverdue'));
    lastExecutorInspect_OverDue = service.get_executor();
}

var lastExecutorInspect_Due = null;
function asyncDueInMonth_Inspection() {

    var UserID = $('[id$=hdnUserID]').val();

    if (lastExecutorInspect_Due != null)
        lastExecutorInspect_Due.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../JibeDashboardService.asmx', 'asyncGet_DueInMonth_Inspection', false, { "UserID": UserID }, onSucc_LoadFunction, Onfail, new Array('dvWebPartInspectionDueInMonth', 'lblWebPartInspectionDueInMonth'));
    lastExecutorInspect_Due = service.get_executor();
}

var lastExecutorInspect_Completed = null;
function asyncCompleted_Inspection() {

    var UserID = $('[id$=hdnUserID]').val();

    if (lastExecutorInspect_Completed != null)
        lastExecutorInspect_Completed.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../JibeDashboardService.asmx', 'asyncGet_Completed_Inspection', false, { "UserID": UserID }, onSuccCompletedInsp_LoadFunction, Onfail, new Array('dvWebPartInspectionCompleted', 'lblWebPartInspectionCompleted'));
    lastExecutorInspect_Completed = service.get_executor();
}
var lastExecutorEvaluation_Feedback = null;
function asyncEvaluation_Feedback() {

    var UserID = $('[id$=hdnUserID]').val();

    if (lastExecutorEvaluation_Feedback != null)
        lastExecutorEvaluation_Feedback.abort();
    var DateFormat = $('[id$=hdnDateFromatMasterPage]').val();
    var service = Sys.Net.WebServiceProxy.invoke('../JibeDashboardService.asmx', 'asyncGet_CrewEvaluation_Feedback', false, { "UserID": UserID, "DateFormat": DateFormat.toString() }, onSucc_LoadFunction, Onfail, new Array('dvWebPartCrewEvaluationFeedback', 'lblWebPartCrewEvaluationFeedback'));
    lastExecutorEvaluation_Feedback = service.get_executor();
}
//-------Purchase Inventory Items Below Treshold ----------------//
var lastExecutorInvItems_BelowTreshold = null;
function asyncInvItems_BelowTreshold() {

    var UserID = $('[id$=hdnUserID]').val();

    if (lastExecutorInvItems_BelowTreshold != null)
        lastExecutorInvItems_BelowTreshold.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../JibeDashboardService.asmx', 'asyncGet_InvItems_BelowTreshold', false, { "UserID": UserID }, onSucc_LoadFunction, Onfail, new Array('dvWebPartBelowTresholdInventoryItems', 'lblWebPartBelowTresholdInventoryItems'));
    lastExecutorInvItems_BelowTreshold = service.get_executor();
}
//-------Operations My Worklist ----------------//
var lastExecutor_asyncGet_MyOperationWorklist = null;
function asyncGet_MyOperationWorklist() {
    var UserID = $('[id$=hdnUserID]').val();
    //alert();
    if (lastExecutor_asyncGet_MyOperationWorklist != null)
        lastExecutor_asyncGet_MyOperationWorklist.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../JibeDashboardService.asmx', 'Get_MyOperationWorklist', false, { 'UserID': UserID }, onSucc_LoadFunction, Onfail, new Array('dvMyOperationWorklist', 'lblWebPartMyOperationWorklist'));
    lastExecutor_asyncGet_MyOperationWorklist = service.get_executor();
}


//-------Operation Worklist DueIn7 Days ----------------//
var lastExecutor_asyncGet_OpsWorklistDueIn7Days = null;
function asyncGet_OpsWorklistDueIn7Days() {
    var UserID = $('[id$=hdnUserID]').val();
    //alert();
    if (lastExecutor_asyncGet_OpsWorklistDueIn7Days != null)
        lastExecutor_asyncGet_OpsWorklistDueIn7Days.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../JibeDashboardService.asmx', 'Get_OpsWorklistDueIn7Days', false, { 'UserID': UserID }, onSucc_LoadFunction, Onfail, new Array('dvWebPartOpsWorklistDueIn7Days', 'lblWebPartOpsWorklistDueIn7Days'));
    lastExecutor_asyncGet_OpsWorklistDueIn7Days = service.get_executor();
}


//--asyncGet_OpsWorklistOverdue--
var lastExecutor_asyncGet_OpsWorklistOverdue = null;
function asyncGet_OpsWorklistOverdue() {
    var UserID = $('[id$=hdnUserID]').val();
    //alert();
    if (lastExecutor_asyncGet_OpsWorklistOverdue != null)
        lastExecutor_asyncGet_OpsWorklistOverdue.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../JibeDashboardService.asmx', 'Get_OpsWorklistOverdue', false, { 'UserID': UserID }, onSucc_LoadFunction, Onfail, new Array('dvWebPartOpsWorklistOverdue', 'lblWebPartOpsWorklistOverdue'));
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

    var service = Sys.Net.WebServiceProxy.invoke('../JibeDashboardService.asmx', 'Get_PMS_Overdue_Job', false, { 'User_ID': UserID }, onSucc_LoadFunction, Onfail, new Array('dvWebPartPMSOverdueJobs', 'lblWebPartPMSOverdueJobs'));
    lastExecutor_asyncGet_PMSOverdueJobs = service.get_executor();
}


// -- survey snippets -- //
//1//
var lastExecutor_asyncGet_Surv_DueinNext30Days = null;
function asyncGet_Surv_DueinNext30Days() {
    var UserID = $('[id$=hdnUserID]').val();
    var DateFormat = $('[id$=hdnDateFromatMasterPage]').val();
    if (lastExecutor_asyncGet_Surv_DueinNext30Days != null)
        lastExecutor_asyncGet_Surv_DueinNext30Days.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../JibeDashboardService.asmx', 'Get_Surv_DueinNext30Days', false, { 'UserID': UserID, "DateFormat": DateFormat.toString() }, onSucc_LoadFunction, Onfail, new Array('dvWebSurvDueinNext30Days', 'lblWebPartSurvDueinNext30Days'));
    lastExecutor_asyncGet_Surv_DueinNext30Days = service.get_executor();
}


//2//
var lastExecutor_asyncGet_Surv_DueinNext7DaysAndOverdue = null;
function asyncGet_Surv_DueinNext7DaysAndOverdue() {
    var UserID = $('[id$=hdnUserID]').val();
    var DateFormat = $('[id$=hdnDateFromatMasterPage]').val();
    if (lastExecutor_asyncGet_Surv_DueinNext7DaysAndOverdue != null)
        lastExecutor_asyncGet_Surv_DueinNext7DaysAndOverdue.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../JibeDashboardService.asmx', 'Get_Surv_DueinNext7DaysAndOverdue', false, { 'UserID': UserID, "DateFormat": DateFormat.toString() }, onSucc_LoadFunction, Onfail, new Array('dvSurvDueinNext7DaysAndOverdue', 'lblWebPartSurvDueinNext7DaysAndOverdue'));
    lastExecutor_asyncGet_Surv_DueinNext7DaysAndOverdue = service.get_executor();
}


//3//
var lastExecutor_asyncGet_Surv_PendingVerification = null;
function asyncGet_Surv_PendingVerification() {
    var UserID = $('[id$=hdnUserID]').val();
    var DateFormat = $('[id$=hdnDateFromatMasterPage]').val();
    if (lastExecutor_asyncGet_Surv_PendingVerification != null)
        lastExecutor_asyncGet_Surv_PendingVerification.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../JibeDashboardService.asmx', 'Get_Surv_PendingVerification', false, { 'UserID': UserID, "DateFormat": DateFormat.toString() }, onSucc_LoadFunction, Onfail, new Array('dvSurvPendingVerification', 'lblWebPartSurvPendingVerification'));
    lastExecutor_asyncGet_Surv_PendingVerification = service.get_executor();
}


//4//
var lastExecutor_asyncGet_Surv_NA_PendingVerification = null;
function asyncGet_Surv_NA_PendingVerification() {
    var UserID = $('[id$=hdnUserID]').val();
    var DateFormat = $('[id$=hdnDateFromatMasterPage]').val();
    if (lastExecutor_asyncGet_Surv_NA_PendingVerification != null)
        lastExecutor_asyncGet_Surv_NA_PendingVerification.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../JibeDashboardService.asmx', 'Get_Surv_NA_PendingVerification', false, { 'UserID': UserID, "DateFormat": DateFormat.toString() }, onSucc_LoadFunction, Onfail, new Array('dvSurvNAPendingVerification', 'lblWebPartSurvNAPendingVerification'));
    lastExecutor_asyncGet_Surv_NA_PendingVerification = service.get_executor();
}
//5// by vasu
var lastExecutor_asyncGet_Surv_ExpDateBydayCountFor90 = null;
function asyncGet_Surv_ExpDateBydayCountFor90() {
    var UserID = $('[id$=hdnUserID]').val();
    var DateFormat = $('[id$=hdnDateFromatMasterPage]').val();
    var ExpairyFromDays = $('[id$=hdnFromDays31]').val();
    var ExpairyToDays = $('[id$=hdnToDays90]').val();
    if (lastExecutor_asyncGet_Surv_ExpDateBydayCountFor90 != null)
        lastExecutor_asyncGet_Surv_ExpDateBydayCountFor90.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../JibeDashboardService.asmx', 'Get_Surv_ExpDateBydayCount', false, { 'UserID': UserID, 'ExpairyFromDaysCount': ExpairyFromDays, 'ExpairyToDaysCount': ExpairyToDays, "DateFormat": DateFormat.toString() }, onSucc_LoadFunction, Onfail, new Array('dvSurvExpiryin31to90days', 'lblWebPartSurvExpiryin31to90days'));
    lastExecutor_asyncGet_Surv_ExpDateBydayCountFor90 = service.get_executor();
}
//6// by vasu
var lastExecutor_asyncGet_Surv_ExpDateBydayCountFor30 = null;
function asyncGet_Surv_ExpDateBydayCountFor30() {
    var UserID = $('[id$=hdnUserID]').val();
    var ExpairyFromDays = $('[id$=hdnFromDays8]').val();
    var ExpairyToDays = $('[id$=hdnToDays30]').val();
    var DateFormat = $('[id$=hdnDateFromatMasterPage]').val();
    if (lastExecutor_asyncGet_Surv_ExpDateBydayCountFor30 != null)
        lastExecutor_asyncGet_Surv_ExpDateBydayCountFor30.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../JibeDashboardService.asmx', 'Get_Surv_ExpDateBydayCount', false, { 'UserID': UserID, 'ExpairyFromDaysCount': ExpairyFromDays, 'ExpairyToDaysCount': ExpairyToDays, "DateFormat": DateFormat.toString() }, onSucc_LoadFunction, Onfail, new Array('dvSurvExpiryin7to30days', 'lblWebPartSurvExpiryin7to30days'));
    lastExecutor_asyncGet_Surv_ExpDateBydayCountFor30 = service.get_executor();
}
//7// by vasu
var lastExecutor_asyncGet_Surv_ExpDateBydayCountFor7 = null;
function asyncGet_Surv_ExpDateBydayCountFor7() {
    var UserID = $('[id$=hdnUserID]').val();
    var ExpairyFromDays = $('[id$=hdnFromDays0]').val();
    var ExpairyToDays = $('[id$=hdnToDays7]').val();
    var DateFormat = $('[id$=hdnDateFromatMasterPage]').val();
    if (lastExecutor_asyncGet_Surv_ExpDateBydayCountFor7 != null)
        lastExecutor_asyncGet_Surv_ExpDateBydayCountFor7.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../JibeDashboardService.asmx', 'Get_Surv_ExpDateBydayCount', false, { 'UserID': UserID, 'ExpairyFromDaysCount': ExpairyFromDays, 'ExpairyToDaysCount': ExpairyToDays, "DateFormat": DateFormat.toString() }, onSucc_LoadFunction, Onfail, new Array('divSurvExpiryinLessThen7days', 'lblWebPartSurvExpiryinLessThen7days'));
    lastExecutor_asyncGet_Surv_ExpDateBydayCountFor7 = service.get_executor();
}

var lastExecutor_asyncGetPendingCardApprovalList = null;
function asyncGetPendingCardApprovalList() {


    var UserID = $('[id$=hdnUserID]').val();

    if (lastExecutor_asyncGetPendingCardApprovalList != null)
        lastExecutor_asyncGetPendingCardApprovalList.abort();
    var DateFormat = $('[id$=hdnDateFromatMasterPage]').val();
    var service = Sys.Net.WebServiceProxy.invoke('../JibeDashboardService.asmx', 'GetPendingCardApprovalList', false, { "UserID": UserID, "DateFormat": DateFormat.toString() }, onSucc_LoadFunction, Onfail, new Array('dvWebPartPendingCardApproval', 'lblWebPartPendingCardApproval'));
    lastExecutor_asyncGetPendingCardApprovalList = service.get_executor();
}



var lastExecutor_asyncGet_PendingSupplierApprovalList = null;
function asyncGet_PendingSupplierApprovalList() {

    var UserID = $('[id$=hdnUserID]').val();

    if (lastExecutor_asyncGet_PendingSupplierApprovalList != null)
        lastExecutor_asyncGet_PendingSupplierApprovalList.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../JibeDashboardService.asmx', 'GetPendingSupplierApprovalList', false, { "UserID": UserID }, onSucc_LoadFunction, Onfail, new Array('dvWebPendingASLEvaluation', 'lblWebPendingASLEvaluation'));
    lastExecutor_asyncGet_PendingSupplierApprovalList = service.get_executor();
}

var lastExecutor_asyncGet_PendingInvoiceApproval = null;
function asyncGet_PendingInvoiceApproval() {

    var UserID = $('[id$=hdnUserID]').val();

    if (lastExecutor_asyncGet_PendingInvoiceApproval != null)
        lastExecutor_asyncGet_PendingInvoiceApproval.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../JibeDashboardService.asmx', 'GetPendingInvoiceApproval', false, { "UserID": UserID }, onSucc_LoadFunction, Onfail, new Array('dvWebPendingInvoiceApproval', 'lblWebPendingInvoiceApproval'));
    lastExecutor_asyncGet_PendingInvoiceApproval = service.get_executor();
}

var lastExecutor_asyncGet_OpexVesselReport = null;
function asyncGet_OpexVesselReport() {

    var UserID = $('[id$=hdnUserID]').val();

    if (lastExecutor_asyncGet_OpexVesselReport != null)
        lastExecutor_asyncGet_OpexVesselReport.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../JibeDashboardService.asmx', 'Get_Opex_Vessel_Report', false, { "UserID": UserID }, onSucc_LoadFunction, Onfail, new Array('dvWebOpexVesselReport', 'lblWebOpexVesselReport'));
    lastExecutor_asyncGet_OpexVesselReport = service.get_executor();
}

var lastExecutor_AsyncGet_VoyageAlert = null;
function AsyncGet_VoyageAlert() {

    var UserID = $('[id$=hdnUserID]').val();

    if (lastExecutor_AsyncGet_VoyageAlert != null)
        lastExecutor_AsyncGet_VoyageAlert.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../JibeDashboardService.asmx', 'AsyncGet_VoyageAlert', false, { "UserID": UserID }, onSucc_LoadFunction, Onfail, new Array('divVoyage', 'lblVoyage'));
    lastExecutor_AsyncGet_VoyageAlert = service.get_executor();
}


var lastExecutor_AsyncGet_CP_SnippetData = null;
function AsyncGet_CP_SnippetData() {

    var UserID = $('[id$=hdnUserID]').val();

    if (lastExecutor_AsyncGet_CP_SnippetData != null)
        lastExecutor_AsyncGet_CP_SnippetData.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../JibeDashboardService.asmx', 'Get_CP_SnippetData', false, { "UserID": UserID }, onSucc_LoadFunction, Onfail, new Array('dvCharterBook', 'lblCharterBook'));
    lastExecutor_AsyncGet_CP_SnippetData = service.get_executor();
}


var lastExecutor_asyncGet_CrewListByPerformance = null;
function asyncGet_CrewListByPerformance() {
    var UserID = $('[id$=hdnUserID]').val();
    if (lastExecutor_asyncGet_CrewListByPerformance != null)
        lastExecutor_asyncGet_CrewListByPerformance.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../JibeDashboardService.asmx', 'Get_CrewListByPerformance', false, { 'UserID': UserID }, onSucc_LoadFunction, Onfail, new Array('dvWebPartCrewPerformance', 'lblWebPartCrewPerformance'));
    lastExecutor_asyncGet_CrewListByPerformance = service.get_executor();
}

var lastExecutor_asyncGet_CrewEvaluationDueList = null;
function asyncGet_CrewEvaluationDueList() {
    var UserID = $('[id$=hdnUserID]').val();
    if (lastExecutor_asyncGet_CrewEvaluationDueList != null)
        lastExecutor_asyncGet_CrewEvaluationDueList.abort();

    var DateFormat = $('[id$=hdnDateFromatMasterPage]').val();
    var service = Sys.Net.WebServiceProxy.invoke('../JibeDashboardService.asmx', 'Get_CrewEvaluationDueList', false, { 'UserID': UserID, "DateFormat": DateFormat.toString() }, onSucc_LoadFunction, Onfail, new Array('dvWebPartCrewEvaluationDue', 'lblWebPartEvaluationDue'));
    lastExecutor_asyncGet_CrewEvaluationDueList = service.get_executor();
}

var lastExecutor_asyncGet_CrewListByPerformanceVerification = null;
function asyncGet_CrewListByPerformanceVerification() {
    var UserID = $('[id$=hdnUserID]').val();
    if (lastExecutor_asyncGet_CrewListByPerformanceVerification != null)
        lastExecutor_asyncGet_CrewListByPerformanceVerification.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../JibeDashboardService.asmx', 'Get_CrewListByPerformanceVerification', false, { 'UserID': UserID }, onSucc_LoadFunction, Onfail, new Array('dvWebPartCrewPerformanceVerification', 'lblWebPartCrewPerformanceVerification'));
    lastExecutor_asyncGet_CrewListByPerformanceVerification = service.get_executor();
}

var lastExecutorCrewOnboardList = null;
function asyncGet_CrewONBDList() {

    var UserID = $('[id$=hdnUserID]').val();

    if (lastExecutorCrewOnboardList != null)
        lastExecutorCrewOnboardList.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../JibeDashboardService.asmx', 'Get_CrewOnboardListRankWise', false, { "UserID": UserID }, onSucc_LoadFunction, Onfail, new Array('dvWebPartCrewONBDList', 'lblWebPartCrewONBDList'));
    lastExecutorCrewOnboardList = service.get_executor();
}

var lastExecutorCrewSeniorityReward = null;
function asyncGet_CrewSeniorityReward() {
    var UserID = $('[id$=hdnUserID]').val();

    if (lastExecutorCrewSeniorityReward != null)
        lastExecutorCrewSeniorityReward.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../JibeDashboardService.asmx', 'Get_CrewSeniorityReward', false, { "UserID": UserID }, onSucc_LoadFunction, Onfail, new Array('dvWebPartCrewSeniorityReward', 'lblWebPartCrewSeniorityReward'));
    lastExecutorCrewSeniorityReward = service.get_executor();
}
var lastExecutorPerformance_Manager = null;
function asyncGet_Performance_Manager() {

    var UserID = $('[id$=hdnUserID]').val();
    var Days = $('[id$=hdnDays]').val();
    if (lastExecutorPerformance_Manager != null)
        lastExecutorPerformance_Manager.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../JibeDashboardService.asmx', 'Get_Performance_Manager', false, { "UserID": UserID, "Days": Days }, onSucc_PerformanceManLoadFunction, Onfail, new Array('dvWebPartPerformanceManager', 'lblWebPartPerformanceManager'));
    lastExecutorPerformance_Manager = service.get_executor();
}
var lastExecutorWorklistIncident180Days = null;
function asyncGet_getWorklist_Incident_180Days() {

    var UserID = $('[id$=hdnUserID]').val();
    var Days = $('[id$=hdnDays]').val();
    if (lastExecutorWorklistIncident180Days != null)
        lastExecutorWorklistIncident180Days.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../JibeDashboardService.asmx', 'getWorklist_Incident_180days', false, {}, onSucc_LoadFunction, Onfail, new Array('dvWebPartWorklistIncident180Days', 'lblWebPartWorklistIncident180Days'));
    lastExecutorWorklistIncident180Days = service.get_executor();
}
var lastExecutorWorklistNearmiss180Days = null;
function asyncGet_getWorklist_Nearmiss_180Days() {

    var UserID = $('[id$=hdnUserID]').val();
    var Days = $('[id$=hdnDays]').val();
    if (lastExecutorWorklistNearmiss180Days != null)
        lastExecutorWorklistNearmiss180Days.abort();
   
    var service = Sys.Net.WebServiceProxy.invoke('../JibeDashboardService.asmx', 'getWorklist_NearMiss_180days', false, {}, onSucc_LoadFunction, Onfail, new Array('dvWebPartWorklistNearmiss180Days', 'lblWebPartWorklistNearmiss180Days'));
    lastExecutorWorklistNearmiss180Days = service.get_executor();
}


//Vetting snnipet //
var lastVettingExpInNext30Days = null;
function Get_Vetting_Exp_In_Next_30Days() {
    
    var UserID = $('[id$=hdnUserID]').val();
    var DateFormat = $('[id$=hdnDateFromatMasterPage]').val();
    if (lastVettingExpInNext30Days != null)
        lastVettingExpInNext30Days.abort();

    
    var service = Sys.Net.WebServiceProxy.invoke('../JibeDashboardService.asmx', 'Get_Vetting_Exp_In_Next_30Days', false, { "UserID": UserID, "DateFormat": DateFormat}, onSucc_LoadFunction, Onfail, new Array('dvWebPartVettingExpInNext30Days', 'lblWebPartVettingExpInNext30Days'));
    lastVettingExpInNext30Days = service.get_executor();
}
var lastgetExpFailedVettingInsp = null;
function Get_Exp_Failed_Vetting_Insp() {
    var UserID = $('[id$=hdnUserID]').val();
    var DateFormat = $('[id$=hdnDateFromatMasterPage]').val();
    if (lastgetExpFailedVettingInsp != null)
        lastgetExpFailedVettingInsp.abort();
    var service = Sys.Net.WebServiceProxy.invoke('../JibeDashboardService.asmx', 'Get_Exp_Failed_Vetting_Insp', false, { "UserID": UserID, "DateFormat": DateFormat }, onSucc_LoadFunction, Onfail, new Array('dvWebPartExpiredFailedVettingInsp', 'lblWebPartExpiredFailedVettingInsp'));
    lastgetExpFailedVettingInsp = service.get_executor();
}
// end //

// Rest Hour Snippet

var lastExecutorRestHour = null;
function asyncGet_getRest_Hour_Data() {
    
    var UserID = $('[id$=hdnUserID]').val();

    if (lastExecutorRestHour != null)
        lastExecutorRestHour.abort();

    var DateFormat = $('[id$=hdnDateFromatMasterPage]').val();

    var service = Sys.Net.WebServiceProxy.invoke('../JibeDashboardService.asmx', 'getRest_Hour_Data', false, { "UserID": UserID, "DateFormat": DateFormat.toString() }, onSucc_LoadFunction, Onfail, new Array('dvWebPartRestHourData', 'lblWebPartRestHourData'));
    lastExecutorRestHour = service.get_executor();
  
}


var lastExecutorCrewRefusedToSign = null;
function asyncGet_getCrewRefusedToSignEval_Data() {
    var DateFormat = $('[id$=hdnDateFromatMasterPage]').val();
    if (lastExecutorCrewRefusedToSign != null)
        lastExecutorCrewRefusedToSign.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../JibeDashboardService.asmx', 'Get_Crw_RefusedToSignEval', false, { "DateFormat": DateFormat.toString() }, onSucc_LoadFunction, Onfail, new Array('dvWebPartCrewrefusedToSignEval', 'lblWebPartCrewRefusedToSignEval'));
    lastExecutorCrewRefusedToSign = service.get_executor();
}
///FUNCTIONS FOR PO AND INVOICE SNIPPET
var lastExecutor_POandInvoice_SummarySinppet = null;

function asyncGet_POAndInvoice_SummarySnippet() {


    if (lastExecutor_POandInvoice_SummarySinppet != null)
        lastExecutor_POandInvoice_SummarySinppet.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../JibeDashboardService.asmx', 'asyncGet_POAndInvoice_SummarySnippet', false, {}, onSucc_LoadFunction, Onfail, new Array('divProcurementApprovalOverview', 'lblProcurementApprovalOverview'));
    lastExecutor_POandInvoice_SummarySinppet = service.get_executor();
}

var lastExecutor_PendingPOApprovals = null;
function asyncGet_Pending_POApprovals_Snippet() {

    var UserID = $('[id$=hdnUserID]').val();
    var DateFormat = $('[id$=hdnDateFromatMasterPage]').val();
    if (lastExecutor_PendingPOApprovals != null)
        lastExecutor_PendingPOApprovals.abort();


    var service = Sys.Net.WebServiceProxy.invoke('../JibeDashboardService.asmx', 'Get_Pending_POApprovals_Snippet', false, { "UserID": UserID, "DateFormat": DateFormat }, onSucc_LoadFunction, Onfail, new Array('divPOPendingMyApproval', 'lblPOPendingMyApproval'));
    lastExecutor_PendingPOApprovals = service.get_executor();
}

var lastExecutor_InvoicesPendingMyVerification = null;
function asyncGet_Pending_InvoiceVerification_Snippet() {

    var UserID = $('[id$=hdnUserID]').val();
    var DateFormat = $('[id$=hdnDateFromatMasterPage]').val();
    if (lastExecutor_InvoicesPendingMyVerification != null)
        lastExecutor_InvoicesPendingMyVerification.abort();


    var service = Sys.Net.WebServiceProxy.invoke('../JibeDashboardService.asmx', 'Get_Pending_InvoiceVerification_Snippet', false, { "UserID": UserID, "DateFormat": DateFormat }, onSucc_LoadFunction, Onfail, new Array('divInvoicesPendingMyVerification', 'lblInvoicesPendingMyVerification'));
    lastExecutor_InvoicesPendingMyVerification = service.get_executor();
}

var lastExecutor_InvoicesPendingMyApproval = null;
function asyncGet_Pending_InvoiceApprovals_Snippet() {

    var UserID = $('[id$=hdnUserID]').val();
    var DateFormat = $('[id$=hdnDateFromatMasterPage]').val();
    if (lastExecutor_InvoicesPendingMyApproval != null)
        lastExecutor_InvoicesPendingMyApproval.abort();


    var service = Sys.Net.WebServiceProxy.invoke('../JibeDashboardService.asmx', 'Get_Pending_InvoiceApprovals_Snippet', false, { "UserID": UserID, "DateFormat": DateFormat }, onSucc_LoadFunction, Onfail, new Array('divInvoicesPendingMyApproval', 'lblInvoicesPendingMyApproval'));
    lastExecutor_InvoicesPendingMyApproval = service.get_executor();
}

/////------------------------------------------------------//////////

function refresh_snippet(innerHtmlVerbs) {

    
    //var innerHtmlVerbs = _this.parentNode.innerHTML.toString();


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

    else if (innerHtmlVerbs.indexOf('lblWebPartRestHourData') != -1) {
    
        asyncGet_getRest_Hour_Data();
    }
    else if (innerHtmlVerbs.indexOf('lblWebPartVettingExpInNext30Days') != -1) {
        Get_Vetting_Exp_In_Next_30Days();
    }
    else if (innerHtmlVerbs.indexOf('lblWebPartExpiredFailedVettingInsp') != -1) {
        Get_Exp_Failed_Vetting_Insp();
    }
    else if (innerHtmlVerbs.indexOf('lblWebPartCrewRefusedToSignEval') != -1) {
        asyncGet_getCrewRefusedToSignEval_Data();
    }
    else if (innerHtmlVerbs.indexOf('lblProcurementApprovalOverview') != -1) {
        asyncGet_POAndInvoice_SummarySnippet();
    }
    else if (innerHtmlVerbs.indexOf('lblPOPendingMyApproval') != -1) {

        asyncGet_Pending_POApprovals_Snippet();
    }
    else if (innerHtmlVerbs.indexOf('lblInvoicesPendingMyVerification') != -1) {

        asyncGet_Pending_InvoiceVerification_Snippet();
    }
    else if (innerHtmlVerbs.indexOf('lblInvoicesPendingMyApproval') != -1) {

        asyncGet_Pending_InvoiceApprovals_Snippet();
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

    var service = Sys.Net.WebServiceProxy.invoke('../JibeDashboardService.asmx', 'Mail_CrewOnboardListRankWise', false, { "UserID": UserID }, onSucc_OpenMail, Onfail, null);
    lastExecutorMailCrewOnboardList = service.get_executor();
}
function onSucc_OpenMail(retval) {
    try {
        window.open("../Crew/EmailEditor.aspx?ID=" + retval, '_blank');
    }
    catch (ex)
    { }
}

function Magnify(_this) {

    $('#Maghead').removeClass('MagnifyAction');
    var innerHtmlVerbs = _this.target.parentElement.parentElement.parentElement.children['0'].lastElementChild.innerHTML; // || event.target.parentElement.parentElement.parentElement.id;
    $("[id$=hdnSnippetId]").val(_this.target.parentElement.id.toString());
    if ($(_this.target.parentElement.parentElement.parentElement.children['0'].firstElementChild).css("background-color") == 'transparent' || $(_this.target.parentElement.parentElement.parentElement.children['0'].firstElementChild).css("background-color") == "rgba(0, 0, 0, 0)")
        $('#Maghead').addClass('MagnifyAction');
    else
        $(".trClass").css("background-color", $(_this.target.parentElement.parentElement.parentElement.children['0'].firstElementChild).css("background-color"));

    $("#dvSnippetContent").css({ "border": "2px solid " + $(_this.target.parentElement.parentElement.parentElement.children['0'].firstElementChild).css("background-color") });
    $("#SnippetTitle").html(_this.target.parentElement.parentElement.parentElement.children['0'].firstElementChild.textContent);

    $(".MagnifyDiv").html("<div id='dvContent'>" + innerHtmlVerbs + "</div>");
    $(".dvSetting").remove();
    $(".dvSettingColor").remove();

    document.getElementById("popupMagnifySnippet").style.display = "block";
    SetPosition_SnippetContent(_this, 'dvSnippetContent');
    $("#popupMagnifySnippet").focus();


}
function CloseSnippet() {
    document.getElementById("popupMagnifySnippet").style.display = "none";
}
function RefreshSnippet(_this) {
    refresh_snippet($("[id$=hdnSnippetId]").val());

    return false;
}
function SetPosition_SnippetContent(evt, TargetctlID) {

    var targObj = document.getElementById(TargetctlID);
    var pageX = 0;
    var pageY = 0;

    pageY = window.innerHeight
|| document.documentElement.clientHeight
|| document.body.clientHeight;

    var pageHeight = document.body.scrollHeight;
    var height = document.getElementById('dvContent').clientHeight;

    var pageWidth = document.body.scrollWidth;
    var width = document.getElementById('dvContent').clientWidth;

    var newHeight = (height > pageY * 0.9 ? pageY * 0.8 : height);


    if (height > newHeight) {
        targObj.style.height = newHeight + "px";
        $(".MagnifyDiv").height(newHeight - 20);
    }
    else {
        $('div#dvSnippetContent').css("height", "auto");
        $('.MagnifyDiv').css("height", "auto");

    }

    targObj.style.top = window.innerHeight / 8 + "px";

    targObj.style.left = (window.innerWidth - width) / 2 + "px";
}
function Setting(event) {

    var targObj = document.getElementById(event.target.id);
    var x = event.target.parentElement.parentElement.parentElement.offsetLeft;
    var Y = event.target.parentElement.parentElement.parentElement.offsetTop;
    var Width = event.target.parentElement.parentElement.parentElement.clientWidth;



    $($("#ColorPickerContainer")[0].lastElementChild).attr('id', event.target.parentElement.parentElement.parentElement.id + 'S_id');
    $("#ColorPickerContainer").show();

    document.getElementById('ColorPickerContainer').style.top = Y + 20 + "px";
    document.getElementById('ColorPickerContainer').style.left = x + (Width - 85) + "px";

    $("#" + event.target.parentElement.parentElement.parentElement.id).has("#txtTitle" + event.target.parentElement.parentElement.parentElement.id).length ? $(".editTitle").css({ 'opacity': '0.3', 'pointer-events': 'none' }) : $(".editTitle").css({ 'opacity': '1', 'pointer-events': 'auto' });
    $("#" + event.target.parentElement.parentElement.parentElement.id).has("#dvcolorPicker" + event.target.parentElement.parentElement.parentElement.id).length ? $(".editColor").css({ 'opacity': '0.3', 'pointer-events': 'none' }) : $(".editColor").css({ 'opacity': '1', 'pointer-events': 'auto' });

    $('#docking').jqxDocking('expandWindow', event.target.parentElement.parentElement.parentElement.id);
    var DepID = $('[id$=hdfUserdepartmentid]').val();
    displayEvent(event, DepID);
}
function focusevt() {

    document.getElementById("ctl00_MainContent_btnEditFavourite").disabled = true;
}
function updateTitle(e, evt) {

    // key press event only for Enter ↵
    var key = (event.keyCode ? event.keyCode : event.which);
    if (key == 13) {

        try {

            var UserID = $('[id$=hdnUserID]').val();
            var SnippetID = evt.id;
            var Title = $('#txtTitle' + evt.id).val();
            if (lastExecutor_ChangeSnippetTitle != null)
                lastExecutor_ChangeSnippetTitle.abort();

            var service = Sys.Net.WebServiceProxy.invoke('../JibeDashboardService.asmx', 'AsyncUpdate_DashboardSnippetTitle', false, { "User_ID": UserID, "SnippetID": SnippetID, "Title": Title }, OnSuccess_SaveSnippetTitle, Onfail, new Array(SnippetID));
            lastExecutor_ChangeSnippetTitle = service.get_executor();

            $("#dvEditTitle" + evt.id).remove();


            $($('#' + evt.id + 'Title')[0].nextElementSibling).css("height", "auto");
            $('div#' + evt.id).css("height", "auto");
            document.getElementById("ctl00_MainContent_btnEditFavourite").disabled = false;
            e.stopPropagation();
        }
        catch (ex) { }
    }

}

function EditTitle(event) {

    document.getElementById("ctl00_MainContent_btnEditFavourite").disabled = true;
    var str1 = event.target.parentElement.parentElement.parentElement.parentElement.parentElement.id.toString();
    var targObj = str1.substr(0, str1.indexOf('S_id'));
    var strTitle = $('#' + targObj + 'Title')[0].textContent.toString().trim();
    var btnSave = '<img id="btnSave' + targObj + '" style="cursor: pointer; margin-right:5px; vertical-align:middle " src="../Images/accept.png" onclick="asyncSaveSnippetTitle(' + targObj + ');" title="Save" />';
    var btnCancel = '<img id="btnCancel' + targObj + '" style="cursor: pointer; margin-right:5px; vertical-align:middle " src="../Images/reject.png" onclick="asyncCancelSnippetTitle(' + targObj + ');"  title="Cancel" />';
    $($('#' + targObj + 'Title')[0].nextElementSibling).scrollTop(0);
    $($('#' + targObj + 'Title')[0].nextElementSibling).prepend('<div  id ="dvEditTitle' + targObj + '" class="dvSetting"  > &nbsp;Title : <input id="txtTitle' + targObj + '" type="text" value="' + strTitle + '" style="width: 322px; background-color: #FFFFE6; margin-right:4px; color:Gray " onfocus="return focusevt()" onkeyPress ="return updateTitle(event,' + targObj + ')" onkeydown ="return updateTitle(event,' + targObj + ')" onkeyup ="return updateTitle(event,' + targObj + ')"   maxlength="50"/>' + btnSave + btnCancel + ' </div>');

    $($('#' + targObj)[0].childNodes['0']).css("height", "auto");
    $($('#' + targObj + 'Title')[0].nextElementSibling).css("height", "auto");
    $('div#' + targObj).css("height", "auto");
    $('#txtTitle' + targObj).attr('maxlength', '50');// Edit Title length has been set to 50 
}
function asyncCancelSnippetTitle(evt) {

    $("#dvEditTitle" + evt.id).remove();

    $($('#' + evt.id + 'Title')[0].nextElementSibling).css("height", "auto");
    $('div#' + evt.id).css("height", "auto");
}
var lastExecutor_ChangeSnippetTitle = null;
function asyncSaveSnippetTitle(evt) {
    try {

        var UserID = $('[id$=hdnUserID]').val();
        var SnippetID = evt.id;
        var Title = $('#txtTitle' + evt.id).val();
        if (lastExecutor_ChangeSnippetTitle != null)
            lastExecutor_ChangeSnippetTitle.abort();

        var service = Sys.Net.WebServiceProxy.invoke('../JibeDashboardService.asmx', 'AsyncUpdate_DashboardSnippetTitle', false, { "User_ID": UserID, "SnippetID": SnippetID, "Title": Title }, OnSuccess_SaveSnippetTitle, Onfail, new Array(SnippetID));
        lastExecutor_ChangeSnippetTitle = service.get_executor();

        $("#dvEditTitle" + evt.id).remove();


        $($('#' + evt.id + 'Title')[0].nextElementSibling).css("height", "auto");
        $('div#' + evt.id).css("height", "auto");
        document.getElementById("ctl00_MainContent_btnEditFavourite").disabled = false;
    }
    catch (ex) { }
}
function OnSuccess_SaveSnippetTitle(retvel, prm) {

    $($('div#' + prm[0] + 'Title')[0].firstElementChild).text(retvel);

}
function EditColor(event) {

    var str1 = event.target.parentElement.parentElement.parentElement.parentElement.parentElement.id.toString();
    var targObj = str1.substr(0, str1.indexOf('S_id'));
    var strTitle = $('#' + targObj + 'Title')[0].textContent.toString().trim();
    var btnSave = '<img id="btnSave' + targObj + '" style="cursor: pointer; margin-right:5px; display: inline; " src="../Images/accept.png" onclick="asyncSaveSnippetColor(' + targObj + ');" title="Save" />';
    var btnCancel = '<img id="btnCancel' + targObj + '" style="cursor: pointer; margin-right:5px; display: inline; " src="../Images/reject.png" onclick="asyncCancelSnippetColor(' + targObj + ');"  title="Cancel" />';


    $($('#' + targObj + 'Title')[0].nextElementSibling).scrollTop(0);
    $($('#' + targObj + 'Title')[0].nextElementSibling).prepend('<div id ="dvcolorPicker' + targObj + '" class="dvSettingColor"  ><table  style="display: inline-flex; margin-right:2px; padding-left:2px; "><tr><td>Title Background : </td><td class="a redDiv"></td><td>&nbsp;</td><td class="a blackDiv"></td><td>&nbsp;</td><td class="a purpleDiv"></td><td>&nbsp;</td><td class="a maroonDiv"></td><td>&nbsp;</td><td class="a orangeDiv"></td><td>&nbsp;</td><td class="a greenDiv"></td><td>&nbsp;</td><td class="a lightBlueDiv"></td><td>&nbsp;</td><td class="a yellowDiv"></td><td>&nbsp;</td><td class="a darkBlueDiv"></td><td>&nbsp;</td><td class="a ThemeDiv"></td></tr></table>' + btnSave + btnCancel + '</div>');

    $(".a").addClass("colorTD");
    $("div#dvcolorPicker" + targObj + " td.a").on("click", function (ev) {

        var color = $(this).css("background-color");
        $("#" + targObj + "Title").css("background-color", color);

    });

    $($('#' + targObj)[0].childNodes['0']).css("height", "auto");
    $($('#' + targObj + 'Title')[0].nextElementSibling).css("height", "auto");

    $('div#' + targObj).css("height", "auto");

}



function asyncCancelSnippetColor(evt) {

    if ($("#" + evt.id + "Title")[0].style.removeProperty) {
        $("#" + evt.id + "Title")[0].style.removeProperty('background-color');
    } else {
        $("#" + evt.id + "Title")[0].style.removeProperty('background-color');
    }

    $("#dvcolorPicker" + evt.id).remove();


    $($('#' + evt.id + 'Title')[0].nextElementSibling).css("height", "auto");
    $('div#' + evt.id).css("height", "auto");
}
var lastExecutor_ChangeSnippetColor = null;
function asyncSaveSnippetColor(evt) {
    try {

        var UserID = $('[id$=hdnUserID]').val();
        var SnippetID = evt.id;
        var color = $('#' + evt.id + "Title").css("background-color");
        if (lastExecutor_ChangeSnippetColor != null)
            lastExecutor_ChangeSnippetColor.abort();

        var service = Sys.Net.WebServiceProxy.invoke('../JibeDashboardService.asmx', 'AsyncUpdate_DashboardSnippetColor', false, { "User_ID": UserID, "SnippetID": SnippetID, "color": color }, OnSuccess_SaveSnippetColor, Onfail, new Array(SnippetID));
        lastExecutor_ChangeSnippetColor = service.get_executor();

        $("#dvcolorPicker" + evt.id).remove();

        $($('#' + evt.id + 'Title')[0].nextElementSibling).css("height", "auto");
        $('div#' + evt.id).css("height", "auto");
    }
    catch (ex) { }
}
function OnSuccess_SaveSnippetColor(retval, prm) {

    if ($("#" + prm[0] + "Title")[0].style.removeProperty) {
        $("#" + prm[0] + "Title")[0].style.removeProperty('background-color');
    } else {
        $("#" + prm[0] + "Title")[0].style.removeProperty('background-color');
    }

    $('div#' + prm[0] + 'Title').removeClass('blackDiv');
    $('div#' + prm[0] + 'Title').removeClass('ThemeDiv');
    $('div#' + prm[0] + 'Title').removeClass('redDiv');
    $('div#' + prm[0] + 'Title').removeClass('brownDiv');
    $('div#' + prm[0] + 'Title').removeClass('greenDiv');
    $('div#' + prm[0] + 'Title').removeClass('orangeDiv');
    $('div#' + prm[0] + 'Title').removeClass('maroonDiv');
    $('div#' + prm[0] + 'Title').removeClass('purpleDiv');
    $('div#' + prm[0] + 'Title').removeClass('darkBlueDiv');
    $('div#' + prm[0] + 'Title').removeClass('yellowDiv');

    $('div#' + prm[0] + 'Title').addClass(retval);
}