var _Vessel_ID = 0, _SystemID = null, _SubSystemID = null, _SystemLocation = null, _SubSystemLocation = null, _SubSysCode=null,_SysCode=null, _Equipment_Type = 1;
var _ActiveItemstate = null;
  var _sort_direction_Eqp = 0;
 var __isResponse = 1, __isResponse1 = 1;
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

function DDlVessel_selectionChange() {

    var VeselID = $('[id$=DDlVessel_List]').val();
    load_FunctionalTree(VeselID, _Equipment_Type);


}

function cbxSafetyAlarm_selectionChange(){
    VeselID = $('[id$=DDlVessel_List]').val();
    load_FunctionalTree(VeselID, _Equipment_Type);
}
function cbxCalibration_selectionChange(){
    VeselID = $('[id$=DDlVessel_List]').val();
    load_FunctionalTree(VeselID, _Equipment_Type);
}
function cbxCritical_selectionChange(){
    VeselID = $('[id$=DDlVessel_List]').val();
    load_FunctionalTree(VeselID, _Equipment_Type);
}
function initTab() {

    
    

    $("#tabs").tabs();
    $('#tabs').on('tabsactivate', function (evt, ui) {
        var _newtab = ui.newPanel.selector;


        //                if (_newtab == "#plannedjob")
        //                    Get_EQP_PlannedJobs(_Vessel_ID, _SystemID, _SubSystemID, _SystemLocation, _SubSystemLocation);
        if (_Vessel_ID.toString() != "0") {
            if (_newtab == "#unplannedjob")
                Get_EQP_UnPlannedJobs(_Vessel_ID, _SystemID, _SubSystemID, _SystemLocation, _SubSystemLocation);
            else if (_newtab == "#runhour")
                Get_EQP_RunningHour(_Vessel_ID, _SystemID, _SubSystemID, _SystemLocation, _SubSystemLocation);
            else if (_newtab == "#requisition")
                Get_EQP_Requisition(_Vessel_ID, _SystemID);
            else if (_newtab == "#sparecons")
                Get_EQP_SpareConsumption(_Vessel_ID, _SystemID, _SubSystemID, _SystemLocation, _SubSystemLocation);
            else if (_newtab == "#eqpreplacement" && _SubSystemLocation != null)
                Get_EQP_Replacement(_Vessel_ID, _SubSystemLocation);
            else  if (_newtab == "#joblibrary")
                Get_EQP_Lib_PlannedJobs(_Vessel_ID, _SystemID, _SubSystemID, _SystemLocation, _SubSystemLocation);
            else if (_newtab == "#spareconstab") {
                    
                    document.getElementById("sparecontabhead").style.display = "block";
                    document.getElementById("spareitems").style.display = "block";
                    document.getElementById("sparepager").style.display = "block";
                    Get_Lib_Items(_SystemID, _SubSysCode, _Vessel_ID, null, null, null, null, _ActiveItemstate, null, null, parseInt("1"), parseInt("15"), 1, parseInt("1"), "tblSpare");
                    document.getElementById("ctl00_MainContent_ucAsyncPager2_hdfBindMethodname").value = "BindNextPageItems";
                }
        }


    });
}
  function onFilterSpare(radiobuttonlist) {

          var rdbBoxCount = radiobuttonlist.getElementsByTagName("input");
    if (rdbBoxCount[0].checked) {

        _ActiveItemstate = null;
    }
    else if (rdbBoxCount[1].checked) {
        _ActiveItemstate = rdbBoxCount[1].value;
    }
    else if (rdbBoxCount[2].checked) {

        _ActiveItemstate = rdbBoxCount[2].value;
    }

    Get_Lib_Items(_SystemID, _SubSysCode, _Vessel_ID, null, null, null, null, _ActiveItemstate, null, null, 1, 15, 1, 1, "tblSpare");

        }


          function onSuccessAsyncBindItems(retVal, ev) {
            __isResponse1 = 1;
            document.getElementById('spareitems').innerHTML = retVal.split("~totalrecordfound~")[0];
            document.getElementById('ctl00_MainContent_ucAsyncPager2_hdfcountTotalRec').value = retVal.split("~totalrecordfound~")[1];
            Asyncpager_BuildPager(BindNextPageItems, 'ctl00_MainContent_ucAsyncPager2');
            document.getElementById('blur-on-updateprogress').style.display = 'none';
            //document.getElementById('ctl00_MainContent_btnRefresh').click();
        }
       
        function BindNextPageItems() {
            __isResponse1 = 0;
            setTimeout(function () { if (__isResponse1 == 0) { document.getElementById('blur-on-updateprogress').style.display = 'block'; } }, 20);
            var pagesize = document.getElementById('ctl00_MainContent_ucAsyncPager2_hdfPageSize').value;
            var pageindex = document.getElementById('ctl00_MainContent_ucAsyncPager2_hdfPageIndex').value;

            Get_Lib_Items(_SystemID, _SubSysCode, _Vessel_ID, null, null, null, null, _ActiveItemstate, null, _sort_direction_Eqp, pageindex, pagesize, 1, parseInt("1"), "tblSpare");

        }

           function AsyncLoadDataOnSort(sort_column) {

            //document.getElementById(_clt_id_page + 'hdfSortColumn').value = sort_column;
           
            if (_sort_direction_Eqp == 0)
                _sort_direction_Eqp = 1;
            else
                _sort_direction_Eqp = 0;

          
            Get_Lib_Items(_SystemID, _SubSysCode, _Vessel_ID, null, null, null, null, _ActiveItemstate, sort_column, _sort_direction_Eqp, parseInt("1"), parseInt("15"), 1, parseInt("1"), "tblSpare");

        }
         function AsyncLoadSpareDataOnSort(sort_column) {

            //document.getElementById(_clt_id_page + 'hdfSortColumn').value = sort_column;
           
            if (_sort_direction_Eqp == 0)
                _sort_direction_Eqp = 1;
            else
                _sort_direction_Eqp = 0;

          
            Get_Lib_Items(_SystemID, _SubSysCode, _Vessel_ID, null, null, null, null, _ActiveItemstate, sort_column, _sort_direction_Eqp, parseInt("1"), parseInt("15"), 1, parseInt("1"), "tblSpare");

        }
var isloaded = false;
/* This function Create Function Tree */
function load_FunctionalTree(vessel_id, Equipment_Type) {


    clear_Equipment_statistics();
    if (isloaded == true) {

        $("#FunctionalTree").jstree("destroy");
        isloaded = true;
    }
    if (vessel_id.toString() != "0") {

        var SafetyAlrm = 0;
        var Calibration = 0;
        var Critical =0;
        if(document.getElementById("cbxSafetyAlarm").checked==true)
            SafetyAlrm = "1";
        if(document.getElementById("cbxCalibration").checked==true)
            Calibration = "1";
         if(document.getElementById("cbxCritical").checked==true)
            Critical = "1";
        
        
        $("#FunctionalTree").jstree({
            'core': {
                'data': {
                    'type': "POST",
                    "async": "true",
                    'contentType': "application/json; charset=utf-8",
                    'url': "../../JIBEWebService.asmx/Get_Function_Tree",
                    'data': "{}",
                    'dataType': 'JSON',
                    'data': function (node) {
                        if (node.id.toString() != '#')
                            return '{"id":' + node.id + ',"vesselid":' + vessel_id + ',"Equipment_Type":' + Equipment_Type.toString() + ',"Safety_Alarm":'+SafetyAlrm.toString()+',"Calibration":'+Calibration.toString()+',"Critical":'+Critical+'}'
                        else
                            return '{"id":null,"vesselid":' + vessel_id + ',"Equipment_Type":' + Equipment_Type.toString() + ',"Safety_Alarm":'+SafetyAlrm.toString()+',"Calibration":'+Calibration.toString()+',"Critical":'+Critical+'}'
                    },
                    'success': function (retvel) {
                        isloaded = true;
                        return retvel.d;


                    }
                   

                },

               
            },


"plugins": ["themes", , "ui", "types"],
	    "ui": {
	    	"select_limit": 1
	    },
	    "types": {
	    	"types": {
	  	    "file": {
		        "icon": {
			    "image": "../../Images/document.png"
			}
		    }
		}
	    }

        });


        $("#FunctionalTree").bind('select_node.jstree', function (e) {

            var prm = $("#FunctionalTree").jstree('get_selected').toString().split(',');

            _Vessel_ID = vessel_id;
            _SubSystemID = null;
            _SystemLocation = null;
            _SubSystemLocation = null;
            _SubSystemID = null;
            _SubSysCode=null;

            if (prm.length > 1) {
                if (prm.length == 5) {
                    _Vessel_ID = vessel_id;
                    _SystemID = prm[0];
                    _SubSystemID = prm[1];
                    _SystemLocation = prm[2];
                    _SubSystemLocation = prm[3];
                     _SubSysCode = prm[4];
                }
                else if (prm.length == 3) {
                    _Vessel_ID = vessel_id; ;
                    _SystemID = prm[0];
                    _SystemLocation = prm[1];
                    _SysCode= prm[2];
                    _SubSysCode=null;
                    _SubSystemID=null;
                }

                 Get_Machinery_Details(_Vessel_ID, _SystemID, _SubSystemID);
                Get_EQP_PlannedJobs(_Vessel_ID, _SystemID, _SubSystemID, _SystemLocation, _SubSystemLocation);
                $("#tabs").tabs('option', 'active', 0);
            }


        });

    }

}



function onSucc_EquipmentFunction(retval, prm) {
    try {

        document.getElementById(prm[0]).innerHTML = retval;

    }
    catch (ex)
    { }
}

function Onfail(result) {
  //  alert('Ereor')
}

var lastExecutorPlannedJobs = null;

function Get_EQP_PlannedJobs(Vessel_ID, SystemID, SubSystemID, SystemLocation, SubSystemLocation) {

    // var UserID = $('[id$=hdnUserID]').val();

    if (lastExecutorPlannedJobs != null)
        lastExecutorPlannedJobs.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../../JibeWebService.asmx', 'Get_EQP_Planned_Jobs', false, { "Vessel_ID": Vessel_ID, "SystemID": SystemID, "SubSystemID": SubSystemID, "SystemLocation": SystemLocation, "SubSystemLocation": SubSystemLocation }, onSucc_EquipmentFunction, Onfail, new Array('plannedjob'));
    lastExecutorPlannedJobs = service.get_executor();
}

var lastExecutorUnPlannedJobs = null;
function Get_EQP_UnPlannedJobs(Vessel_ID, SystemID, SubSystemID, SystemLocation, SubSystemLocation) {

    if (lastExecutorUnPlannedJobs != null)
        lastExecutorUnPlannedJobs.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../../JibeWebService.asmx', 'Get_EQP_UnPlanned_Jobs', false, { "Vessel_ID": Vessel_ID, "SystemID": SystemID, "SubSystemID": SubSystemID, "SystemLocation": SystemLocation, "SubSystemLocation": SubSystemLocation }, onSucc_EquipmentFunction, Onfail, new Array('unplannedjob'));
    lastExecutorUnPlannedJobs = service.get_executor();
}

var lastExecutorRunningHour = null;
function Get_EQP_RunningHour(Vessel_ID, SystemID, SubSystemID, SystemLocation, SubSystemLocation) {

    if (lastExecutorRunningHour != null)
        lastExecutorRunningHour.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../../JibeWebService.asmx', 'Get_EQP_Run_Hours', false, { "Vessel_ID": Vessel_ID, "SystemID": SystemID, "SubSystemID": SubSystemID, "SystemLocation": SystemLocation, "SubSystemLocation": SubSystemLocation }, onSucc_EquipmentFunction, Onfail, new Array('runhour'));
    lastExecutorRunningHour = service.get_executor();
}
var lastExecutorRequisition = null;
function Get_EQP_Requisition(Vessel_ID, SystemID) {

    if (lastExecutorRequisition != null)
        lastExecutorRequisition.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../../JibeWebService.asmx', 'Get_EQP_Requisitions', false, { "Vessel_ID": Vessel_ID, "SystemID": SystemID }, onSucc_EquipmentFunction, Onfail, new Array('requisition'));
    lastExecutorRequisition = service.get_executor();
}

function Get_EQP_OilConsumption(SystemID, SubSystemID, SystemLocation, SubSystemLocation) {

    if (lastExecutor != null)
        lastExecutor.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../../JibeWebService.asmx', 'Get_EQP_OilConsumption', false, { "SystemID": SystemID }, onSucc_EquipmentFunction, Onfail, new Array('oilcons'));
    lastExecutor = service.get_executor();
}
var lastExecutorSpare = null;
function Get_EQP_SpareConsumption(Vessel_ID, SystemID, SubSystemID, SystemLocation, SubSystemLocation) {

    if (lastExecutorSpare != null)
        lastExecutorSpare.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../../JibeWebService.asmx', 'Get_EQP_Spare_Consumption', false, { "Vessel_ID": Vessel_ID, "SystemID": SystemID, "SubSystemID": SubSystemID, "SystemLocation": SystemLocation, "SubSystemLocation": SubSystemLocation }, onSucc_EquipmentFunction, Onfail, new Array('sparecons'));
    lastExecutorSpare = service.get_executor();
}

function Get_EQP_StoreConsumption(SystemID, SubSystemID, SystemLocation, SubSystemLocation) {

    if (lastExecutor != null)
        lastExecutor.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../../JibeWebService.asmx', 'Get_EQP_StoreConsumption', false, { "SystemID": SystemID }, onSucc_EquipmentFunction, Onfail, new Array('storecons'));
    lastExecutor = service.get_executor();
}

var lastExecutorReplaced = null;
function Get_EQP_Replacement(Vessel_ID, Location) {

    if (lastExecutorReplaced != null)
        lastExecutorReplaced.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../../JibeWebService.asmx', 'Get_EQP_Replacement_History', false, { "Vessel_ID": Vessel_ID, "Location": Location }, onSucc_EquipmentFunction, Onfail, new Array('dataEqpreplacement'));
    lastExecutorReplaced = service.get_executor();
}


var lastExecutorLibPlannedJobs = null;

function Get_EQP_Lib_PlannedJobs(Vessel_ID, SystemID, SubSystemID, SystemLocation, SubSystemLocation) {

    // var UserID = $('[id$=hdnUserID]').val();

    if (lastExecutorLibPlannedJobs != null)
        lastExecutorLibPlannedJobs.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../../JibeWebService.asmx', 'Get_EQP_Lib_Planned_Jobs', false, { "Vessel_ID": Vessel_ID, "SystemID": SystemID, "SubSystemID": SubSystemID, "SystemLocation": SystemLocation, "SubSystemLocation": SubSystemLocation }, onSucc_EquipmentFunction, Onfail, new Array('joblibrary'));
    lastExecutorLibPlannedJobs = service.get_executor();
}

function clear_Equipment_statistics() {

    document.getElementById('plannedjob').innerHTML = "";
    document.getElementById('unplannedjob').innerHTML = "";
    document.getElementById('runhour').innerHTML = "";
    document.getElementById('requisition').innerHTML = "";
    document.getElementById('oilcons').innerHTML = "";
    document.getElementById('sparecons').innerHTML = "";
    document.getElementById('storecons').innerHTML = "";
    document.getElementById('dataEqpreplacement').innerHTML = "";
    document.getElementById('dvMachineryDetails').innerHTML = "";
     //  document.getElementById('spareconstab').innerHTML = "";
    _Vessel_ID = 0;
    _SubSystemID = null;
    _SystemLocation = null;
    _SubSystemLocation = null;
    _SubSystemID = null;
    _SubSysCode=null;

}
var lastExecutorMachine = null;

function Get_Machinery_Details(Vessel_ID, SystemID,SubSystemID) {
    if (lastExecutorMachine != null)
        lastExecutorMachine.abort();

    document.getElementById('dvMachineryDetails').innerHTML = "loading...";
    var service = Sys.Net.WebServiceProxy.invoke('../../JibeWebService.asmx', 'Get_Machinery_Details', false, { "Vessel_ID": Vessel_ID, "SystemID": SystemID, "SubSystemID": SubSystemID }, onSucc_EquipmentFunction, Onfail, new Array('dvMachineryDetails'));
    lastExecutorMachine = service.get_executor();
}

//function load_Equipment_statistics(Vessel_ID, SystemID, SubSystemID, SystemLocation, SubSystemLocation,slectedTab) {

//    if (slectedTab="")
//    Get_EQP_PlannedJobs(Vessel_ID, SystemID, SubSystemID, SystemLocation, SubSystemLocation);
//    Get_EQP_UnPlannedJobs(Vessel_ID, SystemID, SubSystemID, SystemLocation, SubSystemLocation);

//}


function ShowEquipmentReplacement() {

    if (_SubSystemLocation != null) {
        var url = 'TEC_Equipment_Replacement.aspx?Vessel_ID=' + _Vessel_ID + "&SystemID=" + _SystemID + "&SubSystemID=" + _SubSystemID + "&SystemLocation=" + _SystemLocation + "&SubSystemLocation=" + _SubSystemLocation;

        document.getElementById('iframeEQPReplacement').src = url;
        showModal('dvEQPReplacement');
    }
    else
        alert('Please select equipment !');


}


   var lastExecutorSpareItems = null;
        function Get_Lib_Items(systemcode, subsystemcode, vesselid, partnumber, drawingnumber, name, longdesc, IsActive, sortby, sortdirection, pagenumber, pagesize, isfetchcount, rowcount, TableID) {

            // var UserID = $('[id$=hdnUserID]').val();

            if (lastExecutorSpareItems != null)
                lastExecutorSpareItems.abort();

            var service = Sys.Net.WebServiceProxy.invoke('../../JibeWebService.asmx', 'Get_Lib_Items__EQPState', false, { "systemcode": systemcode, "subsystemcode": subsystemcode, "vesselid": vesselid, "partnumber": partnumber, "drawingnumber": drawingnumber, "name": name, "longdesc": longdesc, "IsActive": IsActive, "sortby": sortby, "sortdirection": sortdirection, "pagenumber": pagenumber, "pagesize": pagesize, "isfetchcount": isfetchcount, "rowcount": rowcount, "TableID": TableID }, onSuccessAsyncBindItems, Onfail, new Array('spareitems'));
            lastExecutorSpareItems = service.get_executor();
        }
