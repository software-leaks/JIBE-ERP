var isloaded = false;
var equipmentidgeneral = '0';
var equipmentid = '0';
var equipmenttype = '0';
var id = 0;
var oldid = 0;
var vessel_id = 0;
function ddlVessel_selectionChange() {
    document.getElementById('hdfSourceID').value = "0";
    bindTreeSource();
    var elem = document.getElementById("dvSource");
    var btnSearch = document.getElementById("btnSearch");
    if (vessel_id > 0) {

        elem.style.display = "block";
        btnSearch.style.visibility = "visible";
    }
    else {
        elem.style.display = "none";
        btnSearch.style.visibility = "hidden";
    }
    HideDestination();
}


function IsDestinationLinked() {
    alert("The selected equipment is already linked.");
}

function TreeSelectionMsg() {
    alert("Select a System/Subsystem in Source.");
}
function NoRunningHours() {
    alert("Rhr settings is not directly associated on selected System/Subsystem.");
}
function SuccessMessage() {
    alert("Equipment run hour settings saved successfully.");
    //    var elemSourceid = document.getElementById("hdfSourceID");
    //    var elemHdf2 = document.getElementById("hdf2");
    var elemF = document.getElementById("ddlFleet");
    var elem = document.getElementById("ddlVessel");
    //    elem.value = "0";
    //    elemF.value = "0";
    //    elemSourceid = "0";
    //    elemHdf2 = "0";
}
function HideDestination() {
    var elem = document.getElementById("dvDestination");
    elem.style.display = "none";
}

function FailureMessage() {
    if (document.getElementById('hdf2').value == "")
        TreeSelectionMsg();
    else
        alert("Unable to save data.");
}
function MessageFailureIfNoDestinationSelected() {
    alert("Please select a destination before trying to save.");
}
function DisplayPage() {
    var elem = document.getElementById("dvSource");
    if (vessel_id > 0)
        elem.style.display = "block";
    else
        elem.style.display = "none";

    var elem2 = document.getElementById("dvDestination");
    var sourceid = document.getElementById('hdfSourceID').value;

    if (vessel_id > 0)
        elem2.style.display = "block";
    else
        elem2.style.display = "none";
}
function DisplayOnlySource() {
    var elem = document.getElementById("dvSource");
    elem.style.display = "block";

    var elem2 = document.getElementById("dvDestination");
    var sourceid = document.getElementById('hdfSourceID').value;
    elem2.style.display = "none";

}
function DestroyTree() {
    if (isloaded == true) {
        $("#FunctionalTreeSource").jstree("destroy");
        isloaded = true;
    }
}

function bindTreeSource() {

    id = 0;
    vessel_id = $('[id$=ddlVessel]').val();
    if (vessel_id > 0)
        BuildTree();




}

function BuildTree() {

    vessel_id = $('[id$=ddlVessel]').val();
    if (isloaded == true) {
        $("#FunctionalTreeSource").jstree("destroy");
        isloaded = true;
    }

    $("#FunctionalTreeSource").jstree({
        'core': {

            'data': {
                'type': "POST",
                "async": "true",
                'contentType': "application/json; charset=utf-8",
                'url': "../../JIBEWebService.asmx/PMS_Get_SourceSystemSubsystemFunction_Tree",
                'data': "{}",
                'dataType': 'JSON',
                'data': function () {
                    return '{"id":' + id + ',"vesselid":' + vessel_id + ',"equipmentid":' + equipmentidgeneral + ',"equipmenttype":' + equipmentidgeneral + '}'
                },
                'success': function (retvel) {
                    isloaded = true;
                    if (retvel.d.length <= 2) {
                        alert("No data available");
                        var elem = document.getElementById("dvSource");
                        elem.style.display = "none";
                    }
                    else {
                        var elem = document.getElementById("dvSource");
                        elem.style.display = "block";
                    }
                    return retvel.d;
                }

            }
        }
    });
    var tree = $("#FunctionalTreeSource");
    tree.bind("loaded.jstree", function (event, data) {
        if (document.getElementById('hdfSourceID').value != "")
            id = document.getElementById('hdfSourceID').value;
        else
            id = 0;

        if (document.getElementById('hdfSourceID').value > 0) {

            tree.jstree('select_node', document.getElementById('hdfSourceID').value);


        }
        else
            tree.jstree('select_node', document.getElementById('hdfSourceID').value);

    });

    $("#FunctionalTreeSource").bind('select_node.jstree', function (e) {
        id = $("#FunctionalTreeSource").jstree('get_selected').toString();

        if ($('#FunctionalTreeSource').jstree(true).get_parent(id) == '#') {
            document.getElementById('hdf2').value = "System";
        }
        else {
            document.getElementById('hdf2').value = "SubSystem";
        }

        document.getElementById('hdfSourceID').value = id;


        oldid = id;
        
        var clickButton = document.getElementById('btnSearch');
        clickButton.click();

    });

    isloaded = true;
}


function bindBasedOnQueryStringTreeSource() {

    equipmentid = document.getElementById('hdfQueryStringEquipmentID').value;
    equipmenttype = document.getElementById('hdfQueryStringEquipmentType').value;

    if (document.getElementById('hdfSourceID').value != "")
        id = document.getElementById('hdfSourceID').value;
    else
        id = 0;
    vessel_id = $('[id$=ddlVessel]').val();
    if (isloaded == true) {
        $("#FunctionalTreeSource").jstree("destroy");
        isloaded = true;
    }


    $("#FunctionalTreeSource").jstree({
        'core': {

            'data': {
                'type': "POST",
                "async": "true",
                'contentType': "application/json; charset=utf-8",
                'url': "../../JIBEWebService.asmx/PMS_Get_SourceSystemSubsystemFunction_Tree",
                'data': "{}",
                'dataType': 'JSON',
                'data': function () {
                    return '{"id":' + id + ',"vesselid":' + vessel_id + ',"equipmentid":' + equipmentid + ',"equipmenttype":' + equipmenttype + '}'
                },
                'success': function (retvel) {
                    isloaded = true;
                    if (retvel.d.length <= 2) {
                        alert("No data available");
                    }
                    return retvel.d;
                }

            }
        }
    });
    var tree = $("#FunctionalTreeSource");
    tree.bind("loaded.jstree", function (event, data) {
        if (document.getElementById('hdfSourceID').value != "")
            id = document.getElementById('hdfSourceID').value;
        else
            id = 0;

        if (document.getElementById('hdfSourceID').value > 0) {

            tree.jstree('select_node', document.getElementById('hdfSourceID').value);
        }

    });
    $("#FunctionalTreeSource").bind('select_node.jstree', function (e) {
        id = $("#FunctionalTreeSource").jstree('get_selected').toString();

        if ($('#FunctionalTreeSource').jstree(true).get_parent(id) == '#') {
            document.getElementById('hdf2').value = "System";
        }
        else {
            document.getElementById('hdf2').value = "SubSystem";
        }
        if (id > 0 && vessel_id > 0) {
            document.getElementById('hdfSourceID').value = id;

        }
    });
    isloaded = true;

}
//Added in JIT :11565 : set focus for search textbox
function SetFocusOnTreeSelectNode() {
    document.getElementById("ctl00_MainContent_txtSearch").focus();
   
}