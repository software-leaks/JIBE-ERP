var _Vessel_ID = 0, _FunctionCode = null, _SystemID = null, _SystemCode = null, _SubSystemID = null, _SubSystemCode = null, _SystemLocation = null, _SubSystemLocation = null, _Equipment_Type = 1, _DepartmentCode = null, _JobID = null;
var _sort_direction = 0;
var ParentTypeCode = null, SearchText = null, SystemCode = null, SubSystemCode = null, SearchTextLocation = null, VesselId = null, SortBy = null, SortDirection = null, PageNumber = null, PageSize = null, IsFetchCount = null;
var __isResponse = 1, __isResponse1 = 1;
var _retrivedCatSupplier = false;
var _ActiveJobStatus = null, _ActiveItemStatus = null;
var _TempJobsTable = "";
var isSubSystemGeneral = "";
var isSystemRunHour = "";
var _FormType = "SP";
var _MGSSelectedItemCategory = "", _MGSSelectedUnits = "";
var _SystemStatus = ""
var _SubSystemStatus = ""
var _searchjobtext = ""
var selected_department = null;
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
function ddlDeptType_selectinChanged() {
    _SystemID = "0";
    _SubSystemID = "0";
    _Vessel_ID = "0";
    _SystemCode = "0";
    _SubSystemCode = "0";
    selected_department = null;
    document.getElementById('lblSpareCnt').textContent = '';
    Get_Lib_PlannedJobs(_Vessel_ID, _SystemID, _SubSystemID, null, null, null, _ActiveJobStatus, null, null, null, "ID", null, parseInt("1"), parseInt("15"), parseInt("1"), "tblJobs");   // Added by reshma
    Get_Department(_SystemCode);
    Get_Lib_Items(selected_department, _SystemCode, _SubSystemCode, _Vessel_ID, null, null, null, null, _ActiveItemStatus, null, null, null, parseInt("1"), parseInt("15"), 1, parseInt("1"), "tblSpare");
    var VeselID = $('[id$=DDlVessel_List]').val();
    var FunctionCode = $('[id$=DDLDepartment]').val();
    var SearchText = $('[id$=txtSearch]').val();
    var IsActive = $('[id$=ddldisplayRecordType]').val();
    var DeptTypeText;
    var DeptTypeVal = $('[id$=rblDeptType]').find(':checked').val();
    ParentType = "115";
    _FormType = DeptTypeVal;
    var list = document.getElementById($('[id$=rblDeptType]').attr('id'));
    var inputs = list.getElementsByTagName("input");
    var selected;
    for (var i = 0; i < inputs.length; i++) {
        if (inputs[i].type == 'radio' && inputs[i].checked == true) {
            var lbl = inputs[i].parentElement.getElementsByTagName('label');
            DeptTypeText = lbl[0].innerHTML;
            BindDepartments(DeptTypeText, DeptTypeVal);

        }
    }
    document.getElementById($('[id$=DDlVessel_List]').attr('id')).value = "0";

    if (DeptTypeVal == "ST") {

        document.getElementById($('[id$=DDlVessel_List]').attr('id')).disabled = true;
    }
    else {

        document.getElementById($('[id$=DDlVessel_List]').attr('id')).disabled = false;

    }
    if (FunctionCode == null) {

        FunctionCode = "0";
    }
    document.getElementById("dvMachineryDetails").innerHTML = "";



    document.getElementById($('[id$=btnCopyJobs]').attr('id')).disabled = true;
    document.getElementById($('[id$=btnMoveJobs]').attr('id')).disabled = true;
    document.getElementById($('[id$=chkCheckAllJobs]').attr('id')).disabled = true;
    document.getElementById($('[id$=txtSearchJobs]').attr('id')).disabled = true;
    document.getElementById($('[id$=rdJobStatus]').attr('id')).disabled = true;
    document.getElementById($('[id$=txtSearchSpare]').attr('id')).disabled = true;
    document.getElementById($('[id$=rdItemStatus]').attr('id')).disabled = true;




    //Do not enable this here because  this is called inside "BindDepartments" function load_FunctionalTree(VeselID, FunctionCode, SearchText, _Equipment_Type,IsActive,_FormType);

}

function DDlVessel_selectionChange() {

    _SystemID = "0";
    _SubSystemID = "0";
    _Vessel_ID = $('[id$=DDlVessel_List]').val();
    _SystemCode = "0";
    _SubSystemCode = "0";
    selected_department = null;
    document.getElementById('lblSpareCnt').textContent = '';
    Get_Lib_PlannedJobs(_Vessel_ID, _SystemID, _SubSystemID, null, null, null, _ActiveJobStatus, null, null, null, "ID", null, parseInt("1"), parseInt("15"), parseInt("1"), "tblJobs");   // Added by reshma
    Get_Department(_SystemCode);
    Get_Lib_Items(selected_department, _SystemCode, _SubSystemCode, _Vessel_ID, null, null, null, null, _ActiveItemStatus, null, null, null, parseInt("1"), parseInt("15"), 1, parseInt("1"), "tblSpare");
    

    var VeselID = $('[id$=DDlVessel_List]').val();
    var FunctionCode = $('[id$=DDLDepartment]').val();
    var SearchText = $('[id$=txtSearch]').val();
    var IsActive = $('[id$=ddldisplayRecordType]').val();

    ParentType = "115";
    SortBy = 1;
    SortDirection = 1;
    PageNumber = 14;
    PageSize = 1;
    IsFetchCount = 1;

    if (FunctionCode == null) {

        FunctionCode = "0";
    }
    load_FunctionalTree(VeselID, FunctionCode, SearchText, _Equipment_Type, IsActive, _FormType);
    document.getElementById("dvMachineryDetails").innerHTML = "";
    return false;

}


function BindVessel() {

    var Company_ID = document.getElementById($('[id$=hdnCompanyID]').attr('id')).value;

    document.getElementById($('[id$=ddlVessID]').attr('id')).innerHTML = "";
    var params = { CompanyID: Company_ID };
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "../../JIBEWebService.asmx/Get_Vessel",
        data: JSON.stringify(params),
        dataType: "json",
        success: function (Result) {
            Result = Result.d;

            var list = document.getElementById($('[id$=ddlVessID]').attr('id'));

            list.add(new Option("--SELECT--", "0"));
            for (var i = 0; i < Result.length; i++) {
                list.add(new Option(Result[i].Vessel_Name, Result[i].Vessel_ID));
            }


        },
        error: function (Result) {
            alert("Error");
        }
    });



}
function imgFilterTree_Click() {
    var SearchText = $('[id$=txtSearch]').val();
    $("#FunctionalTree").jstree("search", SearchText);

    return false;

}
function ddldisplayRecordType_selectionChange() {

    _SystemID = "0";
    _SubSystemID = "0";
    _Vessel_ID = $('[id$=DDlVessel_List]').val();
    _SystemCode = "0";
    _SubSystemCode = "0";
    selected_department = null;
    document.getElementById('lblSpareCnt').textContent = '';
    Get_Lib_PlannedJobs(_Vessel_ID, _SystemID, _SubSystemID, null, null, null, _ActiveJobStatus, null, null, null, "ID", null, parseInt("1"), parseInt("15"), parseInt("1"), "tblJobs");    // Added by reshma
    Get_Department(_SystemCode);
    Get_Lib_Items(selected_department, _SystemCode, _SubSystemCode, _Vessel_ID, null, null, null, null, _ActiveItemStatus, null, null, null, parseInt("1"), parseInt("15"), 1, parseInt("1"), "tblSpare");
   

    var VeselID = $('[id$=DDlVessel_List]').val();
    var FunctionCode = $('[id$=DDLDepartment]').val();
    var SearchText = $('[id$=txtSearch]').val();
    var IsActive = $('[id$=ddldisplayRecordType]').val();

    ParentType = "115";
    SortBy = 1;
    SortDirection = 1;
    PageNumber = 14;
    PageSize = 1;
    IsFetchCount = 1;
    if (FunctionCode == null) {

        FunctionCode = "0";
    }
    load_FunctionalTree(VeselID, FunctionCode, SearchText, _Equipment_Type, IsActive, _FormType);

    document.getElementById("dvMachineryDetails").innerHTML = "";
}
function DDLDepartment_selectionChange() {

    _SystemID = "0";
    _SubSystemID = "0";
    _Vessel_ID = $('[id$=DDlVessel_List]').val();
    _SystemCode = "0";
    _SubSystemCode = "0";
    selected_department = null;
    document.getElementById('lblSpareCnt').textContent = '';
    Get_Lib_PlannedJobs(_Vessel_ID, _SystemID, _SubSystemID, null, null, null, _ActiveJobStatus, null, null, null, "ID", null, parseInt("1"), parseInt("15"), parseInt("1"), "tblJobs");      // Added by reshma
    Get_Department(_SystemCode);
    Get_Lib_Items(selected_department, _SystemCode, _SubSystemCode, _Vessel_ID, null, null, null, null, _ActiveItemStatus, null, null, null, parseInt("1"), parseInt("15"), 1, parseInt("1"), "tblSpare");
    

    var VeselID = $('[id$=DDlVessel_List]').val();
    var FunctionCode = $('[id$=DDLDepartment]').val();
    var SearchText = $('[id$=txtSearch]').val();
    var IsActive = $('[id$=ddldisplayRecordType]').val();

    ParentType = "115";
    SortBy = 1;
    SortDirection = 1;
    PageNumber = 14;
    PageSize = 1;
    IsFetchCount = 1;
    if (FunctionCode == null) {

        FunctionCode = "0";
    }
    load_FunctionalTree(VeselID, FunctionCode, SearchText, _Equipment_Type, IsActive, _FormType);
    document.getElementById("dvMachineryDetails").innerHTML = "";
    return false;

}
function refreshPage() {


    var VeselID = document.getElementById($('[id$=DDlVessel_List]').attr('id')).value = "0";             //Reshma : In JIT :11216
    var FunctionCode = document.getElementById($('[id$=DDLDepartment]').attr('id')).value = "0";
    var SearchText = document.getElementById($('[id$=txtSearch]').attr('id')).value = "";
    var IsActive = $('[id$=ddldisplayRecordType]').val();
    var DeptTypeText;
    var DeptTypeVal = $('[id$=rblDeptType]').find(':checked').val();
    load_FunctionalTree(VeselID, FunctionCode, SearchText, _Equipment_Type, IsActive, _FormType);
    return false;

}

 
function initTab() {
    // BindDeptType();
    //BindFunction("115", "");
    ddlDeptType_selectinChanged();
    $("#tabs").tabs();
    $('#tabs').on('tabsactivate', function (evt, ui) {
        var _newtab = ui.newPanel.selector;



        if (_Vessel_ID.toString() != "0") {

            if (_newtab == "#joblibrarytab") {
                document.getElementById($('[id$=txtSearchJobs]').attr('id')).value = "";
                document.getElementById('joblibrary').innerHTML = "";
                document.getElementById('ctl00_MainContent_ucAsyncPager1_hdfcountTotalRec').value = 0;
                Asyncpager_BuildPager(BindNextPageJobs, 'ctl00_MainContent_ucAsyncPager1');
                document.getElementById("jobtabhead").style.display = "block";
                document.getElementById("joblibrary").style.display = "block";
                document.getElementById("jobspager").style.display = "block";

                Get_Lib_PlannedJobs(_Vessel_ID, _SystemID, _SubSystemID, null, null, null, _ActiveJobStatus, null, null, null, "ID", null, parseInt("1"), parseInt("15"), parseInt("1"), "tblJobs");     // Added by reshma
                document.getElementById("ctl00_MainContent_ucAsyncPager1_hdfBindMethodname").value = "BindNextPageJobs";
            }
            else if (_newtab == "#spareconstab") {
                //Get_SpareConsumption(_Vessel_ID, _SystemID, _SubSystemID);

                document.getElementById($('[id$=txtSearchSpare]').attr('id')).value = "";

                document.getElementById('sparecons').innerHTML = "";
                document.getElementById('ctl00_MainContent_ucAsyncPager2_hdfcountTotalRec').value = 0;
                Asyncpager_BuildPager(BindNextPageItems, 'ctl00_MainContent_ucAsyncPager2');
                document.getElementById("sparecontabhead").style.display = "block";
                document.getElementById("sparecons").style.display = "block";
                document.getElementById("sparepager").style.display = "block";
                // Get_Department(_SystemCode);
                Get_Lib_Items(selected_department, _SystemCode, _SubSystemCode, _Vessel_ID, null, null, null, null, _ActiveItemStatus, null, null, null, parseInt("1"), parseInt("15"), 1, parseInt("1"), "tblSpare");
                document.getElementById("ctl00_MainContent_ucAsyncPager2_hdfBindMethodname").value = "BindNextPageItems";
            }

            
        }
        if (_Vessel_ID.toString() == "0") {

            if (_newtab == "#joblibrarytab") {
                document.getElementById($('[id$=txtSearchJobs]').attr('id')).value = "";
                document.getElementById('joblibrary').innerHTML = "";
                document.getElementById('ctl00_MainContent_ucAsyncPager1_hdfcountTotalRec').value = 0;
                Asyncpager_BuildPager(BindNextPageJobs, 'ctl00_MainContent_ucAsyncPager1');
                document.getElementById("jobtabhead").style.display = "block";
                document.getElementById("joblibrary").style.display = "block";
                document.getElementById("jobspager").style.display = "block";
                Get_Lib_PlannedJobs(_Vessel_ID, _SystemID, _SubSystemID, null, null, null, _ActiveJobStatus, null, null, null, "ID", null, parseInt("1"), parseInt("15"), parseInt("1"), "tblJobs");    // Added by reshma
                document.getElementById("ctl00_MainContent_ucAsyncPager1_hdfBindMethodname").value = "BindNextPageJobs";
            }
            else if (_newtab == "#spareconstab") {
                //Get_SpareConsumption(_Vessel_ID, _SystemID, _SubSystemID);

                document.getElementById($('[id$=txtSearchSpare]').attr('id')).value = "";
                document.getElementById('sparecons').innerHTML = "";
                document.getElementById('ctl00_MainContent_ucAsyncPager2_hdfcountTotalRec').value = 0;
                Asyncpager_BuildPager(BindNextPageItems, 'ctl00_MainContent_ucAsyncPager2');
                document.getElementById("sparecontabhead").style.display = "block";
                document.getElementById("sparecons").style.display = "block";
                document.getElementById("sparepager").style.display = "block";
                // Get_Department(_SystemCode);
                Get_Lib_Items(selected_department, _SystemCode, _SubSystemCode, _Vessel_ID, null, null, null, null, _ActiveItemStatus, null, null, null, parseInt("1"), parseInt("15"), 1, parseInt("1"), "tblSpare");
                var Company_Code = document.getElementById($('[id$=hdnCompanyID]').attr('id')).value;
                onGetAutoRequestCriticalSpare(Company_Code);
                document.getElementById("ctl00_MainContent_ucAsyncPager2_hdfBindMethodname").value = "BindNextPageItems";
            }

           
        }


    });

    GetRank();

    return false;
}


var isloaded = false;

function load_FunctionalTree(vessel_id, function_code, searchtext, Equipment_Type, isActive, FormType) {


    // clear_Equipment_statistics();
    if (isloaded == true) {

        $("#FunctionalTree").jstree("destroy");
        isloaded = true;

        $("#btnAddSystem").remove();
        $("#btnEditSubSystem").remove();
        $("#btnAddJob").remove();
        $("#btnEditSystem").remove();
        $("#btnAddSubSystem").remove();
        $("#btnAddSpareConsumption").remove();
        $("#btnDeleteSystem").remove();
        $("#btnDeleteSubSystem").remove();
        $("#btnRestoreSystem").remove();
        $("#btnRestoreSubSystem").remove();
    }
    //    if (vessel_id.toString() != "0") {

    addInputTo($("#dvEditDetails"), "btnAddSystem", "Add System", "onAddSystem()");
    addInputTo($("#dvEditDetails"), "btnEditSubSystem", "Edit SubSystem", "onEditSubSystem()");
    addInputTo($("#dvEditDetails"), "btnAddJob", "Add Job", "onAddJob()");
    addInputTo($("#dvEditDetails"), "btnEditSystem", "Edit System", "onEditSystem()");
    addInputTo($("#dvEditDetails"), "btnAddSubSystem", "Add SubSystem", "onAddSubSystem()");
    addInputTo($("#dvEditDetails"), "btnAddSpareConsumption", "Add Spare/Store Item", "onAddSpare()");
    addInputTo($("#dvEditDetails"), "btnDeleteSystem", "Delete System", "onDeleteSystem()");
    addInputTo($("#dvEditDetails"), "btnDeleteSubSystem", "Delete SubSystem", "onDeleteSubSystem()");
    addInputTo($("#dvEditDetails"), "btnRestoreSystem", "Restore System", "onRestoreSystem()");
    addInputTo($("#dvEditDetails"), "btnRestoreSubSystem", "Restore SubSystem", "onRestoreSubSystem()");





    $("#btnEditSubSystem").hide();
    $("#btnAddJob").hide();
    $("#btnEditSystem").hide();
    $("#btnAddSubSystem").hide();
    $("#btnAddSpareConsumption").hide();
    $("#btnAddSystem").hide();
    $("#btnDeleteSystem").hide();
    $("#btnDeleteSubSystem").hide();
    $("#btnRestoreSystem").hide();
    $("#btnRestoreSubSystem").hide();

    var r = [];

    $("#FunctionalTree").jstree({
        'core': {
            'data': {
                'type': "POST",
                "async": "true",
                'contentType': "application/json; charset=utf-8",
                'url': "../../JIBEWebService.asmx/Get_Functional_Tree_Data_ManageSystem",
                'data': "{}",
                'dataType': 'JSON',
                'data': function (node) {


                    if (node.id.toString() != '#')


                        return '{"id":' + node.id + ',"vesselid":' + vessel_id + ',"Equipment_Type":' + Equipment_Type.toString() + ',"function_code":"' + function_code + '","searchText":null,"IsActive":' + parseInt(isActive) + ',"Form_Type":"' + FormType.toString() + '"}'



                    else

                        return '{"id":null,"vesselid":' + vessel_id + ',"Equipment_Type":' + Equipment_Type.toString() + ',"function_code":"' + function_code + '","searchText":null,"IsActive":' + parseInt(isActive) + ',"Form_Type":"' + FormType.toString() + '"}'



                },
                'success': function (retvel) {
                    isloaded = true;
                    //                    if (isActive == "0") {
                    //                        $("#FunctionalTree").css("color", "red");
                    //                    }
                    //                    else {
                    //                        $("#FunctionalTree").css("color", "black");
                    //                    }
                    return retvel.d;

                },
                'error': function (Result) {
                    alert(Result.responseJSON.Message);
                }

            }
        },
        "themes": {

            "theme": "classic",

            "dots": true,

            "icons": true

        },
        "search": {

            "case_insensitive": false


        },

        "plugins": ["themes", "core", "ui", "types", "search", "sort", "wholerow"]



    });




    $("#FunctionalTree").bind('select_node.jstree', function (e) {

        var prm = $("#FunctionalTree").jstree('get_selected').toString().split(',');

        _Vessel_ID = vessel_id;
        _SubSystemID = null;
        _SystemID = null;
        document.getElementById("ctl00_MainContent_hdnVesselID").value = _Vessel_ID;

        isActive = $('[id$=ddldisplayRecordType]').val();
        if (prm.length > 0) {
            if (prm.length == 5) {
                _Vessel_ID = vessel_id;
                _SystemID = prm[0];
                _SubSystemID = prm[1];
                _SystemCode = prm[2];
                _SubSystemCode = prm[3];
                _FunctionCode = prm[4];
                //selected_department = prm[5];
                // _SubSystemStatus = prm[5];


                GetSubSystemStatus(_SubSystemID);
                if ($('.jstree-clicked').length == 1) {
                    isSubSystemGeneral = $('.jstree-clicked').text();
                }

                document.getElementById("ctl00_MainContent_hdnSysID").value = prm[0];
                document.getElementById("ctl00_MainContent_hdnSubSysID").value = prm[1];
                document.getElementById("ctl00_MainContent_hdnSysCode").value = prm[2];
                document.getElementById("ctl00_MainContent_hdnSubSysCode").value = prm[3];
                var hdnTreeNode = document.getElementById("ctl00_MainContent_hdnTreeNodeTypeSelected");
                hdnTreeNode.value = "SubSystem"

                document.getElementById("joblibrarytab").style.display = "block";
                Get_SubMachinery_Details(_Vessel_ID, _SystemID, _SubSystemID);
                document.getElementById($('[id$=btnEditSubSystem]').attr('id')).className = "awesome";
                document.getElementById($('[id$=btnAddJob]').attr('id')).className = "awesome";
                document.getElementById($('[id$=btnAddSpareConsumption]').attr('id')).className = "awesome";
                document.getElementById($('[id$=btnRestoreSubSystem]').attr('id')).className = "awesome";
                document.getElementById($('[id$=btnDeleteSubSystem]').attr('id')).className = "awesome";



                if ($('[id$=rblDeptType]').find(':checked').val() == "ST") {

                    document.getElementById($('[id$=btnCopyJobs]').attr('id')).disabled = true;
                    document.getElementById($('[id$=btnMoveJobs]').attr('id')).disabled = true;
                    document.getElementById($('[id$=chkCheckAllJobs]').attr('id')).disabled = true;
                    document.getElementById($('[id$=txtSearchJobs]').attr('id')).disabled = true;
                    document.getElementById($('[id$=rdJobStatus]').attr('id')).disabled = true;

                }
                else {
                    document.getElementById($('[id$=btnCopyJobs]').attr('id')).disabled = false;
                    document.getElementById($('[id$=btnMoveJobs]').attr('id')).disabled = false;
                    document.getElementById($('[id$=chkCheckAllJobs]').attr('id')).disabled = false;
                    document.getElementById($('[id$=txtSearchJobs]').attr('id')).disabled = false;
                    document.getElementById($('[id$=rdJobStatus]').attr('id')).disabled = false;

                }

            }
            else if (prm.length == 3) {
                _Vessel_ID = vessel_id; ;
                _SystemID = prm[0];
                _SystemCode = prm[1];
                _FunctionCode = prm[2];
                // selected_department = prm[3];
                _SubSystemID = null;
                _SubSystemCode = null;
                GetSystemStatus(_SystemID);
                document.getElementById("ctl00_MainContent_hdnSysID").value = prm[0];
                document.getElementById("ctl00_MainContent_hdnSysCode").value = prm[1];
                document.getElementById("ctl00_MainContent_hdnSubSysID").value = null;
                document.getElementById("ctl00_MainContent_hdnSubSysCode").value = null;
                var hdnTreeNode = document.getElementById("ctl00_MainContent_hdnTreeNodeTypeSelected");
                hdnTreeNode.value = "System";

                Get_Machinery_Details(_Vessel_ID, _SystemID, _SubSystemID);
                document.getElementById($('[id$=btnEditSystem]').attr('id')).className = "awesome";
                document.getElementById($('[id$=btnAddSubSystem]').attr('id')).className = "awesome";
                document.getElementById($('[id$=btnAddSpareConsumption]').attr('id')).className = "awesome";
                document.getElementById($('[id$=btnRestoreSystem]').attr('id')).className = "awesome";
                document.getElementById($('[id$=btnDeleteSystem]').attr('id')).className = "awesome";


                if ($('[id$=rblDeptType]').find(':checked').val() == "ST") {
                    $("#btnAddJob").hide();
                    document.getElementById($('[id$=btnCopyJobs]').attr('id')).disabled = true;
                    document.getElementById($('[id$=btnMoveJobs]').attr('id')).disabled = true;
                    document.getElementById($('[id$=chkCheckAllJobs]').attr('id')).disabled = true;
                    document.getElementById($('[id$=txtSearchJobs]').attr('id')).disabled = true;
                    document.getElementById($('[id$=rdJobStatus]').attr('id')).disabled = true;

                }
                else {
                    document.getElementById($('[id$=btnCopyJobs]').attr('id')).disabled = false;
                    document.getElementById($('[id$=btnMoveJobs]').attr('id')).disabled = false;
                    document.getElementById($('[id$=chkCheckAllJobs]').attr('id')).disabled = false;
                    document.getElementById($('[id$=txtSearchJobs]').attr('id')).disabled = false;
                    document.getElementById($('[id$=rdJobStatus]').attr('id')).disabled = false;

                }

            }
            else if (prm.length == 1) {
                _Vessel_ID = vessel_id; ;
                _FunctionCode = prm[0];

                _SystemID = null;
                _SystemCode = null;
                _SubSystemID = null;
                _SubSystemCode = null;
                document.getElementById("ctl00_MainContent_hdnFunctionCode").value = prm[0];
                document.getElementById('lblSpareCnt').textContent = '0';
                document.getElementById('lblJobCnt').textContent = '0';
                document.getElementById("ctl00_MainContent_hdnSysID").value = null;
                document.getElementById("ctl00_MainContent_hdnSysCode").value = null;
                document.getElementById("ctl00_MainContent_hdnSubSysID").value = null;
                document.getElementById("ctl00_MainContent_hdnSubSysCode").value = null;
                var hdnTreeNode = document.getElementById("ctl00_MainContent_hdnTreeNodeTypeSelected");
                hdnTreeNode.value = "Function";
                $("#btnEditSubSystem").hide();
                $("#btnAddJob").hide();
                $("#btnEditSystem").hide();
                $("#btnAddSubSystem").hide();
                $("#btnAddSpareConsumption").hide();
                $("#btnDeleteSystem").hide();
                $("#btnDeleteSubSystem").hide();
                $("#btnRestoreSystem").hide();
                $("#btnRestoreSubSystem").hide();
                if (isActive == 1) {
                    $("#btnAddSystem").show();
                }
                if ($('[id$=rblDeptType]').find(':checked').val() == "ST") {
                    $("#btnAddJob").hide();
                    document.getElementById($('[id$=btnCopyJobs]').attr('id')).disabled = true;
                    document.getElementById($('[id$=btnMoveJobs]').attr('id')).disabled = true;
                    document.getElementById($('[id$=chkCheckAllJobs]').attr('id')).disabled = true;
                    document.getElementById($('[id$=txtSearchJobs]').attr('id')).disabled = true;
                    document.getElementById($('[id$=rdJobStatus]').attr('id')).disabled = true;

                }
                else {
                    document.getElementById($('[id$=btnCopyJobs]').attr('id')).disabled = false;
                    document.getElementById($('[id$=btnMoveJobs]').attr('id')).disabled = false;
                    document.getElementById($('[id$=chkCheckAllJobs]').attr('id')).disabled = false;
                    document.getElementById($('[id$=txtSearchJobs]').attr('id')).disabled = false;
                    document.getElementById($('[id$=rdJobStatus]').attr('id')).disabled = false;

                }
                document.getElementById($('[id$=btnAddSystem]').attr('id')).className = "awesome";
            }


            $("#tabs").tabs('option', 'active', 0);

            document.getElementById("jobtabhead").style.display = "block";
            document.getElementById("joblibrary").style.display = "block";
            document.getElementById("jobspager").style.display = "block";
            document.getElementById('joblibrary').innerHTML = "";
            document.getElementById('ctl00_MainContent_ucAsyncPager1_hdfcountTotalRec').value = 0;
            Asyncpager_BuildPager(BindNextPageJobs, 'ctl00_MainContent_ucAsyncPager1');

            Get_Lib_PlannedJobs(_Vessel_ID, _SystemID, _SubSystemID, null, null, null, _ActiveJobStatus, null, null, null, "ID", null, parseInt("1"), parseInt("15"), parseInt("1"), "tblJobs");    // Added by reshma : add parameter RA_Mandatory & RA_Approval 
            Get_Lib_PlannedJobsCount(_Vessel_ID, _SystemID, _SubSystemID, null, null, null, _ActiveJobStatus, null, null, null, "ID", null, parseInt("1"), parseInt("15"), parseInt("1"), "tblJobs");     // Added by reshma : add parameter RA_Mandatory & RA_Approval 
            Get_Department(_SystemCode);

            //Get_AllJobs();
            GetCatalogList();
            document.getElementById($('[id$=txtSearchSpare]').attr('id')).disabled = false;
            document.getElementById($('[id$=rdItemStatus]').attr('id')).disabled = false;



        }


    });


    //    }


}



function onMeatCheck(ItemID, objRef) {

    var UserID = document.getElementById($('[id$=hdnUserID]').attr('id')).value;
    var isMeat = 0;
    var row = objRef.parentNode.parentNode;
    var mainTable = document.getElementById("tblSpare");


    if (objRef.checked) {
        isMeat = 1;
    }
    else {
        isMeat = 0;
    }


    Update_Lib_MeatItems(ItemID, isMeat, UserID);
    Get_Department(_SystemCode);
    Get_Lib_Items(selected_department, _SystemCode, _SubSystemCode, _Vessel_ID, null, null, null, null, _ActiveItemStatus, null, null, null, parseInt("1"), parseInt("15"), 1, parseInt("1"), "tblSpare");


    for (var i = 1, row; row = mainTable.rows[i]; i++) {

        var jobid = "tblSpare_Meat" + i;

        var chkItem = document.getElementById(jobid);

        chkItem.disabled = false;



    }
    return false;
}



function onSlopChestCheck(ItemID, objRef) {

    var UserID = document.getElementById($('[id$=hdnUserID]').attr('id')).value;
    var isMeat = 0;
    var row = objRef.parentNode.parentNode.rowIndex;
    var mainTable = document.getElementById("tblSpare");


    if (objRef.checked) {
        isSlopChest = 1;
    }
    else {
        isSlopChest = 0;
    }


    Update_Lib_SlopChestItems(String(ItemID), isSlopChest, UserID);
    Get_Department(_SystemCode);
    Get_Lib_Items(selected_department, _SystemCode, _SubSystemCode, _Vessel_ID, null, null, null, null, _ActiveItemStatus, null, null, null, parseInt("1"), parseInt("15"), 1, parseInt("1"), "tblSpare");

    for (var i = 1, row; row = mainTable.rows[i]; i++) {

        var jobid = "tblSpare_IsSlopchest" + i;

        var chkItem = document.getElementById(jobid);

        chkItem.disabled = false;



    }
    return false;
}
function onSearchJobs() {

    var searchtext = document.getElementById($('[id$=txtSearchJobs]').attr('id')).value;
    if (searchtext == "") {

        searchtext = null;
    }
    Get_Lib_PlannedJobs(_Vessel_ID, _SystemID, _SubSystemID, null, null, null, _ActiveJobStatus, searchtext, null, null, "ID", null, parseInt("1"), parseInt("15"), parseInt("1"), "tblJobs");    // Added by reshma : add parameter RA_Mandatory & RA_Approval 

    return false;
}
function onSearchSpare() {
    var searchtext = document.getElementById($('[id$=txtSearchSpare]').attr('id')).value;
    if (searchtext == "") {

        searchtext = null;
    }
    Get_Department(_SystemCode);
    Get_Lib_Items(selected_department, _SystemCode, _SubSystemCode, _Vessel_ID, null, null, null, null, _ActiveItemStatus, searchtext, null, null, parseInt("1"), parseInt("15"), 1, parseInt("1"), "tblSpare");

    return false;

}
function GetSystemStatus(SystemID) {

    var params = { systemid: parseInt(SystemID) };

    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "../../JIBEWebService.asmx/PMS_Get_SystemStatus",
        data: JSON.stringify(params),
        dataType: "json",
        success: function (Result) {
            Result = Result.d;
            _SystemStatus = Result;
            if (_SystemStatus == 1) {

                $("#btnAddJob").hide();
                $("#btnAddSystem").hide();
                $("#btnEditSystem").show();
                $("#btnAddSubSystem").show();
                $("#btnEditSubSystem").hide();
                $("#btnAddSpareConsumption").hide();
                $("#btnDeleteSystem").show();
                $("#btnDeleteSubSystem").hide();
                $("#btnRestoreSystem").hide();
                $("#btnRestoreSubSystem").hide();

            }
            else if (_SystemStatus == 0) {


                $("#btnAddJob").hide();
                $("#btnAddSystem").hide();
                $("#btnEditSystem").hide();
                $("#btnAddSubSystem").hide();
                $("#btnEditSubSystem").hide();
                $("#btnAddSpareConsumption").hide();
                $("#btnDeleteSystem").hide();
                $("#btnDeleteSubSystem").hide();
                $("#btnRestoreSystem").show();
                $("#btnRestoreSubSystem").hide();


            }
            return Result;

        },
        error: function (Result) {
            alert("Error");
        }
    });

}

function GetSubSystemStatus(SubSystemID) {


    var params = { subsystemid: parseInt(SubSystemID) };

    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "../../JIBEWebService.asmx/PMS_Get_SubSystemStatus",
        data: JSON.stringify(params),
        dataType: "json",
        success: function (Result) {
            Result = Result.d;
            _SubSystemStatus = Result;
            if (_SubSystemStatus == 1) {

                if ($('[id$=rblDeptType]').find(':checked').val() == "ST") {
                    $("#btnAddJob").hide();
                    document.getElementById($('[id$=btnCopyJobs]').attr('id')).disabled = true;
                    document.getElementById($('[id$=btnMoveJobs]').attr('id')).disabled = true;
                    document.getElementById($('[id$=chkCheckAllJobs]').attr('id')).disabled = true;
                    document.getElementById($('[id$=txtSearchJobs]').attr('id')).disabled = true;
                    document.getElementById($('[id$=rdJobStatus]').attr('id')).disabled = true;

                }
                else {
                    $("#btnAddJob").show();
                    document.getElementById($('[id$=btnCopyJobs]').attr('id')).disabled = false;
                    document.getElementById($('[id$=btnMoveJobs]').attr('id')).disabled = false;
                    document.getElementById($('[id$=chkCheckAllJobs]').attr('id')).disabled = false;
                    document.getElementById($('[id$=txtSearchJobs]').attr('id')).disabled = false;
                    document.getElementById($('[id$=rdJobStatus]').attr('id')).disabled = false;
                }

                $("#btnAddSpareConsumption").show();
                $("#btnEditSystem").hide();
                $("#btnAddSystem").hide();
                $("#btnAddSubSystem").hide();
                $("#btnEditSubSystem").show();
                $("#btnDeleteSystem").hide();
                $("#btnDeleteSubSystem").show();
                $("#btnRestoreSystem").hide();
                $("#btnRestoreSubSystem").hide();
            }
            else if (_SubSystemStatus == 0) {

                if ($('[id$=rblDeptType]').find(':checked').val() == "ST") {

                    document.getElementById($('[id$=btnCopyJobs]').attr('id')).disabled = true;
                    document.getElementById($('[id$=btnMoveJobs]').attr('id')).disabled = true;
                    document.getElementById($('[id$=chkCheckAllJobs]').attr('id')).disabled = true;
                    document.getElementById($('[id$=txtSearchJobs]').attr('id')).disabled = true;
                    document.getElementById($('[id$=rdJobStatus]').attr('id')).disabled = true;
                }
                else {
                    document.getElementById($('[id$=btnCopyJobs]').attr('id')).disabled = false;
                    document.getElementById($('[id$=btnMoveJobs]').attr('id')).disabled = false;
                    document.getElementById($('[id$=chkCheckAllJobs]').attr('id')).disabled = false;
                    document.getElementById($('[id$=txtSearchJobs]').attr('id')).disabled = false;
                    document.getElementById($('[id$=rdJobStatus]').attr('id')).disabled = false;
                }

                $("#btnAddJob").hide();
                $("#btnAddSpareConsumption").hide();
                $("#btnEditSystem").hide();
                $("#btnAddSystem").hide();
                $("#btnAddSubSystem").hide();
                $("#btnEditSubSystem").hide();
                $("#btnDeleteSystem").hide();
                $("#btnDeleteSubSystem").hide();
                $("#btnRestoreSystem").hide();
                $("#btnRestoreSubSystem").show();
            }
            return Result;

        },
        error: function (Result) {
            alert("Error");
        }
    });


}

//changes done by reshma : add "Is_RAMandatory" : Is_RAMandatory, "Is_RAApproval": Is_RAApproval 
var lastExecutorLibPlannedJobs1 = null;
function Get_AllJobs() {



    if (lastExecutorLibPlannedJobs1 != null)
        lastExecutorLibPlannedJobs1.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../../JibeWebService.asmx', 'Get_Lib_Planned_JobsID_ManageSystem', false, { "Vessel_ID": _Vessel_ID, "SystemID": _SystemID, "SubSystemID": _SubSystemID, "DeptID": null, "rankid": null, "jobtitle": null, "isactive": _ActiveJobStatus, "searchtext": _searchjobtext, "Is_RAMandatory": null, "Is_RAApproval": null, "sortby": "ID", "sortdirection": null, "pagenumber": 1, "pagesize": null, "isfetchcount": 1, "TableID": "tblAllJob" }, onSuccessBindOfAllJobs, Onfail, new Array('alljobs'));
    lastExecutorLibPlannedJobs1 = service.get_executor();
}

function onSuccessBindOfAllJobs(retval, ev) {

    document.getElementById('alljobs').innerHTML = retval.split("~totalrecordfound~")[0];
    //var temp = document.getElementById('tblJob1');

}
function AsyncLoadJobDataOnSort(sort_column) {

    var searchtextsjobs = document.getElementById($('[id$=txtSearchJobs]').attr('id')).value;
    var pagesizeJob = document.getElementById('ctl00_MainContent_ucAsyncPager1_hdfPageSize').value;
    var pageindexJob = document.getElementById('ctl00_MainContent_ucAsyncPager1_hdfPageIndex').value;


    if (searchtextsjobs == "") {

        searchtextsjobs = null;
    }

    if (_sort_direction == 0)
        _sort_direction = 1;
    else
        _sort_direction = 0;

    Get_Lib_PlannedJobs(_Vessel_ID, _SystemID, _SubSystemID, null, null, null, _ActiveJobStatus, searchtextsjobs, null, null, sort_column, _sort_direction, parseInt(pageindexJob), parseInt(pagesizeJob), parseInt("1"), "tblJobs");   // Added by reshma : add parameter RA_Mandatory & RA_Approval 
    Get_Department(_SystemCode);


}

function AsyncLoadSpareDataOnSort(sort_column) {

    //document.getElementById(_clt_id_page + 'hdfSortColumn').value = sort_column;
    var searchtextspr = document.getElementById($('[id$=txtSearchSpare]').attr('id')).value;

    var pagesizeItem = document.getElementById('ctl00_MainContent_ucAsyncPager2_hdfPageSize').value;
    var pageindexItem = document.getElementById('ctl00_MainContent_ucAsyncPager2_hdfPageIndex').value;
    if (searchtextspr == "") {

        searchtextspr = null;
    }


    if (_sort_direction == 0)
        _sort_direction = 1;
    else
        _sort_direction = 0;


    Get_Lib_Items(selected_department, _SystemCode, _SubSystemCode, _Vessel_ID, null, null, null, null, _ActiveItemStatus, searchtextspr, sort_column, _sort_direction, parseInt(pageindexItem), parseInt(pagesizeItem), 1, parseInt("1"), "tblSpare");
    Get_Department(_SystemCode);

}
function AsyncLoadLocationDataOnSort(sort_column) {

    var searchtextLocation = document.getElementById($('[id$=txtSearchLocation]').attr('id')).value;


    if (searchtextLocation == "") {

        searchtextLocation = null;
    }


    if (_sort_direction == 0)
        _sort_direction = 1;
    else
        _sort_direction = 0;

    Get_Lib_Locations(_SystemCode, _SubSystemCode, searchtextLocation, _Vessel_ID, sort_column, _sort_direction, null, null, null, "tblLocation");
}

function onCopyFromSystem() {
    var params = { systemid: document.getElementById("ctl00_MainContent_hdnSysID").value };
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "../../JIBEWebService.asmx/Get_Catalog_List",
        data: JSON.stringify(params),
        dataType: "json",
        success: function (Result) {
            Result = Result.d;

            if (Result.length > 0) {



                document.getElementById($('[id$=txtSubCatalogueParticulars]').attr('id')).value = Result[0].SYS_PARTICULAR;
                document.getElementById($('[id$=txtSubCatModel]').attr('id')).value = Result[0].MODULE_TYPE;
                document.getElementById($('[id$=txtSubCatSerialNo]').attr('id')).value = Result[0].SERIAL_NUMBER;
                document.getElementById($('[id$=ddlSubCatMaker]').attr('id')).value = Result[0].MAKER;



            }

        },
        error: function (Result) {
            alert("Error");
        }
    });

    return false;

}
//Reshma : When click on Add system hide function & vessel field for store & display function & vessel field for spare and repair
function onAddSystem() {

    clearCatalogFields();
    GetNextSystemCode();
    document.getElementById("ctl00_MainContent_hdnCatalogOperationMode").value = "ADD";
    document.getElementById("ctl00_MainContent_hdnSubCatalogOperationMode").value = null;
    document.getElementById("ctl00_MainContent_ddlCatalogFunction").disabled = false;
    document.getElementById("ctl00_MainContent_txtCatalogueCode").disabled = true;
    document.getElementById('ctl00_MainContent_chkSubSysAdd').checked = true;
    document.getElementById('ctl00_MainContent_chkSubSysAdd').disabled = false;              //Reshma : Added for General Sub System checkbox in JIT : 11214

    $("#divAddSystem").prop('title', 'Add System');
    showModal('divAddSystem', false);
    document.getElementById("ctl00_MainContent_ddlCatalogFunction").value = _FunctionCode;
    BindVessel();
    BindDepartment();
    BindAccountCode();
    BindSystemFunction(ParentType, "");
    BindCatSupplier();
    if ($('[id$=rblDeptType]').find(':checked').val() != "SP") {
        document.getElementById($('[id$=txtCatalogueSetInstalled]').attr('id')).disabled = true;
        document.getElementById($('[id$=ImgAddLocation]').attr('id')).disabled = true;

    }
    else {
        document.getElementById($('[id$=txtCatalogueSetInstalled]').attr('id')).disabled = false;
        document.getElementById($('[id$=ImgAddLocation]').attr('id')).disabled = true;


    }
    if ($('[id$=rblDeptType]').find(':checked').val() == "ST") {                                       //Reshma : JIT : 11481
        //        document.getElementById("ctl00_MainContent_ddlCatalogFunction").disabled = true;
        //        document.getElementById($('[id$=ddlVessID]').attr('id')).disabled = true;

        document.getElementById('ctl00_MainContent_lblFunction').style.display = 'none';
        document.getElementById('ctl00_MainContent_lblVessel').style.display = 'none';
        document.getElementById('ctl00_MainContent_lblFuncMandtory').style.display = 'none';
        document.getElementById('ctl00_MainContent_lblVeslMandtory').style.display = 'none';            
        document.getElementById('ctl00_MainContent_ddlCatalogFunction').style.display = 'none';
        document.getElementById('ctl00_MainContent_ddlVessID').style.display = 'none';
        document.getElementById('ctl00_MainContent_lblFunColon').style.display = 'none';
        document.getElementById('ctl00_MainContent_lblVesselcoln').style.display = 'none';
    }
    else {

        document.getElementById('ctl00_MainContent_lblFunction').style.display = 'inherit';
       

        document.getElementById('ctl00_MainContent_lblVessel').style.display = 'inherit';
        document.getElementById('ctl00_MainContent_lblFuncMandtory').style.display = 'inherit';
       
        document.getElementById('ctl00_MainContent_lblVeslMandtory').style.display = 'inherit';
        document.getElementById('ctl00_MainContent_ddlCatalogFunction').style.display = 'inherit';
        document.getElementById('ctl00_MainContent_ddlVessID').style.display = 'inherit';
        document.getElementById('ctl00_MainContent_lblFunColon').style.display = 'inherit';
        document.getElementById('ctl00_MainContent_lblVesselcoln').style.display = 'inherit';
    }
    return false;
}
function onEditSystem() {

    clearCatalogFields();
    document.getElementById("ctl00_MainContent_hdnCatalogOperationMode").value = "EDIT";
    document.getElementById("ctl00_MainContent_hdnSubCatalogOperationMode").value = null;
    document.getElementById('ctl00_MainContent_chkSubSysAdd').disabled = true;                      //Reshma : Added for General Sub System checkbox in JIT : 11214

    // To enable the function drop down in edit mode for 'Spares' and 'Repairs'.|| To able to change function name when user edit system.
    if ($('[id$=rblDeptType]').find(':checked').val() == "ST") {
        document.getElementById("ctl00_MainContent_ddlCatalogFunction").disabled = true;
    }
    else {
        document.getElementById("ctl00_MainContent_ddlCatalogFunction").disabled = false;
        
    }

    document.getElementById("ctl00_MainContent_txtCatalogueCode").disabled = true;
  



    $("#divAddSystem").prop('title', 'Edit System');
    showModal('divAddSystem', false);

    BindVessel();
    BindDepartment();
    BindAccountCode();
    BindSystemFunction(ParentType, "");
    BindSystemAssignLocation();
    BindCatSupplier();

    return false;

}
function onDeleteSystem() {
    var UserID = document.getElementById("ctl00_MainContent_hdnUserID").value;
    var CatalogCode = _SystemID;
    var conf = confirm("Are you sure you want to delete system");
    if (conf == true) {
        var params = { userid: parseInt(UserID), systemid: parseInt(CatalogCode) };

        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "../../JIBEWebService.asmx/PMS_System_Delete",
            data: JSON.stringify(params),
            dataType: "json",
            success: function (Result) {
                Result = Result.d;
                alert("System Deleted Successfully");
                CatalogOperationMode = "";
            },
            error: function (Result) {
                alert("Error");
            }
        });
        DDlVessel_selectionChange();
    }

}
function onRestoreSystem() {

    var UserID = document.getElementById("ctl00_MainContent_hdnUserID").value;
    var CatalogCode = _SystemID;
    var conf = confirm("Are you sure you want to restore system");
    if (conf == true) {
        var params = { userid: parseInt(UserID), systemid: parseInt(CatalogCode) };

        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "../../JIBEWebService.asmx/PMS_System_Restore",
            data: JSON.stringify(params),
            dataType: "json",
            success: function (Result) {
                Result = Result.d;
                alert("System Restored Successfully");
                CatalogOperationMode = "";
            },
            error: function (Result) {
                alert("Error");
            }
        });
    }
}


function onDeleteSubSystem() {
    var UserID = document.getElementById("ctl00_MainContent_hdnUserID").value;
    var SubCatalogueCode = _SubSystemID;
    // alert(_SubSystemID);
    var conf = confirm("Are you sure you want to delete subsystem");
    if (conf == true) {
        var params = { userid: parseInt(UserID), subsystemid: parseInt(SubCatalogueCode) };

        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "../../JIBEWebService.asmx/PMS_SubSystem_Delete",
            data: JSON.stringify(params),
            dataType: "json",
            success: function (Result) {
                Result = Result.d;
                alert("SubSystem Deleted Successfully");
                CatalogOperationMode = "";
            },
            error: function (Result) {
                alert("Error");
            }
        });
        DDlVessel_selectionChange();
    }

}
function onRestoreSubSystem() {

    var UserID = document.getElementById("ctl00_MainContent_hdnUserID").value;
    var SubCatalogueCode = _SubSystemID;
    var conf = confirm("Are you sure you want to restore subsystem");
    if (conf == true) {
        var params = { userid: parseInt(UserID), subsystemid: parseInt(SubCatalogueCode) };

        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "../../JIBEWebService.asmx/PMS_SubSystem_Restore",
            data: JSON.stringify(params),
            dataType: "json",
            success: function (Result) {
                Result = Result.d;
                alert("SubSystem Restored Successfully");
                CatalogOperationMode = "";
            },
            error: function (Result) {
                alert("Error");
            }
        });
    }
}

// To save System
function onSaveSystem() {



    var CatalogOperationMode = document.getElementById("ctl00_MainContent_hdnCatalogOperationMode").value;
    var VesselId = document.getElementById($('[id$=ddlVessID]').attr('id')).value;
    var FNId = document.getElementById($('[id$=ddlCatalogFunction]').attr('id')).value;
    var UserID = document.getElementById("ctl00_MainContent_hdnUserID").value;
    var CatalogCode = document.getElementById("ctl00_MainContent_txtCatalogueCode").value;
    var SetInstalled = document.getElementById("ctl00_MainContent_txtCatalogueSetInstalled").value.trim();
    var CatalogName = document.getElementById("ctl00_MainContent_txtCatalogName").value.trim();
    var CatalogParticular = document.getElementById("ctl00_MainContent_txtCatalogueParticular").value;
    var Maker = document.getElementById('ctl00_MainContent_ddlCalalogueMaker').value;
    var DeptTypeVal = $('[id$=rblDeptType]').find(':checked').val();
    var Dept = document.getElementById('ctl00_MainContent_ddlCatalogDept').value;
    if (VesselId == "") {

        VesselId = "0";
    }
    if (FNId == "") {
        FNId = "0";
    }

    var CatalogModel = document.getElementById("ctl00_MainContent_txtCatalogueModel").value;
    var SerialNumber = document.getElementById("ctl00_MainContent_txtCatalogueSerialNumber").value;
    var Function = document.getElementById('ctl00_MainContent_ddlCatalogFunction').value;
    var SystemID = document.getElementById("ctl00_MainContent_hdnSysID").value;
    var SubSystemID = document.getElementById("ctl00_MainContent_hdnSubSysID").value;
    var deptType = $('[id$=rblDeptType]').find(':checked').val();
    var isLocationRequired = "1";
    if (document.getElementById('ctl00_MainContent_chkRunHourS').checked == true) {
        var Run_Hours = "1";
    }
    else {
        var Run_Hours = "0";
    }

    if (document.getElementById('ctl00_MainContent_chkCriticalS').checked == true) {
        var Criticals = "1";
    }
    else {
        var Criticals = "0";
    }

    var AccountCode = document.getElementById('ctl00_MainContent_ddlAccountCode').value;
    if (document.getElementById('ctl00_MainContent_chkSubSysAdd').checked == true) {
        var AddSubSysFlag = "1";
    }
    else {
        var AddSubSysFlag = "0";
    }

    if (CatalogOperationMode != "ADD") {
        if (deptType != "ST") {

            if (FNId == "0") {
                alert("Please select the function.");
                return false;
            }
            if (VesselId == "0") {
                alert("Please select the vessel.");
                return false;
            }
        }
    }

    if (CatalogOperationMode != "EDIT") {
        if (deptType != "ST") {

            if (FNId == "0") {
                alert("Please select the function.");
                return false;
            }
            if (VesselId == "0") {
                alert("Please select the vessel.");
                return false;
            }
        }
    }

    if (DeptTypeVal != 'ST' && DeptTypeVal != 'RP') {
        isLocationRequired = "1";
        if (SetInstalled != "") {
            if (isNaN(SetInstalled)) {

                alert("Set installed accept only numeric value.");
                document.getElementById("ctl00_MainContent_txtCatalogueSetInstalled").focus();
                return false;
            }
            if (parseInt(SetInstalled) < 1) {

                alert("Set installed should not be less than 1.");
                document.getElementById("ctl00_MainContent_txtCatalogueSetInstalled").focus();
                return false;
            }
            if (parseInt(SetInstalled) > 12) {
                alert("Sets installed should not be greater than 12.");
                document.getElementById("ctl00_MainContent_txtCatalogueSetInstalled").focus();
                return false;
            }
            if (SetInstalled.indexOf('.') >= 0) {
                alert("Invalid Sets installed");
                document.getElementById("ctl00_MainContent_txtCatalogueSetInstalled").focus();
                return false;
            }
        }
        else {

            alert("Set installed is required");
            document.getElementById("ctl00_MainContent_txtSubCatSetInstalled").focus();
            return false;

        }
    }
    else {
        isLocationRequired = "0";

    }
    
    if (CatalogCode == "") {

        document.getElementById("ctl00_MainContent_txtCatalogueCode").focus();
        alert("Catalogue Code is required.");
        return false;


    }
    else if (CatalogName == "") {

        document.getElementById("ctl00_MainContent_txtCatalogName").focus();
        alert("System name is required.");
        return false;



    }

    else if (Dept == "0") {

        document.getElementById("ctl00_MainContent_ddlCatalogDept").focus();
        alert("System Department is required.");
        return false;
    }




    else {

        if (CatalogOperationMode == "ADD") {
            var params = { userid: UserID, systemcode: CatalogCode, systemdesc: CatalogName, stystemparticular: CatalogParticular, maker: Maker, setinstalled: SetInstalled, model: CatalogModel, dept: Dept, vesselid: VesselId, functionid: Function, accountcode: AccountCode, AddSubSysFlag: parseInt(AddSubSysFlag), SerialNumber: SerialNumber, IsLocationRequired: parseInt(isLocationRequired), Run_Hour: parseInt(Run_Hours), Critical: parseInt(Criticals), DeptType: DeptTypeVal };

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "../../JIBEWebService.asmx/PMS_Insert_NewSystem",
                data: JSON.stringify(params),
                dataType: "json",
                success: function (Result) {
                    Result = Result.d;

                    alert("System Added Successfully");
                    hideModal('divAddSystem');
                    CatalogOperationMode = "";
                    DDlVessel_selectionChange();

                },
                error: function (Result) {
                    alert("Error");
                }
            });

        }
        else if (CatalogOperationMode == "EDIT") {
            var params = { userid: UserID, systemid: SystemID, systemcode: CatalogCode, systemdesc: CatalogName, stystemparticular: CatalogParticular, maker: Maker, setinstalled: SetInstalled, model: CatalogModel, dept: Dept, vesselid: VesselId, functionid: Function, accountcode: AccountCode, SerialNumber: SerialNumber, Run_Hour: parseInt(Run_Hours), Critical: parseInt(Criticals) };
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "../../JIBEWebService.asmx/PMS_Update_System",
                data: JSON.stringify(params),
                dataType: "json",
                success: function (Result) {
                    Result = Result.d;
                    
                    Get_Machinery_Details(VesselId, SystemID, SubSystemID);

                    alert("System Edited Successfully");
                    hideModal('divAddSystem');
                    CatalogOperationMode = "";
                    DDlVessel_selectionChange();//To auto refresh.

                },
                error: function (Result) {
                    alert("Error");
                }
            });


        }

        BindCatSupplier();
        BindDepartment();
        BindSystemFunction(ParentType, "");
    }




    return false;
}





function onAddSubSystem() {
    clearSubCatalogFields();
    document.getElementById("ctl00_MainContent_hdnSubCatalogOperationMode").value = "ADD";
    document.getElementById("ctl00_MainContent_hdnCatalogOperationMode").value = null;
    // document.getElementById($('[id$=btnSubCatDivAddLocation]').attr('id')).disabled = true;
    BindSubCatSupplier();
    $("#divAddSubSystem").prop('title', 'Add SubSystem');
    showModal('divAddSubSystem', false);
    document.getElementById('ctl00_MainContent_chkRunHourSS').disabled = false;
    if ($('[id$=rblDeptType]').find(':checked').val() != "SP") {
        document.getElementById($('[id$=txtSubCatSetInstalled]').attr('id')).disabled = true;
        document.getElementById($('[id$=ImgSubCatAssLocation]').attr('id')).disabled = true;
    }
    else {
        document.getElementById($('[id$=txtSubCatSetInstalled]').attr('id')).disabled = false;
        document.getElementById($('[id$=ImgSubCatAssLocation]').attr('id')).disabled = true;
    }
    return false;
}
function onEditSubSystem() {
    clearSubCatalogFields();

    document.getElementById("ctl00_MainContent_hdnSubCatalogOperationMode").value = "EDIT";
    document.getElementById("ctl00_MainContent_hdnCatalogOperationMode").value = null;
    //document.getElementById($('[id$=btnSubCatDivAddLocation]').attr('id')).disabled = false;
    $("#divAddSubSystem").prop('title', 'Edit SubSystem');
    showModal('divAddSubSystem', false);
    BindSubCatSupplier();
    BindSubSystemAssignLocation();
    // DisabledCopyRunHour();
}




function onJobSearchKeyPress(e) {

    if (e.keyCode == 13) {
        onSearchJobs();
        return false;
    }
    else {
        return true;
    }
    return false;
}

function onSpareSearchKeyPress(e) {

    if (e.keyCode == 13) {
        onSearchSpare();
        return false;
    }
    else {
        return true;
    }

    return false;
}

// To save sub system.
function onSaveSubSystem() {

    var SubCatalogOperationMode = document.getElementById("ctl00_MainContent_hdnSubCatalogOperationMode").value;
    var VesselId = document.getElementById("ctl00_MainContent_hdnVesselID").value;
    var UserID = document.getElementById("ctl00_MainContent_hdnUserID").value;
    var SubCatalogName = document.getElementById("ctl00_MainContent_txtSubCatalogueName").value.trim();
    var SubCatalogParticular = document.getElementById("ctl00_MainContent_txtSubCatalogueParticulars").value;
    var Maker = document.getElementById('ctl00_MainContent_ddlSubCatMaker').value;
    var SubCatalogModel = document.getElementById("ctl00_MainContent_txtSubCatModel").value;
    var SerialNumber = document.getElementById("ctl00_MainContent_txtSubCatSerialNo").value;
    var SystemID = document.getElementById("ctl00_MainContent_hdnSysID").value;
    var SubSystemID = document.getElementById("ctl00_MainContent_hdnSubSysID").value;
    var SystemCode = document.getElementById("ctl00_MainContent_hdnSysCode").value
    var SetInstalled = document.getElementById("ctl00_MainContent_txtSubCatSetInstalled").value.trim();
    var isLocationRequired = "1";
    var DeptTypeVal = $('[id$=rblDeptType]').find(':checked').val();
    if (VesselId == "") {

        VesselId = "0";
    }
    if (document.getElementById('ctl00_MainContent_chkRunHourSS').checked == true) {
        var Run_Hours = "1";
    }
    else {
        var Run_Hours = "0";
    }

    if (document.getElementById('ctl00_MainContent_chkCriticalSS').checked == true) {
        var Criticals = "1";
    }
    else {
        var Criticals = "0";
    }

    if (DeptTypeVal != 'ST' && DeptTypeVal != 'RP') {
        isLocationRequired = "1";
        if (SetInstalled != "") {
            if (isNaN(SetInstalled)) {

                alert("Set installed accept only numeric value.");
                document.getElementById("ctl00_MainContent_txtSubCatSetInstalled").focus();
                return false;
            }
            if (parseInt(SetInstalled) < 1) {

                alert("Set installed should not be less than 1.");
                document.getElementById("ctl00_MainContent_txtSubCatSetInstalled").focus();
                return false;
            }
            if (parseInt(SetInstalled) > 12) {

                alert("Sets installed should not be greater than 12.");
                document.getElementById("ctl00_MainContent_txtSubCatSetInstalled").focus();
                return false;
            }
            if (SetInstalled.indexOf('.') >= 0) {
                alert("Invalid Sets installed");
                document.getElementById("ctl00_MainContent_txtSubCatSetInstalled").focus();
                return false;
            }

        }

        else {

            alert("Set installed is required");
            document.getElementById("ctl00_MainContent_txtSubCatSetInstalled").focus();
            return false;

        }
    }
    else {
        isLocationRequired = "0";


    }

    if (SubCatalogName == "") {
        alert("SubSystem name is required.");
        document.getElementById("ctl00_MainContent_txtSubCatalogueName").focus();
        return false;
    }
    else {

        if (SubCatalogOperationMode == "ADD") {
            var params = { userid: UserID, systemcode: _SystemCode, substystemdesc: SubCatalogName, subsytemparticular: SubCatalogParticular, Maker: Maker, Model: SubCatalogModel, SerialNo: SerialNumber, IsLocationRequired: parseInt(isLocationRequired), Run_Hour: parseInt(Run_Hours), Critical: parseInt(Criticals), setInstalled: SetInstalled };

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "../../JIBEWebService.asmx/PMS_Insert_NewSubSystem",
                data: JSON.stringify(params),
                dataType: "json",
                success: function (Result) {
                    Result = Result.d;




                    //onAddSubSystem();
                    alert("Sub System Added Successfully");
                    hideModal('divAddSubSystem');
                    SubCatalogOperationMode = "";
                    DDlVessel_selectionChange();

                },
                error: function (Result) {
                    alert("Error");
                }
            });

        }
        else if (SubCatalogOperationMode == "EDIT") {
            var params = { userid: UserID, subsystemid: _SubSystemID, systemcode: _SystemCode, subsystemcode: _SubSystemID, substystemdesc: SubCatalogName, subsytemparticular: SubCatalogParticular, Maker: Maker, Model: SubCatalogModel, SerialNo: SerialNumber, Run_Hour: parseInt(Run_Hours), Critical: parseInt(Criticals), setinstalled: SetInstalled };


            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "../../JIBEWebService.asmx/PMS_Update_SubSystem",
                data: JSON.stringify(params),
                dataType: "json",
                success: function (Result) {
                    Result = Result.d;

                    Get_SubMachinery_Details(VesselId, SystemID, SubSystemID);
                    alert("Sub System Edited Successfully");
                    hideModal('divAddSubSystem');
                    SubCatalogOperationMode = "";
                    DDlVessel_selectionChange(); // To auto refresh

                },
                error: function (Result) {
                    alert("Error");
                }
            });

        }


        BindSubCatSupplier();

    }

    return false;
}
function onCheckAllJobs() {
    var mainTable = document.getElementById("tblJobs");

    var tempTable = document.getElementById("tblAllJob");

    var chkAll = document.getElementById("ctl00_MainContent_chkCheckAllJobs")
    var found = false;
    if (tempTable != null && mainTable != null) {

        for (var i = 1, row; row = mainTable.rows[i]; i++) {

            var jobid = "tblJobs_Selected" + i;

            var chkJob = document.getElementById(jobid);

            chkJob.checked = chkAll.checked;



        }
        for (var i = 1, row; row = tempTable.rows[i]; i++) {

            var jobid1 = "tblAllJob_Selected" + i;

            var chkJob1 = document.getElementById(jobid1);

            chkJob1.checked = chkAll.checked;


        }

    }



}
function onAddJob() {
    clearJobsFields();

    GetPMSFrequency("2491", "");
    GetPMSDepartment("2487", "");
    _JobID = "-1";
    BindJobAttachment();
    //GetRank(); Move to Document.ready event
    document.getElementById("ctl00_MainContent_hdnJobOperationMode").value = "ADD";
    document.getElementById($('[id$=btnAttach]').attr('id')).disabled = true;
    document.getElementById($('[id$=ChkMandatoryRAApproval]').attr('id')).disabled = true;        //Added by reshma for RA : disable checkbox
          
    $("#divAddJob").prop('title', 'Add Job');
    showModal('divAddJob', false);
    //document.getElementById("ctl00_MainContent_btnAddNewJob").style.display = "inline";


    return false;
}



function onEditJob(jobid, evt, objthis) {
    _JobID = jobid;
    BindJobAttachment();
    clearJobsFields();
    //GetRank(); Move to Document.ready event
    GetPMSFrequency("2491", "");
    GetPMSDepartment("2487", "");
    document.getElementById("ctl00_MainContent_hdnJobID").value = jobid;
    document.getElementById($('[id$=btnAttach]').attr('id')).disabled = false;
    
    document.getElementById("ctl00_MainContent_hdnJobOperationMode").value = "EDIT";
    var params = { JobID: jobid };

    setTimeout(function () {
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "../../JIBEWebService.asmx/Get_Jobs",
            data: JSON.stringify(params),
            dataType: "json",
            success: function (Result) {
                Result = Result.d;


                if (Result.length > 0) {

                    document.getElementById("ctl00_MainContent_lstDepartment").value = Result[0].DEPT;
                    document.getElementById("ctl00_MainContent_lstFrequency").value = Result[0].FREQ_TYPE;
                    document.getElementById("ctl00_MainContent_ddlRank").value = Result[0].RANK_ID;
                    document.getElementById("ctl00_MainContent_txtJobTitle").value = Result[0].JOB_TITLE;
                    document.getElementById("ctl00_MainContent_txtjobDescription").value = Result[0].JOB_DESC;
                    document.getElementById("ctl00_MainContent_txtFrequency").value = Result[0].FREQ;


                    var RB1 = document.getElementById("ctl00_MainContent_optCMS");
                    var radio = RB1.getElementsByTagName("input");


                    if (Result[0].CMS == "1") {
                        radio[0].checked = true;
                        radio[1].checked = false;
                    }
                    else {
                        radio[0].checked = false;
                        radio[1].checked = true;
                    }


                    var RB1 = document.getElementById("ctl00_MainContent_optCritical");
                    var radio = RB1.getElementsByTagName("input");

                    if (Result[0].CRITCAL == "1") {
                        radio[0].checked = true;
                        radio[1].checked = false;
                    }
                    else {
                        radio[0].checked = false;
                        radio[1].checked = true;
                    }

                    if (Result[0].IS_TECH_REQ == "1") {
                        document.getElementById('ctl00_MainContent_chkIsTechRequired').checked = true;
                    }
                    else {
                        document.getElementById('ctl00_MainContent_chkIsTechRequired').checked = false;

                    }


                    if (Result[0].IS_SAFETY_ALARM == "1") {
                        document.getElementById('ctl00_MainContent_chkSafetyAlarm').checked = true;
                    }
                    else {
                        document.getElementById('ctl00_MainContent_chkSafetyAlarm').checked = false;

                    }

                    if (Result[0].IS_CALIBRATION == "1") {
                        document.getElementById('ctl00_MainContent_chkCalibration').checked = true;
                    }
                    else {
                        document.getElementById('ctl00_MainContent_chkCalibration').checked = false;

                    }

                    if (Result[0].IsRAMandatory == "1") {                                                    //Added by reshma for RA : checkbox checked or unchecked
                        document.getElementById('ctl00_MainContent_ChkMandatoryRA').checked = true;
                    }
                    else {
                        document.getElementById('ctl00_MainContent_ChkMandatoryRA').checked = false;


                    }

                    if (Result[0].IsRAApproval == "1") {                                                    //Added by reshma for RA :checkbox checked or unchecked
                        document.getElementById('ctl00_MainContent_ChkMandatoryRAApproval').checked = true;
                    }
                    else {
                        document.getElementById('ctl00_MainContent_ChkMandatoryRAApproval').checked = false;

                    }

                    document.getElementById("ctl00_MainContent_lblItemCreatedBy").innerHTML = Result[0].CREATED_BY;
                    document.getElementById("ctl00_MainContent_lblItemmodifiedby").innerHTML = Result[0].MODIFY_BY;
                    document.getElementById("ctl00_MainContent_lblItemDeletedBy").innerHTML = Result[0].DELETED_BY;

                    if (document.getElementById('ctl00_MainContent_ChkMandatoryRA').checked == false) {
                        document.getElementById($('[id$=ChkMandatoryRAApproval]').attr('id')).disabled = true;
                    }
                    else {
                        document.getElementById($('[id$=ChkMandatoryRAApproval]').attr('id')).disabled = false;
                    }

                    $("#divAddJob").prop('title', 'Edit Job');
                    showModal('divAddJob', false);


                    // document.getElementById($('[id$=ChkMandatoryRAApproval]').attr('id')).disabled = false;

                }

            },
            error: function (Result) {
                alert("Error");
            }
        });
    }, 100);



}


function onDeleteJob(jobid, evt, objthis) {

    var res = confirm('Are you sure, you want to  delete ?');

    if (res == true) {
        var UserID = document.getElementById("ctl00_MainContent_hdnUserID").value;
        var params = { userid: UserID, JobID: jobid };
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "../../JIBEWebService.asmx/Delete_Jobs",
            data: JSON.stringify(params),
            dataType: "json",
            success: function (Result) {

                var searchtext = document.getElementById($('[id$=txtSearchJobs]').attr('id')).value;
                if (searchtext == "") {

                    searchtext = null;
                }
                Get_Lib_PlannedJobs(_Vessel_ID, _SystemID, _SubSystemID, null, null, null, _ActiveJobStatus, searchtext, null, null, "ID", null, parseInt("1"), parseInt("15"), parseInt("1"), "tblJobs");    // Added by reshma for RA Is_RAMandatory :null , IsRAApproval: null in JIT :11705
                Get_Lib_PlannedJobsCount(_Vessel_ID, _SystemID, _SubSystemID, null, null, null, _ActiveJobStatus, null, null, null, "ID", null, parseInt("1"), parseInt("15"), parseInt("1"), "tblJobs");      // Added by reshma for RA Is_RAMandatory :null , IsRAApproval: null in JIT :11705

            },
            error: function (Result) {
                alert("Error");
            }
        });
    }
}
function onRestoreJob(jobid, evt, objthis) {




    var UserID = document.getElementById("ctl00_MainContent_hdnUserID").value;
    var params = { userid: UserID, JobID: jobid };
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "../../JIBEWebService.asmx/Restore_Jobs",
        data: JSON.stringify(params),
        dataType: "json",
        success: function (Result) {

            Get_Lib_PlannedJobs(_Vessel_ID, _SystemID, _SubSystemID, null, null, null, _ActiveJobStatus, _searchjobtext, null, null, "ID", null, parseInt("1"), parseInt("15"), parseInt("1"), "tblJobs");   // Added by reshma for RA Is_RAMandatory :null , IsRAApproval: null in JIT :11705
            Get_Lib_PlannedJobsCount(_Vessel_ID, _SystemID, _SubSystemID, null, null, null, _ActiveJobStatus, null, null, null, "ID", null, parseInt("1"), parseInt("15"), parseInt("1"), "tblJobs");        // Added by reshma for RA Is_RAMandatory :null , IsRAApproval: null in JIT :11705

        },
        error: function (Result) {
            alert("Error");
        }
    });

}
function onFilterJob(radiobuttonlist) {

    var rdbBoxCount = radiobuttonlist.getElementsByTagName("input");
    if (rdbBoxCount[0].checked) {

        _ActiveJobStatus = null;
    }
    else if (rdbBoxCount[1].checked) {
        _ActiveJobStatus = rdbBoxCount[1].value;
    }
    else if (rdbBoxCount[2].checked) {

        _ActiveJobStatus = rdbBoxCount[2].value;
    }

    var searchtext = document.getElementById($('[id$=txtSearchJobs]').attr('id')).value;
    if (searchtext == "") {

        searchtext = null;
    }
    Get_Lib_PlannedJobs(_Vessel_ID, _SystemID, _SubSystemID, null, null, null, _ActiveJobStatus, searchtext, null, null, "ID", null, parseInt("1"), parseInt("15"), parseInt("1"), "tblJobs");     // Added by reshma for RA Is_RAMandatory :null , IsRAApproval: null in JIT :11705
    Get_Lib_PlannedJobsCount(_Vessel_ID, _SystemID, _SubSystemID, null, null, null, _ActiveJobStatus, searchtext, null, null, "ID", null, parseInt("1"), parseInt("15"), parseInt("1"), "tblJobs");   // Added by reshma for RA Is_RAMandatory :null , IsRAApproval: null in JIT :11705
}
function onJobAttachment() {


    if (_JobID != null && parseInt(_JobID) > 0) {
        document.getElementById('IframeAttach').src = "../PMS/PMSJobsAttachment.aspx?VesselCode=" + _Vessel_ID + "&JobId=" + _JobID;
        $('#dvPopupAddAttachment').prop('title', 'Add Attachments');
        showModal('dvPopupAddAttachment', false, fnCloseJobAttach);

    }
    else {
        alert('Please select or save job !');

    }
    return false;

}

function fnCloseJobAttach() {

    document.getElementById('IframeAttach').src = "";
}
var lastExecutorLibPlannedJobs1 = null;
function BindJobAttachment() {


    if (lastExecutorLibPlannedJobs1 != null)
        lastExecutorLibPlannedJobs1.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../../JibeWebService.asmx', 'Get_Lib_Planned_JobsAttachment_ManageSystem', false, { "Vessel_ID": _Vessel_ID, "jobid": _JobID, "TableID": "tbJobAtt" }, onSuccessBindOfJobsAttach, Onfail, new Array('alljobs'));
    lastExecutorLibPlannedJobs1 = service.get_executor();

    return false;

}
function onDeleteJobAttachment(attachmentpath, event, ths) {

    var UserID = document.getElementById("ctl00_MainContent_hdnUserID").value;


    var res = confirm('Are you sure, you want to  delete ?');

    if (res == true) {
        var UserID = document.getElementById("ctl00_MainContent_hdnUserID").value;
        var params = { Attchment_Path: attachmentpath, userid: UserID };
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "../../JIBEWebService.asmx/Delete_Jobs_Attachement",
            data: JSON.stringify(params),
            dataType: "json",
            success: function (Result) {

                BindJobAttachment();


            },
            error: function (Result) {
                alert("Error");
            }
        });
    }
    return false;

}



function onSuccessBindOfJobsAttach(retval, ev) {

    document.getElementById('jobattachment').innerHTML = retval;


}
function onSaveJob() {
    var JobOperationMode = document.getElementById("ctl00_MainContent_hdnJobOperationMode").value;
    var VesselId = _Vessel_ID;
    var UserID = document.getElementById("ctl00_MainContent_hdnUserID").value;
    var SubSystemID = document.getElementById("ctl00_MainContent_hdnSubSysID").value;
    var SystemID = document.getElementById("ctl00_MainContent_hdnSysID").value;
    var Dept = document.getElementById('ctl00_MainContent_lstDepartment').value;
    var FrequencyType = document.getElementById('ctl00_MainContent_lstFrequency').value;
    var Rank = document.getElementById('ctl00_MainContent_ddlRank').value;
    var JobTitle = document.getElementById('ctl00_MainContent_txtJobTitle').value.trim();
    var JobDesc = document.getElementById('ctl00_MainContent_txtjobDescription').value.trim();
    var JOBID = document.getElementById('ctl00_MainContent_hdnJobID').value;
    var Frequency = document.getElementById('ctl00_MainContent_txtFrequency').value;
    var SafetyAlarm = null;
    var Calibration = null;

    var IsRAMandatory = null;
    var IsRAApproval = null;


    var CMS = $('[id$=optCMS]').find(":checked").val();
    var Critical = $('[id$=optCritical]').find(":checked").val();

    var IsRAMandatory = $('[id$=ChkMandatoryRA]').find(":checked").val();             //Added by Reshma for RA : checkbox checked or unchecked
    var IsRAApproval = $('[id$=ChkMandatoryRAApproval]').find(":checked").val();        //Added by Reshma for RA : checkbox checked or unchecked

    if (document.getElementById('ctl00_MainContent_chkIsTechRequired').checked == true) {
        var IsTechRequired = "1";
    }
    else {
        var IsTechRequired = "0";
    }

    if (document.getElementById('ctl00_MainContent_chkSafetyAlarm').checked == true) {
        SafetyAlarm = "1";
    }
    if (document.getElementById('ctl00_MainContent_chkCalibration').checked == true) {
        Calibration = "1";
    }


    if (document.getElementById('ctl00_MainContent_ChkMandatoryRA').checked == true) {    //Added by Reshma for RA : set value for RA_Mandatory
        IsRAMandatory = "1";
      
    }
    else {
        IsRAMandatory = "0";
    }
    if (document.getElementById('ctl00_MainContent_ChkMandatoryRAApproval').checked == true) {   //Added by Reshma for RA : set value for RA_Approval
        IsRAApproval = "1";
    }
    else {
        IsRAApproval = "0";
    }



    if (document.getElementById('ctl00_MainContent_hdnJobCode').value == "0" || document.getElementById('ctl00_MainContent_hdnJobCode').value == "") {
        var JobCode = null;

    }
    else {

        var JobCode = document.getElementById('ctl00_MainContent_hdnJobCode').innerHTML;

    }

    if (JobTitle == "") {
        alert("Job Title is required.");
        document.getElementById("ctl00_MainContent_txtJobTitle").focus();
        return false;
    }
    if (JobDesc == "") {
        alert("Job Description is required.");
        document.getElementById("ctl00_MainContent_txtjobDescription").focus();
        return false;
    }
    else if (Frequency == "") {
        alert("Frequency is required.");
        document.getElementById("ctl00_MainContent_txtFrequency").focus();
        return false;
    }
    if (Rank == "" || Rank == "0") {

        alert("Rank is required.");
        document.getElementById($('[id$=ddlRank]').attr('id')).focus();
        return false;
    }
    else if (Frequency != "") {
        if (isNaN(Frequency)) {
            alert('Frequency is accept ony numeric value.')
            return false;
        }

        else {


            if (JobOperationMode == "ADD") {
                var params = { userid: parseInt(UserID), systemid: parseInt(SystemID), subsystemid: parseInt(SubSystemID), vesselid: parseInt(VesselId), deptid: parseInt(Dept), rankid: parseInt(Rank), Job_Code: JobCode, jobtitle: JobTitle, jobdesc: JobDesc, frequency: parseInt(Frequency), frequencytype: parseInt(FrequencyType), cms: parseInt(CMS), critical: parseInt(Critical), Is_Tech_Required: IsTechRequired, Is_SafetyAlarm: SafetyAlarm, Is_Calibration: Calibration, Is_RAMandatory: IsRAMandatory, Is_RAApproval: IsRAApproval }; //Added by Reshma for RA JIT: 11705

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "../../JIBEWebService.asmx/LibraryJobSave",
                    data: JSON.stringify(params),
                    dataType: "json",
                    success: function (Result) {
                        Result = Result.d;


                       

                        var searchtext = document.getElementById($('[id$=txtSearchJobs]').attr('id')).value;
                        if (searchtext == "") {

                            searchtext = null;
                        }
                        Get_Lib_PlannedJobs(_Vessel_ID, _SystemID, _SubSystemID, null, null, null, _ActiveJobStatus, searchtext, null, null, "ID", null, parseInt("1"), parseInt("15"), parseInt("1"), "tblJobs");  // Added by reshma for RA Is_RAMandatory :null , IsRAApproval: null in JIT :11705
                        alert("Job Added Successfully");
                        hideModal('divAddJob');
                        JobOperationMode = "";
                        Get_Lib_PlannedJobsCount(_Vessel_ID, _SystemID, _SubSystemID, null, null, null, _ActiveJobStatus, null, null, null, "ID", null, parseInt("1"), parseInt("15"), parseInt("1"), "tblJobs");    // Added by reshma for RA Is_RAMandatory :null , IsRAApproval: null in JIT :11705
                    },
                    error: function (Result) {
                        alert("Error");
                    }
                });

            }
            else if (JobOperationMode == "EDIT") {
                var params = { userid: parseInt(UserID), jobsid: parseInt(JOBID), systemid: parseInt(SystemID), subsystemid: parseInt(SubSystemID), vesselid: parseInt(VesselId), deptid: parseInt(Dept), rankid: parseInt(Rank), Job_Code: JobCode, jobtitle: JobTitle, jobdesc: JobDesc, frequency: parseInt(Frequency), frequencytype: parseInt(FrequencyType), cms: parseInt(CMS), critical: parseInt(Critical), Is_Tech_Required: IsTechRequired, Is_SafetyAlarm: SafetyAlarm, Is_Calibration: Calibration, Is_RAMandatory: IsRAMandatory, Is_RAApproval: IsRAApproval }; //Added by Reshma for RA JIT: 11705


                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "../../JIBEWebService.asmx/LibraryJobUpdate",
                    data: JSON.stringify(params),
                    dataType: "json",
                    success: function (Result) {
                        Result = Result.d;



                        var searchtext = document.getElementById($('[id$=txtSearchJobs]').attr('id')).value;
                        if (searchtext == "") {

                            searchtext = null;
                        }
                        Get_Lib_PlannedJobs(_Vessel_ID, _SystemID, _SubSystemID, null, null, null, _ActiveJobStatus, searchtext, null, null, "ID", null, parseInt("1"), parseInt("15"), parseInt("1"), "tblJobs");   // Added by reshma for RA Is_RAMandatory :null , IsRAApproval: null in JIT :11705
                        alert("Job Edited Successfully");
                        hideModal('divAddJob');
                        JobOperationMode = "";

                    },
                    error: function (Result) {
                        alert("Error");
                    }
                });
            }

            Get_Machinery_Details(VesselId, SystemID, SubSystemID);



        }
    }
    return false;
}


function onCopyJobs() {

    if (_Vessel_ID == "0") {

        document.getElementById('iFrmCopyJobs').src = '../PMS/PMSCopyJobs.aspx';
        $('#dvCopyJobsPopUp').prop('title', 'Copy Jobs');
        showModal('dvCopyJobsPopUp');
    }
    else {


        document.getElementById('iFrmCopyJobs').src = '../PMS/PMSCopyJobs.aspx?VesselCode=' + _Vessel_ID + '&SystemCode=' + _SystemCode + '&SystemID=' + _SystemID + '&SubSystemID=' + _SubSystemID + '&SubSystemCode=' + _SubSystemCode + '&DeptCode=' + _DepartmentCode;
        $('#dvCopyJobsPopUp').prop('title', 'Copy Jobs');
        showModal('dvCopyJobsPopUp');
    }
    return false;
}

/* Function to Move Jobs from One System/SubSystem To Other.*/
function onMoveJobs() {

    if (document.getElementById("ctl00_MainContent_DDlVessel_List").value == "0") {
        alert("Please select the vessel.");
        return false;
    }

    var mainTable = document.getElementById("tblJobs");

    var tempTable = document.getElementById("tblAllJob");
    var found = false;
    if (tempTable != null && mainTable != null) {

        for (var i = 1, row; row = mainTable.rows[i]; i++) {

            var jobid = "tblJobs_Selected" + i;

            var chkJob = document.getElementById(jobid);
            if ((chkJob.checked == true)) {
                for (var j = 1, row1; row1 = tempTable.rows[j]; j++) {

                    if ((row.cells[0].textContent == row1.cells[0].textContent)) {

                        var jobid1 = "tblAllJob_Selected" + j;

                        var chkJob1 = document.getElementById(jobid1);

                        chkJob1.checked = true;

                    }
                }

            }

        }

        var temptable = "<table>";
        var table = document.getElementById("tblAllJob");
        var hdrrow = table.rows[0];
        //                temptable = temptable + "<th>" + table.rows[0].textContent + "</th>";

        temptable = temptable + "<tr><th>" + hdrrow.cells[0].textContent + "</th></tr>"; //+ "<th>" + hdrrow.cells[1].textContent + "</th>" + "<th>" + hdrrow.cells[2].textContent + "</th>" + "<th>" + hdrrow.cells[3].textContent + "</th>" + "<th>" + hdrrow.cells[4].textContent + "</th>" + "<th>" + hdrrow.cells[5].textContent + "</th>" + "<th>" + hdrrow.cells[6].textContent + "</th>" + "<th>" + hdrrow.cells[7].textContent + "</th>" + "<th>" + hdrrow.cells[8].textContent + "</th>" + "<th>"+hdrrow.cells[9].textContent+"</th>";
        for (var i = 1, row; row = table.rows[i]; i++) {

            var jobid = "tblAllJob_Selected" + i;

            var chkJob = document.getElementById(jobid);
            if ((chkJob.checked == true)) {
                temptable = temptable + "<tr>";

                temptable = temptable + "<td>" + row.cells[0].textContent + "</td>"; //+ "<td>" + row.cells[1].textContent + "</td>" + "<td>" + row.cells[2].textContent + "</td>" + "<td>" + row.cells[3].textContent + "</td>" + "<td>" + row.cells[4].textContent + "</td>" + "<td>" + row.cells[5].textContent + "</td>" + "<td>" + row.cells[6].textContent + "</td>" + "<td>" + row.cells[7].textContent + "</td>" + "<td>" + row.cells[8].textContent + "</td>"+ "<td>1</td>" ;
                temptable = temptable + "</tr>";

            }



        }
        temptable = temptable + "</table>"
        //  var tmp = temptable;
        var params = { table: temptable };
        $.ajax({
            type: "POST",
            url: "../../Technical/PMS/PMS_Manage_System_SubSystem.aspx/GetMovingData",
            data: JSON.stringify(params),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {

                if (response.d == "true") {
                    document.getElementById('iFrmMoveJobs').src = "../PMS/PMSMoveJobs.aspx?VesselCode=" + _Vessel_ID + "&SystemCode=" + _SystemCode + "&SystemID=" + _SystemID + "&SubSystemID=" + _SubSystemID + "&SubSystemCode=" + _SubSystemCode + "&DeptCode=" + _DepartmentCode;
                    $('#dvMoveJobsPopUp').prop('title', 'Move Jobs'); /* Title is changed from Copy Jobs To Move Jobs */
                    showModal('dvMoveJobsPopUp');

                    // return true;
                }
                else {
                    alert('Please select job/s to move.');
                }
            },
            error: function (response) {
                alert(response.d);
            }
        });

    }
    else {
        alert('Please select job/s to move.');
    }
    return false;
}
function onAddSpare() {
    clearSpareItemsFields();
    GetItemCategory("469", "");
    GetUnits();
    $("#divAddSpare").prop('title', 'Add Spare/Store Item');
    showModal('divAddSpare', false);
    document.getElementById("ctl00_MainContent_hdnItemOperationMode").value = "ADD";
    document.getElementById("ctl00_MainContent_lnkImageUploadName").style.display = "none";
    document.getElementById("ctl00_MainContent_lnkProductDetailUploadName").style.display = "none";
    return true;
}
function onEditSpare(itemid, evt, objthis) {
    clearSpareItemsFields();
    document.getElementById("ctl00_MainContent_hdnItemID").value = itemid;
    GetItemCategory("469", "");
    GetUnits();
    document.getElementById("ctl00_MainContent_hdnItemOperationMode").value = "EDIT";
    GetSpare(itemid);

   
    $("#divAddSpare").prop('title', 'Edit Spare/Store Item');
    showModal('divAddSpare', false);


}

function GetSpare(itemid) {


    var params = { ItemID: itemid };
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "../../JIBEWebService.asmx/Get_Spare",
        data: JSON.stringify(params),
        dataType: "json",
        success: function (Result) {
            Result = Result.d;


            if (Result.length > 0) {

                document.getElementById("ctl00_MainContent_txtItemPartNumber").value = Result[0].PART_NUM;
                document.getElementById("ctl00_MainContent_txtItemName").value = Result[0].SHORT_DESC;
                document.getElementById("ctl00_MainContent_txtItemDescription").value = Result[0].LONG_DESC;
                document.getElementById("ctl00_MainContent_txtItemDrawingNumber").value = Result[0].DRAW_NUM;
                document.getElementById("ctl00_MainContent_txtMinQty").value = Result[0].MINQty;
                document.getElementById("ctl00_MainContent_txtMaxQty").value = Result[0].MAXQty;
                if (Result[0].IMG_URL != "") {
                    document.getElementById("ctl00_MainContent_lnkImageUploadName").href = "../../Uploads/PURC_Items/" + Result[0].IMG_URL;
                    document.getElementById("ctl00_MainContent_lnkImageUploadName").style.display = "inline-block";
                }
                else {

                    document.getElementById("ctl00_MainContent_lnkImageUploadName").style.display = "none";
                }
                if (Result[0].PROD_DETAILS != "") {
                    document.getElementById("ctl00_MainContent_lnkProductDetailUploadName").href = "../../Uploads/PURC_Items/" + Result[0].PROD_DETAILS;
                    document.getElementById("ctl00_MainContent_lnkProductDetailUploadName").style.display = "inline-block";
                }
                else {
                    document.getElementById("ctl00_MainContent_lnkProductDetailUploadName").style.display = "none";
                }
                document.getElementById("ctl00_MainContent_ddlItemCategory").value = Result[0].ITEM_CAT;
                _MGSSelectedItemCategory = Result[0].ITEM_CAT;
                _MGSSelectedUnits = Result[0].UNIT;
                document.getElementById("ctl00_MainContent_ddlUnit").value = Result[0].UNIT;
                document.getElementById("ctl00_MainContent_hdnImageURL").value = Result[0].IMG_URL;
                document.getElementById("ctl00_MainContent_hdnProductURL").value = Result[0].PROD_DETAILS;

                if (Result[0].CRITCAL == "1") {
                    document.getElementById('ctl00_MainContent_chkItemCritical').checked = true;
                }
                else {
                    document.getElementById('ctl00_MainContent_chkItemCritical').checked = false;

                }





                document.getElementById("ctl00_MainContent_lblSpareCreatedBy").innerHTML = Result[0].CREATED_BY;
                document.getElementById("ctl00_MainContent_lblSpareModifiedBy").innerHTML = Result[0].MODIFY_BY;
                document.getElementById("ctl00_MainContent_lblSpareDeletedBy").innerHTML = Result[0].DELETED_BY;
            }

        },
        error: function (Result) {
            alert("Error");
        }
    });

}
function onDeleteSpare(itemid, evt, objthis) {

    var res = confirm('Are you sure, you want to  delete ?');

    if (res == true) {
        var UserID = document.getElementById("ctl00_MainContent_hdnUserID").value;
        var params = { userid: UserID, ItemID: itemid };
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "../../JIBEWebService.asmx/Delete_Spare",
            data: JSON.stringify(params),
            dataType: "json",
            success: function (Result) {

                var searchtext = document.getElementById($('[id$=txtSearchSpare]').attr('id')).value;
                if (searchtext == "") {

                    searchtext = null;
                }
                Get_Department(_SystemCode);
                Get_Lib_Items(selected_department, _SystemCode, _SubSystemCode, _Vessel_ID, null, null, null, null, _ActiveItemStatus, searchtext, null, _sort_direction, 1, 15, 1, parseInt("1"), "tblSpare");
                Get_Lib_ItemsCount(selected_department, _SystemCode, _SubSystemCode, _Vessel_ID, null, null, null, null, _ActiveItemStatus, null, null, null, parseInt("1"), parseInt("15"), 1, parseInt("1"), "tblSpare");


            },
            error: function (Result) {
                alert("Error");
            }
        });
    }
}
function onRestoreSpare(itemid, evt, objthis) {




    var UserID = document.getElementById("ctl00_MainContent_hdnUserID").value;
    var params = { userid: UserID, ItemID: itemid };
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "../../JIBEWebService.asmx/Restore_Spare",
        data: JSON.stringify(params),
        dataType: "json",
        success: function (Result) {

            var searchtext = document.getElementById($('[id$=txtSearchSpare]').attr('id')).value;
            if (searchtext == "") {

                searchtext = null;
            }
            Get_Department(_SystemCode);
            Get_Lib_Items(selected_department, _SystemCode, _SubSystemCode, _Vessel_ID, null, null, null, null, _ActiveItemStatus, searchtext, null, _sort_direction, 1, 15, 1, parseInt("1"), "tblSpare");
            Get_Lib_ItemsCount(selected_department, _SystemCode, _SubSystemCode, _Vessel_ID, null, null, null, null, _ActiveItemStatus, null, null, null, parseInt("1"), parseInt("15"), 1, parseInt("1"), "tblSpare");


        },
        error: function (Result) {
            alert("Error");
        }
    });

}
function onFilterSpare(radiobuttonlist) {

    var rdbBoxCount = radiobuttonlist.getElementsByTagName("input");
    if (rdbBoxCount[0].checked) {

        _ActiveItemStatus = null;
    }
    else if (rdbBoxCount[1].checked) {
        _ActiveItemStatus = rdbBoxCount[1].value;
    }
    else if (rdbBoxCount[2].checked) {

        _ActiveItemStatus = rdbBoxCount[2].value;
    }
    var searchtext = document.getElementById($('[id$=txtSearchSpare]').attr('id')).value;
    if (searchtext == "") {

        searchtext = null;
    }
    Get_Department(_SystemCode);
    Get_Lib_Items(selected_department, _SystemCode, _SubSystemCode, _Vessel_ID, null, null, null, null, _ActiveItemStatus, searchtext, null, _sort_direction, 1, 15, 1, parseInt("1"), "tblSpare");
    Get_Lib_ItemsCount(selected_department, _SystemCode, _SubSystemCode, _Vessel_ID, null, null, null, null, _ActiveItemStatus, searchtext, null, _sort_direction, parseInt("1"), parseInt("15"), 1, parseInt("1"), "tblSpare");
}
function GetSpareBeforeSave() {

    document.getElementById('ctl00_MainContent_hdnItemUnits').value = document.getElementById('ctl00_MainContent_ddlUnit').value;
    document.getElementById('ctl00_MainContent_hdnItemCategory').value = document.getElementById('ctl00_MainContent_ddlItemCategory').value;


    return false;
}

function onSaveSpare() {

    var imageuploaded = false;
    var ItemOperationMode = document.getElementById("ctl00_MainContent_hdnItemOperationMode").value;
    var VesselId = _Vessel_ID;    //document.getElementById($('[id$=ddlVessID]').attr('id')).value;
    var UserID = document.getElementById("ctl00_MainContent_hdnUserID").value;
    var SubSystemCode = document.getElementById("ctl00_MainContent_hdnSubSysCode").value;
    var SystemCode = document.getElementById("ctl00_MainContent_hdnSysCode").value;
    var PartNumber = document.getElementById("ctl00_MainContent_txtItemPartNumber").value;
    var ItemName = document.getElementById("ctl00_MainContent_txtItemName").value;
    var ItemDescription = document.getElementById("ctl00_MainContent_txtItemDescription").value;
    var ItemDrawingNumber = document.getElementById("ctl00_MainContent_txtItemDrawingNumber").value;
    var Unit = document.getElementById('ctl00_MainContent_ddlUnit').value;
    var itemid = document.getElementById("ctl00_MainContent_hdnItemID").value;
    var MinQty = document.getElementById('ctl00_MainContent_txtMinQty').value;
    var MaxQty = document.getElementById('ctl00_MainContent_txtMaxQty').value;
    var ItemCategory = parseInt(document.getElementById("ctl00_MainContent_ddlItemCategory").value);
    var MinQtyBD = MinQty.split(".")
    var MaxQtyBD = MaxQty.split(".")
    if (PartNumber == "") {
        alert("Part number is required.");
        document.getElementById('ctl00_MainContent_txtItemPartNumber').focus();
        return false;
    }
    else if (ItemName == "") {
        alert("Item Name is required.");
        // document.getElementById('ctl00_MainContent_txtItemName').focus();
        return false;
    }


    else if (parseFloat(MaxQty) < parseFloat(MinQty)) {
        alert('Max quantiy should not be less than min quantity.');
        // document.getElementById('ctl00_MainContent_txtMaxQty').focus();
        return false;
    }


    else if ((MaxQty != "") && isNaN(MaxQty)) {

        alert('Max quantiy is accept only numeric value.')
        //  document.getElementById('ctl00_MainContent_txtMaxQty').focus();
        return false;

    }



    else if ((MinQtyBD[0].length > "16")) {

        alert('Min Qty should not exceed 16 digit before decimal');
        // document.getElementById('ctl00_MainContent_txtMinQty').focus();
        return false;

    }
    else if ((MaxQtyBD[0].length > "16")) {

        alert('Max Qty should not exceed 16 digit before decimal');
        // document.getElementById('ctl00_MainContent_txtMinQty').focus();
        return false;

    }

    else {


        if (parseInt(document.getElementById("ctl00_MainContent_ddlItemCategory").value)) {

            var ItemCategory = parseInt(document.getElementById("ctl00_MainContent_ddlItemCategory").value);

        }
        else {

            var ItemCategory = null;
        }

        //                $.ajax({
        //                    type: "POST",
        //                    url: "../../Technical/PMS/PMS_Manage_System_SubSystem.aspx/ImageUpload",
        //                    data: "{}",
        //                    contentType: "application/json; charset=utf-8",
        //                    dataType: "json",
        //                    success: function (response) {

        //                        var res = response.d.split(",");
        //                        document.getElementById("ctl00_MainContent_hdnImageURL").value = res[0];
        //                        document.getElementById("ctl00_MainContent_hdnProductURL").value = res[1];

        if (document.getElementById("ctl00_MainContent_hdnImageURL").value == "") {
            var imageURL = null
        }
        else {
            var imageURL = document.getElementById("ctl00_MainContent_hdnImageURL").value
        }
        if (document.getElementById("ctl00_MainContent_hdnProductURL").value == "") {
            var ProductURL = null
        }
        else {
            var ProductURL = document.getElementById("ctl00_MainContent_hdnProductURL").value;
        }




        if (document.getElementById('ctl00_MainContent_chkItemCritical').checked == true) {
            var ItemCritical = "1";
        }
        else {
            var ItemCritical = "0";
        }

        var d = Math.pow(10, 2);
        MinQty = (Math.round(parseFloat(MinQty) * d) / d).toFixed(2);
        MaxQty = (Math.round(parseFloat(MaxQty) * d) / d).toFixed(2);

        if (ItemOperationMode == "ADD") {
            var params = { userid: parseInt(UserID), systemcode: SystemCode, subsystemcode: SubSystemCode, vesselid: VesselId, partnumber: PartNumber, name: ItemName, description: ItemDescription, drawingnumber: ItemDrawingNumber, unit: Unit, inventorymin: MinQty, inverntorymax: MaxQty, itemcategory: ItemCategory, image_url: imageURL, product_details: ProductURL, critical_flag: ItemCritical };

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "../../JIBEWebService.asmx/LibraryItemSave",
                data: JSON.stringify(params),
                dataType: "json",
                success: function (Result) {
                    Result = Result.d;
                    //    document.getElementById("ctl00_MainContent_txtCatalogueCode").value = Result[0].SYS_CODE;

                    //onAddSpare();
                    var searchtext = document.getElementById($('[id$=txtSearchSpare]').attr('id')).value;
                    if (searchtext == "") {

                        searchtext = null;
                    }
                    Get_Department(_SystemCode);
                    Get_Lib_Items(selected_department, _SystemCode, _SubSystemCode, _Vessel_ID, null, null, null, null, _ActiveItemStatus, searchtext, null, null, parseInt("1"), parseInt("15"), 1, parseInt("1"), "tblSpare");
                    /*Result=0 Item name with same part no. already exists.
                    -1 any error */
                    if (Result == '0') {
                        alert("Item exists with same Part No/Drawing No.");
                    }
                    else if (Result == '-1') 
                    {
                        alert("Error occurred while saving.");
                    }
                    else {

                        alert("Item Added Successfully");
                        onEditSpare(Result, "", "");
                    }
                    //hideModal('divAddSpare');
                    Get_Lib_ItemsCount(selected_department, _SystemCode, _SubSystemCode, _Vessel_ID, null, null, null, null, _ActiveItemStatus, null, null, null, parseInt("1"), parseInt("15"), 1, parseInt("1"), "tblSpare");
                    ItemOperationMode = "EDIT";
                },
                error: function (Result) {
                    alert("Error");
                }
            });

        }
        else if (ItemOperationMode == "EDIT") {
            var params = { userid: parseInt(UserID), itemid: itemid, systemcode: SystemCode, subsystemcode: SubSystemCode, vesselid: VesselId, partnumber: PartNumber, name: ItemName, description: ItemDescription, drawingnumber: ItemDrawingNumber, unit: Unit, inventorymin: MinQty, inventorymax: MaxQty, itemcategory: ItemCategory, image_url: imageURL, product_details: ProductURL, critical_flag: ItemCritical };
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "../../JIBEWebService.asmx/LibraryItemUpdate",
                data: JSON.stringify(params),
                dataType: "json",
                success: function (Result) {
                    Result = Result.d;

                    //    document.getElementById("ctl00_MainContent_txtCatalogueCode").value = Result[0].SYS_CODE;

                    // onEditSpare(itemid);
                    var searchtext = document.getElementById($('[id$=txtSearchSpare]').attr('id')).value;
                    if (searchtext == "") {

                        searchtext = null;
                    }
                    Get_Department(_SystemCode);
                    Get_Lib_Items(selected_department, _SystemCode, _SubSystemCode, _Vessel_ID, null, null, null, null, _ActiveItemStatus, searchtext, null, null, parseInt("1"), parseInt("15"), 1, parseInt("1"), "tblSpare");
                    alert("Item Edited Successfully");
                    hideModal('divAddSpare');
                    ItemOperationMode = "";

                },
                error: function (Result) {
                    alert("Error");
                }
            });
        }
       

    }

    return false;
}
function onAttachSpareImage() {

    var imageuploaded = false;
    var ItemOperationMode = document.getElementById("ctl00_MainContent_hdnItemOperationMode").value;
    var VesselId = _Vessel_ID;    //document.getElementById($('[id$=ddlVessID]').attr('id')).value;
    var UserID = document.getElementById("ctl00_MainContent_hdnUserID").value;
    var SubSystemCode = document.getElementById("ctl00_MainContent_hdnSubSysCode").value;
    var SystemCode = document.getElementById("ctl00_MainContent_hdnSysCode").value;
    var PartNumber = document.getElementById("ctl00_MainContent_txtItemPartNumber").value;
    var ItemName = document.getElementById("ctl00_MainContent_txtItemName").value;
    var ItemDescription = document.getElementById("ctl00_MainContent_txtItemDescription").value;
    var ItemDrawingNumber = document.getElementById("ctl00_MainContent_txtItemDrawingNumber").value;
    var Unit = document.getElementById('ctl00_MainContent_ddlUnit').value;
    var itemid = document.getElementById("ctl00_MainContent_hdnItemID").value;
    var MinQty = parseFloat(document.getElementById('ctl00_MainContent_txtMinQty').value);
    var MaxQty = parseFloat(document.getElementById('ctl00_MainContent_txtMaxQty').value);
    var ItemCategory = parseInt(document.getElementById("ctl00_MainContent_ddlItemCategory").value);

    if (parseInt(document.getElementById("ctl00_MainContent_ddlItemCategory").value)) {

        var ItemCategory = parseInt(document.getElementById("ctl00_MainContent_ddlItemCategory").value);

    }
    else {

        var ItemCategory = null;
    }


    if (document.getElementById("ctl00_MainContent_hdnImageURL").value == "") {
        var imageURL = null
    }
    else {
        var imageURL = document.getElementById("ctl00_MainContent_hdnImageURL").value
    }
    if (document.getElementById("ctl00_MainContent_hdnProductURL").value == "") {
        var ProductURL = null
    }
    else {
        var ProductURL = document.getElementById("ctl00_MainContent_hdnProductURL").value;
    }




    if (document.getElementById('ctl00_MainContent_chkItemCritical').checked == true) {
        var ItemCritical = "1";
    }
    else {
        var ItemCritical = "0";
    }

       
    if (ItemOperationMode == "EDIT") {
        var params = { userid: parseInt(UserID), itemid: itemid, systemcode: SystemCode, subsystemcode: SubSystemCode, vesselid: VesselId, partnumber: PartNumber, name: ItemName, description: ItemDescription, drawingnumber: ItemDrawingNumber, unit: Unit, inventorymin: MinQty, inventorymax: MaxQty, itemcategory: ItemCategory, image_url: imageURL, product_details: ProductURL, critical_flag: ItemCritical };

        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "../../JIBEWebService.asmx/LibraryItemUpdate",
            data: JSON.stringify(params),
            dataType: "json",
            success: function (Result) {
                Result = Result.d;
                var searchtext = document.getElementById($('[id$=txtSearchSpare]').attr('id')).value;
                if (searchtext == "") {
                    searchtext = null;
                }
                GetSpare(itemid);
                Get_Department(_SystemCode);
                Get_Lib_Items(selected_department, _SystemCode, _SubSystemCode, _Vessel_ID, null, null, null, null, _ActiveItemStatus, searchtext, null, null, parseInt("1"), parseInt("15"), 1, parseInt("1"), "tblSpare");
            },
            error: function (Result) {
                alert("Error");
            }
        });
    }


    return false;
}
function onBtnDivAddLocation() {

    onGetLocation();
    document.getElementById($('[id$=ctl00_MainContent_txtSearchLocation]').attr('id')).value = "";           // added by Reshma : JIT 11390
    $('#divAddLocation').prop('title', 'Assign Location');
    showModal('divAddLocation', false, fnCloseAssignLocation);
    return false;
    
}

function fnCloseAssignLocation() {

    document.getElementById($('[id$=dvAddNewLocation]').attr('id')).style.display = "none"

    document.getElementById($('[id$=divEditLocation]').attr('id')).style.display = "none"

    document.getElementById($('[id$=gvLocation]').attr('id')).innerHTML = "";

    document.getElementById($('[id$=ctl00_MainContent_txtSearchLocation]').attr('id')).value = "";    // added by Reshma : JIT 11390

    document.getElementById($('[id$=ctl00_MainContent_txtLoc_Description]').attr('id')).value = "";   // added by Reshma : JIT 11390

   
    var CatalogOperationMode = document.getElementById("ctl00_MainContent_hdnCatalogOperationMode").value;            // added by Reshma : JIT 11389
    var SubCatalogOperationMode = document.getElementById("ctl00_MainContent_hdnSubCatalogOperationMode").value;

    if (CatalogOperationMode == "ADD") {
        CatalogOperationMode = "";
        hideModal("divAddLocation");
        $("#divAddSystem").prop('title', 'Add System');
        showModal('divAddSystem', false);

    }

    else if (CatalogOperationMode == "EDIT") {
        CatalogOperationMode = "";
        hideModal("divAddLocation");
        onEditSystem();



    }
    if (SubCatalogOperationMode == "ADD") {
        SubCatalogOperationMode = "";
        hideModal("divAddLocation");
        $("#divAddSubSystem").prop('title', 'Add Sub System');
        showModal('divAddSubSystem', false);
    }
    else if (SubCatalogOperationMode == "EDIT") {
        SubCatalogOperationMode = "";
        hideModal("divAddLocation");
        onEditSubSystem();

    }

}
function onGetLocation() {
    var searchText = document.getElementById("ctl00_MainContent_txtSearchLocation").value;
    
    Get_Lib_Locations(_SystemCode, _SubSystemCode, searchText, _Vessel_ID, null, null, parseInt("1"), parseInt("15"), null, "tblLocation");
    document.getElementById($('[id$=ctl00_MainContent_txtSearchLocation]').attr('id')).value = "";   // added by Reshma : JIT 11390
}

function OnSaveNewLocation() {

   
    var SubCatalogOperationMode = document.getElementById("ctl00_MainContent_hdnSubCatalogOperationMode").value;
    var UserID = document.getElementById("ctl00_MainContent_hdnUserID").value;
    var ShortCode = null;  //document.getElementById("ctl00_MainContent_txtLoc_ShortCode").value;
    var ShortDescription = document.getElementById("ctl00_MainContent_txtLoc_Description").value;

    var table = document.getElementById("tblLocation");
    var UserID = document.getElementById("ctl00_MainContent_hdnUserID").value;

    var CatalogOperationMode = document.getElementById("ctl00_MainContent_hdnCatalogOperationMode").value;
    var SubCatalogOperationMode = document.getElementById("ctl00_MainContent_hdnSubCatalogOperationMode").value;
    var SysCode = null;
    var SubSysID = null;
    var Category_Code = "AC";

    if (ShortDescription == "") {
        alert("Location Name is required.");
        document.getElementById('ctl00_MainContent_txtLoc_Description').focus();
        return false;
    }
    if (CatalogOperationMode == "ADD") {

        SysCode = document.getElementById("ctl00_MainContent_txtCatalogueCode").value;
    }

    else if (CatalogOperationMode == "EDIT") {
        SysCode = document.getElementById("ctl00_MainContent_hdnSysCode").value;

    }
    if (SubCatalogOperationMode == "ADD") {

        SysCode = document.getElementById("ctl00_MainContent_hdnSysCode").value;
        SubSysID = null;
    }
    else if (SubCatalogOperationMode == "EDIT") {
        SysCode = document.getElementById("ctl00_MainContent_hdnSysCode").value;
        SubSysID = document.getElementById("ctl00_MainContent_hdnSubSysID").value;

    }


    // var UserID = $('[id$=hdnUserID]').val();
    // var params = { userid: UserID, ShortCode: ShortCode, ShortDescription: ShortDescription };
    var params = { ShortCode: ShortCode, ShortDesc: ShortDescription, SystemCode: SysCode, SubSysCode: SubSysID, VesselID: _Vessel_ID, CatCode: Category_Code, CreatedBy: UserID };
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "../../JIBEWebService.asmx/PMS_Insert_AssignLocation",
        data: JSON.stringify(params),
        dataType: "json",
        success: function (Result) {
            Result = Result.d;


            //    document.getElementById("ctl00_MainContent_txtCatalogueCode").value = Result[0].SYS_CODE;
            //  hideModal('divAddSpare');
            // onAddJob();

            //  document.getElementById("ctl00_MainContent_txtLoc_ShortCode").value = "";
            document.getElementById("ctl00_MainContent_txtLoc_Description").value = "";
            $('#divAddLocation').prop('title', 'Assign Location');
            showModal('divAddLocation', false);
            document.getElementById('dvAddNewLocation').style.display = 'none';

            onGetLocation();
            alert("Location Created Successfully");
            if (CatalogOperationMode == "ADD") {
                CatalogOperationMode = "";
                hideModal("divAddLocation");
                $("#divAddSystem").prop('title', 'Add System');
                showModal('divAddSystem', false);

            }

            else if (CatalogOperationMode == "EDIT") {
                CatalogOperationMode = "";
                hideModal("divAddLocation");
                onEditSystem();



            }
            if (SubCatalogOperationMode == "ADD") {
                SubCatalogOperationMode = "";
                hideModal("divAddLocation");
                $("#divAddSubSystem").prop('title', 'Add Sub System');
                showModal('divAddSubSystem', false);
            }
            else if (SubCatalogOperationMode == "EDIT") {
                SubCatalogOperationMode = "";
                hideModal("divAddLocation");
                onEditSubSystem();

            }
        },
        error: function (Result) {
            alert("Error");
        }
    });



    return false;
}
function onSaveAssignLocation() {

    var table = document.getElementById("tblLocation");
    var UserID = document.getElementById("ctl00_MainContent_hdnUserID").value;

    var CatalogOperationMode = document.getElementById("ctl00_MainContent_hdnCatalogOperationMode").value;
    var SubCatalogOperationMode = document.getElementById("ctl00_MainContent_hdnSubCatalogOperationMode").value;
    var SysCode = null;
    var SubSysCode = null;
    if (CatalogOperationMode == "ADD") {

        SysCode = document.getElementById("ctl00_MainContent_txtCatalogueCode").value;
    }

    else if (CatalogOperationMode == "EDIT") {
        SysCode = document.getElementById("ctl00_MainContent_hdnSysCode").value;

    }
    if (SubCatalogOperationMode == "ADD") {

        SysCode = document.getElementById("ctl00_MainContent_hdnSysCode").value;
        SubSysCode = null;
    }
    else if (SubCatalogOperationMode == "EDIT") {
        SysCode = document.getElementById("ctl00_MainContent_hdnSysCode").value;
        SubSysCode = document.getElementById("ctl00_MainContent_hdnSubSysID").value;

    }


    for (var i = 1, row; row = table.rows[i]; i++) {

        var Locid = "tblLocation_LocAssignFlag" + i;
        var Catid = "tblLocation_Category_Code" + i;
        var chkLoc = document.getElementById(Locid);
        var chkCat = document.getElementById(Catid);
        // if ((chkLoc.checked == true) && (chkLoc.disabled == false)) {

        if (chkCat.checked == true) {
            var Category_Code = "SP";
        }
        else {
            var Category_Code = "AC";
        }
        if (chkCat.disabled != true)
            SaveAssignLocations(UserID, SysCode, SubSysCode, row.cells[0].textContent, _Vessel_ID, Category_Code)

        //  }

    }

    alert("Location Updated Successfully");

    document.getElementById($('[id$=ctl00_MainContent_txtLoc_Description]').attr('id')).value = "";    // added by Reshma : JIT 11390





    return false;

}


function SaveAssignLocations(userid, systemcode, SubSystemCode, locationid, vesselid, Category_Code) {

    // var UserID = $('[id$=hdnUserID]').val();
    var CatalogOperationMode = document.getElementById("ctl00_MainContent_hdnCatalogOperationMode").value;
    var SubCatalogOperationMode = document.getElementById("ctl00_MainContent_hdnSubCatalogOperationMode").value;
    document.getElementById("ctl00_MainContent_lstSubCatVesselLocation").innerHTML = "";
    var params = { systemcode: systemcode, SubSystemCode: SubSystemCode, CategoryCode: Category_Code, ModifiedBy: parseInt(userid), LocationCode: parseInt(locationid), VesselID: parseInt(vesselid) };
    // var params = { userid: parseInt(userid), systemcode: systemcode, SubSystemCode: SubSystemCode, locationid: parseInt(locationid), vesselid: parseInt(vesselid), Category_Code: Category_Code };
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "../../JIBEWebService.asmx/PMS_Update_AssignLocationStatus",
        data: JSON.stringify(params),
        dataType: "json",
        success: function (Result) {
            Result = Result.d;


            //    document.getElementById("ctl00_MainContent_txtCatalogueCode").value = Result[0].SYS_CODE;
            // hideModal('divAddSpare');
            // onAddJob();

            // JobOperationMode = "";
            // 
            if (CatalogOperationMode == "ADD") {
                CatalogOperationMode = "";
                hideModal("divAddLocation");
                $("#divAddSystem").prop('title', 'Add System');
                showModal('divAddSystem', false);

            }

            else if (CatalogOperationMode == "EDIT") {
                CatalogOperationMode = "";
                hideModal("divAddLocation");
                onEditSystem();



            }
            if (SubCatalogOperationMode == "ADD") {
                SubCatalogOperationMode = "";
                hideModal("divAddLocation");
                $("#divAddSubSystem").prop('title', 'Add Sub System');
                showModal('divAddSubSystem', false);
            }
            else if (SubCatalogOperationMode == "EDIT") {
                SubCatalogOperationMode = "";
                hideModal("divAddLocation");
                onEditSubSystem();

            }

        },
        error: function (Result) {
            alert(Result.d);
        }
    });

    return true;

}

function onDeleteSystemAssignLocation() {     //Changes done by Reshma : JIT 11222

    var SetInstalled = document.getElementById("ctl00_MainContent_txtCatalogueSetInstalled").value;
    var CatalogOperationMode = document.getElementById("ctl00_MainContent_hdnCatalogOperationMode").value;
    var SubCatalogOperationMode = document.getElementById("ctl00_MainContent_hdnSubCatalogOperationMode").value;
    var SelectedLocationValue = document.getElementById("ctl00_MainContent_lstcatalogLocation").value;
    if (SelectedLocationValue == "") {
        alert("Please select location to delete.");
        return false;
    }
   
    var UserID = document.getElementById("ctl00_MainContent_hdnUserID").value;
    if (CatalogOperationMode == "EDIT") {
     
     var params = { SystemLocationID: parseInt(SelectedLocationValue), SubSysLocationID: null, VESSELID: parseInt(_Vessel_ID) };
     $.ajax({
         type: "POST",
         contentType: "application/json; charset=utf-8",
         url: "../../JIBEWebService.asmx/PMS_GET_JobsPerformOnLocation",
         data: JSON.stringify(params),
         dataType: "json",
         success: function (Result) {

             Result = Result.d;
             if (Result > 0) {


                 var conf = confirm("Jobs are perform on this location and deleting location may cause jobs to be removed from history page.");
                 if (conf == true) {

                     if (parseInt(SetInstalled) <= 1) {

                         alert("Last location from system can not be deleted.");
                         document.getElementById("ctl00_MainContent_txtCatalogueSetInstalled").focus();
                         return false;

                     }
                     if (CatalogOperationMode == "ADD") {
                         CatalogOperationMode = "";
                         if (!confirm("Are you sure you want to delete this location?")) {
                             return false;
                         }
                     }

                     else if (CatalogOperationMode == "EDIT") {
                         CatalogOperationMode = "";
                         if (!confirm("Are you sure you want to delete this location?")) {
                             return false;
                         }

                     }

                     var params = { userid: parseInt(UserID), AssignlocationID: parseInt(SelectedLocationValue), vesselcode: parseInt(_Vessel_ID) };
                     $.ajax({
                         type: "POST",
                         contentType: "application/json; charset=utf-8",
                         url: "../../JIBEWebService.asmx/LibraryCatalogueLocationAssignDelete",
                         data: JSON.stringify(params),
                         dataType: "json",
                         success: function (Result) {

                             Result = Result.d;

                             GetCatalogList();
                             BindSystemAssignLocation();



                         },
                         error: function (Result) {
                             alert("Error");
                         }
                     });
                 }
             }
             else

                 if (parseInt(SetInstalled) <= 1) {

                     alert("Last location from system can not be deleted.");
                     document.getElementById("ctl00_MainContent_txtCatalogueSetInstalled").focus();
                     return false;

                 }
             if (CatalogOperationMode == "ADD") {
                 CatalogOperationMode = "";
                 if (!confirm("Are you sure you want to delete this location?")) {
                     return false;
                 }


             }

             else if (CatalogOperationMode == "EDIT") {
                 CatalogOperationMode = "";
                 if (!confirm("Are you sure you want to delete this location?")) {
                     return false;
                 }


             }
             var params = { userid: parseInt(UserID), AssignlocationID: parseInt(SelectedLocationValue), vesselcode: parseInt(_Vessel_ID) };
             $.ajax({
                 type: "POST",
                 contentType: "application/json; charset=utf-8",
                 url: "../../JIBEWebService.asmx/LibraryCatalogueLocationAssignDelete",
                 data: JSON.stringify(params),
                 dataType: "json",
                 success: function (Result) {

                     Result = Result.d;

                     GetCatalogList();
                     BindSystemAssignLocation();

                 },
                 error: function (Result) {
                     alert("Error");
                 }
             });


         },

         error: function (Result) {
             alert("Error");
         }
     });

   }

  
    if (SubCatalogOperationMode == "ADD") {
        SubCatalogOperationMode = "";
        if (!confirm("Are you sure you want to delete this location?")) {
            return false;
        }
        BindSubSystemAssignLocation();

    }
    else if (SubCatalogOperationMode == "EDIT") {
        SubCatalogOperationMode = "";
        if (!confirm("Are you sure you want to delete this location?")) {
            return false;
        }
        BindSubSystemAssignLocation();

    }


    return false;
    
}


function onDeleteSubSystemAssignLocation() {          //Changes done by Reshma : JIT 11222

    var SetInstalled = document.getElementById("ctl00_MainContent_txtSubCatSetInstalled").value;
    var CatalogOperationMode = document.getElementById("ctl00_MainContent_hdnCatalogOperationMode").value;
    var SubCatalogOperationMode = document.getElementById("ctl00_MainContent_hdnSubCatalogOperationMode").value;
    var SelectedLocationValue = document.getElementById("ctl00_MainContent_lstSubCatVesselLocation").value;
    if (SelectedLocationValue == "") {
        alert("Please select location to delete.");
        return false;
    }
    var UserID = document.getElementById("ctl00_MainContent_hdnUserID").value;
    if (SubCatalogOperationMode == "EDIT") {

        var params = { SystemLocationID: null, SubSysLocationID: parseInt(SelectedLocationValue), VESSELID: parseInt(_Vessel_ID) };
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "../../JIBEWebService.asmx/PMS_GET_JobsPerformOnLocation",
            data: JSON.stringify(params),
            dataType: "json",
            success: function (Result) {

                Result = Result.d;
                if (Result > 0) {


                    var conf = confirm("Jobs are perform on this location and deleting location may cause jobs to be removed from history page.");
                    if (conf == true) {

                        if (parseInt(SetInstalled) <= 1) {
                            alert("Last location from subsystem can not be deleted.");
                            document.getElementById("ctl00_MainContent_txtCatalogueSetInstalled").focus();
                            return false;
                        }

                        if (SubCatalogOperationMode == "ADD") {
                            SubCatalogOperationMode = "";
                            if (!confirm("Are you sure you want to delete this location?")) {
                                return false;
                            }
                           

                        }
                        else if (SubCatalogOperationMode == "EDIT") {
                            SubCatalogOperationMode = "";
                            if (!confirm("Are you sure you want to delete this location?")) {
                                return false;
                            }
                            
                        }

                        var params = { userid: parseInt(UserID), AssignlocationID: parseInt(SelectedLocationValue), vesselcode: parseInt(_Vessel_ID) };
                        $.ajax({
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            url: "../../JIBEWebService.asmx/LibraryCatalogueLocationAssignDelete",
                            data: JSON.stringify(params),
                            dataType: "json",
                            success: function (Result) {
                                Result = Result.d;
                                
                                GetSubCatalogList();
                                BindSubSystemAssignLocation();
 

                            },
                            error: function (Result) {
                                alert("Error");
                            }
                        });

                    }
                }
                else {

                    if (parseInt(SetInstalled) <= 1) {

                        alert("Last location from subsystem can not be deleted.");
                        document.getElementById("ctl00_MainContent_txtCatalogueSetInstalled").focus();
                        return false;

                    }

                    if (SubCatalogOperationMode == "ADD") {
                        SubCatalogOperationMode = "";
                        if (!confirm("Are you sure you want to delete this location?")) {
                            return false;
                        }
                      

                    }
                    else if (SubCatalogOperationMode == "EDIT") {
                        SubCatalogOperationMode = "";
                        if (!confirm("Are you sure you want to delete this location?")) {
                            return false;
                        }

                    }

                   if (SelectedLocationValue != "" && SetInstalled != 1) {

                var params = { userid: parseInt(UserID), AssignlocationID: parseInt(SelectedLocationValue), vesselcode: parseInt(_Vessel_ID) };
                        $.ajax({
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            url: "../../JIBEWebService.asmx/LibraryCatalogueLocationAssignDelete",
                            data: JSON.stringify(params),
                            dataType: "json",
                            success: function (Result) {
                                Result = Result.d;
                                
                                GetSubCatalogList();
                                BindSubSystemAssignLocation();

                            },
                            error: function (Result) {
                                alert("Error");
                            }
                        });

                    }
                    
                    
                }
              

            },
            error: function (Result) {
                alert("Error");
            }
        });

    }

  
    if (CatalogOperationMode == "ADD") {
        CatalogOperationMode = "";
        if (!confirm("Are you sure you want to delete this location?")) {
            return false;
        }
        BindSystemAssignLocation();

    }

    else if (CatalogOperationMode == "EDIT") {
        CatalogOperationMode = "";
        if (!confirm("Are you sure you want to delete this location?")) {
            return false;
        }
        BindSystemAssignLocation();


    }


    return false;
}
function GetNextSystemCode() {

    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "../../JIBEWebService.asmx/Get_Next_System_Code",
        data: "{}",
        dataType: "json",
        success: function (Result) {
            Result = Result.d;


            document.getElementById("ctl00_MainContent_txtCatalogueCode").value = Result[0].SYS_CODE;



        },
        error: function (Result) {
            alert("Error");
        }
    });



}

function clearCatalogFields() {


    document.getElementById("ctl00_MainContent_txtCatalogueCode").value = "";
    document.getElementById("ctl00_MainContent_txtCatalogueSetInstalled").value = "";
    document.getElementById("ctl00_MainContent_txtCatalogName").value = "";
    document.getElementById("ctl00_MainContent_txtCatalogueParticular").value = "";
    document.getElementById('ctl00_MainContent_ddlCalalogueMaker').value = "0";
    document.getElementById('ctl00_MainContent_ddlCatalogDept').value = "0";
    document.getElementById("ctl00_MainContent_txtCatalogueModel").value = "";
    document.getElementById("ctl00_MainContent_txtCatalogueSerialNumber").value = "";
    document.getElementById('ctl00_MainContent_chkRunHourS').checked = false;
    document.getElementById('ctl00_MainContent_chkCriticalS').checked = false;
    document.getElementById('ctl00_MainContent_ddlCatalogFunction').value = "0";
    document.getElementById('ctl00_MainContent_ddlAccountCode').value = "6100";
    document.getElementById("ctl00_MainContent_lblCatalogueCreatedBy").innerHTML = "";
    document.getElementById("ctl00_MainContent_lblCatalogueModifiedby").innerHTML = "";
    document.getElementById("ctl00_MainContent_lblCatalogueDeletedby").innerHTML = "";
    document.getElementById("ctl00_MainContent_lstcatalogLocation").innerHTML = "";
}

function clearSubCatalogFields() {



    //document.getElementById("ctl00_MainContent_txtSubCatalogueCode").value = "";
    document.getElementById("ctl00_MainContent_txtSubCatSetInstalled").value = "";
    document.getElementById("ctl00_MainContent_txtSubCatalogueName").value = "";
    document.getElementById("ctl00_MainContent_txtSubCatalogueParticulars").value = "";
    document.getElementById('ctl00_MainContent_ddlSubCatMaker').value = "0";
    document.getElementById("ctl00_MainContent_txtSubCatModel").value = "";
    document.getElementById("ctl00_MainContent_txtSubCatSerialNo").value = "";
    document.getElementById('ctl00_MainContent_chkRunHourSS').checked = false;
    document.getElementById('ctl00_MainContent_chkCriticalSS').checked = false;
    //document.getElementById('ctl00_MainContent_chkCopyRunHourSS').checked = false;


    document.getElementById("ctl00_MainContent_lblSubCatalogueCreatedBy").innerHTML = "";
    document.getElementById("ctl00_MainContent_lblSubCatalogueModifiedby").innerHTML = "";
    document.getElementById("ctl00_MainContent_lblSubCatalogueDeletedBy").innerHTML = "";
    document.getElementById("ctl00_MainContent_lstSubCatVesselLocation").innerHTML = "";
}
function clearSpareItemsFields() {

    document.getElementById("ctl00_MainContent_hdnItemID").value = "";
    document.getElementById("ctl00_MainContent_txtItemPartNumber").value = "";
    document.getElementById("ctl00_MainContent_txtItemName").value = "";
    document.getElementById("ctl00_MainContent_txtItemDescription").value = "";
    document.getElementById("ctl00_MainContent_txtItemDrawingNumber").value = "";
    document.getElementById("ctl00_MainContent_txtMinQty").value = "0.00";
    document.getElementById("ctl00_MainContent_txtMaxQty").value = "0.00";
    document.getElementById("ctl00_MainContent_ddlUnit").value = "0";
    document.getElementById("ctl00_MainContent_ddlItemCategory").value = "0";
    document.getElementById('ctl00_MainContent_chkItemCritical').checked = false;
    document.getElementById('ctl00_MainContent_lnkImageUploadName').href = "";
    document.getElementById('ctl00_MainContent_lnkProductDetailUploadName').href = "";
    document.getElementById('ctl00_MainContent_hdnImageURL').innerHTML = "";
    document.getElementById('ctl00_MainContent_hdnProductURL').innerHTML = "";
    document.getElementById('ctl00_MainContent_hdnImageURL').value = "";
    document.getElementById('ctl00_MainContent_hdnProductURL').value = "";
    document.getElementById('ctl00_MainContent_hdnItemUnits').value = "";
    document.getElementById("ctl00_MainContent_hdnItemCategory").value = "";
    document.getElementById("ctl00_MainContent_lblSpareCreatedBy").innerHTML = "";
    document.getElementById("ctl00_MainContent_lblSpareModifiedBy").innerHTML = "";
    document.getElementById("ctl00_MainContent_lblSpareDeletedBy").innerHTML = "";
    _MGSSelectedItemCategory = "";
    _MGSSelectedUnits = "";

}



function clearJobsFields() {


    document.getElementById('ctl00_MainContent_lstDepartment').value = "2488";
    document.getElementById('ctl00_MainContent_lstFrequency').value = "2485";
    document.getElementById('ctl00_MainContent_ddlRank').value = "0";
    document.getElementById('ctl00_MainContent_txtJobTitle').value = "";
    document.getElementById('ctl00_MainContent_txtjobDescription').value = "";
    document.getElementById('ctl00_MainContent_hdnJobID').value = "";
    document.getElementById('ctl00_MainContent_txtFrequency').value = "";

    var RB1 = document.getElementById("ctl00_MainContent_optCMS");
    var radio = RB1.getElementsByTagName("input");

    radio[1].checked = true;

    var RB1 = document.getElementById("ctl00_MainContent_optCritical");
    var radio = RB1.getElementsByTagName("input");
    radio[1].checked = true;

    document.getElementById('ctl00_MainContent_chkIsTechRequired').checked = false;
    document.getElementById('ctl00_MainContent_chkSafetyAlarm').checked = false;
    document.getElementById('ctl00_MainContent_chkCalibration').checked = false;
    document.getElementById('ctl00_MainContent_hdnJobCode').innerHTML = "";
    document.getElementById("ctl00_MainContent_lblItemCreatedBy").innerHTML = "";
    document.getElementById("ctl00_MainContent_lblItemmodifiedby").innerHTML = "";
    document.getElementById("ctl00_MainContent_lblItemDeletedBy").innerHTML = "";
    document.getElementById('ctl00_MainContent_ChkMandatoryRA').checked = false;
    document.getElementById('ctl00_MainContent_ChkMandatoryRAApproval').checked = false;

}

function GetSubCatalogList() {
    var params = { subsystemid: document.getElementById("ctl00_MainContent_hdnSubSysID").value };
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "../../JIBEWebService.asmx/Get_Sub_Catalog_List",
        data: JSON.stringify(params),
        dataType: "json",
        success: function (Result) {
            Result = Result.d;

            //                    var list = document.getElementById("ctl00_MainContent_ddlItemCategory");
            //                    list.add(new Option("--SELECT--", ""));
            //                    for (var i = 0; i < Result.length; i++) {

            //                        list.add(new Option(Result[i].DESCRIPTION, Result[i].CODE));
            //                    }
            if (Result.length > 0) {

                // document.getElementById("ctl00_MainContent_txtSubCatalogueCode").value = Result[0].SUBSYS_CODE;
                document.getElementById($('[id$=txtSubCatSetInstalled]').attr('id')).value = Result[0].SET_INSTALLED

                var setInstalled = document.getElementById($('[id$=txtSubCatSetInstalled]').attr('id')).value;
                if (setInstalled != "") {

                    if (parseInt(setInstalled) > 0) {

                        if ($('[id$=rblDeptType]').find(':checked').val() != "SP") {
                            document.getElementById($('[id$=txtSubCatSetInstalled]').attr('id')).disabled = true;
                            document.getElementById($('[id$=ImgSubCatAssLocation]').attr('id')).disabled = true;
                        }
                        else {
                            document.getElementById($('[id$=txtSubCatSetInstalled]').attr('id')).disabled = true;
                            document.getElementById($('[id$=ImgSubCatAssLocation]').attr('id')).disabled = false;
                        }
                    }
                }

                else if (setInstalled == "" || setInstalled == "0") {



                    if ($('[id$=rblDeptType]').find(':checked').val() != "SP") {
                        document.getElementById($('[id$=txtSubCatSetInstalled]').attr('id')).disabled = true;
                        document.getElementById($('[id$=ImgSubCatAssLocation]').attr('id')).disabled = true;
                    }
                    else {
                        document.getElementById($('[id$=txtSubCatSetInstalled]').attr('id')).disabled = false;
                        document.getElementById($('[id$=ImgSubCatAssLocation]').attr('id')).disabled = true;
                    }


                }
                document.getElementById("ctl00_MainContent_txtSubCatalogueName").value = Result[0].SUBSYS_DESC;
                document.getElementById("ctl00_MainContent_txtSubCatModel").value = Result[0].MODULE_TYPE;
                document.getElementById("ctl00_MainContent_txtSubCatalogueParticulars").value = Result[0].SUBSYS_PARTICULAR;
                document.getElementById("ctl00_MainContent_txtSubCatSerialNo").value = Result[0].SERIAL_NUMBER;
                if (Result[0].MAKER != "" && Result[0].MAKER != "0") {
                    document.getElementById('ctl00_MainContent_ddlSubCatMaker').value = Result[0].MAKER;
                }
                else {
                    document.getElementById('ctl00_MainContent_ddlSubCatMaker').value = "";
                }


                if (Result[0].RUN_HOUR == "1") {

                    document.getElementById("ctl00_MainContent_chkRunHourSS").checked = true;
                    //  document.getElementById("ctl00_MainContent_chkCopyRunHourSS").disabled = false;
                }
                else {
                    document.getElementById('ctl00_MainContent_chkRunHourSS').checked = false;
                    //document.getElementById("ctl00_MainContent_chkCopyRunHourSS").disabled = false;

                }


                if (Result[0].CRITICAL == "1") {
                    document.getElementById('ctl00_MainContent_chkCriticalSS').checked = true;

                }
                else {

                    document.getElementById('ctl00_MainContent_chkCriticalSS').checked = false;

                }

               

                var ischeck = document.getElementById("ctl00_MainContent_txtSubCatalogueName").value
                if (ischeck == 'GENERAL') {

                    document.getElementById('ctl00_MainContent_chkRunHourSS').disabled = true;
                    // document.getElementById('ctl00_MainContent_chkCopyRunHourSS').disabled = true;
                }
                else {

                    document.getElementById('ctl00_MainContent_chkRunHourSS').disabled = false;

                }
                document.getElementById("ctl00_MainContent_lblSubCatalogueCreatedBy").innerHTML = Result[0].CREATED_BY;
                document.getElementById("ctl00_MainContent_lblSubCatalogueModifiedby").innerHTML = Result[0].MODIFY_BY;
                document.getElementById("ctl00_MainContent_lblSubCatalogueDeletedBy").innerHTML = Result[0].DELETED_BY;
                // DisabledCopyRunHour();
            }

        },
        error: function (Result) {
            alert("Error");
        }
    });

}
function GetCatalogList() {
    var params = { systemid: document.getElementById("ctl00_MainContent_hdnSysID").value };
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "../../JIBEWebService.asmx/Get_Catalog_List",
        data: JSON.stringify(params),
        dataType: "json",
        success: function (Result) {
            Result = Result.d;

            if (Result.length > 0) {

                document.getElementById("ctl00_MainContent_txtCatalogueCode").value = Result[0].SYS_CODE;
                document.getElementById("ctl00_MainContent_txtCatalogueSetInstalled").value = Result[0].SET_INSTALLED;

                if (Result[0].Vessel_ID != "" && Result[0].Vessel_ID != "0") {
                    document.getElementById($('[id$=ddlVessID]').attr('id')).value = Result[0].Vessel_ID;
                }
                else {
                    document.getElementById($('[id$=ddlVessID]').attr('id')).value = "";

                }

                var setInstalled = document.getElementById($('[id$=txtCatalogueSetInstalled]').attr('id')).value;
                if (setInstalled != "") {

                    if (parseInt(setInstalled) > 0) {

                        if ($('[id$=rblDeptType]').find(':checked').val() == "ST") {
                            document.getElementById($('[id$=txtCatalogueSetInstalled]').attr('id')).disabled = true;
                            document.getElementById($('[id$=ImgAddLocation]').attr('id')).disabled = true;
                        }
                        else {
                            document.getElementById($('[id$=txtCatalogueSetInstalled]').attr('id')).disabled = true;
                            document.getElementById($('[id$=ImgAddLocation]').attr('id')).disabled = false;
                        }
                    }
                }

                else if (setInstalled == "" || setInstalled == "0") {



                    if ($('[id$=rblDeptType]').find(':checked').val() == "ST") {
                        document.getElementById($('[id$=txtCatalogueSetInstalled]').attr('id')).disabled = true;
                        document.getElementById($('[id$=ImgAddLocation]').attr('id')).disabled = true;
                    }
                    else {
                        document.getElementById($('[id$=txtCatalogueSetInstalled]').attr('id')).disabled = false;
                        document.getElementById($('[id$=ImgAddLocation]').attr('id')).disabled = true;
                    }


                }
                document.getElementById("ctl00_MainContent_txtCatalogName").value = Result[0].SYS_DESC;
                document.getElementById("ctl00_MainContent_txtCatalogueParticular").value = Result[0].SYS_PARTICULAR;


                if (Result[0].MAKER != "" && Result[0].MAKER != "0") {
                    document.getElementById('ctl00_MainContent_ddlCalalogueMaker').value = Result[0].MAKER;
                }
                else {
                    document.getElementById('ctl00_MainContent_ddlCalalogueMaker').value = "";
                }
                if (Result[0].DEPT1 != "" && Result[0].DEPT1 != "0") {
                    document.getElementById('ctl00_MainContent_ddlCatalogDept').value = Result[0].DEPT1;
                    _DepartmentCode = Result[0].DEPT1;
                }
                else {

                    document.getElementById('ctl00_MainContent_ddlCatalogDept').value = "";
                }
                document.getElementById("ctl00_MainContent_txtCatalogueModel").value = Result[0].MODULE_TYPE;
                document.getElementById("ctl00_MainContent_txtCatalogueSerialNumber").value = Result[0].SERIAL_NUMBER;
                if (Result[0].RUN_HOUR == "1") {
                    document.getElementById("ctl00_MainContent_chkRunHourS").checked = true;
                    document.getElementById("ctl00_MainContent_hdnSystemRhrs").value = "1";
                }
                else {
                    document.getElementById('ctl00_MainContent_chkRunHourS').checked = false;
                    document.getElementById("ctl00_MainContent_hdnSystemRhrs").value = "0";
                }
                if (Result[0].CRITICAL == "1") {
                    document.getElementById('ctl00_MainContent_chkCriticalS').checked = true;
                }
                else {
                    document.getElementById('ctl00_MainContent_chkCriticalS').checked = false;

                }

                if (Result[0].FUNCTIONS != "" && Result[0].FUNCTIONS != "0") {
                    document.getElementById('ctl00_MainContent_ddlCatalogFunction').value = Result[0].FUNCTIONS;
                }
                else {
                    document.getElementById('ctl00_MainContent_ddlCatalogFunction').value = "";
                }
                if (Result[0].ACC_CODE != "" && Result[0].ACC_CODE != "0") {
                    document.getElementById('ctl00_MainContent_ddlAccountCode').value = Result[0].ACC_CODE;
                }
                else {

                    document.getElementById('ctl00_MainContent_ddlAccountCode').value = "";
                }

                document.getElementById("ctl00_MainContent_lblCatalogueCreatedBy").innerHTML = Result[0].CREATED_BY;
                document.getElementById("ctl00_MainContent_lblCatalogueModifiedby").innerHTML = Result[0].MODIFY_BY;
                document.getElementById("ctl00_MainContent_lblCatalogueDeletedby").innerHTML = Result[0].DELETED_BY;
            }

        },
        error: function (Result) {
            alert("Error");
        }
    });

}


function GetRank() {
    document.getElementById("ctl00_MainContent_ddlRank").innerHTML = "";
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "../../JIBEWebService.asmx/Get_Rank",
        data: "{}",
        dataType: "json",
        success: function (Result) {
            Result = Result.d;

            var list = document.getElementById("ctl00_MainContent_ddlRank");
            list.add(new Option("--SELECT--", "0"));
            for (var i = 0; i < Result.length; i++) {

                list.add(new Option(Result[i].RANK_NAME, Result[i].ID));
            }


        },
        error: function (Result) {
            alert("Error");
        }
    });


}
function GetPMSFrequency(ParentTypeCode, SearchText) {
    document.getElementById("ctl00_MainContent_lstFrequency").innerHTML = "";
    var params = { parenttypecode: ParentTypeCode, searchtext: SearchText };
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "../../JIBEWebService.asmx/LibraryGetPMSSystemParameterList",
        data: JSON.stringify(params),
        dataType: "json",
        success: function (Result) {
            Result = Result.d;

            var list = document.getElementById("ctl00_MainContent_lstFrequency");

            for (var i = 0; i < Result.length; i++) {

                list.add(new Option(Result[i].DESCRIPTION, Result[i].CODE));
            }

            for (var j = 0; j < list.length; j++) {
                if (list[j].value == '2485') {
                    list[j].selected = true;
                }
            }
        },
        error: function (Result) {
            alert("Error");
        }
    });

}
function Frequency_Changed() {
    isSystemRunHourBased();
    if (_SystemID > 0 && _SubSystemID > 0 && _Vessel_ID > 0) {
        var params = { systemid: _SystemID, subsystemid: _SubSystemID, vesselid: _Vessel_ID };
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "../../JIBEWebService.asmx/PMS_Get_IsSystemSubSystemRunHourBased",
            data: JSON.stringify(params),
            dataType: "json",
            success: function (Result) {
                Result = Result.d;
                if (Result <= 0) {                                                         // changes done by Reshma : Check the condition for sub system is marked as RunHour based or not

                    var list = document.getElementById("ctl00_MainContent_lstFrequency");
                    if (list.value == "2486") {

                        alert("Frequency cannot be selected as run hour.As run hour setting is not set on System/Subsystem.");

                        list.value = "2485";

                    }

                }

                else {
                    if (isSubSystemGeneral == "GENERAL") {                                                     // check the condition for sub system as "GENERAL" 

                        if (isSystemRunHour == 0) {                                                           // if it is "GENERAL" then checked the System is marked as RunHour based or not
                            var list = document.getElementById("ctl00_MainContent_lstFrequency");

                            if (list.value == "2486") {
                                alert("Frequency cannot be selected as run hour.As run hour setting is not set on System/Subsystem.");

                                list.value = "2485";
                            }



                        }

                    }


                }
                isSystemRunHour = "";

            },
            error: function (Result) {
                alert("Error");
            }
        });
    }
    else
        alert("Please select a system and susbsystem.");

}


function isSystemRunHourBased() {
    if (_SystemID > 0 && _Vessel_ID > 0) {
        var params = { systemid: _SystemID, vesselid: _Vessel_ID };
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "../../JIBEWebService.asmx/PMS_Get_IsSystemRunHourBased",
            data: JSON.stringify(params),
            dataType: "json",
            success: function (Result) {
                Result = Result.d;
                isSystemRunHour = Result.toString();
            },
            error: function (Result) {
                alert("Error");
            }
        });
    }
}

function GetPMSDepartment(ParentTypeCode, SearchText) {
    document.getElementById("ctl00_MainContent_lstDepartment").innerHTML = "";
    var params = { parenttypecode: ParentTypeCode, searchtext: SearchText };
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "../../JIBEWebService.asmx/LibraryGetPMSSystemParameterList",
        data: JSON.stringify(params),
        dataType: "json",
        success: function (Result) {
            Result = Result.d;

            var list = document.getElementById("ctl00_MainContent_lstDepartment");

            for (var i = 0; i < Result.length; i++) {

                list.add(new Option(Result[i].DESCRIPTION, Result[i].CODE));
            }

            for (var j = 0; j < list.length; j++) {
                if (list[j].value == '2488') {
                    list[j].selected = true;
                }
            }
        },
        error: function (Result) {
            alert("Error");
        }
    });

}
function GetItemCategory(ParentTypeCode, SearchText) {
    document.getElementById("ctl00_MainContent_ddlItemCategory").innerHTML = "";
    var params = { parenttypecode: ParentTypeCode, searchtext: SearchText };
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "../../JIBEWebService.asmx/LibraryGetSystemParameterList",
        data: JSON.stringify(params),
        dataType: "json",
        success: function (Result) {
            Result = Result.d;

            var list = document.getElementById("ctl00_MainContent_ddlItemCategory");
            list.add(new Option("--SELECT--", "0"));
            for (var i = 0; i < Result.length; i++) {

                list.add(new Option(Result[i].DESCRIPTION, Result[i].CODE));
            }
            if (_MGSSelectedItemCategory == "") {
                _MGSSelectedItemCategory = "0";
            }
            document.getElementById("ctl00_MainContent_ddlItemCategory").value = _MGSSelectedItemCategory;

        },
        error: function (Result) {
            alert("Error");
        }
    });

}
function GetUnits() {
    document.getElementById("ctl00_MainContent_ddlUnit").innerHTML = "";
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "../../JIBEWebService.asmx/Get_Units",
        data: "{}",
        dataType: "json",
        success: function (Result) {
            Result = Result.d;

            var list = document.getElementById("ctl00_MainContent_ddlUnit");
            list.add(new Option("--SELECT--", "0"));
            for (var i = 0; i < Result.length; i++) {

                list.add(new Option(Result[i].ABB, Result[i].MAIN_PACK));
            }

            if (_MGSSelectedUnits == "") {
                _MGSSelectedUnits = "0";
            }
            document.getElementById("ctl00_MainContent_ddlUnit").value = _MGSSelectedUnits
            //    return true;

        },
        error: function (Result) {
            alert("Error");
        }
    });

}

function BindSystemAssignLocation() {
    
    document.getElementById("ctl00_MainContent_lstcatalogLocation").innerHTML = "";
    var params = { systemcode: _SystemCode, vesselID: parseInt(_Vessel_ID) };

    $.ajax({
        type: "POST",
        url: "../../JIBEWebService.asmx/Get_System_Assign_Location",
        data: JSON.stringify(params),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (Result) {
            Result = Result.d;

            document.getElementById("ctl00_MainContent_lstcatalogLocation").innerHTML = "";
            var list = document.getElementById("ctl00_MainContent_lstcatalogLocation");
            //  list.add(new Option("--SELECT--", "0"));
            for (var i = 0; i < Result.length; i++) {

                list.add(new Option(Result[i].LOC_NAME, Result[i].ASS_LOC_ID));
            }

        },
        failure: function (response) {
            alert(response.d);
        },
        error: function (response) {
            alert(response.d);
        }
    });

    return false;
}

function BindSubSystemAssignLocation() {
    document.getElementById("ctl00_MainContent_lstSubCatVesselLocation").innerHTML = "";
    var params = { systemcode: _SystemCode, subsystemcode: parseInt(_SubSystemID), vesselID: parseInt(_Vessel_ID) };

    $.ajax({
        type: "POST",
        url: "../../JIBEWebService.asmx/Get_SubSystem_Assign_Location",
        data: JSON.stringify(params),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (Result) {
            Result = Result.d;

            document.getElementById("ctl00_MainContent_lstSubCatVesselLocation").innerHTML = "";
            var list = document.getElementById("ctl00_MainContent_lstSubCatVesselLocation");
            //  list.add(new Option("--SELECT--", "0"));
            for (var i = 0; i < Result.length; i++) {

                list.add(new Option(Result[i].LOC_NAME, Result[i].ASS_LOC_ID));
            }

        },
        failure: function (response) {
            alert(response.d);
        },
        error: function (response) {
            alert(response.d);
        }
    });
}


function BindSystemFunction(ParentTypeCode, SearchText) {
    document.getElementById("ctl00_MainContent_ddlCatalogFunction").innerHTML = "";
    var params = { parenttypecode: ParentTypeCode, searchtext: SearchText };
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "../../JIBEWebService.asmx/LibraryGetSystemParameterList",
        data: JSON.stringify(params),
        dataType: "json",
        success: function (Result) {
            Result = Result.d;

            var list = document.getElementById("ctl00_MainContent_ddlCatalogFunction");
            list.add(new Option("--SELECT--", "0"));
            for (var i = 0; i < Result.length; i++) {

                list.add(new Option(Result[i].DESCRIPTION, Result[i].CODE));
            }



        },
        error: function (Result) {
            alert("Error");
        }
    });

}
function BindDepartments(DeptTypeText, DeptTypeVal) {
    document.getElementById($('[id$=DDLDepartment]').attr('id')).innerHTML = "";
    var params = { DeptType: DeptTypeText, DeptTypeVal: DeptTypeVal };
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "../../JIBEWebService.asmx/PURC_Get_Dept",
        data: JSON.stringify(params),
        dataType: "json",
        success: function (Result) {
            Result = Result.d;

            var list = document.getElementById($('[id$=DDLDepartment]').attr('id'));
            list.add(new Option("--SELECT--", "0"));
            for (var i = 0; i < Result.length; i++) {

                list.add(new Option(Result[i].DEPT_NAME, Result[i].CODE));
            }

            var VeselID = $('[id$=DDlVessel_List]').val();
            var FunctionCode = $('[id$=DDLDepartment]').val();
            var SearchText = $('[id$=txtSearch]').val();
            var IsActive = $('[id$=ddldisplayRecordType]').val();
            var DeptTypeText;
            var DeptTypeVal = $('[id$=rblDeptType]').find(':checked').val();

            load_FunctionalTree(VeselID, FunctionCode, SearchText, _Equipment_Type, IsActive, _FormType);

        },
        error: function (Result) {
            alert("Error");
        }
    });

}
function BindCatSupplier() {
    document.getElementById("ctl00_MainContent_ddlCalalogueMaker").innerHTML = "";
    document.getElementById("ctl00_MainContent_ddlSubCatMaker").innerHTML = "";
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "../../JIBEWebService.asmx/Get_Supplier",
        data: "{}",
        dataType: "json",
        success: function (Result) {
            Result = Result.d;

            var list = document.getElementById("ctl00_MainContent_ddlCalalogueMaker");

            list.add(new Option("--SELECT--", "0"));

            for (var i = 0; i < Result.length; i++) {
                list.add(new Option(Result[i].SUPPLIER_NAME, Result[i].SUPPLIER));

            }

            //_retrivedCatSupplier = true;

            if (document.getElementById("ctl00_MainContent_hdnCatalogOperationMode").value == "EDIT") {

                GetCatalogList();
                document.getElementById($('[id$=ddlVessID]').attr('id')).disabled = true;

            }
            else {

                document.getElementById($('[id$=ddlVessID]').attr('id')).value = document.getElementById($('[id$=DDlVessel_List]').attr('id')).value;
                document.getElementById($('[id$=ddlCatalogFunction]').attr('id')).value = _FunctionCode;
                if ($('[id$=rblDeptType]').find(':checked').val() != "ST") {
                    document.getElementById($('[id$=ddlVessID]').attr('id')).disabled = false;
                }



            }



        },
        error: function (Result) {
            alert("Error");
        }
    });



    return false;
}

function BindSubCatSupplier() {
    document.getElementById("ctl00_MainContent_ddlCalalogueMaker").innerHTML = "";
    document.getElementById("ctl00_MainContent_ddlSubCatMaker").innerHTML = "";
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "../../JIBEWebService.asmx/Get_Supplier",
        data: "{}",
        dataType: "json",
        success: function (Result) {
            Result = Result.d;


            var list1 = document.getElementById("ctl00_MainContent_ddlSubCatMaker");

            list1.add(new Option("--SELECT--", "0"));
            for (var i = 0; i < Result.length; i++) {

                list1.add(new Option(Result[i].SUPPLIER_NAME, Result[i].SUPPLIER));
            }
            if (document.getElementById("ctl00_MainContent_hdnSubCatalogOperationMode").value == "EDIT") {
                GetSubCatalogList();
            }

        },
        error: function (Result) {
            alert("Error");
        }
    });

}


function BindDepartment() {
    var FormType = $('[id$=rblDeptType]').find(':checked').val();
    document.getElementById("ctl00_MainContent_ddlCatalogDept").innerHTML = "";
    var params = { FormType: FormType };
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "../../JIBEWebService.asmx/Get_Department",
        data: JSON.stringify(params),
        dataType: "json",
        success: function (Result) {
            Result = Result.d;

            var list = document.getElementById("ctl00_MainContent_ddlCatalogDept");

            list.add(new Option("--SELECT--", "0"));
            for (var i = 0; i < Result.length; i++) {
                list.add(new Option(Result[i].NAME_DEPT, Result[i].CODE));
            }


        },
        error: function (Result) {
            alert("Error");
        }
    });

}



function BindAccountCode() {
    document.getElementById("ctl00_MainContent_ddlAccountCode").innerHTML = "";
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "../../JIBEWebService.asmx/Get_AccountCode",
        data: "{}",
        dataType: "json",
        success: function (Result) {
            Result = Result.d;

            var list = document.getElementById("ctl00_MainContent_ddlAccountCode");
            list.add(new Option("--SELECT--", "0"));
            for (var i = 0; i < Result.length; i++) {
                list.add(new Option(Result[i].BUDGET_NAME, Result[i].BUDGET_CODE));
            }


        },
        error: function (Result) {
            alert("Error");
        }
    });

}

function addInputTo(container, id, value, event) {
    var inputToAdd = $("<input/>", { type: "button", id: id, value: value, onclick: event });

    container.append(inputToAdd);
}
function removeInputFrom(container, id, value, event) {
    var inputToRemove = $(id);

    container.remove(inputToRemove);
}
function onSucc_EquipmentFunction(retval, prm) {
    try {

        document.getElementById(prm[0]).innerHTML = retval;

    }
    catch (ex)
        { }
}




function Onfail(result) {
    var res = result.d;
    //    alert(res);
}

var lastExecutorPlannedJobs = null;


var lastExecutorSpare = null;


function onSuccessAsyncBindJobsCount(retVal, ev) {

    document.getElementById('lblJobCnt').textContent = retVal.split("~totalrecordfound~")[1];

    document.getElementById($('[id$=txtSearchJobs]').attr('id')).value = "";

    document.getElementById($('[id$=txtSearchSpare]').attr('id')).value = ""; 
}
function onSuccessAsyncBindItemsCount(retVal, ev) {

    document.getElementById('lblSpareCnt').textContent = retVal.split("~totalrecordfound~")[1];
}

function onSuccessAsyncBindJobs(retVal, ev) {
    __isResponse = 1;
    document.getElementById('joblibrary').innerHTML = retVal.split("~totalrecordfound~")[0];
    document.getElementById('ctl00_MainContent_ucAsyncPager1_hdfcountTotalRec').value = retVal.split("~totalrecordfound~")[1];
    Asyncpager_BuildPager(BindNextPageJobs, 'ctl00_MainContent_ucAsyncPager1');
    document.getElementById('blur-on-updateprogress').style.display = 'none';
    //document.getElementById('ctl00_MainContent_btnRefresh').click();
    Get_AllJobs();

    var mainTable = document.getElementById("tblJobs");
    var tempTable = document.getElementById("tblAllJob");

    if (tempTable != null && mainTable != null) {

        for (var i = 1, row; row = tempTable.rows[i]; i++) {
            var jobid = "tblAllJob_Selected" + i;
            var chkJob = document.getElementById(jobid);
            if ((chkJob.checked == true)) {
                for (var j = 1, row1; row1 = mainTable.rows[j]; j++) {


                    if ((row.cells[0].textContent == row1.cells[0].textContent)) {

                        var jobid1 = "tblJobs_Selected" + j;

                        var chkJob1 = document.getElementById(jobid1);

                        chkJob1.checked = true;

                    }
                }
            }

        }
    }


}
function BindNextPageJobs() {

    var mainTable = document.getElementById("tblJobs");

    var tempTable = document.getElementById("tblAllJob");
    var found = false;


    for (var i = 1, row; row = mainTable.rows[i]; i++) {

        var jobid = "tblJobs_Selected" + i;

        var chkJob = document.getElementById(jobid);
        if ((chkJob.checked == true)) {
            for (var j = 1, row1; row1 = tempTable.rows[j]; j++) {

                if ((row.cells[0].textContent == row1.cells[0].textContent)) {

                    var jobid1 = "tblAllJob_Selected" + j;

                    var chkJob1 = document.getElementById(jobid1);

                    chkJob1.checked = true;

                }
            }

        }

    }

    //    table1.innerHTML = "<table>" + _TempJobsTable + "</table>";
    __isResponse = 0;
    setTimeout(function () { if (__isResponse == 0) { document.getElementById('blur-on-updateprogress').style.display = 'block'; } }, 20);
    var pagesize = document.getElementById('ctl00_MainContent_ucAsyncPager1_hdfPageSize').value;
    var pageindex = document.getElementById('ctl00_MainContent_ucAsyncPager1_hdfPageIndex').value;

    var searchtext = document.getElementById($('[id$=txtSearchJobs]').attr('id')).value;
    if (searchtext == "") {

        searchtext = null;
    }
    Get_Lib_PlannedJobs(_Vessel_ID, _SystemID, _SubSystemID, null, null, null, null, searchtext, null, null, null, _sort_direction, pageindex, pagesize, parseInt("1"), "tblJobs")   // Added by reshma for RA Is_RAMandatory :null , IsRAApproval: null in JIT :11705 

}


function onSuccessAsyncBindItems(retVal, ev) {
    __isResponse1 = 1;
    document.getElementById('sparecons').innerHTML = retVal.split("~totalrecordfound~")[0];
    document.getElementById('ctl00_MainContent_ucAsyncPager2_hdfcountTotalRec').value = retVal.split("~totalrecordfound~")[1];
    Asyncpager_BuildPager(BindNextPageItems, 'ctl00_MainContent_ucAsyncPager2');
    document.getElementById('blur-on-updateprogress').style.display = 'none';
    //document.getElementById('ctl00_MainContent_btnRefresh').click();


    var mainTable = document.getElementById("tblSpare");
    if (_SystemCode == "PROVI") {

        for (var i = 1, row; row = mainTable.rows[i]; i++) {

            var jobid = "tblSpare_Meat" + i;

            var chkItem = document.getElementById(jobid);

            chkItem.disabled = false;



        }

    }

    if (selected_department == "BS") {

        for (var i = 1, row; row = mainTable.rows[i]; i++) {

            var jobid = "tblSpare_IsSlopchest" + i;

            var chkItem = document.getElementById(jobid);

            chkItem.disabled = false;
        }
    }
}

function BindNextPageItems() {
    __isResponse1 = 0;
    setTimeout(function () { if (__isResponse1 == 0) { document.getElementById('blur-on-updateprogress').style.display = 'block'; } }, 20);
    var pagesize = document.getElementById('ctl00_MainContent_ucAsyncPager2_hdfPageSize').value;
    var pageindex = document.getElementById('ctl00_MainContent_ucAsyncPager2_hdfPageIndex').value;
    var searchtext = document.getElementById($('[id$=txtSearchSpare]').attr('id')).value;
    if (searchtext == "") {

        searchtext = null;
    }
    Get_Department(_SystemCode);
    Get_Lib_Items(selected_department, _SystemCode, _SubSystemCode, _Vessel_ID, null, null, null, null, _ActiveItemStatus, searchtext, null, _sort_direction, pageindex, pagesize, 1, parseInt("1"), "tblSpare");

}

//Changes Done by reshma for RA: Is_RAMandatory , IsRAApproval in JIT :11705
var lastExecutorLibPlannedJobs = null;
function Get_Lib_PlannedJobs(Vessel_ID, SystemID, SubSystemID, DeptID, rankid, jobtitle, isactive, searchtext, Is_RAMandatory, Is_RAApproval, sortby, sortdirection, pagenumber, pagesize, isfetchcount, TableID) {

    // var UserID = $('[id$=hdnUserID]').val();

    if (lastExecutorLibPlannedJobs != null)
        lastExecutorLibPlannedJobs.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../../JibeWebService.asmx', 'Get_Lib_Planned_Jobs_ManageSystem', false, { "Vessel_ID": Vessel_ID, "SystemID": SystemID, "SubSystemID": SubSystemID, "DeptID": DeptID, "rankid": rankid, "jobtitle": jobtitle, "isactive": isactive, "searchtext": searchtext, "Is_RAMandatory" : Is_RAMandatory, "Is_RAApproval": Is_RAApproval, "sortby": sortby, "sortdirection": sortdirection, "pagenumber": pagenumber, "pagesize": pagesize, "isfetchcount": isfetchcount, "TableID": TableID }, onSuccessAsyncBindJobs, Onfail, new Array('joblibrary'));
    lastExecutorLibPlannedJobs = service.get_executor();
}

//Changes Done by reshma for RA Is_RAMandatory, IsRAApproval in JIT :11705
var lastExecutorLibPlannedJobsCnt = null;
function Get_Lib_PlannedJobsCount(Vessel_ID, SystemID, SubSystemID, DeptID, rankid, jobtitle, isactive, searchtext, Is_RAMandatory, Is_RAApproval, sortby, sortdirection, pagenumber, pagesize, isfetchcount, TableID) {

    // var UserID = $('[id$=hdnUserID]').val();

    if (lastExecutorLibPlannedJobsCnt != null)
        lastExecutorLibPlannedJobsCnt.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../../JibeWebService.asmx', 'Get_Lib_Planned_Jobs_ManageSystem', false, { "Vessel_ID": Vessel_ID, "SystemID": SystemID, "SubSystemID": SubSystemID, "DeptID": DeptID, "rankid": rankid, "jobtitle": jobtitle, "isactive": isactive, "searchtext": searchtext, "Is_RAMandatory": Is_RAMandatory, "Is_RAApproval": Is_RAApproval, "sortby": sortby, "sortdirection": sortdirection, "pagenumber": pagenumber, "pagesize": pagesize, "isfetchcount": isfetchcount, "TableID": TableID }, onSuccessAsyncBindJobsCount, Onfail, new Array(''));
    lastExecutorLibPlannedJobsCnt = service.get_executor();
}

var lastExecutorSpare = null;
function Get_Lib_Items(deptcode, systemcode, subsystemcode, vesselid, partnumber, drawingnumber, name, longdesc, IsActive, searchtext, sortby, sortdirection, pagenumber, pagesize, isfetchcount, rowcount, TableID) {

    // var UserID = $('[id$=hdnUserID]').val();

    if (lastExecutorSpare != null)
        lastExecutorSpare.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../../JibeWebService.asmx', 'Get_Lib_Items_ManageSystem', false, { "deptcode": deptcode, "systemcode": systemcode, "subsystemcode": subsystemcode, "vesselid": vesselid, "partnumber": partnumber, "drawingnumber": drawingnumber, "name": name, "longdesc": longdesc, "IsActive": IsActive, "searchtext": searchtext, "sortby": sortby, "sortdirection": sortdirection, "pagenumber": pagenumber, "pagesize": pagesize, "isfetchcount": isfetchcount, "rowcount": rowcount, "TableID": TableID }, onSuccessAsyncBindItems, Onfail, new Array('sparecons'));
    lastExecutorSpare = service.get_executor();
}


var lastExecutorSpareCount = null;
function Get_Lib_ItemsCount(deptcode, systemcode, subsystemcode, vesselid, partnumber, drawingnumber, name, longdesc, IsActive, searchtext, sortby, sortdirection, pagenumber, pagesize, isfetchcount, rowcount, TableID) {

    // var UserID = $('[id$=hdnUserID]').val();

    if (lastExecutorSpareCount != null)
        lastExecutorSpareCount.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../../JibeWebService.asmx', 'Get_Lib_Items_ManageSystem', false, { "deptcode": deptcode, "systemcode": systemcode, "subsystemcode": subsystemcode, "vesselid": vesselid, "partnumber": partnumber, "drawingnumber": drawingnumber, "name": name, "longdesc": longdesc, "IsActive": IsActive, "searchtext": searchtext, "sortby": sortby, "sortdirection": sortdirection, "pagenumber": pagenumber, "pagesize": pagesize, "isfetchcount": isfetchcount, "rowcount": rowcount, "TableID": TableID }, onSuccessAsyncBindItemsCount, Onfail, new Array(''));
    lastExecutorSpareCount = service.get_executor();
}


var lastExecutorLocation = null;
function Get_Lib_Locations(systemcode, SubSystemCode, searchtext, vesselid, sortby, sortdirection, pagenumber, pagesize, isfetchcount, TableID) {

    // var UserID = $('[id$=hdnUserID]').val();

    if (lastExecutorLocation != null)
        lastExecutorLocation.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../../JibeWebService.asmx', 'PMS_Get_AssignLocation', false, { "systemcode": systemcode, "SubSystemCode": SubSystemCode, "searchtext": searchtext, "vesselid": vesselid, "sortby": sortby, "sortdirection": sortdirection, "pagenumber": pagenumber, "pagesize": pagesize, "isfetchcount": isfetchcount, "TableID": TableID }, onSucc_EquipmentFunction, Onfail, new Array('gvLocation'));
    lastExecutorLocation = service.get_executor();
}

var lastExecutorEForm = null;
function Get_Lib_EForm(searchtext, vesselid, jobid, sortby, sortdirection, pagenumber, pagesize, isfetchcount, TableID) {

    // var UserID = $('[id$=hdnUserID]').val();

    if (lastExecutorEForm != null)
        lastExecutorEForm.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../../JibeWebService.asmx', 'PMS_Get_EForm', false, { "searchtext": searchtext, "vesselid": vesselid, "jobid": jobid, "sortby": sortby, "sortdirection": sortdirection, "pagenumber": pagenumber, "pagesize": pagesize, "isfetchcount": isfetchcount, "TableID": TableID }, onSucc_EFormFunction, Onfail, new Array('gveForm'));
    lastExecutorEForm = service.get_executor();
}



var lastExecutorSLPCItems = null;
function Update_Lib_SlopChestItems(id, isSlopChest, UserID) {

    // var UserID = $('[id$=hdnUserID]').val();

    if (lastExecutorSLPCItems != null)
        lastExecutorSLPCItems.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../../JibeWebService.asmx', 'UpdateSlopChestItems', false, { "id": id, "isSlopChest": isSlopChest, "UserID": UserID }, onSucc_EFormFunction, Onfail)
    lastExecutorSLPCItems = service.get_executor();
}


var lastExecutorMeatItems = null;
function Update_Lib_MeatItems(id, isMeat, UserID) {

    // var UserID = $('[id$=hdnUserID]').val();

    if (lastExecutorMeatItems != null)
        lastExecutorMeatItems.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../../JibeWebService.asmx', 'UpdateIsMeat', false, { "ID": id, "isMeat": isMeat, "USERID": UserID }, onSucc_EFormFunction, Onfail)
    lastExecutorMeatItems = service.get_executor();
}



var lastExecutorMachine = null;

function Get_Machinery_Details(Vessel_ID, SystemID, SubSystemID) {
    if (lastExecutorMachine != null)
        lastExecutorMachine.abort();

    document.getElementById('dvMachineryDetails').innerHTML = "loading...";
    var service = Sys.Net.WebServiceProxy.invoke('../../JibeWebService.asmx', 'Get_Machinery_Details', false, { "Vessel_ID": Vessel_ID, "SystemID": SystemID, "SubSystemID": SubSystemID }, onSucc_EquipmentFunction, Onfail, new Array('dvMachineryDetails'));
    lastExecutorMachine = service.get_executor();
}

var lastExecutorSubMachine = null;
function Get_SubMachinery_Details(Vessel_ID, SystemID, SubSystemID) {
    if (lastExecutorSubMachine != null)
        lastExecutorSubMachine.abort();

    document.getElementById('dvMachineryDetails').innerHTML = "loading...";
    var service = Sys.Net.WebServiceProxy.invoke('../../JibeWebService.asmx', 'Get_SubMachinery_Details', false, { "Vessel_ID": Vessel_ID, "SystemID": SystemID, "SubSystemID": SubSystemID }, onSucc_EquipmentFunction, Onfail, new Array('dvMachineryDetails'));
    lastExecutorSubMachine = service.get_executor();
}
function ShowEquipmentReplacement() {

    if (_SubSystemLocation != null) {
        var url = 'TEC_Equipment_Replacement.aspx?Vessel_ID=' + _Vessel_ID + "&SystemID=" + _SystemID + "&SubSystemID=" + _SubSystemID + "&SystemLocation=" + _SystemLocation + "&SubSystemLocation=" + _SubSystemLocation;

        document.getElementById('iframeEQPReplacement').src = url;
        $('#dvEQPReplacement').prop('title', 'Equipment Replacement');
        showModal('dvEQPReplacement');
    }
    else
        alert('Please select equipment !');


}



function onAssignEForm(JobID) {
    var searchText = document.getElementById($('[id$=txtSearchEForm]').attr('id')).value;
    _JobID = JobID;
    Get_Lib_EForm(searchText, _Vessel_ID, JobID, null, null, parseInt("1"), parseInt("15"), null, "tblEForm")

    $('#divEFormAssign').prop('title', 'Assign E-Form');
    showModal('divEFormAssign', false);
    return false;
}
function onSearchEForm() {
    var searchText = document.getElementById($('[id$=txtSearchEForm]').attr('id')).value;
    var table = document.getElementById("tblEForm");
    Get_Lib_EForm(searchText, _Vessel_ID, _JobID, null, null, parseInt("1"), parseInt("15"), null, "tblEForm");

    $('#divEFormAssign').prop('title', 'Assign E-Form');
    showModal('divEFormAssign', false);
    return false;
}
function onSucc_EFormFunction(retVal, ev) {

    try {

        document.getElementById(ev[0]).innerHTML = retVal;

        var table = document.getElementById("tblEForm");
        if (table != null) {

            table.rows[0].cells[0].style.display = "none";
            table.rows[0].cells[1].style.display = "none";
            for (var i = 1, row; row = table.rows[i]; i++) {

                var formid = "tblEForm_AssignFlag" + i;

                var chkEForm = document.getElementById(formid);

                table.rows[i].cells[0].style.display = "none";
                table.rows[i].cells[1].style.display = "none";
                chkEForm.disabled = false

            }
        }

    }
    catch (ex)
        { }

}
function onSaveAssignEForm() {

    var table = document.getElementById("tblEForm");
    var UserID = document.getElementById($('[id$=hdnUserID]').attr('id')).value;


    for (var i = 1, row; row = table.rows[i]; i++) {

        var formid = "tblEForm_AssignFlag" + i;

        var chkEForm = document.getElementById(formid);


        // if ((chkEForm.checked == true) ) {

        if (row.cells[0].textContent.trim() == "" || row.cells[0].textContent == null) {

            if (chkEForm.checked == true) {

                SaveAssignEForm(null, _Vessel_ID, _JobID, row.cells[1].textContent, 1, UserID);
            }
        }
        else if (row.cells[0].textContent.trim() != "" || row.cells[0].textContent != null) {

            if (chkEForm.checked == false) {

                SaveAssignEForm(row.cells[0].textContent, _Vessel_ID, _JobID, row.cells[1].textContent, 0, UserID)
            }

        }




    }



    return false;

}


function SaveAssignEForm(mappingID, vesselid, jobid, formid, assignflag, userid) {
    var searchText = document.getElementById($('[id$=txtSearchEForm]').attr('id')).value;

    var params = { ID: parseInt(mappingID), Vessel_ID: parseInt(vesselid), Job_ID: parseInt(_JobID), Form_ID: parseInt(formid), ChkStatus: parseInt(assignflag), UserID: parseInt(userid) };
    // var params = { userid: parseInt(userid), systemcode: systemcode, SubSystemCode: SubSystemCode, locationid: parseInt(locationid), vesselid: parseInt(vesselid), Category_Code: Category_Code };
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "../../JIBEWebService.asmx/PMS_Insert_AssignEForm",
        data: JSON.stringify(params),
        dataType: "json",
        success: function (Result) {
            Result = Result.d;

            Get_Lib_EForm(searchText, _Vessel_ID, _JobID, null, null, parseInt("1"), parseInt("15"), null, "tblEForm")
            Get_AllJobs();
            alert("eForm assigned Successfully");

        },
        error: function (Result) {
            alert(Result.d);
        }
    });

    return true;

}
function onAutoRequestCriticalSpare() {
    var Company_Code = document.getElementById($('[id$=hdnCompanyID]').attr('id')).value;
    var IsAutoReqsn = document.getElementById($('[id$=chkAutoRequest]').attr('id')).value;

    if (document.getElementById($('[id$=chkAutoRequest]').attr('id')).checked == true) {
        IsAutoReqsn = true;
    }
    else {

        IsAutoReqsn = false;
    }
    var params = { Company_Code: parseInt(Company_Code), IsAutoReqsn: IsAutoReqsn };
    // var params = { userid: parseInt(userid), systemcode: systemcode, SubSystemCode: SubSystemCode, locationid: parseInt(locationid), vesselid: parseInt(vesselid), Category_Code: Category_Code };
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "../../JIBEWebService.asmx/PURC_Insert_AutoRequision",
        data: JSON.stringify(params),
        dataType: "json",
        success: function (Result) {
            Result = Result.d;

            onGetAutoRequestCriticalSpare(Company_Code);


        },
        error: function (Result) {
            alert(Result.d);
        }
    });

    return false;
}
function onGetAutoRequestCriticalSpare(Company_Code) {

    var params = { Company_Code: parseInt(Company_Code) };
    // var params = { userid: parseInt(userid), systemcode: systemcode, SubSystemCode: SubSystemCode, locationid: parseInt(locationid), vesselid: parseInt(vesselid), Category_Code: Category_Code };
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "../../JIBEWebService.asmx/PURC_Get_AutoRequision",
        data: JSON.stringify(params),
        dataType: "json",
        success: function (Result) {
            Result = Result.d;




        },
        error: function (Result) {
            alert(Result.d);
        }
    });

    return false;
}

function onEditLocation(LocationID, ShordCode, LocationName, evt, objthis) {
    document.getElementById($('[id$=divEditLocation]').attr('id')).style.display = "block";
    document.getElementById($('[id$=hdnLocaID]').attr('id')).value = LocationID;

    var str = LocationName;
    //  document.getElementById($('[id$=txtShCode]').attr('id')).value = ShordCode;
    var str1 = str.toString().split('#');
    document.getElementById($('[id$=txtLocation1]').attr('id')).value = str.toString().split('#')[0];
    var tempstr = "";
    for (var i = 1; i < str1.length; i++) {

        if (tempstr == "") {
            tempstr = str.toString().split('#')[i];

        }
        else {
            tempstr = tempstr + "#" + str.toString().split('#')[i];

        }


    }

    document.getElementById($('[id$=txtLocation2]').attr('id')).value = tempstr;



    return false;

}

function OnSaveEditLocation() {



    var LocationName = document.getElementById($('[id$=txtLocation1]').attr('id')).value + "#" + document.getElementById($('[id$=txtLocation2]').attr('id')).value;
    var LocationID = document.getElementById($('[id$=hdnLocaID]').attr('id')).value
    var UserID = document.getElementById($('[id$=hdnUserID]').attr('id')).value;

    var CatalogOperationMode = document.getElementById($('[id$=hdnCatalogOperationMode]').attr('id')).value;
    var SubCatalogOperationMode = document.getElementById($('[id$=hdnSubCatalogOperationMode]').attr('id')).value;
    var params = { LocationID: LocationID, ShortDescription: LocationName, userid: UserID };

    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "../../JIBEWebService.asmx/SaveEditLocation",
        data: JSON.stringify(params),
        dataType: "json",
        success: function (Result) {
            Result = Result.d;



            document.getElementById($('[id$=divEditLocation]').attr('id')).style.display = "none";

            alert("Location Updated Successfully");


            document.getElementById($('[id$=divEditLocation]').attr('id')).style.display = "none"

            if (CatalogOperationMode == "ADD") {
                BindSystemAssignLocation();
            }
            else if (CatalogOperationMode == "EDIT") {
                BindSystemAssignLocation();
            }
            if (SubCatalogOperationMode == "ADD") {
                BindSubSystemAssignLocation();
            }
            else if (SubCatalogOperationMode == "EDIT") {
                BindSubSystemAssignLocation();
            }
            Get_Lib_Locations(_SystemCode, _SubSystemCode, null, _Vessel_ID, null, null, parseInt("1"), parseInt("15"), null, "tblLocation");


        },
        error: function (Result) {
            alert(Result.d);
        }
    });

    return false;
}

function Get_Department(_SystemCode) {

    if (_SystemCode != "0" && _SystemCode != null) {
        var params = { systemid: _SystemCode };
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "../../JIBEWebService.asmx/Get_Department_By_System_Code",
            data: JSON.stringify(params),
            dataType: "json",
            success: function (Result) {
                Result = Result.d;
                selected_department = Result;


                // Get_Lib_Items(selected_department, _SystemCode, _SubSystemCode, _Vessel_ID, null, null, null, null, _ActiveItemStatus, null, null, null, parseInt("1"), parseInt("15"), 1, parseInt("1"), "tblSpare");

                Get_Lib_ItemsCount(selected_department, _SystemCode, _SubSystemCode, _Vessel_ID, null, null, null, null, _ActiveItemStatus, null, null, null, parseInt("1"), parseInt("15"), 1, parseInt("1"), "tblSpare");

            },
            error: function (Result) {
                alert("Error");
            }
        });
    }

}