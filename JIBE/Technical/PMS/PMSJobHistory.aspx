<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="PMSJobHistory.aspx.cs" Inherits="Technical_PMS_PMSJobHistory" EnableEventValidation="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="../../UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<%@ Register Src="../../UserControl/ucCustomDropDownList.ascx" TagName="ucfDropdown"
    TagPrefix="CustomFilter" %>
<%@ Register Src="../../UserControl/ucCustomStringFilter.ascx" TagName="ucfString"
    TagPrefix="CustomFilter" %>
<%@ Register Src="../../UserControl/ucCustomNumberFilter.ascx" TagName="ucfNumber"
    TagPrefix="CustomFilter" %>
<%@ Register Src="../../UserControl/ucCustomDateFilter.ascx" TagName="ucfDate" TagPrefix="CustomFilter" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/tooltipsytle.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/CssCustomFilterControls.css" rel="stylesheet" type="text/css" />
    <link href="../../Purchase/styles/Purchase.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/boxover.js" type="text/javascript"></script>
    <script src="../../Scripts/Purc_Functions_Common.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../../Scripts/ModalPopUp.js" type="text/jscript"></script>
    <script src="../../Scripts/Common_Functions.js" type="text/jscript"></script>
    <script src="../../Scripts/JSCustomFilterControls.js" type="text/javascript"></script>
    <script src="../../Scripts/ucCustomDropDownList.js" type="text/javascript"></script>
    <link href="../../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/SMSPopup.js" type="text/javascript"></script>
    <script lang="javascript" type="text/javascript">


        var lastExecutorJobsCount = null;
      //  debugger;
        function PMs_Get_OverdueJobsCount(jobid, fleetcode, dtVessel,  Function_ID,  System_ID,  System_Location_ID,  SubSystem_ID,  SubSystem_Location_ID,
                                            DTCF_RANKID, SearchJobID, SearchJobTitle, IsCritical, IsCMS, fromdate, todate,  IsHistory,
                                            JobStatus, DueDateFlageSearch,  PendingOfcVerify,  SafetyAlarm,  Calibration, advfromdate, advtodate,  PostponeJob,  FollowupRAdded,
                                           JobWithMandateRiskAssess, JobWithSubRiskAssess, JobWithDDock, Is_RAMandatory, Is_RASubmitted) {
            document.getElementById($('[id$=lblTotalJobs]').attr('id')).style.fontSize = "10px";
            document.getElementById($('[id$=lblOverdueJobs]').attr('id')).style.fontSize = "10px";
            document.getElementById($('[id$=lblCriticalOverdueJobs]').attr('id')).style.fontSize = "10px";
            document.getElementById($('[id$=lblTotalJobs]').attr('id')).textContent = "Loading...";
            document.getElementById($('[id$=lblOverdueJobs]').attr('id')).textContent = "Loading...";
            document.getElementById($('[id$=lblCriticalOverdueJobs]').attr('id')).textContent = "Loading...";
            var params = {jobid:jobid, fleetcode:fleetcode, dtVessel:dtVessel,  Function_ID:Function_ID,  System_ID:System_ID,  System_Location_ID:System_Location_ID,  SubSystem_ID:SubSystem_ID,  SubSystem_Location_ID:SubSystem_Location_ID,
                                            DTCF_RANKID:DTCF_RANKID, SearchJobID:SearchJobID, SearchJobTitle:SearchJobTitle, IsCritical:IsCritical, IsCMS:IsCMS, fromdate:fromdate, todate:todate,  IsHistory:IsHistory,
                                            JobStatus:JobStatus, DueDateFlageSearch:DueDateFlageSearch,  PendingOfcVerify:PendingOfcVerify,  SafetyAlarm:SafetyAlarm,  Calibration:Calibration, advfromdate:advfromdate, advtodate:advtodate,  PostponeJob:PostponeJob,  FollowupRAdded:FollowupRAdded,
                                            JobWithMandateRiskAssess: JobWithMandateRiskAssess, JobWithSubRiskAssess: JobWithSubRiskAssess, JobWithDDock: JobWithDDock, Is_RAMandatory: Is_RAMandatory, Is_RASubmitted: Is_RASubmitted
                                        };
                                        $.ajax({
                                            type: "POST",
                                            contentType: "application/json; charset=utf-8",
                                            url: "../../JIBEWebService.asmx/PMs_Get_OverdueJobsCount",
                                            data: JSON.stringify(params),
                                            dataType: "json",
                                            success: function (Result) {
                                                Result = Result.d;

                                                document.getElementById($('[id$=lblTotalJobs]').attr('id')).style.fontSize = "16px";
                                                document.getElementById($('[id$=lblOverdueJobs]').attr('id')).style.fontSize = "16px";
                                                document.getElementById($('[id$=lblCriticalOverdueJobs]').attr('id')).style.fontSize = "16px";
                                                document.getElementById($('[id$=lblTotalJobs]').attr('id')).textContent = Result.TotalJobsCount;

                                                document.getElementById($('[id$=lblOverdueJobs]').attr('id')).textContent = Result.OverdueJobsCount;

                                                document.getElementById($('[id$=lblCriticalOverdueJobs]').attr('id')).textContent = Result.CriticalJobsCount;
                                            

                                            },
                                            error: function (Result) {
                                               
                                            }
                                        });

        }


        function DocOpen(filename) {

            var filepath = "../../uploads/PmsJobs/";
            window.open(filepath + filename);
        }


        function showdetails(path) {
            window.open(path);
            return false;
        }
        function ValidateFilter() {

            var FromDate = document.getElementById($('[id$=txtFromDate]').attr('id'));
            var ToDate = document.getElementById($('[id$=txtToDate]').attr('id'));
            var rblDueType = document.getElementById($('[id$=rbtDueType]').attr('id'));
            var rblDueTypeinput = rblDueType.getElementsByTagName('input');
            var flag;
            var dates = {
                convert: function (d) {
                    // Converts the date in d to a date-object. The input can be:
                    //   a date object: returned without modification
                    //  an array      : Interpreted as [year,month,day]. NOTE: month is 0-11.
                    //   a number     : Interpreted as number of milliseconds
                    //                  since 1 Jan 1970 (a timestamp) 
                    //   a string     : Any format supported by the javascript engine, like
                    //                  "YYYY/MM/DD", "MM/DD/YYYY", "Jan 31 2009" etc.
                    //  an object     : Interpreted as an object with year, month and date
                    //                  attributes.  **NOTE** month is 0-11.
                    return (
            d.constructor === Date ? d :
            d.constructor === Array ? new Date(d[0], d[1], d[2]) :
            d.constructor === Number ? new Date(d) :
            d.constructor === String ? new Date(d) :
            typeof d === "object" ? new Date(d.year, d.month, d.date) :
            NaN
        );
                },
                compare: function (a, b) {
                    // Compare two dates (could be of any type supported by the convert
                    // function above) and returns:
                    //  -1 : if a < b
                    //   0 : if a = b
                    //   1 : if a > b
                    // NaN : if a or b is an illegal date
                    // NOTE: The code inside isFinite does an assignment (=).
                    return (
            isFinite(a = this.convert(a).valueOf()) &&
            isFinite(b = this.convert(b).valueOf()) ?
            (a > b) - (a < b) :
            NaN
        );
                },
                inRange: function (d, start, end) {
                    // Checks if date in d is between dates in start and end.
                    // Returns a boolean or NaN:
                    //    true  : if d is between start and end (inclusive)
                    //    false : if d is before start or after end
                    //    NaN   : if one or more of the dates is illegal.
                    // NOTE: The code inside isFinite does an assignment (=).
                    return (
            isFinite(d = this.convert(d).valueOf()) &&
            isFinite(start = this.convert(start).valueOf()) &&
            isFinite(end = this.convert(end).valueOf()) ?
            start <= d && d <= end :
            NaN
        );
                }


            }

            var flag = false;
            var selected;

            //            for (var i = 0; i < rblDueTypeinput.length; i++) {
            ////                if (rblDueTypeinput[i].checked && rblDueTypeinput[i].value != '0') {
            //                if (rblDueTypeinput[i].checked && rblDueTypeinput[i].value != '0') {
            //                    flag = true;
            //                    break;
            //                }
            //            }

            if (rblDueTypeinput.length > 0) {
                if (rblDueTypeinput[0].checked || rblDueTypeinput[1].checked) {
                    flag = true;
                }
            }

            var Fchunks = FromDate.value.split('-');
            var FformattedDate = [Fchunks[1], Fchunks[0], Fchunks[2]].join("/");
            var Tchunks = ToDate.value.split('-');
            var TformattedDate = [Tchunks[1], Tchunks[0], Tchunks[2]].join("/");


            var today = new Date();
            var dd = today.getDate();
            var mm = today.getMonth() + 1; //January is 0!

            var yyyy = today.getFullYear();
            if (dd < 10) {
                dd = '0' + dd
            }
            if (mm < 10) {
                mm = '0' + mm
            }
            var today = mm + '/' + dd + '/' + yyyy;


            var FDate = new Date(FformattedDate);
            var TDate = new Date(TformattedDate);


            var strFDate = FromDate.value;
            var strTDate = ToDate.value;



            if (dates.compare(FformattedDate, today) == 1) {/* Added by Someshwar on 20-08-2016  */
                alert("From Date should not be greater than current date.");
                return false;
            }

            else if (strFDate.trim() != "" && strTDate.trim() != "") {
                if (dates.compare(FDate, TDate) == 1) {

                    alert("To Date Cannot be less than From Date");
                    return false;
                }
            }
            else if (strFDate.trim() == "" && strTDate.trim() == "") {

                if (flag == true) {
                    alert("Both dates cannot be empty while using the Job Due/Overdue filter. Enter either one or both the range dates.");         //Reshma : Message change as mention in JIT : 11177

                    return false;
                }

            }

            //PMs_Get_OverdueJobsCount();

        }
        function getOperatingSystem() {
            var OSName = "";
            var Browser = "";

            if (navigator.appVersion.indexOf("Win") != -1) OSName = "Windows";
            if (navigator.appVersion.indexOf("Mac") != -1) OSName = "MacOS";
            if (navigator.appVersion.indexOf("X11") != -1) OSName = "UNIX";
            if (navigator.appVersion.indexOf("Linux") != -1) OSName = "Linux";

            if (OSName == "MacOS") {
                document.getElementById("ctl00_MainContent_btnExport").style.display = "none";
            }

            if (OSName == "Windows") {
                document.getElementById("ctl00_MainContent_btnMacExport").style.display = "none";
            }

        }


        function showJobDetails(url) {

            try {
                var frame = document.getElementById("iFrmJobsDetails");
                frameDoc = frame.contentDocument || frame.contentWindow.document;
                frameDoc.removeChild(frameDoc.documentElement);

            }
            catch (ex) {

            }

            document.getElementById('iFrmJobsDetails').src = url;
            showModal('dvJobsDetails');

        }


        function OpenWorkListCrewInvolved(vid, jobid, jobhistoryid) {


            $('#dvPopupFrame').attr("Title", "Add maintenance feedback for this job");
            $('#dvPopupFrame').css({ "width": "700px", "height": "600px", "text-allign": "center" });
            $('#frPopupFrame').load(function () { this.style.height = (this.contentWindow.document.body.offsetHeight + 20) + 'px'; });

            var URL = "../../Technical/PMS/PMSJob_Crew_Involved.aspx?VID=" + vid + "&JobID=" + jobid + "&JobHistoryID=" + jobhistoryid + "&Mode=ADD&rnd=" + Math.random();

            document.getElementById("frPopupFrame").src = URL;
            showModal('dvPopupFrame', true);
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
        function searchKeyPress(e) {
            // look for window.event in case event isn't passed in
            if (e.keyCode == 13) {
                document.getElementById($('[id$=btnRetrieve]').attr('id')).click();
                return false;
            }
            return true;
        }

       
    </script>
    <style type="text/css">
        .page
        {
            width: 1440px;
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
    <center>
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
            <asp:Panel runat="server" DefaultButton="btnRetrieve">
                <asp:UpdatePanel ID="UpdPnlFilter" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                        <table cellpadding="2" cellspacing="0" width="100%" style="color: Black;">
                            <tr>
                                <td align="left" style="vertical-align: top;">
                                    Fleet : &nbsp;
                                </td>
                                <td align="left" style="vertical-align: top;">
                                    <asp:DropDownList ID="DDLFleet" runat="server" AppendDataBoundItems="True" AutoPostBack="true"
                                        Height="20px" OnSelectedIndexChanged="DDLFleet_SelectedIndexChanged" Width="160px">
                                        <asp:ListItem Selected="True" Value="0">--SELECT ALL--</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td align="left" style="vertical-align: top;">
                                    Function : &nbsp;
                                </td>
                                <td align="left" style="vertical-align: top;">
                                    <asp:DropDownList ID="ddlFunction" runat="server" AppendDataBoundItems="True" AutoPostBack="true"
                                        OnSelectedIndexChanged="ddlFunction_SelectedIndexChanged" Width="150px">
                                    </asp:DropDownList>
                                </td>
                                <td rowspan="2" align="left" style="vertical-align: top;">
                                    <asp:CheckBoxList ID="rbtDueType" runat="server" RepeatDirection="Vertical" AutoPostBack="true"
                                        OnSelectedIndexChanged="rbtDueType_SelectedIndexChanged">
                                        <asp:ListItem Text="Job Due" Value="1" Selected="False"></asp:ListItem>
                                        <asp:ListItem Text="Job Overdue" Value="2" Selected="False"></asp:ListItem>
                                    </asp:CheckBoxList>
                                </td>
                                <td align="left" style="vertical-align: top;">
                                    Date is between : &nbsp;
                                </td>
                                <td align="left" style="vertical-align: top;">
                                    <asp:TextBox ID="txtFromDate" runat="server" CssClass="txtInput" EnableViewState="true"
                                        Font-Size="11px" Width="120px"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="txtFromDate_CalendarExtender" runat="server" Format="dd-MM-yyyy"
                                        TargetControlID="txtFromDate">
                                    </ajaxToolkit:CalendarExtender>
                                </td>
                                <td colspan="5" align="left">
                                    &nbsp;&nbsp;Pending Office Verification:&nbsp; &nbsp;
                                    <asp:CheckBox ID="chkOfcVerify" runat="server"></asp:CheckBox>
                                </td>
                                <td>
                                    <asp:ImageButton ID="btnExport" runat="server" CommandArgument="ExportFrom_IE" Height="25px"
                                        ImageUrl="~/Images/XLS.jpg" OnClick="btnExport_Click" Style="display: block;"
                                        ToolTip="Export to Excel formatted" />
                                </td>
                                <td style="vertical-align: bottom;">
                                    <asp:ImageButton ID="btnMacExport" runat="server" CommandArgument="ExportFrom_MAC"
                                        Height="25px" ImageUrl="~/Images/Export-mac.png" OnClick="btnExport_Click" Style="display: none;"
                                        ToolTip="Export to Excel unformatted" />
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="vertical-align: top;">
                                    Vessel : <b style="color:Red; vertical-align:middle;">*</b>
                                </td>
                                <td align="left" style="vertical-align: top;" class="style2">
                                    <CustomFilter:ucfDropdown ID="DDLVessel" runat="server" UseInHeader="false" Width="160"
                                        Height="200" OnApplySearch="DDLVesselApplySearch" />
                                </td>
                                <td align="left" style="vertical-align: top;">
                                    System /Location :&nbsp;
                                </td>
                                <td align="left" style="vertical-align: top;" class="style7">
                                    <asp:DropDownList ID="ddlSystem_location" runat="server" AppendDataBoundItems="True"
                                        AutoPostBack="True" OnSelectedIndexChanged="ddlSystem_location_SelectedIndexChanged"
                                        Width="150px">
                                    </asp:DropDownList>
                                </td>
                                <td align="left" style="vertical-align: top;">
                                    And : &nbsp;
                                </td>
                                <td align="left" style="vertical-align: top;">
                                    <asp:TextBox ID="txtToDate" runat="server" CssClass="txtInput" EnableViewState="true"
                                        Font-Size="11px" Width="120px"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="txtToDate_CalendarExtender" runat="server" Format="dd-MM-yyyy"
                                        TargetControlID="txtToDate">
                                    </ajaxToolkit:CalendarExtender>
                                </td>
                                <td align="left" style="white-space: nowrap" class="style4">
                                    Critical:&nbsp;
                                </td>
                                <td align="left">
                                    <asp:CheckBox ID="chkCritical" runat="server"></asp:CheckBox>
                                </td>
                                <td align="left" rowspan="2">
                                    <asp:RadioButtonList ID="rbtnJobTypes" runat="server" RepeatDirection="Vertical"
                                        AutoPostBack="false">
                                        <asp:ListItem Text="Planned" Value="PMS" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="UnPlanned" Value="NONPMS"></asp:ListItem>
                                    </asp:RadioButtonList>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="vertical-align: top;">
                                    Rank : &nbsp;
                                </td>
                                <td align="left" style="vertical-align: top;">
                                    <CustomFilter:ucfDropdown ID="ucf_DDLRank" runat="server" Height="200"
                                        UseInHeader="false" Width="160" />
                                </td>
                                <td align="left" style="vertical-align: top;">
                                    Subsystem / Location :&nbsp;
                                </td>
                                <td align="left" style="vertical-align: top;">
                                    <asp:DropDownList ID="ddlSubSystem_location" runat="server" Width="150px">
                                    </asp:DropDownList>
                                </td>
                                <td></td>
                                <td align="left"  style="vertical-align: top;">
                                    Search by code/title : &nbsp;
                                </td>
                                <td align="left" style="vertical-align: top;">
                                    <asp:TextBox ID="txtSearchJobTitle" runat="server" EnableViewState="true" Font-Size="11px"
                                        CssClass="txtInput" Width="120px" ></asp:TextBox>
                                </td>
                                <td align="left" style="vertical-align: top;">
                                    CMS:&nbsp;
                                </td>
                                <td align="left" style="vertical-align: top;">
                                    <asp:CheckBox ID="chkCMS" runat="server"></asp:CheckBox>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </asp:Panel>
            <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                    <table width="100%">
                        <tr>
                            <td align="right">
                                <asp:ImageButton ID="btnRetrieve" runat="server" Height="25px" ImageAlign="AbsBottom"
                                    ImageUrl="~/Images/SearchButton.png" OnClick="btnRetrieve_Click" ToolTip="Search"
                                    OnClientClick="return ValidateFilter()" />
                                &nbsp;
                                <asp:ImageButton ID="btnClearFilter" runat="server" Height="25px" ImageUrl="~/Images/filter-delete-icon.png"
                                    ToolTip="Clear Filter" OnClick="btnClearFilter_Click" />
                                &nbsp;
                                <%--  <asp:LinkButton ID="lnkAdvText" runat="server" OnClientClick="toggleAdvSearch(this)">Open Advance Filter</asp:LinkButton>--%>
                                <a id="advText" href="#" onclick="toggleAdvSearch(this)">Open Advance Filter</a>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="UpdAdvFltr" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                    <asp:HiddenField ID="hfAdv" runat="server" Value="c" />
                    <div id="dvAdvanceFilter" align="left" class="hide">
                        <table border="0" cellpadding="1" cellspacing="1" style="width: 65%; background-color: #efefef;">
                            <tr>
                                <td valign="top" style="border: 1px solid #aabbdd;">
                                    <table border="0" cellpadding="2" cellspacing="1" width="100%">
                                        <tr style="background-color: #aabbdd;">
                                            <td style="text-align: left; font-weight: bold;" colspan="2">
                                                Actual Job Performance Date:
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left;">
                                                Between:
                                            </td>
                                            <td style="text-align: left;">
                                                <asp:TextBox ID="txtActFrmDate" runat="server" CssClass="txtInput" EnableViewState="true"
                                                    Font-Size="11px" Width="120px"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd-MM-yyyy"
                                                    TargetControlID="txtActFrmDate">
                                                </ajaxToolkit:CalendarExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left;">
                                                And:
                                            </td>
                                            <td style="text-align: left;">
                                                <asp:TextBox ID="txtActToDate" runat="server" CssClass="txtInput" EnableViewState="true"
                                                    Font-Size="11px" Width="120px"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd-MM-yyyy"
                                                    TargetControlID="txtActToDate">
                                                </ajaxToolkit:CalendarExtender>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td valign="top" style="border: 1px solid #aabbdd; width: 300px">
                                    <table border="0" cellpadding="1" cellspacing="2" width="100%">
                                        <tr style="background-color: #aabbdd">
                                            <td colspan="2" style="text-align: left;">
                                                &nbsp; &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left;">
                                                Calibration:
                                            </td>
                                            <td style="text-align: left;">
                                                <asp:CheckBox ID="chkAdvCalibration" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left;">
                                                Safety Alarm:
                                            </td>
                                            <td style="text-align: left;">
                                                <asp:CheckBox ID="chkAdvSafetyAlarm" runat="server" />
                                            </td>
                                        </tr>
                                       
                                    </table>
                                </td>
                                 <td valign="top" style="border: 1px solid #aabbdd; width: 350px">
                                <table border="0" cellpadding="1" cellspacing="2" width="100%">
                                    <tr style="background-color: #aabbdd">
                                        <td colspan="2" style="text-align: left;">
                                          &nbsp; &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left;">
                                             Mandatory Risk Assessment
                                        </td>
                                        <td style="text-align: left;">
                                           <asp:RadioButtonList ID="rbtnMRA" runat="server" RepeatDirection="Horizontal"
                                            AutoPostBack="false">
                                            <asp:ListItem Text="ALL" Value="ALL" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="YES" Value="YES"></asp:ListItem>
                                            <asp:ListItem Text="NO" Value="NO"></asp:ListItem>
                                        </asp:RadioButtonList>
                                   
                                        </td>
                                    </tr>
                                     <tr>
                                        <td style="text-align: left;">
                                            Risk Assessment Submitted
                                        </td>
                                        <td style="text-align: left;">
                                          <asp:RadioButtonList ID="rbtnRASubmitted" runat="server" RepeatDirection="Horizontal"
                                            AutoPostBack="false">
                                            <asp:ListItem Text="ALL" Value="ALL" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="YES" Value="YES"></asp:ListItem>
                                            <asp:ListItem Text="NO" Value="NO"></asp:ListItem>
                                        </asp:RadioButtonList>
                                   
                                        </td>
                                    </tr>                                    
                                </table>
                            </td>
                                
                            </tr>
                        </table>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <table width="80%" align="left">
                        <tr>
                            <td align="left" width="8%">
                                Total Jobs : &nbsp;
                            </td>
                            <td align="left" width="13%">
                                <asp:Label ID="lblTotalJobs" runat="server" Font-Bold="true" Font-Size="10px" Text=""
                                    ForeColor="#3498DB"></asp:Label>
                            </td>
                            <td align="left" width="11%">
                                Total Overdue Jobs : &nbsp;
                            </td>
                            <td align="left" width="13%">
                                <asp:Label ID="lblOverdueJobs" runat="server" Font-Bold="true" Font-Size="10px" Text=""
                                    ForeColor="Red" ToolTip="Repeated jobs are consider only once in a count"></asp:Label>
                            </td>
                            <td align="left" width="13%">
                                Total Critical Overdue Jobs : &nbsp;
                            </td>
                            <td align="left" width="13%">
                                <asp:Label ID="lblCriticalOverdueJobs" runat="server" Font-Bold="true" Font-Size="10px"
                                    Text="" ForeColor="Red" ToolTip="Repeated jobs are consider only once in a count"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
            <div style="border: 1px solid #cccccc; margin-top: 25px;">
                <asp:UpdatePanel ID="UpdPnlGrid" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                        <div>
                            <asp:GridView ID="gvStatus" runat="server" EmptyDataText="NO RECORDS FOUND" AutoGenerateColumns="False"
                                CellPadding="2" ShowHeaderWhenEmpty="true" OnRowDataBound="gvStatus_RowDataBound"
                                Width="100%" AllowSorting="true" OnSorting="gvStatus_Sorting" DataKeyNames="JOB_ID"
                                CssClass="gridmain-css" GridLines="None">
                                <HeaderStyle CssClass="HeaderStyle-css" />
                                <RowStyle CssClass="RowStyle-css" />
                                <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                <SelectedRowStyle BackColor="#FFFFCC" />
                                <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Vessel">
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lblVesseleNameHeader" runat="server" ForeColor="Blue" CommandArgument="Vessel_Name"
                                                CommandName="Sort">Vessel&nbsp;</asp:LinkButton>
                                            <img id="Vessel_Name" runat="server" visible="false" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblVesselName" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Vessel_Name") %>'></asp:Label>
                                            <asp:Label ID="lblVesselCode" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Vessel_ID") %>'></asp:Label>
                                            <asp:Label ID="lblLocationID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.LocationID") %>'></asp:Label>
                                            <asp:Label ID="lblJobHistoryID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.JobHistoryID") %>'></asp:Label>
                                            <asp:Label ID="lblSysID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.SysID") %>'></asp:Label>
                                            <asp:Label ID="lblSubSysID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.SubSysID") %>'></asp:Label>
                                            <asp:Label ID="lblFunctionID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.FunctionID") %>'></asp:Label>
                                            <asp:Label ID="lblFleetID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.FleetID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Machinery" HeaderText="System" Visible="false" />
                                    <asp:TemplateField HeaderText="Location">
                                        <HeaderTemplate>
                                            <asp:Label ID="lblLocationHeader" runat="server">System Location&nbsp;</asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblLocation" runat="server" onclick='<%#"asyncGetMachinery_Popup(&#39;"+Eval("System_ID").ToString()+"&#39;,event,this);" %>'
                                                Text='<%# DataBinder.Eval(Container,"DataItem.Location") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="SubSystem" Visible="false">
                                        <HeaderTemplate>
                                            <asp:Label ID="lblSubSystemHeader" runat="server">SubSystem&nbsp;</asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblSubSystem" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.SubSystem") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="SUBLocation" HeaderText="Sub-System Location" />
                                    <asp:TemplateField HeaderText="Job Code" HeaderStyle-Width="50px">
                                        <HeaderTemplate>
                                            Job Code
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblJobCode" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.JOB_ID") %>'></asp:Label>
                                            <asp:HyperLink ID="hlnkJobCode" runat="server" Target="_blank" Text='<%# DataBinder.Eval(Container,"DataItem.Job_Code") %>'></asp:HyperLink>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Job Title">
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lblJobTitleHeader" runat="server" CommandArgument="JOB_TITLE"
                                                CommandName="Sort">Job Title&nbsp;</asp:LinkButton>
                                            <img id="JOB_TITLE" runat="server" visible="false" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblJobTitle" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.JOB_TITLE") %>'></asp:Label>
                                            <asp:Label ID="lblJobDescription" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Job_Description") %>'></asp:Label>
                                            <asp:Label ID="lblOverDueFlage" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.OVERDUEFLAGE") %>'></asp:Label>
                                            <asp:Label ID="lblNext30dayFlage" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.NEXT30DAYSFLAGE") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Frequency">
                                        <HeaderTemplate>
                                            Freq.
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblFrequency" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.FREQUENCY") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Frequency Name">
                                        <HeaderTemplate>
                                            Freq. Name
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblFrequencyType" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.FREQUENCY_TYPE") %>'></asp:Label>
                                            <asp:Label ID="lblFrequencyName" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Frequency_Name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Due Dt." Visible="true">
                                        <HeaderTemplate>
                                            Due Dt.
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblHDue" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.DUE_DATE","{0:dd-MM-yyyy}") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="False" HorizontalAlign="Center" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Done">
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lblJobTitleHeaderDone" runat="server" CommandArgument="DATE_DONE"
                                                CommandName="Sort">Done&nbsp;</asp:LinkButton>
                                            <img id="DATE_DONE" runat="server" visible="false" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblNextDone" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.NEXT_DONE","{0:dd-MM-yyyy}") %>'></asp:Label>
                                            <asp:HyperLink ID="hlnkNextDone" runat="server" Target="_blank" Text='<%# DataBinder.Eval(Container,"DataItem.NEXT_DONE","{0:dd-MM-yyyy}") %>'></asp:HyperLink>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="OD. Days" Visible="true">
                                        <HeaderTemplate>
                                            OD. Days
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblODDays" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.OD_DAYS") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="False" HorizontalAlign="Center" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Next Due">
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lblJobTitleHeaderNdue" runat="server" CommandArgument="DATE_NEXT_DUE"
                                                CommandName="Sort">Next Due&nbsp;</asp:LinkButton>
                                            <img id="DATE_NEXT_DUE" runat="server" visible="false" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblNextDue" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.DATE_NEXT_DUE","{0:dd-MM-yyyy}") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Rhrs">
                                        <HeaderTemplate>
                                            Rhrs
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblRHrsdone" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.RHRS_DONE") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="RemRhrs" Visible="false">
                                        <HeaderTemplate>
                                            Rem. Rhrs
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblRemRHrsdone" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.RemRhrs") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="CMS">
                                        <HeaderTemplate>
                                            CMS
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblCms" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.CMS") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="False" HorizontalAlign="Center" Width="50px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Critical">
                                        <HeaderTemplate>
                                            Critical
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblCritical" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Critical") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="False" HorizontalAlign="Center" Width="50px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Mandatory Risk Assessment">
                                        <HeaderTemplate>
                                            Mandatory Risk Assessment
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblMandatoryRA" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.IsRAMandatory") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="False" HorizontalAlign="Center" Width="50px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Department" Visible="false">
                                        <HeaderTemplate>
                                            Department
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblDepartment" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Department") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="False" HorizontalAlign="Left" Width="70px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Rank" Visible="false">
                                        <HeaderTemplate>
                                            Rank
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblRank" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.RankName") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="False" HorizontalAlign="Center" Width="70px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Remarks" Visible="false">
                                        <HeaderTemplate>
                                            Remarks
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblJobHistoryRemaks" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Remarks") %>'></asp:Label>
                                            <asp:Label ID="lblFullJobHistoryRemaks" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.FullRemarks") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="False" HorizontalAlign="Left" Width="70px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                        <HeaderTemplate>
                                            <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                <tr>
                                                    <td align="center">
                                                        <asp:Label ID="lblAction" runat="server">Action</asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <span id="lblActionDisplayText" style="height: 15px; width: 100px; color: #1E607F">
                                                        </span>
                                                    </td>
                                                </tr>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <table cellpadding="1" cellspacing="0">
                                                <tr align="center">
                                                    <td style="border-color: transparent; width: 20px">
                                                        <asp:Label ID="lblFilePathIfSingle" Visible="false" runat="server" Text='<%# Eval("FilePathIfSingle") %>'></asp:Label>
                                                        <asp:ImageButton ID="ImgJobDoneAtt" runat="server" Height="16px" Visible='<%# Eval("AttCount").ToString() =="0" ? false : true%>'
                                                            ForeColor="Black" ImageUrl="~/Images/attach-icon.png" />
                                                    </td>
                                                    <td style="border-color: transparent; width: 20px">
                                                        <asp:HyperLink ID="ImgHistory" runat="server" Text="Select" Height="12px" ForeColor="Black"
                                                            ToolTip="Job History" Target="_blank" ImageUrl="~/Purchase/Image/JobHistory.png"
                                                            onmouseover="DisplayActionInHeader('Job History','gvStatus')"></asp:HyperLink>
                                                    </td>
                                                    <td style="border-color: transparent; width: 20px">
                                                        <asp:ImageButton ID="ImgRHours" runat="server" Text="Select" Height="12px" ForeColor="Black"
                                                            Visible="false" ToolTip="Running hours" ImageUrl="~/Purchase/Image/Rhrs.png"
                                                            onmouseover="DisplayActionInHeader('Running hrs','gvStatus')"></asp:ImageButton>
                                                    </td>
                                                    <td style="border-color: transparent; width: 20px">
                                                        <asp:ImageButton ID="ImgSpareUsed" runat="server" Text="Select" Height="16px" Width="16px"
                                                            CommandArgument='<%#Eval("JOB_ID")%>' ForeColor="Black" ToolTip="Spare Item Used"
                                                            ImageUrl="~/Purchase/Image/ToolBox.png" Visible='<%#DataBinder.Eval(Container.DataItem, "Spare_used_flag").ToString() =="N"? false : true %>'
                                                            onmouseover="DisplayActionInHeader('Spare Item Used','gvStatus')" />
                                                    </td>
                                                    <td style="border-color: transparent; width: 20px">
                                                        <asp:ImageButton ID="ImgFeedback" runat="server" Text="Select" Height="16px" Width="16px"
                                                            CommandArgument='<%#Eval("JOB_ID")%>' ForeColor="Black" ToolTip="Add Feedback"
                                                            OnClientClick='<%# "OpenWorkListCrewInvolved("+ Eval("VESSEL_ID").ToString() +","+ UDFLib.ConvertToInteger(Eval("JOB_ID")).ToString()  +","+ Eval("JobHistoryID").ToString() +");return false;" %>'
                                                            ImageUrl="~/Purchase/Image/Add-Maker.png" Visible='<%#Eval("JobHistoryID").ToString() =="" ? false : true %>'
                                                            onmouseover="DisplayActionInHeader('Add Feedback','gvStatus')" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="False" HorizontalAlign="Center" Width="200px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                        <div style="margin-top: 2px; border: 1px solid #cccccc; vertical-align: bottom; padding: 2px;
                            color: Black; text-align: left;">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="width: 100%">
                                        <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" PageSize="50" OnBindDataItem="BindJobStatus" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnExport" />
                        <asp:PostBackTrigger ControlID="btnMacExport" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
        <div id="dvJobsDetails" style="display: none; width: 830px;" title=''>
            <iframe id="iFrmJobsDetails" src="" frameborder="0" style="height: 560px; width: 100%">
            </iframe>
        </div>
        <div id="dvPopupFrame" class="draggable" style="display: none; background-color: #CBE1EF;
            border-color: #5C87B2; border-style: solid; border-width: 1px; position: absolute;
            left: 0.5%; top: 15%; width: 900px; z-index: 1; color: black" title=''>
            <div class="content">
                <iframe id="frPopupFrame" src="" frameborder="0" height="100%" width="100%"></iframe>
            </div>
        </div>
    </center>
</asp:Content>
