<%@ Page Title="JIBE::DashBoard" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="DashBoard_CommonNew.aspx.cs" Inherits="Infrastructure_DashBoard_CommonNew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <script src="../scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/AsyncResponse.js" type="text/javascript"></script>
    <script src="../Scripts/InterviewSchedule_DataHandler.js" type="text/javascript"></script>
    <script src="../Scripts/DocExpiry_DataHandler.js" type="text/javascript"></script>
    <script src="../Scripts/CrewChangeEvent_DataHandler.js" type="text/javascript"></script>
    <script src="../Scripts/CrewContract_DataHandler.js" type="text/javascript"></script>
    <script src="../Scripts/CrewUSVisaAlert_DataHandler.js" type="text/javascript"></script>
    <link href="../Styles/DashBoardCommon.css?v=1" rel="stylesheet" type="text/css" />
    <script src="../Scripts/DashboardCommonNew.js" type="text/javascript"></script>
    <script src="../Scripts/SMSPopup.js" type="text/javascript"></script>
    <link href="../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/common_functions.js" type="text/javascript"></script>
    <script src="../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <link href="../jqxWidgets/jqx.base.css" rel="stylesheet" type="text/css" />
    <link href="../jqxWidgets/Dashboard_Blue.css" rel="stylesheet" type="text/css" />
    <script src="../jqxWidgets/jquery-1.11.1.min.js" type="text/javascript"></script>
    <script src="../jqxWidgets/demos.js" type="text/javascript"></script>
    <script src="../jqxWidgets/jqxcore.js" type="text/javascript"></script>
    <script src="../jqxWidgets/jqxwindow.js" type="text/javascript"></script>
    <script src="../jqxWidgets/jqxdocking.js" type="text/javascript"></script>
    <script src="../jqxWidgets/jqxbuttons.js" type="text/javascript"></script>
    <script src="../jqxWidgets/jqxdraw.js" type="text/javascript"></script>
    <script src="../jqxWidgets/jqxchart.core.js" type="text/javascript"></script>
    <script src="../jqxWidgets/jqxdata.js" type="text/javascript"></script>
    <script type="text/javascript">
        var ForMyActionLabels = "";
        var lastExecutor_ResetDashboard = null;
       // var __isResponse = 1;
        function Reset_Newdashboard() {
            try {


                var UserID = $('[id$=hdnUserID]').val();

                if (lastExecutor_ResetDashboard != null)
                    lastExecutor_ResetDashboard.abort();

                var service = Sys.Net.WebServiceProxy.invoke('../JibeDashboardService.asmx', 'AsyncInsert_DashboardLayout', false, { "User_ID": UserID, "Layout": "" }, OnsuccessReset_Newdashboard, Onfail, null);
                lastExecutor_ResetDashboard = service.get_executor();

            }
            catch (ex) { }

        }
        function OnsuccessReset_Newdashboard() {

            location.reload();

        }

        $(document).ready(function () {



            Load_Newdashboard();


        });

        var lastExecutorDashboard = null;
        function Load_Newdashboard() {
            try {
              //  __isResponse = 0;
              //  setTimeout(function () { if (__isResponse == 0) { document.getElementById('blur-on-updateprogress').style.display = 'block'; } }, 20);

                $("#docking").jqxDocking({ theme: "Dark-Blue" });
                $('#docking').jqxDocking({ keyboardNavigation: true, orientation: 'horizontal', mode: 'docked' });
                $('#docking').jqxDocking({ width: "100%" });
                $('#docking').jqxDocking('hideAllCloseButtons');
                $("#docking").jqxDocking('showAllCollapseButtons');
                $("#docking").jqxDocking('focus');
                $('#docking').jqxDocking({ windowsOffset: 7 });
                $("#expandbutton").jqxButton({ theme: "Dark-Blue" });
                $("#collapsebutton").jqxButton({ theme: "Dark-Blue" });
                $("#btnReset").jqxButton({ theme: "Dark-Blue" });

                var UserID = $('[id$=hdnUserID]').val();
                var DepID = $('[id$=hdfUserdepartmentid]').val();
                if (lastExecutorDashboard != null)
                    lastExecutorDashboard.abort();

                var service = Sys.Net.WebServiceProxy.invoke('../JibeDashboardService.asmx', 'AsyncGet_DashboardSnippets', false, { "User_ID": UserID, "DepID": DepID }, onSuccessasyncBindNewdashboard, Onfail, null);
                lastExecutorDashboard = service.get_executor();

            }
            catch (ex) { }
        }

        function onSuccessasyncBindNewdashboard(dataValues) {

            var jsFunctions_Auto_Refresh = "";
            for (var i = 0; i < dataValues.length; i++) {

                if (document.getElementById(dataValues[i].Snippet_ID + "Title") != null) {
                    if (dataValues[i].Active_Status != "0" && dataValues[i].ACCESS != "0") {

                        if (dataValues[i].ForMyAction == "1") {
                            if (ForMyActionLabels.length > 0)
                                ForMyActionLabels = ForMyActionLabels.concat(",");

                            ForMyActionLabels = ForMyActionLabels.concat(dataValues[i].Snippet_ID);
                        }

                        document.getElementById(dataValues[i].Snippet_ID + "Title").className += dataValues[i].Department_Color;

                        if (dataValues[i].Snippet_Function_Name.toString() != "") {
                            if (dataValues[i].Auto_Refresh == "1") {
                                if (dataValues[i].IsCountSpecific == "1")
                                    jsFunctions_Auto_Refresh = jsFunctions_Auto_Refresh.concat('setTimeout(' + dataValues[i].Snippet_Function_Name + '(' + dataValues[i].FromDay + ',' + dataValues[i].ToDay + '), 200);');
                                else
                                    jsFunctions_Auto_Refresh = jsFunctions_Auto_Refresh.concat('setTimeout(' + dataValues[i].Snippet_Function_Name + ', 200);');
                            }

                            var fnstring = dataValues[i].Snippet_Function_Name;
                            var fn = window[fnstring];
                            if (dataValues[i].IsCountSpecific == "1") {

                                setTimeout(fn(dataValues[i].FromDay, dataValues[i].ToDay), 200);
                            }
                            else {
                                setTimeout(fn(), 200);
                            }


                        }

                    }
                    else {
                        $('#docking').jqxDocking('closeWindow', dataValues[i].Snippet_ID);
                    }
                }
            }

            if (jsFunctions_Auto_Refresh.length > 0) {

                setTimeout(jsFunctions_Auto_Refresh, 300000);
            }

            var DepID = $('[id$=hdfUserdepartmentid]').val();
            if (DepID != null)
                $('[id$=lblDashBoard]').val(dataValues[0].VALUE);

            var $input = $('<img src="../Images/Refresh_14.png" id="btnRefresh" onclick="Refresha(event);" alt="refresh" title="Refresh" style=" display:inline; float:right;margin-right:20px;  cursor:pointer;" />');
            if ($('#btnRefresh').length == 0)
                $(".Dashboard").append($input);


            $('#collapsebutton').on('click', function () {
                for (var i = 0; i < dataValues.length; i++) {
                    if (document.getElementById(dataValues[i].Snippet_ID + "Title") != null)
                        $('#docking').jqxDocking('collapseWindow', dataValues[i].Snippet_ID);
                }
            });



            $('#expandbutton').on('click', function () {
                for (var i = 0; i < dataValues.length; i++) {
                    if (document.getElementById(dataValues[i].Snippet_ID + "Title") != null)
                        $('#docking').jqxDocking('expandWindow', dataValues[i].Snippet_ID);
                }
            });



            $('#docking').on('dragEnd', function (event) {
                displayEvent(event);
            });

            GetDashBoard_Layout();

        }
        var lastExecutor_GetDashboardLayout = null;
        function GetDashBoard_Layout() {
            try {

                var UserID = $('[id$=hdnUserID]').val();

                if (lastExecutor_GetDashboardLayout != null)
                    lastExecutor_GetDashboardLayout.abort();

                var service = Sys.Net.WebServiceProxy.invoke('../JibeDashboardService.asmx', 'AsyncGet_DashboardLayout', false, { "User_ID": UserID }, onSuccessGetDashBoard_Layout, Onfail, null);
                lastExecutor_GetDashboardLayout = service.get_executor();
            }
            catch (ex) { }
        }
        function onSuccessGetDashBoard_Layout(retval) {
            if (retval != "") {
                var PortCallsVesselMinz = false;
                var PortcallsMonthMinz = false;
                if (retval.substr(retval.indexOf('lblWebPartPortCallsVessel') + 40, 4) == 'true') {
                    var strPre = retval.substring(0, retval.indexOf('lblWebPartPortCallsVessel') + 40) + 'false';
                    var strPost = retval.substring(retval.indexOf('lblWebPartPortCallsVessel') + 44, retval.length);
                    retval = strPre + strPost;
                    PortCallsVesselMinz = true;
                }
                if (retval.substr(retval.indexOf('lblWebPartPortcallsMonth') + 39, 4) == 'true') {
                    var strPre = retval.substring(0, retval.indexOf('lblWebPartPortcallsMonth') + 39) + 'false';
                    var strPost = retval.substring(retval.indexOf('lblWebPartPortcallsMonth') + 43, retval.length);
                    retval = strPre + strPost;
                    PortcallsMonthMinz = true;
                }

                $('#docking').jqxDocking('importLayout', retval);

                if (PortCallsVesselMinz == true)
                    $('#docking').jqxDocking('collapseWindow', "lblWebPartPortCallsVessel");

                if (PortcallsMonthMinz == true)
                    $('#docking').jqxDocking('collapseWindow', "lblWebPartPortcallsMonth");
            }
            //__isResponse = 1;
            //document.getElementById('blur-on-updateprogress').style.display = 'none';
        }
        var lastExecutor_DashboardLayout = null;
        function displayEvent(event) {
            try {

                var UserID = $('[id$=hdnUserID]').val();
                var Layout = $('#docking').jqxDocking('exportLayout');
                if (lastExecutor_DashboardLayout != null)
                    lastExecutor_DashboardLayout.abort();

                var service = Sys.Net.WebServiceProxy.invoke('../JibeDashboardService.asmx', 'AsyncInsert_DashboardLayout', false, { "User_ID": UserID, "Layout": Layout }, onSuccessdisplayEvent, Onfail, null);
                lastExecutor_DashboardLayout = service.get_executor();
            }
            catch (ex) { }
        }
        function onSuccessdisplayEvent() {

        }


        function Onfail(msg) {
            alert(msg);
        }

        function Refresha(event) {
            refresh_snippet(event);
            return false;
        }
    </script>
    <script type="text/javascript">


        function ShowCardApprovalDiv(CrewId) {
            document.getElementById("frPopupFrame").src = '../Crew/ProposeCrewCard.aspx?CrewID=' + CrewId;
            document.getElementById("dvPopupFrame").style.display = "block";
            showModal('dvPopupFrame');
        }

        function onLoad() {
            var isOpera = !!window.opera || navigator.userAgent.indexOf(' OPR/') >= 0;
            // Opera 8.0+ (UA detection to detect Blink/v8-powered Opera)
            var isFirefox = typeof InstallTrigger !== 'undefined';   // Firefox 1.0+
            var isSafari = Object.prototype.toString.call(window.HTMLElement).indexOf('Constructor') > 0;
            // At least Safari 3+: "[object HTMLElementConstructor]"
            var isChrome = !!window.chrome && !isOpera;              // Chrome 1+
            var isIE = /*@cc_on!@*/false || !!document.documentMode;   // At least IE6

            if (isFirefox == true) {
                var cont = document.getElementsByClassName("badgeNCR");
                var cont1 = document.getElementsByClassName("badgeNonNCR");

                var len = cont.length;
                var len1 = cont1.length;

                var i = 0;
                for (i = 0; i < cont.length; i++) {

                    cont[i].className = "badgeNCRFF";
                    cont = document.getElementsByClassName("badgeNCR");
                    if (cont.length == 0) {
                        break;
                    }
                    else {
                        i = -1;
                    }
                }

                var i = 0;
                for (i = 0; i < cont1.length; i++) {

                    cont1[i].className = "badgeNonNCRFF";
                    cont1 = document.getElementsByClassName("badgeNonNCR");
                    if (cont1.length == 0) {
                        break;
                    }
                    else {
                        i = -1;
                    }
                }

            }
            if (isSafari == true) {
                var cont = document.getElementsByClassName("badgeNCR");
                var cont1 = document.getElementsByClassName("badgeNonNCR");
                var len = cont.length;
                var len1 = cont1.length;

                var i = 0;
                for (i = 0; i < cont.length; i++) {

                    cont[i].className = "badgeNCRSaf";
                    cont = document.getElementsByClassName("badgeNCR");
                    if (cont.length == 0) {
                        break;
                    }
                    else {
                        i = -1;
                    }
                }

                var i = 0;
                for (i = 0; i < cont1.length; i++) {
                    cont1[i].className = "badgeNonNCRSaf";

                    cont1 = document.getElementsByClassName("badgeNonNCR");
                    if (cont1.length == 0) {
                        break;
                    }
                    else {
                        i = -1;
                    }
                }
            }
            if (isChrome == true) {
                var cont = document.getElementsByClassName("badgeNCR");
                var cont1 = document.getElementsByClassName("badgeNonNCR");
                var len = cont.length;
                var len1 = cont1.length;

                var i = 0;
                for (i = 0; i < cont.length; i++) {
                    cont[i].className = "badgeNCRCRM";

                    cont = document.getElementsByClassName("badgeNCR");
                    if (cont.length == 0) {
                        break;
                    }
                    else {
                        i = -1;
                    }
                }
                var i = 0;
                for (i = 0; i < cont1.length; i++) {
                    cont1[i].className = "badgeNonNCRCRM";
                    cont1 = document.getElementsByClassName("badgeNonNCR");
                    if (cont1.length == 0) {
                        break;
                    }
                    else {
                        i = -1;
                    }
                }
            }
            if (isOpera == true) {
                var cont = document.getElementsByClassName("badgeNCR");
                var cont1 = document.getElementsByClassName("badgeNonNCR");
                var len = cont.length;
                var len1 = cont1.length;

                var i = 0;
                for (i = 0; i < cont.length; i++) {

                    cont[i].className = "badgeNCRCRM";
                    cont = document.getElementsByClassName("badgeNCR");
                    if (cont.length == 0) {
                        break;
                    }
                    else {
                        i = -1;
                    }
                }
                var i = 0;
                for (i = 0; i < cont1.length; i++) {

                    cont1[i].className = "badgeNonNCRCRM";
                    cont1 = document.getElementsByClassName("badgeNonNCR");
                    if (cont1.length == 0) {
                        break;
                    }
                    else {
                        i = -1;
                    }
                }
            }
            $("#lst90").removeClass("bold");
            $("#lst180").removeClass("bold");
            $("#lst365").removeClass("bold");
            $("#lstAll").removeClass("bold");
            $("#lst365").addClass("bold");
        }


        function SetPerformanceManagerDays(days) {
            if (days == -1) {

                //document.getElementById('ctl00_MainContent_WebPartTitle_gwplblWebPartPerformanceManager').innerText = "Performance Manager (All days)";

                $("#lst90").removeClass("bold");
                $("#lst180").removeClass("bold");
                $("#lst365").removeClass("bold");
                $("#lstAll").removeClass("bold");
                $("#lstAll").addClass("bold");
            }
            if (days == 90) {

                //document.getElementById('ctl00_MainContent_WebPartTitle_gwplblWebPartPerformanceManager').innerText = "Performance Manager (Last 90 Days)";


                $("#lst90").removeClass("bold");
                $("#lst180").removeClass("bold");
                $("#lst365").removeClass("bold");
                $("#lstAll").removeClass("bold");
                $("#lst90").addClass("bold");

            }
            if (days == 180) {

                //    document.getElementById('ctl00_MainContent_WebPartTitle_gwplblWebPartPerformanceManager').innerText = "Performance Manager (Last 180 Days)";

                $("#lst90").removeClass("bold");
                $("#lst180").removeClass("bold");
                $("#lst365").removeClass("bold");
                $("#lstAll").removeClass("bold");
                $("#lst180").addClass("bold");

            }
            if (days == 365) {

                //  document.getElementById('ctl00_MainContent_WebPartTitle_gwplblWebPartPerformanceManager').innerText = "Performance Manager (Running 365 Days)";

                $("#lst90").removeClass("bold");
                $("#lst180").removeClass("bold");
                $("#lst365").removeClass("bold");
                $("#lstAll").removeClass("bold");
                $("#lst365").addClass("bold");
            }
            var hiddenDays = document.getElementById('<%=hdnDays.ClientID%>');
            hiddenDays.value = days;
            asyncGet_Performance_Manager();
        }
    </script>
    <style type="text/css">
        .AnomalyCell
        {
            background-color: Red;
            cursor: pointer;
        }
        .NoAnomaly
        {
            background-color: Green;
            cursor: pointer;
        }
        .NoData
        {
            background: url(../Images/noreport.png);
            background-repeat: no-repeat;
        }
        
        .ForMyAction .PartTitleStyle-css
        {
            color: White;
            background-color: Red;
            filter: progid:DXImageTransform.Microsoft.Gradient(GradientType=0,StartColorStr=Red,EndColorStr='#99BBE6');
            background: -webkit-gradient(linear, left top, right top, from(Red), to(#99BBE6));
            background: -moz-linear-gradient(right,  #CEE3F6,  #99BBE6);
            background: linear-gradient(90deg ,Red, #99BBE6);
        }
        
        .badgeNCR
        {
            position: relative;
            text-decoration: none;
        }
        .badgeNCR[data-badge]:after
        {
            content: attr(data-badge);
            position: absolute;
            top: -10px;
            right: -35px;
            background: red;
            color: white;
            width: 20px;
            height: 20px;
            text-align: center;
            text-decoration: none;
            line-height: 20px;
            border-radius: 50%;
        }
        .badgeNonNCR
        {
            position: relative;
            text-decoration: none;
        }
        .badgeNonNCR[data-badge]:after
        {
            content: attr(data-badge);
            position: absolute;
            top: -10px;
            right: -35px;
            background: gray;
            color: white;
            width: 20px;
            height: 20px;
            text-align: center;
            text-decoration: none;
            line-height: 20px;
            border-radius: 50%;
        }
        
        
        
        .badgeNCRFF
        {
            position: relative;
            text-decoration: none;
        }
        .badgeNCRFF[data-badge]:after
        {
            content: attr(data-badge);
            position: absolute;
            top: -0px;
            right: -35px;
            background: red;
            color: white;
            width: 20px;
            height: 20px;
            text-align: center;
            text-decoration: none;
            line-height: 20px;
            border-radius: 50%;
        }
        .badgeNonNCRFF
        {
            position: relative;
            text-decoration: none;
        }
        .badgeNonNCRFF[data-badge]:after
        {
            content: attr(data-badge);
            position: absolute;
            top: -0px;
            right: -35px;
            background: gray;
            color: white;
            width: 20px;
            height: 20px;
            text-align: center;
            text-decoration: none;
            line-height: 20px;
            border-radius: 50%;
        }
        
        
        .badgeNCRCRM
        {
            position: relative;
            text-decoration: none;
        }
        .badgeNCRCRM[data-badge]:after
        {
            content: attr(data-badge);
            position: absolute;
            top: 3px;
            right: -35px;
            background: red;
            color: white;
            width: 20px;
            height: 20px;
            text-align: center;
            text-decoration: none;
            line-height: 20px;
            border-radius: 50%;
        }
        .badgeNonNCRCRM
        {
            position: relative;
            text-decoration: none;
        }
        .badgeNonNCRCRM[data-badge]:after
        {
            content: attr(data-badge);
            position: absolute;
            top: 3px;
            right: -35px;
            background: gray;
            color: white;
            width: 20px;
            height: 20px;
            text-align: center;
            text-decoration: none;
            line-height: 20px;
            border-radius: 50%;
        }
        
        .badgeNCRSaf
        {
            position: relative;
            text-decoration: none;
        }
        .badgeNCRSaf[data-badge]:after
        {
            content: attr(data-badge);
            position: absolute;
            top: -10px;
            right: -40px;
            background: red;
            color: white;
            width: 20px;
            height: 20px;
            text-align: center;
            text-decoration: none;
            line-height: 20px;
            border-radius: 50%;
        }
        .badgeNonNCRSaf
        {
            position: relative;
            text-decoration: none;
        }
        .badgeNonNCRSaf[data-badge]:after
        {
            content: attr(data-badge);
            position: absolute;
            top: -10px;
            right: -40px;
            background: gray;
            color: white;
            width: 20px;
            height: 20px;
            text-align: center;
            text-decoration: none;
            line-height: 20px;
            border-radius: 50%;
        }
        .row-OnBoardCount-Css
        {
            font-size: 10px;
            background-color: transparent;
            color: Black;
            text-align: center;
            font-family: Verdana;
            border: 1px dotted gray;
            padding-left: 5px;
            height: 18px;
        }
        .row-Per-Css
        {
            font-size: 10px;
            background-color: transparent;
            color: Black;
            text-align: left;
            font-family: Verdana;
            border-bottom: 1px solid #cccccc;
            padding-top: 0px;
            padding: 0px;
            border-left: 0px solid white;
            border-right: 0px solid white;
            width: 60px;
        }
    </style>
    <style type="text/css">
        .page
        {
            min-width: 1400px;
            width: 1400px;
            overflow: hidden;
        }
        .Dashboard
        {
            white-space: nowrap;
        }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div id="blur-on-updateprogress" style="display: none">
        <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 2;
            color: black">
            <img src="../Images/loaderbar.gif" alt="Please Wait" />
        </div>
    </div>
    <asp:UpdatePanel ID="updMain" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="page-title" style="border: 1px solid #cccccc; height: 25px; vertical-align: bottom;
                background: url(../Images/bg.png) left -10px repeat-x; color: Black; text-align: left;
                padding: 0px; background-color: #F6CEE3; text-align: center; font-weight: bold">
                <table cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td style="width: 85%; text-align: center;">
                            <asp:Label ID="lblDashBoard" runat="server" Text="Dashboard"></asp:Label>
                        </td>
                        <td style="width: 5%; text-align: right; border-right: 2px solid Transparent">
                            <input type="button" id="btnReset" value="Reset" onclick="Reset_Newdashboard();" />
                        </td>
                        <td style="width: 5%; text-align: center; border-right: 2px solid Transparent">
                            <input type="button" id="collapsebutton" value="Collapse" />
                        </td>
                        <td style="width: 5%; text-align: left; border-right: 2px solid Transparent">
                            <input type="button" id="expandbutton" value="Expand" />
                        </td>
                    </tr>
                </table>
            </div>
            <asp:HiddenField ID="hdnUserID" runat="server" Value="0" />
            <asp:HiddenField ID="hdfUserdepartmentid" runat="server" Value="0" />
            <asp:HiddenField ID="hdfUserCompanyID" runat="server" Value="0" />
            <asp:HiddenField ID="hdnDays" runat="server" Value="365" />
            <div id="page-content" style="border-left: 1px solid #cccccc; border-right: 1px solid #cccccc;
                border-bottom: 1px solid #cccccc; padding: 0px; color: #282829; overflow: hidden;
                width: 100%; background-color: #505050;">
                <%-- <div >--%>
                <div id='jqxWidget'>
                    <div id="docking">
                        <div >
                            <div id="lblWebPartInspectionCompleted" style="max-height: 300px">
                                <div id="lblWebPartInspectionCompletedTitle" class="Dashboard">
                                    Completed Inspections
                                </div>
                                <div style="max-height: 300px">
                                    <div id="dvWebPartInspectionCompleted">
                                    </div>
                                </div>
                            </div>
                            <div id="lblWebPartMyShortcuts" style="max-height: 300px">
                                <div id="lblWebPartMyShortcutsTitle" class="Dashboard">
                                    My Shortcuts
                                </div>
                                <div style="max-height: 300px">
                                    <div id="dvMyShortsCuts_Menu">
                                        <asp:ImageButton ID="btnEditFavourite" runat="server" Text="Edit Favourite" ImageAlign="Baseline"
                                            ImageUrl="~/Images/dash-FavLink-Edit.png" OnClientClick="OpenPopupWindow('POP__Menu', 'Edit Menu', 'Snippets/Dash_Edit_MyShortcuts_Menu.aspx','popup',600,500,null,null,false,false,true,false);return false;"
                                            Height="12px" Width="20px" />
                                    </div>
                                </div>
                            </div>
                            <div id="lblWebPartEvents_Done" style="max-height: 300px">
                                <div id="lblWebPartEvents_DoneTitle" class="Dashboard">
                                    Events done in last 90 days
                                </div>
                                <%--<div style="overflow-y: auto; height: auto; max-height: 300px;">--%>
                                <div style="max-height: 300px">
                                    <div id="dvEvents_Done">
                                    </div>
                                </div>
                                <%--</div>--%>
                            </div>
                            <div id="lblWebPartPerformanceManager" style="max-height: 300px">
                                <div id="lblWebPartPerformanceManagerTitle" class="Dashboard">
                                    Performance Manager
                                </div>
                                <div style="max-height: 300px">
                                    <div id="dvWebPartPerformanceManager">
                                        <table style=" margin-bottom:4px"  >
                                            <tr>
                                                <td>
                                                </td>
                                                <td>
                                                    &nbsp;&nbsp;&nbsp;
                                                </td>
                                                <td>
                                                    <div style="text-align: right">
                                                        <div style="cursor: pointer;" onclick="SetPerformanceManagerDays(90)" id="lst90">
                                                            &nbsp; <span style="text-decoration: underline; font-family: Tahoma ,Tahoma, Sans-Serif">
                                                                Last 90 Days</span> </div>
                                                </td>
                                                <td>
                                                    <div style="cursor: pointer;" onclick="SetPerformanceManagerDays(180)" id="lst180">
                                                        &nbsp; <span style="text-decoration: underline; font-family: Tahoma ,Tahoma, Sans-Serif">
                                                            Last 180 Days</span></div>
                                                </td>
                                                <td>
                                                    <div style="cursor: pointer;" onclick="SetPerformanceManagerDays(365)" id="lst365">
                                                        &nbsp; <span style="text-decoration: underline; font-family: Tahoma ,Tahoma, Sans-Serif">
                                                            Running 365 Days</span> </div>
                                                </td>
                                                <td>
                                                    <div style="cursor: pointer;" onclick="SetPerformanceManagerDays(-1)" id="lstAll">
                                                        &nbsp; <span style="text-decoration: underline; font-family: Tahoma ,Tahoma, Sans-Serif">
                                                            All Days</span> </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div id="divPerManLastUpdated" style="font-family: Tahoma ,Tahoma, Sans-Serif; padding-bottom: 2px">
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div >
                            <div id="lblWebPartOverdueFileScheduleApproval" style="max-height: 300px">
                                <div id="lblWebPartOverdueFileScheduleApprovalTitle" class="Dashboard">
                                    Overdue File Schedules Approval
                                </div>
                                <div style="max-height: 300px">
                                    <div id="dvWebPartOverdueFileScheduleApproval">
                                    </div>
                                </div>
                            </div>
                            <div id="lblWebPartOverdueFileScheduleReceiving" style="max-height: 300px">
                                <div id="lblWebPartOverdueFileScheduleReceivingTitle" class="Dashboard">
                                    Overdue File Receiving
                                </div>
                                <div style="max-height: 300px">
                                    <div id="dvWebPartOverdueFileScheduleReceiving">
                                    </div>
                                </div>
                            </div>
                            <div id="lblWebPartPendingForApproval" style="max-height: 300px">
                                <div id="lblWebPartPendingForApprovalTitle" class="Dashboard">
                                    Pending Approval - Reqsn
                                </div>
                                <div style="max-height: 300px">
                                    <div id="dvPending_ReqsnPO">
                                    </div>
                                </div>
                            </div>
                            <div id="lblWebPartPendingTravelPO" style="max-height: 300px">
                                <div id="lblWebPartPendingTravelPOTitle" class="Dashboard">
                                    Pending Approval - Travel
                                </div>
                                <div style="max-height: 300px">
                                    <div id="dvPending_TravelPO">
                                    </div>
                                </div>
                            </div>
                            <div id="lblWebPartPendingLogisticPO" style="max-height: 300px">
                                <div id="lblWebPartPendingLogisticPOTitle" class="Dashboard">
                                    Pending Approval - Logistic
                                </div>
                                <div style="max-height: 300px">
                                    <div id="dvPending_LogisticPO">
                                    </div>
                                </div>
                            </div>
                            <div id="lblWebPartPendingInterviews_By_User" style="max-height: 300px">
                                <div id="lblWebPartPendingInterviews_By_UserTitle" class="Dashboard">
                                    My Crew Interviews
                                </div>
                                <div style="max-height: 300px">
                                    <div id="dvInterviewSchedules_By_UserID">
                                    </div>
                                </div>
                            </div>
                            <div id="lblWebPartContractToSign" style="max-height: 300px">
                                <div id="lblWebPartContractToSignTitle" class="Dashboard">
                                    Contracts Pending To Be Signed by Office
                                </div>
                                <div style="max-height: 300px">
                                    <div id="dvContractToSign">
                                    </div>
                                </div>
                            </div>
                            <div id="lblWebPartContractToVerify" style="max-height: 300px">
                                <div id="lblWebPartContractToVerifyTitle" class="Dashboard">
                                    Contracts Pending To Be Verified By Office
                                </div>
                                <div style="max-height: 300px">
                                    <div id="dvContractToVerify">
                                    </div>
                                </div>
                            </div>
                            <div id="lblWebPartCrewCardProposed" style="max-height: 300px">
                                <div id="lblWebPartCrewCardProposedTitle" class="Dashboard">
                                    Red Cards / Yellow Cards Proposed
                                </div>
                                <div style="max-height: 300px">
                                    <div id="dvCrewCardProposed">
                                    </div>
                                </div>
                            </div>
                            <div id="lblWebPartSuppliedPROV" style="max-height: 300px">
                                <div id="lblWebPartSuppliedPROVTitle" class="Dashboard">
                                    Provision Last Supplied,with delivery updated
                                </div>
                                <div style="max-height: 300px">
                                    <div id="dvProvisionLastSupplied">
                                    </div>
                                </div>
                            </div>
                            <div id="lblWepPartReqsnCount" style="max-height: 300px">
                                <div id="lblWepPartReqsnCountTitle" class="Dashboard">
                                    Requisitions from Vessels
                                </div>
                                <div style="max-height: 300px">
                                    <div id="dvReqsnCount">
                                    </div>
                                </div>
                            </div>
                            <div id="lblWebpartPendingWorkList" style="max-height: 300px">
                                <div id="lblWebpartPendingWorkListTitle" class="Dashboard">
                                    My WorkList
                                </div>
                                <div style="max-height: 300px">
                                    <div id="dvMyWork_List">
                                    </div>
                                </div>
                            </div>
                            <div id="lblWebPartPendingWorkListVerification" style="max-height: 300px">
                                <div id="lblWebPartPendingWorkListVerificationTitle" class="Dashboard">
                                    Pending WorkList Verifications
                                </div>
                                <div style="max-height: 300px">
                                    <div id="dvPending_WorkList_Verification">
                                    </div>
                                </div>
                            </div>
                            <div id="lblWebPartWorkListDue7Days" style="max-height: 300px">
                                <div id="lblWebPartWorkListDue7DaysTitle" class="Dashboard">
                                    WorkList due in next 7 days
                                </div>
                                <div style="max-height: 300px">
                                    <div id="dvWebPartWorkListDue7Days">
                                    </div>
                                </div>
                            </div>
                            <div id="lblWebPartPendingCTMApproval" style="max-height: 300px">
                                <div id="lblWebPartPendingCTMApprovalTitle" class="Dashboard">
                                    Pending Approval - CTM
                                </div>
                                <div style="max-height: 300px">
                                    <div id="dvWebpartPendingCTMApproval">
                                    </div>
                                </div>
                            </div>
                            <div id="lblWebPartCylinderOilConsumption" style="max-height: 300px">
                                <div id="lblWebPartCylinderOilConsumptionTitle" class="Dashboard">
                                    Cylinder Oil Consumption
                                </div>
                                <div style="max-height: 300px">
                                    <div id="dvWebPartCylinderOilConsumption">
                                    </div>
                                </div>
                            </div>
                            <div id="lblWebPartPendingCrewBriefing" style="max-height: 300px">
                                <div id="lblWebPartPendingCrewBriefingTitle" class="Dashboard">
                                    Pending Crew Briefing
                                </div>
                                <div style="max-height: 300px">
                                    <div id="dvWebPartPendingCrewBriefing">
                                    </div>
                                </div>
                            </div>
                            <div id="lblWebPartPendingCardApproval" style="max-height: 300px">
                                <div id="lblWebPartPendingCardApprovalTitle" class="Dashboard">
                                    Pending Card Approval
                                </div>
                                <div style="max-height: 300px">
                                    <div id="dvWebPartPendingCardApproval">
                                    </div>
                                </div>
                            </div>
                            <div id="lblWebPendingASLEvaluation" style="max-height: 300px">
                                <div id="lblWebPendingASLEvaluationTitle" class="Dashboard">
                                    Pending ASL Approval
                                </div>
                                <div style="max-height: 300px">
                                    <div id="dvWebPendingASLEvaluation">
                                    </div>
                                </div>
                            </div>
                            <div id="lblWebPendingInvoiceApproval" style="max-height: 300px">
                                <div id="lblWebPendingInvoiceApprovalTitle" class="Dashboard">
                                    No. of Pending Invoices
                                </div>
                                <div style="max-height: 300px">
                                    <div id="dvWebPendingInvoiceApproval">
                                    </div>
                                </div>
                            </div>
                            <div id="lblWebOpexVesselReport" style="max-height: 300px">
                                <div id="lblWebOpexVesselReportTitle" class="Dashboard">
                                    Opex(Running 365 Days)
                                </div>
                                <div style="max-height: 300px">
                                    <div id="dvWebOpexVesselReport">
                                    </div>
                                </div>
                            </div>
                            <div id="lblVoyage" style="max-height: 300px">
                                <div id="lblVoyageTitle" class="Dashboard">
                                    Voyage Report
                                </div>
                                <div style="max-height: 300px">
                                    <div id="divVoyage">
                                    </div>
                                </div>
                            </div>
                            <div id="lblCharterBook" style="max-height: 300px">
                                <div id="lblCharterBookTitle" class="Dashboard">
                                    Charter Book
                                </div>
                                <div style="max-height: 300px">
                                    <div id="dvCharterBook">
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div >
                            <div id="lblWebPartInspectionOverdue" style="max-height: 300px;">
                                <div id="lblWebPartInspectionOverdueTitle" class="Dashboard">
                                    Inspection Overdue
                                </div>
                                <div style="max-height: 300px">
                                    <div id="dvWebPartInspectionOverdue">
                                    </div>
                                </div>
                            </div>
                            <div id="lblWebPartInspectionDueInMonth" style="max-height: 300px;">
                                <div id="lblWebPartInspectionDueInMonthTitle" class="Dashboard">
                                    Inspection Due in Next 30 Days
                                </div>
                                <div style="max-height: 300px">
                                    <div id="dvWebPartInspectionDueInMonth">
                                    </div>
                                </div>
                            </div>
                            <div id="lblWebPartAllPendingInterviews" style="max-height: 300px;">
                                <div id="lblWebPartAllPendingInterviewsTitle" class="Dashboard">
                                    All Pending Interviews
                                </div>
                                <div style="max-height: 300px">
                                    <div id="dvInterviewSchedules">
                                    </div>
                                </div>
                            </div>
                            <div id="lblWebPartEvaluations" style="max-height: 300px;">
                                <div id="lblWebPartEvaluationsTitle" class="Dashboard">
                                    Crew evaluation planned for next 7 days
                                </div>
                                <div style="max-height: 300px">
                                    <div id="dvEvaluations">
                                    </div>
                                </div>
                            </div>
                            <div id="lblWebPartUSVisaAlert" style="max-height: 300px;">
                                <div id="lblWebPartUSVisaAlertTitle" class="Dashboard">
                                    List of crew with expired US VISA, or, with no data
                                </div>
                                <div style="max-height: 300px">
                                    <div id="dvUSVisaAlert">
                                    </div>
                                </div>
                            </div>
                            <div id="lblWebPartReqsnProcessing" style="max-height: 300px;">
                                <div id="lblWebPartReqsnProcessingTitle" class="Dashboard">
                                    Requisitions-Avg time taken(in days) at stages
                                </div>
                                <div style="max-height: 300px">
                                    <div id="dvReqsnProcessing">
                                    </div>
                                </div>
                            </div>
                            <div id="lblWebPartDocumentsexpire" style="max-height: 300px;">
                                <div id="lblWebPartDocumentsexpireTitle" class="Dashboard">
                                    Documents expiring in next 90 days or already expired
                                </div>
                                <div style="max-height: 300px">
                                    <div id="dvDocAlerts_Container" style="max-height: 300px; overflow: scroll;">
                                        <div id="dvDocExpiryAlerts">
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div id="lblWebPartCrewChangeAlerts" style="max-height: 300px;">
                                <div id="lblWebPartCrewChangeAlertsTitle" class="Dashboard">
                                    Crew events planned for next 10 days
                                </div>
                                <div style="max-height: 300px">
                                    <div id="dvCrewChangeAlerts">
                                    </div>
                                </div>
                            </div>
                            <div id="lblWebPartPendingNCRBYDept" style="max-height: 300px;">
                                <div id="lblWebPartPendingNCRBYDeptTitle" class="Dashboard">
                                    Pending NCR - Based on your department
                                </div>
                                <div style="max-height: 300px">
                                    <div id="dvPendingNCRByDept">
                                    </div>
                                </div>
                            </div>
                            <div id="lblWebPartCrewComplaints" style="max-height: 300px;">
                                <div id="lblWebPartCrewComplaintsTitle" class="Dashboard">
                                    Crew Complaints
                                </div>
                                <div style="max-height: 300px">
                                    <div id="dvCrewComplaints">
                                    </div>
                                </div>
                            </div>
                            <div id="lblWebPartPendingNCRALLDept" style="max-height: 300px;">
                                <div id="lblWebPartPendingNCRALLDeptTitle" class="Dashboard">
                                    Pending NCR - All department
                                </div>
                                <div style="max-height: 300px">
                                    <div id="dvPendingNCRALLDept">
                                    </div>
                                </div>
                            </div>
                            <div id="lblWebPartPendingNCRByAssignor" style="max-height: 300px;">
                                <div id="lblWebPartPendingNCRByAssignorTitle" class="Dashboard">
                                    Pending NCR PSC - Based on your department
                                </div>
                                <div style="max-height: 300px">
                                    <div id="dvPendingNCRByAssignor">
                                    </div>
                                </div>
                            </div>
                            <div id="lblWebPartPortCallsVessel" style="height: 400px;">
                                <div id="lblWebPartPortCallsVesselTitle" class="Dashboard">
                                    Port calls per Vessel in last 60 days
                                </div>
                                <div style="max-height: 400px">
                                    <div id="dvWebPartPortCallsVessel" style="width: inherit; height: inherit; position: relative;
                                        left: 0px; top: 0px;">
                                    </div>
                                </div>
                            </div>
                            <div id="lblWebPartPortcallsMonth" style="height: 400px;">
                                <div id="lblWebPartPortcallsMonthTitle" class="Dashboard">
                                    Port calls per month in last 12 months
                                </div>
                                <div style="max-height: 400px;">
                                    <div id="dvWebPartPortcallsMonth" style="width: inherit; height: inherit; position: relative;
                                        left: 0px; top: 0px;">
                                    </div>
                                </div>
                            </div>
                            <div id="lblWebPartEvaluationBelow60" style="max-height: 300px;">
                                <div id="lblWebPartEvaluationBelow60Title" class="Dashboard">
                                    Crew evaluations below 60%
                                </div>
                                <div style="max-height: 300px">
                                    <div id="dvWebPartEvaluationBelow60">
                                    </div>
                                </div>
                            </div>
                            <div id="lblWebPartDecklogAnomalies" style="max-height: 300px;">
                                <div id="lblWebPartDecklogAnomaliesTitle" class="Dashboard">
                                    Deck Log Anomalies
                                </div>
                                <div style="max-height: 300px">
                                    <div id="dvWebPartDecklogAnomalies">
                                    </div>
                                </div>
                            </div>
                            <div id="lblWebPartEnginelogAnomalies" style="max-height: 300px;">
                                <div id="lblWebPartEnginelogAnomaliesTitle" class="Dashboard">
                                    Engine Log Anomalies
                                </div>
                                <div style="max-height: 300px">
                                    <div id="dvWebPartEnginelogAnomalies">
                                    </div>
                                </div>
                            </div>
                            <div id="lblWebPartCTMConfirmationFromVessel" style="max-height: 300px;">
                                <div id="lblWebPartCTMConfirmationFromVesselTitle" class="Dashboard">
                                    CTM confirmation not received from vessel
                                </div>
                                <div style="max-height: 300px">
                                    <div id="dvWebPartCTMConfirmationFromVessel">
                                    </div>
                                </div>
                            </div>
                            <div id="lblWebPartMyOperationWorklist" style="max-height: 300px;">
                                <div id="lblWebPartMyOperationWorklistTitle" class="Dashboard">
                                    My Worklist - Operations
                                </div>
                                <div style="max-height: 300px">
                                    <div id="dvMyOperationWorklist">
                                    </div>
                                </div>
                            </div>
                            <div id="lblWebPartOpsWorklistDueIn7Days" style="max-height: 300px;">
                                <div id="lblWebPartOpsWorklistDueIn7DaysTitle" class="Dashboard">
                                    WorkList due in next 7 days - Operations
                                </div>
                                <div style="max-height: 300px">
                                    <div id="dvWebPartOpsWorklistDueIn7Days">
                                    </div>
                                </div>
                            </div>
                            <div id="lblWebPartOpsWorklistOverdue" style="max-height: 300px;">
                                <div id="lblWebPartOpsWorklistOverdueTitle" class="Dashboard">
                                    WorkList Overdue - Operations
                                </div>
                                <div style="max-height: 300px">
                                    <div id="dvWebPartOpsWorklistOverdue">
                                    </div>
                                </div>
                            </div>
                            <div id="lblWebPartSurvDueinNext30Days" style="max-height: 300px;">
                                <div id="lblWebPartSurvDueinNext30DaysTitle" class="Dashboard">
                                    Surveys due in next 30 days
                                </div>
                                <div style="max-height: 300px">
                                    <div id="dvWebSurvDueinNext30Days">
                                    </div>
                                </div>
                            </div>
                            <div id="lblWebPartSurvDueinNext7DaysAndOverdue" style="max-height: 300px;">
                                <div id="lblWebPartSurvDueinNext7DaysAndOverdueTitle" class="Dashboard">
                                    Surveys due in next 7 days and overdue
                                </div>
                                <div style="max-height: 300px">
                                    <div id="dvSurvDueinNext7DaysAndOverdue">
                                    </div>
                                </div>
                            </div>
                            <div id="lblWebPartSurvPendingVerification" style="max-height: 300px;">
                                <div id="lblWebPartSurvPendingVerificationTitle" class="Dashboard">
                                    Surveys (Active) pending for verification
                                </div>
                                <div style="max-height: 300px">
                                    <div id="dvSurvPendingVerification">
                                    </div>
                                </div>
                            </div>
                            <div id="lblWebPartSurvNAPendingVerification" style="max-height: 300px;">
                                <div id="lblWebPartSurvNAPendingVerificationTitle" class="Dashboard">
                                    Surveys (Not Active) pending for verification
                                </div>
                                <div style="max-height: 300px">
                                    <div id="dvSurvNAPendingVerification">
                                    </div>
                                </div>
                            </div>
                            <div id="lblWebPartSurvExpiryin31to90days" style="max-height: 300px;">
                                <div id="lblWebPartSurvExpiryin31to90daysTitle" class="Dashboard">
                                    Surveys expiry in 31-90 days
                                </div>
                                <div style="max-height: 300px">
                                    <div id="dvSurvExpiryin31to90days">
                                    </div>
                                </div>
                            </div>
                            <div id="lblWebPartSurvExpiryin7to30days" style="max-height: 300px;">
                                <div id="lblWebPartSurvExpiryin7to30daysTitle" class="Dashboard">
                                    Surveys expiry in 7-30 days
                                </div>
                                <div style="max-height: 300px">
                                    <div id="dvSurvExpiryin7to30days">
                                    </div>
                                </div>
                            </div>
                            <div id="lblWebPartSurvExpiryinLessThen7days" style="max-height: 300px;">
                                <div id="lblWebPartSurvExpiryinLessThen7daysTitle" class="Dashboard">
                                    Surveys expiry in less than 7 days or already overdue
                                </div>
                                <div style="max-height: 300px">
                                    <div id="divSurvExpiryinLessThen7days">
                                    </div>
                                </div>
                            </div>
                            <!-- End Snippets for Survey -->
                            <div id="lblWebPartPMSOverdueJobs" style="max-height: 300px;">
                                <div id="lblWebPartPMSOverdueJobsTitle" class="Dashboard">
                                    PMS Overdue Jobs
                                </div>
                                <div style="max-height: 300px">
                                    <div id="dvWebPartPMSOverdueJobs">
                                    </div>
                                </div>
                            </div>
                            <div id="lblWebPartBelowTresholdInventoryItems" style="max-height: 300px;">
                                <div id="lblWebPartBelowTresholdInventoryItemsTitle" class="Dashboard">
                                    Inventory Items Below Treshold
                                </div>
                                <div style="max-height: 300px">
                                    <div id="dvWebPartBelowTresholdInventoryItems">
                                    </div>
                                </div>
                            </div>
                            <div id="lblWebPartCrewPerformance" style="max-height: 300px;">
                                <div id="lblWebPartCrewPerformanceTitle" class="Dashboard">
                                    Crew Performance
                                </div>
                                <div style="max-height: 300px">
                                    <div id="dvWebPartCrewPerformance">
                                    </div>
                                </div>
                            </div>
                            <div id="lblWebPartEvaluationDue" style="max-height: 300px;">
                                <div id="lblWebPartEvaluationDueTitle" class="Dashboard">
                                    Evaluation Overdue
                                </div>
                                <div style="max-height: 300px">
                                    <div id="dvWebPartCrewEvaluationDue">
                                    </div>
                                </div>
                            </div>
                            <div id="lblWebPartCrewEvaluationFeedback" style="max-height: 300px;">
                                <div id="lblWebPartCrewEvaluationFeedbackTitle" class="Dashboard">
                                    Pending Feedback Request
                                </div>
                                <div style="max-height: 300px">
                                    <div id="dvWebPartCrewEvaluationFeedback">
                                    </div>
                                </div>
                            </div>
                            <div id="lblWebPartCrewPerformanceVerification" style="max-height: 300px;">
                                <div id="lblWebPartCrewPerformanceVerificationTitle" class="Dashboard">
                                    Crew Performance Verification
                                </div>
                                <div style="max-height: 300px">
                                    <div id="dvWebPartCrewPerformanceVerification">
                                    </div>
                                </div>
                            </div>
                            <div id="lblWebPartCrewONBDList" style="max-height: 300px;">
                                <div id="lblWebPartCrewONBDListTitle" class="Dashboard">
                                    Onboard Crew Count
                                </div>
                                <div style="max-height: 300px">
                                    <div id="dvWebPartCrewONBDList">
                                        <asp:LinkButton ID="lnkSendMail" runat="server" BackColor="Yellow" OnClientClick="SendMail('lblWebPartCrewONBDList');return false;">FollowUp</asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                            <div id="lblWebPartCrewSeniorityReward" style="max-height: 300px;">
                                <div id="lblWebPartCrewSeniorityRewardTitle" class="Dashboard">
                                    Seniority Reward
                                </div>
                                <div style="max-height: 300px">
                                    <div id="dvWebPartCrewSeniorityReward">
                                    </div>
                                </div>
                            </div>
                            <div id="lblWebPartWorklistIncident180Days" style="max-height: 300px;">
                                <div id="lblWebPartWorklistIncident180DaysTitle" class="Dashboard">
                                    Incidents reported in last 180 Days
                                </div>
                                <div style="max-height: 300px">
                                    <div id="dvWebPartWorklistIncident180Days">
                                    </div>
                                </div>
                            </div>
                            <div id="lblWebPartWorklistNearmiss180Days" style="max-height: 300px;">
                                <div id="lblWebPartWorklistNearmiss180DaysTitle" class="Dashboard">
                                    Near miss reported in last 180 Days
                                </div>
                                <div style="max-height: 300px">
                                    <div id="dvWebPartWorklistNearmiss180Days">
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <%-- </div>--%>
                </div>
            </div>
            <asp:Label ID="lblErrorMessage" Font-Size="11px" Font-Italic="true" ForeColor="Red"
                Visible="false" runat="server"></asp:Label>
            <div id="dvPopupFrame" class="draggable" style="display: none; background-color: #CBE1EF;
                border-color: #5C87B2; border-style: solid; border-width: 1px; position: absolute;
                left: 0.5%; top: 15%; width: 40%; height: 550px; z-index: 1; color: black" title=''>
                <div class="content">
                    <iframe id="frPopupFrame" src="" frameborder="0" height="550px" width="100%"></iframe>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
