var _Vessel_ID = 0, _FunctionCode = null, _SystemID = null, _SystemCode = null, _SubSystemID = null, _SubSystemCode = null, _SystemLocation = null, _SubSystemLocation = null, _Equipment_Type = 1, _DepartmentCode = null, _JobID = null;
var _sort_direction = 0;
var ParentTypeCode = null, SearchText = null, SystemCode = null, SubSystemCode = null, SearchTextLocation = null, VesselId = null, SortBy = null, SortDirection = null, PageNumber = null, PageSize = null, IsFetchCount = null;
var __isResponse = 1, __isResponse1 = 1;
var _retrivedCatSupplier = false;
var _ActiveJobStatus = null, _ActiveItemStatus = null;
var _TempJobsTable = "";
var groupFlag = false;
var LocationFlag = false;
var datatable = []; //For datatable in this


var _MGSSelectedItemCategory = "", _MGSSelectedUnits = "";
function ddlEquipmentType_selectinChanged(obj) {

    var Equipment_Type = $('[id$=ddlEquipmentType]').find(":checked").val();

    if (Equipment_Type == 0)
        _Equipment_Type = Equipment_Type;
    else if (Equipment_Type == "AC")
        _Equipment_Type = 1;
    else if (Equipment_Type == "SP")
        _Equipment_Type = 2

    DDlVessel_selectionChange();


}

var version = "";
var Parentid = "";


//debugger;
function AddNewClick() {
    document.getElementById($('[id$=dvAddNew]').attr('id')).style.display = "none";
    document.getElementById("dvCheckList").style.display = "block";
    document.getElementById("dvGroup").style.display = "none";
    document.getElementById("dvLocation").style.display = "none";
    document.getElementById('dvCKItemsQB').style.display = "none";
    document.getElementById('dvSaveItems').style.display = "none";

    document.getElementById($('[id$=hdnStatus]').attr('id')).value = "0";

    var ver = 1.1;
    version = ver.toFixed(1);
    Parentid = "";
    return false;
}
// DDlVessel_selectionChange
function GetCheckList(vari) {

    var value = vari;
    //load_FunctionalTree(VeselID, _Equipment_Type);

    var service = Sys.Net.WebServiceProxy.invoke('../../JibeWebServiceInspection.asmx', 'Get_CheckList_Table', false, { "CheckList_ID": value }, onSucc_EquipmentFunction1, Onfail1);


    return false;
}
//debugger;
function onSucc_EquipmentFunction1(service) {
    document.getElementById('FunctionalTree').innerHTML = service;

    var status = document.getElementById('hdnStat').value;

    //hdnChildFlag
    var childFlag = document.getElementById('hdnChildFlag').value;
    document.getElementById('dvEditPublish').style.display = 'block';
    if (status == 0) {
        //alert('Publish');
        if (childFlag == 0) {
            document.getElementById($('[id$=btnPublish]').attr('id')).disabled = false;
            document.getElementById($('[id$=btnEdit]').attr('id')).disabled = true;
            //document.getElementById($('[id$=dvUP3]').attr('id')).disabled = false;
            document.getElementById('dvUP3').style.display = 'none';
            document.getElementById('dvUpdateButton').style.display = 'none';

        }
        else {
            document.getElementById($('[id$=btnPublish]').attr('id')).disabled = true;
            document.getElementById($('[id$=btnEdit]').attr('id')).disabled = true;
            document.getElementById('dvUP3').style.display = 'block';
            document.getElementById('dvUpdateButton').style.display = 'block';

        }
    }
    else {
        //alert('Edit');
        if (childFlag == 0) {
            document.getElementById($('[id$=btnPublish]').attr('id')).disabled = true;
            document.getElementById($('[id$=btnEdit]').attr('id')).disabled = false;
            document.getElementById('dvUP3').style.display = 'block';
            document.getElementById('dvUpdateButton').style.display = 'block';
        }
        else {
            document.getElementById($('[id$=btnPublish]').attr('id')).disabled = true;
            document.getElementById($('[id$=btnEdit]').attr('id')).disabled = true;
            document.getElementById('dvUP3').style.display = 'none';
            document.getElementById('dvUpdateButton').style.display = 'none';
        }
    }
}
function onSucc_Item_Isert(service) {
    document.getElementById('FunctionalTree').innerHTML = service;

    var chkListID = document.getElementById($('[id$=hdnfldChecklstID]').attr('id')).value;
    var locationID = document.getElementById($('[id$=hdnfldLocationID]').attr('id')).value;
    datatable = [];

    var table = document.getElementById("tblChkListQB");
    for (var i = 1, row; row = table.rows[i]; i++) {
        var tempCell = row.cells[0];
        //var tempCell2 = row.cells[3];

        var id = tempCell.innerHTML;
        var chk = document.getElementById("tblChkListQB_Active_Status" + i).checked;
        if (chk == true) {
            datatable.push([id.trim(), 1]);
        }
        else {
            datatable.push([id.trim(), 0]);
        }
    }
    GetCheckListItemQB(chkListID, locationID);
}
function Onfail1() {
    //lastExecutorMachine = null;
    alert('Fail.');
}
var locID = "";
function GetLocations(vari, Location_ID) {
    var value = vari;
    locID = Location_ID;
    document.getElementById('dvLocationDDL').innerHTML = '<select id="txtLocation" style="Width:150px;" ><option value="0" selected>--Select--</option></select>';
    //var service = Sys.Net.WebServiceProxy.invoke('../../JibeWebServiceInspection.asmx', 'Get_Location', false, { "VesselTypeID": value, "Location_ID": Location_ID }, onSucc_LocationBinding, Onfail1);

    return false;
}

function onSucc_LocationBinding(service) {
    //  document.getElementById('dvLocationDDL').innerHTML = service;
    document.getElementById('dvLocationDDL').innerHTML = '<select id="txtLocation" style="Width:150px;" ><option value="0" selected>--Select--</option></select>';

}

function ShowAddDiv(Checklist_ID, ChecklistItem_ID, CheckList_Name, NodeType, Vessel_Type, checklistType, Grading_Type, Checklist_IDCopy, Parent_ID, Location_ID, evt, objthis) {
    //objthis.style.color = 'Red';
    //objthis.uniqueID.style.color = 'Red';
    //document.getElementById(objthis.uniqueID).style.color = 'Red';
    lastExecutorMachineInsertCheckList = null;
    var status = document.getElementById('hdnStat').value;
    var childFlag = document.getElementById('hdnChildFlag').value;

    if (status == 0) {
        if (Checklist_ID != "" && NodeType != "") {
            checklistSelected(Checklist_ID, CheckList_Name, Vessel_Type, checklistType, Grading_Type);
            //document.getElementById("ctl00_MainContent_hdnfldVesselType").value = Vessel_Type;
            document.getElementById($('[id$=hdnfldVesselType]').attr('id')).value = Vessel_Type;
            groupFlag = false;
            LocationFlag = false;
            document.getElementById($('[id$=hdnfldVesselType]').attr('id')).value = Vessel_Type;
            document.getElementById($('[id$=hdnfldGroupID]').attr('id')).value = "";
            GetLocations(Vessel_Type, "");
        }
        else if (NodeType == "Group") {
            groupSelected(ChecklistItem_ID, CheckList_Name, Checklist_IDCopy);
            groupFlag = true;
            LocationFlag = true;
            if (Parent_ID != "") {
                document.getElementById($('[id$=hdnParentID]').attr('id')).value = Parent_ID;
            }
            else {
                document.getElementById($('[id$=hdnParentID]').attr('id')).value = "";
            }
            //var vari = document.getElementById($('[id$=hdnfldVesselType]').attr('id')).value;

            GetLocations(Vessel_Type, "");
        }
        else if (NodeType == "Location") {
            document.getElementById($('[id$=txtfilter]').attr('id')).value = "";
            locationSelected(ChecklistItem_ID, CheckList_Name, Checklist_IDCopy, Parent_ID, Location_ID);
            //document.getElementById($('[id$=txtfilter]').attr('id')).value = "";
            //var vari = document.getElementById("ctl00_MainContent_hdnfldVesselType").value;
            //var vari = document.getElementById($('[id$=hdnfldVesselType]').attr('id')).value;
            //GetLocations(vari, Location_ID);
            GetLocations(Vessel_Type, Location_ID);
            LocationFlag = true;
            //LocationFlag = false;



        }
        else {
            //    alert('Item is selected') 
        }
    }
    else {
    }


}

function checklistSelected(Checklist_ID, CheckList_Name, Vessel_Type, checklistType, Grading_Type) {
    document.getElementById($('[id$=dvAddNew]').attr('id')).style.display = "none";
    document.getElementById("dvCheckList").style.display = "block";
    document.getElementById("dvGroup").style.display = "block";
    document.getElementById("dvGroupUpdate").style.display = "none";
    document.getElementById("dvLocation").style.display = "block";
    document.getElementById('dvCKItemsQB').style.display = "none";
    document.getElementById('dvSaveItems').style.display = "none";

    document.getElementById('dvbtnDelLoc').style.display = "none";

    document.getElementById('dvItemsFilter').style.display = "none";

    document.getElementById($('[id$=hdnfldChecklstID]').attr('id')).value = Checklist_ID;
    document.getElementById($('[id$=txtChecklistName]').attr('id')).value = CheckList_Name;
    document.getElementById($('[id$=ddlvesselType]').attr('id')).value = Vessel_Type;
    document.getElementById($('[id$=ddlvesselType]').attr('id')).disabled = true;
    document.getElementById($('[id$=ddlchecklistType]').attr('id')).value = checklistType;
    document.getElementById($('[id$=ddlchecklistType]').attr('id')).disabled = true;
    document.getElementById($('[id$=ddlGrades]').attr('id')).value = Grading_Type;

    document.getElementById($('[id$=hdnfldLocationID]').attr('id')).value = "";
    document.getElementById($('[id$=txtLocationName]').attr('id')).value = "";
    //document.getElementById("ctl00_MainContent_txtLocation").value = "";
}

function groupSelected(ChecklistItem_ID, CheckList_Name, Checklist_IDCopy) {//Vessel_Type IS used as  Checklist ID
    document.getElementById($('[id$=dvAddNew]').attr('id')).style.display = "none";
    document.getElementById("dvCheckList").style.display = "none";
    document.getElementById("dvGroupUpdate").style.display = "block";
    document.getElementById("dvGroup").style.display = "block";
    document.getElementById("dvLocation").style.display = "block";
    document.getElementById('dvCKItemsQB').style.display = "none";
    document.getElementById('dvSaveItems').style.display = "none";

    document.getElementById('dvbtnDelLoc').style.display = "none";

    document.getElementById('dvItemsFilter').style.display = "none";

    document.getElementById($('[id$=lblLocationCaption]').attr('id')).innerHTML = 'Add Location:';
    document.getElementById($('[id$=hdnfldChecklstID]').attr('id')).value = Checklist_IDCopy;
    document.getElementById($('[id$=hdnfldGroupID]').attr('id')).value = ChecklistItem_ID;
    document.getElementById($('[id$=txtEditGroupName]').attr('id')).value = CheckList_Name;
    document.getElementById($('[id$=hdnfldLocationID]').attr('id')).value = "";
    document.getElementById($('[id$=txtLocationName]').attr('id')).value = "";
}



function locationSelected(ChecklistItem_ID, CheckList_Name, Vessel_Type, checklistType, Grading_Type) {// Vessel_Type IS used as  Checklist ID   &&&  checklistType is for parent id of location
    document.getElementById($('[id$=dvAddNew]').attr('id')).style.display = "none";
    document.getElementById("dvCheckList").style.display = "none";
    document.getElementById("dvGroup").style.display = "none";
    document.getElementById("dvGroupUpdate").style.display = "none";
    document.getElementById("dvLocation").style.display = "block";
    document.getElementById('dvCKItemsQB').style.display = "block";
    document.getElementById('dvSaveItems').style.display = "block";

    document.getElementById('dvbtnDelLoc').style.display = "block";

    document.getElementById('dvItemsFilter').style.display = "block";
    document.getElementById($('[id$=lblLocationCaption]').attr('id')).innerHTML = 'Edit Location:';

    document.getElementById($('[id$=hdnfldChecklstID]').attr('id')).value = Vessel_Type;
    document.getElementById($('[id$=hdnfldGroupID]').attr('id')).value = checklistType;
    document.getElementById($('[id$=hdnfldLocationID]').attr('id')).value = ChecklistItem_ID;
    document.getElementById($('[id$=txtLocationName]').attr('id')).value = CheckList_Name;

    GetCheckListItemQB1(Vessel_Type, ChecklistItem_ID);
}

function GetCheckListItemQB1(Vessel_Type, ChecklistItem_ID) {// Vessel_Type IS used as  Checklist ID  && ChecklistItem_ID as Location ID

    var value = Vessel_Type, valuepID = ChecklistItem_ID;

    var descr = document.getElementById($('[id$=txtfilter]').attr('id')).value;

    var service = Sys.Net.WebServiceProxy.invoke('../../JibeWebServiceInspection.asmx', 'Get_CheckListItetemsQB_filter_Table', false, { "CheckList_ID": value, "Parent_ID": valuepID, "description": descr }, onSucc_BindCHKItemsQB1, OnfailQB);
    //var service = Sys.Net.WebServiceProxy.invoke('../../JibeWebService.asmx', 'Get_CheckListItetemsQB_Table', false, { "CheckList_ID": value, "Parent_ID": valuepID }, onSucc_BindCHKItemsQB, OnfailQB);

    return false;
}

function onSucc_BindCHKItemsQB1(service) {
    lastExecutorMachineSearch = null;
    document.getElementById('dvCKItemsQB').innerHTML = service;

    if (service.indexOf("tblChkListQB_Active_Status") > 0) {
        var count = (service.match(/tblChkListQB_Active_Status/g).length);
        var i = 1;
        for (i = 1; i < count + 1; i++) {
            var chk = document.getElementById("tblChkListQB_Active_Status" + i);
            chk.disabled = false;
        }


        var table = document.getElementById("tblChkListQB");
        for (var i = 1, row; row = table.rows[i]; i++) {
            var tempCell = row.cells[0];
            tempCell.style.display = "none";

        }
    }
    datatable = [];

    var table = document.getElementById("tblChkListQB");
    for (var i = 1, row; row = table.rows[i]; i++) {
        var tempCell = row.cells[0];
        //var tempCell2 = row.cells[3];

        var id = tempCell.innerHTML;
        var chk = document.getElementById("tblChkListQB_Active_Status" + i).checked;
        if (chk == true) {
            datatable.push([id.trim(), 1]);
        }
        else {
            datatable.push([id.trim(), 0]);
        }
    }


    //    alert('Success');
}

function GetCheckListItemQB(Vessel_Type, ChecklistItem_ID) {// Vessel_Type IS used as  Checklist ID  && ChecklistItem_ID as Location ID

    var value = Vessel_Type, valuepID = ChecklistItem_ID;

    var descr = document.getElementById($('[id$=txtfilter]').attr('id')).value;

    var service = Sys.Net.WebServiceProxy.invoke('../../JibeWebServiceInspection.asmx', 'Get_CheckListItetemsQB_filter_Table', false, { "CheckList_ID": value, "Parent_ID": valuepID, "description": descr }, onSucc_BindCHKItemsQB, OnfailQB);
    //var service = Sys.Net.WebServiceProxy.invoke('../../JibeWebService.asmx', 'Get_CheckListItetemsQB_Table', false, { "CheckList_ID": value, "Parent_ID": valuepID }, onSucc_BindCHKItemsQB, OnfailQB);

    return false;
}

function onSucc_BindCHKItemsQB(service) {
    lastExecutorMachineSearch = null;
    document.getElementById('dvCKItemsQB').innerHTML = service;

    if (service.indexOf("tblChkListQB_Active_Status") > 0) {
        var count = (service.match(/tblChkListQB_Active_Status/g).length);
        var i = 1;
        for (i = 1; i < count + 1; i++) {
            var chk = document.getElementById("tblChkListQB_Active_Status" + i);
            chk.disabled = false;
        }


        var table = document.getElementById("tblChkListQB");
        for (var i = 1, row; row = table.rows[i]; i++) {
            var tempCell = row.cells[0];
            tempCell.style.display = "none";

        }
    }
    //        datatable = [];

    //        var table = document.getElementById("tblChkListQB");
    //        for (var i = 1, row; row = table.rows[i]; i++) {
    //            var tempCell = row.cells[0];
    //            //var tempCell2 = row.cells[3];
    //            
    //            var id = tempCell.innerHTML;
    //            var chk = document.getElementById("tblChkListQB_Active_Status" + i).checked;
    //            if (chk == true) {
    //                datatable.push([id.trim(), 1]);
    //            }
    //            else {
    //                datatable.push([id.trim(), 0]);
    //            }
    //        }


    //    alert('Success');
}
function OnfailQB() {
    //alert('Fail');
}

function QuestionValueChange(ID, Status) {
    for (var i = 0; i < datatable.length; i++) {
        //var ins = datatable[i];
        var ins = datatable[i][0];
        var n = ins.localeCompare(ID);
        if (n == 0) {
            datatable[i][1] = Status;
        }
    }
}


var lastExecutorMachineInsertCheckList = null;
function InsertCheckList() {

    if (lastExecutorMachineInsertCheckList == null) {//lastExecutorMachine.abort();

        var chkListID = document.getElementById($('[id$=hdnfldChecklstID]').attr('id')).value;
        var chkName = document.getElementById($('[id$=txtChecklistName]').attr('id')).value;
        var vesltype = document.getElementById($('[id$=ddlvesselType]').attr('id')).value;
        var chktype = document.getElementById($('[id$=ddlchecklistType]').attr('id')).value;
        var locGrades = document.getElementById($('[id$=ddlGrades]').attr('id')).value;
        var SessionUserID = document.getElementById($('[id$=hdnfldUserID]').attr('id')).value;

        if (chkName == "" || vesltype == "0" || chktype == "0" || locGrades == "0") {
            alert('All fields are mandatory.');
            return false;
        }

        var stat = document.getElementById($('[id$=hdnStatus]').attr('id')).value;

        //document.getElementById($('[id$=hdnStatus]').attr('id')).value = "0";
        // version = 1.0;

        var service = Sys.Net.WebServiceProxy.invoke('../../JibeWebServiceInspection.asmx', 'INS_CheckList_Table', false, { "Checklist_ID": chkListID, "Parent_ID": Parentid, "Version": version, "CheckList_Name": chkName, "Vessel_Type": vesltype,
            "checklist_Type": chktype, "Location_Grade": locGrades, "Created_By": SessionUserID, "Modified_By": SessionUserID, "Status": stat
        }, onSucc_CHKLST_Save, OnFail_InsertCheckList, new Array('FunctionalTree'));

        lastExecutorMachineInsertCheckList = service.get_executor();

        document.getElementById($('[id$=hdnfldVesselType]').attr('id')).value = vesltype;
        GetLocations(vesltype, "");

        //    //document.getElementById($('[id$=hdnfldChecklstID]').attr('id')).value = "";
        //    document.getElementById($('[id$=txtChecklistName]').attr('id')).value = "";
        //    document.getElementById($('[id$=ddlvesselType]').attr('id')).value = "0";
        //    document.getElementById($('[id$=ddlchecklistType]').attr('id')).value = "0";
        //    document.getElementById($('[id$=ddlGrades]').attr('id')).value = "0";
        //document.getElementById('dvCheckList').style.display = "none";

    }
    return false;
}

function OnFail_InsertCheckList(error) {
    lastExecutorMachineInsertCheckList = null;
    alert(error.get_message());

}
function onSucc_CHKLST_Save(service) {
    //lastExecutorMachine = null;
    document.getElementById('FunctionalTree').innerHTML = service;

    var status = 0;
    if (document.getElementById('hdnStat').value == -5) {
        lastExecutorMachineInsertCheckList = null;
        alert('Checklist already exists.');
    }
    else {
        status = document.getElementById('hdnStat').value;

        //hdnChildFlag
        var childFlag = document.getElementById('hdnChildFlag').value;
        document.getElementById('dvEditPublish').style.display = 'block';
        if (status == 0) {
            //alert('Publish');
            if (childFlag == 0) {
                document.getElementById($('[id$=btnPublish]').attr('id')).disabled = false;
                document.getElementById($('[id$=btnEdit]').attr('id')).disabled = true;
                //document.getElementById($('[id$=dvUP3]').attr('id')).disabled = false;
                document.getElementById('dvUP3').style.display = 'none';
                document.getElementById('dvUpdateButton').style.display = 'none';

            }
            else {
                document.getElementById($('[id$=btnPublish]').attr('id')).disabled = true;
                document.getElementById($('[id$=btnEdit]').attr('id')).disabled = true;
                document.getElementById('dvUP3').style.display = 'block';
                document.getElementById('dvUpdateButton').style.display = 'block';

            }
        }
        else {
            //alert('Edit');
            if (childFlag == 0) {
                document.getElementById($('[id$=btnPublish]').attr('id')).disabled = true;
                document.getElementById($('[id$=btnEdit]').attr('id')).disabled = false;
                document.getElementById('dvUP3').style.display = 'block';
                document.getElementById('dvUpdateButton').style.display = 'block';
            }
            else {
                document.getElementById($('[id$=btnPublish]').attr('id')).disabled = true;
                document.getElementById($('[id$=btnEdit]').attr('id')).disabled = true;
                document.getElementById('dvUP3').style.display = 'none';
                document.getElementById('dvUpdateButton').style.display = 'none';
            }
        }
        alert('Checklist saved successfully.');
        document.getElementById('SPremarks').innerHTML = '* Checklist saved successfully. Please select to add group/location.';
    }

}

var lastExecutorMachine = null;
function InsertGroup() {
    if (lastExecutorMachine != null)
        lastExecutorMachine.abort();

    if (document.getElementById($('[id$=txtGroupName]').attr('id')).value != "") {


        var chkListID = document.getElementById($('[id$=hdnfldChecklstID]').attr('id')).value;
        var ParentID = document.getElementById($('[id$=hdnfldGroupID]').attr('id')).value;
        var chkDes = document.getElementById($('[id$=txtGroupName]').attr('id')).value;
        var SessionUserID = document.getElementById($('[id$=hdnfldUserID]').attr('id')).value;

        if (groupFlag == false) {
            ParentID = null;
        }

        var service = Sys.Net.WebServiceProxy.invoke('../../JibeWebServiceInspection.asmx', 'INS_CHKGroupANDLocation', false, { "ID": null, "ParentID": ParentID, "Checklist_ID": chkListID, "LocationId": null, "NodeType": 'Group', "Description": chkDes, "Created_By": SessionUserID, "Modified_By": SessionUserID, "ActiveStatus": null }, onSucc_EquipmentFunction1, Onfail1, new Array('FunctionalTree'));
        lastExecutorMachine = service.get_executor();

        //document.getElementById("ctl00_MainContent_txtGroupName").value = "";
        document.getElementById($('[id$=txtGroupName]').attr('id')).value = "";
        //groupFlag = false;
    }
    else {
        alert('Please enter group name.');
    }

    return false;

}

var lastExecutorMachine = null;
function UpdateGroup() {
    if (lastExecutorMachine != null)
        lastExecutorMachine.abort();
    if (document.getElementById($('[id$=txtEditGroupName]').attr('id')).value != "") {
        var chkListID = document.getElementById($('[id$=hdnfldChecklstID]').attr('id')).value;
        var groupID = document.getElementById($('[id$=hdnfldGroupID]').attr('id')).value;
        var ParentID = document.getElementById($('[id$=hdnParentID]').attr('id')).value;
        var chkDes = document.getElementById($('[id$=txtEditGroupName]').attr('id')).value; //document.getElementById("ctl00_MainContent_txtGroupName").value;
        var SessionUserID = document.getElementById($('[id$=hdnfldUserID]').attr('id')).value;

        var service = Sys.Net.WebServiceProxy.invoke('../../JibeWebServiceInspection.asmx', 'INS_CHKGroupANDLocation', false, { "ID": groupID, "ParentID": ParentID, "Checklist_ID": chkListID, "LocationId": null, "NodeType": 'Group', "Description": chkDes, "Created_By": SessionUserID, "Modified_By": SessionUserID, "ActiveStatus": null }, onSucc_EquipmentFunction1, Onfail1, new Array('FunctionalTree'));
        lastExecutorMachine = service.get_executor();

        document.getElementById($('[id$=hdnfldGroupID]').attr('id')).value = "";
        document.getElementById($('[id$=txtEditGroupName]').attr('id')).value = "";
        document.getElementById('SPremarks').innerHTML = '* Group updated successfully. Please select to add group/location. ';


        document.getElementById("dvGroupUpdate").style.display = "none";
    }
    else {
        alert('Please enter group name.');
    }


    return false;

}

var lastExecutorMachine = null;
function DeleteGroup() {
    if (lastExecutorMachine != null)
        lastExecutorMachine.abort();

    var chkListID = document.getElementById($('[id$=hdnfldChecklstID]').attr('id')).value;
    var groupID = document.getElementById($('[id$=hdnfldGroupID]').attr('id')).value;
    var chkDes = document.getElementById($('[id$=txtEditGroupName]').attr('id')).value; //document.getElementById("ctl00_MainContent_txtGroupName").value;
    var SessionUserID = document.getElementById($('[id$=hdnfldUserID]').attr('id')).value;

    var activstatus = 0;

    var service = Sys.Net.WebServiceProxy.invoke('../../JibeWebServiceInspection.asmx', 'INS_CHKGroupANDLocation', false, { "ID": groupID, "ParentID": null, "Checklist_ID": chkListID, "LocationId": null, "NodeType": 'Group', "Description": chkDes, "Created_By": SessionUserID, "Modified_By": SessionUserID, "ActiveStatus": activstatus }, onSucc_EquipmentFunction1, Onfail1, new Array('FunctionalTree'));
    lastExecutorMachine = service.get_executor();

    document.getElementById($('[id$=hdnfldGroupID]').attr('id')).value = "";
    document.getElementById($('[id$=txtEditGroupName]').attr('id')).value = "";

    document.getElementById($('[id$=dvAddNew]').attr('id')).style.display = "none";
    document.getElementById("dvCheckList").style.display = "none";
    document.getElementById("dvGroup").style.display = "none";
    document.getElementById("dvGroupUpdate").style.display = "none";
    document.getElementById("dvLocation").style.display = "none";
    document.getElementById('dvCKItemsQB').style.display = "none";
    document.getElementById('dvSaveItems').style.display = "none";

    return false;

}


var lastExecutorMachine = null;
function InsertLocation() {
    if (lastExecutorMachine != null)
        lastExecutorMachine.abort();
    if (document.getElementById($('[id$=txtLocationName]').attr('id')).value != "") {
        var chkListID = document.getElementById($('[id$=hdnfldChecklstID]').attr('id')).value;
        var groupID = document.getElementById($('[id$=hdnfldGroupID]').attr('id')).value;
        var locationID = document.getElementById($('[id$=hdnfldLocationID]').attr('id')).value;
        var locationName = document.getElementById($('[id$=txtLocationName]').attr('id')).value;
        var locationElelment = document.getElementById('txtLocation');
        var location = locationElelment.options[locationElelment.selectedIndex].value;
        var SessionUserID = document.getElementById($('[id$=hdnfldUserID]').attr('id')).value;


        if (LocationFlag == false) {
            groupID = null;
        }

        var service = Sys.Net.WebServiceProxy.invoke('../../JibeWebServiceInspection.asmx', 'INS_CHKGroupANDLocation', false, { "ID": locationID, "ParentID": groupID, "Checklist_ID": chkListID, "LocationId": location, "NodeType": 'Location', "Description": locationName, "Created_By": SessionUserID, "Modified_By": SessionUserID, "ActiveStatus": null }, onSucc_EquipmentFunction1, Onfail1, new Array('FunctionalTree'));
        lastExecutorMachine = service.get_executor();



        if (locationID != "") {
            document.getElementById('SPremarks').innerHTML = '* Location updated successfully. Please select and perform. ';
        }
        else {
            //document.getElementById($('[id$=hdnfldGroupID]').attr('id')).value = "";
            document.getElementById($('[id$=hdnfldLocationID]').attr('id')).value = "";
            document.getElementById($('[id$=txtLocationName]').attr('id')).value = "";
            locationElelment.value = 0;
        }
        //LocationFlag = false;
    }
    else {
        alert('Please enter location name.');
    }
    return false;

}
var lastExecutorMachine = null;
function DeleteLocation() {
    if (lastExecutorMachine != null)
        lastExecutorMachine.abort();

    var chkListID = document.getElementById($('[id$=hdnfldChecklstID]').attr('id')).value;
    var groupID = document.getElementById($('[id$=hdnfldGroupID]').attr('id')).value;
    var locationID = document.getElementById($('[id$=hdnfldLocationID]').attr('id')).value;
    var locationName = document.getElementById($('[id$=txtLocationName]').attr('id')).value;
    var locationElelment = document.getElementById('txtLocation');
    var location = locationElelment.options[locationElelment.selectedIndex].value;
    var SessionUserID = document.getElementById($('[id$=hdnfldUserID]').attr('id')).value;
    var activstatus = 0;
    if (LocationFlag == false) {
        groupID = null;
    }

    var service = Sys.Net.WebServiceProxy.invoke('../../JibeWebServiceInspection.asmx', 'INS_CHKGroupANDLocation', false, { "ID": locationID, "ParentID": groupID, "Checklist_ID": chkListID, "LocationId": location, "NodeType": 'Location', "Description": locationName, "Created_By": SessionUserID, "Modified_By": SessionUserID, "ActiveStatus": activstatus }, onSucc_EquipmentFunction1, Onfail1, new Array('FunctionalTree'));
    lastExecutorMachine = service.get_executor();

    //document.getElementById($('[id$=hdnfldGroupID]').attr('id')).value = "";
    document.getElementById($('[id$=hdnfldLocationID]').attr('id')).value = "";

    document.getElementById($('[id$=dvAddNew]').attr('id')).style.display = "none";
    document.getElementById("dvCheckList").style.display = "none";
    document.getElementById("dvGroup").style.display = "none";
    document.getElementById("dvGroupUpdate").style.display = "none";
    document.getElementById("dvLocation").style.display = "none";
    document.getElementById('dvCKItemsQB').style.display = "none";
    document.getElementById('dvSaveItems').style.display = "none";

    //LocationFlag = false;
    return false;

}
//debugger;
var lastExecutorMachine = null;
function InsertItems() {
    //document.getElementById($('[id$=txtfilter]').attr('id')).value = "";
    if (lastExecutorMachine != null)
        lastExecutorMachine.abort();
    //    var flagselected = 0;
    //    var table = document.getElementById("tblChkListQB");
    //    for (var i = 1, row; row = table.rows[i]; i++) {
    //        var tempCell = row.cells[0];
    //       
    //        var chk = document.getElementById("tblChkListQB_Active_Status" + i).checked;
    //        if (chk == true) {
    //        flagselected=0;
    //        break;
    //        }
    //        else {
    //            flagselected += 1;
    //        }
    //    }

    //    if (flagselected > 0) {
    //        alert('Please select Items to save.');
    //        return false;
    //    }

    var chkListID = document.getElementById($('[id$=hdnfldChecklstID]').attr('id')).value;
    var groupID = document.getElementById($('[id$=hdnfldGroupID]').attr('id')).value;
    var locationID = document.getElementById($('[id$=hdnfldLocationID]').attr('id')).value;

    var SessionUserID = document.getElementById($('[id$=hdnfldUserID]').attr('id')).value;

    var tableArray = "";
    //var table = document.getElementById("tblChkListQB");
    //for (var i = 1, row; row = table.rows[i]; i++) {
    //        var tempCell = row.cells[0];
    //        tableArray += '{ID:' + tempCell.innerHTML + ',';

    //        var chk = document.getElementById("tblChkListQB_Active_Status" + i).checked;
    //        if (chk == true) {
    //            tableArray += 'Active_Status:1},';
    //        }
    //        else {
    //            tableArray += 'Active_Status:0},';
    //        }
    //    }
    datatable = [];

    var table = document.getElementById("tblChkListQB");
    for (var i = 1, row; row = table.rows[i]; i++) {
        var tempCell = row.cells[0];
        //var tempCell2 = row.cells[3];

        var id = tempCell.innerHTML;
        var chk = document.getElementById("tblChkListQB_Active_Status" + i).checked;
        if (chk == true) {
            datatable.push([id.trim(), 1]);
        }
        else {
            datatable.push([id.trim(), 0]);
        }
    }

    for (var i = 0; i < datatable.length; i++) {
        tableArray += '{ID:' + datatable[i][0] + ',';
        tableArray += 'Active_Status:' + datatable[i][1] + '},';
    }
    var service = Sys.Net.WebServiceProxy.invoke('../../JibeWebServiceInspection.asmx', 'INS_CHKGroupANDLocation', false, { "ID": '', "ParentID": locationID, "Checklist_ID": chkListID, "LocationId": '', "NodeType": 'Item', "Description": tableArray, "Created_By": SessionUserID, "Modified_By": SessionUserID, "ActiveStatus": null }, onSucc_Item_Isert, Onfail1, new Array('FunctionalTree'));
    lastExecutorMachine = service.get_executor();
    return false;

}

/// this update the status of cheklist i.e currently in Draft as published (1) when user click on 'Publish' button.
var lastExecutorMachine = null;
function UpdateStatus() {
    if (lastExecutorMachine != null)
        lastExecutorMachine.abort();

    if (navigator.userAgent.indexOf("Safari") != -1 && navigator.userAgent.indexOf('Chrome') == -1) // Modified by anjali DT :31-May-2016 JIT:9766 || // Chrome has both 'Chrome' and 'Safari' inside userAgent string.Safari has only 'Safari'.
    {
        var chkID = "";
        if (document.getElementById($('[id$=hdnQuerystring]').attr('id')).value != "") {
            chkID = document.getElementById($('[id$=hdnQuerystring]').attr('id')).value;
        }
        else {
            if (document.getElementById($('[id$=hdnfldChecklstID]').attr('id')).value != "") {
                chkID = document.getElementById($('[id$=hdnfldChecklstID]').attr('id')).value;
            }
            else {
                alert('Please select checklist.');
                return false;
            }
        }

    }
    else {
        var table = document.getElementById("__tbl_remark");
        var Count = table.innerHTML.indexOf("Location"); //Added by Anjali DT:6-Jun-2016|| To check ,location is added in checklist or not , if not it will not allow to publish checklist.
        if ((table.rows.length != 1) && (Count != -1)) {
            for (var i = 1, row; row = table.rows[i]; i++) {
                var tempCell = row.cells[0];
                tempCell.click();
                document.getElementById("dvCheckList").style.display = "none";
                document.getElementById("dvGroup").style.display = "none";
                document.getElementById("dvGroupUpdate").style.display = "none";
                document.getElementById("dvLocation").style.display = "none";
                break;

            }
        }
        else {
            document.getElementById('SPremarks').innerHTML = '* Please add Groups and locations .';
            alert('Unable to publish checklist.');
            return false;
        }
    }
    var chkID = "";
    if (document.getElementById($('[id$=hdnQuerystring]').attr('id')).value != "") {
        chkID = document.getElementById($('[id$=hdnQuerystring]').attr('id')).value;
    }
    else {
        chkID = document.getElementById($('[id$=hdnfldChecklstID]').attr('id')).value;
    }

    var SessionUserID = document.getElementById($('[id$=hdnfldUserID]').attr('id')).value;

    var service = Sys.Net.WebServiceProxy.invoke('../../JibeWebServiceInspection.asmx', 'INS_Publish_CheckList', false, { "Checklist_ID": chkID, "Created_By": SessionUserID, "Status": 1 }, onSucc_Publish_Isert, Onfail_Publish_Isert, new Array('FunctionalTree'));

    lastExecutorMachine = service.get_executor();
    return false;
}



function onSucc_Publish_Isert(service) {

    var str = service.split("Alert");
    if (str.length < 2) {

        document.getElementById('FunctionalTree').innerHTML = service;
        alert('Successfully published.');
        document.getElementById($('[id$=btnPublish]').attr('id')).disabled = false;
        var chkID = "";
        if (document.getElementById($('[id$=hdnQuerystring]').attr('id')).value != "") {
            chkID = document.getElementById($('[id$=hdnQuerystring]').attr('id')).value;
        }
        else {
            chkID = document.getElementById($('[id$=hdnfldChecklstID]').attr('id')).value;
        }
        //UpdateDivClick();
        GetCheckList(chkID);
    }
}
function Onfail_Publish_Isert() {
    alert('Onfail_Publish_Isert-Fail');
    //    alert(error.get_message());
    //    alert("Stack Trace: " + error.get_stackTrace());
}



var lastExecutorMachine = null;
function EditClick() {
    if (lastExecutorMachine != null)
        lastExecutorMachine.abort();

    var chkID = "";
    if (document.getElementById($('[id$=hdnQuerystring]').attr('id')).value != "") {
        chkID = document.getElementById($('[id$=hdnQuerystring]').attr('id')).value;
    }
    else {
        chkID = document.getElementById($('[id$=hdnfldChecklstID]').attr('id')).value;
    }
    // var chkID = document.getElementById($('[id$=hdnQuerystring]').attr('id')).value;
    //var chkVersion = document.getElementById($('[id$=hdnCHecklistVersion]').attr('id')).value;
    var SessionUserID = document.getElementById($('[id$=hdnfldUserID]').attr('id')).value;

    var params = { Checklist_ID: chkID };
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "../../JibeWebServiceInspection.asmx/INS_Edit_CheckList",
        data: JSON.stringify(params),
        dataType: "json",
        success: function (Result) {
            Result = Result.d;


            document.getElementById($('[id$=hdnQuerystring]').attr('id')).value = Result;
            //            getScheduleList();

            chkID = "";
            if (document.getElementById($('[id$=hdnQuerystring]').attr('id')).value != "") {
                chkID = document.getElementById($('[id$=hdnQuerystring]').attr('id')).value;
            }
            else {
                chkID = document.getElementById($('[id$=hdnfldChecklstID]').attr('id')).value;
            }



            var service = Sys.Net.WebServiceProxy.invoke('../../JibeWebServiceInspection.asmx', 'INS_Get_CheckList', false, { "Checklist_ID": chkID }, onSucc_Edit_Isert, Onfail_Publish_Isert, new Array('FunctionalTree'));
            lastExecutorMachine = service.get_executor();
        },
        error: function (Result) {
            alert("Error.");
        }
    });


    // document.getElementById($('[id$=hdnQuerystring]').attr('id')).value = "";
    return false;
}

function onSucc_Edit_Isert(service) {


    document.getElementById('FunctionalTree').innerHTML = service;
    //alert('publish Successfully.');

    var status = document.getElementById('hdnStat').value;

    //hdnChildFlag
    var childFlag = document.getElementById('hdnChildFlag').value;
    document.getElementById('dvEditPublish').style.display = 'block';
    if (status == 0) {
        //alert('Publish');
        if (childFlag == 0) {
            document.getElementById($('[id$=btnPublish]').attr('id')).disabled = false;
            document.getElementById($('[id$=btnEdit]').attr('id')).disabled = true;
            //document.getElementById($('[id$=dvUP3]').attr('id')).disabled = false;

        }
        else {
            document.getElementById($('[id$=btnPublish]').attr('id')).disabled = true;
            document.getElementById($('[id$=btnEdit]').attr('id')).disabled = true;

        }
    }
    else {
        //alert('Edit');
        if (childFlag == 0) {
            document.getElementById($('[id$=btnPublish]').attr('id')).disabled = true;
            document.getElementById($('[id$=btnEdit]').attr('id')).disabled = false;
            document.getElementById('dvUP3').style.display = 'block';
            document.getElementById('dvUpdateButton').style.display = 'block';
        }
        else {
            document.getElementById($('[id$=btnPublish]').attr('id')).disabled = true;
            document.getElementById($('[id$=btnEdit]').attr('id')).disabled = true;
            document.getElementById('dvUP3').style.display = 'none';
            document.getElementById('dvUpdateButton').style.display = 'none';
        }
    }



}
var lastExecutorMachineSearch = null;
function filter() {
    try {
        if (lastExecutorMachineSearch == null) {
            var descr = document.getElementById($('[id$=txtfilter]').attr('id')).value;

            if (descr == 'Type to Search')
                descr = "";

            var value = document.getElementById($('[id$=hdnfldChecklstID]').attr('id')).value;

            var valuepID = document.getElementById($('[id$=hdnfldLocationID]').attr('id')).value;

            var flagselected = 0;
            var table = document.getElementById("tblChkListQB");
            if (table != null) {
                for (var i = 1, row; row = table.rows[i]; i++) {
                    var tempCell = row.cells[0];

                    var chk = document.getElementById("tblChkListQB_Active_Status" + i).checked;
                    if (chk == true) {
                        flagselected = 0;
                        break;
                    }
                    else {
                        flagselected += 1;
                    }
                }
            }
            else {
                flagselected += 1;
            }

            if (flagselected == 0) {
                //            alert('Please select Items to save.');
                //            return false;

                InsertItems();
            }
            else {


                var service = Sys.Net.WebServiceProxy.invoke('../../JibeWebServiceInspection.asmx', 'Get_CheckListItetemsQB_filter_Table', false, { "CheckList_ID": value, "Parent_ID": valuepID, "description": descr }, onSucc_BindCHKItemsQB, OnfailQB);
                lastExecutorMachineSearch = service.get_executor();
            }

            //        
        }
        else {
            lastExecutorMachineSearch = null;
        }
        return false;
    }
    catch (ex) {
        return false;
    }
}

function ItemSave() {
    document.getElementById($('[id$=txtfilter]').attr('id')).value = "";
    InsertItems();
    return false;
}

var lastExecutorMachineSearch = null;
function BindGradeOptions() {
    if (lastExecutorMachine != null)
        lastExecutorMachine.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../../JibeWebServiceInspection.asmx', 'BindRadioButtons', false, { "Grade_ID": document.getElementById($('[id$=ddlGradingType]').attr('id')).value }, onSucc_BindGradeOptions, Onfail1);
    lastExecutorMachineSearch = service.get_executor();
    //ddlGradingType
    // alert(document.getElementById($('[id$=ddlGradingType]').attr('id')).value);
    //alert('Call from dropdown change ');
    return false;
}

function onSucc_BindGradeOptions(service) {
    // alert('Success');
    //var ob = document.getElementById($('[id$=rdoGradings]').attr('id')).value;
    document.getElementById('rdoGradings').innerHTML = '';

    var i = 0;
    var objJson = JSON.parse(service);

    $.each(objJson, function () {

        //var rdb = "<asp:ListItem value=" + objJson[i].id + " text=" + objJson[i].optiontext + " />";
        var rdb = "<input id=RadioButton" + i + " type=radio name=City value=" + objJson[i].id + "  disabled=true/><label for=RadioButton" + i + ">" + objJson[i].optiontext + "</label><br />";

        $('#rdoGradings').append(rdb);

        i++;

    });


}

var lastExecutorMachineSearch = null;
function closeAddQuestion() {
    document.getElementById($('[id$=txtCriteria]').attr('id')).value = "";
    document.getElementById($('[id$=ddlCatName]').attr('id')).value = "0";
    document.getElementById($('[id$=ddlGradingType]').attr('id')).value = "0";
    document.getElementById('rdoGradings').innerHTML = "";
    hideModal('dvAddQuestions');
    return false;
}
var lastExecutorMachine = null;
function SaveChecklistQuestion() {
    //string Criteria,string catID,string GradingType,string createdBy
    //This is validation code to restrict special characters on save
    if (specialcharecter() == false) {
        return false;
    }
    if (lastExecutorMachine != null)
        lastExecutorMachine.abort();
    if (document.getElementById($('[id$=txtCriteria]').attr('id')).value.trim() == "") {
        alert('All fields are mandatory.');
        return false;
    }

    if (document.getElementById($('[id$=ddlCatName]').attr('id')).value == "0") {
        alert('All fields are mandatory.');
        return false;
    }

    if (document.getElementById($('[id$=ddlGradingType]').attr('id')).value == "0") {
        alert('All fields are mandatory.');
        return false;
    }
    var service = Sys.Net.WebServiceProxy.invoke('../../JibeWebServiceInspection.asmx', 'SaveChecklistQuestion', false, { "Criteria": document.getElementById($('[id$=txtCriteria]').attr('id')).value, "catID": document.getElementById($('[id$=ddlCatName]').attr('id')).value, "GradingType": document.getElementById($('[id$=ddlGradingType]').attr('id')).value, "createdBy": document.getElementById($('[id$=hdnfldUserID]').attr('id')).value }, onSucc_SaveQuestion, onfail_SaveQuestion);
    lastExecutorMachine = service.get_executor();

    return false;
}

function AddNewQuestionModal() {
    closeAddQuestion();
    var att = document.createAttribute("title");
    att.value = "Add New Question";
    document.getElementById('dvAddQuestions').setAttributeNode(att);

    showModal('dvAddQuestions');
    return false;

}

function onfail_SaveQuestion() {
    closeAddQuestion();
    alert('Question already exists.');
}

//This is validation function to restrict special characters on save
function specialcharecter() {
    var iChars = "!`@#$%^&*()+=[]\\\';/{}|\":<>~_";
    var data = document.getElementById($('[id$=txtCriteria]').attr('id')).value;
    for (var i = 0; i < data.length; i++) {
        if (iChars.indexOf(data.charAt(i)) != -1) {
            //            alert("Your entered special characters. \nThese are not allowed.");
            alert("Special characters are not allowed except (?,space,comma and (-)).");
            //document.getElementById($('[id$=txtCriteria]').attr('id')).value = "";
            return false;
        }
    }
}

function onSucc_SaveQuestion(service) {
    //alert(service);

    closeAddQuestion();
    lastExecutorMachineSearch = null;
    filter();
    document.getElementById($('[id$=txtCriteria]').attr('id')).value = "";
    document.getElementById($('[id$=ddlCatName]').attr('id')).value = "0";
    document.getElementById($('[id$=ddlGradingType]').attr('id')).value = "0";
    document.getElementById('rdoGradings').innerHTML = "";

    var att = document.createAttribute("title");
    att.value = "Add New Question";
    document.getElementById('dvAddQuestions').setAttributeNode(att);

}

function UpdateDivClick() {
    if (document.getElementById('dvShowUpdates').style.display == "block") {
        document.getElementById('dvShowUpdates').style.display = "none"
    }
    else {
        document.getElementById('dvShowUpdates').style.display = "block";
    }


    return false;
}


