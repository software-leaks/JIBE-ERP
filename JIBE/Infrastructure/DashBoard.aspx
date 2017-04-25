<%@ Page Title="JIBE::DashBoard" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="DashBoard.aspx.cs" Inherits="Infrastructure_DashBoard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <script src="../scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/AsyncResponse.js" type="text/javascript"></script>
    <script src="../Scripts/InterviewSchedule_DataHandler.js" type="text/javascript"></script>
    <script src="../Scripts/DocExpiry_DataHandler.js?v=1" type="text/javascript"></script>
    <script src="../Scripts/CrewChangeEvent_DataHandler.js" type="text/javascript"></script>
    <script src="../Scripts/CrewContract_DataHandler.js" type="text/javascript"></script>
    <script src="../Scripts/CrewUSVisaAlert_DataHandler.js" type="text/javascript"></script>
    <link href="../Styles/DashBoardCommon.css?v=1" rel="stylesheet" type="text/css" />
    <script src="../Scripts/DashboardCommonNew.js?v=1" type="text/javascript"></script>
    <script src="../Scripts/SMSPopup.js" type="text/javascript"></script>
    <link href="../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/common_functions.js" type="text/javascript"></script>
    <script src="../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <link href="../jqxWidgets/styles/jqx.base.css" rel="stylesheet" type="text/css" />
    <link href="../jqxWidgets/styles/Dashboard_Blue.css" rel="stylesheet" type="text/css" />
    <script src="../jqxWidgets/scripts/jquery-1.11.1.min.js" type="text/javascript"></script>
    <script src="../jqxWidgets/scripts/demos.js" type="text/javascript"></script>
    <script src="../jqxWidgets/Controls/jqxcore.js" type="text/javascript"></script>
    <script src="../jqxWidgets/Controls/jqxwindow.js" type="text/javascript"></script>
    <script src="../jqxWidgets/Controls/jqxdocking.js" type="text/javascript"></script>
    <script src="../jqxWidgets/Controls/jqxbuttons.js" type="text/javascript"></script>
    <script src="../jqxWidgets/Controls/jqxdraw.js" type="text/javascript"></script>
    <script src="../jqxWidgets/Controls/jqxchart.core.js" type="text/javascript"></script>
    <script src="../jqxWidgets/Controls/jqxdata.js" type="text/javascript"></script>
    <script type="text/javascript">
        var ForMyActionLabels = "";

        var lastExecutor_ResetDashboard = null;
        //var __isResponse = 1;
        function Reset_Newdashboard() {
            try {
                var r = confirm("This will reset position and settings of all snippets !");
                if (r == true) {
                    var DepID = $('[id$=hdfUserdepartmentid]').val();
                    var UserID = $('[id$=hdnUserID]').val();

                    if (lastExecutor_ResetDashboard != null)
                        lastExecutor_ResetDashboard.abort();

                    var service = Sys.Net.WebServiceProxy.invoke('../JibeDashboardService.asmx', 'AsyncInsert_DashboardLayout', false, { "User_ID": UserID, "Layout": "", "DepID": DepID }, OnsuccessReset_Newdashboard, Onfail, null);
                    lastExecutor_ResetDashboard = service.get_executor();
                }
            }
            catch (ex) { }

        }
        function OnsuccessReset_Newdashboard() {

            location.reload();

        }
        var lastExecutor_DefaultDashboard = null;
        function SetDefault_Newdashboard() {
            try {
                var r = confirm("This will set the current position and settings as Default for all snippets !");
                if (r == true) {
                    var DepID = $('[id$=hdfUserdepartmentid]').val();
                    var UserID = $('[id$=hdnUserID]').val();

                    if (lastExecutor_DefaultDashboard != null)
                        lastExecutor_DefaultDashboard.abort();

                    var service = Sys.Net.WebServiceProxy.invoke('../JibeDashboardService.asmx', 'AsyncInsert_DefaultDashboard', false, { "User_ID": UserID }, OnsuccessDefault_Newdashboard, Onfail, null);
                    lastExecutor_DefaultDashboard = service.get_executor();
                }
            }
            catch (ex) { }
        }
        function OnsuccessDefault_Newdashboard() {
            alert('Default position and setting saved successfully !');
        }
        $(document).keydown(function (e) {

            if (e.keyCode == 27 && $('#popupMagnifySnippet').css('display') == 'block') {
                $("#popupMagnifySnippet").hide();
            }
        });
        $(document).ready(function () {
            //document.getElementById('blur-on-updateprogress').style.display = 'block';
            Load_Newdashboard();
        });

        $(document).click(function (event) {
            if (this.id != 'ColorPickerContainer' && event.target.id != 'btnSetting') {
                $("#ColorPickerContainer").hide();
            }
        });

        var lastExecutorDashboard = null;
        function Load_Newdashboard() {
            try {
                // __isResponse = 0;
                // setTimeout(function () { if (__isResponse == 0) { document.getElementById('blur-on-updateprogress').style.display = 'block'; } }, 20);

                //$("#docking").jqxDocking({ theme: "Dark-Blue" });

                $("#expandbutton").jqxButton({ theme: "Dark-Blue" });
                $("#collapsebutton").jqxButton({ theme: "Dark-Blue" });
                $("#btnReset").jqxButton({ theme: "Dark-Blue" });
                try {
                    $("[id$=btnSetDefault]").jqxButton({ theme: "Dark-Blue" });
                } catch (ex) { }
                var DepID = $('[id$=hdfUserdepartmentid]').val();

                GetDashBoard_Layout(DepID);
            }
            catch (ex) { }
        }

        var lastExecutor_GetDashboardLayout = null;
        function GetDashBoard_Layout(DepID) {
            try {

                var UserID = $('[id$=hdnUserID]').val();

                if (lastExecutor_GetDashboardLayout != null)
                    lastExecutor_GetDashboardLayout.abort();

                //time span for new data
                var d = new Date();
                var timespan = d.getTime();

                var service = Sys.Net.WebServiceProxy.invoke('../JibeDashboardService.asmx', 'AsyncGet_DashboardLayout', false, { "User_ID": UserID, "DepID": DepID, "TimeSpan": timespan }, onSuccessGetDashBoard_Layout, Onfail, null);
                lastExecutor_GetDashboardLayout = service.get_executor();
            }
            catch (ex) { }
        }
        function onSuccessGetDashBoard_Layout(retval) {
            document.getElementById('docking').style.display = 'block';
            $('#docking').jqxDocking({ keyboardNavigation: true, orientation: 'horizontal', mode: 'docked' });
            $('#docking').jqxDocking({ width: "100%" });
            $('#docking').jqxDocking('hideAllCloseButtons');
            $("#docking").jqxDocking('showAllCollapseButtons');
            $("#docking").jqxDocking('focus');
            $('#docking').jqxDocking({ windowsOffset: 7 });

            if (retval != "") {
                var Layout = retval.replace(/true/g, 'false');
                $('#docking').jqxDocking('importLayout', Layout);
                //$('#docking').jqxDocking('importLayout', retval);
            }
            document.getElementById('blur-on-updateprogress').style.display = 'none';

            var UserID = $('[id$=hdnUserID]').val();
            var DepID = $('[id$=hdfUserdepartmentid]').val();
            if (lastExecutorDashboard != null)
                lastExecutorDashboard.abort();

            //time span for new data
            var d = new Date();
            var timespan = d.getTime();

            var service = Sys.Net.WebServiceProxy.invoke('../JibeDashboardService.asmx', 'AsyncGet_DashboardSnippets', false, { "User_ID": UserID, "DepID": DepID, "TimeSpan": timespan }, onSuccessasyncBindNewdashboard, Onfail, new Array(retval));
            lastExecutorDashboard = service.get_executor();

        }



        function onSuccessasyncBindNewdashboard(dataValues, retval1) {
            var Department = "";
            var jsFunctions_Auto_Refresh = "";
            var Iscollaps = false;
            var retvalstring = retval1.toString();

            for (var i = 0; i < dataValues.length; i++) {
                if (document.getElementById(dataValues[i].Snippet_ID + "Title") != null) {
                    var Snipt = retvalstring.indexOf(dataValues[i].Snippet_ID) + dataValues[i].Snippet_ID.length + 15;

                    ///Check active status and Access to the snippets
                    if (dataValues[i].Active_Status != "0" && dataValues[i].ACCESS != "0") {
                        $("#" + dataValues[i].Snippet_ID).show();
                        Department = dataValues[i].VALUE;
                        if (dataValues[i].ForMyAction == "1") {
                            if (ForMyActionLabels.length > 0)
                                ForMyActionLabels = ForMyActionLabels.concat(",");

                            ForMyActionLabels = ForMyActionLabels.concat(dataValues[i].Snippet_ID);
                        }

                        document.getElementById(dataValues[i].Snippet_ID + "Title").className += dataValues[i].Department_Color;
                        $('div#' + dataValues[i].Snippet_ID + 'Title').addClass(dataValues[i].HeaderColourCss);
                        $($('div#' + dataValues[i].Snippet_ID + 'Title')[0].firstElementChild).text(dataValues[i].HeaderTitle);

                        if (retvalstring.substr(Snipt, 1) == "f") {///Snippts which are not collapsed
                            if (dataValues[i].Snippet_Function_Name.toString() != "") {

                                if (dataValues[i].Auto_Refresh == "1") {
                                    jsFunctions_Auto_Refresh = jsFunctions_Auto_Refresh.concat('setTimeout(' + dataValues[i].Snippet_Function_Name + '(), 200);');
                                }
                                $("#" + dataValues[i].Snippet_ID).attr("rel", dataValues[i].Snippet_Function_Name);

                                var fnstring = dataValues[i].Snippet_Function_Name;
                                var fn = window[fnstring];
                                if (dataValues[i].IsCountSpecific == "1") {
                                    if (dataValues[i].Snippet_ID == 'lblWebPartSurvExpiryin31to90days') {
                                        $("[id$=hdnFromDays31]").val(dataValues[i].FromDay);
                                        $("[id$=hdnToDays90]").val(dataValues[i].ToDay);
                                    } else if (dataValues[i].Snippet_ID == 'lblWebPartSurvExpiryin7to30days') {
                                        $("[id$=hdnFromDays8]").val(dataValues[i].FromDay);
                                        $("[id$=hdnToDays30]").val(dataValues[i].ToDay);
                                    } else if (dataValues[i].Snippet_ID == 'lblWebPartSurvExpiryinLessThen7days') {
                                        $("[id$=hdnFromDays0]").val(dataValues[i].FromDay);
                                        $("[id$=hdnToDays7]").val(dataValues[i].ToDay);
                                    }
                                }
                                setTimeout(fn, 200);
                            }
                        }

                        $('#' + dataValues[i].Snippet_ID).on('focus', function (event) {
                            if ($('#dvEditTitle' + event.target.id).length == 0)
                                $('#' + event.target.id).blur();

                        });
                    }
                    else {
                        $('#docking').jqxDocking('closeWindow', dataValues[i].Snippet_ID);
                    }
                }
            }

            if (jsFunctions_Auto_Refresh.length > 0) {
                function Refresh_Functions() {
                    setTimeout(jsFunctions_Auto_Refresh, 200);
                    setTimeout(Refresh_Functions, 300000);
                }
                setTimeout(Refresh_Functions, 300000);
            }

            var DepID = $('[id$=hdfUserdepartmentid]').val();
            if (DepID != null && DepID != 0)
                $('[id$=lblDashBoard]').html(Department + ' Dashboard');
            //$('[id$=lblDashBoard]').val(Department + ' Dashboard');

            var $input = $('<img src="../Images/Refresh_14.png" id="btnRefresh" onclick="Refresha(event);" alt="refresh" title="Refresh" style=" display:inline; float:right;margin-right:20px;  cursor:pointer;" />');
            if ($('#btnRefresh').length == 0)
                $(".Dashboard").append($input);

            var $input1 = $('<img src="../Images/rsz_zoom-in-2-xxl.png" id="btnMagnify" class="btnMagnify" onclick="Magnify(event);" alt="magnify" title="Magnify" style=" display:inline; float:right;margin-right:8px;  cursor:pointer;" />');
            if ($('#btnMagnify').length == 0)
                $(".Dashboard").append($input1);

            var $input2 = $('<img src="../Images/rsz_settings-17-xxl.png" id="btnSetting" onclick="Setting(event);" alt="setting" title="Setting" style=" display:inline; float:right;margin-right:8px;  cursor:pointer;" />');
            if ($('#btnSetting').length == 0)
                $(".Dashboard").append($input2);

   
            $('#collapsebutton').on('click', function (event) {
                for (var i = 0; i < dataValues.length; i++) {
                    if (document.getElementById(dataValues[i].Snippet_ID + "Title") != null)
                        $('#docking').jqxDocking('collapseWindow', dataValues[i].Snippet_ID);
                }
                displayEvent(event, DepID);
            });


            $('#expandbutton').on('click', function (event) {
                for (var i = 0; i < dataValues.length; i++) {
                    if (document.getElementById(dataValues[i].Snippet_ID + "Title") != null)
                        $('#docking').jqxDocking('expandWindow', dataValues[i].Snippet_ID);
                }
                displayEvent(event, DepID);

                var CollapsedSnippets = $(".jqx-icon-arrow-down");
                CollapsedSnippets.each(function (index) {
                    var innerHtmlVerbs = $(".jqx-icon-arrow-down")[index].parentElement.parentElement.id.toString();
                    $("#" + innerHtmlVerbs.replace("Title", "") + " .jqx-window-content .SnippetsLoader").remove();
                    $("#" + innerHtmlVerbs.replace("Title", "") + " .jqx-window-content").append("<img src='../Images/loader.gif' class='SnippetsLoader' />");

                    setTimeout(function () {
                        refresh_snippet(innerHtmlVerbs);
                    }, 300);
                });
            });

            $('.jqx-window-collapse-button-background').on('click', function (event) {
                ///Calling snippets expand manually 
                displayEvent(event, DepID);
                $('#docking').jqxDocking('expandWindow', event.target.parentNode.parentNode.parentNode.parentNode.id);
            });

            $('#docking').on('dragEnd', function (event) {
                displayEvent(event, DepID);
            });

            var PortCallsVesselMinz = false;
            var PortcallsMonthMinz = false;
            var retval = retval1.toString();
            if (retval != "") {
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
                var OpexVesselReport = false;

                if (retval.substr(retval.indexOf('lblWebOpexVesselReport') + 37, 4) == 'true') {
                    var strPre = retval.substring(0, retval.indexOf('lblWebOpexVesselReport') + 37) + 'false';
                    var strPost = retval.substring(retval.indexOf('lblWebOpexVesselReport') + 41, retval.length);
                    retval = strPre + strPost;
                    OpexVesselReport = true;
                }
                setTimeout(function () {
                    $('#docking').jqxDocking('importLayout', retval);
                }, 2000);

                if (PortCallsVesselMinz == true) {
                    setTimeout(function () {
                        $('#docking').jqxDocking('collapseWindow', "lblWebPartPortCallsVessel");
                    }, 500);

                }
                if (PortcallsMonthMinz == true) {
                    setTimeout(function () {
                        $('#docking').jqxDocking('collapseWindow', "lblWebPartPortcallsMonth");
                    }, 500);

                }

                if (OpexVesselReport == true) {
                    setTimeout(function () {
                        $('#docking').jqxDocking('collapseWindow', "lblWebOpexVesselReport");
                    }, 2000);

                }
            }
        }

        $(document).ready(function () {
            $("body").on("click", ".jqx-icon-arrow-down", function () {
                var innerHtmlVerbs = this.parentElement.parentElement.id.toString();

                ///Show loader in snippets
                $("#" + innerHtmlVerbs.replace("Title", "") + " .jqx-window-content .SnippetsLoader").remove();
                $("#" + innerHtmlVerbs.replace("Title", "") + " .jqx-window-content").append("<img src='../Images/loader.gif' class='SnippetsLoader' />");

                ///Call snippets methods
                setTimeout(function () {
                    refresh_snippet(innerHtmlVerbs);
                }, 300);
            });

            $("body").on("click", ".btnMagnify", function () {
                $("#btnRefreshSnippet").click();
            });
        });


        var lastExecutor_DashboardLayout = null;
        function displayEvent(event, DepID) {
            try {

                var UserID = $('[id$=hdnUserID]').val();
                var Layout = $('#docking').jqxDocking('exportLayout');


                if (event.target != null && event.target.id == 'expandbutton')
                    Layout = Layout.replace(/true/g, 'false');
                else if (event.target != null && event.target.id == 'collapsebutton')
                    Layout = Layout.replace(/false/g, 'true');
                else if (event.currentTarget.className == 'jqx-window-collapse-button-background') {
                    var id = (event.target.parentElement.parentElement.id).substr(0, (event.target.parentElement.parentElement.id).indexOf('Title'));
                    if ((event.target.className.toString()).indexOf("arrow-down") > -1) {
                        var strPre = Layout.substring(0, Layout.indexOf(id) + (id.length + 15)) + 'false';
                        var strPost = Layout.substring(Layout.indexOf(id) + (id.length + 19), Layout.length);
                        Layout = strPre + strPost;
                    } else {
                        var strPre = Layout.substring(0, Layout.indexOf(id) + (id.length + 15)) + 'true';
                        var strPost = Layout.substring(Layout.indexOf(id) + (id.length + 20), Layout.length);
                        Layout = strPre + strPost;
                    }
                }
                else if (event.target.id == 'btnSetting' && (event.target.parentElement.firstElementChild.nextElementSibling.nextElementSibling.firstElementChild.className.toString()).indexOf("arrow-down") > -1) {
                    var id = event.target.parentElement.parentElement.parentElement.id;
                    var strPre = Layout.substring(0, Layout.indexOf(id) + (id.length + 15)) + 'false';
                    var strPost = Layout.substring(Layout.indexOf(id) + (id.length + 19), Layout.length);
                    Layout = strPre + strPost;
                }
                if (lastExecutor_DashboardLayout != null)
                    lastExecutor_DashboardLayout.abort();



                if (isJson(Layout)) { /// Validate JSON - if valid then only save layout, else show alert       
                    var service = Sys.Net.WebServiceProxy.invoke('../JibeDashboardService.asmx', 'AsyncInsert_DashboardLayout', false, { "User_ID": UserID, "Layout": Layout, "DepID": DepID }, onSuccessdisplayEvent, Onfail, null);
                    lastExecutor_DashboardLayout = service.get_executor();
                }
                else {
                    alert("Error while saving the dashboard layout, Please try again");
                }

            }
            catch (ex) { console.log(ex); }
        }
        function onSuccessdisplayEvent() {

        }

        ///Validating layout JSON
        function isJson(str) {
            try {
                JSON.parse(str);
            } catch (e) {
                return false;
            }
            return true;
        }

        function Onfail(msg) {
            console.log(msg);
        }

        function Refresha(event) {
            var innerHtmlVerbs = event.target.parentElement.id.toString();
            refresh_snippet(innerHtmlVerbs);
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

        function CreateNewVetting(Vetting_Type_ID) {
            document.getElementById('IframeCreateVetting').src = "../Technical/Vetting/Vetting_CreateNewVetting.aspx?Vetting_Type_ID=" + Vetting_Type_ID
            showModal('dvPopupCreateVetting');
            $("#dvPopupCreateVetting").prop('title', 'Create New Vetting ');
            return false;
        }

        function VettingDetail(Vetting_ID) {
            hideModal("dvPopupCreateVetting");
            window.open("Vetting_Details.aspx?Vetting_Id=" + Vetting_ID);
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
        
        .ForMyAction .jqx-widget-header, .MagnifyAction
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
            margin: -10px 0px 0px -10px;
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
            margin: -10px 0px 0px -10px;
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
            position: absolute; /*top: -0px;*/
            margin: -10px 0px 0px -10px;
            right: -35px;
            background: red;
            color: white;
            width: 20px;
            height: 20px;
            text-align: center;
            text-decoration: none;
            line-height: 20px;
            border-radius: 50%;
            font-weight: bold;
        }
        
        .badgeNonNCRFF
        {
            position: relative;
            text-decoration: none;
        }
        .badgeNonNCRFF[data-badge]:after
        {
            content: attr(data-badge);
            position: absolute; /*top: -0px;*/
            margin: -10px 0px 0px -10px;
            right: -35px;
            background: gray;
            color: white;
            width: 20px;
            height: 20px;
            text-align: center;
            text-decoration: none;
            line-height: 20px;
            border-radius: 50%;
            font-weight: bold;
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
            margin: -10px 0px 0px -10px;
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
            margin: -10px 0px 0px -10px;
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
            margin: -10px 0px 0px -10px;
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
            margin: -10px 0px 0px -10px;
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
        .redDiv
        {
            background-color: #660066;
        }
        .blackDiv
        {
            background-color: Black;
        }
        .lightBlueDiv
        {
            background-color: rgba(96, 170, 210, 1);
        }
        .greenDiv
        {
            background-color: #5C8A00;
        }
        .orangeDiv
        {
            background-color: #CC5200;
        }
        .maroonDiv
        {
            background-color: #800000;
        }
        .purpleDiv
        {
            background-color: #330066;
        }
        .darkBlueDiv
        {
            background-color: #0033CC;
        }
        .yellowDiv
        {
            background-color: #B28F00;
        }
        .ThemeDiv
        {
            background-color: rgba(58, 79, 99, 1);
        }
        .colorTD
        {
            height: 12px;
            width: 15px;
            cursor: pointer;
            margin-right: 4px;
        }
        .arrow-up
        {
            width: 0;
            height: 0;
            border-left: 10px solid transparent;
            border-right: 10px solid transparent;
            border-bottom: 10px solid #CCCCCC;
        }
        .page
        {
            min-width: 1400px;
            width: 1400px; /*overflow: hidden;*/ /* width: 100%;*/
        }
        .Dashboard
        {
            white-space: nowrap;
            color: White;
        }
        .MagnifyDiv
        {
        }
        .RefreshSnippet
        {
        }
        .editTitle
        {
            display: inline;
            float: right;
            margin-right: 8px;
            cursor: pointer;
        }
        .editColor
        {
            display: inline;
            float: right;
            margin-right: 8px;
            cursor: pointer;
        }
        .dvSetting
        {
            background-color: White;
            display: table;
            position: relative;
            display: inline-block;
            vertical-align: middle;
            padding-top: 3px;
            height: 25px;
            border-bottom: 1px solid silver;
            width: 100%;
        }
        .dvSettingColor
        {
            background-color: White;
            display: table;
            position: relative;
            display: inline-block;
            vertical-align: middle;
            padding-top: 3px;
            height: 25px;
            border-bottom: 1px solid silver;
            width: 100%;
        }
        .rowHeight{height: 25px;}
        .clsHitmap{width: 90px;float: left;}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div id="blur-on-updateprogress" style="display: block">
        <div id="divProgress" style="position: absolute; left: 49%; top: 200px; z-index: 2;
            width: 100%; height: 100%; color: black">
            <img src="../Images/loaderbar.gif" alt="Please Wait" />
        </div>
    </div>
    <asp:UpdatePanel ID="updMain" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="page-title" style="border: 1px solid #cccccc; height: 25px; vertical-align: bottom;
                background: url(../Images/bg.png) left -10px repeat-x; color: Black; text-align: left;
                padding: 0px; background-color: #F6CEE3; text-align: center; font-weight: bold;">
                <table cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td style="width: 85%; padding-left: 20%">
                            <asp:Label ID="lblDashBoard" runat="server" Text="Dashboard"></asp:Label>
                            <img src="../Images/loader.gif" class="SnippetsLoader" style="display:none;">
                        </td>
                        <td style="width: 10%; text-align: right; border-right: 2px solid Transparent">
                            <input type="button" id="btnSetDefault" runat="server" value="Save Current Setting as Default for all Users"
                                onclick="SetDefault_Newdashboard();return false;" />
                        </td>
                        <td style="width: 5%; text-align: right; border-right: 2px solid Transparent">
                            <input type="button" id="btnReset" value="Reset" onclick="Reset_Newdashboard();return false;" />
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
            <asp:HiddenField ID="hdnSnippetId" runat="server" Value="0" />
            <asp:HiddenField ID="hdnFromDays0" runat="server" Value="0" />
            <asp:HiddenField ID="hdnToDays7" runat="server" Value="0" />
            <asp:HiddenField ID="hdnFromDays8" runat="server" Value="0" />
            <asp:HiddenField ID="hdnToDays30" runat="server" Value="0" />
            <asp:HiddenField ID="hdnFromDays31" runat="server" Value="0" />
            <asp:HiddenField ID="hdnToDays90" runat="server" Value="0" />
            <asp:HiddenField ID="hdnDept" runat="server" Value="0" />
            <div id="page-content" style="border-left: 1px solid #cccccc; border-right: 1px solid #cccccc;
                border-bottom: 1px solid #cccccc; padding: 0px; color: #282829; overflow: hidden;
                min-height: 600px; width: 100%; background-color: #333;">
                <%-- <div >--%>
                <div id='jqxWidget' style="width: 100.9%;">
                    <div id="docking" style="display: none">
                        <div>
                            <div id="lblWebPartInspectionCompleted" style="max-height: 300px" runat="server"
                                clientidmode="Static">
                                <div id="lblWebPartInspectionCompletedTitle" class="Dashboard">
                                    Completed Inspections In Last 90 Days
                                </div>
                                <div style="max-height: 300px;">
                                    <div id="dvWebPartInspectionCompleted">
                                    </div>
                                </div>
                            </div>
                            <div id="lblWebPartMyShortcuts" style="max-height: 300px" runat="server" clientidmode="Static">
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
                            <div id="lblWebPartEvents_Done" style="max-height: 300px" runat="server" clientidmode="Static">
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
                            <div id="lblWebPartPerformanceManager" style="max-height: 300px" runat="server" clientidmode="Static">
                                <div id="lblWebPartPerformanceManagerTitle" class="Dashboard">
                                    Performance Manager
                                </div>
                                <div style="max-height: 300px">
                                    <div id="dvWebPartPerformanceManager">
                                        <table style="margin-bottom: 4px">
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
                                                                Last 90 Days</span>
                                                        </div>
                                                </td>
                                                <td>
                                                    <div style="cursor: pointer;" onclick="SetPerformanceManagerDays(180)" id="lst180">
                                                        &nbsp; <span style="text-decoration: underline; font-family: Tahoma ,Tahoma, Sans-Serif">
                                                            Last 180 Days</span></div>
                                                </td>
                                                <td>
                                                    <div style="cursor: pointer;" onclick="SetPerformanceManagerDays(365)" id="lst365">
                                                        &nbsp; <span style="text-decoration: underline; font-family: Tahoma ,Tahoma, Sans-Serif">
                                                            Running 365 Days</span>
                                                    </div>
                                                </td>
                                                <td>
                                                    <div style="cursor: pointer;" onclick="SetPerformanceManagerDays(-1)" id="lstAll">
                                                        &nbsp; <span style="text-decoration: underline; font-family: Tahoma ,Tahoma, Sans-Serif">
                                                            All Days</span>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div id="divPerManLastUpdated" style="font-family: Tahoma ,Tahoma, Sans-Serif; padding-bottom: 2px">
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div>
                            <div id="lblWebPartOverdueFileScheduleApproval" style="max-height: 300px; height: auto;">
                                <div id="lblWebPartOverdueFileScheduleApprovalTitle" class="Dashboard">
                                    Overdue File Schedules Approval
                                </div>
                                <div style="max-height: 300px;">
                                    <div id="dvWebPartOverdueFileScheduleApproval">
                                    </div>
                                </div>
                            </div>
                            <div id="lblWebPartOverdueFileScheduleReceiving" style="max-height: 300px" runat="server"
                                clientidmode="Static">
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
                        <div>
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
                                    <div id="dvDocAlerts_Container">
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
                            <div id="lblWebPartRestHourData" style="max-height: 300px">
                                <div id="lblWebPartRestHourDataTitle" class="Dashboard">
                                    Rest hour Data
                                </div>
                                <div style="max-height: 300px">
                                    <div id="dvWebPartRestHourData">
                                    </div>
                                </div>
                            </div>
                            <div id="lblWebPartCrewRefusedToSignEval" style="max-height: 300px">
                                <div id="lblWebPartCrewRefusedToSignEvalTitle" class="Dashboard">
                                    Crew refused to sign evaluation
                                </div>
                                <div style="max-height: 300px">
                                    <div id="dvWebPartCrewrefusedToSignEval" clientidmode="Static" runat="server">
                                    </div>
                                </div>
                            </div>
                            <div id="lblWebPartVettingExpInNext30Days" style="max-height: 300px">
                                <div id="lblWebPartVettingExpInNext30DaysTitle" class="Dashboard">
                                    Vetting expires in the next 30 Days
                                </div>
                                <div style="max-height: 300px">
                                    <div id="dvWebPartVettingExpInNext30Days">
                                    </div>
                                </div>
                            </div>
                            <div id="lblWebPartExpiredFailedVettingInsp" style="max-height: 300px">
                                <div id="lblWebPartExpiredFailedVettingInspTitle" class="Dashboard">
                                    Expired and Failed Vetting inspections
                                </div>
                                <div style="max-height: 300px">
                                    <div id="dvWebPartExpiredFailedVettingInsp">
                                    </div>
                                </div>
                            </div>
                            <div id="lblProcurementApprovalOverview" style="max-height: 300px">
                                <div id="lblProcurementApprovalOverviewTitle" class="Dashboard">
                                    Procurement Approval Overview
                                </div>
                                <div style="max-height: 300px">
                                    <div id="divProcurementApprovalOverview">
                                    </div>
                                </div>
                            </div>
                            <div id="lblPOPendingMyApproval" style="max-height: 300px">
                                <div id="lblPOPendingMyApprovalTitle" class="Dashboard">
                                    PO Pending My Approval
                                </div>
                                <div style="max-height: 300px">
                                    <div id="divPOPendingMyApproval" clientidmode="Static" runat="server">
                                    </div>
                                </div>
                            </div>
                            <div id="lblInvoicesPendingMyVerification" style="max-height: 300px">
                                <div id="lblInvoicesPendingMyVerificationTitle" class="Dashboard">
                                    Invoices Pending My Verification
                                </div>
                                <div style="max-height: 300px">
                                    <div id="divInvoicesPendingMyVerification" clientidmode="Static" runat="server">
                                    </div>
                                </div>
                            </div>
                            <div id="lblInvoicesPendingMyApproval" style="max-height: 300px">
                                <div id="lblInvoicesPendingMyApprovalTitle" class="Dashboard">
                                    Invoices Pending My Approval
                                </div>
                                <div style="max-height: 300px">
                                    <div id="divInvoicesPendingMyApproval" clientidmode="Static" runat="server">
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <%-- </div>--%>
                </div>
            </div>
            <div id="popupMagnifySnippet" style="width: 100%; height: 100%; top: 0px; left: 0px;
                opacity: 90; display: none; position: fixed; background-color: transparent; overflow: auto;
                z-index: 996">
                <div id="dvSnippetContent" style="display: block; width: auto; height: auto; min-width: 350px;
                    min-height: 50px; position: absolute; background-color: white; z-index: 998">
                    <table width="100%" cellpadding="0" cellspacing="0">
                        <tr class="trClass">
                            <td>
                                <div id="Maghead">
                                    <table width="100%">
                                        <tr>
                                            <td align="left" style="color: White; padding-left: 2px; font-family: Verdana; padding-bottom: 2px;">
                                                <div id="SnippetTitle">
                                                </div>
                                            </td>
                                            <td align="right" style="display: inline; float: right">
                                                <img src="../Images/Refresh_14.png" id="btnRefreshSnippet" onclick="RefreshSnippet(event);"
                                                    alt="refresh" title="Refresh" style="cursor: pointer;" />
                                                <asp:ImageButton ID="imgReportIssue" ImageUrl="../jqxWidgets/images/close_white.png"
                                                    runat="server" ToolTip="Close" OnClientClick="CloseSnippet() ;return false;">
                                                </asp:ImageButton>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="MagnifyDiv" style="overflow-y: auto;">
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
                <div style="background-color: Black; width: 100%; height: 100%; opacity: 0.5; z-index: 997;
                    overflow: auto; position: fixed;">
                </div>
            </div>
            <div id="ColorPickerContainer" style="display: none; height: auto; width: auto; position: absolute;">
                <div class="arrow-up">
                </div>
                <div style="background-color: #CCCCCC; height: auto; width: 50px; z-index: 10001;">
                    <table style="padding: 2px">
                        <tr>
                            <td>
                                <img src="../Images/rsz_edit-pen.png" id="Img2" onclick="EditTitle(event); return false;"
                                    alt="Edit Title" title="Edit Title" class="editTitle" />
                            </td>
                            <td>
                                <img src="../Images/rsz_water_drop-512.png" id="Img1" onclick="EditColor(event); return false;"
                                    alt="Change Background Colour" class="editColor" title="Change Background Colour" />
                            </td>
                        </tr>
                    </table>
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
            <div id="dvPopupCreateVetting" style="display: none; width: 600px; text-align: center;"
                title="Create New Vetting">
                <iframe id="IframeCreateVetting" src="" frameborder="0" style="height: 400px; width: 600px;">
                </iframe>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
