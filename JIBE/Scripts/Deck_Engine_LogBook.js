
var lastExecutor = null;
function Onfail() {

}


function Onfail(msg) {

    // alert(msg._message);
}




///////////////////////////// Deck Log Book Methods ////////////////////////////////////////////////////////////////////////////////////////


function ASync_Get_DeckLog_Value_Chages(Vessel_Id, LogBook_Dtl_ID, LogHours_ID, Column_Name, evt, objthis) {
 
    if (lastExecutor != null)
        lastExecutor.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../../JibeWebService.asmx', 'Get_DeckLogBook_Values_Changes', false, { "Vessel_Id": Vessel_Id, "LogBook_Dtl_ID": LogBook_Dtl_ID, "LogHours_ID": LogHours_ID, "Column_Name": Column_Name }, onSuccessASync_Get_DeckLog_Value_Chages, Onfail, new Array(evt, objthis));
    lastExecutor = service.get_executor();

}


function onSuccessASync_Get_DeckLog_Value_Chages(retVal, eventArgs) {

    js_ShowToolTip(retVal, eventArgs[0], eventArgs[1]);
}




function ASync_Get_WheelLookOut_Value_Chages(Vessel_Id, LookOut_Dtl_ID, Log_WATCH_ID, Column_Name, evt, objthis) {

    if (lastExecutor != null)
        lastExecutor.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../../JibeWebService.asmx', 'Get_DeckLogBook_Wheel_And_Look_Values_Changes', false, { "Vessel_Id": Vessel_Id, "LookOut_Dtl_ID": LookOut_Dtl_ID, "Log_WATCH_ID": Log_WATCH_ID, "Column_Name": Column_Name }, onSuccessASync_Get_WheelLookOut_Value_Chages, Onfail, new Array(evt, objthis));
    lastExecutor = service.get_executor();

}


function onSuccessASync_Get_WheelLookOut_Value_Chages(retVal, eventArgs) {

    js_ShowToolTip(retVal, eventArgs[0], eventArgs[1]);
}





function ASync_Get_WaterInHold_Value_Chages(Vessel_Id, WaterInHold_Dtl_ID, Hold_Tank_ID, Column_Name, evt, objthis) {

    if (lastExecutor != null)
        lastExecutor.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../../JibeWebService.asmx', 'Get_DeckLogBook_Water_In_Hold_Values_Changes', false, { "Vessel_Id": Vessel_Id, "WaterInHold_Dtl_ID": WaterInHold_Dtl_ID, "Hold_Tank_ID": Hold_Tank_ID, "Column_Name": Column_Name }, onSuccessASync_Get_WaterInHold_Value_Chages, Onfail, new Array(evt, objthis));
    lastExecutor = service.get_executor();

}


function onSuccessASync_Get_WaterInHold_Value_Chages(retVal, eventArgs) {

    js_ShowToolTip(retVal, eventArgs[0], eventArgs[1]);
}





function ASync_Get_WaterInTank_Value_Chages(Vessel_Id, WaterInTank_Dtl_ID, Hold_Tank_ID, Column_Name, evt, objthis) {

    if (lastExecutor != null)
        lastExecutor.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../../JibeWebService.asmx', 'Get_DeckLogBook_Water_In_Tank_Values_Changes', false, { "Vessel_Id": Vessel_Id, "WaterInTank_Dtl_ID": WaterInTank_Dtl_ID, "Hold_Tank_ID": Hold_Tank_ID, "Column_Name": Column_Name }, onSuccessASync_Get_WaterInTank_Value_Chages, Onfail, new Array(evt, objthis));
    lastExecutor = service.get_executor();

}


function onSuccessASync_Get_WaterInTank_Value_Chages(retVal, eventArgs) {

    js_ShowToolTip(retVal, eventArgs[0], eventArgs[1]);
}


 



//---------------------------------------End Deck Log Book Method -----------------------------------------------------------------------------


///////////////////////////// Engine Log Book Methods ////////////////////////////////////////////////////////////////////////////////////////


//Details

function ASync_Get_ERLog_Details_Values_Changes(Log_ID, Vessel_ID, Column_Name, evt, objthis) 
{

    if (lastExecutor != null)
        lastExecutor.abort();



    var service = Sys.Net.WebServiceProxy.invoke('../../JibeWebService.asmx', 'Get_ERLog_Details_Values_Changes', false, {"Log_ID": Log_ID, "Vessel_ID": Vessel_ID, "Column_Name": Column_Name }, onSuccessASync_Get_ERLog_Details_Values_Changes, Onfail, new Array(evt, objthis));
    lastExecutor = service.get_executor();

}


function onSuccessASync_Get_ERLog_Details_Values_Changes(retVal, eventArgs) 
{

    js_ShowToolTip(retVal, eventArgs[0], eventArgs[1]);
}

//Main engeen 01
function ASync_Get_ERLogME_01_Values_Changes(Id, Log_ID, Vessel_ID, Column_Name, evt, objthis) {

    if (lastExecutor != null)
        lastExecutor.abort();



    var service = Sys.Net.WebServiceProxy.invoke('../../JibeWebService.asmx', 'Get_ERLogME_01_Values_Changes', false, { "ID": Id, "Log_ID": Log_ID, "Vessel_ID": Vessel_ID, "Column_Name": Column_Name }, onSuccessASync_Get_ERLogME_01_Values_Changes, Onfail, new Array(evt, objthis));
    lastExecutor = service.get_executor();

}


function onSuccessASync_Get_ERLogME_01_Values_Changes(retVal, eventArgs) {

    js_ShowToolTip(retVal, eventArgs[0], eventArgs[1]);
}
//Main engeen 01
function ASync_Get_ERLogME_02_Values_Changes(Id, Log_ID, Vessel_ID, Column_Name, evt, objthis) {

    if (lastExecutor != null)
        lastExecutor.abort();



    var service = Sys.Net.WebServiceProxy.invoke('../../JibeWebService.asmx', 'Get_ERLogME_02_Values_Changes', false, { "ID": Id, "Log_ID": Log_ID, "Vessel_ID": Vessel_ID, "Column_Name": Column_Name }, onSuccessASync_Get_ERLogME_02_Values_Changes, Onfail, new Array(evt, objthis));
    lastExecutor = service.get_executor();

}


function onSuccessASync_Get_ERLogME_02_Values_Changes(retVal, eventArgs) {

    js_ShowToolTip(retVal, eventArgs[0], eventArgs[1]);
}

//Main engeen TB
function ASync_Get_ERLogME_TB_Values_Changes(Id, Log_ID, Vessel_ID, Column_Name, evt, objthis) {

    if (lastExecutor != null)
        lastExecutor.abort();



    var service = Sys.Net.WebServiceProxy.invoke('../../JibeWebService.asmx', 'Get_ERLogME_TB_Values_Changes', false, { "ID": Id, "Log_ID": Log_ID, "Vessel_ID": Vessel_ID, "Column_Name": Column_Name }, onSuccessASync_Get_ERLogME_TB_Values_Changes, Onfail, new Array(evt, objthis));
    lastExecutor = service.get_executor();

}


function onSuccessASync_Get_ERLogME_TB_Values_Changes(retVal, eventArgs) {

    js_ShowToolTip(retVal, eventArgs[0], eventArgs[1]);
}

//Main engeen TB
function ASync_Get_ERLog_AC_FW_MISC_Values_Changes(Id, Log_ID, Vessel_ID, Column_Name, evt, objthis) {

    if (lastExecutor != null)
        lastExecutor.abort();



    var service = Sys.Net.WebServiceProxy.invoke('../../JibeWebService.asmx', 'Get_ERLog_AC_FW_MISC_Values_Changes', false, { "ID": Id, "Log_ID": Log_ID, "Vessel_ID": Vessel_ID, "Column_Name": Column_Name }, onSuccessASync_Get_ERLog_AC_FW_MISC_Values_Changes, Onfail, new Array(evt, objthis));
    lastExecutor = service.get_executor();

}


function onSuccessASync_Get_ERLog_AC_FW_MISC_Values_Changes(retVal, eventArgs) {

    js_ShowToolTip(retVal, eventArgs[0], eventArgs[1]);
}


//Main engeen TB
function ASync_Get_ERLog_TASG_Values_Changes(Id, Log_ID, Vessel_ID, Column_Name, evt, objthis) {

    if (lastExecutor != null)
        lastExecutor.abort();



    var service = Sys.Net.WebServiceProxy.invoke('../../JibeWebService.asmx', 'Get_ERLog_TASG_Values_Changes', false, { "ID": Id, "Log_ID": Log_ID, "Vessel_ID": Vessel_ID, "Column_Name": Column_Name }, onSuccessASync_Get_ERLog_TASG_Values_Changes, Onfail, new Array(evt, objthis));
    lastExecutor = service.get_executor();

}


function onSuccessASync_Get_ERLog_TASG_Values_Changes(retVal, eventArgs) {

    js_ShowToolTip(retVal, eventArgs[0], eventArgs[1]);
}

//Main engeen TB
function ASync_Get_ERLog_GTREngine_Values_Changes(Id, Log_ID, Vessel_ID, Column_Name, evt, objthis) {

    if (lastExecutor != null)
        lastExecutor.abort();



    var service = Sys.Net.WebServiceProxy.invoke('../../JibeWebService.asmx', 'Get_ERLog_GTREngine_Values_Changes', false, { "ID": Id, "Log_ID": Log_ID, "Vessel_ID": Vessel_ID, "Column_Name": Column_Name }, onSuccessASync_Get_ERLog_GTREngine_Values_Changes, Onfail, new Array(evt, objthis));
    lastExecutor = service.get_executor();

}


function onSuccessASync_Get_ERLog_GTREngine_Values_Changes(retVal, eventArgs) {

    js_ShowToolTip(retVal, eventArgs[0], eventArgs[1]);
}

//Main engeen TB
function ASync_Get_ERLog_TankLevel_Values_Changes(Id, Log_ID, Vessel_ID, Column_Name, evt, objthis) {

    if (lastExecutor != null)
        lastExecutor.abort();



    var service = Sys.Net.WebServiceProxy.invoke('../../JibeWebService.asmx', 'Get_ERLog_GTREngine_Values_Changes', false, { "ID": Id, "Log_ID": Log_ID, "Vessel_ID": Vessel_ID, "Column_Name": Column_Name }, onSuccessASync_Get_ERLog_GTREngine_Values_Changes, Onfail, new Array(evt, objthis));
    lastExecutor = service.get_executor();

}


function onSuccessASync_Get_ERLog_TankLevel_Values_Changes(retVal, eventArgs) {

    js_ShowToolTip(retVal, eventArgs[0], eventArgs[1]);
}











