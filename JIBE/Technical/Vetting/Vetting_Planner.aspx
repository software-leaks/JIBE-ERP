<%@ Page Title="Vetting Planner" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Vetting_Planner.aspx.cs" Inherits="Technical_Vetting_Vetting_Planner" %>

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
    <link href="../../Styles/CssCustomFilterControls.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery.min.js" type="text/javascript"></script>
    <link href="../../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/ModalPopUp.js" type="text/jscript"></script>
    <script src="../../Scripts/Common_Functions.js" type="text/jscript"></script>
    <script src="../../Scripts/JSCustomFilterControls.js" type="text/javascript"></script>
    <script src="../../Scripts/ucCustomDropDownList.js" type="text/javascript"></script>
    <script lang="javascript" type="text/javascript">
        $(document).ready(function () {

            var strDateFormat = '<%= strDateFormat %>';
            $("body").on("click", "#" + $('[id$=btnRetrieve]').attr('id'), function () {
                ;
                if ($.trim($("#" + $('[id$=txtLVetFromDate]').attr('id')).val()) != "") {
                    if (IsInvalidDate($.trim($("#" + $('[id$=txtLVetFromDate]').attr('id')).val()), strDateFormat)) {
                        alert("Enter valid Date<%= UDFLib.DateFormatMessage() %>");
                        $("#" + $('[id$=txtLVetFromDate]').attr('id')).focus();
                        return false;
                    }
                }
                if ($.trim($("#" + $('[id$=txtLVetToDate]').attr('id')).val()) != "") {
                    if (IsInvalidDate($.trim($("#" + $('[id$=txtLVetToDate]').attr('id')).val()), strDateFormat)) {
                        alert("Enter valid Date<%= UDFLib.DateFormatMessage() %>");
                        $("#" + $('[id$=txtLVetToDate]').attr('id')).focus();
                        return false;
                    }
                }
                if ($.trim($("#" + $('[id$=txtEVetFromDate]').attr('id')).val()) != "") {
                    if (IsInvalidDate($.trim($("#" + $('[id$=txtEVetFromDate]').attr('id')).val()), strDateFormat)) {
                        alert("Enter valid Date<%= UDFLib.DateFormatMessage() %>");
                        $("#" + $('[id$=txtEVetFromDate]').attr('id')).focus();
                        return false;
                    }
                }
                if ($.trim($("#" + $('[id$=txtEVetToDate]').attr('id')).val()) != "") {
                    if (IsInvalidDate($.trim($("#" + $('[id$=txtEVetToDate]').attr('id')).val()), strDateFormat)) {
                        alert("Enter valid Date<%= UDFLib.DateFormatMessage() %>");
                        $("#" + $('[id$=txtEVetToDate]').attr('id')).focus();
                        return false;
                    }
                }
                if ($.trim($("#" + $('[id$=txtPVetDateFromDate]').attr('id')).val()) != "") {
                    if (IsInvalidDate($.trim($("#" + $('[id$=txtPVetDateFromDate]').attr('id')).val()), strDateFormat)) {
                        alert("Enter valid Date<%= UDFLib.DateFormatMessage() %>");
                        $("#" + $('[id$=txtPVetDateFromDate]').attr('id')).focus();
                        return false;
                    }
                }
                if ($.trim($("#" + $('[id$=txtPVetDateToDate]').attr('id')).val()) != "") {
                    if (IsInvalidDate($.trim($("#" + $('[id$=txtPVetDateToDate]').attr('id')).val()), strDateFormat)) {
                        alert("Enter valid Date<%= UDFLib.DateFormatMessage() %>");
                        $("#" + $('[id$=txtPVetDateToDate]').attr('id')).focus();
                        return false;
                    }
                }
            });

            $("body").on("click", "#" + $('[id$=btnCRetrieve]').attr('id'), function () {
                if ($.trim($("#" + $('[id$=txtCLVetFromDate]').attr('id')).val()) != "") {
                    if (IsInvalidDate($.trim($("#" + $('[id$=txtCLVetFromDate]').attr('id')).val()), strDateFormat)) {
                        alert("Enter valid Date<%= UDFLib.DateFormatMessage() %>");
                        $("#" + $('[id$=txtCLVetFromDate]').attr('id')).focus();
                        return false;
                    }
                }
                if ($.trim($("#" + $('[id$=txtCLVetToDate]').attr('id')).val()) != "") {
                    if (IsInvalidDate($.trim($("#" + $('[id$=txtCLVetToDate]').attr('id')).val()), strDateFormat)) {
                        alert("Enter valid Date<%= UDFLib.DateFormatMessage() %>");
                        $("#" + $('[id$=txtCLVetToDate]').attr('id')).focus();
                        return false;
                    }
                }
                if ($.trim($("#" + $('[id$=txtCEVetFromDate]').attr('id')).val()) != "") {
                    if (IsInvalidDate($.trim($("#" + $('[id$=txtCEVetFromDate]').attr('id')).val()), strDateFormat)) {
                        alert("Enter valid Date<%= UDFLib.DateFormatMessage() %>");
                        $("#" + $('[id$=txtCEVetFromDate]').attr('id')).focus();
                        return false;
                    }
                }
                if ($.trim($("#" + $('[id$=txtCEVetToDate]').attr('id')).val()) != "") {
                    if (IsInvalidDate($.trim($("#" + $('[id$=txtCEVetToDate]').attr('id')).val()), strDateFormat)) {
                        alert("Enter valid Date<%= UDFLib.DateFormatMessage() %>");
                        $("#" + $('[id$=txtCEVetToDate]').attr('id')).focus();
                        return false;
                    }
                }
                if ($.trim($("#" + $('[id$=txtCPVetFromDate]').attr('id')).val()) != "") {
                    if (IsInvalidDate($.trim($("#" + $('[id$=txtCPVetFromDate]').attr('id')).val()), strDateFormat)) {
                        alert("Enter valid Date<%= UDFLib.DateFormatMessage() %>");
                        $("#" + $('[id$=txtCPVetFromDate]').attr('id')).focus();
                        return false;
                    }
                }
                if ($.trim($("#" + $('[id$=txtCPVetToDate]').attr('id')).val()) != "") {
                    if (IsInvalidDate($.trim($("#" + $('[id$=txtCPVetToDate]').attr('id')).val()), strDateFormat)) {
                        alert("Enter valid Date<%= UDFLib.DateFormatMessage() %>");
                        $("#" + $('[id$=txtCPVetToDate]').attr('id')).focus();
                        return false;
                    }
                }
            });
        });
     
        function CreateNewVetting(VesselID, TypeID, Date, Questionnaire, OilMajor, Inspector, Port, PortCallID, NoDays, RespNextDue) {

            document.getElementById('iFrmNewVetting').src = "Vetting_CreateNewVetting.aspx?VesselID=" + VesselID + "&Vetting_Type_ID=" + TypeID + "&Date=" + Date + "&Questionnaire=" + Questionnaire + "&OilMajor=" + OilMajor + "&Inspector=" + Inspector + "&Port=" + Port + "&PortCallID=" + PortCallID + "&NoDays=" + NoDays + "&RespNextDue=" + RespNextDue;
            $("#dvNewVetting").prop('title', 'Create New Vetting ');
            showModal('dvNewVetting');
            return false;

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

        function toggleSerachPostBack() {
            if ($("#<%= hfAdv.ClientID %>").val() == "o") {
                $("#dvAdvanceFilter").show();
                $("#advText").text("Close Advance Filter");
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
        function toggleCAdvSearch(obj) {


            if ($(obj).text() == "Open Advance Filter") {
                $(obj).text("Close Advance Filter");
                $("#dvCAdvanceFilter").show();
            }
            else {
                $(obj).text("Open Advance Filter");
                $("#dvCAdvanceFilter").hide();
            }

            if ($("#<%= hfCAdv.ClientID %>").val() == "c") {
                $("#<%= hfCAdv.ClientID %>").val('o');
            }
            else {
                $("#<%= hfCAdv.ClientID %>").val('c');
            }
        }

        function toggleCSerachPostBack() {
            if ($("#<%= hfCAdv.ClientID %>").val() == "o") {
                $("#dvCAdvanceFilter").show();
                $("#advCText").text("Close Advance Filter");
            }

        }
        function toggleCOnSearchClearFilter(obj, objval) {


            if (objval == 'o') {
                $(obj).text("Close Advance Filter");
                $("#dvCAdvanceFilter").show();
            }
            else {
                $("#advCText").text("Open Advance Filter");
                $("#dvCAdvanceFilter").hide();
            }


        }
        function searchKeyPress(e) {
            // look for window.event in case event isn't passed in
            if (e.keyCode == 13) {
                document.getElementById($('[id$=btnRetrieve]').attr('id')).click();
                return false;
            }
            return true;
        }
        function getOperatingSystem() {
            var OSName = "";
            var Browser = "";

            if (navigator.appVersion.indexOf("Win") != -1) OSName = "Windows";
            if (navigator.appVersion.indexOf("Mac") != -1) OSName = "MacOS";
            if (navigator.appVersion.indexOf("X11") != -1) OSName = "UNIX";
            if (navigator.appVersion.indexOf("Linux") != -1) OSName = "Linux";

            if (OSName == "MacOS") {
                document.getElementById($('[id$=btnExport]').attr('id')).style.display = "none";
                document.getElementById($('[id$=btnCExport]').attr('id')).style.display = "none";
            }

            if (OSName == "Windows") {
                document.getElementById($('[id$=btnMacExport]').attr('id')).style.display = "none";
                document.getElementById($('[id$=btnCMacExport]').attr('id')).style.display = "none";
            }

        }
      

        function VettingDetail(Vetting_ID) {
            hideModal("dvNewVetting");
            window.open("Vetting_Details.aspx?Vetting_Id=" + Vetting_ID);
        }

        function PortCall(VesselID, TypeID, Date, Questionnaire, OilMajor, Inspector, Port, NoDays, RespNextDue) {
            document.getElementById('IframePortCall').src = "Vetting_PortCall.aspx?VesselID=" + VesselID + "&TypeID=" + TypeID + "&Date=" + Date + "&Questionnaire=" + Questionnaire + "&OilMajor=" + OilMajor + "&Inspector=" + Inspector + "&Port=" + Port + "&NoDays=" + NoDays + "&RespNextDue=" + RespNextDue; ;
            $("#dvPortCall").prop('title', 'Port');
            showModal('dvPortCall', false);
            return false;
        }


        function HidePortCall(VesselID, TypeID, Date, Questionnaire, OilMajor, Inspector, Port, PortCallID, NoDays, RespNextDue) {
            hideModal("dvPortCall");
            CreateNewVetting(VesselID, TypeID, Date, Questionnaire, OilMajor, Inspector, Port, PortCallID, NoDays, RespNextDue);
        }
    </script>
    <style type="text/css">
        .GroupHeaderStyle-css
       {
   
            text-align: center;
          
        }
        .ajax__tab_xp .ajax__tab_body
        {
            padding: 0px !important;
        }
        .page
        {
            min-width: 100%;
        }
        .chkbox
        {
            position: absolute;
            margin-top: -5px;
        }
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
        .awesomelnkButton
        {
            border: 1px solid #2980B9;
            display: inline-block;
            cursor: pointer;
            background: #FFF;
            color: #3498DB;
            font-size: 14px;
            padding: 6px 8px;
            text-decoration: none;
            text-shadow: 0px 1px 0px #2980B9;
            margin-right: 5px;
            margin-bottom: 5px;
            border-radius: 20px;
        }
        .hide
        {
            display: none;
        }
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
        <asp:Label ID="lblPageTitle" runat="server" Font-Bold="true" Font-Size="14px"></asp:Label>
    </div>
    <div class="page-content" style="font-family: Tahoma; font-size: 12px">
        <ajaxToolkit:TabContainer ID="tbCntr" runat="server" Width="100%" 
            ActiveTabIndex="1">
            <ajaxToolkit:TabPanel ID="tbVetOverview" runat="server" Font-Names="Tahoma">
                <HeaderTemplate>
                    Vetting Overview
                
</HeaderTemplate>
                
<ContentTemplate>
                    <asp:Panel ID="Panel1" runat="server" DefaultButton="btnRetrieve">
                        <asp:UpdatePanel ID="UpdPnlFilter" UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                                <div style="color: Black; padding: 10px 1%">
                                    <table cellpadding="2" cellspacing="0" width="100%" border="0">
                                        <tr>
                                            <td align="left" style="vertical-align: top; width: 8%;">
                                                Fleet :
                                            </td>
                                            <td align="left" style="vertical-align: top; width: 10%;">
                                                <asp:DropDownList ID="DDLFleet" runat="server" AppendDataBoundItems="True" AutoPostBack="true"
                                                    Height="20px" OnSelectedIndexChanged="DDLFleet_SelectedIndexChanged" Width="160px">
                                                    <asp:ListItem Selected="True" Value="0">--Select All--</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td align="left" style="vertical-align: top; width: 8%;">
                                                Vessel :
                                            </td>
                                            <td align="left" style="vertical-align: top; width: 10%;">
                                                <CustomFilter:ucfDropdown ID="DDLVessel" runat="server" UseInHeader="false" Width="160"
                                                    Height="200" />
                                            </td>
                                            <td align="left" style="vertical-align: top; width: 8%;">
                                                Oil Majors :
                                            </td>
                                            <td align="left" style="vertical-align: top; width: 10%">
                                                <CustomFilter:ucfDropdown ID="DDLOilMajors" runat="server" Height="200" UseInHeader="false"
                                                    Width="160" />
                                            </td>
                                            <td style="vertical-align: bottom;" align="right" colspan="3">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:ImageButton ID="btnRetrieve" runat="server" Height="25px" ImageAlign="AbsBottom"
                                                                ImageUrl="~/Images/SearchButton.png" OnClick="btnRetrieve_Click" ToolTip="Search"
                                                                OnClientClick="return ValidateFilter()" />
                                                        </td>
                                                        <td>
                                                            <asp:ImageButton ID="btnClearFilter" runat="server" Height="24px" ImageUrl="~/Images/filter-delete-icon.png"
                                                                ToolTip="Clear Filter" OnClick="btnClearFilter_Click" />
                                                        </td>
                                                        <td>
                                                            <asp:ImageButton ID="btnExport" runat="server" CommandArgument="ExportFrom_IE" Height="25px"
                                                                ImageUrl="~/Images/XLS.jpg" OnClick="btnExport_Click" Style="display: block;"
                                                                ToolTip="Export to Excel" />
                                                            <asp:ImageButton ID="btnMacExport" runat="server" CommandArgument="ExportFrom_MAC"
                                                                Height="25px" ImageUrl="~/Images/Export-mac.png" OnClick="btnExport_Click" Style="display: none;"
                                                                ToolTip="Export to Excel unformatted" />
                                                        </td>
                                                        <td style="font-size: 11px; line-height: 25px; vertical-align: top">
                                                            <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server">
                                                                <ContentTemplate>
                                                                    <a id="advText" href="#" onclick="toggleAdvSearch(this)">Open Advance Filter</a>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                Vetting Type :
                                            </td>
                                            <td align="left">
                                                <CustomFilter:ucfDropdown ID="DDLVettingType" runat="server" Height="200" UseInHeader="false"
                                                    Width="160" />
                                            </td>
                                            <td align="left">
                                                Expires in Days :
                                            </td>
                                            <td align="left" >
                                                <asp:DropDownList ID="DDLExInDays" runat="server">
                                                    <asp:ListItem Value="0" Text="-- Select --"></asp:ListItem>
                                                    <asp:ListItem Value="15" Text="15"></asp:ListItem>
                                                    <asp:ListItem Value="30" Text="30"></asp:ListItem>
                                                    <asp:ListItem Value="45" Text="45"></asp:ListItem>
                                                    <asp:ListItem Value="60" Text="60"></asp:ListItem>
                                                    <asp:ListItem Value="90" Text="90"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td align="left">
                                                Planned Vetting :
                                            </td>
                                            <td align="left" style="padding-bottom: 2px;">
                                                <asp:RadioButtonList ID="rbtVetState" runat="server" RepeatDirection="Horizontal"
                                                    AutoPostBack="false">
                                                    <asp:ListItem Text="All" Value="0" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="No" Value="2"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                            <td align="left" width="10%">
                                                By Vessel assignment :
                                            </td>
                                            <td align="left" style="width: 2%;">
                                                <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                                    <ContentTemplate>
                                                        <asp:CheckBox ID="chkOVesselAssign" RepeatDirection="Horizontal" AutoPostBack="true"
                                                            Checked="true" runat="server" OnCheckedChanged="chkOVesselAssign_CheckedChanged">
                                                        </asp:CheckBox>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                            <td align="right">
                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                    <ContentTemplate>
                                                        <div style="padding: 8px 0%;">
                                                                <asp:Button ID="ImgAddNewVetting" runat="server"  Text="Create New Vetting" ToolTip="Create New Vetting"  style="float: right; font-size: 11px;"
                                                                 OnClientClick="CreateNewVetting(0,0,'',0,0,0,0,0,0,'')"/>
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </asp:Panel>
                    <asp:UpdatePanel ID="UpdAdvFltr" UpdateMode="Conditional" runat="server">
                        <ContentTemplate>
                            <asp:HiddenField ID="hfAdv" runat="server" Value="c" />
                            <div id="dvAdvanceFilter" align="left" class="hide" style="padding: 10px 1%">
                                <table border="0" cellpadding="1" cellspacing="1" style="width: 75%; background-color: #efefef;">
                                    <tr>
                                        <td valign="top" style="border: 1px solid #aabbdd; width: 400px;">
                                            <table border="0" cellpadding="2" cellspacing="1" width="100%">
                                                <tr style="background-color: #aabbdd;">
                                                    <td style="text-align: left; font-weight: bold;" colspan="2">
                                                        &nbsp;&nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left;">
                                                        Inspector :
                                                    </td>
                                                    <td style="text-align: left;">
                                                        <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                                            <ContentTemplate>
                                                                <CustomFilter:ucfDropdown ID="DDLInspector" runat="server" Height="200" UseInHeader="false"
                                                                    Width="160" />
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td valign="top" style="border: 1px solid #aabbdd; width: 300px;">
                                            <table border="0" cellpadding="2" cellspacing="1" width="100%">
                                                <tr style="background-color: #aabbdd;">
                                                    <td style="text-align: left; font-weight: bold;" colspan="2">
                                                        Last Vetting
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left;">
                                                        Between:
                                                    </td>
                                                    <td style="text-align: left;">
                                                        <asp:TextBox ID="txtLVetFromDate" runat="server" CssClass="txtInput" EnableViewState="true"
                                                            Font-Size="11px" Width="120px"></asp:TextBox>
                                                        <ajaxToolkit:CalendarExtender ID="cexLVetFromDate" runat="server" Format="dd-MM-yyyy"
                                                            TargetControlID="txtLVetFromDate">
                                                        </ajaxToolkit:CalendarExtender>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left;">
                                                        And:
                                                    </td>
                                                    <td style="text-align: left;">
                                                        <asp:TextBox ID="txtLVetToDate" runat="server" CssClass="txtInput" EnableViewState="true"
                                                            Font-Size="11px" Width="120px"></asp:TextBox>
                                                        <ajaxToolkit:CalendarExtender ID="cexLVetToDate" runat="server" Format="dd-MM-yyyy"
                                                            TargetControlID="txtLVetToDate">
                                                        </ajaxToolkit:CalendarExtender>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td valign="top" style="border: 1px solid #aabbdd; width: 300px">
                                            <table border="0" cellpadding="1" cellspacing="2" width="100%">
                                                <tr style="background-color: #aabbdd">
                                                    <td style="text-align: left; font-weight: bold;" colspan="2">
                                                        Expiry Date
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left;">
                                                        Between:
                                                    </td>
                                                    <td style="text-align: left;">
                                                        <asp:TextBox ID="txtEVetFromDate" runat="server" CssClass="txtInput" EnableViewState="true"
                                                            Font-Size="11px" Width="120px"></asp:TextBox>
                                                        <ajaxToolkit:CalendarExtender ID="cexEVetFromDate" runat="server" Format="dd-MM-yyyy"
                                                            TargetControlID="txtEVetFromDate">
                                                        </ajaxToolkit:CalendarExtender>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left;">
                                                        And:
                                                    </td>
                                                    <td style="text-align: left;">
                                                        <asp:TextBox ID="txtEVetToDate" runat="server" CssClass="txtInput" EnableViewState="true"
                                                            Font-Size="11px" Width="120px"></asp:TextBox>
                                                        <ajaxToolkit:CalendarExtender ID="cexEVetToDate" runat="server" Format="dd-MM-yyyy"
                                                            TargetControlID="txtEVetToDate">
                                                        </ajaxToolkit:CalendarExtender>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td valign="top" style="border: 1px solid #aabbdd; width: 300px;">
                                            <table border="0" cellpadding="1" cellspacing="2" width="100%">
                                                <tr style="background-color: #aabbdd">
                                                    <td style="text-align: left; font-weight: bold;" colspan="2">
                                                        Planned Vetting Date
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left;">
                                                        Between:
                                                    </td>
                                                    <td style="text-align: left;">
                                                        <asp:TextBox ID="txtPVetDateFromDate" runat="server" CssClass="txtInput" EnableViewState="true"
                                                            Font-Size="11px" Width="120px"></asp:TextBox>
                                                        <ajaxToolkit:CalendarExtender ID="cexPVetFromDate" runat="server" Format="dd-MM-yyyy"
                                                            TargetControlID="txtPVetDateFromDate">
                                                        </ajaxToolkit:CalendarExtender>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left;">
                                                        And:
                                                    </td>
                                                    <td style="text-align: left;">
                                                        <asp:TextBox ID="txtPVetDateToDate" runat="server" CssClass="txtInput" EnableViewState="true"
                                                            Font-Size="11px" Width="120px"></asp:TextBox>
                                                        <ajaxToolkit:CalendarExtender ID="cexPVetToDate" runat="server" Format="dd-MM-yyyy"
                                                            TargetControlID="txtPVetDateToDate">
                                                        </ajaxToolkit:CalendarExtender>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <div>
                    </div>
                    <div style="border: 0px solid #cccccc; margin-top: 17px; width: 100%;">
                        <asp:UpdatePanel ID="UpdPnlGrid" UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                                <div style="padding: 0px 1%">
                                    <asp:GridView ID="gvVetting" runat="server" EmptyDataText="NO RECORDS FOUND" AutoGenerateColumns="true"
                                        CellPadding="2" ShowHeaderWhenEmpty="true" OnRowDataBound="gvVetting_RowDataBound"
                                        Width="100%" CssClass="gridmain-css" GridLines="None" OnDataBound="gvVetting_DataBound"
                                        OnRowCreated="gvVetting_RowCreated">
                                        <HeaderStyle CssClass="HeaderStyle-css" />
                                        <RowStyle CssClass="RowStyle-css" Height="20px" />
                                        <AlternatingRowStyle CssClass="AlternatingRowStyle-css" Height="20px" />
                                        <SelectedRowStyle BackColor="#FFFFCC" />
                                        <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                    </asp:GridView>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="btnExport" />
                                <asp:PostBackTrigger ControlID="btnMacExport" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                
</ContentTemplate>
            
</ajaxToolkit:TabPanel>
            <ajaxToolkit:TabPanel ID="tbCalView" runat="server">
                <HeaderTemplate>
                    Calendar View
                
</HeaderTemplate>
                
<ContentTemplate>
                    <asp:Panel ID="Panel2" runat="server" DefaultButton="btnCRetrieve">
                        <asp:UpdatePanel ID="UpdCPnlFilter" UpdateMode="Conditional" runat="server"><ContentTemplate>
                                <div style="color: Black; padding: 10px 1%">
                                    <table cellpadding="2" cellspacing="0" width="100%" border="0">
                                        <tr>
                                            <td align="left" style="vertical-align: top; width: 8%;">
                                                Fleet : &nbsp;
                                            </td>
                                            <td align="left" style="vertical-align: top; width: 10%;">
                                                <asp:DropDownList ID="DDLCFleet" runat="server" AppendDataBoundItems="True" AutoPostBack="true"
                                                    Height="20px" OnSelectedIndexChanged="DDLCFleet_SelectedIndexChanged" Width="160px">
                                                    <asp:ListItem Selected="True" Value="0">--Select All--</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td align="left" style="vertical-align: top; width: 8%;">
                                                Vessel : &nbsp;
                                            </td>
                                            <td align="left" style="vertical-align: top; width: 10%;">
                                                <CustomFilter:ucfDropdown ID="DDLCVessel" runat="server" UseInHeader="false" Width="160"
                                                    Height="200" />
                                            </td>
                                            <td align="left" style="vertical-align: top; width: 8%;">
                                                Oil Majors : &nbsp;
                                            </td>
                                            <td align="left" style="vertical-align: top; width: 10%;">
                                                <CustomFilter:ucfDropdown ID="DDLCOilMajor" runat="server" Height="200" UseInHeader="false"
                                                    Width="160" />
                                            </td>
                                            <td style="vertical-align: bottom;" align="right" colspan="3">
                                                <table align="right">
                                                    <tr>
                                                        <td>
                                                            <asp:ImageButton ID="btnCRetrieve" runat="server" Height="25px" ImageAlign="AbsBottom"
                                                                ImageUrl="~/Images/SearchButton.png" OnClick="btnCRetrieve_Click" ToolTip="Search"
                                                                OnClientClick="return CValidateFilter()" />
                                                                
                                                        </td>
                                                        <td>
                                                            <asp:ImageButton ID="btnCClearFilter" runat="server" Height="24px" ImageUrl="~/Images/filter-delete-icon.png"
                                                                ToolTip="Clear Filter" OnClick="btnCClearFilter_Click" />
                                                        </td>
                                                        <td style="vertical-align: bottom; width: 20px;">
                                                            <asp:ImageButton ID="btnCExport" runat="server" CommandArgument="ExportFrom_IE" Height="25px"
                                                                ImageUrl="~/Images/XLS.jpg" OnClick="btnCExport_Click" Style="display: block;"
                                                                ToolTip="Export to Excel" />
                                                            <asp:ImageButton ID="btnCMacExport" runat="server" CommandArgument="ExportFrom_MAC"
                                                                Height="25px" ImageUrl="~/Images/Export-mac.png" OnClick="btnCExport_Click" Style="display: none;"
                                                                ToolTip="Export to Excel unformatted" />
                                                        </td>
                                                        <td style="font-size: 11px; line-height: 25px; vertical-align: top">
                                                            <asp:UpdatePanel ID="UpdatePanel4" UpdateMode="Conditional" runat="server">
                                                                <ContentTemplate>
                                                                    <a id="advCText" href="#" onclick="toggleCAdvSearch(this)">Open Advance Filter</a>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                Vetting Type :
                                            </td>
                                            <td align="left" >
                                                <CustomFilter:ucfDropdown ID="DDLCVettingType" runat="server" Height="200" UseInHeader="false"
                                                    Width="160" />
                                            </td>
                                            <td align="left">
                                                Expires in Days :
                                            </td>
                                            <td align="left" >
                                                <asp:DropDownList ID="DDLCExInDays" runat="server">
                                                    <asp:ListItem Value="0" Text="-- Select --"></asp:ListItem>
                                                    <asp:ListItem Value="15" Text="15"></asp:ListItem>
                                                    <asp:ListItem Value="30" Text="30"></asp:ListItem>
                                                    <asp:ListItem Value="45" Text="45"></asp:ListItem>
                                                    <asp:ListItem Value="60" Text="60"></asp:ListItem>
                                                    <asp:ListItem Value="90" Text="90"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td align="left">
                                                Planned Vetting :
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="rbtCVetState" runat="server" RepeatDirection="Horizontal"
                                                    AutoPostBack="false">
                                                    <asp:ListItem Text="All" Value="0" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="No" Value="2"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                            <td align="left" style="width: 10%;">
                                                By Vessel assignment :
                                            </td>
                                            <td align="left" style="width: 2%;">
                                                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                                    <ContentTemplate>
                                                        <asp:CheckBox ID="chkCVesselAssign" RepeatDirection="Horizontal" AutoPostBack="true"
                                                            Checked="true" runat="server" OnCheckedChanged="chkCVesselAssign_CheckedChanged">
                                                        </asp:CheckBox>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                            <td align="right">
                                                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                                    <ContentTemplate>
                                                     <div style="padding: 8px 0%;">
                                                            <asp:Button  ID="ImgAddNewCalVetting" runat="server"   ToolTip="Create New Vetting"  style="float: right; font-size: 11px;"  OnClientClick="CreateNewVetting()"  Text="Create New Vetting" />
                                                   </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            
</ContentTemplate>
</asp:UpdatePanel>

                    </asp:Panel>

                    <asp:UpdatePanel ID="updCAdvFilter" UpdateMode="Conditional" runat="server"><ContentTemplate>
                            <asp:HiddenField ID="hfCAdv" runat="server" Value="c" />
                            <div id="dvCAdvanceFilter" align="left" class="hide" style="padding: 0px 1%">
                                <table border="0" cellpadding="1" cellspacing="1" style="width: 75%; background-color: #efefef;">
                                    <tr>
                                        <td valign="top" style="border: 1px solid #aabbdd; width: 400px;">
                                            <table border="0" cellpadding="2" cellspacing="1" width="100%">
                                                <tr style="background-color: #aabbdd;">
                                                    <td style="text-align: left; font-weight: bold;" colspan="2">
                                                        &nbsp;&nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left;">
                                                        Inspector :
                                                    </td>
                                                    <td style="text-align: left;">
                                                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                            <ContentTemplate>
                                                                <CustomFilter:ucfDropdown ID="DDLCInspector" runat="server" Height="200" UseInHeader="false"
                                                                    Width="160" />
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td valign="top" style="border: 1px solid #aabbdd; width: 300px;">
                                            <table border="0" cellpadding="2" cellspacing="1" width="100%">
                                                <tr style="background-color: #aabbdd;">
                                                    <td style="text-align: left; font-weight: bold;" colspan="2">
                                                        Last Vetting
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left;">
                                                        Between:
                                                    </td>
                                                    <td style="text-align: left;">
                                                        <asp:TextBox ID="txtCLVetFromDate" runat="server" CssClass="txtInput" EnableViewState="true"
                                                            Font-Size="11px" Width="120px"></asp:TextBox>
                                                        <ajaxToolkit:CalendarExtender ID="cexCLVetFromDate" runat="server" Format="dd-MM-yyyy"
                                                            TargetControlID="txtCLVetFromDate">
                                                        </ajaxToolkit:CalendarExtender>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left;">
                                                        And:
                                                    </td>
                                                    <td style="text-align: left;">
                                                        <asp:TextBox ID="txtCLVetToDate" runat="server" CssClass="txtInput" EnableViewState="true"
                                                            Font-Size="11px" Width="120px"></asp:TextBox>
                                                        <ajaxToolkit:CalendarExtender ID="cexCLVetToDate" runat="server" Format="dd-MM-yyyy"
                                                            TargetControlID="txtCLVetToDate">
                                                        </ajaxToolkit:CalendarExtender>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td valign="top" style="border: 1px solid #aabbdd; width: 300px">
                                            <table border="0" cellpadding="1" cellspacing="2" width="100%">
                                                <tr style="background-color: #aabbdd">
                                                    <td style="text-align: left; font-weight: bold;" colspan="2">
                                                        Expiry Date
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left;">
                                                        Between:
                                                    </td>
                                                    <td style="text-align: left;">
                                                        <asp:TextBox ID="txtCEVetFromDate" runat="server" CssClass="txtInput" EnableViewState="true"
                                                            Font-Size="11px" Width="120px"></asp:TextBox>
                                                        <ajaxToolkit:CalendarExtender ID="cexCEVetFromDate" runat="server" Format="dd-MM-yyyy"
                                                            TargetControlID="txtCEVetFromDate">
                                                        </ajaxToolkit:CalendarExtender>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left;">
                                                        And:
                                                    </td>
                                                    <td style="text-align: left;">
                                                        <asp:TextBox ID="txtCEVetToDate" runat="server" CssClass="txtInput" EnableViewState="true"
                                                            Font-Size="11px" Width="120px"></asp:TextBox>
                                                        <ajaxToolkit:CalendarExtender ID="cexCEVetToDate" runat="server" Format="dd-MM-yyyy"
                                                            TargetControlID="txtCEVetToDate">
                                                        </ajaxToolkit:CalendarExtender>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td valign="top" style="border: 1px solid #aabbdd; width: 300px;">
                                            <table border="0" cellpadding="1" cellspacing="2" width="100%">
                                                <tr style="background-color: #aabbdd">
                                                    <td style="text-align: left; font-weight: bold;" colspan="2">
                                                        Planned Vetting Date
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left;">
                                                        Between:
                                                    </td>
                                                    <td style="text-align: left;">
                                                        <asp:TextBox ID="txtCPVetFromDate" runat="server" CssClass="txtInput" EnableViewState="true"
                                                            Font-Size="11px" Width="120px"></asp:TextBox>
                                                        <ajaxToolkit:CalendarExtender ID="cexCPVetFromDate" runat="server" Format="dd-MM-yyyy"
                                                            TargetControlID="txtCPVetFromDate">
                                                        </ajaxToolkit:CalendarExtender>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left;">
                                                        And:
                                                    </td>
                                                    <td style="text-align: left;">
                                                        <asp:TextBox ID="txtCPVetToDate" runat="server" CssClass="txtInput" EnableViewState="true"
                                                            Font-Size="11px" Width="120px"></asp:TextBox>
                                                        <ajaxToolkit:CalendarExtender ID="cexCPVetToDate" runat="server" Format="dd-MM-yyyy"
                                                            TargetControlID="txtCPVetToDate">
                                                        </ajaxToolkit:CalendarExtender>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        
</ContentTemplate>
</asp:UpdatePanel>

                    <div style="margin-top: 10px; padding: 5px 1%">
                        <div>
                            <asp:UpdatePanel ID="UpdatePanel8" UpdateMode="Conditional" runat="server"><ContentTemplate>
                                    <table style="width: 100%;">
                                        <tr>
                                            <td style="width: 20%;">
                                                <table style="width: 300px;">
                                                    <tr>
                                                        <td style="background-color: green; border: 2px solid White; color: Black; width: 50%;"
                                                            align="Center">
                                                            Completed
                                                        </td>
                                                        <td style="background-color: Yellow; color: Black; border: 2px solid White;" align="Center">
                                                            Expire in 60-30 days
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="background-color: green; color: Black; border: 2px solid Yellow;" align="Center">
                                                            In-Progress
                                                        </td>
                                                        <td style="border: 2px solid White; background-color: Orange; color: Black;" align="Center">
                                                            Expire in 30-0 days
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="background-color: Blue; color: White; border: 2px solid White;" align="Center">
                                                            Planned
                                                        </td>
                                                        <td style="background-color: Red; color: Black; border: 2px solid White;" align="Center">
                                                            Overdue
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="vertical-align: bottom;">
                                                <table align="center" style="width: auto;">
                                                    <tr>
                                                        <td>
                                                            <asp:ImageButton ID="ImgPrev" runat="server" ImageUrl="~/Images/Prev6Month.png" Height="15px"
                                                                OnClick="ImgPrev_Click" />
                                                        </td>
                                                        <td style="vertical-align: bottom;">
                                                            <asp:DropDownList ID="DDLMonth" runat="server" Font-Size="14px" Width="200px" Height="20px"
                                                                AutoPostBack="True" OnSelectedIndexChanged="DDLMonth_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td>
                                                            <asp:ImageButton ID="ImgNext" runat="server" ImageUrl="~/Images/Next6Month.png" Height="15px"
                                                                OnClick="ImgNext_Click" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                
</ContentTemplate>
</asp:UpdatePanel>

                        </div>
                        <asp:UpdatePanel ID="updCPnlGrid" UpdateMode="Conditional" runat="server"><ContentTemplate>
                                <div style="border: 1px solid #cccccc;">
                                    <asp:GridView ID="gvCalVetting" runat="server" EmptyDataText="NO RECORDS FOUND" AutoGenerateColumns="true"
                                        CellPadding="2" ShowHeaderWhenEmpty="true" OnRowDataBound="gvCalVetting_RowDataBound"
                                        Width="100%" CssClass="gridmain-css" GridLines="None" OnDataBound="gvCalVetting_DataBound"
                                        OnRowCreated="gvCalVetting_RowCreated" OnSorting="gvCalVetting_Sorting">
                                        <HeaderStyle CssClass="HeaderStyle-css" />
                                        <RowStyle CssClass="RowStyle-css" Height="30px" />
                                        <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                        <SelectedRowStyle BackColor="#FFFFCC" />
                                        <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                    </asp:GridView>
                                </div>
                            
</ContentTemplate>
<Triggers>
<asp:PostBackTrigger ControlID="btnCExport" />
<asp:PostBackTrigger ControlID="btnCMacExport" />
</Triggers>
</asp:UpdatePanel>

                    </div>
                
</ContentTemplate>
            
</ajaxToolkit:TabPanel>
        </ajaxToolkit:TabContainer>
    </div>
    <div id="dvNewVetting" style="display: none; width: 600px;" title=''>
        <iframe id="iFrmNewVetting" src="" frameborder="0" style="height: 400px; width: 100%">
        </iframe>
    </div>
    <div id="dvPortCall" style="display: none; width: 815px; text-align: center;" title="Port">
        <iframe id="IframePortCall" src="" frameborder="0" style="height: 250px; width: 815px;">
        </iframe>
    </div>

</asp:Content>
