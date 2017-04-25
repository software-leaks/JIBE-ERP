<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FMSFileLoader.aspx.cs" Inherits="FMSFileLoader"
    EnableEventValidation="false" EnableViewState="true" ViewStateMode="Enabled" %>

<%@ Register Src="~/UserControl/ctlRecordNavigation.ascx" TagName="ctlRecordNavigation"
    TagPrefix="uc1" %>
<%@ Register Src="~/UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<%@ Register Src="~/UserControl/ucCustomDropDownList.ascx" TagName="ucfDropdown"
    TagPrefix="CustomFilter" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Document</title>
    <link href="css/Main.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../Scripts/AsyncResponse.js"></script>
    <script src="../Scripts/jquery.min1.8.js" type="text/javascript"></script>
    <script src="../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <script src="JS/common.js" type="text/javascript"></script>
    <link href="../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/Common_Functions.js" type="text/jscript"></script>
    <link href="../Styles/CssCustomFilterControls.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/ucCustomDropDownList.js" type="text/javascript"></script>
    <script src="../Scripts/JSCustomFilterControls.js" type="text/javascript"></script>
    <style type="text/css">
        .awesome
        {
            border: 1px solid #2980B9;
            display: inline-block;
            cursor: pointer;
            background: #3498DB;
            color: #FFF;
            font-size: 14px;
            padding: 6px 8px;
            text-decoration: none;
            text-shadow: 0px 1px 0px #2980B9;
            margin-right: 5px;
            margin-bottom: 5px;
            border-radius: 0px;
        }
        body
        {
            font-family: Tahoma;
            font-size: 11px;
        }
        historyBox
        {
            background-color: #efefef;
            border: 1px solid inset;
            height: 200px;
        }
        .WrapText
        {
            white-space: normal;
            word-wrap: break-word;
        }
        .label
        {
            padding-bottom: 5px;
            padding-top: 5px;
        }
        .btnGetFormsReceived
        {
            display: none;
        }
        .click-nav
        {
            width: 20px;
            text-align: center;
        }
        
        .click-nav ul
        {
            padding: 0px;
            width: 20px;
            text-align: center;
        }
        .click-nav ul li
        {
            position: relative;
            list-style: none;
            cursor: pointer;
        }
        .click-nav ul li ul
        {
            position: absolute;
            border: 1px solid black;
        }
        .click-nav ul .clicker
        {
            position: relative;
            background: #2284B5;
            color: #FFF;
            text-decoration: underline;
        }
        .click-nav ul .clicker:hover, .click-nav ul .active
        {
            background: #196F9A;
        }
        
        .click-nav ul li a
        {
            transition: background-color 0.2s ease-in-out;
            -webkit-transition: background-color 0.2s ease-in-out;
            -moz-transition: background-color 0.2s ease-in-out;
            display: block;
            padding: 4px 0px 4px 0px;
            background: #FFF;
            color: #333;
            text-decoration: none;
        }
        .click-nav ul li a:hover
        {
            background: #F2F2F2;
        }
        
        /* Fallbacks */
        
        .click-nav .no-js:hover ul
        {
            display: block;
        }
        .click-nav .no-js:active ul
        {
            display: block;
        }
        .click-nav .no-js ul
        {
            display: none;
        }
        tr.Approved
        {
            color: #5C8A00;
        }
        .highlightRow
        {
            background-color: orange;
        }
        tr.Completed
        {
            color: #1947D1;
        }
        tr.Due
        {
            color: #B26B00;
        }
        
        tr.OverDue
        {
            color: #FF3333;
        }
        .FormSelected
        {
            padding-left: 0px;
            margin-left: 0px;
        }
        .FormsReceived
        {
            background-color: #BBCFCF;
            font-weight: bold;
            height: 35px;
        }
        .FormsNotReceived
        {
            background-color: #E0EBEB;
            font-weight: normal;
            height: 30px;
        }
        .saveCenter
        {
            float: right;
        }
        .treePadding
        {
            padding-left: 5px;
        }
    </style>
    <style type="text/css">
        .overflowmanagement
        {
            text-overflow: ellipsis;
            overflow: hidden;
            display: block;
            white-space: nowrap;
        }
        .TFtable
        {
            width: 100%;
            border-collapse: collapse;
        }
        .TFtable td
        {
            border: #959EAF 0px outset;
        }
        /* provide some minimal visual accomodation for IE8 and below */
        .TFtable tr
        {
            background: #e6f1f5;
        }
        /*  Define the background color for all the ODD background rows  */
        .TFtable tr:nth-child(odd)
        {
            background: #e6f1f5;
        }
        /*  Define the background color for all the EVEN background rows  */
        .TFtable tr:nth-child(even)
        {
            background: #e6f1f5;
        }
        .ArchivedLable
        {
            color: Red;
            font-size: 11px;
        }
        
        div.ex2
        {
            max-width: 500px;
            margin: auto;
            border: 3px solid #73AD21;
        }
    </style>
</head>
<body style="margin: 0px; overflow-y: hidden;">
    <script type="text/javascript">
        function showModaltest(dvPopUpID, isDraggable, callbackFunction, callback_Help_Function) {
            try {
                if (dvPopUpID) {
                    var dvPopUp = document.getElementById(dvPopUpID);

                    $(dvPopUp).show();
                    $(dvPopUp).css({ 'border': '1px solid #aabbee', 'background-color': 'white' });

                    if (!document.getElementById('overlay')) {
                        var dvOverlay = document.createElement('div');
                        dvOverlay.id = 'overlay';
                        document.body.appendChild(dvOverlay);
                    }

                    //Remove the already added header (if exists)
                    //-------------------------------------------
                    if (document.getElementById(dvPopUpID + '_dvModalPopupHeader'))
                        dvPopUp.removeChild(document.getElementById(dvPopUpID + '_dvModalPopupHeader'));

                    //create new header
                    //-------------------------------------------            
                    var dvModalPopupHeader = document.createElement('div');
                    dvModalPopupHeader.id = dvPopUpID + '_dvModalPopupHeader';

                    isModalOpen = 1;
                    ModalPopUpID = dvPopUpID;
                    var title = $(dvPopUp).attr('title') == undefined ? '' : $(dvPopUp).attr('title');

                    dvModalPopupHeader.innerHTML = "<span id='" + dvPopUpID + '_dvModalPopupTitle' + "' >" + title + "</span>";
                    $(dvModalPopupHeader).css({ 'width': '100%', 'height': 22, backgroundColor: "transparent", color: 'Black', 'background-color': '#A9D0F5', 'font-weight': 'Bold', 'cursor': 'move' });

                    $(dvPopUp).attr('title', '');

                    var dvModalPopupControlBox = document.createElement('div');
                    $(dvModalPopupControlBox).css({ 'height': 20, backgroundColor: "transparent", 'right': 2, 'top': 2, 'position': 'absolute' });

                    var dvModalPopupCloseButton = document.createElement('div');
                    dvModalPopupCloseButton.id = dvPopUpID + '_dvModalPopupCloseButton';
                    dvModalPopupCloseButton.innerHTML = '<img id="closePopupbutton" src="/' + __app_name + '/images/close.png" style="cursor:pointer;" alt="Press ESC to Close" id="imgbtnPopupClose">';
                    $(dvModalPopupCloseButton).css({ 'height': 20, backgroundColor: "transparent", 'top': 0, 'float': 'right' });
                    $(dvModalPopupCloseButton).click(function () { $('#overlay').hide(); $(dvPopUp).hide(); try { setTimeout(callbackFunction, 1); } catch (ex) { } });
                    dvModalPopupControlBox.appendChild(dvModalPopupCloseButton);

                    if (callback_Help_Function) {
                        var dvModalPopupHelpButton = document.createElement('div');
                        dvModalPopupHelpButton.id = dvPopUpID + '_dvModalPopupHelpButton';
                        dvModalPopupHelpButton.innerHTML = '<img  src="/' + __app_name + '/images/help16.png" style="cursor:pointer;">';
                        $(dvModalPopupHelpButton).css({ 'height': 20, backgroundColor: "transparent", 'padding-right': 5, 'top': 0, 'float': 'right' });
                        $(dvModalPopupHelpButton).click(function () { try { setTimeout(callback_Help_Function, 1); } catch (ex) { } });
                        dvModalPopupControlBox.appendChild(dvModalPopupHelpButton);
                    }

                    dvModalPopupHeader.appendChild(dvModalPopupControlBox);
                    dvPopUp.insertBefore(dvModalPopupHeader, dvPopUp.firstChild);

                    var maskHeight = $(document).height();
                    var maskWidth = $(document).width();
                    var h = dvPopUp.clientHeight;
                    var w = dvPopUp.clientWidth;


                    var t = ($(window).height() / 2 - h / 2) / 2;
                    var l = $(window).width() / 2 - w / 2;

                    $('#overlay').css({ 'width': '1637px', 'height': '1556px', backgroundColor: "black", 'position': "absolute", 'top': 0, 'left': 0, 'z-index': 999 });

                    $(dvPopUp).css({ 'top': t, 'left': l, 'z-index': 1000, 'position': "absolute", 'border': '4px solid #A9D0F5', 'background-color': 'white', 'padding': 0 });


                    isDraggable = typeof (isDraggable) == 'undefined' ? true : isDraggable;

                    $('#overlay').fadeTo("fast", 0.5);
                    moveToCenterdv(dvPopUp);

                    if (isDraggable)
                        $(dvPopUp).draggable();
                }
            }
            catch (e) { }

        }
        function moveToCenterdv(obj) {
            if (obj) {
                $(obj).css("position", "absolute"); $(obj).css("top", ($(window).height() - $(obj).height()) / 2 + $(window).scrollTop() + "px"); $(obj).css("left", ($(document).width() - $(obj).width()) / 2 + $(document).scrollLeft() + "px");
            }
            return true;
        }


        $(document).ready(function () {
            //  Confirm();
            var DocID = '<%=this.Request.QueryString["DocID"]%>'
            if (DocID == '') {
                $('[id$=dvLeftDocDetails]').toggle();
                $('[id$=dvFormReceived]').toggle();
                $("#txtFromDate").attr("disabled", "disabled");


            }
        });
        function Confirm() {

            $('.click-nav > ul').toggleClass('no-js js');
            $('.click-nav .js ul').hide();
            $('.click-nav .js').click(function (e) {
                $('.click-nav .js ul').slideToggle();
                $('.clicker').toggleClass('active');
                e.stopPropagation();
            });
            $(document).click(function () {
                if ($('.click-nav .js ul').is(':visible')) {
                    $('.click-nav .js ul', this).slideUp();
                    $('.clicker').removeClass('active');
                }

            });
            if (document.getElementById('btnReceived') != null && document.getElementById('btnPendingApp') != null) {
                if (document.getElementById('btnReceived').disabled == true || document.getElementById('btnPendingApp').disabled == true) {

                    $("tr.alt").addClass("Completed");

                    $('tr.alt').each(function () {


                        if ($('td.status:contains("Approved")', this).length) {
                            $(this).removeClass('Completed');
                            $(this).addClass('Approved');
                        }
                    });
                }
                else {

                    $("tr.alt").addClass("Due");

                    $('tr.alt').each(function () {
                        if ($('td.Overdue:contains("OverDue")', this).length) {
                            $(this).removeClass('Due');
                            $(this).addClass('OverDue');
                        }
                    });
                }
            }
        }

        function GridColour() {

            if (document.getElementById('btnReceived') != null && document.getElementById('btnPendingApp') != null) {
                if (document.getElementById('btnReceived').disabled == true || document.getElementById('btnPendingApp').disabled == true) {

                    $("tr.alt").addClass("Completed");

                    $('tr.alt').each(function () {


                        if ($('td.status:contains("Approved")', this).length) {
                            $(this).removeClass('Completed');
                            $(this).addClass('Approved');
                        }
                    });
                }
                else {

                    $("tr.alt").addClass("Due");

                    $('tr.alt').each(function () {
                        if ($('td.Overdue:contains("OverDue")', this).length) {
                            $(this).removeClass('Due');
                            $(this).addClass('OverDue');
                        }
                    });
                }
            }
        }


        function SetDocID(DocID, LogFileID, index) {

            document.getElementById('dvDocDetails').style.display = 'block';
            $('[id$=hdnFrmRecDocID]').val(DocID);
            $('[id$=hdnFrmRecDocName]').val(LogFileID);

        }

        // Added by Anjali . To show and hide div conditionally depends on schedule status.
        function Show_hideDiv(Status) {
            if ((Status == 'Un-assigned') || (Status == 'Re-schedule')) {
                document.getElementById('divReschedule_info').style.display = 'block';
                document.getElementById('dvDocDetails').style.display = 'none'
            }
            else {
                document.getElementById('dvDocDetails').style.display = 'block';
                document.getElementById('divReschedule_info').style.display = 'none';
            }
        }



        function GetFormsReceived(period) {
            document.getElementById('dvDocDetails').style.display = 'none';
            if (document.getElementById('btnReceived').disabled == true || document.getElementById('btnPendingApp').disabled == true) {
                $('[id$=hdnPeriodRec]').val(period);
            }
            else {
                $('[id$=hdnPeriodDue]').val(period);
            }
            __doPostBack("<%=btnGetFormsReceived.UniqueID %>", "onclick");

        }
        function Call_onDocumentClick(VesselID, ScheduleID, StatusID, OfficeID, index) {

            $('[id$=hdnVSL]').val(VesselID);
            $('[id$=hdnSchID]').val(ScheduleID);
            $('[id$=hdnStaID]').val(StatusID);
            $('[id$=hdnOffID]').val(OfficeID);
            $('[id$=hdnIndex]').val(index);
            __doPostBack("<%=btnGetFileDtl.UniqueID %>", "onclick")
        }
        function UpdatePage() {

            //  alert("TEst");

            hideModal("dvScheduleInsp");
            __doPostBack("<%=btnRefresh.UniqueID %>", "onclick");

        }
        function onAddFollowUp() {
            $("#dvInsRemark").prop('title', 'Add Followup');
            document.getElementById('dvInsRemark').style.display = 'inline';
            showModal('dvInsRemark', true);

            return false;
        }
        function onEditForm() {
            document.getElementById('dvEditForm').style.display = 'inline';

            showModal('dvEditForm', true);
            $("#dvEditForm").prop('title', 'Edit Forms');
            //            document.getElementById('dvEditForm').title = "Edit Forms";
            var textinputs = document.querySelectorAll('input[type=checkbox]');
            for (var i = 0; i < textinputs.length; i++) {
                if (textinputs[i].id.indexOf('trvFile') > -1) {
                    //textinputs[i].onchange = TreeOnCheckchange();
                    textinputs[i].addEventListener("change", TreeOnCheckchange);
                }
            }
            return false;
        }

        function TreeOnCheckchange(evt) {
            var flag = false;
            var textinputs = document.querySelectorAll('input[type=checkbox]');
            for (var i = 0; i < textinputs.length; i++) {
                if (textinputs[i].id.indexOf('trvFile') > -1) {

                    if (textinputs[i].checked == true) {
                        if (textinputs[i].id == evt.currentTarget.id) {
                            flag = true;
                            //break;
                        }
                        else {
                            document.getElementById(textinputs[i].id).checked = false;
                        }
                    }
                }
            }

            if (flag == true) {
                document.getElementById(evt.currentTarget.id).checked = true;
            }

            var treeViewData = window["<%=trvFile.ClientID%>" + "_Data"];
            if (treeViewData.selectedNodeID.value != "") {
                var selectedNode = document.getElementById(treeViewData.selectedNodeID.value);
                var value = selectedNode.href.substring(selectedNode.href.indexOf(",") + 3, selectedNode.href.length - 2);
                var text = selectedNode.innerHTML;
                //alert("Text: " + text + "\r\n" + "Value: " + value);
            } else {
                //alert("No node selected.")
            }
        }

        function onAddAttachment() {
            $("#dvPopupAddAttachment").prop('title', 'Add Attachments');

            document.getElementById('dvPopupAddAttachment').style.display = 'inline';
            showModal('dvPopupAddAttachment', true, fn_OnClose);

            return false;
        }

        function OpenDocApproval(url) {
            var docId = '<%=GetDocID() %>';
            var title = "";
            url = url + docId;



            document.getElementById('dvFileApproval').style.display = 'inline';
            document.getElementById('dvFileApproval').title = 'Approval Configuration';

            document.getElementById("IfrmFileApproval").src = url;

            //  window.open(url);
            $("#dvFileApproval").prop('title', 'Approval Configuration');
            showModal('dvFileApproval', true);

            return false;
        }
        function OpenDoc(url) {
            var docId = '<%=GetDocID() %>';
            var title = "";
            url = url + docId;

            if (url.indexOf("CheckOutForm") >= 0) {
                title = "Document CheckOut"
                document.getElementById('dvCheckInOut').title = title;
                document.getElementById('dvCheckInOut').style.display = 'inline';

                // url.cont
                showModal('dvCheckInOut', true);
            }
            if (url.indexOf("CheckInForm") >= 0) {
                title = "Document CheckIn";
                document.getElementById('dvCheckInOut').title = title;
                document.getElementById('dvCheckInOut').style.display = 'inline';

                //url.cont
                showModal('dvCheckInOut', true);
            }

            document.getElementById("ifrmDocCheckInOut").src = url;

            return false;
        }
        function OpenCheckOutDoc(url) {

            var docId = '<%=GetDocID() %>';
            url = url + docId;
            var checkStatus = confirm("You are about to Check Out the document.\n\nPlease save the document to your local system,edit and Check In from the same location.\n\nDo you want to continue?");
            if (checkStatus) {
                window.location.href = url;
            }
            return false;
        }
        function ViewHistory() {

            var docId = '<%=GetDocID() %>';

            showModal('dvHistory', true, closeHistory);
            document.getElementById("dvHistory").title = "Version History";

            return false;
        }
        function ViewSchApprovalHistory() {
            showModal('dvAppHistory', true, closeAppHistory);
            document.getElementById("dvAppHistory").title = "Approval History";

            return false;
        }

        function HideDiv() {

        }


        function closeHistory() {
            hideModal('dvHistory');
            return false;
        }

        function closeAppHistory() {

            document.getElementById("dvAppHistory").style.display = 'none';
            return false;
        }

        function Async_getHistory() {
            var docId = '<%=GetDocID() %>';
            var url = "docHistory.aspx?FileID=" + docId;
            var params = "";

            obj = new AsyncResponse(url, params, response_getHistory);
            obj.getResponse();
            return false;
        }
        function response_getHistory(retval) {
            var ar, arS;

            if (retval.indexOf('Working') >= 0) { return; }
            try {

                retval = clearXMLTags(retval);

                if (document.getElementById("dvDocHistory") != null) {
                    document.getElementById("dvDocHistory").innerHTML = retval;
                }
            }
            catch (ex) { alert(ex.message) }
        }

        function clearXMLTags(retval) {
            try {
                retval = retval.replace('<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">', '');
            }
            catch (ex) { }
            return retval;
        }


        function DocSchedule() {
            if (document.getElementById("divApprovalMessage") != null) {
                alert('Document is still not approved.');
                return false;
            }
            else {
                document.getElementById('IframeScheduleInsp').src = 'FMSSchedule.aspx?DocId=' + '<%=GetDocID() %>' + '&SchID=-1';
                document.getElementById('dvScheduleInsp').title = 'Document Scheduling';
                showModaltest('dvScheduleInsp', true, null, null);
                return false;

            }
        }
        function DocScheduleInfo(SchID) {
            if (document.getElementById("divApprovalMessage") != null) {
                alert('Document is still not approved.');
                return false;
            }
            else {
                document.getElementById('IframeScheduleInsp').src = 'FMSSchedule.aspx?DocId=' + '<%=GetDocID() %>' + '&SchID=' + SchID;
                document.getElementById('dvScheduleInsp').title = 'Document Scheduling';
                showModaltest('dvScheduleInsp', true, null, null);
                return false;

            }
        }
        function RefreshPage() {
            window.location.reload();
        }

        function fn_OnClose() {
            $('[id$=btnLoadFiles]').trigger('click');

            return false;

        }
        function SetSelectedvalue(listItem) {
            document.getElementById('hdnchekedUser').value = listItem.parentNode.childNodes[1].innerText;
        }
        function HighlightRow(val) {

        }

        function searchKeyPress(e) {
            // look for window.event in case event isn't passed in
            if (e.keyCode == 13) {
                document.getElementById('btnMainsearch').click();
                return false;
            }
            return true;
        }
        function searchKeyPressDate(e) {
            // look for window.event in case event isn't passed in
            if (e.keyCode == 13) {
                document.getElementById('btnFilter').click();
                return false;
            }
            return true;
        }
        function GetSelectedNode() {
            var treeViewData = window["<%=trvFile.ClientID%>" + "_Data"];
            if (treeViewData.selectedNodeID.value != "") {
                var selectedNode = document.getElementById(treeViewData.selectedNodeID.value);
                var value = selectedNode.href.substring(selectedNode.href.indexOf(",") + 3, selectedNode.href.length - 2);
                var text = selectedNode.innerHTML;

                var n = value.indexOf("\\");
                if (specialcharecter() == false) {
                    return false;
                }
                if (n > 0) {
                    var res = value.split("\\");
                    //hdnChangedParentID
                    document.getElementById($('[id$=hdnChangedParentID]').attr('id')).value = res[res.length - 1];
                    //alert(res[res.length - 1]);
                    return true;
                }
                else {
                    document.getElementById($('[id$=hdnChangedParentID]').attr('id')).value = value;
                    //alert(value);                    
                    return true;

                }

            } else {
                alert("No node selected.");
                return false;
            }

        }

        function SetEnd(TB) {

            var searchInput = $("#txtMainSearch");

            searchInput
            .putCursorAtEnd() // should be chainable
            .on("focus", function () { // could be on any event
                searchInput.putCursorAtEnd()
            });
        }

        jQuery.fn.putCursorAtEnd = function () {

            return this.each(function () {

                // Cache references
                var $el = $(this),
        el = this;

                // Only focus if input isn't already
                if (!$el.is(":focus")) {
                    $el.focus();
                }

                // If this function exists... (IE 9+)
                if (el.setSelectionRange) {

                    // Double the length because Opera is inconsistent about whether a carriage return is one character or two.
                    var len = $el.val().length * 2;

                    // Timeout seems to be required for Blink
                    setTimeout(function () {
                        el.setSelectionRange(len, len);
                    }, 1);

                } else {

                    // As a fallback, replace the contents with itself
                    // Doesn't work in Chrome, but Chrome supports setSelectionRange
                    $el.val($el.val());

                }

                // Scroll to the bottom, in case we're in a tall textarea
                // (Necessary for Firefox and Chrome)
                this.scrollTop = 999999;

            });

        };

        function ValidationOnDate() {
            if (document.getElementById("txtFromDate").value != "") {
                var strFromDate = document.getElementById("txtFromDate").value;
                if (strFromDate != "") {
                    var currDate = new Date();
                    var strCurrentDt = currDate.format("dd-MM-yyyy");
                    var dt1 = parseInt(strCurrentDt.substring(0, 2), 10);
                    var mon1 = parseInt(strCurrentDt.substring(3, 5), 10);
                    var yr1 = parseInt(strCurrentDt.substring(6, 10), 10);

                    var dt2 = parseInt(strFromDate.substring(0, 2), 10);
                    var mon2 = parseInt(strFromDate.substring(3, 5), 10);
                    var yr2 = parseInt(strFromDate.substring(6, 10), 10);

                    var CurrentDt = new Date(yr1, mon1, dt1);
                    var FromDate = new Date(yr2, mon2, dt2);
                    if (FromDate == 'Invalid Date') {
                        alert('Invalid From Date.');
                        return false;
                    }

                }


            }
            if (document.getElementById("txtTillDate").value != "") {
                var strToDate = document.getElementById("txtTillDate").value;
                if (strToDate != "") {

                    var currDate = new Date();
                    var strCurrentDt = currDate.format("dd-MM-yyyy");
                    var dt1 = parseInt(strCurrentDt.substring(0, 2), 10);
                    var mon1 = parseInt(strCurrentDt.substring(3, 5), 10);
                    var yr1 = parseInt(strCurrentDt.substring(6, 10), 10);

                    var dt2 = parseInt(strToDate.substring(0, 2), 10);
                    var mon2 = parseInt(strToDate.substring(3, 5), 10);
                    var yr2 = parseInt(strToDate.substring(6, 10), 10);

                    var CurrentDt = new Date(yr1, mon1, dt1);
                    var ToDate = new Date(yr2, mon2, dt2);

                    if (ToDate == 'Invalid Date') {
                        alert('Invalid Till Date.');
                        return false;
                    }

                }
            }
            return true;
        }

        //This is validation function to restrict special characters on save
        function specialcharecter() {

            if (document.getElementById($('[id$=txtFormName]').attr('id')) != null) {
                var iChars = "!`#$%^&*()+=[]\\\';,/{}|\":<>~_";
                var data = document.getElementById($('[id$=txtFormName]').attr('id')).value;
                var fullPath = document.getElementById($('[id$=txtFormName]').attr('id')).value;
                if (fullPath) {
                    var startIndex = (fullPath.indexOf('\\') >= 0 ? fullPath.lastIndexOf('\\') : fullPath.lastIndexOf('/'));
                    var filename = fullPath.substring(startIndex);
                    if (filename.indexOf('\\') === 0 || filename.indexOf('/') === 0) {
                        filename = filename.substring(1);
                    }
                }
                for (var i = 0; i < filename.length; i++) {
                    if (iChars.indexOf(filename.charAt(i)) != -1) {
                        alert("File name with special characters is not allowed.");

                        return false;
                    }
                }
            }
            return true;
        }


        ///javascript function use to clear src of image control if there is an Chief engineer image not found.///
        function HideCEImage() {
            var ImgCE = document.getElementById('frmVoyage_ImgCE');
            ImgCE.src = '';
        }

        ///javascript function use to clear src of image control if there is an master image not found.///
        function HideMasterImage() {
            var ImgCE = document.getElementById('frmVoyage_Img2E');
            ImgCE.src = '';
        }

        //To check file exists or not.To avoid page refresh if file not found.
        function IsFileExists(filepath) {
            var IsFile = false;
            var applicationPath = '<%=ConfigurationManager.AppSettings["APP_NAME"]%>';
            var d = new Date();
            var timespan = d.getTime();
            if (filepath == 0)
                var url = window.location.protocol + "//" + window.location.host + window.location.pathname + "?MethodName=FileExists&FileName=" + $("#hdnFilePath").val() + "&t=" + timespan;
            else
                var url = window.location.protocol + "//" + window.location.host + window.location.pathname + "?MethodName=FileExists&FileName=" + $("#hdnDocPath").val() + "&t=" + timespan;


            $.ajax({
                url: url,
                contentType: "application/json; charset=utf-8",
                type: 'GET',
                async: false,
                success: function (response) {
                    if (response == "1") {
                        IsFile = true;
                    }
                }
            });

            if (IsFile) {
                return true;
            }
            else {
                alert("File not found.");
                return false;
            }
        }


    </script>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdateProgress ID="upUpdateProgress" runat="server">
        <ProgressTemplate>
            <div id="blur-on-updateprogress">
                &nbsp;</div>
            <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
                color: black">
                <img src="../Images/loaderbar.gif" alt="Please Wait" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <div id="divMessage" align="center">
        <asp:Label ID="dvMessage" runat="server" ForeColor="Blue" Font-Size="Medium"></asp:Label>
    </div>
    <div id="dvMain" runat="server">
        <div id="dvFrame1" style="padding: 1px;">
            <div id="dvHistory" title="Version History" style="display: none; border: 1px solid inset;
                background-color: #efddef; text-align: left; height: 400px;">
                <div id="dvDocHistory" style="width: 100%; overflow-y: scroll; height: 380px;">
                    <asp:Repeater ID="dtrOppGrid" runat="server">
                        <HeaderTemplate>
                            <table cellspacing="0" cellpadding="4" border="0" id="dvOperations" style="color: #333333;
                                width: 100%; border: 1px solid gray;">
                                <tr align="left" style="color: black; background-color: #CCCCCC; border-top: 1px solid gray;
                                    font-size: 11px; font-weight: bold;">
                                    <td scope="col" style="border-right: 1px solid #999999; border-left: 1px solid #999999;">
                                        Date
                                    </td>
                                    <td scope="col" style="border-right: 1px solid #999999;">
                                        Action
                                    </td>
                                    <td scope="col" style="border-right: 1px solid #999999;">
                                        User
                                    </td>
                                    <td scope="col" style="border-right: 1px solid #999999;">
                                        Version
                                    </td>
                                    <td scope="col" style="border-right: 1px solid #999999;">
                                        Download and View
                                    </td>
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr style="background-color: white; border-right: 1px solid gray; border-left: 1px solid gray;
                                border-bottom: 1px solid gray">
                                <td style="border-right: 1px solid #999999; border-left: 1px solid #999999;">
                                    <%# ConvertDateToString(Convert.ToString( Eval("Operation_Date")),"dd-MMM-yy HH:mm")%>
                                </td>
                                <td style="border-right: 1px solid #999999;">
                                    <%# Eval("Operation_Type") %>
                                </td>
                                <td style="border-right: 1px solid #999999;">
                                    <%# Eval("First_Name")%>
                                </td>
                                <td style="border-right: 1px solid #999999;" align="center">
                                    <%# Eval("Version") %>
                                </td>
                                <td style="border-right: 1px solid #999999;">
                                    <a href="FMSGetLatest.aspx?DocVer=<%# Eval("DocID")%>-<%# Eval("Version") %>">
                                        <%# Convert.ToString(Eval("LogFileID"))%></a>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </div>
        <div id="dvAppHistoryFrame" style="padding: 1px;">
            <div id="dvAppHistory" title="Approval History" class="historyBox" style="display: none;
                border: 1px solid inset; background-color: #efddef; padding: 2px; text-align: left;
                height: 400px; width: 800px;">
                <div id="dvSchAppHistory" style="width: 100%; overflow-y: scroll; height: 380px;">
                    <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="grdSchAppHistory" runat="server" EmptyDataText="NO RECORDS FOUND"
                                AutoGenerateColumns="False" DataKeyNames="FileID" CellPadding="1" CellSpacing="0"
                                Width="100%" GridLines="both" AllowSorting="true" CssClass="gridmain-css">
                                <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                <RowStyle CssClass="RowStyle-css" />
                                <HeaderStyle CssClass="HeaderStyle-css" Height="25px" />
                                <PagerStyle CssClass="PMSPagerStyle-css" />
                                <SelectedRowStyle BackColor="#FFFFCC" />
                                <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                <Columns>
                                    <asp:TemplateField HeaderText="File Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFileName" runat="server" Text='<%#Eval("File_Name")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="100px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Version">
                                        <ItemTemplate>
                                            <asp:Label ID="lblVersion" runat="server" Text='<%#Eval("Version")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="30px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Vessel">
                                        <ItemTemplate>
                                            <asp:Label ID="lblVesselName" runat="server" Text='<%#Eval("Vessel_Name")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="100px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Schedule Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSchDate" runat="server" Text='<%#Eval("Schedule_Date")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="80px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Level">
                                        <ItemTemplate>
                                            <asp:Label ID="lblLevel" runat="server" Text='<%#Eval("Level")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="30px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Status">
                                        <ItemTemplate>
                                            <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("Status")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="180px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Approval/Rework Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblARDate" runat="server" Text='<%#Eval("TDate")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="140px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
        <div id="dvFrame" style="width: 100%;">
            <table width="100%">
                <tr>
                    <td style="width: 50%; vertical-align: top; border: 1px solid black; height: 780px;">
                        <div id="dvLeftDocDetails" style="width: 100%; vertical-align: top; height: 780px;
                            overflow-y: scroll;">
                            <table width="100%">
                                <tr>
                                    <td>
                                        <div style="overflow: hidden; margin-bottom: 1px; border: 1px solid outset;" id="Div2">
                                            <asp:UpdatePanel ID="UpdatePanel18" runat="server">
                                                <ContentTemplate>
                                                    <table id="docProperties" style="width: 100%; border: 1px solid black;">
                                                        <tr>
                                                            <td>
                                                                <asp:Button ID="ImgBtnDocSchedule" runat="server" Text="Schedule" CssClass="awesome"
                                                                    OnClientClick="DocSchedule();return false;" alt="File Schedule" Visible="False" />
                                                                <asp:Button ID="ImgSetApproval" runat="server" Text="Approval Configuration" CssClass="awesome"
                                                                    AlternateText="Office Approval" OnClientClick="return OpenDocApproval('FMSApprovarLevelUserConfig.aspx?FileID=');"
                                                                    Visible="False" />
                                                                <asp:Label ID="lblArchivedTitle" runat="server" CssClass="ArchivedLable"></asp:Label>
                                                            </td>
                                                            <td style="text-align: right;">
                                                                <asp:CheckBox ID="chkautoforwardMail" runat="server" Text="Auto Forward" AutoPostBack="true"
                                                                    Font-Size="12px" TextAlign="left" OnCheckedChanged="chkautoforwardMail_CheckedChanged"
                                                                    Visible="false" />&nbsp;
                                                                <%if (GetStatus() == "CHECKED OUT")
                                                                  {
                                                                      if (Session["USERID"].ToString() == CheckOutUserID())
                                                                      {
                                                                %>
                                                                <asp:ImageButton ID="ImgSubCheckIn" runat="server" ImageUrl="~/Images/checkin.png"
                                                                    OnClientClick="return OpenDoc('FMSCheckInForm.aspx?FileID=');" ToolTip="Check In" />
                                                                <%
                                                                      }
                                                                  }
                                                                  else
                                                                  {
                                                                %>
                                                                <asp:ImageButton ID="ImgCheckOut" runat="server" ImageUrl="~/Images/checkout.png"
                                                                    OnClientClick="return OpenCheckOutDoc('FMSCheckOutForm.aspx?FileID=');" ToolTip="Check Out"
                                                                    Width="22px" />
                                                                <asp:ImageButton ID="ImgCheckIn" runat="server" ImageUrl="~/Images/checkin.png" OnClientClick="return OpenDoc('FMSCheckInForm.aspx?FileID=');"
                                                                    ToolTip="Check In" />
                                                                <%
                                                                  }
                                                                %>
                                                                <asp:ImageButton ID="ImgViewHistory" runat="server" ImageUrl="~/Images/history.png"
                                                                    OnClientClick="return ViewHistory();" ToolTip="View Version History" />
                                                                <asp:ImageButton ID="ImgSchAppHistory" runat="server" ImageUrl="~/Images/Schhistory.png"
                                                                    OnClientClick="return ViewSchApprovalHistory();" ToolTip="View Approval History" />
                                                                <asp:ImageButton ID="ImgGetLatest" runat="server" ImageUrl="~/Images/getLatest.png"
                                                                    OnClientClick="return OpenDoc('FMSGetLatest.aspx?FileID=');" ToolTip="Get Latest File" />
                                                                <asp:ImageButton ID="btnDelete" runat="server" AlternateText="Archive file" ImageUrl="~/Images/Delete-icon.png"
                                                                    OnClick="btnDeleteE_Click" Visible="false" ToolTip="Archive file" OnClientClick="return confirm('Archiving of a form will un-assign this form from all the vessels & forms which are in approval process for one or more vessels will continue till it ends. Are you sure wanted to Archive?')" />
                                                                <asp:ImageButton ID="btnUnarchive" runat="server" ImageUrl="~/Images/UnArchive.png"
                                                                    ToolTip="Un-archive file" OnClick="btnUnarchive_Click" Visible="false" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div style="margin-bottom: 1px; border: 1px solid outset;">
                                            <table style="width: 100%">
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblDocName" runat="server" Text="" Visible="false"></asp:Label>
                                                        <asp:Label ID="lblCurrentVersion" runat="server" Text="" Visible="false"></asp:Label>
                                                        <br />
                                                        <asp:Label ID="lblOppStatus" runat="server" Text="" Visible="false"></asp:Label>
                                                    </td>
                                                </tr>
                                                <%-- <tr>
                                                    <td>
                                                        <span style="color: #666">Revision :</span>
                                                    </td>
                                                </tr>--%>
                                                <tr>
                                                    <td>
                                                        <div id="dvDocHistory1" style="width: 100%;">
                                                            <table cellspacing="0" cellpadding="2" style="border: 1px solid gray; width: 100%;
                                                                font-size: 11px;">
                                                                <tr>
                                                                    <td style="width: 80px;">
                                                                        File Name:
                                                                    </td>
                                                                    <td>
                                                                        <asp:UpdatePanel ID="UpdatePanel22" runat="server">
                                                                            <ContentTemplate>
                                                                                <%--<asp:Label ID="lblFileName" runat="server" Font-Bold="True" ForeColor="#003366"></asp:Label>--%>
                                                                                <asp:HyperLink ID="lnkFileName1" runat="server" Target="_blank" Font-Bold="True"
                                                                                    ForeColor="#003366" Visible="false"></asp:HyperLink>
                                                                                <asp:LinkButton ID="lnkFileName" runat="server" Font-Bold="True" ForeColor="#003366"
                                                                                    OnClick="lnkFileName_Click" Visible="false"></asp:LinkButton>
                                                                                <asp:ImageButton ID="btnForce_Download" runat="server" ImageUrl="~/Images/forceDownload.png"
                                                                                    ToolTip="Force download" OnClick="btnForce_Download_Click" OnClientClick="return IsFileExists(0);" />
                                                                                <asp:HiddenField ID="hdnFilePath" runat="server" />
                                                                            </ContentTemplate>
                                                                            <Triggers>
                                                                                <asp:PostBackTrigger ControlID="lnkFileName" />
                                                                                <asp:PostBackTrigger ControlID="btnForce_Download" />
                                                                            </Triggers>
                                                                        </asp:UpdatePanel>
                                                                    </td>
                                                                    <td>
                                                                        Version:
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblLatestVersion" runat="server" Font-Bold="True" ForeColor="#003366">
                                                                        </asp:Label>
                                                                    </td>
                                                                    <td align="right">
                                                                        <asp:ImageButton ID="ImgEditForm" runat="server" ImageUrl="~/Images/edit-new.png"
                                                                            OnClientClick="return onEditForm();" ToolTip="Edit" Style="height: 16px" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        Form Type:
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblFormType" runat="server" Font-Bold="True" ForeColor="#003366"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        Department:
                                                                    </td>
                                                                    <td colspan="2">
                                                                        <asp:Label ID="lblDepartment" runat="server" Font-Bold="True" ForeColor="#003366"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        Created By:
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblCreatedBy" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                    <td class="style1">
                                                                        Last action:
                                                                    </td>
                                                                    <td colspan="2">
                                                                        <asp:Label ID="lblLastOperation" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        Creation Date:
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblCreationDate" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        Last action date:
                                                                    </td>
                                                                    <td colspan="2">
                                                                        <asp:Label ID="lblLastOperationDt" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        Remarks:
                                                                    </td>
                                                                    <td colspan="5" style="white-space: normal; word-wrap: break-word;">
                                                                        <asp:Label ID="lblRemark" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: left; vertical-align: top;">
                                        <asp:UpdatePanel ID="updRAForms" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblTRAF" runat="server" Text="Risk Assessment Forms:" Font-Bold="true"
                                                                Font-Size="11px"></asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <div style="margin-top: 2px; font-size: 12px;" id="dvRange" runat="server">
                                                                From:
                                                                <asp:TextBox ID="txtFromDate" runat="server" Width="120px" CssClass="txtInput" onkeypress="return searchKeyPressDate(event);"></asp:TextBox>&nbsp;
                                                                <tlk4:CalendarExtender ID="calFromDate" runat="server" TargetControlID="txtFromDate"
                                                                    Format="dd/MM/yyyy">
                                                                </tlk4:CalendarExtender>
                                                                Till:
                                                                <asp:TextBox ID="txtTillDate" runat="server" Width="120px" CssClass="txtInput" onkeypress="return searchKeyPressDate(event);"></asp:TextBox>
                                                                <tlk4:CalendarExtender ID="calTillDate" runat="server" TargetControlID="txtTillDate"
                                                                    Format="dd/MM/yyyy">
                                                                </tlk4:CalendarExtender>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td id="tdRAForms" colspan="2">
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Panel ID="pnlRAForms" runat="server">
                                                                            <asp:DataList ID="dlRAForms" runat="server" RepeatColumns="5" RepeatDirection="Vertical"
                                                                                EmptyDataText="NO RECORDS FOUND" RepeatLayout="Table" CellSpacing="2">
                                                                                <ItemTemplate>
                                                                                    <div style="background-color: #C3EBFF; border-radius: 2px; padding: 1px; border: 1px solid #ACC9C9">
                                                                                        <table>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <asp:HyperLink ID="hlRAForms" runat="server" Text='<%# Eval("Work_Category_Name")%>'
                                                                                                        Font-Names="Calibri" Target="_blank" NavigateUrl='<%# "~/JRA/Libraries/HazardTemplate.aspx?DocID="+Eval("Work_Categ_ID").ToString() %>'>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                                                    </asp:HyperLink>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </div>
                                                                                </ItemTemplate>
                                                                            </asp:DataList>
                                                                        </asp:Panel>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div id="dvVesselDocHistory" style="width: 100%;">
                                            <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                                                <ContentTemplate>
                                                    <table style="width: 100%;">
                                                        <tr>
                                                            <td style="width: 8%" align="right">
                                                                Fleet :&nbsp;
                                                            </td>
                                                            <td style="width: 20%" align="left">
                                                                <asp:DropDownList ID="ddlFleet" Width="98%" runat="server" OnSelectedIndexChanged="ddlFleet_SelectedIndexChanged"
                                                                    AutoPostBack="true">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td style="width: 8%" align="right">
                                                                Vessel :&nbsp;
                                                            </td>
                                                            <td style="width: 20%" align="left">
                                                                <asp:DropDownList ID="ddlVessel" Width="98%" runat="server">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td style="width: 8%" align="right">
                                                                Status :&nbsp;
                                                            </td>
                                                            <td style="width: 20%" align="left">
                                                                <asp:DropDownList ID="ddlStatus" Width="98%" runat="server">
                                                                    <%--  <asp:ListItem Text="ALL" Value="ALL" Selected="True"></asp:ListItem>
                                                                    <asp:ListItem Text="Pending" Value="PENDING"></asp:ListItem>
                                                                    <asp:ListItem Text="Completed" Value="COMPLETED"></asp:ListItem>
                                                                    <asp:ListItem Text="Rework" Value="REWORK"></asp:ListItem>
                                                                    <asp:ListItem Text="Approved" Value="APPROVED"></asp:ListItem>--%>
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td align="center" style="width: 5%">
                                                                <%--<asp:Button ID="btnSearch" runat="server" Text="Refresh" />--%>
                                                                <asp:ImageButton ID="btnFilter" runat="server" ImageUrl="~/Images/SearchButton.png"
                                                                    OnClick="btnFilter_Click" OnClientClick="return ValidationOnDate();" />
                                                            </td>
                                                            <td align="center" style="width: 5%">
                                                                <%--<asp:Button ID="btnRefresh" runat="server" Text="Refresh" OnClick="btnRefresh_Click1" />--%>
                                                                <asp:ImageButton ID="btnRefresh" runat="server" ImageUrl="~/Images/Refresh-icon.png"
                                                                    OnClick="btnRefresh_Click" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <div>
                                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                            <ContentTemplate>
                                                                <asp:GridView ID="gvVesselDocHistory" runat="server" EmptyDataText="NO RECORDS FOUND"
                                                                    AutoGenerateColumns="False" DataKeyNames="ID" CellPadding="1" CellSpacing="0"
                                                                    Width="100%" GridLines="both" AllowSorting="true" CssClass="gridmain-css">
                                                                    <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                                                    <RowStyle CssClass="RowStyle-css" />
                                                                    <HeaderStyle CssClass="HeaderStyle-css" Height="25px" />
                                                                    <PagerStyle CssClass="PMSPagerStyle-css" />
                                                                    <SelectedRowStyle BackColor="#FFFFCC" />
                                                                    <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Vessel">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblVesselName" runat="server" Text='<%#Eval("Vessel_Name")%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="120px" CssClass="PMSGridItemStyle-css">
                                                                            </ItemStyle>
                                                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Remarks">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblRemarks" runat="server" Text='<%#Eval("Schedule_Desc")%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="150px" CssClass="PMSGridItemStyle-css">
                                                                            </ItemStyle>
                                                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Due Date">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblSchDate" runat="server" Text='<%#Eval("Schedule_Date")%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="50px" CssClass="PMSGridItemStyle-css">
                                                                            </ItemStyle>
                                                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Completion Date">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblLastDone" runat="server" Text='<%#Eval("Completion_Date")%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="80px" CssClass="PMSGridItemStyle-css">
                                                                            </ItemStyle>
                                                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Status">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("Status")%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="80px" CssClass="PMSGridItemStyle-css">
                                                                            </ItemStyle>
                                                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Next Due Date">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblDue" runat="server" Text='<%# Eval("Next_Due_Date") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="80px" CssClass="PMSGridItemStyle-css">
                                                                            </ItemStyle>
                                                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="View">
                                                                            <ItemTemplate>
                                                                                <%--OnClientClick="javascript:document.getElementById('dvDocDetails').style.display='block';"--%>
                                                                                <asp:ImageButton ID="ImgAttach" runat="server" Text="View" OnCommand="onDocumentClick"
                                                                                    OnClientClick='<%# String.Format("javascript:return Show_hideDiv(\"{0}\")", Eval("Status").ToString()) %>'
                                                                                    CommandArgument='<%#Eval("[Vessel_ID]")+","+Eval("[Schedule_ID]")+","+Eval("[Office_ID]")+","+Eval("[ID]")+","+((GridViewRow) Container).RowIndex%>'
                                                                                    ForeColor="Black" ToolTip="View" ImageUrl="~/Images/Arrow2Right.png" Height="16px">
                                                                                </asp:ImageButton>
                                                                            </ItemTemplate>
                                                                            <ItemStyle Wrap="true" HorizontalAlign="Center" Width="50px" CssClass="PMSGridItemStyle-css">
                                                                            </ItemStyle>
                                                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                                <uc1:ucCustomPager ID="ucCustomPagerSch" runat="server" PageSize="10" OnBindDataItem="LoadDocScheduleGrid" />
                                                                <br />
                                                                <br />
                                                                <br />
                                                                <asp:HiddenField ID="HiddenFlag" runat="server" EnableViewState="False" />
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                    <br />
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div id="dvFormReceived" style="width: 100%; vertical-align: top; display: none;">
                            <asp:UpdatePanel ID="UpdatePanel19" runat="server">
                                <ContentTemplate>
                                    <div style="margin-right: 20px">
                                        <table align="right" cellpadding="1" cellspacing="0" style="border: 1px solid #e4e4e4;
                                            padding-left: 0px; margin-top: 10px" id="tblAppUserVessel" runat="server" width="97.5%">
                                            <tr>
                                                <td>
                                                    <table align="left">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblSearch" runat="server" Text="Search"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtMainSearch" runat="server" onkeypress="return searchKeyPress(event);"
                                                                    onfocus="SetEnd(this);"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:Button ID="btnMainsearch" runat="server" Text="Search" OnClick="btnMainsearch_Click"
                                                                    OnClientClick="SetEnd(this)" />
                                                            </td>
                                                            <td>
                                                                <asp:Button ID="btnClearSearch" runat="server" Text="Clear Filter" OnClick="btnClearSearch_Click" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td align="center" style="border: 1px solid #e4e4e4; background-color: #e4e4d4; font-weight: bold">
                                                    <asp:Label ID="lblUserVesselAssigment" runat="server" ClientIDMode="Static" Text="By Vessel assignment"></asp:Label>
                                                </td>
                                                <td align="center" style="border: 1px solid #e4e4e4;">
                                                    <asp:CheckBox ID="chkVesselAssign" RepeatDirection="Horizontal" AutoPostBack="true"
                                                        Checked="true" ClientIDMode="Static" runat="server" OnCheckedChanged="chkVesselAssign_CheckedChanged">
                                                        <%-- <asp:ListItem Text="" Value="1" Selected="True"></asp:ListItem>--%>
                                                    </asp:CheckBox>
                                                    <asp:HiddenField ID="hdncheckedVesselAssign" ClientIDMode="Static" runat="server" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div style="margin-left: 20px; margin-top: 58px; margin-bottom: 10px; margin-right: 20px;">
                            <asp:UpdatePanel ID="UpdatePanel24" runat="server">
                                <ContentTemplate>
                                    <table width="100%" align="left" cellpadding="1" cellspacing="0" style="border: 1px solid #e4e4e4;
                                        padding-left: 0px; margin-top: -10px;" id="tblSearch" runat="server">
                                        <tr style="border: 1px solid #e4e4e4;">
                                            <td align="right" style="width: 4%;">
                                                <asp:Label ID="lblFleet" runat="server" ClientIDMode="Static" Text="Fleet "></asp:Label>
                                            </td>
                                            <td align="left" style="width: 8%;">
                                                <asp:DropDownList ID="ddlFleetSearch" runat="server" OnSelectedIndexChanged="ddlFleetSearch_SelectedIndexChanged"
                                                    AutoPostBack="true" Width="55px">
                                                </asp:DropDownList>
                                            </td>
                                            <td align="right" style="width: 4%;">
                                                <asp:Label ID="lblVesselSearch" runat="server" ClientIDMode="Static" Text="Vessel">
                                                </asp:Label>
                                            </td>
                                            <td align="left" style="width: 10%;">
                                                <CustomFilter:ucfDropdown ID="ddlVesselSearch" runat="server" UseInHeader="false"
                                                    Width="130" Height="200" />
                                            </td>
                                            <td align="right" style="width: 8%;">
                                                <asp:Label ID="lblFormTypeSearch" runat="server" ClientIDMode="Static" Text="Form Type">
                                                </asp:Label>
                                            </td>
                                            <td align="left" style="width: 9%;">
                                                <asp:DropDownList ID="ddlFormTypeSearch" runat="server" Font-Size="12px" Width="100px">
                                                </asp:DropDownList>
                                            </td>
                                            <td align="right" style="width: 8%;">
                                                <asp:Label ID="lblFormStatusSearch" runat="server" ClientIDMode="Static" Text="Form Status">
                                                </asp:Label>
                                            </td>
                                            <td align="left" style="width: 10%;">
                                                <asp:DropDownList ID="ddlStatusSearch" runat="server" Font-Size="12px" Width="100px">
                                                </asp:DropDownList>
                                            </td>
                                            <td align="right" style="width: 8%;">
                                                <asp:Label ID="lblDepartmentSearch" runat="server" ClientIDMode="Static" Text="Department">
                                                </asp:Label>
                                            </td>
                                            <td align="left" style="width: 9%;">
                                                <asp:DropDownList ID="ddlDepartmentSearch" runat="server" Font-Size="12px" Width="100px">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div style="margin-left: 20px; margin-top: 81px; margin-bottom: 10px; margin-right: 20px">
                            <asp:UpdatePanel ID="UpdatePanel20" runat="server">
                                <ContentTemplate>
                                    <asp:Button ID="btnReceived" BorderStyle="None" Text="Received Forms" runat="server"
                                        OnClick="FormsReceived" />
                                    <asp:Button ID="btnPending" BorderStyle="None" CssClass="FormSelected" Text="Due/Overdue/Rework Forms"
                                        runat="server" OnClick="FormsPending" />
                                    <asp:Button ID="btnPendingApp" BorderStyle="None" Text="Forms Pending For Approval"
                                        runat="server" OnClick="FormsPendingApprover" />
                                    <div style="font-weight: bold; background-color: Silver; width: 100%; text-align: center;
                                        max-height: 25px; padding-top: 6px">
                                        <table width="100%">
                                            <tr>
                                                <td align="right" style="width: 40%; padding-right: 5px; vertical-align: top">
                                                    <asp:Label ID="lblFormsStatus" runat="server" Text="Label"></asp:Label>
                                                </td>
                                                <td style="width: 20px; padding-top: 0px; vertical-align: top">
                                                    <div class="click-nav" style="vertical-align: top; margin-top: -14px">
                                                        <ul class="no-js" style="vertical-align: top">
                                                            <li><a id="lnkPeriod" href="#" class="clicker" runat="server" style="vertical-align: top">
                                                            </a>
                                                                <ul>
                                                                    <li><a href="#" onclick="GetFormsReceived('7');">7</a></li>
                                                                    <li><a href="#" onclick="GetFormsReceived('15');">15 </a></li>
                                                                    <li><a href="#" onclick="GetFormsReceived('30');">30 </a></li>
                                                                    <li><a href="#" onclick="GetFormsReceived('60');">60 </a></li>
                                                                    <li><a href="#" onclick="GetFormsReceived('90');">90 </a></li>
                                                                </ul>
                                                            </li>
                                                        </ul>
                                                    </div>
                                                </td>
                                                <td align="left" style="padding-left: 5px; padding-top: 0px; vertical-align: top">
                                                    <span>Days</span>
                                                    <asp:HiddenField ID="hdnPeriodRec" runat="server" />
                                                    <asp:HiddenField ID="hdnPeriodDue" runat="server" />
                                                    <asp:HiddenField ID="hdnVSL" runat="server" />
                                                    <asp:HiddenField ID="hdnSchID" runat="server" />
                                                    <asp:HiddenField ID="hdnStaID" runat="server" />
                                                    <asp:HiddenField ID="hdnOffID" runat="server" />
                                                    <asp:HiddenField ID="hdnIndex" runat="server" />
                                                    <asp:Button ID="btnGetFormsReceived" runat="server" CssClass="btnGetFormsReceived"
                                                        OnClick="btnGetFormsReceived_Click" />
                                                    <asp:Button ID="btnGetFileDtl" runat="server" CssClass="btnGetFormsReceived" OnClick="btnGetFileDtl_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                        <table width="100%" align="left" cellpadding="1" cellspacing="0" style="border: 1px solid #e4e4e4;
                                            padding-left: 0px; margin-top: -10px" id="tblApp" runat="server" visible="false">
                                            <tr style="border: 1px solid #e4e4e4;">
                                                <td align="center" style="border: 1px solid #e4e4e4; background-color: #e4e4d4">
                                                    <asp:Label ID="lblhdrStatus" runat="server" ClientIDMode="Static" Text="Status :"></asp:Label>
                                                </td>
                                                <td align="left" style="border: 1px solid #e4e4e4;">
                                                    <asp:RadioButtonList ID="rblPending" RepeatDirection="Horizontal" runat="server"
                                                        AutoPostBack="true" ClientIDMode="Static" OnSelectedIndexChanged="rblPending_SelectedIndexChanged">
                                                        <asp:ListItem Text="Pending For Approval" Value="0" Selected="True"></asp:ListItem>
                                                        <asp:ListItem Text="Approved" Value="1"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                                <td align="center" style="border: 1px solid #e4e4e4; background-color: #e4e4d4">
                                                    <asp:Label ID="lblMyApprovals" runat="server" ClientIDMode="Static" Text="My Approvals :"></asp:Label>
                                                </td>
                                                <td align="center" style="border: 1px solid #e4e4e4;">
                                                    <asp:CheckBoxList ID="chkUser" RepeatDirection="Horizontal" AutoPostBack="true" ClientIDMode="Static"
                                                        runat="server" OnSelectedIndexChanged="chkUser_SelectedIndexChanged">
                                                        <asp:ListItem Text="" Value="1" Selected="True"></asp:ListItem>
                                                    </asp:CheckBoxList>
                                                    <asp:HiddenField ID="hdnchekedUser" ClientIDMode="Static" runat="server" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <asp:HiddenField ID="hdnFrmRecDocID" runat="server" />
                                    <asp:HiddenField ID="hdnFrmRecDocName" runat="server" />
                                    <div style="margin-left: 20px; margin-bottom: 10px; margin-right: 20px; overflow-y: scroll;
                                        width: 97.5%; height: 630px;">
                                        <asp:Repeater ID="MainRepeater" runat="server">
                                            <HeaderTemplate>
                                                <table width="100%">
                                                    <tr>
                                                        <th>
                                                        </th>
                                                        <th>
                                                        </th>
                                                    </tr>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td style="vertical-align: top; color: #003366; width: 5%">
                                                        <b>
                                                            <asp:Label ID="Label1" runat="server" Text='<%# (Mode == "Received") || (Mode == "Pending") ?Eval("Completion_Date") :Eval("Schedule_Date")%>'></asp:Label>
                                                        </b>
                                                    </td>
                                                    <td style="padding-top: 20px; vertical-align: bottom">
                                                        <asp:Repeater ID="cdcatalog" runat="server" OnItemDataBound="cdcatalog_ItemDataBound"
                                                            DataSource='<%# ((System.Data.DataRowView) Container.DataItem).Row.GetChildRows("EventMembers") %>'>
                                                            <HeaderTemplate>
                                                                <table border="0" width="100%">
                                                                    <tr>
                                                                        <th align="left" style="color: #003366; width: 14%">
                                                                            VSL&nbsp;
                                                                        </th>
                                                                        <th align="left" style="color: #003366; width: 45%">
                                                                            Form Name&nbsp;
                                                                        </th>
                                                                        <th align="left" style="color: #003366; width: 14%">
                                                                            Due Date&nbsp;
                                                                        </th>
                                                                        <th align="left" style="color: #003366; width: 14%">
                                                                            Completion Date&nbsp;
                                                                        </th>
                                                                        <th align="left" style="color: #003366; width: 10%">
                                                                            Status&nbsp;
                                                                        </th>
                                                                        <th align="left" style="color: #003366; width: 45%" id="tdUser" runat="server" visible="false">
                                                                            Approver&nbsp;
                                                                        </th>
                                                                        <th align="left" style="color: #003366; width: 5%">
                                                                            View&nbsp;
                                                                        </th>
                                                                    </tr>
                                                                    <tr>
                                                                        <th colspan="7">
                                                                            <hr>
                                                                        </th>
                                                                    </tr>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <tr id="itemRow" runat="server" class="alt" style="cursor: pointer" onclick='<%# "SetDocID(&#39;"+ (((System.Data.DataRow) Container.DataItem)["DocID"]) +"&#39;,&#39;"+ (((System.Data.DataRow) Container.DataItem)["LogFileID"]) +"&#39;,&#39;"+ (((System.Data.DataRow) Container.DataItem)["Index"]) +"&#39;);Call_onDocumentClick(&#39;"+(((System.Data.DataRow) Container.DataItem)["Vessel_ID"])+"&#39;,&#39;"+(((System.Data.DataRow) Container.DataItem)["Schedule_ID"])+"&#39;,&#39;"+(((System.Data.DataRow) Container.DataItem)["Office_ID"])+"&#39;,&#39;"+(((System.Data.DataRow) Container.DataItem)["ID"])+"&#39;,&#39;"+(((System.Data.DataRow) Container.DataItem)["Index"])+"&#39;);" %>'>
                                                                    <td>
                                                                        <%# ((System.Data.DataRow)Container.DataItem)["Vessel_Name"]%>&nbsp;
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblID" runat="server" Text='<%# ((System.Data.DataRow)Container.DataItem)["LogFileID"] %>'
                                                                            CssClass="label"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <%# ((System.Data.DataRow)Container.DataItem)["Schedule_Date"]%>&nbsp;
                                                                    </td>
                                                                    <td>
                                                                        <%# ((System.Data.DataRow)Container.DataItem)["Completion_Date"]%>&nbsp;
                                                                    </td>
                                                                    <td class="status">
                                                                        <%# ((System.Data.DataRow)Container.DataItem)["Status"]%>&nbsp;
                                                                    </td>
                                                                    <td id="tdApp" runat="server" visible="false">
                                                                        <asp:Label ID="lblrApprover" Visible="false" runat="server" Text='<%# ((System.Data.DataRow)Container.DataItem)["ApproverName"]%>'></asp:Label>
                                                                        &nbsp;
                                                                    </td>
                                                                    <td class="Overdue">
                                                                        <asp:ImageButton runat="server" Text="Attachment" OnClientClick='<%# "SetDocID(&#39;"+ (((System.Data.DataRow) Container.DataItem)["DocID"]) +"&#39;,&#39;"+ (((System.Data.DataRow) Container.DataItem)["LogFileID"]) +"&#39;,&#39;"+ (((System.Data.DataRow) Container.DataItem)["Index"]) +"&#39;);" %>'
                                                                            OnCommand="onDocumentClick" CommandArgument='<%#(((System.Data.DataRow) Container.DataItem)["Vessel_ID"])+","+(((System.Data.DataRow) Container.DataItem)["Schedule_ID"])+","+(((System.Data.DataRow) Container.DataItem)["Office_ID"])+","+(((System.Data.DataRow) Container.DataItem)["ID"])+","+(((System.Data.DataRow) Container.DataItem)["Index"])%>'
                                                                            ForeColor="Black" ToolTip="View" ImageUrl="~/Images/Arrow2Right.png" Height="16px">
                                                                        </asp:ImageButton>
                                                                        <asp:Label ID="Label1" runat="server" Text='<%# ((System.Data.DataRow)Container.DataItem)["OverDue"] %>'
                                                                            CssClass="btnGetFormsReceived"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                            <SeparatorTemplate>
                                                                <tr>
                                                                    <td colspan="7">
                                                                        <hr style="border-color: #F3F3F7; border-width: 0.1px">
                                                                    </td>
                                                                </tr>
                                                            </SeparatorTemplate>
                                                            <FooterTemplate>
                                                                </table>
                                                            </FooterTemplate>
                                                        </asp:Repeater>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <SeparatorTemplate>
                                                <tr>
                                                    <td colspan="7">
                                                        <br />
                                                    </td>
                                                </tr>
                                            </SeparatorTemplate>
                                            <FooterTemplate>
                                                </table>
                                            </FooterTemplate>
                                        </asp:Repeater>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </td>
                    <td style="width: 50%; vertical-align: top; border: 1px solid black; height: 780px;">
                        <div id="dvDocDetails" style="display: none; vertical-align: top; height: 780px;
                            overflow-y: scroll;" class="2">
                            <table width="100%" align="top">
                                <tr>
                                    <td>
                                        <asp:UpdatePanel ID="UpdatePanel17" runat="server">
                                            <ContentTemplate>
                                                <asp:Label ID="lblApl" runat="server" Text="Approval Status" Font-Size="14px"></asp:Label>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:UpdatePanel ID="UpdatePanel16" runat="server">
                                            <ContentTemplate>
                                                <asp:DataList ID="dlApprovalLevel" runat="server" RepeatLayout="Table" RepeatColumns="5"
                                                    RepeatDirection="Horizontal" OnItemDataBound="dlApprovalLevel_ItemDataBound">
                                                    <ItemTemplate>
                                                        <table cellspacing="1" width="200px" cellpadding="1" style="border: solid 1px; border-collapse: seperate"
                                                            id="tbl" runat="server" visible="true">
                                                            <tr>
                                                                <td colspan="2">
                                                                    <asp:Label ID="lbllevel" Style="border: 2px solid gray; text-align: center;" runat="server"
                                                                        Text='<%# "Level "+ Eval("Approval_Level") %>' Font-Size="12px" Width="100%"
                                                                        CssClass="label" Visible="true"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <table class="TFtable" style="padding: 0px;" cellpadding="1" cellspacing="1">
                                                                        <tr>
                                                                            <td width="40%" style="font-weight: bold">
                                                                                Approved By:
                                                                            </td>
                                                                            <td>
                                                                                <div style="clear: both; overflow: hidden;">
                                                                                    <asp:Label ID="lblHeader2" runat="server" CssClass="overflowmanagement" Style="max-width: 150px;
                                                                                        float: left;"></asp:Label></div>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <table width="100%" cellpadding="1" cellspacing="1">
                                                                        <tr>
                                                                            <td width="45%" style="font-weight: bold">
                                                                                Approval date:
                                                                            </td>
                                                                            <td>
                                                                                <div style="clear: both; overflow: hidden;">
                                                                                    <asp:Label ID="lblHeader4" runat="server"></asp:Label></div>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <table class="TFtable" cellpadding="1" cellspacing="1">
                                                                        <tr>
                                                                            <td width="40%" style="font-weight: bold">
                                                                                Remarks:
                                                                            </td>
                                                                            <td>
                                                                                <div style="clear: both; overflow: hidden;">
                                                                                    <asp:TextBox ID="lblHeader6" TextMode="MultiLine" ReadOnly="true" runat="server"
                                                                                        Style="max-width: 150px; float: left; border: 1px none #fff"></asp:TextBox>
                                                                                    <asp:Label ID="lblHeader7" runat="server" BackColor="" Font-Size="Small" CssClass="overflowmanagement"
                                                                                        Style="max-width: 150px; float: left;"></asp:Label>
                                                                                </div>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                </asp:DataList>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr style="border: 1px solid black;">
                                    <td width="60%">
                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                            <ContentTemplate>
                                                <asp:Label ID="lblFName" runat="server" Text='File Name:' Font-Size="14px"></asp:Label>
                                                <asp:HyperLink ID="lnkDocID1" runat="server" Target="_blank" Font-Bold="true" Font-Size="14px"
                                                    Visible="false"></asp:HyperLink>
                                                <asp:LinkButton ID="lnkDocID" runat="server" Font-Bold="True" Font-Size="14px" OnClick="lnkDocID_Click"
                                                    Visible="false"></asp:LinkButton>
                                                <asp:ImageButton ID="btnForce_Download_Doc" runat="server" ImageUrl="~/Images/forceDownload.png"
                                                    ToolTip="Force download" OnClientClick="return IsFileExists();" OnClick="btnForce_Download_Doc_Click" />
                                                <asp:HiddenField ID="hdnDocPath" runat="server" />
                                                <br>
                                                <br />
                                                <asp:Label ID="lblVName" runat="server" Text="Vessel Name:" Font-Size="14px"></asp:Label>
                                                <asp:Label ID="lblVessName" runat="server" Font-Bold="true" Font-Size="14px"></asp:Label>
                                                <%-- <asp:LinkButton ID="lnkDocID" runat="server"></asp:LinkButton>--%>
                                                <asp:HiddenField ID="hdnDocName" runat="server" />
                                                <asp:HiddenField ID="hdnScheduleStatusID" runat="server" />
                                                <asp:HiddenField ID="hdnSchOfficeID" runat="server" />
                                                <asp:HiddenField ID="hdnSchVesselID" runat="server" />
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:PostBackTrigger ControlID="lnkDocID" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </td>
                                    <td align="right">
                                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                            <ContentTemplate>
                                                <%--  <uc1:ctlRecordNavigation ID="ctlRecordNavigationReport" OnNavigateRow="BindVesselDocHistroy"
                                                    runat="server" />--%>
                                                <asp:Label ID="lblInspectionStatus" runat="server" Font-Bold="true" ForeColor="red"
                                                    Font-Size="12px"></asp:Label>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <%--     vasu--%>
                                <tr>
                                    <td colspan="2">
                                        <table width="100%" style="border: 1px solid grey;">
                                            <tr>
                                                <td style="width: 20%">
                                                    <asp:UpdatePanel ID="UpdatePanel21" runat="server">
                                                        <ContentTemplate>
                                                            <asp:Label ID="lblRRAForm" runat="server" Text="Risk Assessment Forms:" Font-Size="14px"></asp:Label>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </td>
                                                <td id="tdVesselRAForm" runat="server">
                                                    <asp:UpdatePanel ID="upVessel_RAForms" runat="server" UpdateMode="Conditional">
                                                        <ContentTemplate>
                                                            <div id="divRA" style="height: 100px; overflow-y: scroll;" runat="server">
                                                                <asp:DataList ID="dlVessel_RAForms" runat="server" RepeatColumns="5" RepeatDirection="Vertical"
                                                                    RepeatLayout="Table" CellSpacing="2">
                                                                    <ItemTemplate>
                                                                        <div style="background-color: #C3EBFF; border-radius: 2px; padding: 1px; border: 1px solid #ACC9C9;min-height:50px;width:120px;">
                                                                            <asp:HyperLink ID="hlRAForms" runat="server" Text='<%# Eval("Work_Category_Name")%>'
                                                                                Target="_blank" NavigateUrl='<%# "~/JRA/RiskAssessmentDetails.aspx?Assessment_ID="+Eval("Assessment_ID").ToString()+"&Vessel_ID="+Eval("Vessel_ID").ToString() %>'>
                                                                Target="_blank" Font-Size="12px" Font-Names="Calibri"></asp:HyperLink>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                </asp:DataList>
                                                            </div>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <div style="border: 1px solid black; text-align: left; height: 100px;">
                                            <asp:UpdatePanel ID="updVesselHist" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <%--<iframe id="ifrmDocument" name="ifrmDocument" src="<%=GetDocPath()%>" style="  height: 557px;
                                                        width: 100%; border: 1px; padding: 0; margin: 0;" frameborder="no"  ></iframe>--%>
                                                    <table width="100%">
                                                        <tr>
                                                            <td width="15%">
                                                                <asp:Label ID="lblDDate" runat="server" Text="Due Date:"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblDueDate" runat="server" Text=""></asp:Label>
                                                            </td>
                                                            <td width="15%">
                                                                <asp:Label ID="lblCDate" runat="server" Text="Completion Date:"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblCompletionDate" runat="server" Text="" Width="200px"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblVNum" runat="server" Text="Voyage Number:"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblVoyageNum" runat="server" Text="" Width="200px"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblVessAt" runat="server" Text="Vessel Location:"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblVesselAt" runat="server" Text="" Width="200px"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Button ID="btnRework" runat="server" Text="Re-Work" OnClick="btnRework_Click"
                                                                    Visible="False" />
                                                            </td>
                                                            <td>
                                                                <asp:Button ID="BtnApprove" runat="server" Text="Approve" OnClick="btnApprove_Click"
                                                                    Visible="False" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <table style="width: 100%">
                                            <tr>
                                                <td style="width: 50%">
                                                    <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                                        <ContentTemplate>
                                                            <asp:ImageButton ID="imgAddFollowup" runat="server" ImageUrl="../Images/AddFollowup.png"
                                                                OnClientClick="return onAddFollowUp();" Visible="false" />
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </td>
                                                <td style="width: 50%">
                                                    <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                                        <ContentTemplate>
                                                            <asp:ImageButton ID="ImgAddAttachment" runat="server" ImageUrl="../Images/AddAttachment.png"
                                                                OnClientClick="return onAddAttachment();" Visible="false" />
                                                            <asp:Button ID="btnLoadFiles" OnClick="btnLoadFiles_Click" runat="server" Height="10px" />
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 50%; vertical-align: top; height: 200px;">
                                                    <div>
                                                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                                            <ContentTemplate>
                                                                <asp:GridView ID="gvFollowup" AutoGenerateColumns="false" runat="server">
                                                                    <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                                                    <RowStyle CssClass="RowStyle-css" />
                                                                    <HeaderStyle CssClass="HeaderStyle-css" Height="25px" />
                                                                    <PagerStyle CssClass="PMSPagerStyle-css" />
                                                                    <SelectedRowStyle BackColor="#FFFFCC" />
                                                                    <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                                                    <Columns>
                                                                        <asp:BoundField DataField="Date_Of_Created" HeaderText="Date" ItemStyle-HorizontalAlign="Left"
                                                                            HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="440px" />
                                                                        <asp:BoundField DataField="Created_By" HeaderText="User Name" ItemStyle-HorizontalAlign="Left"
                                                                            HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="440px" />
                                                                        <asp:BoundField DataField="Remark" HeaderText="Followup" ItemStyle-HorizontalAlign="Left"
                                                                            HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="440px" ItemStyle-CssClass="WrapText" />
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                </td>
                                                <td style="width: 50%; vertical-align: top; height: 200px;">
                                                    <div>
                                                        <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                                            <ContentTemplate>
                                                                <asp:GridView ID="gvAttachment" AutoGenerateColumns="false" runat="server" OnRowDataBound="gvAttachment_RowDataBound">
                                                                    <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                                                    <RowStyle CssClass="RowStyle-css" />
                                                                    <HeaderStyle CssClass="HeaderStyle-css" Height="25px" />
                                                                    <PagerStyle CssClass="PMSPagerStyle-css" />
                                                                    <SelectedRowStyle BackColor="#FFFFCC" />
                                                                    <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Date" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="200px">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblAttachmentDate" runat="server" Text='<%#Eval("Created_Date")%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Attachments" ItemStyle-Width="400px">
                                                                            <ItemTemplate>
                                                                                <asp:HyperLink ID="lnkAttachment" runat="server" Target="_blank" Text='<%# Eval("Attachment_Name") %>'
                                                                                    NavigateUrl='<%# "/" + System.Configuration.ConfigurationManager.AppSettings["APP_NAME"].ToString() + "/Uploads/FMS/"+ Eval("Attachment_Path").ToString() %>'></asp:HyperLink>
                                                                                <asp:ImageButton ID="btnForce_Download_Attachmen" runat="server" ImageUrl="~/Images/forceDownload.png"
                                                                                    ToolTip="Force download" OnClick="btnForce_Download_Attachmen_Click" CommandArgument='<%# Eval("Attachment_Path").ToString()%>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:ImageButton ID="imgbtnDelete" ImageUrl="~/Purchase/Image/Close.gif" runat="server"
                                                                                    OnClick="imgbtnDelete_Click" CommandArgument='<%#Eval("ID")+","+Eval("Attachment_Path")+","+Eval("Office_ID")+","+Eval("Vessel_ID") %>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <%-- <asp:BoundField DataField="Attachment_Name" HeaderText="File Name" ItemStyle-HorizontalAlign="Left"
                                                            HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="440px" />--%>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <div style="margin-top: 290px;">
                                <asp:UpdatePanel ID="updRightPanel" runat="server">
                                    <ContentTemplate>
                                        <asp:FormView ID="frmVoyage" runat="server" Width="100%" BorderWidth="0px" Font-Size="Small"
                                            Font-Names="Tahoma" OnDataBound="frmVoyage_DataBound">
                                            <RowStyle CssClass="PMSGridRowStyle-css" Height="18" />
                                            <ItemTemplate>
                                                <table>
                                                    <tr>
                                                        <td style="width: 50%;">
                                                            <table width="100%">
                                                                <tr>
                                                                    <td style='font-weight: bold; font-size: 10px; color: #092B4C; text-align: center;
                                                                        width: 15%;'>
                                                                        Master :
                                                                    </td>
                                                                    <td style='width: 10%;'>
                                                                        <asp:Image ID="Img2E" runat="server" src='<%# "/"+ System.Configuration.ConfigurationManager.AppSettings["APP_NAME"].ToString() +"/Uploads/CrewImages/" + Eval("VSL2E_PHOTOURL") %>'
                                                                            alt="" Height="40px" Width="40px" CssClass="transactLog" />
                                                                    </td>
                                                                    <td style="font-size: 10px; color: #0D3E6E; line-height: 20px; text-align: left;">
                                                                        <a style="font-size: 10px; color: #0D3E6E" target="_blank" href='<%# "/"+ System.Configuration.ConfigurationManager.AppSettings["APP_NAME"].ToString() +"/crew/CrewDetails.aspx?ID="+ Eval("VSLM_ID") %>'>
                                                                            <%# Eval("VSL2E_FULLNAME") %>
                                                                        </a>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td style='text-align: center;'>
                                                            &nbsp;
                                                        </td>
                                                        <td style="width: 50%;">
                                                            <table width="100%">
                                                                <tr>
                                                                    <td style='font-weight: bold; font-size: 10px; color: #092B4C; text-align: center;
                                                                        width: 25%;'>
                                                                        Chief Engineer:
                                                                    </td>
                                                                    <td style='width: 10%;'>
                                                                        <asp:Image ID="ImgCE" runat="server" ImageUrl='<%#"/"+ System.Configuration.ConfigurationManager.AppSettings["APP_NAME"].ToString() +"/Uploads/CrewImages/" +Eval("VSLCE_PHOTOURL") %>'
                                                                            alt="" Height="40px" Width="40px" CssClass="transactLog" />
                                                                    </td>
                                                                    <td style="font-size: 10px; color: #0D3E6E; line-height: 20px; text-align: left">
                                                                        <a style="font-size: 10px; color: #0D3E6E" target="_blank" href='<%# "/"+ System.Configuration.ConfigurationManager.AppSettings["APP_NAME"].ToString() +"/crew/CrewDetails.aspx?ID=" +  Eval("VSLCE_ID") %>'>
                                                                            <%# Eval("VSLCE_FULLNAME") %>
                                                                        </a>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                        </asp:FormView>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                        <div style="width: 50%; display: none; font-size: 14px;" id="divReschedule_info">
                            Form Status
                            <asp:UpdatePanel ID="upnlScheduleinfo" runat="server">
                                <ContentTemplate>
                                    <table width="100%" style="border: thin inset #999999; font-size: 12px;">
                                        <tr>
                                            <td colspan="2" align="center" style="border: thin groove #000000">
                                                <asp:Label ID="lblstatus1" runat="server" CssClass="label"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 8%; font-weight: bold;">
                                                <asp:Label ID="lblRe_ScheduledBy" runat="server"></asp:Label>
                                            </td>
                                            <td style="width: 19%">
                                                <asp:Label ID="lblReScheduledBy" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 8%; font-weight: bold;">
                                                Date :
                                            </td>
                                            <td>
                                                <asp:Label ID="lblDate" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 8%; font-weight: bold;">
                                                Remarks :
                                            </td>
                                            <td>
                                                <asp:Label ID="Label5" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                    <table width="100%">
                                        <tr>
                                            <td style="width: 25%">
                                                File name :
                                            </td>
                                            <td style="width: 60%; font-weight: bold;">
                                                <asp:Label ID="lblFileName" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 25%">
                                                Vessel name :
                                            </td>
                                            <td style="width: 50%; font-weight: bold;">
                                                <asp:Label ID="lblVessel" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div id="divApprovalMessage" align="center" runat="server" style="overflow: hidden;
        margin-bottom: 1px; border: 1px solid outset;">
        <table id="Table1" style="width: 100%;">
            <tr>
                <td>
                    <span style="color: #666">File Name:</span>
                    <asp:Label ID="lblDocName1" runat="server" Text=""></asp:Label>
                    &nbsp;|&nbsp; <span style="color: #666">Version:</span>
                    <asp:Label ID="lblCurrentVersion1" runat="server" Text=""></asp:Label>
                    <br />
                    <span style="color: #666">Status:</span>
                    <asp:Label ID="lblOppStatus1" runat="server" Text=""></asp:Label>
                    &nbsp;|&nbsp; <span style="color: #666">Level:</span>
                    <asp:Label ID="lblLevel" runat="server" Text=""></asp:Label>
                    <br />
                    <asp:Label ID="lblApprovalPendingBy" runat="server" Font-Bold="True" ForeColor="#003366"
                        Text="Approval Pending with: "></asp:Label>
                    <asp:Label ID="lblUser" runat="server" Font-Bold="True" ForeColor="#003366"></asp:Label>
                    <br />
                    <asp:Label ID="lblComment" runat="server" Font-Bold="True" ForeColor="#003366"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <div id="dvSchedule" style="display: none; width: 1300px;">
        <iframe id="IframeSchedule" src="" frameborder="0" style="height: 650px; width: 100%">
        </iframe>
    </div>
    <div id="dvScheduleInsp" style="display: none; width: 1300px;">
        <iframe id="IframeScheduleInsp" src="" frameborder="0" style="height: 650px; width: 100%">
        </iframe>
    </div>
    <div id="dvCheckInOut" title="" style="border: 1px solid inset; background-color: #efddef;
        text-align: left; height: 400px; display: none;">
        <iframe id="ifrmDocCheckInOut" name="ifrmDocCheckInOut" style="height: 400px; width: 600px;
            border: 1px; padding: 0; margin: 0;" frameborder="no"></iframe>
    </div>
    <div id="dvFileApproval" title="Approval Levels" style="border: 1px solid inset;
        background-color: #efddef; text-align: left; height: 500px; width: 600px; display: none;">
        <iframe id="IfrmFileApproval" name="IfrmFileApproval" style="height: 480px; width: 600px;
            border: 1px; padding: 0; margin: 0;" frameborder="no"></iframe>
    </div>
    <div id="dvPopupAddAttachment" title="Add Attachments" style="display: none; width: 500px;">
        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
            <ContentTemplate>
                <table style="width: 100%">
                    <tr>
                        <td style="text-align: left">
                            <asp:Label runat="server" ID="myThrobber" Text="Select file" Style="display: none;"><img align="absmiddle" alt="" src="uploading.gif"/></asp:Label>
                            <tlk4:AjaxFileUpload ID="AjaxFileUpload1" runat="server" Padding-Bottom="2" Padding-Left="2"
                                Padding-Right="1" Padding-Top="2" ThrobberID="myThrobber" OnUploadComplete="AjaxFileUpload1_OnUploadComplete"
                                MaximumNumberOfFiles="10"></tlk4:AjaxFileUpload>
                            <%--<ajaxToolkit:AsyncFileUpload ID="AjaxFileUpload1" runat="server" runat="server" Padding-Bottom="2" Padding-Left="2"
                                Padding-Right="1" Padding-Top="2" ThrobberID="myThrobber" OnUploadComplete="AjaxFileUpload1_OnUploadComplete"
                                MaximumNumberOfFiles="10" />--%>
                            <%--<cc1:AjaxFileUpload ID="AjaxFileUpload1" runat="server" Padding-Bottom="2" Padding-Left="2"
                                Padding-Right="1" Padding-Top="2" ThrobberID="myThrobber" OnUploadComplete="AjaxFileUpload1_OnUploadComplete"
                                MaximumNumberOfFiles="10" />--%>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left">
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div id="dvInsRemark" title="Add Followup" style="width: 200px; display: none; border: 1px solid #cccccc;
        background-color: White; border-radius: 5px 5px 5px 5px;">
        <asp:UpdatePanel ID="UpdatePanel11" runat="server">
            <ContentTemplate>
                <table cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td>
                            <asp:TextBox ID="txtNewRemark" runat="server" Rows="6" TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; background-color: #cccccc">
                            <%--<input type="button" id="btnSaveRemark" value="Save" onclick="ASync_Ins_Remark(event,this)" />--%>
                            <asp:Button ID="btnSaveRemark" runat="server" Text="Save" OnClick="btnSaveRemark_Click" />
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div id="dvReworkRemark" title="Rework Schedule" style="width: 300px; display: none;">
        <asp:UpdatePanel ID="UpdatePanel12" runat="server">
            <ContentTemplate>
                <table style="width: 100%;">
                    <tr>
                        <td>
                            <asp:TextBox ID="txtRRemark" runat="server" Width="98%" Height="200px" CssClass="txt"
                                TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                <ContentTemplate>
                                    <asp:Button ID="btnSaveRRemark" runat="server" Text="Save" OnClick="btnSaveRRemark_Click"
                                        OnClientClick="return confirm('Are you sure want to re work the schedule?')">
                                    </asp:Button>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div id="dvAppRemark" title="Approve Schedule" style="width: 300px; display: none;">
        <asp:UpdatePanel ID="UpdatePanel13" runat="server">
            <ContentTemplate>
                <table style="width: 100%;">
                    <tr>
                        <td>
                            <asp:TextBox ID="txtAppRemark" runat="server" Width="98%" Height="200px" CssClass="txt"
                                TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                <ContentTemplate>
                                    <asp:Button ID="BtnAppRemark" runat="server" Text="Save" OnClick="BtnAppRemark_Click"
                                        OnClientClick="return confirm('Are you sure want to approve the schedule?')">
                                    </asp:Button>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div id="dvEditForm" style="display: none; font-size: 12px; width: 800px; height: 400px;"
        title="Edit Forms">
        <asp:UpdatePanel ID="UpdatePanel23" runat="server">
            <ContentTemplate>
                <div id="dveditform" style="width: 490px; height: 431px; float: left;">
                    <table width="100%">
                        <tr>
                            <td style="vertical-align: top; width: 100px;">
                                <asp:Label ID="Label3" runat="server" Text="File Name" Height="5px" Font-Size="12px"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtFormName" runat="server" Font-Size="12px" Width="200px"></asp:TextBox>
                                &nbsp;&nbsp;
                                <asp:TextBox ID="txtExtension" runat="server" Font-Size="12px" Width="50px" Enabled="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="vertical-align: top; width: 100px;">
                                <asp:Label ID="Label2" runat="server" Text="Form Type" Height="5px" Font-Size="12px"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlFormType" runat="server" Font-Size="12px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="vertical-align: top;">
                                <asp:Label ID="lblDept" runat="server" Text="Department" Height="5px" Font-Size="12px"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlDepartments" runat="server" Font-Size="12px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <table width="100%">
                                    <tr>
                                        <td style="text-align: left; width: 21%;">
                                            <asp:Label ID="lblRAForms" runat="server" Text="Risk Assessment Forms" Font-Size="12px"></asp:Label>
                                        </td>
                                        <td style="border: 1px solid grey;">
                                            <asp:UpdatePanel ID="updAttachedRAF" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <div id="dvRAEditForms" style="height: 130px; overflow-y: scroll;" runat="server">
                                                        <asp:DataList ID="dlRAFormsEdit" runat="server" RepeatColumns="5" RepeatDirection="Vertical"
                                                            RepeatLayout="Table" CellSpacing="2">
                                                            <ItemTemplate>
                                                                <div style="background-color: #C3EBFF; border-radius: 2px; padding: 1px; border: 1px solid #ACC9C9;
                                                                    min-height: 65px;">
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:HyperLink ID="hlRAForms" runat="server" Text='<%# Eval("Work_Category_Name")%>'
                                                                                    Target="_blank" Font-Names="Calibri" NavigateUrl='<%# "~/JRA/Libraries/HazardTemplate.aspx?DocID="+Eval("Work_Categ_ID").ToString() %>'>
                                                                                </asp:HyperLink>
                                                                                <asp:HiddenField ID="hdnRAFrm" runat="server" Value='<%# Eval("Work_Categ_ID")%>' />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                            </ItemTemplate>
                                                        </asp:DataList>
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <%--  <td style="text-align: left; vertical-align: top;">
                               
                            </td>
                            <td style="text-align: left; vertical-align: top;">
                              
                            </td>--%>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                                <asp:Button ID="lbtnRAForms" OnClick="OnClick_lbtnRAForms" runat="server" Text="Attach RA Forms"
                                    Visible="true" ForeColor="Black"></asp:Button>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left; vertical-align: top;">
                                <asp:Label ID="lblRemarks" runat="server" Text="Remarks" Font-Size="12px"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtRemarks" TextMode="MultiLine" runat="server" Width="324px" Height="89px"
                                    Font-Size="12px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Button ID="BtnSaveForm" runat="server" Text="Save" OnClick="BtnSaveForm_Click"
                                    CssClass="saveCenter" OnClientClick="return GetSelectedNode();"></asp:Button>
                            </td>
                        </tr>
                    </table>
                </div>
                <div style="width: 300px; height: 360px; float: right; margin-top: 1px; border: 1px solid #cccccc;
                    border-radius: 5px 5px 5px 5px; background-color: #f8f8f8;">
                    <asp:UpdatePanel ID="UpdatePanel25" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <%--     <asp:TreeView ID="trvFile" runat="server" ImageSet="Arrows" NodeIndent="15" ForeColor="Black"
                                                                Width="100%" BorderColor="#cccccc" ShowCheckBoxes="All" 
                                                                EnableClientScript="true" >
                                                             
                                                                <HoverNodeStyle Font-Underline="True" ForeColor="#000" />
                                                                <NodeStyle Font-Names="Tahoma" Font-Size="12px" ForeColor="Black" HorizontalPadding="2px"
                                                                    NodeSpacing="0px" VerticalPadding="2px" />
                                                                <ParentNodeStyle Font-Bold="False" />
                                                                <SelectedNodeStyle BackColor="#B5B5B5" Font-Underline="False" HorizontalPadding="0px"
                                                                    VerticalPadding="0px" CssClass="SelectedNodeStyle" />
                                                            </asp:TreeView>--%>
                            <table>
                                <tr>
                                    <td>
                                        <div style="border-bottom: 1px solid #cccccc; padding-bottom: 2px;">
                                            <span style="float: left; font-weight: bold;">Folder : </span>
                                            <asp:Label ID="lblSelectedFolderName" runat="server" Text="" Font-Bold="true"></asp:Label>
                                        </div>
                                    </td>
                                    <%--  <td>
                                        <asp:Label ID="lblSelectedFolderName" runat="server" Text="" Font-Bold="true"></asp:Label>
                                    </td>--%>
                                </tr>
                                <tr>
                                    <td>
                                        <div style="overflow: scroll; height: 330px; width: 280px; float: right; margin-top: 5px;">
                                            <asp:TreeView ID="trvFile" runat="server" ForeColor="Black" OnSelectedNodeChanged="trvFile_SelectedNodeChanged">
                                                <NodeStyle ImageUrl="~/Images/DocTree/folder.gif" ForeColor="Black" CssClass="treePadding" />
                                                <SelectedNodeStyle Font-Bold="true" ForeColor="Blue" />
                                            </asp:TreeView>
                                            <asp:HiddenField ID="hdnParentID" runat="server" />
                                            <asp:HiddenField ID="hdnChangedParentID" runat="server" />
                                        </div>
                                    </td>
                                </tr>
                            </table>
                            <%--   <div class="fileTreeDemo" style="margin-left: 20px;">
                                                        </div>--%>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div id="divRAFC" runat="server">
        <asp:UpdatePanel ID="udpRACategory" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div id="divRACategory" title="Attach RA Forms" style="display: none; border: 1px solid Gray;
                    text-align: left; font-size: 12px; color: Black; width: 250px; height: 300px;">
                    <table>
                        <tr>
                            <td style="width: 5px">
                                <asp:Label ID="Label4" runat="server" Text="Search" Font-Size="12px"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCatSearch" runat="server" Width="160px">
                                </asp:TextBox>
                            </td>
                            <td style="float: left;">
                                <asp:ImageButton ID="imgbtnSearch" ImageUrl="~/images/search.gif" runat="server"
                                    OnClick="imgbtnSearch_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <asp:Button ID="btnAdd" runat="server" Text="Save" OnClick="btnAdd_Click"></asp:Button>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <asp:Label ID="lblRAFrm" runat="server" Text="RA Forms" Font-Bold="true" Font-Size="12px"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <div style="overflow-y: scroll; height: 200px; border: 1px solid black;">
                                    <asp:CheckBoxList ID="chklRAF_Category" runat="server" Width="230px" Font-Size="12px">
                                    </asp:CheckBoxList>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <asp:HiddenField ID="hdnMode" runat="server" />
    </form>
</body>
</html>
