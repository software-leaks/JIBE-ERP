var Arr = [];
var currArrayIndex = 0;
function GetCheckListName(SheduleID, Inspid) {

    var value = SheduleID;
    var ValInspid = Inspid;
    //load_FunctionalTree(VeselID, _Equipment_Type);

    var service = Sys.Net.WebServiceProxy.invoke('../../JibeWebServiceInspection.asmx', 'Get_CheckListName_Table', false, { "ScheduleID": value, "Inspid": ValInspid }, onSucc_ChecklistName, Onfail_ChecklistName);


    return false;
}



function onSucc_ChecklistName(service) {
    document.getElementById('dvchecklistHeads').innerHTML = service;

    //document.getElementById('__tbl_remark')

    var table = document.getElementById("__tbl_remark");
    if (table != undefined) {
        if (table.rows.length == 2) {
            DirectBind();
        }
    }
    //alert(table.rows.length);
    //    for (var i = 1, row; row = table.rows[i]; i++) {
    //        //var tempCell = row.cells[0];
    //       //tempCell.style.display = "none";

    //    }
}
function Onfail_ChecklistName() {
    alert('Fail');
}


function ShowSelectedChecklist(ChecklistID, evt, objthis) {
    GetCheckListWithRatings(ChecklistID);
}

var lastExecutorMachine = null;
function DirectBind() {
    if (lastExecutorMachine != null)
        lastExecutorMachine.abort();

    //ScheduleID
    var value = document.getElementById($('[id$=hdnQuerystring]').attr('id')).value;
    var InspID = document.getElementById($('[id$=hdnQuerystringInspID]').attr('id')).value;
    var service = Sys.Net.WebServiceProxy.invoke('../../JibeWebServiceInspection.asmx', 'Get_DirectBindChecklist_Table', false, { "ScheduleID": value, "Inspection_ID": InspID }, onSucc_ChecklistWithRatings, Onfail_ChecklistWithRatings);


    return false;
}

/// This function will call service to get selected checklist details 
var lastExecutorMachine = null;
function GetCheckListWithRatings(vari) {
    if (lastExecutorMachine != null)
        lastExecutorMachine.abort();


    document.getElementById('FunctionalTree').innerHTML = '<span style=\'Margin-left:10 px;\'>Loading...</span>';
    var value = vari;
    var InspID = document.getElementById($('[id$=hdnQuerystringInspID]').attr('id')).value;

    document.getElementById($('[id$=hdnChecklistID]').attr('id')).value = vari;


    var service = Sys.Net.WebServiceProxy.invoke('../../JibeWebServiceInspection.asmx', 'Get_CheckListTable_Ratings', false, { "CheckList_ID": value, "Inspection_ID": InspID }, onSucc_ChecklistWithRatings, Onfail_ChecklistWithRatings);
    lastExecutorMachine = service.get_executor();

    return false;
}

function onSucc_ChecklistWithRatings(service) {
    document.getElementById('FunctionalTree').innerHTML = service;
 

    var val = document.getElementById('hdnFinalise1').value;

    if (document.getElementById('hdnchkID') != undefined) {
        document.getElementById($('[id$=hdnChecklistID]').attr('id')).value = document.getElementById('hdnchkID').value;
    }

    if (val == "1") {
        var x = document.getElementsByClassName("rating-star");
        var x1 = document.getElementsByClassName("Item-Option");

        var x2 = document.getElementsByClassName("attachJob");
        var x3 = document.getElementsByClassName("addJob");
        document.getElementById('dvPrint').style.display = 'none';

        //alert(x.length);
        for (var i = 0; i < x.length; i++) {
            x[i].disabled = true;
            //$(x[i]).find('input, textarea, select').attr('readonly', true)
        }
        //alert(x1.length);
        for (var i = 0; i < x1.length; i++) {
            x1[i].disabled = true;

            //$(x[i]).find('input, textarea, select').attr('readonly', true)
        }
        for (var i = 0; i < x2.length; i++) {
            //x2[i].disabled = true;
            x2[i].onclick = null;
        }

        for (var i = 0; i < x3.length; i++) {
            //x3[i].disabled = true;
            x3[i].onclick = null;

        }
    }
    else {

        document.getElementById('dvPrint').style.display = 'block';
    }

    document.getElementById($('[id$=divProgress]').attr('id')).style.display = 'None';

}
function Onfail_ChecklistWithRatings() {
    
}


var lastExecutorMachine = null;
function GetCheckListWithRatingsHDFL() {
    if (lastExecutorMachine != null)
        lastExecutorMachine.abort();

    var value = document.getElementById($('[id$=hdnChecklistID]').attr('id')).value;
    var InspID = document.getElementById($('[id$=hdnQuerystringInspID]').attr('id')).value;

    var service = Sys.Net.WebServiceProxy.invoke('../../JibeWebServiceInspection.asmx', 'Get_CheckListTable_Ratings', false, { "CheckList_ID": value, "Inspection_ID": InspID }, onSucc_ChecklistWithRatings, Onfail_ChecklistWithRatings);
    lastExecutorMachine = service.get_executor();

    return false;
}


function SaveRatings() {
    //alert('Fail');
    var radios = document.getElementsByName('rating-input-1');
    for (i = 0; i < radios.length; i++) {
        if (radios[i].checked) {
            var val = radios[i].value;
            //return radios[i].value;
        }
    }

    var radiosItem = document.getElementsByName('dvItem-');
    for (i = 0; i < radiosItem.length; i++) {
        if (radiosItem[i].checked) {
            //return radiosItem[i].value;
        }
    }

    return false;
}
//Added by Anjali DT:2-Jun-2016 || JIT:9099. To uncheck all star when , user click on newly added uncheck all stars button..
// id = id of star to be disbaled.
//Array need to be updated , to save star ratings.
// If array contain elements with NodeID of first star ,then CurrentVal updated as -1 i.e no star selected.
// If array does not contain element with NodeID of first star ,then items will be added to array.
function UncheckAllStars(id, NodeID, ChecklistID) {

    document.getElementById(id).checked = true;
    document.getElementById(id).checked = false;
    var index;
    var InspID = document.getElementById($('[id$=hdnQuerystringInspID]').attr('id')).value;
    var SessionUserID = document.getElementById($('[id$=hdnfldUserID]').attr('id')).value;

    if (Arr.length > 0) {
        if (Arr[currArrayIndex - 1] == undefined) {
            index = -1
        }
        // check for 'NodeID' in all items of array.
        for (var i = 0; i < Arr.length; i++) {
            index = Arr[i].indexOf(NodeID);
            // if node found then mark that node's value as '-1'
            if (index != -1) {
                Arr[i][3] = -1;
                break;
            }
        }

        //if 'NodeID' not found in array then add new item in array.
        if (index == -1) {
            Arr[currArrayIndex] = [];
            Arr[currArrayIndex][0] = "Node_ID";
            Arr[currArrayIndex][1] = NodeID;
            Arr[currArrayIndex][2] = "System_Current_Report";
            Arr[currArrayIndex][3] = -1
            Arr[currArrayIndex][4] = "Inspection_ID";
            Arr[currArrayIndex][5] = InspID;
            Arr[currArrayIndex][6] = "Created_By";
            Arr[currArrayIndex][7] = SessionUserID;
            Arr[currArrayIndex][8] = "cheklistID";
            Arr[currArrayIndex][9] = ChecklistID;
            currArrayIndex++;
        }

    }
    else {
        Arr[currArrayIndex] = [];
        Arr[currArrayIndex][0] = "Node_ID";
        Arr[currArrayIndex][1] = NodeID;
        Arr[currArrayIndex][2] = "System_Current_Report";
        Arr[currArrayIndex][3] = -1
        Arr[currArrayIndex][4] = "Inspection_ID";
        Arr[currArrayIndex][5] = InspID;
        Arr[currArrayIndex][6] = "Created_By";
        Arr[currArrayIndex][7] = SessionUserID;
        Arr[currArrayIndex][8] = "cheklistID";
        Arr[currArrayIndex][9] = ChecklistID;
        currArrayIndex++;
    }
}

var lastExecutorMachine = null;
function Get_Ratings(LocationID, NodeID, CurrentVal, Rating, RatingType, ChecklistID) {
    if (lastExecutorMachine != null)
        lastExecutorMachine.abort();

    var InspID = document.getElementById($('[id$=hdnQuerystringInspID]').attr('id')).value;
    var remarks = "";
    var SessionUserID = document.getElementById($('[id$=hdnfldUserID]').attr('id')).value;

    var matchedFlag = false;
    if (Arr.length > 0) {
        for (var i = 0; i < Arr.length; i++) {
            if (Arr[i][1] == NodeID) {
                Arr[i][3] = CurrentVal;
                matchedFlag = true;
                break;
            }
        }
    }
    else {

        Arr[currArrayIndex] = [];
        Arr[currArrayIndex][0] = "Node_ID";
        Arr[currArrayIndex][1] = NodeID;
        Arr[currArrayIndex][2] = "System_Current_Report";
        Arr[currArrayIndex][3] = CurrentVal;
        Arr[currArrayIndex][4] = "Inspection_ID";
        Arr[currArrayIndex][5] = InspID;
        Arr[currArrayIndex][6] = "Created_By";
        Arr[currArrayIndex][7] = SessionUserID;
        Arr[currArrayIndex][8] = "cheklistID";
        Arr[currArrayIndex][9] = ChecklistID;


        currArrayIndex++;
        matchedFlag = true;
    }

    if (matchedFlag == false) {
        Arr[currArrayIndex] = [];
        Arr[currArrayIndex][0] = "Node_ID";
        Arr[currArrayIndex][1] = NodeID;
        Arr[currArrayIndex][2] = "System_Current_Report";
        Arr[currArrayIndex][3] = CurrentVal;
        Arr[currArrayIndex][4] = "Inspection_ID";
        Arr[currArrayIndex][5] = InspID;
        Arr[currArrayIndex][6] = "Created_By";
        Arr[currArrayIndex][7] = SessionUserID;
        Arr[currArrayIndex][8] = "cheklistID";
        Arr[currArrayIndex][9] = ChecklistID;
        currArrayIndex++;
    }

    return false;
}


function onSucc_ChecklistWithRatings1(service) {
    document.getElementById('FunctionalTree').innerHTML = service.d;
    var val = "";
    if (document.getElementById('hdnFinalise1') != undefined)
        val = document.getElementById('hdnFinalise1').value;

    if (val == "1") {
        var x = document.getElementsByClassName("rating-star");
        var x1 = document.getElementsByClassName("Item-Option");

        var x2 = document.getElementsByClassName("attachJob");
        var x3 = document.getElementsByClassName("addJob");

        //alert(x.length);
        for (var i = 0; i < x.length; i++) {
            x[i].disabled = true;
            //$(x[i]).find('input, textarea, select').attr('readonly', true)
        }
        //alert(x1.length);
        for (var i = 0; i < x1.length; i++) {
            x1[i].disabled = true;
            //$(x[i]).find('input, textarea, select').attr('readonly', true)
        }
        for (var i = 0; i < x2.length; i++) {
            //x2[i].disabled = true;
            x2[i].onclick = null;
        }

        for (var i = 0; i < x3.length; i++) {
            // x3[i].disabled = true;
            x3[i].onclick = null;


        }
    }
    document.getElementById('dvPrint').style.display = 'block';
    alert('Ratings saved successfully.');

    var SheduleID = document.getElementById($('[id$=hdnQuerystring]').attr('id')).value;

    GetCheckListName(SheduleID);
}

var lastExecutorMachine = null;
function SaveAll() {
    if (lastExecutorMachine != null)
        lastExecutorMachine.abort();



    var tableArray = "";

    if (Arr.length > 0) {
        for (var i = 0; i < Arr.length; i++) {
            tableArray += "{";
            for (var j = 0; j < Arr[i].length; j++) {
                if (Arr[i].length - 1 != j) {
                    tableArray += Arr[i][j] + ",";
                }
                else {
                    tableArray += Arr[i][j];
                }
            }
            tableArray += "}";

        }
    }
    else {
        var InspID = document.getElementById($('[id$=hdnQuerystringInspID]').attr('id')).value;
        var chkID = document.getElementById($('[id$=hdnChecklistID]').attr('id')).value;
        tableArray += chkID + ",";
        tableArray += InspID;
    }

    Arr = [];
    currArrayIndex = 0;
    Arr[currArrayIndex] = [];


    $.ajax({
        type: "POST",
        url: "../../JibeWebServiceInspection.asmx/INS_Checklist_Ratings",
        //data: JSON.stringify({ arr: Arr, Node_ID: NodeID, System_Current_Report: CurrentVal, Inspection_ID: InspID, Created_By: SessionUserID, cheklistID: ChecklistID }),

        data: JSON.stringify({ arr: tableArray }),
        //data: jsonText,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: onSucc_ChecklistWithRatings1,
        failure: Onfail_ChecklistWithRatings,
        error: onerror
    });

    //    var service = Sys.Net.WebServiceProxy.invoke('../../JibeWebService.asmx', 'INS_Checklist_Ratings', false, { "arr": Arr, "Node_ID": NodeID, "System_Current_Report": CurrentVal, "Inspection_ID": InspID, "Created_By": SessionUserID, "cheklistID": ChecklistID }, onSucc_ChecklistWithRatings, Onfail_ChecklistWithRatings);

    //    lastExecutorMachine = service.get_executor();

    return false;
}

function onerror(retval) {
    alert(retval);
}
function Get_JobsAtteched(LocationNodeID, Checklist_ItemID) {

    var InspID = document.getElementById($('[id$=hdnQuerystringInspID]').attr('id')).value;
    var SheduleID = document.getElementById($('[id$=hdnQuerystring]').attr('id')).value;
    var remarks = "";
    var SessionUserID = document.getElementById($('[id$=hdnfldUserID]').attr('id')).value;
    var VesselID = document.getElementById($('[id$=hdnVesselID]').attr('id')).value;


    document.getElementById('IframeEditJob').src = '../Inspection/AttachJob.aspx?LocationNodeID=' + LocationNodeID + '&LocationID=' + Checklist_ItemID + '&InspID=' + InspID + '&SheduleID=' + SheduleID + '&VesselID=' + VesselID;
    ///alert(document.getElementById('IframeJob').src);
    showModal('dvEditJob');
    $("#dvEditJob").prop('title', 'Assign Job');

}


//function AddDefect(LocationID, InspID, VesselID) {
//debugger;
function AddDefect(LocationNodeID, LocationID) {

    var InspID = document.getElementById($('[id$=hdnQuerystringInspID]').attr('id')).value;
    var SheduleID = document.getElementById($('[id$=hdnQuerystring]').attr('id')).value;
    var remarks = "";
    var SessionUserID = document.getElementById($('[id$=hdnfldUserID]').attr('id')).value;
    var VesselID = document.getElementById($('[id$=hdnVesselID]').attr('id')).value;

    document.getElementById('IframeJob').src = '../Inspection/AddConditionReport.aspx?LocationNodeID=' + LocationNodeID + '&LocationID=' + LocationID + '&InspID=' + InspID + '&VesselID=' + VesselID;
    ///alert(document.getElementById('IframeJob').src);
    showModal('dvJob', false, GetCheckListWithRatingsHDFL);
    $("#dvJob").prop('title', 'Defect/Condition Report');
    return false;
}
function ExportRatings() {

    var chkID = document.getElementById($('[id$=hdnChecklistID]').attr('id')).value;
    var InspID = document.getElementById($('[id$=hdnQuerystringInspID]').attr('id')).value;

    var service = Sys.Net.WebServiceProxy.invoke('../../JibeWebServiceInspection.asmx', 'INS_ExportToExcel', false, { "CheckList_ID": chkID, "Inspection_ID": InspID }, onSucc_Export, Onfail_Export);

    return false;
}

function onSucc_Export(service) {
    alert(service);
}
function Onfail_Export() {
    alert('Fail');
}


function saveCloseChild() {
    var chkID = document.getElementById($('[id$=hdnChecklistID]').attr('id')).value;


    //location.reload();
    GetCheckListWithRatings(chkID);
    //$find("dvJob").hide(); 
    hideModal("dvEditJob");

    return true;
}


function UpdatePage() {

    var chkID = document.getElementById($('[id$=hdnChecklistID]').attr('id')).value;

    GetCheckListWithRatings(chkID);
    //$find("dvJob").hide(); 
    hideModal("dvJob");

    return true;
}
var lastExecutorMachine = null;
function Finalize() {
    if (lastExecutorMachine != null)
        lastExecutorMachine.abort();

    var chkID = document.getElementById($('[id$=hdnChecklistID]').attr('id')).value;
    var InspID = document.getElementById($('[id$=hdnQuerystringInspID]').attr('id')).value;
    var SheduleID = document.getElementById($('[id$=hdnQuerystring]').attr('id')).value;
    var SessionUserID = document.getElementById($('[id$=hdnfldUserID]').attr('id')).value;

    var service = Sys.Net.WebServiceProxy.invoke('../../JibeWebServiceInspection.asmx', 'Finalize', false, { "CheckList_ID": chkID, "Inspection_ID": InspID, "Shedule_ID": SheduleID, "Created_By": SessionUserID }, onSucc_ChecklistWithRatings, Onfail_ChecklistWithRatings);

    lastExecutorMachine = service.get_executor();
    return false;
}