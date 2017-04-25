$(document).ready(onDocument_Ready);

function onDocument_Ready() {
    var CrewID = $('[id$=HiddenField_CrewID]').val();
    var CurrentUserID = $('[id$=HiddenField_UserID]').val();
    var SelectedTab = $('[id$=HiddenField_SelectTab]').val();

    var strPhotoUploadpath = $('[id$=HiddenField_PhotoUploadPath]').val();
    var strDocumentUploadpath = $('[id$=HiddenField_DocumentUploadPath]').val();

    $("#tabs").tabs();
    $('#tabs').bind('tabsselect', function (event, ui) {
        $('[id$=HiddenField_SelectTab]').val(ui.index);

        if (ui.index == 0) {
            GetPersonalDetails(CrewID);
        }
        //Pre-Joining Exp Tab
        if (ui.index == 1) {
            $('[id$=ImgReloadPreJoining]').trigger('click');
        }

        //Interview Tab
        if (ui.index == 2) {
            GetInterviewResult(CrewID);
        }

        //Voyage Tab
        if (ui.index == 3) {
            GetCrewVoyages(CrewID);
        }

        //Documents Tab
        if (ui.index == 4) {
            ReloadDocuments(CrewID);
        }
        //NOK/Dependents Tab
        if (ui.index == 5) {
            GetCrewNOKAndDependents(CrewID);
        }

        //Bank Acc Tab
        if (ui.index == 6) {
            GetCrewBankAcc(CrewID);
        }
        //Documents Tab
        if (ui.index == 7) {
            GetCrewFeedback(CrewID);
        }

        //Crew Complaints Tab
        if (ui.index == 8) {
            GetCrewComplaints(CrewID);
        }
        if (ui.index == 9) {
            GetEvaluationResult(CrewID);
        }
        if (ui.index == 10) {
            GetCrewLog(CrewID);
        }
        if (ui.index == 11) {
            GetCrewTravelLog(CrewID);
        }
        if (ui.index == 12) {
            GetCrewTrainingLog(CrewID);
        }
        if (ui.index == 13) {
            GetCrewQueries(CrewID);
        }
        if (ui.index == 14) {
            Get_Crew_MedicalHistory(CrewID);
        }
        if (ui.index == 15) {
            GetCrewMaintenanceFeedback(CrewID);
        }
        if (ui.index == 16) {
            GetCrewMatrixDetails(CrewID);
        }
        if (ui.index == 17) {
            GetCrewFBMInfo(CrewID);
        }
        if (ui.index == 18) {
            GetCrewSeniority(CrewID);
        }

        // Crew Confidential
        if (ui.index == 19) {
            GetConfidentialResult(CrewID);
        }

    });

    if (SelectTab != "")
        $("#tabs").tabs('select', SelectedTab);
    else
        $("#tabs").tabs('select', 3);

    crewPhotoUploadSettings(CrewID, strPhotoUploadpath, CurrentUserID);
    crewDocumentUploadSettings(CrewID, strDocumentUploadpath, CurrentUserID);

    $('#frmCrewCard').load(function () {
        this.style.height = (this.contentWindow.document.body.offsetHeight + 20) + 'px';
    });

    $('.draggable').draggable();

    SelectTab(SelectedTab);
    Bind_HoverEffect();
    setTimeout($('.vesselinfo').InfoBox(), 2000);
}

function SelectTab(tID) {
    if (tID)
        $("#tabs").tabs('select', tID);
    else
        $("#tabs").tabs('select', 3);
};

function Bind_HoverEffect() {
    $('.event-link').bind({ mouseenter: function () { $('#dvVoyageInfo').html("View Event"); }, mouseleave: function () { $('#dvVoyageInfo').html(""); } });
    $('.print-link').bind({ mouseenter: function () { $('#dvVoyageInfo').html("Print Contract"); }, mouseleave: function () { $('#dvVoyageInfo').html(""); } });
    $('.wages-link').bind({ mouseenter: function () { $('#dvVoyageInfo').html("View/Edit Wages"); }, mouseleave: function () { $('#dvVoyageInfo').html(""); } });
    $('.salary-link').bind({ mouseenter: function () { $('#dvVoyageInfo').html("Salary Instruction"); }, mouseleave: function () { $('#dvVoyageInfo').html(""); } });
    $('.doc-link').bind({ mouseenter: function () { $('#dvVoyageInfo').html("Upload Documents"); }, mouseleave: function () { $('#dvVoyageInfo').html(""); } });

    $('.travel-link').bind({ mouseenter: function () { $('#dvVoyageInfo').html("New Travel Request"); }, mouseleave: function () { $('#dvVoyageInfo').html(""); } });
    $('.transfer-link').bind({ mouseenter: function () { $('#dvVoyageInfo').html("Transfer/Promotion"); }, mouseleave: function () { $('#dvVoyageInfo').html(""); } });
    $('.download-contract').bind({ mouseenter: function () { $('#dvVoyageInfo').html("Download and Print Contract"); }, mouseleave: function () { $('#dvVoyageInfo').html(""); } });
}
function UCase(obj) {
    if (obj) {
        obj.value = obj.value.toString().toUpperCase();
    }
}
function onMouseOver(obj) {
    if (obj) {
        obj.style.border = '2px solid gray;';
    }
}
function onMouseOut(obj) {
    if (obj) {
        obj.style.border = '1px solid gray;';
    }
}

function crewPhotoUploadSettings(CrewID_, strUploadpath_, UserID_) {
    $('#fileInput').uploadify({
        'uploader': '../scripts/uploadify/uploadify.swf',
        'script': '../UserControl/CrewPhotoHandler.ashx',
        'scriptData': { 'id': CrewID_, 'userid': UserID_, 'uploadpath': strUploadpath_ },
        'cancelImg': '../scripts/uploadify/cancel.png',
        'auto': true,
        'multi': false,
        'fileDesc': 'Image Files',
        'fileExt': '*.jpg;*.png;*.gif;*.bmp;*.jpeg',
        'queueSizeLimit': 90,
        'sizeLimit': 1024000,
        'buttonText': 'Browse Photo',
        'buttonImage': '',
        'folder': '/uploads',
        'onError': function (event, ID, fileObj, errorObj) { if (errorObj.type == 'File Size') { alert('The document size exceeds the permited file size (1 MB)'); } else { alert(errorObj.type + ' Error: ' + errorObj.info); } },
        'onSelect': function (event, queueID, fileObj) { if (fileObj.size > 1024000) { alert('Unable to upload file: ' + fileObj.name + '\n\nThe file size (' + parseFloat(fileObj.size) / 1000 + 'KB) exceded the maximum file size (1 MB)'); } },
        'onAllComplete': function (event, queueID, fileObj, response, data) { $('#dvCrewPhotoUploader').hide(); window.location.href = 'CrewPhotoCrop.aspx?ID=' + CrewID_; }
    });
}

function crewDocumentUploadSettings(CrewID_, strUploadpath_, UserID_) {
    $('#crewDocumentInput').uploadify({
        'uploader': '../scripts/uploadify/uploadify.swf',
        'script': '../UserControl/CrewDocumentUploader.ashx',
        'scriptData': { 'id': CrewID_, 'userid': UserID_, 'uploadpath': strUploadpath_ },
        'multi': true,
        'auto': true,
        'sizeLimit': 1024000,
        'buttonText': 'Browse Files ...',
        'folder': '/Uploads/CrewDocuments',
        'cancelImg': '../scripts/uploadify/img/cancel.png',
        'onError': function (event, ID, fileObj, errorObj) { if (errorObj.type == 'File Size') { alert('The document size exceeds the permited file size (1 MB)'); } else { alert(errorObj.type + ' Error: ' + errorObj.info); }; $('#crewDocumentInput').uploadifyClearQueue(); return false; },
        'onCancel': function (event, queueID, fileObj, data) { $('#dvPersonalDetails').show(); },
        'onAllComplete': function (event, queueID, fileObj, response, data) { $('[id$=ImgReloadDocuments]').trigger('click'); $('#dvCrewDocumentUploader').hide(); }
    });
}

function initAutoComplete(controlid_, list_) {
    var options = { serviceUrl: '../UserControl/AutoComplete_Handler.ashx', params: { list: list_} };
    $('.autocomplete').autocomplete(options);
    $('[id$=' + controlid_ + ']').autocomplete(options);
    $('#' + controlid_).autocomplete(options);
}


function showDialog(dialog_) {
    //alert(navigator.appVersion);
    var OSName = "";
    var Browser = "";
    var CrewID = $('[id$=HiddenField_CrewID]').val();

    var DocURL = 'CrewPrepDocuments.aspx?CrewID=' + CrewID;


    if (navigator.appVersion.indexOf("Win") != -1) OSName = "Windows";
    if (navigator.appVersion.indexOf("Mac") != -1) OSName = "MacOS";
    if (navigator.appVersion.indexOf("X11") != -1) OSName = "UNIX";
    if (navigator.appVersion.indexOf("Linux") != -1) OSName = "Linux";

    if (navigator.appVersion.indexOf("MSIE") != -1) Browser = "MSIE";
    if (navigator.appVersion.indexOf("Chrome") != -1) Browser = "Chrome";
    if (navigator.appVersion.indexOf("Firefox") != -1) Browser = "Firefox";

    if (dialog_ == '#dvCrewPhotoUploader' || dialog_ == '#dvCrewPhotoUploader1' || dialog_ == '#PhotoUploaderAddEditCrew') {
        $('#dvPopupFrame').attr("Title", "Upload Photo");
        $('#dvPopupFrame').css({ "width": "425px", "height": "150px", "text-allign": "center" });

        var PhotoURL = "";
        if (dialog_ == '#dvCrewPhotoUploader')
            PhotoURL = 'Crew_UploadPhoto.aspx?CrewID=' + CrewID + '&ParentPage=CrewDetails.aspx';
        else if (dialog_ == '#PhotoUploaderAddEditCrew')
            PhotoURL = 'Crew_UploadPhoto.aspx?CrewID=' + CrewID + '&ParentPage=AddEditCrewNew.aspx';
        else
            PhotoURL = 'Crew_UploadPhoto.aspx?CrewID=' + CrewID + '&ParentPage=AddEditCrew.aspx';

        document.getElementById("frPopupFrame").src = PhotoURL;
        dialog_ = '#dvPopupFrame';
        showModal('dvPopupFrame', true);
    }

    if (dialog_ == '#dvCrewDocumentUploader') {
        //if (OSName == "MacOS") {
        document.getElementById("frPopupFrame").src = DocURL;
        dialog_ = '#dvPopupFrame';

        $('#dvPopupFrame').attr("Title", "Upload Perpetual Documents");
        $('#dvPopupFrame').css({ "width": "1000px", "height": "570px", "text-allign": "center" });
        $('#frPopupFrame').load(function () { this.style.width = '1000px'; });

        showModal('dvPopupFrame', true);
        
    }

};

function getDialogContent(CrewID) {
    $.get('CrewPhotoCrop.aspx?ID=' + CrewID, function (data) {
        $('#dialog').html(data);
    });
};
function InterviewonClick(CrewID, InterviewID) {
    if (CrewID) {
        window.open("Interview.aspx?ID=" + InterviewID + "@CrewID=" + CrewID)
    }
}

function ReloadPhoto() {
    $('[id$=ImgReloadDocuments]').trigger('click');
}
function CrewPhotoCrop(PageName) {
    var CrewID = $('[id$=HiddenField_CrewID]').val();
    window.location.href = 'CrewPhotoCrop.aspx?ID=' + CrewID + '&ParentPageName=' + PageName;
}

function ShowNotification(title, msg, permanent) {
    try {
        if (permanent == true) {
            $.notifier.broadcast(
	                {
	                    ttl: title,
	                    msg: msg,
	                    skin: 'rounded,red'
	                }
                    );
        }
        else {
            $.notifier.broadcast(
	                {
	                    ttl: title,
	                    msg: msg,
	                    skin: 'rounded,red',
	                    duration: 5000
	                }
                    );
        }
    }
    catch (ex) {
        alert(msg);
    }
}

$(function () {
    // Dialog			
    $('#dialog').dialog({
        autoOpen: false,
        modal: true,
        width: 800,
        buttons: {
            "Close": function () {
                $(this).dialog("close");
            },
            "Reload": function () {
                var url = "ViewEvent.aspx?id=" + $('#dialog').attr('alt') + "&rnd=" + Math.random();

                $("#dialog").dialog({ title: 'Loading Data ...' });

                $.get(url, function (data) {
                    $('#dialog').html(data);
                    $("#dialog").dialog({ title: 'Event Details' });
                });
            }
        }
    });


    //hover states on the static widgets
    $('#dialog_link, ul#icons li').hover(
					function () { $(this).addClass('ui-state-hover'); },
					function () { $(this).removeClass('ui-state-hover'); }
				);

});

var thisCrewID = 0;
function DeleteInterview(ID, CrewID, UserID) {
    var iConfirm = confirm('Are you sure, you want to delete the record?');

    if (iConfirm == true) {
        thisCrewID = CrewID;
        if (lastExecutor != null)
            lastExecutor.abort();
        var service = Sys.Net.WebServiceProxy.invoke('../JibeWebService.asmx', 'Del_Interview', false, { "InterviewID": ID, "CrewID": CrewID, "Deleted_By": UserID }, DeleteInterview_onSuccess, DeleteInterview_onFail);

        lastExecutor = service.get_executor();
    }
}
function DeleteInterview_onSuccess(retval) {
    if (retval == "1") {
        GetInterviewResult(thisCrewID);
    }
    if (retval == "-1") {
        alert('Interview can not be deleted.');
    }
}
function DeleteInterview_onFail(err_) {
    alert(err_._message);
}

var thisCrewID = 0;
function DeleteBrief(ID, CrewID, UserID) {
    var iConfirm = confirm('Are you sure, you want to delete the record?');

    if (iConfirm == true) {
        thisCrewID = CrewID;
        if (lastExecutor != null)
            lastExecutor.abort();
        var service = Sys.Net.WebServiceProxy.invoke('../JibeWebService.asmx', 'Del_Interview', false, { "InterviewID": ID, "CrewID": CrewID, "Deleted_By": UserID }, DeleteBrief_onSuccess, DeleteBrief_onFail);

        lastExecutor = service.get_executor();
    }
}
function DeleteBrief_onSuccess(retval) {
    if (retval == "1") {
        GetInterviewResult(thisCrewID);
    }
    if (retval == "-1") {
        alert('Briefing can not be deleted.');
    }
}
function DeleteBrief_onFail(err_) {
    alert(err_._message);
}

function Load_InterviewResults(CrewID) {
    $('#dvCrewFeedback').html('<div style="text-align:center"><img src="../Images/ajax-loader.gif" /></div>');
    $('#dvCrewFeedback').load("CrewDetails_InterviewResult.aspx?ID=" + CrewID + "&rnd=" + Math.random() + ' #dvInterviewResult');
}

function ReloadDocuments(CrewID) {
    $("#iFrame_Documents").attr("src", "CrewDetails_Documents.aspx?ID=" + CrewID);
}

function IsSSNValid(SSNNumber) {
    if (SSNNumber == "") {
        return true;
    }
    if (SSNNumber == "000-00-0000") {
        return true;
    }
    if (SSNNumber.split('-').length == 3) {
        if (SSNNumber.split('-')[0].length != 3 || SSNNumber.split('-')[1].length != 2 || SSNNumber.split('-')[2].length != 4 || SSNNumber.split('-')[0] == "666" || SSNNumber.split('-')[0] == "900" || SSNNumber.split('-')[0] == "999" || SSNNumber.split('-')[0] == "000" || SSNNumber.split('-')[1] == "00" || SSNNumber.split('-')[2] == "0000") {
            return true;
        }
    }
    else {
        return true;
    }
    if (SSNNumber.split('-').length == 3) {
        if (parseInt(SSNNumber.split('-')[0]) >= 900 && parseInt(SSNNumber.split('-')[0]) <= 999) {
            return true;
        }
    }
}

