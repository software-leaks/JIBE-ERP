

var _PMSEqp_Vessel_ID=0,_PMSEqp_ID=null,_PMSEqpParent_ID=null,_PMSEqpNode_Type=null,_PMS_VesselType=null;


var isloaded = false;
function DDlVessel_selectionChange() {

    _PMSEqp_Vessel_ID = $('[id$=DDlVessel_List]').val();
    ParentType = "115";
    SortBy = 1;
    SortDirection = 1;
    PageNumber = 14;
    PageSize = 1;
    IsFetchCount = 1;


    load_FunctionalTree(_PMSEqp_Vessel_ID);



    return false;

}


function initTab()
{

  $("#tabs").tabs();
            $('#tabs').on('tabsactivate', function (evt, ui) {
                var _newtab = ui.newPanel.selector;



                if (_PMSEqp_Vessel_ID.toString() != "0") {

                    if (_newtab == "#joblibrarytab") {
                        document.getElementById("jobtabhead").style.display = "block";
                        document.getElementById("joblibrary").style.display = "block";
                        document.getElementById("jobspager").style.display = "block";
                       // Get_Lib_PlannedJobs(_Vessel_ID, _SystemID, _SubSystemID, null, null, null, _ActiveJobStatus, "ID", null, parseInt("1"), parseInt("15"), parseInt("1"), "tblJobs");
                        //document.getElementById("ctl00_MainContent_ucAsyncPager1_hdfBindMethodname").value = "BindNextPageJobs";
                         document.getElementById( $('[id$=ucAsyncPager1_hdfBindMethodname]').attr('id')).value = "BindNextPageJobs";
                    }
                    else if (_newtab == "#spareconstab") {
                        //Get_SpareConsumption(_Vessel_ID, _SystemID, _SubSystemID);
                        document.getElementById("sparecontabhead").style.display = "block";
                        document.getElementById("sparecons").style.display = "block";
                        document.getElementById("sparepager").style.display = "block";
                       // Get_Lib_Items(_SystemCode, _SubSystemCode, _Vessel_ID, null, null, null, null, _ActiveItemStatus, null, null, parseInt("1"), parseInt("15"), 1, parseInt("1"), "tblSpare");
                        document.getElementById( $('[id$=ucAsyncPager2_hdfBindMethodname]').attr('id')).value = "BindNextPageItems";
                    }

                }


            });
    load_FunctionalTree(_PMSEqp_Vessel_ID);
}
function load_FunctionalTree(vessel_id) {


    //clear_Equipment_statistics();
    if (isloaded == true) {

        $("#FunctionalTree").jstree("destroy");
        isloaded = true;
              $("#btnAddGroup").remove();
                $("#btnEditGroup").remove();
                $("#btnAddEquipment").remove();
                $("#btnEditEquipment").remove();
                $("#btnAddSpareConsumption").remove();
                $("#btnAddJob").remove();
    }
    if (vessel_id.toString() != "0") {

                addInputTo($("#dvEditDetails"), "btnAddGroup", "Add Group", "onAddGroup()");
                addInputTo($("#dvEditDetails"), "btnEditGroup", "Edit Group", "onEditGroup()");
                addInputTo($("#dvEditDetails"), "btnAddEquipment", "Add Equipment", "onAddEquipment()");
                addInputTo($("#dvEditDetails"), "btnEditEquipment", "Edit Equipment", "onEditEquipment()");
                addInputTo($("#dvEditDetails"), "btnAddSpareConsumption", "Add Spare Item", "onAddSpare()");
                addInputTo($("#dvEditDetails"), "btnAddJob", "Add Job", "onAddJob()");
                $("#btnAddGroup").show();
                $("#btnEditGroup").hide();
                $("#btnAddEquipment").hide();
                $("#btnEditEquipment").hide();
                $("#btnAddSpareConsumption").hide();
                $("#btnAddJob").hide();
        $("#FunctionalTree").jstree({
            'core': {
                'data': {
                    'type': "POST",
                    "async": "true",
                    'contentType': "application/json; charset=utf-8",
                    'url': "/jibe/JIBEWebService.asmx/TEC_Get_FunctionalTreeDataEqpStruct",
                    'data': "{}",
                    'dataType': 'JSON',
                    'data': function (node) {
                        if (node.id.toString() != '#')
                            return '{"id":' + node.id + ',"vesselid":' + vessel_id + '}'
                        else
                            return '{"id":null,"vesselid":' + vessel_id + '}'
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

            //alert(prm.length);
            _PMSEqp_ID = null;
            _PMSEqpParent_ID=null;
            _PMSEqpNode_Type=null;
            if (prm.length >= 0) {
              
                if (prm.length == 3) {
                    _PMSEqp_ID = prm[0];
                    _PMSEqpParent_ID=prm[1];
                    _PMSEqpNode_Type=prm[2];

                    if(_PMSEqpNode_Type=="Group")
                    {
                        $("#btnAddGroup").hide();
                        $("#btnEditGroup").show();
                        $("#btnAddEquipment").show();
                        $("#btnEditEquipment").hide();
                        $("#btnAddSpareConsumption").hide();
                        $("#btnAddJob").hide();
                    }
                    else
                    {
                         $("#btnAddGroup").hide();
                        $("#btnEditGroup").hide();
                        $("#btnAddEquipment").show();
                        $("#btnEditEquipment").show();
                        $("#btnAddSpareConsumption").show();
                        $("#btnAddJob").show();
                    }
                }
              
                $("#tabs").tabs('option', 'active', 0);
            }


        });

    }

}
function onAddGroup()
{
  $("#divAddGroup").prop('title', 'Add Group');
            showModal('divAddGroup', false);
document.getElementById($('[id$=hdnGroupOperationMode]').attr('id')).value="ADD";
 BindSystemFunction(ParentType, "");
 BindGroupSupplier();
}
function onEditGroup()
{
  $("#divAddGroup").prop('title', 'Edit Group');
  showModal('divAddGroup', false);
  document.getElementById($('[id$=hdnGroupOperationMode]').attr('id')).value="EDIT";
  document.getElementById($('[id$=ddlFunction]').attr('id')).disabled = true;
  BindSystemFunction(ParentType, "");
  BindGroupSupplier();
}

function onSaveGroup()
{
    var GroupOperationMode = document.getElementById($('[id$=hdnGroupOperationMode]').attr('id')).value;
    var Function = document.getElementById($('[id$=ddlFunction]').attr('id')).value;
    var GroupName = document.getElementById($('[id$=txtGroupName]').attr('id')).value;
    var GroupDesc = document.getElementById($('[id$=txtGroupDesc]').attr('id')).value;
    var Model = document.getElementById($('[id$=txtModel]').attr('id')).value;
    var Maker = document.getElementById($('[id$=ddlMaker]').attr('id')).value;
    var UserID = document.getElementById($('[id$=hdnUserID]').attr('id')).value;
    var ParentEqpID=_PMSEqp_ID;
    var NodeType="Group";
  

                    if (GroupOperationMode == "ADD") {
                        var params = { ParentEqpID:parseInt(ParentEqpID) , EqpName:GroupName, EqpDescription:GroupDesc, Maker:Maker, Model:Model, NodeType:NodeType, Function:parseInt(Function), Vessel_ID:parseInt(_PMSEqp_Vessel_ID), CreatedBy:parseInt(UserID), ActiveStatus:1 };

                        $.ajax({
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            url: "../../JIBEWebService.asmx/TEC_Insert_PMSGroup",
                            data: JSON.stringify(params),
                            dataType: "json",
                            success: function (Result) {
                                Result = Result.d;


                            
                                alert("Group Added Successfully");
                                hideModal('divAddGroup');
                                GroupOperationMode = "";

                            },
                            error: function (Result) {
                                alert("Error");
                            }
                        });

                    }
                    else if (GroupOperationMode == "EDIT") {
                        var params = { ParentEqpID:parseInt(ParentEqpID) , EqpName:GroupName, EqpDescription:GroupDesc, Maker:parseInt(Maker), Model:Model, NodeType:NodeType, Function:parseInt(Function), Vessel_ID:parseInt(_PMSEqp_Vessel_ID), CreatedBy:parseInt(UserID), ActiveStatus:1 };
                        $.ajax({
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            url: "../../JIBEWebService.asmx/LibraryCatalogueUpdate",
                            data: JSON.stringify(params),
                            dataType: "json",
                            success: function (Result) {
                                Result = Result.d;



                               alert("Group Edited Successfully");
                               hideModal('divAddGroup');
                               GroupOperationMode = "";

                            },
                            error: function (Result) {
                                alert("Error");
                            }
                        });

                        
                    }

}
 function BindGroupSupplier() {
            document.getElementById($('[id$=ddlMaker]').attr('id')).innerHTML = "";
           
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "../../JIBEWebService.asmx/Get_Supplier",
                data: "{}",
                dataType: "json",
                success: function (Result) {
                    Result = Result.d;

                    var list = document.getElementById($('[id$=ddlMaker]').attr('id'));

                    list.add(new Option("--SELECT--", "0"));

                    for (var i = 0; i < Result.length; i++) {
                        list.add(new Option(Result[i].SUPPLIER_NAME, Result[i].SUPPLIER));

                    }

                    //_retrivedCatSupplier = true;
                    
                    if(document.getElementById($('[id$=hdnGroupOperationMode]').attr('id')).value=="EDIT"){

                    GetGroupInfo();
                    }
                },
                error: function (Result) {
                    alert("Error");
                }
            });

            
           
            return false;
        }
function BindSystemFunction(ParentTypeCode, SearchText) {
        document.getElementById( $('[id$=ddlFunction]').attr('id')).innerHTML = "";
        var params = { parenttypecode: ParentTypeCode, searchtext: SearchText };
        $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "../../JIBEWebService.asmx/LibraryGetSystemParameterList",
        data: JSON.stringify(params),
        dataType: "json",
        success: function (Result) {
        Result = Result.d;

        var list = document.getElementById( $('[id$=ddlFunction]').attr('id'));
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
 function GetGroupInfo()
 {
    
     var params = { EQPID: _PMSEqp_ID };
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "../../JIBEWebService.asmx/TEC_Get_PMSGroupInfo",
                data: JSON.stringify(params),
                dataType: "json",
             
                success:function (Result) {

                Result=JSON.parse(Result.d);
                     document.getElementById($('[id$=ddlFunction]').attr('id')).value=Result[0].Functions;
                   document.getElementById($('[id$=txtGroupName]').attr('id')).value=Result[0].EQP_Name;
                   document.getElementById($('[id$=txtGroupDesc]').attr('id')).value=Result[0].EQP_Description;
                   document.getElementById($('[id$=txtModel]').attr('id')).value=Result[0].model;
                   document.getElementById($('[id$=ddlMaker]').attr('id')).value=Result[0].maker;
                },
                error: function (Result) {
                    alert("Error");
                }
            });
 }
 function refreshPage() {

 load_FunctionalTree(_PMSEqp_Vessel_ID);
 return false;
}

function addInputTo(container, id, value, event) {
    var inputToAdd = $("<input/>", { type: "button", id: id, value: value, onclick: event });

    container.append(inputToAdd);
}
function removeInputFrom(container, id, value, event) {
    var inputToRemove = $(id);

    container.remove(inputToRemove);
}