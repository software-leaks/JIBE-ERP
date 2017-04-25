<%@ Page Title="Worklist" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Worklist.aspx.cs" Inherits="Technical_Worklist" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <meta name="keywords" content="">
    <link href="../../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery-1.8.2.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <script src="../../Scripts/Common_Functions.js" type="text/javascript"></script>
    <style type="text/css">
        select
        {
            font-size: 12px;
            height: 21px;
        }
        .gradiant-css-blue
        {
            filter: progid:DXImageTransform.Microsoft.Gradient(GradientType=0,StartColorStr='#CEE3F6',EndColorStr='#99BBE6');
            background: -webkit-gradient(linear, left top, left bottom, from(#CEE3F6), to(#99BBE6));
            background: -moz-linear-gradient(top,  #CEE3F6,  #99BBE6);
            color: Black;
        }
        .hide
        {
            display: none;
        }
        
        .numberCircle
        {
            border-radius: 50%;
            behavior: url(PIE.htc); /* remove if you don't care about IE8 */
            width: 22px;
            height: 18px;
            padding: 6px;
            background: white;
            color: rgba(0, 160, 209, 1);
            text-align: center;
            font: 16px Arial, sans-serif;
        }
        .numberCircleh
        {
            border-radius: 50%;
            behavior: url(PIE.htc); /* remove if you don't care about IE8 */
            width: 22px;
            height: 18px;
            padding: 6px; /* background: rgba(204, 204, 204, 1);*/
            background: rgba(254, 254, 0, 1);
            color: rgba(0, 160, 209, 1);
            text-align: center;
            font: 16px Arial, sans-serif;
        }
        .numberCircle1
        {
            border-radius: 50%;
            behavior: url(PIE.htc); /* remove if you don't care about IE8 */
            width: 22px;
            height: 18px;
            padding: 6px;
            background: white;
            color: rgba(32, 80, 129, 1);
            text-align: center;
            font: 16px Arial, sans-serif;
        }
        .numberCircle1h
        {
            border-radius: 50%;
            display: table;
            behavior: url(PIE.htc); /* remove if you don't care about IE8 */
            width: 22px;
            height: 18px;
            padding: 6px;
            background: white; /* background: rgba(204, 204, 204, 1);*/
            background: rgba(254, 254, 0, 1);
            color: rgba(0, 160, 209, 1);
            text-align: center;
            font: 16px Arial, sans-serif;
        }
        .numberCircle2
        {
            display: table;
            behavior: url(PIE.htc); /* remove if you don't care about IE8 */
            width: 22px;
            height: 18px;
            padding: 6px;
            background: white;
            color: rgba(0, 160, 209, 1);
            text-align: center;
            font: 16px Arial, sans-serif;
        }
        .numberCircle2h
        {
            display: table;
            behavior: url(PIE.htc); /* remove if you don't care about IE8 */
            width: 22px;
            height: 18px;
            padding: 6px; /* background: rgba(204, 204, 204, 1);*/
            background: rgba(254, 254, 0, 1);
            color: rgba(0, 160, 209, 1);
            text-align: center;
            font: 16px Arial, sans-serif;
        }
        .spinner
        {
            width: 30px;
            height: 30px;
            background-color: White;
            border-radius: 100%;
            -webkit-animation: scaleout 1.0s infinite ease-in-out;
            animation: scaleout 1.0s infinite ease-in-out;
        }
        .CssMargin
        {
            margin-right: 9px;
        }
    </style>
    <script language='javascript' type='text/javascript'>
        function divCategorylink() {
            document.getElementById("divCategory").style.display = "block";
        }

        function OpenCategoryDiv() {
            var divCategory = document.getElementById("divCategory");
            divCategory.style.display = 'inline';
            var hdnFlagCheck = document.getElementById("ctl00_MainContent_hdnFlagCheck");
            hdnFlagCheck.value = 'true';
        }

        function CloseMe() {
            var divCategory = document.getElementById("divCategory");
            divCategory.style.display = 'none';
            var hdnFlagCheck = document.getElementById("ctl00_MainContent_hdnFlagCheck");
            hdnFlagCheck.value = 'false';
        }

        function SetAndClose() {
            var divCategory = document.getElementById("divCategory");
            divCategory.style.display = 'none';
            var hdnFlagCheck = document.getElementById("ctl00_MainContent_hdnFlagCheck");
            hdnFlagCheck.value = 'false';
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

        function VesselChange() {
            if (lastExecutor != null)
                lastExecutor.abort();

            GetWorklistCnt(0);
            GetWorklistCnt(1);
            GetWorklistCnt(2);
            GetWorklistCnt(3);
            GetWorklistCnt(4);
        }

        $(document).ready(function () {
           
        });


        var lastExecutor = null;
        function GetWorklistCnt(ResultCateg) {

            var Fleet_ID = $('#<%=ddlFleet.ClientID %>').val();
            var Vessel_ID = $('#<%=ddlVessels.ClientID %>').val();
            var JOB_STATUS = "";
            var JOB_TYPE = "";


            var rblJobStausButtons = document.getElementById('<%=rblJobStaus.ClientID%>');
            var radio = rblJobStausButtons.getElementsByTagName("input");
            for (var i = 0; i < radio.length; i++) {
                if (radio[i].checked) {
                    //your Code goes here....
                    JOB_STATUS = radio[i].value;
                    break;
                }
            }
            var rblJobTypeButtons = document.getElementById('<%=rblJobType.ClientID%>');
            var radio1 = rblJobTypeButtons.getElementsByTagName("input");
            for (var i = 0; i < radio1.length; i++) {
                if (radio1[i].checked) {
                    //your Code goes here....
                    JOB_TYPE = radio1[i].value;
                    break;
                }
            }



            var service = Sys.Net.WebServiceProxy.invoke('/' + __app_name + '/JibeWebService.asmx', 'asyncLoadWoklistCounts', false, { "ResultCateg": ResultCateg }, OnSuccGet_Record_Information, Onfail, new Array(ResultCateg, ''));
            lastExecutor = service.get_executor();
        }
        function OnSuccGet_Record_Information(retval, prm) 
        {
            try {

                if (prm[0] == "0") {

                    var divl24 = document.getElementById('<%=divl24.ClientID%>');
                    $('#<%=divl24.ClientID%>').addClass("numberCircle");

                    if (curbtn == prm[0]) {
                        $('#<%=divl24.ClientID%>').addClass("numberCircle2h");
                    }
                    else {
                        $('#<%=divl24.ClientID%>').addClass("numberCircle2");
                    }
                    document.getElementById("hf2").value = 0;
                    divl24.innerHTML = retval[0];
                }
                if (prm[0] == "1") {
                    var followCnt = document.getElementById('<%=followCnt.ClientID%>');
                    $('#<%=followCnt.ClientID%>').addClass("numberCircle");

                    if (curbtn == prm[0]) {
                        $('#<%=followCnt.ClientID%>').addClass("numberCircle2h");
                    }
                    else {
                        $('#<%=followCnt.ClientID%>').addClass("numberCircle2");
                    }
                    document.getElementById("hf2").value = 1;
                    followCnt.innerHTML = retval[0];
                }
                if (prm[0] == "2") {
                    var ncr7 = document.getElementById('<%=ncr7.ClientID%>');
                    $('#<%=ncr7.ClientID%>').addClass("numberCircle");

                    if (curbtn == prm[0]) {
                        $('#<%=ncr7.ClientID%>').addClass("numberCircle2h");
                    }
                    else {
                        $('#<%=ncr7.ClientID%>').addClass("numberCircle2");
                    }
                    document.getElementById("hf2").value = 2;

                    ncr7.innerHTML = retval[0];
                }
                if (prm[0] == "3") {
                    var jComp7 = document.getElementById('<%=jComp7.ClientID%>');
                    $('#<%=jComp7.ClientID%>').addClass("numberCircle1");

                    if (curbtn == prm[0]) {
                        $('#<%=jComp7.ClientID%>').addClass("numberCircle2h");
                    }
                    else {
                        $('#<%=jComp7.ClientID%>').addClass("numberCircle2");
                    }
                    document.getElementById("hf2").value = 3;
                    jComp7.innerHTML = retval[0];
                }
                if (prm[0] == "4") 
                {
                    var jCnv = document.getElementById('<%=jCnv.ClientID%>');
                    $('#<%=jCnv.ClientID%>').addClass("numberCircle");

                    if (curbtn == prm[0]) {
                        $('#<%=jCnv.ClientID%>').addClass("numberCircle2h");
                    }
                    else {
                        $('#<%=jCnv.ClientID%>').addClass("numberCircle2");
                    }

                    document.getElementById("hf2").value = 4;
                    jCnv.innerHTML = retval[0];
                }
                // To maintain space between circle and text for chrome browser. Added on DT:21-07-2016
                if (navigator.appVersion.indexOf("Chrome/") != -1) {
                    var divl24 = document.getElementById('<%=divl24.ClientID%>');
                    $('#<%=divl24.ClientID%>').addClass("CssMargin");

                    var followCnt = document.getElementById('<%=followCnt.ClientID%>');
                    $('#<%=followCnt.ClientID%>').addClass("CssMargin");

                    var ncr7 = document.getElementById('<%=ncr7.ClientID%>');
                    $('#<%=ncr7.ClientID%>').addClass("CssMargin");

                    var jComp7 = document.getElementById('<%=jComp7.ClientID%>');
                    $('#<%=jComp7.ClientID%>').addClass("CssMargin");

                    var jCnv = document.getElementById('<%=jCnv.ClientID%>');
                    $('#<%=jCnv.ClientID%>').addClass("CssMargin");
                }

            }
            catch (ex)
    { }
        }
        function Onfail(retval) {
            // alert(retval);
        }

        var curbtn = "";
        function showDialog(url) {
            window.open(url);
        }
        function showEmailNotifiers() {
            document.getElementById("dvEmailNotifiers").style.display = 'block';
        }
        function hideEmailNotifiers() {
            document.getElementById("dvEmailNotifiers").style.display = 'none';
        }
        function showFollowups(V, W, O) {
            var src = window.event.srcElement;
            var pos = $(src).offset();
            var width = $(src).width();

            var url = 'Task_Followups.aspx?WLID=' + W + '&VID=' + V + '&OFFID=' + O;

            $('#iframeFollowups').load(function () { this.style.height = (this.contentWindow.document.body.offsetHeight + 20) + 'px'; });
            $('#iframeFollowups').attr("src", url);
            $('#dialog').show();
            $("#dialog").css({ "left": (pos.left - 600) + "px", "top": pos.top + "px", "width": 600 });


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
        function DivClicked(tp) {
            
            if (tp == "-1") {
                curbtn = "-1";
            }
            if (tp == "0") {
                var btnHidden = $('#<%= btnHidden.ClientID %>');
                if (btnHidden != null) {
                    btnHidden.click();
                    curbtn = "0";

                }
            }
            if (tp == "1") {
                var btnHidden1 = $('#<%= btnHidden1.ClientID %>');
                if (btnHidden1 != null) {
                    btnHidden1.click();
                    curbtn = "1";
                }
            }
            if (tp == "2") {
                var btnHidden2 = $('#<%= btnHidden2.ClientID %>');
                if (btnHidden2 != null) {
                    btnHidden2.click();
                    curbtn = "2";
                }
            }
            if (tp == "3") {
                var btnHidden3 = $('#<%= btnHidden3.ClientID %>');
                if (btnHidden3 != null) {
                    btnHidden3.click();
                    curbtn = "3";
                }
            }
            if (tp == "4") {
                var btnHidden4 = $('#<%= btnHidden4.ClientID %>');
                if (btnHidden4 != null) {
                    btnHidden4.click();
                    curbtn = "4";
                }
            }

        }
      
        // Empty function is used for vetting.Do not delete or edit.
        function saveCloseChild() {

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

        $(document).ready(function () {
            restConditionCheckbox();

            //            $('#ctl00_MainContent_rblJobStaus_0').click(function () {
            //                $('input:checkbox[id^="ctl00_MainContent_rblJobStaus_"]').not(this).prop('checked', this.checked);



            //            });
            //            $("#ctl00_MainContent_rblJobStaus_1").change(function () {
            //                if ($('#ctl00_MainContent_rblJobStaus_0').is(":checked")) {
            //                    alert('x');
            //                    document.getElementById("ctl00_MainContent_rblJobStaus_0").checked = false;
            //                }
            //                else {
            //                    alert('y');
            //                    document.getElementById("ctl00_MainContent_rblJobStaus_0").checked = true;
            //                } 
            //            });

            //            $("[id*=ctl00_MainContent_rblJobStaus_]").change(function () {
            //                alert('x');
            //                if ($('input[id*=ctl00_MainContent_rblJobStaus_][type=checkbox[id^="ctl00_MainContent_rblJobStaus_"]]:checked').length == $('input[id*=ctl00_MainContent_rblJobStaus_][type=checkbox[id^="ctl00_MainContent_rblJobStaus_"]]').length) {
            //                    $('#ctl00_MainContent_rblJobStaus_0').prop('checked', true);
            //                } else {
            //                    $('#ctl00_MainContent_rblJobStaus_0').prop('checked', false);
            //                }


            //            });




            //            $(document).on("mousemove", function (event) {

            //                $("#dx").text(event.pageX);
            //                $("#dy").text(event.pageY);


            //     });

        });

        function restConditionCheckbox() {
            $('#ctl00_MainContent_rblJobStaus_0').click(function () {
                $('input:checkbox[id^="ctl00_MainContent_rblJobStaus_"]').not(this).prop('checked', this.checked);
            });

            $("[id=ctl00_MainContent_rblJobStaus_1]").change(function () {

                if (document.getElementById("ctl00_MainContent_rblJobStaus_1").checked == false) {

                    $('[id="ctl00_MainContent_rblJobStaus_0"]').prop('checked', false);
                }
                if (document.getElementById("ctl00_MainContent_rblJobStaus_2").checked == false) {

                    $('[id="ctl00_MainContent_rblJobStaus_0"]').prop('checked', false);
                }
                if (document.getElementById("ctl00_MainContent_rblJobStaus_3").checked == false) {

                    $('[id="ctl00_MainContent_rblJobStaus_0"]').prop('checked', false);
                }
                if (document.getElementById("ctl00_MainContent_rblJobStaus_4").checked == false) {

                    $('[id="ctl00_MainContent_rblJobStaus_0"]').prop('checked', false);
                }
                if (document.getElementById("ctl00_MainContent_rblJobStaus_5").checked == false) {

                    $('[id="ctl00_MainContent_rblJobStaus_0"]').prop('checked', false);
                }

                var lenofa = 0;

                if (document.getElementById("ctl00_MainContent_rblJobStaus_1").checked == true)
                { lenofa = lenofa + 1 }
                if (document.getElementById("ctl00_MainContent_rblJobStaus_2").checked == true)
                { lenofa = lenofa + 1 }
                if (document.getElementById("ctl00_MainContent_rblJobStaus_3").checked == true)
                { lenofa = lenofa + 1 }
                if (document.getElementById("ctl00_MainContent_rblJobStaus_4").checked == true)
                { lenofa = lenofa + 1 }
                if (document.getElementById("ctl00_MainContent_rblJobStaus_5").checked == true)
                { lenofa = lenofa + 1 }

                if (lenofa == 5) {
                    $('[id="ctl00_MainContent_rblJobStaus_0"]').prop('checked', true);

                }

            });
            $("[id=ctl00_MainContent_rblJobStaus_2]").change(function () {

                if (document.getElementById("ctl00_MainContent_rblJobStaus_1").checked == false) {

                    $('[id="ctl00_MainContent_rblJobStaus_0"]').prop('checked', false);
                }
                if (document.getElementById("ctl00_MainContent_rblJobStaus_2").checked == false) {

                    $('[id="ctl00_MainContent_rblJobStaus_0"]').prop('checked', false);
                }
                if (document.getElementById("ctl00_MainContent_rblJobStaus_3").checked == false) {

                    $('[id="ctl00_MainContent_rblJobStaus_0"]').prop('checked', false);
                }
                if (document.getElementById("ctl00_MainContent_rblJobStaus_4").checked == false) {

                    $('[id="ctl00_MainContent_rblJobStaus_0"]').prop('checked', false);
                }
                if (document.getElementById("ctl00_MainContent_rblJobStaus_5").checked == false) {

                    $('[id="ctl00_MainContent_rblJobStaus_0"]').prop('checked', false);
                }

                var lenofa = 0;

                if (document.getElementById("ctl00_MainContent_rblJobStaus_1").checked == true)
                { lenofa = lenofa + 1 }
                if (document.getElementById("ctl00_MainContent_rblJobStaus_2").checked == true)
                { lenofa = lenofa + 1 }
                if (document.getElementById("ctl00_MainContent_rblJobStaus_3").checked == true)
                { lenofa = lenofa + 1 }
                if (document.getElementById("ctl00_MainContent_rblJobStaus_4").checked == true)
                { lenofa = lenofa + 1 }
                if (document.getElementById("ctl00_MainContent_rblJobStaus_5").checked == true)
                { lenofa = lenofa + 1 }

                if (lenofa == 5) {
                    $('[id="ctl00_MainContent_rblJobStaus_0"]').prop('checked', true);

                }

            });
            $("[id=ctl00_MainContent_rblJobStaus_3]").change(function () {

                if (document.getElementById("ctl00_MainContent_rblJobStaus_1").checked == false) {

                    $('[id="ctl00_MainContent_rblJobStaus_0"]').prop('checked', false);
                }
                if (document.getElementById("ctl00_MainContent_rblJobStaus_2").checked == false) {

                    $('[id="ctl00_MainContent_rblJobStaus_0"]').prop('checked', false);
                }
                if (document.getElementById("ctl00_MainContent_rblJobStaus_3").checked == false) {

                    $('[id="ctl00_MainContent_rblJobStaus_0"]').prop('checked', false);
                }
                if (document.getElementById("ctl00_MainContent_rblJobStaus_4").checked == false) {

                    $('[id="ctl00_MainContent_rblJobStaus_0"]').prop('checked', false);
                }
                if (document.getElementById("ctl00_MainContent_rblJobStaus_5").checked == false) {

                    $('[id="ctl00_MainContent_rblJobStaus_0"]').prop('checked', false);
                }

                var lenofa = 0;

                if (document.getElementById("ctl00_MainContent_rblJobStaus_1").checked == true)
                { lenofa = lenofa + 1 }
                if (document.getElementById("ctl00_MainContent_rblJobStaus_2").checked == true)
                { lenofa = lenofa + 1 }
                if (document.getElementById("ctl00_MainContent_rblJobStaus_3").checked == true)
                { lenofa = lenofa + 1 }
                if (document.getElementById("ctl00_MainContent_rblJobStaus_4").checked == true)
                { lenofa = lenofa + 1 }
                if (document.getElementById("ctl00_MainContent_rblJobStaus_5").checked == true)
                { lenofa = lenofa + 1 }

                if (lenofa == 5) {
                    $('[id="ctl00_MainContent_rblJobStaus_0"]').prop('checked', true);

                }

            });
            $("[id=ctl00_MainContent_rblJobStaus_4]").change(function () {

                if (document.getElementById("ctl00_MainContent_rblJobStaus_1").checked == false) {

                    $('[id="ctl00_MainContent_rblJobStaus_0"]').prop('checked', false);
                }
                if (document.getElementById("ctl00_MainContent_rblJobStaus_2").checked == false) {

                    $('[id="ctl00_MainContent_rblJobStaus_0"]').prop('checked', false);
                }
                if (document.getElementById("ctl00_MainContent_rblJobStaus_3").checked == false) {

                    $('[id="ctl00_MainContent_rblJobStaus_0"]').prop('checked', false);
                }
                if (document.getElementById("ctl00_MainContent_rblJobStaus_4").checked == false) {

                    $('[id="ctl00_MainContent_rblJobStaus_0"]').prop('checked', false);
                }
                if (document.getElementById("ctl00_MainContent_rblJobStaus_5").checked == false) {

                    $('[id="ctl00_MainContent_rblJobStaus_0"]').prop('checked', false);
                }

                var lenofa = 0;

                if (document.getElementById("ctl00_MainContent_rblJobStaus_1").checked == true)
                { lenofa = lenofa + 1 }
                if (document.getElementById("ctl00_MainContent_rblJobStaus_2").checked == true)
                { lenofa = lenofa + 1 }
                if (document.getElementById("ctl00_MainContent_rblJobStaus_3").checked == true)
                { lenofa = lenofa + 1 }
                if (document.getElementById("ctl00_MainContent_rblJobStaus_4").checked == true)
                { lenofa = lenofa + 1 }
                if (document.getElementById("ctl00_MainContent_rblJobStaus_5").checked == true)
                { lenofa = lenofa + 1 }

                if (lenofa == 5) {
                    $('[id="ctl00_MainContent_rblJobStaus_0"]').prop('checked', true);

                }

            });
            $("[id=ctl00_MainContent_rblJobStaus_5]").change(function () {

                if (document.getElementById("ctl00_MainContent_rblJobStaus_1").checked == false) {

                    $('[id="ctl00_MainContent_rblJobStaus_0"]').prop('checked', false);
                }
                if (document.getElementById("ctl00_MainContent_rblJobStaus_2").checked == false) {

                    $('[id="ctl00_MainContent_rblJobStaus_0"]').prop('checked', false);
                }
                if (document.getElementById("ctl00_MainContent_rblJobStaus_3").checked == false) {

                    $('[id="ctl00_MainContent_rblJobStaus_0"]').prop('checked', false);
                }
                if (document.getElementById("ctl00_MainContent_rblJobStaus_4").checked == false) {

                    $('[id="ctl00_MainContent_rblJobStaus_0"]').prop('checked', false);
                }
                if (document.getElementById("ctl00_MainContent_rblJobStaus_5").checked == false) {

                    $('[id="ctl00_MainContent_rblJobStaus_0"]').prop('checked', false);
                }

                var lenofa = 0;

                if (document.getElementById("ctl00_MainContent_rblJobStaus_1").checked == true)
                { lenofa = lenofa + 1 }
                if (document.getElementById("ctl00_MainContent_rblJobStaus_2").checked == true)
                { lenofa = lenofa + 1 }
                if (document.getElementById("ctl00_MainContent_rblJobStaus_3").checked == true)
                { lenofa = lenofa + 1 }
                if (document.getElementById("ctl00_MainContent_rblJobStaus_4").checked == true)
                { lenofa = lenofa + 1 }
                if (document.getElementById("ctl00_MainContent_rblJobStaus_5").checked == true)
                { lenofa = lenofa + 1 }

                if (lenofa == 5) {
                    $('[id="ctl00_MainContent_rblJobStaus_0"]').prop('checked', true);

                }

            });
            $(document).on("mousemove", function (event) {

                $("#dx").text(event.pageX);
                $("#dy").text(event.pageY);


            });
        }

        function showFollowups(V, W, O) {




            var x = 0;
            var y = 0;
            var min = 0;

            x = $("#dx").text();
            y = $("#dy").text();








            var url = 'Task_Followups.aspx?WLID=' + W + '&VID=' + V + '&OFFID=' + O;

            $('#iframeFollowups').load(function () { this.style.height = (this.contentWindow.document.body.offsetHeight + 20) + 'px'; });
            $('#iframeFollowups').attr("src", url);
            $('#dialog').show();

            $("#dialog").css({ "left": (x - 600) + "px", "top": (y) + "px", "width": 600 });


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
        function chekAfilter() {
            alert($("#<%= hfAdv.ClientID %>").val());
        }
        function SetCurButton() {

            return;
            var btnindex = document.getElementById("hf2").value;
            if (btnindex == "0") {

                var divl24 = document.getElementById('<%=divl24.ClientID%>');

                $('#<%=divl24.ClientID%>').addClass("numberCircleh");

            }
            if (btnindex == "1") {
                var followCnt = document.getElementById('<%=followCnt.ClientID%>');



                $('#<%=followCnt.ClientID%>').addClass("numberCircle1h");

                followCnt.innerHTML = retval[0];
            }
            if (btnindex == "2") {
                var ncr7 = document.getElementById('<%=ncr7.ClientID%>');



                $('#<%=ncr7.ClientID%>').addClass("numberCircleh");

            }
            if (btnindex == "3") {
                var jComp7 = document.getElementById('<%=jComp7.ClientID%>');



                $('#<%=jComp7.ClientID%>').addClass("numberCircle1h");


                jComp7.innerHTML = retval[0];
            }
            if (btnindex == "4") {
                var jCnv = document.getElementById('<%=jCnv.ClientID%>');

                $('#<%=jCnv.ClientID%>').addClass("numberCircle2h");

            }

        }
     


    </script>
    <script type="text/javascript">
        var lastExecutorDetails = null;
        if (lastExecutorDetails != null)
            lastExecutorDetails.abort();
        function abcxx(Worklist_ID, Office_ID, Vessel_ID, data1, data2) {
            // alert(Worklist_ID);
            //js_ShowToolTip(Worklist_ID, data1, data2);
            var service = Sys.Net.WebServiceProxy.invoke('/' + __app_name + '/JibeWebService.asmx', 'asyncGet_Function_Information', false, { "Worklist_ID": Worklist_ID, "WL_Office_ID": Office_ID, "Vessel_ID": Vessel_ID }, OnSuccGet_Get_Function_Information, Onfail, new Array(data1, data2));
            lastExecutorDetails = service.get_executor();
        }


        function OnSuccGet_Get_Function_Information(retval, prm) {
            try {



                js_ShowToolTip(retval, prm[0], prm[1]);
            }
            catch (ex)
    { }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:UpdateProgress ID="upUpdateProgress" runat="server">
                <ProgressTemplate>
                    <div id="divProgress" style="background-color: transparent; position: absolute; left: 49%;
                        top: 20px; z-index: 2; color: black">
                        <img src="../../images/loaderbar.gif" alt="Please Wait" />
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
            <div class="page-title">
                Worklist
            </div>
            <div id="dx" style="display: none">
            </div>
            <div id="dy" style="display: none">
            </div>
            <div id="dvPageContent" style="margin-top: -2px; border: 1px solid #cccccc; vertical-align: bottom;
                padding: 4px; color: Black; text-align: left; background-color: #fff;">
                <div id="dvDefaultFilter">
                    <asp:HiddenField runat="server" ID="hf1" Value="" />
                    <asp:HiddenField runat="server" ID="hf2" Value="5" ClientIDMode="Static" />
                    <table style="text-align: right; height: 47px; padding-top: 3px; padding-bottom: 3px;
                        font-size: 11px">
                        <tr>
                            <td>
                                <div style="width: 170px; background-color: rgba(0, 160, 209, 1); color: White; height: 40px;
                                    cursor: pointer; padding: 2px; font-weight: bold; text-align: left" onclick="javascript:DivClicked(0); return true;">
                                    <table>
                                        <tr>
                                            <td>
                                                <div id="divl24" runat="server">
                                                </div>
                                            </td>
                                            <td>
                                                New jobs created in last 24 Hrs
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                            <td>
                                <div style="width: 220px; background-color: rgba(32, 80, 129, 1); color: White; height: 40px;
                                    cursor: pointer; padding: 2px; font-weight: bold; text-align: left" onclick="javascript:DivClicked(1); return true;">
                                    <table>
                                        <tr>
                                            <td>
                                                <div id="followCnt" runat="server">
                                                </div>
                                            </td>
                                            <td>
                                                Followup/Response received from vessel in Last 24 Hrs
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                            <td>
                                <div style="width: 170px; background-color: rgba(0, 160, 209, 1); color: White; height: 40px;
                                    cursor: pointer; padding: 2px; font-weight: bold; text-align: left" onclick="javascript:DivClicked(2); return true;">
                                    <table>
                                        <tr>
                                            <td>
                                                <div id="ncr7" runat="server">
                                                </div>
                                            </td>
                                            <td>
                                                NCR reported in<br />
                                                last 7 days
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                            <td>
                                <div style="width: 190px; background-color: rgba(32, 80, 129, 1); color: White; height: 40px;
                                    cursor: pointer; padding: 2px; font-weight: bold; text-align: left" onclick="javascript:DivClicked(3); return true;">
                                    <table>
                                        <tr>
                                            <td>
                                                <div id="jComp7" runat="server">
                                                </div>
                                            </td>
                                            <td>
                                                Jobs completed in<br />
                                                last 7 days
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                            <td>
                                <div style="width: 170px; background-color: rgba(0, 160, 209, 1); color: White; height: 40px;
                                    cursor: pointer; padding: 2px; font-weight: bold; text-align: left" onclick="javascript:DivClicked(4); return true;">
                                    <table>
                                        <tr>
                                            <td>
                                                <div id="jCnv" runat="server">
                                                </div>
                                            </td>
                                            <td>
                                                Jobs completed
                                                <br />
                                                but not verified.
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                    </table>
                    <table border="0" cellpadding="1" cellspacing="1" style="width: 100%;">
                        <tr>
                            <td valign="top" style="border: 1px solid #aabbdd;">
                                <table border="0" cellpadding="4" cellspacing="1">
                                    <tr>
                                        <td style="text-align: right;">
                                            <asp:Label ID="lblFleet" Text="Fleet" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlFleet" runat="server" Width="135px" OnSelectedIndexChanged="ddlFleet_SelectedIndexChanged"
                                                AutoPostBack="true">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="ddlVessel_Manager" runat="server" Width="135px" OnSelectedIndexChanged="ddlFleet_SelectedIndexChanged"
                                                Visible="false" AutoPostBack="true">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right;">
                                            Vessels:
                                        </td>
                                        <td style="text-align: left;">
                                            <asp:DropDownList ID="ddlVessels" runat="server" Width="135px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td valign="top" style="border: 1px solid #aabbdd;">
                                <table border="0" cellpadding="4" cellspacing="1">
                                    <tr>
                                        <td style="text-align: right;">
                                            Status:
                                        </td>
                                        <td>
                                            <asp:CheckBoxList ID="rblJobStaus" runat="server" RepeatDirection="Horizontal" TextAlign="Right"
                                                CellPadding="1" CellSpacing="0">
                                                <asp:ListItem Value="0" Text="All"></asp:ListItem>
                                                <asp:ListItem Value="PENDING" Text="Pending" Selected="True"></asp:ListItem>
                                                <asp:ListItem Value="COMPLETED" Text="Completed"></asp:ListItem>
                                                <asp:ListItem Value="REWORKED" Text="Re-worked"></asp:ListItem>
                                                <asp:ListItem Value="CLOSED" Text="Verified"></asp:ListItem>
                                                <asp:ListItem Value="OVERDUEPENDING" Text="Overdue"></asp:ListItem>
                                            </asp:CheckBoxList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right;">
                                            Type:
                                        </td>
                                        <td valign="top" style="text-align: left;">
                                            <asp:RadioButtonList ID="rblJobType" runat="server" RepeatDirection="Horizontal"
                                                TextAlign="Right" CellPadding="1" CellSpacing="0">
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td valign="top" style="border: 1px solid #aabbdd;">
                                <table border="0" cellpadding="4" cellspacing="1">
                                    <tr>
                                        <td style="text-align: right;">
                                            Inspector:
                                        </td>
                                        <td style="text-align: left;">
                                            <asp:DropDownList ID="ddlInspector" runat="server" Width="135px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right;">
                                            Job Code/Desc:
                                        </td>
                                        <td style="text-align: left;">
                                            <asp:TextBox ID="txtDescription" runat="server" Width="129px"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td valign="top" style="border: 1px solid #aabbdd;">
                                <table>
                                    <tr>
                                        <td style="font-weight: bold">
                                            <asp:Label ID="lblReportcaption" runat="server" Text="Reports:" Visible="false"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlReports" runat="server" Visible="false">
                                                <asp:ListItem Value="0" Text="Job List"></asp:ListItem>
                                                <asp:ListItem Value="1" Text="Jobs Progress"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="ImgBtnReport" ImageUrl="~/Images/reportview.gif" runat="server"
                                                Visible="false" OnClick="ImgBtnReport_Click" />
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                </table>
                                <table border="0" cellpadding="1" cellspacing="1" width="100%" style="margin-top: 10px">
                                    <tr>
                                        <td style="width: 150px; text-align: center;">
                                            <asp:ImageButton ID="ImgBtnSearch" ImageUrl="~/Images/SearchAndReload.png" runat="server"
                                                OnClick="ImgBtnSearch_Click" />
                                            <asp:ImageButton ID="ImgBtnAddNewJob" ImageUrl="~/Images/AddNewJob.png" runat="server"
                                                OnClientClick="javascript:window.open('addnewjob.aspx?OFFID=0&WLID=0&VID=0');return false;" />
                                            <asp:ImageButton ID="ImgBtnClearFilter" ImageUrl="~/Images/ClearFilter.png" runat="server"
                                                OnClick="ImgBtnClearFilter_Click" />
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="ImgExportToExcel" src="../../Images/DocTree/xls.jpg" Height="20px"
                                                Visible="true" runat="server" AlternateText="Export" OnClick="ImgExportToExcel_Click"
                                                Style="margin-left: 10px" ToolTip="Export With Images" />
                                        </td>
                                        <td align="center">
                                            <asp:ImageButton ID="imgExpWithoutImg" runat="server" AlternateText="Export" Height="20px"
                                                ToolTip="Export Without Images" src="../../Images/DocTree/xlsx.png" OnClick="imgExpWithoutImg_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
                <asp:HiddenField ID="hfAdv" runat="server" Value="c" />
                <asp:Button runat="server" ID="btnHidden" Style="display: none" OnClick="btnHidden_OnClick"
                    Text="0" />
                <asp:Button runat="server" ID="btnHidden1" Style="display: none" OnClick="btnHidden1_OnClick"
                    Text="0" />
                <asp:Button runat="server" ID="btnHidden2" Style="display: none" OnClick="btnHidden2_OnClick"
                    Text="0" />
                <asp:Button runat="server" ID="btnHidden3" Style="display: none" OnClick="btnHidden3_OnClick"
                    Text="0" />
                <asp:Button runat="server" ID="btnHidden4" Style="display: none" OnClick="btnHidden4_OnClick"
                    Text="0" />
                <%--<div style="text-align: right; height: 20px;">--%>
                <div style="height: 20px;">
                    <table runat="server" id="tblInspectionDetail" style="width: 100%;">
                        <tr>
                            <td style="padding-left: 0px; width: 10%;">
                                <asp:Label ID="lblInspectorName" runat="server" Text="Inspector Name : " Visible="false"></asp:Label>
                            </td>
                            <td style="padding-left: 0px; width: 20%;">
                                <asp:Label ID="lblInspectorNameValue" runat="server" Visible="false"></asp:Label>
                            </td>
                            <td style="padding-left: 0px; width: 10%;">
                                <asp:Label ID="lblInspectionDate" runat="server" Text="Inspection Date : " Visible="false"></asp:Label>
                            </td>
                            <td style="padding-left: 0px; width: 20%;">
                                <asp:Label ID="lblInspectionDateValue" runat="server" Visible="false"></asp:Label>
                            </td>
                            <td style="text-align: right">
                                <a id="advText" href="#" onclick="toggleAdvSearch(this)">Open Advance Filter</a>
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="dvAdvanceFilter" style="background-color: #efefef;" class="hide">
                    <table border="0" cellpadding="1" cellspacing="1" style="width: 100%;">
                        <tr>
                            <td valign="top" style="border: 1px solid #aabbdd;">
                                <table border="0" cellpadding="2" cellspacing="1">
                                    <tr>
                                        <td style="text-align: right; width: 100px;">
                                            Department On Ship:
                                        </td>
                                        <td style="text-align: left;">
                                            <asp:DropDownList ID="ddldeptShip" runat="server" Width="135px" AutoPostBack="True"
                                                OnSelectedIndexChanged="Filter_Changed">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right;">
                                            Department in Office:
                                        </td>
                                        <td style="text-align: left;">
                                            <asp:DropDownList ID="ddlOffice" runat="server" Width="135px" AutoPostBack="True"
                                                OnSelectedIndexChanged="Filter_Changed">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right;">
                                            Priority:
                                        </td>
                                        <td style="text-align: left;">
                                            <asp:DropDownList ID="ddlPriority" runat="server" Width="135px" AutoPostBack="True"
                                                OnSelectedIndexChanged="Filter_Changed">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right;">
                                            Assignor:
                                        </td>
                                        <td style="text-align: left;">
                                            <asp:DropDownList ID="ddlAssignor" runat="server" Width="135px" AutoPostBack="True"
                                                OnSelectedIndexChanged="Filter_Changed">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td valign="top" style="border: 1px solid #aabbdd;">
                                <table border="0" cellpadding="2" cellspacing="1" width="100%">
                                    <tr style="background-color: #aabbdd">
                                        <td style="text-align: left;">
                                            <asp:Label ID="abc" runat="server" Text="Categories" Font-Bold="true"></asp:Label>
                                        </td>
                                        <td style="text-align: right;">
                                            <a href="JavaScript:OpenCategoryDiv()">Search<img src="../../Images/search.gif" style="border: 0px"
                                                alt="Search Categories" /></a>&nbsp;&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right;">
                                            Nature:
                                        </td>
                                        <td style="text-align: left;">
                                            <asp:DropDownList ID="ddlNature" runat="server" Width="129px" AutoPostBack="True"
                                                OnSelectedIndexChanged="ddlNature_SelectedIndexChanged">
                                                <asp:ListItem Value="0" Text="-SELECT ALL-"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right;">
                                            Primary:
                                        </td>
                                        <td style="text-align: left;">
                                            <asp:DropDownList ID="ddlPrimary" runat="server" Width="129px" AutoPostBack="True"
                                                OnSelectedIndexChanged="ddlPrimary_SelectedIndexChanged">
                                                <asp:ListItem Value="0" Text="-SELECT ALL-"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right;">
                                            Secondary:
                                        </td>
                                        <td style="text-align: left;">
                                            <asp:DropDownList ID="ddlSecondary" runat="server" Width="129px" AutoPostBack="True"
                                                OnSelectedIndexChanged="ddlSecondary_SelectedIndexChanged">
                                                <asp:ListItem Value="0" Text="-SELECT ALL-"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr style="display: none">
                                        <td style="text-align: right;">
                                            Minor:
                                        </td>
                                        <td style="text-align: left;">
                                            <asp:DropDownList ID="ddlMinor" runat="server" Width="129px" AutoPostBack="True">
                                                <asp:ListItem Value="0" Text="-SELECT ALL-"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td valign="top" style="border: 1px solid #aabbdd;">
                                <table border="0" cellpadding="2" cellspacing="1" width="100%">
                                    <tr>
                                        <td>
                                            PIC
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlPIC" runat="server" Width="129px" AutoPostBack="True" OnSelectedIndexChanged="Filter_Changed">
                                                <asp:ListItem Value="0" Text="-SELECT ALL-"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Modified in last
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtModifiedInDays" runat="server" Width="50px" OnTextChanged="Filter_Changed"
                                                AutoPostBack="true"></asp:TextBox>
                                            Days
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td valign="top" style="border: 1px solid #aabbdd; width: 150px">
                                <table border="0" cellpadding="1" cellspacing="2" width="100%">
                                    <tr style="background-color: #aabbdd">
                                        <td colspan="2" style="text-align: left;">
                                            <asp:Label ID="Label2" runat="server" Text="Date Raised" Font-Bold="true"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right;">
                                            From:
                                        </td>
                                        <td style="text-align: left;">
                                            <asp:TextBox ID="txtFromDate" CssClass="" runat="server" Width="80px" OnTextChanged="Filter_Changed"
                                                AutoPostBack="true"></asp:TextBox>
                                            <cc1:CalendarExtender ID="calFromDate" runat="server" Enabled="True" TargetControlID="txtFromDate"
                                                Format="dd/MM/yyyy">
                                            </cc1:CalendarExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right;">
                                            To:
                                        </td>
                                        <td style="text-align: left;">
                                            <asp:TextBox ID="txtToDate" CssClass="" runat="server" Width="80px" OnTextChanged="Filter_Changed"
                                                AutoPostBack="true"></asp:TextBox>
                                            <cc1:CalendarExtender ID="calToDate" runat="server" Enabled="True" TargetControlID="txtToDate"
                                                Format="dd/MM/yyyy">
                                            </cc1:CalendarExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" style="height: 5px">
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td valign="top" style="border: 1px solid #aabbdd;">
                                <table border="0" cellpadding="1" cellspacing="2" width="100%">
                                    <tr style="background-color: #aabbdd">
                                        <td colspan="2" style="text-align: left;">
                                            <asp:Label ID="Label3" runat="server" Text="Expected Date of Compln" Font-Bold="true"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right;">
                                            From:
                                        </td>
                                        <td style="text-align: left;">
                                            <asp:TextBox ID="txtExpectedCompFrom" CssClass="" runat="server" Width="80px" OnTextChanged="Filter_Changed"
                                                AutoPostBack="true"></asp:TextBox>
                                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" TargetControlID="txtExpectedCompFrom"
                                                Format="dd/MM/yyyy">
                                            </cc1:CalendarExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right;">
                                            To:
                                        </td>
                                        <td style="text-align: left;">
                                            <asp:TextBox ID="txtExpectedCompTo" CssClass="" runat="server" Width="80px" OnTextChanged="Filter_Changed"
                                                AutoPostBack="true"></asp:TextBox>
                                            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" TargetControlID="txtExpectedCompTo"
                                                Format="dd/MM/yyyy">
                                            </cc1:CalendarExtender>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td valign="top" style="border: 1px solid #aabbdd;">
                                <table border="0" cellpadding="0" cellspacing="0" width="200px" style="text-align: left;">
                                    <tr>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:CheckBox ID="chkDrydock" runat="server" Text="Jobs that deffered to Drydock"
                                                AutoPostBack="True" OnCheckedChanged="Filter_Changed" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:CheckBox ID="chkSentToShip" runat="server" Text="Jobs Sent to ship from office"
                                                AutoPostBack="True" OnCheckedChanged="Filter_Changed" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:CheckBox ID="chkRequisition" runat="server" Text="Having Requisition No." AutoPostBack="True"
                                                OnCheckedChanged="Filter_Changed" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:CheckBox ID="chkFlaggedJobs" runat="server" Text="Show jobs flagged for Meeting"
                                                AutoPostBack="True" OnCheckedChanged="Filter_Changed" /><asp:Image ID="Image1" runat="server"
                                                    ImageUrl="~/Images/Flag_ON.png" ImageAlign="Middle" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding-top: 12px">
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="dvDataGrid">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td style="width: 100%">
                                <div style="width: 100%; overflow: auto; text-align: left;">
                                    <div id="Div1" style="border: 1px solid #aabbdd; background-color: #efefef; padding: 8px;
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
                                                        &nbsp;&nbsp;Critical
                                                    </td>
                                                    <td>
                                                        <div style="height: 6px; width: 6px; background-color: Blue;" title="Priority: URGENT">
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
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                    <asp:GridView ID="grdJoblist" runat="server" AutoGenerateColumns="false" CellPadding="4"
                                        EnableModelValidation="True" AllowSorting="true" Width="100%" GridLines="None"
                                        OnRowCommand="grdJoblist_RowCommand" OnRowDataBound="grdJoblist_RowDataBound"
                                        OnSorting="grdJoblist_Sorting" AllowPaging="false" DataKeyNames="ToolTipDisplay">
                                        <Columns>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-Width="10px">
                                                <ItemTemplate>
                                                    <asp:Image ID="imgAnyChanges" runat="server" Height="16px" Width="16px" ImageUrl="~/Images/exclamation.gif"
                                                        Visible='<%#Eval("MODIFIED").ToString()=="1"?true:false %>' ToolTip="Modified in last 3 days." />
                                                    <div style="height: 6px; width: 6px; background-color: <%#Eval("WL_PRIORITY_COLOR").ToString()%>"
                                                        title="Priority: <%#Eval("PRIORITY").ToString()%>">
                                                    </div>
                                                </ItemTemplate>
                                                <ControlStyle Width="15px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Image ID="ImgJobAddedFrom" runat="server" ForeColor="Black" Height="16px"></asp:Image>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="Vessel">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblVesselShortName" runat="server" Text='<%#Eval("Vessel_Short_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ControlStyle Width="30px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                                HeaderText="Code">
                                                <HeaderTemplate>
                                                    <div style="white-space: nowrap">
                                                        <asp:LinkButton ID="lblWORKLIST_IDHeader" runat="server" CommandName="Sort" CommandArgument="WORKLIST_ID"
                                                            ForeColor="Black">Code&nbsp;</asp:LinkButton>
                                                        <img id="WORKLIST_ID" runat="server" visible="false" /></div>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lbljobcodegriditem" runat="server" Text='<%#Eval("WLID_DISPLAY") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ControlStyle Width="35px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Job Description" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <a href='ViewJob.aspx?OFFID=<%#Eval("OFFICE_ID") %>&WLID=<%#Eval("WORKLIST_ID") %>&VID=<%#Eval("VESSEL_ID") %>'
                                                        target="_blank" style="cursor: hand; color: Blue; text-decoration: none;">
                                                        <asp:Label ID="jd" runat="server" ToolTip='<%#Eval("JOB_DESCRIPTION")%>' Text='<%#Eval("JOB_DESCRIPTION").ToString().Length > 80 ?  Eval("JOB_DESCRIPTION").ToString().Substring(0, 80) + "..." : Eval("JOB_DESCRIPTION").ToString() %>'></asp:Label></a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="Assignor">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgrdAssignor" runat="server" Text='<%#Eval("AssignorName") %>'></asp:Label></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="PIC">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPIC" runat="server" Text='<%#Eval("USER_NAME") %>'></asp:Label></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="Date Raised">
                                                <HeaderTemplate>
                                                    <div style="white-space: nowrap">
                                                        <asp:LinkButton ID="lblDATE_RAISEDHeader" runat="server" CommandName="Sort" CommandArgument="DATE_RAISED"
                                                            ForeColor="Black">Date Raised&nbsp;</asp:LinkButton>
                                                        <img id="DATE_RAISED" runat="server" visible="false" /></div>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgrdRaisedDate" runat="server" Text='<%# Eval("DATE_RAISED","{0:dd/MMM/yy}").ToString() == "01/01/1900" ? "" : Eval("DATE_RAISED","{0:d/MMM/yy}")  %>'></asp:Label></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="Office Dept">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgrdofficeDept" runat="server" Text='<%#Eval("INOFFICE_DEPT") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="Vessel Dept">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgrdVesselDept" runat="server" Text='<%#Eval("ONSHIP_DEPT") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                                HeaderText="Expected Compln">
                                                <HeaderTemplate>
                                                    <div style="white-space: nowrap">
                                                        <asp:LinkButton ID="lblDATE_ESTMTD_CMPLTNDHeader" runat="server" CommandName="Sort"
                                                            CommandArgument="DATE_ESTMTD_CMPLTN" ForeColor="Black">Expected Completion&nbsp;</asp:LinkButton>
                                                        <img id="DATE_ESTMTD_CMPLTN" runat="server" visible="false" /></div>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDATE_ESTMTD_CMPLTN" runat="server" Text='<%# Eval("DATE_ESTMTD_CMPLTN","{0:dd/MMM/yy}").ToString() == "01/01/1900" ? "" : Eval("DATE_ESTMTD_CMPLTN","{0:d/MMM/yy}") %>'></asp:Label></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                                HeaderText="Completed">
                                                <HeaderTemplate>
                                                    <div style="white-space: nowrap">
                                                        <asp:LinkButton ID="lblDATE_COMPLETEDHeader" runat="server" CommandName="Sort" CommandArgument="DATE_COMPLETED"
                                                            ForeColor="Black">Completed&nbsp;</asp:LinkButton>
                                                        <img id="DATE_COMPLETED" runat="server" visible="false" /></div>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgrdCompletedOn" runat="server" Text='<%# Eval("DATE_COMPLETED","{0:dd/MMM/yy}").ToString() == "01/01/1900" ? "" : Eval("DATE_COMPLETED","{0:d/MMM/yy}") %>'></asp:Label></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                                HeaderText="Type">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgrdNCR" runat="server" Text='<%#Eval("Worklist_Type_Display").ToString()%>'></asp:Label></ItemTemplate>
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
                                                HeaderText="FollowUps">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imgRemarks" runat="server" ImageUrl='~/Images/remark.gif' CssClass="job-remarks"
                                                        CommandName="ADD_FOLLOWUP" CommandArgument='<%#Eval("VESSEL_ID").ToString() + "," + Eval("WORKLIST_ID").ToString()  + "," + Eval("OFFICE_ID").ToString() + "," + Eval("WORKLIST_STATUS").ToString() %>'>
                                                    </asp:ImageButton>
                                                </ItemTemplate>
                                                <ItemStyle Width="20px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                                HeaderText="">
                                                <ItemTemplate>
                                                    <a href='AddNewJob.aspx?OFFID=<%#Eval("OFFICE_ID")%>&WLID=<%#Eval("WORKLIST_ID")%>&VID=<%#Eval("VESSEL_ID")%>'
                                                        target="_blank">
                                                        <asp:Image ID="imgEdit" runat="server" Height="16px" Width="16px" ImageUrl='~/Images/edit.gif'
                                                            ToolTip="Edit job details" Visible='<%#(Eval("WORKLIST_ID").ToString() != "0" && (Eval("DATE_COMPLETED","{0:dd/MMM/yy}").ToString() == "01/01/1900" ||Eval("DATE_COMPLETED","{0:dd/MMM/yy}").ToString() == ""))?true:false %>' />
                                                    </a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                                HeaderText="">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imgEmail" runat="server" ImageUrl="~/Images/EMail.png" CommandName="EMailJob"
                                                        ToolTip="Send job details via email" CommandArgument='<%#Eval("OFFICE_ID").ToString()+ "," + Eval("WORKLIST_ID").ToString() + "," + Eval("VESSEL_ID").ToString()%>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
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
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImgDelete" runat="server" Text="Delete" OnCommand="ProgramDelete"
                                                        OnClientClick="JavaScript:return confirm('Are you sure, you want to delete the Worklist Job?');"
                                                        CommandArgument='<%#Eval("WORKLIST_ID")+";"+Eval("VESSEL_ID").ToString()+";"+Eval("OFFICE_ID").ToString() %>'
                                                        ForeColor="Black" ToolTip="Remove Item" ImageUrl="~/Images/delete.png" Height="16px">
                                                    </asp:ImageButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Image ID="ImgLocations" runat="server" ForeColor="Black" ImageUrl="~/Images/location.gif"
                                                        Height="16px"></asp:Image>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Function_Text" HeaderText="Function_Text" Visible="false" />
                                            <asp:BoundField DataField="Location_Text" HeaderText="Location_Text" Visible="false" />
                                            <asp:BoundField DataField="Sub_Location_Text" HeaderText="Sub_Location_Text" Visible="false" />
                                        </Columns>
                                        <EmptyDataTemplate>
                                            <label id="Label1" runat="server">
                                                No jobs found !!</label>
                                        </EmptyDataTemplate>
                                        <HeaderStyle CssClass="HeaderStyle-css Crew-HeaderStyle-css" />
                                        <PagerStyle CssClass="PagerStyle-css" />
                                        <%--<RowStyle CssClass="RowStyle-css" />--%>
                                        <AlternatingRowStyle CssClass="AlternatingRowStyle-css" Font-Size="12px" />
                                        <SelectedRowStyle CssClass="SelectedRowStyle-css" />
                                    </asp:GridView>
                                    <auc:CustomPager ID="ucCustomPagerctp" OnBindDataItem="Search_Worklist" AlwaysGetRecordsCount="true"
                                        RecordCountCaption="Total Jobs" runat="server" />
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="ImgExportToExcel" />
            <asp:PostBackTrigger ControlID="imgExpWithoutImg" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <div id="divCategory" style="display: none; border: 2px solid gray; background-color: #E0E0E0;
                height: 400px; position: absolute; width: 700px; left: 25%; top: 15%; z-index: 2;
                color: black">
                <table width="100%">
                    <tr>
                        <td colspan="4" style="font-size: 12px; text-align: center; background-color: #555;
                            color: White; padding: 5px;">
                            Category Selection
                            <input type="hidden" runat="server" id="hdnFlagCheck" value="false" />
                            <input type="hidden" runat="server" id="hdnNature" value="0" />
                            <input type="hidden" runat="server" id="hdnPrimary" value="0" />
                            <input type="hidden" runat="server" id="hdnSecondary" value="0" />
                            <input type="hidden" runat="server" id="hdnMinor" value="0" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 25%" align="left">
                            <asp:TextBox ID="txtNature" runat="server" Width="150px" Enabled="false"></asp:TextBox>
                        </td>
                        <td style="width: 25%" align="left">
                            <asp:TextBox ID="txtPrimary" runat="server" Width="150px" Enabled="false"></asp:TextBox>
                        </td>
                        <td style="width: 25%" align="left">
                            <asp:TextBox ID="txtSecondary" runat="server" Width="150px" Enabled="false"></asp:TextBox>
                        </td>
                        <td style="width: 25%" align="left">
                            <asp:TextBox ID="txtMinor" runat="server" Width="150px" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="font-size: 14px; height: 22px; width: 25%">
                            Nature:
                        </td>
                        <td align="left" style="font-size: 14px; height: 22px; width: 25%">
                            Primary:
                        </td>
                        <td align="left" style="font-size: 14px; height: 22px; width: 25%">
                            Secondary :
                        </td>
                        <td align="left" style="font-size: 14px; height: 22px; width: 25%">
                            Minor:
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 25%" align="left">
                            <asp:ListBox ID="lbNature" runat="server" Width="160px" Height="280px" AutoPostBack="true"
                                OnSelectedIndexChanged="lbNature_SelectedIndexChanged"></asp:ListBox>
                        </td>
                        <td style="width: 25%" align="left">
                            <asp:ListBox ID="lbPrimary" runat="server" Width="160px" Height="280px" AutoPostBack="true"
                                OnSelectedIndexChanged="lbPrimary_SelectedIndexChanged"></asp:ListBox>
                        </td>
                        <td style="width: 25%" align="left">
                            <asp:ListBox ID="lbSecondary" runat="server" Width="160px" Height="280px" AutoPostBack="true"
                                OnSelectedIndexChanged="lbSecondary_SelectedIndexChanged"></asp:ListBox>
                        </td>
                        <td style="width: 25%" align="left">
                            <asp:ListBox ID="lbMinor" runat="server" Width="160px" Height="280px" AutoPostBack="true"
                                OnSelectedIndexChanged="lbMinor_SelectedIndexChanged"></asp:ListBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="right">
                            <asp:Button ID="btnSelectAndClose" Text="Select And Close" runat="server" OnClientClick="JavaScript:SetAndClose();"
                                OnClick="btnSelectAndClose_OnClick" />
                        </td>
                        <td style="width: 25%" align="right">
                            <input type="button" id="btnCloseMe" value="Cancel" onclick="JavaScript:CloseMe()" />
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div id="dvAddFollowUp" title="Add Follow-Up" style="display: none; width: 500px;">
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>
                <asp:HiddenField ID="hdnOfficeID" runat="server" />
                <asp:HiddenField ID="hdnWorklistlID" runat="server" />
                <asp:HiddenField ID="hdnVesselID" runat="server" />
                <table width="100%" cellpadding="0" cellspacing="5">
                    <tr>
                        <td style="text-align: left">
                            Date:
                        </td>
                        <td>
                            <asp:TextBox ID="txtFollowupDate" runat="server" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            Message:
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:TextBox ID="txtMessage" runat="server" TextMode="MultiLine" Height="200px" Width="480px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center">
                            <asp:Button ID="btnSaveFollowUpAndClose" Text="Save And Close" runat="server" OnClientClick="hideModal('dvAddFollowUp');"
                                OnClick="btnSaveFollowUpAndClose_OnClick" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div id="dialog" title="Follow-ups" style="top: 0px; left: 0px; width: 600px; display: none;
        position: absolute;">
        Loading Data ...
        <iframe id="iframeFollowups" style="width: 100%; height: 100%; border: 0px;"></iframe>
    </div>
</asp:Content>
