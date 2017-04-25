


function onSucc_LoadFunction(retval, prm) {
    try {
        debugger;
            document.getElementById(prm[0]).innerHTML = retval;
       
    }
    catch (ex)
    { }
}


function onSucc_LoadFunction2(retval, prm) {
    try {
        debugger;
        document.getElementById(prm[0]).value = retval;
        

    }
    catch (ex)
    { }
}


function Onfail(msg) {

    alert(msg._message);
}


function AsyncGet_PortCallDetails(Vessel_Id,divId,user,pageURL) {
    debugger;
    var service = Sys.Net.WebServiceProxy.invoke('../JiBeVMService.asmx', 'CreateTableDPLPort', false, { "Vessel_Id": Vessel_Id, "user": user, "pageURL": pageURL }, onSucc_LoadFunction, Onfail, new Array(divId));
    VM_PortDetails = service.get_executor();
}




function Delete_Port(portId, vesselid, userid) {

    var service = Sys.Net.WebServiceProxy.invoke('../JiBeVMService.asmx', 'DeletePort', false, { "portId": portId, "vesselid": vesselid, "userid": userid }, onSucc_LoadFunction, Onfail);
    VM_PortDetails2 = service.get_executor();
}


function AddThisPort(vesselid) {
    var service = Sys.Net.WebServiceProxy.invoke('../JiBeVMService.asmx', 'AddThisPort', false, { "vesselid": vesselid }, onSucc_LoadFunction, Onfail);
   
    VM_PortDetails3 = service.get_executor();
}

function Update_Port(portId, vesselid, userid) {

    var service = Sys.Net.WebServiceProxy.invoke('../JiBeVMService.asmx', 'funUpdate', false, { "portId": portId, "vesselid": vesselid, "userid": userid }, onSucc_LoadFunction, Onfail);
    VM_PortDetails4 = service.get_executor();
}

function hiddenCountPrev(vesselid, hdn) {
 debugger;
    var service2 = Sys.Net.WebServiceProxy.invoke('../JiBeVMService.asmx', 'VM_Get_DPL_Count', false, { "vesselid": vesselid }, onSucc_LoadFunction2, Onfail, new Array(hdn));
    VM_PortDetailsp = service2.get_executor();
   
    return document.getElementById("ctl00_MainContent_hiddenCount").value;
}

