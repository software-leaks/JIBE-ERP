<%@ Page Title="Vessel Inspection" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="SuperintendentInspection.aspx.cs" Inherits="Technical_Worklist_SuperintendentInspection"
    EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<%@ Register Src="~/UserControl/ucCustomDropDownList.ascx" TagName="ucCustomDropDownList"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/tooltipsytle.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/CssCustomFilterControls.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../../Scripts/Common_Functions.js" type="text/javascript"></script>
    <script src="../../Scripts/JSCustomFilterControls.js" type="text/javascript"></script>
    <script src="../../Scripts/ucCustomDropDownList.js" type="text/javascript"></script>
    <link href="../../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/SMSPopup.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <script src="../../Scripts/Wizard.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.highlight.js" type="text/javascript"></script>
    <script type="text/javascript">
        var MONTH_NAMES = new Array('January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December', 'Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec');
        var DAY_NAMES = new Array('Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday', 'Sun', 'Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat');
        function LZ(x) { return (x < 0 || x > 9 ? "" : "0") + x }



        function OpenInspection() {

            showModal('dvIReOpenInsp');
        }
        function CancelReopen() {

            hideModal('dvIReOpenInsp');
        }
        function isDate(val, format) {
            var date = getDateFromFormat(val, format);
            if (date == 0) { return false; }
            return true;
        }

        function compareDates(date1, dateformat1, date2, dateformat2) {
            var d1 = getDateFromFormat(date1, dateformat1);
            var d2 = getDateFromFormat(date2, dateformat2);
            if (d1 == 0 || d2 == 0) {
                return -1;
            }
            else if (d1 > d2) {
                return 1;
            }
            return 0;
        }
        $(document).ready(function () {
            $('input').keypress(function (event) {
                if (event.keyCode == 13) {
                    event.preventDefault();

                }
            });
        });
        $('input').keypress(function (event) {
            if (event.keyCode == 13) {
                event.preventDefault();

            }
        });
        function formatDate(date, format) {
            format = format + "";
            var result = "";
            var i_format = 0;
            var c = "";
            var token = "";
            var y = date.getYear() + "";
            var M = date.getMonth() + 1;
            var d = date.getDate();
            var E = date.getDay();
            var H = date.getHours();
            var m = date.getMinutes();
            var s = date.getSeconds();
            var yyyy, yy, MMM, MM, dd, hh, h, mm, ss, ampm, HH, H, KK, K, kk, k;
            // Convert real date parts into formatted versions
            var value = new Object();
            if (y.length < 4) { y = "" + (y - 0 + 1900); }
            value["y"] = "" + y;
            value["yyyy"] = y;
            value["yy"] = y.substring(2, 4);
            value["M"] = M;
            value["MM"] = LZ(M);
            value["MMM"] = MONTH_NAMES[M - 1];
            value["NNN"] = MONTH_NAMES[M + 11];
            value["d"] = d;
            value["dd"] = LZ(d);
            value["E"] = DAY_NAMES[E + 7];
            value["EE"] = DAY_NAMES[E];
            value["H"] = H;
            value["HH"] = LZ(H);
            if (H == 0) { value["h"] = 12; }
            else if (H > 12) { value["h"] = H - 12; }
            else { value["h"] = H; }
            value["hh"] = LZ(value["h"]);
            if (H > 11) { value["K"] = H - 12; } else { value["K"] = H; }
            value["k"] = H + 1;
            value["KK"] = LZ(value["K"]);
            value["kk"] = LZ(value["k"]);
            if (H > 11) { value["a"] = "PM"; }
            else { value["a"] = "AM"; }
            value["m"] = m;
            value["mm"] = LZ(m);
            value["s"] = s;
            value["ss"] = LZ(s);
            while (i_format < format.length) {
                c = format.charAt(i_format);
                token = "";
                while ((format.charAt(i_format) == c) && (i_format < format.length)) {
                    token += format.charAt(i_format++);
                }
                if (value[token] != null) { result = result + value[token]; }
                else { result = result + token; }
            }
            return result;
        }


        function _isInteger(val) {
            var digits = "1234567890";
            for (var i = 0; i < val.length; i++) {
                if (digits.indexOf(val.charAt(i)) == -1) { return false; }
            }
            return true;
        }
        function _getInt(str, i, minlength, maxlength) {
            for (var x = maxlength; x >= minlength; x--) {
                var token = str.substring(i, i + x);
                if (token.length < minlength) { return null; }
                if (_isInteger(token)) { return token; }
            }
            return null;
        }


        function getDateFromFormat(val, format) {
            val = val + "";
            format = format + "";
            var i_val = 0;
            var i_format = 0;
            var c = "";
            var token = "";
            var token2 = "";
            var x, y;
            var now = new Date();
            var year = now.getYear();
            var month = now.getMonth() + 1;
            var date = 1;
            var hh = now.getHours();
            var mm = now.getMinutes();
            var ss = now.getSeconds();
            var ampm = "";

            while (i_format < format.length) {
                // Get next token from format string
                c = format.charAt(i_format);
                token = "";
                while ((format.charAt(i_format) == c) && (i_format < format.length)) {
                    token += format.charAt(i_format++);
                }
                // Extract contents of value based on format token
                if (token == "yyyy" || token == "yy" || token == "y") {
                    if (token == "yyyy") { x = 4; y = 4; }
                    if (token == "yy") { x = 2; y = 2; }
                    if (token == "y") { x = 2; y = 4; }
                    year = _getInt(val, i_val, x, y);
                    if (year == null) { return 0; }
                    i_val += year.length;
                    if (year.length == 2) {
                        if (year > 70) { year = 1900 + (year - 0); }
                        else { year = 2000 + (year - 0); }
                    }
                }
                else if (token == "MMM" || token == "NNN") {
                    month = 0;
                    for (var i = 0; i < MONTH_NAMES.length; i++) {
                        var month_name = MONTH_NAMES[i];
                        if (val.substring(i_val, i_val + month_name.length).toLowerCase() == month_name.toLowerCase()) {
                            if (token == "MMM" || (token == "NNN" && i > 11)) {
                                month = i + 1;
                                if (month > 12) { month -= 12; }
                                i_val += month_name.length;
                                break;
                            }
                        }
                    }
                    if ((month < 1) || (month > 12)) { return 0; }
                }
                else if (token == "EE" || token == "E") {
                    for (var i = 0; i < DAY_NAMES.length; i++) {
                        var day_name = DAY_NAMES[i];
                        if (val.substring(i_val, i_val + day_name.length).toLowerCase() == day_name.toLowerCase()) {
                            i_val += day_name.length;
                            break;
                        }
                    }
                }
                else if (token == "MM" || token == "M") {
                    month = _getInt(val, i_val, token.length, 2);
                    if (month == null || (month < 1) || (month > 12)) { return 0; }
                    i_val += month.length;
                }
                else if (token == "dd" || token == "d") {
                    date = _getInt(val, i_val, token.length, 2);
                    if (date == null || (date < 1) || (date > 31)) { return 0; }
                    i_val += date.length;
                }
                else if (token == "hh" || token == "h") {
                    hh = _getInt(val, i_val, token.length, 2);
                    if (hh == null || (hh < 1) || (hh > 12)) { return 0; }
                    i_val += hh.length;
                }
                else if (token == "HH" || token == "H") {
                    hh = _getInt(val, i_val, token.length, 2);
                    if (hh == null || (hh < 0) || (hh > 23)) { return 0; }
                    i_val += hh.length;
                }
                else if (token == "KK" || token == "K") {
                    hh = _getInt(val, i_val, token.length, 2);
                    if (hh == null || (hh < 0) || (hh > 11)) { return 0; }
                    i_val += hh.length;
                }
                else if (token == "kk" || token == "k") {
                    hh = _getInt(val, i_val, token.length, 2);
                    if (hh == null || (hh < 1) || (hh > 24)) { return 0; }
                    i_val += hh.length; hh--;
                }
                else if (token == "mm" || token == "m") {
                    mm = _getInt(val, i_val, token.length, 2);
                    if (mm == null || (mm < 0) || (mm > 59)) { return 0; }
                    i_val += mm.length;
                }
                else if (token == "ss" || token == "s") {
                    ss = _getInt(val, i_val, token.length, 2);
                    if (ss == null || (ss < 0) || (ss > 59)) { return 0; }
                    i_val += ss.length;
                }
                else if (token == "a") {
                    if (val.substring(i_val, i_val + 2).toLowerCase() == "am") { ampm = "AM"; }
                    else if (val.substring(i_val, i_val + 2).toLowerCase() == "pm") { ampm = "PM"; }
                    else { return 0; }
                    i_val += 2;
                }
                else {
                    if (val.substring(i_val, i_val + token.length) != token) { return 0; }
                    else { i_val += token.length; }
                }
            }
            // If there are any trailing characters left in the value, it doesn't match
            if (i_val != val.length) { return 0; }
            // Is date valid for month?
            if (month == 2) {
                // Check for leap year
                if (((year % 4 == 0) && (year % 100 != 0)) || (year % 400 == 0)) { // leap year
                    if (date > 29) { return 0; }
                }
                else { if (date > 28) { return 0; } }
            }
            if ((month == 4) || (month == 6) || (month == 9) || (month == 11)) {
                if (date > 30) { return 0; }
            }
            // Correct hours value
            if (hh < 12 && ampm == "PM") { hh = hh - 0 + 12; }
            else if (hh > 11 && ampm == "AM") { hh -= 12; }
            var newdate = new Date(year, month - 1, date, hh, mm, ss);
            return newdate.getTime();
        }


        function parseDate(val) {
            var preferEuro = (arguments.length == 2) ? arguments[1] : false;
            generalFormats = new Array('y-M-d', 'MMM d, y', 'MMM d,y', 'y-MMM-d', 'd-MMM-y', 'MMM d');
            monthFirst = new Array('M/d/y', 'M-d-y', 'M.d.y', 'MMM-d', 'M/d', 'M-d');
            dateFirst = new Array('d/M/y', 'd-M-y', 'd.M.y', 'd-MMM', 'd/M', 'd-M');
            var checkList = new Array('generalFormats', preferEuro ? 'dateFirst' : 'monthFirst', preferEuro ? 'monthFirst' : 'dateFirst');
            var d = null;
            for (var i = 0; i < checkList.length; i++) {
                var l = window[checkList[i]];
                for (var j = 0; j < l.length; j++) {
                    d = getDateFromFormat(val, l[j]);
                    if (d != 0) { return new Date(d); }
                }
            }
            return null;
        }

    </script>
    <script type="text/javascript">
        function showDialog(url) {
            window.open(url);
        }
        function showEmailNotifiers() {
            document.getElementById("dvEmailNotifiers").style.display = 'block';
        }
        function hideEmailNotifiers() {
            document.getElementById("dvEmailNotifiers").style.display = 'none';
        }

        function showDiv(dv) {
            if (dv) {
                $('#' + dv).show();
            }
        }
        function closeDiv(dv) {
            if (dv) {
                $('#' + dv).hide();
            }
        }
        function checkAll(objRef) {
            var GridView = objRef.parentNode.parentNode.parentNode;
            var inputList = GridView.getElementsByTagName("input");
            for (var i = 0; i < inputList.length; i++) {
                //Get the Cell To find out ColumnIndex
                var row = inputList[i].parentNode.parentNode;
                if (inputList[i].type == "checkbox" && objRef != inputList[i]) {
                    if (objRef.checked) {
                        //If the header checkbox is checked
                        //check all checkboxes
                        //and highlight all rows

                        inputList[i].checked = true;
                    }
                    else {
                        //If the header checkbox is checked
                        //uncheck all checkboxes
                        //and change rowcolor back to original

                        inputList[i].checked = false;
                    }
                }
            }
        }



        function Check_Click(objRef) {
            //Get the Row based on checkbox
            var row = objRef.parentNode.parentNode;


            //Get the reference of GridView
            var GridView = row.parentNode;

            //Get all input elements in Gridview
            var inputList = GridView.getElementsByTagName("input");

            for (var i = 0; i < inputList.length; i++) {
                //The First element is the Header Checkbox
                var headerCheckBox = inputList[0];

                //Based on all or none checkboxes
                //are checked check/uncheck Header Checkbox
                var checked = true;
                if (inputList[i].type == "checkbox" && inputList[i] != headerCheckBox) {
                    if (!inputList[i].checked) {
                        checked = false;
                        break;
                    }
                }
            }
            headerCheckBox.checked = checked;

        }


        function handleStatus() {


            if (document.getElementById('<%=rdbPlanned.ClientID%>').checked == true) {
                document.getElementById('<%=compd.ClientID%>').style.display = "none";
                document.getElementById('<%=dur.ClientID%>').style.display = "none"
                document.getElementById('<%=onb.ClientID%>').style.display = "none"
                document.getElementById('<%=obs.ClientID%>').style.display = "none"

                document.getElementById('<%=durjobs.ClientID%>').style.display = "table-row"


            }
            else {
                document.getElementById('<%=compd.ClientID%>').style.display = "table-row"
                document.getElementById('<%=dur.ClientID%>').style.display = "table-row"
                document.getElementById('<%=onb.ClientID%>').style.display = "table-row"
                document.getElementById('<%=obs.ClientID%>').style.display = "table-row"
                document.getElementById('<%=durjobs.ClientID%>').style.display = "none"
                if (document.getElementById('<%=txtCompletionDate.ClientID%>').text != "") {


                    var minutes = 1000 * 60;
                    var hours = minutes * 60;
                    var days = hours * 24;


                    var Insection = getDateFromFormat(document.getElementById('<%=txtInsectionDate.ClientID%>').value, "dd/MMM/yy");
                    var Completion = getDateFromFormat(document.getElementById('<%=txtCompletionDate.ClientID%>').value, "dd/MMM/yy");

                    var diff_date = Math.round((Completion - Insection) / days);
                    if (diff_date < 0) {
                        document.getElementById('<%=txtCompletionDate.ClientID%>').value = "";
                        // alert('Completion date can not be lesser than the Scheduled Date!');
                        document.getElementById('<%=txtDuration.ClientID%>').value = "";
                        return;
                    }

                    document.getElementById('<%=txtDuration.ClientID%>').value = Math.abs(diff_date) + 1;

                }

            }

        }

        function UpdatePage() {

            //  alert("TEst");

            hideModal("dvSchInsp");
            //  $('#UpdatePanelport').submit();
            // alert('<%= btnSearch.ClientID %>')
            // __doPostBack("UpdatePanelport", "");
            __doPostBack("<%=btnSearch.UniqueID %>", "onclick");

        }

        function ValidateDateDiff() {

            if (document.getElementById('<%=rdbCompleted.ClientID%>').checked == true) {

                if (document.getElementById('<%=txtCompletionDate.ClientID%>').text != "") {


                    var minutes = 1000 * 60;
                    var hours = minutes * 60;
                    var days = hours * 24;






                    var Insection = getDateFromFormat(document.getElementById('<%=txtInsectionDate.ClientID%>').value, "dd/MMM/yy");
                    var Completion = getDateFromFormat(document.getElementById('<%=txtCompletionDate.ClientID%>').value, "dd/MMM/yy");

                    var diff_date = Math.round((Completion - Insection) / days);
                    if (diff_date < 0) {
                        document.getElementById('<%=txtCompletionDate.ClientID%>').value = "";
                        alert('Completion date can not be lesser than the scheduled date.');
                        document.getElementById('<%=txtDuration.ClientID%>').value = "";
                        return;
                    }







                    //debugger;

                    document.getElementById('<%=txtDuration.ClientID%>').value = Math.abs(diff_date) + 1;

                }
            }


        }

        function CalculateOnShore() {


            if (document.getElementById('<%=txtOnboard.ClientID%>').value != "") {

                var Dur = document.getElementById('<%=txtDuration.ClientID%>').value

                var OnBoard = document.getElementById('<%=txtOnboard.ClientID%>').value;

                document.getElementById('<%=txtOnShore.ClientID%>').value = Dur - OnBoard

            }
        }

        //====startInspAttachments====//

        function OpenAttachment(InspectionDetailId, Vessel_Id, Vessel_Name, Schedule_Date, FileCnt, FirstFilePath) {

            if (FileCnt == 1) {
                window.open("../../Uploads/Technical/" + FirstFilePath, "_blank");
            }
            else {
                var pagesrc = "InspAttachment.aspx?InspectionDetailId=" + InspectionDetailId + "&Vessel_Id=" + Vessel_Id + "&Vessel_Name=" + Vessel_Name + "&Schedule_Date=" + Schedule_Date;

                document.getElementById("ifAttach").src = pagesrc;
                showModal('divAttach', false, fn_CloseAttachment);
                $("#divAttach").prop('title', 'Inspection Attachments');
            }
            $("#imgbtnPopupClose").click(function () {
                var btnsearch = document.getElementById('<%=btnSearch.ClientID%>')
                btnsearch.click();
            });
        }
        function fn_CloseAttachment() {
            var btnsearch = document.getElementById('<%=btnSearch.ClientID%>')
            btnsearch.click();
        }
        function GetInspectionReport(InspID, InspectorID, VesselID, ReportType) {
            //        InspectionReport.aspx?InspID="+Eval("SchDetailId")+"&InspectorID="+Eval("Inspctor")+"&VesselID="+Eval("Vessel_Id")+"&ReportType=&#39;"+ Convert.ToString("O")+"&#39;
            try {
                $('#dvInspReport').load("InspectionReport.aspx?InspID=" + InspID + "&InspectorID=" + InspectorID + "&VesselID=" + VesselID + "&ReportType=" + ReportType + "&rnd=" + Math.random() + '#page-content');
            }
            catch (ex) { }
        }

        function ScheduleInspection(ScheduleID, InspectionID) {


            if (ScheduleID == null && InspectionID == null) {
                document.getElementById('IframeSchInsp').src = "ScheduleInspection.aspx";
            }
            else {
                document.getElementById('IframeSchInsp').src = "ScheduleInspection.aspx?ScheduleID=" + ScheduleID + "&SchDetailId=" + InspectionID;
            }

            showModal('dvSchInsp');
            $("#dvSchInsp").prop('title', 'Schedule Inspection');
            return false;
        }
		
    </script>
    <script type="text/javascript">
        function AddremovePortSelection(objthis) {

            if (objthis.checked == false)
                document.getElementById('<%= hdfNewinspectionPortid.ClientID %>').value = document.getElementById('<%= hdfNewinspectionPortid.ClientID %>').value.replace(objthis.value, 0);
            else if (document.getElementById('<%= hdfNewinspectionPortid.ClientID %>').value.indexOf(objthis.value) < 0)
                document.getElementById('<%= hdfNewinspectionPortid.ClientID %>').value += "," + objthis.value.toString();

        }
    </script>
    <script type="text/javascript">
        function Get_JobsAtteched(InspID, SheduleID, VesselID)// Old OnclientClick event function defination not in used now - comment by Pranav Sakpal on 2016-04-29
        {
            document.getElementById('IframeEditJob').src = '../Inspection/AttachJob.aspx?InspID=' + InspID + '&SheduleID=' + SheduleID + '&VesselID=' + VesselID; ;
            showModal('dvEditJob');
            $("#dvEditJob").prop('title', 'Assign Job');

        }

        function Get_JobsTEST(ControlID, InspID, SheduleID, VesselID)// IN this function as early created SheduleID which is not usefull this is just a length of inspectionID and SheduleID not in use. - commented by Pranav Sakpal on 2016-04-29
        {

            document.getElementById('IframeEditJob').src = '../Inspection/AttachJob.aspx?InspID=' + InspID + '&SheduleID=' + SheduleID + '&VesselID=' + VesselID + '&ControlID=' + ControlID;
            showModal('dvEditJob');
            $("#dvEditJob").prop('title', 'Assign Job');

        }

        function updateJobCounts(ControlID, Counts) {



            document.getElementById(ControlID).innerHTML = Counts;
        }

        function saveCloseChild() {

            hideModal('dvEditJob');
        }

    </script>
    <style type="text/css">
        .imgheight img
        {
            height: 16px;
        }
        
        .OverDueCell
        {
            background-color: Red;
            color: White;
            padding-left: 22px;
        }
        .OverDueCell:Link, OverDueCell:visited
        {
            color: White;
        }
        .PaddingCellCss
        {
            padding-left: 20px;
            white-space: normal;
        }
        .PaddingCellHCss
        {
            /**padding-left: 22px;*/
            text-align: center;
        }
        .page
        {
            width: 1440px;
            background-color: #fff;
            margin: 5px auto 0px auto;
            border: 1px solid #496077;
        }
        .OverDueCell a
        {
            color: Yellow;
        }
        .dateTimeTextboxStyle
        {
            border-color: Gray;
        }
        .HeadetTHStyle
        {
            text-align: center;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div id="page-content" style="z-index: -2; white-space: nowrap;">
        <asp:UpdateProgress ID="upUpdateProgress" runat="server">
            <ProgressTemplate>
                <div id="blur-on-updateprogress">
                    &nbsp;</div>
                <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
                    color: black">
                    <img src="../../Images/loaderbar.gif" alt="Please Wait" />
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <div style="font-family: Tahoma; font-size: 12px; height: 100%;">
            <div id="page-header" class="page-title">
                <b>Vessel Inspection</b>
            </div>
            <div style="color: Black;">
                <asp:UpdatePanel ID="UpdatePanelport" runat="server">
                    <ContentTemplate>
                        <div style="margin: 1px; padding: 1px; border: 1px solid #cccccc;">
                            <table width="100%" cellpadding="1" cellspacing="0">
                                <tr>
                                    <td width="100%" valign="top" style="color: Black">
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <table width="100%" cellpadding="3" cellspacing="0">
                                                    <tr>
                                                        <td width="20%" align="right" valign="top">
                                                            <asp:Label ID="lblFleet" Text="Fleet :" runat="server"></asp:Label>
                                                        </td>
                                                        <td width="25%" valign="top" align="left">
                                                            <asp:DropDownList ID="DDLFleet" runat="server" UseInHeader="false" OnSelectedIndexChanged="DDLFleet_SelectedIndexChanged"
                                                                AutoPostBack="true" Width="160" />
                                                            <asp:DropDownList ID="ddlVessel_Manager" runat="server" Width="160" OnSelectedIndexChanged="DDLFleet_SelectedIndexChanged"
                                                                Visible="false" AutoPostBack="true">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td width="20%" align="right" valign="top">
                                                            Status :
                                                        </td>
                                                        <td width="25%" valign="top" align="left">
                                                            <asp:DropDownList ID="ddlStatus" runat="server" UseInHeader="false" Width="160" AutoPostBack="True"
                                                                OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged">
                                                                <asp:ListItem Value="0" Text="--SELECT--" Selected="True"></asp:ListItem>
                                                                <asp:ListItem Value="Pending" Text="Pending"></asp:ListItem>
                                                                <asp:ListItem Value="Overdue" Text="Overdue"></asp:ListItem>
                                                                <asp:ListItem Value="Completed" Text="Completed"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td width="20%" align="right" valign="top">
                                                            Date From :
                                                        </td>
                                                        <td width="25%" valign="top" align="left">
                                                            <asp:TextBox ID="txtFromDate" CssClass="input dateTimeTextboxStyle" runat="server"
                                                                Width="120px" AutoPostBack="True" OnTextChanged="txtFromDate_TextChanged"></asp:TextBox>
                                                            <cc1:CalendarExtender ID="calFromDate" runat="server" Enabled="True" TargetControlID="txtFromDate"
                                                                Format="dd/MMM/yy">
                                                            </cc1:CalendarExtender>
                                                        </td>
                                                        <td width="20%" align="right" valign="top">
                                                            Inspector :
                                                        </td>
                                                        <td width="25%" valign="top" align="left">
                                                            <asp:DropDownList ID="DDLInspector" runat="server" UseInHeader="false" Style="margin-right: 50px"
                                                                Width="160" AutoPostBack="True" OnSelectedIndexChanged="DDLInspector_SelectedIndexChanged" />
                                                        </td>
                                                        <td>
                                                            <asp:Button ID="btnSearch" Text="Search" runat="server" Width="150px" Height="30px"
                                                                OnClick="btnSearch_Click" />
                                                        </td>
                                                        <td width="10%">
                                                            <asp:Button ID="btnScheduleInspection" Text="Schedule Inspections" runat="server"
                                                                UseSubmitBehavior="false" Width="150px" Height="30px" ToolTip="Schedule New Inspection"
                                                                OnClientClick="return ScheduleInspection(null,null);" />
                                                            <%--OnClick="btnScheduleInspection_Click"--%>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="20%" align="right" valign="top">
                                                            Vessel :
                                                        </td>
                                                        <td width="25%" valign="top" align="left">
                                                            <asp:DropDownList ID="DDLVessel" runat="server" UseInHeader="false" Width="160" AutoPostBack="True"
                                                                OnSelectedIndexChanged="DDLVessel_SelectedIndexChanged" />
                                                        </td>
                                                        <td width="20%" align="right" valign="top">
                                                            Schedule Type :
                                                        </td>
                                                        <td width="25%" valign="top" align="left">
                                                            <asp:DropDownList ID="ddlScheduleType" runat="server" UseInHeader="false" Width="160"
                                                                AutoPostBack="True" OnSelectedIndexChanged="ddlScheduleType_SelectedIndexChanged">
                                                                <asp:ListItem Value="0" Text="--SELECT--" Selected="True"></asp:ListItem>
                                                                <asp:ListItem Text="Weekly" Value="Weekly"></asp:ListItem>
                                                                <asp:ListItem Text="Month Wise" Value="Monthwise"></asp:ListItem>
                                                                <asp:ListItem Text="One Time" Value="Onetime"></asp:ListItem>
                                                                <asp:ListItem Text="Duration Wise" Value="Duration"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td width="20%" align="right" valign="top">
                                                            Date To :
                                                        </td>
                                                        <td width="25%" valign="top" align="left">
                                                            <asp:TextBox ID="txtToDate" CssClass="input dateTimeTextboxStyle" runat="server"
                                                                Width="120px" AutoPostBack="True" OnTextChanged="txtToDate_TextChanged"></asp:TextBox>
                                                            <cc1:CalendarExtender ID="calToDate" runat="server" Enabled="True" TargetControlID="txtToDate"
                                                                Format="dd/MMM/yy">
                                                            </cc1:CalendarExtender>
                                                        </td>
                                                        <td width="20%" align="right" valign="top">
                                                            Inspection Type:
                                                        </td>
                                                        <td valign="top" align="left">
                                                            <asp:DropDownList ID="ddlInspectionType" runat="server" UseInHeader="false" Width="160"
                                                                AutoPostBack="True" OnSelectedIndexChanged="ddlInspectionType_SelectedIndexChanged" />
                                                        </td>
                                                        <td>
                                                            <asp:Button ID="btnClear" Text="Clear" runat="server" Width="150px" ToolTip="Clear Selection"
                                                                Height="30px" OnClick="btnClear_Click" />
                                                        </td>
                                                        <td align="center">
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <table width="100%" style="background-color: Yellow; margin: 5px 0px 5px 0px;border:1px solid #cccccc;" id="tbl_Inspection" runat="server"
                            visible="false">
                            <tr>
                                <td>
                                    <b>Certificate Name:</b>&nbsp;<asp:Label ID="lblCertificateName" Text="-" runat="server" />
                                </td>
                            </tr>
                        </table>
                        <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:GridView ID="gvInspecrionSchedule" runat="server" EmptyDataText="No Records Found !!"
                                    AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" DataKeyNames="InspectorID"
                                    CellPadding="3" GridLines="None" CellSpacing="0" Width="100%" AllowSorting="True"
                                    OnRowCreated="gvInspecrionSchedule_RowCreated" Font-Size="12px" CssClass="GridView-css"
                                    OnRowCommand="gvInspecrionSchedule_RowCommand" OnSorting="gvInspecrionSchedule_Sorting"
                                    OnRowDataBound="gvInspecrionSchedule_RowDataBound">
                                    <HeaderStyle CssClass="HeaderStyle-css Crew-HeaderStyle-css" Wrap="false" />
                                    <PagerStyle CssClass="PagerStyle-css" />
                                    <RowStyle CssClass="RowStyle-css" />
                                    <AlternatingRowStyle CssClass="AlternatingRowStyle-css" Wrap="true" />
                                    <SelectedRowStyle CssClass="SelectedRowStyle-css" Wrap="true" />
                                    <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Vessel Name" HeaderStyle-HorizontalAlign="Center">
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="lbtVesslNameHeader" runat="server" CommandName="Sort" ForeColor="Blue"
                                                    CommandArgument="Vessel_Name">Vessel Name</asp:LinkButton>
                                                <img id="Vessel_Name" runat="server" visible="false" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblVesselName" runat="server" Text='<%#Eval("VESSEL_NAME")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="From">
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="lblInspectionScheduleDateHeader" runat="server" CommandName="Sort"
                                                    ForeColor="Blue" CommandArgument="Schedule_date">From</asp:LinkButton>
                                                <img id="Schedule_date" runat="server" visible="false" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                                    <ContentTemplate>
                                                        <asp:LinkButton ID="lblInspectionScheduleDate" runat="server" Style="cursor: pointer"
                                                            Text='<%# DataBinder.Eval(Container,"DataItem.Schedule_date","{0:dd/MMM/yy}") %>'
                                                            CommandName="INSPUPD" CommandArgument='<%#Eval("ScheduleID")+";"+Eval("ActualInspectorID")+";"+Eval("SchDetailId")+";"+Eval("AsgnCnt").ToString()+";"+Eval("InspectorID")+";"+Eval("Schedule_date")+";"+Eval("ActualDate")+";"+Eval("DurJobs")+";"+Eval("Vessel_ID")+";"+Eval("PortID")%>'
                                                            Enabled='<%# GetUpdateEnabled(Eval("ActualDate").ToString().Trim().Length) %>'></asp:LinkButton>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Justify" Width="125px" CssClass="PaddingCellCss">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" CssClass="PaddingCellHCss" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="To">
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="lblInspectionActualDateHeader" runat="server" CommandName="Sort"
                                                    ForeColor="Blue" CommandArgument="ActualDate">To</asp:LinkButton>
                                                <img id="ActualDate" runat="server" visible="false" backcolor="Gray" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblInspectionActualDate" runat="server" Style="cursor: pointer" Text='<%# DataBinder.Eval(Container,"DataItem.ActualDate","{0:dd/MMM/yy}") %>'
                                                    CommandName="INSPUPD" CommandArgument='<%#Eval("ScheduleID")+";"+Eval("ActualInspectorID")+";"+Eval("SchDetailId")+";"+Eval("AsgnCnt").ToString()+";"+Eval("InspectorID")+";"+Eval("Schedule_date")+";"+Eval("ActualDate")%>'
                                                    Enabled='<%# GetUpdateEnabled(Eval("ActualDate").ToString().Trim().Length) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                Port
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblPort" runat="server" Text='<%#Eval("Port")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Justify" CssClass="PaddingCellCss"></ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" CssClass="PaddingCellHCss" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center">
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="lblInspectorHeader" runat="server" CommandName="Sort" ForeColor="Blue"
                                                    CommandArgument="First_Name">Inspector</asp:LinkButton>
                                                <img id="First_Name" runat="server" visible="false" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lnkInspector" runat="server" Text='<%#Eval("InspectorName")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblInspectionDateHeader" runat="server">Inspection Date</asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblInspectionDate" runat="server" Style="cursor: pointer" Text='<%# DataBinder.Eval(Container,"DataItem.ActualDate","{0:dd/MMM/yy}") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Justify" Width="130px" CssClass="PaddingCellCss">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" CssClass="PaddingCellHCss" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                Actual Inspector
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblActualInspectorName" runat="server" Text='<%#Eval("ActualInspectorName")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Justify" CssClass="PaddingCellCss"></ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" CssClass="PaddingCellHCss" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                InspectionType
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblInspectionType" runat="server" Text='<%# Eval("InspectionTypeName") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Justify" CssClass="PaddingCellCss"></ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" CssClass="PaddingCellHCss" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                Total
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblDuration" runat="server" Text='<%# UDFLib.ConvertToInteger(Eval("OnBoard"))+UDFLib.ConvertToInteger(Eval("OnShore")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Justify" CssClass="PaddingCellCss"></ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" CssClass="PaddingCellHCss" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                On Board
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblObBoardI" runat="server" Text='<%# UDFLib.ConvertToInteger(Eval("OnBoard"))%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Justify" CssClass="PaddingCellCss"></ItemStyle>
                                            <HeaderStyle Wrap="false" HorizontalAlign="Center" CssClass="PaddingCellHCss" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                On Shore
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblOnShoreI" runat="server" Text='<%#UDFLib.ConvertToInteger(Eval("OnShore"))%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Justify" CssClass="PaddingCellCss"></ItemStyle>
                                            <HeaderStyle Wrap="false" HorizontalAlign="Center" CssClass="PaddingCellHCss" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblNextDue" runat="server">Next Due</asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lblNexDuei" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.NextDue","{0:dd/MMM/yy}") %>'
                                                    CommandName="INSPUPD" CommandArgument='<%#Eval("ScheduleID")+";"+Eval("ActualInspectorID")+";"+Eval("SchDetailId")+";"+Eval("AsgnCnt").ToString()+";"+Eval("InspectorID")+";"+Eval("Schedule_date")+";"+Eval("ActualDate")%>'
                                                    Enabled='<%# GetUpdateEnabled(Eval("ActualDate").ToString().Trim().Length) %>'></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Justify" CssClass="PaddingCellCss"></ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" CssClass="PaddingCellHCss" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblFromPortHeader" runat="server">Inspection Report</asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:HyperLink ID="lnkExeCommInspectionReport" runat="server" ImageUrl="~/Images/summary-icon.png"
                                                    NavigateUrl='<%#"InspectionReportChecklist.aspx?ScheduleID="+Eval("ScheduleID")+"&InspID="+Eval("SchDetailId")+"&InspectorID="+Eval("Inspctor")+"&VesselID="+Eval("Vessel_Id")+"&ReportType=&#39;"+ Convert.ToString("C")+"&#39;" %>'
                                                    Target="_blank" ToolTip='Checklist Report' Height="16px"></asp:HyperLink>
                                                &nbsp;&nbsp;
                                                <asp:HyperLink ID="lnkRe" runat="server" Text='<%# Eval("AsgnCnt").ToString() %>'
                                                    Font-Bold="true" NavigateUrl='<%#"../Worklist/SupdtInspReport.aspx?SchDetailId="+Eval("SchDetailId")+"&ShowImages=1"%>'
                                                    Target="_blank" Style="vertical-align: super;" ToolTip='Inspection Worklist Report'
                                                    Height="16px"></asp:HyperLink>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Justify" CssClass="PaddingCellCss"></ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" CssClass="PaddingCellHCss" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Schedule">
                                            <HeaderTemplate>
                                                <asp:Label ID="lblEditHeader" runat="server">Schedule</asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                &nbsp;&nbsp;
                                                <asp:LinkButton ID="lnkEdit" runat="server" CommandName="EDIT_SCHEDULE" OnClientClick='<%# " return ScheduleInspection("+Eval("ScheduleID")+","+Eval("SchDetailId")+")"%>'
                                                    CommandArgument='<%#Eval("ScheduleID")+";"+Eval("SchDetailId")+";"+Eval("Vessel_Id")+";"+Eval("Inspctor")%>'
                                                    Enabled='<%#Eval("FrequencyType").ToString()=="Onetime"?false:true  %>' Text='<%#Eval("FrequencyType").ToString() %>'></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Justify" Width="70px" CssClass="PaddingCellCss">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="false" HorizontalAlign="Center" CssClass="PaddingCellHCss" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblDelete" runat="server" Visible='<%# GetAccessInfo("d","","0") %>'>Action</asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <%--<img id="lnkAssignChecklist" src="../../Images/Check_List_001.png" title="Assign Jobs"
                                                                onclick="Get_JobsTEST('<%# ((GridViewRow)Container).FindControl("lnkRe").ClientID %>','<%# Eval("SchDetailId") %>','<%# Eval("ScheduleID").ToString().Length %>','<%# Eval("Vessel_ID") %>');" />--%>
                                                            <asp:ImageButton ID="lnkAssignChecklist" runat="server" Width="20px" Height="20px"
                                                                OnClientClick='<%# "Get_JobsAtteched("+Eval("SchDetailId")+","+Eval("ScheduleID").ToString().Length+","+Eval("Vessel_ID")+")"%>'
                                                                Enabled='<%# GetAccessInfo("e",Eval("ActualDate").ToString(),"1") %>' ImageUrl="~/Images/Check_List_001.png"
                                                                ToolTip="Assign Jobs"></asp:ImageButton>
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td>
                                                            <asp:HyperLink ID="HyperLink3" runat="server" ImageUrl="../../Images/Actions-rating-icon.png"
                                                                NavigateUrl='<%#"../Inspection/CheckListRating.aspx?ScheduleID="+Eval("ScheduleID")+"&InspID="+Eval("SchDetailId")+"&VesselID="+Eval("Vessel_Id")%>'
                                                                Target="_blank" ToolTip='Checklist Rating'></asp:HyperLink>
                                                        </td>
                                                        <td>
                                                            <asp:Image ID="imgAttach" runat="server" ImageUrl="../../Images/attachment.png" Style="cursor: pointer"
                                                                Visible='<%#Eval("FileCnt").ToString()=="0"?false:true%>' onclick='<%# "OpenAttachment(&#39;"+Eval("InspectionDetailId").ToString()+"&#39;,&#39;"+Eval("Vessel_Id").ToString()+"&#39;,&#39;"+Eval("Vessel_Name").ToString()+"&#39;,&#39;"+Eval("Schedule_Date").ToString()+"&#39;,&#39;"+Eval("FileCnt").ToString()+"&#39;,&#39;"+Eval("FirstFilePath").ToString()+"&#39;)" %>'
                                                                ToolTip="View Attachments" />
                                                        </td>
                                                        <td>
                                                            <asp:ImageButton ID="ImgDelete" runat="server" Text="Delete" CommandName="onDelete"
                                                                Visible='<%# GetAccessInfo("d",Eval("ActualDate").ToString(),"1") %>' OnClientClick="return confirm('Are you sure you want to delete this?')"
                                                                CommandArgument='<%#Eval("InspectionDetailId")%>' ForeColor="Black" ToolTip="Delete"
                                                                ImageUrl="~/Images/delete.png" Height="16px"></asp:ImageButton>
                                                        </td>
                                                        <td>
                                                            <asp:ImageButton ID="ImgReOpenInsp" runat="server" Text="Delete" CommandName="ReOpen"
                                                                Visible='<%# Eval("ActualDate").ToString()==""?false:true  %>' OnClientClick="return OpenInspection();"
                                                                CommandArgument='<%#Eval("InspectionDetailId")%>' ForeColor="Black" ToolTip="Re-Open"
                                                                ImageUrl="~/Images/RestoreDiv.png" Height="16px"></asp:ImageButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Justify" Width="70px" CssClass="PaddingCellCss">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="false" HorizontalAlign="Center" CssClass="PaddingCellHCss" />
                                        </asp:TemplateField>
                                        <%-- ====startInspAttachments====--%>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblHeaderAttachments" runat="server">Attachments</asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:HyperLink ID="imgAttachAdd" runat="server" Style="cursor: pointer; color: Blue;
                                                    text-decoration: underline" onclick='<%# "OpenAttachment(&#39;"+Eval("InspectionDetailId").ToString()+"&#39;,&#39;"+Eval("Vessel_Id").ToString()+"&#39;,&#39;"+Eval("Vessel_Name").ToString()+"&#39;,&#39;"+Eval("Schedule_Date").ToString()+"&#39;,&#39;0&#39;,&#39;"+Eval("FirstFilePath").ToString()+"&#39;)" %>'>Add</asp:HyperLink>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Justify" CssClass="PaddingCellCss"></ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" CssClass="PaddingCellHCss" />
                                        </asp:TemplateField>
                                        <%-- ====endInspAttachments====--%>
                                    </Columns>
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <div style="margin-top: 2px; vertical-align: bottom; 
                            background: url(../Images/bg.png) left -10px repeat-x; color: Black; text-align: left;">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td>
                                        <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" PageSize="30" OnBindDataItem="Load_Current_Schedules" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:HiddenField ID="HiddenFlag" runat="server" EnableViewState="False" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hdnScheduleID" runat="server" />
    <asp:HiddenField ID="hdncheckedchecklists" runat="server" />
    <div id="dvWorklist" style="display: none; height: 520px;" title="Inspection Scheduling">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <div id="dx" style="display: none">
                </div>
                <div id="dy" style="display: none">
                </div>
                <script type="text/javascript">
                    $(document).on("mousemove", function (event) {

                        $("#dx").text(event.pageX);
                        $("#dy").text(event.pageY);

                    });


                    function showFollowups(V, W, O, M) {

                        var evt = window.event || M; // this assign evt with the event object
                        var src = evt.srcElement; // this assign current with the event target
                        var pos = 0;
                        var width = 0;
                        var x = 0;
                        var y = 0;
                        var min = 0;
                        if (src == null) {
                            src = evt.target;
                            x = evt.x;
                            y = evt.y;
                            min = 210;
                        }
                        else {
                            pos = $(src).offset();
                            width = $(src).width();
                            x = pos.left;
                            y = pos.top;
                            min = 120;
                        }
                        // var src = window.event.srcElement;
                        x = $("#dx").text();
                        y = $("#dy").text();


                        var url = 'Task_Followups.aspx?WLID=' + W + '&VID=' + V + '&OFFID=' + O;

                        $('#iframeFollowups').load(function () { this.style.height = (this.contentWindow.document.body.offsetHeight + 20) + 'px'; });
                        $('#iframeFollowups').attr("src", url);
                        $('#dialog').show();
                        $("#dialog").css({ "left": (x - 600) + "px", "top": (y - min) + "px", "width": 600 });


                    }
                </script>
                <div id="dialog" title="Follow-ups" style="top: 0px; left: 0px; width: 600px; display: none;
                    position: absolute;">
                    <iframe id="iframeFollowups" style="width: 100%; height: 100%; border: 0px;"></iframe>
                </div>
                <input type="hidden" runat="server" id="hdnFlagCheck" value="false" />
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td style="width: 100%">
                            <div style="width: 100%; overflow: auto; text-align: left; height: 450px;">
                                <div id="Div2" style="border: 1px solid #aabbdd; background-color: #efefef; padding: 8px;
                                    margin-top: 5px; margin-bottom: 2px;">
                                    <div style="float: right; position: relative; font-weight: normal;">
                                        <asp:Image ID="imgFlag" runat="server" ImageUrl="~/Images/Flag_ON.png" ImageAlign="AbsMiddle" />Flagged
                                        for Technical Meeting</div>
                                    <div>
                                        <table cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td style="font-weight: bold; padding-right: 20px;">
                                                    Total Jobs:&nbsp;&nbsp;<asp:Label ID="lblRecordCount" runat="server" Text="0"></asp:Label>
                                                </td>
                                                <td style="font-weight: normal; padding-right: 10px;">
                                                    Priority:
                                                </td>
                                                <td>
                                                    <div style="height: 6px; width: 6px; background-color: Red;" title="Priority: URGENT">
                                                    </div>
                                                </td>
                                                <td style="font-weight: bold; padding-right: 20px;">
                                                    &nbsp;&nbsp;Urgent
                                                </td>
                                                <td>
                                                    <div style="height: 6px; width: 6px; background-color: Yellow;" title="Priority: URGENT">
                                                    </div>
                                                </td>
                                                <td style="font-weight: bold; padding-right: 20px;">
                                                    &nbsp;&nbsp;High
                                                </td>
                                                <td>
                                                    <div style="height: 6px; width: 6px; background-color: transparent;" title="Priority: URGENT">
                                                    </div>
                                                </td>
                                                <td style="font-weight: bold; padding-right: 20px;">
                                                    JOB Status Filter:&nbsp;
                                                </td>
                                                <td style="font-weight: bold; padding-right: 20px;">
                                                    <asp:RadioButtonList ID="rblJobStaus" runat="server" RepeatDirection="Horizontal"
                                                        TextAlign="Right" CellPadding="1" CellSpacing="0" AutoPostBack="True" OnSelectedIndexChanged="Filter_Changed">
                                                        <asp:ListItem Value="0" Text="All"></asp:ListItem>
                                                        <asp:ListItem Value="PENDING" Text="Pending" Selected="True"></asp:ListItem>
                                                        <asp:ListItem Value="COMPLETED" Text="Completed"></asp:ListItem>
                                                        <asp:ListItem Value="REWORKED" Text="Re-worked"></asp:ListItem>
                                                        <asp:ListItem Value="CLOSED" Text="Verified"></asp:ListItem>
                                                        <asp:ListItem Value="OVERDUEPENDING" Text="Overdue"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                                <asp:GridView ID="grdJoblist" runat="server" AutoGenerateColumns="false" CellPadding="4"
                                    ShowHeaderWhenEmpty="true" DataKeyNames="WORKLIST_ID,VESSEL_ID,OFFICE_ID" EnableModelValidation="True"
                                    AllowSorting="false" Width="100%" GridLines="None" OnRowCommand="grdJoblist_RowCommand"
                                    OnRowDataBound="grdJoblist_RowDataBound" OnSorting="grdJoblist_Sorting" AllowPaging="false">
                                    <Columns>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="10px" Visible="true">
                                            <ItemTemplate>
                                                <asp:Image ID="imgAnyChanges" runat="server" Height="16px" Width="16px" ImageUrl="~/Images/exclamation.gif"
                                                    Visible='<%#Eval("MODIFIED").ToString()=="1"?true:false %>' ToolTip="Modified in last 3 days." />
                                                <div style="height: 6px; width: 6px; background-color: <%#Eval("WL_PRIORITY_COLOR").ToString()%>"
                                                    title="Priority: <%#Eval("PRIORITY").ToString()%>">
                                                </div>
                                            </ItemTemplate>
                                            <ControlStyle Width="0px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center"
                                            HeaderText="Vessel" SortExpression="Vessel_Short_Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblVesselShortName" runat="server" Text='<%#Eval("Vessel_Name") %>'
                                                    Style="white-space: nowrap"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                            HeaderText="Code" SortExpression="WORKLIST_ID">
                                            <ItemTemplate>
                                                <asp:Label ID="lbljobcodegriditem" runat="server" Text='<%#Eval("WLID_DISPLAY") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ControlStyle Width="35px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Job Description" SortExpression="JOB_DESCRIPTION"
                                            ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <a href='ViewJob.aspx?OFFID=<%#Eval("OFFICE_ID") %>&WLID=<%#Eval("WORKLIST_ID") %>&VID=<%#Eval("VESSEL_ID") %>'
                                                    target="_blank" style="cursor: hand; color: Blue; text-decoration: none;">
                                                    <asp:Label ID="jd" runat="server" ToolTip='<%#Eval("JOB_DESCRIPTION")%>' Text='<%#Eval("JOB_DESCRIPTION").ToString().Length > 80 ?  Eval("JOB_DESCRIPTION").ToString().Substring(0, 80) + "..." : Eval("JOB_DESCRIPTION").ToString() %>'></asp:Label></a>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                            HeaderText="Assignor" SortExpression="AssignorName">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgrdAssignor" runat="server" Text='<%#Eval("AssignorName") %>'></asp:Label></ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                            HeaderText="PIC" SortExpression="PIC">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPIC" runat="server" Text='<%#Eval("USER_NAME") %>'></asp:Label></ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                            HeaderText="Date Raised" SortExpression="DATE_RAISED">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgrdRaisedDate" runat="server" Text='<%# Eval("DATE_RAISED","{0:dd/MMM/yy}").ToString() == "01/01/1900" ? "" : Eval("DATE_RAISED","{0:d/MMM/yy}")  %>'></asp:Label></ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                            HeaderText="Office Dept" SortExpression="INOFFICE_DEPT">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgrdofficeDept" runat="server" Text='<%#Eval("INOFFICE_DEPT") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                            HeaderText="Vessel Dept" SortExpression="ONSHIP_DEPT">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgrdVesselDept" runat="server" Text='<%#Eval("ONSHIP_DEPT") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                            HeaderText="Expected Compln" SortExpression="DATE_ESTMTD_CMPLTN">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDATE_ESTMTD_CMPLTN" runat="server" Text='<%# Eval("DATE_ESTMTD_CMPLTN","{0:dd/MMM/yy}").ToString() == "01/01/1900" ? "" : Eval("DATE_ESTMTD_CMPLTN","{0:d/MMM/yy}") %>'></asp:Label></ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                            HeaderText="Completed" SortExpression="DATE_COMPLETED">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgrdCompletedOn" runat="server" Text='<%# Eval("DATE_COMPLETED","{0:dd/MMM/yy}").ToString() == "01/01/1900" ? "" : Eval("DATE_COMPLETED","{0:d/MMM/yy}") %>'></asp:Label></ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                            HeaderText="NCR" SortExpression="NCR">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgrdNCR" runat="server" Text='<%#Eval("NCR").ToString()=="0"?"":"YES" %>'></asp:Label></ItemTemplate>
                                            <ControlStyle Width="30px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                            HeaderText="Att.">
                                            <ItemTemplate>
                                                <asp:Image ID="ImgAttachment" runat="server" ImageUrl="~/Images/attach.png" AlternateText="Attachment" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="WORKLIST_STATUS" HeaderText="Status" />
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                            HeaderText="">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="imgFlagOFF" runat="server" ImageUrl="~/Images/Flag_Off.png"
                                                    Visible='<%# !Convert.ToBoolean(Eval("FLAG_Tech_Meeting")) %>' CommandName="FlagJobForMeeting"
                                                    CommandArgument='<%#Eval("OFFICE_ID").ToString()+ "," + Eval("WORKLIST_ID").ToString() + "," + Eval("VESSEL_ID").ToString()+ ",1"%>' />
                                                <asp:ImageButton ID="imgFlagON" runat="server" ImageUrl="~/Images/Flag_ON.png" Visible='<%# Convert.ToBoolean(Eval("FLAG_Tech_Meeting")) %>'
                                                    CommandName="FlagJobForMeeting" CommandArgument='<%#Eval("OFFICE_ID").ToString()+ "," + Eval("WORKLIST_ID").ToString() + "," + Eval("VESSEL_ID").ToString()+ ",0"%>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="checkAll" runat="server" onclick="checkAll(this);" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="checkRow" runat="server" onclick="Check_Click(this)" Checked='<%# SelectCheckbox(Eval("WORKLIST_ID").ToString(),Eval("VESSEL_ID").ToString(),Eval("OFFICE_ID").ToString()) %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataTemplate>
                                        <label id="Label1" runat="server">
                                            No jobs found !!</label>
                                    </EmptyDataTemplate>
                                    <HeaderStyle CssClass="HeaderStyle-css Crew-HeaderStyle-css" />
                                    <PagerStyle CssClass="PagerStyle-css" />
                                    <RowStyle CssClass="RowStyle-css" />
                                    <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                    <SelectedRowStyle CssClass="SelectedRowStyle-css" />
                                    <SortedAscendingCellStyle CssClass="SortedAscendingCellStyle-css" />
                                    <SortedAscendingHeaderStyle CssClass="SortedAscendingHeaderStyle-css" />
                                    <SortedDescendingCellStyle CssClass="SortedDescendingCellStyle-css" />
                                    <SortedDescendingHeaderStyle CssClass="SortedDescendingHeaderStyle" />
                                </asp:GridView>
                                <auc:CustomPager ID="ucCustomPagerctp" OnBindDataItem="Search_Worklist" AlwaysGetRecordsCount="true"
                                    PageSize="10" RecordCountCaption="Total Jobs" runat="server" />
                            </div>
                        </td>
                    </tr>
                </table>
                <div style="text-align: right">
                    <asp:Button ID="btnAssign" runat="server" Text="Assign" Style="margin-top: 10px;
                        width: 100px" OnClick="btnAssign_Click" Visible="false" />
                    &nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnAssignandClose" runat="server" Text="Save and Close" Style="margin-top: 10px;
                        width: 200px" OnClick="btnAssignandClose_Click" />
                    <asp:Button ID="btnGenerateReport" runat="server" Text="Generate Report" Style="margin-top: 10px;
                        width: 150px; margin-left: 10px; margin-right: 10px" OnClick="btnGenerateReport_Click" />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div id="dvUpdateInspe" style="display: none;" title="Update Inspection" onkeydown="return (event.keyCode!=13);">
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>
                <table style="margin: 10px">
                    <tr>
                        <td>
                            Status:
                        </td>
                        <td>
                            <asp:RadioButton ID="rdbPlanned" runat="server" Text="Planned" GroupName="Plan" Checked="true"
                                onkeydown="return (event.keyCode!=13);" onchange="handleStatus();" />
                            <asp:RadioButton ID="rdbCompleted" runat="server" Text="Completed" GroupName="Plan"
                                onkeydown="return (event.keyCode!=13);" onchange="handleStatus();" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Inspection date:
                        </td>
                        <td>
                            <asp:TextBox ID="txtInsectionDate" CssClass="input" runat="server" Width="120px"
                                onkeydown="return (event.keyCode!=13);" onchange="ValidateDateDiff()"></asp:TextBox>
                            <cc1:CalendarExtender ID="caltxtInsectionDate" runat="server" Enabled="True" TargetControlID="txtInsectionDate"
                                Format="dd/MMM/yy" />
                        </td>
                    </tr>
                    <tr id="compd" runat="server">
                        <td>
                            Completion date: <span style="color: Red">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCompletionDate" CssClass="input" runat="server" onchange="ValidateDateDiff()"
                                onkeydown="return (event.keyCode!=13);" Width="120px"></asp:TextBox>
                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" TargetControlID="txtCompletionDate"
                                Format="dd/MMM/yy" />
                        </td>
                    </tr>
                    <tr id="durjobs" runat="server">
                        <td>
                            Duration (days): <span style="color: Red">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtDurJobsU" runat="server" Width="120px" onkeydown="return (event.keyCode!=13);"></asp:TextBox>&nbsp;
                        </td>
                    </tr>
                    <tr id="dur" runat="server">
                        <td>
                            Duration (days):
                        </td>
                        <td>
                            <asp:TextBox ID="txtDuration" runat="server" Width="120px" onkeydown="return (event.keyCode!=13);"></asp:TextBox>&nbsp;
                        </td>
                    </tr>
                    <tr id="onb" runat="server">
                        <td>
                            On board (days): <span style="color: Red">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtOnboard" runat="server" Width="120px" onchange="CalculateOnShore()"
                                onkeydown="return (event.keyCode!=13);"></asp:TextBox>&nbsp;
                        </td>
                    </tr>
                    <tr id="obs" runat="server">
                        <td>
                            On shore (days): <span style="color: Red">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtOnShore" runat="server" Width="120px" onkeydown="return (event.keyCode!=13);"></asp:TextBox>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Inspector: <span style="color: Red">*</span>
                        </td>
                        <td>
                            <asp:DropDownList ID="DDLInspectorA" runat="server" UseInHeader="false" Width="160" />
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top;">
                            Port: <span style="color: Red">*</span>
                        </td>
                        <td style="vertical-align: top;">
                            <asp:DropDownList ID="DDLPort" runat="server" Width="160" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <asp:Button ID="btnUpdateInspection" runat="server" Text="Update inspection" OnClick="btnUpdateInspection_Click" />
                            <asp:HiddenField ID="hdfNewinspectionPortid" runat="server" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div id="dvIReOpenInsp" title="Re-Open Inspection" style="display: none;">
        <table>
            <tr>
                <td>
                    <asp:Label ID="lblReason" runat="server" Text="Reason" Font-Size="12px"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtReason" runat="server" TextMode="MultiLine" Height="80px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="BtnOpen" runat="server" Text="Open" Style="margin-top: 10px; width: 100px"
                        OnClick="BtnOpen_Click" />
                </td>
                <td>
                    <asp:Button ID="BtnCancel" runat="server" Text="Cancel" Style="margin-top: 10px;
                        width: 100px" OnClientClick="return CancelReopen();" />
                </td>
            </tr>
        </table>
    </div>
    <%--Text='<%#Eval("FrequencyType").ToString()=="Onetime"?"Unscheduled":"Reschedule"  %>'--%>
    <div id="divAttach" style="display: none; width: 1000px; height: 490px;" title="Inspection Attachments">
        <iframe id="ifAttach" src="" style="width: 100%; height: 450px;"></iframe>
    </div>
    <div id="dvSchInsp" style="display: none; width: 1000px;" title="Schedule Inspection">
        <iframe id="IframeSchInsp" src="" frameborder="0" style="height: 500px; width: 100%">
        </iframe>
    </div>
    <%--    <asp:UpdatePanel ID="UpdatePanel4" runat="server" >
            <ContentTemplate>
    <div id="dvInspReport" runat="server"  style="display:inline;" >
    
    </div>
    </ContentTemplate>
    </asp:UpdatePanel>--%>
    <%--Visible='<%# Eval("ActualDate").ToString() != "" ? true : false%>'--%>
    <div id="dvEditJob" style="display: none; width: 1400px;" title="Assign Job">
        <iframe id="IframeEditJob" src="" frameborder="0" style="height: 500px; width: 100%">
        </iframe>
    </div>
</asp:Content>
