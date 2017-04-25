<%@ Page Title="Crew Dashboard" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="Crew_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="../scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/wdScrollTab/docs/css/dbx.js" type="text/javascript"></script>
    <script src="../Scripts/AsyncResponse.js" type="text/javascript"></script>
    <script src="../Scripts/InterviewSchedule_DataHandler.js" type="text/javascript"></script>
    <script src="../Scripts/DocExpiry_DataHandler.js" type="text/javascript"></script>
    <script src="../Scripts/CrewChangeEvent_DataHandler.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.tools.min.js" type="text/javascript"></script>
    <script src="../Scripts/AsyncUserSessionLog_DataHandler.js" type="text/javascript"></script>
    <script src="../Scripts/CrewContract_DataHandler.js" type="text/javascript"></script>
    <link href="../Scripts/wdScrollTab/docs/css/dbx.css" rel="stylesheet" type="text/css" />
    <link href="sitemapstyler/sitemapstyler.css" rel="stylesheet" type="text/css" media="screen" />
    <script type="text/javascript" src="sitemapstyler/sitemapstyler.js"></script>
    <style type="text/css">
        ul.topnav
        {
            list-style: none;
            padding: 0 0px;
            margin: 0;
            float: left;
            width: 100%;
            font-size: 11px;
            border: 0;
        }
        ul.topnav li
        {
            float: left;
            margin: 0;
            padding: 0 0 0 0; /*--position: relative; Declare X and Y axis base for sub navigation--*/
        }
        ul.topnav li a
        {
            padding: 2px 2px;
            color: #333;
            display: block;
            text-decoration: none;
            float: left;
        }
        ul.topnav li a:hover
        {
            background: url(../Images/img02.jpg) no-repeat center top;
        }
        ul.topnav li span
        {
            /*--Drop down trigger styles--*/
            width: 17px;
            height: 35px;
            float: left;
        }
        ul.topnav li span.subhover
        {
            background-position: center bottom;
            cursor: pointer;
        }
        /*--Hover effect for trigger--*/
        ul.topnav li ul.subnav
        {
            list-style: none; /*--position: absolute; Important - Keeps subnav from affecting main navigation flow--*/
            left: 0;
            top: 35px;
            margin: 0;
            padding: 0;
            display: block;
            float: left;
        }
        ul.topnav li ul.subnav li
        {
            margin: 0;
            padding: 0; /*--border-top: 1px solid #252525; Create bevel effect--*/ /*--border-bottom: 1px solid #444; Create bevel effect--*/
            clear: both;
        }
        html ul.topnav li ul.subnav li a
        {
            float: left;
            background: #fff url(dropdown_linkbg.gif) no-repeat 10px center;
            padding-left: 10px;
            width: 180px;
        }
        html ul.topnav li ul.subnav li a:hover
        {
            background: #444 url(../Images/img02.jpg) repeat-x 10px center;
            color: #fff;
        }
        .tooltip
        {
            display: none;
            background: transparent url(../Images/black_arrow.png);
            font-size: 12px;
            height: 70px;
            width: 160px;
            padding: 25px;
            color: #fff;
        }
        .fixed-header
        {
            top: 0px;
            background-color: #5D7B9D;
            color: White;
        }
        .interview-schedule-table
        {
            padding: 0;
            border-collapse: collapse;
            width: 100%;
            color: Black;
        }
        .interview-schedule-header
        {
            background-color: #D8D8D8;
            color: Black;
        }
        
        .interview-schedule-table td
        {
            border: 1px solid #BDBDBD;
            padding: 2px;
            font-size: 11px;
        }
        
        .interview-schedule-table .icon
        {
            border: 0px solid gray;
            height: 16px;
            width: 16px;
            margin-top: 2px;
            background: url(../Images/Interview_1.png) no-repeat;
        }
        .interview-schedule-table .Candidate
        {
            background-color: #cfdfef;
            padding: 0 5px 0 5px;
        }
        .interview-schedule-table .PlanDate
        {
            background-color: #cfdfef;
            padding: 0 5px 0 5px;
        }
        .Overdue
        {
            background-color: Red;
            color: yellow;
            font-weight: bold;
        }
        .Due
        {
            background-color: yellow;
            color: red;
            font-weight: bold;
        }
        .gradiant-css-blue
        {
            filter: progid:DXImageTransform.Microsoft.Gradient(GradientType=0,StartColorStr='#CEE3F6',EndColorStr='#99BBE6');
            background: -webkit-gradient(linear, left top, left bottom, from(#CEE3F6), to(#99BBE6));
            background: -moz-linear-gradient(top,  #CEE3F6,  #99BBE6);
            color: Black;
        }
        .data-table
        {
            padding: 0;
            border-collapse: collapse;
            width: 100%;
            color: Black;
        }
        .data-table-header
        {
            background-color: #D8D8D8;
            color: Black;
        }
        .data-table td
        {
            border: 1px solid #BDBDBD;
            padding: 2px;
            font-size: 11px;
        }
        .callout
        {
            position:absolute;right:0px;width:32px;height:20px;background:transparent;background-image:url('../images/callout.png');padding-top:4px;padding-left:4px;
        }
    </style>
    <style id="worldclock" type="text/css">
        .digiClock
        {
            background-image: url(../images/header-columns-bg.gif);
            background-repeat: repeat-x;
            border: 1px solid #dfdfdf;
            text-align: center;
            padding: 2px;
            width: 173px;
            color: #000;
            margin: 2px;
            font-family: Tahoma;
            font-size: 12px;
        }
        .digiClock div
        {
            text-align: center;
            padding-top: 5px;
            margin-top: 5px;
            white-space: nowrap;
            color: #888;
        }
        .digiClock div div
        {
            background-image: url(../images/low_large.png);
            background-position: center;
            background-repeat: no-repeat;
            height: 22px;
            text-align: center;
            padding: 4px 0 0 0;
            white-space: nowrap;
            color: #fff;
        }
    </style>
    <script type="text/javascript">
        window.onload = function () {
            //initialise the docking boxes manager
            var manager = new dbxManager('main'); 	//session ID [/-_a-zA-Z0-9/]

            //create new docking boxes group
            var sidebar1 = new dbxGroup(
		    'sidebar1', 		// container ID [/-_a-zA-Z0-9/]
		    'vertical', 		// orientation ['vertical'|'horizontal']
		    '7', 			// drag threshold ['n' pixels]
		    'yes', 		// restrict drag movement to container axis ['yes'|'no']
		    '10', 			// animate re-ordering [frames per transition, or '0' for no effect]
		    'yes', 			// include open/close toggle buttons ['yes'|'no']
		    'open', 		// default state ['open'|'closed']

		    'Maximize', 		// word for "open", as in "open this box"
		    'Minimize', 		// word for "close", as in "close this box"
		    'click-down and drag to move this box', // sentence for "move this box" by mouse
		    'click to %toggle% this box', // pattern-match sentence for "(open|close) this box" by mouse
		    'use the arrow keys to move this box', // sentence for "move this box" by keyboard
		    ', or press the enter key to %toggle% it',  // pattern-match sentence-fragment for "(open|close) this box" by keyboard
		    '%mytitle%  [%dbxtitle%]' // pattern-match syntax for title-attribute conflicts
		    );

            var sidebar2 = new dbxGroup(
		    'sidebar2', 		// container ID [/-_a-zA-Z0-9/]
		    'vertical', 		// orientation ['vertical'|'horizontal']
		    '7', 			// drag threshold ['n' pixels]
		    'no', 		// restrict drag movement to container axis ['yes'|'no']
		    '10', 			// animate re-ordering [frames per transition, or '0' for no effect]
		    'yes', 			// include open/close toggle buttons ['yes'|'no']
		    'open', 		// default state ['open'|'closed']

		    'Maximize', 		// word for "open", as in "open this box"
		    'Minimize', 		// word for "close", as in "close this box"
		    'click-down and drag to move this box', // sentence for "move this box" by mouse
		    'click to %toggle% this box', // pattern-match sentence for "(open|close) this box" by mouse
		    'use the arrow keys to move this box', // sentence for "move this box" by keyboard
		    ', or press the enter key to %toggle% it',  // pattern-match sentence-fragment for "(open|close) this box" by keyboard
		    '%mytitle%  [%dbxtitle%]' // pattern-match syntax for title-attribute conflicts
		    );

        };

        $(document).ready(function () {
            $('[id$=chk_box0]').bind('click', function () { if (!($(this)).is(':checked')) { $('#leftbox1').hide(); } else { $('#leftbox1').show(); } });
            $('[id$=chk_box1]').bind('click', function () { if (!($(this)).is(':checked')) { $('#midbox1').hide(); } else { $('#midbox1').show(); } });
            $('[id$=chk_box2]').bind('click', function () { if (!($(this)).is(':checked')) { $('#midbox2').hide(); } else { $('#midbox2').show(); } });
            $('[id$=chk_box3]').bind('click', function () { if (!($(this)).is(':checked')) { $('#midbox3').hide(); } else { $('#midbox3').show(); } });
            $('[id$=chk_box4]').bind('click', function () { if (!($(this)).is(':checked')) { $('#midbox4').hide(); } else { $('#midbox4').show(); } });
            $('[id$=chk_box5]').bind('click', function () { if (!($(this)).is(':checked')) { $('#midbox5').hide(); } else { $('#midbox5').show(); } });
            $('[id$=chk_box6]').bind('click', function () { if (!($(this)).is(':checked')) { $('#midbox6').hide(); } else { $('#midbox6').show(); } });
            $('[id$=chk_box7]').bind('click', function () { if (!($(this)).is(':checked')) { $('#midbox7').hide(); } else { $('#midbox7').show(); } });


            if (!$('[id$=chk_box0]').is(':checked')) { $('#leftbox1').hide(); }
            if (!$('[id$=chk_box1]').is(':checked')) { $('#midbox1').hide(); }
            if (!$('[id$=chk_box2]').is(':checked')) { $('#midbox2').hide(); }
            if (!$('[id$=chk_box3]').is(':checked')) { $('#midbox3').hide(); }
            if (!$('[id$=chk_box4]').is(':checked')) { $('#midbox4').hide(); }
            if (!$('[id$=chk_box5]').is(':checked')) { $('#midbox5').hide(); }
            if (!$('[id$=chk_box6]').is(':checked')) { $('#midbox6').hide(); }
            if (!$('[id$=chk_box7]').is(':checked')) { $('#midbox7').hide(); }


            setTimeout(load_PendingInterviewList, 100);
            setTimeout(load_DocumentExpiryList, 200);
            setTimeout(load_CrewChangeAlerts, 300);
            setTimeout(load_CrewCpmplaints, 400);
            setTimeout(load_CrewUSVisaAlerts, 500);
            setTimeout(load_Contract_ToSign_Alerts, 600);
            setTimeout(load_Contract_ToVerify_Alerts, 700);

            Timer();


            $('#imgReloadDocumentExpiryList').bind('click', function () {
                setTimeout(load_DocumentExpiryList, 200);
            });


            sitemapstyler();
        });

        function Timer() {

            Get_UserSessionLog();

            setTimeout(Timer, 10000);

        }
        function Get_UserSessionLog() {
            var UserID = $('[id$=hdnUserID]').val();
            Async_getUserSessionLog(UserID);
        }
        function load_PendingInterviewList() {
            var UserID = $('[id$=hdnUserID]').val();
            Async_getPendingInterviewList(UserID);
        }
        function load_DocumentExpiryList() {
            var UserID = $('[id$=hdnUserID]').val();
            Async_getDocumentExpiryList(UserID);
        }
        function load_CrewChangeAlerts() {
            var UserID = $('[id$=hdnUserID]').val();
            Async_getCrewChangeAlerts(UserID);
        }
        function load_CrewCpmplaints() {
            var UserID = $('[id$=hdnUserID]').val();
            var url = "CrewComplaintList.aspx?userid=" + UserID + "&rnd=" + Math.random();
            $.get(url, function (data) { $('#dvCrewComplaints').html(data); });
        }
        function showEscalationLog(wlid, vid, userid) {

            var oMe = window.event.srcElement
            var check = oMe.src.toLowerCase();
            var url = "CrewComplaintLog.aspx?wlid=" + wlid + "&vid=" + vid + "&userid=" + userid + "&rnd=" + Math.random();

            if (check.lastIndexOf("plus.png") != -1) {
                oMe.src = "../images/Minus.png"
                $('.' + wlid).show();
                $.get(url, function (data) { $('#dvLog' + wlid).html(data); });
            }
            else {
                oMe.src = "../images/Plus.png"
                $('.' + wlid).hide();
            }
        }
        function load_CrewUSVisaAlerts() {
            var UserID = $('[id$=hdnUserID]').val();
            Async_getUSVisaAlerts(UserID);
        }

        function load_Contract_ToSign_Alerts() {
            var UserID = $('[id$=hdnUserID]').val();
            Async_getContract_ToSign_Alerts(UserID);
        }

        function load_Contract_ToVerify_Alerts() {
            var UserID = $('[id$=hdnUserID]').val();
            Async_getContract_ToVerify_Alerts(UserID);
        }

        
    </script>
    <script id="js_USVisaAlert_Helper" type="text/javascript">

        function appendRow(tbody, cssClass) {
            try {
                var tr = document.createElement('tr');
                if (cssClass)
                    tr.className = cssClass;
                tbody.appendChild(tr);
            }
            catch (ex) { }
            return tr;
        }
        function appendCell(tr, html, title, cssClass) {
            try {
                var td = document.createElement('td');
                if (html)
                    td.innerHTML = html;

                if (title)
                    td.title = title;

                if (cssClass) {
                    td.className = cssClass;
                }

                tr.appendChild(td);
            }
            catch (ex) { }
            return td;
        }
        function Async_getUSVisaAlerts(userid) {
            var url = "../webservice.asmx/getUSVisaAlerts";
            var params = 'userid=' + userid;

            obj = new AsyncResponse(url, params, response_getUSVisaAlerts);
            obj.getResponse();
        }
        function response_getUSVisaAlerts(retval) {
            var ar, arS;

            if (retval.indexOf('Working') >= 0) { return; }
            try {

                retval = clearXMLTags(retval);
                if (retval.indexOf('ERROR:', 0) >= 0) {
                    alert(retval);
                    return;
                }

                if (retval.trim().length > 0) {
                    //alert(retval);
                    document.getElementById('dvUSVisaAlert').innerHTML = "";
                    var arVal = eval(retval);

                    var t = document.createElement('table');
                    var tb = document.createElement('tbody');
                    t.className = "data-table";

                    var th = appendRow(tb, 'data-table-header');

                    appendCell(th, 'Vessel');
                    appendCell(th, 'Staff Code');
                    appendCell(th, 'Staff Name');
                    appendCell(th, 'Rank');
                    appendCell(th, 'US Visa Number');
                    appendCell(th, 'US Visa Expiry Date');
                    appendCell(th, 'US Visa Status');


                    tb.appendChild(th);

                    for (var i = 0; i < arVal.length; i++) {
                        var lstItem = arVal[i];
                        var tr = appendRow(tb);
                        var sLink = "";

                        appendCell(tr, lstItem.Vessel_Short_Name);
                        appendCell(tr, "<a href='../Crew/CrewDetails.aspx?ID=" + lstItem.CrewID + "' target='_blank'>" + lstItem.Staff_Code + "</a>");
                        appendCell(tr, lstItem.Staff_FullName);
                        appendCell(tr, lstItem.Rank_Short_Name);
                        appendCell(tr, lstItem.Us_Visa_Number);
                        appendCell(tr, lstItem.Us_Visa_Expiry, null, lstItem.Overdue);
                        appendCell(tr, lstItem.Visa_Status, null, lstItem.Overdue);

                    }

                    t.appendChild(tb);

                    var dvRes = document.getElementById('dvUSVisaAlert');
                    dvRes.innerHTML = "";
                    dvRes.appendChild(t);
                    //alert(t.innerHTML);
                }
            }
            catch (ex) { alert(ex); }

        }
    
    
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:HiddenField ID="hdnUserID" runat="server" Value="0" />
    <table style="width: 100%;" cellpadding="0" cellspacing="5">
        <tr>
            <td style="vertical-align: top; width: 160px;">
                <div class="dbx-group" id="sidebar1">
                    <div id="leftbox0" class="dbx-box">
                        <h3 class="dbx-handle">
                            Quick Links&nbsp;</h3>
                        <div id="leftbox0_content" class="dbx-content">
                            <asp:Label ID="lblULMenu" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div id="leftbox1" class="dbx-box">
                        <h3 class="dbx-handle">
                            Users Online Status&nbsp;</h3>
                        <div id="leftbox1_content" class="dbx-content">
                            <div>
                                <div id="dvUserSessionLog">
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="rightbox1" class="dbx-box">
                        <h3 class="dbx-handle">
                            Settings&nbsp;</h3>
                        <div id="rightbox1_content" class="dbx-content">
                            <%--<embed src=http://flash-clocks.com/free-flash-clocks-blog-topics/free-flash-clock-183.swf width=200 height=200 wmode=transparent type=application/x-shockwave-flash></embed>--%>
                            <embed src="http://flash-clocks.com/free-flash-clocks-blog-topics/free-flash-clock-147.swf"
                                width="150" height="150" wmode="transparent" type="application/x-shockwave-flash"></embed>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <table style="text-align: left">
                                        <tr>
                                            <td>
                                                <asp:CheckBox ID="chk_box0" Checked="true" runat="server" Text="Users Online Status" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:CheckBox ID="chk_box1" Checked="true" runat="server" Text="Document Expiry Alerts" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:CheckBox ID="chk_box2" Checked="true" runat="server" Text="Crew Change Alerts" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:CheckBox ID="chk_box3" Checked="true" runat="server" Text="Pending Interview Alerts" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:CheckBox ID="chk_box4" Checked="true" runat="server" Text="Crew Complaint Alerts" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:CheckBox ID="chk_box5" Checked="true" runat="server" Text="US Visa Alerts" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:CheckBox ID="chk_box6" Checked="true" runat="server" Text="Contract Sign Alerts" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:CheckBox ID="chk_box7" Checked="true" runat="server" Text="Contract Verify Alerts" />
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </td>
            <td style="vertical-align: top;">
                <div class="dbx-group" id="sidebar2" style="width: 100%;">
                    <div id="midbox1" class="dbx-box widget-page" style="width: 100%;">
                        <h3 class="dbx-handle">
                            Document Expiry Alerts:&nbsp;</h3>
                        <div id="midbox1_content" class="dbx-content">
                            <div class="gradiant-css-blue dbx-content">
                                <table style="width: 100%">
                                    <tr>
                                        <td>
                                            Documents expiring in next 90 days or already expired:
                                        </td>
                                        <td style="text-align: right; padding-right: 5px;">
                                            <%--<img id="imgReloadDocumentExpiryList" src="../Images/refresh-icon.png" alt="Refresh" style="height:18px;cursor:hand;"/>--%>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div id="dvDocAlerts_Container" style="max-height: 300px; overflow: scroll;">
                                <div id="dvDocExpiryAlerts">
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="midbox2" class="dbx-box widget-page" style="width: 100%;">
                        <h3 class="dbx-handle">
                            Crew Change Alerts:&nbsp;</h3>
                        <div id="midbox2_content" class="dbx-content">
                            <div class="gradiant-css-blue dbx-content" style="padding: 2px">
                                Crew events planned for next 10 days:
                            </div>
                            <div id="dvCrewChangeAlerts" style="max-height: 300px; overflow: scroll;">
                            </div>
                        </div>
                    </div>
                    <div id="midbox3" class="dbx-box widget-page" style="width: 100%;">
                        <h3 class="dbx-handle">
                            Pending Interviews:&nbsp;</h3>
                        <div id="midbox3_content" class="dbx-content">
                            <div class="gradiant-css-blue dbx-content">
                                Interviewes scheduled but not yet taken:
                            </div>
                            <div style="max-height: 300px; overflow: auto;">
                                <div id="dvInterviewSchedules">
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="midbox4" class="dbx-box widget-page" style="width: 100%;">
                        <h3 class="dbx-handle">
                            Crew Complaints:&nbsp;</h3>
                        <div id="midbox4_content" class="dbx-content">
                            <div style="max-height: 300px; overflow: auto;">
                                <div id="dvCrewComplaints">
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="midbox5" class="dbx-box widget-page" style="width: 100%;">
                        <h3 class="dbx-handle">
                            US Visa Alert:&nbsp;</h3>
                        <div id="midbox5_content" class="dbx-content">
                            <div class="gradiant-css-blue dbx-content" style="padding: 2px">
                                List of crew with expired US VISA, or, with no data:
                            </div>
                            <div style="max-height: 300px; overflow: auto;">
                                <div id="dvUSVisaAlert">
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="midbox6" class="dbx-box widget-page" style="width: 100%;">
                        <h3 class="dbx-handle">
                            Contracts Pending To Be Signed by Office:&nbsp;</h3>
                        <div id="midbox6_content" class="dbx-content" style="padding: 2px">
                            <div class="gradiant-css-blue dbx-content">
                                Lilst of contracts pending to be digitally signed by Office
                            </div>
                            <div style="max-height: 300px; overflow: auto;">
                                <div id="dvContractToSign">
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="midbox7" class="dbx-box widget-page" style="width: 100%;">
                        <h3 class="dbx-handle">
                            Contracts Pending To Be Verified By Office:&nbsp;</h3>
                        <div id="midbox7_content" class="dbx-content">
                            <div class="gradiant-css-blue dbx-content" style="padding: 2px">
                                Lilst of contracts pending to be verified
                            </div>
                            <div style="max-height: 300px; overflow: auto;">
                                <div id="dvContractToVerify">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </td>
        </tr>
    </table>
    
</asp:Content>
