<%@ Page Title="Vetting Details" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Vetting_Details.aspx.cs" Inherits="Technical_Vetting_Vetting_VettingDetails"
    EnableEventValidation="false"  %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="../../UserControl/ucCustomDropDownList.ascx" TagName="ucfDropdown"
    TagPrefix="CustomFilter" %>
<%@ Register Src="../../UserControl/ucCustomStringFilter.ascx" TagName="ucfString"
    TagPrefix="CustomFilter" %>
<%@ Register Src="../../UserControl/ucCustomNumberFilter.ascx" TagName="ucfNumber"
    TagPrefix="CustomFilter" %>
<%@ Register Src="../../UserControl/ucCustomDateFilter.ascx" TagName="ucfDate" TagPrefix="CustomFilter" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../../Styles/tooltipsytle.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery.min.js" type="text/javascript"></script>
    <link href="../../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/CssCustomFilterControls.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/Common_Functions.js" type="text/jscript"></script>
    <script src="../../Scripts/ModalPopUp.js" type="text/jscript"></script>
    <script src="../../Scripts/JSCustomFilterControls.js" type="text/javascript"></script>
    <script src="../../Scripts/ucCustomDropDownList.js" type="text/javascript"></script>
    <script lang="javascript" type="text/javascript">

        function ValidateOnBtnSave() {

            var strDateFormat = '<%= strDateFormat %>';
            var strCheckVetStatus = document.getElementById($('[id$=hdnStatus]').attr('id')).value;
            if ($.trim($("#" + $('[id$=txtVetDate]').attr('id')).val()) == "") { //Check for blank date field 
                alert("Enter Date");
                $("#" + $('[id$=txtVetDate]').attr('id')).focus();
                return false;
            }
            if ($.trim($("#" + $('[id$=DDLQuestionnaire]').attr('id')).val()) == "0") {
                alert("Select Questionnaire");
                $("#" + $('[id$=DDLQuestionnaire]').attr('id')).focus();
                return false;
            }

//            if ($.trim($("#" + $('[id$=DDLOilMajor]').attr('id')).val()) == "0") {
//                alert("Select Oil Major");
//                $("#" + $('[id$=DDLOilMajor]').attr('id')).focus();
//                return false;
//            }
//            if ($.trim($("#" + $('[id$=DDLPort]').attr('id')).val()) == "0") {
//                alert("Select Port");
//                $("#" + $('[id$=DDLPort]').attr('id')).focus();
//                return false;
//            }
            if (strCheckVetStatus != "Planned") {

                if ($.trim($("#" + $('[id$=DDLInspector]').attr('id')).val()) == "0") {
                    alert("Select Inspector");
                    $("#" + $('[id$=DDLInspector]').attr('id')).focus();
                    return false;
                }
                if ($.trim($("#" + $('[id$=txtNoOfDays]').attr('id')).val()) == "") {
                    alert("Enter Number Of Days");
                    $("#" + $('[id$=txtNoOfDays]').attr('id')).focus();
                    return false;
                }

                if (isNaN($.trim($("#" + $('[id$=txtNoOfDays]').attr('id')).val()))) {
                    alert("Invalid Number Of Days");
                    $("#" + $('[id$=txtNoOfDays]').attr('id')).focus();
                    return false;
                }



            }
            if ($.trim($("#" + $('[id$=txtVetDate]').attr('id')).val()) != "") {
                if (IsInvalidDate($.trim($("#" + $('[id$=txtVetDate]').attr('id')).val()), strDateFormat)) {
                    alert("Enter valid Date<%= UDFLib.DateFormatMessage() %>");
                    $("#" + $('[id$=txtVetDate]').attr('id')).focus();
                    return false;
                }
            }

            if ($.trim($("#" + $('[id$=DDLQuestionnaire]').attr('id')).val()) != $.trim($("#" + $('[id$=hdnQuestionnaireID]').attr('id')).val())) {
                var rowscount = document.getElementById($('[id$=hdfObsCount]').attr('id')).value;
                if (rowscount > 0) {
                    var cnf = confirm("All Observation added against the Questionnaire will be removed.\nAre you sure you want to change the Questionnaire?");
                    if (cnf == 1) {
                        return true;
                    }
                    else if (cnf == 0) {
                        return false;
                    }
                }

            }
        }


        function ValidateOnImgStatus() {
            var strDateFormat = '<%= strDateFormat %>';
            var strCheckVetStatus = document.getElementById($('[id$=hdnStatus]').attr('id')).value;
            if ($.trim($("#" + $('[id$=txtValidUntil]').attr('id')).val()) != "") {
                if (IsInvalidDate($.trim($("#" + $('[id$=txtValidUntil]').attr('id')).val()), strDateFormat)) {
                    alert("Enter valid Date<%= UDFLib.DateFormatMessage() %>");
                    $("#" + $('[id$=txtValidUntil]').attr('id')).focus();
                    return false;
                }
            }
            if ($.trim($("#" + $('[id$=txtRespondBy]').attr('id')).val()) != "") {
                if (IsInvalidDate($.trim($("#" + $('[id$=txtRespondBy]').attr('id')).val()), strDateFormat)) {
                    alert("Enter valid Date<%= UDFLib.DateFormatMessage() %>");
                    $("#" + $('[id$=txtRespondBy]').attr('id')).focus();
                    return false;
                }
            }
            if (strCheckVetStatus == "Completed") {

                if ($.trim($("#" + $('[id$=txtValidUntil]').attr('id')).val()) == "") { //Check for blank date field 
                    alert("Enter Date");
                    $("#" + $('[id$=txtValidUntil]').attr('id')).focus();
                    return false;
                }


            }
            if (strCheckVetStatus == "In-Progress") {

                if ($.trim($("#" + $('[id$=txtRespondBy]').attr('id')).val()) == "") { //Check for blank date field 
                    alert("Enter Date");
                    $("#" + $('[id$=txtRespondBy]').attr('id')).focus();
                    return false;
                }
            }
        }
        function CreateNewObs(VettingID, FleetCode, VesselID) {

            var pagesrc = "Vetting_AddObservationNotes.aspx?Vetting_ID=" + VettingID + "&FleetCode=" + FleetCode + "&Vessel_ID=" + VesselID + "&Mode=Add";

            document.getElementById("iFrmNewObs").src = pagesrc;
            $("#dvNewObs").prop('title', 'Add Note/Observation');
            showModal('dvNewObs', true, CloseNote_Observation);

        }


        function CloseNote_Observation() {
            hideModal('dvNewObs')
            document.getElementById($('[id$=btnRetrieve]').attr('id')).click();
        }
        function AddAttachment(VettingID) {
            var pagesrc = "Vetting_Attachments.aspx?Vetting_ID=" + VettingID;

            document.getElementById("iFrmVetAttach").src = pagesrc;
            $("#divVetAttach").prop('title', 'Vetting Attachments');
            showModal('divVetAttach');
        }
        function AddRemarks(VettingID) {
            var pagesrc = "Vetting_Remarks.aspx?Vetting_ID=" + VettingID;

            document.getElementById("iFrmVetRemark").src = pagesrc;
            $("#divVetRemark").prop('title', 'Vetting Remarks');
            showModal('divVetRemark');
        }
        function UpdatePage() {

            hideModal("divVetAttach");
            __doPostBack("<%=btnRetrieve.UniqueID %>", "onclick");

        }
        function ValidateFilter() {
        }
        function CValidateFilter() {
        }
        function toggleAdvSearch(obj) {


            if ($(obj).text() == "Open Advance Filter") {
                $(obj).text("Close Advance Filter");
                $("#dvAdvanceFilter").show();
            }
            else {
                $(obj).text("Open Advance Filter");
                $("#dvAdvanceFilter").hide();
            }

            if ($("#<%= hfAdv.ClientID %>").val() == "c") {
                $("#<%= hfAdv.ClientID %>").val('o');
            }
            else {
                $("#<%= hfAdv.ClientID %>").val('c');
            }
        }

        function toggleSerachPostBack(obj, objval) {
            if (obj != null) {
                if (objval == 'o') {
                    $(obj).text("Close Advance Filter");
                    $("#dvAdvanceFilter").show();
                }
                else {
                    $("#advText").text("Open Advance Filter");
                    $("#dvAdvanceFilter").hide();
                }
            }
        }
        function toggleOnSearchClearFilter(obj, objval) {


            if (objval == 'o') {
                $(obj).text("Close Advance Filter");
                $("#dvAdvanceFilter").show();
            }
            else {
                $("#advText").text("Open Advance Filter");
                $("#dvAdvanceFilter").hide();
            }


        }
        function getOperatingSystem() {
            var OSName = "";
            var Browser = "";

            if (navigator.appVersion.indexOf("Win") != -1) OSName = "Windows";
            if (navigator.appVersion.indexOf("Mac") != -1) OSName = "MacOS";
            if (navigator.appVersion.indexOf("X11") != -1) OSName = "UNIX";
            if (navigator.appVersion.indexOf("Linux") != -1) OSName = "Linux";

            var pnlObsjobs = document.getElementById($('[id$=PnlObsJobs]').attr('id'));


            if (OSName == "MacOS") {
                if (document.getElementById($('[id$=btnExport]').attr('id')) != null) {
                    document.getElementById($('[id$=btnExport]').attr('id')).style.display = "none";
                }

            }

            if (OSName == "Windows") {
                if (document.getElementById($('[id$=btnMacExport]').attr('id')) != null) {
                    document.getElementById($('[id$=btnMacExport]').attr('id')).style.display = "none";
                }

            }


        }



        var PendingJobs = null;
        function BindObsPendingJobs(Vetting_ID, Question_ID, Observation_ID, ev, objthis) {

            if (PendingJobs != null)
                PendingJobs.abort();

            var service = Sys.Net.WebServiceProxy.invoke('../../JibeWebService.asmx', 'VET_Get_OBSWisePendingJobsTooltip', false, { 'Vetting_ID': parseInt(Vetting_ID), "Question_ID": parseInt(Question_ID), "Observation_ID": parseInt(Observation_ID) }, onSuccessBindJobs, OnfailBindJobs, new Array(ev, objthis));
            PendingJobs = service.get_executor();
        }
        var CompletedJobs = null;
        function BindObsCompletedJobs(Vetting_ID, Question_ID, Observation_ID, ev, objthis) {

            if (CompletedJobs != null)
                CompletedJobs.abort();

            var service = Sys.Net.WebServiceProxy.invoke('../../JibeWebService.asmx', 'VET_Get_CompletedJobsTooltip', false, { 'Vetting_ID': parseInt(Vetting_ID), "Question_ID": parseInt(Question_ID), "Observation_ID": parseInt(Observation_ID) }, onSuccessBindJobs, OnfailBindJobs, new Array(ev, objthis));
            CompletedJobs = service.get_executor();
        }
        var VerifiedJobs = null;
        function BindObsVerifiedJobs(Vetting_ID, Question_ID, Observation_ID, ev, objthis) {

            if (VerifiedJobs != null)
                VerifiedJobs.abort();

            var service = Sys.Net.WebServiceProxy.invoke('../../JibeWebService.asmx', 'VET_Get_VerifiedJobsTooltip', false, { 'Vetting_ID': parseInt(Vetting_ID), "Question_ID": parseInt(Question_ID), "Observation_ID": parseInt(Observation_ID) }, onSuccessBindJobs, OnfailBindJobs, new Array(ev, objthis));
            VerifiedJobs = service.get_executor();
        }
        var DeferredJobs = null;
        function BindObsDeferredJobs(Vetting_ID, Question_ID, Observation_ID, ev, objthis) {

            if (DeferredJobs != null)
                DeferredJobs.abort();

            var service = Sys.Net.WebServiceProxy.invoke('../../JibeWebService.asmx', 'VET_Get_DefferedJobsTooltip', false, { 'Vetting_ID': parseInt(Vetting_ID), "Question_ID": parseInt(Question_ID), "Observation_ID": parseInt(Observation_ID) }, onSuccessBindJobs, OnfailBindJobs, new Array(ev, objthis));
            DeferredJobs = service.get_executor();
        }

        //----------------OnSuccess & onFail common function for all tooltip---------------
        function onSuccessBindJobs(retVal, ev) {


            js_ShowToolTip_Fixed(retVal, ev[0], ev[1], "Jobs");
        }

        function OnfailBindJobs(result) {
            //  var res = result.d;
            // alert(res);
        }


        var QuestionExe = null;
        function BindQuestions(Questionnaire_ID, SectionNo, Question_ID, ev, objthis) {

            if (QuestionExe != null)
                QuestionExe.abort();

            var service = Sys.Net.WebServiceProxy.invoke('../../JibeWebService.asmx', 'VET_Get_QuestionForTooltip', false, { 'Questionnaire_ID': parseInt(Questionnaire_ID), "SectionNo": parseInt(SectionNo), "Question_ID": parseInt(Question_ID) }, onSuccessBindQuest, OnfailBindQuest, new Array(ev, objthis));
            QuestionExe = service.get_executor();
        }

        //----------------OnSuccess & onFail common function for all tooltip---------------
        function onSuccessBindQuest(retVal, ev) {


            js_ShowToolTip(retVal, ev[0], ev[1]);
        }

        function OnfailBindQuest(result) {
            //var res = result.d;
            //alert(res);
        }



        function saveCloseChild() {

            hideModal('divAssignJobs');
            document.getElementById($('[id$=btnRetrieve]').attr('id')).click();

        }


        function onPerformVetting(VettingID) {

            var pagesrc = "Vetting_CreateNewVetting.aspx?Vetting_ID=" + VettingID + "&Mode=PV";

            document.getElementById("iFrmPerVet").src = pagesrc;
            $("#divPerVet").prop('title', 'Perform Vetting');
            showModal('divPerVet');
        }

        function OnImportObservation(VettingID) {
            $("#divImportObs").prop('title', 'Import Observation');
            showModal('divImportObs');
        }

        $(document).ready(function () {
            var wh = 'Vetting_ID=<%=Request.QueryString["Vetting_ID"]%> '
            Get_Record_Information_Details('VET_DTL_Vetting', wh);
            Get_CrewRecord_Information_Details('VET_DTL_Vetting', wh);

        });


        function Get_RecordInfo() {
            var wh = 'Vetting_ID=<%=Request.QueryString["Vetting_ID"]%> '
            Get_Record_Information_Details('VET_DTL_Vetting', wh);
            Get_CrewRecord_Information_Details('VET_DTL_Vetting', wh);
        }


        function PortCall(VesselID, TypeID, Date, Questionnaire, OilMajor, Inspector, Port, NoDays, RespNextDue) {
            document.getElementById('IframePortCall').src = "Vetting_PortCall.aspx?VesselID=" + VesselID + "&TypeID=" + TypeID + "&Date=" + Date + "&Questionnaire=" + Questionnaire + "&OilMajor=" + OilMajor + "&Inspector=" + Inspector + "&Port=" + Port + "&NoDays=" + NoDays + "&RespNextDue=" + RespNextDue;
            $("#dvPortCall").prop('title', 'Port');
            showModal('dvPortCall', false);
            return false;
        }

        function HidePortCall(VesselID, TypeID, Date, Questionnaire, OilMajor, Inspector, Port, PortCallID, NoDays, RespNextDue) {
            hideModal("dvPortCall");
            CreateNewVetting(VesselID, TypeID, Date, Questionnaire, OilMajor, Inspector, Port, PortCallID, NoDays, RespNextDue);
        }

        function CreateNewVetting(VesselID, TypeID, Date, Questionnaire, OilMajor, Inspector, Port, PortCallID, NoDays, RespNextDue) {
            var hdfVettingID = document.getElementById('<%= hdfVettingID.ClientID%>').value;
            document.getElementById('iFrmPerVet').src = "Vetting_CreateNewVetting.aspx?Vetting_ID=" + hdfVettingID + "&Mode=PV&VesselID=" + VesselID + "&TypeID=" + TypeID + "&Date=" + Date + "&Questionnaire=" + Questionnaire + "&OilMajor=" + OilMajor + "&Inspector=" + Inspector + "&Port=" + Port + "&PortCallID=" + PortCallID + "&NoDays=" + NoDays + "&RespNextDue=" + RespNextDue;
            showModal('divPerVet');
            $("#divPerVet").prop('title', 'Perform Vetting ');
            return false;

        }

        function UpdatePageAfterSave(Vetting_ID) {

            hideModal("divPerVet");
            CreateNewObs(Vetting_ID);
            document.getElementById($('[id$=btnCreateVetting]').attr('id')).click();

        }
        function Confirm(msg) {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm(msg + ' Are you sure you want to upload the XML file?')) {
                confirm_value.value = "Yes";
                document.getElementById($('[id$=btnYesConfirm]').attr('id')).click();

            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }

        function ImportConfirm(IsDataInValid, IsObsExists, VesselName, Port, VettingDate) {
            if (IsDataInValid == 1) {
                if (confirm('The following data from xml file does not match the existing data ,' + VesselName + ',' + Port + ',' + VettingDate + ' Are you sure you want to upload the XML file?')) {
                    if (IsObsExists == 1) {
                        if (confirm('Warning! The vetting inspection already contains observation. Uploading the XML file will add the observations to the current observation, Are you sure you want to upload the XML file?')) {
                            document.getElementById($('[id$=btnconfirm]').attr('id')).click();
                        }

                    }
                    else {

                        document.getElementById($('[id$=btnconfirm]').attr('id')).click();
                    }
                }
            }
            else {

                if (IsObsExists == 1) {
                    if (confirm('Warning! The vetting inspection already contains observation. Uploading the XML file will add the observations to the current observation, Are you sure you want to upload the XML file?')) {
                        document.getElementById($('[id$=btnconfirm]').attr('id')).click();
                    }

                }
                else {

                    document.getElementById($('[id$=btnconfirm]').attr('id')).click();
                }
            }

        }
       
    </script>
    <style type="text/css">
        .lbl
        {
            color: #349fe7;
        }
        .lblDetails
        {
            padding-top: 3px;
            padding-bottom: 3px;
            padding-right: 3px;
            padding-left: 2px;
            border: 1px solid #cccccc;
        }
        .Qpoint
        {
            cursor: pointer;
        }
        .lblDetailsDisable
        {
            padding-top: 3px;
            padding-bottom: 3px;
            padding-right: 3px;
            padding-left: 2px;
            border: 1px solid #cccccc;
            background-color: #cccccc;
        }
        .ihyperlink img
        {
            height: 12px;
        }
        .hidedropdown
        {
            display: none;
        }
        body
        {
            color: black;
            font-family: Tahoma;
            font-size: 12px;
        }
        select
        {
            height: 21px;
            font-family: Tahoma;
            font-size: 12px;
        }
        input
        {
            font-family: Tahoma;
            font-size: 12px;
        }
        textarea
        {
            font-family: Tahoma;
            font-size: 12px;
        }
        .link
        {
            text-decoration: none;
            text-transform: capitalize;
        }
        .roundedBox
        {
            border-radius: 5px;
            border: 2px solid white;
            background-color: #DBDFD5;
            text-align: center;
            font-size: 14px;
            color: #555;
            margin: 2px;
            padding: 2px;
        }
        .gradiant-css-blue
        {
            filter: progid:DXImageTransform.Microsoft.Gradient(GradientType=0,StartColorStr='#CEE3F6',EndColorStr='#99BBE6');
            background: -webkit-gradient(linear, left top, left bottom, from(#CEE3F6), to(#99BBE6));
            background: -moz-linear-gradient(top,  #CEE3F6,  #99BBE6);
            color: Black;
        }
        .data
        {
            border: 1px solid #efefef;
            background-color: #F5F6CE;
        }
        .row-header
        {
            font-weight: bold;
        }
        .hide
        {
            display: none;
        }
        
        
        
        
        .page
        {
            max-width: 100%;
        }
        .selectlist select
        {
            -webkit-appearance: none;
            -moz-appearance: none;
            text-indent: 1px;
            text-overflow: '';
            border: 0px;
        }
        .selectlist select::-ms-expand
        {
            display: none;
        }
        
        .badge
        {
            background-image: url(../../Images/VET_Error.png);
            width: 33px;
            height: 28px;
            background-repeat: no-repeat;
            display: block;
            position: relative;
        }
        .badge div
        {
            background: red none repeat scroll 0 0;
            border-radius: 50px;
            bottom: 0;
            color: #fff;
            display: inline;
            float: right;
            font-size: 8px;
            height: 12px;
            padding: 3px;
            position: absolute;
            right: 0;
            text-align: center;
            width: 12px;
        }
        .badge div a
        {
            text-decoration: none;
        }
        .style1
        {
            width: 100px;
            height: 18px;
        }
        .style2
        {
            width: 120px;
            height: 18px;
        }
        .style3
        {
            width: 200px;
            height: 18px;
        }
        .GroupHeaderStyle-css
       {
   
            text-align: center;
          
        }
        .bigblue{font-size:16px; color:#fff; background:#349fe7; border:1px solid #399fe7; padding:10px 50px; margin:0 auto; font-weight:bold;}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
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
    <div class="page-title">
        <asp:Label ID="lblPageTitle" runat="server" Font-Bold="true" Font-Size="14px" Text="Vetting Details"></asp:Label>
    </div>
    <div class="page-content" style="font-family: Tahoma; background-color: #f2f2f2;
        margin-top: 5px; font-size: 12px">
        <div>
            <asp:UpdatePanel ID="UpdPnlFilter" runat="server" >
                <ContentTemplate>
                    <table style="width: 100%; margin-top: 10px;">
                        <tr>
                            <td style="width: 30%;" class="row-header">
                                Vetting Details
                                <asp:Button ID="btnCreateVetting" runat="server" OnClick="btnCreateVetting_Click"
                                    Style="visibility: hidden;" />
                            </td>
                            <td style="width: 80%; padding-right: 3px;" align="right">
                                <table style="height: 20px;">
                                    <tr>
                                        <td style="width: 10px;">
                                            <asp:Label ID="Label1" runat="server" Text="Status:" Width="50px"></asp:Label>
                                        </td>
                                        <td style="width: 10px;" align="left" class="selectlist">
                                            <%-- <asp:Label ID="lblVettingStatus" runat="server" Text="InProgress"  Width="80px"  ></asp:Label>--%>
                                            <asp:DropDownList ID="ddlVettingStatus" runat="server" Width="100px" OnSelectedIndexChanged="ddlVettingStatus_SelectedIndexChanged"
                                                CssClass="selectlist" AutoPostBack="true">
                                                <asp:ListItem Value="Planned" Text="Planned"></asp:ListItem>
                                                <asp:ListItem Value="In-Progress" Text="In-Progress"></asp:ListItem>
                                                <asp:ListItem Value="Completed" Text="Completed"></asp:ListItem>
                                                <asp:ListItem Value="Failed" Text="Failed"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 10px;" align="left">
                                            <asp:Label ID="lblRespondBy" runat="server" Text="Respond By:" Visible="False" Width="70px"
                                                Height="16px"></asp:Label>
                                            <asp:Label ID="lblValidUntil" runat="server" Text="Valid Untill:" Visible="false"
                                                Width="70px"></asp:Label>
                                            <asp:Label ID="lblVettingDate" runat="server" Text="Vetting Date:" Visible="false"
                                                Width="90px"></asp:Label>
                                        </td>
                                        <td style="width: 10px;" align="left">
                                            <asp:TextBox ID="txtRespondBy" runat="server" Visible="false" OnTextChanged="txtRespondBy_TextChanged"
                                                AutoPostBack="True" Width="100px"></asp:TextBox>
                                            <cc1:CalendarExtender ID="calResBy" runat="server" Enabled="True" TargetControlID="txtRespondBy"
                                                Format="dd/MM/yyyy">
                                            </cc1:CalendarExtender>
                                            <asp:TextBox ID="txtValidUntil" runat="server" Visible="false" AutoPostBack="True"
                                                OnTextChanged="txtValidUntil_TextChanged" Width="100px"></asp:TextBox>
                                            <cc1:CalendarExtender ID="CalValidUntil" runat="server" Enabled="True" TargetControlID="txtValidUntil"
                                                Format="dd/MM/yyyy">
                                            </cc1:CalendarExtender>
                                            <asp:TextBox ID="txtFValidUntil" runat="server" Visible="false" Enabled="false" AutoPostBack="True"
                                                Width="100px"></asp:TextBox>
                                            <cc1:CalendarExtender ID="CalFValidUntil" runat="server" Enabled="false" TargetControlID="txtFValidUntil"
                                                Format="dd/MM/yyyy">
                                            </cc1:CalendarExtender>
                                            <asp:TextBox ID="txtVettingDate" runat="server" Visible="false" AutoPostBack="True"
                                                Width="100px"></asp:TextBox>
                                            <cc1:CalendarExtender ID="CalVetDate" runat="server" Enabled="True" TargetControlID="txtVettingDate"
                                                Format="dd/MM/yyyy">
                                            </cc1:CalendarExtender>
                                        </td>
                                        <td style="width: 50px; vertical-align: middle;">
                                            <asp:ImageButton ID="ImgSaveStatus" runat="server" Height="18px" ImageUrl="~/Images/Save.png"
                                                OnClick="ImgSaveStatus_Click" ToolTip="Save" Visible="False" OnClientClick="return ValidateOnImgStatus()" />
                                            <asp:ImageButton ID="ImgCancelSave" runat="server" Height="18px" ImageUrl="~/Images/Clear.gif"
                                                OnClick="ImgCancelSave_Click" ToolTip="Cancel" Visible="False" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 30px; vertical-align: middle;" align="left">
                                <asp:ImageButton ID="ImgRework" runat="server" Height="18px" ImageUrl="~/Images/Rework-icon.png"
                                    OnClick="btnRework_Click" ToolTip="Rework" Visible="False" OnClientClick="return confirm('Are you sure you want to re-open the vetting?')" />
                                <asp:HiddenField ID="hdnStatus" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" style="border: 1px solid  #bfbfbf; height: 60px; background-color: White;
                                padding: 5px;">
                                <asp:Panel ID="pnlEdit" runat="server" Height="60px">
                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional" >
                                        <ContentTemplate>
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td style="height: 20px; width: 7%;">
                                                        <asp:Label ID="Label3" runat="server" Text="Vetting Name: "></asp:Label>
                                                    </td>
                                                    <td style="width: 10%; font-weight: bold; font-size: 11px;">
                                                        <asp:Label ID="lblVettingName" runat="server" CssClass="lbl"></asp:Label>
                                                    </td>
                                                    <td style="width: 7%;">
                                                        <asp:Label ID="Label4" runat="server" Text="Vessel Name: "></asp:Label>
                                                    </td>
                                                    <td style="width: 10%; font-weight: bold; font-size: 11px;">
                                                        <asp:Label ID="lblVesName" runat="server" CssClass="lbl"></asp:Label>
                                                    </td>
                                                    <td style="width: 5%;">
                                                        <asp:Label ID="Label5" runat="server" Text="Type : "></asp:Label>
                                                    </td>
                                                    <td style="width: 10%; font-weight: bold; font-size: 11px;">
                                                        <asp:Label ID="lblVetType" runat="server" CssClass="lbl"></asp:Label>
                                                    </td>
                                                    <td style="width: 7%;">
                                                        <asp:Label ID="Label6" runat="server" Text="Questionnaire : "></asp:Label>
                                                    </td>
                                                    <td align="left" style="width: 10%; font-weight: bold; font-size: 11px;">
                                                        <asp:Label ID="lblQuesnrName" runat="server" CssClass="lbl"></asp:Label>
                                                    </td>
                                                    <td style="width: 7%;">
                                                        <asp:Label ID="Label7" runat="server" Text="Inspector : "></asp:Label>
                                                    </td>
                                                    <td style="width: 8%; font-weight: bold; font-size: 11px;">
                                                        <asp:Label ID="lblInspName" runat="server" CssClass="lbl"></asp:Label>
                                                    </td>
                                                    <td rowspan="3" align="right" style="vertical-align: middle; width: 4%;">
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <asp:Button ID="BtnEdit" runat="server" Text="Edit" Width="100px" Enabled="False"
                                                                        OnClick="BtnEdit_Click" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="10">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 20px; width: 7%;">
                                                        <asp:Label ID="Label8" runat="server" Text="Oil Major: "></asp:Label>
                                                    </td>
                                                    <td style="width: 15%; font-weight: bold; font-size: 11px;">
                                                        <asp:Label ID="lblOM" runat="server" CssClass="lbl"></asp:Label>
                                                    </td>
                                                    <td style="width: 7%;">
                                                        <asp:Label ID="Label10" runat="server" Text="Port : "></asp:Label>
                                                    </td>
                                                    <td style="width: 8%; font-weight: bold; font-size: 11px;">
                                                        <asp:Label ID="lblPort" runat="server" CssClass="lbl"></asp:Label>
                                                    </td>
                                                    <td style="width: 7%;">
                                                        <asp:Label ID="Label12" runat="server" Text="Vetting Date : "></asp:Label>
                                                    </td>
                                                    <td style="width: 7%; font-weight: bold; font-size: 11px;">
                                                        <asp:Label ID="lblVetDate" runat="server" CssClass="lbl"></asp:Label>
                                                    </td>
                                                    <td style="width: 8%;">
                                                        <asp:Label ID="Label14" runat="server" Text="Number Of Days : "></asp:Label>
                                                    </td>
                                                    <td style="width: 7%; font-weight: bold; font-size: 11px;">
                                                        <asp:Label ID="lblNoOdDays" runat="server" CssClass="lbl"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </asp:Panel>
                                <asp:Panel ID="pnlSave" runat="server" Visible="false" Height="60px" Width="100%">
                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional"  >
                                        <ContentTemplate>
                                            <table style="width: 100%; background-color: White;">
                                                <tr>
                                                    <td style="height: 20px; width: 15%;" colspan="2">
                                                        <asp:Label ID="Label9" runat="server" Text="Vetting Name: "></asp:Label>
                                                    </td>
                                                    <td style="width: 3%;">
                                                        <asp:Label ID="lblSVetName" runat="server" CssClass="lbl"></asp:Label>
                                                    </td>
                                                    <td style="width: 20%;" colspan="2">
                                                        <asp:Label ID="Label13" runat="server" Text="Vessel Name: "></asp:Label>
                                                    </td>
                                                    <td style="width: 5%;">
                                                        <asp:Label ID="lblSVesName" runat="server" CssClass="lbl"></asp:Label>
                                                    </td>
                                                    <td colspan="2">
                                                        <asp:Label ID="Label17" runat="server" Text="Type : "></asp:Label>
                                                    </td>
                                                    <td style="width: 3%;">
                                                        <asp:Label ID="lblSVetType" runat="server" CssClass="lbl"></asp:Label>
                                                    </td>
                                                    <td style="width: 8%;">
                                                        <asp:Label ID="Label19" runat="server" Text="Questionnaire : "></asp:Label>
                                                    </td>
                                                    <td style="vertical-align: middle; width: 1%;">
                                                        <b style="color: Red">*</b>
                                                    </td>
                                                    <td style="width: 3%;">
                                                        <asp:DropDownList ID="DDLQuestionnaire" runat="server" Width="120px">
                                                        </asp:DropDownList>
                                                        <asp:HiddenField ID="hdnQuestionnaireID" runat="server" />
                                                    </td>
                                                    <td style="width: 8%;">
                                                        <asp:Label ID="Label21" runat="server" Text="Inspector : "></asp:Label>
                                                    </td>
                                                    <td style="vertical-align: middle; width: 1%;">
                                                        <asp:Label ID="lblInspMandate" runat="server" Text="*" ForeColor="Red"></asp:Label>
                                                    </td>
                                                    <td style="width: 12%;">
                                                        <asp:DropDownList ID="DDLInspector" runat="server" Width="120px">
                                                        </asp:DropDownList>
                                                        <asp:HiddenField ID="hdnInsptrID" runat="server" />
                                                    </td>
                                                    <td rowspan="3" style="vertical-align: middle; width: 3%;" align="right">
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <asp:Button ID="BtnSave" OnClientClick="return ValidateOnBtnSave()" runat="server"
                                                                        Text="Save" Width="100px" OnClick="BtnSave_Click" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Button ID="BtnCancel" runat="server" Text="Cancel" Width="100px" OnClick="BtnCancel_Click" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="15">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 20px; width: 7%;">
                                                        <asp:Label ID="Label23" runat="server" Text="Oil Major: "></asp:Label>
                                                    </td>
                                                    <td style="vertical-align: middle; width: 1%;">
                                                       
                                                    </td>
                                                    <td style="width: 10%;">
                                                        <asp:DropDownList ID="DDLOilMajor" runat="server" Width="150px">
                                                        </asp:DropDownList>
                                                        <asp:HiddenField ID="hdnOMID" runat="server" />
                                                    </td>
                                                    <td style="width: 8%;">
                                                        <asp:Label ID="Label25" runat="server" Text="Port : "></asp:Label>
                                                    </td>
                                                    <td style="vertical-align: middle; width: 1%;">
                                                      
                                                    </td>
                                                    <td style="width: 10%;">
                                                        <asp:DropDownList ID="DDLPort" runat="server" Width="150px">
                                                        </asp:DropDownList>
                                                        <asp:HiddenField ID="hdnPortID" runat="server" />
                                                    </td>
                                                    <td style="width: 10%;">
                                                        <asp:Label ID="Label27" runat="server" Text="Vetting Date : "></asp:Label>
                                                    </td>
                                                    <td style="vertical-align: middle; width: 1%;">
                                                        <b style="color: Red">*</b>
                                                    </td>
                                                    <td style="width: 10%;">
                                                        <asp:TextBox ID="txtVetDate" runat="server" CssClass="txtInput" EnableViewState="true"
                                                            Font-Size="11px" Width="120px"></asp:TextBox>
                                                        <ajaxToolkit:CalendarExtender ID="txtVetDate_CalendarExtender" runat="server" Format="dd-MM-yyyy"
                                                            TargetControlID="txtVetDate">
                                                        </ajaxToolkit:CalendarExtender>
                                                    </td>
                                                    <td style="width: 10%;">
                                                        <asp:Label ID="Label29" runat="server" Text="Number Of Days : "></asp:Label>
                                                    </td>
                                                    <td style="vertical-align: middle; width: 1%;">
                                                        <asp:Label ID="lblNOD" runat="server" Text="*" ForeColor="Red"></asp:Label>
                                                    </td>
                                                    <td style="width: 3%;">
                                                        <asp:TextBox ID="txtNoOfDays" runat="server"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </asp:Panel>
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div>
            <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                <ContentTemplate>
                    <asp:Panel ID="pnlPerformVet" runat="server" Height="400px">
                        
                        <table align="center">
                            <tr>
                                <td style="width: 100%;" align="center">
                                    <table style="width: 100%;">
                                       <tr>
                                       <td style="height:20px;"></td>
                                       </tr>
                                        <tr>
                                            <td align="center">
                                            <asp:Button ID="btnPerVetting" runat="server"   Style="display: block;" ToolTip="Perform Vetting" Text="Perform Vetting" CssClass="bigblue"/>
                                         
                                            </td>
                                        </tr>
                                      
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
            <div>
                <asp:UpdatePanel ID="updObsJobs" runat="server">
                    <ContentTemplate>
                        <asp:Panel ID="PnlObsJobs" runat="server" Visible="false">
                            <table style="width: 100%;">
                                <tr>
                                    <td style="width: 70%" class="row-header">
                                        Observation and Jobs
                                    </td>
                                    <td style="width: 30%;">
                                        <table align="right">
                                            <tr>
                                                <td style="vertical-align: bottom;">
                                                    <asp:ImageButton ID="btnPdfExport" runat="server" CommandArgument="ExportFrom_IE"
                                                        Height="18px" ImageUrl="~/Images/VET_PdfExport.png" OnClick="btnPdfExport_Click"
                                                        Style="display: block;" ToolTip="Export to PDF" />
                                                </td>
                                                <td style="vertical-align: bottom;">
                                                    <asp:ImageButton ID="btnExport" runat="server" CommandArgument="ExportFrom_IE" Height="20px"
                                                        ImageUrl="~/Images/XLS.jpg" OnClick="btnExport_Click" Style="display: block;"
                                                        ToolTip="Export to Excel" />
                                                </td>
                                                <td style="vertical-align: bottom;">
                                                    <asp:ImageButton ID="btnMacExport" runat="server" CommandArgument="ExportFrom_MAC"
                                                        Height="20px" ImageUrl="~/Images/Export-mac.png" OnClick="btnExport_Click" Style="display: none;"
                                                        ToolTip="Export to Excel unformatted" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="border: 1px solid  #bfbfbf; background-color: White;">
                                        <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
                                            <ContentTemplate>
                                                <asp:Panel ID="pnlFilter" runat="server" DefaultButton="btnRetrieve">
                                                    <div style="color: Black; padding: 10px 1%; width: 98%;">
                                                        <table cellpadding="0" cellspacing="0" width="100%" border="0">
                                                            <tr>
                                                                <td align="left" style="vertical-align: top; width: 100px;">
                                                                    Section : &nbsp;
                                                                </td>
                                                                <td align="left" style="vertical-align: top; width: 120px;">
                                                                    <CustomFilter:ucfDropdown ID="DDLSection" runat="server" UseInHeader="false" Width="160"
                                                                        Height="200" />
                                                                </td>
                                                                <td align="left" style="vertical-align: top; width: 100px;">
                                                                    Question : &nbsp;
                                                                </td>
                                                                <td align="left" style="vertical-align: top; width: 120px;">
                                                                    <CustomFilter:ucfDropdown ID="DDLQuestion" runat="server" UseInHeader="false" Width="160"
                                                                        Height="200" />
                                                                </td>
                                                                <td align="left" style="vertical-align: top; width: 100px;">
                                                                    Type : &nbsp;
                                                                </td>
                                                                <td align="left" style="vertical-align: top; width: 120px;">
                                                                    <CustomFilter:ucfDropdown ID="DDLObsType" runat="server" Height="200" UseInHeader="false"
                                                                        Width="160" />
                                                                </td>
                                                                <td align="right" style="vertical-align: top; width: 200px;">
                                                                    <table align="right">
                                                                        <tr>
                                                                            <td>
                                                                                <asp:UpdatePanel ID="UpdatePanel5" UpdateMode="Conditional" runat="server">
                                                                                    <ContentTemplate>
                                                                                        <asp:ImageButton ID="btnRetrieve" runat="server" Height="25px" ImageAlign="AbsBottom"
                                                                                            ImageUrl="~/Images/SearchButton.png" OnClick="btnRetrieve_Click" ToolTip="Search"
                                                                                            OnClientClick="return ValidateFilter()" />
                                                                                    </ContentTemplate>
                                                                                </asp:UpdatePanel>
                                                                            </td>
                                                                            <td>
                                                                                <asp:ImageButton ID="btnClearFilter" runat="server" Height="25px" ImageUrl="~/Images/filter-delete-icon.png"
                                                                                    ToolTip="Clear Filter" OnClick="btnClearFilter_Click" />
                                                                            </td>
                                                                            <td style="font-size: 11px; line-height: 25px; vertical-align: top">
                                                                                <%--  <asp:LinkButton ID="lnkAdvText" runat="server" OnClientClick="toggleAdvSearch(this)">Open Advance Filter</asp:LinkButton>--%>
                                                                                <a id="advText" href="#" onclick="toggleAdvSearch(this)">Open Advance Filter</a>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left" style="vertical-align: top;">
                                                                    Job Status : &nbsp;
                                                                </td>
                                                                <td align="left" style="vertical-align: top;">
                                                                    <CustomFilter:ucfDropdown ID="DDLJobStatus" runat="server" Height="200" UseInHeader="false"
                                                                        Width="160" />
                                                                </td>
                                                                <td align="left" style="vertical-align: top;">
                                                                    Observation Status : &nbsp;
                                                                </td>
                                                                <td align="left" style="vertical-align: top;" >
                                                                    <CustomFilter:ucfDropdown ID="DDLObsStatus" runat="server" Height="200" UseInHeader="false"
                                                                        Width="160" />
                                                                </td>
                                                                <td colspan="3">
                                                                  
                                                                    <div style="padding: 5px 0px; overflow: hidden;">
                                            <asp:UpdatePanel ID="UpdAddObs" UpdateMode="Conditional" runat="server">
                                                <ContentTemplate>
                                                    <table align="right">
                                                        <tr>
                                                          <td>
                                                                   <asp:UpdatePanel ID="UpdatePanel22" runat="server">
                                                                            <ContentTemplate>                                                            
                                                                             
                                                                                <asp:ImageButton ID="btnForce_Download" runat="server" ImageUrl="~/Images/Download_Errorlog.png"
                                                                                    ToolTip="Force download" OnClick="btnForce_Download_Click" Visible="false"  />                                                                             
                                                                            </ContentTemplate>
                                                                            <Triggers>
                                                                               
                                                                                <asp:PostBackTrigger ControlID="btnForce_Download" />
                                                                            </Triggers>
                                                                        </asp:UpdatePanel>
                                                            </td>
                                                            <td>
                                                                <asp:ImageButton ID="ImgImportObs" runat="server" ImageAlign="Right" ForeColor="Black" Visible="false"
                                                                    ToolTip="Import XML File" ImageUrl="~/Images/VET_ImportObs.png" >
                                                                </asp:ImageButton>
                                                            
                                                            </td>
                                                            <td>
                                                                <asp:ImageButton ID="ImgRemark" runat="server" ImageAlign="Right" ForeColor="Black"
                                                                    ToolTip="Add Remark" ImageUrl="~/Images/VET_Summary.png"></asp:ImageButton>
                                                            </td>
                                                            <td>
                                                                <asp:ImageButton ID="ImgVetAttchement" runat="server" ImageAlign="Right" ForeColor="Black"
                                                                    ToolTip="Add Attachment" ImageUrl="~/Images/VET_Attach.png"></asp:ImageButton>
                                                            </td>
                                                            <td>
                                                                 <asp:Button  ID="ImgAddNewObs" runat="server"   ToolTip="Add Observation"  Text="Add Observation" style="float: right; font-size: 11px;" />
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
                                                </asp:Panel>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                            <ContentTemplate>
                                                <div style="padding: 0px 1%">
                                                    <asp:HiddenField ID="hfAdv" runat="server" Value="c" />
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <div id="dvAdvanceFilter" align="left" class="hide" style="padding: 0px 1%">
                                            <table border="0" cellpadding="1" cellspacing="1" style="width: 30%; background-color: #efefef;">
                                                <tr>
                                                    <td valign="top" style="border: 1px solid #aabbdd; width: 200px;">
                                                        <table border="0" cellpadding="2" cellspacing="1" width="100%">
                                                            <tr style="background-color: #aabbdd;">
                                                                <td style="text-align: left; font-weight: bold;" colspan="2">
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="text-align: left;">
                                                                    Category
                                                                </td>
                                                                <td style="text-align: left;">
                                                                    <asp:UpdatePanel ID="updCategory" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                                                        <ContentTemplate>
                                                                            <CustomFilter:ucfDropdown ID="DDLCategory" runat="server" Height="200" UseInHeader="false"
                                                                                Width="160" />
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="text-align: left;">
                                                                    Risk Level
                                                                </td>
                                                                <td style="text-align: left;">
                                                                    <asp:UpdatePanel ID="updRiskLevel" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                                                        <ContentTemplate>
                                                                            <CustomFilter:ucfDropdown ID="DDLRiskLevel" runat="server" Height="200" UseInHeader="false"
                                                                                Width="160" />
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        
                                        <div style="width: 98%; height: 400px; margin: 0px 1%; background-color: White; overflow-y: auto;">
                                            <asp:UpdatePanel ID="UpdPnlGrid" UpdateMode="Conditional" runat="server">
                                                <ContentTemplate>
                                                    <div>
                                                        <asp:GridView ID="gvObs" runat="server" EmptyDataText="NO RECORDS FOUND" AutoGenerateColumns="false"
                                                            CellPadding="2" ShowHeaderWhenEmpty="true" OnRowDataBound="gvObs_RowDataBound"
                                                            Width="99.9%" CssClass="gridmain-css" GridLines="None" OnRowCreated="gvObs_RowCreated"
                                                            OnRowCommand="gvObs_RowCommand">
                                                            <HeaderStyle CssClass="HeaderStyle-css" />
                                                            <RowStyle CssClass="RowStyle-css" Height="20px" />
                                                            <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                                            <SelectedRowStyle BackColor="#FFFFCC" />
                                                            <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Section">
                                                                    <HeaderTemplate>
                                                                        Section
                                                                    </HeaderTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSection" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Section_No") %>'
                                                                            onmouseover='<%#"BindQuestions(&#39;"+ Eval("Questionnaire_ID").ToString() +"&#39;,&#39;"+Eval("Section_No").ToString()+"&#39;,0,event,this);" %>'                                                                          
                                                                            ></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="QuestionID" Visible="false">
                                                                    <HeaderTemplate>
                                                                        QuestionID
                                                                    </HeaderTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblQuestionID" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Question_ID") %>'></asp:Label>
                                                                        <asp:HiddenField ID="hdnFleetCode" runat="server" Value='<%# DataBinder.Eval(Container,"DataItem.FleetCode") %>' />
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Question">
                                                                    <HeaderTemplate>
                                                                        Question
                                                                    </HeaderTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblQuestion" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Question_No") %>'
                                                                            onmouseover='<%#"BindQuestions(&#39;"+ Eval("Questionnaire_ID").ToString() +"&#39;,&#39;"+Eval("Section_No").ToString()+"&#39;,&#39;"+Eval("Question_ID").ToString()+"&#39;,event,this);" %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="OBSID" Visible="false">
                                                                    <HeaderTemplate>
                                                                        OBSID
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblOBSID" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Observation_ID") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Type">
                                                                    <HeaderTemplate>
                                                                        Type
                                                                    </HeaderTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblOBSType" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Observation_Type_Name") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Content">
                                                                    <HeaderTemplate>
                                                                        Content
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblOBSDesc" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Description") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="300px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Status">
                                                                    <HeaderTemplate>
                                                                        Status
                                                                    </HeaderTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lnkOBSStatus" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Status") %>'
                                                                            CommandName="Status"></asp:LinkButton>
                                                                        <asp:DropDownList ID="ddlOBSStatus" runat="server" OnSelectedIndexChanged="ddlOBSStatus_SelectedIndexChanged"
                                                                            Visible="false" AutoPostBack="true">
                                                                            <asp:ListItem Value="Open" Text="Open"></asp:ListItem>
                                                                            <asp:ListItem Value="Closed" Text="Closed"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <asp:HiddenField ID="hdnObsID" runat="server" Value='<%# DataBinder.Eval(Container,"DataItem.Observation_ID") %>' />
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Category">
                                                                    <HeaderTemplate>
                                                                        Category
                                                                    </HeaderTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblOBS_Category" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.OBSCategory_Name") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Risk Level">
                                                                    <HeaderTemplate>
                                                                        Risk Level
                                                                    </HeaderTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblRiskLevel" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Risk_Level") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="New">
                                                                    <HeaderTemplate>
                                                                        New
                                                                    </HeaderTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <ItemTemplate>
                                                                        <asp:HyperLink ID="ImgNewJob" runat="server" Text="Select" Height="12px" ForeColor="Black"
                                                                            ToolTip="Add New Job" Target="_blank" ImageUrl="~/Images/VET_NewJOb.png" CssClass="ihyperlink"></asp:HyperLink>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Assign">
                                                                    <HeaderTemplate>
                                                                        Assign
                                                                    </HeaderTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="ImgAssignJob" ImageUrl="~/Images/VET_AssignJob.png" Width="12px"
                                                                            Height="12px" runat="server" rel='<%#Eval("Observation_ID").ToString() %>' ToolTip="Assign Job" />
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Pending">
                                                                    <HeaderTemplate>
                                                                        Pending
                                                                    </HeaderTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lnkPenJobsCnt" runat="server" OnClientClick='<%#"BindObsPendingJobs(&#39;"+ Eval("Vetting_ID").ToString() +"&#39;,&#39;"+Eval("Question_ID").ToString()+"&#39;,&#39;"+Eval("Observation_ID").ToString()+"&#39;,event,this);" %>'></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Completed">
                                                                    <HeaderTemplate>
                                                                        Completed
                                                                    </HeaderTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lnkCompletedJobsCnt" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.CompletedJobs") %>'
                                                                            OnClientClick='<%#"BindObsCompletedJobs(&#39;"+ Eval("Vetting_ID").ToString() +"&#39;,&#39;"+Eval("Question_ID").ToString()+"&#39;,&#39;"+Eval("Observation_ID").ToString()+"&#39;,event,this);" %>'></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Verified">
                                                                    <HeaderTemplate>
                                                                        Verified
                                                                    </HeaderTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lnkVerJobsCnt" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.VerifiedJobs") %>'
                                                                            OnClientClick='<%#"BindObsVerifiedJobs(&#39;"+ Eval("Vetting_ID").ToString() +"&#39;,&#39;"+Eval("Question_ID").ToString()+"&#39;,&#39;"+Eval("Observation_ID").ToString()+"&#39;,event,this);" %>'></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Deferred">
                                                                    <HeaderTemplate>
                                                                        Deferred
                                                                    </HeaderTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lnkDefJobsCnt" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.DeferredJobs") %>'
                                                                            OnClientClick='<%#"BindObsDeferredJobs(&#39;"+ Eval("Vetting_ID").ToString() +"&#39;,&#39;"+Eval("Question_ID").ToString()+"&#39;,&#39;"+Eval("Observation_ID").ToString()+"&#39;,event,this);" %>'></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Response">
                                                                    <HeaderTemplate>
                                                                        Response
                                                                    </HeaderTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblResponseCnt" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.ResponseCnt") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Action">
                                                                    <HeaderTemplate>
                                                                        Action
                                                                    </HeaderTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <ItemTemplate>
                                                                        <table>
                                                                            <tr>
                                                                                <td style="border-bottom: 0px; width: 35px;">
                                                                                    <div class="badge" runat="server" id="dvRJobs" >
                                                                                        <div>
                                                                                            <asp:HyperLink ID="lnkRelatedObs" runat="server" ToolTip="Observations" Height="12px"
                                                                                                ForeColor="White" Target="_blank"></asp:HyperLink>
                                                                                        </div>
                                                                                    </div>
                                                                                </td>
                                                                                <td style="border-bottom: 0px; width: 18px;">
                                                                                    <asp:ImageButton ID="ImgEdit" ImageUrl="~/Images/Edit.gif" Width="16px" Height="16px"
                                                                                        runat="server" />
                                                                                </td>
                                                                                <td style="border-bottom: 0px; width: 18px;">
                                                                                    <asp:ImageButton ID="ImgDelete" ImageUrl="~/Images/delete.png" Width="16px" Height="16px"
                                                                                        runat="server" OnClientClick="return confirm('Are you sure you want to delete the observation and all its related jobs and responses?')"
                                                                                        OnCommand="ImgDelete_Click" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.Observation_ID")+","+DataBinder.Eval(Container,"DataItem.Question_ID")+","+DataBinder.Eval(Container,"DataItem.Vetting_ID") %>' />
                                                                                </td>                                                                             
                                                                            </tr>
                                                                        </table>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                    <asp:HiddenField ID="hdfObsCount" runat="server" />
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:PostBackTrigger ControlID="btnExport" />
                                                    <asp:PostBackTrigger ControlID="btnMacExport" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div id="dvNewObs" style="display: none; width: 1300px;" title=''>
                <iframe id="iFrmNewObs" src="" frameborder="0" style="height:785px; width: 100%">
                </iframe>
            </div>
            <div id="divVetAttach" style="display: none; width: 550px;" title=''>
                <iframe id="iFrmVetAttach" src="" frameborder="0" style="height: 320px; width: 550px;">
                </iframe>
            </div>
            <div id="divVetRemark" style="display: none; width: 830px;" title=''>
                <iframe id="iFrmVetRemark" src="" frameborder="0" style="height: 300px; width: 830px;">
                </iframe>
            </div>
            <div id="divAssignJobs" style="display: none; width: 1200px;" title=''>
                <iframe id="iFrmAssignJobs" src="" frameborder="0" style="height: 500px; width: 1200px;">
                </iframe>
            </div>
            <div id="divPerVet" style="display: none; width: 650px;" title=''>
                <iframe id="iFrmPerVet" src="" frameborder="0" style="height: 420px; width: 650px;">
                </iframe>
            </div>
        </div>
        <div style="position: relative; bottom: 0px;">
            <asp:Panel ID="pnlFooter" runat="server">
                <table style="width: 100%">
                    <tr>
                        <td style="width: 50%;" align="left">
                            <div id="dvRecordInformation" style="text-align: left; width: 100%; background-color: #f1f4f5" align="left">
                            </div>
                        </td>
                        <td style="width: 50%;" align="right">
                            <div id="dvVETCrewInformation" style="text-align: left; width: 100%;  background-color: #f1f4f5"
                                align="right">
                            </div>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </div>
        <div id="dvPortCall" style="display: none; width: 815px;" title="Port">
            <iframe id="IframePortCall" src="" frameborder="0" style="height: 250px; width: 815px;">
            </iframe>
        </div>
    </div>
    <asp:HiddenField ID="hdfVettingID" runat="server" />
   
      <div id="divImportObs" style="display: none; width: 500px; height: 100px;" title="Import Observation">
        <table width="100%" style="padding: 10px;">
            <tr id="Tr1" runat="server">
                <td align="left" style="font-weight: bold;">
                    <asp:Label ID="lblXML" runat="server" Text="Import XML Report :"> </asp:Label>
                </td>
                <td>
                </td>
                <td>
                    <asp:FileUpload ID="FileUpXMLReport" runat="server" Width="255px" Height="23px" />
                    <asp:HiddenField ID="GUIDSession"  runat="server"/>
                </td>
                <td>
                    <asp:Button ID="btnUpload" runat="server" Text="Upload" Width="80px" OnClick="btnUpload_Click" />
                          <asp:Button ID="btnconfirm" runat="server"  Width="150px" style="display:none"
                                Height="25px" OnClick="btnconfirm_Click" />
                </td>
            </tr>
        </table>
    </div>
  
  
   
</asp:Content>
