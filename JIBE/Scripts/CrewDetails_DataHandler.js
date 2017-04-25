
var __app_name = location.pathname.split('/')[1];

var lastExecutor_WebServiceProxy = null;


//-------------INTERVIEW RESULT TAB ---------------------
//-------------------------------------------------------
function GetInterviewResult(CrewID) {
    $('#dvInterviewResult').html('<div style="text-align:center"><img src="../Images/ajax-loader.gif" /></div>');
    $('#dvInterviewResult').load("CrewDetails_InterviewResult.aspx?ID=" + CrewID + "&rnd=" + Math.random() + ' #dvCrewInterviewResults');
    CrewReferenceCheck(CrewID);
}

function CrewReferenceCheck(CrewID) {
    if (lastExecutor_WebServiceProxy != null)
        lastExecutor_WebServiceProxy.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../JibeWebService.asmx', 'referenceCheckForCrew', false, { "CrewID": CrewID }, ReferenceCheck_onSuccess, ReferenceCheck_onFail);

    lastExecutor_WebServiceProxy = service.get_executor();
}
function ReferenceCheck_onSuccess(retval) {
    $('[id$=HiddenField_ReferenceCount]').val(retval);
}
function ReferenceCheck_onFail(err_) {
    alert(err_._message);
}

//------------EVALUATION RESULT TAB ---------------------
//-------------------------------------------------------
function GetEvaluationResult(CrewID) {
    $('#dvEvaluations').html('<div style="text-align:center"><img src="../Images/ajax-loader.gif" /></div>');
    $('#dvEvaluations').load("CrewDetails_EvaluationResult.aspx?ID=" + CrewID + "&rnd=" + Math.random() + ' #dvCrewEvaluationResults');
}

//------------CREW ACTIVITY LOG TAB ---------------------
//-------------------------------------------------------
function GetCrewLog(CrewID) {
    $('#dvCrewLog').html('<div style="text-align:center"><img src="../Images/ajax-loader.gif" /></div>');
    $('#dvCrewLog').load("CrewDetails_Log.aspx?ID=" + CrewID + "&rnd=" + Math.random() + ' #dvCrewLogGrid');
}

//------------CREW FEEDBACK LOG TAB ---------------------
//-------------------------------------------------------
function GetCrewFeedback(CrewID) {
    $('#dvCrewFeedback').html('<div style="text-align:center"><img src="../Images/ajax-loader.gif" /></div>');
    $('#dvCrewFeedback').load("CrewDetails_Feedback.aspx?CrewID=" + CrewID + "&rnd=" + Math.random() + ' #dvCrewFeedbackGrid');
}
function AddNewFeedback(CrewID) {
    $('#dvPopupFrame').attr("Title", "Add New Feedback");
    $('#dvPopupFrame').css({ "width": "800px", "height": "400px", "text-allign": "center" });
    $('#frPopupFrame').load(function () { this.style.height = '400px'; this.style.width = '800px' });

    var URL = "CrewDetails_Feedback.aspx?CrewID=" + CrewID +"&Mode=ADD&rnd=" + Math.random();
    document.getElementById("frPopupFrame").src = URL;
    showModal('dvPopupFrame', true);
    //$("#dvPopupFrame").animate({ top: "100" });

}

//------------CREW BANK ACCOUNTS TAB ---------------------
//-------------------------------------------------------
function GetCrewBankAcc(CrewID) {
    $('#dvCrewBankAccounts').html('<div style="text-align:center"><img src="../Images/ajax-loader.gif" /></div>');
    $('#dvCrewBankAccounts').load("CrewDetails_BankAcc.aspx?CrewID=" + CrewID + "&Mode=VIEW&rnd=" + Math.random() + ' #dvCrewBankAccGrid');
}
function InsertCrewBankAcc(CrewID) {
    $('#dvPopupFrame').attr("Title", "New Bank Account");
    $('#dvPopupFrame').css({ "width": "800px", "height": "570px", "text-allign": "center" });
    $('#frPopupFrame').load(function () { this.style.height = '570px'; this.style.width = '800px' });

    var URL = "CrewDetails_BankAcc.aspx?CrewID=" + CrewID + "&Mode=INSERT&rnd=" + Math.random();
    document.getElementById("frPopupFrame").src = URL;
    showModal('dvPopupFrame', true);
    //$("#dvPopupFrame").animate({ top: "100" });
}

function EditCrewBankAcc(BankAccID, CrewID, UserID) {
    $('#dvPopupFrame').attr("Title" , "Edit Bank Account");
    $('#dvPopupFrame').css({ "width": "800px", "height": "570px", "text-allign": "center" });
    $('#frPopupFrame').load(function () { this.style.height = '570px'; this.style.width = '800px' });

    var URL = "CrewDetails_BankAcc.aspx?CrewID=" + CrewID + "&AccID=" + BankAccID + "&UserID=" + UserID + "&Mode=EDIT&rnd=" + Math.random();
    document.getElementById("frPopupFrame").src = URL;
    showModal('dvPopupFrame', true);
    //$("#dvPopupFrame").animate({ top: "100" });
    
}
var thisCrewID=0;
function DeleteCrewBankAcc(BankAccID,CrewID,UserID) {
    var iConfirm = confirm('Are you sure, you want to delete the record?');
    
    if (iConfirm == true) {
        thisCrewID = CrewID;        
        if (lastExecutor_WebServiceProxy != null)
            lastExecutor_WebServiceProxy.abort();
        
        var service = Sys.Net.WebServiceProxy.invoke('../JibeWebService.asmx', 'Del_BankAccount', false, { "ID": BankAccID, "CrewID": CrewID, "Deleted_By": UserID }, DeleteCrewBankAcc_onSuccess, DeleteCrewBankAcc_onFail);
        
        lastExecutor_WebServiceProxy = service.get_executor();        
    }
}
function DeleteCrewBankAcc_onSuccess(retval) {
    if (retval == "1") {
        GetCrewBankAcc(thisCrewID);
    }
    if (retval == "-1") {
        alert('Default account can not be deleted.');
    } 
}
function DeleteCrewBankAcc_onFail(err_) {
    alert(err_._message);
}


//------------CREW NOK AND DEPENDENT TAB ----------------
//-------------------------------------------------------

function GetCrewNOKAndDependents(CrewID) {
    $('#dvContent_NOK_Dependent').html('<div style="text-align:center"><img src="../Images/ajax-loader.gif" /></div>');
    $('#dvContent_NOK_Dependent').load("CrewDetails_NOK.aspx?CrewID=" + CrewID + "&rnd=" + Math.random() + ' #dvCrewNOKAndDependents');
}
function AndNewDependent(CrewID) {
    $('#dvPopupFrame').attr("Title", "New Dependent");
    $('#dvPopupFrame').css({ "width": "850px", "height": "340px", "text-allign": "center" });
    $('#frPopupFrame').load(function () { this.style.height = "340px";this.style.width="850px" });

    var URL = "CrewDetails_NOK.aspx?CrewID=" + CrewID + "&Mode=INSERT&rnd=" + Math.random();
    document.getElementById("frPopupFrame").src = URL;
    showModal('dvPopupFrame', true);
    //$("#dvPopupFrame").animate({ top: "100" });
}
function EditDependent(CrewID,DependentID) {
    $('#dvPopupFrame').attr("Title", "Edit Dependent Details");
    $('#dvPopupFrame').css({ "width": "850px", "height": "340px", "text-allign": "center" });
    $('#frPopupFrame').load(function () { this.style.height = "340px"; this.style.width = "850px" });

    var URL = "CrewDetails_NOK.aspx?CrewID=" + CrewID + "&DependentID=" + DependentID + "&Mode=EDIT_DEP&rnd=" + Math.random();
    document.getElementById("frPopupFrame").src = URL;
    showModal('dvPopupFrame', true);
    //$("#dvPopupFrame").animate({ top: "100" });
}

function AddNOK(CrewID) {
    $('#dvPopupFrame').attr("Title", "Add NOK Details");
    $('#dvPopupFrame').css({ "width": "750px", "height": "310px", "text-allign": "center" });
    $('#frPopupFrame').load(function () { this.style.height = "310px"; this.style.width = "750px" });

    var URL = "CrewDetails_NOK.aspx?CrewID=" + CrewID + "&Mode=ADD_NOK&rnd=" + Math.random();
    document.getElementById("frPopupFrame").src = URL;
    showModal('dvPopupFrame', true);
    //$("#dvPopupFrame").animate({ top: "100" });
}
function EditNOK(CrewID, NOKID) {
    $('#dvPopupFrame').attr("Title", "Edit NOK Details");
    $('#dvPopupFrame').css({ "width": "750px", "height": "310px", "text-allign": "center" });
    $('#frPopupFrame').load(function () { this.style.height = "310px"; this.style.width = "750px" });

    var URL = "CrewDetails_NOK.aspx?CrewID=" + CrewID + "&DependentID=" + NOKID + "&Mode=EDIT_NOK&rnd=" + Math.random();
    document.getElementById("frPopupFrame").src = URL;
    showModal('dvPopupFrame', true);
    //$("#dvPopupFrame").animate({ top: "100" });
}

function DeleteCrewDependent(DependentID,CrewID, UserID) {
    var iConfirm = confirm('Are you sure, you want to delete the record?');

    if (iConfirm == true) {
        thisCrewID = CrewID;
        if (lastExecutor_WebServiceProxy != null)
            lastExecutor_WebServiceProxy.abort();

        var service = Sys.Net.WebServiceProxy.invoke('../JibeWebService.asmx', 'Del_CrewDependent', false, { "ID": DependentID, "Deleted_By": UserID }, DeleteCrewDependent_onSuccess, DeleteCrewDependent_onFail);

        lastExecutor_WebServiceProxy = service.get_executor();
    }
}
function DeleteCrewDependent_onSuccess(retval) {
    if (retval == "1") {
        GetCrewNOKAndDependents(thisCrewID);
    }
    if (retval == "-1") {
        alert('Dependent can not be deleted.');
    }
}
function DeleteCrewDependent_onFail(err_) {
    alert(err_._message);
}

//------------TRAVEL TAB ----------------------------
//-------------------------------------------------------
function GetCrewTravelLog(CrewID) {
    $('#dvContent_TravelLog').html('<div style="text-align:center"><img src="../Images/ajax-loader.gif" /></div>');
    $('#dvContent_TravelLog').load("CrewDetails_TravelLog.aspx?CrewID=" + CrewID + "&Mode=VIEW&rnd=" + Math.random() + ' #dvCrewTravelLog');
}

//------------TRAINING TAB ----------------------------
//-------------------------------------------------------
function GetCrewTrainingLog(CrewID) {
    $('#dvContent_TrainingLog').html('<div style="text-align:center"><img src="../Images/ajax-loader.gif" /></div>');
    $('#dvContent_TrainingLog').load("CrewDetails_Training.aspx?CrewID=" + CrewID + "&Mode=VIEW&rnd=" + Math.random() + ' #dvTraining');
    
}

function AndCrewTraining(CrewID) {
    $('#dvPopupFrame').attr("Title", "Add Crew Training");
    $('#dvPopupFrame').css({ "width": "600px", "height": "600px", "text-allign": "center" });
    $('#frPopupFrame').load(function () { this.style.height = '600px'; this.style.width = '600px' });


    //var URL = "jqupload/default.aspx?CrewID=" + CrewID + "&Mode=INSERT&rnd=" + Math.random() + ' #dvTraining';
    var URL = "CrewDetails_Training.aspx?CrewID=" + CrewID + "&Mode=INSERT&rnd=" + Math.random() + ' #dvTraining';
    document.getElementById("frPopupFrame").src = URL;

    var fn_callback = "callback_GetCrewTrainingLog(" + CrewID + ")";
    showModal('dvPopupFrame', true, fn_callback);
}
function callback_GetCrewTrainingLog(CrewID) {
    GetCrewTrainingLog(CrewID);
}
function EditTraining(CrewID, TrainingID) {
    $('#dvPopupFrame').attr("Title", "Edit Crew Training");
    $('#dvPopupFrame').css({ "width": "600px", "height": "600px", "text-allign": "center" });
    $('#frPopupFrame').load(function () { this.style.height = '600px'; this.style.width = '600px' });

    var URL = "CrewDetails_Training.aspx?CrewID=" + CrewID + "&TrainingID=" + TrainingID + "&Mode=EDIT&rnd=" + Math.random() + ' #dvTraining';
    document.getElementById("frPopupFrame").src = URL;

    var fn_callback = "callback_GetCrewTrainingLog(" + CrewID + ")";
    showModal('dvPopupFrame', true, fn_callback);
}
function DeleteTraining(CrewID, TrainingID, UserID) {
    var iConfirm = confirm('Are you sure, you want to delete the record?');

    if (iConfirm == true) {
        thisCrewID = CrewID;
        if (lastExecutor_WebServiceProxy != null)
            lastExecutor_WebServiceProxy.abort();

        var service = Sys.Net.WebServiceProxy.invoke('../JibeWebService.asmx', 'Del_CrewTraining', false, { "TrainingID": TrainingID, "Deleted_By": UserID }, function (retval) { DeleteTraining_onSuccess(retval,CrewID); }, DeleteTraining_onFail);

        lastExecutor_WebServiceProxy = service.get_executor();
    }
}
function DeleteTraining_onSuccess(retval, CrewID) {
    if (retval == "1") {
        alert('Training record deleted.');
        GetCrewTrainingLog(CrewID);
    }
    else {
        alert('Unable to delete training record!!');
    }
    
}
function DeleteTraining_onFail(err_) {
    alert(err_._message);
}


//------------CREW VOYAGES-------------- ----------------
//-------------------------------------------------------

function GetCrewVoyages(CrewID) {

    $('#dvContent_Voyages').html('<div style="text-align:center"><img src="../Images/ajax-loader.gif" /></div>');
    $('#dvContent_Voyages').load("CrewDetails_Voyages.aspx?CrewID=" + CrewID + "&Mode=VIEW&rnd=" + Math.random() + ' #dvCrewVoyagesGrid');
    $('.vesselinfo').InfoBox();

}
var iCrewID = 0;
function AndVoyage(CrewID) {
    iCrewID = CrewID;
    CheckCrewSeniority(CrewID); 
}
function CheckCrewSeniority(CrewID) {
    if (lastExecutor_WebServiceProxy != null)
        lastExecutor_WebServiceProxy.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../JibeWebService.asmx', 'CheckCrewSeniority', false, { "CrewId": CrewID }, CheckCrewSeniority_onSuccess, CheckCrewSeniority_onFail);

    lastExecutor_WebServiceProxy = service.get_executor();
}
function CheckCrewSeniority_onSuccess(retval) {
    if (retval == "0") {
        alert('Company & Rank Seniority Not set.Update Company & Rank Seniority.');
    }
    else if (retval == "1") {
        alert('Company Seniority Not set.Update Company Seniority.');
    }
    else if (retval == "2") {
        alert('Rank Seniority Not set.Update Rank Seniority.');
    }
    else {
        $('#dvPopupFrame').attr("Title", "Add New Service/Status");
        $('#dvPopupFrame').css({ "width": "800px", "height": "550px", "text-allign": "center" });
        $('#frPopupFrame').load(function () { this.style.height = '550px'; this.style.width = '800px' });

        var URL = "CrewDetails_Voyages.aspx?CrewID=" + iCrewID + "&Mode=ADD_VOY&rnd=" + Math.random();
        document.getElementById("frPopupFrame").src = URL;
        showModal('dvPopupFrame', true);
    }
}
function CheckCrewSeniority_onFail(err_) {
    alert(err_._message);
}

//------------------
function EditVoyage(CrewID, VoyID) {
    $('#dvPopupFrame').attr("Title", "Edit Service/Status Details");
    $('#dvPopupFrame').css({ "width": "800px", "height": "550px", "text-allign": "center" });
    $('#frPopupFrame').load(function () { this.style.height = '550px'; this.style.width = '800px' });

    var URL = "CrewDetails_Voyages.aspx?CrewID=" + CrewID + "&VoyID=" + VoyID + "&Mode=EDIT_VOY&rnd=" + Math.random();
    document.getElementById("frPopupFrame").src = URL;
    //showModal('dvPopupFrame', true, null, function () { $('#dvPopupFrame').balloon(); });
    showModal('dvPopupFrame', true,null);
}

function DeleteVoyage(CrewID, VoyID, UserID) {
    var iConfirm = confirm('Are you sure, you want to delete the record?');

    if (iConfirm == true) {
        thisCrewID = CrewID;
        if (lastExecutor_WebServiceProxy != null)
            lastExecutor_WebServiceProxy.abort();

        var service = Sys.Net.WebServiceProxy.invoke('../JibeWebService.asmx', 'Del_CrewVoyage', false, { "CrewID": CrewID, "VoyID": VoyID, "Deleted_By": UserID }, DeleteVoyage_onSuccess, DeleteVoyage_onFail);

        lastExecutor_WebServiceProxy = service.get_executor();
    }
}
function DeleteVoyage_onSuccess(retval) {
    if (retval == "1") {
        alert('Voyage deleted.');
    }
    else if (retval == "-1") {
        alert('Voyage can not be deleted as there is an event planned for this voyage.Please remove the staff from the event and then delete the voyage');
    }
    else if (retval == "-2") {
        alert('Voyage can not be deleted as there is a vessel assignment for this staff on this voyage. Remove the vessel assignment and then delete the voyage');
    }
    else if (retval == "-3") {
        alert('Voyage can not be deleted as there is a transfer/promotion planned for this voyage. Delete the transfer/promotion record  and then delete the voyage');
    }
    GetCrewVoyages(thisCrewID);
}
function DeleteVoyage_onFail(err_) {
    alert(err_._message);
}

function EditEOC(VoyID, CrewID) {
    $('#dvPopupFrame').attr("Title", "Edit EOC Details");
    $('#dvPopupFrame').css({ "width": "600px", "height": "400px", "text-allign": "center" });
    $('#frPopupFrame').load(function () { this.style.height = '400px'; this.style.width = '600px' });

    var URL = "CrewDetails_EditEOC.aspx?CrewID=" + CrewID + "&VoyID=" + VoyID + "&Mode=EDIT_EOC&rnd=" + Math.random();
    document.getElementById("frPopupFrame").src = URL;
    showModal('dvPopupFrame', true);
}

function showEvent(ID, CrewID) {
    var url = "ViewEvent.aspx?id=" + ID + "&CrewID=" + CrewID + "&rnd=" + Math.random();
    //-- show dialog --
    $('#dialog').dialog('open');

    //-- load event data --
    $.get(url, function (data) {
        $('#dialog').html(data);
    });
    //-- remember event id --
    $('#dialog').attr('alt', ID + "&CrewID=" + CrewID);
}

function showTransferLog(evt, CrewID, VoyID, UserID) {
    if (lastExecutor_WebServiceProxy != null)
        lastExecutor_WebServiceProxy.abort();

    var service = Sys.Net.WebServiceProxy.invoke("../webservice.asmx", "getCrewTransferDetails", false, { "crewid": CrewID, "voyageid": VoyID, "userid": UserID }, showTransferLog_onSuccess, showTransferLog_onFail, evt);
    lastExecutor_WebServiceProxy = service.get_executor();
}
function showTransferLog_onSuccess(retVal, evt) {
    if (retVal.indexOf("No record") < 0)
        js_ShowToolTip(retVal, evt, evt.srcElement);
}
function showTransferLog_onFail(retVal) {
    //alert(retVal._message);
}

//------------SALARY INSTRUCTION TAB --------------------
//-------------------------------------------------------
function GetSalaryInstructions(CrewID) {

    $('#dvContent_SalaryInstructions').html('<div style="text-align:center"><img src="../Images/ajax-loader.gif" /></div>');
    $('#dvContent_SalaryInstructions').load("CrewDetails_SalaryInstruction.aspx?CrewID=" + CrewID + "&Mode=VIEW&rnd=" + Math.random() + ' #dvSalaryInstructions');
    
}
function AddNewSalaryInstruction(e, CrewID, VoyID, Vessel_ID) {
    $('#dvPopupFrame').attr("Title", "New Salary Instruction");
    $('#dvPopupFrame').css({ "width": "700px", "text-allign": "center" });
    $('#frPopupFrame').load(function () { this.style.width = '700px' });

    var URL = "CrewDetails_SalaryInstruction.aspx?CrewID=" + CrewID + "&VoyID=" + VoyID + "&Vessel_ID=" + Vessel_ID + "&Mode=ADD&rnd=" + Math.random();
    document.getElementById("frPopupFrame").src = URL;
    showModal('dvPopupFrame', true);
}

////------------TRAVEL LOG TAB ----------------------------
////-------------------------------------------------------
//function GetCrewTravelLog(CrewID) {
//    $('#dvContent_TravelLog').html('<div style="text-align:center"><img src="../Images/ajax-loader.gif" /></div>');
//    $('#dvContent_TravelLog').load("CrewDetails_TravelLog.aspx?CrewID=" + CrewID + "&Mode=VIEW&rnd=" + Math.random() + ' #dvCrewTravelLog');
//}

//------------CREW COMPLAINT TAB ------------------------
//-------------------------------------------------------
function GetCrewComplaints(CrewID) {

    $('#dvContent_CrewComplaints').html('<div style="text-align:center"><img src="../Images/ajax-loader.gif" /></div>');
    $('#dvContent_CrewComplaints').load("CrewDetails_CrewComplaints.aspx?CrewID=" + CrewID + "&Mode=VIEW&rnd=" + Math.random() + ' #dvCrewComplaints');

}

//------------CREW MAINTENANCE FEEDBACK TAB ---------------------
//-------------------------------------------------------
function GetCrewMaintenanceFeedback(CrewID) {
    $('#dvCrewMaintenanceFeedback').html('<div style="text-align:center"><img src="../Images/ajax-loader.gif" /></div>');
    $('#dvCrewMaintenanceFeedback').load("CrewDetails_MaintenanceFeedback.aspx?CrewID=" + CrewID + "&rnd=" + Math.random() + ' #dvCrewFeedbackGrid');
}

//------------CREW Reference Details ---------------------
//-------------------------------------------------------
function ReferenceDetails(CrewID, Mode) {
    $('#dvPopupFrame').attr("Title", "Referrer Details");
    //$('#dvPopupFrame').css({ "width": "700px", "height": "500px", "text-allign": "center" });
    $('#frPopupFrame').load(function () { this.style.height = (this.contentWindow.document.body.offsetHeight + 20) + 'px'; });

    var URL = "../Crew/CrewDetails_RefererDetails.aspx?CrewID=" + CrewID + "&Mode=" + Mode + "&rnd=" + Math.random();
    document.getElementById("frPopupFrame").src = URL;
    showModal('dvPopupFrame', true);
    // alert($('[id$=HiddenField_ReferenceCount]').val());
}

//------------CREW FBM Info TAB ---------------------
//-------------------------------------------------------
function GetCrewFBMInfo(CrewID) {
    $('#dvContent_FBM').html('<div style="text-align:center"><img src="../Images/ajax-loader.gif" /></div>');
    $('#dvContent_FBM').load("CrewDetails_FBM.aspx?CrewID=" + CrewID + "&rnd=" + Math.random() + ' #dvCrewFBM');
}


//------------PROPOSE YELLOW CARD------------------------
//-------------------------------------------------------
function ProposeYellowCard(CrewID) {
    $('#dvPopupFrame').attr("Title", "Propose Yellow/Red Card");
    $('#dvPopupFrame').css({ "width": "800px", "height": "500px", "text-allign": "center" });
    $('#frPopupFrame').load(function () { var h = (this.contentWindow.document.body.offsetHeight + 20); this.style.height = h + 'px'; $('#dvPopupFrame').css({ "height": h + 40 + 'px' }); });

    var URL = "ProposeCrewCard.aspx?CrewID=" + CrewID + "&Mode=INSERT&rnd=" + Math.random();
    document.getElementById("frPopupFrame").src = URL;
    showModal('dvPopupFrame', true);

}

//------------EDIT CREW PROFILE STATUS-------------------
//-------------------------------------------------------
function EditCrewPrifileStatus(CrewID) {
    $('#dvPopupFrame').attr("Title", "Edit Status");
    $('#dvPopupFrame').css({ "width": "500px", "height": "500px", "text-allign": "center" });
    $('#frPopupFrame').load(function () { this.style.height = '400px'; this.style.width = '400px' });

    var URL = "CrewDetails_EditStatus.aspx?CrewID=" + CrewID + "&Mode=EDIT&rnd=" + Math.random();
    document.getElementById("frPopupFrame").src = URL;
    showModal('dvPopupFrame', true);
}

//------------NEW UNPLANNED EVALUATION-------------------
//-------------------------------------------------------
function NewUnPlannedEvaluation(CrewID) {
    $('#dvPopupFrame').attr("Title", "New Unplanned Evaluation");
    $('#dvPopupFrame').css({ "width": "600px", "height": "380px", "text-allign": "center" });
    $('#frPopupFrame').load(function () { this.style.height = '380px'; this.style.width = '600px' });

    var URL = "CrewDetails_NewEvaluation.aspx?CrewID=" + CrewID + "&Mode=EDIT&rnd=" + Math.random();
    document.getElementById("frPopupFrame").src = URL;
    showModal('dvPopupFrame', true);
}

//------------EDIT INTERVIEW SCHEDULE-------------------
//-------------------------------------------------------
function EditInterviewSchedule(ParentPage, InterviewID) {
    $('#dvPopupFrame').attr("Title", "Edit Interview Schedule");
    $('#dvPopupFrame').css({ "width": "400px", "height": "400px", "text-allign": "center" });
    $('#frPopupFrame').load(function () { this.style.height = '400px'; this.style.width = '400px' });

    var URL = "EditInterviewSchedule.aspx?ParentPage=" + ParentPage + "&InterviewID=" + InterviewID + "&Mode=EDIT&rnd=" + Math.random();
    document.getElementById("frPopupFrame").src = URL;
    showModal('dvPopupFrame', true);
}


//------------CREW QUERY TAB ----------------------------
//-------------------------------------------------------
function GetCrewQueries(CrewID) {
    $('#dvCrewQueries').html('<div style="text-align:center"><img src="../Images/ajax-loader.gif" /></div>');
    try {
        $('#dvCrewQueries').load("/"+__app_name+"/crew/CrewDetails_CrewQueries.aspx?CrewID=" + CrewID + "&Mode=VIEW&rnd=" + Math.random() + ' #dvCrewQueries');
    }
    catch (ex) {
        $('#dvCrewQueries').html(ex.Message);
    }
}

//------------MEDICAL HISTORY TAB ----------------------------
//-------------------------------------------------------
function Get_Crew_MedicalHistory(CrewID) {
    $('#dvCrewMedHistory').html('<div style="text-align:center"><img src="../Images/ajax-loader.gif" /></div>');
    try {
        $('#dvCrewMedHistory').load("/"+__app_name+"/crew/CrewDetails_MedHistory.aspx?CrewID=" + CrewID + "&Mode=VIEW&rnd=" + Math.random() + ' #dvMedicalHistory');
    }
    catch (ex) {
        $('#dvCrewMedHistory').html(ex.Message);
    }
}

//------------MEDICAL HISTORY DETAILS ----------------------------

function Add_MedHistory_Details(CrewID) {
    $('#dvPopupFrame').attr("Title", "Add/Edit Medical History");
    $('#dvPopupFrame').css({ "width": "1000px", "height": "600px", "text-allign": "center" });
    $('#frPopupFrame').load(function () { this.style.height = '1000px'; this.style.width = '100%' });

    var URL = "AddCrewMedicalHistory.aspx?CrewID=" + CrewID + "&Mode=INSERT&rnd=" + Math.random();
    document.getElementById("frPopupFrame").src = URL;
    
    var fn_callback = "Get_Crew_MedicalHistory(" + CrewID + ")";
    showModal('dvPopupFrame', true, fn_callback);
}


function Show_MedHistory_Details(CrewID, ID,Vessel_ID,Office_ID) {
    $('#dvPopupFrame').attr("Title", "Show Medical History Details");
    $('#dvPopupFrame').css({ "width": "1000px", "height": "600px", "text-allign": "center" });
    $('#frPopupFrame').load(function () { this.style.height = '1000px'; this.style.width = '100%' });

    var URL = "AddCrewMedicalHistory.aspx?CrewID=" + CrewID + "&ID=" + ID + "&Vessel_ID=" + Vessel_ID + "&Office_ID=" + Office_ID + "&Mode=VIEW&rnd=" + Math.random();
    document.getElementById("frPopupFrame").src = URL;

    var fn_callback = "Get_Crew_MedicalHistory(" + CrewID + ")";
    showModal('dvPopupFrame', true, fn_callback);
}

//------------SHOW SENIORITY LOG ------------
function Show_Seniority_Log(evt, CrewID, VoyID, UserID) {
    if (lastExecutor_WebServiceProxy != null)
        lastExecutor_WebServiceProxy.abort();
//    var service = Sys.Net.WebServiceProxy.invoke("../webservice.asmx", "Get_Seniority_Log", false, { "CrewID": CrewID, "VoyageID": VoyID, "UserID": UserID }, Show_Seniority_Log_onSuccess, Show_Seniority_Log_onFail, evt);
//    lastExecutor_WebServiceProxy = service.get_executor();

    var service = Sys.Net.WebServiceProxy.invoke("../JibeWebservice.asmx", "Get_Seniority_Log", false, { "CrewID": CrewID, "VoyageID": VoyID, "UserID": UserID }, Show_Seniority_Log_onSuccess, Show_Seniority_Log_onFail, evt);
    lastExecutor_WebServiceProxy = service.get_executor();

}
function Show_Seniority_Log_onSuccess(retVal, evt) {
    if (retVal.indexOf("No record") < 0)
        js_ShowToolTip(retVal, evt, evt.srcElement);
}
function Show_Seniority_Log_onFail(retVal) {
    //alert(retVal._message);
}
//------------------- TRAINING TAB --------------------------
function showTrainingAttachments(event, TrainingID) {
    evt = event;
    if (lastExecutor_WebServiceProxy != null)
        lastExecutor_WebServiceProxy.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../JibeWebService.asmx', 'Get_Training_Attachments', false, { "TrainingID": TrainingID, "UserID": 0 }, showTrainingAttachments_onSuccess, showTrainingAttachments_onFail);
    lastExecutor_WebServiceProxy = service.get_executor();

}

function showTrainingAttachments_onSuccess(retval) {    
    js_ShowToolTip_Fixed(retval, evt, null, "Attachments");
}
function showTrainingAttachments_onFail(err_) {
    alert(err_._message);
}

function openTrainingAttachment(obj) {
    window.open("../Uploads/CrewDocuments/" + $(obj).attr('alt'));
}

function OpenAttachmentWindow(CrewID, TrainingID) {
    $('#dvPopupFrame').attr("Title", "Training Attachments");
    $('#dvPopupFrame').css({ "width": "350px", "text-allign": "center" });
    $('#frPopupFrame').load(function () { this.style.height = (this.contentWindow.document.body.offsetHeight + 20) + 'px'; });

    var URL = "CrewDetails_Training.aspx?CrewID=" + CrewID + "&TrainingID=" + TrainingID + "&Mode=ATT&rnd=" + Math.random() + ' #dvTraining';
    document.getElementById("frPopupFrame").src = URL;

    var fn_callback = "callback_GetCrewTrainingLog(" + CrewID + ")";
    showModal('dvPopupFrame', true, fn_callback);
}


function ViewEvaluation(evt, objthis, CrewId, EvaluationId) {
    if (lastExecutor_WebServiceProxy != null)
        lastExecutor_WebServiceProxy.abort();
    var service = Sys.Net.WebServiceProxy.invoke('../JibeWebService.asmx', 'Get_BelowAverageEvaluationDetails', false, { "CrewID": CrewId, "EvaluationId": EvaluationId }, onSuccess1, onFail1, new Array(evt, objthis));

    lastExecutor_WebServiceProxy = service.get_executor();
}
function onSuccess1(retVal, evt) {
    if (retVal.indexOf("No record") < 0) {
        var str = '<table><tr><td>Evaluation Below Average</td></tr><tr><td>';
        var str1 = '</td></tr></table>';
        js_ShowToolTip(str + retVal + str1, evt[0], evt[1]);
    }
}
function onFail1(retVal) {
    //alert(retVal._message);
}

function ViewFBMDetails(FBM_ID, UserID) {
    window.open("../QMS/FBM/FBM_Main_Report_Details.aspx?FBMID=" + FBM_ID + "&UserID=" + UserID);
}

function DeleteEvaluation(Id, CrewID, VoyID, UserID) {
    var iConfirm = confirm('Are you sure, you want to delete the record?');

    if (iConfirm == true) {
        thisCrewID = CrewID;
        if (lastExecutor_WebServiceProxy != null)
            lastExecutor_WebServiceProxy.abort();

        var service = Sys.Net.WebServiceProxy.invoke('../JibeWebService.asmx', 'Del_CrewEvaluation', false, { "ID": Id, "CrewId": CrewID, "VoyageId": VoyID, "Deleted_By": UserID }, DeleteEvaluation_onSuccess, DeleteEvaluation_onFail);

        lastExecutor_WebServiceProxy = service.get_executor();
    }
}
function DeleteEvaluation_onSuccess(retval) {
    if (retval == "1") {
        alert('Evaluation deleted.');
    }
    else if (retval == "-3") {
        alert('Evaluation cannot be deleted');
    }
    GetEvaluationResult(thisCrewID);
}
function DeleteEvaluation_onFail(err_) {
    alert(err_._message);
}
//------------CREW OTHER SERVICES-------------- ----------------
//-------------------------------------------------------

function GetOtherServices(CrewID) {

    $('#dvOtherServices').html('<div style="text-align:center"><img src="../Images/ajax-loader.gif" /></div>');
    $('#dvOtherServices').load("CrewDetails_Services.aspx?CrewID=" + CrewID + "&Mode=VIEW&rnd=" + Math.random() + ' #dvCrewOtherServiceGrid');
    //$('.vesselinfo').InfoBox();

}
//------------CREW OTHER SERVICES-------------- ----------------
//-------------------------------------------------------
function GetCrewMatrixDetails(CrewID) {
    document.getElementById("ctl00_MainContent_frmCrewMatrix").src = 'CrewDetails_CrewMatrix.aspx?CrewID=' + CrewID + '&rnd=' + Math.random();
}
function GetCrewSeniority(CrewID) {
    document.getElementById("ctl00_MainContent_frmSeniority").src = 'CrewDetails_Seniority.aspx?CrewID=' + CrewID + '&rnd=' + Math.random() ;
}
function AddOtherServices(CrewID) {
    $('#dvPopupFrame').attr("Title", "Add Other Services");
    $('#dvPopupFrame').css({ "width": "500px", "height": "300px", "text-allign": "center" });
    //  $('#frPopupFrame').css({ "width": "780px", "height": "510px", "text-allign": "center" });
    $('#frPopupFrame').load(function () { this.style.height = (this.contentWindow.document.body.offsetHeight + 200) + 'px'; });

    var URL = "CrewDetails_Services.aspx?CrewID=" + CrewID + "&Mode=ADD&rnd=" + Math.random();
    document.getElementById("frPopupFrame").src = URL;
    showModal('dvPopupFrame', true);
    //$("#dvPopupFrame").animate({ top: "100" });
}
function EditOtherServices(CrewID,ID) {
    $('#dvPopupFrame').attr("Title", "Edit Other Services");
    $('#dvPopupFrame').css({ "width": "500px", "height": "300px", "text-allign": "center" });
    // $('#frPopupFrame').css({ "width": "780px", "height": "510px", "text-allign": "center" });
    $('#frPopupFrame').load(function () { this.style.height = (this.contentWindow.document.body.offsetHeight + 200) + 'px'; });

    var URL = "CrewDetails_Services.aspx?CrewID=" + CrewID + "&ID=" + ID + "&Mode=EDIT&rnd=" + Math.random();
    document.getElementById("frPopupFrame").src = URL;
    showModal('dvPopupFrame', true);
    //$("#dvPopupFrame").animate({ top: "100" });
}

function DeleteOtherServices(CrewID, ID, UserID) {
    var iConfirm = confirm('Are you sure, you want to delete the record?');

    if (iConfirm == true) {
        thisCrewID = CrewID;
        if (lastExecutor_WebServiceProxy != null)
            lastExecutor_WebServiceProxy.abort();

        var service = Sys.Net.WebServiceProxy.invoke('../JibeWebService.asmx', 'Del_CrewOtherServices', false, { "CrewID": CrewID, "ID": ID, "Deleted_By": UserID }, DeleteOtherServices_onSuccess, DeleteOtherServices_onFail);

        lastExecutor_WebServiceProxy = service.get_executor();
    }
}
function DeleteOtherServices_onSuccess(retval) {
    if (retval == "1") {
        alert('Service deleted.');
    }
    GetOtherServices(thisCrewID);
}
function DeleteOtherServices_onFail(err_) {
    alert(err_._message);
}


//Crew Confidential

function GetConfidentialResult(CrewID) {
    $('#frmConfidential').html('<div style="text-align:center"><img src="../Images/ajax-loader.gif" /></div>');
    $('#frmConfidential').load("CrewDetails_Confidential.aspx?ID=" + CrewID + "&rnd=" + Math.random() + ' #dvCrewConfidentialResults');
    
}

function EditConfidential(CrewID) {
    $('#dvPopupFrame').attr("Title", "Edit Confidential Details");
    $('#dvPopupFrame').css({ "width": "1130px", "height": "680px", "text-allign": "center" });
    $('#frPopupFrame').load(function () { this.style.height = '680px'; this.style.width = '1130px' });

    var URL = "CrewDetails_Confidential.aspx?ID=" + CrewID + "&Mode=EDIT";
    document.getElementById("frPopupFrame").src = URL;
    showModal('dvPopupFrame', true);
    //$("#dvPopupFrame").animate({ top: "100" });
}


//Personal Tab
function GetPersonalDetails(CrewID) {
    $('#frmPersonal').html('<div style="text-align:center"><img src="../Images/ajax-loader.gif" /></div>');
    $('#frmPersonal').load("CrewDetails_PersonalDetails.aspx?ID=" + CrewID + "&rnd=" + Math.random() + ' #dvCrewPersonal');
   
}

function EditContactDetails(CrewID) {
    $('#dvPopupFrame').attr("Title", "Edit Contact Details");
    $('#dvPopupFrame').css({ "width": "710px", "height": "220px", "text-allign": "center" });
    $('#frPopupFrame').load(function () { this.style.height = '220px'; this.style.width = '710px' });
    var URL = "CrewDetails_PersonalDetails.aspx?ID=" + CrewID + "&Mode=EDITCONTACT";
    document.getElementById("frPopupFrame").src = URL;
    showModal('dvPopupFrame', true);
}

function EditGeneralDetails(CrewID) {
  
    $('#dvPopupFrame').attr("Title", "Edit General Details");
    $('#dvPopupFrame').css({ "width": "940px", "height": "500px", "text-allign": "center" });
    $('#frPopupFrame').load(function () { this.style.height = '500px'; this.style.width = '940px' });
    var URL = "CrewDetails_PersonalDetails.aspx?ID=" + CrewID + "&Mode=EDITGeneral";
    document.getElementById("frPopupFrame").src = URL;
    showModal('dvPopupFrame', true);
}

function EditPassportDetails(CrewID) {
    $('#dvPopupFrame').attr("Title", "Edit Documents Details");
    $('#dvPopupFrame').css({ "width": "1200px", "height": "370px", "text-allign": "center" });
    $('#frPopupFrame').load(function () { this.style.height = '370px'; this.style.width = '1200px' });
    var URL = "CrewDetails_PersonalDetails.aspx?ID=" + CrewID + "&Mode=EDITPassport";
    document.getElementById("frPopupFrame").src = URL;
    showModal('dvPopupFrame', true);
}
function EditDimensionDetails(CrewID) {
    $('#dvPopupFrame').attr("Title", "Edit Dimension Details");
    $('#dvPopupFrame').css({ "width": "880px", "height": "250px", "text-allign": "center" });
    $('#frPopupFrame').load(function () { this.style.height = '250px'; this.style.width = '880px' });
    var URL = "CrewDetails_PersonalDetails.aspx?ID=" + CrewID + "&Mode=EDITDimension";
    document.getElementById("frPopupFrame").src = URL;
    showModal('dvPopupFrame', true);
}

function EditConfiguredFieldDetails(CrewID) {
    $('#dvPopupFrame').attr("Title", "Edit Additional Fields Details");
    $('#dvPopupFrame').css({ "width": "800px", "height": "180px", "text-allign": "center" });
    $('#frPopupFrame').load(function () { this.style.height = '180px'; this.style.width = '800px' });
    var URL = "CrewDetails_PersonalDetails.aspx?ID=" + CrewID + "&Mode=EDITConf";
    document.getElementById("frPopupFrame").src = URL;
    showModal('dvPopupFrame', true);
}

function EditCrewBankAccAllotment(CrewID) {
    $('#dvPopupFrame').attr("Title", "Edit Bank Allotment Details");
    $('#dvPopupFrame').css({ "width": "500px", "height": "200px", "text-allign": "center" });
    $('#frPopupFrame').load(function () { this.style.height = '200px'; this.style.width = '500px' });

    var URL = "CrewDetails_BankAcc.aspx?CrewID=" + CrewID +  "&Mode=EDITAllotment&rnd=" + Math.random();
    document.getElementById("frPopupFrame").src = URL;
    showModal('dvPopupFrame', true);
    //$("#dvPopupFrame").animate({ top: "100" });

}