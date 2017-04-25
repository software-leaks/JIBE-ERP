<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master" CodeFile="SupInspCalendar.aspx.cs"
    Inherits="Technical_Worklist_SupInspCalendar" Title="Vessel Calendar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../Scripts/Common_Functions.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../../Scripts/ModalPopUp.js?v=1" type="text/javascript"></script>
    <style type="text/css">
        .page
        {
            width: 1700px;
            overflow-y: hidden;
        }
        .DateStyle
        {
            background-color: #ADEAFF;
            color: Black;
            max-width: 15px;
            min-width: 15px;
            border: 1px solid #E6E6E6;
            border-collapse: collapse;
        }
        .DayStyle
        {
            background-color: #ADEAFF;
            color: Black;
            border: 1px solid #E6E6E6;
            border-collapse: collapse;
        }
        .NormStyle
        {
            min-width: 15px;
            max-width: 15px;
            border-left: 1px solid #E6E6E6;
            border-right: 1px solid #E6E6E6;
            border-bottom: 1px solid #E6E6E6;
            border-collapse: collapse;
            height: 15px;
            text-align: center;
        }
        
        #t00
        {
            padding: 0px;
            border-collapse: collapse;
        }
        #t01
        {
            padding: 0px;
            border-collapse: collapse;
        }
        .MonthStyle
        {
            background: url(../../Images/gridheaderbg-silver-image.png) left -0px repeat-x;
            background-color: #9575CD;
            font-weight: bold;
            color: Black;
            border: 1px solid darkgrey;
            border-collapse: collapse;
            padding: 5px;
        }
        .SupStyle
        {
            background-color: #F3F3F3;
            min-width: 175px;
            max-width: 175px;
            color: Black;
            border: 1px solid #E6E6E6;
            border-collapse: collapse;
        }
        .VesselStyle
        {
            background-color: #F3F3F3;
            color: Black;
            min-width: 175px;
            max-width: 175px;
            border-collapse: collapse;
            border-top: 1px solid #dfdbe7;
        }
        .inspectioncalendar
        {
            width: 20px;
            margin: -10px 9px -7px 10px;
        }
        #spanRenewalInspection
        {
            width: 80px;
            float: left;
        }
        .VewSurvey
        {
            border-color: #716C6C;
            border-width: 2px;
            border-style: solid;
            height: 25px;
            background-color: #E3E3E3;
            cursor: pointer;
            width: 190px;
        }
        .CrewImage
        {
            width: 70px;
        }
        .TOP td
        {
            border-top: 1px solid #dfdbe7 !important;
        }
        .BOTTOM td
        {
            border-bottom: 1px solid #dfdbe7 !important;
        }
        .Highlight
        {
            background-color: #f3f30b !important;
        }
        .ReminderTextWrap{max-width:375px;}
        .SurveyCertificateExpiry img{width:13px;}
        .SurveyCertificateReminder img{width:12px;}
    </style>
    <script type="text/javascript">


        var year;
        var month;
        var pFilterCompany = "0";

        Date.isLeapYear = function (year) {
            return (((year % 4 === 0) && (year % 100 !== 0)) || (year % 400 === 0));
        };

        Date.getDaysInMonth = function (year, month) {
            return [31, (Date.isLeapYear(year) ? 29 : 28), 31, 30, 31, 30, 31, 31, 30, 31, 30, 31][month];
        };

        Date.prototype.isLeapYear = function () {
            return Date.isLeapYear(this.getFullYear());
        };

        Date.prototype.getDaysInMonth = function () {
            return Date.getDaysInMonth(this.getFullYear(), this.getMonth());
        };

        Date.prototype.addMonths = function (value) {
            var n = this.getDate();
            this.setDate(1);
            this.setMonth(this.getMonth() + value);
            this.setDate(Math.min(n, this.getDaysInMonth()));
            return this;
        };


        function abc(x, a, b) {
            if (x) {
                var m = "test";

                js_ShowToolTip(m, a, b);
            }

        }

        var SlotDic = {};

        $(document).ready(function () {

            SlotDic[1] = 1;
            SlotDic[2] = 1;
            SlotDic[3] = 1;
            SlotDic[4] = 4;
            SlotDic[5] = 4;
            SlotDic[6] = 4;
            SlotDic[7] = 7;
            SlotDic[8] = 7;
            SlotDic[9] = 7;
            SlotDic[10] = 10;
            SlotDic[11] = 10;
            SlotDic[12] = 10;
            Current();


            $("body").on("mouseover", ".NormStyle div,.SurveyCertificateExpiry div,.SurveyCertificateReminder div", function () {
                HighlightDate($(this).attr("rel"))
            });

            $("body").on("mouseout", ".NormStyle div,.SurveyCertificateExpiry div,.SurveyCertificateReminder div", function () {
                $(".DateStyle").removeClass("Highlight");
                $(".DayStyle").removeClass("Highlight");
            });
        });



        function LoadCalendar(pUserCompanyId, pStartDate) {

            $("#<%=upUpdateProgress.ClientID %>").show();
            pStartDate = $("#hdnSelectedvalue").val();

            pUserCompanyId = document.getElementById($('[id$=hfcompanyid]').attr('id')).value;

            var options = {
                url: '/' + __app_name + '/JibeWebServiceInspection.asmx/asncLoadCalendar',
                data: '{ pUserCompanyId: "' + pUserCompanyId + '", pStartDate: "' + pStartDate + '" }',
                dataType: 'json',
                type: 'POST',
                async: true,
                contentType: "application/json; charset=utf-8",
                success: function (response) {
                    if (response != '') {
                        OnSuccGet_Table(response, null);
                        BindCSS();
                        $("#<%=upUpdateProgress.ClientID %>").hide();
                    }
                },
                error: function () {
                    Onfail();
                }
            }
            $.ajax(options);
        }


        var lTempDate;
        var lasty;
        var lastm;
        function Current() {
            var lcomid = document.getElementById($('[id$=hfcompanyid]').attr('id')).value;
            lTempDate = new Date();
            $("#hdnSelectedvalue").val(lTempDate.getFullYear() + "-" + parseInt(lTempDate.getMonth() + 1) + "-" + lTempDate.getDate());
            LoadCalendar(lcomid, lTempDate);
        }


        function Next() {

            var lcomid = document.getElementById($('[id$=hfcompanyid]').attr('id')).value;
            lTempDate = lTempDate.addMonths(1);
            js_HideTooltip_Fixed();
            $("#hdnSelectedvalue").val(lTempDate.getFullYear() + "-" + parseInt(lTempDate.getMonth() + 1) + "-" + lTempDate.getDate());
            LoadCalendar(lcomid, lTempDate);
        }

        function LastLoad() {
            var lcomid = document.getElementById($('[id$=hfcompanyid]').attr('id')).value;
            $("#hdnSelectedvalue").val(lTempDate.getFullYear() + "-" + parseInt(lTempDate.getMonth() + 1) + "-" + lTempDate.getDate());
            LoadCalendar(lcomid, lTempDate);
        }


        function Prev() {
            var lcomid = document.getElementById($('[id$=hfcompanyid]').attr('id')).value;
            lTempDate = lTempDate.addMonths(-1);
            js_HideTooltip_Fixed();
            $("#hdnSelectedvalue").val(lTempDate.getFullYear() + "-" + parseInt(lTempDate.getMonth() + 1) + "-" + lTempDate.getDate());
            LoadCalendar(lcomid, lTempDate);
        }

        function OnSuccGet_Table(retval, prm) {
            try {

                document.getElementById("ctl00_MainContent_lblSelection").innerHTML = retval.d[1];
                var cont = retval.d[0];
                var cont0 = retval.d[2];
                $("#newt").html(cont0);
                $("#newt0").html(cont);

                setHeight();
            }
            catch (ex)
            { console.log(ex.message); }
        }

        var lastExecutor = null;
        function Get_InspInfo(UserId, Schedule_Date, VESSEL_NAME, lEndDate, evt, objthis) {

            if (lastExecutor != null)
                lastExecutor.abort();

            var service = Sys.Net.WebServiceProxy.invoke('/' + __app_name + '/JibeWebServiceInspection.asmx', 'asyncGet_InspInfo', false, { "UserId": UserId, "Schedule_Date": Schedule_Date, "VESSEL_NAME": VESSEL_NAME, "lEndDate": lEndDate }, OnSuccGet_Record_Information, Onfail, new Array(evt, objthis));
            lastExecutor = service.get_executor();
        }
        function OnSuccGet_Record_Information(retval, prm) {
            try {
                js_ShowToolTip(retval, prm[0], prm[1]);

            }
            catch (ex)
            { }
        }
        function Onfail(retval) {

        }
        function getCompany() {
            var DropdownList = document.getElementById('<%=ddlVessel_Manager.ClientID %>');
            pFilterCompany = DropdownList.value;
            Current();
        }

        function HighlightDate(Date) {
            $(".DateStyle").removeClass("Highlight");
            $(".DayStyle").removeClass("Highlight");

            $(".DateStyle[rel='" + Date + "']").addClass("Highlight");
            $(".DayStyle[rel='" + Date + "']").addClass("Highlight");
        }

        function OpenInspectionSchedule(VesselID, Surv_Details_ID, Surv_Vessel_ID, OfficeID) {
            document.getElementById('IframeSchInsp').src = "ScheduleInspection.aspx?Page=Calendar&VesselID=" + VesselID + "&Surv_Details_ID=" + Surv_Details_ID + "&Surv_Vessel_ID=" + Surv_Vessel_ID + "&OfficeID=" + OfficeID;
            showModal('dvSchInsp');
            $("#dvSchInsp_dvModalPopupTitle").html('Schedule Inspection');
            $("#dvSchInsp").css("z-index", "99999");
        }

        function SurveyCertificateExpiry(Surv_Details_ID, Surv_Vessel_ID, Vessel_ID, OfficeID, evt, objthis) {
            if (lastExecutor != null)
                lastExecutor.abort();

            var service = Sys.Net.WebServiceProxy.invoke('/' + __app_name + '/JibeWebServiceInspection.asmx', 'asyncGet_SurveyCertificateToolTip', false, { "Surv_Details_ID": Surv_Details_ID, "Surv_Vessel_ID": Surv_Vessel_ID, "Vessel_ID": Vessel_ID, "OfficeID": OfficeID, "Type": "E" }, SurveyCertificateSuccess, Onfail, new Array(evt, objthis));
            lastExecutor = service.get_executor();
        }

        function setHeight() {
            $("#Main_Div").css("min-height", parseInt($("#newt0").height() + 200) + "px");
        }

        function SurveyCertificateSuccess(retval, prm) {

            var json = JSON.parse(retval);

            var CertificateName = json[0]["Survey_Cert_Name"];
            var IssueDate = json[0]["DateOfIssue"].split(' ')[0];
            var CalculatedExpiryDate = json[0]["CalculatedExpiryDate"].split(' ')[0];
            var RenewalInspectionDate = json[0]["RenewalDate"].split(' ')[0];
            var Inspection_ID = json[0]["RenewalInspection_ID"];

            var VesselID = json[0]["Vessel_ID"];
            var Surv_Details_ID = json[0]["Surv_Details_ID"];
            var Surv_Vessel_ID = json[0]["Surv_Vessel_ID"];
            var OfficeID = json[0]["OfficeID"];

            var InspectionCalendar = "";
            if (RenewalInspectionDate == "")///Disable Inspection scheduling icon if already Inspection is created
                InspectionCalendar = "<img class='inspectioncalendar' style='cursor:pointer;' onclick='OpenInspectionSchedule(" + VesselID + "," + Surv_Details_ID + "," + Surv_Vessel_ID + "," + OfficeID + ");' title='Create new inspection' src='../../Images/InspectionCalendar.png '/>";
            else {
                InspectionCalendar = "<img class='inspectioncalendar' src='../../Images/InspectionCalendar.png '/>";

                var Date = RenewalInspectionDate;
                RenewalInspectionDate = "<a href='../../Technical/Inspection/SuperintendentInspection.aspx?InspectionId=" + Inspection_ID + "&CertificateName=" + CertificateName + "' target='_blank' style='cursor:pointer;'>" + Date + "</a>"
            }

            var HTML = "<table cellpadding='3px' style='font-size:11px;color:#0D3E6E;'>";

            HTML += "<tr><td><b>Certificate Name</b></td><td>:</td><td>" + CertificateName + "</td></tr>";
            HTML += "<tr><td><b>Issue Date</b></td><td>:</td><td>" + IssueDate + "</td></tr>";
            HTML += "<tr><td><b>Calculated Expiry Date</b></td><td>:</td><td>" + CalculatedExpiryDate + "</td></tr>";
            if (RenewalInspectionDate == "")
                HTML += "<tr><td><b>Renewal Inspection</b></td><td>:</td><td><div id='spanRenewalInspection'>Not Scheduled</div>" + InspectionCalendar + "</td></tr>";
            else
                HTML += "<tr><td><b>Renewal Inspection</b></td><td>:</td><td><div id='spanRenewalInspection'>" + RenewalInspectionDate + "</div>" + InspectionCalendar + "</td></tr>";

            HTML += "<tr style='height:10px;'><td colspan='3'></td></tr>"
            HTML += "<tr><td colspan='3'  style='text-align: center;'><input type='button' class='VewSurvey' value='View Survey/Certificate' onclick='openSurveyPage(" + VesselID + "," + Surv_Details_ID + "," + Surv_Vessel_ID + "," + OfficeID + ");' /></td></tr>"
            HTML += "</table>";

            js_ShowToolTip_Fixed(HTML, prm[0], prm[1]);

        }

        function SurveyCertificateReminder(Surv_Details_ID, Surv_Vessel_ID, Vessel_ID, OfficeID, evt, objthis) {


            if (lastExecutor != null)
                lastExecutor.abort();

            var service = Sys.Net.WebServiceProxy.invoke('/' + __app_name + '/JibeWebServiceInspection.asmx', 'asyncGet_SurveyCertificateToolTip', false, { "Surv_Details_ID": Surv_Details_ID, "Surv_Vessel_ID": Surv_Vessel_ID, "Vessel_ID": Vessel_ID, "OfficeID": OfficeID, "Type": "R" }, SurveyCertificateReminderSuccess, Onfail, new Array(evt, objthis));
            lastExecutor = service.get_executor();

        }

        function SurveyCertificateReminderSuccess(retval, prm) {

            var json = JSON.parse(retval);
            var CertificateName = json[0]["Survey_Cert_Name"];
            var IssueDate = json[0]["DateOfIssue"].split(' ')[0];
            var CalculatedExpiryDate = json[0]["CalculatedExpiryDate"].split(' ')[0];
            var ReminderText = json[0]["FollowupReminder"];
            if (ReminderText.length > 200) {
                ReminderText = ReminderText.substring(0, 200) + "...";
            }
            var ReminderTextdt = json[0]["FollowupReminderDt"].split(' ')[0];

            var VesselID = json[0]["Vessel_ID"];
            var Surv_Details_ID = json[0]["Surv_Details_ID"];
            var Surv_Vessel_ID = json[0]["Surv_Vessel_ID"];
            var OfficeID = json[0]["OfficeID"];

            var HTML = "<table cellpadding='3px' style='font-size:11px;color:#0D3E6E;'>";

            HTML += "<tr><td><b>Certificate Name</b></td><td>:</td><td>" + CertificateName + "</td></tr>";
            HTML += "<tr><td><b>Issue Date</b></td><td>:</td><td>" + IssueDate + "</td></tr>";
            HTML += "<tr><td><b>Calculated Expiry Date</b></td><td>:</td><td>" + CalculatedExpiryDate + "</td></tr>";
            HTML += "<tr><td><b>Reminder Date</b></td><td>:</td><td>" + ReminderTextdt + "</td></tr>";
            HTML += "<tr><td style='vertical-align: top;'><b>Reminder</b></td><td style='vertical-align: top;'>:</td><td><div class='ReminderTextWrap'>" + ReminderText + "<div></td></tr>";
            HTML += "<tr style='height:10px;'><td colspan='3'></td></tr>"
            HTML += "<tr><td colspan='3'  style='text-align: center;'><input type='button' class='VewSurvey' value='View Survey/Certificate'  onclick='openSurveyPage(" + VesselID + "," + Surv_Details_ID + "," + Surv_Vessel_ID + "," + OfficeID + ");' /></td></tr>"
            HTML += "</table>";

            js_ShowToolTip_Fixed(HTML, prm[0], prm[1]);
        }

        function openSurveyPage(VesselID, Surv_Details_ID, Surv_Vessel_ID, OfficeID) {
            var URL = "../../Surveys/SurveyDetails.aspx?vid=" + VesselID + "&s_v_id=" + Surv_Vessel_ID + "&s_d_id=" + Surv_Details_ID + "&off_id=" + OfficeID + "&page=calendar";
            window.open(URL, "_blank", "", "");
        }

        function BindDate(RenewalDate, ReturnInspectionID, FormattedDate) {
            hideModal("dvSchInsp");
            $("#spanRenewalInspection").html('');
            $("#spanRenewalInspection").html("<a href='../../Technical/Inspection/SuperintendentInspection.aspx?InspectionId=" + ReturnInspectionID + "' target='_blank' style='cursor:pointer;'>" + RenewalDate + "</a>");
            $(".inspectioncalendar").removeAttr("onclick");
            LoadCalendar('', '');
        }


        function BindCSS() {
            var data = $(".VesselStyle");
            data.each(function (index) {
                $(".VesselStyle")[index].parentElement.className = "TOP";
                if ($(".VesselStyle")[index].parentElement.previousElementSibling != null)
                    $(".VesselStyle")[index].parentElement.previousElementSibling.className = "BOTTOM";
            });
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div id="Main_Div">
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
        <asp:UpdatePanel ID="updSupInspCal" runat="server">
            <ContentTemplate>
                <div style="font-size: 12px; width: 100%; height: 100%;">
                    <div id="page-header" class="page-title">
                        Vessel Calendar
                    </div>
                    <table style="width: 100%;">
                        <tr>
                            <td style="font-size: 14px; font-weight: bold; text-align: left; width: 200px">
                                <asp:Label ID="lblSelection" runat="server"></asp:Label>
                            </td>
                            <td style="text-align: left; width: 60px">
                                <asp:Label ID="lblFleet" Text="Company :" runat="server"></asp:Label>
                            </td>
                            <td style="text-align: left;">
                                <asp:DropDownList ID="ddlVessel_Manager" runat="server" Width="135px" onchange="getCompany()">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <div style="text-align: right;">
                                    <asp:HiddenField ID="hdnStartMonth" runat="server" Value="" />
                                    <input type="button" id="MovePrev" value=" < " title="Previous Month" onclick="Prev();" />
                                    <input type="button" id="Cur" value=" Current " onclick="Current();" />
                                    <input type="button" id="MoveNext" value=" > " title="Next Month" onclick="Next();" />
                                </div>
                            </td>
                        </tr>
                    </table>
                    <div id="newt" style="text-align: left; overflow-x: hidden;">
                    </div>
                    <div id="newt0" style="text-align: left; overflow-y: scroll; overflow-x: hidden;
                        border-bottom: 1px solid #c6c6c6; max-height: 800px;">
                    </div>
                    <asp:HiddenField ID="hfcompanyid" runat="server" ClientIDMode="Static" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <asp:HiddenField ID="hdnSelectedvalue" runat="server" ClientIDMode="Static" />
    <div id="dvSchInsp" style="display: none; width: 1000px;" title="Schedule Inspection">
        <iframe id="IframeSchInsp" src="" frameborder="0" style="height: 530px; width: 99%">
        </iframe>
    </div>
</asp:Content>
